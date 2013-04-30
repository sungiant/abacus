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

namespace Sungiant.Abacus
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Half
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
    }
}