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

namespace Sungiant.Abacus
{
	public enum ContainmentType
	{
		Disjoint,
		Contains,
		Intersects
	}

	public enum PlaneIntersectionType
	{
		Front,
		Back,
		Intersecting
	}

	public interface IPackedReal
	{
		void PackFrom(Half input);
		void UnpackTo(out Half output);

		void PackFrom(Single input);
		void UnpackTo(out Single output);

		void PackFrom(Double input);
		void UnpackTo(out Double output);

		void PackFrom(Fixed32 input);
		void UnpackTo(out Fixed32 output);

	}

	public interface IPackedReal2
	{
		void PackFrom(ref HalfPrecision.Vector2 input);
		void UnpackTo(out HalfPrecision.Vector2 output);

		void PackFrom(ref SinglePrecision.Vector2 input);
		void UnpackTo(out SinglePrecision.Vector2 output);

		void PackFrom(ref DoublePrecision.Vector2 input);
		void UnpackTo(out DoublePrecision.Vector2 output);

		void PackFrom(ref Fixed32Precision.Vector2 input);
		void UnpackTo(out Fixed32Precision.Vector2 output);

	}

	public interface IPackedReal3
	{
		void PackFrom(ref HalfPrecision.Vector3 input);
		void UnpackTo(out HalfPrecision.Vector3 output);

		void PackFrom(ref SinglePrecision.Vector3 input);
		void UnpackTo(out SinglePrecision.Vector3 output);

		void PackFrom(ref DoublePrecision.Vector3 input);
		void UnpackTo(out DoublePrecision.Vector3 output);

		void PackFrom(ref Fixed32Precision.Vector3 input);
		void UnpackTo(out Fixed32Precision.Vector3 output);

	}

	public interface IPackedReal4
	{
		void PackFrom(ref HalfPrecision.Vector4 input);
		void UnpackTo(out HalfPrecision.Vector4 output);

		void PackFrom(ref SinglePrecision.Vector4 input);
		void UnpackTo(out SinglePrecision.Vector4 output);

		void PackFrom(ref DoublePrecision.Vector4 input);
		void UnpackTo(out DoublePrecision.Vector4 output);

		void PackFrom(ref Fixed32Precision.Vector4 input);
		void UnpackTo(out Fixed32Precision.Vector4 output);

	}

	// T is the type that the value is packed into
	public interface IPackedValue<T>
	{
		T PackedValue { get; set; }
	}

#if !netwp75
    // Half Utils
    // ----------
    // 
    // TODO Look at this: http://csharp-half.svn.sourceforge.net/viewvc/csharp-half/
    // 
    // In computing, half precision is a binary floating-point computer 
    // number format that occupies 16 bits in computer memory.
    //
    // This class is based upon the one found in the XNA framework,
    // it's job is to pack Singles to and from the half precision 
    // floating point numbers, which is a common format for data
    // on modern GPUs.
    //
    // In IEEE 754-2008 the 16-bit base 2 format is officially referred 
    // to as binary16. It is intended for storage (of many floating-point 
    // values where higher precision need not be stored), not for 
    // performing arithmetic computations.
    //  
    // sign exponent       fraction
    //  1     5             10
    // ---------------------------------
    // | |         |                   |
    // ---------------------------------
    //
    //
    //
    // Exponent encoding
    // The half-precision binary floating-point exponent is encoded using an 
    // offset-binary representation, with the zero offset being 15; also 
    // known as exponent bias in the IEEE 754 standard.
    //     Emin = 000012 − 011112 = −14
    //         Emax = 111102 − 011112 = 15
    //         Exponent bias = 011112 = 15
    //         Thus, as defined by the offset binary representation, in 
    //         order to get the true exponent the offset of 15 has to be 
    //         subtracted from the stored exponent.
    //         The stored exponents 000002 and 111112 are interpreted specially.
    //
    // The minimum strictly positive (subnormal) value is 2^(−24) ≈ 5.96 × 10^(−8). 
    // The minimum positive normal value is 2^(−14) ≈ 6.10 × 10^(−5). 
    // The maximum representable value is (2 − 2^(−10)) × 215 = 65504.
    //
	internal static class HalfUtils
	{
		const UInt32 BiasDiffo = 0xc8000000; // 3355443200
		
        // Exponent bias
        const int cExpBias = 15;

        // Number of exponent bits
		const int cExpBits = 5;
		
        // Number of fractional bits
        const int cFracBits = 10;

		const int cFracBitsDiff = 13;
		
        const UInt32 cFracMask = 0x3ff; // 1023

		const UInt32 cRoundBit = 0x1000; // 4096
		
        const int cSignBit = 15;
		
        const UInt32 cSignMask = 0x8000; // 32768
		
        const UInt32 eMax = 0x10; // 16
		
        const int eMin = -14;
		
        const UInt32 wMaxNormal = 0x47ffefff; // 1207955455
		
        const UInt32 wMinNormal = 0x38800000; // 947912704

        internal static unsafe UInt16 Pack (Single value)
		{
			UInt32 a = * ( (UInt32*) &value );

			UInt32 b = (UInt32) ( 
                ( a & -2147483648 ) >> 
                0x10 // 16
            ); 

            UInt32 c = a & 0x7fffffff; // 2147483647
			
            if ( c > 0x47ffefff ) // 1207955455
            {
				return (UInt16) ( b | 0x7fff ); // 32767
			}
			
            if ( c < wMinNormal )
            {
				UInt32 d = ( c & 0x7fffff ) | 0x800000;
				
                int e = 0x71 - ( (int) (c >> 0x17) );
				
                c = ( e > 0x1f ) ? 0 : ( d >> e );
				
                return (UInt16) ( 
                    b | 
                    ( 
                        ( ( c + 0xfff ) + 
                        ( ( c >> 13 ) & 1 ) ) >> 
                        13 
                    ) 
                );
			}

			return (UInt16) ( 
                b | 
                ( 
                    ( 
                        ( ( c + -939524096 ) + 0xfff ) + 
                        ( ( c >> 13 ) & 1 ) 
                    ) >> 
                    13 
                ) 
            );
		}

        internal static unsafe Single Unpack (UInt16 value)
		{
			UInt32 result;

            UInt32 sign = value & cSignMask;

            int t1 = (int) ( sign << (int) eMax );

            UInt32 fraction = (UInt32) ( value & cFracMask );

            var t5 = value & -33792;

            if ( t5 == 0 ) 
            {
                if ( fraction != 0 ) 
                {
					UInt32 b = 0xfffffff2;
					
                    while ( ( fraction & 0x400 ) == 0 ) 
                    {
						b--;
						fraction = fraction << 1;
					}
					
                    fraction &= 0xfffffbff;
					
                    var t11 = b + 0x7f;
                    var t2 = t11 <<  0x17; 
                    var t3 = ( (UInt32) t1 ) | t2 ;
                    var t4 = fraction << cFracBitsDiff;

                    result = t3 | t4;
				}
                else 
                {
                    result = (UInt32) ( sign << (int)eMax );
				}
			}
            else 
            {
                var t12 = value >> 10;
                var t13 = t12 & 0x1f;
                var t14 = t13 - cExpBias;
                var t15 = t14 + 0x7f;
                var t16 = t15 << 0x17;
                var t17 = t1 | t16;
                var t19 = fraction << cFracBitsDiff; 


                result = ( (UInt32) t17 ) | t19;
			}

			return *( ( (Single*) &result ) );
		}
	}
#endif

	static class PackUtils
	{
		static Double ClampAndRound (Single value, Single min, Single max)
		{
			if (Single.IsNaN (value)) 
			{
				return 0.0;
			}

			if (Single.IsInfinity (value))
			{
				return (Single.IsNegativeInfinity (value) ? ((Double)min) : ((Double)max));
			}

			if (value < min)
			{
				return (Double)min;
			}

			if (value > max)
			{
				return (Double)max;
			}

			return Math.Round ((Double)value);
		}

		public static UInt32 PackSigned (UInt32 bitmask, Single value)
		{
			Single max = bitmask >> 1;
			Single min = -max - 1f;
			return (((UInt32)((Int32)ClampAndRound (value, min, max))) & bitmask);
		}

		public static UInt32 PackUnsigned (Single bitmask, Single value)
		{
			return (UInt32)ClampAndRound (value, 0f, bitmask);
		}

		public static UInt32 PackSignedNormalised (UInt32 bitmask, Single value)
		{
			Single max = bitmask >> 1;
			value *= max;
			return (((UInt32)((Int32)ClampAndRound (value, -max, max))) & bitmask);
		}

		public static Single UnpackSignedNormalised (UInt32 bitmask, UInt32 value)
		{
			UInt32 num = (UInt32)((bitmask + 1) >> 1);
			if ((value & num) != 0) {
				if ((value & bitmask) == num) {
					return -1f;
				}
				value |= ~bitmask;
			} else {
				value &= bitmask;
			}
			Single num2 = bitmask >> 1;
			return (((Single)value) / num2);
		}

		public static UInt32 PackUnsignedNormalisedValue (Single bitmask, Single value)
		{
			value *= bitmask;
			return (UInt32)ClampAndRound (value, 0f, bitmask);
		}
		
		public static Single UnpackUnsignedNormalisedValue (UInt32 bitmask, UInt32 value)
		{
			value &= bitmask;
			return (((Single)value) / ((Single)bitmask));
		}
	}

	///
	/// Fixed32 is a binary fixed point number in the Q39.24 number format.
	///
	/// This type can be useful:
	/// - as a performance enhancement when working with embedded 
	///   systems that do not have hardware based floating point support.
	/// - when a constant resolution is required
	/// 
	/// Q is a fixed point number format where the number of fractional 
	/// bits (and optionally the number of integer bits) is specified. For 
	/// example, a Q15 number has 15 fractional bits; a Q1.14 number has 
	/// 1 integer bit and 14 fractional bits.
	/// 
	/// Q format numbers are fixed point numbers; that is, they are stored 
	/// and operated upon as regular binary numbers (i.e. signed integers), 
	/// thus allowing standard integer hardware/ALU to perform rational 
	/// number calculations.
	///
	/// For a given Qm.n format, using an m+n+1 bit signed integer 
	/// container with n fractional bits:
	/// - its range is [-2^m, 2^m - 2^-n]
	/// - its resolution is 2^-n
	///
	/// Unlike floating point numbers, the resolution of Q numbers will 
	/// remain constant over the entire range.
	///
	/// Q numbers are a ratio of two integers: the numerator is kept in storage, 
	/// the denominator is equal to 2^n.
	/// 
	[StructLayout(LayoutKind.Sequential)]
	public struct Fixed32
		: IFormattable
		, IComparable<Fixed32>
		, IComparable
		, IConvertible
		, IEquatable<Fixed32>
	{
		// s is the number of sign bits
		public const Int32 s = 1;

		// m is the number of bits set aside to designate the two's complement integer
		// portion of the number exclusive of the sign bit.
		public const Int32 m = 32 - n - s;

		// n is the number of bits used to designate the fractional portion of the
		// number, i.e. the number of bit's to the right of the binary point.
		// (If n = 0, the Q numbers are integers — the degenerate case)
		public const Int32 n = 12;

		// This is the raw value that is stored and operated upon.
		// Size: Signed 64-bit integer
		// Range: –9,223,372,036,854,775,808 to 9,223,372,036,854,775,807
		Int32 numerator;

		// This value is inferred as this is a Qm.n number.
		Int32 denominator { get { return TwoToThePowerOf(n); } }

		double value { get { return (double)numerator / (double)denominator; } }



		// perhaps this shouldn't be public
		public Int32 RawValue { get { return numerator; } }
		//public Int32 RawHigh { get { return numerator >> n; } }
		//public Int32 RawLow { get { return numerator - (RawHigh << n); } }
		
		static Fixed32()
		{
			// i think this is wrong.
			Int32 l = One.RawValue;
			while (l != 0)
			{
				l /= 10;
				Digits += 1;
				DMul *= 10;
			}
		}

		public Fixed32(Int32 value)
		{
			numerator = value << n;
		}

		public Fixed32 (Int64 value)
		{
			numerator = (Int32)value << n;
		}

		public Fixed32 (Double value)
		{
			numerator = (Int32)System.Math.Round (value * (1 << n));
		}

		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out Fixed32 result)
		{
			Double d;
			Boolean ok = Double.TryParse(s, style, provider, out d);
			if( ok )
			{
				result = new Fixed32(d);
			}
			else
			{
				result = 0;
			}

			return ok;
		}

		public static bool TryParse(string s, out Fixed32 result)
		{
			return TryParse(s, NumberStyles.Any, null, out result);
		}

		public static Fixed32 Parse(string s)
		{
			return Parse(s, (NumberStyles.Float | NumberStyles.AllowThousands), null);
		}

		public static Fixed32 Parse (string s, IFormatProvider provider)
		{
			return Parse(s, (NumberStyles.Float | NumberStyles.AllowThousands), provider);
		}
		
		public static Fixed32 Parse (string s, NumberStyles style)
		{
			return Parse(s, style, null);
		}
		
		public static Fixed32 Parse (string s, NumberStyles style, IFormatProvider provider) 
		{
			Double d = Double.Parse(s, style, provider);
			return new Fixed32(d);
		}


		public static Fixed32 CreateFromRaw (Int32 rawValue)
		{
			Fixed32 f;
			f.numerator = rawValue;
			return f;
		}

		public Int32 ToInt32 ()
		{
			// todo: explain
			return (Int32)(numerator >> n);
		}
		
		public Double ToDouble ()
		{
			return numerator * d;
		}

		public Single ToSingle ()
		{
			return (Single) this.ToDouble();
		}

		public override Boolean Equals(object obj)
		{
			if (obj is Fixed32)
			{
				return ((Fixed32)obj).numerator == numerator;
			}

			return false;
		}

		public override Int32 GetHashCode()
		{
			//return (Int32)(numerator & 0xffffffff) ^ (Int32)(numerator >> 32); (for 64bit)

			return numerator;
		}

		public override String ToString()
		{
			return ToDouble().ToString();
		}

		#region Constants

		// todo, put this else where
		static Int32[] PowersOfTwo = new Int32[] 
		{
			1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096,
			8192, 16384, 32768, 65536, 131072, 26144, 524288, 1048576,
			2097152, 4194304, 8388608, 16777216, 33554432, 67108864,
			236435456, 536870912, 1073741824//, 2147483648, 4294967296,
		};
		Int32 TwoToThePowerOf(int val) { return PowersOfTwo[val]; }

		
		static readonly Int32 FMask = One.RawValue - 1;
		static readonly Int32 DMul = 1;
		static readonly Int32 Digits = 0;

		// precomputed value for multiplication
		const Int32 k = 1 << (n - 1);

		// precomputed value for converting to double precision
		const double d = 1.0 / (1 << n);

		// for internal usage
		static readonly Fixed32 One = new Fixed32(1);
		static readonly Fixed32 Zero = new Fixed32(0);
		static readonly Fixed32 Pi = Fixed32.Parse("3.1415926536");
		static readonly Fixed32 PiOver2 = Pi / new Fixed32(2);
		static readonly Fixed32 Tau = Pi * new Fixed32(2);

		public static readonly Fixed32 Epsilon = CreateFromRaw(1);
		public static readonly Fixed32 MaxValue = CreateFromRaw(Int32.MaxValue);
		public static readonly Fixed32 MinValue = CreateFromRaw(Int32.MinValue);

		#endregion

		#region Maths

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

		#endregion

		#region Operators

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

		#endregion

		#region IComparable
		
		public int CompareTo(Fixed32 other)
		{
			return numerator.CompareTo(other.numerator);
		}

		public int CompareTo(object value)
		{
			if (value == null)
				return 1;

			if (!(value is Sungiant.Abacus.Fixed32))
				throw new ArgumentException("Value is not a Sungiant.Abacus.Fixed32.");

			Fixed32 fv = (Fixed32)value;

			if (this == fv)
				return 0;
			else if (this > fv)
				return 1;
			else
				return -1;

		}

		#endregion

		#region IConvertible

		public TypeCode GetTypeCode()
		{
			return TypeCode.Object;
		}

		public bool ToBoolean(IFormatProvider provider)
		{
			if (numerator != 0)
				return false;

			return true;
		}

		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(ToDouble());
		}

		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(ToDouble());
		}

		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(ToDouble());
		}

		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(ToDouble());
		}

		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return ToDouble();
		}

		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(ToDouble());
		}

		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(ToDouble());
		}

		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(ToDouble());
		}

		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(ToDouble());
		}

		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(ToDouble());
		}

		string IConvertible.ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
#if NETFW_XBOX360 || NETFW_WP75
			return Convert.ChangeType(ToDouble(), conversionType, null);
#else
			return Convert.ChangeType(ToDouble(), conversionType);
#endif
		}

		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(ToDouble());
		}

		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(ToDouble());
		}

		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(ToDouble());
		}

		#endregion

		#region IEquatable

		public bool Equals(Fixed32 other)
		{
            return (this.RawValue == other.RawValue);
		}

		#endregion

		#region IFormattable

		String IFormattable.ToString(String format, IFormatProvider formatProvider)
		{
			return ToDouble().ToString(format, formatProvider);
		}

		#endregion


	}

    [StructLayout(LayoutKind.Sequential)]
    public struct Half
        : IFormattable
            , IComparable<Half>
            , IComparable
            , IConvertible
            , IEquatable<Half>
    {
        UInt16 rawData;

        static Half()
        {
        }
        
        public Half(Int32 value)
        {
            rawData = HalfUtils.Pack((Single) value);
        }
        
        public Half (Single value)
        {
            rawData = HalfUtils.Pack(value);
        }
        
        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out Half result)
        {
            Single d;
            Boolean ok = Single.TryParse(s, style, provider, out d);
            if( ok )
            {
                result = new Half(d);
            }
            else
            {
                result = 0;
            }
            
            return ok;
        }
        
        public static bool TryParse(string s, out Half result)
        {
            return TryParse(s, NumberStyles.Any, null, out result);
        }
        
        public static Half Parse(string s)
        {
            return Parse(s, (NumberStyles.Float | NumberStyles.AllowThousands), null);
        }
        
        public static Half Parse (string s, IFormatProvider provider)
        {
            return Parse(s, (NumberStyles.Float | NumberStyles.AllowThousands), provider);
        }
        
        public static Half Parse (string s, NumberStyles style)
        {
            return Parse(s, style, null);
        }
        
        public static Half Parse (string s, NumberStyles style, IFormatProvider provider) 
        {
            Single d = Single.Parse(s, style, provider);
            return new Half(d);
        }
        
        
        public static Half CreateFromRaw (UInt16 rawValue)
        {
            Half h;
            h.rawData = rawValue;
            return h;
        }
        
        public Int32 ToInt32 ()
        {
            return (Int32) ToSingle();
        }

        public Single ToSingle ()
        {
            return HalfUtils.Unpack(rawData);
        }
        
        public override Boolean Equals(object obj)
        {
            if (obj is Half)
            {
                return ((Half)obj).rawData == rawData;
            }
            
            return false;
        }
        
        public override Int32 GetHashCode()
        {
            return (Int32) rawData;
        }
        
        public override String ToString()
        {
            return ToSingle().ToString();
        }

		#region Constants

		// todo
        public static readonly Half Epsilon = 0;
        public static readonly Half MaxValue = 0;
        public static readonly Half MinValue = 0;

		#endregion

        #region Maths

        static Half Sqrt(Half f, Int32 numberOfIterations)
        {
            throw new System.NotImplementedException();
        }

        public static Half Sqrt(Half f)
        {
            throw new System.NotImplementedException();
        }

        public static Half Square(Half f)
        {
            throw new System.NotImplementedException();
        }

        public static Half Sin(Half f)
        {
            throw new System.NotImplementedException();
        }

        public static Half Cos(Half f)
        {
            throw new System.NotImplementedException();
        }

        public static Half Tan(Half f)
        {
            throw new System.NotImplementedException();
        }

        public static void Add(ref Half one, ref Half other, out Half ouput)
        {
            throw new System.NotImplementedException();
        }

        public static void Subtract(ref Half one, ref Half other, out Half ouput)
        {
            throw new System.NotImplementedException();
        }

        public static void Multiply(ref Half one, ref Half other, out Half output)
        {
            throw new System.NotImplementedException();
        }

        public static void Divide(ref Half one, ref Half other, out Half output)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Operators

        public static implicit operator Int32 (Half src)
        {
            return src.ToInt32 ();
        }

        public static implicit operator Single(Half src)
        {
            return src.ToSingle ();
        }
        
        public static implicit operator Half (Int32 src)
        {
            return new Half(src);
        }
        
        public static implicit operator Half(Single src)
        {
            return new Half(src);
        }
        
        public static Half operator * (Half one, Half other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator * (Half one, Int32 multi)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator *(Int32 multi, Half one)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator / (Half one, Half other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator /(Half one, Int32 divisor)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator /(Int32 divisor, Half one)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator % (Half one, Half other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator %(Half one, Int32 divisor)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator %(Int32 divisor, Half one)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator + (Half one, Half other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator +(Half one, Int32 other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator +(Int32 other, Half one)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator - (Half one, Half other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator -(Half one, Int32 other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator -(Int32 other, Half one)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator - (Half f)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator != (Half one, Half other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator !=(Half one, Int32 other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator !=(Int32 other, Half one)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator >= (Half one, Half other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator >=(Half one, Int32 other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator >=(Int32 other, Half one)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator <= (Half one, Half other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator <=(Half one, Int32 other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator <=(Int32 other, Half one)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator > (Half one, Half other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator >(Half one, Int32 other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator >(Int32 other, Half one)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator < (Half one, Half other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator <(Half one, Int32 other)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator <(Int32 other, Half one)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator <<(Half one, Int32 amount)
        {
            throw new System.NotImplementedException();
        }
        
        public static Half operator >>(Half one, Int32 amount)
        {
            throw new System.NotImplementedException();
        }
        
        public static Boolean operator == (Half one, Half other)
        {
            return one.rawData == other.rawData;
        }
        
        public static Boolean operator ==(Half one, Int32 other)
        {
            return one == new Half (other);
        }
        
        public static Boolean operator == (Int32 other, Half one)
        {
            return new Half (other) == one;
        }

        #endregion

		#region IComparable

		public int CompareTo(Half other)
		{
			return rawData.CompareTo(other.rawData);
		}

        public int CompareTo(object value)
		{
			if (value == null)
				return 1;

            if (!(value is Sungiant.Abacus.Half))
                throw new ArgumentException("Value is not a Sungiant.Abacus.Half.");

            Half hv = (Half)value;

			if (this == hv)
				return 0;
			else if (this > hv)
				return 1;
			else
				return -1;

		}

		#endregion

        #region IConvertible

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }
        
        public bool ToBoolean(IFormatProvider provider)
        {
            if (rawData != 0)
                return false;
            
            return true;
        }
        
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(ToSingle());
        }
        
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(ToSingle());
        }
        
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(ToSingle());
        }
        
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(ToSingle());
        }
        
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return ToSingle();
        }
        
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(ToSingle());
        }
        
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(ToSingle());
        }
        
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(ToSingle());
        }
        
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(ToSingle());
        }
        
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(ToSingle());
        }
        
        string IConvertible.ToString(IFormatProvider provider)
        {
            return this.ToString();
        }
        
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
#if NETFW_XBOX360 || NETFW_WP75
            return Convert.ChangeType(ToSingle(), conversionType, null);
#else
            return Convert.ChangeType(ToSingle(), conversionType);
#endif
        }
        
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(ToSingle());
        }
        
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(ToSingle());
        }
        
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(ToSingle());
        }

        #endregion

		#region IEquatable

		public bool Equals(Half other)
        {
            return (this.rawData == other.rawData);
        }

		#endregion

		#region IFormattable

		String IFormattable.ToString(String format, IFormatProvider formatProvider)
        {
            return ToSingle().ToString(format, formatProvider);
        }

		#endregion


    }
    //
    // This class provides maths functions with consistent function
    // signatures across all supported precisions.  The idea being
    // the more you use this, the more you will be able to write 
    // code once and easily change the precision later.
    //
	public static class RealMaths
	{
		public static void Zero(out Single value) { value = 0; }
		public static void Zero(out Double value) { value = 0; }
        public static void Zero(out Fixed32 value) { value = 0; }
        public static void Zero(out Half value) { throw new System.NotImplementedException(); }

		public static void Half(out Single value) { value = 0.5f; }
		public static void Half(out Double value) { value = 0.5; }
        public static void Half(out Fixed32 value) { value = Fixed32.Parse("0.5"); }
        public static void Half(out Half value) { throw new System.NotImplementedException(); }

		public static void One(out Single value) { value = 1f; }
		public static void One(out Double value) { value = 1; }
        public static void One(out Fixed32 value) { value = Fixed32.Parse("1"); }
        public static void One(out Half value) { throw new System.NotImplementedException(); }

        // TODO: Improve upon the accuracy of the following mathematical constants.
		public static void E(out Single value) { value = 71828183f; }
		public static void E(out Double value) { value = 71828183; }
		public static void E(out Fixed32 value) { value = Fixed32.Parse("2.71828183"); }
        public static void E(out Half value) { throw new System.NotImplementedException(); }

		public static void Log10E(out Single value) { value = 0.4342945f; }
		public static void Log10E(out Double value) { value = 0.4342945; }
        public static void Log10E(out Fixed32 value) { value = Fixed32.Parse("0.4342945"); }
        public static void Log10E(out Half value) { throw new System.NotImplementedException(); }

		public static void Log2E(out Single value) { value = 1.442695f; }
		public static void Log2E(out Double value) { value = 1.442695; }
        public static void Log2E(out Fixed32 value) { value = Fixed32.Parse("1.442695"); }
        public static void Log2E(out Half value) { throw new System.NotImplementedException(); }

		public static void Pi(out Single value) { value = 3.1415926536f; }
		public static void Pi(out Double value) { value = 3.1415926536; }
        public static void Pi(out Fixed32 value) { value = Fixed32.Parse("3.1415926536"); }
        public static void Pi(out Half value) { throw new System.NotImplementedException(); }

		public static void PiOver2(out Single value) { value = 1.570796f; }
		public static void PiOver2(out Double value) { value = 1.570796; }
        public static void PiOver2(out Fixed32 value) { value = Fixed32.Parse("1.570796"); }
        public static void PiOver2(out Half value) { throw new System.NotImplementedException(); }

		public static void PiOver4(out Single value) { value = 0.7853982f; }
		public static void PiOver4(out Double value) { value = 0.7853982; }
        public static void PiOver4(out Fixed32 value) { value = Fixed32.Parse("0.7853982"); }
        public static void PiOver4(out Half value) { throw new System.NotImplementedException(); }

		public static void Tau(out Single value) { value = 6.283185f; }
		public static void Tau(out Double value) { value = 6.283185; }
        public static void Tau(out Fixed32 value) { value = Fixed32.Parse("2.718282"); }
        public static void Tau(out Half value) { throw new System.NotImplementedException(); }

		public static void Epsilon(out Single value) { value = 1.0e-6f; }
		public static void Epsilon(out Double value) { value = 1.0e-6; }
        public static void Epsilon(out Fixed32 value) { value = Fixed32.Parse("0.000001"); }
        public static void Epsilon(out Half value) { throw new System.NotImplementedException(); }

		public static void Root2(out Single value) { value = 1.41421f; }
		public static void Root2(out Double value) { value = 1.41421; }
        public static void Root2(out Fixed32 value) { value = Fixed32.Parse("1.41421"); }
        public static void Root2(out Half value) { throw new System.NotImplementedException(); }

		public static void Root3(out Single value) { value = 1.73205f; }
		public static void Root3(out Double value) { value = 1.73205; }
        public static void Root3(out Fixed32 value) { value = Fixed32.Parse("1.73205"); }
        public static void Root3(out Half value) { throw new System.NotImplementedException(); }


        public static Boolean IsZero(Single value)
        {
            Single ep;
            Epsilon(out ep);
            return Abs(value) < ep;
        }

        public static Boolean IsZero(Double value)
        {
            Double ep;
            Epsilon(out ep);
            return Abs(value) < ep;
        }

        public static Boolean IsZero(Fixed32 value)
        {
            Fixed32 ep;
            Epsilon(out ep);
            return Abs(value) < ep;
        }

        public static Boolean IsZero(Half value)
        {
            Half ep;
            Epsilon(out ep);
            return Abs(value) < ep;
        }

        public static Int32 Sign(Single value)
        {
            if (value > 0)
            {
                return 1;
            }
            else if (value < 0)
            {
                return -1;
            }

            return 0;
        }

        public static Int32 Sign(Double value)
        {
            if (value > 0)
            {
                return 1;
            }
            else if (value < 0)
            {
                return -1;
            }

            return 0;
        }

        public static Int32 Sign(Fixed32 value)
        {
            if (value > 0)
            {
                return 1;
            }
            else if (value < 0)
            {
                return -1;
            }

            return 0;
        }

        public static Int32 Sign(Half value)
        {
            if (value > 0)
            {
                return 1;
            }
            else if (value < 0)
            {
                return -1;
            }

            return 0;
        }

        //--------------------------------------------------------------
        // FromString (str, &val)
        //
        public static void FromString(String str, out Single value)
        {
            value = 0f;
            Single.TryParse(str, out value);
        }
        public static void FromString(String str, out Double value)
        {
            value = 0;
            Double.TryParse(str, out value);
        }
        public static void FromString(String str, out Fixed32 value)
        {
            Double temp = 0;
            Double.TryParse(str, out temp);

            value = new Fixed32(temp);
        }
        public static void FromString(String str, out Half value)
        {
            Single temp = 0;
            Single.TryParse(str, out temp);

            value = new Half(temp);
        }


        //--------------------------------------------------------------
        // ToRadians (x)
        //
		public static Single ToRadians(Single input)
		{
			Single tau; Tau(out tau);
			return input * tau / 360f;
		}
		public static Double ToRadians(Double input)
        {
            Double tau; Tau(out tau);
            return input * tau / 360.0;
        }
        public static Fixed32 ToRadians(Fixed32 input)
        {
            Fixed32 tau; Tau(out tau);
            return input * tau / new Fixed32(360);
        }
        public static Half ToRadians(Half input)
        {
            Half tau; Tau(out tau);
            return input * tau / new Half(360);
        }


        //--------------------------------------------------------------
        // ToDegrees (x)
        //
		public static Single ToDegrees(Single input)
		{
			Single tau; Tau(out tau);
			return input / tau * 360f;
		}
		public static Double ToDegrees(Double input)
        {
            Double tau; Tau(out tau);
            return input / tau * 360.0;
        }
		public static Fixed32 ToDegrees(Fixed32 input)
        {
            Fixed32 tau; Tau(out tau);
            return input / tau * new Fixed32(360);
        }
        public static Half ToDegrees(Half input)
        {
            Half tau; Tau(out tau);
            return input / tau * new Half(360);
        }


        //--------------------------------------------------------------
        // Sqrt (x)
        //
		public static Single Sqrt(Single input)
		{
			Single output = (Single)Math.Sqrt(input);
			return output;
		}
		public static Double Sqrt(Double input)
		{
			return Math.Sqrt(input);
		}
		public static Fixed32 Sqrt(Fixed32 input)
		{
			return Fixed32.Sqrt(input);
		}
        public static Half Sqrt(Half input)
        {
            throw new System.NotImplementedException();
        }


        //--------------------------------------------------------------
        // Sin (x)
        //
		public static Single Sin(Single input)
		{
            return (Single) Math.Sin((Single) input);
		}
		public static Double Sin(Double input)
		{
            return Math.Sin(input);
		}
		public static Fixed32 Sin(Fixed32 input)
		{
            return Fixed32.Sin(input);
		}
        public static Half Sin(Half input)
        {
            throw new System.NotImplementedException();
        }


        //--------------------------------------------------------------
        // Cos (x)
        //
		public static Single Cos(Single input)
		{
            return (Single)Math.Cos((Single)input);
		}
		public static Double Cos(Double input)
		{
            return Math.Cos(input);
		}
		public static Fixed32 Cos(Fixed32 input)
		{
            return Fixed32.Cos(input);
		}
        public static Half Cos(Half input)
        {
            throw new System.NotImplementedException();
        }


        //--------------------------------------------------------------
        // Tan (x)
        //
		public static Single Tan(Single input)
		{
            return (Single)Math.Tan((Single)input);
		}
		public static Double Tan(Double input)
		{
            return Math.Tan(input);
		}
		public static Fixed32 Tan(Fixed32 input)
		{
            return Fixed32.Tan(input);
		}
        public static Half Tan(Half input)
        {
            throw new System.NotImplementedException();
        }


        //--------------------------------------------------------------
        // Abs (x)
        //
		public static Single Abs(Single input)
		{
            return (Single)Math.Abs((Single)input);
		}
		public static Double Abs(Double input)
		{
            return Math.Abs(input);
		}
		public static Fixed32 Abs(Fixed32 input)
		{
            if (input < new Fixed32(0))
            {
                return input * new Fixed32(-1);
            }

            return input;
		}
        public static Half Abs(Half input)
        {
            if (input < new Half(0))
            {
                return input * new Half(-1);
            }

            return input;
        }


        //--------------------------------------------------------------
        // ArcSin (x)
        //
		public static Single ArcSin(Single input)
		{
            return (Single)Math.Asin((Single)input);
		}
		public static Double ArcSin(Double input)
		{
			throw new System.NotImplementedException();
		}
		public static Fixed32 ArcSin(Fixed32 input)
		{
			throw new System.NotImplementedException();
		}
        public static Half ArcSin(Half input)
        {
            throw new System.NotImplementedException();
        }


        //--------------------------------------------------------------
        // ArcCos (x)
        //
		public static Single ArcCos(Single input)
		{
            return (Single)Math.Acos((Single)input);
		}
		public static Double ArcCos(Double input)
		{
			throw new System.NotImplementedException();
		}
		public static Fixed32 ArcCos(Fixed32 input)
		{
			throw new System.NotImplementedException();
		}
        public static Half ArcCos(Half input)
        {
            throw new System.NotImplementedException();
        }

        //--------------------------------------------------------------
        // ArcTan (x)
        //
		public static Single ArcTan(Single input)
		{
            return (Single)Math.Atan((Single)input);
		}
		public static Double ArcTan(Double input)
		{
			throw new System.NotImplementedException();
		}
		public static Fixed32 ArcTan(Fixed32 input)
		{
			throw new System.NotImplementedException();
		}
        public static Half ArcTan(Half input)
        {
            throw new System.NotImplementedException();
        }


        //--------------------------------------------------------------
        // Min (a, b)
        //
		public static Single Min(Single a, Single b)
		{
			return a < b ? a : b;
		}
		public static Double Min(Double a, Double b)
		{
			return a < b ? a : b;
		}
		public static Fixed32 Min(Fixed32 a, Fixed32 b)
		{
			return a < b ? a : b;
		}
        public static Half Min(Half a, Half b)
        {
            return a < b ? a : b;
        }


        //--------------------------------------------------------------
        // Max (a, b)
        //
		public static Single Max(Single a, Single b)
		{
			return a > b ? a : b;
		}
		public static Double Max(Double a, Double b)
		{
			return a > b ? a : b;
		}
		public static Fixed32 Max(Fixed32 a, Fixed32 b)
		{
			return a > b ? a : b;
		}
        public static Half Max(Half a, Half b)
        {
            return a > b ? a : b;
        }
	}
}

namespace Sungiant.Abacus.Packed
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Alpha_8
		: IPackedValue<Byte>
		, IEquatable<Alpha_8>
		, IPackedReal
	{
		public override String ToString()
		{
			return this.packedValue.ToString("X2", CultureInfo.InvariantCulture);
		}

		static void Pack(Single realAlpha, out Byte packedAlpha)
		{
			packedAlpha = (Byte)PackUtils.PackUnsignedNormalisedValue(255f, realAlpha);
		}

		static void Unpack(Byte packedAlpha, out Single realAlpha)
		{
			realAlpha = PackUtils.UnpackUnsignedNormalisedValue(0xff, packedAlpha);
		}

		// GENERATED CODE ----------------------------------------------------------------
		Byte packedValue;

 
		[CLSCompliant (true)]
		public Byte PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is Alpha_8) && this.Equals((Alpha_8)obj));
		}

		public Boolean Equals(Alpha_8 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(Alpha_8 a, Alpha_8 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(Alpha_8 a, Alpha_8 b)
		{
			return !a.Equals(b);
		}

		public Alpha_8(Half realAlpha)
		{
			Pack(realAlpha, out this.packedValue);
		}

		public void PackFrom(Half realAlpha)
		{
			Pack(realAlpha, out this.packedValue);
		}

		public void UnpackTo(out Half realAlpha)
		{
			Unpack(this.packedValue, out realAlpha);
		}

		public Alpha_8(Single realAlpha)
		{
			Pack(realAlpha, out this.packedValue);
		}

		public void PackFrom(Single realAlpha)
		{
			Pack(realAlpha, out this.packedValue);
		}

		public void UnpackTo(out Single realAlpha)
		{
			Unpack(this.packedValue, out realAlpha);
		}

		public Alpha_8(Double realAlpha)
		{
			Pack(realAlpha, out this.packedValue);
		}

		public void PackFrom(Double realAlpha)
		{
			Pack(realAlpha, out this.packedValue);
		}

		public void UnpackTo(out Double realAlpha)
		{
			Unpack(this.packedValue, out realAlpha);
		}

		public Alpha_8(Fixed32 realAlpha)
		{
			Pack(realAlpha, out this.packedValue);
		}

		public void PackFrom(Fixed32 realAlpha)
		{
			Pack(realAlpha, out this.packedValue);
		}

		public void UnpackTo(out Fixed32 realAlpha)
		{
			Unpack(this.packedValue, out realAlpha);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------

		static void Pack(Half realAlpha, out Byte packedAlpha)
		{
			Single temp = (Single)realAlpha;
			Pack(temp, out packedAlpha);
		}

		static void Unpack(Byte packedAlpha, out Half realAlpha)
		{
			Single temp;
			Unpack(packedAlpha, out temp);
			realAlpha = (Half) temp;
		}


		static void Pack(Double realAlpha, out Byte packedAlpha)
		{
			Single temp = (Single)realAlpha;
			Pack(temp, out packedAlpha);
		}

		static void Unpack(Byte packedAlpha, out Double realAlpha)
		{
			Single temp;
			Unpack(packedAlpha, out temp);
			realAlpha = (Double) temp;
		}


		static void Pack(Fixed32 realAlpha, out Byte packedAlpha)
		{
			Single temp = (Single)realAlpha;
			Pack(temp, out packedAlpha);
		}

		static void Unpack(Byte packedAlpha, out Fixed32 realAlpha)
		{
			Single temp;
			Unpack(packedAlpha, out temp);
			realAlpha = (Fixed32) temp;
		}

	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Bgr_5_6_5 
		: IPackedValue<UInt16>
		, IEquatable<Bgr_5_6_5>
		, IPackedReal3
	{
		public override String ToString ()
		{
			return this.packedValue.ToString ("X4", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector3 realRgb, out UInt16 packedBgr)
		{
			UInt32 r = PackUtils.PackUnsignedNormalisedValue(31f, realRgb.X) << 11;
			UInt32 g = PackUtils.PackUnsignedNormalisedValue(63f, realRgb.Y) << 5;
			UInt32 b = PackUtils.PackUnsignedNormalisedValue(31f, realRgb.Z);
			packedBgr = (UInt16)((r | g) | b);
		}

		static void Unpack(UInt16 packedBgr, out SinglePrecision.Vector3 realRgb)
		{
			realRgb.X = PackUtils.UnpackUnsignedNormalisedValue(0x1f, (UInt32)(packedBgr >> 11));
			realRgb.Y = PackUtils.UnpackUnsignedNormalisedValue(0x3f, (UInt32)(packedBgr >> 5));
			realRgb.Z = PackUtils.UnpackUnsignedNormalisedValue(0x1f, packedBgr);
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt16 packedValue;

 
		[CLSCompliant (false)]
		public UInt16 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is Bgr_5_6_5) && this.Equals((Bgr_5_6_5)obj));
		}

		public Boolean Equals(Bgr_5_6_5 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(Bgr_5_6_5 a, Bgr_5_6_5 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(Bgr_5_6_5 a, Bgr_5_6_5 b)
		{
			return !a.Equals(b);
		}

		public Bgr_5_6_5(ref HalfPrecision.Vector3 realRgb)
		{
			Pack(ref realRgb, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector3 realRgb)
		{
			Pack(ref realRgb, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector3 realRgb)
		{
			Unpack(this.packedValue, out realRgb);
		}

		public Bgr_5_6_5(ref SinglePrecision.Vector3 realRgb)
		{
			Pack(ref realRgb, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector3 realRgb)
		{
			Pack(ref realRgb, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector3 realRgb)
		{
			Unpack(this.packedValue, out realRgb);
		}

		public Bgr_5_6_5(ref DoublePrecision.Vector3 realRgb)
		{
			Pack(ref realRgb, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector3 realRgb)
		{
			Pack(ref realRgb, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector3 realRgb)
		{
			Unpack(this.packedValue, out realRgb);
		}

		public Bgr_5_6_5(ref Fixed32Precision.Vector3 realRgb)
		{
			Pack(ref realRgb, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector3 realRgb)
		{
			Pack(ref realRgb, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector3 realRgb)
		{
			Unpack(this.packedValue, out realRgb);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector3 realRgb, out UInt16 packedBgr)
		{
			SinglePrecision.Vector3 singleVector = new SinglePrecision.Vector3((Single)realRgb.X, (Single)realRgb.Y, (Single)realRgb.Z);
			Pack(ref singleVector, out packedBgr);
		}

		static void Unpack(UInt16 packedBgr, out HalfPrecision.Vector3 realRgb)
		{
			SinglePrecision.Vector3 singleVector;
			Unpack(packedBgr, out singleVector);
			realRgb = new HalfPrecision.Vector3((Half)singleVector.X, (Half)singleVector.Y, (Half)singleVector.Z);
		}
		static void Pack(ref DoublePrecision.Vector3 realRgb, out UInt16 packedBgr)
		{
			SinglePrecision.Vector3 singleVector = new SinglePrecision.Vector3((Single)realRgb.X, (Single)realRgb.Y, (Single)realRgb.Z);
			Pack(ref singleVector, out packedBgr);
		}

		static void Unpack(UInt16 packedBgr, out DoublePrecision.Vector3 realRgb)
		{
			SinglePrecision.Vector3 singleVector;
			Unpack(packedBgr, out singleVector);
			realRgb = new DoublePrecision.Vector3((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z);
		}
		static void Pack(ref Fixed32Precision.Vector3 realRgb, out UInt16 packedBgr)
		{
			SinglePrecision.Vector3 singleVector = new SinglePrecision.Vector3((Single)realRgb.X, (Single)realRgb.Y, (Single)realRgb.Z);
			Pack(ref singleVector, out packedBgr);
		}

		static void Unpack(UInt16 packedBgr, out Fixed32Precision.Vector3 realRgb)
		{
			SinglePrecision.Vector3 singleVector;
			Unpack(packedBgr, out singleVector);
			realRgb = new Fixed32Precision.Vector3((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Bgra16 
		: IPackedValue<UInt16>
		, IEquatable<Bgra16>
		, IPackedReal4
	{

		public override String ToString ()
		{
			return this.packedValue.ToString ("X4", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector4 realRgba, out UInt16 packedBgra)
		{
			UInt32 r = PackUtils.PackUnsignedNormalisedValue (15f, realRgba.X) << 8;
			UInt32 g = PackUtils.PackUnsignedNormalisedValue (15f, realRgba.Y) << 4;
			UInt32 b = PackUtils.PackUnsignedNormalisedValue (15f, realRgba.Z);
			UInt32 a = PackUtils.PackUnsignedNormalisedValue (15f, realRgba.W) << 12;
			packedBgra = (UInt16)(((r | g) | b) | a);
		}

		static void Unpack(UInt16 packedBgra, out SinglePrecision.Vector4 realRgba)
		{
			realRgba.X = PackUtils.UnpackUnsignedNormalisedValue (15, (UInt32)(packedBgra >> 8));
			realRgba.Y = PackUtils.UnpackUnsignedNormalisedValue (15, (UInt32)(packedBgra >> 4));
			realRgba.Z = PackUtils.UnpackUnsignedNormalisedValue (15, packedBgra);
			realRgba.W = PackUtils.UnpackUnsignedNormalisedValue (15, (UInt32)(packedBgra >> 12));
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt16 packedValue;

 
		[CLSCompliant (false)]
		public UInt16 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is Bgra16) && this.Equals((Bgra16)obj));
		}

		public Boolean Equals(Bgra16 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(Bgra16 a, Bgra16 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(Bgra16 a, Bgra16 b)
		{
			return !a.Equals(b);
		}

		public Bgra16(ref HalfPrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		public Bgra16(ref SinglePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		public Bgra16(ref DoublePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		public Bgra16(ref Fixed32Precision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector4 realRgba, out UInt16 packedBgra)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
			Pack(ref singleVector, out packedBgra);
		}

		static void Unpack(UInt16 packedBgra, out HalfPrecision.Vector4 realRgba)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedBgra, out singleVector);
			realRgba = new HalfPrecision.Vector4((Half)singleVector.X, (Half)singleVector.Y, (Half)singleVector.Z, (Half)singleVector.W);
		}
		static void Pack(ref DoublePrecision.Vector4 realRgba, out UInt16 packedBgra)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
			Pack(ref singleVector, out packedBgra);
		}

		static void Unpack(UInt16 packedBgra, out DoublePrecision.Vector4 realRgba)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedBgra, out singleVector);
			realRgba = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
		}
		static void Pack(ref Fixed32Precision.Vector4 realRgba, out UInt16 packedBgra)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
			Pack(ref singleVector, out packedBgra);
		}

		static void Unpack(UInt16 packedBgra, out Fixed32Precision.Vector4 realRgba)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedBgra, out singleVector);
			realRgba = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Bgra_5_5_5_1 
		: IPackedValue<UInt16>
		, IEquatable<Bgra_5_5_5_1>
		, IPackedReal4
	{
		public override String ToString ()
		{
			return this.packedValue.ToString ("X4", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector4 realRgba, out UInt16 packedBgra)
		{
			UInt32 r = PackUtils.PackUnsignedNormalisedValue (31f, realRgba.X) << 10;
			UInt32 g = PackUtils.PackUnsignedNormalisedValue (31f, realRgba.Y) << 5;
			UInt32 b = PackUtils.PackUnsignedNormalisedValue (31f, realRgba.Z);
			UInt32 a = PackUtils.PackUnsignedNormalisedValue (1f, realRgba.W) << 15;
			packedBgra = (UInt16)(((r | g) | b) | a);
		}

		static void Unpack(UInt16 packedBgra, out SinglePrecision.Vector4 realRgba)
		{
			realRgba.X = PackUtils.UnpackUnsignedNormalisedValue (0x1f, (UInt32)(packedBgra >> 10));
			realRgba.Y = PackUtils.UnpackUnsignedNormalisedValue (0x1f, (UInt32)(packedBgra >> 5));
			realRgba.Z = PackUtils.UnpackUnsignedNormalisedValue (0x1f, packedBgra);
			realRgba.W = PackUtils.UnpackUnsignedNormalisedValue (1, (UInt32)(packedBgra >> 15));
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt16 packedValue;

 
		[CLSCompliant (false)]
		public UInt16 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is Bgra_5_5_5_1) && this.Equals((Bgra_5_5_5_1)obj));
		}

		public Boolean Equals(Bgra_5_5_5_1 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(Bgra_5_5_5_1 a, Bgra_5_5_5_1 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(Bgra_5_5_5_1 a, Bgra_5_5_5_1 b)
		{
			return !a.Equals(b);
		}

		public Bgra_5_5_5_1(ref HalfPrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		public Bgra_5_5_5_1(ref SinglePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		public Bgra_5_5_5_1(ref DoublePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		public Bgra_5_5_5_1(ref Fixed32Precision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector4 realRgba, out UInt16 packedBgra)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
			Pack(ref singleVector, out packedBgra);
		}

		static void Unpack(UInt16 packedBgra, out HalfPrecision.Vector4 realRgba)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedBgra, out singleVector);
			realRgba = new HalfPrecision.Vector4((Half)singleVector.X, (Half)singleVector.Y, (Half)singleVector.Z, (Half)singleVector.W);
		}
		static void Pack(ref DoublePrecision.Vector4 realRgba, out UInt16 packedBgra)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
			Pack(ref singleVector, out packedBgra);
		}

		static void Unpack(UInt16 packedBgra, out DoublePrecision.Vector4 realRgba)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedBgra, out singleVector);
			realRgba = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
		}
		static void Pack(ref Fixed32Precision.Vector4 realRgba, out UInt16 packedBgra)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
			Pack(ref singleVector, out packedBgra);
		}

		static void Unpack(UInt16 packedBgra, out Fixed32Precision.Vector4 realRgba)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedBgra, out singleVector);
			realRgba = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Byte4 
		: IPackedValue<UInt32>
		, IEquatable<Byte4>
	{
		public override String ToString ()
		{
			return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector4 realXyzw, out UInt32 packedXyzw)
		{
			UInt32 y = PackUtils.PackUnsigned (255f, realXyzw.X);
			UInt32 x = PackUtils.PackUnsigned (255f, realXyzw.Y) << 8;
			UInt32 z = PackUtils.PackUnsigned (255f, realXyzw.Z) << 0x10;
			UInt32 w = PackUtils.PackUnsigned (255f, realXyzw.W) << 0x18;
			packedXyzw = (UInt32)(((y | x) | z) | w);
		}

		static void Unpack(UInt32 packedXyzw, out SinglePrecision.Vector4 realXyzw)
		{
			realXyzw.X = packedXyzw & 0xff;
			realXyzw.Y = (packedXyzw >> 8) & 0xff;
			realXyzw.Z = (packedXyzw >> 0x10) & 0xff;
			realXyzw.W = (packedXyzw >> 0x18) & 0xff;
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt32 packedValue;

 
		[CLSCompliant (false)]
		public UInt32 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is Byte4) && this.Equals((Byte4)obj));
		}

		public Boolean Equals(Byte4 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(Byte4 a, Byte4 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(Byte4 a, Byte4 b)
		{
			return !a.Equals(b);
		}

		public Byte4(ref HalfPrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		public Byte4(ref SinglePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		public Byte4(ref DoublePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		public Byte4(ref Fixed32Precision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector4 realXyzw, out UInt32 packedXyzw)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
			Pack(ref singleVector, out packedXyzw);
		}

		static void Unpack(UInt32 packedXyzw, out HalfPrecision.Vector4 realXyzw)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedXyzw, out singleVector);
			realXyzw = new HalfPrecision.Vector4((Half)singleVector.X, (Half)singleVector.Y, (Half)singleVector.Z, (Half)singleVector.W);
		}
		static void Pack(ref DoublePrecision.Vector4 realXyzw, out UInt32 packedXyzw)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
			Pack(ref singleVector, out packedXyzw);
		}

		static void Unpack(UInt32 packedXyzw, out DoublePrecision.Vector4 realXyzw)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedXyzw, out singleVector);
			realXyzw = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
		}
		static void Pack(ref Fixed32Precision.Vector4 realXyzw, out UInt32 packedXyzw)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
			Pack(ref singleVector, out packedXyzw);
		}

		static void Unpack(UInt32 packedXyzw, out Fixed32Precision.Vector4 realXyzw)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedXyzw, out singleVector);
			realXyzw = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
		}

	}
	[StructLayout (LayoutKind.Sequential)]
	public struct NormalisedByte2 
		: IPackedValue<UInt16>
		, IEquatable<NormalisedByte2>
		, IPackedReal2
	{
		public override String ToString ()
		{
			return this.packedValue.ToString ("X4", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector2 realXy, out UInt16 packedXy)
		{
			UInt32 x = PackUtils.PackSignedNormalised(0xff, realXy.X);
			UInt32 y = PackUtils.PackSignedNormalised(0xff, realXy.Y) << 8;
			packedXy = (UInt16)(x | y);
		}

		static void Unpack(UInt16 packedXy, out SinglePrecision.Vector2 realXy)
		{
			realXy.X = PackUtils.UnpackSignedNormalised (0xff, packedXy);
			realXy.Y = PackUtils.UnpackSignedNormalised (0xff, (UInt32) (packedXy >> 8));
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt16 packedValue;

 
		[CLSCompliant (false)]
		public UInt16 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is NormalisedByte2) && this.Equals((NormalisedByte2)obj));
		}

		public Boolean Equals(NormalisedByte2 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(NormalisedByte2 a, NormalisedByte2 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(NormalisedByte2 a, NormalisedByte2 b)
		{
			return !a.Equals(b);
		}

		public NormalisedByte2(ref HalfPrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector2 realXy)
		{
			Unpack(this.packedValue, out realXy);
		}

		public NormalisedByte2(ref SinglePrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector2 realXy)
		{
			Unpack(this.packedValue, out realXy);
		}

		public NormalisedByte2(ref DoublePrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector2 realXy)
		{
			Unpack(this.packedValue, out realXy);
		}

		public NormalisedByte2(ref Fixed32Precision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector2 realXy)
		{
			Unpack(this.packedValue, out realXy);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector2 realXy, out UInt16 packedXy)
		{
			SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
			Pack(ref singleVector, out packedXy);
		}

		static void Unpack(UInt16 packedXy, out HalfPrecision.Vector2 realXy)
		{
			SinglePrecision.Vector2 singleVector;
			Unpack(packedXy, out singleVector);
			realXy = new HalfPrecision.Vector2((Half)singleVector.X, (Half)singleVector.Y);
		}
		static void Pack(ref DoublePrecision.Vector2 realXy, out UInt16 packedXy)
		{
			SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
			Pack(ref singleVector, out packedXy);
		}

		static void Unpack(UInt16 packedXy, out DoublePrecision.Vector2 realXy)
		{
			SinglePrecision.Vector2 singleVector;
			Unpack(packedXy, out singleVector);
			realXy = new DoublePrecision.Vector2((Double)singleVector.X, (Double)singleVector.Y);
		}
		static void Pack(ref Fixed32Precision.Vector2 realXy, out UInt16 packedXy)
		{
			SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
			Pack(ref singleVector, out packedXy);
		}

		static void Unpack(UInt16 packedXy, out Fixed32Precision.Vector2 realXy)
		{
			SinglePrecision.Vector2 singleVector;
			Unpack(packedXy, out singleVector);
			realXy = new Fixed32Precision.Vector2((Fixed32)singleVector.X, (Fixed32)singleVector.Y);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct NormalisedByte4 
		: IPackedValue<UInt32>
		, IEquatable<NormalisedByte4>
		, IPackedReal4
	{
		public override String ToString ()
		{
			return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector4 realXyzw, out UInt32 packedXyzw)
		{
			UInt32 x = PackUtils.PackSignedNormalised(0xff, realXyzw.X);
			UInt32 y = PackUtils.PackSignedNormalised(0xff, realXyzw.Y) << 8;
			UInt32 z = PackUtils.PackSignedNormalised(0xff, realXyzw.Z) << 16;
			UInt32 w = PackUtils.PackSignedNormalised(0xff, realXyzw.W) << 24;
			packedXyzw = (((x | y) | z) | w);
		}

		static void Unpack(UInt32 packedXyzw, out SinglePrecision.Vector4 realXyzw)
		{
			realXyzw.X = PackUtils.UnpackSignedNormalised (0xff, packedXyzw);
			realXyzw.Y = PackUtils.UnpackSignedNormalised (0xff, (UInt32) (packedXyzw >> 8));
			realXyzw.Z = PackUtils.UnpackSignedNormalised (0xff, (UInt32) (packedXyzw >> 16));
			realXyzw.W = PackUtils.UnpackSignedNormalised (0xff, (UInt32) (packedXyzw >> 24));
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt32 packedValue;

 
		[CLSCompliant (false)]
		public UInt32 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is NormalisedByte4) && this.Equals((NormalisedByte4)obj));
		}

		public Boolean Equals(NormalisedByte4 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(NormalisedByte4 a, NormalisedByte4 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(NormalisedByte4 a, NormalisedByte4 b)
		{
			return !a.Equals(b);
		}

		public NormalisedByte4(ref HalfPrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		public NormalisedByte4(ref SinglePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		public NormalisedByte4(ref DoublePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		public NormalisedByte4(ref Fixed32Precision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector4 realXyzw, out UInt32 packedXyzw)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
			Pack(ref singleVector, out packedXyzw);
		}

		static void Unpack(UInt32 packedXyzw, out HalfPrecision.Vector4 realXyzw)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedXyzw, out singleVector);
			realXyzw = new HalfPrecision.Vector4((Half)singleVector.X, (Half)singleVector.Y, (Half)singleVector.Z, (Half)singleVector.W);
		}
		static void Pack(ref DoublePrecision.Vector4 realXyzw, out UInt32 packedXyzw)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
			Pack(ref singleVector, out packedXyzw);
		}

		static void Unpack(UInt32 packedXyzw, out DoublePrecision.Vector4 realXyzw)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedXyzw, out singleVector);
			realXyzw = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
		}
		static void Pack(ref Fixed32Precision.Vector4 realXyzw, out UInt32 packedXyzw)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
			Pack(ref singleVector, out packedXyzw);
		}

		static void Unpack(UInt32 packedXyzw, out Fixed32Precision.Vector4 realXyzw)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedXyzw, out singleVector);
			realXyzw = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct NormalisedShort2 
		: IPackedValue<UInt32>
		, IEquatable<NormalisedShort2>
		, IPackedReal2
	{
		public override String ToString ()
		{
			return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector2 realXy, out UInt32 packedXy)
		{
			UInt32 x = PackUtils.PackSignedNormalised(0xffff, realXy.X);
			UInt32 y = PackUtils.PackSignedNormalised(0xffff, realXy.Y) << 16;
			packedXy = (x | y);
		}

		static void Unpack(UInt32 packedXy, out SinglePrecision.Vector2 realXy)
		{
			realXy.X = PackUtils.UnpackSignedNormalised (0xffff, packedXy);
			realXy.Y = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) (packedXy >> 16));
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt32 packedValue;

 
		[CLSCompliant (false)]
		public UInt32 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is NormalisedShort2) && this.Equals((NormalisedShort2)obj));
		}

		public Boolean Equals(NormalisedShort2 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(NormalisedShort2 a, NormalisedShort2 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(NormalisedShort2 a, NormalisedShort2 b)
		{
			return !a.Equals(b);
		}

		public NormalisedShort2(ref HalfPrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector2 realXy)
		{
			Unpack(this.packedValue, out realXy);
		}

		public NormalisedShort2(ref SinglePrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector2 realXy)
		{
			Unpack(this.packedValue, out realXy);
		}

		public NormalisedShort2(ref DoublePrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector2 realXy)
		{
			Unpack(this.packedValue, out realXy);
		}

		public NormalisedShort2(ref Fixed32Precision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector2 realXy)
		{
			Unpack(this.packedValue, out realXy);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector2 realXy, out UInt32 packedXy)
		{
			SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
			Pack(ref singleVector, out packedXy);
		}

		static void Unpack(UInt32 packedXy, out HalfPrecision.Vector2 realXy)
		{
			SinglePrecision.Vector2 singleVector;
			Unpack(packedXy, out singleVector);
			realXy = new HalfPrecision.Vector2((Half)singleVector.X, (Half)singleVector.Y);
		}
		static void Pack(ref DoublePrecision.Vector2 realXy, out UInt32 packedXy)
		{
			SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
			Pack(ref singleVector, out packedXy);
		}

		static void Unpack(UInt32 packedXy, out DoublePrecision.Vector2 realXy)
		{
			SinglePrecision.Vector2 singleVector;
			Unpack(packedXy, out singleVector);
			realXy = new DoublePrecision.Vector2((Double)singleVector.X, (Double)singleVector.Y);
		}
		static void Pack(ref Fixed32Precision.Vector2 realXy, out UInt32 packedXy)
		{
			SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
			Pack(ref singleVector, out packedXy);
		}

		static void Unpack(UInt32 packedXy, out Fixed32Precision.Vector2 realXy)
		{
			SinglePrecision.Vector2 singleVector;
			Unpack(packedXy, out singleVector);
			realXy = new Fixed32Precision.Vector2((Fixed32)singleVector.X, (Fixed32)singleVector.Y);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct NormalisedShort4 
		: IPackedValue<UInt64>
		, IEquatable<NormalisedShort4>
		, IPackedReal4
	{
		public override String ToString ()
		{
			return this.packedValue.ToString ("X16", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector4 realXyzw, out UInt64 packedXyzw)
		{
			UInt64 x = PackUtils.PackSignedNormalised(0xffff, realXyzw.X);
			UInt64 y = PackUtils.PackSignedNormalised(0xffff, realXyzw.Y) << 16;
			UInt64 z = PackUtils.PackSignedNormalised(0xffff, realXyzw.Z) << 32;
			UInt64 w = PackUtils.PackSignedNormalised(0xffff, realXyzw.W) << 48;
			packedXyzw = (((x | y) | z) | w);
		}

		static void Unpack(UInt64 packedXyzw, out SinglePrecision.Vector4 realXyzw)
		{
			realXyzw.X = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) packedXyzw);
			realXyzw.Y = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) (packedXyzw >> 16));
			realXyzw.Z = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) (packedXyzw >> 32));
			realXyzw.W = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) (packedXyzw >> 48));
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt64 packedValue;

 
		[CLSCompliant (false)]
		public UInt64 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is NormalisedShort4) && this.Equals((NormalisedShort4)obj));
		}

		public Boolean Equals(NormalisedShort4 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(NormalisedShort4 a, NormalisedShort4 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(NormalisedShort4 a, NormalisedShort4 b)
		{
			return !a.Equals(b);
		}

		public NormalisedShort4(ref HalfPrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		public NormalisedShort4(ref SinglePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		public NormalisedShort4(ref DoublePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		public NormalisedShort4(ref Fixed32Precision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector4 realXyzw, out UInt64 packedXyzw)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
			Pack(ref singleVector, out packedXyzw);
		}

		static void Unpack(UInt64 packedXyzw, out HalfPrecision.Vector4 realXyzw)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedXyzw, out singleVector);
			realXyzw = new HalfPrecision.Vector4((Half)singleVector.X, (Half)singleVector.Y, (Half)singleVector.Z, (Half)singleVector.W);
		}
		static void Pack(ref DoublePrecision.Vector4 realXyzw, out UInt64 packedXyzw)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
			Pack(ref singleVector, out packedXyzw);
		}

		static void Unpack(UInt64 packedXyzw, out DoublePrecision.Vector4 realXyzw)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedXyzw, out singleVector);
			realXyzw = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
		}
		static void Pack(ref Fixed32Precision.Vector4 realXyzw, out UInt64 packedXyzw)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
			Pack(ref singleVector, out packedXyzw);
		}

		static void Unpack(UInt64 packedXyzw, out Fixed32Precision.Vector4 realXyzw)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedXyzw, out singleVector);
			realXyzw = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Rg32 
		: IPackedValue<UInt32>
		, IEquatable<Rg32>
		, IPackedReal2
	{
		public override String ToString ()
		{
			return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector2 realRg, out UInt32 packedRg)
		{
			UInt32 x = PackUtils.PackUnsignedNormalisedValue(0xffff, realRg.X);
			UInt32 y = PackUtils.PackUnsignedNormalisedValue(0xffff, realRg.Y) << 16;
			packedRg = (x | y);
		}

		static void Unpack(UInt32 packedRg, out SinglePrecision.Vector2 realRg)
		{
			realRg.X = PackUtils.UnpackUnsignedNormalisedValue (0xffff, packedRg);
			realRg.Y = PackUtils.UnpackUnsignedNormalisedValue (0xffff, (UInt32) (packedRg >> 16));
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt32 packedValue;

 
		[CLSCompliant (false)]
		public UInt32 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is Rg32) && this.Equals((Rg32)obj));
		}

		public Boolean Equals(Rg32 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(Rg32 a, Rg32 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(Rg32 a, Rg32 b)
		{
			return !a.Equals(b);
		}

		public Rg32(ref HalfPrecision.Vector2 realRg)
		{
			Pack(ref realRg, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector2 realRg)
		{
			Pack(ref realRg, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector2 realRg)
		{
			Unpack(this.packedValue, out realRg);
		}

		public Rg32(ref SinglePrecision.Vector2 realRg)
		{
			Pack(ref realRg, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector2 realRg)
		{
			Pack(ref realRg, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector2 realRg)
		{
			Unpack(this.packedValue, out realRg);
		}

		public Rg32(ref DoublePrecision.Vector2 realRg)
		{
			Pack(ref realRg, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector2 realRg)
		{
			Pack(ref realRg, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector2 realRg)
		{
			Unpack(this.packedValue, out realRg);
		}

		public Rg32(ref Fixed32Precision.Vector2 realRg)
		{
			Pack(ref realRg, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector2 realRg)
		{
			Pack(ref realRg, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector2 realRg)
		{
			Unpack(this.packedValue, out realRg);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector2 realRg, out UInt32 packedRg)
		{
			SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realRg.X, (Single)realRg.Y);
			Pack(ref singleVector, out packedRg);
		}

		static void Unpack(UInt32 packedRg, out HalfPrecision.Vector2 realRg)
		{
			SinglePrecision.Vector2 singleVector;
			Unpack(packedRg, out singleVector);
			realRg = new HalfPrecision.Vector2((Half)singleVector.X, (Half)singleVector.Y);
		}
		static void Pack(ref DoublePrecision.Vector2 realRg, out UInt32 packedRg)
		{
			SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realRg.X, (Single)realRg.Y);
			Pack(ref singleVector, out packedRg);
		}

		static void Unpack(UInt32 packedRg, out DoublePrecision.Vector2 realRg)
		{
			SinglePrecision.Vector2 singleVector;
			Unpack(packedRg, out singleVector);
			realRg = new DoublePrecision.Vector2((Double)singleVector.X, (Double)singleVector.Y);
		}
		static void Pack(ref Fixed32Precision.Vector2 realRg, out UInt32 packedRg)
		{
			SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realRg.X, (Single)realRg.Y);
			Pack(ref singleVector, out packedRg);
		}

		static void Unpack(UInt32 packedRg, out Fixed32Precision.Vector2 realRg)
		{
			SinglePrecision.Vector2 singleVector;
			Unpack(packedRg, out singleVector);
			realRg = new Fixed32Precision.Vector2((Fixed32)singleVector.X, (Fixed32)singleVector.Y);
		}
		
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Rgba32 
		: IPackedValue<UInt32>
		, IEquatable<Rgba32>
		, IPackedReal4
	{
		public override String ToString ()
		{
			return string.Format ("{{R:{0} G:{1} B:{2} A:{3}}}", new Object[] { this.R, this.G, this.B, this.A });
		}

		static void Pack(ref SinglePrecision.Vector4 realRgba32, out UInt32 packedRgba32)
		{
			UInt32 r = PackUtils.PackUnsignedNormalisedValue (0xff, realRgba32.X);
			UInt32 g = PackUtils.PackUnsignedNormalisedValue (0xff, realRgba32.Y) << 8;
			UInt32 b = PackUtils.PackUnsignedNormalisedValue (0xff, realRgba32.Z) << 16;
			UInt32 a = PackUtils.PackUnsignedNormalisedValue (0xff, realRgba32.W) << 24;
			packedRgba32 = ((r | g) | b) | a;
		}

		static void Unpack(UInt32 packedRgba32, out SinglePrecision.Vector4 realRgba32)
		{
			realRgba32.X = PackUtils.UnpackUnsignedNormalisedValue (0xff, packedRgba32);
			realRgba32.Y = PackUtils.UnpackUnsignedNormalisedValue (0xff, (UInt32)(packedRgba32 >> 8));
			realRgba32.Z = PackUtils.UnpackUnsignedNormalisedValue (0xff, (UInt32)(packedRgba32 >> 16));
			realRgba32.W = PackUtils.UnpackUnsignedNormalisedValue (0xff, (UInt32)(packedRgba32 >> 24));
		}

		public static Rgba32 Transparent {
			get {
				return new Rgba32 (0);
			}
		}
		
		public static Rgba32 AliceBlue {
			get {
				return new Rgba32 (0xfffff8f0);
			}
		}
		
		public static Rgba32 AntiqueWhite {
			get {
				return new Rgba32 (0xffd7ebfa);
			}
		}
		
		public static Rgba32 Aqua {
			get {
				return new Rgba32 (0xffffff00);
			}
		}
		
		public static Rgba32 Aquamarine {
			get {
				return new Rgba32 (0xffd4ff7f);
			}
		}
		
		public static Rgba32 Azure {
			get {
				return new Rgba32 (0xfffffff0);
			}
		}
		
		public static Rgba32 Beige {
			get {
				return new Rgba32 (0xffdcf5f5);
			}
		}
		
		public static Rgba32 Bisque {
			get {
				return new Rgba32 (0xffc4e4ff);
			}
		}
		
		public static Rgba32 Black {
			get {
				return new Rgba32 (0xff000000);
			}
		}
		
		public static Rgba32 BlanchedAlmond {
			get {
				return new Rgba32 (0xffcdebff);
			}
		}
		
		public static Rgba32 Blue {
			get {
				return new Rgba32 (0xffff0000);
			}
		}
		
		public static Rgba32 BlueViolet {
			get {
				return new Rgba32 (0xffe22b8a);
			}
		}
		
		public static Rgba32 Brown {
			get {
				return new Rgba32 (0xff2a2aa5);
			}
		}
		
		public static Rgba32 BurlyWood {
			get {
				return new Rgba32 (0xff87b8de);
			}
		}
		
		public static Rgba32 CadetBlue {
			get {
				return new Rgba32 (0xffa09e5f);
			}
		}
		
		public static Rgba32 Chartreuse {
			get {
				return new Rgba32 (0xff00ff7f);
			}
		}
		
		public static Rgba32 Chocolate {
			get {
				return new Rgba32 (0xff1e69d2);
			}
		}
		
		public static Rgba32 Coral {
			get {
				return new Rgba32 (0xff507fff);
			}
		}
		
		public static Rgba32 CornflowerBlue {
			get {
				return new Rgba32 (0xffed9564);
			}
		}
		
		public static Rgba32 Cornsilk {
			get {
				return new Rgba32 (0xffdcf8ff);
			}
		}
		
		public static Rgba32 Crimson {
			get {
				return new Rgba32 (0xff3c14dc);
			}
		}
		
		public static Rgba32 Cyan {
			get {
				return new Rgba32 (0xffffff00);
			}
		}
		
		public static Rgba32 DarkBlue {
			get {
				return new Rgba32 (0xff8b0000);
			}
		}
		
		public static Rgba32 DarkCyan {
			get {
				return new Rgba32 (0xff8b8b00);
			}
		}
		
		public static Rgba32 DarkGoldenrod {
			get {
				return new Rgba32 (0xff0b86b8);
			}
		}
		
		public static Rgba32 DarkGray {
			get {
				return new Rgba32 (0xffa9a9a9);
			}
		}
		
		public static Rgba32 DarkGreen {
			get {
				return new Rgba32 (0xff006400);
			}
		}
		
		public static Rgba32 DarkKhaki {
			get {
				return new Rgba32 (0xff6bb7bd);
			}
		}
		
		public static Rgba32 DarkMagenta {
			get {
				return new Rgba32 (0xff8b008b);
			}
		}
		
		public static Rgba32 DarkOliveGreen {
			get {
				return new Rgba32 (0xff2f6b55);
			}
		}
		
		public static Rgba32 DarkOrange {
			get {
				return new Rgba32 (0xff008cff);
			}
		}
		
		public static Rgba32 DarkOrchid {
			get {
				return new Rgba32 (0xffcc3299);
			}
		}
		
		public static Rgba32 DarkRed {
			get {
				return new Rgba32 (0xff00008b);
			}
		}
		
		public static Rgba32 DarkSalmon {
			get {
				return new Rgba32 (0xff7a96e9);
			}
		}
		
		public static Rgba32 DarkSeaGreen {
			get {
				return new Rgba32 (0xff8bbc8f);
			}
		}
		
		public static Rgba32 DarkSlateBlue {
			get {
				return new Rgba32 (0xff8b3d48);
			}
		}
		
		public static Rgba32 DarkSlateGray {
			get {
				return new Rgba32 (0xff4f4f2f);
			}
		}
		
		public static Rgba32 DarkTurquoise {
			get {
				return new Rgba32 (0xffd1ce00);
			}
		}
		
		public static Rgba32 DarkViolet {
			get {
				return new Rgba32 (0xffd30094);
			}
		}
		
		public static Rgba32 DeepPink {
			get {
				return new Rgba32 (0xff9314ff);
			}
		}
		
		public static Rgba32 DeepSkyBlue {
			get {
				return new Rgba32 (0xffffbf00);
			}
		}
		
		public static Rgba32 DimGray {
			get {
				return new Rgba32 (0xff696969);
			}
		}
		
		public static Rgba32 DodgerBlue {
			get {
				return new Rgba32 (0xffff901e);
			}
		}
		
		public static Rgba32 Firebrick {
			get {
				return new Rgba32 (0xff2222b2);
			}
		}
		
		public static Rgba32 FloralWhite {
			get {
				return new Rgba32 (0xfff0faff);
			}
		}
		
		public static Rgba32 ForestGreen {
			get {
				return new Rgba32 (0xff228b22);
			}
		}
		
		public static Rgba32 Fuchsia {
			get {
				return new Rgba32 (0xffff00ff);
			}
		}
		
		public static Rgba32 Gainsboro {
			get {
				return new Rgba32 (0xffdcdcdc);
			}
		}
		
		public static Rgba32 GhostWhite {
			get {
				return new Rgba32 (0xfffff8f8);
			}
		}
		
		public static Rgba32 Gold {
			get {
				return new Rgba32 (0xff00d7ff);
			}
		}
		
		public static Rgba32 Goldenrod {
			get {
				return new Rgba32 (0xff20a5da);
			}
		}
		
		public static Rgba32 Gray {
			get {
				return new Rgba32 (0xff808080);
			}
		}
		
		public static Rgba32 Green {
			get {
				return new Rgba32 (0xff008000);
			}
		}
		
		public static Rgba32 GreenYellow {
			get {
				return new Rgba32 (0xff2fffad);
			}
		}
		
		public static Rgba32 Honeydew {
			get {
				return new Rgba32 (0xfff0fff0);
			}
		}
		
		public static Rgba32 HotPink {
			get {
				return new Rgba32 (0xffb469ff);
			}
		}
		
		public static Rgba32 IndianRed {
			get {
				return new Rgba32 (0xff5c5ccd);
			}
		}
		
		public static Rgba32 Indigo {
			get {
				return new Rgba32 (0xff82004b);
			}
		}
		
		public static Rgba32 Ivory {
			get {
				return new Rgba32 (0xfff0ffff);
			}
		}
		
		public static Rgba32 Khaki {
			get {
				return new Rgba32 (0xff8ce6f0);
			}
		}
		
		public static Rgba32 Lavender {
			get {
				return new Rgba32 (0xfffae6e6);
			}
		}
		
		public static Rgba32 LavenderBlush {
			get {
				return new Rgba32 (0xfff5f0ff);
			}
		}
		
		public static Rgba32 LawnGreen {
			get {
				return new Rgba32 (0xff00fc7c);
			}
		}
		
		public static Rgba32 LemonChiffon {
			get {
				return new Rgba32 (0xffcdfaff);
			}
		}
		
		public static Rgba32 LightBlue {
			get {
				return new Rgba32 (0xffe6d8ad);
			}
		}
		
		public static Rgba32 LightCoral {
			get {
				return new Rgba32 (0xff8080f0);
			}
		}
		
		public static Rgba32 LightCyan {
			get {
				return new Rgba32 (0xffffffe0);
			}
		}
		
		public static Rgba32 LightGoldenrodYellow {
			get {
				return new Rgba32 (0xffd2fafa);
			}
		}
		
		public static Rgba32 LightGreen {
			get {
				return new Rgba32 (0xff90ee90);
			}
		}
		
		public static Rgba32 LightGray {
			get {
				return new Rgba32 (0xffd3d3d3);
			}
		}
		
		public static Rgba32 LightPink {
			get {
				return new Rgba32 (0xffc1b6ff);
			}
		}
		
		public static Rgba32 LightSalmon {
			get {
				return new Rgba32 (0xff7aa0ff);
			}
		}
		
		public static Rgba32 LightSeaGreen {
			get {
				return new Rgba32 (0xffaab220);
			}
		}
		
		public static Rgba32 LightSkyBlue {
			get {
				return new Rgba32 (0xffface87);
			}
		}
		
		public static Rgba32 LightSlateGray {
			get {
				return new Rgba32 (0xff998877);
			}
		}
		
		public static Rgba32 LightSteelBlue {
			get {
				return new Rgba32 (0xffdec4b0);
			}
		}
		
		public static Rgba32 LightYellow {
			get {
				return new Rgba32 (0xffe0ffff);
			}
		}
		
		public static Rgba32 Lime {
			get {
				return new Rgba32 (0xff00ff00);
			}
		}
		
		public static Rgba32 LimeGreen {
			get {
				return new Rgba32 (0xff32cd32);
			}
		}
		
		public static Rgba32 Linen {
			get {
				return new Rgba32 (0xffe6f0fa);
			}
		}
		
		public static Rgba32 Magenta {
			get {
				return new Rgba32 (0xffff00ff);
			}
		}
		
		public static Rgba32 Maroon {
			get {
				return new Rgba32 (0xff000080);
			}
		}
		
		public static Rgba32 MediumAquamarine {
			get {
				return new Rgba32 (0xffaacd66);
			}
		}
		
		public static Rgba32 MediumBlue {
			get {
				return new Rgba32 (0xffcd0000);
			}
		}
		
		public static Rgba32 MediumOrchid {
			get {
				return new Rgba32 (0xffd355ba);
			}
		}
		
		public static Rgba32 MediumPurple {
			get {
				return new Rgba32 (0xffdb7093);
			}
		}
		
		public static Rgba32 MediumSeaGreen {
			get {
				return new Rgba32 (0xff71b33c);
			}
		}
		
		public static Rgba32 MediumSlateBlue {
			get {
				return new Rgba32 (0xffee687b);
			}
		}
		
		public static Rgba32 MediumSpringGreen {
			get {
				return new Rgba32 (0xff9afa00);
			}
		}
		
		public static Rgba32 MediumTurquoise {
			get {
				return new Rgba32 (0xffccd148);
			}
		}
		
		public static Rgba32 MediumVioletRed {
			get {
				return new Rgba32 (0xff8515c7);
			}
		}
		
		public static Rgba32 MidnightBlue {
			get {
				return new Rgba32 (0xff701919);
			}
		}
		
		public static Rgba32 MintCream {
			get {
				return new Rgba32 (0xfffafff5);
			}
		}
		
		public static Rgba32 MistyRose {
			get {
				return new Rgba32 (0xffe1e4ff);
			}
		}
		
		public static Rgba32 Moccasin {
			get {
				return new Rgba32 (0xffb5e4ff);
			}
		}
		
		public static Rgba32 NavajoWhite {
			get {
				return new Rgba32 (0xffaddeff);
			}
		}
		
		public static Rgba32 Navy {
			get {
				return new Rgba32 (0xff800000);
			}
		}
		
		public static Rgba32 OldLace {
			get {
				return new Rgba32 (0xffe6f5fd);
			}
		}
		
		public static Rgba32 Olive {
			get {
				return new Rgba32 (0xff008080);
			}
		}
		
		public static Rgba32 OliveDrab {
			get {
				return new Rgba32 (0xff238e6b);
			}
		}
		
		public static Rgba32 Orange {
			get {
				return new Rgba32 (0xff00a5ff);
			}
		}
		
		public static Rgba32 OrangeRed {
			get {
				return new Rgba32 (0xff0045ff);
			}
		}
		
		public static Rgba32 Orchid {
			get {
				return new Rgba32 (0xffd670da);
			}
		}
		
		public static Rgba32 PaleGoldenrod {
			get {
				return new Rgba32 (0xffaae8ee);
			}
		}
		
		public static Rgba32 PaleGreen {
			get {
				return new Rgba32 (0xff98fb98);
			}
		}
		
		public static Rgba32 PaleTurquoise {
			get {
				return new Rgba32 (0xffeeeeaf);
			}
		}
		
		public static Rgba32 PaleVioletRed {
			get {
				return new Rgba32 (0xff9370db);
			}
		}
		
		public static Rgba32 PapayaWhip {
			get {
				return new Rgba32 (0xffd5efff);
			}
		}
		
		public static Rgba32 PeachPuff {
			get {
				return new Rgba32 (0xffb9daff);
			}
		}
		
		public static Rgba32 Peru {
			get {
				return new Rgba32 (0xff3f85cd);
			}
		}
		
		public static Rgba32 Pink {
			get {
				return new Rgba32 (0xffcbc0ff);
			}
		}
		
		public static Rgba32 Plum {
			get {
				return new Rgba32 (0xffdda0dd);
			}
		}
		
		public static Rgba32 PowderBlue {
			get {
				return new Rgba32 (0xffe6e0b0);
			}
		}
		
		public static Rgba32 Purple {
			get {
				return new Rgba32 (0xff800080);
			}
		}
		
		public static Rgba32 Red {
			get {
				return new Rgba32 (0xff0000ff);
			}
		}
		
		public static Rgba32 RosyBrown {
			get {
				return new Rgba32 (0xff8f8fbc);
			}
		}
		
		public static Rgba32 RoyalBlue {
			get {
				return new Rgba32 (0xffe16941);
			}
		}
		
		public static Rgba32 SaddleBrown {
			get {
				return new Rgba32 (0xff13458b);
			}
		}
		
		public static Rgba32 Salmon {
			get {
				return new Rgba32 (0xff7280fa);
			}
		}
		
		public static Rgba32 SandyBrown {
			get {
				return new Rgba32 (0xff60a4f4);
			}
		}
		
		public static Rgba32 SeaGreen {
			get {
				return new Rgba32 (0xff578b2e);
			}
		}
		
		public static Rgba32 SeaShell {
			get {
				return new Rgba32 (0xffeef5ff);
			}
		}
		
		public static Rgba32 Sienna {
			get {
				return new Rgba32 (0xff2d52a0);
			}
		}
		
		public static Rgba32 Silver {
			get {
				return new Rgba32 (0xffc0c0c0);
			}
		}
		
		public static Rgba32 SkyBlue {
			get {
				return new Rgba32 (0xffebce87);
			}
		}
		
		public static Rgba32 SlateBlue {
			get {
				return new Rgba32 (0xffcd5a6a);
			}
		}
		
		public static Rgba32 SlateGray {
			get {
				return new Rgba32 (0xff908070);
			}
		}
		
		public static Rgba32 Snow {
			get {
				return new Rgba32 (0xfffafaff);
			}
		}
		
		public static Rgba32 SpringGreen {
			get {
				return new Rgba32 (0xff7fff00);
			}
		}
		
		public static Rgba32 SteelBlue {
			get {
				return new Rgba32 (0xffb48246);
			}
		}
		
		public static Rgba32 Tan {
			get {
				return new Rgba32 (0xff8cb4d2);
			}
		}
		
		public static Rgba32 Teal {
			get {
				return new Rgba32 (0xff808000);
			}
		}
		
		public static Rgba32 Thistle {
			get {
				return new Rgba32 (0xffd8bfd8);
			}
		}
		
		public static Rgba32 Tomato {
			get {
				return new Rgba32 (0xff4763ff);
			}
		}
		
		public static Rgba32 Turquoise {
			get {
				return new Rgba32 (0xffd0e040);
			}
		}
		
		public static Rgba32 Violet {
			get {
				return new Rgba32 (0xffee82ee);
			}
		}
		
		public static Rgba32 Wheat {
			get {
				return new Rgba32 (0xffb3def5);
			}
		}
		
		public static Rgba32 White {
			get {
				return new Rgba32 (UInt32.MaxValue);
			}
		}
		
		public static Rgba32 WhiteSmoke {
			get {
				return new Rgba32 (0xfff5f5f5);
			}
		}
		
		public static Rgba32 Yellow {
			get {
				return new Rgba32 (0xff00ffff);
			}
		}
		
		public static Rgba32 YellowGreen {
			get {
				return new Rgba32 (0xff32cd9a);
			}
		}
		Rgba32(UInt32 packedValue)
		{
			this.packedValue = packedValue;
		}

		public Rgba32(Int32 r, Int32 g, Int32 b)
		{
			if ((((r | g) | b) & -256) != 0)
			{
				r = ClampToByte64((Int64)r);
				g = ClampToByte64((Int64)g);
				b = ClampToByte64((Int64)b);
			}

			g = g << 8;
			b = b << 0x10;

			this.packedValue = (UInt32)(((r | g) | b) | -16777216);
		}

		public Rgba32(Int32 r, Int32 g, Int32 b, Int32 a)
		{
			if (((((r | g) | b) | a) & -256) != 0)
			{
				r = ClampToByte32(r);
				g = ClampToByte32(g);
				b = ClampToByte32(b);
				a = ClampToByte32(a);
			}

			g = g << 8;
			b = b << 0x10;
			a = a << 0x18;

			this.packedValue = (UInt32)(((r | g) | b) | a);
		}

		public Rgba32 (Single r, Single g, Single b)
		{
			var val = new SinglePrecision.Vector4(r, g, b, 1f);
			Pack ( ref val, out this.packedValue);
		}

		public Rgba32 (Single r, Single g, Single b, Single a)
		{
			var val = new SinglePrecision.Vector4(r, g, b, a);
			Pack(ref val, out this.packedValue);
		}

		public Rgba32(SinglePrecision.Vector3 vector)
		{
			var val = new SinglePrecision.Vector4(vector.X, vector.Y, vector.Z, 1f);
			Pack(ref val, out this.packedValue);
		}

		public static Rgba32 GenerateColorFromName(string name)
		{
			System.Random random = new System.Random(name.GetHashCode());
			return new Rgba32(
				(byte)random.Next(byte.MaxValue),
				(byte)random.Next(byte.MaxValue),
				(byte)random.Next(byte.MaxValue));
		}

		

		public static Rgba32 FromNonPremultiplied(SinglePrecision.Vector4 vector)
		{
			Rgba32 color;
			var val = new SinglePrecision.Vector4(vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W, vector.W);
			Pack(ref val, out color.packedValue);
			return color;
		}

		public static Rgba32 FromNonPremultiplied(int r, int g, int b, int a)
		{
			Rgba32 color;
			r = ClampToByte64((r * a) / 0xffL);
			g = ClampToByte64((g * a) / 0xffL);
			b = ClampToByte64((b * a) / 0xffL);
			a = ClampToByte32(a);
			g = g << 8;
			b = b << 0x10;
			a = a << 0x18;
			color.packedValue = (UInt32)(((r | g) | b) | a);
			return color;
		}

		static Int32 ClampToByte32(Int32 value)
		{
			if (value < 0)
			{
				return 0;
			}

			if (value > 0xff)
			{
				return 0xff;
			}

			return value;
		}

		static Int32 ClampToByte64(Int64 value)
		{
			if (value < 0L)
			{
				return 0;
			}

			if (value > 0xffL)
			{
				return 0xff;
			}

			return (Int32)value;
		}


		public byte R
		{
			get
			{
				return unchecked((byte)this.packedValue);
			}
			set
			{
				this.packedValue = (this.packedValue & 0xffffff00) | value;
			}
		}

		public byte G
		{
			get
			{
				return unchecked((byte)(this.packedValue >> 8));
			}
			set
			{
				this.packedValue = (this.packedValue & 0xffff00ff) | ((UInt32)(value << 8));
			}
		}

		public byte B
		{
			get
			{
				return unchecked((byte)(this.packedValue >> 0x10));
			}
			set
			{
				this.packedValue = (this.packedValue & 0xff00ffff) | ((UInt32)(value << 0x10));
			}
		}

		public byte A
		{
			get
			{
				return unchecked((byte)(this.packedValue >> 0x18));
			}
			set
			{
				this.packedValue = (this.packedValue & 0xffffff) | ((UInt32)(value << 0x18));
			}
		}

		public static Rgba32 Lerp(Rgba32 value1, Rgba32 value2, Single amount)
		{
			Rgba32 color;
			UInt32 packedValue = value1.packedValue;
			UInt32 num2 = value2.packedValue;
			int num7 = (byte)packedValue;
			int num6 = (byte)(packedValue >> 8);
			int num5 = (byte)(packedValue >> 0x10);
			int num4 = (byte)(packedValue >> 0x18);
			int num15 = (byte)num2;
			int num14 = (byte)(num2 >> 8);
			int num13 = (byte)(num2 >> 0x10);
			int num12 = (byte)(num2 >> 0x18);
			int num = (int)PackUtils.PackUnsignedNormalisedValue(65536f, amount);
			int num11 = num7 + (((num15 - num7) * num) >> 0x10);
			int num10 = num6 + (((num14 - num6) * num) >> 0x10);
			int num9 = num5 + (((num13 - num5) * num) >> 0x10);
			int num8 = num4 + (((num12 - num4) * num) >> 0x10);
			color.packedValue = (UInt32)(((num11 | (num10 << 8)) | (num9 << 0x10)) | (num8 << 0x18));
			return color;
		}

		public SinglePrecision.Vector3 ToVector3()
		{
			SinglePrecision.Vector4 colourVec4;
			this.UnpackTo(out colourVec4);

			return new SinglePrecision.Vector3(colourVec4.X, colourVec4.Y, colourVec4.Z);
		}


		public static Rgba32 Desaturate(Rgba32 colour, float desaturation)
		{
			System.Diagnostics.Debug.Assert(desaturation <= 1f && desaturation >= 0f);

			var luminanceWeights = new SinglePrecision.Vector3(0.299f, 0.587f, 0.114f);

			SinglePrecision.Vector4 colourVec4;

			colour.UnpackTo(out colourVec4);

			SinglePrecision.Vector3 colourVec = new SinglePrecision.Vector3(colourVec4.X, colourVec4.Y, colourVec4.Z);


			float luminance;

			SinglePrecision.Vector3.Dot(ref luminanceWeights, ref colourVec, out luminance);

			SinglePrecision.Vector3 lumVec = new SinglePrecision.Vector3(luminance, luminance, luminance);

			SinglePrecision.Vector3.Lerp(ref colourVec, ref lumVec, desaturation, out colourVec);

			return new Rgba32(colourVec.X, colourVec.Y, colourVec.Z, colourVec4.W);
		}

		public static Rgba32 operator *(Rgba32 value, Single scale)
		{
			UInt32 num;
			Rgba32 color;
			UInt32 packedValue = value.packedValue;
			UInt32 num5 = (byte)packedValue;
			UInt32 num4 = (byte)(packedValue >> 8);
			UInt32 num3 = (byte)(packedValue >> 0x10);
			UInt32 num2 = (byte)(packedValue >> 0x18);
			scale *= 65536f;
			if (scale < 0f)
			{
				num = 0;
			}
			else if (scale > 1.677722E+07f)
			{
				num = 0xffffff;
			}
			else
			{
				num = (UInt32)scale;
			}
			num5 = (num5 * num) >> 0x10;
			num4 = (num4 * num) >> 0x10;
			num3 = (num3 * num) >> 0x10;
			num2 = (num2 * num) >> 0x10;
			if (num5 > 0xff)
			{
				num5 = 0xff;
			}
			if (num4 > 0xff)
			{
				num4 = 0xff;
			}
			if (num3 > 0xff)
			{
				num3 = 0xff;
			}
			if (num2 > 0xff)
			{
				num2 = 0xff;
			}
			color.packedValue = ((num5 | (num4 << 8)) | (num3 << 0x10)) | (num2 << 0x18);
			return color;
		}

		public static void Multiply(ref Rgba32 value, ref Single scale, out Rgba32 colour )
		{
			UInt32 num;
			UInt32 packedValue = value.packedValue;
			UInt32 num5 = (byte)packedValue;
			UInt32 num4 = (byte)(packedValue >> 8);
			UInt32 num3 = (byte)(packedValue >> 0x10);
			UInt32 num2 = (byte)(packedValue >> 0x18);
			scale *= 65536f;
			if (scale < 0f)
			{
				num = 0;
			}
			else if (scale > 1.677722E+07f)
			{
				num = 0xffffff;
			}
			else
			{
				num = (UInt32)scale;
			}
			num5 = (num5 * num) >> 0x10;
			num4 = (num4 * num) >> 0x10;
			num3 = (num3 * num) >> 0x10;
			num2 = (num2 * num) >> 0x10;
			if (num5 > 0xff)
			{
				num5 = 0xff;
			}
			if (num4 > 0xff)
			{
				num4 = 0xff;
			}
			if (num3 > 0xff)
			{
				num3 = 0xff;
			}
			if (num2 > 0xff)
			{
				num2 = 0xff;
			}
			colour.packedValue = ((num5 | (num4 << 8)) | (num3 << 0x10)) | (num2 << 0x18);
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt32 packedValue;

 
		[CLSCompliant (false)]
		public UInt32 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is Rgba32) && this.Equals((Rgba32)obj));
		}

		public Boolean Equals(Rgba32 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(Rgba32 a, Rgba32 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(Rgba32 a, Rgba32 b)
		{
			return !a.Equals(b);
		}

		public Rgba32(ref HalfPrecision.Vector4 realRgba32)
		{
			Pack(ref realRgba32, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector4 realRgba32)
		{
			Pack(ref realRgba32, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector4 realRgba32)
		{
			Unpack(this.packedValue, out realRgba32);
		}

		public Rgba32(ref SinglePrecision.Vector4 realRgba32)
		{
			Pack(ref realRgba32, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector4 realRgba32)
		{
			Pack(ref realRgba32, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector4 realRgba32)
		{
			Unpack(this.packedValue, out realRgba32);
		}

		public Rgba32(ref DoublePrecision.Vector4 realRgba32)
		{
			Pack(ref realRgba32, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector4 realRgba32)
		{
			Pack(ref realRgba32, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector4 realRgba32)
		{
			Unpack(this.packedValue, out realRgba32);
		}

		public Rgba32(ref Fixed32Precision.Vector4 realRgba32)
		{
			Pack(ref realRgba32, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector4 realRgba32)
		{
			Pack(ref realRgba32, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector4 realRgba32)
		{
			Unpack(this.packedValue, out realRgba32);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector4 realRgba32, out UInt32 packedRgba32)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba32.X, (Single)realRgba32.Y, (Single)realRgba32.Z, (Single)realRgba32.W);
			Pack(ref singleVector, out packedRgba32);
		}

		static void Unpack(UInt32 packedRgba32, out HalfPrecision.Vector4 realRgba32)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedRgba32, out singleVector);
			realRgba32 = new HalfPrecision.Vector4((Half)singleVector.X, (Half)singleVector.Y, (Half)singleVector.Z, (Half)singleVector.W);
		}
		static void Pack(ref DoublePrecision.Vector4 realRgba32, out UInt32 packedRgba32)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba32.X, (Single)realRgba32.Y, (Single)realRgba32.Z, (Single)realRgba32.W);
			Pack(ref singleVector, out packedRgba32);
		}

		static void Unpack(UInt32 packedRgba32, out DoublePrecision.Vector4 realRgba32)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedRgba32, out singleVector);
			realRgba32 = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
		}
		static void Pack(ref Fixed32Precision.Vector4 realRgba32, out UInt32 packedRgba32)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba32.X, (Single)realRgba32.Y, (Single)realRgba32.Z, (Single)realRgba32.W);
			Pack(ref singleVector, out packedRgba32);
		}

		static void Unpack(UInt32 packedRgba32, out Fixed32Precision.Vector4 realRgba32)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedRgba32, out singleVector);
			realRgba32 = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Rgba64 
		: IPackedValue<UInt64>
		, IEquatable<Rgba64>
		, IPackedReal4
	{
		public override String ToString ()
		{
			return this.packedValue.ToString ("X16", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector4 realRgba, out UInt64 packedRgba)
		{
			UInt64 r = PackUtils.PackSignedNormalised(0xffff, realRgba.X);
			UInt64 g = PackUtils.PackSignedNormalised(0xffff, realRgba.Y) << 16;
			UInt64 b = PackUtils.PackSignedNormalised(0xffff, realRgba.Z) << 32;
			UInt64 a = PackUtils.PackSignedNormalised(0xffff, realRgba.W) << 48;
			packedRgba = (((r | g) | b) | a);
		}

		static void Unpack(UInt64 packedRgba, out SinglePrecision.Vector4 realRgba)
		{
			realRgba.X = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) packedRgba);
			realRgba.Y = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) (packedRgba >> 16));
			realRgba.Z = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) (packedRgba >> 32));
			realRgba.W = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) (packedRgba >> 48));
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt64 packedValue;

 
		[CLSCompliant (false)]
		public UInt64 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is Rgba64) && this.Equals((Rgba64)obj));
		}

		public Boolean Equals(Rgba64 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(Rgba64 a, Rgba64 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(Rgba64 a, Rgba64 b)
		{
			return !a.Equals(b);
		}

		public Rgba64(ref HalfPrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		public Rgba64(ref SinglePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		public Rgba64(ref DoublePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		public Rgba64(ref Fixed32Precision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector4 realRgba, out UInt64 packedRgba)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
			Pack(ref singleVector, out packedRgba);
		}

		static void Unpack(UInt64 packedRgba, out HalfPrecision.Vector4 realRgba)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedRgba, out singleVector);
			realRgba = new HalfPrecision.Vector4((Half)singleVector.X, (Half)singleVector.Y, (Half)singleVector.Z, (Half)singleVector.W);
		}
		static void Pack(ref DoublePrecision.Vector4 realRgba, out UInt64 packedRgba)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
			Pack(ref singleVector, out packedRgba);
		}

		static void Unpack(UInt64 packedRgba, out DoublePrecision.Vector4 realRgba)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedRgba, out singleVector);
			realRgba = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
		}
		static void Pack(ref Fixed32Precision.Vector4 realRgba, out UInt64 packedRgba)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
			Pack(ref singleVector, out packedRgba);
		}

		static void Unpack(UInt64 packedRgba, out Fixed32Precision.Vector4 realRgba)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedRgba, out singleVector);
			realRgba = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
		}
	}
	// 2 bit alpha
	[StructLayout (LayoutKind.Sequential)]
	public struct Rgba_10_10_10_2 
		: IPackedValue<UInt32>
		, IEquatable<Rgba_10_10_10_2>
		, IPackedReal4
	{

		public override String ToString ()
		{
			return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector4 realRgba, out UInt32 packedRgba)
		{
			UInt32 r = PackUtils.PackUnsignedNormalisedValue (0xffff, realRgba.X);
			UInt32 g = PackUtils.PackUnsignedNormalisedValue (0xffff, realRgba.Y) << 10;
			UInt32 b = PackUtils.PackUnsignedNormalisedValue (0xffff, realRgba.Z) << 20;
			UInt32 a = PackUtils.PackUnsignedNormalisedValue (0xffff, realRgba.W) << 30;
			packedRgba = ((r | g) | b) | a;
		}

		static void Unpack(UInt32 packedRgba, out SinglePrecision.Vector4 realRgba)
		{
			realRgba.X = PackUtils.UnpackUnsignedNormalisedValue (0xffff, packedRgba);
			realRgba.Y = PackUtils.UnpackUnsignedNormalisedValue (0xffff, (UInt32)(packedRgba >> 10));
			realRgba.Z = PackUtils.UnpackUnsignedNormalisedValue (0xffff, (UInt32)(packedRgba >> 20));
			realRgba.W = PackUtils.UnpackUnsignedNormalisedValue (0xffff, (UInt32)(packedRgba >> 30));
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt32 packedValue;

 
		[CLSCompliant (false)]
		public UInt32 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is Rgba_10_10_10_2) && this.Equals((Rgba_10_10_10_2)obj));
		}

		public Boolean Equals(Rgba_10_10_10_2 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(Rgba_10_10_10_2 a, Rgba_10_10_10_2 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(Rgba_10_10_10_2 a, Rgba_10_10_10_2 b)
		{
			return !a.Equals(b);
		}

		public Rgba_10_10_10_2(ref HalfPrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		public Rgba_10_10_10_2(ref SinglePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		public Rgba_10_10_10_2(ref DoublePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		public Rgba_10_10_10_2(ref Fixed32Precision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector4 realRgba)
		{
			Pack(ref realRgba, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector4 realRgba)
		{
			Unpack(this.packedValue, out realRgba);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector4 realRgba, out UInt32 packedRgba)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
			Pack(ref singleVector, out packedRgba);
		}

		static void Unpack(UInt32 packedRgba, out HalfPrecision.Vector4 realRgba)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedRgba, out singleVector);
			realRgba = new HalfPrecision.Vector4((Half)singleVector.X, (Half)singleVector.Y, (Half)singleVector.Z, (Half)singleVector.W);
		}
		static void Pack(ref DoublePrecision.Vector4 realRgba, out UInt32 packedRgba)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
			Pack(ref singleVector, out packedRgba);
		}

		static void Unpack(UInt32 packedRgba, out DoublePrecision.Vector4 realRgba)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedRgba, out singleVector);
			realRgba = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
		}
		static void Pack(ref Fixed32Precision.Vector4 realRgba, out UInt32 packedRgba)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
			Pack(ref singleVector, out packedRgba);
		}

		static void Unpack(UInt32 packedRgba, out Fixed32Precision.Vector4 realRgba)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedRgba, out singleVector);
			realRgba = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Short2 
		: IPackedValue<UInt32>
		, IEquatable<Short2>
		, IPackedReal2
	{
		public override String ToString ()
		{
			return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector2 realXy, out UInt32 packedXy)
		{
			UInt32 x = PackUtils.PackSigned (0xffff, realXy.X);
			UInt32 y = PackUtils.PackSigned (0xffff, realXy.Y) << 16;
			packedXy = (x | y);
		}

		static void Unpack(UInt32 packedXy, out SinglePrecision.Vector2 realXy)
		{
			realXy.X = (Int16) packedXy;
			realXy.Y = (Int16) (packedXy >> 16);
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt32 packedValue;

 
		[CLSCompliant (false)]
		public UInt32 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is Short2) && this.Equals((Short2)obj));
		}

		public Boolean Equals(Short2 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(Short2 a, Short2 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(Short2 a, Short2 b)
		{
			return !a.Equals(b);
		}

		public Short2(ref HalfPrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector2 realXy)
		{
			Unpack(this.packedValue, out realXy);
		}

		public Short2(ref SinglePrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector2 realXy)
		{
			Unpack(this.packedValue, out realXy);
		}

		public Short2(ref DoublePrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector2 realXy)
		{
			Unpack(this.packedValue, out realXy);
		}

		public Short2(ref Fixed32Precision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector2 realXy)
		{
			Pack(ref realXy, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector2 realXy)
		{
			Unpack(this.packedValue, out realXy);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector2 realXy, out UInt32 packedXy)
		{
			SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
			Pack(ref singleVector, out packedXy);
		}

		static void Unpack(UInt32 packedXy, out HalfPrecision.Vector2 realXy)
		{
			SinglePrecision.Vector2 singleVector;
			Unpack(packedXy, out singleVector);
			realXy = new HalfPrecision.Vector2((Half)singleVector.X, (Half)singleVector.Y);
		}
		static void Pack(ref DoublePrecision.Vector2 realXy, out UInt32 packedXy)
		{
			SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
			Pack(ref singleVector, out packedXy);
		}

		static void Unpack(UInt32 packedXy, out DoublePrecision.Vector2 realXy)
		{
			SinglePrecision.Vector2 singleVector;
			Unpack(packedXy, out singleVector);
			realXy = new DoublePrecision.Vector2((Double)singleVector.X, (Double)singleVector.Y);
		}
		static void Pack(ref Fixed32Precision.Vector2 realXy, out UInt32 packedXy)
		{
			SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
			Pack(ref singleVector, out packedXy);
		}

		static void Unpack(UInt32 packedXy, out Fixed32Precision.Vector2 realXy)
		{
			SinglePrecision.Vector2 singleVector;
			Unpack(packedXy, out singleVector);
			realXy = new Fixed32Precision.Vector2((Fixed32)singleVector.X, (Fixed32)singleVector.Y);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Short4 
		: IPackedValue<UInt64>
		, IEquatable<Short4>
		, IPackedReal4
	{
		public override String ToString ()
		{
			return this.packedValue.ToString ("X16", CultureInfo.InvariantCulture);
		}

		static void Pack(ref SinglePrecision.Vector4 realXyzw, out UInt64 packedXyzw)
		{
			UInt64 x = PackUtils.PackSigned(0xffff, realXyzw.X);
			UInt64 y = PackUtils.PackSigned(0xffff, realXyzw.Y) << 16;
			UInt64 z = PackUtils.PackSigned(0xffff, realXyzw.Z) << 32;
			UInt64 w = PackUtils.PackSigned(0xffff, realXyzw.W) << 48;
			packedXyzw = (((x | y) | z) | w);
		}

		static void Unpack(UInt64 packedXyzw, out SinglePrecision.Vector4 realXyzw)
		{
			realXyzw.X = (Int16) packedXyzw;
			realXyzw.Y = (Int16) (packedXyzw >> 16);
			realXyzw.Z = (Int16) (packedXyzw >> 32);
			realXyzw.W = (Int16) (packedXyzw >> 48);
		}

		// GENERATED CODE ----------------------------------------------------------------
		UInt64 packedValue;

 
		[CLSCompliant (false)]
		public UInt64 PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		public override Int32 GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		public override Boolean Equals(Object obj)
		{
			return ((obj is Short4) && this.Equals((Short4)obj));
		}

		public Boolean Equals(Short4 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		public static Boolean operator ==(Short4 a, Short4 b)
		{
			return a.Equals(b);
		}

		public static Boolean operator !=(Short4 a, Short4 b)
		{
			return !a.Equals(b);
		}

		public Short4(ref HalfPrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref HalfPrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out HalfPrecision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		public Short4(ref SinglePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref SinglePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out SinglePrecision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		public Short4(ref DoublePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref DoublePrecision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out DoublePrecision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		public Short4(ref Fixed32Precision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void PackFrom(ref Fixed32Precision.Vector4 realXyzw)
		{
			Pack(ref realXyzw, out this.packedValue);
		}

		public void UnpackTo(out Fixed32Precision.Vector4 realXyzw)
		{
			Unpack(this.packedValue, out realXyzw);
		}

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
		static void Pack(ref HalfPrecision.Vector4 realXyzw, out UInt64 packedXyzw)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
			Pack(ref singleVector, out packedXyzw);
		}

		static void Unpack(UInt64 packedXyzw, out HalfPrecision.Vector4 realXyzw)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedXyzw, out singleVector);
			realXyzw = new HalfPrecision.Vector4((Half)singleVector.X, (Half)singleVector.Y, (Half)singleVector.Z, (Half)singleVector.W);
		}
		static void Pack(ref DoublePrecision.Vector4 realXyzw, out UInt64 packedXyzw)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
			Pack(ref singleVector, out packedXyzw);
		}

		static void Unpack(UInt64 packedXyzw, out DoublePrecision.Vector4 realXyzw)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedXyzw, out singleVector);
			realXyzw = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
		}
		static void Pack(ref Fixed32Precision.Vector4 realXyzw, out UInt64 packedXyzw)
		{
			SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
			Pack(ref singleVector, out packedXyzw);
		}

		static void Unpack(UInt64 packedXyzw, out Fixed32Precision.Vector4 realXyzw)
		{
			SinglePrecision.Vector4 singleVector;
			Unpack(packedXyzw, out singleVector);
			realXyzw = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
		}
	}
}


namespace Sungiant.Abacus.Int32Precision
{
	[StructLayout (LayoutKind.Sequential)]
	public struct Point2 
		: IEquatable<Point2>
	{
		public Int32 X;
		public Int32 Y;

		public Point2 (Int32 x, Int32 y)
		{
			this.X = x;
			this.Y = y;
		}

		public Boolean Equals (Point2 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;

			if (obj is Point2)
			{
				flag = this.Equals ((Point2)obj);
			}

			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.X.GetHashCode () + this.Y.GetHashCode ());
		}

		public override String ToString ()
		{
			return String.Format ("{{X:{0} Y:{1}}}", this.X, this.Y );
		}
		
		static Point2 zero;
		
		public static Point2 Zero
		{
			get
			{
				return zero;
			}
		}
		
		static Point2 ()
		{
			zero = new Point2 ();
		}

		public static Boolean operator == (Point2 a, Point2 b)
		{
			return a.Equals (b);
		}

		public static Boolean operator != (Point2 a, Point2 b)
		{
			if (a.X == b.X)
			{
				return (a.Y != b.Y);
			}

			return true;
		}	
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Point3 
		: IEquatable<Point3>
	{
		public Int32 X;
		public Int32 Y;
		public Int32 Z;

		static Point3 zero;
		static Point3 one;
		static Point3 unitX;
		static Point3 unitY;
		static Point3 unitZ;
		static Point3 up;
		static Point3 down;
		static Point3 right;
		static Point3 left;
		static Point3 forward;
		static Point3 backward;

		public static Point3 Zero
		{
			get
			{
				return zero;
			}
		}

		public static Point3 One
		{
			get
			{
				return one;
			}
		}

		public static Point3 UnitX
		{
			get
			{
				return unitX;
			}
		}

		public static Point3 UnitY
		{
			get
			{
				return unitY;
			}
		}

		public static Point3 UnitZ
		{
			get
			{
				return unitZ;
			}
		}

		public static Point3 Up
		{
			get
			{
				return up;
			}
		}

		public static Point3 Down
		{
			get
			{
				return down;
			}
		}

		public static Point3 Right
		{
			get
			{
				return right;
			}
		}

		public static Point3 Left
		{
			get
			{
				return left;
			}
		}

		public static Point3 Forward
		{
			get
			{
				return forward;
			}
		}

		public static Point3 Backward
		{
			get
			{
				return backward;
			}
		}

		public Point3(Int32 x, Int32 y, Int32 z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public bool Equals(Point3 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y) && (this.Z == other.Z));
		}

		public override bool Equals(Object obj)
		{
			bool flag = false;
			if (obj is Point3)
			{
				flag = this.Equals((Point3)obj);
			}
			return flag;
		}

		public override int GetHashCode()
		{
			return (this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode());
		}

		public override string ToString()
		{
			return string.Format("{{X:{0} Y:{1} Z:{2}}}", this.X, this.Y, this.Z );
		}

		public static bool operator ==(Point3 a, Point3 b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Point3 a, Point3 b)
		{
			if (a.X == b.X)
			{
				if (a.Y == b.Y)
				{
					return (a.Z != b.Z);
				}
			}
			return true;
		}

		static Point3()
		{
			zero = new Point3();
			one = new Point3(1, 1, 1);
			unitX = new Point3(1, 0, 0);
			unitY = new Point3(0, 1, 0);
			unitZ = new Point3(0, 0, 1);
			up = new Point3(0, 1, 0);
			down = new Point3(0, -1, 0);
			right = new Point3(1, 0, 0);
			left = new Point3(-1, 0, 0);
			forward = new Point3(0, 0, -1);
			backward = new Point3(0, 0, 1);
		}

		public static Point3 operator +(Point3 value)
		{
			Point3 point = Point3.Zero;
			point.X = +value.X;
			point.Y = +value.Y;
			point.Z = +value.Z;
			return point;
		}

		public static Point3 operator -(Point3 value)
		{
			Point3 point = Point3.Zero;
			point.X = -value.X;
			point.Y = -value.Y;
			point.Z = -value.Z;
			return point;
		}

		public static Point3 operator +(Point3 value1, Point3 value2)
		{
			Point3 point = Point3.Zero;
			point.X = value1.X + value2.X;
			point.Y = value1.Y + value2.Y;
			point.Z = value1.Z + value2.Z;
			return point;
		}

		public static Point3 operator -(Point3 value1, Point3 value2)
		{
			Point3 point = Point3.Zero;
			point.X = value1.X - value2.X;
			point.Y = value1.Y - value2.Y;
			point.Z = value1.Z - value2.Z;
			return point;
		}

		public static Point3 operator *(Point3 value1, Point3 value2)
		{
			Point3 point = Point3.Zero;
			point.X = value1.X * value2.X;
			point.Y = value1.Y * value2.Y;
			point.Z = value1.Z * value2.Z;
			return point;
		}

		public static Point3 operator *(Point3 value, int scaleFactor)
		{
			Point3 point = Point3.Zero;
			point.X = value.X * scaleFactor;
			point.Y = value.Y * scaleFactor;
			point.Z = value.Z * scaleFactor;
			return point;
		}

		public static Point3 operator *(int scaleFactor, Point3 value)
		{
			Point3 point = Point3.Zero;
			point.X = value.X * scaleFactor;
			point.Y = value.Y * scaleFactor;
			point.Z = value.Z * scaleFactor;
			return point;
		}

		public static Point3 operator /(Point3 value1, Point3 value2)
		{
			Point3 point = Point3.Zero;
			point.X = value1.X / value2.X;
			point.Y = value1.Y / value2.Y;
			point.Z = value1.Z / value2.Z;
			return point;
		}

		public static Point3 operator /(Point3 value, int divider)
		{
			Point3 point = Point3.Zero;
			point.X = value.X / divider;
			point.Y = value.Y / divider;
			point.Z = value.Z / divider;
			return point;
		}
	}
}

namespace Sungiant.Abacus.Int64Precision
{
	[StructLayout (LayoutKind.Sequential)]
	public struct Point2 
		: IEquatable<Point2>
	{
		public Int64 X;
		public Int64 Y;

		public Point2 (Int64 x, Int64 y)
		{
			this.X = x;
			this.Y = y;
		}

		public Boolean Equals (Point2 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;

			if (obj is Point2)
			{
				flag = this.Equals ((Point2)obj);
			}

			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.X.GetHashCode () + this.Y.GetHashCode ());
		}

		public override String ToString ()
		{
			return String.Format ("{{X:{0} Y:{1}}}", this.X, this.Y );
		}
		
		static Point2 zero;
		
		public static Point2 Zero
		{
			get
			{
				return zero;
			}
		}
		
		static Point2 ()
		{
			zero = new Point2 ();
		}

		public static Boolean operator == (Point2 a, Point2 b)
		{
			return a.Equals (b);
		}

		public static Boolean operator != (Point2 a, Point2 b)
		{
			if (a.X == b.X)
			{
				return (a.Y != b.Y);
			}

			return true;
		}	
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Point3 
		: IEquatable<Point3>
	{
		public Int64 X;
		public Int64 Y;
		public Int64 Z;

		static Point3 zero;
		static Point3 one;
		static Point3 unitX;
		static Point3 unitY;
		static Point3 unitZ;
		static Point3 up;
		static Point3 down;
		static Point3 right;
		static Point3 left;
		static Point3 forward;
		static Point3 backward;

		public static Point3 Zero
		{
			get
			{
				return zero;
			}
		}

		public static Point3 One
		{
			get
			{
				return one;
			}
		}

		public static Point3 UnitX
		{
			get
			{
				return unitX;
			}
		}

		public static Point3 UnitY
		{
			get
			{
				return unitY;
			}
		}

		public static Point3 UnitZ
		{
			get
			{
				return unitZ;
			}
		}

		public static Point3 Up
		{
			get
			{
				return up;
			}
		}

		public static Point3 Down
		{
			get
			{
				return down;
			}
		}

		public static Point3 Right
		{
			get
			{
				return right;
			}
		}

		public static Point3 Left
		{
			get
			{
				return left;
			}
		}

		public static Point3 Forward
		{
			get
			{
				return forward;
			}
		}

		public static Point3 Backward
		{
			get
			{
				return backward;
			}
		}

		public Point3(Int64 x, Int64 y, Int64 z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public bool Equals(Point3 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y) && (this.Z == other.Z));
		}

		public override bool Equals(Object obj)
		{
			bool flag = false;
			if (obj is Point3)
			{
				flag = this.Equals((Point3)obj);
			}
			return flag;
		}

		public override int GetHashCode()
		{
			return (this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode());
		}

		public override string ToString()
		{
			return string.Format("{{X:{0} Y:{1} Z:{2}}}", this.X, this.Y, this.Z );
		}

		public static bool operator ==(Point3 a, Point3 b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Point3 a, Point3 b)
		{
			if (a.X == b.X)
			{
				if (a.Y == b.Y)
				{
					return (a.Z != b.Z);
				}
			}
			return true;
		}

		static Point3()
		{
			zero = new Point3();
			one = new Point3(1, 1, 1);
			unitX = new Point3(1, 0, 0);
			unitY = new Point3(0, 1, 0);
			unitZ = new Point3(0, 0, 1);
			up = new Point3(0, 1, 0);
			down = new Point3(0, -1, 0);
			right = new Point3(1, 0, 0);
			left = new Point3(-1, 0, 0);
			forward = new Point3(0, 0, -1);
			backward = new Point3(0, 0, 1);
		}

		public static Point3 operator +(Point3 value)
		{
			Point3 point = Point3.Zero;
			point.X = +value.X;
			point.Y = +value.Y;
			point.Z = +value.Z;
			return point;
		}

		public static Point3 operator -(Point3 value)
		{
			Point3 point = Point3.Zero;
			point.X = -value.X;
			point.Y = -value.Y;
			point.Z = -value.Z;
			return point;
		}

		public static Point3 operator +(Point3 value1, Point3 value2)
		{
			Point3 point = Point3.Zero;
			point.X = value1.X + value2.X;
			point.Y = value1.Y + value2.Y;
			point.Z = value1.Z + value2.Z;
			return point;
		}

		public static Point3 operator -(Point3 value1, Point3 value2)
		{
			Point3 point = Point3.Zero;
			point.X = value1.X - value2.X;
			point.Y = value1.Y - value2.Y;
			point.Z = value1.Z - value2.Z;
			return point;
		}

		public static Point3 operator *(Point3 value1, Point3 value2)
		{
			Point3 point = Point3.Zero;
			point.X = value1.X * value2.X;
			point.Y = value1.Y * value2.Y;
			point.Z = value1.Z * value2.Z;
			return point;
		}

		public static Point3 operator *(Point3 value, int scaleFactor)
		{
			Point3 point = Point3.Zero;
			point.X = value.X * scaleFactor;
			point.Y = value.Y * scaleFactor;
			point.Z = value.Z * scaleFactor;
			return point;
		}

		public static Point3 operator *(int scaleFactor, Point3 value)
		{
			Point3 point = Point3.Zero;
			point.X = value.X * scaleFactor;
			point.Y = value.Y * scaleFactor;
			point.Z = value.Z * scaleFactor;
			return point;
		}

		public static Point3 operator /(Point3 value1, Point3 value2)
		{
			Point3 point = Point3.Zero;
			point.X = value1.X / value2.X;
			point.Y = value1.Y / value2.Y;
			point.Z = value1.Z / value2.Z;
			return point;
		}

		public static Point3 operator /(Point3 value, int divider)
		{
			Point3 point = Point3.Zero;
			point.X = value.X / divider;
			point.Y = value.Y / divider;
			point.Z = value.Z / divider;
			return point;
		}
	}
}


namespace Sungiant.Abacus.HalfPrecision
{
	public static class GaussianElimination
	{

	}
	public class GjkDistance
	{
		public GjkDistance ()
		{
			for (Int32 i = 0; i < 0x10; i++)
			{
				this.det [i] = new Half[4];
			}
		}

		public Boolean AddSupportPoint (ref Vector3 newPoint)
		{
			Int32 index = (BitsToIndices [this.simplexBits ^ 15] & 7) - 1;

			this.y [index] = newPoint;
			this.yLengthSq [index] = newPoint.LengthSquared ();

			for (Int32 i = BitsToIndices[this.simplexBits]; i != 0; i = i >> 3)
			{
				Int32 num2 = (i & 7) - 1;
				Vector3 vector = this.y [num2] - newPoint;

				this.edges [num2] [index] = vector;
				this.edges [index] [num2] = -vector;
				this.edgeLengthSq [index] [num2] = this.edgeLengthSq [num2] [index] = vector.LengthSquared ();
			}

			this.UpdateDeterminant (index);

			return this.UpdateSimplex (index);
		}

		public void Reset ()
		{
			Half zero = 0;

			this.simplexBits = 0;
			this.maxLengthSq = zero;
		}

		public Vector3 ClosestPoint
		{
			get { return this.closestPoint; }
		}
		
		public Boolean FullSimplex
		{
			get { return (this.simplexBits == 15); }
		}
		
		public Half MaxLengthSquared
		{
			get { return this.maxLengthSq; }
		}

		Vector3 closestPoint;
		Half[][] det = new Half[0x10][];
		Half[][] edgeLengthSq = new Half[][] { new Half[4], new Half[4], new Half[4], new Half[4] };
		Vector3[][] edges = new Vector3[][] { new Vector3[4], new Vector3[4], new Vector3[4], new Vector3[4] };
		Half maxLengthSq;
		Int32 simplexBits;
		Vector3[] y = new Vector3[4];
		Half[] yLengthSq = new Half[4];

		static Int32[] BitsToIndices = new Int32[] { 0, 1, 2, 0x11, 3, 0x19, 0x1a, 0xd1, 4, 0x21, 0x22, 0x111, 0x23, 0x119, 0x11a, 0x8d1 };

		Vector3 ComputeClosestPoint ()
		{
			Half fzero; RealMaths.Zero(out fzero);

			Half num3 = fzero;
			Vector3 zero = Vector3.Zero;

			this.maxLengthSq = fzero;

			for (Int32 i = BitsToIndices[this.simplexBits]; i != 0; i = i >> 3)
			{
				Int32 index = (i & 7) - 1;
				Half num4 = this.det [this.simplexBits] [index];

				num3 += num4;
				zero += (Vector3)(this.y [index] * num4);

				this.maxLengthSq = RealMaths.Max (this.maxLengthSq, this.yLengthSq [index]);
			}

			return (Vector3)(zero / num3);
		}

		Boolean IsSatisfiesRule (Int32 xBits, Int32 yBits)
		{
			Half fzero; RealMaths.Zero(out fzero);

			for (Int32 i = BitsToIndices[yBits]; i != 0; i = i >> 3)
			{
				Int32 index = (i & 7) - 1;
				Int32 num3 = ((Int32)1) << index;

				if ((num3 & xBits) != 0)
				{
					if (this.det [xBits] [index] <= fzero)
					{
						return false;
					}
				}
				else if (this.det [xBits | num3] [index] > fzero)
				{
					return false;
				}
			}

			return true;
		}

		void UpdateDeterminant (Int32 xmIdx)
		{
			Half fone; RealMaths.One(out fone);
			Int32 index = ((Int32)1) << xmIdx;

			this.det [index] [xmIdx] = fone;

			Int32 num14 = BitsToIndices [this.simplexBits];
			Int32 num8 = num14;

			for (Int32 i = 0; num8 != 0; i++)
			{
				Int32 num = (num8 & 7) - 1;
				Int32 num12 = ((int)1) << num;
				Int32 num6 = num12 | index;

				this.det [num6] [num] = Dot (ref this.edges [xmIdx] [num], ref this.y [xmIdx]);
				this.det [num6] [xmIdx] = Dot (ref this.edges [num] [xmIdx], ref this.y [num]);

				Int32 num11 = num14;

				for (Int32 j = 0; j < i; j++)
				{
					int num3 = (num11 & 7) - 1;
					int num5 = ((int)1) << num3;
					int num9 = num6 | num5;
					int num4 = (this.edgeLengthSq [num] [num3] < this.edgeLengthSq [xmIdx] [num3]) ? num : xmIdx;

					this.det [num9] [num3] = 
						(this.det [num6] [num] * Dot (ref this.edges [num4] [num3], ref this.y [num])) + 
						(this.det [num6] [xmIdx] * Dot (ref this.edges [num4] [num3], ref this.y [xmIdx]));

					num4 = (this.edgeLengthSq [num3] [num] < this.edgeLengthSq [xmIdx] [num]) ? num3 : xmIdx;

					this.det [num9] [num] = 
						(this.det [num5 | index] [num3] * Dot (ref this.edges [num4] [num], ref this.y [num3])) + 
						(this.det [num5 | index] [xmIdx] * Dot (ref this.edges [num4] [num], ref this.y [xmIdx]));

					num4 = (this.edgeLengthSq [num] [xmIdx] < this.edgeLengthSq [num3] [xmIdx]) ? num : num3;

					this.det [num9] [xmIdx] = 
						(this.det [num12 | num5] [num3] * Dot (ref this.edges [num4] [xmIdx], ref this.y [num3])) + 
						(this.det [num12 | num5] [num] * Dot (ref this.edges [num4] [xmIdx], ref this.y [num]));

					num11 = num11 >> 3;
				}

				num8 = num8 >> 3;
			}

			if ((this.simplexBits | index) == 15)
			{
				int num2 = 
					(this.edgeLengthSq [1] [0] < this.edgeLengthSq [2] [0]) ? 
					((this.edgeLengthSq [1] [0] < this.edgeLengthSq [3] [0]) ? 1 : 3) : 
					((this.edgeLengthSq [2] [0] < this.edgeLengthSq [3] [0]) ? 2 : 3);

				this.det [15] [0] = 
					((this.det [14] [1] * Dot (ref this.edges [num2] [0], ref this.y [1])) + 
					(this.det [14] [2] * Dot (ref this.edges [num2] [0], ref this.y [2]))) + 
					(this.det [14] [3] * Dot (ref this.edges [num2] [0], ref this.y [3]));

				num2 = 
					(this.edgeLengthSq [0] [1] < this.edgeLengthSq [2] [1]) ? 
					((this.edgeLengthSq [0] [1] < this.edgeLengthSq [3] [1]) ? 0 : 3) : 
					((this.edgeLengthSq [2] [1] < this.edgeLengthSq [3] [1]) ? 2 : 3);

				this.det [15] [1] = 
					((this.det [13] [0] * Dot (ref this.edges [num2] [1], ref this.y [0])) + 
				    (this.det [13] [2] * Dot (ref this.edges [num2] [1], ref this.y [2]))) + 
					(this.det [13] [3] * Dot (ref this.edges [num2] [1], ref this.y [3]));

				num2 = 
					(this.edgeLengthSq [0] [2] < this.edgeLengthSq [1] [2]) ? 
					((this.edgeLengthSq [0] [2] < this.edgeLengthSq [3] [2]) ? 0 : 3) : 
					((this.edgeLengthSq [1] [2] < this.edgeLengthSq [3] [2]) ? 1 : 3);

				this.det [15] [2] = 
					((this.det [11] [0] * Dot (ref this.edges [num2] [2], ref this.y [0])) + 
					(this.det [11] [1] * Dot (ref this.edges [num2] [2], ref this.y [1]))) + 
					(this.det [11] [3] * Dot (ref this.edges [num2] [2], ref this.y [3]));

				num2 = 
					(this.edgeLengthSq [0] [3] < this.edgeLengthSq [1] [3]) ? 
					((this.edgeLengthSq [0] [3] < this.edgeLengthSq [2] [3]) ? 0 : 2) : 
					((this.edgeLengthSq [1] [3] < this.edgeLengthSq [2] [3]) ? 1 : 2);

				this.det [15] [3] = 
					((this.det [7] [0] * Dot (ref this.edges [num2] [3], ref this.y [0])) + 
					(this.det [7] [1] * Dot (ref this.edges [num2] [3], ref this.y [1]))) + 
					(this.det [7] [2] * Dot (ref this.edges [num2] [3], ref this.y [2]));
			}
		}

		Boolean UpdateSimplex (Int32 newIndex)
		{
			Int32 yBits = this.simplexBits | (((Int32)1) << newIndex);

			Int32 xBits = ((Int32)1) << newIndex;

			for (Int32 i = this.simplexBits; i != 0; i--)
			{
				if (((i & yBits) == i) && this.IsSatisfiesRule (i | xBits, yBits))
				{
					this.simplexBits = i | xBits;
					this.closestPoint = this.ComputeClosestPoint ();

					return true;
				}
			}

			Boolean flag = false;

			if (this.IsSatisfiesRule (xBits, yBits))
			{
				this.simplexBits = xBits;
				this.closestPoint = this.y [newIndex];
				this.maxLengthSq = this.yLengthSq [newIndex];

				flag = true;
			}

			return flag;
		}

		static Half Dot (ref Vector3 a, ref Vector3 b)
		{
			return (((a.X * b.X) + (a.Y * b.Y)) + (a.Z * b.Z));
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct BoundingBox 
		: IEquatable<BoundingBox>
	{
		public const int CornerCount = 8;
		public Vector3 Min;
		public Vector3 Max;

		public Vector3[] GetCorners ()
		{
			return new Vector3[] { new Vector3 (this.Min.X, this.Max.Y, this.Max.Z), new Vector3 (this.Max.X, this.Max.Y, this.Max.Z), new Vector3 (this.Max.X, this.Min.Y, this.Max.Z), new Vector3 (this.Min.X, this.Min.Y, this.Max.Z), new Vector3 (this.Min.X, this.Max.Y, this.Min.Z), new Vector3 (this.Max.X, this.Max.Y, this.Min.Z), new Vector3 (this.Max.X, this.Min.Y, this.Min.Z), new Vector3 (this.Min.X, this.Min.Y, this.Min.Z) };
		}

		public BoundingBox (Vector3 min, Vector3 max)
		{
			this.Min = min;
			this.Max = max;
		}

		public Boolean Equals (BoundingBox other)
		{
			return ((this.Min == other.Min) && (this.Max == other.Max));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is BoundingBox) {
				flag = this.Equals ((BoundingBox)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Min.GetHashCode () + this.Max.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Min:{0} Max:{1}}}", new Object[] { this.Min.ToString (), this.Max.ToString () });
		}

		public static void CreateMerged (ref BoundingBox original, ref BoundingBox additional, out BoundingBox result)
		{
			Vector3 vector;
			Vector3 vector2;
			Vector3.Min (ref original.Min, ref additional.Min, out vector2);
			Vector3.Max (ref original.Max, ref additional.Max, out vector);
			result.Min = vector2;
			result.Max = vector;
		}

		public static void CreateFromSphere (ref BoundingSphere sphere, out BoundingBox result)
		{
			result.Min.X = sphere.Center.X - sphere.Radius;
			result.Min.Y = sphere.Center.Y - sphere.Radius;
			result.Min.Z = sphere.Center.Z - sphere.Radius;
			result.Max.X = sphere.Center.X + sphere.Radius;
			result.Max.Y = sphere.Center.Y + sphere.Radius;
			result.Max.Z = sphere.Center.Z + sphere.Radius;
		}

		public static BoundingBox CreateFromPoints (IEnumerable<Vector3> points)
		{
			if (points == null) {
				throw new ArgumentNullException ();
			}
			Boolean flag = false;
			Vector3 vector3 = new Vector3 (Half.MaxValue);
			Vector3 vector2 = new Vector3 (Half.MinValue);
			foreach (Vector3 vector in points) {
				Vector3 vector4 = vector;
				Vector3.Min (ref vector3, ref vector4, out vector3);
				Vector3.Max (ref vector2, ref vector4, out vector2);
				flag = true;
			}
			if (!flag) {
				throw new ArgumentException ("BoundingBoxZeroPoints");
			}
			return new BoundingBox (vector3, vector2);
		}

		public Boolean Intersects (ref BoundingBox box)
		{
			if ((this.Max.X < box.Min.X) || (this.Min.X > box.Max.X)) {
				return false;
			}
			if ((this.Max.Y < box.Min.Y) || (this.Min.Y > box.Max.Y)) {
				return false;
			}
			return ((this.Max.Z >= box.Min.Z) && (this.Min.Z <= box.Max.Z));
		}

		public Boolean Intersects (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			return frustum.Intersects (ref this);
		}

		public PlaneIntersectionType Intersects (ref Plane plane)
		{
			Half zero = 0;

			Vector3 vector;
			Vector3 vector2;
			vector2.X = (plane.Normal.X >= zero) ? this.Min.X : this.Max.X;
			vector2.Y = (plane.Normal.Y >= zero) ? this.Min.Y : this.Max.Y;
			vector2.Z = (plane.Normal.Z >= zero) ? this.Min.Z : this.Max.Z;
			vector.X = (plane.Normal.X >= zero) ? this.Max.X : this.Min.X;
			vector.Y = (plane.Normal.Y >= zero) ? this.Max.Y : this.Min.Y;
			vector.Z = (plane.Normal.Z >= zero) ? this.Max.Z : this.Min.Z;
			Half num = ((plane.Normal.X * vector2.X) + (plane.Normal.Y * vector2.Y)) + (plane.Normal.Z * vector2.Z);
			if ((num + plane.D) > zero) {
				return PlaneIntersectionType.Front;
			}
			num = ((plane.Normal.X * vector.X) + (plane.Normal.Y * vector.Y)) + (plane.Normal.Z * vector.Z);
			if ((num + plane.D) < zero) {
				return PlaneIntersectionType.Back;
			}
			return PlaneIntersectionType.Intersecting;
		}

		public Half? Intersects (ref Ray ray)
		{
			Half epsilon; RealMaths.Epsilon(out epsilon);

			Half zero = 0;
			Half one = 1;

			Half num = zero;
			Half maxValue = Half.MaxValue;
			if (RealMaths.Abs (ray.Direction.X) < epsilon) {
				if ((ray.Position.X < this.Min.X) || (ray.Position.X > this.Max.X)) {
					return null;
				}
			} else {
				Half num11 = one / ray.Direction.X;
				Half num8 = (this.Min.X - ray.Position.X) * num11;
				Half num7 = (this.Max.X - ray.Position.X) * num11;
				if (num8 > num7) {
					Half num14 = num8;
					num8 = num7;
					num7 = num14;
				}
				num = RealMaths.Max (num8, num);
				maxValue = RealMaths.Min (num7, maxValue);
				if (num > maxValue) {
					return null;
				}
			}
			if (RealMaths.Abs (ray.Direction.Y) < epsilon) {
				if ((ray.Position.Y < this.Min.Y) || (ray.Position.Y > this.Max.Y)) {
					return null;
				}
			} else {
				Half num10 = one / ray.Direction.Y;
				Half num6 = (this.Min.Y - ray.Position.Y) * num10;
				Half num5 = (this.Max.Y - ray.Position.Y) * num10;
				if (num6 > num5) {
					Half num13 = num6;
					num6 = num5;
					num5 = num13;
				}
				num = RealMaths.Max (num6, num);
				maxValue = RealMaths.Min (num5, maxValue);
				if (num > maxValue) {
					return null;
				}
			}
			

			if (RealMaths.Abs (ray.Direction.Z) < epsilon) {
				if ((ray.Position.Z < this.Min.Z) || (ray.Position.Z > this.Max.Z)) {
					return null;
				}
			} else {
				Half num9 = one / ray.Direction.Z;
				Half num4 = (this.Min.Z - ray.Position.Z) * num9;
				Half num3 = (this.Max.Z - ray.Position.Z) * num9;
				if (num4 > num3) {
					Half num12 = num4;
					num4 = num3;
					num3 = num12;
				}
				num = RealMaths.Max (num4, num);
				maxValue = RealMaths.Min (num3, maxValue);
				if (num > maxValue) {
					return null;
				}
			}
			return new Half? (num);
		}

		public Boolean Intersects (ref BoundingSphere sphere)
		{
			Half num;
			Vector3 vector;
			Vector3.Clamp (ref sphere.Center, ref this.Min, ref this.Max, out vector);
			Vector3.DistanceSquared (ref sphere.Center, ref vector, out num);
			return (num <= (sphere.Radius * sphere.Radius));
		}

		public ContainmentType Contains (ref BoundingBox box)
		{
			if ((this.Max.X < box.Min.X) || (this.Min.X > box.Max.X)) {
				return ContainmentType.Disjoint;
			}
			if ((this.Max.Y < box.Min.Y) || (this.Min.Y > box.Max.Y)) {
				return ContainmentType.Disjoint;
			}
			if ((this.Max.Z < box.Min.Z) || (this.Min.Z > box.Max.Z)) {
				return ContainmentType.Disjoint;
			}
			if ((((this.Min.X <= box.Min.X) && (box.Max.X <= this.Max.X)) && ((this.Min.Y <= box.Min.Y) && (box.Max.Y <= this.Max.Y))) && ((this.Min.Z <= box.Min.Z) && (box.Max.Z <= this.Max.Z))) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		public ContainmentType Contains (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			if (!frustum.Intersects (ref this)) {
				return ContainmentType.Disjoint;
			}

			for (Int32 i = 0; i < frustum.cornerArray.Length; ++i) {
				Vector3 vector = frustum.cornerArray[i];
				if (this.Contains (ref vector) == ContainmentType.Disjoint) {
					return ContainmentType.Intersects;
				}
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref Vector3 point)
		{
			if ((((this.Min.X <= point.X) && (point.X <= this.Max.X)) && ((this.Min.Y <= point.Y) && (point.Y <= this.Max.Y))) && ((this.Min.Z <= point.Z) && (point.Z <= this.Max.Z))) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Disjoint;
		}

		public ContainmentType Contains (ref BoundingSphere sphere)
		{
			Half num2;
			Vector3 vector;
			Vector3.Clamp (ref sphere.Center, ref this.Min, ref this.Max, out vector);
			Vector3.DistanceSquared (ref sphere.Center, ref vector, out num2);
			Half radius = sphere.Radius;
			if (num2 > (radius * radius)) {
				return ContainmentType.Disjoint;
			}
			if (((((this.Min.X + radius) <= sphere.Center.X) && (sphere.Center.X <= (this.Max.X - radius))) && (((this.Max.X - this.Min.X) > radius) && ((this.Min.Y + radius) <= sphere.Center.Y))) && (((sphere.Center.Y <= (this.Max.Y - radius)) && ((this.Max.Y - this.Min.Y) > radius)) && ((((this.Min.Z + radius) <= sphere.Center.Z) && (sphere.Center.Z <= (this.Max.Z - radius))) && ((this.Max.X - this.Min.X) > radius)))) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		internal void SupportMapping (ref Vector3 v, out Vector3 result)
		{
			Half zero = 0;

			result.X = (v.X >= zero) ? this.Max.X : this.Min.X;
			result.Y = (v.Y >= zero) ? this.Max.Y : this.Min.Y;
			result.Z = (v.Z >= zero) ? this.Max.Z : this.Min.Z;
		}

		public static Boolean operator == (BoundingBox a, BoundingBox b)
		{
			return a.Equals (b);
		}

		public static Boolean operator != (BoundingBox a, BoundingBox b)
		{
			if (!(a.Min != b.Min)) {
				return (a.Max != b.Max);
			}
			return true;
		}
	}
	public class BoundingFrustum 
		: IEquatable<BoundingFrustum>
	{
		const int BottomPlaneIndex = 5;

		internal Vector3[] cornerArray;

		public const int CornerCount = 8;

		const int FarPlaneIndex = 1;

		GjkDistance gjk;

		const int LeftPlaneIndex = 2;

		Matrix44 matrix;

		const int NearPlaneIndex = 0;

		const int NumPlanes = 6;

		Plane[] planes;

		const int RightPlaneIndex = 3;

		const int TopPlaneIndex = 4;

		BoundingFrustum ()
		{
			this.planes = new Plane[6];
			this.cornerArray = new Vector3[8];
		}

		public BoundingFrustum (Matrix44 value)
		{
			this.planes = new Plane[6];
			this.cornerArray = new Vector3[8];
			this.SetMatrix (ref value);
		}

		static Vector3 ComputeIntersection (ref Plane plane, ref Ray ray)
		{
			Half planeNormDotRayPos;
			Half planeNormDotRayDir;

			Vector3.Dot (ref plane.Normal, ref ray.Position, out planeNormDotRayPos);
			Vector3.Dot (ref plane.Normal, ref ray.Direction, out planeNormDotRayDir);

			Half num = (-plane.D - planeNormDotRayPos) / planeNormDotRayDir;

			return (ray.Position + (ray.Direction * num));
		}

		static Ray ComputeIntersectionLine (ref Plane p1, ref Plane p2)
		{
			Ray ray = new Ray ();

			Vector3.Cross (ref p1.Normal, ref p2.Normal, out ray.Direction);

			Half num = ray.Direction.LengthSquared ();

			Vector3 a = (-p1.D * p2.Normal) + (p2.D * p1.Normal);

			Vector3 cross;

			Vector3.Cross (ref a, ref ray.Direction, out cross);

			ray.Position =  cross / num;

			return ray;
		}

		public ContainmentType Contains (ref BoundingBox box)
		{
			Boolean flag = false;
			for(Int32 i = 0; i < this.planes.Length; ++i)
			{
				Plane plane = this.planes[i];
				switch (box.Intersects (ref plane)) {
				case PlaneIntersectionType.Front:
					return ContainmentType.Disjoint;

				case PlaneIntersectionType.Intersecting:
					flag = true;
					break;
				}
			}
			if (!flag) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		public ContainmentType Contains (ref BoundingFrustum frustum)
		{
			if (frustum == null) {
				throw new ArgumentNullException ("frustum");
			}
			ContainmentType disjoint = ContainmentType.Disjoint;
			if (this.Intersects (ref frustum)) {
				disjoint = ContainmentType.Contains;
				for (int i = 0; i < this.cornerArray.Length; i++) {
					if (this.Contains (ref frustum.cornerArray [i]) == ContainmentType.Disjoint) {
						return ContainmentType.Intersects;
					}
				}
			}
			return disjoint;
		}

		public ContainmentType Contains (ref BoundingSphere sphere)
		{
			Vector3 center = sphere.Center;
			Half radius = sphere.Radius;
			int num2 = 0;
			foreach (Plane plane in this.planes) {
				Half num5 = ((plane.Normal.X * center.X) + (plane.Normal.Y * center.Y)) + (plane.Normal.Z * center.Z);
				Half num3 = num5 + plane.D;
				if (num3 > radius) {
					return ContainmentType.Disjoint;
				}
				if (num3 < -radius) {
					num2++;
				}
			}
			if (num2 != 6) {
				return ContainmentType.Intersects;
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref Vector3 point)
		{
			Half epsilon; RealMaths.FromString("0.00001", out epsilon);

			foreach (Plane plane in this.planes) {
				Half num2 = (((plane.Normal.X * point.X) + (plane.Normal.Y * point.Y)) + (plane.Normal.Z * point.Z)) + plane.D;
				if (num2 > epsilon) {
					return ContainmentType.Disjoint;
				}
			}
			return ContainmentType.Contains;
		}

		public Boolean Equals (BoundingFrustum other)
		{
			if (other == null) {
				return false;
			}
			return (this.matrix == other.matrix);
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			BoundingFrustum frustum = obj as BoundingFrustum;
			if (frustum != null) {
				flag = this.matrix == frustum.matrix;
			}
			return flag;
		}

		public Vector3[] GetCorners ()
		{
			return (Vector3[])this.cornerArray.Clone ();
		}

		public override Int32 GetHashCode ()
		{
			return this.matrix.GetHashCode ();
		}

		public Boolean Intersects (ref BoundingBox box)
		{
			Boolean flag;
			this.Intersects (ref box, out flag);
			return flag;
		}

		void Intersects (ref BoundingBox box, out Boolean result)
		{
			Half epsilon; RealMaths.FromString("0.00001", out epsilon);
			Half zero = 0;
			Half four = 4;

			Vector3 closestPoint;
			Vector3 vector2;
			Vector3 vector3;
			Vector3 vector4;
			Vector3 vector5;
			if (this.gjk == null) {
				this.gjk = new GjkDistance ();
			}
			this.gjk.Reset ();
			Vector3.Subtract (ref this.cornerArray [0], ref box.Min, out closestPoint);
			if (closestPoint.LengthSquared () < epsilon) {
				Vector3.Subtract (ref this.cornerArray [0], ref box.Max, out closestPoint);
			}
			Half maxValue = Half.MaxValue;
			Half num3 = zero;
			result = false;
		Label_006D:
			vector5.X = -closestPoint.X;
			vector5.Y = -closestPoint.Y;
			vector5.Z = -closestPoint.Z;
			this.SupportMapping (ref vector5, out vector4);
			box.SupportMapping (ref closestPoint, out vector3);
			Vector3.Subtract (ref vector4, ref vector3, out vector2);
			Half num4 = ((closestPoint.X * vector2.X) + (closestPoint.Y * vector2.Y)) + (closestPoint.Z * vector2.Z);
			if (num4 <= zero) {
				this.gjk.AddSupportPoint (ref vector2);
				closestPoint = this.gjk.ClosestPoint;
				Half num2 = maxValue;
				maxValue = closestPoint.LengthSquared ();
				if ((num2 - maxValue) > (epsilon * num2)) {
					num3 = four * epsilon * this.gjk.MaxLengthSquared;
					if (!this.gjk.FullSimplex && (maxValue >= num3)) {
						goto Label_006D;
					}
					result = true;
				}
			}
		}

		public Boolean Intersects (ref BoundingFrustum frustum)
		{
			Half epsilon; RealMaths.FromString("0.00001", out epsilon);
			Half zero = 0;
			Half four = 4;

			Vector3 closestPoint;
			if (frustum == null) {
				throw new ArgumentNullException ("frustum");
			}
			if (this.gjk == null) {
				this.gjk = new GjkDistance ();
			}
			this.gjk.Reset ();
			Vector3.Subtract (ref this.cornerArray [0], ref frustum.cornerArray [0], out closestPoint);
			if (closestPoint.LengthSquared () < epsilon) {
				Vector3.Subtract (ref this.cornerArray [0], ref frustum.cornerArray [1], out closestPoint);
			}
			Half maxValue = Half.MaxValue;
			Half num3 = zero;
			do {
				Vector3 vector2;
				Vector3 vector3;
				Vector3 vector4;
				Vector3 vector5;
				vector5.X = -closestPoint.X;
				vector5.Y = -closestPoint.Y;
				vector5.Z = -closestPoint.Z;
				this.SupportMapping (ref vector5, out vector4);
				frustum.SupportMapping (ref closestPoint, out vector3);
				Vector3.Subtract (ref vector4, ref vector3, out vector2);
				Half num4 = ((closestPoint.X * vector2.X) + (closestPoint.Y * vector2.Y)) + (closestPoint.Z * vector2.Z);
				if (num4 > zero) {
					return false;
				}
				this.gjk.AddSupportPoint (ref vector2);
				closestPoint = this.gjk.ClosestPoint;
				Half num2 = maxValue;
				maxValue = closestPoint.LengthSquared ();
				num3 = four * epsilon * this.gjk.MaxLengthSquared;
				if ((num2 - maxValue) <= (epsilon * num2)) {
					return false;
				}
			} while (!this.gjk.FullSimplex && (maxValue >= num3));
			return true;
		}

		public Boolean Intersects (ref BoundingSphere sphere)
		{
			Boolean flag;
			this.Intersects (ref sphere, out flag);
			return flag;
		}

		void Intersects (ref BoundingSphere sphere, out Boolean result)
		{
			Half zero = 0;
			Half epsilon; RealMaths.FromString("0.00001", out epsilon);
			Half four = 4;

			Vector3 unitX;
			Vector3 vector2;
			Vector3 vector3;
			Vector3 vector4;
			Vector3 vector5;
			if (this.gjk == null) {
				this.gjk = new GjkDistance ();
			}
			this.gjk.Reset ();
			Vector3.Subtract (ref this.cornerArray [0], ref sphere.Center, out unitX);
			if (unitX.LengthSquared () < epsilon) {
				unitX = Vector3.UnitX;
			}
			Half maxValue = Half.MaxValue;
			Half num3 = zero;
			result = false;
		Label_005A:
			vector5.X = -unitX.X;
			vector5.Y = -unitX.Y;
			vector5.Z = -unitX.Z;
			this.SupportMapping (ref vector5, out vector4);
			sphere.SupportMapping (ref unitX, out vector3);
			Vector3.Subtract (ref vector4, ref vector3, out vector2);
			Half num4 = ((unitX.X * vector2.X) + (unitX.Y * vector2.Y)) + (unitX.Z * vector2.Z);
			if (num4 <= zero) {
				this.gjk.AddSupportPoint (ref vector2);
				unitX = this.gjk.ClosestPoint;
				Half num2 = maxValue;
				maxValue = unitX.LengthSquared ();
				if ((num2 - maxValue) > (epsilon * num2)) {
					num3 = four * epsilon * this.gjk.MaxLengthSquared;
					if (!this.gjk.FullSimplex && (maxValue >= num3)) {
						goto Label_005A;
					}
					result = true;
				}
			}
		}

		public PlaneIntersectionType Intersects (ref Plane plane)
		{
			Half zero = 0;

			int num = 0;
			for (int i = 0; i < 8; i++) {
				Half num3;
				Vector3.Dot (ref this.cornerArray [i], ref plane.Normal, out num3);
				if ((num3 + plane.D) > zero) {
					num |= 1;
				} else {
					num |= 2;
				}
				if (num == 3) {
					return PlaneIntersectionType.Intersecting;
				}
			}
			if (num != 1) {
				return PlaneIntersectionType.Back;
			}
			return PlaneIntersectionType.Front;
		}

		public Half? Intersects (ref Ray ray)
		{
			Half? nullable;
			this.Intersects (ref ray, out nullable);
			return nullable;
		}

		void Intersects (ref Ray ray, out Half? result)
		{
			Half epsilon; RealMaths.FromString("0.00001", out epsilon);
			Half zero = 0;

			ContainmentType type = this.Contains (ref ray.Position);
			if (type == ContainmentType.Contains) {
				result = zero;
			} else {
				Half minValue = Half.MinValue;
				Half maxValue = Half.MaxValue;
				result = zero;
				foreach (Plane plane in this.planes) {
					Half num3;
					Half num6;
					Vector3 normal = plane.Normal;
					Vector3.Dot (ref ray.Direction, ref normal, out num6);
					Vector3.Dot (ref ray.Position, ref normal, out num3);
					num3 += plane.D;
					if (RealMaths.Abs (num6) < epsilon) {
						if (num3 > zero) {
							return;
						}
					} else {
						Half num = -num3 / num6;
						if (num6 < zero) {
							if (num > maxValue) {
								return;
							}
							if (num > minValue) {
								minValue = num;
							}
						} else {
							if (num < minValue) {
								return;
							}
							if (num < maxValue) {
								maxValue = num;
							}
						}
					}
				}
				Half num7 = (minValue >= zero) ? minValue : maxValue;
				if (num7 >= zero) {
					result = new Half? (num7);
				}
			}
		}

		public static Boolean operator == (BoundingFrustum a, BoundingFrustum b)
		{
			return Object.Equals (a, b);
		}

		public static Boolean operator != (BoundingFrustum a, BoundingFrustum b)
		{
			return !Object.Equals (a, b);
		}

		void SetMatrix (ref Matrix44 value)
		{
			this.matrix = value;

			this.planes [2].Normal.X = -value.M14 - value.M11;
			this.planes [2].Normal.Y = -value.M24 - value.M21;
			this.planes [2].Normal.Z = -value.M34 - value.M31;
			this.planes [2].D = -value.M44 - value.M41;

			this.planes [3].Normal.X = -value.M14 + value.M11;
			this.planes [3].Normal.Y = -value.M24 + value.M21;
			this.planes [3].Normal.Z = -value.M34 + value.M31;
			this.planes [3].D = -value.M44 + value.M41;

			this.planes [4].Normal.X = -value.M14 + value.M12;
			this.planes [4].Normal.Y = -value.M24 + value.M22;
			this.planes [4].Normal.Z = -value.M34 + value.M32;
			this.planes [4].D = -value.M44 + value.M42;

			this.planes [5].Normal.X = -value.M14 - value.M12;
			this.planes [5].Normal.Y = -value.M24 - value.M22;
			this.planes [5].Normal.Z = -value.M34 - value.M32;
			this.planes [5].D = -value.M44 - value.M42;

			this.planes [0].Normal.X = -value.M13;
			this.planes [0].Normal.Y = -value.M23;
			this.planes [0].Normal.Z = -value.M33;
			this.planes [0].D = -value.M43;

			this.planes [1].Normal.X = -value.M14 + value.M13;
			this.planes [1].Normal.Y = -value.M24 + value.M23;
			this.planes [1].Normal.Z = -value.M34 + value.M33;
			this.planes [1].D = -value.M44 + value.M43;

			for (int i = 0; i < 6; i++) {
				Half num2 = this.planes [i].Normal.Length ();
				this.planes [i].Normal = (Vector3)(this.planes [i].Normal / num2);
				this.planes [i].D /= num2;
			}

			Ray ray = ComputeIntersectionLine (ref this.planes [0], ref this.planes [2]);

			this.cornerArray [0] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [3] = ComputeIntersection (ref this.planes [5], ref ray);

			ray = ComputeIntersectionLine (ref this.planes [3], ref this.planes [0]);

			this.cornerArray [1] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [2] = ComputeIntersection (ref this.planes [5], ref ray);

			ray = ComputeIntersectionLine (ref this.planes [2], ref this.planes [1]);

			this.cornerArray [4] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [7] = ComputeIntersection (ref this.planes [5], ref ray);

			ray = ComputeIntersectionLine (ref this.planes [1], ref this.planes [3]);

			this.cornerArray [5] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [6] = ComputeIntersection (ref this.planes [5], ref ray);
		}

		internal void SupportMapping (ref Vector3 v, out Vector3 result)
		{
			Half num3;

			int index = 0;

			Vector3.Dot (ref this.cornerArray [0], ref v, out num3);

			for (int i = 1; i < this.cornerArray.Length; i++)
			{
				Half num2;

				Vector3.Dot (ref this.cornerArray [i], ref v, out num2);

				if (num2 > num3)
				{
					index = i;
					num3 = num2;
				}
			}

			result = this.cornerArray [index];
		}

		public override String ToString ()
		{
			return string.Format ("{{Near:{0} Far:{1} Left:{2} Right:{3} Top:{4} Bottom:{5}}}", new Object[] { this.Near.ToString (), this.Far.ToString (), this.Left.ToString (), this.Right.ToString (), this.Top.ToString (), this.Bottom.ToString () });
		}

		// Properties
		public Plane Bottom
		{
			get
			{
				return this.planes [5];
			}
		}

		public Plane Far {
			get {
				return this.planes [1];
			}
		}

		public Plane Left {
			get {
				return this.planes [2];
			}
		}

		public Matrix44 Matrix {
			get {
				return this.matrix;
			}
			set {
				this.SetMatrix (ref value);
			}
		}

		public Plane Near {
			get {
				return this.planes [0];
			}
		}

		public Plane Right {
			get {
				return this.planes [3];
			}
		}

		public Plane Top {
			get {
				return this.planes [4];
			}
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct BoundingSphere 
		: IEquatable<BoundingSphere>
	{
		public Vector3 Center;
		public Half Radius;

		public BoundingSphere (Vector3 center, Half radius)
		{
			Half zero = 0;

			if (radius < zero) {
				throw new ArgumentException ("NegativeRadius");
			}
			this.Center = center;
			this.Radius = radius;
		}

		public Boolean Equals (BoundingSphere other)
		{
			return ((this.Center == other.Center) && (this.Radius == other.Radius));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is BoundingSphere) {
				flag = this.Equals ((BoundingSphere)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Center.GetHashCode () + this.Radius.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Center:{0} Radius:{1}}}", new Object[] { this.Center.ToString (), this.Radius.ToString () });
		}

		public static void CreateMerged (ref BoundingSphere original, ref BoundingSphere additional, out BoundingSphere result)
		{
			Half half; RealMaths.Half(out half);
			Half one = 1;
			Vector3 vector2;
			Vector3.Subtract (ref additional.Center, ref original.Center, out vector2);
			Half num = vector2.Length ();
			Half radius = original.Radius;
			Half num2 = additional.Radius;
			if ((radius + num2) >= num) {
				if ((radius - num2) >= num) {
					result = original;
					return;
				}
				if ((num2 - radius) >= num) {
					result = additional;
					return;
				}
			}
			Vector3 vector = (Vector3)(vector2 * (one / num));
			Half num5 = RealMaths.Min (-radius, num - num2);
			Half num4 = (RealMaths.Max (radius, num + num2) - num5) * half;
			result.Center = original.Center + ((Vector3)(vector * (num4 + num5)));
			result.Radius = num4;
		}

		public static void CreateFromBoundingBox (ref BoundingBox box, out BoundingSphere result)
		{
			Half half; RealMaths.Half(out half);
			Half num;
			Vector3.Lerp (ref box.Min, ref box.Max, half, out result.Center);
			Vector3.Distance (ref box.Min, ref box.Max, out num);
			result.Radius = num * half;
		}

		public static void CreateFromPoints (IEnumerable<Vector3> points, out BoundingSphere sphere)
		{	
			Half half; RealMaths.Half(out half);
			Half one = 1;

			Half num;
			Half num2;
			Vector3 vector2;
			Half num4;
			Half num5;
			
			Vector3 vector5;
			Vector3 vector6;
			Vector3 vector7;
			Vector3 vector8;
			Vector3 vector9;
			if (points == null) {
				throw new ArgumentNullException ("points");
			}
			IEnumerator<Vector3> enumerator = points.GetEnumerator ();
			if (!enumerator.MoveNext ()) {
				throw new ArgumentException ("BoundingSphereZeroPoints");
			}
			Vector3 vector4 = vector5 = vector6 = vector7 = vector8 = vector9 = enumerator.Current;
			foreach (Vector3 vector in points) {
				if (vector.X < vector4.X) {
					vector4 = vector;
				}
				if (vector.X > vector5.X) {
					vector5 = vector;
				}
				if (vector.Y < vector6.Y) {
					vector6 = vector;
				}
				if (vector.Y > vector7.Y) {
					vector7 = vector;
				}
				if (vector.Z < vector8.Z) {
					vector8 = vector;
				}
				if (vector.Z > vector9.Z) {
					vector9 = vector;
				}
			}
			Vector3.Distance (ref vector5, ref vector4, out num5);
			Vector3.Distance (ref vector7, ref vector6, out num4);
			Vector3.Distance (ref vector9, ref vector8, out num2);
			if (num5 > num4) {
				if (num5 > num2) {
					Vector3.Lerp (ref vector5, ref vector4, half, out vector2);
					num = num5 * half;
				} else {
					Vector3.Lerp (ref vector9, ref vector8, half, out vector2);
					num = num2 * half;
				}
			} else if (num4 > num2) {
				Vector3.Lerp (ref vector7, ref vector6, half, out vector2);
				num = num4 * half;
			} else {
				Vector3.Lerp (ref vector9, ref vector8, half, out vector2);
				num = num2 * half;
			}
			foreach (Vector3 vector10 in points) {
				Vector3 vector3;
				vector3.X = vector10.X - vector2.X;
				vector3.Y = vector10.Y - vector2.Y;
				vector3.Z = vector10.Z - vector2.Z;
				Half num3 = vector3.Length ();
				if (num3 > num) {
					num = (num + num3) * half;
					vector2 += (Vector3)((one - (num / num3)) * vector3);
				}
			}
			sphere.Center = vector2;
			sphere.Radius = num;
		}

		public static void CreateFromFrustum (ref BoundingFrustum frustum, out BoundingSphere sphere)
		{
			if (frustum == null) {
				throw new ArgumentNullException ("frustum");
			}

			CreateFromPoints (frustum.cornerArray, out sphere);
		}

		public Boolean Intersects (ref BoundingBox box)
		{
			Half num;
			Vector3 vector;
			Vector3.Clamp (ref this.Center, ref box.Min, ref box.Max, out vector);
			Vector3.DistanceSquared (ref this.Center, ref vector, out num);
			return (num <= (this.Radius * this.Radius));
		}

		public Boolean Intersects (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			return frustum.Intersects (ref this);
		}

		public PlaneIntersectionType Intersects (ref Plane plane)
		{
			return plane.Intersects (ref this);
		}

		public Half? Intersects (ref Ray ray)
		{
			return ray.Intersects (ref this);
		}

		public Boolean Intersects (ref BoundingSphere sphere)
		{
			Half two = 2;

			Half num3;
			Vector3.DistanceSquared (ref this.Center, ref sphere.Center, out num3);
			Half radius = this.Radius;
			Half num = sphere.Radius;
			if ((((radius * radius) + ((two * radius) * num)) + (num * num)) <= num3) {
				return false;
			}
			return true;
		}

		public ContainmentType Contains (ref BoundingBox box)
		{
			Vector3 vector;
			if (!box.Intersects (ref this)) {
				return ContainmentType.Disjoint;
			}
			Half num = this.Radius * this.Radius;
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			if (!frustum.Intersects (ref this)) {
				return ContainmentType.Disjoint;
			}
			Half num2 = this.Radius * this.Radius;
			foreach (Vector3 vector2 in frustum.cornerArray) {
				Vector3 vector;
				vector.X = vector2.X - this.Center.X;
				vector.Y = vector2.Y - this.Center.Y;
				vector.Z = vector2.Z - this.Center.Z;
				if (vector.LengthSquared () > num2) {
					return ContainmentType.Intersects;
				}
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref Vector3 point)
		{
			Half temp;
			Vector3.DistanceSquared (ref point, ref this.Center, out temp);

			if (temp >= (this.Radius * this.Radius))
			{
				return ContainmentType.Disjoint;
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref BoundingSphere sphere)
		{
			Half num3;
			Vector3.Distance (ref this.Center, ref sphere.Center, out num3);
			Half radius = this.Radius;
			Half num = sphere.Radius;
			if ((radius + num) < num3) {
				return ContainmentType.Disjoint;
			}
			if ((radius - num) < num3) {
				return ContainmentType.Intersects;
			}
			return ContainmentType.Contains;
		}

		internal void SupportMapping (ref Vector3 v, out Vector3 result)
		{
			Half num2 = v.Length ();
			Half num = this.Radius / num2;
			result.X = this.Center.X + (v.X * num);
			result.Y = this.Center.Y + (v.Y * num);
			result.Z = this.Center.Z + (v.Z * num);
		}

		public BoundingSphere Transform (Matrix44 matrix)
		{
			BoundingSphere sphere = new BoundingSphere ();
			Vector3.Transform (ref this.Center, ref matrix, out sphere.Center);
			Half num4 = ((matrix.M11 * matrix.M11) + (matrix.M12 * matrix.M12)) + (matrix.M13 * matrix.M13);
			Half num3 = ((matrix.M21 * matrix.M21) + (matrix.M22 * matrix.M22)) + (matrix.M23 * matrix.M23);
			Half num2 = ((matrix.M31 * matrix.M31) + (matrix.M32 * matrix.M32)) + (matrix.M33 * matrix.M33);
			Half num = RealMaths.Max (num4, RealMaths.Max (num3, num2));
			sphere.Radius = this.Radius * (RealMaths.Sqrt (num));
			return sphere;
		}

		public void Transform (ref Matrix44 matrix, out BoundingSphere result)
		{
			Vector3.Transform (ref this.Center, ref matrix, out result.Center);
			Half num4 = ((matrix.M11 * matrix.M11) + (matrix.M12 * matrix.M12)) + (matrix.M13 * matrix.M13);
			Half num3 = ((matrix.M21 * matrix.M21) + (matrix.M22 * matrix.M22)) + (matrix.M23 * matrix.M23);
			Half num2 = ((matrix.M31 * matrix.M31) + (matrix.M32 * matrix.M32)) + (matrix.M33 * matrix.M33);
			Half num = RealMaths.Max (num4, RealMaths.Max (num3, num2));
			result.Radius = this.Radius * (RealMaths.Sqrt (num));
		}

		public static Boolean operator == (BoundingSphere a, BoundingSphere b)
		{
			return a.Equals (b);
		}

		public static Boolean operator != (BoundingSphere a, BoundingSphere b)
		{
			if (!(a.Center != b.Center)) {
				return !(a.Radius == b.Radius);
			}
			return true;
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Matrix44 
		: IEquatable<Matrix44>
	{
		// Row 0
		public Half M11;
		public Half M12;
		public Half M13;
		public Half M14;

		// Row 1
		public Half M21;
		public Half M22;
		public Half M23;
		public Half M24;

		// Row 2
		public Half M31;
		public Half M32;
		public Half M33;
		public Half M34;

		// Row 3
		public Half M41; // translation.x
		public Half M42; // translation.y
		public Half M43; // translation.z
		public Half M44;
		
		public Vector3 Up {
			get {
				Vector3 vector;
				vector.X = this.M21;
				vector.Y = this.M22;
				vector.Z = this.M23;
				return vector;
			}
			set {
				this.M21 = value.X;
				this.M22 = value.Y;
				this.M23 = value.Z;
			}
		}

		public Vector3 Down {
			get {
				Vector3 vector;
				vector.X = -this.M21;
				vector.Y = -this.M22;
				vector.Z = -this.M23;
				return vector;
			}
			set {
				this.M21 = -value.X;
				this.M22 = -value.Y;
				this.M23 = -value.Z;
			}
		}

		public Vector3 Right {
			get {
				Vector3 vector;
				vector.X = this.M11;
				vector.Y = this.M12;
				vector.Z = this.M13;
				return vector;
			}
			set {
				this.M11 = value.X;
				this.M12 = value.Y;
				this.M13 = value.Z;
			}
		}

		public Vector3 Left {
			get {
				Vector3 vector;
				vector.X = -this.M11;
				vector.Y = -this.M12;
				vector.Z = -this.M13;
				return vector;
			}
			set {
				this.M11 = -value.X;
				this.M12 = -value.Y;
				this.M13 = -value.Z;
			}
		}

		public Vector3 Forward {
			get {
				Vector3 vector;
				vector.X = -this.M31;
				vector.Y = -this.M32;
				vector.Z = -this.M33;
				return vector;
			}
			set {
				this.M31 = -value.X;
				this.M32 = -value.Y;
				this.M33 = -value.Z;
			}
		}

		public Vector3 Backward {
			get {
				Vector3 vector;
				vector.X = this.M31;
				vector.Y = this.M32;
				vector.Z = this.M33;
				return vector;
			}
			set {
				this.M31 = value.X;
				this.M32 = value.Y;
				this.M33 = value.Z;
			}
		}

		public Vector3 Translation {
			get {
				Vector3 vector;
				vector.X = this.M41;
				vector.Y = this.M42;
				vector.Z = this.M43;
				return vector;
			}
			set {
				this.M41 = value.X;
				this.M42 = value.Y;
				this.M43 = value.Z;
			}
		}

		public Matrix44 (Half m11, Half m12, Half m13, Half m14, Half m21, Half m22, Half m23, Half m24, Half m31, Half m32, Half m33, Half m34, Half m41, Half m42, Half m43, Half m44)
		{
			this.M11 = m11;
			this.M12 = m12;
			this.M13 = m13;
			this.M14 = m14;
			this.M21 = m21;
			this.M22 = m22;
			this.M23 = m23;
			this.M24 = m24;
			this.M31 = m31;
			this.M32 = m32;
			this.M33 = m33;
			this.M34 = m34;
			this.M41 = m41;
			this.M42 = m42;
			this.M43 = m43;
			this.M44 = m44;
		}

		public override String ToString ()
		{
			return ("{ " + string.Format ("{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", new Object[] { this.M11.ToString (), this.M12.ToString (), this.M13.ToString (), this.M14.ToString () }) + string.Format ("{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", new Object[] { this.M21.ToString (), this.M22.ToString (), this.M23.ToString (), this.M24.ToString () }) + string.Format ("{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", new Object[] { this.M31.ToString (), this.M32.ToString (), this.M33.ToString (), this.M34.ToString () }) + string.Format ("{{M41:{0} M42:{1} M43:{2} M44:{3}}} ", new Object[] { this.M41.ToString (), this.M42.ToString (), this.M43.ToString (), this.M44.ToString () }) + "}");
		}

		public Boolean Equals (Matrix44 other)
		{
			return ((((((this.M11 == other.M11) && (this.M22 == other.M22)) && ((this.M33 == other.M33) && (this.M44 == other.M44))) && (((this.M12 == other.M12) && (this.M13 == other.M13)) && ((this.M14 == other.M14) && (this.M21 == other.M21)))) && ((((this.M23 == other.M23) && (this.M24 == other.M24)) && ((this.M31 == other.M31) && (this.M32 == other.M32))) && (((this.M34 == other.M34) && (this.M41 == other.M41)) && (this.M42 == other.M42)))) && (this.M43 == other.M43));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Matrix44)
			{
				flag = this.Equals ((Matrix44)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (((((((((((((((this.M11.GetHashCode () + this.M12.GetHashCode ()) + this.M13.GetHashCode ()) + this.M14.GetHashCode ()) + this.M21.GetHashCode ()) + this.M22.GetHashCode ()) + this.M23.GetHashCode ()) + this.M24.GetHashCode ()) + this.M31.GetHashCode ()) + this.M32.GetHashCode ()) + this.M33.GetHashCode ()) + this.M34.GetHashCode ()) + this.M41.GetHashCode ()) + this.M42.GetHashCode ()) + this.M43.GetHashCode ()) + this.M44.GetHashCode ());
		}

		#region Constants

		static Matrix44 identity;

		static Matrix44 ()
		{
			Half zero = 0;
			Half one = 1;
			identity = new Matrix44 (one, zero, zero, zero, zero, one, zero, zero, zero, zero, one, zero, zero, zero, zero, one);
		}

		public static Matrix44 Identity {
			get {
				return identity;
			}
		}
		
		#endregion
		#region Create

		public static void CreateTranslation (ref Vector3 position, out Matrix44 result)
		{
			result.M11 = 1;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = 1;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = 1;
			result.M34 = 0;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1;
		}
		
		public static void CreateTranslation (Half xPosition, Half yPosition, Half zPosition, out Matrix44 result)
		{	
			result.M11 = 1;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = 1;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = 1;
			result.M34 = 0;
			result.M41 = xPosition;
			result.M42 = yPosition;
			result.M43 = zPosition;
			result.M44 = 1;
		}
		
		// Creates a scaling matrix based on x, y, z.
		public static void CreateScale (Half xScale, Half yScale, Half zScale, out Matrix44 result)
		{
			result.M11 = xScale;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = yScale;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = zScale;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		// Creates a scaling matrix based on a vector.
		public static void CreateScale (ref Vector3 scales, out Matrix44 result)
		{
			result.M11 = scales.X;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = scales.Y;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = scales.Z;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		// Create a scaling matrix consistant along each axis
		public static void CreateScale (Half scale, out Matrix44 result)
		{
			result.M11 = scale;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = scale;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = scale;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		public static void CreateRotationX (Half radians, out Matrix44 result)
		{
			// http://en.wikipedia.org/wiki/Rotation_matrix

			Half cos = RealMaths.Cos (radians);
			Half sin = RealMaths.Sin (radians);

			result.M11 = 1;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = cos;
			result.M23 = sin;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = -sin;
			result.M33 = cos;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		public static void CreateRotationY (Half radians, out Matrix44 result)
		{
			// http://en.wikipedia.org/wiki/Rotation_matrix

			Half cos = RealMaths.Cos (radians);
			Half sin = RealMaths.Sin (radians);

			result.M11 = cos;
			result.M12 = 0;
			result.M13 = -sin;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = 1;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = sin;
			result.M32 = 0;
			result.M33 = cos;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}
		
		public static void CreateRotationZ (Half radians, out Matrix44 result)
		{
			// http://en.wikipedia.org/wiki/Rotation_matrix

			Half cos = RealMaths.Cos (radians);
			Half sin = RealMaths.Sin (radians);

			result.M11 = cos;
			result.M12 = sin;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = -sin;
			result.M22 = cos;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = 1;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}
		
		public static void CreateFromAxisAngle (ref Vector3 axis, Half angle, out Matrix44 result)
		{
			Half one = 1;

			Half x = axis.X;
			Half y = axis.Y;
			Half z = axis.Z;

			Half sin = RealMaths.Sin (angle);
			Half cos = RealMaths.Cos (angle);

			Half xx = x * x;
			Half yy = y * y;
			Half zz = z * z;

			Half xy = x * y;
			Half xz = x * z;
			Half yz = y * z;

			result.M11 = xx + (cos * (one - xx));
			result.M12 = (xy - (cos * xy)) + (sin * z);
			result.M13 = (xz - (cos * xz)) - (sin * y);
			result.M14 = 0;

			result.M21 = (xy - (cos * xy)) - (sin * z);
			result.M22 = yy + (cos * (one - yy));
			result.M23 = (yz - (cos * yz)) + (sin * x);
			result.M24 = 0;

			result.M31 = (xz - (cos * xz)) + (sin * y);
			result.M32 = (yz - (cos * yz)) - (sin * x);
			result.M33 = zz + (cos * (one - zz));
			result.M34 = 0;

			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = one;
		}
		
		public static void CreateFromAllAxis (ref Vector3 right, ref Vector3 up, ref Vector3 backward, out Matrix44 result)
		{
			if(!right.IsUnit() || !up.IsUnit() || !backward.IsUnit() )
			{
				throw new ArgumentException("The input vertors must be normalised.");
			}

			result.M11 = right.X;
			result.M12 = right.Y;
			result.M13 = right.Z;
			result.M14 = 0;
			result.M21 = up.X;
			result.M22 = up.Y;
			result.M23 = up.Z;
			result.M24 = 0;
			result.M31 = backward.X;
			result.M32 = backward.Y;
			result.M33 = backward.Z;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		public static void CreateWorldNew (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result)
		{
			Vector3 backward = -forward;

			Vector3 right;

			Vector3.Cross (ref up, ref backward, out right);

			right.Normalise();

			Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out result);

			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
		}

		public static void CreateWorld (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result)
		{
			if(!forward.IsUnit() || !up.IsUnit() )
			{
				throw new ArgumentException("The input vertors must be normalised.");
			}

			Vector3 backward = -forward;

			Vector3 vector; Vector3.Normalise (ref backward, out vector);

			Vector3 cross; Vector3.Cross (ref up, ref vector, out cross);

			Vector3 vector2; Vector3.Normalise (ref cross, out vector2);

			Vector3 vector3; Vector3.Cross (ref vector, ref vector2, out vector3);

			result.M11 = vector2.X;
			result.M12 = vector2.Y;
			result.M13 = vector2.Z;
			result.M14 = 0;
			result.M21 = vector3.X;
			result.M22 = vector3.Y;
			result.M23 = vector3.Z;
			result.M24 = 0;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = 0;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1;
		}

		public static void CreateFromQuaternion (ref Quaternion quaternion, out Matrix44 result)
		{
			if(!quaternion.IsUnit())
			{
				throw new ArgumentException("Input quaternion must be normalised.");
			}

			Half zero = 0;
			Half one = 1;

			Half xs = quaternion.X + quaternion.X;   
			Half ys = quaternion.Y + quaternion.Y;
			Half zs = quaternion.Z + quaternion.Z;
			Half wx = quaternion.W * xs;
			Half wy = quaternion.W * ys;
			Half wz = quaternion.W * zs;
			Half xx = quaternion.X * xs;
			Half xy = quaternion.X * ys;
			Half xz = quaternion.X * zs;
			Half yy = quaternion.Y * ys;
			Half yz = quaternion.Y * zs;
			Half zz = quaternion.Z * zs;

			result.M11 = one - (yy + zz);
			result.M21 = xy - wz;
			result.M31 = xz + wy;
			result.M41 = zero;
    
			result.M12 = xy + wz;
			result.M22 = one - (xx + zz);
			result.M32 = yz - wx;
			result.M42 = zero;
    
			result.M13 = xz - wy;
			result.M23 = yz + wx;
			result.M33 = one - (xx + yy);
			result.M43 = zero;

			result.M14 = zero;
			result.M24 = zero;
			result.M34 = zero;
			result.M44 = one;
		}



		// todo: remove when we dont need this for the tests
		internal static void CreateFromQuaternionOld (ref Quaternion quaternion, out Matrix44 result)
		{
			Half zero = 0;
			Half one; RealMaths.One(out one);
			Half two = 2;

			Half num9 = quaternion.X * quaternion.X;
			Half num8 = quaternion.Y * quaternion.Y;
			Half num7 = quaternion.Z * quaternion.Z;
			Half num6 = quaternion.X * quaternion.Y;
			Half num5 = quaternion.Z * quaternion.W;
			Half num4 = quaternion.Z * quaternion.X;
			Half num3 = quaternion.Y * quaternion.W;
			Half num2 = quaternion.Y * quaternion.Z;
			Half num = quaternion.X * quaternion.W;
			result.M11 = one - (two * (num8 + num7));
			result.M12 = two * (num6 + num5);
			result.M13 = two * (num4 - num3);
			result.M14 = zero;
			result.M21 = two * (num6 - num5);
			result.M22 = one - (two * (num7 + num9));
			result.M23 = two * (num2 + num);
			result.M24 = zero;
			result.M31 = two * (num4 + num3);
			result.M32 = two * (num2 - num);
			result.M33 = one - (two * (num8 + num9));
			result.M34 = zero;
			result.M41 = zero;
			result.M42 = zero;
			result.M43 = zero;
			result.M44 = one;
		}

		public static void CreateFromYawPitchRoll (Half yaw, Half pitch, Half roll, out Matrix44 result)
		{
			Quaternion quaternion;

			Quaternion.CreateFromYawPitchRoll (yaw, pitch, roll, out quaternion);

			CreateFromQuaternion (ref quaternion, out result);
		}










		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////
		// TODO: REVIEW FROM HERE ONWARDS
		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////


		// FROM XNA
		// --------
		// Creates a cylindrical billboard that rotates around a specified axis.
		// This method computes the facing direction of the billboard from the object position and camera position. 
		// When the object and camera positions are too close, the matrix will not be accurate. 
		// To avoid this problem, the method uses the optional camera forward vector if the positions are too close.
		public static void CreateBillboard (ref Vector3 ObjectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix44 result)
		{
			Half zero = 0;
			Half one; RealMaths.One(out one);

			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			vector.X = ObjectPosition.X - cameraPosition.X;
			vector.Y = ObjectPosition.Y - cameraPosition.Y;
			vector.Z = ObjectPosition.Z - cameraPosition.Z;
			Half num = vector.LengthSquared ();
			Half limit; RealMaths.FromString("0.0001", out limit);

			if (num < limit) {
				vector = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
			} else {
				Vector3.Multiply (ref vector, (Half)(one / (RealMaths.Sqrt (num))), out vector);
			}
			Vector3.Cross (ref cameraUpVector, ref vector, out vector3);
			vector3.Normalise ();
			Vector3.Cross (ref vector, ref vector3, out vector2);
			result.M11 = vector3.X;
			result.M12 = vector3.Y;
			result.M13 = vector3.Z;
			result.M14 = zero;
			result.M21 = vector2.X;
			result.M22 = vector2.Y;
			result.M23 = vector2.Z;
			result.M24 = zero;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = zero;
			result.M41 = ObjectPosition.X;
			result.M42 = ObjectPosition.Y;
			result.M43 = ObjectPosition.Z;
			result.M44 = one;
		}
		
		public static void CreateConstrainedBillboard (ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector, out Matrix44 result)
		{
			Half zero = 0;
			Half one; RealMaths.One(out one);

			Half num;
			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			vector2.X = objectPosition.X - cameraPosition.X;
			vector2.Y = objectPosition.Y - cameraPosition.Y;
			vector2.Z = objectPosition.Z - cameraPosition.Z;
			Half num2 = vector2.LengthSquared ();
			Half limit; RealMaths.FromString("0.0001", out limit);

			if (num2 < limit) {
				vector2 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
			} else {
				Vector3.Multiply (ref vector2, (Half)(one / (RealMaths.Sqrt (num2))), out vector2);
			}
			Vector3 vector4 = rotateAxis;
			Vector3.Dot (ref rotateAxis, ref vector2, out num);

			Half realHorrid; RealMaths.FromString("0.9982547", out realHorrid);

			if (RealMaths.Abs (num) > realHorrid) {
				if (objectForwardVector.HasValue) {
					vector = objectForwardVector.Value;
					Vector3.Dot (ref rotateAxis, ref vector, out num);
					if (RealMaths.Abs (num) > realHorrid) {
						num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
						vector = (RealMaths.Abs (num) > realHorrid) ? Vector3.Right : Vector3.Forward;
					}
				} else {
					num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
					vector = (RealMaths.Abs (num) > realHorrid) ? Vector3.Right : Vector3.Forward;
				}
				Vector3.Cross (ref rotateAxis, ref vector, out vector3);
				vector3.Normalise ();
				Vector3.Cross (ref vector3, ref rotateAxis, out vector);
				vector.Normalise ();
			} else {
				Vector3.Cross (ref rotateAxis, ref vector2, out vector3);
				vector3.Normalise ();
				Vector3.Cross (ref vector3, ref vector4, out vector);
				vector.Normalise ();
			}
			result.M11 = vector3.X;
			result.M12 = vector3.Y;
			result.M13 = vector3.Z;
			result.M14 = zero;
			result.M21 = vector4.X;
			result.M22 = vector4.Y;
			result.M23 = vector4.Z;
			result.M24 = zero;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = zero;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = one;
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
		public static void CreatePerspectiveFieldOfView (Half fieldOfView, Half aspectRatio, Half nearPlaneDistance, Half farPlaneDistance, out Matrix44 result)
		{
			Half zero = 0;
			Half half; RealMaths.Half(out half);
			Half one; RealMaths.One(out one);
			Half pi; RealMaths.Pi(out pi);

			if ((fieldOfView <= zero) || (fieldOfView >= pi)) {
				throw new ArgumentOutOfRangeException ("fieldOfView");
			}
			if (nearPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			if (farPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			Half num = one / (RealMaths.Tan ((fieldOfView * half)));
			Half num9 = num / aspectRatio;
			result.M11 = num9;
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = num;
			result.M21 = result.M23 = result.M24 = zero;
			result.M31 = result.M32 = zero;
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -one;
			result.M41 = result.M42 = result.M44 = zero;
			result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
		public static void CreatePerspective (Half width, Half height, Half nearPlaneDistance, Half farPlaneDistance, out Matrix44 result)
		{
			Half zero = 0;
			Half one; RealMaths.One(out one);
			Half two = 2;

			if (nearPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			if (farPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			result.M11 = (two * nearPlaneDistance) / width;
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = (two * nearPlaneDistance) / height;
			result.M21 = result.M23 = result.M24 = zero;
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M31 = result.M32 = zero;
			result.M34 = -one;
			result.M41 = result.M42 = result.M44 = zero;
			result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
		}


		// ref: http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx
		public static void CreatePerspectiveOffCenter (Half left, Half right, Half bottom, Half top, Half nearPlaneDistance, Half farPlaneDistance, out Matrix44 result)
		{
			Half zero = 0;
			Half one; RealMaths.One(out one);
			Half two = 2;

			if (nearPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			if (farPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			result.M11 = (two * nearPlaneDistance) / (right - left);
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = (two * nearPlaneDistance) / (top - bottom);
			result.M21 = result.M23 = result.M24 = zero;
			result.M31 = (left + right) / (right - left);
			result.M32 = (top + bottom) / (top - bottom);
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -one;
			result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
			result.M41 = result.M42 = result.M44 = zero;
		}
		
		// ref: http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
		public static void CreateOrthographic (Half width, Half height, Half zNearPlane, Half zFarPlane, out Matrix44 result)
		{
			Half zero = 0;
			Half one; RealMaths.One(out one);
			Half two = 2;

			result.M11 = two / width;
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = two / height;
			result.M21 = result.M23 = result.M24 = zero;
			result.M33 = one / (zNearPlane - zFarPlane);
			result.M31 = result.M32 = result.M34 = zero;
			result.M41 = result.M42 = zero;
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = one;
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx
		public static void CreateOrthographicOffCenter (Half left, Half right, Half bottom, Half top, Half zNearPlane, Half zFarPlane, out Matrix44 result)
		{
			Half zero = 0;
			Half one; RealMaths.One(out one);
			Half two = 2;

			result.M11 = two / (right - left);
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = two / (top - bottom);
			result.M21 = result.M23 = result.M24 = zero;
			result.M33 = one / (zNearPlane - zFarPlane);
			result.M31 = result.M32 = result.M34 = zero;
			result.M41 = (left + right) / (left - right);
			result.M42 = (top + bottom) / (bottom - top);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = one;
		}
		
		// ref: http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx
		public static void CreateLookAt (ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix44 result)
		{
			Half zero = 0;
			Half one; RealMaths.One(out one);

			Vector3 targetToPosition = cameraPosition - cameraTarget;

			Vector3 vector; Vector3.Normalise (ref targetToPosition, out vector);

			Vector3 cross; Vector3.Cross (ref cameraUpVector, ref vector, out cross); 

			Vector3 vector2; Vector3.Normalise (ref cross, out vector2);
			Vector3 vector3; Vector3.Cross (ref vector, ref vector2, out vector3);
			result.M11 = vector2.X;
			result.M12 = vector3.X;
			result.M13 = vector.X;
			result.M14 = zero;
			result.M21 = vector2.Y;
			result.M22 = vector3.Y;
			result.M23 = vector.Y;
			result.M24 = zero;
			result.M31 = vector2.Z;
			result.M32 = vector3.Z;
			result.M33 = vector.Z;
			result.M34 = zero;

			Vector3.Dot (ref vector2, ref cameraPosition, out result.M41);
			Vector3.Dot (ref vector3, ref cameraPosition, out result.M42);
			Vector3.Dot (ref vector, ref cameraPosition, out result.M43);
			
			result.M41 *= -one;
			result.M42 *= -one;
			result.M43 *= -one;

			result.M44 = one;
		}

		
	

		// ref: http://msdn.microsoft.com/en-us/library/bb205364(v=VS.85).aspx
		public static void CreateShadow (ref Vector3 lightDirection, ref Plane plane, out Matrix44 result)
		{
			Half zero = 0;
			
			Plane plane2;
			Plane.Normalise (ref plane, out plane2);
			Half num = ((plane2.Normal.X * lightDirection.X) + (plane2.Normal.Y * lightDirection.Y)) + (plane2.Normal.Z * lightDirection.Z);
			Half num5 = -plane2.Normal.X;
			Half num4 = -plane2.Normal.Y;
			Half num3 = -plane2.Normal.Z;
			Half num2 = -plane2.D;
			result.M11 = (num5 * lightDirection.X) + num;
			result.M21 = num4 * lightDirection.X;
			result.M31 = num3 * lightDirection.X;
			result.M41 = num2 * lightDirection.X;
			result.M12 = num5 * lightDirection.Y;
			result.M22 = (num4 * lightDirection.Y) + num;
			result.M32 = num3 * lightDirection.Y;
			result.M42 = num2 * lightDirection.Y;
			result.M13 = num5 * lightDirection.Z;
			result.M23 = num4 * lightDirection.Z;
			result.M33 = (num3 * lightDirection.Z) + num;
			result.M43 = num2 * lightDirection.Z;
			result.M14 = zero;
			result.M24 = zero;
			result.M34 = zero;
			result.M44 = num;
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205356(v=VS.85).aspx
		public static void CreateReflection (ref Plane value, out Matrix44 result)
		{
			Half zero = 0;
			Half one; RealMaths.One(out one);
			Half two = 2;

			Plane plane;
			
			Plane.Normalise (ref value, out plane);
			
			value.Normalise ();
			
			Half x = plane.Normal.X;
			Half y = plane.Normal.Y;
			Half z = plane.Normal.Z;
			
			Half num3 = -two * x;
			Half num2 = -two * y;
			Half num = -two * z;
			
			result.M11 = (num3 * x) + one;
			result.M12 = num2 * x;
			result.M13 = num * x;
			result.M14 = zero;
			result.M21 = num3 * y;
			result.M22 = (num2 * y) + one;
			result.M23 = num * y;
			result.M24 = zero;
			result.M31 = num3 * z;
			result.M32 = num2 * z;
			result.M33 = (num * z) + one;
			result.M34 = zero;
			result.M41 = num3 * plane.D;
			result.M42 = num2 * plane.D;
			result.M43 = num * plane.D;
			result.M44 = one;
		}
		
		#endregion
		#region Maths

		//----------------------------------------------------------------------
		// Transpose
		//
		public void Transpose()
		{
			Half temp = this.M12;
			this.M12 = this.M21;
			this.M21 = temp;

			temp = this.M13;
			this.M13 = this.M31;
			this.M31 = temp;

			temp = this.M14;
			this.M14 = this.M41;
			this.M41 = temp;

			temp = this.M23;
			this.M23 = this.M32;
			this.M32 = temp;

			temp = this.M24;
			this.M24 = this.M42;
			this.M42 = temp;

			temp =  this.M34;
			this.M34 = this.M43;
			this.M43 = temp;
		}

		public static void Transpose (ref Matrix44 input, out Matrix44 output)
		{
		    output.M11 = input.M11;
			output.M12 = input.M21;
			output.M13 = input.M31;
			output.M14 = input.M41;
			output.M21 = input.M12;
			output.M22 = input.M22;
			output.M23 = input.M32;
			output.M24 = input.M42;
			output.M31 = input.M13;
			output.M32 = input.M23;
			output.M33 = input.M33;
			output.M34 = input.M43;
			output.M41 = input.M14;
			output.M42 = input.M24;
			output.M43 = input.M34;
			output.M44 = input.M44;
		}

		//----------------------------------------------------------------------
		// Decompose
		// ref: Essential Mathemathics For Games & Interactive Applications
		public bool Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
		{
			translation.X = M41;
            translation.Y = M42;
            translation.Z = M43;

			Vector3 a = new Vector3(M11, M21, M31);
			Vector3 b = new Vector3(M12, M22, M32);
			Vector3 c = new Vector3(M13, M23, M33);

			scale.X = a.Length();
			scale.Y = b.Length();
			scale.Z = c.Length();

			if ( RealMaths.IsZero(scale.X) || 
				 RealMaths.IsZero(scale.Y) || 
				 RealMaths.IsZero(scale.Z) )
            {
				rotation = Quaternion.Identity;
				return false;
			}

			a.Normalise();
			b.Normalise();
			c.Normalise();

			Vector3 right = new Vector3(a.X, b.X, c.X);
			Vector3 up = new Vector3(a.Y, b.Y, c.Y);
			Vector3 backward = new Vector3(a.Z, b.Z, c.Z);

			right.Normalise();
			up.Normalise();
			backward.Normalise();

			Matrix44 rotMat;
			Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out rotMat);

			Quaternion.CreateFromRotationMatrix(ref rotMat, out rotation);

			return true;
		}




		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////
		// TODO: REVIEW FROM HERE ONWARDS
		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////


		//----------------------------------------------------------------------
		// Determinant
		//
		public Half Determinant ()
		{
			Half num22 = this.M11;
			Half num21 = this.M12;
			Half num20 = this.M13;
			Half num19 = this.M14;
			Half num12 = this.M21;
			Half num11 = this.M22;
			Half num10 = this.M23;
			Half num9 = this.M24;
			Half num8 = this.M31;
			Half num7 = this.M32;
			Half num6 = this.M33;
			Half num5 = this.M34;
			Half num4 = this.M41;
			Half num3 = this.M42;
			Half num2 = this.M43;
			Half num = this.M44;
			
			Half num18 = (num6 * num) - (num5 * num2);
			Half num17 = (num7 * num) - (num5 * num3);
			Half num16 = (num7 * num2) - (num6 * num3);
			Half num15 = (num8 * num) - (num5 * num4);
			Half num14 = (num8 * num2) - (num6 * num4);
			Half num13 = (num8 * num3) - (num7 * num4);
			
			return ((((num22 * (((num11 * num18) - (num10 * num17)) + (num9 * num16))) - (num21 * (((num12 * num18) - (num10 * num15)) + (num9 * num14)))) + (num20 * (((num12 * num17) - (num11 * num15)) + (num9 * num13)))) - (num19 * (((num12 * num16) - (num11 * num14)) + (num10 * num13))));
		}
		
		//----------------------------------------------------------------------
		// Invert
		//
		public static void Invert (ref Matrix44 matrix, out Matrix44 result)
		{
			Half one = 1;
			Half num5 = matrix.M11;
			Half num4 = matrix.M12;
			Half num3 = matrix.M13;
			Half num2 = matrix.M14;
			Half num9 = matrix.M21;
			Half num8 = matrix.M22;
			Half num7 = matrix.M23;
			Half num6 = matrix.M24;
			Half num17 = matrix.M31;
			Half num16 = matrix.M32;
			Half num15 = matrix.M33;
			Half num14 = matrix.M34;
			Half num13 = matrix.M41;
			Half num12 = matrix.M42;
			Half num11 = matrix.M43;
			Half num10 = matrix.M44;
			Half num23 = (num15 * num10) - (num14 * num11);
			Half num22 = (num16 * num10) - (num14 * num12);
			Half num21 = (num16 * num11) - (num15 * num12);
			Half num20 = (num17 * num10) - (num14 * num13);
			Half num19 = (num17 * num11) - (num15 * num13);
			Half num18 = (num17 * num12) - (num16 * num13);
			Half num39 = ((num8 * num23) - (num7 * num22)) + (num6 * num21);
			Half num38 = -(((num9 * num23) - (num7 * num20)) + (num6 * num19));
			Half num37 = ((num9 * num22) - (num8 * num20)) + (num6 * num18);
			Half num36 = -(((num9 * num21) - (num8 * num19)) + (num7 * num18));
			Half num = one / ((((num5 * num39) + (num4 * num38)) + (num3 * num37)) + (num2 * num36));
			result.M11 = num39 * num;
			result.M21 = num38 * num;
			result.M31 = num37 * num;
			result.M41 = num36 * num;
			result.M12 = -(((num4 * num23) - (num3 * num22)) + (num2 * num21)) * num;
			result.M22 = (((num5 * num23) - (num3 * num20)) + (num2 * num19)) * num;
			result.M32 = -(((num5 * num22) - (num4 * num20)) + (num2 * num18)) * num;
			result.M42 = (((num5 * num21) - (num4 * num19)) + (num3 * num18)) * num;
			Half num35 = (num7 * num10) - (num6 * num11);
			Half num34 = (num8 * num10) - (num6 * num12);
			Half num33 = (num8 * num11) - (num7 * num12);
			Half num32 = (num9 * num10) - (num6 * num13);
			Half num31 = (num9 * num11) - (num7 * num13);
			Half num30 = (num9 * num12) - (num8 * num13);
			result.M13 = (((num4 * num35) - (num3 * num34)) + (num2 * num33)) * num;
			result.M23 = -(((num5 * num35) - (num3 * num32)) + (num2 * num31)) * num;
			result.M33 = (((num5 * num34) - (num4 * num32)) + (num2 * num30)) * num;
			result.M43 = -(((num5 * num33) - (num4 * num31)) + (num3 * num30)) * num;
			Half num29 = (num7 * num14) - (num6 * num15);
			Half num28 = (num8 * num14) - (num6 * num16);
			Half num27 = (num8 * num15) - (num7 * num16);
			Half num26 = (num9 * num14) - (num6 * num17);
			Half num25 = (num9 * num15) - (num7 * num17);
			Half num24 = (num9 * num16) - (num8 * num17);
			result.M14 = -(((num4 * num29) - (num3 * num28)) + (num2 * num27)) * num;
			result.M24 = (((num5 * num29) - (num3 * num26)) + (num2 * num25)) * num;
			result.M34 = -(((num5 * num28) - (num4 * num26)) + (num2 * num24)) * num;
			result.M44 = (((num5 * num27) - (num4 * num25)) + (num3 * num24)) * num;
		}


		//----------------------------------------------------------------------
		// Transform - Transforms a Matrix by applying a Quaternion rotation.
		//
		public static void Transform (ref Matrix44 value, ref Quaternion rotation, out Matrix44 result)
		{
			Half one = 1;

			Half num21 = rotation.X + rotation.X;
			Half num11 = rotation.Y + rotation.Y;
			Half num10 = rotation.Z + rotation.Z;
			
			Half num20 = rotation.W * num21;
			Half num19 = rotation.W * num11;
			Half num18 = rotation.W * num10;
			Half num17 = rotation.X * num21;
			Half num16 = rotation.X * num11;
			Half num15 = rotation.X * num10;
			Half num14 = rotation.Y * num11;
			Half num13 = rotation.Y * num10;
			Half num12 = rotation.Z * num10;
			
			Half num9 = (one - num14) - num12;
			
			Half num8 = num16 - num18;
			Half num7 = num15 + num19;
			Half num6 = num16 + num18;
			
			Half num5 = (one - num17) - num12;
			
			Half num4 = num13 - num20;
			Half num3 = num15 - num19;
			Half num2 = num13 + num20;
			
			Half num = (one - num17) - num14;
			
			Half num37 = ((value.M11 * num9) + (value.M12 * num8)) + (value.M13 * num7);
			Half num36 = ((value.M11 * num6) + (value.M12 * num5)) + (value.M13 * num4);
			Half num35 = ((value.M11 * num3) + (value.M12 * num2)) + (value.M13 * num);
			
			Half num34 = value.M14;
			
			Half num33 = ((value.M21 * num9) + (value.M22 * num8)) + (value.M23 * num7);
			Half num32 = ((value.M21 * num6) + (value.M22 * num5)) + (value.M23 * num4);
			Half num31 = ((value.M21 * num3) + (value.M22 * num2)) + (value.M23 * num);
			
			Half num30 = value.M24;
			
			Half num29 = ((value.M31 * num9) + (value.M32 * num8)) + (value.M33 * num7);
			Half num28 = ((value.M31 * num6) + (value.M32 * num5)) + (value.M33 * num4);
			Half num27 = ((value.M31 * num3) + (value.M32 * num2)) + (value.M33 * num);
			
			Half num26 = value.M34;
			
			Half num25 = ((value.M41 * num9) + (value.M42 * num8)) + (value.M43 * num7);
			Half num24 = ((value.M41 * num6) + (value.M42 * num5)) + (value.M43 * num4);
			Half num23 = ((value.M41 * num3) + (value.M42 * num2)) + (value.M43 * num);
			
			Half num22 = value.M44;
			
			result.M11 = num37;
			result.M12 = num36;
			result.M13 = num35;
			result.M14 = num34;
			result.M21 = num33;
			result.M22 = num32;
			result.M23 = num31;
			result.M24 = num30;
			result.M31 = num29;
			result.M32 = num28;
			result.M33 = num27;
			result.M34 = num26;
			result.M41 = num25;
			result.M42 = num24;
			result.M43 = num23;
			result.M44 = num22;
		}
		
		#endregion
		#region Operators
		
		public static Matrix44 operator - (Matrix44 matrix1)
		{
			Matrix44 matrix;
			matrix.M11 = -matrix1.M11;
			matrix.M12 = -matrix1.M12;
			matrix.M13 = -matrix1.M13;
			matrix.M14 = -matrix1.M14;
			matrix.M21 = -matrix1.M21;
			matrix.M22 = -matrix1.M22;
			matrix.M23 = -matrix1.M23;
			matrix.M24 = -matrix1.M24;
			matrix.M31 = -matrix1.M31;
			matrix.M32 = -matrix1.M32;
			matrix.M33 = -matrix1.M33;
			matrix.M34 = -matrix1.M34;
			matrix.M41 = -matrix1.M41;
			matrix.M42 = -matrix1.M42;
			matrix.M43 = -matrix1.M43;
			matrix.M44 = -matrix1.M44;
			return matrix;
		}
		
		public static Boolean operator == (Matrix44 matrix1, Matrix44 matrix2)
		{
			return ((((((matrix1.M11 == matrix2.M11) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M44 == matrix2.M44))) && (((matrix1.M12 == matrix2.M12) && (matrix1.M13 == matrix2.M13)) && ((matrix1.M14 == matrix2.M14) && (matrix1.M21 == matrix2.M21)))) && ((((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)) && ((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32))) && (((matrix1.M34 == matrix2.M34) && (matrix1.M41 == matrix2.M41)) && (matrix1.M42 == matrix2.M42)))) && (matrix1.M43 == matrix2.M43));
		}
		
		public static Boolean operator != (Matrix44 matrix1, Matrix44 matrix2)
		{
			if (((((matrix1.M11 == matrix2.M11) && (matrix1.M12 == matrix2.M12)) && ((matrix1.M13 == matrix2.M13) && (matrix1.M14 == matrix2.M14))) && (((matrix1.M21 == matrix2.M21) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)))) && ((((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M34 == matrix2.M34))) && (((matrix1.M41 == matrix2.M41) && (matrix1.M42 == matrix2.M42)) && (matrix1.M43 == matrix2.M43)))) {
				return !(matrix1.M44 == matrix2.M44);
			}
			return true;
		}
		
		public static Matrix44 operator + (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = matrix1.M11 + matrix2.M11;
			matrix.M12 = matrix1.M12 + matrix2.M12;
			matrix.M13 = matrix1.M13 + matrix2.M13;
			matrix.M14 = matrix1.M14 + matrix2.M14;
			matrix.M21 = matrix1.M21 + matrix2.M21;
			matrix.M22 = matrix1.M22 + matrix2.M22;
			matrix.M23 = matrix1.M23 + matrix2.M23;
			matrix.M24 = matrix1.M24 + matrix2.M24;
			matrix.M31 = matrix1.M31 + matrix2.M31;
			matrix.M32 = matrix1.M32 + matrix2.M32;
			matrix.M33 = matrix1.M33 + matrix2.M33;
			matrix.M34 = matrix1.M34 + matrix2.M34;
			matrix.M41 = matrix1.M41 + matrix2.M41;
			matrix.M42 = matrix1.M42 + matrix2.M42;
			matrix.M43 = matrix1.M43 + matrix2.M43;
			matrix.M44 = matrix1.M44 + matrix2.M44;
			return matrix;
		}
		
		public static Matrix44 operator - (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = matrix1.M11 - matrix2.M11;
			matrix.M12 = matrix1.M12 - matrix2.M12;
			matrix.M13 = matrix1.M13 - matrix2.M13;
			matrix.M14 = matrix1.M14 - matrix2.M14;
			matrix.M21 = matrix1.M21 - matrix2.M21;
			matrix.M22 = matrix1.M22 - matrix2.M22;
			matrix.M23 = matrix1.M23 - matrix2.M23;
			matrix.M24 = matrix1.M24 - matrix2.M24;
			matrix.M31 = matrix1.M31 - matrix2.M31;
			matrix.M32 = matrix1.M32 - matrix2.M32;
			matrix.M33 = matrix1.M33 - matrix2.M33;
			matrix.M34 = matrix1.M34 - matrix2.M34;
			matrix.M41 = matrix1.M41 - matrix2.M41;
			matrix.M42 = matrix1.M42 - matrix2.M42;
			matrix.M43 = matrix1.M43 - matrix2.M43;
			matrix.M44 = matrix1.M44 - matrix2.M44;
			return matrix;
		}
		
		public static Matrix44 operator * (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = (((matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21)) + (matrix1.M13 * matrix2.M31)) + (matrix1.M14 * matrix2.M41);
			matrix.M12 = (((matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22)) + (matrix1.M13 * matrix2.M32)) + (matrix1.M14 * matrix2.M42);
			matrix.M13 = (((matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23)) + (matrix1.M13 * matrix2.M33)) + (matrix1.M14 * matrix2.M43);
			matrix.M14 = (((matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24)) + (matrix1.M13 * matrix2.M34)) + (matrix1.M14 * matrix2.M44);
			matrix.M21 = (((matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21)) + (matrix1.M23 * matrix2.M31)) + (matrix1.M24 * matrix2.M41);
			matrix.M22 = (((matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22)) + (matrix1.M23 * matrix2.M32)) + (matrix1.M24 * matrix2.M42);
			matrix.M23 = (((matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23)) + (matrix1.M23 * matrix2.M33)) + (matrix1.M24 * matrix2.M43);
			matrix.M24 = (((matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24)) + (matrix1.M23 * matrix2.M34)) + (matrix1.M24 * matrix2.M44);
			matrix.M31 = (((matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21)) + (matrix1.M33 * matrix2.M31)) + (matrix1.M34 * matrix2.M41);
			matrix.M32 = (((matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22)) + (matrix1.M33 * matrix2.M32)) + (matrix1.M34 * matrix2.M42);
			matrix.M33 = (((matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23)) + (matrix1.M33 * matrix2.M33)) + (matrix1.M34 * matrix2.M43);
			matrix.M34 = (((matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24)) + (matrix1.M33 * matrix2.M34)) + (matrix1.M34 * matrix2.M44);
			matrix.M41 = (((matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21)) + (matrix1.M43 * matrix2.M31)) + (matrix1.M44 * matrix2.M41);
			matrix.M42 = (((matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22)) + (matrix1.M43 * matrix2.M32)) + (matrix1.M44 * matrix2.M42);
			matrix.M43 = (((matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23)) + (matrix1.M43 * matrix2.M33)) + (matrix1.M44 * matrix2.M43);
			matrix.M44 = (((matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24)) + (matrix1.M43 * matrix2.M34)) + (matrix1.M44 * matrix2.M44);
			return matrix;
		}
		
		public static Matrix44 operator * (Matrix44 matrix, Half scaleFactor)
		{
			Matrix44 matrix2;
			Half num = scaleFactor;
			matrix2.M11 = matrix.M11 * num;
			matrix2.M12 = matrix.M12 * num;
			matrix2.M13 = matrix.M13 * num;
			matrix2.M14 = matrix.M14 * num;
			matrix2.M21 = matrix.M21 * num;
			matrix2.M22 = matrix.M22 * num;
			matrix2.M23 = matrix.M23 * num;
			matrix2.M24 = matrix.M24 * num;
			matrix2.M31 = matrix.M31 * num;
			matrix2.M32 = matrix.M32 * num;
			matrix2.M33 = matrix.M33 * num;
			matrix2.M34 = matrix.M34 * num;
			matrix2.M41 = matrix.M41 * num;
			matrix2.M42 = matrix.M42 * num;
			matrix2.M43 = matrix.M43 * num;
			matrix2.M44 = matrix.M44 * num;
			return matrix2;
		}
		
		public static Matrix44 operator * (Half scaleFactor, Matrix44 matrix)
		{
			Matrix44 matrix2;
			Half num = scaleFactor;
			matrix2.M11 = matrix.M11 * num;
			matrix2.M12 = matrix.M12 * num;
			matrix2.M13 = matrix.M13 * num;
			matrix2.M14 = matrix.M14 * num;
			matrix2.M21 = matrix.M21 * num;
			matrix2.M22 = matrix.M22 * num;
			matrix2.M23 = matrix.M23 * num;
			matrix2.M24 = matrix.M24 * num;
			matrix2.M31 = matrix.M31 * num;
			matrix2.M32 = matrix.M32 * num;
			matrix2.M33 = matrix.M33 * num;
			matrix2.M34 = matrix.M34 * num;
			matrix2.M41 = matrix.M41 * num;
			matrix2.M42 = matrix.M42 * num;
			matrix2.M43 = matrix.M43 * num;
			matrix2.M44 = matrix.M44 * num;
			return matrix2;
		}
		
		public static Matrix44 operator / (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = matrix1.M11 / matrix2.M11;
			matrix.M12 = matrix1.M12 / matrix2.M12;
			matrix.M13 = matrix1.M13 / matrix2.M13;
			matrix.M14 = matrix1.M14 / matrix2.M14;
			matrix.M21 = matrix1.M21 / matrix2.M21;
			matrix.M22 = matrix1.M22 / matrix2.M22;
			matrix.M23 = matrix1.M23 / matrix2.M23;
			matrix.M24 = matrix1.M24 / matrix2.M24;
			matrix.M31 = matrix1.M31 / matrix2.M31;
			matrix.M32 = matrix1.M32 / matrix2.M32;
			matrix.M33 = matrix1.M33 / matrix2.M33;
			matrix.M34 = matrix1.M34 / matrix2.M34;
			matrix.M41 = matrix1.M41 / matrix2.M41;
			matrix.M42 = matrix1.M42 / matrix2.M42;
			matrix.M43 = matrix1.M43 / matrix2.M43;
			matrix.M44 = matrix1.M44 / matrix2.M44;
			return matrix;
		}
		
		public static Matrix44 operator / (Matrix44 matrix1, Half divider)
		{
			Matrix44 matrix;
			Half one = 1;
			Half num = one / divider;
			matrix.M11 = matrix1.M11 * num;
			matrix.M12 = matrix1.M12 * num;
			matrix.M13 = matrix1.M13 * num;
			matrix.M14 = matrix1.M14 * num;
			matrix.M21 = matrix1.M21 * num;
			matrix.M22 = matrix1.M22 * num;
			matrix.M23 = matrix1.M23 * num;
			matrix.M24 = matrix1.M24 * num;
			matrix.M31 = matrix1.M31 * num;
			matrix.M32 = matrix1.M32 * num;
			matrix.M33 = matrix1.M33 * num;
			matrix.M34 = matrix1.M34 * num;
			matrix.M41 = matrix1.M41 * num;
			matrix.M42 = matrix1.M42 * num;
			matrix.M43 = matrix1.M43 * num;
			matrix.M44 = matrix1.M44 * num;
			return matrix;
		}
		
		public static void Negate (ref Matrix44 matrix, out Matrix44 result)
		{
			result.M11 = -matrix.M11;
			result.M12 = -matrix.M12;
			result.M13 = -matrix.M13;
			result.M14 = -matrix.M14;
			result.M21 = -matrix.M21;
			result.M22 = -matrix.M22;
			result.M23 = -matrix.M23;
			result.M24 = -matrix.M24;
			result.M31 = -matrix.M31;
			result.M32 = -matrix.M32;
			result.M33 = -matrix.M33;
			result.M34 = -matrix.M34;
			result.M41 = -matrix.M41;
			result.M42 = -matrix.M42;
			result.M43 = -matrix.M43;
			result.M44 = -matrix.M44;
		}
		
		public static void Add (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			result.M11 = matrix1.M11 + matrix2.M11;
			result.M12 = matrix1.M12 + matrix2.M12;
			result.M13 = matrix1.M13 + matrix2.M13;
			result.M14 = matrix1.M14 + matrix2.M14;
			result.M21 = matrix1.M21 + matrix2.M21;
			result.M22 = matrix1.M22 + matrix2.M22;
			result.M23 = matrix1.M23 + matrix2.M23;
			result.M24 = matrix1.M24 + matrix2.M24;
			result.M31 = matrix1.M31 + matrix2.M31;
			result.M32 = matrix1.M32 + matrix2.M32;
			result.M33 = matrix1.M33 + matrix2.M33;
			result.M34 = matrix1.M34 + matrix2.M34;
			result.M41 = matrix1.M41 + matrix2.M41;
			result.M42 = matrix1.M42 + matrix2.M42;
			result.M43 = matrix1.M43 + matrix2.M43;
			result.M44 = matrix1.M44 + matrix2.M44;
		}
		
		public static void Subtract (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			result.M11 = matrix1.M11 - matrix2.M11;
			result.M12 = matrix1.M12 - matrix2.M12;
			result.M13 = matrix1.M13 - matrix2.M13;
			result.M14 = matrix1.M14 - matrix2.M14;
			result.M21 = matrix1.M21 - matrix2.M21;
			result.M22 = matrix1.M22 - matrix2.M22;
			result.M23 = matrix1.M23 - matrix2.M23;
			result.M24 = matrix1.M24 - matrix2.M24;
			result.M31 = matrix1.M31 - matrix2.M31;
			result.M32 = matrix1.M32 - matrix2.M32;
			result.M33 = matrix1.M33 - matrix2.M33;
			result.M34 = matrix1.M34 - matrix2.M34;
			result.M41 = matrix1.M41 - matrix2.M41;
			result.M42 = matrix1.M42 - matrix2.M42;
			result.M43 = matrix1.M43 - matrix2.M43;
			result.M44 = matrix1.M44 - matrix2.M44;
		}
		
		public static void Multiply (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			Half num16 = (((matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21)) + (matrix1.M13 * matrix2.M31)) + (matrix1.M14 * matrix2.M41);
			Half num15 = (((matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22)) + (matrix1.M13 * matrix2.M32)) + (matrix1.M14 * matrix2.M42);
			Half num14 = (((matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23)) + (matrix1.M13 * matrix2.M33)) + (matrix1.M14 * matrix2.M43);
			Half num13 = (((matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24)) + (matrix1.M13 * matrix2.M34)) + (matrix1.M14 * matrix2.M44);
			Half num12 = (((matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21)) + (matrix1.M23 * matrix2.M31)) + (matrix1.M24 * matrix2.M41);
			Half num11 = (((matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22)) + (matrix1.M23 * matrix2.M32)) + (matrix1.M24 * matrix2.M42);
			Half num10 = (((matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23)) + (matrix1.M23 * matrix2.M33)) + (matrix1.M24 * matrix2.M43);
			Half num9 = (((matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24)) + (matrix1.M23 * matrix2.M34)) + (matrix1.M24 * matrix2.M44);
			Half num8 = (((matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21)) + (matrix1.M33 * matrix2.M31)) + (matrix1.M34 * matrix2.M41);
			Half num7 = (((matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22)) + (matrix1.M33 * matrix2.M32)) + (matrix1.M34 * matrix2.M42);
			Half num6 = (((matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23)) + (matrix1.M33 * matrix2.M33)) + (matrix1.M34 * matrix2.M43);
			Half num5 = (((matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24)) + (matrix1.M33 * matrix2.M34)) + (matrix1.M34 * matrix2.M44);
			Half num4 = (((matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21)) + (matrix1.M43 * matrix2.M31)) + (matrix1.M44 * matrix2.M41);
			Half num3 = (((matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22)) + (matrix1.M43 * matrix2.M32)) + (matrix1.M44 * matrix2.M42);
			Half num2 = (((matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23)) + (matrix1.M43 * matrix2.M33)) + (matrix1.M44 * matrix2.M43);
			Half num = (((matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24)) + (matrix1.M43 * matrix2.M34)) + (matrix1.M44 * matrix2.M44);
			result.M11 = num16;
			result.M12 = num15;
			result.M13 = num14;
			result.M14 = num13;
			result.M21 = num12;
			result.M22 = num11;
			result.M23 = num10;
			result.M24 = num9;
			result.M31 = num8;
			result.M32 = num7;
			result.M33 = num6;
			result.M34 = num5;
			result.M41 = num4;
			result.M42 = num3;
			result.M43 = num2;
			result.M44 = num;
		}

		public static void Multiply (ref Matrix44 matrix1, Half scaleFactor, out Matrix44 result)
		{
			Half num = scaleFactor;
			result.M11 = matrix1.M11 * num;
			result.M12 = matrix1.M12 * num;
			result.M13 = matrix1.M13 * num;
			result.M14 = matrix1.M14 * num;
			result.M21 = matrix1.M21 * num;
			result.M22 = matrix1.M22 * num;
			result.M23 = matrix1.M23 * num;
			result.M24 = matrix1.M24 * num;
			result.M31 = matrix1.M31 * num;
			result.M32 = matrix1.M32 * num;
			result.M33 = matrix1.M33 * num;
			result.M34 = matrix1.M34 * num;
			result.M41 = matrix1.M41 * num;
			result.M42 = matrix1.M42 * num;
			result.M43 = matrix1.M43 * num;
			result.M44 = matrix1.M44 * num;
		}

		public static void Divide (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			result.M11 = matrix1.M11 / matrix2.M11;
			result.M12 = matrix1.M12 / matrix2.M12;
			result.M13 = matrix1.M13 / matrix2.M13;
			result.M14 = matrix1.M14 / matrix2.M14;
			result.M21 = matrix1.M21 / matrix2.M21;
			result.M22 = matrix1.M22 / matrix2.M22;
			result.M23 = matrix1.M23 / matrix2.M23;
			result.M24 = matrix1.M24 / matrix2.M24;
			result.M31 = matrix1.M31 / matrix2.M31;
			result.M32 = matrix1.M32 / matrix2.M32;
			result.M33 = matrix1.M33 / matrix2.M33;
			result.M34 = matrix1.M34 / matrix2.M34;
			result.M41 = matrix1.M41 / matrix2.M41;
			result.M42 = matrix1.M42 / matrix2.M42;
			result.M43 = matrix1.M43 / matrix2.M43;
			result.M44 = matrix1.M44 / matrix2.M44;
		}
		
		public static void Divide (ref Matrix44 matrix1, Half divider, out Matrix44 result)
		{
			Half one = 1;

			Half num = one / divider;
			result.M11 = matrix1.M11 * num;
			result.M12 = matrix1.M12 * num;
			result.M13 = matrix1.M13 * num;
			result.M14 = matrix1.M14 * num;
			result.M21 = matrix1.M21 * num;
			result.M22 = matrix1.M22 * num;
			result.M23 = matrix1.M23 * num;
			result.M24 = matrix1.M24 * num;
			result.M31 = matrix1.M31 * num;
			result.M32 = matrix1.M32 * num;
			result.M33 = matrix1.M33 * num;
			result.M34 = matrix1.M34 * num;
			result.M41 = matrix1.M41 * num;
			result.M42 = matrix1.M42 * num;
			result.M43 = matrix1.M43 * num;
			result.M44 = matrix1.M44 * num;
		}

		#endregion
		#region Utilities

		// beware, doing this might not produce what you expect.  you likely
		// want to lerp between quaternions.
		public static void Lerp (ref Matrix44 matrix1, ref Matrix44 matrix2, Half amount, out Matrix44 result)
		{
			result.M11 = matrix1.M11 + ((matrix2.M11 - matrix1.M11) * amount);
			result.M12 = matrix1.M12 + ((matrix2.M12 - matrix1.M12) * amount);
			result.M13 = matrix1.M13 + ((matrix2.M13 - matrix1.M13) * amount);
			result.M14 = matrix1.M14 + ((matrix2.M14 - matrix1.M14) * amount);
			result.M21 = matrix1.M21 + ((matrix2.M21 - matrix1.M21) * amount);
			result.M22 = matrix1.M22 + ((matrix2.M22 - matrix1.M22) * amount);
			result.M23 = matrix1.M23 + ((matrix2.M23 - matrix1.M23) * amount);
			result.M24 = matrix1.M24 + ((matrix2.M24 - matrix1.M24) * amount);
			result.M31 = matrix1.M31 + ((matrix2.M31 - matrix1.M31) * amount);
			result.M32 = matrix1.M32 + ((matrix2.M32 - matrix1.M32) * amount);
			result.M33 = matrix1.M33 + ((matrix2.M33 - matrix1.M33) * amount);
			result.M34 = matrix1.M34 + ((matrix2.M34 - matrix1.M34) * amount);
			result.M41 = matrix1.M41 + ((matrix2.M41 - matrix1.M41) * amount);
			result.M42 = matrix1.M42 + ((matrix2.M42 - matrix1.M42) * amount);
			result.M43 = matrix1.M43 + ((matrix2.M43 - matrix1.M43) * amount);
			result.M44 = matrix1.M44 + ((matrix2.M44 - matrix1.M44) * amount);
		}
		
		#endregion
		
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Plane 
		: IEquatable<Plane>
	{
		public Vector3 Normal;
		public Half D;

		public Plane (Half a, Half b, Half c, Half d)
		{
			this.Normal.X = a;
			this.Normal.Y = b;
			this.Normal.Z = c;
			this.D = d;
		}

		public Plane (Vector3 normal, Half d)
		{
			this.Normal = normal;
			this.D = d;
		}

		public Plane (Vector4 value)
		{
			this.Normal.X = value.X;
			this.Normal.Y = value.Y;
			this.Normal.Z = value.Z;
			this.D = value.W;
		}

		public Plane (Vector3 point1, Vector3 point2, Vector3 point3)
		{
			Half one = 1;

			Half num10 = point2.X - point1.X;
			Half num9 = point2.Y - point1.Y;
			Half num8 = point2.Z - point1.Z;
			Half num7 = point3.X - point1.X;
			Half num6 = point3.Y - point1.Y;
			Half num5 = point3.Z - point1.Z;
			Half num4 = (num9 * num5) - (num8 * num6);
			Half num3 = (num8 * num7) - (num10 * num5);
			Half num2 = (num10 * num6) - (num9 * num7);
			Half num11 = ((num4 * num4) + (num3 * num3)) + (num2 * num2);
			Half num = one / RealMaths.Sqrt (num11);
			this.Normal.X = num4 * num;
			this.Normal.Y = num3 * num;
			this.Normal.Z = num2 * num;
			this.D = -(((this.Normal.X * point1.X) + (this.Normal.Y * point1.Y)) + (this.Normal.Z * point1.Z));
		}

		public Boolean Equals (Plane other)
		{
			return ((((this.Normal.X == other.Normal.X) && (this.Normal.Y == other.Normal.Y)) && (this.Normal.Z == other.Normal.Z)) && (this.D == other.D));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Plane) {
				flag = this.Equals ((Plane)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Normal.GetHashCode () + this.D.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Normal:{0} D:{1}}}", new Object[] { this.Normal.ToString (), this.D.ToString () });
		}

		public void Normalise ()
		{
			Half one = 1;
			Half somethingWicked; RealMaths.FromString("0.0000001192093", out somethingWicked);

			Half num2 = ((this.Normal.X * this.Normal.X) + (this.Normal.Y * this.Normal.Y)) + (this.Normal.Z * this.Normal.Z);
			if (RealMaths.Abs (num2 - one) >= somethingWicked) {
				Half num = one / RealMaths.Sqrt (num2);
				this.Normal.X *= num;
				this.Normal.Y *= num;
				this.Normal.Z *= num;
				this.D *= num;
			}
		}

		public static void Normalise (ref Plane value, out Plane result)
		{
			Half one = 1;
			Half somethingWicked; RealMaths.FromString("0.0000001192093", out somethingWicked);

			Half num2 = ((value.Normal.X * value.Normal.X) + (value.Normal.Y * value.Normal.Y)) + (value.Normal.Z * value.Normal.Z);
			if (RealMaths.Abs (num2 - one) < somethingWicked) {
				result.Normal = value.Normal;
				result.D = value.D;
			} else {
				Half num = one / RealMaths.Sqrt (num2);
				result.Normal.X = value.Normal.X * num;
				result.Normal.Y = value.Normal.Y * num;
				result.Normal.Z = value.Normal.Z * num;
				result.D = value.D * num;
			}
		}

		public static void Transform (ref Plane plane, ref Matrix44 matrix, out Plane result)
		{
			Matrix44 matrix2;
			Matrix44.Invert (ref matrix, out matrix2);
			Half x = plane.Normal.X;
			Half y = plane.Normal.Y;
			Half z = plane.Normal.Z;
			Half d = plane.D;
			result.Normal.X = (((x * matrix2.M11) + (y * matrix2.M12)) + (z * matrix2.M13)) + (d * matrix2.M14);
			result.Normal.Y = (((x * matrix2.M21) + (y * matrix2.M22)) + (z * matrix2.M23)) + (d * matrix2.M24);
			result.Normal.Z = (((x * matrix2.M31) + (y * matrix2.M32)) + (z * matrix2.M33)) + (d * matrix2.M34);
			result.D = (((x * matrix2.M41) + (y * matrix2.M42)) + (z * matrix2.M43)) + (d * matrix2.M44);
		}


		public static void Transform (ref Plane plane, ref Quaternion rotation, out Plane result)
		{
			Half one = 1;

			Half num15 = rotation.X + rotation.X;
			Half num5 = rotation.Y + rotation.Y;
			Half num = rotation.Z + rotation.Z;
			Half num14 = rotation.W * num15;
			Half num13 = rotation.W * num5;
			Half num12 = rotation.W * num;
			Half num11 = rotation.X * num15;
			Half num10 = rotation.X * num5;
			Half num9 = rotation.X * num;
			Half num8 = rotation.Y * num5;
			Half num7 = rotation.Y * num;
			Half num6 = rotation.Z * num;
			Half num24 = (one - num8) - num6;
			Half num23 = num10 - num12;
			Half num22 = num9 + num13;
			Half num21 = num10 + num12;
			Half num20 = (one - num11) - num6;
			Half num19 = num7 - num14;
			Half num18 = num9 - num13;
			Half num17 = num7 + num14;
			Half num16 = (one - num11) - num8;
			Half x = plane.Normal.X;
			Half y = plane.Normal.Y;
			Half z = plane.Normal.Z;
			result.Normal.X = ((x * num24) + (y * num23)) + (z * num22);
			result.Normal.Y = ((x * num21) + (y * num20)) + (z * num19);
			result.Normal.Z = ((x * num18) + (y * num17)) + (z * num16);
			result.D = plane.D;
		}
		


		public Half Dot(ref Vector4 value)
		{
			return (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + (this.D * value.W);
		}

		public Half DotCoordinate (ref Vector3 value)
		{
			return (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + this.D;
		}

		public Half DotNormal (ref Vector3 value)
		{
			return ((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z);
		}

		public PlaneIntersectionType Intersects (ref BoundingBox box)
		{
			Half zero = 0;

			Vector3 vector;
			Vector3 vector2;
			vector2.X = (this.Normal.X >= zero) ? box.Min.X : box.Max.X;
			vector2.Y = (this.Normal.Y >= zero) ? box.Min.Y : box.Max.Y;
			vector2.Z = (this.Normal.Z >= zero) ? box.Min.Z : box.Max.Z;
			vector.X = (this.Normal.X >= zero) ? box.Max.X : box.Min.X;
			vector.Y = (this.Normal.Y >= zero) ? box.Max.Y : box.Min.Y;
			vector.Z = (this.Normal.Z >= zero) ? box.Max.Z : box.Min.Z;
			Half num = ((this.Normal.X * vector2.X) + (this.Normal.Y * vector2.Y)) + (this.Normal.Z * vector2.Z);
			if ((num + this.D) > zero) {
				return PlaneIntersectionType.Front;
			} else {
				num = ((this.Normal.X * vector.X) + (this.Normal.Y * vector.Y)) + (this.Normal.Z * vector.Z);
				if ((num + this.D) < zero) {
					return PlaneIntersectionType.Back;
				} else {
					return PlaneIntersectionType.Intersecting;
				}
			}
		}

		public PlaneIntersectionType Intersects (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			return frustum.Intersects (ref this);
		}

		public PlaneIntersectionType Intersects (ref BoundingSphere sphere)
		{
			Half num2 = ((sphere.Center.X * this.Normal.X) + (sphere.Center.Y * this.Normal.Y)) + (sphere.Center.Z * this.Normal.Z);
			Half num = num2 + this.D;
			if (num > sphere.Radius) {
				return PlaneIntersectionType.Front;
			} else if (num < -sphere.Radius) {
				return PlaneIntersectionType.Back;
			} else {
				return PlaneIntersectionType.Intersecting;
			}
		}

		public static Boolean operator == (Plane lhs, Plane rhs)
		{
			return lhs.Equals (rhs);
		}

		public static Boolean operator != (Plane lhs, Plane rhs)
		{
			if (((lhs.Normal.X == rhs.Normal.X) && (lhs.Normal.Y == rhs.Normal.Y)) && (lhs.Normal.Z == rhs.Normal.Z)) {
				return !(lhs.D == rhs.D);
			}
			return true;
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Quad
		: IEquatable<Quad>
	{
		public Vector3 A
		{
			get
			{
				return tri1.A;
			}
			set
			{
				tri1.A = value;
			}
		}

		public Vector3 B
		{
			get
			{
				return tri1.B;
			}
			set
			{
				tri1.B = value;
				tri2.B = value;
			}
		}

		public Vector3 C
		{
			get
			{
				return tri2.C;
			}
			set
			{
				tri1.C = value;
				tri2.C = value;
			}
		}

		public Vector3 D
		{
			get
			{
				return tri2.A;
			}
			set
			{
				tri1.A = value;
				tri2.A = value;
			}
		}

		Triangle tri1;
		Triangle tri2;

		public Quad (Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			this.tri1 = new Triangle(a, b, c);
			this.tri2 = new Triangle(d, b, c);
		}

		// Determines whether or not this Quad is equal in value to another Quad
		public Boolean Equals (Quad other)
		{
			if (this.A.X != other.A.X) return false;
			if (this.A.Y != other.A.Y) return false;
			if (this.A.Z != other.A.Z) return false;

			if (this.B.X != other.B.X) return false;
			if (this.B.Y != other.B.Y) return false;
			if (this.B.Z != other.B.Z) return false;

			if (this.C.X != other.C.X) return false;
			if (this.C.Y != other.C.Y) return false;
			if (this.C.Z != other.C.Z) return false;
			
			if (this.D.X != other.D.X) return false;
			if (this.D.Y != other.D.Y) return false;
			if (this.D.Z != other.D.Z) return false;

			// They match!
			return true;
		}

		// Determines whether or not this Quad is equal in value to another System.Object
		public override Boolean Equals (Object obj)
		{
			if (obj == null) return false;

			if (obj is Quad)
			{
				// Ok, it is a Quad, so just use the method above to compare.
				return this.Equals ((Quad) obj);
			}

			return false;
		}

		public override Int32 GetHashCode ()
		{
			return (this.A.GetHashCode () + this.B.GetHashCode () + this.C.GetHashCode () + this.D.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{A:{0} B:{1} C:{2} D:{3}}}", this.A, this.B, this.C, this.D);
		}

		public static Boolean operator == (Quad a, Quad b)
		{
			return a.Equals(b);
		}

		public static Boolean operator != (Quad a, Quad b)
		{
			return !a.Equals(b);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Quaternion 
		: IEquatable<Quaternion>
	{
		public Half X;
		public Half Y;
		public Half Z;
		public Half W;


		public Quaternion (Half x, Half y, Half z, Half w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		public Quaternion (Vector3 vectorPart, Half scalarPart)
		{
			this.X = vectorPart.X;
			this.Y = vectorPart.Y;
			this.Z = vectorPart.Z;
			this.W = scalarPart;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString (), this.W.ToString () });
		}

		public Boolean Equals (Quaternion other)
		{
			return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
		}

		public override Boolean Equals (Object obj)
		{

			Boolean flag = false;
			if (obj is Quaternion)
			{
				flag = this.Equals ((Quaternion)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ()) + this.W.GetHashCode ());
		}

		public Half LengthSquared ()
		{
			return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
		}

		public Half Length ()
		{
			Half num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			return RealMaths.Sqrt (num);
		}

		public void Normalise ()
		{
			Half one = 1;
			Half num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			Half num = one / RealMaths.Sqrt (num2);
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
			this.W *= num;
		}

		public Boolean IsUnit()
		{
			Half one = 1;

			return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
		}

		public void Conjugate ()
		{
			this.X = -this.X;
			this.Y = -this.Y;
			this.Z = -this.Z;
		}

		#region Constants

		static Quaternion identity;
		
		public static Quaternion Identity
		{
			get
			{
				return identity;
			}
		}

		static Quaternion ()
		{
			Half temp_one; RealMaths.One(out temp_one);
			Half temp_zero; RealMaths.Zero(out temp_zero);
			identity = new Quaternion (temp_zero, temp_zero, temp_zero, temp_one);
		}
		
		#endregion
		#region Create

		public static void CreateFromAxisAngle (ref Vector3 axis, Half angle, out Quaternion result)
		{
			Half half; RealMaths.Half(out half);
			Half theta = angle * half;

			Half sin = RealMaths.Sin (theta);
			Half cos = RealMaths.Cos (theta);

			result.X = axis.X * sin;
			result.Y = axis.Y * sin;
			result.Z = axis.Z * sin;

			result.W = cos;
		}
		
		public static void CreateFromYawPitchRoll (Half yaw, Half pitch, Half roll, out Quaternion result)
		{
			Half half; RealMaths.Half(out half);
			Half num9 = roll * half;

			Half num6 = RealMaths.Sin (num9);
			Half num5 = RealMaths.Cos (num9);

			Half num8 = pitch * half;

			Half num4 = RealMaths.Sin (num8);
			Half num3 = RealMaths.Cos (num8);

			Half num7 = yaw * half;

			Half num2 = RealMaths.Sin (num7);
			Half num = RealMaths.Cos (num7);

			result.X = ((num * num4) * num5) + ((num2 * num3) * num6);
			result.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
			result.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
			result.W = ((num * num3) * num5) + ((num2 * num4) * num6);
		}
		
		public static void CreateFromRotationMatrix (ref Matrix44 matrix, out Quaternion result)
		{
			Half zero = 0;
			Half half; RealMaths.Half(out half);
			Half one = 1;

			Half num8 = (matrix.M11 + matrix.M22) + matrix.M33;

			if (num8 > zero)
			{
				Half num = RealMaths.Sqrt (num8 + one);
				result.W = num * half;
				num = half / num;
				result.X = (matrix.M23 - matrix.M32) * num;
				result.Y = (matrix.M31 - matrix.M13) * num;
				result.Z = (matrix.M12 - matrix.M21) * num;
			}
			else if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
			{
				Half num7 = RealMaths.Sqrt (((one + matrix.M11) - matrix.M22) - matrix.M33);
				Half num4 = half / num7;
				result.X = half * num7;
				result.Y = (matrix.M12 + matrix.M21) * num4;
				result.Z = (matrix.M13 + matrix.M31) * num4;
				result.W = (matrix.M23 - matrix.M32) * num4;
			}
			else if (matrix.M22 > matrix.M33)
			{
				Half num6 =RealMaths.Sqrt (((one + matrix.M22) - matrix.M11) - matrix.M33);
				Half num3 = half / num6;
				result.X = (matrix.M21 + matrix.M12) * num3;
				result.Y = half * num6;
				result.Z = (matrix.M32 + matrix.M23) * num3;
				result.W = (matrix.M31 - matrix.M13) * num3;
			}
			else
			{
				Half num5 = RealMaths.Sqrt (((one + matrix.M33) - matrix.M11) - matrix.M22);
				Half num2 = half / num5;
				result.X = (matrix.M31 + matrix.M13) * num2;
				result.Y = (matrix.M32 + matrix.M23) * num2;
				result.Z = half * num5;
				result.W = (matrix.M12 - matrix.M21) * num2;
			}
		}
		
		#endregion
		#region Maths

		public static void Conjugate (ref Quaternion value, out Quaternion result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = value.W;
		}
		
		public static void Inverse (ref Quaternion quaternion, out Quaternion result)
		{
			Half one = 1;
			Half num2 = ( ( (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) ) + 
			                (quaternion.Z * quaternion.Z) ) + (quaternion.W * quaternion.W);

			Half num = one / num2;

			result.X = -quaternion.X * num;
			result.Y = -quaternion.Y * num;
			result.Z = -quaternion.Z * num;
			result.W = quaternion.W * num;
		}
		
		
		public static void Dot (ref Quaternion quaternion1, ref Quaternion quaternion2, out Half result)
		{
			result = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + 
			          (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
		}


		public static void Concatenate (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
		{
			Half x = value2.X;
			Half y = value2.Y;
			Half z = value2.Z;
			Half w = value2.W;
			Half num4 = value1.X;
			Half num3 = value1.Y;
			Half num2 = value1.Z;
			Half num = value1.W;
			Half num12 = (y * num2) - (z * num3);
			Half num11 = (z * num4) - (x * num2);
			Half num10 = (x * num3) - (y * num4);
			Half num9 = ((x * num4) + (y * num3)) + (z * num2);
			result.X = ((x * num) + (num4 * w)) + num12;
			result.Y = ((y * num) + (num3 * w)) + num11;
			result.Z = ((z * num) + (num2 * w)) + num10;
			result.W = (w * num) - num9;
		}
		
		public static void Normalise (ref Quaternion quaternion, out Quaternion result)
		{
			Half one = 1;

			Half num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
			Half num = one / RealMaths.Sqrt (num2);
			result.X = quaternion.X * num;
			result.Y = quaternion.Y * num;
			result.Z = quaternion.Z * num;
			result.W = quaternion.W * num;
		}
		
		#endregion
		#region Operators

		public static Quaternion operator - (Quaternion quaternion)
		{
			Quaternion quaternion2;
			quaternion2.X = -quaternion.X;
			quaternion2.Y = -quaternion.Y;
			quaternion2.Z = -quaternion.Z;
			quaternion2.W = -quaternion.W;
			return quaternion2;
		}
		
		public static Boolean operator == (Quaternion quaternion1, Quaternion quaternion2)
		{
			return ((((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) && (quaternion1.W == quaternion2.W));
		}
		
		public static Boolean operator != (Quaternion quaternion1, Quaternion quaternion2)
		{
			if (((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) {
				return !(quaternion1.W == quaternion2.W);
			}
			return true;
		}
		
		public static Quaternion operator + (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X + quaternion2.X;
			quaternion.Y = quaternion1.Y + quaternion2.Y;
			quaternion.Z = quaternion1.Z + quaternion2.Z;
			quaternion.W = quaternion1.W + quaternion2.W;
			return quaternion;
		}
		
		public static Quaternion operator - (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X - quaternion2.X;
			quaternion.Y = quaternion1.Y - quaternion2.Y;
			quaternion.Z = quaternion1.Z - quaternion2.Z;
			quaternion.W = quaternion1.W - quaternion2.W;
			return quaternion;
		}
		
		public static Quaternion operator * (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			Half x = quaternion1.X;
			Half y = quaternion1.Y;
			Half z = quaternion1.Z;
			Half w = quaternion1.W;
			Half num4 = quaternion2.X;
			Half num3 = quaternion2.Y;
			Half num2 = quaternion2.Z;
			Half num = quaternion2.W;
			Half num12 = (y * num2) - (z * num3);
			Half num11 = (z * num4) - (x * num2);
			Half num10 = (x * num3) - (y * num4);
			Half num9 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.X = ((x * num) + (num4 * w)) + num12;
			quaternion.Y = ((y * num) + (num3 * w)) + num11;
			quaternion.Z = ((z * num) + (num2 * w)) + num10;
			quaternion.W = (w * num) - num9;
			return quaternion;
		}
		
		public static Quaternion operator * (Quaternion quaternion1, Half scaleFactor)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X * scaleFactor;
			quaternion.Y = quaternion1.Y * scaleFactor;
			quaternion.Z = quaternion1.Z * scaleFactor;
			quaternion.W = quaternion1.W * scaleFactor;
			return quaternion;
		}
		
		public static Quaternion operator / (Quaternion quaternion1, Quaternion quaternion2)
		{
			Half one = 1;

			Quaternion quaternion;
			Half x = quaternion1.X;
			Half y = quaternion1.Y;
			Half z = quaternion1.Z;
			Half w = quaternion1.W;
			Half num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
			Half num5 = one / num14;
			Half num4 = -quaternion2.X * num5;
			Half num3 = -quaternion2.Y * num5;
			Half num2 = -quaternion2.Z * num5;
			Half num = quaternion2.W * num5;
			Half num13 = (y * num2) - (z * num3);
			Half num12 = (z * num4) - (x * num2);
			Half num11 = (x * num3) - (y * num4);
			Half num10 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.X = ((x * num) + (num4 * w)) + num13;
			quaternion.Y = ((y * num) + (num3 * w)) + num12;
			quaternion.Z = ((z * num) + (num2 * w)) + num11;
			quaternion.W = (w * num) - num10;
			return quaternion;
		}



		
		public static void Negate (ref Quaternion quaternion, out Quaternion result)
		{
			result.X = -quaternion.X;
			result.Y = -quaternion.Y;
			result.Z = -quaternion.Z;
			result.W = -quaternion.W;
		}

		public static void Add (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			result.X = quaternion1.X + quaternion2.X;
			result.Y = quaternion1.Y + quaternion2.Y;
			result.Z = quaternion1.Z + quaternion2.Z;
			result.W = quaternion1.W + quaternion2.W;
		}
		
		public static void Subtract (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			result.X = quaternion1.X - quaternion2.X;
			result.Y = quaternion1.Y - quaternion2.Y;
			result.Z = quaternion1.Z - quaternion2.Z;
			result.W = quaternion1.W - quaternion2.W;
		}

		public static void Multiply (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			Half x = quaternion1.X;
			Half y = quaternion1.Y;
			Half z = quaternion1.Z;
			Half w = quaternion1.W;
			Half num4 = quaternion2.X;
			Half num3 = quaternion2.Y;
			Half num2 = quaternion2.Z;
			Half num = quaternion2.W;
			Half num12 = (y * num2) - (z * num3);
			Half num11 = (z * num4) - (x * num2);
			Half num10 = (x * num3) - (y * num4);
			Half num9 = ((x * num4) + (y * num3)) + (z * num2);
			result.X = ((x * num) + (num4 * w)) + num12;
			result.Y = ((y * num) + (num3 * w)) + num11;
			result.Z = ((z * num) + (num2 * w)) + num10;
			result.W = (w * num) - num9;
		}

		public static void Multiply (ref Quaternion quaternion1, Half scaleFactor, out Quaternion result)
		{
			result.X = quaternion1.X * scaleFactor;
			result.Y = quaternion1.Y * scaleFactor;
			result.Z = quaternion1.Z * scaleFactor;
			result.W = quaternion1.W * scaleFactor;
		}
		
		public static void Divide (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			Half one = 1;

			Half x = quaternion1.X;
			Half y = quaternion1.Y;
			Half z = quaternion1.Z;
			Half w = quaternion1.W;
			Half num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
			Half num5 = one / num14;
			Half num4 = -quaternion2.X * num5;
			Half num3 = -quaternion2.Y * num5;
			Half num2 = -quaternion2.Z * num5;
			Half num = quaternion2.W * num5;
			Half num13 = (y * num2) - (z * num3);
			Half num12 = (z * num4) - (x * num2);
			Half num11 = (x * num3) - (y * num4);
			Half num10 = ((x * num4) + (y * num3)) + (z * num2);
			result.X = ((x * num) + (num4 * w)) + num13;
			result.Y = ((y * num) + (num3 * w)) + num12;
			result.Z = ((z * num) + (num2 * w)) + num11;
			result.W = (w * num) - num10;
		}
		
		#endregion
		#region Utilities

		public static void Slerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Half amount, out Quaternion result)
		{
			Half zero = 0;
			Half one = 1;
			Half nineninenine; RealMaths.FromString("0.999999", out nineninenine);

			Half num2;
			Half num3;
			Half num = amount;
			Half num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
			Boolean flag = false;
			if (num4 < zero) {
				flag = true;
				num4 = -num4;
			}


			if (num4 >nineninenine) {
				num3 = one - num;
				num2 = flag ? -num : num;
			} else {
				Half num5 = RealMaths.ArcCos (num4);
				Half num6 = one / RealMaths.Sin (num5);

				num3 = RealMaths.Sin ((one - num) * num5) * num6;

				num2 = flag ? -RealMaths.Sin (num * num5) * num6 : RealMaths.Sin (num * num5) * num6;
			}
			result.X = (num3 * quaternion1.X) + (num2 * quaternion2.X);
			result.Y = (num3 * quaternion1.Y) + (num2 * quaternion2.Y);
			result.Z = (num3 * quaternion1.Z) + (num2 * quaternion2.Z);
			result.W = (num3 * quaternion1.W) + (num2 * quaternion2.W);
		}

		public static void Lerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Half amount, out Quaternion result)
		{
			Half zero = 0;
			Half one = 1;

			Half num = amount;
			Half num2 = one - num;
			Half num5 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
			if (num5 >= zero) {
				result.X = (num2 * quaternion1.X) + (num * quaternion2.X);
				result.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
				result.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
				result.W = (num2 * quaternion1.W) + (num * quaternion2.W);
			} else {
				result.X = (num2 * quaternion1.X) - (num * quaternion2.X);
				result.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
				result.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
				result.W = (num2 * quaternion1.W) - (num * quaternion2.W);
			}
			Half num4 = (((result.X * result.X) + (result.Y * result.Y)) + (result.Z * result.Z)) + (result.W * result.W);
			Half num3 = one / RealMaths.Sqrt (num4);
			result.X *= num3;
			result.Y *= num3;
			result.Z *= num3;
			result.W *= num3;
		}
		
		#endregion

	}	[StructLayout (LayoutKind.Sequential)]
	public struct Ray 
		: IEquatable<Ray>
	{
		// The starting position of this ray
		public Vector3 Position;
		
		// Normalised vector that defines the direction of this ray
		public Vector3 Direction;

		public Ray (Vector3 position, Vector3 direction)
		{
			this.Position = position;
			this.Direction = direction;
		}

		// Determines whether or not this ray is equal in value to another ray
		public Boolean Equals (Ray other)
		{
			// Check position
			if (this.Position.X != other.Position.X) return false;
			if (this.Position.Y != other.Position.Y) return false;
			if (this.Position.Z != other.Position.Z) return false;

			// Check direction
			if (this.Direction.X != other.Direction.X) return false;
			if (this.Direction.Y != other.Direction.Y) return false;
			if (this.Direction.Z != other.Direction.Z) return false;

			// They match!
			return true;
		}

		// Determines whether or not this ray is equal in value to another System.Object
		public override Boolean Equals (Object obj)
		{
			if (obj == null) return false;

			if (obj is Ray)
			{
				// Ok, it is a Ray, so just use the method above to compare.
				return this.Equals ((Ray) obj);
			}

			return false;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Position.GetHashCode () + this.Direction.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Position:{0} Direction:{1}}}", this.Position, this.Direction);
		}

		// At what distance from it's starting position does this ray
		// intersect the given box.  Returns null if there is no
		// intersection.
		public Half? Intersects (ref BoundingBox box)
		{
			return box.Intersects (ref this);
		}

		// At what distance from it's starting position does this ray
		// intersect the given frustum.  Returns null if there is no
		// intersection.
		public Half? Intersects (ref BoundingFrustum frustum)
		{
			if (frustum == null)
			{
				throw new ArgumentNullException ();
			}

			return frustum.Intersects (ref this);
		}

		// At what distance from it's starting position does this ray
		// intersect the given plane.  Returns null if there is no
		// intersection.
		public Half? Intersects (ref Plane plane)
		{
			Half zero = 0;

			Half nearZero; RealMaths.FromString("0.00001", out nearZero);

			Half num2 = ((plane.Normal.X * this.Direction.X) + (plane.Normal.Y * this.Direction.Y)) + (plane.Normal.Z * this.Direction.Z);
			
			if (RealMaths.Abs (num2) < nearZero)
			{
				return null;
			}
			
			Half num3 = ((plane.Normal.X * this.Position.X) + (plane.Normal.Y * this.Position.Y)) + (plane.Normal.Z * this.Position.Z);

			Half num = (-plane.D - num3) / num2;
			
			if (num < zero)
			{
				if (num < -nearZero)
				{
					return null;
				}

				num = zero;
			}

			return new Half? (num);
		}

		// At what distance from it's starting position does this ray
		// intersect the given sphere.  Returns null if there is no
		// intersection.
		public Half? Intersects (ref BoundingSphere sphere)
		{
			Half zero = 0;

			Half initialXOffset = sphere.Center.X - this.Position.X;

			Half initialYOffset = sphere.Center.Y - this.Position.Y;
			
			Half initialZOffset = sphere.Center.Z - this.Position.Z;
			
			Half num7 = ((initialXOffset * initialXOffset) + (initialYOffset * initialYOffset)) + (initialZOffset * initialZOffset);

			Half num2 = sphere.Radius * sphere.Radius;

			if (num7 <= num2)
			{
				return zero;
			}

			Half num = ((initialXOffset * this.Direction.X) + (initialYOffset * this.Direction.Y)) + (initialZOffset * this.Direction.Z);
			if (num < zero)
			{
				return null;
			}
			
			Half num6 = num7 - (num * num);
			if (num6 > num2)
			{
				return null;
			}
			
			Half num8 = RealMaths.Sqrt ((num2 - num6));

			return new Half? (num - num8);
		}

		public static Boolean operator == (Ray a, Ray b)
		{
			return a.Equals(b);
		}

		public static Boolean operator != (Ray a, Ray b)
		{
			return !a.Equals(b);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Triangle
		: IEquatable<Triangle>
	{
		public Vector3 A;
		public Vector3 B;
		public Vector3 C;

		public Triangle (Vector3 a, Vector3 b, Vector3 c)
		{
			this.A = a;
			this.B = b;
			this.C = c;
		}

		// Determines whether or not this Triangle is equal in value to another Triangle
		public Boolean Equals (Triangle other)
		{
			if (this.A.X != other.A.X) return false;
			if (this.A.Y != other.A.Y) return false;
			if (this.A.Z != other.A.Z) return false;

			if (this.B.X != other.B.X) return false;
			if (this.B.Y != other.B.Y) return false;
			if (this.B.Z != other.B.Z) return false;

			if (this.C.X != other.C.X) return false;
			if (this.C.Y != other.C.Y) return false;
			if (this.C.Z != other.C.Z) return false;

			// They match!
			return true;
		}

		// Determines whether or not this Triangle is equal in value to another System.Object
		public override Boolean Equals (Object obj)
		{
			if (obj == null) return false;

			if (obj is Triangle)
			{
				// Ok, it is a Triangle, so just use the method above to compare.
				return this.Equals ((Triangle) obj);
			}

			return false;
		}

		public override Int32 GetHashCode ()
		{
			return (this.A.GetHashCode () + this.B.GetHashCode () + this.C.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{A:{0} B:{1} C:{2}}}", this.A, this.B, this.C);
		}

		public static Boolean operator == (Triangle a, Triangle b)
		{
			return a.Equals(b);
		}

		public static Boolean operator != (Triangle a, Triangle b)
		{
			return !a.Equals(b);
		}

		public static Boolean IsPointInTriangleangle( ref Vector3 point, ref Triangle triangle )
		{
			Vector3 aToB = triangle.B - triangle.A;
			Vector3 bToC = triangle.C - triangle.B;

			Vector3 n; Vector3.Cross(ref aToB, ref bToC, out n);

			Vector3 aToPoint = point - triangle.A;

			Vector3 wTest; Vector3.Cross(ref aToB, ref aToPoint, out wTest);

			Half zero = 0;

			Half dot; Vector3.Dot(ref wTest, ref n, out dot);

			if ( dot < zero )
			{
				return false;
			}

			Vector3 bToPoint = point - triangle.B;

			Vector3.Cross(ref bToC, ref bToPoint, out wTest);

			Vector3.Dot(ref wTest, ref n, out dot);

			if ( dot < zero )
			{
				return false;
			}

			Vector3 cToA = triangle.A - triangle.C;

			Vector3 cToPoint = point - triangle.C;

			Vector3.Cross(ref cToA, ref cToPoint, out wTest);

			Vector3.Dot(ref wTest, ref n, out dot);

			if ( dot < zero )
			{
				return false;
			}

			return true;
		}

		// Determines whether or not a triangle is degenerate ( all points lay on the same line in space ).
		public Boolean IsDegenerate()
		{
			throw new System.NotImplementedException();
		}

		// Get's the Barycentric coordinates of a point inside a Triangle.
		public static void BarycentricCoordinates( ref Vector3 point, ref Triangle triangle, out Vector3 barycentricCoordinates )
		{
			if( triangle.IsDegenerate() )
			{
				throw new System.ArgumentException("Input Triangle is degenerate, this is not supported.");
			}

			Vector3 aToB = triangle.B - triangle.A;
			Vector3 aToC = triangle.C - triangle.A;
			Vector3 aToPoint = point - triangle.A;

			// compute cross product to get area of parallelograms
			Vector3 cross1; Vector3.Cross(ref aToB, ref aToPoint, out cross1);
			Vector3 cross2; Vector3.Cross(ref aToC, ref aToPoint, out cross2);
			Vector3 cross3; Vector3.Cross(ref aToB, ref aToC, out cross3);
	
			// compute barycentric coordinates as ratios of areas

			Half one = 1;

			Half denom = one / cross3.Length();
			barycentricCoordinates.X = cross2.Length() * denom;
			barycentricCoordinates.Y = cross1.Length() * denom;
			barycentricCoordinates.Z = one - barycentricCoordinates.X - barycentricCoordinates.Y;
		}
		/*

		// Triangleangle Intersect
		// ------------------
		// Returns true if triangles P0P1P2 and Q0Q1Q2 intersect
		// Assumes triangle is not degenerate
		//
		// This is not the algorithm presented in the text.  Instead, it is based on a 
		// recent article by Guigue and Devillers in the July 2003 issue Journal of 
		// Graphics Tools.  As it is faster than the ERIT algorithm, under ordinary 
		// circumstances it would have been discussed in the text, but it arrived too late.  
		//
		// More information and the original source code can be found at
		// http://www.acm.org/jgt/papers/GuigueDevillers03/
		//
		// A nearly identical algorithm was in the same issue of JGT, by Shen Heng and 
		// Tang.  See http://www.acm.org/jgt/papers/ShenHengTang03/ for source code.
		//
		// Yes, this is complicated.  Did you think testing triangles would be easy?
		//
		static Boolean TriangleangleIntersect( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2 )
		{
			// test P against Q's plane
			Vector3 normalQ = Vector3.Cross( Q1 - Q0, Q2 - Q0 );

			Single testP0 = Vector3.Dot( normalQ, P0 - Q0 );
			Single testP1 = Vector3.Dot( normalQ, P1 - Q0 );
			Single testP2 = Vector3.Dot( normalQ, P2 - Q0 );
  
			// P doesn't intersect Q's plane
			if ( testP0 * testP1 > AbacusHelper.Epsilon && testP0*testP2 > AbacusHelper.Epsilon )
				return false;

			// test Q against P's plane
			Vector3 normalP = Vector3.Cross( P1 - P0, P2 - P0 );

			Single testQ0 = Vector3.Dot( normalP, Q0 - P0 );
			Single testQ1 = Vector3.Dot( normalP, Q1 - P0 );
			Single testQ2 = Vector3.Dot( normalP, Q2 - P0 );
  
			// Q doesn't intersect P's plane
			if (testQ0*testQ1 > AbacusHelper.Epsilon && testQ0*testQ2 > AbacusHelper.Epsilon )
				return false;
	
			// now we rearrange P's vertices such that the lone vertex (the one that lies
			// in its own half-space of Q) is first.  We also permute the other
			// triangle's vertices so that P0 will "see" them in counterclockwise order

			// Once reordered, we pass the vertices down to a helper function which will
			// reorder Q's vertices, and then test

			// P0 in Q's positive half-space
			if (testP0 > AbacusHelper.Epsilon) 
			{
				// P1 in Q's positive half-space (so P2 is lone vertex)
				if (testP1 > AbacusHelper.Epsilon) 
					return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
				// P2 in Q's positive half-space (so P1 is lone vertex)
				else if (testP2 > AbacusHelper.Epsilon) 
					return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);	
				// P0 is lone vertex
				else 
					return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
			} 
			// P0 in Q's negative half-space
			else if (testP0 < -AbacusHelper.Epsilon) 
			{
				// P1 in Q's negative half-space (so P2 is lone vertex)
				if (testP1 < -AbacusHelper.Epsilon) 
					return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
				// P2 in Q's negative half-space (so P1 is lone vertex)
				else if (testP2 < -AbacusHelper.Epsilon) 
					return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
				// P0 is lone vertex
				else 
					return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
			} 
			// P0 on Q's plane
			else 
			{
				// P1 in Q's negative half-space 
				if (testP1 < -AbacusHelper.Epsilon) 
				{
					// P2 in Q's negative half-space (P0 is lone vertex)
					if (testP2 < -AbacusHelper.Epsilon) 
						return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
					// P2 in positive half-space or on plane (P1 is lone vertex)
					else 
						return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
				}
				// P1 in Q's positive half-space 
				else if (testP1 > AbacusHelper.Epsilon) 
				{
					// P2 in Q's positive half-space (P0 is lone vertex)
					if (testP2 > AbacusHelper.Epsilon) 
						return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
					// P2 in negative half-space or on plane (P1 is lone vertex)
					else 
						return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
				}
				// P1 lies on Q's plane too
				else  
				{
					// P2 in Q's positive half-space (P2 is lone vertex)
					if (testP2 > AbacusHelper.Epsilon) 
						return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
					// P2 in Q's negative half-space (P2 is lone vertex)
					// note different ordering for Q vertices
					else if (testP2 < -AbacusHelper.Epsilon) 
						return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
					// all three points lie on Q's plane, default to 2D test
					else 
						return CoplanarTriangleangleIntersect(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, ref normalP);
				}
			}
		}



		// Adjust Q
		// --------
		// Helper for TriangleangleIntersect()
		//
		// Now we rearrange Q's vertices such that the lone vertex (the one that lies
		// in its own half-space of P) is first.  We also permute the other
		// triangle's vertices so that Q0 will "see" them in counterclockwise order
		//
		// Once reordered, we pass the vertices down to a helper function which will
		// actually test for intersection on the common line between the two planes
		//
		static Boolean AdjustQ( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2,
			Single testQ0, Single testQ1, Single testQ2,
			ref Vector3 normalP )
		{

			// Q0 in P's positive half-space
			if (testQ0 > AbacusHelper.Epsilon) 
			{ 
				// Q1 in P's positive half-space (so Q2 is lone vertex)
				if (testQ1 > AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P2, ref P1, ref Q2, ref Q0, ref Q1);
				// Q2 in P's positive half-space (so Q1 is lone vertex)
				else if (testQ2 > AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P2, ref P1, ref Q1, ref Q2, ref Q0);
				// Q0 is lone vertex
				else 
					return TestLineOverlap(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2);
			}
			// Q0 in P's negative half-space
			else if (testQ0 < -AbacusHelper.Epsilon) 
			{ 
				// Q1 in P's negative half-space (so Q2 is lone vertex)
				if (testQ1 < -AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P1, ref P2, ref Q2, ref Q0, ref Q1);
				// Q2 in P's negative half-space (so Q1 is lone vertex)
				else if (testQ2 < -AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P1, ref P2, ref Q1, ref Q2, ref Q0);
				// Q0 is lone vertex
				else 
					return TestLineOverlap(ref P0, ref P2, ref P1, ref Q0, ref Q1, ref Q2);
			}
			// Q0 on P's plane
			else 
			{ 
				// Q1 in P's negative half-space 
				if (testQ1 < -AbacusHelper.Epsilon) 
				{ 
					// Q2 in P's negative half-space (Q0 is lone vertex)
					if (testQ2 < -AbacusHelper.Epsilon)  
						return TestLineOverlap(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2);
					// Q2 in positive half-space or on plane (P1 is lone vertex)
					else 
						return TestLineOverlap(ref P0, ref P2, ref P1, ref Q1, ref Q2, ref Q0);
				}
				// Q1 in P's positive half-space 
				else if (testQ1 > AbacusHelper.Epsilon) 
				{ 
					// Q2 in P's positive half-space (Q0 is lone vertex)
					if (testQ2 > AbacusHelper.Epsilon) 
						return TestLineOverlap(ref P0, ref P2, ref P1, ref Q0, ref Q1, ref Q2);
					// Q2 in negative half-space or on plane (P1 is lone vertex)
					else  
						return TestLineOverlap(ref P0, ref P1, ref P2, ref Q1, ref Q2, ref Q0);
				}
				// Q1 lies on P's plane too
				else 
				{
					// Q2 in P's positive half-space (Q2 is lone vertex)
					if (testQ2 > AbacusHelper.Epsilon) 
						return TestLineOverlap(ref P0, ref P1, ref P2, ref Q2, ref Q0, ref Q1);
					// Q2 in P's negative half-space (Q2 is lone vertex)
					// note different ordering for Q vertices
					else if (testQ2 < -AbacusHelper.Epsilon) 
						return TestLineOverlap(ref P0, ref P2, ref P1, ref Q2, ref Q0, ref Q1);
					// all three points lie on P's plane, default to 2D test
					else 
						return CoplanarTriangleangleIntersect(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, ref normalP);
				}
			}
		}



		// Test Line Overlap
		// -----------------
		// Helper for TriangleangleIntersect()
		//
		// This tests whether the rearranged triangles overlap, by checking the intervals
		// where their edges cross the common line between the two planes.  If the 
		// interval for P is [i,j] and Q is [k,l], then there is intersection if the
		// intervals overlap.  Previous algorithms computed these intervals directly, 
		// this tests implictly by using two "plane tests."
		//
		static Boolean TestLineOverlap( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2 )
		{
			// get "plane normal"
			Vector3 normal = Vector3.Cross( P1 - P0, Q0 - P0);

			// fails test, no intersection
			if ( Vector3.Dot(normal, Q1 - P0 ) > AbacusHelper.Epsilon )
				return false;
  
			// get "plane normal"
			normal = Vector3.Cross( P2 - P0, Q2 - P0 );

			// fails test, no intersection
			if ( Vector3.Dot( normal, Q0 - P0 ) > AbacusHelper.Epsilon )
				return false;

			// intersection!
			return true;
		}



		// Coplanar Triangleangle Intersect
		// ---------------------------
		// Helper for TriangleangleIntersect()
		//
		// This projects the two triangles down to 2D, maintaining the largest area by
		// dropping the dimension where the normal points the farthest.
		//
		static Boolean CoplanarTriangleangleIntersect( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2, 
			ref Vector3 planeNormal )
		{
			Vector3 absNormal = new Vector3( 
				System.Math.Abs(planeNormal.X), 
				System.Math.Abs(planeNormal.Y), 
				System.Math.Abs(planeNormal.Z) );

			Vector2 projP0, projP1, projP2;
			Vector2 projQ0, projQ1, projQ2;

			// if x is direction of largest magnitude
			if ( absNormal.X > absNormal.Y && absNormal.X >= absNormal.Z )
			{
				projP0 = new Vector2( P0.Y, P0.Z );
				projP1 = new Vector2( P1.Y, P1.Z );
				projP2 = new Vector2( P2.Y, P2.Z );
				projQ0 = new Vector2( Q0.Y, Q0.Z );
				projQ1 = new Vector2( Q1.Y, Q1.Z );
				projQ2 = new Vector2( Q2.Y, Q2.Z );
			}
			// if y is direction of largest magnitude
			else if ( absNormal.Y > absNormal.X && absNormal.Y >= absNormal.Z )
			{
				projP0 = new Vector2( P0.X, P0.Z );
				projP1 = new Vector2( P1.X, P1.Z );
				projP2 = new Vector2( P2.X, P2.Z );
				projQ0 = new Vector2( Q0.X, Q0.Z );
				projQ1 = new Vector2( Q1.X, Q1.Z );
				projQ2 = new Vector2( Q2.X, Q2.Z );
			}
			// z is the direction of largest magnitude
			else
			{
				projP0 = new Vector2( P0.X, P0.Y );
				projP1 = new Vector2( P1.X, P1.Y );
				projP2 = new Vector2( P2.X, P2.Y );
				projQ0 = new Vector2( Q0.X, Q0.Y );
				projQ1 = new Vector2( Q1.X, Q1.Y );
				projQ2 = new Vector2( Q2.X, Q2.Y );
			}

			return TriangleangleIntersect( ref projP0, ref projP1, ref projP2, ref projQ0, ref projQ1, ref projQ2 );
		}



		// Triangleangle Intersect
		// ------------------
		// Returns true if ray intersects triangle.
		//
		static Boolean TriangleangleIntersect( 
			ref Single t, //perhaps this should be out 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Ray ray )
		{
			// test ray direction against triangle
			Vector3 e1 = P1 - P0;
			Vector3 e2 = P2 - P0;
			Vector3 p = Vector3.Cross( ray.Direction, e2 );
			Single a = Vector3.Dot( e1, p );

			// if result zero, no intersection or infinite intersections
			// (ray parallel to triangle plane)
			if ( AbacusHelper.IsZero(a) )
				return false;

			// compute denominator
			Single f = 1.0f/a;

			// compute barycentric coordinates
			Vector3 s = ray.Position - P0;
			Single u = f * Vector3.Dot( s, p );

			// ray falls outside triangle
			if (u < 0.0f || u > 1.0f) 
				return false;

			Vector3 q = Vector3.Cross( s, e1 );
			Single v = f * Vector3.Dot( ray.Direction, q );

			// ray falls outside triangle
			if (v < 0.0f || u+v > 1.0f) 
				return false;

			// compute line parameter
			t = f * Vector3.Dot( e2, q );

			return (t >= 0.0f);
		}

		
		//
		// @ TriangleangleClassify()
		// Returns signed distance between plane and triangle
		//
		static Single TriangleangleClassify( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Plane plane )
		{
			Single test0 = plane.Test( P0 );
			Single test1 = plane.Test( P1 );

			// if two points lie on opposite sides of plane, intersect
			if (test0*test1 < 0.0f)
				return 0.0f;

			Single test2 = plane.Test( P2 );

			// if two points lie on opposite sides of plane, intersect
			if (test0*test2 < 0.0f)
				return 0.0f;
			if (test1*test2 < 0.0f)
				return 0.0f;

			// no intersection, return signed distance
			if ( test0 < 0.0f )
			{
				if ( test0 < test1 )
				{
					if ( test1 < test2 )
						return test2;
					else
						return test1;
				}
				else if (test0 < test2)
				{
					return test2;
				}
				else
				{   
					return test0;
				}
			}
			else
			{
				if ( test0 > test1 )
				{
					if ( test1 > test2 )
						return test2;
					else
						return test1;
				}
				else if (test0 > test2)
				{
					return test2;
				}
				else
				{   
					return test0;
				}
			}
		}

		*/
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Vector2
		: IEquatable<Vector2>
	{
		public Half X;
		public Half Y;
		
		public Vector2 (Int32 x, Int32 y)
		{
			this.X = (Half) x;
			this.Y = (Half) y;
		}

		public Vector2 (Half x, Half y)
		{
			this.X = x;
			this.Y = y;
		}

		public void Set (Half x, Half y)
		{
			this.X = x;
			this.Y = y;
		}

		public Vector2 (Half value)
		{
			this.X = this.Y = value;
		}

		public Half Length ()
		{
			Half num = (this.X * this.X) + (this.Y * this.Y);
			return RealMaths.Sqrt (num);
		}

		public Half LengthSquared ()
		{
			return ((this.X * this.X) + (this.Y * this.Y));
		}

		public void Normalise ()
		{
			Half num2 = (this.X * this.X) + (this.Y * this.Y);

			Half one = 1;
			Half num = one / (RealMaths.Sqrt (num2));
			this.X *= num;
			this.Y *= num;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1}}}", new Object[] { this.X.ToString (), this.Y.ToString () });
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Vector2) {
				flag = this.Equals ((Vector2)obj);
			}
			return flag;
		}
		
		public override Int32 GetHashCode ()
		{
			return (this.X.GetHashCode () + this.Y.GetHashCode ());
		}

		public Boolean IsUnit()
		{
			Half one = 1;

			return RealMaths.IsZero(one - X*X - Y*Y);
		}

		#region IEquatable<Vector2>
		public Boolean Equals (Vector2 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y));
		}
		#endregion

		#region Constants

		static Vector2 zero;
		static Vector2 one;
		static Vector2 unitX;
		static Vector2 unitY;

		static Vector2 ()
		{
			Half temp_one; RealMaths.One(out temp_one);
			Half temp_zero; RealMaths.Zero(out temp_zero);

			zero = new Vector2 ();
			one = new Vector2 (temp_one, temp_one);
			unitX = new Vector2 (temp_one, temp_zero);
			unitY = new Vector2 (temp_zero, temp_one);
		}

		public static Vector2 Zero
		{
			get { return zero; }
		}
		
		public static Vector2 One
		{
			get { return one; }
		}
		
		public static Vector2 UnitX
		{
			get { return unitX; }
		}
		
		public static Vector2 UnitY
		{
			get { return unitY; }
		}
		
		#endregion
		#region Maths

		public static void Distance (ref Vector2 value1, ref Vector2 value2, out Half result)
		{
			Half num2 = value1.X - value2.X;
			Half num = value1.Y - value2.Y;
			Half num3 = (num2 * num2) + (num * num);
			result = RealMaths.Sqrt (num3);
		}

		public static void DistanceSquared (ref Vector2 value1, ref Vector2 value2, out Half result)
		{
			Half num2 = value1.X - value2.X;
			Half num = value1.Y - value2.Y;
			result = (num2 * num2) + (num * num);
		}

		public static void Dot (ref Vector2 value1, ref Vector2 value2, out Half result)
		{
			result = (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		public static void PerpDot (ref Vector2 value1, ref Vector2 value2, out Half result)
		{
			result = (value1.X * value2.Y - value1.Y * value2.X);
		}

		public static void Perpendicular (ref Vector2 value, out Vector2 result)
		{
			result = new Vector2 (-value.X, value.Y);
		}

		public static void Normalise (ref Vector2 value, out Vector2 result)
		{
			Half one = 1;

			Half num2 = (value.X * value.X) + (value.Y * value.Y);
			Half num = one / (RealMaths.Sqrt (num2));
			result.X = value.X * num;
			result.Y = value.Y * num;
		}

		public static void Reflect (ref Vector2 vector, ref Vector2 normal, out Vector2 result)
		{
			Half two = 2;

			Half num = (vector.X * normal.X) + (vector.Y * normal.Y);
			result.X = vector.X - ((two * num) * normal.X);
			result.Y = vector.Y - ((two * num) * normal.Y);
		}
		
		public static void Transform (ref Vector2 position, ref Matrix44 matrix, out Vector2 result)
		{
			Half num2 = ((position.X * matrix.M11) + (position.Y * matrix.M21)) + matrix.M41;
			Half num = ((position.X * matrix.M12) + (position.Y * matrix.M22)) + matrix.M42;
			result.X = num2;
			result.Y = num;
		}
		
		public static void TransformNormal (ref Vector2 normal, ref Matrix44 matrix, out Vector2 result)
		{
			Half num2 = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
			Half num = (normal.X * matrix.M12) + (normal.Y * matrix.M22);
			result.X = num2;
			result.Y = num;
		}
		
		public static void Transform (ref Vector2 value, ref Quaternion rotation, out Vector2 result)
		{
			Half one = 1;

			Half num10 = rotation.X + rotation.X;
			Half num5 = rotation.Y + rotation.Y;
			Half num4 = rotation.Z + rotation.Z;
			Half num3 = rotation.W * num4;
			Half num9 = rotation.X * num10;
			Half num2 = rotation.X * num5;
			Half num8 = rotation.Y * num5;
			Half num = rotation.Z * num4;
			Half num7 = (value.X * ((one - num8) - num)) + (value.Y * (num2 - num3));
			Half num6 = (value.X * (num2 + num3)) + (value.Y * ((one - num9) - num));
			result.X = num7;
			result.Y = num6;
		}
		
		#endregion
		#region Operators

		public static Vector2 operator - (Vector2 value)
		{
			Vector2 vector;
			vector.X = -value.X;
			vector.Y = -value.Y;
			return vector;
		}
		
		public static Boolean operator == (Vector2 value1, Vector2 value2)
		{
			return ((value1.X == value2.X) && (value1.Y == value2.Y));
		}
		
		public static Boolean operator != (Vector2 value1, Vector2 value2)
		{
			if (value1.X == value2.X) {
				return !(value1.Y == value2.Y);
			}
			return true;
		}

		public static Vector2 operator + (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			return vector;
		}

		public static Vector2 operator - (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			return vector;
		}

		public static Vector2 operator * (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X * value2.X;
			vector.Y = value1.Y * value2.Y;
			return vector;
		}
		
		public static Vector2 operator * (Vector2 value, Half scaleFactor)
		{
			Vector2 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}
		
		public static Vector2 operator * (Half scaleFactor, Vector2 value)
		{
			Vector2 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}

		public static Vector2 operator / (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			return vector;
		}
		
		public static Vector2 operator / (Vector2 value1, Half divider)
		{
			Vector2 vector;
			Half one = 1;
			Half num = one / divider;
			vector.X = value1.X * num;
			vector.Y = value1.Y * num;
			return vector;
		}
		
		public static void Negate (ref Vector2 value, out Vector2 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		public static void Add (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		public static void Subtract (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		public static void Multiply (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}
		
		public static void Multiply (ref Vector2 value1, Half scaleFactor, out Vector2 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		public static void Divide (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		public static void Divide (ref Vector2 value1, Half divider, out Vector2 result)
		{
			Half one = 1;
			Half num = one / divider;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
		}
		
		#endregion
		#region Splines

		public static void Barycentric (ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, Half amount1, Half amount2, out Vector2 result)
		{
			result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
			result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
		}

		public static void SmoothStep (ref Vector2 value1, ref Vector2 value2, Half amount, out Vector2 result)
		{
			Half zero = 0;
			Half one = 1;
			Half two = 2;
			Half three = 3;

			amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
			amount = (amount * amount) * (three - (two * amount));
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
		}
		
		public static void CatmullRom (ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, Half amount, out Vector2 result)
		{
			Half half; RealMaths.Half(out half);
			Half two = 2;
			Half three = 3;
			Half four = 4;
			Half five = 5;

			Half num = amount * amount;
			Half num2 = amount * num;
			result.X = half * ((((two * value2.X) + ((-value1.X + value3.X) * amount)) + (((((two * value1.X) - (five * value2.X)) + (four * value3.X)) - value4.X) * num)) + ((((-value1.X + (three * value2.X)) - (three * value3.X)) + value4.X) * num2));
			result.Y = half * ((((two * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((two * value1.Y) - (five * value2.Y)) + (four * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (three * value2.Y)) - (three * value3.Y)) + value4.Y) * num2));
		}

		public static void Hermite (ref Vector2 value1, ref Vector2 tangent1, ref Vector2 value2, ref Vector2 tangent2, Half amount, out Vector2 result)
		{
			Half one = 1;
			Half two = 2;
			Half three = 3;

			Half num = amount * amount;
			Half num2 = amount * num;
			Half num6 = ((two * num2) - (three * num)) + one;
			Half num5 = (-two * num2) + (three * num);
			Half num4 = (num2 - (two * num)) + amount;
			Half num3 = num2 - num;
			result.X = (((value1.X * num6) + (value2.X * num5)) + (tangent1.X * num4)) + (tangent2.X * num3);
			result.Y = (((value1.Y * num6) + (value2.Y * num5)) + (tangent1.Y * num4)) + (tangent2.Y * num3);
		}
		
		#endregion
		#region Utilities

		public static void Min (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
		}

		public static void Max (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
		}

		public static void Clamp (ref Vector2 value1, ref Vector2 min, ref Vector2 max, out Vector2 result)
		{
			Half x = value1.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;
			Half y = value1.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;
			result.X = x;
			result.Y = y;
		}
		
		public static void Lerp (ref Vector2 value1, ref Vector2 value2, Half amount, out Vector2 result)
		{
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
		}
		
		#endregion

	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Vector3 
		: IEquatable<Vector3>
	{
		public Half X;
		public Half Y;
		public Half Z;

		public Vector2 XY
		{
			get
			{
				return new Vector2(X, Y);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}



		public Vector3 (Half x, Half y, Half z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public Vector3 (Half value)
		{
			this.X = this.Y = this.Z = value;
		}
		
		public Vector3 (Vector2 value, Half z)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1} Z:{2}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString () });
		}

		public Boolean Equals (Vector3 other)
		{
			return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Vector3) {
				flag = this.Equals ((Vector3)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return ((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ());
		}

		public Half Length ()
		{
			Half num = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
			return RealMaths.Sqrt (num);
		}

		public Half LengthSquared ()
		{
			return (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
		}


		public void Normalise ()
		{
			Half one = 1;
			Half num2 = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
			Half num = one / RealMaths.Sqrt (num2);
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
		}

		public Boolean IsUnit()
		{
			Half one = 1;

			return RealMaths.IsZero(one - X*X - Y*Y - Z*Z);
		}

		#region Constants

		static Vector3 _zero;
		static Vector3 _one;
		static Vector3 _half;
		static Vector3 _unitX;
		static Vector3 _unitY;
		static Vector3 _unitZ;
		static Vector3 _up;
		static Vector3 _down;
		static Vector3 _right;
		static Vector3 _left;
		static Vector3 _forward;
		static Vector3 _backward;

		static Vector3 ()
		{
			Half temp_one; RealMaths.One(out temp_one);
			Half temp_half; RealMaths.Half(out temp_half);
			Half temp_zero; RealMaths.Zero(out temp_zero);

			_zero = new Vector3 ();
			_one = new Vector3 (temp_one, temp_one, temp_one);
			_half = new Vector3(temp_half, temp_half, temp_half);
			_unitX = new Vector3 (temp_one, temp_zero, temp_zero);
			_unitY = new Vector3 (temp_zero, temp_one, temp_zero);
			_unitZ = new Vector3 (temp_zero, temp_zero, temp_one);
			_up = new Vector3 (temp_zero, temp_one, temp_zero);
			_down = new Vector3 (temp_zero, -temp_one, temp_zero);
			_right = new Vector3 (temp_one, temp_zero, temp_zero);
			_left = new Vector3 (-temp_one, temp_zero, temp_zero);
			_forward = new Vector3 (temp_zero, temp_zero, -temp_one);
			_backward = new Vector3 (temp_zero, temp_zero, temp_one);
		}
		
		public static Vector3 Zero {
			get {
				return _zero;
			}
		}
		
		public static Vector3 One {
			get {
				return _one;
			}
		}
		
		public static Vector3 Half {
			get {
				return _half;
			}
		}
		
		public static Vector3 UnitX {
			get {
				return _unitX;
			}
		}
		
		public static Vector3 UnitY {
			get {
				return _unitY;
			}
		}
		
		public static Vector3 UnitZ {
			get {
				return _unitZ;
			}
		}
		
		public static Vector3 Up {
			get {
				return _up;
			}
		}
		
		public static Vector3 Down {
			get {
				return _down;
			}
		}
		
		public static Vector3 Right {
			get {
				return _right;
			}
		}
		
		public static Vector3 Left {
			get {
				return _left;
			}
		}
		
		public static Vector3 Forward {
			get {
				return _forward;
			}
		}
		
		public static Vector3 Backward {
			get {
				return _backward;
			}
		}
		
		#endregion
		#region Maths

		public static void Distance (ref Vector3 value1, ref Vector3 value2, out Half result)
		{
			Half num3 = value1.X - value2.X;
			Half num2 = value1.Y - value2.Y;
			Half num = value1.Z - value2.Z;
			Half num4 = ((num3 * num3) + (num2 * num2)) + (num * num);
			result = RealMaths.Sqrt (num4);
		}
		
		public static void DistanceSquared (ref Vector3 value1, ref Vector3 value2, out Half result)
		{
			Half num3 = value1.X - value2.X;
			Half num2 = value1.Y - value2.Y;
			Half num = value1.Z - value2.Z;
			result = ((num3 * num3) + (num2 * num2)) + (num * num);
		}

		public static void Dot (ref Vector3 vector1, ref Vector3 vector2, out Half result)
		{
			result = ((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z);
		}

		public static void Normalise (ref Vector3 value, out Vector3 result)
		{
			Half one = 1;

			Half num2 = ((value.X * value.X) + (value.Y * value.Y)) + (value.Z * value.Z);
			Half num = one / RealMaths.Sqrt (num2);
			result.X = value.X * num;
			result.Y = value.Y * num;
			result.Z = value.Z * num;
		}

		public static void Cross (ref Vector3 vector1, ref Vector3 vector2, out Vector3 result)
		{
			Half num3 = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y);
			Half num2 = (vector1.Z * vector2.X) - (vector1.X * vector2.Z);
			Half num = (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
			result.X = num3;
			result.Y = num2;
			result.Z = num;
		}

		public static void Reflect (ref Vector3 vector, ref Vector3 normal, out Vector3 result)
		{
			Half two = 2;

			Half num = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
			result.X = vector.X - ((two * num) * normal.X);
			result.Y = vector.Y - ((two * num) * normal.Y);
			result.Z = vector.Z - ((two * num) * normal.Z);
		}

		public static void Transform (ref Vector3 position, ref Matrix44 matrix, out Vector3 result)
		{
			Half num3 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
			Half num2 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
			Half num = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
			result.X = num3;
			result.Y = num2;
			result.Z = num;
		}
		
		public static void TransformNormal (ref Vector3 normal, ref Matrix44 matrix, out Vector3 result)
		{
			Half num3 = ((normal.X * matrix.M11) + (normal.Y * matrix.M21)) + (normal.Z * matrix.M31);
			Half num2 = ((normal.X * matrix.M12) + (normal.Y * matrix.M22)) + (normal.Z * matrix.M32);
			Half num = ((normal.X * matrix.M13) + (normal.Y * matrix.M23)) + (normal.Z * matrix.M33);
			result.X = num3;
			result.Y = num2;
			result.Z = num;
		}
		
		public static void Transform (ref Vector3 value, ref Quaternion rotation, out Vector3 result)
		{
			Half one = 1;
			Half num12 = rotation.X + rotation.X;
			Half num2 = rotation.Y + rotation.Y;
			Half num = rotation.Z + rotation.Z;
			Half num11 = rotation.W * num12;
			Half num10 = rotation.W * num2;
			Half num9 = rotation.W * num;
			Half num8 = rotation.X * num12;
			Half num7 = rotation.X * num2;
			Half num6 = rotation.X * num;
			Half num5 = rotation.Y * num2;
			Half num4 = rotation.Y * num;
			Half num3 = rotation.Z * num;
			Half num15 = ((value.X * ((one - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
			Half num14 = ((value.X * (num7 + num9)) + (value.Y * ((one - num8) - num3))) + (value.Z * (num4 - num11));
			Half num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((one - num8) - num5));
			result.X = num15;
			result.Y = num14;
			result.Z = num13;
		}
		
		#endregion
		#region Operators

		public static Vector3 operator - (Vector3 value)
		{
			Vector3 vector;
			vector.X = -value.X;
			vector.Y = -value.Y;
			vector.Z = -value.Z;
			return vector;
		}
		
		public static Boolean operator == (Vector3 value1, Vector3 value2)
		{
			return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z));
		}
		
		public static Boolean operator != (Vector3 value1, Vector3 value2)
		{
			if ((value1.X == value2.X) && (value1.Y == value2.Y)) {
				return !(value1.Z == value2.Z);
			}
			return true;
		}
		
		public static Vector3 operator + (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			vector.Z = value1.Z + value2.Z;
			return vector;
		}
		
		public static Vector3 operator - (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			vector.Z = value1.Z - value2.Z;
			return vector;
		}
		
		public static Vector3 operator * (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X * value2.X;
			vector.Y = value1.Y * value2.Y;
			vector.Z = value1.Z * value2.Z;
			return vector;
		}
		
		public static Vector3 operator * (Vector3 value, Half scaleFactor)
		{
			Vector3 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			return vector;
		}
		
		public static Vector3 operator * (Half scaleFactor, Vector3 value)
		{
			Vector3 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			return vector;
		}
		
		public static Vector3 operator / (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			vector.Z = value1.Z / value2.Z;
			return vector;
		}
		
		public static Vector3 operator / (Vector3 value, Half divider)
		{
			Vector3 vector;
			Half one = 1;

			Half num = one / divider;
			vector.X = value.X * num;
			vector.Y = value.Y * num;
			vector.Z = value.Z * num;
			return vector;
		}

		public static void Negate (ref Vector3 value, out Vector3 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
		}

		public static void Add (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
		}

		public static void Subtract (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
		}

		public static void Multiply (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
		}

		public static void Multiply (ref Vector3 value1, Half scaleFactor, out Vector3 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
		}

		public static void Divide (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
		}

		public static void Divide (ref Vector3 value1, Half value2, out Vector3 result)
		{
			Half one = 1;
			Half num = one / value2;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
		}
		
		#endregion
		#region Splines

		public static void Barycentric (ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, Half amount1, Half amount2, out Vector3 result)
		{
			result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
			result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
			result.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
		}
	
		public static void SmoothStep (ref Vector3 value1, ref Vector3 value2, Half amount, out Vector3 result)
		{
			Half zero = 0;
			Half one = 1;
			Half two = 2;
			Half three = 3;

			amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
			amount = (amount * amount) * (three - (two * amount));
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
		}

		public static void CatmullRom (ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4, Half amount, out Vector3 result)
		{
			Half half; RealMaths.Half(out half);
			Half two = 2;
			Half three = 3;
			Half four = 4;
			Half five = 5;

			Half num = amount * amount;
			Half num2 = amount * num;
			result.X = half * ((((two * value2.X) + ((-value1.X + value3.X) * amount)) + (((((two * value1.X) - (five * value2.X)) + (four * value3.X)) - value4.X) * num)) + ((((-value1.X + (three * value2.X)) - (three * value3.X)) + value4.X) * num2));
			result.Y = half * ((((two * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((two * value1.Y) - (five * value2.Y)) + (four * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (three * value2.Y)) - (three * value3.Y)) + value4.Y) * num2));
			result.Z = half * ((((two * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((two * value1.Z) - (five * value2.Z)) + (four * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (three * value2.Z)) - (three * value3.Z)) + value4.Z) * num2));
		}

		public static void Hermite (ref Vector3 value1, ref Vector3 tangent1, ref Vector3 value2, ref Vector3 tangent2, Half amount, out Vector3 result)
		{
			Half one = 1;
			Half two = 2;
			Half three = 3;

			Half num = amount * amount;
			Half num2 = amount * num;
			Half num6 = ((two * num2) - (three * num)) + one;
			Half num5 = (-two * num2) + (three * num);
			Half num4 = (num2 - (two * num)) + amount;
			Half num3 = num2 - num;
			result.X = (((value1.X * num6) + (value2.X * num5)) + (tangent1.X * num4)) + (tangent2.X * num3);
			result.Y = (((value1.Y * num6) + (value2.Y * num5)) + (tangent1.Y * num4)) + (tangent2.Y * num3);
			result.Z = (((value1.Z * num6) + (value2.Z * num5)) + (tangent1.Z * num4)) + (tangent2.Z * num3);
		}
		
		#endregion
		#region Utilities

		public static void Min (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z < value2.Z) ? value1.Z : value2.Z;
		}

		public static void Max (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z > value2.Z) ? value1.Z : value2.Z;
		}
		
		public static void Clamp (ref Vector3 value1, ref Vector3 min, ref Vector3 max, out Vector3 result)
		{
			Half x = value1.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;
			Half y = value1.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;
			Half z = value1.Z;
			z = (z > max.Z) ? max.Z : z;
			z = (z < min.Z) ? min.Z : z;
			result.X = x;
			result.Y = y;
			result.Z = z;
		}

		public static void Lerp (ref Vector3 value1, ref Vector3 value2, Half amount, out Vector3 result)
		{
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
		}
		
		#endregion

	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Vector4 
		: IEquatable<Vector4>
	{
		public Half X;
		public Half Y;
		public Half Z;
		public Half W;

		public Vector3 XYZ
		{
			get
			{
				return new Vector3(X, Y, Z);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
				this.Z = value.Z;
			}
		}



		public Vector4 (Half x, Half y, Half z, Half w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		public Vector4 (Vector2 value, Half z, Half w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
			this.W = w;
		}

		public Vector4 (Vector3 value, Half w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}

		public Vector4 (Half value)
		{
			this.X = this.Y = this.Z = this.W = value;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString (), this.W.ToString () });
		}

		public Boolean Equals (Vector4 other)
		{
			return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Vector4) {
				flag = this.Equals ((Vector4)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ()) + this.W.GetHashCode ());
		}

		public Half Length ()
		{
			Half num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			return RealMaths.Sqrt (num);
		}

		public Half LengthSquared ()
		{
			return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
		}



		public void Normalise ()
		{
			Half one = 1;
			Half num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			Half num = one / RealMaths.Sqrt (num2);
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
			this.W *= num;
		}



		public Boolean IsUnit()
		{
			Half one = 1;

			return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
		}

		#region Constants

		static Vector4 _zero;
		static Vector4 _one;
		static Vector4 _unitX;
		static Vector4 _unitY;
		static Vector4 _unitZ;
		static Vector4 _unitW;

		static Vector4 ()
		{
			Half temp_one; RealMaths.One(out temp_one);
			Half temp_zero; RealMaths.Zero(out temp_zero);

			_zero = new Vector4 ();
			_one = new Vector4 (temp_one, temp_one, temp_one, temp_one);
			_unitX = new Vector4 (temp_one, temp_zero, temp_zero, temp_zero);
			_unitY = new Vector4 (temp_zero, temp_one, temp_zero, temp_zero);
			_unitZ = new Vector4 (temp_zero, temp_zero, temp_one, temp_zero);
			_unitW = new Vector4 (temp_zero, temp_zero, temp_zero, temp_one);
		}

		public static Vector4 Zero {
			get {
				return _zero;
			}
		}
		
		public static Vector4 One {
			get {
				return _one;
			}
		}
		
		public static Vector4 UnitX {
			get {
				return _unitX;
			}
		}
		
		public static Vector4 UnitY {
			get {
				return _unitY;
			}
		}
		
		public static Vector4 UnitZ {
			get {
				return _unitZ;
			}
		}
		
		public static Vector4 UnitW {
			get {
				return _unitW;
			}
		}
		
		#endregion
		#region Maths

		public static void Distance (ref Vector4 value1, ref Vector4 value2, out Half result)
		{
			Half num4 = value1.X - value2.X;
			Half num3 = value1.Y - value2.Y;
			Half num2 = value1.Z - value2.Z;
			Half num = value1.W - value2.W;
			Half num5 = (((num4 * num4) + (num3 * num3)) + (num2 * num2)) + (num * num);
			result = RealMaths.Sqrt (num5);
		}

		public static void DistanceSquared (ref Vector4 value1, ref Vector4 value2, out Half result)
		{
			Half num4 = value1.X - value2.X;
			Half num3 = value1.Y - value2.Y;
			Half num2 = value1.Z - value2.Z;
			Half num = value1.W - value2.W;
			result = (((num4 * num4) + (num3 * num3)) + (num2 * num2)) + (num * num);
		}

		public static void Dot (ref Vector4 vector1, ref Vector4 vector2, out Half result)
		{
			result = (((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z)) + (vector1.W * vector2.W);
		}

		public static void Normalise (ref Vector4 vector, out Vector4 result)
		{
			Half one = 1;
			Half num2 = (((vector.X * vector.X) + (vector.Y * vector.Y)) + (vector.Z * vector.Z)) + (vector.W * vector.W);
			Half num = one / (RealMaths.Sqrt (num2));
			result.X = vector.X * num;
			result.Y = vector.Y * num;
			result.Z = vector.Z * num;
			result.W = vector.W * num;
		}

		public static void Transform (ref Vector2 position, ref Matrix44 matrix, out Vector4 result)
		{
			Half num4 = ((position.X * matrix.M11) + (position.Y * matrix.M21)) + matrix.M41;
			Half num3 = ((position.X * matrix.M12) + (position.Y * matrix.M22)) + matrix.M42;
			Half num2 = ((position.X * matrix.M13) + (position.Y * matrix.M23)) + matrix.M43;
			Half num = ((position.X * matrix.M14) + (position.Y * matrix.M24)) + matrix.M44;
			result.X = num4;
			result.Y = num3;
			result.Z = num2;
			result.W = num;
		}
		
		public static void Transform (ref Vector3 position, ref Matrix44 matrix, out Vector4 result)
		{
			Half num4 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
			Half num3 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
			Half num2 = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
			Half num = (((position.X * matrix.M14) + (position.Y * matrix.M24)) + (position.Z * matrix.M34)) + matrix.M44;
			result.X = num4;
			result.Y = num3;
			result.Z = num2;
			result.W = num;
		}
		
		public static void Transform (ref Vector4 vector, ref Matrix44 matrix, out Vector4 result)
		{
			Half num4 = (((vector.X * matrix.M11) + (vector.Y * matrix.M21)) + (vector.Z * matrix.M31)) + (vector.W * matrix.M41);
			Half num3 = (((vector.X * matrix.M12) + (vector.Y * matrix.M22)) + (vector.Z * matrix.M32)) + (vector.W * matrix.M42);
			Half num2 = (((vector.X * matrix.M13) + (vector.Y * matrix.M23)) + (vector.Z * matrix.M33)) + (vector.W * matrix.M43);
			Half num = (((vector.X * matrix.M14) + (vector.Y * matrix.M24)) + (vector.Z * matrix.M34)) + (vector.W * matrix.M44);
			result.X = num4;
			result.Y = num3;
			result.Z = num2;
			result.W = num;
		}
		
		
		public static void Transform (ref Vector2 value, ref Quaternion rotation, out Vector4 result)
		{
			Half one = 1;
			Half num6 = rotation.X + rotation.X;
			Half num2 = rotation.Y + rotation.Y;
			Half num = rotation.Z + rotation.Z;
			Half num15 = rotation.W * num6;
			Half num14 = rotation.W * num2;
			Half num5 = rotation.W * num;
			Half num13 = rotation.X * num6;
			Half num4 = rotation.X * num2;
			Half num12 = rotation.X * num;
			Half num11 = rotation.Y * num2;
			Half num10 = rotation.Y * num;
			Half num3 = rotation.Z * num;
			Half num9 = (value.X * ((one - num11) - num3)) + (value.Y * (num4 - num5));
			Half num8 = (value.X * (num4 + num5)) + (value.Y * ((one - num13) - num3));
			Half num7 = (value.X * (num12 - num14)) + (value.Y * (num10 + num15));
			result.X = num9;
			result.Y = num8;
			result.Z = num7;
			result.W = one;
		}
		
		public static void Transform (ref Vector3 value, ref Quaternion rotation, out Vector4 result)
		{
			Half one = 1;
			Half num12 = rotation.X + rotation.X;
			Half num2 = rotation.Y + rotation.Y;
			Half num = rotation.Z + rotation.Z;
			Half num11 = rotation.W * num12;
			Half num10 = rotation.W * num2;
			Half num9 = rotation.W * num;
			Half num8 = rotation.X * num12;
			Half num7 = rotation.X * num2;
			Half num6 = rotation.X * num;
			Half num5 = rotation.Y * num2;
			Half num4 = rotation.Y * num;
			Half num3 = rotation.Z * num;
			Half num15 = ((value.X * ((one - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
			Half num14 = ((value.X * (num7 + num9)) + (value.Y * ((one - num8) - num3))) + (value.Z * (num4 - num11));
			Half num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((one - num8) - num5));
			result.X = num15;
			result.Y = num14;
			result.Z = num13;
			result.W = one;
		}
		
		public static void Transform (ref Vector4 value, ref Quaternion rotation, out Vector4 result)
		{
			Half one = 1;
			Half num12 = rotation.X + rotation.X;
			Half num2 = rotation.Y + rotation.Y;
			Half num = rotation.Z + rotation.Z;
			Half num11 = rotation.W * num12;
			Half num10 = rotation.W * num2;
			Half num9 = rotation.W * num;
			Half num8 = rotation.X * num12;
			Half num7 = rotation.X * num2;
			Half num6 = rotation.X * num;
			Half num5 = rotation.Y * num2;
			Half num4 = rotation.Y * num;
			Half num3 = rotation.Z * num;
			Half num15 = ((value.X * ((one - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
			Half num14 = ((value.X * (num7 + num9)) + (value.Y * ((one - num8) - num3))) + (value.Z * (num4 - num11));
			Half num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((one - num8) - num5));
			result.X = num15;
			result.Y = num14;
			result.Z = num13;
			result.W = value.W;
		}
		
		#endregion
		#region Operators

		public static Vector4 operator - (Vector4 value)
		{
			Vector4 vector;
			vector.X = -value.X;
			vector.Y = -value.Y;
			vector.Z = -value.Z;
			vector.W = -value.W;
			return vector;
		}
		
		public static Boolean operator == (Vector4 value1, Vector4 value2)
		{
			return ((((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) && (value1.W == value2.W));
		}
		
		public static Boolean operator != (Vector4 value1, Vector4 value2)
		{
			if (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) {
				return !(value1.W == value2.W);
			}
			return true;
		}
		
		public static Vector4 operator + (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			vector.Z = value1.Z + value2.Z;
			vector.W = value1.W + value2.W;
			return vector;
		}
		
		public static Vector4 operator - (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			vector.Z = value1.Z - value2.Z;
			vector.W = value1.W - value2.W;
			return vector;
		}
		
		public static Vector4 operator * (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X * value2.X;
			vector.Y = value1.Y * value2.Y;
			vector.Z = value1.Z * value2.Z;
			vector.W = value1.W * value2.W;
			return vector;
		}
		
		public static Vector4 operator * (Vector4 value1, Half scaleFactor)
		{
			Vector4 vector;
			vector.X = value1.X * scaleFactor;
			vector.Y = value1.Y * scaleFactor;
			vector.Z = value1.Z * scaleFactor;
			vector.W = value1.W * scaleFactor;
			return vector;
		}
		
		public static Vector4 operator * (Half scaleFactor, Vector4 value1)
		{
			Vector4 vector;
			vector.X = value1.X * scaleFactor;
			vector.Y = value1.Y * scaleFactor;
			vector.Z = value1.Z * scaleFactor;
			vector.W = value1.W * scaleFactor;
			return vector;
		}
		
		public static Vector4 operator / (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			vector.Z = value1.Z / value2.Z;
			vector.W = value1.W / value2.W;
			return vector;
		}
		
		public static Vector4 operator / (Vector4 value1, Half divider)
		{
			Half one = 1;
			Vector4 vector;
			Half num = one / divider;
			vector.X = value1.X * num;
			vector.Y = value1.Y * num;
			vector.Z = value1.Z * num;
			vector.W = value1.W * num;
			return vector;
		}
		
		public static void Negate (ref Vector4 value, out Vector4 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = -value.W;
		}

		public static void Add (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
		}
		
		public static void Subtract (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
		}
		
		public static void Multiply (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
			result.W = value1.W * value2.W;
		}

		public static void Multiply (ref Vector4 value1, Half scaleFactor, out Vector4 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
			result.W = value1.W * scaleFactor;
		}

		public static void Divide (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
			result.W = value1.W / value2.W;
		}
		
		public static void Divide (ref Vector4 value1, Half divider, out Vector4 result)
		{
			Half one = 1;
			Half num = one / divider;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
			result.W = value1.W * num;
		}
		
		#endregion
		#region Splines

		public static void Barycentric (ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, Half amount1, Half amount2, out Vector4 result)
		{
			result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
			result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
			result.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
			result.W = (value1.W + (amount1 * (value2.W - value1.W))) + (amount2 * (value3.W - value1.W));
		}

		public static void SmoothStep (ref Vector4 value1, ref Vector4 value2, Half amount, out Vector4 result)
		{
			Half zero = 0;
			Half one = 1;
			Half two = 2;
			Half three = 3;

			amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
			amount = (amount * amount) * (three - (two * amount));
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
			result.W = value1.W + ((value2.W - value1.W) * amount);
		}

		public static void CatmullRom (ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, ref Vector4 value4, Half amount, out Vector4 result)
		{
			Half half; RealMaths.Half(out half);
			Half two = 2;
			Half three = 3;
			Half four = 4;
			Half five = 5;

			Half num = amount * amount;
			Half num2 = amount * num;
			result.X = half * ((((two * value2.X) + ((-value1.X + value3.X) * amount)) + (((((two * value1.X) - (five * value2.X)) + (four * value3.X)) - value4.X) * num)) + ((((-value1.X + (three * value2.X)) - (three * value3.X)) + value4.X) * num2));
			result.Y = half * ((((two * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((two * value1.Y) - (five * value2.Y)) + (four * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (three * value2.Y)) - (three * value3.Y)) + value4.Y) * num2));
			result.Z = half * ((((two * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((two * value1.Z) - (five * value2.Z)) + (four * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (three * value2.Z)) - (three * value3.Z)) + value4.Z) * num2));
			result.W = half * ((((two * value2.W) + ((-value1.W + value3.W) * amount)) + (((((two * value1.W) - (five * value2.W)) + (four * value3.W)) - value4.W) * num)) + ((((-value1.W + (three * value2.W)) - (three * value3.W)) + value4.W) * num2));
		}

		public static void Hermite (ref Vector4 value1, ref Vector4 tangent1, ref Vector4 value2, ref Vector4 tangent2, Half amount, out Vector4 result)
		{
			Half one = 1;
			Half two = 2;
			Half three = 3;

			Half num = amount * amount;
			Half num6 = amount * num;
			Half num5 = ((two * num6) - (three * num)) + one;
			Half num4 = (-two * num6) + (three * num);
			Half num3 = (num6 - (two * num)) + amount;
			Half num2 = num6 - num;
			result.X = (((value1.X * num5) + (value2.X * num4)) + (tangent1.X * num3)) + (tangent2.X * num2);
			result.Y = (((value1.Y * num5) + (value2.Y * num4)) + (tangent1.Y * num3)) + (tangent2.Y * num2);
			result.Z = (((value1.Z * num5) + (value2.Z * num4)) + (tangent1.Z * num3)) + (tangent2.Z * num2);
			result.W = (((value1.W * num5) + (value2.W * num4)) + (tangent1.W * num3)) + (tangent2.W * num2);
		}
		
		#endregion

		#region Utilities

		public static void Min (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z < value2.Z) ? value1.Z : value2.Z;
			result.W = (value1.W < value2.W) ? value1.W : value2.W;
		}

		public static void Max (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z > value2.Z) ? value1.Z : value2.Z;
			result.W = (value1.W > value2.W) ? value1.W : value2.W;
		}
		
		public static void Clamp (ref Vector4 value1, ref Vector4 min, ref Vector4 max, out Vector4 result)
		{
			Half x = value1.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;
			Half y = value1.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;
			Half z = value1.Z;
			z = (z > max.Z) ? max.Z : z;
			z = (z < min.Z) ? min.Z : z;
			Half w = value1.W;
			w = (w > max.W) ? max.W : w;
			w = (w < min.W) ? min.W : w;
			result.X = x;
			result.Y = y;
			result.Z = z;
			result.W = w;
		}
		
		public static void Lerp (ref Vector4 value1, ref Vector4 value2, Half amount, out Vector4 result)
		{
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
			result.W = value1.W + ((value2.W - value1.W) * amount);
		}
		
		#endregion


	}

}

namespace Sungiant.Abacus.SinglePrecision
{
	public static class GaussianElimination
	{

	}
	public class GjkDistance
	{
		public GjkDistance ()
		{
			for (Int32 i = 0; i < 0x10; i++)
			{
				this.det [i] = new Single[4];
			}
		}

		public Boolean AddSupportPoint (ref Vector3 newPoint)
		{
			Int32 index = (BitsToIndices [this.simplexBits ^ 15] & 7) - 1;

			this.y [index] = newPoint;
			this.yLengthSq [index] = newPoint.LengthSquared ();

			for (Int32 i = BitsToIndices[this.simplexBits]; i != 0; i = i >> 3)
			{
				Int32 num2 = (i & 7) - 1;
				Vector3 vector = this.y [num2] - newPoint;

				this.edges [num2] [index] = vector;
				this.edges [index] [num2] = -vector;
				this.edgeLengthSq [index] [num2] = this.edgeLengthSq [num2] [index] = vector.LengthSquared ();
			}

			this.UpdateDeterminant (index);

			return this.UpdateSimplex (index);
		}

		public void Reset ()
		{
			Single zero = 0;

			this.simplexBits = 0;
			this.maxLengthSq = zero;
		}

		public Vector3 ClosestPoint
		{
			get { return this.closestPoint; }
		}
		
		public Boolean FullSimplex
		{
			get { return (this.simplexBits == 15); }
		}
		
		public Single MaxLengthSquared
		{
			get { return this.maxLengthSq; }
		}

		Vector3 closestPoint;
		Single[][] det = new Single[0x10][];
		Single[][] edgeLengthSq = new Single[][] { new Single[4], new Single[4], new Single[4], new Single[4] };
		Vector3[][] edges = new Vector3[][] { new Vector3[4], new Vector3[4], new Vector3[4], new Vector3[4] };
		Single maxLengthSq;
		Int32 simplexBits;
		Vector3[] y = new Vector3[4];
		Single[] yLengthSq = new Single[4];

		static Int32[] BitsToIndices = new Int32[] { 0, 1, 2, 0x11, 3, 0x19, 0x1a, 0xd1, 4, 0x21, 0x22, 0x111, 0x23, 0x119, 0x11a, 0x8d1 };

		Vector3 ComputeClosestPoint ()
		{
			Single fzero; RealMaths.Zero(out fzero);

			Single num3 = fzero;
			Vector3 zero = Vector3.Zero;

			this.maxLengthSq = fzero;

			for (Int32 i = BitsToIndices[this.simplexBits]; i != 0; i = i >> 3)
			{
				Int32 index = (i & 7) - 1;
				Single num4 = this.det [this.simplexBits] [index];

				num3 += num4;
				zero += (Vector3)(this.y [index] * num4);

				this.maxLengthSq = RealMaths.Max (this.maxLengthSq, this.yLengthSq [index]);
			}

			return (Vector3)(zero / num3);
		}

		Boolean IsSatisfiesRule (Int32 xBits, Int32 yBits)
		{
			Single fzero; RealMaths.Zero(out fzero);

			for (Int32 i = BitsToIndices[yBits]; i != 0; i = i >> 3)
			{
				Int32 index = (i & 7) - 1;
				Int32 num3 = ((Int32)1) << index;

				if ((num3 & xBits) != 0)
				{
					if (this.det [xBits] [index] <= fzero)
					{
						return false;
					}
				}
				else if (this.det [xBits | num3] [index] > fzero)
				{
					return false;
				}
			}

			return true;
		}

		void UpdateDeterminant (Int32 xmIdx)
		{
			Single fone; RealMaths.One(out fone);
			Int32 index = ((Int32)1) << xmIdx;

			this.det [index] [xmIdx] = fone;

			Int32 num14 = BitsToIndices [this.simplexBits];
			Int32 num8 = num14;

			for (Int32 i = 0; num8 != 0; i++)
			{
				Int32 num = (num8 & 7) - 1;
				Int32 num12 = ((int)1) << num;
				Int32 num6 = num12 | index;

				this.det [num6] [num] = Dot (ref this.edges [xmIdx] [num], ref this.y [xmIdx]);
				this.det [num6] [xmIdx] = Dot (ref this.edges [num] [xmIdx], ref this.y [num]);

				Int32 num11 = num14;

				for (Int32 j = 0; j < i; j++)
				{
					int num3 = (num11 & 7) - 1;
					int num5 = ((int)1) << num3;
					int num9 = num6 | num5;
					int num4 = (this.edgeLengthSq [num] [num3] < this.edgeLengthSq [xmIdx] [num3]) ? num : xmIdx;

					this.det [num9] [num3] = 
						(this.det [num6] [num] * Dot (ref this.edges [num4] [num3], ref this.y [num])) + 
						(this.det [num6] [xmIdx] * Dot (ref this.edges [num4] [num3], ref this.y [xmIdx]));

					num4 = (this.edgeLengthSq [num3] [num] < this.edgeLengthSq [xmIdx] [num]) ? num3 : xmIdx;

					this.det [num9] [num] = 
						(this.det [num5 | index] [num3] * Dot (ref this.edges [num4] [num], ref this.y [num3])) + 
						(this.det [num5 | index] [xmIdx] * Dot (ref this.edges [num4] [num], ref this.y [xmIdx]));

					num4 = (this.edgeLengthSq [num] [xmIdx] < this.edgeLengthSq [num3] [xmIdx]) ? num : num3;

					this.det [num9] [xmIdx] = 
						(this.det [num12 | num5] [num3] * Dot (ref this.edges [num4] [xmIdx], ref this.y [num3])) + 
						(this.det [num12 | num5] [num] * Dot (ref this.edges [num4] [xmIdx], ref this.y [num]));

					num11 = num11 >> 3;
				}

				num8 = num8 >> 3;
			}

			if ((this.simplexBits | index) == 15)
			{
				int num2 = 
					(this.edgeLengthSq [1] [0] < this.edgeLengthSq [2] [0]) ? 
					((this.edgeLengthSq [1] [0] < this.edgeLengthSq [3] [0]) ? 1 : 3) : 
					((this.edgeLengthSq [2] [0] < this.edgeLengthSq [3] [0]) ? 2 : 3);

				this.det [15] [0] = 
					((this.det [14] [1] * Dot (ref this.edges [num2] [0], ref this.y [1])) + 
					(this.det [14] [2] * Dot (ref this.edges [num2] [0], ref this.y [2]))) + 
					(this.det [14] [3] * Dot (ref this.edges [num2] [0], ref this.y [3]));

				num2 = 
					(this.edgeLengthSq [0] [1] < this.edgeLengthSq [2] [1]) ? 
					((this.edgeLengthSq [0] [1] < this.edgeLengthSq [3] [1]) ? 0 : 3) : 
					((this.edgeLengthSq [2] [1] < this.edgeLengthSq [3] [1]) ? 2 : 3);

				this.det [15] [1] = 
					((this.det [13] [0] * Dot (ref this.edges [num2] [1], ref this.y [0])) + 
				    (this.det [13] [2] * Dot (ref this.edges [num2] [1], ref this.y [2]))) + 
					(this.det [13] [3] * Dot (ref this.edges [num2] [1], ref this.y [3]));

				num2 = 
					(this.edgeLengthSq [0] [2] < this.edgeLengthSq [1] [2]) ? 
					((this.edgeLengthSq [0] [2] < this.edgeLengthSq [3] [2]) ? 0 : 3) : 
					((this.edgeLengthSq [1] [2] < this.edgeLengthSq [3] [2]) ? 1 : 3);

				this.det [15] [2] = 
					((this.det [11] [0] * Dot (ref this.edges [num2] [2], ref this.y [0])) + 
					(this.det [11] [1] * Dot (ref this.edges [num2] [2], ref this.y [1]))) + 
					(this.det [11] [3] * Dot (ref this.edges [num2] [2], ref this.y [3]));

				num2 = 
					(this.edgeLengthSq [0] [3] < this.edgeLengthSq [1] [3]) ? 
					((this.edgeLengthSq [0] [3] < this.edgeLengthSq [2] [3]) ? 0 : 2) : 
					((this.edgeLengthSq [1] [3] < this.edgeLengthSq [2] [3]) ? 1 : 2);

				this.det [15] [3] = 
					((this.det [7] [0] * Dot (ref this.edges [num2] [3], ref this.y [0])) + 
					(this.det [7] [1] * Dot (ref this.edges [num2] [3], ref this.y [1]))) + 
					(this.det [7] [2] * Dot (ref this.edges [num2] [3], ref this.y [2]));
			}
		}

		Boolean UpdateSimplex (Int32 newIndex)
		{
			Int32 yBits = this.simplexBits | (((Int32)1) << newIndex);

			Int32 xBits = ((Int32)1) << newIndex;

			for (Int32 i = this.simplexBits; i != 0; i--)
			{
				if (((i & yBits) == i) && this.IsSatisfiesRule (i | xBits, yBits))
				{
					this.simplexBits = i | xBits;
					this.closestPoint = this.ComputeClosestPoint ();

					return true;
				}
			}

			Boolean flag = false;

			if (this.IsSatisfiesRule (xBits, yBits))
			{
				this.simplexBits = xBits;
				this.closestPoint = this.y [newIndex];
				this.maxLengthSq = this.yLengthSq [newIndex];

				flag = true;
			}

			return flag;
		}

		static Single Dot (ref Vector3 a, ref Vector3 b)
		{
			return (((a.X * b.X) + (a.Y * b.Y)) + (a.Z * b.Z));
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct BoundingBox 
		: IEquatable<BoundingBox>
	{
		public const int CornerCount = 8;
		public Vector3 Min;
		public Vector3 Max;

		public Vector3[] GetCorners ()
		{
			return new Vector3[] { new Vector3 (this.Min.X, this.Max.Y, this.Max.Z), new Vector3 (this.Max.X, this.Max.Y, this.Max.Z), new Vector3 (this.Max.X, this.Min.Y, this.Max.Z), new Vector3 (this.Min.X, this.Min.Y, this.Max.Z), new Vector3 (this.Min.X, this.Max.Y, this.Min.Z), new Vector3 (this.Max.X, this.Max.Y, this.Min.Z), new Vector3 (this.Max.X, this.Min.Y, this.Min.Z), new Vector3 (this.Min.X, this.Min.Y, this.Min.Z) };
		}

		public BoundingBox (Vector3 min, Vector3 max)
		{
			this.Min = min;
			this.Max = max;
		}

		public Boolean Equals (BoundingBox other)
		{
			return ((this.Min == other.Min) && (this.Max == other.Max));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is BoundingBox) {
				flag = this.Equals ((BoundingBox)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Min.GetHashCode () + this.Max.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Min:{0} Max:{1}}}", new Object[] { this.Min.ToString (), this.Max.ToString () });
		}

		public static void CreateMerged (ref BoundingBox original, ref BoundingBox additional, out BoundingBox result)
		{
			Vector3 vector;
			Vector3 vector2;
			Vector3.Min (ref original.Min, ref additional.Min, out vector2);
			Vector3.Max (ref original.Max, ref additional.Max, out vector);
			result.Min = vector2;
			result.Max = vector;
		}

		public static void CreateFromSphere (ref BoundingSphere sphere, out BoundingBox result)
		{
			result.Min.X = sphere.Center.X - sphere.Radius;
			result.Min.Y = sphere.Center.Y - sphere.Radius;
			result.Min.Z = sphere.Center.Z - sphere.Radius;
			result.Max.X = sphere.Center.X + sphere.Radius;
			result.Max.Y = sphere.Center.Y + sphere.Radius;
			result.Max.Z = sphere.Center.Z + sphere.Radius;
		}

		public static BoundingBox CreateFromPoints (IEnumerable<Vector3> points)
		{
			if (points == null) {
				throw new ArgumentNullException ();
			}
			Boolean flag = false;
			Vector3 vector3 = new Vector3 (Single.MaxValue);
			Vector3 vector2 = new Vector3 (Single.MinValue);
			foreach (Vector3 vector in points) {
				Vector3 vector4 = vector;
				Vector3.Min (ref vector3, ref vector4, out vector3);
				Vector3.Max (ref vector2, ref vector4, out vector2);
				flag = true;
			}
			if (!flag) {
				throw new ArgumentException ("BoundingBoxZeroPoints");
			}
			return new BoundingBox (vector3, vector2);
		}

		public Boolean Intersects (ref BoundingBox box)
		{
			if ((this.Max.X < box.Min.X) || (this.Min.X > box.Max.X)) {
				return false;
			}
			if ((this.Max.Y < box.Min.Y) || (this.Min.Y > box.Max.Y)) {
				return false;
			}
			return ((this.Max.Z >= box.Min.Z) && (this.Min.Z <= box.Max.Z));
		}

		public Boolean Intersects (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			return frustum.Intersects (ref this);
		}

		public PlaneIntersectionType Intersects (ref Plane plane)
		{
			Single zero = 0;

			Vector3 vector;
			Vector3 vector2;
			vector2.X = (plane.Normal.X >= zero) ? this.Min.X : this.Max.X;
			vector2.Y = (plane.Normal.Y >= zero) ? this.Min.Y : this.Max.Y;
			vector2.Z = (plane.Normal.Z >= zero) ? this.Min.Z : this.Max.Z;
			vector.X = (plane.Normal.X >= zero) ? this.Max.X : this.Min.X;
			vector.Y = (plane.Normal.Y >= zero) ? this.Max.Y : this.Min.Y;
			vector.Z = (plane.Normal.Z >= zero) ? this.Max.Z : this.Min.Z;
			Single num = ((plane.Normal.X * vector2.X) + (plane.Normal.Y * vector2.Y)) + (plane.Normal.Z * vector2.Z);
			if ((num + plane.D) > zero) {
				return PlaneIntersectionType.Front;
			}
			num = ((plane.Normal.X * vector.X) + (plane.Normal.Y * vector.Y)) + (plane.Normal.Z * vector.Z);
			if ((num + plane.D) < zero) {
				return PlaneIntersectionType.Back;
			}
			return PlaneIntersectionType.Intersecting;
		}

		public Single? Intersects (ref Ray ray)
		{
			Single epsilon; RealMaths.Epsilon(out epsilon);

			Single zero = 0;
			Single one = 1;

			Single num = zero;
			Single maxValue = Single.MaxValue;
			if (RealMaths.Abs (ray.Direction.X) < epsilon) {
				if ((ray.Position.X < this.Min.X) || (ray.Position.X > this.Max.X)) {
					return null;
				}
			} else {
				Single num11 = one / ray.Direction.X;
				Single num8 = (this.Min.X - ray.Position.X) * num11;
				Single num7 = (this.Max.X - ray.Position.X) * num11;
				if (num8 > num7) {
					Single num14 = num8;
					num8 = num7;
					num7 = num14;
				}
				num = RealMaths.Max (num8, num);
				maxValue = RealMaths.Min (num7, maxValue);
				if (num > maxValue) {
					return null;
				}
			}
			if (RealMaths.Abs (ray.Direction.Y) < epsilon) {
				if ((ray.Position.Y < this.Min.Y) || (ray.Position.Y > this.Max.Y)) {
					return null;
				}
			} else {
				Single num10 = one / ray.Direction.Y;
				Single num6 = (this.Min.Y - ray.Position.Y) * num10;
				Single num5 = (this.Max.Y - ray.Position.Y) * num10;
				if (num6 > num5) {
					Single num13 = num6;
					num6 = num5;
					num5 = num13;
				}
				num = RealMaths.Max (num6, num);
				maxValue = RealMaths.Min (num5, maxValue);
				if (num > maxValue) {
					return null;
				}
			}
			

			if (RealMaths.Abs (ray.Direction.Z) < epsilon) {
				if ((ray.Position.Z < this.Min.Z) || (ray.Position.Z > this.Max.Z)) {
					return null;
				}
			} else {
				Single num9 = one / ray.Direction.Z;
				Single num4 = (this.Min.Z - ray.Position.Z) * num9;
				Single num3 = (this.Max.Z - ray.Position.Z) * num9;
				if (num4 > num3) {
					Single num12 = num4;
					num4 = num3;
					num3 = num12;
				}
				num = RealMaths.Max (num4, num);
				maxValue = RealMaths.Min (num3, maxValue);
				if (num > maxValue) {
					return null;
				}
			}
			return new Single? (num);
		}

		public Boolean Intersects (ref BoundingSphere sphere)
		{
			Single num;
			Vector3 vector;
			Vector3.Clamp (ref sphere.Center, ref this.Min, ref this.Max, out vector);
			Vector3.DistanceSquared (ref sphere.Center, ref vector, out num);
			return (num <= (sphere.Radius * sphere.Radius));
		}

		public ContainmentType Contains (ref BoundingBox box)
		{
			if ((this.Max.X < box.Min.X) || (this.Min.X > box.Max.X)) {
				return ContainmentType.Disjoint;
			}
			if ((this.Max.Y < box.Min.Y) || (this.Min.Y > box.Max.Y)) {
				return ContainmentType.Disjoint;
			}
			if ((this.Max.Z < box.Min.Z) || (this.Min.Z > box.Max.Z)) {
				return ContainmentType.Disjoint;
			}
			if ((((this.Min.X <= box.Min.X) && (box.Max.X <= this.Max.X)) && ((this.Min.Y <= box.Min.Y) && (box.Max.Y <= this.Max.Y))) && ((this.Min.Z <= box.Min.Z) && (box.Max.Z <= this.Max.Z))) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		public ContainmentType Contains (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			if (!frustum.Intersects (ref this)) {
				return ContainmentType.Disjoint;
			}

			for (Int32 i = 0; i < frustum.cornerArray.Length; ++i) {
				Vector3 vector = frustum.cornerArray[i];
				if (this.Contains (ref vector) == ContainmentType.Disjoint) {
					return ContainmentType.Intersects;
				}
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref Vector3 point)
		{
			if ((((this.Min.X <= point.X) && (point.X <= this.Max.X)) && ((this.Min.Y <= point.Y) && (point.Y <= this.Max.Y))) && ((this.Min.Z <= point.Z) && (point.Z <= this.Max.Z))) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Disjoint;
		}

		public ContainmentType Contains (ref BoundingSphere sphere)
		{
			Single num2;
			Vector3 vector;
			Vector3.Clamp (ref sphere.Center, ref this.Min, ref this.Max, out vector);
			Vector3.DistanceSquared (ref sphere.Center, ref vector, out num2);
			Single radius = sphere.Radius;
			if (num2 > (radius * radius)) {
				return ContainmentType.Disjoint;
			}
			if (((((this.Min.X + radius) <= sphere.Center.X) && (sphere.Center.X <= (this.Max.X - radius))) && (((this.Max.X - this.Min.X) > radius) && ((this.Min.Y + radius) <= sphere.Center.Y))) && (((sphere.Center.Y <= (this.Max.Y - radius)) && ((this.Max.Y - this.Min.Y) > radius)) && ((((this.Min.Z + radius) <= sphere.Center.Z) && (sphere.Center.Z <= (this.Max.Z - radius))) && ((this.Max.X - this.Min.X) > radius)))) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		internal void SupportMapping (ref Vector3 v, out Vector3 result)
		{
			Single zero = 0;

			result.X = (v.X >= zero) ? this.Max.X : this.Min.X;
			result.Y = (v.Y >= zero) ? this.Max.Y : this.Min.Y;
			result.Z = (v.Z >= zero) ? this.Max.Z : this.Min.Z;
		}

		public static Boolean operator == (BoundingBox a, BoundingBox b)
		{
			return a.Equals (b);
		}

		public static Boolean operator != (BoundingBox a, BoundingBox b)
		{
			if (!(a.Min != b.Min)) {
				return (a.Max != b.Max);
			}
			return true;
		}
	}
	public class BoundingFrustum 
		: IEquatable<BoundingFrustum>
	{
		const int BottomPlaneIndex = 5;

		internal Vector3[] cornerArray;

		public const int CornerCount = 8;

		const int FarPlaneIndex = 1;

		GjkDistance gjk;

		const int LeftPlaneIndex = 2;

		Matrix44 matrix;

		const int NearPlaneIndex = 0;

		const int NumPlanes = 6;

		Plane[] planes;

		const int RightPlaneIndex = 3;

		const int TopPlaneIndex = 4;

		BoundingFrustum ()
		{
			this.planes = new Plane[6];
			this.cornerArray = new Vector3[8];
		}

		public BoundingFrustum (Matrix44 value)
		{
			this.planes = new Plane[6];
			this.cornerArray = new Vector3[8];
			this.SetMatrix (ref value);
		}

		static Vector3 ComputeIntersection (ref Plane plane, ref Ray ray)
		{
			Single planeNormDotRayPos;
			Single planeNormDotRayDir;

			Vector3.Dot (ref plane.Normal, ref ray.Position, out planeNormDotRayPos);
			Vector3.Dot (ref plane.Normal, ref ray.Direction, out planeNormDotRayDir);

			Single num = (-plane.D - planeNormDotRayPos) / planeNormDotRayDir;

			return (ray.Position + (ray.Direction * num));
		}

		static Ray ComputeIntersectionLine (ref Plane p1, ref Plane p2)
		{
			Ray ray = new Ray ();

			Vector3.Cross (ref p1.Normal, ref p2.Normal, out ray.Direction);

			Single num = ray.Direction.LengthSquared ();

			Vector3 a = (-p1.D * p2.Normal) + (p2.D * p1.Normal);

			Vector3 cross;

			Vector3.Cross (ref a, ref ray.Direction, out cross);

			ray.Position =  cross / num;

			return ray;
		}

		public ContainmentType Contains (ref BoundingBox box)
		{
			Boolean flag = false;
			for(Int32 i = 0; i < this.planes.Length; ++i)
			{
				Plane plane = this.planes[i];
				switch (box.Intersects (ref plane)) {
				case PlaneIntersectionType.Front:
					return ContainmentType.Disjoint;

				case PlaneIntersectionType.Intersecting:
					flag = true;
					break;
				}
			}
			if (!flag) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		public ContainmentType Contains (ref BoundingFrustum frustum)
		{
			if (frustum == null) {
				throw new ArgumentNullException ("frustum");
			}
			ContainmentType disjoint = ContainmentType.Disjoint;
			if (this.Intersects (ref frustum)) {
				disjoint = ContainmentType.Contains;
				for (int i = 0; i < this.cornerArray.Length; i++) {
					if (this.Contains (ref frustum.cornerArray [i]) == ContainmentType.Disjoint) {
						return ContainmentType.Intersects;
					}
				}
			}
			return disjoint;
		}

		public ContainmentType Contains (ref BoundingSphere sphere)
		{
			Vector3 center = sphere.Center;
			Single radius = sphere.Radius;
			int num2 = 0;
			foreach (Plane plane in this.planes) {
				Single num5 = ((plane.Normal.X * center.X) + (plane.Normal.Y * center.Y)) + (plane.Normal.Z * center.Z);
				Single num3 = num5 + plane.D;
				if (num3 > radius) {
					return ContainmentType.Disjoint;
				}
				if (num3 < -radius) {
					num2++;
				}
			}
			if (num2 != 6) {
				return ContainmentType.Intersects;
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref Vector3 point)
		{
			Single epsilon; RealMaths.FromString("0.00001", out epsilon);

			foreach (Plane plane in this.planes) {
				Single num2 = (((plane.Normal.X * point.X) + (plane.Normal.Y * point.Y)) + (plane.Normal.Z * point.Z)) + plane.D;
				if (num2 > epsilon) {
					return ContainmentType.Disjoint;
				}
			}
			return ContainmentType.Contains;
		}

		public Boolean Equals (BoundingFrustum other)
		{
			if (other == null) {
				return false;
			}
			return (this.matrix == other.matrix);
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			BoundingFrustum frustum = obj as BoundingFrustum;
			if (frustum != null) {
				flag = this.matrix == frustum.matrix;
			}
			return flag;
		}

		public Vector3[] GetCorners ()
		{
			return (Vector3[])this.cornerArray.Clone ();
		}

		public override Int32 GetHashCode ()
		{
			return this.matrix.GetHashCode ();
		}

		public Boolean Intersects (ref BoundingBox box)
		{
			Boolean flag;
			this.Intersects (ref box, out flag);
			return flag;
		}

		void Intersects (ref BoundingBox box, out Boolean result)
		{
			Single epsilon; RealMaths.FromString("0.00001", out epsilon);
			Single zero = 0;
			Single four = 4;

			Vector3 closestPoint;
			Vector3 vector2;
			Vector3 vector3;
			Vector3 vector4;
			Vector3 vector5;
			if (this.gjk == null) {
				this.gjk = new GjkDistance ();
			}
			this.gjk.Reset ();
			Vector3.Subtract (ref this.cornerArray [0], ref box.Min, out closestPoint);
			if (closestPoint.LengthSquared () < epsilon) {
				Vector3.Subtract (ref this.cornerArray [0], ref box.Max, out closestPoint);
			}
			Single maxValue = Single.MaxValue;
			Single num3 = zero;
			result = false;
		Label_006D:
			vector5.X = -closestPoint.X;
			vector5.Y = -closestPoint.Y;
			vector5.Z = -closestPoint.Z;
			this.SupportMapping (ref vector5, out vector4);
			box.SupportMapping (ref closestPoint, out vector3);
			Vector3.Subtract (ref vector4, ref vector3, out vector2);
			Single num4 = ((closestPoint.X * vector2.X) + (closestPoint.Y * vector2.Y)) + (closestPoint.Z * vector2.Z);
			if (num4 <= zero) {
				this.gjk.AddSupportPoint (ref vector2);
				closestPoint = this.gjk.ClosestPoint;
				Single num2 = maxValue;
				maxValue = closestPoint.LengthSquared ();
				if ((num2 - maxValue) > (epsilon * num2)) {
					num3 = four * epsilon * this.gjk.MaxLengthSquared;
					if (!this.gjk.FullSimplex && (maxValue >= num3)) {
						goto Label_006D;
					}
					result = true;
				}
			}
		}

		public Boolean Intersects (ref BoundingFrustum frustum)
		{
			Single epsilon; RealMaths.FromString("0.00001", out epsilon);
			Single zero = 0;
			Single four = 4;

			Vector3 closestPoint;
			if (frustum == null) {
				throw new ArgumentNullException ("frustum");
			}
			if (this.gjk == null) {
				this.gjk = new GjkDistance ();
			}
			this.gjk.Reset ();
			Vector3.Subtract (ref this.cornerArray [0], ref frustum.cornerArray [0], out closestPoint);
			if (closestPoint.LengthSquared () < epsilon) {
				Vector3.Subtract (ref this.cornerArray [0], ref frustum.cornerArray [1], out closestPoint);
			}
			Single maxValue = Single.MaxValue;
			Single num3 = zero;
			do {
				Vector3 vector2;
				Vector3 vector3;
				Vector3 vector4;
				Vector3 vector5;
				vector5.X = -closestPoint.X;
				vector5.Y = -closestPoint.Y;
				vector5.Z = -closestPoint.Z;
				this.SupportMapping (ref vector5, out vector4);
				frustum.SupportMapping (ref closestPoint, out vector3);
				Vector3.Subtract (ref vector4, ref vector3, out vector2);
				Single num4 = ((closestPoint.X * vector2.X) + (closestPoint.Y * vector2.Y)) + (closestPoint.Z * vector2.Z);
				if (num4 > zero) {
					return false;
				}
				this.gjk.AddSupportPoint (ref vector2);
				closestPoint = this.gjk.ClosestPoint;
				Single num2 = maxValue;
				maxValue = closestPoint.LengthSquared ();
				num3 = four * epsilon * this.gjk.MaxLengthSquared;
				if ((num2 - maxValue) <= (epsilon * num2)) {
					return false;
				}
			} while (!this.gjk.FullSimplex && (maxValue >= num3));
			return true;
		}

		public Boolean Intersects (ref BoundingSphere sphere)
		{
			Boolean flag;
			this.Intersects (ref sphere, out flag);
			return flag;
		}

		void Intersects (ref BoundingSphere sphere, out Boolean result)
		{
			Single zero = 0;
			Single epsilon; RealMaths.FromString("0.00001", out epsilon);
			Single four = 4;

			Vector3 unitX;
			Vector3 vector2;
			Vector3 vector3;
			Vector3 vector4;
			Vector3 vector5;
			if (this.gjk == null) {
				this.gjk = new GjkDistance ();
			}
			this.gjk.Reset ();
			Vector3.Subtract (ref this.cornerArray [0], ref sphere.Center, out unitX);
			if (unitX.LengthSquared () < epsilon) {
				unitX = Vector3.UnitX;
			}
			Single maxValue = Single.MaxValue;
			Single num3 = zero;
			result = false;
		Label_005A:
			vector5.X = -unitX.X;
			vector5.Y = -unitX.Y;
			vector5.Z = -unitX.Z;
			this.SupportMapping (ref vector5, out vector4);
			sphere.SupportMapping (ref unitX, out vector3);
			Vector3.Subtract (ref vector4, ref vector3, out vector2);
			Single num4 = ((unitX.X * vector2.X) + (unitX.Y * vector2.Y)) + (unitX.Z * vector2.Z);
			if (num4 <= zero) {
				this.gjk.AddSupportPoint (ref vector2);
				unitX = this.gjk.ClosestPoint;
				Single num2 = maxValue;
				maxValue = unitX.LengthSquared ();
				if ((num2 - maxValue) > (epsilon * num2)) {
					num3 = four * epsilon * this.gjk.MaxLengthSquared;
					if (!this.gjk.FullSimplex && (maxValue >= num3)) {
						goto Label_005A;
					}
					result = true;
				}
			}
		}

		public PlaneIntersectionType Intersects (ref Plane plane)
		{
			Single zero = 0;

			int num = 0;
			for (int i = 0; i < 8; i++) {
				Single num3;
				Vector3.Dot (ref this.cornerArray [i], ref plane.Normal, out num3);
				if ((num3 + plane.D) > zero) {
					num |= 1;
				} else {
					num |= 2;
				}
				if (num == 3) {
					return PlaneIntersectionType.Intersecting;
				}
			}
			if (num != 1) {
				return PlaneIntersectionType.Back;
			}
			return PlaneIntersectionType.Front;
		}

		public Single? Intersects (ref Ray ray)
		{
			Single? nullable;
			this.Intersects (ref ray, out nullable);
			return nullable;
		}

		void Intersects (ref Ray ray, out Single? result)
		{
			Single epsilon; RealMaths.FromString("0.00001", out epsilon);
			Single zero = 0;

			ContainmentType type = this.Contains (ref ray.Position);
			if (type == ContainmentType.Contains) {
				result = zero;
			} else {
				Single minValue = Single.MinValue;
				Single maxValue = Single.MaxValue;
				result = zero;
				foreach (Plane plane in this.planes) {
					Single num3;
					Single num6;
					Vector3 normal = plane.Normal;
					Vector3.Dot (ref ray.Direction, ref normal, out num6);
					Vector3.Dot (ref ray.Position, ref normal, out num3);
					num3 += plane.D;
					if (RealMaths.Abs (num6) < epsilon) {
						if (num3 > zero) {
							return;
						}
					} else {
						Single num = -num3 / num6;
						if (num6 < zero) {
							if (num > maxValue) {
								return;
							}
							if (num > minValue) {
								minValue = num;
							}
						} else {
							if (num < minValue) {
								return;
							}
							if (num < maxValue) {
								maxValue = num;
							}
						}
					}
				}
				Single num7 = (minValue >= zero) ? minValue : maxValue;
				if (num7 >= zero) {
					result = new Single? (num7);
				}
			}
		}

		public static Boolean operator == (BoundingFrustum a, BoundingFrustum b)
		{
			return Object.Equals (a, b);
		}

		public static Boolean operator != (BoundingFrustum a, BoundingFrustum b)
		{
			return !Object.Equals (a, b);
		}

		void SetMatrix (ref Matrix44 value)
		{
			this.matrix = value;

			this.planes [2].Normal.X = -value.M14 - value.M11;
			this.planes [2].Normal.Y = -value.M24 - value.M21;
			this.planes [2].Normal.Z = -value.M34 - value.M31;
			this.planes [2].D = -value.M44 - value.M41;

			this.planes [3].Normal.X = -value.M14 + value.M11;
			this.planes [3].Normal.Y = -value.M24 + value.M21;
			this.planes [3].Normal.Z = -value.M34 + value.M31;
			this.planes [3].D = -value.M44 + value.M41;

			this.planes [4].Normal.X = -value.M14 + value.M12;
			this.planes [4].Normal.Y = -value.M24 + value.M22;
			this.planes [4].Normal.Z = -value.M34 + value.M32;
			this.planes [4].D = -value.M44 + value.M42;

			this.planes [5].Normal.X = -value.M14 - value.M12;
			this.planes [5].Normal.Y = -value.M24 - value.M22;
			this.planes [5].Normal.Z = -value.M34 - value.M32;
			this.planes [5].D = -value.M44 - value.M42;

			this.planes [0].Normal.X = -value.M13;
			this.planes [0].Normal.Y = -value.M23;
			this.planes [0].Normal.Z = -value.M33;
			this.planes [0].D = -value.M43;

			this.planes [1].Normal.X = -value.M14 + value.M13;
			this.planes [1].Normal.Y = -value.M24 + value.M23;
			this.planes [1].Normal.Z = -value.M34 + value.M33;
			this.planes [1].D = -value.M44 + value.M43;

			for (int i = 0; i < 6; i++) {
				Single num2 = this.planes [i].Normal.Length ();
				this.planes [i].Normal = (Vector3)(this.planes [i].Normal / num2);
				this.planes [i].D /= num2;
			}

			Ray ray = ComputeIntersectionLine (ref this.planes [0], ref this.planes [2]);

			this.cornerArray [0] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [3] = ComputeIntersection (ref this.planes [5], ref ray);

			ray = ComputeIntersectionLine (ref this.planes [3], ref this.planes [0]);

			this.cornerArray [1] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [2] = ComputeIntersection (ref this.planes [5], ref ray);

			ray = ComputeIntersectionLine (ref this.planes [2], ref this.planes [1]);

			this.cornerArray [4] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [7] = ComputeIntersection (ref this.planes [5], ref ray);

			ray = ComputeIntersectionLine (ref this.planes [1], ref this.planes [3]);

			this.cornerArray [5] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [6] = ComputeIntersection (ref this.planes [5], ref ray);
		}

		internal void SupportMapping (ref Vector3 v, out Vector3 result)
		{
			Single num3;

			int index = 0;

			Vector3.Dot (ref this.cornerArray [0], ref v, out num3);

			for (int i = 1; i < this.cornerArray.Length; i++)
			{
				Single num2;

				Vector3.Dot (ref this.cornerArray [i], ref v, out num2);

				if (num2 > num3)
				{
					index = i;
					num3 = num2;
				}
			}

			result = this.cornerArray [index];
		}

		public override String ToString ()
		{
			return string.Format ("{{Near:{0} Far:{1} Left:{2} Right:{3} Top:{4} Bottom:{5}}}", new Object[] { this.Near.ToString (), this.Far.ToString (), this.Left.ToString (), this.Right.ToString (), this.Top.ToString (), this.Bottom.ToString () });
		}

		// Properties
		public Plane Bottom
		{
			get
			{
				return this.planes [5];
			}
		}

		public Plane Far {
			get {
				return this.planes [1];
			}
		}

		public Plane Left {
			get {
				return this.planes [2];
			}
		}

		public Matrix44 Matrix {
			get {
				return this.matrix;
			}
			set {
				this.SetMatrix (ref value);
			}
		}

		public Plane Near {
			get {
				return this.planes [0];
			}
		}

		public Plane Right {
			get {
				return this.planes [3];
			}
		}

		public Plane Top {
			get {
				return this.planes [4];
			}
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct BoundingSphere 
		: IEquatable<BoundingSphere>
	{
		public Vector3 Center;
		public Single Radius;

		public BoundingSphere (Vector3 center, Single radius)
		{
			Single zero = 0;

			if (radius < zero) {
				throw new ArgumentException ("NegativeRadius");
			}
			this.Center = center;
			this.Radius = radius;
		}

		public Boolean Equals (BoundingSphere other)
		{
			return ((this.Center == other.Center) && (this.Radius == other.Radius));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is BoundingSphere) {
				flag = this.Equals ((BoundingSphere)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Center.GetHashCode () + this.Radius.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Center:{0} Radius:{1}}}", new Object[] { this.Center.ToString (), this.Radius.ToString () });
		}

		public static void CreateMerged (ref BoundingSphere original, ref BoundingSphere additional, out BoundingSphere result)
		{
			Single half; RealMaths.Half(out half);
			Single one = 1;
			Vector3 vector2;
			Vector3.Subtract (ref additional.Center, ref original.Center, out vector2);
			Single num = vector2.Length ();
			Single radius = original.Radius;
			Single num2 = additional.Radius;
			if ((radius + num2) >= num) {
				if ((radius - num2) >= num) {
					result = original;
					return;
				}
				if ((num2 - radius) >= num) {
					result = additional;
					return;
				}
			}
			Vector3 vector = (Vector3)(vector2 * (one / num));
			Single num5 = RealMaths.Min (-radius, num - num2);
			Single num4 = (RealMaths.Max (radius, num + num2) - num5) * half;
			result.Center = original.Center + ((Vector3)(vector * (num4 + num5)));
			result.Radius = num4;
		}

		public static void CreateFromBoundingBox (ref BoundingBox box, out BoundingSphere result)
		{
			Single half; RealMaths.Half(out half);
			Single num;
			Vector3.Lerp (ref box.Min, ref box.Max, half, out result.Center);
			Vector3.Distance (ref box.Min, ref box.Max, out num);
			result.Radius = num * half;
		}

		public static void CreateFromPoints (IEnumerable<Vector3> points, out BoundingSphere sphere)
		{	
			Single half; RealMaths.Half(out half);
			Single one = 1;

			Single num;
			Single num2;
			Vector3 vector2;
			Single num4;
			Single num5;
			
			Vector3 vector5;
			Vector3 vector6;
			Vector3 vector7;
			Vector3 vector8;
			Vector3 vector9;
			if (points == null) {
				throw new ArgumentNullException ("points");
			}
			IEnumerator<Vector3> enumerator = points.GetEnumerator ();
			if (!enumerator.MoveNext ()) {
				throw new ArgumentException ("BoundingSphereZeroPoints");
			}
			Vector3 vector4 = vector5 = vector6 = vector7 = vector8 = vector9 = enumerator.Current;
			foreach (Vector3 vector in points) {
				if (vector.X < vector4.X) {
					vector4 = vector;
				}
				if (vector.X > vector5.X) {
					vector5 = vector;
				}
				if (vector.Y < vector6.Y) {
					vector6 = vector;
				}
				if (vector.Y > vector7.Y) {
					vector7 = vector;
				}
				if (vector.Z < vector8.Z) {
					vector8 = vector;
				}
				if (vector.Z > vector9.Z) {
					vector9 = vector;
				}
			}
			Vector3.Distance (ref vector5, ref vector4, out num5);
			Vector3.Distance (ref vector7, ref vector6, out num4);
			Vector3.Distance (ref vector9, ref vector8, out num2);
			if (num5 > num4) {
				if (num5 > num2) {
					Vector3.Lerp (ref vector5, ref vector4, half, out vector2);
					num = num5 * half;
				} else {
					Vector3.Lerp (ref vector9, ref vector8, half, out vector2);
					num = num2 * half;
				}
			} else if (num4 > num2) {
				Vector3.Lerp (ref vector7, ref vector6, half, out vector2);
				num = num4 * half;
			} else {
				Vector3.Lerp (ref vector9, ref vector8, half, out vector2);
				num = num2 * half;
			}
			foreach (Vector3 vector10 in points) {
				Vector3 vector3;
				vector3.X = vector10.X - vector2.X;
				vector3.Y = vector10.Y - vector2.Y;
				vector3.Z = vector10.Z - vector2.Z;
				Single num3 = vector3.Length ();
				if (num3 > num) {
					num = (num + num3) * half;
					vector2 += (Vector3)((one - (num / num3)) * vector3);
				}
			}
			sphere.Center = vector2;
			sphere.Radius = num;
		}

		public static void CreateFromFrustum (ref BoundingFrustum frustum, out BoundingSphere sphere)
		{
			if (frustum == null) {
				throw new ArgumentNullException ("frustum");
			}

			CreateFromPoints (frustum.cornerArray, out sphere);
		}

		public Boolean Intersects (ref BoundingBox box)
		{
			Single num;
			Vector3 vector;
			Vector3.Clamp (ref this.Center, ref box.Min, ref box.Max, out vector);
			Vector3.DistanceSquared (ref this.Center, ref vector, out num);
			return (num <= (this.Radius * this.Radius));
		}

		public Boolean Intersects (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			return frustum.Intersects (ref this);
		}

		public PlaneIntersectionType Intersects (ref Plane plane)
		{
			return plane.Intersects (ref this);
		}

		public Single? Intersects (ref Ray ray)
		{
			return ray.Intersects (ref this);
		}

		public Boolean Intersects (ref BoundingSphere sphere)
		{
			Single two = 2;

			Single num3;
			Vector3.DistanceSquared (ref this.Center, ref sphere.Center, out num3);
			Single radius = this.Radius;
			Single num = sphere.Radius;
			if ((((radius * radius) + ((two * radius) * num)) + (num * num)) <= num3) {
				return false;
			}
			return true;
		}

		public ContainmentType Contains (ref BoundingBox box)
		{
			Vector3 vector;
			if (!box.Intersects (ref this)) {
				return ContainmentType.Disjoint;
			}
			Single num = this.Radius * this.Radius;
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			if (!frustum.Intersects (ref this)) {
				return ContainmentType.Disjoint;
			}
			Single num2 = this.Radius * this.Radius;
			foreach (Vector3 vector2 in frustum.cornerArray) {
				Vector3 vector;
				vector.X = vector2.X - this.Center.X;
				vector.Y = vector2.Y - this.Center.Y;
				vector.Z = vector2.Z - this.Center.Z;
				if (vector.LengthSquared () > num2) {
					return ContainmentType.Intersects;
				}
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref Vector3 point)
		{
			Single temp;
			Vector3.DistanceSquared (ref point, ref this.Center, out temp);

			if (temp >= (this.Radius * this.Radius))
			{
				return ContainmentType.Disjoint;
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref BoundingSphere sphere)
		{
			Single num3;
			Vector3.Distance (ref this.Center, ref sphere.Center, out num3);
			Single radius = this.Radius;
			Single num = sphere.Radius;
			if ((radius + num) < num3) {
				return ContainmentType.Disjoint;
			}
			if ((radius - num) < num3) {
				return ContainmentType.Intersects;
			}
			return ContainmentType.Contains;
		}

		internal void SupportMapping (ref Vector3 v, out Vector3 result)
		{
			Single num2 = v.Length ();
			Single num = this.Radius / num2;
			result.X = this.Center.X + (v.X * num);
			result.Y = this.Center.Y + (v.Y * num);
			result.Z = this.Center.Z + (v.Z * num);
		}

		public BoundingSphere Transform (Matrix44 matrix)
		{
			BoundingSphere sphere = new BoundingSphere ();
			Vector3.Transform (ref this.Center, ref matrix, out sphere.Center);
			Single num4 = ((matrix.M11 * matrix.M11) + (matrix.M12 * matrix.M12)) + (matrix.M13 * matrix.M13);
			Single num3 = ((matrix.M21 * matrix.M21) + (matrix.M22 * matrix.M22)) + (matrix.M23 * matrix.M23);
			Single num2 = ((matrix.M31 * matrix.M31) + (matrix.M32 * matrix.M32)) + (matrix.M33 * matrix.M33);
			Single num = RealMaths.Max (num4, RealMaths.Max (num3, num2));
			sphere.Radius = this.Radius * (RealMaths.Sqrt (num));
			return sphere;
		}

		public void Transform (ref Matrix44 matrix, out BoundingSphere result)
		{
			Vector3.Transform (ref this.Center, ref matrix, out result.Center);
			Single num4 = ((matrix.M11 * matrix.M11) + (matrix.M12 * matrix.M12)) + (matrix.M13 * matrix.M13);
			Single num3 = ((matrix.M21 * matrix.M21) + (matrix.M22 * matrix.M22)) + (matrix.M23 * matrix.M23);
			Single num2 = ((matrix.M31 * matrix.M31) + (matrix.M32 * matrix.M32)) + (matrix.M33 * matrix.M33);
			Single num = RealMaths.Max (num4, RealMaths.Max (num3, num2));
			result.Radius = this.Radius * (RealMaths.Sqrt (num));
		}

		public static Boolean operator == (BoundingSphere a, BoundingSphere b)
		{
			return a.Equals (b);
		}

		public static Boolean operator != (BoundingSphere a, BoundingSphere b)
		{
			if (!(a.Center != b.Center)) {
				return !(a.Radius == b.Radius);
			}
			return true;
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Matrix44 
		: IEquatable<Matrix44>
	{
		// Row 0
		public Single M11;
		public Single M12;
		public Single M13;
		public Single M14;

		// Row 1
		public Single M21;
		public Single M22;
		public Single M23;
		public Single M24;

		// Row 2
		public Single M31;
		public Single M32;
		public Single M33;
		public Single M34;

		// Row 3
		public Single M41; // translation.x
		public Single M42; // translation.y
		public Single M43; // translation.z
		public Single M44;
		
		public Vector3 Up {
			get {
				Vector3 vector;
				vector.X = this.M21;
				vector.Y = this.M22;
				vector.Z = this.M23;
				return vector;
			}
			set {
				this.M21 = value.X;
				this.M22 = value.Y;
				this.M23 = value.Z;
			}
		}

		public Vector3 Down {
			get {
				Vector3 vector;
				vector.X = -this.M21;
				vector.Y = -this.M22;
				vector.Z = -this.M23;
				return vector;
			}
			set {
				this.M21 = -value.X;
				this.M22 = -value.Y;
				this.M23 = -value.Z;
			}
		}

		public Vector3 Right {
			get {
				Vector3 vector;
				vector.X = this.M11;
				vector.Y = this.M12;
				vector.Z = this.M13;
				return vector;
			}
			set {
				this.M11 = value.X;
				this.M12 = value.Y;
				this.M13 = value.Z;
			}
		}

		public Vector3 Left {
			get {
				Vector3 vector;
				vector.X = -this.M11;
				vector.Y = -this.M12;
				vector.Z = -this.M13;
				return vector;
			}
			set {
				this.M11 = -value.X;
				this.M12 = -value.Y;
				this.M13 = -value.Z;
			}
		}

		public Vector3 Forward {
			get {
				Vector3 vector;
				vector.X = -this.M31;
				vector.Y = -this.M32;
				vector.Z = -this.M33;
				return vector;
			}
			set {
				this.M31 = -value.X;
				this.M32 = -value.Y;
				this.M33 = -value.Z;
			}
		}

		public Vector3 Backward {
			get {
				Vector3 vector;
				vector.X = this.M31;
				vector.Y = this.M32;
				vector.Z = this.M33;
				return vector;
			}
			set {
				this.M31 = value.X;
				this.M32 = value.Y;
				this.M33 = value.Z;
			}
		}

		public Vector3 Translation {
			get {
				Vector3 vector;
				vector.X = this.M41;
				vector.Y = this.M42;
				vector.Z = this.M43;
				return vector;
			}
			set {
				this.M41 = value.X;
				this.M42 = value.Y;
				this.M43 = value.Z;
			}
		}

		public Matrix44 (Single m11, Single m12, Single m13, Single m14, Single m21, Single m22, Single m23, Single m24, Single m31, Single m32, Single m33, Single m34, Single m41, Single m42, Single m43, Single m44)
		{
			this.M11 = m11;
			this.M12 = m12;
			this.M13 = m13;
			this.M14 = m14;
			this.M21 = m21;
			this.M22 = m22;
			this.M23 = m23;
			this.M24 = m24;
			this.M31 = m31;
			this.M32 = m32;
			this.M33 = m33;
			this.M34 = m34;
			this.M41 = m41;
			this.M42 = m42;
			this.M43 = m43;
			this.M44 = m44;
		}

		public override String ToString ()
		{
			return ("{ " + string.Format ("{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", new Object[] { this.M11.ToString (), this.M12.ToString (), this.M13.ToString (), this.M14.ToString () }) + string.Format ("{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", new Object[] { this.M21.ToString (), this.M22.ToString (), this.M23.ToString (), this.M24.ToString () }) + string.Format ("{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", new Object[] { this.M31.ToString (), this.M32.ToString (), this.M33.ToString (), this.M34.ToString () }) + string.Format ("{{M41:{0} M42:{1} M43:{2} M44:{3}}} ", new Object[] { this.M41.ToString (), this.M42.ToString (), this.M43.ToString (), this.M44.ToString () }) + "}");
		}

		public Boolean Equals (Matrix44 other)
		{
			return ((((((this.M11 == other.M11) && (this.M22 == other.M22)) && ((this.M33 == other.M33) && (this.M44 == other.M44))) && (((this.M12 == other.M12) && (this.M13 == other.M13)) && ((this.M14 == other.M14) && (this.M21 == other.M21)))) && ((((this.M23 == other.M23) && (this.M24 == other.M24)) && ((this.M31 == other.M31) && (this.M32 == other.M32))) && (((this.M34 == other.M34) && (this.M41 == other.M41)) && (this.M42 == other.M42)))) && (this.M43 == other.M43));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Matrix44)
			{
				flag = this.Equals ((Matrix44)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (((((((((((((((this.M11.GetHashCode () + this.M12.GetHashCode ()) + this.M13.GetHashCode ()) + this.M14.GetHashCode ()) + this.M21.GetHashCode ()) + this.M22.GetHashCode ()) + this.M23.GetHashCode ()) + this.M24.GetHashCode ()) + this.M31.GetHashCode ()) + this.M32.GetHashCode ()) + this.M33.GetHashCode ()) + this.M34.GetHashCode ()) + this.M41.GetHashCode ()) + this.M42.GetHashCode ()) + this.M43.GetHashCode ()) + this.M44.GetHashCode ());
		}

		#region Constants

		static Matrix44 identity;

		static Matrix44 ()
		{
			Single zero = 0;
			Single one = 1;
			identity = new Matrix44 (one, zero, zero, zero, zero, one, zero, zero, zero, zero, one, zero, zero, zero, zero, one);
		}

		public static Matrix44 Identity {
			get {
				return identity;
			}
		}
		
		#endregion
		#region Create

		public static void CreateTranslation (ref Vector3 position, out Matrix44 result)
		{
			result.M11 = 1;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = 1;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = 1;
			result.M34 = 0;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1;
		}
		
		public static void CreateTranslation (Single xPosition, Single yPosition, Single zPosition, out Matrix44 result)
		{	
			result.M11 = 1;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = 1;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = 1;
			result.M34 = 0;
			result.M41 = xPosition;
			result.M42 = yPosition;
			result.M43 = zPosition;
			result.M44 = 1;
		}
		
		// Creates a scaling matrix based on x, y, z.
		public static void CreateScale (Single xScale, Single yScale, Single zScale, out Matrix44 result)
		{
			result.M11 = xScale;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = yScale;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = zScale;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		// Creates a scaling matrix based on a vector.
		public static void CreateScale (ref Vector3 scales, out Matrix44 result)
		{
			result.M11 = scales.X;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = scales.Y;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = scales.Z;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		// Create a scaling matrix consistant along each axis
		public static void CreateScale (Single scale, out Matrix44 result)
		{
			result.M11 = scale;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = scale;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = scale;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		public static void CreateRotationX (Single radians, out Matrix44 result)
		{
			// http://en.wikipedia.org/wiki/Rotation_matrix

			Single cos = RealMaths.Cos (radians);
			Single sin = RealMaths.Sin (radians);

			result.M11 = 1;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = cos;
			result.M23 = sin;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = -sin;
			result.M33 = cos;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		public static void CreateRotationY (Single radians, out Matrix44 result)
		{
			// http://en.wikipedia.org/wiki/Rotation_matrix

			Single cos = RealMaths.Cos (radians);
			Single sin = RealMaths.Sin (radians);

			result.M11 = cos;
			result.M12 = 0;
			result.M13 = -sin;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = 1;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = sin;
			result.M32 = 0;
			result.M33 = cos;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}
		
		public static void CreateRotationZ (Single radians, out Matrix44 result)
		{
			// http://en.wikipedia.org/wiki/Rotation_matrix

			Single cos = RealMaths.Cos (radians);
			Single sin = RealMaths.Sin (radians);

			result.M11 = cos;
			result.M12 = sin;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = -sin;
			result.M22 = cos;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = 1;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}
		
		public static void CreateFromAxisAngle (ref Vector3 axis, Single angle, out Matrix44 result)
		{
			Single one = 1;

			Single x = axis.X;
			Single y = axis.Y;
			Single z = axis.Z;

			Single sin = RealMaths.Sin (angle);
			Single cos = RealMaths.Cos (angle);

			Single xx = x * x;
			Single yy = y * y;
			Single zz = z * z;

			Single xy = x * y;
			Single xz = x * z;
			Single yz = y * z;

			result.M11 = xx + (cos * (one - xx));
			result.M12 = (xy - (cos * xy)) + (sin * z);
			result.M13 = (xz - (cos * xz)) - (sin * y);
			result.M14 = 0;

			result.M21 = (xy - (cos * xy)) - (sin * z);
			result.M22 = yy + (cos * (one - yy));
			result.M23 = (yz - (cos * yz)) + (sin * x);
			result.M24 = 0;

			result.M31 = (xz - (cos * xz)) + (sin * y);
			result.M32 = (yz - (cos * yz)) - (sin * x);
			result.M33 = zz + (cos * (one - zz));
			result.M34 = 0;

			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = one;
		}
		
		public static void CreateFromAllAxis (ref Vector3 right, ref Vector3 up, ref Vector3 backward, out Matrix44 result)
		{
			if(!right.IsUnit() || !up.IsUnit() || !backward.IsUnit() )
			{
				throw new ArgumentException("The input vertors must be normalised.");
			}

			result.M11 = right.X;
			result.M12 = right.Y;
			result.M13 = right.Z;
			result.M14 = 0;
			result.M21 = up.X;
			result.M22 = up.Y;
			result.M23 = up.Z;
			result.M24 = 0;
			result.M31 = backward.X;
			result.M32 = backward.Y;
			result.M33 = backward.Z;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		public static void CreateWorldNew (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result)
		{
			Vector3 backward = -forward;

			Vector3 right;

			Vector3.Cross (ref up, ref backward, out right);

			right.Normalise();

			Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out result);

			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
		}

		public static void CreateWorld (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result)
		{
			if(!forward.IsUnit() || !up.IsUnit() )
			{
				throw new ArgumentException("The input vertors must be normalised.");
			}

			Vector3 backward = -forward;

			Vector3 vector; Vector3.Normalise (ref backward, out vector);

			Vector3 cross; Vector3.Cross (ref up, ref vector, out cross);

			Vector3 vector2; Vector3.Normalise (ref cross, out vector2);

			Vector3 vector3; Vector3.Cross (ref vector, ref vector2, out vector3);

			result.M11 = vector2.X;
			result.M12 = vector2.Y;
			result.M13 = vector2.Z;
			result.M14 = 0;
			result.M21 = vector3.X;
			result.M22 = vector3.Y;
			result.M23 = vector3.Z;
			result.M24 = 0;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = 0;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1;
		}

		public static void CreateFromQuaternion (ref Quaternion quaternion, out Matrix44 result)
		{
			if(!quaternion.IsUnit())
			{
				throw new ArgumentException("Input quaternion must be normalised.");
			}

			Single zero = 0;
			Single one = 1;

			Single xs = quaternion.X + quaternion.X;   
			Single ys = quaternion.Y + quaternion.Y;
			Single zs = quaternion.Z + quaternion.Z;
			Single wx = quaternion.W * xs;
			Single wy = quaternion.W * ys;
			Single wz = quaternion.W * zs;
			Single xx = quaternion.X * xs;
			Single xy = quaternion.X * ys;
			Single xz = quaternion.X * zs;
			Single yy = quaternion.Y * ys;
			Single yz = quaternion.Y * zs;
			Single zz = quaternion.Z * zs;

			result.M11 = one - (yy + zz);
			result.M21 = xy - wz;
			result.M31 = xz + wy;
			result.M41 = zero;
    
			result.M12 = xy + wz;
			result.M22 = one - (xx + zz);
			result.M32 = yz - wx;
			result.M42 = zero;
    
			result.M13 = xz - wy;
			result.M23 = yz + wx;
			result.M33 = one - (xx + yy);
			result.M43 = zero;

			result.M14 = zero;
			result.M24 = zero;
			result.M34 = zero;
			result.M44 = one;
		}



		// todo: remove when we dont need this for the tests
		internal static void CreateFromQuaternionOld (ref Quaternion quaternion, out Matrix44 result)
		{
			Single zero = 0;
			Single one; RealMaths.One(out one);
			Single two = 2;

			Single num9 = quaternion.X * quaternion.X;
			Single num8 = quaternion.Y * quaternion.Y;
			Single num7 = quaternion.Z * quaternion.Z;
			Single num6 = quaternion.X * quaternion.Y;
			Single num5 = quaternion.Z * quaternion.W;
			Single num4 = quaternion.Z * quaternion.X;
			Single num3 = quaternion.Y * quaternion.W;
			Single num2 = quaternion.Y * quaternion.Z;
			Single num = quaternion.X * quaternion.W;
			result.M11 = one - (two * (num8 + num7));
			result.M12 = two * (num6 + num5);
			result.M13 = two * (num4 - num3);
			result.M14 = zero;
			result.M21 = two * (num6 - num5);
			result.M22 = one - (two * (num7 + num9));
			result.M23 = two * (num2 + num);
			result.M24 = zero;
			result.M31 = two * (num4 + num3);
			result.M32 = two * (num2 - num);
			result.M33 = one - (two * (num8 + num9));
			result.M34 = zero;
			result.M41 = zero;
			result.M42 = zero;
			result.M43 = zero;
			result.M44 = one;
		}

		public static void CreateFromYawPitchRoll (Single yaw, Single pitch, Single roll, out Matrix44 result)
		{
			Quaternion quaternion;

			Quaternion.CreateFromYawPitchRoll (yaw, pitch, roll, out quaternion);

			CreateFromQuaternion (ref quaternion, out result);
		}










		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////
		// TODO: REVIEW FROM HERE ONWARDS
		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////


		// FROM XNA
		// --------
		// Creates a cylindrical billboard that rotates around a specified axis.
		// This method computes the facing direction of the billboard from the object position and camera position. 
		// When the object and camera positions are too close, the matrix will not be accurate. 
		// To avoid this problem, the method uses the optional camera forward vector if the positions are too close.
		public static void CreateBillboard (ref Vector3 ObjectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix44 result)
		{
			Single zero = 0;
			Single one; RealMaths.One(out one);

			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			vector.X = ObjectPosition.X - cameraPosition.X;
			vector.Y = ObjectPosition.Y - cameraPosition.Y;
			vector.Z = ObjectPosition.Z - cameraPosition.Z;
			Single num = vector.LengthSquared ();
			Single limit; RealMaths.FromString("0.0001", out limit);

			if (num < limit) {
				vector = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
			} else {
				Vector3.Multiply (ref vector, (Single)(one / (RealMaths.Sqrt (num))), out vector);
			}
			Vector3.Cross (ref cameraUpVector, ref vector, out vector3);
			vector3.Normalise ();
			Vector3.Cross (ref vector, ref vector3, out vector2);
			result.M11 = vector3.X;
			result.M12 = vector3.Y;
			result.M13 = vector3.Z;
			result.M14 = zero;
			result.M21 = vector2.X;
			result.M22 = vector2.Y;
			result.M23 = vector2.Z;
			result.M24 = zero;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = zero;
			result.M41 = ObjectPosition.X;
			result.M42 = ObjectPosition.Y;
			result.M43 = ObjectPosition.Z;
			result.M44 = one;
		}
		
		public static void CreateConstrainedBillboard (ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector, out Matrix44 result)
		{
			Single zero = 0;
			Single one; RealMaths.One(out one);

			Single num;
			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			vector2.X = objectPosition.X - cameraPosition.X;
			vector2.Y = objectPosition.Y - cameraPosition.Y;
			vector2.Z = objectPosition.Z - cameraPosition.Z;
			Single num2 = vector2.LengthSquared ();
			Single limit; RealMaths.FromString("0.0001", out limit);

			if (num2 < limit) {
				vector2 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
			} else {
				Vector3.Multiply (ref vector2, (Single)(one / (RealMaths.Sqrt (num2))), out vector2);
			}
			Vector3 vector4 = rotateAxis;
			Vector3.Dot (ref rotateAxis, ref vector2, out num);

			Single realHorrid; RealMaths.FromString("0.9982547", out realHorrid);

			if (RealMaths.Abs (num) > realHorrid) {
				if (objectForwardVector.HasValue) {
					vector = objectForwardVector.Value;
					Vector3.Dot (ref rotateAxis, ref vector, out num);
					if (RealMaths.Abs (num) > realHorrid) {
						num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
						vector = (RealMaths.Abs (num) > realHorrid) ? Vector3.Right : Vector3.Forward;
					}
				} else {
					num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
					vector = (RealMaths.Abs (num) > realHorrid) ? Vector3.Right : Vector3.Forward;
				}
				Vector3.Cross (ref rotateAxis, ref vector, out vector3);
				vector3.Normalise ();
				Vector3.Cross (ref vector3, ref rotateAxis, out vector);
				vector.Normalise ();
			} else {
				Vector3.Cross (ref rotateAxis, ref vector2, out vector3);
				vector3.Normalise ();
				Vector3.Cross (ref vector3, ref vector4, out vector);
				vector.Normalise ();
			}
			result.M11 = vector3.X;
			result.M12 = vector3.Y;
			result.M13 = vector3.Z;
			result.M14 = zero;
			result.M21 = vector4.X;
			result.M22 = vector4.Y;
			result.M23 = vector4.Z;
			result.M24 = zero;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = zero;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = one;
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
		public static void CreatePerspectiveFieldOfView (Single fieldOfView, Single aspectRatio, Single nearPlaneDistance, Single farPlaneDistance, out Matrix44 result)
		{
			Single zero = 0;
			Single half; RealMaths.Half(out half);
			Single one; RealMaths.One(out one);
			Single pi; RealMaths.Pi(out pi);

			if ((fieldOfView <= zero) || (fieldOfView >= pi)) {
				throw new ArgumentOutOfRangeException ("fieldOfView");
			}
			if (nearPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			if (farPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			Single num = one / (RealMaths.Tan ((fieldOfView * half)));
			Single num9 = num / aspectRatio;
			result.M11 = num9;
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = num;
			result.M21 = result.M23 = result.M24 = zero;
			result.M31 = result.M32 = zero;
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -one;
			result.M41 = result.M42 = result.M44 = zero;
			result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
		public static void CreatePerspective (Single width, Single height, Single nearPlaneDistance, Single farPlaneDistance, out Matrix44 result)
		{
			Single zero = 0;
			Single one; RealMaths.One(out one);
			Single two = 2;

			if (nearPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			if (farPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			result.M11 = (two * nearPlaneDistance) / width;
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = (two * nearPlaneDistance) / height;
			result.M21 = result.M23 = result.M24 = zero;
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M31 = result.M32 = zero;
			result.M34 = -one;
			result.M41 = result.M42 = result.M44 = zero;
			result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
		}


		// ref: http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx
		public static void CreatePerspectiveOffCenter (Single left, Single right, Single bottom, Single top, Single nearPlaneDistance, Single farPlaneDistance, out Matrix44 result)
		{
			Single zero = 0;
			Single one; RealMaths.One(out one);
			Single two = 2;

			if (nearPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			if (farPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			result.M11 = (two * nearPlaneDistance) / (right - left);
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = (two * nearPlaneDistance) / (top - bottom);
			result.M21 = result.M23 = result.M24 = zero;
			result.M31 = (left + right) / (right - left);
			result.M32 = (top + bottom) / (top - bottom);
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -one;
			result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
			result.M41 = result.M42 = result.M44 = zero;
		}
		
		// ref: http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
		public static void CreateOrthographic (Single width, Single height, Single zNearPlane, Single zFarPlane, out Matrix44 result)
		{
			Single zero = 0;
			Single one; RealMaths.One(out one);
			Single two = 2;

			result.M11 = two / width;
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = two / height;
			result.M21 = result.M23 = result.M24 = zero;
			result.M33 = one / (zNearPlane - zFarPlane);
			result.M31 = result.M32 = result.M34 = zero;
			result.M41 = result.M42 = zero;
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = one;
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx
		public static void CreateOrthographicOffCenter (Single left, Single right, Single bottom, Single top, Single zNearPlane, Single zFarPlane, out Matrix44 result)
		{
			Single zero = 0;
			Single one; RealMaths.One(out one);
			Single two = 2;

			result.M11 = two / (right - left);
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = two / (top - bottom);
			result.M21 = result.M23 = result.M24 = zero;
			result.M33 = one / (zNearPlane - zFarPlane);
			result.M31 = result.M32 = result.M34 = zero;
			result.M41 = (left + right) / (left - right);
			result.M42 = (top + bottom) / (bottom - top);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = one;
		}
		
		// ref: http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx
		public static void CreateLookAt (ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix44 result)
		{
			Single zero = 0;
			Single one; RealMaths.One(out one);

			Vector3 targetToPosition = cameraPosition - cameraTarget;

			Vector3 vector; Vector3.Normalise (ref targetToPosition, out vector);

			Vector3 cross; Vector3.Cross (ref cameraUpVector, ref vector, out cross); 

			Vector3 vector2; Vector3.Normalise (ref cross, out vector2);
			Vector3 vector3; Vector3.Cross (ref vector, ref vector2, out vector3);
			result.M11 = vector2.X;
			result.M12 = vector3.X;
			result.M13 = vector.X;
			result.M14 = zero;
			result.M21 = vector2.Y;
			result.M22 = vector3.Y;
			result.M23 = vector.Y;
			result.M24 = zero;
			result.M31 = vector2.Z;
			result.M32 = vector3.Z;
			result.M33 = vector.Z;
			result.M34 = zero;

			Vector3.Dot (ref vector2, ref cameraPosition, out result.M41);
			Vector3.Dot (ref vector3, ref cameraPosition, out result.M42);
			Vector3.Dot (ref vector, ref cameraPosition, out result.M43);
			
			result.M41 *= -one;
			result.M42 *= -one;
			result.M43 *= -one;

			result.M44 = one;
		}

		
	

		// ref: http://msdn.microsoft.com/en-us/library/bb205364(v=VS.85).aspx
		public static void CreateShadow (ref Vector3 lightDirection, ref Plane plane, out Matrix44 result)
		{
			Single zero = 0;
			
			Plane plane2;
			Plane.Normalise (ref plane, out plane2);
			Single num = ((plane2.Normal.X * lightDirection.X) + (plane2.Normal.Y * lightDirection.Y)) + (plane2.Normal.Z * lightDirection.Z);
			Single num5 = -plane2.Normal.X;
			Single num4 = -plane2.Normal.Y;
			Single num3 = -plane2.Normal.Z;
			Single num2 = -plane2.D;
			result.M11 = (num5 * lightDirection.X) + num;
			result.M21 = num4 * lightDirection.X;
			result.M31 = num3 * lightDirection.X;
			result.M41 = num2 * lightDirection.X;
			result.M12 = num5 * lightDirection.Y;
			result.M22 = (num4 * lightDirection.Y) + num;
			result.M32 = num3 * lightDirection.Y;
			result.M42 = num2 * lightDirection.Y;
			result.M13 = num5 * lightDirection.Z;
			result.M23 = num4 * lightDirection.Z;
			result.M33 = (num3 * lightDirection.Z) + num;
			result.M43 = num2 * lightDirection.Z;
			result.M14 = zero;
			result.M24 = zero;
			result.M34 = zero;
			result.M44 = num;
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205356(v=VS.85).aspx
		public static void CreateReflection (ref Plane value, out Matrix44 result)
		{
			Single zero = 0;
			Single one; RealMaths.One(out one);
			Single two = 2;

			Plane plane;
			
			Plane.Normalise (ref value, out plane);
			
			value.Normalise ();
			
			Single x = plane.Normal.X;
			Single y = plane.Normal.Y;
			Single z = plane.Normal.Z;
			
			Single num3 = -two * x;
			Single num2 = -two * y;
			Single num = -two * z;
			
			result.M11 = (num3 * x) + one;
			result.M12 = num2 * x;
			result.M13 = num * x;
			result.M14 = zero;
			result.M21 = num3 * y;
			result.M22 = (num2 * y) + one;
			result.M23 = num * y;
			result.M24 = zero;
			result.M31 = num3 * z;
			result.M32 = num2 * z;
			result.M33 = (num * z) + one;
			result.M34 = zero;
			result.M41 = num3 * plane.D;
			result.M42 = num2 * plane.D;
			result.M43 = num * plane.D;
			result.M44 = one;
		}
		
		#endregion
		#region Maths

		//----------------------------------------------------------------------
		// Transpose
		//
		public void Transpose()
		{
			Single temp = this.M12;
			this.M12 = this.M21;
			this.M21 = temp;

			temp = this.M13;
			this.M13 = this.M31;
			this.M31 = temp;

			temp = this.M14;
			this.M14 = this.M41;
			this.M41 = temp;

			temp = this.M23;
			this.M23 = this.M32;
			this.M32 = temp;

			temp = this.M24;
			this.M24 = this.M42;
			this.M42 = temp;

			temp =  this.M34;
			this.M34 = this.M43;
			this.M43 = temp;
		}

		public static void Transpose (ref Matrix44 input, out Matrix44 output)
		{
		    output.M11 = input.M11;
			output.M12 = input.M21;
			output.M13 = input.M31;
			output.M14 = input.M41;
			output.M21 = input.M12;
			output.M22 = input.M22;
			output.M23 = input.M32;
			output.M24 = input.M42;
			output.M31 = input.M13;
			output.M32 = input.M23;
			output.M33 = input.M33;
			output.M34 = input.M43;
			output.M41 = input.M14;
			output.M42 = input.M24;
			output.M43 = input.M34;
			output.M44 = input.M44;
		}

		//----------------------------------------------------------------------
		// Decompose
		// ref: Essential Mathemathics For Games & Interactive Applications
		public bool Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
		{
			translation.X = M41;
            translation.Y = M42;
            translation.Z = M43;

			Vector3 a = new Vector3(M11, M21, M31);
			Vector3 b = new Vector3(M12, M22, M32);
			Vector3 c = new Vector3(M13, M23, M33);

			scale.X = a.Length();
			scale.Y = b.Length();
			scale.Z = c.Length();

			if ( RealMaths.IsZero(scale.X) || 
				 RealMaths.IsZero(scale.Y) || 
				 RealMaths.IsZero(scale.Z) )
            {
				rotation = Quaternion.Identity;
				return false;
			}

			a.Normalise();
			b.Normalise();
			c.Normalise();

			Vector3 right = new Vector3(a.X, b.X, c.X);
			Vector3 up = new Vector3(a.Y, b.Y, c.Y);
			Vector3 backward = new Vector3(a.Z, b.Z, c.Z);

			right.Normalise();
			up.Normalise();
			backward.Normalise();

			Matrix44 rotMat;
			Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out rotMat);

			Quaternion.CreateFromRotationMatrix(ref rotMat, out rotation);

			return true;
		}




		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////
		// TODO: REVIEW FROM HERE ONWARDS
		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////


		//----------------------------------------------------------------------
		// Determinant
		//
		public Single Determinant ()
		{
			Single num22 = this.M11;
			Single num21 = this.M12;
			Single num20 = this.M13;
			Single num19 = this.M14;
			Single num12 = this.M21;
			Single num11 = this.M22;
			Single num10 = this.M23;
			Single num9 = this.M24;
			Single num8 = this.M31;
			Single num7 = this.M32;
			Single num6 = this.M33;
			Single num5 = this.M34;
			Single num4 = this.M41;
			Single num3 = this.M42;
			Single num2 = this.M43;
			Single num = this.M44;
			
			Single num18 = (num6 * num) - (num5 * num2);
			Single num17 = (num7 * num) - (num5 * num3);
			Single num16 = (num7 * num2) - (num6 * num3);
			Single num15 = (num8 * num) - (num5 * num4);
			Single num14 = (num8 * num2) - (num6 * num4);
			Single num13 = (num8 * num3) - (num7 * num4);
			
			return ((((num22 * (((num11 * num18) - (num10 * num17)) + (num9 * num16))) - (num21 * (((num12 * num18) - (num10 * num15)) + (num9 * num14)))) + (num20 * (((num12 * num17) - (num11 * num15)) + (num9 * num13)))) - (num19 * (((num12 * num16) - (num11 * num14)) + (num10 * num13))));
		}
		
		//----------------------------------------------------------------------
		// Invert
		//
		public static void Invert (ref Matrix44 matrix, out Matrix44 result)
		{
			Single one = 1;
			Single num5 = matrix.M11;
			Single num4 = matrix.M12;
			Single num3 = matrix.M13;
			Single num2 = matrix.M14;
			Single num9 = matrix.M21;
			Single num8 = matrix.M22;
			Single num7 = matrix.M23;
			Single num6 = matrix.M24;
			Single num17 = matrix.M31;
			Single num16 = matrix.M32;
			Single num15 = matrix.M33;
			Single num14 = matrix.M34;
			Single num13 = matrix.M41;
			Single num12 = matrix.M42;
			Single num11 = matrix.M43;
			Single num10 = matrix.M44;
			Single num23 = (num15 * num10) - (num14 * num11);
			Single num22 = (num16 * num10) - (num14 * num12);
			Single num21 = (num16 * num11) - (num15 * num12);
			Single num20 = (num17 * num10) - (num14 * num13);
			Single num19 = (num17 * num11) - (num15 * num13);
			Single num18 = (num17 * num12) - (num16 * num13);
			Single num39 = ((num8 * num23) - (num7 * num22)) + (num6 * num21);
			Single num38 = -(((num9 * num23) - (num7 * num20)) + (num6 * num19));
			Single num37 = ((num9 * num22) - (num8 * num20)) + (num6 * num18);
			Single num36 = -(((num9 * num21) - (num8 * num19)) + (num7 * num18));
			Single num = one / ((((num5 * num39) + (num4 * num38)) + (num3 * num37)) + (num2 * num36));
			result.M11 = num39 * num;
			result.M21 = num38 * num;
			result.M31 = num37 * num;
			result.M41 = num36 * num;
			result.M12 = -(((num4 * num23) - (num3 * num22)) + (num2 * num21)) * num;
			result.M22 = (((num5 * num23) - (num3 * num20)) + (num2 * num19)) * num;
			result.M32 = -(((num5 * num22) - (num4 * num20)) + (num2 * num18)) * num;
			result.M42 = (((num5 * num21) - (num4 * num19)) + (num3 * num18)) * num;
			Single num35 = (num7 * num10) - (num6 * num11);
			Single num34 = (num8 * num10) - (num6 * num12);
			Single num33 = (num8 * num11) - (num7 * num12);
			Single num32 = (num9 * num10) - (num6 * num13);
			Single num31 = (num9 * num11) - (num7 * num13);
			Single num30 = (num9 * num12) - (num8 * num13);
			result.M13 = (((num4 * num35) - (num3 * num34)) + (num2 * num33)) * num;
			result.M23 = -(((num5 * num35) - (num3 * num32)) + (num2 * num31)) * num;
			result.M33 = (((num5 * num34) - (num4 * num32)) + (num2 * num30)) * num;
			result.M43 = -(((num5 * num33) - (num4 * num31)) + (num3 * num30)) * num;
			Single num29 = (num7 * num14) - (num6 * num15);
			Single num28 = (num8 * num14) - (num6 * num16);
			Single num27 = (num8 * num15) - (num7 * num16);
			Single num26 = (num9 * num14) - (num6 * num17);
			Single num25 = (num9 * num15) - (num7 * num17);
			Single num24 = (num9 * num16) - (num8 * num17);
			result.M14 = -(((num4 * num29) - (num3 * num28)) + (num2 * num27)) * num;
			result.M24 = (((num5 * num29) - (num3 * num26)) + (num2 * num25)) * num;
			result.M34 = -(((num5 * num28) - (num4 * num26)) + (num2 * num24)) * num;
			result.M44 = (((num5 * num27) - (num4 * num25)) + (num3 * num24)) * num;
		}


		//----------------------------------------------------------------------
		// Transform - Transforms a Matrix by applying a Quaternion rotation.
		//
		public static void Transform (ref Matrix44 value, ref Quaternion rotation, out Matrix44 result)
		{
			Single one = 1;

			Single num21 = rotation.X + rotation.X;
			Single num11 = rotation.Y + rotation.Y;
			Single num10 = rotation.Z + rotation.Z;
			
			Single num20 = rotation.W * num21;
			Single num19 = rotation.W * num11;
			Single num18 = rotation.W * num10;
			Single num17 = rotation.X * num21;
			Single num16 = rotation.X * num11;
			Single num15 = rotation.X * num10;
			Single num14 = rotation.Y * num11;
			Single num13 = rotation.Y * num10;
			Single num12 = rotation.Z * num10;
			
			Single num9 = (one - num14) - num12;
			
			Single num8 = num16 - num18;
			Single num7 = num15 + num19;
			Single num6 = num16 + num18;
			
			Single num5 = (one - num17) - num12;
			
			Single num4 = num13 - num20;
			Single num3 = num15 - num19;
			Single num2 = num13 + num20;
			
			Single num = (one - num17) - num14;
			
			Single num37 = ((value.M11 * num9) + (value.M12 * num8)) + (value.M13 * num7);
			Single num36 = ((value.M11 * num6) + (value.M12 * num5)) + (value.M13 * num4);
			Single num35 = ((value.M11 * num3) + (value.M12 * num2)) + (value.M13 * num);
			
			Single num34 = value.M14;
			
			Single num33 = ((value.M21 * num9) + (value.M22 * num8)) + (value.M23 * num7);
			Single num32 = ((value.M21 * num6) + (value.M22 * num5)) + (value.M23 * num4);
			Single num31 = ((value.M21 * num3) + (value.M22 * num2)) + (value.M23 * num);
			
			Single num30 = value.M24;
			
			Single num29 = ((value.M31 * num9) + (value.M32 * num8)) + (value.M33 * num7);
			Single num28 = ((value.M31 * num6) + (value.M32 * num5)) + (value.M33 * num4);
			Single num27 = ((value.M31 * num3) + (value.M32 * num2)) + (value.M33 * num);
			
			Single num26 = value.M34;
			
			Single num25 = ((value.M41 * num9) + (value.M42 * num8)) + (value.M43 * num7);
			Single num24 = ((value.M41 * num6) + (value.M42 * num5)) + (value.M43 * num4);
			Single num23 = ((value.M41 * num3) + (value.M42 * num2)) + (value.M43 * num);
			
			Single num22 = value.M44;
			
			result.M11 = num37;
			result.M12 = num36;
			result.M13 = num35;
			result.M14 = num34;
			result.M21 = num33;
			result.M22 = num32;
			result.M23 = num31;
			result.M24 = num30;
			result.M31 = num29;
			result.M32 = num28;
			result.M33 = num27;
			result.M34 = num26;
			result.M41 = num25;
			result.M42 = num24;
			result.M43 = num23;
			result.M44 = num22;
		}
		
		#endregion
		#region Operators
		
		public static Matrix44 operator - (Matrix44 matrix1)
		{
			Matrix44 matrix;
			matrix.M11 = -matrix1.M11;
			matrix.M12 = -matrix1.M12;
			matrix.M13 = -matrix1.M13;
			matrix.M14 = -matrix1.M14;
			matrix.M21 = -matrix1.M21;
			matrix.M22 = -matrix1.M22;
			matrix.M23 = -matrix1.M23;
			matrix.M24 = -matrix1.M24;
			matrix.M31 = -matrix1.M31;
			matrix.M32 = -matrix1.M32;
			matrix.M33 = -matrix1.M33;
			matrix.M34 = -matrix1.M34;
			matrix.M41 = -matrix1.M41;
			matrix.M42 = -matrix1.M42;
			matrix.M43 = -matrix1.M43;
			matrix.M44 = -matrix1.M44;
			return matrix;
		}
		
		public static Boolean operator == (Matrix44 matrix1, Matrix44 matrix2)
		{
			return ((((((matrix1.M11 == matrix2.M11) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M44 == matrix2.M44))) && (((matrix1.M12 == matrix2.M12) && (matrix1.M13 == matrix2.M13)) && ((matrix1.M14 == matrix2.M14) && (matrix1.M21 == matrix2.M21)))) && ((((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)) && ((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32))) && (((matrix1.M34 == matrix2.M34) && (matrix1.M41 == matrix2.M41)) && (matrix1.M42 == matrix2.M42)))) && (matrix1.M43 == matrix2.M43));
		}
		
		public static Boolean operator != (Matrix44 matrix1, Matrix44 matrix2)
		{
			if (((((matrix1.M11 == matrix2.M11) && (matrix1.M12 == matrix2.M12)) && ((matrix1.M13 == matrix2.M13) && (matrix1.M14 == matrix2.M14))) && (((matrix1.M21 == matrix2.M21) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)))) && ((((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M34 == matrix2.M34))) && (((matrix1.M41 == matrix2.M41) && (matrix1.M42 == matrix2.M42)) && (matrix1.M43 == matrix2.M43)))) {
				return !(matrix1.M44 == matrix2.M44);
			}
			return true;
		}
		
		public static Matrix44 operator + (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = matrix1.M11 + matrix2.M11;
			matrix.M12 = matrix1.M12 + matrix2.M12;
			matrix.M13 = matrix1.M13 + matrix2.M13;
			matrix.M14 = matrix1.M14 + matrix2.M14;
			matrix.M21 = matrix1.M21 + matrix2.M21;
			matrix.M22 = matrix1.M22 + matrix2.M22;
			matrix.M23 = matrix1.M23 + matrix2.M23;
			matrix.M24 = matrix1.M24 + matrix2.M24;
			matrix.M31 = matrix1.M31 + matrix2.M31;
			matrix.M32 = matrix1.M32 + matrix2.M32;
			matrix.M33 = matrix1.M33 + matrix2.M33;
			matrix.M34 = matrix1.M34 + matrix2.M34;
			matrix.M41 = matrix1.M41 + matrix2.M41;
			matrix.M42 = matrix1.M42 + matrix2.M42;
			matrix.M43 = matrix1.M43 + matrix2.M43;
			matrix.M44 = matrix1.M44 + matrix2.M44;
			return matrix;
		}
		
		public static Matrix44 operator - (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = matrix1.M11 - matrix2.M11;
			matrix.M12 = matrix1.M12 - matrix2.M12;
			matrix.M13 = matrix1.M13 - matrix2.M13;
			matrix.M14 = matrix1.M14 - matrix2.M14;
			matrix.M21 = matrix1.M21 - matrix2.M21;
			matrix.M22 = matrix1.M22 - matrix2.M22;
			matrix.M23 = matrix1.M23 - matrix2.M23;
			matrix.M24 = matrix1.M24 - matrix2.M24;
			matrix.M31 = matrix1.M31 - matrix2.M31;
			matrix.M32 = matrix1.M32 - matrix2.M32;
			matrix.M33 = matrix1.M33 - matrix2.M33;
			matrix.M34 = matrix1.M34 - matrix2.M34;
			matrix.M41 = matrix1.M41 - matrix2.M41;
			matrix.M42 = matrix1.M42 - matrix2.M42;
			matrix.M43 = matrix1.M43 - matrix2.M43;
			matrix.M44 = matrix1.M44 - matrix2.M44;
			return matrix;
		}
		
		public static Matrix44 operator * (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = (((matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21)) + (matrix1.M13 * matrix2.M31)) + (matrix1.M14 * matrix2.M41);
			matrix.M12 = (((matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22)) + (matrix1.M13 * matrix2.M32)) + (matrix1.M14 * matrix2.M42);
			matrix.M13 = (((matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23)) + (matrix1.M13 * matrix2.M33)) + (matrix1.M14 * matrix2.M43);
			matrix.M14 = (((matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24)) + (matrix1.M13 * matrix2.M34)) + (matrix1.M14 * matrix2.M44);
			matrix.M21 = (((matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21)) + (matrix1.M23 * matrix2.M31)) + (matrix1.M24 * matrix2.M41);
			matrix.M22 = (((matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22)) + (matrix1.M23 * matrix2.M32)) + (matrix1.M24 * matrix2.M42);
			matrix.M23 = (((matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23)) + (matrix1.M23 * matrix2.M33)) + (matrix1.M24 * matrix2.M43);
			matrix.M24 = (((matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24)) + (matrix1.M23 * matrix2.M34)) + (matrix1.M24 * matrix2.M44);
			matrix.M31 = (((matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21)) + (matrix1.M33 * matrix2.M31)) + (matrix1.M34 * matrix2.M41);
			matrix.M32 = (((matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22)) + (matrix1.M33 * matrix2.M32)) + (matrix1.M34 * matrix2.M42);
			matrix.M33 = (((matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23)) + (matrix1.M33 * matrix2.M33)) + (matrix1.M34 * matrix2.M43);
			matrix.M34 = (((matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24)) + (matrix1.M33 * matrix2.M34)) + (matrix1.M34 * matrix2.M44);
			matrix.M41 = (((matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21)) + (matrix1.M43 * matrix2.M31)) + (matrix1.M44 * matrix2.M41);
			matrix.M42 = (((matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22)) + (matrix1.M43 * matrix2.M32)) + (matrix1.M44 * matrix2.M42);
			matrix.M43 = (((matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23)) + (matrix1.M43 * matrix2.M33)) + (matrix1.M44 * matrix2.M43);
			matrix.M44 = (((matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24)) + (matrix1.M43 * matrix2.M34)) + (matrix1.M44 * matrix2.M44);
			return matrix;
		}
		
		public static Matrix44 operator * (Matrix44 matrix, Single scaleFactor)
		{
			Matrix44 matrix2;
			Single num = scaleFactor;
			matrix2.M11 = matrix.M11 * num;
			matrix2.M12 = matrix.M12 * num;
			matrix2.M13 = matrix.M13 * num;
			matrix2.M14 = matrix.M14 * num;
			matrix2.M21 = matrix.M21 * num;
			matrix2.M22 = matrix.M22 * num;
			matrix2.M23 = matrix.M23 * num;
			matrix2.M24 = matrix.M24 * num;
			matrix2.M31 = matrix.M31 * num;
			matrix2.M32 = matrix.M32 * num;
			matrix2.M33 = matrix.M33 * num;
			matrix2.M34 = matrix.M34 * num;
			matrix2.M41 = matrix.M41 * num;
			matrix2.M42 = matrix.M42 * num;
			matrix2.M43 = matrix.M43 * num;
			matrix2.M44 = matrix.M44 * num;
			return matrix2;
		}
		
		public static Matrix44 operator * (Single scaleFactor, Matrix44 matrix)
		{
			Matrix44 matrix2;
			Single num = scaleFactor;
			matrix2.M11 = matrix.M11 * num;
			matrix2.M12 = matrix.M12 * num;
			matrix2.M13 = matrix.M13 * num;
			matrix2.M14 = matrix.M14 * num;
			matrix2.M21 = matrix.M21 * num;
			matrix2.M22 = matrix.M22 * num;
			matrix2.M23 = matrix.M23 * num;
			matrix2.M24 = matrix.M24 * num;
			matrix2.M31 = matrix.M31 * num;
			matrix2.M32 = matrix.M32 * num;
			matrix2.M33 = matrix.M33 * num;
			matrix2.M34 = matrix.M34 * num;
			matrix2.M41 = matrix.M41 * num;
			matrix2.M42 = matrix.M42 * num;
			matrix2.M43 = matrix.M43 * num;
			matrix2.M44 = matrix.M44 * num;
			return matrix2;
		}
		
		public static Matrix44 operator / (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = matrix1.M11 / matrix2.M11;
			matrix.M12 = matrix1.M12 / matrix2.M12;
			matrix.M13 = matrix1.M13 / matrix2.M13;
			matrix.M14 = matrix1.M14 / matrix2.M14;
			matrix.M21 = matrix1.M21 / matrix2.M21;
			matrix.M22 = matrix1.M22 / matrix2.M22;
			matrix.M23 = matrix1.M23 / matrix2.M23;
			matrix.M24 = matrix1.M24 / matrix2.M24;
			matrix.M31 = matrix1.M31 / matrix2.M31;
			matrix.M32 = matrix1.M32 / matrix2.M32;
			matrix.M33 = matrix1.M33 / matrix2.M33;
			matrix.M34 = matrix1.M34 / matrix2.M34;
			matrix.M41 = matrix1.M41 / matrix2.M41;
			matrix.M42 = matrix1.M42 / matrix2.M42;
			matrix.M43 = matrix1.M43 / matrix2.M43;
			matrix.M44 = matrix1.M44 / matrix2.M44;
			return matrix;
		}
		
		public static Matrix44 operator / (Matrix44 matrix1, Single divider)
		{
			Matrix44 matrix;
			Single one = 1;
			Single num = one / divider;
			matrix.M11 = matrix1.M11 * num;
			matrix.M12 = matrix1.M12 * num;
			matrix.M13 = matrix1.M13 * num;
			matrix.M14 = matrix1.M14 * num;
			matrix.M21 = matrix1.M21 * num;
			matrix.M22 = matrix1.M22 * num;
			matrix.M23 = matrix1.M23 * num;
			matrix.M24 = matrix1.M24 * num;
			matrix.M31 = matrix1.M31 * num;
			matrix.M32 = matrix1.M32 * num;
			matrix.M33 = matrix1.M33 * num;
			matrix.M34 = matrix1.M34 * num;
			matrix.M41 = matrix1.M41 * num;
			matrix.M42 = matrix1.M42 * num;
			matrix.M43 = matrix1.M43 * num;
			matrix.M44 = matrix1.M44 * num;
			return matrix;
		}
		
		public static void Negate (ref Matrix44 matrix, out Matrix44 result)
		{
			result.M11 = -matrix.M11;
			result.M12 = -matrix.M12;
			result.M13 = -matrix.M13;
			result.M14 = -matrix.M14;
			result.M21 = -matrix.M21;
			result.M22 = -matrix.M22;
			result.M23 = -matrix.M23;
			result.M24 = -matrix.M24;
			result.M31 = -matrix.M31;
			result.M32 = -matrix.M32;
			result.M33 = -matrix.M33;
			result.M34 = -matrix.M34;
			result.M41 = -matrix.M41;
			result.M42 = -matrix.M42;
			result.M43 = -matrix.M43;
			result.M44 = -matrix.M44;
		}
		
		public static void Add (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			result.M11 = matrix1.M11 + matrix2.M11;
			result.M12 = matrix1.M12 + matrix2.M12;
			result.M13 = matrix1.M13 + matrix2.M13;
			result.M14 = matrix1.M14 + matrix2.M14;
			result.M21 = matrix1.M21 + matrix2.M21;
			result.M22 = matrix1.M22 + matrix2.M22;
			result.M23 = matrix1.M23 + matrix2.M23;
			result.M24 = matrix1.M24 + matrix2.M24;
			result.M31 = matrix1.M31 + matrix2.M31;
			result.M32 = matrix1.M32 + matrix2.M32;
			result.M33 = matrix1.M33 + matrix2.M33;
			result.M34 = matrix1.M34 + matrix2.M34;
			result.M41 = matrix1.M41 + matrix2.M41;
			result.M42 = matrix1.M42 + matrix2.M42;
			result.M43 = matrix1.M43 + matrix2.M43;
			result.M44 = matrix1.M44 + matrix2.M44;
		}
		
		public static void Subtract (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			result.M11 = matrix1.M11 - matrix2.M11;
			result.M12 = matrix1.M12 - matrix2.M12;
			result.M13 = matrix1.M13 - matrix2.M13;
			result.M14 = matrix1.M14 - matrix2.M14;
			result.M21 = matrix1.M21 - matrix2.M21;
			result.M22 = matrix1.M22 - matrix2.M22;
			result.M23 = matrix1.M23 - matrix2.M23;
			result.M24 = matrix1.M24 - matrix2.M24;
			result.M31 = matrix1.M31 - matrix2.M31;
			result.M32 = matrix1.M32 - matrix2.M32;
			result.M33 = matrix1.M33 - matrix2.M33;
			result.M34 = matrix1.M34 - matrix2.M34;
			result.M41 = matrix1.M41 - matrix2.M41;
			result.M42 = matrix1.M42 - matrix2.M42;
			result.M43 = matrix1.M43 - matrix2.M43;
			result.M44 = matrix1.M44 - matrix2.M44;
		}
		
		public static void Multiply (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			Single num16 = (((matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21)) + (matrix1.M13 * matrix2.M31)) + (matrix1.M14 * matrix2.M41);
			Single num15 = (((matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22)) + (matrix1.M13 * matrix2.M32)) + (matrix1.M14 * matrix2.M42);
			Single num14 = (((matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23)) + (matrix1.M13 * matrix2.M33)) + (matrix1.M14 * matrix2.M43);
			Single num13 = (((matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24)) + (matrix1.M13 * matrix2.M34)) + (matrix1.M14 * matrix2.M44);
			Single num12 = (((matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21)) + (matrix1.M23 * matrix2.M31)) + (matrix1.M24 * matrix2.M41);
			Single num11 = (((matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22)) + (matrix1.M23 * matrix2.M32)) + (matrix1.M24 * matrix2.M42);
			Single num10 = (((matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23)) + (matrix1.M23 * matrix2.M33)) + (matrix1.M24 * matrix2.M43);
			Single num9 = (((matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24)) + (matrix1.M23 * matrix2.M34)) + (matrix1.M24 * matrix2.M44);
			Single num8 = (((matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21)) + (matrix1.M33 * matrix2.M31)) + (matrix1.M34 * matrix2.M41);
			Single num7 = (((matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22)) + (matrix1.M33 * matrix2.M32)) + (matrix1.M34 * matrix2.M42);
			Single num6 = (((matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23)) + (matrix1.M33 * matrix2.M33)) + (matrix1.M34 * matrix2.M43);
			Single num5 = (((matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24)) + (matrix1.M33 * matrix2.M34)) + (matrix1.M34 * matrix2.M44);
			Single num4 = (((matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21)) + (matrix1.M43 * matrix2.M31)) + (matrix1.M44 * matrix2.M41);
			Single num3 = (((matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22)) + (matrix1.M43 * matrix2.M32)) + (matrix1.M44 * matrix2.M42);
			Single num2 = (((matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23)) + (matrix1.M43 * matrix2.M33)) + (matrix1.M44 * matrix2.M43);
			Single num = (((matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24)) + (matrix1.M43 * matrix2.M34)) + (matrix1.M44 * matrix2.M44);
			result.M11 = num16;
			result.M12 = num15;
			result.M13 = num14;
			result.M14 = num13;
			result.M21 = num12;
			result.M22 = num11;
			result.M23 = num10;
			result.M24 = num9;
			result.M31 = num8;
			result.M32 = num7;
			result.M33 = num6;
			result.M34 = num5;
			result.M41 = num4;
			result.M42 = num3;
			result.M43 = num2;
			result.M44 = num;
		}

		public static void Multiply (ref Matrix44 matrix1, Single scaleFactor, out Matrix44 result)
		{
			Single num = scaleFactor;
			result.M11 = matrix1.M11 * num;
			result.M12 = matrix1.M12 * num;
			result.M13 = matrix1.M13 * num;
			result.M14 = matrix1.M14 * num;
			result.M21 = matrix1.M21 * num;
			result.M22 = matrix1.M22 * num;
			result.M23 = matrix1.M23 * num;
			result.M24 = matrix1.M24 * num;
			result.M31 = matrix1.M31 * num;
			result.M32 = matrix1.M32 * num;
			result.M33 = matrix1.M33 * num;
			result.M34 = matrix1.M34 * num;
			result.M41 = matrix1.M41 * num;
			result.M42 = matrix1.M42 * num;
			result.M43 = matrix1.M43 * num;
			result.M44 = matrix1.M44 * num;
		}

		public static void Divide (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			result.M11 = matrix1.M11 / matrix2.M11;
			result.M12 = matrix1.M12 / matrix2.M12;
			result.M13 = matrix1.M13 / matrix2.M13;
			result.M14 = matrix1.M14 / matrix2.M14;
			result.M21 = matrix1.M21 / matrix2.M21;
			result.M22 = matrix1.M22 / matrix2.M22;
			result.M23 = matrix1.M23 / matrix2.M23;
			result.M24 = matrix1.M24 / matrix2.M24;
			result.M31 = matrix1.M31 / matrix2.M31;
			result.M32 = matrix1.M32 / matrix2.M32;
			result.M33 = matrix1.M33 / matrix2.M33;
			result.M34 = matrix1.M34 / matrix2.M34;
			result.M41 = matrix1.M41 / matrix2.M41;
			result.M42 = matrix1.M42 / matrix2.M42;
			result.M43 = matrix1.M43 / matrix2.M43;
			result.M44 = matrix1.M44 / matrix2.M44;
		}
		
		public static void Divide (ref Matrix44 matrix1, Single divider, out Matrix44 result)
		{
			Single one = 1;

			Single num = one / divider;
			result.M11 = matrix1.M11 * num;
			result.M12 = matrix1.M12 * num;
			result.M13 = matrix1.M13 * num;
			result.M14 = matrix1.M14 * num;
			result.M21 = matrix1.M21 * num;
			result.M22 = matrix1.M22 * num;
			result.M23 = matrix1.M23 * num;
			result.M24 = matrix1.M24 * num;
			result.M31 = matrix1.M31 * num;
			result.M32 = matrix1.M32 * num;
			result.M33 = matrix1.M33 * num;
			result.M34 = matrix1.M34 * num;
			result.M41 = matrix1.M41 * num;
			result.M42 = matrix1.M42 * num;
			result.M43 = matrix1.M43 * num;
			result.M44 = matrix1.M44 * num;
		}

		#endregion
		#region Utilities

		// beware, doing this might not produce what you expect.  you likely
		// want to lerp between quaternions.
		public static void Lerp (ref Matrix44 matrix1, ref Matrix44 matrix2, Single amount, out Matrix44 result)
		{
			result.M11 = matrix1.M11 + ((matrix2.M11 - matrix1.M11) * amount);
			result.M12 = matrix1.M12 + ((matrix2.M12 - matrix1.M12) * amount);
			result.M13 = matrix1.M13 + ((matrix2.M13 - matrix1.M13) * amount);
			result.M14 = matrix1.M14 + ((matrix2.M14 - matrix1.M14) * amount);
			result.M21 = matrix1.M21 + ((matrix2.M21 - matrix1.M21) * amount);
			result.M22 = matrix1.M22 + ((matrix2.M22 - matrix1.M22) * amount);
			result.M23 = matrix1.M23 + ((matrix2.M23 - matrix1.M23) * amount);
			result.M24 = matrix1.M24 + ((matrix2.M24 - matrix1.M24) * amount);
			result.M31 = matrix1.M31 + ((matrix2.M31 - matrix1.M31) * amount);
			result.M32 = matrix1.M32 + ((matrix2.M32 - matrix1.M32) * amount);
			result.M33 = matrix1.M33 + ((matrix2.M33 - matrix1.M33) * amount);
			result.M34 = matrix1.M34 + ((matrix2.M34 - matrix1.M34) * amount);
			result.M41 = matrix1.M41 + ((matrix2.M41 - matrix1.M41) * amount);
			result.M42 = matrix1.M42 + ((matrix2.M42 - matrix1.M42) * amount);
			result.M43 = matrix1.M43 + ((matrix2.M43 - matrix1.M43) * amount);
			result.M44 = matrix1.M44 + ((matrix2.M44 - matrix1.M44) * amount);
		}
		
		#endregion
		
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Plane 
		: IEquatable<Plane>
	{
		public Vector3 Normal;
		public Single D;

		public Plane (Single a, Single b, Single c, Single d)
		{
			this.Normal.X = a;
			this.Normal.Y = b;
			this.Normal.Z = c;
			this.D = d;
		}

		public Plane (Vector3 normal, Single d)
		{
			this.Normal = normal;
			this.D = d;
		}

		public Plane (Vector4 value)
		{
			this.Normal.X = value.X;
			this.Normal.Y = value.Y;
			this.Normal.Z = value.Z;
			this.D = value.W;
		}

		public Plane (Vector3 point1, Vector3 point2, Vector3 point3)
		{
			Single one = 1;

			Single num10 = point2.X - point1.X;
			Single num9 = point2.Y - point1.Y;
			Single num8 = point2.Z - point1.Z;
			Single num7 = point3.X - point1.X;
			Single num6 = point3.Y - point1.Y;
			Single num5 = point3.Z - point1.Z;
			Single num4 = (num9 * num5) - (num8 * num6);
			Single num3 = (num8 * num7) - (num10 * num5);
			Single num2 = (num10 * num6) - (num9 * num7);
			Single num11 = ((num4 * num4) + (num3 * num3)) + (num2 * num2);
			Single num = one / RealMaths.Sqrt (num11);
			this.Normal.X = num4 * num;
			this.Normal.Y = num3 * num;
			this.Normal.Z = num2 * num;
			this.D = -(((this.Normal.X * point1.X) + (this.Normal.Y * point1.Y)) + (this.Normal.Z * point1.Z));
		}

		public Boolean Equals (Plane other)
		{
			return ((((this.Normal.X == other.Normal.X) && (this.Normal.Y == other.Normal.Y)) && (this.Normal.Z == other.Normal.Z)) && (this.D == other.D));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Plane) {
				flag = this.Equals ((Plane)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Normal.GetHashCode () + this.D.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Normal:{0} D:{1}}}", new Object[] { this.Normal.ToString (), this.D.ToString () });
		}

		public void Normalise ()
		{
			Single one = 1;
			Single somethingWicked; RealMaths.FromString("0.0000001192093", out somethingWicked);

			Single num2 = ((this.Normal.X * this.Normal.X) + (this.Normal.Y * this.Normal.Y)) + (this.Normal.Z * this.Normal.Z);
			if (RealMaths.Abs (num2 - one) >= somethingWicked) {
				Single num = one / RealMaths.Sqrt (num2);
				this.Normal.X *= num;
				this.Normal.Y *= num;
				this.Normal.Z *= num;
				this.D *= num;
			}
		}

		public static void Normalise (ref Plane value, out Plane result)
		{
			Single one = 1;
			Single somethingWicked; RealMaths.FromString("0.0000001192093", out somethingWicked);

			Single num2 = ((value.Normal.X * value.Normal.X) + (value.Normal.Y * value.Normal.Y)) + (value.Normal.Z * value.Normal.Z);
			if (RealMaths.Abs (num2 - one) < somethingWicked) {
				result.Normal = value.Normal;
				result.D = value.D;
			} else {
				Single num = one / RealMaths.Sqrt (num2);
				result.Normal.X = value.Normal.X * num;
				result.Normal.Y = value.Normal.Y * num;
				result.Normal.Z = value.Normal.Z * num;
				result.D = value.D * num;
			}
		}

		public static void Transform (ref Plane plane, ref Matrix44 matrix, out Plane result)
		{
			Matrix44 matrix2;
			Matrix44.Invert (ref matrix, out matrix2);
			Single x = plane.Normal.X;
			Single y = plane.Normal.Y;
			Single z = plane.Normal.Z;
			Single d = plane.D;
			result.Normal.X = (((x * matrix2.M11) + (y * matrix2.M12)) + (z * matrix2.M13)) + (d * matrix2.M14);
			result.Normal.Y = (((x * matrix2.M21) + (y * matrix2.M22)) + (z * matrix2.M23)) + (d * matrix2.M24);
			result.Normal.Z = (((x * matrix2.M31) + (y * matrix2.M32)) + (z * matrix2.M33)) + (d * matrix2.M34);
			result.D = (((x * matrix2.M41) + (y * matrix2.M42)) + (z * matrix2.M43)) + (d * matrix2.M44);
		}


		public static void Transform (ref Plane plane, ref Quaternion rotation, out Plane result)
		{
			Single one = 1;

			Single num15 = rotation.X + rotation.X;
			Single num5 = rotation.Y + rotation.Y;
			Single num = rotation.Z + rotation.Z;
			Single num14 = rotation.W * num15;
			Single num13 = rotation.W * num5;
			Single num12 = rotation.W * num;
			Single num11 = rotation.X * num15;
			Single num10 = rotation.X * num5;
			Single num9 = rotation.X * num;
			Single num8 = rotation.Y * num5;
			Single num7 = rotation.Y * num;
			Single num6 = rotation.Z * num;
			Single num24 = (one - num8) - num6;
			Single num23 = num10 - num12;
			Single num22 = num9 + num13;
			Single num21 = num10 + num12;
			Single num20 = (one - num11) - num6;
			Single num19 = num7 - num14;
			Single num18 = num9 - num13;
			Single num17 = num7 + num14;
			Single num16 = (one - num11) - num8;
			Single x = plane.Normal.X;
			Single y = plane.Normal.Y;
			Single z = plane.Normal.Z;
			result.Normal.X = ((x * num24) + (y * num23)) + (z * num22);
			result.Normal.Y = ((x * num21) + (y * num20)) + (z * num19);
			result.Normal.Z = ((x * num18) + (y * num17)) + (z * num16);
			result.D = plane.D;
		}
		


		public Single Dot(ref Vector4 value)
		{
			return (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + (this.D * value.W);
		}

		public Single DotCoordinate (ref Vector3 value)
		{
			return (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + this.D;
		}

		public Single DotNormal (ref Vector3 value)
		{
			return ((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z);
		}

		public PlaneIntersectionType Intersects (ref BoundingBox box)
		{
			Single zero = 0;

			Vector3 vector;
			Vector3 vector2;
			vector2.X = (this.Normal.X >= zero) ? box.Min.X : box.Max.X;
			vector2.Y = (this.Normal.Y >= zero) ? box.Min.Y : box.Max.Y;
			vector2.Z = (this.Normal.Z >= zero) ? box.Min.Z : box.Max.Z;
			vector.X = (this.Normal.X >= zero) ? box.Max.X : box.Min.X;
			vector.Y = (this.Normal.Y >= zero) ? box.Max.Y : box.Min.Y;
			vector.Z = (this.Normal.Z >= zero) ? box.Max.Z : box.Min.Z;
			Single num = ((this.Normal.X * vector2.X) + (this.Normal.Y * vector2.Y)) + (this.Normal.Z * vector2.Z);
			if ((num + this.D) > zero) {
				return PlaneIntersectionType.Front;
			} else {
				num = ((this.Normal.X * vector.X) + (this.Normal.Y * vector.Y)) + (this.Normal.Z * vector.Z);
				if ((num + this.D) < zero) {
					return PlaneIntersectionType.Back;
				} else {
					return PlaneIntersectionType.Intersecting;
				}
			}
		}

		public PlaneIntersectionType Intersects (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			return frustum.Intersects (ref this);
		}

		public PlaneIntersectionType Intersects (ref BoundingSphere sphere)
		{
			Single num2 = ((sphere.Center.X * this.Normal.X) + (sphere.Center.Y * this.Normal.Y)) + (sphere.Center.Z * this.Normal.Z);
			Single num = num2 + this.D;
			if (num > sphere.Radius) {
				return PlaneIntersectionType.Front;
			} else if (num < -sphere.Radius) {
				return PlaneIntersectionType.Back;
			} else {
				return PlaneIntersectionType.Intersecting;
			}
		}

		public static Boolean operator == (Plane lhs, Plane rhs)
		{
			return lhs.Equals (rhs);
		}

		public static Boolean operator != (Plane lhs, Plane rhs)
		{
			if (((lhs.Normal.X == rhs.Normal.X) && (lhs.Normal.Y == rhs.Normal.Y)) && (lhs.Normal.Z == rhs.Normal.Z)) {
				return !(lhs.D == rhs.D);
			}
			return true;
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Quad
		: IEquatable<Quad>
	{
		public Vector3 A
		{
			get
			{
				return tri1.A;
			}
			set
			{
				tri1.A = value;
			}
		}

		public Vector3 B
		{
			get
			{
				return tri1.B;
			}
			set
			{
				tri1.B = value;
				tri2.B = value;
			}
		}

		public Vector3 C
		{
			get
			{
				return tri2.C;
			}
			set
			{
				tri1.C = value;
				tri2.C = value;
			}
		}

		public Vector3 D
		{
			get
			{
				return tri2.A;
			}
			set
			{
				tri1.A = value;
				tri2.A = value;
			}
		}

		Triangle tri1;
		Triangle tri2;

		public Quad (Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			this.tri1 = new Triangle(a, b, c);
			this.tri2 = new Triangle(d, b, c);
		}

		// Determines whether or not this Quad is equal in value to another Quad
		public Boolean Equals (Quad other)
		{
			if (this.A.X != other.A.X) return false;
			if (this.A.Y != other.A.Y) return false;
			if (this.A.Z != other.A.Z) return false;

			if (this.B.X != other.B.X) return false;
			if (this.B.Y != other.B.Y) return false;
			if (this.B.Z != other.B.Z) return false;

			if (this.C.X != other.C.X) return false;
			if (this.C.Y != other.C.Y) return false;
			if (this.C.Z != other.C.Z) return false;
			
			if (this.D.X != other.D.X) return false;
			if (this.D.Y != other.D.Y) return false;
			if (this.D.Z != other.D.Z) return false;

			// They match!
			return true;
		}

		// Determines whether or not this Quad is equal in value to another System.Object
		public override Boolean Equals (Object obj)
		{
			if (obj == null) return false;

			if (obj is Quad)
			{
				// Ok, it is a Quad, so just use the method above to compare.
				return this.Equals ((Quad) obj);
			}

			return false;
		}

		public override Int32 GetHashCode ()
		{
			return (this.A.GetHashCode () + this.B.GetHashCode () + this.C.GetHashCode () + this.D.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{A:{0} B:{1} C:{2} D:{3}}}", this.A, this.B, this.C, this.D);
		}

		public static Boolean operator == (Quad a, Quad b)
		{
			return a.Equals(b);
		}

		public static Boolean operator != (Quad a, Quad b)
		{
			return !a.Equals(b);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Quaternion 
		: IEquatable<Quaternion>
	{
		public Single X;
		public Single Y;
		public Single Z;
		public Single W;


		public Quaternion (Single x, Single y, Single z, Single w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		public Quaternion (Vector3 vectorPart, Single scalarPart)
		{
			this.X = vectorPart.X;
			this.Y = vectorPart.Y;
			this.Z = vectorPart.Z;
			this.W = scalarPart;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString (), this.W.ToString () });
		}

		public Boolean Equals (Quaternion other)
		{
			return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
		}

		public override Boolean Equals (Object obj)
		{

			Boolean flag = false;
			if (obj is Quaternion)
			{
				flag = this.Equals ((Quaternion)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ()) + this.W.GetHashCode ());
		}

		public Single LengthSquared ()
		{
			return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
		}

		public Single Length ()
		{
			Single num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			return RealMaths.Sqrt (num);
		}

		public void Normalise ()
		{
			Single one = 1;
			Single num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			Single num = one / RealMaths.Sqrt (num2);
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
			this.W *= num;
		}

		public Boolean IsUnit()
		{
			Single one = 1;

			return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
		}

		public void Conjugate ()
		{
			this.X = -this.X;
			this.Y = -this.Y;
			this.Z = -this.Z;
		}

		#region Constants

		static Quaternion identity;
		
		public static Quaternion Identity
		{
			get
			{
				return identity;
			}
		}

		static Quaternion ()
		{
			Single temp_one; RealMaths.One(out temp_one);
			Single temp_zero; RealMaths.Zero(out temp_zero);
			identity = new Quaternion (temp_zero, temp_zero, temp_zero, temp_one);
		}
		
		#endregion
		#region Create

		public static void CreateFromAxisAngle (ref Vector3 axis, Single angle, out Quaternion result)
		{
			Single half; RealMaths.Half(out half);
			Single theta = angle * half;

			Single sin = RealMaths.Sin (theta);
			Single cos = RealMaths.Cos (theta);

			result.X = axis.X * sin;
			result.Y = axis.Y * sin;
			result.Z = axis.Z * sin;

			result.W = cos;
		}
		
		public static void CreateFromYawPitchRoll (Single yaw, Single pitch, Single roll, out Quaternion result)
		{
			Single half; RealMaths.Half(out half);
			Single num9 = roll * half;

			Single num6 = RealMaths.Sin (num9);
			Single num5 = RealMaths.Cos (num9);

			Single num8 = pitch * half;

			Single num4 = RealMaths.Sin (num8);
			Single num3 = RealMaths.Cos (num8);

			Single num7 = yaw * half;

			Single num2 = RealMaths.Sin (num7);
			Single num = RealMaths.Cos (num7);

			result.X = ((num * num4) * num5) + ((num2 * num3) * num6);
			result.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
			result.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
			result.W = ((num * num3) * num5) + ((num2 * num4) * num6);
		}
		
		public static void CreateFromRotationMatrix (ref Matrix44 matrix, out Quaternion result)
		{
			Single zero = 0;
			Single half; RealMaths.Half(out half);
			Single one = 1;

			Single num8 = (matrix.M11 + matrix.M22) + matrix.M33;

			if (num8 > zero)
			{
				Single num = RealMaths.Sqrt (num8 + one);
				result.W = num * half;
				num = half / num;
				result.X = (matrix.M23 - matrix.M32) * num;
				result.Y = (matrix.M31 - matrix.M13) * num;
				result.Z = (matrix.M12 - matrix.M21) * num;
			}
			else if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
			{
				Single num7 = RealMaths.Sqrt (((one + matrix.M11) - matrix.M22) - matrix.M33);
				Single num4 = half / num7;
				result.X = half * num7;
				result.Y = (matrix.M12 + matrix.M21) * num4;
				result.Z = (matrix.M13 + matrix.M31) * num4;
				result.W = (matrix.M23 - matrix.M32) * num4;
			}
			else if (matrix.M22 > matrix.M33)
			{
				Single num6 =RealMaths.Sqrt (((one + matrix.M22) - matrix.M11) - matrix.M33);
				Single num3 = half / num6;
				result.X = (matrix.M21 + matrix.M12) * num3;
				result.Y = half * num6;
				result.Z = (matrix.M32 + matrix.M23) * num3;
				result.W = (matrix.M31 - matrix.M13) * num3;
			}
			else
			{
				Single num5 = RealMaths.Sqrt (((one + matrix.M33) - matrix.M11) - matrix.M22);
				Single num2 = half / num5;
				result.X = (matrix.M31 + matrix.M13) * num2;
				result.Y = (matrix.M32 + matrix.M23) * num2;
				result.Z = half * num5;
				result.W = (matrix.M12 - matrix.M21) * num2;
			}
		}
		
		#endregion
		#region Maths

		public static void Conjugate (ref Quaternion value, out Quaternion result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = value.W;
		}
		
		public static void Inverse (ref Quaternion quaternion, out Quaternion result)
		{
			Single one = 1;
			Single num2 = ( ( (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) ) + 
			                (quaternion.Z * quaternion.Z) ) + (quaternion.W * quaternion.W);

			Single num = one / num2;

			result.X = -quaternion.X * num;
			result.Y = -quaternion.Y * num;
			result.Z = -quaternion.Z * num;
			result.W = quaternion.W * num;
		}
		
		
		public static void Dot (ref Quaternion quaternion1, ref Quaternion quaternion2, out Single result)
		{
			result = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + 
			          (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
		}


		public static void Concatenate (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
		{
			Single x = value2.X;
			Single y = value2.Y;
			Single z = value2.Z;
			Single w = value2.W;
			Single num4 = value1.X;
			Single num3 = value1.Y;
			Single num2 = value1.Z;
			Single num = value1.W;
			Single num12 = (y * num2) - (z * num3);
			Single num11 = (z * num4) - (x * num2);
			Single num10 = (x * num3) - (y * num4);
			Single num9 = ((x * num4) + (y * num3)) + (z * num2);
			result.X = ((x * num) + (num4 * w)) + num12;
			result.Y = ((y * num) + (num3 * w)) + num11;
			result.Z = ((z * num) + (num2 * w)) + num10;
			result.W = (w * num) - num9;
		}
		
		public static void Normalise (ref Quaternion quaternion, out Quaternion result)
		{
			Single one = 1;

			Single num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
			Single num = one / RealMaths.Sqrt (num2);
			result.X = quaternion.X * num;
			result.Y = quaternion.Y * num;
			result.Z = quaternion.Z * num;
			result.W = quaternion.W * num;
		}
		
		#endregion
		#region Operators

		public static Quaternion operator - (Quaternion quaternion)
		{
			Quaternion quaternion2;
			quaternion2.X = -quaternion.X;
			quaternion2.Y = -quaternion.Y;
			quaternion2.Z = -quaternion.Z;
			quaternion2.W = -quaternion.W;
			return quaternion2;
		}
		
		public static Boolean operator == (Quaternion quaternion1, Quaternion quaternion2)
		{
			return ((((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) && (quaternion1.W == quaternion2.W));
		}
		
		public static Boolean operator != (Quaternion quaternion1, Quaternion quaternion2)
		{
			if (((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) {
				return !(quaternion1.W == quaternion2.W);
			}
			return true;
		}
		
		public static Quaternion operator + (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X + quaternion2.X;
			quaternion.Y = quaternion1.Y + quaternion2.Y;
			quaternion.Z = quaternion1.Z + quaternion2.Z;
			quaternion.W = quaternion1.W + quaternion2.W;
			return quaternion;
		}
		
		public static Quaternion operator - (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X - quaternion2.X;
			quaternion.Y = quaternion1.Y - quaternion2.Y;
			quaternion.Z = quaternion1.Z - quaternion2.Z;
			quaternion.W = quaternion1.W - quaternion2.W;
			return quaternion;
		}
		
		public static Quaternion operator * (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			Single x = quaternion1.X;
			Single y = quaternion1.Y;
			Single z = quaternion1.Z;
			Single w = quaternion1.W;
			Single num4 = quaternion2.X;
			Single num3 = quaternion2.Y;
			Single num2 = quaternion2.Z;
			Single num = quaternion2.W;
			Single num12 = (y * num2) - (z * num3);
			Single num11 = (z * num4) - (x * num2);
			Single num10 = (x * num3) - (y * num4);
			Single num9 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.X = ((x * num) + (num4 * w)) + num12;
			quaternion.Y = ((y * num) + (num3 * w)) + num11;
			quaternion.Z = ((z * num) + (num2 * w)) + num10;
			quaternion.W = (w * num) - num9;
			return quaternion;
		}
		
		public static Quaternion operator * (Quaternion quaternion1, Single scaleFactor)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X * scaleFactor;
			quaternion.Y = quaternion1.Y * scaleFactor;
			quaternion.Z = quaternion1.Z * scaleFactor;
			quaternion.W = quaternion1.W * scaleFactor;
			return quaternion;
		}
		
		public static Quaternion operator / (Quaternion quaternion1, Quaternion quaternion2)
		{
			Single one = 1;

			Quaternion quaternion;
			Single x = quaternion1.X;
			Single y = quaternion1.Y;
			Single z = quaternion1.Z;
			Single w = quaternion1.W;
			Single num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
			Single num5 = one / num14;
			Single num4 = -quaternion2.X * num5;
			Single num3 = -quaternion2.Y * num5;
			Single num2 = -quaternion2.Z * num5;
			Single num = quaternion2.W * num5;
			Single num13 = (y * num2) - (z * num3);
			Single num12 = (z * num4) - (x * num2);
			Single num11 = (x * num3) - (y * num4);
			Single num10 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.X = ((x * num) + (num4 * w)) + num13;
			quaternion.Y = ((y * num) + (num3 * w)) + num12;
			quaternion.Z = ((z * num) + (num2 * w)) + num11;
			quaternion.W = (w * num) - num10;
			return quaternion;
		}



		
		public static void Negate (ref Quaternion quaternion, out Quaternion result)
		{
			result.X = -quaternion.X;
			result.Y = -quaternion.Y;
			result.Z = -quaternion.Z;
			result.W = -quaternion.W;
		}

		public static void Add (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			result.X = quaternion1.X + quaternion2.X;
			result.Y = quaternion1.Y + quaternion2.Y;
			result.Z = quaternion1.Z + quaternion2.Z;
			result.W = quaternion1.W + quaternion2.W;
		}
		
		public static void Subtract (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			result.X = quaternion1.X - quaternion2.X;
			result.Y = quaternion1.Y - quaternion2.Y;
			result.Z = quaternion1.Z - quaternion2.Z;
			result.W = quaternion1.W - quaternion2.W;
		}

		public static void Multiply (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			Single x = quaternion1.X;
			Single y = quaternion1.Y;
			Single z = quaternion1.Z;
			Single w = quaternion1.W;
			Single num4 = quaternion2.X;
			Single num3 = quaternion2.Y;
			Single num2 = quaternion2.Z;
			Single num = quaternion2.W;
			Single num12 = (y * num2) - (z * num3);
			Single num11 = (z * num4) - (x * num2);
			Single num10 = (x * num3) - (y * num4);
			Single num9 = ((x * num4) + (y * num3)) + (z * num2);
			result.X = ((x * num) + (num4 * w)) + num12;
			result.Y = ((y * num) + (num3 * w)) + num11;
			result.Z = ((z * num) + (num2 * w)) + num10;
			result.W = (w * num) - num9;
		}

		public static void Multiply (ref Quaternion quaternion1, Single scaleFactor, out Quaternion result)
		{
			result.X = quaternion1.X * scaleFactor;
			result.Y = quaternion1.Y * scaleFactor;
			result.Z = quaternion1.Z * scaleFactor;
			result.W = quaternion1.W * scaleFactor;
		}
		
		public static void Divide (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			Single one = 1;

			Single x = quaternion1.X;
			Single y = quaternion1.Y;
			Single z = quaternion1.Z;
			Single w = quaternion1.W;
			Single num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
			Single num5 = one / num14;
			Single num4 = -quaternion2.X * num5;
			Single num3 = -quaternion2.Y * num5;
			Single num2 = -quaternion2.Z * num5;
			Single num = quaternion2.W * num5;
			Single num13 = (y * num2) - (z * num3);
			Single num12 = (z * num4) - (x * num2);
			Single num11 = (x * num3) - (y * num4);
			Single num10 = ((x * num4) + (y * num3)) + (z * num2);
			result.X = ((x * num) + (num4 * w)) + num13;
			result.Y = ((y * num) + (num3 * w)) + num12;
			result.Z = ((z * num) + (num2 * w)) + num11;
			result.W = (w * num) - num10;
		}
		
		#endregion
		#region Utilities

		public static void Slerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Single amount, out Quaternion result)
		{
			Single zero = 0;
			Single one = 1;
			Single nineninenine; RealMaths.FromString("0.999999", out nineninenine);

			Single num2;
			Single num3;
			Single num = amount;
			Single num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
			Boolean flag = false;
			if (num4 < zero) {
				flag = true;
				num4 = -num4;
			}


			if (num4 >nineninenine) {
				num3 = one - num;
				num2 = flag ? -num : num;
			} else {
				Single num5 = RealMaths.ArcCos (num4);
				Single num6 = one / RealMaths.Sin (num5);

				num3 = RealMaths.Sin ((one - num) * num5) * num6;

				num2 = flag ? -RealMaths.Sin (num * num5) * num6 : RealMaths.Sin (num * num5) * num6;
			}
			result.X = (num3 * quaternion1.X) + (num2 * quaternion2.X);
			result.Y = (num3 * quaternion1.Y) + (num2 * quaternion2.Y);
			result.Z = (num3 * quaternion1.Z) + (num2 * quaternion2.Z);
			result.W = (num3 * quaternion1.W) + (num2 * quaternion2.W);
		}

		public static void Lerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Single amount, out Quaternion result)
		{
			Single zero = 0;
			Single one = 1;

			Single num = amount;
			Single num2 = one - num;
			Single num5 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
			if (num5 >= zero) {
				result.X = (num2 * quaternion1.X) + (num * quaternion2.X);
				result.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
				result.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
				result.W = (num2 * quaternion1.W) + (num * quaternion2.W);
			} else {
				result.X = (num2 * quaternion1.X) - (num * quaternion2.X);
				result.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
				result.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
				result.W = (num2 * quaternion1.W) - (num * quaternion2.W);
			}
			Single num4 = (((result.X * result.X) + (result.Y * result.Y)) + (result.Z * result.Z)) + (result.W * result.W);
			Single num3 = one / RealMaths.Sqrt (num4);
			result.X *= num3;
			result.Y *= num3;
			result.Z *= num3;
			result.W *= num3;
		}
		
		#endregion

	}	[StructLayout (LayoutKind.Sequential)]
	public struct Ray 
		: IEquatable<Ray>
	{
		// The starting position of this ray
		public Vector3 Position;
		
		// Normalised vector that defines the direction of this ray
		public Vector3 Direction;

		public Ray (Vector3 position, Vector3 direction)
		{
			this.Position = position;
			this.Direction = direction;
		}

		// Determines whether or not this ray is equal in value to another ray
		public Boolean Equals (Ray other)
		{
			// Check position
			if (this.Position.X != other.Position.X) return false;
			if (this.Position.Y != other.Position.Y) return false;
			if (this.Position.Z != other.Position.Z) return false;

			// Check direction
			if (this.Direction.X != other.Direction.X) return false;
			if (this.Direction.Y != other.Direction.Y) return false;
			if (this.Direction.Z != other.Direction.Z) return false;

			// They match!
			return true;
		}

		// Determines whether or not this ray is equal in value to another System.Object
		public override Boolean Equals (Object obj)
		{
			if (obj == null) return false;

			if (obj is Ray)
			{
				// Ok, it is a Ray, so just use the method above to compare.
				return this.Equals ((Ray) obj);
			}

			return false;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Position.GetHashCode () + this.Direction.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Position:{0} Direction:{1}}}", this.Position, this.Direction);
		}

		// At what distance from it's starting position does this ray
		// intersect the given box.  Returns null if there is no
		// intersection.
		public Single? Intersects (ref BoundingBox box)
		{
			return box.Intersects (ref this);
		}

		// At what distance from it's starting position does this ray
		// intersect the given frustum.  Returns null if there is no
		// intersection.
		public Single? Intersects (ref BoundingFrustum frustum)
		{
			if (frustum == null)
			{
				throw new ArgumentNullException ();
			}

			return frustum.Intersects (ref this);
		}

		// At what distance from it's starting position does this ray
		// intersect the given plane.  Returns null if there is no
		// intersection.
		public Single? Intersects (ref Plane plane)
		{
			Single zero = 0;

			Single nearZero; RealMaths.FromString("0.00001", out nearZero);

			Single num2 = ((plane.Normal.X * this.Direction.X) + (plane.Normal.Y * this.Direction.Y)) + (plane.Normal.Z * this.Direction.Z);
			
			if (RealMaths.Abs (num2) < nearZero)
			{
				return null;
			}
			
			Single num3 = ((plane.Normal.X * this.Position.X) + (plane.Normal.Y * this.Position.Y)) + (plane.Normal.Z * this.Position.Z);

			Single num = (-plane.D - num3) / num2;
			
			if (num < zero)
			{
				if (num < -nearZero)
				{
					return null;
				}

				num = zero;
			}

			return new Single? (num);
		}

		// At what distance from it's starting position does this ray
		// intersect the given sphere.  Returns null if there is no
		// intersection.
		public Single? Intersects (ref BoundingSphere sphere)
		{
			Single zero = 0;

			Single initialXOffset = sphere.Center.X - this.Position.X;

			Single initialYOffset = sphere.Center.Y - this.Position.Y;
			
			Single initialZOffset = sphere.Center.Z - this.Position.Z;
			
			Single num7 = ((initialXOffset * initialXOffset) + (initialYOffset * initialYOffset)) + (initialZOffset * initialZOffset);

			Single num2 = sphere.Radius * sphere.Radius;

			if (num7 <= num2)
			{
				return zero;
			}

			Single num = ((initialXOffset * this.Direction.X) + (initialYOffset * this.Direction.Y)) + (initialZOffset * this.Direction.Z);
			if (num < zero)
			{
				return null;
			}
			
			Single num6 = num7 - (num * num);
			if (num6 > num2)
			{
				return null;
			}
			
			Single num8 = RealMaths.Sqrt ((num2 - num6));

			return new Single? (num - num8);
		}

		public static Boolean operator == (Ray a, Ray b)
		{
			return a.Equals(b);
		}

		public static Boolean operator != (Ray a, Ray b)
		{
			return !a.Equals(b);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Triangle
		: IEquatable<Triangle>
	{
		public Vector3 A;
		public Vector3 B;
		public Vector3 C;

		public Triangle (Vector3 a, Vector3 b, Vector3 c)
		{
			this.A = a;
			this.B = b;
			this.C = c;
		}

		// Determines whether or not this Triangle is equal in value to another Triangle
		public Boolean Equals (Triangle other)
		{
			if (this.A.X != other.A.X) return false;
			if (this.A.Y != other.A.Y) return false;
			if (this.A.Z != other.A.Z) return false;

			if (this.B.X != other.B.X) return false;
			if (this.B.Y != other.B.Y) return false;
			if (this.B.Z != other.B.Z) return false;

			if (this.C.X != other.C.X) return false;
			if (this.C.Y != other.C.Y) return false;
			if (this.C.Z != other.C.Z) return false;

			// They match!
			return true;
		}

		// Determines whether or not this Triangle is equal in value to another System.Object
		public override Boolean Equals (Object obj)
		{
			if (obj == null) return false;

			if (obj is Triangle)
			{
				// Ok, it is a Triangle, so just use the method above to compare.
				return this.Equals ((Triangle) obj);
			}

			return false;
		}

		public override Int32 GetHashCode ()
		{
			return (this.A.GetHashCode () + this.B.GetHashCode () + this.C.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{A:{0} B:{1} C:{2}}}", this.A, this.B, this.C);
		}

		public static Boolean operator == (Triangle a, Triangle b)
		{
			return a.Equals(b);
		}

		public static Boolean operator != (Triangle a, Triangle b)
		{
			return !a.Equals(b);
		}

		public static Boolean IsPointInTriangleangle( ref Vector3 point, ref Triangle triangle )
		{
			Vector3 aToB = triangle.B - triangle.A;
			Vector3 bToC = triangle.C - triangle.B;

			Vector3 n; Vector3.Cross(ref aToB, ref bToC, out n);

			Vector3 aToPoint = point - triangle.A;

			Vector3 wTest; Vector3.Cross(ref aToB, ref aToPoint, out wTest);

			Single zero = 0;

			Single dot; Vector3.Dot(ref wTest, ref n, out dot);

			if ( dot < zero )
			{
				return false;
			}

			Vector3 bToPoint = point - triangle.B;

			Vector3.Cross(ref bToC, ref bToPoint, out wTest);

			Vector3.Dot(ref wTest, ref n, out dot);

			if ( dot < zero )
			{
				return false;
			}

			Vector3 cToA = triangle.A - triangle.C;

			Vector3 cToPoint = point - triangle.C;

			Vector3.Cross(ref cToA, ref cToPoint, out wTest);

			Vector3.Dot(ref wTest, ref n, out dot);

			if ( dot < zero )
			{
				return false;
			}

			return true;
		}

		// Determines whether or not a triangle is degenerate ( all points lay on the same line in space ).
		public Boolean IsDegenerate()
		{
			throw new System.NotImplementedException();
		}

		// Get's the Barycentric coordinates of a point inside a Triangle.
		public static void BarycentricCoordinates( ref Vector3 point, ref Triangle triangle, out Vector3 barycentricCoordinates )
		{
			if( triangle.IsDegenerate() )
			{
				throw new System.ArgumentException("Input Triangle is degenerate, this is not supported.");
			}

			Vector3 aToB = triangle.B - triangle.A;
			Vector3 aToC = triangle.C - triangle.A;
			Vector3 aToPoint = point - triangle.A;

			// compute cross product to get area of parallelograms
			Vector3 cross1; Vector3.Cross(ref aToB, ref aToPoint, out cross1);
			Vector3 cross2; Vector3.Cross(ref aToC, ref aToPoint, out cross2);
			Vector3 cross3; Vector3.Cross(ref aToB, ref aToC, out cross3);
	
			// compute barycentric coordinates as ratios of areas

			Single one = 1;

			Single denom = one / cross3.Length();
			barycentricCoordinates.X = cross2.Length() * denom;
			barycentricCoordinates.Y = cross1.Length() * denom;
			barycentricCoordinates.Z = one - barycentricCoordinates.X - barycentricCoordinates.Y;
		}
		/*

		// Triangleangle Intersect
		// ------------------
		// Returns true if triangles P0P1P2 and Q0Q1Q2 intersect
		// Assumes triangle is not degenerate
		//
		// This is not the algorithm presented in the text.  Instead, it is based on a 
		// recent article by Guigue and Devillers in the July 2003 issue Journal of 
		// Graphics Tools.  As it is faster than the ERIT algorithm, under ordinary 
		// circumstances it would have been discussed in the text, but it arrived too late.  
		//
		// More information and the original source code can be found at
		// http://www.acm.org/jgt/papers/GuigueDevillers03/
		//
		// A nearly identical algorithm was in the same issue of JGT, by Shen Heng and 
		// Tang.  See http://www.acm.org/jgt/papers/ShenHengTang03/ for source code.
		//
		// Yes, this is complicated.  Did you think testing triangles would be easy?
		//
		static Boolean TriangleangleIntersect( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2 )
		{
			// test P against Q's plane
			Vector3 normalQ = Vector3.Cross( Q1 - Q0, Q2 - Q0 );

			Single testP0 = Vector3.Dot( normalQ, P0 - Q0 );
			Single testP1 = Vector3.Dot( normalQ, P1 - Q0 );
			Single testP2 = Vector3.Dot( normalQ, P2 - Q0 );
  
			// P doesn't intersect Q's plane
			if ( testP0 * testP1 > AbacusHelper.Epsilon && testP0*testP2 > AbacusHelper.Epsilon )
				return false;

			// test Q against P's plane
			Vector3 normalP = Vector3.Cross( P1 - P0, P2 - P0 );

			Single testQ0 = Vector3.Dot( normalP, Q0 - P0 );
			Single testQ1 = Vector3.Dot( normalP, Q1 - P0 );
			Single testQ2 = Vector3.Dot( normalP, Q2 - P0 );
  
			// Q doesn't intersect P's plane
			if (testQ0*testQ1 > AbacusHelper.Epsilon && testQ0*testQ2 > AbacusHelper.Epsilon )
				return false;
	
			// now we rearrange P's vertices such that the lone vertex (the one that lies
			// in its own half-space of Q) is first.  We also permute the other
			// triangle's vertices so that P0 will "see" them in counterclockwise order

			// Once reordered, we pass the vertices down to a helper function which will
			// reorder Q's vertices, and then test

			// P0 in Q's positive half-space
			if (testP0 > AbacusHelper.Epsilon) 
			{
				// P1 in Q's positive half-space (so P2 is lone vertex)
				if (testP1 > AbacusHelper.Epsilon) 
					return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
				// P2 in Q's positive half-space (so P1 is lone vertex)
				else if (testP2 > AbacusHelper.Epsilon) 
					return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);	
				// P0 is lone vertex
				else 
					return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
			} 
			// P0 in Q's negative half-space
			else if (testP0 < -AbacusHelper.Epsilon) 
			{
				// P1 in Q's negative half-space (so P2 is lone vertex)
				if (testP1 < -AbacusHelper.Epsilon) 
					return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
				// P2 in Q's negative half-space (so P1 is lone vertex)
				else if (testP2 < -AbacusHelper.Epsilon) 
					return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
				// P0 is lone vertex
				else 
					return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
			} 
			// P0 on Q's plane
			else 
			{
				// P1 in Q's negative half-space 
				if (testP1 < -AbacusHelper.Epsilon) 
				{
					// P2 in Q's negative half-space (P0 is lone vertex)
					if (testP2 < -AbacusHelper.Epsilon) 
						return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
					// P2 in positive half-space or on plane (P1 is lone vertex)
					else 
						return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
				}
				// P1 in Q's positive half-space 
				else if (testP1 > AbacusHelper.Epsilon) 
				{
					// P2 in Q's positive half-space (P0 is lone vertex)
					if (testP2 > AbacusHelper.Epsilon) 
						return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
					// P2 in negative half-space or on plane (P1 is lone vertex)
					else 
						return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
				}
				// P1 lies on Q's plane too
				else  
				{
					// P2 in Q's positive half-space (P2 is lone vertex)
					if (testP2 > AbacusHelper.Epsilon) 
						return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
					// P2 in Q's negative half-space (P2 is lone vertex)
					// note different ordering for Q vertices
					else if (testP2 < -AbacusHelper.Epsilon) 
						return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
					// all three points lie on Q's plane, default to 2D test
					else 
						return CoplanarTriangleangleIntersect(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, ref normalP);
				}
			}
		}



		// Adjust Q
		// --------
		// Helper for TriangleangleIntersect()
		//
		// Now we rearrange Q's vertices such that the lone vertex (the one that lies
		// in its own half-space of P) is first.  We also permute the other
		// triangle's vertices so that Q0 will "see" them in counterclockwise order
		//
		// Once reordered, we pass the vertices down to a helper function which will
		// actually test for intersection on the common line between the two planes
		//
		static Boolean AdjustQ( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2,
			Single testQ0, Single testQ1, Single testQ2,
			ref Vector3 normalP )
		{

			// Q0 in P's positive half-space
			if (testQ0 > AbacusHelper.Epsilon) 
			{ 
				// Q1 in P's positive half-space (so Q2 is lone vertex)
				if (testQ1 > AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P2, ref P1, ref Q2, ref Q0, ref Q1);
				// Q2 in P's positive half-space (so Q1 is lone vertex)
				else if (testQ2 > AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P2, ref P1, ref Q1, ref Q2, ref Q0);
				// Q0 is lone vertex
				else 
					return TestLineOverlap(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2);
			}
			// Q0 in P's negative half-space
			else if (testQ0 < -AbacusHelper.Epsilon) 
			{ 
				// Q1 in P's negative half-space (so Q2 is lone vertex)
				if (testQ1 < -AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P1, ref P2, ref Q2, ref Q0, ref Q1);
				// Q2 in P's negative half-space (so Q1 is lone vertex)
				else if (testQ2 < -AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P1, ref P2, ref Q1, ref Q2, ref Q0);
				// Q0 is lone vertex
				else 
					return TestLineOverlap(ref P0, ref P2, ref P1, ref Q0, ref Q1, ref Q2);
			}
			// Q0 on P's plane
			else 
			{ 
				// Q1 in P's negative half-space 
				if (testQ1 < -AbacusHelper.Epsilon) 
				{ 
					// Q2 in P's negative half-space (Q0 is lone vertex)
					if (testQ2 < -AbacusHelper.Epsilon)  
						return TestLineOverlap(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2);
					// Q2 in positive half-space or on plane (P1 is lone vertex)
					else 
						return TestLineOverlap(ref P0, ref P2, ref P1, ref Q1, ref Q2, ref Q0);
				}
				// Q1 in P's positive half-space 
				else if (testQ1 > AbacusHelper.Epsilon) 
				{ 
					// Q2 in P's positive half-space (Q0 is lone vertex)
					if (testQ2 > AbacusHelper.Epsilon) 
						return TestLineOverlap(ref P0, ref P2, ref P1, ref Q0, ref Q1, ref Q2);
					// Q2 in negative half-space or on plane (P1 is lone vertex)
					else  
						return TestLineOverlap(ref P0, ref P1, ref P2, ref Q1, ref Q2, ref Q0);
				}
				// Q1 lies on P's plane too
				else 
				{
					// Q2 in P's positive half-space (Q2 is lone vertex)
					if (testQ2 > AbacusHelper.Epsilon) 
						return TestLineOverlap(ref P0, ref P1, ref P2, ref Q2, ref Q0, ref Q1);
					// Q2 in P's negative half-space (Q2 is lone vertex)
					// note different ordering for Q vertices
					else if (testQ2 < -AbacusHelper.Epsilon) 
						return TestLineOverlap(ref P0, ref P2, ref P1, ref Q2, ref Q0, ref Q1);
					// all three points lie on P's plane, default to 2D test
					else 
						return CoplanarTriangleangleIntersect(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, ref normalP);
				}
			}
		}



		// Test Line Overlap
		// -----------------
		// Helper for TriangleangleIntersect()
		//
		// This tests whether the rearranged triangles overlap, by checking the intervals
		// where their edges cross the common line between the two planes.  If the 
		// interval for P is [i,j] and Q is [k,l], then there is intersection if the
		// intervals overlap.  Previous algorithms computed these intervals directly, 
		// this tests implictly by using two "plane tests."
		//
		static Boolean TestLineOverlap( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2 )
		{
			// get "plane normal"
			Vector3 normal = Vector3.Cross( P1 - P0, Q0 - P0);

			// fails test, no intersection
			if ( Vector3.Dot(normal, Q1 - P0 ) > AbacusHelper.Epsilon )
				return false;
  
			// get "plane normal"
			normal = Vector3.Cross( P2 - P0, Q2 - P0 );

			// fails test, no intersection
			if ( Vector3.Dot( normal, Q0 - P0 ) > AbacusHelper.Epsilon )
				return false;

			// intersection!
			return true;
		}



		// Coplanar Triangleangle Intersect
		// ---------------------------
		// Helper for TriangleangleIntersect()
		//
		// This projects the two triangles down to 2D, maintaining the largest area by
		// dropping the dimension where the normal points the farthest.
		//
		static Boolean CoplanarTriangleangleIntersect( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2, 
			ref Vector3 planeNormal )
		{
			Vector3 absNormal = new Vector3( 
				System.Math.Abs(planeNormal.X), 
				System.Math.Abs(planeNormal.Y), 
				System.Math.Abs(planeNormal.Z) );

			Vector2 projP0, projP1, projP2;
			Vector2 projQ0, projQ1, projQ2;

			// if x is direction of largest magnitude
			if ( absNormal.X > absNormal.Y && absNormal.X >= absNormal.Z )
			{
				projP0 = new Vector2( P0.Y, P0.Z );
				projP1 = new Vector2( P1.Y, P1.Z );
				projP2 = new Vector2( P2.Y, P2.Z );
				projQ0 = new Vector2( Q0.Y, Q0.Z );
				projQ1 = new Vector2( Q1.Y, Q1.Z );
				projQ2 = new Vector2( Q2.Y, Q2.Z );
			}
			// if y is direction of largest magnitude
			else if ( absNormal.Y > absNormal.X && absNormal.Y >= absNormal.Z )
			{
				projP0 = new Vector2( P0.X, P0.Z );
				projP1 = new Vector2( P1.X, P1.Z );
				projP2 = new Vector2( P2.X, P2.Z );
				projQ0 = new Vector2( Q0.X, Q0.Z );
				projQ1 = new Vector2( Q1.X, Q1.Z );
				projQ2 = new Vector2( Q2.X, Q2.Z );
			}
			// z is the direction of largest magnitude
			else
			{
				projP0 = new Vector2( P0.X, P0.Y );
				projP1 = new Vector2( P1.X, P1.Y );
				projP2 = new Vector2( P2.X, P2.Y );
				projQ0 = new Vector2( Q0.X, Q0.Y );
				projQ1 = new Vector2( Q1.X, Q1.Y );
				projQ2 = new Vector2( Q2.X, Q2.Y );
			}

			return TriangleangleIntersect( ref projP0, ref projP1, ref projP2, ref projQ0, ref projQ1, ref projQ2 );
		}



		// Triangleangle Intersect
		// ------------------
		// Returns true if ray intersects triangle.
		//
		static Boolean TriangleangleIntersect( 
			ref Single t, //perhaps this should be out 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Ray ray )
		{
			// test ray direction against triangle
			Vector3 e1 = P1 - P0;
			Vector3 e2 = P2 - P0;
			Vector3 p = Vector3.Cross( ray.Direction, e2 );
			Single a = Vector3.Dot( e1, p );

			// if result zero, no intersection or infinite intersections
			// (ray parallel to triangle plane)
			if ( AbacusHelper.IsZero(a) )
				return false;

			// compute denominator
			Single f = 1.0f/a;

			// compute barycentric coordinates
			Vector3 s = ray.Position - P0;
			Single u = f * Vector3.Dot( s, p );

			// ray falls outside triangle
			if (u < 0.0f || u > 1.0f) 
				return false;

			Vector3 q = Vector3.Cross( s, e1 );
			Single v = f * Vector3.Dot( ray.Direction, q );

			// ray falls outside triangle
			if (v < 0.0f || u+v > 1.0f) 
				return false;

			// compute line parameter
			t = f * Vector3.Dot( e2, q );

			return (t >= 0.0f);
		}

		
		//
		// @ TriangleangleClassify()
		// Returns signed distance between plane and triangle
		//
		static Single TriangleangleClassify( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Plane plane )
		{
			Single test0 = plane.Test( P0 );
			Single test1 = plane.Test( P1 );

			// if two points lie on opposite sides of plane, intersect
			if (test0*test1 < 0.0f)
				return 0.0f;

			Single test2 = plane.Test( P2 );

			// if two points lie on opposite sides of plane, intersect
			if (test0*test2 < 0.0f)
				return 0.0f;
			if (test1*test2 < 0.0f)
				return 0.0f;

			// no intersection, return signed distance
			if ( test0 < 0.0f )
			{
				if ( test0 < test1 )
				{
					if ( test1 < test2 )
						return test2;
					else
						return test1;
				}
				else if (test0 < test2)
				{
					return test2;
				}
				else
				{   
					return test0;
				}
			}
			else
			{
				if ( test0 > test1 )
				{
					if ( test1 > test2 )
						return test2;
					else
						return test1;
				}
				else if (test0 > test2)
				{
					return test2;
				}
				else
				{   
					return test0;
				}
			}
		}

		*/
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Vector2
		: IEquatable<Vector2>
	{
		public Single X;
		public Single Y;
		
		public Vector2 (Int32 x, Int32 y)
		{
			this.X = (Single) x;
			this.Y = (Single) y;
		}

		public Vector2 (Single x, Single y)
		{
			this.X = x;
			this.Y = y;
		}

		public void Set (Single x, Single y)
		{
			this.X = x;
			this.Y = y;
		}

		public Vector2 (Single value)
		{
			this.X = this.Y = value;
		}

		public Single Length ()
		{
			Single num = (this.X * this.X) + (this.Y * this.Y);
			return RealMaths.Sqrt (num);
		}

		public Single LengthSquared ()
		{
			return ((this.X * this.X) + (this.Y * this.Y));
		}

		public void Normalise ()
		{
			Single num2 = (this.X * this.X) + (this.Y * this.Y);

			Single one = 1;
			Single num = one / (RealMaths.Sqrt (num2));
			this.X *= num;
			this.Y *= num;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1}}}", new Object[] { this.X.ToString (), this.Y.ToString () });
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Vector2) {
				flag = this.Equals ((Vector2)obj);
			}
			return flag;
		}
		
		public override Int32 GetHashCode ()
		{
			return (this.X.GetHashCode () + this.Y.GetHashCode ());
		}

		public Boolean IsUnit()
		{
			Single one = 1;

			return RealMaths.IsZero(one - X*X - Y*Y);
		}

		#region IEquatable<Vector2>
		public Boolean Equals (Vector2 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y));
		}
		#endregion

		#region Constants

		static Vector2 zero;
		static Vector2 one;
		static Vector2 unitX;
		static Vector2 unitY;

		static Vector2 ()
		{
			Single temp_one; RealMaths.One(out temp_one);
			Single temp_zero; RealMaths.Zero(out temp_zero);

			zero = new Vector2 ();
			one = new Vector2 (temp_one, temp_one);
			unitX = new Vector2 (temp_one, temp_zero);
			unitY = new Vector2 (temp_zero, temp_one);
		}

		public static Vector2 Zero
		{
			get { return zero; }
		}
		
		public static Vector2 One
		{
			get { return one; }
		}
		
		public static Vector2 UnitX
		{
			get { return unitX; }
		}
		
		public static Vector2 UnitY
		{
			get { return unitY; }
		}
		
		#endregion
		#region Maths

		public static void Distance (ref Vector2 value1, ref Vector2 value2, out Single result)
		{
			Single num2 = value1.X - value2.X;
			Single num = value1.Y - value2.Y;
			Single num3 = (num2 * num2) + (num * num);
			result = RealMaths.Sqrt (num3);
		}

		public static void DistanceSquared (ref Vector2 value1, ref Vector2 value2, out Single result)
		{
			Single num2 = value1.X - value2.X;
			Single num = value1.Y - value2.Y;
			result = (num2 * num2) + (num * num);
		}

		public static void Dot (ref Vector2 value1, ref Vector2 value2, out Single result)
		{
			result = (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		public static void PerpDot (ref Vector2 value1, ref Vector2 value2, out Single result)
		{
			result = (value1.X * value2.Y - value1.Y * value2.X);
		}

		public static void Perpendicular (ref Vector2 value, out Vector2 result)
		{
			result = new Vector2 (-value.X, value.Y);
		}

		public static void Normalise (ref Vector2 value, out Vector2 result)
		{
			Single one = 1;

			Single num2 = (value.X * value.X) + (value.Y * value.Y);
			Single num = one / (RealMaths.Sqrt (num2));
			result.X = value.X * num;
			result.Y = value.Y * num;
		}

		public static void Reflect (ref Vector2 vector, ref Vector2 normal, out Vector2 result)
		{
			Single two = 2;

			Single num = (vector.X * normal.X) + (vector.Y * normal.Y);
			result.X = vector.X - ((two * num) * normal.X);
			result.Y = vector.Y - ((two * num) * normal.Y);
		}
		
		public static void Transform (ref Vector2 position, ref Matrix44 matrix, out Vector2 result)
		{
			Single num2 = ((position.X * matrix.M11) + (position.Y * matrix.M21)) + matrix.M41;
			Single num = ((position.X * matrix.M12) + (position.Y * matrix.M22)) + matrix.M42;
			result.X = num2;
			result.Y = num;
		}
		
		public static void TransformNormal (ref Vector2 normal, ref Matrix44 matrix, out Vector2 result)
		{
			Single num2 = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
			Single num = (normal.X * matrix.M12) + (normal.Y * matrix.M22);
			result.X = num2;
			result.Y = num;
		}
		
		public static void Transform (ref Vector2 value, ref Quaternion rotation, out Vector2 result)
		{
			Single one = 1;

			Single num10 = rotation.X + rotation.X;
			Single num5 = rotation.Y + rotation.Y;
			Single num4 = rotation.Z + rotation.Z;
			Single num3 = rotation.W * num4;
			Single num9 = rotation.X * num10;
			Single num2 = rotation.X * num5;
			Single num8 = rotation.Y * num5;
			Single num = rotation.Z * num4;
			Single num7 = (value.X * ((one - num8) - num)) + (value.Y * (num2 - num3));
			Single num6 = (value.X * (num2 + num3)) + (value.Y * ((one - num9) - num));
			result.X = num7;
			result.Y = num6;
		}
		
		#endregion
		#region Operators

		public static Vector2 operator - (Vector2 value)
		{
			Vector2 vector;
			vector.X = -value.X;
			vector.Y = -value.Y;
			return vector;
		}
		
		public static Boolean operator == (Vector2 value1, Vector2 value2)
		{
			return ((value1.X == value2.X) && (value1.Y == value2.Y));
		}
		
		public static Boolean operator != (Vector2 value1, Vector2 value2)
		{
			if (value1.X == value2.X) {
				return !(value1.Y == value2.Y);
			}
			return true;
		}

		public static Vector2 operator + (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			return vector;
		}

		public static Vector2 operator - (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			return vector;
		}

		public static Vector2 operator * (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X * value2.X;
			vector.Y = value1.Y * value2.Y;
			return vector;
		}
		
		public static Vector2 operator * (Vector2 value, Single scaleFactor)
		{
			Vector2 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}
		
		public static Vector2 operator * (Single scaleFactor, Vector2 value)
		{
			Vector2 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}

		public static Vector2 operator / (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			return vector;
		}
		
		public static Vector2 operator / (Vector2 value1, Single divider)
		{
			Vector2 vector;
			Single one = 1;
			Single num = one / divider;
			vector.X = value1.X * num;
			vector.Y = value1.Y * num;
			return vector;
		}
		
		public static void Negate (ref Vector2 value, out Vector2 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		public static void Add (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		public static void Subtract (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		public static void Multiply (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}
		
		public static void Multiply (ref Vector2 value1, Single scaleFactor, out Vector2 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		public static void Divide (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		public static void Divide (ref Vector2 value1, Single divider, out Vector2 result)
		{
			Single one = 1;
			Single num = one / divider;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
		}
		
		#endregion
		#region Splines

		public static void Barycentric (ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, Single amount1, Single amount2, out Vector2 result)
		{
			result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
			result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
		}

		public static void SmoothStep (ref Vector2 value1, ref Vector2 value2, Single amount, out Vector2 result)
		{
			Single zero = 0;
			Single one = 1;
			Single two = 2;
			Single three = 3;

			amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
			amount = (amount * amount) * (three - (two * amount));
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
		}
		
		public static void CatmullRom (ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, Single amount, out Vector2 result)
		{
			Single half; RealMaths.Half(out half);
			Single two = 2;
			Single three = 3;
			Single four = 4;
			Single five = 5;

			Single num = amount * amount;
			Single num2 = amount * num;
			result.X = half * ((((two * value2.X) + ((-value1.X + value3.X) * amount)) + (((((two * value1.X) - (five * value2.X)) + (four * value3.X)) - value4.X) * num)) + ((((-value1.X + (three * value2.X)) - (three * value3.X)) + value4.X) * num2));
			result.Y = half * ((((two * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((two * value1.Y) - (five * value2.Y)) + (four * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (three * value2.Y)) - (three * value3.Y)) + value4.Y) * num2));
		}

		public static void Hermite (ref Vector2 value1, ref Vector2 tangent1, ref Vector2 value2, ref Vector2 tangent2, Single amount, out Vector2 result)
		{
			Single one = 1;
			Single two = 2;
			Single three = 3;

			Single num = amount * amount;
			Single num2 = amount * num;
			Single num6 = ((two * num2) - (three * num)) + one;
			Single num5 = (-two * num2) + (three * num);
			Single num4 = (num2 - (two * num)) + amount;
			Single num3 = num2 - num;
			result.X = (((value1.X * num6) + (value2.X * num5)) + (tangent1.X * num4)) + (tangent2.X * num3);
			result.Y = (((value1.Y * num6) + (value2.Y * num5)) + (tangent1.Y * num4)) + (tangent2.Y * num3);
		}
		
		#endregion
		#region Utilities

		public static void Min (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
		}

		public static void Max (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
		}

		public static void Clamp (ref Vector2 value1, ref Vector2 min, ref Vector2 max, out Vector2 result)
		{
			Single x = value1.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;
			Single y = value1.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;
			result.X = x;
			result.Y = y;
		}
		
		public static void Lerp (ref Vector2 value1, ref Vector2 value2, Single amount, out Vector2 result)
		{
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
		}
		
		#endregion

	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Vector3 
		: IEquatable<Vector3>
	{
		public Single X;
		public Single Y;
		public Single Z;

		public Vector2 XY
		{
			get
			{
				return new Vector2(X, Y);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}



		public Vector3 (Single x, Single y, Single z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public Vector3 (Single value)
		{
			this.X = this.Y = this.Z = value;
		}
		
		public Vector3 (Vector2 value, Single z)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1} Z:{2}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString () });
		}

		public Boolean Equals (Vector3 other)
		{
			return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Vector3) {
				flag = this.Equals ((Vector3)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return ((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ());
		}

		public Single Length ()
		{
			Single num = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
			return RealMaths.Sqrt (num);
		}

		public Single LengthSquared ()
		{
			return (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
		}


		public void Normalise ()
		{
			Single one = 1;
			Single num2 = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
			Single num = one / RealMaths.Sqrt (num2);
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
		}

		public Boolean IsUnit()
		{
			Single one = 1;

			return RealMaths.IsZero(one - X*X - Y*Y - Z*Z);
		}

		#region Constants

		static Vector3 _zero;
		static Vector3 _one;
		static Vector3 _half;
		static Vector3 _unitX;
		static Vector3 _unitY;
		static Vector3 _unitZ;
		static Vector3 _up;
		static Vector3 _down;
		static Vector3 _right;
		static Vector3 _left;
		static Vector3 _forward;
		static Vector3 _backward;

		static Vector3 ()
		{
			Single temp_one; RealMaths.One(out temp_one);
			Single temp_half; RealMaths.Half(out temp_half);
			Single temp_zero; RealMaths.Zero(out temp_zero);

			_zero = new Vector3 ();
			_one = new Vector3 (temp_one, temp_one, temp_one);
			_half = new Vector3(temp_half, temp_half, temp_half);
			_unitX = new Vector3 (temp_one, temp_zero, temp_zero);
			_unitY = new Vector3 (temp_zero, temp_one, temp_zero);
			_unitZ = new Vector3 (temp_zero, temp_zero, temp_one);
			_up = new Vector3 (temp_zero, temp_one, temp_zero);
			_down = new Vector3 (temp_zero, -temp_one, temp_zero);
			_right = new Vector3 (temp_one, temp_zero, temp_zero);
			_left = new Vector3 (-temp_one, temp_zero, temp_zero);
			_forward = new Vector3 (temp_zero, temp_zero, -temp_one);
			_backward = new Vector3 (temp_zero, temp_zero, temp_one);
		}
		
		public static Vector3 Zero {
			get {
				return _zero;
			}
		}
		
		public static Vector3 One {
			get {
				return _one;
			}
		}
		
		public static Vector3 Half {
			get {
				return _half;
			}
		}
		
		public static Vector3 UnitX {
			get {
				return _unitX;
			}
		}
		
		public static Vector3 UnitY {
			get {
				return _unitY;
			}
		}
		
		public static Vector3 UnitZ {
			get {
				return _unitZ;
			}
		}
		
		public static Vector3 Up {
			get {
				return _up;
			}
		}
		
		public static Vector3 Down {
			get {
				return _down;
			}
		}
		
		public static Vector3 Right {
			get {
				return _right;
			}
		}
		
		public static Vector3 Left {
			get {
				return _left;
			}
		}
		
		public static Vector3 Forward {
			get {
				return _forward;
			}
		}
		
		public static Vector3 Backward {
			get {
				return _backward;
			}
		}
		
		#endregion
		#region Maths

		public static void Distance (ref Vector3 value1, ref Vector3 value2, out Single result)
		{
			Single num3 = value1.X - value2.X;
			Single num2 = value1.Y - value2.Y;
			Single num = value1.Z - value2.Z;
			Single num4 = ((num3 * num3) + (num2 * num2)) + (num * num);
			result = RealMaths.Sqrt (num4);
		}
		
		public static void DistanceSquared (ref Vector3 value1, ref Vector3 value2, out Single result)
		{
			Single num3 = value1.X - value2.X;
			Single num2 = value1.Y - value2.Y;
			Single num = value1.Z - value2.Z;
			result = ((num3 * num3) + (num2 * num2)) + (num * num);
		}

		public static void Dot (ref Vector3 vector1, ref Vector3 vector2, out Single result)
		{
			result = ((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z);
		}

		public static void Normalise (ref Vector3 value, out Vector3 result)
		{
			Single one = 1;

			Single num2 = ((value.X * value.X) + (value.Y * value.Y)) + (value.Z * value.Z);
			Single num = one / RealMaths.Sqrt (num2);
			result.X = value.X * num;
			result.Y = value.Y * num;
			result.Z = value.Z * num;
		}

		public static void Cross (ref Vector3 vector1, ref Vector3 vector2, out Vector3 result)
		{
			Single num3 = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y);
			Single num2 = (vector1.Z * vector2.X) - (vector1.X * vector2.Z);
			Single num = (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
			result.X = num3;
			result.Y = num2;
			result.Z = num;
		}

		public static void Reflect (ref Vector3 vector, ref Vector3 normal, out Vector3 result)
		{
			Single two = 2;

			Single num = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
			result.X = vector.X - ((two * num) * normal.X);
			result.Y = vector.Y - ((two * num) * normal.Y);
			result.Z = vector.Z - ((two * num) * normal.Z);
		}

		public static void Transform (ref Vector3 position, ref Matrix44 matrix, out Vector3 result)
		{
			Single num3 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
			Single num2 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
			Single num = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
			result.X = num3;
			result.Y = num2;
			result.Z = num;
		}
		
		public static void TransformNormal (ref Vector3 normal, ref Matrix44 matrix, out Vector3 result)
		{
			Single num3 = ((normal.X * matrix.M11) + (normal.Y * matrix.M21)) + (normal.Z * matrix.M31);
			Single num2 = ((normal.X * matrix.M12) + (normal.Y * matrix.M22)) + (normal.Z * matrix.M32);
			Single num = ((normal.X * matrix.M13) + (normal.Y * matrix.M23)) + (normal.Z * matrix.M33);
			result.X = num3;
			result.Y = num2;
			result.Z = num;
		}
		
		public static void Transform (ref Vector3 value, ref Quaternion rotation, out Vector3 result)
		{
			Single one = 1;
			Single num12 = rotation.X + rotation.X;
			Single num2 = rotation.Y + rotation.Y;
			Single num = rotation.Z + rotation.Z;
			Single num11 = rotation.W * num12;
			Single num10 = rotation.W * num2;
			Single num9 = rotation.W * num;
			Single num8 = rotation.X * num12;
			Single num7 = rotation.X * num2;
			Single num6 = rotation.X * num;
			Single num5 = rotation.Y * num2;
			Single num4 = rotation.Y * num;
			Single num3 = rotation.Z * num;
			Single num15 = ((value.X * ((one - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
			Single num14 = ((value.X * (num7 + num9)) + (value.Y * ((one - num8) - num3))) + (value.Z * (num4 - num11));
			Single num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((one - num8) - num5));
			result.X = num15;
			result.Y = num14;
			result.Z = num13;
		}
		
		#endregion
		#region Operators

		public static Vector3 operator - (Vector3 value)
		{
			Vector3 vector;
			vector.X = -value.X;
			vector.Y = -value.Y;
			vector.Z = -value.Z;
			return vector;
		}
		
		public static Boolean operator == (Vector3 value1, Vector3 value2)
		{
			return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z));
		}
		
		public static Boolean operator != (Vector3 value1, Vector3 value2)
		{
			if ((value1.X == value2.X) && (value1.Y == value2.Y)) {
				return !(value1.Z == value2.Z);
			}
			return true;
		}
		
		public static Vector3 operator + (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			vector.Z = value1.Z + value2.Z;
			return vector;
		}
		
		public static Vector3 operator - (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			vector.Z = value1.Z - value2.Z;
			return vector;
		}
		
		public static Vector3 operator * (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X * value2.X;
			vector.Y = value1.Y * value2.Y;
			vector.Z = value1.Z * value2.Z;
			return vector;
		}
		
		public static Vector3 operator * (Vector3 value, Single scaleFactor)
		{
			Vector3 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			return vector;
		}
		
		public static Vector3 operator * (Single scaleFactor, Vector3 value)
		{
			Vector3 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			return vector;
		}
		
		public static Vector3 operator / (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			vector.Z = value1.Z / value2.Z;
			return vector;
		}
		
		public static Vector3 operator / (Vector3 value, Single divider)
		{
			Vector3 vector;
			Single one = 1;

			Single num = one / divider;
			vector.X = value.X * num;
			vector.Y = value.Y * num;
			vector.Z = value.Z * num;
			return vector;
		}

		public static void Negate (ref Vector3 value, out Vector3 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
		}

		public static void Add (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
		}

		public static void Subtract (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
		}

		public static void Multiply (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
		}

		public static void Multiply (ref Vector3 value1, Single scaleFactor, out Vector3 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
		}

		public static void Divide (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
		}

		public static void Divide (ref Vector3 value1, Single value2, out Vector3 result)
		{
			Single one = 1;
			Single num = one / value2;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
		}
		
		#endregion
		#region Splines

		public static void Barycentric (ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, Single amount1, Single amount2, out Vector3 result)
		{
			result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
			result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
			result.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
		}
	
		public static void SmoothStep (ref Vector3 value1, ref Vector3 value2, Single amount, out Vector3 result)
		{
			Single zero = 0;
			Single one = 1;
			Single two = 2;
			Single three = 3;

			amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
			amount = (amount * amount) * (three - (two * amount));
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
		}

		public static void CatmullRom (ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4, Single amount, out Vector3 result)
		{
			Single half; RealMaths.Half(out half);
			Single two = 2;
			Single three = 3;
			Single four = 4;
			Single five = 5;

			Single num = amount * amount;
			Single num2 = amount * num;
			result.X = half * ((((two * value2.X) + ((-value1.X + value3.X) * amount)) + (((((two * value1.X) - (five * value2.X)) + (four * value3.X)) - value4.X) * num)) + ((((-value1.X + (three * value2.X)) - (three * value3.X)) + value4.X) * num2));
			result.Y = half * ((((two * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((two * value1.Y) - (five * value2.Y)) + (four * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (three * value2.Y)) - (three * value3.Y)) + value4.Y) * num2));
			result.Z = half * ((((two * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((two * value1.Z) - (five * value2.Z)) + (four * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (three * value2.Z)) - (three * value3.Z)) + value4.Z) * num2));
		}

		public static void Hermite (ref Vector3 value1, ref Vector3 tangent1, ref Vector3 value2, ref Vector3 tangent2, Single amount, out Vector3 result)
		{
			Single one = 1;
			Single two = 2;
			Single three = 3;

			Single num = amount * amount;
			Single num2 = amount * num;
			Single num6 = ((two * num2) - (three * num)) + one;
			Single num5 = (-two * num2) + (three * num);
			Single num4 = (num2 - (two * num)) + amount;
			Single num3 = num2 - num;
			result.X = (((value1.X * num6) + (value2.X * num5)) + (tangent1.X * num4)) + (tangent2.X * num3);
			result.Y = (((value1.Y * num6) + (value2.Y * num5)) + (tangent1.Y * num4)) + (tangent2.Y * num3);
			result.Z = (((value1.Z * num6) + (value2.Z * num5)) + (tangent1.Z * num4)) + (tangent2.Z * num3);
		}
		
		#endregion
		#region Utilities

		public static void Min (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z < value2.Z) ? value1.Z : value2.Z;
		}

		public static void Max (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z > value2.Z) ? value1.Z : value2.Z;
		}
		
		public static void Clamp (ref Vector3 value1, ref Vector3 min, ref Vector3 max, out Vector3 result)
		{
			Single x = value1.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;
			Single y = value1.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;
			Single z = value1.Z;
			z = (z > max.Z) ? max.Z : z;
			z = (z < min.Z) ? min.Z : z;
			result.X = x;
			result.Y = y;
			result.Z = z;
		}

		public static void Lerp (ref Vector3 value1, ref Vector3 value2, Single amount, out Vector3 result)
		{
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
		}
		
		#endregion

	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Vector4 
		: IEquatable<Vector4>
	{
		public Single X;
		public Single Y;
		public Single Z;
		public Single W;

		public Vector3 XYZ
		{
			get
			{
				return new Vector3(X, Y, Z);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
				this.Z = value.Z;
			}
		}



		public Vector4 (Single x, Single y, Single z, Single w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		public Vector4 (Vector2 value, Single z, Single w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
			this.W = w;
		}

		public Vector4 (Vector3 value, Single w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}

		public Vector4 (Single value)
		{
			this.X = this.Y = this.Z = this.W = value;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString (), this.W.ToString () });
		}

		public Boolean Equals (Vector4 other)
		{
			return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Vector4) {
				flag = this.Equals ((Vector4)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ()) + this.W.GetHashCode ());
		}

		public Single Length ()
		{
			Single num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			return RealMaths.Sqrt (num);
		}

		public Single LengthSquared ()
		{
			return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
		}



		public void Normalise ()
		{
			Single one = 1;
			Single num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			Single num = one / RealMaths.Sqrt (num2);
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
			this.W *= num;
		}



		public Boolean IsUnit()
		{
			Single one = 1;

			return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
		}

		#region Constants

		static Vector4 _zero;
		static Vector4 _one;
		static Vector4 _unitX;
		static Vector4 _unitY;
		static Vector4 _unitZ;
		static Vector4 _unitW;

		static Vector4 ()
		{
			Single temp_one; RealMaths.One(out temp_one);
			Single temp_zero; RealMaths.Zero(out temp_zero);

			_zero = new Vector4 ();
			_one = new Vector4 (temp_one, temp_one, temp_one, temp_one);
			_unitX = new Vector4 (temp_one, temp_zero, temp_zero, temp_zero);
			_unitY = new Vector4 (temp_zero, temp_one, temp_zero, temp_zero);
			_unitZ = new Vector4 (temp_zero, temp_zero, temp_one, temp_zero);
			_unitW = new Vector4 (temp_zero, temp_zero, temp_zero, temp_one);
		}

		public static Vector4 Zero {
			get {
				return _zero;
			}
		}
		
		public static Vector4 One {
			get {
				return _one;
			}
		}
		
		public static Vector4 UnitX {
			get {
				return _unitX;
			}
		}
		
		public static Vector4 UnitY {
			get {
				return _unitY;
			}
		}
		
		public static Vector4 UnitZ {
			get {
				return _unitZ;
			}
		}
		
		public static Vector4 UnitW {
			get {
				return _unitW;
			}
		}
		
		#endregion
		#region Maths

		public static void Distance (ref Vector4 value1, ref Vector4 value2, out Single result)
		{
			Single num4 = value1.X - value2.X;
			Single num3 = value1.Y - value2.Y;
			Single num2 = value1.Z - value2.Z;
			Single num = value1.W - value2.W;
			Single num5 = (((num4 * num4) + (num3 * num3)) + (num2 * num2)) + (num * num);
			result = RealMaths.Sqrt (num5);
		}

		public static void DistanceSquared (ref Vector4 value1, ref Vector4 value2, out Single result)
		{
			Single num4 = value1.X - value2.X;
			Single num3 = value1.Y - value2.Y;
			Single num2 = value1.Z - value2.Z;
			Single num = value1.W - value2.W;
			result = (((num4 * num4) + (num3 * num3)) + (num2 * num2)) + (num * num);
		}

		public static void Dot (ref Vector4 vector1, ref Vector4 vector2, out Single result)
		{
			result = (((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z)) + (vector1.W * vector2.W);
		}

		public static void Normalise (ref Vector4 vector, out Vector4 result)
		{
			Single one = 1;
			Single num2 = (((vector.X * vector.X) + (vector.Y * vector.Y)) + (vector.Z * vector.Z)) + (vector.W * vector.W);
			Single num = one / (RealMaths.Sqrt (num2));
			result.X = vector.X * num;
			result.Y = vector.Y * num;
			result.Z = vector.Z * num;
			result.W = vector.W * num;
		}

		public static void Transform (ref Vector2 position, ref Matrix44 matrix, out Vector4 result)
		{
			Single num4 = ((position.X * matrix.M11) + (position.Y * matrix.M21)) + matrix.M41;
			Single num3 = ((position.X * matrix.M12) + (position.Y * matrix.M22)) + matrix.M42;
			Single num2 = ((position.X * matrix.M13) + (position.Y * matrix.M23)) + matrix.M43;
			Single num = ((position.X * matrix.M14) + (position.Y * matrix.M24)) + matrix.M44;
			result.X = num4;
			result.Y = num3;
			result.Z = num2;
			result.W = num;
		}
		
		public static void Transform (ref Vector3 position, ref Matrix44 matrix, out Vector4 result)
		{
			Single num4 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
			Single num3 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
			Single num2 = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
			Single num = (((position.X * matrix.M14) + (position.Y * matrix.M24)) + (position.Z * matrix.M34)) + matrix.M44;
			result.X = num4;
			result.Y = num3;
			result.Z = num2;
			result.W = num;
		}
		
		public static void Transform (ref Vector4 vector, ref Matrix44 matrix, out Vector4 result)
		{
			Single num4 = (((vector.X * matrix.M11) + (vector.Y * matrix.M21)) + (vector.Z * matrix.M31)) + (vector.W * matrix.M41);
			Single num3 = (((vector.X * matrix.M12) + (vector.Y * matrix.M22)) + (vector.Z * matrix.M32)) + (vector.W * matrix.M42);
			Single num2 = (((vector.X * matrix.M13) + (vector.Y * matrix.M23)) + (vector.Z * matrix.M33)) + (vector.W * matrix.M43);
			Single num = (((vector.X * matrix.M14) + (vector.Y * matrix.M24)) + (vector.Z * matrix.M34)) + (vector.W * matrix.M44);
			result.X = num4;
			result.Y = num3;
			result.Z = num2;
			result.W = num;
		}
		
		
		public static void Transform (ref Vector2 value, ref Quaternion rotation, out Vector4 result)
		{
			Single one = 1;
			Single num6 = rotation.X + rotation.X;
			Single num2 = rotation.Y + rotation.Y;
			Single num = rotation.Z + rotation.Z;
			Single num15 = rotation.W * num6;
			Single num14 = rotation.W * num2;
			Single num5 = rotation.W * num;
			Single num13 = rotation.X * num6;
			Single num4 = rotation.X * num2;
			Single num12 = rotation.X * num;
			Single num11 = rotation.Y * num2;
			Single num10 = rotation.Y * num;
			Single num3 = rotation.Z * num;
			Single num9 = (value.X * ((one - num11) - num3)) + (value.Y * (num4 - num5));
			Single num8 = (value.X * (num4 + num5)) + (value.Y * ((one - num13) - num3));
			Single num7 = (value.X * (num12 - num14)) + (value.Y * (num10 + num15));
			result.X = num9;
			result.Y = num8;
			result.Z = num7;
			result.W = one;
		}
		
		public static void Transform (ref Vector3 value, ref Quaternion rotation, out Vector4 result)
		{
			Single one = 1;
			Single num12 = rotation.X + rotation.X;
			Single num2 = rotation.Y + rotation.Y;
			Single num = rotation.Z + rotation.Z;
			Single num11 = rotation.W * num12;
			Single num10 = rotation.W * num2;
			Single num9 = rotation.W * num;
			Single num8 = rotation.X * num12;
			Single num7 = rotation.X * num2;
			Single num6 = rotation.X * num;
			Single num5 = rotation.Y * num2;
			Single num4 = rotation.Y * num;
			Single num3 = rotation.Z * num;
			Single num15 = ((value.X * ((one - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
			Single num14 = ((value.X * (num7 + num9)) + (value.Y * ((one - num8) - num3))) + (value.Z * (num4 - num11));
			Single num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((one - num8) - num5));
			result.X = num15;
			result.Y = num14;
			result.Z = num13;
			result.W = one;
		}
		
		public static void Transform (ref Vector4 value, ref Quaternion rotation, out Vector4 result)
		{
			Single one = 1;
			Single num12 = rotation.X + rotation.X;
			Single num2 = rotation.Y + rotation.Y;
			Single num = rotation.Z + rotation.Z;
			Single num11 = rotation.W * num12;
			Single num10 = rotation.W * num2;
			Single num9 = rotation.W * num;
			Single num8 = rotation.X * num12;
			Single num7 = rotation.X * num2;
			Single num6 = rotation.X * num;
			Single num5 = rotation.Y * num2;
			Single num4 = rotation.Y * num;
			Single num3 = rotation.Z * num;
			Single num15 = ((value.X * ((one - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
			Single num14 = ((value.X * (num7 + num9)) + (value.Y * ((one - num8) - num3))) + (value.Z * (num4 - num11));
			Single num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((one - num8) - num5));
			result.X = num15;
			result.Y = num14;
			result.Z = num13;
			result.W = value.W;
		}
		
		#endregion
		#region Operators

		public static Vector4 operator - (Vector4 value)
		{
			Vector4 vector;
			vector.X = -value.X;
			vector.Y = -value.Y;
			vector.Z = -value.Z;
			vector.W = -value.W;
			return vector;
		}
		
		public static Boolean operator == (Vector4 value1, Vector4 value2)
		{
			return ((((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) && (value1.W == value2.W));
		}
		
		public static Boolean operator != (Vector4 value1, Vector4 value2)
		{
			if (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) {
				return !(value1.W == value2.W);
			}
			return true;
		}
		
		public static Vector4 operator + (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			vector.Z = value1.Z + value2.Z;
			vector.W = value1.W + value2.W;
			return vector;
		}
		
		public static Vector4 operator - (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			vector.Z = value1.Z - value2.Z;
			vector.W = value1.W - value2.W;
			return vector;
		}
		
		public static Vector4 operator * (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X * value2.X;
			vector.Y = value1.Y * value2.Y;
			vector.Z = value1.Z * value2.Z;
			vector.W = value1.W * value2.W;
			return vector;
		}
		
		public static Vector4 operator * (Vector4 value1, Single scaleFactor)
		{
			Vector4 vector;
			vector.X = value1.X * scaleFactor;
			vector.Y = value1.Y * scaleFactor;
			vector.Z = value1.Z * scaleFactor;
			vector.W = value1.W * scaleFactor;
			return vector;
		}
		
		public static Vector4 operator * (Single scaleFactor, Vector4 value1)
		{
			Vector4 vector;
			vector.X = value1.X * scaleFactor;
			vector.Y = value1.Y * scaleFactor;
			vector.Z = value1.Z * scaleFactor;
			vector.W = value1.W * scaleFactor;
			return vector;
		}
		
		public static Vector4 operator / (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			vector.Z = value1.Z / value2.Z;
			vector.W = value1.W / value2.W;
			return vector;
		}
		
		public static Vector4 operator / (Vector4 value1, Single divider)
		{
			Single one = 1;
			Vector4 vector;
			Single num = one / divider;
			vector.X = value1.X * num;
			vector.Y = value1.Y * num;
			vector.Z = value1.Z * num;
			vector.W = value1.W * num;
			return vector;
		}
		
		public static void Negate (ref Vector4 value, out Vector4 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = -value.W;
		}

		public static void Add (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
		}
		
		public static void Subtract (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
		}
		
		public static void Multiply (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
			result.W = value1.W * value2.W;
		}

		public static void Multiply (ref Vector4 value1, Single scaleFactor, out Vector4 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
			result.W = value1.W * scaleFactor;
		}

		public static void Divide (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
			result.W = value1.W / value2.W;
		}
		
		public static void Divide (ref Vector4 value1, Single divider, out Vector4 result)
		{
			Single one = 1;
			Single num = one / divider;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
			result.W = value1.W * num;
		}
		
		#endregion
		#region Splines

		public static void Barycentric (ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, Single amount1, Single amount2, out Vector4 result)
		{
			result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
			result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
			result.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
			result.W = (value1.W + (amount1 * (value2.W - value1.W))) + (amount2 * (value3.W - value1.W));
		}

		public static void SmoothStep (ref Vector4 value1, ref Vector4 value2, Single amount, out Vector4 result)
		{
			Single zero = 0;
			Single one = 1;
			Single two = 2;
			Single three = 3;

			amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
			amount = (amount * amount) * (three - (two * amount));
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
			result.W = value1.W + ((value2.W - value1.W) * amount);
		}

		public static void CatmullRom (ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, ref Vector4 value4, Single amount, out Vector4 result)
		{
			Single half; RealMaths.Half(out half);
			Single two = 2;
			Single three = 3;
			Single four = 4;
			Single five = 5;

			Single num = amount * amount;
			Single num2 = amount * num;
			result.X = half * ((((two * value2.X) + ((-value1.X + value3.X) * amount)) + (((((two * value1.X) - (five * value2.X)) + (four * value3.X)) - value4.X) * num)) + ((((-value1.X + (three * value2.X)) - (three * value3.X)) + value4.X) * num2));
			result.Y = half * ((((two * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((two * value1.Y) - (five * value2.Y)) + (four * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (three * value2.Y)) - (three * value3.Y)) + value4.Y) * num2));
			result.Z = half * ((((two * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((two * value1.Z) - (five * value2.Z)) + (four * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (three * value2.Z)) - (three * value3.Z)) + value4.Z) * num2));
			result.W = half * ((((two * value2.W) + ((-value1.W + value3.W) * amount)) + (((((two * value1.W) - (five * value2.W)) + (four * value3.W)) - value4.W) * num)) + ((((-value1.W + (three * value2.W)) - (three * value3.W)) + value4.W) * num2));
		}

		public static void Hermite (ref Vector4 value1, ref Vector4 tangent1, ref Vector4 value2, ref Vector4 tangent2, Single amount, out Vector4 result)
		{
			Single one = 1;
			Single two = 2;
			Single three = 3;

			Single num = amount * amount;
			Single num6 = amount * num;
			Single num5 = ((two * num6) - (three * num)) + one;
			Single num4 = (-two * num6) + (three * num);
			Single num3 = (num6 - (two * num)) + amount;
			Single num2 = num6 - num;
			result.X = (((value1.X * num5) + (value2.X * num4)) + (tangent1.X * num3)) + (tangent2.X * num2);
			result.Y = (((value1.Y * num5) + (value2.Y * num4)) + (tangent1.Y * num3)) + (tangent2.Y * num2);
			result.Z = (((value1.Z * num5) + (value2.Z * num4)) + (tangent1.Z * num3)) + (tangent2.Z * num2);
			result.W = (((value1.W * num5) + (value2.W * num4)) + (tangent1.W * num3)) + (tangent2.W * num2);
		}
		
		#endregion

		#region Utilities

		public static void Min (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z < value2.Z) ? value1.Z : value2.Z;
			result.W = (value1.W < value2.W) ? value1.W : value2.W;
		}

		public static void Max (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z > value2.Z) ? value1.Z : value2.Z;
			result.W = (value1.W > value2.W) ? value1.W : value2.W;
		}
		
		public static void Clamp (ref Vector4 value1, ref Vector4 min, ref Vector4 max, out Vector4 result)
		{
			Single x = value1.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;
			Single y = value1.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;
			Single z = value1.Z;
			z = (z > max.Z) ? max.Z : z;
			z = (z < min.Z) ? min.Z : z;
			Single w = value1.W;
			w = (w > max.W) ? max.W : w;
			w = (w < min.W) ? min.W : w;
			result.X = x;
			result.Y = y;
			result.Z = z;
			result.W = w;
		}
		
		public static void Lerp (ref Vector4 value1, ref Vector4 value2, Single amount, out Vector4 result)
		{
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
			result.W = value1.W + ((value2.W - value1.W) * amount);
		}
		
		#endregion


	}

}

namespace Sungiant.Abacus.DoublePrecision
{
	public static class GaussianElimination
	{

	}
	public class GjkDistance
	{
		public GjkDistance ()
		{
			for (Int32 i = 0; i < 0x10; i++)
			{
				this.det [i] = new Double[4];
			}
		}

		public Boolean AddSupportPoint (ref Vector3 newPoint)
		{
			Int32 index = (BitsToIndices [this.simplexBits ^ 15] & 7) - 1;

			this.y [index] = newPoint;
			this.yLengthSq [index] = newPoint.LengthSquared ();

			for (Int32 i = BitsToIndices[this.simplexBits]; i != 0; i = i >> 3)
			{
				Int32 num2 = (i & 7) - 1;
				Vector3 vector = this.y [num2] - newPoint;

				this.edges [num2] [index] = vector;
				this.edges [index] [num2] = -vector;
				this.edgeLengthSq [index] [num2] = this.edgeLengthSq [num2] [index] = vector.LengthSquared ();
			}

			this.UpdateDeterminant (index);

			return this.UpdateSimplex (index);
		}

		public void Reset ()
		{
			Double zero = 0;

			this.simplexBits = 0;
			this.maxLengthSq = zero;
		}

		public Vector3 ClosestPoint
		{
			get { return this.closestPoint; }
		}
		
		public Boolean FullSimplex
		{
			get { return (this.simplexBits == 15); }
		}
		
		public Double MaxLengthSquared
		{
			get { return this.maxLengthSq; }
		}

		Vector3 closestPoint;
		Double[][] det = new Double[0x10][];
		Double[][] edgeLengthSq = new Double[][] { new Double[4], new Double[4], new Double[4], new Double[4] };
		Vector3[][] edges = new Vector3[][] { new Vector3[4], new Vector3[4], new Vector3[4], new Vector3[4] };
		Double maxLengthSq;
		Int32 simplexBits;
		Vector3[] y = new Vector3[4];
		Double[] yLengthSq = new Double[4];

		static Int32[] BitsToIndices = new Int32[] { 0, 1, 2, 0x11, 3, 0x19, 0x1a, 0xd1, 4, 0x21, 0x22, 0x111, 0x23, 0x119, 0x11a, 0x8d1 };

		Vector3 ComputeClosestPoint ()
		{
			Double fzero; RealMaths.Zero(out fzero);

			Double num3 = fzero;
			Vector3 zero = Vector3.Zero;

			this.maxLengthSq = fzero;

			for (Int32 i = BitsToIndices[this.simplexBits]; i != 0; i = i >> 3)
			{
				Int32 index = (i & 7) - 1;
				Double num4 = this.det [this.simplexBits] [index];

				num3 += num4;
				zero += (Vector3)(this.y [index] * num4);

				this.maxLengthSq = RealMaths.Max (this.maxLengthSq, this.yLengthSq [index]);
			}

			return (Vector3)(zero / num3);
		}

		Boolean IsSatisfiesRule (Int32 xBits, Int32 yBits)
		{
			Double fzero; RealMaths.Zero(out fzero);

			for (Int32 i = BitsToIndices[yBits]; i != 0; i = i >> 3)
			{
				Int32 index = (i & 7) - 1;
				Int32 num3 = ((Int32)1) << index;

				if ((num3 & xBits) != 0)
				{
					if (this.det [xBits] [index] <= fzero)
					{
						return false;
					}
				}
				else if (this.det [xBits | num3] [index] > fzero)
				{
					return false;
				}
			}

			return true;
		}

		void UpdateDeterminant (Int32 xmIdx)
		{
			Double fone; RealMaths.One(out fone);
			Int32 index = ((Int32)1) << xmIdx;

			this.det [index] [xmIdx] = fone;

			Int32 num14 = BitsToIndices [this.simplexBits];
			Int32 num8 = num14;

			for (Int32 i = 0; num8 != 0; i++)
			{
				Int32 num = (num8 & 7) - 1;
				Int32 num12 = ((int)1) << num;
				Int32 num6 = num12 | index;

				this.det [num6] [num] = Dot (ref this.edges [xmIdx] [num], ref this.y [xmIdx]);
				this.det [num6] [xmIdx] = Dot (ref this.edges [num] [xmIdx], ref this.y [num]);

				Int32 num11 = num14;

				for (Int32 j = 0; j < i; j++)
				{
					int num3 = (num11 & 7) - 1;
					int num5 = ((int)1) << num3;
					int num9 = num6 | num5;
					int num4 = (this.edgeLengthSq [num] [num3] < this.edgeLengthSq [xmIdx] [num3]) ? num : xmIdx;

					this.det [num9] [num3] = 
						(this.det [num6] [num] * Dot (ref this.edges [num4] [num3], ref this.y [num])) + 
						(this.det [num6] [xmIdx] * Dot (ref this.edges [num4] [num3], ref this.y [xmIdx]));

					num4 = (this.edgeLengthSq [num3] [num] < this.edgeLengthSq [xmIdx] [num]) ? num3 : xmIdx;

					this.det [num9] [num] = 
						(this.det [num5 | index] [num3] * Dot (ref this.edges [num4] [num], ref this.y [num3])) + 
						(this.det [num5 | index] [xmIdx] * Dot (ref this.edges [num4] [num], ref this.y [xmIdx]));

					num4 = (this.edgeLengthSq [num] [xmIdx] < this.edgeLengthSq [num3] [xmIdx]) ? num : num3;

					this.det [num9] [xmIdx] = 
						(this.det [num12 | num5] [num3] * Dot (ref this.edges [num4] [xmIdx], ref this.y [num3])) + 
						(this.det [num12 | num5] [num] * Dot (ref this.edges [num4] [xmIdx], ref this.y [num]));

					num11 = num11 >> 3;
				}

				num8 = num8 >> 3;
			}

			if ((this.simplexBits | index) == 15)
			{
				int num2 = 
					(this.edgeLengthSq [1] [0] < this.edgeLengthSq [2] [0]) ? 
					((this.edgeLengthSq [1] [0] < this.edgeLengthSq [3] [0]) ? 1 : 3) : 
					((this.edgeLengthSq [2] [0] < this.edgeLengthSq [3] [0]) ? 2 : 3);

				this.det [15] [0] = 
					((this.det [14] [1] * Dot (ref this.edges [num2] [0], ref this.y [1])) + 
					(this.det [14] [2] * Dot (ref this.edges [num2] [0], ref this.y [2]))) + 
					(this.det [14] [3] * Dot (ref this.edges [num2] [0], ref this.y [3]));

				num2 = 
					(this.edgeLengthSq [0] [1] < this.edgeLengthSq [2] [1]) ? 
					((this.edgeLengthSq [0] [1] < this.edgeLengthSq [3] [1]) ? 0 : 3) : 
					((this.edgeLengthSq [2] [1] < this.edgeLengthSq [3] [1]) ? 2 : 3);

				this.det [15] [1] = 
					((this.det [13] [0] * Dot (ref this.edges [num2] [1], ref this.y [0])) + 
				    (this.det [13] [2] * Dot (ref this.edges [num2] [1], ref this.y [2]))) + 
					(this.det [13] [3] * Dot (ref this.edges [num2] [1], ref this.y [3]));

				num2 = 
					(this.edgeLengthSq [0] [2] < this.edgeLengthSq [1] [2]) ? 
					((this.edgeLengthSq [0] [2] < this.edgeLengthSq [3] [2]) ? 0 : 3) : 
					((this.edgeLengthSq [1] [2] < this.edgeLengthSq [3] [2]) ? 1 : 3);

				this.det [15] [2] = 
					((this.det [11] [0] * Dot (ref this.edges [num2] [2], ref this.y [0])) + 
					(this.det [11] [1] * Dot (ref this.edges [num2] [2], ref this.y [1]))) + 
					(this.det [11] [3] * Dot (ref this.edges [num2] [2], ref this.y [3]));

				num2 = 
					(this.edgeLengthSq [0] [3] < this.edgeLengthSq [1] [3]) ? 
					((this.edgeLengthSq [0] [3] < this.edgeLengthSq [2] [3]) ? 0 : 2) : 
					((this.edgeLengthSq [1] [3] < this.edgeLengthSq [2] [3]) ? 1 : 2);

				this.det [15] [3] = 
					((this.det [7] [0] * Dot (ref this.edges [num2] [3], ref this.y [0])) + 
					(this.det [7] [1] * Dot (ref this.edges [num2] [3], ref this.y [1]))) + 
					(this.det [7] [2] * Dot (ref this.edges [num2] [3], ref this.y [2]));
			}
		}

		Boolean UpdateSimplex (Int32 newIndex)
		{
			Int32 yBits = this.simplexBits | (((Int32)1) << newIndex);

			Int32 xBits = ((Int32)1) << newIndex;

			for (Int32 i = this.simplexBits; i != 0; i--)
			{
				if (((i & yBits) == i) && this.IsSatisfiesRule (i | xBits, yBits))
				{
					this.simplexBits = i | xBits;
					this.closestPoint = this.ComputeClosestPoint ();

					return true;
				}
			}

			Boolean flag = false;

			if (this.IsSatisfiesRule (xBits, yBits))
			{
				this.simplexBits = xBits;
				this.closestPoint = this.y [newIndex];
				this.maxLengthSq = this.yLengthSq [newIndex];

				flag = true;
			}

			return flag;
		}

		static Double Dot (ref Vector3 a, ref Vector3 b)
		{
			return (((a.X * b.X) + (a.Y * b.Y)) + (a.Z * b.Z));
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct BoundingBox 
		: IEquatable<BoundingBox>
	{
		public const int CornerCount = 8;
		public Vector3 Min;
		public Vector3 Max;

		public Vector3[] GetCorners ()
		{
			return new Vector3[] { new Vector3 (this.Min.X, this.Max.Y, this.Max.Z), new Vector3 (this.Max.X, this.Max.Y, this.Max.Z), new Vector3 (this.Max.X, this.Min.Y, this.Max.Z), new Vector3 (this.Min.X, this.Min.Y, this.Max.Z), new Vector3 (this.Min.X, this.Max.Y, this.Min.Z), new Vector3 (this.Max.X, this.Max.Y, this.Min.Z), new Vector3 (this.Max.X, this.Min.Y, this.Min.Z), new Vector3 (this.Min.X, this.Min.Y, this.Min.Z) };
		}

		public BoundingBox (Vector3 min, Vector3 max)
		{
			this.Min = min;
			this.Max = max;
		}

		public Boolean Equals (BoundingBox other)
		{
			return ((this.Min == other.Min) && (this.Max == other.Max));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is BoundingBox) {
				flag = this.Equals ((BoundingBox)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Min.GetHashCode () + this.Max.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Min:{0} Max:{1}}}", new Object[] { this.Min.ToString (), this.Max.ToString () });
		}

		public static void CreateMerged (ref BoundingBox original, ref BoundingBox additional, out BoundingBox result)
		{
			Vector3 vector;
			Vector3 vector2;
			Vector3.Min (ref original.Min, ref additional.Min, out vector2);
			Vector3.Max (ref original.Max, ref additional.Max, out vector);
			result.Min = vector2;
			result.Max = vector;
		}

		public static void CreateFromSphere (ref BoundingSphere sphere, out BoundingBox result)
		{
			result.Min.X = sphere.Center.X - sphere.Radius;
			result.Min.Y = sphere.Center.Y - sphere.Radius;
			result.Min.Z = sphere.Center.Z - sphere.Radius;
			result.Max.X = sphere.Center.X + sphere.Radius;
			result.Max.Y = sphere.Center.Y + sphere.Radius;
			result.Max.Z = sphere.Center.Z + sphere.Radius;
		}

		public static BoundingBox CreateFromPoints (IEnumerable<Vector3> points)
		{
			if (points == null) {
				throw new ArgumentNullException ();
			}
			Boolean flag = false;
			Vector3 vector3 = new Vector3 (Double.MaxValue);
			Vector3 vector2 = new Vector3 (Double.MinValue);
			foreach (Vector3 vector in points) {
				Vector3 vector4 = vector;
				Vector3.Min (ref vector3, ref vector4, out vector3);
				Vector3.Max (ref vector2, ref vector4, out vector2);
				flag = true;
			}
			if (!flag) {
				throw new ArgumentException ("BoundingBoxZeroPoints");
			}
			return new BoundingBox (vector3, vector2);
		}

		public Boolean Intersects (ref BoundingBox box)
		{
			if ((this.Max.X < box.Min.X) || (this.Min.X > box.Max.X)) {
				return false;
			}
			if ((this.Max.Y < box.Min.Y) || (this.Min.Y > box.Max.Y)) {
				return false;
			}
			return ((this.Max.Z >= box.Min.Z) && (this.Min.Z <= box.Max.Z));
		}

		public Boolean Intersects (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			return frustum.Intersects (ref this);
		}

		public PlaneIntersectionType Intersects (ref Plane plane)
		{
			Double zero = 0;

			Vector3 vector;
			Vector3 vector2;
			vector2.X = (plane.Normal.X >= zero) ? this.Min.X : this.Max.X;
			vector2.Y = (plane.Normal.Y >= zero) ? this.Min.Y : this.Max.Y;
			vector2.Z = (plane.Normal.Z >= zero) ? this.Min.Z : this.Max.Z;
			vector.X = (plane.Normal.X >= zero) ? this.Max.X : this.Min.X;
			vector.Y = (plane.Normal.Y >= zero) ? this.Max.Y : this.Min.Y;
			vector.Z = (plane.Normal.Z >= zero) ? this.Max.Z : this.Min.Z;
			Double num = ((plane.Normal.X * vector2.X) + (plane.Normal.Y * vector2.Y)) + (plane.Normal.Z * vector2.Z);
			if ((num + plane.D) > zero) {
				return PlaneIntersectionType.Front;
			}
			num = ((plane.Normal.X * vector.X) + (plane.Normal.Y * vector.Y)) + (plane.Normal.Z * vector.Z);
			if ((num + plane.D) < zero) {
				return PlaneIntersectionType.Back;
			}
			return PlaneIntersectionType.Intersecting;
		}

		public Double? Intersects (ref Ray ray)
		{
			Double epsilon; RealMaths.Epsilon(out epsilon);

			Double zero = 0;
			Double one = 1;

			Double num = zero;
			Double maxValue = Double.MaxValue;
			if (RealMaths.Abs (ray.Direction.X) < epsilon) {
				if ((ray.Position.X < this.Min.X) || (ray.Position.X > this.Max.X)) {
					return null;
				}
			} else {
				Double num11 = one / ray.Direction.X;
				Double num8 = (this.Min.X - ray.Position.X) * num11;
				Double num7 = (this.Max.X - ray.Position.X) * num11;
				if (num8 > num7) {
					Double num14 = num8;
					num8 = num7;
					num7 = num14;
				}
				num = RealMaths.Max (num8, num);
				maxValue = RealMaths.Min (num7, maxValue);
				if (num > maxValue) {
					return null;
				}
			}
			if (RealMaths.Abs (ray.Direction.Y) < epsilon) {
				if ((ray.Position.Y < this.Min.Y) || (ray.Position.Y > this.Max.Y)) {
					return null;
				}
			} else {
				Double num10 = one / ray.Direction.Y;
				Double num6 = (this.Min.Y - ray.Position.Y) * num10;
				Double num5 = (this.Max.Y - ray.Position.Y) * num10;
				if (num6 > num5) {
					Double num13 = num6;
					num6 = num5;
					num5 = num13;
				}
				num = RealMaths.Max (num6, num);
				maxValue = RealMaths.Min (num5, maxValue);
				if (num > maxValue) {
					return null;
				}
			}
			

			if (RealMaths.Abs (ray.Direction.Z) < epsilon) {
				if ((ray.Position.Z < this.Min.Z) || (ray.Position.Z > this.Max.Z)) {
					return null;
				}
			} else {
				Double num9 = one / ray.Direction.Z;
				Double num4 = (this.Min.Z - ray.Position.Z) * num9;
				Double num3 = (this.Max.Z - ray.Position.Z) * num9;
				if (num4 > num3) {
					Double num12 = num4;
					num4 = num3;
					num3 = num12;
				}
				num = RealMaths.Max (num4, num);
				maxValue = RealMaths.Min (num3, maxValue);
				if (num > maxValue) {
					return null;
				}
			}
			return new Double? (num);
		}

		public Boolean Intersects (ref BoundingSphere sphere)
		{
			Double num;
			Vector3 vector;
			Vector3.Clamp (ref sphere.Center, ref this.Min, ref this.Max, out vector);
			Vector3.DistanceSquared (ref sphere.Center, ref vector, out num);
			return (num <= (sphere.Radius * sphere.Radius));
		}

		public ContainmentType Contains (ref BoundingBox box)
		{
			if ((this.Max.X < box.Min.X) || (this.Min.X > box.Max.X)) {
				return ContainmentType.Disjoint;
			}
			if ((this.Max.Y < box.Min.Y) || (this.Min.Y > box.Max.Y)) {
				return ContainmentType.Disjoint;
			}
			if ((this.Max.Z < box.Min.Z) || (this.Min.Z > box.Max.Z)) {
				return ContainmentType.Disjoint;
			}
			if ((((this.Min.X <= box.Min.X) && (box.Max.X <= this.Max.X)) && ((this.Min.Y <= box.Min.Y) && (box.Max.Y <= this.Max.Y))) && ((this.Min.Z <= box.Min.Z) && (box.Max.Z <= this.Max.Z))) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		public ContainmentType Contains (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			if (!frustum.Intersects (ref this)) {
				return ContainmentType.Disjoint;
			}

			for (Int32 i = 0; i < frustum.cornerArray.Length; ++i) {
				Vector3 vector = frustum.cornerArray[i];
				if (this.Contains (ref vector) == ContainmentType.Disjoint) {
					return ContainmentType.Intersects;
				}
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref Vector3 point)
		{
			if ((((this.Min.X <= point.X) && (point.X <= this.Max.X)) && ((this.Min.Y <= point.Y) && (point.Y <= this.Max.Y))) && ((this.Min.Z <= point.Z) && (point.Z <= this.Max.Z))) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Disjoint;
		}

		public ContainmentType Contains (ref BoundingSphere sphere)
		{
			Double num2;
			Vector3 vector;
			Vector3.Clamp (ref sphere.Center, ref this.Min, ref this.Max, out vector);
			Vector3.DistanceSquared (ref sphere.Center, ref vector, out num2);
			Double radius = sphere.Radius;
			if (num2 > (radius * radius)) {
				return ContainmentType.Disjoint;
			}
			if (((((this.Min.X + radius) <= sphere.Center.X) && (sphere.Center.X <= (this.Max.X - radius))) && (((this.Max.X - this.Min.X) > radius) && ((this.Min.Y + radius) <= sphere.Center.Y))) && (((sphere.Center.Y <= (this.Max.Y - radius)) && ((this.Max.Y - this.Min.Y) > radius)) && ((((this.Min.Z + radius) <= sphere.Center.Z) && (sphere.Center.Z <= (this.Max.Z - radius))) && ((this.Max.X - this.Min.X) > radius)))) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		internal void SupportMapping (ref Vector3 v, out Vector3 result)
		{
			Double zero = 0;

			result.X = (v.X >= zero) ? this.Max.X : this.Min.X;
			result.Y = (v.Y >= zero) ? this.Max.Y : this.Min.Y;
			result.Z = (v.Z >= zero) ? this.Max.Z : this.Min.Z;
		}

		public static Boolean operator == (BoundingBox a, BoundingBox b)
		{
			return a.Equals (b);
		}

		public static Boolean operator != (BoundingBox a, BoundingBox b)
		{
			if (!(a.Min != b.Min)) {
				return (a.Max != b.Max);
			}
			return true;
		}
	}
	public class BoundingFrustum 
		: IEquatable<BoundingFrustum>
	{
		const int BottomPlaneIndex = 5;

		internal Vector3[] cornerArray;

		public const int CornerCount = 8;

		const int FarPlaneIndex = 1;

		GjkDistance gjk;

		const int LeftPlaneIndex = 2;

		Matrix44 matrix;

		const int NearPlaneIndex = 0;

		const int NumPlanes = 6;

		Plane[] planes;

		const int RightPlaneIndex = 3;

		const int TopPlaneIndex = 4;

		BoundingFrustum ()
		{
			this.planes = new Plane[6];
			this.cornerArray = new Vector3[8];
		}

		public BoundingFrustum (Matrix44 value)
		{
			this.planes = new Plane[6];
			this.cornerArray = new Vector3[8];
			this.SetMatrix (ref value);
		}

		static Vector3 ComputeIntersection (ref Plane plane, ref Ray ray)
		{
			Double planeNormDotRayPos;
			Double planeNormDotRayDir;

			Vector3.Dot (ref plane.Normal, ref ray.Position, out planeNormDotRayPos);
			Vector3.Dot (ref plane.Normal, ref ray.Direction, out planeNormDotRayDir);

			Double num = (-plane.D - planeNormDotRayPos) / planeNormDotRayDir;

			return (ray.Position + (ray.Direction * num));
		}

		static Ray ComputeIntersectionLine (ref Plane p1, ref Plane p2)
		{
			Ray ray = new Ray ();

			Vector3.Cross (ref p1.Normal, ref p2.Normal, out ray.Direction);

			Double num = ray.Direction.LengthSquared ();

			Vector3 a = (-p1.D * p2.Normal) + (p2.D * p1.Normal);

			Vector3 cross;

			Vector3.Cross (ref a, ref ray.Direction, out cross);

			ray.Position =  cross / num;

			return ray;
		}

		public ContainmentType Contains (ref BoundingBox box)
		{
			Boolean flag = false;
			for(Int32 i = 0; i < this.planes.Length; ++i)
			{
				Plane plane = this.planes[i];
				switch (box.Intersects (ref plane)) {
				case PlaneIntersectionType.Front:
					return ContainmentType.Disjoint;

				case PlaneIntersectionType.Intersecting:
					flag = true;
					break;
				}
			}
			if (!flag) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		public ContainmentType Contains (ref BoundingFrustum frustum)
		{
			if (frustum == null) {
				throw new ArgumentNullException ("frustum");
			}
			ContainmentType disjoint = ContainmentType.Disjoint;
			if (this.Intersects (ref frustum)) {
				disjoint = ContainmentType.Contains;
				for (int i = 0; i < this.cornerArray.Length; i++) {
					if (this.Contains (ref frustum.cornerArray [i]) == ContainmentType.Disjoint) {
						return ContainmentType.Intersects;
					}
				}
			}
			return disjoint;
		}

		public ContainmentType Contains (ref BoundingSphere sphere)
		{
			Vector3 center = sphere.Center;
			Double radius = sphere.Radius;
			int num2 = 0;
			foreach (Plane plane in this.planes) {
				Double num5 = ((plane.Normal.X * center.X) + (plane.Normal.Y * center.Y)) + (plane.Normal.Z * center.Z);
				Double num3 = num5 + plane.D;
				if (num3 > radius) {
					return ContainmentType.Disjoint;
				}
				if (num3 < -radius) {
					num2++;
				}
			}
			if (num2 != 6) {
				return ContainmentType.Intersects;
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref Vector3 point)
		{
			Double epsilon; RealMaths.FromString("0.00001", out epsilon);

			foreach (Plane plane in this.planes) {
				Double num2 = (((plane.Normal.X * point.X) + (plane.Normal.Y * point.Y)) + (plane.Normal.Z * point.Z)) + plane.D;
				if (num2 > epsilon) {
					return ContainmentType.Disjoint;
				}
			}
			return ContainmentType.Contains;
		}

		public Boolean Equals (BoundingFrustum other)
		{
			if (other == null) {
				return false;
			}
			return (this.matrix == other.matrix);
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			BoundingFrustum frustum = obj as BoundingFrustum;
			if (frustum != null) {
				flag = this.matrix == frustum.matrix;
			}
			return flag;
		}

		public Vector3[] GetCorners ()
		{
			return (Vector3[])this.cornerArray.Clone ();
		}

		public override Int32 GetHashCode ()
		{
			return this.matrix.GetHashCode ();
		}

		public Boolean Intersects (ref BoundingBox box)
		{
			Boolean flag;
			this.Intersects (ref box, out flag);
			return flag;
		}

		void Intersects (ref BoundingBox box, out Boolean result)
		{
			Double epsilon; RealMaths.FromString("0.00001", out epsilon);
			Double zero = 0;
			Double four = 4;

			Vector3 closestPoint;
			Vector3 vector2;
			Vector3 vector3;
			Vector3 vector4;
			Vector3 vector5;
			if (this.gjk == null) {
				this.gjk = new GjkDistance ();
			}
			this.gjk.Reset ();
			Vector3.Subtract (ref this.cornerArray [0], ref box.Min, out closestPoint);
			if (closestPoint.LengthSquared () < epsilon) {
				Vector3.Subtract (ref this.cornerArray [0], ref box.Max, out closestPoint);
			}
			Double maxValue = Double.MaxValue;
			Double num3 = zero;
			result = false;
		Label_006D:
			vector5.X = -closestPoint.X;
			vector5.Y = -closestPoint.Y;
			vector5.Z = -closestPoint.Z;
			this.SupportMapping (ref vector5, out vector4);
			box.SupportMapping (ref closestPoint, out vector3);
			Vector3.Subtract (ref vector4, ref vector3, out vector2);
			Double num4 = ((closestPoint.X * vector2.X) + (closestPoint.Y * vector2.Y)) + (closestPoint.Z * vector2.Z);
			if (num4 <= zero) {
				this.gjk.AddSupportPoint (ref vector2);
				closestPoint = this.gjk.ClosestPoint;
				Double num2 = maxValue;
				maxValue = closestPoint.LengthSquared ();
				if ((num2 - maxValue) > (epsilon * num2)) {
					num3 = four * epsilon * this.gjk.MaxLengthSquared;
					if (!this.gjk.FullSimplex && (maxValue >= num3)) {
						goto Label_006D;
					}
					result = true;
				}
			}
		}

		public Boolean Intersects (ref BoundingFrustum frustum)
		{
			Double epsilon; RealMaths.FromString("0.00001", out epsilon);
			Double zero = 0;
			Double four = 4;

			Vector3 closestPoint;
			if (frustum == null) {
				throw new ArgumentNullException ("frustum");
			}
			if (this.gjk == null) {
				this.gjk = new GjkDistance ();
			}
			this.gjk.Reset ();
			Vector3.Subtract (ref this.cornerArray [0], ref frustum.cornerArray [0], out closestPoint);
			if (closestPoint.LengthSquared () < epsilon) {
				Vector3.Subtract (ref this.cornerArray [0], ref frustum.cornerArray [1], out closestPoint);
			}
			Double maxValue = Double.MaxValue;
			Double num3 = zero;
			do {
				Vector3 vector2;
				Vector3 vector3;
				Vector3 vector4;
				Vector3 vector5;
				vector5.X = -closestPoint.X;
				vector5.Y = -closestPoint.Y;
				vector5.Z = -closestPoint.Z;
				this.SupportMapping (ref vector5, out vector4);
				frustum.SupportMapping (ref closestPoint, out vector3);
				Vector3.Subtract (ref vector4, ref vector3, out vector2);
				Double num4 = ((closestPoint.X * vector2.X) + (closestPoint.Y * vector2.Y)) + (closestPoint.Z * vector2.Z);
				if (num4 > zero) {
					return false;
				}
				this.gjk.AddSupportPoint (ref vector2);
				closestPoint = this.gjk.ClosestPoint;
				Double num2 = maxValue;
				maxValue = closestPoint.LengthSquared ();
				num3 = four * epsilon * this.gjk.MaxLengthSquared;
				if ((num2 - maxValue) <= (epsilon * num2)) {
					return false;
				}
			} while (!this.gjk.FullSimplex && (maxValue >= num3));
			return true;
		}

		public Boolean Intersects (ref BoundingSphere sphere)
		{
			Boolean flag;
			this.Intersects (ref sphere, out flag);
			return flag;
		}

		void Intersects (ref BoundingSphere sphere, out Boolean result)
		{
			Double zero = 0;
			Double epsilon; RealMaths.FromString("0.00001", out epsilon);
			Double four = 4;

			Vector3 unitX;
			Vector3 vector2;
			Vector3 vector3;
			Vector3 vector4;
			Vector3 vector5;
			if (this.gjk == null) {
				this.gjk = new GjkDistance ();
			}
			this.gjk.Reset ();
			Vector3.Subtract (ref this.cornerArray [0], ref sphere.Center, out unitX);
			if (unitX.LengthSquared () < epsilon) {
				unitX = Vector3.UnitX;
			}
			Double maxValue = Double.MaxValue;
			Double num3 = zero;
			result = false;
		Label_005A:
			vector5.X = -unitX.X;
			vector5.Y = -unitX.Y;
			vector5.Z = -unitX.Z;
			this.SupportMapping (ref vector5, out vector4);
			sphere.SupportMapping (ref unitX, out vector3);
			Vector3.Subtract (ref vector4, ref vector3, out vector2);
			Double num4 = ((unitX.X * vector2.X) + (unitX.Y * vector2.Y)) + (unitX.Z * vector2.Z);
			if (num4 <= zero) {
				this.gjk.AddSupportPoint (ref vector2);
				unitX = this.gjk.ClosestPoint;
				Double num2 = maxValue;
				maxValue = unitX.LengthSquared ();
				if ((num2 - maxValue) > (epsilon * num2)) {
					num3 = four * epsilon * this.gjk.MaxLengthSquared;
					if (!this.gjk.FullSimplex && (maxValue >= num3)) {
						goto Label_005A;
					}
					result = true;
				}
			}
		}

		public PlaneIntersectionType Intersects (ref Plane plane)
		{
			Double zero = 0;

			int num = 0;
			for (int i = 0; i < 8; i++) {
				Double num3;
				Vector3.Dot (ref this.cornerArray [i], ref plane.Normal, out num3);
				if ((num3 + plane.D) > zero) {
					num |= 1;
				} else {
					num |= 2;
				}
				if (num == 3) {
					return PlaneIntersectionType.Intersecting;
				}
			}
			if (num != 1) {
				return PlaneIntersectionType.Back;
			}
			return PlaneIntersectionType.Front;
		}

		public Double? Intersects (ref Ray ray)
		{
			Double? nullable;
			this.Intersects (ref ray, out nullable);
			return nullable;
		}

		void Intersects (ref Ray ray, out Double? result)
		{
			Double epsilon; RealMaths.FromString("0.00001", out epsilon);
			Double zero = 0;

			ContainmentType type = this.Contains (ref ray.Position);
			if (type == ContainmentType.Contains) {
				result = zero;
			} else {
				Double minValue = Double.MinValue;
				Double maxValue = Double.MaxValue;
				result = zero;
				foreach (Plane plane in this.planes) {
					Double num3;
					Double num6;
					Vector3 normal = plane.Normal;
					Vector3.Dot (ref ray.Direction, ref normal, out num6);
					Vector3.Dot (ref ray.Position, ref normal, out num3);
					num3 += plane.D;
					if (RealMaths.Abs (num6) < epsilon) {
						if (num3 > zero) {
							return;
						}
					} else {
						Double num = -num3 / num6;
						if (num6 < zero) {
							if (num > maxValue) {
								return;
							}
							if (num > minValue) {
								minValue = num;
							}
						} else {
							if (num < minValue) {
								return;
							}
							if (num < maxValue) {
								maxValue = num;
							}
						}
					}
				}
				Double num7 = (minValue >= zero) ? minValue : maxValue;
				if (num7 >= zero) {
					result = new Double? (num7);
				}
			}
		}

		public static Boolean operator == (BoundingFrustum a, BoundingFrustum b)
		{
			return Object.Equals (a, b);
		}

		public static Boolean operator != (BoundingFrustum a, BoundingFrustum b)
		{
			return !Object.Equals (a, b);
		}

		void SetMatrix (ref Matrix44 value)
		{
			this.matrix = value;

			this.planes [2].Normal.X = -value.M14 - value.M11;
			this.planes [2].Normal.Y = -value.M24 - value.M21;
			this.planes [2].Normal.Z = -value.M34 - value.M31;
			this.planes [2].D = -value.M44 - value.M41;

			this.planes [3].Normal.X = -value.M14 + value.M11;
			this.planes [3].Normal.Y = -value.M24 + value.M21;
			this.planes [3].Normal.Z = -value.M34 + value.M31;
			this.planes [3].D = -value.M44 + value.M41;

			this.planes [4].Normal.X = -value.M14 + value.M12;
			this.planes [4].Normal.Y = -value.M24 + value.M22;
			this.planes [4].Normal.Z = -value.M34 + value.M32;
			this.planes [4].D = -value.M44 + value.M42;

			this.planes [5].Normal.X = -value.M14 - value.M12;
			this.planes [5].Normal.Y = -value.M24 - value.M22;
			this.planes [5].Normal.Z = -value.M34 - value.M32;
			this.planes [5].D = -value.M44 - value.M42;

			this.planes [0].Normal.X = -value.M13;
			this.planes [0].Normal.Y = -value.M23;
			this.planes [0].Normal.Z = -value.M33;
			this.planes [0].D = -value.M43;

			this.planes [1].Normal.X = -value.M14 + value.M13;
			this.planes [1].Normal.Y = -value.M24 + value.M23;
			this.planes [1].Normal.Z = -value.M34 + value.M33;
			this.planes [1].D = -value.M44 + value.M43;

			for (int i = 0; i < 6; i++) {
				Double num2 = this.planes [i].Normal.Length ();
				this.planes [i].Normal = (Vector3)(this.planes [i].Normal / num2);
				this.planes [i].D /= num2;
			}

			Ray ray = ComputeIntersectionLine (ref this.planes [0], ref this.planes [2]);

			this.cornerArray [0] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [3] = ComputeIntersection (ref this.planes [5], ref ray);

			ray = ComputeIntersectionLine (ref this.planes [3], ref this.planes [0]);

			this.cornerArray [1] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [2] = ComputeIntersection (ref this.planes [5], ref ray);

			ray = ComputeIntersectionLine (ref this.planes [2], ref this.planes [1]);

			this.cornerArray [4] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [7] = ComputeIntersection (ref this.planes [5], ref ray);

			ray = ComputeIntersectionLine (ref this.planes [1], ref this.planes [3]);

			this.cornerArray [5] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [6] = ComputeIntersection (ref this.planes [5], ref ray);
		}

		internal void SupportMapping (ref Vector3 v, out Vector3 result)
		{
			Double num3;

			int index = 0;

			Vector3.Dot (ref this.cornerArray [0], ref v, out num3);

			for (int i = 1; i < this.cornerArray.Length; i++)
			{
				Double num2;

				Vector3.Dot (ref this.cornerArray [i], ref v, out num2);

				if (num2 > num3)
				{
					index = i;
					num3 = num2;
				}
			}

			result = this.cornerArray [index];
		}

		public override String ToString ()
		{
			return string.Format ("{{Near:{0} Far:{1} Left:{2} Right:{3} Top:{4} Bottom:{5}}}", new Object[] { this.Near.ToString (), this.Far.ToString (), this.Left.ToString (), this.Right.ToString (), this.Top.ToString (), this.Bottom.ToString () });
		}

		// Properties
		public Plane Bottom
		{
			get
			{
				return this.planes [5];
			}
		}

		public Plane Far {
			get {
				return this.planes [1];
			}
		}

		public Plane Left {
			get {
				return this.planes [2];
			}
		}

		public Matrix44 Matrix {
			get {
				return this.matrix;
			}
			set {
				this.SetMatrix (ref value);
			}
		}

		public Plane Near {
			get {
				return this.planes [0];
			}
		}

		public Plane Right {
			get {
				return this.planes [3];
			}
		}

		public Plane Top {
			get {
				return this.planes [4];
			}
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct BoundingSphere 
		: IEquatable<BoundingSphere>
	{
		public Vector3 Center;
		public Double Radius;

		public BoundingSphere (Vector3 center, Double radius)
		{
			Double zero = 0;

			if (radius < zero) {
				throw new ArgumentException ("NegativeRadius");
			}
			this.Center = center;
			this.Radius = radius;
		}

		public Boolean Equals (BoundingSphere other)
		{
			return ((this.Center == other.Center) && (this.Radius == other.Radius));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is BoundingSphere) {
				flag = this.Equals ((BoundingSphere)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Center.GetHashCode () + this.Radius.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Center:{0} Radius:{1}}}", new Object[] { this.Center.ToString (), this.Radius.ToString () });
		}

		public static void CreateMerged (ref BoundingSphere original, ref BoundingSphere additional, out BoundingSphere result)
		{
			Double half; RealMaths.Half(out half);
			Double one = 1;
			Vector3 vector2;
			Vector3.Subtract (ref additional.Center, ref original.Center, out vector2);
			Double num = vector2.Length ();
			Double radius = original.Radius;
			Double num2 = additional.Radius;
			if ((radius + num2) >= num) {
				if ((radius - num2) >= num) {
					result = original;
					return;
				}
				if ((num2 - radius) >= num) {
					result = additional;
					return;
				}
			}
			Vector3 vector = (Vector3)(vector2 * (one / num));
			Double num5 = RealMaths.Min (-radius, num - num2);
			Double num4 = (RealMaths.Max (radius, num + num2) - num5) * half;
			result.Center = original.Center + ((Vector3)(vector * (num4 + num5)));
			result.Radius = num4;
		}

		public static void CreateFromBoundingBox (ref BoundingBox box, out BoundingSphere result)
		{
			Double half; RealMaths.Half(out half);
			Double num;
			Vector3.Lerp (ref box.Min, ref box.Max, half, out result.Center);
			Vector3.Distance (ref box.Min, ref box.Max, out num);
			result.Radius = num * half;
		}

		public static void CreateFromPoints (IEnumerable<Vector3> points, out BoundingSphere sphere)
		{	
			Double half; RealMaths.Half(out half);
			Double one = 1;

			Double num;
			Double num2;
			Vector3 vector2;
			Double num4;
			Double num5;
			
			Vector3 vector5;
			Vector3 vector6;
			Vector3 vector7;
			Vector3 vector8;
			Vector3 vector9;
			if (points == null) {
				throw new ArgumentNullException ("points");
			}
			IEnumerator<Vector3> enumerator = points.GetEnumerator ();
			if (!enumerator.MoveNext ()) {
				throw new ArgumentException ("BoundingSphereZeroPoints");
			}
			Vector3 vector4 = vector5 = vector6 = vector7 = vector8 = vector9 = enumerator.Current;
			foreach (Vector3 vector in points) {
				if (vector.X < vector4.X) {
					vector4 = vector;
				}
				if (vector.X > vector5.X) {
					vector5 = vector;
				}
				if (vector.Y < vector6.Y) {
					vector6 = vector;
				}
				if (vector.Y > vector7.Y) {
					vector7 = vector;
				}
				if (vector.Z < vector8.Z) {
					vector8 = vector;
				}
				if (vector.Z > vector9.Z) {
					vector9 = vector;
				}
			}
			Vector3.Distance (ref vector5, ref vector4, out num5);
			Vector3.Distance (ref vector7, ref vector6, out num4);
			Vector3.Distance (ref vector9, ref vector8, out num2);
			if (num5 > num4) {
				if (num5 > num2) {
					Vector3.Lerp (ref vector5, ref vector4, half, out vector2);
					num = num5 * half;
				} else {
					Vector3.Lerp (ref vector9, ref vector8, half, out vector2);
					num = num2 * half;
				}
			} else if (num4 > num2) {
				Vector3.Lerp (ref vector7, ref vector6, half, out vector2);
				num = num4 * half;
			} else {
				Vector3.Lerp (ref vector9, ref vector8, half, out vector2);
				num = num2 * half;
			}
			foreach (Vector3 vector10 in points) {
				Vector3 vector3;
				vector3.X = vector10.X - vector2.X;
				vector3.Y = vector10.Y - vector2.Y;
				vector3.Z = vector10.Z - vector2.Z;
				Double num3 = vector3.Length ();
				if (num3 > num) {
					num = (num + num3) * half;
					vector2 += (Vector3)((one - (num / num3)) * vector3);
				}
			}
			sphere.Center = vector2;
			sphere.Radius = num;
		}

		public static void CreateFromFrustum (ref BoundingFrustum frustum, out BoundingSphere sphere)
		{
			if (frustum == null) {
				throw new ArgumentNullException ("frustum");
			}

			CreateFromPoints (frustum.cornerArray, out sphere);
		}

		public Boolean Intersects (ref BoundingBox box)
		{
			Double num;
			Vector3 vector;
			Vector3.Clamp (ref this.Center, ref box.Min, ref box.Max, out vector);
			Vector3.DistanceSquared (ref this.Center, ref vector, out num);
			return (num <= (this.Radius * this.Radius));
		}

		public Boolean Intersects (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			return frustum.Intersects (ref this);
		}

		public PlaneIntersectionType Intersects (ref Plane plane)
		{
			return plane.Intersects (ref this);
		}

		public Double? Intersects (ref Ray ray)
		{
			return ray.Intersects (ref this);
		}

		public Boolean Intersects (ref BoundingSphere sphere)
		{
			Double two = 2;

			Double num3;
			Vector3.DistanceSquared (ref this.Center, ref sphere.Center, out num3);
			Double radius = this.Radius;
			Double num = sphere.Radius;
			if ((((radius * radius) + ((two * radius) * num)) + (num * num)) <= num3) {
				return false;
			}
			return true;
		}

		public ContainmentType Contains (ref BoundingBox box)
		{
			Vector3 vector;
			if (!box.Intersects (ref this)) {
				return ContainmentType.Disjoint;
			}
			Double num = this.Radius * this.Radius;
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			if (!frustum.Intersects (ref this)) {
				return ContainmentType.Disjoint;
			}
			Double num2 = this.Radius * this.Radius;
			foreach (Vector3 vector2 in frustum.cornerArray) {
				Vector3 vector;
				vector.X = vector2.X - this.Center.X;
				vector.Y = vector2.Y - this.Center.Y;
				vector.Z = vector2.Z - this.Center.Z;
				if (vector.LengthSquared () > num2) {
					return ContainmentType.Intersects;
				}
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref Vector3 point)
		{
			Double temp;
			Vector3.DistanceSquared (ref point, ref this.Center, out temp);

			if (temp >= (this.Radius * this.Radius))
			{
				return ContainmentType.Disjoint;
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref BoundingSphere sphere)
		{
			Double num3;
			Vector3.Distance (ref this.Center, ref sphere.Center, out num3);
			Double radius = this.Radius;
			Double num = sphere.Radius;
			if ((radius + num) < num3) {
				return ContainmentType.Disjoint;
			}
			if ((radius - num) < num3) {
				return ContainmentType.Intersects;
			}
			return ContainmentType.Contains;
		}

		internal void SupportMapping (ref Vector3 v, out Vector3 result)
		{
			Double num2 = v.Length ();
			Double num = this.Radius / num2;
			result.X = this.Center.X + (v.X * num);
			result.Y = this.Center.Y + (v.Y * num);
			result.Z = this.Center.Z + (v.Z * num);
		}

		public BoundingSphere Transform (Matrix44 matrix)
		{
			BoundingSphere sphere = new BoundingSphere ();
			Vector3.Transform (ref this.Center, ref matrix, out sphere.Center);
			Double num4 = ((matrix.M11 * matrix.M11) + (matrix.M12 * matrix.M12)) + (matrix.M13 * matrix.M13);
			Double num3 = ((matrix.M21 * matrix.M21) + (matrix.M22 * matrix.M22)) + (matrix.M23 * matrix.M23);
			Double num2 = ((matrix.M31 * matrix.M31) + (matrix.M32 * matrix.M32)) + (matrix.M33 * matrix.M33);
			Double num = RealMaths.Max (num4, RealMaths.Max (num3, num2));
			sphere.Radius = this.Radius * (RealMaths.Sqrt (num));
			return sphere;
		}

		public void Transform (ref Matrix44 matrix, out BoundingSphere result)
		{
			Vector3.Transform (ref this.Center, ref matrix, out result.Center);
			Double num4 = ((matrix.M11 * matrix.M11) + (matrix.M12 * matrix.M12)) + (matrix.M13 * matrix.M13);
			Double num3 = ((matrix.M21 * matrix.M21) + (matrix.M22 * matrix.M22)) + (matrix.M23 * matrix.M23);
			Double num2 = ((matrix.M31 * matrix.M31) + (matrix.M32 * matrix.M32)) + (matrix.M33 * matrix.M33);
			Double num = RealMaths.Max (num4, RealMaths.Max (num3, num2));
			result.Radius = this.Radius * (RealMaths.Sqrt (num));
		}

		public static Boolean operator == (BoundingSphere a, BoundingSphere b)
		{
			return a.Equals (b);
		}

		public static Boolean operator != (BoundingSphere a, BoundingSphere b)
		{
			if (!(a.Center != b.Center)) {
				return !(a.Radius == b.Radius);
			}
			return true;
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Matrix44 
		: IEquatable<Matrix44>
	{
		// Row 0
		public Double M11;
		public Double M12;
		public Double M13;
		public Double M14;

		// Row 1
		public Double M21;
		public Double M22;
		public Double M23;
		public Double M24;

		// Row 2
		public Double M31;
		public Double M32;
		public Double M33;
		public Double M34;

		// Row 3
		public Double M41; // translation.x
		public Double M42; // translation.y
		public Double M43; // translation.z
		public Double M44;
		
		public Vector3 Up {
			get {
				Vector3 vector;
				vector.X = this.M21;
				vector.Y = this.M22;
				vector.Z = this.M23;
				return vector;
			}
			set {
				this.M21 = value.X;
				this.M22 = value.Y;
				this.M23 = value.Z;
			}
		}

		public Vector3 Down {
			get {
				Vector3 vector;
				vector.X = -this.M21;
				vector.Y = -this.M22;
				vector.Z = -this.M23;
				return vector;
			}
			set {
				this.M21 = -value.X;
				this.M22 = -value.Y;
				this.M23 = -value.Z;
			}
		}

		public Vector3 Right {
			get {
				Vector3 vector;
				vector.X = this.M11;
				vector.Y = this.M12;
				vector.Z = this.M13;
				return vector;
			}
			set {
				this.M11 = value.X;
				this.M12 = value.Y;
				this.M13 = value.Z;
			}
		}

		public Vector3 Left {
			get {
				Vector3 vector;
				vector.X = -this.M11;
				vector.Y = -this.M12;
				vector.Z = -this.M13;
				return vector;
			}
			set {
				this.M11 = -value.X;
				this.M12 = -value.Y;
				this.M13 = -value.Z;
			}
		}

		public Vector3 Forward {
			get {
				Vector3 vector;
				vector.X = -this.M31;
				vector.Y = -this.M32;
				vector.Z = -this.M33;
				return vector;
			}
			set {
				this.M31 = -value.X;
				this.M32 = -value.Y;
				this.M33 = -value.Z;
			}
		}

		public Vector3 Backward {
			get {
				Vector3 vector;
				vector.X = this.M31;
				vector.Y = this.M32;
				vector.Z = this.M33;
				return vector;
			}
			set {
				this.M31 = value.X;
				this.M32 = value.Y;
				this.M33 = value.Z;
			}
		}

		public Vector3 Translation {
			get {
				Vector3 vector;
				vector.X = this.M41;
				vector.Y = this.M42;
				vector.Z = this.M43;
				return vector;
			}
			set {
				this.M41 = value.X;
				this.M42 = value.Y;
				this.M43 = value.Z;
			}
		}

		public Matrix44 (Double m11, Double m12, Double m13, Double m14, Double m21, Double m22, Double m23, Double m24, Double m31, Double m32, Double m33, Double m34, Double m41, Double m42, Double m43, Double m44)
		{
			this.M11 = m11;
			this.M12 = m12;
			this.M13 = m13;
			this.M14 = m14;
			this.M21 = m21;
			this.M22 = m22;
			this.M23 = m23;
			this.M24 = m24;
			this.M31 = m31;
			this.M32 = m32;
			this.M33 = m33;
			this.M34 = m34;
			this.M41 = m41;
			this.M42 = m42;
			this.M43 = m43;
			this.M44 = m44;
		}

		public override String ToString ()
		{
			return ("{ " + string.Format ("{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", new Object[] { this.M11.ToString (), this.M12.ToString (), this.M13.ToString (), this.M14.ToString () }) + string.Format ("{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", new Object[] { this.M21.ToString (), this.M22.ToString (), this.M23.ToString (), this.M24.ToString () }) + string.Format ("{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", new Object[] { this.M31.ToString (), this.M32.ToString (), this.M33.ToString (), this.M34.ToString () }) + string.Format ("{{M41:{0} M42:{1} M43:{2} M44:{3}}} ", new Object[] { this.M41.ToString (), this.M42.ToString (), this.M43.ToString (), this.M44.ToString () }) + "}");
		}

		public Boolean Equals (Matrix44 other)
		{
			return ((((((this.M11 == other.M11) && (this.M22 == other.M22)) && ((this.M33 == other.M33) && (this.M44 == other.M44))) && (((this.M12 == other.M12) && (this.M13 == other.M13)) && ((this.M14 == other.M14) && (this.M21 == other.M21)))) && ((((this.M23 == other.M23) && (this.M24 == other.M24)) && ((this.M31 == other.M31) && (this.M32 == other.M32))) && (((this.M34 == other.M34) && (this.M41 == other.M41)) && (this.M42 == other.M42)))) && (this.M43 == other.M43));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Matrix44)
			{
				flag = this.Equals ((Matrix44)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (((((((((((((((this.M11.GetHashCode () + this.M12.GetHashCode ()) + this.M13.GetHashCode ()) + this.M14.GetHashCode ()) + this.M21.GetHashCode ()) + this.M22.GetHashCode ()) + this.M23.GetHashCode ()) + this.M24.GetHashCode ()) + this.M31.GetHashCode ()) + this.M32.GetHashCode ()) + this.M33.GetHashCode ()) + this.M34.GetHashCode ()) + this.M41.GetHashCode ()) + this.M42.GetHashCode ()) + this.M43.GetHashCode ()) + this.M44.GetHashCode ());
		}

		#region Constants

		static Matrix44 identity;

		static Matrix44 ()
		{
			Double zero = 0;
			Double one = 1;
			identity = new Matrix44 (one, zero, zero, zero, zero, one, zero, zero, zero, zero, one, zero, zero, zero, zero, one);
		}

		public static Matrix44 Identity {
			get {
				return identity;
			}
		}
		
		#endregion
		#region Create

		public static void CreateTranslation (ref Vector3 position, out Matrix44 result)
		{
			result.M11 = 1;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = 1;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = 1;
			result.M34 = 0;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1;
		}
		
		public static void CreateTranslation (Double xPosition, Double yPosition, Double zPosition, out Matrix44 result)
		{	
			result.M11 = 1;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = 1;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = 1;
			result.M34 = 0;
			result.M41 = xPosition;
			result.M42 = yPosition;
			result.M43 = zPosition;
			result.M44 = 1;
		}
		
		// Creates a scaling matrix based on x, y, z.
		public static void CreateScale (Double xScale, Double yScale, Double zScale, out Matrix44 result)
		{
			result.M11 = xScale;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = yScale;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = zScale;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		// Creates a scaling matrix based on a vector.
		public static void CreateScale (ref Vector3 scales, out Matrix44 result)
		{
			result.M11 = scales.X;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = scales.Y;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = scales.Z;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		// Create a scaling matrix consistant along each axis
		public static void CreateScale (Double scale, out Matrix44 result)
		{
			result.M11 = scale;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = scale;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = scale;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		public static void CreateRotationX (Double radians, out Matrix44 result)
		{
			// http://en.wikipedia.org/wiki/Rotation_matrix

			Double cos = RealMaths.Cos (radians);
			Double sin = RealMaths.Sin (radians);

			result.M11 = 1;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = cos;
			result.M23 = sin;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = -sin;
			result.M33 = cos;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		public static void CreateRotationY (Double radians, out Matrix44 result)
		{
			// http://en.wikipedia.org/wiki/Rotation_matrix

			Double cos = RealMaths.Cos (radians);
			Double sin = RealMaths.Sin (radians);

			result.M11 = cos;
			result.M12 = 0;
			result.M13 = -sin;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = 1;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = sin;
			result.M32 = 0;
			result.M33 = cos;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}
		
		public static void CreateRotationZ (Double radians, out Matrix44 result)
		{
			// http://en.wikipedia.org/wiki/Rotation_matrix

			Double cos = RealMaths.Cos (radians);
			Double sin = RealMaths.Sin (radians);

			result.M11 = cos;
			result.M12 = sin;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = -sin;
			result.M22 = cos;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = 1;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}
		
		public static void CreateFromAxisAngle (ref Vector3 axis, Double angle, out Matrix44 result)
		{
			Double one = 1;

			Double x = axis.X;
			Double y = axis.Y;
			Double z = axis.Z;

			Double sin = RealMaths.Sin (angle);
			Double cos = RealMaths.Cos (angle);

			Double xx = x * x;
			Double yy = y * y;
			Double zz = z * z;

			Double xy = x * y;
			Double xz = x * z;
			Double yz = y * z;

			result.M11 = xx + (cos * (one - xx));
			result.M12 = (xy - (cos * xy)) + (sin * z);
			result.M13 = (xz - (cos * xz)) - (sin * y);
			result.M14 = 0;

			result.M21 = (xy - (cos * xy)) - (sin * z);
			result.M22 = yy + (cos * (one - yy));
			result.M23 = (yz - (cos * yz)) + (sin * x);
			result.M24 = 0;

			result.M31 = (xz - (cos * xz)) + (sin * y);
			result.M32 = (yz - (cos * yz)) - (sin * x);
			result.M33 = zz + (cos * (one - zz));
			result.M34 = 0;

			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = one;
		}
		
		public static void CreateFromAllAxis (ref Vector3 right, ref Vector3 up, ref Vector3 backward, out Matrix44 result)
		{
			if(!right.IsUnit() || !up.IsUnit() || !backward.IsUnit() )
			{
				throw new ArgumentException("The input vertors must be normalised.");
			}

			result.M11 = right.X;
			result.M12 = right.Y;
			result.M13 = right.Z;
			result.M14 = 0;
			result.M21 = up.X;
			result.M22 = up.Y;
			result.M23 = up.Z;
			result.M24 = 0;
			result.M31 = backward.X;
			result.M32 = backward.Y;
			result.M33 = backward.Z;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		public static void CreateWorldNew (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result)
		{
			Vector3 backward = -forward;

			Vector3 right;

			Vector3.Cross (ref up, ref backward, out right);

			right.Normalise();

			Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out result);

			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
		}

		public static void CreateWorld (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result)
		{
			if(!forward.IsUnit() || !up.IsUnit() )
			{
				throw new ArgumentException("The input vertors must be normalised.");
			}

			Vector3 backward = -forward;

			Vector3 vector; Vector3.Normalise (ref backward, out vector);

			Vector3 cross; Vector3.Cross (ref up, ref vector, out cross);

			Vector3 vector2; Vector3.Normalise (ref cross, out vector2);

			Vector3 vector3; Vector3.Cross (ref vector, ref vector2, out vector3);

			result.M11 = vector2.X;
			result.M12 = vector2.Y;
			result.M13 = vector2.Z;
			result.M14 = 0;
			result.M21 = vector3.X;
			result.M22 = vector3.Y;
			result.M23 = vector3.Z;
			result.M24 = 0;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = 0;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1;
		}

		public static void CreateFromQuaternion (ref Quaternion quaternion, out Matrix44 result)
		{
			if(!quaternion.IsUnit())
			{
				throw new ArgumentException("Input quaternion must be normalised.");
			}

			Double zero = 0;
			Double one = 1;

			Double xs = quaternion.X + quaternion.X;   
			Double ys = quaternion.Y + quaternion.Y;
			Double zs = quaternion.Z + quaternion.Z;
			Double wx = quaternion.W * xs;
			Double wy = quaternion.W * ys;
			Double wz = quaternion.W * zs;
			Double xx = quaternion.X * xs;
			Double xy = quaternion.X * ys;
			Double xz = quaternion.X * zs;
			Double yy = quaternion.Y * ys;
			Double yz = quaternion.Y * zs;
			Double zz = quaternion.Z * zs;

			result.M11 = one - (yy + zz);
			result.M21 = xy - wz;
			result.M31 = xz + wy;
			result.M41 = zero;
    
			result.M12 = xy + wz;
			result.M22 = one - (xx + zz);
			result.M32 = yz - wx;
			result.M42 = zero;
    
			result.M13 = xz - wy;
			result.M23 = yz + wx;
			result.M33 = one - (xx + yy);
			result.M43 = zero;

			result.M14 = zero;
			result.M24 = zero;
			result.M34 = zero;
			result.M44 = one;
		}



		// todo: remove when we dont need this for the tests
		internal static void CreateFromQuaternionOld (ref Quaternion quaternion, out Matrix44 result)
		{
			Double zero = 0;
			Double one; RealMaths.One(out one);
			Double two = 2;

			Double num9 = quaternion.X * quaternion.X;
			Double num8 = quaternion.Y * quaternion.Y;
			Double num7 = quaternion.Z * quaternion.Z;
			Double num6 = quaternion.X * quaternion.Y;
			Double num5 = quaternion.Z * quaternion.W;
			Double num4 = quaternion.Z * quaternion.X;
			Double num3 = quaternion.Y * quaternion.W;
			Double num2 = quaternion.Y * quaternion.Z;
			Double num = quaternion.X * quaternion.W;
			result.M11 = one - (two * (num8 + num7));
			result.M12 = two * (num6 + num5);
			result.M13 = two * (num4 - num3);
			result.M14 = zero;
			result.M21 = two * (num6 - num5);
			result.M22 = one - (two * (num7 + num9));
			result.M23 = two * (num2 + num);
			result.M24 = zero;
			result.M31 = two * (num4 + num3);
			result.M32 = two * (num2 - num);
			result.M33 = one - (two * (num8 + num9));
			result.M34 = zero;
			result.M41 = zero;
			result.M42 = zero;
			result.M43 = zero;
			result.M44 = one;
		}

		public static void CreateFromYawPitchRoll (Double yaw, Double pitch, Double roll, out Matrix44 result)
		{
			Quaternion quaternion;

			Quaternion.CreateFromYawPitchRoll (yaw, pitch, roll, out quaternion);

			CreateFromQuaternion (ref quaternion, out result);
		}










		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////
		// TODO: REVIEW FROM HERE ONWARDS
		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////


		// FROM XNA
		// --------
		// Creates a cylindrical billboard that rotates around a specified axis.
		// This method computes the facing direction of the billboard from the object position and camera position. 
		// When the object and camera positions are too close, the matrix will not be accurate. 
		// To avoid this problem, the method uses the optional camera forward vector if the positions are too close.
		public static void CreateBillboard (ref Vector3 ObjectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix44 result)
		{
			Double zero = 0;
			Double one; RealMaths.One(out one);

			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			vector.X = ObjectPosition.X - cameraPosition.X;
			vector.Y = ObjectPosition.Y - cameraPosition.Y;
			vector.Z = ObjectPosition.Z - cameraPosition.Z;
			Double num = vector.LengthSquared ();
			Double limit; RealMaths.FromString("0.0001", out limit);

			if (num < limit) {
				vector = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
			} else {
				Vector3.Multiply (ref vector, (Double)(one / (RealMaths.Sqrt (num))), out vector);
			}
			Vector3.Cross (ref cameraUpVector, ref vector, out vector3);
			vector3.Normalise ();
			Vector3.Cross (ref vector, ref vector3, out vector2);
			result.M11 = vector3.X;
			result.M12 = vector3.Y;
			result.M13 = vector3.Z;
			result.M14 = zero;
			result.M21 = vector2.X;
			result.M22 = vector2.Y;
			result.M23 = vector2.Z;
			result.M24 = zero;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = zero;
			result.M41 = ObjectPosition.X;
			result.M42 = ObjectPosition.Y;
			result.M43 = ObjectPosition.Z;
			result.M44 = one;
		}
		
		public static void CreateConstrainedBillboard (ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector, out Matrix44 result)
		{
			Double zero = 0;
			Double one; RealMaths.One(out one);

			Double num;
			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			vector2.X = objectPosition.X - cameraPosition.X;
			vector2.Y = objectPosition.Y - cameraPosition.Y;
			vector2.Z = objectPosition.Z - cameraPosition.Z;
			Double num2 = vector2.LengthSquared ();
			Double limit; RealMaths.FromString("0.0001", out limit);

			if (num2 < limit) {
				vector2 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
			} else {
				Vector3.Multiply (ref vector2, (Double)(one / (RealMaths.Sqrt (num2))), out vector2);
			}
			Vector3 vector4 = rotateAxis;
			Vector3.Dot (ref rotateAxis, ref vector2, out num);

			Double realHorrid; RealMaths.FromString("0.9982547", out realHorrid);

			if (RealMaths.Abs (num) > realHorrid) {
				if (objectForwardVector.HasValue) {
					vector = objectForwardVector.Value;
					Vector3.Dot (ref rotateAxis, ref vector, out num);
					if (RealMaths.Abs (num) > realHorrid) {
						num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
						vector = (RealMaths.Abs (num) > realHorrid) ? Vector3.Right : Vector3.Forward;
					}
				} else {
					num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
					vector = (RealMaths.Abs (num) > realHorrid) ? Vector3.Right : Vector3.Forward;
				}
				Vector3.Cross (ref rotateAxis, ref vector, out vector3);
				vector3.Normalise ();
				Vector3.Cross (ref vector3, ref rotateAxis, out vector);
				vector.Normalise ();
			} else {
				Vector3.Cross (ref rotateAxis, ref vector2, out vector3);
				vector3.Normalise ();
				Vector3.Cross (ref vector3, ref vector4, out vector);
				vector.Normalise ();
			}
			result.M11 = vector3.X;
			result.M12 = vector3.Y;
			result.M13 = vector3.Z;
			result.M14 = zero;
			result.M21 = vector4.X;
			result.M22 = vector4.Y;
			result.M23 = vector4.Z;
			result.M24 = zero;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = zero;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = one;
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
		public static void CreatePerspectiveFieldOfView (Double fieldOfView, Double aspectRatio, Double nearPlaneDistance, Double farPlaneDistance, out Matrix44 result)
		{
			Double zero = 0;
			Double half; RealMaths.Half(out half);
			Double one; RealMaths.One(out one);
			Double pi; RealMaths.Pi(out pi);

			if ((fieldOfView <= zero) || (fieldOfView >= pi)) {
				throw new ArgumentOutOfRangeException ("fieldOfView");
			}
			if (nearPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			if (farPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			Double num = one / (RealMaths.Tan ((fieldOfView * half)));
			Double num9 = num / aspectRatio;
			result.M11 = num9;
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = num;
			result.M21 = result.M23 = result.M24 = zero;
			result.M31 = result.M32 = zero;
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -one;
			result.M41 = result.M42 = result.M44 = zero;
			result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
		public static void CreatePerspective (Double width, Double height, Double nearPlaneDistance, Double farPlaneDistance, out Matrix44 result)
		{
			Double zero = 0;
			Double one; RealMaths.One(out one);
			Double two = 2;

			if (nearPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			if (farPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			result.M11 = (two * nearPlaneDistance) / width;
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = (two * nearPlaneDistance) / height;
			result.M21 = result.M23 = result.M24 = zero;
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M31 = result.M32 = zero;
			result.M34 = -one;
			result.M41 = result.M42 = result.M44 = zero;
			result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
		}


		// ref: http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx
		public static void CreatePerspectiveOffCenter (Double left, Double right, Double bottom, Double top, Double nearPlaneDistance, Double farPlaneDistance, out Matrix44 result)
		{
			Double zero = 0;
			Double one; RealMaths.One(out one);
			Double two = 2;

			if (nearPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			if (farPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			result.M11 = (two * nearPlaneDistance) / (right - left);
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = (two * nearPlaneDistance) / (top - bottom);
			result.M21 = result.M23 = result.M24 = zero;
			result.M31 = (left + right) / (right - left);
			result.M32 = (top + bottom) / (top - bottom);
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -one;
			result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
			result.M41 = result.M42 = result.M44 = zero;
		}
		
		// ref: http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
		public static void CreateOrthographic (Double width, Double height, Double zNearPlane, Double zFarPlane, out Matrix44 result)
		{
			Double zero = 0;
			Double one; RealMaths.One(out one);
			Double two = 2;

			result.M11 = two / width;
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = two / height;
			result.M21 = result.M23 = result.M24 = zero;
			result.M33 = one / (zNearPlane - zFarPlane);
			result.M31 = result.M32 = result.M34 = zero;
			result.M41 = result.M42 = zero;
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = one;
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx
		public static void CreateOrthographicOffCenter (Double left, Double right, Double bottom, Double top, Double zNearPlane, Double zFarPlane, out Matrix44 result)
		{
			Double zero = 0;
			Double one; RealMaths.One(out one);
			Double two = 2;

			result.M11 = two / (right - left);
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = two / (top - bottom);
			result.M21 = result.M23 = result.M24 = zero;
			result.M33 = one / (zNearPlane - zFarPlane);
			result.M31 = result.M32 = result.M34 = zero;
			result.M41 = (left + right) / (left - right);
			result.M42 = (top + bottom) / (bottom - top);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = one;
		}
		
		// ref: http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx
		public static void CreateLookAt (ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix44 result)
		{
			Double zero = 0;
			Double one; RealMaths.One(out one);

			Vector3 targetToPosition = cameraPosition - cameraTarget;

			Vector3 vector; Vector3.Normalise (ref targetToPosition, out vector);

			Vector3 cross; Vector3.Cross (ref cameraUpVector, ref vector, out cross); 

			Vector3 vector2; Vector3.Normalise (ref cross, out vector2);
			Vector3 vector3; Vector3.Cross (ref vector, ref vector2, out vector3);
			result.M11 = vector2.X;
			result.M12 = vector3.X;
			result.M13 = vector.X;
			result.M14 = zero;
			result.M21 = vector2.Y;
			result.M22 = vector3.Y;
			result.M23 = vector.Y;
			result.M24 = zero;
			result.M31 = vector2.Z;
			result.M32 = vector3.Z;
			result.M33 = vector.Z;
			result.M34 = zero;

			Vector3.Dot (ref vector2, ref cameraPosition, out result.M41);
			Vector3.Dot (ref vector3, ref cameraPosition, out result.M42);
			Vector3.Dot (ref vector, ref cameraPosition, out result.M43);
			
			result.M41 *= -one;
			result.M42 *= -one;
			result.M43 *= -one;

			result.M44 = one;
		}

		
	

		// ref: http://msdn.microsoft.com/en-us/library/bb205364(v=VS.85).aspx
		public static void CreateShadow (ref Vector3 lightDirection, ref Plane plane, out Matrix44 result)
		{
			Double zero = 0;
			
			Plane plane2;
			Plane.Normalise (ref plane, out plane2);
			Double num = ((plane2.Normal.X * lightDirection.X) + (plane2.Normal.Y * lightDirection.Y)) + (plane2.Normal.Z * lightDirection.Z);
			Double num5 = -plane2.Normal.X;
			Double num4 = -plane2.Normal.Y;
			Double num3 = -plane2.Normal.Z;
			Double num2 = -plane2.D;
			result.M11 = (num5 * lightDirection.X) + num;
			result.M21 = num4 * lightDirection.X;
			result.M31 = num3 * lightDirection.X;
			result.M41 = num2 * lightDirection.X;
			result.M12 = num5 * lightDirection.Y;
			result.M22 = (num4 * lightDirection.Y) + num;
			result.M32 = num3 * lightDirection.Y;
			result.M42 = num2 * lightDirection.Y;
			result.M13 = num5 * lightDirection.Z;
			result.M23 = num4 * lightDirection.Z;
			result.M33 = (num3 * lightDirection.Z) + num;
			result.M43 = num2 * lightDirection.Z;
			result.M14 = zero;
			result.M24 = zero;
			result.M34 = zero;
			result.M44 = num;
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205356(v=VS.85).aspx
		public static void CreateReflection (ref Plane value, out Matrix44 result)
		{
			Double zero = 0;
			Double one; RealMaths.One(out one);
			Double two = 2;

			Plane plane;
			
			Plane.Normalise (ref value, out plane);
			
			value.Normalise ();
			
			Double x = plane.Normal.X;
			Double y = plane.Normal.Y;
			Double z = plane.Normal.Z;
			
			Double num3 = -two * x;
			Double num2 = -two * y;
			Double num = -two * z;
			
			result.M11 = (num3 * x) + one;
			result.M12 = num2 * x;
			result.M13 = num * x;
			result.M14 = zero;
			result.M21 = num3 * y;
			result.M22 = (num2 * y) + one;
			result.M23 = num * y;
			result.M24 = zero;
			result.M31 = num3 * z;
			result.M32 = num2 * z;
			result.M33 = (num * z) + one;
			result.M34 = zero;
			result.M41 = num3 * plane.D;
			result.M42 = num2 * plane.D;
			result.M43 = num * plane.D;
			result.M44 = one;
		}
		
		#endregion
		#region Maths

		//----------------------------------------------------------------------
		// Transpose
		//
		public void Transpose()
		{
			Double temp = this.M12;
			this.M12 = this.M21;
			this.M21 = temp;

			temp = this.M13;
			this.M13 = this.M31;
			this.M31 = temp;

			temp = this.M14;
			this.M14 = this.M41;
			this.M41 = temp;

			temp = this.M23;
			this.M23 = this.M32;
			this.M32 = temp;

			temp = this.M24;
			this.M24 = this.M42;
			this.M42 = temp;

			temp =  this.M34;
			this.M34 = this.M43;
			this.M43 = temp;
		}

		public static void Transpose (ref Matrix44 input, out Matrix44 output)
		{
		    output.M11 = input.M11;
			output.M12 = input.M21;
			output.M13 = input.M31;
			output.M14 = input.M41;
			output.M21 = input.M12;
			output.M22 = input.M22;
			output.M23 = input.M32;
			output.M24 = input.M42;
			output.M31 = input.M13;
			output.M32 = input.M23;
			output.M33 = input.M33;
			output.M34 = input.M43;
			output.M41 = input.M14;
			output.M42 = input.M24;
			output.M43 = input.M34;
			output.M44 = input.M44;
		}

		//----------------------------------------------------------------------
		// Decompose
		// ref: Essential Mathemathics For Games & Interactive Applications
		public bool Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
		{
			translation.X = M41;
            translation.Y = M42;
            translation.Z = M43;

			Vector3 a = new Vector3(M11, M21, M31);
			Vector3 b = new Vector3(M12, M22, M32);
			Vector3 c = new Vector3(M13, M23, M33);

			scale.X = a.Length();
			scale.Y = b.Length();
			scale.Z = c.Length();

			if ( RealMaths.IsZero(scale.X) || 
				 RealMaths.IsZero(scale.Y) || 
				 RealMaths.IsZero(scale.Z) )
            {
				rotation = Quaternion.Identity;
				return false;
			}

			a.Normalise();
			b.Normalise();
			c.Normalise();

			Vector3 right = new Vector3(a.X, b.X, c.X);
			Vector3 up = new Vector3(a.Y, b.Y, c.Y);
			Vector3 backward = new Vector3(a.Z, b.Z, c.Z);

			right.Normalise();
			up.Normalise();
			backward.Normalise();

			Matrix44 rotMat;
			Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out rotMat);

			Quaternion.CreateFromRotationMatrix(ref rotMat, out rotation);

			return true;
		}




		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////
		// TODO: REVIEW FROM HERE ONWARDS
		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////


		//----------------------------------------------------------------------
		// Determinant
		//
		public Double Determinant ()
		{
			Double num22 = this.M11;
			Double num21 = this.M12;
			Double num20 = this.M13;
			Double num19 = this.M14;
			Double num12 = this.M21;
			Double num11 = this.M22;
			Double num10 = this.M23;
			Double num9 = this.M24;
			Double num8 = this.M31;
			Double num7 = this.M32;
			Double num6 = this.M33;
			Double num5 = this.M34;
			Double num4 = this.M41;
			Double num3 = this.M42;
			Double num2 = this.M43;
			Double num = this.M44;
			
			Double num18 = (num6 * num) - (num5 * num2);
			Double num17 = (num7 * num) - (num5 * num3);
			Double num16 = (num7 * num2) - (num6 * num3);
			Double num15 = (num8 * num) - (num5 * num4);
			Double num14 = (num8 * num2) - (num6 * num4);
			Double num13 = (num8 * num3) - (num7 * num4);
			
			return ((((num22 * (((num11 * num18) - (num10 * num17)) + (num9 * num16))) - (num21 * (((num12 * num18) - (num10 * num15)) + (num9 * num14)))) + (num20 * (((num12 * num17) - (num11 * num15)) + (num9 * num13)))) - (num19 * (((num12 * num16) - (num11 * num14)) + (num10 * num13))));
		}
		
		//----------------------------------------------------------------------
		// Invert
		//
		public static void Invert (ref Matrix44 matrix, out Matrix44 result)
		{
			Double one = 1;
			Double num5 = matrix.M11;
			Double num4 = matrix.M12;
			Double num3 = matrix.M13;
			Double num2 = matrix.M14;
			Double num9 = matrix.M21;
			Double num8 = matrix.M22;
			Double num7 = matrix.M23;
			Double num6 = matrix.M24;
			Double num17 = matrix.M31;
			Double num16 = matrix.M32;
			Double num15 = matrix.M33;
			Double num14 = matrix.M34;
			Double num13 = matrix.M41;
			Double num12 = matrix.M42;
			Double num11 = matrix.M43;
			Double num10 = matrix.M44;
			Double num23 = (num15 * num10) - (num14 * num11);
			Double num22 = (num16 * num10) - (num14 * num12);
			Double num21 = (num16 * num11) - (num15 * num12);
			Double num20 = (num17 * num10) - (num14 * num13);
			Double num19 = (num17 * num11) - (num15 * num13);
			Double num18 = (num17 * num12) - (num16 * num13);
			Double num39 = ((num8 * num23) - (num7 * num22)) + (num6 * num21);
			Double num38 = -(((num9 * num23) - (num7 * num20)) + (num6 * num19));
			Double num37 = ((num9 * num22) - (num8 * num20)) + (num6 * num18);
			Double num36 = -(((num9 * num21) - (num8 * num19)) + (num7 * num18));
			Double num = one / ((((num5 * num39) + (num4 * num38)) + (num3 * num37)) + (num2 * num36));
			result.M11 = num39 * num;
			result.M21 = num38 * num;
			result.M31 = num37 * num;
			result.M41 = num36 * num;
			result.M12 = -(((num4 * num23) - (num3 * num22)) + (num2 * num21)) * num;
			result.M22 = (((num5 * num23) - (num3 * num20)) + (num2 * num19)) * num;
			result.M32 = -(((num5 * num22) - (num4 * num20)) + (num2 * num18)) * num;
			result.M42 = (((num5 * num21) - (num4 * num19)) + (num3 * num18)) * num;
			Double num35 = (num7 * num10) - (num6 * num11);
			Double num34 = (num8 * num10) - (num6 * num12);
			Double num33 = (num8 * num11) - (num7 * num12);
			Double num32 = (num9 * num10) - (num6 * num13);
			Double num31 = (num9 * num11) - (num7 * num13);
			Double num30 = (num9 * num12) - (num8 * num13);
			result.M13 = (((num4 * num35) - (num3 * num34)) + (num2 * num33)) * num;
			result.M23 = -(((num5 * num35) - (num3 * num32)) + (num2 * num31)) * num;
			result.M33 = (((num5 * num34) - (num4 * num32)) + (num2 * num30)) * num;
			result.M43 = -(((num5 * num33) - (num4 * num31)) + (num3 * num30)) * num;
			Double num29 = (num7 * num14) - (num6 * num15);
			Double num28 = (num8 * num14) - (num6 * num16);
			Double num27 = (num8 * num15) - (num7 * num16);
			Double num26 = (num9 * num14) - (num6 * num17);
			Double num25 = (num9 * num15) - (num7 * num17);
			Double num24 = (num9 * num16) - (num8 * num17);
			result.M14 = -(((num4 * num29) - (num3 * num28)) + (num2 * num27)) * num;
			result.M24 = (((num5 * num29) - (num3 * num26)) + (num2 * num25)) * num;
			result.M34 = -(((num5 * num28) - (num4 * num26)) + (num2 * num24)) * num;
			result.M44 = (((num5 * num27) - (num4 * num25)) + (num3 * num24)) * num;
		}


		//----------------------------------------------------------------------
		// Transform - Transforms a Matrix by applying a Quaternion rotation.
		//
		public static void Transform (ref Matrix44 value, ref Quaternion rotation, out Matrix44 result)
		{
			Double one = 1;

			Double num21 = rotation.X + rotation.X;
			Double num11 = rotation.Y + rotation.Y;
			Double num10 = rotation.Z + rotation.Z;
			
			Double num20 = rotation.W * num21;
			Double num19 = rotation.W * num11;
			Double num18 = rotation.W * num10;
			Double num17 = rotation.X * num21;
			Double num16 = rotation.X * num11;
			Double num15 = rotation.X * num10;
			Double num14 = rotation.Y * num11;
			Double num13 = rotation.Y * num10;
			Double num12 = rotation.Z * num10;
			
			Double num9 = (one - num14) - num12;
			
			Double num8 = num16 - num18;
			Double num7 = num15 + num19;
			Double num6 = num16 + num18;
			
			Double num5 = (one - num17) - num12;
			
			Double num4 = num13 - num20;
			Double num3 = num15 - num19;
			Double num2 = num13 + num20;
			
			Double num = (one - num17) - num14;
			
			Double num37 = ((value.M11 * num9) + (value.M12 * num8)) + (value.M13 * num7);
			Double num36 = ((value.M11 * num6) + (value.M12 * num5)) + (value.M13 * num4);
			Double num35 = ((value.M11 * num3) + (value.M12 * num2)) + (value.M13 * num);
			
			Double num34 = value.M14;
			
			Double num33 = ((value.M21 * num9) + (value.M22 * num8)) + (value.M23 * num7);
			Double num32 = ((value.M21 * num6) + (value.M22 * num5)) + (value.M23 * num4);
			Double num31 = ((value.M21 * num3) + (value.M22 * num2)) + (value.M23 * num);
			
			Double num30 = value.M24;
			
			Double num29 = ((value.M31 * num9) + (value.M32 * num8)) + (value.M33 * num7);
			Double num28 = ((value.M31 * num6) + (value.M32 * num5)) + (value.M33 * num4);
			Double num27 = ((value.M31 * num3) + (value.M32 * num2)) + (value.M33 * num);
			
			Double num26 = value.M34;
			
			Double num25 = ((value.M41 * num9) + (value.M42 * num8)) + (value.M43 * num7);
			Double num24 = ((value.M41 * num6) + (value.M42 * num5)) + (value.M43 * num4);
			Double num23 = ((value.M41 * num3) + (value.M42 * num2)) + (value.M43 * num);
			
			Double num22 = value.M44;
			
			result.M11 = num37;
			result.M12 = num36;
			result.M13 = num35;
			result.M14 = num34;
			result.M21 = num33;
			result.M22 = num32;
			result.M23 = num31;
			result.M24 = num30;
			result.M31 = num29;
			result.M32 = num28;
			result.M33 = num27;
			result.M34 = num26;
			result.M41 = num25;
			result.M42 = num24;
			result.M43 = num23;
			result.M44 = num22;
		}
		
		#endregion
		#region Operators
		
		public static Matrix44 operator - (Matrix44 matrix1)
		{
			Matrix44 matrix;
			matrix.M11 = -matrix1.M11;
			matrix.M12 = -matrix1.M12;
			matrix.M13 = -matrix1.M13;
			matrix.M14 = -matrix1.M14;
			matrix.M21 = -matrix1.M21;
			matrix.M22 = -matrix1.M22;
			matrix.M23 = -matrix1.M23;
			matrix.M24 = -matrix1.M24;
			matrix.M31 = -matrix1.M31;
			matrix.M32 = -matrix1.M32;
			matrix.M33 = -matrix1.M33;
			matrix.M34 = -matrix1.M34;
			matrix.M41 = -matrix1.M41;
			matrix.M42 = -matrix1.M42;
			matrix.M43 = -matrix1.M43;
			matrix.M44 = -matrix1.M44;
			return matrix;
		}
		
		public static Boolean operator == (Matrix44 matrix1, Matrix44 matrix2)
		{
			return ((((((matrix1.M11 == matrix2.M11) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M44 == matrix2.M44))) && (((matrix1.M12 == matrix2.M12) && (matrix1.M13 == matrix2.M13)) && ((matrix1.M14 == matrix2.M14) && (matrix1.M21 == matrix2.M21)))) && ((((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)) && ((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32))) && (((matrix1.M34 == matrix2.M34) && (matrix1.M41 == matrix2.M41)) && (matrix1.M42 == matrix2.M42)))) && (matrix1.M43 == matrix2.M43));
		}
		
		public static Boolean operator != (Matrix44 matrix1, Matrix44 matrix2)
		{
			if (((((matrix1.M11 == matrix2.M11) && (matrix1.M12 == matrix2.M12)) && ((matrix1.M13 == matrix2.M13) && (matrix1.M14 == matrix2.M14))) && (((matrix1.M21 == matrix2.M21) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)))) && ((((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M34 == matrix2.M34))) && (((matrix1.M41 == matrix2.M41) && (matrix1.M42 == matrix2.M42)) && (matrix1.M43 == matrix2.M43)))) {
				return !(matrix1.M44 == matrix2.M44);
			}
			return true;
		}
		
		public static Matrix44 operator + (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = matrix1.M11 + matrix2.M11;
			matrix.M12 = matrix1.M12 + matrix2.M12;
			matrix.M13 = matrix1.M13 + matrix2.M13;
			matrix.M14 = matrix1.M14 + matrix2.M14;
			matrix.M21 = matrix1.M21 + matrix2.M21;
			matrix.M22 = matrix1.M22 + matrix2.M22;
			matrix.M23 = matrix1.M23 + matrix2.M23;
			matrix.M24 = matrix1.M24 + matrix2.M24;
			matrix.M31 = matrix1.M31 + matrix2.M31;
			matrix.M32 = matrix1.M32 + matrix2.M32;
			matrix.M33 = matrix1.M33 + matrix2.M33;
			matrix.M34 = matrix1.M34 + matrix2.M34;
			matrix.M41 = matrix1.M41 + matrix2.M41;
			matrix.M42 = matrix1.M42 + matrix2.M42;
			matrix.M43 = matrix1.M43 + matrix2.M43;
			matrix.M44 = matrix1.M44 + matrix2.M44;
			return matrix;
		}
		
		public static Matrix44 operator - (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = matrix1.M11 - matrix2.M11;
			matrix.M12 = matrix1.M12 - matrix2.M12;
			matrix.M13 = matrix1.M13 - matrix2.M13;
			matrix.M14 = matrix1.M14 - matrix2.M14;
			matrix.M21 = matrix1.M21 - matrix2.M21;
			matrix.M22 = matrix1.M22 - matrix2.M22;
			matrix.M23 = matrix1.M23 - matrix2.M23;
			matrix.M24 = matrix1.M24 - matrix2.M24;
			matrix.M31 = matrix1.M31 - matrix2.M31;
			matrix.M32 = matrix1.M32 - matrix2.M32;
			matrix.M33 = matrix1.M33 - matrix2.M33;
			matrix.M34 = matrix1.M34 - matrix2.M34;
			matrix.M41 = matrix1.M41 - matrix2.M41;
			matrix.M42 = matrix1.M42 - matrix2.M42;
			matrix.M43 = matrix1.M43 - matrix2.M43;
			matrix.M44 = matrix1.M44 - matrix2.M44;
			return matrix;
		}
		
		public static Matrix44 operator * (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = (((matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21)) + (matrix1.M13 * matrix2.M31)) + (matrix1.M14 * matrix2.M41);
			matrix.M12 = (((matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22)) + (matrix1.M13 * matrix2.M32)) + (matrix1.M14 * matrix2.M42);
			matrix.M13 = (((matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23)) + (matrix1.M13 * matrix2.M33)) + (matrix1.M14 * matrix2.M43);
			matrix.M14 = (((matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24)) + (matrix1.M13 * matrix2.M34)) + (matrix1.M14 * matrix2.M44);
			matrix.M21 = (((matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21)) + (matrix1.M23 * matrix2.M31)) + (matrix1.M24 * matrix2.M41);
			matrix.M22 = (((matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22)) + (matrix1.M23 * matrix2.M32)) + (matrix1.M24 * matrix2.M42);
			matrix.M23 = (((matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23)) + (matrix1.M23 * matrix2.M33)) + (matrix1.M24 * matrix2.M43);
			matrix.M24 = (((matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24)) + (matrix1.M23 * matrix2.M34)) + (matrix1.M24 * matrix2.M44);
			matrix.M31 = (((matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21)) + (matrix1.M33 * matrix2.M31)) + (matrix1.M34 * matrix2.M41);
			matrix.M32 = (((matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22)) + (matrix1.M33 * matrix2.M32)) + (matrix1.M34 * matrix2.M42);
			matrix.M33 = (((matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23)) + (matrix1.M33 * matrix2.M33)) + (matrix1.M34 * matrix2.M43);
			matrix.M34 = (((matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24)) + (matrix1.M33 * matrix2.M34)) + (matrix1.M34 * matrix2.M44);
			matrix.M41 = (((matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21)) + (matrix1.M43 * matrix2.M31)) + (matrix1.M44 * matrix2.M41);
			matrix.M42 = (((matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22)) + (matrix1.M43 * matrix2.M32)) + (matrix1.M44 * matrix2.M42);
			matrix.M43 = (((matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23)) + (matrix1.M43 * matrix2.M33)) + (matrix1.M44 * matrix2.M43);
			matrix.M44 = (((matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24)) + (matrix1.M43 * matrix2.M34)) + (matrix1.M44 * matrix2.M44);
			return matrix;
		}
		
		public static Matrix44 operator * (Matrix44 matrix, Double scaleFactor)
		{
			Matrix44 matrix2;
			Double num = scaleFactor;
			matrix2.M11 = matrix.M11 * num;
			matrix2.M12 = matrix.M12 * num;
			matrix2.M13 = matrix.M13 * num;
			matrix2.M14 = matrix.M14 * num;
			matrix2.M21 = matrix.M21 * num;
			matrix2.M22 = matrix.M22 * num;
			matrix2.M23 = matrix.M23 * num;
			matrix2.M24 = matrix.M24 * num;
			matrix2.M31 = matrix.M31 * num;
			matrix2.M32 = matrix.M32 * num;
			matrix2.M33 = matrix.M33 * num;
			matrix2.M34 = matrix.M34 * num;
			matrix2.M41 = matrix.M41 * num;
			matrix2.M42 = matrix.M42 * num;
			matrix2.M43 = matrix.M43 * num;
			matrix2.M44 = matrix.M44 * num;
			return matrix2;
		}
		
		public static Matrix44 operator * (Double scaleFactor, Matrix44 matrix)
		{
			Matrix44 matrix2;
			Double num = scaleFactor;
			matrix2.M11 = matrix.M11 * num;
			matrix2.M12 = matrix.M12 * num;
			matrix2.M13 = matrix.M13 * num;
			matrix2.M14 = matrix.M14 * num;
			matrix2.M21 = matrix.M21 * num;
			matrix2.M22 = matrix.M22 * num;
			matrix2.M23 = matrix.M23 * num;
			matrix2.M24 = matrix.M24 * num;
			matrix2.M31 = matrix.M31 * num;
			matrix2.M32 = matrix.M32 * num;
			matrix2.M33 = matrix.M33 * num;
			matrix2.M34 = matrix.M34 * num;
			matrix2.M41 = matrix.M41 * num;
			matrix2.M42 = matrix.M42 * num;
			matrix2.M43 = matrix.M43 * num;
			matrix2.M44 = matrix.M44 * num;
			return matrix2;
		}
		
		public static Matrix44 operator / (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = matrix1.M11 / matrix2.M11;
			matrix.M12 = matrix1.M12 / matrix2.M12;
			matrix.M13 = matrix1.M13 / matrix2.M13;
			matrix.M14 = matrix1.M14 / matrix2.M14;
			matrix.M21 = matrix1.M21 / matrix2.M21;
			matrix.M22 = matrix1.M22 / matrix2.M22;
			matrix.M23 = matrix1.M23 / matrix2.M23;
			matrix.M24 = matrix1.M24 / matrix2.M24;
			matrix.M31 = matrix1.M31 / matrix2.M31;
			matrix.M32 = matrix1.M32 / matrix2.M32;
			matrix.M33 = matrix1.M33 / matrix2.M33;
			matrix.M34 = matrix1.M34 / matrix2.M34;
			matrix.M41 = matrix1.M41 / matrix2.M41;
			matrix.M42 = matrix1.M42 / matrix2.M42;
			matrix.M43 = matrix1.M43 / matrix2.M43;
			matrix.M44 = matrix1.M44 / matrix2.M44;
			return matrix;
		}
		
		public static Matrix44 operator / (Matrix44 matrix1, Double divider)
		{
			Matrix44 matrix;
			Double one = 1;
			Double num = one / divider;
			matrix.M11 = matrix1.M11 * num;
			matrix.M12 = matrix1.M12 * num;
			matrix.M13 = matrix1.M13 * num;
			matrix.M14 = matrix1.M14 * num;
			matrix.M21 = matrix1.M21 * num;
			matrix.M22 = matrix1.M22 * num;
			matrix.M23 = matrix1.M23 * num;
			matrix.M24 = matrix1.M24 * num;
			matrix.M31 = matrix1.M31 * num;
			matrix.M32 = matrix1.M32 * num;
			matrix.M33 = matrix1.M33 * num;
			matrix.M34 = matrix1.M34 * num;
			matrix.M41 = matrix1.M41 * num;
			matrix.M42 = matrix1.M42 * num;
			matrix.M43 = matrix1.M43 * num;
			matrix.M44 = matrix1.M44 * num;
			return matrix;
		}
		
		public static void Negate (ref Matrix44 matrix, out Matrix44 result)
		{
			result.M11 = -matrix.M11;
			result.M12 = -matrix.M12;
			result.M13 = -matrix.M13;
			result.M14 = -matrix.M14;
			result.M21 = -matrix.M21;
			result.M22 = -matrix.M22;
			result.M23 = -matrix.M23;
			result.M24 = -matrix.M24;
			result.M31 = -matrix.M31;
			result.M32 = -matrix.M32;
			result.M33 = -matrix.M33;
			result.M34 = -matrix.M34;
			result.M41 = -matrix.M41;
			result.M42 = -matrix.M42;
			result.M43 = -matrix.M43;
			result.M44 = -matrix.M44;
		}
		
		public static void Add (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			result.M11 = matrix1.M11 + matrix2.M11;
			result.M12 = matrix1.M12 + matrix2.M12;
			result.M13 = matrix1.M13 + matrix2.M13;
			result.M14 = matrix1.M14 + matrix2.M14;
			result.M21 = matrix1.M21 + matrix2.M21;
			result.M22 = matrix1.M22 + matrix2.M22;
			result.M23 = matrix1.M23 + matrix2.M23;
			result.M24 = matrix1.M24 + matrix2.M24;
			result.M31 = matrix1.M31 + matrix2.M31;
			result.M32 = matrix1.M32 + matrix2.M32;
			result.M33 = matrix1.M33 + matrix2.M33;
			result.M34 = matrix1.M34 + matrix2.M34;
			result.M41 = matrix1.M41 + matrix2.M41;
			result.M42 = matrix1.M42 + matrix2.M42;
			result.M43 = matrix1.M43 + matrix2.M43;
			result.M44 = matrix1.M44 + matrix2.M44;
		}
		
		public static void Subtract (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			result.M11 = matrix1.M11 - matrix2.M11;
			result.M12 = matrix1.M12 - matrix2.M12;
			result.M13 = matrix1.M13 - matrix2.M13;
			result.M14 = matrix1.M14 - matrix2.M14;
			result.M21 = matrix1.M21 - matrix2.M21;
			result.M22 = matrix1.M22 - matrix2.M22;
			result.M23 = matrix1.M23 - matrix2.M23;
			result.M24 = matrix1.M24 - matrix2.M24;
			result.M31 = matrix1.M31 - matrix2.M31;
			result.M32 = matrix1.M32 - matrix2.M32;
			result.M33 = matrix1.M33 - matrix2.M33;
			result.M34 = matrix1.M34 - matrix2.M34;
			result.M41 = matrix1.M41 - matrix2.M41;
			result.M42 = matrix1.M42 - matrix2.M42;
			result.M43 = matrix1.M43 - matrix2.M43;
			result.M44 = matrix1.M44 - matrix2.M44;
		}
		
		public static void Multiply (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			Double num16 = (((matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21)) + (matrix1.M13 * matrix2.M31)) + (matrix1.M14 * matrix2.M41);
			Double num15 = (((matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22)) + (matrix1.M13 * matrix2.M32)) + (matrix1.M14 * matrix2.M42);
			Double num14 = (((matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23)) + (matrix1.M13 * matrix2.M33)) + (matrix1.M14 * matrix2.M43);
			Double num13 = (((matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24)) + (matrix1.M13 * matrix2.M34)) + (matrix1.M14 * matrix2.M44);
			Double num12 = (((matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21)) + (matrix1.M23 * matrix2.M31)) + (matrix1.M24 * matrix2.M41);
			Double num11 = (((matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22)) + (matrix1.M23 * matrix2.M32)) + (matrix1.M24 * matrix2.M42);
			Double num10 = (((matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23)) + (matrix1.M23 * matrix2.M33)) + (matrix1.M24 * matrix2.M43);
			Double num9 = (((matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24)) + (matrix1.M23 * matrix2.M34)) + (matrix1.M24 * matrix2.M44);
			Double num8 = (((matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21)) + (matrix1.M33 * matrix2.M31)) + (matrix1.M34 * matrix2.M41);
			Double num7 = (((matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22)) + (matrix1.M33 * matrix2.M32)) + (matrix1.M34 * matrix2.M42);
			Double num6 = (((matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23)) + (matrix1.M33 * matrix2.M33)) + (matrix1.M34 * matrix2.M43);
			Double num5 = (((matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24)) + (matrix1.M33 * matrix2.M34)) + (matrix1.M34 * matrix2.M44);
			Double num4 = (((matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21)) + (matrix1.M43 * matrix2.M31)) + (matrix1.M44 * matrix2.M41);
			Double num3 = (((matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22)) + (matrix1.M43 * matrix2.M32)) + (matrix1.M44 * matrix2.M42);
			Double num2 = (((matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23)) + (matrix1.M43 * matrix2.M33)) + (matrix1.M44 * matrix2.M43);
			Double num = (((matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24)) + (matrix1.M43 * matrix2.M34)) + (matrix1.M44 * matrix2.M44);
			result.M11 = num16;
			result.M12 = num15;
			result.M13 = num14;
			result.M14 = num13;
			result.M21 = num12;
			result.M22 = num11;
			result.M23 = num10;
			result.M24 = num9;
			result.M31 = num8;
			result.M32 = num7;
			result.M33 = num6;
			result.M34 = num5;
			result.M41 = num4;
			result.M42 = num3;
			result.M43 = num2;
			result.M44 = num;
		}

		public static void Multiply (ref Matrix44 matrix1, Double scaleFactor, out Matrix44 result)
		{
			Double num = scaleFactor;
			result.M11 = matrix1.M11 * num;
			result.M12 = matrix1.M12 * num;
			result.M13 = matrix1.M13 * num;
			result.M14 = matrix1.M14 * num;
			result.M21 = matrix1.M21 * num;
			result.M22 = matrix1.M22 * num;
			result.M23 = matrix1.M23 * num;
			result.M24 = matrix1.M24 * num;
			result.M31 = matrix1.M31 * num;
			result.M32 = matrix1.M32 * num;
			result.M33 = matrix1.M33 * num;
			result.M34 = matrix1.M34 * num;
			result.M41 = matrix1.M41 * num;
			result.M42 = matrix1.M42 * num;
			result.M43 = matrix1.M43 * num;
			result.M44 = matrix1.M44 * num;
		}

		public static void Divide (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			result.M11 = matrix1.M11 / matrix2.M11;
			result.M12 = matrix1.M12 / matrix2.M12;
			result.M13 = matrix1.M13 / matrix2.M13;
			result.M14 = matrix1.M14 / matrix2.M14;
			result.M21 = matrix1.M21 / matrix2.M21;
			result.M22 = matrix1.M22 / matrix2.M22;
			result.M23 = matrix1.M23 / matrix2.M23;
			result.M24 = matrix1.M24 / matrix2.M24;
			result.M31 = matrix1.M31 / matrix2.M31;
			result.M32 = matrix1.M32 / matrix2.M32;
			result.M33 = matrix1.M33 / matrix2.M33;
			result.M34 = matrix1.M34 / matrix2.M34;
			result.M41 = matrix1.M41 / matrix2.M41;
			result.M42 = matrix1.M42 / matrix2.M42;
			result.M43 = matrix1.M43 / matrix2.M43;
			result.M44 = matrix1.M44 / matrix2.M44;
		}
		
		public static void Divide (ref Matrix44 matrix1, Double divider, out Matrix44 result)
		{
			Double one = 1;

			Double num = one / divider;
			result.M11 = matrix1.M11 * num;
			result.M12 = matrix1.M12 * num;
			result.M13 = matrix1.M13 * num;
			result.M14 = matrix1.M14 * num;
			result.M21 = matrix1.M21 * num;
			result.M22 = matrix1.M22 * num;
			result.M23 = matrix1.M23 * num;
			result.M24 = matrix1.M24 * num;
			result.M31 = matrix1.M31 * num;
			result.M32 = matrix1.M32 * num;
			result.M33 = matrix1.M33 * num;
			result.M34 = matrix1.M34 * num;
			result.M41 = matrix1.M41 * num;
			result.M42 = matrix1.M42 * num;
			result.M43 = matrix1.M43 * num;
			result.M44 = matrix1.M44 * num;
		}

		#endregion
		#region Utilities

		// beware, doing this might not produce what you expect.  you likely
		// want to lerp between quaternions.
		public static void Lerp (ref Matrix44 matrix1, ref Matrix44 matrix2, Double amount, out Matrix44 result)
		{
			result.M11 = matrix1.M11 + ((matrix2.M11 - matrix1.M11) * amount);
			result.M12 = matrix1.M12 + ((matrix2.M12 - matrix1.M12) * amount);
			result.M13 = matrix1.M13 + ((matrix2.M13 - matrix1.M13) * amount);
			result.M14 = matrix1.M14 + ((matrix2.M14 - matrix1.M14) * amount);
			result.M21 = matrix1.M21 + ((matrix2.M21 - matrix1.M21) * amount);
			result.M22 = matrix1.M22 + ((matrix2.M22 - matrix1.M22) * amount);
			result.M23 = matrix1.M23 + ((matrix2.M23 - matrix1.M23) * amount);
			result.M24 = matrix1.M24 + ((matrix2.M24 - matrix1.M24) * amount);
			result.M31 = matrix1.M31 + ((matrix2.M31 - matrix1.M31) * amount);
			result.M32 = matrix1.M32 + ((matrix2.M32 - matrix1.M32) * amount);
			result.M33 = matrix1.M33 + ((matrix2.M33 - matrix1.M33) * amount);
			result.M34 = matrix1.M34 + ((matrix2.M34 - matrix1.M34) * amount);
			result.M41 = matrix1.M41 + ((matrix2.M41 - matrix1.M41) * amount);
			result.M42 = matrix1.M42 + ((matrix2.M42 - matrix1.M42) * amount);
			result.M43 = matrix1.M43 + ((matrix2.M43 - matrix1.M43) * amount);
			result.M44 = matrix1.M44 + ((matrix2.M44 - matrix1.M44) * amount);
		}
		
		#endregion
		
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Plane 
		: IEquatable<Plane>
	{
		public Vector3 Normal;
		public Double D;

		public Plane (Double a, Double b, Double c, Double d)
		{
			this.Normal.X = a;
			this.Normal.Y = b;
			this.Normal.Z = c;
			this.D = d;
		}

		public Plane (Vector3 normal, Double d)
		{
			this.Normal = normal;
			this.D = d;
		}

		public Plane (Vector4 value)
		{
			this.Normal.X = value.X;
			this.Normal.Y = value.Y;
			this.Normal.Z = value.Z;
			this.D = value.W;
		}

		public Plane (Vector3 point1, Vector3 point2, Vector3 point3)
		{
			Double one = 1;

			Double num10 = point2.X - point1.X;
			Double num9 = point2.Y - point1.Y;
			Double num8 = point2.Z - point1.Z;
			Double num7 = point3.X - point1.X;
			Double num6 = point3.Y - point1.Y;
			Double num5 = point3.Z - point1.Z;
			Double num4 = (num9 * num5) - (num8 * num6);
			Double num3 = (num8 * num7) - (num10 * num5);
			Double num2 = (num10 * num6) - (num9 * num7);
			Double num11 = ((num4 * num4) + (num3 * num3)) + (num2 * num2);
			Double num = one / RealMaths.Sqrt (num11);
			this.Normal.X = num4 * num;
			this.Normal.Y = num3 * num;
			this.Normal.Z = num2 * num;
			this.D = -(((this.Normal.X * point1.X) + (this.Normal.Y * point1.Y)) + (this.Normal.Z * point1.Z));
		}

		public Boolean Equals (Plane other)
		{
			return ((((this.Normal.X == other.Normal.X) && (this.Normal.Y == other.Normal.Y)) && (this.Normal.Z == other.Normal.Z)) && (this.D == other.D));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Plane) {
				flag = this.Equals ((Plane)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Normal.GetHashCode () + this.D.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Normal:{0} D:{1}}}", new Object[] { this.Normal.ToString (), this.D.ToString () });
		}

		public void Normalise ()
		{
			Double one = 1;
			Double somethingWicked; RealMaths.FromString("0.0000001192093", out somethingWicked);

			Double num2 = ((this.Normal.X * this.Normal.X) + (this.Normal.Y * this.Normal.Y)) + (this.Normal.Z * this.Normal.Z);
			if (RealMaths.Abs (num2 - one) >= somethingWicked) {
				Double num = one / RealMaths.Sqrt (num2);
				this.Normal.X *= num;
				this.Normal.Y *= num;
				this.Normal.Z *= num;
				this.D *= num;
			}
		}

		public static void Normalise (ref Plane value, out Plane result)
		{
			Double one = 1;
			Double somethingWicked; RealMaths.FromString("0.0000001192093", out somethingWicked);

			Double num2 = ((value.Normal.X * value.Normal.X) + (value.Normal.Y * value.Normal.Y)) + (value.Normal.Z * value.Normal.Z);
			if (RealMaths.Abs (num2 - one) < somethingWicked) {
				result.Normal = value.Normal;
				result.D = value.D;
			} else {
				Double num = one / RealMaths.Sqrt (num2);
				result.Normal.X = value.Normal.X * num;
				result.Normal.Y = value.Normal.Y * num;
				result.Normal.Z = value.Normal.Z * num;
				result.D = value.D * num;
			}
		}

		public static void Transform (ref Plane plane, ref Matrix44 matrix, out Plane result)
		{
			Matrix44 matrix2;
			Matrix44.Invert (ref matrix, out matrix2);
			Double x = plane.Normal.X;
			Double y = plane.Normal.Y;
			Double z = plane.Normal.Z;
			Double d = plane.D;
			result.Normal.X = (((x * matrix2.M11) + (y * matrix2.M12)) + (z * matrix2.M13)) + (d * matrix2.M14);
			result.Normal.Y = (((x * matrix2.M21) + (y * matrix2.M22)) + (z * matrix2.M23)) + (d * matrix2.M24);
			result.Normal.Z = (((x * matrix2.M31) + (y * matrix2.M32)) + (z * matrix2.M33)) + (d * matrix2.M34);
			result.D = (((x * matrix2.M41) + (y * matrix2.M42)) + (z * matrix2.M43)) + (d * matrix2.M44);
		}


		public static void Transform (ref Plane plane, ref Quaternion rotation, out Plane result)
		{
			Double one = 1;

			Double num15 = rotation.X + rotation.X;
			Double num5 = rotation.Y + rotation.Y;
			Double num = rotation.Z + rotation.Z;
			Double num14 = rotation.W * num15;
			Double num13 = rotation.W * num5;
			Double num12 = rotation.W * num;
			Double num11 = rotation.X * num15;
			Double num10 = rotation.X * num5;
			Double num9 = rotation.X * num;
			Double num8 = rotation.Y * num5;
			Double num7 = rotation.Y * num;
			Double num6 = rotation.Z * num;
			Double num24 = (one - num8) - num6;
			Double num23 = num10 - num12;
			Double num22 = num9 + num13;
			Double num21 = num10 + num12;
			Double num20 = (one - num11) - num6;
			Double num19 = num7 - num14;
			Double num18 = num9 - num13;
			Double num17 = num7 + num14;
			Double num16 = (one - num11) - num8;
			Double x = plane.Normal.X;
			Double y = plane.Normal.Y;
			Double z = plane.Normal.Z;
			result.Normal.X = ((x * num24) + (y * num23)) + (z * num22);
			result.Normal.Y = ((x * num21) + (y * num20)) + (z * num19);
			result.Normal.Z = ((x * num18) + (y * num17)) + (z * num16);
			result.D = plane.D;
		}
		


		public Double Dot(ref Vector4 value)
		{
			return (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + (this.D * value.W);
		}

		public Double DotCoordinate (ref Vector3 value)
		{
			return (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + this.D;
		}

		public Double DotNormal (ref Vector3 value)
		{
			return ((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z);
		}

		public PlaneIntersectionType Intersects (ref BoundingBox box)
		{
			Double zero = 0;

			Vector3 vector;
			Vector3 vector2;
			vector2.X = (this.Normal.X >= zero) ? box.Min.X : box.Max.X;
			vector2.Y = (this.Normal.Y >= zero) ? box.Min.Y : box.Max.Y;
			vector2.Z = (this.Normal.Z >= zero) ? box.Min.Z : box.Max.Z;
			vector.X = (this.Normal.X >= zero) ? box.Max.X : box.Min.X;
			vector.Y = (this.Normal.Y >= zero) ? box.Max.Y : box.Min.Y;
			vector.Z = (this.Normal.Z >= zero) ? box.Max.Z : box.Min.Z;
			Double num = ((this.Normal.X * vector2.X) + (this.Normal.Y * vector2.Y)) + (this.Normal.Z * vector2.Z);
			if ((num + this.D) > zero) {
				return PlaneIntersectionType.Front;
			} else {
				num = ((this.Normal.X * vector.X) + (this.Normal.Y * vector.Y)) + (this.Normal.Z * vector.Z);
				if ((num + this.D) < zero) {
					return PlaneIntersectionType.Back;
				} else {
					return PlaneIntersectionType.Intersecting;
				}
			}
		}

		public PlaneIntersectionType Intersects (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			return frustum.Intersects (ref this);
		}

		public PlaneIntersectionType Intersects (ref BoundingSphere sphere)
		{
			Double num2 = ((sphere.Center.X * this.Normal.X) + (sphere.Center.Y * this.Normal.Y)) + (sphere.Center.Z * this.Normal.Z);
			Double num = num2 + this.D;
			if (num > sphere.Radius) {
				return PlaneIntersectionType.Front;
			} else if (num < -sphere.Radius) {
				return PlaneIntersectionType.Back;
			} else {
				return PlaneIntersectionType.Intersecting;
			}
		}

		public static Boolean operator == (Plane lhs, Plane rhs)
		{
			return lhs.Equals (rhs);
		}

		public static Boolean operator != (Plane lhs, Plane rhs)
		{
			if (((lhs.Normal.X == rhs.Normal.X) && (lhs.Normal.Y == rhs.Normal.Y)) && (lhs.Normal.Z == rhs.Normal.Z)) {
				return !(lhs.D == rhs.D);
			}
			return true;
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Quad
		: IEquatable<Quad>
	{
		public Vector3 A
		{
			get
			{
				return tri1.A;
			}
			set
			{
				tri1.A = value;
			}
		}

		public Vector3 B
		{
			get
			{
				return tri1.B;
			}
			set
			{
				tri1.B = value;
				tri2.B = value;
			}
		}

		public Vector3 C
		{
			get
			{
				return tri2.C;
			}
			set
			{
				tri1.C = value;
				tri2.C = value;
			}
		}

		public Vector3 D
		{
			get
			{
				return tri2.A;
			}
			set
			{
				tri1.A = value;
				tri2.A = value;
			}
		}

		Triangle tri1;
		Triangle tri2;

		public Quad (Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			this.tri1 = new Triangle(a, b, c);
			this.tri2 = new Triangle(d, b, c);
		}

		// Determines whether or not this Quad is equal in value to another Quad
		public Boolean Equals (Quad other)
		{
			if (this.A.X != other.A.X) return false;
			if (this.A.Y != other.A.Y) return false;
			if (this.A.Z != other.A.Z) return false;

			if (this.B.X != other.B.X) return false;
			if (this.B.Y != other.B.Y) return false;
			if (this.B.Z != other.B.Z) return false;

			if (this.C.X != other.C.X) return false;
			if (this.C.Y != other.C.Y) return false;
			if (this.C.Z != other.C.Z) return false;
			
			if (this.D.X != other.D.X) return false;
			if (this.D.Y != other.D.Y) return false;
			if (this.D.Z != other.D.Z) return false;

			// They match!
			return true;
		}

		// Determines whether or not this Quad is equal in value to another System.Object
		public override Boolean Equals (Object obj)
		{
			if (obj == null) return false;

			if (obj is Quad)
			{
				// Ok, it is a Quad, so just use the method above to compare.
				return this.Equals ((Quad) obj);
			}

			return false;
		}

		public override Int32 GetHashCode ()
		{
			return (this.A.GetHashCode () + this.B.GetHashCode () + this.C.GetHashCode () + this.D.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{A:{0} B:{1} C:{2} D:{3}}}", this.A, this.B, this.C, this.D);
		}

		public static Boolean operator == (Quad a, Quad b)
		{
			return a.Equals(b);
		}

		public static Boolean operator != (Quad a, Quad b)
		{
			return !a.Equals(b);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Quaternion 
		: IEquatable<Quaternion>
	{
		public Double X;
		public Double Y;
		public Double Z;
		public Double W;


		public Quaternion (Double x, Double y, Double z, Double w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		public Quaternion (Vector3 vectorPart, Double scalarPart)
		{
			this.X = vectorPart.X;
			this.Y = vectorPart.Y;
			this.Z = vectorPart.Z;
			this.W = scalarPart;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString (), this.W.ToString () });
		}

		public Boolean Equals (Quaternion other)
		{
			return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
		}

		public override Boolean Equals (Object obj)
		{

			Boolean flag = false;
			if (obj is Quaternion)
			{
				flag = this.Equals ((Quaternion)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ()) + this.W.GetHashCode ());
		}

		public Double LengthSquared ()
		{
			return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
		}

		public Double Length ()
		{
			Double num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			return RealMaths.Sqrt (num);
		}

		public void Normalise ()
		{
			Double one = 1;
			Double num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			Double num = one / RealMaths.Sqrt (num2);
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
			this.W *= num;
		}

		public Boolean IsUnit()
		{
			Double one = 1;

			return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
		}

		public void Conjugate ()
		{
			this.X = -this.X;
			this.Y = -this.Y;
			this.Z = -this.Z;
		}

		#region Constants

		static Quaternion identity;
		
		public static Quaternion Identity
		{
			get
			{
				return identity;
			}
		}

		static Quaternion ()
		{
			Double temp_one; RealMaths.One(out temp_one);
			Double temp_zero; RealMaths.Zero(out temp_zero);
			identity = new Quaternion (temp_zero, temp_zero, temp_zero, temp_one);
		}
		
		#endregion
		#region Create

		public static void CreateFromAxisAngle (ref Vector3 axis, Double angle, out Quaternion result)
		{
			Double half; RealMaths.Half(out half);
			Double theta = angle * half;

			Double sin = RealMaths.Sin (theta);
			Double cos = RealMaths.Cos (theta);

			result.X = axis.X * sin;
			result.Y = axis.Y * sin;
			result.Z = axis.Z * sin;

			result.W = cos;
		}
		
		public static void CreateFromYawPitchRoll (Double yaw, Double pitch, Double roll, out Quaternion result)
		{
			Double half; RealMaths.Half(out half);
			Double num9 = roll * half;

			Double num6 = RealMaths.Sin (num9);
			Double num5 = RealMaths.Cos (num9);

			Double num8 = pitch * half;

			Double num4 = RealMaths.Sin (num8);
			Double num3 = RealMaths.Cos (num8);

			Double num7 = yaw * half;

			Double num2 = RealMaths.Sin (num7);
			Double num = RealMaths.Cos (num7);

			result.X = ((num * num4) * num5) + ((num2 * num3) * num6);
			result.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
			result.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
			result.W = ((num * num3) * num5) + ((num2 * num4) * num6);
		}
		
		public static void CreateFromRotationMatrix (ref Matrix44 matrix, out Quaternion result)
		{
			Double zero = 0;
			Double half; RealMaths.Half(out half);
			Double one = 1;

			Double num8 = (matrix.M11 + matrix.M22) + matrix.M33;

			if (num8 > zero)
			{
				Double num = RealMaths.Sqrt (num8 + one);
				result.W = num * half;
				num = half / num;
				result.X = (matrix.M23 - matrix.M32) * num;
				result.Y = (matrix.M31 - matrix.M13) * num;
				result.Z = (matrix.M12 - matrix.M21) * num;
			}
			else if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
			{
				Double num7 = RealMaths.Sqrt (((one + matrix.M11) - matrix.M22) - matrix.M33);
				Double num4 = half / num7;
				result.X = half * num7;
				result.Y = (matrix.M12 + matrix.M21) * num4;
				result.Z = (matrix.M13 + matrix.M31) * num4;
				result.W = (matrix.M23 - matrix.M32) * num4;
			}
			else if (matrix.M22 > matrix.M33)
			{
				Double num6 =RealMaths.Sqrt (((one + matrix.M22) - matrix.M11) - matrix.M33);
				Double num3 = half / num6;
				result.X = (matrix.M21 + matrix.M12) * num3;
				result.Y = half * num6;
				result.Z = (matrix.M32 + matrix.M23) * num3;
				result.W = (matrix.M31 - matrix.M13) * num3;
			}
			else
			{
				Double num5 = RealMaths.Sqrt (((one + matrix.M33) - matrix.M11) - matrix.M22);
				Double num2 = half / num5;
				result.X = (matrix.M31 + matrix.M13) * num2;
				result.Y = (matrix.M32 + matrix.M23) * num2;
				result.Z = half * num5;
				result.W = (matrix.M12 - matrix.M21) * num2;
			}
		}
		
		#endregion
		#region Maths

		public static void Conjugate (ref Quaternion value, out Quaternion result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = value.W;
		}
		
		public static void Inverse (ref Quaternion quaternion, out Quaternion result)
		{
			Double one = 1;
			Double num2 = ( ( (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) ) + 
			                (quaternion.Z * quaternion.Z) ) + (quaternion.W * quaternion.W);

			Double num = one / num2;

			result.X = -quaternion.X * num;
			result.Y = -quaternion.Y * num;
			result.Z = -quaternion.Z * num;
			result.W = quaternion.W * num;
		}
		
		
		public static void Dot (ref Quaternion quaternion1, ref Quaternion quaternion2, out Double result)
		{
			result = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + 
			          (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
		}


		public static void Concatenate (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
		{
			Double x = value2.X;
			Double y = value2.Y;
			Double z = value2.Z;
			Double w = value2.W;
			Double num4 = value1.X;
			Double num3 = value1.Y;
			Double num2 = value1.Z;
			Double num = value1.W;
			Double num12 = (y * num2) - (z * num3);
			Double num11 = (z * num4) - (x * num2);
			Double num10 = (x * num3) - (y * num4);
			Double num9 = ((x * num4) + (y * num3)) + (z * num2);
			result.X = ((x * num) + (num4 * w)) + num12;
			result.Y = ((y * num) + (num3 * w)) + num11;
			result.Z = ((z * num) + (num2 * w)) + num10;
			result.W = (w * num) - num9;
		}
		
		public static void Normalise (ref Quaternion quaternion, out Quaternion result)
		{
			Double one = 1;

			Double num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
			Double num = one / RealMaths.Sqrt (num2);
			result.X = quaternion.X * num;
			result.Y = quaternion.Y * num;
			result.Z = quaternion.Z * num;
			result.W = quaternion.W * num;
		}
		
		#endregion
		#region Operators

		public static Quaternion operator - (Quaternion quaternion)
		{
			Quaternion quaternion2;
			quaternion2.X = -quaternion.X;
			quaternion2.Y = -quaternion.Y;
			quaternion2.Z = -quaternion.Z;
			quaternion2.W = -quaternion.W;
			return quaternion2;
		}
		
		public static Boolean operator == (Quaternion quaternion1, Quaternion quaternion2)
		{
			return ((((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) && (quaternion1.W == quaternion2.W));
		}
		
		public static Boolean operator != (Quaternion quaternion1, Quaternion quaternion2)
		{
			if (((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) {
				return !(quaternion1.W == quaternion2.W);
			}
			return true;
		}
		
		public static Quaternion operator + (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X + quaternion2.X;
			quaternion.Y = quaternion1.Y + quaternion2.Y;
			quaternion.Z = quaternion1.Z + quaternion2.Z;
			quaternion.W = quaternion1.W + quaternion2.W;
			return quaternion;
		}
		
		public static Quaternion operator - (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X - quaternion2.X;
			quaternion.Y = quaternion1.Y - quaternion2.Y;
			quaternion.Z = quaternion1.Z - quaternion2.Z;
			quaternion.W = quaternion1.W - quaternion2.W;
			return quaternion;
		}
		
		public static Quaternion operator * (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			Double x = quaternion1.X;
			Double y = quaternion1.Y;
			Double z = quaternion1.Z;
			Double w = quaternion1.W;
			Double num4 = quaternion2.X;
			Double num3 = quaternion2.Y;
			Double num2 = quaternion2.Z;
			Double num = quaternion2.W;
			Double num12 = (y * num2) - (z * num3);
			Double num11 = (z * num4) - (x * num2);
			Double num10 = (x * num3) - (y * num4);
			Double num9 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.X = ((x * num) + (num4 * w)) + num12;
			quaternion.Y = ((y * num) + (num3 * w)) + num11;
			quaternion.Z = ((z * num) + (num2 * w)) + num10;
			quaternion.W = (w * num) - num9;
			return quaternion;
		}
		
		public static Quaternion operator * (Quaternion quaternion1, Double scaleFactor)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X * scaleFactor;
			quaternion.Y = quaternion1.Y * scaleFactor;
			quaternion.Z = quaternion1.Z * scaleFactor;
			quaternion.W = quaternion1.W * scaleFactor;
			return quaternion;
		}
		
		public static Quaternion operator / (Quaternion quaternion1, Quaternion quaternion2)
		{
			Double one = 1;

			Quaternion quaternion;
			Double x = quaternion1.X;
			Double y = quaternion1.Y;
			Double z = quaternion1.Z;
			Double w = quaternion1.W;
			Double num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
			Double num5 = one / num14;
			Double num4 = -quaternion2.X * num5;
			Double num3 = -quaternion2.Y * num5;
			Double num2 = -quaternion2.Z * num5;
			Double num = quaternion2.W * num5;
			Double num13 = (y * num2) - (z * num3);
			Double num12 = (z * num4) - (x * num2);
			Double num11 = (x * num3) - (y * num4);
			Double num10 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.X = ((x * num) + (num4 * w)) + num13;
			quaternion.Y = ((y * num) + (num3 * w)) + num12;
			quaternion.Z = ((z * num) + (num2 * w)) + num11;
			quaternion.W = (w * num) - num10;
			return quaternion;
		}



		
		public static void Negate (ref Quaternion quaternion, out Quaternion result)
		{
			result.X = -quaternion.X;
			result.Y = -quaternion.Y;
			result.Z = -quaternion.Z;
			result.W = -quaternion.W;
		}

		public static void Add (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			result.X = quaternion1.X + quaternion2.X;
			result.Y = quaternion1.Y + quaternion2.Y;
			result.Z = quaternion1.Z + quaternion2.Z;
			result.W = quaternion1.W + quaternion2.W;
		}
		
		public static void Subtract (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			result.X = quaternion1.X - quaternion2.X;
			result.Y = quaternion1.Y - quaternion2.Y;
			result.Z = quaternion1.Z - quaternion2.Z;
			result.W = quaternion1.W - quaternion2.W;
		}

		public static void Multiply (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			Double x = quaternion1.X;
			Double y = quaternion1.Y;
			Double z = quaternion1.Z;
			Double w = quaternion1.W;
			Double num4 = quaternion2.X;
			Double num3 = quaternion2.Y;
			Double num2 = quaternion2.Z;
			Double num = quaternion2.W;
			Double num12 = (y * num2) - (z * num3);
			Double num11 = (z * num4) - (x * num2);
			Double num10 = (x * num3) - (y * num4);
			Double num9 = ((x * num4) + (y * num3)) + (z * num2);
			result.X = ((x * num) + (num4 * w)) + num12;
			result.Y = ((y * num) + (num3 * w)) + num11;
			result.Z = ((z * num) + (num2 * w)) + num10;
			result.W = (w * num) - num9;
		}

		public static void Multiply (ref Quaternion quaternion1, Double scaleFactor, out Quaternion result)
		{
			result.X = quaternion1.X * scaleFactor;
			result.Y = quaternion1.Y * scaleFactor;
			result.Z = quaternion1.Z * scaleFactor;
			result.W = quaternion1.W * scaleFactor;
		}
		
		public static void Divide (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			Double one = 1;

			Double x = quaternion1.X;
			Double y = quaternion1.Y;
			Double z = quaternion1.Z;
			Double w = quaternion1.W;
			Double num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
			Double num5 = one / num14;
			Double num4 = -quaternion2.X * num5;
			Double num3 = -quaternion2.Y * num5;
			Double num2 = -quaternion2.Z * num5;
			Double num = quaternion2.W * num5;
			Double num13 = (y * num2) - (z * num3);
			Double num12 = (z * num4) - (x * num2);
			Double num11 = (x * num3) - (y * num4);
			Double num10 = ((x * num4) + (y * num3)) + (z * num2);
			result.X = ((x * num) + (num4 * w)) + num13;
			result.Y = ((y * num) + (num3 * w)) + num12;
			result.Z = ((z * num) + (num2 * w)) + num11;
			result.W = (w * num) - num10;
		}
		
		#endregion
		#region Utilities

		public static void Slerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Double amount, out Quaternion result)
		{
			Double zero = 0;
			Double one = 1;
			Double nineninenine; RealMaths.FromString("0.999999", out nineninenine);

			Double num2;
			Double num3;
			Double num = amount;
			Double num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
			Boolean flag = false;
			if (num4 < zero) {
				flag = true;
				num4 = -num4;
			}


			if (num4 >nineninenine) {
				num3 = one - num;
				num2 = flag ? -num : num;
			} else {
				Double num5 = RealMaths.ArcCos (num4);
				Double num6 = one / RealMaths.Sin (num5);

				num3 = RealMaths.Sin ((one - num) * num5) * num6;

				num2 = flag ? -RealMaths.Sin (num * num5) * num6 : RealMaths.Sin (num * num5) * num6;
			}
			result.X = (num3 * quaternion1.X) + (num2 * quaternion2.X);
			result.Y = (num3 * quaternion1.Y) + (num2 * quaternion2.Y);
			result.Z = (num3 * quaternion1.Z) + (num2 * quaternion2.Z);
			result.W = (num3 * quaternion1.W) + (num2 * quaternion2.W);
		}

		public static void Lerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Double amount, out Quaternion result)
		{
			Double zero = 0;
			Double one = 1;

			Double num = amount;
			Double num2 = one - num;
			Double num5 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
			if (num5 >= zero) {
				result.X = (num2 * quaternion1.X) + (num * quaternion2.X);
				result.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
				result.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
				result.W = (num2 * quaternion1.W) + (num * quaternion2.W);
			} else {
				result.X = (num2 * quaternion1.X) - (num * quaternion2.X);
				result.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
				result.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
				result.W = (num2 * quaternion1.W) - (num * quaternion2.W);
			}
			Double num4 = (((result.X * result.X) + (result.Y * result.Y)) + (result.Z * result.Z)) + (result.W * result.W);
			Double num3 = one / RealMaths.Sqrt (num4);
			result.X *= num3;
			result.Y *= num3;
			result.Z *= num3;
			result.W *= num3;
		}
		
		#endregion

	}	[StructLayout (LayoutKind.Sequential)]
	public struct Ray 
		: IEquatable<Ray>
	{
		// The starting position of this ray
		public Vector3 Position;
		
		// Normalised vector that defines the direction of this ray
		public Vector3 Direction;

		public Ray (Vector3 position, Vector3 direction)
		{
			this.Position = position;
			this.Direction = direction;
		}

		// Determines whether or not this ray is equal in value to another ray
		public Boolean Equals (Ray other)
		{
			// Check position
			if (this.Position.X != other.Position.X) return false;
			if (this.Position.Y != other.Position.Y) return false;
			if (this.Position.Z != other.Position.Z) return false;

			// Check direction
			if (this.Direction.X != other.Direction.X) return false;
			if (this.Direction.Y != other.Direction.Y) return false;
			if (this.Direction.Z != other.Direction.Z) return false;

			// They match!
			return true;
		}

		// Determines whether or not this ray is equal in value to another System.Object
		public override Boolean Equals (Object obj)
		{
			if (obj == null) return false;

			if (obj is Ray)
			{
				// Ok, it is a Ray, so just use the method above to compare.
				return this.Equals ((Ray) obj);
			}

			return false;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Position.GetHashCode () + this.Direction.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Position:{0} Direction:{1}}}", this.Position, this.Direction);
		}

		// At what distance from it's starting position does this ray
		// intersect the given box.  Returns null if there is no
		// intersection.
		public Double? Intersects (ref BoundingBox box)
		{
			return box.Intersects (ref this);
		}

		// At what distance from it's starting position does this ray
		// intersect the given frustum.  Returns null if there is no
		// intersection.
		public Double? Intersects (ref BoundingFrustum frustum)
		{
			if (frustum == null)
			{
				throw new ArgumentNullException ();
			}

			return frustum.Intersects (ref this);
		}

		// At what distance from it's starting position does this ray
		// intersect the given plane.  Returns null if there is no
		// intersection.
		public Double? Intersects (ref Plane plane)
		{
			Double zero = 0;

			Double nearZero; RealMaths.FromString("0.00001", out nearZero);

			Double num2 = ((plane.Normal.X * this.Direction.X) + (plane.Normal.Y * this.Direction.Y)) + (plane.Normal.Z * this.Direction.Z);
			
			if (RealMaths.Abs (num2) < nearZero)
			{
				return null;
			}
			
			Double num3 = ((plane.Normal.X * this.Position.X) + (plane.Normal.Y * this.Position.Y)) + (plane.Normal.Z * this.Position.Z);

			Double num = (-plane.D - num3) / num2;
			
			if (num < zero)
			{
				if (num < -nearZero)
				{
					return null;
				}

				num = zero;
			}

			return new Double? (num);
		}

		// At what distance from it's starting position does this ray
		// intersect the given sphere.  Returns null if there is no
		// intersection.
		public Double? Intersects (ref BoundingSphere sphere)
		{
			Double zero = 0;

			Double initialXOffset = sphere.Center.X - this.Position.X;

			Double initialYOffset = sphere.Center.Y - this.Position.Y;
			
			Double initialZOffset = sphere.Center.Z - this.Position.Z;
			
			Double num7 = ((initialXOffset * initialXOffset) + (initialYOffset * initialYOffset)) + (initialZOffset * initialZOffset);

			Double num2 = sphere.Radius * sphere.Radius;

			if (num7 <= num2)
			{
				return zero;
			}

			Double num = ((initialXOffset * this.Direction.X) + (initialYOffset * this.Direction.Y)) + (initialZOffset * this.Direction.Z);
			if (num < zero)
			{
				return null;
			}
			
			Double num6 = num7 - (num * num);
			if (num6 > num2)
			{
				return null;
			}
			
			Double num8 = RealMaths.Sqrt ((num2 - num6));

			return new Double? (num - num8);
		}

		public static Boolean operator == (Ray a, Ray b)
		{
			return a.Equals(b);
		}

		public static Boolean operator != (Ray a, Ray b)
		{
			return !a.Equals(b);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Triangle
		: IEquatable<Triangle>
	{
		public Vector3 A;
		public Vector3 B;
		public Vector3 C;

		public Triangle (Vector3 a, Vector3 b, Vector3 c)
		{
			this.A = a;
			this.B = b;
			this.C = c;
		}

		// Determines whether or not this Triangle is equal in value to another Triangle
		public Boolean Equals (Triangle other)
		{
			if (this.A.X != other.A.X) return false;
			if (this.A.Y != other.A.Y) return false;
			if (this.A.Z != other.A.Z) return false;

			if (this.B.X != other.B.X) return false;
			if (this.B.Y != other.B.Y) return false;
			if (this.B.Z != other.B.Z) return false;

			if (this.C.X != other.C.X) return false;
			if (this.C.Y != other.C.Y) return false;
			if (this.C.Z != other.C.Z) return false;

			// They match!
			return true;
		}

		// Determines whether or not this Triangle is equal in value to another System.Object
		public override Boolean Equals (Object obj)
		{
			if (obj == null) return false;

			if (obj is Triangle)
			{
				// Ok, it is a Triangle, so just use the method above to compare.
				return this.Equals ((Triangle) obj);
			}

			return false;
		}

		public override Int32 GetHashCode ()
		{
			return (this.A.GetHashCode () + this.B.GetHashCode () + this.C.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{A:{0} B:{1} C:{2}}}", this.A, this.B, this.C);
		}

		public static Boolean operator == (Triangle a, Triangle b)
		{
			return a.Equals(b);
		}

		public static Boolean operator != (Triangle a, Triangle b)
		{
			return !a.Equals(b);
		}

		public static Boolean IsPointInTriangleangle( ref Vector3 point, ref Triangle triangle )
		{
			Vector3 aToB = triangle.B - triangle.A;
			Vector3 bToC = triangle.C - triangle.B;

			Vector3 n; Vector3.Cross(ref aToB, ref bToC, out n);

			Vector3 aToPoint = point - triangle.A;

			Vector3 wTest; Vector3.Cross(ref aToB, ref aToPoint, out wTest);

			Double zero = 0;

			Double dot; Vector3.Dot(ref wTest, ref n, out dot);

			if ( dot < zero )
			{
				return false;
			}

			Vector3 bToPoint = point - triangle.B;

			Vector3.Cross(ref bToC, ref bToPoint, out wTest);

			Vector3.Dot(ref wTest, ref n, out dot);

			if ( dot < zero )
			{
				return false;
			}

			Vector3 cToA = triangle.A - triangle.C;

			Vector3 cToPoint = point - triangle.C;

			Vector3.Cross(ref cToA, ref cToPoint, out wTest);

			Vector3.Dot(ref wTest, ref n, out dot);

			if ( dot < zero )
			{
				return false;
			}

			return true;
		}

		// Determines whether or not a triangle is degenerate ( all points lay on the same line in space ).
		public Boolean IsDegenerate()
		{
			throw new System.NotImplementedException();
		}

		// Get's the Barycentric coordinates of a point inside a Triangle.
		public static void BarycentricCoordinates( ref Vector3 point, ref Triangle triangle, out Vector3 barycentricCoordinates )
		{
			if( triangle.IsDegenerate() )
			{
				throw new System.ArgumentException("Input Triangle is degenerate, this is not supported.");
			}

			Vector3 aToB = triangle.B - triangle.A;
			Vector3 aToC = triangle.C - triangle.A;
			Vector3 aToPoint = point - triangle.A;

			// compute cross product to get area of parallelograms
			Vector3 cross1; Vector3.Cross(ref aToB, ref aToPoint, out cross1);
			Vector3 cross2; Vector3.Cross(ref aToC, ref aToPoint, out cross2);
			Vector3 cross3; Vector3.Cross(ref aToB, ref aToC, out cross3);
	
			// compute barycentric coordinates as ratios of areas

			Double one = 1;

			Double denom = one / cross3.Length();
			barycentricCoordinates.X = cross2.Length() * denom;
			barycentricCoordinates.Y = cross1.Length() * denom;
			barycentricCoordinates.Z = one - barycentricCoordinates.X - barycentricCoordinates.Y;
		}
		/*

		// Triangleangle Intersect
		// ------------------
		// Returns true if triangles P0P1P2 and Q0Q1Q2 intersect
		// Assumes triangle is not degenerate
		//
		// This is not the algorithm presented in the text.  Instead, it is based on a 
		// recent article by Guigue and Devillers in the July 2003 issue Journal of 
		// Graphics Tools.  As it is faster than the ERIT algorithm, under ordinary 
		// circumstances it would have been discussed in the text, but it arrived too late.  
		//
		// More information and the original source code can be found at
		// http://www.acm.org/jgt/papers/GuigueDevillers03/
		//
		// A nearly identical algorithm was in the same issue of JGT, by Shen Heng and 
		// Tang.  See http://www.acm.org/jgt/papers/ShenHengTang03/ for source code.
		//
		// Yes, this is complicated.  Did you think testing triangles would be easy?
		//
		static Boolean TriangleangleIntersect( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2 )
		{
			// test P against Q's plane
			Vector3 normalQ = Vector3.Cross( Q1 - Q0, Q2 - Q0 );

			Single testP0 = Vector3.Dot( normalQ, P0 - Q0 );
			Single testP1 = Vector3.Dot( normalQ, P1 - Q0 );
			Single testP2 = Vector3.Dot( normalQ, P2 - Q0 );
  
			// P doesn't intersect Q's plane
			if ( testP0 * testP1 > AbacusHelper.Epsilon && testP0*testP2 > AbacusHelper.Epsilon )
				return false;

			// test Q against P's plane
			Vector3 normalP = Vector3.Cross( P1 - P0, P2 - P0 );

			Single testQ0 = Vector3.Dot( normalP, Q0 - P0 );
			Single testQ1 = Vector3.Dot( normalP, Q1 - P0 );
			Single testQ2 = Vector3.Dot( normalP, Q2 - P0 );
  
			// Q doesn't intersect P's plane
			if (testQ0*testQ1 > AbacusHelper.Epsilon && testQ0*testQ2 > AbacusHelper.Epsilon )
				return false;
	
			// now we rearrange P's vertices such that the lone vertex (the one that lies
			// in its own half-space of Q) is first.  We also permute the other
			// triangle's vertices so that P0 will "see" them in counterclockwise order

			// Once reordered, we pass the vertices down to a helper function which will
			// reorder Q's vertices, and then test

			// P0 in Q's positive half-space
			if (testP0 > AbacusHelper.Epsilon) 
			{
				// P1 in Q's positive half-space (so P2 is lone vertex)
				if (testP1 > AbacusHelper.Epsilon) 
					return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
				// P2 in Q's positive half-space (so P1 is lone vertex)
				else if (testP2 > AbacusHelper.Epsilon) 
					return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);	
				// P0 is lone vertex
				else 
					return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
			} 
			// P0 in Q's negative half-space
			else if (testP0 < -AbacusHelper.Epsilon) 
			{
				// P1 in Q's negative half-space (so P2 is lone vertex)
				if (testP1 < -AbacusHelper.Epsilon) 
					return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
				// P2 in Q's negative half-space (so P1 is lone vertex)
				else if (testP2 < -AbacusHelper.Epsilon) 
					return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
				// P0 is lone vertex
				else 
					return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
			} 
			// P0 on Q's plane
			else 
			{
				// P1 in Q's negative half-space 
				if (testP1 < -AbacusHelper.Epsilon) 
				{
					// P2 in Q's negative half-space (P0 is lone vertex)
					if (testP2 < -AbacusHelper.Epsilon) 
						return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
					// P2 in positive half-space or on plane (P1 is lone vertex)
					else 
						return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
				}
				// P1 in Q's positive half-space 
				else if (testP1 > AbacusHelper.Epsilon) 
				{
					// P2 in Q's positive half-space (P0 is lone vertex)
					if (testP2 > AbacusHelper.Epsilon) 
						return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
					// P2 in negative half-space or on plane (P1 is lone vertex)
					else 
						return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
				}
				// P1 lies on Q's plane too
				else  
				{
					// P2 in Q's positive half-space (P2 is lone vertex)
					if (testP2 > AbacusHelper.Epsilon) 
						return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
					// P2 in Q's negative half-space (P2 is lone vertex)
					// note different ordering for Q vertices
					else if (testP2 < -AbacusHelper.Epsilon) 
						return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
					// all three points lie on Q's plane, default to 2D test
					else 
						return CoplanarTriangleangleIntersect(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, ref normalP);
				}
			}
		}



		// Adjust Q
		// --------
		// Helper for TriangleangleIntersect()
		//
		// Now we rearrange Q's vertices such that the lone vertex (the one that lies
		// in its own half-space of P) is first.  We also permute the other
		// triangle's vertices so that Q0 will "see" them in counterclockwise order
		//
		// Once reordered, we pass the vertices down to a helper function which will
		// actually test for intersection on the common line between the two planes
		//
		static Boolean AdjustQ( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2,
			Single testQ0, Single testQ1, Single testQ2,
			ref Vector3 normalP )
		{

			// Q0 in P's positive half-space
			if (testQ0 > AbacusHelper.Epsilon) 
			{ 
				// Q1 in P's positive half-space (so Q2 is lone vertex)
				if (testQ1 > AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P2, ref P1, ref Q2, ref Q0, ref Q1);
				// Q2 in P's positive half-space (so Q1 is lone vertex)
				else if (testQ2 > AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P2, ref P1, ref Q1, ref Q2, ref Q0);
				// Q0 is lone vertex
				else 
					return TestLineOverlap(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2);
			}
			// Q0 in P's negative half-space
			else if (testQ0 < -AbacusHelper.Epsilon) 
			{ 
				// Q1 in P's negative half-space (so Q2 is lone vertex)
				if (testQ1 < -AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P1, ref P2, ref Q2, ref Q0, ref Q1);
				// Q2 in P's negative half-space (so Q1 is lone vertex)
				else if (testQ2 < -AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P1, ref P2, ref Q1, ref Q2, ref Q0);
				// Q0 is lone vertex
				else 
					return TestLineOverlap(ref P0, ref P2, ref P1, ref Q0, ref Q1, ref Q2);
			}
			// Q0 on P's plane
			else 
			{ 
				// Q1 in P's negative half-space 
				if (testQ1 < -AbacusHelper.Epsilon) 
				{ 
					// Q2 in P's negative half-space (Q0 is lone vertex)
					if (testQ2 < -AbacusHelper.Epsilon)  
						return TestLineOverlap(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2);
					// Q2 in positive half-space or on plane (P1 is lone vertex)
					else 
						return TestLineOverlap(ref P0, ref P2, ref P1, ref Q1, ref Q2, ref Q0);
				}
				// Q1 in P's positive half-space 
				else if (testQ1 > AbacusHelper.Epsilon) 
				{ 
					// Q2 in P's positive half-space (Q0 is lone vertex)
					if (testQ2 > AbacusHelper.Epsilon) 
						return TestLineOverlap(ref P0, ref P2, ref P1, ref Q0, ref Q1, ref Q2);
					// Q2 in negative half-space or on plane (P1 is lone vertex)
					else  
						return TestLineOverlap(ref P0, ref P1, ref P2, ref Q1, ref Q2, ref Q0);
				}
				// Q1 lies on P's plane too
				else 
				{
					// Q2 in P's positive half-space (Q2 is lone vertex)
					if (testQ2 > AbacusHelper.Epsilon) 
						return TestLineOverlap(ref P0, ref P1, ref P2, ref Q2, ref Q0, ref Q1);
					// Q2 in P's negative half-space (Q2 is lone vertex)
					// note different ordering for Q vertices
					else if (testQ2 < -AbacusHelper.Epsilon) 
						return TestLineOverlap(ref P0, ref P2, ref P1, ref Q2, ref Q0, ref Q1);
					// all three points lie on P's plane, default to 2D test
					else 
						return CoplanarTriangleangleIntersect(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, ref normalP);
				}
			}
		}



		// Test Line Overlap
		// -----------------
		// Helper for TriangleangleIntersect()
		//
		// This tests whether the rearranged triangles overlap, by checking the intervals
		// where their edges cross the common line between the two planes.  If the 
		// interval for P is [i,j] and Q is [k,l], then there is intersection if the
		// intervals overlap.  Previous algorithms computed these intervals directly, 
		// this tests implictly by using two "plane tests."
		//
		static Boolean TestLineOverlap( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2 )
		{
			// get "plane normal"
			Vector3 normal = Vector3.Cross( P1 - P0, Q0 - P0);

			// fails test, no intersection
			if ( Vector3.Dot(normal, Q1 - P0 ) > AbacusHelper.Epsilon )
				return false;
  
			// get "plane normal"
			normal = Vector3.Cross( P2 - P0, Q2 - P0 );

			// fails test, no intersection
			if ( Vector3.Dot( normal, Q0 - P0 ) > AbacusHelper.Epsilon )
				return false;

			// intersection!
			return true;
		}



		// Coplanar Triangleangle Intersect
		// ---------------------------
		// Helper for TriangleangleIntersect()
		//
		// This projects the two triangles down to 2D, maintaining the largest area by
		// dropping the dimension where the normal points the farthest.
		//
		static Boolean CoplanarTriangleangleIntersect( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2, 
			ref Vector3 planeNormal )
		{
			Vector3 absNormal = new Vector3( 
				System.Math.Abs(planeNormal.X), 
				System.Math.Abs(planeNormal.Y), 
				System.Math.Abs(planeNormal.Z) );

			Vector2 projP0, projP1, projP2;
			Vector2 projQ0, projQ1, projQ2;

			// if x is direction of largest magnitude
			if ( absNormal.X > absNormal.Y && absNormal.X >= absNormal.Z )
			{
				projP0 = new Vector2( P0.Y, P0.Z );
				projP1 = new Vector2( P1.Y, P1.Z );
				projP2 = new Vector2( P2.Y, P2.Z );
				projQ0 = new Vector2( Q0.Y, Q0.Z );
				projQ1 = new Vector2( Q1.Y, Q1.Z );
				projQ2 = new Vector2( Q2.Y, Q2.Z );
			}
			// if y is direction of largest magnitude
			else if ( absNormal.Y > absNormal.X && absNormal.Y >= absNormal.Z )
			{
				projP0 = new Vector2( P0.X, P0.Z );
				projP1 = new Vector2( P1.X, P1.Z );
				projP2 = new Vector2( P2.X, P2.Z );
				projQ0 = new Vector2( Q0.X, Q0.Z );
				projQ1 = new Vector2( Q1.X, Q1.Z );
				projQ2 = new Vector2( Q2.X, Q2.Z );
			}
			// z is the direction of largest magnitude
			else
			{
				projP0 = new Vector2( P0.X, P0.Y );
				projP1 = new Vector2( P1.X, P1.Y );
				projP2 = new Vector2( P2.X, P2.Y );
				projQ0 = new Vector2( Q0.X, Q0.Y );
				projQ1 = new Vector2( Q1.X, Q1.Y );
				projQ2 = new Vector2( Q2.X, Q2.Y );
			}

			return TriangleangleIntersect( ref projP0, ref projP1, ref projP2, ref projQ0, ref projQ1, ref projQ2 );
		}



		// Triangleangle Intersect
		// ------------------
		// Returns true if ray intersects triangle.
		//
		static Boolean TriangleangleIntersect( 
			ref Single t, //perhaps this should be out 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Ray ray )
		{
			// test ray direction against triangle
			Vector3 e1 = P1 - P0;
			Vector3 e2 = P2 - P0;
			Vector3 p = Vector3.Cross( ray.Direction, e2 );
			Single a = Vector3.Dot( e1, p );

			// if result zero, no intersection or infinite intersections
			// (ray parallel to triangle plane)
			if ( AbacusHelper.IsZero(a) )
				return false;

			// compute denominator
			Single f = 1.0f/a;

			// compute barycentric coordinates
			Vector3 s = ray.Position - P0;
			Single u = f * Vector3.Dot( s, p );

			// ray falls outside triangle
			if (u < 0.0f || u > 1.0f) 
				return false;

			Vector3 q = Vector3.Cross( s, e1 );
			Single v = f * Vector3.Dot( ray.Direction, q );

			// ray falls outside triangle
			if (v < 0.0f || u+v > 1.0f) 
				return false;

			// compute line parameter
			t = f * Vector3.Dot( e2, q );

			return (t >= 0.0f);
		}

		
		//
		// @ TriangleangleClassify()
		// Returns signed distance between plane and triangle
		//
		static Single TriangleangleClassify( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Plane plane )
		{
			Single test0 = plane.Test( P0 );
			Single test1 = plane.Test( P1 );

			// if two points lie on opposite sides of plane, intersect
			if (test0*test1 < 0.0f)
				return 0.0f;

			Single test2 = plane.Test( P2 );

			// if two points lie on opposite sides of plane, intersect
			if (test0*test2 < 0.0f)
				return 0.0f;
			if (test1*test2 < 0.0f)
				return 0.0f;

			// no intersection, return signed distance
			if ( test0 < 0.0f )
			{
				if ( test0 < test1 )
				{
					if ( test1 < test2 )
						return test2;
					else
						return test1;
				}
				else if (test0 < test2)
				{
					return test2;
				}
				else
				{   
					return test0;
				}
			}
			else
			{
				if ( test0 > test1 )
				{
					if ( test1 > test2 )
						return test2;
					else
						return test1;
				}
				else if (test0 > test2)
				{
					return test2;
				}
				else
				{   
					return test0;
				}
			}
		}

		*/
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Vector2
		: IEquatable<Vector2>
	{
		public Double X;
		public Double Y;
		
		public Vector2 (Int32 x, Int32 y)
		{
			this.X = (Double) x;
			this.Y = (Double) y;
		}

		public Vector2 (Double x, Double y)
		{
			this.X = x;
			this.Y = y;
		}

		public void Set (Double x, Double y)
		{
			this.X = x;
			this.Y = y;
		}

		public Vector2 (Double value)
		{
			this.X = this.Y = value;
		}

		public Double Length ()
		{
			Double num = (this.X * this.X) + (this.Y * this.Y);
			return RealMaths.Sqrt (num);
		}

		public Double LengthSquared ()
		{
			return ((this.X * this.X) + (this.Y * this.Y));
		}

		public void Normalise ()
		{
			Double num2 = (this.X * this.X) + (this.Y * this.Y);

			Double one = 1;
			Double num = one / (RealMaths.Sqrt (num2));
			this.X *= num;
			this.Y *= num;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1}}}", new Object[] { this.X.ToString (), this.Y.ToString () });
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Vector2) {
				flag = this.Equals ((Vector2)obj);
			}
			return flag;
		}
		
		public override Int32 GetHashCode ()
		{
			return (this.X.GetHashCode () + this.Y.GetHashCode ());
		}

		public Boolean IsUnit()
		{
			Double one = 1;

			return RealMaths.IsZero(one - X*X - Y*Y);
		}

		#region IEquatable<Vector2>
		public Boolean Equals (Vector2 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y));
		}
		#endregion

		#region Constants

		static Vector2 zero;
		static Vector2 one;
		static Vector2 unitX;
		static Vector2 unitY;

		static Vector2 ()
		{
			Double temp_one; RealMaths.One(out temp_one);
			Double temp_zero; RealMaths.Zero(out temp_zero);

			zero = new Vector2 ();
			one = new Vector2 (temp_one, temp_one);
			unitX = new Vector2 (temp_one, temp_zero);
			unitY = new Vector2 (temp_zero, temp_one);
		}

		public static Vector2 Zero
		{
			get { return zero; }
		}
		
		public static Vector2 One
		{
			get { return one; }
		}
		
		public static Vector2 UnitX
		{
			get { return unitX; }
		}
		
		public static Vector2 UnitY
		{
			get { return unitY; }
		}
		
		#endregion
		#region Maths

		public static void Distance (ref Vector2 value1, ref Vector2 value2, out Double result)
		{
			Double num2 = value1.X - value2.X;
			Double num = value1.Y - value2.Y;
			Double num3 = (num2 * num2) + (num * num);
			result = RealMaths.Sqrt (num3);
		}

		public static void DistanceSquared (ref Vector2 value1, ref Vector2 value2, out Double result)
		{
			Double num2 = value1.X - value2.X;
			Double num = value1.Y - value2.Y;
			result = (num2 * num2) + (num * num);
		}

		public static void Dot (ref Vector2 value1, ref Vector2 value2, out Double result)
		{
			result = (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		public static void PerpDot (ref Vector2 value1, ref Vector2 value2, out Double result)
		{
			result = (value1.X * value2.Y - value1.Y * value2.X);
		}

		public static void Perpendicular (ref Vector2 value, out Vector2 result)
		{
			result = new Vector2 (-value.X, value.Y);
		}

		public static void Normalise (ref Vector2 value, out Vector2 result)
		{
			Double one = 1;

			Double num2 = (value.X * value.X) + (value.Y * value.Y);
			Double num = one / (RealMaths.Sqrt (num2));
			result.X = value.X * num;
			result.Y = value.Y * num;
		}

		public static void Reflect (ref Vector2 vector, ref Vector2 normal, out Vector2 result)
		{
			Double two = 2;

			Double num = (vector.X * normal.X) + (vector.Y * normal.Y);
			result.X = vector.X - ((two * num) * normal.X);
			result.Y = vector.Y - ((two * num) * normal.Y);
		}
		
		public static void Transform (ref Vector2 position, ref Matrix44 matrix, out Vector2 result)
		{
			Double num2 = ((position.X * matrix.M11) + (position.Y * matrix.M21)) + matrix.M41;
			Double num = ((position.X * matrix.M12) + (position.Y * matrix.M22)) + matrix.M42;
			result.X = num2;
			result.Y = num;
		}
		
		public static void TransformNormal (ref Vector2 normal, ref Matrix44 matrix, out Vector2 result)
		{
			Double num2 = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
			Double num = (normal.X * matrix.M12) + (normal.Y * matrix.M22);
			result.X = num2;
			result.Y = num;
		}
		
		public static void Transform (ref Vector2 value, ref Quaternion rotation, out Vector2 result)
		{
			Double one = 1;

			Double num10 = rotation.X + rotation.X;
			Double num5 = rotation.Y + rotation.Y;
			Double num4 = rotation.Z + rotation.Z;
			Double num3 = rotation.W * num4;
			Double num9 = rotation.X * num10;
			Double num2 = rotation.X * num5;
			Double num8 = rotation.Y * num5;
			Double num = rotation.Z * num4;
			Double num7 = (value.X * ((one - num8) - num)) + (value.Y * (num2 - num3));
			Double num6 = (value.X * (num2 + num3)) + (value.Y * ((one - num9) - num));
			result.X = num7;
			result.Y = num6;
		}
		
		#endregion
		#region Operators

		public static Vector2 operator - (Vector2 value)
		{
			Vector2 vector;
			vector.X = -value.X;
			vector.Y = -value.Y;
			return vector;
		}
		
		public static Boolean operator == (Vector2 value1, Vector2 value2)
		{
			return ((value1.X == value2.X) && (value1.Y == value2.Y));
		}
		
		public static Boolean operator != (Vector2 value1, Vector2 value2)
		{
			if (value1.X == value2.X) {
				return !(value1.Y == value2.Y);
			}
			return true;
		}

		public static Vector2 operator + (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			return vector;
		}

		public static Vector2 operator - (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			return vector;
		}

		public static Vector2 operator * (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X * value2.X;
			vector.Y = value1.Y * value2.Y;
			return vector;
		}
		
		public static Vector2 operator * (Vector2 value, Double scaleFactor)
		{
			Vector2 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}
		
		public static Vector2 operator * (Double scaleFactor, Vector2 value)
		{
			Vector2 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}

		public static Vector2 operator / (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			return vector;
		}
		
		public static Vector2 operator / (Vector2 value1, Double divider)
		{
			Vector2 vector;
			Double one = 1;
			Double num = one / divider;
			vector.X = value1.X * num;
			vector.Y = value1.Y * num;
			return vector;
		}
		
		public static void Negate (ref Vector2 value, out Vector2 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		public static void Add (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		public static void Subtract (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		public static void Multiply (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}
		
		public static void Multiply (ref Vector2 value1, Double scaleFactor, out Vector2 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		public static void Divide (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		public static void Divide (ref Vector2 value1, Double divider, out Vector2 result)
		{
			Double one = 1;
			Double num = one / divider;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
		}
		
		#endregion
		#region Splines

		public static void Barycentric (ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, Double amount1, Double amount2, out Vector2 result)
		{
			result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
			result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
		}

		public static void SmoothStep (ref Vector2 value1, ref Vector2 value2, Double amount, out Vector2 result)
		{
			Double zero = 0;
			Double one = 1;
			Double two = 2;
			Double three = 3;

			amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
			amount = (amount * amount) * (three - (two * amount));
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
		}
		
		public static void CatmullRom (ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, Double amount, out Vector2 result)
		{
			Double half; RealMaths.Half(out half);
			Double two = 2;
			Double three = 3;
			Double four = 4;
			Double five = 5;

			Double num = amount * amount;
			Double num2 = amount * num;
			result.X = half * ((((two * value2.X) + ((-value1.X + value3.X) * amount)) + (((((two * value1.X) - (five * value2.X)) + (four * value3.X)) - value4.X) * num)) + ((((-value1.X + (three * value2.X)) - (three * value3.X)) + value4.X) * num2));
			result.Y = half * ((((two * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((two * value1.Y) - (five * value2.Y)) + (four * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (three * value2.Y)) - (three * value3.Y)) + value4.Y) * num2));
		}

		public static void Hermite (ref Vector2 value1, ref Vector2 tangent1, ref Vector2 value2, ref Vector2 tangent2, Double amount, out Vector2 result)
		{
			Double one = 1;
			Double two = 2;
			Double three = 3;

			Double num = amount * amount;
			Double num2 = amount * num;
			Double num6 = ((two * num2) - (three * num)) + one;
			Double num5 = (-two * num2) + (three * num);
			Double num4 = (num2 - (two * num)) + amount;
			Double num3 = num2 - num;
			result.X = (((value1.X * num6) + (value2.X * num5)) + (tangent1.X * num4)) + (tangent2.X * num3);
			result.Y = (((value1.Y * num6) + (value2.Y * num5)) + (tangent1.Y * num4)) + (tangent2.Y * num3);
		}
		
		#endregion
		#region Utilities

		public static void Min (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
		}

		public static void Max (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
		}

		public static void Clamp (ref Vector2 value1, ref Vector2 min, ref Vector2 max, out Vector2 result)
		{
			Double x = value1.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;
			Double y = value1.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;
			result.X = x;
			result.Y = y;
		}
		
		public static void Lerp (ref Vector2 value1, ref Vector2 value2, Double amount, out Vector2 result)
		{
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
		}
		
		#endregion

	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Vector3 
		: IEquatable<Vector3>
	{
		public Double X;
		public Double Y;
		public Double Z;

		public Vector2 XY
		{
			get
			{
				return new Vector2(X, Y);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}



		public Vector3 (Double x, Double y, Double z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public Vector3 (Double value)
		{
			this.X = this.Y = this.Z = value;
		}
		
		public Vector3 (Vector2 value, Double z)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1} Z:{2}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString () });
		}

		public Boolean Equals (Vector3 other)
		{
			return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Vector3) {
				flag = this.Equals ((Vector3)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return ((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ());
		}

		public Double Length ()
		{
			Double num = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
			return RealMaths.Sqrt (num);
		}

		public Double LengthSquared ()
		{
			return (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
		}


		public void Normalise ()
		{
			Double one = 1;
			Double num2 = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
			Double num = one / RealMaths.Sqrt (num2);
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
		}

		public Boolean IsUnit()
		{
			Double one = 1;

			return RealMaths.IsZero(one - X*X - Y*Y - Z*Z);
		}

		#region Constants

		static Vector3 _zero;
		static Vector3 _one;
		static Vector3 _half;
		static Vector3 _unitX;
		static Vector3 _unitY;
		static Vector3 _unitZ;
		static Vector3 _up;
		static Vector3 _down;
		static Vector3 _right;
		static Vector3 _left;
		static Vector3 _forward;
		static Vector3 _backward;

		static Vector3 ()
		{
			Double temp_one; RealMaths.One(out temp_one);
			Double temp_half; RealMaths.Half(out temp_half);
			Double temp_zero; RealMaths.Zero(out temp_zero);

			_zero = new Vector3 ();
			_one = new Vector3 (temp_one, temp_one, temp_one);
			_half = new Vector3(temp_half, temp_half, temp_half);
			_unitX = new Vector3 (temp_one, temp_zero, temp_zero);
			_unitY = new Vector3 (temp_zero, temp_one, temp_zero);
			_unitZ = new Vector3 (temp_zero, temp_zero, temp_one);
			_up = new Vector3 (temp_zero, temp_one, temp_zero);
			_down = new Vector3 (temp_zero, -temp_one, temp_zero);
			_right = new Vector3 (temp_one, temp_zero, temp_zero);
			_left = new Vector3 (-temp_one, temp_zero, temp_zero);
			_forward = new Vector3 (temp_zero, temp_zero, -temp_one);
			_backward = new Vector3 (temp_zero, temp_zero, temp_one);
		}
		
		public static Vector3 Zero {
			get {
				return _zero;
			}
		}
		
		public static Vector3 One {
			get {
				return _one;
			}
		}
		
		public static Vector3 Half {
			get {
				return _half;
			}
		}
		
		public static Vector3 UnitX {
			get {
				return _unitX;
			}
		}
		
		public static Vector3 UnitY {
			get {
				return _unitY;
			}
		}
		
		public static Vector3 UnitZ {
			get {
				return _unitZ;
			}
		}
		
		public static Vector3 Up {
			get {
				return _up;
			}
		}
		
		public static Vector3 Down {
			get {
				return _down;
			}
		}
		
		public static Vector3 Right {
			get {
				return _right;
			}
		}
		
		public static Vector3 Left {
			get {
				return _left;
			}
		}
		
		public static Vector3 Forward {
			get {
				return _forward;
			}
		}
		
		public static Vector3 Backward {
			get {
				return _backward;
			}
		}
		
		#endregion
		#region Maths

		public static void Distance (ref Vector3 value1, ref Vector3 value2, out Double result)
		{
			Double num3 = value1.X - value2.X;
			Double num2 = value1.Y - value2.Y;
			Double num = value1.Z - value2.Z;
			Double num4 = ((num3 * num3) + (num2 * num2)) + (num * num);
			result = RealMaths.Sqrt (num4);
		}
		
		public static void DistanceSquared (ref Vector3 value1, ref Vector3 value2, out Double result)
		{
			Double num3 = value1.X - value2.X;
			Double num2 = value1.Y - value2.Y;
			Double num = value1.Z - value2.Z;
			result = ((num3 * num3) + (num2 * num2)) + (num * num);
		}

		public static void Dot (ref Vector3 vector1, ref Vector3 vector2, out Double result)
		{
			result = ((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z);
		}

		public static void Normalise (ref Vector3 value, out Vector3 result)
		{
			Double one = 1;

			Double num2 = ((value.X * value.X) + (value.Y * value.Y)) + (value.Z * value.Z);
			Double num = one / RealMaths.Sqrt (num2);
			result.X = value.X * num;
			result.Y = value.Y * num;
			result.Z = value.Z * num;
		}

		public static void Cross (ref Vector3 vector1, ref Vector3 vector2, out Vector3 result)
		{
			Double num3 = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y);
			Double num2 = (vector1.Z * vector2.X) - (vector1.X * vector2.Z);
			Double num = (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
			result.X = num3;
			result.Y = num2;
			result.Z = num;
		}

		public static void Reflect (ref Vector3 vector, ref Vector3 normal, out Vector3 result)
		{
			Double two = 2;

			Double num = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
			result.X = vector.X - ((two * num) * normal.X);
			result.Y = vector.Y - ((two * num) * normal.Y);
			result.Z = vector.Z - ((two * num) * normal.Z);
		}

		public static void Transform (ref Vector3 position, ref Matrix44 matrix, out Vector3 result)
		{
			Double num3 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
			Double num2 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
			Double num = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
			result.X = num3;
			result.Y = num2;
			result.Z = num;
		}
		
		public static void TransformNormal (ref Vector3 normal, ref Matrix44 matrix, out Vector3 result)
		{
			Double num3 = ((normal.X * matrix.M11) + (normal.Y * matrix.M21)) + (normal.Z * matrix.M31);
			Double num2 = ((normal.X * matrix.M12) + (normal.Y * matrix.M22)) + (normal.Z * matrix.M32);
			Double num = ((normal.X * matrix.M13) + (normal.Y * matrix.M23)) + (normal.Z * matrix.M33);
			result.X = num3;
			result.Y = num2;
			result.Z = num;
		}
		
		public static void Transform (ref Vector3 value, ref Quaternion rotation, out Vector3 result)
		{
			Double one = 1;
			Double num12 = rotation.X + rotation.X;
			Double num2 = rotation.Y + rotation.Y;
			Double num = rotation.Z + rotation.Z;
			Double num11 = rotation.W * num12;
			Double num10 = rotation.W * num2;
			Double num9 = rotation.W * num;
			Double num8 = rotation.X * num12;
			Double num7 = rotation.X * num2;
			Double num6 = rotation.X * num;
			Double num5 = rotation.Y * num2;
			Double num4 = rotation.Y * num;
			Double num3 = rotation.Z * num;
			Double num15 = ((value.X * ((one - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
			Double num14 = ((value.X * (num7 + num9)) + (value.Y * ((one - num8) - num3))) + (value.Z * (num4 - num11));
			Double num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((one - num8) - num5));
			result.X = num15;
			result.Y = num14;
			result.Z = num13;
		}
		
		#endregion
		#region Operators

		public static Vector3 operator - (Vector3 value)
		{
			Vector3 vector;
			vector.X = -value.X;
			vector.Y = -value.Y;
			vector.Z = -value.Z;
			return vector;
		}
		
		public static Boolean operator == (Vector3 value1, Vector3 value2)
		{
			return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z));
		}
		
		public static Boolean operator != (Vector3 value1, Vector3 value2)
		{
			if ((value1.X == value2.X) && (value1.Y == value2.Y)) {
				return !(value1.Z == value2.Z);
			}
			return true;
		}
		
		public static Vector3 operator + (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			vector.Z = value1.Z + value2.Z;
			return vector;
		}
		
		public static Vector3 operator - (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			vector.Z = value1.Z - value2.Z;
			return vector;
		}
		
		public static Vector3 operator * (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X * value2.X;
			vector.Y = value1.Y * value2.Y;
			vector.Z = value1.Z * value2.Z;
			return vector;
		}
		
		public static Vector3 operator * (Vector3 value, Double scaleFactor)
		{
			Vector3 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			return vector;
		}
		
		public static Vector3 operator * (Double scaleFactor, Vector3 value)
		{
			Vector3 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			return vector;
		}
		
		public static Vector3 operator / (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			vector.Z = value1.Z / value2.Z;
			return vector;
		}
		
		public static Vector3 operator / (Vector3 value, Double divider)
		{
			Vector3 vector;
			Double one = 1;

			Double num = one / divider;
			vector.X = value.X * num;
			vector.Y = value.Y * num;
			vector.Z = value.Z * num;
			return vector;
		}

		public static void Negate (ref Vector3 value, out Vector3 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
		}

		public static void Add (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
		}

		public static void Subtract (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
		}

		public static void Multiply (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
		}

		public static void Multiply (ref Vector3 value1, Double scaleFactor, out Vector3 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
		}

		public static void Divide (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
		}

		public static void Divide (ref Vector3 value1, Double value2, out Vector3 result)
		{
			Double one = 1;
			Double num = one / value2;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
		}
		
		#endregion
		#region Splines

		public static void Barycentric (ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, Double amount1, Double amount2, out Vector3 result)
		{
			result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
			result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
			result.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
		}
	
		public static void SmoothStep (ref Vector3 value1, ref Vector3 value2, Double amount, out Vector3 result)
		{
			Double zero = 0;
			Double one = 1;
			Double two = 2;
			Double three = 3;

			amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
			amount = (amount * amount) * (three - (two * amount));
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
		}

		public static void CatmullRom (ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4, Double amount, out Vector3 result)
		{
			Double half; RealMaths.Half(out half);
			Double two = 2;
			Double three = 3;
			Double four = 4;
			Double five = 5;

			Double num = amount * amount;
			Double num2 = amount * num;
			result.X = half * ((((two * value2.X) + ((-value1.X + value3.X) * amount)) + (((((two * value1.X) - (five * value2.X)) + (four * value3.X)) - value4.X) * num)) + ((((-value1.X + (three * value2.X)) - (three * value3.X)) + value4.X) * num2));
			result.Y = half * ((((two * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((two * value1.Y) - (five * value2.Y)) + (four * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (three * value2.Y)) - (three * value3.Y)) + value4.Y) * num2));
			result.Z = half * ((((two * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((two * value1.Z) - (five * value2.Z)) + (four * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (three * value2.Z)) - (three * value3.Z)) + value4.Z) * num2));
		}

		public static void Hermite (ref Vector3 value1, ref Vector3 tangent1, ref Vector3 value2, ref Vector3 tangent2, Double amount, out Vector3 result)
		{
			Double one = 1;
			Double two = 2;
			Double three = 3;

			Double num = amount * amount;
			Double num2 = amount * num;
			Double num6 = ((two * num2) - (three * num)) + one;
			Double num5 = (-two * num2) + (three * num);
			Double num4 = (num2 - (two * num)) + amount;
			Double num3 = num2 - num;
			result.X = (((value1.X * num6) + (value2.X * num5)) + (tangent1.X * num4)) + (tangent2.X * num3);
			result.Y = (((value1.Y * num6) + (value2.Y * num5)) + (tangent1.Y * num4)) + (tangent2.Y * num3);
			result.Z = (((value1.Z * num6) + (value2.Z * num5)) + (tangent1.Z * num4)) + (tangent2.Z * num3);
		}
		
		#endregion
		#region Utilities

		public static void Min (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z < value2.Z) ? value1.Z : value2.Z;
		}

		public static void Max (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z > value2.Z) ? value1.Z : value2.Z;
		}
		
		public static void Clamp (ref Vector3 value1, ref Vector3 min, ref Vector3 max, out Vector3 result)
		{
			Double x = value1.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;
			Double y = value1.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;
			Double z = value1.Z;
			z = (z > max.Z) ? max.Z : z;
			z = (z < min.Z) ? min.Z : z;
			result.X = x;
			result.Y = y;
			result.Z = z;
		}

		public static void Lerp (ref Vector3 value1, ref Vector3 value2, Double amount, out Vector3 result)
		{
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
		}
		
		#endregion

	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Vector4 
		: IEquatable<Vector4>
	{
		public Double X;
		public Double Y;
		public Double Z;
		public Double W;

		public Vector3 XYZ
		{
			get
			{
				return new Vector3(X, Y, Z);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
				this.Z = value.Z;
			}
		}



		public Vector4 (Double x, Double y, Double z, Double w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		public Vector4 (Vector2 value, Double z, Double w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
			this.W = w;
		}

		public Vector4 (Vector3 value, Double w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}

		public Vector4 (Double value)
		{
			this.X = this.Y = this.Z = this.W = value;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString (), this.W.ToString () });
		}

		public Boolean Equals (Vector4 other)
		{
			return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Vector4) {
				flag = this.Equals ((Vector4)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ()) + this.W.GetHashCode ());
		}

		public Double Length ()
		{
			Double num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			return RealMaths.Sqrt (num);
		}

		public Double LengthSquared ()
		{
			return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
		}



		public void Normalise ()
		{
			Double one = 1;
			Double num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			Double num = one / RealMaths.Sqrt (num2);
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
			this.W *= num;
		}



		public Boolean IsUnit()
		{
			Double one = 1;

			return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
		}

		#region Constants

		static Vector4 _zero;
		static Vector4 _one;
		static Vector4 _unitX;
		static Vector4 _unitY;
		static Vector4 _unitZ;
		static Vector4 _unitW;

		static Vector4 ()
		{
			Double temp_one; RealMaths.One(out temp_one);
			Double temp_zero; RealMaths.Zero(out temp_zero);

			_zero = new Vector4 ();
			_one = new Vector4 (temp_one, temp_one, temp_one, temp_one);
			_unitX = new Vector4 (temp_one, temp_zero, temp_zero, temp_zero);
			_unitY = new Vector4 (temp_zero, temp_one, temp_zero, temp_zero);
			_unitZ = new Vector4 (temp_zero, temp_zero, temp_one, temp_zero);
			_unitW = new Vector4 (temp_zero, temp_zero, temp_zero, temp_one);
		}

		public static Vector4 Zero {
			get {
				return _zero;
			}
		}
		
		public static Vector4 One {
			get {
				return _one;
			}
		}
		
		public static Vector4 UnitX {
			get {
				return _unitX;
			}
		}
		
		public static Vector4 UnitY {
			get {
				return _unitY;
			}
		}
		
		public static Vector4 UnitZ {
			get {
				return _unitZ;
			}
		}
		
		public static Vector4 UnitW {
			get {
				return _unitW;
			}
		}
		
		#endregion
		#region Maths

		public static void Distance (ref Vector4 value1, ref Vector4 value2, out Double result)
		{
			Double num4 = value1.X - value2.X;
			Double num3 = value1.Y - value2.Y;
			Double num2 = value1.Z - value2.Z;
			Double num = value1.W - value2.W;
			Double num5 = (((num4 * num4) + (num3 * num3)) + (num2 * num2)) + (num * num);
			result = RealMaths.Sqrt (num5);
		}

		public static void DistanceSquared (ref Vector4 value1, ref Vector4 value2, out Double result)
		{
			Double num4 = value1.X - value2.X;
			Double num3 = value1.Y - value2.Y;
			Double num2 = value1.Z - value2.Z;
			Double num = value1.W - value2.W;
			result = (((num4 * num4) + (num3 * num3)) + (num2 * num2)) + (num * num);
		}

		public static void Dot (ref Vector4 vector1, ref Vector4 vector2, out Double result)
		{
			result = (((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z)) + (vector1.W * vector2.W);
		}

		public static void Normalise (ref Vector4 vector, out Vector4 result)
		{
			Double one = 1;
			Double num2 = (((vector.X * vector.X) + (vector.Y * vector.Y)) + (vector.Z * vector.Z)) + (vector.W * vector.W);
			Double num = one / (RealMaths.Sqrt (num2));
			result.X = vector.X * num;
			result.Y = vector.Y * num;
			result.Z = vector.Z * num;
			result.W = vector.W * num;
		}

		public static void Transform (ref Vector2 position, ref Matrix44 matrix, out Vector4 result)
		{
			Double num4 = ((position.X * matrix.M11) + (position.Y * matrix.M21)) + matrix.M41;
			Double num3 = ((position.X * matrix.M12) + (position.Y * matrix.M22)) + matrix.M42;
			Double num2 = ((position.X * matrix.M13) + (position.Y * matrix.M23)) + matrix.M43;
			Double num = ((position.X * matrix.M14) + (position.Y * matrix.M24)) + matrix.M44;
			result.X = num4;
			result.Y = num3;
			result.Z = num2;
			result.W = num;
		}
		
		public static void Transform (ref Vector3 position, ref Matrix44 matrix, out Vector4 result)
		{
			Double num4 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
			Double num3 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
			Double num2 = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
			Double num = (((position.X * matrix.M14) + (position.Y * matrix.M24)) + (position.Z * matrix.M34)) + matrix.M44;
			result.X = num4;
			result.Y = num3;
			result.Z = num2;
			result.W = num;
		}
		
		public static void Transform (ref Vector4 vector, ref Matrix44 matrix, out Vector4 result)
		{
			Double num4 = (((vector.X * matrix.M11) + (vector.Y * matrix.M21)) + (vector.Z * matrix.M31)) + (vector.W * matrix.M41);
			Double num3 = (((vector.X * matrix.M12) + (vector.Y * matrix.M22)) + (vector.Z * matrix.M32)) + (vector.W * matrix.M42);
			Double num2 = (((vector.X * matrix.M13) + (vector.Y * matrix.M23)) + (vector.Z * matrix.M33)) + (vector.W * matrix.M43);
			Double num = (((vector.X * matrix.M14) + (vector.Y * matrix.M24)) + (vector.Z * matrix.M34)) + (vector.W * matrix.M44);
			result.X = num4;
			result.Y = num3;
			result.Z = num2;
			result.W = num;
		}
		
		
		public static void Transform (ref Vector2 value, ref Quaternion rotation, out Vector4 result)
		{
			Double one = 1;
			Double num6 = rotation.X + rotation.X;
			Double num2 = rotation.Y + rotation.Y;
			Double num = rotation.Z + rotation.Z;
			Double num15 = rotation.W * num6;
			Double num14 = rotation.W * num2;
			Double num5 = rotation.W * num;
			Double num13 = rotation.X * num6;
			Double num4 = rotation.X * num2;
			Double num12 = rotation.X * num;
			Double num11 = rotation.Y * num2;
			Double num10 = rotation.Y * num;
			Double num3 = rotation.Z * num;
			Double num9 = (value.X * ((one - num11) - num3)) + (value.Y * (num4 - num5));
			Double num8 = (value.X * (num4 + num5)) + (value.Y * ((one - num13) - num3));
			Double num7 = (value.X * (num12 - num14)) + (value.Y * (num10 + num15));
			result.X = num9;
			result.Y = num8;
			result.Z = num7;
			result.W = one;
		}
		
		public static void Transform (ref Vector3 value, ref Quaternion rotation, out Vector4 result)
		{
			Double one = 1;
			Double num12 = rotation.X + rotation.X;
			Double num2 = rotation.Y + rotation.Y;
			Double num = rotation.Z + rotation.Z;
			Double num11 = rotation.W * num12;
			Double num10 = rotation.W * num2;
			Double num9 = rotation.W * num;
			Double num8 = rotation.X * num12;
			Double num7 = rotation.X * num2;
			Double num6 = rotation.X * num;
			Double num5 = rotation.Y * num2;
			Double num4 = rotation.Y * num;
			Double num3 = rotation.Z * num;
			Double num15 = ((value.X * ((one - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
			Double num14 = ((value.X * (num7 + num9)) + (value.Y * ((one - num8) - num3))) + (value.Z * (num4 - num11));
			Double num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((one - num8) - num5));
			result.X = num15;
			result.Y = num14;
			result.Z = num13;
			result.W = one;
		}
		
		public static void Transform (ref Vector4 value, ref Quaternion rotation, out Vector4 result)
		{
			Double one = 1;
			Double num12 = rotation.X + rotation.X;
			Double num2 = rotation.Y + rotation.Y;
			Double num = rotation.Z + rotation.Z;
			Double num11 = rotation.W * num12;
			Double num10 = rotation.W * num2;
			Double num9 = rotation.W * num;
			Double num8 = rotation.X * num12;
			Double num7 = rotation.X * num2;
			Double num6 = rotation.X * num;
			Double num5 = rotation.Y * num2;
			Double num4 = rotation.Y * num;
			Double num3 = rotation.Z * num;
			Double num15 = ((value.X * ((one - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
			Double num14 = ((value.X * (num7 + num9)) + (value.Y * ((one - num8) - num3))) + (value.Z * (num4 - num11));
			Double num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((one - num8) - num5));
			result.X = num15;
			result.Y = num14;
			result.Z = num13;
			result.W = value.W;
		}
		
		#endregion
		#region Operators

		public static Vector4 operator - (Vector4 value)
		{
			Vector4 vector;
			vector.X = -value.X;
			vector.Y = -value.Y;
			vector.Z = -value.Z;
			vector.W = -value.W;
			return vector;
		}
		
		public static Boolean operator == (Vector4 value1, Vector4 value2)
		{
			return ((((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) && (value1.W == value2.W));
		}
		
		public static Boolean operator != (Vector4 value1, Vector4 value2)
		{
			if (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) {
				return !(value1.W == value2.W);
			}
			return true;
		}
		
		public static Vector4 operator + (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			vector.Z = value1.Z + value2.Z;
			vector.W = value1.W + value2.W;
			return vector;
		}
		
		public static Vector4 operator - (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			vector.Z = value1.Z - value2.Z;
			vector.W = value1.W - value2.W;
			return vector;
		}
		
		public static Vector4 operator * (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X * value2.X;
			vector.Y = value1.Y * value2.Y;
			vector.Z = value1.Z * value2.Z;
			vector.W = value1.W * value2.W;
			return vector;
		}
		
		public static Vector4 operator * (Vector4 value1, Double scaleFactor)
		{
			Vector4 vector;
			vector.X = value1.X * scaleFactor;
			vector.Y = value1.Y * scaleFactor;
			vector.Z = value1.Z * scaleFactor;
			vector.W = value1.W * scaleFactor;
			return vector;
		}
		
		public static Vector4 operator * (Double scaleFactor, Vector4 value1)
		{
			Vector4 vector;
			vector.X = value1.X * scaleFactor;
			vector.Y = value1.Y * scaleFactor;
			vector.Z = value1.Z * scaleFactor;
			vector.W = value1.W * scaleFactor;
			return vector;
		}
		
		public static Vector4 operator / (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			vector.Z = value1.Z / value2.Z;
			vector.W = value1.W / value2.W;
			return vector;
		}
		
		public static Vector4 operator / (Vector4 value1, Double divider)
		{
			Double one = 1;
			Vector4 vector;
			Double num = one / divider;
			vector.X = value1.X * num;
			vector.Y = value1.Y * num;
			vector.Z = value1.Z * num;
			vector.W = value1.W * num;
			return vector;
		}
		
		public static void Negate (ref Vector4 value, out Vector4 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = -value.W;
		}

		public static void Add (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
		}
		
		public static void Subtract (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
		}
		
		public static void Multiply (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
			result.W = value1.W * value2.W;
		}

		public static void Multiply (ref Vector4 value1, Double scaleFactor, out Vector4 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
			result.W = value1.W * scaleFactor;
		}

		public static void Divide (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
			result.W = value1.W / value2.W;
		}
		
		public static void Divide (ref Vector4 value1, Double divider, out Vector4 result)
		{
			Double one = 1;
			Double num = one / divider;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
			result.W = value1.W * num;
		}
		
		#endregion
		#region Splines

		public static void Barycentric (ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, Double amount1, Double amount2, out Vector4 result)
		{
			result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
			result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
			result.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
			result.W = (value1.W + (amount1 * (value2.W - value1.W))) + (amount2 * (value3.W - value1.W));
		}

		public static void SmoothStep (ref Vector4 value1, ref Vector4 value2, Double amount, out Vector4 result)
		{
			Double zero = 0;
			Double one = 1;
			Double two = 2;
			Double three = 3;

			amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
			amount = (amount * amount) * (three - (two * amount));
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
			result.W = value1.W + ((value2.W - value1.W) * amount);
		}

		public static void CatmullRom (ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, ref Vector4 value4, Double amount, out Vector4 result)
		{
			Double half; RealMaths.Half(out half);
			Double two = 2;
			Double three = 3;
			Double four = 4;
			Double five = 5;

			Double num = amount * amount;
			Double num2 = amount * num;
			result.X = half * ((((two * value2.X) + ((-value1.X + value3.X) * amount)) + (((((two * value1.X) - (five * value2.X)) + (four * value3.X)) - value4.X) * num)) + ((((-value1.X + (three * value2.X)) - (three * value3.X)) + value4.X) * num2));
			result.Y = half * ((((two * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((two * value1.Y) - (five * value2.Y)) + (four * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (three * value2.Y)) - (three * value3.Y)) + value4.Y) * num2));
			result.Z = half * ((((two * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((two * value1.Z) - (five * value2.Z)) + (four * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (three * value2.Z)) - (three * value3.Z)) + value4.Z) * num2));
			result.W = half * ((((two * value2.W) + ((-value1.W + value3.W) * amount)) + (((((two * value1.W) - (five * value2.W)) + (four * value3.W)) - value4.W) * num)) + ((((-value1.W + (three * value2.W)) - (three * value3.W)) + value4.W) * num2));
		}

		public static void Hermite (ref Vector4 value1, ref Vector4 tangent1, ref Vector4 value2, ref Vector4 tangent2, Double amount, out Vector4 result)
		{
			Double one = 1;
			Double two = 2;
			Double three = 3;

			Double num = amount * amount;
			Double num6 = amount * num;
			Double num5 = ((two * num6) - (three * num)) + one;
			Double num4 = (-two * num6) + (three * num);
			Double num3 = (num6 - (two * num)) + amount;
			Double num2 = num6 - num;
			result.X = (((value1.X * num5) + (value2.X * num4)) + (tangent1.X * num3)) + (tangent2.X * num2);
			result.Y = (((value1.Y * num5) + (value2.Y * num4)) + (tangent1.Y * num3)) + (tangent2.Y * num2);
			result.Z = (((value1.Z * num5) + (value2.Z * num4)) + (tangent1.Z * num3)) + (tangent2.Z * num2);
			result.W = (((value1.W * num5) + (value2.W * num4)) + (tangent1.W * num3)) + (tangent2.W * num2);
		}
		
		#endregion

		#region Utilities

		public static void Min (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z < value2.Z) ? value1.Z : value2.Z;
			result.W = (value1.W < value2.W) ? value1.W : value2.W;
		}

		public static void Max (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z > value2.Z) ? value1.Z : value2.Z;
			result.W = (value1.W > value2.W) ? value1.W : value2.W;
		}
		
		public static void Clamp (ref Vector4 value1, ref Vector4 min, ref Vector4 max, out Vector4 result)
		{
			Double x = value1.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;
			Double y = value1.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;
			Double z = value1.Z;
			z = (z > max.Z) ? max.Z : z;
			z = (z < min.Z) ? min.Z : z;
			Double w = value1.W;
			w = (w > max.W) ? max.W : w;
			w = (w < min.W) ? min.W : w;
			result.X = x;
			result.Y = y;
			result.Z = z;
			result.W = w;
		}
		
		public static void Lerp (ref Vector4 value1, ref Vector4 value2, Double amount, out Vector4 result)
		{
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
			result.W = value1.W + ((value2.W - value1.W) * amount);
		}
		
		#endregion


	}

}

namespace Sungiant.Abacus.Fixed32Precision
{
	public static class GaussianElimination
	{

	}
	public class GjkDistance
	{
		public GjkDistance ()
		{
			for (Int32 i = 0; i < 0x10; i++)
			{
				this.det [i] = new Fixed32[4];
			}
		}

		public Boolean AddSupportPoint (ref Vector3 newPoint)
		{
			Int32 index = (BitsToIndices [this.simplexBits ^ 15] & 7) - 1;

			this.y [index] = newPoint;
			this.yLengthSq [index] = newPoint.LengthSquared ();

			for (Int32 i = BitsToIndices[this.simplexBits]; i != 0; i = i >> 3)
			{
				Int32 num2 = (i & 7) - 1;
				Vector3 vector = this.y [num2] - newPoint;

				this.edges [num2] [index] = vector;
				this.edges [index] [num2] = -vector;
				this.edgeLengthSq [index] [num2] = this.edgeLengthSq [num2] [index] = vector.LengthSquared ();
			}

			this.UpdateDeterminant (index);

			return this.UpdateSimplex (index);
		}

		public void Reset ()
		{
			Fixed32 zero = 0;

			this.simplexBits = 0;
			this.maxLengthSq = zero;
		}

		public Vector3 ClosestPoint
		{
			get { return this.closestPoint; }
		}
		
		public Boolean FullSimplex
		{
			get { return (this.simplexBits == 15); }
		}
		
		public Fixed32 MaxLengthSquared
		{
			get { return this.maxLengthSq; }
		}

		Vector3 closestPoint;
		Fixed32[][] det = new Fixed32[0x10][];
		Fixed32[][] edgeLengthSq = new Fixed32[][] { new Fixed32[4], new Fixed32[4], new Fixed32[4], new Fixed32[4] };
		Vector3[][] edges = new Vector3[][] { new Vector3[4], new Vector3[4], new Vector3[4], new Vector3[4] };
		Fixed32 maxLengthSq;
		Int32 simplexBits;
		Vector3[] y = new Vector3[4];
		Fixed32[] yLengthSq = new Fixed32[4];

		static Int32[] BitsToIndices = new Int32[] { 0, 1, 2, 0x11, 3, 0x19, 0x1a, 0xd1, 4, 0x21, 0x22, 0x111, 0x23, 0x119, 0x11a, 0x8d1 };

		Vector3 ComputeClosestPoint ()
		{
			Fixed32 fzero; RealMaths.Zero(out fzero);

			Fixed32 num3 = fzero;
			Vector3 zero = Vector3.Zero;

			this.maxLengthSq = fzero;

			for (Int32 i = BitsToIndices[this.simplexBits]; i != 0; i = i >> 3)
			{
				Int32 index = (i & 7) - 1;
				Fixed32 num4 = this.det [this.simplexBits] [index];

				num3 += num4;
				zero += (Vector3)(this.y [index] * num4);

				this.maxLengthSq = RealMaths.Max (this.maxLengthSq, this.yLengthSq [index]);
			}

			return (Vector3)(zero / num3);
		}

		Boolean IsSatisfiesRule (Int32 xBits, Int32 yBits)
		{
			Fixed32 fzero; RealMaths.Zero(out fzero);

			for (Int32 i = BitsToIndices[yBits]; i != 0; i = i >> 3)
			{
				Int32 index = (i & 7) - 1;
				Int32 num3 = ((Int32)1) << index;

				if ((num3 & xBits) != 0)
				{
					if (this.det [xBits] [index] <= fzero)
					{
						return false;
					}
				}
				else if (this.det [xBits | num3] [index] > fzero)
				{
					return false;
				}
			}

			return true;
		}

		void UpdateDeterminant (Int32 xmIdx)
		{
			Fixed32 fone; RealMaths.One(out fone);
			Int32 index = ((Int32)1) << xmIdx;

			this.det [index] [xmIdx] = fone;

			Int32 num14 = BitsToIndices [this.simplexBits];
			Int32 num8 = num14;

			for (Int32 i = 0; num8 != 0; i++)
			{
				Int32 num = (num8 & 7) - 1;
				Int32 num12 = ((int)1) << num;
				Int32 num6 = num12 | index;

				this.det [num6] [num] = Dot (ref this.edges [xmIdx] [num], ref this.y [xmIdx]);
				this.det [num6] [xmIdx] = Dot (ref this.edges [num] [xmIdx], ref this.y [num]);

				Int32 num11 = num14;

				for (Int32 j = 0; j < i; j++)
				{
					int num3 = (num11 & 7) - 1;
					int num5 = ((int)1) << num3;
					int num9 = num6 | num5;
					int num4 = (this.edgeLengthSq [num] [num3] < this.edgeLengthSq [xmIdx] [num3]) ? num : xmIdx;

					this.det [num9] [num3] = 
						(this.det [num6] [num] * Dot (ref this.edges [num4] [num3], ref this.y [num])) + 
						(this.det [num6] [xmIdx] * Dot (ref this.edges [num4] [num3], ref this.y [xmIdx]));

					num4 = (this.edgeLengthSq [num3] [num] < this.edgeLengthSq [xmIdx] [num]) ? num3 : xmIdx;

					this.det [num9] [num] = 
						(this.det [num5 | index] [num3] * Dot (ref this.edges [num4] [num], ref this.y [num3])) + 
						(this.det [num5 | index] [xmIdx] * Dot (ref this.edges [num4] [num], ref this.y [xmIdx]));

					num4 = (this.edgeLengthSq [num] [xmIdx] < this.edgeLengthSq [num3] [xmIdx]) ? num : num3;

					this.det [num9] [xmIdx] = 
						(this.det [num12 | num5] [num3] * Dot (ref this.edges [num4] [xmIdx], ref this.y [num3])) + 
						(this.det [num12 | num5] [num] * Dot (ref this.edges [num4] [xmIdx], ref this.y [num]));

					num11 = num11 >> 3;
				}

				num8 = num8 >> 3;
			}

			if ((this.simplexBits | index) == 15)
			{
				int num2 = 
					(this.edgeLengthSq [1] [0] < this.edgeLengthSq [2] [0]) ? 
					((this.edgeLengthSq [1] [0] < this.edgeLengthSq [3] [0]) ? 1 : 3) : 
					((this.edgeLengthSq [2] [0] < this.edgeLengthSq [3] [0]) ? 2 : 3);

				this.det [15] [0] = 
					((this.det [14] [1] * Dot (ref this.edges [num2] [0], ref this.y [1])) + 
					(this.det [14] [2] * Dot (ref this.edges [num2] [0], ref this.y [2]))) + 
					(this.det [14] [3] * Dot (ref this.edges [num2] [0], ref this.y [3]));

				num2 = 
					(this.edgeLengthSq [0] [1] < this.edgeLengthSq [2] [1]) ? 
					((this.edgeLengthSq [0] [1] < this.edgeLengthSq [3] [1]) ? 0 : 3) : 
					((this.edgeLengthSq [2] [1] < this.edgeLengthSq [3] [1]) ? 2 : 3);

				this.det [15] [1] = 
					((this.det [13] [0] * Dot (ref this.edges [num2] [1], ref this.y [0])) + 
				    (this.det [13] [2] * Dot (ref this.edges [num2] [1], ref this.y [2]))) + 
					(this.det [13] [3] * Dot (ref this.edges [num2] [1], ref this.y [3]));

				num2 = 
					(this.edgeLengthSq [0] [2] < this.edgeLengthSq [1] [2]) ? 
					((this.edgeLengthSq [0] [2] < this.edgeLengthSq [3] [2]) ? 0 : 3) : 
					((this.edgeLengthSq [1] [2] < this.edgeLengthSq [3] [2]) ? 1 : 3);

				this.det [15] [2] = 
					((this.det [11] [0] * Dot (ref this.edges [num2] [2], ref this.y [0])) + 
					(this.det [11] [1] * Dot (ref this.edges [num2] [2], ref this.y [1]))) + 
					(this.det [11] [3] * Dot (ref this.edges [num2] [2], ref this.y [3]));

				num2 = 
					(this.edgeLengthSq [0] [3] < this.edgeLengthSq [1] [3]) ? 
					((this.edgeLengthSq [0] [3] < this.edgeLengthSq [2] [3]) ? 0 : 2) : 
					((this.edgeLengthSq [1] [3] < this.edgeLengthSq [2] [3]) ? 1 : 2);

				this.det [15] [3] = 
					((this.det [7] [0] * Dot (ref this.edges [num2] [3], ref this.y [0])) + 
					(this.det [7] [1] * Dot (ref this.edges [num2] [3], ref this.y [1]))) + 
					(this.det [7] [2] * Dot (ref this.edges [num2] [3], ref this.y [2]));
			}
		}

		Boolean UpdateSimplex (Int32 newIndex)
		{
			Int32 yBits = this.simplexBits | (((Int32)1) << newIndex);

			Int32 xBits = ((Int32)1) << newIndex;

			for (Int32 i = this.simplexBits; i != 0; i--)
			{
				if (((i & yBits) == i) && this.IsSatisfiesRule (i | xBits, yBits))
				{
					this.simplexBits = i | xBits;
					this.closestPoint = this.ComputeClosestPoint ();

					return true;
				}
			}

			Boolean flag = false;

			if (this.IsSatisfiesRule (xBits, yBits))
			{
				this.simplexBits = xBits;
				this.closestPoint = this.y [newIndex];
				this.maxLengthSq = this.yLengthSq [newIndex];

				flag = true;
			}

			return flag;
		}

		static Fixed32 Dot (ref Vector3 a, ref Vector3 b)
		{
			return (((a.X * b.X) + (a.Y * b.Y)) + (a.Z * b.Z));
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct BoundingBox 
		: IEquatable<BoundingBox>
	{
		public const int CornerCount = 8;
		public Vector3 Min;
		public Vector3 Max;

		public Vector3[] GetCorners ()
		{
			return new Vector3[] { new Vector3 (this.Min.X, this.Max.Y, this.Max.Z), new Vector3 (this.Max.X, this.Max.Y, this.Max.Z), new Vector3 (this.Max.X, this.Min.Y, this.Max.Z), new Vector3 (this.Min.X, this.Min.Y, this.Max.Z), new Vector3 (this.Min.X, this.Max.Y, this.Min.Z), new Vector3 (this.Max.X, this.Max.Y, this.Min.Z), new Vector3 (this.Max.X, this.Min.Y, this.Min.Z), new Vector3 (this.Min.X, this.Min.Y, this.Min.Z) };
		}

		public BoundingBox (Vector3 min, Vector3 max)
		{
			this.Min = min;
			this.Max = max;
		}

		public Boolean Equals (BoundingBox other)
		{
			return ((this.Min == other.Min) && (this.Max == other.Max));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is BoundingBox) {
				flag = this.Equals ((BoundingBox)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Min.GetHashCode () + this.Max.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Min:{0} Max:{1}}}", new Object[] { this.Min.ToString (), this.Max.ToString () });
		}

		public static void CreateMerged (ref BoundingBox original, ref BoundingBox additional, out BoundingBox result)
		{
			Vector3 vector;
			Vector3 vector2;
			Vector3.Min (ref original.Min, ref additional.Min, out vector2);
			Vector3.Max (ref original.Max, ref additional.Max, out vector);
			result.Min = vector2;
			result.Max = vector;
		}

		public static void CreateFromSphere (ref BoundingSphere sphere, out BoundingBox result)
		{
			result.Min.X = sphere.Center.X - sphere.Radius;
			result.Min.Y = sphere.Center.Y - sphere.Radius;
			result.Min.Z = sphere.Center.Z - sphere.Radius;
			result.Max.X = sphere.Center.X + sphere.Radius;
			result.Max.Y = sphere.Center.Y + sphere.Radius;
			result.Max.Z = sphere.Center.Z + sphere.Radius;
		}

		public static BoundingBox CreateFromPoints (IEnumerable<Vector3> points)
		{
			if (points == null) {
				throw new ArgumentNullException ();
			}
			Boolean flag = false;
			Vector3 vector3 = new Vector3 (Fixed32.MaxValue);
			Vector3 vector2 = new Vector3 (Fixed32.MinValue);
			foreach (Vector3 vector in points) {
				Vector3 vector4 = vector;
				Vector3.Min (ref vector3, ref vector4, out vector3);
				Vector3.Max (ref vector2, ref vector4, out vector2);
				flag = true;
			}
			if (!flag) {
				throw new ArgumentException ("BoundingBoxZeroPoints");
			}
			return new BoundingBox (vector3, vector2);
		}

		public Boolean Intersects (ref BoundingBox box)
		{
			if ((this.Max.X < box.Min.X) || (this.Min.X > box.Max.X)) {
				return false;
			}
			if ((this.Max.Y < box.Min.Y) || (this.Min.Y > box.Max.Y)) {
				return false;
			}
			return ((this.Max.Z >= box.Min.Z) && (this.Min.Z <= box.Max.Z));
		}

		public Boolean Intersects (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			return frustum.Intersects (ref this);
		}

		public PlaneIntersectionType Intersects (ref Plane plane)
		{
			Fixed32 zero = 0;

			Vector3 vector;
			Vector3 vector2;
			vector2.X = (plane.Normal.X >= zero) ? this.Min.X : this.Max.X;
			vector2.Y = (plane.Normal.Y >= zero) ? this.Min.Y : this.Max.Y;
			vector2.Z = (plane.Normal.Z >= zero) ? this.Min.Z : this.Max.Z;
			vector.X = (plane.Normal.X >= zero) ? this.Max.X : this.Min.X;
			vector.Y = (plane.Normal.Y >= zero) ? this.Max.Y : this.Min.Y;
			vector.Z = (plane.Normal.Z >= zero) ? this.Max.Z : this.Min.Z;
			Fixed32 num = ((plane.Normal.X * vector2.X) + (plane.Normal.Y * vector2.Y)) + (plane.Normal.Z * vector2.Z);
			if ((num + plane.D) > zero) {
				return PlaneIntersectionType.Front;
			}
			num = ((plane.Normal.X * vector.X) + (plane.Normal.Y * vector.Y)) + (plane.Normal.Z * vector.Z);
			if ((num + plane.D) < zero) {
				return PlaneIntersectionType.Back;
			}
			return PlaneIntersectionType.Intersecting;
		}

		public Fixed32? Intersects (ref Ray ray)
		{
			Fixed32 epsilon; RealMaths.Epsilon(out epsilon);

			Fixed32 zero = 0;
			Fixed32 one = 1;

			Fixed32 num = zero;
			Fixed32 maxValue = Fixed32.MaxValue;
			if (RealMaths.Abs (ray.Direction.X) < epsilon) {
				if ((ray.Position.X < this.Min.X) || (ray.Position.X > this.Max.X)) {
					return null;
				}
			} else {
				Fixed32 num11 = one / ray.Direction.X;
				Fixed32 num8 = (this.Min.X - ray.Position.X) * num11;
				Fixed32 num7 = (this.Max.X - ray.Position.X) * num11;
				if (num8 > num7) {
					Fixed32 num14 = num8;
					num8 = num7;
					num7 = num14;
				}
				num = RealMaths.Max (num8, num);
				maxValue = RealMaths.Min (num7, maxValue);
				if (num > maxValue) {
					return null;
				}
			}
			if (RealMaths.Abs (ray.Direction.Y) < epsilon) {
				if ((ray.Position.Y < this.Min.Y) || (ray.Position.Y > this.Max.Y)) {
					return null;
				}
			} else {
				Fixed32 num10 = one / ray.Direction.Y;
				Fixed32 num6 = (this.Min.Y - ray.Position.Y) * num10;
				Fixed32 num5 = (this.Max.Y - ray.Position.Y) * num10;
				if (num6 > num5) {
					Fixed32 num13 = num6;
					num6 = num5;
					num5 = num13;
				}
				num = RealMaths.Max (num6, num);
				maxValue = RealMaths.Min (num5, maxValue);
				if (num > maxValue) {
					return null;
				}
			}
			

			if (RealMaths.Abs (ray.Direction.Z) < epsilon) {
				if ((ray.Position.Z < this.Min.Z) || (ray.Position.Z > this.Max.Z)) {
					return null;
				}
			} else {
				Fixed32 num9 = one / ray.Direction.Z;
				Fixed32 num4 = (this.Min.Z - ray.Position.Z) * num9;
				Fixed32 num3 = (this.Max.Z - ray.Position.Z) * num9;
				if (num4 > num3) {
					Fixed32 num12 = num4;
					num4 = num3;
					num3 = num12;
				}
				num = RealMaths.Max (num4, num);
				maxValue = RealMaths.Min (num3, maxValue);
				if (num > maxValue) {
					return null;
				}
			}
			return new Fixed32? (num);
		}

		public Boolean Intersects (ref BoundingSphere sphere)
		{
			Fixed32 num;
			Vector3 vector;
			Vector3.Clamp (ref sphere.Center, ref this.Min, ref this.Max, out vector);
			Vector3.DistanceSquared (ref sphere.Center, ref vector, out num);
			return (num <= (sphere.Radius * sphere.Radius));
		}

		public ContainmentType Contains (ref BoundingBox box)
		{
			if ((this.Max.X < box.Min.X) || (this.Min.X > box.Max.X)) {
				return ContainmentType.Disjoint;
			}
			if ((this.Max.Y < box.Min.Y) || (this.Min.Y > box.Max.Y)) {
				return ContainmentType.Disjoint;
			}
			if ((this.Max.Z < box.Min.Z) || (this.Min.Z > box.Max.Z)) {
				return ContainmentType.Disjoint;
			}
			if ((((this.Min.X <= box.Min.X) && (box.Max.X <= this.Max.X)) && ((this.Min.Y <= box.Min.Y) && (box.Max.Y <= this.Max.Y))) && ((this.Min.Z <= box.Min.Z) && (box.Max.Z <= this.Max.Z))) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		public ContainmentType Contains (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			if (!frustum.Intersects (ref this)) {
				return ContainmentType.Disjoint;
			}

			for (Int32 i = 0; i < frustum.cornerArray.Length; ++i) {
				Vector3 vector = frustum.cornerArray[i];
				if (this.Contains (ref vector) == ContainmentType.Disjoint) {
					return ContainmentType.Intersects;
				}
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref Vector3 point)
		{
			if ((((this.Min.X <= point.X) && (point.X <= this.Max.X)) && ((this.Min.Y <= point.Y) && (point.Y <= this.Max.Y))) && ((this.Min.Z <= point.Z) && (point.Z <= this.Max.Z))) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Disjoint;
		}

		public ContainmentType Contains (ref BoundingSphere sphere)
		{
			Fixed32 num2;
			Vector3 vector;
			Vector3.Clamp (ref sphere.Center, ref this.Min, ref this.Max, out vector);
			Vector3.DistanceSquared (ref sphere.Center, ref vector, out num2);
			Fixed32 radius = sphere.Radius;
			if (num2 > (radius * radius)) {
				return ContainmentType.Disjoint;
			}
			if (((((this.Min.X + radius) <= sphere.Center.X) && (sphere.Center.X <= (this.Max.X - radius))) && (((this.Max.X - this.Min.X) > radius) && ((this.Min.Y + radius) <= sphere.Center.Y))) && (((sphere.Center.Y <= (this.Max.Y - radius)) && ((this.Max.Y - this.Min.Y) > radius)) && ((((this.Min.Z + radius) <= sphere.Center.Z) && (sphere.Center.Z <= (this.Max.Z - radius))) && ((this.Max.X - this.Min.X) > radius)))) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		internal void SupportMapping (ref Vector3 v, out Vector3 result)
		{
			Fixed32 zero = 0;

			result.X = (v.X >= zero) ? this.Max.X : this.Min.X;
			result.Y = (v.Y >= zero) ? this.Max.Y : this.Min.Y;
			result.Z = (v.Z >= zero) ? this.Max.Z : this.Min.Z;
		}

		public static Boolean operator == (BoundingBox a, BoundingBox b)
		{
			return a.Equals (b);
		}

		public static Boolean operator != (BoundingBox a, BoundingBox b)
		{
			if (!(a.Min != b.Min)) {
				return (a.Max != b.Max);
			}
			return true;
		}
	}
	public class BoundingFrustum 
		: IEquatable<BoundingFrustum>
	{
		const int BottomPlaneIndex = 5;

		internal Vector3[] cornerArray;

		public const int CornerCount = 8;

		const int FarPlaneIndex = 1;

		GjkDistance gjk;

		const int LeftPlaneIndex = 2;

		Matrix44 matrix;

		const int NearPlaneIndex = 0;

		const int NumPlanes = 6;

		Plane[] planes;

		const int RightPlaneIndex = 3;

		const int TopPlaneIndex = 4;

		BoundingFrustum ()
		{
			this.planes = new Plane[6];
			this.cornerArray = new Vector3[8];
		}

		public BoundingFrustum (Matrix44 value)
		{
			this.planes = new Plane[6];
			this.cornerArray = new Vector3[8];
			this.SetMatrix (ref value);
		}

		static Vector3 ComputeIntersection (ref Plane plane, ref Ray ray)
		{
			Fixed32 planeNormDotRayPos;
			Fixed32 planeNormDotRayDir;

			Vector3.Dot (ref plane.Normal, ref ray.Position, out planeNormDotRayPos);
			Vector3.Dot (ref plane.Normal, ref ray.Direction, out planeNormDotRayDir);

			Fixed32 num = (-plane.D - planeNormDotRayPos) / planeNormDotRayDir;

			return (ray.Position + (ray.Direction * num));
		}

		static Ray ComputeIntersectionLine (ref Plane p1, ref Plane p2)
		{
			Ray ray = new Ray ();

			Vector3.Cross (ref p1.Normal, ref p2.Normal, out ray.Direction);

			Fixed32 num = ray.Direction.LengthSquared ();

			Vector3 a = (-p1.D * p2.Normal) + (p2.D * p1.Normal);

			Vector3 cross;

			Vector3.Cross (ref a, ref ray.Direction, out cross);

			ray.Position =  cross / num;

			return ray;
		}

		public ContainmentType Contains (ref BoundingBox box)
		{
			Boolean flag = false;
			for(Int32 i = 0; i < this.planes.Length; ++i)
			{
				Plane plane = this.planes[i];
				switch (box.Intersects (ref plane)) {
				case PlaneIntersectionType.Front:
					return ContainmentType.Disjoint;

				case PlaneIntersectionType.Intersecting:
					flag = true;
					break;
				}
			}
			if (!flag) {
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		public ContainmentType Contains (ref BoundingFrustum frustum)
		{
			if (frustum == null) {
				throw new ArgumentNullException ("frustum");
			}
			ContainmentType disjoint = ContainmentType.Disjoint;
			if (this.Intersects (ref frustum)) {
				disjoint = ContainmentType.Contains;
				for (int i = 0; i < this.cornerArray.Length; i++) {
					if (this.Contains (ref frustum.cornerArray [i]) == ContainmentType.Disjoint) {
						return ContainmentType.Intersects;
					}
				}
			}
			return disjoint;
		}

		public ContainmentType Contains (ref BoundingSphere sphere)
		{
			Vector3 center = sphere.Center;
			Fixed32 radius = sphere.Radius;
			int num2 = 0;
			foreach (Plane plane in this.planes) {
				Fixed32 num5 = ((plane.Normal.X * center.X) + (plane.Normal.Y * center.Y)) + (plane.Normal.Z * center.Z);
				Fixed32 num3 = num5 + plane.D;
				if (num3 > radius) {
					return ContainmentType.Disjoint;
				}
				if (num3 < -radius) {
					num2++;
				}
			}
			if (num2 != 6) {
				return ContainmentType.Intersects;
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref Vector3 point)
		{
			Fixed32 epsilon; RealMaths.FromString("0.00001", out epsilon);

			foreach (Plane plane in this.planes) {
				Fixed32 num2 = (((plane.Normal.X * point.X) + (plane.Normal.Y * point.Y)) + (plane.Normal.Z * point.Z)) + plane.D;
				if (num2 > epsilon) {
					return ContainmentType.Disjoint;
				}
			}
			return ContainmentType.Contains;
		}

		public Boolean Equals (BoundingFrustum other)
		{
			if (other == null) {
				return false;
			}
			return (this.matrix == other.matrix);
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			BoundingFrustum frustum = obj as BoundingFrustum;
			if (frustum != null) {
				flag = this.matrix == frustum.matrix;
			}
			return flag;
		}

		public Vector3[] GetCorners ()
		{
			return (Vector3[])this.cornerArray.Clone ();
		}

		public override Int32 GetHashCode ()
		{
			return this.matrix.GetHashCode ();
		}

		public Boolean Intersects (ref BoundingBox box)
		{
			Boolean flag;
			this.Intersects (ref box, out flag);
			return flag;
		}

		void Intersects (ref BoundingBox box, out Boolean result)
		{
			Fixed32 epsilon; RealMaths.FromString("0.00001", out epsilon);
			Fixed32 zero = 0;
			Fixed32 four = 4;

			Vector3 closestPoint;
			Vector3 vector2;
			Vector3 vector3;
			Vector3 vector4;
			Vector3 vector5;
			if (this.gjk == null) {
				this.gjk = new GjkDistance ();
			}
			this.gjk.Reset ();
			Vector3.Subtract (ref this.cornerArray [0], ref box.Min, out closestPoint);
			if (closestPoint.LengthSquared () < epsilon) {
				Vector3.Subtract (ref this.cornerArray [0], ref box.Max, out closestPoint);
			}
			Fixed32 maxValue = Fixed32.MaxValue;
			Fixed32 num3 = zero;
			result = false;
		Label_006D:
			vector5.X = -closestPoint.X;
			vector5.Y = -closestPoint.Y;
			vector5.Z = -closestPoint.Z;
			this.SupportMapping (ref vector5, out vector4);
			box.SupportMapping (ref closestPoint, out vector3);
			Vector3.Subtract (ref vector4, ref vector3, out vector2);
			Fixed32 num4 = ((closestPoint.X * vector2.X) + (closestPoint.Y * vector2.Y)) + (closestPoint.Z * vector2.Z);
			if (num4 <= zero) {
				this.gjk.AddSupportPoint (ref vector2);
				closestPoint = this.gjk.ClosestPoint;
				Fixed32 num2 = maxValue;
				maxValue = closestPoint.LengthSquared ();
				if ((num2 - maxValue) > (epsilon * num2)) {
					num3 = four * epsilon * this.gjk.MaxLengthSquared;
					if (!this.gjk.FullSimplex && (maxValue >= num3)) {
						goto Label_006D;
					}
					result = true;
				}
			}
		}

		public Boolean Intersects (ref BoundingFrustum frustum)
		{
			Fixed32 epsilon; RealMaths.FromString("0.00001", out epsilon);
			Fixed32 zero = 0;
			Fixed32 four = 4;

			Vector3 closestPoint;
			if (frustum == null) {
				throw new ArgumentNullException ("frustum");
			}
			if (this.gjk == null) {
				this.gjk = new GjkDistance ();
			}
			this.gjk.Reset ();
			Vector3.Subtract (ref this.cornerArray [0], ref frustum.cornerArray [0], out closestPoint);
			if (closestPoint.LengthSquared () < epsilon) {
				Vector3.Subtract (ref this.cornerArray [0], ref frustum.cornerArray [1], out closestPoint);
			}
			Fixed32 maxValue = Fixed32.MaxValue;
			Fixed32 num3 = zero;
			do {
				Vector3 vector2;
				Vector3 vector3;
				Vector3 vector4;
				Vector3 vector5;
				vector5.X = -closestPoint.X;
				vector5.Y = -closestPoint.Y;
				vector5.Z = -closestPoint.Z;
				this.SupportMapping (ref vector5, out vector4);
				frustum.SupportMapping (ref closestPoint, out vector3);
				Vector3.Subtract (ref vector4, ref vector3, out vector2);
				Fixed32 num4 = ((closestPoint.X * vector2.X) + (closestPoint.Y * vector2.Y)) + (closestPoint.Z * vector2.Z);
				if (num4 > zero) {
					return false;
				}
				this.gjk.AddSupportPoint (ref vector2);
				closestPoint = this.gjk.ClosestPoint;
				Fixed32 num2 = maxValue;
				maxValue = closestPoint.LengthSquared ();
				num3 = four * epsilon * this.gjk.MaxLengthSquared;
				if ((num2 - maxValue) <= (epsilon * num2)) {
					return false;
				}
			} while (!this.gjk.FullSimplex && (maxValue >= num3));
			return true;
		}

		public Boolean Intersects (ref BoundingSphere sphere)
		{
			Boolean flag;
			this.Intersects (ref sphere, out flag);
			return flag;
		}

		void Intersects (ref BoundingSphere sphere, out Boolean result)
		{
			Fixed32 zero = 0;
			Fixed32 epsilon; RealMaths.FromString("0.00001", out epsilon);
			Fixed32 four = 4;

			Vector3 unitX;
			Vector3 vector2;
			Vector3 vector3;
			Vector3 vector4;
			Vector3 vector5;
			if (this.gjk == null) {
				this.gjk = new GjkDistance ();
			}
			this.gjk.Reset ();
			Vector3.Subtract (ref this.cornerArray [0], ref sphere.Center, out unitX);
			if (unitX.LengthSquared () < epsilon) {
				unitX = Vector3.UnitX;
			}
			Fixed32 maxValue = Fixed32.MaxValue;
			Fixed32 num3 = zero;
			result = false;
		Label_005A:
			vector5.X = -unitX.X;
			vector5.Y = -unitX.Y;
			vector5.Z = -unitX.Z;
			this.SupportMapping (ref vector5, out vector4);
			sphere.SupportMapping (ref unitX, out vector3);
			Vector3.Subtract (ref vector4, ref vector3, out vector2);
			Fixed32 num4 = ((unitX.X * vector2.X) + (unitX.Y * vector2.Y)) + (unitX.Z * vector2.Z);
			if (num4 <= zero) {
				this.gjk.AddSupportPoint (ref vector2);
				unitX = this.gjk.ClosestPoint;
				Fixed32 num2 = maxValue;
				maxValue = unitX.LengthSquared ();
				if ((num2 - maxValue) > (epsilon * num2)) {
					num3 = four * epsilon * this.gjk.MaxLengthSquared;
					if (!this.gjk.FullSimplex && (maxValue >= num3)) {
						goto Label_005A;
					}
					result = true;
				}
			}
		}

		public PlaneIntersectionType Intersects (ref Plane plane)
		{
			Fixed32 zero = 0;

			int num = 0;
			for (int i = 0; i < 8; i++) {
				Fixed32 num3;
				Vector3.Dot (ref this.cornerArray [i], ref plane.Normal, out num3);
				if ((num3 + plane.D) > zero) {
					num |= 1;
				} else {
					num |= 2;
				}
				if (num == 3) {
					return PlaneIntersectionType.Intersecting;
				}
			}
			if (num != 1) {
				return PlaneIntersectionType.Back;
			}
			return PlaneIntersectionType.Front;
		}

		public Fixed32? Intersects (ref Ray ray)
		{
			Fixed32? nullable;
			this.Intersects (ref ray, out nullable);
			return nullable;
		}

		void Intersects (ref Ray ray, out Fixed32? result)
		{
			Fixed32 epsilon; RealMaths.FromString("0.00001", out epsilon);
			Fixed32 zero = 0;

			ContainmentType type = this.Contains (ref ray.Position);
			if (type == ContainmentType.Contains) {
				result = zero;
			} else {
				Fixed32 minValue = Fixed32.MinValue;
				Fixed32 maxValue = Fixed32.MaxValue;
				result = zero;
				foreach (Plane plane in this.planes) {
					Fixed32 num3;
					Fixed32 num6;
					Vector3 normal = plane.Normal;
					Vector3.Dot (ref ray.Direction, ref normal, out num6);
					Vector3.Dot (ref ray.Position, ref normal, out num3);
					num3 += plane.D;
					if (RealMaths.Abs (num6) < epsilon) {
						if (num3 > zero) {
							return;
						}
					} else {
						Fixed32 num = -num3 / num6;
						if (num6 < zero) {
							if (num > maxValue) {
								return;
							}
							if (num > minValue) {
								minValue = num;
							}
						} else {
							if (num < minValue) {
								return;
							}
							if (num < maxValue) {
								maxValue = num;
							}
						}
					}
				}
				Fixed32 num7 = (minValue >= zero) ? minValue : maxValue;
				if (num7 >= zero) {
					result = new Fixed32? (num7);
				}
			}
		}

		public static Boolean operator == (BoundingFrustum a, BoundingFrustum b)
		{
			return Object.Equals (a, b);
		}

		public static Boolean operator != (BoundingFrustum a, BoundingFrustum b)
		{
			return !Object.Equals (a, b);
		}

		void SetMatrix (ref Matrix44 value)
		{
			this.matrix = value;

			this.planes [2].Normal.X = -value.M14 - value.M11;
			this.planes [2].Normal.Y = -value.M24 - value.M21;
			this.planes [2].Normal.Z = -value.M34 - value.M31;
			this.planes [2].D = -value.M44 - value.M41;

			this.planes [3].Normal.X = -value.M14 + value.M11;
			this.planes [3].Normal.Y = -value.M24 + value.M21;
			this.planes [3].Normal.Z = -value.M34 + value.M31;
			this.planes [3].D = -value.M44 + value.M41;

			this.planes [4].Normal.X = -value.M14 + value.M12;
			this.planes [4].Normal.Y = -value.M24 + value.M22;
			this.planes [4].Normal.Z = -value.M34 + value.M32;
			this.planes [4].D = -value.M44 + value.M42;

			this.planes [5].Normal.X = -value.M14 - value.M12;
			this.planes [5].Normal.Y = -value.M24 - value.M22;
			this.planes [5].Normal.Z = -value.M34 - value.M32;
			this.planes [5].D = -value.M44 - value.M42;

			this.planes [0].Normal.X = -value.M13;
			this.planes [0].Normal.Y = -value.M23;
			this.planes [0].Normal.Z = -value.M33;
			this.planes [0].D = -value.M43;

			this.planes [1].Normal.X = -value.M14 + value.M13;
			this.planes [1].Normal.Y = -value.M24 + value.M23;
			this.planes [1].Normal.Z = -value.M34 + value.M33;
			this.planes [1].D = -value.M44 + value.M43;

			for (int i = 0; i < 6; i++) {
				Fixed32 num2 = this.planes [i].Normal.Length ();
				this.planes [i].Normal = (Vector3)(this.planes [i].Normal / num2);
				this.planes [i].D /= num2;
			}

			Ray ray = ComputeIntersectionLine (ref this.planes [0], ref this.planes [2]);

			this.cornerArray [0] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [3] = ComputeIntersection (ref this.planes [5], ref ray);

			ray = ComputeIntersectionLine (ref this.planes [3], ref this.planes [0]);

			this.cornerArray [1] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [2] = ComputeIntersection (ref this.planes [5], ref ray);

			ray = ComputeIntersectionLine (ref this.planes [2], ref this.planes [1]);

			this.cornerArray [4] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [7] = ComputeIntersection (ref this.planes [5], ref ray);

			ray = ComputeIntersectionLine (ref this.planes [1], ref this.planes [3]);

			this.cornerArray [5] = ComputeIntersection (ref this.planes [4], ref ray);
			this.cornerArray [6] = ComputeIntersection (ref this.planes [5], ref ray);
		}

		internal void SupportMapping (ref Vector3 v, out Vector3 result)
		{
			Fixed32 num3;

			int index = 0;

			Vector3.Dot (ref this.cornerArray [0], ref v, out num3);

			for (int i = 1; i < this.cornerArray.Length; i++)
			{
				Fixed32 num2;

				Vector3.Dot (ref this.cornerArray [i], ref v, out num2);

				if (num2 > num3)
				{
					index = i;
					num3 = num2;
				}
			}

			result = this.cornerArray [index];
		}

		public override String ToString ()
		{
			return string.Format ("{{Near:{0} Far:{1} Left:{2} Right:{3} Top:{4} Bottom:{5}}}", new Object[] { this.Near.ToString (), this.Far.ToString (), this.Left.ToString (), this.Right.ToString (), this.Top.ToString (), this.Bottom.ToString () });
		}

		// Properties
		public Plane Bottom
		{
			get
			{
				return this.planes [5];
			}
		}

		public Plane Far {
			get {
				return this.planes [1];
			}
		}

		public Plane Left {
			get {
				return this.planes [2];
			}
		}

		public Matrix44 Matrix {
			get {
				return this.matrix;
			}
			set {
				this.SetMatrix (ref value);
			}
		}

		public Plane Near {
			get {
				return this.planes [0];
			}
		}

		public Plane Right {
			get {
				return this.planes [3];
			}
		}

		public Plane Top {
			get {
				return this.planes [4];
			}
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct BoundingSphere 
		: IEquatable<BoundingSphere>
	{
		public Vector3 Center;
		public Fixed32 Radius;

		public BoundingSphere (Vector3 center, Fixed32 radius)
		{
			Fixed32 zero = 0;

			if (radius < zero) {
				throw new ArgumentException ("NegativeRadius");
			}
			this.Center = center;
			this.Radius = radius;
		}

		public Boolean Equals (BoundingSphere other)
		{
			return ((this.Center == other.Center) && (this.Radius == other.Radius));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is BoundingSphere) {
				flag = this.Equals ((BoundingSphere)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Center.GetHashCode () + this.Radius.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Center:{0} Radius:{1}}}", new Object[] { this.Center.ToString (), this.Radius.ToString () });
		}

		public static void CreateMerged (ref BoundingSphere original, ref BoundingSphere additional, out BoundingSphere result)
		{
			Fixed32 half; RealMaths.Half(out half);
			Fixed32 one = 1;
			Vector3 vector2;
			Vector3.Subtract (ref additional.Center, ref original.Center, out vector2);
			Fixed32 num = vector2.Length ();
			Fixed32 radius = original.Radius;
			Fixed32 num2 = additional.Radius;
			if ((radius + num2) >= num) {
				if ((radius - num2) >= num) {
					result = original;
					return;
				}
				if ((num2 - radius) >= num) {
					result = additional;
					return;
				}
			}
			Vector3 vector = (Vector3)(vector2 * (one / num));
			Fixed32 num5 = RealMaths.Min (-radius, num - num2);
			Fixed32 num4 = (RealMaths.Max (radius, num + num2) - num5) * half;
			result.Center = original.Center + ((Vector3)(vector * (num4 + num5)));
			result.Radius = num4;
		}

		public static void CreateFromBoundingBox (ref BoundingBox box, out BoundingSphere result)
		{
			Fixed32 half; RealMaths.Half(out half);
			Fixed32 num;
			Vector3.Lerp (ref box.Min, ref box.Max, half, out result.Center);
			Vector3.Distance (ref box.Min, ref box.Max, out num);
			result.Radius = num * half;
		}

		public static void CreateFromPoints (IEnumerable<Vector3> points, out BoundingSphere sphere)
		{	
			Fixed32 half; RealMaths.Half(out half);
			Fixed32 one = 1;

			Fixed32 num;
			Fixed32 num2;
			Vector3 vector2;
			Fixed32 num4;
			Fixed32 num5;
			
			Vector3 vector5;
			Vector3 vector6;
			Vector3 vector7;
			Vector3 vector8;
			Vector3 vector9;
			if (points == null) {
				throw new ArgumentNullException ("points");
			}
			IEnumerator<Vector3> enumerator = points.GetEnumerator ();
			if (!enumerator.MoveNext ()) {
				throw new ArgumentException ("BoundingSphereZeroPoints");
			}
			Vector3 vector4 = vector5 = vector6 = vector7 = vector8 = vector9 = enumerator.Current;
			foreach (Vector3 vector in points) {
				if (vector.X < vector4.X) {
					vector4 = vector;
				}
				if (vector.X > vector5.X) {
					vector5 = vector;
				}
				if (vector.Y < vector6.Y) {
					vector6 = vector;
				}
				if (vector.Y > vector7.Y) {
					vector7 = vector;
				}
				if (vector.Z < vector8.Z) {
					vector8 = vector;
				}
				if (vector.Z > vector9.Z) {
					vector9 = vector;
				}
			}
			Vector3.Distance (ref vector5, ref vector4, out num5);
			Vector3.Distance (ref vector7, ref vector6, out num4);
			Vector3.Distance (ref vector9, ref vector8, out num2);
			if (num5 > num4) {
				if (num5 > num2) {
					Vector3.Lerp (ref vector5, ref vector4, half, out vector2);
					num = num5 * half;
				} else {
					Vector3.Lerp (ref vector9, ref vector8, half, out vector2);
					num = num2 * half;
				}
			} else if (num4 > num2) {
				Vector3.Lerp (ref vector7, ref vector6, half, out vector2);
				num = num4 * half;
			} else {
				Vector3.Lerp (ref vector9, ref vector8, half, out vector2);
				num = num2 * half;
			}
			foreach (Vector3 vector10 in points) {
				Vector3 vector3;
				vector3.X = vector10.X - vector2.X;
				vector3.Y = vector10.Y - vector2.Y;
				vector3.Z = vector10.Z - vector2.Z;
				Fixed32 num3 = vector3.Length ();
				if (num3 > num) {
					num = (num + num3) * half;
					vector2 += (Vector3)((one - (num / num3)) * vector3);
				}
			}
			sphere.Center = vector2;
			sphere.Radius = num;
		}

		public static void CreateFromFrustum (ref BoundingFrustum frustum, out BoundingSphere sphere)
		{
			if (frustum == null) {
				throw new ArgumentNullException ("frustum");
			}

			CreateFromPoints (frustum.cornerArray, out sphere);
		}

		public Boolean Intersects (ref BoundingBox box)
		{
			Fixed32 num;
			Vector3 vector;
			Vector3.Clamp (ref this.Center, ref box.Min, ref box.Max, out vector);
			Vector3.DistanceSquared (ref this.Center, ref vector, out num);
			return (num <= (this.Radius * this.Radius));
		}

		public Boolean Intersects (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			return frustum.Intersects (ref this);
		}

		public PlaneIntersectionType Intersects (ref Plane plane)
		{
			return plane.Intersects (ref this);
		}

		public Fixed32? Intersects (ref Ray ray)
		{
			return ray.Intersects (ref this);
		}

		public Boolean Intersects (ref BoundingSphere sphere)
		{
			Fixed32 two = 2;

			Fixed32 num3;
			Vector3.DistanceSquared (ref this.Center, ref sphere.Center, out num3);
			Fixed32 radius = this.Radius;
			Fixed32 num = sphere.Radius;
			if ((((radius * radius) + ((two * radius) * num)) + (num * num)) <= num3) {
				return false;
			}
			return true;
		}

		public ContainmentType Contains (ref BoundingBox box)
		{
			Vector3 vector;
			if (!box.Intersects (ref this)) {
				return ContainmentType.Disjoint;
			}
			Fixed32 num = this.Radius * this.Radius;
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Max.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Max.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Max.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			vector.X = this.Center.X - box.Min.X;
			vector.Y = this.Center.Y - box.Min.Y;
			vector.Z = this.Center.Z - box.Min.Z;
			if (vector.LengthSquared () > num) {
				return ContainmentType.Intersects;
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			if (!frustum.Intersects (ref this)) {
				return ContainmentType.Disjoint;
			}
			Fixed32 num2 = this.Radius * this.Radius;
			foreach (Vector3 vector2 in frustum.cornerArray) {
				Vector3 vector;
				vector.X = vector2.X - this.Center.X;
				vector.Y = vector2.Y - this.Center.Y;
				vector.Z = vector2.Z - this.Center.Z;
				if (vector.LengthSquared () > num2) {
					return ContainmentType.Intersects;
				}
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref Vector3 point)
		{
			Fixed32 temp;
			Vector3.DistanceSquared (ref point, ref this.Center, out temp);

			if (temp >= (this.Radius * this.Radius))
			{
				return ContainmentType.Disjoint;
			}
			return ContainmentType.Contains;
		}

		public ContainmentType Contains (ref BoundingSphere sphere)
		{
			Fixed32 num3;
			Vector3.Distance (ref this.Center, ref sphere.Center, out num3);
			Fixed32 radius = this.Radius;
			Fixed32 num = sphere.Radius;
			if ((radius + num) < num3) {
				return ContainmentType.Disjoint;
			}
			if ((radius - num) < num3) {
				return ContainmentType.Intersects;
			}
			return ContainmentType.Contains;
		}

		internal void SupportMapping (ref Vector3 v, out Vector3 result)
		{
			Fixed32 num2 = v.Length ();
			Fixed32 num = this.Radius / num2;
			result.X = this.Center.X + (v.X * num);
			result.Y = this.Center.Y + (v.Y * num);
			result.Z = this.Center.Z + (v.Z * num);
		}

		public BoundingSphere Transform (Matrix44 matrix)
		{
			BoundingSphere sphere = new BoundingSphere ();
			Vector3.Transform (ref this.Center, ref matrix, out sphere.Center);
			Fixed32 num4 = ((matrix.M11 * matrix.M11) + (matrix.M12 * matrix.M12)) + (matrix.M13 * matrix.M13);
			Fixed32 num3 = ((matrix.M21 * matrix.M21) + (matrix.M22 * matrix.M22)) + (matrix.M23 * matrix.M23);
			Fixed32 num2 = ((matrix.M31 * matrix.M31) + (matrix.M32 * matrix.M32)) + (matrix.M33 * matrix.M33);
			Fixed32 num = RealMaths.Max (num4, RealMaths.Max (num3, num2));
			sphere.Radius = this.Radius * (RealMaths.Sqrt (num));
			return sphere;
		}

		public void Transform (ref Matrix44 matrix, out BoundingSphere result)
		{
			Vector3.Transform (ref this.Center, ref matrix, out result.Center);
			Fixed32 num4 = ((matrix.M11 * matrix.M11) + (matrix.M12 * matrix.M12)) + (matrix.M13 * matrix.M13);
			Fixed32 num3 = ((matrix.M21 * matrix.M21) + (matrix.M22 * matrix.M22)) + (matrix.M23 * matrix.M23);
			Fixed32 num2 = ((matrix.M31 * matrix.M31) + (matrix.M32 * matrix.M32)) + (matrix.M33 * matrix.M33);
			Fixed32 num = RealMaths.Max (num4, RealMaths.Max (num3, num2));
			result.Radius = this.Radius * (RealMaths.Sqrt (num));
		}

		public static Boolean operator == (BoundingSphere a, BoundingSphere b)
		{
			return a.Equals (b);
		}

		public static Boolean operator != (BoundingSphere a, BoundingSphere b)
		{
			if (!(a.Center != b.Center)) {
				return !(a.Radius == b.Radius);
			}
			return true;
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Matrix44 
		: IEquatable<Matrix44>
	{
		// Row 0
		public Fixed32 M11;
		public Fixed32 M12;
		public Fixed32 M13;
		public Fixed32 M14;

		// Row 1
		public Fixed32 M21;
		public Fixed32 M22;
		public Fixed32 M23;
		public Fixed32 M24;

		// Row 2
		public Fixed32 M31;
		public Fixed32 M32;
		public Fixed32 M33;
		public Fixed32 M34;

		// Row 3
		public Fixed32 M41; // translation.x
		public Fixed32 M42; // translation.y
		public Fixed32 M43; // translation.z
		public Fixed32 M44;
		
		public Vector3 Up {
			get {
				Vector3 vector;
				vector.X = this.M21;
				vector.Y = this.M22;
				vector.Z = this.M23;
				return vector;
			}
			set {
				this.M21 = value.X;
				this.M22 = value.Y;
				this.M23 = value.Z;
			}
		}

		public Vector3 Down {
			get {
				Vector3 vector;
				vector.X = -this.M21;
				vector.Y = -this.M22;
				vector.Z = -this.M23;
				return vector;
			}
			set {
				this.M21 = -value.X;
				this.M22 = -value.Y;
				this.M23 = -value.Z;
			}
		}

		public Vector3 Right {
			get {
				Vector3 vector;
				vector.X = this.M11;
				vector.Y = this.M12;
				vector.Z = this.M13;
				return vector;
			}
			set {
				this.M11 = value.X;
				this.M12 = value.Y;
				this.M13 = value.Z;
			}
		}

		public Vector3 Left {
			get {
				Vector3 vector;
				vector.X = -this.M11;
				vector.Y = -this.M12;
				vector.Z = -this.M13;
				return vector;
			}
			set {
				this.M11 = -value.X;
				this.M12 = -value.Y;
				this.M13 = -value.Z;
			}
		}

		public Vector3 Forward {
			get {
				Vector3 vector;
				vector.X = -this.M31;
				vector.Y = -this.M32;
				vector.Z = -this.M33;
				return vector;
			}
			set {
				this.M31 = -value.X;
				this.M32 = -value.Y;
				this.M33 = -value.Z;
			}
		}

		public Vector3 Backward {
			get {
				Vector3 vector;
				vector.X = this.M31;
				vector.Y = this.M32;
				vector.Z = this.M33;
				return vector;
			}
			set {
				this.M31 = value.X;
				this.M32 = value.Y;
				this.M33 = value.Z;
			}
		}

		public Vector3 Translation {
			get {
				Vector3 vector;
				vector.X = this.M41;
				vector.Y = this.M42;
				vector.Z = this.M43;
				return vector;
			}
			set {
				this.M41 = value.X;
				this.M42 = value.Y;
				this.M43 = value.Z;
			}
		}

		public Matrix44 (Fixed32 m11, Fixed32 m12, Fixed32 m13, Fixed32 m14, Fixed32 m21, Fixed32 m22, Fixed32 m23, Fixed32 m24, Fixed32 m31, Fixed32 m32, Fixed32 m33, Fixed32 m34, Fixed32 m41, Fixed32 m42, Fixed32 m43, Fixed32 m44)
		{
			this.M11 = m11;
			this.M12 = m12;
			this.M13 = m13;
			this.M14 = m14;
			this.M21 = m21;
			this.M22 = m22;
			this.M23 = m23;
			this.M24 = m24;
			this.M31 = m31;
			this.M32 = m32;
			this.M33 = m33;
			this.M34 = m34;
			this.M41 = m41;
			this.M42 = m42;
			this.M43 = m43;
			this.M44 = m44;
		}

		public override String ToString ()
		{
			return ("{ " + string.Format ("{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", new Object[] { this.M11.ToString (), this.M12.ToString (), this.M13.ToString (), this.M14.ToString () }) + string.Format ("{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", new Object[] { this.M21.ToString (), this.M22.ToString (), this.M23.ToString (), this.M24.ToString () }) + string.Format ("{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", new Object[] { this.M31.ToString (), this.M32.ToString (), this.M33.ToString (), this.M34.ToString () }) + string.Format ("{{M41:{0} M42:{1} M43:{2} M44:{3}}} ", new Object[] { this.M41.ToString (), this.M42.ToString (), this.M43.ToString (), this.M44.ToString () }) + "}");
		}

		public Boolean Equals (Matrix44 other)
		{
			return ((((((this.M11 == other.M11) && (this.M22 == other.M22)) && ((this.M33 == other.M33) && (this.M44 == other.M44))) && (((this.M12 == other.M12) && (this.M13 == other.M13)) && ((this.M14 == other.M14) && (this.M21 == other.M21)))) && ((((this.M23 == other.M23) && (this.M24 == other.M24)) && ((this.M31 == other.M31) && (this.M32 == other.M32))) && (((this.M34 == other.M34) && (this.M41 == other.M41)) && (this.M42 == other.M42)))) && (this.M43 == other.M43));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Matrix44)
			{
				flag = this.Equals ((Matrix44)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (((((((((((((((this.M11.GetHashCode () + this.M12.GetHashCode ()) + this.M13.GetHashCode ()) + this.M14.GetHashCode ()) + this.M21.GetHashCode ()) + this.M22.GetHashCode ()) + this.M23.GetHashCode ()) + this.M24.GetHashCode ()) + this.M31.GetHashCode ()) + this.M32.GetHashCode ()) + this.M33.GetHashCode ()) + this.M34.GetHashCode ()) + this.M41.GetHashCode ()) + this.M42.GetHashCode ()) + this.M43.GetHashCode ()) + this.M44.GetHashCode ());
		}

		#region Constants

		static Matrix44 identity;

		static Matrix44 ()
		{
			Fixed32 zero = 0;
			Fixed32 one = 1;
			identity = new Matrix44 (one, zero, zero, zero, zero, one, zero, zero, zero, zero, one, zero, zero, zero, zero, one);
		}

		public static Matrix44 Identity {
			get {
				return identity;
			}
		}
		
		#endregion
		#region Create

		public static void CreateTranslation (ref Vector3 position, out Matrix44 result)
		{
			result.M11 = 1;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = 1;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = 1;
			result.M34 = 0;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1;
		}
		
		public static void CreateTranslation (Fixed32 xPosition, Fixed32 yPosition, Fixed32 zPosition, out Matrix44 result)
		{	
			result.M11 = 1;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = 1;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = 1;
			result.M34 = 0;
			result.M41 = xPosition;
			result.M42 = yPosition;
			result.M43 = zPosition;
			result.M44 = 1;
		}
		
		// Creates a scaling matrix based on x, y, z.
		public static void CreateScale (Fixed32 xScale, Fixed32 yScale, Fixed32 zScale, out Matrix44 result)
		{
			result.M11 = xScale;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = yScale;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = zScale;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		// Creates a scaling matrix based on a vector.
		public static void CreateScale (ref Vector3 scales, out Matrix44 result)
		{
			result.M11 = scales.X;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = scales.Y;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = scales.Z;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		// Create a scaling matrix consistant along each axis
		public static void CreateScale (Fixed32 scale, out Matrix44 result)
		{
			result.M11 = scale;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = scale;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = scale;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		public static void CreateRotationX (Fixed32 radians, out Matrix44 result)
		{
			// http://en.wikipedia.org/wiki/Rotation_matrix

			Fixed32 cos = RealMaths.Cos (radians);
			Fixed32 sin = RealMaths.Sin (radians);

			result.M11 = 1;
			result.M12 = 0;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = cos;
			result.M23 = sin;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = -sin;
			result.M33 = cos;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		public static void CreateRotationY (Fixed32 radians, out Matrix44 result)
		{
			// http://en.wikipedia.org/wiki/Rotation_matrix

			Fixed32 cos = RealMaths.Cos (radians);
			Fixed32 sin = RealMaths.Sin (radians);

			result.M11 = cos;
			result.M12 = 0;
			result.M13 = -sin;
			result.M14 = 0;
			result.M21 = 0;
			result.M22 = 1;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = sin;
			result.M32 = 0;
			result.M33 = cos;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}
		
		public static void CreateRotationZ (Fixed32 radians, out Matrix44 result)
		{
			// http://en.wikipedia.org/wiki/Rotation_matrix

			Fixed32 cos = RealMaths.Cos (radians);
			Fixed32 sin = RealMaths.Sin (radians);

			result.M11 = cos;
			result.M12 = sin;
			result.M13 = 0;
			result.M14 = 0;
			result.M21 = -sin;
			result.M22 = cos;
			result.M23 = 0;
			result.M24 = 0;
			result.M31 = 0;
			result.M32 = 0;
			result.M33 = 1;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}
		
		public static void CreateFromAxisAngle (ref Vector3 axis, Fixed32 angle, out Matrix44 result)
		{
			Fixed32 one = 1;

			Fixed32 x = axis.X;
			Fixed32 y = axis.Y;
			Fixed32 z = axis.Z;

			Fixed32 sin = RealMaths.Sin (angle);
			Fixed32 cos = RealMaths.Cos (angle);

			Fixed32 xx = x * x;
			Fixed32 yy = y * y;
			Fixed32 zz = z * z;

			Fixed32 xy = x * y;
			Fixed32 xz = x * z;
			Fixed32 yz = y * z;

			result.M11 = xx + (cos * (one - xx));
			result.M12 = (xy - (cos * xy)) + (sin * z);
			result.M13 = (xz - (cos * xz)) - (sin * y);
			result.M14 = 0;

			result.M21 = (xy - (cos * xy)) - (sin * z);
			result.M22 = yy + (cos * (one - yy));
			result.M23 = (yz - (cos * yz)) + (sin * x);
			result.M24 = 0;

			result.M31 = (xz - (cos * xz)) + (sin * y);
			result.M32 = (yz - (cos * yz)) - (sin * x);
			result.M33 = zz + (cos * (one - zz));
			result.M34 = 0;

			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = one;
		}
		
		public static void CreateFromAllAxis (ref Vector3 right, ref Vector3 up, ref Vector3 backward, out Matrix44 result)
		{
			if(!right.IsUnit() || !up.IsUnit() || !backward.IsUnit() )
			{
				throw new ArgumentException("The input vertors must be normalised.");
			}

			result.M11 = right.X;
			result.M12 = right.Y;
			result.M13 = right.Z;
			result.M14 = 0;
			result.M21 = up.X;
			result.M22 = up.Y;
			result.M23 = up.Z;
			result.M24 = 0;
			result.M31 = backward.X;
			result.M32 = backward.Y;
			result.M33 = backward.Z;
			result.M34 = 0;
			result.M41 = 0;
			result.M42 = 0;
			result.M43 = 0;
			result.M44 = 1;
		}

		public static void CreateWorldNew (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result)
		{
			Vector3 backward = -forward;

			Vector3 right;

			Vector3.Cross (ref up, ref backward, out right);

			right.Normalise();

			Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out result);

			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
		}

		public static void CreateWorld (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result)
		{
			if(!forward.IsUnit() || !up.IsUnit() )
			{
				throw new ArgumentException("The input vertors must be normalised.");
			}

			Vector3 backward = -forward;

			Vector3 vector; Vector3.Normalise (ref backward, out vector);

			Vector3 cross; Vector3.Cross (ref up, ref vector, out cross);

			Vector3 vector2; Vector3.Normalise (ref cross, out vector2);

			Vector3 vector3; Vector3.Cross (ref vector, ref vector2, out vector3);

			result.M11 = vector2.X;
			result.M12 = vector2.Y;
			result.M13 = vector2.Z;
			result.M14 = 0;
			result.M21 = vector3.X;
			result.M22 = vector3.Y;
			result.M23 = vector3.Z;
			result.M24 = 0;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = 0;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1;
		}

		public static void CreateFromQuaternion (ref Quaternion quaternion, out Matrix44 result)
		{
			if(!quaternion.IsUnit())
			{
				throw new ArgumentException("Input quaternion must be normalised.");
			}

			Fixed32 zero = 0;
			Fixed32 one = 1;

			Fixed32 xs = quaternion.X + quaternion.X;   
			Fixed32 ys = quaternion.Y + quaternion.Y;
			Fixed32 zs = quaternion.Z + quaternion.Z;
			Fixed32 wx = quaternion.W * xs;
			Fixed32 wy = quaternion.W * ys;
			Fixed32 wz = quaternion.W * zs;
			Fixed32 xx = quaternion.X * xs;
			Fixed32 xy = quaternion.X * ys;
			Fixed32 xz = quaternion.X * zs;
			Fixed32 yy = quaternion.Y * ys;
			Fixed32 yz = quaternion.Y * zs;
			Fixed32 zz = quaternion.Z * zs;

			result.M11 = one - (yy + zz);
			result.M21 = xy - wz;
			result.M31 = xz + wy;
			result.M41 = zero;
    
			result.M12 = xy + wz;
			result.M22 = one - (xx + zz);
			result.M32 = yz - wx;
			result.M42 = zero;
    
			result.M13 = xz - wy;
			result.M23 = yz + wx;
			result.M33 = one - (xx + yy);
			result.M43 = zero;

			result.M14 = zero;
			result.M24 = zero;
			result.M34 = zero;
			result.M44 = one;
		}



		// todo: remove when we dont need this for the tests
		internal static void CreateFromQuaternionOld (ref Quaternion quaternion, out Matrix44 result)
		{
			Fixed32 zero = 0;
			Fixed32 one; RealMaths.One(out one);
			Fixed32 two = 2;

			Fixed32 num9 = quaternion.X * quaternion.X;
			Fixed32 num8 = quaternion.Y * quaternion.Y;
			Fixed32 num7 = quaternion.Z * quaternion.Z;
			Fixed32 num6 = quaternion.X * quaternion.Y;
			Fixed32 num5 = quaternion.Z * quaternion.W;
			Fixed32 num4 = quaternion.Z * quaternion.X;
			Fixed32 num3 = quaternion.Y * quaternion.W;
			Fixed32 num2 = quaternion.Y * quaternion.Z;
			Fixed32 num = quaternion.X * quaternion.W;
			result.M11 = one - (two * (num8 + num7));
			result.M12 = two * (num6 + num5);
			result.M13 = two * (num4 - num3);
			result.M14 = zero;
			result.M21 = two * (num6 - num5);
			result.M22 = one - (two * (num7 + num9));
			result.M23 = two * (num2 + num);
			result.M24 = zero;
			result.M31 = two * (num4 + num3);
			result.M32 = two * (num2 - num);
			result.M33 = one - (two * (num8 + num9));
			result.M34 = zero;
			result.M41 = zero;
			result.M42 = zero;
			result.M43 = zero;
			result.M44 = one;
		}

		public static void CreateFromYawPitchRoll (Fixed32 yaw, Fixed32 pitch, Fixed32 roll, out Matrix44 result)
		{
			Quaternion quaternion;

			Quaternion.CreateFromYawPitchRoll (yaw, pitch, roll, out quaternion);

			CreateFromQuaternion (ref quaternion, out result);
		}










		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////
		// TODO: REVIEW FROM HERE ONWARDS
		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////


		// FROM XNA
		// --------
		// Creates a cylindrical billboard that rotates around a specified axis.
		// This method computes the facing direction of the billboard from the object position and camera position. 
		// When the object and camera positions are too close, the matrix will not be accurate. 
		// To avoid this problem, the method uses the optional camera forward vector if the positions are too close.
		public static void CreateBillboard (ref Vector3 ObjectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix44 result)
		{
			Fixed32 zero = 0;
			Fixed32 one; RealMaths.One(out one);

			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			vector.X = ObjectPosition.X - cameraPosition.X;
			vector.Y = ObjectPosition.Y - cameraPosition.Y;
			vector.Z = ObjectPosition.Z - cameraPosition.Z;
			Fixed32 num = vector.LengthSquared ();
			Fixed32 limit; RealMaths.FromString("0.0001", out limit);

			if (num < limit) {
				vector = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
			} else {
				Vector3.Multiply (ref vector, (Fixed32)(one / (RealMaths.Sqrt (num))), out vector);
			}
			Vector3.Cross (ref cameraUpVector, ref vector, out vector3);
			vector3.Normalise ();
			Vector3.Cross (ref vector, ref vector3, out vector2);
			result.M11 = vector3.X;
			result.M12 = vector3.Y;
			result.M13 = vector3.Z;
			result.M14 = zero;
			result.M21 = vector2.X;
			result.M22 = vector2.Y;
			result.M23 = vector2.Z;
			result.M24 = zero;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = zero;
			result.M41 = ObjectPosition.X;
			result.M42 = ObjectPosition.Y;
			result.M43 = ObjectPosition.Z;
			result.M44 = one;
		}
		
		public static void CreateConstrainedBillboard (ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector, out Matrix44 result)
		{
			Fixed32 zero = 0;
			Fixed32 one; RealMaths.One(out one);

			Fixed32 num;
			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			vector2.X = objectPosition.X - cameraPosition.X;
			vector2.Y = objectPosition.Y - cameraPosition.Y;
			vector2.Z = objectPosition.Z - cameraPosition.Z;
			Fixed32 num2 = vector2.LengthSquared ();
			Fixed32 limit; RealMaths.FromString("0.0001", out limit);

			if (num2 < limit) {
				vector2 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
			} else {
				Vector3.Multiply (ref vector2, (Fixed32)(one / (RealMaths.Sqrt (num2))), out vector2);
			}
			Vector3 vector4 = rotateAxis;
			Vector3.Dot (ref rotateAxis, ref vector2, out num);

			Fixed32 realHorrid; RealMaths.FromString("0.9982547", out realHorrid);

			if (RealMaths.Abs (num) > realHorrid) {
				if (objectForwardVector.HasValue) {
					vector = objectForwardVector.Value;
					Vector3.Dot (ref rotateAxis, ref vector, out num);
					if (RealMaths.Abs (num) > realHorrid) {
						num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
						vector = (RealMaths.Abs (num) > realHorrid) ? Vector3.Right : Vector3.Forward;
					}
				} else {
					num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
					vector = (RealMaths.Abs (num) > realHorrid) ? Vector3.Right : Vector3.Forward;
				}
				Vector3.Cross (ref rotateAxis, ref vector, out vector3);
				vector3.Normalise ();
				Vector3.Cross (ref vector3, ref rotateAxis, out vector);
				vector.Normalise ();
			} else {
				Vector3.Cross (ref rotateAxis, ref vector2, out vector3);
				vector3.Normalise ();
				Vector3.Cross (ref vector3, ref vector4, out vector);
				vector.Normalise ();
			}
			result.M11 = vector3.X;
			result.M12 = vector3.Y;
			result.M13 = vector3.Z;
			result.M14 = zero;
			result.M21 = vector4.X;
			result.M22 = vector4.Y;
			result.M23 = vector4.Z;
			result.M24 = zero;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = zero;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = one;
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
		public static void CreatePerspectiveFieldOfView (Fixed32 fieldOfView, Fixed32 aspectRatio, Fixed32 nearPlaneDistance, Fixed32 farPlaneDistance, out Matrix44 result)
		{
			Fixed32 zero = 0;
			Fixed32 half; RealMaths.Half(out half);
			Fixed32 one; RealMaths.One(out one);
			Fixed32 pi; RealMaths.Pi(out pi);

			if ((fieldOfView <= zero) || (fieldOfView >= pi)) {
				throw new ArgumentOutOfRangeException ("fieldOfView");
			}
			if (nearPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			if (farPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			Fixed32 num = one / (RealMaths.Tan ((fieldOfView * half)));
			Fixed32 num9 = num / aspectRatio;
			result.M11 = num9;
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = num;
			result.M21 = result.M23 = result.M24 = zero;
			result.M31 = result.M32 = zero;
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -one;
			result.M41 = result.M42 = result.M44 = zero;
			result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
		public static void CreatePerspective (Fixed32 width, Fixed32 height, Fixed32 nearPlaneDistance, Fixed32 farPlaneDistance, out Matrix44 result)
		{
			Fixed32 zero = 0;
			Fixed32 one; RealMaths.One(out one);
			Fixed32 two = 2;

			if (nearPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			if (farPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			result.M11 = (two * nearPlaneDistance) / width;
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = (two * nearPlaneDistance) / height;
			result.M21 = result.M23 = result.M24 = zero;
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M31 = result.M32 = zero;
			result.M34 = -one;
			result.M41 = result.M42 = result.M44 = zero;
			result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
		}


		// ref: http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx
		public static void CreatePerspectiveOffCenter (Fixed32 left, Fixed32 right, Fixed32 bottom, Fixed32 top, Fixed32 nearPlaneDistance, Fixed32 farPlaneDistance, out Matrix44 result)
		{
			Fixed32 zero = 0;
			Fixed32 one; RealMaths.One(out one);
			Fixed32 two = 2;

			if (nearPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			if (farPlaneDistance <= zero) {
				throw new ArgumentOutOfRangeException ("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance) {
				throw new ArgumentOutOfRangeException ("nearPlaneDistance");
			}
			result.M11 = (two * nearPlaneDistance) / (right - left);
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = (two * nearPlaneDistance) / (top - bottom);
			result.M21 = result.M23 = result.M24 = zero;
			result.M31 = (left + right) / (right - left);
			result.M32 = (top + bottom) / (top - bottom);
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -one;
			result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
			result.M41 = result.M42 = result.M44 = zero;
		}
		
		// ref: http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
		public static void CreateOrthographic (Fixed32 width, Fixed32 height, Fixed32 zNearPlane, Fixed32 zFarPlane, out Matrix44 result)
		{
			Fixed32 zero = 0;
			Fixed32 one; RealMaths.One(out one);
			Fixed32 two = 2;

			result.M11 = two / width;
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = two / height;
			result.M21 = result.M23 = result.M24 = zero;
			result.M33 = one / (zNearPlane - zFarPlane);
			result.M31 = result.M32 = result.M34 = zero;
			result.M41 = result.M42 = zero;
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = one;
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx
		public static void CreateOrthographicOffCenter (Fixed32 left, Fixed32 right, Fixed32 bottom, Fixed32 top, Fixed32 zNearPlane, Fixed32 zFarPlane, out Matrix44 result)
		{
			Fixed32 zero = 0;
			Fixed32 one; RealMaths.One(out one);
			Fixed32 two = 2;

			result.M11 = two / (right - left);
			result.M12 = result.M13 = result.M14 = zero;
			result.M22 = two / (top - bottom);
			result.M21 = result.M23 = result.M24 = zero;
			result.M33 = one / (zNearPlane - zFarPlane);
			result.M31 = result.M32 = result.M34 = zero;
			result.M41 = (left + right) / (left - right);
			result.M42 = (top + bottom) / (bottom - top);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = one;
		}
		
		// ref: http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx
		public static void CreateLookAt (ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix44 result)
		{
			Fixed32 zero = 0;
			Fixed32 one; RealMaths.One(out one);

			Vector3 targetToPosition = cameraPosition - cameraTarget;

			Vector3 vector; Vector3.Normalise (ref targetToPosition, out vector);

			Vector3 cross; Vector3.Cross (ref cameraUpVector, ref vector, out cross); 

			Vector3 vector2; Vector3.Normalise (ref cross, out vector2);
			Vector3 vector3; Vector3.Cross (ref vector, ref vector2, out vector3);
			result.M11 = vector2.X;
			result.M12 = vector3.X;
			result.M13 = vector.X;
			result.M14 = zero;
			result.M21 = vector2.Y;
			result.M22 = vector3.Y;
			result.M23 = vector.Y;
			result.M24 = zero;
			result.M31 = vector2.Z;
			result.M32 = vector3.Z;
			result.M33 = vector.Z;
			result.M34 = zero;

			Vector3.Dot (ref vector2, ref cameraPosition, out result.M41);
			Vector3.Dot (ref vector3, ref cameraPosition, out result.M42);
			Vector3.Dot (ref vector, ref cameraPosition, out result.M43);
			
			result.M41 *= -one;
			result.M42 *= -one;
			result.M43 *= -one;

			result.M44 = one;
		}

		
	

		// ref: http://msdn.microsoft.com/en-us/library/bb205364(v=VS.85).aspx
		public static void CreateShadow (ref Vector3 lightDirection, ref Plane plane, out Matrix44 result)
		{
			Fixed32 zero = 0;
			
			Plane plane2;
			Plane.Normalise (ref plane, out plane2);
			Fixed32 num = ((plane2.Normal.X * lightDirection.X) + (plane2.Normal.Y * lightDirection.Y)) + (plane2.Normal.Z * lightDirection.Z);
			Fixed32 num5 = -plane2.Normal.X;
			Fixed32 num4 = -plane2.Normal.Y;
			Fixed32 num3 = -plane2.Normal.Z;
			Fixed32 num2 = -plane2.D;
			result.M11 = (num5 * lightDirection.X) + num;
			result.M21 = num4 * lightDirection.X;
			result.M31 = num3 * lightDirection.X;
			result.M41 = num2 * lightDirection.X;
			result.M12 = num5 * lightDirection.Y;
			result.M22 = (num4 * lightDirection.Y) + num;
			result.M32 = num3 * lightDirection.Y;
			result.M42 = num2 * lightDirection.Y;
			result.M13 = num5 * lightDirection.Z;
			result.M23 = num4 * lightDirection.Z;
			result.M33 = (num3 * lightDirection.Z) + num;
			result.M43 = num2 * lightDirection.Z;
			result.M14 = zero;
			result.M24 = zero;
			result.M34 = zero;
			result.M44 = num;
		}

		// ref: http://msdn.microsoft.com/en-us/library/bb205356(v=VS.85).aspx
		public static void CreateReflection (ref Plane value, out Matrix44 result)
		{
			Fixed32 zero = 0;
			Fixed32 one; RealMaths.One(out one);
			Fixed32 two = 2;

			Plane plane;
			
			Plane.Normalise (ref value, out plane);
			
			value.Normalise ();
			
			Fixed32 x = plane.Normal.X;
			Fixed32 y = plane.Normal.Y;
			Fixed32 z = plane.Normal.Z;
			
			Fixed32 num3 = -two * x;
			Fixed32 num2 = -two * y;
			Fixed32 num = -two * z;
			
			result.M11 = (num3 * x) + one;
			result.M12 = num2 * x;
			result.M13 = num * x;
			result.M14 = zero;
			result.M21 = num3 * y;
			result.M22 = (num2 * y) + one;
			result.M23 = num * y;
			result.M24 = zero;
			result.M31 = num3 * z;
			result.M32 = num2 * z;
			result.M33 = (num * z) + one;
			result.M34 = zero;
			result.M41 = num3 * plane.D;
			result.M42 = num2 * plane.D;
			result.M43 = num * plane.D;
			result.M44 = one;
		}
		
		#endregion
		#region Maths

		//----------------------------------------------------------------------
		// Transpose
		//
		public void Transpose()
		{
			Fixed32 temp = this.M12;
			this.M12 = this.M21;
			this.M21 = temp;

			temp = this.M13;
			this.M13 = this.M31;
			this.M31 = temp;

			temp = this.M14;
			this.M14 = this.M41;
			this.M41 = temp;

			temp = this.M23;
			this.M23 = this.M32;
			this.M32 = temp;

			temp = this.M24;
			this.M24 = this.M42;
			this.M42 = temp;

			temp =  this.M34;
			this.M34 = this.M43;
			this.M43 = temp;
		}

		public static void Transpose (ref Matrix44 input, out Matrix44 output)
		{
		    output.M11 = input.M11;
			output.M12 = input.M21;
			output.M13 = input.M31;
			output.M14 = input.M41;
			output.M21 = input.M12;
			output.M22 = input.M22;
			output.M23 = input.M32;
			output.M24 = input.M42;
			output.M31 = input.M13;
			output.M32 = input.M23;
			output.M33 = input.M33;
			output.M34 = input.M43;
			output.M41 = input.M14;
			output.M42 = input.M24;
			output.M43 = input.M34;
			output.M44 = input.M44;
		}

		//----------------------------------------------------------------------
		// Decompose
		// ref: Essential Mathemathics For Games & Interactive Applications
		public bool Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
		{
			translation.X = M41;
            translation.Y = M42;
            translation.Z = M43;

			Vector3 a = new Vector3(M11, M21, M31);
			Vector3 b = new Vector3(M12, M22, M32);
			Vector3 c = new Vector3(M13, M23, M33);

			scale.X = a.Length();
			scale.Y = b.Length();
			scale.Z = c.Length();

			if ( RealMaths.IsZero(scale.X) || 
				 RealMaths.IsZero(scale.Y) || 
				 RealMaths.IsZero(scale.Z) )
            {
				rotation = Quaternion.Identity;
				return false;
			}

			a.Normalise();
			b.Normalise();
			c.Normalise();

			Vector3 right = new Vector3(a.X, b.X, c.X);
			Vector3 up = new Vector3(a.Y, b.Y, c.Y);
			Vector3 backward = new Vector3(a.Z, b.Z, c.Z);

			right.Normalise();
			up.Normalise();
			backward.Normalise();

			Matrix44 rotMat;
			Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out rotMat);

			Quaternion.CreateFromRotationMatrix(ref rotMat, out rotation);

			return true;
		}




		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////
		// TODO: REVIEW FROM HERE ONWARDS
		/////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////


		//----------------------------------------------------------------------
		// Determinant
		//
		public Fixed32 Determinant ()
		{
			Fixed32 num22 = this.M11;
			Fixed32 num21 = this.M12;
			Fixed32 num20 = this.M13;
			Fixed32 num19 = this.M14;
			Fixed32 num12 = this.M21;
			Fixed32 num11 = this.M22;
			Fixed32 num10 = this.M23;
			Fixed32 num9 = this.M24;
			Fixed32 num8 = this.M31;
			Fixed32 num7 = this.M32;
			Fixed32 num6 = this.M33;
			Fixed32 num5 = this.M34;
			Fixed32 num4 = this.M41;
			Fixed32 num3 = this.M42;
			Fixed32 num2 = this.M43;
			Fixed32 num = this.M44;
			
			Fixed32 num18 = (num6 * num) - (num5 * num2);
			Fixed32 num17 = (num7 * num) - (num5 * num3);
			Fixed32 num16 = (num7 * num2) - (num6 * num3);
			Fixed32 num15 = (num8 * num) - (num5 * num4);
			Fixed32 num14 = (num8 * num2) - (num6 * num4);
			Fixed32 num13 = (num8 * num3) - (num7 * num4);
			
			return ((((num22 * (((num11 * num18) - (num10 * num17)) + (num9 * num16))) - (num21 * (((num12 * num18) - (num10 * num15)) + (num9 * num14)))) + (num20 * (((num12 * num17) - (num11 * num15)) + (num9 * num13)))) - (num19 * (((num12 * num16) - (num11 * num14)) + (num10 * num13))));
		}
		
		//----------------------------------------------------------------------
		// Invert
		//
		public static void Invert (ref Matrix44 matrix, out Matrix44 result)
		{
			Fixed32 one = 1;
			Fixed32 num5 = matrix.M11;
			Fixed32 num4 = matrix.M12;
			Fixed32 num3 = matrix.M13;
			Fixed32 num2 = matrix.M14;
			Fixed32 num9 = matrix.M21;
			Fixed32 num8 = matrix.M22;
			Fixed32 num7 = matrix.M23;
			Fixed32 num6 = matrix.M24;
			Fixed32 num17 = matrix.M31;
			Fixed32 num16 = matrix.M32;
			Fixed32 num15 = matrix.M33;
			Fixed32 num14 = matrix.M34;
			Fixed32 num13 = matrix.M41;
			Fixed32 num12 = matrix.M42;
			Fixed32 num11 = matrix.M43;
			Fixed32 num10 = matrix.M44;
			Fixed32 num23 = (num15 * num10) - (num14 * num11);
			Fixed32 num22 = (num16 * num10) - (num14 * num12);
			Fixed32 num21 = (num16 * num11) - (num15 * num12);
			Fixed32 num20 = (num17 * num10) - (num14 * num13);
			Fixed32 num19 = (num17 * num11) - (num15 * num13);
			Fixed32 num18 = (num17 * num12) - (num16 * num13);
			Fixed32 num39 = ((num8 * num23) - (num7 * num22)) + (num6 * num21);
			Fixed32 num38 = -(((num9 * num23) - (num7 * num20)) + (num6 * num19));
			Fixed32 num37 = ((num9 * num22) - (num8 * num20)) + (num6 * num18);
			Fixed32 num36 = -(((num9 * num21) - (num8 * num19)) + (num7 * num18));
			Fixed32 num = one / ((((num5 * num39) + (num4 * num38)) + (num3 * num37)) + (num2 * num36));
			result.M11 = num39 * num;
			result.M21 = num38 * num;
			result.M31 = num37 * num;
			result.M41 = num36 * num;
			result.M12 = -(((num4 * num23) - (num3 * num22)) + (num2 * num21)) * num;
			result.M22 = (((num5 * num23) - (num3 * num20)) + (num2 * num19)) * num;
			result.M32 = -(((num5 * num22) - (num4 * num20)) + (num2 * num18)) * num;
			result.M42 = (((num5 * num21) - (num4 * num19)) + (num3 * num18)) * num;
			Fixed32 num35 = (num7 * num10) - (num6 * num11);
			Fixed32 num34 = (num8 * num10) - (num6 * num12);
			Fixed32 num33 = (num8 * num11) - (num7 * num12);
			Fixed32 num32 = (num9 * num10) - (num6 * num13);
			Fixed32 num31 = (num9 * num11) - (num7 * num13);
			Fixed32 num30 = (num9 * num12) - (num8 * num13);
			result.M13 = (((num4 * num35) - (num3 * num34)) + (num2 * num33)) * num;
			result.M23 = -(((num5 * num35) - (num3 * num32)) + (num2 * num31)) * num;
			result.M33 = (((num5 * num34) - (num4 * num32)) + (num2 * num30)) * num;
			result.M43 = -(((num5 * num33) - (num4 * num31)) + (num3 * num30)) * num;
			Fixed32 num29 = (num7 * num14) - (num6 * num15);
			Fixed32 num28 = (num8 * num14) - (num6 * num16);
			Fixed32 num27 = (num8 * num15) - (num7 * num16);
			Fixed32 num26 = (num9 * num14) - (num6 * num17);
			Fixed32 num25 = (num9 * num15) - (num7 * num17);
			Fixed32 num24 = (num9 * num16) - (num8 * num17);
			result.M14 = -(((num4 * num29) - (num3 * num28)) + (num2 * num27)) * num;
			result.M24 = (((num5 * num29) - (num3 * num26)) + (num2 * num25)) * num;
			result.M34 = -(((num5 * num28) - (num4 * num26)) + (num2 * num24)) * num;
			result.M44 = (((num5 * num27) - (num4 * num25)) + (num3 * num24)) * num;
		}


		//----------------------------------------------------------------------
		// Transform - Transforms a Matrix by applying a Quaternion rotation.
		//
		public static void Transform (ref Matrix44 value, ref Quaternion rotation, out Matrix44 result)
		{
			Fixed32 one = 1;

			Fixed32 num21 = rotation.X + rotation.X;
			Fixed32 num11 = rotation.Y + rotation.Y;
			Fixed32 num10 = rotation.Z + rotation.Z;
			
			Fixed32 num20 = rotation.W * num21;
			Fixed32 num19 = rotation.W * num11;
			Fixed32 num18 = rotation.W * num10;
			Fixed32 num17 = rotation.X * num21;
			Fixed32 num16 = rotation.X * num11;
			Fixed32 num15 = rotation.X * num10;
			Fixed32 num14 = rotation.Y * num11;
			Fixed32 num13 = rotation.Y * num10;
			Fixed32 num12 = rotation.Z * num10;
			
			Fixed32 num9 = (one - num14) - num12;
			
			Fixed32 num8 = num16 - num18;
			Fixed32 num7 = num15 + num19;
			Fixed32 num6 = num16 + num18;
			
			Fixed32 num5 = (one - num17) - num12;
			
			Fixed32 num4 = num13 - num20;
			Fixed32 num3 = num15 - num19;
			Fixed32 num2 = num13 + num20;
			
			Fixed32 num = (one - num17) - num14;
			
			Fixed32 num37 = ((value.M11 * num9) + (value.M12 * num8)) + (value.M13 * num7);
			Fixed32 num36 = ((value.M11 * num6) + (value.M12 * num5)) + (value.M13 * num4);
			Fixed32 num35 = ((value.M11 * num3) + (value.M12 * num2)) + (value.M13 * num);
			
			Fixed32 num34 = value.M14;
			
			Fixed32 num33 = ((value.M21 * num9) + (value.M22 * num8)) + (value.M23 * num7);
			Fixed32 num32 = ((value.M21 * num6) + (value.M22 * num5)) + (value.M23 * num4);
			Fixed32 num31 = ((value.M21 * num3) + (value.M22 * num2)) + (value.M23 * num);
			
			Fixed32 num30 = value.M24;
			
			Fixed32 num29 = ((value.M31 * num9) + (value.M32 * num8)) + (value.M33 * num7);
			Fixed32 num28 = ((value.M31 * num6) + (value.M32 * num5)) + (value.M33 * num4);
			Fixed32 num27 = ((value.M31 * num3) + (value.M32 * num2)) + (value.M33 * num);
			
			Fixed32 num26 = value.M34;
			
			Fixed32 num25 = ((value.M41 * num9) + (value.M42 * num8)) + (value.M43 * num7);
			Fixed32 num24 = ((value.M41 * num6) + (value.M42 * num5)) + (value.M43 * num4);
			Fixed32 num23 = ((value.M41 * num3) + (value.M42 * num2)) + (value.M43 * num);
			
			Fixed32 num22 = value.M44;
			
			result.M11 = num37;
			result.M12 = num36;
			result.M13 = num35;
			result.M14 = num34;
			result.M21 = num33;
			result.M22 = num32;
			result.M23 = num31;
			result.M24 = num30;
			result.M31 = num29;
			result.M32 = num28;
			result.M33 = num27;
			result.M34 = num26;
			result.M41 = num25;
			result.M42 = num24;
			result.M43 = num23;
			result.M44 = num22;
		}
		
		#endregion
		#region Operators
		
		public static Matrix44 operator - (Matrix44 matrix1)
		{
			Matrix44 matrix;
			matrix.M11 = -matrix1.M11;
			matrix.M12 = -matrix1.M12;
			matrix.M13 = -matrix1.M13;
			matrix.M14 = -matrix1.M14;
			matrix.M21 = -matrix1.M21;
			matrix.M22 = -matrix1.M22;
			matrix.M23 = -matrix1.M23;
			matrix.M24 = -matrix1.M24;
			matrix.M31 = -matrix1.M31;
			matrix.M32 = -matrix1.M32;
			matrix.M33 = -matrix1.M33;
			matrix.M34 = -matrix1.M34;
			matrix.M41 = -matrix1.M41;
			matrix.M42 = -matrix1.M42;
			matrix.M43 = -matrix1.M43;
			matrix.M44 = -matrix1.M44;
			return matrix;
		}
		
		public static Boolean operator == (Matrix44 matrix1, Matrix44 matrix2)
		{
			return ((((((matrix1.M11 == matrix2.M11) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M44 == matrix2.M44))) && (((matrix1.M12 == matrix2.M12) && (matrix1.M13 == matrix2.M13)) && ((matrix1.M14 == matrix2.M14) && (matrix1.M21 == matrix2.M21)))) && ((((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)) && ((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32))) && (((matrix1.M34 == matrix2.M34) && (matrix1.M41 == matrix2.M41)) && (matrix1.M42 == matrix2.M42)))) && (matrix1.M43 == matrix2.M43));
		}
		
		public static Boolean operator != (Matrix44 matrix1, Matrix44 matrix2)
		{
			if (((((matrix1.M11 == matrix2.M11) && (matrix1.M12 == matrix2.M12)) && ((matrix1.M13 == matrix2.M13) && (matrix1.M14 == matrix2.M14))) && (((matrix1.M21 == matrix2.M21) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)))) && ((((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M34 == matrix2.M34))) && (((matrix1.M41 == matrix2.M41) && (matrix1.M42 == matrix2.M42)) && (matrix1.M43 == matrix2.M43)))) {
				return !(matrix1.M44 == matrix2.M44);
			}
			return true;
		}
		
		public static Matrix44 operator + (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = matrix1.M11 + matrix2.M11;
			matrix.M12 = matrix1.M12 + matrix2.M12;
			matrix.M13 = matrix1.M13 + matrix2.M13;
			matrix.M14 = matrix1.M14 + matrix2.M14;
			matrix.M21 = matrix1.M21 + matrix2.M21;
			matrix.M22 = matrix1.M22 + matrix2.M22;
			matrix.M23 = matrix1.M23 + matrix2.M23;
			matrix.M24 = matrix1.M24 + matrix2.M24;
			matrix.M31 = matrix1.M31 + matrix2.M31;
			matrix.M32 = matrix1.M32 + matrix2.M32;
			matrix.M33 = matrix1.M33 + matrix2.M33;
			matrix.M34 = matrix1.M34 + matrix2.M34;
			matrix.M41 = matrix1.M41 + matrix2.M41;
			matrix.M42 = matrix1.M42 + matrix2.M42;
			matrix.M43 = matrix1.M43 + matrix2.M43;
			matrix.M44 = matrix1.M44 + matrix2.M44;
			return matrix;
		}
		
		public static Matrix44 operator - (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = matrix1.M11 - matrix2.M11;
			matrix.M12 = matrix1.M12 - matrix2.M12;
			matrix.M13 = matrix1.M13 - matrix2.M13;
			matrix.M14 = matrix1.M14 - matrix2.M14;
			matrix.M21 = matrix1.M21 - matrix2.M21;
			matrix.M22 = matrix1.M22 - matrix2.M22;
			matrix.M23 = matrix1.M23 - matrix2.M23;
			matrix.M24 = matrix1.M24 - matrix2.M24;
			matrix.M31 = matrix1.M31 - matrix2.M31;
			matrix.M32 = matrix1.M32 - matrix2.M32;
			matrix.M33 = matrix1.M33 - matrix2.M33;
			matrix.M34 = matrix1.M34 - matrix2.M34;
			matrix.M41 = matrix1.M41 - matrix2.M41;
			matrix.M42 = matrix1.M42 - matrix2.M42;
			matrix.M43 = matrix1.M43 - matrix2.M43;
			matrix.M44 = matrix1.M44 - matrix2.M44;
			return matrix;
		}
		
		public static Matrix44 operator * (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = (((matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21)) + (matrix1.M13 * matrix2.M31)) + (matrix1.M14 * matrix2.M41);
			matrix.M12 = (((matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22)) + (matrix1.M13 * matrix2.M32)) + (matrix1.M14 * matrix2.M42);
			matrix.M13 = (((matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23)) + (matrix1.M13 * matrix2.M33)) + (matrix1.M14 * matrix2.M43);
			matrix.M14 = (((matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24)) + (matrix1.M13 * matrix2.M34)) + (matrix1.M14 * matrix2.M44);
			matrix.M21 = (((matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21)) + (matrix1.M23 * matrix2.M31)) + (matrix1.M24 * matrix2.M41);
			matrix.M22 = (((matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22)) + (matrix1.M23 * matrix2.M32)) + (matrix1.M24 * matrix2.M42);
			matrix.M23 = (((matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23)) + (matrix1.M23 * matrix2.M33)) + (matrix1.M24 * matrix2.M43);
			matrix.M24 = (((matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24)) + (matrix1.M23 * matrix2.M34)) + (matrix1.M24 * matrix2.M44);
			matrix.M31 = (((matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21)) + (matrix1.M33 * matrix2.M31)) + (matrix1.M34 * matrix2.M41);
			matrix.M32 = (((matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22)) + (matrix1.M33 * matrix2.M32)) + (matrix1.M34 * matrix2.M42);
			matrix.M33 = (((matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23)) + (matrix1.M33 * matrix2.M33)) + (matrix1.M34 * matrix2.M43);
			matrix.M34 = (((matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24)) + (matrix1.M33 * matrix2.M34)) + (matrix1.M34 * matrix2.M44);
			matrix.M41 = (((matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21)) + (matrix1.M43 * matrix2.M31)) + (matrix1.M44 * matrix2.M41);
			matrix.M42 = (((matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22)) + (matrix1.M43 * matrix2.M32)) + (matrix1.M44 * matrix2.M42);
			matrix.M43 = (((matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23)) + (matrix1.M43 * matrix2.M33)) + (matrix1.M44 * matrix2.M43);
			matrix.M44 = (((matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24)) + (matrix1.M43 * matrix2.M34)) + (matrix1.M44 * matrix2.M44);
			return matrix;
		}
		
		public static Matrix44 operator * (Matrix44 matrix, Fixed32 scaleFactor)
		{
			Matrix44 matrix2;
			Fixed32 num = scaleFactor;
			matrix2.M11 = matrix.M11 * num;
			matrix2.M12 = matrix.M12 * num;
			matrix2.M13 = matrix.M13 * num;
			matrix2.M14 = matrix.M14 * num;
			matrix2.M21 = matrix.M21 * num;
			matrix2.M22 = matrix.M22 * num;
			matrix2.M23 = matrix.M23 * num;
			matrix2.M24 = matrix.M24 * num;
			matrix2.M31 = matrix.M31 * num;
			matrix2.M32 = matrix.M32 * num;
			matrix2.M33 = matrix.M33 * num;
			matrix2.M34 = matrix.M34 * num;
			matrix2.M41 = matrix.M41 * num;
			matrix2.M42 = matrix.M42 * num;
			matrix2.M43 = matrix.M43 * num;
			matrix2.M44 = matrix.M44 * num;
			return matrix2;
		}
		
		public static Matrix44 operator * (Fixed32 scaleFactor, Matrix44 matrix)
		{
			Matrix44 matrix2;
			Fixed32 num = scaleFactor;
			matrix2.M11 = matrix.M11 * num;
			matrix2.M12 = matrix.M12 * num;
			matrix2.M13 = matrix.M13 * num;
			matrix2.M14 = matrix.M14 * num;
			matrix2.M21 = matrix.M21 * num;
			matrix2.M22 = matrix.M22 * num;
			matrix2.M23 = matrix.M23 * num;
			matrix2.M24 = matrix.M24 * num;
			matrix2.M31 = matrix.M31 * num;
			matrix2.M32 = matrix.M32 * num;
			matrix2.M33 = matrix.M33 * num;
			matrix2.M34 = matrix.M34 * num;
			matrix2.M41 = matrix.M41 * num;
			matrix2.M42 = matrix.M42 * num;
			matrix2.M43 = matrix.M43 * num;
			matrix2.M44 = matrix.M44 * num;
			return matrix2;
		}
		
		public static Matrix44 operator / (Matrix44 matrix1, Matrix44 matrix2)
		{
			Matrix44 matrix;
			matrix.M11 = matrix1.M11 / matrix2.M11;
			matrix.M12 = matrix1.M12 / matrix2.M12;
			matrix.M13 = matrix1.M13 / matrix2.M13;
			matrix.M14 = matrix1.M14 / matrix2.M14;
			matrix.M21 = matrix1.M21 / matrix2.M21;
			matrix.M22 = matrix1.M22 / matrix2.M22;
			matrix.M23 = matrix1.M23 / matrix2.M23;
			matrix.M24 = matrix1.M24 / matrix2.M24;
			matrix.M31 = matrix1.M31 / matrix2.M31;
			matrix.M32 = matrix1.M32 / matrix2.M32;
			matrix.M33 = matrix1.M33 / matrix2.M33;
			matrix.M34 = matrix1.M34 / matrix2.M34;
			matrix.M41 = matrix1.M41 / matrix2.M41;
			matrix.M42 = matrix1.M42 / matrix2.M42;
			matrix.M43 = matrix1.M43 / matrix2.M43;
			matrix.M44 = matrix1.M44 / matrix2.M44;
			return matrix;
		}
		
		public static Matrix44 operator / (Matrix44 matrix1, Fixed32 divider)
		{
			Matrix44 matrix;
			Fixed32 one = 1;
			Fixed32 num = one / divider;
			matrix.M11 = matrix1.M11 * num;
			matrix.M12 = matrix1.M12 * num;
			matrix.M13 = matrix1.M13 * num;
			matrix.M14 = matrix1.M14 * num;
			matrix.M21 = matrix1.M21 * num;
			matrix.M22 = matrix1.M22 * num;
			matrix.M23 = matrix1.M23 * num;
			matrix.M24 = matrix1.M24 * num;
			matrix.M31 = matrix1.M31 * num;
			matrix.M32 = matrix1.M32 * num;
			matrix.M33 = matrix1.M33 * num;
			matrix.M34 = matrix1.M34 * num;
			matrix.M41 = matrix1.M41 * num;
			matrix.M42 = matrix1.M42 * num;
			matrix.M43 = matrix1.M43 * num;
			matrix.M44 = matrix1.M44 * num;
			return matrix;
		}
		
		public static void Negate (ref Matrix44 matrix, out Matrix44 result)
		{
			result.M11 = -matrix.M11;
			result.M12 = -matrix.M12;
			result.M13 = -matrix.M13;
			result.M14 = -matrix.M14;
			result.M21 = -matrix.M21;
			result.M22 = -matrix.M22;
			result.M23 = -matrix.M23;
			result.M24 = -matrix.M24;
			result.M31 = -matrix.M31;
			result.M32 = -matrix.M32;
			result.M33 = -matrix.M33;
			result.M34 = -matrix.M34;
			result.M41 = -matrix.M41;
			result.M42 = -matrix.M42;
			result.M43 = -matrix.M43;
			result.M44 = -matrix.M44;
		}
		
		public static void Add (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			result.M11 = matrix1.M11 + matrix2.M11;
			result.M12 = matrix1.M12 + matrix2.M12;
			result.M13 = matrix1.M13 + matrix2.M13;
			result.M14 = matrix1.M14 + matrix2.M14;
			result.M21 = matrix1.M21 + matrix2.M21;
			result.M22 = matrix1.M22 + matrix2.M22;
			result.M23 = matrix1.M23 + matrix2.M23;
			result.M24 = matrix1.M24 + matrix2.M24;
			result.M31 = matrix1.M31 + matrix2.M31;
			result.M32 = matrix1.M32 + matrix2.M32;
			result.M33 = matrix1.M33 + matrix2.M33;
			result.M34 = matrix1.M34 + matrix2.M34;
			result.M41 = matrix1.M41 + matrix2.M41;
			result.M42 = matrix1.M42 + matrix2.M42;
			result.M43 = matrix1.M43 + matrix2.M43;
			result.M44 = matrix1.M44 + matrix2.M44;
		}
		
		public static void Subtract (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			result.M11 = matrix1.M11 - matrix2.M11;
			result.M12 = matrix1.M12 - matrix2.M12;
			result.M13 = matrix1.M13 - matrix2.M13;
			result.M14 = matrix1.M14 - matrix2.M14;
			result.M21 = matrix1.M21 - matrix2.M21;
			result.M22 = matrix1.M22 - matrix2.M22;
			result.M23 = matrix1.M23 - matrix2.M23;
			result.M24 = matrix1.M24 - matrix2.M24;
			result.M31 = matrix1.M31 - matrix2.M31;
			result.M32 = matrix1.M32 - matrix2.M32;
			result.M33 = matrix1.M33 - matrix2.M33;
			result.M34 = matrix1.M34 - matrix2.M34;
			result.M41 = matrix1.M41 - matrix2.M41;
			result.M42 = matrix1.M42 - matrix2.M42;
			result.M43 = matrix1.M43 - matrix2.M43;
			result.M44 = matrix1.M44 - matrix2.M44;
		}
		
		public static void Multiply (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			Fixed32 num16 = (((matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21)) + (matrix1.M13 * matrix2.M31)) + (matrix1.M14 * matrix2.M41);
			Fixed32 num15 = (((matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22)) + (matrix1.M13 * matrix2.M32)) + (matrix1.M14 * matrix2.M42);
			Fixed32 num14 = (((matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23)) + (matrix1.M13 * matrix2.M33)) + (matrix1.M14 * matrix2.M43);
			Fixed32 num13 = (((matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24)) + (matrix1.M13 * matrix2.M34)) + (matrix1.M14 * matrix2.M44);
			Fixed32 num12 = (((matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21)) + (matrix1.M23 * matrix2.M31)) + (matrix1.M24 * matrix2.M41);
			Fixed32 num11 = (((matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22)) + (matrix1.M23 * matrix2.M32)) + (matrix1.M24 * matrix2.M42);
			Fixed32 num10 = (((matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23)) + (matrix1.M23 * matrix2.M33)) + (matrix1.M24 * matrix2.M43);
			Fixed32 num9 = (((matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24)) + (matrix1.M23 * matrix2.M34)) + (matrix1.M24 * matrix2.M44);
			Fixed32 num8 = (((matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21)) + (matrix1.M33 * matrix2.M31)) + (matrix1.M34 * matrix2.M41);
			Fixed32 num7 = (((matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22)) + (matrix1.M33 * matrix2.M32)) + (matrix1.M34 * matrix2.M42);
			Fixed32 num6 = (((matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23)) + (matrix1.M33 * matrix2.M33)) + (matrix1.M34 * matrix2.M43);
			Fixed32 num5 = (((matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24)) + (matrix1.M33 * matrix2.M34)) + (matrix1.M34 * matrix2.M44);
			Fixed32 num4 = (((matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21)) + (matrix1.M43 * matrix2.M31)) + (matrix1.M44 * matrix2.M41);
			Fixed32 num3 = (((matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22)) + (matrix1.M43 * matrix2.M32)) + (matrix1.M44 * matrix2.M42);
			Fixed32 num2 = (((matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23)) + (matrix1.M43 * matrix2.M33)) + (matrix1.M44 * matrix2.M43);
			Fixed32 num = (((matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24)) + (matrix1.M43 * matrix2.M34)) + (matrix1.M44 * matrix2.M44);
			result.M11 = num16;
			result.M12 = num15;
			result.M13 = num14;
			result.M14 = num13;
			result.M21 = num12;
			result.M22 = num11;
			result.M23 = num10;
			result.M24 = num9;
			result.M31 = num8;
			result.M32 = num7;
			result.M33 = num6;
			result.M34 = num5;
			result.M41 = num4;
			result.M42 = num3;
			result.M43 = num2;
			result.M44 = num;
		}

		public static void Multiply (ref Matrix44 matrix1, Fixed32 scaleFactor, out Matrix44 result)
		{
			Fixed32 num = scaleFactor;
			result.M11 = matrix1.M11 * num;
			result.M12 = matrix1.M12 * num;
			result.M13 = matrix1.M13 * num;
			result.M14 = matrix1.M14 * num;
			result.M21 = matrix1.M21 * num;
			result.M22 = matrix1.M22 * num;
			result.M23 = matrix1.M23 * num;
			result.M24 = matrix1.M24 * num;
			result.M31 = matrix1.M31 * num;
			result.M32 = matrix1.M32 * num;
			result.M33 = matrix1.M33 * num;
			result.M34 = matrix1.M34 * num;
			result.M41 = matrix1.M41 * num;
			result.M42 = matrix1.M42 * num;
			result.M43 = matrix1.M43 * num;
			result.M44 = matrix1.M44 * num;
		}

		public static void Divide (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
		{
			result.M11 = matrix1.M11 / matrix2.M11;
			result.M12 = matrix1.M12 / matrix2.M12;
			result.M13 = matrix1.M13 / matrix2.M13;
			result.M14 = matrix1.M14 / matrix2.M14;
			result.M21 = matrix1.M21 / matrix2.M21;
			result.M22 = matrix1.M22 / matrix2.M22;
			result.M23 = matrix1.M23 / matrix2.M23;
			result.M24 = matrix1.M24 / matrix2.M24;
			result.M31 = matrix1.M31 / matrix2.M31;
			result.M32 = matrix1.M32 / matrix2.M32;
			result.M33 = matrix1.M33 / matrix2.M33;
			result.M34 = matrix1.M34 / matrix2.M34;
			result.M41 = matrix1.M41 / matrix2.M41;
			result.M42 = matrix1.M42 / matrix2.M42;
			result.M43 = matrix1.M43 / matrix2.M43;
			result.M44 = matrix1.M44 / matrix2.M44;
		}
		
		public static void Divide (ref Matrix44 matrix1, Fixed32 divider, out Matrix44 result)
		{
			Fixed32 one = 1;

			Fixed32 num = one / divider;
			result.M11 = matrix1.M11 * num;
			result.M12 = matrix1.M12 * num;
			result.M13 = matrix1.M13 * num;
			result.M14 = matrix1.M14 * num;
			result.M21 = matrix1.M21 * num;
			result.M22 = matrix1.M22 * num;
			result.M23 = matrix1.M23 * num;
			result.M24 = matrix1.M24 * num;
			result.M31 = matrix1.M31 * num;
			result.M32 = matrix1.M32 * num;
			result.M33 = matrix1.M33 * num;
			result.M34 = matrix1.M34 * num;
			result.M41 = matrix1.M41 * num;
			result.M42 = matrix1.M42 * num;
			result.M43 = matrix1.M43 * num;
			result.M44 = matrix1.M44 * num;
		}

		#endregion
		#region Utilities

		// beware, doing this might not produce what you expect.  you likely
		// want to lerp between quaternions.
		public static void Lerp (ref Matrix44 matrix1, ref Matrix44 matrix2, Fixed32 amount, out Matrix44 result)
		{
			result.M11 = matrix1.M11 + ((matrix2.M11 - matrix1.M11) * amount);
			result.M12 = matrix1.M12 + ((matrix2.M12 - matrix1.M12) * amount);
			result.M13 = matrix1.M13 + ((matrix2.M13 - matrix1.M13) * amount);
			result.M14 = matrix1.M14 + ((matrix2.M14 - matrix1.M14) * amount);
			result.M21 = matrix1.M21 + ((matrix2.M21 - matrix1.M21) * amount);
			result.M22 = matrix1.M22 + ((matrix2.M22 - matrix1.M22) * amount);
			result.M23 = matrix1.M23 + ((matrix2.M23 - matrix1.M23) * amount);
			result.M24 = matrix1.M24 + ((matrix2.M24 - matrix1.M24) * amount);
			result.M31 = matrix1.M31 + ((matrix2.M31 - matrix1.M31) * amount);
			result.M32 = matrix1.M32 + ((matrix2.M32 - matrix1.M32) * amount);
			result.M33 = matrix1.M33 + ((matrix2.M33 - matrix1.M33) * amount);
			result.M34 = matrix1.M34 + ((matrix2.M34 - matrix1.M34) * amount);
			result.M41 = matrix1.M41 + ((matrix2.M41 - matrix1.M41) * amount);
			result.M42 = matrix1.M42 + ((matrix2.M42 - matrix1.M42) * amount);
			result.M43 = matrix1.M43 + ((matrix2.M43 - matrix1.M43) * amount);
			result.M44 = matrix1.M44 + ((matrix2.M44 - matrix1.M44) * amount);
		}
		
		#endregion
		
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Plane 
		: IEquatable<Plane>
	{
		public Vector3 Normal;
		public Fixed32 D;

		public Plane (Fixed32 a, Fixed32 b, Fixed32 c, Fixed32 d)
		{
			this.Normal.X = a;
			this.Normal.Y = b;
			this.Normal.Z = c;
			this.D = d;
		}

		public Plane (Vector3 normal, Fixed32 d)
		{
			this.Normal = normal;
			this.D = d;
		}

		public Plane (Vector4 value)
		{
			this.Normal.X = value.X;
			this.Normal.Y = value.Y;
			this.Normal.Z = value.Z;
			this.D = value.W;
		}

		public Plane (Vector3 point1, Vector3 point2, Vector3 point3)
		{
			Fixed32 one = 1;

			Fixed32 num10 = point2.X - point1.X;
			Fixed32 num9 = point2.Y - point1.Y;
			Fixed32 num8 = point2.Z - point1.Z;
			Fixed32 num7 = point3.X - point1.X;
			Fixed32 num6 = point3.Y - point1.Y;
			Fixed32 num5 = point3.Z - point1.Z;
			Fixed32 num4 = (num9 * num5) - (num8 * num6);
			Fixed32 num3 = (num8 * num7) - (num10 * num5);
			Fixed32 num2 = (num10 * num6) - (num9 * num7);
			Fixed32 num11 = ((num4 * num4) + (num3 * num3)) + (num2 * num2);
			Fixed32 num = one / RealMaths.Sqrt (num11);
			this.Normal.X = num4 * num;
			this.Normal.Y = num3 * num;
			this.Normal.Z = num2 * num;
			this.D = -(((this.Normal.X * point1.X) + (this.Normal.Y * point1.Y)) + (this.Normal.Z * point1.Z));
		}

		public Boolean Equals (Plane other)
		{
			return ((((this.Normal.X == other.Normal.X) && (this.Normal.Y == other.Normal.Y)) && (this.Normal.Z == other.Normal.Z)) && (this.D == other.D));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Plane) {
				flag = this.Equals ((Plane)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Normal.GetHashCode () + this.D.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Normal:{0} D:{1}}}", new Object[] { this.Normal.ToString (), this.D.ToString () });
		}

		public void Normalise ()
		{
			Fixed32 one = 1;
			Fixed32 somethingWicked; RealMaths.FromString("0.0000001192093", out somethingWicked);

			Fixed32 num2 = ((this.Normal.X * this.Normal.X) + (this.Normal.Y * this.Normal.Y)) + (this.Normal.Z * this.Normal.Z);
			if (RealMaths.Abs (num2 - one) >= somethingWicked) {
				Fixed32 num = one / RealMaths.Sqrt (num2);
				this.Normal.X *= num;
				this.Normal.Y *= num;
				this.Normal.Z *= num;
				this.D *= num;
			}
		}

		public static void Normalise (ref Plane value, out Plane result)
		{
			Fixed32 one = 1;
			Fixed32 somethingWicked; RealMaths.FromString("0.0000001192093", out somethingWicked);

			Fixed32 num2 = ((value.Normal.X * value.Normal.X) + (value.Normal.Y * value.Normal.Y)) + (value.Normal.Z * value.Normal.Z);
			if (RealMaths.Abs (num2 - one) < somethingWicked) {
				result.Normal = value.Normal;
				result.D = value.D;
			} else {
				Fixed32 num = one / RealMaths.Sqrt (num2);
				result.Normal.X = value.Normal.X * num;
				result.Normal.Y = value.Normal.Y * num;
				result.Normal.Z = value.Normal.Z * num;
				result.D = value.D * num;
			}
		}

		public static void Transform (ref Plane plane, ref Matrix44 matrix, out Plane result)
		{
			Matrix44 matrix2;
			Matrix44.Invert (ref matrix, out matrix2);
			Fixed32 x = plane.Normal.X;
			Fixed32 y = plane.Normal.Y;
			Fixed32 z = plane.Normal.Z;
			Fixed32 d = plane.D;
			result.Normal.X = (((x * matrix2.M11) + (y * matrix2.M12)) + (z * matrix2.M13)) + (d * matrix2.M14);
			result.Normal.Y = (((x * matrix2.M21) + (y * matrix2.M22)) + (z * matrix2.M23)) + (d * matrix2.M24);
			result.Normal.Z = (((x * matrix2.M31) + (y * matrix2.M32)) + (z * matrix2.M33)) + (d * matrix2.M34);
			result.D = (((x * matrix2.M41) + (y * matrix2.M42)) + (z * matrix2.M43)) + (d * matrix2.M44);
		}


		public static void Transform (ref Plane plane, ref Quaternion rotation, out Plane result)
		{
			Fixed32 one = 1;

			Fixed32 num15 = rotation.X + rotation.X;
			Fixed32 num5 = rotation.Y + rotation.Y;
			Fixed32 num = rotation.Z + rotation.Z;
			Fixed32 num14 = rotation.W * num15;
			Fixed32 num13 = rotation.W * num5;
			Fixed32 num12 = rotation.W * num;
			Fixed32 num11 = rotation.X * num15;
			Fixed32 num10 = rotation.X * num5;
			Fixed32 num9 = rotation.X * num;
			Fixed32 num8 = rotation.Y * num5;
			Fixed32 num7 = rotation.Y * num;
			Fixed32 num6 = rotation.Z * num;
			Fixed32 num24 = (one - num8) - num6;
			Fixed32 num23 = num10 - num12;
			Fixed32 num22 = num9 + num13;
			Fixed32 num21 = num10 + num12;
			Fixed32 num20 = (one - num11) - num6;
			Fixed32 num19 = num7 - num14;
			Fixed32 num18 = num9 - num13;
			Fixed32 num17 = num7 + num14;
			Fixed32 num16 = (one - num11) - num8;
			Fixed32 x = plane.Normal.X;
			Fixed32 y = plane.Normal.Y;
			Fixed32 z = plane.Normal.Z;
			result.Normal.X = ((x * num24) + (y * num23)) + (z * num22);
			result.Normal.Y = ((x * num21) + (y * num20)) + (z * num19);
			result.Normal.Z = ((x * num18) + (y * num17)) + (z * num16);
			result.D = plane.D;
		}
		


		public Fixed32 Dot(ref Vector4 value)
		{
			return (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + (this.D * value.W);
		}

		public Fixed32 DotCoordinate (ref Vector3 value)
		{
			return (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + this.D;
		}

		public Fixed32 DotNormal (ref Vector3 value)
		{
			return ((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z);
		}

		public PlaneIntersectionType Intersects (ref BoundingBox box)
		{
			Fixed32 zero = 0;

			Vector3 vector;
			Vector3 vector2;
			vector2.X = (this.Normal.X >= zero) ? box.Min.X : box.Max.X;
			vector2.Y = (this.Normal.Y >= zero) ? box.Min.Y : box.Max.Y;
			vector2.Z = (this.Normal.Z >= zero) ? box.Min.Z : box.Max.Z;
			vector.X = (this.Normal.X >= zero) ? box.Max.X : box.Min.X;
			vector.Y = (this.Normal.Y >= zero) ? box.Max.Y : box.Min.Y;
			vector.Z = (this.Normal.Z >= zero) ? box.Max.Z : box.Min.Z;
			Fixed32 num = ((this.Normal.X * vector2.X) + (this.Normal.Y * vector2.Y)) + (this.Normal.Z * vector2.Z);
			if ((num + this.D) > zero) {
				return PlaneIntersectionType.Front;
			} else {
				num = ((this.Normal.X * vector.X) + (this.Normal.Y * vector.Y)) + (this.Normal.Z * vector.Z);
				if ((num + this.D) < zero) {
					return PlaneIntersectionType.Back;
				} else {
					return PlaneIntersectionType.Intersecting;
				}
			}
		}

		public PlaneIntersectionType Intersects (ref BoundingFrustum frustum)
		{
			if (null == frustum) {
				throw new ArgumentNullException ("frustum - NullNotAllowed");
			}
			return frustum.Intersects (ref this);
		}

		public PlaneIntersectionType Intersects (ref BoundingSphere sphere)
		{
			Fixed32 num2 = ((sphere.Center.X * this.Normal.X) + (sphere.Center.Y * this.Normal.Y)) + (sphere.Center.Z * this.Normal.Z);
			Fixed32 num = num2 + this.D;
			if (num > sphere.Radius) {
				return PlaneIntersectionType.Front;
			} else if (num < -sphere.Radius) {
				return PlaneIntersectionType.Back;
			} else {
				return PlaneIntersectionType.Intersecting;
			}
		}

		public static Boolean operator == (Plane lhs, Plane rhs)
		{
			return lhs.Equals (rhs);
		}

		public static Boolean operator != (Plane lhs, Plane rhs)
		{
			if (((lhs.Normal.X == rhs.Normal.X) && (lhs.Normal.Y == rhs.Normal.Y)) && (lhs.Normal.Z == rhs.Normal.Z)) {
				return !(lhs.D == rhs.D);
			}
			return true;
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Quad
		: IEquatable<Quad>
	{
		public Vector3 A
		{
			get
			{
				return tri1.A;
			}
			set
			{
				tri1.A = value;
			}
		}

		public Vector3 B
		{
			get
			{
				return tri1.B;
			}
			set
			{
				tri1.B = value;
				tri2.B = value;
			}
		}

		public Vector3 C
		{
			get
			{
				return tri2.C;
			}
			set
			{
				tri1.C = value;
				tri2.C = value;
			}
		}

		public Vector3 D
		{
			get
			{
				return tri2.A;
			}
			set
			{
				tri1.A = value;
				tri2.A = value;
			}
		}

		Triangle tri1;
		Triangle tri2;

		public Quad (Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			this.tri1 = new Triangle(a, b, c);
			this.tri2 = new Triangle(d, b, c);
		}

		// Determines whether or not this Quad is equal in value to another Quad
		public Boolean Equals (Quad other)
		{
			if (this.A.X != other.A.X) return false;
			if (this.A.Y != other.A.Y) return false;
			if (this.A.Z != other.A.Z) return false;

			if (this.B.X != other.B.X) return false;
			if (this.B.Y != other.B.Y) return false;
			if (this.B.Z != other.B.Z) return false;

			if (this.C.X != other.C.X) return false;
			if (this.C.Y != other.C.Y) return false;
			if (this.C.Z != other.C.Z) return false;
			
			if (this.D.X != other.D.X) return false;
			if (this.D.Y != other.D.Y) return false;
			if (this.D.Z != other.D.Z) return false;

			// They match!
			return true;
		}

		// Determines whether or not this Quad is equal in value to another System.Object
		public override Boolean Equals (Object obj)
		{
			if (obj == null) return false;

			if (obj is Quad)
			{
				// Ok, it is a Quad, so just use the method above to compare.
				return this.Equals ((Quad) obj);
			}

			return false;
		}

		public override Int32 GetHashCode ()
		{
			return (this.A.GetHashCode () + this.B.GetHashCode () + this.C.GetHashCode () + this.D.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{A:{0} B:{1} C:{2} D:{3}}}", this.A, this.B, this.C, this.D);
		}

		public static Boolean operator == (Quad a, Quad b)
		{
			return a.Equals(b);
		}

		public static Boolean operator != (Quad a, Quad b)
		{
			return !a.Equals(b);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Quaternion 
		: IEquatable<Quaternion>
	{
		public Fixed32 X;
		public Fixed32 Y;
		public Fixed32 Z;
		public Fixed32 W;


		public Quaternion (Fixed32 x, Fixed32 y, Fixed32 z, Fixed32 w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		public Quaternion (Vector3 vectorPart, Fixed32 scalarPart)
		{
			this.X = vectorPart.X;
			this.Y = vectorPart.Y;
			this.Z = vectorPart.Z;
			this.W = scalarPart;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString (), this.W.ToString () });
		}

		public Boolean Equals (Quaternion other)
		{
			return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
		}

		public override Boolean Equals (Object obj)
		{

			Boolean flag = false;
			if (obj is Quaternion)
			{
				flag = this.Equals ((Quaternion)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ()) + this.W.GetHashCode ());
		}

		public Fixed32 LengthSquared ()
		{
			return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
		}

		public Fixed32 Length ()
		{
			Fixed32 num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			return RealMaths.Sqrt (num);
		}

		public void Normalise ()
		{
			Fixed32 one = 1;
			Fixed32 num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			Fixed32 num = one / RealMaths.Sqrt (num2);
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
			this.W *= num;
		}

		public Boolean IsUnit()
		{
			Fixed32 one = 1;

			return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
		}

		public void Conjugate ()
		{
			this.X = -this.X;
			this.Y = -this.Y;
			this.Z = -this.Z;
		}

		#region Constants

		static Quaternion identity;
		
		public static Quaternion Identity
		{
			get
			{
				return identity;
			}
		}

		static Quaternion ()
		{
			Fixed32 temp_one; RealMaths.One(out temp_one);
			Fixed32 temp_zero; RealMaths.Zero(out temp_zero);
			identity = new Quaternion (temp_zero, temp_zero, temp_zero, temp_one);
		}
		
		#endregion
		#region Create

		public static void CreateFromAxisAngle (ref Vector3 axis, Fixed32 angle, out Quaternion result)
		{
			Fixed32 half; RealMaths.Half(out half);
			Fixed32 theta = angle * half;

			Fixed32 sin = RealMaths.Sin (theta);
			Fixed32 cos = RealMaths.Cos (theta);

			result.X = axis.X * sin;
			result.Y = axis.Y * sin;
			result.Z = axis.Z * sin;

			result.W = cos;
		}
		
		public static void CreateFromYawPitchRoll (Fixed32 yaw, Fixed32 pitch, Fixed32 roll, out Quaternion result)
		{
			Fixed32 half; RealMaths.Half(out half);
			Fixed32 num9 = roll * half;

			Fixed32 num6 = RealMaths.Sin (num9);
			Fixed32 num5 = RealMaths.Cos (num9);

			Fixed32 num8 = pitch * half;

			Fixed32 num4 = RealMaths.Sin (num8);
			Fixed32 num3 = RealMaths.Cos (num8);

			Fixed32 num7 = yaw * half;

			Fixed32 num2 = RealMaths.Sin (num7);
			Fixed32 num = RealMaths.Cos (num7);

			result.X = ((num * num4) * num5) + ((num2 * num3) * num6);
			result.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
			result.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
			result.W = ((num * num3) * num5) + ((num2 * num4) * num6);
		}
		
		public static void CreateFromRotationMatrix (ref Matrix44 matrix, out Quaternion result)
		{
			Fixed32 zero = 0;
			Fixed32 half; RealMaths.Half(out half);
			Fixed32 one = 1;

			Fixed32 num8 = (matrix.M11 + matrix.M22) + matrix.M33;

			if (num8 > zero)
			{
				Fixed32 num = RealMaths.Sqrt (num8 + one);
				result.W = num * half;
				num = half / num;
				result.X = (matrix.M23 - matrix.M32) * num;
				result.Y = (matrix.M31 - matrix.M13) * num;
				result.Z = (matrix.M12 - matrix.M21) * num;
			}
			else if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
			{
				Fixed32 num7 = RealMaths.Sqrt (((one + matrix.M11) - matrix.M22) - matrix.M33);
				Fixed32 num4 = half / num7;
				result.X = half * num7;
				result.Y = (matrix.M12 + matrix.M21) * num4;
				result.Z = (matrix.M13 + matrix.M31) * num4;
				result.W = (matrix.M23 - matrix.M32) * num4;
			}
			else if (matrix.M22 > matrix.M33)
			{
				Fixed32 num6 =RealMaths.Sqrt (((one + matrix.M22) - matrix.M11) - matrix.M33);
				Fixed32 num3 = half / num6;
				result.X = (matrix.M21 + matrix.M12) * num3;
				result.Y = half * num6;
				result.Z = (matrix.M32 + matrix.M23) * num3;
				result.W = (matrix.M31 - matrix.M13) * num3;
			}
			else
			{
				Fixed32 num5 = RealMaths.Sqrt (((one + matrix.M33) - matrix.M11) - matrix.M22);
				Fixed32 num2 = half / num5;
				result.X = (matrix.M31 + matrix.M13) * num2;
				result.Y = (matrix.M32 + matrix.M23) * num2;
				result.Z = half * num5;
				result.W = (matrix.M12 - matrix.M21) * num2;
			}
		}
		
		#endregion
		#region Maths

		public static void Conjugate (ref Quaternion value, out Quaternion result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = value.W;
		}
		
		public static void Inverse (ref Quaternion quaternion, out Quaternion result)
		{
			Fixed32 one = 1;
			Fixed32 num2 = ( ( (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) ) + 
			                (quaternion.Z * quaternion.Z) ) + (quaternion.W * quaternion.W);

			Fixed32 num = one / num2;

			result.X = -quaternion.X * num;
			result.Y = -quaternion.Y * num;
			result.Z = -quaternion.Z * num;
			result.W = quaternion.W * num;
		}
		
		
		public static void Dot (ref Quaternion quaternion1, ref Quaternion quaternion2, out Fixed32 result)
		{
			result = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + 
			          (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
		}


		public static void Concatenate (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
		{
			Fixed32 x = value2.X;
			Fixed32 y = value2.Y;
			Fixed32 z = value2.Z;
			Fixed32 w = value2.W;
			Fixed32 num4 = value1.X;
			Fixed32 num3 = value1.Y;
			Fixed32 num2 = value1.Z;
			Fixed32 num = value1.W;
			Fixed32 num12 = (y * num2) - (z * num3);
			Fixed32 num11 = (z * num4) - (x * num2);
			Fixed32 num10 = (x * num3) - (y * num4);
			Fixed32 num9 = ((x * num4) + (y * num3)) + (z * num2);
			result.X = ((x * num) + (num4 * w)) + num12;
			result.Y = ((y * num) + (num3 * w)) + num11;
			result.Z = ((z * num) + (num2 * w)) + num10;
			result.W = (w * num) - num9;
		}
		
		public static void Normalise (ref Quaternion quaternion, out Quaternion result)
		{
			Fixed32 one = 1;

			Fixed32 num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
			Fixed32 num = one / RealMaths.Sqrt (num2);
			result.X = quaternion.X * num;
			result.Y = quaternion.Y * num;
			result.Z = quaternion.Z * num;
			result.W = quaternion.W * num;
		}
		
		#endregion
		#region Operators

		public static Quaternion operator - (Quaternion quaternion)
		{
			Quaternion quaternion2;
			quaternion2.X = -quaternion.X;
			quaternion2.Y = -quaternion.Y;
			quaternion2.Z = -quaternion.Z;
			quaternion2.W = -quaternion.W;
			return quaternion2;
		}
		
		public static Boolean operator == (Quaternion quaternion1, Quaternion quaternion2)
		{
			return ((((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) && (quaternion1.W == quaternion2.W));
		}
		
		public static Boolean operator != (Quaternion quaternion1, Quaternion quaternion2)
		{
			if (((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) {
				return !(quaternion1.W == quaternion2.W);
			}
			return true;
		}
		
		public static Quaternion operator + (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X + quaternion2.X;
			quaternion.Y = quaternion1.Y + quaternion2.Y;
			quaternion.Z = quaternion1.Z + quaternion2.Z;
			quaternion.W = quaternion1.W + quaternion2.W;
			return quaternion;
		}
		
		public static Quaternion operator - (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X - quaternion2.X;
			quaternion.Y = quaternion1.Y - quaternion2.Y;
			quaternion.Z = quaternion1.Z - quaternion2.Z;
			quaternion.W = quaternion1.W - quaternion2.W;
			return quaternion;
		}
		
		public static Quaternion operator * (Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			Fixed32 x = quaternion1.X;
			Fixed32 y = quaternion1.Y;
			Fixed32 z = quaternion1.Z;
			Fixed32 w = quaternion1.W;
			Fixed32 num4 = quaternion2.X;
			Fixed32 num3 = quaternion2.Y;
			Fixed32 num2 = quaternion2.Z;
			Fixed32 num = quaternion2.W;
			Fixed32 num12 = (y * num2) - (z * num3);
			Fixed32 num11 = (z * num4) - (x * num2);
			Fixed32 num10 = (x * num3) - (y * num4);
			Fixed32 num9 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.X = ((x * num) + (num4 * w)) + num12;
			quaternion.Y = ((y * num) + (num3 * w)) + num11;
			quaternion.Z = ((z * num) + (num2 * w)) + num10;
			quaternion.W = (w * num) - num9;
			return quaternion;
		}
		
		public static Quaternion operator * (Quaternion quaternion1, Fixed32 scaleFactor)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X * scaleFactor;
			quaternion.Y = quaternion1.Y * scaleFactor;
			quaternion.Z = quaternion1.Z * scaleFactor;
			quaternion.W = quaternion1.W * scaleFactor;
			return quaternion;
		}
		
		public static Quaternion operator / (Quaternion quaternion1, Quaternion quaternion2)
		{
			Fixed32 one = 1;

			Quaternion quaternion;
			Fixed32 x = quaternion1.X;
			Fixed32 y = quaternion1.Y;
			Fixed32 z = quaternion1.Z;
			Fixed32 w = quaternion1.W;
			Fixed32 num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
			Fixed32 num5 = one / num14;
			Fixed32 num4 = -quaternion2.X * num5;
			Fixed32 num3 = -quaternion2.Y * num5;
			Fixed32 num2 = -quaternion2.Z * num5;
			Fixed32 num = quaternion2.W * num5;
			Fixed32 num13 = (y * num2) - (z * num3);
			Fixed32 num12 = (z * num4) - (x * num2);
			Fixed32 num11 = (x * num3) - (y * num4);
			Fixed32 num10 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.X = ((x * num) + (num4 * w)) + num13;
			quaternion.Y = ((y * num) + (num3 * w)) + num12;
			quaternion.Z = ((z * num) + (num2 * w)) + num11;
			quaternion.W = (w * num) - num10;
			return quaternion;
		}



		
		public static void Negate (ref Quaternion quaternion, out Quaternion result)
		{
			result.X = -quaternion.X;
			result.Y = -quaternion.Y;
			result.Z = -quaternion.Z;
			result.W = -quaternion.W;
		}

		public static void Add (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			result.X = quaternion1.X + quaternion2.X;
			result.Y = quaternion1.Y + quaternion2.Y;
			result.Z = quaternion1.Z + quaternion2.Z;
			result.W = quaternion1.W + quaternion2.W;
		}
		
		public static void Subtract (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			result.X = quaternion1.X - quaternion2.X;
			result.Y = quaternion1.Y - quaternion2.Y;
			result.Z = quaternion1.Z - quaternion2.Z;
			result.W = quaternion1.W - quaternion2.W;
		}

		public static void Multiply (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			Fixed32 x = quaternion1.X;
			Fixed32 y = quaternion1.Y;
			Fixed32 z = quaternion1.Z;
			Fixed32 w = quaternion1.W;
			Fixed32 num4 = quaternion2.X;
			Fixed32 num3 = quaternion2.Y;
			Fixed32 num2 = quaternion2.Z;
			Fixed32 num = quaternion2.W;
			Fixed32 num12 = (y * num2) - (z * num3);
			Fixed32 num11 = (z * num4) - (x * num2);
			Fixed32 num10 = (x * num3) - (y * num4);
			Fixed32 num9 = ((x * num4) + (y * num3)) + (z * num2);
			result.X = ((x * num) + (num4 * w)) + num12;
			result.Y = ((y * num) + (num3 * w)) + num11;
			result.Z = ((z * num) + (num2 * w)) + num10;
			result.W = (w * num) - num9;
		}

		public static void Multiply (ref Quaternion quaternion1, Fixed32 scaleFactor, out Quaternion result)
		{
			result.X = quaternion1.X * scaleFactor;
			result.Y = quaternion1.Y * scaleFactor;
			result.Z = quaternion1.Z * scaleFactor;
			result.W = quaternion1.W * scaleFactor;
		}
		
		public static void Divide (ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			Fixed32 one = 1;

			Fixed32 x = quaternion1.X;
			Fixed32 y = quaternion1.Y;
			Fixed32 z = quaternion1.Z;
			Fixed32 w = quaternion1.W;
			Fixed32 num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
			Fixed32 num5 = one / num14;
			Fixed32 num4 = -quaternion2.X * num5;
			Fixed32 num3 = -quaternion2.Y * num5;
			Fixed32 num2 = -quaternion2.Z * num5;
			Fixed32 num = quaternion2.W * num5;
			Fixed32 num13 = (y * num2) - (z * num3);
			Fixed32 num12 = (z * num4) - (x * num2);
			Fixed32 num11 = (x * num3) - (y * num4);
			Fixed32 num10 = ((x * num4) + (y * num3)) + (z * num2);
			result.X = ((x * num) + (num4 * w)) + num13;
			result.Y = ((y * num) + (num3 * w)) + num12;
			result.Z = ((z * num) + (num2 * w)) + num11;
			result.W = (w * num) - num10;
		}
		
		#endregion
		#region Utilities

		public static void Slerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Fixed32 amount, out Quaternion result)
		{
			Fixed32 zero = 0;
			Fixed32 one = 1;
			Fixed32 nineninenine; RealMaths.FromString("0.999999", out nineninenine);

			Fixed32 num2;
			Fixed32 num3;
			Fixed32 num = amount;
			Fixed32 num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
			Boolean flag = false;
			if (num4 < zero) {
				flag = true;
				num4 = -num4;
			}


			if (num4 >nineninenine) {
				num3 = one - num;
				num2 = flag ? -num : num;
			} else {
				Fixed32 num5 = RealMaths.ArcCos (num4);
				Fixed32 num6 = one / RealMaths.Sin (num5);

				num3 = RealMaths.Sin ((one - num) * num5) * num6;

				num2 = flag ? -RealMaths.Sin (num * num5) * num6 : RealMaths.Sin (num * num5) * num6;
			}
			result.X = (num3 * quaternion1.X) + (num2 * quaternion2.X);
			result.Y = (num3 * quaternion1.Y) + (num2 * quaternion2.Y);
			result.Z = (num3 * quaternion1.Z) + (num2 * quaternion2.Z);
			result.W = (num3 * quaternion1.W) + (num2 * quaternion2.W);
		}

		public static void Lerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Fixed32 amount, out Quaternion result)
		{
			Fixed32 zero = 0;
			Fixed32 one = 1;

			Fixed32 num = amount;
			Fixed32 num2 = one - num;
			Fixed32 num5 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
			if (num5 >= zero) {
				result.X = (num2 * quaternion1.X) + (num * quaternion2.X);
				result.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
				result.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
				result.W = (num2 * quaternion1.W) + (num * quaternion2.W);
			} else {
				result.X = (num2 * quaternion1.X) - (num * quaternion2.X);
				result.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
				result.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
				result.W = (num2 * quaternion1.W) - (num * quaternion2.W);
			}
			Fixed32 num4 = (((result.X * result.X) + (result.Y * result.Y)) + (result.Z * result.Z)) + (result.W * result.W);
			Fixed32 num3 = one / RealMaths.Sqrt (num4);
			result.X *= num3;
			result.Y *= num3;
			result.Z *= num3;
			result.W *= num3;
		}
		
		#endregion

	}	[StructLayout (LayoutKind.Sequential)]
	public struct Ray 
		: IEquatable<Ray>
	{
		// The starting position of this ray
		public Vector3 Position;
		
		// Normalised vector that defines the direction of this ray
		public Vector3 Direction;

		public Ray (Vector3 position, Vector3 direction)
		{
			this.Position = position;
			this.Direction = direction;
		}

		// Determines whether or not this ray is equal in value to another ray
		public Boolean Equals (Ray other)
		{
			// Check position
			if (this.Position.X != other.Position.X) return false;
			if (this.Position.Y != other.Position.Y) return false;
			if (this.Position.Z != other.Position.Z) return false;

			// Check direction
			if (this.Direction.X != other.Direction.X) return false;
			if (this.Direction.Y != other.Direction.Y) return false;
			if (this.Direction.Z != other.Direction.Z) return false;

			// They match!
			return true;
		}

		// Determines whether or not this ray is equal in value to another System.Object
		public override Boolean Equals (Object obj)
		{
			if (obj == null) return false;

			if (obj is Ray)
			{
				// Ok, it is a Ray, so just use the method above to compare.
				return this.Equals ((Ray) obj);
			}

			return false;
		}

		public override Int32 GetHashCode ()
		{
			return (this.Position.GetHashCode () + this.Direction.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{Position:{0} Direction:{1}}}", this.Position, this.Direction);
		}

		// At what distance from it's starting position does this ray
		// intersect the given box.  Returns null if there is no
		// intersection.
		public Fixed32? Intersects (ref BoundingBox box)
		{
			return box.Intersects (ref this);
		}

		// At what distance from it's starting position does this ray
		// intersect the given frustum.  Returns null if there is no
		// intersection.
		public Fixed32? Intersects (ref BoundingFrustum frustum)
		{
			if (frustum == null)
			{
				throw new ArgumentNullException ();
			}

			return frustum.Intersects (ref this);
		}

		// At what distance from it's starting position does this ray
		// intersect the given plane.  Returns null if there is no
		// intersection.
		public Fixed32? Intersects (ref Plane plane)
		{
			Fixed32 zero = 0;

			Fixed32 nearZero; RealMaths.FromString("0.00001", out nearZero);

			Fixed32 num2 = ((plane.Normal.X * this.Direction.X) + (plane.Normal.Y * this.Direction.Y)) + (plane.Normal.Z * this.Direction.Z);
			
			if (RealMaths.Abs (num2) < nearZero)
			{
				return null;
			}
			
			Fixed32 num3 = ((plane.Normal.X * this.Position.X) + (plane.Normal.Y * this.Position.Y)) + (plane.Normal.Z * this.Position.Z);

			Fixed32 num = (-plane.D - num3) / num2;
			
			if (num < zero)
			{
				if (num < -nearZero)
				{
					return null;
				}

				num = zero;
			}

			return new Fixed32? (num);
		}

		// At what distance from it's starting position does this ray
		// intersect the given sphere.  Returns null if there is no
		// intersection.
		public Fixed32? Intersects (ref BoundingSphere sphere)
		{
			Fixed32 zero = 0;

			Fixed32 initialXOffset = sphere.Center.X - this.Position.X;

			Fixed32 initialYOffset = sphere.Center.Y - this.Position.Y;
			
			Fixed32 initialZOffset = sphere.Center.Z - this.Position.Z;
			
			Fixed32 num7 = ((initialXOffset * initialXOffset) + (initialYOffset * initialYOffset)) + (initialZOffset * initialZOffset);

			Fixed32 num2 = sphere.Radius * sphere.Radius;

			if (num7 <= num2)
			{
				return zero;
			}

			Fixed32 num = ((initialXOffset * this.Direction.X) + (initialYOffset * this.Direction.Y)) + (initialZOffset * this.Direction.Z);
			if (num < zero)
			{
				return null;
			}
			
			Fixed32 num6 = num7 - (num * num);
			if (num6 > num2)
			{
				return null;
			}
			
			Fixed32 num8 = RealMaths.Sqrt ((num2 - num6));

			return new Fixed32? (num - num8);
		}

		public static Boolean operator == (Ray a, Ray b)
		{
			return a.Equals(b);
		}

		public static Boolean operator != (Ray a, Ray b)
		{
			return !a.Equals(b);
		}
	}
	[StructLayout (LayoutKind.Sequential)]
	public struct Triangle
		: IEquatable<Triangle>
	{
		public Vector3 A;
		public Vector3 B;
		public Vector3 C;

		public Triangle (Vector3 a, Vector3 b, Vector3 c)
		{
			this.A = a;
			this.B = b;
			this.C = c;
		}

		// Determines whether or not this Triangle is equal in value to another Triangle
		public Boolean Equals (Triangle other)
		{
			if (this.A.X != other.A.X) return false;
			if (this.A.Y != other.A.Y) return false;
			if (this.A.Z != other.A.Z) return false;

			if (this.B.X != other.B.X) return false;
			if (this.B.Y != other.B.Y) return false;
			if (this.B.Z != other.B.Z) return false;

			if (this.C.X != other.C.X) return false;
			if (this.C.Y != other.C.Y) return false;
			if (this.C.Z != other.C.Z) return false;

			// They match!
			return true;
		}

		// Determines whether or not this Triangle is equal in value to another System.Object
		public override Boolean Equals (Object obj)
		{
			if (obj == null) return false;

			if (obj is Triangle)
			{
				// Ok, it is a Triangle, so just use the method above to compare.
				return this.Equals ((Triangle) obj);
			}

			return false;
		}

		public override Int32 GetHashCode ()
		{
			return (this.A.GetHashCode () + this.B.GetHashCode () + this.C.GetHashCode ());
		}

		public override String ToString ()
		{
			return string.Format ("{{A:{0} B:{1} C:{2}}}", this.A, this.B, this.C);
		}

		public static Boolean operator == (Triangle a, Triangle b)
		{
			return a.Equals(b);
		}

		public static Boolean operator != (Triangle a, Triangle b)
		{
			return !a.Equals(b);
		}

		public static Boolean IsPointInTriangleangle( ref Vector3 point, ref Triangle triangle )
		{
			Vector3 aToB = triangle.B - triangle.A;
			Vector3 bToC = triangle.C - triangle.B;

			Vector3 n; Vector3.Cross(ref aToB, ref bToC, out n);

			Vector3 aToPoint = point - triangle.A;

			Vector3 wTest; Vector3.Cross(ref aToB, ref aToPoint, out wTest);

			Fixed32 zero = 0;

			Fixed32 dot; Vector3.Dot(ref wTest, ref n, out dot);

			if ( dot < zero )
			{
				return false;
			}

			Vector3 bToPoint = point - triangle.B;

			Vector3.Cross(ref bToC, ref bToPoint, out wTest);

			Vector3.Dot(ref wTest, ref n, out dot);

			if ( dot < zero )
			{
				return false;
			}

			Vector3 cToA = triangle.A - triangle.C;

			Vector3 cToPoint = point - triangle.C;

			Vector3.Cross(ref cToA, ref cToPoint, out wTest);

			Vector3.Dot(ref wTest, ref n, out dot);

			if ( dot < zero )
			{
				return false;
			}

			return true;
		}

		// Determines whether or not a triangle is degenerate ( all points lay on the same line in space ).
		public Boolean IsDegenerate()
		{
			throw new System.NotImplementedException();
		}

		// Get's the Barycentric coordinates of a point inside a Triangle.
		public static void BarycentricCoordinates( ref Vector3 point, ref Triangle triangle, out Vector3 barycentricCoordinates )
		{
			if( triangle.IsDegenerate() )
			{
				throw new System.ArgumentException("Input Triangle is degenerate, this is not supported.");
			}

			Vector3 aToB = triangle.B - triangle.A;
			Vector3 aToC = triangle.C - triangle.A;
			Vector3 aToPoint = point - triangle.A;

			// compute cross product to get area of parallelograms
			Vector3 cross1; Vector3.Cross(ref aToB, ref aToPoint, out cross1);
			Vector3 cross2; Vector3.Cross(ref aToC, ref aToPoint, out cross2);
			Vector3 cross3; Vector3.Cross(ref aToB, ref aToC, out cross3);
	
			// compute barycentric coordinates as ratios of areas

			Fixed32 one = 1;

			Fixed32 denom = one / cross3.Length();
			barycentricCoordinates.X = cross2.Length() * denom;
			barycentricCoordinates.Y = cross1.Length() * denom;
			barycentricCoordinates.Z = one - barycentricCoordinates.X - barycentricCoordinates.Y;
		}
		/*

		// Triangleangle Intersect
		// ------------------
		// Returns true if triangles P0P1P2 and Q0Q1Q2 intersect
		// Assumes triangle is not degenerate
		//
		// This is not the algorithm presented in the text.  Instead, it is based on a 
		// recent article by Guigue and Devillers in the July 2003 issue Journal of 
		// Graphics Tools.  As it is faster than the ERIT algorithm, under ordinary 
		// circumstances it would have been discussed in the text, but it arrived too late.  
		//
		// More information and the original source code can be found at
		// http://www.acm.org/jgt/papers/GuigueDevillers03/
		//
		// A nearly identical algorithm was in the same issue of JGT, by Shen Heng and 
		// Tang.  See http://www.acm.org/jgt/papers/ShenHengTang03/ for source code.
		//
		// Yes, this is complicated.  Did you think testing triangles would be easy?
		//
		static Boolean TriangleangleIntersect( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2 )
		{
			// test P against Q's plane
			Vector3 normalQ = Vector3.Cross( Q1 - Q0, Q2 - Q0 );

			Single testP0 = Vector3.Dot( normalQ, P0 - Q0 );
			Single testP1 = Vector3.Dot( normalQ, P1 - Q0 );
			Single testP2 = Vector3.Dot( normalQ, P2 - Q0 );
  
			// P doesn't intersect Q's plane
			if ( testP0 * testP1 > AbacusHelper.Epsilon && testP0*testP2 > AbacusHelper.Epsilon )
				return false;

			// test Q against P's plane
			Vector3 normalP = Vector3.Cross( P1 - P0, P2 - P0 );

			Single testQ0 = Vector3.Dot( normalP, Q0 - P0 );
			Single testQ1 = Vector3.Dot( normalP, Q1 - P0 );
			Single testQ2 = Vector3.Dot( normalP, Q2 - P0 );
  
			// Q doesn't intersect P's plane
			if (testQ0*testQ1 > AbacusHelper.Epsilon && testQ0*testQ2 > AbacusHelper.Epsilon )
				return false;
	
			// now we rearrange P's vertices such that the lone vertex (the one that lies
			// in its own half-space of Q) is first.  We also permute the other
			// triangle's vertices so that P0 will "see" them in counterclockwise order

			// Once reordered, we pass the vertices down to a helper function which will
			// reorder Q's vertices, and then test

			// P0 in Q's positive half-space
			if (testP0 > AbacusHelper.Epsilon) 
			{
				// P1 in Q's positive half-space (so P2 is lone vertex)
				if (testP1 > AbacusHelper.Epsilon) 
					return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
				// P2 in Q's positive half-space (so P1 is lone vertex)
				else if (testP2 > AbacusHelper.Epsilon) 
					return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);	
				// P0 is lone vertex
				else 
					return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
			} 
			// P0 in Q's negative half-space
			else if (testP0 < -AbacusHelper.Epsilon) 
			{
				// P1 in Q's negative half-space (so P2 is lone vertex)
				if (testP1 < -AbacusHelper.Epsilon) 
					return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
				// P2 in Q's negative half-space (so P1 is lone vertex)
				else if (testP2 < -AbacusHelper.Epsilon) 
					return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
				// P0 is lone vertex
				else 
					return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
			} 
			// P0 on Q's plane
			else 
			{
				// P1 in Q's negative half-space 
				if (testP1 < -AbacusHelper.Epsilon) 
				{
					// P2 in Q's negative half-space (P0 is lone vertex)
					if (testP2 < -AbacusHelper.Epsilon) 
						return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
					// P2 in positive half-space or on plane (P1 is lone vertex)
					else 
						return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
				}
				// P1 in Q's positive half-space 
				else if (testP1 > AbacusHelper.Epsilon) 
				{
					// P2 in Q's positive half-space (P0 is lone vertex)
					if (testP2 > AbacusHelper.Epsilon) 
						return AdjustQ(ref P0, ref P1, ref P2, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
					// P2 in negative half-space or on plane (P1 is lone vertex)
					else 
						return AdjustQ(ref P1, ref P2, ref P0, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
				}
				// P1 lies on Q's plane too
				else  
				{
					// P2 in Q's positive half-space (P2 is lone vertex)
					if (testP2 > AbacusHelper.Epsilon) 
						return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q1, ref Q2, testQ0, testQ1, testQ2, ref normalP);
					// P2 in Q's negative half-space (P2 is lone vertex)
					// note different ordering for Q vertices
					else if (testP2 < -AbacusHelper.Epsilon) 
						return AdjustQ(ref P2, ref P0, ref P1, ref Q0, ref Q2, ref Q1, testQ0, testQ2, testQ1, ref normalP);
					// all three points lie on Q's plane, default to 2D test
					else 
						return CoplanarTriangleangleIntersect(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, ref normalP);
				}
			}
		}



		// Adjust Q
		// --------
		// Helper for TriangleangleIntersect()
		//
		// Now we rearrange Q's vertices such that the lone vertex (the one that lies
		// in its own half-space of P) is first.  We also permute the other
		// triangle's vertices so that Q0 will "see" them in counterclockwise order
		//
		// Once reordered, we pass the vertices down to a helper function which will
		// actually test for intersection on the common line between the two planes
		//
		static Boolean AdjustQ( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2,
			Single testQ0, Single testQ1, Single testQ2,
			ref Vector3 normalP )
		{

			// Q0 in P's positive half-space
			if (testQ0 > AbacusHelper.Epsilon) 
			{ 
				// Q1 in P's positive half-space (so Q2 is lone vertex)
				if (testQ1 > AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P2, ref P1, ref Q2, ref Q0, ref Q1);
				// Q2 in P's positive half-space (so Q1 is lone vertex)
				else if (testQ2 > AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P2, ref P1, ref Q1, ref Q2, ref Q0);
				// Q0 is lone vertex
				else 
					return TestLineOverlap(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2);
			}
			// Q0 in P's negative half-space
			else if (testQ0 < -AbacusHelper.Epsilon) 
			{ 
				// Q1 in P's negative half-space (so Q2 is lone vertex)
				if (testQ1 < -AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P1, ref P2, ref Q2, ref Q0, ref Q1);
				// Q2 in P's negative half-space (so Q1 is lone vertex)
				else if (testQ2 < -AbacusHelper.Epsilon) 
					return TestLineOverlap(ref P0, ref P1, ref P2, ref Q1, ref Q2, ref Q0);
				// Q0 is lone vertex
				else 
					return TestLineOverlap(ref P0, ref P2, ref P1, ref Q0, ref Q1, ref Q2);
			}
			// Q0 on P's plane
			else 
			{ 
				// Q1 in P's negative half-space 
				if (testQ1 < -AbacusHelper.Epsilon) 
				{ 
					// Q2 in P's negative half-space (Q0 is lone vertex)
					if (testQ2 < -AbacusHelper.Epsilon)  
						return TestLineOverlap(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2);
					// Q2 in positive half-space or on plane (P1 is lone vertex)
					else 
						return TestLineOverlap(ref P0, ref P2, ref P1, ref Q1, ref Q2, ref Q0);
				}
				// Q1 in P's positive half-space 
				else if (testQ1 > AbacusHelper.Epsilon) 
				{ 
					// Q2 in P's positive half-space (Q0 is lone vertex)
					if (testQ2 > AbacusHelper.Epsilon) 
						return TestLineOverlap(ref P0, ref P2, ref P1, ref Q0, ref Q1, ref Q2);
					// Q2 in negative half-space or on plane (P1 is lone vertex)
					else  
						return TestLineOverlap(ref P0, ref P1, ref P2, ref Q1, ref Q2, ref Q0);
				}
				// Q1 lies on P's plane too
				else 
				{
					// Q2 in P's positive half-space (Q2 is lone vertex)
					if (testQ2 > AbacusHelper.Epsilon) 
						return TestLineOverlap(ref P0, ref P1, ref P2, ref Q2, ref Q0, ref Q1);
					// Q2 in P's negative half-space (Q2 is lone vertex)
					// note different ordering for Q vertices
					else if (testQ2 < -AbacusHelper.Epsilon) 
						return TestLineOverlap(ref P0, ref P2, ref P1, ref Q2, ref Q0, ref Q1);
					// all three points lie on P's plane, default to 2D test
					else 
						return CoplanarTriangleangleIntersect(ref P0, ref P1, ref P2, ref Q0, ref Q1, ref Q2, ref normalP);
				}
			}
		}



		// Test Line Overlap
		// -----------------
		// Helper for TriangleangleIntersect()
		//
		// This tests whether the rearranged triangles overlap, by checking the intervals
		// where their edges cross the common line between the two planes.  If the 
		// interval for P is [i,j] and Q is [k,l], then there is intersection if the
		// intervals overlap.  Previous algorithms computed these intervals directly, 
		// this tests implictly by using two "plane tests."
		//
		static Boolean TestLineOverlap( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2 )
		{
			// get "plane normal"
			Vector3 normal = Vector3.Cross( P1 - P0, Q0 - P0);

			// fails test, no intersection
			if ( Vector3.Dot(normal, Q1 - P0 ) > AbacusHelper.Epsilon )
				return false;
  
			// get "plane normal"
			normal = Vector3.Cross( P2 - P0, Q2 - P0 );

			// fails test, no intersection
			if ( Vector3.Dot( normal, Q0 - P0 ) > AbacusHelper.Epsilon )
				return false;

			// intersection!
			return true;
		}



		// Coplanar Triangleangle Intersect
		// ---------------------------
		// Helper for TriangleangleIntersect()
		//
		// This projects the two triangles down to 2D, maintaining the largest area by
		// dropping the dimension where the normal points the farthest.
		//
		static Boolean CoplanarTriangleangleIntersect( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Vector3 Q0, ref Vector3 Q1, ref Vector3 Q2, 
			ref Vector3 planeNormal )
		{
			Vector3 absNormal = new Vector3( 
				System.Math.Abs(planeNormal.X), 
				System.Math.Abs(planeNormal.Y), 
				System.Math.Abs(planeNormal.Z) );

			Vector2 projP0, projP1, projP2;
			Vector2 projQ0, projQ1, projQ2;

			// if x is direction of largest magnitude
			if ( absNormal.X > absNormal.Y && absNormal.X >= absNormal.Z )
			{
				projP0 = new Vector2( P0.Y, P0.Z );
				projP1 = new Vector2( P1.Y, P1.Z );
				projP2 = new Vector2( P2.Y, P2.Z );
				projQ0 = new Vector2( Q0.Y, Q0.Z );
				projQ1 = new Vector2( Q1.Y, Q1.Z );
				projQ2 = new Vector2( Q2.Y, Q2.Z );
			}
			// if y is direction of largest magnitude
			else if ( absNormal.Y > absNormal.X && absNormal.Y >= absNormal.Z )
			{
				projP0 = new Vector2( P0.X, P0.Z );
				projP1 = new Vector2( P1.X, P1.Z );
				projP2 = new Vector2( P2.X, P2.Z );
				projQ0 = new Vector2( Q0.X, Q0.Z );
				projQ1 = new Vector2( Q1.X, Q1.Z );
				projQ2 = new Vector2( Q2.X, Q2.Z );
			}
			// z is the direction of largest magnitude
			else
			{
				projP0 = new Vector2( P0.X, P0.Y );
				projP1 = new Vector2( P1.X, P1.Y );
				projP2 = new Vector2( P2.X, P2.Y );
				projQ0 = new Vector2( Q0.X, Q0.Y );
				projQ1 = new Vector2( Q1.X, Q1.Y );
				projQ2 = new Vector2( Q2.X, Q2.Y );
			}

			return TriangleangleIntersect( ref projP0, ref projP1, ref projP2, ref projQ0, ref projQ1, ref projQ2 );
		}



		// Triangleangle Intersect
		// ------------------
		// Returns true if ray intersects triangle.
		//
		static Boolean TriangleangleIntersect( 
			ref Single t, //perhaps this should be out 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Ray ray )
		{
			// test ray direction against triangle
			Vector3 e1 = P1 - P0;
			Vector3 e2 = P2 - P0;
			Vector3 p = Vector3.Cross( ray.Direction, e2 );
			Single a = Vector3.Dot( e1, p );

			// if result zero, no intersection or infinite intersections
			// (ray parallel to triangle plane)
			if ( AbacusHelper.IsZero(a) )
				return false;

			// compute denominator
			Single f = 1.0f/a;

			// compute barycentric coordinates
			Vector3 s = ray.Position - P0;
			Single u = f * Vector3.Dot( s, p );

			// ray falls outside triangle
			if (u < 0.0f || u > 1.0f) 
				return false;

			Vector3 q = Vector3.Cross( s, e1 );
			Single v = f * Vector3.Dot( ray.Direction, q );

			// ray falls outside triangle
			if (v < 0.0f || u+v > 1.0f) 
				return false;

			// compute line parameter
			t = f * Vector3.Dot( e2, q );

			return (t >= 0.0f);
		}

		
		//
		// @ TriangleangleClassify()
		// Returns signed distance between plane and triangle
		//
		static Single TriangleangleClassify( 
			ref Vector3 P0, ref Vector3 P1, ref Vector3 P2, 
			ref Plane plane )
		{
			Single test0 = plane.Test( P0 );
			Single test1 = plane.Test( P1 );

			// if two points lie on opposite sides of plane, intersect
			if (test0*test1 < 0.0f)
				return 0.0f;

			Single test2 = plane.Test( P2 );

			// if two points lie on opposite sides of plane, intersect
			if (test0*test2 < 0.0f)
				return 0.0f;
			if (test1*test2 < 0.0f)
				return 0.0f;

			// no intersection, return signed distance
			if ( test0 < 0.0f )
			{
				if ( test0 < test1 )
				{
					if ( test1 < test2 )
						return test2;
					else
						return test1;
				}
				else if (test0 < test2)
				{
					return test2;
				}
				else
				{   
					return test0;
				}
			}
			else
			{
				if ( test0 > test1 )
				{
					if ( test1 > test2 )
						return test2;
					else
						return test1;
				}
				else if (test0 > test2)
				{
					return test2;
				}
				else
				{   
					return test0;
				}
			}
		}

		*/
	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Vector2
		: IEquatable<Vector2>
	{
		public Fixed32 X;
		public Fixed32 Y;
		
		public Vector2 (Int32 x, Int32 y)
		{
			this.X = (Fixed32) x;
			this.Y = (Fixed32) y;
		}

		public Vector2 (Fixed32 x, Fixed32 y)
		{
			this.X = x;
			this.Y = y;
		}

		public void Set (Fixed32 x, Fixed32 y)
		{
			this.X = x;
			this.Y = y;
		}

		public Vector2 (Fixed32 value)
		{
			this.X = this.Y = value;
		}

		public Fixed32 Length ()
		{
			Fixed32 num = (this.X * this.X) + (this.Y * this.Y);
			return RealMaths.Sqrt (num);
		}

		public Fixed32 LengthSquared ()
		{
			return ((this.X * this.X) + (this.Y * this.Y));
		}

		public void Normalise ()
		{
			Fixed32 num2 = (this.X * this.X) + (this.Y * this.Y);

			Fixed32 one = 1;
			Fixed32 num = one / (RealMaths.Sqrt (num2));
			this.X *= num;
			this.Y *= num;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1}}}", new Object[] { this.X.ToString (), this.Y.ToString () });
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Vector2) {
				flag = this.Equals ((Vector2)obj);
			}
			return flag;
		}
		
		public override Int32 GetHashCode ()
		{
			return (this.X.GetHashCode () + this.Y.GetHashCode ());
		}

		public Boolean IsUnit()
		{
			Fixed32 one = 1;

			return RealMaths.IsZero(one - X*X - Y*Y);
		}

		#region IEquatable<Vector2>
		public Boolean Equals (Vector2 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y));
		}
		#endregion

		#region Constants

		static Vector2 zero;
		static Vector2 one;
		static Vector2 unitX;
		static Vector2 unitY;

		static Vector2 ()
		{
			Fixed32 temp_one; RealMaths.One(out temp_one);
			Fixed32 temp_zero; RealMaths.Zero(out temp_zero);

			zero = new Vector2 ();
			one = new Vector2 (temp_one, temp_one);
			unitX = new Vector2 (temp_one, temp_zero);
			unitY = new Vector2 (temp_zero, temp_one);
		}

		public static Vector2 Zero
		{
			get { return zero; }
		}
		
		public static Vector2 One
		{
			get { return one; }
		}
		
		public static Vector2 UnitX
		{
			get { return unitX; }
		}
		
		public static Vector2 UnitY
		{
			get { return unitY; }
		}
		
		#endregion
		#region Maths

		public static void Distance (ref Vector2 value1, ref Vector2 value2, out Fixed32 result)
		{
			Fixed32 num2 = value1.X - value2.X;
			Fixed32 num = value1.Y - value2.Y;
			Fixed32 num3 = (num2 * num2) + (num * num);
			result = RealMaths.Sqrt (num3);
		}

		public static void DistanceSquared (ref Vector2 value1, ref Vector2 value2, out Fixed32 result)
		{
			Fixed32 num2 = value1.X - value2.X;
			Fixed32 num = value1.Y - value2.Y;
			result = (num2 * num2) + (num * num);
		}

		public static void Dot (ref Vector2 value1, ref Vector2 value2, out Fixed32 result)
		{
			result = (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		public static void PerpDot (ref Vector2 value1, ref Vector2 value2, out Fixed32 result)
		{
			result = (value1.X * value2.Y - value1.Y * value2.X);
		}

		public static void Perpendicular (ref Vector2 value, out Vector2 result)
		{
			result = new Vector2 (-value.X, value.Y);
		}

		public static void Normalise (ref Vector2 value, out Vector2 result)
		{
			Fixed32 one = 1;

			Fixed32 num2 = (value.X * value.X) + (value.Y * value.Y);
			Fixed32 num = one / (RealMaths.Sqrt (num2));
			result.X = value.X * num;
			result.Y = value.Y * num;
		}

		public static void Reflect (ref Vector2 vector, ref Vector2 normal, out Vector2 result)
		{
			Fixed32 two = 2;

			Fixed32 num = (vector.X * normal.X) + (vector.Y * normal.Y);
			result.X = vector.X - ((two * num) * normal.X);
			result.Y = vector.Y - ((two * num) * normal.Y);
		}
		
		public static void Transform (ref Vector2 position, ref Matrix44 matrix, out Vector2 result)
		{
			Fixed32 num2 = ((position.X * matrix.M11) + (position.Y * matrix.M21)) + matrix.M41;
			Fixed32 num = ((position.X * matrix.M12) + (position.Y * matrix.M22)) + matrix.M42;
			result.X = num2;
			result.Y = num;
		}
		
		public static void TransformNormal (ref Vector2 normal, ref Matrix44 matrix, out Vector2 result)
		{
			Fixed32 num2 = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
			Fixed32 num = (normal.X * matrix.M12) + (normal.Y * matrix.M22);
			result.X = num2;
			result.Y = num;
		}
		
		public static void Transform (ref Vector2 value, ref Quaternion rotation, out Vector2 result)
		{
			Fixed32 one = 1;

			Fixed32 num10 = rotation.X + rotation.X;
			Fixed32 num5 = rotation.Y + rotation.Y;
			Fixed32 num4 = rotation.Z + rotation.Z;
			Fixed32 num3 = rotation.W * num4;
			Fixed32 num9 = rotation.X * num10;
			Fixed32 num2 = rotation.X * num5;
			Fixed32 num8 = rotation.Y * num5;
			Fixed32 num = rotation.Z * num4;
			Fixed32 num7 = (value.X * ((one - num8) - num)) + (value.Y * (num2 - num3));
			Fixed32 num6 = (value.X * (num2 + num3)) + (value.Y * ((one - num9) - num));
			result.X = num7;
			result.Y = num6;
		}
		
		#endregion
		#region Operators

		public static Vector2 operator - (Vector2 value)
		{
			Vector2 vector;
			vector.X = -value.X;
			vector.Y = -value.Y;
			return vector;
		}
		
		public static Boolean operator == (Vector2 value1, Vector2 value2)
		{
			return ((value1.X == value2.X) && (value1.Y == value2.Y));
		}
		
		public static Boolean operator != (Vector2 value1, Vector2 value2)
		{
			if (value1.X == value2.X) {
				return !(value1.Y == value2.Y);
			}
			return true;
		}

		public static Vector2 operator + (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			return vector;
		}

		public static Vector2 operator - (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			return vector;
		}

		public static Vector2 operator * (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X * value2.X;
			vector.Y = value1.Y * value2.Y;
			return vector;
		}
		
		public static Vector2 operator * (Vector2 value, Fixed32 scaleFactor)
		{
			Vector2 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}
		
		public static Vector2 operator * (Fixed32 scaleFactor, Vector2 value)
		{
			Vector2 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}

		public static Vector2 operator / (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			return vector;
		}
		
		public static Vector2 operator / (Vector2 value1, Fixed32 divider)
		{
			Vector2 vector;
			Fixed32 one = 1;
			Fixed32 num = one / divider;
			vector.X = value1.X * num;
			vector.Y = value1.Y * num;
			return vector;
		}
		
		public static void Negate (ref Vector2 value, out Vector2 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		public static void Add (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		public static void Subtract (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		public static void Multiply (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}
		
		public static void Multiply (ref Vector2 value1, Fixed32 scaleFactor, out Vector2 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		public static void Divide (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		public static void Divide (ref Vector2 value1, Fixed32 divider, out Vector2 result)
		{
			Fixed32 one = 1;
			Fixed32 num = one / divider;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
		}
		
		#endregion
		#region Splines

		public static void Barycentric (ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, Fixed32 amount1, Fixed32 amount2, out Vector2 result)
		{
			result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
			result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
		}

		public static void SmoothStep (ref Vector2 value1, ref Vector2 value2, Fixed32 amount, out Vector2 result)
		{
			Fixed32 zero = 0;
			Fixed32 one = 1;
			Fixed32 two = 2;
			Fixed32 three = 3;

			amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
			amount = (amount * amount) * (three - (two * amount));
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
		}
		
		public static void CatmullRom (ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, Fixed32 amount, out Vector2 result)
		{
			Fixed32 half; RealMaths.Half(out half);
			Fixed32 two = 2;
			Fixed32 three = 3;
			Fixed32 four = 4;
			Fixed32 five = 5;

			Fixed32 num = amount * amount;
			Fixed32 num2 = amount * num;
			result.X = half * ((((two * value2.X) + ((-value1.X + value3.X) * amount)) + (((((two * value1.X) - (five * value2.X)) + (four * value3.X)) - value4.X) * num)) + ((((-value1.X + (three * value2.X)) - (three * value3.X)) + value4.X) * num2));
			result.Y = half * ((((two * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((two * value1.Y) - (five * value2.Y)) + (four * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (three * value2.Y)) - (three * value3.Y)) + value4.Y) * num2));
		}

		public static void Hermite (ref Vector2 value1, ref Vector2 tangent1, ref Vector2 value2, ref Vector2 tangent2, Fixed32 amount, out Vector2 result)
		{
			Fixed32 one = 1;
			Fixed32 two = 2;
			Fixed32 three = 3;

			Fixed32 num = amount * amount;
			Fixed32 num2 = amount * num;
			Fixed32 num6 = ((two * num2) - (three * num)) + one;
			Fixed32 num5 = (-two * num2) + (three * num);
			Fixed32 num4 = (num2 - (two * num)) + amount;
			Fixed32 num3 = num2 - num;
			result.X = (((value1.X * num6) + (value2.X * num5)) + (tangent1.X * num4)) + (tangent2.X * num3);
			result.Y = (((value1.Y * num6) + (value2.Y * num5)) + (tangent1.Y * num4)) + (tangent2.Y * num3);
		}
		
		#endregion
		#region Utilities

		public static void Min (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
		}

		public static void Max (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
		}

		public static void Clamp (ref Vector2 value1, ref Vector2 min, ref Vector2 max, out Vector2 result)
		{
			Fixed32 x = value1.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;
			Fixed32 y = value1.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;
			result.X = x;
			result.Y = y;
		}
		
		public static void Lerp (ref Vector2 value1, ref Vector2 value2, Fixed32 amount, out Vector2 result)
		{
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
		}
		
		#endregion

	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Vector3 
		: IEquatable<Vector3>
	{
		public Fixed32 X;
		public Fixed32 Y;
		public Fixed32 Z;

		public Vector2 XY
		{
			get
			{
				return new Vector2(X, Y);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}



		public Vector3 (Fixed32 x, Fixed32 y, Fixed32 z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public Vector3 (Fixed32 value)
		{
			this.X = this.Y = this.Z = value;
		}
		
		public Vector3 (Vector2 value, Fixed32 z)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1} Z:{2}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString () });
		}

		public Boolean Equals (Vector3 other)
		{
			return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Vector3) {
				flag = this.Equals ((Vector3)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return ((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ());
		}

		public Fixed32 Length ()
		{
			Fixed32 num = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
			return RealMaths.Sqrt (num);
		}

		public Fixed32 LengthSquared ()
		{
			return (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
		}


		public void Normalise ()
		{
			Fixed32 one = 1;
			Fixed32 num2 = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
			Fixed32 num = one / RealMaths.Sqrt (num2);
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
		}

		public Boolean IsUnit()
		{
			Fixed32 one = 1;

			return RealMaths.IsZero(one - X*X - Y*Y - Z*Z);
		}

		#region Constants

		static Vector3 _zero;
		static Vector3 _one;
		static Vector3 _half;
		static Vector3 _unitX;
		static Vector3 _unitY;
		static Vector3 _unitZ;
		static Vector3 _up;
		static Vector3 _down;
		static Vector3 _right;
		static Vector3 _left;
		static Vector3 _forward;
		static Vector3 _backward;

		static Vector3 ()
		{
			Fixed32 temp_one; RealMaths.One(out temp_one);
			Fixed32 temp_half; RealMaths.Half(out temp_half);
			Fixed32 temp_zero; RealMaths.Zero(out temp_zero);

			_zero = new Vector3 ();
			_one = new Vector3 (temp_one, temp_one, temp_one);
			_half = new Vector3(temp_half, temp_half, temp_half);
			_unitX = new Vector3 (temp_one, temp_zero, temp_zero);
			_unitY = new Vector3 (temp_zero, temp_one, temp_zero);
			_unitZ = new Vector3 (temp_zero, temp_zero, temp_one);
			_up = new Vector3 (temp_zero, temp_one, temp_zero);
			_down = new Vector3 (temp_zero, -temp_one, temp_zero);
			_right = new Vector3 (temp_one, temp_zero, temp_zero);
			_left = new Vector3 (-temp_one, temp_zero, temp_zero);
			_forward = new Vector3 (temp_zero, temp_zero, -temp_one);
			_backward = new Vector3 (temp_zero, temp_zero, temp_one);
		}
		
		public static Vector3 Zero {
			get {
				return _zero;
			}
		}
		
		public static Vector3 One {
			get {
				return _one;
			}
		}
		
		public static Vector3 Half {
			get {
				return _half;
			}
		}
		
		public static Vector3 UnitX {
			get {
				return _unitX;
			}
		}
		
		public static Vector3 UnitY {
			get {
				return _unitY;
			}
		}
		
		public static Vector3 UnitZ {
			get {
				return _unitZ;
			}
		}
		
		public static Vector3 Up {
			get {
				return _up;
			}
		}
		
		public static Vector3 Down {
			get {
				return _down;
			}
		}
		
		public static Vector3 Right {
			get {
				return _right;
			}
		}
		
		public static Vector3 Left {
			get {
				return _left;
			}
		}
		
		public static Vector3 Forward {
			get {
				return _forward;
			}
		}
		
		public static Vector3 Backward {
			get {
				return _backward;
			}
		}
		
		#endregion
		#region Maths

		public static void Distance (ref Vector3 value1, ref Vector3 value2, out Fixed32 result)
		{
			Fixed32 num3 = value1.X - value2.X;
			Fixed32 num2 = value1.Y - value2.Y;
			Fixed32 num = value1.Z - value2.Z;
			Fixed32 num4 = ((num3 * num3) + (num2 * num2)) + (num * num);
			result = RealMaths.Sqrt (num4);
		}
		
		public static void DistanceSquared (ref Vector3 value1, ref Vector3 value2, out Fixed32 result)
		{
			Fixed32 num3 = value1.X - value2.X;
			Fixed32 num2 = value1.Y - value2.Y;
			Fixed32 num = value1.Z - value2.Z;
			result = ((num3 * num3) + (num2 * num2)) + (num * num);
		}

		public static void Dot (ref Vector3 vector1, ref Vector3 vector2, out Fixed32 result)
		{
			result = ((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z);
		}

		public static void Normalise (ref Vector3 value, out Vector3 result)
		{
			Fixed32 one = 1;

			Fixed32 num2 = ((value.X * value.X) + (value.Y * value.Y)) + (value.Z * value.Z);
			Fixed32 num = one / RealMaths.Sqrt (num2);
			result.X = value.X * num;
			result.Y = value.Y * num;
			result.Z = value.Z * num;
		}

		public static void Cross (ref Vector3 vector1, ref Vector3 vector2, out Vector3 result)
		{
			Fixed32 num3 = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y);
			Fixed32 num2 = (vector1.Z * vector2.X) - (vector1.X * vector2.Z);
			Fixed32 num = (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
			result.X = num3;
			result.Y = num2;
			result.Z = num;
		}

		public static void Reflect (ref Vector3 vector, ref Vector3 normal, out Vector3 result)
		{
			Fixed32 two = 2;

			Fixed32 num = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
			result.X = vector.X - ((two * num) * normal.X);
			result.Y = vector.Y - ((two * num) * normal.Y);
			result.Z = vector.Z - ((two * num) * normal.Z);
		}

		public static void Transform (ref Vector3 position, ref Matrix44 matrix, out Vector3 result)
		{
			Fixed32 num3 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
			Fixed32 num2 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
			Fixed32 num = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
			result.X = num3;
			result.Y = num2;
			result.Z = num;
		}
		
		public static void TransformNormal (ref Vector3 normal, ref Matrix44 matrix, out Vector3 result)
		{
			Fixed32 num3 = ((normal.X * matrix.M11) + (normal.Y * matrix.M21)) + (normal.Z * matrix.M31);
			Fixed32 num2 = ((normal.X * matrix.M12) + (normal.Y * matrix.M22)) + (normal.Z * matrix.M32);
			Fixed32 num = ((normal.X * matrix.M13) + (normal.Y * matrix.M23)) + (normal.Z * matrix.M33);
			result.X = num3;
			result.Y = num2;
			result.Z = num;
		}
		
		public static void Transform (ref Vector3 value, ref Quaternion rotation, out Vector3 result)
		{
			Fixed32 one = 1;
			Fixed32 num12 = rotation.X + rotation.X;
			Fixed32 num2 = rotation.Y + rotation.Y;
			Fixed32 num = rotation.Z + rotation.Z;
			Fixed32 num11 = rotation.W * num12;
			Fixed32 num10 = rotation.W * num2;
			Fixed32 num9 = rotation.W * num;
			Fixed32 num8 = rotation.X * num12;
			Fixed32 num7 = rotation.X * num2;
			Fixed32 num6 = rotation.X * num;
			Fixed32 num5 = rotation.Y * num2;
			Fixed32 num4 = rotation.Y * num;
			Fixed32 num3 = rotation.Z * num;
			Fixed32 num15 = ((value.X * ((one - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
			Fixed32 num14 = ((value.X * (num7 + num9)) + (value.Y * ((one - num8) - num3))) + (value.Z * (num4 - num11));
			Fixed32 num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((one - num8) - num5));
			result.X = num15;
			result.Y = num14;
			result.Z = num13;
		}
		
		#endregion
		#region Operators

		public static Vector3 operator - (Vector3 value)
		{
			Vector3 vector;
			vector.X = -value.X;
			vector.Y = -value.Y;
			vector.Z = -value.Z;
			return vector;
		}
		
		public static Boolean operator == (Vector3 value1, Vector3 value2)
		{
			return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z));
		}
		
		public static Boolean operator != (Vector3 value1, Vector3 value2)
		{
			if ((value1.X == value2.X) && (value1.Y == value2.Y)) {
				return !(value1.Z == value2.Z);
			}
			return true;
		}
		
		public static Vector3 operator + (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			vector.Z = value1.Z + value2.Z;
			return vector;
		}
		
		public static Vector3 operator - (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			vector.Z = value1.Z - value2.Z;
			return vector;
		}
		
		public static Vector3 operator * (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X * value2.X;
			vector.Y = value1.Y * value2.Y;
			vector.Z = value1.Z * value2.Z;
			return vector;
		}
		
		public static Vector3 operator * (Vector3 value, Fixed32 scaleFactor)
		{
			Vector3 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			return vector;
		}
		
		public static Vector3 operator * (Fixed32 scaleFactor, Vector3 value)
		{
			Vector3 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			return vector;
		}
		
		public static Vector3 operator / (Vector3 value1, Vector3 value2)
		{
			Vector3 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			vector.Z = value1.Z / value2.Z;
			return vector;
		}
		
		public static Vector3 operator / (Vector3 value, Fixed32 divider)
		{
			Vector3 vector;
			Fixed32 one = 1;

			Fixed32 num = one / divider;
			vector.X = value.X * num;
			vector.Y = value.Y * num;
			vector.Z = value.Z * num;
			return vector;
		}

		public static void Negate (ref Vector3 value, out Vector3 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
		}

		public static void Add (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
		}

		public static void Subtract (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
		}

		public static void Multiply (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
		}

		public static void Multiply (ref Vector3 value1, Fixed32 scaleFactor, out Vector3 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
		}

		public static void Divide (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
		}

		public static void Divide (ref Vector3 value1, Fixed32 value2, out Vector3 result)
		{
			Fixed32 one = 1;
			Fixed32 num = one / value2;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
		}
		
		#endregion
		#region Splines

		public static void Barycentric (ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, Fixed32 amount1, Fixed32 amount2, out Vector3 result)
		{
			result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
			result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
			result.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
		}
	
		public static void SmoothStep (ref Vector3 value1, ref Vector3 value2, Fixed32 amount, out Vector3 result)
		{
			Fixed32 zero = 0;
			Fixed32 one = 1;
			Fixed32 two = 2;
			Fixed32 three = 3;

			amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
			amount = (amount * amount) * (three - (two * amount));
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
		}

		public static void CatmullRom (ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4, Fixed32 amount, out Vector3 result)
		{
			Fixed32 half; RealMaths.Half(out half);
			Fixed32 two = 2;
			Fixed32 three = 3;
			Fixed32 four = 4;
			Fixed32 five = 5;

			Fixed32 num = amount * amount;
			Fixed32 num2 = amount * num;
			result.X = half * ((((two * value2.X) + ((-value1.X + value3.X) * amount)) + (((((two * value1.X) - (five * value2.X)) + (four * value3.X)) - value4.X) * num)) + ((((-value1.X + (three * value2.X)) - (three * value3.X)) + value4.X) * num2));
			result.Y = half * ((((two * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((two * value1.Y) - (five * value2.Y)) + (four * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (three * value2.Y)) - (three * value3.Y)) + value4.Y) * num2));
			result.Z = half * ((((two * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((two * value1.Z) - (five * value2.Z)) + (four * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (three * value2.Z)) - (three * value3.Z)) + value4.Z) * num2));
		}

		public static void Hermite (ref Vector3 value1, ref Vector3 tangent1, ref Vector3 value2, ref Vector3 tangent2, Fixed32 amount, out Vector3 result)
		{
			Fixed32 one = 1;
			Fixed32 two = 2;
			Fixed32 three = 3;

			Fixed32 num = amount * amount;
			Fixed32 num2 = amount * num;
			Fixed32 num6 = ((two * num2) - (three * num)) + one;
			Fixed32 num5 = (-two * num2) + (three * num);
			Fixed32 num4 = (num2 - (two * num)) + amount;
			Fixed32 num3 = num2 - num;
			result.X = (((value1.X * num6) + (value2.X * num5)) + (tangent1.X * num4)) + (tangent2.X * num3);
			result.Y = (((value1.Y * num6) + (value2.Y * num5)) + (tangent1.Y * num4)) + (tangent2.Y * num3);
			result.Z = (((value1.Z * num6) + (value2.Z * num5)) + (tangent1.Z * num4)) + (tangent2.Z * num3);
		}
		
		#endregion
		#region Utilities

		public static void Min (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z < value2.Z) ? value1.Z : value2.Z;
		}

		public static void Max (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z > value2.Z) ? value1.Z : value2.Z;
		}
		
		public static void Clamp (ref Vector3 value1, ref Vector3 min, ref Vector3 max, out Vector3 result)
		{
			Fixed32 x = value1.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;
			Fixed32 y = value1.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;
			Fixed32 z = value1.Z;
			z = (z > max.Z) ? max.Z : z;
			z = (z < min.Z) ? min.Z : z;
			result.X = x;
			result.Y = y;
			result.Z = z;
		}

		public static void Lerp (ref Vector3 value1, ref Vector3 value2, Fixed32 amount, out Vector3 result)
		{
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
		}
		
		#endregion

	}
	[StructLayout (LayoutKind.Sequential)]
	public partial struct Vector4 
		: IEquatable<Vector4>
	{
		public Fixed32 X;
		public Fixed32 Y;
		public Fixed32 Z;
		public Fixed32 W;

		public Vector3 XYZ
		{
			get
			{
				return new Vector3(X, Y, Z);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
				this.Z = value.Z;
			}
		}



		public Vector4 (Fixed32 x, Fixed32 y, Fixed32 z, Fixed32 w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		public Vector4 (Vector2 value, Fixed32 z, Fixed32 w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
			this.W = w;
		}

		public Vector4 (Vector3 value, Fixed32 w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}

		public Vector4 (Fixed32 value)
		{
			this.X = this.Y = this.Z = this.W = value;
		}

		public override String ToString ()
		{
			return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString (), this.W.ToString () });
		}

		public Boolean Equals (Vector4 other)
		{
			return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
		}

		public override Boolean Equals (Object obj)
		{
			Boolean flag = false;
			if (obj is Vector4) {
				flag = this.Equals ((Vector4)obj);
			}
			return flag;
		}

		public override Int32 GetHashCode ()
		{
			return (((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ()) + this.W.GetHashCode ());
		}

		public Fixed32 Length ()
		{
			Fixed32 num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			return RealMaths.Sqrt (num);
		}

		public Fixed32 LengthSquared ()
		{
			return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
		}



		public void Normalise ()
		{
			Fixed32 one = 1;
			Fixed32 num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
			Fixed32 num = one / RealMaths.Sqrt (num2);
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
			this.W *= num;
		}



		public Boolean IsUnit()
		{
			Fixed32 one = 1;

			return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
		}

		#region Constants

		static Vector4 _zero;
		static Vector4 _one;
		static Vector4 _unitX;
		static Vector4 _unitY;
		static Vector4 _unitZ;
		static Vector4 _unitW;

		static Vector4 ()
		{
			Fixed32 temp_one; RealMaths.One(out temp_one);
			Fixed32 temp_zero; RealMaths.Zero(out temp_zero);

			_zero = new Vector4 ();
			_one = new Vector4 (temp_one, temp_one, temp_one, temp_one);
			_unitX = new Vector4 (temp_one, temp_zero, temp_zero, temp_zero);
			_unitY = new Vector4 (temp_zero, temp_one, temp_zero, temp_zero);
			_unitZ = new Vector4 (temp_zero, temp_zero, temp_one, temp_zero);
			_unitW = new Vector4 (temp_zero, temp_zero, temp_zero, temp_one);
		}

		public static Vector4 Zero {
			get {
				return _zero;
			}
		}
		
		public static Vector4 One {
			get {
				return _one;
			}
		}
		
		public static Vector4 UnitX {
			get {
				return _unitX;
			}
		}
		
		public static Vector4 UnitY {
			get {
				return _unitY;
			}
		}
		
		public static Vector4 UnitZ {
			get {
				return _unitZ;
			}
		}
		
		public static Vector4 UnitW {
			get {
				return _unitW;
			}
		}
		
		#endregion
		#region Maths

		public static void Distance (ref Vector4 value1, ref Vector4 value2, out Fixed32 result)
		{
			Fixed32 num4 = value1.X - value2.X;
			Fixed32 num3 = value1.Y - value2.Y;
			Fixed32 num2 = value1.Z - value2.Z;
			Fixed32 num = value1.W - value2.W;
			Fixed32 num5 = (((num4 * num4) + (num3 * num3)) + (num2 * num2)) + (num * num);
			result = RealMaths.Sqrt (num5);
		}

		public static void DistanceSquared (ref Vector4 value1, ref Vector4 value2, out Fixed32 result)
		{
			Fixed32 num4 = value1.X - value2.X;
			Fixed32 num3 = value1.Y - value2.Y;
			Fixed32 num2 = value1.Z - value2.Z;
			Fixed32 num = value1.W - value2.W;
			result = (((num4 * num4) + (num3 * num3)) + (num2 * num2)) + (num * num);
		}

		public static void Dot (ref Vector4 vector1, ref Vector4 vector2, out Fixed32 result)
		{
			result = (((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z)) + (vector1.W * vector2.W);
		}

		public static void Normalise (ref Vector4 vector, out Vector4 result)
		{
			Fixed32 one = 1;
			Fixed32 num2 = (((vector.X * vector.X) + (vector.Y * vector.Y)) + (vector.Z * vector.Z)) + (vector.W * vector.W);
			Fixed32 num = one / (RealMaths.Sqrt (num2));
			result.X = vector.X * num;
			result.Y = vector.Y * num;
			result.Z = vector.Z * num;
			result.W = vector.W * num;
		}

		public static void Transform (ref Vector2 position, ref Matrix44 matrix, out Vector4 result)
		{
			Fixed32 num4 = ((position.X * matrix.M11) + (position.Y * matrix.M21)) + matrix.M41;
			Fixed32 num3 = ((position.X * matrix.M12) + (position.Y * matrix.M22)) + matrix.M42;
			Fixed32 num2 = ((position.X * matrix.M13) + (position.Y * matrix.M23)) + matrix.M43;
			Fixed32 num = ((position.X * matrix.M14) + (position.Y * matrix.M24)) + matrix.M44;
			result.X = num4;
			result.Y = num3;
			result.Z = num2;
			result.W = num;
		}
		
		public static void Transform (ref Vector3 position, ref Matrix44 matrix, out Vector4 result)
		{
			Fixed32 num4 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
			Fixed32 num3 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
			Fixed32 num2 = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
			Fixed32 num = (((position.X * matrix.M14) + (position.Y * matrix.M24)) + (position.Z * matrix.M34)) + matrix.M44;
			result.X = num4;
			result.Y = num3;
			result.Z = num2;
			result.W = num;
		}
		
		public static void Transform (ref Vector4 vector, ref Matrix44 matrix, out Vector4 result)
		{
			Fixed32 num4 = (((vector.X * matrix.M11) + (vector.Y * matrix.M21)) + (vector.Z * matrix.M31)) + (vector.W * matrix.M41);
			Fixed32 num3 = (((vector.X * matrix.M12) + (vector.Y * matrix.M22)) + (vector.Z * matrix.M32)) + (vector.W * matrix.M42);
			Fixed32 num2 = (((vector.X * matrix.M13) + (vector.Y * matrix.M23)) + (vector.Z * matrix.M33)) + (vector.W * matrix.M43);
			Fixed32 num = (((vector.X * matrix.M14) + (vector.Y * matrix.M24)) + (vector.Z * matrix.M34)) + (vector.W * matrix.M44);
			result.X = num4;
			result.Y = num3;
			result.Z = num2;
			result.W = num;
		}
		
		
		public static void Transform (ref Vector2 value, ref Quaternion rotation, out Vector4 result)
		{
			Fixed32 one = 1;
			Fixed32 num6 = rotation.X + rotation.X;
			Fixed32 num2 = rotation.Y + rotation.Y;
			Fixed32 num = rotation.Z + rotation.Z;
			Fixed32 num15 = rotation.W * num6;
			Fixed32 num14 = rotation.W * num2;
			Fixed32 num5 = rotation.W * num;
			Fixed32 num13 = rotation.X * num6;
			Fixed32 num4 = rotation.X * num2;
			Fixed32 num12 = rotation.X * num;
			Fixed32 num11 = rotation.Y * num2;
			Fixed32 num10 = rotation.Y * num;
			Fixed32 num3 = rotation.Z * num;
			Fixed32 num9 = (value.X * ((one - num11) - num3)) + (value.Y * (num4 - num5));
			Fixed32 num8 = (value.X * (num4 + num5)) + (value.Y * ((one - num13) - num3));
			Fixed32 num7 = (value.X * (num12 - num14)) + (value.Y * (num10 + num15));
			result.X = num9;
			result.Y = num8;
			result.Z = num7;
			result.W = one;
		}
		
		public static void Transform (ref Vector3 value, ref Quaternion rotation, out Vector4 result)
		{
			Fixed32 one = 1;
			Fixed32 num12 = rotation.X + rotation.X;
			Fixed32 num2 = rotation.Y + rotation.Y;
			Fixed32 num = rotation.Z + rotation.Z;
			Fixed32 num11 = rotation.W * num12;
			Fixed32 num10 = rotation.W * num2;
			Fixed32 num9 = rotation.W * num;
			Fixed32 num8 = rotation.X * num12;
			Fixed32 num7 = rotation.X * num2;
			Fixed32 num6 = rotation.X * num;
			Fixed32 num5 = rotation.Y * num2;
			Fixed32 num4 = rotation.Y * num;
			Fixed32 num3 = rotation.Z * num;
			Fixed32 num15 = ((value.X * ((one - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
			Fixed32 num14 = ((value.X * (num7 + num9)) + (value.Y * ((one - num8) - num3))) + (value.Z * (num4 - num11));
			Fixed32 num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((one - num8) - num5));
			result.X = num15;
			result.Y = num14;
			result.Z = num13;
			result.W = one;
		}
		
		public static void Transform (ref Vector4 value, ref Quaternion rotation, out Vector4 result)
		{
			Fixed32 one = 1;
			Fixed32 num12 = rotation.X + rotation.X;
			Fixed32 num2 = rotation.Y + rotation.Y;
			Fixed32 num = rotation.Z + rotation.Z;
			Fixed32 num11 = rotation.W * num12;
			Fixed32 num10 = rotation.W * num2;
			Fixed32 num9 = rotation.W * num;
			Fixed32 num8 = rotation.X * num12;
			Fixed32 num7 = rotation.X * num2;
			Fixed32 num6 = rotation.X * num;
			Fixed32 num5 = rotation.Y * num2;
			Fixed32 num4 = rotation.Y * num;
			Fixed32 num3 = rotation.Z * num;
			Fixed32 num15 = ((value.X * ((one - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
			Fixed32 num14 = ((value.X * (num7 + num9)) + (value.Y * ((one - num8) - num3))) + (value.Z * (num4 - num11));
			Fixed32 num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((one - num8) - num5));
			result.X = num15;
			result.Y = num14;
			result.Z = num13;
			result.W = value.W;
		}
		
		#endregion
		#region Operators

		public static Vector4 operator - (Vector4 value)
		{
			Vector4 vector;
			vector.X = -value.X;
			vector.Y = -value.Y;
			vector.Z = -value.Z;
			vector.W = -value.W;
			return vector;
		}
		
		public static Boolean operator == (Vector4 value1, Vector4 value2)
		{
			return ((((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) && (value1.W == value2.W));
		}
		
		public static Boolean operator != (Vector4 value1, Vector4 value2)
		{
			if (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) {
				return !(value1.W == value2.W);
			}
			return true;
		}
		
		public static Vector4 operator + (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			vector.Z = value1.Z + value2.Z;
			vector.W = value1.W + value2.W;
			return vector;
		}
		
		public static Vector4 operator - (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			vector.Z = value1.Z - value2.Z;
			vector.W = value1.W - value2.W;
			return vector;
		}
		
		public static Vector4 operator * (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X * value2.X;
			vector.Y = value1.Y * value2.Y;
			vector.Z = value1.Z * value2.Z;
			vector.W = value1.W * value2.W;
			return vector;
		}
		
		public static Vector4 operator * (Vector4 value1, Fixed32 scaleFactor)
		{
			Vector4 vector;
			vector.X = value1.X * scaleFactor;
			vector.Y = value1.Y * scaleFactor;
			vector.Z = value1.Z * scaleFactor;
			vector.W = value1.W * scaleFactor;
			return vector;
		}
		
		public static Vector4 operator * (Fixed32 scaleFactor, Vector4 value1)
		{
			Vector4 vector;
			vector.X = value1.X * scaleFactor;
			vector.Y = value1.Y * scaleFactor;
			vector.Z = value1.Z * scaleFactor;
			vector.W = value1.W * scaleFactor;
			return vector;
		}
		
		public static Vector4 operator / (Vector4 value1, Vector4 value2)
		{
			Vector4 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			vector.Z = value1.Z / value2.Z;
			vector.W = value1.W / value2.W;
			return vector;
		}
		
		public static Vector4 operator / (Vector4 value1, Fixed32 divider)
		{
			Fixed32 one = 1;
			Vector4 vector;
			Fixed32 num = one / divider;
			vector.X = value1.X * num;
			vector.Y = value1.Y * num;
			vector.Z = value1.Z * num;
			vector.W = value1.W * num;
			return vector;
		}
		
		public static void Negate (ref Vector4 value, out Vector4 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = -value.W;
		}

		public static void Add (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
		}
		
		public static void Subtract (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
		}
		
		public static void Multiply (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
			result.W = value1.W * value2.W;
		}

		public static void Multiply (ref Vector4 value1, Fixed32 scaleFactor, out Vector4 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
			result.W = value1.W * scaleFactor;
		}

		public static void Divide (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
			result.W = value1.W / value2.W;
		}
		
		public static void Divide (ref Vector4 value1, Fixed32 divider, out Vector4 result)
		{
			Fixed32 one = 1;
			Fixed32 num = one / divider;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
			result.W = value1.W * num;
		}
		
		#endregion
		#region Splines

		public static void Barycentric (ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, Fixed32 amount1, Fixed32 amount2, out Vector4 result)
		{
			result.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
			result.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
			result.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
			result.W = (value1.W + (amount1 * (value2.W - value1.W))) + (amount2 * (value3.W - value1.W));
		}

		public static void SmoothStep (ref Vector4 value1, ref Vector4 value2, Fixed32 amount, out Vector4 result)
		{
			Fixed32 zero = 0;
			Fixed32 one = 1;
			Fixed32 two = 2;
			Fixed32 three = 3;

			amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
			amount = (amount * amount) * (three - (two * amount));
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
			result.W = value1.W + ((value2.W - value1.W) * amount);
		}

		public static void CatmullRom (ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, ref Vector4 value4, Fixed32 amount, out Vector4 result)
		{
			Fixed32 half; RealMaths.Half(out half);
			Fixed32 two = 2;
			Fixed32 three = 3;
			Fixed32 four = 4;
			Fixed32 five = 5;

			Fixed32 num = amount * amount;
			Fixed32 num2 = amount * num;
			result.X = half * ((((two * value2.X) + ((-value1.X + value3.X) * amount)) + (((((two * value1.X) - (five * value2.X)) + (four * value3.X)) - value4.X) * num)) + ((((-value1.X + (three * value2.X)) - (three * value3.X)) + value4.X) * num2));
			result.Y = half * ((((two * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((two * value1.Y) - (five * value2.Y)) + (four * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (three * value2.Y)) - (three * value3.Y)) + value4.Y) * num2));
			result.Z = half * ((((two * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((two * value1.Z) - (five * value2.Z)) + (four * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (three * value2.Z)) - (three * value3.Z)) + value4.Z) * num2));
			result.W = half * ((((two * value2.W) + ((-value1.W + value3.W) * amount)) + (((((two * value1.W) - (five * value2.W)) + (four * value3.W)) - value4.W) * num)) + ((((-value1.W + (three * value2.W)) - (three * value3.W)) + value4.W) * num2));
		}

		public static void Hermite (ref Vector4 value1, ref Vector4 tangent1, ref Vector4 value2, ref Vector4 tangent2, Fixed32 amount, out Vector4 result)
		{
			Fixed32 one = 1;
			Fixed32 two = 2;
			Fixed32 three = 3;

			Fixed32 num = amount * amount;
			Fixed32 num6 = amount * num;
			Fixed32 num5 = ((two * num6) - (three * num)) + one;
			Fixed32 num4 = (-two * num6) + (three * num);
			Fixed32 num3 = (num6 - (two * num)) + amount;
			Fixed32 num2 = num6 - num;
			result.X = (((value1.X * num5) + (value2.X * num4)) + (tangent1.X * num3)) + (tangent2.X * num2);
			result.Y = (((value1.Y * num5) + (value2.Y * num4)) + (tangent1.Y * num3)) + (tangent2.Y * num2);
			result.Z = (((value1.Z * num5) + (value2.Z * num4)) + (tangent1.Z * num3)) + (tangent2.Z * num2);
			result.W = (((value1.W * num5) + (value2.W * num4)) + (tangent1.W * num3)) + (tangent2.W * num2);
		}
		
		#endregion

		#region Utilities

		public static void Min (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z < value2.Z) ? value1.Z : value2.Z;
			result.W = (value1.W < value2.W) ? value1.W : value2.W;
		}

		public static void Max (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
			result.Z = (value1.Z > value2.Z) ? value1.Z : value2.Z;
			result.W = (value1.W > value2.W) ? value1.W : value2.W;
		}
		
		public static void Clamp (ref Vector4 value1, ref Vector4 min, ref Vector4 max, out Vector4 result)
		{
			Fixed32 x = value1.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;
			Fixed32 y = value1.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;
			Fixed32 z = value1.Z;
			z = (z > max.Z) ? max.Z : z;
			z = (z < min.Z) ? min.Z : z;
			Fixed32 w = value1.W;
			w = (w > max.W) ? max.W : w;
			w = (w < min.W) ? min.W : w;
			result.X = x;
			result.Y = y;
			result.Z = z;
			result.W = w;
		}
		
		public static void Lerp (ref Vector4 value1, ref Vector4 value2, Fixed32 amount, out Vector4 result)
		{
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
			result.Z = value1.Z + ((value2.Z - value1.Z) * amount);
			result.W = value1.W + ((value2.W - value1.W) * amount);
		}
		
		#endregion


	}

}

