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
		public static implicit operator Int32 (Fixed32 src)
		{
			return src.ToInt32 ();
		}

		public static explicit operator Single (Fixed32 src)
		{
			return src.ToSingle ();
		}

		public static explicit operator Double (Fixed32 src)
		{
			return src.ToDouble ();
		}

		public static implicit operator Fixed32 (Int32 src)
		{
			return new Fixed32(src);
		}

		public static implicit operator Fixed32(Single src)
		{
			return new Fixed32(src);
		}

		public static implicit operator Fixed32(Double src)
		{
			return new Fixed32(src);
		}

		public static Fixed32 operator * (Fixed32 one, Fixed32 other)
		{
			Fixed32 output;
			Multiply(ref one, ref other, out output);
			return output;
		}
		
		public static Fixed32 operator * (Fixed32 one, Int32 multi)
		{
			return CreateFromRaw (one.numerator * multi);
		}

		public static Fixed32 operator *(Int32 multi, Fixed32 one)
		{
			return CreateFromRaw (one.numerator * multi);
		}

		public static Fixed32 operator / (Fixed32 one, Fixed32 other)
		{
			Fixed32 output;
			Divide(ref one, ref other, out output);
			return output;
		}

		public static Fixed32 operator /(Fixed32 one, Int32 divisor)
		{
			return one / new Fixed32(divisor);
		}

		public static Fixed32 operator /(Int32 divisor, Fixed32 one)
		{
			return new Fixed32 (divisor) / one;
		}

		public static Fixed32 operator % (Fixed32 one, Fixed32 other)
		{
			return CreateFromRaw (one.numerator % other.numerator);
		}

		public static Fixed32 operator %(Fixed32 one, Int32 divisor)
		{
			return one % new Fixed32 (divisor);
		}

		public static Fixed32 operator %(Int32 divisor, Fixed32 one)
		{
			return new Fixed32 (divisor) % one;
		}

		public static Fixed32 operator + (Fixed32 one, Fixed32 other)
		{
			Fixed32 output;
			Add(ref one, ref other, out output);
			return output;
		}

		public static Fixed32 operator +(Fixed32 one, Int32 other)
		{
			return one + new Fixed32 (other);
		}

		public static Fixed32 operator +(Int32 other, Fixed32 one)
		{
			return one + new Fixed32 (other);
		}

		public static Fixed32 operator - (Fixed32 one, Fixed32 other)
		{
			Fixed32 output;
			Subtract(ref one, ref other, out output);
			return output;
		}

		public static Fixed32 operator -(Fixed32 one, Int32 other)
		{
			return one - new Fixed32 (other);
		}

		public static Fixed32 operator -(Int32 other, Fixed32 one)
		{
			return new Fixed32 (other) - one;
		}
		
		public static Fixed32 operator - (Fixed32 f)
		{
			return CreateFromRaw (-f.numerator);
		}

		public static Boolean operator != (Fixed32 one, Fixed32 other)
		{
			return one.numerator != other.numerator;
		}

		public static Boolean operator !=(Fixed32 one, Int32 other)
		{
			return one != new Fixed32 (other);
		}

		public static Boolean operator !=(Int32 other, Fixed32 one)
		{
			return new Fixed32 (other) != one;
		}

		public static Boolean operator >= (Fixed32 one, Fixed32 other)
		{
			return one.numerator >= other.numerator;
		}

		public static Boolean operator >=(Fixed32 one, Int32 other)
		{
			return one >= new Fixed32 (other);
		}

		public static Boolean operator >=(Int32 other, Fixed32 one)
		{
			return new Fixed32 (other) >= one;
		}

		public static Boolean operator <= (Fixed32 one, Fixed32 other)
		{
			return one.numerator <= other.numerator;
		}

		public static Boolean operator <=(Fixed32 one, Int32 other)
		{
			return one <= new Fixed32 (other);
		}

		public static Boolean operator <=(Int32 other, Fixed32 one)
		{
			return new Fixed32 (other) <= one;
		}

		public static Boolean operator > (Fixed32 one, Fixed32 other)
		{
			return one.numerator > other.numerator;
		}

		public static Boolean operator >(Fixed32 one, Int32 other)
		{
			return one > new Fixed32 (other);
		}

		public static Boolean operator >(Int32 other, Fixed32 one)
		{
			return new Fixed32 (other) > one;
		}

		public static Boolean operator < (Fixed32 one, Fixed32 other)
		{
			return one.numerator < other.numerator;
		}

		public static Boolean operator <(Fixed32 one, Int32 other)
		{
			return one < new Fixed32 (other);
		}

		public static Boolean operator <(Int32 other, Fixed32 one)
		{
			return new Fixed32 (other) < one;
		}

		public static Fixed32 operator <<(Fixed32 one, Int32 amount)
		{
			return CreateFromRaw (one.numerator << amount);
		}

		public static Fixed32 operator >>(Fixed32 one, Int32 amount)
		{
			return CreateFromRaw (one.numerator >> amount);
		}

		public static Boolean operator == (Fixed32 one, Fixed32 other)
		{
			return one.numerator == other.numerator;
		}

		public static Boolean operator ==(Fixed32 one, Int32 other)
		{
			return one == new Fixed32 (other);
		}
		
		public static Boolean operator == (Int32 other, Fixed32 one)
		{
			return new Fixed32 (other) == one;
		}
	}
}

