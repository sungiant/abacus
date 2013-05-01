// ┌────────────────────────────────────────────────────────────────────────┐ \\
// │ Abacus - Fast, efficient, cross precision, maths library               │ \\
// ├────────────────────────────────────────────────────────────────────────┤ \\
// │ Brought to you by:                                                     │ \\
// │          _________                    .__               __             │ \\
// │         /   _____/__ __  ____    ____ |__|____    _____/  |_           │ \\
// │         \_____  \|  |  \/    \  / ___\|  \__  \  /    \   __\          │ \\
// │         /        \  |  /   |  \/ /_/  >  |/ __ \|   |  \  |            │ \\
// │        /_______  /____/|___|  /\___  /|__(____  /___|  /__|            │ \\
// │                \/           \//_____/         \/     \/                │ \\
// │                                                                        │ \\
// ├────────────────────────────────────────────────────────────────────────┤ \\
// │ Copyright © 2013 A.J.Pook (http://sungiant.github.com)                 │ \\
// ├────────────────────────────────────────────────────────────────────────┤ \\
// │ Permission is hereby granted, free of charge, to any person obtaining  │ \\
// │ a copy of this software and associated documentation files (the        │ \\
// │ "Software"), to deal in the Software without restriction, including    │ \\
// │ without limitation the rights to use, copy, modify, merge, publish,    │ \\
// │ distribute, sublicense, and/or sellcopies of the Software, and to      │ \\
// │ permit persons to whom the Software is furnished to do so, subject to  │ \\
// │ the following conditions:                                              │ \\
// │                                                                        │ \\
// │ The above copyright notice and this permission notice shall be         │ \\
// │ included in all copies or substantial portions of the Software.        │ \\
// │                                                                        │ \\
// │ THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,        │ \\
// │ EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF     │ \\
// │ MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. │ \\
// │ IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY   │ \\
// │ CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,   │ \\
// │ TORT OR OTHERWISE, ARISING FROM,OUT OF OR IN CONNECTION WITH THE       │ \\
// │ SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                 │ \\
// └────────────────────────────────────────────────────────────────────────┘ \\


using System;
using NUnit.Framework;

namespace Sungiant.Abacus.Tests
{
    /*
    0 01111 0000000000 = 1
    0 01111 0000000001 = 1 + 2−10 = 1.0009765625 (next biggest float after 1)
    1 10000 0000000000 = −2

    0 11110 1111111111 = 65504  (max half precision)

    0 00001 0000000000 = 2−14 ≈ 6.10352 × 10−5 (minimum positive normal)
    0 00000 1111111111 = 2−14 - 2−24 ≈ 6.09756 × 10−5 (maximum subnormal)
    0 00000 0000000001 = 2−24 ≈ 5.96046 × 10−8 (minimum positive subnormal)

    0 00000 0000000000 = 0
    1 00000 0000000000 = −0

    0 11111 0000000000 = infinity
    1 11111 0000000000 = −infinity

    0 01101 0101010101 ≈ 0.33325... ≈ 1/3 
     */
    [TestFixture]
    public static class HalfUtilsTests
    {
        [Test]
        public static void TestCommonValues()
        {
            Single testStart = 1000f;

            while(testStart > -testStart)
            {
                TestPackToUnpack(0f, Single.Epsilon);

                testStart -= 0.01f;
            }
        }

        [Test]
        public static void TestAllPossibleHalfValues()
        {
            for (UInt16 packed = UInt16.MinValue; packed < UInt16.MaxValue; ++packed)  // 0 - 65535
            {
                TestUnpackToPack(packed);
            }
        }

        [Test]
        public static void TestZero()
        {
            TestPackToUnpack(0f, 0f);
        }

        static void TestPackToUnpack(Single input, Single epsilon)
        {
            UInt16 packed = HalfUtils.Pack(input);
            
            Single unpacked = HalfUtils.Unpack(packed);
            
            Assert.AreEqual(input, unpacked, epsilon);
        }

        static void TestUnpackToPack(UInt16 input)
        {
            Single unpacked = HalfUtils.Unpack(input);

            UInt16 packed = HalfUtils.Pack(unpacked);

            Assert.AreEqual(input, packed);
        }

        static void TestIntRange(int fromInt, int toInt, int multiple)
        { 
            for(int testInt = fromInt; testInt <= toInt; ++testInt)
            {
                UInt16 packed = HalfUtils.Pack((Single) testInt );
                int unpacked = (int) HalfUtils.Unpack(packed);

                int epsilon = 0;
                
                if(multiple != 0)
                {
                    int remainder = (testInt % multiple);

                    // WTF NUINT: Have to do this because nunit doesn't
                    //            seem to understand negative episilon values:
                    //
                    // Expected: -4095 +/- -1
                    // But was: -4096
                    //
                    epsilon = Math.Abs(remainder);
                }

                Assert.That(unpacked, Is.EqualTo( testInt ).Within( epsilon ) );
            }
        }

        [Test]
        public static void TestIntegerPrecision1()
        {
            // Integers between 0 and 2048 can be exactly represented
            TestIntRange(0, 2048, 0); // zero is tested elsewhere
            TestIntRange(-2048, 0, 0);
        }

        [Test]
        public static void TestIntegerPrecision2()
        {
            // Integers between 2049 and 4096 round to a multiple of 2 (even number)
            TestIntRange(2049, 4096, 2);
            TestIntRange(-4096, -2049, 2);
        }
        
        [Test]
        public static void TestIntegerPrecision3()
        {
            // Integers between 4097 and 8192 round to a multiple of 4
            TestIntRange(4097, 8192, 4);
            TestIntRange(-8192, -4097, 4);
        }
        
        [Test]
        public static void TestIntegerPrecision4()
        {
            // Integers between 8193 and 16384 round to a multiple of 8
            TestIntRange(8193, 16384, 8);
            TestIntRange(-16384, -8193, 8);
        }
        
        [Test]
        public static void TestIntegerPrecision5()
        {
            // Integers between 16385 and 32768 round to a multiple of 16
            TestIntRange(16385, 32768, 16);
            TestIntRange(-32768, -16385, 16);
        }
        
        [Test]
        public static void TestIntegerPrecision6()
        {
            // Integers between 32769 and 65519 round to a multiple of 32
            TestIntRange(32769, 65519, 32);
            TestIntRange(-65519, -32769, 32);
        }
    }
}
