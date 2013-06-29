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
	public interface IPackedReal
	{
		void PackFrom(Single input);
		void UnpackTo(out Single output);

		void PackFrom(Double input);
		void UnpackTo(out Double output);

	}

	public interface IPackedReal2
	{
		void PackFrom(ref SinglePrecision.Vector2 input);
		void UnpackTo(out SinglePrecision.Vector2 output);

		void PackFrom(ref DoublePrecision.Vector2 input);
		void UnpackTo(out DoublePrecision.Vector2 output);

	}

	public interface IPackedReal3
	{
		void PackFrom(ref SinglePrecision.Vector3 input);
		void UnpackTo(out SinglePrecision.Vector3 output);

		void PackFrom(ref DoublePrecision.Vector3 input);
		void UnpackTo(out DoublePrecision.Vector3 output);

	}

	public interface IPackedReal4
	{
		void PackFrom(ref SinglePrecision.Vector4 input);
		void UnpackTo(out SinglePrecision.Vector4 output);

		void PackFrom(ref DoublePrecision.Vector4 input);
		void UnpackTo(out DoublePrecision.Vector4 output);

	}

	// T is the type that the value is packed into
	public interface IPackedValue<T>
	{
		T PackedValue { get; set; }
	}

	internal static class PackUtils
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

		public static void Half(out Single value) { value = 0.5f; }
		public static void Half(out Double value) { value = 0.5; }

		public static void One(out Single value) { value = 1f; }
		public static void One(out Double value) { value = 1; }

        // TODO: Improve upon the accuracy of the following mathematical constants.
		public static void E(out Single value) { value = 71828183f; }
		public static void E(out Double value) { value = 71828183; }

		public static void Log10E(out Single value) { value = 0.4342945f; }
		public static void Log10E(out Double value) { value = 0.4342945; }

		public static void Log2E(out Single value) { value = 1.442695f; }
		public static void Log2E(out Double value) { value = 1.442695; }

		public static void Pi(out Single value) { value = 3.1415926536f; }
		public static void Pi(out Double value) { value = 3.1415926536; }

		public static void PiOver2(out Single value) { value = 1.570796f; }
		public static void PiOver2(out Double value) { value = 1.570796; }

		public static void PiOver4(out Single value) { value = 0.7853982f; }
		public static void PiOver4(out Double value) { value = 0.7853982; }

		public static void Tau(out Single value) { value = 6.283185f; }
		public static void Tau(out Double value) { value = 6.283185; }

		public static void Epsilon(out Single value) { value = 1.0e-6f; }
		public static void Epsilon(out Double value) { value = 1.0e-6; }

		public static void Root2(out Single value) { value = 1.41421f; }
		public static void Root2(out Double value) { value = 1.41421; }

		public static void Root3(out Single value) { value = 1.73205f; }
		public static void Root3(out Double value) { value = 1.73205; }

		internal static void TestTolerance(out Single value) { value = 1.0e-4f; }
		internal static void TestTolerance(out Double value) { value = 1.0e-7; }

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


        //--------------------------------------------------------------
        // FromFraction(numerator, denominator, &val)
        //
        public static void FromFraction(Int32 numerator, Int32 denominator, out Single value)
        {
            value = (Single) numerator / (Single) denominator;
        }
        public static void FromFraction(Int32 numerator, Int32 denominator, out Double value)
        {
            value = (Double) numerator / (Double) denominator;
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

		//--------------------------------------------------------------
		// WithinEpsilon(a, b)
		//
		public static Boolean WithinEpsilon(Single a, Single b)
		{
			Single num = a - b;
			return ((-Single.Epsilon <= num) && (num <= Single.Epsilon));
		}
		public static Boolean WithinEpsilon(Double a, Double b)
		{
			Double num = a - b;
			return ((-Double.Epsilon <= num) && (num <= Double.Epsilon));
		}
	}

	public static class RandomExtensions
	{
		// Returns a random Single between 0.0 & 1.0
		public static Single NextSingle(this System.Random r)
		{
			return (Single) r.NextDouble();
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------

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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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

		// SINGLE PRECISION CASTS ----------------------------------------------------------------
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
	}
}


namespace Sungiant.Abacus.SinglePrecision
{
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
	public partial struct Vector2
		: IEquatable<Vector2>
	{
		/// <summary>
		/// Gets or sets the x-component of the vector.
		/// </summary>
		public Single X;

		/// <summary>
		/// Gets or sets the y-component of the vector.
		/// </summary>
		public Single Y;

		/// <summary>
		/// Initilises a new instance ofVector2 from two Single values 
		/// representing X and Y respectively.
		/// </summary>
		public Vector2 (Single x, Single y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Calculates the length of the vector.
		/// </summary>
		public Single Length ()
		{
			Single num = (this.X * this.X) + (this.Y * this.Y);
			return RealMaths.Sqrt (num);
		}

		/// <summary>
		/// Calculates the length of the vector squared.
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
			return string.Format ("{{X:{0} Y:{1}}}", new Object[] { this.X.ToString (), this.Y.ToString () });
		}

		/// <summary>
		/// Gets the hash code of the vector object.
		/// </summary>
		public override Int32 GetHashCode ()
		{
			return (this.X.GetHashCode () + this.Y.GetHashCode ());
		}

		/// <summary>
		/// Detemines whether the vector is of unit length.
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
		/// Defines the unit vector for the x-axis.
		/// </summary>
		readonly static Vector2 unitX;

		/// <summary>
		/// Defines the unit vector for the y-axis.
		/// </summary>
		readonly static Vector2 unitY;

		/// <summary>
		/// Static constructor used to initilise static constants.
		/// </summary>
		static Vector2 ()
		{
			Single temp_one; RealMaths.One(out temp_one);
			Single temp_zero; RealMaths.Zero(out temp_zero);

			zero = new Vector2 ();
			one = new Vector2 (temp_one, temp_one);
			unitX = new Vector2 (temp_one, temp_zero);
			unitY = new Vector2 (temp_zero, temp_one);
		}

		/// <summary>
		/// Returns a Vector2 with all of its components set to zero.
		/// </summary>
		public static Vector2 Zero
		{
			get { return zero; }
		}
		
		/// <summary>
		/// Returns a Vector2 with both of its components set to one.
		/// </summary>
		public static Vector2 One
		{
			get { return one; }
		}
		
		/// <summary>
		/// Returns the unit vector for the x-axis.
		/// </summary>
		public static Vector2 UnitX
		{
			get { return unitX; }
		}
		
		/// <summary>
		/// Returns the unit vector for the y-axis.
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
			ref Vector2 value1, ref Vector2 value2, out Single result)
		{
			Single a = value1.X - value2.X;
			Single b = value1.Y - value2.Y;
			
			Single c = (a * a) + (b * b);

			result = RealMaths.Sqrt (c);
		}

		/// <summary>
		/// Calculates the distance between two vectors squared.
		/// </summary>
		public static void DistanceSquared (
			ref Vector2 value1, ref Vector2 value2, out Single result)
		{
			Single a = value1.X - value2.X;
			Single b = value1.Y - value2.Y;
			
			result = (a * a) + (b * b);
		}

		/// <summary>
		/// Calculates the dot product of two vectors. If the two vectors are 
		/// unit vectors, the dot product returns a floating point value between
		/// -1 and 1 that can be used to determine some properties of the angle 
		/// between two vectors. For example, it can show whether the vectors 
		/// are orthogonal, parallel, or have an acute or obtuse angle between 
		/// them.
		/// </summary>
		public static void Dot (
			ref Vector2 value1, ref Vector2 value2, out Single result)
		{
			result = (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		/// <summary>
		/// Creates a unit vector from the specified vector. The result is a 
		/// vector one unit in length pointing in the same direction as the 
		/// original vector.
		/// </summary>
		public static void Normalise (ref Vector2 value, out Vector2 result)
		{
			Single lengthSquared = 
				(value.X * value.X) + (value.Y * value.Y);

			Single epsilon; RealMaths.Epsilon(out epsilon);

			if( lengthSquared <= epsilon || 
				Single.IsInfinity(lengthSquared) )
			{
				throw new ArgumentOutOfRangeException();
			}

			Single one = 1;
			Single multiplier = one / RealMaths.Sqrt (lengthSquared);

			result.X = value.X * multiplier;
			result.Y = value.Y * multiplier;

		}

		/// <summary>
		/// Returns the value of an incident vector reflected across the a 
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

			Single twoDot = dot * 2;

			// Starting vector minus twice the length of the adjcent projected 
			// along the normal.
			result = vector - (twoDot * normal);
		}

		/// <summary>
		/// Transforms a vector normal by a matrix.
		/// </summary>
		public static void TransformNormal (
			ref Vector2 normal, ref Matrix44 matrix, out Vector2 result)
		{
			if( !normal.IsUnit() )
			{
				throw new ArgumentOutOfRangeException();
			}

			Single a = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
			Single b = (normal.X * matrix.M12) + (normal.Y * matrix.M22);
			
			result.X = a;
			result.Y = b;
		}

		/// <summary>
		/// Transforms a Vector3 by a specified Matrix.
		/// </summary>
		public static void Transform (
			ref Vector2 position, ref Matrix44 matrix, out Vector2 result)
		{
			Single a = 
				((position.X * matrix.M11) + (position.Y * matrix.M21)) + 
				matrix.M41;

			Single b = 
				((position.X * matrix.M12) + (position.Y * matrix.M22)) + 
				matrix.M42;
			
			result.X = a;
			result.Y = b;
		}

		/// <summary>
		/// Transforms a Vector3 by a specified Quaternion.
		/// </summary>
		public static void Transform (
			ref Vector2 value, ref Quaternion rotation, out Vector2 result)
		{
			Single one = 1;

			Single a = rotation.X + rotation.X;
			Single b = rotation.Y + rotation.Y;
			Single c = rotation.Z + rotation.Z;
			Single d = rotation.W * c;
			Single e = rotation.X * a;
			Single f = rotation.X * b;
			Single g = rotation.Y * b;
			Single h = rotation.Z * c;
			Single i = (value.X * ((one - g) - h)) + (value.Y * (f - d));
			Single j = (value.X * (f + d)) + (value.Y * ((one - e) - h));

			result.X = i;
			result.Y = j;
		}
		
		// Equality Operators //----------------------------------------------//

		/// <summary>
		///
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
		///
		/// </summary>
		public Boolean Equals (Vector2 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y));
		}

		#endregion

		/// <summary>
		///
		/// </summary>
		public static Boolean operator == (Vector2 value1, Vector2 value2)
		{
			return ((value1.X == value2.X) && (value1.Y == value2.Y));
		}

		/// <summary>
		///
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
		///
		/// </summary>
		public static void Add (
			ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		/// <summary>
		///
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
		///
		/// </summary>
		public static void Subtract (
			ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		/// <summary>
		///
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
		///
		/// </summary>
		public static void Negate (ref Vector2 value, out Vector2 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		/// <summary>
		///
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
		///
		/// </summary>
		public static void Multiply (
			ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}

		/// <summary>
		///
		/// </summary>
		public static void Multiply (
			ref Vector2 value1, Single scaleFactor, out Vector2 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		/// <summary>
		///
		/// </summary>
		public static Vector2 operator * (
			Single scaleFactor, Vector2 value)
		{
			Vector2 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}

		/// <summary>
		///
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
		///
		/// </summary>		
		public static Vector2 operator * (
			Vector2 value, Single scaleFactor)
		{
			Vector2 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}

		// Division Operators //----------------------------------------------//

		/// <summary>
		///
		/// </summary>
		public static void Divide (
			ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		/// <summary>
		///
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
		///
		/// </summary>
		public static Vector2 operator / (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			return vector;
		}

		/// <summary>
		///
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
		/// Interpolates between two values using a cubic equation.
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

			// Make sure that the weighting value is within the supported range.
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

			// Make sure that the weighting value is within the supported range.
			if( amount < zero || amount > one )
			{
				throw new ArgumentOutOfRangeException();
			}

			Single half; RealMaths.Half(out half);
			Single two = 2;
			Single three = 3;
			Single four = 4;
			Single five = 5;

			Single temp = amount * amount;
			Single temp2 = amount * temp;

			result.X = 
				half * ((((two * b.X) + ((-a.X + c.X) * amount)) + 
				(((((two * a.X) - (five * b.X)) + (four * c.X)) - d.X) * 
				temp)) + ((((-a.X + (three * b.X)) - (three * c.X)) + d.X) * 
				temp2));
			
			result.Y = half * ((((two * b.Y) + ((-a.Y + c.Y) * amount)) + 
				(((((two * a.Y) - (five * b.Y)) + (four * c.Y)) - d.Y) * 
				temp)) + ((((-a.Y + (three * b.Y)) - (three * c.Y)) + d.Y) * 
				temp2));
		}

		/// <summary>
		/// Performs a Hermite spline interpolation.
		/// </summary>
		public static void Hermite (
			ref Vector2 a, 
			ref Vector2 tangent1, 
			ref Vector2 b, 
			ref Vector2 tangent2, 
			Single amount, 
			out Vector2 result)
		{
			Single zero = 0;
			Single one = 1;
			Single two = 2;
			Single three = 3;

			// Make sure that the weighting value is within the supported range.
			if( amount < zero || amount > one )
			{
				throw new ArgumentOutOfRangeException();
			}

			// Make sure that the tangents have been normalised.
			if( !tangent1.IsUnit() || !tangent2.IsUnit() )
			{
				throw new ArgumentOutOfRangeException();
			}

			Single temp = amount * amount;
			Single temp2 = amount * temp;
			Single temp6 = ((two * temp2) - (three * temp)) + one;
			Single temp5 = (-two * temp2) + (three * temp);
			Single temp4 = (temp2 - (two * temp)) + amount;
			Single temp3 = temp2 - temp;

			result.X = 
				(((a.X * temp6) + (b.X * temp5)) + 
				(tangent1.X * temp4)) + (tangent2.X * temp3);

			result.Y = 
				(((a.Y * temp6) + (b.Y * temp5)) + 
				(tangent1.Y * temp4)) + (tangent2.Y * temp3);
		}

		// Utilities //-------------------------------------------------------//

		/// <summary>
		/// Returns a vector that contains the lowest value from each matching pair of components.
		/// </summary>
		public static void Min (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
		}

		/// <summary>
		/// Returns a vector that contains the highest value from each matching pair of components.
		/// </summary>
		public static void Max (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
		}

		/// <summary>
		/// Restricts a value to be within a specified range.
		/// </summary>
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

		/// <summary>
		/// Performs a linear interpolation between two vectors.
		/// </summary>
		public static void Lerp (ref Vector2 value1, ref Vector2 value2, Single amount, out Vector2 result)
		{
			Single zero = 0;
			Single one = 1;
			if( amount < zero || amount > one )
			{
				throw new ArgumentOutOfRangeException();
			}
			
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
		}

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
	public partial struct Vector2
		: IEquatable<Vector2>
	{
		/// <summary>
		/// Gets or sets the x-component of the vector.
		/// </summary>
		public Double X;

		/// <summary>
		/// Gets or sets the y-component of the vector.
		/// </summary>
		public Double Y;

		/// <summary>
		/// Initilises a new instance ofVector2 from two Double values 
		/// representing X and Y respectively.
		/// </summary>
		public Vector2 (Double x, Double y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Calculates the length of the vector.
		/// </summary>
		public Double Length ()
		{
			Double num = (this.X * this.X) + (this.Y * this.Y);
			return RealMaths.Sqrt (num);
		}

		/// <summary>
		/// Calculates the length of the vector squared.
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
			return string.Format ("{{X:{0} Y:{1}}}", new Object[] { this.X.ToString (), this.Y.ToString () });
		}

		/// <summary>
		/// Gets the hash code of the vector object.
		/// </summary>
		public override Int32 GetHashCode ()
		{
			return (this.X.GetHashCode () + this.Y.GetHashCode ());
		}

		/// <summary>
		/// Detemines whether the vector is of unit length.
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
		/// Defines the unit vector for the x-axis.
		/// </summary>
		readonly static Vector2 unitX;

		/// <summary>
		/// Defines the unit vector for the y-axis.
		/// </summary>
		readonly static Vector2 unitY;

		/// <summary>
		/// Static constructor used to initilise static constants.
		/// </summary>
		static Vector2 ()
		{
			Double temp_one; RealMaths.One(out temp_one);
			Double temp_zero; RealMaths.Zero(out temp_zero);

			zero = new Vector2 ();
			one = new Vector2 (temp_one, temp_one);
			unitX = new Vector2 (temp_one, temp_zero);
			unitY = new Vector2 (temp_zero, temp_one);
		}

		/// <summary>
		/// Returns a Vector2 with all of its components set to zero.
		/// </summary>
		public static Vector2 Zero
		{
			get { return zero; }
		}
		
		/// <summary>
		/// Returns a Vector2 with both of its components set to one.
		/// </summary>
		public static Vector2 One
		{
			get { return one; }
		}
		
		/// <summary>
		/// Returns the unit vector for the x-axis.
		/// </summary>
		public static Vector2 UnitX
		{
			get { return unitX; }
		}
		
		/// <summary>
		/// Returns the unit vector for the y-axis.
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
			ref Vector2 value1, ref Vector2 value2, out Double result)
		{
			Double a = value1.X - value2.X;
			Double b = value1.Y - value2.Y;
			
			Double c = (a * a) + (b * b);

			result = RealMaths.Sqrt (c);
		}

		/// <summary>
		/// Calculates the distance between two vectors squared.
		/// </summary>
		public static void DistanceSquared (
			ref Vector2 value1, ref Vector2 value2, out Double result)
		{
			Double a = value1.X - value2.X;
			Double b = value1.Y - value2.Y;
			
			result = (a * a) + (b * b);
		}

		/// <summary>
		/// Calculates the dot product of two vectors. If the two vectors are 
		/// unit vectors, the dot product returns a floating point value between
		/// -1 and 1 that can be used to determine some properties of the angle 
		/// between two vectors. For example, it can show whether the vectors 
		/// are orthogonal, parallel, or have an acute or obtuse angle between 
		/// them.
		/// </summary>
		public static void Dot (
			ref Vector2 value1, ref Vector2 value2, out Double result)
		{
			result = (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		/// <summary>
		/// Creates a unit vector from the specified vector. The result is a 
		/// vector one unit in length pointing in the same direction as the 
		/// original vector.
		/// </summary>
		public static void Normalise (ref Vector2 value, out Vector2 result)
		{
			Double lengthSquared = 
				(value.X * value.X) + (value.Y * value.Y);

			Double epsilon; RealMaths.Epsilon(out epsilon);

			if( lengthSquared <= epsilon || 
				Double.IsInfinity(lengthSquared) )
			{
				throw new ArgumentOutOfRangeException();
			}

			Double one = 1;
			Double multiplier = one / RealMaths.Sqrt (lengthSquared);

			result.X = value.X * multiplier;
			result.Y = value.Y * multiplier;

		}

		/// <summary>
		/// Returns the value of an incident vector reflected across the a 
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

			Double twoDot = dot * 2;

			// Starting vector minus twice the length of the adjcent projected 
			// along the normal.
			result = vector - (twoDot * normal);
		}

		/// <summary>
		/// Transforms a vector normal by a matrix.
		/// </summary>
		public static void TransformNormal (
			ref Vector2 normal, ref Matrix44 matrix, out Vector2 result)
		{
			if( !normal.IsUnit() )
			{
				throw new ArgumentOutOfRangeException();
			}

			Double a = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
			Double b = (normal.X * matrix.M12) + (normal.Y * matrix.M22);
			
			result.X = a;
			result.Y = b;
		}

		/// <summary>
		/// Transforms a Vector3 by a specified Matrix.
		/// </summary>
		public static void Transform (
			ref Vector2 position, ref Matrix44 matrix, out Vector2 result)
		{
			Double a = 
				((position.X * matrix.M11) + (position.Y * matrix.M21)) + 
				matrix.M41;

			Double b = 
				((position.X * matrix.M12) + (position.Y * matrix.M22)) + 
				matrix.M42;
			
			result.X = a;
			result.Y = b;
		}

		/// <summary>
		/// Transforms a Vector3 by a specified Quaternion.
		/// </summary>
		public static void Transform (
			ref Vector2 value, ref Quaternion rotation, out Vector2 result)
		{
			Double one = 1;

			Double a = rotation.X + rotation.X;
			Double b = rotation.Y + rotation.Y;
			Double c = rotation.Z + rotation.Z;
			Double d = rotation.W * c;
			Double e = rotation.X * a;
			Double f = rotation.X * b;
			Double g = rotation.Y * b;
			Double h = rotation.Z * c;
			Double i = (value.X * ((one - g) - h)) + (value.Y * (f - d));
			Double j = (value.X * (f + d)) + (value.Y * ((one - e) - h));

			result.X = i;
			result.Y = j;
		}
		
		// Equality Operators //----------------------------------------------//

		/// <summary>
		///
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
		///
		/// </summary>
		public Boolean Equals (Vector2 other)
		{
			return ((this.X == other.X) && (this.Y == other.Y));
		}

		#endregion

		/// <summary>
		///
		/// </summary>
		public static Boolean operator == (Vector2 value1, Vector2 value2)
		{
			return ((value1.X == value2.X) && (value1.Y == value2.Y));
		}

		/// <summary>
		///
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
		///
		/// </summary>
		public static void Add (
			ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		/// <summary>
		///
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
		///
		/// </summary>
		public static void Subtract (
			ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		/// <summary>
		///
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
		///
		/// </summary>
		public static void Negate (ref Vector2 value, out Vector2 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		/// <summary>
		///
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
		///
		/// </summary>
		public static void Multiply (
			ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}

		/// <summary>
		///
		/// </summary>
		public static void Multiply (
			ref Vector2 value1, Double scaleFactor, out Vector2 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		/// <summary>
		///
		/// </summary>
		public static Vector2 operator * (
			Double scaleFactor, Vector2 value)
		{
			Vector2 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}

		/// <summary>
		///
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
		///
		/// </summary>		
		public static Vector2 operator * (
			Vector2 value, Double scaleFactor)
		{
			Vector2 vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}

		// Division Operators //----------------------------------------------//

		/// <summary>
		///
		/// </summary>
		public static void Divide (
			ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		/// <summary>
		///
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
		///
		/// </summary>
		public static Vector2 operator / (Vector2 value1, Vector2 value2)
		{
			Vector2 vector;
			vector.X = value1.X / value2.X;
			vector.Y = value1.Y / value2.Y;
			return vector;
		}

		/// <summary>
		///
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
		/// Interpolates between two values using a cubic equation.
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

			// Make sure that the weighting value is within the supported range.
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

			// Make sure that the weighting value is within the supported range.
			if( amount < zero || amount > one )
			{
				throw new ArgumentOutOfRangeException();
			}

			Double half; RealMaths.Half(out half);
			Double two = 2;
			Double three = 3;
			Double four = 4;
			Double five = 5;

			Double temp = amount * amount;
			Double temp2 = amount * temp;

			result.X = 
				half * ((((two * b.X) + ((-a.X + c.X) * amount)) + 
				(((((two * a.X) - (five * b.X)) + (four * c.X)) - d.X) * 
				temp)) + ((((-a.X + (three * b.X)) - (three * c.X)) + d.X) * 
				temp2));
			
			result.Y = half * ((((two * b.Y) + ((-a.Y + c.Y) * amount)) + 
				(((((two * a.Y) - (five * b.Y)) + (four * c.Y)) - d.Y) * 
				temp)) + ((((-a.Y + (three * b.Y)) - (three * c.Y)) + d.Y) * 
				temp2));
		}

		/// <summary>
		/// Performs a Hermite spline interpolation.
		/// </summary>
		public static void Hermite (
			ref Vector2 a, 
			ref Vector2 tangent1, 
			ref Vector2 b, 
			ref Vector2 tangent2, 
			Double amount, 
			out Vector2 result)
		{
			Double zero = 0;
			Double one = 1;
			Double two = 2;
			Double three = 3;

			// Make sure that the weighting value is within the supported range.
			if( amount < zero || amount > one )
			{
				throw new ArgumentOutOfRangeException();
			}

			// Make sure that the tangents have been normalised.
			if( !tangent1.IsUnit() || !tangent2.IsUnit() )
			{
				throw new ArgumentOutOfRangeException();
			}

			Double temp = amount * amount;
			Double temp2 = amount * temp;
			Double temp6 = ((two * temp2) - (three * temp)) + one;
			Double temp5 = (-two * temp2) + (three * temp);
			Double temp4 = (temp2 - (two * temp)) + amount;
			Double temp3 = temp2 - temp;

			result.X = 
				(((a.X * temp6) + (b.X * temp5)) + 
				(tangent1.X * temp4)) + (tangent2.X * temp3);

			result.Y = 
				(((a.Y * temp6) + (b.Y * temp5)) + 
				(tangent1.Y * temp4)) + (tangent2.Y * temp3);
		}

		// Utilities //-------------------------------------------------------//

		/// <summary>
		/// Returns a vector that contains the lowest value from each matching pair of components.
		/// </summary>
		public static void Min (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = (value1.X < value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
		}

		/// <summary>
		/// Returns a vector that contains the highest value from each matching pair of components.
		/// </summary>
		public static void Max (ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = (value1.X > value2.X) ? value1.X : value2.X;
			result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
		}

		/// <summary>
		/// Restricts a value to be within a specified range.
		/// </summary>
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

		/// <summary>
		/// Performs a linear interpolation between two vectors.
		/// </summary>
		public static void Lerp (ref Vector2 value1, ref Vector2 value2, Double amount, out Vector2 result)
		{
			Double zero = 0;
			Double one = 1;
			if( amount < zero || amount > one )
			{
				throw new ArgumentOutOfRangeException();
			}
			
			result.X = value1.X + ((value2.X - value1.X) * amount);
			result.Y = value1.Y + ((value2.Y - value1.Y) * amount);
		}

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

