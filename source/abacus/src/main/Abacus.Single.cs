// ┌────────────────────────────────────────────────────────────────────────┐ \\
// │    _____ ___.                                                          │ \\
// │   /  _  \\_ |__ _____    ____  __ __  ______                           │ \\
// │  /  /_\  \| __ \\__  \ _/ ___\|  |  \/  ___/                           │ \\
// │ /    |    \ \_\ \/ __ \\  \___|  |  /\___ \                            │ \\
// │ \____|__  /___  (____  /\___  >____//____  >                           │ \\
// │         \/    \/     \/     \/           \/  v1.0.1                    │ \\
// │                                                                        │ \\
// │ Fast, efficient, cross platform, cross precision, maths library.       │ \\
// │                                                                        │ \\
// │        ________________________________________________________        │ \\
// │       /  ____________________________________________________  \       │ \\
// │       | |<_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> | |       │ \\
// │       | |<_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> | |       │ \\
// │       | |_|___|___|___|___|___|___|___|___|___|___|___|___|__| |       │ \\
// │       |  ____________________________________________________  |       │ \\
// │       | | |   |   |   |   |   |   |   |   |   |   |   |   |  | |       │ \\
// │       | |<_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> | |       │ \\
// │       | |<_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> | |       │ \\
// │       | |<_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> | |       │ \\
// │       | |<_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> <_> | |       │ \\
// │       | |<_>_<_>_<_>_<_>_<_>_<_>_<_>_<_>_<_>_<_>_<_>_<_>_<_>_| |       │ \\
// │       \________________________________________________________/       │ \\
// │                                                                        │ \\
// ├────────────────────────────────────────────────────────────────────────┤ \\
// │ Copyright © 2012 - 2020 Ash Pook                                       │ \\
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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Abacus.SinglePrecision
{
    /// <summary>
    /// Single precision Quaternion.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Quaternion : IEquatable<Quaternion> {
        public Single I, J, K, U;

        public Quaternion (Single i, Single j, Single k, Single u) { I = i; J = j; K = k; U = u; }

        public Quaternion (Vector3 vectorPart, Single scalarPart) { I = vectorPart.X; J = vectorPart.Y; K = vectorPart.Z; U = scalarPart; }

        public override String ToString () { return String.Format ("(I:{0}, J:{1}, K:{2}, U:{3})", I, J, K, U); }

        public override Int32 GetHashCode () {
            return U.GetHashCode ().ShiftAndWrap (6) ^ K.GetHashCode ().ShiftAndWrap (4)
                 ^ J.GetHashCode ().ShiftAndWrap (2) ^ I.GetHashCode ();
        }

        public override Boolean Equals (Object obj) { return (obj is Quaternion) ? this.Equals ((Quaternion) obj) : false; }

        public Boolean Equals (Quaternion other) {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        // Constants //-------------------------------------------------------//

        static Quaternion identity, zero;

        static Quaternion () {
            identity = new Quaternion (0, 0, 0, 1);
            zero     = new Quaternion (0, 0, 0, 0);
        }

        public static Quaternion Identity { get { return identity; } }
        public static Quaternion Zero     { get { return zero; } }

        // Operators //-------------------------------------------------------//

        public static void Equals (ref Quaternion q1, ref Quaternion q2, out Boolean result) {
            result = (q1.I == q2.I) && (q1.J == q2.J) && (q1.K == q2.K) && (q1.U == q2.U);
        }

        public static void Add (ref Quaternion q1, ref Quaternion q2, out Quaternion result) {
            result.I = q1.I + q2.I; result.J = q1.J + q2.J; result.K = q1.K + q2.K; result.U = q1.U + q2.U;
        }

        public static void Subtract (ref Quaternion q1, ref Quaternion q2, out Quaternion result) {
            result.I = q1.I - q2.I; result.J = q1.J - q2.J; result.K = q1.K - q2.K; result.U = q1.U - q2.U;
        }

        public static void Negate (ref Quaternion quaternion, out Quaternion result) {
            result.I = -quaternion.I; result.J = -quaternion.J; result.K = -quaternion.K; result.U = -quaternion.U;
        }

        // http://www.euclideanspace.com/maths/algebra/realNormedAlgebra/quaternions/arithmetic/index.htm
        public static void Multiply (ref Quaternion q1, ref Quaternion q2, out Quaternion result) {
            result.I = q1.I * q2.U + q1.U * q2.I + q1.J * q2.K - q1.K * q2.J;
            result.J = q1.U * q2.J - q1.I * q2.K + q1.J * q2.U + q1.K * q2.I;
            result.K = q1.U * q2.K + q1.I * q2.J - q1.J * q2.I + q1.K * q2.U;
            result.U = q1.U * q2.U - q1.I * q2.I - q1.J * q2.J - q1.K * q2.K;
        }

#if (FUNCTION_VARIANTS)
        public static Boolean    operator == (Quaternion a, Quaternion b) { Boolean    result; Equals   (ref a, ref b, out result); return  result; }
        public static Boolean    operator != (Quaternion a, Quaternion b) { Boolean    result; Equals   (ref a, ref b, out result); return !result; }
        public static Quaternion operator  + (Quaternion a, Quaternion b) { Quaternion result; Add      (ref a, ref b, out result); return  result; }
        public static Quaternion operator  - (Quaternion a, Quaternion b) { Quaternion result; Subtract (ref a, ref b, out result); return  result; }
        public static Quaternion operator  - (Quaternion v)               { Quaternion result; Negate   (ref v,        out result); return  result; }
        public static Quaternion operator  * (Quaternion a, Quaternion b) { Quaternion result; Multiply (ref a, ref b, out result); return  result; }
        public static Quaternion operator  ~ (Quaternion v)               { Quaternion result; Normalise(ref v,        out result); return  result; }

        public static Boolean    Equals      (Quaternion a, Quaternion b) { Boolean    result; Equals   (ref a, ref b, out result); return  result; }
        public static Quaternion Add         (Quaternion a, Quaternion b) { Quaternion result; Add      (ref a, ref b, out result); return  result; }
        public static Quaternion Subtract    (Quaternion a, Quaternion b) { Quaternion result; Subtract (ref a, ref b, out result); return  result; }
        public static Quaternion Negate      (Quaternion v)               { Quaternion result; Negate   (ref v,        out result); return  result; }
        public static Quaternion Multiply    (Quaternion a, Quaternion b) { Quaternion result; Multiply (ref a, ref b, out result); return  result; }
#endif

        // Utilities //-------------------------------------------------------//

        public static void Lerp (ref Quaternion q1, ref Quaternion q2, ref Single amount, out Quaternion result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Single remaining = 1 - amount;
            Single r = remaining;
            Single a = amount;
            result.U = (r * q1.U) + (a * q2.U);
            result.I = (r * q1.I) + (a * q2.I);
            result.J = (r * q1.J) + (a * q2.J);
            result.K = (r * q1.K) + (a * q2.K);
        }

        public static void IsUnit (ref Quaternion q, out Boolean result) {
            result = Maths.IsZero((Single) 1 - q.U * q.U - q.I * q.I - q.J * q.J - q.K * q.K);
        }

#if (FUNCTION_VARIANTS)
        public bool IsUnit () { Boolean result; IsUnit (ref this, out result); return result; }

        public static Boolean    IsUnit (Quaternion q) { Boolean result; IsUnit (ref q, out result); return result; }
        public static Quaternion Lerp   (Quaternion a, Quaternion b, Single amount) { Quaternion result; Lerp (ref a, ref b, ref amount, out result); return result; }
#endif

        // Maths //-----------------------------------------------------------//

        public static void LengthSquared (ref Quaternion quaternion, out Single result) {
            result = (quaternion.I * quaternion.I) + (quaternion.J * quaternion.J)
                   + (quaternion.K * quaternion.K) + (quaternion.U * quaternion.U);
        }

        public static void Length (ref Quaternion quaternion, out Single result) {
            Single lengthSquared = (quaternion.I * quaternion.I) + (quaternion.J * quaternion.J)
                                 + (quaternion.K * quaternion.K) + (quaternion.U * quaternion.U);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void Conjugate (ref Quaternion value, out Quaternion result) {
            result.I = -value.I; result.J = -value.J;
            result.K = -value.K; result.U = value.U;
        }

        public static void Inverse (ref Quaternion quaternion, out Quaternion result) {
            Single a = (quaternion.I * quaternion.I) + (quaternion.J * quaternion.J)
                     + (quaternion.K * quaternion.K) + (quaternion.U * quaternion.U);
            Single b = 1 / a;
            result.I = -quaternion.I * b; result.J = -quaternion.J * b;
            result.K = -quaternion.K * b; result.U =  quaternion.U * b;
        }

        public static void Dot (ref Quaternion q1, ref Quaternion q2, out Single result) {
            result = (q1.I * q2.I) + (q1.J * q2.J) + (q1.K * q2.K) + (q1.U * q2.U);
        }

        public static void Concatenate (ref Quaternion q1, ref Quaternion q2, out Quaternion result) {
            Single a = (q1.K * q2.J) - (q1.J * q2.K);
            Single b = (q1.I * q2.K) - (q1.K * q2.I);
            Single c = (q1.J * q2.I) - (q1.I * q2.J);
            Single d = (q1.I * q2.I) - (q1.J * q2.J);
            Single i = (q1.U * q2.I) + (q1.I * q2.U) + a;
            Single j = (q1.U * q2.J) + (q1.J * q2.U) + b;
            Single k = (q1.U * q2.K) + (q1.K * q2.U) + c;
            Single u = (q1.U * q2.U) - (q1.K * q2.K) - d;
            result.I = i; result.J = j; result.K = k; result.U = u;
        }

        public static void Normalise (ref Quaternion quaternion, out Quaternion result) {
            Single a = (quaternion.I * quaternion.I) + (quaternion.J * quaternion.J)
                     + (quaternion.K * quaternion.K) + (quaternion.U * quaternion.U);
            Single b = 1 / Maths.Sqrt (a);
            result.I = quaternion.I * b; result.J = quaternion.J * b;
            result.K = quaternion.K * b; result.U = quaternion.U * b;
        }

        public static void Transform (ref Quaternion rotation, ref Vector3 vector, out Vector3 result) {
            Single i = rotation.I, j = rotation.J, k = rotation.K, u = rotation.U;
            Single ii = i * i, jj = j * j, kk = k * k;
            Single ui = u * i, uj = u * j, uk = u * k;
            Single ij = i * j, ik = i * k, jk = j * k;
            Single x = vector.X - (2 * vector.X * (jj + kk)) + (2 * vector.Y * (ij - uk)) + (2 * vector.Z * (ik + uj));
            Single y = vector.Y + (2 * vector.X * (ij + uk)) - (2 * vector.Y * (ii + kk)) + (2 * vector.Z * (jk - ui));
            Single z = vector.Z + (2 * vector.X * (ik - uj)) + (2 * vector.Y * (jk + ui)) - (2 * vector.Z * (ii + jj));
            result.X = x; result.Y = y; result.Z = z;
        }

        public static void Transform (ref Quaternion rotation, ref Vector4 vector, out Vector4 result) {
            Single i = rotation.I, j = rotation.J, k = rotation.K, u = rotation.U;
            Single ii = i * i, jj = j * j, kk = k * k;
            Single ui = u * i, uj = u * j, uk = u * k;
            Single ij = i * j, ik = i * k, jk = j * k;
            Single x = vector.X - (vector.X * 2 * (jj + kk)) + (vector.Y * 2 * (ij - uk)) + (vector.Z * 2 * (ik + uj));
            Single y = vector.Y + (vector.X * 2 * (ij + uk)) - (vector.Y * 2 * (ii + kk)) + (vector.Z * 2 * (jk - ui));
            Single z = vector.Z + (vector.X * 2 * (ik - uj)) + (vector.Y * 2 * (jk + ui)) - (vector.Z * 2 * (ii + jj));
            Single w = vector.W;
            result.X = x; result.Y = y; result.Z = z; result.W = w;
        }

#if (FUNCTION_VARIANTS)
        public Single     LengthSquared () { Single result; LengthSquared (ref this, out result); return result; }
        public Single     Length        () { Single result; Length (ref this, out result); return result; }
        public void       Normalise     () { Normalise (ref this, out this); }
        public Quaternion Conjugate     () { Conjugate (ref this, out this); return this; }
        public Quaternion Inverse       () { Inverse (ref this, out this); return this; }
        public Single     Dot           (Quaternion q) { Single result; Dot (ref this, ref q, out result); return result; }
        public Quaternion Concatenate   (Quaternion q) { Concatenate (ref this, ref q, out this); return this; }
        public Vector3    Transform     (Vector3 v) { Vector3 result; Transform (ref this, ref v, out result); return result; }
        public Vector4    Transform     (Vector4 v) { Vector4 result; Transform (ref this, ref v, out result); return result; }

        public static Single     LengthSquared (Quaternion q) { Single result; LengthSquared (ref q, out result); return result; }
        public static Single     Length        (Quaternion q) { Single result; Length (ref q, out result); return result; }
        public static Quaternion Normalise     (Quaternion q) { Quaternion result; Normalise (ref q, out result); return result; }
        public static Quaternion Conjugate     (Quaternion q) { Quaternion result; Conjugate (ref q, out result); return result; }
        public static Quaternion Inverse       (Quaternion q) { Quaternion result; Inverse (ref q, out result); return result; }
        public static Single     Dot           (Quaternion a, Quaternion b) { Single result; Dot (ref a, ref b, out result); return result; }
        public static Quaternion Concatenate   (Quaternion a, Quaternion b) { Quaternion result; Concatenate (ref a, ref b, out result); return result; }
        public static Vector3    Transform     (Quaternion rotation, Vector3 v) { Vector3 result; Transform (ref rotation, ref v, out result); return result; }
        public static Vector4    Transform     (Quaternion rotation, Vector4 v) { Vector4 result; Transform (ref rotation, ref v, out result); return result; }
#endif
        // Creation //--------------------------------------------------------//

        public static void CreateFromAxisAngle (ref Vector3 axis, ref Single angle, out Quaternion result) {
            Single theta = angle * Maths.Half;
            Single sin = Maths.Sin (theta), cos = Maths.Cos (theta);
            result.I = axis.X * sin;
            result.J = axis.Y * sin;
            result.K = axis.Z * sin;
            result.U = cos;
        }

        public static void CreateFromYawPitchRoll (ref Single yaw, ref Single pitch, ref Single roll, out Quaternion result) {
            Single hr = roll * Maths.Half, hp = pitch * Maths.Half, hy = yaw * Maths.Half;
            Single shr = Maths.Sin (hr), chr = Maths.Cos (hr);
            Single shp = Maths.Sin (hp), chp = Maths.Cos (hp);
            Single shy = Maths.Sin (hy), chy = Maths.Cos (hy);
            result.I = (chy * shp * chr) + (shy * chp * shr);
            result.J = (shy * chp * chr) - (chy * shp * shr);
            result.K = (chy * chp * shr) - (shy * shp * chr);
            result.U = (chy * chp * chr) + (shy * shp * shr);
        }

        // http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/
        public static void CreateFromRotationMatrix (ref Matrix44 m, out Quaternion result) {
            Single tr = m.R0C0 + m.R1C1 + m.R2C2;
            if (tr > 0) {
                Single s = Maths.Sqrt (tr + 1) * 2;
                result.U = Maths.Quarter * s;
                result.I = (m.R1C2 - m.R2C1) / s;
                result.J = (m.R2C0 - m.R0C2) / s;
                result.K = (m.R0C1 - m.R1C0) / s;
            }
            else if ((m.R0C0 >= m.R1C1) && (m.R0C0 >= m.R2C2)) {
                Single s = Maths.Sqrt (1 + m.R0C0 - m.R1C1 - m.R2C2) * 2;
                result.U = (m.R1C2 - m.R2C1) / s;
                result.I = Maths.Quarter * s;
                result.J = (m.R0C1 + m.R1C0) / s;
                result.K = (m.R0C2 + m.R2C0) / s;
            }
            else if (m.R1C1 > m.R2C2) {
                Single s = Maths.Sqrt (1 + m.R1C1 - m.R0C0 - m.R2C2) * 2;
                result.U = (m.R2C0 - m.R0C2) / s;
                result.I = (m.R1C0 + m.R0C1) / s;
                result.J = Maths.Quarter * s;
                result.K = (m.R2C1 + m.R1C2) / s;
            }
            else {
                Single s = Maths.Sqrt (1 + m.R2C2 - m.R0C0 - m.R1C1) * 2;
                result.U = (m.R0C1 - m.R1C0) / s;
                result.I = (m.R2C0 + m.R0C2) / s;
                result.J = (m.R2C1 + m.R1C2) / s;
                result.K = Maths.Quarter * s;
            }
        }


#if (FUNCTION_VARIANTS)
        public static Quaternion CreateFromAxisAngle      (Vector3 axis, Single angle) { Quaternion result; CreateFromAxisAngle (ref axis, ref angle, out result); return result; }
        public static Quaternion CreateFromYawPitchRoll   (Single yaw, Single pitch, Single roll) { Quaternion result; CreateFromYawPitchRoll (ref yaw, ref pitch, ref roll, out result); return result; }
        public static Quaternion CreateFromRotationMatrix (Matrix44 matrix) { Quaternion result; CreateFromRotationMatrix (ref matrix, out result); return result; }
#endif
    }
    /// <summary>
    /// Single precision Matrix44.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Matrix44 : IEquatable<Matrix44> {
        public Single R0C0, R0C1, R0C2, R0C3;
        public Single R1C0, R1C1, R1C2, R1C3;
        public Single R2C0, R2C1, R2C2, R2C3;
        public Single R3C0, R3C1, R3C2, R3C3;

        public Matrix44 (
            Single m00, Single m01, Single m02, Single m03, Single m10, Single m11, Single m12, Single m13,
            Single m20, Single m21, Single m22, Single m23, Single m30, Single m31, Single m32, Single m33) {
            this.R0C0 = m00; this.R0C1 = m01; this.R0C2 = m02; this.R0C3 = m03;
            this.R1C0 = m10; this.R1C1 = m11; this.R1C2 = m12; this.R1C3 = m13;
            this.R2C0 = m20; this.R2C1 = m21; this.R2C2 = m22; this.R2C3 = m23;
            this.R3C0 = m30; this.R3C1 = m31; this.R3C2 = m32; this.R3C3 = m33;
        }

        public override String ToString () {
            return String.Format ("((R0C0:{0}, R0C1:{1}, R0C2:{2}, R0C3:{3}), ", this.R0C0, this.R0C1, this.R0C2, this.R0C3)
                 + String.Format  ("(R1C0:{0}, R1C1:{1}, R1C2:{2}, R1C3:{3}), ", this.R1C0, this.R1C1, this.R1C2, this.R1C3)
                 + String.Format  ("(R2C0:{0}, R2C1:{1}, R2C2:{2}, R2C3:{3}), ", this.R2C0, this.R2C1, this.R2C2, this.R2C3)
                 + String.Format  ("(R3C0:{0}, R3C1:{1}, R3C2:{2}, R3C3:{3}))",  this.R3C0, this.R3C1, this.R3C2, this.R3C3);
        }

        public override Int32 GetHashCode () {
            return R0C0.GetHashCode ()                  ^ R0C1.GetHashCode ().ShiftAndWrap (2)
                ^ R0C2.GetHashCode ().ShiftAndWrap (4)  ^ R0C3.GetHashCode ().ShiftAndWrap (6)
                ^ R1C0.GetHashCode ().ShiftAndWrap (8)  ^ R1C1.GetHashCode ().ShiftAndWrap (10)
                ^ R1C2.GetHashCode ().ShiftAndWrap (12) ^ R1C3.GetHashCode ().ShiftAndWrap (14)
                ^ R2C0.GetHashCode ().ShiftAndWrap (16) ^ R2C1.GetHashCode ().ShiftAndWrap (18)
                ^ R2C2.GetHashCode ().ShiftAndWrap (20) ^ R2C3.GetHashCode ().ShiftAndWrap (22)
                ^ R3C0.GetHashCode ().ShiftAndWrap (24) ^ R3C1.GetHashCode ().ShiftAndWrap (26)
                ^ R3C2.GetHashCode ().ShiftAndWrap (28) ^ R3C3.GetHashCode ().ShiftAndWrap (30);
        }

        public override Boolean Equals (Object obj) { return (obj is Matrix44) ? this.Equals ((Matrix44)obj) : false; }

        public Boolean Equals (Matrix44 other) {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        public Boolean IsSymmetric () {
            Matrix44 transpose = this;
            Transpose (ref transpose, out transpose);
            return transpose.Equals (this);
        }

        public Boolean IsSkewSymmetric () {
            Matrix44 transpose = this;
            Transpose (ref transpose, out transpose);
            Negate (ref transpose, out transpose);
            return transpose.Equals (this);
        }

        // Accessors //-------------------------------------------------------//

        public Vector3 Up          { get { return new Vector3 ( R1C0,  R1C1,  R1C2); } set { R1C0 =  value.X; R1C1 =  value.Y; R1C2 =  value.Z; } }
        public Vector3 Down        { get { return new Vector3 (-R1C0, -R1C1, -R1C2); } set { R1C0 = -value.X; R1C1 = -value.Y; R1C2 = -value.Z; } }
        public Vector3 Right       { get { return new Vector3 ( R0C0,  R0C1,  R0C2); } set { R0C0 =  value.X; R0C1 =  value.Y; R0C2 =  value.Z; } }
        public Vector3 Left        { get { return new Vector3 (-R0C0, -R0C1, -R0C2); } set { R0C0 = -value.X; R0C1 = -value.Y; R0C2 = -value.Z; } }
        public Vector3 Forward     { get { return new Vector3 (-R2C0, -R2C1, -R2C2); } set { R2C0 = -value.X; R2C1 = -value.Y; R2C2 = -value.Z; } }
        public Vector3 Backward    { get { return new Vector3 ( R2C0,  R2C1,  R2C2); } set { R2C0 =  value.X; R2C1 =  value.Y; R2C2 =  value.Z; } }
        public Vector3 Translation { get { return new Vector3 ( R3C0,  R3C1,  R3C2); } set { R3C0 =  value.X; R3C1 =  value.Y; R3C2 =  value.Z; } }

        // Constants //-------------------------------------------------------//

        static Matrix44 identity, zero;

        static Matrix44 () {
            identity = new Matrix44 (1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
            zero     = new Matrix44 (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        public static Matrix44 Identity { get { return identity; } }
        public static Matrix44 Zero     { get { return zero; } }

        // Operators //-------------------------------------------------------//

        public static void Equals (ref Matrix44 a, ref Matrix44 b, out Boolean result) {
            result = (a.R0C0 == b.R0C0) && (a.R1C1 == b.R1C1) &&
                     (a.R2C2 == b.R2C2) && (a.R3C3 == b.R3C3) &&
                     (a.R0C1 == b.R0C1) && (a.R0C2 == b.R0C2) &&
                     (a.R0C3 == b.R0C3) && (a.R1C0 == b.R1C0) &&
                     (a.R1C2 == b.R1C2) && (a.R1C3 == b.R1C3) &&
                     (a.R2C0 == b.R2C0) && (a.R2C1 == b.R2C1) &&
                     (a.R2C3 == b.R2C3) && (a.R3C0 == b.R3C0) &&
                     (a.R3C1 == b.R3C1) && (a.R3C2 == b.R3C2);
        }

        public static void Add (ref Matrix44 a, ref Matrix44 b, out Matrix44 result) {
            result.R0C0 = a.R0C0 + b.R0C0; result.R0C1 = a.R0C1 + b.R0C1;
            result.R0C2 = a.R0C2 + b.R0C2; result.R0C3 = a.R0C3 + b.R0C3;
            result.R1C0 = a.R1C0 + b.R1C0; result.R1C1 = a.R1C1 + b.R1C1;
            result.R1C2 = a.R1C2 + b.R1C2; result.R1C3 = a.R1C3 + b.R1C3;
            result.R2C0 = a.R2C0 + b.R2C0; result.R2C1 = a.R2C1 + b.R2C1;
            result.R2C2 = a.R2C2 + b.R2C2; result.R2C3 = a.R2C3 + b.R2C3;
            result.R3C0 = a.R3C0 + b.R3C0; result.R3C1 = a.R3C1 + b.R3C1;
            result.R3C2 = a.R3C2 + b.R3C2; result.R3C3 = a.R3C3 + b.R3C3;
        }

        public static void Subtract (ref Matrix44 a, ref Matrix44 b, out Matrix44 result) {
            result.R0C0 = a.R0C0 - b.R0C0; result.R0C1 = a.R0C1 - b.R0C1;
            result.R0C2 = a.R0C2 - b.R0C2; result.R0C3 = a.R0C3 - b.R0C3;
            result.R1C0 = a.R1C0 - b.R1C0; result.R1C1 = a.R1C1 - b.R1C1;
            result.R1C2 = a.R1C2 - b.R1C2; result.R1C3 = a.R1C3 - b.R1C3;
            result.R2C0 = a.R2C0 - b.R2C0; result.R2C1 = a.R2C1 - b.R2C1;
            result.R2C2 = a.R2C2 - b.R2C2; result.R2C3 = a.R2C3 - b.R2C3;
            result.R3C0 = a.R3C0 - b.R3C0; result.R3C1 = a.R3C1 - b.R3C1;
            result.R3C2 = a.R3C2 - b.R3C2; result.R3C3 = a.R3C3 - b.R3C3;
        }

        public static void Negate (ref Matrix44 m, out Matrix44 result) {
            result.R0C0 = -m.R0C0; result.R0C1 = -m.R0C1;
            result.R0C2 = -m.R0C2; result.R0C3 = -m.R0C3;
            result.R1C0 = -m.R1C0; result.R1C1 = -m.R1C1;
            result.R1C2 = -m.R1C2; result.R1C3 = -m.R1C3;
            result.R2C0 = -m.R2C0; result.R2C1 = -m.R2C1;
            result.R2C2 = -m.R2C2; result.R2C3 = -m.R2C3;
            result.R3C0 = -m.R3C0; result.R3C1 = -m.R3C1;
            result.R3C2 = -m.R3C2; result.R3C3 = -m.R3C3;
        }

        public static void Product (ref Matrix44 a, ref Matrix44 b, out Matrix44 result) {
            Single r0c0 = (a.R0C0 * b.R0C0) + (a.R0C1 * b.R1C0) + (a.R0C2 * b.R2C0) + (a.R0C3 * b.R3C0);
            Single r0c1 = (a.R0C0 * b.R0C1) + (a.R0C1 * b.R1C1) + (a.R0C2 * b.R2C1) + (a.R0C3 * b.R3C1);
            Single r0c2 = (a.R0C0 * b.R0C2) + (a.R0C1 * b.R1C2) + (a.R0C2 * b.R2C2) + (a.R0C3 * b.R3C2);
            Single r0c3 = (a.R0C0 * b.R0C3) + (a.R0C1 * b.R1C3) + (a.R0C2 * b.R2C3) + (a.R0C3 * b.R3C3);
            Single r1c0 = (a.R1C0 * b.R0C0) + (a.R1C1 * b.R1C0) + (a.R1C2 * b.R2C0) + (a.R1C3 * b.R3C0);
            Single r1c1 = (a.R1C0 * b.R0C1) + (a.R1C1 * b.R1C1) + (a.R1C2 * b.R2C1) + (a.R1C3 * b.R3C1);
            Single r1c2 = (a.R1C0 * b.R0C2) + (a.R1C1 * b.R1C2) + (a.R1C2 * b.R2C2) + (a.R1C3 * b.R3C2);
            Single r1c3 = (a.R1C0 * b.R0C3) + (a.R1C1 * b.R1C3) + (a.R1C2 * b.R2C3) + (a.R1C3 * b.R3C3);
            Single r2c0 = (a.R2C0 * b.R0C0) + (a.R2C1 * b.R1C0) + (a.R2C2 * b.R2C0) + (a.R2C3 * b.R3C0);
            Single r2c1 = (a.R2C0 * b.R0C1) + (a.R2C1 * b.R1C1) + (a.R2C2 * b.R2C1) + (a.R2C3 * b.R3C1);
            Single r2c2 = (a.R2C0 * b.R0C2) + (a.R2C1 * b.R1C2) + (a.R2C2 * b.R2C2) + (a.R2C3 * b.R3C2);
            Single r2c3 = (a.R2C0 * b.R0C3) + (a.R2C1 * b.R1C3) + (a.R2C2 * b.R2C3) + (a.R2C3 * b.R3C3);
            Single r3c0 = (a.R3C0 * b.R0C0) + (a.R3C1 * b.R1C0) + (a.R3C2 * b.R2C0) + (a.R3C3 * b.R3C0);
            Single r3c1 = (a.R3C0 * b.R0C1) + (a.R3C1 * b.R1C1) + (a.R3C2 * b.R2C1) + (a.R3C3 * b.R3C1);
            Single r3c2 = (a.R3C0 * b.R0C2) + (a.R3C1 * b.R1C2) + (a.R3C2 * b.R2C2) + (a.R3C3 * b.R3C2);
            Single r3c3 = (a.R3C0 * b.R0C3) + (a.R3C1 * b.R1C3) + (a.R3C2 * b.R2C3) + (a.R3C3 * b.R3C3);
            result.R0C0 = r0c0; result.R0C1 = r0c1; result.R0C2 = r0c2; result.R0C3 = r0c3;
            result.R1C0 = r1c0; result.R1C1 = r1c1; result.R1C2 = r1c2; result.R1C3 = r1c3;
            result.R2C0 = r2c0; result.R2C1 = r2c1; result.R2C2 = r2c2; result.R2C3 = r2c3;
            result.R3C0 = r3c0; result.R3C1 = r3c1; result.R3C2 = r3c2; result.R3C3 = r3c3; 
        }

        public static void Multiply (ref Matrix44 m, ref Single f, out Matrix44 result) {
            result.R0C0 = m.R0C0 * f; result.R0C1 = m.R0C1 * f;
            result.R0C2 = m.R0C2 * f; result.R0C3 = m.R0C3 * f;
            result.R1C0 = m.R1C0 * f; result.R1C1 = m.R1C1 * f;
            result.R1C2 = m.R1C2 * f; result.R1C3 = m.R1C3 * f;
            result.R2C0 = m.R2C0 * f; result.R2C1 = m.R2C1 * f;
            result.R2C2 = m.R2C2 * f; result.R2C3 = m.R2C3 * f;
            result.R3C0 = m.R3C0 * f; result.R3C1 = m.R3C1 * f;
            result.R3C2 = m.R3C2 * f; result.R3C3 = m.R3C3 * f;
        }

#if (FUNCTION_VARIANTS)
        public static Boolean  operator == (Matrix44 a, Matrix44 b) { Boolean result;  Equals   (ref a, ref b, out result); return  result; }
        public static Boolean  operator != (Matrix44 a, Matrix44 b) { Boolean result;  Equals   (ref a, ref b, out result); return !result; }
        public static Matrix44 operator  + (Matrix44 a, Matrix44 b) { Matrix44 result; Add      (ref a, ref b, out result); return  result; }
        public static Matrix44 operator  - (Matrix44 a, Matrix44 b) { Matrix44 result; Subtract (ref a, ref b, out result); return  result; }
        public static Matrix44 operator  - (Matrix44 v)             { Matrix44 result; Negate   (ref v, out result);        return  result; }
        public static Matrix44 operator  * (Matrix44 a, Matrix44 b) { Matrix44 result; Product  (ref a, ref b, out result); return  result; }
        public static Matrix44 operator  * (Matrix44 v, Single f)   { Matrix44 result; Multiply (ref v, ref f, out result); return  result; }
        public static Matrix44 operator  * (Single f, Matrix44 v)   { Matrix44 result; Multiply (ref v, ref f, out result); return  result; }

        public static Boolean  Equals      (Matrix44 a, Matrix44 b) { Boolean  result; Equals   (ref a, ref b, out result); return result; }
        public static Matrix44 Add         (Matrix44 a, Matrix44 b) { Matrix44 result; Add      (ref a, ref b, out result); return result; }
        public static Matrix44 Subtract    (Matrix44 a, Matrix44 b) { Matrix44 result; Subtract (ref a, ref b, out result); return result; }
        public static Matrix44 Negate      (Matrix44 v)             { Matrix44 result; Negate   (ref v, out result);        return result; }
        public static Matrix44 Product     (Matrix44 a, Matrix44 b) { Matrix44 result; Product  (ref a, ref b, out result); return result; }
        public static Matrix44 Multiply    (Matrix44 v, Single f)   { Matrix44 result; Multiply (ref v, ref f, out result); return result; }
#endif

        // Utilities //-------------------------------------------------------//

        public static void Lerp (ref Matrix44 a, ref Matrix44 b, ref Single amount, out Matrix44 result) {
            Debug.Assert (amount > 0 && amount <= 1);
            result.R0C0 = a.R0C0 + ((b.R0C0 - a.R0C0) * amount);
            result.R0C1 = a.R0C1 + ((b.R0C1 - a.R0C1) * amount);
            result.R0C2 = a.R0C2 + ((b.R0C2 - a.R0C2) * amount);
            result.R0C3 = a.R0C3 + ((b.R0C3 - a.R0C3) * amount);
            result.R1C0 = a.R1C0 + ((b.R1C0 - a.R1C0) * amount);
            result.R1C1 = a.R1C1 + ((b.R1C1 - a.R1C1) * amount);
            result.R1C2 = a.R1C2 + ((b.R1C2 - a.R1C2) * amount);
            result.R1C3 = a.R1C3 + ((b.R1C3 - a.R1C3) * amount);
            result.R2C0 = a.R2C0 + ((b.R2C0 - a.R2C0) * amount);
            result.R2C1 = a.R2C1 + ((b.R2C1 - a.R2C1) * amount);
            result.R2C2 = a.R2C2 + ((b.R2C2 - a.R2C2) * amount);
            result.R2C3 = a.R2C3 + ((b.R2C3 - a.R2C3) * amount);
            result.R3C0 = a.R3C0 + ((b.R3C0 - a.R3C0) * amount);
            result.R3C1 = a.R3C1 + ((b.R3C1 - a.R3C1) * amount);
            result.R3C2 = a.R3C2 + ((b.R3C2 - a.R3C2) * amount);
            result.R3C3 = a.R3C3 + ((b.R3C3 - a.R3C3) * amount);
        }
        
#if (FUNCTION_VARIANTS)
        public static Matrix44 Lerp (Matrix44 a, Matrix44 b, Single amount) { Matrix44 result; Lerp (ref a, ref b, ref amount, out result); return result; }
#endif

        // Maths //-----------------------------------------------------------//

        public static void Transpose (ref Matrix44 m, out Matrix44 result) {
            result.R0C0 = m.R0C0; result.R1C1 = m.R1C1;
            result.R2C2 = m.R2C2; result.R3C3 = m.R3C3;
            Single t = m.R0C1; result.R0C1 = m.R1C0; result.R1C0 = t;
                   t = m.R0C2; result.R0C2 = m.R2C0; result.R2C0 = t;
                   t = m.R0C3; result.R0C3 = m.R3C0; result.R3C0 = t;
                   t = m.R1C2; result.R1C2 = m.R2C1; result.R2C1 = t;
                   t = m.R1C3; result.R1C3 = m.R3C1; result.R3C1 = t;
                   t = m.R2C3; result.R2C3 = m.R3C2; result.R3C2 = t;
        }

        public static void Decompose (ref Matrix44 matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation, out Boolean result) {
            translation.X = matrix.R3C0; translation.Y = matrix.R3C1; translation.Z = matrix.R3C2;
            Vector3 a = new Vector3 (matrix.R0C0, matrix.R1C0, matrix.R2C0);
            Vector3 b = new Vector3 (matrix.R0C1, matrix.R1C1, matrix.R2C1);
            Vector3 c = new Vector3 (matrix.R0C2, matrix.R1C2, matrix.R2C2);
            Single aLen; Vector3.Length (ref a, out aLen); scale.X = aLen;
            Single bLen; Vector3.Length (ref b, out bLen); scale.Y = bLen;
            Single cLen; Vector3.Length (ref c, out cLen); scale.Z = cLen;
            if (Maths.IsZero (scale.X) || Maths.IsZero (scale.Y) || Maths.IsZero (scale.Z)) {
                rotation = Quaternion.Identity;
                result = false;
            }
            if (aLen < Maths.Epsilon) a = Vector3.Zero;
            else Vector3.Normalise (ref a, out a);
            if (bLen < Maths.Epsilon) b = Vector3.Zero;
            else Vector3.Normalise (ref b, out b);
            if (cLen < Maths.Epsilon) c = Vector3.Zero;
            else Vector3.Normalise (ref c, out c);
            Vector3 right = new Vector3 (a.X, b.X, c.X);
            Vector3 up = new Vector3 (a.Y, b.Y, c.Y);
            Vector3 backward = new Vector3 (a.Z, b.Z, c.Z);
            if (right.Equals (Vector3.Zero)) right = Vector3.Right;
            if (up.Equals (Vector3.Zero)) up = Vector3.Up;
            if (backward.Equals (Vector3.Zero)) backward = Vector3.Backward;
            Vector3.Normalise (ref right, out right);
            Vector3.Normalise (ref up, out up);
            Vector3.Normalise (ref backward, out backward);
            Matrix44 rotMat;
            Matrix44.CreateFromCartesianAxes (ref right, ref up, ref backward, out rotMat);
            Quaternion.CreateFromRotationMatrix (ref rotMat, out rotation);
            result = true;
        }

        public static void Determinant (ref Matrix44 m, out Single result) {
            result = + m.R0C3 * m.R1C2 * m.R2C1 * m.R3C0 - m.R0C2 * m.R1C3 * m.R2C1 * m.R3C0
                     - m.R0C3 * m.R1C1 * m.R2C2 * m.R3C0 + m.R0C1 * m.R1C3 * m.R2C2 * m.R3C0
                     + m.R0C2 * m.R1C1 * m.R2C3 * m.R3C0 - m.R0C1 * m.R1C2 * m.R2C3 * m.R3C0
                     - m.R0C3 * m.R1C2 * m.R2C0 * m.R3C1 + m.R0C2 * m.R1C3 * m.R2C0 * m.R3C1
                     + m.R0C3 * m.R1C0 * m.R2C2 * m.R3C1 - m.R0C0 * m.R1C3 * m.R2C2 * m.R3C1
                     - m.R0C2 * m.R1C0 * m.R2C3 * m.R3C1 + m.R0C0 * m.R1C2 * m.R2C3 * m.R3C1
                     + m.R0C3 * m.R1C1 * m.R2C0 * m.R3C2 - m.R0C1 * m.R1C3 * m.R2C0 * m.R3C2
                     - m.R0C3 * m.R1C0 * m.R2C1 * m.R3C2 + m.R0C0 * m.R1C3 * m.R2C1 * m.R3C2
                     + m.R0C1 * m.R1C0 * m.R2C3 * m.R3C2 - m.R0C0 * m.R1C1 * m.R2C3 * m.R3C2
                     - m.R0C2 * m.R1C1 * m.R2C0 * m.R3C3 + m.R0C1 * m.R1C2 * m.R2C0 * m.R3C3
                     + m.R0C2 * m.R1C0 * m.R2C1 * m.R3C3 - m.R0C0 * m.R1C2 * m.R2C1 * m.R3C3
                     - m.R0C1 * m.R1C0 * m.R2C2 * m.R3C3 + m.R0C0 * m.R1C1 * m.R2C2 * m.R3C3;
        }

        public static void Invert (ref Matrix44 m, out Matrix44 result) {
            Single d; Determinant (ref m, out d); Single s = 1 / d;
            Single r0c0 = m.R1C2 * m.R2C3 * m.R3C1 - m.R1C3 * m.R2C2 * m.R3C1 + m.R1C3 * m.R2C1 * m.R3C2 - m.R1C1 * m.R2C3 * m.R3C2 - m.R1C2 * m.R2C1 * m.R3C3 + m.R1C1 * m.R2C2 * m.R3C3;
            Single r0c1 = m.R0C3 * m.R2C2 * m.R3C1 - m.R0C2 * m.R2C3 * m.R3C1 - m.R0C3 * m.R2C1 * m.R3C2 + m.R0C1 * m.R2C3 * m.R3C2 + m.R0C2 * m.R2C1 * m.R3C3 - m.R0C1 * m.R2C2 * m.R3C3;
            Single r0c2 = m.R0C2 * m.R1C3 * m.R3C1 - m.R0C3 * m.R1C2 * m.R3C1 + m.R0C3 * m.R1C1 * m.R3C2 - m.R0C1 * m.R1C3 * m.R3C2 - m.R0C2 * m.R1C1 * m.R3C3 + m.R0C1 * m.R1C2 * m.R3C3;
            Single r0c3 = m.R0C3 * m.R1C2 * m.R2C1 - m.R0C2 * m.R1C3 * m.R2C1 - m.R0C3 * m.R1C1 * m.R2C2 + m.R0C1 * m.R1C3 * m.R2C2 + m.R0C2 * m.R1C1 * m.R2C3 - m.R0C1 * m.R1C2 * m.R2C3;
            Single r1c0 = m.R1C3 * m.R2C2 * m.R3C0 - m.R1C2 * m.R2C3 * m.R3C0 - m.R1C3 * m.R2C0 * m.R3C2 + m.R1C0 * m.R2C3 * m.R3C2 + m.R1C2 * m.R2C0 * m.R3C3 - m.R1C0 * m.R2C2 * m.R3C3;
            Single r1c1 = m.R0C2 * m.R2C3 * m.R3C0 - m.R0C3 * m.R2C2 * m.R3C0 + m.R0C3 * m.R2C0 * m.R3C2 - m.R0C0 * m.R2C3 * m.R3C2 - m.R0C2 * m.R2C0 * m.R3C3 + m.R0C0 * m.R2C2 * m.R3C3;
            Single r1c2 = m.R0C3 * m.R1C2 * m.R3C0 - m.R0C2 * m.R1C3 * m.R3C0 - m.R0C3 * m.R1C0 * m.R3C2 + m.R0C0 * m.R1C3 * m.R3C2 + m.R0C2 * m.R1C0 * m.R3C3 - m.R0C0 * m.R1C2 * m.R3C3;
            Single r1c3 = m.R0C2 * m.R1C3 * m.R2C0 - m.R0C3 * m.R1C2 * m.R2C0 + m.R0C3 * m.R1C0 * m.R2C2 - m.R0C0 * m.R1C3 * m.R2C2 - m.R0C2 * m.R1C0 * m.R2C3 + m.R0C0 * m.R1C2 * m.R2C3;
            Single r2c0 = m.R1C1 * m.R2C3 * m.R3C0 - m.R1C3 * m.R2C1 * m.R3C0 + m.R1C3 * m.R2C0 * m.R3C1 - m.R1C0 * m.R2C3 * m.R3C1 - m.R1C1 * m.R2C0 * m.R3C3 + m.R1C0 * m.R2C1 * m.R3C3;
            Single r2c1 = m.R0C3 * m.R2C1 * m.R3C0 - m.R0C1 * m.R2C3 * m.R3C0 - m.R0C3 * m.R2C0 * m.R3C1 + m.R0C0 * m.R2C3 * m.R3C1 + m.R0C1 * m.R2C0 * m.R3C3 - m.R0C0 * m.R2C1 * m.R3C3;
            Single r2c2 = m.R0C1 * m.R1C3 * m.R3C0 - m.R0C3 * m.R1C1 * m.R3C0 + m.R0C3 * m.R1C0 * m.R3C1 - m.R0C0 * m.R1C3 * m.R3C1 - m.R0C1 * m.R1C0 * m.R3C3 + m.R0C0 * m.R1C1 * m.R3C3;
            Single r2c3 = m.R0C3 * m.R1C1 * m.R2C0 - m.R0C1 * m.R1C3 * m.R2C0 - m.R0C3 * m.R1C0 * m.R2C1 + m.R0C0 * m.R1C3 * m.R2C1 + m.R0C1 * m.R1C0 * m.R2C3 - m.R0C0 * m.R1C1 * m.R2C3;
            Single r3c0 = m.R1C2 * m.R2C1 * m.R3C0 - m.R1C1 * m.R2C2 * m.R3C0 - m.R1C2 * m.R2C0 * m.R3C1 + m.R1C0 * m.R2C2 * m.R3C1 + m.R1C1 * m.R2C0 * m.R3C2 - m.R1C0 * m.R2C1 * m.R3C2;
            Single r3c1 = m.R0C1 * m.R2C2 * m.R3C0 - m.R0C2 * m.R2C1 * m.R3C0 + m.R0C2 * m.R2C0 * m.R3C1 - m.R0C0 * m.R2C2 * m.R3C1 - m.R0C1 * m.R2C0 * m.R3C2 + m.R0C0 * m.R2C1 * m.R3C2;
            Single r3c2 = m.R0C2 * m.R1C1 * m.R3C0 - m.R0C1 * m.R1C2 * m.R3C0 - m.R0C2 * m.R1C0 * m.R3C1 + m.R0C0 * m.R1C2 * m.R3C1 + m.R0C1 * m.R1C0 * m.R3C2 - m.R0C0 * m.R1C1 * m.R3C2;
            Single r3c3 = m.R0C1 * m.R1C2 * m.R2C0 - m.R0C2 * m.R1C1 * m.R2C0 + m.R0C2 * m.R1C0 * m.R2C1 - m.R0C0 * m.R1C2 * m.R2C1 - m.R0C1 * m.R1C0 * m.R2C2 + m.R0C0 * m.R1C1 * m.R2C2;
            result.R0C0 = r0c0; result.R0C1 = r0c1; result.R0C2 = r0c2; result.R0C3 = r0c3;
            result.R1C0 = r1c0; result.R1C1 = r1c1; result.R1C2 = r1c2; result.R1C3 = r1c3;
            result.R2C0 = r2c0; result.R2C1 = r2c1; result.R2C2 = r2c2; result.R2C3 = r2c3;
            result.R3C0 = r3c0; result.R3C1 = r3c1; result.R3C2 = r3c2; result.R3C3 = r3c3; 
            Multiply (ref result, ref s, out result);
        }

        public static void Transform (ref Matrix44 m, ref Quaternion q, out Matrix44 result) {
            Boolean qIsUnit; Quaternion.IsUnit (ref q, out qIsUnit);
            Debug.Assert (qIsUnit);
            Single twoI = q.I + q.I, twoJ = q.J + q.J, twoK = q.K + q.K;
            Single twoUI = q.U * twoI, twoUJ = q.U * twoJ, twoUK = q.U * twoK;
            Single twoII = q.I * twoI, twoIJ = q.I * twoJ, twoIK = q.I * twoK;
            Single twoJJ = q.J * twoJ, twoJK = q.J * twoK, twoKK = q.K * twoK;
            Single tR0C0 = 1 - twoJJ - twoKK;
            Single tR1C0 = twoIJ - twoUK;
            Single tR2C0 = twoIK + twoUJ;
            Single tR0C1 = twoIJ + twoUK;
            Single tR1C1 = 1 - twoII - twoKK;
            Single tR2C1 = twoJK - twoUI;
            Single tR0C2 = twoIK - twoUJ;
            Single tR1C2 = twoJK + twoUI;
            Single tR2C2 = 1 - twoII - twoJJ;
            Single r0c0 = m.R0C0 * tR0C0 + m.R0C1 * tR1C0 + m.R0C2 * tR2C0;
            Single r0c1 = m.R0C0 * tR0C1 + m.R0C1 * tR1C1 + m.R0C2 * tR2C1;
            Single r0c2 = m.R0C0 * tR0C2 + m.R0C1 * tR1C2 + m.R0C2 * tR2C2;
            Single r1c0 = m.R1C0 * tR0C0 + m.R1C1 * tR1C0 + m.R1C2 * tR2C0;
            Single r1c1 = m.R1C0 * tR0C1 + m.R1C1 * tR1C1 + m.R1C2 * tR2C1;
            Single r1c2 = m.R1C0 * tR0C2 + m.R1C1 * tR1C2 + m.R1C2 * tR2C2;
            Single r2c0 = m.R2C0 * tR0C0 + m.R2C1 * tR1C0 + m.R2C2 * tR2C0;
            Single r2c1 = m.R2C0 * tR0C1 + m.R2C1 * tR1C1 + m.R2C2 * tR2C1;
            Single r2c2 = m.R2C0 * tR0C2 + m.R2C1 * tR1C2 + m.R2C2 * tR2C2;
            Single r3c0 = m.R3C0 * tR0C0 + m.R3C1 * tR1C0 + m.R3C2 * tR2C0;
            Single r3c1 = m.R3C0 * tR0C1 + m.R3C1 * tR1C1 + m.R3C2 * tR2C1;
            Single r3c2 = m.R3C0 * tR0C2 + m.R3C1 * tR1C2 + m.R3C2 * tR2C2;
            result.R0C0 = r0c0; result.R0C1 = r0c1; result.R0C2 = r0c2; result.R0C3 = m.R0C3;
            result.R1C0 = r1c0; result.R1C1 = r1c1; result.R1C2 = r1c2; result.R1C3 = m.R1C3;
            result.R2C0 = r2c0; result.R2C1 = r2c1; result.R2C2 = r2c2; result.R2C3 = m.R2C3;
            result.R3C0 = r3c0; result.R3C1 = r3c1; result.R3C2 = r3c2; result.R3C3 = m.R3C3; 
        }

        public static void Transform (ref Matrix44 matrix, ref Vector3 vector, out Vector3 result) {
            Single x = (vector.X * matrix.R0C0) + (vector.Y * matrix.R1C0) + (vector.Z * matrix.R2C0) + matrix.R3C0;
            Single y = (vector.X * matrix.R0C1) + (vector.Y * matrix.R1C1) + (vector.Z * matrix.R2C1) + matrix.R3C1;
            Single z = (vector.X * matrix.R0C2) + (vector.Y * matrix.R1C2) + (vector.Z * matrix.R2C2) + matrix.R3C2;
            Single w = (vector.X * matrix.R0C3) + (vector.Y * matrix.R1C3) + (vector.Z * matrix.R2C3) + matrix.R3C3;
            result.X = x / w; result.Y = y / w; result.Z = z / w;
        }

        public static void Transform (ref Matrix44 matrix, ref Vector4 vector, out Vector4 result) {
            Single x = (vector.X * matrix.R0C0) + (vector.Y * matrix.R1C0) + (vector.Z * matrix.R2C0) + (vector.W * matrix.R3C0);
            Single y = (vector.X * matrix.R0C1) + (vector.Y * matrix.R1C1) + (vector.Z * matrix.R2C1) + (vector.W * matrix.R3C1);
            Single z = (vector.X * matrix.R0C2) + (vector.Y * matrix.R1C2) + (vector.Z * matrix.R2C2) + (vector.W * matrix.R3C2);
            Single w = (vector.X * matrix.R0C3) + (vector.Y * matrix.R1C3) + (vector.Z * matrix.R2C3) + (vector.W * matrix.R3C3);
            result.X = x; result.Y = y; result.Z = z; result.W = w;
        }

#if (FUNCTION_VARIANTS)
        public Single   Determinant ()                    { Single result; Determinant (ref this, out result); return result; }
        public Matrix44 Transpose   ()                    { Transpose (ref this, out this); return this; }
        public Matrix44 Invert      ()                    { Invert (ref this, out this); return this; }
        public Matrix44 Transform   (Quaternion rotation) { Matrix44 result; Transform (ref this, ref rotation, out result); return result; }
        public Vector3  Transform   (Vector3 v)           { Vector3 result; Transform (ref this, ref v, out result); return result; } 
        public Vector4  Transform   (Vector4 v)           { Vector4 result; Transform (ref this, ref v, out result); return result; } 

        public static Single   Determinant (Matrix44 matrix)                      { Single result; Determinant (ref matrix, out result); return result; }
        public static Matrix44 Transpose   (Matrix44 input)                       { Matrix44 result; Transpose (ref input, out result); return result; }
        public static Matrix44 Invert      (Matrix44 matrix)                      { Matrix44 result; Invert (ref matrix, out result); return result; }
        public static Matrix44 Transform   (Matrix44 matrix, Quaternion rotation) { Matrix44 result; Transform (ref matrix, ref rotation, out result); return result; }
        public static Vector3  Transform   (Matrix44 matrix, Vector3 v)           { Vector3 result; Transform (ref matrix, ref v, out result); return result; } 
        public static Vector4  Transform   (Matrix44 matrix, Vector4 v)           { Vector4 result; Transform (ref matrix, ref v, out result); return result; } 
#endif

        // Creation //--------------------------------------------------------//

        public static void CreateTranslation (ref Vector3 position, out Matrix44 result) {
            result.R0C0 = 1;          result.R0C1 = 0;          result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = 1;          result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = 0;          result.R2C1 = 0;          result.R2C2 = 1;          result.R2C3 = 0;
            result.R3C0 = position.X; result.R3C1 = position.Y; result.R3C2 = position.Z; result.R3C3 = 1;
        }

        public static void CreateTranslation (ref Single x, ref Single y, ref Single z, out Matrix44 result) {
            result.R0C0 = 1;          result.R0C1 = 0;          result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = 1;          result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = 0;          result.R2C1 = 0;          result.R2C2 = 1;          result.R2C3 = 0;
            result.R3C0 = x;          result.R3C1 = y;          result.R3C2 = z;          result.R3C3 = 1;
        }

        public static void CreateScale (ref Vector3 scale, out Matrix44 result) {
            result.R0C0 = scale.X;    result.R0C1 = 0;          result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = scale.Y;    result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = 0;          result.R2C1 = 0;          result.R2C2 = scale.Z;    result.R2C3 = 0;
            result.R3C0 = 0;          result.R3C1 = 0;          result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateScale (ref Single x, ref Single y, ref Single z, out Matrix44 result) {
            result.R0C0 = x;          result.R0C1 = 0;          result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = y;          result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = 0;          result.R2C1 = 0;          result.R2C2 = z;          result.R2C3 = 0;
            result.R3C0 = 0;          result.R3C1 = 0;          result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateScale (ref Single scale, out Matrix44 result) {
            result.R0C0 = scale;      result.R0C1 = 0;          result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = scale;      result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = 0;          result.R2C1 = 0;          result.R2C2 = scale;      result.R2C3 = 0;
            result.R3C0 = 0;          result.R3C1 = 0;          result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateRotationX (ref Single radians, out Matrix44 result) {
            Single cos = Maths.Cos (radians), sin = Maths.Sin (radians);
            result.R0C0 = 1;          result.R0C1 = 0;          result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = cos;        result.R1C2 = sin;        result.R1C3 = 0;
            result.R2C0 = 0;          result.R2C1 = -sin;       result.R2C2 = cos;        result.R2C3 = 0;
            result.R3C0 = 0;          result.R3C1 = 0;          result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateRotationY (ref Single radians, out Matrix44 result) {
            Single cos = Maths.Cos (radians), sin = Maths.Sin (radians);
            result.R0C0 = cos;        result.R0C1 = 0;          result.R0C2 = -sin;       result.R0C3 = 0;
            result.R1C0 = 0;          result.R1C1 = 1;          result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = sin;        result.R2C1 = 0;          result.R2C2 = cos;        result.R2C3 = 0;
            result.R3C0 = 0;          result.R3C1 = 0;          result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateRotationZ (ref Single radians, out Matrix44 result) {
            Single cos = Maths.Cos (radians), sin = Maths.Sin (radians);
            result.R0C0 = cos;       result.R0C1 = sin;         result.R0C2 = 0;          result.R0C3 = 0;
            result.R1C0 = -sin;      result.R1C1 = cos;         result.R1C2 = 0;          result.R1C3 = 0;
            result.R2C0 = 0;         result.R2C1 = 0;           result.R2C2 = 1;          result.R2C3 = 0;
            result.R3C0 = 0;         result.R3C1 = 0;           result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateFromAxisAngle (ref Vector3 axis, ref Single angle, out Matrix44 result) {
            Single x = axis.X, y = axis.Y, z = axis.Z;
            Single sin = Maths.Sin (angle), cos = Maths.Cos (angle);
            Single xx = x * x, yy = y * y, zz = z * z;
            Single xy = x * y, xz = x * z, yz = y * z;
            result.R0C0 = xx + (cos * (1 - xx));       result.R0C1 = xy - (cos * xy) + (sin * z); result.R0C2 = xz - (cos * xz) - (sin * y); result.R0C3 = 0;
            result.R1C0 = xy - (cos * xy) - (sin * z); result.R1C1 = yy + (cos * (1 - yy));       result.R1C2 = yz - (cos * yz) + (sin * x); result.R1C3 = 0;
            result.R2C0 = xz - (cos * xz) + (sin * y); result.R2C1 = yz - (cos * yz) - (sin * x); result.R2C2 = zz + (cos * (1 - zz));       result.R2C3 = 0;
            result.R3C0 = 0;                           result.R3C1 = 0;                           result.R3C2 = 0;                           result.R3C3 = 1;
        }

        // Axes must be pair-wise perpendicular and have unit length.
        public static void CreateFromCartesianAxes (ref Vector3 right, ref Vector3 up, ref Vector3 backward, out Matrix44 result) {
            result.R0C0 = right.X;    result.R0C1 = right.Y;    result.R0C2 = right.Z;    result.R0C3 = 0;
            result.R1C0 = up.X;       result.R1C1 = up.Y;       result.R1C2 = up.Z;       result.R1C3 = 0;
            result.R2C0 = backward.X; result.R2C1 = backward.Y; result.R2C2 = backward.Z; result.R2C3 = 0;
            result.R3C0 = 0;          result.R3C1 = 0;          result.R3C2 = 0;          result.R3C3 = 1;
        }

        public static void CreateWorld (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 result) {
            Vector3 backward; Vector3.Negate (ref forward, out backward); Vector3.Normalise (ref backward, out backward);
            Vector3 right; Vector3.Cross (ref up, ref backward, out right); Vector3.Normalise (ref right, out right);
            Vector3 finalUp; Vector3.Cross (ref right, ref backward, out finalUp); Vector3.Normalise (ref finalUp, out finalUp);
            result.R0C0 = right.X;    result.R0C1 = right.Y;    result.R0C2 = right.Z;    result.R0C3 = 0;
            result.R1C0 = finalUp.X;  result.R1C1 = finalUp.Y;  result.R1C2 = finalUp.Z;  result.R1C3 = 0;
            result.R2C0 = backward.X; result.R2C1 = backward.Y; result.R2C2 = backward.Z; result.R2C3 = 0;
            result.R3C0 = position.X; result.R3C1 = position.Y; result.R3C2 = position.Z; result.R3C3 = 1;
        }

        // http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToMatrix/
        public static void CreateFromQuaternion (ref Quaternion q, out Matrix44 result) {
            Boolean qIsUnit; Quaternion.IsUnit (ref q, out qIsUnit); Debug.Assert (qIsUnit);
            Single twoI = q.I + q.I, twoJ = q.J + q.J, twoK = q.K + q.K;
            Single twoUI = q.U * twoI, twoUJ = q.U * twoJ, twoUK = q.U * twoK;
            Single twoII = q.I * twoI, twoIJ = q.I * twoJ, twoIK = q.I * twoK;
            Single twoJJ = q.J * twoJ, twoJK = q.J * twoK, twoKK = q.K * twoK;
            result.R0C0 = 1 - twoJJ - twoKK; result.R1C0 = twoIJ - twoUK;     result.R2C0 = twoIK + twoUJ;     result.R3C0 = 0;
            result.R0C1 = twoIJ + twoUK;     result.R1C1 = 1 - twoII - twoKK; result.R2C1 = twoJK - twoUI;     result.R3C1 = 0;
            result.R0C2 = twoIK - twoUJ;     result.R1C2 = twoJK + twoUI;     result.R2C2 = 1 - twoII - twoJJ; result.R3C2 = 0;
            result.R0C3 = 0;                 result.R1C3 = 0;                 result.R2C3 = 0;                 result.R3C3 = 1;
        }

        // Angle of rotation, in radians. Angles are measured anti-clockwise when viewed from the rotation axis (positive side) toward the origin.
        public static void CreateFromYawPitchRoll (ref Single yaw, ref Single pitch, ref Single roll, out Matrix44 result) {
            Single cy = Maths.Cos (yaw), sy = Maths.Sin (yaw);
            Single cx = Maths.Cos (pitch), sx = Maths.Sin (pitch);
            Single cz = Maths.Cos (roll), sz = Maths.Sin (roll);
            result.R0C0 =  cz*cy+sz*sx*sy; result.R0C1 =  sz*cx; result.R0C2 = -cz*sy+sz*sx*cy; result.R0C3 = 0;
            result.R1C0 = -sz*cy+cz*sx*sy; result.R1C1 =  cz*cx; result.R1C2 = -cz*sy+sz*sx*cy; result.R1C3 = 0;
            result.R2C0 =  cx*sy;          result.R2C1 = -sx;    result.R2C2 =  cx*cy;          result.R2C3 = 0;
            result.R3C0 = 0;               result.R3C1 = 0;      result.R3C2 = 0;               result.R3C3 = 1;
        }

        // http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
        public static void CreatePerspectiveFieldOfView (ref Single fieldOfView, ref Single aspectRatio, ref Single nearPlaneDistance, ref Single farPlaneDistance, out Matrix44 result) {
            Debug.Assert (fieldOfView > 0 && fieldOfView < Maths.Pi);
            Debug.Assert (nearPlaneDistance > 0);
            Debug.Assert (farPlaneDistance > 0);
            Debug.Assert (nearPlaneDistance < farPlaneDistance);
            Single yScale = (Single) 1 / (Maths.Tan (fieldOfView * Maths.Half));
            Single xScale = yScale / aspectRatio;
            Single f1 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            Single f2 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            result.R0C0 = xScale; result.R0C1 = 0;      result.R0C2 = 0;  result.R0C3 =  0;
            result.R1C0 = 0;      result.R1C1 = yScale; result.R1C2 = 0;  result.R1C3 =  0;
            result.R2C0 = 0;      result.R2C1 = 0;      result.R2C2 = f1; result.R2C3 = -1;
            result.R3C0 = 0;      result.R3C1 = 0;      result.R3C2 = f2; result.R3C3 =  0;
        }

        // http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
        public static void CreatePerspective (ref Single width, ref Single height, ref Single nearPlaneDistance, ref Single farPlaneDistance, out Matrix44 result) {
            Debug.Assert (nearPlaneDistance > 0);
            Debug.Assert (farPlaneDistance > 0);
            Debug.Assert (nearPlaneDistance < farPlaneDistance);
            result.R0C0 = (nearPlaneDistance * 2) / width;
            result.R0C1 = result.R0C2 = result.R0C3 = 0;
            result.R1C1 = (nearPlaneDistance * 2) / height;
            result.R1C0 = result.R1C2 = result.R1C3 = 0;
            result.R2C2 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.R2C0 = result.R2C1 = 0;
            result.R2C3 = -1;
            result.R3C0 = result.R3C1 = result.R3C3 = 0;
            result.R3C2 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
        }

        // http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx
        public static void CreatePerspectiveOffCenter (ref Single left, ref Single right, ref Single bottom, ref Single top, ref Single nearPlaneDistance, ref Single farPlaneDistance, out Matrix44 result) {
            Debug.Assert (nearPlaneDistance > 0);
            Debug.Assert (farPlaneDistance > 0);
            Debug.Assert (nearPlaneDistance < farPlaneDistance);
            result.R0C0 = (nearPlaneDistance * 2) / (right - left);
            result.R0C1 = result.R0C2 = result.R0C3 = 0;
            result.R1C1 = (nearPlaneDistance * 2) / (top - bottom);
            result.R1C0 = result.R1C2 = result.R1C3 = 0;
            result.R2C0 = (left + right) / (right - left);
            result.R2C1 = (top + bottom) / (top - bottom);
            result.R2C2 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.R2C3 = -1;
            result.R3C2 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            result.R3C0 = result.R3C1 = result.R3C3 = 0;
        }

        // http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
        public static void CreateOrthographic (ref Single width, ref Single height, ref Single zNearPlane, ref Single zFarPlane, out Matrix44 result) {
            result.R0C0 = 2 / width;
            result.R0C1 = result.R0C2 = result.R0C3 = 0;
            result.R1C1 = 2 / height;
            result.R1C0 = result.R1C2 = result.R1C3 = 0;
            result.R2C2 = 1 / (zNearPlane - zFarPlane);
            result.R2C0 = result.R2C1 = result.R2C3 = 0;
            result.R3C0 = result.R3C1 = 0;
            result.R3C2 = zNearPlane / (zNearPlane - zFarPlane);
            result.R3C3 = 1;
        }

        // http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx
        public static void CreateOrthographicOffCenter (ref Single left, ref Single right, ref Single bottom, ref Single top, ref Single zNearPlane, ref Single zFarPlane, out Matrix44 result) {
            result.R0C0 = 2 / (right - left);
            result.R0C1 = result.R0C2 = result.R0C3 = 0;
            result.R1C1 = 2 / (top - bottom);
            result.R1C0 = result.R1C2 = result.R1C3 = 0;
            result.R2C2 = 1 / (zNearPlane - zFarPlane);
            result.R2C0 = result.R2C1 = result.R2C3 = 0;
            result.R3C0 = (left + right) / (left - right);
            result.R3C1 = (top + bottom) / (bottom - top);
            result.R3C2 = zNearPlane / (zNearPlane - zFarPlane);
            result.R3C3 = 1;
        }

        // http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx
        public static void CreateLookAt (ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix44 result) {
            Vector3 forward; Vector3.Subtract (ref cameraPosition, ref cameraTarget, out forward); Vector3.Normalise (ref forward, out forward);
            Vector3 right; Vector3.Cross (ref cameraUpVector, ref forward, out right); Vector3.Normalise (ref right, out right);
            Vector3 up; Vector3.Cross (ref forward, ref right, out up); Vector3.Normalise (ref up, out up);
            Single a; Vector3.Dot (ref right, ref cameraPosition, out a);
            Single b; Vector3.Dot (ref up, ref cameraPosition, out b);
            Single c; Vector3.Dot (ref forward, ref cameraPosition, out c);
            result.R0C0 = right.X;    result.R0C1 = up.X;       result.R0C2 = forward.X;  result.R0C3 = 0;
            result.R1C0 = right.Y;    result.R1C1 = up.Y;       result.R1C2 = forward.Y;  result.R1C3 = 0;
            result.R2C0 = right.Z;    result.R2C1 = up.Z;       result.R2C2 = forward.Z;  result.R2C3 = 0;
            result.R3C0 = -a;         result.R3C1 = -b;         result.R3C2 = -c;         result.R3C3 = 1;
        }

#if (FUNCTION_VARIANTS)
        public static Matrix44 CreateTranslation            (Single xPosition, Single yPosition, Single zPosition) { Matrix44 result; CreateTranslation (ref xPosition, ref yPosition, ref zPosition, out result); return result; }
        public static Matrix44 CreateTranslation            (Vector3 position) { Matrix44 result; CreateTranslation (ref position, out result); return result; }
        public static Matrix44 CreateScale                  (Single xScale, Single yScale, Single zScale) { Matrix44 result; CreateScale (ref xScale, ref yScale, ref zScale, out result); return result; }
        public static Matrix44 CreateScale                  (Vector3 scales) { Matrix44 result; CreateScale (ref scales, out result); return result; }
        public static Matrix44 CreateScale                  (Single scale) { Matrix44 result; CreateScale (ref scale, out result); return result; }
        public static Matrix44 CreateRotationX              (Single radians) { Matrix44 result; CreateRotationX (ref radians, out result); return result; }
        public static Matrix44 CreateRotationY              (Single radians) { Matrix44 result; CreateRotationY (ref radians, out result); return result; }
        public static Matrix44 CreateRotationZ              (Single radians) { Matrix44 result; CreateRotationZ (ref radians, out result); return result; }
        public static Matrix44 CreateFromAxisAngle          (Vector3 axis, Single angle) { Matrix44 result; CreateFromAxisAngle (ref axis, ref angle, out result); return result; }
        public static Matrix44 CreateFromCartesianAxes      (Vector3 right, Vector3 up, Vector3 backward) { Matrix44 result; CreateFromCartesianAxes (ref right, ref up, ref backward, out result); return result; }
        public static Matrix44 CreateWorld                  (Vector3 position, Vector3 forward, Vector3 up) { Matrix44 result; CreateWorld (ref position, ref forward, ref up, out result); return result; }
        public static Matrix44 CreateFromQuaternion         (Quaternion quaternion) { Matrix44 result; CreateFromQuaternion (ref quaternion, out result); return result; }
        public static Matrix44 CreateFromYawPitchRoll       (Single yaw, Single pitch, Single roll) { Matrix44 result; CreateFromYawPitchRoll (ref yaw, ref pitch, ref roll, out result); return result; }
        public static Matrix44 CreatePerspectiveFieldOfView (Single fieldOfView,  Single aspectRatio, Single nearPlane, Single farPlane) { Matrix44 result; CreatePerspectiveFieldOfView (ref fieldOfView, ref aspectRatio, ref nearPlane, ref farPlane, out result); return result; }
        public static Matrix44 CreatePerspective            (Single width, Single height, Single nearPlane, Single farPlane) { Matrix44 result; CreatePerspective (ref width, ref height, ref nearPlane, ref farPlane, out result); return result; }
        public static Matrix44 CreatePerspectiveOffCenter   (Single left, Single right, Single bottom, Single top, Single nearPlane, Single farPlane) { Matrix44 result; CreatePerspectiveOffCenter (ref left, ref right, ref bottom, ref top, ref nearPlane, ref farPlane, out result); return result; }
        public static Matrix44 CreateOrthographic           (Single width, Single height, Single nearPlane, Single farPlane) { Matrix44 result; CreateOrthographic (ref width, ref height, ref nearPlane, ref farPlane, out result); return result; }
        public static Matrix44 CreateOrthographicOffCenter  (Single left, Single right, Single bottom, Single top, Single nearPlane, Single farPlane) { Matrix44 result; CreateOrthographicOffCenter (ref left, ref right, ref bottom, ref top, ref nearPlane, ref farPlane, out result); return result; }
        public static Matrix44 CreateLookAt                 (Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector) { Matrix44 result; CreateLookAt (ref cameraPosition, ref cameraTarget, ref cameraUpVector, out result); return result; }
#endif

    }

    /// <summary>
    /// Single precision Vector2.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector2 : IEquatable<Vector2> {
        public Single X, Y;

        public Vector2 (Single x, Single y) { X = x; Y = y; }

        public override String ToString () { return String.Format ("(X:{0}, Y:{1})", X, Y); }

        public override Int32 GetHashCode () { return X.GetHashCode () ^ Y.GetHashCode ().ShiftAndWrap (2); }

        public override Boolean Equals (Object obj) { return (obj is Vector2) ? this.Equals ((Vector2) obj) : false; }

        public Boolean Equals (Vector2 other) { Boolean result; Equals (ref this, ref other, out result); return result; }

        // Constants //-------------------------------------------------------//

        readonly static Vector2 zero, one;
        readonly static Vector2 unitX, unitY;

        static Vector2 () {
            zero =      new Vector2 ();
            one =       new Vector2 (1, 1);
            unitX =     new Vector2 (1, 0);
            unitY =     new Vector2 (0, 1);
        }

        public static Vector2 Zero  { get { return zero; } }
        public static Vector2 One   { get { return one; } }
        public static Vector2 UnitX { get { return unitX; } }
        public static Vector2 UnitY { get { return unitY; } }

        // Operators //-------------------------------------------------------//

        public static void Equals (ref Vector2 a, ref Vector2 b, out Boolean result) {
            result = (a.X == b.X) && (a.Y == b.Y);
        }

        public static void Add (ref Vector2 a, ref Vector2 b, out Vector2 result) {
            result.X = a.X + b.X; result.Y = a.Y + b.Y;
        }

        public static void Subtract (ref Vector2 a, ref Vector2 b, out Vector2 result) {
            result.X = a.X - b.X; result.Y = a.Y - b.Y;
        }

        public static void Negate (ref Vector2 v, out Vector2 result) {
            result.X = -v.X; result.Y = -v.Y;
        }

        public static void Multiply (ref Vector2 a, ref Vector2 b, out Vector2 result) {
            result.X = a.X * b.X; result.Y = a.Y * b.Y;
        }

        public static void Multiply (ref Vector2 v, ref Single f, out Vector2 result) {
            result.X = v.X * f; result.Y = v.Y * f;
        }

        public static void Divide (ref Vector2 a, ref Vector2 b, out Vector2 result) {
            result.X = a.X / b.X; result.Y = a.Y / b.Y;
        }

        public static void Divide (ref Vector2 v, ref Single d, out Vector2 result) {
            Single num = 1 / d;
            result.X = v.X * num; result.Y = v.Y * num;
        }

#if (FUNCTION_VARIANTS)
        public static Boolean operator == (Vector2 a, Vector2 b) { Boolean result; Equals   (ref a, ref b, out result); return  result; }
        public static Boolean operator != (Vector2 a, Vector2 b) { Boolean result; Equals   (ref a, ref b, out result); return !result; }
        public static Vector2 operator  + (Vector2 a, Vector2 b) { Vector2 result; Add      (ref a, ref b, out result); return  result; }
        public static Vector2 operator  - (Vector2 a, Vector2 b) { Vector2 result; Subtract (ref a, ref b, out result); return  result; }
        public static Vector2 operator  - (Vector2 v)            { Vector2 result; Negate   (ref v,        out result); return  result; }
        public static Vector2 operator  * (Vector2 a, Vector2 b) { Vector2 result; Multiply (ref a, ref b, out result); return  result; }
        public static Vector2 operator  * (Vector2 v, Single f)  { Vector2 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector2 operator  * (Single f,  Vector2 v) { Vector2 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector2 operator  / (Vector2 a, Vector2 b) { Vector2 result; Divide   (ref a, ref b, out result); return  result; }
        public static Vector2 operator  / (Vector2 a, Single d)  { Vector2 result; Divide   (ref a, ref d, out result); return  result; }
        public static Single  operator  | (Vector2 a, Vector2 d) { Single  result; Dot      (ref a, ref d, out result); return  result; }
        public static Vector2 operator  ~ (Vector2 v)            { Vector2 result; Normalise(ref v,        out result); return  result; }

        public static Boolean Equals      (Vector2 a, Vector2 b) { Boolean result; Equals   (ref a, ref b, out result); return  result; }
        public static Vector2 Add         (Vector2 a, Vector2 b) { Vector2 result; Add      (ref a, ref b, out result); return  result; }
        public static Vector2 Subtract    (Vector2 a, Vector2 b) { Vector2 result; Subtract (ref a, ref b, out result); return  result; }
        public static Vector2 Negate      (Vector2 v)            { Vector2 result; Negate   (ref v,        out result); return  result; }
        public static Vector2 Multiply    (Vector2 a, Vector2 b) { Vector2 result; Multiply (ref a, ref b, out result); return  result; }
        public static Vector2 Multiply    (Vector2 v, Single f)  { Vector2 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector2 Divide      (Vector2 a, Vector2 b) { Vector2 result; Divide   (ref a, ref b, out result); return  result; }
        public static Vector2 Divide      (Vector2 a, Single d)  { Vector2 result; Divide   (ref a, ref d, out result); return  result; }
#endif

        // Utilities //-------------------------------------------------------//

        public static void Min (ref Vector2 a, ref Vector2 b, out Vector2 result) {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
        }

        public static void Max (ref Vector2 a, ref Vector2 b, out Vector2 result) {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
        }

        public static void Clamp (ref Vector2 v, ref Vector2 min, ref Vector2 max, out Vector2 result) {
            Single x = v.X; x = (x > max.X) ? max.X : x; x = (x < min.X) ? min.X : x; result.X = x;
            Single y = v.Y; y = (y > max.Y) ? max.Y : y; y = (y < min.Y) ? min.Y : y; result.Y = y;
        }

        public static void Lerp (ref Vector2 a, ref Vector2 b, Single amount, out Vector2 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
        }

        public static void IsUnit (ref Vector2 vector, out Boolean result) {
            result = Maths.IsZero(1 - vector.X * vector.X - vector.Y * vector.Y);
        }

#if (FUNCTION_VARIANTS)
        public Boolean IsUnit        () { Boolean result; IsUnit (ref this, out result); return result; }
        public Vector2 Clamp         (Vector2 min, Vector2 max) { Clamp (ref this, ref min, ref max, out this); return this; }

        public static Vector2 Min    (Vector2 a, Vector2 b) { Vector2 result; Min (ref a, ref b, out result); return result; }
        public static Vector2 Max    (Vector2 a, Vector2 b) { Vector2 result; Max (ref a, ref b, out result); return result; }
        public static Vector2 Clamp  (Vector2 v, Vector2 min, Vector2 max) { Vector2 result; Clamp (ref v, ref min, ref max, out result); return result; }
        public static Vector2 Lerp   (Vector2 a, Vector2 b, Single amount) { Vector2 result; Lerp (ref a, ref b, amount, out result); return result; }
        public static Boolean IsUnit (Vector2 v) { Boolean result; IsUnit (ref v, out result); return result; }

#endif
        
        // Splines //---------------------------------------------------------//

        public static void SmoothStep (ref Vector2 vector1, ref Vector2 vector2, Single amount, out Vector2 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            amount = (amount * amount) * (3 - (2 * amount));
            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
        }

        public static void CatmullRom (ref Vector2 vector1, ref Vector2 vector2, ref Vector2 vector3, ref Vector2 vector4, Single amount, out Vector2 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Single squared = amount * amount;
            Single cubed = amount * squared;
            result.X  = 2 * vector2.X;
            result.X += (vector3.X - vector1.X) * amount;
            result.X += ((2 * vector1.X) + (4 * vector3.X) - (5 * vector2.X) - (vector4.X)) * squared;
            result.X += ((3 * vector2.X) + (vector4.X) - (vector1.X)  - (3 * vector3.X)) * cubed;
            result.X *= Maths.Half;
            result.Y  = 2 * vector2.Y;
            result.Y += (vector3.Y - vector1.Y) * amount;
            result.Y += ((2 * vector1.Y) + (4 * vector3.Y) - (5 * vector2.Y) - (vector4.Y)) * squared;
            result.Y += ((3 * vector2.Y) + (vector4.Y) - (vector1.Y) - (3 * vector3.Y)) * cubed;
            result.Y *= Maths.Half;
        }

        public static void Hermite (ref Vector2 vector1, ref Vector2 tangent1, ref Vector2 vector2, ref Vector2 tangent2, Single amount, out Vector2 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector2.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector2.IsUnit (ref tangent2, out tangent2IsUnit);
            Debug.Assert (tangent1IsUnit && tangent2IsUnit);
            Single squared = amount * amount;
            Single cubed = amount * squared;
            Single a = ((cubed * 2) - (squared * 3)) + 1;
            Single b = (-cubed * 2) + (squared * 3);
            Single c = (cubed - (squared * 2)) + amount;
            Single d = cubed - squared;
            result.X = (vector1.X * a) + (vector2.X * b) + (tangent1.X * c) + (tangent2.X * d);
            result.Y = (vector1.Y * a) + (vector2.Y * b) + (tangent1.Y * c) + (tangent2.Y * d);
        }

#if (FUNCTION_VARIANTS)
        public static Vector2 SmoothStep (Vector2 vector1, Vector2 vector2, Single amount) { Vector2 result; SmoothStep (ref vector1, ref vector2, amount, out result); return result; }
        public static Vector2 CatmullRom (Vector2 vector1, Vector2 vector2, Vector2 vector3, Vector2 vector4, Single amount) { Vector2 result; CatmullRom (ref vector1, ref vector2, ref vector3, ref vector4, amount, out result); return result; }
        public static Vector2 Hermite    (Vector2 vector1, Vector2 tangent1, Vector2 vector2, Vector2 tangent2, Single amount) { Vector2 result; Hermite (ref vector1, ref tangent1, ref vector2, ref tangent2, amount, out result); return result; }
#endif

        // Maths //-----------------------------------------------------------//

        public static void Distance (ref Vector2 a, ref Vector2 b, out Single result) {
            Single dx = a.X - b.X, dy = a.Y - b.Y;
            Single lengthSquared = (dx * dx) + (dy * dy);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void DistanceSquared (ref Vector2 a, ref Vector2 b, out Single result) {
            Single dx = a.X - b.X, dy = a.Y - b.Y;
            result = (dx * dx) + (dy * dy);
        }

        public static void Dot (ref Vector2 a, ref Vector2 b, out Single result) {
            result = (a.X * b.X) + (a.Y * b.Y);
        }

        public static void Normalise (ref Vector2 vector, out Vector2 result) {
            Single lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y);
            Debug.Assert (lengthSquared > Maths.Epsilon && !Single.IsInfinity(lengthSquared));
            Single multiplier = 1 / Maths.Sqrt (lengthSquared);
            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
        }

        public static void Reflect (ref Vector2 vector, ref Vector2 normal, out Vector2 result) {
            Boolean normalIsUnit; Vector2.IsUnit (ref normal, out normalIsUnit);
            Debug.Assert (normalIsUnit);
            Single dot; Dot(ref vector, ref normal, out dot);
            Single twoDot = dot * 2;
            Vector2 m;
            Vector2.Multiply (ref normal, ref twoDot, out m);
            Vector2.Subtract (ref vector, ref m, out result);
        }

        public static void Length (ref Vector2 vector, out Single result) {
            Single lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void LengthSquared (ref Vector2 vector, out Single result) {
            result = (vector.X * vector.X) + (vector.Y * vector.Y);
        }

#if (FUNCTION_VARIANTS)
        public Single  Length        () { Single result; Length (ref this, out result); return result; }
        public Single  LengthSquared () { Single result; LengthSquared (ref this, out result); return result; }
        public Vector2 Normalise     () { Normalise (ref this, out this); return this; }

        public static Single  Distance        (Vector2 a, Vector2 b) { Single result; Distance (ref a, ref b, out result); return result; }
        public static Single  DistanceSquared (Vector2 a, Vector2 b) { Single result; DistanceSquared (ref a, ref b, out result); return result; }
        public static Single  Dot             (Vector2 a, Vector2 b) { Single result; Dot (ref a, ref b, out result); return result; }
        public static Vector2 Normalise       (Vector2 v) { Vector2 result; Normalise (ref v, out result); return result; }
        public static Vector2 Reflect         (Vector2 v, Vector2 normal) { Vector2 result; Reflect (ref v, ref normal, out result); return result; }
        public static Single  Length          (Vector2 v) { Single result; Length (ref v, out result); return result; }
        public static Single  LengthSquared   (Vector2 v) { Single result; LengthSquared (ref v, out result); return result; }
#endif


    }

    /// <summary>
    /// Single precision Vector3.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector3 : IEquatable<Vector3> {
        public Single X, Y, Z;

        public Vector3 (Single x, Single y, Single z) { X = x; Y = y; Z = z; }

        public Vector3 (Vector2 value, Single z) { X = value.X; Y = value.Y; Z = z; }

        public override String ToString () { return string.Format ("(X:{0}, Y:{1}, Z:{2})", X, Y, Z); }

        public override Int32 GetHashCode () {
            return X.GetHashCode () ^ Y.GetHashCode ().ShiftAndWrap (2) ^ Z.GetHashCode ().ShiftAndWrap (4);
        }

        public override Boolean Equals (Object obj) { return (obj is Vector3) ? this.Equals ((Vector3) obj) : false; }

        public Boolean Equals (Vector3 other) {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        // Constants //-------------------------------------------------------//

        static Vector3 zero, one;
        static Vector3 unitX, unitY, unitZ;
        static Vector3 up, down, right, left, forward, backward;

        static Vector3 () {
            zero =      new Vector3 ();
            one =       new Vector3 ( 1,  1,  1);
            unitX =     new Vector3 ( 1,  0,  0);
            unitY =     new Vector3 ( 0,  1,  0);
            unitZ =     new Vector3 ( 0,  0,  1);
            up =        new Vector3 ( 0,  1,  0);
            down =      new Vector3 ( 0, -1,  0);
            right =     new Vector3 ( 1,  0,  0);
            left =      new Vector3 (-1,  0,  0);
            forward =   new Vector3 ( 0,  0, -1);
            backward =  new Vector3 ( 0,  0,  1);
        }
        
        public static Vector3 Zero     { get { return zero; } }
        public static Vector3 One      { get { return one; } }
        public static Vector3 UnitX    { get { return unitX; } }
        public static Vector3 UnitY    { get { return unitY; } }
        public static Vector3 UnitZ    { get { return unitZ; } }
        public static Vector3 Up       { get { return up; } }
        public static Vector3 Down     { get { return down; } }
        public static Vector3 Right    { get { return right; } }
        public static Vector3 Left     { get { return left; } }
        public static Vector3 Forward  { get { return forward; } }
        public static Vector3 Backward { get { return backward; } }

        // Operators //-------------------------------------------------------//

        public static void Equals (ref Vector3 a, ref Vector3 b, out Boolean result) {
            result = (a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z);
        }

        public static void Add (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            result.X = a.X + b.X; result.Y = a.Y + b.Y; result.Z = a.Z + b.Z;
        }

        public static void Subtract (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            result.X = a.X - b.X; result.Y = a.Y - b.Y; result.Z = a.Z - b.Z;
        }

        public static void Negate (ref Vector3 value, out Vector3 result) {
            result.X = -value.X; result.Y = -value.Y; result.Z = -value.Z;
        }

        public static void Multiply (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            result.X = a.X * b.X; result.Y = a.Y * b.Y; result.Z = a.Z * b.Z;
        }

        public static void Multiply (ref Vector3 a, ref Single f, out Vector3 result) {
            result.X = a.X * f; result.Y = a.Y * f; result.Z = a.Z * f;
        }

        public static void Divide (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            result.X = a.X / b.X; result.Y = a.Y / b.Y; result.Z = a.Z / b.Z;
        }

        public static void Divide (ref Vector3 a, ref Single d, out Vector3 result) {
            Single num = 1 / d;
            result.X = a.X * num; result.Y = a.Y * num; result.Z = a.Z * num;
        }

#if (FUNCTION_VARIANTS)
        public static Boolean operator == (Vector3 a, Vector3 b) { Boolean result; Equals   (ref a, ref b, out result); return  result; }
        public static Boolean operator != (Vector3 a, Vector3 b) { Boolean result; Equals   (ref a, ref b, out result); return !result; }
        public static Vector3 operator  + (Vector3 a, Vector3 b) { Vector3 result; Add      (ref a, ref b, out result); return  result; }
        public static Vector3 operator  - (Vector3 a, Vector3 b) { Vector3 result; Subtract (ref a, ref b, out result); return  result; }
        public static Vector3 operator  - (Vector3 v)            { Vector3 result; Negate   (ref v,        out result); return  result; }
        public static Vector3 operator  * (Vector3 a, Vector3 b) { Vector3 result; Multiply (ref a, ref b, out result); return  result; }
        public static Vector3 operator  * (Vector3 v, Single f)  { Vector3 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector3 operator  * (Single f,  Vector3 v) { Vector3 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector3 operator  / (Vector3 a, Vector3 b) { Vector3 result; Divide   (ref a, ref b, out result); return  result; }
        public static Vector3 operator  / (Vector3 a, Single d)  { Vector3 result; Divide   (ref a, ref d, out result); return  result; }
        public static Vector3 operator  ^ (Vector3 a, Vector3 d) { Vector3 result; Cross    (ref a, ref d, out result); return  result; }
        public static Single  operator  | (Vector3 a, Vector3 d) { Single  result; Dot      (ref a, ref d, out result); return  result; }
        public static Vector3 operator  ~ (Vector3 v)            { Vector3 result; Normalise(ref v,        out result); return  result; }

        public static Boolean Equals      (Vector3 a, Vector3 b) { Boolean result; Equals   (ref a, ref b, out result); return  result; }
        public static Vector3 Add         (Vector3 a, Vector3 b) { Vector3 result; Add      (ref a, ref b, out result); return  result; }
        public static Vector3 Subtract    (Vector3 a, Vector3 b) { Vector3 result; Subtract (ref a, ref b, out result); return  result; }
        public static Vector3 Negate      (Vector3 v)            { Vector3 result; Negate   (ref v,        out result); return  result; }
        public static Vector3 Multiply    (Vector3 a, Vector3 b) { Vector3 result; Multiply (ref a, ref b, out result); return  result; }
        public static Vector3 Multiply    (Vector3 v, Single f)  { Vector3 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector3 Divide      (Vector3 a, Vector3 b) { Vector3 result; Divide   (ref a, ref b, out result); return  result; }
        public static Vector3 Divide      (Vector3 a, Single d)  { Vector3 result; Divide   (ref a, ref d, out result); return  result; }
#endif

        // Utilities //-------------------------------------------------------//

        public static void Min (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            result.X = (a.X < b.X) ? a.X : b.X; result.Y = (a.Y < b.Y) ? a.Y : b.Y;
            result.Z = (a.Z < b.Z) ? a.Z : b.Z;
        }

        public static void Max (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            result.X = (a.X > b.X) ? a.X : b.X; result.Y = (a.Y > b.Y) ? a.Y : b.Y;
            result.Z = (a.Z > b.Z) ? a.Z : b.Z;
        }

        public static void Clamp (ref Vector3 v, ref Vector3 min, ref Vector3 max, out Vector3 result) {
            Single x = v.X; x = (x > max.X) ? max.X : x; x = (x < min.X) ? min.X : x; result.X = x;
            Single y = v.Y; y = (y > max.Y) ? max.Y : y; y = (y < min.Y) ? min.Y : y; result.Y = y;
            Single z = v.Z; z = (z > max.Z) ? max.Z : z; z = (z < min.Z) ? min.Z : z; result.Z = z;
        }

        public static void Lerp (ref Vector3 a, ref Vector3 b, ref Single amount, out Vector3 result){
            Debug.Assert (amount >= 0 && amount <= 1);
            result.X = a.X + ((b.X - a.X) * amount); result.Y = a.Y + ((b.Y - a.Y) * amount);
            result.Z = a.Z + ((b.Z - a.Z) * amount);
        }

        public static void IsUnit (ref Vector3 vector, out Boolean result) {
            result = Maths.IsZero (1 - vector.X * vector.X - vector.Y * vector.Y - vector.Z * vector.Z);
        }

#if (FUNCTION_VARIANTS)
        public Boolean IsUnit        () { Boolean result; IsUnit (ref this, out result); return result; }
        public Vector3 Clamp         (Vector3 min, Vector3 max) { Clamp (ref this, ref min, ref max, out this); return this; }

        public static Vector3 Min    (Vector3 a, Vector3 b) { Vector3 result; Min (ref a, ref b, out result); return result; }
        public static Vector3 Max    (Vector3 a, Vector3 b) { Vector3 result; Max (ref a, ref b, out result); return result; }
        public static Vector3 Clamp  (Vector3 v, Vector3 min, Vector3 max) { Vector3 result; Clamp (ref v, ref min, ref max, out result); return result; }
        public static Vector3 Lerp   (Vector3 a, Vector3 b, ref Single amount) { Vector3 result; Lerp (ref a, ref b, ref amount, out result); return result; }
        public static Boolean IsUnit (Vector3 v) { Boolean result; IsUnit (ref v, out result); return result; }

#endif

        // Splines //---------------------------------------------------------//

        public static void SmoothStep (ref Vector3 vector1, ref Vector3 vector2, ref Single amount, out Vector3 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            amount = (amount * amount) * (3 - (2 * amount));
            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
            result.Z = vector1.Z + ((vector2.Z - vector1.Z) * amount);
        }

        public static void CatmullRom (ref Vector3 vector1, ref Vector3 vector2, ref Vector3 vector3, ref Vector3 vector4, ref Single amount, out Vector3 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Single squared = amount * amount;
            Single cubed = amount * squared;
            result.X  = 2 * vector2.X;
            result.X += (vector3.X - vector1.X) * amount;
            result.X += ((2 * vector1.X) + (4 * vector3.X) - (5 * vector2.X) - (vector4.X)) * squared;
            result.X += ((3 * vector2.X) + (vector4.X) - (vector1.X)  - (3 * vector3.X)) * cubed;
            result.X *= Maths.Half;
            result.Y  = 2 * vector2.Y;
            result.Y += (vector3.Y - vector1.Y) * amount;
            result.Y += ((2 * vector1.Y) + (4 * vector3.Y) - (5 * vector2.Y) - (vector4.Y)) * squared;
            result.Y += ((3 * vector2.Y) + (vector4.Y) - (vector1.Y) - (3 * vector3.Y)) * cubed;
            result.Y *= Maths.Half;
            result.Z  = 2 * vector2.Z;
            result.Z += (vector3.Z - vector1.Z) * amount;
            result.Z += ((2 * vector1.Z) + (4 * vector3.Z) - (5 * vector2.Z) - (vector4.Z)) * squared;
            result.Z += ((3 * vector2.Z) + (vector4.Z) - (vector1.Z) - (3 * vector3.Z)) * cubed;
            result.Z *= Maths.Half;
        }

        public static void Hermite (ref Vector3 vector1, ref Vector3 tangent1, ref Vector3 vector2, ref Vector3 tangent2, ref Single amount, out Vector3 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector3.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector3.IsUnit (ref tangent2, out tangent2IsUnit);
            Debug.Assert (tangent1IsUnit && tangent2IsUnit);
            Single squared = amount * amount;
            Single cubed = amount * squared;
            Single a = ((cubed * 2) - (squared * 3)) + 1;
            Single b = (-cubed * 2) + (squared * 3);
            Single c = (cubed - (squared * 2)) + amount;
            Single d = cubed - squared;
            result.X = (vector1.X * a) + (vector2.X * b) + (tangent1.X * c) + (tangent2.X * d);
            result.Y = (vector1.Y * a) + (vector2.Y * b) + (tangent1.Y * c) + (tangent2.Y * d);
            result.Z = (vector1.Z * a) + (vector2.Z * b) + (tangent1.Z * c) + (tangent2.Z * d);
        }

#if (FUNCTION_VARIANTS)
        public static Vector3 SmoothStep (Vector3 vector1, Vector3 vector2, ref Single amount) { Vector3 result; SmoothStep (ref vector1, ref vector2, ref amount, out result); return result; }
        public static Vector3 CatmullRom (Vector3 vector1, Vector3 vector2, Vector3 vector3, Vector3 vector4, ref Single amount) { Vector3 result; CatmullRom (ref vector1, ref vector2, ref vector3, ref vector4, ref amount, out result); return result; }
        public static Vector3 Hermite    (Vector3 vector1, Vector3 tangent1, Vector3 vector2, Vector3 tangent2, ref Single amount) { Vector3 result; Hermite (ref vector1, ref tangent1, ref vector2, ref tangent2, ref amount, out result); return result; }
#endif

        // Maths //-----------------------------------------------------------//

        public static void Distance (ref Vector3 a, ref Vector3 b, out Single result) {
            Single dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z;
            Single lengthSquared = (dx * dx) + (dy * dy) + (dz * dz);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void DistanceSquared (ref Vector3 a, ref Vector3 b, out Single result) {
            Single dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z;
            result = (dx * dx) + (dy * dy) + (dz * dz);
        }

        public static void Dot (ref Vector3 a, ref Vector3 b, out Single result) {
            result = (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        public static void Normalise (ref Vector3 vector, out Vector3 result) {
            Single lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z);
            Debug.Assert (lengthSquared > Maths.Epsilon && !Single.IsInfinity(lengthSquared));
            Single multiplier = 1 / Maths.Sqrt (lengthSquared);
            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
        }

        public static void Cross (ref Vector3 a, ref Vector3 b, out Vector3 result) {
            Single x = (a.Y * b.Z) - (a.Z * b.Y);
            Single y = (a.Z * b.X) - (a.X * b.Z);
            Single z = (a.X * b.Y) - (a.Y * b.X);
            result.X = x; result.Y = y; result.Z = z;
        }

        public static void Reflect (ref Vector3 vector, ref Vector3 normal, out Vector3 result) {
            Boolean normalIsUnit; Vector3.IsUnit (ref normal, out normalIsUnit);
            Debug.Assert (normalIsUnit);
            Single t = (vector.X * normal.X) + (vector.Y * normal.Y) + (vector.Z * normal.Z);
            Single x = vector.X - ((2 * t) * normal.X);
            Single y = vector.Y - ((2 * t) * normal.Y);
            Single z = vector.Z - ((2 * t) * normal.Z);
            result.X = x; result.Y = y; result.Z = z;
        }

        public static void Length (ref Vector3 vector, out Single result) {
            Single lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void LengthSquared (ref Vector3 vector, out Single result) {
            result = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z);
        }

#if (FUNCTION_VARIANTS)
        public Single  Length        () { Single result; Length (ref this, out result); return result; }
        public Single  LengthSquared () { Single result; LengthSquared (ref this, out result); return result; }
        public Vector3 Normalise     () { Normalise (ref this, out this); return this; }

        public static Single  Distance        (Vector3 a, Vector3 b) { Single result; Distance (ref a, ref b, out result); return result; } 
        public static Single  DistanceSquared (Vector3 a, Vector3 b) { Single result; DistanceSquared (ref a, ref b, out result); return result; } 
        public static Single  Dot             (Vector3 a, Vector3 b) { Single result; Dot (ref a, ref b, out result); return result; } 
        public static Vector3 Cross           (Vector3 a, Vector3 b) { Vector3 result; Cross (ref a, ref b, out result); return result; } 
        public static Vector3 Normalise       (Vector3 v) { Vector3 result; Normalise (ref v, out result); return result; }
         
        public static Vector3 Reflect         (Vector3 v, Vector3 normal) { Vector3 result; Reflect (ref v, ref normal, out result); return result; } 
        public static Single  Length          (Vector3 v) { Single result; Length (ref v, out result); return result; } 
        public static Single  LengthSquared   (Vector3 v) { Single result; LengthSquared (ref v, out result); return result; }
#endif

    }

    /// <summary>
    /// Single precision Vector4.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector4 : IEquatable<Vector4> {
        public Single X, Y, Z, W;

        public Vector4 (Single x, Single y, Single z, Single w) { X = x; Y = y; Z = z; W = w; }

        public Vector4 (Vector2 value, Single z, Single w) { X = value.X; Y = value.Y; Z = z; W = w; }

        public Vector4 (Vector3 value, Single w) { X = value.X; Y = value.Y; Z = value.Z; W = w; }

        public override String ToString () { return string.Format ("(X:{0}, Y:{1}, Z:{2}, W:{3})", X, Y, Z, W); }

        public override Int32 GetHashCode () {
            return W.GetHashCode ().ShiftAndWrap (6) ^ Z.GetHashCode ().ShiftAndWrap (4)
                 ^ Y.GetHashCode ().ShiftAndWrap (2) ^ X.GetHashCode ();
        }

        public override Boolean Equals (Object obj) { return (obj is Vector4) ? this.Equals ((Vector4)obj) : false; }

        public Boolean Equals (Vector4 other) {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        // Constants //-------------------------------------------------------//

        static Vector4 zero, one;
        static Vector4 unitX, unitY, unitZ, unitW;

        static Vector4 () {
            zero =      new Vector4 ();
            one =       new Vector4 (1, 1, 1, 1);
            unitX =     new Vector4 (1, 0, 0, 0);
            unitY =     new Vector4 (0, 1, 0, 0);
            unitZ =     new Vector4 (0, 0, 1, 0);
            unitW =     new Vector4 (0, 0, 0, 1);
        }

        public static Vector4 Zero  { get { return zero; } }
        public static Vector4 One   { get { return one; } }
        public static Vector4 UnitX { get { return unitX; } }
        public static Vector4 UnitY { get { return unitY; } }
        public static Vector4 UnitZ { get { return unitZ; } }
        public static Vector4 UnitW { get { return unitW; } }

        // Operators //-------------------------------------------------------//

        public static void Equals (ref Vector4 a, ref Vector4 b, out Boolean result) {
            result = (a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z) && (a.W == b.W);
        }

        public static void Add (ref Vector4 a, ref Vector4 b, out Vector4 result) {
            result.X = a.X + b.X; result.Y = a.Y + b.Y; result.Z = a.Z + b.Z; result.W = a.W + b.W;
        }

        public static void Subtract (ref Vector4 a, ref Vector4 b, out Vector4 result) {
            result.X = a.X - b.X; result.Y = a.Y - b.Y; result.Z = a.Z - b.Z; result.W = a.W - b.W;
        }

        public static void Negate (ref Vector4 v, out Vector4 result) {
            result.X = -v.X; result.Y = -v.Y; result.Z = -v.Z; result.W = -v.W;
        }

        public static void Multiply (ref Vector4 a, ref Vector4 b, out Vector4 result) {
            result.X = a.X * b.X; result.Y = a.Y * b.Y; result.Z = a.Z * b.Z; result.W = a.W * b.W;
        }

        public static void Multiply (ref Vector4 v, ref Single f, out Vector4 result) {
            result.X = v.X * f; result.Y = v.Y * f; result.Z = v.Z * f; result.W = v.W * f;
        }

        public static void Divide (ref Vector4 a, ref Vector4 b, out Vector4 result) {
            result.X = a.X / b.X; result.Y = a.Y / b.Y; result.Z = a.Z / b.Z; result.W = a.W / b.W;
        }

        public static void Divide (ref Vector4 v, ref Single d, out Vector4 result) {
            Single num = 1 / d;
            result.X = v.X * num; result.Y = v.Y * num; result.Z = v.Z * num; result.W = v.W * num;
        }

#if (FUNCTION_VARIANTS)
        public static Boolean operator == (Vector4 a, Vector4 b) { Boolean result; Equals   (ref a, ref b, out result); return  result; }
        public static Boolean operator != (Vector4 a, Vector4 b) { Boolean result; Equals   (ref a, ref b, out result); return !result; }
        public static Vector4 operator  + (Vector4 a, Vector4 b) { Vector4 result; Add      (ref a, ref b, out result); return  result; }
        public static Vector4 operator  - (Vector4 a, Vector4 b) { Vector4 result; Subtract (ref a, ref b, out result); return  result; }
        public static Vector4 operator  - (Vector4 v)            { Vector4 result; Negate   (ref v,        out result); return  result; }
        public static Vector4 operator  * (Vector4 a, Vector4 b) { Vector4 result; Multiply (ref a, ref b, out result); return  result; }
        public static Vector4 operator  * (Vector4 v, Single f)  { Vector4 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector4 operator  * (Single f,  Vector4 v) { Vector4 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector4 operator  / (Vector4 a, Vector4 b) { Vector4 result; Divide   (ref a, ref b, out result); return  result; }
        public static Vector4 operator  / (Vector4 a, Single d)  { Vector4 result; Divide   (ref a, ref d, out result); return  result; }
        public static Single  operator  | (Vector4 a, Vector4 d) { Single  result; Dot      (ref a, ref d, out result); return  result; }
        public static Vector4 operator  ~ (Vector4 v)            { Vector4 result; Normalise(ref v,        out result); return  result; }

        public static Boolean Equals      (Vector4 a, Vector4 b) { Boolean result; Equals   (ref a, ref b, out result); return  result; }
        public static Vector4 Add         (Vector4 a, Vector4 b) { Vector4 result; Add      (ref a, ref b, out result); return  result; }
        public static Vector4 Subtract    (Vector4 a, Vector4 b) { Vector4 result; Subtract (ref a, ref b, out result); return  result; }
        public static Vector4 Negate      (Vector4 v)            { Vector4 result; Negate   (ref v,        out result); return  result; }
        public static Vector4 Multiply    (Vector4 a, Vector4 b) { Vector4 result; Multiply (ref a, ref b, out result); return  result; }
        public static Vector4 Multiply    (Vector4 v, Single f)  { Vector4 result; Multiply (ref v, ref f, out result); return  result; }
        public static Vector4 Divide      (Vector4 a, Vector4 b) { Vector4 result; Divide   (ref a, ref b, out result); return  result; }
        public static Vector4 Divide      (Vector4 a, Single d)  { Vector4 result; Divide   (ref a, ref d, out result); return  result; }
#endif

        // Utilities //-------------------------------------------------------//

        public static void Min (ref Vector4 a, ref Vector4 b, out Vector4 result) {
            result.X = (a.X < b.X) ? a.X : b.X; result.Y = (a.Y < b.Y) ? a.Y : b.Y;
            result.Z = (a.Z < b.Z) ? a.Z : b.Z; result.W = (a.W < b.W) ? a.W : b.W;
        }

        public static void Max (ref Vector4 a, ref Vector4 b, out Vector4 result) {
            result.X = (a.X > b.X) ? a.X : b.X; result.Y = (a.Y > b.Y) ? a.Y : b.Y;
            result.Z = (a.Z > b.Z) ? a.Z : b.Z; result.W = (a.W > b.W) ? a.W : b.W;
        }

        public static void Clamp (ref Vector4 v, ref Vector4 min, ref Vector4 max, out Vector4 result) {
            Single x = v.X; x = (x > max.X) ? max.X : x; x = (x < min.X) ? min.X : x; result.X = x;
            Single y = v.Y; y = (y > max.Y) ? max.Y : y; y = (y < min.Y) ? min.Y : y; result.Y = y;
            Single z = v.Z; z = (z > max.Z) ? max.Z : z; z = (z < min.Z) ? min.Z : z; result.Z = z;
            Single w = v.W; w = (w > max.W) ? max.W : w; w = (w < min.W) ? min.W : w; result.W = w;
        }

        public static void Lerp (ref Vector4 a, ref Vector4 b, ref Single amount, out Vector4 result){
            Debug.Assert (amount >= 0 && amount <= 1);
            result.X = a.X + ((b.X - a.X) * amount); result.Y = a.Y + ((b.Y - a.Y) * amount);
            result.Z = a.Z + ((b.Z - a.Z) * amount); result.W = a.W + ((b.W - a.W) * amount);
        }

        public static void IsUnit (ref Vector4 vector, out Boolean result) {
            result = Maths.IsZero (1 - vector.X * vector.X - vector.Y * vector.Y - vector.Z * vector.Z - vector.W * vector.W);
        }

#if (FUNCTION_VARIANTS)
        public Boolean IsUnit        () { Boolean result; IsUnit (ref this, out result); return result; }
        public Vector4 Clamp         (Vector4 min, Vector4 max) { Clamp (ref this, ref min, ref max, out this); return this; }

        public static Vector4 Min    (Vector4 a, Vector4 b) { Vector4 result; Min (ref a, ref b, out result); return result; }
        public static Vector4 Max    (Vector4 a, Vector4 b) { Vector4 result; Max (ref a, ref b, out result); return result; }
        public static Vector4 Clamp  (Vector4 v, Vector4 min, Vector4 max) { Vector4 result; Clamp (ref v, ref min, ref max, out result); return result; }
        public static Vector4 Lerp   (Vector4 a, Vector4 b, Single amount) { Vector4 result; Lerp (ref a, ref b, ref amount, out result); return result; }
        public static Boolean IsUnit (Vector4 v) { Boolean result; IsUnit (ref v, out result); return result; }

#endif

        // Splines //---------------------------------------------------------//

        public static void SmoothStep (ref Vector4 vector1, ref Vector4 vector2, ref Single amount, out Vector4 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            amount = (amount * amount) * (3 - (2 * amount));
            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
            result.Z = vector1.Z + ((vector2.Z - vector1.Z) * amount);
            result.W = vector1.W + ((vector2.W - vector1.W) * amount);
        }

        public static void CatmullRom (ref Vector4 vector1, ref Vector4 vector2, ref Vector4 vector3, ref Vector4 vector4, ref Single amount, out Vector4 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Single squared = amount * amount;
            Single cubed = amount * squared;
            result.X  = 2 * vector2.X;
            result.X += (vector3.X - vector1.X) * amount;
            result.X += ((2 * vector1.X) + (4 * vector3.X) - (5 * vector2.X) - (vector4.X)) * squared;
            result.X += ((3 * vector2.X) + (vector4.X) - (vector1.X)  - (3 * vector3.X)) * cubed;
            result.X *= Maths.Half;
            result.Y  = 2 * vector2.Y;
            result.Y += (vector3.Y - vector1.Y) * amount;
            result.Y += ((2 * vector1.Y) + (4 * vector3.Y) - (5 * vector2.Y) - (vector4.Y)) * squared;
            result.Y += ((3 * vector2.Y) + (vector4.Y) - (vector1.Y) - (3 * vector3.Y)) * cubed;
            result.Y *= Maths.Half;
            result.Z  = 2 * vector2.Z;
            result.Z += (vector3.Z - vector1.Z) * amount;
            result.Z += ((2 * vector1.Z) + (4 * vector3.Z) - (5 * vector2.Z) - (vector4.Z)) * squared;
            result.Z += ((3 * vector2.Z) + (vector4.Z) - (vector1.Z) - (3 * vector3.Z)) * cubed;
            result.Z *= Maths.Half;
            result.W  = 2 * vector2.W;
            result.W += (vector3.W - vector1.W) * amount;
            result.W += ((2 * vector1.W) + (4 * vector3.W) - (5 * vector2.W) - (vector4.W)) * squared;
            result.W += ((3 * vector2.W) + (vector4.W) - (vector1.W) - (3 * vector3.W)) * cubed;
            result.W *= Maths.Half;
        }

        public static void Hermite (ref Vector4 vector1, ref Vector4 tangent1, ref Vector4 vector2, ref Vector4 tangent2, ref Single amount, out Vector4 result) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector4.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector4.IsUnit (ref tangent2, out tangent2IsUnit);
            Debug.Assert (tangent1IsUnit && tangent2IsUnit);
            Single squared = amount * amount;
            Single cubed = amount * squared;
            Single a = ((cubed * 2) - (squared * 3)) + 1;
            Single b = (-cubed * 2) + (squared * 3);
            Single c = (cubed - (squared * 2)) + amount;
            Single d = cubed - squared;
            result.X = (vector1.X * a) + (vector2.X * b) + (tangent1.X * c) + (tangent2.X * d);
            result.Y = (vector1.Y * a) + (vector2.Y * b) + (tangent1.Y * c) + (tangent2.Y * d);
            result.Z = (vector1.Z * a) + (vector2.Z * b) + (tangent1.Z * c) + (tangent2.Z * d);
            result.W = (vector1.W * a) + (vector2.W * b) + (tangent1.W * c) + (tangent2.W * d);
        }

#if (FUNCTION_VARIANTS)
        public static Vector4 SmoothStep (Vector4 vector1, Vector4 vector2, ref Single amount) { Vector4 result; SmoothStep (ref vector1, ref vector2, ref amount, out result); return result; }
        public static Vector4 CatmullRom (Vector4 vector1, Vector4 vector2, Vector4 vector3, Vector4 vector4, ref Single amount) { Vector4 result; CatmullRom (ref vector1, ref vector2, ref vector3, ref vector4, ref amount, out result); return result; }
        public static Vector4 Hermite    (Vector4 vector1, Vector4 tangent1, Vector4 vector2, Vector4 tangent2, ref Single amount) { Vector4 result; Hermite (ref vector1, ref tangent1, ref vector2, ref tangent2, ref amount, out result); return result; }
#endif

        // Maths //-----------------------------------------------------------//

        public static void Distance (ref Vector4 a, ref Vector4 b, out Single result) {
            Single dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z, dw = a.W - b.W;
            Single lengthSquared = (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void DistanceSquared (ref Vector4 a, ref Vector4 b, out Single result) {
            Single dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z, dw = a.W - b.W;
            result = (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
        }

        public static void Dot (ref Vector4 a, ref Vector4 b, out Single result) {
            result = (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z) + (a.W * b.W);
        }

        public static void Normalise (ref Vector4 vector, out Vector4 result) {
            Single lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z) + (vector.W * vector.W);
            Debug.Assert (lengthSquared > Maths.Epsilon && !Single.IsInfinity(lengthSquared));
            Single multiplier = 1 / (Maths.Sqrt (lengthSquared));
            result.X = vector.X * multiplier; result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier; result.W = vector.W * multiplier;
        }

        public static void Length (ref Vector4 vector, out Single result) {
            Single lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z) + (vector.W * vector.W);
            result = Maths.Sqrt (lengthSquared);
        }

        public static void LengthSquared (ref Vector4 vector, out Single result) {
            result = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z) + (vector.W * vector.W);
        }


#if (FUNCTION_VARIANTS)
        public Single  Length        () { Single result; Length (ref this, out result); return result; }
        public Single  LengthSquared () { Single result; LengthSquared (ref this, out result); return result; }
        public Vector4 Normalise     () { Normalise (ref this, out this); return this; }

        public static Single  Distance        ( Vector4 a, Vector4 b) { Single result; Distance (ref a, ref b, out result); return result; } 
        public static Single  DistanceSquared (Vector4 a, Vector4 b) { Single result; DistanceSquared (ref a, ref b, out result); return result; } 
        public static Single  Dot             (Vector4 a, Vector4 b) { Single result; Dot (ref a, ref b, out result); return result; } 
        public static Vector4 Normalise       (Vector4 v) { Vector4 result; Normalise (ref v, out result); return result; }
        public static Single  Length          (Vector4 v) { Single result; Length (ref v, out result); return result; } 
        public static Single  LengthSquared   (Vector4 v) { Single result; LengthSquared (ref v, out result); return result; }
#endif
    }

    /// <summary>
    /// Provides maths functions with consistent function signatures across supported precisions.
    /// </summary>
    public static class Maths {
        public static readonly Single Epsilon = 0.000001f;
        public static readonly Single E = 2.71828182845904523536028747135f;
        public static readonly Single Half = 0.5f;
        public static readonly Single Quarter = 0.25f;
        public static readonly Single Log10E = 0.43429448190325182765112891892f;
        public static readonly Single Log2E = 1.44269504088896340735992468100f;
        public static readonly Single Pi = 3.14159265358979323846264338328f;
        public static readonly Single HalfPi = 1.57079632679489661923132169164f;
        public static readonly Single QuarterPi = 0.78539816339744830961566084582f;
        public static readonly Single Root2 = 1.41421356237309504880168872421f;
        public static readonly Single Root3 = 1.73205080756887729352744634151f;
        public static readonly Single Tau = 6.28318530717958647692528676656f;
        public static readonly Single Deg2Rad = 0.01745329251994329576923690768f;
        public static readonly Single Rad2Deg = 57.29577951308232087679815481409f;
        public static readonly Single Zero = 0.0f;
        public static readonly Single One = 1.0f;

        public static Single Sqrt (Single v) { return (Single) Math.Sqrt (v); }

        public static Single Sin (Single v) { return (Single) Math.Sin (v); }
        public static Single Cos (Single v) { return (Single) Math.Cos (v); }
        public static Single Tan (Single v) { return (Single) Math.Tan (v); }

        public static Single ToRadians          (Single input) { return input * Deg2Rad; }
        public static Single ToDegrees          (Single input) { return input * Rad2Deg; }
        public static Single FromFraction       (Int32 numerator, Int32 denominator) { return (Single) numerator / (Single) denominator; }
        public static Single FromFraction       (Int64 numerator, Int64 denominator) { return (Single) numerator / (Single) denominator; }

        public static Single Min                (Single a, Single b) { return a < b ? a : b; }
        public static Single Max                (Single a, Single b) { return a > b ? a : b; }
        public static Single Clamp              (Single value, Single min, Single max) { if (value < min) return min; else if (value > max) return max; else return value; }
        public static Single Lerp               (Single a, Single b, Single t) { return a + ((b - a) * t); }
        public static Single Abs                (Single v) { return (v < 0) ? -v : v; }

        public static Single FromString         (String str) { Single result = Zero; Single.TryParse (str, out result); return result; }
        public static void    FromString        (String str, out Single value) { Single.TryParse (str, out value); }

        public static Boolean IsZero            (Single value) { return Abs(value) < Epsilon; }
        public static Boolean WithinEpsilon     (Single a, Single b) { Single num = a - b; return ((-Epsilon <= num) && (num <= Epsilon)); }
        public static Int32   Sign              (Single value) { if (value > 0) return 1; else if (value < 0) return -1; return 0; }
    }


    internal static class Int32Extensions { // http://msdn.microsoft.com/en-us/library/system.object.gethashcode(v=vs.110).aspx
        public static Int32 ShiftAndWrap (this Int32 value, Int32 positions = 2) {
            positions = positions & 0x1F;
            uint number = BitConverter.ToUInt32 (BitConverter.GetBytes(value), 0);
            uint wrapped = number >> (32 - positions);
            return BitConverter.ToInt32 (BitConverter.GetBytes ((number << positions) | wrapped), 0);
        }
    }
}
