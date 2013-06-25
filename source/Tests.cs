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
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using NUnit.Framework;

namespace Sungiant.Abacus.Tests
{

	[TestFixture]
    public class RealMaths
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
            Byte packed = Byte.MinValue;
            while ( packed < Byte.MaxValue )
            {
                ++packed;

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
            UInt16 packed = UInt16.MinValue;
            while ( packed < UInt16.MaxValue )
            {
                ++packed;
                
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
            UInt16 packed = UInt16.MinValue;
            while ( packed < UInt16.MaxValue )
            {
                ++packed;
                
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
            UInt16 packed = UInt16.MinValue;
            while ( packed < UInt16.MaxValue )
            {
                ++packed;
                
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
            UInt16 packed = UInt16.MinValue;
            while ( packed < UInt16.MaxValue )
            {
                ++packed;

                var packedObj = new NormalisedByte2();
                
                packedObj.PackedValue = packed;
                
                SinglePrecision.Vector2 unpacked;
                
                packedObj.UnpackTo(out unpacked);

                Console.WriteLine("p: " + packed + ", v: " + unpacked);
                
                var newPackedObj = new NormalisedByte2(ref unpacked);
                
                Assert.That(newPackedObj.PackedValue, Is.EqualTo(packed).Within(5));
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


namespace Sungiant.Abacus.SinglePrecision.Tests
{
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
	public class Vector2Tests
	{
				/// <summary>
		/// The random number generator used for testing.
		/// </summary>
		static readonly System.Random rand;

		/// <summary>
		/// Static constructor used to ensure that the random number generator
		/// always gets initilised with the same seed, making the tests
		/// behave in a deterministic manner.
		/// </summary>
		static Vector2Tests()
		{
			rand = new System.Random(0);
		}

		/// <summary>
		/// Helper function for getting the next random Single value.
		/// </summary>
		static Single GetNextRandomSingle()
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

		/// <summary>
		/// Helper function for getting the next random Vector2.
		/// </summary>
		static Vector2 GetNextRandomVector2()
		{
			Single a = GetNextRandomSingle();
			Single b = GetNextRandomSingle();

			return new Vector2(a, b);
		}

		/// <summary>
		/// Helper to encapsulate asserting that two vectors are equal.
		/// </summary>
		static void AssertEqualWithinReason(Vector2 a, Vector2 b)
		{
			Single tolerance; RealMaths.TestTolerance(out tolerance);

			Assert.That(a.X, Is.EqualTo(b.X).Within(tolerance));
			Assert.That(a.Y, Is.EqualTo(b.Y).Within(tolerance));
		}
		

		// Test: StructLayout //----------------------------------------------//

		/// <summary>
		/// This test makes sure that the struct layout has been defined
		/// correctly and that X and Y are in the correct order and the only
		/// member variables using reflection.
		/// </summary>
		[Test]
		public void Test_StructLayout_i ()
		{
			Type t = typeof(Vector2);
			
			Assert.That(false, Is.EqualTo(true));
		}

		/// <summary>
		/// This test makes sure that when examining the memory addresses of the 
		/// X and Y member variables of a number of randomly generated Vector2
		/// object the results are as expected. 
		/// </summary>
		[Test]
		public unsafe void Test_StructLayout_ii ()
		{
			for( Int32 i = 0; i < 100; ++ i)
			{
				Vector2 vec = GetNextRandomVector2();

				GCHandle h_vec = GCHandle.Alloc(vec, GCHandleType.Pinned);
				GCHandle h_x = GCHandle.Alloc(vec.X, GCHandleType.Pinned);
				GCHandle h_y = GCHandle.Alloc(vec.Y, GCHandleType.Pinned);

		        IntPtr vecAddress = h_vec.AddrOfPinnedObject();
		        IntPtr resultX = h_x.AddrOfPinnedObject();
		        IntPtr resultY = h_y.AddrOfPinnedObject();

		        String strVecAddress = "0x" + vecAddress.ToString("x");
		        String strResultX = "0x" + resultX.ToString("x");
		        String strResultY = "0x" + resultY.ToString("x");

		        Console.WriteLine("&v: " + strVecAddress);
		        Console.WriteLine("&x: " + strResultX);
		        Console.WriteLine("&y: " + strResultY);

		        Assert.That(resultX, Is.EqualTo(vecAddress));

		        IntPtr q = IntPtr.Add(resultX, sizeof(Single));

		        Assert.That(resultY, Is.EqualTo(q));
				
		        h_vec.Free();
		        h_x.Free();
		        h_y.Free();
			}
		}

		// Test: Constructors //----------------------------------------------//

		/// <summary>
		/// This test goes though each public constuctor and ensures that the 
		/// data members of the structure have been properly set.
		/// </summary>
		[Test]
		public void Test_Constructors_i ()
		{
			// Test default values
			Vector2 a = new Vector2();
			Assert.That(a, Is.EqualTo(Vector2.Zero));

			// Test Vector2( n ) where n is Single
			Single u = -189;
			Single v = 429;

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

			// Test no constructor
			Vector2 e;
			e.X = 0;
			e.Y = 0;
			Assert.That(e, Is.EqualTo(Vector2.Zero));
		}

		// Test Member Fn: ToString //----------------------------------------//

		/// <summary>
		/// For a given example, this test ensures that the ToString function
		/// yields the expected string.
		/// </summary>
		[Test]
		public void TestMemberFn_ToString_i ()
		{
			Vector2 a = new Vector2(42, -17);

			String result = a.ToString();

			String expected = "{X:42 Y:-17}";

			Assert.That(result, Is.EqualTo(expected));
		}

		// Test Member Fn: GetHashCode //-------------------------------------//

		/// <summary>
		/// Makes sure that the hashing function is good by testing 10,000
		/// random scenarios and ensuring that there are no more than 10
		/// collisions.
		/// </summary>
		[Test]
		public void TestMemberFn_GetHashCode_i ()
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

		// Test Member Fn: Length //------------------------------------------//

		/// <summary>
		/// Tests that for a known example the Length member function yields
		/// the correct result.
		/// </summary>
		[Test]
		public void TestMemberFn_Length_i ()
		{
			Vector2 a = new Vector2(30, -40);

			Single expected = 50;

			Single result = a.Length();

			Assert.That(result, Is.EqualTo(expected));
		}

		// Test Member Fn: LengthSquared //-----------------------------------//

		/// <summary>
		/// Tests that for a known example the LengthSquared member function 
		/// yields the correct result.
		/// </summary>
		[Test]
		public void TestMemberFn_LengthSquared_i ()
		{
			Vector2 a = new Vector2(30, -40);

			Single expected = 2500;

			Single result = a.LengthSquared();

			Assert.That(result, Is.EqualTo(expected));
		}

		// Test Member Fn: IsUnit //------------------------------------------//

		/// <summary>
		/// Tests that for the most simple unit vectors the IsUnit member 
		/// function returns the correct result of TRUE.
		/// </summary>
		[Test]
		public void TestMemberFn_IsUnit_i ()
		{
			Assert.That(new Vector2( 1,  0).IsUnit(), Is.EqualTo(true));
			Assert.That(new Vector2(-1,  0).IsUnit(), Is.EqualTo(true));
			Assert.That(new Vector2( 1,  1).IsUnit(), Is.EqualTo(false));
			Assert.That(new Vector2( 0,  0).IsUnit(), Is.EqualTo(false));
			Assert.That(new Vector2( 0, -1).IsUnit(), Is.EqualTo(true));
			Assert.That(new Vector2( 0,  1).IsUnit(), Is.EqualTo(true));
		}

		/// <summary>
		/// This test makes sure that the IsUnit member function returns the 
		/// correct result of TRUE for a number of scenarios where the test 
		/// vector is both random and normalised.
		/// </summary>
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

		/// <summary>
		/// This test ensures that the IsUnit member function correctly
		/// returns TRUE for a collection of vectors, all known to be of unit 
		/// length.
		/// </summary>
		[Test]
		public void TestMemberFn_IsUnit_iii ()
		{
			Single piOver2; RealMaths.PiOver2(out piOver2);

			for( Int32 i = 0; i <= 90; ++ i)
			{
				Single theta = piOver2 / 90 * i;

				Single opposite = RealMaths.Sin(theta);
				Single adjacent = RealMaths.Cos(theta);				

				Assert.That(
					new Vector2( opposite,  adjacent).IsUnit(), 
					Is.EqualTo(true));
				
				Assert.That(
					new Vector2( opposite, -adjacent).IsUnit(), 
					Is.EqualTo(true));
				
				Assert.That(
					new Vector2(-opposite,  adjacent).IsUnit(), 
					Is.EqualTo(true));
				
				Assert.That(
					new Vector2(-opposite, -adjacent).IsUnit(), 
					Is.EqualTo(true));
			}
		}

		/// <summary>
		/// This test makes sure that the IsUnit member function returns the 
		/// correct result of FALSE for a number of scenarios where the test 
		/// vector is randomly generated and not normalised.  It's highly
		/// unlikely that the random generator will create a unit vector!
		/// </summary>
		[Test]
		public void TestMemberFn_IsUnit_iv ()
		{
			for( Int32 i = 0; i < 100; ++ i)
			{
				Vector2 a = GetNextRandomVector2();

				Assert.That(a.IsUnit(), Is.EqualTo(false));
			}
		}
			
		// Test Constant: Zero //---------------------------------------------//

		/// <summary>
		/// Tests to make sure that a Vector2 initilised using the Zero constant
		/// has it's member variables correctly set.
		/// </summary>
		[Test]
		public void TestConstant_Zero ()
		{
			Single zero = 0;
			var v_zero = Vector2.Zero;

			Assert.That(v_zero.X, Is.EqualTo(zero));
			Assert.That(v_zero.Y, Is.EqualTo(zero));

			Assert.That(v_zero, Is.EqualTo(new Vector2(zero, zero)));
		}

		// Test Constant: One //----------------------------------------------//

		/// <summary>
		/// Tests to make sure that a Vector2 initilised using the One constant
		/// has it's member variables correctly set.
		/// </summary>
		[Test]
		public void TestConstant_One ()
		{
			Single one = 1;
			var v_one = Vector2.One;

			Assert.That(v_one.X, Is.EqualTo(one));
			Assert.That(v_one.Y, Is.EqualTo(one));

			Assert.That(v_one, Is.EqualTo(new Vector2(one, one)));
		}

		// Test Constant: UnitX //--------------------------------------------//

		/// <summary>
		/// Tests to make sure that a Vector2 initilised using the UnitX 
		/// constant has it's member variables correctly set.
		/// </summary>
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

		// Test Constant: UnitY //--------------------------------------------//

		/// <summary>
		/// Tests to make sure that a Vector2 initilised using the UnitY
		/// constant has it's member variables correctly set.
		/// </summary>
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

		// Test Static Fn: Distance //----------------------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Distance_i ()
		{
			Vector2 a = new Vector2(0, 4);
			Vector2 b = new Vector2(3, 0);

			Single expected = 5;
			Single result; Vector2.Distance(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Distance_ii ()
		{
			Vector2 a = new Vector2(0, -4);
			Vector2 b = new Vector2(3, 0);

			Single expected = 5;
			Single result; Vector2.Distance(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Distance_iii ()
		{
			Vector2 a = new Vector2(0, -4);
			Vector2 b = new Vector2(-3, 0);

			Single expected = 5;
			Single result; Vector2.Distance(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Distance_iv ()
		{
			Vector2 a = Vector2.Zero;

			Single expected = 0;

			Assert.That(a.Length(), Is.EqualTo(expected));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Distance_v ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				
				Single expected = 
					RealMaths.Sqrt((a.X * a.X) + (a.Y * a.Y));

				Assert.That(a.Length(), Is.EqualTo(expected));
			}
		}

		// Test Static Fn: DistanceSquared //---------------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_DistanceSquared_i ()
		{
			Vector2 a = new Vector2(0, 4);
			Vector2 b = new Vector2(3, 0);

			Single expected = 25;
			Single result;
			Vector2.DistanceSquared(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_DistanceSquared_ii ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				Vector2 b = GetNextRandomVector2();
				Vector2 c = b - a;
				Single expected = (c.X * c.X) + (c.Y * c.Y);
				Single result;
				Vector2.DistanceSquared(ref a, ref b, out result);

				Assert.That(result, Is.EqualTo(expected));
			}
		}

		// Test Static Fn: Dot //---------------------------------------------//

		/// <summary>
		/// 
		/// </summary>
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

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Dot_ii ()
		{
			Vector2 a = new Vector2(1, 0);
			Vector2 b = new Vector2(-1, 0);

			Single expected = -1;
			Single result; Vector2.Dot(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		/// <summary>
		/// 
		/// </summary>
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

		// Test Static Fn: Normalise //---------------------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Normalise_i()
		{
			Vector2 a = Vector2.Zero;

			Vector2 b; Vector2.Normalise(ref a, out b);
		}

		/// <summary>
		/// 
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Normalise_ii()
		{
			Vector2 a = new Vector2(
				Single.MaxValue, 
				Single.MaxValue);

			Vector2 b; Vector2.Normalise(ref a, out b);
		}

		/// <summary>
		/// 
		/// </summary>
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

		// Test Static Fn: Reflect //-----------------------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Reflect_i ()
		{
			Assert.That (true, Is.EqualTo (false));
		}

		// Test Static Fn: TransformMatrix44 //-------------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_TransformMatix44_i ()
		{
			Assert.That (true, Is.EqualTo (false));
		}

		// Test Static Fn: TransformNormal //---------------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_TransformNormal_i ()
		{
			Assert.That (true, Is.EqualTo (false));
		}

		// Test Static Fn: TransformQuaternion //-----------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_TransformQuaternion_i ()
		{
			Assert.That (true, Is.EqualTo (false));
		}

		// Test Operator: Equality //-----------------------------------------//

		/// <summary>
		/// Helper method for testing equality.
		/// </summary>
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

		/// <summary>
		/// Makes sure that, for a known example, all the equality opperators
		/// and functions yield the expected result of TRUE when two equal  
		/// Vector2 objects are compared.
		/// </summary>
		[Test]
		public void TestOperator_Equality_i ()
		{
			var a = new Vector2(44, -54);
			var b = new Vector2(44, -54);

			Boolean expected = true;

			this.TestEquality(a, b, expected);
		}

		/// <summary>
		/// Makes sure that, for a known example, all the equality opperators
		/// and functions yield the expected result of FALSE when two unequal  
		/// Vector2 objects are compared.
		/// </summary>
		[Test]
		public void TestOperator_Equality_ii ()
		{
			var a = new Vector2(44, 54);
			var b = new Vector2(44, -54);

			Boolean expected = false;

			this.TestEquality(a, b, expected);
		}

		/// <summary>
		/// Tests to make sure that all the equality opperators and functions 
		/// yield the expected result of TRUE when used on a number of randomly 
		/// generated pairs of equal Vector2 objects.
		/// </summary>
		[Test]
		public void TestOperator_Equality_iii ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();

				Vector2 b = a;

				this.TestEquality(a, b, true);
			}
		}


		// Test Operator: Addition //-----------------------------------------//

		/// <summary>
		/// Helper method for testing addition.
		/// </summary>
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
		/// Assert that, for a known example, all the addition opperators
		/// and functions yield the correct result.
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
		/// Assert that, for a known example involving the zero vector, all the 
		/// addition opperators and functions yield the correct result.
		/// </summary>
		[Test]
		public void TestOperator_Addition_ii ()
		{
			var a = new Vector2(-2313, 88);

			var expected = a;

			this.TestAddition(a, Vector2.Zero, expected);
		}

		/// <summary>
		/// Assert that, for a known example involving two zero vectors, all the 
		/// addition opperators and functions yield the correct result of zero.
		/// </summary>
		[Test]
		public void TestOperator_Addition_iii ()
		{
			this.TestAddition(Vector2.Zero, Vector2.Zero, Vector2.Zero);
		}

		/// <summary>
		/// Assert that, for a number of randomly generated scenarios, all the 
		/// addition opperators and functions yield the same results as a
		/// manual addition calculation.
		/// </summary>
		[Test]
		public void TestOperator_Addition_iv ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				var b = GetNextRandomVector2();

				var expected = new Vector2(a.X + b.X, a.Y + b.Y);

				this.TestAddition(a, b, expected);
			}
		}

		// Test Operator: Subtraction //--------------------------------------//
		
		/// <summary>
		/// Helper method for testing subtraction.
		/// </summary>
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
		/// Assert that, for known examples, all the subtraction opperators
		/// and functions yield the correct result.
		/// <summary>
		[Test]
		public void TestOperator_Subtraction_i ()
		{
			var a = new Vector2(12, -4);
			var b = new Vector2(15, 11);
			var c = new Vector2(-423, 342);

			var expected = new Vector2(-3, -15);

			this.TestSubtraction(a, b, expected);
			this.TestSubtraction(c, Vector2.Zero, c);
		}

		/// <summary>
		/// Assert that when subtracting the zero vector fromt the zero vector, 
		/// all the subtraction opperators and functions yield the correct 
		/// result.
		/// <summary>
		[Test]
		public void TestOperator_Subtraction_ii ()
		{
			this.TestSubtraction(Vector2.Zero, Vector2.Zero, Vector2.Zero);
		}

		/// <summary>
		/// Assert that, for a number of randomly generated scenarios, all the 
		/// subtraction opperators and functions yield the same results as a
		/// manual subtraction calculation.
		/// </summary>
		[Test]
		public void TestOperator_Subtraction_iii ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				var b = GetNextRandomVector2();

				var expected = new Vector2(a.X - b.X, a.Y - b.Y);

				this.TestSubtraction(a, b, expected);
			}
		}

		// Test Operator: Negation //-----------------------------------------//
		
		/// <summary>
		/// Helper method for testing negation.
		/// </summary>
		void TestNegation(Vector2 a, Vector2 expected )
		{
			// This test asserts the following:
			//   -a == expected

			var result_1a = -a;

			Vector2 result_1b; Vector2.Negate(ref a, out result_1b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
		}

		/// <summary>
		/// Assert that, for known examples, all the negation opperators
		/// and functions yield the correct result.
		/// </summary>
		[Test]
		public void TestOperator_Negation_i ()
		{
			Single r = 3432;
			Single s = -6218;
			Single t = -3432;
			Single u = 6218;

			var a = new Vector2(r, s);
			var b = new Vector2(u, t);
			var c = new Vector2(t, u);
			var d = new Vector2(s, r);

			this.TestNegation(a, c);
			this.TestNegation(b, d);
		}

		/// <summary>
		/// Assert that, for known examples involving the zero vector, all the 
		/// negation opperators and functions yield the correct result.
		/// </summary>
		[Test]
		public void TestOperator_Negation_ii ()
		{
			Single t = -3432;
			Single u = 6218;
			Single r = 3432;
			Single s = -6218;

			var c = new Vector2(t, u);
			var d = new Vector2(s, r);

			this.TestNegation(c, Vector2.Zero - c);
			this.TestNegation(d, Vector2.Zero - d);
		}

		/// <summary>
		/// Assert that when negating the zero vector, all the 
		/// negation opperators and functions yield the correct result.
		/// </summary>
		[Test]
		public void TestOperator_Negation_iii ()
		{
			this.TestNegation(Vector2.Zero, Vector2.Zero);
		}

		/// <summary>
		/// Assert that, for a number of randomly generated scenarios, all the 
		/// negation opperators and functions yield the same results as a
		/// manual negation calculation.
		/// </summary>
		[Test]
		public void TestOperator_Negation_iv ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				this.TestNegation(a, Vector2.Zero - a);
			}
		}

		// Test Operator: Multiplication //-----------------------------------//

		/// <summary>
		/// Helper method for testing multiplication.
		/// </summary>
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

		/// <summary>
		/// Assert that, for a known example, all the multiplication opperators
		/// and functions yield the correct result.
		/// </summary>
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

		/// <summary>
		/// Assert that, for a number of randomly generated scenarios, all the 
		/// multiplication opperators and functions yield the same results as a
		/// manual multiplication calculation.
		/// </summary>
		[Test]
		public void TestOperator_Multiplication_ii ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				var b = GetNextRandomVector2();

				var c = new Vector2(a.X * b.X, a.Y * b.Y);

				this.TestMultiplication(a, b, c);
			}
		}


		// Test Operator: Division //-----------------------------------------//

		/// <summary>
		/// Helper method for testing division.
		/// </summary>
		void TestDivision(Vector2 a, Vector2 b, Vector2 expected )
		{
			// This test asserts the following:
			//   a / b == expected

			var result_1a = a / b;

			Vector2 result_1b; Vector2.Divide(ref a, ref b, out result_1b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
		}

		/// <summary>
		/// Assert that, for a known example using whole numbers, all the 
		/// division opperators and functions yield the correct result.
		/// </summary>
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

		/// <summary>
		/// Assert that, for a known example using fractional numbers, all the 
		/// division opperators and functions yield the correct result.
		/// </summary>
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

		/// <summary>
		/// Assert that, for a number of randomly generated scenarios, all the 
		/// division opperators and functions yield the same results as a
		/// manual addition division.
		/// </summary>
		[Test]
		public void TestOperator_Division_iii ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				var b = GetNextRandomVector2();

				var c = new Vector2(a.X / b.X, a.Y / b.Y);

				this.TestDivision(a, b, c);
			}
		}

		// Test Static Fn: SmoothStep //--------------------------------------//

		/// <summary>
		/// This test runs a number of random scenarios and makes sure that when
		/// the weighting parameter is at it's limits the spline passes directly 
		/// through the correct control points.
		/// </summary>
		[Test]
		public void TestStaticFn_SmoothStep_i ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				var b = GetNextRandomVector2();

				Single amount1 = 0;
				Vector2 result1;

				Vector2.SmoothStep (
					ref a, ref b, amount1, out result1);

				AssertEqualWithinReason(result1, a);

				Single amount2 = 1;
				Vector2 result2;

				Vector2.SmoothStep (
					ref a, ref b, amount2, out result2);

				AssertEqualWithinReason(result2, b);
			}
		}

		/// <summary>
		/// Ensure that an argument out of range exception is thrown if the 
		/// amount parameter is less than zero.
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_SmoothStep_ii()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();

			Single amount = -1;
			Vector2 result;

			Vector2.SmoothStep (
				ref a, ref b, amount, out result);
		}

		/// <summary>
		/// This test ensures that an argument out of range exception is thrown
		/// if the amount parameter is greater than one.
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_SmoothStep_iii()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();

			Single amount = 2;
			Vector2 result;

			Vector2.SmoothStep (
				ref a, ref b, amount, out result);
		}

		/// <summary>
		/// This tests compares results against a known example.
		/// </summary>
		[Test]
		public void TestStaticFn_SmoothStep_iv ()
		{
			var a = new Vector2( -30, -30 );
			var b = new Vector2( +30, +30 );

			Single one = 1;

			Single i; RealMaths.FromFraction(1755, 64, out i); // 27.421875
			Single j; RealMaths.FromFraction( 165,  8, out j); // 20.625
			Single k; RealMaths.FromFraction( 705, 64, out k); // 11.015625

			var knownResults = new List<Tuple<Single, Vector2>>
			{
				new Tuple<Single, Vector2>( 0, a ),
				new Tuple<Single, Vector2>( (one * 1) / 8, new Vector2( -i, -i ) ),
				new Tuple<Single, Vector2>( (one * 2) / 8, new Vector2( -j, -j ) ),
				new Tuple<Single, Vector2>( (one * 3) / 8, new Vector2( -k, -k ) ),
				new Tuple<Single, Vector2>( (one * 4) / 8, Vector2.Zero ),
				new Tuple<Single, Vector2>( (one * 5) / 8, new Vector2(  k,  k ) ),
				new Tuple<Single, Vector2>( (one * 6) / 8, new Vector2(  j,  j ) ),
				new Tuple<Single, Vector2>( (one * 7) / 8, new Vector2(  i,  i ) ),
				new Tuple<Single, Vector2>( 1, b ),
			};

			foreach(var knownResult in knownResults )
			{
				Vector2 result;

				Vector2.SmoothStep (
					ref a, ref b, knownResult.Item1, out result);

				AssertEqualWithinReason(result, knownResult.Item2);
			}
		}

		// Test Static Fn: CatmullRom //--------------------------------------//

		/// <summary>
		/// This test runs a number of random scenarios and makes sure that when
		/// the weighting parameter is at it's limits the spline passes directly 
		/// through the correct control points.
		/// </summary>
		[Test]
		public void TestStaticFn_CatmullRom_i ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				var b = GetNextRandomVector2();
				var c = GetNextRandomVector2();
				var d = GetNextRandomVector2();

				Single amount1 = 0;
				Vector2 result1;

				Vector2.CatmullRom (
					ref a, ref b, ref c, ref d, amount1, out result1);

				AssertEqualWithinReason(result1, b);

				Single amount2 = 1;
				Vector2 result2;

				Vector2.CatmullRom (
					ref a, ref b, ref c, ref d, amount2, out result2);

				AssertEqualWithinReason(result2, c);
			}
		}

		/// <summary>
		/// This tests compares results against a known example.
		/// </summary>
		[Test]
		public void TestStaticFn_CatmullRom_ii()
		{
			var a = new Vector2( -90, +30 );
			var b = new Vector2( -30, -30 );
			var c = new Vector2( +30, +30 );
			var d = new Vector2( +90, -30 );

			Single one = 1;

			Single u = 15;
			Single v = (Single) 165  / (Single)  8; // 20.5
			Single w = (Single) 45   / (Single)  2; // 20.625
			Single x = (Single) 1755 / (Single) 64; // 27.421875
			Single y = (Single) 15   / (Single)  2; // 14.5
			Single z = (Single) 705  / (Single) 64; // 11.015625

			var knownResults = new List<Tuple<Single, Vector2>>
			{
				new Tuple<Single, Vector2>( 0, b ),
				new Tuple<Single, Vector2>( one * 1 / 8, new Vector2( -w, -x ) ),
				new Tuple<Single, Vector2>( one * 2 / 8, new Vector2( -u, -v ) ),
				new Tuple<Single, Vector2>( one * 3 / 8, new Vector2( -y, -z ) ),
				new Tuple<Single, Vector2>( one * 4 / 8, Vector2.Zero ),
				new Tuple<Single, Vector2>( one * 5 / 8, new Vector2(  y,  z ) ),
				new Tuple<Single, Vector2>( one * 6 / 8, new Vector2(  u,  v ) ),
				new Tuple<Single, Vector2>( one * 7 / 8, new Vector2(  w,  x ) ),
				new Tuple<Single, Vector2>( 1, c ),
			};

			foreach(var knownResult in knownResults )
			{
				Vector2 result;

				Vector2.CatmullRom (
					ref a, ref b, ref c, ref d, knownResult.Item1, out result);

				AssertEqualWithinReason(result, knownResult.Item2);
			}
		}

		/// <summary>
		/// This test ensures that an argument out of range exception is thrown
		/// if the amount parameter is less than zero.
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_CatmullRom_iii()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();
			var c = GetNextRandomVector2();
			var d = GetNextRandomVector2();

			Single amount = -1;
			Vector2 result;

			Vector2.CatmullRom (
				ref a, ref b, ref c, ref d, amount, out result);
		}

		/// <summary>
		/// This test ensures that an argument out of range exception is thrown
		/// if the amount parameter is greater than one.
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_CatmullRom_iv()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();
			var c = GetNextRandomVector2();
			var d = GetNextRandomVector2();

			Single amount = 2;
			Vector2 result;

			Vector2.CatmullRom (
				ref a, ref b, ref c, ref d, amount, out result);
		}

		/// <summary>
		/// This tests compares results against an example where all the control
		/// points are in a straight line.  In this case the resulting spline
		/// should be a straight line.
		/// </summary>
		[Test]
		public void TestStaticFn_CatmullRom_v()
		{
			var a = new Vector2( -90, -90 );
			var b = new Vector2( -30, -30 );
			var c = new Vector2( +30, +30 );
			var d = new Vector2( +90, +90 );

			Single half; RealMaths.Half(out half);
			Single quarter = half / 2;
			Single threeQuarters = quarter * 3;

			var knownResults = new List<Tuple<Single, Vector2>>
			{
				new Tuple<Single, Vector2>( 0, b ),
				new Tuple<Single, Vector2>( quarter, new Vector2( -15, -15 ) ),
				new Tuple<Single, Vector2>( half, Vector2.Zero ),
				new Tuple<Single, Vector2>( threeQuarters, new Vector2( 15, 15 ) ),
				new Tuple<Single, Vector2>( 1, c ),
			};

			foreach(var knownResult in knownResults )
			{
				Vector2 result;

				Vector2.CatmullRom (
					ref a, ref b, ref c, ref d, knownResult.Item1, out result);

				AssertEqualWithinReason(result, knownResult.Item2);
			}
		}

		// Test Static Fn: Hermite //-----------------------------------------//

		/// <summary>
		/// This test runs a number of random scenarios and makes sure that when
		/// the weighting parameter is at it's limits the spline passes directly 
		/// through the correct control points.
		/// </summary>
		[Test]
		public void TestStaticFn_Hermite_i ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a  = GetNextRandomVector2();
				var b  = GetNextRandomVector2();

				var c = GetNextRandomVector2();
				var d = GetNextRandomVector2();

				Vector2 an; Vector2.Normalise(ref c, out an);
				Vector2 bn; Vector2.Normalise(ref d, out bn);

				Single amount1 = 0;
				Vector2 result1;

				Vector2.Hermite (
					ref a, ref an, ref b, ref bn, amount1, out result1);

				AssertEqualWithinReason(result1, b);

				Single amount2 = 1;
				Vector2 result2;

				Vector2.Hermite (
					ref a, ref an, ref b, ref bn, amount2, out result2);

				AssertEqualWithinReason(result2, c);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Hermite_ii ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Hermite_iii ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Hermite_iv ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Hermite_v ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

				/// <summary>
		/// Assert that, running the Min function on a number of randomly
		/// generated pairs of Vector2 objects, yields the same results as those
		/// obtained from performing a manual Min calculation.
		/// </summary>
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

		/// <summary>
		/// Assert that, running the Max function on a number of randomly
		/// generated pairs of Vector2 objects, yields the same results as those
		/// obtained from performing a manual Max calculation.
		/// </summary>
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

		/// <summary>
		/// Assert that, running the Clamp function on a number of randomly
		/// generated Vector2 objects for a given min-max range, yields
		/// results that fall within that range.
		/// </summary>
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

		/// <summary>
		/// Assert that, running the Clamp function on an a Vector2 object known
		/// to fall outside of a given min-max range, yields a result that fall 
		/// within that range.
		/// </summary>
		[Test]
		public void TestStaticFn_Clamp_ii ()
		{
			Vector2 min = new Vector2(-30, 1);
			Vector2 max = new Vector2(32, 130);

			Vector2 a = new Vector2(-1, 13);

			Vector2 expected = a;

			Vector2 result; Vector2.Clamp (ref a, ref min, ref max, out result);

			Assert.That(result.X, Is.LessThanOrEqualTo(max.X));
			Assert.That(result.Y, Is.LessThanOrEqualTo(max.Y));
			Assert.That(result.X, Is.GreaterThanOrEqualTo(min.X));
			Assert.That(result.Y, Is.GreaterThanOrEqualTo(min.Y));

			Assert.That(a, Is.EqualTo(expected));

		}

		/// <summary>
		/// Assert that, running the Lerp function on a number of randomly
		/// generated pairs of Vector2 objects for a range of weighting amounts, 
		/// yields the same results as those obtained from performing a manual 
		/// Lerp calculation.
		/// </summary>
		[Test]
		public void TestStaticFn_Lerp_i ()
		{
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

					AssertEqualWithinReason(result, expected);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Lerp_ii()
		{
			Single delta = 2;
			Vector2 a = GetNextRandomVector2();
			Vector2 b = GetNextRandomVector2();
			Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);
		}

		/// <summary>
		/// 
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Lerp_iii()
		{
			Single delta = -1;
			Vector2 a = GetNextRandomVector2();
			Vector2 b = GetNextRandomVector2();
			Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);
		}

		/// <summary>
		/// 
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Lerp_iv()
		{
			Single delta; RealMaths.Half(out delta);

			delta = -delta;

			Vector2 a = GetNextRandomVector2();
			Vector2 b = GetNextRandomVector2();
			Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);
		}


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
	public class Vector2Tests
	{
				/// <summary>
		/// The random number generator used for testing.
		/// </summary>
		static readonly System.Random rand;

		/// <summary>
		/// Static constructor used to ensure that the random number generator
		/// always gets initilised with the same seed, making the tests
		/// behave in a deterministic manner.
		/// </summary>
		static Vector2Tests()
		{
			rand = new System.Random(0);
		}

		/// <summary>
		/// Helper function for getting the next random Double value.
		/// </summary>
		static Double GetNextRandomDouble()
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

		/// <summary>
		/// Helper function for getting the next random Vector2.
		/// </summary>
		static Vector2 GetNextRandomVector2()
		{
			Double a = GetNextRandomDouble();
			Double b = GetNextRandomDouble();

			return new Vector2(a, b);
		}

		/// <summary>
		/// Helper to encapsulate asserting that two vectors are equal.
		/// </summary>
		static void AssertEqualWithinReason(Vector2 a, Vector2 b)
		{
			Double tolerance; RealMaths.TestTolerance(out tolerance);

			Assert.That(a.X, Is.EqualTo(b.X).Within(tolerance));
			Assert.That(a.Y, Is.EqualTo(b.Y).Within(tolerance));
		}
		

		// Test: StructLayout //----------------------------------------------//

		/// <summary>
		/// This test makes sure that the struct layout has been defined
		/// correctly and that X and Y are in the correct order and the only
		/// member variables using reflection.
		/// </summary>
		[Test]
		public void Test_StructLayout_i ()
		{
			Type t = typeof(Vector2);
			
			Assert.That(false, Is.EqualTo(true));
		}

		/// <summary>
		/// This test makes sure that when examining the memory addresses of the 
		/// X and Y member variables of a number of randomly generated Vector2
		/// object the results are as expected. 
		/// </summary>
		[Test]
		public unsafe void Test_StructLayout_ii ()
		{
			for( Int32 i = 0; i < 100; ++ i)
			{
				Vector2 vec = GetNextRandomVector2();

				GCHandle h_vec = GCHandle.Alloc(vec, GCHandleType.Pinned);
				GCHandle h_x = GCHandle.Alloc(vec.X, GCHandleType.Pinned);
				GCHandle h_y = GCHandle.Alloc(vec.Y, GCHandleType.Pinned);

		        IntPtr vecAddress = h_vec.AddrOfPinnedObject();
		        IntPtr resultX = h_x.AddrOfPinnedObject();
		        IntPtr resultY = h_y.AddrOfPinnedObject();

		        String strVecAddress = "0x" + vecAddress.ToString("x");
		        String strResultX = "0x" + resultX.ToString("x");
		        String strResultY = "0x" + resultY.ToString("x");

		        Console.WriteLine("&v: " + strVecAddress);
		        Console.WriteLine("&x: " + strResultX);
		        Console.WriteLine("&y: " + strResultY);

		        Assert.That(resultX, Is.EqualTo(vecAddress));

		        IntPtr q = IntPtr.Add(resultX, sizeof(Double));

		        Assert.That(resultY, Is.EqualTo(q));
				
		        h_vec.Free();
		        h_x.Free();
		        h_y.Free();
			}
		}

		// Test: Constructors //----------------------------------------------//

		/// <summary>
		/// This test goes though each public constuctor and ensures that the 
		/// data members of the structure have been properly set.
		/// </summary>
		[Test]
		public void Test_Constructors_i ()
		{
			// Test default values
			Vector2 a = new Vector2();
			Assert.That(a, Is.EqualTo(Vector2.Zero));

			// Test Vector2( n ) where n is Double
			Double u = -189;
			Double v = 429;

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

			// Test no constructor
			Vector2 e;
			e.X = 0;
			e.Y = 0;
			Assert.That(e, Is.EqualTo(Vector2.Zero));
		}

		// Test Member Fn: ToString //----------------------------------------//

		/// <summary>
		/// For a given example, this test ensures that the ToString function
		/// yields the expected string.
		/// </summary>
		[Test]
		public void TestMemberFn_ToString_i ()
		{
			Vector2 a = new Vector2(42, -17);

			String result = a.ToString();

			String expected = "{X:42 Y:-17}";

			Assert.That(result, Is.EqualTo(expected));
		}

		// Test Member Fn: GetHashCode //-------------------------------------//

		/// <summary>
		/// Makes sure that the hashing function is good by testing 10,000
		/// random scenarios and ensuring that there are no more than 10
		/// collisions.
		/// </summary>
		[Test]
		public void TestMemberFn_GetHashCode_i ()
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

		// Test Member Fn: Length //------------------------------------------//

		/// <summary>
		/// Tests that for a known example the Length member function yields
		/// the correct result.
		/// </summary>
		[Test]
		public void TestMemberFn_Length_i ()
		{
			Vector2 a = new Vector2(30, -40);

			Double expected = 50;

			Double result = a.Length();

			Assert.That(result, Is.EqualTo(expected));
		}

		// Test Member Fn: LengthSquared //-----------------------------------//

		/// <summary>
		/// Tests that for a known example the LengthSquared member function 
		/// yields the correct result.
		/// </summary>
		[Test]
		public void TestMemberFn_LengthSquared_i ()
		{
			Vector2 a = new Vector2(30, -40);

			Double expected = 2500;

			Double result = a.LengthSquared();

			Assert.That(result, Is.EqualTo(expected));
		}

		// Test Member Fn: IsUnit //------------------------------------------//

		/// <summary>
		/// Tests that for the most simple unit vectors the IsUnit member 
		/// function returns the correct result of TRUE.
		/// </summary>
		[Test]
		public void TestMemberFn_IsUnit_i ()
		{
			Assert.That(new Vector2( 1,  0).IsUnit(), Is.EqualTo(true));
			Assert.That(new Vector2(-1,  0).IsUnit(), Is.EqualTo(true));
			Assert.That(new Vector2( 1,  1).IsUnit(), Is.EqualTo(false));
			Assert.That(new Vector2( 0,  0).IsUnit(), Is.EqualTo(false));
			Assert.That(new Vector2( 0, -1).IsUnit(), Is.EqualTo(true));
			Assert.That(new Vector2( 0,  1).IsUnit(), Is.EqualTo(true));
		}

		/// <summary>
		/// This test makes sure that the IsUnit member function returns the 
		/// correct result of TRUE for a number of scenarios where the test 
		/// vector is both random and normalised.
		/// </summary>
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

		/// <summary>
		/// This test ensures that the IsUnit member function correctly
		/// returns TRUE for a collection of vectors, all known to be of unit 
		/// length.
		/// </summary>
		[Test]
		public void TestMemberFn_IsUnit_iii ()
		{
			Double piOver2; RealMaths.PiOver2(out piOver2);

			for( Int32 i = 0; i <= 90; ++ i)
			{
				Double theta = piOver2 / 90 * i;

				Double opposite = RealMaths.Sin(theta);
				Double adjacent = RealMaths.Cos(theta);				

				Assert.That(
					new Vector2( opposite,  adjacent).IsUnit(), 
					Is.EqualTo(true));
				
				Assert.That(
					new Vector2( opposite, -adjacent).IsUnit(), 
					Is.EqualTo(true));
				
				Assert.That(
					new Vector2(-opposite,  adjacent).IsUnit(), 
					Is.EqualTo(true));
				
				Assert.That(
					new Vector2(-opposite, -adjacent).IsUnit(), 
					Is.EqualTo(true));
			}
		}

		/// <summary>
		/// This test makes sure that the IsUnit member function returns the 
		/// correct result of FALSE for a number of scenarios where the test 
		/// vector is randomly generated and not normalised.  It's highly
		/// unlikely that the random generator will create a unit vector!
		/// </summary>
		[Test]
		public void TestMemberFn_IsUnit_iv ()
		{
			for( Int32 i = 0; i < 100; ++ i)
			{
				Vector2 a = GetNextRandomVector2();

				Assert.That(a.IsUnit(), Is.EqualTo(false));
			}
		}
			
		// Test Constant: Zero //---------------------------------------------//

		/// <summary>
		/// Tests to make sure that a Vector2 initilised using the Zero constant
		/// has it's member variables correctly set.
		/// </summary>
		[Test]
		public void TestConstant_Zero ()
		{
			Double zero = 0;
			var v_zero = Vector2.Zero;

			Assert.That(v_zero.X, Is.EqualTo(zero));
			Assert.That(v_zero.Y, Is.EqualTo(zero));

			Assert.That(v_zero, Is.EqualTo(new Vector2(zero, zero)));
		}

		// Test Constant: One //----------------------------------------------//

		/// <summary>
		/// Tests to make sure that a Vector2 initilised using the One constant
		/// has it's member variables correctly set.
		/// </summary>
		[Test]
		public void TestConstant_One ()
		{
			Double one = 1;
			var v_one = Vector2.One;

			Assert.That(v_one.X, Is.EqualTo(one));
			Assert.That(v_one.Y, Is.EqualTo(one));

			Assert.That(v_one, Is.EqualTo(new Vector2(one, one)));
		}

		// Test Constant: UnitX //--------------------------------------------//

		/// <summary>
		/// Tests to make sure that a Vector2 initilised using the UnitX 
		/// constant has it's member variables correctly set.
		/// </summary>
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

		// Test Constant: UnitY //--------------------------------------------//

		/// <summary>
		/// Tests to make sure that a Vector2 initilised using the UnitY
		/// constant has it's member variables correctly set.
		/// </summary>
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

		// Test Static Fn: Distance //----------------------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Distance_i ()
		{
			Vector2 a = new Vector2(0, 4);
			Vector2 b = new Vector2(3, 0);

			Double expected = 5;
			Double result; Vector2.Distance(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Distance_ii ()
		{
			Vector2 a = new Vector2(0, -4);
			Vector2 b = new Vector2(3, 0);

			Double expected = 5;
			Double result; Vector2.Distance(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Distance_iii ()
		{
			Vector2 a = new Vector2(0, -4);
			Vector2 b = new Vector2(-3, 0);

			Double expected = 5;
			Double result; Vector2.Distance(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Distance_iv ()
		{
			Vector2 a = Vector2.Zero;

			Double expected = 0;

			Assert.That(a.Length(), Is.EqualTo(expected));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Distance_v ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				
				Double expected = 
					RealMaths.Sqrt((a.X * a.X) + (a.Y * a.Y));

				Assert.That(a.Length(), Is.EqualTo(expected));
			}
		}

		// Test Static Fn: DistanceSquared //---------------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_DistanceSquared_i ()
		{
			Vector2 a = new Vector2(0, 4);
			Vector2 b = new Vector2(3, 0);

			Double expected = 25;
			Double result;
			Vector2.DistanceSquared(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_DistanceSquared_ii ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				Vector2 a = GetNextRandomVector2();
				Vector2 b = GetNextRandomVector2();
				Vector2 c = b - a;
				Double expected = (c.X * c.X) + (c.Y * c.Y);
				Double result;
				Vector2.DistanceSquared(ref a, ref b, out result);

				Assert.That(result, Is.EqualTo(expected));
			}
		}

		// Test Static Fn: Dot //---------------------------------------------//

		/// <summary>
		/// 
		/// </summary>
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

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Dot_ii ()
		{
			Vector2 a = new Vector2(1, 0);
			Vector2 b = new Vector2(-1, 0);

			Double expected = -1;
			Double result; Vector2.Dot(ref a, ref b, out result);

			Assert.That(result, Is.EqualTo(expected));
		}

		/// <summary>
		/// 
		/// </summary>
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

		// Test Static Fn: Normalise //---------------------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Normalise_i()
		{
			Vector2 a = Vector2.Zero;

			Vector2 b; Vector2.Normalise(ref a, out b);
		}

		/// <summary>
		/// 
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Normalise_ii()
		{
			Vector2 a = new Vector2(
				Double.MaxValue, 
				Double.MaxValue);

			Vector2 b; Vector2.Normalise(ref a, out b);
		}

		/// <summary>
		/// 
		/// </summary>
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

		// Test Static Fn: Reflect //-----------------------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Reflect_i ()
		{
			Assert.That (true, Is.EqualTo (false));
		}

		// Test Static Fn: TransformMatrix44 //-------------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_TransformMatix44_i ()
		{
			Assert.That (true, Is.EqualTo (false));
		}

		// Test Static Fn: TransformNormal //---------------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_TransformNormal_i ()
		{
			Assert.That (true, Is.EqualTo (false));
		}

		// Test Static Fn: TransformQuaternion //-----------------------------//

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_TransformQuaternion_i ()
		{
			Assert.That (true, Is.EqualTo (false));
		}

		// Test Operator: Equality //-----------------------------------------//

		/// <summary>
		/// Helper method for testing equality.
		/// </summary>
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

		/// <summary>
		/// Makes sure that, for a known example, all the equality opperators
		/// and functions yield the expected result of TRUE when two equal  
		/// Vector2 objects are compared.
		/// </summary>
		[Test]
		public void TestOperator_Equality_i ()
		{
			var a = new Vector2(44, -54);
			var b = new Vector2(44, -54);

			Boolean expected = true;

			this.TestEquality(a, b, expected);
		}

		/// <summary>
		/// Makes sure that, for a known example, all the equality opperators
		/// and functions yield the expected result of FALSE when two unequal  
		/// Vector2 objects are compared.
		/// </summary>
		[Test]
		public void TestOperator_Equality_ii ()
		{
			var a = new Vector2(44, 54);
			var b = new Vector2(44, -54);

			Boolean expected = false;

			this.TestEquality(a, b, expected);
		}

		/// <summary>
		/// Tests to make sure that all the equality opperators and functions 
		/// yield the expected result of TRUE when used on a number of randomly 
		/// generated pairs of equal Vector2 objects.
		/// </summary>
		[Test]
		public void TestOperator_Equality_iii ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();

				Vector2 b = a;

				this.TestEquality(a, b, true);
			}
		}


		// Test Operator: Addition //-----------------------------------------//

		/// <summary>
		/// Helper method for testing addition.
		/// </summary>
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
		/// Assert that, for a known example, all the addition opperators
		/// and functions yield the correct result.
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
		/// Assert that, for a known example involving the zero vector, all the 
		/// addition opperators and functions yield the correct result.
		/// </summary>
		[Test]
		public void TestOperator_Addition_ii ()
		{
			var a = new Vector2(-2313, 88);

			var expected = a;

			this.TestAddition(a, Vector2.Zero, expected);
		}

		/// <summary>
		/// Assert that, for a known example involving two zero vectors, all the 
		/// addition opperators and functions yield the correct result of zero.
		/// </summary>
		[Test]
		public void TestOperator_Addition_iii ()
		{
			this.TestAddition(Vector2.Zero, Vector2.Zero, Vector2.Zero);
		}

		/// <summary>
		/// Assert that, for a number of randomly generated scenarios, all the 
		/// addition opperators and functions yield the same results as a
		/// manual addition calculation.
		/// </summary>
		[Test]
		public void TestOperator_Addition_iv ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				var b = GetNextRandomVector2();

				var expected = new Vector2(a.X + b.X, a.Y + b.Y);

				this.TestAddition(a, b, expected);
			}
		}

		// Test Operator: Subtraction //--------------------------------------//
		
		/// <summary>
		/// Helper method for testing subtraction.
		/// </summary>
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
		/// Assert that, for known examples, all the subtraction opperators
		/// and functions yield the correct result.
		/// <summary>
		[Test]
		public void TestOperator_Subtraction_i ()
		{
			var a = new Vector2(12, -4);
			var b = new Vector2(15, 11);
			var c = new Vector2(-423, 342);

			var expected = new Vector2(-3, -15);

			this.TestSubtraction(a, b, expected);
			this.TestSubtraction(c, Vector2.Zero, c);
		}

		/// <summary>
		/// Assert that when subtracting the zero vector fromt the zero vector, 
		/// all the subtraction opperators and functions yield the correct 
		/// result.
		/// <summary>
		[Test]
		public void TestOperator_Subtraction_ii ()
		{
			this.TestSubtraction(Vector2.Zero, Vector2.Zero, Vector2.Zero);
		}

		/// <summary>
		/// Assert that, for a number of randomly generated scenarios, all the 
		/// subtraction opperators and functions yield the same results as a
		/// manual subtraction calculation.
		/// </summary>
		[Test]
		public void TestOperator_Subtraction_iii ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				var b = GetNextRandomVector2();

				var expected = new Vector2(a.X - b.X, a.Y - b.Y);

				this.TestSubtraction(a, b, expected);
			}
		}

		// Test Operator: Negation //-----------------------------------------//
		
		/// <summary>
		/// Helper method for testing negation.
		/// </summary>
		void TestNegation(Vector2 a, Vector2 expected )
		{
			// This test asserts the following:
			//   -a == expected

			var result_1a = -a;

			Vector2 result_1b; Vector2.Negate(ref a, out result_1b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
		}

		/// <summary>
		/// Assert that, for known examples, all the negation opperators
		/// and functions yield the correct result.
		/// </summary>
		[Test]
		public void TestOperator_Negation_i ()
		{
			Double r = 3432;
			Double s = -6218;
			Double t = -3432;
			Double u = 6218;

			var a = new Vector2(r, s);
			var b = new Vector2(u, t);
			var c = new Vector2(t, u);
			var d = new Vector2(s, r);

			this.TestNegation(a, c);
			this.TestNegation(b, d);
		}

		/// <summary>
		/// Assert that, for known examples involving the zero vector, all the 
		/// negation opperators and functions yield the correct result.
		/// </summary>
		[Test]
		public void TestOperator_Negation_ii ()
		{
			Double t = -3432;
			Double u = 6218;
			Double r = 3432;
			Double s = -6218;

			var c = new Vector2(t, u);
			var d = new Vector2(s, r);

			this.TestNegation(c, Vector2.Zero - c);
			this.TestNegation(d, Vector2.Zero - d);
		}

		/// <summary>
		/// Assert that when negating the zero vector, all the 
		/// negation opperators and functions yield the correct result.
		/// </summary>
		[Test]
		public void TestOperator_Negation_iii ()
		{
			this.TestNegation(Vector2.Zero, Vector2.Zero);
		}

		/// <summary>
		/// Assert that, for a number of randomly generated scenarios, all the 
		/// negation opperators and functions yield the same results as a
		/// manual negation calculation.
		/// </summary>
		[Test]
		public void TestOperator_Negation_iv ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				this.TestNegation(a, Vector2.Zero - a);
			}
		}

		// Test Operator: Multiplication //-----------------------------------//

		/// <summary>
		/// Helper method for testing multiplication.
		/// </summary>
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

		/// <summary>
		/// Assert that, for a known example, all the multiplication opperators
		/// and functions yield the correct result.
		/// </summary>
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

		/// <summary>
		/// Assert that, for a number of randomly generated scenarios, all the 
		/// multiplication opperators and functions yield the same results as a
		/// manual multiplication calculation.
		/// </summary>
		[Test]
		public void TestOperator_Multiplication_ii ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				var b = GetNextRandomVector2();

				var c = new Vector2(a.X * b.X, a.Y * b.Y);

				this.TestMultiplication(a, b, c);
			}
		}


		// Test Operator: Division //-----------------------------------------//

		/// <summary>
		/// Helper method for testing division.
		/// </summary>
		void TestDivision(Vector2 a, Vector2 b, Vector2 expected )
		{
			// This test asserts the following:
			//   a / b == expected

			var result_1a = a / b;

			Vector2 result_1b; Vector2.Divide(ref a, ref b, out result_1b);
			
			Assert.That(result_1a, Is.EqualTo(expected));
			Assert.That(result_1b, Is.EqualTo(expected));
		}

		/// <summary>
		/// Assert that, for a known example using whole numbers, all the 
		/// division opperators and functions yield the correct result.
		/// </summary>
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

		/// <summary>
		/// Assert that, for a known example using fractional numbers, all the 
		/// division opperators and functions yield the correct result.
		/// </summary>
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

		/// <summary>
		/// Assert that, for a number of randomly generated scenarios, all the 
		/// division opperators and functions yield the same results as a
		/// manual addition division.
		/// </summary>
		[Test]
		public void TestOperator_Division_iii ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				var b = GetNextRandomVector2();

				var c = new Vector2(a.X / b.X, a.Y / b.Y);

				this.TestDivision(a, b, c);
			}
		}

		// Test Static Fn: SmoothStep //--------------------------------------//

		/// <summary>
		/// This test runs a number of random scenarios and makes sure that when
		/// the weighting parameter is at it's limits the spline passes directly 
		/// through the correct control points.
		/// </summary>
		[Test]
		public void TestStaticFn_SmoothStep_i ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				var b = GetNextRandomVector2();

				Double amount1 = 0;
				Vector2 result1;

				Vector2.SmoothStep (
					ref a, ref b, amount1, out result1);

				AssertEqualWithinReason(result1, a);

				Double amount2 = 1;
				Vector2 result2;

				Vector2.SmoothStep (
					ref a, ref b, amount2, out result2);

				AssertEqualWithinReason(result2, b);
			}
		}

		/// <summary>
		/// Ensure that an argument out of range exception is thrown if the 
		/// amount parameter is less than zero.
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_SmoothStep_ii()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();

			Double amount = -1;
			Vector2 result;

			Vector2.SmoothStep (
				ref a, ref b, amount, out result);
		}

		/// <summary>
		/// This test ensures that an argument out of range exception is thrown
		/// if the amount parameter is greater than one.
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_SmoothStep_iii()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();

			Double amount = 2;
			Vector2 result;

			Vector2.SmoothStep (
				ref a, ref b, amount, out result);
		}

		/// <summary>
		/// This tests compares results against a known example.
		/// </summary>
		[Test]
		public void TestStaticFn_SmoothStep_iv ()
		{
			var a = new Vector2( -30, -30 );
			var b = new Vector2( +30, +30 );

			Double one = 1;

			Double i; RealMaths.FromFraction(1755, 64, out i); // 27.421875
			Double j; RealMaths.FromFraction( 165,  8, out j); // 20.625
			Double k; RealMaths.FromFraction( 705, 64, out k); // 11.015625

			var knownResults = new List<Tuple<Double, Vector2>>
			{
				new Tuple<Double, Vector2>( 0, a ),
				new Tuple<Double, Vector2>( (one * 1) / 8, new Vector2( -i, -i ) ),
				new Tuple<Double, Vector2>( (one * 2) / 8, new Vector2( -j, -j ) ),
				new Tuple<Double, Vector2>( (one * 3) / 8, new Vector2( -k, -k ) ),
				new Tuple<Double, Vector2>( (one * 4) / 8, Vector2.Zero ),
				new Tuple<Double, Vector2>( (one * 5) / 8, new Vector2(  k,  k ) ),
				new Tuple<Double, Vector2>( (one * 6) / 8, new Vector2(  j,  j ) ),
				new Tuple<Double, Vector2>( (one * 7) / 8, new Vector2(  i,  i ) ),
				new Tuple<Double, Vector2>( 1, b ),
			};

			foreach(var knownResult in knownResults )
			{
				Vector2 result;

				Vector2.SmoothStep (
					ref a, ref b, knownResult.Item1, out result);

				AssertEqualWithinReason(result, knownResult.Item2);
			}
		}

		// Test Static Fn: CatmullRom //--------------------------------------//

		/// <summary>
		/// This test runs a number of random scenarios and makes sure that when
		/// the weighting parameter is at it's limits the spline passes directly 
		/// through the correct control points.
		/// </summary>
		[Test]
		public void TestStaticFn_CatmullRom_i ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a = GetNextRandomVector2();
				var b = GetNextRandomVector2();
				var c = GetNextRandomVector2();
				var d = GetNextRandomVector2();

				Double amount1 = 0;
				Vector2 result1;

				Vector2.CatmullRom (
					ref a, ref b, ref c, ref d, amount1, out result1);

				AssertEqualWithinReason(result1, b);

				Double amount2 = 1;
				Vector2 result2;

				Vector2.CatmullRom (
					ref a, ref b, ref c, ref d, amount2, out result2);

				AssertEqualWithinReason(result2, c);
			}
		}

		/// <summary>
		/// This tests compares results against a known example.
		/// </summary>
		[Test]
		public void TestStaticFn_CatmullRom_ii()
		{
			var a = new Vector2( -90, +30 );
			var b = new Vector2( -30, -30 );
			var c = new Vector2( +30, +30 );
			var d = new Vector2( +90, -30 );

			Double one = 1;

			Double u = 15;
			Double v = (Double) 165  / (Double)  8; // 20.5
			Double w = (Double) 45   / (Double)  2; // 20.625
			Double x = (Double) 1755 / (Double) 64; // 27.421875
			Double y = (Double) 15   / (Double)  2; // 14.5
			Double z = (Double) 705  / (Double) 64; // 11.015625

			var knownResults = new List<Tuple<Double, Vector2>>
			{
				new Tuple<Double, Vector2>( 0, b ),
				new Tuple<Double, Vector2>( one * 1 / 8, new Vector2( -w, -x ) ),
				new Tuple<Double, Vector2>( one * 2 / 8, new Vector2( -u, -v ) ),
				new Tuple<Double, Vector2>( one * 3 / 8, new Vector2( -y, -z ) ),
				new Tuple<Double, Vector2>( one * 4 / 8, Vector2.Zero ),
				new Tuple<Double, Vector2>( one * 5 / 8, new Vector2(  y,  z ) ),
				new Tuple<Double, Vector2>( one * 6 / 8, new Vector2(  u,  v ) ),
				new Tuple<Double, Vector2>( one * 7 / 8, new Vector2(  w,  x ) ),
				new Tuple<Double, Vector2>( 1, c ),
			};

			foreach(var knownResult in knownResults )
			{
				Vector2 result;

				Vector2.CatmullRom (
					ref a, ref b, ref c, ref d, knownResult.Item1, out result);

				AssertEqualWithinReason(result, knownResult.Item2);
			}
		}

		/// <summary>
		/// This test ensures that an argument out of range exception is thrown
		/// if the amount parameter is less than zero.
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_CatmullRom_iii()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();
			var c = GetNextRandomVector2();
			var d = GetNextRandomVector2();

			Double amount = -1;
			Vector2 result;

			Vector2.CatmullRom (
				ref a, ref b, ref c, ref d, amount, out result);
		}

		/// <summary>
		/// This test ensures that an argument out of range exception is thrown
		/// if the amount parameter is greater than one.
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_CatmullRom_iv()
		{
			var a = GetNextRandomVector2();
			var b = GetNextRandomVector2();
			var c = GetNextRandomVector2();
			var d = GetNextRandomVector2();

			Double amount = 2;
			Vector2 result;

			Vector2.CatmullRom (
				ref a, ref b, ref c, ref d, amount, out result);
		}

		/// <summary>
		/// This tests compares results against an example where all the control
		/// points are in a straight line.  In this case the resulting spline
		/// should be a straight line.
		/// </summary>
		[Test]
		public void TestStaticFn_CatmullRom_v()
		{
			var a = new Vector2( -90, -90 );
			var b = new Vector2( -30, -30 );
			var c = new Vector2( +30, +30 );
			var d = new Vector2( +90, +90 );

			Double half; RealMaths.Half(out half);
			Double quarter = half / 2;
			Double threeQuarters = quarter * 3;

			var knownResults = new List<Tuple<Double, Vector2>>
			{
				new Tuple<Double, Vector2>( 0, b ),
				new Tuple<Double, Vector2>( quarter, new Vector2( -15, -15 ) ),
				new Tuple<Double, Vector2>( half, Vector2.Zero ),
				new Tuple<Double, Vector2>( threeQuarters, new Vector2( 15, 15 ) ),
				new Tuple<Double, Vector2>( 1, c ),
			};

			foreach(var knownResult in knownResults )
			{
				Vector2 result;

				Vector2.CatmullRom (
					ref a, ref b, ref c, ref d, knownResult.Item1, out result);

				AssertEqualWithinReason(result, knownResult.Item2);
			}
		}

		// Test Static Fn: Hermite //-----------------------------------------//

		/// <summary>
		/// This test runs a number of random scenarios and makes sure that when
		/// the weighting parameter is at it's limits the spline passes directly 
		/// through the correct control points.
		/// </summary>
		[Test]
		public void TestStaticFn_Hermite_i ()
		{
			for(Int32 i = 0; i < 100; ++i)
			{
				var a  = GetNextRandomVector2();
				var b  = GetNextRandomVector2();

				var c = GetNextRandomVector2();
				var d = GetNextRandomVector2();

				Vector2 an; Vector2.Normalise(ref c, out an);
				Vector2 bn; Vector2.Normalise(ref d, out bn);

				Double amount1 = 0;
				Vector2 result1;

				Vector2.Hermite (
					ref a, ref an, ref b, ref bn, amount1, out result1);

				AssertEqualWithinReason(result1, b);

				Double amount2 = 1;
				Vector2 result2;

				Vector2.Hermite (
					ref a, ref an, ref b, ref bn, amount2, out result2);

				AssertEqualWithinReason(result2, c);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Hermite_ii ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Hermite_iii ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Hermite_iv ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestStaticFn_Hermite_v ()
		{
			Assert.That(true, Is.EqualTo(false));
		}

				/// <summary>
		/// Assert that, running the Min function on a number of randomly
		/// generated pairs of Vector2 objects, yields the same results as those
		/// obtained from performing a manual Min calculation.
		/// </summary>
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

		/// <summary>
		/// Assert that, running the Max function on a number of randomly
		/// generated pairs of Vector2 objects, yields the same results as those
		/// obtained from performing a manual Max calculation.
		/// </summary>
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

		/// <summary>
		/// Assert that, running the Clamp function on a number of randomly
		/// generated Vector2 objects for a given min-max range, yields
		/// results that fall within that range.
		/// </summary>
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

		/// <summary>
		/// Assert that, running the Clamp function on an a Vector2 object known
		/// to fall outside of a given min-max range, yields a result that fall 
		/// within that range.
		/// </summary>
		[Test]
		public void TestStaticFn_Clamp_ii ()
		{
			Vector2 min = new Vector2(-30, 1);
			Vector2 max = new Vector2(32, 130);

			Vector2 a = new Vector2(-1, 13);

			Vector2 expected = a;

			Vector2 result; Vector2.Clamp (ref a, ref min, ref max, out result);

			Assert.That(result.X, Is.LessThanOrEqualTo(max.X));
			Assert.That(result.Y, Is.LessThanOrEqualTo(max.Y));
			Assert.That(result.X, Is.GreaterThanOrEqualTo(min.X));
			Assert.That(result.Y, Is.GreaterThanOrEqualTo(min.Y));

			Assert.That(a, Is.EqualTo(expected));

		}

		/// <summary>
		/// Assert that, running the Lerp function on a number of randomly
		/// generated pairs of Vector2 objects for a range of weighting amounts, 
		/// yields the same results as those obtained from performing a manual 
		/// Lerp calculation.
		/// </summary>
		[Test]
		public void TestStaticFn_Lerp_i ()
		{
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

					AssertEqualWithinReason(result, expected);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Lerp_ii()
		{
			Double delta = 2;
			Vector2 a = GetNextRandomVector2();
			Vector2 b = GetNextRandomVector2();
			Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);
		}

		/// <summary>
		/// 
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Lerp_iii()
		{
			Double delta = -1;
			Vector2 a = GetNextRandomVector2();
			Vector2 b = GetNextRandomVector2();
			Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);
		}

		/// <summary>
		/// 
		/// </summary>
		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestStaticFn_Lerp_iv()
		{
			Double delta; RealMaths.Half(out delta);

			delta = -delta;

			Vector2 a = GetNextRandomVector2();
			Vector2 b = GetNextRandomVector2();
			Vector2 result; Vector2.Lerp (ref a, ref b, delta, out result);
		}


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

