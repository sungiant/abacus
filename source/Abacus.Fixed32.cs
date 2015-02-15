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

#define VARIANTS_ENABLED

using System;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

namespace Abacus.Fixed32Precision
{
    ///
    /// Fixed32 is a binary fixed
    /// point number in the Q39.24 number format.
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
        public const Byte s = 1;

        // m is the number of bits set aside to designate the two's complement integer
        // portion of the number exclusive of the sign bit.
        public const Byte m = 32 - n - s;

        // n is the number of bits used to designate the fractional portion of the
        // number, i.e. the number of bit's to the right of the binary point.
        // (If n = 0, the Q numbers are integers — the degenerate case)
        public const Byte n = 12;

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

        public Fixed32(Int32 value)
        {
            numerator = value << n;
        }

        public Fixed32 (Double value)
        {
            numerator = (Int32)System.Math.Round (value * (1 << n));
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

        public static Fixed32 CreateFromRaw (
            Int32 rawValue)
        {
            Fixed32 f;
            f.numerator = rawValue;
            return f;
        }

        #region Constants

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

            if (!(value is Fixed32))
                throw new ArgumentException("Value is not a Fixed32.");

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
    /// Fixed32 precision Quaternion.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Quaternion
        : IEquatable<Quaternion>
    {
        /// <summary>
        /// Gets or sets the imaginary I-component of the Quaternion.
        /// </summary>
        public Fixed32 I;

        /// <summary>
        /// Gets or sets the imaginary J-component of the Quaternion.
        /// </summary>
        public Fixed32 J;

        /// <summary>
        /// Gets or sets the imaginary K-component of the Quaternion.
        /// </summary>
        public Fixed32 K;

        /// <summary>
        /// Gets or sets the real U-component of the Quaternion.
        /// </summary>
        public Fixed32 U;

        /// <summary>
        /// Initilises a new instance of Quaternion from three imaginary
        /// Fixed32 values and one real Fixed32 value representing
        /// I, J, K and U respectively.
        /// </summary>
        public Quaternion (
            Fixed32 i, Fixed32 j, Fixed32 k, Fixed32 u)
        {
            this.I = i;
            this.J = j;
            this.K = k;
            this.U = u;
        }

        /// <summary>
        /// Initilises a new instance of Quaternion from a Vector3 representing
        /// the imaginary parts of the quaternion (I, J & K) and one
        /// Fixed32 value representing the real part of the
        /// Quaternion (U).
        /// </summary>
        public Quaternion (Vector3 vectorPart, Fixed32 scalarPart)
        {
            this.I = vectorPart.X;
            this.J = vectorPart.Y;
            this.K = vectorPart.Z;
            this.U = scalarPart;
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return String.Format ("{{I:{0} J:{1} K:{2} U:{3}}}",
                I.ToString (), J.ToString (), K.ToString (), U.ToString ());
        }

        /// <summary>
        /// Gets the hash code of the Quaternion object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return U.GetHashCode ().ShiftAndWrap (6)
                 ^ K.GetHashCode ().ShiftAndWrap (4)
                 ^ J.GetHashCode ().ShiftAndWrap (2)
                 ^ I.GetHashCode ();
        }

        /// <summary>
        /// Determines whether or not this Quaternion object is equal to another
        /// object
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            return (obj is Quaternion)
                ? this.Equals ((Quaternion) obj)
                : false;
        }

        #region IEquatable<Quaternion>

        /// <summary>
        /// Determines whether or not this Quaternion object is equal to another
        /// Quaternion object.
        /// </summary>
        public Boolean Equals (Quaternion other)
        {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        #endregion

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines the identity quaternion.
        /// </summary>
        static Quaternion identity;

        /// <summary>
        /// Defines the zero quaternion.
        /// </summary>
        static Quaternion zero;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Quaternion ()
        {
            identity = new Quaternion (0, 0, 0, 1);
            zero = new Quaternion (0, 0, 0, 0);
        }

        /// <summary>
        /// Returns the identity Quaternion.
        /// </summary>
        public static Quaternion Identity
        {
            get { return identity; }
        }

        /// <summary>
        /// Returns the zero Quaternion.
        /// </summary>
        public static Quaternion Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Creates a Quaternion from a vector and an angle to rotate about
        /// the vector.
        /// </summary>
        public static void CreateFromAxisAngle (
            ref Vector3 axis, ref Fixed32 angle, out Quaternion result)
        {
            Fixed32 half; Maths.Half (out half);
            Fixed32 theta = angle * half;

            Fixed32 sin = Maths.Sin (theta);
            Fixed32 cos = Maths.Cos (theta);

            result.I = axis.X * sin;
            result.J = axis.Y * sin;
            result.K = axis.Z * sin;

            result.U = cos;
        }

        /// <summary>
        /// Creates a new Quaternion from specified yaw, pitch, and roll angles.
        /// </summary>
        public static void CreateFromYawPitchRoll (
            ref Fixed32 yaw, ref Fixed32 pitch,
            ref Fixed32 roll, out Quaternion result)
        {
            Fixed32 half; Maths.Half(out half);

            Fixed32 hr = roll * half;
            Fixed32 hp = pitch * half;
            Fixed32 hy = yaw * half;

            Fixed32 shr = Maths.Sin (hr);
            Fixed32 chr = Maths.Cos (hr);
            Fixed32 shp = Maths.Sin (hp);
            Fixed32 chp = Maths.Cos (hp);
            Fixed32 shy = Maths.Sin (hy);
            Fixed32 chy = Maths.Cos (hy);

            result.I = (chy * shp * chr) + (shy * chp * shr);
            result.J = (shy * chp * chr) - (chy * shp * shr);
            result.K = (chy * chp * shr) - (shy * shp * chr);
            result.U = (chy * chp * chr) + (shy * shp * shr);
        }

        /// <summary>
        /// Creates a Quaternion from a rotation Matrix44.
        /// </summary>
        public static void CreateFromRotationMatrix (
            ref Matrix44 m, out Quaternion result)
        {
            // http://www.euclideanspace.com/maths/geometry/rotations/conversions/mToQuaternion/
            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 two = 2;
            Fixed32 quarter; Maths.Quarter (out quarter);

            Fixed32 tr = (m.R0C0 + m.R1C1) + m.R2C2;

            if (tr > zero)
            {
                Fixed32 s = Maths.Sqrt (tr + one) * two;
                result.U = quarter * s;
                result.I = (m.R1C2 - m.R2C1) / s;
                result.J = (m.R2C0 - m.R0C2) / s;
                result.K = (m.R0C1 - m.R1C0) / s;
            }
            else if ((m.R0C0 >= m.R1C1) && (m.R0C0 >= m.R2C2))
            {
                Fixed32 s = Maths.Sqrt (one + m.R0C0 - m.R1C1 - m.R2C2) * two;
                result.U = (m.R1C2 - m.R2C1) / s;
                result.I = quarter * s;
                result.J = (m.R0C1 + m.R1C0) / s;
                result.K = (m.R0C2 + m.R2C0) / s;
            }
            else if (m.R1C1 > m.R2C2)
            {
                Fixed32 s = Maths.Sqrt (one + m.R1C1 - m.R0C0 - m.R2C2) * two;
                result.U = (m.R2C0 - m.R0C2) / s;
                result.I = (m.R1C0 + m.R0C1) / s;
                result.J = quarter * s;
                result.K = (m.R2C1 + m.R1C2) / s;
            }
            else
            {
                Fixed32 s = Maths.Sqrt (one + m.R2C2 - m.R0C0 - m.R1C1) * two;
                result.U = (m.R0C1 - m.R1C0) / s;
                result.I = (m.R2C0 + m.R0C2) / s;
                result.J = (m.R2C1 + m.R1C2) / s;
                result.K = quarter * s;
            }
        }
        /// <summary>
        /// Calculates the length² of a Quaternion.
        /// </summary>
        public static void LengthSquared (
            ref Quaternion quaternion, out Fixed32 result)
        {
            result =
                (quaternion.I * quaternion.I) +
                (quaternion.J * quaternion.J) +
                (quaternion.K * quaternion.K) +
                (quaternion.U * quaternion.U);
        }

        /// <summary>
        /// Calculates the length of a Quaternion.
        /// </summary>
        public static void Length (
            ref Quaternion quaternion, out Fixed32 result)
        {
            Fixed32 lengthSquared =
                (quaternion.I * quaternion.I) +
                (quaternion.J * quaternion.J) +
                (quaternion.K * quaternion.K) +
                (quaternion.U * quaternion.U);

            result = Maths.Sqrt (lengthSquared);
        }


        /// <summary>
        /// Calculates the conjugate of a Quaternion.
        /// </summary>
        public static void Conjugate (
            ref Quaternion value, out Quaternion result)
        {
            result.I = -value.I;
            result.J = -value.J;
            result.K = -value.K;
            result.U = value.U;
        }

        /// <summary>
        /// Calculates the inverse of two Quaternions.
        /// </summary>
        public static void Inverse (
            ref Quaternion quaternion, out Quaternion result)
        {
            Fixed32 one = 1;
            Fixed32 a =
                (quaternion.I * quaternion.I) +
                (quaternion.J * quaternion.J) +
                (quaternion.K * quaternion.K) +
                (quaternion.U * quaternion.U);

            Fixed32 b = one / a;

            result.I = -quaternion.I * b;
            result.J = -quaternion.J * b;
            result.K = -quaternion.K * b;
            result.U =  quaternion.U * b;
        }

        /// <summary>
        /// Calculates the dot product of two Quaternions.
        /// </summary>
        public static void Dot (
            ref Quaternion q1, ref Quaternion q2, out Fixed32 result)
        {
            result =
                (q1.I * q2.I) + (q1.J * q2.J) +
                (q1.K * q2.K) + (q1.U * q2.U);
        }

        /// <summary>
        /// Concatenates two Quaternions; the result represents the first
        /// rotation followed by the second rotation.
        /// </summary>
        public static void Concatenate (
            ref Quaternion q1, ref Quaternion q2, out Quaternion result)
        {
            Fixed32 a = (q2.J * q1.K) - (q2.K * q1.J);
            Fixed32 b = (q2.K * q1.I) - (q2.I * q1.K);
            Fixed32 c = (q2.I * q1.J) - (q2.J * q1.I);
            Fixed32 d = (q2.I * q1.I) - (q2.J * q1.J);

            result.I = (q2.I * q1.U) + (q1.I * q2.U) + a;
            result.J = (q2.J * q1.U) + (q1.J * q2.U) + b;
            result.K = (q2.K * q1.U) + (q1.K * q2.U) + c;
            result.U = (q2.U * q1.U) - (q2.K * q1.K) - d;
        }

        /// <summary>
        /// Divides each component of the quaternion by the length of the
        /// quaternion.
        /// </summary>
        public static void Normalise (
            ref Quaternion quaternion, out Quaternion result)
        {
            Fixed32 one = 1;

            Fixed32 a =
                (quaternion.I * quaternion.I) +
                (quaternion.J * quaternion.J) +
                (quaternion.K * quaternion.K) +
                (quaternion.U * quaternion.U);

            Fixed32 b = one / Maths.Sqrt (a);

            result.I = quaternion.I * b;
            result.J = quaternion.J * b;
            result.K = quaternion.K * b;
            result.U = quaternion.U * b;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not two Quaternion objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static void Equals (
            ref Quaternion q1, ref Quaternion q2, out Boolean result)
        {
            result =
                (q1.I == q2.I) && (q1.J == q2.J) &&
                (q1.K == q2.K) && (q1.U == q2.U);
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Quaternion objects.
        /// </summary>
        public static void Add (
            ref Quaternion q1, ref Quaternion q2, out Quaternion result)
        {
            result.I = q1.I + q2.I;
            result.J = q1.J + q2.J;
            result.K = q1.K + q2.K;
            result.U = q1.U + q2.U;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Quaternion objects.
        /// </summary>
        public static void Subtract (
            ref Quaternion q1, ref Quaternion q2, out Quaternion result)
        {
            result.I = q1.I - q2.I;
            result.J = q1.J - q2.J;
            result.K = q1.K - q2.K;
            result.U = q1.U - q2.U;
        }

        // Negation Operators //----------------------------------------------//

        /// <summary>
        /// Performs negation of a Quaternion object.
        /// </summary>
        public static void Negate (
            ref Quaternion quaternion, out Quaternion result)
        {
            result.I = -quaternion.I;
            result.J = -quaternion.J;
            result.K = -quaternion.K;
            result.U = -quaternion.U;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Quaternion objects,
        /// (Quaternion multiplication is not commutative),
        /// (i^2 = j^2 = k^2 = i j k = -1).
        ///
        /// For Quaternion division the notation q1 / q2 is not ideal, since
        /// Quaternion multiplication is not commutative we need to be able
        /// to distinguish between q1*(q2^-1) and (q2^-1)*q1. This is why
        /// Abacus does not have a division opperator.  If you need
        /// a divide operation just multiply by the inverse.
        /// </summary>
        public static void Multiply (
            ref Quaternion q1, ref Quaternion q2, out Quaternion result)
        {
            // http://www.euclideanspace.com/maths/algebra/realNormedAlgebra/quaternions/arithmetic/index.htm

            result.I = q1.I * q2.U + q1.U * q2.I + q1.J * q2.K - q1.K * q2.J;
            result.J = q1.U * q2.J - q1.I * q2.K + q1.J * q2.U + q1.K * q2.I;
            result.K = q1.U * q2.K + q1.I * q2.J - q1.J * q2.I + q1.K * q2.U;
            result.U = q1.U * q2.U - q1.I * q2.I - q1.J * q2.J - q1.K * q2.K;
        }

        /// <summary>
        /// Perform a spherical linear interpolation between two Quaternions.
        /// Provides a constant-speed motion along a unit-radius great circle
        /// arc, given the ends and an interpolation parameter between 0 and 1.
        /// http://en.wikipedia.org/wiki/Slerp
        /// </summary>
        public static void Slerp (
            ref Quaternion quaternion1,
            ref Quaternion quaternion2,
            ref Fixed32 amount,
            out Quaternion result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 epsilon; Maths.Epsilon (out epsilon);

            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 remaining = one - amount;

            Fixed32 angle;
            Dot (ref quaternion1, ref quaternion2, out angle);

            if (angle < zero)
            {
                Negate (ref quaternion1, out quaternion1);
                angle = -angle;
            }

            Fixed32 theta = Maths.ArcCos (angle);


            Fixed32 r = remaining;
            Fixed32 a = amount;

            // To avoid division by 0 and by very small numbers the
            // Lerp is used when theta is small.
            if (theta > epsilon)
            {
                Fixed32 x = Maths.Sin (remaining * theta);
                Fixed32 y = Maths.Sin (amount * theta);
                Fixed32 z = Maths.Sin (theta);

                r = x / z;
                a = y / z;
            }

            result.U = (r * quaternion1.U) + (a * quaternion2.U);
            result.I = (r * quaternion1.I) + (a * quaternion2.I);
            result.J = (r * quaternion1.J) + (a * quaternion2.J);
            result.K = (r * quaternion1.K) + (a * quaternion2.K);
        }

        /// <summary>
        /// Perform a linear interpolation between two Quaternions.
        /// </summary>
        public static void Lerp (
            ref Quaternion quaternion1,
            ref Quaternion quaternion2,
            ref Fixed32 amount,
            out Quaternion result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            if (amount < zero || amount > one)
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 remaining = one - amount;

            Fixed32 r = remaining;
            Fixed32 a = amount;

            result.U = (r * quaternion1.U) + (a * quaternion2.U);
            result.I = (r * quaternion1.I) + (a * quaternion2.I);
            result.J = (r * quaternion1.J) + (a * quaternion2.J);
            result.K = (r * quaternion1.K) + (a * quaternion2.K);
        }

        /// <summary>
        /// Detemines whether or not the Vector2 is of unit length.
        /// </summary>
        public static void IsUnit (
            ref Quaternion quaternion,
            out Boolean result)
        {
            Fixed32 one = 1;

            result = Maths.IsZero(
                one -
                quaternion.U * quaternion.U -
                quaternion.I * quaternion.I -
                quaternion.J * quaternion.J -
                quaternion.K * quaternion.K);
        }

#if (VARIANTS_ENABLED)

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion CreateFromAxisAngle (
            Vector3 axis,
            Fixed32 angle)
        {
            Quaternion result;
            CreateFromAxisAngle (ref axis, ref angle, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion CreateFromYawPitchRoll (
            Fixed32 yaw,
            Fixed32 pitch,
            Fixed32 roll)
        {
            Quaternion result;
            CreateFromYawPitchRoll (ref yaw, ref pitch, ref roll, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion CreateFromRotationMatrix (
            Matrix44 matrix)
        {
            Quaternion result;
            CreateFromRotationMatrix (ref matrix, out result);
            return result;
        }
        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 LengthSquared (Quaternion quaternion)
        {
            Fixed32 result;
            LengthSquared (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 Length (Quaternion quaternion)
        {
            Fixed32 result;
            Length (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean IsUnit (Quaternion quaternion)
        {
            Boolean result;
            IsUnit (ref quaternion, out result);
            return result;
        }


        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Conjugate (Quaternion quaternion)
        {
            Quaternion result;
            Conjugate (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Inverse (Quaternion quaternion)
        {
            Quaternion result;
            Inverse (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 Dot (
            Quaternion quaternion1,
            Quaternion quaternion2)
        {
            Fixed32 result;
            Dot (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Concatenate (
            Quaternion quaternion1,
            Quaternion quaternion2)
        {
            Quaternion result;
            Concatenate (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Normalise (Quaternion quaternion)
        {
            Quaternion result;
            Normalise (ref quaternion, out result);
            return result;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean Equals (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Boolean result;
            Equals (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator == (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Boolean result;
            Equals (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator != (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Boolean result;
            Equals (ref quaternion1, ref quaternion2, out result);
            return !result;
        }

        // Variant Addition Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Add (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Add (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion operator + (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Add (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        // Variant Subtraction Operators //-----------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Subtract (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Subtract (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion operator - (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Subtract (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        // Variant Negation Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Negate (Quaternion quaternion)
        {
            Quaternion result;
            Negate (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion operator - (Quaternion quaternion)
        {
            Quaternion result;
            Negate (ref quaternion, out result);
            return result;
        }

        // Variant Multiplication Operators //--------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Multiply (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Multiply (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion operator * (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Multiply (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Slerp (
            Quaternion quaternion1,
            Quaternion quaternion2,
            Fixed32 amount)
        {
            Quaternion result;
            Slerp (ref quaternion1, ref quaternion2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Lerp (
            Quaternion quaternion1,
            Quaternion quaternion2,
            Fixed32 amount)
        {
            Quaternion result;
            Lerp (ref quaternion1, ref quaternion2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Fixed32 LengthSquared ()
        {
            Fixed32 result;
            LengthSquared (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Fixed32 Length ()
        {
            Fixed32 result;
            Length (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public void Normalise ()
        {
            Normalise (ref this, out this);
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Boolean IsUnit()
        {
            Boolean result;
            IsUnit (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public void Conjugate ()
        {
            Conjugate (ref this, out this);
        }

#endif
    }
    /// <summary>
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
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return String.Format ("{{X:{0} Y:{1}}}",
                X.ToString (), Y.ToString ());
        }

        /// <summary>
        /// Gets the hash code of the Vector2 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return X.GetHashCode ()
                 ^ Y.GetHashCode ().ShiftAndWrap (2);
        }

        /// <summary>
        /// Determines whether or not this Vector2 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            return (obj is Vector2)
                ? this.Equals ((Vector2) obj)
                : false;
        }

        #region IEquatable<Vector2>

        /// <summary>
        /// Determines whether or not this Vector2 object is equal to another
        /// Vector2 object.
        /// </summary>
        public Boolean Equals (Vector2 other)
        {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        #endregion

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

            result = Maths.Sqrt (lengthSquared);
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

            Fixed32 epsilon; Maths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Fixed32.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 one = 1;
            Fixed32 multiplier = one / Maths.Sqrt (lengthSquared);

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
            Boolean normalIsUnit;
            Vector2.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
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
            Vector2 m;
            Vector2.Multiply (ref normal, ref twoDot, out m);
            Vector2.Subtract (ref vector, ref m, out result);
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector2 vector, ref Matrix44 matrix, out Vector2 result)
        {
            Fixed32 x =
                (vector.X * matrix.R0C0) +
                (vector.Y * matrix.R1C0) +
                matrix.R3C0;

            Fixed32 y =
                (vector.X * matrix.R0C1) +
                (vector.Y * matrix.R1C1) +
                matrix.R3C1;

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

            Fixed32 i = rotation.I;
            Fixed32 j = rotation.J;
            Fixed32 k = rotation.K;
            Fixed32 u = rotation.U;

            Fixed32 ii = i * i;
            Fixed32 jj = j * j;
            Fixed32 kk = k * k;

            Fixed32 uk = u * k;
            Fixed32 ij = i * j;

            result.X =
                + vector.X
                - (two * vector.X  * (jj + kk))
                + (two * vector.Y  * (ij - uk));

            result.Y =
                + vector.Y
                + (two * vector.X * (ij + uk))
                - (two * vector.Y * (ii + kk));
        }

        /// <summary>
        /// Transforms a normalised Vector2 by the specified Matrix.
        /// </summary>
        public static void TransformNormal (
            ref Vector2 normal, ref Matrix44 matrix, out Vector2 result)
        {
            Boolean normalIsUnit;
            Vector2.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Fixed32 x = (normal.X * matrix.R0C0) + (normal.Y * matrix.R1C0);
            Fixed32 y = (normal.X * matrix.R0C1) + (normal.Y * matrix.R1C1);

            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Calculates the length of the Vector2.
        /// </summary>
        public static void Length (
            ref Vector2 vector, out Fixed32 result)
        {
            Fixed32 lengthSquared =
                (vector.X * vector.X) + (vector.Y * vector.Y);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the length of the Vector2 squared.
        /// </summary>
        public static void LengthSquared (
            ref Vector2 vector, out Fixed32 result)
        {
            result = (vector.X * vector.X) + (vector.Y * vector.Y);
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not two Vector2 objects are equal.
        /// </summary>
        public static void Equals (
            ref Vector2 vector1, ref Vector2 vector2, out Boolean result)
        {
            result = (vector1.X == vector2.X) && (vector1.Y == vector2.Y);
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector2 objects.
        /// </summary>
        public static void Add (
            ref Vector2 vector1, ref Vector2 vector2, out Vector2 result)
        {
            result.X = vector1.X + vector2.X;
            result.Y = vector1.Y + vector2.Y;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector2 objects.
        /// </summary>
        public static void Subtract (
            ref Vector2 vector1, ref Vector2 vector2, out Vector2 result)
        {
            result.X = vector1.X - vector2.X;
            result.Y = vector1.Y - vector2.Y;
        }

        // Negation Operators //----------------------------------------------//

        /// <summary>
        /// Performs negation of a Vector2 object.
        /// </summary>
        public static void Negate (ref Vector2 vector, out Vector2 result)
        {
            result.X = -vector.X;
            result.Y = -vector.Y;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector2 objects.
        /// </summary>
        public static void Multiply (
            ref Vector2 vector1, ref Vector2 vector2, out Vector2 result)
        {
            result.X = vector1.X * vector2.X;
            result.Y = vector1.Y * vector2.Y;
        }

        /// <summary>
        /// Performs multiplication of a Vector2 object and a Fixed32
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Vector2 vector, ref Fixed32 scaleFactor, out Vector2 result)
        {
            result.X = vector.X * scaleFactor;
            result.Y = vector.Y * scaleFactor;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector2 objects.
        /// </summary>
        public static void Divide (
            ref Vector2 vector1, ref Vector2 vector2, out Vector2 result)
        {
            result.X = vector1.X / vector2.X;
            result.Y = vector1.Y / vector2.Y;
        }

        /// <summary>
        /// Performs division of a Vector2 object and a Fixed32 precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Vector2 vector1, ref Fixed32 divider, out Vector2 result)
        {
            Fixed32 one = 1;
            Fixed32 num = one / divider;
            result.X = vector1.X * num;
            result.Y = vector1.Y * num;
        }

        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector2 vector1,
            ref Vector2 vector2,
            ref Fixed32 amount,
            out Vector2 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 two = 2;
            Fixed32 three = 3;

            amount = (amount * amount) * (three - (two * amount));

            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
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
            ref Vector2 vector1,
            ref Vector2 vector2,
            ref Vector2 vector3,
            ref Vector2 vector4,
            ref Fixed32 amount,
            out Vector2 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 two = 2;
            Fixed32 three = 3;
            Fixed32 four = 4;
            Fixed32 five = 5;
            Fixed32 half; Maths.Half(out half);

            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;

            ///////
            // X //
            ///////

            // (2 * P2)
            result.X = (two * vector2.X);

            // (-P1 + P3) * t
            result.X += (
                    - vector1.X
                    + vector3.X
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.X += (
                    + (two * vector1.X)
                    - (five * vector2.X)
                    + (four * vector3.X)
                    - (vector4.X)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.X += (
                    - (vector1.X)
                    + (three * vector2.X)
                    - (three * vector3.X)
                    + (vector4.X)
                ) * cubed;

            // 0.5
            result.X *= half;

            ///////
            // Y //
            ///////

            // (2 * P2)
            result.Y = (two * vector2.Y);

            // (-P1 + P3) * t
            result.Y += (
                    - vector1.Y
                    + vector3.Y
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.Y += (
                    + (two * vector1.Y)
                    - (five * vector2.Y)
                    + (four * vector3.Y)
                    - (vector4.Y)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.Y += (
                    - (vector1.Y)
                    + (three * vector2.Y)
                    - (three * vector3.Y)
                    + (vector4.Y)
                ) * cubed;

            // 0.5
            result.Y *= half;
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector2 vector1,
            ref Vector2 tangent1,
            ref Vector2 vector2,
            ref Vector2 tangent2,
            ref Fixed32 amount,
            out Vector2 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector2.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector2.IsUnit (ref tangent2, out tangent2IsUnit);
            if( !tangent1IsUnit || !tangent2IsUnit )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 two = 2;
            Fixed32 three = 3;

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
            ref Fixed32 amount,
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

        /// <summary>
        /// Detemines whether or not the Vector2 is of unit length.
        /// </summary>
        public static void IsUnit (ref Vector2 vector, out Boolean result)
        {
            Fixed32 one = 1;
            result = Maths.IsZero(
                one - vector.X * vector.X - vector.Y * vector.Y);
        }

#if (VARIANTS_ENABLED)

        // Variant Maths //---------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 Distance (
            Vector2 vector1, Vector2 vector2)
        {
            Fixed32 result;
            Distance (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 DistanceSquared (
            Vector2 vector1, Vector2 vector2)
        {
            Fixed32 result;
            DistanceSquared (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 Dot (
            Vector2 vector1, Vector2 vector2)
        {
            Fixed32 result;
            Dot (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Normalise (Vector2 vector)
        {
            Vector2 result;
            Normalise (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Reflect (
            Vector2 vector, Vector2 normal)
        {
            Vector2 result;
            Reflect (ref vector, ref normal, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Transform (
            Vector2 vector, Matrix44 matrix)
        {
            Vector2 result;
            Transform (ref vector, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Transform (
            Vector2 vector, Quaternion rotation)
        {
            Vector2 result;
            Transform (ref vector, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 TransformNormal (
            Vector2 normal, Matrix44 matrix)
        {
            Vector2 result;
            TransformNormal (ref normal, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 Length (Vector2 vector)
        {
            Fixed32 result;
            Length (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 LengthSquared (Vector2 vector)
        {
            Fixed32 result;
            LengthSquared (ref vector, out result);
            return result;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean Equals (Vector2 vector1, Vector2 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator == (Vector2 vector1, Vector2 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator != (Vector2 vector1, Vector2 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return !result;
        }

        // Variant Addition Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Add (
            Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Add (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator + (Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Add (ref vector1, ref vector2, out result);
            return result;
        }

        // Variant Subtraction Operators //-----------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Subtract (
            Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Subtract (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator - (Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Subtract (ref vector1, ref vector2, out result);
            return result;
        }

        // Variant Negation Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Negate (Vector2 vector)
        {
            Vector2 result;
            Negate (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator - (Vector2 vector)
        {
            Vector2 result;
            Negate (ref vector, out result);
            return result;
        }

        // Variant Multiplication Operators //--------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Multiply (
            Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Multiply (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Multiply (
            Vector2 vector, Fixed32 scaleFactor)
        {
            Vector2 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator * (
            Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Multiply (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator * (
            Vector2 vector, Fixed32 scaleFactor)
        {
            Vector2 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator * (
            Fixed32 scaleFactor, Vector2 vector)
        {
            Vector2 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        // Variant Division Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Divide (
            Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Divide (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Divide (
            Vector2 vector1, Fixed32 divider)
        {
            Vector2 result;
            Divide (ref vector1, ref divider, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator / (Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Divide (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator / (Vector2 vector1, Fixed32 divider)
        {
            Vector2 result;
            Divide (ref vector1, ref divider, out result);
            return result;
        }

        // Variant Splines //-------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 SmoothStep (
            Vector2 vector1,
            Vector2 vector2,
            Fixed32 amount)
        {
            Vector2 result;
            SmoothStep (ref vector1, ref vector2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 CatmullRom (
            Vector2 vector1,
            Vector2 vector2,
            Vector2 vector3,
            Vector2 vector4,
            Fixed32 amount)
        {
            Vector2 result;
            CatmullRom (
                ref vector1, ref vector2, ref vector3, ref vector4,
                ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
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
                ref amount, out result);
            return result;
        }

        // Variant Utilities //-----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Min (
            Vector2 vector1,
            Vector2 vector2)
        {
            Vector2 result;
            Min (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Max (
            Vector2 vector1,
            Vector2 vector2)
        {
            Vector2 result;
            Max (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Clamp (
            Vector2 vector,
            Vector2 min,
            Vector2 max)
        {
            Vector2 result;
            Clamp (ref vector, ref min, ref max, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Lerp (
            Vector2 vector1,
            Vector2 vector2,
            Fixed32 amount)
        {
            Vector2 result;
            Lerp (ref vector1, ref vector2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean IsUnit (Vector2 vector)
        {
            Boolean result;
            IsUnit (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Fixed32 Length ()
        {
            Fixed32 result;
            Length (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Fixed32 LengthSquared ()
        {
            Fixed32 result;
            LengthSquared (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public void Normalise ()
        {
            Normalise (ref this, out this);
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Boolean IsUnit()
        {
            Boolean result;
            IsUnit (ref this, out result);
            return result;
        }

#endif

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
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format ("{{X:{0} Y:{1} Z:{2}}}",
                X.ToString (), Y.ToString (), Z.ToString ());
        }

        /// <summary>
        /// Gets the hash code of the Vector3 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return X.GetHashCode ()
                 ^ Y.GetHashCode ().ShiftAndWrap (2)
                 ^ Z.GetHashCode ().ShiftAndWrap (4);
        }

        /// <summary>
        /// Determines whether or not this Vector3 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            return (obj is Vector3)
                ? this.Equals ((Vector3) obj)
                : false;
        }

        #region IEquatable<Vector3>

        /// <summary>
        /// Determines whether or not this Vector3 object is equal to another
        /// Vector3 object.
        /// </summary>
        public Boolean Equals (Vector3 other)
        {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        #endregion

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
            ref Vector3 vector1, ref Vector3 vector2, out Fixed32 result)
        {
            Fixed32 dx = vector1.X - vector2.X;
            Fixed32 dy = vector1.Y - vector2.Y;
            Fixed32 dz = vector1.Z - vector2.Z;

            Fixed32 lengthSquared = (dx * dx) + (dy * dy) + (dz * dz);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector3 vector1, ref Vector3 vector2,
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
            ref Vector3 vector1, ref Vector3 vector2, out Fixed32 result)
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

            Fixed32 epsilon; Maths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Fixed32.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 one = 1;
            Fixed32 multiplier = one / Maths.Sqrt (lengthSquared);

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
        }

        /// <summary>
        /// Calculates the cross product of two vectors.
        /// </summary>
        public static void Cross (
            ref Vector3 vector1, ref Vector3 vector2,
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
            ref Vector3 vector, ref Vector3 normal, out Vector3 result)
        {
            Boolean normalIsUnit;
            Vector3.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
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
            ref Vector3 vector, ref Matrix44 matrix, out Vector3 result)
        {
            Fixed32 x =
                (vector.X * matrix.R0C0) +
                (vector.Y * matrix.R1C0) +
                (vector.Z * matrix.R2C0) + matrix.R3C0;

            Fixed32 y =
                (vector.X * matrix.R0C1) +
                (vector.Y * matrix.R1C1) +
                (vector.Z * matrix.R2C1) + matrix.R3C1;

            Fixed32 z =
                (vector.X * matrix.R0C2) +
                (vector.Y * matrix.R1C2) +
                (vector.Z * matrix.R2C2) + matrix.R3C2;

            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Transforms a vector by a specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector3 vector, ref Quaternion rotation, out Vector3 result)
        {
            Fixed32 two = 2;

            Fixed32 i = rotation.I;
            Fixed32 j = rotation.J;
            Fixed32 k = rotation.K;
            Fixed32 u = rotation.U;

            Fixed32 ii = i * i;
            Fixed32 jj = j * j;
            Fixed32 kk = k * k;

            Fixed32 ui = u * i;
            Fixed32 uj = u * j;
            Fixed32 uk = u * k;
            Fixed32 ij = i * j;
            Fixed32 ik = i * k;
            Fixed32 jk = j * k;

            result.X =
                + vector.X
                - (two * vector.X * (jj + kk))
                + (two * vector.Y * (ij - uk))
                + (two * vector.Z * (ik + uj));

            result.Y =
                + vector.Y
                + (two * vector.X * (ij + uk))
                - (two * vector.Y * (ii + kk))
                + (two * vector.Z * (jk - ui));

            result.Z =
                + vector.Z
                + (two * vector.X * (ik - uj))
                + (two * vector.Y * (jk + ui))
                - (two * vector.Z * (ii + jj));
        }

        /// <summary>
        /// Transforms a normalised Vector3 by a Matrix44.
        /// </summary>
        public static void TransformNormal (
            ref Vector3 normal, ref Matrix44 matrix, out Vector3 result)
        {
            Boolean normalIsUnit;
            Vector3.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Fixed32 x =
                (normal.X * matrix.R0C0) +
                (normal.Y * matrix.R1C0) +
                (normal.Z * matrix.R2C0);

            Fixed32 y =
                (normal.X * matrix.R0C1) +
                (normal.Y * matrix.R1C1) +
                (normal.Z * matrix.R2C1);

            Fixed32 z =
                (normal.X * matrix.R0C2) +
                (normal.Y * matrix.R1C2) +
                (normal.Z * matrix.R2C2);

            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Calculates the length of the Vector3.
        /// </summary>
        public static void Length (
            ref Vector3 vector, out Fixed32 result)
        {
            Fixed32 lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the length of the Vector3 squared.
        /// </summary>
        public static void LengthSquared (
            ref Vector3 vector, out Fixed32 result)
        {
            result =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z);
        }
        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not two Vector3 objects are equal.
        /// </summary>
        public static void Equals (
            ref Vector3 value1, ref Vector3 vector2, out Boolean result)
        {
            result =
                (value1.X == vector2.X) &&
                (value1.Y == vector2.Y) &&
                (value1.Z == vector2.Z);
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector3 objects.
        /// </summary>
        public static void Add (
            ref Vector3 value1, ref Vector3 vector2, out Vector3 result)
        {
            result.X = value1.X + vector2.X;
            result.Y = value1.Y + vector2.Y;
            result.Z = value1.Z + vector2.Z;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector3 objects.
        /// </summary>
        public static void Subtract (
            ref Vector3 value1, ref Vector3 vector2, out Vector3 result)
        {
            result.X = value1.X - vector2.X;
            result.Y = value1.Y - vector2.Y;
            result.Z = value1.Z - vector2.Z;
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

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector3 objects.
        /// </summary>
        public static void Multiply (
            ref Vector3 value1, ref Vector3 vector2, out Vector3 result)
        {
            result.X = value1.X * vector2.X;
            result.Y = value1.Y * vector2.Y;
            result.Z = value1.Z * vector2.Z;
        }

        /// <summary>
        /// Performs multiplication of a Vector3 object and a Fixed32
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Vector3 value1, ref Fixed32 scaleFactor, out Vector3 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector3 objects.
        /// </summary>
        public static void Divide (
            ref Vector3 value1, ref Vector3 vector2, out Vector3 result)
        {
            result.X = value1.X / vector2.X;
            result.Y = value1.Y / vector2.Y;
            result.Z = value1.Z / vector2.Z;
        }

        /// <summary>
        /// Performs division of a Vector3 object and a Fixed32 precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Vector3 value1, ref Fixed32 vector2, out Vector3 result)
        {
            Fixed32 one = 1;
            Fixed32 num = one / vector2;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
        }

        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector3 vector1,
            ref Vector3 vector2,
            ref Fixed32 amount,
            out Vector3 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 two = 2;
            Fixed32 three = 3;

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
            ref Fixed32 amount,
            out Vector3 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 two = 2;
            Fixed32 three = 3;
            Fixed32 four = 4;
            Fixed32 five = 5;
            Fixed32 half; Maths.Half(out half);

            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;

            ///////
            // X //
            ///////

            // (2 * P2)
            result.X = (two * vector2.X);

            // (-P1 + P3) * t
            result.X += (
                    - vector1.X
                    + vector3.X
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.X += (
                    + (two * vector1.X)
                    - (five * vector2.X)
                    + (four * vector3.X)
                    - (vector4.X)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.X += (
                    - (vector1.X)
                    + (three * vector2.X)
                    - (three * vector3.X)
                    + (vector4.X)
                ) * cubed;

            // 0.5
            result.X *= half;

            ///////
            // Y //
            ///////

            // (2 * P2)
            result.Y = (two * vector2.Y);

            // (-P1 + P3) * t
            result.Y += (
                    - vector1.Y
                    + vector3.Y
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.Y += (
                    + (two * vector1.Y)
                    - (five * vector2.Y)
                    + (four * vector3.Y)
                    - (vector4.Y)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.Y += (
                    - (vector1.Y)
                    + (three * vector2.Y)
                    - (three * vector3.Y)
                    + (vector4.Y)
                ) * cubed;

            // 0.5
            result.Y *= half;

            ///////
            // Z //
            ///////

            // (2 * P2)
            result.Z = (two * vector2.Z);

            // (-P1 + P3) * t
            result.Z += (
                    - vector1.Z
                    + vector3.Z
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.Z += (
                    + (two * vector1.Z)
                    - (five * vector2.Z)
                    + (four * vector3.Z)
                    - (vector4.Z)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.Z += (
                    - (vector1.Z)
                    + (three * vector2.Z)
                    - (three * vector3.Z)
                    + (vector4.Z)
                ) * cubed;

            // 0.5
            result.Z *= half;
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector3 vector1,
            ref Vector3 tangent1,
            ref Vector3 vector2,
            ref Vector3 tangent2,
            ref Fixed32 amount,
            out Vector3 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector3.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector3.IsUnit (ref tangent2, out tangent2IsUnit);
            if( !tangent1IsUnit || !tangent2IsUnit )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 two = 2;
            Fixed32 three = 3;

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
            ref Fixed32 amount,
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

        /// <summary>
        /// Detemines whether or not the Vector3 is of unit length.
        /// </summary>
        public static void IsUnit (ref Vector3 vector, out Boolean result)
        {
            Fixed32 one = 1;
            result = Maths.IsZero(
                one
                - vector.X * vector.X
                - vector.Y * vector.Y
                - vector.Z * vector.Z);
        }
#if (VARIANTS_ENABLED)

        // Variant Maths //---------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 Distance (
            Vector3 vector1, Vector3 vector2)
        {
            Fixed32 result;
            Distance (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 DistanceSquared (
            Vector3 vector1, Vector3 vector2)
        {
            Fixed32 result;
            DistanceSquared (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 Dot (
            Vector3 vector1, Vector3 vector2)
        {
            Fixed32 result;
            Dot (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Normalise (Vector3 vector)
        {
            Vector3 result;
            Normalise (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Reflect (
            Vector3 vector, Vector3 normal)
        {
            Vector3 result;
            Reflect (ref vector, ref normal, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Transform (
            Vector3 vector, Matrix44 matrix)
        {
            Vector3 result;
            Transform (ref vector, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Transform (
            Vector3 vector, Quaternion rotation)
        {
            Vector3 result;
            Transform (ref vector, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 TransformNormal (
            Vector3 normal, Matrix44 matrix)
        {
            Vector3 result;
            TransformNormal (ref normal, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 Length (Vector3 vector)
        {
            Fixed32 result;
            Length (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 LengthSquared (Vector3 vector)
        {
            Fixed32 result;
            LengthSquared (ref vector, out result);
            return result;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean Equals (Vector3 vector1, Vector3 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator == (Vector3 vector1, Vector3 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator != (Vector3 vector1, Vector3 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return !result;
        }

        // Variant Addition Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Add (
            Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Add (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator + (Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Add (ref vector1, ref vector2, out result);
            return result;
        }

        // Variant Subtraction Operators //-----------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Subtract (
            Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Subtract (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator - (Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Subtract (ref vector1, ref vector2, out result);
            return result;
        }

        // Variant Negation Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Negate (Vector3 vector)
        {
            Vector3 result;
            Negate (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator - (Vector3 vector)
        {
            Vector3 result;
            Negate (ref vector, out result);
            return result;
        }

        // Variant Multiplication Operators //--------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Multiply (
            Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Multiply (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Multiply (
            Vector3 vector, Fixed32 scaleFactor)
        {
            Vector3 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator * (
            Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Multiply (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator * (
            Vector3 vector, Fixed32 scaleFactor)
        {
            Vector3 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator * (
            Fixed32 scaleFactor, Vector3 vector)
        {
            Vector3 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        // Variant Division Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Divide (
            Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Divide (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Divide (
            Vector3 vector1, Fixed32 divider)
        {
            Vector3 result;
            Divide (ref vector1, ref divider, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator / (Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Divide (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator / (Vector3 vector1, Fixed32 divider)
        {
            Vector3 result;
            Divide (ref vector1, ref divider, out result);
            return result;
        }

        // Variant Splines //-------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 SmoothStep (
            Vector3 vector1,
            Vector3 vector2,
            Fixed32 amount)
        {
            Vector3 result;
            SmoothStep (ref vector1, ref vector2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 CatmullRom (
            Vector3 vector1,
            Vector3 vector2,
            Vector3 vector3,
            Vector3 vector4,
            Fixed32 amount)
        {
            Vector3 result;
            CatmullRom (
                ref vector1, ref vector2, ref vector3, ref vector4,
                ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Hermite (
            Vector3 vector1,
            Vector3 tangent1,
            Vector3 vector2,
            Vector3 tangent2,
            Fixed32 amount)
        {
            Vector3 result;
            Hermite (
                ref vector1, ref tangent1,
                ref vector2, ref tangent2,
                ref amount, out result);
            return result;
        }

        // Variant Utilities //-----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Min (
            Vector3 vector1,
            Vector3 vector2)
        {
            Vector3 result;
            Min (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Max (
            Vector3 vector1,
            Vector3 vector2)
        {
            Vector3 result;
            Max (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Clamp (
            Vector3 vector,
            Vector3 min,
            Vector3 max)
        {
            Vector3 result;
            Clamp (ref vector, ref min, ref max, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Lerp (
            Vector3 vector1,
            Vector3 vector2,
            Fixed32 amount)
        {
            Vector3 result;
            Lerp (ref vector1, ref vector2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean IsUnit (Vector3 vector)
        {
            Boolean result;
            IsUnit (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Fixed32 Length ()
        {
            Fixed32 result;
            Length (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Fixed32 LengthSquared ()
        {
            Fixed32 result;
            LengthSquared (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public void Normalise ()
        {
            Normalise (ref this, out this);
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Boolean IsUnit()
        {
            Boolean result;
            IsUnit (ref this, out result);
            return result;
        }

#endif
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
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}",
                X.ToString (), Y.ToString (), Z.ToString (), W.ToString ());
        }

        /// <summary>
        /// Gets the hash code of the Vector4 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return X.GetHashCode ()
                 ^ Y.GetHashCode ().ShiftAndWrap (2)
                 ^ Z.GetHashCode ().ShiftAndWrap (4)
                 ^ W.GetHashCode ().ShiftAndWrap (6);
        }

        /// <summary>
        /// Determines whether or not this Vector4 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            return (obj is Vector4)
                ? this.Equals ((Vector4)obj)
                : false;
        }

        #region IEquatable<Vector4>

        /// <summary>
        /// Determines whether or not this Vector4 object is equal to another
        /// Vector4 object.
        /// </summary>
        public Boolean Equals (Vector4 other)
        {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        #endregion

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
            ref Vector4 vector1, ref Vector4 vector2, out Fixed32 result)
        {
            Fixed32 dx = vector1.X - vector2.X;
            Fixed32 dy = vector1.Y - vector2.Y;
            Fixed32 dz = vector1.Z - vector2.Z;
            Fixed32 dw = vector1.W - vector2.W;

            Fixed32 lengthSquared =
                (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector4 vector1, ref Vector4 vector2, out Fixed32 result)
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
            ref Vector4 vector1, ref Vector4 vector2, out Fixed32 result)
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
            ref Vector4 vector, out Vector4 result)
        {
            Fixed32 lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z) +
                (vector.W * vector.W);

            Fixed32 epsilon; Maths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Fixed32.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 one = 1;
            Fixed32 multiplier = one / (Maths.Sqrt (lengthSquared));

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
            result.W = vector.W * multiplier;
        }

        /// <summary>
        /// Transforms a Vector4 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector4 vector, ref Matrix44 matrix, out Vector4 result)
        {
            Fixed32 x =
                (vector.X * matrix.R0C0) +
                (vector.Y * matrix.R1C0) +
                (vector.Z * matrix.R2C0) +
                (vector.W * matrix.R3C0);

            Fixed32 y =
                (vector.X * matrix.R0C1) +
                (vector.Y * matrix.R1C1) +
                (vector.Z * matrix.R2C1) +
                (vector.W * matrix.R3C1);

            Fixed32 z =
                (vector.X * matrix.R0C2) +
                (vector.Y * matrix.R1C2) +
                (vector.Z * matrix.R2C2) +
                (vector.W * matrix.R3C2);

            Fixed32 w =
                (vector.X * matrix.R0C3) +
                (vector.Y * matrix.R1C3) +
                (vector.Z * matrix.R2C3) +
                (vector.W * matrix.R3C3);

            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }

        /// <summary>
        /// Transforms a Vector4 by the specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector4 vector, ref Quaternion rotation, out Vector4 result)
        {
            Fixed32 two = 2;

            Fixed32 i = rotation.I;
            Fixed32 j = rotation.J;
            Fixed32 k = rotation.K;
            Fixed32 u = rotation.U;

            Fixed32 ii = i * i;
            Fixed32 jj = j * j;
            Fixed32 kk = k * k;

            Fixed32 ui = u * i;
            Fixed32 uj = u * j;
            Fixed32 uk = u * k;
            Fixed32 ij = i * j;
            Fixed32 ik = i * k;
            Fixed32 jk = j * k;

            result.X =
                + vector.X
                - (two * vector.X * (jj + kk))
                + (two * vector.Y * (ij - uk))
                + (two * vector.Z * (ik + uj));

            result.Y =
                + vector.Y
                + (two * vector.X * (ij + uk))
                - (two * vector.Y * (ii + kk))
                + (two * vector.Z * (jk - ui));

            result.Z =
                + vector.Z
                + (two * vector.X * (ik - uj))
                + (two * vector.Y * (jk + ui))
                - (two * vector.Z * (ii + jj));

            result.W = vector.W;
        }

        /// <summary>
        /// Transforms a normalised Vector4 by a Matrix44.
        /// </summary>
        public static void TransformNormal (
            ref Vector4 normal, ref Matrix44 matrix, out Vector4 result)
        {
            Boolean normalIsUnit;
            Vector4.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Fixed32 x =
                (normal.X * matrix.R0C0) + (normal.Y * matrix.R1C0) +
                (normal.Z * matrix.R2C0) + (normal.W * matrix.R3C0);

            Fixed32 y =
                (normal.X * matrix.R0C1) + (normal.Y * matrix.R1C1) +
                (normal.Z * matrix.R2C1) + (normal.W * matrix.R3C1);

            Fixed32 z =
                (normal.X * matrix.R0C2) + (normal.Y * matrix.R1C2) +
                (normal.Z * matrix.R2C2) + (normal.W * matrix.R3C2);

            Fixed32 w =
                (normal.X * matrix.R0C3) + (normal.Y * matrix.R1C3) +
                (normal.Z * matrix.R2C3) + (normal.W * matrix.R3C3);

            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }

        /// <summary>
        /// Calculates the length of the Vector4.
        /// </summary>
        public static void Length (ref Vector4 vector, out Fixed32 result)
        {
            Fixed32 lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z) +
                (vector.W * vector.W);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the length of the Vector4 squared.
        /// </summary>
        public static void LengthSquared (
            ref Vector4 vector, out Fixed32 result)
        {
            result =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z) +
                (vector.W * vector.W);
        }
        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not two Vector4 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static void Equals (
            ref Vector4 value1, ref Vector4 value2, out Boolean result)
        {
            result =
                (value1.X == value2.X) &&
                (value1.Y == value2.Y) &&
                (value1.Z == value2.Z) &&
                (value1.W == value2.W);
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector4 objects.
        /// </summary>
        public static void Add (
            ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
            result.W = value1.W + value2.W;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector4 objects.
        /// </summary>
        public static void Subtract (
            ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
            result.W = value1.W - value2.W;
        }

        // Negation Operators //----------------------------------------------//

        /// <summary>
        /// Performs negation of a Vector4 object.
        /// </summary>
        public static void Negate (
            ref Vector4 value, out Vector4 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector4 objects.
        /// </summary>
        public static void Multiply (
            ref Vector4 value1, ref Vector4 value2, out Vector4 result)
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
        public static void Multiply (
            ref Vector4 value1, ref Fixed32 scaleFactor, out Vector4 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
            result.W = value1.W * scaleFactor;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector4 objects.
        /// </summary>
        public static void Divide (
            ref Vector4 value1, ref Vector4 value2, out Vector4 result)
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
        public static void Divide (
            ref Vector4 value1, ref Fixed32 divider, out Vector4 result)
        {
            Fixed32 one = 1;
            Fixed32 num = one / divider;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
            result.W = value1.W * num;
        }

        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector4 vector1,
            ref Vector4 vector2,
            ref Fixed32 amount,
            out Vector4 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 two = 2;
            Fixed32 three = 3;

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
            ref Fixed32 amount,
            out Vector4 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 two = 2;
            Fixed32 three = 3;
            Fixed32 four = 4;
            Fixed32 five = 5;
            Fixed32 half; Maths.Half(out half);

            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;

            ///////
            // X //
            ///////

            // (2 * P2)
            result.X = (two * vector2.X);

            // (-P1 + P3) * t
            result.X += (
                    - vector1.X
                    + vector3.X
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.X += (
                    + (two * vector1.X)
                    - (five * vector2.X)
                    + (four * vector3.X)
                    - (vector4.X)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.X += (
                    - (vector1.X)
                    + (three * vector2.X)
                    - (three * vector3.X)
                    + (vector4.X)
                ) * cubed;

            // 0.5
            result.X *= half;

            ///////
            // Y //
            ///////

            // (2 * P2)
            result.Y = (two * vector2.Y);

            // (-P1 + P3) * t
            result.Y += (
                    - vector1.Y
                    + vector3.Y
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.Y += (
                    + (two * vector1.Y)
                    - (five * vector2.Y)
                    + (four * vector3.Y)
                    - (vector4.Y)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.Y += (
                    - (vector1.Y)
                    + (three * vector2.Y)
                    - (three * vector3.Y)
                    + (vector4.Y)
                ) * cubed;

            // 0.5
            result.Y *= half;

            ///////
            // Z //
            ///////

            // (2 * P2)
            result.Z = (two * vector2.Z);

            // (-P1 + P3) * t
            result.Z += (
                    - vector1.Z
                    + vector3.Z
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.Z += (
                    + (two * vector1.Z)
                    - (five * vector2.Z)
                    + (four * vector3.Z)
                    - (vector4.Z)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.Z += (
                    - (vector1.Z)
                    + (three * vector2.Z)
                    - (three * vector3.Z)
                    + (vector4.Z)
                ) * cubed;

            // 0.5
            result.Z *= half;

            ///////
            // W //
            ///////

            // (2 * P2)
            result.W = (two * vector2.W);

            // (-P1 + P3) * t
            result.W += (
                    - vector1.W
                    + vector3.W
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.W += (
                    + (two * vector1.W)
                    - (five * vector2.W)
                    + (four * vector3.W)
                    - (vector4.W)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.W += (
                    - (vector1.W)
                    + (three * vector2.W)
                    - (three * vector3.W)
                    + (vector4.W)
                ) * cubed;

            // 0.5
            result.W *= half;
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector4 vector1,
            ref Vector4 tangent1,
            ref Vector4 vector2,
            ref Vector4 tangent2,
            ref Fixed32 amount,
            out Vector4 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector4.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector4.IsUnit (ref tangent2, out tangent2IsUnit);
            if( !tangent1IsUnit || !tangent2IsUnit )
            {
                throw new ArgumentOutOfRangeException();
            }

            Fixed32 two = 2;
            Fixed32 three = 3;

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
            ref Fixed32 amount,
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

        /// <summary>
        /// Detemines whether or not the Vector4 is of unit length.
        /// </summary>
        public static void IsUnit (ref Vector4 vector, out Boolean result)
        {
            Fixed32 one = 1;
            result = Maths.IsZero(
                one
                - vector.X * vector.X
                - vector.Y * vector.Y
                - vector.Z * vector.Z
                - vector.W * vector.W);
        }

#if (VARIANTS_ENABLED)

        // Variant Maths //---------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 Distance (
            Vector4 vector1, Vector4 vector2)
        {
            Fixed32 result;
            Distance (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 DistanceSquared (
            Vector4 vector1, Vector4 vector2)
        {
            Fixed32 result;
            DistanceSquared (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 Dot (
            Vector4 vector1, Vector4 vector2)
        {
            Fixed32 result;
            Dot (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Normalise (Vector4 vector)
        {
            Vector4 result;
            Normalise (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Transform (
            Vector4 vector, Matrix44 matrix)
        {
            Vector4 result;
            Transform (ref vector, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Transform (
            Vector4 vector, Quaternion rotation)
        {
            Vector4 result;
            Transform (ref vector, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 TransformNormal (
            Vector4 normal, Matrix44 matrix)
        {
            Vector4 result;
            TransformNormal (ref normal, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 Length (Vector4 vector)
        {
            Fixed32 result;
            Length (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Fixed32 LengthSquared (Vector4 vector)
        {
            Fixed32 result;
            LengthSquared (ref vector, out result);
            return result;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean Equals (Vector4 vector1, Vector4 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator == (Vector4 vector1, Vector4 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator != (Vector4 vector1, Vector4 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return !result;
        }

        // Variant Addition Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Add (
            Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Add (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator + (Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Add (ref vector1, ref vector2, out result);
            return result;
        }

        // Variant Subtraction Operators //-----------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Subtract (
            Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Subtract (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator - (Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Subtract (ref vector1, ref vector2, out result);
            return result;
        }

        // Variant Negation Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Negate (Vector4 vector)
        {
            Vector4 result;
            Negate (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator - (Vector4 vector)
        {
            Vector4 result;
            Negate (ref vector, out result);
            return result;
        }

        // Variant Multiplication Operators //--------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Multiply (
            Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Multiply (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Multiply (
            Vector4 vector, Fixed32 scaleFactor)
        {
            Vector4 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator * (
            Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Multiply (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator * (
            Vector4 vector, Fixed32 scaleFactor)
        {
            Vector4 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator * (
            Fixed32 scaleFactor, Vector4 vector)
        {
            Vector4 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        // Variant Division Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Divide (
            Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Divide (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Divide (
            Vector4 vector1, Fixed32 divider)
        {
            Vector4 result;
            Divide (ref vector1, ref divider, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator / (Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Divide (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator / (Vector4 vector1, Fixed32 divider)
        {
            Vector4 result;
            Divide (ref vector1, ref divider, out result);
            return result;
        }

        // Variant Splines //-------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 SmoothStep (
            Vector4 vector1,
            Vector4 vector2,
            Fixed32 amount)
        {
            Vector4 result;
            SmoothStep (ref vector1, ref vector2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 CatmullRom (
            Vector4 vector1,
            Vector4 vector2,
            Vector4 vector3,
            Vector4 vector4,
            Fixed32 amount)
        {
            Vector4 result;
            CatmullRom (
                ref vector1, ref vector2, ref vector3, ref vector4,
                ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Hermite (
            Vector4 vector1,
            Vector4 tangent1,
            Vector4 vector2,
            Vector4 tangent2,
            Fixed32 amount)
        {
            Vector4 result;
            Hermite (
                ref vector1, ref tangent1,
                ref vector2, ref tangent2,
                ref amount, out result);
            return result;
        }

        // Variant Utilities //-----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Min (
            Vector4 vector1,
            Vector4 vector2)
        {
            Vector4 result;
            Min (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Max (
            Vector4 vector1,
            Vector4 vector2)
        {
            Vector4 result;
            Max (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Clamp (
            Vector4 vector,
            Vector4 min,
            Vector4 max)
        {
            Vector4 result;
            Clamp (ref vector, ref min, ref max, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Lerp (
            Vector4 vector1,
            Vector4 vector2,
            Fixed32 amount)
        {
            Vector4 result;
            Lerp (ref vector1, ref vector2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean IsUnit (Vector4 vector)
        {
            Boolean result;
            IsUnit (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Fixed32 Length ()
        {
            Fixed32 result;
            Length (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Fixed32 LengthSquared ()
        {
            Fixed32 result;
            LengthSquared (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public void Normalise ()
        {
            Normalise (ref this, out this);
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Boolean IsUnit()
        {
            Boolean result;
            IsUnit (ref this, out result);
            return result;
        }

#endif
    }

    /// <summary>
    /// This class provides maths functions with consistent function
    /// signatures across all supported precisions.  The idea being
    /// the more you use this, the more you will be able to write
    /// code once and easily change the precision later.
    /// </summary>
    public static class Maths
    {
        /// <summary>
        /// Provides the constant E.
        /// </summary>
        public static void E (out Fixed32 value)
        {
            value = Fixed32.Parse("2.71828183");
        }

        /// <summary>
        /// Provides the constant Epsilon.
        /// </summary>
        public static void Epsilon (out Fixed32 value)
        {
            value = Fixed32.Parse("0.0001");
        }

        /// <summary>
        /// Provides the constant Half.
        /// </summary>
        public static void Half (out Fixed32 value)
        {
            value = Fixed32.Parse("0.5");
        }

        /// <summary>
        /// Provides the constant Quarter.
        /// </summary>
        public static void Quarter (out Fixed32 value)
        {
            value = Fixed32.Parse("0.25");
        }

        /// <summary>
        /// Provides the constant Log10E.
        /// </summary>
        public static void Log10E (out Fixed32 value)
        {
            value = Fixed32.Parse("0.4342945");
        }

        /// <summary>
        /// Provides the constant Log2E.
        /// </summary>
        public static void Log2E (out Fixed32 value)
        {
            value = Fixed32.Parse("1.442695");
        }

        /// <summary>
        /// Provides the constant Pi.
        /// </summary>
        public static void Pi (out Fixed32 value)
        {
            value = Fixed32.Parse("3.1415926536");
        }

        /// <summary>
        /// Provides the constant Root2.
        /// </summary>
        public static void Root2 (out Fixed32 value)
        {
            value = Fixed32.Parse("1.414213562");
        }

        /// <summary>
        /// Provides the constant Root3.
        /// </summary>
        public static void Root3 (out Fixed32 value)
        {
            value = Fixed32.Parse("1.732050808");
        }

        /// <summary>
        /// Provides the constant Tau.
        /// </summary>
        public static void Tau (out Fixed32 value)
        {
            value = Fixed32.Parse("6.283185");
        }

        /// <summary>
        /// Provides the constant Zero.
        /// </summary>
        public static void Zero (out Fixed32 value)
        {
            value = 0;
        }

        /// <summary>
        /// Provides the constant One.
        /// </summary>
        public static void One (out Fixed32 value)
        {
            value = 1;
        }


        /// <summary>
        /// ArcCos.
        /// </summary>
        public static Fixed32 ArcCos (Fixed32 value)
        {
            throw new NotImplementedException ();
        }

        /// <summary>
        /// ArcSin.
        /// </summary>
        public static Fixed32 ArcSin (Fixed32 value)
        {
            throw new NotImplementedException ();
        }

        /// <summary>
        /// ArcTan.
        /// </summary>
        public static Fixed32 ArcTan (Fixed32 value)
        {
            throw new NotImplementedException ();
        }

        /// <summary>
        /// Cos.
        /// </summary>
        public static Fixed32 Cos (Fixed32 value)
        {
            return Fixed32.Cos(value);
        }

        /// <summary>
        /// Sin.
        /// </summary>
        public static Fixed32 Sin (Fixed32 value)
        {
            return Fixed32.Sin(value);
        }

        /// <summary>
        /// Tan.
        /// </summary>
        public static Fixed32 Tan (Fixed32 value)
        {
            return Fixed32.Tan(value);
        }

        /// <summary>
        /// Sqrt.
        /// </summary>
        public static Fixed32 Sqrt (Fixed32 value)
        {
            return Fixed32.Sqrt(value);
        }

        /// <summary>
        /// Square.
        /// </summary>
        public static Fixed32 Square (Fixed32 value)
        {
            return Fixed32.Square(value);
        }

        /// <summary>
        /// Abs.
        /// </summary>
        public static Fixed32 Abs (Fixed32 value)
        {
            return (value < new Fixed32(0)) ? value * new Fixed32(-1) : value;
        }


        /// <summary>
        /// ToRadians
        /// </summary>
        public static Fixed32 ToRadians(Fixed32 input)
        {
            Fixed32 tau; Tau(out tau);
            return input * tau / ((Fixed32)360);
        }

        /// <summary>
        /// ToDegrees
        /// </summary>
        public static Fixed32 ToDegrees(Fixed32 input)
        {
            Fixed32 tau; Tau(out tau);
            return input / tau * ((Fixed32)360);
        }

        /// <summary>
        /// FromFraction
        /// </summary>
        public static void FromFraction(
            Int32 numerator, Int32 denominator, out Fixed32 value)
        {
            value = (Fixed32) numerator / (Fixed32) denominator;
        }

        /// <summary>
        /// FromString
        /// </summary>
        public static void FromString(String str, out Fixed32 value)
        {
            Fixed32.TryParse(str, out value);
        }

        /// <summary>
        /// IsZero
        /// </summary>
        public static Boolean IsZero(Fixed32 value)
        {
            Fixed32 ep;
            Epsilon(out ep);
            return Abs(value) < ep;
        }

        /// <summary>
        /// Min
        /// </summary>
        public static Fixed32 Min(Fixed32 a, Fixed32 b)
        {
            return a < b ? a : b;
        }

        /// <summary>
        /// Max
        /// </summary>
        public static Fixed32 Max(Fixed32 a, Fixed32 b)
        {
            return a > b ? a : b;
        }

        /// <summary>
        /// WithinEpsilon
        /// </summary>
        public static Boolean WithinEpsilon(Fixed32 a, Fixed32 b)
        {
            Fixed32 num = a - b;
            return ((-Fixed32.Epsilon <= num) && (num <= Fixed32.Epsilon));
        }

        /// <summary>
        /// Sign
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
    /// Fixed32 precision Matrix44.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Matrix44
        : IEquatable<Matrix44>
    {
        /// <summary>
        /// Gets or sets (Row 0, Column 0) of the Matrix44.
        /// </summary>
        public Fixed32 R0C0;

        /// <summary>
        /// Gets or sets (Row 0, Column 1) of the Matrix44.
        /// </summary>
        public Fixed32 R0C1;

        /// <summary>
        /// Gets or sets (Row 0, Column 2) of the Matrix44.
        /// </summary>
        public Fixed32 R0C2;

        /// <summary>
        /// Gets or sets (Row 0, Column 3) of the Matrix44.
        /// </summary>
        public Fixed32 R0C3;

        /// <summary>
        /// Gets or sets (Row 1, Column 0) of the Matrix44.
        /// </summary>
        public Fixed32 R1C0;

        /// <summary>
        /// Gets or sets (Row 1, Column 1) of the Matrix44.
        /// </summary>
        public Fixed32 R1C1;

        /// <summary>
        /// Gets or sets (ow 1, Column 2) of the Matrix44.
        /// </summary>
        public Fixed32 R1C2;

        /// <summary>
        /// Gets or sets (Row 1, Column 3) of the Matrix44.
        /// </summary>
        public Fixed32 R1C3;

        /// <summary>
        /// Gets or sets (Row 2, Column 0) of the Matrix44.
        /// </summary>
        public Fixed32 R2C0;

        /// <summary>
        /// Gets or sets (Row 2, Column 1) of the Matrix44.
        /// </summary>
        public Fixed32 R2C1;

        /// <summary>
        /// Gets or sets (Row 2, Column 2) of the Matrix44.
        /// </summary>
        public Fixed32 R2C2;

        /// <summary>
        /// Gets or sets (Row 2, Column 3) of the Matrix44.
        /// </summary>
        public Fixed32 R2C3;

        /// <summary>
        /// Gets or sets (Row 3, Column 0) of the Matrix44.
        /// </summary>
        public Fixed32 R3C0; // translation.x

        /// <summary>
        /// Gets or sets (Row 3, Column 1) of the Matrix44.
        /// </summary>
        public Fixed32 R3C1; // translation.y

        /// <summary>
        /// Gets or sets (Row 3, Column 2) of the Matrix44.
        /// </summary>
        public Fixed32 R3C2; // translation.z

        /// <summary>
        /// Gets or sets (Row 3, Column 3) of the Matrix44.
        /// </summary>
        public Fixed32 R3C3;

        /// <summary>
        /// Initilises a new instance of Matrix44 from sixteen Fixed32
        /// values representing the matrix, in row major order, respectively.
        /// </summary>
        public Matrix44 (
            Fixed32 m00,
            Fixed32 m01,
            Fixed32 m02,
            Fixed32 m03,
            Fixed32 m10,
            Fixed32 m11,
            Fixed32 m12,
            Fixed32 m13,
            Fixed32 m20,
            Fixed32 m21,
            Fixed32 m22,
            Fixed32 m23,
            Fixed32 m30,
            Fixed32 m31,
            Fixed32 m32,
            Fixed32 m33)
        {
            this.R0C0 = m00;
            this.R0C1 = m01;
            this.R0C2 = m02;
            this.R0C3 = m03;
            this.R1C0 = m10;
            this.R1C1 = m11;
            this.R1C2 = m12;
            this.R1C3 = m13;
            this.R2C0 = m20;
            this.R2C1 = m21;
            this.R2C2 = m22;
            this.R2C3 = m23;
            this.R3C0 = m30;
            this.R3C1 = m31;
            this.R3C2 = m32;
            this.R3C3 = m33;
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return
                "{ " +
                String.Format (
                    "{{R0C0:{0} R0C1:{1} R0C2:{2} R0C3:{3}}} ",
                    new Object[] {
                        this.R0C0.ToString (),
                        this.R0C1.ToString (),
                        this.R0C2.ToString (),
                        this.R0C3.ToString ()}) +
                String.Format (
                    "{{R1C0:{0} R1C1:{1} R1C2:{2} R1C3:{3}}} ",
                    new Object[] {
                        this.R1C0.ToString (),
                        this.R1C1.ToString (),
                        this.R1C2.ToString (),
                        this.R1C3.ToString ()}) +
                String.Format (
                    "{{R2C0:{0} R2C1:{1} R2C2:{2} R2C3:{3}}} ",
                    new Object[] {
                        this.R2C0.ToString (),
                        this.R2C1.ToString (),
                        this.R2C2.ToString (),
                        this.R2C3.ToString ()}) +
                String.Format (
                    "{{R3C0:{0} R3C1:{1} R3C2:{2} R3C3:{3}}} ",
                    new Object[] {
                        this.R3C0.ToString (),
                        this.R3C1.ToString (),
                        this.R3C2.ToString (),
                        this.R3C3.ToString ()}) +
                "}";
        }

        /// <summary>
        /// Gets the hash code of the Matrix44 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return R0C0.GetHashCode ()
                ^ R0C1.GetHashCode ().ShiftAndWrap (2)
                ^ R0C2.GetHashCode ().ShiftAndWrap (4)
                ^ R0C3.GetHashCode ().ShiftAndWrap (6)
                ^ R1C0.GetHashCode ().ShiftAndWrap (8)
                ^ R1C1.GetHashCode ().ShiftAndWrap (10)
                ^ R1C2.GetHashCode ().ShiftAndWrap (12)
                ^ R1C3.GetHashCode ().ShiftAndWrap (14)
                ^ R2C0.GetHashCode ().ShiftAndWrap (16)
                ^ R2C1.GetHashCode ().ShiftAndWrap (18)
                ^ R2C2.GetHashCode ().ShiftAndWrap (20)
                ^ R2C3.GetHashCode ().ShiftAndWrap (22)
                ^ R3C0.GetHashCode ().ShiftAndWrap (24)
                ^ R3C1.GetHashCode ().ShiftAndWrap (26)
                ^ R3C2.GetHashCode ().ShiftAndWrap (28)
                ^ R3C3.GetHashCode ().ShiftAndWrap (30);
        }

        /// <summary>
        /// Determines whether or not this Matrix44 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            return (obj is Matrix44)
                ? this.Equals ((Matrix44)obj)
                : false;
        }

        #region IEquatable<Matrix44>

        /// <summary>
        /// Determines whether or not this Matrix44 object is equal to another
        /// Matrix44 object.
        /// </summary>
        public Boolean Equals (Matrix44 other)
        {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        #endregion

        /// <summary>
        /// Gets and sets the up vector of the Matrix44.
        /// </summary>
        public Vector3 Up
        {
            get
            {
                Vector3 vector;
                vector.X = this.R1C0;
                vector.Y = this.R1C1;
                vector.Z = this.R1C2;
                return vector;
            }
            set
            {
                this.R1C0 = value.X;
                this.R1C1 = value.Y;
                this.R1C2 = value.Z;
            }
        }

        /// <summary>
        /// Gets and sets the down vector of the Matrix44.
        /// </summary>
        public Vector3 Down
        {
            get
            {
                Vector3 vector;
                vector.X = -this.R1C0;
                vector.Y = -this.R1C1;
                vector.Z = -this.R1C2;
                return vector;
            }
            set
            {
                this.R1C0 = -value.X;
                this.R1C1 = -value.Y;
                this.R1C2 = -value.Z;
            }
        }

        /// <summary>
        /// Gets and sets the right vector of the Matrix44.
        /// </summary>
        public Vector3 Right
        {
            get
            {
                Vector3 vector;
                vector.X = this.R0C0;
                vector.Y = this.R0C1;
                vector.Z = this.R0C2;
                return vector;
            }
            set
            {
                this.R0C0 = value.X;
                this.R0C1 = value.Y;
                this.R0C2 = value.Z;
            }
        }

        /// <summary>
        /// Gets and sets the left vector of the Matrix44.
        /// </summary>
        public Vector3 Left
        {
            get
            {
                Vector3 vector;
                vector.X = -this.R0C0;
                vector.Y = -this.R0C1;
                vector.Z = -this.R0C2;
                return vector;
            }
            set
            {
                this.R0C0 = -value.X;
                this.R0C1 = -value.Y;
                this.R0C2 = -value.Z;
            }
        }

        /// <summary>
        /// Gets and sets the forward vector of the Matrix44.
        /// </summary>
        public Vector3 Forward
        {
            get
            {
                Vector3 vector;
                vector.X = -this.R2C0;
                vector.Y = -this.R2C1;
                vector.Z = -this.R2C2;
                return vector;
            }
            set
            {
                this.R2C0 = -value.X;
                this.R2C1 = -value.Y;
                this.R2C2 = -value.Z;
            }
        }

        /// <summary>
        /// Gets and sets the backward vector of the Matrix44.
        /// </summary>
        public Vector3 Backward
        {
            get
            {
                Vector3 vector;
                vector.X = this.R2C0;
                vector.Y = this.R2C1;
                vector.Z = this.R2C2;
                return vector;
            }
            set
            {
                this.R2C0 = value.X;
                this.R2C1 = value.Y;
                this.R2C2 = value.Z;
            }
        }

        /// <summary>
        /// Gets and sets the translation vector of the Matrix44.
        /// </summary>
        public Vector3 Translation
        {
            get
            {
                Vector3 vector;
                vector.X = this.R3C0;
                vector.Y = this.R3C1;
                vector.Z = this.R3C2;
                return vector;
            }
            set
            {
                this.R3C0 = value.X;
                this.R3C1 = value.Y;
                this.R3C2 = value.Z;
            }
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines the identity matrix.
        /// </summary>
        static Matrix44 identity;

        /// <summary>
        /// Defines the zero matrix.
        /// </summary>
        static Matrix44 zero;

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

            zero = new Matrix44 (
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0);
        }

        /// <summary>
        /// Returns the identity matrix.
        /// </summary>
        public static Matrix44 Identity
        {
            get { return identity; }
        }

        /// <summary>
        /// Returns the zero matrix.
        /// </summary>
        public static Matrix44 Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Creates a translation matrix from a position.
        /// </summary>
        public static void CreateTranslation (
            ref Vector3 position,
            out Matrix44 result)
        {
            result.R0C0 = 1;
            result.R0C1 = 0;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = 1;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = 0;
            result.R2C2 = 1;
            result.R2C3 = 0;
            result.R3C0 = position.X;
            result.R3C1 = position.Y;
            result.R3C2 = position.Z;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a translation matrix from a position.
        /// </summary>
        public static void CreateTranslation (
            ref Fixed32 xPosition,
            ref Fixed32 yPosition,
            ref Fixed32 zPosition,
            out Matrix44 result)
        {
            result.R0C0 = 1;
            result.R0C1 = 0;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = 1;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = 0;
            result.R2C2 = 1;
            result.R2C3 = 0;
            result.R3C0 = xPosition;
            result.R3C1 = yPosition;
            result.R3C2 = zPosition;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a scaling matrix based on x, y, z.
        /// </summary>
        public static void CreateScale (
            ref Fixed32 xScale,
            ref Fixed32 yScale,
            ref Fixed32 zScale,
            out Matrix44 result)
        {
            result.R0C0 = xScale;
            result.R0C1 = 0;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = yScale;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = 0;
            result.R2C2 = zScale;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a scaling matrix based on a vector.
        /// </summary>
        public static void CreateScale (
            ref Vector3 scales,
            out Matrix44 result)
        {
            result.R0C0 = scales.X;
            result.R0C1 = 0;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = scales.Y;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = 0;
            result.R2C2 = scales.Z;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Create a scaling matrix consistant along each axis.
        /// </summary>
        public static void CreateScale (
            ref Fixed32 scale,
            out Matrix44 result)
        {
            result.R0C0 = scale;
            result.R0C1 = 0;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = scale;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = 0;
            result.R2C2 = scale;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a matrix representing a given rotation about the X axis.
        /// </summary>
        public static void CreateRotationX (
            ref Fixed32 radians,
            out Matrix44 result)
        {
            // http://en.wikipedia.org/wiki/Rotation_matrix
            Fixed32 cos = Maths.Cos (radians);
            Fixed32 sin = Maths.Sin (radians);

            result.R0C0 = 1;
            result.R0C1 = 0;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = cos;
            result.R1C2 = sin;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = -sin;
            result.R2C2 = cos;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a matrix representing a given rotation about the Y axis.
        /// </summary>
        public static void CreateRotationY (
            ref Fixed32 radians,
            out Matrix44 result)
        {
            // http://en.wikipedia.org/wiki/Rotation_matrix
            Fixed32 cos = Maths.Cos (radians);
            Fixed32 sin = Maths.Sin (radians);

            result.R0C0 = cos;
            result.R0C1 = 0;
            result.R0C2 = -sin;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = 1;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = sin;
            result.R2C1 = 0;
            result.R2C2 = cos;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a matrix representing a given rotation about the Z axis.
        /// </summary>
        public static void CreateRotationZ (
            ref Fixed32 radians,
            out Matrix44 result)
        {
            // http://en.wikipedia.org/wiki/Rotation_matrix
            Fixed32 cos = Maths.Cos (radians);
            Fixed32 sin = Maths.Sin (radians);

            result.R0C0 = cos;
            result.R0C1 = sin;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = -sin;
            result.R1C1 = cos;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = 0;
            result.R2C2 = 1;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a new Matrix44 that rotates around an arbitrary vector.
        /// </summary>
        public static void CreateFromAxisAngle (
            ref Vector3 axis,
            ref Fixed32 angle,
            out Matrix44 result)
        {
            Fixed32 x = axis.X;
            Fixed32 y = axis.Y;
            Fixed32 z = axis.Z;

            Fixed32 sin = Maths.Sin (angle);
            Fixed32 cos = Maths.Cos (angle);

            Fixed32 xx = x * x;
            Fixed32 yy = y * y;
            Fixed32 zz = z * z;

            Fixed32 xy = x * y;
            Fixed32 xz = x * z;
            Fixed32 yz = y * z;

            result.R0C0 = xx + (cos * (1 - xx));
            result.R0C1 = xy - (cos * xy) + (sin * z);
            result.R0C2 = xz - (cos * xz) - (sin * y);
            result.R0C3 = 0;

            result.R1C0 = xy - (cos * xy) - (sin * z);
            result.R1C1 = yy + (cos * (1 - yy));
            result.R1C2 = yz - (cos * yz) + (sin * x);
            result.R1C3 = 0;

            result.R2C0 = xz - (cos * xz) + (sin * y);
            result.R2C1 = yz - (cos * yz) - (sin * x);
            result.R2C2 = zz + (cos * (1 - zz));
            result.R2C3 = 0;

            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a new Matrix44 from an ordered triplet of vectors (axes)
        /// that are pair-wise perpendicular, have unit length and have an
        /// orientation for each axis.
        /// </summary>
        public static void CreateFromCartesianAxes (
            ref Vector3 right,
            ref Vector3 up,
            ref Vector3 backward,
            out Matrix44 result)
        {
            Boolean isRightUnit; Vector3.IsUnit (ref right, out isRightUnit);
            Boolean isUpUnit; Vector3.IsUnit (ref up, out isUpUnit);
            Boolean isBackwardUnit;
            Vector3.IsUnit (ref backward, out isBackwardUnit);

            if(!isRightUnit || !isUpUnit || !isBackwardUnit )
                throw new ArgumentException(
                    "The input vertors must be normalised.");

            // Perhaps we shd assert here is the Vectors are not pair-wise
            // perpendicular.

            result.R0C0 = right.X;
            result.R0C1 = right.Y;
            result.R0C2 = right.Z;
            result.R0C3 = 0;
            result.R1C0 = up.X;
            result.R1C1 = up.Y;
            result.R1C2 = up.Z;
            result.R1C3 = 0;
            result.R2C0 = backward.X;
            result.R2C1 = backward.Y;
            result.R2C2 = backward.Z;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a world matrix.
        /// This matrix includes rotation and translation, but not scaling.
        /// </summary>
        public static void CreateWorld (
            ref Vector3 position,
            ref Vector3 forward,
            ref Vector3 up,
            out Matrix44 result)
        {
            Vector3 backward;
            Vector3.Negate (ref forward, out backward);
            Vector3.Normalise (ref backward, out backward);

            Vector3 right;
            Vector3.Cross (ref up, ref backward, out right);
            Vector3.Normalise (ref right, out right);

            // We don't know if the inputs were actually perpendicular,
            // best make sure.
            Vector3 finalUp;
            Vector3.Cross (ref right, ref backward, out finalUp);
            Vector3.Normalise (ref finalUp, out finalUp);

            result.R0C0 = right.X;
            result.R0C1 = right.Y;
            result.R0C2 = right.Z;
            result.R0C3 = 0;
            result.R1C0 = finalUp.X;
            result.R1C1 = finalUp.Y;
            result.R1C2 = finalUp.Z;
            result.R1C3 = 0;
            result.R2C0 = backward.X;
            result.R2C1 = backward.Y;
            result.R2C2 = backward.Z;
            result.R2C3 = 0;
            result.R3C0 = position.X;
            result.R3C1 = position.Y;
            result.R3C2 = position.Z;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a rotation matrix from the given quaternion.
        /// </summary>
        public static void CreateFromQuaternion (
            ref Quaternion q, out Matrix44 result)
        {
            // http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToMatrix/

            Boolean qIsUnit;
            Quaternion.IsUnit (ref q, out qIsUnit);

            if(!qIsUnit)
                throw new ArgumentException(
                    "Input quaternion must be normalised.");

            Fixed32 zero = 0;
            Fixed32 one = 1;

            Fixed32 twoI = q.I + q.I;
            Fixed32 twoJ = q.J + q.J;
            Fixed32 twoK = q.K + q.K;

            Fixed32 twoUI = q.U * twoI;
            Fixed32 twoUJ = q.U * twoJ;
            Fixed32 twoUK = q.U * twoK;
            Fixed32 twoII = q.I * twoI;
            Fixed32 twoIJ = q.I * twoJ;
            Fixed32 twoIK = q.I * twoK;
            Fixed32 twoJJ = q.J * twoJ;
            Fixed32 twoJK = q.J * twoK;
            Fixed32 twoKK = q.K * twoK;

            result.R0C0 = one - twoJJ - twoKK;
            result.R1C0 = twoIJ - twoUK;
            result.R2C0 = twoIK + twoUJ;
            result.R3C0 = zero;

            result.R0C1 = twoIJ + twoUK;
            result.R1C1 = one - twoII - twoKK;
            result.R2C1 = twoJK - twoUI;
            result.R3C1 = zero;

            result.R0C2 = twoIK - twoUJ;
            result.R1C2 = twoJK + twoUI;
            result.R2C2 = one - twoII - twoJJ;
            result.R3C2 = zero;

            result.R0C3 = zero;
            result.R1C3 = zero;
            result.R2C3 = zero;
            result.R3C3 = one;
        }

        /// <summary>
        /// Creates a new rotation matrix from a specified yaw, pitch, and roll.
        /// </summary>
        public static void CreateFromYawPitchRoll (
            ref Fixed32 yaw,
            ref Fixed32 pitch,
            ref Fixed32 roll,
            out Matrix44 result)
        {
            Quaternion quaternion;

            Quaternion.CreateFromYawPitchRoll (
                ref yaw, ref pitch, ref roll, out quaternion);

            CreateFromQuaternion (ref quaternion, out result);
        }

        /// <summary>
        /// Creates a cylindrical billboard that rotates around a specified
        /// axis.  This method computes the facing direction of the billboard
        /// from the object position and camera position.  When the object and
        /// camera positions are too close, the matrix will not be accurate.
        /// To avoid this problem, the method uses the optional camera forward
        /// vector if the positions are too close.
        /// </summary>
        public static void CreateBillboard (
            ref Vector3 objectPosition,
            ref Vector3 cameraPosition,
            ref Vector3 cameraUpVector,
            ref Vector3? cameraForwardVector,
            out Matrix44 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 epsilon; Maths.Epsilon (out epsilon);

            Vector3 camToObjVec = objectPosition - cameraPosition;
            Fixed32 camToObjVecLL = camToObjVec.LengthSquared ();

            Vector3 v1;
            if (camToObjVecLL < epsilon)
            {
                v1 = cameraForwardVector.HasValue
                   ? -cameraForwardVector.Value
                   : Vector3.Forward;
            }
            else
            {
                Fixed32 t = one / Maths.Sqrt (camToObjVecLL);
                Vector3.Multiply (ref camToObjVec, ref t, out v1);
            }

            Vector3 v2;
            Vector3.Cross (ref cameraUpVector, ref v1, out v2);
            Vector3.Normalise (ref v2, out v2);

            Vector3 v3;
            Vector3.Cross (ref v1, ref v2, out v3);

            result.R0C0 = v2.X;
            result.R0C1 = v2.Y;
            result.R0C2 = v2.Z;
            result.R0C3 = zero;
            result.R1C0 = v3.X;
            result.R1C1 = v3.Y;
            result.R1C2 = v3.Z;
            result.R1C3 = zero;
            result.R2C0 = v1.X;
            result.R2C1 = v1.Y;
            result.R2C2 = v1.Z;
            result.R2C3 = zero;
            result.R3C0 = objectPosition.X;
            result.R3C1 = objectPosition.Y;
            result.R3C2 = objectPosition.Z;
            result.R3C3 = one;
        }

        /// <summary>
        /// Creates a cylindrical billboard that rotates around a specified
        /// axis.
        /// </summary>
        /// <remarks>
        /// This method computes the facing direction of the billboard from the
        /// object position and camera position. When the object and camera
        /// positions are too close, the matrix will not be accurate. To avoid
        /// this problem, the method uses the optional camera forward vector if
        /// the positions are too close.
        /// </remarks>
        public static void CreateConstrainedBillboard (
            ref Vector3 objectPosition,
            ref Vector3 cameraPosition,
            ref Vector3 rotateAxis,
            ref Vector3? cameraForwardVector,
            ref Vector3? objectForwardVector,
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
            Fixed32 limit;
            Maths.FromString("0.0001", out limit);

            if (num2 < limit)
            {
                vector2 = cameraForwardVector.HasValue
                        ? -cameraForwardVector.Value
                        : Vector3.Forward;
            }
            else
            {
                Fixed32 t = one / Maths.Sqrt (num2);
                Vector3.Multiply (ref vector2, ref t, out vector2);
            }

            Vector3 vector4 = rotateAxis;
            Vector3.Dot (ref rotateAxis, ref vector2, out num);

            Fixed32 realHorrid;
            Maths.FromString("0.9982547", out realHorrid);

            if (Maths.Abs (num) > realHorrid)
            {
                if (objectForwardVector.HasValue)
                {
                    vector = objectForwardVector.Value;
                    Vector3.Dot (ref rotateAxis, ref vector, out num);

                    if (Maths.Abs (num) > realHorrid)
                    {
                        num = (rotateAxis.X * Vector3.Forward.X)
                            + (rotateAxis.Y * Vector3.Forward.Y)
                            + (rotateAxis.Z * Vector3.Forward.Z);

                        vector = (Maths.Abs (num) > realHorrid)
                               ? Vector3.Right
                               : Vector3.Forward;
                    }
                }
                else
                {
                    num = (rotateAxis.X * Vector3.Forward.X)
                        + (rotateAxis.Y * Vector3.Forward.Y)
                        + (rotateAxis.Z * Vector3.Forward.Z);

                    vector = (Maths.Abs (num) > realHorrid)
                           ? Vector3.Right
                           : Vector3.Forward;
                }

                Vector3.Cross (ref rotateAxis, ref vector, out vector3);
                Vector3.Normalise (ref vector3, out vector3);
                Vector3.Cross (ref vector3, ref rotateAxis, out vector);
                Vector3.Normalise (ref vector, out vector);
            }
            else
            {
                Vector3.Cross (ref rotateAxis, ref vector2, out vector3);
                Vector3.Normalise (ref vector3, out vector3);
                Vector3.Cross (ref vector3, ref vector4, out vector);
                Vector3.Normalise (ref vector, out vector);
            }

            result.R0C0 = vector3.X;
            result.R0C1 = vector3.Y;
            result.R0C2 = vector3.Z;
            result.R0C3 = zero;
            result.R1C0 = vector4.X;
            result.R1C1 = vector4.Y;
            result.R1C2 = vector4.Z;
            result.R1C3 = zero;
            result.R2C0 = vector.X;
            result.R2C1 = vector.Y;
            result.R2C2 = vector.Z;
            result.R2C3 = zero;
            result.R3C0 = objectPosition.X;
            result.R3C1 = objectPosition.Y;
            result.R3C2 = objectPosition.Z;
            result.R3C3 = one;
        }

        /// <summary>
        /// Builds a perspective projection matrix based on a field of view.
        /// </summary>
        /// <remarks>
        /// Projection space refers to the space after applying projection
        /// transformation from view space. After the projection transformation,
        /// visible content has x- and y-coordinates ranging from −1 to 1, and a
        /// z-coordinate ranging from 0 to 1.
        /// </remarks>
        public static void CreatePerspectiveFieldOfView (
            ref Fixed32 fieldOfView,
            ref Fixed32 aspectRatio,
            ref Fixed32 nearPlaneDistance,
            ref Fixed32 farPlaneDistance,
            out Matrix44 result)
        {
            // http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
            Fixed32 zero = 0;
            Fixed32 half; Maths.Half (out half);
            Fixed32 one = 1;
            Fixed32 pi; Maths.Pi (out pi);

            if (fieldOfView <= zero || fieldOfView >= pi)
                throw new ArgumentOutOfRangeException ("fieldOfView");

            if (nearPlaneDistance <= zero)
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");

            if (farPlaneDistance <= zero)
                throw new ArgumentOutOfRangeException ("farPlaneDistance");

            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");

            // xScale     0          0              0
            // 0        yScale       0              0
            // 0        0        zf/(zn-zf)        -1
            // 0        0        zn*zf/(zn-zf)      0

            // where:
            // yScale = cot(fovY/2)
            // xScale = yScale / aspect ratio

            // yScale = cot(fovY/2)
            Fixed32 yScale = one / (Maths.Tan (fieldOfView * half));

            // xScale = yScale / aspect ratio
            Fixed32 xScale = yScale / aspectRatio;

            result.R0C0 = xScale;
            result.R0C1 = zero;
            result.R0C2 = zero;
            result.R0C3 = zero;

            result.R1C0 = zero;
            result.R1C1 = yScale;
            result.R1C2 = zero;
            result.R1C3 = zero;

            result.R2C0 = zero;
            result.R2C1 = zero;
            // zf/(zn-zf)
            result.R2C2 = farPlaneDistance
                        / (nearPlaneDistance - farPlaneDistance);
            result.R2C3 = -one;

            result.R3C0 = zero;
            result.R3C1 = zero;
            // zn*zf/(zn-zf)
            result.R3C2 = (nearPlaneDistance * farPlaneDistance)
                        / (nearPlaneDistance - farPlaneDistance);
            result.R3C3 = zero;
        }

        /// <summary>
        /// Builds a perspective projection matrix.
        /// </summary>
        /// <remarks>
        /// Projection space refers to the space after applying projection
        /// transformation from view space. After the projection transformation,
        /// visible content has x- and y-coordinates ranging from −1 to 1, and a
        /// z-coordinate ranging from 0 to 1.
        /// </remarks>
        public static void CreatePerspective (
            ref Fixed32 width,
            ref Fixed32 height,
            ref Fixed32 nearPlaneDistance,
            ref Fixed32 farPlaneDistance,
            out Matrix44 result)
        {
            // http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
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
            result.R0C0 = (two * nearPlaneDistance) / width;
            result.R0C1 = result.R0C2 = result.R0C3 = zero;
            result.R1C1 = (two * nearPlaneDistance) / height;
            result.R1C0 = result.R1C2 = result.R1C3 = zero;
            result.R2C2 = farPlaneDistance
                        / (nearPlaneDistance - farPlaneDistance);
            result.R2C0 = result.R2C1 = zero;
            result.R2C3 = -one;
            result.R3C0 = result.R3C1 = result.R3C3 = zero;
            result.R3C2 = (nearPlaneDistance * farPlaneDistance)
                        / (nearPlaneDistance - farPlaneDistance);
        }

        /// <summary>
        /// Builds a customized, perspective projection matrix.
        /// </summary>
        /// <remarks>
        /// Projection space refers to the space after applying projection
        /// transformation from view space. After the projection transformation,
        /// visible content has x- and y-coordinates ranging from −1 to 1, and a
        /// z-coordinate ranging from 0 to 1.
        /// </remarks>
        public static void CreatePerspectiveOffCenter (
            ref Fixed32 left,
            ref Fixed32 right,
            ref Fixed32 bottom,
            ref Fixed32 top,
            ref Fixed32 nearPlaneDistance,
            ref Fixed32 farPlaneDistance,
            out Matrix44 result)
        {
            // http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx

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
            result.R0C0 = (two * nearPlaneDistance) / (right - left);
            result.R0C1 = result.R0C2 = result.R0C3 = zero;
            result.R1C1 = (two * nearPlaneDistance) / (top - bottom);
            result.R1C0 = result.R1C2 = result.R1C3 = zero;
            result.R2C0 = (left + right) / (right - left);
            result.R2C1 = (top + bottom) / (top - bottom);
            result.R2C2 = farPlaneDistance
                        / (nearPlaneDistance - farPlaneDistance);
            result.R2C3 = -one;
            result.R3C2 = (nearPlaneDistance * farPlaneDistance)
                        / (nearPlaneDistance - farPlaneDistance);
            result.R3C0 = result.R3C1 = result.R3C3 = zero;
        }

        /// <summary>
        /// Builds an orthogonal projection matrix.
        /// </summary>
        /// <remarks>
        /// Projection space refers to the space after applying projection
        /// transformation from view space. After the projection transformation,
        /// visible content has x and y coordinates ranging from -1 to 1, and z
        /// coordinates ranging from 0 to 1.
        ///
        /// Unlike perspective projection, in orthographic projection there is
        /// no perspective foreshortening.
        ///
        /// The viewable area of this orthographic projection extends from left
        /// to right on the x-axis, bottom to top on the y-axis, and zNearPlane
        /// to zFarPlane on the z-axis. These values are relative to the
        /// position and x, y, and z-axes of the view.
        /// </remarks>
        public static void CreateOrthographic (
            ref Fixed32 width,
            ref Fixed32 height,
            ref Fixed32 zNearPlane,
            ref Fixed32 zFarPlane,
            out Matrix44 result)
        {
            // http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 two = 2;

            result.R0C0 = two / width;
            result.R0C1 = result.R0C2 = result.R0C3 = zero;
            result.R1C1 = two / height;
            result.R1C0 = result.R1C2 = result.R1C3 = zero;
            result.R2C2 = one / (zNearPlane - zFarPlane);
            result.R2C0 = result.R2C1 = result.R2C3 = zero;
            result.R3C0 = result.R3C1 = zero;
            result.R3C2 = zNearPlane / (zNearPlane - zFarPlane);
            result.R3C3 = one;
        }

        /// <summary>
        /// Builds a customized, orthogonal projection matrix.
        /// </summary>
        /// <remarks>
        /// Projection space refers to the space after applying projection
        /// transformation from view space. After the projection transformation,
        /// visible content has x and y coordinates ranging from -1 to 1, and z
        /// coordinates ranging from 0 to 1.
        ///
        /// Unlike perspective projection, in orthographic projection there is
        /// no perspective foreshortening.
        ///
        /// The viewable area of this orthographic projection extends from left
        /// to right on the x-axis, bottom to top on the y-axis, and zNearPlane
        /// to zFarPlane on the z-axis. These values are relative to the
        /// position and x, y, and z-axes of the view.
        /// </remarks>
        public static void CreateOrthographicOffCenter (
            ref Fixed32 left,
            ref Fixed32 right,
            ref Fixed32 bottom,
            ref Fixed32 top,
            ref Fixed32 zNearPlane,
            ref Fixed32 zFarPlane,
            out Matrix44 result)
        {
            // http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx

            Fixed32 zero = 0;
            Fixed32 one = 1;
            Fixed32 two = 2;

            result.R0C0 = two / (right - left);
            result.R0C1 = result.R0C2 = result.R0C3 = zero;
            result.R1C1 = two / (top - bottom);
            result.R1C0 = result.R1C2 = result.R1C3 = zero;
            result.R2C2 = one / (zNearPlane - zFarPlane);
            result.R2C0 = result.R2C1 = result.R2C3 = zero;
            result.R3C0 = (left + right) / (left - right);
            result.R3C1 = (top + bottom) / (bottom - top);
            result.R3C2 = zNearPlane / (zNearPlane - zFarPlane);
            result.R3C3 = one;
        }

        /// <summary>
        /// Creates a view matrix.
        /// </summary>
        /// <remarks>
        /// View space, sometimes called camera space, is similar to world space
        /// in that it is typically used for the entire scene. However, in view
        /// space, the origin is at the viewer or camera.
        /// </remarks>
        public static void CreateLookAt (
            ref Vector3 cameraPosition,
            ref Vector3 cameraTarget,
            ref Vector3 cameraUpVector,
            out Matrix44 result)
        {
            // http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx

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

            result.R0C0 = right.X;
            result.R0C1 = up.X;
            result.R0C2 = forward.X;
            result.R0C3 = zero;

            result.R1C0 = right.Y;
            result.R1C1 = up.Y;
            result.R1C2 = forward.Y;
            result.R1C3 = zero;

            result.R2C0 = right.Z;
            result.R2C1 = up.Z;
            result.R2C2 = forward.Z;
            result.R2C3 = zero;

            Fixed32 a;
            Fixed32 b;
            Fixed32 c;

            Vector3.Dot (ref right, ref cameraPosition, out a);
            Vector3.Dot (ref up, ref cameraPosition, out b);
            Vector3.Dot (ref forward, ref cameraPosition, out c);

            result.R3C0 = -a;
            result.R3C1 = -b;
            result.R3C2 = -c;

            result.R3C3 = one;
        }

        /// <summary>
        /// Transposes the rows and columns of a matrix.  The transpose of a
        /// given matrix is the matrix which is formed by turning all the rows
        /// of a given matrix into columns and vice-versa.
        /// N.B. On a computer, one can often avoid explicitly transposing a
        /// matrix in memory by simply accessing the same data in a
        /// different order.
        /// </summary>
        public static void Transpose (ref Matrix44 m, out Matrix44 result)
        {
            result.R0C0 = m.R0C0;
            result.R1C1 = m.R1C1;
            result.R2C2 = m.R2C2;
            result.R3C3 = m.R3C3;

            Fixed32 t = m.R0C1;
            result.R0C1 = m.R1C0;
            result.R1C0 = t;

            t = m.R0C2;
            result.R0C2 = m.R2C0;
            result.R2C0 = t;

            t = m.R0C3;
            result.R0C3 = m.R3C0;
            result.R3C0 = t;

            t = m.R1C2;
            result.R1C2 = m.R2C1;
            result.R2C1 = t;

            t = m.R1C3;
            result.R1C3 = m.R3C1;
            result.R3C1 = t;

            t =  m.R2C3;
            result.R2C3 = m.R3C2;
            result.R3C2 = t;
        }

        /// <summary>
        /// Reference Implementation:
        /// Essential Mathemathics For Games & Interactive Applications
        /// </summary>
        public static void Decompose (
            ref Matrix44 matrix,
            out Vector3 scale,
            out Quaternion rotation,
            out Vector3 translation,
            out Boolean result)
        {
            translation.X = matrix.R3C0;
            translation.Y = matrix.R3C1;
            translation.Z = matrix.R3C2;

            Vector3 a = new Vector3(matrix.R0C0, matrix.R1C0, matrix.R2C0);
            Vector3 b = new Vector3(matrix.R0C1, matrix.R1C1, matrix.R2C1);
            Vector3 c = new Vector3(matrix.R0C2, matrix.R1C2, matrix.R2C2);

            Fixed32 aLen; Vector3.Length(ref a, out aLen); scale.X = aLen;
            Fixed32 bLen; Vector3.Length(ref b, out bLen); scale.Y = bLen;
            Fixed32 cLen; Vector3.Length(ref c, out cLen); scale.Z = cLen;

            if ( Maths.IsZero(scale.X) ||
                 Maths.IsZero(scale.Y) ||
                 Maths.IsZero(scale.Z) )
            {
                rotation = Quaternion.Identity;
                result = false;
            }

            Fixed32 epsilon; Maths.Epsilon(out epsilon);

            if (aLen < epsilon) a = Vector3.Zero;
            else Vector3.Normalise(ref a, out a);

            if (bLen < epsilon) b = Vector3.Zero;
            else Vector3.Normalise(ref b, out b);

            if (cLen < epsilon) c = Vector3.Zero;
            else Vector3.Normalise(ref c, out c);

            Vector3 right = new Vector3 (a.X, b.X, c.X);
            Vector3 up = new Vector3 (a.Y, b.Y, c.Y);
            Vector3 backward = new Vector3 (a.Z, b.Z, c.Z);

            if (right == Vector3.Zero) right = Vector3.Right;
            if (up == Vector3.Zero) up = Vector3.Up;
            if (backward == Vector3.Zero) backward = Vector3.Backward;

            Vector3.Normalise (ref right, out right);
            Vector3.Normalise (ref up, out up);
            Vector3.Normalise (ref backward, out backward);

            Matrix44 rotMat;
            Matrix44.CreateFromCartesianAxes(
                ref right, ref up, ref backward, out rotMat);

            Quaternion.CreateFromRotationMatrix (ref rotMat, out rotation);

            result = true;
        }

        /// <summary>
        /// A determinant is a scalar number which is calculated from a matrix.
        /// This number can determine whether a set of linear equations are
        /// solvable, in other words whether the matrix can be inverted.
        /// </summary>
        public static void Determinant (ref Matrix44 m, out Fixed32 result)
        {
            result =
                + m.R0C3 * m.R1C2 * m.R2C1 * m.R3C0
                - m.R0C2 * m.R1C3 * m.R2C1 * m.R3C0
                - m.R0C3 * m.R1C1 * m.R2C2 * m.R3C0
                + m.R0C1 * m.R1C3 * m.R2C2 * m.R3C0
                + m.R0C2 * m.R1C1 * m.R2C3 * m.R3C0
                - m.R0C1 * m.R1C2 * m.R2C3 * m.R3C0
                - m.R0C3 * m.R1C2 * m.R2C0 * m.R3C1
                + m.R0C2 * m.R1C3 * m.R2C0 * m.R3C1
                + m.R0C3 * m.R1C0 * m.R2C2 * m.R3C1
                - m.R0C0 * m.R1C3 * m.R2C2 * m.R3C1
                - m.R0C2 * m.R1C0 * m.R2C3 * m.R3C1
                + m.R0C0 * m.R1C2 * m.R2C3 * m.R3C1
                + m.R0C3 * m.R1C1 * m.R2C0 * m.R3C2
                - m.R0C1 * m.R1C3 * m.R2C0 * m.R3C2
                - m.R0C3 * m.R1C0 * m.R2C1 * m.R3C2
                + m.R0C0 * m.R1C3 * m.R2C1 * m.R3C2
                + m.R0C1 * m.R1C0 * m.R2C3 * m.R3C2
                - m.R0C0 * m.R1C1 * m.R2C3 * m.R3C2
                - m.R0C2 * m.R1C1 * m.R2C0 * m.R3C3
                + m.R0C1 * m.R1C2 * m.R2C0 * m.R3C3
                + m.R0C2 * m.R1C0 * m.R2C1 * m.R3C3
                - m.R0C0 * m.R1C2 * m.R2C1 * m.R3C3
                - m.R0C1 * m.R1C0 * m.R2C2 * m.R3C3
                + m.R0C0 * m.R1C1 * m.R2C2 * m.R3C3;
        }

        /// <summary>
        /// The inverse of a matrix is another matrix that when multiplied
        /// by the original matrix yields the identity matrix.
        /// </summary>
        public static void Invert (ref Matrix44 m, out Matrix44 result)
        {
            Fixed32 one = 1;
            Fixed32 d;
            Determinant (ref m, out d);
            Fixed32 s = one / d;

            result.R0C0 =
                + m.R1C2 * m.R2C3 * m.R3C1 - m.R1C3 * m.R2C2 * m.R3C1
                + m.R1C3 * m.R2C1 * m.R3C2 - m.R1C1 * m.R2C3 * m.R3C2
                - m.R1C2 * m.R2C1 * m.R3C3 + m.R1C1 * m.R2C2 * m.R3C3;

            result.R0C1 =
                + m.R0C3 * m.R2C2 * m.R3C1 - m.R0C2 * m.R2C3 * m.R3C1
                - m.R0C3 * m.R2C1 * m.R3C2 + m.R0C1 * m.R2C3 * m.R3C2
                + m.R0C2 * m.R2C1 * m.R3C3 - m.R0C1 * m.R2C2 * m.R3C3;

            result.R0C2 =
                + m.R0C2 * m.R1C3 * m.R3C1 - m.R0C3 * m.R1C2 * m.R3C1
                + m.R0C3 * m.R1C1 * m.R3C2 - m.R0C1 * m.R1C3 * m.R3C2
                - m.R0C2 * m.R1C1 * m.R3C3 + m.R0C1 * m.R1C2 * m.R3C3;

            result.R0C3 =
                + m.R0C3 * m.R1C2 * m.R2C1 - m.R0C2 * m.R1C3 * m.R2C1
                - m.R0C3 * m.R1C1 * m.R2C2 + m.R0C1 * m.R1C3 * m.R2C2
                + m.R0C2 * m.R1C1 * m.R2C3 - m.R0C1 * m.R1C2 * m.R2C3;

            result.R1C0 =
                + m.R1C3 * m.R2C2 * m.R3C0 - m.R1C2 * m.R2C3 * m.R3C0
                - m.R1C3 * m.R2C0 * m.R3C2 + m.R1C0 * m.R2C3 * m.R3C2
                + m.R1C2 * m.R2C0 * m.R3C3 - m.R1C0 * m.R2C2 * m.R3C3;

            result.R1C1 =
                + m.R0C2 * m.R2C3 * m.R3C0 - m.R0C3 * m.R2C2 * m.R3C0
                + m.R0C3 * m.R2C0 * m.R3C2 - m.R0C0 * m.R2C3 * m.R3C2
                - m.R0C2 * m.R2C0 * m.R3C3 + m.R0C0 * m.R2C2 * m.R3C3;

            result.R1C2 =
                + m.R0C3 * m.R1C2 * m.R3C0 - m.R0C2 * m.R1C3 * m.R3C0
                - m.R0C3 * m.R1C0 * m.R3C2 + m.R0C0 * m.R1C3 * m.R3C2
                + m.R0C2 * m.R1C0 * m.R3C3 - m.R0C0 * m.R1C2 * m.R3C3;

            result.R1C3 =
                + m.R0C2 * m.R1C3 * m.R2C0 - m.R0C3 * m.R1C2 * m.R2C0
                + m.R0C3 * m.R1C0 * m.R2C2 - m.R0C0 * m.R1C3 * m.R2C2
                - m.R0C2 * m.R1C0 * m.R2C3 + m.R0C0 * m.R1C2 * m.R2C3;

            result.R2C0 =
                + m.R1C1 * m.R2C3 * m.R3C0 - m.R1C3 * m.R2C1 * m.R3C0
                + m.R1C3 * m.R2C0 * m.R3C1 - m.R1C0 * m.R2C3 * m.R3C1
                - m.R1C1 * m.R2C0 * m.R3C3 + m.R1C0 * m.R2C1 * m.R3C3;

            result.R2C1 =
                + m.R0C3 * m.R2C1 * m.R3C0 - m.R0C1 * m.R2C3 * m.R3C0
                - m.R0C3 * m.R2C0 * m.R3C1 + m.R0C0 * m.R2C3 * m.R3C1
                + m.R0C1 * m.R2C0 * m.R3C3 - m.R0C0 * m.R2C1 * m.R3C3;

            result.R2C2 =
                + m.R0C1 * m.R1C3 * m.R3C0 - m.R0C3 * m.R1C1 * m.R3C0
                + m.R0C3 * m.R1C0 * m.R3C1 - m.R0C0 * m.R1C3 * m.R3C1
                - m.R0C1 * m.R1C0 * m.R3C3 + m.R0C0 * m.R1C1 * m.R3C3;

            result.R2C3 =
                + m.R0C3 * m.R1C1 * m.R2C0 - m.R0C1 * m.R1C3 * m.R2C0
                - m.R0C3 * m.R1C0 * m.R2C1 + m.R0C0 * m.R1C3 * m.R2C1
                + m.R0C1 * m.R1C0 * m.R2C3 - m.R0C0 * m.R1C1 * m.R2C3;

            result.R3C0 =
                + m.R1C2 * m.R2C1 * m.R3C0 - m.R1C1 * m.R2C2 * m.R3C0
                - m.R1C2 * m.R2C0 * m.R3C1 + m.R1C0 * m.R2C2 * m.R3C1
                + m.R1C1 * m.R2C0 * m.R3C2 - m.R1C0 * m.R2C1 * m.R3C2;

            result.R3C1 =
                + m.R0C1 * m.R2C2 * m.R3C0 - m.R0C2 * m.R2C1 * m.R3C0
                + m.R0C2 * m.R2C0 * m.R3C1 - m.R0C0 * m.R2C2 * m.R3C1
                - m.R0C1 * m.R2C0 * m.R3C2 + m.R0C0 * m.R2C1 * m.R3C2;

            result.R3C2 =
                + m.R0C2 * m.R1C1 * m.R3C0 - m.R0C1 * m.R1C2 * m.R3C0
                - m.R0C2 * m.R1C0 * m.R3C1 + m.R0C0 * m.R1C2 * m.R3C1
                + m.R0C1 * m.R1C0 * m.R3C2 - m.R0C0 * m.R1C1 * m.R3C2;

            result.R3C3 =
                + m.R0C1 * m.R1C2 * m.R2C0 - m.R0C2 * m.R1C1 * m.R2C0
                + m.R0C2 * m.R1C0 * m.R2C1 - m.R0C0 * m.R1C2 * m.R2C1
                - m.R0C1 * m.R1C0 * m.R2C2 + m.R0C0 * m.R1C1 * m.R2C2;


            Multiply (ref result, ref s, out result);
        }

        /// <summary>
        /// Transforms a Matrix (m) by applying a Quaternion rotation (q).
        /// </summary>
        public static void Transform (
            ref Matrix44 m, ref Quaternion q, out Matrix44 result)
        {
            Boolean qIsUnit;
            Quaternion.IsUnit (ref q, out qIsUnit);

            if(!qIsUnit)
                throw new ArgumentException(
                    "Input quaternion must be normalised.");

            // Could just do Matrix44.CreateFromQuaternionHere, but we won't
            // use all of the data, so just calculate what we need.
            Fixed32 zero = 0;
            Fixed32 one = 1;

            Fixed32 twoI = q.I + q.I;
            Fixed32 twoJ = q.J + q.J;
            Fixed32 twoK = q.K + q.K;

            Fixed32 twoUI = q.U * twoI;
            Fixed32 twoUJ = q.U * twoJ;
            Fixed32 twoUK = q.U * twoK;
            Fixed32 twoII = q.I * twoI;
            Fixed32 twoIJ = q.I * twoJ;
            Fixed32 twoIK = q.I * twoK;
            Fixed32 twoJJ = q.J * twoJ;
            Fixed32 twoJK = q.J * twoK;
            Fixed32 twoKK = q.K * twoK;

            Fixed32 tR0C0 = one - twoJJ - twoKK;
            Fixed32 tR1C0 = twoIJ - twoUK;
            Fixed32 tR2C0 = twoIK + twoUJ;
            //Fixed32 tR3C0 = zero;

            Fixed32 tR0C1 = twoIJ + twoUK;
            Fixed32 tR1C1 = one - twoII - twoKK;
            Fixed32 tR2C1 = twoJK - twoUI;
            //Fixed32 tR3C1 = zero;

            Fixed32 tR0C2 = twoIK - twoUJ;
            Fixed32 tR1C2 = twoJK + twoUI;
            Fixed32 tR2C2 = one - twoII - twoJJ;
            //Fixed32 tR3C2 = zero;

            //Fixed32 tR0C3 = zero;
            //Fixed32 tR1C3 = zero;
            //Fixed32 tR2C3 = zero;
            //Fixed32 tR3C3 = zero;


            // Could just multiply here, but we know a bunch of stuff in `t`
            // will be zero, so doing the following is the same, but with less
            // operations.
            result.R0C0 = m.R0C0 * tR0C0 + m.R0C1 * tR1C0 + m.R0C2 * tR2C0;
            result.R0C1 = m.R0C0 * tR0C1 + m.R0C1 * tR1C1 + m.R0C2 * tR2C1;
            result.R0C2 = m.R0C0 * tR0C2 + m.R0C1 * tR1C2 + m.R0C2 * tR2C2;
            result.R0C3 = m.R0C3;

            result.R1C0 = m.R1C0 * tR0C0 + m.R1C1 * tR1C0 + m.R1C2 * tR2C0;
            result.R1C1 = m.R1C0 * tR0C1 + m.R1C1 * tR1C1 + m.R1C2 * tR2C1;
            result.R1C2 = m.R1C0 * tR0C2 + m.R1C1 * tR1C2 + m.R1C2 * tR2C2;
            result.R1C3 = m.R1C3;

            result.R2C0 = m.R2C0 * tR0C0 + m.R2C1 * tR1C0 + m.R2C2 * tR2C0;
            result.R2C1 = m.R2C0 * tR0C1 + m.R2C1 * tR1C1 + m.R2C2 * tR2C1;
            result.R2C2 = m.R2C0 * tR0C2 + m.R2C1 * tR1C2 + m.R2C2 * tR2C2;
            result.R2C3 = m.R2C3;

            result.R3C0 = m.R3C0 * tR0C0 + m.R3C1 * tR1C0 + m.R3C2 * tR2C0;
            result.R3C1 = m.R3C0 * tR0C1 + m.R3C1 * tR1C1 + m.R3C2 * tR2C1;
            result.R3C2 = m.R3C0 * tR0C2 + m.R3C1 * tR1C2 + m.R3C2 * tR2C2;
            result.R3C3 = m.R3C3;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not two Matrix44 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static void Equals (
            ref Matrix44 m1, ref Matrix44 m2, out Boolean result)
        {
            result =
                (m1.R0C0 == m2.R0C0) && (m1.R1C1 == m2.R1C1) &&
                (m1.R2C2 == m2.R2C2) && (m1.R3C3 == m2.R3C3) &&
                (m1.R0C1 == m2.R0C1) && (m1.R0C2 == m2.R0C2) &&
                (m1.R0C3 == m2.R0C3) && (m1.R1C0 == m2.R1C0) &&
                (m1.R1C2 == m2.R1C2) && (m1.R1C3 == m2.R1C3) &&
                (m1.R2C0 == m2.R2C0) && (m1.R2C1 == m2.R2C1) &&
                (m1.R2C3 == m2.R2C3) && (m1.R3C0 == m2.R3C0) &&
                (m1.R3C1 == m2.R3C1) && (m1.R3C2 == m2.R3C2);
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Matrix44 objects.
        /// </summary>
        public static void Add (
            ref Matrix44 m1, ref Matrix44 m2, out Matrix44 result)
        {
            result.R0C0 = m1.R0C0 + m2.R0C0;
            result.R0C1 = m1.R0C1 + m2.R0C1;
            result.R0C2 = m1.R0C2 + m2.R0C2;
            result.R0C3 = m1.R0C3 + m2.R0C3;
            result.R1C0 = m1.R1C0 + m2.R1C0;
            result.R1C1 = m1.R1C1 + m2.R1C1;
            result.R1C2 = m1.R1C2 + m2.R1C2;
            result.R1C3 = m1.R1C3 + m2.R1C3;
            result.R2C0 = m1.R2C0 + m2.R2C0;
            result.R2C1 = m1.R2C1 + m2.R2C1;
            result.R2C2 = m1.R2C2 + m2.R2C2;
            result.R2C3 = m1.R2C3 + m2.R2C3;
            result.R3C0 = m1.R3C0 + m2.R3C0;
            result.R3C1 = m1.R3C1 + m2.R3C1;
            result.R3C2 = m1.R3C2 + m2.R3C2;
            result.R3C3 = m1.R3C3 + m2.R3C3;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Matrix44 objects.
        /// </summary>
        public static void Subtract (
            ref Matrix44 m1, ref Matrix44 m2, out Matrix44 result)
        {
            result.R0C0 = m1.R0C0 - m2.R0C0;
            result.R0C1 = m1.R0C1 - m2.R0C1;
            result.R0C2 = m1.R0C2 - m2.R0C2;
            result.R0C3 = m1.R0C3 - m2.R0C3;
            result.R1C0 = m1.R1C0 - m2.R1C0;
            result.R1C1 = m1.R1C1 - m2.R1C1;
            result.R1C2 = m1.R1C2 - m2.R1C2;
            result.R1C3 = m1.R1C3 - m2.R1C3;
            result.R2C0 = m1.R2C0 - m2.R2C0;
            result.R2C1 = m1.R2C1 - m2.R2C1;
            result.R2C2 = m1.R2C2 - m2.R2C2;
            result.R2C3 = m1.R2C3 - m2.R2C3;
            result.R3C0 = m1.R3C0 - m2.R3C0;
            result.R3C1 = m1.R3C1 - m2.R3C1;
            result.R3C2 = m1.R3C2 - m2.R3C2;
            result.R3C3 = m1.R3C3 - m2.R3C3;
        }

        // Negation Operators //----------------------------------------------//

        /// <summary>
        /// Performs negation of a Matrix44 object.
        /// </summary>
        public static void Negate (ref Matrix44 matrix, out Matrix44 result)
        {
            result.R0C0 = -matrix.R0C0;
            result.R0C1 = -matrix.R0C1;
            result.R0C2 = -matrix.R0C2;
            result.R0C3 = -matrix.R0C3;
            result.R1C0 = -matrix.R1C0;
            result.R1C1 = -matrix.R1C1;
            result.R1C2 = -matrix.R1C2;
            result.R1C3 = -matrix.R1C3;
            result.R2C0 = -matrix.R2C0;
            result.R2C1 = -matrix.R2C1;
            result.R2C2 = -matrix.R2C2;
            result.R2C3 = -matrix.R2C3;
            result.R3C0 = -matrix.R3C0;
            result.R3C1 = -matrix.R3C1;
            result.R3C2 = -matrix.R3C2;
            result.R3C3 = -matrix.R3C3;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Matrix44 objects.
        /// </summary>
        public static void Multiply (
            ref Matrix44 m1, ref Matrix44 m2, out Matrix44 result)
        {
            result.R0C0 =
                (m1.R0C0 * m2.R0C0) + (m1.R0C1 * m2.R1C0) +
                (m1.R0C2 * m2.R2C0) + (m1.R0C3 * m2.R3C0);

            result.R0C1 =
                (m1.R0C0 * m2.R0C1) + (m1.R0C1 * m2.R1C1) +
                (m1.R0C2 * m2.R2C1) + (m1.R0C3 * m2.R3C1);

            result.R0C2 =
                (m1.R0C0 * m2.R0C2) + (m1.R0C1 * m2.R1C2) +
                (m1.R0C2 * m2.R2C2) + (m1.R0C3 * m2.R3C2);

            result.R0C3 =
                (m1.R0C0 * m2.R0C3) + (m1.R0C1 * m2.R1C3) +
                (m1.R0C2 * m2.R2C3) + (m1.R0C3 * m2.R3C3);

            result.R1C0 =
                (m1.R1C0 * m2.R0C0) + (m1.R1C1 * m2.R1C0) +
                (m1.R1C2 * m2.R2C0) + (m1.R1C3 * m2.R3C0);

            result.R1C1 =
                (m1.R1C0 * m2.R0C1) + (m1.R1C1 * m2.R1C1) +
                (m1.R1C2 * m2.R2C1) + (m1.R1C3 * m2.R3C1);

            result.R1C2 =
                (m1.R1C0 * m2.R0C2) + (m1.R1C1 * m2.R1C2) +
                (m1.R1C2 * m2.R2C2) + (m1.R1C3 * m2.R3C2);

            result.R1C3 =
                (m1.R1C0 * m2.R0C3) + (m1.R1C1 * m2.R1C3) +
                (m1.R1C2 * m2.R2C3) + (m1.R1C3 * m2.R3C3);

            result.R2C0 =
                (m1.R2C0 * m2.R0C0) + (m1.R2C1 * m2.R1C0) +
                (m1.R2C2 * m2.R2C0) + (m1.R2C3 * m2.R3C0);

            result.R2C1 =
                (m1.R2C0 * m2.R0C1) + (m1.R2C1 * m2.R1C1) +
                (m1.R2C2 * m2.R2C1) + (m1.R2C3 * m2.R3C1);

            result.R2C2 =
                (m1.R2C0 * m2.R0C2) + (m1.R2C1 * m2.R1C2) +
                (m1.R2C2 * m2.R2C2) + (m1.R2C3 * m2.R3C2);

            result.R2C3 =
                (m1.R2C0 * m2.R0C3) + (m1.R2C1 * m2.R1C3) +
                (m1.R2C2 * m2.R2C3) + (m1.R2C3 * m2.R3C3);

            result.R3C0 =
                (m1.R3C0 * m2.R0C0) + (m1.R3C1 * m2.R1C0) +
                (m1.R3C2 * m2.R2C0) + (m1.R3C3 * m2.R3C0);

            result.R3C1 =
                (m1.R3C0 * m2.R0C1) + (m1.R3C1 * m2.R1C1) +
                (m1.R3C2 * m2.R2C1) + (m1.R3C3 * m2.R3C1);

            result.R3C2 =
                (m1.R3C0 * m2.R0C2) + (m1.R3C1 * m2.R1C2) +
                (m1.R3C2 * m2.R2C2) + (m1.R3C3 * m2.R3C2);

            result.R3C3 =
                (m1.R3C0 * m2.R0C3) + (m1.R3C1 * m2.R1C3) +
                (m1.R3C2 * m2.R2C3) + (m1.R3C3 * m2.R3C3);
        }

        /// <summary>
        /// Performs multiplication of a Matrix44 object and a Fixed32
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Matrix44 matrix,
            ref Fixed32 scaleFactor,
            out Matrix44 result)
        {
            result.R0C0 = matrix.R0C0 * scaleFactor;
            result.R0C1 = matrix.R0C1 * scaleFactor;
            result.R0C2 = matrix.R0C2 * scaleFactor;
            result.R0C3 = matrix.R0C3 * scaleFactor;
            result.R1C0 = matrix.R1C0 * scaleFactor;
            result.R1C1 = matrix.R1C1 * scaleFactor;
            result.R1C2 = matrix.R1C2 * scaleFactor;
            result.R1C3 = matrix.R1C3 * scaleFactor;
            result.R2C0 = matrix.R2C0 * scaleFactor;
            result.R2C1 = matrix.R2C1 * scaleFactor;
            result.R2C2 = matrix.R2C2 * scaleFactor;
            result.R2C3 = matrix.R2C3 * scaleFactor;
            result.R3C0 = matrix.R3C0 * scaleFactor;
            result.R3C1 = matrix.R3C1 * scaleFactor;
            result.R3C2 = matrix.R3C2 * scaleFactor;
            result.R3C3 = matrix.R3C3 * scaleFactor;
        }

        /// <summary>
        /// Doing this might not produce what you expect, perhaps you should
        /// lerp between quaternions.
        /// </summary>
        public static void Lerp (
            ref Matrix44 m1, ref Matrix44 m2, ref Fixed32 amount,
            out Matrix44 result)
        {
            Fixed32 zero = 0;
            Fixed32 one = 1;

            if (amount < zero || amount > one)
                throw new ArgumentOutOfRangeException ();

            result.R0C0 = m1.R0C0 + ((m2.R0C0 - m1.R0C0) * amount);
            result.R0C1 = m1.R0C1 + ((m2.R0C1 - m1.R0C1) * amount);
            result.R0C2 = m1.R0C2 + ((m2.R0C2 - m1.R0C2) * amount);
            result.R0C3 = m1.R0C3 + ((m2.R0C3 - m1.R0C3) * amount);
            result.R1C0 = m1.R1C0 + ((m2.R1C0 - m1.R1C0) * amount);
            result.R1C1 = m1.R1C1 + ((m2.R1C1 - m1.R1C1) * amount);
            result.R1C2 = m1.R1C2 + ((m2.R1C2 - m1.R1C2) * amount);
            result.R1C3 = m1.R1C3 + ((m2.R1C3 - m1.R1C3) * amount);
            result.R2C0 = m1.R2C0 + ((m2.R2C0 - m1.R2C0) * amount);
            result.R2C1 = m1.R2C1 + ((m2.R2C1 - m1.R2C1) * amount);
            result.R2C2 = m1.R2C2 + ((m2.R2C2 - m1.R2C2) * amount);
            result.R2C3 = m1.R2C3 + ((m2.R2C3 - m1.R2C3) * amount);
            result.R3C0 = m1.R3C0 + ((m2.R3C0 - m1.R3C0) * amount);
            result.R3C1 = m1.R3C1 + ((m2.R3C1 - m1.R3C1) * amount);
            result.R3C2 = m1.R3C2 + ((m2.R3C2 - m1.R3C2) * amount);
            result.R3C3 = m1.R3C3 + ((m2.R3C3 - m1.R3C3) * amount);
        }

        /// <summary>
        /// A square matrix whose transpose is equal to itself is called a
        /// symmetric matrix.
        /// </summary>
        public Boolean IsSymmetric ()
        {
            Matrix44 transpose = this;
            Transpose (ref transpose, out transpose);
            return (transpose == this);
        }

        /// <summary>
        /// A square matrix whose transpose is equal to its negative is called
        /// a skew-symmetric matrix.
        /// </summary>
        public Boolean IsSkewSymmetric ()
        {
            Matrix44 transpose = this;
            Transpose (ref transpose, out transpose);
            return (transpose == -this);
        }

#if (VARIANTS_ENABLED)

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateTranslation (Vector3 position)
        {
            Matrix44 result;
            CreateTranslation (ref position, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateTranslation (
            Fixed32 xPosition,
            Fixed32 yPosition,
            Fixed32 zPosition)
        {
            Matrix44 result;
            CreateTranslation (
                ref xPosition, ref yPosition, ref zPosition,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateScale (
            Fixed32 xScale,
            Fixed32 yScale,
            Fixed32 zScale)
        {
            Matrix44 result;
            CreateScale (
                ref xScale, ref yScale, ref zScale,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateScale (Vector3 scales)
        {
            Matrix44 result;
            CreateScale (ref scales, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateScale (Fixed32 scale)
        {
            Matrix44 result;
            CreateScale (ref scale, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateRotationX (Fixed32 radians)
        {
            Matrix44 result;
            CreateRotationX (ref radians, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateRotationY (Fixed32 radians)
        {
            Matrix44 result;
            CreateRotationY (ref radians, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateRotationZ (Fixed32 radians)
        {
            Matrix44 result;
            CreateRotationZ (ref radians, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateFromAxisAngle (
            Vector3 axis,
            Fixed32 angle)
        {
            Matrix44 result;
            CreateFromAxisAngle (ref axis, ref angle, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateFromCartesianAxes (
            Vector3 right,
            Vector3 up,
            Vector3 backward)
        {
            Matrix44 result;
            CreateFromCartesianAxes (
                ref right, ref up, ref backward, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateWorld (
            Vector3 position,
            Vector3 forward,
            Vector3 up)
        {
            Matrix44 result;
            CreateWorld (ref position, ref forward, ref up, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateFromQuaternion (Quaternion quaternion)
        {
            Matrix44 result;
            CreateFromQuaternion (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateFromYawPitchRoll (
            Fixed32 yaw,
            Fixed32 pitch,
            Fixed32 roll)
        {
            Matrix44 result;
            CreateFromYawPitchRoll (ref yaw, ref pitch, ref roll, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateBillboard (
            Vector3 objectPosition,
            Vector3 cameraPosition,
            Vector3 cameraUpVector,
            Vector3? cameraForwardVector)
        {
            Matrix44 result;
            CreateBillboard (
                ref objectPosition, ref cameraPosition,
                ref cameraUpVector, ref cameraForwardVector,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateConstrainedBillboard (
            Vector3 objectPosition,
            Vector3 cameraPosition,
            Vector3 rotateAxis,
            Vector3? cameraForwardVector,
            Vector3? objectForwardVector)
        {
            Matrix44 result;
            CreateConstrainedBillboard (
                ref objectPosition, ref cameraPosition,
                ref rotateAxis, ref cameraForwardVector, ref objectForwardVector,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreatePerspectiveFieldOfView (
            Fixed32 fieldOfView,
            Fixed32 aspectRatio,
            Fixed32 nearPlane,
            Fixed32 farPlane)
        {
            Matrix44 result;
            CreatePerspectiveFieldOfView (
                ref fieldOfView, ref aspectRatio, ref nearPlane, ref farPlane,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreatePerspective (
            Fixed32 width,
            Fixed32 height,
            Fixed32 nearPlane,
            Fixed32 farPlane)
        {
            Matrix44 result;
            CreatePerspective (
                ref width, ref height, ref nearPlane, ref farPlane,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreatePerspectiveOffCenter (
            Fixed32 left,
            Fixed32 right,
            Fixed32 bottom,
            Fixed32 top,
            Fixed32 nearPlane,
            Fixed32 farPlane)
        {
            Matrix44 result;
            CreatePerspectiveOffCenter (
                ref left, ref right, ref bottom,
                ref top, ref nearPlane, ref farPlane,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateOrthographic (
            Fixed32 width,
            Fixed32 height,
            Fixed32 nearPlane,
            Fixed32 farPlane)
        {
            Matrix44 result;
            CreateOrthographic (
                ref width, ref height, ref nearPlane, ref farPlane,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateOrthographicOffCenter (
            Fixed32 left,
            Fixed32 right,
            Fixed32 bottom,
            Fixed32 top,
            Fixed32 nearPlane,
            Fixed32 farPlane)
        {
            Matrix44 result;
            CreateOrthographicOffCenter (
                ref left, ref right, ref bottom,
                ref top, ref nearPlane, ref farPlane,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateLookAt (
            Vector3 cameraPosition,
            Vector3 cameraTarget,
            Vector3 cameraUpVector)
        {
            Matrix44 result;
            CreateLookAt (
                ref cameraPosition, ref cameraTarget, ref cameraUpVector,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Transpose (Matrix44 input)
        {
            Matrix44 result;
            Transpose (ref input, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Fixed32 Determinant (Matrix44 matrix)
        {
            Fixed32 result;
            Determinant (ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Invert (Matrix44 matrix)
        {
            Matrix44 result;
            Invert (ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Transform (Matrix44 value, Quaternion rotation)
        {
            Matrix44 result;
            Transform (ref value, ref rotation, out result);
            return result;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean Equals (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Boolean result;
            Equals (ref matrix1, ref matrix2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator == (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Boolean result;
            Equals (ref matrix1, ref matrix2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator != (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Boolean result;
            Equals (ref matrix1, ref matrix2, out result);
            return !result;
        }

        // Variant Addition Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Add (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            Add (ref matrix1, ref matrix2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator + (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            Add (ref matrix1, ref matrix2, out result);
            return result;
        }

        // Variant Subtraction Operators //-----------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Subtract (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            Subtract (ref matrix1, ref matrix2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator - (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            Subtract (ref matrix1, ref matrix2, out result);
            return result;
        }

        // Variant Negation Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Negate (Matrix44 matrix)
        {
            Matrix44 result;
            Negate (ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator - (Matrix44 matrix)
        {
            Matrix44 result;
            Negate (ref matrix, out result);
            return result;
        }

        // Variant Multiplication Operators //--------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Multiply (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            Multiply (ref matrix1, ref matrix2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Multiply (
            Matrix44 matrix, Fixed32 scaleFactor)
        {
            Matrix44 result;
            Multiply (ref matrix, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator * (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            Multiply (ref matrix1, ref matrix2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator * (
            Matrix44 matrix, Fixed32 scaleFactor)
        {
            Matrix44 result;
            Multiply (ref matrix, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator * (
            Fixed32 scaleFactor, Matrix44 matrix)
        {
            Matrix44 result;
            Multiply (ref matrix, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Lerp (
            Matrix44 matrix1,
            Matrix44 matrix2,
            Fixed32 amount)
        {
            Matrix44 result;
            Lerp (ref matrix1, ref matrix2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public void Transpose ()
        {
            Transpose (ref this, out this);
        }

#endif
    }

    internal static class Int32Extensions
    {
        // http://msdn.microsoft.com/en-us/library/system.object.gethashcode(v=vs.110).aspx
        public static Int32 ShiftAndWrap (
            this Int32 value, Int32 positions = 2)
        {
            positions = positions & 0x1F;

            // Save the existing bit pattern, but interpret it as an unsigned
            // integer.
            uint number = BitConverter.ToUInt32(
                BitConverter.GetBytes(value), 0);
            // Preserve the bits to be discarded.
            uint wrapped = number >> (32 - positions);
            // Shift and wrap the discarded bits.
            return BitConverter.ToInt32 (
                BitConverter.GetBytes ((number << positions) | wrapped), 0);
        }
    }

    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a random Fixed32 between 0.0 & 1.0
        /// </summary>
        public static Fixed32 NextFixed32(this System.Random r)
        {
            Fixed32 max = 1;
            Fixed32 min = 0;

            Int32 randomRawValue =
                r.Next(min.RawValue, max.RawValue);

            return Fixed32.CreateFromRaw(randomRawValue);
        }
    }
}
