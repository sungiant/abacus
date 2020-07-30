// ┌────────────────────────────────────────────────────────────────────────┐ \\
// │    _____ ___.                                                          │ \\
// │   /  _  \\_ |__ _____    ____  __ __  ______                           │ \\
// │  /  /_\  \| __ \\__  \ _/ ___\|  |  \/  ___/                           │ \\
// │ /    |    \ \_\ \/ __ \\  \___|  |  /\___ \                            │ \\
// │ \____|__  /___  (____  /\___  >____//____  >                           │ \\
// │         \/    \/     \/     \/           \/  v1.0.2                    │ \\
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

namespace Abacus.Fixed64Precision {

    [TestFixture]
    public class Fixed64Tests {

        static readonly Random rand = new Random(0);
        
        static readonly Double[] d_test_values = { 0.9, 0.5, 0.1, 0.01, 0.001, -0.001, -0.01, -0.1, -0.5, -0.9, -9000, 3802, 3.14159265, 0.0, 10, 100, 2.0, 4.0, 16.0, 64.0 };

        static readonly Double d_min_value = Fixed64.MinValue.ToDouble();
        static readonly Double d_max_value = Fixed64.MaxValue.ToDouble();

        static readonly Double i64_min_value = Fixed64.MinValue.ToInt64();
        static readonly Double i64_max_value = Fixed64.MaxValue.ToInt64();

        // ---- //

        [Test]
        public void TestDoubleConversion () {
            foreach (Double value in d_test_values) {
                Fixed64 f = value;
                Assert.That (f.ToDouble (), Is.EqualTo (value).Within (MathsTests.TestTolerance));
            }
        }

        [Test] // check that a double representing the known minimum converts back and forth properly.
        public void TestDoubleConversion_MinValue () {
            Fixed64 fmin = d_min_value;
            Assert.That (fmin.ToDouble (), Is.EqualTo (d_min_value).Within (MathsTests.TestTolerance));
        }

        [Test] // check that underflow clamps to minimum value.
        public void TestDoubleConversion_MinValueUnderflow () { 
            Fixed64 fminUnder = d_min_value - 10.0;
            Assert.That (fminUnder.ToDouble (), Is.EqualTo (d_min_value).Within (MathsTests.TestTolerance));
        }

        [Test] // check that a double representing the known maximum converts back and forth properly.
        public void TestDoubleConversion_MaxValue () { 
            Fixed64 fmax = d_max_value;
            Assert.That (fmax.ToDouble (), Is.EqualTo (d_max_value).Within (MathsTests.TestTolerance));
        }

        [Test] // check that overflow clamps to maximum value.
        public void TestDoubleConversion_MaxValueOverflow () { 
            Fixed64 fmaxOver = d_max_value + 10.0;
            Assert.That (fmaxOver.ToDouble (), Is.EqualTo (d_max_value).Within (MathsTests.TestTolerance));
        }

        // ---- //

        [Test]
        public void TestInt64Conversion_MinValue () {
            Fixed64 fmin = i64_min_value;
            Assert.That (fmin.ToInt64 (), Is.EqualTo (i64_min_value).Within (MathsTests.TestTolerance));
        }

        [Test]
        public void TestInt64Conversion_MinValueUnderflow () {
            Fixed64 fminUnder = i64_min_value - 10;
            Assert.That (fminUnder.ToDouble (), Is.EqualTo (i64_min_value).Within (MathsTests.TestTolerance));
        }

        [Test]
        public void TestInt64Conversion_MaxValue () {
            Fixed64 fmax = i64_max_value;
            Assert.That (fmax.ToInt64 (), Is.EqualTo (i64_max_value).Within (MathsTests.TestTolerance));
        }

        [Test]
        public void TestInt64Conversion_MaxValueOverflow () {
            Fixed64 fmaxOver = i64_max_value + 10;
            Assert.That (fmaxOver.ToInt64 (), Is.EqualTo (i64_max_value).Within (MathsTests.TestTolerance));
        }

        // ---- //

        [Test]
        public void TestGetHashCode () {
            var hs1 = new HashSet<Fixed64>();
            var hs2 = new HashSet<Int32>();
            for(Int32 i = 0; i < 10000; ++i) {
                Fixed64 randomValue = (Fixed64) rand.NextDouble();
                hs1.Add(randomValue);
                hs2.Add(randomValue.GetHashCode());
            }
            Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
        }

        [Test]
        public void TestEquality () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                for (int j = i; j < d_test_values.Length; ++ j) {
                    Fixed64 fi = d_test_values[i];
                    Fixed64 fj = d_test_values[j];
                    Boolean result = fi == fj;
                    Boolean expected = d_test_values[i] == d_test_values[j];
                    Assert.That (result, Is.EqualTo (expected));
                }
            }
        }

        [Test]
        public void TestInequality () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                for (int j = i; j < d_test_values.Length; ++ j) {
                    Fixed64 fi = d_test_values[i], fj = d_test_values[j];
                    Boolean result = fi != fj;
                    Boolean expected = d_test_values[i] != d_test_values[j];
                    Assert.That (result, Is.EqualTo (expected));
                }
            }
        }

        [Test]
        public void TestLessThan () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                for (int j = i; j < d_test_values.Length; ++ j) {
                    Fixed64 fi = d_test_values[i], fj = d_test_values[j];
                    Boolean result = fi < fj;
                    Boolean expected = d_test_values[i] < d_test_values[j];
                    Assert.That (result, Is.EqualTo (expected));
                }
            }
        }

        [Test]
        public void TestLessThanOrEqualTo () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                for (int j = i; j < d_test_values.Length; ++ j) {
                    Fixed64 fi = d_test_values[i], fj = d_test_values[j];
                    Boolean result = fi <= fj;
                    Boolean expected = d_test_values[i] <= d_test_values[j];
                    Assert.That (result, Is.EqualTo (expected));
                }
            }
        }

        [Test]
        public void TestGreaterThan () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                for (int j = i; j < d_test_values.Length; ++ j) {
                    Fixed64 fi = d_test_values[i], fj = d_test_values[j];
                    Boolean result = fi > fj;
                    Boolean expected = d_test_values[i] > d_test_values[j];
                    Assert.That (result, Is.EqualTo (expected));
                }
            }
        }

        [Test]
        public void TestGreaterThanOrEqualTo () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                for (int j = i; j < d_test_values.Length; ++ j) {
                    Fixed64 fi = d_test_values[i], fj = d_test_values[j];
                    Boolean result = fi >= fj;
                    Boolean expected = d_test_values[i] >= d_test_values[j];
                    Assert.That (result, Is.EqualTo (expected));
                }
            }
        }

        // ---- //

        [Test]
        public void TestAddition () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                for (int j = i; j < d_test_values.Length; ++ j) {
                    Fixed64 fi = d_test_values[i], fj = d_test_values[j];
                    Fixed64 fk = fi + fj;
                    Double result = fk.ToDouble ();
                    Double expected = DoublePrecision.Maths.Clamp (d_test_values[i] + d_test_values[j], d_min_value, d_max_value);
                    Assert.That (result, Is.EqualTo (expected).Within (MathsTests.TestTolerance));
                }
            }
        }

        [Test]
        public void TestAddition_Overflow () {
            Fixed64 t = 10;
            Fixed64 result = Fixed64.MaxValue + t;
            Fixed64 expected = Fixed64.MaxValue;
            Assert.That (result, Is.EqualTo (expected));
        }

        [Test]
        public void TestAddition_Underflow () {
            Fixed64 t = - 10;
            Fixed64 result = Fixed64.MinValue + t;
            Fixed64 expected = Fixed64.MinValue;
            Assert.That (result, Is.EqualTo (expected));
        }

        [Test]
        public void TestSubtraction () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                for (int j = i; j < d_test_values.Length; ++ j) {
                    Fixed64 fi = d_test_values[i], fj = d_test_values[j];
                    Fixed64 fk = fi - fj;
                    Double result = fk.ToDouble ();
                    Double expected = DoublePrecision.Maths.Clamp (d_test_values[i] - d_test_values[j], d_min_value, d_max_value);
                    Assert.That (result, Is.EqualTo (expected).Within (MathsTests.TestTolerance));
                }
            }
        }

        [Test]
        public void TestSubtraction_Overflow () {
            Fixed64 t = - 10;
            Fixed64 result = Fixed64.MaxValue - t;
            Fixed64 expected = Fixed64.MaxValue;
            Assert.That (result, Is.EqualTo (expected));
        }

        [Test]
        public void TestSubtraction_Underflow () {
            Fixed64 t = 10;
            Fixed64 result = Fixed64.MinValue - t;
            Fixed64 expected = Fixed64.MinValue;
            Assert.That (result, Is.EqualTo (expected));
        }

        [Test]
        public void TestNegation () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                Fixed64 fi = d_test_values[i];
                Fixed64 fk = -fi;
                Double result = fk.ToDouble ();
                Double expected = DoublePrecision.Maths.Clamp (-d_test_values[i], d_min_value, d_max_value);
                Assert.That (result, Is.EqualTo (expected).Within (MathsTests.TestTolerance));
            }
        }

        [Test]
        public void TestNegation_Overflow () {
            Fixed64 result = -Fixed64.MinValue;
            Fixed64 expected = Fixed64.MaxValue;
            Assert.That (result, Is.EqualTo (expected));
        }

        [Test]
        public void TestNegation_NoUnderflow () {
            Fixed64 result = -Fixed64.MaxValue;
            Fixed64 expected = Fixed64.MinValue + Fixed64.Resolution;
            Assert.That (result, Is.EqualTo (expected));
        }

        [Test]
        public void TestMultiplication () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                for (int j = i; j < d_test_values.Length; ++ j) {
                    Fixed64 fi = d_test_values[i], fj = d_test_values[j];
                    Fixed64 fk = fi * fj;
                    Double result = fk.ToDouble ();
                    Double expected = DoublePrecision.Maths.Clamp (d_test_values[i] * d_test_values[j], d_min_value, d_max_value);
                    Assert.That (result,
                        Is.EqualTo (expected).Within (MathsTests.TestTolerance).                            // Check that result is within test tolerance for Fixed64
                        Or.EqualTo (expected).Within (MathsTests.PercentageTolerance * Math.Abs (result))); // or that result is within test percentage for Fixed64.
                }
            }
        }

        [Test]
        public void TestMultiplication_Overflow () {
            Fixed64 expected = Fixed64.MaxValue;
            Fixed64 t = 10;
            Fixed64 result1 = Fixed64.MaxValue * t;
            Fixed64 result2 = Fixed64.MinValue * -t;
            Assert.That (result1, Is.EqualTo (expected));
            Assert.That (result2, Is.EqualTo (expected));
        }

        [Test]
        public void TestMultiplication_Underflow () {
            Fixed64 expected = Fixed64.MinValue;
            Fixed64 t = 10;
            Fixed64 result1 = Fixed64.MinValue * t;
            Fixed64 result2 = Fixed64.MaxValue * -t;
            Assert.That (result1, Is.EqualTo (expected));
            Assert.That (result2, Is.EqualTo (expected));
        }

        [Test]
        public void TestDivision () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                for (int j = i; j < d_test_values.Length; ++ j) {
                    Fixed64 fi = d_test_values[i], fj = d_test_values[j];
                    if (fj == 0.0)
                        continue;
                    Fixed64 fk = fi / fj;
                    Double result = fk.ToDouble ();
                    Double expected = DoublePrecision.Maths.Clamp (d_test_values[i] / d_test_values[j], d_min_value, d_max_value);
                    Assert.That (result,
                        Is.EqualTo (expected).Within (MathsTests.TestTolerance).                            // Check that result is within test tolerance for Fixed64
                        Or.EqualTo (expected).Within (MathsTests.PercentageTolerance * Math.Abs (result))); // or that result is within test percentage for Fixed64.
                }
            }
        }

        [Test]
        public void TestDivision_Overflow () {
            Fixed64 expected = Fixed64.MaxValue;
            Fixed64 t = 0.1;
            Fixed64 result1 = Fixed64.MaxValue / t;
            Fixed64 result2 = Fixed64.MinValue / -t;
            Assert.That (result1, Is.EqualTo (expected));
            Assert.That (result2, Is.EqualTo (expected));
        }

        [Test]
        public void TestDivision_Underflow () {
            Fixed64 expected = Fixed64.MinValue;
            Fixed64 t = 0.1;
            Fixed64 result1 = Fixed64.MinValue / t;
            Fixed64 result2 = Fixed64.MaxValue / -t;
            Assert.That (result1, Is.EqualTo (expected));
            Assert.That (result2, Is.EqualTo (expected));
        }

        [Test]
        public void TestDivision_Small () {
            Fixed64 t = Fixed64.Epsilon;
            Fixed64 result = t / 1000000;
            Fixed64 expected = 0;
            Assert.That (result, Is.EqualTo (expected));
        }

        [Test]
        public void TestSqrt () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                Fixed64 fi = d_test_values[i];
                Fixed64 fs = fi; Fixed64.Sqrt (ref fs, out fs);
                Double result = fs.ToDouble ();
                Double expected = Math.Sqrt (d_test_values[i]);
                if (Double.IsNaN (expected)) {
                    Assert.That (result, Is.EqualTo (0));
                }
                else {
                    Fixed64 f = expected;
                    Assert.That (f.ToDouble (), Is.EqualTo (expected).Within (MathsTests.TestTolerance));   // double check storage
                    Assert.That (result,
                        Is.EqualTo (expected).Within (MathsTests.TestTolerance).                            // Check that result is within test tolerance for Fixed64
                        Or.EqualTo (expected).Within (MathsTests.PercentageTolerance * Math.Abs (result))); // or that result is within test percentage for Fixed64.
                }
            }
        }
        
        [Test]
        public void TestModulo () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                for (int j = i; j < d_test_values.Length; ++ j) {
                    Fixed64 fi = d_test_values[i], fj = d_test_values[j];
                    if (fj == 0.0)
                        continue;
                    Fixed64 fk = fi % fj;
                    Double result = fk.ToDouble ();
                    Double expected = DoublePrecision.Maths.Clamp (d_test_values[i] % d_test_values[j], d_min_value, d_max_value);
                    Assert.That (result,
                        Is.EqualTo (expected).Within (MathsTests.TestTolerance).                            // Check that result is within test tolerance for Fixed64
                        Or.EqualTo (expected).Within (MathsTests.PercentageTolerance * Math.Abs (result))); // or that result is within test percentage for Fixed64.
                }
            }
        }
        
        [Test]
        public void TestAbs () {
            for (int i = 0; i < d_test_values.Length; ++ i) {
                Fixed64 fi = d_test_values[i];
                Fixed64 fa = fi;
                Fixed64.Abs (ref fa, out fa);
                Double result = fa.ToDouble ();
                Double expected = Math.Abs (d_test_values[i]);
                Fixed64 f = expected;
                Assert.That (result, Is.EqualTo (expected).Within (MathsTests.TestTolerance));
            }
        }

        [Test]
        public void TestAbs_Overflow () {
            Fixed64 result = Fixed64.MinValue;
            Fixed64.Abs (ref result, out result);
            Fixed64 expected = Fixed64.MaxValue;
            Assert.That (result, Is.EqualTo (expected));
        
        }

        [Test]
        public void TestSin () {
            for (Double deg = -360.0; deg <= 360.0; deg += 0.1) {
                Double rad = deg * DoublePrecision.Maths.Deg2Rad;
                Fixed64 f = rad;
                Fixed64 fs = Fixed64.Sin (f);
                Double result = fs.ToDouble ();
                Double expected = Math.Sin (rad);
                Assert.That (result,
                    Is.EqualTo (expected).Within (MathsTests.TestTolerance).                            // Check that result is within test tolerance for Fixed64
                    Or.EqualTo (expected).Within (MathsTests.PercentageTolerance * Math.Abs (result))); // or that result is within test percentage for Fixed64.
            }
        }

        [Test]
        public void TestCos () {
            for (Double deg = -360.0; deg <= 360.0; deg += 0.1) {
                Double rad = deg * DoublePrecision.Maths.Deg2Rad;
                Fixed64 f = rad;
                Fixed64 fs = Fixed64.Cos (f);
                Double result = fs.ToDouble ();
                Double expected = Math.Cos (rad);
                Assert.That (result,
                    Is.EqualTo (expected).Within (MathsTests.TestTolerance).                            // Check that result is within test tolerance for Fixed64
                    Or.EqualTo (expected).Within (MathsTests.PercentageTolerance * Math.Abs (result))); // or that result is within test percentage for Fixed64.
            }
        }

        [Test]
        public void TestTan () {
            for (Double deg = -360.0; deg <= 360.0; deg += 0.1) {
                Double rad = deg * DoublePrecision.Maths.Deg2Rad;
                Fixed64 f = rad;
                Fixed64 fs = Fixed64.Tan (f);
                Double result = fs.ToDouble ();
                Double expected = Math.Tan (rad);

                if (Double.IsNaN (expected)) {
                    Assert.That (result, Is.EqualTo (0));
                }
                else if (expected < d_min_value) {
                    Assert.That (result, Is.EqualTo (0));
                }
                else if (expected > d_max_value) {
                    Assert.That (result, Is.EqualTo (0));
                }
                else {
                    Assert.That (result,
                        Is.EqualTo (expected).Within (MathsTests.TestTolerance).                            // Check that result is within test tolerance for Fixed64
                        Or.EqualTo (expected).Within (MathsTests.PercentageTolerance * Math.Abs (result))); // or that result is within test percentage for Fixed64.
                }
            }
        }

        [Test]
        public void TestArcSin () {
            for (Double x = -1.0; x <= 1.0; x += 0.01) {
                Double rad = x * DoublePrecision.Maths.Deg2Rad;
                Fixed64 f = rad;
                Fixed64 fs = Fixed64.ArcSin (f);
                Double result = fs.ToDouble ();
                Double expected = Math.Asin (rad);
                Assert.That (result,
                    Is.EqualTo (expected).Within (MathsTests.TestTolerance).                            // Check that result is within test tolerance for Fixed64
                    Or.EqualTo (expected).Within (MathsTests.PercentageTolerance * Math.Abs (result))); // or that result is within test percentage for Fixed64.
            }
        }

        [Test]
        public void TestArcCos () {
            for (Double x = -1.0; x <= 1.0; x += 0.01) {
                Double rad = x * DoublePrecision.Maths.Deg2Rad;
                Fixed64 f = rad;
                Fixed64 fs = Fixed64.ArcCos (f);
                Double result = fs.ToDouble ();
                Double expected = Math.Acos (rad);
                Assert.That (result,
                    Is.EqualTo (expected).Within (MathsTests.TestTolerance).                            // Check that result is within test tolerance for Fixed64
                    Or.EqualTo (expected).Within (MathsTests.PercentageTolerance * Math.Abs (result))); // or that result is within test percentage for Fixed64.
            }
        }

        [Test]
        public void TestArcTan () {
            for (Double x = -360.0; x <= 360.0; x += 0.01) {
                Fixed64 f = x;
                Fixed64 fs = Fixed64.ArcTan (f);
                Double result = fs.ToDouble ();
                Double expected = Math.Atan (x);
                Assert.That (result,
                    Is.EqualTo (expected).Within (MathsTests.TestTolerance).                            // Check that result is within test tolerance for Fixed64
                    Or.EqualTo (expected).Within (MathsTests.PercentageTolerance * Math.Abs (result))); // or that result is within test percentage for Fixed64.
            }
        }
    }

    [TestFixture]
    public class MathsTests {
        public static readonly Double TestTolerance = 0.01;
        public static readonly Double PercentageTolerance = 0.0001;

        [Test]
        public void TestConstants () {
            Assert.That (Maths.E.ToDouble (), Is.EqualTo (DoublePrecision.Maths.E).Within (MathsTests.TestTolerance));
            Assert.That (Maths.Half.ToDouble (), Is.EqualTo (DoublePrecision.Maths.Half).Within (MathsTests.TestTolerance));
            Assert.That (Maths.Quarter.ToDouble (), Is.EqualTo (DoublePrecision.Maths.Quarter).Within (MathsTests.TestTolerance));
            Assert.That (Maths.Log10E.ToDouble (), Is.EqualTo (DoublePrecision.Maths.Log10E).Within (MathsTests.TestTolerance));
            Assert.That (Maths.Log2E.ToDouble (), Is.EqualTo (DoublePrecision.Maths.Log2E).Within (MathsTests.TestTolerance));
            Assert.That (Maths.Pi.ToDouble (), Is.EqualTo (DoublePrecision.Maths.Pi).Within (MathsTests.TestTolerance));
            Assert.That (Maths.HalfPi.ToDouble (), Is.EqualTo (DoublePrecision.Maths.HalfPi).Within (MathsTests.TestTolerance));
            Assert.That (Maths.QuarterPi.ToDouble (), Is.EqualTo (DoublePrecision.Maths.QuarterPi).Within (MathsTests.TestTolerance));
            Assert.That (Maths.Root2.ToDouble (), Is.EqualTo (DoublePrecision.Maths.Root2).Within (MathsTests.TestTolerance));
            Assert.That (Maths.Root3.ToDouble (), Is.EqualTo (DoublePrecision.Maths.Root3).Within (MathsTests.TestTolerance));
            Assert.That (Maths.Tau.ToDouble (), Is.EqualTo (DoublePrecision.Maths.Tau).Within (MathsTests.TestTolerance));
            Assert.That (Maths.Rad2Deg.ToDouble (), Is.EqualTo (DoublePrecision.Maths.Rad2Deg).Within (MathsTests.TestTolerance));
            Assert.That (Maths.Deg2Rad.ToDouble (), Is.EqualTo (DoublePrecision.Maths.Deg2Rad).Within (MathsTests.TestTolerance));
            Assert.That (Maths.Zero.ToDouble (), Is.EqualTo (DoublePrecision.Maths.Zero).Within (MathsTests.TestTolerance));
            Assert.That (Maths.One.ToDouble (), Is.EqualTo (DoublePrecision.Maths.One).Within (MathsTests.TestTolerance));
            
        }

    }

    [TestFixture]
    public class Vector2Tests {
        static readonly Random rand = new Random(0);

        static Fixed64 GetNextRandomFixed64 () {
            Fixed64 randomValue = (Fixed64) rand.NextDouble();
            Fixed64 multiplier = 1000;
            randomValue *= multiplier;
            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;
            if (randomBoolean) randomValue = 0 - randomValue;
            return randomValue;
        }

        static Vector2 GetNextRandomVector2 () {
            Fixed64 a = GetNextRandomFixed64();
            Fixed64 b = GetNextRandomFixed64();
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

        static Fixed64 GetNextRandomFixed64 () {
            Fixed64 randomValue = (Fixed64) rand.NextDouble();
            Fixed64 multiplier = 1000;
            randomValue *= multiplier;
            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;
            if (randomBoolean) randomValue = 0 - randomValue;
            return randomValue;
        }

        static Vector3 GetNextRandomVector3 () {
            Fixed64 a = GetNextRandomFixed64();
            Fixed64 b = GetNextRandomFixed64();
            Fixed64 c = GetNextRandomFixed64();
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

        static Fixed64 GetNextRandomFixed64 () {
            Fixed64 randomValue = (Fixed64) rand.NextDouble();
            Fixed64 multiplier = 1000;
            randomValue *= multiplier;
            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;
            if (randomBoolean) randomValue = 0 - randomValue;
            return randomValue;
        }

        static Vector4 GetNextRandomVector4 () {
            Fixed64 a = GetNextRandomFixed64();
            Fixed64 b = GetNextRandomFixed64();
            Fixed64 c = GetNextRandomFixed64();
            Fixed64 d = GetNextRandomFixed64();
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
        
        static Fixed64 GetNextRandomFixed64 () {
            Fixed64 randomValue = (Fixed64) rand.NextDouble();
            Fixed64 multiplier = 1000;
            randomValue *= multiplier;
            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;
            if (randomBoolean) randomValue = 0 - randomValue;
            return randomValue;
        }

        internal static Quaternion GetNextRandomQuaternion () {
            Fixed64 yaw = Maths.Pi * (Fixed64) rand.Next(0, 360) / (Fixed64) 180;
            Fixed64 pitch = Maths.Pi * (Fixed64) rand.Next(0, 360) / (Fixed64) 180;
            Fixed64 roll = Maths.Pi * (Fixed64) rand.Next(0, 360) / (Fixed64) 180;
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
        
        static Fixed64 GetNextRandomFixed64 () {
            Fixed64 randomValue = (Fixed64) rand.NextDouble();
            Fixed64 zero = 0;
            Fixed64 multiplier = 1000;
            randomValue *= multiplier;
            Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;
            if (randomBoolean) randomValue = zero - randomValue;
            return randomValue;
        }

        internal static Matrix44 GetNextRandomMatrix44 () {
            Fixed64 a = GetNextRandomFixed64();
            Fixed64 b = GetNextRandomFixed64();
            Fixed64 c = GetNextRandomFixed64();
            Fixed64 d = GetNextRandomFixed64();
            Fixed64 e = GetNextRandomFixed64();
            Fixed64 f = GetNextRandomFixed64();
            Fixed64 g = GetNextRandomFixed64();
            Fixed64 h = GetNextRandomFixed64();
            Fixed64 i = GetNextRandomFixed64();
            Fixed64 j = GetNextRandomFixed64();
            Fixed64 k = GetNextRandomFixed64();
            Fixed64 l = GetNextRandomFixed64();
            Fixed64 m = GetNextRandomFixed64();
            Fixed64 n = GetNextRandomFixed64();
            Fixed64 o = GetNextRandomFixed64();
            Fixed64 p = GetNextRandomFixed64();
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
