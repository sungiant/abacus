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
// │ Copyright © 2013 A.J.Pook (http://abacus3d.github.com)                 │ \\
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

namespace Abacus
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPackedReal
    {

        /// <summary>
        /// 
        /// </summary>
        void PackFrom(Single input);

        /// <summary>
        /// 
        /// </summary>
        void UnpackTo(out Single output);

        /// <summary>
        /// 
        /// </summary>
        void PackFrom(Double input);

        /// <summary>
        /// 
        /// </summary>
        void UnpackTo(out Double output);

        /// <summary>
        /// 
        /// </summary>
        void PackFrom(Fixed32 input);

        /// <summary>
        /// 
        /// </summary>
        void UnpackTo(out Fixed32 output);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPackedReal2
    {

        /// <summary>
        /// 
        /// </summary>
        void PackFrom(ref SinglePrecision.Vector2 input);

        /// <summary>
        /// 
        /// </summary>
        void UnpackTo(out SinglePrecision.Vector2 output);

        /// <summary>
        /// 
        /// </summary>
        void PackFrom(ref DoublePrecision.Vector2 input);

        /// <summary>
        /// 
        /// </summary>
        void UnpackTo(out DoublePrecision.Vector2 output);

        /// <summary>
        /// 
        /// </summary>
        void PackFrom(ref Fixed32Precision.Vector2 input);

        /// <summary>
        /// 
        /// </summary>
        void UnpackTo(out Fixed32Precision.Vector2 output);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPackedReal3
    {
        /// <summary>
        /// 
        /// </summary>
        void PackFrom(ref SinglePrecision.Vector3 input);

        /// <summary>
        /// 
        /// </summary>
        void UnpackTo(out SinglePrecision.Vector3 output);
        /// <summary>
        /// 
        /// </summary>
        void PackFrom(ref DoublePrecision.Vector3 input);

        /// <summary>
        /// 
        /// </summary>
        void UnpackTo(out DoublePrecision.Vector3 output);
        /// <summary>
        /// 
        /// </summary>
        void PackFrom(ref Fixed32Precision.Vector3 input);

        /// <summary>
        /// 
        /// </summary>
        void UnpackTo(out Fixed32Precision.Vector3 output);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPackedReal4
    {
        /// <summary>
        /// 
        /// </summary>
        void PackFrom(ref SinglePrecision.Vector4 input);

        /// <summary>
        /// 
        /// </summary>
        void UnpackTo(out SinglePrecision.Vector4 output);
        /// <summary>
        /// 
        /// </summary>
        void PackFrom(ref DoublePrecision.Vector4 input);

        /// <summary>
        /// 
        /// </summary>
        void UnpackTo(out DoublePrecision.Vector4 output);
        /// <summary>
        /// 
        /// </summary>
        void PackFrom(ref Fixed32Precision.Vector4 input);

        /// <summary>
        /// 
        /// </summary>
        void UnpackTo(out Fixed32Precision.Vector4 output);
    }

    /// <summary>
    /// T is the type that the value is packed into
    /// </summary>
    public interface IPackedValue<T>
    {
        /// <summary>
        /// todo
        /// </summary>
        T PackedValue { get; set; }
    }

    /// <summary>
    /// todo
    /// </summary>
    internal static class PackUtils
    {
        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        internal static UInt32 PackSigned (UInt32 bitmask, Single value)
        {
            Single max = bitmask >> 1;
            Single min = -max - 1f;
            return (((UInt32)((Int32)ClampAndRound (value, min, max))) & bitmask);
        }

        /// <summary>
        /// todo
        /// </summary>
        internal static UInt32 PackUnsigned (Single bitmask, Single value)
        {
            return (UInt32)ClampAndRound (value, 0f, bitmask);
        }

        /// <summary>
        /// todo
        /// </summary>
        internal static UInt32 PackSignedNormalised (UInt32 bitmask, Single value)
        {
            if (value > 1f || value < 0f)
                throw new ArgumentException ("Input value must be normalised.");

            Single max = bitmask >> 1;
            value *= max;
            UInt32 result = (((UInt32)((Int32)ClampAndRound (value, -max, max))) & bitmask);
            return result;
        }

        /// <summary>
        /// todo
        /// </summary>
        internal static Single UnpackSignedNormalised (UInt32 bitmask, UInt32 value)
        {
            UInt32 num = (UInt32)((bitmask + 1) >> 1);

            if ((value & num) != 0)
            {
                if ((value & bitmask) == num)
                {
                    return -1f;
                }

                value |= ~bitmask;
            }
            else
            {
                value &= bitmask;
            }

            Single num2 = bitmask >> 1;

            Single result = (((Single)value) / num2);

            if (result > 1f || result < 0f)
                throw new ArgumentException ("Input value does not yield a normalised result.");

            return result;
        }

        /// <summary>
        /// todo
        /// </summary>
        internal static UInt32 PackUnsignedNormalisedValue (Single bitmask, Single value)
        {
            if (value > 1f || value < 0f)
                throw new ArgumentException ("Input value must be normalised.");

            value *= bitmask;
            UInt32 result = (UInt32)ClampAndRound (value, 0f, bitmask);
            return result;
        }
        
        /// <summary>
        /// todo
        /// </summary>
        internal static Single UnpackUnsignedNormalisedValue (UInt32 bitmask, UInt32 value)
        {
            value &= bitmask;
            Single result = (((Single)value) / ((Single)bitmask));

            if (result > 1f || result < 0f)
                throw new ArgumentException ("Input value does not yield a normalised result.");

            return result;
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
    [StructLayout(LayoutKind.Sequential), Serializable]
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



        public static bool IsInfinity(Fixed32 value)
        {
            return ( value == Fixed32.NegativeInfinity || value == Fixed32.PositiveInfinity );
        }

        public static bool IsNegativeInfinity(Fixed32 value)
        {
            return ( value == Fixed32.NegativeInfinity );
        }
        
        public static bool IsPositiveInfinity(Fixed32 value)
        {
            return ( value == Fixed32.PositiveInfinity );
        }

        public static bool IsNaNInfinity(Fixed32 value)
        {
            return ( value == Fixed32.NaN );
        }

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

        
        //static readonly Int32 FMask = One.RawValue - 1;
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
        public static readonly Fixed32 MaxValue = CreateFromRaw(Int32.MaxValue - 2);
        public static readonly Fixed32 MinValue = CreateFromRaw(Int32.MinValue + 1);
        public static readonly Fixed32 PositiveInfinity = CreateFromRaw(Int32.MaxValue - 1);
        public static readonly Fixed32 NegativeInfinity = CreateFromRaw(Int32.MinValue);
        public static readonly Fixed32 NaN = CreateFromRaw(Int32.MaxValue);

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
        
        internal static Fixed32 Sqrt (Fixed32 f)
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

        internal static Fixed32 Square (Fixed32 f)
        {
            int v = f.numerator >> (n / 2);
            int w = f.numerator >> (n - (n / 2));
            return CreateFromRaw (v * w);
        }

        internal static Fixed32 Sin (Fixed32 f)
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
        
        internal static Fixed32 Cos (Fixed32 f)
        {
            return Sin (PiOver2 - f);
        }
        
        internal static Fixed32 Tan (Fixed32 f)
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

            if (!(value is Abacus.Fixed32))
                throw new ArgumentException("Value is not a Abacus.Fixed32.");

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

    /// <summary>
    /// This class provides maths functions with consistent function
    /// signatures across all supported precisions.  The idea being
    /// the more you use this, the more you will be able to write
    /// code once and easily change the precision later.
    /// </summary>
    public static class RealMaths
    {
        /// <summary>
        /// Assigns a Single precision real number representing
        /// zero to the output value.
        /// </summary>
        public static void Zero(out Single value) { value = 0; }

        /// <summary>
        /// Assigns a Double precision real number representing
        /// zero to the output value.
        /// </summary>
        public static void Zero(out Double value) { value = 0; }

        /// <summary>
        /// Assigns a Fixed32 precision real number representing
        /// zero to the output value.
        /// </summary>
        public static void Zero(out Fixed32 value) { value = 0; }

        /// <summary>
        /// Assigns a Single precision real number representing half to the
        /// output value.
        /// </summary>
        public static void Half(out Single value) { value = 0.5f; }

        /// <summary>
        /// Assigns a Double precision real number representing half to the
        /// output value.
        /// </summary>
        public static void Half(out Double value) { value = 0.5; }

        /// <summary>
        /// Assigns a Fixed32 precision real number representing half to the
        /// output value.
        /// </summary>
        public static void Half(out Fixed32 value) { value = Fixed32.Parse("0.5"); }

        /// <summary>
        /// Assigns a Single precision real number representing
        /// one to the output value.
        /// </summary>
        public static void One(out Single value) { value = 1; }

        /// <summary>
        /// Assigns a Double precision real number representing
        /// one to the output value.
        /// </summary>
        public static void One(out Double value) { value = 1; }

        /// <summary>
        /// Assigns a Fixed32 precision real number representing
        /// one to the output value.
        /// </summary>
        public static void One(out Fixed32 value) { value = 1; }


        /// <summary>
        /// Assigns a Single precision real number representing the mathematical
        /// constant E, Euler's number, to the output value.
        /// </summary>
        public static void E(out Single value) { value = 2.71828183f; }

        /// <summary>
        /// Assigns a Double precision real number representing the mathematical
        /// constant E, Euler's number, to the output value.
        /// </summary>
        public static void E(out Double value) { value = 2.71828182845904523536028747135266249775724709369995; }

        /// <summary>
        /// Assigns a Fixed32 precision real number representing the mathematical
        /// constant E, Euler's number, to the output value.
        /// </summary>
        public static void E(out Fixed32 value) { value = Fixed32.Parse("2.71828183"); }

        /// <summary>
        /// Assigns a Single precision real number representing the common
        /// logarithm of the mathematical constant E to the output value.
        /// </summary>
        public static void Log10E(out Single value) { value = 0.4342944821f; }

        /// <summary>
        /// Assigns a Double precision real number representing the binary
        /// logarithm of the mathematical constant E to the output value.
        /// </summary>
        public static void Log10E(out Double value) { value = 0.4342945; }

        /// <summary>
        /// Assigns a Fixed32 precision real number representing the binary
        /// logarithm of the mathematical constant E to the output value.
        /// </summary>
        public static void Log10E(out Fixed32 value) { value = Fixed32.Parse("0.4342945"); }

        /// <summary>
        /// Assigns a Single precision real number representing the binary
        /// logarithm of the mathematical constant E to the output value.
        /// </summary>
        public static void Log2E(out Single value) { value = 1.442695f; }

        /// <summary>
        /// Assigns a Double precision real number representing the binary
        /// logarithm of the mathematical constant E to the output value.
        /// </summary>
        public static void Log2E(out Double value) { value = 1.442695; }

        /// <summary>
        /// Assigns a Fixed32 precision real number representing the binary
        /// logarithm of the mathematical constant E to the output value.
        /// </summary>
        public static void Log2E(out Fixed32 value) { value = Fixed32.Parse("1.442695"); }

        /// <summary>
        /// Assigns a Single precision real number representing the
        /// mathematical constant π to the output value.
        /// </summary>
        public static void Pi(out Single value) { value = 3.1415926536f; }

        /// <summary>
        /// Assigns a Double precision real number representing the
        /// mathematical constant π to the output value.
        /// </summary>
        public static void Pi(out Double value) { value = 3.14159265358979323846264338327950288; }

        /// <summary>
        /// Assigns a Fixed32 precision real number representing the
        /// mathematical constant π to the output value.
        /// </summary>
        public static void Pi(out Fixed32 value) { value = Fixed32.Parse("3.1415926536"); }

        /// <summary>
        /// Assigns a Single precision real number representing the
        /// mathematical constant 2π to the output value.
        /// </summary>
        public static void Tau(out Single value) { value = 6.283185f; }

        /// <summary>
        /// Assigns a Double precision real number representing the
        /// mathematical constant 2π to the output value.
        /// </summary>
        public static void Tau(out Double value) { value = 6.283185; }

        /// <summary>
        /// Assigns a Fixed32 precision real number representing the
        /// mathematical constant 2π to the output value.
        /// </summary>
        public static void Tau(out Fixed32 value) { value = Fixed32.Parse("6.283185"); }

        /// <summary>
        /// todo
        /// </summary>
        public static void Epsilon(out Single value) { value = 1.0e-6f; }

        /// <summary>
        /// todo
        /// </summary>
        public static void Epsilon(out Double value) { value = 1.0e-6; }

        public static void Epsilon(out Fixed32 value) { value = Fixed32.Parse("0.0001"); }

        /// <summary>
        /// todo
        /// </summary>
        public static void Root2(out Single value) { value = 1.414213562f; }

        /// <summary>
        /// todo
        /// </summary>
        public static void Root2(out Double value) { value = 1.414213562; }

        public static void Root2(out Fixed32 value) { value = Fixed32.Parse("1.414213562"); }

        /// <summary>
        /// todo
        /// </summary>
        public static void Root3(out Single value) { value = 1.732050808f; }

        /// <summary>
        /// todo
        /// </summary>
        public static void Root3(out Double value) { value = 1.732050808; }

        public static void Root3(out Fixed32 value) { value = Fixed32.Parse("1.732050808"); }

        /// <summary>
        /// todo
        /// </summary>
        internal static void TestTolerance(out Single value) { value = 1.0e-3f; }

        /// <summary>
        /// todo
        /// </summary>
        internal static void TestTolerance(out Double value) { value = 1.0e-7; }

        /// <summary>
        /// todo
        /// </summary>
        internal static void TestTolerance(out Fixed32 value) { value = Fixed32.Parse("0.0001"); }

        /// <summary>
        /// todo
        /// </summary>
        public static Single Sqrt(Single input)
        {
            Single output = (Single)Math.Sqrt(input);
            return output;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Double Sqrt(Double input)
        {
            return Math.Sqrt(input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 Sqrt(Fixed32 input)
        {
            return Fixed32.Sqrt(input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Single Square(Single input)
        {
            return input * input;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Double Square(Double input)
        {
            return input * input;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 Square(Fixed32 input)
        {
            return Fixed32.Square(input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Single Sin(Single input)
        {
            return (Single) Math.Sin((Single) input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Double Sin(Double input)
        {
            return Math.Sin(input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 Sin(Fixed32 input)
        {
            return Fixed32.Sin(input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Single Cos(Single input)
        {
            return (Single)Math.Cos((Single)input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Double Cos(Double input)
        {
            return Math.Cos(input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 Cos(Fixed32 input)
        {
            return Fixed32.Cos(input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Single Tan(Single input)
        {
            return (Single)Math.Tan((Single)input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Double Tan(Double input)
        {
            return Math.Tan(input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 Tan(Fixed32 input)
        {
            return Fixed32.Tan(input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Single Abs(Single input)
        {
            return (Single)Math.Abs((Single)input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Double Abs(Double input)
        {
            return Math.Abs(input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 Abs(Fixed32 input)
        {
            if (input < new Fixed32(0))
            {
                return input * new Fixed32(-1);
            }

            return input;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Single ArcSin(Single input)
        {
            return (Single)Math.Asin((Single)input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Double ArcSin(Double input)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 ArcSin(Fixed32 input)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Single ArcCos(Single input)
        {
            return (Single)Math.Acos((Single)input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Double ArcCos(Double input)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 ArcCos(Fixed32 input)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Single ArcTan(Single input)
        {
            return (Single)Math.Atan((Single)input);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Double ArcTan(Double input)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 ArcTan(Fixed32 input)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Single ToRadians(Single input)
        {
            Single tau; Tau(out tau);
            return input * tau / ((Single)360);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Single ToDegrees(Single input)
        {
            Single tau; Tau(out tau);
            return input / tau * ((Single)360);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void FromFraction(Int32 numerator, Int32 denominator, out Single value)
        {
            value = (Single) numerator / (Single) denominator;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void FromString(String str, out Single value)
        {
            Single.TryParse(str, out value);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean IsZero(Single value)
        {
            Single ep;
            Epsilon(out ep);
            return Abs(value) < ep;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Single Min(Single a, Single b)
        {
            return a < b ? a : b;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Single Max(Single a, Single b)
        {
            return a > b ? a : b;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean WithinEpsilon(Single a, Single b)
        {
            Single num = a - b;
            return ((-Single.Epsilon <= num) && (num <= Single.Epsilon));
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        public static Double ToRadians(Double input)
        {
            Double tau; Tau(out tau);
            return input * tau / ((Double)360);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Double ToDegrees(Double input)
        {
            Double tau; Tau(out tau);
            return input / tau * ((Double)360);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void FromFraction(Int32 numerator, Int32 denominator, out Double value)
        {
            value = (Double) numerator / (Double) denominator;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void FromString(String str, out Double value)
        {
            Double.TryParse(str, out value);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean IsZero(Double value)
        {
            Double ep;
            Epsilon(out ep);
            return Abs(value) < ep;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Double Min(Double a, Double b)
        {
            return a < b ? a : b;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Double Max(Double a, Double b)
        {
            return a > b ? a : b;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean WithinEpsilon(Double a, Double b)
        {
            Double num = a - b;
            return ((-Double.Epsilon <= num) && (num <= Double.Epsilon));
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 ToRadians(Fixed32 input)
        {
            Fixed32 tau; Tau(out tau);
            return input * tau / ((Fixed32)360);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 ToDegrees(Fixed32 input)
        {
            Fixed32 tau; Tau(out tau);
            return input / tau * ((Fixed32)360);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void FromFraction(Int32 numerator, Int32 denominator, out Fixed32 value)
        {
            value = (Fixed32) numerator / (Fixed32) denominator;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void FromString(String str, out Fixed32 value)
        {
            Fixed32.TryParse(str, out value);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean IsZero(Fixed32 value)
        {
            Fixed32 ep;
            Epsilon(out ep);
            return Abs(value) < ep;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 Min(Fixed32 a, Fixed32 b)
        {
            return a < b ? a : b;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Fixed32 Max(Fixed32 a, Fixed32 b)
        {
            return a > b ? a : b;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean WithinEpsilon(Fixed32 a, Fixed32 b)
        {
            Fixed32 num = a - b;
            return ((-Fixed32.Epsilon <= num) && (num <= Fixed32.Epsilon));
        }

        /// <summary>
        /// todo
        /// </summary>
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

    }

    /// <summary>
    /// todo
    /// </summary>
    internal static class MarshalHelper
    {
        // Copies data from an unmanaged memory pointer to a managed 32-bit signed integer array.
        internal static void Copy( IntPtr source, Int32[] destination, Int32 startIndex, Int32 length )
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        // Copies data from an unmanaged memory pointer to a managed 64-bit signed integer array.
        internal static void Copy( IntPtr source, Int64[] destination, Int32 startIndex, Int32 length )
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        // Copies data from an unmanaged memory pointer to a managed single-precision floating-point number array.
        internal static void Copy( IntPtr source, Single[] destination, Int32 startIndex, Int32 length )
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        // Copies data from an unmanaged memory pointer to a managed double-precision floating-point number array.
        internal static void Copy( IntPtr source, Double[] destination, Int32 startIndex, Int32 length )
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        // Copies data from an unmanaged memory pointer to a managed fixed32-precision fixed-point number array.
        internal static void Copy( IntPtr source, Fixed32[] destination, Int32 startIndex, Int32 length )
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// todo
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a random Single between 0.0 & 1.0
        /// </summary>
        public static Single NextSingle(this System.Random r)
        {
            return (Single) r.NextDouble();
        }

        /// <summary>
        /// Returns a random Fixed32 between 0.0 & 1.0
        /// </summary>
        public static Fixed32 NextFixed32(this System.Random r)
        {
            Fixed32 max = 1;
            Fixed32 min = 0;

            Int32 randomInt32 = r.Next(min.RawValue, max.RawValue);

            return Fixed32.CreateFromRaw(randomInt32);
        }
    }

}

namespace Abacus.Packed
{
    /// <summary>
    /// todo
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Alpha_8
        : IPackedValue<Byte>
        , IEquatable<Alpha_8>
        , IPackedReal
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString()
        {
            return this.packedValue.ToString("X2", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(Single realAlpha, out Byte packedAlpha)
        {
            if (realAlpha < 0f || realAlpha > 1f) 
                throw new ArgumentException ("A component of the input source is not unsigned and normalised: " + realAlpha);

            packedAlpha = (Byte)PackUtils.PackUnsignedNormalisedValue(255f, realAlpha);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(Byte packedAlpha, out Single realAlpha)
        {
            realAlpha = PackUtils.UnpackUnsignedNormalisedValue(0xff, packedAlpha);

            if (realAlpha < 0f || realAlpha > 1f) 
                throw new Exception ("A the input source doesn't yeild an unsigned normalised output: " + packedAlpha);
        }

        /// <summary>
        /// todo
        /// </summary>
        Byte packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is Alpha_8) && this.Equals((Alpha_8)obj));
        }

        #region IEquatable<Alpha_8>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(Alpha_8 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(Alpha_8 a, Alpha_8 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(Alpha_8 a, Alpha_8 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public Alpha_8(Single realAlpha)
        {
            Pack(realAlpha, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(Single realAlpha)
        {
            Pack(realAlpha, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Single realAlpha)
        {
            Unpack(this.packedValue, out realAlpha);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Alpha_8(Double realAlpha)
        {
            Pack(realAlpha, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(Double realAlpha)
        {
            Pack(realAlpha, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Double realAlpha)
        {
            Unpack(this.packedValue, out realAlpha);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Alpha_8(Fixed32 realAlpha)
        {
            Pack(realAlpha, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(Fixed32 realAlpha)
        {
            Pack(realAlpha, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32 realAlpha)
        {
            Unpack(this.packedValue, out realAlpha);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(Double realAlpha, out Byte packedAlpha)
        {
            Single temp = (Single)realAlpha;
            Pack(temp, out packedAlpha);
        }

        /// <summary>
        /// 
        /// </summary>
        static void Unpack(Byte packedAlpha, out Double realAlpha)
        {
            Single temp;
            Unpack(packedAlpha, out temp);
            realAlpha = (Double) temp;
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(Fixed32 realAlpha, out Byte packedAlpha)
        {
            Single temp = (Single)realAlpha;
            Pack(temp, out packedAlpha);
        }

        /// <summary>
        /// 
        /// </summary>
        static void Unpack(Byte packedAlpha, out Fixed32 realAlpha)
        {
            Single temp;
            Unpack(packedAlpha, out temp);
            realAlpha = (Fixed32) temp;
        }

    }

    /// <summary>
    /// todo
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Bgr_5_6_5 
        : IPackedValue<UInt16>
        , IEquatable<Bgr_5_6_5>
        , IPackedReal3
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return this.packedValue.ToString ("X4", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref SinglePrecision.Vector3 realRgb, out UInt16 packedBgr)
        {
            if (realRgb.X < 0f || realRgb.X > 1f ||
                realRgb.Y < 0f || realRgb.Y > 1f ||
                realRgb.Z < 0f || realRgb.Z > 1f ) 
                throw new ArgumentException ("A component of the input source is not unsigned and normalised: " + realRgb);

            UInt32 r = PackUtils.PackUnsignedNormalisedValue(31f, realRgb.X) << 11;
            UInt32 g = PackUtils.PackUnsignedNormalisedValue(63f, realRgb.Y) << 5;
            UInt32 b = PackUtils.PackUnsignedNormalisedValue(31f, realRgb.Z);
            packedBgr = (UInt16)((r | g) | b);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt16 packedBgr, out SinglePrecision.Vector3 realRgb)
        {
            realRgb.X = PackUtils.UnpackUnsignedNormalisedValue(0x1f, (UInt32)(packedBgr >> 11));
            realRgb.Y = PackUtils.UnpackUnsignedNormalisedValue(0x3f, (UInt32)(packedBgr >> 5));
            realRgb.Z = PackUtils.UnpackUnsignedNormalisedValue(0x1f, packedBgr);

            if (realRgb.X < 0f || realRgb.X > 1f ||
                realRgb.Y < 0f || realRgb.Y > 1f ||
                realRgb.Z < 0f || realRgb.Z > 1f ) 
                throw new Exception ("A the input source doesn't yeild an unsigned normalised output: " + packedBgr);
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt16 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is Bgr_5_6_5) && this.Equals((Bgr_5_6_5)obj));
        }

        #region IEquatable<Bgr_5_6_5>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(Bgr_5_6_5 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(Bgr_5_6_5 a, Bgr_5_6_5 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(Bgr_5_6_5 a, Bgr_5_6_5 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public Bgr_5_6_5(ref SinglePrecision.Vector3 realRgb)
        {
            Pack(ref realRgb, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector3 realRgb)
        {
            Pack(ref realRgb, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector3 realRgb)
        {
            Unpack(this.packedValue, out realRgb);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Bgr_5_6_5(ref DoublePrecision.Vector3 realRgb)
        {
            Pack(ref realRgb, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector3 realRgb)
        {
            Pack(ref realRgb, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector3 realRgb)
        {
            Unpack(this.packedValue, out realRgb);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Bgr_5_6_5(ref Fixed32Precision.Vector3 realRgb)
        {
            Pack(ref realRgb, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector3 realRgb)
        {
            Pack(ref realRgb, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector3 realRgb)
        {
            Unpack(this.packedValue, out realRgb);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector3 realRgb, out UInt16 packedBgr)
        {
            SinglePrecision.Vector3 singleVector = new SinglePrecision.Vector3((Single)realRgb.X, (Single)realRgb.Y, (Single)realRgb.Z);
            Pack(ref singleVector, out packedBgr);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt16 packedBgr, out DoublePrecision.Vector3 realRgb)
        {
            SinglePrecision.Vector3 singleVector;
            Unpack(packedBgr, out singleVector);
            realRgb = new DoublePrecision.Vector3((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector3 realRgb, out UInt16 packedBgr)
        {
            SinglePrecision.Vector3 singleVector = new SinglePrecision.Vector3((Single)realRgb.X, (Single)realRgb.Y, (Single)realRgb.Z);
            Pack(ref singleVector, out packedBgr);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt16 packedBgr, out Fixed32Precision.Vector3 realRgb)
        {
            SinglePrecision.Vector3 singleVector;
            Unpack(packedBgr, out singleVector);
            realRgb = new Fixed32Precision.Vector3((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z);
        }
    }

    /// <summary>
    /// todo
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Bgra16 
        : IPackedValue<UInt16>
        , IEquatable<Bgra16>
        , IPackedReal4
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return this.packedValue.ToString ("X4", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref SinglePrecision.Vector4 realRgba, out UInt16 packedBgra)
        {
            if (realRgba.X < 0f || realRgba.X > 1f ||
                realRgba.Y < 0f || realRgba.Y > 1f ||
                realRgba.Z < 0f || realRgba.Z > 1f ||
                realRgba.W < 0f || realRgba.W > 1f ) 
                throw new ArgumentException ("A component of the input source is not unsigned and normalised: " + realRgba);

            UInt32 r = PackUtils.PackUnsignedNormalisedValue (15f, realRgba.X) << 8;
            UInt32 g = PackUtils.PackUnsignedNormalisedValue (15f, realRgba.Y) << 4;
            UInt32 b = PackUtils.PackUnsignedNormalisedValue (15f, realRgba.Z);
            UInt32 a = PackUtils.PackUnsignedNormalisedValue (15f, realRgba.W) << 12;
            packedBgra = (UInt16)(((r | g) | b) | a);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt16 packedBgra, out SinglePrecision.Vector4 realRgba)
        {
            realRgba.X = PackUtils.UnpackUnsignedNormalisedValue (15, (UInt32)(packedBgra >> 8));
            realRgba.Y = PackUtils.UnpackUnsignedNormalisedValue (15, (UInt32)(packedBgra >> 4));
            realRgba.Z = PackUtils.UnpackUnsignedNormalisedValue (15, packedBgra);
            realRgba.W = PackUtils.UnpackUnsignedNormalisedValue (15, (UInt32)(packedBgra >> 12));

            if (realRgba.X < 0f || realRgba.X > 1f ||
                realRgba.Y < 0f || realRgba.Y > 1f ||
                realRgba.Z < 0f || realRgba.Z > 1f ||
                realRgba.W < 0f || realRgba.W > 1f ) 
                throw new Exception ("A the input source doesn't yeild an unsigned normalised output: " + packedBgra);
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt16 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is Bgra16) && this.Equals((Bgra16)obj));
        }

        #region IEquatable<Bgra16>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(Bgra16 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(Bgra16 a, Bgra16 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(Bgra16 a, Bgra16 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public Bgra16(ref SinglePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Bgra16(ref DoublePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Bgra16(ref Fixed32Precision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector4 realRgba, out UInt16 packedBgra)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
            Pack(ref singleVector, out packedBgra);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt16 packedBgra, out DoublePrecision.Vector4 realRgba)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedBgra, out singleVector);
            realRgba = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector4 realRgba, out UInt16 packedBgra)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
            Pack(ref singleVector, out packedBgra);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt16 packedBgra, out Fixed32Precision.Vector4 realRgba)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedBgra, out singleVector);
            realRgba = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
        }
    }

    /// <summary>
    /// todo
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Bgra_5_5_5_1 
        : IPackedValue<UInt16>
        , IEquatable<Bgra_5_5_5_1>
        , IPackedReal4
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return this.packedValue.ToString ("X4", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref SinglePrecision.Vector4 realRgba, out UInt16 packedBgra)
        {
            if (realRgba.X < 0f || realRgba.X > 1f ||
                realRgba.Y < 0f || realRgba.Y > 1f ||
                realRgba.Z < 0f || realRgba.Z > 1f ||
                realRgba.W < 0f || realRgba.W > 1f ) 
                throw new ArgumentException ("A component of the input source is not unsigned and normalised: " + realRgba);

            UInt32 r = PackUtils.PackUnsignedNormalisedValue (31f, realRgba.X) << 10;
            UInt32 g = PackUtils.PackUnsignedNormalisedValue (31f, realRgba.Y) << 5;
            UInt32 b = PackUtils.PackUnsignedNormalisedValue (31f, realRgba.Z);
            UInt32 a = PackUtils.PackUnsignedNormalisedValue (1f, realRgba.W) << 15;
            packedBgra = (UInt16)(((r | g) | b) | a);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt16 packedBgra, out SinglePrecision.Vector4 realRgba)
        {
            realRgba.X = PackUtils.UnpackUnsignedNormalisedValue (0x1f, (UInt32)(packedBgra >> 10));
            realRgba.Y = PackUtils.UnpackUnsignedNormalisedValue (0x1f, (UInt32)(packedBgra >> 5));
            realRgba.Z = PackUtils.UnpackUnsignedNormalisedValue (0x1f, packedBgra);
            realRgba.W = PackUtils.UnpackUnsignedNormalisedValue (1, (UInt32)(packedBgra >> 15));

            if (realRgba.X < 0f || realRgba.X > 1f ||
                realRgba.Y < 0f || realRgba.Y > 1f ||
                realRgba.Z < 0f || realRgba.Z > 1f ||
                realRgba.W < 0f || realRgba.W > 1f ) 
                throw new Exception ("A the input source doesn't yeild an unsigned normalised output: " + packedBgra);
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt16 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is Bgra_5_5_5_1) && this.Equals((Bgra_5_5_5_1)obj));
        }

        #region IEquatable<Bgra_5_5_5_1>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(Bgra_5_5_5_1 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(Bgra_5_5_5_1 a, Bgra_5_5_5_1 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(Bgra_5_5_5_1 a, Bgra_5_5_5_1 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public Bgra_5_5_5_1(ref SinglePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Bgra_5_5_5_1(ref DoublePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Bgra_5_5_5_1(ref Fixed32Precision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector4 realRgba, out UInt16 packedBgra)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
            Pack(ref singleVector, out packedBgra);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt16 packedBgra, out DoublePrecision.Vector4 realRgba)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedBgra, out singleVector);
            realRgba = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector4 realRgba, out UInt16 packedBgra)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
            Pack(ref singleVector, out packedBgra);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt16 packedBgra, out Fixed32Precision.Vector4 realRgba)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedBgra, out singleVector);
            realRgba = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
        }
    }

    /// <summary>
    /// todo
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Byte4 
        : IPackedValue<UInt32>
        , IEquatable<Byte4>
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref SinglePrecision.Vector4 realXyzw, out UInt32 packedXyzw)
        {
            UInt32 y = PackUtils.PackUnsigned (255f, realXyzw.X);
            UInt32 x = PackUtils.PackUnsigned (255f, realXyzw.Y) << 8;
            UInt32 z = PackUtils.PackUnsigned (255f, realXyzw.Z) << 0x10;
            UInt32 w = PackUtils.PackUnsigned (255f, realXyzw.W) << 0x18;
            packedXyzw = (UInt32)(((y | x) | z) | w);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedXyzw, out SinglePrecision.Vector4 realXyzw)
        {
            realXyzw.X = packedXyzw & 0xff;
            realXyzw.Y = (packedXyzw >> 8) & 0xff;
            realXyzw.Z = (packedXyzw >> 0x10) & 0xff;
            realXyzw.W = (packedXyzw >> 0x18) & 0xff;
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt32 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is Byte4) && this.Equals((Byte4)obj));
        }

        #region IEquatable<Byte4>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(Byte4 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(Byte4 a, Byte4 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(Byte4 a, Byte4 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public Byte4(ref SinglePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector4 realXyzw)
        {
            Unpack(this.packedValue, out realXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Byte4(ref DoublePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector4 realXyzw)
        {
            Unpack(this.packedValue, out realXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Byte4(ref Fixed32Precision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector4 realXyzw)
        {
            Unpack(this.packedValue, out realXyzw);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector4 realXyzw, out UInt32 packedXyzw)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
            Pack(ref singleVector, out packedXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedXyzw, out DoublePrecision.Vector4 realXyzw)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedXyzw, out singleVector);
            realXyzw = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector4 realXyzw, out UInt32 packedXyzw)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
            Pack(ref singleVector, out packedXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedXyzw, out Fixed32Precision.Vector4 realXyzw)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedXyzw, out singleVector);
            realXyzw = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
        }
    }

    /// <summary>
    /// todo
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct NormalisedByte2 
        : IPackedValue<UInt16>
        , IEquatable<NormalisedByte2>
        , IPackedReal2
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return this.packedValue.ToString ("X4", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref SinglePrecision.Vector2 realXy, out UInt16 packedXy)
        {
            if (realXy.X < -1f || realXy.X > 1f ||
                realXy.Y < -1f || realXy.Y > 1f ) 
                throw new ArgumentException ("A component of the input source is not normalised: " + realXy);

            UInt32 x = PackUtils.PackSignedNormalised(0xff, realXy.X);
            UInt32 y = PackUtils.PackSignedNormalised(0xff, realXy.Y) << 8;
            packedXy = (UInt16)(x | y);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt16 packedXy, out SinglePrecision.Vector2 realXy)
        {
            realXy.X = PackUtils.UnpackSignedNormalised (0xff, packedXy);
            realXy.Y = PackUtils.UnpackSignedNormalised (0xff, (UInt32) (packedXy >> 8));

            if (realXy.X < -1f || realXy.X > 1f ||
                realXy.Y < -1f || realXy.Y > 1f ) 
                throw new Exception ("A the input source doesn't yeild a normalised output: " + packedXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt16 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is NormalisedByte2) && this.Equals((NormalisedByte2)obj));
        }

        #region IEquatable<NormalisedByte2>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(NormalisedByte2 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(NormalisedByte2 a, NormalisedByte2 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(NormalisedByte2 a, NormalisedByte2 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public NormalisedByte2(ref SinglePrecision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector2 realXy)
        {
            Unpack(this.packedValue, out realXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        public NormalisedByte2(ref DoublePrecision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector2 realXy)
        {
            Unpack(this.packedValue, out realXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        public NormalisedByte2(ref Fixed32Precision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector2 realXy)
        {
            Unpack(this.packedValue, out realXy);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector2 realXy, out UInt16 packedXy)
        {
            SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
            Pack(ref singleVector, out packedXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt16 packedXy, out DoublePrecision.Vector2 realXy)
        {
            SinglePrecision.Vector2 singleVector;
            Unpack(packedXy, out singleVector);
            realXy = new DoublePrecision.Vector2((Double)singleVector.X, (Double)singleVector.Y);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector2 realXy, out UInt16 packedXy)
        {
            SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
            Pack(ref singleVector, out packedXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt16 packedXy, out Fixed32Precision.Vector2 realXy)
        {
            SinglePrecision.Vector2 singleVector;
            Unpack(packedXy, out singleVector);
            realXy = new Fixed32Precision.Vector2((Fixed32)singleVector.X, (Fixed32)singleVector.Y);
        }
    }

    /// <summary>
    /// todo
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct NormalisedByte4 
        : IPackedValue<UInt32>
        , IEquatable<NormalisedByte4>
        , IPackedReal4
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref SinglePrecision.Vector4 realXyzw, out UInt32 packedXyzw)
        {
            if (realXyzw.X < -1f || realXyzw.X > 1f ||
                realXyzw.Y < -1f || realXyzw.Y > 1f ||
                realXyzw.Z < -1f || realXyzw.Z > 1f ||
                realXyzw.W < -1f || realXyzw.W > 1f ) 
                throw new ArgumentException ("A component of the input source is not normalised: " + realXyzw);

            UInt32 x = PackUtils.PackSignedNormalised(0xff, realXyzw.X);
            UInt32 y = PackUtils.PackSignedNormalised(0xff, realXyzw.Y) << 8;
            UInt32 z = PackUtils.PackSignedNormalised(0xff, realXyzw.Z) << 16;
            UInt32 w = PackUtils.PackSignedNormalised(0xff, realXyzw.W) << 24;
            packedXyzw = (((x | y) | z) | w);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedXyzw, out SinglePrecision.Vector4 realXyzw)
        {
            realXyzw.X = PackUtils.UnpackSignedNormalised (0xff, packedXyzw);
            realXyzw.Y = PackUtils.UnpackSignedNormalised (0xff, (UInt32) (packedXyzw >> 8));
            realXyzw.Z = PackUtils.UnpackSignedNormalised (0xff, (UInt32) (packedXyzw >> 16));
            realXyzw.W = PackUtils.UnpackSignedNormalised (0xff, (UInt32) (packedXyzw >> 24));

            if (realXyzw.X < -1f || realXyzw.X > 1f ||
                realXyzw.Y < -1f || realXyzw.Y > 1f ||
                realXyzw.Z < -1f || realXyzw.Z > 1f ||
                realXyzw.W < -1f || realXyzw.W > 1f ) 
                throw new Exception ("A the input source doesn't yeild a normalised output: " + packedXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt32 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is NormalisedByte4) && this.Equals((NormalisedByte4)obj));
        }

        #region IEquatable<NormalisedByte4>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(NormalisedByte4 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(NormalisedByte4 a, NormalisedByte4 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(NormalisedByte4 a, NormalisedByte4 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public NormalisedByte4(ref SinglePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector4 realXyzw)
        {
            Unpack(this.packedValue, out realXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        public NormalisedByte4(ref DoublePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector4 realXyzw)
        {
            Unpack(this.packedValue, out realXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        public NormalisedByte4(ref Fixed32Precision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector4 realXyzw)
        {
            Unpack(this.packedValue, out realXyzw);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector4 realXyzw, out UInt32 packedXyzw)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
            Pack(ref singleVector, out packedXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedXyzw, out DoublePrecision.Vector4 realXyzw)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedXyzw, out singleVector);
            realXyzw = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector4 realXyzw, out UInt32 packedXyzw)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
            Pack(ref singleVector, out packedXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedXyzw, out Fixed32Precision.Vector4 realXyzw)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedXyzw, out singleVector);
            realXyzw = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
        }
    }

    /// <summary>
    /// todo
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct NormalisedShort2 
        : IPackedValue<UInt32>
        , IEquatable<NormalisedShort2>
        , IPackedReal2
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref SinglePrecision.Vector2 realXy, out UInt32 packedXy)
        {
            if (realXy.X < -1f || realXy.X > 1f ||
                realXy.Y < -1f || realXy.Y > 1f ) 
                throw new ArgumentException ("A component of the input source is not normalised: " + realXy);

            UInt32 x = PackUtils.PackSignedNormalised(0xffff, realXy.X);
            UInt32 y = PackUtils.PackSignedNormalised(0xffff, realXy.Y) << 16;
            packedXy = (x | y);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedXy, out SinglePrecision.Vector2 realXy)
        {
            realXy.X = PackUtils.UnpackSignedNormalised (0xffff, packedXy);
            realXy.Y = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) (packedXy >> 16));

            if (realXy.X < -1f || realXy.X > 1f ||
                realXy.Y < -1f || realXy.Y > 1f ) 
                throw new Exception ("A the input source doesn't yeild a normalised output: " + packedXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt32 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is NormalisedShort2) && this.Equals((NormalisedShort2)obj));
        }

        #region IEquatable<NormalisedShort2>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(NormalisedShort2 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(NormalisedShort2 a, NormalisedShort2 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(NormalisedShort2 a, NormalisedShort2 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public NormalisedShort2(ref SinglePrecision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector2 realXy)
        {
            Unpack(this.packedValue, out realXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        public NormalisedShort2(ref DoublePrecision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector2 realXy)
        {
            Unpack(this.packedValue, out realXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        public NormalisedShort2(ref Fixed32Precision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector2 realXy)
        {
            Unpack(this.packedValue, out realXy);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector2 realXy, out UInt32 packedXy)
        {
            SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
            Pack(ref singleVector, out packedXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedXy, out DoublePrecision.Vector2 realXy)
        {
            SinglePrecision.Vector2 singleVector;
            Unpack(packedXy, out singleVector);
            realXy = new DoublePrecision.Vector2((Double)singleVector.X, (Double)singleVector.Y);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector2 realXy, out UInt32 packedXy)
        {
            SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
            Pack(ref singleVector, out packedXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedXy, out Fixed32Precision.Vector2 realXy)
        {
            SinglePrecision.Vector2 singleVector;
            Unpack(packedXy, out singleVector);
            realXy = new Fixed32Precision.Vector2((Fixed32)singleVector.X, (Fixed32)singleVector.Y);
        }
    }

    /// <summary>
    /// todo
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct NormalisedShort4 
        : IPackedValue<UInt64>
        , IEquatable<NormalisedShort4>
        , IPackedReal4
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref SinglePrecision.Vector4 realXyzw, out UInt64 packedXyzw)
        {
            if (realXyzw.X < -1f || realXyzw.X > 1f ||
                realXyzw.Y < -1f || realXyzw.Y > 1f ||
                realXyzw.Z < -1f || realXyzw.Z > 1f ||
                realXyzw.W < -1f || realXyzw.W > 1f ) 
                throw new ArgumentException ("A component of the input source is not normalised: " + realXyzw);

            UInt64 x = (UInt64) PackUtils.PackSignedNormalised(0xffff, realXyzw.X);
            UInt64 y = ((UInt64) PackUtils.PackSignedNormalised(0xffff, realXyzw.Y)) << 16;
            UInt64 z = ((UInt64) PackUtils.PackSignedNormalised(0xffff, realXyzw.Z)) << 32;
            UInt64 w = ((UInt64) PackUtils.PackSignedNormalised(0xffff, realXyzw.W)) << 48;
            packedXyzw = (((x | y) | z) | w);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt64 packedXyzw, out SinglePrecision.Vector4 realXyzw)
        {
            realXyzw.X = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) packedXyzw);
            realXyzw.Y = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) (packedXyzw >> 16));
            realXyzw.Z = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) (packedXyzw >> 32));
            realXyzw.W = PackUtils.UnpackSignedNormalised (0xffff, (UInt32) (packedXyzw >> 48));

            if (realXyzw.X < -1f || realXyzw.X > 1f ||
                realXyzw.Y < -1f || realXyzw.Y > 1f ||
                realXyzw.Z < -1f || realXyzw.Z > 1f ||
                realXyzw.W < -1f || realXyzw.W > 1f ) 
                throw new Exception ("A the input source doesn't yeild a normalised output: " + packedXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt64 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is NormalisedShort4) && this.Equals((NormalisedShort4)obj));
        }

        #region IEquatable<NormalisedShort4>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(NormalisedShort4 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(NormalisedShort4 a, NormalisedShort4 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(NormalisedShort4 a, NormalisedShort4 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public NormalisedShort4(ref SinglePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector4 realXyzw)
        {
            Unpack(this.packedValue, out realXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        public NormalisedShort4(ref DoublePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector4 realXyzw)
        {
            Unpack(this.packedValue, out realXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        public NormalisedShort4(ref Fixed32Precision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector4 realXyzw)
        {
            Unpack(this.packedValue, out realXyzw);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector4 realXyzw, out UInt64 packedXyzw)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
            Pack(ref singleVector, out packedXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt64 packedXyzw, out DoublePrecision.Vector4 realXyzw)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedXyzw, out singleVector);
            realXyzw = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector4 realXyzw, out UInt64 packedXyzw)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
            Pack(ref singleVector, out packedXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt64 packedXyzw, out Fixed32Precision.Vector4 realXyzw)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedXyzw, out singleVector);
            realXyzw = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
        }
    }

    /// <summary>
    /// todo
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Rg32 
        : IPackedValue<UInt32>
        , IEquatable<Rg32>
        , IPackedReal2
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref SinglePrecision.Vector2 realRg, out UInt32 packedRg)
        {
            if (realRg.X < -1f || realRg.X > 1f ||
                realRg.Y < -1f || realRg.Y > 1f ) 
                throw new ArgumentException ("A component of the input source is not normalised: " + realRg);

            UInt32 x = PackUtils.PackUnsignedNormalisedValue(0xffff, realRg.X);
            UInt32 y = PackUtils.PackUnsignedNormalisedValue(0xffff, realRg.Y) << 16;
            packedRg = (x | y);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedRg, out SinglePrecision.Vector2 realRg)
        {
            realRg.X = PackUtils.UnpackUnsignedNormalisedValue (0xffff, packedRg);
            realRg.Y = PackUtils.UnpackUnsignedNormalisedValue (0xffff, (UInt32) (packedRg >> 16));

            if (realRg.X < -1f || realRg.X > 1f ||
                realRg.Y < -1f || realRg.Y > 1f ) 
                throw new Exception ("A the input source doesn't yeild a normalised output: " + packedRg);
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt32 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is Rg32) && this.Equals((Rg32)obj));
        }

        #region IEquatable<Rg32>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(Rg32 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(Rg32 a, Rg32 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(Rg32 a, Rg32 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public Rg32(ref SinglePrecision.Vector2 realRg)
        {
            Pack(ref realRg, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector2 realRg)
        {
            Pack(ref realRg, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector2 realRg)
        {
            Unpack(this.packedValue, out realRg);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Rg32(ref DoublePrecision.Vector2 realRg)
        {
            Pack(ref realRg, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector2 realRg)
        {
            Pack(ref realRg, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector2 realRg)
        {
            Unpack(this.packedValue, out realRg);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Rg32(ref Fixed32Precision.Vector2 realRg)
        {
            Pack(ref realRg, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector2 realRg)
        {
            Pack(ref realRg, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector2 realRg)
        {
            Unpack(this.packedValue, out realRg);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector2 realRg, out UInt32 packedRg)
        {
            SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realRg.X, (Single)realRg.Y);
            Pack(ref singleVector, out packedRg);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedRg, out DoublePrecision.Vector2 realRg)
        {
            SinglePrecision.Vector2 singleVector;
            Unpack(packedRg, out singleVector);
            realRg = new DoublePrecision.Vector2((Double)singleVector.X, (Double)singleVector.Y);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector2 realRg, out UInt32 packedRg)
        {
            SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realRg.X, (Single)realRg.Y);
            Pack(ref singleVector, out packedRg);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedRg, out Fixed32Precision.Vector2 realRg)
        {
            SinglePrecision.Vector2 singleVector;
            Unpack(packedRg, out singleVector);
            realRg = new Fixed32Precision.Vector2((Fixed32)singleVector.X, (Fixed32)singleVector.Y);
        }
    }
    
    /// <summary>
    /// todo
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public partial struct Rgba32
        : IPackedValue<UInt32>
        , IEquatable<Rgba32>
        , IPackedReal4
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return string.Format ("{{R:{0} G:{1} B:{2} A:{3}}}", new Object[] { this.R, this.G, this.B, this.A });
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref SinglePrecision.Vector4 realRgba, out UInt32 packedRgba)
        {
            if (realRgba.X < 0f || realRgba.X > 1f ||
                realRgba.Y < 0f || realRgba.Y > 1f ||
                realRgba.Z < 0f || realRgba.Z > 1f ||
                realRgba.W < 0f || realRgba.W > 1f ) 
                throw new ArgumentException ("A component of the input source is not unsigned and normalised: " + realRgba);

            UInt32 r = PackUtils.PackUnsignedNormalisedValue (0xff, realRgba.X);
            UInt32 g = PackUtils.PackUnsignedNormalisedValue (0xff, realRgba.Y) << 8;
            UInt32 b = PackUtils.PackUnsignedNormalisedValue (0xff, realRgba.Z) << 16;
            UInt32 a = PackUtils.PackUnsignedNormalisedValue (0xff, realRgba.W) << 24;
            packedRgba = ((r | g) | b) | a;
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedRgba, out SinglePrecision.Vector4 realRgba)
        {
            realRgba.X = PackUtils.UnpackUnsignedNormalisedValue (0xff, packedRgba);
            realRgba.Y = PackUtils.UnpackUnsignedNormalisedValue (0xff, (UInt32)(packedRgba >> 8));
            realRgba.Z = PackUtils.UnpackUnsignedNormalisedValue (0xff, (UInt32)(packedRgba >> 16));
            realRgba.W = PackUtils.UnpackUnsignedNormalisedValue (0xff, (UInt32)(packedRgba >> 24));

            if (realRgba.X < 0f || realRgba.X > 1f ||
                realRgba.Y < 0f || realRgba.Y > 1f ||
                realRgba.Z < 0f || realRgba.Z > 1f ||
                realRgba.W < 0f || realRgba.W > 1f ) 
                throw new Exception ("A the input source doesn't yeild an unsigned normalised output: " + packedRgba);
        }

        /// <summary>
        /// Transparent
        /// </summary>
        public static Rgba32 Transparent
        {
            get { return new Rgba32 (0); }
        }

        /// <summary>
        /// AliceBlue
        /// </summary>
        public static Rgba32 AliceBlue
        {
            get { return new Rgba32 (4294965488); }
        }

        /// <summary>
        /// AntiqueWhite
        /// </summary>
        public static Rgba32 AntiqueWhite
        {
            get { return new Rgba32 (4292340730); }
        }

        /// <summary>
        /// Aqua
        /// </summary>
        public static Rgba32 Aqua
        {
            get { return new Rgba32 (4294967040); }
        }

        /// <summary>
        /// Aquamarine
        /// </summary>
        public static Rgba32 Aquamarine
        {
            get { return new Rgba32 (4292149119); }
        }

        /// <summary>
        /// Azure
        /// </summary>
        public static Rgba32 Azure
        {
            get { return new Rgba32 (4294967280); }
        }

        /// <summary>
        /// Beige
        /// </summary>
        public static Rgba32 Beige
        {
            get { return new Rgba32 (4292670965); }
        }

        /// <summary>
        /// Bisque
        /// </summary>
        public static Rgba32 Bisque
        {
            get { return new Rgba32 (4291093759); }
        }

        /// <summary>
        /// Black
        /// </summary>
        public static Rgba32 Black
        {
            get { return new Rgba32 (4278190080); }
        }

        /// <summary>
        /// BlanchedAlmond
        /// </summary>
        public static Rgba32 BlanchedAlmond
        {
            get { return new Rgba32 (4291685375); }
        }

        /// <summary>
        /// Blue
        /// </summary>
        public static Rgba32 Blue
        {
            get { return new Rgba32 (4294901760); }
        }

        /// <summary>
        /// BlueViolet
        /// </summary>
        public static Rgba32 BlueViolet
        {
            get { return new Rgba32 (4293012362); }
        }

        /// <summary>
        /// Brown
        /// </summary>
        public static Rgba32 Brown
        {
            get { return new Rgba32 (4280953509); }
        }

        /// <summary>
        /// BurlyWood
        /// </summary>
        public static Rgba32 BurlyWood
        {
            get { return new Rgba32 (4287084766); }
        }

        /// <summary>
        /// CadetBlue
        /// </summary>
        public static Rgba32 CadetBlue
        {
            get { return new Rgba32 (4288716383); }
        }

        /// <summary>
        /// Chartreuse
        /// </summary>
        public static Rgba32 Chartreuse
        {
            get { return new Rgba32 (4278255487); }
        }

        /// <summary>
        /// Chocolate
        /// </summary>
        public static Rgba32 Chocolate
        {
            get { return new Rgba32 (4280183250); }
        }

        /// <summary>
        /// Coral
        /// </summary>
        public static Rgba32 Coral
        {
            get { return new Rgba32 (4283465727); }
        }

        /// <summary>
        /// CornflowerBlue
        /// </summary>
        public static Rgba32 CornflowerBlue
        {
            get { return new Rgba32 (4293760356); }
        }

        /// <summary>
        /// Cornsilk
        /// </summary>
        public static Rgba32 Cornsilk
        {
            get { return new Rgba32 (4292671743); }
        }

        /// <summary>
        /// Crimson
        /// </summary>
        public static Rgba32 Crimson
        {
            get { return new Rgba32 (4282127580); }
        }

        /// <summary>
        /// Cyan
        /// </summary>
        public static Rgba32 Cyan
        {
            get { return new Rgba32 (4294967040); }
        }

        /// <summary>
        /// DarkBlue
        /// </summary>
        public static Rgba32 DarkBlue
        {
            get { return new Rgba32 (4287299584); }
        }

        /// <summary>
        /// DarkCyan
        /// </summary>
        public static Rgba32 DarkCyan
        {
            get { return new Rgba32 (4287335168); }
        }

        /// <summary>
        /// DarkGoldenrod
        /// </summary>
        public static Rgba32 DarkGoldenrod
        {
            get { return new Rgba32 (4278945464); }
        }

        /// <summary>
        /// DarkGrey
        /// </summary>
        public static Rgba32 DarkGrey
        {
            get { return new Rgba32 (4289309097); }
        }

        /// <summary>
        /// DarkGreen
        /// </summary>
        public static Rgba32 DarkGreen
        {
            get { return new Rgba32 (4278215680); }
        }

        /// <summary>
        /// DarkKhaki
        /// </summary>
        public static Rgba32 DarkKhaki
        {
            get { return new Rgba32 (4285249469); }
        }

        /// <summary>
        /// DarkMagenta
        /// </summary>
        public static Rgba32 DarkMagenta
        {
            get { return new Rgba32 (4287299723); }
        }

        /// <summary>
        /// DarkOliveGreen
        /// </summary>
        public static Rgba32 DarkOliveGreen
        {
            get { return new Rgba32 (4281297749); }
        }

        /// <summary>
        /// DarkOrange
        /// </summary>
        public static Rgba32 DarkOrange
        {
            get { return new Rgba32 (4278226175); }
        }

        /// <summary>
        /// DarkOrchid
        /// </summary>
        public static Rgba32 DarkOrchid
        {
            get { return new Rgba32 (4291572377); }
        }

        /// <summary>
        /// DarkRed
        /// </summary>
        public static Rgba32 DarkRed
        {
            get { return new Rgba32 (4278190219); }
        }

        /// <summary>
        /// DarkSalmon
        /// </summary>
        public static Rgba32 DarkSalmon
        {
            get { return new Rgba32 (4286224105); }
        }

        /// <summary>
        /// DarkSeaGreen
        /// </summary>
        public static Rgba32 DarkSeaGreen
        {
            get { return new Rgba32 (4287347855); }
        }

        /// <summary>
        /// DarkSlateBlue
        /// </summary>
        public static Rgba32 DarkSlateBlue
        {
            get { return new Rgba32 (4287315272); }
        }

        /// <summary>
        /// DarkSlateGrey
        /// </summary>
        public static Rgba32 DarkSlateGrey
        {
            get { return new Rgba32 (4283387695); }
        }

        /// <summary>
        /// DarkTurquoise
        /// </summary>
        public static Rgba32 DarkTurquoise
        {
            get { return new Rgba32 (4291939840); }
        }

        /// <summary>
        /// DarkViolet
        /// </summary>
        public static Rgba32 DarkViolet
        {
            get { return new Rgba32 (4292018324); }
        }

        /// <summary>
        /// DeepPink
        /// </summary>
        public static Rgba32 DeepPink
        {
            get { return new Rgba32 (4287829247); }
        }

        /// <summary>
        /// DeepSkyBlue
        /// </summary>
        public static Rgba32 DeepSkyBlue
        {
            get { return new Rgba32 (4294950656); }
        }

        /// <summary>
        /// DimGrey
        /// </summary>
        public static Rgba32 DimGrey
        {
            get { return new Rgba32 (4285098345); }
        }

        /// <summary>
        /// DodgerBlue
        /// </summary>
        public static Rgba32 DodgerBlue
        {
            get { return new Rgba32 (4294938654); }
        }

        /// <summary>
        /// Firebrick
        /// </summary>
        public static Rgba32 Firebrick
        {
            get { return new Rgba32 (4280427186); }
        }

        /// <summary>
        /// FloralWhite
        /// </summary>
        public static Rgba32 FloralWhite
        {
            get { return new Rgba32 (4293982975); }
        }

        /// <summary>
        /// ForestGreen
        /// </summary>
        public static Rgba32 ForestGreen
        {
            get { return new Rgba32 (4280453922); }
        }

        /// <summary>
        /// Fuchsia
        /// </summary>
        public static Rgba32 Fuchsia
        {
            get { return new Rgba32 (4294902015); }
        }

        /// <summary>
        /// Gainsboro
        /// </summary>
        public static Rgba32 Gainsboro
        {
            get { return new Rgba32 (4292664540); }
        }

        /// <summary>
        /// GhostWhite
        /// </summary>
        public static Rgba32 GhostWhite
        {
            get { return new Rgba32 (4294965496); }
        }

        /// <summary>
        /// Gold
        /// </summary>
        public static Rgba32 Gold
        {
            get { return new Rgba32 (4278245375); }
        }

        /// <summary>
        /// Goldenrod
        /// </summary>
        public static Rgba32 Goldenrod
        {
            get { return new Rgba32 (4280329690); }
        }

        /// <summary>
        /// Grey
        /// </summary>
        public static Rgba32 Grey
        {
            get { return new Rgba32 (4286611584); }
        }

        /// <summary>
        /// Green
        /// </summary>
        public static Rgba32 Green
        {
            get { return new Rgba32 (4278222848); }
        }

        /// <summary>
        /// GreenYellow
        /// </summary>
        public static Rgba32 GreenYellow
        {
            get { return new Rgba32 (4281335725); }
        }

        /// <summary>
        /// Honeydew
        /// </summary>
        public static Rgba32 Honeydew
        {
            get { return new Rgba32 (4293984240); }
        }

        /// <summary>
        /// HotPink
        /// </summary>
        public static Rgba32 HotPink
        {
            get { return new Rgba32 (4290013695); }
        }

        /// <summary>
        /// IndianRed
        /// </summary>
        public static Rgba32 IndianRed
        {
            get { return new Rgba32 (4284243149); }
        }

        /// <summary>
        /// Indigo
        /// </summary>
        public static Rgba32 Indigo
        {
            get { return new Rgba32 (4286709835); }
        }

        /// <summary>
        /// Ivory
        /// </summary>
        public static Rgba32 Ivory
        {
            get { return new Rgba32 (4293984255); }
        }

        /// <summary>
        /// Khaki
        /// </summary>
        public static Rgba32 Khaki
        {
            get { return new Rgba32 (4287424240); }
        }

        /// <summary>
        /// Lavender
        /// </summary>
        public static Rgba32 Lavender
        {
            get { return new Rgba32 (4294633190); }
        }

        /// <summary>
        /// LavenderBlush
        /// </summary>
        public static Rgba32 LavenderBlush
        {
            get { return new Rgba32 (4294308095); }
        }

        /// <summary>
        /// LawnGreen
        /// </summary>
        public static Rgba32 LawnGreen
        {
            get { return new Rgba32 (4278254716); }
        }

        /// <summary>
        /// LemonChiffon
        /// </summary>
        public static Rgba32 LemonChiffon
        {
            get { return new Rgba32 (4291689215); }
        }

        /// <summary>
        /// LightBlue
        /// </summary>
        public static Rgba32 LightBlue
        {
            get { return new Rgba32 (4293318829); }
        }

        /// <summary>
        /// LightCoral
        /// </summary>
        public static Rgba32 LightCoral
        {
            get { return new Rgba32 (4286611696); }
        }

        /// <summary>
        /// LightCyan
        /// </summary>
        public static Rgba32 LightCyan
        {
            get { return new Rgba32 (4294967264); }
        }

        /// <summary>
        /// LightGoldenrodYellow
        /// </summary>
        public static Rgba32 LightGoldenrodYellow
        {
            get { return new Rgba32 (4292016890); }
        }

        /// <summary>
        /// LightGreen
        /// </summary>
        public static Rgba32 LightGreen
        {
            get { return new Rgba32 (4287688336); }
        }

        /// <summary>
        /// LightGrey
        /// </summary>
        public static Rgba32 LightGrey
        {
            get { return new Rgba32 (4292072403); }
        }

        /// <summary>
        /// LightPink
        /// </summary>
        public static Rgba32 LightPink
        {
            get { return new Rgba32 (4290885375); }
        }

        /// <summary>
        /// LightSalmon
        /// </summary>
        public static Rgba32 LightSalmon
        {
            get { return new Rgba32 (4286226687); }
        }

        /// <summary>
        /// LightSeaGreen
        /// </summary>
        public static Rgba32 LightSeaGreen
        {
            get { return new Rgba32 (4289376800); }
        }

        /// <summary>
        /// LightSkyBlue
        /// </summary>
        public static Rgba32 LightSkyBlue
        {
            get { return new Rgba32 (4294626951); }
        }

        /// <summary>
        /// LightSlateGrey
        /// </summary>
        public static Rgba32 LightSlateGrey
        {
            get { return new Rgba32 (4288252023); }
        }

        /// <summary>
        /// LightSteelBlue
        /// </summary>
        public static Rgba32 LightSteelBlue
        {
            get { return new Rgba32 (4292789424); }
        }

        /// <summary>
        /// LightYellow
        /// </summary>
        public static Rgba32 LightYellow
        {
            get { return new Rgba32 (4292935679); }
        }

        /// <summary>
        /// Lime
        /// </summary>
        public static Rgba32 Lime
        {
            get { return new Rgba32 (4278255360); }
        }

        /// <summary>
        /// LimeGreen
        /// </summary>
        public static Rgba32 LimeGreen
        {
            get { return new Rgba32 (4281519410); }
        }

        /// <summary>
        /// Linen
        /// </summary>
        public static Rgba32 Linen
        {
            get { return new Rgba32 (4293325050); }
        }

        /// <summary>
        /// Magenta
        /// </summary>
        public static Rgba32 Magenta
        {
            get { return new Rgba32 (4294902015); }
        }

        /// <summary>
        /// Maroon
        /// </summary>
        public static Rgba32 Maroon
        {
            get { return new Rgba32 (4278190208); }
        }

        /// <summary>
        /// MediumAquamarine
        /// </summary>
        public static Rgba32 MediumAquamarine
        {
            get { return new Rgba32 (4289383782); }
        }

        /// <summary>
        /// MediumBlue
        /// </summary>
        public static Rgba32 MediumBlue
        {
            get { return new Rgba32 (4291624960); }
        }

        /// <summary>
        /// MediumOrchid
        /// </summary>
        public static Rgba32 MediumOrchid
        {
            get { return new Rgba32 (4292040122); }
        }

        /// <summary>
        /// MediumPurple
        /// </summary>
        public static Rgba32 MediumPurple
        {
            get { return new Rgba32 (4292571283); }
        }

        /// <summary>
        /// MediumSeaGreen
        /// </summary>
        public static Rgba32 MediumSeaGreen
        {
            get { return new Rgba32 (4285641532); }
        }

        /// <summary>
        /// MediumSlateBlue
        /// </summary>
        public static Rgba32 MediumSlateBlue
        {
            get { return new Rgba32 (4293814395); }
        }

        /// <summary>
        /// MediumSpringGreen
        /// </summary>
        public static Rgba32 MediumSpringGreen
        {
            get { return new Rgba32 (4288346624); }
        }

        /// <summary>
        /// MediumTurquoise
        /// </summary>
        public static Rgba32 MediumTurquoise
        {
            get { return new Rgba32 (4291613000); }
        }

        /// <summary>
        /// MediumVioletRed
        /// </summary>
        public static Rgba32 MediumVioletRed
        {
            get { return new Rgba32 (4286911943); }
        }

        /// <summary>
        /// MidnightBlue
        /// </summary>
        public static Rgba32 MidnightBlue
        {
            get { return new Rgba32 (4285536537); }
        }

        /// <summary>
        /// MintCream
        /// </summary>
        public static Rgba32 MintCream
        {
            get { return new Rgba32 (4294639605); }
        }

        /// <summary>
        /// MistyRose
        /// </summary>
        public static Rgba32 MistyRose
        {
            get { return new Rgba32 (4292994303); }
        }

        /// <summary>
        /// Moccasin
        /// </summary>
        public static Rgba32 Moccasin
        {
            get { return new Rgba32 (4290110719); }
        }

        /// <summary>
        /// NavajoWhite
        /// </summary>
        public static Rgba32 NavajoWhite
        {
            get { return new Rgba32 (4289584895); }
        }

        /// <summary>
        /// Navy
        /// </summary>
        public static Rgba32 Navy
        {
            get { return new Rgba32 (4286578688); }
        }

        /// <summary>
        /// OldLace
        /// </summary>
        public static Rgba32 OldLace
        {
            get { return new Rgba32 (4293326333); }
        }

        /// <summary>
        /// Olive
        /// </summary>
        public static Rgba32 Olive
        {
            get { return new Rgba32 (4278222976); }
        }

        /// <summary>
        /// OliveDrab
        /// </summary>
        public static Rgba32 OliveDrab
        {
            get { return new Rgba32 (4280520299); }
        }

        /// <summary>
        /// Orange
        /// </summary>
        public static Rgba32 Orange
        {
            get { return new Rgba32 (4278232575); }
        }

        /// <summary>
        /// OrangeRed
        /// </summary>
        public static Rgba32 OrangeRed
        {
            get { return new Rgba32 (4278207999); }
        }

        /// <summary>
        /// Orchid
        /// </summary>
        public static Rgba32 Orchid
        {
            get { return new Rgba32 (4292243674); }
        }

        /// <summary>
        /// PaleGoldenrod
        /// </summary>
        public static Rgba32 PaleGoldenrod
        {
            get { return new Rgba32 (4289390830); }
        }

        /// <summary>
        /// PaleGreen
        /// </summary>
        public static Rgba32 PaleGreen
        {
            get { return new Rgba32 (4288215960); }
        }

        /// <summary>
        /// PaleTurquoise
        /// </summary>
        public static Rgba32 PaleTurquoise
        {
            get { return new Rgba32 (4293848751); }
        }

        /// <summary>
        /// PaleVioletRed
        /// </summary>
        public static Rgba32 PaleVioletRed
        {
            get { return new Rgba32 (4287852763); }
        }

        /// <summary>
        /// PapayaWhip
        /// </summary>
        public static Rgba32 PapayaWhip
        {
            get { return new Rgba32 (4292210687); }
        }

        /// <summary>
        /// PeachPuff
        /// </summary>
        public static Rgba32 PeachPuff
        {
            get { return new Rgba32 (4290370303); }
        }

        /// <summary>
        /// Peru
        /// </summary>
        public static Rgba32 Peru
        {
            get { return new Rgba32 (4282353101); }
        }

        /// <summary>
        /// Pink
        /// </summary>
        public static Rgba32 Pink
        {
            get { return new Rgba32 (4291543295); }
        }

        /// <summary>
        /// Plum
        /// </summary>
        public static Rgba32 Plum
        {
            get { return new Rgba32 (4292714717); }
        }

        /// <summary>
        /// PowderBlue
        /// </summary>
        public static Rgba32 PowderBlue
        {
            get { return new Rgba32 (4293320880); }
        }

        /// <summary>
        /// Purple
        /// </summary>
        public static Rgba32 Purple
        {
            get { return new Rgba32 (4286578816); }
        }

        /// <summary>
        /// Red
        /// </summary>
        public static Rgba32 Red
        {
            get { return new Rgba32 (4278190335); }
        }

        /// <summary>
        /// RosyBrown
        /// </summary>
        public static Rgba32 RosyBrown
        {
            get { return new Rgba32 (4287598524); }
        }

        /// <summary>
        /// RoyalBlue
        /// </summary>
        public static Rgba32 RoyalBlue
        {
            get { return new Rgba32 (4292962625); }
        }

        /// <summary>
        /// SaddleBrown
        /// </summary>
        public static Rgba32 SaddleBrown
        {
            get { return new Rgba32 (4279453067); }
        }

        /// <summary>
        /// Salmon
        /// </summary>
        public static Rgba32 Salmon
        {
            get { return new Rgba32 (4285694202); }
        }

        /// <summary>
        /// SandyBrown
        /// </summary>
        public static Rgba32 SandyBrown
        {
            get { return new Rgba32 (4284523764); }
        }

        /// <summary>
        /// SeaGreen
        /// </summary>
        public static Rgba32 SeaGreen
        {
            get { return new Rgba32 (4283927342); }
        }

        /// <summary>
        /// SeaShell
        /// </summary>
        public static Rgba32 SeaShell
        {
            get { return new Rgba32 (4293850623); }
        }

        /// <summary>
        /// Sienna
        /// </summary>
        public static Rgba32 Sienna
        {
            get { return new Rgba32 (4281160352); }
        }

        /// <summary>
        /// Silver
        /// </summary>
        public static Rgba32 Silver
        {
            get { return new Rgba32 (4290822336); }
        }

        /// <summary>
        /// SkyBlue
        /// </summary>
        public static Rgba32 SkyBlue
        {
            get { return new Rgba32 (4293643911); }
        }

        /// <summary>
        /// SlateBlue
        /// </summary>
        public static Rgba32 SlateBlue
        {
            get { return new Rgba32 (4291648106); }
        }

        /// <summary>
        /// SlateGrey
        /// </summary>
        public static Rgba32 SlateGrey
        {
            get { return new Rgba32 (4287660144); }
        }

        /// <summary>
        /// Snow
        /// </summary>
        public static Rgba32 Snow
        {
            get { return new Rgba32 (4294638335); }
        }

        /// <summary>
        /// SpringGreen
        /// </summary>
        public static Rgba32 SpringGreen
        {
            get { return new Rgba32 (4286578432); }
        }

        /// <summary>
        /// SteelBlue
        /// </summary>
        public static Rgba32 SteelBlue
        {
            get { return new Rgba32 (4290019910); }
        }

        /// <summary>
        /// Tan
        /// </summary>
        public static Rgba32 Tan
        {
            get { return new Rgba32 (4287411410); }
        }

        /// <summary>
        /// Teal
        /// </summary>
        public static Rgba32 Teal
        {
            get { return new Rgba32 (4286611456); }
        }

        /// <summary>
        /// Thistle
        /// </summary>
        public static Rgba32 Thistle
        {
            get { return new Rgba32 (4292394968); }
        }

        /// <summary>
        /// Tomato
        /// </summary>
        public static Rgba32 Tomato
        {
            get { return new Rgba32 (4282868735); }
        }

        /// <summary>
        /// Turquoise
        /// </summary>
        public static Rgba32 Turquoise
        {
            get { return new Rgba32 (4291878976); }
        }

        /// <summary>
        /// Violet
        /// </summary>
        public static Rgba32 Violet
        {
            get { return new Rgba32 (4293821166); }
        }

        /// <summary>
        /// Wheat
        /// </summary>
        public static Rgba32 Wheat
        {
            get { return new Rgba32 (4289978101); }
        }

        /// <summary>
        /// White
        /// </summary>
        public static Rgba32 White
        {
            get { return new Rgba32 (4294967295); }
        }

        /// <summary>
        /// WhiteSmoke
        /// </summary>
        public static Rgba32 WhiteSmoke
        {
            get { return new Rgba32 (4294309365); }
        }

        /// <summary>
        /// Yellow
        /// </summary>
        public static Rgba32 Yellow
        {
            get { return new Rgba32 (4278255615); }
        }

        /// <summary>
        /// YellowGreen
        /// </summary>
        public static Rgba32 YellowGreen
        {
            get { return new Rgba32 (4281519514); }
        }


        /// <summary>
        /// todo
        /// </summary>
        UInt32 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is Rgba32) && this.Equals((Rgba32)obj));
        }

        #region IEquatable<Rgba32>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(Rgba32 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(Rgba32 a, Rgba32 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(Rgba32 a, Rgba32 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public Rgba32(ref SinglePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Rgba32(ref DoublePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Rgba32(ref Fixed32Precision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector4 realRgba, out UInt32 packedRgba)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
            Pack(ref singleVector, out packedRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedRgba, out DoublePrecision.Vector4 realRgba)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedRgba, out singleVector);
            realRgba = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector4 realRgba, out UInt32 packedRgba)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
            Pack(ref singleVector, out packedRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedRgba, out Fixed32Precision.Vector4 realRgba)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedRgba, out singleVector);
            realRgba = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Byte R
        {
            get { return unchecked((Byte)this.packedValue); }
            set { this.packedValue = (this.packedValue & 0xffffff00) | value; }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Byte G
        {
            get { return unchecked((Byte)(this.packedValue >> 8)); }
            set { this.packedValue = (this.packedValue & 0xffff00ff) | ((UInt32)(value << 8)); }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Byte B
        {
            get { return unchecked((Byte)(this.packedValue >> 0x10)); }
            set { this.packedValue = (this.packedValue & 0xff00ffff) | ((UInt32)(value << 0x10)); }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Byte A
        {
            get { return unchecked((Byte)(this.packedValue >> 0x18)); }
            set { this.packedValue = (this.packedValue & 0xffffff) | ((UInt32)(value << 0x18)); }
        }

        /// <summary>
        /// todo
        /// </summary>
        Rgba32(UInt32 packedValue)
        {
            this.packedValue = packedValue;
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        public Rgba32 (Single r, Single g, Single b)
        {
            var val = new SinglePrecision.Vector4(r, g, b, 1f);
            Pack ( ref val, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Rgba32 (Single r, Single g, Single b, Single a)
        {
            var val = new SinglePrecision.Vector4(r, g, b, a);
            Pack(ref val, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Rgba32(SinglePrecision.Vector3 vector)
        {
            var val = new SinglePrecision.Vector4(vector.X, vector.Y, vector.Z, 1f);
            Pack(ref val, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Rgba32 FromNonPremultiplied(SinglePrecision.Vector4 vector)
        {
            Rgba32 color;
            var val = new SinglePrecision.Vector4(vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W, vector.W);
            Pack(ref val, out color.packedValue);
            return color;
        }
        /// <summary>
        /// todo
        /// </summary>
        public Rgba32(DoublePrecision.Vector3 vector)
        {
            var val = new DoublePrecision.Vector4(vector.X, vector.Y, vector.Z, 1f);
            Pack(ref val, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Rgba32 FromNonPremultiplied(DoublePrecision.Vector4 vector)
        {
            Rgba32 color;
            var val = new DoublePrecision.Vector4(vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W, vector.W);
            Pack(ref val, out color.packedValue);
            return color;
        }
        /// <summary>
        /// todo
        /// </summary>
        public Rgba32(Fixed32Precision.Vector3 vector)
        {
            var val = new Fixed32Precision.Vector4(vector.X, vector.Y, vector.Z, 1f);
            Pack(ref val, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Rgba32 FromNonPremultiplied(Fixed32Precision.Vector4 vector)
        {
            Rgba32 color;
            var val = new Fixed32Precision.Vector4(vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W, vector.W);
            Pack(ref val, out color.packedValue);
            return color;
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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
        

        /// <summary>
        /// todo
        /// </summary>
        public static Rgba32 Lerp(Rgba32 value1, Rgba32 value2, Single amount)
        {
            if (amount > 1f)
                throw new ArgumentException("Amount: " + amount + " must be <= 1.");
            if (amount < 0f)
                throw new ArgumentException("Amount: " + amount + " must be >= 0.");

            Rgba32 colour;
            UInt32 packedValue1 = value1.packedValue;
            UInt32 packedValue2 = value2.packedValue;

            Int32 r1 = (Byte) (packedValue1);
            Int32 g1 = (Byte) (packedValue1 >> 8);
            Int32 b1 = (Byte) (packedValue1 >> 0x10);
            Int32 a1 = (Byte) (packedValue1 >> 0x18);

            Int32 r2 = (Byte) (packedValue2);
            Int32 g2 = (Byte) (packedValue2 >> 8);
            Int32 b2 = (Byte) (packedValue2 >> 0x10);
            Int32 a2 = (Byte) (packedValue2 >> 0x18);

            Int32 num = (Int32) PackUtils.PackUnsignedNormalisedValue(65536f, amount);

            Int32 r = r1 + (((r2 - r1) * num) >> 0x10);
            Int32 g = g1 + (((g2 - g1) * num) >> 0x10);
            Int32 b = b1 + (((b2 - b1) * num) >> 0x10);
            Int32 a = a1 + (((a2 - a1) * num) >> 0x10);

            colour.packedValue = 
                (UInt32)(((r | (g << 8)) | (b << 0x10)) | (a << 0x18));
            
            return colour;
        }

        /// <summary>
        /// todo
        /// </summary>
        public void ToVector3(out SinglePrecision.Vector3 result)
        {
            SinglePrecision.Vector4 colourVec4;
            this.UnpackTo(out colourVec4);

            result = new SinglePrecision.Vector3(colourVec4.X, colourVec4.Y, colourVec4.Z);
        }
        /// <summary>
        /// todo
        /// </summary>
        public void ToVector3(out DoublePrecision.Vector3 result)
        {
            DoublePrecision.Vector4 colourVec4;
            this.UnpackTo(out colourVec4);

            result = new DoublePrecision.Vector3(colourVec4.X, colourVec4.Y, colourVec4.Z);
        }
        /// <summary>
        /// todo
        /// </summary>
        public void ToVector3(out Fixed32Precision.Vector3 result)
        {
            Fixed32Precision.Vector4 colourVec4;
            this.UnpackTo(out colourVec4);

            result = new Fixed32Precision.Vector3(colourVec4.X, colourVec4.Y, colourVec4.Z);
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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


    }

    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Rgba64
        : IPackedValue<UInt64>
        , IEquatable<Rgba64>
        , IPackedReal4
    {
        public override String ToString ()
        {
            return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
        }

        static void Pack(ref SinglePrecision.Vector4 realRgba, out UInt64 packedRgba)
        {
            if (realRgba.X < 0f || realRgba.X > 1f ||
                realRgba.Y < 0f || realRgba.Y > 1f ||
                realRgba.Z < 0f || realRgba.Z > 1f ||
                realRgba.W < 0f || realRgba.W > 1f )
                throw new ArgumentException ("A component of the input source is not unsigned and normalised: " + realRgba);

            UInt64 r = (UInt64) PackUtils.PackUnsignedNormalisedValue(0xffff, realRgba.X);
            UInt64 g = ((UInt64) PackUtils.PackUnsignedNormalisedValue(0xffff, realRgba.Y)) << 16;
            UInt64 b = ((UInt64) PackUtils.PackUnsignedNormalisedValue(0xffff, realRgba.Z)) << 32;
            UInt64 a = ((UInt64) PackUtils.PackUnsignedNormalisedValue(0xffff, realRgba.W)) << 48;
            packedRgba = (((r | g) | b) | a);
        }

        static void Unpack(UInt64 packedRgba, out SinglePrecision.Vector4 realRgba)
        {
            realRgba.X = PackUtils.UnpackUnsignedNormalisedValue (0xffff, (UInt32) packedRgba);
            realRgba.Y = PackUtils.UnpackUnsignedNormalisedValue (0xffff, (UInt32) (packedRgba >> 16));
            realRgba.Z = PackUtils.UnpackUnsignedNormalisedValue (0xffff, (UInt32) (packedRgba >> 32));
            realRgba.W = PackUtils.UnpackUnsignedNormalisedValue (0xffff, (UInt32) (packedRgba >> 48));

            if (realRgba.X < 0f || realRgba.X > 1f ||
                realRgba.Y < 0f || realRgba.Y > 1f ||
                realRgba.Z < 0f || realRgba.Z > 1f ||
                realRgba.W < 0f || realRgba.W > 1f )
                throw new Exception ("A the input source doesn't yeild a unsigned normalised output: " + packedRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt64 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is Rgba64) && this.Equals((Rgba64)obj));
        }

        #region IEquatable<Rgba64>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(Rgba64 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(Rgba64 a, Rgba64 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(Rgba64 a, Rgba64 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public Rgba64(ref SinglePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Rgba64(ref DoublePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Rgba64(ref Fixed32Precision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector4 realRgba, out UInt64 packedRgba)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
            Pack(ref singleVector, out packedRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt64 packedRgba, out DoublePrecision.Vector4 realRgba)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedRgba, out singleVector);
            realRgba = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector4 realRgba, out UInt64 packedRgba)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
            Pack(ref singleVector, out packedRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt64 packedRgba, out Fixed32Precision.Vector4 realRgba)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedRgba, out singleVector);
            realRgba = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
        }
    }

    // 2 bit alpha
    [StructLayout (LayoutKind.Sequential), Serializable]
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
            if (realRgba.X < 0f || realRgba.X > 1f ||
                realRgba.Y < 0f || realRgba.Y > 1f ||
                realRgba.Z < 0f || realRgba.Z > 1f ||
                realRgba.W < 0f || realRgba.W > 1f ) 
                throw new ArgumentException ("A component of the input source is not unsigned and normalised: " + realRgba);

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

            if (realRgba.X < 0f || realRgba.X > 1f ||
                realRgba.Y < 0f || realRgba.Y > 1f ||
                realRgba.Z < 0f || realRgba.Z > 1f ||
                realRgba.W < 0f || realRgba.W > 1f ) 
                throw new Exception ("A the input source doesn't yeild an unsigned normalised output: " + packedRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt32 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is Rgba_10_10_10_2) && this.Equals((Rgba_10_10_10_2)obj));
        }

        #region IEquatable<Rgba_10_10_10_2>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(Rgba_10_10_10_2 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(Rgba_10_10_10_2 a, Rgba_10_10_10_2 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(Rgba_10_10_10_2 a, Rgba_10_10_10_2 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public Rgba_10_10_10_2(ref SinglePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Rgba_10_10_10_2(ref DoublePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Rgba_10_10_10_2(ref Fixed32Precision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector4 realRgba)
        {
            Pack(ref realRgba, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector4 realRgba)
        {
            Unpack(this.packedValue, out realRgba);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector4 realRgba, out UInt32 packedRgba)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
            Pack(ref singleVector, out packedRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedRgba, out DoublePrecision.Vector4 realRgba)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedRgba, out singleVector);
            realRgba = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector4 realRgba, out UInt32 packedRgba)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realRgba.X, (Single)realRgba.Y, (Single)realRgba.Z, (Single)realRgba.W);
            Pack(ref singleVector, out packedRgba);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedRgba, out Fixed32Precision.Vector4 realRgba)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedRgba, out singleVector);
            realRgba = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
        }
    }

    /// <summary>
    /// todo
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Short2 
        : IPackedValue<UInt32>
        , IEquatable<Short2>
        , IPackedReal2
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref SinglePrecision.Vector2 realXy, out UInt32 packedXy)
        {
            UInt32 x = PackUtils.PackSigned (0xffff, realXy.X);
            UInt32 y = PackUtils.PackSigned (0xffff, realXy.Y) << 16;
            packedXy = (x | y);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedXy, out SinglePrecision.Vector2 realXy)
        {
            realXy.X = (Int16) packedXy;
            realXy.Y = (Int16) (packedXy >> 16);
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt32 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is Short2) && this.Equals((Short2)obj));
        }

        #region IEquatable<Short2>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(Short2 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(Short2 a, Short2 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(Short2 a, Short2 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public Short2(ref SinglePrecision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector2 realXy)
        {
            Unpack(this.packedValue, out realXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Short2(ref DoublePrecision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector2 realXy)
        {
            Unpack(this.packedValue, out realXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Short2(ref Fixed32Precision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector2 realXy)
        {
            Pack(ref realXy, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector2 realXy)
        {
            Unpack(this.packedValue, out realXy);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector2 realXy, out UInt32 packedXy)
        {
            SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
            Pack(ref singleVector, out packedXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedXy, out DoublePrecision.Vector2 realXy)
        {
            SinglePrecision.Vector2 singleVector;
            Unpack(packedXy, out singleVector);
            realXy = new DoublePrecision.Vector2((Double)singleVector.X, (Double)singleVector.Y);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector2 realXy, out UInt32 packedXy)
        {
            SinglePrecision.Vector2 singleVector = new SinglePrecision.Vector2((Single)realXy.X, (Single)realXy.Y);
            Pack(ref singleVector, out packedXy);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt32 packedXy, out Fixed32Precision.Vector2 realXy)
        {
            SinglePrecision.Vector2 singleVector;
            Unpack(packedXy, out singleVector);
            realXy = new Fixed32Precision.Vector2((Fixed32)singleVector.X, (Fixed32)singleVector.Y);
        }

    }
    
    /// <summary>
    /// todo
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Short4 
        : IPackedValue<UInt64>
        , IEquatable<Short4>
        , IPackedReal4
    {
        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return this.packedValue.ToString ("X8", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref SinglePrecision.Vector4 realXyzw, out UInt64 packedXyzw)
        {
            UInt64 x = (UInt64) PackUtils.PackSigned(0xffff, realXyzw.X);
            UInt64 y = ((UInt64) PackUtils.PackSigned(0xffff, realXyzw.Y)) << 16;
            UInt64 z = ((UInt64) PackUtils.PackSigned(0xffff, realXyzw.Z)) << 32;
            UInt64 w = ((UInt64) PackUtils.PackSigned(0xffff, realXyzw.W)) << 48;
            packedXyzw = (((x | y) | z) | w);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt64 packedXyzw, out SinglePrecision.Vector4 realXyzw)
        {
            realXyzw.X = ((Int16) packedXyzw);
            realXyzw.Y = ((Int16) (packedXyzw >> 16));
            realXyzw.Z = ((Int16) (packedXyzw >> 32));
            realXyzw.W = ((Int16) (packedXyzw >> 48));
        }

        /// <summary>
        /// todo
        /// </summary>
        UInt64 packedValue;

        #region IPackedValue

        /// <summary>
        /// todo
        /// </summary>
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

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return ((obj is Short4) && this.Equals((Short4)obj));
        }

        #region IEquatable<Short4>

        /// <summary>
        /// todo
        /// </summary>
        public Boolean Equals(Short4 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        #endregion

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator ==(Short4 a, Short4 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static Boolean operator !=(Short4 a, Short4 b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// todo
        /// </summary>
        public Short4(ref SinglePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref SinglePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out SinglePrecision.Vector4 realXyzw)
        {
            Unpack(this.packedValue, out realXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Short4(ref DoublePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref DoublePrecision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out DoublePrecision.Vector4 realXyzw)
        {
            Unpack(this.packedValue, out realXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        public Short4(ref Fixed32Precision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void PackFrom(ref Fixed32Precision.Vector4 realXyzw)
        {
            Pack(ref realXyzw, out this.packedValue);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void UnpackTo(out Fixed32Precision.Vector4 realXyzw)
        {
            Unpack(this.packedValue, out realXyzw);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref DoublePrecision.Vector4 realXyzw, out UInt64 packedXyzw)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
            Pack(ref singleVector, out packedXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt64 packedXyzw, out DoublePrecision.Vector4 realXyzw)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedXyzw, out singleVector);
            realXyzw = new DoublePrecision.Vector4((Double)singleVector.X, (Double)singleVector.Y, (Double)singleVector.Z, (Double)singleVector.W);
        }
        /// <summary>
        /// todo
        /// </summary>
        static void Pack(ref Fixed32Precision.Vector4 realXyzw, out UInt64 packedXyzw)
        {
            SinglePrecision.Vector4 singleVector = new SinglePrecision.Vector4((Single)realXyzw.X, (Single)realXyzw.Y, (Single)realXyzw.Z, (Single)realXyzw.W);
            Pack(ref singleVector, out packedXyzw);
        }

        /// <summary>
        /// todo
        /// </summary>
        static void Unpack(UInt64 packedXyzw, out Fixed32Precision.Vector4 realXyzw)
        {
            SinglePrecision.Vector4 singleVector;
            Unpack(packedXyzw, out singleVector);
            realXyzw = new Fixed32Precision.Vector4((Fixed32)singleVector.X, (Fixed32)singleVector.Y, (Fixed32)singleVector.Z, (Fixed32)singleVector.W);
        }

    }

}


namespace Abacus.Int32Precision
{
    /// <summary>
    /// Represents a Int32 precision point on a 2D integer grid.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Point2 
        : IEquatable<Point2>
    {
        /// <summary>
        /// Gets or sets the x-component of the point.
        /// </summary>
        public Int32 X;

        /// <summary>
        /// Gets or sets the y-component of the point.
        /// </summary>
        public Int32 Y;

        /// <summary>
        /// Initilises a new instance of Point2 from two Int32 values 
        /// representing X and Y respectively.
        /// </summary>
        public Point2 (Int32 x, Int32 y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return String.Format ("{{X:{0} Y:{1}}}", this.X, this.Y );
        }

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (this.X.GetHashCode () + this.Y.GetHashCode ());
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Point2 with all of its components set to zero.
        /// </summary>
        static Point2 zero;

        /// <summary>
        /// Defines a Point2 with all of its components set to one.
        /// </summary>
        static Point2 one;

        /// <summary>
        /// Defines the unit Point2 for the X-axis.
        /// </summary>
        static Point2 unitX;

        /// <summary>
        /// Defines the unit Point2 for the Y-axis.
        /// </summary>
        static Point2 unitY;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Point2()
        {
            zero = new Point2();
            one = new Point2(1, 1);
            unitX = new Point2(1, 0);
            unitY = new Point2(0, 1);
        }

        /// <summary>
        /// Returns a Point2 with all of its components set to zero.
        /// </summary>
        public static Point2 Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Returns a Point2 with all of its components set to one.
        /// </summary>
        public static Point2 One
        {
            get { return one; }
        }

        /// <summary>
        /// Returns the unit Point2 for the X-axis.
        /// </summary>
        public static Point2 UnitX
        {
            get { return unitX; }
        }

        /// <summary>
        /// Returns the unit Point2 for the Y-axis.
        /// </summary>
        public static Point2 UnitY
        {
            get { return unitY; }
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Point2 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Point2) {
                flag = this.Equals ((Point2)obj);
            }
            return flag;
        }

        #region IEquatable<Point2>

        /// <summary>
        /// Determines whether or not this Point2 object is equal to another
        /// Point2 object.
        /// </summary>
        public Boolean Equals (Point2 other)
        {
            return ((this.X == other.X) && (this.Y == other.Y));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Point2 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Point2 value1, Point2 value2)
        {
            return ((value1.X == value2.X) && (value1.Y == value2.Y));
        }

        /// <summary>
        /// Determines whether or not two Point2 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Point2 value1, Point2 value2)
        {
            if (value1.X == value2.X) {
                return !(value1.Y == value2.Y);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Point2 objects.
        /// </summary>
        public static void Add (
            ref Point2 value1, ref Point2 value2, out Point2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        /// <summary>
        /// Performs addition of two Point2 objects using the (X+Y) operator. 
        /// </summary>
        public static Point2 operator + (Point2 value1, Point2 value2)
        {
            Point2 point;
            point.X = value1.X + value2.X;
            point.Y = value1.Y + value2.Y;
            return point;
        }


        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Point2 objects.
        /// </summary>
        public static void Subtract (
            ref Point2 value1, ref Point2 value2, out Point2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }

        /// <summary>
        /// Performs subtraction of two Point2 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Point2 operator - (Point2 value1, Point2 value2)
        {
            Point2 point;
            point.X = value1.X - value2.X;
            point.Y = value1.Y - value2.Y;
            return point;
        }


        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Point2 object.
        /// </summary>
        public static void Negate (ref Point2 value, out Point2 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
        }

        /// <summary>
        /// Performs negation of a Point2 object using the (-X) operator.
        /// </summary>
        public static Point2 operator - (Point2 value)
        {
            Point2 point;
            point.X = -value.X;
            point.Y = -value.Y;
            return point;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Point2 objects.
        /// </summary>
        public static void Multiply (
            ref Point2 value1, ref Point2 value2, out Point2 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
        }

        /// <summary>
        /// Performs multiplication of a Point2 object and a Int32
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Point2 value, Int32 scaleFactor, out Point2 result)
        {
            result.X = value.X * scaleFactor;
            result.Y = value.Y * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Point2 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Point2 operator * (
            Point2 value1, Point2 value2)
        {
            Point2 point;
            point.X = value1.X * value2.X;
            point.Y = value1.Y * value2.Y;
            return point;
        }

        /// <summary>
        /// Performs multiplication of a Point2 object and a Int32
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Point2 operator * (
            Point2 value, Int32 scaleFactor)
        {
            Point2 point;
            point.X = value.X * scaleFactor;
            point.Y = value.Y * scaleFactor;
            return point;
        }

        /// <summary>
        /// Performs multiplication of a Int32 precision scaling factor 
        /// and aPoint2 object using the (x*Y) operator.
        /// </summary>
        public static Point2 operator * (
            Int32 scaleFactor, Point2 value)
        {
            Point2 point;
            point.X = value.X * scaleFactor;
            point.Y = value.Y * scaleFactor;
            return point;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Point2 objects.
        /// </summary>
        public static void Divide (
            ref Point2 value1, ref Point2 value2, out Point2 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
        }

        /// <summary>
        /// Performs division of a Point2 object and a Int32 precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Point2 value1, Int32 divider, out Point2 result)
        {
            Int32 one = 1;
            Int32 num = one / divider;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
        }

        /// <summary>
        /// Performs division of two Point2 objects using the (X/Y) operator.
        /// </summary>
        public static Point2 operator / (Point2 value1, Point2 value2)
        {
            Point2 point;
            point.X = value1.X / value2.X;
            point.Y = value1.Y / value2.Y;
            return point;
        }

        /// <summary>
        /// Performs division of a Point2 object and a Int32 precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Point2 operator / (Point2 value1, Int32 divider)
        {
            Point2 point;
            Int32 one = 1;
            Int32 num = one / divider;
            point.X = value1.X * num;
            point.Y = value1.Y * num;
            return point;
        }
        
    }

    /// <summary>
    /// Represents a Int32 precision point on a 3D integer grid.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Point3 
        : IEquatable<Point3>
    {
        /// <summary>
        /// Gets or sets the x-component of the point.
        /// </summary>
        public Int32 X;

        /// <summary>
        /// Gets or sets the y-component of the point.
        /// </summary>
        public Int32 Y;

        /// <summary>
        /// Gets or sets the z-component of the point.
        /// </summary>
        public Int32 Z;

        /// <summary>
        /// Initilises a new instance of Point3 from three Int32 values 
        /// representing X, Y and Z respectively.
        /// </summary>
        public Point3(Int32 x, Int32 y, Int32 z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        
        /// <summary>
        /// Initilises a new instance of Point3 from one Point2 value
        /// representing X and Y and one Int32 value representing Z.
        /// </summary>
        public Point3 (Point2 value, Int32 z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("{{X:{0} Y:{1} Z:{2}}}", this.X, this.Y, this.Z );
        }

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        public override Int32 GetHashCode()
        {
            return (this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode());
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Point3 with all of its components set to zero.
        /// </summary>
        static Point3 zero;

        /// <summary>
        /// Defines a Point3 with all of its components set to one.
        /// </summary>
        static Point3 one;

        /// <summary>
        /// Defines the unit Point3 for the X-axis.
        /// </summary>
        static Point3 unitX;

        /// <summary>
        /// Defines the unit Point3 for the Y-axis.
        /// </summary>
        static Point3 unitY;

        /// <summary>
        /// Defines the unit Point3 for the Z-axis.
        /// </summary>
        static Point3 unitZ;

        /// <summary>
        /// Defines a unit Point3 designating up (0, 1, 0).
        /// </summary>
        static Point3 up;

        /// <summary>
        /// Defines a unit Point3 designating down (0, −1, 0).
        /// </summary>
        static Point3 down;

        /// <summary>
        /// Defines a unit Point3 pointing to the right (1, 0, 0).
        /// </summary>
        static Point3 right;

        /// <summary>
        /// Defines a unit Point3 designating left (−1, 0, 0).
        /// </summary>
        static Point3 left;

        /// <summary>
        /// Defines a unit Point3 designating forward in a right-handed 
        /// coordinate system (0, 0, -1).
        /// </summary>
        static Point3 forward;

        /// <summary>
        /// Defines a unit Point3 designating backward in a right-handed 
        /// coordinate system (0, 0, 1).
        /// </summary>
        static Point3 backward;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
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

        /// <summary>
        /// Returns a Point3 with all of its components set to zero.
        /// </summary>
        public static Point3 Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Returns a Point3 with all of its components set to one.
        /// </summary>
        public static Point3 One
        {
            get { return one; }
        }

        /// <summary>
        /// Returns the unit Point3 for the X-axis.
        /// </summary>
        public static Point3 UnitX
        {
            get { return unitX; }
        }

        /// <summary>
        /// Returns the unit Point3 for the Y-axis.
        /// </summary>
        public static Point3 UnitY
        {
            get { return unitY; }
        }

        /// <summary>
        /// Returns the unit Point3 for the Z-axis.
        /// </summary>
        public static Point3 UnitZ
        {
            get { return unitZ; }
        }

        /// <summary>
        /// Returns a unit Point3 designating up (0, 1, 0).
        /// </summary>
        public static Point3 Up
        {
            get { return up; }
        }

        /// <summary>
        /// Returns a unit Point3 designating down (0, −1, 0).
        /// </summary>
        public static Point3 Down
        {
            get { return down; }
        }

        /// <summary>
        /// Returns a unit Point3 pointing to the right (1, 0, 0).
        /// </summary>
        public static Point3 Right
        {
            get { return right; }
        }

        /// <summary>
        /// Returns a unit Point3 designating left (−1, 0, 0).
        /// </summary>
        public static Point3 Left
        {
            get { return left; }
        }

        /// <summary>
        /// Returns a unit Point3 designating forward in a right-handed 
        /// coordinate system (0, 0, -1).
        /// </summary>
        public static Point3 Forward
        {
            get { return forward; }
        }

        /// <summary>
        /// Returns a unit Point3 designating backward in a right-handed 
        /// coordinate system (0, 0, 1).
        /// </summary>
        public static Point3 Backward
        {
            get { return backward; }
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Point3 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Point3) {
                flag = this.Equals ((Point3)obj);
            }
            return flag;
        }

        #region IEquatable<Point3>

        /// <summary>
        /// Determines whether or not this Point3 object is equal to another
        /// Point3 object.
        /// </summary>
        public Boolean Equals (Point3 other)
        {
            return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Point3 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Point3 value1, Point3 value2)
        {
            return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z));
        }

        /// <summary>
        /// Determines whether or not two Point3 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Point3 value1, Point3 value2)
        {
            if ((value1.X == value2.X) && (value1.Y == value2.Y)) {
                return !(value1.Z == value2.Z);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Point3 objects.
        /// </summary>
        public static void Add (ref Point3 value1, ref Point3 value2, out Point3 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
        }

        /// <summary>
        /// Performs addition of two Point3 objects using the (X+Y) operator. 
        /// </summary>
        public static Point3 operator + (Point3 value1, Point3 value2)
        {
            Point3 point;
            point.X = value1.X + value2.X;
            point.Y = value1.Y + value2.Y;
            point.Z = value1.Z + value2.Z;
            return point;
        }


        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Point3 objects.
        /// </summary>
        public static void Subtract (ref Point3 value1, ref Point3 value2, out Point3 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        /// <summary>
        /// Performs subtraction of two Point3 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Point3 operator - (Point3 value1, Point3 value2)
        {
            Point3 point;
            point.X = value1.X - value2.X;
            point.Y = value1.Y - value2.Y;
            point.Z = value1.Z - value2.Z;
            return point;
        }


        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Point3 object.
        /// </summary>
        public static void Negate (ref Point3 value, out Point3 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
        }

        /// <summary>
        /// Performs negation of a Point3 object using the (-X) operator.
        /// </summary>
        public static Point3 operator - (Point3 value)
        {
            Point3 point;
            point.X = -value.X;
            point.Y = -value.Y;
            point.Z = -value.Z;
            return point;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Point3 objects.
        /// </summary>
        public static void Multiply (ref Point3 value1, ref Point3 value2, out Point3 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
        }

        /// <summary>
        /// Performs multiplication of a Point3 object and a Int32
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Point3 value1, Int32 scaleFactor, out Point3 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Point3 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Point3 operator * (Point3 value1, Point3 value2)
        {
            Point3 point;
            point.X = value1.X * value2.X;
            point.Y = value1.Y * value2.Y;
            point.Z = value1.Z * value2.Z;
            return point;
        }

        /// <summary>
        /// Performs multiplication of a Point3 object and a Int32
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Point3 operator * (Point3 value, Int32 scaleFactor)
        {
            Point3 point;
            point.X = value.X * scaleFactor;
            point.Y = value.Y * scaleFactor;
            point.Z = value.Z * scaleFactor;
            return point;
        }

        /// <summary>
        /// Performs multiplication of a Int32 precision scaling factor 
        /// and aPoint3 object using the (x*Y) operator.
        /// </summary>
        public static Point3 operator * (Int32 scaleFactor, Point3 value)
        {
            Point3 point;
            point.X = value.X * scaleFactor;
            point.Y = value.Y * scaleFactor;
            point.Z = value.Z * scaleFactor;
            return point;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Point3 objects.
        /// </summary>
        public static void Divide (ref Point3 value1, ref Point3 value2, out Point3 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
        }

        /// <summary>
        /// Performs division of a Point3 object and a Int32 precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Point3 value1, Int32 value2, out Point3 result)
        {
            Int32 one = 1;
            Int32 num = one / value2;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
        }

        /// <summary>
        /// Performs division of two Point3 objects using the (X/Y) operator.
        /// </summary>
        public static Point3 operator / (Point3 value1, Point3 value2)
        {
            Point3 point;
            point.X = value1.X / value2.X;
            point.Y = value1.Y / value2.Y;
            point.Z = value1.Z / value2.Z;
            return point;
        }

        /// <summary>
        /// Performs division of a Point3 object and a Int32 precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Point3 operator / (Point3 value, Int32 divider)
        {
            Point3 point;
            Int32 one = 1;

            Int32 num = one / divider;
            point.X = value.X * num;
            point.Y = value.Y * num;
            point.Z = value.Z * num;
            return point;
        }
        
    }

}

namespace Abacus.Int64Precision
{
    /// <summary>
    /// Represents a Int64 precision point on a 2D integer grid.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Point2 
        : IEquatable<Point2>
    {
        /// <summary>
        /// Gets or sets the x-component of the point.
        /// </summary>
        public Int64 X;

        /// <summary>
        /// Gets or sets the y-component of the point.
        /// </summary>
        public Int64 Y;

        /// <summary>
        /// Initilises a new instance of Point2 from two Int64 values 
        /// representing X and Y respectively.
        /// </summary>
        public Point2 (Int64 x, Int64 y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return String.Format ("{{X:{0} Y:{1}}}", this.X, this.Y );
        }

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (this.X.GetHashCode () + this.Y.GetHashCode ());
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Point2 with all of its components set to zero.
        /// </summary>
        static Point2 zero;

        /// <summary>
        /// Defines a Point2 with all of its components set to one.
        /// </summary>
        static Point2 one;

        /// <summary>
        /// Defines the unit Point2 for the X-axis.
        /// </summary>
        static Point2 unitX;

        /// <summary>
        /// Defines the unit Point2 for the Y-axis.
        /// </summary>
        static Point2 unitY;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Point2()
        {
            zero = new Point2();
            one = new Point2(1, 1);
            unitX = new Point2(1, 0);
            unitY = new Point2(0, 1);
        }

        /// <summary>
        /// Returns a Point2 with all of its components set to zero.
        /// </summary>
        public static Point2 Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Returns a Point2 with all of its components set to one.
        /// </summary>
        public static Point2 One
        {
            get { return one; }
        }

        /// <summary>
        /// Returns the unit Point2 for the X-axis.
        /// </summary>
        public static Point2 UnitX
        {
            get { return unitX; }
        }

        /// <summary>
        /// Returns the unit Point2 for the Y-axis.
        /// </summary>
        public static Point2 UnitY
        {
            get { return unitY; }
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Point2 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Point2) {
                flag = this.Equals ((Point2)obj);
            }
            return flag;
        }

        #region IEquatable<Point2>

        /// <summary>
        /// Determines whether or not this Point2 object is equal to another
        /// Point2 object.
        /// </summary>
        public Boolean Equals (Point2 other)
        {
            return ((this.X == other.X) && (this.Y == other.Y));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Point2 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Point2 value1, Point2 value2)
        {
            return ((value1.X == value2.X) && (value1.Y == value2.Y));
        }

        /// <summary>
        /// Determines whether or not two Point2 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Point2 value1, Point2 value2)
        {
            if (value1.X == value2.X) {
                return !(value1.Y == value2.Y);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Point2 objects.
        /// </summary>
        public static void Add (
            ref Point2 value1, ref Point2 value2, out Point2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        /// <summary>
        /// Performs addition of two Point2 objects using the (X+Y) operator. 
        /// </summary>
        public static Point2 operator + (Point2 value1, Point2 value2)
        {
            Point2 point;
            point.X = value1.X + value2.X;
            point.Y = value1.Y + value2.Y;
            return point;
        }


        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Point2 objects.
        /// </summary>
        public static void Subtract (
            ref Point2 value1, ref Point2 value2, out Point2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }

        /// <summary>
        /// Performs subtraction of two Point2 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Point2 operator - (Point2 value1, Point2 value2)
        {
            Point2 point;
            point.X = value1.X - value2.X;
            point.Y = value1.Y - value2.Y;
            return point;
        }


        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Point2 object.
        /// </summary>
        public static void Negate (ref Point2 value, out Point2 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
        }

        /// <summary>
        /// Performs negation of a Point2 object using the (-X) operator.
        /// </summary>
        public static Point2 operator - (Point2 value)
        {
            Point2 point;
            point.X = -value.X;
            point.Y = -value.Y;
            return point;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Point2 objects.
        /// </summary>
        public static void Multiply (
            ref Point2 value1, ref Point2 value2, out Point2 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
        }

        /// <summary>
        /// Performs multiplication of a Point2 object and a Int64
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Point2 value, Int64 scaleFactor, out Point2 result)
        {
            result.X = value.X * scaleFactor;
            result.Y = value.Y * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Point2 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Point2 operator * (
            Point2 value1, Point2 value2)
        {
            Point2 point;
            point.X = value1.X * value2.X;
            point.Y = value1.Y * value2.Y;
            return point;
        }

        /// <summary>
        /// Performs multiplication of a Point2 object and a Int64
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Point2 operator * (
            Point2 value, Int64 scaleFactor)
        {
            Point2 point;
            point.X = value.X * scaleFactor;
            point.Y = value.Y * scaleFactor;
            return point;
        }

        /// <summary>
        /// Performs multiplication of a Int64 precision scaling factor 
        /// and aPoint2 object using the (x*Y) operator.
        /// </summary>
        public static Point2 operator * (
            Int64 scaleFactor, Point2 value)
        {
            Point2 point;
            point.X = value.X * scaleFactor;
            point.Y = value.Y * scaleFactor;
            return point;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Point2 objects.
        /// </summary>
        public static void Divide (
            ref Point2 value1, ref Point2 value2, out Point2 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
        }

        /// <summary>
        /// Performs division of a Point2 object and a Int64 precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Point2 value1, Int64 divider, out Point2 result)
        {
            Int64 one = 1;
            Int64 num = one / divider;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
        }

        /// <summary>
        /// Performs division of two Point2 objects using the (X/Y) operator.
        /// </summary>
        public static Point2 operator / (Point2 value1, Point2 value2)
        {
            Point2 point;
            point.X = value1.X / value2.X;
            point.Y = value1.Y / value2.Y;
            return point;
        }

        /// <summary>
        /// Performs division of a Point2 object and a Int64 precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Point2 operator / (Point2 value1, Int64 divider)
        {
            Point2 point;
            Int64 one = 1;
            Int64 num = one / divider;
            point.X = value1.X * num;
            point.Y = value1.Y * num;
            return point;
        }
        
    }

    /// <summary>
    /// Represents a Int64 precision point on a 3D integer grid.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Point3 
        : IEquatable<Point3>
    {
        /// <summary>
        /// Gets or sets the x-component of the point.
        /// </summary>
        public Int64 X;

        /// <summary>
        /// Gets or sets the y-component of the point.
        /// </summary>
        public Int64 Y;

        /// <summary>
        /// Gets or sets the z-component of the point.
        /// </summary>
        public Int64 Z;

        /// <summary>
        /// Initilises a new instance of Point3 from three Int64 values 
        /// representing X, Y and Z respectively.
        /// </summary>
        public Point3(Int64 x, Int64 y, Int64 z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        
        /// <summary>
        /// Initilises a new instance of Point3 from one Point2 value
        /// representing X and Y and one Int64 value representing Z.
        /// </summary>
        public Point3 (Point2 value, Int64 z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("{{X:{0} Y:{1} Z:{2}}}", this.X, this.Y, this.Z );
        }

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        public override Int32 GetHashCode()
        {
            return (this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode());
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Point3 with all of its components set to zero.
        /// </summary>
        static Point3 zero;

        /// <summary>
        /// Defines a Point3 with all of its components set to one.
        /// </summary>
        static Point3 one;

        /// <summary>
        /// Defines the unit Point3 for the X-axis.
        /// </summary>
        static Point3 unitX;

        /// <summary>
        /// Defines the unit Point3 for the Y-axis.
        /// </summary>
        static Point3 unitY;

        /// <summary>
        /// Defines the unit Point3 for the Z-axis.
        /// </summary>
        static Point3 unitZ;

        /// <summary>
        /// Defines a unit Point3 designating up (0, 1, 0).
        /// </summary>
        static Point3 up;

        /// <summary>
        /// Defines a unit Point3 designating down (0, −1, 0).
        /// </summary>
        static Point3 down;

        /// <summary>
        /// Defines a unit Point3 pointing to the right (1, 0, 0).
        /// </summary>
        static Point3 right;

        /// <summary>
        /// Defines a unit Point3 designating left (−1, 0, 0).
        /// </summary>
        static Point3 left;

        /// <summary>
        /// Defines a unit Point3 designating forward in a right-handed 
        /// coordinate system (0, 0, -1).
        /// </summary>
        static Point3 forward;

        /// <summary>
        /// Defines a unit Point3 designating backward in a right-handed 
        /// coordinate system (0, 0, 1).
        /// </summary>
        static Point3 backward;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
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

        /// <summary>
        /// Returns a Point3 with all of its components set to zero.
        /// </summary>
        public static Point3 Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Returns a Point3 with all of its components set to one.
        /// </summary>
        public static Point3 One
        {
            get { return one; }
        }

        /// <summary>
        /// Returns the unit Point3 for the X-axis.
        /// </summary>
        public static Point3 UnitX
        {
            get { return unitX; }
        }

        /// <summary>
        /// Returns the unit Point3 for the Y-axis.
        /// </summary>
        public static Point3 UnitY
        {
            get { return unitY; }
        }

        /// <summary>
        /// Returns the unit Point3 for the Z-axis.
        /// </summary>
        public static Point3 UnitZ
        {
            get { return unitZ; }
        }

        /// <summary>
        /// Returns a unit Point3 designating up (0, 1, 0).
        /// </summary>
        public static Point3 Up
        {
            get { return up; }
        }

        /// <summary>
        /// Returns a unit Point3 designating down (0, −1, 0).
        /// </summary>
        public static Point3 Down
        {
            get { return down; }
        }

        /// <summary>
        /// Returns a unit Point3 pointing to the right (1, 0, 0).
        /// </summary>
        public static Point3 Right
        {
            get { return right; }
        }

        /// <summary>
        /// Returns a unit Point3 designating left (−1, 0, 0).
        /// </summary>
        public static Point3 Left
        {
            get { return left; }
        }

        /// <summary>
        /// Returns a unit Point3 designating forward in a right-handed 
        /// coordinate system (0, 0, -1).
        /// </summary>
        public static Point3 Forward
        {
            get { return forward; }
        }

        /// <summary>
        /// Returns a unit Point3 designating backward in a right-handed 
        /// coordinate system (0, 0, 1).
        /// </summary>
        public static Point3 Backward
        {
            get { return backward; }
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Point3 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Point3) {
                flag = this.Equals ((Point3)obj);
            }
            return flag;
        }

        #region IEquatable<Point3>

        /// <summary>
        /// Determines whether or not this Point3 object is equal to another
        /// Point3 object.
        /// </summary>
        public Boolean Equals (Point3 other)
        {
            return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Point3 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Point3 value1, Point3 value2)
        {
            return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z));
        }

        /// <summary>
        /// Determines whether or not two Point3 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Point3 value1, Point3 value2)
        {
            if ((value1.X == value2.X) && (value1.Y == value2.Y)) {
                return !(value1.Z == value2.Z);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Point3 objects.
        /// </summary>
        public static void Add (ref Point3 value1, ref Point3 value2, out Point3 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
        }

        /// <summary>
        /// Performs addition of two Point3 objects using the (X+Y) operator. 
        /// </summary>
        public static Point3 operator + (Point3 value1, Point3 value2)
        {
            Point3 point;
            point.X = value1.X + value2.X;
            point.Y = value1.Y + value2.Y;
            point.Z = value1.Z + value2.Z;
            return point;
        }


        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Point3 objects.
        /// </summary>
        public static void Subtract (ref Point3 value1, ref Point3 value2, out Point3 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        /// <summary>
        /// Performs subtraction of two Point3 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Point3 operator - (Point3 value1, Point3 value2)
        {
            Point3 point;
            point.X = value1.X - value2.X;
            point.Y = value1.Y - value2.Y;
            point.Z = value1.Z - value2.Z;
            return point;
        }


        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Point3 object.
        /// </summary>
        public static void Negate (ref Point3 value, out Point3 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
        }

        /// <summary>
        /// Performs negation of a Point3 object using the (-X) operator.
        /// </summary>
        public static Point3 operator - (Point3 value)
        {
            Point3 point;
            point.X = -value.X;
            point.Y = -value.Y;
            point.Z = -value.Z;
            return point;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Point3 objects.
        /// </summary>
        public static void Multiply (ref Point3 value1, ref Point3 value2, out Point3 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
        }

        /// <summary>
        /// Performs multiplication of a Point3 object and a Int64
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Point3 value1, Int64 scaleFactor, out Point3 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Point3 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Point3 operator * (Point3 value1, Point3 value2)
        {
            Point3 point;
            point.X = value1.X * value2.X;
            point.Y = value1.Y * value2.Y;
            point.Z = value1.Z * value2.Z;
            return point;
        }

        /// <summary>
        /// Performs multiplication of a Point3 object and a Int64
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Point3 operator * (Point3 value, Int64 scaleFactor)
        {
            Point3 point;
            point.X = value.X * scaleFactor;
            point.Y = value.Y * scaleFactor;
            point.Z = value.Z * scaleFactor;
            return point;
        }

        /// <summary>
        /// Performs multiplication of a Int64 precision scaling factor 
        /// and aPoint3 object using the (x*Y) operator.
        /// </summary>
        public static Point3 operator * (Int64 scaleFactor, Point3 value)
        {
            Point3 point;
            point.X = value.X * scaleFactor;
            point.Y = value.Y * scaleFactor;
            point.Z = value.Z * scaleFactor;
            return point;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Point3 objects.
        /// </summary>
        public static void Divide (ref Point3 value1, ref Point3 value2, out Point3 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
        }

        /// <summary>
        /// Performs division of a Point3 object and a Int64 precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Point3 value1, Int64 value2, out Point3 result)
        {
            Int64 one = 1;
            Int64 num = one / value2;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
        }

        /// <summary>
        /// Performs division of two Point3 objects using the (X/Y) operator.
        /// </summary>
        public static Point3 operator / (Point3 value1, Point3 value2)
        {
            Point3 point;
            point.X = value1.X / value2.X;
            point.Y = value1.Y / value2.Y;
            point.Z = value1.Z / value2.Z;
            return point;
        }

        /// <summary>
        /// Performs division of a Point3 object and a Int64 precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Point3 operator / (Point3 value, Int64 divider)
        {
            Point3 point;
            Int64 one = 1;

            Int64 num = one / divider;
            point.X = value.X * num;
            point.Y = value.Y * num;
            point.Z = value.Z * num;
            return point;
        }
        
    }

}


namespace Abacus.SinglePrecision
{
    /// <summary>
    /// todo
    /// </summary>
    internal class GjkDistance
    {
        /// <summary>
        /// todo
        /// </summary>
        internal GjkDistance ()
        {
            for (Int32 i = 0; i < 0x10; i++)
            {
                this.det [i] = new Single[4];
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        internal Boolean AddSupportPoint (ref Vector3 newPoint)
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

                this.edgeLengthSq [index] [num2] = 
                    this.edgeLengthSq [num2] [index] = 
                        vector.LengthSquared ();
            }

            this.UpdateDeterminant (index);

            return this.UpdateSimplex (index);
        }

        /// <summary>
        /// todo
        /// </summary>
        internal void Reset ()
        {
            Single zero = 0;

            this.simplexBits = 0;
            this.maxLengthSq = zero;
        }

        /// <summary>
        /// todo
        /// </summary>
        internal Vector3 ClosestPoint
        {
            get { return this.closestPoint; }
        }

        /// <summary>
        /// todo
        /// </summary>
        internal Boolean FullSimplex
        {
            get { return (this.simplexBits == 15); }
        }
        
        /// <summary>
        /// todo
        /// </summary>
        internal Single MaxLengthSquared
        {
            get { return this.maxLengthSq; }
        }

        /// <summary>
        /// todo
        /// </summary>
        Vector3 closestPoint;

        /// <summary>
        /// todo
        /// </summary>
        Single[][] det = new Single[0x10][];

        /// <summary>
        /// todo
        /// </summary>
        Single[][] edgeLengthSq = 
            new Single[][] 
            { 
                new Single[4], 
                new Single[4], 
                new Single[4], 
                new Single[4] 
            };
        
        /// <summary>
        /// todo
        /// </summary>
        Vector3[][] edges = 
            new Vector3[][] 
            { 
                new Vector3[4], 
                new Vector3[4], 
                new Vector3[4], 
                new Vector3[4] 
            };
        
        /// <summary>
        /// todo
        /// </summary>
        Single maxLengthSq;

        /// <summary>
        /// todo
        /// </summary>
        Int32 simplexBits;

        /// <summary>
        /// todo
        /// </summary>
        Vector3[] y = new Vector3[4];

        /// <summary>
        /// todo
        /// </summary>
        Single[] yLengthSq = new Single[4];

        /// <summary>
        /// todo
        /// </summary>
        static Int32[] BitsToIndices = 
            new Int32[] 
            { 
                0, 1, 2, 0x11, 3, 0x19, 0x1a, 0xd1, 
                4, 0x21, 0x22, 0x111, 0x23, 0x119, 0x11a, 0x8d1 
            };

        /// <summary>
        /// todo
        /// </summary>
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

                this.maxLengthSq = 
                RealMaths.Max (this.maxLengthSq, this.yLengthSq [index]);
            }

            return (Vector3)(zero / num3);
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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

                this.det [num6] [num] = 
                    Dot (ref this.edges [xmIdx] [num], ref this.y [xmIdx]);

                this.det [num6] [xmIdx] = 
                    Dot (ref this.edges [num] [xmIdx], ref this.y [num]);

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

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        static Single Dot (ref Vector3 a, ref Vector3 b)
        {
            return (((a.X * b.X) + (a.Y * b.Y)) + (a.Z * b.Z));
        }
    }
    /// <summary>
    /// Single precision Matrix44.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Matrix44 
        : IEquatable<Matrix44>
    {
        /// <summary>
        /// Gets or sets (Row 1, Column 1) of the Matrix44.
        /// </summary>
        public Single M11;

        /// <summary>
        /// Gets or sets (Row 1, Column 2) of the Matrix44.
        /// </summary>
        public Single M12;

        /// <summary>
        /// Gets or sets (Row 1, Column 3) of the Matrix44.
        /// </summary>
        public Single M13;

        /// <summary>
        /// Gets or sets (Row 1, Column 4) of the Matrix44.
        /// </summary>
        public Single M14;

        /// <summary>
        /// Gets or sets (Row 2, Column 1) of the Matrix44.
        /// </summary>
        public Single M21;

        /// <summary>
        /// Gets or sets (Row 2, Column 2) of the Matrix44.
        /// </summary>
        public Single M22;

        /// <summary>
        /// Gets or sets (ow 2, Column 3) of the Matrix44.
        /// </summary>
        public Single M23;

        /// <summary>
        /// Gets or sets (Row 2, Column 4) of the Matrix44.
        /// </summary>
        public Single M24;

        /// <summary>
        /// Row 3, Column 1) of the Matrix44.
        /// </summary>
        public Single M31;

        /// <summary>
        /// Gets or sets (Row 3, Column 2) of the Matrix44.
        /// </summary>
        public Single M32;

        /// <summary>
        /// Gets or sets (Row 3, Column 3) of the Matrix44.
        /// </summary>
        public Single M33;

        /// <summary>
        /// Gets or sets (Row 3, Column 4) of the Matrix44.
        /// </summary>
        public Single M34;

        /// <summary>
        /// Gets or sets (Row 4, Column 1) of the Matrix44.
        /// </summary>
        public Single M41; // translation.x

        /// <summary>
        /// Gets or sets (Row 4, Column 2) of the Matrix44.
        /// </summary>
        public Single M42; // translation.y

        /// <summary>
        /// Gets or sets (Row 4, Column 3) of the Matrix44.
        /// </summary>
        public Single M43; // translation.z

        /// <summary>
        /// Gets or sets (Row 4, Column 4) of the Matrix44.
        /// </summary>
        public Single M44;
        
        /// <summary>
        /// Initilises a new instance of Matrix44 from sixteen Single 
        /// values representing the matrix, in row major order, respectively.
        /// </summary>
        public Matrix44 (
            Single m11, 
            Single m12, 
            Single m13, 
            Single m14, 
            Single m21, 
            Single m22, 
            Single m23, 
            Single m24, 
            Single m31, 
            Single m32, 
            Single m33, 
            Single m34, 
            Single m41, 
            Single m42, 
            Single m43, 
            Single m44)
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

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return 
                (
                    "{ " + 
                    string.Format ("{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", 
                        new Object[] 
                        { 
                            this.M11.ToString (), 
                            this.M12.ToString (), 
                            this.M13.ToString (), 
                            this.M14.ToString () 
                        }
                    ) + 
                    string.Format ("{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", 
                        new Object[] 
                        { 
                            this.M21.ToString (), 
                            this.M22.ToString (), 
                            this.M23.ToString (), 
                            this.M24.ToString () 
                            }
                    ) + 
                    string.Format ("{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", 
                        new Object[] 
                        { 
                            this.M31.ToString (), 
                            this.M32.ToString (), 
                            this.M33.ToString (), 
                            this.M34.ToString () 
                        }
                    ) + string.Format ("{{M41:{0} M42:{1} M43:{2} M44:{3}}} ", 
                    new Object[] 
                    { 
                        this.M41.ToString (), 
                        this.M42.ToString (), 
                        this.M43.ToString (), 
                        this.M44.ToString () 
                    }
                    ) + 
                    "}"
                );
        }

        /// <summary>
        /// Gets the hash code of the Matrix44 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return 
                (((((((((((((((
                    this.M11.GetHashCode () + 
                    this.M12.GetHashCode ()) + 
                    this.M13.GetHashCode ()) + 
                    this.M14.GetHashCode ()) + 
                    this.M21.GetHashCode ()) + 
                    this.M22.GetHashCode ()) + 
                    this.M23.GetHashCode ()) + 
                    this.M24.GetHashCode ()) + 
                    this.M31.GetHashCode ()) + 
                    this.M32.GetHashCode ()) + 
                    this.M33.GetHashCode ()) + 
                    this.M34.GetHashCode ()) + 
                    this.M41.GetHashCode ()) + 
                    this.M42.GetHashCode ()) + 
                    this.M43.GetHashCode ()) + 
                    this.M44.GetHashCode ());
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Up 
        {
            get 
            {
                Vector3 vector;
                vector.X = this.M21;
                vector.Y = this.M22;
                vector.Z = this.M23;
                return vector;
            }
            set 
            {
                this.M21 = value.X;
                this.M22 = value.Y;
                this.M23 = value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Down 
        {
            get 
            {
                Vector3 vector;
                vector.X = -this.M21;
                vector.Y = -this.M22;
                vector.Z = -this.M23;
                return vector;
            }
            set 
            {
                this.M21 = -value.X;
                this.M22 = -value.Y;
                this.M23 = -value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Right 
        {
            get 
            {
                Vector3 vector;
                vector.X = this.M11;
                vector.Y = this.M12;
                vector.Z = this.M13;
                return vector;
            }
            set 
            {
                this.M11 = value.X;
                this.M12 = value.Y;
                this.M13 = value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Left
        {
            get
            {
                Vector3 vector;
                vector.X = -this.M11;
                vector.Y = -this.M12;
                vector.Z = -this.M13;
                return vector;
            }
            set
            {
                this.M11 = -value.X;
                this.M12 = -value.Y;
                this.M13 = -value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Forward
        {
            get
            {
                Vector3 vector;
                vector.X = -this.M31;
                vector.Y = -this.M32;
                vector.Z = -this.M33;
                return vector;
            }
            set 
            {
                this.M31 = -value.X;
                this.M32 = -value.Y;
                this.M33 = -value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Backward
        {
            get
            {
                Vector3 vector;
                vector.X = this.M31;
                vector.Y = this.M32;
                vector.Z = this.M33;
                return vector;
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
                this.M33 = value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Translation
        {
            get 
            {
                Vector3 vector;
                vector.X = this.M41;
                vector.Y = this.M42;
                vector.Z = this.M43;
                return vector;
            }
            set
            {
                this.M41 = value.X;
                this.M42 = value.Y;
                this.M43 = value.Z;
            }
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines the identity matrix.
        /// </summary>
        static Matrix44 identity;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Matrix44 ()
        {
            identity = new Matrix44 (
                1, 0, 0, 0, 
                0, 1, 0, 0, 
                0, 0, 1, 0, 
                0, 0, 0, 1);
        }

        /// <summary>
        /// Returns the identity matrix.
        /// </summary>
        public static Matrix44 Identity 
        {
            get { return identity; }
        }
        
        /// <summary>
        /// todo
        /// </summary>
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
        
        /// <summary>
        /// todo
        /// </summary>
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
        
        /// <summary>
        /// Creates a scaling matrix based on x, y, z.
        /// </summary>
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

        /// <summary>
        /// Creates a scaling matrix based on a vector.
        /// </summary>
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

        /// <summary>
        /// Create a scaling matrix consistant along each axis
        /// </summary>
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

        /// <summary>
        /// http://en.wikipedia.org/wiki/Rotation_matrix
        /// </summary>
        public static void CreateRotationX (Single radians, out Matrix44 result)
        {
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

        /// <summary>
        /// http://en.wikipedia.org/wiki/Rotation_matrix
        /// </summary>
        public static void CreateRotationY (Single radians, out Matrix44 result)
        {
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

        /// <summary>
        /// http://en.wikipedia.org/wiki/Rotation_matrix
        /// </summary>
        public static void CreateRotationZ (Single radians, out Matrix44 result)
        {
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
        
        /// <summary>
        /// todo
        /// </summary>
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
        
        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        public static void CreateWorldNew (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result)
        {
            Vector3 backward = -forward;

            Vector3 right;

            Vector3.Cross (ref up, ref backward, out right);

            Vector3.Normalise(ref right, out right);

            Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out result);

            result.M41 = position.X;
            result.M42 = position.Y;
            result.M43 = position.Z;
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        public static void CreateFromYawPitchRoll (Single yaw, Single pitch, Single roll, out Matrix44 result)
        {
            Quaternion quaternion;

            Quaternion.CreateFromYawPitchRoll (yaw, pitch, roll, out quaternion);

            CreateFromQuaternion (ref quaternion, out result);
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Creates a cylindrical billboard that rotates around a specified axis.
        /// This method computes the facing direction of the billboard from the object position and camera position. 
        /// When the object and camera positions are too close, the matrix will not be accurate. 
        /// To avoid this problem, the method uses the optional camera forward vector if the positions are too close.
        /// </summary>
        public static void CreateBillboard (ref Vector3 ObjectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix44 result)
        {
            Single zero = 0;
            Single one = 1;

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

            Vector3.Normalise (ref vector3, out vector3);

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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// todo
        /// </summary>
        public static void CreateConstrainedBillboard (
            ref Vector3 objectPosition, 
            ref Vector3 cameraPosition, 
            ref Vector3 rotateAxis, 
            Vector3? cameraForwardVector, 
            Vector3? objectForwardVector, 
            out Matrix44 result)
        {
            Single zero = 0;
            Single one = 1;

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
                Vector3.Normalise (ref vector3, out vector3);
                Vector3.Cross (ref vector3, ref rotateAxis, out vector);
                Vector3.Normalise (ref vector, out vector);
            } else {
                Vector3.Cross (ref rotateAxis, ref vector2, out vector3);
                Vector3.Normalise (ref vector3, out vector3);
                Vector3.Cross (ref vector3, ref vector4, out vector);
                Vector3.Normalise (ref vector, out vector);
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

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
        /// </summary>
        public static void CreatePerspectiveFieldOfView (
            Single fieldOfView, 
            Single aspectRatio, 
            Single nearPlaneDistance, 
            Single farPlaneDistance, 
            out Matrix44 result)
        {
            Single zero = 0;
            Single half; RealMaths.Half(out half);
            Single one = 1;
            Single pi; RealMaths.Pi(out pi);

            if ((fieldOfView <= zero) || (fieldOfView >= pi))
            {
                throw new ArgumentOutOfRangeException ("fieldOfView");
            }

            if (nearPlaneDistance <= zero)
            {
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");
            }

            if (farPlaneDistance <= zero)
            {
                throw new ArgumentOutOfRangeException ("farPlaneDistance");
            }

            if (nearPlaneDistance >= farPlaneDistance)
            {
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");
            }

            //
            // xScale     0          0              0
            // 0        yScale       0              0
            // 0        0        zf/(zn-zf)        -1
            // 0        0        zn*zf/(zn-zf)      0
            //
            // where:
            //
            // yScale = cot(fovY/2)
            //     
            // xScale = yScale / aspect ratio
            //

            // yScale = cot(fovY/2)
            Single yScale = one / ( RealMaths.Tan ( fieldOfView * half ) );

            // xScale = yScale / aspect ratio
            Single xScale = yScale / aspectRatio;

            result.M11 = xScale;
            result.M12 = zero;
            result.M13 = zero;
            result.M14 = zero;
            
            result.M21 = zero;
            result.M22 = yScale;
            result.M23 = zero;
            result.M24 = zero;

            result.M31 = zero;
            result.M32 = zero;
            result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance); // zf/(zn-zf)
            result.M34 = -one;

            result.M41 = zero;
            result.M42 = zero;
            result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance); // zn*zf/(zn-zf)
            result.M44 = zero;
        }



        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
        /// </summary>
        public static void CreatePerspective (
            Single width, 
            Single height, 
            Single nearPlaneDistance, 
            Single farPlaneDistance, 
            out Matrix44 result)
        {
            Single zero = 0;
            Single one = 1;
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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx
        /// </summary>
        public static void CreatePerspectiveOffCenter (
            Single left, 
            Single right, 
            Single bottom, 
            Single top, 
            Single nearPlaneDistance, 
            Single farPlaneDistance, 
            out Matrix44 result)
        {
            Single zero = 0;
            Single one = 1;
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
        
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
        /// </summary>
        public static void CreateOrthographic (
            Single width, 
            Single height, 
            Single zNearPlane, 
            Single zFarPlane, 
            out Matrix44 result)
        {
            Single zero = 0;
            Single one = 1;
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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx
        /// </summary>
        public static void CreateOrthographicOffCenter (
            Single left, 
            Single right, 
            Single bottom, 
            Single top, 
            Single zNearPlane, 
            Single zFarPlane, 
            out Matrix44 result)
        {
            Single zero = 0;
            Single one = 1;
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
        
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx
        /// </summary>
        public static void CreateLookAt (
            ref Vector3 cameraPosition, 
            ref Vector3 cameraTarget, 
            ref Vector3 cameraUpVector, 
            out Matrix44 result)
        {
            Single zero = 0;
            Single one = 1;

            Vector3 forward = cameraPosition - cameraTarget;
            Vector3.Normalise (ref forward, out forward);

            Vector3 right;
            Vector3.Cross (ref cameraUpVector, ref forward, out right); 
            Vector3.Normalise (ref right, out right);
            
            Vector3 up;
            Vector3.Cross (ref forward, ref right, out up);
            Vector3.Normalise (ref up, out up);
            
            result.M11 = right.X;
            result.M12 = up.X;
            result.M13 = forward.X;
            result.M14 = zero;

            result.M21 = right.Y;
            result.M22 = up.Y;
            result.M23 = forward.Y;
            result.M24 = zero;

            result.M31 = right.Z;
            result.M32 = up.Z;
            result.M33 = forward.Z;
            result.M34 = zero;

            Single a;
            Single b;
            Single c;

            Vector3.Dot (ref right, ref cameraPosition, out a);
            Vector3.Dot (ref up, ref cameraPosition, out b);
            Vector3.Dot (ref forward, ref cameraPosition, out c);
            
            result.M41 = -a;
            result.M42 = -b;
            result.M43 = -c;

            result.M44 = one;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void Transpose (ref Matrix44 input, out Matrix44 output)
        {
            output.M11 = input.M11;
            output.M22 = input.M22;
            output.M33 = input.M33;
            output.M44 = input.M44;

            Single temp = input.M12;
            output.M12 = input.M21;
            output.M21 = temp;

            temp = input.M13;
            output.M13 = input.M31;
            output.M31 = temp;

            temp = input.M14;
            output.M14 = input.M41;
            output.M41 = temp;

            temp = input.M23;
            output.M23 = input.M32;
            output.M32 = temp;

            temp = input.M24;
            output.M24 = input.M42;
            output.M42 = temp;

            temp =  input.M34;
            output.M34 = input.M43;
            output.M43 = temp;
        }

        /// <summary>
        /// Essential Mathemathics For Games & Interactive Applications
        /// </summary>
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

            Vector3.Normalise(ref a, out a);
            Vector3.Normalise(ref b, out b);
            Vector3.Normalise(ref c, out c);

            Vector3 right = new Vector3(a.X, b.X, c.X);
            Vector3 up = new Vector3(a.Y, b.Y, c.Y);
            Vector3 backward = new Vector3(a.Z, b.Z, c.Z);

            Vector3.Normalise(ref right, out right);
            Vector3.Normalise(ref up, out up);
            Vector3.Normalise(ref backward, out backward);

            Matrix44 rotMat;
            Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out rotMat);

            Quaternion.CreateFromRotationMatrix(ref rotMat, out rotation);

            return true;
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// todo
        /// </summary>
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
        
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// todo
        /// </summary>
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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Transforms a Matrix by applying a Quaternion rotation.
        /// </summary>
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

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Matrix44 object is equal to another
        /// object
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;

            if (obj is Matrix44)
            {
                flag = this.Equals ((Matrix44) obj);
            }
            
            return flag;
        }

        #region IEquatable<Matrix44>

        /// <summary>
        /// Determines whether or not this Matrix44 object is equal to another
        /// Matrix44 object.
        /// </summary>
        public Boolean Equals (Matrix44 other)
        {
            return 
                (this.M11 == other.M11) && 
                (this.M22 == other.M22) && 
                (this.M33 == other.M33) && 
                (this.M44 == other.M44) && 
                (this.M12 == other.M12) && 
                (this.M13 == other.M13) && 
                (this.M14 == other.M14) && 
                (this.M21 == other.M21) && 
                (this.M23 == other.M23) && 
                (this.M24 == other.M24) && 
                (this.M31 == other.M31) && 
                (this.M32 == other.M32) && 
                (this.M34 == other.M34) && 
                (this.M41 == other.M41) && 
                (this.M42 == other.M42) && 
                (this.M43 == other.M43);
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Matrix44 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Matrix44 matrix1, Matrix44 matrix2)
        {
            return ((((((matrix1.M11 == matrix2.M11) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M44 == matrix2.M44))) && (((matrix1.M12 == matrix2.M12) && (matrix1.M13 == matrix2.M13)) && ((matrix1.M14 == matrix2.M14) && (matrix1.M21 == matrix2.M21)))) && ((((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)) && ((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32))) && (((matrix1.M34 == matrix2.M34) && (matrix1.M41 == matrix2.M41)) && (matrix1.M42 == matrix2.M42)))) && (matrix1.M43 == matrix2.M43));
        }
        
        /// <summary>
        /// Determines whether or not two Matrix44 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Matrix44 matrix1, Matrix44 matrix2)
        {
            if ((matrix1.M11 == matrix2.M11) && 
                (matrix1.M12 == matrix2.M12) && 
                (matrix1.M13 == matrix2.M13) && 
                (matrix1.M14 == matrix2.M14) && 
                (matrix1.M21 == matrix2.M21) && 
                (matrix1.M22 == matrix2.M22) && 
                (matrix1.M23 == matrix2.M23) && 
                (matrix1.M24 == matrix2.M24) && 
                (matrix1.M31 == matrix2.M31) && 
                (matrix1.M32 == matrix2.M32) && 
                (matrix1.M33 == matrix2.M33) && 
                (matrix1.M34 == matrix2.M34) && 
                (matrix1.M41 == matrix2.M41) && 
                (matrix1.M42 == matrix2.M42) && 
                (matrix1.M43 == matrix2.M43))
            {
                return !(matrix1.M44 == matrix2.M44);
            }

            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Matrix44 objects.
        /// </summary>
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

        /// <summary>
        /// Performs addition of two Matrix44 objects using the (X+Y) operator. 
        /// </summary>
        public static Matrix44 operator + (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
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
            return result;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Matrix44 objects.
        /// </summary>
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

        /// <summary>
        /// Performs subtraction of two Matrix44 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Matrix44 operator - (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
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
            return result;
        }

        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Matrix44 object.
        /// </summary>
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

        /// <summary>
        /// Performs negation of a Matrix44 object using the (-X) operator.
        /// </summary>
        public static Matrix44 operator - (Matrix44 matrix)
        {
            Matrix44 result;
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
            return result;
        }
        
        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Matrix44 objects.
        /// </summary>
        public static void Multiply (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
        {   
            result.M11 = 
                (matrix1.M11 * matrix2.M11) + 
                (matrix1.M12 * matrix2.M21) + 
                (matrix1.M13 * matrix2.M31) + 
                (matrix1.M14 * matrix2.M41);

            result.M12 =
                (matrix1.M11 * matrix2.M12) + 
                (matrix1.M12 * matrix2.M22) + 
                (matrix1.M13 * matrix2.M32) + 
                (matrix1.M14 * matrix2.M42);

            result.M13 = 
                (matrix1.M11 * matrix2.M13) + 
                (matrix1.M12 * matrix2.M23) + 
                (matrix1.M13 * matrix2.M33) + 
                (matrix1.M14 * matrix2.M43);

            result.M14 = 
                (matrix1.M11 * matrix2.M14) + 
                (matrix1.M12 * matrix2.M24) + 
                (matrix1.M13 * matrix2.M34) + 
                (matrix1.M14 * matrix2.M44);

            result.M21 = 
                (matrix1.M21 * matrix2.M11) + 
                (matrix1.M22 * matrix2.M21) + 
                (matrix1.M23 * matrix2.M31) + 
                (matrix1.M24 * matrix2.M41);

            result.M22 = 
                (matrix1.M21 * matrix2.M12) + 
                (matrix1.M22 * matrix2.M22) + 
                (matrix1.M23 * matrix2.M32) + 
                (matrix1.M24 * matrix2.M42);

            result.M23 = 
                (matrix1.M21 * matrix2.M13) + 
                (matrix1.M22 * matrix2.M23) + 
                (matrix1.M23 * matrix2.M33) + 
                (matrix1.M24 * matrix2.M43);

            result.M24 = 
                (matrix1.M21 * matrix2.M14) + 
                (matrix1.M22 * matrix2.M24) + 
                (matrix1.M23 * matrix2.M34) + 
                (matrix1.M24 * matrix2.M44);

            result.M31 = 
                (matrix1.M31 * matrix2.M11) + 
                (matrix1.M32 * matrix2.M21) + 
                (matrix1.M33 * matrix2.M31) + 
                (matrix1.M34 * matrix2.M41);

            result.M32 = 
                (matrix1.M31 * matrix2.M12) + 
                (matrix1.M32 * matrix2.M22) + 
                (matrix1.M33 * matrix2.M32) + 
                (matrix1.M34 * matrix2.M42);

            result.M33 = 
                (matrix1.M31 * matrix2.M13) + 
                (matrix1.M32 * matrix2.M23) + 
                (matrix1.M33 * matrix2.M33) + 
                (matrix1.M34 * matrix2.M43);

            result.M34 = 
                (matrix1.M31 * matrix2.M14) + 
                (matrix1.M32 * matrix2.M24) + 
                (matrix1.M33 * matrix2.M34) + 
                (matrix1.M34 * matrix2.M44);

            result.M41 = 
                (matrix1.M41 * matrix2.M11) + 
                (matrix1.M42 * matrix2.M21) + 
                (matrix1.M43 * matrix2.M31) + 
                (matrix1.M44 * matrix2.M41);

            result.M42 = 
                (matrix1.M41 * matrix2.M12) + 
                (matrix1.M42 * matrix2.M22) + 
                (matrix1.M43 * matrix2.M32) + 
                (matrix1.M44 * matrix2.M42);

            result.M43 = 
                (matrix1.M41 * matrix2.M13) + 
                (matrix1.M42 * matrix2.M23) + 
                (matrix1.M43 * matrix2.M33) + 
                (matrix1.M44 * matrix2.M43);

            result.M44 = 
                (matrix1.M41 * matrix2.M14) + 
                (matrix1.M42 * matrix2.M24) + 
                (matrix1.M43 * matrix2.M34) + 
                (matrix1.M44 * matrix2.M44);
        }

        /// <summary>
        /// Performs multiplication of a Matrix44 object and a Single
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Matrix44 matrix1, Single scaleFactor, out Matrix44 result)
        {
            result.M11 = matrix1.M11 * scaleFactor;
            result.M12 = matrix1.M12 * scaleFactor;
            result.M13 = matrix1.M13 * scaleFactor;
            result.M14 = matrix1.M14 * scaleFactor;
            result.M21 = matrix1.M21 * scaleFactor;
            result.M22 = matrix1.M22 * scaleFactor;
            result.M23 = matrix1.M23 * scaleFactor;
            result.M24 = matrix1.M24 * scaleFactor;
            result.M31 = matrix1.M31 * scaleFactor;
            result.M32 = matrix1.M32 * scaleFactor;
            result.M33 = matrix1.M33 * scaleFactor;
            result.M34 = matrix1.M34 * scaleFactor;
            result.M41 = matrix1.M41 * scaleFactor;
            result.M42 = matrix1.M42 * scaleFactor;
            result.M43 = matrix1.M43 * scaleFactor;
            result.M44 = matrix1.M44 * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Matrix44 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Matrix44 operator * (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            
            result.M11 = 
                (matrix1.M11 * matrix2.M11) + 
                (matrix1.M12 * matrix2.M21) + 
                (matrix1.M13 * matrix2.M31) + 
                (matrix1.M14 * matrix2.M41);

            result.M12 =
                (matrix1.M11 * matrix2.M12) + 
                (matrix1.M12 * matrix2.M22) + 
                (matrix1.M13 * matrix2.M32) + 
                (matrix1.M14 * matrix2.M42);

            result.M13 = 
                (matrix1.M11 * matrix2.M13) + 
                (matrix1.M12 * matrix2.M23) + 
                (matrix1.M13 * matrix2.M33) + 
                (matrix1.M14 * matrix2.M43);

            result.M14 = 
                (matrix1.M11 * matrix2.M14) + 
                (matrix1.M12 * matrix2.M24) + 
                (matrix1.M13 * matrix2.M34) + 
                (matrix1.M14 * matrix2.M44);

            result.M21 = 
                (matrix1.M21 * matrix2.M11) + 
                (matrix1.M22 * matrix2.M21) + 
                (matrix1.M23 * matrix2.M31) + 
                (matrix1.M24 * matrix2.M41);

            result.M22 = 
                (matrix1.M21 * matrix2.M12) + 
                (matrix1.M22 * matrix2.M22) + 
                (matrix1.M23 * matrix2.M32) + 
                (matrix1.M24 * matrix2.M42);

            result.M23 = 
                (matrix1.M21 * matrix2.M13) + 
                (matrix1.M22 * matrix2.M23) + 
                (matrix1.M23 * matrix2.M33) + 
                (matrix1.M24 * matrix2.M43);

            result.M24 = 
                (matrix1.M21 * matrix2.M14) + 
                (matrix1.M22 * matrix2.M24) + 
                (matrix1.M23 * matrix2.M34) + 
                (matrix1.M24 * matrix2.M44);

            result.M31 = 
                (matrix1.M31 * matrix2.M11) + 
                (matrix1.M32 * matrix2.M21) + 
                (matrix1.M33 * matrix2.M31) + 
                (matrix1.M34 * matrix2.M41);

            result.M32 = 
                (matrix1.M31 * matrix2.M12) + 
                (matrix1.M32 * matrix2.M22) + 
                (matrix1.M33 * matrix2.M32) + 
                (matrix1.M34 * matrix2.M42);

            result.M33 = 
                (matrix1.M31 * matrix2.M13) + 
                (matrix1.M32 * matrix2.M23) + 
                (matrix1.M33 * matrix2.M33) + 
                (matrix1.M34 * matrix2.M43);

            result.M34 = 
                (matrix1.M31 * matrix2.M14) + 
                (matrix1.M32 * matrix2.M24) + 
                (matrix1.M33 * matrix2.M34) + 
                (matrix1.M34 * matrix2.M44);

            result.M41 = 
                (matrix1.M41 * matrix2.M11) + 
                (matrix1.M42 * matrix2.M21) + 
                (matrix1.M43 * matrix2.M31) + 
                (matrix1.M44 * matrix2.M41);

            result.M42 = 
                (matrix1.M41 * matrix2.M12) + 
                (matrix1.M42 * matrix2.M22) + 
                (matrix1.M43 * matrix2.M32) + 
                (matrix1.M44 * matrix2.M42);

            result.M43 = 
                (matrix1.M41 * matrix2.M13) + 
                (matrix1.M42 * matrix2.M23) + 
                (matrix1.M43 * matrix2.M33) + 
                (matrix1.M44 * matrix2.M43);

            result.M44 = 
                (matrix1.M41 * matrix2.M14) + 
                (matrix1.M42 * matrix2.M24) + 
                (matrix1.M43 * matrix2.M34) + 
                (matrix1.M44 * matrix2.M44);

            return result;
        }
        
        /// <summary>
        /// Performs multiplication of a Matrix44 object and a Single
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Matrix44 operator * (Matrix44 matrix1, Single scaleFactor)
        {
            Matrix44 result;
            result.M11 = matrix1.M11 * scaleFactor;
            result.M12 = matrix1.M12 * scaleFactor;
            result.M13 = matrix1.M13 * scaleFactor;
            result.M14 = matrix1.M14 * scaleFactor;
            result.M21 = matrix1.M21 * scaleFactor;
            result.M22 = matrix1.M22 * scaleFactor;
            result.M23 = matrix1.M23 * scaleFactor;
            result.M24 = matrix1.M24 * scaleFactor;
            result.M31 = matrix1.M31 * scaleFactor;
            result.M32 = matrix1.M32 * scaleFactor;
            result.M33 = matrix1.M33 * scaleFactor;
            result.M34 = matrix1.M34 * scaleFactor;
            result.M41 = matrix1.M41 * scaleFactor;
            result.M42 = matrix1.M42 * scaleFactor;
            result.M43 = matrix1.M43 * scaleFactor;
            result.M44 = matrix1.M44 * scaleFactor;
            return result;
        }
        
        /// <summary>
        /// Performs multiplication of a Single precision scaling factor 
        /// and aMatrix44 object using the (x*Y) operator.
        /// </summary>
        public static Matrix44 operator * (Single scaleFactor, Matrix44 matrix1)
        {
            Matrix44 result;
            result.M11 = matrix1.M11 * scaleFactor;
            result.M12 = matrix1.M12 * scaleFactor;
            result.M13 = matrix1.M13 * scaleFactor;
            result.M14 = matrix1.M14 * scaleFactor;
            result.M21 = matrix1.M21 * scaleFactor;
            result.M22 = matrix1.M22 * scaleFactor;
            result.M23 = matrix1.M23 * scaleFactor;
            result.M24 = matrix1.M24 * scaleFactor;
            result.M31 = matrix1.M31 * scaleFactor;
            result.M32 = matrix1.M32 * scaleFactor;
            result.M33 = matrix1.M33 * scaleFactor;
            result.M34 = matrix1.M34 * scaleFactor;
            result.M41 = matrix1.M41 * scaleFactor;
            result.M42 = matrix1.M42 * scaleFactor;
            result.M43 = matrix1.M43 * scaleFactor;
            result.M44 = matrix1.M44 * scaleFactor;
            return result;
        }
        
        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Matrix44 objects.
        /// </summary>
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

        /// <summary>
        /// Performs division of a Matrix44 object and a Single precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Matrix44 matrix1, Single divider, out Matrix44 result)
        {
            result.M11 = matrix1.M11 / divider;
            result.M12 = matrix1.M12 / divider;
            result.M13 = matrix1.M13 / divider;
            result.M14 = matrix1.M14 / divider;
            result.M21 = matrix1.M21 / divider;
            result.M22 = matrix1.M22 / divider;
            result.M23 = matrix1.M23 / divider;
            result.M24 = matrix1.M24 / divider;
            result.M31 = matrix1.M31 / divider;
            result.M32 = matrix1.M32 / divider;
            result.M33 = matrix1.M33 / divider;
            result.M34 = matrix1.M34 / divider;
            result.M41 = matrix1.M41 / divider;
            result.M42 = matrix1.M42 / divider;
            result.M43 = matrix1.M43 / divider;
            result.M44 = matrix1.M44 / divider;
        }

        /// <summary>
        /// Performs division of two Matrix44 objects using the (X/Y) operator.
        /// </summary>
        public static Matrix44 operator / (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;

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

            return result;
        }
        
        /// <summary>
        /// Performs division of a Matrix44 object and a Single precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Matrix44 operator / (Matrix44 matrix1, Single divider)
        {
            Matrix44 result;

            result.M11 = matrix1.M11 / divider;
            result.M12 = matrix1.M12 / divider;
            result.M13 = matrix1.M13 / divider;
            result.M14 = matrix1.M14 / divider;
            result.M21 = matrix1.M21 / divider;
            result.M22 = matrix1.M22 / divider;
            result.M23 = matrix1.M23 / divider;
            result.M24 = matrix1.M24 / divider;
            result.M31 = matrix1.M31 / divider;
            result.M32 = matrix1.M32 / divider;
            result.M33 = matrix1.M33 / divider;
            result.M34 = matrix1.M34 / divider;
            result.M41 = matrix1.M41 / divider;
            result.M42 = matrix1.M42 / divider;
            result.M43 = matrix1.M43 / divider;
            result.M44 = matrix1.M44 / divider;

            return result;
        }

        /// <summary>
        /// beware, doing this might not produce what you expect.  you likely
        /// want to lerp between quaternions.
        /// </summary>
        public static void Lerp (ref Matrix44 matrix1, ref Matrix44 matrix2, Single amount, out Matrix44 result)
        {
            Single zero = 0;
            Single one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

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


    }

    /// <summary>
    /// Single precision Quaternion.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Quaternion 
        : IEquatable<Quaternion>
    {
        /// <summary>
        /// todo
        /// </summary>
        public Single X;

        /// <summary>
        /// todo
        /// </summary>
        public Single Y;

        /// <summary>
        /// todo
        /// </summary>
        public Single Z;

        /// <summary>
        /// todo
        /// </summary>
        public Single W;

        /// <summary>
        /// todo
        /// </summary>
        public Quaternion (Single x, Single y, Single z, Single w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// todo
        /// </summary>
        public Quaternion (Vector3 vectorPart, Single scalarPart)
        {
            this.X = vectorPart.X;
            this.Y = vectorPart.Y;
            this.Z = vectorPart.Z;
            this.W = scalarPart;
        }

        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString (), this.W.ToString () });
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ()) + this.W.GetHashCode ());
        }

        /// <summary>
        /// todo
        /// </summary>
        public Single LengthSquared ()
        {
            return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
        }

        /// <summary>
        /// todo
        /// </summary>
        public Single Length ()
        {
            Single num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
            return RealMaths.Sqrt (num);
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        public Boolean IsUnit()
        {
            Single one = 1;

            return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void Conjugate ()
        {
            this.X = -this.X;
            this.Y = -this.Y;
            this.Z = -this.Z;
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines the identity quaternion.
        /// </summary>
        static Quaternion identity;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Quaternion ()
        {
            identity = new Quaternion (0, 0, 0, 1);
        }

        /// <summary>
        /// Returns the identity Quaternion.
        /// </summary>
        public static Quaternion Identity
        {
            get { return identity; }
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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
        /// <summary>
        /// todo
        /// </summary>
        public static void Conjugate (ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = value.W;
        }
        
        /// <summary>
        /// todo
        /// </summary>
        public static void Inverse (ref Quaternion quaternion, out Quaternion result)
        {
            Single one = 1;
            Single a =
                (quaternion.X * quaternion.X) + 
                (quaternion.Y * quaternion.Y) + 
                (quaternion.Z * quaternion.Z) + 
                (quaternion.W * quaternion.W);

            Single b = one / a;

            result.X = -quaternion.X * b;
            result.Y = -quaternion.Y * b;
            result.Z = -quaternion.Z * b;
            result.W =  quaternion.W * b;
        }
        
        /// <summary>
        /// todo
        /// </summary>
        public static void Dot (ref Quaternion quaternion1, ref Quaternion quaternion2, out Single result)
        {
            result = 
                (quaternion1.X * quaternion2.X) + 
                (quaternion1.Y * quaternion2.Y) + 
                (quaternion1.Z * quaternion2.Z) + 
                (quaternion1.W * quaternion2.W);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void Concatenate (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            Single x = value2.X;
            Single y = value2.Y;
            Single z = value2.Z;
            Single w = value2.W;

            Single a = value1.X;
            Single b = value1.Y;
            Single c = value1.Z;
            Single d = value1.W;

            Single e = (y * c) - (z * b);
            Single f = (z * a) - (x * c);
            Single g = (x * b) - (y * a);
            Single h = ((x * a) + (y * b)) + (z * c);

            result.X = (x * d) + (a * w) + e;
            result.Y = (y * d) + (b * w) + f;
            result.Z = (z * d) + (c * w) + g;
            result.W = (w * d) - h;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void Normalise (ref Quaternion quaternion, out Quaternion result)
        {
            Single one = 1;

            Single a = 
                (quaternion.X * quaternion.X) + 
                (quaternion.Y * quaternion.Y) + 
                (quaternion.Z * quaternion.Z) + 
                (quaternion.W * quaternion.W);

            Single b = one / RealMaths.Sqrt (a);

            result.X = quaternion.X * b;
            result.Y = quaternion.Y * b;
            result.Z = quaternion.Z * b;
            result.W = quaternion.W * b;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Quaternion object is equal to another
        /// object
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            
            if (obj is Quaternion)
            {
                flag = this.Equals ((Quaternion) obj);
            }

            return flag;
        }

        #region IEquatable<Quaternion>

        /// <summary>
        /// Determines whether or not this Quaternion object is equal to another
        /// Quaternion object.
        /// </summary>
        public Boolean Equals (Quaternion other)
        {
            return 
                (this.X == other.X) && 
                (this.Y == other.Y) && 
                (this.Z == other.Z) && 
                (this.W == other.W);
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Quaternion objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Quaternion value1, Quaternion value2)
        {
            return ((((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) && (value1.W == value2.W));
        }
        
        /// <summary>
        /// Determines whether or not two Quaternion objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Quaternion value1, Quaternion value2)
        {
            if (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) {
                return !(value1.W == value2.W);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Quaternion objects.
        /// </summary>
        public static void Add (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
            result.W = value1.W + value2.W;
        }

        /// <summary>
        /// Performs addition of two Quaternion objects using the (X+Y) operator. 
        /// </summary>
        public static Quaternion operator + (Quaternion value1, Quaternion value2)
        {
            Quaternion quat;
            quat.X = value1.X + value2.X;
            quat.Y = value1.Y + value2.Y;
            quat.Z = value1.Z + value2.Z;
            quat.W = value1.W + value2.W;
            return quat;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Quaternion objects.
        /// </summary>
        public static void Subtract (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
            result.W = value1.W - value2.W;
        }

        /// <summary>
        /// Performs subtraction of two Quaternion objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Quaternion operator - (Quaternion value1, Quaternion value2)
        {
            Quaternion quat;
            quat.X = value1.X - value2.X;
            quat.Y = value1.Y - value2.Y;
            quat.Z = value1.Z - value2.Z;
            quat.W = value1.W - value2.W;
            return quat;
        }

        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Quaternion object.
        /// </summary>
        public static void Negate (ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        /// <summary>
        /// Performs negation of a Quaternion object using the (-X) operator.
        /// </summary>
        public static Quaternion operator - (Quaternion value)
        {
            Quaternion quat;
            quat.X = -value.X;
            quat.Y = -value.Y;
            quat.Z = -value.Z;
            quat.W = -value.W;
            return quat;
        }
        
        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Quaternion objects.
        /// </summary>
        public static void Multiply (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            Single x1 = value1.X;
            Single y1 = value1.Y;
            Single z1 = value1.Z;
            Single w1 = value1.W;

            Single x2 = value2.X;
            Single y2 = value2.Y;
            Single z2 = value2.Z;
            Single w2 = value2.W;

            Single a = (y1 * z2) - (z1 * y2);
            Single b = (z1 * x2) - (x1 * z2);
            Single c = (x1 * y2) - (y1 * x2);
            Single d = ((x1 * x2) + (y1 * y2)) + (z1 * z2);

            result.X = ((x1 * w2) + (x2 * w1)) + a;
            result.Y = ((y1 * w2) + (y2 * w1)) + b;
            result.Z = ((z1 * w2) + (z2 * w1)) + c;
            result.W = (w1 * w2) - d;
        }

        /// <summary>
        /// Performs multiplication of a Quaternion object and a Single
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Quaternion value1, Single scaleFactor, out Quaternion result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
            result.W = value1.W * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Quaternion objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Quaternion operator * (Quaternion value1, Quaternion value2)
        {
            Quaternion quaternion;
            
            Single x1 = value1.X;
            Single y1 = value1.Y;
            Single z1 = value1.Z;
            Single w1 = value1.W;

            Single x2 = value2.X;
            Single y2 = value2.Y;
            Single z2 = value2.Z;
            Single w2 = value2.W;

            Single a = (y1 * z2) - (z1 * y2);
            Single b = (z1 * x2) - (x1 * z2);
            Single c = (x1 * y2) - (y1 * x2);
            Single d = ((x1 * x2) + (y1 * y2)) + (z1 * z2);

            quaternion.X = ((x1 * w2) + (x2 * w1)) + a;
            quaternion.Y = ((y1 * w2) + (y2 * w1)) + b;
            quaternion.Z = ((z1 * w2) + (z2 * w1)) + c;
            quaternion.W = (w1 * w2) - d;

            return quaternion;
        }
        
        /// <summary>
        /// Performs multiplication of a Quaternion object and a Single
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Quaternion operator * (Quaternion value1, Single scaleFactor)
        {
            Quaternion quat;
            quat.X = value1.X * scaleFactor;
            quat.Y = value1.Y * scaleFactor;
            quat.Z = value1.Z * scaleFactor;
            quat.W = value1.W * scaleFactor;
            return quat;
        }
        
        /// <summary>
        /// Performs multiplication of a Single precision scaling factor 
        /// and aQuaternion object using the (x*Y) operator.
        /// </summary>
        public static Quaternion operator * (Single scaleFactor, Quaternion value1)
        {
            Quaternion quat;
            quat.X = value1.X * scaleFactor;
            quat.Y = value1.Y * scaleFactor;
            quat.Z = value1.Z * scaleFactor;
            quat.W = value1.W * scaleFactor;
            return quat;
        }
        
        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Quaternion objects.
        /// </summary>
        public static void Divide (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            Single one = 1;

            Single x = value1.X;
            Single y = value1.Y;
            Single z = value1.Z;
            Single w = value1.W;

            Single a = 
                (value2.X * value2.X) + 
                (value2.Y * value2.Y) +
                (value2.Z * value2.Z) + 
                (value2.W * value2.W);

            Single b = one / a;

            Single c = -value2.X * b;
            Single d = -value2.Y * b;
            Single e = -value2.Z * b;
            Single f = value2.W * b;

            Single g = (y * e) - (z * d);
            Single h = (z * c) - (x * e);
            Single i = (x * d) - (y * c);
            Single j = ((x * c) + (y * d)) + (z * e);

            result.X = ((x * f) + (c * w)) + g;
            result.Y = ((y * f) + (d * w)) + h;
            result.Z = ((z * f) + (e * w)) + i;
            result.W = (w * f) - j;
        }

        /// <summary>
        /// Performs division of a Quaternion object and a Single precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Quaternion value1, Single divider, out Quaternion result)
        {
            Single one = 1;
            Single a = one / divider;

            result.X = value1.X * a;
            result.Y = value1.Y * a;
            result.Z = value1.Z * a;
            result.W = value1.W * a;
        }

        /// <summary>
        /// Performs division of two Quaternion objects using the (X/Y) operator.
        /// </summary>
        public static Quaternion operator / (Quaternion value1, Quaternion value2)
        {
            Single one = 1;

            Quaternion quaternion;

            Single x = value1.X;
            Single y = value1.Y;
            Single z = value1.Z;
            Single w = value1.W;

            Single a = 
                (value2.X * value2.X) + 
                (value2.Y * value2.Y) +
                (value2.Z * value2.Z) + 
                (value2.W * value2.W);

            Single b = one / a;

            Single c = -value2.X * b;
            Single d = -value2.Y * b;
            Single e = -value2.Z * b;
            Single f =  value2.W * b;

            Single g = (y * e) - (z * d);
            Single h = (z * c) - (x * e);
            Single i = (x * d) - (y * c);
            Single j = (x * c) + (y * d) + (z * e);

            quaternion.X = (x * f) + (c * w) + g;
            quaternion.Y = (y * f) + (d * w) + h;
            quaternion.Z = (z * f) + (e * w) + i;
            quaternion.W = (w * f) - j;

            return quaternion;
        }
        
        /// <summary>
        /// Performs division of a Quaternion object and a Single precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Quaternion operator / (Quaternion value1, Single divider)
        {
            Single one = 1;

            Quaternion quat;

            Single num = one / divider;

            quat.X = value1.X * num;
            quat.Y = value1.Y * num;
            quat.Z = value1.Z * num;
            quat.W = value1.W * num;

            return quat;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void Slerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Single amount, out Quaternion result)
        {
            Single zero = 0;
            Single one = 1;

            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

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

        /// <summary>
        /// todo
        /// </summary>
        public static void Lerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Single amount, out Quaternion result)
        {
            Single zero = 0;
            Single one = 1;

            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

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

    }    /// <summary>
    /// Single precision Vector2.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector2
        : IEquatable<Vector2>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector2.
        /// </summary>
        public Single X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector2.
        /// </summary>
        public Single Y;

        /// <summary>
        /// Initilises a new instance of Vector2 from two Single values
        /// representing X and Y respectively.
        /// </summary>
        public Vector2 (Single x, Single y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Calculates the length of the Vector2.
        /// </summary>
        public Single Length ()
        {
            Single num = (this.X * this.X) + (this.Y * this.Y);
            return RealMaths.Sqrt (num);
        }

        /// <summary>
        /// Calculates the length of the Vector2 squared.
        /// </summary>
        public Single LengthSquared ()
        {
            return ((this.X * this.X) + (this.Y * this.Y));
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format (
                "{{X:{0} Y:{1}}}",
                new Object[]
                {
                    this.X.ToString (),
                    this.Y.ToString ()
                }
                );
        }

        /// <summary>
        /// Gets the hash code of the Vector2 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (this.X.GetHashCode () + this.Y.GetHashCode ());
        }

        /// <summary>
        /// Detemines whether or not the Vector2 is of unit length.
        /// </summary>
        public Boolean IsUnit()
        {
            Single one = 1;
            return RealMaths.IsZero(one - X*X - Y*Y);
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Vector2 with all of its components set to zero.
        /// </summary>
        readonly static Vector2 zero;

        /// <summary>
        /// Defines a Vector2 with all of its components set to one.
        /// </summary>
        readonly static Vector2 one;

        /// <summary>
        /// Defines the unit Vector2 for the X-axis.
        /// </summary>
        readonly static Vector2 unitX;

        /// <summary>
        /// Defines the unit Vector2 for the Y-axis.
        /// </summary>
        readonly static Vector2 unitY;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Vector2 ()
        {
            zero =      new Vector2 ();
            one =       new Vector2 (1, 1);

            unitX =     new Vector2 (1, 0);
            unitY =     new Vector2 (0, 1);
        }

        /// <summary>
        /// Returns a Vector2 with all of its components set to zero.
        /// </summary>
        public static Vector2 Zero
        {
            get { return zero; }
        }
        
        /// <summary>
        /// Returns a Vector2 with all of its components set to one.
        /// </summary>
        public static Vector2 One
        {
            get { return one; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the X-axis.
        /// </summary>
        public static Vector2 UnitX
        {
            get { return unitX; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the Y-axis.
        /// </summary>
        public static Vector2 UnitY
        {
            get { return unitY; }
        }

        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static void Distance (
            ref Vector2 vector1, ref Vector2 vector2, out Single result)
        {
            Single dx = vector1.X - vector2.X;
            Single dy = vector1.Y - vector2.Y;

            Single lengthSquared = (dx * dx) + (dy * dy);

            result = RealMaths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector2 vector1, ref Vector2 vector2, out Single result)
        {
            Single dx = vector1.X - vector2.X;
            Single dy = vector1.Y - vector2.Y;

            result = (dx * dx) + (dy * dy);
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static void Dot (
            ref Vector2 vector1, ref Vector2 vector2, out Single result)
        {
            result = (vector1.X * vector2.X) + (vector1.Y * vector2.Y);
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static void Normalise (ref Vector2 vector, out Vector2 result)
        {
            Single lengthSquared =
                (vector.X * vector.X) + (vector.Y * vector.Y);

            Single epsilon; RealMaths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Single.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single one = 1;
            Single multiplier = one / RealMaths.Sqrt (lengthSquared);

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;

        }

        /// <summary>
        /// Returns the vector of an incident vector reflected across the a
        /// specified normal vector.
        /// </summary>
        public static void Reflect (
            ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            // dot = vector . normal
            //     = |vector| * [normal] * cosθ
            //     = |vector| * cosθ
            //     = adjacent
            Single dot;
            Dot(ref vector, ref normal, out dot);

            Single two = 2;
            Single twoDot = dot * two;

            // Starting vector minus twice the length of the adjcent projected
            // along the normal.
            result = vector - (twoDot * normal);
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector2 vector, ref Matrix44 matrix, out Vector2 result)
        {
            Single x =
                (vector.X * matrix.M11) +
                (vector.Y * matrix.M21) +
                matrix.M41;

            Single y =
                (vector.X * matrix.M12) +
                (vector.Y * matrix.M22) +
                matrix.M42;

            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector2 vector, ref Quaternion rotation, out Vector2 result)
        {
            Single two = 2;

            Single x = rotation.X;
            Single y = rotation.Y;
            Single z = rotation.Z;
            Single w = rotation.W;

            Single xx = x * x;
            Single yy = y * y;
            Single zz = z * z;

            Single wz = w * z;
            Single xy = x * y;

            result.X =
                + vector.X
                - (two * vector.X  * (yy + zz))
                + (two * vector.Y  * (xy - wz));

            result.Y =
                + vector.Y
                + (two * vector.X * (xy + wz))
                - (two * vector.Y * (xx + zz));
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix.
        /// </summary>
        public static void TransformNormal (
            ref Vector2 normal, ref Matrix44 matrix, out Vector2 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Single x = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
            Single y = (normal.X * matrix.M12) + (normal.Y * matrix.M22);

            result.X = x;
            result.Y = y;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Vector2 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Vector2) {
                flag = this.Equals ((Vector2)obj);
            }
            return flag;
        }

        #region IEquatable<Vector2>

        /// <summary>
        /// Determines whether or not this Vector2 object is equal to another
        /// Vector2 object.
        /// </summary>
        public Boolean Equals (Vector2 other)
        {
            return ((this.X == other.X) && (this.Y == other.Y));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Vector2 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Vector2 value1, Vector2 value2)
        {
            return ((value1.X == value2.X) && (value1.Y == value2.Y));
        }

        /// <summary>
        /// Determines whether or not two Vector2 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Vector2 value1, Vector2 value2)
        {
            if (value1.X == value2.X) {
                return !(value1.Y == value2.Y);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector2 objects.
        /// </summary>
        public static void Add (
            ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        /// <summary>
        /// Performs addition of two Vector2 objects using the (X+Y) operator. 
        /// </summary>
        public static Vector2 operator + (Vector2 value1, Vector2 value2)
        {
            Vector2 vector;
            vector.X = value1.X + value2.X;
            vector.Y = value1.Y + value2.Y;
            return vector;
        }


        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector2 objects.
        /// </summary>
        public static void Subtract (
            ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }

        /// <summary>
        /// Performs subtraction of two Vector2 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Vector2 operator - (Vector2 value1, Vector2 value2)
        {
            Vector2 vector;
            vector.X = value1.X - value2.X;
            vector.Y = value1.Y - value2.Y;
            return vector;
        }


        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Vector2 object.
        /// </summary>
        public static void Negate (ref Vector2 value, out Vector2 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
        }

        /// <summary>
        /// Performs negation of a Vector2 object using the (-X) operator.
        /// </summary>
        public static Vector2 operator - (Vector2 value)
        {
            Vector2 vector;
            vector.X = -value.X;
            vector.Y = -value.Y;
            return vector;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector2 objects.
        /// </summary>
        public static void Multiply (
            ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
        }

        /// <summary>
        /// Performs multiplication of a Vector2 object and a Single
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Vector2 value, Single scaleFactor, out Vector2 result)
        {
            result.X = value.X * scaleFactor;
            result.Y = value.Y * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Vector2 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Vector2 operator * (
            Vector2 value1, Vector2 value2)
        {
            Vector2 vector;
            vector.X = value1.X * value2.X;
            vector.Y = value1.Y * value2.Y;
            return vector;
        }

        /// <summary>
        /// Performs multiplication of a Vector2 object and a Single
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Vector2 operator * (
            Vector2 value, Single scaleFactor)
        {
            Vector2 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            return vector;
        }

        /// <summary>
        /// Performs multiplication of a Single precision scaling factor 
        /// and aVector2 object using the (x*Y) operator.
        /// </summary>
        public static Vector2 operator * (
            Single scaleFactor, Vector2 value)
        {
            Vector2 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            return vector;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector2 objects.
        /// </summary>
        public static void Divide (
            ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
        }

        /// <summary>
        /// Performs division of a Vector2 object and a Single precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Vector2 value1, Single divider, out Vector2 result)
        {
            Single one = 1;
            Single num = one / divider;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
        }

        /// <summary>
        /// Performs division of two Vector2 objects using the (X/Y) operator.
        /// </summary>
        public static Vector2 operator / (Vector2 value1, Vector2 value2)
        {
            Vector2 vector;
            vector.X = value1.X / value2.X;
            vector.Y = value1.Y / value2.Y;
            return vector;
        }

        /// <summary>
        /// Performs division of a Vector2 object and a Single precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Vector2 operator / (Vector2 value1, Single divider)
        {
            Vector2 vector;
            Single one = 1;
            Single num = one / divider;
            vector.X = value1.X * num;
            vector.Y = value1.Y * num;
            return vector;
        }
        
        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector2 a,
            ref Vector2 b,
            Single amount,
            out Vector2 result)
        {
            Single zero = 0;
            Single one = 1;
            Single two = 2;
            Single three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            amount = (amount * amount) * (three - (two * amount));

            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// Features:
        /// - The spline passes through all of the control points.
        /// - The spline is C^1 continuous, meaning that there are no
        ///   discontinuities in the tangent direction and magnitude.
        /// - The spline is not C^2 continuous.  The second derivative is
        ///   linearly interpolated within each segment, causing the curvature
        ///   to vary linearly over the length of the segment.
        /// </summary>
        public static void CatmullRom (
            ref Vector2 a,
            ref Vector2 b,
            ref Vector2 c,
            ref Vector2 d,
            Single amount,
            out Vector2 result)
        {
            Single zero = 0;
            Single one = 1;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single half; RealMaths.Half(out half);
            Single two = 2;
            Single three = 3;
            Single four = 4;
            Single five = 5;

            Single squared = amount * amount;
            Single cubed = amount * squared;

            result.X =
                half * ((two * b.X) + ((-a.X + c.X) * amount) +
                (((two * a.X) - (five * b.X) + (four * c.X) - d.X) * squared) +
                ((-a.X + (three * b.X) - (three * c.X) + d.X) * cubed));

            result.Y = half * ((two * b.Y) + ((-a.Y + c.Y) * amount) +
                (((two * a.Y) - (five * b.Y) + (four * c.Y) - d.Y) * squared) +
                ((-a.Y + (three * b.Y) - (three * c.Y) + d.Y) * cubed));
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector2 vector1,
            ref Vector2 tangent1,
            ref Vector2 vector2,
            ref Vector2 tangent2,
            Single amount,
            out Vector2 result)
        {
            Single zero = 0;
            Single one = 1;
            Single two = 2;
            Single three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            if( !tangent1.IsUnit() || !tangent2.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single squared = amount * amount;
            Single cubed = amount * squared;

            Single a = ((two * cubed) - (three * squared)) + one;
            Single b = (-two * cubed) + (three * squared);
            Single c = (cubed - (two * squared)) + amount;
            Single d = cubed - squared;

            result.X =
                (vector1.X * a) + (vector2.X * b) +
                (tangent1.X * c) + (tangent2.X * d);

            result.Y =
                (vector1.Y * a) + (vector2.Y * b) +
                (tangent1.Y * c) + (tangent2.Y * d);
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Min (
            ref Vector2 a, 
            ref Vector2 b, 
            out Vector2 result)
        {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Max (
            ref Vector2 a, 
            ref Vector2 b, 
            out Vector2 result)
        {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static void Clamp (
            ref Vector2 a, 
            ref Vector2 min, 
            ref Vector2 max, 
            out Vector2 result)
        {
            Single x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            
            Single y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;

            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static void Lerp (
            ref Vector2 a, 
            ref Vector2 b, 
            Single amount, 
            out Vector2 result)
        {
            Single zero = 0;
            Single one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }
            
            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
        }


        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static Single Distance (
            Vector2 vector1, Vector2 vector2)
        {
            Single result;
            Distance (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static Single DistanceSquared (
            Vector2 vector1, Vector2 vector2)
        {
            Single result;
            DistanceSquared (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static Single Dot (
            Vector2 vector1, Vector2 vector2)
        {
            Single result;
            Dot (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static Vector2 Normalise (Vector2 vector)
        {
            Vector2 result;
            Normalise (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Returns the vector of an incident vector reflected across the a
        /// specified normal vector.
        /// </summary>
        public static Vector2 Reflect (
            Vector2 vector, Vector2 normal)
        {
            Vector2 result;
            Reflect (ref vector, ref normal, out result);
            return result;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix44.
        /// </summary>
        public static Vector2 Transform (
            Vector2 vector, Matrix44 matrix)
        {
            Vector2 result;
            Transform (ref vector, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Quaternion.
        /// </summary>
        public static Vector2 Transform (
            Vector2 vector, Quaternion rotation)
        {
            Vector2 result;
            Transform (ref vector, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix.
        /// </summary>
        public static Vector2 TransformNormal (
            Vector2 normal, Matrix44 matrix)
        {
            Vector2 result;
            TransformNormal (ref normal, ref matrix, out result);
            return result;
        }


        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector2 objects.
        /// </summary>
        public static Vector2 Add (
            Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Add (ref value1, ref value2, out result);
            return result;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector2 objects.
        /// </summary>
        public static Vector2 Subtract (
            Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Subtract (ref value1, ref value2, out result);
            return result;
        }

        // Negation Operators //----------------------------------------------//

        /// <summary>
        /// Performs negation of a Vector2 object.
        /// </summary>
        public static Vector2 Negate (Vector2 value)
        {
            Vector2 result;
            Negate (ref value, out result);
            return result;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector2 objects.
        /// </summary>
        public static Vector2 Multiply (
            Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Multiply (ref value1, ref value2, out result);
            return result;
        }

        /// <summary>
        /// Performs multiplication of a Vector2 object and a Single
        /// precision scaling factor.
        /// </summary>
        public static Vector2 Multiply (
            Vector2 value, Single scaleFactor)
        {
            Vector2 result;
            Multiply (ref value, scaleFactor, out result);
            return result;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector2 objects.
        /// </summary>
        public static Vector2 Divide (
            Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Divide (ref value1, ref value2, out result);
            return result;
        }

        /// <summary>
        /// Performs division of a Vector2 object and a Single precision
        /// scaling factor.
        /// </summary>
        public static Vector2 Divide (
            Vector2 value1, Single divider)
        {
            Vector2 result;
            Divide (ref value1, divider, out result);
            return result;
        }

        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static Vector2 SmoothStep (
            Vector2 a,
            Vector2 b,
            Single amount)
        {
            Vector2 result;
            SmoothStep (ref a, ref b, amount, out result);
            return result;
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        public static Vector2 CatmullRom (
            Vector2 a,
            Vector2 b,
            Vector2 c,
            Vector2 d,
            Single amount)
        {
            Vector2 result;
            CatmullRom (ref a, ref b, ref c, ref d, amount, out result);
            return result;
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static Vector2 Hermite (
            Vector2 vector1,
            Vector2 tangent1,
            Vector2 vector2,
            Vector2 tangent2,
            Single amount)
        {
            Vector2 result;
            Hermite (
                ref vector1, ref tangent1,
                ref vector2, ref tangent2,
                amount, out result);
            return result;
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching
        /// pair of components.
        /// </summary>
        public static Vector2 Min (
            Vector2 a,
            Vector2 b)
        {
            Vector2 result;
            Min (ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching
        /// pair of components.
        /// </summary>
        public static Vector2 Max (
            ref Vector2 a,
            ref Vector2 b)
        {
            Vector2 result;
            Max (ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static Vector2 Clamp (
            ref Vector2 a,
            ref Vector2 min,
            ref Vector2 max)
        {
            Vector2 result;
            Clamp (ref a, ref min, ref max, out result);
            return result;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static Vector2 Lerp (
            ref Vector2 a,
            ref Vector2 b,
            Single amount)
        {
            Vector2 result;
            Lerp (ref a, ref b, amount, out result);
            return result;
        }

    }

    /// <summary>
    /// Single precision Vector3.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector3 
        : IEquatable<Vector3>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector3.
        /// </summary>
        public Single X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector3.
        /// </summary>
        public Single Y;

        /// <summary>
        /// Gets or sets the Z-component of the Vector3.
        /// </summary>
        public Single Z;

        /// <summary>
        /// Initilises a new instance of Vector3 from three Single values 
        /// representing X, Y and Z respectively.
        /// </summary>
        public Vector3 (Single x, Single y, Single z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        
        /// <summary>
        /// Initilises a new instance of Vector3 from one Vector2 value
        /// representing X and Y and one Single value representing Z.
        /// </summary>
        public Vector3 (Vector2 value, Single z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }

        /// <summary>
        /// Calculates the length of the Vector3.
        /// </summary>
        public Single Length ()
        {
            Single num = (this.X * this.X) + 
                              (this.Y * this.Y) + 
                              (this.Z * this.Z);

            return RealMaths.Sqrt (num);
        }

        /// <summary>
        /// Calculates the length of the Vector3 squared.
        /// </summary>
        public Single LengthSquared ()
        {
            return
                (this.X * this.X) + 
                (this.Y * this.Y) + 
                (this.Z * this.Z);
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format (
                "{{X:{0} Y:{1} Z:{2}}}", 
                new Object[] 
                { 
                    this.X.ToString (), 
                    this.Y.ToString (), 
                    this.Z.ToString () 
                }
                );
        }

        /// <summary>
        /// Gets the hash code of the Vector3 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (
                this.X.GetHashCode () + 
                this.Y.GetHashCode () + 
                this.Z.GetHashCode ()
                );
        }

        /// <summary>
        /// Detemines whether or not the Vector3 is of unit length.
        /// </summary>
        public Boolean IsUnit()
        {
            Single one = 1;
            return RealMaths.IsZero(one - X*X - Y*Y - Z*Z);
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Vector3 with all of its components set to zero.
        /// </summary>
        static Vector3 zero;

        /// <summary>
        /// Defines a Vector3 with all of its components set to one.
        /// </summary>
        static Vector3 one;

        /// <summary>
        /// Defines the unit Vector3 for the X-axis.
        /// </summary>
        static Vector3 unitX;

        /// <summary>
        /// Defines the unit Vector3 for the Y-axis.
        /// </summary>
        static Vector3 unitY;

        /// <summary>
        /// Defines the unit Vector3 for the Z-axis.
        /// </summary>
        static Vector3 unitZ;

        /// <summary>
        /// Defines a unit Vector3 designating up (0, 1, 0).
        /// </summary>
        static Vector3 up;

        /// <summary>
        /// Defines a unit Vector3 designating down (0, −1, 0).
        /// </summary>
        static Vector3 down;

        /// <summary>
        /// Defines a unit Vector3 pointing to the right (1, 0, 0).
        /// </summary>
        static Vector3 right;

        /// <summary>
        /// Defines a unit Vector3 designating left (−1, 0, 0).
        /// </summary>
        static Vector3 left;

        /// <summary>
        /// Defines a unit Vector3 designating forward in a right-handed 
        /// coordinate system (0, 0, -1).
        /// </summary>
        static Vector3 forward;

        /// <summary>
        /// Defines a unit Vector3 designating backward in a right-handed 
        /// coordinate system (0, 0, 1).
        /// </summary>
        static Vector3 backward;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Vector3 ()
        {
            zero =      new Vector3 ();
            one =       new Vector3 ( 1,  1,  1);

            unitX =     new Vector3 ( 1,  0,  0);
            unitY =     new Vector3 ( 0,  1,  0);
            unitZ =     new Vector3 ( 0,  0,  1);

            up =        new Vector3 ( 0,  1,  0);
            down =      new Vector3 ( 0, -1,  0);
            right =     new Vector3 ( 1,  0,  0);
            left =      new Vector3 (-1,  0,  0);
            forward =   new Vector3 ( 0,  0, -1);
            backward =  new Vector3 ( 0,  0,  1);
        }
        
        /// <summary>
        /// Returns a Vector3 with all of its components set to zero.
        /// </summary>
        public static Vector3 Zero
        {
            get { return zero; }
        }
        
        /// <summary>
        /// Returns a Vector3 with all of its components set to one.
        /// </summary>
        public static Vector3 One
        {
            get { return one; }
        }
        
        /// <summary>
        /// Returns the unit Vector3 for the X-axis.
        /// </summary>
        public static Vector3 UnitX
        {
            get { return unitX; }
        }

        /// <summary>
        /// Returns the unit Vector3 for the Y-axis.
        /// </summary>
        public static Vector3 UnitY
        {
            get { return unitY; }
        }
        
        /// <summary>
        /// Returns the unit Vector3 for the Z-axis.
        /// </summary>
        public static Vector3 UnitZ
        {
            get { return unitZ; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating up (0, 1, 0).
        /// </summary>
        public static Vector3 Up
        {
            get { return up; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating down (0, −1, 0).
        /// </summary>
        public static Vector3 Down
        {
            get { return down; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 pointing to the right (1, 0, 0).
        /// </summary>
        public static Vector3 Right
        {
            get { return right; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating left (−1, 0, 0).
        /// </summary>
        public static Vector3 Left
        {
            get { return left; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating forward in a right-handed 
        /// coordinate system (0, 0, -1).
        /// </summary>
        public static Vector3 Forward
        {
            get { return forward; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating backward in a right-handed 
        /// coordinate system (0, 0, 1).
        /// </summary>
        public static Vector3 Backward
        {
            get { return backward; }
        }
        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static void Distance (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Single result)
        {
            Single dx = vector1.X - vector2.X;
            Single dy = vector1.Y - vector2.Y;
            Single dz = vector1.Z - vector2.Z;

            Single lengthSquared = (dx * dx) + (dy * dy) + (dz * dz);

            result = RealMaths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Single result)
        {
            Single dx = vector1.X - vector2.X;
            Single dy = vector1.Y - vector2.Y;
            Single dz = vector1.Z - vector2.Z;

            result = (dx * dx) + (dy * dy) + (dz * dz);
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static void Dot (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Single result)
        {
            result =
                (vector1.X * vector2.X) +
                (vector1.Y * vector2.Y) +
                (vector1.Z * vector2.Z);
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static void Normalise (ref Vector3 vector, out Vector3 result)
        {
            Single lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z);

            Single epsilon; RealMaths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Single.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single one = 1;
            Single multiplier = one / RealMaths.Sqrt (lengthSquared);

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
        }

        /// <summary>
        /// Calculates the cross product of two vectors.
        /// </summary>
        public static void Cross (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Vector3 result)
        {
            result.X = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y);
            result.Y = (vector1.Z * vector2.X) - (vector1.X * vector2.Z);
            result.Z = (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
        }

        /// <summary>
        /// Returns the vector of an incident vector reflected across the a
        /// specified normal vector.
        /// </summary>
        public static void Reflect (
            ref Vector3 vector,
            ref Vector3 normal,
            out Vector3 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single two = 2;

            Single num =
                (vector.X * normal.X) +
                (vector.Y * normal.Y) +
                (vector.Z * normal.Z);

            result.X = vector.X - ((two * num) * normal.X);
            result.Y = vector.Y - ((two * num) * normal.Y);
            result.Z = vector.Z - ((two * num) * normal.Z);
        }

        /// <summary>
        /// Transforms a Vector3 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector3 vector,
            ref Matrix44 matrix,
            out Vector3 result)
        {
            Single x =
                (vector.X * matrix.M11) +
                (vector.Y * matrix.M21) +
                (vector.Z * matrix.M31) + matrix.M41;

            Single y =
                (vector.X * matrix.M12) +
                (vector.Y * matrix.M22) +
                (vector.Z * matrix.M32) + matrix.M42;

            Single z =
                (vector.X * matrix.M13) +
                (vector.Y * matrix.M23) +
                (vector.Z * matrix.M33) + matrix.M43;

            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Transforms a vector by a specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector3 vector,
            ref Quaternion rotation,
            out Vector3 result)
        {
            Single two = 2;

            Single x = rotation.X;
            Single y = rotation.Y;
            Single z = rotation.Z;
            Single w = rotation.W;

            Single xx = x * x;
            Single yy = y * y;
            Single zz = z * z;

            Single wx = w * x;
            Single wy = w * y;
            Single wz = w * z;
            Single xy = x * y;
            Single xz = x * z;
            Single yz = y * z;

            result.X =
                + vector.X
                - (two * vector.X * (yy + zz))
                + (two * vector.Y * (xy - wz))
                + (two * vector.Z * (xz + wy));

            result.Y =
                + vector.Y
                + (two * vector.X * (xy + wz))
                - (two * vector.Y * (xx + zz))
                + (two * vector.Z * (yz - wx));

            result.Z =
                + vector.Z
                + (two * vector.X * (xz - wy))
                + (two * vector.Y * (yz + wx))
                - (two * vector.Z * (xx + yy));
        }

        /// <summary>
        /// Transforms a normalised Vector3 by a Matrix44.
        /// </summary>
        public static void TransformNormal (
            ref Vector3 normal,
            ref Matrix44 matrix,
            out Vector3 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Single x =
                (normal.X * matrix.M11) +
                (normal.Y * matrix.M21) +
                (normal.Z * matrix.M31);

            Single y =
                (normal.X * matrix.M12) +
                (normal.Y * matrix.M22) +
                (normal.Z * matrix.M32);

            Single z =
                (normal.X * matrix.M13) +
                (normal.Y * matrix.M23) +
                (normal.Z * matrix.M33);

            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Vector3 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Vector3) {
                flag = this.Equals ((Vector3)obj);
            }
            return flag;
        }

        #region IEquatable<Vector3>

        /// <summary>
        /// Determines whether or not this Vector3 object is equal to another
        /// Vector3 object.
        /// </summary>
        public Boolean Equals (Vector3 other)
        {
            return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Vector3 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Vector3 value1, Vector3 value2)
        {
            return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z));
        }

        /// <summary>
        /// Determines whether or not two Vector3 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Vector3 value1, Vector3 value2)
        {
            if ((value1.X == value2.X) && (value1.Y == value2.Y)) {
                return !(value1.Z == value2.Z);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector3 objects.
        /// </summary>
        public static void Add (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
        }

        /// <summary>
        /// Performs addition of two Vector3 objects using the (X+Y) operator. 
        /// </summary>
        public static Vector3 operator + (Vector3 value1, Vector3 value2)
        {
            Vector3 vector;
            vector.X = value1.X + value2.X;
            vector.Y = value1.Y + value2.Y;
            vector.Z = value1.Z + value2.Z;
            return vector;
        }


        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector3 objects.
        /// </summary>
        public static void Subtract (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        /// <summary>
        /// Performs subtraction of two Vector3 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Vector3 operator - (Vector3 value1, Vector3 value2)
        {
            Vector3 vector;
            vector.X = value1.X - value2.X;
            vector.Y = value1.Y - value2.Y;
            vector.Z = value1.Z - value2.Z;
            return vector;
        }


        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Vector3 object.
        /// </summary>
        public static void Negate (ref Vector3 value, out Vector3 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
        }

        /// <summary>
        /// Performs negation of a Vector3 object using the (-X) operator.
        /// </summary>
        public static Vector3 operator - (Vector3 value)
        {
            Vector3 vector;
            vector.X = -value.X;
            vector.Y = -value.Y;
            vector.Z = -value.Z;
            return vector;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector3 objects.
        /// </summary>
        public static void Multiply (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
        }

        /// <summary>
        /// Performs multiplication of a Vector3 object and a Single
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Vector3 value1, Single scaleFactor, out Vector3 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Vector3 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Vector3 operator * (Vector3 value1, Vector3 value2)
        {
            Vector3 vector;
            vector.X = value1.X * value2.X;
            vector.Y = value1.Y * value2.Y;
            vector.Z = value1.Z * value2.Z;
            return vector;
        }

        /// <summary>
        /// Performs multiplication of a Vector3 object and a Single
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Vector3 operator * (Vector3 value, Single scaleFactor)
        {
            Vector3 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            vector.Z = value.Z * scaleFactor;
            return vector;
        }

        /// <summary>
        /// Performs multiplication of a Single precision scaling factor 
        /// and aVector3 object using the (x*Y) operator.
        /// </summary>
        public static Vector3 operator * (Single scaleFactor, Vector3 value)
        {
            Vector3 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            vector.Z = value.Z * scaleFactor;
            return vector;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector3 objects.
        /// </summary>
        public static void Divide (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
        }

        /// <summary>
        /// Performs division of a Vector3 object and a Single precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Vector3 value1, Single value2, out Vector3 result)
        {
            Single one = 1;
            Single num = one / value2;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
        }

        /// <summary>
        /// Performs division of two Vector3 objects using the (X/Y) operator.
        /// </summary>
        public static Vector3 operator / (Vector3 value1, Vector3 value2)
        {
            Vector3 vector;
            vector.X = value1.X / value2.X;
            vector.Y = value1.Y / value2.Y;
            vector.Z = value1.Z / value2.Z;
            return vector;
        }

        /// <summary>
        /// Performs division of a Vector3 object and a Single precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
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
        
        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector3 vector1,
            ref Vector3 vector2,
            Single amount,
            out Vector3 result)
        {
            Single zero = 0;
            Single one = 1;
            Single two = 2;
            Single three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
            amount = (amount * amount) * (three - (two * amount));

            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
            result.Z = vector1.Z + ((vector2.Z - vector1.Z) * amount);
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// Features:
        /// - The spline passes through all of the control points.
        /// - The spline is C^1 continuous, meaning that there are no
        ///   discontinuities in the tangent direction and magnitude.
        /// - The spline is not C^2 continuous.  The second derivative is
        ///   linearly interpolated within each segment, causing the curvature
        ///   to vary linearly over the length of the segment.
        /// </summary>
        public static void CatmullRom (
            ref Vector3 vector1,
            ref Vector3 vector2,
            ref Vector3 vector3,
            ref Vector3 vector4,
            Single amount,
            out Vector3 result)
        {
            Single half; RealMaths.Half(out half);
            Single zero = 0;
            Single one = 1;
            Single two = 2;
            Single three = 3;
            Single four = 4;
            Single five = 5;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single squared = amount * amount;
            Single cubed = amount * squared;

            result.X =
                half * ((((two * vector2.X) + ((-vector1.X + vector3.X) *
                amount)) + (((((two * vector1.X) - (five * vector2.X)) + (four *
                vector3.X)) - vector4.X) * squared)) + ((((-vector1.X + (three *
                vector2.X)) - (three * vector3.X)) + vector4.X) * cubed));

            result.Y =
                half * ((((two * vector2.Y) + ((-vector1.Y + vector3.Y) *
                amount)) + (((((two * vector1.Y) - (five * vector2.Y)) + (four *
                vector3.Y)) - vector4.Y) * squared)) + ((((-vector1.Y + (three *
                vector2.Y)) - (three * vector3.Y)) + vector4.Y) * cubed));

            result.Z =
                half * ((((two * vector2.Z) + ((-vector1.Z + vector3.Z) *
                amount)) + (((((two * vector1.Z) - (five * vector2.Z)) + (four *
                vector3.Z)) - vector4.Z) * squared)) + ((((-vector1.Z + (three *
                vector2.Z)) - (three * vector3.Z)) + vector4.Z) * cubed));
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector3 vector1,
            ref Vector3 tangent1,
            ref Vector3 vector2,
            ref Vector3 tangent2,
            Single amount,
            out Vector3 result)
        {
            Single zero = 0;
            Single one = 1;
            Single two = 2;
            Single three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            if( !tangent1.IsUnit() || !tangent2.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single squared = amount * amount;
            Single cubed = amount * squared;

            Single a = ((two * cubed) - (three * squared)) + one;
            Single b = (-two * cubed) + (three * squared);
            Single c = (cubed - (two * squared)) + amount;
            Single d = cubed - squared;

            result.X =
                (vector1.X * a) + (vector2.X * b) +
                (tangent1.X * c) + (tangent2.X * d);

            result.Y =
                (vector1.Y * a) + (vector2.Y * b) +
                (tangent1.Y * c) + (tangent2.Y * d);

            result.Z =
                (vector1.Z * a) + (vector2.Z * b) +
                (tangent1.Z * c) + (tangent2.Z * d);
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Min (
            ref Vector3 a,
            ref Vector3 b,
            out Vector3 result)
        {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
            result.Z = (a.Z < b.Z) ? a.Z : b.Z;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Max (
            ref Vector3 a,
            ref Vector3 b,
            out Vector3 result)
        {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
            result.Z = (a.Z > b.Z) ? a.Z : b.Z;
        }
        
        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static void Clamp (
            ref Vector3 a,
            ref Vector3 min,
            ref Vector3 max,
            out Vector3 result)
        {
            Single x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            Single y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            Single z = a.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static void Lerp (
            ref Vector3 a,
            ref Vector3 b,
            Single amount,
            out Vector3 result)
        {
            Single zero = 0;
            Single one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }
            
            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
            result.Z = a.Z + ((b.Z - a.Z) * amount);
        }
    }

    /// <summary>
    /// Single precision Vector4.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector4 
        : IEquatable<Vector4>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector4.
        /// </summary>
        public Single X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector4.
        /// </summary>
        public Single Y;

        /// <summary>
        /// Gets or sets the Z-component of the Vector4.
        /// </summary>
        public Single Z;

        /// <summary>
        /// Gets or sets the W-component of the Vector4.
        /// </summary>
        public Single W;

        /// <summary>
        /// Initilises a new instance of Vector4 from four Single values 
        /// representing X, Y, Z and W respectively.
        /// </summary>
        public Vector4 (
            Single x, 
            Single y, 
            Single z, 
            Single w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initilises a new instance of Vector4 from one Vector2 value
        /// representing X and Y and two Single values representing Z and
        /// W respectively.
        /// </summary>
        public Vector4 (Vector2 value, Single z, Single w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initilises a new instance of Vector4 from one Vector3 value
        /// representing X, Y and Z and one Single value representing W.
        /// </summary>
        public Vector4 (Vector3 value, Single w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        /// <summary>
        /// Calculates the length of the Vector4.
        /// </summary>
        public Single Length ()
        {
            Single num = (this.X * this.X) + 
                              (this.Y * this.Y) + 
                              (this.Z * this.Z) + 
                              (this.W * this.W);
            
            return RealMaths.Sqrt (num);
        }

        /// <summary>
        /// Calculates the length of the Vector4 squared.
        /// </summary>
        public Single LengthSquared ()
        {
            return 
                (this.X * this.X) + 
                (this.Y * this.Y) + 
                (this.Z * this.Z) + 
                (this.W * this.W);
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format (
                "{{X:{0} Y:{1} Z:{2} W:{3}}}", 
                new Object[] 
                { 
                    this.X.ToString (), 
                    this.Y.ToString (), 
                    this.Z.ToString (), 
                    this.W.ToString () 
                }
                );
        }

        /// <summary>
        /// Gets the hash code of the Vector4 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (
                this.X.GetHashCode () + 
                this.Y.GetHashCode () + 
                this.Z.GetHashCode () + 
                this.W.GetHashCode ()
                );
        }

        /// <summary>
        /// Detemines whether or not the Vector4 is of unit length.
        /// </summary>
        public Boolean IsUnit()
        {
            Single one = 1;
            return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Vector2 with all of its components set to zero.
        /// </summary>
        static Vector4 zero;

        /// <summary>
        /// Defines a Vector2 with all of its components set to one.
        /// </summary>
        static Vector4 one;

        /// <summary>
        /// Defines the unit Vector2 for the X-axis.
        /// </summary>
        static Vector4 unitX;

        /// <summary>
        /// Defines the unit Vector2 for the Y-axis.
        /// </summary>
        static Vector4 unitY;

        /// <summary>
        /// Defines the unit Vector2 for the Z-axis.
        /// </summary>
        static Vector4 unitZ;

        /// <summary>
        /// Defines the unit Vector2 for the W-axis.
        /// </summary>
        static Vector4 unitW;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Vector4 ()
        {
            zero =      new Vector4 ();
            one =       new Vector4 (1, 1, 1, 1);

            unitX =     new Vector4 (1, 0, 0, 0);
            unitY =     new Vector4 (0, 1, 0, 0);
            unitZ =     new Vector4 (0, 0, 1, 0);
            unitW =     new Vector4 (0, 0, 0, 1);
        }

        /// <summary>
        /// Returns a Vector4 with all of its components set to zero.
        /// </summary>
        public static Vector4 Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Returns a Vector4 with all of its components set to one.
        /// </summary>
        public static Vector4 One
        {
            get { return one; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the X-axis.
        /// </summary>
        public static Vector4 UnitX
        {
            get { return unitX; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the Y-axis.
        /// </summary>
        public static Vector4 UnitY
        {
            get { return unitY; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the Z-axis.
        /// </summary>
        public static Vector4 UnitZ
        {
            get { return unitZ; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the W-axis.
        /// </summary>
        public static Vector4 UnitW
        {
            get { return unitW; }
        }
        
        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static void Distance (
            ref Vector4 vector1,
            ref Vector4 vector2,
            out Single result)
        {
            Single dx = vector1.X - vector2.X;
            Single dy = vector1.Y - vector2.Y;
            Single dz = vector1.Z - vector2.Z;
            Single dw = vector1.W - vector2.W;

            Single lengthSquared =
                (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);

            result = RealMaths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector4 vector1,
            ref Vector4 vector2,
            out Single result)
        {
            Single dx = vector1.X - vector2.X;
            Single dy = vector1.Y - vector2.Y;
            Single dz = vector1.Z - vector2.Z;
            Single dw = vector1.W - vector2.W;

            result = (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static void Dot (
            ref Vector4 vector1,
            ref Vector4 vector2,
            out Single result)
        {
            result =
                (vector1.X * vector2.X) +
                (vector1.Y * vector2.Y) +
                (vector1.Z * vector2.Z) +
                (vector1.W * vector2.W);
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static void Normalise (
            ref Vector4 vector,
            out Vector4 result)
        {
            Single lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z) +
                (vector.W * vector.W);

            Single epsilon; RealMaths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Single.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single one = 1;
            Single multiplier = one / (RealMaths.Sqrt (lengthSquared));

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
            result.W = vector.W * multiplier;
        }

        /// <summary>
        /// Transforms a Vector4 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector4 vector,
            ref Matrix44 matrix,
            out Vector4 result)
        {
            Single x =
                (vector.X * matrix.M11) +
                (vector.Y * matrix.M21) +
                (vector.Z * matrix.M31) +
                (vector.W * matrix.M41);

            Single y =
                (vector.X * matrix.M12) +
                (vector.Y * matrix.M22) +
                (vector.Z * matrix.M32) +
                (vector.W * matrix.M42);

            Single z =
                (vector.X * matrix.M13) +
                (vector.Y * matrix.M23) +
                (vector.Z * matrix.M33) +
                (vector.W * matrix.M43);

            Single w =
                (vector.X * matrix.M14) +
                (vector.Y * matrix.M24) +
                (vector.Z * matrix.M34) +
                (vector.W * matrix.M44);

            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }

        /// <summary>
        /// Transforms a Vector4 by the specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector4 vector,
            ref Quaternion rotation,
            out Vector4 result)
        {
            Single two = 2;

            Single x = rotation.X;
            Single y = rotation.Y;
            Single z = rotation.Z;
            Single w = rotation.W;

            Single xx = x * x;
            Single yy = y * y;
            Single zz = z * z;

            Single wx = w * x;
            Single wy = w * y;
            Single wz = w * z;
            Single xy = x * y;
            Single xz = x * z;
            Single yz = y * z;

            result.X =
                + vector.X
                - (two * vector.X * (yy + zz))
                + (two * vector.Y * (xy - wz))
                + (two * vector.Z * (xz + wy));

            result.Y =
                + vector.Y
                + (two * vector.X * (xy + wz))
                - (two * vector.Y * (xx + zz))
                + (two * vector.Z * (yz - wx));

            result.Z =
                + vector.Z
                + (two * vector.X * (xz - wy))
                + (two * vector.Y * (yz + wx))
                - (two * vector.Z * (xx + yy));

            result.W = vector.W;
        }

        /// <summary>
        /// Transforms a normalised Vector4 by a Matrix44.
        /// </summary>
        public static void TransformNormal (
            ref Vector4 normal,
            ref Matrix44 matrix,
            out Vector4 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Single x =
                (normal.X * matrix.M11) + (normal.Y * matrix.M21) +
                (normal.Z * matrix.M31) + (normal.W * matrix.M41);

            Single y =
                (normal.X * matrix.M12) + (normal.Y * matrix.M22) +
                (normal.Z * matrix.M32) + (normal.W * matrix.M42);

            Single z =
                (normal.X * matrix.M13) + (normal.Y * matrix.M23) +
                (normal.Z * matrix.M33) + (normal.W * matrix.M43);

            Single w =
                (normal.X * matrix.M14) + (normal.Y * matrix.M24) +
                (normal.Z * matrix.M34) + (normal.W * matrix.M44);

            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Vector4 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Vector4) {
                flag = this.Equals ((Vector4)obj);
            }
            return flag;
        }

        #region IEquatable<Vector4>

        /// <summary>
        /// Determines whether or not this Vector4 object is equal to another
        /// Vector4 object.
        /// </summary>
        public Boolean Equals (Vector4 other)
        {
            return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Vector4 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Vector4 value1, Vector4 value2)
        {
            return ((((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) && (value1.W == value2.W));
        }
        
        /// <summary>
        /// Determines whether or not two Vector4 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Vector4 value1, Vector4 value2)
        {
            if (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) {
                return !(value1.W == value2.W);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector4 objects.
        /// </summary>
        public static void Add (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
            result.W = value1.W + value2.W;
        }

        /// <summary>
        /// Performs addition of two Vector4 objects using the (X+Y) operator. 
        /// </summary>
        public static Vector4 operator + (Vector4 value1, Vector4 value2)
        {
            Vector4 vector;
            vector.X = value1.X + value2.X;
            vector.Y = value1.Y + value2.Y;
            vector.Z = value1.Z + value2.Z;
            vector.W = value1.W + value2.W;
            return vector;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector4 objects.
        /// </summary>
        public static void Subtract (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
            result.W = value1.W - value2.W;
        }

        /// <summary>
        /// Performs subtraction of two Vector4 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Vector4 operator - (Vector4 value1, Vector4 value2)
        {
            Vector4 vector;
            vector.X = value1.X - value2.X;
            vector.Y = value1.Y - value2.Y;
            vector.Z = value1.Z - value2.Z;
            vector.W = value1.W - value2.W;
            return vector;
        }

        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Vector4 object.
        /// </summary>
        public static void Negate (ref Vector4 value, out Vector4 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        /// <summary>
        /// Performs negation of a Vector4 object using the (-X) operator.
        /// </summary>
        public static Vector4 operator - (Vector4 value)
        {
            Vector4 vector;
            vector.X = -value.X;
            vector.Y = -value.Y;
            vector.Z = -value.Z;
            vector.W = -value.W;
            return vector;
        }
        
        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector4 objects.
        /// </summary>
        public static void Multiply (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
            result.W = value1.W * value2.W;
        }

        /// <summary>
        /// Performs multiplication of a Vector4 object and a Single
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Vector4 value1, Single scaleFactor, out Vector4 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
            result.W = value1.W * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Vector4 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Vector4 operator * (Vector4 value1, Vector4 value2)
        {
            Vector4 vector;
            vector.X = value1.X * value2.X;
            vector.Y = value1.Y * value2.Y;
            vector.Z = value1.Z * value2.Z;
            vector.W = value1.W * value2.W;
            return vector;
        }
        
        /// <summary>
        /// Performs multiplication of a Vector4 object and a Single
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Vector4 operator * (Vector4 value1, Single scaleFactor)
        {
            Vector4 vector;
            vector.X = value1.X * scaleFactor;
            vector.Y = value1.Y * scaleFactor;
            vector.Z = value1.Z * scaleFactor;
            vector.W = value1.W * scaleFactor;
            return vector;
        }
        
        /// <summary>
        /// Performs multiplication of a Single precision scaling factor 
        /// and aVector4 object using the (x*Y) operator.
        /// </summary>
        public static Vector4 operator * (Single scaleFactor, Vector4 value1)
        {
            Vector4 vector;
            vector.X = value1.X * scaleFactor;
            vector.Y = value1.Y * scaleFactor;
            vector.Z = value1.Z * scaleFactor;
            vector.W = value1.W * scaleFactor;
            return vector;
        }
        
        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector4 objects.
        /// </summary>
        public static void Divide (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
            result.W = value1.W / value2.W;
        }

        /// <summary>
        /// Performs division of a Vector4 object and a Single precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Vector4 value1, Single divider, out Vector4 result)
        {
            Single one = 1;
            Single num = one / divider;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
            result.W = value1.W * num;
        }

        /// <summary>
        /// Performs division of two Vector4 objects using the (X/Y) operator.
        /// </summary>
        public static Vector4 operator / (Vector4 value1, Vector4 value2)
        {
            Vector4 vector;
            vector.X = value1.X / value2.X;
            vector.Y = value1.Y / value2.Y;
            vector.Z = value1.Z / value2.Z;
            vector.W = value1.W / value2.W;
            return vector;
        }
        
        /// <summary>
        /// Performs division of a Vector4 object and a Single precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
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

        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector4 vector1,
            ref Vector4 vector2,
            Single amount,
            out Vector4 result)
        {
            Single zero = 0;
            Single one = 1;
            Single two = 2;
            Single three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
            amount = (amount * amount) * (three - (two * amount));

            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
            result.Z = vector1.Z + ((vector2.Z - vector1.Z) * amount);
            result.W = vector1.W + ((vector2.W - vector1.W) * amount);
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// Features:
        /// - The spline passes through all of the control points.
        /// - The spline is C^1 continuous, meaning that there are no
        ///   discontinuities in the tangent direction and magnitude.
        /// - The spline is not C^2 continuous.  The second derivative is
        ///   linearly interpolated within each segment, causing the curvature
        ///   to vary linearly over the length of the segment.
        /// </summary>
        public static void CatmullRom (
            ref Vector4 vector1,
            ref Vector4 vector2,
            ref Vector4 vector3,
            ref Vector4 vector4,
            Single amount,
            out Vector4 result)
        {
            Single half; RealMaths.Half(out half);
            Single zero = 0;
            Single one = 1;
            Single two = 2;
            Single three = 3;
            Single four = 4;
            Single five = 5;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single squared = amount * amount;
            Single cubed = amount * squared;

            result.X =
                half * ((((two * vector2.X) + ((-vector1.X + vector3.X) *
                amount)) + (((((two * vector1.X) - (five * vector2.X)) + (four *
                vector3.X)) - vector4.X) * squared)) + ((((-vector1.X + (three *
                vector2.X)) - (three * vector3.X)) + vector4.X) * cubed));

            result.Y =
                half * ((((two * vector2.Y) + ((-vector1.Y + vector3.Y) *
                amount)) + (((((two * vector1.Y) - (five * vector2.Y)) + (four *
                vector3.Y)) - vector4.Y) * squared)) + ((((-vector1.Y + (three *
                vector2.Y)) - (three * vector3.Y)) + vector4.Y) * cubed));

            result.Z =
                half * ((((two * vector2.Z) + ((-vector1.Z + vector3.Z) *
                amount)) + (((((two * vector1.Z) - (five * vector2.Z)) + (four *
                vector3.Z)) - vector4.Z) * squared)) + ((((-vector1.Z + (three *
                vector2.Z)) - (three * vector3.Z)) + vector4.Z) * cubed));

            result.W =
                half * ((((two * vector2.W) + ((-vector1.W + vector3.W) *
                amount)) + (((((two * vector1.W) - (five * vector2.W)) + (four *
                vector3.W)) - vector4.W) * squared)) + ((((-vector1.W + (three *
                vector2.W)) - (three * vector3.W)) + vector4.W) * cubed));
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector4 vector1,
            ref Vector4 tangent1,
            ref Vector4 vector2,
            ref Vector4 tangent2,
            Single amount,
            out Vector4 result)
        {
            Single zero = 0;
            Single one = 1;
            Single two = 2;
            Single three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            if( !tangent1.IsUnit() || !tangent2.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single squared = amount * amount;
            Single cubed = amount * squared;

            Single a = ((two * cubed) - (three * squared)) + one;
            Single b = (-two * cubed) + (three * squared);
            Single c = (cubed - (two * squared)) + amount;
            Single d = cubed - squared;

            result.X =
                (vector1.X * a) + (vector2.X * b) +
                (tangent1.X * c) + (tangent2.X * d);

            result.Y =
                (vector1.Y * a) + (vector2.Y * b) +
                (tangent1.Y * c) + (tangent2.Y * d);

            result.Z =
                (vector1.Z * a) + (vector2.Z * b) +
                (tangent1.Z * c) + (tangent2.Z * d);

            result.W =
                (vector1.W * a) + (vector2.W * b) +
                (tangent1.W * c) + (tangent2.W * d);
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Min (
            ref Vector4 a,
            ref Vector4 b,
            out Vector4 result)
        {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
            result.Z = (a.Z < b.Z) ? a.Z : b.Z;
            result.W = (a.W < b.W) ? a.W : b.W;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Max (
            ref Vector4 a,
            ref Vector4 b,
            out Vector4 result)
        {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
            result.Z = (a.Z > b.Z) ? a.Z : b.Z;
            result.W = (a.W > b.W) ? a.W : b.W;
        }
        
        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static void Clamp (
            ref Vector4 a,
            ref Vector4 min,
            ref Vector4 max,
            out Vector4 result)
        {
            Single x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            Single y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            Single z = a.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            Single w = a.W;
            w = (w > max.W) ? max.W : w;
            w = (w < min.W) ? min.W : w;
            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }
        
        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static void Lerp (
            ref Vector4 a,
            ref Vector4 b,
            Single amount,
            out Vector4 result)
        {
            Single zero = 0;
            Single one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }
            
            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
            result.Z = a.Z + ((b.Z - a.Z) * amount);
            result.W = a.W + ((b.W - a.W) * amount);
        }

    }

}

namespace Abacus.DoublePrecision
{
    /// <summary>
    /// todo
    /// </summary>
    internal class GjkDistance
    {
        /// <summary>
        /// todo
        /// </summary>
        internal GjkDistance ()
        {
            for (Int32 i = 0; i < 0x10; i++)
            {
                this.det [i] = new Double[4];
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        internal Boolean AddSupportPoint (ref Vector3 newPoint)
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

                this.edgeLengthSq [index] [num2] = 
                    this.edgeLengthSq [num2] [index] = 
                        vector.LengthSquared ();
            }

            this.UpdateDeterminant (index);

            return this.UpdateSimplex (index);
        }

        /// <summary>
        /// todo
        /// </summary>
        internal void Reset ()
        {
            Double zero = 0;

            this.simplexBits = 0;
            this.maxLengthSq = zero;
        }

        /// <summary>
        /// todo
        /// </summary>
        internal Vector3 ClosestPoint
        {
            get { return this.closestPoint; }
        }

        /// <summary>
        /// todo
        /// </summary>
        internal Boolean FullSimplex
        {
            get { return (this.simplexBits == 15); }
        }
        
        /// <summary>
        /// todo
        /// </summary>
        internal Double MaxLengthSquared
        {
            get { return this.maxLengthSq; }
        }

        /// <summary>
        /// todo
        /// </summary>
        Vector3 closestPoint;

        /// <summary>
        /// todo
        /// </summary>
        Double[][] det = new Double[0x10][];

        /// <summary>
        /// todo
        /// </summary>
        Double[][] edgeLengthSq = 
            new Double[][] 
            { 
                new Double[4], 
                new Double[4], 
                new Double[4], 
                new Double[4] 
            };
        
        /// <summary>
        /// todo
        /// </summary>
        Vector3[][] edges = 
            new Vector3[][] 
            { 
                new Vector3[4], 
                new Vector3[4], 
                new Vector3[4], 
                new Vector3[4] 
            };
        
        /// <summary>
        /// todo
        /// </summary>
        Double maxLengthSq;

        /// <summary>
        /// todo
        /// </summary>
        Int32 simplexBits;

        /// <summary>
        /// todo
        /// </summary>
        Vector3[] y = new Vector3[4];

        /// <summary>
        /// todo
        /// </summary>
        Double[] yLengthSq = new Double[4];

        /// <summary>
        /// todo
        /// </summary>
        static Int32[] BitsToIndices = 
            new Int32[] 
            { 
                0, 1, 2, 0x11, 3, 0x19, 0x1a, 0xd1, 
                4, 0x21, 0x22, 0x111, 0x23, 0x119, 0x11a, 0x8d1 
            };

        /// <summary>
        /// todo
        /// </summary>
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

                this.maxLengthSq = 
                RealMaths.Max (this.maxLengthSq, this.yLengthSq [index]);
            }

            return (Vector3)(zero / num3);
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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

                this.det [num6] [num] = 
                    Dot (ref this.edges [xmIdx] [num], ref this.y [xmIdx]);

                this.det [num6] [xmIdx] = 
                    Dot (ref this.edges [num] [xmIdx], ref this.y [num]);

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

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        static Double Dot (ref Vector3 a, ref Vector3 b)
        {
            return (((a.X * b.X) + (a.Y * b.Y)) + (a.Z * b.Z));
        }
    }
    /// <summary>
    /// Double precision Matrix44.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Matrix44 
        : IEquatable<Matrix44>
    {
        /// <summary>
        /// Gets or sets (Row 1, Column 1) of the Matrix44.
        /// </summary>
        public Double M11;

        /// <summary>
        /// Gets or sets (Row 1, Column 2) of the Matrix44.
        /// </summary>
        public Double M12;

        /// <summary>
        /// Gets or sets (Row 1, Column 3) of the Matrix44.
        /// </summary>
        public Double M13;

        /// <summary>
        /// Gets or sets (Row 1, Column 4) of the Matrix44.
        /// </summary>
        public Double M14;

        /// <summary>
        /// Gets or sets (Row 2, Column 1) of the Matrix44.
        /// </summary>
        public Double M21;

        /// <summary>
        /// Gets or sets (Row 2, Column 2) of the Matrix44.
        /// </summary>
        public Double M22;

        /// <summary>
        /// Gets or sets (ow 2, Column 3) of the Matrix44.
        /// </summary>
        public Double M23;

        /// <summary>
        /// Gets or sets (Row 2, Column 4) of the Matrix44.
        /// </summary>
        public Double M24;

        /// <summary>
        /// Row 3, Column 1) of the Matrix44.
        /// </summary>
        public Double M31;

        /// <summary>
        /// Gets or sets (Row 3, Column 2) of the Matrix44.
        /// </summary>
        public Double M32;

        /// <summary>
        /// Gets or sets (Row 3, Column 3) of the Matrix44.
        /// </summary>
        public Double M33;

        /// <summary>
        /// Gets or sets (Row 3, Column 4) of the Matrix44.
        /// </summary>
        public Double M34;

        /// <summary>
        /// Gets or sets (Row 4, Column 1) of the Matrix44.
        /// </summary>
        public Double M41; // translation.x

        /// <summary>
        /// Gets or sets (Row 4, Column 2) of the Matrix44.
        /// </summary>
        public Double M42; // translation.y

        /// <summary>
        /// Gets or sets (Row 4, Column 3) of the Matrix44.
        /// </summary>
        public Double M43; // translation.z

        /// <summary>
        /// Gets or sets (Row 4, Column 4) of the Matrix44.
        /// </summary>
        public Double M44;
        
        /// <summary>
        /// Initilises a new instance of Matrix44 from sixteen Double 
        /// values representing the matrix, in row major order, respectively.
        /// </summary>
        public Matrix44 (
            Double m11, 
            Double m12, 
            Double m13, 
            Double m14, 
            Double m21, 
            Double m22, 
            Double m23, 
            Double m24, 
            Double m31, 
            Double m32, 
            Double m33, 
            Double m34, 
            Double m41, 
            Double m42, 
            Double m43, 
            Double m44)
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

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return 
                (
                    "{ " + 
                    string.Format ("{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", 
                        new Object[] 
                        { 
                            this.M11.ToString (), 
                            this.M12.ToString (), 
                            this.M13.ToString (), 
                            this.M14.ToString () 
                        }
                    ) + 
                    string.Format ("{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", 
                        new Object[] 
                        { 
                            this.M21.ToString (), 
                            this.M22.ToString (), 
                            this.M23.ToString (), 
                            this.M24.ToString () 
                            }
                    ) + 
                    string.Format ("{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", 
                        new Object[] 
                        { 
                            this.M31.ToString (), 
                            this.M32.ToString (), 
                            this.M33.ToString (), 
                            this.M34.ToString () 
                        }
                    ) + string.Format ("{{M41:{0} M42:{1} M43:{2} M44:{3}}} ", 
                    new Object[] 
                    { 
                        this.M41.ToString (), 
                        this.M42.ToString (), 
                        this.M43.ToString (), 
                        this.M44.ToString () 
                    }
                    ) + 
                    "}"
                );
        }

        /// <summary>
        /// Gets the hash code of the Matrix44 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return 
                (((((((((((((((
                    this.M11.GetHashCode () + 
                    this.M12.GetHashCode ()) + 
                    this.M13.GetHashCode ()) + 
                    this.M14.GetHashCode ()) + 
                    this.M21.GetHashCode ()) + 
                    this.M22.GetHashCode ()) + 
                    this.M23.GetHashCode ()) + 
                    this.M24.GetHashCode ()) + 
                    this.M31.GetHashCode ()) + 
                    this.M32.GetHashCode ()) + 
                    this.M33.GetHashCode ()) + 
                    this.M34.GetHashCode ()) + 
                    this.M41.GetHashCode ()) + 
                    this.M42.GetHashCode ()) + 
                    this.M43.GetHashCode ()) + 
                    this.M44.GetHashCode ());
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Up 
        {
            get 
            {
                Vector3 vector;
                vector.X = this.M21;
                vector.Y = this.M22;
                vector.Z = this.M23;
                return vector;
            }
            set 
            {
                this.M21 = value.X;
                this.M22 = value.Y;
                this.M23 = value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Down 
        {
            get 
            {
                Vector3 vector;
                vector.X = -this.M21;
                vector.Y = -this.M22;
                vector.Z = -this.M23;
                return vector;
            }
            set 
            {
                this.M21 = -value.X;
                this.M22 = -value.Y;
                this.M23 = -value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Right 
        {
            get 
            {
                Vector3 vector;
                vector.X = this.M11;
                vector.Y = this.M12;
                vector.Z = this.M13;
                return vector;
            }
            set 
            {
                this.M11 = value.X;
                this.M12 = value.Y;
                this.M13 = value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Left
        {
            get
            {
                Vector3 vector;
                vector.X = -this.M11;
                vector.Y = -this.M12;
                vector.Z = -this.M13;
                return vector;
            }
            set
            {
                this.M11 = -value.X;
                this.M12 = -value.Y;
                this.M13 = -value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Forward
        {
            get
            {
                Vector3 vector;
                vector.X = -this.M31;
                vector.Y = -this.M32;
                vector.Z = -this.M33;
                return vector;
            }
            set 
            {
                this.M31 = -value.X;
                this.M32 = -value.Y;
                this.M33 = -value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Backward
        {
            get
            {
                Vector3 vector;
                vector.X = this.M31;
                vector.Y = this.M32;
                vector.Z = this.M33;
                return vector;
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
                this.M33 = value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Translation
        {
            get 
            {
                Vector3 vector;
                vector.X = this.M41;
                vector.Y = this.M42;
                vector.Z = this.M43;
                return vector;
            }
            set
            {
                this.M41 = value.X;
                this.M42 = value.Y;
                this.M43 = value.Z;
            }
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines the identity matrix.
        /// </summary>
        static Matrix44 identity;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Matrix44 ()
        {
            identity = new Matrix44 (
                1, 0, 0, 0, 
                0, 1, 0, 0, 
                0, 0, 1, 0, 
                0, 0, 0, 1);
        }

        /// <summary>
        /// Returns the identity matrix.
        /// </summary>
        public static Matrix44 Identity 
        {
            get { return identity; }
        }
        
        /// <summary>
        /// todo
        /// </summary>
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
        
        /// <summary>
        /// todo
        /// </summary>
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
        
        /// <summary>
        /// Creates a scaling matrix based on x, y, z.
        /// </summary>
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

        /// <summary>
        /// Creates a scaling matrix based on a vector.
        /// </summary>
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

        /// <summary>
        /// Create a scaling matrix consistant along each axis
        /// </summary>
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

        /// <summary>
        /// http://en.wikipedia.org/wiki/Rotation_matrix
        /// </summary>
        public static void CreateRotationX (Double radians, out Matrix44 result)
        {
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

        /// <summary>
        /// http://en.wikipedia.org/wiki/Rotation_matrix
        /// </summary>
        public static void CreateRotationY (Double radians, out Matrix44 result)
        {
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

        /// <summary>
        /// http://en.wikipedia.org/wiki/Rotation_matrix
        /// </summary>
        public static void CreateRotationZ (Double radians, out Matrix44 result)
        {
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
        
        /// <summary>
        /// todo
        /// </summary>
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
        
        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        public static void CreateWorldNew (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result)
        {
            Vector3 backward = -forward;

            Vector3 right;

            Vector3.Cross (ref up, ref backward, out right);

            Vector3.Normalise(ref right, out right);

            Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out result);

            result.M41 = position.X;
            result.M42 = position.Y;
            result.M43 = position.Z;
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        public static void CreateFromYawPitchRoll (Double yaw, Double pitch, Double roll, out Matrix44 result)
        {
            Quaternion quaternion;

            Quaternion.CreateFromYawPitchRoll (yaw, pitch, roll, out quaternion);

            CreateFromQuaternion (ref quaternion, out result);
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Creates a cylindrical billboard that rotates around a specified axis.
        /// This method computes the facing direction of the billboard from the object position and camera position. 
        /// When the object and camera positions are too close, the matrix will not be accurate. 
        /// To avoid this problem, the method uses the optional camera forward vector if the positions are too close.
        /// </summary>
        public static void CreateBillboard (ref Vector3 ObjectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;

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

            Vector3.Normalise (ref vector3, out vector3);

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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// todo
        /// </summary>
        public static void CreateConstrainedBillboard (
            ref Vector3 objectPosition, 
            ref Vector3 cameraPosition, 
            ref Vector3 rotateAxis, 
            Vector3? cameraForwardVector, 
            Vector3? objectForwardVector, 
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;

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
                Vector3.Normalise (ref vector3, out vector3);
                Vector3.Cross (ref vector3, ref rotateAxis, out vector);
                Vector3.Normalise (ref vector, out vector);
            } else {
                Vector3.Cross (ref rotateAxis, ref vector2, out vector3);
                Vector3.Normalise (ref vector3, out vector3);
                Vector3.Cross (ref vector3, ref vector4, out vector);
                Vector3.Normalise (ref vector, out vector);
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

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
        /// </summary>
        public static void CreatePerspectiveFieldOfView (
            Double fieldOfView, 
            Double aspectRatio, 
            Double nearPlaneDistance, 
            Double farPlaneDistance, 
            out Matrix44 result)
        {
            Double zero = 0;
            Double half; RealMaths.Half(out half);
            Double one = 1;
            Double pi; RealMaths.Pi(out pi);

            if ((fieldOfView <= zero) || (fieldOfView >= pi))
            {
                throw new ArgumentOutOfRangeException ("fieldOfView");
            }

            if (nearPlaneDistance <= zero)
            {
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");
            }

            if (farPlaneDistance <= zero)
            {
                throw new ArgumentOutOfRangeException ("farPlaneDistance");
            }

            if (nearPlaneDistance >= farPlaneDistance)
            {
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");
            }

            //
            // xScale     0          0              0
            // 0        yScale       0              0
            // 0        0        zf/(zn-zf)        -1
            // 0        0        zn*zf/(zn-zf)      0
            //
            // where:
            //
            // yScale = cot(fovY/2)
            //     
            // xScale = yScale / aspect ratio
            //

            // yScale = cot(fovY/2)
            Double yScale = one / ( RealMaths.Tan ( fieldOfView * half ) );

            // xScale = yScale / aspect ratio
            Double xScale = yScale / aspectRatio;

            result.M11 = xScale;
            result.M12 = zero;
            result.M13 = zero;
            result.M14 = zero;
            
            result.M21 = zero;
            result.M22 = yScale;
            result.M23 = zero;
            result.M24 = zero;

            result.M31 = zero;
            result.M32 = zero;
            result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance); // zf/(zn-zf)
            result.M34 = -one;

            result.M41 = zero;
            result.M42 = zero;
            result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance); // zn*zf/(zn-zf)
            result.M44 = zero;
        }



        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
        /// </summary>
        public static void CreatePerspective (
            Double width, 
            Double height, 
            Double nearPlaneDistance, 
            Double farPlaneDistance, 
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;
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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx
        /// </summary>
        public static void CreatePerspectiveOffCenter (
            Double left, 
            Double right, 
            Double bottom, 
            Double top, 
            Double nearPlaneDistance, 
            Double farPlaneDistance, 
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;
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
        
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
        /// </summary>
        public static void CreateOrthographic (
            Double width, 
            Double height, 
            Double zNearPlane, 
            Double zFarPlane, 
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;
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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx
        /// </summary>
        public static void CreateOrthographicOffCenter (
            Double left, 
            Double right, 
            Double bottom, 
            Double top, 
            Double zNearPlane, 
            Double zFarPlane, 
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;
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
        
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx
        /// </summary>
        public static void CreateLookAt (
            ref Vector3 cameraPosition, 
            ref Vector3 cameraTarget, 
            ref Vector3 cameraUpVector, 
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;

            Vector3 forward = cameraPosition - cameraTarget;
            Vector3.Normalise (ref forward, out forward);

            Vector3 right;
            Vector3.Cross (ref cameraUpVector, ref forward, out right); 
            Vector3.Normalise (ref right, out right);
            
            Vector3 up;
            Vector3.Cross (ref forward, ref right, out up);
            Vector3.Normalise (ref up, out up);
            
            result.M11 = right.X;
            result.M12 = up.X;
            result.M13 = forward.X;
            result.M14 = zero;

            result.M21 = right.Y;
            result.M22 = up.Y;
            result.M23 = forward.Y;
            result.M24 = zero;

            result.M31 = right.Z;
            result.M32 = up.Z;
            result.M33 = forward.Z;
            result.M34 = zero;

            Double a;
            Double b;
            Double c;

            Vector3.Dot (ref right, ref cameraPosition, out a);
            Vector3.Dot (ref up, ref cameraPosition, out b);
            Vector3.Dot (ref forward, ref cameraPosition, out c);
            
            result.M41 = -a;
            result.M42 = -b;
            result.M43 = -c;

            result.M44 = one;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void Transpose (ref Matrix44 input, out Matrix44 output)
        {
            output.M11 = input.M11;
            output.M22 = input.M22;
            output.M33 = input.M33;
            output.M44 = input.M44;

            Double temp = input.M12;
            output.M12 = input.M21;
            output.M21 = temp;

            temp = input.M13;
            output.M13 = input.M31;
            output.M31 = temp;

            temp = input.M14;
            output.M14 = input.M41;
            output.M41 = temp;

            temp = input.M23;
            output.M23 = input.M32;
            output.M32 = temp;

            temp = input.M24;
            output.M24 = input.M42;
            output.M42 = temp;

            temp =  input.M34;
            output.M34 = input.M43;
            output.M43 = temp;
        }

        /// <summary>
        /// Essential Mathemathics For Games & Interactive Applications
        /// </summary>
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

            Vector3.Normalise(ref a, out a);
            Vector3.Normalise(ref b, out b);
            Vector3.Normalise(ref c, out c);

            Vector3 right = new Vector3(a.X, b.X, c.X);
            Vector3 up = new Vector3(a.Y, b.Y, c.Y);
            Vector3 backward = new Vector3(a.Z, b.Z, c.Z);

            Vector3.Normalise(ref right, out right);
            Vector3.Normalise(ref up, out up);
            Vector3.Normalise(ref backward, out backward);

            Matrix44 rotMat;
            Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out rotMat);

            Quaternion.CreateFromRotationMatrix(ref rotMat, out rotation);

            return true;
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// todo
        /// </summary>
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
        
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// todo
        /// </summary>
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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Transforms a Matrix by applying a Quaternion rotation.
        /// </summary>
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

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Matrix44 object is equal to another
        /// object
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;

            if (obj is Matrix44)
            {
                flag = this.Equals ((Matrix44) obj);
            }
            
            return flag;
        }

        #region IEquatable<Matrix44>

        /// <summary>
        /// Determines whether or not this Matrix44 object is equal to another
        /// Matrix44 object.
        /// </summary>
        public Boolean Equals (Matrix44 other)
        {
            return 
                (this.M11 == other.M11) && 
                (this.M22 == other.M22) && 
                (this.M33 == other.M33) && 
                (this.M44 == other.M44) && 
                (this.M12 == other.M12) && 
                (this.M13 == other.M13) && 
                (this.M14 == other.M14) && 
                (this.M21 == other.M21) && 
                (this.M23 == other.M23) && 
                (this.M24 == other.M24) && 
                (this.M31 == other.M31) && 
                (this.M32 == other.M32) && 
                (this.M34 == other.M34) && 
                (this.M41 == other.M41) && 
                (this.M42 == other.M42) && 
                (this.M43 == other.M43);
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Matrix44 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Matrix44 matrix1, Matrix44 matrix2)
        {
            return ((((((matrix1.M11 == matrix2.M11) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M44 == matrix2.M44))) && (((matrix1.M12 == matrix2.M12) && (matrix1.M13 == matrix2.M13)) && ((matrix1.M14 == matrix2.M14) && (matrix1.M21 == matrix2.M21)))) && ((((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)) && ((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32))) && (((matrix1.M34 == matrix2.M34) && (matrix1.M41 == matrix2.M41)) && (matrix1.M42 == matrix2.M42)))) && (matrix1.M43 == matrix2.M43));
        }
        
        /// <summary>
        /// Determines whether or not two Matrix44 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Matrix44 matrix1, Matrix44 matrix2)
        {
            if ((matrix1.M11 == matrix2.M11) && 
                (matrix1.M12 == matrix2.M12) && 
                (matrix1.M13 == matrix2.M13) && 
                (matrix1.M14 == matrix2.M14) && 
                (matrix1.M21 == matrix2.M21) && 
                (matrix1.M22 == matrix2.M22) && 
                (matrix1.M23 == matrix2.M23) && 
                (matrix1.M24 == matrix2.M24) && 
                (matrix1.M31 == matrix2.M31) && 
                (matrix1.M32 == matrix2.M32) && 
                (matrix1.M33 == matrix2.M33) && 
                (matrix1.M34 == matrix2.M34) && 
                (matrix1.M41 == matrix2.M41) && 
                (matrix1.M42 == matrix2.M42) && 
                (matrix1.M43 == matrix2.M43))
            {
                return !(matrix1.M44 == matrix2.M44);
            }

            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Matrix44 objects.
        /// </summary>
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

        /// <summary>
        /// Performs addition of two Matrix44 objects using the (X+Y) operator. 
        /// </summary>
        public static Matrix44 operator + (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
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
            return result;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Matrix44 objects.
        /// </summary>
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

        /// <summary>
        /// Performs subtraction of two Matrix44 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Matrix44 operator - (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
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
            return result;
        }

        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Matrix44 object.
        /// </summary>
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

        /// <summary>
        /// Performs negation of a Matrix44 object using the (-X) operator.
        /// </summary>
        public static Matrix44 operator - (Matrix44 matrix)
        {
            Matrix44 result;
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
            return result;
        }
        
        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Matrix44 objects.
        /// </summary>
        public static void Multiply (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
        {   
            result.M11 = 
                (matrix1.M11 * matrix2.M11) + 
                (matrix1.M12 * matrix2.M21) + 
                (matrix1.M13 * matrix2.M31) + 
                (matrix1.M14 * matrix2.M41);

            result.M12 =
                (matrix1.M11 * matrix2.M12) + 
                (matrix1.M12 * matrix2.M22) + 
                (matrix1.M13 * matrix2.M32) + 
                (matrix1.M14 * matrix2.M42);

            result.M13 = 
                (matrix1.M11 * matrix2.M13) + 
                (matrix1.M12 * matrix2.M23) + 
                (matrix1.M13 * matrix2.M33) + 
                (matrix1.M14 * matrix2.M43);

            result.M14 = 
                (matrix1.M11 * matrix2.M14) + 
                (matrix1.M12 * matrix2.M24) + 
                (matrix1.M13 * matrix2.M34) + 
                (matrix1.M14 * matrix2.M44);

            result.M21 = 
                (matrix1.M21 * matrix2.M11) + 
                (matrix1.M22 * matrix2.M21) + 
                (matrix1.M23 * matrix2.M31) + 
                (matrix1.M24 * matrix2.M41);

            result.M22 = 
                (matrix1.M21 * matrix2.M12) + 
                (matrix1.M22 * matrix2.M22) + 
                (matrix1.M23 * matrix2.M32) + 
                (matrix1.M24 * matrix2.M42);

            result.M23 = 
                (matrix1.M21 * matrix2.M13) + 
                (matrix1.M22 * matrix2.M23) + 
                (matrix1.M23 * matrix2.M33) + 
                (matrix1.M24 * matrix2.M43);

            result.M24 = 
                (matrix1.M21 * matrix2.M14) + 
                (matrix1.M22 * matrix2.M24) + 
                (matrix1.M23 * matrix2.M34) + 
                (matrix1.M24 * matrix2.M44);

            result.M31 = 
                (matrix1.M31 * matrix2.M11) + 
                (matrix1.M32 * matrix2.M21) + 
                (matrix1.M33 * matrix2.M31) + 
                (matrix1.M34 * matrix2.M41);

            result.M32 = 
                (matrix1.M31 * matrix2.M12) + 
                (matrix1.M32 * matrix2.M22) + 
                (matrix1.M33 * matrix2.M32) + 
                (matrix1.M34 * matrix2.M42);

            result.M33 = 
                (matrix1.M31 * matrix2.M13) + 
                (matrix1.M32 * matrix2.M23) + 
                (matrix1.M33 * matrix2.M33) + 
                (matrix1.M34 * matrix2.M43);

            result.M34 = 
                (matrix1.M31 * matrix2.M14) + 
                (matrix1.M32 * matrix2.M24) + 
                (matrix1.M33 * matrix2.M34) + 
                (matrix1.M34 * matrix2.M44);

            result.M41 = 
                (matrix1.M41 * matrix2.M11) + 
                (matrix1.M42 * matrix2.M21) + 
                (matrix1.M43 * matrix2.M31) + 
                (matrix1.M44 * matrix2.M41);

            result.M42 = 
                (matrix1.M41 * matrix2.M12) + 
                (matrix1.M42 * matrix2.M22) + 
                (matrix1.M43 * matrix2.M32) + 
                (matrix1.M44 * matrix2.M42);

            result.M43 = 
                (matrix1.M41 * matrix2.M13) + 
                (matrix1.M42 * matrix2.M23) + 
                (matrix1.M43 * matrix2.M33) + 
                (matrix1.M44 * matrix2.M43);

            result.M44 = 
                (matrix1.M41 * matrix2.M14) + 
                (matrix1.M42 * matrix2.M24) + 
                (matrix1.M43 * matrix2.M34) + 
                (matrix1.M44 * matrix2.M44);
        }

        /// <summary>
        /// Performs multiplication of a Matrix44 object and a Double
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Matrix44 matrix1, Double scaleFactor, out Matrix44 result)
        {
            result.M11 = matrix1.M11 * scaleFactor;
            result.M12 = matrix1.M12 * scaleFactor;
            result.M13 = matrix1.M13 * scaleFactor;
            result.M14 = matrix1.M14 * scaleFactor;
            result.M21 = matrix1.M21 * scaleFactor;
            result.M22 = matrix1.M22 * scaleFactor;
            result.M23 = matrix1.M23 * scaleFactor;
            result.M24 = matrix1.M24 * scaleFactor;
            result.M31 = matrix1.M31 * scaleFactor;
            result.M32 = matrix1.M32 * scaleFactor;
            result.M33 = matrix1.M33 * scaleFactor;
            result.M34 = matrix1.M34 * scaleFactor;
            result.M41 = matrix1.M41 * scaleFactor;
            result.M42 = matrix1.M42 * scaleFactor;
            result.M43 = matrix1.M43 * scaleFactor;
            result.M44 = matrix1.M44 * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Matrix44 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Matrix44 operator * (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            
            result.M11 = 
                (matrix1.M11 * matrix2.M11) + 
                (matrix1.M12 * matrix2.M21) + 
                (matrix1.M13 * matrix2.M31) + 
                (matrix1.M14 * matrix2.M41);

            result.M12 =
                (matrix1.M11 * matrix2.M12) + 
                (matrix1.M12 * matrix2.M22) + 
                (matrix1.M13 * matrix2.M32) + 
                (matrix1.M14 * matrix2.M42);

            result.M13 = 
                (matrix1.M11 * matrix2.M13) + 
                (matrix1.M12 * matrix2.M23) + 
                (matrix1.M13 * matrix2.M33) + 
                (matrix1.M14 * matrix2.M43);

            result.M14 = 
                (matrix1.M11 * matrix2.M14) + 
                (matrix1.M12 * matrix2.M24) + 
                (matrix1.M13 * matrix2.M34) + 
                (matrix1.M14 * matrix2.M44);

            result.M21 = 
                (matrix1.M21 * matrix2.M11) + 
                (matrix1.M22 * matrix2.M21) + 
                (matrix1.M23 * matrix2.M31) + 
                (matrix1.M24 * matrix2.M41);

            result.M22 = 
                (matrix1.M21 * matrix2.M12) + 
                (matrix1.M22 * matrix2.M22) + 
                (matrix1.M23 * matrix2.M32) + 
                (matrix1.M24 * matrix2.M42);

            result.M23 = 
                (matrix1.M21 * matrix2.M13) + 
                (matrix1.M22 * matrix2.M23) + 
                (matrix1.M23 * matrix2.M33) + 
                (matrix1.M24 * matrix2.M43);

            result.M24 = 
                (matrix1.M21 * matrix2.M14) + 
                (matrix1.M22 * matrix2.M24) + 
                (matrix1.M23 * matrix2.M34) + 
                (matrix1.M24 * matrix2.M44);

            result.M31 = 
                (matrix1.M31 * matrix2.M11) + 
                (matrix1.M32 * matrix2.M21) + 
                (matrix1.M33 * matrix2.M31) + 
                (matrix1.M34 * matrix2.M41);

            result.M32 = 
                (matrix1.M31 * matrix2.M12) + 
                (matrix1.M32 * matrix2.M22) + 
                (matrix1.M33 * matrix2.M32) + 
                (matrix1.M34 * matrix2.M42);

            result.M33 = 
                (matrix1.M31 * matrix2.M13) + 
                (matrix1.M32 * matrix2.M23) + 
                (matrix1.M33 * matrix2.M33) + 
                (matrix1.M34 * matrix2.M43);

            result.M34 = 
                (matrix1.M31 * matrix2.M14) + 
                (matrix1.M32 * matrix2.M24) + 
                (matrix1.M33 * matrix2.M34) + 
                (matrix1.M34 * matrix2.M44);

            result.M41 = 
                (matrix1.M41 * matrix2.M11) + 
                (matrix1.M42 * matrix2.M21) + 
                (matrix1.M43 * matrix2.M31) + 
                (matrix1.M44 * matrix2.M41);

            result.M42 = 
                (matrix1.M41 * matrix2.M12) + 
                (matrix1.M42 * matrix2.M22) + 
                (matrix1.M43 * matrix2.M32) + 
                (matrix1.M44 * matrix2.M42);

            result.M43 = 
                (matrix1.M41 * matrix2.M13) + 
                (matrix1.M42 * matrix2.M23) + 
                (matrix1.M43 * matrix2.M33) + 
                (matrix1.M44 * matrix2.M43);

            result.M44 = 
                (matrix1.M41 * matrix2.M14) + 
                (matrix1.M42 * matrix2.M24) + 
                (matrix1.M43 * matrix2.M34) + 
                (matrix1.M44 * matrix2.M44);

            return result;
        }
        
        /// <summary>
        /// Performs multiplication of a Matrix44 object and a Double
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Matrix44 operator * (Matrix44 matrix1, Double scaleFactor)
        {
            Matrix44 result;
            result.M11 = matrix1.M11 * scaleFactor;
            result.M12 = matrix1.M12 * scaleFactor;
            result.M13 = matrix1.M13 * scaleFactor;
            result.M14 = matrix1.M14 * scaleFactor;
            result.M21 = matrix1.M21 * scaleFactor;
            result.M22 = matrix1.M22 * scaleFactor;
            result.M23 = matrix1.M23 * scaleFactor;
            result.M24 = matrix1.M24 * scaleFactor;
            result.M31 = matrix1.M31 * scaleFactor;
            result.M32 = matrix1.M32 * scaleFactor;
            result.M33 = matrix1.M33 * scaleFactor;
            result.M34 = matrix1.M34 * scaleFactor;
            result.M41 = matrix1.M41 * scaleFactor;
            result.M42 = matrix1.M42 * scaleFactor;
            result.M43 = matrix1.M43 * scaleFactor;
            result.M44 = matrix1.M44 * scaleFactor;
            return result;
        }
        
        /// <summary>
        /// Performs multiplication of a Double precision scaling factor 
        /// and aMatrix44 object using the (x*Y) operator.
        /// </summary>
        public static Matrix44 operator * (Double scaleFactor, Matrix44 matrix1)
        {
            Matrix44 result;
            result.M11 = matrix1.M11 * scaleFactor;
            result.M12 = matrix1.M12 * scaleFactor;
            result.M13 = matrix1.M13 * scaleFactor;
            result.M14 = matrix1.M14 * scaleFactor;
            result.M21 = matrix1.M21 * scaleFactor;
            result.M22 = matrix1.M22 * scaleFactor;
            result.M23 = matrix1.M23 * scaleFactor;
            result.M24 = matrix1.M24 * scaleFactor;
            result.M31 = matrix1.M31 * scaleFactor;
            result.M32 = matrix1.M32 * scaleFactor;
            result.M33 = matrix1.M33 * scaleFactor;
            result.M34 = matrix1.M34 * scaleFactor;
            result.M41 = matrix1.M41 * scaleFactor;
            result.M42 = matrix1.M42 * scaleFactor;
            result.M43 = matrix1.M43 * scaleFactor;
            result.M44 = matrix1.M44 * scaleFactor;
            return result;
        }
        
        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Matrix44 objects.
        /// </summary>
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

        /// <summary>
        /// Performs division of a Matrix44 object and a Double precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Matrix44 matrix1, Double divider, out Matrix44 result)
        {
            result.M11 = matrix1.M11 / divider;
            result.M12 = matrix1.M12 / divider;
            result.M13 = matrix1.M13 / divider;
            result.M14 = matrix1.M14 / divider;
            result.M21 = matrix1.M21 / divider;
            result.M22 = matrix1.M22 / divider;
            result.M23 = matrix1.M23 / divider;
            result.M24 = matrix1.M24 / divider;
            result.M31 = matrix1.M31 / divider;
            result.M32 = matrix1.M32 / divider;
            result.M33 = matrix1.M33 / divider;
            result.M34 = matrix1.M34 / divider;
            result.M41 = matrix1.M41 / divider;
            result.M42 = matrix1.M42 / divider;
            result.M43 = matrix1.M43 / divider;
            result.M44 = matrix1.M44 / divider;
        }

        /// <summary>
        /// Performs division of two Matrix44 objects using the (X/Y) operator.
        /// </summary>
        public static Matrix44 operator / (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;

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

            return result;
        }
        
        /// <summary>
        /// Performs division of a Matrix44 object and a Double precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Matrix44 operator / (Matrix44 matrix1, Double divider)
        {
            Matrix44 result;

            result.M11 = matrix1.M11 / divider;
            result.M12 = matrix1.M12 / divider;
            result.M13 = matrix1.M13 / divider;
            result.M14 = matrix1.M14 / divider;
            result.M21 = matrix1.M21 / divider;
            result.M22 = matrix1.M22 / divider;
            result.M23 = matrix1.M23 / divider;
            result.M24 = matrix1.M24 / divider;
            result.M31 = matrix1.M31 / divider;
            result.M32 = matrix1.M32 / divider;
            result.M33 = matrix1.M33 / divider;
            result.M34 = matrix1.M34 / divider;
            result.M41 = matrix1.M41 / divider;
            result.M42 = matrix1.M42 / divider;
            result.M43 = matrix1.M43 / divider;
            result.M44 = matrix1.M44 / divider;

            return result;
        }

        /// <summary>
        /// beware, doing this might not produce what you expect.  you likely
        /// want to lerp between quaternions.
        /// </summary>
        public static void Lerp (ref Matrix44 matrix1, ref Matrix44 matrix2, Double amount, out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

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


    }

    /// <summary>
    /// Double precision Quaternion.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Quaternion 
        : IEquatable<Quaternion>
    {
        /// <summary>
        /// todo
        /// </summary>
        public Double X;

        /// <summary>
        /// todo
        /// </summary>
        public Double Y;

        /// <summary>
        /// todo
        /// </summary>
        public Double Z;

        /// <summary>
        /// todo
        /// </summary>
        public Double W;

        /// <summary>
        /// todo
        /// </summary>
        public Quaternion (Double x, Double y, Double z, Double w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// todo
        /// </summary>
        public Quaternion (Vector3 vectorPart, Double scalarPart)
        {
            this.X = vectorPart.X;
            this.Y = vectorPart.Y;
            this.Z = vectorPart.Z;
            this.W = scalarPart;
        }

        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString (), this.W.ToString () });
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ()) + this.W.GetHashCode ());
        }

        /// <summary>
        /// todo
        /// </summary>
        public Double LengthSquared ()
        {
            return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
        }

        /// <summary>
        /// todo
        /// </summary>
        public Double Length ()
        {
            Double num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
            return RealMaths.Sqrt (num);
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        public Boolean IsUnit()
        {
            Double one = 1;

            return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void Conjugate ()
        {
            this.X = -this.X;
            this.Y = -this.Y;
            this.Z = -this.Z;
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines the identity quaternion.
        /// </summary>
        static Quaternion identity;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Quaternion ()
        {
            identity = new Quaternion (0, 0, 0, 1);
        }

        /// <summary>
        /// Returns the identity Quaternion.
        /// </summary>
        public static Quaternion Identity
        {
            get { return identity; }
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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
        /// <summary>
        /// todo
        /// </summary>
        public static void Conjugate (ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = value.W;
        }
        
        /// <summary>
        /// todo
        /// </summary>
        public static void Inverse (ref Quaternion quaternion, out Quaternion result)
        {
            Double one = 1;
            Double a =
                (quaternion.X * quaternion.X) + 
                (quaternion.Y * quaternion.Y) + 
                (quaternion.Z * quaternion.Z) + 
                (quaternion.W * quaternion.W);

            Double b = one / a;

            result.X = -quaternion.X * b;
            result.Y = -quaternion.Y * b;
            result.Z = -quaternion.Z * b;
            result.W =  quaternion.W * b;
        }
        
        /// <summary>
        /// todo
        /// </summary>
        public static void Dot (ref Quaternion quaternion1, ref Quaternion quaternion2, out Double result)
        {
            result = 
                (quaternion1.X * quaternion2.X) + 
                (quaternion1.Y * quaternion2.Y) + 
                (quaternion1.Z * quaternion2.Z) + 
                (quaternion1.W * quaternion2.W);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void Concatenate (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            Double x = value2.X;
            Double y = value2.Y;
            Double z = value2.Z;
            Double w = value2.W;

            Double a = value1.X;
            Double b = value1.Y;
            Double c = value1.Z;
            Double d = value1.W;

            Double e = (y * c) - (z * b);
            Double f = (z * a) - (x * c);
            Double g = (x * b) - (y * a);
            Double h = ((x * a) + (y * b)) + (z * c);

            result.X = (x * d) + (a * w) + e;
            result.Y = (y * d) + (b * w) + f;
            result.Z = (z * d) + (c * w) + g;
            result.W = (w * d) - h;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void Normalise (ref Quaternion quaternion, out Quaternion result)
        {
            Double one = 1;

            Double a = 
                (quaternion.X * quaternion.X) + 
                (quaternion.Y * quaternion.Y) + 
                (quaternion.Z * quaternion.Z) + 
                (quaternion.W * quaternion.W);

            Double b = one / RealMaths.Sqrt (a);

            result.X = quaternion.X * b;
            result.Y = quaternion.Y * b;
            result.Z = quaternion.Z * b;
            result.W = quaternion.W * b;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Quaternion object is equal to another
        /// object
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            
            if (obj is Quaternion)
            {
                flag = this.Equals ((Quaternion) obj);
            }

            return flag;
        }

        #region IEquatable<Quaternion>

        /// <summary>
        /// Determines whether or not this Quaternion object is equal to another
        /// Quaternion object.
        /// </summary>
        public Boolean Equals (Quaternion other)
        {
            return 
                (this.X == other.X) && 
                (this.Y == other.Y) && 
                (this.Z == other.Z) && 
                (this.W == other.W);
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Quaternion objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Quaternion value1, Quaternion value2)
        {
            return ((((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) && (value1.W == value2.W));
        }
        
        /// <summary>
        /// Determines whether or not two Quaternion objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Quaternion value1, Quaternion value2)
        {
            if (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) {
                return !(value1.W == value2.W);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Quaternion objects.
        /// </summary>
        public static void Add (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
            result.W = value1.W + value2.W;
        }

        /// <summary>
        /// Performs addition of two Quaternion objects using the (X+Y) operator. 
        /// </summary>
        public static Quaternion operator + (Quaternion value1, Quaternion value2)
        {
            Quaternion quat;
            quat.X = value1.X + value2.X;
            quat.Y = value1.Y + value2.Y;
            quat.Z = value1.Z + value2.Z;
            quat.W = value1.W + value2.W;
            return quat;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Quaternion objects.
        /// </summary>
        public static void Subtract (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
            result.W = value1.W - value2.W;
        }

        /// <summary>
        /// Performs subtraction of two Quaternion objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Quaternion operator - (Quaternion value1, Quaternion value2)
        {
            Quaternion quat;
            quat.X = value1.X - value2.X;
            quat.Y = value1.Y - value2.Y;
            quat.Z = value1.Z - value2.Z;
            quat.W = value1.W - value2.W;
            return quat;
        }

        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Quaternion object.
        /// </summary>
        public static void Negate (ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        /// <summary>
        /// Performs negation of a Quaternion object using the (-X) operator.
        /// </summary>
        public static Quaternion operator - (Quaternion value)
        {
            Quaternion quat;
            quat.X = -value.X;
            quat.Y = -value.Y;
            quat.Z = -value.Z;
            quat.W = -value.W;
            return quat;
        }
        
        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Quaternion objects.
        /// </summary>
        public static void Multiply (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            Double x1 = value1.X;
            Double y1 = value1.Y;
            Double z1 = value1.Z;
            Double w1 = value1.W;

            Double x2 = value2.X;
            Double y2 = value2.Y;
            Double z2 = value2.Z;
            Double w2 = value2.W;

            Double a = (y1 * z2) - (z1 * y2);
            Double b = (z1 * x2) - (x1 * z2);
            Double c = (x1 * y2) - (y1 * x2);
            Double d = ((x1 * x2) + (y1 * y2)) + (z1 * z2);

            result.X = ((x1 * w2) + (x2 * w1)) + a;
            result.Y = ((y1 * w2) + (y2 * w1)) + b;
            result.Z = ((z1 * w2) + (z2 * w1)) + c;
            result.W = (w1 * w2) - d;
        }

        /// <summary>
        /// Performs multiplication of a Quaternion object and a Double
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Quaternion value1, Double scaleFactor, out Quaternion result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
            result.W = value1.W * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Quaternion objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Quaternion operator * (Quaternion value1, Quaternion value2)
        {
            Quaternion quaternion;
            
            Double x1 = value1.X;
            Double y1 = value1.Y;
            Double z1 = value1.Z;
            Double w1 = value1.W;

            Double x2 = value2.X;
            Double y2 = value2.Y;
            Double z2 = value2.Z;
            Double w2 = value2.W;

            Double a = (y1 * z2) - (z1 * y2);
            Double b = (z1 * x2) - (x1 * z2);
            Double c = (x1 * y2) - (y1 * x2);
            Double d = ((x1 * x2) + (y1 * y2)) + (z1 * z2);

            quaternion.X = ((x1 * w2) + (x2 * w1)) + a;
            quaternion.Y = ((y1 * w2) + (y2 * w1)) + b;
            quaternion.Z = ((z1 * w2) + (z2 * w1)) + c;
            quaternion.W = (w1 * w2) - d;

            return quaternion;
        }
        
        /// <summary>
        /// Performs multiplication of a Quaternion object and a Double
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Quaternion operator * (Quaternion value1, Double scaleFactor)
        {
            Quaternion quat;
            quat.X = value1.X * scaleFactor;
            quat.Y = value1.Y * scaleFactor;
            quat.Z = value1.Z * scaleFactor;
            quat.W = value1.W * scaleFactor;
            return quat;
        }
        
        /// <summary>
        /// Performs multiplication of a Double precision scaling factor 
        /// and aQuaternion object using the (x*Y) operator.
        /// </summary>
        public static Quaternion operator * (Double scaleFactor, Quaternion value1)
        {
            Quaternion quat;
            quat.X = value1.X * scaleFactor;
            quat.Y = value1.Y * scaleFactor;
            quat.Z = value1.Z * scaleFactor;
            quat.W = value1.W * scaleFactor;
            return quat;
        }
        
        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Quaternion objects.
        /// </summary>
        public static void Divide (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            Double one = 1;

            Double x = value1.X;
            Double y = value1.Y;
            Double z = value1.Z;
            Double w = value1.W;

            Double a = 
                (value2.X * value2.X) + 
                (value2.Y * value2.Y) +
                (value2.Z * value2.Z) + 
                (value2.W * value2.W);

            Double b = one / a;

            Double c = -value2.X * b;
            Double d = -value2.Y * b;
            Double e = -value2.Z * b;
            Double f = value2.W * b;

            Double g = (y * e) - (z * d);
            Double h = (z * c) - (x * e);
            Double i = (x * d) - (y * c);
            Double j = ((x * c) + (y * d)) + (z * e);

            result.X = ((x * f) + (c * w)) + g;
            result.Y = ((y * f) + (d * w)) + h;
            result.Z = ((z * f) + (e * w)) + i;
            result.W = (w * f) - j;
        }

        /// <summary>
        /// Performs division of a Quaternion object and a Double precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Quaternion value1, Double divider, out Quaternion result)
        {
            Double one = 1;
            Double a = one / divider;

            result.X = value1.X * a;
            result.Y = value1.Y * a;
            result.Z = value1.Z * a;
            result.W = value1.W * a;
        }

        /// <summary>
        /// Performs division of two Quaternion objects using the (X/Y) operator.
        /// </summary>
        public static Quaternion operator / (Quaternion value1, Quaternion value2)
        {
            Double one = 1;

            Quaternion quaternion;

            Double x = value1.X;
            Double y = value1.Y;
            Double z = value1.Z;
            Double w = value1.W;

            Double a = 
                (value2.X * value2.X) + 
                (value2.Y * value2.Y) +
                (value2.Z * value2.Z) + 
                (value2.W * value2.W);

            Double b = one / a;

            Double c = -value2.X * b;
            Double d = -value2.Y * b;
            Double e = -value2.Z * b;
            Double f =  value2.W * b;

            Double g = (y * e) - (z * d);
            Double h = (z * c) - (x * e);
            Double i = (x * d) - (y * c);
            Double j = (x * c) + (y * d) + (z * e);

            quaternion.X = (x * f) + (c * w) + g;
            quaternion.Y = (y * f) + (d * w) + h;
            quaternion.Z = (z * f) + (e * w) + i;
            quaternion.W = (w * f) - j;

            return quaternion;
        }
        
        /// <summary>
        /// Performs division of a Quaternion object and a Double precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Quaternion operator / (Quaternion value1, Double divider)
        {
            Double one = 1;

            Quaternion quat;

            Double num = one / divider;

            quat.X = value1.X * num;
            quat.Y = value1.Y * num;
            quat.Z = value1.Z * num;
            quat.W = value1.W * num;

            return quat;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void Slerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Double amount, out Quaternion result)
        {
            Double zero = 0;
            Double one = 1;

            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

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

        /// <summary>
        /// todo
        /// </summary>
        public static void Lerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Double amount, out Quaternion result)
        {
            Double zero = 0;
            Double one = 1;

            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

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

    }    /// <summary>
    /// Double precision Vector2.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector2
        : IEquatable<Vector2>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector2.
        /// </summary>
        public Double X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector2.
        /// </summary>
        public Double Y;

        /// <summary>
        /// Initilises a new instance of Vector2 from two Double values
        /// representing X and Y respectively.
        /// </summary>
        public Vector2 (Double x, Double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Calculates the length of the Vector2.
        /// </summary>
        public Double Length ()
        {
            Double num = (this.X * this.X) + (this.Y * this.Y);
            return RealMaths.Sqrt (num);
        }

        /// <summary>
        /// Calculates the length of the Vector2 squared.
        /// </summary>
        public Double LengthSquared ()
        {
            return ((this.X * this.X) + (this.Y * this.Y));
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format (
                "{{X:{0} Y:{1}}}",
                new Object[]
                {
                    this.X.ToString (),
                    this.Y.ToString ()
                }
                );
        }

        /// <summary>
        /// Gets the hash code of the Vector2 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (this.X.GetHashCode () + this.Y.GetHashCode ());
        }

        /// <summary>
        /// Detemines whether or not the Vector2 is of unit length.
        /// </summary>
        public Boolean IsUnit()
        {
            Double one = 1;
            return RealMaths.IsZero(one - X*X - Y*Y);
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Vector2 with all of its components set to zero.
        /// </summary>
        readonly static Vector2 zero;

        /// <summary>
        /// Defines a Vector2 with all of its components set to one.
        /// </summary>
        readonly static Vector2 one;

        /// <summary>
        /// Defines the unit Vector2 for the X-axis.
        /// </summary>
        readonly static Vector2 unitX;

        /// <summary>
        /// Defines the unit Vector2 for the Y-axis.
        /// </summary>
        readonly static Vector2 unitY;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Vector2 ()
        {
            zero =      new Vector2 ();
            one =       new Vector2 (1, 1);

            unitX =     new Vector2 (1, 0);
            unitY =     new Vector2 (0, 1);
        }

        /// <summary>
        /// Returns a Vector2 with all of its components set to zero.
        /// </summary>
        public static Vector2 Zero
        {
            get { return zero; }
        }
        
        /// <summary>
        /// Returns a Vector2 with all of its components set to one.
        /// </summary>
        public static Vector2 One
        {
            get { return one; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the X-axis.
        /// </summary>
        public static Vector2 UnitX
        {
            get { return unitX; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the Y-axis.
        /// </summary>
        public static Vector2 UnitY
        {
            get { return unitY; }
        }

        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static void Distance (
            ref Vector2 vector1, ref Vector2 vector2, out Double result)
        {
            Double dx = vector1.X - vector2.X;
            Double dy = vector1.Y - vector2.Y;

            Double lengthSquared = (dx * dx) + (dy * dy);

            result = RealMaths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector2 vector1, ref Vector2 vector2, out Double result)
        {
            Double dx = vector1.X - vector2.X;
            Double dy = vector1.Y - vector2.Y;

            result = (dx * dx) + (dy * dy);
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static void Dot (
            ref Vector2 vector1, ref Vector2 vector2, out Double result)
        {
            result = (vector1.X * vector2.X) + (vector1.Y * vector2.Y);
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static void Normalise (ref Vector2 vector, out Vector2 result)
        {
            Double lengthSquared =
                (vector.X * vector.X) + (vector.Y * vector.Y);

            Double epsilon; RealMaths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Double.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double one = 1;
            Double multiplier = one / RealMaths.Sqrt (lengthSquared);

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;

        }

        /// <summary>
        /// Returns the vector of an incident vector reflected across the a
        /// specified normal vector.
        /// </summary>
        public static void Reflect (
            ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            // dot = vector . normal
            //     = |vector| * [normal] * cosθ
            //     = |vector| * cosθ
            //     = adjacent
            Double dot;
            Dot(ref vector, ref normal, out dot);

            Double two = 2;
            Double twoDot = dot * two;

            // Starting vector minus twice the length of the adjcent projected
            // along the normal.
            result = vector - (twoDot * normal);
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector2 vector, ref Matrix44 matrix, out Vector2 result)
        {
            Double x =
                (vector.X * matrix.M11) +
                (vector.Y * matrix.M21) +
                matrix.M41;

            Double y =
                (vector.X * matrix.M12) +
                (vector.Y * matrix.M22) +
                matrix.M42;

            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector2 vector, ref Quaternion rotation, out Vector2 result)
        {
            Double two = 2;

            Double x = rotation.X;
            Double y = rotation.Y;
            Double z = rotation.Z;
            Double w = rotation.W;

            Double xx = x * x;
            Double yy = y * y;
            Double zz = z * z;

            Double wz = w * z;
            Double xy = x * y;

            result.X =
                + vector.X
                - (two * vector.X  * (yy + zz))
                + (two * vector.Y  * (xy - wz));

            result.Y =
                + vector.Y
                + (two * vector.X * (xy + wz))
                - (two * vector.Y * (xx + zz));
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix.
        /// </summary>
        public static void TransformNormal (
            ref Vector2 normal, ref Matrix44 matrix, out Vector2 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Double x = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
            Double y = (normal.X * matrix.M12) + (normal.Y * matrix.M22);

            result.X = x;
            result.Y = y;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Vector2 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Vector2) {
                flag = this.Equals ((Vector2)obj);
            }
            return flag;
        }

        #region IEquatable<Vector2>

        /// <summary>
        /// Determines whether or not this Vector2 object is equal to another
        /// Vector2 object.
        /// </summary>
        public Boolean Equals (Vector2 other)
        {
            return ((this.X == other.X) && (this.Y == other.Y));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Vector2 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Vector2 value1, Vector2 value2)
        {
            return ((value1.X == value2.X) && (value1.Y == value2.Y));
        }

        /// <summary>
        /// Determines whether or not two Vector2 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Vector2 value1, Vector2 value2)
        {
            if (value1.X == value2.X) {
                return !(value1.Y == value2.Y);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector2 objects.
        /// </summary>
        public static void Add (
            ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        /// <summary>
        /// Performs addition of two Vector2 objects using the (X+Y) operator. 
        /// </summary>
        public static Vector2 operator + (Vector2 value1, Vector2 value2)
        {
            Vector2 vector;
            vector.X = value1.X + value2.X;
            vector.Y = value1.Y + value2.Y;
            return vector;
        }


        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector2 objects.
        /// </summary>
        public static void Subtract (
            ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }

        /// <summary>
        /// Performs subtraction of two Vector2 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Vector2 operator - (Vector2 value1, Vector2 value2)
        {
            Vector2 vector;
            vector.X = value1.X - value2.X;
            vector.Y = value1.Y - value2.Y;
            return vector;
        }


        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Vector2 object.
        /// </summary>
        public static void Negate (ref Vector2 value, out Vector2 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
        }

        /// <summary>
        /// Performs negation of a Vector2 object using the (-X) operator.
        /// </summary>
        public static Vector2 operator - (Vector2 value)
        {
            Vector2 vector;
            vector.X = -value.X;
            vector.Y = -value.Y;
            return vector;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector2 objects.
        /// </summary>
        public static void Multiply (
            ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
        }

        /// <summary>
        /// Performs multiplication of a Vector2 object and a Double
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Vector2 value, Double scaleFactor, out Vector2 result)
        {
            result.X = value.X * scaleFactor;
            result.Y = value.Y * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Vector2 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Vector2 operator * (
            Vector2 value1, Vector2 value2)
        {
            Vector2 vector;
            vector.X = value1.X * value2.X;
            vector.Y = value1.Y * value2.Y;
            return vector;
        }

        /// <summary>
        /// Performs multiplication of a Vector2 object and a Double
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Vector2 operator * (
            Vector2 value, Double scaleFactor)
        {
            Vector2 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            return vector;
        }

        /// <summary>
        /// Performs multiplication of a Double precision scaling factor 
        /// and aVector2 object using the (x*Y) operator.
        /// </summary>
        public static Vector2 operator * (
            Double scaleFactor, Vector2 value)
        {
            Vector2 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            return vector;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector2 objects.
        /// </summary>
        public static void Divide (
            ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
        }

        /// <summary>
        /// Performs division of a Vector2 object and a Double precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Vector2 value1, Double divider, out Vector2 result)
        {
            Double one = 1;
            Double num = one / divider;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
        }

        /// <summary>
        /// Performs division of two Vector2 objects using the (X/Y) operator.
        /// </summary>
        public static Vector2 operator / (Vector2 value1, Vector2 value2)
        {
            Vector2 vector;
            vector.X = value1.X / value2.X;
            vector.Y = value1.Y / value2.Y;
            return vector;
        }

        /// <summary>
        /// Performs division of a Vector2 object and a Double precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Vector2 operator / (Vector2 value1, Double divider)
        {
            Vector2 vector;
            Double one = 1;
            Double num = one / divider;
            vector.X = value1.X * num;
            vector.Y = value1.Y * num;
            return vector;
        }
        
        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector2 a,
            ref Vector2 b,
            Double amount,
            out Vector2 result)
        {
            Double zero = 0;
            Double one = 1;
            Double two = 2;
            Double three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            amount = (amount * amount) * (three - (two * amount));

            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// Features:
        /// - The spline passes through all of the control points.
        /// - The spline is C^1 continuous, meaning that there are no
        ///   discontinuities in the tangent direction and magnitude.
        /// - The spline is not C^2 continuous.  The second derivative is
        ///   linearly interpolated within each segment, causing the curvature
        ///   to vary linearly over the length of the segment.
        /// </summary>
        public static void CatmullRom (
            ref Vector2 a,
            ref Vector2 b,
            ref Vector2 c,
            ref Vector2 d,
            Double amount,
            out Vector2 result)
        {
            Double zero = 0;
            Double one = 1;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double half; RealMaths.Half(out half);
            Double two = 2;
            Double three = 3;
            Double four = 4;
            Double five = 5;

            Double squared = amount * amount;
            Double cubed = amount * squared;

            result.X =
                half * ((two * b.X) + ((-a.X + c.X) * amount) +
                (((two * a.X) - (five * b.X) + (four * c.X) - d.X) * squared) +
                ((-a.X + (three * b.X) - (three * c.X) + d.X) * cubed));

            result.Y = half * ((two * b.Y) + ((-a.Y + c.Y) * amount) +
                (((two * a.Y) - (five * b.Y) + (four * c.Y) - d.Y) * squared) +
                ((-a.Y + (three * b.Y) - (three * c.Y) + d.Y) * cubed));
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector2 vector1,
            ref Vector2 tangent1,
            ref Vector2 vector2,
            ref Vector2 tangent2,
            Double amount,
            out Vector2 result)
        {
            Double zero = 0;
            Double one = 1;
            Double two = 2;
            Double three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            if( !tangent1.IsUnit() || !tangent2.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double squared = amount * amount;
            Double cubed = amount * squared;

            Double a = ((two * cubed) - (three * squared)) + one;
            Double b = (-two * cubed) + (three * squared);
            Double c = (cubed - (two * squared)) + amount;
            Double d = cubed - squared;

            result.X =
                (vector1.X * a) + (vector2.X * b) +
                (tangent1.X * c) + (tangent2.X * d);

            result.Y =
                (vector1.Y * a) + (vector2.Y * b) +
                (tangent1.Y * c) + (tangent2.Y * d);
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Min (
            ref Vector2 a, 
            ref Vector2 b, 
            out Vector2 result)
        {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Max (
            ref Vector2 a, 
            ref Vector2 b, 
            out Vector2 result)
        {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static void Clamp (
            ref Vector2 a, 
            ref Vector2 min, 
            ref Vector2 max, 
            out Vector2 result)
        {
            Double x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            
            Double y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;

            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static void Lerp (
            ref Vector2 a, 
            ref Vector2 b, 
            Double amount, 
            out Vector2 result)
        {
            Double zero = 0;
            Double one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }
            
            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
        }


        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static Double Distance (
            Vector2 vector1, Vector2 vector2)
        {
            Double result;
            Distance (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static Double DistanceSquared (
            Vector2 vector1, Vector2 vector2)
        {
            Double result;
            DistanceSquared (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static Double Dot (
            Vector2 vector1, Vector2 vector2)
        {
            Double result;
            Dot (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static Vector2 Normalise (Vector2 vector)
        {
            Vector2 result;
            Normalise (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Returns the vector of an incident vector reflected across the a
        /// specified normal vector.
        /// </summary>
        public static Vector2 Reflect (
            Vector2 vector, Vector2 normal)
        {
            Vector2 result;
            Reflect (ref vector, ref normal, out result);
            return result;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix44.
        /// </summary>
        public static Vector2 Transform (
            Vector2 vector, Matrix44 matrix)
        {
            Vector2 result;
            Transform (ref vector, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Quaternion.
        /// </summary>
        public static Vector2 Transform (
            Vector2 vector, Quaternion rotation)
        {
            Vector2 result;
            Transform (ref vector, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix.
        /// </summary>
        public static Vector2 TransformNormal (
            Vector2 normal, Matrix44 matrix)
        {
            Vector2 result;
            TransformNormal (ref normal, ref matrix, out result);
            return result;
        }


        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector2 objects.
        /// </summary>
        public static Vector2 Add (
            Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Add (ref value1, ref value2, out result);
            return result;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector2 objects.
        /// </summary>
        public static Vector2 Subtract (
            Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Subtract (ref value1, ref value2, out result);
            return result;
        }

        // Negation Operators //----------------------------------------------//

        /// <summary>
        /// Performs negation of a Vector2 object.
        /// </summary>
        public static Vector2 Negate (Vector2 value)
        {
            Vector2 result;
            Negate (ref value, out result);
            return result;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector2 objects.
        /// </summary>
        public static Vector2 Multiply (
            Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Multiply (ref value1, ref value2, out result);
            return result;
        }

        /// <summary>
        /// Performs multiplication of a Vector2 object and a Double
        /// precision scaling factor.
        /// </summary>
        public static Vector2 Multiply (
            Vector2 value, Double scaleFactor)
        {
            Vector2 result;
            Multiply (ref value, scaleFactor, out result);
            return result;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector2 objects.
        /// </summary>
        public static Vector2 Divide (
            Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Divide (ref value1, ref value2, out result);
            return result;
        }

        /// <summary>
        /// Performs division of a Vector2 object and a Double precision
        /// scaling factor.
        /// </summary>
        public static Vector2 Divide (
            Vector2 value1, Double divider)
        {
            Vector2 result;
            Divide (ref value1, divider, out result);
            return result;
        }

        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static Vector2 SmoothStep (
            Vector2 a,
            Vector2 b,
            Double amount)
        {
            Vector2 result;
            SmoothStep (ref a, ref b, amount, out result);
            return result;
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        public static Vector2 CatmullRom (
            Vector2 a,
            Vector2 b,
            Vector2 c,
            Vector2 d,
            Double amount)
        {
            Vector2 result;
            CatmullRom (ref a, ref b, ref c, ref d, amount, out result);
            return result;
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static Vector2 Hermite (
            Vector2 vector1,
            Vector2 tangent1,
            Vector2 vector2,
            Vector2 tangent2,
            Double amount)
        {
            Vector2 result;
            Hermite (
                ref vector1, ref tangent1,
                ref vector2, ref tangent2,
                amount, out result);
            return result;
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching
        /// pair of components.
        /// </summary>
        public static Vector2 Min (
            Vector2 a,
            Vector2 b)
        {
            Vector2 result;
            Min (ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching
        /// pair of components.
        /// </summary>
        public static Vector2 Max (
            ref Vector2 a,
            ref Vector2 b)
        {
            Vector2 result;
            Max (ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static Vector2 Clamp (
            ref Vector2 a,
            ref Vector2 min,
            ref Vector2 max)
        {
            Vector2 result;
            Clamp (ref a, ref min, ref max, out result);
            return result;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static Vector2 Lerp (
            ref Vector2 a,
            ref Vector2 b,
            Double amount)
        {
            Vector2 result;
            Lerp (ref a, ref b, amount, out result);
            return result;
        }

    }

    /// <summary>
    /// Double precision Vector3.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector3 
        : IEquatable<Vector3>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector3.
        /// </summary>
        public Double X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector3.
        /// </summary>
        public Double Y;

        /// <summary>
        /// Gets or sets the Z-component of the Vector3.
        /// </summary>
        public Double Z;

        /// <summary>
        /// Initilises a new instance of Vector3 from three Double values 
        /// representing X, Y and Z respectively.
        /// </summary>
        public Vector3 (Double x, Double y, Double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        
        /// <summary>
        /// Initilises a new instance of Vector3 from one Vector2 value
        /// representing X and Y and one Double value representing Z.
        /// </summary>
        public Vector3 (Vector2 value, Double z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }

        /// <summary>
        /// Calculates the length of the Vector3.
        /// </summary>
        public Double Length ()
        {
            Double num = (this.X * this.X) + 
                              (this.Y * this.Y) + 
                              (this.Z * this.Z);

            return RealMaths.Sqrt (num);
        }

        /// <summary>
        /// Calculates the length of the Vector3 squared.
        /// </summary>
        public Double LengthSquared ()
        {
            return
                (this.X * this.X) + 
                (this.Y * this.Y) + 
                (this.Z * this.Z);
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format (
                "{{X:{0} Y:{1} Z:{2}}}", 
                new Object[] 
                { 
                    this.X.ToString (), 
                    this.Y.ToString (), 
                    this.Z.ToString () 
                }
                );
        }

        /// <summary>
        /// Gets the hash code of the Vector3 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (
                this.X.GetHashCode () + 
                this.Y.GetHashCode () + 
                this.Z.GetHashCode ()
                );
        }

        /// <summary>
        /// Detemines whether or not the Vector3 is of unit length.
        /// </summary>
        public Boolean IsUnit()
        {
            Double one = 1;
            return RealMaths.IsZero(one - X*X - Y*Y - Z*Z);
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Vector3 with all of its components set to zero.
        /// </summary>
        static Vector3 zero;

        /// <summary>
        /// Defines a Vector3 with all of its components set to one.
        /// </summary>
        static Vector3 one;

        /// <summary>
        /// Defines the unit Vector3 for the X-axis.
        /// </summary>
        static Vector3 unitX;

        /// <summary>
        /// Defines the unit Vector3 for the Y-axis.
        /// </summary>
        static Vector3 unitY;

        /// <summary>
        /// Defines the unit Vector3 for the Z-axis.
        /// </summary>
        static Vector3 unitZ;

        /// <summary>
        /// Defines a unit Vector3 designating up (0, 1, 0).
        /// </summary>
        static Vector3 up;

        /// <summary>
        /// Defines a unit Vector3 designating down (0, −1, 0).
        /// </summary>
        static Vector3 down;

        /// <summary>
        /// Defines a unit Vector3 pointing to the right (1, 0, 0).
        /// </summary>
        static Vector3 right;

        /// <summary>
        /// Defines a unit Vector3 designating left (−1, 0, 0).
        /// </summary>
        static Vector3 left;

        /// <summary>
        /// Defines a unit Vector3 designating forward in a right-handed 
        /// coordinate system (0, 0, -1).
        /// </summary>
        static Vector3 forward;

        /// <summary>
        /// Defines a unit Vector3 designating backward in a right-handed 
        /// coordinate system (0, 0, 1).
        /// </summary>
        static Vector3 backward;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Vector3 ()
        {
            zero =      new Vector3 ();
            one =       new Vector3 ( 1,  1,  1);

            unitX =     new Vector3 ( 1,  0,  0);
            unitY =     new Vector3 ( 0,  1,  0);
            unitZ =     new Vector3 ( 0,  0,  1);

            up =        new Vector3 ( 0,  1,  0);
            down =      new Vector3 ( 0, -1,  0);
            right =     new Vector3 ( 1,  0,  0);
            left =      new Vector3 (-1,  0,  0);
            forward =   new Vector3 ( 0,  0, -1);
            backward =  new Vector3 ( 0,  0,  1);
        }
        
        /// <summary>
        /// Returns a Vector3 with all of its components set to zero.
        /// </summary>
        public static Vector3 Zero
        {
            get { return zero; }
        }
        
        /// <summary>
        /// Returns a Vector3 with all of its components set to one.
        /// </summary>
        public static Vector3 One
        {
            get { return one; }
        }
        
        /// <summary>
        /// Returns the unit Vector3 for the X-axis.
        /// </summary>
        public static Vector3 UnitX
        {
            get { return unitX; }
        }

        /// <summary>
        /// Returns the unit Vector3 for the Y-axis.
        /// </summary>
        public static Vector3 UnitY
        {
            get { return unitY; }
        }
        
        /// <summary>
        /// Returns the unit Vector3 for the Z-axis.
        /// </summary>
        public static Vector3 UnitZ
        {
            get { return unitZ; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating up (0, 1, 0).
        /// </summary>
        public static Vector3 Up
        {
            get { return up; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating down (0, −1, 0).
        /// </summary>
        public static Vector3 Down
        {
            get { return down; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 pointing to the right (1, 0, 0).
        /// </summary>
        public static Vector3 Right
        {
            get { return right; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating left (−1, 0, 0).
        /// </summary>
        public static Vector3 Left
        {
            get { return left; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating forward in a right-handed 
        /// coordinate system (0, 0, -1).
        /// </summary>
        public static Vector3 Forward
        {
            get { return forward; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating backward in a right-handed 
        /// coordinate system (0, 0, 1).
        /// </summary>
        public static Vector3 Backward
        {
            get { return backward; }
        }
        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static void Distance (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Double result)
        {
            Double dx = vector1.X - vector2.X;
            Double dy = vector1.Y - vector2.Y;
            Double dz = vector1.Z - vector2.Z;

            Double lengthSquared = (dx * dx) + (dy * dy) + (dz * dz);

            result = RealMaths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Double result)
        {
            Double dx = vector1.X - vector2.X;
            Double dy = vector1.Y - vector2.Y;
            Double dz = vector1.Z - vector2.Z;

            result = (dx * dx) + (dy * dy) + (dz * dz);
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static void Dot (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Double result)
        {
            result =
                (vector1.X * vector2.X) +
                (vector1.Y * vector2.Y) +
                (vector1.Z * vector2.Z);
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static void Normalise (ref Vector3 vector, out Vector3 result)
        {
            Double lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z);

            Double epsilon; RealMaths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Double.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double one = 1;
            Double multiplier = one / RealMaths.Sqrt (lengthSquared);

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
        }

        /// <summary>
        /// Calculates the cross product of two vectors.
        /// </summary>
        public static void Cross (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Vector3 result)
        {
            result.X = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y);
            result.Y = (vector1.Z * vector2.X) - (vector1.X * vector2.Z);
            result.Z = (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
        }

        /// <summary>
        /// Returns the vector of an incident vector reflected across the a
        /// specified normal vector.
        /// </summary>
        public static void Reflect (
            ref Vector3 vector,
            ref Vector3 normal,
            out Vector3 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double two = 2;

            Double num =
                (vector.X * normal.X) +
                (vector.Y * normal.Y) +
                (vector.Z * normal.Z);

            result.X = vector.X - ((two * num) * normal.X);
            result.Y = vector.Y - ((two * num) * normal.Y);
            result.Z = vector.Z - ((two * num) * normal.Z);
        }

        /// <summary>
        /// Transforms a Vector3 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector3 vector,
            ref Matrix44 matrix,
            out Vector3 result)
        {
            Double x =
                (vector.X * matrix.M11) +
                (vector.Y * matrix.M21) +
                (vector.Z * matrix.M31) + matrix.M41;

            Double y =
                (vector.X * matrix.M12) +
                (vector.Y * matrix.M22) +
                (vector.Z * matrix.M32) + matrix.M42;

            Double z =
                (vector.X * matrix.M13) +
                (vector.Y * matrix.M23) +
                (vector.Z * matrix.M33) + matrix.M43;

            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Transforms a vector by a specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector3 vector,
            ref Quaternion rotation,
            out Vector3 result)
        {
            Double two = 2;

            Double x = rotation.X;
            Double y = rotation.Y;
            Double z = rotation.Z;
            Double w = rotation.W;

            Double xx = x * x;
            Double yy = y * y;
            Double zz = z * z;

            Double wx = w * x;
            Double wy = w * y;
            Double wz = w * z;
            Double xy = x * y;
            Double xz = x * z;
            Double yz = y * z;

            result.X =
                + vector.X
                - (two * vector.X * (yy + zz))
                + (two * vector.Y * (xy - wz))
                + (two * vector.Z * (xz + wy));

            result.Y =
                + vector.Y
                + (two * vector.X * (xy + wz))
                - (two * vector.Y * (xx + zz))
                + (two * vector.Z * (yz - wx));

            result.Z =
                + vector.Z
                + (two * vector.X * (xz - wy))
                + (two * vector.Y * (yz + wx))
                - (two * vector.Z * (xx + yy));
        }

        /// <summary>
        /// Transforms a normalised Vector3 by a Matrix44.
        /// </summary>
        public static void TransformNormal (
            ref Vector3 normal,
            ref Matrix44 matrix,
            out Vector3 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Double x =
                (normal.X * matrix.M11) +
                (normal.Y * matrix.M21) +
                (normal.Z * matrix.M31);

            Double y =
                (normal.X * matrix.M12) +
                (normal.Y * matrix.M22) +
                (normal.Z * matrix.M32);

            Double z =
                (normal.X * matrix.M13) +
                (normal.Y * matrix.M23) +
                (normal.Z * matrix.M33);

            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Vector3 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Vector3) {
                flag = this.Equals ((Vector3)obj);
            }
            return flag;
        }

        #region IEquatable<Vector3>

        /// <summary>
        /// Determines whether or not this Vector3 object is equal to another
        /// Vector3 object.
        /// </summary>
        public Boolean Equals (Vector3 other)
        {
            return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Vector3 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Vector3 value1, Vector3 value2)
        {
            return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z));
        }

        /// <summary>
        /// Determines whether or not two Vector3 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Vector3 value1, Vector3 value2)
        {
            if ((value1.X == value2.X) && (value1.Y == value2.Y)) {
                return !(value1.Z == value2.Z);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector3 objects.
        /// </summary>
        public static void Add (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
        }

        /// <summary>
        /// Performs addition of two Vector3 objects using the (X+Y) operator. 
        /// </summary>
        public static Vector3 operator + (Vector3 value1, Vector3 value2)
        {
            Vector3 vector;
            vector.X = value1.X + value2.X;
            vector.Y = value1.Y + value2.Y;
            vector.Z = value1.Z + value2.Z;
            return vector;
        }


        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector3 objects.
        /// </summary>
        public static void Subtract (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        /// <summary>
        /// Performs subtraction of two Vector3 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Vector3 operator - (Vector3 value1, Vector3 value2)
        {
            Vector3 vector;
            vector.X = value1.X - value2.X;
            vector.Y = value1.Y - value2.Y;
            vector.Z = value1.Z - value2.Z;
            return vector;
        }


        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Vector3 object.
        /// </summary>
        public static void Negate (ref Vector3 value, out Vector3 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
        }

        /// <summary>
        /// Performs negation of a Vector3 object using the (-X) operator.
        /// </summary>
        public static Vector3 operator - (Vector3 value)
        {
            Vector3 vector;
            vector.X = -value.X;
            vector.Y = -value.Y;
            vector.Z = -value.Z;
            return vector;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector3 objects.
        /// </summary>
        public static void Multiply (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
        }

        /// <summary>
        /// Performs multiplication of a Vector3 object and a Double
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Vector3 value1, Double scaleFactor, out Vector3 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Vector3 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Vector3 operator * (Vector3 value1, Vector3 value2)
        {
            Vector3 vector;
            vector.X = value1.X * value2.X;
            vector.Y = value1.Y * value2.Y;
            vector.Z = value1.Z * value2.Z;
            return vector;
        }

        /// <summary>
        /// Performs multiplication of a Vector3 object and a Double
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Vector3 operator * (Vector3 value, Double scaleFactor)
        {
            Vector3 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            vector.Z = value.Z * scaleFactor;
            return vector;
        }

        /// <summary>
        /// Performs multiplication of a Double precision scaling factor 
        /// and aVector3 object using the (x*Y) operator.
        /// </summary>
        public static Vector3 operator * (Double scaleFactor, Vector3 value)
        {
            Vector3 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            vector.Z = value.Z * scaleFactor;
            return vector;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector3 objects.
        /// </summary>
        public static void Divide (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
        }

        /// <summary>
        /// Performs division of a Vector3 object and a Double precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Vector3 value1, Double value2, out Vector3 result)
        {
            Double one = 1;
            Double num = one / value2;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
        }

        /// <summary>
        /// Performs division of two Vector3 objects using the (X/Y) operator.
        /// </summary>
        public static Vector3 operator / (Vector3 value1, Vector3 value2)
        {
            Vector3 vector;
            vector.X = value1.X / value2.X;
            vector.Y = value1.Y / value2.Y;
            vector.Z = value1.Z / value2.Z;
            return vector;
        }

        /// <summary>
        /// Performs division of a Vector3 object and a Double precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
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
        
        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector3 vector1,
            ref Vector3 vector2,
            Double amount,
            out Vector3 result)
        {
            Double zero = 0;
            Double one = 1;
            Double two = 2;
            Double three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
            amount = (amount * amount) * (three - (two * amount));

            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
            result.Z = vector1.Z + ((vector2.Z - vector1.Z) * amount);
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// Features:
        /// - The spline passes through all of the control points.
        /// - The spline is C^1 continuous, meaning that there are no
        ///   discontinuities in the tangent direction and magnitude.
        /// - The spline is not C^2 continuous.  The second derivative is
        ///   linearly interpolated within each segment, causing the curvature
        ///   to vary linearly over the length of the segment.
        /// </summary>
        public static void CatmullRom (
            ref Vector3 vector1,
            ref Vector3 vector2,
            ref Vector3 vector3,
            ref Vector3 vector4,
            Double amount,
            out Vector3 result)
        {
            Double half; RealMaths.Half(out half);
            Double zero = 0;
            Double one = 1;
            Double two = 2;
            Double three = 3;
            Double four = 4;
            Double five = 5;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double squared = amount * amount;
            Double cubed = amount * squared;

            result.X =
                half * ((((two * vector2.X) + ((-vector1.X + vector3.X) *
                amount)) + (((((two * vector1.X) - (five * vector2.X)) + (four *
                vector3.X)) - vector4.X) * squared)) + ((((-vector1.X + (three *
                vector2.X)) - (three * vector3.X)) + vector4.X) * cubed));

            result.Y =
                half * ((((two * vector2.Y) + ((-vector1.Y + vector3.Y) *
                amount)) + (((((two * vector1.Y) - (five * vector2.Y)) + (four *
                vector3.Y)) - vector4.Y) * squared)) + ((((-vector1.Y + (three *
                vector2.Y)) - (three * vector3.Y)) + vector4.Y) * cubed));

            result.Z =
                half * ((((two * vector2.Z) + ((-vector1.Z + vector3.Z) *
                amount)) + (((((two * vector1.Z) - (five * vector2.Z)) + (four *
                vector3.Z)) - vector4.Z) * squared)) + ((((-vector1.Z + (three *
                vector2.Z)) - (three * vector3.Z)) + vector4.Z) * cubed));
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector3 vector1,
            ref Vector3 tangent1,
            ref Vector3 vector2,
            ref Vector3 tangent2,
            Double amount,
            out Vector3 result)
        {
            Double zero = 0;
            Double one = 1;
            Double two = 2;
            Double three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            if( !tangent1.IsUnit() || !tangent2.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double squared = amount * amount;
            Double cubed = amount * squared;

            Double a = ((two * cubed) - (three * squared)) + one;
            Double b = (-two * cubed) + (three * squared);
            Double c = (cubed - (two * squared)) + amount;
            Double d = cubed - squared;

            result.X =
                (vector1.X * a) + (vector2.X * b) +
                (tangent1.X * c) + (tangent2.X * d);

            result.Y =
                (vector1.Y * a) + (vector2.Y * b) +
                (tangent1.Y * c) + (tangent2.Y * d);

            result.Z =
                (vector1.Z * a) + (vector2.Z * b) +
                (tangent1.Z * c) + (tangent2.Z * d);
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Min (
            ref Vector3 a,
            ref Vector3 b,
            out Vector3 result)
        {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
            result.Z = (a.Z < b.Z) ? a.Z : b.Z;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Max (
            ref Vector3 a,
            ref Vector3 b,
            out Vector3 result)
        {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
            result.Z = (a.Z > b.Z) ? a.Z : b.Z;
        }
        
        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static void Clamp (
            ref Vector3 a,
            ref Vector3 min,
            ref Vector3 max,
            out Vector3 result)
        {
            Double x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            Double y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            Double z = a.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static void Lerp (
            ref Vector3 a,
            ref Vector3 b,
            Double amount,
            out Vector3 result)
        {
            Double zero = 0;
            Double one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }
            
            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
            result.Z = a.Z + ((b.Z - a.Z) * amount);
        }
    }

    /// <summary>
    /// Double precision Vector4.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector4 
        : IEquatable<Vector4>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector4.
        /// </summary>
        public Double X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector4.
        /// </summary>
        public Double Y;

        /// <summary>
        /// Gets or sets the Z-component of the Vector4.
        /// </summary>
        public Double Z;

        /// <summary>
        /// Gets or sets the W-component of the Vector4.
        /// </summary>
        public Double W;

        /// <summary>
        /// Initilises a new instance of Vector4 from four Double values 
        /// representing X, Y, Z and W respectively.
        /// </summary>
        public Vector4 (
            Double x, 
            Double y, 
            Double z, 
            Double w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initilises a new instance of Vector4 from one Vector2 value
        /// representing X and Y and two Double values representing Z and
        /// W respectively.
        /// </summary>
        public Vector4 (Vector2 value, Double z, Double w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initilises a new instance of Vector4 from one Vector3 value
        /// representing X, Y and Z and one Double value representing W.
        /// </summary>
        public Vector4 (Vector3 value, Double w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        /// <summary>
        /// Calculates the length of the Vector4.
        /// </summary>
        public Double Length ()
        {
            Double num = (this.X * this.X) + 
                              (this.Y * this.Y) + 
                              (this.Z * this.Z) + 
                              (this.W * this.W);
            
            return RealMaths.Sqrt (num);
        }

        /// <summary>
        /// Calculates the length of the Vector4 squared.
        /// </summary>
        public Double LengthSquared ()
        {
            return 
                (this.X * this.X) + 
                (this.Y * this.Y) + 
                (this.Z * this.Z) + 
                (this.W * this.W);
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format (
                "{{X:{0} Y:{1} Z:{2} W:{3}}}", 
                new Object[] 
                { 
                    this.X.ToString (), 
                    this.Y.ToString (), 
                    this.Z.ToString (), 
                    this.W.ToString () 
                }
                );
        }

        /// <summary>
        /// Gets the hash code of the Vector4 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (
                this.X.GetHashCode () + 
                this.Y.GetHashCode () + 
                this.Z.GetHashCode () + 
                this.W.GetHashCode ()
                );
        }

        /// <summary>
        /// Detemines whether or not the Vector4 is of unit length.
        /// </summary>
        public Boolean IsUnit()
        {
            Double one = 1;
            return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Vector2 with all of its components set to zero.
        /// </summary>
        static Vector4 zero;

        /// <summary>
        /// Defines a Vector2 with all of its components set to one.
        /// </summary>
        static Vector4 one;

        /// <summary>
        /// Defines the unit Vector2 for the X-axis.
        /// </summary>
        static Vector4 unitX;

        /// <summary>
        /// Defines the unit Vector2 for the Y-axis.
        /// </summary>
        static Vector4 unitY;

        /// <summary>
        /// Defines the unit Vector2 for the Z-axis.
        /// </summary>
        static Vector4 unitZ;

        /// <summary>
        /// Defines the unit Vector2 for the W-axis.
        /// </summary>
        static Vector4 unitW;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Vector4 ()
        {
            zero =      new Vector4 ();
            one =       new Vector4 (1, 1, 1, 1);

            unitX =     new Vector4 (1, 0, 0, 0);
            unitY =     new Vector4 (0, 1, 0, 0);
            unitZ =     new Vector4 (0, 0, 1, 0);
            unitW =     new Vector4 (0, 0, 0, 1);
        }

        /// <summary>
        /// Returns a Vector4 with all of its components set to zero.
        /// </summary>
        public static Vector4 Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Returns a Vector4 with all of its components set to one.
        /// </summary>
        public static Vector4 One
        {
            get { return one; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the X-axis.
        /// </summary>
        public static Vector4 UnitX
        {
            get { return unitX; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the Y-axis.
        /// </summary>
        public static Vector4 UnitY
        {
            get { return unitY; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the Z-axis.
        /// </summary>
        public static Vector4 UnitZ
        {
            get { return unitZ; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the W-axis.
        /// </summary>
        public static Vector4 UnitW
        {
            get { return unitW; }
        }
        
        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static void Distance (
            ref Vector4 vector1,
            ref Vector4 vector2,
            out Double result)
        {
            Double dx = vector1.X - vector2.X;
            Double dy = vector1.Y - vector2.Y;
            Double dz = vector1.Z - vector2.Z;
            Double dw = vector1.W - vector2.W;

            Double lengthSquared =
                (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);

            result = RealMaths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector4 vector1,
            ref Vector4 vector2,
            out Double result)
        {
            Double dx = vector1.X - vector2.X;
            Double dy = vector1.Y - vector2.Y;
            Double dz = vector1.Z - vector2.Z;
            Double dw = vector1.W - vector2.W;

            result = (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static void Dot (
            ref Vector4 vector1,
            ref Vector4 vector2,
            out Double result)
        {
            result =
                (vector1.X * vector2.X) +
                (vector1.Y * vector2.Y) +
                (vector1.Z * vector2.Z) +
                (vector1.W * vector2.W);
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static void Normalise (
            ref Vector4 vector,
            out Vector4 result)
        {
            Double lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z) +
                (vector.W * vector.W);

            Double epsilon; RealMaths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Double.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double one = 1;
            Double multiplier = one / (RealMaths.Sqrt (lengthSquared));

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
            result.W = vector.W * multiplier;
        }

        /// <summary>
        /// Transforms a Vector4 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector4 vector,
            ref Matrix44 matrix,
            out Vector4 result)
        {
            Double x =
                (vector.X * matrix.M11) +
                (vector.Y * matrix.M21) +
                (vector.Z * matrix.M31) +
                (vector.W * matrix.M41);

            Double y =
                (vector.X * matrix.M12) +
                (vector.Y * matrix.M22) +
                (vector.Z * matrix.M32) +
                (vector.W * matrix.M42);

            Double z =
                (vector.X * matrix.M13) +
                (vector.Y * matrix.M23) +
                (vector.Z * matrix.M33) +
                (vector.W * matrix.M43);

            Double w =
                (vector.X * matrix.M14) +
                (vector.Y * matrix.M24) +
                (vector.Z * matrix.M34) +
                (vector.W * matrix.M44);

            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }

        /// <summary>
        /// Transforms a Vector4 by the specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector4 vector,
            ref Quaternion rotation,
            out Vector4 result)
        {
            Double two = 2;

            Double x = rotation.X;
            Double y = rotation.Y;
            Double z = rotation.Z;
            Double w = rotation.W;

            Double xx = x * x;
            Double yy = y * y;
            Double zz = z * z;

            Double wx = w * x;
            Double wy = w * y;
            Double wz = w * z;
            Double xy = x * y;
            Double xz = x * z;
            Double yz = y * z;

            result.X =
                + vector.X
                - (two * vector.X * (yy + zz))
                + (two * vector.Y * (xy - wz))
                + (two * vector.Z * (xz + wy));

            result.Y =
                + vector.Y
                + (two * vector.X * (xy + wz))
                - (two * vector.Y * (xx + zz))
                + (two * vector.Z * (yz - wx));

            result.Z =
                + vector.Z
                + (two * vector.X * (xz - wy))
                + (two * vector.Y * (yz + wx))
                - (two * vector.Z * (xx + yy));

            result.W = vector.W;
        }

        /// <summary>
        /// Transforms a normalised Vector4 by a Matrix44.
        /// </summary>
        public static void TransformNormal (
            ref Vector4 normal,
            ref Matrix44 matrix,
            out Vector4 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Double x =
                (normal.X * matrix.M11) + (normal.Y * matrix.M21) +
                (normal.Z * matrix.M31) + (normal.W * matrix.M41);

            Double y =
                (normal.X * matrix.M12) + (normal.Y * matrix.M22) +
                (normal.Z * matrix.M32) + (normal.W * matrix.M42);

            Double z =
                (normal.X * matrix.M13) + (normal.Y * matrix.M23) +
                (normal.Z * matrix.M33) + (normal.W * matrix.M43);

            Double w =
                (normal.X * matrix.M14) + (normal.Y * matrix.M24) +
                (normal.Z * matrix.M34) + (normal.W * matrix.M44);

            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Vector4 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Vector4) {
                flag = this.Equals ((Vector4)obj);
            }
            return flag;
        }

        #region IEquatable<Vector4>

        /// <summary>
        /// Determines whether or not this Vector4 object is equal to another
        /// Vector4 object.
        /// </summary>
        public Boolean Equals (Vector4 other)
        {
            return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Vector4 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Vector4 value1, Vector4 value2)
        {
            return ((((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) && (value1.W == value2.W));
        }
        
        /// <summary>
        /// Determines whether or not two Vector4 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Vector4 value1, Vector4 value2)
        {
            if (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) {
                return !(value1.W == value2.W);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector4 objects.
        /// </summary>
        public static void Add (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
            result.W = value1.W + value2.W;
        }

        /// <summary>
        /// Performs addition of two Vector4 objects using the (X+Y) operator. 
        /// </summary>
        public static Vector4 operator + (Vector4 value1, Vector4 value2)
        {
            Vector4 vector;
            vector.X = value1.X + value2.X;
            vector.Y = value1.Y + value2.Y;
            vector.Z = value1.Z + value2.Z;
            vector.W = value1.W + value2.W;
            return vector;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector4 objects.
        /// </summary>
        public static void Subtract (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
            result.W = value1.W - value2.W;
        }

        /// <summary>
        /// Performs subtraction of two Vector4 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Vector4 operator - (Vector4 value1, Vector4 value2)
        {
            Vector4 vector;
            vector.X = value1.X - value2.X;
            vector.Y = value1.Y - value2.Y;
            vector.Z = value1.Z - value2.Z;
            vector.W = value1.W - value2.W;
            return vector;
        }

        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Vector4 object.
        /// </summary>
        public static void Negate (ref Vector4 value, out Vector4 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        /// <summary>
        /// Performs negation of a Vector4 object using the (-X) operator.
        /// </summary>
        public static Vector4 operator - (Vector4 value)
        {
            Vector4 vector;
            vector.X = -value.X;
            vector.Y = -value.Y;
            vector.Z = -value.Z;
            vector.W = -value.W;
            return vector;
        }
        
        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector4 objects.
        /// </summary>
        public static void Multiply (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
            result.W = value1.W * value2.W;
        }

        /// <summary>
        /// Performs multiplication of a Vector4 object and a Double
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Vector4 value1, Double scaleFactor, out Vector4 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
            result.W = value1.W * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Vector4 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Vector4 operator * (Vector4 value1, Vector4 value2)
        {
            Vector4 vector;
            vector.X = value1.X * value2.X;
            vector.Y = value1.Y * value2.Y;
            vector.Z = value1.Z * value2.Z;
            vector.W = value1.W * value2.W;
            return vector;
        }
        
        /// <summary>
        /// Performs multiplication of a Vector4 object and a Double
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Vector4 operator * (Vector4 value1, Double scaleFactor)
        {
            Vector4 vector;
            vector.X = value1.X * scaleFactor;
            vector.Y = value1.Y * scaleFactor;
            vector.Z = value1.Z * scaleFactor;
            vector.W = value1.W * scaleFactor;
            return vector;
        }
        
        /// <summary>
        /// Performs multiplication of a Double precision scaling factor 
        /// and aVector4 object using the (x*Y) operator.
        /// </summary>
        public static Vector4 operator * (Double scaleFactor, Vector4 value1)
        {
            Vector4 vector;
            vector.X = value1.X * scaleFactor;
            vector.Y = value1.Y * scaleFactor;
            vector.Z = value1.Z * scaleFactor;
            vector.W = value1.W * scaleFactor;
            return vector;
        }
        
        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector4 objects.
        /// </summary>
        public static void Divide (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
            result.W = value1.W / value2.W;
        }

        /// <summary>
        /// Performs division of a Vector4 object and a Double precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Vector4 value1, Double divider, out Vector4 result)
        {
            Double one = 1;
            Double num = one / divider;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
            result.W = value1.W * num;
        }

        /// <summary>
        /// Performs division of two Vector4 objects using the (X/Y) operator.
        /// </summary>
        public static Vector4 operator / (Vector4 value1, Vector4 value2)
        {
            Vector4 vector;
            vector.X = value1.X / value2.X;
            vector.Y = value1.Y / value2.Y;
            vector.Z = value1.Z / value2.Z;
            vector.W = value1.W / value2.W;
            return vector;
        }
        
        /// <summary>
        /// Performs division of a Vector4 object and a Double precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
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

        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector4 vector1,
            ref Vector4 vector2,
            Double amount,
            out Vector4 result)
        {
            Double zero = 0;
            Double one = 1;
            Double two = 2;
            Double three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
            amount = (amount * amount) * (three - (two * amount));

            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
            result.Z = vector1.Z + ((vector2.Z - vector1.Z) * amount);
            result.W = vector1.W + ((vector2.W - vector1.W) * amount);
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// Features:
        /// - The spline passes through all of the control points.
        /// - The spline is C^1 continuous, meaning that there are no
        ///   discontinuities in the tangent direction and magnitude.
        /// - The spline is not C^2 continuous.  The second derivative is
        ///   linearly interpolated within each segment, causing the curvature
        ///   to vary linearly over the length of the segment.
        /// </summary>
        public static void CatmullRom (
            ref Vector4 vector1,
            ref Vector4 vector2,
            ref Vector4 vector3,
            ref Vector4 vector4,
            Double amount,
            out Vector4 result)
        {
            Double half; RealMaths.Half(out half);
            Double zero = 0;
            Double one = 1;
            Double two = 2;
            Double three = 3;
            Double four = 4;
            Double five = 5;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double squared = amount * amount;
            Double cubed = amount * squared;

            result.X =
                half * ((((two * vector2.X) + ((-vector1.X + vector3.X) *
                amount)) + (((((two * vector1.X) - (five * vector2.X)) + (four *
                vector3.X)) - vector4.X) * squared)) + ((((-vector1.X + (three *
                vector2.X)) - (three * vector3.X)) + vector4.X) * cubed));

            result.Y =
                half * ((((two * vector2.Y) + ((-vector1.Y + vector3.Y) *
                amount)) + (((((two * vector1.Y) - (five * vector2.Y)) + (four *
                vector3.Y)) - vector4.Y) * squared)) + ((((-vector1.Y + (three *
                vector2.Y)) - (three * vector3.Y)) + vector4.Y) * cubed));

            result.Z =
                half * ((((two * vector2.Z) + ((-vector1.Z + vector3.Z) *
                amount)) + (((((two * vector1.Z) - (five * vector2.Z)) + (four *
                vector3.Z)) - vector4.Z) * squared)) + ((((-vector1.Z + (three *
                vector2.Z)) - (three * vector3.Z)) + vector4.Z) * cubed));

            result.W =
                half * ((((two * vector2.W) + ((-vector1.W + vector3.W) *
                amount)) + (((((two * vector1.W) - (five * vector2.W)) + (four *
                vector3.W)) - vector4.W) * squared)) + ((((-vector1.W + (three *
                vector2.W)) - (three * vector3.W)) + vector4.W) * cubed));
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector4 vector1,
            ref Vector4 tangent1,
            ref Vector4 vector2,
            ref Vector4 tangent2,
            Double amount,
            out Vector4 result)
        {
            Double zero = 0;
            Double one = 1;
            Double two = 2;
            Double three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            if( !tangent1.IsUnit() || !tangent2.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double squared = amount * amount;
            Double cubed = amount * squared;

            Double a = ((two * cubed) - (three * squared)) + one;
            Double b = (-two * cubed) + (three * squared);
            Double c = (cubed - (two * squared)) + amount;
            Double d = cubed - squared;

            result.X =
                (vector1.X * a) + (vector2.X * b) +
                (tangent1.X * c) + (tangent2.X * d);

            result.Y =
                (vector1.Y * a) + (vector2.Y * b) +
                (tangent1.Y * c) + (tangent2.Y * d);

            result.Z =
                (vector1.Z * a) + (vector2.Z * b) +
                (tangent1.Z * c) + (tangent2.Z * d);

            result.W =
                (vector1.W * a) + (vector2.W * b) +
                (tangent1.W * c) + (tangent2.W * d);
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Min (
            ref Vector4 a,
            ref Vector4 b,
            out Vector4 result)
        {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
            result.Z = (a.Z < b.Z) ? a.Z : b.Z;
            result.W = (a.W < b.W) ? a.W : b.W;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Max (
            ref Vector4 a,
            ref Vector4 b,
            out Vector4 result)
        {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
            result.Z = (a.Z > b.Z) ? a.Z : b.Z;
            result.W = (a.W > b.W) ? a.W : b.W;
        }
        
        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static void Clamp (
            ref Vector4 a,
            ref Vector4 min,
            ref Vector4 max,
            out Vector4 result)
        {
            Double x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            Double y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            Double z = a.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            Double w = a.W;
            w = (w > max.W) ? max.W : w;
            w = (w < min.W) ? min.W : w;
            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }
        
        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static void Lerp (
            ref Vector4 a,
            ref Vector4 b,
            Double amount,
            out Vector4 result)
        {
            Double zero = 0;
            Double one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }
            
            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
            result.Z = a.Z + ((b.Z - a.Z) * amount);
            result.W = a.W + ((b.W - a.W) * amount);
        }

    }

}

namespace Abacus.Fixed32Precision
{
    /// <summary>
    /// todo
    /// </summary>
    internal class GjkDistance
    {
        /// <summary>
        /// todo
        /// </summary>
        internal GjkDistance ()
        {
            for (Int32 i = 0; i < 0x10; i++)
            {
                this.det [i] = new Fixed32[4];
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        internal Boolean AddSupportPoint (ref Vector3 newPoint)
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

                this.edgeLengthSq [index] [num2] = 
                    this.edgeLengthSq [num2] [index] = 
                        vector.LengthSquared ();
            }

            this.UpdateDeterminant (index);

            return this.UpdateSimplex (index);
        }

        /// <summary>
        /// todo
        /// </summary>
        internal void Reset ()
        {
            Fixed32 zero = 0;

            this.simplexBits = 0;
            this.maxLengthSq = zero;
        }

        /// <summary>
        /// todo
        /// </summary>
        internal Vector3 ClosestPoint
        {
            get { return this.closestPoint; }
        }

        /// <summary>
        /// todo
        /// </summary>
        internal Boolean FullSimplex
        {
            get { return (this.simplexBits == 15); }
        }
        
        /// <summary>
        /// todo
        /// </summary>
        internal Fixed32 MaxLengthSquared
        {
            get { return this.maxLengthSq; }
        }

        /// <summary>
        /// todo
        /// </summary>
        Vector3 closestPoint;

        /// <summary>
        /// todo
        /// </summary>
        Fixed32[][] det = new Fixed32[0x10][];

        /// <summary>
        /// todo
        /// </summary>
        Fixed32[][] edgeLengthSq = 
            new Fixed32[][] 
            { 
                new Fixed32[4], 
                new Fixed32[4], 
                new Fixed32[4], 
                new Fixed32[4] 
            };
        
        /// <summary>
        /// todo
        /// </summary>
        Vector3[][] edges = 
            new Vector3[][] 
            { 
                new Vector3[4], 
                new Vector3[4], 
                new Vector3[4], 
                new Vector3[4] 
            };
        
        /// <summary>
        /// todo
        /// </summary>
        Fixed32 maxLengthSq;

        /// <summary>
        /// todo
        /// </summary>
        Int32 simplexBits;

        /// <summary>
        /// todo
        /// </summary>
        Vector3[] y = new Vector3[4];

        /// <summary>
        /// todo
        /// </summary>
        Fixed32[] yLengthSq = new Fixed32[4];

        /// <summary>
        /// todo
        /// </summary>
        static Int32[] BitsToIndices = 
            new Int32[] 
            { 
                0, 1, 2, 0x11, 3, 0x19, 0x1a, 0xd1, 
                4, 0x21, 0x22, 0x111, 0x23, 0x119, 0x11a, 0x8d1 
            };

        /// <summary>
        /// todo
        /// </summary>
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

                this.maxLengthSq = 
                RealMaths.Max (this.maxLengthSq, this.yLengthSq [index]);
            }

            return (Vector3)(zero / num3);
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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

                this.det [num6] [num] = 
                    Dot (ref this.edges [xmIdx] [num], ref this.y [xmIdx]);

                this.det [num6] [xmIdx] = 
                    Dot (ref this.edges [num] [xmIdx], ref this.y [num]);

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

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        static Fixed32 Dot (ref Vector3 a, ref Vector3 b)
        {
            return (((a.X * b.X) + (a.Y * b.Y)) + (a.Z * b.Z));
        }
    }
    /// <summary>
    /// Fixed32 precision Matrix44.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Matrix44 
        : IEquatable<Matrix44>
    {
        /// <summary>
        /// Gets or sets (Row 1, Column 1) of the Matrix44.
        /// </summary>
        public Fixed32 M11;

        /// <summary>
        /// Gets or sets (Row 1, Column 2) of the Matrix44.
        /// </summary>
        public Fixed32 M12;

        /// <summary>
        /// Gets or sets (Row 1, Column 3) of the Matrix44.
        /// </summary>
        public Fixed32 M13;

        /// <summary>
        /// Gets or sets (Row 1, Column 4) of the Matrix44.
        /// </summary>
        public Fixed32 M14;

        /// <summary>
        /// Gets or sets (Row 2, Column 1) of the Matrix44.
        /// </summary>
        public Fixed32 M21;

        /// <summary>
        /// Gets or sets (Row 2, Column 2) of the Matrix44.
        /// </summary>
        public Fixed32 M22;

        /// <summary>
        /// Gets or sets (ow 2, Column 3) of the Matrix44.
        /// </summary>
        public Fixed32 M23;

        /// <summary>
        /// Gets or sets (Row 2, Column 4) of the Matrix44.
        /// </summary>
        public Fixed32 M24;

        /// <summary>
        /// Row 3, Column 1) of the Matrix44.
        /// </summary>
        public Fixed32 M31;

        /// <summary>
        /// Gets or sets (Row 3, Column 2) of the Matrix44.
        /// </summary>
        public Fixed32 M32;

        /// <summary>
        /// Gets or sets (Row 3, Column 3) of the Matrix44.
        /// </summary>
        public Fixed32 M33;

        /// <summary>
        /// Gets or sets (Row 3, Column 4) of the Matrix44.
        /// </summary>
        public Fixed32 M34;

        /// <summary>
        /// Gets or sets (Row 4, Column 1) of the Matrix44.
        /// </summary>
        public Fixed32 M41; // translation.x

        /// <summary>
        /// Gets or sets (Row 4, Column 2) of the Matrix44.
        /// </summary>
        public Fixed32 M42; // translation.y

        /// <summary>
        /// Gets or sets (Row 4, Column 3) of the Matrix44.
        /// </summary>
        public Fixed32 M43; // translation.z

        /// <summary>
        /// Gets or sets (Row 4, Column 4) of the Matrix44.
        /// </summary>
        public Fixed32 M44;
        
        /// <summary>
        /// Initilises a new instance of Matrix44 from sixteen Fixed32 
        /// values representing the matrix, in row major order, respectively.
        /// </summary>
        public Matrix44 (
            Fixed32 m11, 
            Fixed32 m12, 
            Fixed32 m13, 
            Fixed32 m14, 
            Fixed32 m21, 
            Fixed32 m22, 
            Fixed32 m23, 
            Fixed32 m24, 
            Fixed32 m31, 
            Fixed32 m32, 
            Fixed32 m33, 
            Fixed32 m34, 
            Fixed32 m41, 
            Fixed32 m42, 
            Fixed32 m43, 
            Fixed32 m44)
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

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return 
                (
                    "{ " + 
                    string.Format ("{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", 
                        new Object[] 
                        { 
                            this.M11.ToString (), 
                            this.M12.ToString (), 
                            this.M13.ToString (), 
                            this.M14.ToString () 
                        }
                    ) + 
                    string.Format ("{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", 
                        new Object[] 
                        { 
                            this.M21.ToString (), 
                            this.M22.ToString (), 
                            this.M23.ToString (), 
                            this.M24.ToString () 
                            }
                    ) + 
                    string.Format ("{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", 
                        new Object[] 
                        { 
                            this.M31.ToString (), 
                            this.M32.ToString (), 
                            this.M33.ToString (), 
                            this.M34.ToString () 
                        }
                    ) + string.Format ("{{M41:{0} M42:{1} M43:{2} M44:{3}}} ", 
                    new Object[] 
                    { 
                        this.M41.ToString (), 
                        this.M42.ToString (), 
                        this.M43.ToString (), 
                        this.M44.ToString () 
                    }
                    ) + 
                    "}"
                );
        }

        /// <summary>
        /// Gets the hash code of the Matrix44 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return 
                (((((((((((((((
                    this.M11.GetHashCode () + 
                    this.M12.GetHashCode ()) + 
                    this.M13.GetHashCode ()) + 
                    this.M14.GetHashCode ()) + 
                    this.M21.GetHashCode ()) + 
                    this.M22.GetHashCode ()) + 
                    this.M23.GetHashCode ()) + 
                    this.M24.GetHashCode ()) + 
                    this.M31.GetHashCode ()) + 
                    this.M32.GetHashCode ()) + 
                    this.M33.GetHashCode ()) + 
                    this.M34.GetHashCode ()) + 
                    this.M41.GetHashCode ()) + 
                    this.M42.GetHashCode ()) + 
                    this.M43.GetHashCode ()) + 
                    this.M44.GetHashCode ());
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Up 
        {
            get 
            {
                Vector3 vector;
                vector.X = this.M21;
                vector.Y = this.M22;
                vector.Z = this.M23;
                return vector;
            }
            set 
            {
                this.M21 = value.X;
                this.M22 = value.Y;
                this.M23 = value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Down 
        {
            get 
            {
                Vector3 vector;
                vector.X = -this.M21;
                vector.Y = -this.M22;
                vector.Z = -this.M23;
                return vector;
            }
            set 
            {
                this.M21 = -value.X;
                this.M22 = -value.Y;
                this.M23 = -value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Right 
        {
            get 
            {
                Vector3 vector;
                vector.X = this.M11;
                vector.Y = this.M12;
                vector.Z = this.M13;
                return vector;
            }
            set 
            {
                this.M11 = value.X;
                this.M12 = value.Y;
                this.M13 = value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Left
        {
            get
            {
                Vector3 vector;
                vector.X = -this.M11;
                vector.Y = -this.M12;
                vector.Z = -this.M13;
                return vector;
            }
            set
            {
                this.M11 = -value.X;
                this.M12 = -value.Y;
                this.M13 = -value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Forward
        {
            get
            {
                Vector3 vector;
                vector.X = -this.M31;
                vector.Y = -this.M32;
                vector.Z = -this.M33;
                return vector;
            }
            set 
            {
                this.M31 = -value.X;
                this.M32 = -value.Y;
                this.M33 = -value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Backward
        {
            get
            {
                Vector3 vector;
                vector.X = this.M31;
                vector.Y = this.M32;
                vector.Z = this.M33;
                return vector;
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
                this.M33 = value.Z;
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        public Vector3 Translation
        {
            get 
            {
                Vector3 vector;
                vector.X = this.M41;
                vector.Y = this.M42;
                vector.Z = this.M43;
                return vector;
            }
            set
            {
                this.M41 = value.X;
                this.M42 = value.Y;
                this.M43 = value.Z;
            }
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines the identity matrix.
        /// </summary>
        static Matrix44 identity;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Matrix44 ()
        {
            identity = new Matrix44 (
                1, 0, 0, 0, 
                0, 1, 0, 0, 
                0, 0, 1, 0, 
                0, 0, 0, 1);
        }

        /// <summary>
        /// Returns the identity matrix.
        /// </summary>
        public static Matrix44 Identity 
        {
            get { return identity; }
        }
        
        /// <summary>
        /// todo
        /// </summary>
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
        
        /// <summary>
        /// todo
        /// </summary>
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
        
        /// <summary>
        /// Creates a scaling matrix based on x, y, z.
        /// </summary>
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

        /// <summary>
        /// Creates a scaling matrix based on a vector.
        /// </summary>
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

        /// <summary>
        /// Create a scaling matrix consistant along each axis
        /// </summary>
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

        /// <summary>
        /// http://en.wikipedia.org/wiki/Rotation_matrix
        /// </summary>
        public static void CreateRotationX (Fixed32 radians, out Matrix44 result)
        {
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

        /// <summary>
        /// http://en.wikipedia.org/wiki/Rotation_matrix
        /// </summary>
        public static void CreateRotationY (Fixed32 radians, out Matrix44 result)
        {
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

        /// <summary>
        /// http://en.wikipedia.org/wiki/Rotation_matrix
        /// </summary>
        public static void CreateRotationZ (Fixed32 radians, out Matrix44 result)
        {
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
        
        /// <summary>
        /// todo
        /// </summary>
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
        
        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        public static void CreateWorldNew (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result)
        {
            Vector3 backward = -forward;

            Vector3 right;

            Vector3.Cross (ref up, ref backward, out right);

            Vector3.Normalise(ref right, out right);

            Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out result);

            result.M41 = position.X;
            result.M42 = position.Y;
            result.M43 = position.Z;
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        public static void CreateFromYawPitchRoll (Fixed32 yaw, Fixed32 pitch, Fixed32 roll, out Matrix44 result)
        {
            Quaternion quaternion;

            Quaternion.CreateFromYawPitchRoll (yaw, pitch, roll, out quaternion);

            CreateFromQuaternion (ref quaternion, out result);
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Creates a cylindrical billboard that rotates around a specified axis.
        /// This method computes the facing direction of the billboard from the object position and camera position. 
        /// When the object and camera positions are too close, the matrix will not be accurate. 
        /// To avoid this problem, the method uses the optional camera forward vector if the positions are too close.
        /// </summary>
        public static void CreateBillboard (ref Vector3 ObjectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix44 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

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

            Vector3.Normalise (ref vector3, out vector3);

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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// todo
        /// </summary>
        public static void CreateConstrainedBillboard (
            ref Vector3 objectPosition, 
            ref Vector3 cameraPosition, 
            ref Vector3 rotateAxis, 
            Vector3? cameraForwardVector, 
            Vector3? objectForwardVector, 
            out Matrix44 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

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
                Vector3.Normalise (ref vector3, out vector3);
                Vector3.Cross (ref vector3, ref rotateAxis, out vector);
                Vector3.Normalise (ref vector, out vector);
            } else {
                Vector3.Cross (ref rotateAxis, ref vector2, out vector3);
                Vector3.Normalise (ref vector3, out vector3);
                Vector3.Cross (ref vector3, ref vector4, out vector);
                Vector3.Normalise (ref vector, out vector);
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

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
        /// </summary>
        public static void CreatePerspectiveFieldOfView (
            Fixed32 fieldOfView, 
            Fixed32 aspectRatio, 
            Fixed32 nearPlaneDistance, 
            Fixed32 farPlaneDistance, 
            out Matrix44 result)
        {
            Fixed32 zero = 0;
            Fixed32 half; RealMaths.Half(out half);
            Fixed32 one = 1;
            Fixed32 pi; RealMaths.Pi(out pi);

            if ((fieldOfView <= zero) || (fieldOfView >= pi))
            {
                throw new ArgumentOutOfRangeException ("fieldOfView");
            }

            if (nearPlaneDistance <= zero)
            {
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");
            }

            if (farPlaneDistance <= zero)
            {
                throw new ArgumentOutOfRangeException ("farPlaneDistance");
            }

            if (nearPlaneDistance >= farPlaneDistance)
            {
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");
            }

            //
            // xScale     0          0              0
            // 0        yScale       0              0
            // 0        0        zf/(zn-zf)        -1
            // 0        0        zn*zf/(zn-zf)      0
            //
            // where:
            //
            // yScale = cot(fovY/2)
            //     
            // xScale = yScale / aspect ratio
            //

            // yScale = cot(fovY/2)
            Fixed32 yScale = one / ( RealMaths.Tan ( fieldOfView * half ) );

            // xScale = yScale / aspect ratio
            Fixed32 xScale = yScale / aspectRatio;

            result.M11 = xScale;
            result.M12 = zero;
            result.M13 = zero;
            result.M14 = zero;
            
            result.M21 = zero;
            result.M22 = yScale;
            result.M23 = zero;
            result.M24 = zero;

            result.M31 = zero;
            result.M32 = zero;
            result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance); // zf/(zn-zf)
            result.M34 = -one;

            result.M41 = zero;
            result.M42 = zero;
            result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance); // zn*zf/(zn-zf)
            result.M44 = zero;
        }



        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
        /// </summary>
        public static void CreatePerspective (
            Fixed32 width, 
            Fixed32 height, 
            Fixed32 nearPlaneDistance, 
            Fixed32 farPlaneDistance, 
            out Matrix44 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx
        /// </summary>
        public static void CreatePerspectiveOffCenter (
            Fixed32 left, 
            Fixed32 right, 
            Fixed32 bottom, 
            Fixed32 top, 
            Fixed32 nearPlaneDistance, 
            Fixed32 farPlaneDistance, 
            out Matrix44 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
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
        
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
        /// </summary>
        public static void CreateOrthographic (
            Fixed32 width, 
            Fixed32 height, 
            Fixed32 zNearPlane, 
            Fixed32 zFarPlane, 
            out Matrix44 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx
        /// </summary>
        public static void CreateOrthographicOffCenter (
            Fixed32 left, 
            Fixed32 right, 
            Fixed32 bottom, 
            Fixed32 top, 
            Fixed32 zNearPlane, 
            Fixed32 zFarPlane, 
            out Matrix44 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
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
        
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx
        /// </summary>
        public static void CreateLookAt (
            ref Vector3 cameraPosition, 
            ref Vector3 cameraTarget, 
            ref Vector3 cameraUpVector, 
            out Matrix44 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            Vector3 forward = cameraPosition - cameraTarget;
            Vector3.Normalise (ref forward, out forward);

            Vector3 right;
            Vector3.Cross (ref cameraUpVector, ref forward, out right); 
            Vector3.Normalise (ref right, out right);
            
            Vector3 up;
            Vector3.Cross (ref forward, ref right, out up);
            Vector3.Normalise (ref up, out up);
            
            result.M11 = right.X;
            result.M12 = up.X;
            result.M13 = forward.X;
            result.M14 = zero;

            result.M21 = right.Y;
            result.M22 = up.Y;
            result.M23 = forward.Y;
            result.M24 = zero;

            result.M31 = right.Z;
            result.M32 = up.Z;
            result.M33 = forward.Z;
            result.M34 = zero;

            Fixed32 a;
            Fixed32 b;
            Fixed32 c;

            Vector3.Dot (ref right, ref cameraPosition, out a);
            Vector3.Dot (ref up, ref cameraPosition, out b);
            Vector3.Dot (ref forward, ref cameraPosition, out c);
            
            result.M41 = -a;
            result.M42 = -b;
            result.M43 = -c;

            result.M44 = one;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void Transpose (ref Matrix44 input, out Matrix44 output)
        {
            output.M11 = input.M11;
            output.M22 = input.M22;
            output.M33 = input.M33;
            output.M44 = input.M44;

            Fixed32 temp = input.M12;
            output.M12 = input.M21;
            output.M21 = temp;

            temp = input.M13;
            output.M13 = input.M31;
            output.M31 = temp;

            temp = input.M14;
            output.M14 = input.M41;
            output.M41 = temp;

            temp = input.M23;
            output.M23 = input.M32;
            output.M32 = temp;

            temp = input.M24;
            output.M24 = input.M42;
            output.M42 = temp;

            temp =  input.M34;
            output.M34 = input.M43;
            output.M43 = temp;
        }

        /// <summary>
        /// Essential Mathemathics For Games & Interactive Applications
        /// </summary>
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

            Vector3.Normalise(ref a, out a);
            Vector3.Normalise(ref b, out b);
            Vector3.Normalise(ref c, out c);

            Vector3 right = new Vector3(a.X, b.X, c.X);
            Vector3 up = new Vector3(a.Y, b.Y, c.Y);
            Vector3 backward = new Vector3(a.Z, b.Z, c.Z);

            Vector3.Normalise(ref right, out right);
            Vector3.Normalise(ref up, out up);
            Vector3.Normalise(ref backward, out backward);

            Matrix44 rotMat;
            Matrix44.CreateFromAllAxis(ref right, ref up, ref backward, out rotMat);

            Quaternion.CreateFromRotationMatrix(ref rotMat, out rotation);

            return true;
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// todo
        /// </summary>
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
        
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// todo
        /// </summary>
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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Transforms a Matrix by applying a Quaternion rotation.
        /// </summary>
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

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Matrix44 object is equal to another
        /// object
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;

            if (obj is Matrix44)
            {
                flag = this.Equals ((Matrix44) obj);
            }
            
            return flag;
        }

        #region IEquatable<Matrix44>

        /// <summary>
        /// Determines whether or not this Matrix44 object is equal to another
        /// Matrix44 object.
        /// </summary>
        public Boolean Equals (Matrix44 other)
        {
            return 
                (this.M11 == other.M11) && 
                (this.M22 == other.M22) && 
                (this.M33 == other.M33) && 
                (this.M44 == other.M44) && 
                (this.M12 == other.M12) && 
                (this.M13 == other.M13) && 
                (this.M14 == other.M14) && 
                (this.M21 == other.M21) && 
                (this.M23 == other.M23) && 
                (this.M24 == other.M24) && 
                (this.M31 == other.M31) && 
                (this.M32 == other.M32) && 
                (this.M34 == other.M34) && 
                (this.M41 == other.M41) && 
                (this.M42 == other.M42) && 
                (this.M43 == other.M43);
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Matrix44 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Matrix44 matrix1, Matrix44 matrix2)
        {
            return ((((((matrix1.M11 == matrix2.M11) && (matrix1.M22 == matrix2.M22)) && ((matrix1.M33 == matrix2.M33) && (matrix1.M44 == matrix2.M44))) && (((matrix1.M12 == matrix2.M12) && (matrix1.M13 == matrix2.M13)) && ((matrix1.M14 == matrix2.M14) && (matrix1.M21 == matrix2.M21)))) && ((((matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24)) && ((matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32))) && (((matrix1.M34 == matrix2.M34) && (matrix1.M41 == matrix2.M41)) && (matrix1.M42 == matrix2.M42)))) && (matrix1.M43 == matrix2.M43));
        }
        
        /// <summary>
        /// Determines whether or not two Matrix44 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Matrix44 matrix1, Matrix44 matrix2)
        {
            if ((matrix1.M11 == matrix2.M11) && 
                (matrix1.M12 == matrix2.M12) && 
                (matrix1.M13 == matrix2.M13) && 
                (matrix1.M14 == matrix2.M14) && 
                (matrix1.M21 == matrix2.M21) && 
                (matrix1.M22 == matrix2.M22) && 
                (matrix1.M23 == matrix2.M23) && 
                (matrix1.M24 == matrix2.M24) && 
                (matrix1.M31 == matrix2.M31) && 
                (matrix1.M32 == matrix2.M32) && 
                (matrix1.M33 == matrix2.M33) && 
                (matrix1.M34 == matrix2.M34) && 
                (matrix1.M41 == matrix2.M41) && 
                (matrix1.M42 == matrix2.M42) && 
                (matrix1.M43 == matrix2.M43))
            {
                return !(matrix1.M44 == matrix2.M44);
            }

            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Matrix44 objects.
        /// </summary>
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

        /// <summary>
        /// Performs addition of two Matrix44 objects using the (X+Y) operator. 
        /// </summary>
        public static Matrix44 operator + (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
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
            return result;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Matrix44 objects.
        /// </summary>
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

        /// <summary>
        /// Performs subtraction of two Matrix44 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Matrix44 operator - (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
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
            return result;
        }

        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Matrix44 object.
        /// </summary>
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

        /// <summary>
        /// Performs negation of a Matrix44 object using the (-X) operator.
        /// </summary>
        public static Matrix44 operator - (Matrix44 matrix)
        {
            Matrix44 result;
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
            return result;
        }
        
        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Matrix44 objects.
        /// </summary>
        public static void Multiply (ref Matrix44 matrix1, ref Matrix44 matrix2, out Matrix44 result)
        {   
            result.M11 = 
                (matrix1.M11 * matrix2.M11) + 
                (matrix1.M12 * matrix2.M21) + 
                (matrix1.M13 * matrix2.M31) + 
                (matrix1.M14 * matrix2.M41);

            result.M12 =
                (matrix1.M11 * matrix2.M12) + 
                (matrix1.M12 * matrix2.M22) + 
                (matrix1.M13 * matrix2.M32) + 
                (matrix1.M14 * matrix2.M42);

            result.M13 = 
                (matrix1.M11 * matrix2.M13) + 
                (matrix1.M12 * matrix2.M23) + 
                (matrix1.M13 * matrix2.M33) + 
                (matrix1.M14 * matrix2.M43);

            result.M14 = 
                (matrix1.M11 * matrix2.M14) + 
                (matrix1.M12 * matrix2.M24) + 
                (matrix1.M13 * matrix2.M34) + 
                (matrix1.M14 * matrix2.M44);

            result.M21 = 
                (matrix1.M21 * matrix2.M11) + 
                (matrix1.M22 * matrix2.M21) + 
                (matrix1.M23 * matrix2.M31) + 
                (matrix1.M24 * matrix2.M41);

            result.M22 = 
                (matrix1.M21 * matrix2.M12) + 
                (matrix1.M22 * matrix2.M22) + 
                (matrix1.M23 * matrix2.M32) + 
                (matrix1.M24 * matrix2.M42);

            result.M23 = 
                (matrix1.M21 * matrix2.M13) + 
                (matrix1.M22 * matrix2.M23) + 
                (matrix1.M23 * matrix2.M33) + 
                (matrix1.M24 * matrix2.M43);

            result.M24 = 
                (matrix1.M21 * matrix2.M14) + 
                (matrix1.M22 * matrix2.M24) + 
                (matrix1.M23 * matrix2.M34) + 
                (matrix1.M24 * matrix2.M44);

            result.M31 = 
                (matrix1.M31 * matrix2.M11) + 
                (matrix1.M32 * matrix2.M21) + 
                (matrix1.M33 * matrix2.M31) + 
                (matrix1.M34 * matrix2.M41);

            result.M32 = 
                (matrix1.M31 * matrix2.M12) + 
                (matrix1.M32 * matrix2.M22) + 
                (matrix1.M33 * matrix2.M32) + 
                (matrix1.M34 * matrix2.M42);

            result.M33 = 
                (matrix1.M31 * matrix2.M13) + 
                (matrix1.M32 * matrix2.M23) + 
                (matrix1.M33 * matrix2.M33) + 
                (matrix1.M34 * matrix2.M43);

            result.M34 = 
                (matrix1.M31 * matrix2.M14) + 
                (matrix1.M32 * matrix2.M24) + 
                (matrix1.M33 * matrix2.M34) + 
                (matrix1.M34 * matrix2.M44);

            result.M41 = 
                (matrix1.M41 * matrix2.M11) + 
                (matrix1.M42 * matrix2.M21) + 
                (matrix1.M43 * matrix2.M31) + 
                (matrix1.M44 * matrix2.M41);

            result.M42 = 
                (matrix1.M41 * matrix2.M12) + 
                (matrix1.M42 * matrix2.M22) + 
                (matrix1.M43 * matrix2.M32) + 
                (matrix1.M44 * matrix2.M42);

            result.M43 = 
                (matrix1.M41 * matrix2.M13) + 
                (matrix1.M42 * matrix2.M23) + 
                (matrix1.M43 * matrix2.M33) + 
                (matrix1.M44 * matrix2.M43);

            result.M44 = 
                (matrix1.M41 * matrix2.M14) + 
                (matrix1.M42 * matrix2.M24) + 
                (matrix1.M43 * matrix2.M34) + 
                (matrix1.M44 * matrix2.M44);
        }

        /// <summary>
        /// Performs multiplication of a Matrix44 object and a Fixed32
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Matrix44 matrix1, Fixed32 scaleFactor, out Matrix44 result)
        {
            result.M11 = matrix1.M11 * scaleFactor;
            result.M12 = matrix1.M12 * scaleFactor;
            result.M13 = matrix1.M13 * scaleFactor;
            result.M14 = matrix1.M14 * scaleFactor;
            result.M21 = matrix1.M21 * scaleFactor;
            result.M22 = matrix1.M22 * scaleFactor;
            result.M23 = matrix1.M23 * scaleFactor;
            result.M24 = matrix1.M24 * scaleFactor;
            result.M31 = matrix1.M31 * scaleFactor;
            result.M32 = matrix1.M32 * scaleFactor;
            result.M33 = matrix1.M33 * scaleFactor;
            result.M34 = matrix1.M34 * scaleFactor;
            result.M41 = matrix1.M41 * scaleFactor;
            result.M42 = matrix1.M42 * scaleFactor;
            result.M43 = matrix1.M43 * scaleFactor;
            result.M44 = matrix1.M44 * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Matrix44 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Matrix44 operator * (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            
            result.M11 = 
                (matrix1.M11 * matrix2.M11) + 
                (matrix1.M12 * matrix2.M21) + 
                (matrix1.M13 * matrix2.M31) + 
                (matrix1.M14 * matrix2.M41);

            result.M12 =
                (matrix1.M11 * matrix2.M12) + 
                (matrix1.M12 * matrix2.M22) + 
                (matrix1.M13 * matrix2.M32) + 
                (matrix1.M14 * matrix2.M42);

            result.M13 = 
                (matrix1.M11 * matrix2.M13) + 
                (matrix1.M12 * matrix2.M23) + 
                (matrix1.M13 * matrix2.M33) + 
                (matrix1.M14 * matrix2.M43);

            result.M14 = 
                (matrix1.M11 * matrix2.M14) + 
                (matrix1.M12 * matrix2.M24) + 
                (matrix1.M13 * matrix2.M34) + 
                (matrix1.M14 * matrix2.M44);

            result.M21 = 
                (matrix1.M21 * matrix2.M11) + 
                (matrix1.M22 * matrix2.M21) + 
                (matrix1.M23 * matrix2.M31) + 
                (matrix1.M24 * matrix2.M41);

            result.M22 = 
                (matrix1.M21 * matrix2.M12) + 
                (matrix1.M22 * matrix2.M22) + 
                (matrix1.M23 * matrix2.M32) + 
                (matrix1.M24 * matrix2.M42);

            result.M23 = 
                (matrix1.M21 * matrix2.M13) + 
                (matrix1.M22 * matrix2.M23) + 
                (matrix1.M23 * matrix2.M33) + 
                (matrix1.M24 * matrix2.M43);

            result.M24 = 
                (matrix1.M21 * matrix2.M14) + 
                (matrix1.M22 * matrix2.M24) + 
                (matrix1.M23 * matrix2.M34) + 
                (matrix1.M24 * matrix2.M44);

            result.M31 = 
                (matrix1.M31 * matrix2.M11) + 
                (matrix1.M32 * matrix2.M21) + 
                (matrix1.M33 * matrix2.M31) + 
                (matrix1.M34 * matrix2.M41);

            result.M32 = 
                (matrix1.M31 * matrix2.M12) + 
                (matrix1.M32 * matrix2.M22) + 
                (matrix1.M33 * matrix2.M32) + 
                (matrix1.M34 * matrix2.M42);

            result.M33 = 
                (matrix1.M31 * matrix2.M13) + 
                (matrix1.M32 * matrix2.M23) + 
                (matrix1.M33 * matrix2.M33) + 
                (matrix1.M34 * matrix2.M43);

            result.M34 = 
                (matrix1.M31 * matrix2.M14) + 
                (matrix1.M32 * matrix2.M24) + 
                (matrix1.M33 * matrix2.M34) + 
                (matrix1.M34 * matrix2.M44);

            result.M41 = 
                (matrix1.M41 * matrix2.M11) + 
                (matrix1.M42 * matrix2.M21) + 
                (matrix1.M43 * matrix2.M31) + 
                (matrix1.M44 * matrix2.M41);

            result.M42 = 
                (matrix1.M41 * matrix2.M12) + 
                (matrix1.M42 * matrix2.M22) + 
                (matrix1.M43 * matrix2.M32) + 
                (matrix1.M44 * matrix2.M42);

            result.M43 = 
                (matrix1.M41 * matrix2.M13) + 
                (matrix1.M42 * matrix2.M23) + 
                (matrix1.M43 * matrix2.M33) + 
                (matrix1.M44 * matrix2.M43);

            result.M44 = 
                (matrix1.M41 * matrix2.M14) + 
                (matrix1.M42 * matrix2.M24) + 
                (matrix1.M43 * matrix2.M34) + 
                (matrix1.M44 * matrix2.M44);

            return result;
        }
        
        /// <summary>
        /// Performs multiplication of a Matrix44 object and a Fixed32
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Matrix44 operator * (Matrix44 matrix1, Fixed32 scaleFactor)
        {
            Matrix44 result;
            result.M11 = matrix1.M11 * scaleFactor;
            result.M12 = matrix1.M12 * scaleFactor;
            result.M13 = matrix1.M13 * scaleFactor;
            result.M14 = matrix1.M14 * scaleFactor;
            result.M21 = matrix1.M21 * scaleFactor;
            result.M22 = matrix1.M22 * scaleFactor;
            result.M23 = matrix1.M23 * scaleFactor;
            result.M24 = matrix1.M24 * scaleFactor;
            result.M31 = matrix1.M31 * scaleFactor;
            result.M32 = matrix1.M32 * scaleFactor;
            result.M33 = matrix1.M33 * scaleFactor;
            result.M34 = matrix1.M34 * scaleFactor;
            result.M41 = matrix1.M41 * scaleFactor;
            result.M42 = matrix1.M42 * scaleFactor;
            result.M43 = matrix1.M43 * scaleFactor;
            result.M44 = matrix1.M44 * scaleFactor;
            return result;
        }
        
        /// <summary>
        /// Performs multiplication of a Fixed32 precision scaling factor 
        /// and aMatrix44 object using the (x*Y) operator.
        /// </summary>
        public static Matrix44 operator * (Fixed32 scaleFactor, Matrix44 matrix1)
        {
            Matrix44 result;
            result.M11 = matrix1.M11 * scaleFactor;
            result.M12 = matrix1.M12 * scaleFactor;
            result.M13 = matrix1.M13 * scaleFactor;
            result.M14 = matrix1.M14 * scaleFactor;
            result.M21 = matrix1.M21 * scaleFactor;
            result.M22 = matrix1.M22 * scaleFactor;
            result.M23 = matrix1.M23 * scaleFactor;
            result.M24 = matrix1.M24 * scaleFactor;
            result.M31 = matrix1.M31 * scaleFactor;
            result.M32 = matrix1.M32 * scaleFactor;
            result.M33 = matrix1.M33 * scaleFactor;
            result.M34 = matrix1.M34 * scaleFactor;
            result.M41 = matrix1.M41 * scaleFactor;
            result.M42 = matrix1.M42 * scaleFactor;
            result.M43 = matrix1.M43 * scaleFactor;
            result.M44 = matrix1.M44 * scaleFactor;
            return result;
        }
        
        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Matrix44 objects.
        /// </summary>
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

        /// <summary>
        /// Performs division of a Matrix44 object and a Fixed32 precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Matrix44 matrix1, Fixed32 divider, out Matrix44 result)
        {
            result.M11 = matrix1.M11 / divider;
            result.M12 = matrix1.M12 / divider;
            result.M13 = matrix1.M13 / divider;
            result.M14 = matrix1.M14 / divider;
            result.M21 = matrix1.M21 / divider;
            result.M22 = matrix1.M22 / divider;
            result.M23 = matrix1.M23 / divider;
            result.M24 = matrix1.M24 / divider;
            result.M31 = matrix1.M31 / divider;
            result.M32 = matrix1.M32 / divider;
            result.M33 = matrix1.M33 / divider;
            result.M34 = matrix1.M34 / divider;
            result.M41 = matrix1.M41 / divider;
            result.M42 = matrix1.M42 / divider;
            result.M43 = matrix1.M43 / divider;
            result.M44 = matrix1.M44 / divider;
        }

        /// <summary>
        /// Performs division of two Matrix44 objects using the (X/Y) operator.
        /// </summary>
        public static Matrix44 operator / (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;

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

            return result;
        }
        
        /// <summary>
        /// Performs division of a Matrix44 object and a Fixed32 precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Matrix44 operator / (Matrix44 matrix1, Fixed32 divider)
        {
            Matrix44 result;

            result.M11 = matrix1.M11 / divider;
            result.M12 = matrix1.M12 / divider;
            result.M13 = matrix1.M13 / divider;
            result.M14 = matrix1.M14 / divider;
            result.M21 = matrix1.M21 / divider;
            result.M22 = matrix1.M22 / divider;
            result.M23 = matrix1.M23 / divider;
            result.M24 = matrix1.M24 / divider;
            result.M31 = matrix1.M31 / divider;
            result.M32 = matrix1.M32 / divider;
            result.M33 = matrix1.M33 / divider;
            result.M34 = matrix1.M34 / divider;
            result.M41 = matrix1.M41 / divider;
            result.M42 = matrix1.M42 / divider;
            result.M43 = matrix1.M43 / divider;
            result.M44 = matrix1.M44 / divider;

            return result;
        }

        /// <summary>
        /// beware, doing this might not produce what you expect.  you likely
        /// want to lerp between quaternions.
        /// </summary>
        public static void Lerp (ref Matrix44 matrix1, ref Matrix44 matrix2, Fixed32 amount, out Matrix44 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

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


    }

    /// <summary>
    /// Fixed32 precision Quaternion.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Quaternion 
        : IEquatable<Quaternion>
    {
        /// <summary>
        /// todo
        /// </summary>
        public Fixed32 X;

        /// <summary>
        /// todo
        /// </summary>
        public Fixed32 Y;

        /// <summary>
        /// todo
        /// </summary>
        public Fixed32 Z;

        /// <summary>
        /// todo
        /// </summary>
        public Fixed32 W;

        /// <summary>
        /// todo
        /// </summary>
        public Quaternion (Fixed32 x, Fixed32 y, Fixed32 z, Fixed32 w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// todo
        /// </summary>
        public Quaternion (Vector3 vectorPart, Fixed32 scalarPart)
        {
            this.X = vectorPart.X;
            this.Y = vectorPart.Y;
            this.Z = vectorPart.Z;
            this.W = scalarPart;
        }

        /// <summary>
        /// todo
        /// </summary>
        public override String ToString ()
        {
            return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}", new Object[] { this.X.ToString (), this.Y.ToString (), this.Z.ToString (), this.W.ToString () });
        }

        /// <summary>
        /// todo
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (((this.X.GetHashCode () + this.Y.GetHashCode ()) + this.Z.GetHashCode ()) + this.W.GetHashCode ());
        }

        /// <summary>
        /// todo
        /// </summary>
        public Fixed32 LengthSquared ()
        {
            return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
        }

        /// <summary>
        /// todo
        /// </summary>
        public Fixed32 Length ()
        {
            Fixed32 num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
            return RealMaths.Sqrt (num);
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
        public Boolean IsUnit()
        {
            Fixed32 one = 1;

            return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
        }

        /// <summary>
        /// todo
        /// </summary>
        public void Conjugate ()
        {
            this.X = -this.X;
            this.Y = -this.Y;
            this.Z = -this.Z;
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines the identity quaternion.
        /// </summary>
        static Quaternion identity;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Quaternion ()
        {
            identity = new Quaternion (0, 0, 0, 1);
        }

        /// <summary>
        /// Returns the identity Quaternion.
        /// </summary>
        public static Quaternion Identity
        {
            get { return identity; }
        }

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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

        /// <summary>
        /// todo
        /// </summary>
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
        /// <summary>
        /// todo
        /// </summary>
        public static void Conjugate (ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = value.W;
        }
        
        /// <summary>
        /// todo
        /// </summary>
        public static void Inverse (ref Quaternion quaternion, out Quaternion result)
        {
            Fixed32 one = 1;
            Fixed32 a =
                (quaternion.X * quaternion.X) + 
                (quaternion.Y * quaternion.Y) + 
                (quaternion.Z * quaternion.Z) + 
                (quaternion.W * quaternion.W);

            Fixed32 b = one / a;

            result.X = -quaternion.X * b;
            result.Y = -quaternion.Y * b;
            result.Z = -quaternion.Z * b;
            result.W =  quaternion.W * b;
        }
        
        /// <summary>
        /// todo
        /// </summary>
        public static void Dot (ref Quaternion quaternion1, ref Quaternion quaternion2, out Fixed32 result)
        {
            result = 
                (quaternion1.X * quaternion2.X) + 
                (quaternion1.Y * quaternion2.Y) + 
                (quaternion1.Z * quaternion2.Z) + 
                (quaternion1.W * quaternion2.W);
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void Concatenate (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            Fixed32 x = value2.X;
            Fixed32 y = value2.Y;
            Fixed32 z = value2.Z;
            Fixed32 w = value2.W;

            Fixed32 a = value1.X;
            Fixed32 b = value1.Y;
            Fixed32 c = value1.Z;
            Fixed32 d = value1.W;

            Fixed32 e = (y * c) - (z * b);
            Fixed32 f = (z * a) - (x * c);
            Fixed32 g = (x * b) - (y * a);
            Fixed32 h = ((x * a) + (y * b)) + (z * c);

            result.X = (x * d) + (a * w) + e;
            result.Y = (y * d) + (b * w) + f;
            result.Z = (z * d) + (c * w) + g;
            result.W = (w * d) - h;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void Normalise (ref Quaternion quaternion, out Quaternion result)
        {
            Fixed32 one = 1;

            Fixed32 a = 
                (quaternion.X * quaternion.X) + 
                (quaternion.Y * quaternion.Y) + 
                (quaternion.Z * quaternion.Z) + 
                (quaternion.W * quaternion.W);

            Fixed32 b = one / RealMaths.Sqrt (a);

            result.X = quaternion.X * b;
            result.Y = quaternion.Y * b;
            result.Z = quaternion.Z * b;
            result.W = quaternion.W * b;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Quaternion object is equal to another
        /// object
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            
            if (obj is Quaternion)
            {
                flag = this.Equals ((Quaternion) obj);
            }

            return flag;
        }

        #region IEquatable<Quaternion>

        /// <summary>
        /// Determines whether or not this Quaternion object is equal to another
        /// Quaternion object.
        /// </summary>
        public Boolean Equals (Quaternion other)
        {
            return 
                (this.X == other.X) && 
                (this.Y == other.Y) && 
                (this.Z == other.Z) && 
                (this.W == other.W);
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Quaternion objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Quaternion value1, Quaternion value2)
        {
            return ((((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) && (value1.W == value2.W));
        }
        
        /// <summary>
        /// Determines whether or not two Quaternion objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Quaternion value1, Quaternion value2)
        {
            if (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) {
                return !(value1.W == value2.W);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Quaternion objects.
        /// </summary>
        public static void Add (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
            result.W = value1.W + value2.W;
        }

        /// <summary>
        /// Performs addition of two Quaternion objects using the (X+Y) operator. 
        /// </summary>
        public static Quaternion operator + (Quaternion value1, Quaternion value2)
        {
            Quaternion quat;
            quat.X = value1.X + value2.X;
            quat.Y = value1.Y + value2.Y;
            quat.Z = value1.Z + value2.Z;
            quat.W = value1.W + value2.W;
            return quat;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Quaternion objects.
        /// </summary>
        public static void Subtract (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
            result.W = value1.W - value2.W;
        }

        /// <summary>
        /// Performs subtraction of two Quaternion objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Quaternion operator - (Quaternion value1, Quaternion value2)
        {
            Quaternion quat;
            quat.X = value1.X - value2.X;
            quat.Y = value1.Y - value2.Y;
            quat.Z = value1.Z - value2.Z;
            quat.W = value1.W - value2.W;
            return quat;
        }

        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Quaternion object.
        /// </summary>
        public static void Negate (ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        /// <summary>
        /// Performs negation of a Quaternion object using the (-X) operator.
        /// </summary>
        public static Quaternion operator - (Quaternion value)
        {
            Quaternion quat;
            quat.X = -value.X;
            quat.Y = -value.Y;
            quat.Z = -value.Z;
            quat.W = -value.W;
            return quat;
        }
        
        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Quaternion objects.
        /// </summary>
        public static void Multiply (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            Fixed32 x1 = value1.X;
            Fixed32 y1 = value1.Y;
            Fixed32 z1 = value1.Z;
            Fixed32 w1 = value1.W;

            Fixed32 x2 = value2.X;
            Fixed32 y2 = value2.Y;
            Fixed32 z2 = value2.Z;
            Fixed32 w2 = value2.W;

            Fixed32 a = (y1 * z2) - (z1 * y2);
            Fixed32 b = (z1 * x2) - (x1 * z2);
            Fixed32 c = (x1 * y2) - (y1 * x2);
            Fixed32 d = ((x1 * x2) + (y1 * y2)) + (z1 * z2);

            result.X = ((x1 * w2) + (x2 * w1)) + a;
            result.Y = ((y1 * w2) + (y2 * w1)) + b;
            result.Z = ((z1 * w2) + (z2 * w1)) + c;
            result.W = (w1 * w2) - d;
        }

        /// <summary>
        /// Performs multiplication of a Quaternion object and a Fixed32
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Quaternion value1, Fixed32 scaleFactor, out Quaternion result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
            result.W = value1.W * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Quaternion objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Quaternion operator * (Quaternion value1, Quaternion value2)
        {
            Quaternion quaternion;
            
            Fixed32 x1 = value1.X;
            Fixed32 y1 = value1.Y;
            Fixed32 z1 = value1.Z;
            Fixed32 w1 = value1.W;

            Fixed32 x2 = value2.X;
            Fixed32 y2 = value2.Y;
            Fixed32 z2 = value2.Z;
            Fixed32 w2 = value2.W;

            Fixed32 a = (y1 * z2) - (z1 * y2);
            Fixed32 b = (z1 * x2) - (x1 * z2);
            Fixed32 c = (x1 * y2) - (y1 * x2);
            Fixed32 d = ((x1 * x2) + (y1 * y2)) + (z1 * z2);

            quaternion.X = ((x1 * w2) + (x2 * w1)) + a;
            quaternion.Y = ((y1 * w2) + (y2 * w1)) + b;
            quaternion.Z = ((z1 * w2) + (z2 * w1)) + c;
            quaternion.W = (w1 * w2) - d;

            return quaternion;
        }
        
        /// <summary>
        /// Performs multiplication of a Quaternion object and a Fixed32
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Quaternion operator * (Quaternion value1, Fixed32 scaleFactor)
        {
            Quaternion quat;
            quat.X = value1.X * scaleFactor;
            quat.Y = value1.Y * scaleFactor;
            quat.Z = value1.Z * scaleFactor;
            quat.W = value1.W * scaleFactor;
            return quat;
        }
        
        /// <summary>
        /// Performs multiplication of a Fixed32 precision scaling factor 
        /// and aQuaternion object using the (x*Y) operator.
        /// </summary>
        public static Quaternion operator * (Fixed32 scaleFactor, Quaternion value1)
        {
            Quaternion quat;
            quat.X = value1.X * scaleFactor;
            quat.Y = value1.Y * scaleFactor;
            quat.Z = value1.Z * scaleFactor;
            quat.W = value1.W * scaleFactor;
            return quat;
        }
        
        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Quaternion objects.
        /// </summary>
        public static void Divide (ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            Fixed32 one = 1;

            Fixed32 x = value1.X;
            Fixed32 y = value1.Y;
            Fixed32 z = value1.Z;
            Fixed32 w = value1.W;

            Fixed32 a = 
                (value2.X * value2.X) + 
                (value2.Y * value2.Y) +
                (value2.Z * value2.Z) + 
                (value2.W * value2.W);

            Fixed32 b = one / a;

            Fixed32 c = -value2.X * b;
            Fixed32 d = -value2.Y * b;
            Fixed32 e = -value2.Z * b;
            Fixed32 f = value2.W * b;

            Fixed32 g = (y * e) - (z * d);
            Fixed32 h = (z * c) - (x * e);
            Fixed32 i = (x * d) - (y * c);
            Fixed32 j = ((x * c) + (y * d)) + (z * e);

            result.X = ((x * f) + (c * w)) + g;
            result.Y = ((y * f) + (d * w)) + h;
            result.Z = ((z * f) + (e * w)) + i;
            result.W = (w * f) - j;
        }

        /// <summary>
        /// Performs division of a Quaternion object and a Fixed32 precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Quaternion value1, Fixed32 divider, out Quaternion result)
        {
            Fixed32 one = 1;
            Fixed32 a = one / divider;

            result.X = value1.X * a;
            result.Y = value1.Y * a;
            result.Z = value1.Z * a;
            result.W = value1.W * a;
        }

        /// <summary>
        /// Performs division of two Quaternion objects using the (X/Y) operator.
        /// </summary>
        public static Quaternion operator / (Quaternion value1, Quaternion value2)
        {
            Fixed32 one = 1;

            Quaternion quaternion;

            Fixed32 x = value1.X;
            Fixed32 y = value1.Y;
            Fixed32 z = value1.Z;
            Fixed32 w = value1.W;

            Fixed32 a = 
                (value2.X * value2.X) + 
                (value2.Y * value2.Y) +
                (value2.Z * value2.Z) + 
                (value2.W * value2.W);

            Fixed32 b = one / a;

            Fixed32 c = -value2.X * b;
            Fixed32 d = -value2.Y * b;
            Fixed32 e = -value2.Z * b;
            Fixed32 f =  value2.W * b;

            Fixed32 g = (y * e) - (z * d);
            Fixed32 h = (z * c) - (x * e);
            Fixed32 i = (x * d) - (y * c);
            Fixed32 j = (x * c) + (y * d) + (z * e);

            quaternion.X = (x * f) + (c * w) + g;
            quaternion.Y = (y * f) + (d * w) + h;
            quaternion.Z = (z * f) + (e * w) + i;
            quaternion.W = (w * f) - j;

            return quaternion;
        }
        
        /// <summary>
        /// Performs division of a Quaternion object and a Fixed32 precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Quaternion operator / (Quaternion value1, Fixed32 divider)
        {
            Fixed32 one = 1;

            Quaternion quat;

            Fixed32 num = one / divider;

            quat.X = value1.X * num;
            quat.Y = value1.Y * num;
            quat.Z = value1.Z * num;
            quat.W = value1.W * num;

            return quat;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void Slerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Fixed32 amount, out Quaternion result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

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

        /// <summary>
        /// todo
        /// </summary>
        public static void Lerp (ref Quaternion quaternion1, ref Quaternion quaternion2, Fixed32 amount, out Quaternion result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

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

    }    /// <summary>
    /// Fixed32 precision Vector2.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector2
        : IEquatable<Vector2>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector2.
        /// </summary>
        public Fixed32 X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector2.
        /// </summary>
        public Fixed32 Y;

        /// <summary>
        /// Initilises a new instance of Vector2 from two Fixed32 values
        /// representing X and Y respectively.
        /// </summary>
        public Vector2 (Fixed32 x, Fixed32 y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Calculates the length of the Vector2.
        /// </summary>
        public Fixed32 Length ()
        {
            Fixed32 num = (this.X * this.X) + (this.Y * this.Y);
            return RealMaths.Sqrt (num);
        }

        /// <summary>
        /// Calculates the length of the Vector2 squared.
        /// </summary>
        public Fixed32 LengthSquared ()
        {
            return ((this.X * this.X) + (this.Y * this.Y));
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format (
                "{{X:{0} Y:{1}}}",
                new Object[]
                {
                    this.X.ToString (),
                    this.Y.ToString ()
                }
                );
        }

        /// <summary>
        /// Gets the hash code of the Vector2 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (this.X.GetHashCode () + this.Y.GetHashCode ());
        }

        /// <summary>
        /// Detemines whether or not the Vector2 is of unit length.
        /// </summary>
        public Boolean IsUnit()
        {
            Fixed32 one = 1;
            return RealMaths.IsZero(one - X*X - Y*Y);
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Vector2 with all of its components set to zero.
        /// </summary>
        readonly static Vector2 zero;

        /// <summary>
        /// Defines a Vector2 with all of its components set to one.
        /// </summary>
        readonly static Vector2 one;

        /// <summary>
        /// Defines the unit Vector2 for the X-axis.
        /// </summary>
        readonly static Vector2 unitX;

        /// <summary>
        /// Defines the unit Vector2 for the Y-axis.
        /// </summary>
        readonly static Vector2 unitY;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Vector2 ()
        {
            zero =      new Vector2 ();
            one =       new Vector2 (1, 1);

            unitX =     new Vector2 (1, 0);
            unitY =     new Vector2 (0, 1);
        }

        /// <summary>
        /// Returns a Vector2 with all of its components set to zero.
        /// </summary>
        public static Vector2 Zero
        {
            get { return zero; }
        }
        
        /// <summary>
        /// Returns a Vector2 with all of its components set to one.
        /// </summary>
        public static Vector2 One
        {
            get { return one; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the X-axis.
        /// </summary>
        public static Vector2 UnitX
        {
            get { return unitX; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the Y-axis.
        /// </summary>
        public static Vector2 UnitY
        {
            get { return unitY; }
        }

        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static void Distance (
            ref Vector2 vector1, ref Vector2 vector2, out Fixed32 result)
        {
            Fixed32 dx = vector1.X - vector2.X;
            Fixed32 dy = vector1.Y - vector2.Y;

            Fixed32 lengthSquared = (dx * dx) + (dy * dy);

            result = RealMaths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector2 vector1, ref Vector2 vector2, out Fixed32 result)
        {
            Fixed32 dx = vector1.X - vector2.X;
            Fixed32 dy = vector1.Y - vector2.Y;

            result = (dx * dx) + (dy * dy);
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static void Dot (
            ref Vector2 vector1, ref Vector2 vector2, out Fixed32 result)
        {
            result = (vector1.X * vector2.X) + (vector1.Y * vector2.Y);
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static void Normalise (ref Vector2 vector, out Vector2 result)
        {
            Fixed32 lengthSquared =
                (vector.X * vector.X) + (vector.Y * vector.Y);

            Fixed32 epsilon; RealMaths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Fixed32.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 one = 1;
            Fixed32 multiplier = one / RealMaths.Sqrt (lengthSquared);

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;

        }

        /// <summary>
        /// Returns the vector of an incident vector reflected across the a
        /// specified normal vector.
        /// </summary>
        public static void Reflect (
            ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            // dot = vector . normal
            //     = |vector| * [normal] * cosθ
            //     = |vector| * cosθ
            //     = adjacent
            Fixed32 dot;
            Dot(ref vector, ref normal, out dot);

            Fixed32 two = 2;
            Fixed32 twoDot = dot * two;

            // Starting vector minus twice the length of the adjcent projected
            // along the normal.
            result = vector - (twoDot * normal);
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector2 vector, ref Matrix44 matrix, out Vector2 result)
        {
            Fixed32 x =
                (vector.X * matrix.M11) +
                (vector.Y * matrix.M21) +
                matrix.M41;

            Fixed32 y =
                (vector.X * matrix.M12) +
                (vector.Y * matrix.M22) +
                matrix.M42;

            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector2 vector, ref Quaternion rotation, out Vector2 result)
        {
            Fixed32 two = 2;

            Fixed32 x = rotation.X;
            Fixed32 y = rotation.Y;
            Fixed32 z = rotation.Z;
            Fixed32 w = rotation.W;

            Fixed32 xx = x * x;
            Fixed32 yy = y * y;
            Fixed32 zz = z * z;

            Fixed32 wz = w * z;
            Fixed32 xy = x * y;

            result.X =
                + vector.X
                - (two * vector.X  * (yy + zz))
                + (two * vector.Y  * (xy - wz));

            result.Y =
                + vector.Y
                + (two * vector.X * (xy + wz))
                - (two * vector.Y * (xx + zz));
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix.
        /// </summary>
        public static void TransformNormal (
            ref Vector2 normal, ref Matrix44 matrix, out Vector2 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Fixed32 x = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
            Fixed32 y = (normal.X * matrix.M12) + (normal.Y * matrix.M22);

            result.X = x;
            result.Y = y;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Vector2 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Vector2) {
                flag = this.Equals ((Vector2)obj);
            }
            return flag;
        }

        #region IEquatable<Vector2>

        /// <summary>
        /// Determines whether or not this Vector2 object is equal to another
        /// Vector2 object.
        /// </summary>
        public Boolean Equals (Vector2 other)
        {
            return ((this.X == other.X) && (this.Y == other.Y));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Vector2 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Vector2 value1, Vector2 value2)
        {
            return ((value1.X == value2.X) && (value1.Y == value2.Y));
        }

        /// <summary>
        /// Determines whether or not two Vector2 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Vector2 value1, Vector2 value2)
        {
            if (value1.X == value2.X) {
                return !(value1.Y == value2.Y);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector2 objects.
        /// </summary>
        public static void Add (
            ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        /// <summary>
        /// Performs addition of two Vector2 objects using the (X+Y) operator. 
        /// </summary>
        public static Vector2 operator + (Vector2 value1, Vector2 value2)
        {
            Vector2 vector;
            vector.X = value1.X + value2.X;
            vector.Y = value1.Y + value2.Y;
            return vector;
        }


        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector2 objects.
        /// </summary>
        public static void Subtract (
            ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }

        /// <summary>
        /// Performs subtraction of two Vector2 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Vector2 operator - (Vector2 value1, Vector2 value2)
        {
            Vector2 vector;
            vector.X = value1.X - value2.X;
            vector.Y = value1.Y - value2.Y;
            return vector;
        }


        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Vector2 object.
        /// </summary>
        public static void Negate (ref Vector2 value, out Vector2 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
        }

        /// <summary>
        /// Performs negation of a Vector2 object using the (-X) operator.
        /// </summary>
        public static Vector2 operator - (Vector2 value)
        {
            Vector2 vector;
            vector.X = -value.X;
            vector.Y = -value.Y;
            return vector;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector2 objects.
        /// </summary>
        public static void Multiply (
            ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
        }

        /// <summary>
        /// Performs multiplication of a Vector2 object and a Fixed32
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Vector2 value, Fixed32 scaleFactor, out Vector2 result)
        {
            result.X = value.X * scaleFactor;
            result.Y = value.Y * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Vector2 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Vector2 operator * (
            Vector2 value1, Vector2 value2)
        {
            Vector2 vector;
            vector.X = value1.X * value2.X;
            vector.Y = value1.Y * value2.Y;
            return vector;
        }

        /// <summary>
        /// Performs multiplication of a Vector2 object and a Fixed32
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Vector2 operator * (
            Vector2 value, Fixed32 scaleFactor)
        {
            Vector2 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            return vector;
        }

        /// <summary>
        /// Performs multiplication of a Fixed32 precision scaling factor 
        /// and aVector2 object using the (x*Y) operator.
        /// </summary>
        public static Vector2 operator * (
            Fixed32 scaleFactor, Vector2 value)
        {
            Vector2 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            return vector;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector2 objects.
        /// </summary>
        public static void Divide (
            ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
        }

        /// <summary>
        /// Performs division of a Vector2 object and a Fixed32 precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Vector2 value1, Fixed32 divider, out Vector2 result)
        {
            Fixed32 one = 1;
            Fixed32 num = one / divider;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
        }

        /// <summary>
        /// Performs division of two Vector2 objects using the (X/Y) operator.
        /// </summary>
        public static Vector2 operator / (Vector2 value1, Vector2 value2)
        {
            Vector2 vector;
            vector.X = value1.X / value2.X;
            vector.Y = value1.Y / value2.Y;
            return vector;
        }

        /// <summary>
        /// Performs division of a Vector2 object and a Fixed32 precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
        public static Vector2 operator / (Vector2 value1, Fixed32 divider)
        {
            Vector2 vector;
            Fixed32 one = 1;
            Fixed32 num = one / divider;
            vector.X = value1.X * num;
            vector.Y = value1.Y * num;
            return vector;
        }
        
        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector2 a,
            ref Vector2 b,
            Fixed32 amount,
            out Vector2 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 two = 2;
            Fixed32 three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            amount = (amount * amount) * (three - (two * amount));

            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// Features:
        /// - The spline passes through all of the control points.
        /// - The spline is C^1 continuous, meaning that there are no
        ///   discontinuities in the tangent direction and magnitude.
        /// - The spline is not C^2 continuous.  The second derivative is
        ///   linearly interpolated within each segment, causing the curvature
        ///   to vary linearly over the length of the segment.
        /// </summary>
        public static void CatmullRom (
            ref Vector2 a,
            ref Vector2 b,
            ref Vector2 c,
            ref Vector2 d,
            Fixed32 amount,
            out Vector2 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 half; RealMaths.Half(out half);
            Fixed32 two = 2;
            Fixed32 three = 3;
            Fixed32 four = 4;
            Fixed32 five = 5;

            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;

            result.X =
                half * ((two * b.X) + ((-a.X + c.X) * amount) +
                (((two * a.X) - (five * b.X) + (four * c.X) - d.X) * squared) +
                ((-a.X + (three * b.X) - (three * c.X) + d.X) * cubed));

            result.Y = half * ((two * b.Y) + ((-a.Y + c.Y) * amount) +
                (((two * a.Y) - (five * b.Y) + (four * c.Y) - d.Y) * squared) +
                ((-a.Y + (three * b.Y) - (three * c.Y) + d.Y) * cubed));
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector2 vector1,
            ref Vector2 tangent1,
            ref Vector2 vector2,
            ref Vector2 tangent2,
            Fixed32 amount,
            out Vector2 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 two = 2;
            Fixed32 three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            if( !tangent1.IsUnit() || !tangent2.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;

            Fixed32 a = ((two * cubed) - (three * squared)) + one;
            Fixed32 b = (-two * cubed) + (three * squared);
            Fixed32 c = (cubed - (two * squared)) + amount;
            Fixed32 d = cubed - squared;

            result.X =
                (vector1.X * a) + (vector2.X * b) +
                (tangent1.X * c) + (tangent2.X * d);

            result.Y =
                (vector1.Y * a) + (vector2.Y * b) +
                (tangent1.Y * c) + (tangent2.Y * d);
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Min (
            ref Vector2 a, 
            ref Vector2 b, 
            out Vector2 result)
        {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Max (
            ref Vector2 a, 
            ref Vector2 b, 
            out Vector2 result)
        {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static void Clamp (
            ref Vector2 a, 
            ref Vector2 min, 
            ref Vector2 max, 
            out Vector2 result)
        {
            Fixed32 x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            
            Fixed32 y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;

            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static void Lerp (
            ref Vector2 a, 
            ref Vector2 b, 
            Fixed32 amount, 
            out Vector2 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }
            
            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
        }


        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static Fixed32 Distance (
            Vector2 vector1, Vector2 vector2)
        {
            Fixed32 result;
            Distance (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static Fixed32 DistanceSquared (
            Vector2 vector1, Vector2 vector2)
        {
            Fixed32 result;
            DistanceSquared (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static Fixed32 Dot (
            Vector2 vector1, Vector2 vector2)
        {
            Fixed32 result;
            Dot (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static Vector2 Normalise (Vector2 vector)
        {
            Vector2 result;
            Normalise (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Returns the vector of an incident vector reflected across the a
        /// specified normal vector.
        /// </summary>
        public static Vector2 Reflect (
            Vector2 vector, Vector2 normal)
        {
            Vector2 result;
            Reflect (ref vector, ref normal, out result);
            return result;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix44.
        /// </summary>
        public static Vector2 Transform (
            Vector2 vector, Matrix44 matrix)
        {
            Vector2 result;
            Transform (ref vector, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Quaternion.
        /// </summary>
        public static Vector2 Transform (
            Vector2 vector, Quaternion rotation)
        {
            Vector2 result;
            Transform (ref vector, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix.
        /// </summary>
        public static Vector2 TransformNormal (
            Vector2 normal, Matrix44 matrix)
        {
            Vector2 result;
            TransformNormal (ref normal, ref matrix, out result);
            return result;
        }


        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector2 objects.
        /// </summary>
        public static Vector2 Add (
            Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Add (ref value1, ref value2, out result);
            return result;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector2 objects.
        /// </summary>
        public static Vector2 Subtract (
            Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Subtract (ref value1, ref value2, out result);
            return result;
        }

        // Negation Operators //----------------------------------------------//

        /// <summary>
        /// Performs negation of a Vector2 object.
        /// </summary>
        public static Vector2 Negate (Vector2 value)
        {
            Vector2 result;
            Negate (ref value, out result);
            return result;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector2 objects.
        /// </summary>
        public static Vector2 Multiply (
            Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Multiply (ref value1, ref value2, out result);
            return result;
        }

        /// <summary>
        /// Performs multiplication of a Vector2 object and a Fixed32
        /// precision scaling factor.
        /// </summary>
        public static Vector2 Multiply (
            Vector2 value, Fixed32 scaleFactor)
        {
            Vector2 result;
            Multiply (ref value, scaleFactor, out result);
            return result;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector2 objects.
        /// </summary>
        public static Vector2 Divide (
            Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Divide (ref value1, ref value2, out result);
            return result;
        }

        /// <summary>
        /// Performs division of a Vector2 object and a Fixed32 precision
        /// scaling factor.
        /// </summary>
        public static Vector2 Divide (
            Vector2 value1, Fixed32 divider)
        {
            Vector2 result;
            Divide (ref value1, divider, out result);
            return result;
        }

        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static Vector2 SmoothStep (
            Vector2 a,
            Vector2 b,
            Fixed32 amount)
        {
            Vector2 result;
            SmoothStep (ref a, ref b, amount, out result);
            return result;
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        public static Vector2 CatmullRom (
            Vector2 a,
            Vector2 b,
            Vector2 c,
            Vector2 d,
            Fixed32 amount)
        {
            Vector2 result;
            CatmullRom (ref a, ref b, ref c, ref d, amount, out result);
            return result;
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static Vector2 Hermite (
            Vector2 vector1,
            Vector2 tangent1,
            Vector2 vector2,
            Vector2 tangent2,
            Fixed32 amount)
        {
            Vector2 result;
            Hermite (
                ref vector1, ref tangent1,
                ref vector2, ref tangent2,
                amount, out result);
            return result;
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching
        /// pair of components.
        /// </summary>
        public static Vector2 Min (
            Vector2 a,
            Vector2 b)
        {
            Vector2 result;
            Min (ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching
        /// pair of components.
        /// </summary>
        public static Vector2 Max (
            ref Vector2 a,
            ref Vector2 b)
        {
            Vector2 result;
            Max (ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static Vector2 Clamp (
            ref Vector2 a,
            ref Vector2 min,
            ref Vector2 max)
        {
            Vector2 result;
            Clamp (ref a, ref min, ref max, out result);
            return result;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static Vector2 Lerp (
            ref Vector2 a,
            ref Vector2 b,
            Fixed32 amount)
        {
            Vector2 result;
            Lerp (ref a, ref b, amount, out result);
            return result;
        }

    }

    /// <summary>
    /// Fixed32 precision Vector3.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector3 
        : IEquatable<Vector3>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector3.
        /// </summary>
        public Fixed32 X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector3.
        /// </summary>
        public Fixed32 Y;

        /// <summary>
        /// Gets or sets the Z-component of the Vector3.
        /// </summary>
        public Fixed32 Z;

        /// <summary>
        /// Initilises a new instance of Vector3 from three Fixed32 values 
        /// representing X, Y and Z respectively.
        /// </summary>
        public Vector3 (Fixed32 x, Fixed32 y, Fixed32 z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        
        /// <summary>
        /// Initilises a new instance of Vector3 from one Vector2 value
        /// representing X and Y and one Fixed32 value representing Z.
        /// </summary>
        public Vector3 (Vector2 value, Fixed32 z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }

        /// <summary>
        /// Calculates the length of the Vector3.
        /// </summary>
        public Fixed32 Length ()
        {
            Fixed32 num = (this.X * this.X) + 
                              (this.Y * this.Y) + 
                              (this.Z * this.Z);

            return RealMaths.Sqrt (num);
        }

        /// <summary>
        /// Calculates the length of the Vector3 squared.
        /// </summary>
        public Fixed32 LengthSquared ()
        {
            return
                (this.X * this.X) + 
                (this.Y * this.Y) + 
                (this.Z * this.Z);
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format (
                "{{X:{0} Y:{1} Z:{2}}}", 
                new Object[] 
                { 
                    this.X.ToString (), 
                    this.Y.ToString (), 
                    this.Z.ToString () 
                }
                );
        }

        /// <summary>
        /// Gets the hash code of the Vector3 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (
                this.X.GetHashCode () + 
                this.Y.GetHashCode () + 
                this.Z.GetHashCode ()
                );
        }

        /// <summary>
        /// Detemines whether or not the Vector3 is of unit length.
        /// </summary>
        public Boolean IsUnit()
        {
            Fixed32 one = 1;
            return RealMaths.IsZero(one - X*X - Y*Y - Z*Z);
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Vector3 with all of its components set to zero.
        /// </summary>
        static Vector3 zero;

        /// <summary>
        /// Defines a Vector3 with all of its components set to one.
        /// </summary>
        static Vector3 one;

        /// <summary>
        /// Defines the unit Vector3 for the X-axis.
        /// </summary>
        static Vector3 unitX;

        /// <summary>
        /// Defines the unit Vector3 for the Y-axis.
        /// </summary>
        static Vector3 unitY;

        /// <summary>
        /// Defines the unit Vector3 for the Z-axis.
        /// </summary>
        static Vector3 unitZ;

        /// <summary>
        /// Defines a unit Vector3 designating up (0, 1, 0).
        /// </summary>
        static Vector3 up;

        /// <summary>
        /// Defines a unit Vector3 designating down (0, −1, 0).
        /// </summary>
        static Vector3 down;

        /// <summary>
        /// Defines a unit Vector3 pointing to the right (1, 0, 0).
        /// </summary>
        static Vector3 right;

        /// <summary>
        /// Defines a unit Vector3 designating left (−1, 0, 0).
        /// </summary>
        static Vector3 left;

        /// <summary>
        /// Defines a unit Vector3 designating forward in a right-handed 
        /// coordinate system (0, 0, -1).
        /// </summary>
        static Vector3 forward;

        /// <summary>
        /// Defines a unit Vector3 designating backward in a right-handed 
        /// coordinate system (0, 0, 1).
        /// </summary>
        static Vector3 backward;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Vector3 ()
        {
            zero =      new Vector3 ();
            one =       new Vector3 ( 1,  1,  1);

            unitX =     new Vector3 ( 1,  0,  0);
            unitY =     new Vector3 ( 0,  1,  0);
            unitZ =     new Vector3 ( 0,  0,  1);

            up =        new Vector3 ( 0,  1,  0);
            down =      new Vector3 ( 0, -1,  0);
            right =     new Vector3 ( 1,  0,  0);
            left =      new Vector3 (-1,  0,  0);
            forward =   new Vector3 ( 0,  0, -1);
            backward =  new Vector3 ( 0,  0,  1);
        }
        
        /// <summary>
        /// Returns a Vector3 with all of its components set to zero.
        /// </summary>
        public static Vector3 Zero
        {
            get { return zero; }
        }
        
        /// <summary>
        /// Returns a Vector3 with all of its components set to one.
        /// </summary>
        public static Vector3 One
        {
            get { return one; }
        }
        
        /// <summary>
        /// Returns the unit Vector3 for the X-axis.
        /// </summary>
        public static Vector3 UnitX
        {
            get { return unitX; }
        }

        /// <summary>
        /// Returns the unit Vector3 for the Y-axis.
        /// </summary>
        public static Vector3 UnitY
        {
            get { return unitY; }
        }
        
        /// <summary>
        /// Returns the unit Vector3 for the Z-axis.
        /// </summary>
        public static Vector3 UnitZ
        {
            get { return unitZ; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating up (0, 1, 0).
        /// </summary>
        public static Vector3 Up
        {
            get { return up; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating down (0, −1, 0).
        /// </summary>
        public static Vector3 Down
        {
            get { return down; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 pointing to the right (1, 0, 0).
        /// </summary>
        public static Vector3 Right
        {
            get { return right; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating left (−1, 0, 0).
        /// </summary>
        public static Vector3 Left
        {
            get { return left; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating forward in a right-handed 
        /// coordinate system (0, 0, -1).
        /// </summary>
        public static Vector3 Forward
        {
            get { return forward; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating backward in a right-handed 
        /// coordinate system (0, 0, 1).
        /// </summary>
        public static Vector3 Backward
        {
            get { return backward; }
        }
        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static void Distance (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Fixed32 result)
        {
            Fixed32 dx = vector1.X - vector2.X;
            Fixed32 dy = vector1.Y - vector2.Y;
            Fixed32 dz = vector1.Z - vector2.Z;

            Fixed32 lengthSquared = (dx * dx) + (dy * dy) + (dz * dz);

            result = RealMaths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Fixed32 result)
        {
            Fixed32 dx = vector1.X - vector2.X;
            Fixed32 dy = vector1.Y - vector2.Y;
            Fixed32 dz = vector1.Z - vector2.Z;

            result = (dx * dx) + (dy * dy) + (dz * dz);
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static void Dot (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Fixed32 result)
        {
            result =
                (vector1.X * vector2.X) +
                (vector1.Y * vector2.Y) +
                (vector1.Z * vector2.Z);
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static void Normalise (ref Vector3 vector, out Vector3 result)
        {
            Fixed32 lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z);

            Fixed32 epsilon; RealMaths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Fixed32.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 one = 1;
            Fixed32 multiplier = one / RealMaths.Sqrt (lengthSquared);

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
        }

        /// <summary>
        /// Calculates the cross product of two vectors.
        /// </summary>
        public static void Cross (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Vector3 result)
        {
            result.X = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y);
            result.Y = (vector1.Z * vector2.X) - (vector1.X * vector2.Z);
            result.Z = (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
        }

        /// <summary>
        /// Returns the vector of an incident vector reflected across the a
        /// specified normal vector.
        /// </summary>
        public static void Reflect (
            ref Vector3 vector,
            ref Vector3 normal,
            out Vector3 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 two = 2;

            Fixed32 num =
                (vector.X * normal.X) +
                (vector.Y * normal.Y) +
                (vector.Z * normal.Z);

            result.X = vector.X - ((two * num) * normal.X);
            result.Y = vector.Y - ((two * num) * normal.Y);
            result.Z = vector.Z - ((two * num) * normal.Z);
        }

        /// <summary>
        /// Transforms a Vector3 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector3 vector,
            ref Matrix44 matrix,
            out Vector3 result)
        {
            Fixed32 x =
                (vector.X * matrix.M11) +
                (vector.Y * matrix.M21) +
                (vector.Z * matrix.M31) + matrix.M41;

            Fixed32 y =
                (vector.X * matrix.M12) +
                (vector.Y * matrix.M22) +
                (vector.Z * matrix.M32) + matrix.M42;

            Fixed32 z =
                (vector.X * matrix.M13) +
                (vector.Y * matrix.M23) +
                (vector.Z * matrix.M33) + matrix.M43;

            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Transforms a vector by a specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector3 vector,
            ref Quaternion rotation,
            out Vector3 result)
        {
            Fixed32 two = 2;

            Fixed32 x = rotation.X;
            Fixed32 y = rotation.Y;
            Fixed32 z = rotation.Z;
            Fixed32 w = rotation.W;

            Fixed32 xx = x * x;
            Fixed32 yy = y * y;
            Fixed32 zz = z * z;

            Fixed32 wx = w * x;
            Fixed32 wy = w * y;
            Fixed32 wz = w * z;
            Fixed32 xy = x * y;
            Fixed32 xz = x * z;
            Fixed32 yz = y * z;

            result.X =
                + vector.X
                - (two * vector.X * (yy + zz))
                + (two * vector.Y * (xy - wz))
                + (two * vector.Z * (xz + wy));

            result.Y =
                + vector.Y
                + (two * vector.X * (xy + wz))
                - (two * vector.Y * (xx + zz))
                + (two * vector.Z * (yz - wx));

            result.Z =
                + vector.Z
                + (two * vector.X * (xz - wy))
                + (two * vector.Y * (yz + wx))
                - (two * vector.Z * (xx + yy));
        }

        /// <summary>
        /// Transforms a normalised Vector3 by a Matrix44.
        /// </summary>
        public static void TransformNormal (
            ref Vector3 normal,
            ref Matrix44 matrix,
            out Vector3 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Fixed32 x =
                (normal.X * matrix.M11) +
                (normal.Y * matrix.M21) +
                (normal.Z * matrix.M31);

            Fixed32 y =
                (normal.X * matrix.M12) +
                (normal.Y * matrix.M22) +
                (normal.Z * matrix.M32);

            Fixed32 z =
                (normal.X * matrix.M13) +
                (normal.Y * matrix.M23) +
                (normal.Z * matrix.M33);

            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Vector3 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Vector3) {
                flag = this.Equals ((Vector3)obj);
            }
            return flag;
        }

        #region IEquatable<Vector3>

        /// <summary>
        /// Determines whether or not this Vector3 object is equal to another
        /// Vector3 object.
        /// </summary>
        public Boolean Equals (Vector3 other)
        {
            return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Vector3 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Vector3 value1, Vector3 value2)
        {
            return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z));
        }

        /// <summary>
        /// Determines whether or not two Vector3 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Vector3 value1, Vector3 value2)
        {
            if ((value1.X == value2.X) && (value1.Y == value2.Y)) {
                return !(value1.Z == value2.Z);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector3 objects.
        /// </summary>
        public static void Add (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
        }

        /// <summary>
        /// Performs addition of two Vector3 objects using the (X+Y) operator. 
        /// </summary>
        public static Vector3 operator + (Vector3 value1, Vector3 value2)
        {
            Vector3 vector;
            vector.X = value1.X + value2.X;
            vector.Y = value1.Y + value2.Y;
            vector.Z = value1.Z + value2.Z;
            return vector;
        }


        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector3 objects.
        /// </summary>
        public static void Subtract (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        /// <summary>
        /// Performs subtraction of two Vector3 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Vector3 operator - (Vector3 value1, Vector3 value2)
        {
            Vector3 vector;
            vector.X = value1.X - value2.X;
            vector.Y = value1.Y - value2.Y;
            vector.Z = value1.Z - value2.Z;
            return vector;
        }


        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Vector3 object.
        /// </summary>
        public static void Negate (ref Vector3 value, out Vector3 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
        }

        /// <summary>
        /// Performs negation of a Vector3 object using the (-X) operator.
        /// </summary>
        public static Vector3 operator - (Vector3 value)
        {
            Vector3 vector;
            vector.X = -value.X;
            vector.Y = -value.Y;
            vector.Z = -value.Z;
            return vector;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector3 objects.
        /// </summary>
        public static void Multiply (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
        }

        /// <summary>
        /// Performs multiplication of a Vector3 object and a Fixed32
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Vector3 value1, Fixed32 scaleFactor, out Vector3 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Vector3 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Vector3 operator * (Vector3 value1, Vector3 value2)
        {
            Vector3 vector;
            vector.X = value1.X * value2.X;
            vector.Y = value1.Y * value2.Y;
            vector.Z = value1.Z * value2.Z;
            return vector;
        }

        /// <summary>
        /// Performs multiplication of a Vector3 object and a Fixed32
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Vector3 operator * (Vector3 value, Fixed32 scaleFactor)
        {
            Vector3 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            vector.Z = value.Z * scaleFactor;
            return vector;
        }

        /// <summary>
        /// Performs multiplication of a Fixed32 precision scaling factor 
        /// and aVector3 object using the (x*Y) operator.
        /// </summary>
        public static Vector3 operator * (Fixed32 scaleFactor, Vector3 value)
        {
            Vector3 vector;
            vector.X = value.X * scaleFactor;
            vector.Y = value.Y * scaleFactor;
            vector.Z = value.Z * scaleFactor;
            return vector;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector3 objects.
        /// </summary>
        public static void Divide (ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
        }

        /// <summary>
        /// Performs division of a Vector3 object and a Fixed32 precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Vector3 value1, Fixed32 value2, out Vector3 result)
        {
            Fixed32 one = 1;
            Fixed32 num = one / value2;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
        }

        /// <summary>
        /// Performs division of two Vector3 objects using the (X/Y) operator.
        /// </summary>
        public static Vector3 operator / (Vector3 value1, Vector3 value2)
        {
            Vector3 vector;
            vector.X = value1.X / value2.X;
            vector.Y = value1.Y / value2.Y;
            vector.Z = value1.Z / value2.Z;
            return vector;
        }

        /// <summary>
        /// Performs division of a Vector3 object and a Fixed32 precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
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
        
        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector3 vector1,
            ref Vector3 vector2,
            Fixed32 amount,
            out Vector3 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 two = 2;
            Fixed32 three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
            amount = (amount * amount) * (three - (two * amount));

            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
            result.Z = vector1.Z + ((vector2.Z - vector1.Z) * amount);
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// Features:
        /// - The spline passes through all of the control points.
        /// - The spline is C^1 continuous, meaning that there are no
        ///   discontinuities in the tangent direction and magnitude.
        /// - The spline is not C^2 continuous.  The second derivative is
        ///   linearly interpolated within each segment, causing the curvature
        ///   to vary linearly over the length of the segment.
        /// </summary>
        public static void CatmullRom (
            ref Vector3 vector1,
            ref Vector3 vector2,
            ref Vector3 vector3,
            ref Vector3 vector4,
            Fixed32 amount,
            out Vector3 result)
        {
            Fixed32 half; RealMaths.Half(out half);
            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 two = 2;
            Fixed32 three = 3;
            Fixed32 four = 4;
            Fixed32 five = 5;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;

            result.X =
                half * ((((two * vector2.X) + ((-vector1.X + vector3.X) *
                amount)) + (((((two * vector1.X) - (five * vector2.X)) + (four *
                vector3.X)) - vector4.X) * squared)) + ((((-vector1.X + (three *
                vector2.X)) - (three * vector3.X)) + vector4.X) * cubed));

            result.Y =
                half * ((((two * vector2.Y) + ((-vector1.Y + vector3.Y) *
                amount)) + (((((two * vector1.Y) - (five * vector2.Y)) + (four *
                vector3.Y)) - vector4.Y) * squared)) + ((((-vector1.Y + (three *
                vector2.Y)) - (three * vector3.Y)) + vector4.Y) * cubed));

            result.Z =
                half * ((((two * vector2.Z) + ((-vector1.Z + vector3.Z) *
                amount)) + (((((two * vector1.Z) - (five * vector2.Z)) + (four *
                vector3.Z)) - vector4.Z) * squared)) + ((((-vector1.Z + (three *
                vector2.Z)) - (three * vector3.Z)) + vector4.Z) * cubed));
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector3 vector1,
            ref Vector3 tangent1,
            ref Vector3 vector2,
            ref Vector3 tangent2,
            Fixed32 amount,
            out Vector3 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 two = 2;
            Fixed32 three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            if( !tangent1.IsUnit() || !tangent2.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;

            Fixed32 a = ((two * cubed) - (three * squared)) + one;
            Fixed32 b = (-two * cubed) + (three * squared);
            Fixed32 c = (cubed - (two * squared)) + amount;
            Fixed32 d = cubed - squared;

            result.X =
                (vector1.X * a) + (vector2.X * b) +
                (tangent1.X * c) + (tangent2.X * d);

            result.Y =
                (vector1.Y * a) + (vector2.Y * b) +
                (tangent1.Y * c) + (tangent2.Y * d);

            result.Z =
                (vector1.Z * a) + (vector2.Z * b) +
                (tangent1.Z * c) + (tangent2.Z * d);
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Min (
            ref Vector3 a,
            ref Vector3 b,
            out Vector3 result)
        {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
            result.Z = (a.Z < b.Z) ? a.Z : b.Z;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Max (
            ref Vector3 a,
            ref Vector3 b,
            out Vector3 result)
        {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
            result.Z = (a.Z > b.Z) ? a.Z : b.Z;
        }
        
        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static void Clamp (
            ref Vector3 a,
            ref Vector3 min,
            ref Vector3 max,
            out Vector3 result)
        {
            Fixed32 x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            Fixed32 y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            Fixed32 z = a.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static void Lerp (
            ref Vector3 a,
            ref Vector3 b,
            Fixed32 amount,
            out Vector3 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }
            
            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
            result.Z = a.Z + ((b.Z - a.Z) * amount);
        }
    }

    /// <summary>
    /// Fixed32 precision Vector4.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector4 
        : IEquatable<Vector4>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector4.
        /// </summary>
        public Fixed32 X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector4.
        /// </summary>
        public Fixed32 Y;

        /// <summary>
        /// Gets or sets the Z-component of the Vector4.
        /// </summary>
        public Fixed32 Z;

        /// <summary>
        /// Gets or sets the W-component of the Vector4.
        /// </summary>
        public Fixed32 W;

        /// <summary>
        /// Initilises a new instance of Vector4 from four Fixed32 values 
        /// representing X, Y, Z and W respectively.
        /// </summary>
        public Vector4 (
            Fixed32 x, 
            Fixed32 y, 
            Fixed32 z, 
            Fixed32 w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initilises a new instance of Vector4 from one Vector2 value
        /// representing X and Y and two Fixed32 values representing Z and
        /// W respectively.
        /// </summary>
        public Vector4 (Vector2 value, Fixed32 z, Fixed32 w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initilises a new instance of Vector4 from one Vector3 value
        /// representing X, Y and Z and one Fixed32 value representing W.
        /// </summary>
        public Vector4 (Vector3 value, Fixed32 w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        /// <summary>
        /// Calculates the length of the Vector4.
        /// </summary>
        public Fixed32 Length ()
        {
            Fixed32 num = (this.X * this.X) + 
                              (this.Y * this.Y) + 
                              (this.Z * this.Z) + 
                              (this.W * this.W);
            
            return RealMaths.Sqrt (num);
        }

        /// <summary>
        /// Calculates the length of the Vector4 squared.
        /// </summary>
        public Fixed32 LengthSquared ()
        {
            return 
                (this.X * this.X) + 
                (this.Y * this.Y) + 
                (this.Z * this.Z) + 
                (this.W * this.W);
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format (
                "{{X:{0} Y:{1} Z:{2} W:{3}}}", 
                new Object[] 
                { 
                    this.X.ToString (), 
                    this.Y.ToString (), 
                    this.Z.ToString (), 
                    this.W.ToString () 
                }
                );
        }

        /// <summary>
        /// Gets the hash code of the Vector4 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return (
                this.X.GetHashCode () + 
                this.Y.GetHashCode () + 
                this.Z.GetHashCode () + 
                this.W.GetHashCode ()
                );
        }

        /// <summary>
        /// Detemines whether or not the Vector4 is of unit length.
        /// </summary>
        public Boolean IsUnit()
        {
            Fixed32 one = 1;
            return RealMaths.IsZero(one - W*W - X*X - Y*Y - Z*Z);
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Vector2 with all of its components set to zero.
        /// </summary>
        static Vector4 zero;

        /// <summary>
        /// Defines a Vector2 with all of its components set to one.
        /// </summary>
        static Vector4 one;

        /// <summary>
        /// Defines the unit Vector2 for the X-axis.
        /// </summary>
        static Vector4 unitX;

        /// <summary>
        /// Defines the unit Vector2 for the Y-axis.
        /// </summary>
        static Vector4 unitY;

        /// <summary>
        /// Defines the unit Vector2 for the Z-axis.
        /// </summary>
        static Vector4 unitZ;

        /// <summary>
        /// Defines the unit Vector2 for the W-axis.
        /// </summary>
        static Vector4 unitW;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Vector4 ()
        {
            zero =      new Vector4 ();
            one =       new Vector4 (1, 1, 1, 1);

            unitX =     new Vector4 (1, 0, 0, 0);
            unitY =     new Vector4 (0, 1, 0, 0);
            unitZ =     new Vector4 (0, 0, 1, 0);
            unitW =     new Vector4 (0, 0, 0, 1);
        }

        /// <summary>
        /// Returns a Vector4 with all of its components set to zero.
        /// </summary>
        public static Vector4 Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Returns a Vector4 with all of its components set to one.
        /// </summary>
        public static Vector4 One
        {
            get { return one; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the X-axis.
        /// </summary>
        public static Vector4 UnitX
        {
            get { return unitX; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the Y-axis.
        /// </summary>
        public static Vector4 UnitY
        {
            get { return unitY; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the Z-axis.
        /// </summary>
        public static Vector4 UnitZ
        {
            get { return unitZ; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the W-axis.
        /// </summary>
        public static Vector4 UnitW
        {
            get { return unitW; }
        }
        
        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static void Distance (
            ref Vector4 vector1,
            ref Vector4 vector2,
            out Fixed32 result)
        {
            Fixed32 dx = vector1.X - vector2.X;
            Fixed32 dy = vector1.Y - vector2.Y;
            Fixed32 dz = vector1.Z - vector2.Z;
            Fixed32 dw = vector1.W - vector2.W;

            Fixed32 lengthSquared =
                (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);

            result = RealMaths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector4 vector1,
            ref Vector4 vector2,
            out Fixed32 result)
        {
            Fixed32 dx = vector1.X - vector2.X;
            Fixed32 dy = vector1.Y - vector2.Y;
            Fixed32 dz = vector1.Z - vector2.Z;
            Fixed32 dw = vector1.W - vector2.W;

            result = (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static void Dot (
            ref Vector4 vector1,
            ref Vector4 vector2,
            out Fixed32 result)
        {
            result =
                (vector1.X * vector2.X) +
                (vector1.Y * vector2.Y) +
                (vector1.Z * vector2.Z) +
                (vector1.W * vector2.W);
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static void Normalise (
            ref Vector4 vector,
            out Vector4 result)
        {
            Fixed32 lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z) +
                (vector.W * vector.W);

            Fixed32 epsilon; RealMaths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Fixed32.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 one = 1;
            Fixed32 multiplier = one / (RealMaths.Sqrt (lengthSquared));

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
            result.W = vector.W * multiplier;
        }

        /// <summary>
        /// Transforms a Vector4 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector4 vector,
            ref Matrix44 matrix,
            out Vector4 result)
        {
            Fixed32 x =
                (vector.X * matrix.M11) +
                (vector.Y * matrix.M21) +
                (vector.Z * matrix.M31) +
                (vector.W * matrix.M41);

            Fixed32 y =
                (vector.X * matrix.M12) +
                (vector.Y * matrix.M22) +
                (vector.Z * matrix.M32) +
                (vector.W * matrix.M42);

            Fixed32 z =
                (vector.X * matrix.M13) +
                (vector.Y * matrix.M23) +
                (vector.Z * matrix.M33) +
                (vector.W * matrix.M43);

            Fixed32 w =
                (vector.X * matrix.M14) +
                (vector.Y * matrix.M24) +
                (vector.Z * matrix.M34) +
                (vector.W * matrix.M44);

            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }

        /// <summary>
        /// Transforms a Vector4 by the specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector4 vector,
            ref Quaternion rotation,
            out Vector4 result)
        {
            Fixed32 two = 2;

            Fixed32 x = rotation.X;
            Fixed32 y = rotation.Y;
            Fixed32 z = rotation.Z;
            Fixed32 w = rotation.W;

            Fixed32 xx = x * x;
            Fixed32 yy = y * y;
            Fixed32 zz = z * z;

            Fixed32 wx = w * x;
            Fixed32 wy = w * y;
            Fixed32 wz = w * z;
            Fixed32 xy = x * y;
            Fixed32 xz = x * z;
            Fixed32 yz = y * z;

            result.X =
                + vector.X
                - (two * vector.X * (yy + zz))
                + (two * vector.Y * (xy - wz))
                + (two * vector.Z * (xz + wy));

            result.Y =
                + vector.Y
                + (two * vector.X * (xy + wz))
                - (two * vector.Y * (xx + zz))
                + (two * vector.Z * (yz - wx));

            result.Z =
                + vector.Z
                + (two * vector.X * (xz - wy))
                + (two * vector.Y * (yz + wx))
                - (two * vector.Z * (xx + yy));

            result.W = vector.W;
        }

        /// <summary>
        /// Transforms a normalised Vector4 by a Matrix44.
        /// </summary>
        public static void TransformNormal (
            ref Vector4 normal,
            ref Matrix44 matrix,
            out Vector4 result)
        {
            if( !normal.IsUnit() )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Fixed32 x =
                (normal.X * matrix.M11) + (normal.Y * matrix.M21) +
                (normal.Z * matrix.M31) + (normal.W * matrix.M41);

            Fixed32 y =
                (normal.X * matrix.M12) + (normal.Y * matrix.M22) +
                (normal.Z * matrix.M32) + (normal.W * matrix.M42);

            Fixed32 z =
                (normal.X * matrix.M13) + (normal.Y * matrix.M23) +
                (normal.Z * matrix.M33) + (normal.W * matrix.M43);

            Fixed32 w =
                (normal.X * matrix.M14) + (normal.Y * matrix.M24) +
                (normal.Z * matrix.M34) + (normal.W * matrix.M44);

            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not this Vector4 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            Boolean flag = false;
            if (obj is Vector4) {
                flag = this.Equals ((Vector4)obj);
            }
            return flag;
        }

        #region IEquatable<Vector4>

        /// <summary>
        /// Determines whether or not this Vector4 object is equal to another
        /// Vector4 object.
        /// </summary>
        public Boolean Equals (Vector4 other)
        {
            return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
        }

        #endregion

        /// <summary>
        /// Determines whether or not two Vector4 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static Boolean operator == (Vector4 value1, Vector4 value2)
        {
            return ((((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) && (value1.W == value2.W));
        }
        
        /// <summary>
        /// Determines whether or not two Vector4 objects are not equal using
        /// the (X!=Y) operator.
        /// </summary>
        public static Boolean operator != (Vector4 value1, Vector4 value2)
        {
            if (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z)) {
                return !(value1.W == value2.W);
            }
            return true;
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector4 objects.
        /// </summary>
        public static void Add (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
            result.W = value1.W + value2.W;
        }

        /// <summary>
        /// Performs addition of two Vector4 objects using the (X+Y) operator. 
        /// </summary>
        public static Vector4 operator + (Vector4 value1, Vector4 value2)
        {
            Vector4 vector;
            vector.X = value1.X + value2.X;
            vector.Y = value1.Y + value2.Y;
            vector.Z = value1.Z + value2.Z;
            vector.W = value1.W + value2.W;
            return vector;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector4 objects.
        /// </summary>
        public static void Subtract (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
            result.W = value1.W - value2.W;
        }

        /// <summary>
        /// Performs subtraction of two Vector4 objects using the (X-Y) 
        /// operator.
        /// </summary>
        public static Vector4 operator - (Vector4 value1, Vector4 value2)
        {
            Vector4 vector;
            vector.X = value1.X - value2.X;
            vector.Y = value1.Y - value2.Y;
            vector.Z = value1.Z - value2.Z;
            vector.W = value1.W - value2.W;
            return vector;
        }

        // Negation Operators //----------------------------------------------//
        
        /// <summary>
        /// Performs negation of a Vector4 object.
        /// </summary>
        public static void Negate (ref Vector4 value, out Vector4 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        /// <summary>
        /// Performs negation of a Vector4 object using the (-X) operator.
        /// </summary>
        public static Vector4 operator - (Vector4 value)
        {
            Vector4 vector;
            vector.X = -value.X;
            vector.Y = -value.Y;
            vector.Z = -value.Z;
            vector.W = -value.W;
            return vector;
        }
        
        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector4 objects.
        /// </summary>
        public static void Multiply (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
            result.W = value1.W * value2.W;
        }

        /// <summary>
        /// Performs multiplication of a Vector4 object and a Fixed32
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (ref Vector4 value1, Fixed32 scaleFactor, out Vector4 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
            result.W = value1.W * scaleFactor;
        }

        /// <summary>
        /// Performs muliplication of two Vector4 objects using the (X*Y)
        /// operator.
        /// </summary>
        public static Vector4 operator * (Vector4 value1, Vector4 value2)
        {
            Vector4 vector;
            vector.X = value1.X * value2.X;
            vector.Y = value1.Y * value2.Y;
            vector.Z = value1.Z * value2.Z;
            vector.W = value1.W * value2.W;
            return vector;
        }
        
        /// <summary>
        /// Performs multiplication of a Vector4 object and a Fixed32
        /// precision scaling factor using the (X*y) operator.
        /// </summary>
        public static Vector4 operator * (Vector4 value1, Fixed32 scaleFactor)
        {
            Vector4 vector;
            vector.X = value1.X * scaleFactor;
            vector.Y = value1.Y * scaleFactor;
            vector.Z = value1.Z * scaleFactor;
            vector.W = value1.W * scaleFactor;
            return vector;
        }
        
        /// <summary>
        /// Performs multiplication of a Fixed32 precision scaling factor 
        /// and aVector4 object using the (x*Y) operator.
        /// </summary>
        public static Vector4 operator * (Fixed32 scaleFactor, Vector4 value1)
        {
            Vector4 vector;
            vector.X = value1.X * scaleFactor;
            vector.Y = value1.Y * scaleFactor;
            vector.Z = value1.Z * scaleFactor;
            vector.W = value1.W * scaleFactor;
            return vector;
        }
        
        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector4 objects.
        /// </summary>
        public static void Divide (ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
            result.W = value1.W / value2.W;
        }

        /// <summary>
        /// Performs division of a Vector4 object and a Fixed32 precision
        /// scaling factor.
        /// </summary>
        public static void Divide (ref Vector4 value1, Fixed32 divider, out Vector4 result)
        {
            Fixed32 one = 1;
            Fixed32 num = one / divider;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
            result.W = value1.W * num;
        }

        /// <summary>
        /// Performs division of two Vector4 objects using the (X/Y) operator.
        /// </summary>
        public static Vector4 operator / (Vector4 value1, Vector4 value2)
        {
            Vector4 vector;
            vector.X = value1.X / value2.X;
            vector.Y = value1.Y / value2.Y;
            vector.Z = value1.Z / value2.Z;
            vector.W = value1.W / value2.W;
            return vector;
        }
        
        /// <summary>
        /// Performs division of a Vector4 object and a Fixed32 precision
        /// scaling factor using the (X/y) operator.
        /// </summary>
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

        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector4 vector1,
            ref Vector4 vector2,
            Fixed32 amount,
            out Vector4 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 two = 2;
            Fixed32 three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
            amount = (amount * amount) * (three - (two * amount));

            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
            result.Z = vector1.Z + ((vector2.Z - vector1.Z) * amount);
            result.W = vector1.W + ((vector2.W - vector1.W) * amount);
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// Features:
        /// - The spline passes through all of the control points.
        /// - The spline is C^1 continuous, meaning that there are no
        ///   discontinuities in the tangent direction and magnitude.
        /// - The spline is not C^2 continuous.  The second derivative is
        ///   linearly interpolated within each segment, causing the curvature
        ///   to vary linearly over the length of the segment.
        /// </summary>
        public static void CatmullRom (
            ref Vector4 vector1,
            ref Vector4 vector2,
            ref Vector4 vector3,
            ref Vector4 vector4,
            Fixed32 amount,
            out Vector4 result)
        {
            Fixed32 half; RealMaths.Half(out half);
            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 two = 2;
            Fixed32 three = 3;
            Fixed32 four = 4;
            Fixed32 five = 5;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;

            result.X =
                half * ((((two * vector2.X) + ((-vector1.X + vector3.X) *
                amount)) + (((((two * vector1.X) - (five * vector2.X)) + (four *
                vector3.X)) - vector4.X) * squared)) + ((((-vector1.X + (three *
                vector2.X)) - (three * vector3.X)) + vector4.X) * cubed));

            result.Y =
                half * ((((two * vector2.Y) + ((-vector1.Y + vector3.Y) *
                amount)) + (((((two * vector1.Y) - (five * vector2.Y)) + (four *
                vector3.Y)) - vector4.Y) * squared)) + ((((-vector1.Y + (three *
                vector2.Y)) - (three * vector3.Y)) + vector4.Y) * cubed));

            result.Z =
                half * ((((two * vector2.Z) + ((-vector1.Z + vector3.Z) *
                amount)) + (((((two * vector1.Z) - (five * vector2.Z)) + (four *
                vector3.Z)) - vector4.Z) * squared)) + ((((-vector1.Z + (three *
                vector2.Z)) - (three * vector3.Z)) + vector4.Z) * cubed));

            result.W =
                half * ((((two * vector2.W) + ((-vector1.W + vector3.W) *
                amount)) + (((((two * vector1.W) - (five * vector2.W)) + (four *
                vector3.W)) - vector4.W) * squared)) + ((((-vector1.W + (three *
                vector2.W)) - (three * vector3.W)) + vector4.W) * cubed));
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector4 vector1,
            ref Vector4 tangent1,
            ref Vector4 vector2,
            ref Vector4 tangent2,
            Fixed32 amount,
            out Vector4 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 two = 2;
            Fixed32 three = 3;

            // Make sure that the weighting vector is within the supported range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            if( !tangent1.IsUnit() || !tangent2.IsUnit() )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;

            Fixed32 a = ((two * cubed) - (three * squared)) + one;
            Fixed32 b = (-two * cubed) + (three * squared);
            Fixed32 c = (cubed - (two * squared)) + amount;
            Fixed32 d = cubed - squared;

            result.X =
                (vector1.X * a) + (vector2.X * b) +
                (tangent1.X * c) + (tangent2.X * d);

            result.Y =
                (vector1.Y * a) + (vector2.Y * b) +
                (tangent1.Y * c) + (tangent2.Y * d);

            result.Z =
                (vector1.Z * a) + (vector2.Z * b) +
                (tangent1.Z * c) + (tangent2.Z * d);

            result.W =
                (vector1.W * a) + (vector2.W * b) +
                (tangent1.W * c) + (tangent2.W * d);
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Min (
            ref Vector4 a,
            ref Vector4 b,
            out Vector4 result)
        {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
            result.Z = (a.Z < b.Z) ? a.Z : b.Z;
            result.W = (a.W < b.W) ? a.W : b.W;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching 
        /// pair of components.
        /// </summary>
        public static void Max (
            ref Vector4 a,
            ref Vector4 b,
            out Vector4 result)
        {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
            result.Z = (a.Z > b.Z) ? a.Z : b.Z;
            result.W = (a.W > b.W) ? a.W : b.W;
        }
        
        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static void Clamp (
            ref Vector4 a,
            ref Vector4 min,
            ref Vector4 max,
            out Vector4 result)
        {
            Fixed32 x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            Fixed32 y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            Fixed32 z = a.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            Fixed32 w = a.W;
            w = (w > max.W) ? max.W : w;
            w = (w < min.W) ? min.W : w;
            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }
        
        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static void Lerp (
            ref Vector4 a,
            ref Vector4 b,
            Fixed32 amount,
            out Vector4 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }
            
            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
            result.Z = a.Z + ((b.Z - a.Z) * amount);
            result.W = a.W + ((b.W - a.W) * amount);
        }

    }

}

