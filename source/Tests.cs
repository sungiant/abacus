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
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Sungiant.Abacus.Tests
{

	[TestFixture]
    public class RealMaths
	{
	}

    [TestFixture]
    public class PackUtils
    {
    }
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
	/// <summary>
	///This is a test class for Fixed32Test and is intended
	///to contain all Fixed32Test Unit Tests
	///</summary>
	[TestFixture]
	public class Fixed32Tests
	{
		[Test]
		public void TestBigMultiply ()
		{
			Fixed32 a = Fixed32.MaxValue / 2;
			a -= 1;

			Assert.That((a * 2) + 1, Is.Not.EqualTo(Fixed32.MaxValue));


			/*
			long basevalue = 1000000;
			Fixed32 f = new Fixed32 (basevalue);
			Fixed32 g = new Fixed32 (basevalue);
			Fixed32 h = new Fixed32 (basevalue * basevalue);
			Assert.That (f * g, Is.EqualTo (h));

			// This is testing the test, really, to make sure 'basevalue' was
			// large enough to trigger overflow in the first place.
			Fixed32 bad_h = Fixed32.CreateFromRaw ((f.RawValue * g.RawValue) >> Fixed32.n);
			Assert.That (bad_h, Is.Not.EqualTo (h));
			 * 
			 */
		}

		[Test]
		public void TestMaxRange()
		{
			double max = System.Math.Pow(2.0, Fixed32.m) - System.Math.Pow(2.0, -Fixed32.n);
			Assert.That(Fixed32.MaxValue.ToDouble(), Is.EqualTo(max));
		}


		[Test]
		public void TestMinRange()
		{
			double min = -System.Math.Pow(2.0, Fixed32.m);
			Assert.That(Fixed32.MinValue.ToDouble(), Is.EqualTo(min));
		}

		[Test]
		public void TestResolution()
		{
			// we should be able to represent this exactly
			double res = (double)-System.Math.Pow(2.0, -Fixed32.n);
			Fixed32 f = (Fixed32) res;
			Assert.That((double)f, Is.EqualTo(res));
		
			// but not this
			double res2 = res / 2;
			Fixed32 f2 = (Fixed32) res2;
			Assert.That((double)f2, Is.Not.EqualTo(res2));

		}

		[Test]
		public void TestExtremeValueMultiply()
		{
			Assert.That((Fixed32.MaxValue - 10) * (Fixed32.MaxValue - 10), Is.EqualTo(Fixed32.MaxValue));
			Assert.That(Fixed32.MaxValue * Fixed32.MaxValue, Is.EqualTo(Fixed32.MaxValue));

			Assert.That(Fixed32.MinValue * Fixed32.MinValue, Is.EqualTo(Fixed32.MaxValue));

			Assert.That(Fixed32.MaxValue * Fixed32.MinValue, Is.EqualTo(Fixed32.MinValue));
		}


		/// <summary>
		/// Check that division doesn't get rounded to integers
		/// </summary>
		[Test]
		public void TestDivide ()
		{
			const long numerator = 1;
			const long denominator = 10;

			Fixed32 f = new Fixed32 (numerator);
			Fixed32 g = new Fixed32 (denominator);
			Fixed32 h = new Fixed32 (((double)numerator) / denominator);

			Assert.That (f / g, Is.EqualTo (h));
		}

		[Test]
		public void TestToDouble ()
		{
			double[] values = { 0.0, 1.0, 1.0 / 3, -1, -1.0 / 3 };

			foreach (double value in values) {
				Fixed32 f = new Fixed32 (value);
				double d = f.ToDouble ();
				double error = System.Math.Abs (d - value);
				Assert.That (error < 0.001);
			}
		}

		
		/// <summary>
		/// Make sure tiny multiplies work properly - these inputs are so small
		/// they underflow to zero
		/// </summary>
		[Test]
		public void TestTinyMultiply ()
		{
			int[] values = { 2, 1, 0, -1, -2 };

			foreach (int value in values) {
				Fixed32 f = Fixed32.CreateFromRaw (value);
				Fixed32 fsq = f * f;
				Assert.That (fsq, Is.EqualTo (new Fixed32(0)));
			}
		}
		

		/// <summary>
		/// Make sure signs are handled correctly during multiplication
		/// </summary>
		[Test]
		public void TestPosNegMultiply ()
		{

			Fixed32 one; global::Sungiant.Abacus.RealMaths.One(out one);
			Fixed32 negOne = -one;

			Assert.That (one * one, Is.EqualTo (one));
			Assert.That (negOne * one, Is.EqualTo (negOne));
			Assert.That (one * negOne, Is.EqualTo (negOne));
			Assert.That (negOne * negOne, Is.EqualTo (one));
		}

		
		[Test]
		public void TestSmallMultiply ()
		{
			double[] values = { 0.9, 0.5, 0.1, 0.01, 0.001, -0.001, -0.01, -0.1, -0.5, -0.9 };

			foreach (double value in values) {
				Fixed32 f = new Fixed32 (value);
				Fixed32 fsq = f * f;

				// Actually we tolerate a slight rounding error here.  We shouldn't have to, but it's not crucial to fix it.
				long diff = System.Math.Abs (new Fixed32 (value * value).RawValue - fsq.RawValue);
				Assert.That (diff < 2);
			}
		}
		
		[Test]
		public void TestToString ()
		{
			Assert.That (new Fixed32 (0).ToString (), Is.EqualTo ("0"));
			Assert.That (new Fixed32 (3.03125).ToString (), Is.EqualTo ("3.03125"));
			Assert.That (string.Format ("{0:0.000}", new Fixed32 (3.141593)), Is.EqualTo ("3.142"));
		}

		[Test]
		public void TestConstructFromString ()
		{
            Fixed32 zero; global::Sungiant.Abacus.RealMaths.Zero(out zero);
            Fixed32 one; global::Sungiant.Abacus.RealMaths.One(out one);
			Assert.That (Fixed32.Parse("0"), Is.EqualTo (zero));
			Assert.That (Fixed32.Parse("-0"), Is.EqualTo (zero));
			Assert.That (Fixed32.Parse("0."), Is.EqualTo (zero));
			Assert.That (Fixed32.Parse("0.0"), Is.EqualTo (zero));
			Assert.That (Fixed32.Parse("0.00"), Is.EqualTo (zero));

			Assert.That (Fixed32.Parse("1"), Is.EqualTo (one));
			Assert.That (Fixed32.Parse("1."), Is.EqualTo (one));
			Assert.That (Fixed32.Parse("1.0"), Is.EqualTo (one));
			Assert.That (Fixed32.Parse("-1"), Is.EqualTo (new Fixed32 (-1)));
			Assert.That (Fixed32.Parse("-1."), Is.EqualTo (new Fixed32 (-1)));
			Assert.That (Fixed32.Parse("-1.0"), Is.EqualTo (new Fixed32 (-1)));

			Assert.That (Fixed32.Parse("0.500"), Is.EqualTo (one / new Fixed32(2)));
			Assert.That (Fixed32.Parse("-0.500"), Is.EqualTo (-one / new Fixed32(2)).Within(Fixed32.Epsilon));

			Assert.That (Fixed32.Parse("-0.005"), Is.EqualTo (-one / new Fixed32(200)));
			Assert.That (Fixed32.Parse("-0.344999432563782"), Is.EqualTo (new Fixed32 (-0.344999432563782d)));
			Assert.That (Fixed32.Parse("0.938602924346924"), Is.EqualTo (new Fixed32 (0.938602924346924d)));

			Assert.That (Fixed32.Parse("100.001"), Is.EqualTo (new Fixed32 (100) + one / new Fixed32(1000)));

			
		}

		[Test]
		public void TestSin ()
		{
			double t = 0.1;

			Fixed32 zero = 0;
			Fixed32 one = 1;
			Fixed32 two = 2;
			Fixed32 three = 3;
            Fixed32 pi; global::Sungiant.Abacus.RealMaths.Pi(out pi);
            Fixed32 tau; global::Sungiant.Abacus.RealMaths.Tau(out tau);

			double p1 = Fixed32.Sin(zero).ToDouble();
			double e1 = zero.ToDouble();
			Assert.That(p1, Is.EqualTo(e1).Within(t));

			double p2 = Fixed32.Sin(pi / two).ToDouble();
			double e2 = one.ToDouble();
			Assert.That(p2, Is.EqualTo(e2).Within(t));

			double p3 = Fixed32.Sin(pi).ToDouble();
			double e3 = zero.ToDouble();
			Assert.That(p3, Is.EqualTo(e3).Within(t));

			double p4 = Fixed32.Sin(three * pi / two).ToDouble();
			double e4 = -one.ToDouble();
			Assert.That(p4, Is.EqualTo(e4).Within(t));

			double p5 = Fixed32.Sin(tau).ToDouble();
			double e5 = zero.ToDouble();
			Assert.That(p5, Is.EqualTo(e5).Within(t));

			for (float f = 0.0f; f < System.Math.PI * 2.0f; f += 0.1f)
			{
				Assert.That(System.Math.Sin(f), Is.EqualTo(Fixed32.Sin(new Fixed32(f)).ToDouble()).Within(t));
			}
		}

		[Test]
		public void TestCos ()
		{
			Fixed32 zero = 0;
			Fixed32 one = 1;
			Fixed32 two = 2;
			Fixed32 three = 3;
            Fixed32 pi; global::Sungiant.Abacus.RealMaths.Pi(out pi);
            Fixed32 tau; global::Sungiant.Abacus.RealMaths.Tau(out tau);

			Assert.That (Fixed32.Cos (zero), Is.EqualTo (one));
			Assert.That (Fixed32.Cos (pi / two), Is.EqualTo (zero));
			Assert.That (Fixed32.Cos (pi), Is.EqualTo (-one));
			Assert.That(Fixed32.Cos(three * pi / two), Is.EqualTo(zero));
			Assert.That (Fixed32.Cos (tau), Is.EqualTo (one));

			const float epsilon = 0.0001f;
			for (float f = 0.0f; f < System.Math.PI * 2.0f; f += 0.1f) {
				float singleResult = (float) System.Math.Cos(f);
				float fixedResult = Fixed32.Cos(new Fixed32(f)).ToSingle();

				Assert.That(
					singleResult, 
					Is.EqualTo(fixedResult).Within(epsilon),
					string.Format("was: {0}, should have been: {1}", fixedResult, singleResult));
			}
		}
	}


}

namespace Sungiant.Abacus.Packed.Tests
{
	public static class Settings
    {
        public const Int32 NumTests = 50000;
    }

    [TestFixture]
    public static class Alpha_8Tests
    {
        [Test]
        public static void TestAllPossibleValues()
        {
            for (Byte packed = Byte.MinValue; packed < Byte.MaxValue; ++packed)
            {
                var packedObj = new Alpha_8();

                packedObj.PackedValue = packed;

                Single unpacked;

                packedObj.UnpackTo(out unpacked);

                var newPackedObj = new Alpha_8(unpacked);

                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }

    [TestFixture]
    public class Bgr_5_6_5Tests
    {
        [Test]
        public static void TestAllPossibleValues()
        {
            for (UInt16 packed = UInt16.MinValue; packed < UInt16.MaxValue; ++packed)
            {
                var packedObj = new Bgr_5_6_5();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector3 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Bgr_5_6_5(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Bgra16Tests
    {
        [Test]
        public static void TestAllPossibleValues()
        {
            for (UInt16 packed = UInt16.MinValue; packed < UInt16.MaxValue; ++packed)
            {
                var packedObj = new Bgra16();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Bgra16(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Bgra_5_5_5_1Tests
    {
        [Test]
        public static void TestAllPossibleValues()
        {
            for (UInt16 packed = UInt16.MinValue; packed < UInt16.MaxValue; ++packed)
            {
                var packedObj = new Bgra_5_5_5_1();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);

                var newPackedObj = new Bgra_5_5_5_1(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Byte4Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];

            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);

                var packedObj = new Byte4();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Byte4(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class NormalisedByte2Tests
    {
        [Test]
        public static void TestAllPossibleValues()
        {
            for (UInt16 packed = UInt16.MinValue; packed < UInt16.MaxValue; ++packed)
            {
                var packedObj = new NormalisedByte2();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector2 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new NormalisedByte2(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class NormalisedByte4Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);
                var packedObj = new NormalisedByte4();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new NormalisedByte4(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class NormalisedShort2Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);
                var packedObj = new NormalisedShort2();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector2 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new NormalisedShort2(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Rg32Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);
                var packedObj = new Rg32();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector2 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Rg32(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Rgba32Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);
                var packedObj = new Rgba32();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Rgba32(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Rgba64Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[8];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt64(buff, 0);
                UInt64 packed = BitConverter.ToUInt64(buff, 0);
                
                var packedObj = new Rgba64();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Rgba64(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Rgba_10_10_10_2Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);
                var packedObj = new Rgba_10_10_10_2();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Rgba_10_10_10_2(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Short2Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);
                var packedObj = new Short2();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector2 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Short2(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Short4Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[8];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt64(buff, 0);

                UInt64 packed = BitConverter.ToUInt64(buff, 0);
                
                var packedObj = new Short4();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Short4(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }

}


namespace Sungiant.Abacus.HalfPrecision.Tests
{
	[TestFixture]
	public class Matrix44Tests
	{
		[Test]
		public void TestTranspose_MemberFn ()
		{
			Matrix44 startMatrix = new Matrix44(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

			Matrix44 testMatrix = startMatrix;

			Matrix44 testMatrixExpectedTranspose = new Matrix44(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

			testMatrix.Transpose();

			Assert.That(testMatrix, Is.EqualTo(testMatrixExpectedTranspose));
		}

		[Test]
		public void TestTranspose_StaticFn ()
		{
			Matrix44 startMatrix = new Matrix44(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

			Matrix44 testMatrix = startMatrix;

			Matrix44 testMatrixExpectedTranspose = new Matrix44(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

			// RUN THE STATIC VERSION OF THE FUNCTION
			Matrix44 resultMatrix = Matrix44.Identity;

			Matrix44.Transpose(ref testMatrix, out resultMatrix);

			Assert.That(resultMatrix, Is.EqualTo(testMatrixExpectedTranspose));

		}

		[Test]
		public void CreateFromQuaternion ()
		{
			Half pi; RealMaths.Pi(out pi);

			Quaternion q;
			Quaternion.CreateFromYawPitchRoll(pi, pi*2, pi / 2, out q);

			Matrix44 mat1;
			Matrix44.CreateFromQuaternion(ref q, out mat1);

			Matrix44 mat2;
			Matrix44.CreateFromQuaternionOld(ref q, out mat2);

			Assert.That(mat1, Is.EqualTo(mat2));
		}

		[Test]
		public void Decompose ()
		{
            Matrix44 scale;
            Matrix44.CreateScale(4, 2, 3, out scale);

            Matrix44 rotation;
            Half pi; RealMaths.Pi(out pi);
            Matrix44.CreateRotationY(pi, out rotation);

            Matrix44 translation;
            Matrix44.CreateTranslation(100, 5, 3, out translation);

            Matrix44 m = rotation * scale;
            //m = translation * m;
			m.Translation = new Vector3(100, 5, 3);

            Vector3 outScale;
            Quaternion outRotation;
            Vector3 outTranslation;

            m.Decompose(out outScale, out outRotation, out outTranslation);

			Matrix44 mat;
            Matrix44.CreateFromQuaternion(ref outRotation, out mat);

			Assert.That(outScale, Is.EqualTo(new Vector3(4, 2, 3)));
			Assert.That(mat, Is.EqualTo(rotation));
			Assert.That(outTranslation, Is.EqualTo(new Vector3(100, 5, 3)));

		}
	}}

namespace Sungiant.Abacus.SinglePrecision.Tests
{
	[TestFixture]
	public class Matrix44Tests
	{
		[Test]
		public void TestTranspose_MemberFn ()
		{
			Matrix44 startMatrix = new Matrix44(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

			Matrix44 testMatrix = startMatrix;

			Matrix44 testMatrixExpectedTranspose = new Matrix44(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

			testMatrix.Transpose();

			Assert.That(testMatrix, Is.EqualTo(testMatrixExpectedTranspose));
		}

		[Test]
		public void TestTranspose_StaticFn ()
		{
			Matrix44 startMatrix = new Matrix44(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

			Matrix44 testMatrix = startMatrix;

			Matrix44 testMatrixExpectedTranspose = new Matrix44(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

			// RUN THE STATIC VERSION OF THE FUNCTION
			Matrix44 resultMatrix = Matrix44.Identity;

			Matrix44.Transpose(ref testMatrix, out resultMatrix);

			Assert.That(resultMatrix, Is.EqualTo(testMatrixExpectedTranspose));

		}

		[Test]
		public void CreateFromQuaternion ()
		{
			Single pi; RealMaths.Pi(out pi);

			Quaternion q;
			Quaternion.CreateFromYawPitchRoll(pi, pi*2, pi / 2, out q);

			Matrix44 mat1;
			Matrix44.CreateFromQuaternion(ref q, out mat1);

			Matrix44 mat2;
			Matrix44.CreateFromQuaternionOld(ref q, out mat2);

			Assert.That(mat1, Is.EqualTo(mat2));
		}

		[Test]
		public void Decompose ()
		{
            Matrix44 scale;
            Matrix44.CreateScale(4, 2, 3, out scale);

            Matrix44 rotation;
            Single pi; RealMaths.Pi(out pi);
            Matrix44.CreateRotationY(pi, out rotation);

            Matrix44 translation;
            Matrix44.CreateTranslation(100, 5, 3, out translation);

            Matrix44 m = rotation * scale;
            //m = translation * m;
			m.Translation = new Vector3(100, 5, 3);

            Vector3 outScale;
            Quaternion outRotation;
            Vector3 outTranslation;

            m.Decompose(out outScale, out outRotation, out outTranslation);

			Matrix44 mat;
            Matrix44.CreateFromQuaternion(ref outRotation, out mat);

			Assert.That(outScale, Is.EqualTo(new Vector3(4, 2, 3)));
			Assert.That(mat, Is.EqualTo(rotation));
			Assert.That(outTranslation, Is.EqualTo(new Vector3(100, 5, 3)));

		}
	}}

namespace Sungiant.Abacus.DoublePrecision.Tests
{
	[TestFixture]
	public class Matrix44Tests
	{
		[Test]
		public void TestTranspose_MemberFn ()
		{
			Matrix44 startMatrix = new Matrix44(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

			Matrix44 testMatrix = startMatrix;

			Matrix44 testMatrixExpectedTranspose = new Matrix44(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

			testMatrix.Transpose();

			Assert.That(testMatrix, Is.EqualTo(testMatrixExpectedTranspose));
		}

		[Test]
		public void TestTranspose_StaticFn ()
		{
			Matrix44 startMatrix = new Matrix44(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

			Matrix44 testMatrix = startMatrix;

			Matrix44 testMatrixExpectedTranspose = new Matrix44(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

			// RUN THE STATIC VERSION OF THE FUNCTION
			Matrix44 resultMatrix = Matrix44.Identity;

			Matrix44.Transpose(ref testMatrix, out resultMatrix);

			Assert.That(resultMatrix, Is.EqualTo(testMatrixExpectedTranspose));

		}

		[Test]
		public void CreateFromQuaternion ()
		{
			Double pi; RealMaths.Pi(out pi);

			Quaternion q;
			Quaternion.CreateFromYawPitchRoll(pi, pi*2, pi / 2, out q);

			Matrix44 mat1;
			Matrix44.CreateFromQuaternion(ref q, out mat1);

			Matrix44 mat2;
			Matrix44.CreateFromQuaternionOld(ref q, out mat2);

			Assert.That(mat1, Is.EqualTo(mat2));
		}

		[Test]
		public void Decompose ()
		{
            Matrix44 scale;
            Matrix44.CreateScale(4, 2, 3, out scale);

            Matrix44 rotation;
            Double pi; RealMaths.Pi(out pi);
            Matrix44.CreateRotationY(pi, out rotation);

            Matrix44 translation;
            Matrix44.CreateTranslation(100, 5, 3, out translation);

            Matrix44 m = rotation * scale;
            //m = translation * m;
			m.Translation = new Vector3(100, 5, 3);

            Vector3 outScale;
            Quaternion outRotation;
            Vector3 outTranslation;

            m.Decompose(out outScale, out outRotation, out outTranslation);

			Matrix44 mat;
            Matrix44.CreateFromQuaternion(ref outRotation, out mat);

			Assert.That(outScale, Is.EqualTo(new Vector3(4, 2, 3)));
			Assert.That(mat, Is.EqualTo(rotation));
			Assert.That(outTranslation, Is.EqualTo(new Vector3(100, 5, 3)));

		}
	}}

namespace Sungiant.Abacus.Fixed32Precision.Tests
{
	[TestFixture]
	public class Matrix44Tests
	{
		[Test]
		public void TestTranspose_MemberFn ()
		{
			Matrix44 startMatrix = new Matrix44(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

			Matrix44 testMatrix = startMatrix;

			Matrix44 testMatrixExpectedTranspose = new Matrix44(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

			testMatrix.Transpose();

			Assert.That(testMatrix, Is.EqualTo(testMatrixExpectedTranspose));
		}

		[Test]
		public void TestTranspose_StaticFn ()
		{
			Matrix44 startMatrix = new Matrix44(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

			Matrix44 testMatrix = startMatrix;

			Matrix44 testMatrixExpectedTranspose = new Matrix44(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

			// RUN THE STATIC VERSION OF THE FUNCTION
			Matrix44 resultMatrix = Matrix44.Identity;

			Matrix44.Transpose(ref testMatrix, out resultMatrix);

			Assert.That(resultMatrix, Is.EqualTo(testMatrixExpectedTranspose));

		}

		[Test]
		public void CreateFromQuaternion ()
		{
			Fixed32 pi; RealMaths.Pi(out pi);

			Quaternion q;
			Quaternion.CreateFromYawPitchRoll(pi, pi*2, pi / 2, out q);

			Matrix44 mat1;
			Matrix44.CreateFromQuaternion(ref q, out mat1);

			Matrix44 mat2;
			Matrix44.CreateFromQuaternionOld(ref q, out mat2);

			Assert.That(mat1, Is.EqualTo(mat2));
		}

		[Test]
		public void Decompose ()
		{
            Matrix44 scale;
            Matrix44.CreateScale(4, 2, 3, out scale);

            Matrix44 rotation;
            Fixed32 pi; RealMaths.Pi(out pi);
            Matrix44.CreateRotationY(pi, out rotation);

            Matrix44 translation;
            Matrix44.CreateTranslation(100, 5, 3, out translation);

            Matrix44 m = rotation * scale;
            //m = translation * m;
			m.Translation = new Vector3(100, 5, 3);

            Vector3 outScale;
            Quaternion outRotation;
            Vector3 outTranslation;

            m.Decompose(out outScale, out outRotation, out outTranslation);

			Matrix44 mat;
            Matrix44.CreateFromQuaternion(ref outRotation, out mat);

			Assert.That(outScale, Is.EqualTo(new Vector3(4, 2, 3)));
			Assert.That(mat, Is.EqualTo(rotation));
			Assert.That(outTranslation, Is.EqualTo(new Vector3(100, 5, 3)));

		}
	}}

