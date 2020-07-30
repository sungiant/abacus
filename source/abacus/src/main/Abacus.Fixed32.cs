// ┌────────────────────────────────────────────────────────────────────────┐ \\
// │    _____ ___.                                                          │ \\
// │   /  _  \\_ |__ _____    ____  __ __  ______                           │ \\
// │  /  /_\  \| __ \\__  \ _/ ___\|  |  \/  ___/                           │ \\
// │ /    |    \ \_\ \/ __ \\  \___|  |  /\___ \                            │ \\
// │ \____|__  /___  (____  /\___  >____//____  >                           │ \\
// │         \/    \/     \/     \/           \/  v1.0.1                    │ \\
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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Globalization;

namespace Abacus.Fixed32Precision
{
    /// <summary>
    /// 32 bit signed Q16.16 number.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct Fixed32 : IEquatable<Fixed32> {
        public const Byte S = 1;  // number of sign bits
        public const Byte M = 15; // number of integer bits
        public const Byte N = 16; // number of fractional bits

        Int32 numerator; // raw integer data

        const Int32 denominator = (Int32) 1 << N;

        public static readonly Fixed32 Epsilon = CreateRaw (10);
        public static readonly Fixed32 Resolution = CreateRaw (1);
        public static readonly Fixed32 MaxValue = CreateRaw (Int32.MaxValue);
        public static readonly Fixed32 MinValue = CreateRaw (Int32.MinValue);

        public Int32 Numerator   { get { return numerator; } set { numerator = value; } }
        public Int32 Denominator { get { return denominator; } }
        public Int32 High        { get { return numerator >> N; } }
        public Int32 Low         { get { return numerator - (High << N); } }

        public static Fixed32 CreateRaw (Int32 value) { Fixed32 f; f.numerator = value; return f; }

        public override Boolean Equals(object obj) {
            if (obj is Fixed32)
                return ((Fixed32)obj).numerator == numerator;
            return false;
        }

        public override Int32 GetHashCode () { return numerator.GetHashCode(); }
        
        public Boolean Equals               (Fixed32 other) { return this.numerator == other.numerator; }
        public Boolean GreaterThan          (Fixed32 other) { return this.numerator >  other.numerator; }
        public Boolean GreaterThanOrEqualTo (Fixed32 other) { return this.numerator >= other.numerator; }
        public Boolean LessThan             (Fixed32 other) { return this.numerator <  other.numerator; }
        public Boolean LessThanOrEqualTo    (Fixed32 other) { return this.numerator <= other.numerator; }

        public static Boolean IsInfinity (Fixed32 f) { return false; }
        public static Boolean IsNegativeInfinity (Fixed32 f) { return false; }
        public static Boolean IsPositiveInfinity (Fixed32 f) { return false; }
        public static Boolean IsNaN (Fixed32 f) { return false; }
        public static Boolean IsNegative (Fixed32 f) { return f < 0; }


        // Piggy back //------------------------------------------------------//

        public static Boolean TryParse (string s, out Fixed32 result) { Double d = 0.0; Boolean ok = Double.TryParse (s, NumberStyles.Any, null, out d); result = d; return ok; }
    
        public override String ToString () { return ToDouble().ToString(); }

        // Conversions //-----------------------------------------------------//

        public static void CreateFrom (Int32 value, out Fixed32 result) {
            result.numerator = (Int32) value << N;
        }

        public static void CreateFrom (Int64 value, out Fixed32 result) {
            result.numerator = (Int32) value << N;
        }

        public static void CreateFrom (Single value, out Fixed32 result) {
            Int64 temp = (Int64) Math.Round (value * denominator);
            Saturate (ref temp, out result.numerator);
        }

        public static void CreateFrom (Double value, out Fixed32 result) {
            Int64 temp = (Int64) Math.Round (value * denominator);
            Saturate (ref temp, out result.numerator);
        }

        public Int32  ToInt32  () { return (Int32) (numerator >> N); }
        public Int64  ToInt64  () { return (Int64) (numerator >> N); }
        public Single ToSingle () { return invds * (Single) numerator; }
        public Double ToDouble () { return invdd * (Double) numerator; }

        // Maths //-----------------------------------------------------------//

        public static void Add (ref Fixed32 a, ref Fixed32 b, out Fixed32 result) {
            // Based on: https://en.wikipedia.org/wiki/Q_(number_format)#Addition
            Int32 temp = a.numerator + b.numerator;
            // with improved satuturation based on: https://codereview.stackexchange.com/questions/115869/saturated-signed-addition
            int w = (sizeof (Int32) << 3) - 1;
            Int32 mask = (~(a.numerator ^ b.numerator) & (a.numerator ^ temp)) >> w;
            Int32 max_min = (temp >> w) ^ (((Int32) 1) << w);
            result.numerator = (~mask & temp) + (mask & max_min);
        }

        public static void Subtract (ref Fixed32 a, ref Fixed32 b, out Fixed32 result) {
            // Based on: https://en.wikipedia.org/wiki/Q_(number_format)#Subtraction
            Int32 temp = a.numerator - b.numerator;
            // with improved satuturation based on: https://codereview.stackexchange.com/questions/115869/saturated-signed-addition
            int w = (sizeof (Int32) << 3) - 1;
            Int32 mask = ((a.numerator ^ b.numerator) & (a.numerator ^ temp)) >> w;
            Int32 max_min = (temp >> w) ^ (((Int32) 1) << w);
            result.numerator = (~mask & temp) + (mask & max_min);
        }

        public static void Multiply (ref Fixed32 a, ref Fixed32 b, out Fixed32 result) {
            // Based on: https://en.wikipedia.org/wiki/Q_(number_format)#Multiplication
            //// precomputed value:
            //#define K   (1 << (Q - 1))
            //// saturate to range of int16_t
            //int16_t sat16(int32_t x)
            //{
            //  if (x > 0x7FFF) return 0x7FFF;
            //  else if (x < -0x8000) return -0x8000;
            //  else return (int16_t)x;
            //}
            //int16_t q_mul(int16_t a, int16_t b)
            //{
            //  int16_t result;
            //  int32_t temp;
            //  temp = (int32_t)a * (int32_t)b; // result type is operand's type
            //  // Rounding; mid values are rounded up
            //  temp += K;
            //  // Correct by dividing by base and saturate result
            //  result = sat16(temp >> Q);
            //  return result;
            //}
            Int64 temp = (Int64) a.numerator * (Int64) b.numerator;
            temp += big_k;
            temp = temp >> N;
            Saturate (ref temp, out result.numerator);
        }

        public static void Divide (ref Fixed32 a, ref Fixed32 b, out Fixed32 result) {
            // Based on: https://en.wikipedia.org/wiki/Q_(number_format)#Division
            //int16_t q_div(int16_t a, int16_t b)
            //{
            //  /* pre-multiply by the base (Upscale to Q16 so that the result will be in Q8 format) */
            //  int32_t temp = (int32_t)a << Q;
            //  /* Rounding: mid values are rounded up (down for negative values). */
            //  /* OR compare most significant bits i.e. if (((temp >> 31) & 1) == ((b >> 15) & 1)) */
            //  if ((temp >= 0 && b >= 0) || (temp < 0 && b < 0)) {   
            //    temp += b / 2;    /* OR shift 1 bit i.e. temp += (b >> 1); */
            //  } else {
            //    temp -= b / 2;    /* OR shift 1 bit i.e. temp -= (b >> 1); */
            //  }
            //  return (int16_t)(temp / b);
            //}
            if (b.numerator == 0) throw new DivideByZeroException ();
            Int64 big_a = (Int64) a.numerator;
            Int64 big_b = (Int64) b.numerator;
            Int64 temp = big_a << N;
            if ((temp >= 0 && big_b >= 0) || (temp < 0 && big_b < 0)) { temp = temp + (big_b / 2); }
            else { temp = temp - (big_b / 2); }
            temp = temp / big_b;
            Saturate (ref temp, out result.numerator);
        }

        public static void Modulo (ref Fixed32 a, ref Fixed32 b, out Fixed32 result) {
            Int64 temp = (Int64)a.numerator % (Int64)b.numerator;
            Saturate (ref temp, out result.numerator);
        }

        public static void Negate (ref Fixed32 f, out Fixed32 result) {
            result.numerator = (f.numerator == Int32.MinValue)
                ? Int32.MaxValue // overflow case
                : -f.numerator;
        }

        public static void Sqrt (ref Fixed32 f, out Fixed32 result) {
            // Based on: https://groups.google.com/forum/?hl=fr%05aacf5997b615c37&fromgroups#!topic/comp.lang.c/IpwKbw0MAxw/discussion
            ///*
            // * long sqrtL2L( long X );
            // *
            // * Long to long point square roots.
            // * RETURNS the integer square root of X (long).
            // * REMAINDER is in the local variable r of type long on return.  
            // * REQUIRES X is positive.
            // *
            // * Christophe MEESSEN, 1993.
            // */
            //long sqrtL2L( long X ) {
            //
            //  unsigned long t, q, b, r;
            //
            //  r = X;
            //  b = 0x40000000;
            //  q = 0;
            //
            //  while( b >= 256 ) {
            //    t = q + b;
            //    q = q / 2;     /* shift right 1 bit */
            //    if( r >= t ) {
            //      r = r - t;
            //      q = q + b;
            //    }
            //    b = b / 4;     /* shift right 2 bits */
            //  }
            //
            //  return( q );
            //}

            if (f.numerator <= 0) { result = 0; return; }
            UInt32 t, q, b, r;
            r = (UInt32) f.numerator;
            b = 1073741824; // http://www.thealmightyguru.com/Pointless/PowersOf2.html
            q = 0;
            while (b >= 256) {
                t = q + b;
                if (r >= t) {
                    r -= t;
                    q = t + b;
                }
                r <<= 1;
                b >>= 1;
            }
            q >>= 8;
            result.numerator = (Int32) q;
        }

        public static void Sin (ref Fixed32 f, out Fixed32 result) {
            Fixed32 Tau = Fixed32.CreateFrom (6.28318530717958647692528676656);
            Fixed32 Pi = Fixed32.CreateFrom (3.14159265358979323846264338328);
            Fixed32 HalfPi = Fixed32.CreateFrom (1.57079632679489661923132169164);
            // Based on: https://stackoverflow.com/questions/605124/fixed-point-math-in-c
            Fixed32 fx = f;

            for (; fx < 0; fx += Tau);

            if (fx > Tau)
                fx %= Tau;

            if (fx <= HalfPi) {
                SinLookup (ref fx, out result);
                return;
            }
            
            if (fx <= Pi) {
                fx = Pi - fx;
                SinLookup (ref fx, out result);
                return;
            }
            
            if (fx <= HalfPi * 3) {
                fx = fx - Pi;
                SinLookup (ref fx, out result);
                result = -result;
                return;
            }
            
            fx = Tau - fx;
            SinLookup (ref fx, out result);
            result = -result;
            return;
        }

        public static void Cos (ref Fixed32 f, out Fixed32 result) {
            Fixed32 HalfPi = Fixed32.CreateFrom (1.57079632679489661923132169164);
            Fixed32 fx = HalfPi - f;
            Sin (ref fx, out result);
        }

        public static void Tan (ref Fixed32 f, out Fixed32 result) {
            Fixed32 s, c;
            Sin (ref f, out s);
            Cos (ref f, out c);
            if (c < Epsilon && c > -Epsilon) {
                result = 0;
                return;
            }
            result = s / c;
        }

        public static void ArcSin (ref Fixed32 f, out Fixed32 result) {
            // From the half-angle formula: https://en.wikipedia.org/wiki/Inverse_trigonometric_functions
            // arcsin (f) == 2 * arctan (f / (1 + √(1 - f²))) : -1 <= f <= 1
            if (f < -1 || f > 1) throw new ArgumentOutOfRangeException ();
            result = 1 - f * f;
            Sqrt (ref result, out result);
            result += 1;
            result = f / result;
            ArcTan (ref result, out result);
            result *= 2;
        }

        public static void ArcCos (ref Fixed32 f, out Fixed32 result) {
            // From the half-angle formula: https://en.wikipedia.org/wiki/Inverse_trigonometric_functions
            // arccos (f) == 2 * arctan (√(1 - f²) / (1 + f)) : -1 <= f <= 1
            if (f < -1 || f > 1) throw new ArgumentOutOfRangeException ();
            result = 1 - f * f;
            Sqrt (ref result, out result);
            result /= f + 1;
            ArcTan (ref result, out result);
            result *= 2;
        }

        public static void ArcTan (ref Fixed32 f, out Fixed32 result) {
            // ArcTan approximation implemented using appropriate Tayor series expansion: https://proofwiki.org/wiki/Power_Series_Expansion_for_Real_Arctangent_Function
            // best accuracy for which falls within the range of -1 <= f <= 1, see: https://spin.atomicobject.com/2012/04/24/implementing-advanced-math-functions/
            // Valid input for the ArcTan function falls within the range of -∞ < f < ∞,
            // trig identities are used to facilitate performing the approximation within the most accurate range: https://en.wikipedia.org/wiki/Inverse_trigonometric_functions
                        Fixed32 HalfPi = Fixed32.CreateFrom (1.57079632679489661923132169164);
            Fixed32 temp = f;
            Boolean use_negative_identity = temp < 0;
            if (use_negative_identity) temp = -temp;
            Boolean use_reciprocal_identity = temp > 1;
            if (use_reciprocal_identity) temp = 1 / temp;
            Fixed32 tt = temp * temp;
            Fixed32 numerator = temp;
            Fixed32 denominator = 1;
            result = temp;                                         // arctan (f) =~ f - (f³/3) - (f⁵/5) - (f⁷/7) - (f⁹/9) ... : -1 <= f <= 1
            for (int i = 0; i < 32 / 2; ++i) {
                numerator *= tt;
                denominator += 2;
                temp = numerator / denominator;
                if (temp == 0) break;
                result -= temp;
                numerator *= tt;
                denominator += 2;
                temp = numerator / denominator;
                if (temp == 0) break;
                result += temp;
            }
            if (use_reciprocal_identity) result = HalfPi - result; // arctan (f) + arctan (1/f) == π/2
            if (use_negative_identity) result = -result;           // arctan (-f) == -arctan (f)
        }


        // Internal //--------------------------------------------------------//

        static readonly Int32 k = 1 << (N - 1);

        static readonly Double invdd = 1.0  / (Double) denominator;
        static readonly Single invds = 1.0f / (Single) denominator;

        static readonly Int64 big_k = (Int64) k;
        static readonly Int64 bigMin = (Int64) Int32.MinValue;
        static readonly Int64 bigMax = (Int64) Int32.MaxValue;

        static void Saturate (ref Int64 big, out Int32 result) {
            if (big < bigMin) { result = Int32.MinValue; return; }
            if (big > bigMax) { result = Int32.MaxValue; return; }
            result = (Int32) big;
        }
        static void SinLookup (ref Fixed32 rad, out Fixed32 result) {
            Fixed32 Rad2Deg = Fixed32.CreateFrom (57.29577951308232087679815481409);
            Fixed32 deg = rad * Rad2Deg;
            Int32 p = (Int32) deg.ToInt32 ();

            if (p == 90) {
                result.numerator = sinLUT[p];
                return;
            }
            Int32 lowi = sinLUT[p];
            Int32 highi = sinLUT[p + 1];

            Fixed32 q = deg - (Fixed32) p;

            // Lerp between two values
            Fixed32 low = CreateRaw (sinLUT[p]);
            Fixed32 high = CreateRaw (sinLUT[p + 1]);
            result = low + q * (high - low);
        }

        static readonly Int32[] sinLUT = {
            0x0,
            0x478, 0x8EF, 0xD66, 0x11DC, 0x1650, 0x1AC2, 0x1F33, 0x23A1, 0x280C,
            0x2C74, 0x30D9, 0x353A, 0x3996, 0x3DEF, 0x4242, 0x4690, 0x4AD9, 0x4F1C,
            0x5358, 0x578F, 0x5BBE, 0x5FE6, 0x6407, 0x6820, 0x6C31, 0x7039, 0x7439,
            0x782F, 0x7C1C, 0x8000, 0x83DA, 0x87A9, 0x8B6D, 0x8F27, 0x92D6, 0x9679,
            0x9A11, 0x9D9C, 0xA11B, 0xA48E, 0xA7F3, 0xAB4C, 0xAE97, 0xB1D5, 0xB505,
            0xB827, 0xBB3A, 0xBE3F, 0xC135, 0xC41B, 0xC6F3, 0xC9BB, 0xCC73, 0xCF1C,
            0xD1B4, 0xD43C, 0xD6B3, 0xD91A, 0xDB6F, 0xDDB4, 0xDFE7, 0xE209, 0xE419,
            0xE617, 0xE804, 0xE9DE, 0xEBA6, 0xED5C, 0xEEFF, 0xF090, 0xF20E, 0xF378,
            0xF4D0, 0xF615, 0xF747, 0xF865, 0xF970, 0xFA68, 0xFB4C, 0xFC1C, 0xFCD9,
            0xFD82, 0xFE18, 0xFE99, 0xFF07, 0xFF60, 0xFFA6, 0xFFD8, 0xFFF6, 0x10000,
            };


        // Function Variants //-----------------------------------------------//

        public static Fixed32 Add      (Fixed32 a, Fixed32 b) { Fixed32 result; Add (ref a, ref b, out result); return result; }
        public static Fixed32 Subtract (Fixed32 a, Fixed32 b) { Fixed32 result; Subtract (ref a, ref b, out result); return result; }
        public static Fixed32 Multiply (Fixed32 a, Fixed32 b) { Fixed32 result; Multiply (ref a, ref b, out result); return result; }
        public static Fixed32 Divide   (Fixed32 a, Fixed32 b) { Fixed32 result; Divide (ref a, ref b, out result); return result; }
        public static Fixed32 Modulo   (Fixed32 a, Fixed32 b) { Fixed32 result; Modulo (ref a, ref b, out result); return result; }
        public static Fixed32 Negate   (Fixed32 f) { Fixed32 result; Negate (ref f, out result); return result; }
        
        public static Fixed32 operator  + (Fixed32 a, Fixed32 b) { Fixed32 result; Add (ref a, ref b, out result); return result; }
        public static Fixed32 operator  - (Fixed32 a, Fixed32 b) { Fixed32 result; Subtract (ref a, ref b, out result); return result; }
        public static Fixed32 operator  * (Fixed32 a, Fixed32 b) { Fixed32 result; Multiply (ref a, ref b, out result); return result; }
        public static Fixed32 operator  / (Fixed32 a, Fixed32 b) { Fixed32 result; Divide (ref a, ref b, out result); return result; }
        public static Fixed32 operator  % (Fixed32 a, Fixed32 b) { Fixed32 result; Modulo (ref a, ref b, out result); return result; }
        public static Fixed32 operator  - (Fixed32 f) { Fixed32 result; Negate (ref f, out result); return result; }
        public static Fixed32 operator  + (Fixed32 f) { return f; }

        public static Fixed32 Sqrt     (Fixed32 f) { Fixed32 result; Sqrt (ref f, out result); return result; }

        public static Fixed32 Sin      (Fixed32 f) { Fixed32 result; Sin  (ref f, out result); return result; }
        public static Fixed32 Cos      (Fixed32 f) { Fixed32 result; Cos  (ref f, out result); return result; }
        public static Fixed32 Tan      (Fixed32 f) { Fixed32 result; Tan  (ref f, out result); return result; }

        public static Fixed32 ArcSin   (Fixed32 f) { Fixed32 result; ArcSin  (ref f, out result); return result; }
        public static Fixed32 ArcCos   (Fixed32 f) { Fixed32 result; ArcCos  (ref f, out result); return result; }
        public static Fixed32 ArcTan   (Fixed32 f) { Fixed32 result; ArcTan  (ref f, out result); return result; }

        public static Boolean operator == (Fixed32 a, Fixed32 b) { return a.Equals (b); }
        public static Boolean operator != (Fixed32 a, Fixed32 b) { return !a.Equals (b); }
        public static Boolean operator >= (Fixed32 a, Fixed32 b) { return a.GreaterThanOrEqualTo (b); }
        public static Boolean operator <= (Fixed32 a, Fixed32 b) { return a.LessThanOrEqualTo (b); }
        public static Boolean operator  > (Fixed32 a, Fixed32 b) { return a.GreaterThan (b); }
        public static Boolean operator  < (Fixed32 a, Fixed32 b) { return a.LessThan (b); }

        public static explicit operator Int32  (Fixed32 f) { return f.ToInt32 (); }
        public static explicit operator Int64  (Fixed32 f) { return f.ToInt64 (); }
        public static explicit operator Single (Fixed32 f) { return f.ToSingle (); }
        public static explicit operator Double (Fixed32 f) { return f.ToDouble (); }

        public static implicit operator Fixed32 (Int32 v)  { Fixed32 f; CreateFrom (v, out f); return f; }
        public static implicit operator Fixed32 (Int64 v)  { Fixed32 f; CreateFrom (v, out f); return f; }
        public static implicit operator Fixed32 (Single v) { Fixed32 f; CreateFrom (v, out f); return f; }
        public static implicit operator Fixed32 (Double v) { Fixed32 f; CreateFrom (v, out f); return f; }

        public static Fixed32 CreateFrom (Int32 v)  { Fixed32 f; CreateFrom (v, out f); return f; }
        public static Fixed32 CreateFrom (Int64 v)  { Fixed32 f; CreateFrom (v, out f); return f; }
        public static Fixed32 CreateFrom (Single v) { Fixed32 f; CreateFrom (v, out f); return f; }
        public static Fixed32 CreateFrom (Double v) { Fixed32 f; CreateFrom (v, out f); return f; }
    }

    /// <summary>
    /// Fixed32 precision Quaternion.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Quaternion : IEquatable<Quaternion> {
        public Fixed32 I, J, K, U;

        public Quaternion (Fixed32 i, Fixed32 j, Fixed32 k, Fixed32 u) { I = i; J = j; K = k; U = u; }

        public Quaternion (Vector3 vectorPart, Fixed32 scalarPart) { I = vectorPart.X; J = vectorPart.Y; K = vectorPart.Z; U = scalarPart; }

        public override String ToString () { return String.Format ("(I:{0}, J:{1}, K:{2}, U:{3})", I, J, K, U); }

        public override Int32 GetHashCode () {
            return U.GetHashCode ().ShiftAndWrap (6) ^ K.GetHashCode ().ShiftAndWrap (4)
                 ^ J.GetHashCode ().ShiftAndWrap (2) ^ I.GetHashCode ();
        }

        public override Boolean Equals (Object obj) { return (obj is Quaternion) ? this.Equals ((Quaternion) obj) : false; }

        public Boolean Equals (Quaternion other) {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        // Constants //-------------------------------------------------------//

        static Quaternion identity, zero;

        static Quaternion () {
            identity = new Quaternion (0, 0, 0, 1);
            zero     = new Quaternion (0, 0, 0, 0);
        }

        public static Quaternion Identity { get { return identity; } }
        public static Quaternion Zero     { get { return zero; } }

        // Operators //-------------------------------------------------------//

        public static void Equals (ref Quaternion q1, ref Quaternion q2, out Boolean result) {
            result = (q1.I == q2.I) && (q1.J == q2.J) && (q1.K == q2.K) && (q1.U == q2.U);
        }

        public static void Add (ref Quaternion q1, ref Quaternion q2, out Quaternion result) {
            result.I = q1.I + q2.I; result.J = q1.J + q2.J; result.K = q1.K + q2.K; result.U = q1.U + q2.U;
        }

        public static void Subtract (ref Quaternion q1, ref Quaternion q2, out Quaternion result) {
            result.I = q1.I - q2.I; result.J = q1.J - q2.J; result.K = q1.K - q2.K; result.U = q1.U - q2.U;
        }

        public static void Negate (ref Quaternion quaternion, out Quaternion result) {
            result.I = -quaternion.I; result.J = -quaternion.J; result.K = -quaternion.K; result.U = -quaternion.U;
        }

        // http://www.euclideanspace.com/maths/algebra/realNormedAlgebra/quaternions/arithmetic/index.htm
        public static void Multiply (ref Quaternion q1, ref Quaternion q2, out Quaternion result) {
            result.I = q1.I * q2.U + q1.U * q2.I + q1.J * q2.K - q1.K * q2.J;
            result.J = q1.U * q2.J - q1.I * q2.K + q1.J * q2.U + q1.K * q2.I;
            result.K = q1.U * q2.K + q1.I * q2.J - q1.J * q2.I + q1.K * q2.U;
            result.U = q1.U * q2.U - q1.I * q2.I - q1.J * q2.J - q1.K * q2.K;
        }

#if (FUNCTION_VARIANTS)
        public static Boolean    operator == (Quaternion a, Quaternion b) { Boolean    result; Equals   (ref a, ref b, out result); return  result; }
        public static Boolean    operator != (Quaternion a, Quaternion b) { Boolean    result; Equals   (ref a, ref b, out result); return !result; }
        public static Quaternion operator  + (Quaternion a, Quaternion b) { Quaternion result; Add      (ref a, ref b, out result); return  result; }
        public static Quaternion operator  - (Quaternion a, Quaternion b) { Quaternion result; Subtract (ref a, ref b, out result); return  result; }
        public static Quaternion operator  - (Quaternion v)               { Quaternion result; Negate   (ref v,        out result); return  result; }
        public static Quaternion operator  * (Quaternion a, Quaternion b) { Quaternion result; Multiply (ref a, ref b, out result); return  result; }
        public static Quaternion operator  ~ (Quaternion v)               { Quaternion result; Normalise(ref v,        out result); return  result; }

        public static Boolean    Equals      (Quaternion a, Quaternion b) { Boolean    result; Equals   (ref a, ref b, out result); return  result; }
        public static Quaternion Add         (Quaternion a, Quaternion b) { Quaternion result; Add      (ref a, ref b, out result); return  result; }
        public static Quaternion Subtract    (Quaternion a, Quaternion b) { Quaternion result; Subtract (ref a, ref b, out result); return  result; }
        public static Quaternion Negate      (Quaternion v)               { Quaternion result; Negate   (ref v,        out result); return  result; }
        public static Quaternion Multiply    (Quaternion a, Quaternion b) { Quaternion result; Multiply (ref a, ref b, out result); return  result; }
#endif

        // Utilities //-------------------------------------------------------//

        public static void Lerp (ref Quaternion q1, ref Quaternion q2, ref Fixed32 amount, out Quaternion result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Fixed32 remaining = 1 - amount;
            Fixed32 r = remaining;
            Fixed32 a = amount;
            result.U = (r * q1.U) + (a * q2.U);
            result.I = (r * q1.I) + (a * q2.I);
            result.J = (r * q1.J) + (a * q2.J);
            result.K = (r * q1.K) + (a * q2.K);
        }

        // http://en.wikipedia.org/wiki/Slerp
        public static void Slerp (ref Quaternion q1, ref Quaternion q2, ref Fixed32 amount,out Quaternion result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Fixed32 remaining = 1 - amount;
            Fixed32 angle;
            Dot (ref q1, ref q2, out angle);
            if (angle < 0) {
                Negate (ref q1, out q1);
                angle = -angle;
            }
            Fixed32 theta = Maths.ArcCos (angle);
            Fixed32 r = remaining;
            Fixed32 a = amount;
            if (theta > Maths.Epsilon) {
                Fixed32 x = Maths.Sin (remaining * theta);
                Fixed32 y = Maths.Sin (amount * theta);
                Fixed32 z = Maths.Sin (theta);
                r = x / z;
                a = y / z;
            }
            result.U = (r * q1.U) + (a * q2.U);
            result.I = (r * q1.I) + (a * q2.I);
            result.J = (r * q1.J) + (a * q2.J);
            result.K = (r * q1.K) + (a * q2.K);
        }

        public static void IsUnit (ref Quaternion q, out Boolean result) {
            result = Maths.IsZero((Fixed32) 1 - q.U * q.U - q.I * q.I - q.J * q.J - q.K * q.K);
        }

#if (FUNCTION_VARIANTS)
        public bool IsUnit () { Boolean result; IsUnit (ref this, out result); return result; }

        public static Boolean    IsUnit (Quaternion q) { Boolean result; IsUnit (ref q, out result); return result; }
        public static Quaternion Lerp   (Quaternion a, Quaternion b, Fixed32 amount) { Quaternion result; Lerp (ref a, ref b, ref amount, out result); return result; }
        public static Quaternion Slerp  (Quaternion a, Quaternion b, Fixed32 amount) { Quaternion result; Slerp (ref a, ref b, ref amount, out result); return result; }

#endif

        // Maths //-----------------------------------------------------------//

        public static void LengthSquared (ref Quaternion quaternion, out Fixed32 result) {
            result = (quaternion.I * quaternion.I) + (quaternion.J * quaternion.J)
                   + (quaternion.K * quaternion.K) + (quaternion.U * quaternion.U);
        }

        public static void Length (ref Quaternion quaternion, out Fixed32 result) {
            Fixed32 lengthSquared = (quaternion.I * quaternion.I) + (quaternion.J * quaternion.J)
                                 + (quaternion.K * quaternion.K) + (quaternion.U * quaternion.U);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void Conjugate (ref Quaternion value, out Quaternion result) {
            result.I = -value.I; result.J = -value.J;
            result.K = -value.K; result.U = value.U;
        }

        public static void Inverse (ref Quaternion quaternion, out Quaternion result) {
            Fixed32 a = (quaternion.I * quaternion.I) + (quaternion.J * quaternion.J)
                     + (quaternion.K * quaternion.K) + (quaternion.U * quaternion.U);
            Fixed32 b = 1 / a;
            result.I = -quaternion.I * b; result.J = -quaternion.J * b;
            result.K = -quaternion.K * b; result.U =  quaternion.U * b;
        }

        public static void Dot (ref Quaternion q1, ref Quaternion q2, out Fixed32 result) {
            result = (q1.I * q2.I) + (q1.J * q2.J) + (q1.K * q2.K) + (q1.U * q2.U);
        }

        public static void Concatenate (ref Quaternion q1, ref Quaternion q2, out Quaternion result) {
            Fixed32 a = (q1.K * q2.J) - (q1.J * q2.K);
            Fixed32 b = (q1.I * q2.K) - (q1.K * q2.I);
            Fixed32 c = (q1.J * q2.I) - (q1.I * q2.J);
            Fixed32 d = (q1.I * q2.I) - (q1.J * q2.J);
            Fixed32 i = (q1.U * q2.I) + (q1.I * q2.U) + a;
            Fixed32 j = (q1.U * q2.J) + (q1.J * q2.U) + b;
            Fixed32 k = (q1.U * q2.K) + (q1.K * q2.U) + c;
            Fixed32 u = (q1.U * q2.U) - (q1.K * q2.K) - d;
            result.I = i; result.J = j; result.K = k; result.U = u;
        }

        public static void Normalise (ref Quaternion quaternion, out Quaternion result) {
            Fixed32 a = (quaternion.I * quaternion.I) + (quaternion.J * quaternion.J)
                     + (quaternion.K * quaternion.K) + (quaternion.U * quaternion.U);
            Fixed32 b = 1 / Maths.Sqrt (a);
            result.I = quaternion.I * b; result.J = quaternion.J * b;
            result.K = quaternion.K * b; result.U = quaternion.U * b;
        }

        public static void Transform (ref Quaternion rotation, ref Vector3 vector, out Vector3 result) {
            Fixed32 i = rotation.I, j = rotation.J, k = rotation.K, u = rotation.U;
            Fixed32 ii = i * i, jj = j * j, kk = k * k;
            Fixed32 ui = u * i, uj = u * j, uk = u * k;
            Fixed32 ij = i * j, ik = i * k, jk = j * k;
            Fixed32 x = vector.X - (2 * vector.X * (jj + kk)) + (2 * vector.Y * (ij - uk)) + (2 * vector.Z * (ik + uj));
            Fixed32 y = vector.Y + (2 * vector.X * (ij + uk)) - (2 * vector.Y * (ii + kk)) + (2 * vector.Z * (jk - ui));
            Fixed32 z = vector.Z + (2 * vector.X * (ik - uj)) + (2 * vector.Y * (jk + ui)) - (2 * vector.Z * (ii + jj));
            result.X = x; result.Y = y; result.Z = z;
        }

        public static void Transform (ref Quaternion rotation, ref Vector4 vector, out Vector4 result) {
            Fixed32 i = rotation.I, j = rotation.J, k = rotation.K, u = rotation.U;
            Fixed32 ii = i * i, jj = j * j, kk = k * k;
            Fixed32 ui = u * i, uj = u * j, uk = u * k;
            Fixed32 ij = i * j, ik = i * k, jk = j * k;
            Fixed32 x = vector.X - (vector.X * 2 * (jj + kk)) + (vector.Y * 2 * (ij - uk)) + (vector.Z * 2 * (ik + uj));
            Fixed32 y = vector.Y + (vector.X * 2 * (ij + uk)) - (vector.Y * 2 * (ii + kk)) + (vector.Z * 2 * (jk - ui));
            Fixed32 z = vector.Z + (vector.X * 2 * (ik - uj)) + (vector.Y * 2 * (jk + ui)) - (vector.Z * 2 * (ii + jj));
            Fixed32 w = vector.W;
            result.X = x; result.Y = y; result.Z = z; result.W = w;
        }

#if (FUNCTION_VARIANTS)
        public Fixed32     LengthSquared () { Fixed32 result; LengthSquared (ref this, out result); return result; }
        public Fixed32     Length        () { Fixed32 result; Length (ref this, out result); return result; }
        public void       Normalise     () { Normalise (ref this, out this); }
        public Quaternion Conjugate     () { Conjugate (ref this, out this); return this; }
        public Quaternion Inverse       () { Inverse (ref this, out this); return this; }
        public Fixed32     Dot           (Quaternion q) { Fixed32 result; Dot (ref this, ref q, out result); return result; }
        public Quaternion Concatenate   (Quaternion q) { Concatenate (ref this, ref q, out this); return this; }
        public Vector3    Transform     (Vector3 v) { Vector3 result; Transform (ref this, ref v, out result); return result; }
        public Vector4    Transform     (Vector4 v) { Vector4 result; Transform (ref this, ref v, out result); return result; }

        public static Fixed32     LengthSquared (Quaternion q) { Fixed32 result; LengthSquared (ref q, out result); return result; }
        public static Fixed32     Length        (Quaternion q) { Fixed32 result; Length (ref q, out result); return result; }
        public static Quaternion Normalise     (Quaternion q) { Quaternion result; Normalise (ref q, out result); return result; }
        public static Quaternion Conjugate     (Quaternion q) { Quaternion result; Conjugate (ref q, out result); return result; }
        public static Quaternion Inverse       (Quaternion q) { Quaternion result; Inverse (ref q, out result); return result; }
        public static Fixed32     Dot           (Quaternion a, Quaternion b) { Fixed32 result; Dot (ref a, ref b, out result); return result; }
        public static Quaternion Concatenate   (Quaternion a, Quaternion b) { Quaternion result; Concatenate (ref a, ref b, out result); return result; }
        public static Vector3    Transform     (Quaternion rotation, Vector3 v) { Vector3 result; Transform (ref rotation, ref v, out result); return result; }
        public static Vector4    Transform     (Quaternion rotation, Vector4 v) { Vector4 result; Transform (ref rotation, ref v, out result); return result; }
#endif
        // Creation //--------------------------------------------------------//

        public static void CreateFromAxisAngle (ref Vector3 axis, ref Fixed32 angle, out Quaternion result) {
            Fixed32 theta = angle * Maths.Half;
            Fixed32 sin = Maths.Sin (theta), cos = Maths.Cos (theta);
            result.I = axis.X * sin;
            result.J = axis.Y * sin;
            result.K = axis.Z * sin;
            result.U = cos;
        }

        public static void CreateFromYawPitchRoll (ref Fixed32 yaw, ref Fixed32 pitch, ref Fixed32 roll, out Quaternion result) {
            Fixed32 hr = roll * Maths.Half, hp = pitch * Maths.Half, hy = yaw * Maths.Half;
            Fixed32 shr = Maths.Sin (hr), chr = Maths.Cos (hr);
            Fixed32 shp = Maths.Sin (hp), chp = Maths.Cos (hp);
            Fixed32 shy = Maths.Sin (hy), chy = Maths.Cos (hy);
            result.I = (chy * shp * chr) + (shy * chp * shr);
            result.J = (shy * chp * chr) - (chy * shp * shr);
            result.K = (chy * chp * shr) - (shy * shp * chr);
            result.U = (chy * chp * chr) + (shy * shp * shr);
        }

        // http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/
        public static void CreateFromRotationMatrix (ref Matrix44 m, out Quaternion result) {
            Fixed32 tr = m.R0C0 + m.R1C1 + m.R2C2;
            if (tr > 0) {
                Fixed32 s = Maths.Sqrt (tr + 1) * 2;
                result.U = Maths.Quarter * s;
                result.I = (m.R1C2 - m.R2C1) / s;
                result.J = (m.R2C0 - m.R0C2) / s;
                result.K = (m.R0C1 - m.R1C0) / s;
            }
            else if ((m.R0C0 >= m.R1C1) && (m.R0C0 >= m.R2C2)) {
                Fixed32 s = Maths.Sqrt (1 + m.R0C0 - m.R1C1 - m.R2C2) * 2;
                result.U = (m.R1C2 - m.R2C1) / s;
                result.I = Maths.Quarter * s;
                result.J = (m.R0C1 + m.R1C0) / s;
                result.K = (m.R0C2 + m.R2C0) / s;
            }
            else if (m.R1C1 > m.R2C2) {
                Fixed32 s = Maths.Sqrt (1 + m.R1C1 - m.R0C0 - m.R2C2) * 2;
                result.U = (m.R2C0 - m.R0C2) / s;
                result.I = (m.R1C0 + m.R0C1) / s;
                result.J = Maths.Quarter * s;
                result.K = (m.R2C1 + m.R1C2) / s;
            }
            else {
                Fixed32 s = Maths.Sqrt (1 + m.R2C2 - m.R0C0 - m.R1C1) * 2;
                result.U = (m.R0C1 - m.R1C0) / s;
                result.I = (m.R2C0 + m.R0C2) / s;
                result.J = (m.R2C1 + m.R1C2) / s;
                result.K = Maths.Quarter * s;
            }
        }


#if (FUNCTION_VARIANTS)
        public static Quaternion CreateFromAxisAngle      (Vector3 axis, Fixed32 angle) { Quaternion result; CreateFromAxisAngle (ref axis, ref angle, out result); return result; }
        public static Quaternion CreateFromYawPitchRoll   (Fixed32 yaw, Fixed32 pitch, Fixed32 roll) { Quaternion result; CreateFromYawPitchRoll (ref yaw, ref pitch, ref roll, out result); return result; }
        public static Quaternion CreateFromRotationMatrix (Matrix44 matrix) { Quaternion result; CreateFromRotationMatrix (ref matrix, out result); return result; }
#endif
    }
    /// <summary>
    /// Fixed32 precision Matrix44.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Matrix44 : IEquatable<Matrix44> {
        public Fixed32 R0C0, R0C1, R0C2, R0C3;
        public Fixed32 R1C0, R1C1, R1C2, R1C3;
        public Fixed32 R2C0, R2C1, R2C2, R2C3;
        public Fixed32 R3C0, R3C1, R3C2, R3C3;

        public Matrix44 (
            Fixed32 m00, Fixed32 m01, Fixed32 m02, Fixed32 m03, Fixed32 m10, Fixed32 m11, Fixed32 m12, Fixed32 m13,
            Fixed32 m20, Fixed32 m21, Fixed32 m22, Fixed32 m23, Fixed32 m30, Fixed32 m31, Fixed32 m32, Fixed32 m33) {
            this.R0C0 = m00; this.R0C1 = m01; this.R0C2 = m02; this.R0C3 = m03;
            this.R1C0 = m10; this.R1C1 = m11; this.R1C2 = m12; this.R1C3 = m13;
            this.R2C0 = m20; this.R2C1 = m21; this.R2C2 = m22; this.R2C3 = m23;
            this.R3C0 = m30; this.R3C1 = m31; this.R3C2 = m32; this.R3C3 = m33;
        }

        public override String ToString () {
            return String.Format ("((R0C0:{0}, R0C1:{1}, R0C2:{2}, R0C3:{3}), ", this.R0C0, this.R0C1, this.R0C2, this.R0C3)
                 + String.Format  ("(R1C0:{0}, R1C1:{1}, R1C2:{2}, R1C3:{3}), ", this.R1C0, this.R1C1, this.R1C2, this.R1C3)
                 + String.Format  ("(R2C0:{0}, R2C1:{1}, R2C2:{2}, R2C3:{3}), ", this.R2C0, this.R2C1, this.R2C2, this.R2C3)
                 + String.Format  ("(R3C0:{0}, R3C1:{1}, R3C2:{2}, R3C3:{3}))",  this.R3C0, this.R3C1, this.R3C2, this.R3C3);
        }

        public override Int32 GetHashCode () {
            return R0C0.GetHashCode ()                  ^ R0C1.GetHashCode ().ShiftAndWrap (2)
                ^ R0C2.GetHashCode ().ShiftAndWrap (4)  ^ R0C3.GetHashCode ().ShiftAndWrap (6)
                ^ R1C0.GetHashCode ().ShiftAndWrap (8)  ^ R1C1.GetHashCode ().ShiftAndWrap (10)
                ^ R1C2.GetHashCode ().ShiftAndWrap (12) ^ R1C3.GetHashCode ().ShiftAndWrap (14)
                ^ R2C0.GetHashCode ().ShiftAndWrap (16) ^ R2C1.GetHashCode ().ShiftAndWrap (18)
                ^ R2C2.GetHashCode ().ShiftAndWrap (20) ^ R2C3.GetHashCode ().ShiftAndWrap (22)
                ^ R3C0.GetHashCode ().ShiftAndWrap (24) ^ R3C1.GetHashCode ().ShiftAndWrap (26)
                ^ R3C2.GetHashCode ().ShiftAndWrap (28) ^ R3C3.GetHashCode ().ShiftAndWrap (30);
        }

        public override Boolean Equals (Object obj) { return (obj is Matrix44) ? this.Equals ((Matrix44)obj) : false; }

        public Boolean Equals (Matrix44 other) {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        public Boolean IsSymmetric () {
            Matrix44 transpose = this;
            Transpose (ref transpose, out transpose);
            return transpose.Equals (this);
        }

        public Boolean IsSkewSymmetric () {
            Matrix44 transpose = this;
            Transpose (ref transpose, out transpose);
            Negate (ref transpose, out transpose);
            return transpose.Equals (this);
        }

        // Accessors //-------------------------------------------------------//

        public Vector3 Up          { get { return new Vector3 ( R1C0,  R1C1,  R1C2); } set { R1C0 =  value.X; R1C1 =  value.Y; R1C2 =  value.Z; } }
        public Vector3 Down        { get { return new Vector3 (-R1C0, -R1C1, -R1C2); } set { R1C0 = -value.X; R1C1 = -value.Y; R1C2 = -value.Z; } }
        public Vector3 Right       { get { return new Vector3 ( R0C0,  R0C1,  R0C2); } set { R0C0 =  value.X; R0C1 =  value.Y; R0C2 =  value.Z; } }
        public Vector3 Left        { get { return new Vector3 (-R0C0, -R0C1, -R0C2); } set { R0C0 = -value.X; R0C1 = -value.Y; R0C2 = -value.Z; } }
        public Vector3 Forward     { get { return new Vector3 (-R2C0, -R2C1, -R2C2); } set { R2C0 = -value.X; R2C1 = -value.Y; R2C2 = -value.Z; } }
        public Vector3 Backward    { get { return new Vector3 ( R2C0,  R2C1,  R2C2); } set { R2C0 =  value.X; R2C1 =  value.Y; R2C2 =  value.Z; } }
        public Vector3 Translation { get { return new Vector3 ( R3C0,  R3C1,  R3C2); } set { R3C0 =  value.X; R3C1 =  value.Y; R3C2 =  value.Z; } }

        // Constants //-------------------------------------------------------//

        static Matrix44 identity, zero;

        static Matrix44 () {
            identity = new Matrix44 (1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
            zero     = new Matrix44 (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        public static Matrix44 Identity { get { return identity; } }
        public static Matrix44 Zero     { get { return zero; } }

        // Operators //-------------------------------------------------------//

        public static void Equals (ref Matrix44 a, ref Matrix44 b, out Boolean result) {
            result = (a.R0C0 == b.R0C0) && (a.R1C1 == b.R1C1) &&
                     (a.R2C2 == b.R2C2) && (a.R3C3 == b.R3C3) &&
                     (a.R0C1 == b.R0C1) && (a.R0C2 == b.R0C2) &&
                     (a.R0C3 == b.R0C3) && (a.R1C0 == b.R1C0) &&
                     (a.R1C2 == b.R1C2) && (a.R1C3 == b.R1C3) &&
                     (a.R2C0 == b.R2C0) && (a.R2C1 == b.R2C1) &&
                     (a.R2C3 == b.R2C3) && (a.R3C0 == b.R3C0) &&
                     (a.R3C1 == b.R3C1) && (a.R3C2 == b.R3C2);
        }

        public static void Add (ref Matrix44 a, ref Matrix44 b, out Matrix44 result) {
            result.R0C0 = a.R0C0 + b.R0C0; result.R0C1 = a.R0C1 + b.R0C1;
            result.R0C2 = a.R0C2 + b.R0C2; result.R0C3 = a.R0C3 + b.R0C3;
            result.R1C0 = a.R1C0 + b.R1C0; result.R1C1 = a.R1C1 + b.R1C1;
            result.R1C2 = a.R1C2 + b.R1C2; result.R1C3 = a.R1C3 + b.R1C3;
            result.R2C0 = a.R2C0 + b.R2C0; result.R2C1 = a.R2C1 + b.R2C1;
            result.R2C2 = a.R2C2 + b.R2C2; result.R2C3 = a.R2C3 + b.R2C3;
            result.R3C0 = a.R3C0 + b.R3C0; result.R3C1 = a.R3C1 + b.R3C1;
            result.R3C2 = a.R3C2 + b.R3C2; result.R3C3 = a.R3C3 + b.R3C3;
        }

        public static void Subtract (ref Matrix44 a, ref Matrix44 b, out Matrix44 result) {
            result.R0C0 = a.R0C0 - b.R0C0; result.R0C1 = a.R0C1 - b.R0C1;
            result.R0C2 = a.R0C2 - b.R0C2; result.R0C3 = a.R0C3 - b.R0C3;
            result.R1C0 = a.R1C0 - b.R1C0; result.R1C1 = a.R1C1 - b.R1C1;
            result.R1C2 = a.R1C2 - b.R1C2; result.R1C3 = a.R1C3 - b.R1C3;
            result.R2C0 = a.R2C0 - b.R2C0; result.R2C1 = a.R2C1 - b.R2C1;
            result.R2C2 = a.R2C2 - b.R2C2; result.R2C3 = a.R2C3 - b.R2C3;
            result.R3C0 = a.R3C0 - b.R3C0; result.R3C1 = a.R3C1 - b.R3C1;
            result.R3C2 = a.R3C2 - b.R3C2; result.R3C3 = a.R3C3 - b.R3C3;
        }

        public static void Negate (ref Matrix44 m, out Matrix44 result) {
            result.R0C0 = -m.R0C0; result.R0C1 = -m.R0C1;
            result.R0C2 = -m.R0C2; result.R0C3 = -m.R0C3;
            result.R1C0 = -m.R1C0; result.R1C1 = -m.R1C1;
            result.R1C2 = -m.R1C2; result.R1C3 = -m.R1C3;
            result.R2C0 = -m.R2C0; result.R2C1 = -m.R2C1;
            result.R2C2 = -m.R2C2; result.R2C3 = -m.R2C3;
            result.R3C0 = -m.R3C0; result.R3C1 = -m.R3C1;
            result.R3C2 = -m.R3C2; result.R3C3 = -m.R3C3;
        }

        public static void Product (ref Matrix44 a, ref Matrix44 b, out Matrix44 result) {
            Fixed32 r0c0 = (a.R0C0 * b.R0C0) + (a.R0C1 * b.R1C0) + (a.R0C2 * b.R2C0) + (a.R0C3 * b.R3C0);
            Fixed32 r0c1 = (a.R0C0 * b.R0C1) + (a.R0C1 * b.R1C1) + (a.R0C2 * b.R2C1) + (a.R0C3 * b.R3C1);
            Fixed32 r0c2 = (a.R0C0 * b.R0C2) + (a.R0C1 * b.R1C2) + (a.R0C2 * b.R2C2) + (a.R0C3 * b.R3C2);
            Fixed32 r0c3 = (a.R0C0 * b.R0C3) + (a.R0C1 * b.R1C3) + (a.R0C2 * b.R2C3) + (a.R0C3 * b.R3C3);
            Fixed32 r1c0 = (a.R1C0 * b.R0C0) + (a.R1C1 * b.R1C0) + (a.R1C2 * b.R2C0) + (a.R1C3 * b.R3C0);
            Fixed32 r1c1 = (a.R1C0 * b.R0C1) + (a.R1C1 * b.R1C1) + (a.R1C2 * b.R2C1) + (a.R1C3 * b.R3C1);
            Fixed32 r1c2 = (a.R1C0 * b.R0C2) + (a.R1C1 * b.R1C2) + (a.R1C2 * b.R2C2) + (a.R1C3 * b.R3C2);
            Fixed32 r1c3 = (a.R1C0 * b.R0C3) + (a.R1C1 * b.R1C3) + (a.R1C2 * b.R2C3) + (a.R1C3 * b.R3C3);
            Fixed32 r2c0 = (a.R2C0 * b.R0C0) + (a.R2C1 * b.R1C0) + (a.R2C2 * b.R2C0) + (a.R2C3 * b.R3C0);
            Fixed32 r2c1 = (a.R2C0 * b.R0C1) + (a.R2C1 * b.R1C1) + (a.R2C2 * b.R2C1) + (a.R2C3 * b.R3C1);
            Fixed32 r2c2 = (a.R2C0 * b.R0C2) + (a.R2C1 * b.R1C2) + (a.R2C2 * b.R2C2) + (a.R2C3 * b.R3C2);
            Fixed32 r2c3 = (a.R2C0 * b.R0C3) + (a.R2C1 * b.R1C3) + (a.R2C2 * b.R2C3) + (a.R2C3 * b.R3C3);
            Fixed32 r3c0 = (a.R3C0 * b.R0C0) + (a.R3C1 * b.R1C0) + (a.R3C2 * b.R2C0) + (a.R3C3 * b.R3C0);
            Fixed32 r3c1 = (a.R3C0 * b.R0C1) + (a.R3C1 * b.R1C1) + (a.R3C2 * b.R2C1) + (a.R3C3 * b.R3C1);
            Fixed32 r3c2 = (a.R3C0 * b.R0C2) + (a.R3C1 * b.R1C2) + (a.R3C2 * b.R2C2) + (a.R3C3 * b.R3C2);
            Fixed32 r3c3 = (a.R3C0 * b.R0C3) + (a.R3C1 * b.R1C3) + (a.R3C2 * b.R2C3) + (a.R3C3 * b.R3C3);
            result.R0C0 = r0c0; result.R0C1 = r0c1; result.R0C2 = r0c2; result.R0C3 = r0c3;
            result.R1C0 = r1c0; result.R1C1 = r1c1; result.R1C2 = r1c2; result.R1C3 = r1c3;
            result.R2C0 = r2c0; result.R2C1 = r2c1; result.R2C2 = r2c2; result.R2C3 = r2c3;
            result.R3C0 = r3c0; result.R3C1 = r3c1; result.R3C2 = r3c2; result.R3C3 = r3c3; 
        }

        public static void Multiply (ref Matrix44 m, ref Fixed32 f, out Matrix44 result) {
            result.R0C0 = m.R0C0 * f; result.R0C1 = m.R0C1 * f;
            result.R0C2 = m.R0C2 * f; result.R0C3 = m.R0C3 * f;
            result.R1C0 = m.R1C0 * f; result.R1C1 = m.R1C1 * f;
            result.R1C2 = m.R1C2 * f; result.R1C3 = m.R1C3 * f;
            result.R2C0 = m.R2C0 * f; result.R2C1 = m.R2C1 * f;
            result.R2C2 = m.R2C2 * f; result.R2C3 = m.R2C3 * f;
            result.R3C0 = m.R3C0 * f; result.R3C1 = m.R3C1 * f;
            result.R3C2 = m.R3C2 * f; result.R3C3 = m.R3C3 * f;
        }

#if (FUNCTION_VARIANTS)
        public static Boolean  operator == (Matrix44 a, Matrix44 b) { Boolean result;  Equals   (ref a, ref b, out result); return  result; }
        public static Boolean  operator != (Matrix44 a, Matrix44 b) { Boolean result;  Equals   (ref a, ref b, out result); return !result; }
        public static Matrix44 operator  + (Matrix44 a, Matrix44 b) { Matrix44 result; Add      (ref a, ref b, out result); return  result; }
        public static Matrix44 operator  - (Matrix44 a, Matrix44 b) { Matrix44 result; Subtract (ref a, ref b, out result); return  result; }
        public static Matrix44 operator  - (Matrix44 v)             { Matrix44 result; Negate   (ref v, out result);        return  result; }
        public static Matrix44 operator  * (Matrix44 a, Matrix44 b) { Matrix44 result; Product  (ref a, ref b, out result); return  result; }
        public static Matrix44 operator  * (Matrix44 v, Fixed32 f)   { Matrix44 result; Multiply (ref v, ref f, out result); return  result; }
        public static Matrix44 operator  * (Fixed32 f, Matrix44 v)   { Matrix44 result; Multiply (ref v, ref f, out result); return  result; }

        public static Boolean  Equals      (Matrix44 a, Matrix44 b) { Boolean  result; Equals   (ref a, ref b, out result); return result; }
        public static Matrix44 Add         (Matrix44 a, Matrix44 b) { Matrix44 result; Add      (ref a, ref b, out result); return result; }
        public static Matrix44 Subtract    (Matrix44 a, Matrix44 b) { Matrix44 result; Subtract (ref a, ref b, out result); return result; }
        public static Matrix44 Negate      (Matrix44 v)             { Matrix44 result; Negate   (ref v, out result);        return result; }
        public static Matrix44 Product     (Matrix44 a, Matrix44 b) { Matrix44 result; Product  (ref a, ref b, out result); return result; }
        public static Matrix44 Multiply    (Matrix44 v, Fixed32 f)   { Matrix44 result; Multiply (ref v, ref f, out result); return result; }
#endif

        // Utilities //-------------------------------------------------------//

        public static void Lerp (ref Matrix44 a, ref Matrix44 b, ref Fixed32 amount, out Matrix44 result) {
            Debug.Assert (amount > 0 && amount <= 1);
            result.R0C0 = a.R0C0 + ((b.R0C0 - a.R0C0) * amount);
            result.R0C1 = a.R0C1 + ((b.R0C1 - a.R0C1) * amount);
            result.R0C2 = a.R0C2 + ((b.R0C2 - a.R0C2) * amount);
            result.R0C3 = a.R0C3 + ((b.R0C3 - a.R0C3) * amount);
            result.R1C0 = a.R1C0 + ((b.R1C0 - a.R1C0) * amount);
            result.R1C1 = a.R1C1 + ((b.R1C1 - a.R1C1) * amount);
            result.R1C2 = a.R1C2 + ((b.R1C2 - a.R1C2) * amount);
            result.R1C3 = a.R1C3 + ((b.R1C3 - a.R1C3) * amount);
            result.R2C0 = a.R2C0 + ((b.R2C0 - a.R2C0) * amount);
            result.R2C1 = a.R2C1 + ((b.R2C1 - a.R2C1) * amount);
            result.R2C2 = a.R2C2 + ((b.R2C2 - a.R2C2) * amount);
            result.R2C3 = a.R2C3 + ((b.R2C3 - a.R2C3) * amount);
            result.R3C0 = a.R3C0 + ((b.R3C0 - a.R3C0) * amount);
            result.R3C1 = a.R3C1 + ((b.R3C1 - a.R3C1) * amount);
            result.R3C2 = a.R3C2 + ((b.R3C2 - a.R3C2) * amount);
            result.R3C3 = a.R3C3 + ((b.R3C3 - a.R3C3) * amount);
        }
        
#if (FUNCTION_VARIANTS)
        public static Matrix44 Lerp (Matrix44 a, Matrix44 b, Fixed32 amount) { Matrix44 result; Lerp (ref a, ref b, ref amount, out result); return result; }
#endif

        // Maths //-----------------------------------------------------------//

        public static void Transpose (ref Matrix44 m, out Matrix44 result) {
            result.R0C0 = m.R0C0; result.R1C1 = m.R1C1;
            result.R2C2 = m.R2C2; result.R3C3 = m.R3C3;
            Fixed32 t = m.R0C1; result.R0C1 = m.R1C0; result.R1C0 = t;
                   t = m.R0C2; result.R0C2 = m.R2C0; result.R2C0 = t;
                   t = m.R0C3; result.R0C3 = m.R3C0; result.R3C0 = t;
                   t = m.R1C2; result.R1C2 = m.R2C1; result.R2C1 = t;
                   t = m.R1C3; result.R1C3 = m.R3C1; result.R3C1 = t;
                   t = m.R2C3; result.R2C3 = m.R3C2; result.R3C2 = t;
        }

        public static void Decompose (ref Matrix44 matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation, out Boolean result) {
            translation.X = matrix.R3C0; translation.Y = matrix.R3C1; translation.Z = matrix.R3C2;
            Vector3 a = new Vector3 (matrix.R0C0, matrix.R1C0, matrix.R2C0);
            Vector3 b = new Vector3 (matrix.R0C1, matrix.R1C1, matrix.R2C1);
            Vector3 c = new Vector3 (matrix.R0C2, matrix.R1C2, matrix.R2C2);
            Fixed32 aLen; Vector3.Length (ref a, out aLen); scale.X = aLen;
            Fixed32 bLen; Vector3.Length (ref b, out bLen); scale.Y = bLen;
            Fixed32 cLen; Vector3.Length (ref c, out cLen); scale.Z = cLen;
            if (Maths.IsZero (scale.X) || Maths.IsZero (scale.Y) || Maths.IsZero (scale.Z)) {
                rotation = Quaternion.Identity;
                result = false;
            }
            if (aLen < Maths.Epsilon) a = Vector3.Zero;
            else Vector3.Normalise (ref a, out a);
            if (bLen < Maths.Epsilon) b = Vector3.Zero;
            else Vector3.Normalise (ref b, out b);
            if (cLen < Maths.Epsilon) c = Vector3.Zero;
            else Vector3.Normalise (ref c, out c);
            Vector3 right = new Vector3 (a.X, b.X, c.X);
            Vector3 up = new Vector3 (a.Y, b.Y, c.Y);
            Vector3 backward = new Vector3 (a.Z, b.Z, c.Z);
            if (right.Equals (Vector3.Zero)) right = Vector3.Right;
            if (up.Equals (Vector3.Zero)) up = Vector3.Up;
            if (backward.Equals (Vector3.Zero)) backward = Vector3.Backward;
            Vector3.Normalise (ref right, out right);
            Vector3.Normalise (ref up, out up);
            Vector3.Normalise (ref backward, out backward);
            Matrix44 rotMat;
            Matrix44.CreateFromCartesianAxes (ref right, ref up, ref backward, out rotMat);
            Quaternion.CreateFromRotationMatrix (ref rotMat, out rotation);
            result = true;
        }

        public static void Determinant (ref Matrix44 m, out Fixed32 result) {
            result = + m.R0C3 * m.R1C2 * m.R2C1 * m.R3C0 - m.R0C2 * m.R1C3 * m.R2C1 * m.R3C0
                     - m.R0C3 * m.R1C1 * m.R2C2 * m.R3C0 + m.R0C1 * m.R1C3 * m.R2C2 * m.R3C0
                     + m.R0C2 * m.R1C1 * m.R2C3 * m.R3C0 - m.R0C1 * m.R1C2 * m.R2C3 * m.R3C0
                     - m.R0C3 * m.R1C2 * m.R2C0 * m.R3C1 + m.R0C2 * m.R1C3 * m.R2C0 * m.R3C1
                     + m.R0C3 * m.R1C0 * m.R2C2 * m.R3C1 - m.R0C0 * m.R1C3 * m.R2C2 * m.R3C1
                     - m.R0C2 * m.R1C0 * m.R2C3 * m.R3C1 + m.R0C0 * m.R1C2 * m.R2C3 * m.R3C1
                     + m.R0C3 * m.R1C1 * m.R2C0 * m.R3C2 - m.R0C1 * m.R1C3 * m.R2C0 * m.R3C2
                     - m.R0C3 * m.R1C0 * m.R2C1 * m.R3C2 + m.R0C0 * m.R1C3 * m.R2C1 * m.R3C2
                     + m.R0C1 * m.R1C0 * m.R2C3 * m.R3C2 - m.R0C0 * m.R1C1 * m.R2C3 * m.R3C2
                     - m.R0C2 * m.R1C1 * m.R2C0 * m.R3C3 + m.R0C1 * m.R1C2 * m.R2C0 * m.R3C3
                     + m.R0C2 * m.R1C0 * m.R2C1 * m.R3C3 - m.R0C0 * m.R1C2 * m.R2C1 * m.R3C3
                     - m.R0C1 * m.R1C0 * m.R2C2 * m.R3C3 + m.R0C0 * m.R1C1 * m.R2C2 * m.R3C3;
        }

        public static void Invert (ref Matrix44 m, out Matrix44 result) {
            Fixed32 d; Determinant (ref m, out d); Fixed32 s = 1 / d;
            Fixed32 r0c0 = m.R1C2 * m.R2C3 * m.R3C1 - m.R1C3 * m.R2C2 * m.R3C1 + m.R1C3 * m.R2C1 * m.R3C2 - m.R1C1 * m.R2C3 * m.R3C2 - m.R1C2 * m.R2C1 * m.R3C3 + m.R1C1 * m.R2C2 * m.R3C3;
            Fixed32 r0c1 = m.R0C3 * m.R2C2 * m.R3C1 - m.R0C2 * m.R2C3 * m.R3C1 - m.R0C3 * m.R2C1 * m.R3C2 + m.R0C1 * m.R2C3 * m.R3C2 + m.R0C2 * m.R2C1 * m.R3C3 - m.R0C1 * m.R2C2 * m.R3C3;
            Fixed32 r0c2 = m.R0C2 * m.R1C3 * m.R3C1 - m.R0C3 * m.R1C2 * m.R3C1 + m.R0C3 * m.R1C1 * m.R3C2 - m.R0C1 * m.R1C3 * m.R3C2 - m.R0C2 * m.R1C1 * m.R3C3 + m.R0C1 * m.R1C2 * m.R3C3;
            Fixed32 r0c3 = m.R0C3 * m.R1C2 * m.R2C1 - m.R0C2 * m.R1C3 * m.R2C1 - m.R0C3 * m.R1C1 * m.R2C2 + m.R0C1 * m.R1C3 * m.R2C2 + m.R0C2 * m.R1C1 * m.R2C3 - m.R0C1 * m.R1C2 * m.R2C3;
            Fixed32 r1c0 = m.R1C3 * m.R2C2 * m.R3C0 - m.R1C2 * m.R2C3 * m.R3C0 - m.R1C3 * m.R2C0 * m.R3C2 + m.R1C0 * m.R2C3 * m.R3C2 + m.R1C2 * m.R2C0 * m.R3C3 - m.R1C0 * m.R2C2 * m.R3C3;
            Fixed32 r1c1 = m.R0C2 * m.R2C3 * m.R3C0 - m.R0C3 * m.R2C2 * m.R3C0 + m.R0C3 * m.R2C0 * m.R3C2 - m.R0C0 * m.R2C3 * m.R3C2 - m.R0C2 * m.R2C0 * m.R3C3 + m.R0C0 * m.R2C2 * m.R3C3;
            Fixed32 r1c2 = m.R0C3 * m.R1C2 * m.R3C0 - m.R0C2 * m.R1C3 * m.R3C0 - m.R0C3 * m.R1C0 * m.R3C2 + m.R0C0 * m.R1C3 * m.R3C2 + m.R0C2 * m.R1C0 * m.R3C3 - m.R0C0 * m.R1C2 * m.R3C3;
            Fixed32 r1c3 = m.R0C2 * m.R1C3 * m.R2C0 - m.R0C3 * m.R1C2 * m.R2C0 + m.R0C3 * m.R1C0 * m.R2C2 - m.R0C0 * m.R1C3 * m.R2C2 - m.R0C2 * m.R1C0 * m.R2C3 + m.R0C0 * m.R1C2 * m.R2C3;
            Fixed32 r2c0 = m.R1C1 * m.R2C3 * m.R3C0 - m.R1C3 * m.R2C1 * m.R3C0 + m.R1C3 * m.R2C0 * m.R3C1 - m.R1C0 * m.R2C3 * m.R3C1 - m.R1C1 * m.R2C0 * m.R3C3 + m.R1C0 * m.R2C1 * m.R3C3;
            Fixed32 r2c1 = m.R0C3 * m.R2C1 * m.R3C0 - m.R0C1 * m.R2C3 * m.R3C0 - m.R0C3 * m.R2C0 * m.R3C1 + m.R0C0 * m.R2C3 * m.R3C1 + m.R0C1 * m.R2C0 * m.R3C3 - m.R0C0 * m.R2C1 * m.R3C3;
            Fixed32 r2c2 = m.R0C1 * m.R1C3 * m.R3C0 - m.R0C3 * m.R1C1 * m.R3C0 + m.R0C3 * m.R1C0 * m.R3C1 - m.R0C0 * m.R1C3 * m.R3C1 - m.R0C1 * m.R1C0 * m.R3C3 + m.R0C0 * m.R1C1 * m.R3C3;
            Fixed32 r2c3 = m.R0C3 * m.R1C1 * m.R2C0 - m.R0C1 * m.R1C3 * m.R2C0 - m.R0C3 * m.R1C0 * m.R2C1 + m.R0C0 * m.R1C3 * m.R2C1 + m.R0C1 * m.R1C0 * m.R2C3 - m.R0C0 * m.R1C1 * m.R2C3;
            Fixed32 r3c0 = m.R1C2 * m.R2C1 * m.R3C0 - m.R1C1 * m.R2C2 * m.R3C0 - m.R1C2 * m.R2C0 * m.R3C1 + m.R1C0 * m.R2C2 * m.R3C1 + m.R1C1 * m.R2C0 * m.R3C2 - m.R1C0 * m.R2C1 * m.R3C2;
            Fixed32 r3c1 = m.R0C1 * m.R2C2 * m.R3C0 - m.R0C2 * m.R2C1 * m.R3C0 + m.R0C2 * m.R2C0 * m.R3C1 - m.R0C0 * m.R2C2 * m.R3C1 - m.R0C1 * m.R2C0 * m.R3C2 + m.R0C0 * m.R2C1 * m.R3C2;
            Fixed32 r3c2 = m.R0C2 * m.R1C1 * m.R3C0 - m.R0C1 * m.R1C2 * m.R3C0 - m.R0C2 * m.R1C0 * m.R3C1 + m.R0C0 * m.R1C2 * m.R3C1 + m.R0C1 * m.R1C0 * m.R3C2 - m.R0C0 * m.R1C1 * m.R3C2;
            Fixed32 r3c3 = m.R0C1 * m.R1C2 * m.R2C0 - m.R0C2 * m.R1C1 * m.R2C0 + m.R0C2 * m.R1C0 * m.R2C1 - m.R0C0 * m.R1C2 * m.R2C1 - m.R0C1 * m.R1C0 * m.R2C2 + m.R0C0 * m.R1C1 * m.R2C2;
            result.R0C0 = r0c0; result.R0C1 = r0c1; result.R0C2 = r0c2; result.R0C3 = r0c3;
            result.R1C0 = r1c0; result.R1C1 = r1c1; result.R1C2 = r1c2; result.R1C3 = r1c3;
            result.R2C0 = r2c0; result.R2C1 = r2c1; result.R2C2 = r2c2; result.R2C3 = r2c3;
            result.R3C0 = r3c0; result.R3C1 = r3c1; result.R3C2 = r3c2; result.R3C3 = r3c3; 
            Multiply (ref result, ref s, out result);
        }

        public static void Transform (ref Matrix44 m, ref Quaternion q, out Matrix44 result) {
            Boolean qIsUnit; Quaternion.IsUnit (ref q, out qIsUnit);
            Debug.Assert (qIsUnit);
            Fixed32 twoI = q.I + q.I, twoJ = q.J + q.J, twoK = q.K + q.K;
            Fixed32 twoUI = q.U * twoI, twoUJ = q.U * twoJ, twoUK = q.U * twoK;
            Fixed32 twoII = q.I * twoI, twoIJ = q.I * twoJ, twoIK = q.I * twoK;
            Fixed32 twoJJ = q.J * twoJ, twoJK = q.J * twoK, twoKK = q.K * twoK;
            Fixed32 tR0C0 = 1 - twoJJ - twoKK;
            Fixed32 tR1C0 = twoIJ - twoUK;
            Fixed32 tR2C0 = twoIK + twoUJ;
            Fixed32 tR0C1 = twoIJ + twoUK;
            Fixed32 tR1C1 = 1 - twoII - twoKK;
            Fixed32 tR2C1 = twoJK - twoUI;
            Fixed32 tR0C2 = twoIK - twoUJ;
            Fixed32 tR1C2 = twoJK + twoUI;
            Fixed32 tR2C2 = 1 - twoII - twoJJ;
            Fixed32 r0c0 = m.R0C0 * tR0C0 + m.R0C1 * tR1C0 + m.R0C2 * tR2C0;
            Fixed32 r0c1 = m.R0C0 * tR0C1 + m.R0C1 * tR1C1 + m.R0C2 * tR2C1;
            Fixed32 r0c2 = m.R0C0 * tR0C2 + m.R0C1 * tR1C2 + m.R0C2 * tR2C2;
            Fixed32 r1c0 = m.R1C0 * tR0C0 + m.R1C1 * tR1C0 + m.R1C2 * tR2C0;
            Fixed32 r1c1 = m.R1C0 * tR0C1 + m.R1C1 * tR1C1 + m.R1C2 * tR2C1;
            Fixed32 r1c2 = m.R1C0 * tR0C2 + m.R1C1 * tR1C2 + m.R1C2 * tR2C2;
            Fixed32 r2c0 = m.R2C0 * tR0C0 + m.R2C1 * tR1C0 + m.R2C2 * tR2C0;
            Fixed32 r2c1 = m.R2C0 * tR0C1 + m.R2C1 * tR1C1 + m.R2C2 * tR2C1;
            Fixed32 r2c2 = m.R2C0 * tR0C2 + m.R2C1 * tR1C2 + m.R2C2 * tR2C2;
            Fixed32 r3c0 = m.R3C0 * tR0C0 + m.R3C1 * tR1C0 + m.R3C2 * tR2C0;
            Fixed32 r3c1 = m.R3C0 * tR0C1 + m.R3C1 * tR1C1 + m.R3C2 * tR2C1;
            Fixed32 r3c2 = m.R3C0 * tR0C2 + m.R3C1 * tR1C2 + m.R3C2 * tR2C2;
            result.R0C0 = r0c0; result.R0C1 = r0c1; result.R0C2 = r0c2; result.R0C3 = m.R0C3;
            result.R1C0 = r1c0; result.R1C1 = r1c1; result.R1C2 = r1c2; result.R1C3 = m.R1C3;
            result.R2C0 = r2c0; result.R2C1 = r2c1; result.R2C2 = r2c2; result.R2C3 = m.R2C3;
            result.R3C0 = r3c0; result.R3C1 = r3c1; result.R3C2 = r3c2; result.R3C3 = m.R3C3; 
        }

        public static void Transform (ref Matrix44 m, ref Vector3 v, out Vector3 result) {
            Fixed32 x = (v.X * m.R0C0) + (v.Y * m.R1C0) + (v.Z * m.R2C0) + m.R3C0;
            Fixed32 y = (v.X * m.R0C1) + (v.Y * m.R1C1) + (v.Z * m.R2C1) + m.R3C1;
            Fixed32 z = (v.X * m.R0C2) + (v.Y * m.R1C2) + (v.Z * m.R2C2) + m.R3C2;
            Fixed32 w = (v.X * m.R0C3) + (v.Y * m.R1C3) + (v.Z * m.R2C3) + m.R3C3;
            result.X = x / w; result.Y = y / w; result.Z = z / w;
        }

        public static void Transform (ref Matrix44 m, ref Vector4 v, out Vector4 result) {
            Fixed32 x = (v.X * m.R0C0) + (v.Y * m.R1C0) + (v.Z * m.R2C0) + (v.W * m.R3C0);
            Fixed32 y = (v.X * m.R0C1) + (v.Y * m.R1C1) + (v.Z * m.R2C1) + (v.W * m.R3C1);
            Fixed32 z = (v.X * m.R0C2) + (v.Y * m.R1C2) + (v.Z * m.R2C2) + (v.W * m.R3C2);
            Fixed32 w = (v.X * m.R0C3) + (v.Y * m.R1C3) + (v.Z * m.R2C3) + (v.W * m.R3C3);
            result.X = x; result.Y = y; result.Z = z; result.W = w;
        }

#if (FUNCTION_VARIANTS)
        public Fixed32   Determinant ()                    { Fixed32 result; Determinant (ref this, out result); return result; }
        public Matrix44 Transpose   ()                    { Transpose (ref this, out this); return this; }
        public Matrix44 Invert      ()                    { Invert (ref this, out this); return this; }
        public Matrix44 Transform   (Quaternion rotation) { Matrix44 result; Transform (ref this, ref rotation, out result); return result; }
        public Vector3  Transform   (Vector3 v)           { Vector3 result; Transform (ref this, ref v, out result); return result; } 
        public Vector4  Transform   (Vector4 v)           { Vector4 result; Transform (ref this, ref v, out result); return result; } 

        public static Fixed32   Determinant (Matrix44 matrix)                      { Fixed32 result; Determinant (ref matrix, out result); return result; }
        public static Matrix44 Transpose   (Matrix44 input)                       { Matrix44 result; Transpose (ref input, out result); return result; }
        public static Matrix44 Invert      (Matrix44 matrix)                      { Matrix44 result; Invert (ref matrix, out result); return result; }
        public static Matrix44 Transform   (Matrix44 matrix, Quaternion rotation) { Matrix44 result; Transform (ref matrix, ref rotation, out result); return result; }
        public static Vector3  Transform   (Matrix44 matrix, Vector3 v)           { Vector3 result; Transform (ref matrix, ref v, out result); return result; } 
        public static Vector4  Transform   (Matrix44 matrix, Vector4 v)           { Vector4 result; Transform (ref matrix, ref v, out result); return result; } 
#endif

        // Creation //--------------------------------------------------------//

        public static void CreateTranslation (ref Vector3 position, out Matrix44 result) {
            result.R0C0 = 1;          result.R0C1 = 0;          result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = 1;          result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = 0;          result.R2C1 = 0;          result.R2C2 = 1;          result.R2C3 = 0;
            result.R3C0 = position.X; result.R3C1 = position.Y; result.R3C2 = position.Z; result.R3C3 = 1;
        }

        public static void CreateTranslation (ref Fixed32 x, ref Fixed32 y, ref Fixed32 z, out Matrix44 result) {
            result.R0C0 = 1;          result.R0C1 = 0;          result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = 1;          result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = 0;          result.R2C1 = 0;          result.R2C2 = 1;          result.R2C3 = 0;
            result.R3C0 = x;          result.R3C1 = y;          result.R3C2 = z;          result.R3C3 = 1;
        }

        public static void CreateScale (ref Vector3 scale, out Matrix44 result) {
            result.R0C0 = scale.X;    result.R0C1 = 0;          result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = scale.Y;    result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = 0;          result.R2C1 = 0;          result.R2C2 = scale.Z;    result.R2C3 = 0;
            result.R3C0 = 0;          result.R3C1 = 0;          result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateScale (ref Fixed32 x, ref Fixed32 y, ref Fixed32 z, out Matrix44 result) {
            result.R0C0 = x;          result.R0C1 = 0;          result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = y;          result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = 0;          result.R2C1 = 0;          result.R2C2 = z;          result.R2C3 = 0;
            result.R3C0 = 0;          result.R3C1 = 0;          result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateScale (ref Fixed32 scale, out Matrix44 result) {
            result.R0C0 = scale;      result.R0C1 = 0;          result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = scale;      result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = 0;          result.R2C1 = 0;          result.R2C2 = scale;      result.R2C3 = 0;
            result.R3C0 = 0;          result.R3C1 = 0;          result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateRotationX (ref Fixed32 radians, out Matrix44 result) {
            Fixed32 cos = Maths.Cos (radians), sin = Maths.Sin (radians);
            result.R0C0 = 1;          result.R0C1 = 0;          result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = cos;        result.R1C2 = sin;        result.R1C3 = 0;
            result.R2C0 = 0;          result.R2C1 = -sin;       result.R2C2 = cos;        result.R2C3 = 0;
            result.R3C0 = 0;          result.R3C1 = 0;          result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateRotationY (ref Fixed32 radians, out Matrix44 result) {
            Fixed32 cos = Maths.Cos (radians), sin = Maths.Sin (radians);
            result.R0C0 = cos;        result.R0C1 = 0;          result.R0C2 = -sin;       result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = 1;          result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = sin;        result.R2C1 = 0;          result.R2C2 = cos;        result.R2C3 = 0;
            result.R3C0 = 0;          result.R3C1 = 0;          result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateRotationZ (ref Fixed32 radians, out Matrix44 result) {
            Fixed32 cos = Maths.Cos (radians), sin = Maths.Sin (radians);
            result.R0C0 = cos;       result.R0C1 = sin;         result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = -sin;      result.R1C1 = cos;         result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = 0;         result.R2C1 = 0;           result.R2C2 = 1;          result.R2C3 = 0;
            result.R3C0 = 0;         result.R3C1 = 0;           result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateFromAxisAngle (ref Vector3 axis, ref Fixed32 angle, out Matrix44 result) {
            Fixed32 x = axis.X, y = axis.Y, z = axis.Z;
            Fixed32 sin = Maths.Sin (angle), cos = Maths.Cos (angle);
            Fixed32 xx = x * x, yy = y * y, zz = z * z;
            Fixed32 xy = x * y, xz = x * z, yz = y * z;
            result.R0C0 = xx + (cos * (1 - xx));       result.R0C1 = xy - (cos * xy) + (sin * z); result.R0C2 = xz - (cos * xz) - (sin * y); result.R0C3 = 0;
            result.R1C0 = xy - (cos * xy) - (sin * z); result.R1C1 = yy + (cos * (1 - yy));       result.R1C2 = yz - (cos * yz) + (sin * x); result.R1C3 = 0;
            result.R2C0 = xz - (cos * xz) + (sin * y); result.R2C1 = yz - (cos * yz) - (sin * x); result.R2C2 = zz + (cos * (1 - zz));       result.R2C3 = 0;
            result.R3C0 = 0;                           result.R3C1 = 0;                           result.R3C2 = 0;                           result.R3C3 = 1;
        }

        // Axes must be pair-wise perpendicular and have unit length.
        public static void CreateFromCartesianAxes (ref Vector3 right, ref Vector3 up, ref Vector3 backward, out Matrix44 result) {
            result.R0C0 = right.X;    result.R0C1 = right.Y;    result.R0C2 = right.Z;    result.R0C3 = 0;
            result.R1C0 = up.X;       result.R1C1 = up.Y;       result.R1C2 = up.Z;       result.R1C3 = 0;
            result.R2C0 = backward.X; result.R2C1 = backward.Y; result.R2C2 = backward.Z; result.R2C3 = 0;
            result.R3C0 = 0;          result.R3C1 = 0;          result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateWorld (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result) {
            Vector3 backward; Vector3.Negate (ref forward, out backward); Vector3.Normalise (ref backward, out backward);
            Vector3 right; Vector3.Cross (ref up, ref backward, out right); Vector3.Normalise (ref right, out right);
            Vector3 finalUp; Vector3.Cross (ref right, ref backward, out finalUp); Vector3.Normalise (ref finalUp, out finalUp);
            result.R0C0 = right.X;    result.R0C1 = right.Y;    result.R0C2 = right.Z;    result.R0C3 = 0;
            result.R1C0 = finalUp.X;  result.R1C1 = finalUp.Y;  result.R1C2 = finalUp.Z;  result.R1C3 = 0;
            result.R2C0 = backward.X; result.R2C1 = backward.Y; result.R2C2 = backward.Z; result.R2C3 = 0;
            result.R3C0 = position.X; result.R3C1 = position.Y; result.R3C2 = position.Z; result.R3C3 = 1;
        }

        // http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToMatrix/
        public static void CreateFromQuaternion (ref Quaternion q, out Matrix44 result) {
            Boolean qIsUnit; Quaternion.IsUnit (ref q, out qIsUnit); Debug.Assert (qIsUnit);
            Fixed32 twoI = q.I + q.I, twoJ = q.J + q.J, twoK = q.K + q.K;
            Fixed32 twoUI = q.U * twoI, twoUJ = q.U * twoJ, twoUK = q.U * twoK;
            Fixed32 twoII = q.I * twoI, twoIJ = q.I * twoJ, twoIK = q.I * twoK;
            Fixed32 twoJJ = q.J * twoJ, twoJK = q.J * twoK, twoKK = q.K * twoK;
            result.R0C0 = 1 - twoJJ - twoKK; result.R1C0 = twoIJ - twoUK;     result.R2C0 = twoIK + twoUJ;     result.R3C0 = 0;
            result.R0C1 = twoIJ + twoUK;     result.R1C1 = 1 - twoII - twoKK; result.R2C1 = twoJK - twoUI;     result.R3C1 = 0;
            result.R0C2 = twoIK - twoUJ;     result.R1C2 = twoJK + twoUI;     result.R2C2 = 1 - twoII - twoJJ; result.R3C2 = 0;
            result.R0C3 = 0;                 result.R1C3 = 0;                 result.R2C3 = 0;                 result.R3C3 = 1;
        }

        // Angle of rotation, in radians. Angles are measured anti-clockwise when viewed from the rotation axis (positive side) toward the origin.
        public static void CreateFromYawPitchRoll (ref Fixed32 yaw, ref Fixed32 pitch, ref Fixed32 roll, out Matrix44 result) {
            Fixed32 cy = Maths.Cos (yaw), sy = Maths.Sin (yaw);
            Fixed32 cx = Maths.Cos (pitch), sx = Maths.Sin (pitch);
            Fixed32 cz = Maths.Cos (roll), sz = Maths.Sin (roll);
            result.R0C0 =  cz*cy+sz*sx*sy; result.R0C1 =  sz*cx; result.R0C2 = -cz*sy+sz*sx*cy; result.R0C3 = 0;
            result.R1C0 = -sz*cy+cz*sx*sy; result.R1C1 =  cz*cx; result.R1C2 = -cz*sy+sz*sx*cy; result.R1C3 = 0;
            result.R2C0 =  cx*sy;          result.R2C1 = -sx;    result.R2C2 =  cx*cy;          result.R2C3 = 0;
            result.R3C0 = 0;               result.R3C1 = 0;      result.R3C2 = 0;               result.R3C3 = 1;
        }

        // http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
        public static void CreatePerspectiveFieldOfView (ref Fixed32 fieldOfView, ref Fixed32 aspectRatio, ref Fixed32 nearPlaneDistance, ref Fixed32 farPlaneDistance, out Matrix44 result) {
            Debug.Assert (fieldOfView > 0 && fieldOfView < Maths.Pi);
            Debug.Assert (nearPlaneDistance > 0);
            Debug.Assert (farPlaneDistance > 0);
            Debug.Assert (nearPlaneDistance < farPlaneDistance);
            Fixed32 yScale = (Fixed32) 1 / (Maths.Tan (fieldOfView * Maths.Half));
            Fixed32 xScale = yScale / aspectRatio;
            Fixed32 f1 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            Fixed32 f2 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            result.R0C0 = xScale; result.R0C1 = 0;      result.R0C2 = 0;  result.R0C3 =  0;
            result.R1C0 = 0;      result.R1C1 = yScale; result.R1C2 = 0;  result.R1C3 =  0;
            result.R2C0 = 0;      result.R2C1 = 0;      result.R2C2 = f1; result.R2C3 = -1;
            result.R3C0 = 0;      result.R3C1 = 0;      result.R3C2 = f2; result.R3C3 =  0;
        }

        // http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
        public static void CreatePerspective (ref Fixed32 width, ref Fixed32 height, ref Fixed32 nearPlaneDistance, ref Fixed32 farPlaneDistance, out Matrix44 result) {
            Debug.Assert (nearPlaneDistance > 0);
            Debug.Assert (farPlaneDistance > 0);
            Debug.Assert (nearPlaneDistance < farPlaneDistance);
            result.R0C0 = (nearPlaneDistance * 2) / width;
            result.R0C1 = result.R0C2 = result.R0C3 = 0;
            result.R1C1 = (nearPlaneDistance * 2) / height;
            result.R1C0 = result.R1C2 = result.R1C3 = 0;
            result.R2C2 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.R2C0 = result.R2C1 = 0;
            result.R2C3 = -1;
            result.R3C0 = result.R3C1 = result.R3C3 = 0;
            result.R3C2 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
        }

        // http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx
        public static void CreatePerspectiveOffCenter (ref Fixed32 left, ref Fixed32 right, ref Fixed32 bottom, ref Fixed32 top, ref Fixed32 nearPlaneDistance, ref Fixed32 farPlaneDistance, out Matrix44 result) {
            Debug.Assert (nearPlaneDistance > 0);
            Debug.Assert (farPlaneDistance > 0);
            Debug.Assert (nearPlaneDistance < farPlaneDistance);
            result.R0C0 = (nearPlaneDistance * 2) / (right - left);
            result.R0C1 = result.R0C2 = result.R0C3 = 0;
            result.R1C1 = (nearPlaneDistance * 2) / (top - bottom);
            result.R1C0 = result.R1C2 = result.R1C3 = 0;
            result.R2C0 = (left + right) / (right - left);
            result.R2C1 = (top + bottom) / (top - bottom);
            result.R2C2 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.R2C3 = -1;
            result.R3C2 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            result.R3C0 = result.R3C1 = result.R3C3 = 0;
        }

        // http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
        public static void CreateOrthographic (ref Fixed32 width, ref Fixed32 height, ref Fixed32 zNearPlane, ref Fixed32 zFarPlane, out Matrix44 result) {
            result.R0C0 = 2 / width;
            result.R0C1 = result.R0C2 = result.R0C3 = 0;
            result.R1C1 = 2 / height;
            result.R1C0 = result.R1C2 = result.R1C3 = 0;
            result.R2C2 = 1 / (zNearPlane - zFarPlane);
            result.R2C0 = result.R2C1 = result.R2C3 = 0;
            result.R3C0 = result.R3C1 = 0;
            result.R3C2 = zNearPlane / (zNearPlane - zFarPlane);
            result.R3C3 = 1;
        }

        // http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx
        public static void CreateOrthographicOffCenter (ref Fixed32 left, ref Fixed32 right, ref Fixed32 bottom, ref Fixed32 top, ref Fixed32 zNearPlane, ref Fixed32 zFarPlane, out Matrix44 result) {
            result.R0C0 = 2 / (right - left);
            result.R0C1 = result.R0C2 = result.R0C3 = 0;
            result.R1C1 = 2 / (top - bottom);
            result.R1C0 = result.R1C2 = result.R1C3 = 0;
            result.R2C2 = 1 / (zNearPlane - zFarPlane);
            result.R2C0 = result.R2C1 = result.R2C3 = 0;
            result.R3C0 = (left + right) / (left - right);
            result.R3C1 = (top + bottom) / (bottom - top);
            result.R3C2 = zNearPlane / (zNearPlane - zFarPlane);
            result.R3C3 = 1;
        }

        // http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx
        public static void CreateLookAt (ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix44 result) {
            Vector3 forward; Vector3.Subtract (ref cameraPosition, ref cameraTarget, out forward); Vector3.Normalise (ref forward, out forward);
            Vector3 right; Vector3.Cross (ref cameraUpVector, ref forward, out right); Vector3.Normalise (ref right, out right);
            Vector3 up; Vector3.Cross (ref forward, ref right, out up); Vector3.Normalise (ref up, out up);
            Fixed32 a; Vector3.Dot (ref right, ref cameraPosition, out a);
            Fixed32 b; Vector3.Dot (ref up, ref cameraPosition, out b);
            Fixed32 c; Vector3.Dot (ref forward, ref cameraPosition, out c);
            result.R0C0 = right.X;    result.R0C1 = up.X;       result.R0C2 = forward.X;  result.R0C3 = 0;
            result.R1C0 = right.Y;    result.R1C1 = up.Y;       result.R1C2 = forward.Y;  result.R1C3 = 0;
            result.R2C0 = right.Z;    result.R2C1 = up.Z;       result.R2C2 = forward.Z;  result.R2C3 = 0;
            result.R3C0 = -a;         result.R3C1 = -b;         result.R3C2 = -c;         result.R3C3 = 1;
        }

#if (FUNCTION_VARIANTS)
        public static Matrix44 CreateTranslation            (Fixed32 xPosition, Fixed32 yPosition, Fixed32 zPosition) { Matrix44 result; CreateTranslation (ref xPosition, ref yPosition, ref zPosition, out result); return result; }
        public static Matrix44 CreateTranslation            (Vector3 position) { Matrix44 result; CreateTranslation (ref position, out result); return result; }
        public static Matrix44 CreateScale                  (Fixed32 xScale, Fixed32 yScale, Fixed32 zScale) { Matrix44 result; CreateScale (ref xScale, ref yScale, ref zScale, out result); return result; }
        public static Matrix44 CreateScale                  (Vector3 scales) { Matrix44 result; CreateScale (ref scales, out result); return result; }
        public static Matrix44 CreateScale                  (Fixed32 scale) { Matrix44 result; CreateScale (ref scale, out result); return result; }
        public static Matrix44 CreateRotationX              (Fixed32 radians) { Matrix44 result; CreateRotationX (ref radians, out result); return result; }
        public static Matrix44 CreateRotationY              (Fixed32 radians) { Matrix44 result; CreateRotationY (ref radians, out result); return result; }
        public static Matrix44 CreateRotationZ              (Fixed32 radians) { Matrix44 result; CreateRotationZ (ref radians, out result); return result; }
        public static Matrix44 CreateFromAxisAngle          (Vector3 axis, Fixed32 angle) { Matrix44 result; CreateFromAxisAngle (ref axis, ref angle, out result); return result; }
        public static Matrix44 CreateFromCartesianAxes      (Vector3 right, Vector3 up, Vector3 backward) { Matrix44 result; CreateFromCartesianAxes (ref right, ref up, ref backward, out result); return result; }
        public static Matrix44 CreateWorld                  (Vector3 position, Vector3 forward, Vector3 up) { Matrix44 result; CreateWorld (ref position, ref forward, ref up, out result); return result; }
        public static Matrix44 CreateFromQuaternion         (Quaternion quaternion) { Matrix44 result; CreateFromQuaternion (ref quaternion, out result); return result; }
        public static Matrix44 CreateFromYawPitchRoll       (Fixed32 yaw, Fixed32 pitch, Fixed32 roll) { Matrix44 result; CreateFromYawPitchRoll (ref yaw, ref pitch, ref roll, out result); return result; }
        public static Matrix44 CreatePerspectiveFieldOfView (Fixed32 fieldOfView,  Fixed32 aspectRatio, Fixed32 nearPlane, Fixed32 farPlane) { Matrix44 result; CreatePerspectiveFieldOfView (ref fieldOfView, ref aspectRatio, ref nearPlane, ref farPlane, out result); return result; }
        public static Matrix44 CreatePerspective            (Fixed32 width, Fixed32 height, Fixed32 nearPlane, Fixed32 farPlane) { Matrix44 result; CreatePerspective (ref width, ref height, ref nearPlane, ref farPlane, out result); return result; }
        public static Matrix44 CreatePerspectiveOffCenter   (Fixed32 left, Fixed32 right, Fixed32 bottom, Fixed32 top, Fixed32 nearPlane, Fixed32 farPlane) { Matrix44 result; CreatePerspectiveOffCenter (ref left, ref right, ref bottom, ref top, ref nearPlane, ref farPlane, out result); return result; }
        public static Matrix44 CreateOrthographic           (Fixed32 width, Fixed32 height, Fixed32 nearPlane, Fixed32 farPlane) { Matrix44 result; CreateOrthographic (ref width, ref height, ref nearPlane, ref farPlane, out result); return result; }
        public static Matrix44 CreateOrthographicOffCenter  (Fixed32 left, Fixed32 right, Fixed32 bottom, Fixed32 top, Fixed32 nearPlane, Fixed32 farPlane) { Matrix44 result; CreateOrthographicOffCenter (ref left, ref right, ref bottom, ref top, ref nearPlane, ref farPlane, out result); return result; }
        public static Matrix44 CreateLookAt                 (Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector) { Matrix44 result; CreateLookAt (ref cameraPosition, ref cameraTarget, ref cameraUpVector, out result); return result; }
#endif

    }

    /// <summary>
    /// Fixed32 precision Vector2.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector2 : IEquatable<Vector2> {
        public Fixed32 X, Y;

        public Vector2 (Fixed32 x, Fixed32 y) { X = x; Y = y; }

        public override String ToString () { return String.Format ("(X:{0}, Y:{1})", X, Y); }

        public override Int32 GetHashCode () { return X.GetHashCode () ^ Y.GetHashCode ().ShiftAndWrap (2); }

        public override Boolean Equals (Object obj) { return (obj is Vector2) ? this.Equals ((Vector2) obj) : false; }

        public Boolean Equals (Vector2 other) { Boolean result; Equals (ref this, ref other, out result); return result; }

        // Constants //-------------------------------------------------------//

        readonly static Vector2 zero, one;
        readonly static Vector2 unitX, unitY;

        static Vector2 () {
            zero =      new Vector2 ();
            one =       new Vector2 (1, 1);
            unitX =     new Vector2 (1, 0);
            unitY =     new Vector2 (0, 1);
        }

        public static Vector2 Zero  { get { return zero; } }
        public static Vector2 One   { get { return one; } }
        public static Vector2 UnitX { get { return unitX; } }
        public static Vector2 UnitY { get { return unitY; } }

        // Operators //-------------------------------------------------------//

        public static void Equals (ref Vector2 a, ref Vector2 b, out Boolean result) {
            result = (a.X == b.X) && (a.Y == b.Y);
        }

        public static void Add (ref Vector2 a, ref Vector2 b, out Vector2 result) {
            result.X = a.X + b.X; result.Y = a.Y + b.Y;
        }

        public static void Subtract (ref Vector2 a, ref Vector2 b, out Vector2 result) {
            result.X = a.X - b.X; result.Y = a.Y - b.Y;
        }

        public static void Negate (ref Vector2 v, out Vector2 result) {
            result.X = -v.X; result.Y = -v.Y;
        }

        public static void Multiply (ref Vector2 a, ref Vector2 b, out Vector2 result) {
            result.X = a.X * b.X; result.Y = a.Y * b.Y;
        }

        public static void Multiply (ref Vector2 v, ref Fixed32 f, out Vector2 result) {
            result.X = v.X * f; result.Y = v.Y * f;
        }

        public static void Divide (ref Vector2 a, ref Vector2 b, out Vector2 result) {
            result.X = a.X / b.X; result.Y = a.Y / b.Y;
        }

        public static void Divide (ref Vector2 v, ref Fixed32 d, out Vector2 result) {
            Fixed32 num = 1 / d;
            result.X = v.X * num; result.Y = v.Y * num;
        }

#if (FUNCTION_VARIANTS)
        public static Boolean operator == (Vector2 a, Vector2 b) { Boolean result; Equals   (ref a, ref b, out result); return  result; }
        public static Boolean operator != (Vector2 a, Vector2 b) { Boolean result; Equals   (ref a, ref b, out result); return !result; }
        public static Vector2 operator  + (Vector2 a, Vector2 b) { Vector2 result; Add      (ref a, ref b, out result); return  result; }
        public static Vector2 operator  - (Vector2 a, Vector2 b) { Vector2 result; Subtract (ref a, ref b, out result); return  result; }
        public static Vector2 operator  - (Vector2 v)            { Vector2 result; Negate   (ref v,        out result); return  result; }
        public static Vector2 operator  * (Vector2 a, Vector2 b) { Vector2 result; Multiply (ref a, ref b, out result); return  result; }
        public static Vector2 operator  * (Vector2 v, Fixed32 f)  { Vector2 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector2 operator  * (Fixed32 f,  Vector2 v) { Vector2 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector2 operator  / (Vector2 a, Vector2 b) { Vector2 result; Divide   (ref a, ref b, out result); return  result; }
        public static Vector2 operator  / (Vector2 a, Fixed32 d)  { Vector2 result; Divide   (ref a, ref d, out result); return  result; }
        public static Fixed32  operator  | (Vector2 a, Vector2 d) { Fixed32  result; Dot      (ref a, ref d, out result); return  result; }
        public static Vector2 operator  ~ (Vector2 v)            { Vector2 result; Normalise(ref v,        out result); return  result; }

        public static Boolean Equals      (Vector2 a, Vector2 b) { Boolean result; Equals   (ref a, ref b, out result); return  result; }
        public static Vector2 Add         (Vector2 a, Vector2 b) { Vector2 result; Add      (ref a, ref b, out result); return  result; }
        public static Vector2 Subtract    (Vector2 a, Vector2 b) { Vector2 result; Subtract (ref a, ref b, out result); return  result; }
        public static Vector2 Negate      (Vector2 v)            { Vector2 result; Negate   (ref v,        out result); return  result; }
        public static Vector2 Multiply    (Vector2 a, Vector2 b) { Vector2 result; Multiply (ref a, ref b, out result); return  result; }
        public static Vector2 Multiply    (Vector2 v, Fixed32 f)  { Vector2 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector2 Divide      (Vector2 a, Vector2 b) { Vector2 result; Divide   (ref a, ref b, out result); return  result; }
        public static Vector2 Divide      (Vector2 a, Fixed32 d)  { Vector2 result; Divide   (ref a, ref d, out result); return  result; }
#endif

        // Utilities //-------------------------------------------------------//

        public static void Min (ref Vector2 a, ref Vector2 b, out Vector2 result) {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
        }

        public static void Max (ref Vector2 a, ref Vector2 b, out Vector2 result) {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
        }

        public static void Clamp (ref Vector2 v, ref Vector2 min, ref Vector2 max, out Vector2 result) {
            Fixed32 x = v.X; x = (x > max.X) ? max.X : x; x = (x < min.X) ? min.X : x; result.X = x;
            Fixed32 y = v.Y; y = (y > max.Y) ? max.Y : y; y = (y < min.Y) ? min.Y : y; result.Y = y;
        }

        public static void Lerp (ref Vector2 a, ref Vector2 b, Fixed32 amount, out Vector2 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
        }

        public static void IsUnit (ref Vector2 vector, out Boolean result) {
            result = Maths.IsZero(1 - vector.X * vector.X - vector.Y * vector.Y);
        }

#if (FUNCTION_VARIANTS)
        public Boolean IsUnit        () { Boolean result; IsUnit (ref this, out result); return result; }
        public Vector2 Clamp         (Vector2 min, Vector2 max) { Clamp (ref this, ref min, ref max, out this); return this; }

        public static Vector2 Min    (Vector2 a, Vector2 b) { Vector2 result; Min (ref a, ref b, out result); return result; }
        public static Vector2 Max    (Vector2 a, Vector2 b) { Vector2 result; Max (ref a, ref b, out result); return result; }
        public static Vector2 Clamp  (Vector2 v, Vector2 min, Vector2 max) { Vector2 result; Clamp (ref v, ref min, ref max, out result); return result; }
        public static Vector2 Lerp   (Vector2 a, Vector2 b, Fixed32 amount) { Vector2 result; Lerp (ref a, ref b, amount, out result); return result; }
        public static Boolean IsUnit (Vector2 v) { Boolean result; IsUnit (ref v, out result); return result; }

#endif
        
        // Splines //---------------------------------------------------------//

        public static void SmoothStep (ref Vector2 v1, ref Vector2 v2, Fixed32 amount, out Vector2 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            amount = (amount * amount) * (3 - (2 * amount));
            result.X = v1.X + ((v2.X - v1.X) * amount);
            result.Y = v1.Y + ((v2.Y - v1.Y) * amount);
        }

        public static void CatmullRom (ref Vector2 v1, ref Vector2 v2, ref Vector2 v3, ref Vector2 v4, Fixed32 amount, out Vector2 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;
            result.X  = 2 * v2.X;
            result.X += (v3.X - v1.X) * amount;
            result.X += ((2 * v1.X) + (4 * v3.X) - (5 * v2.X) - (v4.X)) * squared;
            result.X += ((3 * v2.X) + (v4.X) - (v1.X)  - (3 * v3.X)) * cubed;
            result.X *= Maths.Half;
            result.Y  = 2 * v2.Y;
            result.Y += (v3.Y - v1.Y) * amount;
            result.Y += ((2 * v1.Y) + (4 * v3.Y) - (5 * v2.Y) - (v4.Y)) * squared;
            result.Y += ((3 * v2.Y) + (v4.Y) - (v1.Y) - (3 * v3.Y)) * cubed;
            result.Y *= Maths.Half;
        }

        public static void Hermite (ref Vector2 v1, ref Vector2 tangent1, ref Vector2 v2, ref Vector2 tangent2, Fixed32 amount, out Vector2 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector2.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector2.IsUnit (ref tangent2, out tangent2IsUnit);
            Debug.Assert (tangent1IsUnit && tangent2IsUnit);
            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;
            Fixed32 a = ((cubed * 2) - (squared * 3)) + 1;
            Fixed32 b = (-cubed * 2) + (squared * 3);
            Fixed32 c = (cubed - (squared * 2)) + amount;
            Fixed32 d = cubed - squared;
            result.X = (v1.X * a) + (v2.X * b) + (tangent1.X * c) + (tangent2.X * d);
            result.Y = (v1.Y * a) + (v2.Y * b) + (tangent1.Y * c) + (tangent2.Y * d);
        }

#if (FUNCTION_VARIANTS)
        public static Vector2 SmoothStep (Vector2 v1, Vector2 v2, Fixed32 amount) { Vector2 result; SmoothStep (ref v1, ref v2, amount, out result); return result; }
        public static Vector2 CatmullRom (Vector2 v1, Vector2 v2, Vector2 v3, Vector2 v4, Fixed32 amount) { Vector2 result; CatmullRom (ref v1, ref v2, ref v3, ref v4, amount, out result); return result; }
        public static Vector2 Hermite    (Vector2 v1, Vector2 tangent1, Vector2 v2, Vector2 tangent2, Fixed32 amount) { Vector2 result; Hermite (ref v1, ref tangent1, ref v2, ref tangent2, amount, out result); return result; }
#endif

        // Maths //-----------------------------------------------------------//

        public static void Distance (ref Vector2 a, ref Vector2 b, out Fixed32 result) {
            Fixed32 dx = a.X - b.X, dy = a.Y - b.Y;
            Fixed32 lengthSquared = (dx * dx) + (dy * dy);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void DistanceSquared (ref Vector2 a, ref Vector2 b, out Fixed32 result) {
            Fixed32 dx = a.X - b.X, dy = a.Y - b.Y;
            result = (dx * dx) + (dy * dy);
        }

        public static void Dot (ref Vector2 a, ref Vector2 b, out Fixed32 result) {
            result = (a.X * b.X) + (a.Y * b.Y);
        }

        public static void Normalise (ref Vector2 vector, out Vector2 result) {
            Fixed32 lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y);
            Debug.Assert (lengthSquared > Maths.Epsilon && !Fixed32.IsInfinity(lengthSquared));
            Fixed32 multiplier = 1 / Maths.Sqrt (lengthSquared);
            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
        }

        public static void Reflect (ref Vector2 vector, ref Vector2 normal, out Vector2 result) {
            Boolean normalIsUnit; Vector2.IsUnit (ref normal, out normalIsUnit);
            Debug.Assert (normalIsUnit);
            Fixed32 dot; Dot(ref vector, ref normal, out dot);
            Fixed32 twoDot = dot * 2;
            Vector2 m;
            Vector2.Multiply (ref normal, ref twoDot, out m);
            Vector2.Subtract (ref vector, ref m, out result);
        }

        public static void Length (ref Vector2 vector, out Fixed32 result) {
            Fixed32 lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void LengthSquared (ref Vector2 vector, out Fixed32 result) {
            result = (vector.X * vector.X) + (vector.Y * vector.Y);
        }

#if (FUNCTION_VARIANTS)
        public Fixed32  Length        () { Fixed32 result; Length (ref this, out result); return result; }
        public Fixed32  LengthSquared () { Fixed32 result; LengthSquared (ref this, out result); return result; }
        public Vector2 Normalise     () { Normalise (ref this, out this); return this; }

        public static Fixed32  Distance        (Vector2 a, Vector2 b) { Fixed32 result; Distance (ref a, ref b, out result); return result; }
        public static Fixed32  DistanceSquared (Vector2 a, Vector2 b) { Fixed32 result; DistanceSquared (ref a, ref b, out result); return result; }
        public static Fixed32  Dot             (Vector2 a, Vector2 b) { Fixed32 result; Dot (ref a, ref b, out result); return result; }
        public static Vector2 Normalise       (Vector2 v) { Vector2 result; Normalise (ref v, out result); return result; }
        public static Vector2 Reflect         (Vector2 v, Vector2 normal) { Vector2 result; Reflect (ref v, ref normal, out result); return result; }
        public static Fixed32  Length          (Vector2 v) { Fixed32 result; Length (ref v, out result); return result; }
        public static Fixed32  LengthSquared   (Vector2 v) { Fixed32 result; LengthSquared (ref v, out result); return result; }
#endif


    }

    /// <summary>
    /// Fixed32 precision Vector3.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector3 : IEquatable<Vector3> {
        public Fixed32 X, Y, Z;

        public Vector3 (Fixed32 x, Fixed32 y, Fixed32 z) { X = x; Y = y; Z = z; }

        public Vector3 (Vector2 value, Fixed32 z) { X = value.X; Y = value.Y; Z = z; }

        public override String ToString () { return string.Format ("(X:{0}, Y:{1}, Z:{2})", X, Y, Z); }

        public override Int32 GetHashCode () {
            return X.GetHashCode () ^ Y.GetHashCode ().ShiftAndWrap (2) ^ Z.GetHashCode ().ShiftAndWrap (4);
        }

        public override Boolean Equals (Object obj) { return (obj is Vector3) ? this.Equals ((Vector3) obj) : false; }

        public Boolean Equals (Vector3 other) {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        // Constants //-------------------------------------------------------//

        static Vector3 zero, one;
        static Vector3 unitX, unitY, unitZ;
        static Vector3 up, down, right, left, forward, backward;

        static Vector3 () {
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
        
        public static Vector3 Zero     { get { return zero; } }
        public static Vector3 One      { get { return one; } }
        public static Vector3 UnitX    { get { return unitX; } }
        public static Vector3 UnitY    { get { return unitY; } }
        public static Vector3 UnitZ    { get { return unitZ; } }
        public static Vector3 Up       { get { return up; } }
        public static Vector3 Down     { get { return down; } }
        public static Vector3 Right    { get { return right; } }
        public static Vector3 Left     { get { return left; } }
        public static Vector3 Forward  { get { return forward; } }
        public static Vector3 Backward { get { return backward; } }

        // Operators //-------------------------------------------------------//

        public static void Equals (ref Vector3 a, ref Vector3 b, out Boolean result) {
            result = (a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z);
        }

        public static void Add (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            result.X = a.X + b.X; result.Y = a.Y + b.Y; result.Z = a.Z + b.Z;
        }

        public static void Subtract (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            result.X = a.X - b.X; result.Y = a.Y - b.Y; result.Z = a.Z - b.Z;
        }

        public static void Negate (ref Vector3 value, out Vector3 result) {
            result.X = -value.X; result.Y = -value.Y; result.Z = -value.Z;
        }

        public static void Multiply (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            result.X = a.X * b.X; result.Y = a.Y * b.Y; result.Z = a.Z * b.Z;
        }

        public static void Multiply (ref Vector3 a, ref Fixed32 f, out Vector3 result) {
            result.X = a.X * f; result.Y = a.Y * f; result.Z = a.Z * f;
        }

        public static void Divide (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            result.X = a.X / b.X; result.Y = a.Y / b.Y; result.Z = a.Z / b.Z;
        }

        public static void Divide (ref Vector3 a, ref Fixed32 d, out Vector3 result) {
            Fixed32 num = 1 / d;
            result.X = a.X * num; result.Y = a.Y * num; result.Z = a.Z * num;
        }

#if (FUNCTION_VARIANTS)
        public static Boolean operator == (Vector3 a, Vector3 b) { Boolean result; Equals   (ref a, ref b, out result); return  result; }
        public static Boolean operator != (Vector3 a, Vector3 b) { Boolean result; Equals   (ref a, ref b, out result); return !result; }
        public static Vector3 operator  + (Vector3 a, Vector3 b) { Vector3 result; Add      (ref a, ref b, out result); return  result; }
        public static Vector3 operator  - (Vector3 a, Vector3 b) { Vector3 result; Subtract (ref a, ref b, out result); return  result; }
        public static Vector3 operator  - (Vector3 v)            { Vector3 result; Negate   (ref v,        out result); return  result; }
        public static Vector3 operator  * (Vector3 a, Vector3 b) { Vector3 result; Multiply (ref a, ref b, out result); return  result; }
        public static Vector3 operator  * (Vector3 v, Fixed32 f)  { Vector3 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector3 operator  * (Fixed32 f,  Vector3 v) { Vector3 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector3 operator  / (Vector3 a, Vector3 b) { Vector3 result; Divide   (ref a, ref b, out result); return  result; }
        public static Vector3 operator  / (Vector3 a, Fixed32 d)  { Vector3 result; Divide   (ref a, ref d, out result); return  result; }
        public static Vector3 operator  ^ (Vector3 a, Vector3 d) { Vector3 result; Cross    (ref a, ref d, out result); return  result; }
        public static Fixed32  operator  | (Vector3 a, Vector3 d) { Fixed32  result; Dot      (ref a, ref d, out result); return  result; }
        public static Vector3 operator  ~ (Vector3 v)            { Vector3 result; Normalise(ref v,        out result); return  result; }

        public static Boolean Equals      (Vector3 a, Vector3 b) { Boolean result; Equals   (ref a, ref b, out result); return  result; }
        public static Vector3 Add         (Vector3 a, Vector3 b) { Vector3 result; Add      (ref a, ref b, out result); return  result; }
        public static Vector3 Subtract    (Vector3 a, Vector3 b) { Vector3 result; Subtract (ref a, ref b, out result); return  result; }
        public static Vector3 Negate      (Vector3 v)            { Vector3 result; Negate   (ref v,        out result); return  result; }
        public static Vector3 Multiply    (Vector3 a, Vector3 b) { Vector3 result; Multiply (ref a, ref b, out result); return  result; }
        public static Vector3 Multiply    (Vector3 v, Fixed32 f)  { Vector3 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector3 Divide      (Vector3 a, Vector3 b) { Vector3 result; Divide   (ref a, ref b, out result); return  result; }
        public static Vector3 Divide      (Vector3 a, Fixed32 d)  { Vector3 result; Divide   (ref a, ref d, out result); return  result; }
#endif

        // Utilities //-------------------------------------------------------//

        public static void Min (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            result.X = (a.X < b.X) ? a.X : b.X; result.Y = (a.Y < b.Y) ? a.Y : b.Y;
            result.Z = (a.Z < b.Z) ? a.Z : b.Z;
        }

        public static void Max (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            result.X = (a.X > b.X) ? a.X : b.X; result.Y = (a.Y > b.Y) ? a.Y : b.Y;
            result.Z = (a.Z > b.Z) ? a.Z : b.Z;
        }

        public static void Clamp (ref Vector3 v, ref Vector3 min, ref Vector3 max, out Vector3 result) {
            Fixed32 x = v.X; x = (x > max.X) ? max.X : x; x = (x < min.X) ? min.X : x; result.X = x;
            Fixed32 y = v.Y; y = (y > max.Y) ? max.Y : y; y = (y < min.Y) ? min.Y : y; result.Y = y;
            Fixed32 z = v.Z; z = (z > max.Z) ? max.Z : z; z = (z < min.Z) ? min.Z : z; result.Z = z;
        }

        public static void Lerp (ref Vector3 a, ref Vector3 b, ref Fixed32 amount, out Vector3 result){
            Debug.Assert (amount >= 0 && amount <= 1);
            result.X = a.X + ((b.X - a.X) * amount); result.Y = a.Y + ((b.Y - a.Y) * amount);
            result.Z = a.Z + ((b.Z - a.Z) * amount);
        }

        public static void IsUnit (ref Vector3 vector, out Boolean result) {
            result = Maths.IsZero (1 - vector.X * vector.X - vector.Y * vector.Y - vector.Z * vector.Z);
        }

#if (FUNCTION_VARIANTS)
        public Boolean IsUnit        () { Boolean result; IsUnit (ref this, out result); return result; }
        public Vector3 Clamp         (Vector3 min, Vector3 max) { Clamp (ref this, ref min, ref max, out this); return this; }

        public static Vector3 Min    (Vector3 a, Vector3 b) { Vector3 result; Min (ref a, ref b, out result); return result; }
        public static Vector3 Max    (Vector3 a, Vector3 b) { Vector3 result; Max (ref a, ref b, out result); return result; }
        public static Vector3 Clamp  (Vector3 v, Vector3 min, Vector3 max) { Vector3 result; Clamp (ref v, ref min, ref max, out result); return result; }
        public static Vector3 Lerp   (Vector3 a, Vector3 b, ref Fixed32 amount) { Vector3 result; Lerp (ref a, ref b, ref amount, out result); return result; }
        public static Boolean IsUnit (Vector3 v) { Boolean result; IsUnit (ref v, out result); return result; }

#endif

        // Splines //---------------------------------------------------------//

        public static void SmoothStep (ref Vector3 v1, ref Vector3 v2, ref Fixed32 amount, out Vector3 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            amount = (amount * amount) * (3 - (2 * amount));
            result.X = v1.X + ((v2.X - v1.X) * amount);
            result.Y = v1.Y + ((v2.Y - v1.Y) * amount);
            result.Z = v1.Z + ((v2.Z - v1.Z) * amount);
        }

        public static void CatmullRom (ref Vector3 v1, ref Vector3 v2, ref Vector3 v3, ref Vector3 v4, ref Fixed32 amount, out Vector3 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;
            result.X  = 2 * v2.X;
            result.X += (v3.X - v1.X) * amount;
            result.X += ((2 * v1.X) + (4 * v3.X) - (5 * v2.X) - (v4.X)) * squared;
            result.X += ((3 * v2.X) + (v4.X) - (v1.X)  - (3 * v3.X)) * cubed;
            result.X *= Maths.Half;
            result.Y  = 2 * v2.Y;
            result.Y += (v3.Y - v1.Y) * amount;
            result.Y += ((2 * v1.Y) + (4 * v3.Y) - (5 * v2.Y) - (v4.Y)) * squared;
            result.Y += ((3 * v2.Y) + (v4.Y) - (v1.Y) - (3 * v3.Y)) * cubed;
            result.Y *= Maths.Half;
            result.Z  = 2 * v2.Z;
            result.Z += (v3.Z - v1.Z) * amount;
            result.Z += ((2 * v1.Z) + (4 * v3.Z) - (5 * v2.Z) - (v4.Z)) * squared;
            result.Z += ((3 * v2.Z) + (v4.Z) - (v1.Z) - (3 * v3.Z)) * cubed;
            result.Z *= Maths.Half;
        }

        public static void Hermite (ref Vector3 v1, ref Vector3 tangent1, ref Vector3 v2, ref Vector3 tangent2, ref Fixed32 amount, out Vector3 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector3.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector3.IsUnit (ref tangent2, out tangent2IsUnit);
            Debug.Assert (tangent1IsUnit && tangent2IsUnit);
            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;
            Fixed32 a = ((cubed * 2) - (squared * 3)) + 1;
            Fixed32 b = (-cubed * 2) + (squared * 3);
            Fixed32 c = (cubed - (squared * 2)) + amount;
            Fixed32 d = cubed - squared;
            result.X = (v1.X * a) + (v2.X * b) + (tangent1.X * c) + (tangent2.X * d);
            result.Y = (v1.Y * a) + (v2.Y * b) + (tangent1.Y * c) + (tangent2.Y * d);
            result.Z = (v1.Z * a) + (v2.Z * b) + (tangent1.Z * c) + (tangent2.Z * d);
        }

#if (FUNCTION_VARIANTS)
        public static Vector3 SmoothStep (Vector3 v1, Vector3 v2, ref Fixed32 amount) { Vector3 result; SmoothStep (ref v1, ref v2, ref amount, out result); return result; }
        public static Vector3 CatmullRom (Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, ref Fixed32 amount) { Vector3 result; CatmullRom (ref v1, ref v2, ref v3, ref v4, ref amount, out result); return result; }
        public static Vector3 Hermite    (Vector3 v1, Vector3 tangent1, Vector3 v2, Vector3 tangent2, ref Fixed32 amount) { Vector3 result; Hermite (ref v1, ref tangent1, ref v2, ref tangent2, ref amount, out result); return result; }
#endif

        // Maths //-----------------------------------------------------------//

        public static void Distance (ref Vector3 a, ref Vector3 b, out Fixed32 result) {
            Fixed32 dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z;
            Fixed32 lengthSquared = (dx * dx) + (dy * dy) + (dz * dz);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void DistanceSquared (ref Vector3 a, ref Vector3 b, out Fixed32 result) {
            Fixed32 dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z;
            result = (dx * dx) + (dy * dy) + (dz * dz);
        }

        public static void Dot (ref Vector3 a, ref Vector3 b, out Fixed32 result) {
            result = (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        public static void Normalise (ref Vector3 vector, out Vector3 result) {
            Fixed32 lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z);
            Debug.Assert (lengthSquared > Maths.Epsilon && !Fixed32.IsInfinity(lengthSquared));
            Fixed32 multiplier = 1 / Maths.Sqrt (lengthSquared);
            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
        }

        public static void Cross (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            Fixed32 x = (a.Y * b.Z) - (a.Z * b.Y);
            Fixed32 y = (a.Z * b.X) - (a.X * b.Z);
            Fixed32 z = (a.X * b.Y) - (a.Y * b.X);
            result.X = x; result.Y = y; result.Z = z;
        }

        public static void Reflect (ref Vector3 vector, ref Vector3 normal, out Vector3 result) {
            Boolean normalIsUnit; Vector3.IsUnit (ref normal, out normalIsUnit);
            Debug.Assert (normalIsUnit);
            Fixed32 t = (vector.X * normal.X) + (vector.Y * normal.Y) + (vector.Z * normal.Z);
            Fixed32 x = vector.X - ((2 * t) * normal.X);
            Fixed32 y = vector.Y - ((2 * t) * normal.Y);
            Fixed32 z = vector.Z - ((2 * t) * normal.Z);
            result.X = x; result.Y = y; result.Z = z;
        }

        public static void Length (ref Vector3 vector, out Fixed32 result) {
            Fixed32 lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void LengthSquared (ref Vector3 vector, out Fixed32 result) {
            result = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z);
        }

#if (FUNCTION_VARIANTS)
        public Fixed32  Length        () { Fixed32 result; Length (ref this, out result); return result; }
        public Fixed32  LengthSquared () { Fixed32 result; LengthSquared (ref this, out result); return result; }
        public Vector3 Normalise     () { Normalise (ref this, out this); return this; }

        public static Fixed32  Distance        (Vector3 a, Vector3 b) { Fixed32 result; Distance (ref a, ref b, out result); return result; } 
        public static Fixed32  DistanceSquared (Vector3 a, Vector3 b) { Fixed32 result; DistanceSquared (ref a, ref b, out result); return result; } 
        public static Fixed32  Dot             (Vector3 a, Vector3 b) { Fixed32 result; Dot (ref a, ref b, out result); return result; } 
        public static Vector3 Cross           (Vector3 a, Vector3 b) { Vector3 result; Cross (ref a, ref b, out result); return result; } 
        public static Vector3 Normalise       (Vector3 v) { Vector3 result; Normalise (ref v, out result); return result; }
         
        public static Vector3 Reflect         (Vector3 v, Vector3 normal) { Vector3 result; Reflect (ref v, ref normal, out result); return result; } 
        public static Fixed32  Length          (Vector3 v) { Fixed32 result; Length (ref v, out result); return result; } 
        public static Fixed32  LengthSquared   (Vector3 v) { Fixed32 result; LengthSquared (ref v, out result); return result; }
#endif

    }

    /// <summary>
    /// Fixed32 precision Vector4.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector4 : IEquatable<Vector4> {
        public Fixed32 X, Y, Z, W;

        public Vector4 (Fixed32 x, Fixed32 y, Fixed32 z, Fixed32 w) { X = x; Y = y; Z = z; W = w; }

        public Vector4 (Vector2 value, Fixed32 z, Fixed32 w) { X = value.X; Y = value.Y; Z = z; W = w; }

        public Vector4 (Vector3 value, Fixed32 w) { X = value.X; Y = value.Y; Z = value.Z; W = w; }

        public override String ToString () { return string.Format ("(X:{0}, Y:{1}, Z:{2}, W:{3})", X, Y, Z, W); }

        public override Int32 GetHashCode () {
            return W.GetHashCode ().ShiftAndWrap (6) ^ Z.GetHashCode ().ShiftAndWrap (4)
                 ^ Y.GetHashCode ().ShiftAndWrap (2) ^ X.GetHashCode ();
        }

        public override Boolean Equals (Object obj) { return (obj is Vector4) ? this.Equals ((Vector4)obj) : false; }

        public Boolean Equals (Vector4 other) {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        // Constants //-------------------------------------------------------//

        static Vector4 zero, one;
        static Vector4 unitX, unitY, unitZ, unitW;

        static Vector4 () {
            zero =      new Vector4 ();
            one =       new Vector4 (1, 1, 1, 1);
            unitX =     new Vector4 (1, 0, 0, 0);
            unitY =     new Vector4 (0, 1, 0, 0);
            unitZ =     new Vector4 (0, 0, 1, 0);
            unitW =     new Vector4 (0, 0, 0, 1);
        }

        public static Vector4 Zero  { get { return zero; } }
        public static Vector4 One   { get { return one; } }
        public static Vector4 UnitX { get { return unitX; } }
        public static Vector4 UnitY { get { return unitY; } }
        public static Vector4 UnitZ { get { return unitZ; } }
        public static Vector4 UnitW { get { return unitW; } }

        // Operators //-------------------------------------------------------//

        public static void Equals (ref Vector4 a, ref Vector4 b, out Boolean result) {
            result = (a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z) && (a.W == b.W);
        }

        public static void Add (ref Vector4 a, ref Vector4 b, out Vector4 result) {
            result.X = a.X + b.X; result.Y = a.Y + b.Y; result.Z = a.Z + b.Z; result.W = a.W + b.W;
        }

        public static void Subtract (ref Vector4 a, ref Vector4 b, out Vector4 result) {
            result.X = a.X - b.X; result.Y = a.Y - b.Y; result.Z = a.Z - b.Z; result.W = a.W - b.W;
        }

        public static void Negate (ref Vector4 v, out Vector4 result) {
            result.X = -v.X; result.Y = -v.Y; result.Z = -v.Z; result.W = -v.W;
        }

        public static void Multiply (ref Vector4 a, ref Vector4 b, out Vector4 result) {
            result.X = a.X * b.X; result.Y = a.Y * b.Y; result.Z = a.Z * b.Z; result.W = a.W * b.W;
        }

        public static void Multiply (ref Vector4 v, ref Fixed32 f, out Vector4 result) {
            result.X = v.X * f; result.Y = v.Y * f; result.Z = v.Z * f; result.W = v.W * f;
        }

        public static void Divide (ref Vector4 a, ref Vector4 b, out Vector4 result) {
            result.X = a.X / b.X; result.Y = a.Y / b.Y; result.Z = a.Z / b.Z; result.W = a.W / b.W;
        }

        public static void Divide (ref Vector4 v, ref Fixed32 d, out Vector4 result) {
            Fixed32 num = 1 / d;
            result.X = v.X * num; result.Y = v.Y * num; result.Z = v.Z * num; result.W = v.W * num;
        }

#if (FUNCTION_VARIANTS)
        public static Boolean operator == (Vector4 a, Vector4 b) { Boolean result; Equals   (ref a, ref b, out result); return  result; }
        public static Boolean operator != (Vector4 a, Vector4 b) { Boolean result; Equals   (ref a, ref b, out result); return !result; }
        public static Vector4 operator  + (Vector4 a, Vector4 b) { Vector4 result; Add      (ref a, ref b, out result); return  result; }
        public static Vector4 operator  - (Vector4 a, Vector4 b) { Vector4 result; Subtract (ref a, ref b, out result); return  result; }
        public static Vector4 operator  - (Vector4 v)            { Vector4 result; Negate   (ref v,        out result); return  result; }
        public static Vector4 operator  * (Vector4 a, Vector4 b) { Vector4 result; Multiply (ref a, ref b, out result); return  result; }
        public static Vector4 operator  * (Vector4 v, Fixed32 f)  { Vector4 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector4 operator  * (Fixed32 f,  Vector4 v) { Vector4 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector4 operator  / (Vector4 a, Vector4 b) { Vector4 result; Divide   (ref a, ref b, out result); return  result; }
        public static Vector4 operator  / (Vector4 a, Fixed32 d)  { Vector4 result; Divide   (ref a, ref d, out result); return  result; }
        public static Fixed32  operator  | (Vector4 a, Vector4 d) { Fixed32  result; Dot      (ref a, ref d, out result); return  result; }
        public static Vector4 operator  ~ (Vector4 v)            { Vector4 result; Normalise(ref v,        out result); return  result; }

        public static Boolean Equals      (Vector4 a, Vector4 b) { Boolean result; Equals   (ref a, ref b, out result); return  result; }
        public static Vector4 Add         (Vector4 a, Vector4 b) { Vector4 result; Add      (ref a, ref b, out result); return  result; }
        public static Vector4 Subtract    (Vector4 a, Vector4 b) { Vector4 result; Subtract (ref a, ref b, out result); return  result; }
        public static Vector4 Negate      (Vector4 v)            { Vector4 result; Negate   (ref v,        out result); return  result; }
        public static Vector4 Multiply    (Vector4 a, Vector4 b) { Vector4 result; Multiply (ref a, ref b, out result); return  result; }
        public static Vector4 Multiply    (Vector4 v, Fixed32 f)  { Vector4 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector4 Divide      (Vector4 a, Vector4 b) { Vector4 result; Divide   (ref a, ref b, out result); return  result; }
        public static Vector4 Divide      (Vector4 a, Fixed32 d)  { Vector4 result; Divide   (ref a, ref d, out result); return  result; }
#endif

        // Utilities //-------------------------------------------------------//

        public static void Min (ref Vector4 a, ref Vector4 b, out Vector4 result) {
            result.X = (a.X < b.X) ? a.X : b.X; result.Y = (a.Y < b.Y) ? a.Y : b.Y;
            result.Z = (a.Z < b.Z) ? a.Z : b.Z; result.W = (a.W < b.W) ? a.W : b.W;
        }

        public static void Max (ref Vector4 a, ref Vector4 b, out Vector4 result) {
            result.X = (a.X > b.X) ? a.X : b.X; result.Y = (a.Y > b.Y) ? a.Y : b.Y;
            result.Z = (a.Z > b.Z) ? a.Z : b.Z; result.W = (a.W > b.W) ? a.W : b.W;
        }

        public static void Clamp (ref Vector4 v, ref Vector4 min, ref Vector4 max, out Vector4 result) {
            Fixed32 x = v.X; x = (x > max.X) ? max.X : x; x = (x < min.X) ? min.X : x; result.X = x;
            Fixed32 y = v.Y; y = (y > max.Y) ? max.Y : y; y = (y < min.Y) ? min.Y : y; result.Y = y;
            Fixed32 z = v.Z; z = (z > max.Z) ? max.Z : z; z = (z < min.Z) ? min.Z : z; result.Z = z;
            Fixed32 w = v.W; w = (w > max.W) ? max.W : w; w = (w < min.W) ? min.W : w; result.W = w;
        }

        public static void Lerp (ref Vector4 a, ref Vector4 b, ref Fixed32 amount, out Vector4 result){
            Debug.Assert (amount >= 0 && amount <= 1);
            result.X = a.X + ((b.X - a.X) * amount); result.Y = a.Y + ((b.Y - a.Y) * amount);
            result.Z = a.Z + ((b.Z - a.Z) * amount); result.W = a.W + ((b.W - a.W) * amount);
        }

        public static void IsUnit (ref Vector4 vector, out Boolean result) {
            result = Maths.IsZero (1 - vector.X * vector.X - vector.Y * vector.Y - vector.Z * vector.Z - vector.W * vector.W);
        }

#if (FUNCTION_VARIANTS)
        public Boolean IsUnit        () { Boolean result; IsUnit (ref this, out result); return result; }
        public Vector4 Clamp         (Vector4 min, Vector4 max) { Clamp (ref this, ref min, ref max, out this); return this; }

        public static Vector4 Min    (Vector4 a, Vector4 b) { Vector4 result; Min (ref a, ref b, out result); return result; }
        public static Vector4 Max    (Vector4 a, Vector4 b) { Vector4 result; Max (ref a, ref b, out result); return result; }
        public static Vector4 Clamp  (Vector4 v, Vector4 min, Vector4 max) { Vector4 result; Clamp (ref v, ref min, ref max, out result); return result; }
        public static Vector4 Lerp   (Vector4 a, Vector4 b, Fixed32 amount) { Vector4 result; Lerp (ref a, ref b, ref amount, out result); return result; }
        public static Boolean IsUnit (Vector4 v) { Boolean result; IsUnit (ref v, out result); return result; }

#endif

        // Splines //---------------------------------------------------------//

        public static void SmoothStep (ref Vector4 v1, ref Vector4 v2, ref Fixed32 amount, out Vector4 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            amount = (amount * amount) * (3 - (2 * amount));
            result.X = v1.X + ((v2.X - v1.X) * amount);
            result.Y = v1.Y + ((v2.Y - v1.Y) * amount);
            result.Z = v1.Z + ((v2.Z - v1.Z) * amount);
            result.W = v1.W + ((v2.W - v1.W) * amount);
        }

        public static void CatmullRom (ref Vector4 v1, ref Vector4 v2, ref Vector4 v3, ref Vector4 v4, ref Fixed32 amount, out Vector4 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;
            result.X  = 2 * v2.X;
            result.X += (v3.X - v1.X) * amount;
            result.X += ((2 * v1.X) + (4 * v3.X) - (5 * v2.X) - (v4.X)) * squared;
            result.X += ((3 * v2.X) + (v4.X) - (v1.X)  - (3 * v3.X)) * cubed;
            result.X *= Maths.Half;
            result.Y  = 2 * v2.Y;
            result.Y += (v3.Y - v1.Y) * amount;
            result.Y += ((2 * v1.Y) + (4 * v3.Y) - (5 * v2.Y) - (v4.Y)) * squared;
            result.Y += ((3 * v2.Y) + (v4.Y) - (v1.Y) - (3 * v3.Y)) * cubed;
            result.Y *= Maths.Half;
            result.Z  = 2 * v2.Z;
            result.Z += (v3.Z - v1.Z) * amount;
            result.Z += ((2 * v1.Z) + (4 * v3.Z) - (5 * v2.Z) - (v4.Z)) * squared;
            result.Z += ((3 * v2.Z) + (v4.Z) - (v1.Z) - (3 * v3.Z)) * cubed;
            result.Z *= Maths.Half;
            result.W  = 2 * v2.W;
            result.W += (v3.W - v1.W) * amount;
            result.W += ((2 * v1.W) + (4 * v3.W) - (5 * v2.W) - (v4.W)) * squared;
            result.W += ((3 * v2.W) + (v4.W) - (v1.W) - (3 * v3.W)) * cubed;
            result.W *= Maths.Half;
        }

        public static void Hermite (ref Vector4 v1, ref Vector4 tangent1, ref Vector4 v2, ref Vector4 tangent2, ref Fixed32 amount, out Vector4 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector4.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector4.IsUnit (ref tangent2, out tangent2IsUnit);
            Debug.Assert (tangent1IsUnit && tangent2IsUnit);
            Fixed32 squared = amount * amount;
            Fixed32 cubed = amount * squared;
            Fixed32 a = ((cubed * 2) - (squared * 3)) + 1;
            Fixed32 b = (-cubed * 2) + (squared * 3);
            Fixed32 c = (cubed - (squared * 2)) + amount;
            Fixed32 d = cubed - squared;
            result.X = (v1.X * a) + (v2.X * b) + (tangent1.X * c) + (tangent2.X * d);
            result.Y = (v1.Y * a) + (v2.Y * b) + (tangent1.Y * c) + (tangent2.Y * d);
            result.Z = (v1.Z * a) + (v2.Z * b) + (tangent1.Z * c) + (tangent2.Z * d);
            result.W = (v1.W * a) + (v2.W * b) + (tangent1.W * c) + (tangent2.W * d);
        }

#if (FUNCTION_VARIANTS)
        public static Vector4 SmoothStep (Vector4 v1, Vector4 v2, ref Fixed32 amount) { Vector4 result; SmoothStep (ref v1, ref v2, ref amount, out result); return result; }
        public static Vector4 CatmullRom (Vector4 v1, Vector4 v2, Vector4 v3, Vector4 v4, ref Fixed32 amount) { Vector4 result; CatmullRom (ref v1, ref v2, ref v3, ref v4, ref amount, out result); return result; }
        public static Vector4 Hermite    (Vector4 v1, Vector4 tangent1, Vector4 v2, Vector4 tangent2, ref Fixed32 amount) { Vector4 result; Hermite (ref v1, ref tangent1, ref v2, ref tangent2, ref amount, out result); return result; }
#endif

        // Maths //-----------------------------------------------------------//

        public static void Distance (ref Vector4 a, ref Vector4 b, out Fixed32 result) {
            Fixed32 dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z, dw = a.W - b.W;
            Fixed32 lengthSquared = (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void DistanceSquared (ref Vector4 a, ref Vector4 b, out Fixed32 result) {
            Fixed32 dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z, dw = a.W - b.W;
            result = (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
        }

        public static void Dot (ref Vector4 a, ref Vector4 b, out Fixed32 result) {
            result = (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z) + (a.W * b.W);
        }

        public static void Normalise (ref Vector4 vector, out Vector4 result) {
            Fixed32 lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z) + (vector.W * vector.W);
            Debug.Assert (lengthSquared > Maths.Epsilon && !Fixed32.IsInfinity(lengthSquared));
            Fixed32 multiplier = 1 / (Maths.Sqrt (lengthSquared));
            result.X = vector.X * multiplier; result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier; result.W = vector.W * multiplier;
        }

        public static void Length (ref Vector4 vector, out Fixed32 result) {
            Fixed32 lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z) + (vector.W * vector.W);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void LengthSquared (ref Vector4 vector, out Fixed32 result) {
            result = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z) + (vector.W * vector.W);
        }


#if (FUNCTION_VARIANTS)
        public Fixed32  Length        () { Fixed32 result; Length (ref this, out result); return result; }
        public Fixed32  LengthSquared () { Fixed32 result; LengthSquared (ref this, out result); return result; }
        public Vector4 Normalise     () { Normalise (ref this, out this); return this; }

        public static Fixed32  Distance        ( Vector4 a, Vector4 b) { Fixed32 result; Distance (ref a, ref b, out result); return result; } 
        public static Fixed32  DistanceSquared (Vector4 a, Vector4 b) { Fixed32 result; DistanceSquared (ref a, ref b, out result); return result; } 
        public static Fixed32  Dot             (Vector4 a, Vector4 b) { Fixed32 result; Dot (ref a, ref b, out result); return result; } 
        public static Vector4 Normalise       (Vector4 v) { Vector4 result; Normalise (ref v, out result); return result; }
        public static Fixed32  Length          (Vector4 v) { Fixed32 result; Length (ref v, out result); return result; } 
        public static Fixed32  LengthSquared   (Vector4 v) { Fixed32 result; LengthSquared (ref v, out result); return result; }
#endif
    }

    /// <summary>
    /// Provides maths functions with consistent function signatures across supported precisions.
    /// </summary>
    public static class Maths {
        public static readonly Fixed32 Epsilon = (Fixed32) 0.000001;
        public static readonly Fixed32 E = Fixed32.CreateFrom (2.71828182845904523536028747135);
        public static readonly Fixed32 Half = Fixed32.CreateFrom (0.5);
        public static readonly Fixed32 Quarter = Fixed32.CreateFrom (0.25);
        public static readonly Fixed32 Log10E = Fixed32.CreateFrom (0.43429448190325182765112891892);
        public static readonly Fixed32 Log2E = Fixed32.CreateFrom (1.44269504088896340735992468100);
        public static readonly Fixed32 Pi = Fixed32.CreateFrom (3.14159265358979323846264338328);
        public static readonly Fixed32 HalfPi = Fixed32.CreateFrom (1.57079632679489661923132169164);
        public static readonly Fixed32 QuarterPi = Fixed32.CreateFrom (0.78539816339744830961566084582);
        public static readonly Fixed32 Root2 = Fixed32.CreateFrom (1.41421356237309504880168872421);
        public static readonly Fixed32 Root3 = Fixed32.CreateFrom (1.73205080756887729352744634151);
        public static readonly Fixed32 Tau = Fixed32.CreateFrom (6.28318530717958647692528676656);
        public static readonly Fixed32 Deg2Rad = Fixed32.CreateFrom (0.01745329251994329576923690768);
        public static readonly Fixed32 Rad2Deg = Fixed32.CreateFrom (57.29577951308232087679815481409);
        public static readonly Fixed32 Zero = Fixed32.CreateFrom (0.0);
        public static readonly Fixed32 One = Fixed32.CreateFrom (1.0);

        public static Fixed32 Sqrt (Fixed32 v) { return Fixed32.Sqrt (v); }

        public static Fixed32 Sin (Fixed32 v) { return Fixed32.Sin (v); }
        public static Fixed32 Cos (Fixed32 v) { return Fixed32.Cos (v); }
        public static Fixed32 Tan (Fixed32 v) { return Fixed32.Tan (v); }
        public static Fixed32 ArcCos (Fixed32 v) { return Fixed32.ArcCos (v); }
        public static Fixed32 ArcSin (Fixed32 v) { return Fixed32.ArcSin (v); }
        public static Fixed32 ArcTan (Fixed32 v) { return Fixed32.ArcTan (v); }

        public static Fixed32 ToRadians          (Fixed32 input) { return input * Deg2Rad; }
        public static Fixed32 ToDegrees          (Fixed32 input) { return input * Rad2Deg; }
        public static Fixed32 FromFraction       (Int32 numerator, Int32 denominator) { return (Fixed32) numerator / (Fixed32) denominator; }
        public static Fixed32 FromFraction       (Int64 numerator, Int64 denominator) { return (Fixed32) numerator / (Fixed32) denominator; }

        public static Fixed32 Min                (Fixed32 a, Fixed32 b) { return a < b ? a : b; }
        public static Fixed32 Max                (Fixed32 a, Fixed32 b) { return a > b ? a : b; }
        public static Fixed32 Clamp              (Fixed32 value, Fixed32 min, Fixed32 max) { if (value < min) return min; else if (value > max) return max; else return value; }
        public static Fixed32 Lerp               (Fixed32 a, Fixed32 b, Fixed32 t) { return a + ((b - a) * t); }
        public static Fixed32 Abs                (Fixed32 v) { return (v < 0) ? -v : v; }

        public static Fixed32 FromString         (String str) { Fixed32 result = Zero; Fixed32.TryParse (str, out result); return result; }
        public static void    FromString        (String str, out Fixed32 value) { Fixed32.TryParse (str, out value); }

        public static Boolean IsZero            (Fixed32 value) { return Abs(value) < Epsilon; }
        public static Boolean WithinEpsilon     (Fixed32 a, Fixed32 b) { Fixed32 num = a - b; return ((-Epsilon <= num) && (num <= Epsilon)); }
        public static Int32   Sign              (Fixed32 value) { if (value > 0) return 1; else if (value < 0) return -1; return 0; }
    }


    internal static class Int32Extensions { // http://msdn.microsoft.com/en-us/library/system.object.gethashcode(v=vs.110).aspx
        public static Int32 ShiftAndWrap (this Int32 value, Int32 positions = 2) {
            positions = positions & 0x1F;
            uint number = BitConverter.ToUInt32 (BitConverter.GetBytes(value), 0);
            uint wrapped = number >> (32 - positions);
            return BitConverter.ToInt32 (BitConverter.GetBytes ((number << positions) | wrapped), 0);
        }
    }
}
