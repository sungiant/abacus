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
using NUnit.Framework;

namespace Sungiant.Abacus.Tests
{

	[TestFixture]
    public class RealMaths
	{
	}

    [TestFixture]
    public class PackUtils
    {
    }

}

namespace Sungiant.Abacus.Packed.Tests
{
	public static class Settings
    {
        public const Int32 NumTests = 50000;
    }

    [TestFixture]
    public static class Alpha_8Tests
    {
        [Test]
        public static void TestAllPossibleValues()
        {
            for (Byte packed = Byte.MinValue; packed < Byte.MaxValue; ++packed)
            {
                var packedObj = new Alpha_8();

                packedObj.PackedValue = packed;

                Single unpacked;

                packedObj.UnpackTo(out unpacked);

                var newPackedObj = new Alpha_8(unpacked);

                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }

    [TestFixture]
    public class Bgr_5_6_5Tests
    {
        [Test]
        public static void TestAllPossibleValues()
        {
            for (UInt16 packed = UInt16.MinValue; packed < UInt16.MaxValue; ++packed)
            {
                var packedObj = new Bgr_5_6_5();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector3 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Bgr_5_6_5(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Bgra16Tests
    {
        [Test]
        public static void TestAllPossibleValues()
        {
            for (UInt16 packed = UInt16.MinValue; packed < UInt16.MaxValue; ++packed)
            {
                var packedObj = new Bgra16();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Bgra16(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Bgra_5_5_5_1Tests
    {
        [Test]
        public static void TestAllPossibleValues()
        {
            for (UInt16 packed = UInt16.MinValue; packed < UInt16.MaxValue; ++packed)
            {
                var packedObj = new Bgra_5_5_5_1();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);

                var newPackedObj = new Bgra_5_5_5_1(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Byte4Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];

            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);

                var packedObj = new Byte4();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Byte4(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class NormalisedByte2Tests
    {
        [Test]
        public static void TestAllPossibleValues()
        {
            for (UInt16 packed = UInt16.MinValue; packed < UInt16.MaxValue; ++packed)
            {
                var packedObj = new NormalisedByte2();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector2 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new NormalisedByte2(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class NormalisedByte4Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);
                var packedObj = new NormalisedByte4();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new NormalisedByte4(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class NormalisedShort2Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);
                var packedObj = new NormalisedShort2();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector2 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new NormalisedShort2(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Rg32Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);
                var packedObj = new Rg32();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector2 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Rg32(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Rgba32Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);
                var packedObj = new Rgba32();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Rgba32(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Rgba64Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[8];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt64(buff, 0);
                UInt64 packed = BitConverter.ToUInt64(buff, 0);
                
                var packedObj = new Rgba64();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Rgba64(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Rgba_10_10_10_2Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);
                var packedObj = new Rgba_10_10_10_2();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Rgba_10_10_10_2(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Short2Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[4];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt32(buff, 0);
                UInt32 packed = BitConverter.ToUInt32(buff, 0);
                var packedObj = new Short2();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector2 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Short2(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }
    [TestFixture]
    public class Short4Tests
    {
        [Test]
        public static void TestRandomValues()
        {
            var rand = new System.Random();
            var buff = new Byte[8];
            
            for(Int32 i = 0; i < Settings.NumTests; ++i)
            {
                rand.NextBytes(buff);
                BitConverter.ToUInt64(buff, 0);

                UInt64 packed = BitConverter.ToUInt64(buff, 0);
                
                var packedObj = new Short4();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new Short4(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed));
            }
        }
    }

}


namespace Sungiant.Abacus.SinglePrecision.Tests
{
	[TestFixture]
	public class Matrix44Tests
	{
		[Test]
		public void TestTranspose_MemberFn ()
		{
			Matrix44 startMatrix = new Matrix44(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

			Matrix44 testMatrix = startMatrix;

			Matrix44 testMatrixExpectedTranspose = new Matrix44(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

			testMatrix.Transpose();

			Assert.That(testMatrix, Is.EqualTo(testMatrixExpectedTranspose));
		}

		[Test]
		public void TestTranspose_StaticFn ()
		{
			Matrix44 startMatrix = new Matrix44(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

			Matrix44 testMatrix = startMatrix;

			Matrix44 testMatrixExpectedTranspose = new Matrix44(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

			// RUN THE STATIC VERSION OF THE FUNCTION
			Matrix44 resultMatrix = Matrix44.Identity;

			Matrix44.Transpose(ref testMatrix, out resultMatrix);

			Assert.That(resultMatrix, Is.EqualTo(testMatrixExpectedTranspose));

		}

		[Test]
		public void CreateFromQuaternion ()
		{
			Single pi; RealMaths.Pi(out pi);

			Quaternion q;
			Quaternion.CreateFromYawPitchRoll(pi, pi*2, pi / 2, out q);

			Matrix44 mat1;
			Matrix44.CreateFromQuaternion(ref q, out mat1);

			Matrix44 mat2;
			Matrix44.CreateFromQuaternionOld(ref q, out mat2);

			Assert.That(mat1, Is.EqualTo(mat2));
		}

		[Test]
		public void Decompose ()
		{
            Matrix44 scale;
            Matrix44.CreateScale(4, 2, 3, out scale);

            Matrix44 rotation;
            Single pi; RealMaths.Pi(out pi);
            Matrix44.CreateRotationY(pi, out rotation);

            Matrix44 translation;
            Matrix44.CreateTranslation(100, 5, 3, out translation);

            Matrix44 m = rotation * scale;
            //m = translation * m;
			m.Translation = new Vector3(100, 5, 3);

            Vector3 outScale;
            Quaternion outRotation;
            Vector3 outTranslation;

            m.Decompose(out outScale, out outRotation, out outTranslation);

			Matrix44 mat;
            Matrix44.CreateFromQuaternion(ref outRotation, out mat);

			Assert.That(outScale, Is.EqualTo(new Vector3(4, 2, 3)));
			Assert.That(mat, Is.EqualTo(rotation));
			Assert.That(outTranslation, Is.EqualTo(new Vector3(100, 5, 3)));

		}
	}}

namespace Sungiant.Abacus.DoublePrecision.Tests
{
	[TestFixture]
	public class Matrix44Tests
	{
		[Test]
		public void TestTranspose_MemberFn ()
		{
			Matrix44 startMatrix = new Matrix44(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

			Matrix44 testMatrix = startMatrix;

			Matrix44 testMatrixExpectedTranspose = new Matrix44(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

			testMatrix.Transpose();

			Assert.That(testMatrix, Is.EqualTo(testMatrixExpectedTranspose));
		}

		[Test]
		public void TestTranspose_StaticFn ()
		{
			Matrix44 startMatrix = new Matrix44(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

			Matrix44 testMatrix = startMatrix;

			Matrix44 testMatrixExpectedTranspose = new Matrix44(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

			// RUN THE STATIC VERSION OF THE FUNCTION
			Matrix44 resultMatrix = Matrix44.Identity;

			Matrix44.Transpose(ref testMatrix, out resultMatrix);

			Assert.That(resultMatrix, Is.EqualTo(testMatrixExpectedTranspose));

		}

		[Test]
		public void CreateFromQuaternion ()
		{
			Double pi; RealMaths.Pi(out pi);

			Quaternion q;
			Quaternion.CreateFromYawPitchRoll(pi, pi*2, pi / 2, out q);

			Matrix44 mat1;
			Matrix44.CreateFromQuaternion(ref q, out mat1);

			Matrix44 mat2;
			Matrix44.CreateFromQuaternionOld(ref q, out mat2);

			Assert.That(mat1, Is.EqualTo(mat2));
		}

		[Test]
		public void Decompose ()
		{
            Matrix44 scale;
            Matrix44.CreateScale(4, 2, 3, out scale);

            Matrix44 rotation;
            Double pi; RealMaths.Pi(out pi);
            Matrix44.CreateRotationY(pi, out rotation);

            Matrix44 translation;
            Matrix44.CreateTranslation(100, 5, 3, out translation);

            Matrix44 m = rotation * scale;
            //m = translation * m;
			m.Translation = new Vector3(100, 5, 3);

            Vector3 outScale;
            Quaternion outRotation;
            Vector3 outTranslation;

            m.Decompose(out outScale, out outRotation, out outTranslation);

			Matrix44 mat;
            Matrix44.CreateFromQuaternion(ref outRotation, out mat);

			Assert.That(outScale, Is.EqualTo(new Vector3(4, 2, 3)));
			Assert.That(mat, Is.EqualTo(rotation));
			Assert.That(outTranslation, Is.EqualTo(new Vector3(100, 5, 3)));

		}
	}}

