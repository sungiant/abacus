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

namespace Sungiant.Abacus.Packed
{
	public partial struct Rgba32
	{
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

	}
}
