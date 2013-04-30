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

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Sungiant.Abacus.Tests
{
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

			Fixed32 one; RealMaths.One(out one);
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
			Fixed32 zero; RealMaths.Zero(out zero);
			Fixed32 one; RealMaths.One(out one);
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
			Fixed32 pi; RealMaths.Pi(out pi);
			Fixed32 tau; RealMaths.Tau(out tau);

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
			Fixed32 pi; RealMaths.Pi(out pi);
			Fixed32 tau; RealMaths.Tau(out tau);

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

