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
    public partial struct Half
    {
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
    }
}

