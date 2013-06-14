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
	}
	[TestFixture]
	public class Vector2Tests
	{
		[Test]
		public void Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Set ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Length ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void LengthSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void NormaliseMemberFunction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Equals ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void IsUnit ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#region Utilities

		[Test]
		public void Zero ()
		{
			Single zero = 0;
			var v_zero = Vector2.Zero;

			Assert.That(v_zero.X, Is.EqualTo(zero));
			Assert.That(v_zero.Y, Is.EqualTo(zero));

			Assert.That(v_zero, Is.EqualTo(new Vector2(zero, zero)));
		}

		[Test]
		public void One ()
		{
			Single one = 1;
			var v_one = Vector2.One;

			Assert.That(v_one.X, Is.EqualTo(one));
			Assert.That(v_one.Y, Is.EqualTo(one));

			Assert.That(v_one, Is.EqualTo(new Vector2(one, one)));
		}

		[Test]
		public void UnitX ()
		{
			Single zero = 0;
			Single one = 1;

			var v_unit_x = Vector2.UnitX;

			Assert.That(v_unit_x.X, Is.EqualTo(one));
			Assert.That(v_unit_x.Y, Is.EqualTo(zero));

			Assert.That(v_unit_x, Is.EqualTo(new Vector2(one, zero)));
		}

		[Test]
		public void UnitY ()
		{
			Single zero = 0;
			Single one = 1;

			var v_unit_y = Vector2.UnitY;

			Assert.That(v_unit_y.X, Is.EqualTo(zero));
			Assert.That(v_unit_y.Y, Is.EqualTo(one));
			
			Assert.That(v_unit_y, Is.EqualTo(new Vector2(zero, one)));
		}

		#endregion
		#region Utilities

		[Test]
		public void Distance ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void DistanceSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Dot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void PerpDot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Perpendicular ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Normalise ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Reflect ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformMatix44 ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformNormal ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformQuaternion ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Utilities

		[Test]
		public void Addition ()
		{
			var zero = Vector2.Zero;
			Single w = -3;
			Single x = 3;
			Single y = -6;
			Single z = 9;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);
			var c = new Vector2(w, x);

			// test addition with the (+) operator
			var test1_op = a + b;
			var test2_op = b + a;
			var test3_op = c + a;
			var test4_op = c + b;
			var test5_op = b + zero;
			var test6_op = zero + c;
			var test7_op = zero + zero;

			Assert.That(test1_op, Is.EqualTo(c));
			Assert.That(test2_op, Is.EqualTo(c));
			Assert.That(test3_op, Is.Not.EqualTo(c));
			Assert.That(test4_op, Is.Not.EqualTo(c));
			Assert.That(test5_op, Is.EqualTo(b));
			Assert.That(test6_op, Is.EqualTo(c));
			Assert.That(test7_op, Is.EqualTo(zero));

			// Test addition with the static Add function
			Vector2 test1_st; Vector2.Add(ref a, ref b, out test1_st);
			Vector2 test2_st; Vector2.Add(ref b, ref a, out test2_st);
			Vector2 test3_st; Vector2.Add(ref c, ref a, out test3_st);
			Vector2 test4_st; Vector2.Add(ref c, ref b, out test4_st);
			Vector2 test5_st; Vector2.Add(ref b, ref zero, out test5_st);
			Vector2 test6_st; Vector2.Add(ref zero, ref c, out test6_st);
			Vector2 test7_st; Vector2.Add(ref zero, ref zero, out test7_st);

			Assert.That(test1_st, Is.EqualTo(c));
			Assert.That(test2_st, Is.EqualTo(c));
			Assert.That(test3_st, Is.Not.EqualTo(c));
			Assert.That(test4_st, Is.Not.EqualTo(c));
			Assert.That(test5_st, Is.EqualTo(b));
			Assert.That(test6_st, Is.EqualTo(c));
			Assert.That(test7_st, Is.EqualTo(zero));
		}

		[Test]
		public void Subtraction ()
		{
			var zero = Vector2.Zero;
			Single r = 34;
			Single s = -91;
			Single t = -34;
			Single u = 91;

			Single x = 33;
			Single y = -1;
			Single z = 90;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);
			var c = new Vector2(x, z);


			var d = new Vector2(r, s);
			var e = new Vector2(t, u);

			// Test subtraction with the (-) operator
			var test1_op = a - b;
			var test2_op = b - a;
			var test3_op = c - a;
			var test4_op = c - b;
			var test5_op = a - zero;
			var test6_op = zero - d;
			var test7_op = zero - zero;

			Assert.That(test1_op, Is.EqualTo(d));
			Assert.That(test2_op, Is.EqualTo(e));
			Assert.That(test3_op, Is.Not.EqualTo(c));
			Assert.That(test4_op, Is.Not.EqualTo(c));
			Assert.That(test5_op, Is.EqualTo(a));
			Assert.That(test6_op, Is.EqualTo(e));
			Assert.That(test7_op, Is.EqualTo(zero));

			// Test subtraction with the static Subtract function
			Vector2 test1_st; Vector2.Subtract(ref a, ref b, out test1_st);
			Vector2 test2_st; Vector2.Subtract(ref b, ref a, out test2_st);
			Vector2 test3_st; Vector2.Subtract(ref c, ref a, out test3_st);
			Vector2 test4_st; Vector2.Subtract(ref c, ref b, out test4_st);
			Vector2 test5_st; Vector2.Subtract(ref a, ref zero, out test5_st);
			Vector2 test6_st; Vector2.Subtract(ref zero, ref d, out test6_st);
			Vector2 test7_st; Vector2.Subtract(ref zero, ref zero, out test7_st);

			Assert.That(test1_st, Is.EqualTo(d));
			Assert.That(test2_st, Is.EqualTo(e));
			Assert.That(test3_st, Is.Not.EqualTo(c));
			Assert.That(test4_st, Is.Not.EqualTo(c));
			Assert.That(test5_st, Is.EqualTo(a));
			Assert.That(test6_st, Is.EqualTo(e));
			Assert.That(test7_st, Is.EqualTo(zero));
		}

		[Test]
		public void Negation ()
		{
			Single r = 3432;
			Single s = -6218;
			Single t = -3432;
			Single u = 6218;

			var zero = Vector2.Zero;
			var a = new Vector2(r, s);
			var b = new Vector2(u, t);
			var c = new Vector2(t, u);
			var d = new Vector2(s, r);

			// Test negation with the Negate member function
			Vector2 test1_st; Vector2.Negate(ref a, out test1_st);
			Vector2 test2_st; Vector2.Negate(ref b, out test2_st);
			Vector2 test3_st; Vector2.Negate(ref c, out test3_st);
			Vector2 test4_st; Vector2.Negate(ref d, out test4_st);
			Vector2 test5_st; Vector2.Negate(ref zero, out test5_st);

			Assert.That(test1_st, Is.EqualTo(c));
			Assert.That(test2_st, Is.EqualTo(d));
			Assert.That(test3_st, Is.EqualTo(zero - c));
			Assert.That(test4_st, Is.EqualTo(zero - d));
			Assert.That(test5_st, Is.EqualTo(zero));
		}

		[Test]
		public void Multiplication ()
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
		public void Division ()
		{
			Single r = 10;
			Single s = -40;
			Single t = ((Single) 1 ) / ((Single) 10);
			Single u = ((Single) (-1) ) / ((Single) 40 );

			Single x = 2000;
			Single y = 200;
			Single z = -5;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);

			var c = new Vector2(r, s);
			var d = new Vector2(t, u);

			// Test multiplication with the (*) operator
			var test1_op = a / b;
			var test2_op = b / a;

			Assert.That(test1_op, Is.EqualTo(c));
			Assert.That(test2_op, Is.EqualTo(d));

			// Test multiplication with static Multiply function
			Vector2 test1_st; Vector2.Divide(ref a, ref b, out test1_st);
			Vector2 test2_st; Vector2.Divide(ref b, ref a, out test2_st);

			Assert.That(test1_st, Is.EqualTo(c));
			Assert.That(test2_st, Is.EqualTo(d));
		}

		#endregion
		#region Splines

		[Test]
		public void Barycentric ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void SmoothStep ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void CatmullRom ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Hermite ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
				#region Utilities

		[Test]
		public void Min ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Max ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Clamp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Lerp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion

	}	[TestFixture]
	public class Vector3Tests
	{
		[Test]
		public void Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Set ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Length ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void LengthSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void NormaliseMemberFunction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Equals ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void IsUnit ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#region Utilities

		[Test]
		public void Zero ()
		{
			Single zero = 0;
			var v_zero = Vector3.Zero;

			Assert.That(v_zero.X, Is.EqualTo(zero));
			Assert.That(v_zero.Y, Is.EqualTo(zero));
			Assert.That(v_zero.Z, Is.EqualTo(zero));
			
			Assert.That(v_zero, Is.EqualTo(new Vector3(zero, zero, zero)));
		}

		[Test]
		public void One ()
		{
			Single one = 1;
			var v_one = Vector3.One;

			Assert.That(v_one.X, Is.EqualTo(one));
			Assert.That(v_one.Y, Is.EqualTo(one));
			Assert.That(v_one.Z, Is.EqualTo(one));

			Assert.That(v_one, Is.EqualTo(new Vector3(one, one, one)));
		}

		[Test]
		public void UnitX ()
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
		public void UnitY ()
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
		public void UnitZ ()
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
		#region Utilities

		[Test]
		public void Distance ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void DistanceSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Dot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void PerpDot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Perpendicular ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Normalise ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Reflect ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformMatix44 ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformNormal ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformQuaternion ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Utilities

		[Test]
		public void Addition ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Subtraction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Negation ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Multiplication ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Division ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Splines

		[Test]
		public void Barycentric ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void SmoothStep ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void CatmullRom ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Hermite ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
				#region Utilities

		[Test]
		public void Min ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Max ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Clamp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Lerp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion

	}	[TestFixture]
	public class Vector4Tests
	{
		[Test]
		public void Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Set ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Length ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void LengthSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void NormaliseMemberFunction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Equals ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void IsUnit ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#region Utilities

		[Test]
		public void Zero ()
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
		public void One ()
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
		public void UnitX ()
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
		public void UnitY ()
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
		public void UnitZ ()
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
		public void UnitW ()
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
		#region Utilities

		[Test]
		public void Distance ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void DistanceSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Dot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void PerpDot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Perpendicular ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Normalise ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Reflect ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformMatix44 ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformNormal ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformQuaternion ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Utilities

		[Test]
		public void Addition ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Subtraction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Negation ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Multiplication ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Division ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Splines

		[Test]
		public void Barycentric ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void SmoothStep ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void CatmullRom ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Hermite ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
				#region Utilities

		[Test]
		public void Min ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Max ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Clamp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Lerp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion

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
	}
	[TestFixture]
	public class Vector2Tests
	{
		[Test]
		public void Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Set ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Length ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void LengthSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void NormaliseMemberFunction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Equals ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void IsUnit ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#region Utilities

		[Test]
		public void Zero ()
		{
			Double zero = 0;
			var v_zero = Vector2.Zero;

			Assert.That(v_zero.X, Is.EqualTo(zero));
			Assert.That(v_zero.Y, Is.EqualTo(zero));

			Assert.That(v_zero, Is.EqualTo(new Vector2(zero, zero)));
		}

		[Test]
		public void One ()
		{
			Double one = 1;
			var v_one = Vector2.One;

			Assert.That(v_one.X, Is.EqualTo(one));
			Assert.That(v_one.Y, Is.EqualTo(one));

			Assert.That(v_one, Is.EqualTo(new Vector2(one, one)));
		}

		[Test]
		public void UnitX ()
		{
			Double zero = 0;
			Double one = 1;

			var v_unit_x = Vector2.UnitX;

			Assert.That(v_unit_x.X, Is.EqualTo(one));
			Assert.That(v_unit_x.Y, Is.EqualTo(zero));

			Assert.That(v_unit_x, Is.EqualTo(new Vector2(one, zero)));
		}

		[Test]
		public void UnitY ()
		{
			Double zero = 0;
			Double one = 1;

			var v_unit_y = Vector2.UnitY;

			Assert.That(v_unit_y.X, Is.EqualTo(zero));
			Assert.That(v_unit_y.Y, Is.EqualTo(one));
			
			Assert.That(v_unit_y, Is.EqualTo(new Vector2(zero, one)));
		}

		#endregion
		#region Utilities

		[Test]
		public void Distance ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void DistanceSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Dot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void PerpDot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Perpendicular ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Normalise ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Reflect ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformMatix44 ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformNormal ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformQuaternion ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Utilities

		[Test]
		public void Addition ()
		{
			var zero = Vector2.Zero;
			Double w = -3;
			Double x = 3;
			Double y = -6;
			Double z = 9;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);
			var c = new Vector2(w, x);

			// test addition with the (+) operator
			var test1_op = a + b;
			var test2_op = b + a;
			var test3_op = c + a;
			var test4_op = c + b;
			var test5_op = b + zero;
			var test6_op = zero + c;
			var test7_op = zero + zero;

			Assert.That(test1_op, Is.EqualTo(c));
			Assert.That(test2_op, Is.EqualTo(c));
			Assert.That(test3_op, Is.Not.EqualTo(c));
			Assert.That(test4_op, Is.Not.EqualTo(c));
			Assert.That(test5_op, Is.EqualTo(b));
			Assert.That(test6_op, Is.EqualTo(c));
			Assert.That(test7_op, Is.EqualTo(zero));

			// Test addition with the static Add function
			Vector2 test1_st; Vector2.Add(ref a, ref b, out test1_st);
			Vector2 test2_st; Vector2.Add(ref b, ref a, out test2_st);
			Vector2 test3_st; Vector2.Add(ref c, ref a, out test3_st);
			Vector2 test4_st; Vector2.Add(ref c, ref b, out test4_st);
			Vector2 test5_st; Vector2.Add(ref b, ref zero, out test5_st);
			Vector2 test6_st; Vector2.Add(ref zero, ref c, out test6_st);
			Vector2 test7_st; Vector2.Add(ref zero, ref zero, out test7_st);

			Assert.That(test1_st, Is.EqualTo(c));
			Assert.That(test2_st, Is.EqualTo(c));
			Assert.That(test3_st, Is.Not.EqualTo(c));
			Assert.That(test4_st, Is.Not.EqualTo(c));
			Assert.That(test5_st, Is.EqualTo(b));
			Assert.That(test6_st, Is.EqualTo(c));
			Assert.That(test7_st, Is.EqualTo(zero));
		}

		[Test]
		public void Subtraction ()
		{
			var zero = Vector2.Zero;
			Double r = 34;
			Double s = -91;
			Double t = -34;
			Double u = 91;

			Double x = 33;
			Double y = -1;
			Double z = 90;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);
			var c = new Vector2(x, z);


			var d = new Vector2(r, s);
			var e = new Vector2(t, u);

			// Test subtraction with the (-) operator
			var test1_op = a - b;
			var test2_op = b - a;
			var test3_op = c - a;
			var test4_op = c - b;
			var test5_op = a - zero;
			var test6_op = zero - d;
			var test7_op = zero - zero;

			Assert.That(test1_op, Is.EqualTo(d));
			Assert.That(test2_op, Is.EqualTo(e));
			Assert.That(test3_op, Is.Not.EqualTo(c));
			Assert.That(test4_op, Is.Not.EqualTo(c));
			Assert.That(test5_op, Is.EqualTo(a));
			Assert.That(test6_op, Is.EqualTo(e));
			Assert.That(test7_op, Is.EqualTo(zero));

			// Test subtraction with the static Subtract function
			Vector2 test1_st; Vector2.Subtract(ref a, ref b, out test1_st);
			Vector2 test2_st; Vector2.Subtract(ref b, ref a, out test2_st);
			Vector2 test3_st; Vector2.Subtract(ref c, ref a, out test3_st);
			Vector2 test4_st; Vector2.Subtract(ref c, ref b, out test4_st);
			Vector2 test5_st; Vector2.Subtract(ref a, ref zero, out test5_st);
			Vector2 test6_st; Vector2.Subtract(ref zero, ref d, out test6_st);
			Vector2 test7_st; Vector2.Subtract(ref zero, ref zero, out test7_st);

			Assert.That(test1_st, Is.EqualTo(d));
			Assert.That(test2_st, Is.EqualTo(e));
			Assert.That(test3_st, Is.Not.EqualTo(c));
			Assert.That(test4_st, Is.Not.EqualTo(c));
			Assert.That(test5_st, Is.EqualTo(a));
			Assert.That(test6_st, Is.EqualTo(e));
			Assert.That(test7_st, Is.EqualTo(zero));
		}

		[Test]
		public void Negation ()
		{
			Double r = 3432;
			Double s = -6218;
			Double t = -3432;
			Double u = 6218;

			var zero = Vector2.Zero;
			var a = new Vector2(r, s);
			var b = new Vector2(u, t);
			var c = new Vector2(t, u);
			var d = new Vector2(s, r);

			// Test negation with the Negate member function
			Vector2 test1_st; Vector2.Negate(ref a, out test1_st);
			Vector2 test2_st; Vector2.Negate(ref b, out test2_st);
			Vector2 test3_st; Vector2.Negate(ref c, out test3_st);
			Vector2 test4_st; Vector2.Negate(ref d, out test4_st);
			Vector2 test5_st; Vector2.Negate(ref zero, out test5_st);

			Assert.That(test1_st, Is.EqualTo(c));
			Assert.That(test2_st, Is.EqualTo(d));
			Assert.That(test3_st, Is.EqualTo(zero - c));
			Assert.That(test4_st, Is.EqualTo(zero - d));
			Assert.That(test5_st, Is.EqualTo(zero));
		}

		[Test]
		public void Multiplication ()
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
		public void Division ()
		{
			Double r = 10;
			Double s = -40;
			Double t = ((Double) 1 ) / ((Double) 10);
			Double u = ((Double) (-1) ) / ((Double) 40 );

			Double x = 2000;
			Double y = 200;
			Double z = -5;

			var a = new Vector2(x, y);
			var b = new Vector2(y, z);

			var c = new Vector2(r, s);
			var d = new Vector2(t, u);

			// Test multiplication with the (*) operator
			var test1_op = a / b;
			var test2_op = b / a;

			Assert.That(test1_op, Is.EqualTo(c));
			Assert.That(test2_op, Is.EqualTo(d));

			// Test multiplication with static Multiply function
			Vector2 test1_st; Vector2.Divide(ref a, ref b, out test1_st);
			Vector2 test2_st; Vector2.Divide(ref b, ref a, out test2_st);

			Assert.That(test1_st, Is.EqualTo(c));
			Assert.That(test2_st, Is.EqualTo(d));
		}

		#endregion
		#region Splines

		[Test]
		public void Barycentric ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void SmoothStep ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void CatmullRom ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Hermite ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
				#region Utilities

		[Test]
		public void Min ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Max ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Clamp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Lerp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion

	}	[TestFixture]
	public class Vector3Tests
	{
		[Test]
		public void Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Set ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Length ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void LengthSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void NormaliseMemberFunction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Equals ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void IsUnit ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#region Utilities

		[Test]
		public void Zero ()
		{
			Double zero = 0;
			var v_zero = Vector3.Zero;

			Assert.That(v_zero.X, Is.EqualTo(zero));
			Assert.That(v_zero.Y, Is.EqualTo(zero));
			Assert.That(v_zero.Z, Is.EqualTo(zero));
			
			Assert.That(v_zero, Is.EqualTo(new Vector3(zero, zero, zero)));
		}

		[Test]
		public void One ()
		{
			Double one = 1;
			var v_one = Vector3.One;

			Assert.That(v_one.X, Is.EqualTo(one));
			Assert.That(v_one.Y, Is.EqualTo(one));
			Assert.That(v_one.Z, Is.EqualTo(one));

			Assert.That(v_one, Is.EqualTo(new Vector3(one, one, one)));
		}

		[Test]
		public void UnitX ()
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
		public void UnitY ()
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
		public void UnitZ ()
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
		#region Utilities

		[Test]
		public void Distance ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void DistanceSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Dot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void PerpDot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Perpendicular ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Normalise ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Reflect ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformMatix44 ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformNormal ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformQuaternion ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Utilities

		[Test]
		public void Addition ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Subtraction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Negation ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Multiplication ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Division ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Splines

		[Test]
		public void Barycentric ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void SmoothStep ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void CatmullRom ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Hermite ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
				#region Utilities

		[Test]
		public void Min ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Max ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Clamp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Lerp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion

	}	[TestFixture]
	public class Vector4Tests
	{
		[Test]
		public void Constructors ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Set ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Length ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void LengthSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void NormaliseMemberFunction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void ToString ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Equals ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void IsUnit ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void GetHashCode ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#region Utilities

		[Test]
		public void Zero ()
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
		public void One ()
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
		public void UnitX ()
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
		public void UnitY ()
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
		public void UnitZ ()
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
		public void UnitW ()
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
		#region Utilities

		[Test]
		public void Distance ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void DistanceSquared ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Dot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void PerpDot ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Perpendicular ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Normalise ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Reflect ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformMatix44 ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformNormal ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void TransformQuaternion ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Utilities

		[Test]
		public void Addition ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Subtraction ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Negation ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Multiplication ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Division ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
		#region Splines

		[Test]
		public void Barycentric ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void SmoothStep ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void CatmullRom ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Hermite ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion
				#region Utilities

		[Test]
		public void Min ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Max ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Clamp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		[Test]
		public void Lerp ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		#endregion

	}}

