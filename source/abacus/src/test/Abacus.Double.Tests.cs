// ┌────────────────────────────────────────────────────────────────────────┐ \\
// │    _____ ___.                                                          │ \\
// │   /  _  \\_ |__ _____    ____  __ __  ______                           │ \\
// │  /  /_\  \| __ \\__  \ _/ ___\|  |  \/  ___/                           │ \\
// │ /    |    \ \_\ \/ __ \\  \___|  |  /\___ \                            │ \\
// │ \____|__  /___  (____  /\___  >____//____  >                           │ \\
// │         \/    \/     \/     \/           \/  v1.0.0                    │ \\
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
// │ Copyright © 2012 - 2020 Ash Pook                                       │ \\
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
using System.Collections.Generic;
using NUnit.Framework;

namespace Abacus.DoublePrecision {

    [TestFixture]
    public class MathsTests {
        public static readonly Double TestTolerance = 0.000001;
        public static readonly Double PercentageTolerance = 0.0000001;


    }

    [TestFixture]
    public class Vector2Tests {
        static readonly Random rand = new Random(0);

        static Double GetNextRandomDouble () {
            Double randomValue = (Double) rand.NextDouble();
            Double multiplier = 1000;
            randomValue *= multiplier;
            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;
            if (randomBoolean) randomValue = 0 - randomValue;
            return randomValue;
        }

        static Vector2 GetNextRandomVector2 () {
            Double a = GetNextRandomDouble();
            Double b = GetNextRandomDouble();
            return new Vector2(a, b);
        }

        static void AssertEqualWithinReason (Vector2 a, Vector2 b) {
            Assert.That(a.X, Is.EqualTo(b.X).Within(MathsTests.TestTolerance));
            Assert.That(a.Y, Is.EqualTo(b.Y).Within(MathsTests.TestTolerance));
        }

        [Test]
        public void TestMemberFn_GetHashCode_i () {
            var hs1 = new HashSet<Vector2>();
            var hs2 = new HashSet<Int32>();
            for(Int32 i = 0; i < 10000; ++i) {
                var a = GetNextRandomVector2();
                hs1.Add(a);
                hs2.Add(a.GetHashCode());
            }
            Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
        }
    }
    [TestFixture]
    public class Vector3Tests {
        static readonly Random rand = new Random(0);

        static Double GetNextRandomDouble () {
            Double randomValue = (Double) rand.NextDouble();
            Double multiplier = 1000;
            randomValue *= multiplier;
            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;
            if (randomBoolean) randomValue = 0 - randomValue;
            return randomValue;
        }

        static Vector3 GetNextRandomVector3 () {
            Double a = GetNextRandomDouble();
            Double b = GetNextRandomDouble();
            Double c = GetNextRandomDouble();
            return new Vector3(a, b, c);
        }

        static void AssertEqualWithinReason (Vector3 a, Vector3 b) {
            Assert.That(a.X, Is.EqualTo(b.X).Within(MathsTests.TestTolerance));
            Assert.That(a.Y, Is.EqualTo(b.Y).Within(MathsTests.TestTolerance));
            Assert.That(a.Z, Is.EqualTo(b.Z).Within(MathsTests.TestTolerance));
        }

        [Test]
        public void Test_GetHashCode () {
            var hs1 = new HashSet<Vector3>();
            var hs2 = new HashSet<Int32>();
            for(Int32 i = 0; i < 10000; ++i) {
                var a = GetNextRandomVector3();
                hs1.Add(a);
                hs2.Add(a.GetHashCode());
            }
            Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
        }
    }
    [TestFixture]
    public class Vector4Tests {
        static readonly Random rand = new Random(0);

        static Double GetNextRandomDouble () {
            Double randomValue = (Double) rand.NextDouble();
            Double multiplier = 1000;
            randomValue *= multiplier;
            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;
            if (randomBoolean) randomValue = 0 - randomValue;
            return randomValue;
        }

        static Vector4 GetNextRandomVector4 () {
            Double a = GetNextRandomDouble();
            Double b = GetNextRandomDouble();
            Double c = GetNextRandomDouble();
            Double d = GetNextRandomDouble();
            return new Vector4(a, b, c, d);
        }

        static void AssertEqualWithinReason (Vector4 a, Vector4 b) {
            Assert.That(a.X, Is.EqualTo(b.X).Within(MathsTests.TestTolerance));
            Assert.That(a.Y, Is.EqualTo(b.Y).Within(MathsTests.TestTolerance));
            Assert.That(a.Z, Is.EqualTo(b.Z).Within(MathsTests.TestTolerance));
            Assert.That(a.W, Is.EqualTo(b.W).Within(MathsTests.TestTolerance));
        }

        [Test]
        public void Test_GetHashCode () {
            var hs1 = new HashSet<Vector4>();
            var hs2 = new HashSet<Int32>();
            for(Int32 i = 0; i < 10000; ++i) {
                var a = GetNextRandomVector4();
                hs1.Add(a);
                hs2.Add(a.GetHashCode());
            }
            Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
        }
    }
    [TestFixture]
    public class QuaternionTests {
        static readonly Random rand = new Random(0);
        
        static Double GetNextRandomDouble () {
            Double randomValue = (Double) rand.NextDouble();
            Double multiplier = 1000;
            randomValue *= multiplier;
            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;
            if (randomBoolean) randomValue = 0 - randomValue;
            return randomValue;
        }

        internal static Quaternion GetNextRandomQuaternion () {
            Double yaw = Maths.Pi * (Double) rand.Next(0, 360) / (Double) 180;
            Double pitch = Maths.Pi * (Double) rand.Next(0, 360) / (Double) 180;
            Double roll = Maths.Pi * (Double) rand.Next(0, 360) / (Double) 180;
            Quaternion q; Quaternion.CreateFromYawPitchRoll(ref yaw, ref pitch, ref roll, out q);
            return q;
        }

        internal static void AssertEqualWithinReason (Quaternion a, Quaternion b) {
            Assert.That(a.I, Is.EqualTo(b.I).Within(MathsTests.TestTolerance));
            Assert.That(a.J, Is.EqualTo(b.J).Within(MathsTests.TestTolerance));
            Assert.That(a.K, Is.EqualTo(b.K).Within(MathsTests.TestTolerance));
            Assert.That(a.U, Is.EqualTo(b.U).Within(MathsTests.TestTolerance));
        }

        internal static void AssertEqualOrNegatedWithinReason (Quaternion a, Quaternion b) {
            Boolean pass1 =
                Maths.Abs (a.I - b.I) <= MathsTests.TestTolerance &&
                Maths.Abs (a.J - b.J) <= MathsTests.TestTolerance &&
                Maths.Abs (a.K - b.K) <= MathsTests.TestTolerance &&
                Maths.Abs (a.U - b.U) <= MathsTests.TestTolerance;

            Quaternion c;
            Quaternion.Negate (ref b, out c);

            Boolean pass2 =
                Maths.Abs (a.I - c.I) <= MathsTests.TestTolerance &&
                Maths.Abs (a.J - c.J) <= MathsTests.TestTolerance &&
                Maths.Abs (a.K - c.K) <= MathsTests.TestTolerance &&
                Maths.Abs (a.U - c.U) <= MathsTests.TestTolerance;

            Assert.That(pass1 || pass2, Is.EqualTo (true));
        }

        [Test]
        public void TestMemberFn_GetHashCode () {
            var hs1 = new HashSet<Quaternion>();
            var hs2 = new HashSet<Int32>();
            for(Int32 i = 0; i < 10000; ++i) {
                var a = GetNextRandomQuaternion();
                hs1.Add(a);
                hs2.Add(a.GetHashCode());
            }
            Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
        }
    }

    [TestFixture]
    public class Matrix44Tests {
        static readonly Random rand = new Random(0);
        
        static Double GetNextRandomDouble () {
            Double randomValue = (Double) rand.NextDouble();
            Double zero = 0;
            Double multiplier = 1000;
            randomValue *= multiplier;
            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;
            if (randomBoolean) randomValue = zero - randomValue;
            return randomValue;
        }

        internal static Matrix44 GetNextRandomMatrix44 () {
            Double a = GetNextRandomDouble();
            Double b = GetNextRandomDouble();
            Double c = GetNextRandomDouble();
            Double d = GetNextRandomDouble();
            Double e = GetNextRandomDouble();
            Double f = GetNextRandomDouble();
            Double g = GetNextRandomDouble();
            Double h = GetNextRandomDouble();
            Double i = GetNextRandomDouble();
            Double j = GetNextRandomDouble();
            Double k = GetNextRandomDouble();
            Double l = GetNextRandomDouble();
            Double m = GetNextRandomDouble();
            Double n = GetNextRandomDouble();
            Double o = GetNextRandomDouble();
            Double p = GetNextRandomDouble();
            return new Matrix44(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p);
        }

        internal static void AssertEqualWithinReason (Matrix44 a, Matrix44 b) {
            Assert.That(a.R0C0, Is.EqualTo(b.R0C0).Within(MathsTests.TestTolerance));
            Assert.That(a.R0C1, Is.EqualTo(b.R0C1).Within(MathsTests.TestTolerance));
            Assert.That(a.R0C2, Is.EqualTo(b.R0C2).Within(MathsTests.TestTolerance));
            Assert.That(a.R0C3, Is.EqualTo(b.R0C3).Within(MathsTests.TestTolerance));
            Assert.That(a.R1C0, Is.EqualTo(b.R1C0).Within(MathsTests.TestTolerance));
            Assert.That(a.R1C1, Is.EqualTo(b.R1C1).Within(MathsTests.TestTolerance));
            Assert.That(a.R1C2, Is.EqualTo(b.R1C2).Within(MathsTests.TestTolerance));
            Assert.That(a.R1C3, Is.EqualTo(b.R1C3).Within(MathsTests.TestTolerance));
            Assert.That(a.R2C0, Is.EqualTo(b.R2C0).Within(MathsTests.TestTolerance));
            Assert.That(a.R2C1, Is.EqualTo(b.R2C1).Within(MathsTests.TestTolerance));
            Assert.That(a.R2C2, Is.EqualTo(b.R2C2).Within(MathsTests.TestTolerance));
            Assert.That(a.R2C3, Is.EqualTo(b.R2C3).Within(MathsTests.TestTolerance));
            Assert.That(a.R3C0, Is.EqualTo(b.R3C0).Within(MathsTests.TestTolerance));
            Assert.That(a.R3C1, Is.EqualTo(b.R3C1).Within(MathsTests.TestTolerance));
            Assert.That(a.R3C2, Is.EqualTo(b.R3C2).Within(MathsTests.TestTolerance));
            Assert.That(a.R3C3, Is.EqualTo(b.R3C3).Within(MathsTests.TestTolerance));
        }

        [Test]
        public void TestMemberFn_GetHashCode () {
            var hs1 = new HashSet<Matrix44>();
            var hs2 = new HashSet<Int32>();
            for(Int32 i = 0; i < 10000; ++i) {
                var a = GetNextRandomMatrix44 ();
                hs1.Add(a);
                hs2.Add(a.GetHashCode ());
            }
            Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
        }
    }
}
