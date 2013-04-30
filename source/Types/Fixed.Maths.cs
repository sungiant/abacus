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

namespace Sungiant.Abacus
{
	public partial struct Fixed32
	{
		static Fixed32 Sqrt (Fixed32 f, Int32 numberOfIterations)
		{
			if (f.numerator < 0) //NaN in Math.Sqrt
			{
				throw new ArithmeticException("Input Error");
			}
			
			if (f.numerator == 0)
			{
				return Zero;
			}

			Fixed32 k = (f + One) >> 1;
			
			for (Int32 i = 0; i < numberOfIterations; i++)
			{
				k = (k + (f / k)) >> 1;
			}
			
			if (k.numerator < 0)
			{
				throw new ArithmeticException("Overflow");
			}
			
			return k;
		}
		
		public static Fixed32 Sqrt (Fixed32 f)
		{
			Int32 numberOfIterations = 8;
			
			if (f.numerator > 0x64000) // 409,600
			{
				numberOfIterations = 12;
			}

			if (f.numerator > 0x3e8000) // 4,096,000
			{
				numberOfIterations = 16;
			}
			
			return Sqrt (f, numberOfIterations);
		}

		public static Fixed32 Square (Fixed32 f)
		{
			int v = f.numerator >> (n / 2);
			int w = f.numerator >> (n - (n / 2));
			return CreateFromRaw (v * w);
		}

		public static Fixed32 Sin (Fixed32 f)
		{
			Fixed32 x_ = f % Fixed32.Tau;

			if (x_ > Fixed32.Pi)
				x_ -= Fixed32.Tau;

			Fixed32 xx = x_ * x_;

			Fixed32 y = 0;
			y -= Fixed32.One / new Fixed32(2 * 3 * 4 * 5 * 6 * 7);
			y *= xx;
			y += Fixed32.One / new Fixed32(2 * 3 * 4 * 5);
			y *= xx;
			y -= Fixed32.One / new Fixed32(2 * 3);
			y *= xx;
			y += Fixed32.One;
			y *= x_;

			return y;
		}
		
		public static Fixed32 Cos (Fixed32 f)
		{
			return Sin (PiOver2 - f);
		}
		
		public static Fixed32 Tan (Fixed32 f)
		{
			return Sin (f) / Cos (f);
		}
		
		public static void Add (ref Fixed32 one, ref Fixed32 other, out Fixed32 ouput)
		{
			ouput.numerator = checked(one.numerator + other.numerator);
		}

		public static void Subtract(ref Fixed32 one, ref Fixed32 other, out Fixed32 ouput)
		{
			ouput.numerator = checked(one.numerator - other.numerator);
		}

		public static void Multiply(ref Fixed32 one, ref Fixed32 other, out Fixed32 output)
		{
			Int64 temp = (Int64)one.numerator * (Int64)other.numerator;

			// rounds: mid values are rounded up
			temp = temp + k;

			// correct by dividing by base
			try
			{
				output.numerator = (Int32)(temp >> n);
			}
			catch (OverflowException)
			{
				if (temp > 0)
					output.numerator = Int32.MaxValue;
				else
					output.numerator = Int32.MinValue;
				
			}
		}

		public static void Divide(ref Fixed32 one, ref Fixed32 other, out Fixed32 output)
		{
			Int64 temp = ((Int64)one.numerator) << n;

			// pre-multiply by the base (Upscale to Q16 so that the result will be in Q8 format)
			temp = temp + (((Int64)other.numerator) >> 1);

			// So the result will be rounded ; mid values are rounded up.
			output.numerator = (Int32)(temp / ((Int64)other.numerator));
		}
		
	}
}