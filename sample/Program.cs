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
using Sungiant.Abacus.Packed;

namespace Sungiant.Abacus.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Started With Abacus");
            Console.WriteLine("---------------------------");
            Console.WriteLine(System.Environment.NewLine);

            // Abacus supports 3 real number formats Single, Double & it's own Fixed32-Precision format


            // Abacus also supports packing data into various auxilary formats,
            // so data in .NET or Mono memory space can be directly uploaded to a GPU.

            // Examples follow for all fo the supported packed formats:
            
            { // Alpha_8
                Single initialValue = 0.7338f;
                var packedObj = new Alpha_8();
                packedObj.PackFrom(initialValue);
                Single unpackedValue;
                packedObj.UnpackTo(out unpackedValue);
                Console.WriteLine(string.Format("Alpha_8: value({0}), to packed value ({1}), back to unpacked value ({2})", initialValue, packedObj.PackedValue, unpackedValue));
            }


            Vector3 a = new Vector3(100f, -400f, 700f);

            Matrix44 mat;
            Matrix44.CreateFromYawPitchRoll(90f, 0f, 0f, out mat);

            Vector3 res;
            Vector3.Transform(ref a, ref mat, out res);

            //Console.WriteLine(res);


        }
    }
}
