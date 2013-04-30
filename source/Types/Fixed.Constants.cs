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


	}
}
