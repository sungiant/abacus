// ┌────────────────────────────────────────────────────────────────────────┐ \\
// │    _____ ___.                                                          │ \\
// │   /  _  \\_ |__ _____    ____  __ __  ______                           │ \\
// │  /  /_\  \| __ \\__  \ _/ ___\|  |  \/  ___/                           │ \\
// │ /    |    \ \_\ \/ __ \\  \___|  |  /\___ \                            │ \\
// │ \____|__  /___  (____  /\___  >____//____  >                           │ \\
// │         \/    \/     \/     \/           \/                            │ \\
// │                                                                        │ \\
// │ Fast, efficient, cross platform, cross precision, maths library.       │ \\
// │                                                                        │ \\
// │        ________________________________________________________        │ \\
// │       /  ____________________________________________________  \       │ \\
// │       | |<_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> | |       │ \\
// │       | |<_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> | |       │ \\
// │       | |_|___|___|___|___|___|___|___|___|___|___|___|___|__| |       │ \\
// │       |  ____________________________________________________  |       │ \\
// │       | | |   |   |   |   |   |   |   |   |   |   |   |   |  | |       │ \\
// │       | |<_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> | |       │ \\
// │       | |<_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> | |       │ \\
// │       | |<_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> | |       │ \\
// │       | |<_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> | |       │ \\
// │       | |<_>_<_>_<_>_<_>_<_>_<_>_<_>_<_>_<_>_<_>_<_>_<_>_<_>_| |       │ \\
// │       \________________________________________________________/       │ \\
// │                                                                        │ \\
// ├────────────────────────────────────────────────────────────────────────┤ \\
// │ Copyright © 2012 - 2015 ~ Blimey3D (http://www.blimey.io)              │ \\
// ├────────────────────────────────────────────────────────────────────────┤ \\
// │ Authors:                                                               │ \\
// │ ~ Ash Pook (http://www.ajpook.com)                                     │ \\
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
#define TESTS_ENABLED

#if TESTS_ENABLED

using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using NUnit.Framework;
using System.Runtime.CompilerServices;

namespace Abacus.SinglePrecision
{
    static class Settings { internal const UInt32 NumTests = 10000; }

    internal static class MarshalHelper
    {
        // Copies data from an unmanaged memory pointer to a managed
        // Single-precision fixed-point number array.
        internal static void Copy(
            IntPtr source,
            Single[] destination,
            Int32 startIndex,
            Int32 length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }
    }

    /// <summary>
    /// todo
    /// </summary>
    [TestFixture]
    public class MathsTests
    {
        /// <summary>
        /// Provides the constant TestTolerance.
        /// </summary>
        public static void TestTolerance (out Single value)
        {
            value = 1.0e-3f;
        }


        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_Zero_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }
        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_Half_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }
        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_One_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_E_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }
        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_Log10E_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }
        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_Log2E_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_Pi_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_PiOver2_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_PiOver4_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_Tau_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_Epsilon_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_Root2_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestConstant_Root3_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_IsZero_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_FromString_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_FromFraction_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_ToRadians_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_ToDegrees_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Sqrt_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Sin_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Cos_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Tan_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Abs_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_ArcSin_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_ArcCos_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_ArcTan_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Min_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Max_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_WithinEpsilon_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Sign_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }
    }

    /// <summary>
    /// todo
    /// </summary>
    [TestFixture]
    public class Vector2Tests
    {
        /// <summary>
        /// The random number generator used for testing.
        /// </summary>
        static readonly System.Random rand;

        /// <summary>
        /// Static constructor used to ensure that the random number generator
        /// always gets initilised with the same seed, making the tests
        /// behave in a deterministic manner.
        /// </summary>
        static Vector2Tests ()
        {
            rand = new System.Random(0);
        }

        /// <summary>
        /// Helper function for getting the next random Single value.
        /// </summary>
        static Single GetNextRandomSingle ()
        {
            Single randomValue = rand.NextSingle();

            Single zero = 0;
            Single multiplier = 1000;

            randomValue *= multiplier;

            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;

            if( randomBoolean )
                randomValue = zero - randomValue;

            return randomValue;
        }

        /// <summary>
        /// Helper function for getting the next random Vector2.
        /// </summary>
        static Vector2 GetNextRandomVector2 ()
        {
            Single a = GetNextRandomSingle();
            Single b = GetNextRandomSingle();

            return new Vector2(a, b);
        }

        /// <summary>
        /// Helper to encapsulate asserting that two Vector2s are equal.
        /// </summary>
        static void AssertEqualWithinReason (Vector2 a, Vector2 b)
        {
            Single tolerance; MathsTests.TestTolerance(out tolerance);

            Assert.That(a.X, Is.EqualTo(b.X).Within(tolerance));
            Assert.That(a.Y, Is.EqualTo(b.Y).Within(tolerance));
        }


        // Test: StructLayout //----------------------------------------------//

        /// <summary>
        /// This test makes sure that the struct layout has been defined
        /// correctly.
        /// </summary>
        [Test]
        public void Test_StructLayout_i ()
        {
            Type t = typeof(Vector2);

            Assert.That(
                t.StructLayoutAttribute.Value,
                Is.EqualTo(LayoutKind.Sequential));
        }

        /// <summary>
        /// This test makes sure that when examining the memory addresses of the
        /// X and Y member variables of a number of randomly generated Vector2
        /// objects the results are as expected.
        /// </summary>
        [Test]
        public unsafe void Test_StructLayout_ii ()
        {
            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector2 vec = GetNextRandomVector2();

                GCHandle h_vec = GCHandle.Alloc(vec, GCHandleType.Pinned);

                IntPtr vecAddress = h_vec.AddrOfPinnedObject();

                Single[] data = new Single[2];

                // nb: when Fixed32 and Half are moved back into the main
                //     dev branch there will be need for an extension method for
                //     Marshal that will perform the copy for those types.
                MarshalHelper.Copy(vecAddress, data, 0, 2);
                Assert.That(data[0], Is.EqualTo(vec.X));
                Assert.That(data[1], Is.EqualTo(vec.Y));

                h_vec.Free();
            }
        }

        // Test: Constructors //----------------------------------------------//

        /// <summary>
        /// This test goes though each public constuctor and ensures that the
        /// data members of the structure have been properly set.
        /// </summary>
        [Test]
        public void Test_Constructors_i ()
        {
            {
                // Test default values
                Vector2 a = new Vector2();
                Assert.That(a, Is.EqualTo(Vector2.Zero));
            }
            {
                // Test Vector2( Single, Single )
                Single u = -189;
                Single v = 429;
                Vector2 c = new Vector2(u, v);
                Assert.That(c.X, Is.EqualTo(u));
                Assert.That(c.Y, Is.EqualTo(v));
            }
        }

        // Test Member Fn: ToString //----------------------------------------//

        /// <summary>
        /// For a given example, this test ensures that the ToString function
        /// yields the expected string.
        /// </summary>
        [Test]
        public void TestMemberFn_ToString_i ()
        {
            Vector2 a = new Vector2(42, -17);

            String result = a.ToString();

            String expected = "{X:42 Y:-17}";

            Assert.That(result, Is.EqualTo(expected));
        }

        // Test Member Fn: GetHashCode //-------------------------------------//

        /// <summary>
        /// Makes sure that the hashing function is good by testing 10,000
        /// random scenarios and ensuring that there are no more than 10
        /// collisions.
        /// </summary>
        [Test]
        public void TestMemberFn_GetHashCode_i ()
        {
            var hs1 = new System.Collections.Generic.HashSet<Vector2>();
            var hs2 = new System.Collections.Generic.HashSet<Int32>();

            for(Int32 i = 0; i < 10000; ++i)
            {
                var a = GetNextRandomVector2();

                hs1.Add(a);
                hs2.Add(a.GetHashCode());
            }

            Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
        }

        // Test Constant: Zero //---------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector2 initilised using the Zero constant
        /// has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_Zero_i ()
        {
            Vector2 result = Vector2.Zero;
            Vector2 expected = new Vector2(0, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: One //----------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector2 initilised using the One constant
        /// has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_One_i ()
        {
            Vector2 result = Vector2.One;
            Vector2 expected = new Vector2(1, 1);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: UnitX //--------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector2 initilised using the UnitX 
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_UnitX_i ()
        {
            Vector2 result = Vector2.UnitX;
            Vector2 expected = new Vector2(1, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: UnitY //--------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector2 initilised using the UnitY
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_UnitY_i ()
        {
            Vector2 result = Vector2.UnitY;
            Vector2 expected = new Vector2(0, 1);
            AssertEqualWithinReason(result, expected);
        }

        // Test Static Fn: Distance //----------------------------------------//

        /// <summary>
        /// Assert that, for a number of known examples, the Distance method
        /// yeilds the correct results.
        /// </summary>
        [Test]
        public void TestStaticFn_Distance_i ()
        {
            var tests = new Tuple<Vector2, Vector2, Single>[]
            {
                //a -> b -> expected
                new Tuple<Vector2, Vector2, Single> (
                    new Vector2(0, 4), new Vector2(3, 0), 5),

                new Tuple<Vector2, Vector2, Single> (
                    new Vector2(0, -4), new Vector2(3, 0), 5),

                new Tuple<Vector2, Vector2, Single> (
                    new Vector2(0, -4), new Vector2(-3, 0), 5),

                new Tuple<Vector2, Vector2, Single> (
                    Vector2.Zero, Vector2.Zero, 0),
            };

            foreach(var test in tests)
            {
                Vector2 a = test.Item1;
                Vector2 b = test.Item2;
                Single expected = test.Item3;

                Single result;
                Vector2.Distance(ref a, ref b, out result);
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Distance method yeilds the same results as those obtained from
        /// performing a manual calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Distance_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector2 a = GetNextRandomVector2();

                Single expected =
                    Maths.Sqrt((a.X * a.X) + (a.Y * a.Y));

                Assert.That(a.Length(), Is.EqualTo(expected));
            }
        }

        // Test Static Fn: DistanceSquared //---------------------------------//

        /// <summary>
        /// Assert that, for a number of known examples, the DistanceSquared
        /// method yeilds the correct results.
        /// </summary>
        [Test]
        public void TestStaticFn_DistanceSquared_i ()
        {
            var tests = new Tuple<Vector2, Vector2, Single>[]
            {
                //a -> b -> expected
                new Tuple<Vector2, Vector2, Single> (
                    new Vector2(0, 4), new Vector2(3, 0), 25),

                new Tuple<Vector2, Vector2, Single> (
                    new Vector2(0, -4), new Vector2(3, 0), 25),

                new Tuple<Vector2, Vector2, Single> (
                    new Vector2(0, -4), new Vector2(-3, 0), 25),

                new Tuple<Vector2, Vector2, Single> (
                    Vector2.Zero, Vector2.Zero, 0),
            };

            foreach(var test in tests)
            {
                Vector2 a = test.Item1;
                Vector2 b = test.Item2;
                Single expected = test.Item3;

                Single result;
                Vector2.DistanceSquared(ref a, ref b, out result);
                Assert.That(result, Is.EqualTo(expected));
            }
        }


        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// DistanceSquared method yeilds the same results as those obtained
        /// from performing a manual calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_DistanceSquared_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector2 a = GetNextRandomVector2();
                Vector2 b = GetNextRandomVector2();

                Vector2 c = b - a;

                Single expected = (c.X * c.X) + (c.Y * c.Y);
                Single result;

                Vector2.DistanceSquared(ref a, ref b, out result);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        // Test Static Fn: Dot //---------------------------------------------//

        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Dot method yeilds the same results as those obtained from
        /// performing a manual calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_i ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector2 a = GetNextRandomVector2();
                Vector2 b = GetNextRandomVector2();

                Single expected = (a.X * b.X) + (a.Y * b.Y);
                Single result; Vector2.Dot(ref a, ref b, out result);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        /// <summary>
        /// Assert that two unit vectors pointing in opposing directions yeild a
        /// dot product of negative one.
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_ii ()
        {
            Vector2 a = new Vector2(1, 0);
            Vector2 b = new Vector2(-1, 0);

            Single expected = -1;
            Single result; Vector2.Dot(ref a, ref b, out result);

            Assert.That(result, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that two unit vectors pointing in the same direction yeild a
        /// dot product of one.
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_iii ()
        {
            Vector2 a = new Vector2(1, 0);
            Vector2 b = new Vector2(1, 0);

            Single expected = 1;
            Single result; Vector2.Dot(ref a, ref b, out result);

            Assert.That(result, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that two perpendicular unit vectors yeild a dot product of
        /// zero.
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_iv ()
        {
            Vector2 a = new Vector2(1, 0);
            Vector2 b = new Vector2(0, 1);

            Single expected = 0;
            Single result; Vector2.Dot(ref a, ref b, out result);

            Assert.That(result, Is.EqualTo(expected));
        }

        // Test Static Fn: Normalise //---------------------------------------//

        /// <summary>
        /// Assert that, for a known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_Normalise_i()
        {
            {
                Vector2 a = Vector2.Zero;

                Vector2 b;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                    Vector2.Normalise(ref a, out b)
                );
            }

            {
                Vector2 a = new Vector2(
                    Single.MaxValue,
                    Single.MaxValue);

                Vector2 b;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                    Vector2.Normalise(ref a, out b)
                );
            }
        }

        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Normalise method yeilds a unit vector (with length equal to one);
        /// </summary>
        [Test]
        public void TestStaticFn_Normalise_ii ()
        {
            Single epsilon; Maths.Epsilon(out epsilon);

            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector2 a = GetNextRandomVector2();

                Vector2 b; Vector2.Normalise(ref a, out b);
                Single expected = 1;
                Single result1 = b.Length();
                Assert.That(result1, Is.EqualTo(expected).Within(epsilon));

                // The normalise function takes both a ref and out parameter,
                // need to check that if we pass in the same value as both
                // parameters we get the same results.
                Vector2 c = a;
                Vector2.Normalise(ref c, out c);
                Single result2 = c.Length();
                Assert.That(result2, Is.EqualTo(expected).Within(epsilon));
            }
        }

        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Normalise method yeilds a vector, which when multipled by the
        /// length of the original vector results in the same vector as the
        /// original vector;
        /// </summary>
        [Test]
        public void TestStaticFn_Normalise_iii ()
        {
            Single epsilon; Maths.Epsilon(out epsilon);

            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector2 a = GetNextRandomVector2();
                Single l = a.Length();
                Vector2 expected = a;

                Vector2 b; Vector2.Normalise(ref a, out b);
                Vector2 result1 = b * l;
                AssertEqualWithinReason(result1, expected);

                Vector2 c = a;

                // The normalise function takes both a ref and out parameter,
                // need to check that if we pass in the same value as both
                // parameters we get the same results.
                Vector2.Normalise(ref c, out c);
                Vector2 result2 = c * l;
                AssertEqualWithinReason(result2, expected);
            }
        }

        // Test Static Fn: Reflect //-----------------------------------------//

        /// <summary>
        /// Assert that, for a number of known examples, the Reflect method
        /// yeilds the correct results.
        /// </summary>
        [Test]
        public void TestStaticFn_Reflect_i ()
        {
            var a = new Vector2(20, -5);
            var b = new Vector2(1, -1); Vector2.Normalise(ref b, out b);
            var c = new Vector2(2, -1); Vector2.Normalise(ref c, out c);
            var d = Vector2.Zero;
            var e = new Vector2(1, 0);

            var ex1 = new Vector2(-5, 20);
            var ex2 = new Vector2(-16, 13);
            var ex3 = d;

            var tests = new Tuple<Vector2, Vector2, Vector2>[]
            {
                //incident -> normal -> expected
                new Tuple<Vector2, Vector2, Vector2> (a, b, ex1),
                new Tuple<Vector2, Vector2, Vector2> (a, c, ex2),
                new Tuple<Vector2, Vector2, Vector2> (d, e, ex3),
            };

            foreach(var test in tests)
            {
                Vector2 incident = test.Item1;
                Vector2 normal = test.Item2;
                Vector2 expected = test.Item3;
                Vector2 result;
                Vector2.Reflect(ref incident, ref normal, out result);
                AssertEqualWithinReason(result, expected);
            }
        }


        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Reflect method yeilds the same results as those obtained from
        /// performing a manual calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Reflect_ii ()
        {
            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector2 a = GetNextRandomVector2();

                Vector2 b = GetNextRandomVector2();

                Vector2.Normalise(ref b, out b);

                Vector2 result;
                Vector2.Reflect(ref a, ref b, out result);

                Single dot;
                Vector2.Dot(ref a, ref b, out dot);

                Vector2 expected = a - (2 * dot * b);

                AssertEqualWithinReason(result, expected);
            }
        }

        /// <summary>
        /// Assert that an argument exception is thrown if the value passed in
        /// to the normal parameter is not normalised.
        /// </summary>
        [Test]
        public void TestStaticFn_Reflect_iii ()
        {
            Vector2 incident = GetNextRandomVector2();
            Vector2 normal = new Vector2(12, -241);

            Vector2 result;

            Assert.Throws(
                typeof(ArgumentOutOfRangeException),
                () =>
                Vector2.Reflect(ref incident, ref normal, out result)
            );
        }

        // Test Static Fn: TransformMatrix44 //-------------------------------//

        /// <summary>
        ///
        /// </summary>
        [Test]
        public void TestStaticFn_TransformMatix44_i ()
        {
            Single pi; Maths.Pi (out pi);
            Single piOver2 = pi / (Single) 2;
            Single minusPi = -pi;

            Vector2 v1 = new Vector2 (8, 70);

            Matrix44 rotmati = Matrix44.Identity;
            Matrix44 rotmat1; Matrix44.CreateRotationX(ref pi, out rotmat1);
            Matrix44 rotmat2; Matrix44.CreateRotationY(ref piOver2, out rotmat2);
            Matrix44 rotmat3; Matrix44.CreateRotationZ(ref minusPi, out rotmat3);
            Matrix44 rotmat4 = rotmat1 * rotmat2 * rotmat3;

            var tests = new Tuple<Vector2, Matrix44, Vector2>[]
            {
                //vector -> transform -> expected
                new Tuple<Vector2, Matrix44, Vector2>(
                    v1, rotmati, v1),

                new Tuple<Vector2, Matrix44, Vector2>(
                    v1, rotmat1, new Vector2 (8, -70)),

                new Tuple<Vector2, Matrix44, Vector2>(
                    v1, rotmat2, new Vector2 (0, 70)),

                new Tuple<Vector2, Matrix44, Vector2>(
                    v1, rotmat3, new Vector2 (-8, -70)),

                new Tuple<Vector2, Matrix44, Vector2>(
                    v1, rotmat4, new Vector2 (0, 70)),
            };

            foreach (var test in tests)
            {
                Vector2 vec = test.Item1;
                Matrix44 trans = test.Item2;
                Vector2 expected = test.Item3;

                Vector2 result;
                Vector2.Transform (ref vec, ref trans, out result);
                AssertEqualWithinReason (result, expected);
            }
        }

        // Test Static Fn: TransformNormal //---------------------------------//

        /// <summary>
        ///
        /// </summary>
        [Test]
        public void TestStaticFn_TransformNormal_i ()
        {
            Single one = 1;
            Single six = 6;
            Single eight = 8;
            Single ten = 10;
            Single root2; Maths.Root2 (out root2);
            Single pi; Maths.Pi (out pi);
            Single minusPi = -pi;
            Single piOver2 = pi / (Single) 2;
            Single oneOverRoot2 = one / root2;
            Single sixTenths = six / ten;
            Single eightTenths = eight / ten;

            Vector2 v1 = new Vector2 (sixTenths, eightTenths);
            Vector2 v2 = new Vector2 (oneOverRoot2, oneOverRoot2);

            Matrix44 rotmati = Matrix44.Identity;
            Matrix44 rotmat1; Matrix44.CreateRotationX(ref pi, out rotmat1);
            Matrix44 rotmat2; Matrix44.CreateRotationY(ref piOver2, out rotmat2);
            Matrix44 rotmat3; Matrix44.CreateRotationZ(ref minusPi, out rotmat3);
            Matrix44 rotmat4 = rotmat1 * rotmat2 * rotmat3;

            var tests = new Tuple<Vector2, Matrix44, Vector2>[]
            {
                //normal -> transform -> expected
                new Tuple<Vector2, Matrix44, Vector2>(
                    v1, rotmati, v1),

                new Tuple<Vector2, Matrix44, Vector2>(
                    v1, rotmat1, new Vector2 (sixTenths, -eightTenths)),

                new Tuple<Vector2, Matrix44, Vector2>(
                    v1, rotmat2, new Vector2 (0, eightTenths)),

                new Tuple<Vector2, Matrix44, Vector2>(
                    v1, rotmat3, new Vector2 (-sixTenths, -eightTenths)),

                new Tuple<Vector2, Matrix44, Vector2>(
                    v1, rotmat4, new Vector2 (0, eightTenths)),

                //normal -> transform -> expected
                new Tuple<Vector2, Matrix44, Vector2>(
                    v2, rotmati, v2),

                new Tuple<Vector2, Matrix44, Vector2>(
                    v2, rotmat1, new Vector2 (oneOverRoot2, -oneOverRoot2)),

                new Tuple<Vector2, Matrix44, Vector2>(
                    v2, rotmat2, new Vector2 (0, oneOverRoot2)),

                new Tuple<Vector2, Matrix44, Vector2>(
                    v2, rotmat3, new Vector2 (-oneOverRoot2, -oneOverRoot2)),

                new Tuple<Vector2, Matrix44, Vector2>(
                    v2, rotmat4, new Vector2 (0, oneOverRoot2)),
            };

            foreach (var test in tests)
            {
                Vector2 normalVec = test.Item1;
                Matrix44 trans = test.Item2;
                Vector2 expected = test.Item3;

                Vector2 result;

                Vector2.TransformNormal (ref normalVec, ref trans, out result);
                AssertEqualWithinReason(result, expected);

                // should also work with the standard transform fn
                Vector2.Transform (ref normalVec, ref trans, out result);
                AssertEqualWithinReason(result, expected);
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Test]
        public void TestStaticFn_TransformNormal_ii ()
        {
            Matrix44 rotmat = Matrix44.Identity;
            var tests = new Vector2[]
            {
                new Vector2 (21, -532),
                new Vector2 (21, +532),
                new Vector2 (1, -1),
                new Vector2 (-2435, -2),
            };

            foreach (var test in tests)
            {
                Vector2 normal = test;
                Vector2 result;
                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                    Vector2.TransformNormal(ref normal, ref rotmat, out result)
                );
            }
        }

        // Test Static Fn: TransformQuaternion //-----------------------------//

        /// <summary>
        ///
        /// </summary>
        [Test]
        public void TestStaticFn_TransformQuaternion_i ()
        {
            Vector2 v1 = new Vector2 (8, 70);

            Quaternion quatmati = new Quaternion (0, 0, 0, 1); // identity
            Quaternion quatmat1 = new Quaternion (1, 0, 0, 0);
            Quaternion quatmat2 = new Quaternion (0, 1, 0, 0);
            Quaternion quatmat3 = new Quaternion (0, 0, 1, 0);

            var tests = new Tuple<Vector2, Quaternion, Vector2>[]
            {
                //vector -> transform -> expected
                new Tuple<Vector2, Quaternion, Vector2>(
                    v1, quatmati, v1),

                new Tuple<Vector2, Quaternion, Vector2>(
                    v1, quatmat1, new Vector2 ( 8, -70)),

                new Tuple<Vector2, Quaternion, Vector2>(
                    v1, quatmat2, new Vector2 (-8,  70)),

                new Tuple<Vector2, Quaternion, Vector2>(
                    v1, quatmat3, new Vector2 (-8, -70)),
            };

            foreach (var test in tests)
            {
                Vector2 vec = test.Item1;
                Quaternion trans = test.Item2;
                Vector2 expected = test.Item3;

                Vector2 result;
                Vector2.Transform (ref vec, ref trans, out result);
                AssertEqualWithinReason(result, expected);
            }
        }

        /// <summary>
        /// Tests that for a known example the Length member function yields
        /// the correct result.
        /// </summary>
        [Test]
        public void TestStaticFn_Length_i ()
        {
            Vector2 a = new Vector2(3, -4);

            Single expected = 5;

            Single result = a.Length();

            Assert.That(result, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests that for a known example the LengthSquared member function
        /// yields the correct result.
        /// </summary>
        [Test]
        public void TestStaticFn_LengthSquared_i ()
        {
            Vector2 a = new Vector2(3, -4);

            Single expected = 25;

            Single result = a.LengthSquared();

            Assert.That(result, Is.EqualTo(expected));
        }

        // Test Operator: Equality //-----------------------------------------//

        /// <summary>
        /// Helper method for testing equality.
        /// </summary>
        void TestEquality (Vector2 a, Vector2 b, Boolean expected )
        {
            // This test asserts the following:
            //   (a == b) == expected
            //   (b == a) == expected
            //   (a != b) == !expected
            //   (b != a) == !expected

            Boolean result_1a = (a == b);
            Boolean result_1b = (a.Equals(b));
            Boolean result_1c = (a.Equals((Object)b));

            Boolean result_2a = (b == a);
            Boolean result_2b = (b.Equals(a));
            Boolean result_2c = (b.Equals((Object)a));

            Boolean result_3a = (a != b);
            Boolean result_4a = (b != a);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_1c, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
            Assert.That(result_2c, Is.EqualTo(expected));
            Assert.That(result_3a, Is.EqualTo(!expected));
            Assert.That(result_4a, Is.EqualTo(!expected));
        }

        /// <summary>
        /// Makes sure that, for a known example, all the equality opperators
        /// and functions yield the expected result of TRUE when two equal
        /// Vector2 objects are compared.
        /// </summary>
        [Test]
        public void TestOperator_Equality_i ()
        {
            var a = new Vector2(44, -54);
            var b = new Vector2(44, -54);

            Boolean expected = true;

            this.TestEquality(a, b, expected);
        }

        /// <summary>
        /// Makes sure that, for a known example, all the equality opperators
        /// and functions yield the expected result of FALSE when two unequal
        /// Vector2 objects are compared.
        /// </summary>
        [Test]
        public void TestOperator_Equality_ii ()
        {
            var a = new Vector2(44, 54);
            var b = new Vector2(44, -54);

            Boolean expected = false;

            this.TestEquality(a, b, expected);
        }

        /// <summary>
        /// Tests to make sure that all the equality opperators and functions
        /// yield the expected result of TRUE when used on a number of randomly
        /// generated pairs of equal Vector2 objects.
        /// </summary>
        [Test]
        public void TestOperator_Equality_iii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector2();

                Vector2 b = a;

                this.TestEquality(a, b, true);
            }
        }


        // Test Operator: Addition //-----------------------------------------//

        /// <summary>
        /// Helper method for testing addition.
        /// </summary>
        void TestAddition (Vector2 a, Vector2 b, Vector2 expected )
        {
            // This test asserts the following:
            //   a + b == expected
            //   b + a == expected

            var result_1a = a + b;
            var result_2a = b + a;

            Vector2 result_1b; Vector2.Add(ref a, ref b, out result_1b);
            Vector2 result_2b; Vector2.Add(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for a known example, all the addition opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Addition_i ()
        {
            var a = new Vector2(3, -6);
            var b = new Vector2(-6, 12);

            var expected = new Vector2(-3, 6);

            this.TestAddition(a, b, expected);
        }

        /// <summary>
        /// Assert that, for a known example involving the zero vector, all the
        /// addition opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Addition_ii ()
        {
            var a = new Vector2(-2313, 88);

            var expected = a;

            this.TestAddition(a, Vector2.Zero, expected);
        }

        /// <summary>
        /// Assert that, for a known example involving two zero vectors, all the
        /// addition opperators and functions yield the correct result of zero.
        /// </summary>
        [Test]
        public void TestOperator_Addition_iii ()
        {
            this.TestAddition(Vector2.Zero, Vector2.Zero, Vector2.Zero);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// addition opperators and functions yield the same results as a
        /// manual addition calculation.
        /// </summary>
        [Test]
        public void TestOperator_Addition_iv ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector2();
                var b = GetNextRandomVector2();

                var expected = new Vector2(a.X + b.X, a.Y + b.Y);

                this.TestAddition(a, b, expected);
            }
        }

        // Test Operator: Subtraction //--------------------------------------//

        /// <summary>
        /// Helper method for testing subtraction.
        /// </summary>
        void TestSubtraction (Vector2 a, Vector2 b, Vector2 expected )
        {
            // This test asserts the following:
            //   a - b == expected
            //   b - a == -expected

            var result_1a = a - b;
            var result_2a = b - a;

            Vector2 result_1b; Vector2.Subtract(ref a, ref b, out result_1b);
            Vector2 result_2b; Vector2.Subtract(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(-expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(-expected));
        }

        /// <summary>
        /// Assert that, for known examples, all the subtraction opperators
        /// and functions yield the correct result.
        /// <summary>
        [Test]
        public void TestOperator_Subtraction_i ()
        {
            var a = new Vector2(12, -4);
            var b = new Vector2(15, 11);
            var expected = new Vector2(-3, -15);
            this.TestSubtraction(a, b, expected);

            var c = new Vector2(-423, 342);
            this.TestSubtraction(c, Vector2.Zero, c);
        }

        /// <summary>
        /// Assert that when subtracting the zero vector fromt the zero vector,
        /// all the subtraction opperators and functions yield the correct
        /// result.
        /// <summary>
        [Test]
        public void TestOperator_Subtraction_ii ()
        {
            this.TestSubtraction(Vector2.Zero, Vector2.Zero, Vector2.Zero);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// subtraction opperators and functions yield the same results as a
        /// manual subtraction calculation.
        /// </summary>
        [Test]
        public void TestOperator_Subtraction_iii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector2();
                var b = GetNextRandomVector2();

                var expected = new Vector2(a.X - b.X, a.Y - b.Y);

                this.TestSubtraction(a, b, expected);
            }
        }

        // Test Operator: Negation //-----------------------------------------//

        /// <summary>
        /// Helper method for testing negation.
        /// </summary>
        void TestNegation (Vector2 a, Vector2 expected )
        {
            // This test asserts the following:
            //   -a == expected

            var result_1a = -a;

            Vector2 result_1b; Vector2.Negate(ref a, out result_1b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for known examples, all the negation opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_i ()
        {
            Single r = 3432;
            Single s = -6218;
            Single t = -3432;
            Single u = 6218;

            var a = new Vector2(r, s);
            var b = new Vector2(u, t);
            var c = new Vector2(t, u);
            var d = new Vector2(s, r);

            this.TestNegation(a, c);
            this.TestNegation(b, d);
        }

        /// <summary>
        /// Assert that, for known examples involving the zero vector, all the
        /// negation opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_ii ()
        {
            Single t = -3432;
            Single u = 6218;
            Single r = 3432;
            Single s = -6218;

            var c = new Vector2(t, u);
            var d = new Vector2(s, r);

            this.TestNegation(c, Vector2.Zero - c);
            this.TestNegation(d, Vector2.Zero - d);
        }

        /// <summary>
        /// Assert that when negating the zero vector, all the
        /// negation opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_iii ()
        {
            this.TestNegation(Vector2.Zero, Vector2.Zero);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// negation opperators and functions yield the same results as a
        /// manual negation calculation.
        /// </summary>
        [Test]
        public void TestOperator_Negation_iv ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector2();
                this.TestNegation(a, Vector2.Zero - a);
            }
        }

        // Test Operator: Multiplication //-----------------------------------//

        /// <summary>
        /// Helper method for testing multiplication.
        /// </summary>
        void TestMultiplication (Vector2 a, Vector2 b, Vector2 expected )
        {
            // This test asserts the following:
            //   a * b == expected
            //   b * a == expected

            var result_1a = a * b;
            var result_2a = b * a;

            Vector2 result_1b; Vector2.Multiply(ref a, ref b, out result_1b);
            Vector2 result_2b; Vector2.Multiply(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for a known example, all the multiplication opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Multiplication_i ()
        {
            Single r = 18;
            Single s = -54;

            Single x = 3;
            Single y = 6;
            Single z = -9;

            var a = new Vector2(x, y);
            var b = new Vector2(y, z);
            var c = new Vector2(r, s);

            this.TestMultiplication(a, b, c);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// multiplication opperators and functions yield the same results as a
        /// manual multiplication calculation.
        /// </summary>
        [Test]
        public void TestOperator_Multiplication_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector2();
                var b = GetNextRandomVector2();

                var c = new Vector2(a.X * b.X, a.Y * b.Y);

                this.TestMultiplication(a, b, c);
            }
        }


        // Test Operator: Division //-----------------------------------------//

        /// <summary>
        /// Helper method for testing division.
        /// </summary>
        void TestDivision (Vector2 a, Vector2 b, Vector2 expected )
        {
            // This test asserts the following:
            //   a / b == expected

            var result_1a = a / b;

            Vector2 result_1b; Vector2.Divide(ref a, ref b, out result_1b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for a known example using whole numbers, all the
        /// division opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Division_i ()
        {
            Single r = 10;
            Single s = -40;

            Single x = 2000;
            Single y = 200;
            Single z = -5;

            var a = new Vector2(x, y);
            var b = new Vector2(y, z);
            var c = new Vector2(r, s);

            this.TestDivision(a, b, c);
        }

        /// <summary>
        /// Assert that, for a known example using fractional numbers, all the
        /// division opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Division_ii ()
        {
            Single t = ((Single) 1 ) / ((Single) 10);
            Single u = ((Single) (-1) ) / ((Single) 40 );
            Single x = 2000;
            Single y = 200;
            Single z = -5;

            var a = new Vector2(y, z);
            var b = new Vector2(x, y);
            var c = new Vector2(t, u);

            this.TestDivision(a, b, c);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// division opperators and functions yield the same results as a
        /// manual addition division.
        /// </summary>
        [Test]
        public void TestOperator_Division_iii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector2();
                var b = GetNextRandomVector2();

                var c = new Vector2(a.X / b.X, a.Y / b.Y);

                this.TestDivision(a, b, c);
            }
        }

        // Test Static Fn: SmoothStep //--------------------------------------//

        /// <summary>
        /// This test runs a number of random scenarios and makes sure that when
        /// the weighting parameter is at it's limits the spline passes directly
        /// through the correct control points.
        /// </summary>
        [Test]
        public void TestStaticFn_SmoothStep_i ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector2();
                var b = GetNextRandomVector2();

                Single amount1 = 0;
                Vector2 result1;

                Vector2.SmoothStep (
                    ref a, ref b, ref amount1, out result1);

                AssertEqualWithinReason(result1, a);

                Single amount2 = 1;
                Vector2 result2;

                Vector2.SmoothStep (
                    ref a, ref b, ref amount2, out result2);

                AssertEqualWithinReason(result2, b);
            }
        }

        /// <summary>
        /// Assert that, for known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_SmoothStep_ii ()
        {
            var a = GetNextRandomVector2();
            var b = GetNextRandomVector2();

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for (Int32 idx = 0; idx < tests.Length; ++idx)
            {
                Vector2 result;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Vector2.SmoothStep (
                            ref a, ref b, ref tests[idx], out result)
                    );
            }
        }

        /// <summary>
        /// This tests compares results against a known example.
        /// </summary>
        [Test]
        public void TestStaticFn_SmoothStep_iii ()
        {
            var a = new Vector2( -30, -30 );
            var b = new Vector2( +30, +30 );

            Single one = 1;

            Single i;
            Maths.FromFraction(1755, 64, out i); // 27.421875

            Single j;
            Maths.FromFraction( 165,  8, out j); // 20.625

            Single k;
            Maths.FromFraction( 705, 64, out k); // 11.015625

            Single a0 = 0;
            Single a1 = (one * 1) / 8;
            Single a2 = (one * 2) / 8;
            Single a3 = (one * 3) / 8;
            Single a4 = (one * 4) / 8;
            Single a5 = (one * 5) / 8;
            Single a6 = (one * 6) / 8;
            Single a7 = (one * 7) / 8;
            Single a8 = 1;

            Vector2 r0 = a;
            Vector2 r1 = new Vector2( -i, -i );
            Vector2 r2 = new Vector2( -j, -j );
            Vector2 r3 = new Vector2( -k, -k );
            Vector2 r4 = Vector2.Zero;
            Vector2 r5 = new Vector2(  k,  k );
            Vector2 r6 = new Vector2(  j,  j );
            Vector2 r7 = new Vector2(  i,  i );
            Vector2 r8 = b;

            var knownResults = new List<Tuple<Single, Vector2>>
            {
                new Tuple<Single, Vector2>( a0, r0 ),
                new Tuple<Single, Vector2>( a1, r1 ),
                new Tuple<Single, Vector2>( a2, r2 ),
                new Tuple<Single, Vector2>( a3, r3 ),
                new Tuple<Single, Vector2>( a4, r4 ),
                new Tuple<Single, Vector2>( a5, r5 ),
                new Tuple<Single, Vector2>( a6, r6 ),
                new Tuple<Single, Vector2>( a7, r7 ),
                new Tuple<Single, Vector2>( a8, r8 ),
            };

            for (Int32 idx = 0; idx < knownResults.Count; ++idx)
            {
                Vector2 result;
                Single amount = knownResults[idx].Item1;
                Vector2 expected = knownResults[idx].Item2;

                Vector2.SmoothStep (
                    ref a, ref b, ref amount, out result);

                AssertEqualWithinReason(result, expected);
            }
        }

        // Test Static Fn: CatmullRom //--------------------------------------//

        /// <summary>
        /// This test runs a number of random scenarios and makes sure that when
        /// the weighting parameter is at it's limits the spline passes directly
        /// through the correct control points.
        /// </summary>
        [Test]
        public void TestStaticFn_CatmullRom_i ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector2();
                var b = GetNextRandomVector2();
                var c = GetNextRandomVector2();
                var d = GetNextRandomVector2();

                Single amount1 = 0;
                Vector2 result1;

                Vector2.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount1, out result1);

                AssertEqualWithinReason(result1, b);

                Single amount2 = 1;
                Vector2 result2;

                Vector2.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount2, out result2);

                AssertEqualWithinReason(result2, c);
            }
        }

        /// <summary>
        /// This tests compares results against a known example.
        /// </summary>
        [Test]
        public void TestStaticFn_CatmullRom_ii ()
        {
            var a = new Vector2( -90, +30 );
            var b = new Vector2( -30, -30 );
            var c = new Vector2( +30, +30 );
            var d = new Vector2( +90, -30 );

            Single one = 1;

            Single u = 15;
            Single v = (Single) 165  / (Single)  8; // 20.5
            Single w = (Single) 45   / (Single)  2; // 20.625
            Single x = (Single) 1755 / (Single) 64; // 27.421875
            Single y = (Single) 15   / (Single)  2; // 14.5
            Single z = (Single) 705  / (Single) 64; // 11.015625

            Single a0 = 0;
            Single a1 = (one * 1) / 8;
            Single a2 = (one * 2) / 8;
            Single a3 = (one * 3) / 8;
            Single a4 = (one * 4) / 8;
            Single a5 = (one * 5) / 8;
            Single a6 = (one * 6) / 8;
            Single a7 = (one * 7) / 8;
            Single a8 = 1;

            Vector2 r0 = b;
            Vector2 r1 = new Vector2( -w, -x );
            Vector2 r2 = new Vector2( -u, -v );
            Vector2 r3 = new Vector2( -y, -z );
            Vector2 r4 = Vector2.Zero;
            Vector2 r5 = new Vector2(  y,  z );
            Vector2 r6 = new Vector2(  u,  v );
            Vector2 r7 = new Vector2(  w,  x );
            Vector2 r8 = c;

            var knownResults = new List<Tuple<Single, Vector2>>
            {
                new Tuple<Single, Vector2>( a0, r0 ),
                new Tuple<Single, Vector2>( a1, r1 ),
                new Tuple<Single, Vector2>( a2, r2 ),
                new Tuple<Single, Vector2>( a3, r3 ),
                new Tuple<Single, Vector2>( a4, r4 ),
                new Tuple<Single, Vector2>( a5, r5 ),
                new Tuple<Single, Vector2>( a6, r6 ),
                new Tuple<Single, Vector2>( a7, r7 ),
                new Tuple<Single, Vector2>( a8, r8 ),
            };

            for(Int32 idx = 0; idx < knownResults.Count; ++idx)
            {
                Vector2 result;
                Single amount = knownResults[idx].Item1;
                Vector2 expected = knownResults[idx].Item2;

                Vector2.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount, out result);

                AssertEqualWithinReason(result, expected);
            }
        }

        /// <summary>
        /// Assert that, for known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_CatmullRom_iii ()
        {
            var a = GetNextRandomVector2();
            var b = GetNextRandomVector2();
            var c = GetNextRandomVector2();
            var d = GetNextRandomVector2();

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for(Int32 idx = 0; idx < tests.Length; ++idx)
            {
                Vector2 result;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Vector2.CatmullRom (
                            ref a, ref b, ref c, ref d, ref tests[idx], out result)
                );
            }
        }

        /// <summary>
        /// This tests compares results against an example where all the control
        /// points are in a straight line.  In this case the resulting spline
        /// should be a straight line.
        /// </summary>
        [Test]
        public void TestStaticFn_CatmullRom_iv ()
        {
            var a = new Vector2( -90, -90 );
            var b = new Vector2( -30, -30 );
            var c = new Vector2( +30, +30 );
            var d = new Vector2( +90, +90 );

            Single one = 1;

            Single a0 = 0;
            Single a1 = (one * 1) / 4;
            Single a2 = (one * 2) / 4;
            Single a3 = (one * 3) / 4;
            Single a4 = 1;

            Vector2 r0 = b;
            Vector2 r1 = new Vector2( -15, -15 );
            Vector2 r2 = Vector2.Zero;
            Vector2 r3 = new Vector2( 15, 15 );
            Vector2 r4 = c;

            var knownResults = new List<Tuple<Single, Vector2>>
            {
                new Tuple<Single, Vector2>( a0, r0 ),
                new Tuple<Single, Vector2>( a1, r1 ),
                new Tuple<Single, Vector2>( a2, r2 ),
                new Tuple<Single, Vector2>( a3, r3 ),
                new Tuple<Single, Vector2>( a4, r4 ),
            };

            for (Int32 idx = 0; idx < knownResults.Count; ++idx)
            {
                Vector2 result;
                Single amount = knownResults[idx].Item1;
                Vector2 expected = knownResults[idx].Item2;

                Vector2.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount, out result);

                AssertEqualWithinReason(result, expected);
            }
        }

        // Test Static Fn: Hermite //-----------------------------------------//

        /// <summary>
        /// This test runs a number of random scenarios and makes sure that when
        /// the weighting parameter is at it's limits the spline passes directly
        /// through the correct control points.
        /// </summary>
        [Test]
        public void TestStaticFn_Hermite_i ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a  = GetNextRandomVector2();
                var b  = GetNextRandomVector2();

                var c = GetNextRandomVector2();
                var d = GetNextRandomVector2();

                Vector2 an; Vector2.Normalise(ref c, out an);
                Vector2 bn; Vector2.Normalise(ref d, out bn);

                Single amount1 = 0;
                Vector2 result1;

                Vector2.Hermite (
                    ref a, ref an, ref b, ref bn, ref amount1, out result1);

                AssertEqualWithinReason(result1, a);

                Single amount2 = 1;
                Vector2 result2;

                Vector2.Hermite (
                    ref a, ref an, ref b, ref bn, ref amount2, out result2);

                AssertEqualWithinReason(result2, b);
            }
        }

        /// <summary>
        /// Assert that, for a known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_Hermite_ii ()
        {
            var a = GetNextRandomVector2();
            var b = GetNextRandomVector2();
            var c = GetNextRandomVector2();
            var d = GetNextRandomVector2();

            Vector2 an; Vector2.Normalise(ref c, out an);
            Vector2 bn; Vector2.Normalise(ref d, out bn);

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for (Int32 idx = 0; idx < tests.Length; ++idx)
            {
                Vector2 result;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Vector2.Hermite (
                            ref a, ref an, ref b, ref bn, ref tests[idx], out result)
                    );

            }
        }

        /// <summary>
        /// This tests compares results against a known example.
        /// </summary>
        [Test]
        public void TestStaticFn_Hermite_iii ()
        {
            var a = new Vector2( -100, +50 );
            var b = new Vector2( +100, -50 );

            var c = new Vector2( -10, +5 );
            var d = new Vector2( +10, -5 );

            Vector2 an; Vector2.Normalise(ref c, out an);
            Vector2 bn; Vector2.Normalise(ref d, out bn);

            Single one = 1;

            // 100.1953125
            Single e = (Single) 51300 / (Single) 512;

            // 50.09765625
            Single f = (Single) 12825 / (Single) 256;

            // 91.25
            Single g = (Single) 365 / (Single) 4;

            // 45.625
            Single h = (Single) 365 / (Single) 8;

            // 75.7421875
            Single i = (Single) 9695 / (Single) 128;

            // 37.87109375
            Single j = (Single) 9695 / (Single) 256;

            // 56.25
            Single k = (Single) 225 / (Single) 4;

            // 28.125
            Single l = (Single) 225 / (Single) 8;

            // 35.3515625
            Single m = (Single) 4525 / (Single) 128;

            // 17.67578125
            Single n = (Single) 4525 / (Single) 256;

            // 15.625
            Single o = (Single) 125 / (Single) 8;

            // 7.8125
            Single p = (Single) 125 / (Single) 16;

            // 0.3515625
            Single q = (Single) 45 / (Single) 128;

            // 0.17578125
            Single r = (Single) 45 / (Single) 256;

            Single a0 = 0;
            Single a1 = (one * 1) / 8;
            Single a2 = (one * 2) / 8;
            Single a3 = (one * 3) / 8;
            Single a4 = (one * 4) / 8;
            Single a5 = (one * 5) / 8;
            Single a6 = (one * 6) / 8;
            Single a7 = (one * 7) / 8;
            Single a8 = 1;

            Vector2 r0 = b;
            Vector2 r1 = new Vector2(  e, -f );
            Vector2 r2 = new Vector2(  g, -h );
            Vector2 r3 = new Vector2(  i, -j );
            Vector2 r4 = new Vector2(  k, -l );
            Vector2 r5 = new Vector2(  m, -n );
            Vector2 r6 = new Vector2(  o, -p );
            Vector2 r7 = new Vector2( -q,  r );
            Vector2 r8 = c;

            var knownResults = new List<Tuple<Single, Vector2>>
            {
                new Tuple<Single, Vector2>( a0, r0 ),
                new Tuple<Single, Vector2>( a1, r1 ),
                new Tuple<Single, Vector2>( a2, r2 ),
                new Tuple<Single, Vector2>( a3, r3 ),
                new Tuple<Single, Vector2>( a4, r4 ),
                new Tuple<Single, Vector2>( a5, r5 ),
                new Tuple<Single, Vector2>( a6, r6 ),
                new Tuple<Single, Vector2>( a7, r7 ),
                new Tuple<Single, Vector2>( a8, r8 ),
            };

            for (Int32 idx = 0; idx < knownResults.Count; ++idx)
            {
                Vector2 result;
                Single amount = knownResults[idx].Item1;
                Vector2 expected = knownResults[idx].Item2;

                Vector2.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount, out result);

                AssertEqualWithinReason(result, expected);
            }
        }


        /// <summary>
        /// Assert that, running the Min function on a number of randomly
        /// generated pairs of Vector2 objects, yields the same results as those
        /// obtained from performing a manual Min calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Min ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector2 a = GetNextRandomVector2();
                Vector2 b = a * 2;

                Vector2 result;
                Vector2.Min (ref a, ref b, out result);

                Assert.That(result.X, Is.EqualTo(a.X < b.X ? a.X : b.X ));
                Assert.That(result.Y, Is.EqualTo(a.Y < b.Y ? a.Y : b.Y ));
            }
        }

        /// <summary>
        /// Assert that, running the Max function on a number of randomly
        /// generated pairs of Vector2 objects, yields the same results as those
        /// obtained from performing a manual Max calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Max ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector2 a = GetNextRandomVector2();
                Vector2 b = GetNextRandomVector2();

                Vector2 result;
                Vector2.Max (ref a, ref b, out result);

                Assert.That(result.X, Is.EqualTo(a.X > b.X ? a.X : b.X ));
                Assert.That(result.Y, Is.EqualTo(a.Y > b.Y ? a.Y : b.Y ));
            }
        }

        /// <summary>
        /// Assert that, running the Clamp function on a number of randomly
        /// generated Vector2 objects for a given min-max range, yields
        /// results that fall within that range.
        /// </summary>
        [Test]
        public void TestStaticFn_Clamp_i ()
        {
            Vector2 min = new Vector2(-30, 1);
            Vector2 max = new Vector2(32, 130);

            for(Int32 i = 0; i < 100; ++i)
            {
                Vector2 a = GetNextRandomVector2();

                Vector2 result;
                Vector2.Clamp (ref a, ref min, ref max, out result);

                Assert.That(result.X, Is.LessThanOrEqualTo(max.X));
                Assert.That(result.Y, Is.LessThanOrEqualTo(max.Y));
                Assert.That(result.X, Is.GreaterThanOrEqualTo(min.X));
                Assert.That(result.Y, Is.GreaterThanOrEqualTo(min.Y));
            }
        }

        /// <summary>
        /// Assert that, running the Clamp function on an a Vector2 object known
        /// to fall outside of a given min-max range, yields a result that fall
        /// within that range.
        /// </summary>
        [Test]
        public void TestStaticFn_Clamp_ii ()
        {
            Vector2 min = new Vector2(-30, 1);
            Vector2 max = new Vector2(32, 130);

            Vector2 a = new Vector2(-100, 1113);

            Vector2 expected = new Vector2(-30, 130);

            Vector2 result;
            Vector2.Clamp (ref a, ref min, ref max, out result);

            Assert.That(result.X, Is.LessThanOrEqualTo(max.X));
            Assert.That(result.Y, Is.LessThanOrEqualTo(max.Y));
            Assert.That(result.X, Is.GreaterThanOrEqualTo(min.X));
            Assert.That(result.Y, Is.GreaterThanOrEqualTo(min.Y));

            AssertEqualWithinReason(result, expected);

        }

        /// <summary>
        /// Assert that, running the Lerp function on a number of randomly
        /// generated pairs of Vector2 objects for a range of weighting amounts,
        /// yields the same results as those obtained from performing a manual
        /// Lerp calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Lerp_i ()
        {
            for(Int32 j = 0; j < 100; ++j)
            {
                Single delta = j;

                delta = delta / 100;

                for(Int32 i = 0; i < 100; ++i)
                {
                    Vector2 a = GetNextRandomVector2();
                    Vector2 b = GetNextRandomVector2();

                    Vector2 result;
                    Vector2.Lerp (ref a, ref b, ref delta, out result);

                    Vector2 expected = a + ( ( b - a ) * delta );

                    AssertEqualWithinReason(result, expected);
                }
            }
        }

        /// <summary>
        /// Assert that, for a known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_Lerp_ii ()
        {
            Vector2 a = GetNextRandomVector2();
            Vector2 b = GetNextRandomVector2();

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for (Int32 i = 0; i < tests.Length; ++i)
            {
                Vector2 result;
                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Vector2.Lerp (
                            ref a, ref b, ref tests[i], out result)
                    );
            }
        }

        /// <summary>
        /// Tests that for the simple vectors the IsUnit member function
        /// returns the correct result of TRUE.
        /// </summary>
        [Test]
        public void TestStaticFn_IsUnit_i ()
        {
            Vector2 a = new Vector2( 1,  0);
            Vector2 b = new Vector2(-1,  0);
            Vector2 c = new Vector2( 0,  1);
            Vector2 d = new Vector2( 0, -1);
            Vector2 e = new Vector2( 1,  1);
            Vector2 f = new Vector2( 0,  0);

            Boolean aIsUnit;
            Boolean bIsUnit;
            Boolean cIsUnit;
            Boolean dIsUnit;
            Boolean eIsUnit;
            Boolean fIsUnit;

            Vector2.IsUnit(ref a, out aIsUnit);
            Vector2.IsUnit(ref b, out bIsUnit);
            Vector2.IsUnit(ref c, out cIsUnit);
            Vector2.IsUnit(ref d, out dIsUnit);
            Vector2.IsUnit(ref e, out eIsUnit);
            Vector2.IsUnit(ref f, out fIsUnit);

            Assert.That(aIsUnit, Is.EqualTo(true));
            Assert.That(bIsUnit, Is.EqualTo(true));
            Assert.That(cIsUnit, Is.EqualTo(true));
            Assert.That(dIsUnit, Is.EqualTo(true));

            Assert.That(eIsUnit, Is.EqualTo(false));
            Assert.That(fIsUnit, Is.EqualTo(false));
        }

        /// <summary>
        /// This test makes sure that the IsUnit member function returns the
        /// correct result of TRUE for a number of scenarios where the test
        /// vector is both random and normalised.
        /// </summary>
        [Test]
        public void TestStaticFn_IsUnit_ii ()
        {
            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector2 a = GetNextRandomVector2();

                Vector2 b; Vector2.Normalise(ref a, out b);

                Boolean bIsUnit;
                Vector2.IsUnit(ref b, out bIsUnit);

                Assert.That(bIsUnit, Is.EqualTo(true));
            }
        }

        /// <summary>
        /// This test ensures that the IsUnit member function correctly
        /// returns TRUE for a collection of vectors, all known to be of unit
        /// length.
        /// </summary>
        [Test]
        public void TestStaticFn_IsUnit_iii ()
        {
            Single radius = 1;

            Single pi; Maths.Pi(out pi);

            for( Int32 i = 0; i <= 1000; ++ i)
            {
                Single theta = 2 * pi * i * radius / 100;

                Single x = Maths.Sin(theta);
                Single y = Maths.Cos(theta);

                Vector2 a = new Vector2(x,  y);
                Boolean aIsUnit;
                Vector2.IsUnit(ref a, out aIsUnit);

                Assert.That(aIsUnit, Is.EqualTo(true));
            }
        }

        /// <summary>
        /// This test makes sure that the IsUnit member function returns the
        /// correct result of FALSE for a number of scenarios where the test
        /// vector is randomly generated and not normalised.  It's highly
        /// unlikely that the random generator will create a unit vector!
        /// </summary>
        [Test]
        public void TestStaticFn_IsUnit_iv ()
        {
            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector2 a = GetNextRandomVector2();
                Boolean aIsUnit;
                Vector2.IsUnit(ref a, out aIsUnit);
                Assert.That(aIsUnit, Is.EqualTo(false));
            }
        }


    }
    /// <summary>
    ///
    /// </summary>
    [TestFixture]
    public class Vector3Tests
    {
        /// <summary>
        /// The random number generator used for testing.
        /// </summary>
        static readonly System.Random rand;

        /// <summary>
        /// Static constructor used to ensure that the random number generator
        /// always gets initilised with the same seed, making the tests
        /// behave in a deterministic manner.
        /// </summary>
        static Vector3Tests ()
        {
            rand = new System.Random(0);
        }

        /// <summary>
        /// Helper function for getting the next random Single value.
        /// </summary>
        static Single GetNextRandomSingle ()
        {
            Single randomValue = rand.NextSingle();

            Single zero = 0;
            Single multiplier = 1000;

            randomValue *= multiplier;

            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;

            if( randomBoolean )
                randomValue = zero - randomValue;

            return randomValue;
        }

        /// <summary>
        /// Helper function for getting the next random Vector3.
        /// </summary>
        static Vector3 GetNextRandomVector3 ()
        {
            Single a = GetNextRandomSingle();
            Single b = GetNextRandomSingle();
            Single c = GetNextRandomSingle();

            return new Vector3(a, b, c);
        }

        /// <summary>
        /// Helper to encapsulate asserting that two Vector3s are equal.
        /// </summary>
        static void AssertEqualWithinReason (Vector3 a, Vector3 b)
        {
            Single tolerance; MathsTests.TestTolerance(out tolerance);

            Assert.That(a.X, Is.EqualTo(b.X).Within(tolerance));
            Assert.That(a.Y, Is.EqualTo(b.Y).Within(tolerance));
            Assert.That(a.Z, Is.EqualTo(b.Z).Within(tolerance));
        }


        // Test: StructLayout //----------------------------------------------//

        /// <summary>
        /// This test makes sure that the struct layout has been defined
        /// correctly.
        /// </summary>
        [Test]
        public void Test_StructLayout_i ()
        {
            Type t = typeof(Vector3);

            Assert.That(
                t.StructLayoutAttribute.Value,
                Is.EqualTo(LayoutKind.Sequential));
        }

        /// <summary>
        /// This test makes sure that when examining the memory addresses of the
        /// X, Y and Z member variables of a number of randomly generated
        /// Vector3 objects the results are as expected.
        /// </summary>
        [Test]
        public unsafe void Test_StructLayout_ii ()
        {
            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector3 vec = GetNextRandomVector3();

                GCHandle h_vec = GCHandle.Alloc(vec, GCHandleType.Pinned);

                IntPtr vecAddress = h_vec.AddrOfPinnedObject();

                Single[] data = new Single[3];

                // nb: when Fixed32 and Half are moved back into the main
                //     dev branch there will be need for an extension method for
                //     Marshal that will perform the copy for those types.
                MarshalHelper.Copy(vecAddress, data, 0, 3);
                Assert.That(data[0], Is.EqualTo(vec.X));
                Assert.That(data[1], Is.EqualTo(vec.Y));
                Assert.That(data[2], Is.EqualTo(vec.Z));

                h_vec.Free();
            }
        }

        // Test: Constructors //----------------------------------------------//

        /// <summary>
        /// This test goes though each public constuctor and ensures that the
        /// data members of the structure have been properly set.
        /// </summary>
        [Test]
        public void Test_Constructors_i ()
        {
            {
                // Test default values
                Vector3 a = new Vector3();
                Assert.That(a, Is.EqualTo(Vector3.Zero));
            }
            {
                // Test Vector3( Single, Single, Single )
                Single a = -189;
                Single b = 429;
                Single c = 4298;
                Vector3 d = new Vector3(a, b, c);
                Assert.That(d.X, Is.EqualTo(a));
                Assert.That(d.Y, Is.EqualTo(b));
                Assert.That(d.Z, Is.EqualTo(c));
            }
            {
                // Test Vector3( Vector2, Single )
                Vector2 a = new Vector2(-189, 429);
                Single b = 4298;
                Vector3 c = new Vector3(a, b);
                Assert.That(c.X, Is.EqualTo(a.X));
                Assert.That(c.Y, Is.EqualTo(a.Y));
                Assert.That(c.Z, Is.EqualTo(b));
            }
        }

        // Test Member Fn: ToString //----------------------------------------//

        /// <summary>
        /// For a given example, this test ensures that the ToString function
        /// yields the expected string.
        /// </summary>
        [Test]
        public void TestMemberFn_ToString_i ()
        {
            Vector3 a = new Vector3(42, -17, 13);

            String result = a.ToString();

            String expected = "{X:42 Y:-17 Z:13}";

            Assert.That(result, Is.EqualTo(expected));
        }

        // Test Member Fn: GetHashCode //-------------------------------------//

        /// <summary>
        /// Makes sure that the hashing function is good by testing 10,000
        /// random scenarios and ensuring that there are no more than 10
        /// collisions.
        /// </summary>
        [Test]
        public void TestMemberFn_GetHashCode_i ()
        {
            var hs1 = new System.Collections.Generic.HashSet<Vector3>();
            var hs2 = new System.Collections.Generic.HashSet<Int32>();

            for(Int32 i = 0; i < 10000; ++i)
            {
                var a = GetNextRandomVector3();

                hs1.Add(a);
                hs2.Add(a.GetHashCode());
            }

            Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
        }

        // Test Constant: Zero //---------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector3 initilised using the Zero constant
        /// has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_Zero_i ()
        {
            Vector3 result = Vector3.Zero;
            Vector3 expected = new Vector3(0, 0, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: One //----------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector3 initilised using the One constant
        /// has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_One_i ()
        {
            Vector3 result = Vector3.One;
            Vector3 expected = new Vector3(1, 1, 1);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: UnitX //--------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector3 initilised using the UnitX 
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_UnitX_i ()
        {
            Vector3 result = Vector3.UnitX;
            Vector3 expected = new Vector3(1, 0, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: UnitY //--------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector3 initilised using the UnitY
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_UnitY_i ()
        {
            Vector3 result = Vector3.UnitY;
            Vector3 expected = new Vector3(0, 1, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: UnitZ //--------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector3 initilised using the UnitZ
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_UnitZ_i ()
        {
            Vector3 result = Vector3.UnitZ;
            Vector3 expected = new Vector3(0, 0, 1);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: Up //-----------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector3 initilised using the Up
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_Up_i ()
        {
            Vector3 result = Vector3.Up;
            Vector3 expected = new Vector3(0, 1, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: Down //---------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector3 initilised using the Down
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_Down_i ()
        {
            Vector3 result = Vector3.Down;
            Vector3 expected = new Vector3(0, -1, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: Right //--------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector3 initilised using the Right
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_Right_i ()
        {
            Vector3 result = Vector3.Right;
            Vector3 expected = new Vector3(1, 0, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: Left //---------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector3 initilised using the Left
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_Left_i ()
        {
            Vector3 result = Vector3.Left;
            Vector3 expected = new Vector3(-1, 0, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: Forward //------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector3 initilised using the Forward
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_Forward_i ()
        {
            Vector3 result = Vector3.Forward;
            Vector3 expected = new Vector3(0, 0, -1);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: Backward //-----------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector3 initilised using the Backward
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_Backward_i ()
        {
            Vector3 result = Vector3.Backward;
            Vector3 expected = new Vector3(0, 0, 1);
            AssertEqualWithinReason(result, expected);
        }

        // Test Static Fn: Distance //----------------------------------------//

        /// <summary>
        /// Assert that, for a number of known examples, the Distance method
        /// yeilds the correct results.
        /// </summary>
        [Test]
        public void TestStaticFn_Distance_i ()
        {
            var tests = new Tuple<Vector3, Vector3, Single>[]
            {
                //a -> b -> expected
                new Tuple<Vector3, Vector3, Single> (
                    new Vector3(0, 4, 12), new Vector3(3, 0, 0), 13),

                new Tuple<Vector3, Vector3, Single> (
                    new Vector3(0, -4, 12), new Vector3(3, 0, 0), 13),

                new Tuple<Vector3, Vector3, Single> (
                    new Vector3(0, -4, -12), new Vector3(-3, 0, 0), 13),

                new Tuple<Vector3, Vector3, Single> (
                    Vector3.Zero, Vector3.Zero, 0),
            };

            for (Int32 i = 0; i < tests.Length; ++i)
            {
                var test = tests [i];
                Vector3 a = test.Item1;
                Vector3 b = test.Item2;
                Single expected = test.Item3;

                Single result;
                Vector3.Distance(ref a, ref b, out result);
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Distance method yeilds the same results as those obtained from
        /// performing a manual calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Distance_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector3 a = GetNextRandomVector3();

                Single expected =
                    Maths.Sqrt((a.X * a.X) + (a.Y * a.Y) + (a.Z * a.Z));

                Assert.That(a.Length(), Is.EqualTo(expected));
            }
        }

        // Test Static Fn: DistanceSquared //---------------------------------//

        /// <summary>
        /// Assert that, for a number of known examples, the DistanceSquared
        /// method yeilds the correct results.
        /// </summary>
        [Test]
        public void TestStaticFn_DistanceSquared_i ()
        {
            var tests = new Tuple<Vector3, Vector3, Single>[]
            {
                //a -> b -> expected
                new Tuple<Vector3, Vector3, Single> (
                    new Vector3(0, 4, 12), new Vector3(3, 0, 0), 169),

                new Tuple<Vector3, Vector3, Single> (
                    new Vector3(0, -4, 12), new Vector3(3, 0, 0), 169),

                new Tuple<Vector3, Vector3, Single> (
                    new Vector3(0, -4, 12), new Vector3(-3, 0, 0), 169),

                new Tuple<Vector3, Vector3, Single> (
                    Vector3.Zero, Vector3.Zero, 0),
            };

            for (Int32 i = 0; i < tests.Length; ++i)
            {
                var test = tests [i];
                Vector3 a = test.Item1;
                Vector3 b = test.Item2;
                Single expected = test.Item3;

                Single result;
                Vector3.DistanceSquared(ref a, ref b, out result);
                Assert.That(result, Is.EqualTo(expected));
            }
        }


        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// DistanceSquared method yeilds the same results as those obtained
        /// from performing a manual calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_DistanceSquared_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector3 a = GetNextRandomVector3();
                Vector3 b = GetNextRandomVector3();

                Vector3 c = b - a;

                Single expected = (c.X * c.X) + (c.Y * c.Y) + (c.Z * c.Z);
                Single result;

                Vector3.DistanceSquared(ref a, ref b, out result);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        // Test Static Fn: Dot //---------------------------------------------//

        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Dot method yeilds the same results as those obtained from
        /// performing a manual calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_i ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector3 a = GetNextRandomVector3();
                Vector3 b = GetNextRandomVector3();

                Single expected = (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
                Single result; Vector3.Dot(ref a, ref b, out result);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        /// <summary>
        /// Assert that two unit vectors pointing in opposing directions yeild a
        /// dot product of negative one.
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_ii ()
        {
            Vector3 a = new Vector3(0, 0, 1);
            Vector3 b = new Vector3(0, 0, -1);

            Single expected = -1;
            Single result; Vector3.Dot(ref a, ref b, out result);

            Assert.That(result, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that two unit vectors pointing in the same direction yeild a
        /// dot product of one.
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_iii ()
        {
            Vector3 a = new Vector3(0, 0, 1);
            Vector3 b = new Vector3(0, 0, 1);

            Single expected = 1;
            Single result; Vector3.Dot(ref a, ref b, out result);

            Assert.That(result, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that two perpendicular unit vectors yeild a dot product of
        /// zero.
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_iv ()
        {
            Vector3 a = new Vector3(0, 1, 0);
            Vector3 b = new Vector3(0, 0, 1);

            Single expected = 0;
            Single result; Vector3.Dot(ref a, ref b, out result);

            Assert.That(result, Is.EqualTo(expected));
        }

        // Test Static Fn: Normalise //---------------------------------------//

        /// <summary>
        /// Assert that, for a known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_Normalise_i()
        {
            {
                Vector3 a = Vector3.Zero;

                Vector3 b;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                    Vector3.Normalise(ref a, out b)
                );
            }

            {
                Vector3 a = new Vector3(
                    Single.MaxValue,
                    Single.MaxValue,
                    Single.MaxValue);

                Vector3 b;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                    Vector3.Normalise(ref a, out b)
                );
            }
        }

        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Normalise method yeilds a unit vector (with length equal to one);
        /// </summary>
        [Test]
        public void TestStaticFn_Normalise_ii ()
        {
            Single epsilon; Maths.Epsilon(out epsilon);

            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector3 a = GetNextRandomVector3();

                Vector3 b; Vector3.Normalise(ref a, out b);
                Single expected = 1;
                Single result1 = b.Length();
                Assert.That(result1, Is.EqualTo(expected).Within(epsilon));

                // The normalise function takes both a ref and out parameter,
                // need to check that if we pass in the same value as both
                // parameters we get the same results.
                Vector3 c = a;
                Vector3.Normalise(ref c, out c);
                Single result2 = c.Length();
                Assert.That(result2, Is.EqualTo(expected).Within(epsilon));
            }
        }

        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Normalise method yeilds a vector, which when multipled by the
        /// length of the original vector results in the same vector as the
        /// original vector;
        /// </summary>
        [Test]
        public void TestStaticFn_Normalise_iii ()
        {
            Single epsilon; Maths.Epsilon(out epsilon);

            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector3 a = GetNextRandomVector3();
                Single l = a.Length();
                Vector3 expected = a;

                Vector3 b; Vector3.Normalise(ref a, out b);
                Vector3 result1 = b * l;
                AssertEqualWithinReason(result1, expected);

                Vector3 c = a;

                // The normalise function takes both a ref and out parameter,
                // need to check that if we pass in the same value as both
                // parameters we get the same results.
                Vector3.Normalise(ref c, out c);
                Vector3 result2 = c * l;
                AssertEqualWithinReason(result2, expected);
            }
        }

        // Test Static Fn: Cross //-------------------------------------------//
        [Test]
        public void TestStaticFn_Cross_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        // Test Static Fn: Reflect //-----------------------------------------//

        /// <summary>
        /// Assert that, for a number of known examples, the Reflect method
        /// yeilds the correct results.
        /// </summary>
        [Test]
        public void TestStaticFn_Reflect_i ()
        {
            var a = new Vector3(20, -5, 10);
            var b = new Vector3(0, -1, 1); Vector3.Normalise(ref b, out b);
            var c = new Vector3(2, 0, -1); Vector3.Normalise(ref c, out c);
            var d = Vector3.Zero;
            var e = new Vector3(0, 1, 0);


            var ex1 = new Vector3(20, 10, -5);
            var ex2 = new Vector3(-4, -5, 22);
            var ex3 = d;

            var tests = new Tuple<Vector3, Vector3, Vector3>[]
            {
                //incident -> normal -> expected
                new Tuple<Vector3, Vector3, Vector3> (a, b, ex1),
                new Tuple<Vector3, Vector3, Vector3> (a, c, ex2),
                new Tuple<Vector3, Vector3, Vector3> (d, e, ex3),
            };

            for (Int32 i = 0; i < tests.Length; ++i)
            {
                var test = tests [i];
                Vector3 incident = test.Item1;
                Vector3 normal = test.Item2;
                Vector3 expected = test.Item3;

                Vector3 result;
                Vector3.Reflect(ref incident, ref normal, out result);
                AssertEqualWithinReason(result, expected);
            }
        }


        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Reflect method yeilds the same results as those obtained from
        /// performing a manual calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Reflect_ii ()
        {
            Single epsilon; Maths.Epsilon(out epsilon);

            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector3 a = GetNextRandomVector3();

                Vector3 b = GetNextRandomVector3();

                Vector3.Normalise(ref b, out b);

                Vector3 result;
                Vector3.Reflect(ref a, ref b, out result);

                Single dot;
                Vector3.Dot(ref a, ref b, out dot);

                Vector3 expected = a - (2 * dot * b);

                AssertEqualWithinReason(result, expected);
            }
        }

        /// <summary>
        /// Assert that an argument exception is thrown if the value passed in
        /// to the normal parameter is not normalised.
        /// </summary>
        [Test]
        public void TestStaticFn_Reflect_iii ()
        {
            Vector3 incident = GetNextRandomVector3();
            Vector3 normal = new Vector3(12, -241, 123);

            Vector3 result;

            Assert.Throws(
                typeof(ArgumentOutOfRangeException),
                () =>
                Vector3.Reflect(ref incident, ref normal, out result)
            );
        }

        // Test Static Fn: TransformMatrix44 //-------------------------------//

        /// <summary>
        ///
        /// </summary>
        [Test]
        public void TestStaticFn_TransformMatix44_i ()
        {
            Single pi; Maths.Pi (out pi);
            Single piOver2 = pi / (Single) 2;
            Single minusPi = -pi;

            Vector3 v1 = new Vector3 (10, 50, -20);

            Matrix44 rotmati = Matrix44.Identity;
            Matrix44 rotmat1; Matrix44.CreateRotationX(ref pi, out rotmat1);
            Matrix44 rotmat2; Matrix44.CreateRotationY(ref piOver2, out rotmat2);
            Matrix44 rotmat3; Matrix44.CreateRotationZ(ref minusPi, out rotmat3);
            Matrix44 rotmat4 = rotmat1 * rotmat2 * rotmat3;

            var tests = new Tuple<Vector3, Matrix44, Vector3>[]
            {
                //vector -> transform -> expected
                new Tuple<Vector3, Matrix44, Vector3>(
                    v1, rotmati, v1),

                new Tuple<Vector3, Matrix44, Vector3>(
                    v1, rotmat1, new Vector3 (10, -50, 20)),

                new Tuple<Vector3, Matrix44, Vector3>(
                    v1, rotmat2, new Vector3 (-20, 50, -10)),

                new Tuple<Vector3, Matrix44, Vector3>(
                    v1, rotmat3, new Vector3 (-10, -50, -20)),

                new Tuple<Vector3, Matrix44, Vector3>(
                    v1, rotmat4, new Vector3 (-20, 50, -10))
            };

            for (Int32 i = 0; i < tests.Length; ++i)
            {
                var test = tests [i];
                Vector3 vec = test.Item1;
                Matrix44 trans = test.Item2;
                Vector3 expected = test.Item3;

                Vector3 result;
                Vector3.Transform (ref vec, ref trans, out result);
                AssertEqualWithinReason(result, expected);
            }
        }

        // Test Static Fn: TransformNormal //---------------------------------//

        /// <summary>
        ///
        /// </summary>
        [Test]
        public void TestStaticFn_TransformNormal_i ()
        {
            Single one = 1;
            Single six = 6;
            Single eight = 8;
            Single ten = 10;
            Single root3; Maths.Root3 (out root3);
            Single pi; Maths.Pi (out pi);
            Single minusPi = -pi;
            Single piOver2 = pi / (Single) 2;
            Single oneOverRoot3 = one / root3;
            Single sixTenths = six / ten;
            Single eightTenths = eight / ten;

            // todo: find a set of 3 fraction that make up a 3D unit vector
            //       for now just use the 2D one
            Vector3 v1 = new Vector3 (sixTenths, 0, eightTenths);
            Vector3 v2 = new Vector3 (oneOverRoot3, oneOverRoot3, oneOverRoot3);

            Matrix44 rotmati = Matrix44.Identity;
            Matrix44 rotmat1; Matrix44.CreateRotationX(ref pi, out rotmat1);
            Matrix44 rotmat2; Matrix44.CreateRotationY(ref piOver2, out rotmat2);
            Matrix44 rotmat3; Matrix44.CreateRotationZ(ref minusPi, out rotmat3);
            Matrix44 rotmat4 = rotmat1 * rotmat2 * rotmat3;

            var tests = new Tuple<Vector3, Matrix44, Vector3>[]
            {
                //normal -> transform -> expected
                new Tuple<Vector3, Matrix44, Vector3>(
                    v1, rotmati, v1),

                new Tuple<Vector3, Matrix44, Vector3>(
                    v1, rotmat1, new Vector3 (sixTenths, 0, -eightTenths)),

                new Tuple<Vector3, Matrix44, Vector3>(
                    v1, rotmat2, new Vector3 (eightTenths, 0, -sixTenths)),

                new Tuple<Vector3, Matrix44, Vector3>(
                    v1, rotmat3, new Vector3 (-sixTenths, 0, eightTenths)),

                new Tuple<Vector3, Matrix44, Vector3>(
                    v1, rotmat4, new Vector3 (eightTenths, 0, -sixTenths)),

                //normal -> transform -> expected
                new Tuple<Vector3, Matrix44, Vector3>(
                    v2, rotmati, v2),

                new Tuple<Vector3, Matrix44, Vector3>(
                    v2, rotmat1, new Vector3 (oneOverRoot3, -oneOverRoot3, -oneOverRoot3)),

                new Tuple<Vector3, Matrix44, Vector3>(
                    v2, rotmat2, new Vector3 (oneOverRoot3, oneOverRoot3, -oneOverRoot3)),

                new Tuple<Vector3, Matrix44, Vector3>(
                    v2, rotmat3, new Vector3 (-oneOverRoot3, -oneOverRoot3, oneOverRoot3)),

                new Tuple<Vector3, Matrix44, Vector3>(
                    v2, rotmat4, new Vector3 (oneOverRoot3, oneOverRoot3, -oneOverRoot3)),
            };

            for (Int32 i = 0; i < tests.Length; ++i)
            {
                var test = tests [i];
                Vector3 normalVec = test.Item1;
                Matrix44 trans = test.Item2;
                Vector3 expected = test.Item3;

                Vector3 result;

                Vector3.TransformNormal (ref normalVec, ref trans, out result);
                AssertEqualWithinReason(result, expected);

                // should also work with the standard transform fn
                Vector3.Transform (ref normalVec, ref trans, out result);
                AssertEqualWithinReason(result, expected);
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Test]
        public void TestStaticFn_TransformNormal_ii ()
        {
            Matrix44 rotmat = Matrix44.Identity;
            var tests = new Vector3[]
            {
                new Vector3 (21, -532, 0),
                new Vector3 (21, +532, 243),
                new Vector3 (1, -1, 3),
                new Vector3 (-2435, -2, 25342),
            };

            for (Int32 i = 0; i < tests.Length; ++i)
            {
                var test = tests [i];
                Vector3 normal = test;
                Vector3 result;
                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                    Vector3.TransformNormal(ref normal, ref rotmat, out result)
                );
            }
        }

        // Test Static Fn: TransformQuaternion //-----------------------------//

        /// <summary>
        ///
        /// </summary>
        [Test]
        public void TestStaticFn_TransformQuaternion_i ()
        {
            Vector3 v1 = new Vector3 (10, 50, -20);

            Quaternion quatmati = new Quaternion (0, 0, 0, 1); // identity
            Quaternion quatmat1 = new Quaternion (1, 0, 0, 0);
            Quaternion quatmat2 = new Quaternion (0, 1, 0, 0);
            Quaternion quatmat3 = new Quaternion (0, 0, 1, 0);

            var tests = new Tuple<Vector3, Quaternion, Vector3>[]
            {
                //vector -> transform -> expected
                new Tuple<Vector3, Quaternion, Vector3>(
                    v1, quatmati, v1),

                new Tuple<Vector3, Quaternion, Vector3>(
                    v1, quatmat1, new Vector3 ( 10, -50,  20)),

                new Tuple<Vector3, Quaternion, Vector3>(
                    v1, quatmat2, new Vector3 (-10,  50,  20)),

                new Tuple<Vector3, Quaternion, Vector3>(
                    v1, quatmat3, new Vector3 (-10, -50, -20)),
            };

            for (Int32 i = 0; i < tests.Length; ++i)
            {
                var test = tests [i];
                Vector3 vec = test.Item1;
                Quaternion trans = test.Item2;
                Vector3 expected = test.Item3;

                Vector3 result;
                Vector3.Transform (ref vec, ref trans, out result);
                AssertEqualWithinReason(result, expected);
            }
        }

        /// <summary>
        /// Tests that for a known example the Length member function yields
        /// the correct result.
        /// </summary>
        [Test]
        public void TestStaticFn_Length_i ()
        {
            Vector3 a = new Vector3(3, -4, 12);

            Single expected = 13;

            Single result = a.Length();

            Assert.That(result, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests that for a known example the LengthSquared member function
        /// yields the correct result.
        /// </summary>
        [Test]
        public void TestStaticFn_LengthSquared_i ()
        {
            Vector3 a = new Vector3(3, -4, 12);

            Single expected = 169;

            Single result = a.LengthSquared();

            Assert.That(result, Is.EqualTo(expected));
        }

        // Test Operator: Equality //-----------------------------------------//

        /// <summary>
        /// Helper method for testing equality.
        /// </summary>
        void TestEquality (Vector3 a, Vector3 b, Boolean expected )
        {
            // This test asserts the following:
            //   (a == b) == expected
            //   (b == a) == expected
            //   (a != b) == !expected
            //   (b != a) == !expected

            Boolean result_1a = (a == b);
            Boolean result_1b = (a.Equals(b));
            Boolean result_1c = (a.Equals((Object)b));

            Boolean result_2a = (b == a);
            Boolean result_2b = (b.Equals(a));
            Boolean result_2c = (b.Equals((Object)a));

            Boolean result_3a = (a != b);
            Boolean result_4a = (b != a);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_1c, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
            Assert.That(result_2c, Is.EqualTo(expected));
            Assert.That(result_3a, Is.EqualTo(!expected));
            Assert.That(result_4a, Is.EqualTo(!expected));
        }

        /// <summary>
        /// Makes sure that, for a known example, all the equality opperators
        /// and functions yield the expected result of TRUE when two equal
        /// Vector3 objects are compared.
        /// </summary>
        [Test]
        public void TestOperator_Equality_i ()
        {
            var a = new Vector3(44, -54, -22);
            var b = new Vector3(44, -54, -22);

            Boolean expected = true;

            this.TestEquality(a, b, expected);
        }

        /// <summary>
        /// Makes sure that, for a known example, all the equality opperators
        /// and functions yield the expected result of FALSE when two unequal
        /// Vector3 objects are compared.
        /// </summary>
        [Test]
        public void TestOperator_Equality_ii ()
        {
            var a = new Vector3(44, 54, 2);
            var b = new Vector3(44, -54, 2);

            Boolean expected = false;

            this.TestEquality(a, b, expected);
        }

        /// <summary>
        /// Tests to make sure that all the equality opperators and functions
        /// yield the expected result of TRUE when used on a number of randomly
        /// generated pairs of equal Vector3 objects.
        /// </summary>
        [Test]
        public void TestOperator_Equality_iii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector3();

                Vector3 b = a;

                this.TestEquality(a, b, true);
            }
        }


        // Test Operator: Addition //-----------------------------------------//

        /// <summary>
        /// Helper method for testing addition.
        /// </summary>
        void TestAddition (Vector3 a, Vector3 b, Vector3 expected )
        {
            // This test asserts the following:
            //   a + b == expected
            //   b + a == expected

            var result_1a = a + b;
            var result_2a = b + a;

            Vector3 result_1b; Vector3.Add(ref a, ref b, out result_1b);
            Vector3 result_2b; Vector3.Add(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for a known example, all the addition opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Addition_i ()
        {
            var a = new Vector3(3, -6, 44);
            var b = new Vector3(-6, 12, 18);

            var expected = new Vector3(-3, 6, 62);

            this.TestAddition(a, b, expected);
        }

        /// <summary>
        /// Assert that, for a known example involving the zero vector, all the
        /// addition opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Addition_ii ()
        {
            var a = new Vector3(-2313, 88, 199);

            var expected = a;

            this.TestAddition(a, Vector3.Zero, expected);
        }

        /// <summary>
        /// Assert that, for a known example involving two zero vectors, all the
        /// addition opperators and functions yield the correct result of zero.
        /// </summary>
        [Test]
        public void TestOperator_Addition_iii ()
        {
            this.TestAddition(Vector3.Zero, Vector3.Zero, Vector3.Zero);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// addition opperators and functions yield the same results as a
        /// manual addition calculation.
        /// </summary>
        [Test]
        public void TestOperator_Addition_iv ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector3();
                var b = GetNextRandomVector3();

                var expected = new Vector3(
                    a.X + b.X, a.Y + b.Y, a.Z + b.Z);

                this.TestAddition(a, b, expected);
            }
        }

        // Test Operator: Subtraction //--------------------------------------//

        /// <summary>
        /// Helper method for testing subtraction.
        /// </summary>
        void TestSubtraction (Vector3 a, Vector3 b, Vector3 expected )
        {
            // This test asserts the following:
            //   a - b == expected
            //   b - a == -expected

            var result_1a = a - b;
            var result_2a = b - a;

            Vector3 result_1b; Vector3.Subtract(ref a, ref b, out result_1b);
            Vector3 result_2b; Vector3.Subtract(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(-expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(-expected));
        }

        /// <summary>
        /// Assert that, for known examples, all the subtraction opperators
        /// and functions yield the correct result.
        /// <summary>
        [Test]
        public void TestOperator_Subtraction_i ()
        {
            var a = new Vector3(12, -4, 14);
            var b = new Vector3(15, 11, 7);
            var expected = new Vector3(-3, -15, 7);
            this.TestSubtraction(a, b, expected);

            var c = new Vector3(-423, 342, 7);
            this.TestSubtraction(c, Vector3.Zero, c);
        }

        /// <summary>
        /// Assert that when subtracting the zero vector fromt the zero vector,
        /// all the subtraction opperators and functions yield the correct
        /// result.
        /// <summary>
        [Test]
        public void TestOperator_Subtraction_ii ()
        {
            this.TestSubtraction(Vector3.Zero, Vector3.Zero, Vector3.Zero);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// subtraction opperators and functions yield the same results as a
        /// manual subtraction calculation.
        /// </summary>
        [Test]
        public void TestOperator_Subtraction_iii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector3();
                var b = GetNextRandomVector3();

                var expected = new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

                this.TestSubtraction(a, b, expected);
            }
        }

        // Test Operator: Negation //-----------------------------------------//

        /// <summary>
        /// Helper method for testing negation.
        /// </summary>
        void TestNegation (Vector3 a, Vector3 expected )
        {
            // This test asserts the following:
            //   -a == expected

            var result_1a = -a;

            Vector3 result_1b; Vector3.Negate(ref a, out result_1b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for known examples, all the negation opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_i ()
        {
            Single r = 3432;
            Single s = -6218;
            Single t = -3432;
            Single u = 6218;

            var a = new Vector3(r, s, t);
            var b = new Vector3(u, t, s);
            var c = new Vector3(t, u, r);
            var d = new Vector3(s, r, u);

            this.TestNegation(a, c);
            this.TestNegation(b, d);
        }

        /// <summary>
        /// Assert that, for known examples involving the zero vector, all the
        /// negation opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_ii ()
        {
            Single t = -3432;
            Single u = 6218;
            Single r = 3432;
            Single s = -6218;

            var c = new Vector3(t, u, r);
            var d = new Vector3(s, r, u);

            this.TestNegation(c, Vector3.Zero - c);
            this.TestNegation(d, Vector3.Zero - d);
        }

        /// <summary>
        /// Assert that when negating the zero vector, all the
        /// negation opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_iii ()
        {
            this.TestNegation(Vector3.Zero, Vector3.Zero);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// negation opperators and functions yield the same results as a
        /// manual negation calculation.
        /// </summary>
        [Test]
        public void TestOperator_Negation_iv ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector3();
                this.TestNegation(a, Vector3.Zero - a);
            }
        }

        // Test Operator: Multiplication //-----------------------------------//

        /// <summary>
        /// Helper method for testing multiplication.
        /// </summary>
        void TestMultiplication (Vector3 a, Vector3 b, Vector3 expected )
        {
            // This test asserts the following:
            //   a * b == expected
            //   b * a == expected

            var result_1a = a * b;
            var result_2a = b * a;

            Vector3 result_1b; Vector3.Multiply(ref a, ref b, out result_1b);
            Vector3 result_2b; Vector3.Multiply(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for a known example, all the multiplication opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Multiplication_i ()
        {
            Single r = -27;
            Single s = 36;
            Single t = 9;

            Single x = 3;
            Single y = 6;
            Single z = -9;

            var a = new Vector3(x, y, x);
            var b = new Vector3(z, y, x);
            var c = new Vector3(r, s, t);

            this.TestMultiplication(a, b, c);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// multiplication opperators and functions yield the same results as a
        /// manual multiplication calculation.
        /// </summary>
        [Test]
        public void TestOperator_Multiplication_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector3();
                var b = GetNextRandomVector3();

                var c = new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

                this.TestMultiplication(a, b, c);
            }
        }


        // Test Operator: Division //-----------------------------------------//

        /// <summary>
        /// Helper method for testing division.
        /// </summary>
        void TestDivision (Vector3 a, Vector3 b, Vector3 expected )
        {
            // This test asserts the following:
            //   a / b == expected

            var result_1a = a / b;

            Vector3 result_1b; Vector3.Divide(ref a, ref b, out result_1b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for a known example using whole numbers, all the
        /// division opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Division_i ()
        {
            Single r = 10;
            Single s = -40;
            Single t = 1;

            Single x = 2000;
            Single y = 200;
            Single z = -5;

            var a = new Vector3(x, y, x);
            var b = new Vector3(y, z, x);
            var c = new Vector3(r, s, t);

            this.TestDivision(a, b, c);
        }

        /// <summary>
        /// Assert that, for a known example using fractional numbers, all the
        /// division opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Division_ii ()
        {
            Single t = ((Single) 1 ) / ((Single) 10);
            Single u = ((Single) (-1) ) / ((Single) 40 );
            Single v = -20;
            Single w = 100;
            Single x = 2000;
            Single y = 200;
            Single z = -5;

            var a = new Vector3(y, z, w);
            var b = new Vector3(x, y, z);
            var c = new Vector3(t, u, v);

            this.TestDivision(a, b, c);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// division opperators and functions yield the same results as a
        /// manual addition division.
        /// </summary>
        [Test]
        public void TestOperator_Division_iii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector3();
                var b = GetNextRandomVector3();

                var c = new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

                this.TestDivision(a, b, c);
            }
        }

        // Test Static Fn: SmoothStep //--------------------------------------//

        /// <summary>
        /// This test runs a number of random scenarios and makes sure that when
        /// the weighting parameter is at it's limits the spline passes directly
        /// through the correct control points.
        /// </summary>
        [Test]
        public void TestStaticFn_SmoothStep_i ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector3();
                var b = GetNextRandomVector3();

                Single amount1 = 0;
                Vector3 result1;

                Vector3.SmoothStep (
                    ref a, ref b, ref amount1, out result1);

                AssertEqualWithinReason(result1, a);

                Single amount2 = 1;
                Vector3 result2;

                Vector3.SmoothStep (
                    ref a, ref b, ref amount2, out result2);

                AssertEqualWithinReason(result2, b);
            }
        }

        /// <summary>
        /// Assert that, for known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_SmoothStep_ii ()
        {
            var a = GetNextRandomVector3();
            var b = GetNextRandomVector3();

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for (Int32 idx = 0; idx < tests.Length; ++idx)
            {
                Vector3 result;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Vector3.SmoothStep (
                            ref a, ref b, ref tests[idx], out result)
                    );
            }
        }

        /// <summary>
        /// This tests compares results against a known example.
        /// </summary>
        [Test]
        public void TestStaticFn_SmoothStep_iii ()
        {
            var a = new Vector3( -30, -30, -30 );
            var b = new Vector3( +30, +30, +30 );

            Single one = 1;

            Single i;
            Maths.FromFraction(1755, 64, out i); // 27.421875

            Single j;
            Maths.FromFraction( 165,  8, out j); // 20.625

            Single k;
            Maths.FromFraction( 705, 64, out k); // 11.015625

            Single a0 = 0;
            Single a1 = (one * 1) / 8;
            Single a2 = (one * 2) / 8;
            Single a3 = (one * 3) / 8;
            Single a4 = (one * 4) / 8;
            Single a5 = (one * 5) / 8;
            Single a6 = (one * 6) / 8;
            Single a7 = (one * 7) / 8;
            Single a8 = 1;

            Vector3 r0 = a;
            Vector3 r1 = new Vector3( -i, -i, -i );
            Vector3 r2 = new Vector3( -j, -j, -j );
            Vector3 r3 = new Vector3( -k, -k, -k );
            Vector3 r4 = Vector3.Zero;
            Vector3 r5 = new Vector3(  k,  k,  k );
            Vector3 r6 = new Vector3(  j,  j,  j );
            Vector3 r7 = new Vector3(  i,  i,  i );
            Vector3 r8 = b;

            var knownResults = new List<Tuple<Single, Vector3>>
            {
                new Tuple<Single, Vector3>( a0, r0 ),
                new Tuple<Single, Vector3>( a1, r1 ),
                new Tuple<Single, Vector3>( a2, r2 ),
                new Tuple<Single, Vector3>( a3, r3 ),
                new Tuple<Single, Vector3>( a4, r4 ),
                new Tuple<Single, Vector3>( a5, r5 ),
                new Tuple<Single, Vector3>( a6, r6 ),
                new Tuple<Single, Vector3>( a7, r7 ),
                new Tuple<Single, Vector3>( a8, r8 ),
            };

            for (Int32 idx = 0; idx < knownResults.Count; ++idx)
            {
                Vector3 result;
                Single amount = knownResults[idx].Item1;
                Vector3 expected = knownResults[idx].Item2;

                Vector3.SmoothStep (
                    ref a, ref b, ref amount, out result);

                AssertEqualWithinReason(result, expected);
            }
        }

        // Test Static Fn: CatmullRom //--------------------------------------//

        /// <summary>
        /// This test runs a number of random scenarios and makes sure that when
        /// the weighting parameter is at it's limits the spline passes directly
        /// through the correct control points.
        /// </summary>
        [Test]
        public void TestStaticFn_CatmullRom_i ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector3();
                var b = GetNextRandomVector3();
                var c = GetNextRandomVector3();
                var d = GetNextRandomVector3();

                Single amount1 = 0;
                Vector3 result1;

                Vector3.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount1, out result1);

                AssertEqualWithinReason(result1, b);

                Single amount2 = 1;
                Vector3 result2;

                Vector3.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount2, out result2);

                AssertEqualWithinReason(result2, c);
            }
        }

        /// <summary>
        /// This tests compares results against a known example.
        /// </summary>
        [Test]
        public void TestStaticFn_CatmullRom_ii ()
        {
            var a = new Vector3( -90, +30, +90 );
            var b = new Vector3( -30, -30, +30 );
            var c = new Vector3( +30, +30, -30 );
            var d = new Vector3( +90, -30, -90 );

            Single one = 1;

            Single u = 15;
            Single v = (Single) 165  / (Single)  8; // 20.5
            Single w = (Single) 45   / (Single)  2; // 20.625
            Single x = (Single) 1755 / (Single) 64; // 27.421875
            Single y = (Single) 15   / (Single)  2; // 14.5
            Single z = (Single) 705  / (Single) 64; // 11.015625

            Single a0 = 0;
            Single a1 = (one * 1) / 8;
            Single a2 = (one * 2) / 8;
            Single a3 = (one * 3) / 8;
            Single a4 = (one * 4) / 8;
            Single a5 = (one * 5) / 8;
            Single a6 = (one * 6) / 8;
            Single a7 = (one * 7) / 8;
            Single a8 = 1;

            Vector3 r0 = b;
            Vector3 r1 = new Vector3( -w, -x,  w );
            Vector3 r2 = new Vector3( -u, -v,  u );
            Vector3 r3 = new Vector3( -y, -z,  y );
            Vector3 r4 = Vector3.Zero;
            Vector3 r5 = new Vector3(  y,  z, -y );
            Vector3 r6 = new Vector3(  u,  v, -u );
            Vector3 r7 = new Vector3(  w,  x, -w );
            Vector3 r8 = c;

            var knownResults = new List<Tuple<Single, Vector3>>
            {
                new Tuple<Single, Vector3>( a0, r0 ),
                new Tuple<Single, Vector3>( a1, r1 ),
                new Tuple<Single, Vector3>( a2, r2 ),
                new Tuple<Single, Vector3>( a3, r3 ),
                new Tuple<Single, Vector3>( a4, r4 ),
                new Tuple<Single, Vector3>( a5, r5 ),
                new Tuple<Single, Vector3>( a6, r6 ),
                new Tuple<Single, Vector3>( a7, r7 ),
                new Tuple<Single, Vector3>( a8, r8 ),
            };

            for (Int32 idx = 0; idx < knownResults.Count; ++idx)
            {
                Vector3 result;
                Single amount = knownResults[idx].Item1;
                Vector3 expected = knownResults[idx].Item2;

                Vector3.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount, out result);

                AssertEqualWithinReason(result, expected);
            }
        }

        /// <summary>
        /// Assert that, for a known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_CatmullRom_iii ()
        {
            var a = GetNextRandomVector3();
            var b = GetNextRandomVector3();
            var c = GetNextRandomVector3();
            var d = GetNextRandomVector3();

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for (Int32 idx = 0; idx < tests.Length; ++idx)
            {
                Vector3 result;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Vector3.CatmullRom (
                            ref a, ref b, ref c, ref d, ref tests[idx], out result)
                );
            }
        }

        /// <summary>
        /// This tests compares results against an example where all the control
        /// points are in a straight line.  In this case the resulting spline
        /// should be a straight line.
        /// </summary>
        [Test]
        public void TestStaticFn_CatmullRom_iv ()
        {
            var a = new Vector3( -90, -90, -90 );
            var b = new Vector3( -30, -30, -30 );
            var c = new Vector3( +30, +30, +30 );
            var d = new Vector3( +90, +90, +90 );

            Single one = 1;

            Single a0 = 0;
            Single a1 = (one * 1) / 4;
            Single a2 = (one * 2) / 4;
            Single a3 = (one * 3) / 4;
            Single a4 = 1;

            Vector3 r0 = b;
            Vector3 r1 = new Vector3( -15, -15, -15 );
            Vector3 r2 = Vector3.Zero;
            Vector3 r3 = new Vector3(  15,  15,  15 );
            Vector3 r4 = c;

            var knownResults = new List<Tuple<Single, Vector3>>
            {
                new Tuple<Single, Vector3>( a0, r0 ),
                new Tuple<Single, Vector3>( a1, r1 ),
                new Tuple<Single, Vector3>( a2, r2 ),
                new Tuple<Single, Vector3>( a3, r3 ),
                new Tuple<Single, Vector3>( a4, r4 ),
            };

            for (Int32 idx = 0; idx < knownResults.Count; ++idx)
            {
                Vector3 result;
                Single amount = knownResults[idx].Item1;
                Vector3 expected = knownResults[idx].Item2;

                Vector3.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount, out result);

                AssertEqualWithinReason(result, expected);
            }
        }

        // Test Static Fn: Hermite //-----------------------------------------//

        /// <summary>
        /// This test runs a number of random scenarios and makes sure that when
        /// the weighting parameter is at it's limits the spline passes directly
        /// through the correct control points.
        /// </summary>
        [Test]
        public void TestStaticFn_Hermite_i ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a  = GetNextRandomVector3();
                var b  = GetNextRandomVector3();

                var c = GetNextRandomVector3();
                var d = GetNextRandomVector3();

                Vector3 an; Vector3.Normalise(ref c, out an);
                Vector3 bn; Vector3.Normalise(ref d, out bn);

                Single amount1 = 0;
                Vector3 result1;

                Vector3.Hermite (
                    ref a, ref an, ref b, ref bn, ref amount1, out result1);

                AssertEqualWithinReason(result1, a);

                Single amount2 = 1;
                Vector3 result2;

                Vector3.Hermite (
                    ref a, ref an, ref b, ref bn, ref amount2, out result2);

                AssertEqualWithinReason(result2, b);
            }
        }

        /// <summary>
        /// Assert that, for known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_Hermite_ii ()
        {
            var a = GetNextRandomVector3();
            var b = GetNextRandomVector3();
            var c = GetNextRandomVector3();
            var d = GetNextRandomVector3();

            Vector3 an; Vector3.Normalise(ref c, out an);
            Vector3 bn; Vector3.Normalise(ref d, out bn);

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for (Int32 idx = 0; idx < tests.Length; ++idx)
            {
                Vector3 result;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Vector3.Hermite (
                            ref a, ref an, ref b, ref bn, ref tests[idx], out result)
                    );
            }
        }

        /// <summary>
        /// This tests compares results against a known example.
        /// </summary>
        [Test]
        public void TestStaticFn_Hermite_iii ()
        {
            var a = new Vector3( -100, +50, +100 );
            var b = new Vector3( +100, -50, -100 );

            var c = new Vector3( -10, +5, +10 );
            var d = new Vector3( +10, -5, -10 );

            Vector3 an; Vector3.Normalise(ref c, out an);
            Vector3 bn; Vector3.Normalise(ref d, out bn);

            Single one = 1;

            // 100.1953125
            Single e = (Single) 51300 / (Single) 512;

            // 50.09765625
            Single f = (Single) 12825 / (Single) 256;

            // 91.25
            Single g = (Single) 365 / (Single) 4;

            // 45.625
            Single h = (Single) 365 / (Single) 8;

            // 75.7421875
            Single i = (Single) 9695 / (Single) 128;

            // 37.87109375
            Single j = (Single) 9695 / (Single) 256;

            // 56.25
            Single k = (Single) 225 / (Single) 4;

            // 28.125
            Single l = (Single) 225 / (Single) 8;

            // 35.3515625
            Single m = (Single) 4525 / (Single) 128;

            // 17.67578125
            Single n = (Single) 4525 / (Single) 256;

            // 15.625
            Single o = (Single) 125 / (Single) 8;

            // 7.8125
            Single p = (Single) 125 / (Single) 16;

            // 0.3515625
            Single q = (Single) 45 / (Single) 128;

            // 0.17578125
            Single r = (Single) 45 / (Single) 256;

            Single a0 = 0;
            Single a1 = (one * 1) / 8;
            Single a2 = (one * 2) / 8;
            Single a3 = (one * 3) / 8;
            Single a4 = (one * 4) / 8;
            Single a5 = (one * 5) / 8;
            Single a6 = (one * 6) / 8;
            Single a7 = (one * 7) / 8;
            Single a8 = 1;

            Vector3 r0 = b;
            Vector3 r1 = new Vector3(  e, -f, -e );
            Vector3 r2 = new Vector3(  g, -h, -g );
            Vector3 r3 = new Vector3(  i, -j, -i );
            Vector3 r4 = new Vector3(  k, -l, -k );
            Vector3 r5 = new Vector3(  m, -n, -m );
            Vector3 r6 = new Vector3(  o, -p, -o );
            Vector3 r7 = new Vector3( -q,  r,  q );
            Vector3 r8 = c;

            var knownResults = new List<Tuple<Single, Vector3>>
            {
                new Tuple<Single, Vector3>( a0, r0 ),
                new Tuple<Single, Vector3>( a1, r1 ),
                new Tuple<Single, Vector3>( a2, r2 ),
                new Tuple<Single, Vector3>( a3, r3 ),
                new Tuple<Single, Vector3>( a4, r4 ),
                new Tuple<Single, Vector3>( a5, r5 ),
                new Tuple<Single, Vector3>( a6, r6 ),
                new Tuple<Single, Vector3>( a7, r7 ),
                new Tuple<Single, Vector3>( a8, r8 ),
            };

            for (Int32 idx = 0; idx < knownResults.Count; ++idx)
            {
                Vector3 result;
                Single amount = knownResults[idx].Item1;
                Vector3 expected = knownResults[idx].Item2;

                Vector3.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount, out result);

                AssertEqualWithinReason(result, expected);
            }
        }


        /// <summary>
        /// Assert that, running the Min function on a number of randomly
        /// generated pairs of Vector3 objects, yields the same results as those
        /// obtained from performing a manual Min calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Min ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector3 a = GetNextRandomVector3();
                Vector3 b = a * 2;

                Vector3 result;
                Vector3.Min (ref a, ref b, out result);

                Assert.That(result.X, Is.EqualTo(a.X < b.X ? a.X : b.X ));
                Assert.That(result.Y, Is.EqualTo(a.Y < b.Y ? a.Y : b.Y ));
                Assert.That(result.Z, Is.EqualTo(a.Z < b.Z ? a.Z : b.Z ));
            }
        }

        /// <summary>
        /// Assert that, running the Max function on a number of randomly
        /// generated pairs of Vector3 objects, yields the same results as those
        /// obtained from performing a manual Max calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Max ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector3 a = GetNextRandomVector3();
                Vector3 b = GetNextRandomVector3();

                Vector3 result;
                Vector3.Max (ref a, ref b, out result);

                Assert.That(result.X, Is.EqualTo(a.X > b.X ? a.X : b.X ));
                Assert.That(result.Y, Is.EqualTo(a.Y > b.Y ? a.Y : b.Y ));
                Assert.That(result.Z, Is.EqualTo(a.Z > b.Z ? a.Z : b.Z ));
            }
        }

        /// <summary>
        /// Assert that, running the Clamp function on a number of randomly
        /// generated Vector3 objects for a given min-max range, yields
        /// results that fall within that range.
        /// </summary>
        [Test]
        public void TestStaticFn_Clamp_i ()
        {
            Vector3 min = new Vector3(-30, 1, 18);
            Vector3 max = new Vector3(32, 130, 47);

            for(Int32 i = 0; i < 100; ++i)
            {
                Vector3 a = GetNextRandomVector3();

                Vector3 result;
                Vector3.Clamp (ref a, ref min, ref max, out result);

                Assert.That(result.X, Is.LessThanOrEqualTo(max.X));
                Assert.That(result.Y, Is.LessThanOrEqualTo(max.Y));
                Assert.That(result.X, Is.GreaterThanOrEqualTo(min.X));
                Assert.That(result.Y, Is.GreaterThanOrEqualTo(min.Y));
            }
        }

        /// <summary>
        /// Assert that, running the Clamp function on an a Vector3 object known
        /// to fall outside of a given min-max range, yields a result that fall
        /// within that range.
        /// </summary>
        [Test]
        public void TestStaticFn_Clamp_ii ()
        {
            Vector3 min = new Vector3(-30, 1, 18);
            Vector3 max = new Vector3(32, 130, 47);

            Vector3 a = new Vector3(-100, 1113, 50);

            Vector3 expected = new Vector3(-30, 130, 47);

            Vector3 result;
            Vector3.Clamp (ref a, ref min, ref max, out result);

            Assert.That(result.X, Is.LessThanOrEqualTo(max.X));
            Assert.That(result.Y, Is.LessThanOrEqualTo(max.Y));
            Assert.That(result.Z, Is.LessThanOrEqualTo(max.Z));
            Assert.That(result.X, Is.GreaterThanOrEqualTo(min.X));
            Assert.That(result.Y, Is.GreaterThanOrEqualTo(min.Y));
            Assert.That(result.Z, Is.GreaterThanOrEqualTo(min.Z));

            AssertEqualWithinReason(result, expected);

        }

        /// <summary>
        /// Assert that, running the Lerp function on a number of randomly
        /// generated pairs of Vector3 objects for a range of weighting amounts,
        /// yields the same results as those obtained from performing a manual
        /// Lerp calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Lerp_i ()
        {
            for(Int32 j = 0; j < 100; ++j)
            {
                Single delta = j;

                delta = delta / 100;

                for(Int32 i = 0; i < 100; ++i)
                {
                    Vector3 a = GetNextRandomVector3();
                    Vector3 b = GetNextRandomVector3();

                    Vector3 result;
                    Vector3.Lerp (ref a, ref b, ref delta, out result);

                    Vector3 expected = a + ( ( b - a ) * delta );

                    AssertEqualWithinReason(result, expected);
                }
            }
        }

        /// <summary>
        /// Assert that, for a known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_Lerp_ii ()
        {
            Vector3 a = GetNextRandomVector3();
            Vector3 b = GetNextRandomVector3();

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for( Int32 i = 0; i < tests.Length; ++i )
            {
                Vector3 result;
                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Vector3.Lerp (
                            ref a, ref b, ref tests[i], out result)
                    );
            }
        }

        /// <summary>
        /// Tests that for the simple vectors the IsUnit member function
        /// returns the correct result of TRUE.
        /// </summary>
        [Test]
        public void TestStaticFn_IsUnit_i ()
        {
            Vector3 a = new Vector3( 1,  0,  0);
            Vector3 b = new Vector3(-1,  0,  0);
            Vector3 c = new Vector3( 0,  1,  0);
            Vector3 d = new Vector3( 0, -1,  0);
            Vector3 e = new Vector3( 0,  0,  1);
            Vector3 f = new Vector3( 0,  0, -1);
            Vector3 g = new Vector3( 1,  1,  1);
            Vector3 h = new Vector3( 0,  0,  0);

            Assert.That(a.IsUnit(), Is.EqualTo(true));
            Assert.That(b.IsUnit(), Is.EqualTo(true));
            Assert.That(c.IsUnit(), Is.EqualTo(true));
            Assert.That(d.IsUnit(), Is.EqualTo(true));
            Assert.That(e.IsUnit(), Is.EqualTo(true));
            Assert.That(f.IsUnit(), Is.EqualTo(true));

            Assert.That(g.IsUnit(), Is.EqualTo(false));
            Assert.That(h.IsUnit(), Is.EqualTo(false));
        }

        /// <summary>
        /// This test makes sure that the IsUnit member function returns the
        /// correct result of TRUE for a number of scenarios where the test
        /// vector is both random and normalised.
        /// </summary>
        [Test]
        public void TestStaticFn_IsUnit_ii ()
        {
            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector3 a = GetNextRandomVector3();

                Vector3 b; Vector3.Normalise(ref a, out b);

                Assert.That(b.IsUnit(), Is.EqualTo(true));
            }
        }

        /// <summary>
        /// This test ensures that the IsUnit member function correctly
        /// returns TRUE for a collection of vectors, all known to be of unit
        /// length.
        /// </summary>
        [Test]
        public void TestStaticFn_IsUnit_iii ()
        {
            Single radius = 1;

            Single pi; Maths.Pi(out pi);

            for( Int32 i = 0; i <= 31; ++ i)
            {
                for( Int32 j = 0; j <= 31; ++ j)
                {
                    Single theta = 2 * pi * i / 100;
                    Single phi = 2 * pi * j / 100;

                    Single x =
                        Maths.Cos(theta) *
                        Maths.Sin(phi) * radius;

                    Single y =
                        Maths.Sin(theta) *
                        Maths.Sin(phi) * radius;

                    Single z =
                        Maths.Cos(phi) * radius;

                    Assert.That(
                        new Vector3( x,  y,  z).IsUnit(),
                        Is.EqualTo(true));
                }
            }
        }

        /// <summary>
        /// This test makes sure that the IsUnit member function returns the
        /// correct result of FALSE for a number of scenarios where the test
        /// vector is randomly generated and not normalised.  It's highly
        /// unlikely that the random generator will create a unit vector!
        /// </summary>
        [Test]
        public void TestStaticFn_IsUnit_iv ()
        {
            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector3 a = GetNextRandomVector3();

                Assert.That(a.IsUnit(), Is.EqualTo(false));
            }
        }


    }
    /// <summary>
    ///
    /// </summary>
    [TestFixture]
    public class Vector4Tests
    {
        /// <summary>
        /// The random number generator used for testing.
        /// </summary>
        static readonly System.Random rand;

        /// <summary>
        /// Static constructor used to ensure that the random number generator
        /// always gets initilised with the same seed, making the tests
        /// behave in a deterministic manner.
        /// </summary>
        static Vector4Tests ()
        {
            rand = new System.Random(0);
        }

        /// <summary>
        /// Helper function for getting the next random Single value.
        /// </summary>
        static Single GetNextRandomSingle ()
        {
            Single randomValue = rand.NextSingle();

            Single zero = 0;
            Single multiplier = 1000;

            randomValue *= multiplier;

            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;

            if( randomBoolean )
                randomValue = zero - randomValue;

            return randomValue;
        }

        /// <summary>
        /// Helper function for getting the next random Vector4.
        /// </summary>
        static Vector4 GetNextRandomVector4 ()
        {
            Single a = GetNextRandomSingle();
            Single b = GetNextRandomSingle();
            Single c = GetNextRandomSingle();
            Single d = GetNextRandomSingle();

            return new Vector4(a, b, c, d);
        }

        /// <summary>
        /// Helper to encapsulate asserting that two Vector4s are equal.
        /// </summary>
        static void AssertEqualWithinReason (Vector4 a, Vector4 b)
        {
            Single tolerance; MathsTests.TestTolerance(out tolerance);

            Assert.That(a.X, Is.EqualTo(b.X).Within(tolerance));
            Assert.That(a.Y, Is.EqualTo(b.Y).Within(tolerance));
            Assert.That(a.Z, Is.EqualTo(b.Z).Within(tolerance));
            Assert.That(a.W, Is.EqualTo(b.W).Within(tolerance));
        }


        // Test: StructLayout //----------------------------------------------//

        /// <summary>
        /// This test makes sure that the struct layout has been defined
        /// correctly.
        /// </summary>
        [Test]
        public void Test_StructLayout_i ()
        {
            Type t = typeof(Vector4);

            Assert.That(
                t.StructLayoutAttribute.Value,
                Is.EqualTo(LayoutKind.Sequential));
        }

        /// <summary>
        /// This test makes sure that when examining the memory addresses of the
        /// X, Y, Z and W member variables of a number of randomly generated
        /// Vector4 objects the results are as expected.
        /// </summary>
        [Test]
        public unsafe void Test_StructLayout_ii ()
        {
            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector4 vec = GetNextRandomVector4();

                GCHandle h_vec = GCHandle.Alloc(vec, GCHandleType.Pinned);

                IntPtr vecAddress = h_vec.AddrOfPinnedObject();

                Single[] data = new Single[4];

                // nb: when Fixed32 and Half are moved back into the main
                //     dev branch there will be need for an extension method for
                //     Marshal that will perform the copy for those types.
                MarshalHelper.Copy(vecAddress, data, 0, 4);
                Assert.That(data[0], Is.EqualTo(vec.X));
                Assert.That(data[1], Is.EqualTo(vec.Y));
                Assert.That(data[2], Is.EqualTo(vec.Z));
                Assert.That(data[3], Is.EqualTo(vec.W));

                h_vec.Free();
            }
        }

        // Test: Constructors //----------------------------------------------//

        /// <summary>
        /// This test goes though each public constuctor and ensures that the
        /// data members of the structure have been properly set.
        /// </summary>
        [Test]
        public void Test_Constructors_i ()
        {
            {
                // Test default values
                Vector4 a = new Vector4();
                Assert.That(a, Is.EqualTo(Vector4.Zero));
            }
            {
                // Test Vector4( Single, Single, Single, Single )
                Single a = -189;
                Single b = 429;
                Single c = 4298;
                Single d = 341;
                Vector4 e = new Vector4(a, b, c, d);
                Assert.That(e.X, Is.EqualTo(a));
                Assert.That(e.Y, Is.EqualTo(b));
                Assert.That(e.Z, Is.EqualTo(c));
                Assert.That(e.W, Is.EqualTo(d));
            }
            {
                // Test Vector4( Vector2, Single, Single )
                Vector2 a = new Vector2(-189, 429);
                Single b = 4298;
                Single c = 341;
                Vector4 d = new Vector4(a, b, c);
                Assert.That(d.X, Is.EqualTo(a.X));
                Assert.That(d.Y, Is.EqualTo(a.Y));
                Assert.That(d.Z, Is.EqualTo(b));
                Assert.That(d.W, Is.EqualTo(c));
            }
            {
                // Test Vector4( Vector3, Single )
                Vector3 a = new Vector3(-189, 429, 4298);
                Single b = 341;
                Vector4 c = new Vector4(a, b);
                Assert.That(c.X, Is.EqualTo(a.X));
                Assert.That(c.Y, Is.EqualTo(a.Y));
                Assert.That(c.Z, Is.EqualTo(a.Z));
                Assert.That(c.W, Is.EqualTo(b));
            }
        }

        // Test Member Fn: ToString //----------------------------------------//

        /// <summary>
        /// For a given example, this test ensures that the ToString function
        /// yields the expected string.
        /// </summary>
        [Test]
        public void TestMemberFn_ToString_i ()
        {
            Vector4 a = new Vector4(42, -17, 13, 44);

            String result = a.ToString();

            String expected = "{X:42 Y:-17 Z:13 W:44}";

            Assert.That(result, Is.EqualTo(expected));
        }

        // Test Member Fn: GetHashCode //-------------------------------------//

        /// <summary>
        /// Makes sure that the hashing function is good by testing 10,000
        /// random scenarios and ensuring that there are no more than 10
        /// collisions.
        /// </summary>
        [Test]
        public void TestMemberFn_GetHashCode_i ()
        {
            var hs1 = new System.Collections.Generic.HashSet<Vector4>();
            var hs2 = new System.Collections.Generic.HashSet<Int32>();

            for(Int32 i = 0; i < 10000; ++i)
            {
                var a = GetNextRandomVector4();

                hs1.Add(a);
                hs2.Add(a.GetHashCode());
            }

            Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
        }

        // Test Constant: Zero //---------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector4 initilised using the Zero constant
        /// has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_Zero_i ()
        {
            Vector4 result = Vector4.Zero;
            Vector4 expected = new Vector4(0, 0, 0, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: One //----------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector4 initilised using the One constant
        /// has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_One_i ()
        {
            Vector4 result = Vector4.One;
            Vector4 expected = new Vector4(1, 1, 1, 1);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: UnitX //--------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector4 initilised using the UnitX 
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_UnitX_i ()
        {
            Vector4 result = Vector4.UnitX;
            Vector4 expected = new Vector4(1, 0, 0, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: UnitY //--------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector4 initilised using the UnitY
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_UnitY_i ()
        {
            Vector4 result = Vector4.UnitY;
            Vector4 expected = new Vector4(0, 1, 0, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: UnitZ //--------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector4 initilised using the UnitZ 
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_UnitZ_i ()
        {
            Vector4 result = Vector4.UnitZ;
            Vector4 expected = new Vector4(0, 0, 1, 0);
            AssertEqualWithinReason(result, expected);
        }

        // Test Constant: UnitW //--------------------------------------------//

        /// <summary>
        /// Tests to make sure that a Vector4 initilised using the UnitW
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_UnitW_i ()
        {
            Vector4 result = Vector4.UnitW;
            Vector4 expected = new Vector4(0, 0, 0, 1);
            AssertEqualWithinReason(result, expected);
        }

        // Test Static Fn: Distance //----------------------------------------//

        /// <summary>
        /// Assert that, for a number of known examples, the Distance method
        /// yeilds the correct results.
        /// </summary>
        [Test]
        public void TestStaticFn_Distance_i ()
        {
            var tests = new Tuple<Vector4, Vector4, Single>[]
            {
                //a -> b -> expected
                new Tuple<Vector4, Vector4, Single> (
                    new Vector4(0, 4, 12, 0), new Vector4(3, 0, 0, 84), 85),

                new Tuple<Vector4, Vector4, Single> (
                    new Vector4(0, -4, 12, 0), new Vector4(3, 0, 0, 84), 85),

                new Tuple<Vector4, Vector4, Single> (
                    new Vector4(0, -4, -12, 0), new Vector4(-3, 0, 0, -84), 85),

                new Tuple<Vector4, Vector4, Single> (
                    Vector4.Zero, Vector4.Zero, 0),
            };

            foreach(var test in tests)
            {
                Vector4 a = test.Item1;
                Vector4 b = test.Item2;
                Single expected = test.Item3;

                Single result;
                Vector4.Distance(ref a, ref b, out result);
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Distance method yeilds the same results as those obtained from
        /// performing a manual calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Distance_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector4 a = GetNextRandomVector4();

                Single expected =
                    Maths.Sqrt(
                        (a.X * a.X) + (a.Y * a.Y) +
                        (a.Z * a.Z) + (a.W * a.W));

                Assert.That(a.Length(), Is.EqualTo(expected));
            }
        }

        // Test Static Fn: DistanceSquared //---------------------------------//

        /// <summary>
        /// Assert that, for a number of known examples, the DistanceSquared
        /// method yeilds the correct results.
        /// </summary>
        [Test]
        public void TestStaticFn_DistanceSquared_i ()
        {
            var tests = new Tuple<Vector4, Vector4, Single>[]
            {
                //a -> b -> expected
                new Tuple<Vector4, Vector4, Single> (
                    new Vector4(0, 4, 12, 0), new Vector4(3, 0, 0, 84), 7225),

                new Tuple<Vector4, Vector4, Single> (
                    new Vector4(0, -4, 12, 0), new Vector4(3, 0, 0, 84), 7225),

                new Tuple<Vector4, Vector4, Single> (
                    new Vector4(0, -4, -12, 0), new Vector4(-3, 0, 0, -84), 7225),

                new Tuple<Vector4, Vector4, Single> (
                    Vector4.Zero, Vector4.Zero, 0),
            };

            foreach(var test in tests)
            {
                Vector4 a = test.Item1;
                Vector4 b = test.Item2;
                Single expected = test.Item3;

                Single result;
                Vector4.DistanceSquared(ref a, ref b, out result);
                Assert.That(result, Is.EqualTo(expected));
            }
        }


        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// DistanceSquared method yeilds the same results as those obtained
        /// from performing a manual calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_DistanceSquared_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector4 a = GetNextRandomVector4();
                Vector4 b = GetNextRandomVector4();

                Vector4 c = b - a;

                Single expected =
                    (c.X * c.X) + (c.Y * c.Y) + (c.Z * c.Z) + (c.W * c.W);
                Single result;

                Vector4.DistanceSquared(ref a, ref b, out result);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        // Test Static Fn: Dot //---------------------------------------------//

        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Dot method yeilds the same results as those obtained from
        /// performing a manual calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_i ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector4 a = GetNextRandomVector4();
                Vector4 b = GetNextRandomVector4();

                Single expected =
                    (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z) + (a.W * b.W);

                Single result; Vector4.Dot(ref a, ref b, out result);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        /// <summary>
        /// Assert that two unit vectors pointing in opposing directions yeild a
        /// dot product of negative one.
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_ii ()
        {
            Vector4 a = new Vector4(0, 0, 0, 1);
            Vector4 b = new Vector4(0, 0, 0, -1);

            Single expected = -1;
            Single result; Vector4.Dot(ref a, ref b, out result);

            Assert.That(result, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that two unit vectors pointing in the same direction yeild a
        /// dot product of one.
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_iii ()
        {
            Vector4 a = new Vector4(0, 0, 0, 1);
            Vector4 b = new Vector4(0, 0, 0, 1);

            Single expected = 1;
            Single result; Vector4.Dot(ref a, ref b, out result);

            Assert.That(result, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that two perpendicular unit vectors yeild a dot product of
        /// zero.
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_iv ()
        {
            Vector4 a = new Vector4(0, 0, 1, 0);
            Vector4 b = new Vector4(0, 0, 0, 1);

            Single expected = 0;
            Single result; Vector4.Dot(ref a, ref b, out result);

            Assert.That(result, Is.EqualTo(expected));
        }

        // Test Static Fn: Normalise //---------------------------------------//

        /// <summary>
        /// Assert that, for a known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_Normalise_i()
        {
            {
                Vector4 a = Vector4.Zero;

                Vector4 b;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                    Vector4.Normalise(ref a, out b)
                );
            }

            {
                Vector4 a = new Vector4(
                    Single.MaxValue,
                    Single.MaxValue,
                    Single.MaxValue,
                    Single.MaxValue);

                Vector4 b;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                    Vector4.Normalise(ref a, out b)
                );
            }
        }

        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Normalise method yeilds a unit vector (with length equal to one);
        /// </summary>
        [Test]
        public void TestStaticFn_Normalise_ii ()
        {
            Single epsilon; Maths.Epsilon(out epsilon);

            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector4 a = GetNextRandomVector4();

                Vector4 b; Vector4.Normalise(ref a, out b);
                Single expected = 1;
                Single result1 = b.Length();
                Assert.That(result1, Is.EqualTo(expected).Within(epsilon));

                // The normalise function takes both a ref and out parameter,
                // need to check that if we pass in the same value as both
                // parameters we get the same results.
                Vector4 c = a;
                Vector4.Normalise(ref c, out c);
                Single result2 = c.Length();
                Assert.That(result2, Is.EqualTo(expected).Within(epsilon));
            }
        }

        /// <summary>
        /// Assert that, for a number of randomly generated examples, the
        /// Normalise method yeilds a vector, which when multipled by the
        /// length of the original vector results in the same vector as the
        /// original vector;
        /// </summary>
        [Test]
        public void TestStaticFn_Normalise_iii ()
        {
            Single epsilon; Maths.Epsilon(out epsilon);

            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector4 a = GetNextRandomVector4();
                Single l = a.Length();
                Vector4 expected = a;

                Vector4 b; Vector4.Normalise(ref a, out b);
                Vector4 result1 = b * l;
                AssertEqualWithinReason(result1, expected);

                Vector4 c = a;

                // The normalise function takes both a ref and out parameter,
                // need to check that if we pass in the same value as both
                // parameters we get the same results.
                Vector4.Normalise(ref c, out c);
                Vector4 result2 = c * l;
                AssertEqualWithinReason(result2, expected);
            }
        }

        // Test Static Fn: TransformMatrix44 //-------------------------------//

        /// <summary>
        ///
        /// </summary>
        [Test]
        public void TestStaticFn_TransformMatix44_i ()
        {
            Single pi; Maths.Pi (out pi);
            Single piOver2 = pi / (Single) 2;
            Single minusPi = -pi;

            Vector4 v1 = new Vector4 (10, 50, -20, 100);

            Matrix44 rotmati = Matrix44.Identity;
            Matrix44 rotmat1; Matrix44.CreateRotationX(ref pi, out rotmat1);
            Matrix44 rotmat2; Matrix44.CreateRotationY(ref piOver2, out rotmat2);
            Matrix44 rotmat3; Matrix44.CreateRotationZ(ref minusPi, out rotmat3);
            Matrix44 rotmat4 = rotmat1 * rotmat2 * rotmat3;

            var tests = new Tuple<Vector4, Matrix44, Vector4>[]
            {
                //vector -> transform -> expected
                new Tuple<Vector4, Matrix44, Vector4>(
                    v1, rotmati, v1),

                new Tuple<Vector4, Matrix44, Vector4>(
                    v1, rotmat1, new Vector4 (10, -50, 20, 100)),

                new Tuple<Vector4, Matrix44, Vector4>(
                    v1, rotmat2, new Vector4 (-20, 50, -10, 100)),

                new Tuple<Vector4, Matrix44, Vector4>(
                    v1, rotmat3, new Vector4 (-10, -50, -20, 100)),

                new Tuple<Vector4, Matrix44, Vector4>(
                    v1, rotmat4, new Vector4 (-20, 50, -10, 100))
            };

            foreach (var test in tests)
            {
                Vector4 vec = test.Item1;
                Matrix44 trans = test.Item2;
                Vector4 expected = test.Item3;

                Vector4 result;
                Vector4.Transform (ref vec, ref trans, out result);
                AssertEqualWithinReason(result, expected);
            }
        }

        // Test Static Fn: TransformNormal //---------------------------------//

        /// <summary>
        ///
        /// </summary>
        [Test]
        public void TestStaticFn_TransformNormal_i ()
        {
            Single six = 6;
            Single eight = 8;
            Single ten = 10;
            Single pi; Maths.Pi (out pi);
            Single minusPi = -pi;
            Single piOver2 = pi / (Single) 2;
            Single oneOverRoot4; Maths.Half (out oneOverRoot4);
            Single sixTenths = six / ten;
            Single eightTenths = eight / ten;

            // todo: find a set of 3 fraction that make up a 4D unit vector
            //       for now just use the 2D one
            Vector4 v1 = new Vector4 (0, sixTenths, 0, eightTenths);
            Vector4 v2 = new Vector4 (
                oneOverRoot4, oneOverRoot4, oneOverRoot4, oneOverRoot4);

            Matrix44 rotmati = Matrix44.Identity;
            Matrix44 rotmat1; Matrix44.CreateRotationX(ref pi, out rotmat1);
            Matrix44 rotmat2; Matrix44.CreateRotationY(ref piOver2, out rotmat2);
            Matrix44 rotmat3; Matrix44.CreateRotationZ(ref minusPi, out rotmat3);
            Matrix44 rotmat4 = rotmat1 * rotmat2 * rotmat3;

            var tests = new Tuple<Vector4, Matrix44, Vector4>[]
            {
                //normal -> transform -> expected
                new Tuple<Vector4, Matrix44, Vector4>(
                    v1, rotmati, v1),

                new Tuple<Vector4, Matrix44, Vector4>(
                    v1, rotmat1, new Vector4 (0, -sixTenths, 0, eightTenths)),

                new Tuple<Vector4, Matrix44, Vector4>(
                    v1, rotmat2, new Vector4 (0, sixTenths, 0, eightTenths)),

                new Tuple<Vector4, Matrix44, Vector4>(
                    v1, rotmat3, new Vector4 (0, -sixTenths, 0, eightTenths)),

                new Tuple<Vector4, Matrix44, Vector4>(
                    v1, rotmat4, new Vector4 (0, sixTenths, 0, eightTenths)),

                //normal -> transform -> expected
                new Tuple<Vector4, Matrix44, Vector4>(
                    v2, rotmati, v2),

                new Tuple<Vector4, Matrix44, Vector4>(
                    v2, rotmat1, new Vector4 (
                        + oneOverRoot4,
                        - oneOverRoot4,
                        - oneOverRoot4,
                        + oneOverRoot4)),

                new Tuple<Vector4, Matrix44, Vector4>(
                    v2, rotmat2, new Vector4 (
                        + oneOverRoot4,
                        + oneOverRoot4,
                        - oneOverRoot4,
                        + oneOverRoot4)),

                new Tuple<Vector4, Matrix44, Vector4>(
                    v2, rotmat3, new Vector4 (
                        - oneOverRoot4,
                        - oneOverRoot4,
                        + oneOverRoot4,
                        + oneOverRoot4)),

                new Tuple<Vector4, Matrix44, Vector4>(
                    v2, rotmat4, new Vector4 (
                        + oneOverRoot4,
                        + oneOverRoot4,
                        - oneOverRoot4,
                        + oneOverRoot4))
            };

            foreach (var test in tests)
            {
                Vector4 normalVec = test.Item1;
                Matrix44 trans = test.Item2;
                Vector4 expected = test.Item3;

                Vector4 result;

                Vector4.TransformNormal (ref normalVec, ref trans, out result);
                AssertEqualWithinReason(result, expected);

                // should also work with the standard transform fn
                Vector4.Transform (ref normalVec, ref trans, out result);
                AssertEqualWithinReason(result, expected);
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Test]
        public void TestStaticFn_TransformNormal_ii ()
        {
            Matrix44 rotmat = Matrix44.Identity;
            Vector4 normal = new Vector4 (21, -532, 0, 91);
            Vector4 result;
            Assert.Throws(
                typeof(ArgumentOutOfRangeException),
                () =>
                Vector4.TransformNormal(ref normal, ref rotmat, out result)
            );
        }

        // Test Static Fn: TransformQuaternion //-----------------------------//

        /// <summary>
        ///
        /// </summary>
        [Test]
        public void TestStaticFn_TransformQuaternion_i ()
        {
            Vector4 v1 = new Vector4 (10, 50, -20, 100);

            Quaternion quatmati = new Quaternion (0, 0, 0, 1); // identity
            Quaternion quatmat1 = new Quaternion (1, 0, 0, 0);
            Quaternion quatmat2 = new Quaternion (0, 1, 0, 0);
            Quaternion quatmat3 = new Quaternion (0, 0, 1, 0);

            var tests = new Tuple<Vector4, Quaternion, Vector4>[]
            {
                //vector -> transform -> expected
                new Tuple<Vector4, Quaternion, Vector4>(
                    v1, quatmati, v1),

                new Tuple<Vector4, Quaternion, Vector4>(
                    v1, quatmat1, new Vector4 ( 10, -50,  20, 100)),

                new Tuple<Vector4, Quaternion, Vector4>(
                    v1, quatmat2, new Vector4 (-10,  50,  20, 100)),

                new Tuple<Vector4, Quaternion, Vector4>(
                    v1, quatmat3, new Vector4 (-10, -50, -20, 100)),
            };

            foreach (var test in tests)
            {
                Vector4 vec = test.Item1;
                Quaternion trans = test.Item2;
                Vector4 expected = test.Item3;

                Vector4 result;
                Vector4.Transform (ref vec, ref trans, out result);
                AssertEqualWithinReason(result, expected);
            }
        }

        /// <summary>
        /// Tests that for a known example the Length member function yields
        /// the correct result.
        /// </summary>
        [Test]
        public void TestStaticFn_Length_i ()
        {
            Vector4 a = new Vector4(3, -4, 12, 84);

            Single expected = 85;

            Single result = a.Length();

            Assert.That(result, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests that for a known example the LengthSquared member function
        /// yields the correct result.
        /// </summary>
        [Test]
        public void TestStaticFn_LengthSquared_i ()
        {
            Vector4 a = new Vector4(3, -4, 12, 84);

            Single expected = 7225;

            Single result = a.LengthSquared();

            Assert.That(result, Is.EqualTo(expected));
        }

        // Test Operator: Equality //-----------------------------------------//

        /// <summary>
        /// Helper method for testing equality.
        /// </summary>
        void TestEquality (Vector4 a, Vector4 b, Boolean expected )
        {
            // This test asserts the following:
            //   (a == b) == expected
            //   (b == a) == expected
            //   (a != b) == !expected
            //   (b != a) == !expected

            Boolean result_1a = (a == b);
            Boolean result_1b = (a.Equals(b));
            Boolean result_1c = (a.Equals((Object)b));

            Boolean result_2a = (b == a);
            Boolean result_2b = (b.Equals(a));
            Boolean result_2c = (b.Equals((Object)a));

            Boolean result_3a = (a != b);
            Boolean result_4a = (b != a);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_1c, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
            Assert.That(result_2c, Is.EqualTo(expected));
            Assert.That(result_3a, Is.EqualTo(!expected));
            Assert.That(result_4a, Is.EqualTo(!expected));
        }

        /// <summary>
        /// Makes sure that, for a known example, all the equality opperators
        /// and functions yield the expected result of TRUE when two equal
        /// Vector4 objects are compared.
        /// </summary>
        [Test]
        public void TestOperator_Equality_i ()
        {
            var a = new Vector4(44, -54, -22, 11);
            var b = new Vector4(44, -54, -22, 11);

            Boolean expected = true;

            this.TestEquality(a, b, expected);
        }

        /// <summary>
        /// Makes sure that, for a known example, all the equality opperators
        /// and functions yield the expected result of FALSE when two unequal
        /// Vector4 objects are compared.
        /// </summary>
        [Test]
        public void TestOperator_Equality_ii ()
        {
            var a = new Vector4(44, 54, 2, 11);
            var b = new Vector4(44, -54, 2, -1);

            Boolean expected = false;

            this.TestEquality(a, b, expected);
        }

        /// <summary>
        /// Tests to make sure that all the equality opperators and functions
        /// yield the expected result of TRUE when used on a number of randomly
        /// generated pairs of equal Vector4 objects.
        /// </summary>
        [Test]
        public void TestOperator_Equality_iii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector4();

                Vector4 b = a;

                this.TestEquality(a, b, true);
            }
        }


        // Test Operator: Addition //-----------------------------------------//

        /// <summary>
        /// Helper method for testing addition.
        /// </summary>
        void TestAddition (Vector4 a, Vector4 b, Vector4 expected )
        {
            // This test asserts the following:
            //   a + b == expected
            //   b + a == expected

            var result_1a = a + b;
            var result_2a = b + a;

            Vector4 result_1b; Vector4.Add(ref a, ref b, out result_1b);
            Vector4 result_2b; Vector4.Add(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for a known example, all the addition opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Addition_i ()
        {
            var a = new Vector4(3, -6, 44, 11);
            var b = new Vector4(-6, 12, 18, -3);

            var expected = new Vector4(-3, 6, 62, 8);

            this.TestAddition(a, b, expected);
        }

        /// <summary>
        /// Assert that, for a known example involving the zero vector, all the
        /// addition opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Addition_ii ()
        {
            var a = new Vector4(-2313, 88, 199, 42);

            var expected = a;

            this.TestAddition(a, Vector4.Zero, expected);
        }

        /// <summary>
        /// Assert that, for a known example involving two zero vectors, all the
        /// addition opperators and functions yield the correct result of zero.
        /// </summary>
        [Test]
        public void TestOperator_Addition_iii ()
        {
            this.TestAddition(Vector4.Zero, Vector4.Zero, Vector4.Zero);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// addition opperators and functions yield the same results as a
        /// manual addition calculation.
        /// </summary>
        [Test]
        public void TestOperator_Addition_iv ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector4();
                var b = GetNextRandomVector4();

                var expected = new Vector4(
                    a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);

                this.TestAddition(a, b, expected);
            }
        }

        // Test Operator: Subtraction //--------------------------------------//

        /// <summary>
        /// Helper method for testing subtraction.
        /// </summary>
        void TestSubtraction (Vector4 a, Vector4 b, Vector4 expected )
        {
            // This test asserts the following:
            //   a - b == expected
            //   b - a == -expected

            var result_1a = a - b;
            var result_2a = b - a;

            Vector4 result_1b; Vector4.Subtract(ref a, ref b, out result_1b);
            Vector4 result_2b; Vector4.Subtract(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(-expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(-expected));
        }

        /// <summary>
        /// Assert that, for known examples, all the subtraction opperators
        /// and functions yield the correct result.
        /// <summary>
        [Test]
        public void TestOperator_Subtraction_i ()
        {
            var a = new Vector4(12, -4, 14, 18);
            var b = new Vector4(15, 11, 7, 27);
            var expected = new Vector4(-3, -15, 7, -9);
            this.TestSubtraction(a, b, expected);

            var c = new Vector4(-423, 342, 7, -800);
            this.TestSubtraction(c, Vector4.Zero, c);
        }

        /// <summary>
        /// Assert that when subtracting the zero vector fromt the zero vector,
        /// all the subtraction opperators and functions yield the correct
        /// result.
        /// <summary>
        [Test]
        public void TestOperator_Subtraction_ii ()
        {
            this.TestSubtraction(Vector4.Zero, Vector4.Zero, Vector4.Zero);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// subtraction opperators and functions yield the same results as a
        /// manual subtraction calculation.
        /// </summary>
        [Test]
        public void TestOperator_Subtraction_iii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector4();
                var b = GetNextRandomVector4();

                var expected = new Vector4(
                    a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);

                this.TestSubtraction(a, b, expected);
            }
        }

        // Test Operator: Negation //-----------------------------------------//

        /// <summary>
        /// Helper method for testing negation.
        /// </summary>
        void TestNegation (Vector4 a, Vector4 expected )
        {
            // This test asserts the following:
            //   -a == expected

            var result_1a = -a;

            Vector4 result_1b; Vector4.Negate(ref a, out result_1b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for known examples, all the negation opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_i ()
        {
            Single r = 3432;
            Single s = -6218;
            Single t = -3432;
            Single u = 6218;

            var a = new Vector4(r, s, t, u);
            var b = new Vector4(u, t, s, r);
            var c = new Vector4(t, u, r, s);
            var d = new Vector4(s, r, u, t);

            this.TestNegation(a, c);
            this.TestNegation(b, d);
        }

        /// <summary>
        /// Assert that, for known examples involving the zero vector, all the
        /// negation opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_ii ()
        {
            Single t = -3432;
            Single u = 6218;
            Single r = 3432;
            Single s = -6218;

            var c = new Vector4(t, u, r, s);
            var d = new Vector4(s, r, u, t);

            this.TestNegation(c, Vector4.Zero - c);
            this.TestNegation(d, Vector4.Zero - d);
        }

        /// <summary>
        /// Assert that when negating the zero vector, all the
        /// negation opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_iii ()
        {
            this.TestNegation(Vector4.Zero, Vector4.Zero);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// negation opperators and functions yield the same results as a
        /// manual negation calculation.
        /// </summary>
        [Test]
        public void TestOperator_Negation_iv ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector4();
                this.TestNegation(a, Vector4.Zero - a);
            }
        }

        // Test Operator: Multiplication //-----------------------------------//

        /// <summary>
        /// Helper method for testing multiplication.
        /// </summary>
        void TestMultiplication (Vector4 a, Vector4 b, Vector4 expected )
        {
            // This test asserts the following:
            //   a * b == expected
            //   b * a == expected

            var result_1a = a * b;
            var result_2a = b * a;

            Vector4 result_1b; Vector4.Multiply(ref a, ref b, out result_1b);
            Vector4 result_2b; Vector4.Multiply(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for a known example, all the multiplication opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Multiplication_i ()
        {
            Single r = -27;
            Single s = 36;
            Single t = 9;
            Single u = -54;

            Single x = 3;
            Single y = 6;
            Single z = -9;

            var a = new Vector4(x, y, x, y);
            var b = new Vector4(z, y, x, z);
            var c = new Vector4(r, s, t, u);

            this.TestMultiplication(a, b, c);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// multiplication opperators and functions yield the same results as a
        /// manual multiplication calculation.
        /// </summary>
        [Test]
        public void TestOperator_Multiplication_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector4();
                var b = GetNextRandomVector4();

                var c = new Vector4(
                    a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);

                this.TestMultiplication(a, b, c);
            }
        }


        // Test Operator: Division //-----------------------------------------//

        /// <summary>
        /// Helper method for testing division.
        /// </summary>
        void TestDivision (Vector4 a, Vector4 b, Vector4 expected )
        {
            // This test asserts the following:
            //   a / b == expected

            var result_1a = a / b;

            Vector4 result_1b; Vector4.Divide(ref a, ref b, out result_1b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for a known example using whole numbers, all the
        /// division opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Division_i ()
        {
            Single r = 10;
            Single s = -40;
            Single t = 1;
            Single u = -400;

            Single x = 2000;
            Single y = 200;
            Single z = -5;

            var a = new Vector4(x, y, x, x);
            var b = new Vector4(y, z, x, z);
            var c = new Vector4(r, s, t, u);

            this.TestDivision(a, b, c);
        }

        /// <summary>
        /// Assert that, for a known example using fractional numbers, all the
        /// division opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Division_ii ()
        {
            Single s = 4;
            Single t = ((Single) 1 ) / ((Single) 10);
            Single u = ((Single) (-1) ) / ((Single) 40 );
            Single v = -20;
            Single w = 100;
            Single x = 2000;
            Single y = 200;
            Single z = -5;

            var a = new Vector4(y, z, w, v);
            var b = new Vector4(x, y, z, z);
            var c = new Vector4(t, u, v, s);

            this.TestDivision(a, b, c);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// division opperators and functions yield the same results as a
        /// manual addition division.
        /// </summary>
        [Test]
        public void TestOperator_Division_iii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector4();
                var b = GetNextRandomVector4();

                var c = new Vector4(
                    a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);

                this.TestDivision(a, b, c);
            }
        }

        // Test Static Fn: SmoothStep //--------------------------------------//

        /// <summary>
        /// This test runs a number of random scenarios and makes sure that when
        /// the weighting parameter is at it's limits the spline passes directly
        /// through the correct control points.
        /// </summary>
        [Test]
        public void TestStaticFn_SmoothStep_i ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector4();
                var b = GetNextRandomVector4();

                Single amount1 = 0;
                Vector4 result1;

                Vector4.SmoothStep (
                    ref a, ref b, ref amount1, out result1);

                AssertEqualWithinReason(result1, a);

                Single amount2 = 1;
                Vector4 result2;

                Vector4.SmoothStep (
                    ref a, ref b, ref amount2, out result2);

                AssertEqualWithinReason(result2, b);
            }
        }

        /// <summary>
        /// Assert that, for known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_SmoothStep_ii ()
        {
            var a = GetNextRandomVector4();
            var b = GetNextRandomVector4();

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for (Int32 idx = 0; idx < tests.Length; ++idx)
            {
                Vector4 result;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Vector4.SmoothStep (
                            ref a, ref b, ref tests[idx], out result)
                    );
            }
        }

        /// <summary>
        /// This tests compares results against a known example.
        /// </summary>
        [Test]
        public void TestStaticFn_SmoothStep_iii ()
        {
            var a = new Vector4( -30, -30, -30, -30 );
            var b = new Vector4( +30, +30, +30, +30 );

            Single one = 1;

            Single i;
            Maths.FromFraction(1755, 64, out i); // 27.421875

            Single j;
            Maths.FromFraction( 165,  8, out j); // 20.625

            Single k;
            Maths.FromFraction( 705, 64, out k); // 11.015625

            Single a0 = 0;
            Single a1 = (one * 1) / 8;
            Single a2 = (one * 2) / 8;
            Single a3 = (one * 3) / 8;
            Single a4 = (one * 4) / 8;
            Single a5 = (one * 5) / 8;
            Single a6 = (one * 6) / 8;
            Single a7 = (one * 7) / 8;
            Single a8 = 1;

            Vector4 r0 = a;
            Vector4 r1 = new Vector4( -i, -i, -i, -i );
            Vector4 r2 = new Vector4( -j, -j, -j, -j );
            Vector4 r3 = new Vector4( -k, -k, -k, -k );
            Vector4 r4 = Vector4.Zero;
            Vector4 r5 = new Vector4(  k,  k,  k,  k );
            Vector4 r6 = new Vector4(  j,  j,  j,  j );
            Vector4 r7 = new Vector4(  i,  i,  i,  i );
            Vector4 r8 = b;

            var knownResults = new List<Tuple<Single, Vector4>>
            {
                new Tuple<Single, Vector4>( a0, r0 ),
                new Tuple<Single, Vector4>( a1, r1 ),
                new Tuple<Single, Vector4>( a2, r2 ),
                new Tuple<Single, Vector4>( a3, r3 ),
                new Tuple<Single, Vector4>( a4, r4 ),
                new Tuple<Single, Vector4>( a5, r5 ),
                new Tuple<Single, Vector4>( a6, r6 ),
                new Tuple<Single, Vector4>( a7, r7 ),
                new Tuple<Single, Vector4>( a8, r8 ),
            };

            for (Int32 idx = 0; idx < knownResults.Count; ++idx)
            {
                Vector4 result;
                Single amount = knownResults[idx].Item1;
                Vector4 expected = knownResults[idx].Item2;

                Vector4.SmoothStep (
                    ref a, ref b, ref amount, out result);

                AssertEqualWithinReason(result, expected);
            }
        }

        // Test Static Fn: CatmullRom //--------------------------------------//

        /// <summary>
        /// This test runs a number of random scenarios and makes sure that when
        /// the weighting parameter is at it's limits the spline passes directly
        /// through the correct control points.
        /// </summary>
        [Test]
        public void TestStaticFn_CatmullRom_i ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomVector4();
                var b = GetNextRandomVector4();
                var c = GetNextRandomVector4();
                var d = GetNextRandomVector4();

                Single amount1 = 0;
                Vector4 result1;

                Vector4.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount1, out result1);

                AssertEqualWithinReason(result1, b);

                Single amount2 = 1;
                Vector4 result2;

                Vector4.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount2, out result2);

                AssertEqualWithinReason(result2, c);
            }
        }

        /// <summary>
        /// This tests compares results against a known example.
        /// </summary>
        [Test]
        public void TestStaticFn_CatmullRom_ii ()
        {
            var a = new Vector4( -120,  +40,  +40, +120 );
            var b = new Vector4( - 40,  -40,  -40, + 40 );
            var c = new Vector4( + 40,  +40,  +40, - 40 );
            var d = new Vector4( +120,  -40,  -40, -120 );

            Single one = 1;

            Single y = 30;
            Single x = 20;
            Single w = 10;
            Single v = (Single) 585  / (Single) 16; // 36.5625
            Single u = (Single) 55  / (Single) 2; // 27.5
            Single t = (Single) 235  / (Single) 16; // 14.6875

            Single a0 = 0;
            Single a1 = (one * 1) / 8;
            Single a2 = (one * 2) / 8;
            Single a3 = (one * 3) / 8;
            Single a4 = (one * 4) / 8;
            Single a5 = (one * 5) / 8;
            Single a6 = (one * 6) / 8;
            Single a7 = (one * 7) / 8;
            Single a8 = 1;

            Vector4 r0 = b;
            Vector4 r1 = new Vector4( -y, -v, -v,  y );
            Vector4 r2 = new Vector4( -x, -u, -u,  x );
            Vector4 r3 = new Vector4( -w, -t, -t,  w );
            Vector4 r4 = Vector4.Zero;
            Vector4 r5 = new Vector4(  w,  t,  t, -w );
            Vector4 r6 = new Vector4(  x,  u,  u, -x );
            Vector4 r7 = new Vector4(  y,  v,  v, -y );
            Vector4 r8 = c;

            var knownResults = new List<Tuple<Single, Vector4>>
            {
                new Tuple<Single, Vector4>( a0, r0 ),
                new Tuple<Single, Vector4>( a1, r1 ),
                new Tuple<Single, Vector4>( a2, r2 ),
                new Tuple<Single, Vector4>( a3, r3 ),
                new Tuple<Single, Vector4>( a4, r4 ),
                new Tuple<Single, Vector4>( a5, r5 ),
                new Tuple<Single, Vector4>( a6, r6 ),
                new Tuple<Single, Vector4>( a7, r7 ),
                new Tuple<Single, Vector4>( a8, r8 ),
            };

            for (Int32 idx = 0; idx < knownResults.Count; ++idx)
            {
                Vector4 result;
                Single amount = knownResults[idx].Item1;
                Vector4 expected = knownResults[idx].Item2;

                Vector4.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount, out result);

                AssertEqualWithinReason(result, expected);
            }
        }

        /// <summary>
        /// Assert that, for known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_CatmullRom_iii ()
        {
            var a = GetNextRandomVector4();
            var b = GetNextRandomVector4();
            var c = GetNextRandomVector4();
            var d = GetNextRandomVector4();

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for (Int32 idx = 0; idx < tests.Length; ++idx)
            {
                Vector4 result;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Vector4.CatmullRom (
                            ref a, ref b, ref c, ref d, ref tests[idx], out result)
                );
            }
        }

        /// <summary>
        /// This tests compares results against an example where all the control
        /// points are in a straight line.  In this case the resulting spline
        /// should be a straight line.
        /// </summary>
        [Test]
        public void TestStaticFn_CatmullRom_iv ()
        {
            var a = new Vector4( -90, -90, -90, -90 );
            var b = new Vector4( -30, -30, -30, -30 );
            var c = new Vector4( +30, +30, +30, +30 );
            var d = new Vector4( +90, +90, +90, +90 );

            Single one = 1;

            Single a0 = 0;
            Single a1 = (one * 1) / 4;
            Single a2 = (one * 2) / 4;
            Single a3 = (one * 3) / 4;
            Single a4 = 1;

            Vector4 r0 = b;
            Vector4 r1 = new Vector4( -15, -15, -15, -15 );
            Vector4 r2 = Vector4.Zero;
            Vector4 r3 = new Vector4(  15,  15,  15,  15 );
            Vector4 r4 = c;

            var knownResults = new List<Tuple<Single, Vector4>>
            {
                new Tuple<Single, Vector4>( a0, r0 ),
                new Tuple<Single, Vector4>( a1, r1 ),
                new Tuple<Single, Vector4>( a2, r2 ),
                new Tuple<Single, Vector4>( a3, r3 ),
                new Tuple<Single, Vector4>( a4, r4 ),
            };

            for (Int32 idx = 0; idx < knownResults.Count; ++idx)
            {
                Vector4 result;
                Single amount = knownResults[idx].Item1;
                Vector4 expected = knownResults[idx].Item2;

                Vector4.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount, out result);

                AssertEqualWithinReason(result, expected);
            }
        }

        // Test Static Fn: Hermite //-----------------------------------------//

        /// <summary>
        /// This test runs a number of random scenarios and makes sure that when
        /// the weighting parameter is at it's limits the spline passes directly
        /// through the correct control points.
        /// </summary>
        [Test]
        public void TestStaticFn_Hermite_i ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a  = GetNextRandomVector4();
                var b  = GetNextRandomVector4();

                var c = GetNextRandomVector4();
                var d = GetNextRandomVector4();

                Vector4 an; Vector4.Normalise(ref c, out an);
                Vector4 bn; Vector4.Normalise(ref d, out bn);

                Single amount1 = 0;
                Vector4 result1;

                Vector4.Hermite (
                    ref a, ref an, ref b, ref bn, ref amount1, out result1);

                AssertEqualWithinReason(result1, a);

                Single amount2 = 1;
                Vector4 result2;

                Vector4.Hermite (
                    ref a, ref an, ref b, ref bn, ref amount2, out result2);

                AssertEqualWithinReason(result2, b);
            }
        }

        /// <summary>
        /// Assert that, for a known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_Hermite_ii ()
        {
            var a = GetNextRandomVector4();
            var b = GetNextRandomVector4();
            var c = GetNextRandomVector4();
            var d = GetNextRandomVector4();

            Vector4 an; Vector4.Normalise(ref c, out an);
            Vector4 bn; Vector4.Normalise(ref d, out bn);

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for (Int32 idx = 0; idx < tests.Length; ++idx)
            {
                Vector4 result;

                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Vector4.Hermite (
                            ref a, ref an, ref b, ref bn, ref tests[idx], out result)
                    );

            }
        }

        /// <summary>
        /// This tests compares results against a known example.
        /// </summary>
        [Test]
        public void TestStaticFn_Hermite_iii ()
        {
            var a = new Vector4( -100, +50, +100, -50 );
            var b = new Vector4( +100, -50, -100, +50 );

            var c = new Vector4( -10, +5, +10, -5 );
            var d = new Vector4( +10, -5, -10, +5 );

            Vector4 an; Vector4.Normalise(ref c, out an);
            Vector4 bn; Vector4.Normalise(ref d, out bn);

            Single one = 1;

            // 100.1953125
            Single e = (Single) 51300 / (Single) 512;

            // 50.09765625
            Single f = (Single) 12825 / (Single) 256;

            // 91.25
            Single g = (Single) 365 / (Single) 4;

            // 45.625
            Single h = (Single) 365 / (Single) 8;

            // 75.7421875
            Single i = (Single) 9695 / (Single) 128;

            // 37.87109375
            Single j = (Single) 9695 / (Single) 256;

            // 56.25
            Single k = (Single) 225 / (Single) 4;

            // 28.125
            Single l = (Single) 225 / (Single) 8;

            // 35.3515625
            Single m = (Single) 4525 / (Single) 128;

            // 17.67578125
            Single n = (Single) 4525 / (Single) 256;

            // 15.625
            Single o = (Single) 125 / (Single) 8;

            // 7.8125
            Single p = (Single) 125 / (Single) 16;

            // 0.3515625
            Single q = (Single) 45 / (Single) 128;

            // 0.17578125
            Single r = (Single) 45 / (Single) 256;

            Single a0 = 0;
            Single a1 = (one * 1) / 8;
            Single a2 = (one * 2) / 8;
            Single a3 = (one * 3) / 8;
            Single a4 = (one * 4) / 8;
            Single a5 = (one * 5) / 8;
            Single a6 = (one * 6) / 8;
            Single a7 = (one * 7) / 8;
            Single a8 = 1;

            Vector4 r0 = b;
            Vector4 r1 = new Vector4(  e, -f, -e,  f );
            Vector4 r2 = new Vector4(  g, -h, -g,  h );
            Vector4 r3 = new Vector4(  i, -j, -i,  j );
            Vector4 r4 = new Vector4(  k, -l, -k,  l );
            Vector4 r5 = new Vector4(  m, -n, -m,  n );
            Vector4 r6 = new Vector4(  o, -p, -o,  p );
            Vector4 r7 = new Vector4( -q,  r,  q, -r );
            Vector4 r8 = c;

            var knownResults = new List<Tuple<Single, Vector4>>
            {
                new Tuple<Single, Vector4>( a0, r0 ),
                new Tuple<Single, Vector4>( a1, r1 ),
                new Tuple<Single, Vector4>( a2, r2 ),
                new Tuple<Single, Vector4>( a3, r3 ),
                new Tuple<Single, Vector4>( a4, r4 ),
                new Tuple<Single, Vector4>( a5, r5 ),
                new Tuple<Single, Vector4>( a6, r6 ),
                new Tuple<Single, Vector4>( a7, r7 ),
                new Tuple<Single, Vector4>( a8, r8 ),
            };

            for (Int32 idx = 0; idx < knownResults.Count; ++idx)
            {
                Vector4 result;
                Single amount = knownResults[idx].Item1;
                Vector4 expected = knownResults[idx].Item2;

                Vector4.CatmullRom (
                    ref a, ref b, ref c, ref d, ref amount, out result);

                AssertEqualWithinReason(result, expected);
            }
        }


        /// <summary>
        /// Assert that, running the Min function on a number of randomly
        /// generated pairs of Vector4 objects, yields the same results as those
        /// obtained from performing a manual Min calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Min ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector4 a = GetNextRandomVector4();
                Vector4 b = a * 2;

                Vector4 result;
                Vector4.Min (ref a, ref b, out result);

                Assert.That(result.X, Is.EqualTo(a.X < b.X ? a.X : b.X ));
                Assert.That(result.Y, Is.EqualTo(a.Y < b.Y ? a.Y : b.Y ));
                Assert.That(result.Z, Is.EqualTo(a.Z < b.Z ? a.Z : b.Z ));
                Assert.That(result.W, Is.EqualTo(a.W < b.W ? a.W : b.W ));
            }
        }

        /// <summary>
        /// Assert that, running the Max function on a number of randomly
        /// generated pairs of Vector4 objects, yields the same results as those
        /// obtained from performing a manual Max calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Max ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Vector4 a = GetNextRandomVector4();
                Vector4 b = GetNextRandomVector4();

                Vector4 result;
                Vector4.Max (ref a, ref b, out result);

                Assert.That(result.X, Is.EqualTo(a.X > b.X ? a.X : b.X ));
                Assert.That(result.Y, Is.EqualTo(a.Y > b.Y ? a.Y : b.Y ));
                Assert.That(result.Z, Is.EqualTo(a.Z > b.Z ? a.Z : b.Z ));
                Assert.That(result.W, Is.EqualTo(a.W > b.W ? a.W : b.W ));
            }
        }

        /// <summary>
        /// Assert that, running the Clamp function on a number of randomly
        /// generated Vector4 objects for a given min-max range, yields
        /// results that fall within that range.
        /// </summary>
        [Test]
        public void TestStaticFn_Clamp_i ()
        {
            Vector4 min = new Vector4(-30, 1, 18, -22);
            Vector4 max = new Vector4(32, 130, 47, -2);

            for(Int32 i = 0; i < 100; ++i)
            {
                Vector4 a = GetNextRandomVector4();

                Vector4 result;
                Vector4.Clamp (ref a, ref min, ref max, out result);

                Assert.That(result.X, Is.LessThanOrEqualTo(max.X));
                Assert.That(result.Y, Is.LessThanOrEqualTo(max.Y));
                Assert.That(result.X, Is.GreaterThanOrEqualTo(min.X));
                Assert.That(result.Y, Is.GreaterThanOrEqualTo(min.Y));
            }
        }

        /// <summary>
        /// Assert that, running the Clamp function on an a Vector4 object known
        /// to fall outside of a given min-max range, yields a result that fall
        /// within that range.
        /// </summary>
        [Test]
        public void TestStaticFn_Clamp_ii ()
        {
            Vector4 min = new Vector4(-30, 1, 18, -22);
            Vector4 max = new Vector4(32, 130, 47, -2);

            Vector4 a = new Vector4(-100, 1113, 50, 14);

            Vector4 expected = new Vector4(-30, 130, 47, -2);

            Vector4 result;
            Vector4.Clamp (ref a, ref min, ref max, out result);

            Assert.That(result.X, Is.LessThanOrEqualTo(max.X));
            Assert.That(result.Y, Is.LessThanOrEqualTo(max.Y));
            Assert.That(result.Z, Is.LessThanOrEqualTo(max.Z));
            Assert.That(result.W, Is.LessThanOrEqualTo(max.W));
            Assert.That(result.X, Is.GreaterThanOrEqualTo(min.X));
            Assert.That(result.Y, Is.GreaterThanOrEqualTo(min.Y));
            Assert.That(result.Z, Is.GreaterThanOrEqualTo(min.Z));
            Assert.That(result.W, Is.GreaterThanOrEqualTo(min.W));

            AssertEqualWithinReason(result, expected);

        }

        /// <summary>
        /// Assert that, running the Lerp function on a number of randomly
        /// generated pairs of Vector4 objects for a range of weighting amounts,
        /// yields the same results as those obtained from performing a manual
        /// Lerp calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Lerp_i ()
        {
            for(Int32 j = 0; j < 100; ++j)
            {
                Single delta = j;

                delta = delta / 100;

                for(Int32 i = 0; i < 100; ++i)
                {
                    Vector4 a = GetNextRandomVector4();
                    Vector4 b = GetNextRandomVector4();

                    Vector4 result;
                    Vector4.Lerp (ref a, ref b, ref delta, out result);

                    Vector4 expected = a + ( ( b - a ) * delta );

                    AssertEqualWithinReason(result, expected);
                }
            }
        }

        /// <summary>
        /// Assert that, for a known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_Lerp_ii ()
        {
            Vector4 a = GetNextRandomVector4();
            Vector4 b = GetNextRandomVector4();

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for( Int32 i = 0; i < tests.Length; ++i )
            {
                Vector4 result;
                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Vector4.Lerp (
                            ref a, ref b, ref tests[i], out result)
                    );
            }
        }

        /// <summary>
        /// Tests that for the simple vectors the IsUnit member function
        /// returns the correct result of TRUE.
        /// </summary>
        [Test]
        public void TestStaticFn_IsUnit_i ()
        {
            Vector4 a = new Vector4( 1,  0,  0,  0);
            Vector4 b = new Vector4(-1,  0,  0,  0);
            Vector4 c = new Vector4( 0,  1,  0,  0);
            Vector4 d = new Vector4( 0, -1,  0,  0);
            Vector4 e = new Vector4( 0,  0,  1,  0);
            Vector4 f = new Vector4( 0,  0, -1,  0);
            Vector4 g = new Vector4( 0,  0,  0,  1);
            Vector4 h = new Vector4( 0,  0,  0, -1);
            Vector4 i = new Vector4( 1,  1,  1,  1);
            Vector4 j = new Vector4( 0,  0,  0,  0);

            Assert.That(a.IsUnit(), Is.EqualTo(true));
            Assert.That(b.IsUnit(), Is.EqualTo(true));
            Assert.That(c.IsUnit(), Is.EqualTo(true));
            Assert.That(d.IsUnit(), Is.EqualTo(true));
            Assert.That(e.IsUnit(), Is.EqualTo(true));
            Assert.That(f.IsUnit(), Is.EqualTo(true));
            Assert.That(g.IsUnit(), Is.EqualTo(true));
            Assert.That(h.IsUnit(), Is.EqualTo(true));

            Assert.That(i.IsUnit(), Is.EqualTo(false));
            Assert.That(j.IsUnit(), Is.EqualTo(false));
        }

        /// <summary>
        /// This test makes sure that the IsUnit member function returns the
        /// correct result of TRUE for a number of scenarios where the test
        /// vector is both random and normalised.
        /// </summary>
        [Test]
        public void TestStaticFn_IsUnit_ii ()
        {
            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector4 a = GetNextRandomVector4();

                Vector4 b; Vector4.Normalise(ref a, out b);

                Assert.That(b.IsUnit(), Is.EqualTo(true));
            }
        }

        /// <summary>
        /// This test ensures that the IsUnit member function correctly
        /// returns TRUE for a collection of vectors, all known to be of unit
        /// length.
        /// </summary>
        [Test]
        public void TestStaticFn_IsUnit_iii ()
        {
            Single radius = 1;

            Single pi; Maths.Pi(out pi);

            for( Int32 i = 0; i <= 10; ++ i)
            {
                for( Int32 j = 0; j <= 10; ++ j)
                {
                    for( Int32 k = 0; k <= 10; ++ k)
                    {
                        Single theta = 2 * pi * i / 100;
                        Single phi = 2 * pi * j / 100;
                        Single gamma = 2 * pi * k / 100;

                        Single x =
                            Maths.Cos(theta) *
                            Maths.Sin(phi) *
                            Maths.Sin(gamma) * radius;

                        Single y =
                            Maths.Sin(theta) *
                            Maths.Sin(phi) *
                            Maths.Sin(gamma) * radius;

                        Single z =
                            Maths.Cos(phi) *
                            Maths.Sin(gamma) * radius;

                        Single w =
                            Maths.Cos(gamma) * radius;

                        Assert.That(
                            new Vector4(x, y,  z, w).IsUnit(),
                            Is.EqualTo(true));
                    }
                }
            }
        }

        /// <summary>
        /// This test makes sure that the IsUnit member function returns the
        /// correct result of FALSE for a number of scenarios where the test
        /// vector is randomly generated and not normalised.  It's highly
        /// unlikely that the random generator will create a unit vector!
        /// </summary>
        [Test]
        public void TestStaticFn_IsUnit_iv ()
        {
            for( Int32 i = 0; i < 100; ++ i)
            {
                Vector4 a = GetNextRandomVector4();

                Assert.That(a.IsUnit(), Is.EqualTo(false));
            }
        }


    }
    /// <summary>
    ///
    /// </summary>
    [TestFixture]
    public class QuaternionTests
    {
        /// <summary>
        /// The random number generator used for testing.
        /// </summary>
        static readonly System.Random rand;

        /// <summary>
        /// Static constructor used to ensure that the random number generator
        /// always gets initilised with the same seed, making the tests
        /// behave in a deterministic manner.
        /// </summary>
        static QuaternionTests ()
        {
            rand = new System.Random(0);
        }

        /// <summary>
        /// Helper function for getting the next random Single value.
        /// </summary>
        static Single GetNextRandomSingle ()
        {
            Single randomValue = rand.NextSingle();

            Single zero = 0;
            Single multiplier = 1000;

            randomValue *= multiplier;

            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;

            if( randomBoolean )
                randomValue = zero - randomValue;

            return randomValue;
        }

        /// <summary>
        /// Helper function for getting the next random Quaternion.
        /// </summary>
        internal static Quaternion GetNextRandomQuaternion ()
        {
            Single i = GetNextRandomSingle();
            Single j = GetNextRandomSingle();
            Single k = GetNextRandomSingle();
            Single u = GetNextRandomSingle();

            return new Quaternion(i, j, k, u);
        }

        /// <summary>
        /// Helper to encapsulate asserting that two Quaternions are equal.
        /// </summary>
        internal static void AssertEqualWithinReason (Quaternion a, Quaternion b)
        {
            Single tolerance; MathsTests.TestTolerance(out tolerance);

            Assert.That(a.I, Is.EqualTo(b.I).Within(tolerance));
            Assert.That(a.J, Is.EqualTo(b.J).Within(tolerance));
            Assert.That(a.K, Is.EqualTo(b.K).Within(tolerance));
            Assert.That(a.U, Is.EqualTo(b.U).Within(tolerance));
        }


        // Test: StructLayout //----------------------------------------------//

        /// <summary>
        /// This test makes sure that the struct layout has been defined
        /// correctly.
        /// </summary>
        [Test]
        public void Test_StructLayout_i ()
        {
            Type t = typeof(Quaternion);

            Assert.That(
                t.StructLayoutAttribute.Value,
                Is.EqualTo(LayoutKind.Sequential));
        }

        /// <summary>
        /// This test makes sure that when examining the memory addresses of the
        /// X, Y, Z and W member variables of a number of randomly generated
        /// Quaterion objects the results are as expected.
        /// </summary>
        [Test]
        public unsafe void Test_StructLayout_ii ()
        {
            for( Int32 i = 0; i < 100; ++ i)
            {
                Quaternion quat = GetNextRandomQuaternion();

                GCHandle h_quat = GCHandle.Alloc(quat, GCHandleType.Pinned);

                IntPtr quatAddress = h_quat.AddrOfPinnedObject();

                Single[] data = new Single[4];

                // nb: when Fixed32 and Half are moved back into the main
                //     dev branch there will be need for an extension method for
                //     Marshal that will perform the copy for those types.
                MarshalHelper.Copy(quatAddress, data, 0, 4);
                Assert.That(data[0], Is.EqualTo(quat.I));
                Assert.That(data[1], Is.EqualTo(quat.J));
                Assert.That(data[2], Is.EqualTo(quat.K));
                Assert.That(data[3], Is.EqualTo(quat.U));

                h_quat.Free();
            }
        }

        /// <summary>
        /// This test goes though each public constuctor and ensures that the
        /// data members of the structure have been properly set.
        /// </summary>
        [Test]
        public void Test_Constructors_i ()
        {
            {
                // Test default values
                Quaternion a = new Quaternion();
                Assert.That(a, Is.EqualTo(Quaternion.Zero));
            }
            {
                // Test Quaternion( Single, Single, Single, Single )
                Single a = -189;
                Single b = 429;
                Single c = 4298;
                Single d = 341;
                Quaternion e = new Quaternion(a, b, c, d);
                Assert.That(e.I, Is.EqualTo(a));
                Assert.That(e.J, Is.EqualTo(b));
                Assert.That(e.K, Is.EqualTo(c));
                Assert.That(e.U, Is.EqualTo(d));
            }
            {
                // Test Quaternion( Vector3, Single )
                Vector3 a = new Vector3(-189, 429, 4298);
                Single b = 341;
                Quaternion c = new Quaternion(a, b);
                Assert.That(c.I, Is.EqualTo(a.X));
                Assert.That(c.J, Is.EqualTo(a.Y));
                Assert.That(c.K, Is.EqualTo(a.Z));
                Assert.That(c.U, Is.EqualTo(b));
            }
        }

        [Test]
        public void TestMemberFn_ToString ()
        {
            Quaternion a = new Quaternion(42, -17, 13, 44);

            String result = a.ToString();

            String expected = "{I:42 J:-17 K:13 U:44}";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestMemberFn_GetHashCode ()
        {
            var hs1 = new System.Collections.Generic.HashSet<Quaternion>();
            var hs2 = new System.Collections.Generic.HashSet<Int32>();

            for(Int32 i = 0; i < 10000; ++i)
            {
                var a = GetNextRandomQuaternion();

                hs1.Add(a);
                hs2.Add(a.GetHashCode());
            }

            Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
        }

        // Test Constant: Identity //-----------------------------------------//

        /// <summary>
        /// Tests to make sure that a Quaternion initilised using the Identity 
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_Identity_i ()
        {
            Quaternion result = Quaternion.Identity;
            Quaternion expected = new Quaternion (
                0, 0, 0, 1);

            AssertEqualWithinReason(result, expected);
        }        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateFromAxisAngle_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateFromYawPitchRoll_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateFromRotationMatrix_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        // Test Static Fn: Conjugate //---------------------------------------//

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Conjugate_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        // Test Static Fn: Inverse //-----------------------------------------//

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Inverse_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        // Test Static Fn: Distance //----------------------------------------//

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Dot_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        // Test Static Fn: Concatenate //-------------------------------------//

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Concatenate_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        // Test Static Fn: Normalise //---------------------------------------//

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Normalise_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        // Test Operator: Equality //-----------------------------------------//

        /// <summary>
        /// Helper method for testing equality.
        /// </summary>
        void TestEquality (Quaternion a, Quaternion b, Boolean expected )
        {
            // This test asserts the following:
            //   (a == b) == expected
            //   (b == a) == expected
            //   (a != b) == !expected
            //   (b != a) == !expected

            Boolean result_1a = (a == b);
            Boolean result_1b = (a.Equals(b));
            Boolean result_1c = (a.Equals((Object)b));

            Boolean result_2a = (b == a);
            Boolean result_2b = (b.Equals(a));
            Boolean result_2c = (b.Equals((Object)a));

            Boolean result_3a = (a != b);
            Boolean result_4a = (b != a);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_1c, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
            Assert.That(result_2c, Is.EqualTo(expected));
            Assert.That(result_3a, Is.EqualTo(!expected));
            Assert.That(result_4a, Is.EqualTo(!expected));
        }

        /// <summary>
        /// Makes sure that, for a known example, all the equality opperators
        /// and functions yield the expected result of TRUE when two equal
        /// Quaternion objects are compared.
        /// </summary>
        [Test]
        public void TestOperator_Equality_i ()
        {
            var a = new Quaternion(44, -54, -22, 11);
            var b = new Quaternion(44, -54, -22, 11);

            Boolean expected = true;

            this.TestEquality(a, b, expected);
        }

        /// <summary>
        /// Makes sure that, for a known example, all the equality opperators
        /// and functions yield the expected result of FALSE when two unequal
        /// Quaternion objects are compared.
        /// </summary>
        [Test]
        public void TestOperator_Equality_ii ()
        {
            var a = new Quaternion(44, 54, 2, 11);
            var b = new Quaternion(44, -54, 2, -1);

            Boolean expected = false;

            this.TestEquality(a, b, expected);
        }

        /// <summary>
        /// Tests to make sure that all the equality opperators and functions
        /// yield the expected result of TRUE when used on a number of randomly
        /// generated pairs of equal Quaternion objects.
        /// </summary>
        [Test]
        public void TestOperator_Equality_iii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomQuaternion();

                Quaternion b = a;

                this.TestEquality(a, b, true);
            }
        }


        // Test Operator: Addition //-----------------------------------------//

        /// <summary>
        /// Helper method for testing addition.
        /// </summary>
        void TestAddition (Quaternion a, Quaternion b, Quaternion expected )
        {
            // This test asserts the following:
            //   a + b == expected
            //   b + a == expected

            var result_1a = a + b;
            var result_2a = b + a;

            Quaternion result_1b; Quaternion.Add(ref a, ref b, out result_1b);
            Quaternion result_2b; Quaternion.Add(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for a known example, all the addition opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Addition_i ()
        {
            var a = new Quaternion(3, -6, 44, 11);
            var b = new Quaternion(-6, 12, 18, -3);

            var expected = new Quaternion(-3, 6, 62, 8);

            this.TestAddition(a, b, expected);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// addition opperators and functions yield the same results as a
        /// manual addition calculation.
        /// </summary>
        [Test]
        public void TestOperator_Addition_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomQuaternion();
                var b = GetNextRandomQuaternion();

                var expected = new Quaternion(
                    a.I + b.I, a.J + b.J, a.K + b.K, a.U + b.U);

                this.TestAddition(a, b, expected);
            }
        }

        // Test Operator: Subtraction //--------------------------------------//

        /// <summary>
        /// Helper method for testing subtraction.
        /// </summary>
        void TestSubtraction (Quaternion a, Quaternion b, Quaternion expected )
        {
            // This test asserts the following:
            //   a - b == expected
            //   b - a == -expected

            var result_1a = a - b;
            var result_2a = b - a;

            Quaternion result_1b; Quaternion.Subtract(ref a, ref b, out result_1b);
            Quaternion result_2b; Quaternion.Subtract(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(-expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(-expected));
        }

        /// <summary>
        /// Assert that, for known examples, all the subtraction opperators
        /// and functions yield the correct result.
        /// <summary>
        [Test]
        public void TestOperator_Subtraction_i ()
        {
            var a = new Quaternion(12, -4, 14, 18);
            var b = new Quaternion(15, 11, 7, 27);
            var expected = new Quaternion(-3, -15, 7, -9);
            this.TestSubtraction(a, b, expected);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// subtraction opperators and functions yield the same results as a
        /// manual subtraction calculation.
        /// </summary>
        [Test]
        public void TestOperator_Subtraction_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomQuaternion();
                var b = GetNextRandomQuaternion();

                var expected = new Quaternion(
                    a.I - b.I, a.J - b.J, a.K - b.K, a.U - b.U);

                this.TestSubtraction(a, b, expected);
            }
        }

        // Test Operator: Negation //-----------------------------------------//

        /// <summary>
        /// Helper method for testing negation.
        /// </summary>
        void TestNegation (Quaternion a, Quaternion expected )
        {
            // This test asserts the following:
            //   -a == expected

            var result_1a = -a;

            Quaternion result_1b; Quaternion.Negate(ref a, out result_1b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for known examples, all the negation opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_i ()
        {
            Single r = 3432;
            Single s = -6218;
            Single t = -3432;
            Single u = 6218;

            var a = new Quaternion(r, s, t, u);
            var b = new Quaternion(u, t, s, r);
            var c = new Quaternion(t, u, r, s);
            var d = new Quaternion(s, r, u, t);

            this.TestNegation(a, c);
            this.TestNegation(b, d);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// negation opperators and functions yield the same results as a
        /// manual negation calculation.
        /// </summary>
        [Test]
        public void TestOperator_Negation_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomQuaternion();
                this.TestNegation(a, Quaternion.Zero - a);
            }
        }

        // Test Operator: Multiplication //-----------------------------------//

        /// <summary>
        /// Helper method for testing multiplication.
        /// </summary>
        void TestMultiplication (Quaternion a, Quaternion b, Quaternion expected )
        {
            // This test asserts the following:
            //   a * b == expected
            //   Quaternion multiplication is not commutative,
            //   so don't need to test b * a.

            var result_1a = a * b;

            Quaternion result_1b; Quaternion.Multiply(ref a, ref b, out result_1b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for a known example, all the multiplication opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Multiplication_i ()
        {
            var a = new Quaternion(12, 2, 24, 3);
            var b = new Quaternion(12, 6, -2, 2);
            var c = new Quaternion(-88, 334, 90, -102);
            var d = new Quaternion(208, -290, -6, -102);

            this.TestMultiplication(a, b, c);
            this.TestMultiplication(b, a, d);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// multiplication opperators and functions yield the same results as a
        /// manual multiplication calculation.
        /// </summary>
        [Test]
        public void TestOperator_Multiplication_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomQuaternion();
                var b = GetNextRandomQuaternion();

                var c = new Quaternion(
                    a.I*b.U + a.U*b.I + a.J*b.K - a.K*b.J,
                    a.U*b.J - a.I*b.K + a.J*b.U + a.K*b.I,
                    a.U*b.K + a.I*b.J - a.J*b.I + a.K*b.U,
                    a.U*b.U - a.I*b.I - a.J*b.J - a.K*b.K
                );

                this.TestMultiplication(a, b, c);
            }
        }

        // Test: StructLayout //----------------------------------------------//

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public static void TestStaticFn_Slerp_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public static void TestStaticFn_Lerp_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

    }

    /// <summary>
    ///
    /// </summary>
    [TestFixture]
    public class Matrix44Tests
    {
        /// <summary>
        /// The random number generator used for testing.
        /// </summary>
        static readonly System.Random rand;

        /// <summary>
        /// Static constructor used to ensure that the random number generator
        /// always gets initilised with the same seed, making the tests
        /// behave in a deterministic manner.
        /// </summary>
        static Matrix44Tests ()
        {
            rand = new System.Random(0);
        }

        /// <summary>
        /// Helper function for getting the next random Single value.
        /// </summary>
        static Single GetNextRandomSingle ()
        {
            Single randomValue = rand.NextSingle();

            Single zero = 0;
            Single multiplier = 1000;

            randomValue *= multiplier;

            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;

            if( randomBoolean )
                randomValue = zero - randomValue;

            return randomValue;
        }

        /// <summary>
        /// Helper function for getting the next random Matrix44.
        /// </summary>
        internal static Matrix44 GetNextRandomMatrix44 ()
        {
            Single a = GetNextRandomSingle();
            Single b = GetNextRandomSingle();
            Single c = GetNextRandomSingle();
            Single d = GetNextRandomSingle();

            Single e = GetNextRandomSingle();
            Single f = GetNextRandomSingle();
            Single g = GetNextRandomSingle();
            Single h = GetNextRandomSingle();

            Single i = GetNextRandomSingle();
            Single j = GetNextRandomSingle();
            Single k = GetNextRandomSingle();
            Single l = GetNextRandomSingle();

            Single m = GetNextRandomSingle();
            Single n = GetNextRandomSingle();
            Single o = GetNextRandomSingle();
            Single p = GetNextRandomSingle();

            return new Matrix44(
                a, b, c, d,
                e, f, g, h,
                i, j, k, l,
                m, n, o, p);
        }

        /// <summary>
        /// Helper to encapsulate asserting that two Matrix44s are equal.
        /// </summary>
        internal static void AssertEqualWithinReason (Matrix44 a, Matrix44 b)
        {
            Single tolerance;
            MathsTests.TestTolerance(out tolerance);

            Assert.That(a.R0C0, Is.EqualTo(b.R0C0).Within(tolerance));
            Assert.That(a.R0C1, Is.EqualTo(b.R0C1).Within(tolerance));
            Assert.That(a.R0C2, Is.EqualTo(b.R0C2).Within(tolerance));
            Assert.That(a.R0C3, Is.EqualTo(b.R0C3).Within(tolerance));

            Assert.That(a.R1C0, Is.EqualTo(b.R1C0).Within(tolerance));
            Assert.That(a.R1C1, Is.EqualTo(b.R1C1).Within(tolerance));
            Assert.That(a.R1C2, Is.EqualTo(b.R1C2).Within(tolerance));
            Assert.That(a.R1C3, Is.EqualTo(b.R1C3).Within(tolerance));

            Assert.That(a.R2C0, Is.EqualTo(b.R2C0).Within(tolerance));
            Assert.That(a.R2C1, Is.EqualTo(b.R2C1).Within(tolerance));
            Assert.That(a.R2C2, Is.EqualTo(b.R2C2).Within(tolerance));
            Assert.That(a.R2C3, Is.EqualTo(b.R2C3).Within(tolerance));

            Assert.That(a.R3C0, Is.EqualTo(b.R3C0).Within(tolerance));
            Assert.That(a.R3C1, Is.EqualTo(b.R3C1).Within(tolerance));
            Assert.That(a.R3C2, Is.EqualTo(b.R3C2).Within(tolerance));
            Assert.That(a.R3C3, Is.EqualTo(b.R3C3).Within(tolerance));
        }


        /// <summary>
        /// This test makes sure that the struct layout has been defined
        /// correctly.
        /// </summary>
        [Test]
        public void Test_StructLayout_i ()
        {
            Type t = typeof(Matrix44);

            Assert.That(
                t.StructLayoutAttribute.Value,
                Is.EqualTo(LayoutKind.Sequential));
        }

        /// <summary>
        /// This test makes sure that when examining the memory addresses of the
        /// member variables of a number of randomly generated Matrix44
        /// objects the results are as expected.
        /// </summary>
        [Test]
        public unsafe void Test_StructLayout_ii ()
        {
            for( Int32 i = 0; i < 100; ++ i)
            {
                Matrix44 mat = GetNextRandomMatrix44();

                GCHandle h_vec = GCHandle.Alloc(mat, GCHandleType.Pinned);

                IntPtr vecAddress = h_vec.AddrOfPinnedObject();

                Single[] data = new Single[16];

                // nb: when Fixed32 and Half are moved back into the main
                //     dev branch there will be need for an extension method for
                //     Marshal that will perform the copy for those types.
                MarshalHelper.Copy(vecAddress, data, 0, 16);
                Assert.That(data[0], Is.EqualTo(mat.R0C0));
                Assert.That(data[1], Is.EqualTo(mat.R0C1));
                Assert.That(data[2], Is.EqualTo(mat.R0C2));
                Assert.That(data[3], Is.EqualTo(mat.R0C3));
                Assert.That(data[4], Is.EqualTo(mat.R1C0));
                Assert.That(data[5], Is.EqualTo(mat.R1C1));
                Assert.That(data[6], Is.EqualTo(mat.R1C2));
                Assert.That(data[7], Is.EqualTo(mat.R1C3));
                Assert.That(data[8], Is.EqualTo(mat.R2C0));
                Assert.That(data[9], Is.EqualTo(mat.R2C1));
                Assert.That(data[10], Is.EqualTo(mat.R2C2));
                Assert.That(data[11], Is.EqualTo(mat.R2C3));
                Assert.That(data[12], Is.EqualTo(mat.R3C0));
                Assert.That(data[13], Is.EqualTo(mat.R3C1));
                Assert.That(data[14], Is.EqualTo(mat.R3C2));
                Assert.That(data[15], Is.EqualTo(mat.R3C3));

                h_vec.Free();
            }
        }

        /// <summary>
        /// This test goes though each public constuctor and ensures that the
        /// data members of the structure have been properly set.
        /// </summary>
        [Test]
        public void Test_Constructors ()
        {
            {
                // Test default values
                Matrix44 a = new Matrix44();
                Assert.That(a, Is.EqualTo(Matrix44.Zero));
            }
            {
                // Test Matrix44(
                //     Single,
                //     Single,
                //     Single,
                //     Single,
                //     Single,
                //     Single,
                //     Single,
                //     Single,
                //     Single,
                //     Single,
                //     Single,
                //     Single )
                Single a = -18759;
                Single b = 345;
                Single c = 774;
                Single d = -3431;
                Single e = -55;
                Single f = 47;
                Single g = 45;
                Single h = 3461;
                Single i = -4;
                Single j = 453;
                Single k = -3;
                Single l = -3441;
                Single m = -189;
                Single n = 5;
                Single o = -87;
                Single p = 341;

                Matrix44 expected = new Matrix44(
                    a, b, c, d,
                    e, f, g, h,
                    i, j, k, l,
                    m, n, o, p);

                Assert.That(expected.R0C0, Is.EqualTo(a));
                Assert.That(expected.R0C1, Is.EqualTo(b));
                Assert.That(expected.R0C2, Is.EqualTo(c));
                Assert.That(expected.R0C3, Is.EqualTo(d));
                Assert.That(expected.R1C0, Is.EqualTo(e));
                Assert.That(expected.R1C1, Is.EqualTo(f));
                Assert.That(expected.R1C2, Is.EqualTo(g));
                Assert.That(expected.R1C3, Is.EqualTo(h));
                Assert.That(expected.R2C0, Is.EqualTo(i));
                Assert.That(expected.R2C1, Is.EqualTo(j));
                Assert.That(expected.R2C2, Is.EqualTo(k));
                Assert.That(expected.R2C3, Is.EqualTo(l));
                Assert.That(expected.R3C0, Is.EqualTo(m));
                Assert.That(expected.R3C1, Is.EqualTo(n));
                Assert.That(expected.R3C2, Is.EqualTo(o));
                Assert.That(expected.R3C3, Is.EqualTo(p));
            }
        }

        [Test]
        public void TestMemberFn_ToString ()
        {
            Matrix44 a = new Matrix44(
                42, -17, 13, -44,
                46, -23, -22, 90,
                34, -21, 52, 33,
                88, -12, -78, 50);

            String result = a.ToString();

            String expected =
                "{ " +
                "{R0C0:42 R0C1:-17 R0C2:13 R0C3:-44} " +
                "{R1C0:46 R1C1:-23 R1C2:-22 R1C3:90} " +
                "{R2C0:34 R2C1:-21 R2C2:52 R2C3:33} " +
                "{R3C0:88 R3C1:-12 R3C2:-78 R3C3:50} " +
                "}";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestMemberFn_GetHashCode ()
        {
            var hs1 = new System.Collections.Generic.HashSet<Matrix44>();
            var hs2 = new System.Collections.Generic.HashSet<Int32>();

            for(Int32 i = 0; i < 10000; ++i)
            {
                var a = GetNextRandomMatrix44 ();

                hs1.Add(a);
                hs2.Add(a.GetHashCode ());
            }

            Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
        }

        // Test Constant: Identity //-----------------------------------------//

        /// <summary>
        /// Tests to make sure that a Matrix44 initilised using the Identity 
        /// constant has it's member variables correctly set.
        /// </summary>
        [Test]
        public void TestConstant_Identity_i ()
        {
            Matrix44 result = Matrix44.Identity;
            Matrix44 expected = new Matrix44 (
                1, 0, 0, 0, 
                0, 1, 0, 0, 
                0, 0, 1, 0, 
                0, 0, 0, 1);

            AssertEqualWithinReason(result, expected);
        }        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateTranslation_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateTranslation_ii ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateScale_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        //// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateScale_ii ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateScale_iii ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateRotationX_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateRotationY_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateRotationZ_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateFromAxisAngle_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateFromCartesianAxes_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateWorld_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateFromQuaternion_i ()
        {
            Single yaw; Maths.Pi(out yaw); yaw /= (Single) 4;
            Single pitch; Maths.Pi(out pitch); pitch /= (Single) (-8);
            Single roll; Maths.Pi(out roll); roll /= (Single) 2;

            Quaternion q; Quaternion.CreateFromYawPitchRoll(ref yaw, ref pitch, ref roll, out q);
            q.Normalise();

            Matrix44 m; Matrix44.CreateFromQuaternion(ref q, out m);

            Matrix44 expected = new Matrix44 ();
            expected.R0C0 = Single.Parse("-0.270598"); // this is a grim way to do it, make it so we can cast double to fixed
            expected.R0C1 = Single.Parse("0.9238795");
            expected.R0C2 = Single.Parse("-0.270598");
            expected.R0C3 = 0;
            expected.R1C0 = Single.Parse("-0.7071067");
            expected.R1C1 = Single.Parse("6.705523E-08");
            expected.R1C2 = Single.Parse("0.7071067");
            expected.R1C3 = 0;
            expected.R2C0 = Single.Parse("0.6532815");
            expected.R2C1 = Single.Parse("0.3826834");
            expected.R2C2 = Single.Parse("0.6532815");
            expected.R2C3 = 0;
            expected.R3C0 = 0;
            expected.R3C1 = 0;
            expected.R3C2 = 0;
            expected.R3C3 = 1;

            AssertEqualWithinReason(m, expected);
        }

        /// <summary>
        /// Assert that, for a number of examples, a random quaternion can be
        /// selected, converted to a Matrix44 then converted back to the same
        /// quaternion (assuming that the conversion back is correct).
        /// </summary>
        [Test]
        public void TestStaticFn_CreateFromQuaternion_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                Quaternion q = QuaternionTests.GetNextRandomQuaternion();
                Quaternion.Normalise(ref q, out q);

                Matrix44 m;
                Matrix44.CreateFromQuaternion(ref q, out m);

                Quaternion q2;
                Quaternion.CreateFromRotationMatrix(ref m, out q2);

                QuaternionTests.AssertEqualWithinReason(q, q2);
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateFromYawPitchRoll_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateBillboard_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateConstrainedBillboard_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreatePerspectiveFieldOfView_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreatePerspective_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreatePerspectiveOffCenter_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateOrthographic_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateOrthographicOffCenter_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_CreateLookAt_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        // Test Static Fn: Transpose //---------------------------------------//

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Transpose_i ()
        {
            Matrix44 startMatrix = new Matrix44(
                0, 1, 2, 3,
                4, 5, 6, 7,
                8, 9, 10, 11,
                12, 13, 14, 15);

            Matrix44 testMatrix = startMatrix;

            Matrix44 testMatrixExpectedTranspose =
                new Matrix44(
                    0, 4, 8, 12,
                    1, 5, 9, 13,
                    2, 6, 10, 14,
                    3, 7, 11, 15);

            // RUN THE STATIC VERSION OF THE FUNCTION
            Matrix44 resultMatrix = Matrix44.Identity;

            Matrix44.Transpose(ref testMatrix, out resultMatrix);

            Assert.That(resultMatrix, Is.EqualTo(testMatrixExpectedTranspose));
        }

        // Test Static Fn: Decompose //---------------------------------------//

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Decompose_i ()
        {
            Single a1 = 4;
            Single a2 = 2;
            Single a3 = 3;

            Matrix44 scale;
            Matrix44.CreateScale(ref a1, ref a2, ref a3, out scale);

            Matrix44 rotation;
            Single pi; Maths.Pi(out pi);
            Matrix44.CreateRotationY(ref pi, out rotation);


            Single b1 = 100;
            Single b2 = 5;
            Single b3 = 3;

            Matrix44 translation;
            Matrix44.CreateTranslation(ref b1, ref b2, ref b3, out translation);

            Matrix44 m = rotation * scale;
            //m = translation * m;
            m.Translation = new Vector3(100, 5, 3);

            Vector3 outScale;
            Quaternion outRotation;
            Vector3 outTranslation;

            Boolean decomposeOk;
            Matrix44.Decompose(ref m, out outScale, out outRotation, out outTranslation, out decomposeOk);

            Matrix44 mat;
            Matrix44.CreateFromQuaternion(ref outRotation, out mat);

            Assert.That(outScale, Is.EqualTo(new Vector3(4, 2, 3)));
            Assert.That(mat, Is.EqualTo(rotation));
            Assert.That(outTranslation, Is.EqualTo(new Vector3(100, 5, 3)));
        }

        // Test Static Fn: Determinant //-------------------------------------//

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Determinant_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        // Test Static Fn: Invert //------------------------------------------//

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Invert_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }

        // Test Static Fn: Transform //---------------------------------------//

        /// <summary>
        /// todo
        /// </summary>
        [Test]
        public void TestStaticFn_Transform_i ()
        {
            throw new InconclusiveException("Not Implemented");
        }
        // Test Operator: Equality //-----------------------------------------//

        /// <summary>
        /// Helper method for testing equality.
        /// </summary>
        void TestEquality (Matrix44 a, Matrix44 b, Boolean expected )
        {
            // This test asserts the following:
            //   (a == b) == expected
            //   (b == a) == expected
            //   (a != b) == !expected
            //   (b != a) == !expected

            Boolean result_1a = (a == b);
            Boolean result_1b = (a.Equals(b));
            Boolean result_1c = (a.Equals((Object)b));

            Boolean result_2a = (b == a);
            Boolean result_2b = (b.Equals(a));
            Boolean result_2c = (b.Equals((Object)a));

            Boolean result_3a = (a != b);
            Boolean result_4a = (b != a);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_1c, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
            Assert.That(result_2c, Is.EqualTo(expected));
            Assert.That(result_3a, Is.EqualTo(!expected));
            Assert.That(result_4a, Is.EqualTo(!expected));
        }

        /// <summary>
        /// Makes sure that, for a known example, all the equality opperators
        /// and functions yield the expected result of TRUE when two equal
        /// Matrix44 objects are compared.
        /// </summary>
        [Test]
        public void TestOperator_Equality_i ()
        {
            var a = new Matrix44(44, -54, -22, 11, 44, -54, -22, 11, 44, -54, -22, 11, 44, -54, -22, 11);
            var b = new Matrix44(44, -54, -22, 11, 44, -54, -22, 11, 44, -54, -22, 11, 44, -54, -22, 11);

            Boolean expected = true;

            this.TestEquality(a, b, expected);
        }

        /// <summary>
        /// Makes sure that, for a known example, all the equality opperators
        /// and functions yield the expected result of FALSE when two unequal
        /// Matrix44 objects are compared.
        /// </summary>
        [Test]
        public void TestOperator_Equality_ii ()
        {
            var a = new Matrix44(44, 54, 2, 11, 44, -54, -22, 11, 44, -54, -22, 11, 44, -54, -22, 11);
            var b = new Matrix44(44, -54, 2, -1, 44, -54, -22, 11, 44, -54, -22, 11, 44, -54, -22, 11);

            Boolean expected = false;

            this.TestEquality(a, b, expected);
        }

        /// <summary>
        /// Tests to make sure that all the equality opperators and functions
        /// yield the expected result of TRUE when used on a number of randomly
        /// generated pairs of equal Matrix44 objects.
        /// </summary>
        [Test]
        public void TestOperator_Equality_iii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomMatrix44();

                Matrix44 b = a;

                this.TestEquality(a, b, true);
            }
        }


        // Test Operator: Addition //-----------------------------------------//

        /// <summary>
        /// Helper method for testing addition.
        /// </summary>
        void TestAddition (Matrix44 a, Matrix44 b, Matrix44 expected )
        {
            // This test asserts the following:
            //   a + b == expected
            //   b + a == expected

            var result_1a = a + b;
            var result_2a = b + a;

            Matrix44 result_1b; Matrix44.Add(ref a, ref b, out result_1b);
            Matrix44 result_2b; Matrix44.Add(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for a known example, all the addition opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Addition_i ()
        {
            var a = new Matrix44(
                  3, - 6,  44,  11,
                 44, -34, -22,  11,
                 36, -34, -22,  34,
                 44, -34, - 3,  12);

            var b = new Matrix44(
                - 6,  12,  18, - 3,
                 44, -34, -22,  11,
                 44, -54, -34,  11,
                 34, -54, -22,  11);

            var expected = new Matrix44(
                - 3,   6,  62,  8,
                 88, -68, -44, 22,
                 80, -88, -56, 45,
                 78, -88, -25, 23);

            this.TestAddition(a, b, expected);
        }

        /// <summary>
        /// Assert that, for a known example involving the identity matrix, all the
        /// addition opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Addition_ii ()
        {
            var a = new Matrix44(
                  3, - 6,  44,  11,
                 44, -34, -22,  11,
                 36, -34, -22,  34,
                 44, -34, - 3,  12);

            var expected = a;
            expected.R0C0++;
            expected.R1C1++;
            expected.R2C2++;
            expected.R3C3++;

            this.TestAddition(a, Matrix44.Identity, expected);
        }

        /// <summary>
        /// Assert that, for a known example involving two identity matricies,
        /// all the addition opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Addition_iii ()
        {
            var i = Matrix44.Identity;

            var expected = new Matrix44(
                2, 0, 0, 0,
                0, 2, 0, 0,
                0, 0, 2, 0,
                0, 0, 0, 2);

            this.TestAddition(i, i, expected);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// addition opperators and functions yield the same results as a
        /// manual addition calculation.
        /// </summary>
        [Test]
        public void TestOperator_Addition_iv ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomMatrix44();
                var b = GetNextRandomMatrix44();

                var expected = new Matrix44(
                    a.R0C0 + b.R0C0,
                    a.R0C1 + b.R0C1,
                    a.R0C2 + b.R0C2,
                    a.R0C3 + b.R0C3,
                    a.R1C0 + b.R1C0,
                    a.R1C1 + b.R1C1,
                    a.R1C2 + b.R1C2,
                    a.R1C3 + b.R1C3,
                    a.R2C0 + b.R2C0,
                    a.R2C1 + b.R2C1,
                    a.R2C2 + b.R2C2,
                    a.R2C3 + b.R2C3,
                    a.R3C0 + b.R3C0,
                    a.R3C1 + b.R3C1,
                    a.R3C2 + b.R3C2,
                    a.R3C3 + b.R3C3
                    );

                this.TestAddition(a, b, expected);
            }
        }

        // Test Operator: Subtraction //--------------------------------------//

        /// <summary>
        /// Helper method for testing subtraction.
        /// </summary>
        void TestSubtraction (Matrix44 a, Matrix44 b, Matrix44 expected )
        {
            // This test asserts the following:
            //   a - b == expected
            //   b - a == -expected

            var result_1a = a - b;
            var result_2a = b - a;

            Matrix44 result_1b; Matrix44.Subtract(ref a, ref b, out result_1b);
            Matrix44 result_2b; Matrix44.Subtract(ref b, ref a, out result_2b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_2a, Is.EqualTo(-expected));
            Assert.That(result_1b, Is.EqualTo(expected));
            Assert.That(result_2b, Is.EqualTo(-expected));
        }

        /// <summary>
        /// Assert that, for known examples, all the subtraction opperators
        /// and functions yield the correct result.
        /// <summary>
        [Test]
        public void TestOperator_Subtraction_i ()
        {
            var a = new Matrix44(
                12, -4, 14, 18,
                44, -6, -2, -11,
                34, 54, 4, 5,
                4, -6, 2, 2);

            var b = new Matrix44(
                15, 11, 7, 27,
                4, 1, -4, 11,
                3, 21, -22, 19,
                6, -5, 23, 11);

            var expected = new Matrix44(
                -3, -15, 7, -9,
                40, -7, 2, -22,
                31, 33, 26, -14,
                -2, -1, -21, -9);

            this.TestSubtraction(a, b, expected);

            var c = new Matrix44(
                -423, 342, 7, -800,
                44, -54, -22, 11,
                44, -54, -22, 11,
                44, -54, -22, 11);

            this.TestSubtraction(c, Matrix44.Zero, c);
        }

        /// <summary>
        /// Assert that when subtracting the zero matrix fromt the zero matrix,
        /// all the subtraction opperators and functions yield the correct
        /// result.
        /// <summary>
        [Test]
        public void TestOperator_Subtraction_ii ()
        {
            this.TestSubtraction(
                Matrix44.Identity, Matrix44.Zero, Matrix44.Identity);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// subtraction opperators and functions yield the same results as a
        /// manual subtraction calculation.
        /// </summary>
        [Test]
        public void TestOperator_Subtraction_iii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomMatrix44();
                var b = GetNextRandomMatrix44();

                var expected = new Matrix44(
                    a.R0C0 - b.R0C0,
                    a.R0C1 - b.R0C1,
                    a.R0C2 - b.R0C2,
                    a.R0C3 - b.R0C3,
                    a.R1C0 - b.R1C0,
                    a.R1C1 - b.R1C1,
                    a.R1C2 - b.R1C2,
                    a.R1C3 - b.R1C3,
                    a.R2C0 - b.R2C0,
                    a.R2C1 - b.R2C1,
                    a.R2C2 - b.R2C2,
                    a.R2C3 - b.R2C3,
                    a.R3C0 - b.R3C0,
                    a.R3C1 - b.R3C1,
                    a.R3C2 - b.R3C2,
                    a.R3C3 - b.R3C3
                    );

                this.TestSubtraction(a, b, expected);
            }
        }

        // Test Operator: Negation //-----------------------------------------//

        /// <summary>
        /// Helper method for testing negation.
        /// </summary>
        void TestNegation (Matrix44 a, Matrix44 expected )
        {
            // This test asserts the following:
            //   -a == expected

            var result_1a = -a;

            Matrix44 result_1b;
            Matrix44.Negate(ref a, out result_1b);

            Assert.That(result_1a, Is.EqualTo(expected));
            Assert.That(result_1b, Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that, for known examples, all the negation opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_i ()
        {
            Single r = 3432;
            Single s = -6218;
            Single t = -3432;
            Single u = 6218;

            var a = new Matrix44(
                r, s, t, u,
                r, s, t, u,
                r, s, t, u,
                r, s, t, u);

            var b = new Matrix44(
                u, t, s, r,
                u, t, s, r,
                u, t, s, r,
                u, t, s, r);

            var c = new Matrix44(
                t, u, r, s,
                t, u, r, s,
                t, u, r, s,
                t, u, r, s);

            var d = new Matrix44(
                s, r, u, t,
                s, r, u, t,
                s, r, u, t,
                s, r, u, t);

            this.TestNegation(a, c);
            this.TestNegation(b, d);
        }

        /// <summary>
        /// Assert that, for known examples involving the zero matrix, all the
        /// negation opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_ii ()
        {
            Single t = -3432;
            Single u = 6218;
            Single r = 3432;
            Single s = -6218;

            var c = new Matrix44(
                t, u, r, s,
                r, s, t, u,
                r, s, t, u,
                r, s, t, u);

            var d = new Matrix44(
                s, r, u, t,
                r, s, t, u,
                r, s, t, u,
                r, s, t, u);

            this.TestNegation(c, Matrix44.Zero - c);
            this.TestNegation(d, Matrix44.Zero - d);
        }

        /// <summary>
        /// Assert that when negating the zero matrix, all the
        /// negation opperators and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Negation_iii ()
        {
            this.TestNegation(Matrix44.Zero, Matrix44.Zero);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// negation opperators and functions yield the same results as a
        /// manual negation calculation.
        /// </summary>
        [Test]
        public void TestOperator_Negation_iv ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomMatrix44();
                this.TestNegation(a, Matrix44.Zero - a);
            }
        }

        // Test Operator: Multiplication //-----------------------------------//

        /// <summary>
        /// Helper method for testing multiplication.
        /// </summary>
        void TestMultiplication (Matrix44 a, Matrix44 b, Matrix44 expected )
        {
            // This test asserts the following:
            //   a * b == expected

            Matrix44 result_1b; Matrix44.Multiply(ref a, ref b, out result_1b);
            Assert.That(result_1b, Is.EqualTo(expected));

#if (VARIANTS_ENABLED)
            var result_1a = a * b;
            Assert.That(result_1a, Is.EqualTo(expected));
#endif
        }

        /// <summary>
        /// Assert that, for a known example, all the multiplication opperators
        /// and functions yield the correct result.
        /// </summary>
        [Test]
        public void TestOperator_Multiplication_i ()
        {
            var a = new Matrix44();
            a.R0C0 = -27;
            a.R0C1 = 36;
            a.R0C2 = 9;
            a.R0C3 = -54;

            a.R1C0 = 36;
            a.R1C1 = 3;
            a.R1C2 = 9;
            a.R1C3 = 9;

            a.R2C0 = 9;
            a.R2C1 = 9;
            a.R2C2 = -36;
            a.R2C3 = 6;

            a.R3C0 = -24;
            a.R3C1 = 9;
            a.R3C2 = 36;
            a.R3C3 = -12;

            var b = new Matrix44();
            b.R0C0 = 3402;
            b.R0C1 = -1269;
            b.R0C2 = -2187;
            b.R0C3 = 2484;

            b.R1C0 = -999;
            b.R1C1 = 1467;
            b.R1C2 = 351;
            b.R1C3 = -1971;

            b.R2C0 = -387;
            b.R2C1 = 81;
            b.R2C2 = 1674;
            b.R2C3 = -693;

            b.R3C0 = 1584;
            b.R3C1 = -621;
            b.R3C2 = -1863;
            b.R3C3 = 1737;

            this.TestMultiplication(a, a, b);
        }

        /// <summary>
        /// Assert that, for a number of randomly generated scenarios, all the
        /// multiplication opperators and functions yield the same results as a
        /// manual multiplication calculation.
        /// </summary>
        [Test]
        public void TestOperator_Multiplication_ii ()
        {
            for(Int32 i = 0; i < 100; ++i)
            {
                var a = GetNextRandomMatrix44();
                var b = GetNextRandomMatrix44();

                var c = new Matrix44(
                    (a.R0C0 * b.R0C0) + (a.R0C1 * b.R1C0) +
                    (a.R0C2 * b.R2C0) + (a.R0C3 * b.R3C0),
                    (a.R0C0 * b.R0C1) + (a.R0C1 * b.R1C1) +
                    (a.R0C2 * b.R2C1) + (a.R0C3 * b.R3C1),
                    (a.R0C0 * b.R0C2) + (a.R0C1 * b.R1C2) +
                    (a.R0C2 * b.R2C2) + (a.R0C3 * b.R3C2),
                    (a.R0C0 * b.R0C3) + (a.R0C1 * b.R1C3) +
                    (a.R0C2 * b.R2C3) + (a.R0C3 * b.R3C3),
                    (a.R1C0 * b.R0C0) + (a.R1C1 * b.R1C0) +
                    (a.R1C2 * b.R2C0) + (a.R1C3 * b.R3C0),
                    (a.R1C0 * b.R0C1) + (a.R1C1 * b.R1C1) +
                    (a.R1C2 * b.R2C1) + (a.R1C3 * b.R3C1),
                    (a.R1C0 * b.R0C2) + (a.R1C1 * b.R1C2) +
                    (a.R1C2 * b.R2C2) + (a.R1C3 * b.R3C2),
                    (a.R1C0 * b.R0C3) + (a.R1C1 * b.R1C3) +
                    (a.R1C2 * b.R2C3) + (a.R1C3 * b.R3C3),
                    (a.R2C0 * b.R0C0) + (a.R2C1 * b.R1C0) +
                    (a.R2C2 * b.R2C0) + (a.R2C3 * b.R3C0),
                    (a.R2C0 * b.R0C1) + (a.R2C1 * b.R1C1) +
                    (a.R2C2 * b.R2C1) + (a.R2C3 * b.R3C1),
                    (a.R2C0 * b.R0C2) + (a.R2C1 * b.R1C2) +
                    (a.R2C2 * b.R2C2) + (a.R2C3 * b.R3C2),
                    (a.R2C0 * b.R0C3) + (a.R2C1 * b.R1C3) +
                    (a.R2C2 * b.R2C3) + (a.R2C3 * b.R3C3),
                    (a.R3C0 * b.R0C0) + (a.R3C1 * b.R1C0) +
                    (a.R3C2 * b.R2C0) + (a.R3C3 * b.R3C0),
                    (a.R3C0 * b.R0C1) + (a.R3C1 * b.R1C1) +
                    (a.R3C2 * b.R2C1) + (a.R3C3 * b.R3C1),
                    (a.R3C0 * b.R0C2) + (a.R3C1 * b.R1C2) +
                    (a.R3C2 * b.R2C2) + (a.R3C3 * b.R3C2),
                    (a.R3C0 * b.R0C3) + (a.R3C1 * b.R1C3) +
                    (a.R3C2 * b.R2C3) + (a.R3C3 * b.R3C3));

                this.TestMultiplication(a, b, c);
            }
        }

        /// <summary>
        /// Assert that, running the Lerp function on a number of randomly
        /// generated pairs of Matrix44 objects for a range of weighting amounts,
        /// yields the same results as those obtained from performing a manual
        /// Lerp calculation.
        /// </summary>
        [Test]
        public void TestStaticFn_Lerp_i ()
        {
            for(Int32 j = 0; j < 100; ++j)
            {
                Single delta = j;

                delta = delta / 100;

                for(Int32 i = 0; i < 100; ++i)
                {
                    Matrix44 a = GetNextRandomMatrix44();
                    Matrix44 b = GetNextRandomMatrix44();

                    Matrix44 result;
                    Matrix44.Lerp (ref a, ref b, ref delta, out result);

                    Matrix44 expected = a + ( ( b - a ) * delta );

                    AssertEqualWithinReason(result, expected);
                }
            }
        }

        /// <summary>
        /// Assert that, for a known examples where the weighting parameter is
        /// is outside the allowed range, the correct exception is thrown.
        /// </summary>
        [Test]
        public void TestStaticFn_Lerp_ii ()
        {
            Matrix44 a = GetNextRandomMatrix44();
            Matrix44 b = GetNextRandomMatrix44();

            Single half; Maths.Half(out half);

            var tests = new Single[] { 2, half + 1, -half, -1 };

            for( Int32 i = 0; i < tests.Length; ++i )
            {
                Matrix44 result;
                Assert.Throws(
                    typeof(ArgumentOutOfRangeException),
                    () =>
                        Matrix44.Lerp (
                            ref a, ref b, ref tests[i], out result)
                    );
            }
        }
    }
}

#endif
