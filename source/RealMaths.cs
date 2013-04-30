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
using System.Collections.Generic;
using System.Text;

namespace Sungiant.Abacus
{
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
