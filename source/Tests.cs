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
    public class NormalisedShort4Tests
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
                var packedObj = new NormalisedShort4();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector4 unpacked;
                
                packedObj.UnpackTo(out unpacked);
                
                var newPackedObj = new NormalisedShort4(ref unpacked);
                
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

namespace Sungiant.Abacus.Int32Precision.Tests
{
	[TestFixture]
	public class Point2Tests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class Point3Tests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
	}

namespace Sungiant.Abacus.Int64Precision.Tests
{
	[TestFixture]
	public class Point2Tests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class Point3Tests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
	}


namespace Sungiant.Abacus.SinglePrecision.Tests
{

	[TestFixture]
	public class BoundingBoxTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class BoundingFrustumTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class BoundingSphereTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class Matrix44Tests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
		
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
	}	[TestFixture]
	public class PlaneTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class QuadTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class QuaternionTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class RayTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class TriangleTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class Vector2Tests
	{
		[Test]
		public void Test_Constructors ()
		{
			// Test default values
			Vector2 a = new Vector2();
			Assert.That(a, Is.EqualTo(Vector2.Zero));

			// Test Vector2( n ) where n is Single
			Single u = -189;
			Single v = 429;
			Vector2 b1 = new Vector2(u);
			Assert.That(b1.X, Is.EqualTo(u));
			Assert.That(b1.Y, Is.EqualTo(u));
			Vector2 b2 = new Vector2(v);
			Assert.That(b2.X, Is.EqualTo(v));
			Assert.That(b2.Y, Is.EqualTo(v));

			// Test Vector2( x, y ) where x, y are Single
			Vector2 c = new Vector2(u, v);
			Assert.That(c.X, Is.EqualTo(u));
			Assert.That(c.Y, Is.EqualTo(v));

			// Test Vector2( x, y ) where x, y are Int32
			Int32 q = 12334;
			Int32 r = -2145;
			Single s = q;
			Single t = r;
			Vector2 d = new Vector2(q, r);
			Assert.That(d.X, Is.EqualTo(s));
			Assert.That(d.Y, Is.EqualTo(t));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Vector2 a = new Vector2(42, -17);

			String result = a.ToString();

			String expected = "{X:42 Y:-17}";

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			var hs1 = new System.Collections.Generic.HashSet<Vector2>();
			var hs2 = new System.Collections.Generic.HashSet<Int32>();

			for(Int32 i = 0; i < 10000; ++i)
			{
				var a = GetNextRandomVector2();

				hs1.Add(a);
				hs2.Add(a.GetHashCode());
			}

			Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
		}

		[Test]
		public void TestMemberFn_Set ()
		{
			Vector2 a = Vector2.Zero;

			a.Set(14, -19);

			Vector2 expected = new Vector2(14, -19);

			Assert.That(a, Is.EqualTo(expected));
		}

		[Test]
		public void TestMemberFn_Length ()
		{
			Vector2 a = new Vector2(30, -40);

			Single expected = 50;

			Single result = a.Length();

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestMemberFn_LengthSquared ()
		{
			Vector2 a = new Vector2(30, -40);

			Single expected = 2500;

			Single result = a.LengthSquared();

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestMemberFn_IsUnit_i ()
		{
			Assert.That(new Vector2(1, 0).IsUnit(), Is.EqualTo(true));
			Assert.That(new Vector2(-1, 0).IsUnit(), Is.EqualTo(true));
			Assert.That(new Vector2(1, 1).IsUnit(), Is.EqualTo(false));
			Assert.That(new Vector2(0, 0).IsUnit(), Is.EqualTo(false));
			Assert.That(new Vector2(0, -1).IsUnit(), Is.EqualTo(true));
			Assert.That(new Vector2(0, 1).IsUnit(), Is.EqualTo(true));
		}

		[Test]
		public void TestMemberFn_IsUnit_ii ()
		{
			for( Int32 i = 0; i < 100; ++ i)
			{
				Vector2 a = GetNextRandomVector2();

				Vector2 b; Vector2.Normalise(ref a, out b);

				Assert.That(b.IsUnit(), Is.EqualTo(true));
			}
		}

		[Test]
		public void TestMemberFn_IsUnit_iii ()
		{
			Single piOver2; RealMaths.PiOver2(out piOver2);

			for( Int32 i = 0; i <= 90; ++ i)
			{
				Single theta = piOver2 / 90 * i;

				Single opposite = RealMaths.Sin(theta);
				Single adjacent = RealMaths.Cos(theta);				

				Assert.That(new Vector2( opposite,  adjacent).IsUnit(), Is.EqualTo(true));
				Assert.That(new Vector2( opposite, -adjacent).IsUnit(), Is.EqualTo(true));
				Assert.That(new Vector2(-opposite,  adjacent).IsUnit(), Is.EqualTo(true));
				Assert.That(new Vector2(-opposite, -adjacent).IsUnit(), Is.EqualTo(true));
			}
		}

		static System.Random rand;

		static Vector2Tests()
		{
			rand = new System.Random(0);
		}

		public static Single GetNextRandomSingle()
		{
			Single randomValue = rand.NextSingle();

			Single zero = 0;
			Single multiplier = 1000;

			randomValue *= multiplier;

			Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;

			if( randomBoolean )
				randomValue = zero - randomValue;

			return randomValue;
		}


		public static Vector2 GetNextRandomVector2()
		{
			Single a = GetNextRandomSingle();
			Single b = GetNextRandomSingle();

			return new Vector2(a, b);
		}
		#region Utilities

		[Test]
		public void TestConstant_Zero ()
		{
			Single zero = 0;
			var v_zero = Vector2.Zero;

			Assert.That(v_zero.X, Is.EqualTo(zero));
			Assert.That(v_zero.Y, Is.EqualTo(zero));

			Assert.That(v_zero, Is.EqualTo(new Vector2(zero, zero)));
		}

		[Test]
		public void TestConstant_One ()
		{
			Single one = 1;
			var v_one = Vector2.One;

			Assert.That(v_one.X, Is.EqualTo(one));
			Assert.That(v_one.Y, Is.EqualTo(one));

			Assert.That(v_one, Is.EqualTo(new Vector2(one, one)));
		}

		[Test]
		public void TestConstant_UnitX ()
		{
			Single zero = 0;
			Single one = 1;

			var v_unit_x = Vector2.UnitX;

			Assert.That(v_unit_x.X, Is.EqualTo(one));
			Assert.That(v_unit_x.Y, Is.EqualTo(zero));

			Assert.That(v_unit_x, Is.EqualTo(new Vector2(one, zero)));
		}

		[Test]
		public void TestConstant_UnitY ()
		{
			Single zero = 0;
			Single one = 1;

			var v_unit_y = Vector2.UnitY;

			Assert.That(v_unit_y.X, Is.EqualTo(zero));
			Assert.That(v_unit_y.Y, Is.EqualTo(one));
			
			Assert.That(v_unit_y, Is.EqualTo(new Vector2(zero, one)));
		}

		#endregion
		#region Maths

		[Test]
		public void TestStaticFn_Distance_i ()
		{
			Vector2 a = new Vector2(0, 4);
			Vector2 b = new Vector2(3, 0);

			Single expected = 5;
			Single result; Vector2.Distance(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestStaticFn_Distance_ii ()
		{
			Vector2 a = new Vector2(0, -4);
			Vector2 b = new Vector2(3, 0);

			Single expected = 5;
			Single result; Vector2.Distance(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestStaticFn_Distance_iii ()
		{
			Vector2 a = new Vector2(0, -4);
			Vector2 b = new Vector2(-3, 0);

			Single expected = 5;
			Single result; Vector2.Distance(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestStaticFn_Distance_iv ()
		{
			Vector2 a = Vector2.Zero;

			Single expected = 0;

			Assert.That(a.Length(), Is.EqualTo(expected));
		}

		[Test]
		public void TestStaticFn_Distance_v ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				
				Single expected = RealMaths.Sqrt((a.X * a.X) + (a.Y * a.Y));

				Assert.That(a.Length(), Is.EqualTo(expected));
			}
		}

		[Test]
		public void TestStaticFn_DistanceSquared_i ()
		{
			Vector2 a = new Vector2(0, 4);
			Vector2 b = new Vector2(3, 0);

			Single expected = 25;
			Single result; Vector2.DistanceSquared(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestStaticFn_DistanceSquared_ii ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				Vector2 b = GetNextRandomVector2();
				Vector2 c = b - a;
				Single expected = (c.X * c.X) + (c.Y * c.Y);
				Single result; Vector2.DistanceSquared(ref a, ref b, out result);

				Assert.That(result, Is.EqualTo(expected));
			}
		}

		[Test]
		public void TestStaticFn_Dot_i ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				Vector2 b = GetNextRandomVector2();
				Single expected = (a.X * b.X) + (a.Y * b.Y);
				Single result; Vector2.Dot(ref a, ref b, out result);

				Assert.That(result, Is.EqualTo(expected));
			}
		}

		[Test]
		public void TestStaticFn_Dot_ii ()
		{
			Vector2 a = new Vector2(1, 0);
			Vector2 b = new Vector2(-1, 0);

			Single expected = -1;
			Single result; Vector2.Dot(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestStaticFn_Dot_iii ()
		{
			Vector2 a = new Vector2(100, 0);
			Vector2 b = new Vector2(10, 0);

			Single expected = 1;
			Single result; Vector2.Dot(ref a, ref b, out result);

			result = result / (10 * 100);

			Assert.That(result, Is.EqualTo(expected));
		}


		[Test]
		public void TestStaticFn_PerpDot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Perpendicular ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Normalise_i()
		{
			Vector2 a = Vector2.Zero;

			Vector2 b; Vector2.Normalise(ref a, out b);
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Normalise_ii()
		{
			Vector2 a = new Vector2(Single.MaxValue, Single.MaxValue);

			Vector2 b; Vector2.Normalise(ref a, out b);
		}


		[Test]
		public void TestStaticFn_Normalise_iii ()
		{
			Single epsilon; RealMaths.Epsilon(out epsilon);

			for( Int32 i = 0; i < 100; ++ i)
			{
				Vector2 a = GetNextRandomVector2();

				Vector2 b; Vector2.Normalise(ref a, out b);
				
				Single expected = 1;

				Single result = b.Length();

				Assert.That(result, Is.EqualTo(expected).Within(epsilon));
			}

		}

		[Test]
		public void TestStaticFn_Reflect ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformMatix44 ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformNormal ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformQuaternion ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Operators

		// Equality //--------------------------------------------------------//
		
		void TestEquality(Vector2 a, Vector2 b, Boolean expected )
		{
			// This test asserts the following:
			//   (a == b) == expected
			//   (b == a) == expected
			//   (a != b) == !expected
			//   (b != a) == !expected

			Boolean result_1a = (a == b);
			Boolean result_1b = (a.Equals(b));
			Boolean result_1c = (a.Equals((Object)b));
			
			Boolean result_2a = (b == a);
			Boolean result_2b = (b.Equals(a));
			Boolean result_2c = (b.Equals((Object)a));

			Boolean result_3a = (a != b);
			Boolean result_4a = (b != a);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
			Assert.That(result_1c, Is.EqualTo(expected));
			Assert.That(result_2a, Is.EqualTo(expected));
			Assert.That(result_2b, Is.EqualTo(expected));
			Assert.That(result_2c, Is.EqualTo(expected));
			Assert.That(result_3a, Is.EqualTo(!expected));
			Assert.That(result_4a, Is.EqualTo(!expected));
		}

		[Test]
		public void TestOperator_Equality_i ()
		{
			var a = new Vector2(44, -54);
			var b = new Vector2(44, -54);

			Boolean expected = true;

			this.TestEquality(a, b, expected);
		}


		[Test]
		public void TestOperator_Equality_ii ()
		{
			var a = new Vector2(44, 54);
			var b = new Vector2(44, -54);

			Boolean expected = false;

			this.TestEquality(a, b, expected);
		}

		[Test]
		public void TestOperator_Equality_iii ()
		{
			var a = GetNextRandomVector2();

			this.TestEquality(a, a, true);
		}


		// Addition //--------------------------------------------------------//

		void TestAddition(Vector2 a, Vector2 b, Vector2 expected )
		{
			// This test asserts the following:
			//   a + b == expected
			//   b + a == expected

			var result_1a = a + b;
			var result_2a = b + a;

			Vector2 result_1b; Vector2.Add(ref a, ref b, out result_1b);
			Vector2 result_2b; Vector2.Add(ref b, ref a, out result_2b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_2a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
			Assert.That(result_2b, Is.EqualTo(expected));
		}

		/// <summary>
		/// Assert that, for a known example, simple vector addition yields the 
		/// correct result.
		/// </summary>
		[Test]
		public void TestOperator_Addition_i ()
		{
			var a = new Vector2(3, -6);
			var b = new Vector2(-6, 12);

			var expected = new Vector2(-3, 6);

			this.TestAddition(a, b, expected);
		}

		/// <summary>
		/// Assert that, for a known example, vector addition work correctly 
		/// when one zero vector is involved.
		/// </summary>
		[Test]
		public void TestOperator_Addition_ii ()
		{
			var a = new Vector2(-2313, 88);

			var expected = a;

			this.TestAddition(a, Vector2.Zero, expected);
		}

		/// <summary>
		/// Assert that two zero vectors correctly add to yield zero.
		/// </summary>
		[Test]
		public void TestOperator_Addition_iii ()
		{
			this.TestAddition(Vector2.Zero, Vector2.Zero, Vector2.Zero);
		}

		[Test]
		public void TestOperator_Addition_iv ()
		{var a = GetNextRandomVector2();
			
			var b = GetNextRandomVector2();

			var expected = new Vector2(a.X + b.X, a.Y + b.Y);

			this.TestAddition(a, b, expected);
		}


		// Subtraction //-----------------------------------------------------//
		
		void TestSubtraction(Vector2 a, Vector2 b, Vector2 expected )
		{
			// This test asserts the following:
			//   a - b == expected
			//   b - a == -expected

			var result_1a = a - b;
			var result_2a = b - a;

			Vector2 result_1b; Vector2.Subtract(ref a, ref b, out result_1b);
			Vector2 result_2b; Vector2.Subtract(ref b, ref a, out result_2b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_2a, Is.EqualTo(-expected));
			Assert.That(result_1b, Is.EqualTo(expected));
			Assert.That(result_2b, Is.EqualTo(-expected));
		}

		/// <summary>
		/// Assert that, for a known example, simple vector subtraction yields 
		/// the correct result.
		/// <summary>
		[Test]
		public void TestOperator_Subtraction_i ()
		{
			var a = new Vector2(12, -4);
			var b = new Vector2(15, 11);

			var expected = new Vector2(-3, -15);

			this.TestSubtraction(a, b, expected);
		}

		/// <summary>
		/// Assert that, for a known example, vector subtraction work correctly 
		/// when one zero vector is involved.
		/// <summary>
		[Test]
		public void TestOperator_Subtraction_ii ()
		{
			var a = new Vector2(-423, 342);

			var expected = a;

			this.TestSubtraction(a, Vector2.Zero, expected);
		}

		/// <summary>
		/// Assert that two zero vectors correctly subtract to yield zero.
		/// <summary>
		[Test]
		public void TestOperator_Subtraction_iii ()
		{
			this.TestSubtraction(Vector2.Zero, Vector2.Zero, Vector2.Zero);
		}

		[Test]
		public void TestOperator_Subtraction_iv ()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();

			var expected = new Vector2(a.X - b.X, a.Y - b.Y);

			this.TestSubtraction(a, b, expected);
		}


		// Negation //--------------------------------------------------------//
		
		void TestNegation(Vector2 a, Vector2 expected )
		{
			// This test asserts the following:
			//   -a == expected

			var result_1a = -a;

			Vector2 result_1b; Vector2.Negate(ref a, out result_1b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
		}

		[Test]
		public void TestOperator_Negation_i ()
		{
			Single r = 3432;
			Single s = -6218;
			Single t = -3432;
			Single u = 6218;

			var a = new Vector2(r, s);
			var c = new Vector2(t, u);

			this.TestNegation(a, c);
		}

		[Test]
		public void TestOperator_Negation_ii ()
		{
			Single r = 3432;
			Single s = -6218;
			Single t = -3432;
			Single u = 6218;

			var b = new Vector2(u, t);
			var d = new Vector2(s, r);

			this.TestNegation(b, d);
		}

		[Test]
		public void TestOperator_Negation_iii ()
		{
			Single t = -3432;
			Single u = 6218;

			var c = new Vector2(t, u);

			this.TestNegation(c, Vector2.Zero - c);
		}

		[Test]
		public void TestOperator_Negation_iv ()
		{
			Single r = 3432;
			Single s = -6218;

			var d = new Vector2(s, r);

			this.TestNegation(d, Vector2.Zero - d);
		}

		[Test]
		public void TestOperator_Negation_v ()
		{
			this.TestNegation(Vector2.Zero, Vector2.Zero);
		}

		[Test]
		public void TestOperator_Negation_vi ()
		{
			var a = GetNextRandomVector2();
			this.TestNegation(a, Vector2.Zero - a);
		}


		// Multiplication //--------------------------------------------------//
		
		void TestMultiplication(Vector2 a, Vector2 b, Vector2 expected )
		{
			// This test asserts the following:
			//   a * b == expected
			//   b * a == expected

			var result_1a = a * b;
			var result_2a = b * a;

			Vector2 result_1b; Vector2.Multiply(ref a, ref b, out result_1b);
			Vector2 result_2b; Vector2.Multiply(ref b, ref a, out result_2b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_2a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
			Assert.That(result_2b, Is.EqualTo(expected));
		}

		[Test]
		public void TestOperator_Multiplication_i ()
		{
			Single r = 18;
			Single s = -54;

			Single x = 3;
			Single y = 6;
			Single z = -9;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);

			var c = new Vector2(r, s);

			this.TestMultiplication(a, b, c);
		}

		[Test]
		public void TestOperator_Multiplication_ii ()
		{
			Single r = 18;
			Single s = -54;

			Single x = 3;
			Single y = 6;
			Single z = -9;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);

			var c = new Vector2(r, s);

			// Test multiplication with the (*) operator
			var test1_op = a * b;
			var test2_op = b * a;

			Assert.That(test1_op, Is.EqualTo(c));
			Assert.That(test2_op, Is.EqualTo(c));

			// Test multiplication with static Multiply function
			Vector2 test1_st; Vector2.Multiply(ref a, ref b, out test1_st);
			Vector2 test2_st; Vector2.Multiply(ref b, ref a, out test2_st);

			Assert.That(test1_st, Is.EqualTo(c));
			Assert.That(test2_st, Is.EqualTo(c));
		}

		[Test]
		public void TestOperator_Multiplication_iii ()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();

			var c = new Vector2(a.X * b.X, a.Y * b.Y);

			this.TestMultiplication(a, b, c);
		}


		// Division //--------------------------------------------------------//
		
		void TestDivision(Vector2 a, Vector2 b, Vector2 expected )
		{
			// This test asserts the following:
			//   a / b == expected

			var result_1a = a / b;

			Vector2 result_1b; Vector2.Divide(ref a, ref b, out result_1b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
		}

		[Test]
		public void TestOperator_Division_i ()
		{
			Single r = 10;
			Single s = -40;

			Single x = 2000;
			Single y = 200;
			Single z = -5;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);

			var c = new Vector2(r, s);

			this.TestDivision(a, b, c);
		}

		[Test]
		public void TestOperator_Division_ii ()
		{
			Single t = ((Single) 1 ) / ((Single) 10);
			Single u = ((Single) (-1) ) / ((Single) 40 );

			Single x = 2000;
			Single y = 200;
			Single z = -5;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);

			var d = new Vector2(t, u);

			this.TestDivision(b, a, d);
		}

		[Test]
		public void TestOperator_Division_iii ()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();

			var c = new Vector2(a.X / b.X, a.Y / b.Y);

			this.TestDivision(a, b, c);
		}

		#endregion
		#region Splines

		[Test]
		public void TestStaticFn_Barycentric ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_SmoothStep ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_CatmullRom ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Hermite ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
				#region Utilities

		[Test]
		public void TestStaticFn_Min ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				Vector2 b = a * 2;

				Vector2 result; Vector2.Min (ref a, ref b, out result);

				Assert.That(result.X, Is.EqualTo(a.X < b.X ? a.X : b.X ));
				Assert.That(result.Y, Is.EqualTo(a.Y < b.Y ? a.Y : b.Y ));
			}
		}

		[Test]
		public void TestStaticFn_Max ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				Vector2 b = GetNextRandomVector2();

				Vector2 result; Vector2.Max (ref a, ref b, out result);

				Assert.That(result.X, Is.EqualTo(a.X > b.X ? a.X : b.X ));
				Assert.That(result.Y, Is.EqualTo(a.Y > b.Y ? a.Y : b.Y ));
			}
		}

		[Test]
		public void TestStaticFn_Clamp_i ()
		{
			Vector2 min = new Vector2(-30, 1);
			Vector2 max = new Vector2(32, 130);

			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();

				Vector2 result; Vector2.Clamp (ref a, ref min, ref max, out result);

				Assert.That(result.X, Is.LessThanOrEqualTo(max.X));
				Assert.That(result.Y, Is.LessThanOrEqualTo(max.Y));
				Assert.That(result.X, Is.GreaterThanOrEqualTo(min.X));
				Assert.That(result.Y, Is.GreaterThanOrEqualTo(min.Y));
			}
		}

		[Test]
		public void TestStaticFn_Clamp_ii ()
		{
			Vector2 min = new Vector2(-30, 1);
			Vector2 max = new Vector2(32, 130);

			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = new Vector2(-1, 13);

				Vector2 expected = a;

				Vector2 result; Vector2.Clamp (ref a, ref min, ref max, out result);

				Assert.That(result.X, Is.LessThanOrEqualTo(max.X));
				Assert.That(result.Y, Is.LessThanOrEqualTo(max.Y));
				Assert.That(result.X, Is.GreaterThanOrEqualTo(min.X));
				Assert.That(result.Y, Is.GreaterThanOrEqualTo(min.Y));

				Assert.That(a, Is.EqualTo(expected));
			}
		}

		[Test]
		public void TestStaticFn_Lerp_i ()
		{
			Single epsilon; RealMaths.Epsilon(out epsilon);

			for(Int32 j = 0; j < 100; ++j)
			{
				Single delta = j;

				delta = delta / 100;

				for(Int32 i = 0; i < 100; ++i)
				{
					Vector2 a = GetNextRandomVector2();
					Vector2 b = GetNextRandomVector2();

					Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);

					Vector2 expected = a + ( ( b - a ) * delta );

					Assert.That(result, Is.EqualTo(expected).Within(epsilon));
				}
			}
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Lerp_ii()
		{
			Single delta = 2;
			Vector2 a = GetNextRandomVector2();
			Vector2 b = GetNextRandomVector2();
			Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Lerp_iii()
		{
			Single delta = -1;
			Vector2 a = GetNextRandomVector2();
			Vector2 b = GetNextRandomVector2();
			Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Lerp_iv()
		{
			Single delta; RealMaths.Half(out delta);

			delta = -delta;

			Vector2 a = GetNextRandomVector2();
			Vector2 b = GetNextRandomVector2();
			Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);
		}


		#endregion

	}	[TestFixture]
	public class Vector3Tests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_Set ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_Length ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_LengthSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_NormaliseMemberFunction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_IsUnit ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#region Utilities

		[Test]
		public void TestConstant_Zero ()
		{
			Single zero = 0;
			var v_zero = Vector3.Zero;

			Assert.That(v_zero.X, Is.EqualTo(zero));
			Assert.That(v_zero.Y, Is.EqualTo(zero));
			Assert.That(v_zero.Z, Is.EqualTo(zero));
			
			Assert.That(v_zero, Is.EqualTo(new Vector3(zero, zero, zero)));
		}

		[Test]
		public void TestConstant_One ()
		{
			Single one = 1;
			var v_one = Vector3.One;

			Assert.That(v_one.X, Is.EqualTo(one));
			Assert.That(v_one.Y, Is.EqualTo(one));
			Assert.That(v_one.Z, Is.EqualTo(one));

			Assert.That(v_one, Is.EqualTo(new Vector3(one, one, one)));
		}

		[Test]
		public void TestConstant_UnitX ()
		{
			Single zero = 0;
			Single one = 1;

			var v_unit_x = Vector3.UnitX;

			Assert.That(v_unit_x.X, Is.EqualTo(one));
			Assert.That(v_unit_x.Y, Is.EqualTo(zero));
			Assert.That(v_unit_x.Z, Is.EqualTo(zero));

			Assert.That(v_unit_x, Is.EqualTo(new Vector3(one, zero, zero)));
		}

		[Test]
		public void TestConstant_UnitY ()
		{
			Single zero = 0;
			Single one = 1;

			var v_unit_y = Vector3.UnitY;

			Assert.That(v_unit_y.X, Is.EqualTo(zero));
			Assert.That(v_unit_y.Y, Is.EqualTo(one));
			Assert.That(v_unit_y.Z, Is.EqualTo(zero));

			Assert.That(v_unit_y, Is.EqualTo(new Vector3(zero, one, zero)));
		}

		[Test]
		public void TestConstant_UnitZ ()
		{
			Single zero = 0;
			Single one = 1;

			var v_unit_z = Vector3.UnitZ;

			Assert.That(v_unit_z.X, Is.EqualTo(zero));
			Assert.That(v_unit_z.Y, Is.EqualTo(zero));
			Assert.That(v_unit_z.Z, Is.EqualTo(one));

			Assert.That(v_unit_z, Is.EqualTo(new Vector3(zero, zero, one)));
		}

		#endregion
		#region Maths

		[Test]
		public void TestStaticFn_Distance ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_DistanceSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Dot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_PerpDot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Perpendicular ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Normalise ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Reflect ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformMatix44 ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformNormal ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformQuaternion ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Utilities

		[Test]
		public void TestOperator_Addition ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Subtraction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Negation ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Multiplication ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Division ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Splines

		[Test]
		public void TestStaticFn_Barycentric ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_SmoothStep ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_CatmullRom ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Hermite ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
				#region Utilities

		[Test]
		public void TestStaticFn_Min ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Max ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Clamp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Lerp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion

	}	[TestFixture]
	public class Vector4Tests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_Set ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_Length ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_LengthSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_NormaliseMemberFunction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_IsUnit ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#region Utilities

		[Test]
		public void TestConstant_Zero ()
		{
			Single zero = 0;
			var v_zero = Vector4.Zero;

			Assert.That(v_zero.W, Is.EqualTo(zero));
			Assert.That(v_zero.X, Is.EqualTo(zero));
			Assert.That(v_zero.Y, Is.EqualTo(zero));
			Assert.That(v_zero.Z, Is.EqualTo(zero));

			Assert.That(v_zero, Is.EqualTo(new Vector4(zero, zero, zero, zero)));
		}

		[Test]
		public void TestConstant_One ()
		{
			Single one = 1;
			var v_one = Vector4.One;

			Assert.That(v_one.W, Is.EqualTo(one));
			Assert.That(v_one.X, Is.EqualTo(one));
			Assert.That(v_one.Y, Is.EqualTo(one));
			Assert.That(v_one.Z, Is.EqualTo(one));

			Assert.That(v_one, Is.EqualTo(new Vector4(one, one, one, one)));
		}

		[Test]
		public void TestConstant_UnitX ()
		{
			Single zero = 0;
			Single one = 1;

			var v_unit_x = Vector4.UnitX;

			Assert.That(v_unit_x.X, Is.EqualTo(one));
			Assert.That(v_unit_x.Y, Is.EqualTo(zero));
			Assert.That(v_unit_x.Z, Is.EqualTo(zero));
			Assert.That(v_unit_x.W, Is.EqualTo(zero));

			Assert.That(v_unit_x, Is.EqualTo(new Vector4(one, zero, zero, zero)));
		}

		[Test]
		public void TestConstant_UnitY ()
		{
			Single zero = 0;
			Single one = 1;

			var v_unit_y = Vector4.UnitY;

			Assert.That(v_unit_y.X, Is.EqualTo(zero));
			Assert.That(v_unit_y.Y, Is.EqualTo(one));
			Assert.That(v_unit_y.Z, Is.EqualTo(zero));
			Assert.That(v_unit_y.W, Is.EqualTo(zero));

			Assert.That(v_unit_y, Is.EqualTo(new Vector4(zero, one, zero, zero)));
		}

		[Test]
		public void TestConstant_UnitZ ()
		{
			Single zero = 0;
			Single one = 1;

			var v_unit_z = Vector4.UnitZ;

			Assert.That(v_unit_z.X, Is.EqualTo(zero));
			Assert.That(v_unit_z.Y, Is.EqualTo(zero));
			Assert.That(v_unit_z.Z, Is.EqualTo(one));
			Assert.That(v_unit_z.W, Is.EqualTo(zero));
			
			Assert.That(v_unit_z, Is.EqualTo(new Vector4(zero, zero, one, zero)));
		}

		[Test]
		public void TestConstant_UnitW ()
		{
			Single zero = 0;
			Single one = 1;

			var v_unit_w = Vector4.UnitW;

			Assert.That(v_unit_w.X, Is.EqualTo(zero));
			Assert.That(v_unit_w.Y, Is.EqualTo(zero));
			Assert.That(v_unit_w.Z, Is.EqualTo(zero));
			Assert.That(v_unit_w.W, Is.EqualTo(one));


			Assert.That(v_unit_w, Is.EqualTo(new Vector4(zero, zero, zero, one)));
		}

		#endregion
		#region Maths

		[Test]
		public void TestStaticFn_Distance ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_DistanceSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Dot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_PerpDot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Perpendicular ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Normalise ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Reflect ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformMatix44 ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformNormal ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformQuaternion ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Utilities

		[Test]
		public void TestOperator_Addition ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Subtraction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Negation ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Multiplication ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Division ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Splines

		[Test]
		public void TestStaticFn_Barycentric ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_SmoothStep ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_CatmullRom ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Hermite ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
				#region Utilities

		[Test]
		public void TestStaticFn_Min ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Max ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Clamp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Lerp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion

	}}

namespace Sungiant.Abacus.DoublePrecision.Tests
{

	[TestFixture]
	public class BoundingBoxTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class BoundingFrustumTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class BoundingSphereTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class Matrix44Tests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
		
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
	}	[TestFixture]
	public class PlaneTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class QuadTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class QuaternionTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class RayTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class TriangleTests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}
	}
		[TestFixture]
	public class Vector2Tests
	{
		[Test]
		public void Test_Constructors ()
		{
			// Test default values
			Vector2 a = new Vector2();
			Assert.That(a, Is.EqualTo(Vector2.Zero));

			// Test Vector2( n ) where n is Double
			Double u = -189;
			Double v = 429;
			Vector2 b1 = new Vector2(u);
			Assert.That(b1.X, Is.EqualTo(u));
			Assert.That(b1.Y, Is.EqualTo(u));
			Vector2 b2 = new Vector2(v);
			Assert.That(b2.X, Is.EqualTo(v));
			Assert.That(b2.Y, Is.EqualTo(v));

			// Test Vector2( x, y ) where x, y are Double
			Vector2 c = new Vector2(u, v);
			Assert.That(c.X, Is.EqualTo(u));
			Assert.That(c.Y, Is.EqualTo(v));

			// Test Vector2( x, y ) where x, y are Int32
			Int32 q = 12334;
			Int32 r = -2145;
			Double s = q;
			Double t = r;
			Vector2 d = new Vector2(q, r);
			Assert.That(d.X, Is.EqualTo(s));
			Assert.That(d.Y, Is.EqualTo(t));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Vector2 a = new Vector2(42, -17);

			String result = a.ToString();

			String expected = "{X:42 Y:-17}";

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			var hs1 = new System.Collections.Generic.HashSet<Vector2>();
			var hs2 = new System.Collections.Generic.HashSet<Int32>();

			for(Int32 i = 0; i < 10000; ++i)
			{
				var a = GetNextRandomVector2();

				hs1.Add(a);
				hs2.Add(a.GetHashCode());
			}

			Assert.That(hs1.Count, Is.EqualTo(hs2.Count).Within(10));
		}

		[Test]
		public void TestMemberFn_Set ()
		{
			Vector2 a = Vector2.Zero;

			a.Set(14, -19);

			Vector2 expected = new Vector2(14, -19);

			Assert.That(a, Is.EqualTo(expected));
		}

		[Test]
		public void TestMemberFn_Length ()
		{
			Vector2 a = new Vector2(30, -40);

			Double expected = 50;

			Double result = a.Length();

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestMemberFn_LengthSquared ()
		{
			Vector2 a = new Vector2(30, -40);

			Double expected = 2500;

			Double result = a.LengthSquared();

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestMemberFn_IsUnit_i ()
		{
			Assert.That(new Vector2(1, 0).IsUnit(), Is.EqualTo(true));
			Assert.That(new Vector2(-1, 0).IsUnit(), Is.EqualTo(true));
			Assert.That(new Vector2(1, 1).IsUnit(), Is.EqualTo(false));
			Assert.That(new Vector2(0, 0).IsUnit(), Is.EqualTo(false));
			Assert.That(new Vector2(0, -1).IsUnit(), Is.EqualTo(true));
			Assert.That(new Vector2(0, 1).IsUnit(), Is.EqualTo(true));
		}

		[Test]
		public void TestMemberFn_IsUnit_ii ()
		{
			for( Int32 i = 0; i < 100; ++ i)
			{
				Vector2 a = GetNextRandomVector2();

				Vector2 b; Vector2.Normalise(ref a, out b);

				Assert.That(b.IsUnit(), Is.EqualTo(true));
			}
		}

		[Test]
		public void TestMemberFn_IsUnit_iii ()
		{
			Double piOver2; RealMaths.PiOver2(out piOver2);

			for( Int32 i = 0; i <= 90; ++ i)
			{
				Double theta = piOver2 / 90 * i;

				Double opposite = RealMaths.Sin(theta);
				Double adjacent = RealMaths.Cos(theta);				

				Assert.That(new Vector2( opposite,  adjacent).IsUnit(), Is.EqualTo(true));
				Assert.That(new Vector2( opposite, -adjacent).IsUnit(), Is.EqualTo(true));
				Assert.That(new Vector2(-opposite,  adjacent).IsUnit(), Is.EqualTo(true));
				Assert.That(new Vector2(-opposite, -adjacent).IsUnit(), Is.EqualTo(true));
			}
		}

		static System.Random rand;

		static Vector2Tests()
		{
			rand = new System.Random(0);
		}

		public static Double GetNextRandomDouble()
		{
			Double randomValue = rand.NextDouble();

			Double zero = 0;
			Double multiplier = 1000;

			randomValue *= multiplier;

			Boolean randomBoolean = (rand.Next(0, 1) == 0) ? true : false;

			if( randomBoolean )
				randomValue = zero - randomValue;

			return randomValue;
		}


		public static Vector2 GetNextRandomVector2()
		{
			Double a = GetNextRandomDouble();
			Double b = GetNextRandomDouble();

			return new Vector2(a, b);
		}
		#region Utilities

		[Test]
		public void TestConstant_Zero ()
		{
			Double zero = 0;
			var v_zero = Vector2.Zero;

			Assert.That(v_zero.X, Is.EqualTo(zero));
			Assert.That(v_zero.Y, Is.EqualTo(zero));

			Assert.That(v_zero, Is.EqualTo(new Vector2(zero, zero)));
		}

		[Test]
		public void TestConstant_One ()
		{
			Double one = 1;
			var v_one = Vector2.One;

			Assert.That(v_one.X, Is.EqualTo(one));
			Assert.That(v_one.Y, Is.EqualTo(one));

			Assert.That(v_one, Is.EqualTo(new Vector2(one, one)));
		}

		[Test]
		public void TestConstant_UnitX ()
		{
			Double zero = 0;
			Double one = 1;

			var v_unit_x = Vector2.UnitX;

			Assert.That(v_unit_x.X, Is.EqualTo(one));
			Assert.That(v_unit_x.Y, Is.EqualTo(zero));

			Assert.That(v_unit_x, Is.EqualTo(new Vector2(one, zero)));
		}

		[Test]
		public void TestConstant_UnitY ()
		{
			Double zero = 0;
			Double one = 1;

			var v_unit_y = Vector2.UnitY;

			Assert.That(v_unit_y.X, Is.EqualTo(zero));
			Assert.That(v_unit_y.Y, Is.EqualTo(one));
			
			Assert.That(v_unit_y, Is.EqualTo(new Vector2(zero, one)));
		}

		#endregion
		#region Maths

		[Test]
		public void TestStaticFn_Distance_i ()
		{
			Vector2 a = new Vector2(0, 4);
			Vector2 b = new Vector2(3, 0);

			Double expected = 5;
			Double result; Vector2.Distance(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestStaticFn_Distance_ii ()
		{
			Vector2 a = new Vector2(0, -4);
			Vector2 b = new Vector2(3, 0);

			Double expected = 5;
			Double result; Vector2.Distance(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestStaticFn_Distance_iii ()
		{
			Vector2 a = new Vector2(0, -4);
			Vector2 b = new Vector2(-3, 0);

			Double expected = 5;
			Double result; Vector2.Distance(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestStaticFn_Distance_iv ()
		{
			Vector2 a = Vector2.Zero;

			Double expected = 0;

			Assert.That(a.Length(), Is.EqualTo(expected));
		}

		[Test]
		public void TestStaticFn_Distance_v ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				
				Double expected = RealMaths.Sqrt((a.X * a.X) + (a.Y * a.Y));

				Assert.That(a.Length(), Is.EqualTo(expected));
			}
		}

		[Test]
		public void TestStaticFn_DistanceSquared_i ()
		{
			Vector2 a = new Vector2(0, 4);
			Vector2 b = new Vector2(3, 0);

			Double expected = 25;
			Double result; Vector2.DistanceSquared(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestStaticFn_DistanceSquared_ii ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				Vector2 b = GetNextRandomVector2();
				Vector2 c = b - a;
				Double expected = (c.X * c.X) + (c.Y * c.Y);
				Double result; Vector2.DistanceSquared(ref a, ref b, out result);

				Assert.That(result, Is.EqualTo(expected));
			}
		}

		[Test]
		public void TestStaticFn_Dot_i ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				Vector2 b = GetNextRandomVector2();
				Double expected = (a.X * b.X) + (a.Y * b.Y);
				Double result; Vector2.Dot(ref a, ref b, out result);

				Assert.That(result, Is.EqualTo(expected));
			}
		}

		[Test]
		public void TestStaticFn_Dot_ii ()
		{
			Vector2 a = new Vector2(1, 0);
			Vector2 b = new Vector2(-1, 0);

			Double expected = -1;
			Double result; Vector2.Dot(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void TestStaticFn_Dot_iii ()
		{
			Vector2 a = new Vector2(100, 0);
			Vector2 b = new Vector2(10, 0);

			Double expected = 1;
			Double result; Vector2.Dot(ref a, ref b, out result);

			result = result / (10 * 100);

			Assert.That(result, Is.EqualTo(expected));
		}


		[Test]
		public void TestStaticFn_PerpDot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Perpendicular ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Normalise_i()
		{
			Vector2 a = Vector2.Zero;

			Vector2 b; Vector2.Normalise(ref a, out b);
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Normalise_ii()
		{
			Vector2 a = new Vector2(Double.MaxValue, Double.MaxValue);

			Vector2 b; Vector2.Normalise(ref a, out b);
		}


		[Test]
		public void TestStaticFn_Normalise_iii ()
		{
			Double epsilon; RealMaths.Epsilon(out epsilon);

			for( Int32 i = 0; i < 100; ++ i)
			{
				Vector2 a = GetNextRandomVector2();

				Vector2 b; Vector2.Normalise(ref a, out b);
				
				Double expected = 1;

				Double result = b.Length();

				Assert.That(result, Is.EqualTo(expected).Within(epsilon));
			}

		}

		[Test]
		public void TestStaticFn_Reflect ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformMatix44 ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformNormal ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformQuaternion ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Operators

		// Equality //--------------------------------------------------------//
		
		void TestEquality(Vector2 a, Vector2 b, Boolean expected )
		{
			// This test asserts the following:
			//   (a == b) == expected
			//   (b == a) == expected
			//   (a != b) == !expected
			//   (b != a) == !expected

			Boolean result_1a = (a == b);
			Boolean result_1b = (a.Equals(b));
			Boolean result_1c = (a.Equals((Object)b));
			
			Boolean result_2a = (b == a);
			Boolean result_2b = (b.Equals(a));
			Boolean result_2c = (b.Equals((Object)a));

			Boolean result_3a = (a != b);
			Boolean result_4a = (b != a);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
			Assert.That(result_1c, Is.EqualTo(expected));
			Assert.That(result_2a, Is.EqualTo(expected));
			Assert.That(result_2b, Is.EqualTo(expected));
			Assert.That(result_2c, Is.EqualTo(expected));
			Assert.That(result_3a, Is.EqualTo(!expected));
			Assert.That(result_4a, Is.EqualTo(!expected));
		}

		[Test]
		public void TestOperator_Equality_i ()
		{
			var a = new Vector2(44, -54);
			var b = new Vector2(44, -54);

			Boolean expected = true;

			this.TestEquality(a, b, expected);
		}


		[Test]
		public void TestOperator_Equality_ii ()
		{
			var a = new Vector2(44, 54);
			var b = new Vector2(44, -54);

			Boolean expected = false;

			this.TestEquality(a, b, expected);
		}

		[Test]
		public void TestOperator_Equality_iii ()
		{
			var a = GetNextRandomVector2();

			this.TestEquality(a, a, true);
		}


		// Addition //--------------------------------------------------------//

		void TestAddition(Vector2 a, Vector2 b, Vector2 expected )
		{
			// This test asserts the following:
			//   a + b == expected
			//   b + a == expected

			var result_1a = a + b;
			var result_2a = b + a;

			Vector2 result_1b; Vector2.Add(ref a, ref b, out result_1b);
			Vector2 result_2b; Vector2.Add(ref b, ref a, out result_2b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_2a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
			Assert.That(result_2b, Is.EqualTo(expected));
		}

		/// <summary>
		/// Assert that, for a known example, simple vector addition yields the 
		/// correct result.
		/// </summary>
		[Test]
		public void TestOperator_Addition_i ()
		{
			var a = new Vector2(3, -6);
			var b = new Vector2(-6, 12);

			var expected = new Vector2(-3, 6);

			this.TestAddition(a, b, expected);
		}

		/// <summary>
		/// Assert that, for a known example, vector addition work correctly 
		/// when one zero vector is involved.
		/// </summary>
		[Test]
		public void TestOperator_Addition_ii ()
		{
			var a = new Vector2(-2313, 88);

			var expected = a;

			this.TestAddition(a, Vector2.Zero, expected);
		}

		/// <summary>
		/// Assert that two zero vectors correctly add to yield zero.
		/// </summary>
		[Test]
		public void TestOperator_Addition_iii ()
		{
			this.TestAddition(Vector2.Zero, Vector2.Zero, Vector2.Zero);
		}

		[Test]
		public void TestOperator_Addition_iv ()
		{var a = GetNextRandomVector2();
			
			var b = GetNextRandomVector2();

			var expected = new Vector2(a.X + b.X, a.Y + b.Y);

			this.TestAddition(a, b, expected);
		}


		// Subtraction //-----------------------------------------------------//
		
		void TestSubtraction(Vector2 a, Vector2 b, Vector2 expected )
		{
			// This test asserts the following:
			//   a - b == expected
			//   b - a == -expected

			var result_1a = a - b;
			var result_2a = b - a;

			Vector2 result_1b; Vector2.Subtract(ref a, ref b, out result_1b);
			Vector2 result_2b; Vector2.Subtract(ref b, ref a, out result_2b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_2a, Is.EqualTo(-expected));
			Assert.That(result_1b, Is.EqualTo(expected));
			Assert.That(result_2b, Is.EqualTo(-expected));
		}

		/// <summary>
		/// Assert that, for a known example, simple vector subtraction yields 
		/// the correct result.
		/// <summary>
		[Test]
		public void TestOperator_Subtraction_i ()
		{
			var a = new Vector2(12, -4);
			var b = new Vector2(15, 11);

			var expected = new Vector2(-3, -15);

			this.TestSubtraction(a, b, expected);
		}

		/// <summary>
		/// Assert that, for a known example, vector subtraction work correctly 
		/// when one zero vector is involved.
		/// <summary>
		[Test]
		public void TestOperator_Subtraction_ii ()
		{
			var a = new Vector2(-423, 342);

			var expected = a;

			this.TestSubtraction(a, Vector2.Zero, expected);
		}

		/// <summary>
		/// Assert that two zero vectors correctly subtract to yield zero.
		/// <summary>
		[Test]
		public void TestOperator_Subtraction_iii ()
		{
			this.TestSubtraction(Vector2.Zero, Vector2.Zero, Vector2.Zero);
		}

		[Test]
		public void TestOperator_Subtraction_iv ()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();

			var expected = new Vector2(a.X - b.X, a.Y - b.Y);

			this.TestSubtraction(a, b, expected);
		}


		// Negation //--------------------------------------------------------//
		
		void TestNegation(Vector2 a, Vector2 expected )
		{
			// This test asserts the following:
			//   -a == expected

			var result_1a = -a;

			Vector2 result_1b; Vector2.Negate(ref a, out result_1b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
		}

		[Test]
		public void TestOperator_Negation_i ()
		{
			Double r = 3432;
			Double s = -6218;
			Double t = -3432;
			Double u = 6218;

			var a = new Vector2(r, s);
			var c = new Vector2(t, u);

			this.TestNegation(a, c);
		}

		[Test]
		public void TestOperator_Negation_ii ()
		{
			Double r = 3432;
			Double s = -6218;
			Double t = -3432;
			Double u = 6218;

			var b = new Vector2(u, t);
			var d = new Vector2(s, r);

			this.TestNegation(b, d);
		}

		[Test]
		public void TestOperator_Negation_iii ()
		{
			Double t = -3432;
			Double u = 6218;

			var c = new Vector2(t, u);

			this.TestNegation(c, Vector2.Zero - c);
		}

		[Test]
		public void TestOperator_Negation_iv ()
		{
			Double r = 3432;
			Double s = -6218;

			var d = new Vector2(s, r);

			this.TestNegation(d, Vector2.Zero - d);
		}

		[Test]
		public void TestOperator_Negation_v ()
		{
			this.TestNegation(Vector2.Zero, Vector2.Zero);
		}

		[Test]
		public void TestOperator_Negation_vi ()
		{
			var a = GetNextRandomVector2();
			this.TestNegation(a, Vector2.Zero - a);
		}


		// Multiplication //--------------------------------------------------//
		
		void TestMultiplication(Vector2 a, Vector2 b, Vector2 expected )
		{
			// This test asserts the following:
			//   a * b == expected
			//   b * a == expected

			var result_1a = a * b;
			var result_2a = b * a;

			Vector2 result_1b; Vector2.Multiply(ref a, ref b, out result_1b);
			Vector2 result_2b; Vector2.Multiply(ref b, ref a, out result_2b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_2a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
			Assert.That(result_2b, Is.EqualTo(expected));
		}

		[Test]
		public void TestOperator_Multiplication_i ()
		{
			Double r = 18;
			Double s = -54;

			Double x = 3;
			Double y = 6;
			Double z = -9;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);

			var c = new Vector2(r, s);

			this.TestMultiplication(a, b, c);
		}

		[Test]
		public void TestOperator_Multiplication_ii ()
		{
			Double r = 18;
			Double s = -54;

			Double x = 3;
			Double y = 6;
			Double z = -9;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);

			var c = new Vector2(r, s);

			// Test multiplication with the (*) operator
			var test1_op = a * b;
			var test2_op = b * a;

			Assert.That(test1_op, Is.EqualTo(c));
			Assert.That(test2_op, Is.EqualTo(c));

			// Test multiplication with static Multiply function
			Vector2 test1_st; Vector2.Multiply(ref a, ref b, out test1_st);
			Vector2 test2_st; Vector2.Multiply(ref b, ref a, out test2_st);

			Assert.That(test1_st, Is.EqualTo(c));
			Assert.That(test2_st, Is.EqualTo(c));
		}

		[Test]
		public void TestOperator_Multiplication_iii ()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();

			var c = new Vector2(a.X * b.X, a.Y * b.Y);

			this.TestMultiplication(a, b, c);
		}


		// Division //--------------------------------------------------------//
		
		void TestDivision(Vector2 a, Vector2 b, Vector2 expected )
		{
			// This test asserts the following:
			//   a / b == expected

			var result_1a = a / b;

			Vector2 result_1b; Vector2.Divide(ref a, ref b, out result_1b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
		}

		[Test]
		public void TestOperator_Division_i ()
		{
			Double r = 10;
			Double s = -40;

			Double x = 2000;
			Double y = 200;
			Double z = -5;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);

			var c = new Vector2(r, s);

			this.TestDivision(a, b, c);
		}

		[Test]
		public void TestOperator_Division_ii ()
		{
			Double t = ((Double) 1 ) / ((Double) 10);
			Double u = ((Double) (-1) ) / ((Double) 40 );

			Double x = 2000;
			Double y = 200;
			Double z = -5;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);

			var d = new Vector2(t, u);

			this.TestDivision(b, a, d);
		}

		[Test]
		public void TestOperator_Division_iii ()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();

			var c = new Vector2(a.X / b.X, a.Y / b.Y);

			this.TestDivision(a, b, c);
		}

		#endregion
		#region Splines

		[Test]
		public void TestStaticFn_Barycentric ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_SmoothStep ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_CatmullRom ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Hermite ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
				#region Utilities

		[Test]
		public void TestStaticFn_Min ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				Vector2 b = a * 2;

				Vector2 result; Vector2.Min (ref a, ref b, out result);

				Assert.That(result.X, Is.EqualTo(a.X < b.X ? a.X : b.X ));
				Assert.That(result.Y, Is.EqualTo(a.Y < b.Y ? a.Y : b.Y ));
			}
		}

		[Test]
		public void TestStaticFn_Max ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				Vector2 b = GetNextRandomVector2();

				Vector2 result; Vector2.Max (ref a, ref b, out result);

				Assert.That(result.X, Is.EqualTo(a.X > b.X ? a.X : b.X ));
				Assert.That(result.Y, Is.EqualTo(a.Y > b.Y ? a.Y : b.Y ));
			}
		}

		[Test]
		public void TestStaticFn_Clamp_i ()
		{
			Vector2 min = new Vector2(-30, 1);
			Vector2 max = new Vector2(32, 130);

			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();

				Vector2 result; Vector2.Clamp (ref a, ref min, ref max, out result);

				Assert.That(result.X, Is.LessThanOrEqualTo(max.X));
				Assert.That(result.Y, Is.LessThanOrEqualTo(max.Y));
				Assert.That(result.X, Is.GreaterThanOrEqualTo(min.X));
				Assert.That(result.Y, Is.GreaterThanOrEqualTo(min.Y));
			}
		}

		[Test]
		public void TestStaticFn_Clamp_ii ()
		{
			Vector2 min = new Vector2(-30, 1);
			Vector2 max = new Vector2(32, 130);

			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = new Vector2(-1, 13);

				Vector2 expected = a;

				Vector2 result; Vector2.Clamp (ref a, ref min, ref max, out result);

				Assert.That(result.X, Is.LessThanOrEqualTo(max.X));
				Assert.That(result.Y, Is.LessThanOrEqualTo(max.Y));
				Assert.That(result.X, Is.GreaterThanOrEqualTo(min.X));
				Assert.That(result.Y, Is.GreaterThanOrEqualTo(min.Y));

				Assert.That(a, Is.EqualTo(expected));
			}
		}

		[Test]
		public void TestStaticFn_Lerp_i ()
		{
			Double epsilon; RealMaths.Epsilon(out epsilon);

			for(Int32 j = 0; j < 100; ++j)
			{
				Double delta = j;

				delta = delta / 100;

				for(Int32 i = 0; i < 100; ++i)
				{
					Vector2 a = GetNextRandomVector2();
					Vector2 b = GetNextRandomVector2();

					Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);

					Vector2 expected = a + ( ( b - a ) * delta );

					Assert.That(result, Is.EqualTo(expected).Within(epsilon));
				}
			}
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Lerp_ii()
		{
			Double delta = 2;
			Vector2 a = GetNextRandomVector2();
			Vector2 b = GetNextRandomVector2();
			Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Lerp_iii()
		{
			Double delta = -1;
			Vector2 a = GetNextRandomVector2();
			Vector2 b = GetNextRandomVector2();
			Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Lerp_iv()
		{
			Double delta; RealMaths.Half(out delta);

			delta = -delta;

			Vector2 a = GetNextRandomVector2();
			Vector2 b = GetNextRandomVector2();
			Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);
		}


		#endregion

	}	[TestFixture]
	public class Vector3Tests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_Set ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_Length ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_LengthSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_NormaliseMemberFunction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_IsUnit ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#region Utilities

		[Test]
		public void TestConstant_Zero ()
		{
			Double zero = 0;
			var v_zero = Vector3.Zero;

			Assert.That(v_zero.X, Is.EqualTo(zero));
			Assert.That(v_zero.Y, Is.EqualTo(zero));
			Assert.That(v_zero.Z, Is.EqualTo(zero));
			
			Assert.That(v_zero, Is.EqualTo(new Vector3(zero, zero, zero)));
		}

		[Test]
		public void TestConstant_One ()
		{
			Double one = 1;
			var v_one = Vector3.One;

			Assert.That(v_one.X, Is.EqualTo(one));
			Assert.That(v_one.Y, Is.EqualTo(one));
			Assert.That(v_one.Z, Is.EqualTo(one));

			Assert.That(v_one, Is.EqualTo(new Vector3(one, one, one)));
		}

		[Test]
		public void TestConstant_UnitX ()
		{
			Double zero = 0;
			Double one = 1;

			var v_unit_x = Vector3.UnitX;

			Assert.That(v_unit_x.X, Is.EqualTo(one));
			Assert.That(v_unit_x.Y, Is.EqualTo(zero));
			Assert.That(v_unit_x.Z, Is.EqualTo(zero));

			Assert.That(v_unit_x, Is.EqualTo(new Vector3(one, zero, zero)));
		}

		[Test]
		public void TestConstant_UnitY ()
		{
			Double zero = 0;
			Double one = 1;

			var v_unit_y = Vector3.UnitY;

			Assert.That(v_unit_y.X, Is.EqualTo(zero));
			Assert.That(v_unit_y.Y, Is.EqualTo(one));
			Assert.That(v_unit_y.Z, Is.EqualTo(zero));

			Assert.That(v_unit_y, Is.EqualTo(new Vector3(zero, one, zero)));
		}

		[Test]
		public void TestConstant_UnitZ ()
		{
			Double zero = 0;
			Double one = 1;

			var v_unit_z = Vector3.UnitZ;

			Assert.That(v_unit_z.X, Is.EqualTo(zero));
			Assert.That(v_unit_z.Y, Is.EqualTo(zero));
			Assert.That(v_unit_z.Z, Is.EqualTo(one));

			Assert.That(v_unit_z, Is.EqualTo(new Vector3(zero, zero, one)));
		}

		#endregion
		#region Maths

		[Test]
		public void TestStaticFn_Distance ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_DistanceSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Dot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_PerpDot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Perpendicular ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Normalise ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Reflect ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformMatix44 ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformNormal ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformQuaternion ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Utilities

		[Test]
		public void TestOperator_Addition ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Subtraction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Negation ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Multiplication ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Division ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Splines

		[Test]
		public void TestStaticFn_Barycentric ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_SmoothStep ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_CatmullRom ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Hermite ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
				#region Utilities

		[Test]
		public void TestStaticFn_Min ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Max ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Clamp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Lerp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion

	}	[TestFixture]
	public class Vector4Tests
	{
		[Test]
		public void Test_Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Test_Equality ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_Set ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_Length ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_LengthSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_NormaliseMemberFunction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestMemberFn_IsUnit ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#region Utilities

		[Test]
		public void TestConstant_Zero ()
		{
			Double zero = 0;
			var v_zero = Vector4.Zero;

			Assert.That(v_zero.W, Is.EqualTo(zero));
			Assert.That(v_zero.X, Is.EqualTo(zero));
			Assert.That(v_zero.Y, Is.EqualTo(zero));
			Assert.That(v_zero.Z, Is.EqualTo(zero));

			Assert.That(v_zero, Is.EqualTo(new Vector4(zero, zero, zero, zero)));
		}

		[Test]
		public void TestConstant_One ()
		{
			Double one = 1;
			var v_one = Vector4.One;

			Assert.That(v_one.W, Is.EqualTo(one));
			Assert.That(v_one.X, Is.EqualTo(one));
			Assert.That(v_one.Y, Is.EqualTo(one));
			Assert.That(v_one.Z, Is.EqualTo(one));

			Assert.That(v_one, Is.EqualTo(new Vector4(one, one, one, one)));
		}

		[Test]
		public void TestConstant_UnitX ()
		{
			Double zero = 0;
			Double one = 1;

			var v_unit_x = Vector4.UnitX;

			Assert.That(v_unit_x.X, Is.EqualTo(one));
			Assert.That(v_unit_x.Y, Is.EqualTo(zero));
			Assert.That(v_unit_x.Z, Is.EqualTo(zero));
			Assert.That(v_unit_x.W, Is.EqualTo(zero));

			Assert.That(v_unit_x, Is.EqualTo(new Vector4(one, zero, zero, zero)));
		}

		[Test]
		public void TestConstant_UnitY ()
		{
			Double zero = 0;
			Double one = 1;

			var v_unit_y = Vector4.UnitY;

			Assert.That(v_unit_y.X, Is.EqualTo(zero));
			Assert.That(v_unit_y.Y, Is.EqualTo(one));
			Assert.That(v_unit_y.Z, Is.EqualTo(zero));
			Assert.That(v_unit_y.W, Is.EqualTo(zero));

			Assert.That(v_unit_y, Is.EqualTo(new Vector4(zero, one, zero, zero)));
		}

		[Test]
		public void TestConstant_UnitZ ()
		{
			Double zero = 0;
			Double one = 1;

			var v_unit_z = Vector4.UnitZ;

			Assert.That(v_unit_z.X, Is.EqualTo(zero));
			Assert.That(v_unit_z.Y, Is.EqualTo(zero));
			Assert.That(v_unit_z.Z, Is.EqualTo(one));
			Assert.That(v_unit_z.W, Is.EqualTo(zero));
			
			Assert.That(v_unit_z, Is.EqualTo(new Vector4(zero, zero, one, zero)));
		}

		[Test]
		public void TestConstant_UnitW ()
		{
			Double zero = 0;
			Double one = 1;

			var v_unit_w = Vector4.UnitW;

			Assert.That(v_unit_w.X, Is.EqualTo(zero));
			Assert.That(v_unit_w.Y, Is.EqualTo(zero));
			Assert.That(v_unit_w.Z, Is.EqualTo(zero));
			Assert.That(v_unit_w.W, Is.EqualTo(one));


			Assert.That(v_unit_w, Is.EqualTo(new Vector4(zero, zero, zero, one)));
		}

		#endregion
		#region Maths

		[Test]
		public void TestStaticFn_Distance ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_DistanceSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Dot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_PerpDot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Perpendicular ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Normalise ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Reflect ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformMatix44 ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformNormal ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_TransformQuaternion ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Utilities

		[Test]
		public void TestOperator_Addition ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Subtraction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Negation ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Multiplication ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestOperator_Division ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Splines

		[Test]
		public void TestStaticFn_Barycentric ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_SmoothStep ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_CatmullRom ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Hermite ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
				#region Utilities

		[Test]
		public void TestStaticFn_Min ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Max ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Clamp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TestStaticFn_Lerp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion

	}}

