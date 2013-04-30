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
using Sungiant.Abacus.SinglePrecision;

namespace Sungiant.Abacus
{
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



 

}
