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
        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }
        
        public bool ToBoolean(IFormatProvider provider)
        {
            if (rawData != 0)
                return false;
            
            return true;
        }
        
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(ToSingle());
        }
        
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(ToSingle());
        }
        
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(ToSingle());
        }
        
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(ToSingle());
        }
        
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return ToSingle();
        }
        
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(ToSingle());
        }
        
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(ToSingle());
        }
        
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(ToSingle());
        }
        
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(ToSingle());
        }
        
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(ToSingle());
        }
        
        string IConvertible.ToString(IFormatProvider provider)
        {
            return this.ToString();
        }
        
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
#if NETFW_XBOX360 || NETFW_WP75
            return Convert.ChangeType(ToSingle(), conversionType, null);
#else
            return Convert.ChangeType(ToSingle(), conversionType);
#endif
        }
        
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(ToSingle());
        }
        
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(ToSingle());
        }
        
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(ToSingle());
        }
    }
}
