// ┌────────────────────────────────────────────────────────────────────────┐ \\
// │    _____ ___.                                                          │ \\
// │   /  _  \\_ |__ _____    ____  __ __  ______                           │ \\
// │  /  /_\  \| __ \\__  \ _/ ___\|  |  \/  ___/                           │ \\
// │ /    |    \ \_\ \/ __ \\  \___|  |  /\___ \                            │ \\
// │ \____|__  /___  (____  /\___  >____//____  >                           │ \\
// │         \/    \/     \/     \/           \/  v1.1.0                    │ \\
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
using System.Runtime.CompilerServices;
using MI = System.Runtime.CompilerServices.MethodImplAttribute;
using O = System.Runtime.CompilerServices.MethodImplOptions;

namespace Abacus.DoublePrecision
{
    /// <summary>
    /// Double precision Quaternion.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Quaternion : IEquatable<Quaternion> {
        public Double I, J, K, U;

        [MI(O.AggressiveInlining)] public Quaternion (Double i, Double j, Double k, Double u) { I = i; J = j; K = k; U = u; }

        [MI(O.AggressiveInlining)] public Quaternion (Vector3 vectorPart, Double scalarPart) { I = vectorPart.X; J = vectorPart.Y; K = vectorPart.Z; U = scalarPart; }

        public override String ToString () { return String.Format ("(I:{0}, J:{1}, K:{2}, U:{3})", I, J, K, U); }

        [MI(O.AggressiveInlining)] public override Int32 GetHashCode () {
            return U.GetHashCode ().ShiftAndWrap (6) ^ K.GetHashCode ().ShiftAndWrap (4)
                 ^ J.GetHashCode ().ShiftAndWrap (2) ^ I.GetHashCode ();
        }

        [MI(O.AggressiveInlining)] public override Boolean Equals (Object obj) { return (obj is Quaternion) ? this.Equals ((Quaternion) obj) : false; }

        [MI(O.AggressiveInlining)] public Boolean Equals (Quaternion other) { Boolean r; Equals (ref this, ref other, out r); return r; }

        [MI(O.AggressiveInlining)] public Boolean ApproximateEquals (Quaternion other) { Boolean r; ApproximateEquals (ref this, ref other, out r); return r; }

        // Constants //-------------------------------------------------------//

        static Quaternion identity, zero;

        static Quaternion () {
            identity = new Quaternion (0, 0, 0, 1);
            zero     = new Quaternion (0, 0, 0, 0);
        }

        public static Quaternion Identity { get { return identity; } }
        public static Quaternion Zero     { get { return zero; } }

        // Operators //-------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void Equals (ref Quaternion q1, ref Quaternion q2, out Boolean r) {
            r = (q1.I == q2.I) && (q1.J == q2.J) && (q1.K == q2.K) && (q1.U == q2.U);
        }

        [MI(O.AggressiveInlining)] public static void ApproximateEquals (ref Quaternion q1, ref Quaternion q2, out Boolean r) {
            r = Maths.ApproximateEquals (q1.I, q2.I) && Maths.ApproximateEquals (q1.J, q2.J)
                && Maths.ApproximateEquals (q1.K, q2.K) && Maths.ApproximateEquals (q1.U, q2.U);
        }

        [MI(O.AggressiveInlining)] public static void Add (ref Quaternion q1, ref Quaternion q2, out Quaternion r) {
            r.I = q1.I + q2.I; r.J = q1.J + q2.J; r.K = q1.K + q2.K; r.U = q1.U + q2.U;
        }

        [MI(O.AggressiveInlining)] public static void Subtract (ref Quaternion q1, ref Quaternion q2, out Quaternion r) {
            r.I = q1.I - q2.I; r.J = q1.J - q2.J; r.K = q1.K - q2.K; r.U = q1.U - q2.U;
        }

        [MI(O.AggressiveInlining)] public static void Negate (ref Quaternion quaternion, out Quaternion r) {
            r.I = -quaternion.I; r.J = -quaternion.J; r.K = -quaternion.K; r.U = -quaternion.U;
        }

        // http://www.euclideanspace.com/maths/algebra/realNormedAlgebra/quaternions/arithmetic/index.htm
        [MI(O.AggressiveInlining)] public static void Multiply (ref Quaternion q1, ref Quaternion q2, out Quaternion r) {
            r.I = q1.I * q2.U + q1.U * q2.I + q1.J * q2.K - q1.K * q2.J;
            r.J = q1.U * q2.J - q1.I * q2.K + q1.J * q2.U + q1.K * q2.I;
            r.K = q1.U * q2.K + q1.I * q2.J - q1.J * q2.I + q1.K * q2.U;
            r.U = q1.U * q2.U - q1.I * q2.I - q1.J * q2.J - q1.K * q2.K;
        }

        [MI(O.AggressiveInlining)] public static Boolean    operator == (Quaternion a, Quaternion b) { Boolean    r; Equals    (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Boolean    operator != (Quaternion a, Quaternion b) { Boolean    r; Equals    (ref a, ref b, out r); return !r; }
        [MI(O.AggressiveInlining)] public static Quaternion operator  + (Quaternion a, Quaternion b) { Quaternion r; Add       (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Quaternion operator  - (Quaternion a, Quaternion b) { Quaternion r; Subtract  (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Quaternion operator  - (Quaternion v)               { Quaternion r; Negate    (ref v,        out r); return  r; }
        [MI(O.AggressiveInlining)] public static Quaternion operator  * (Quaternion a, Quaternion b) { Quaternion r; Multiply  (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3    operator  * (Vector3 v, Quaternion q)    { Vector3    r; Transform (ref q, ref v, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector4    operator  * (Vector4 v, Quaternion q)    { Vector4    r; Transform (ref q, ref v, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3    operator  * (Quaternion q, Vector3 v)    { Vector3    r; Transform (ref q, ref v, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector4    operator  * (Quaternion q, Vector4 v)    { Vector4    r; Transform (ref q, ref v, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Quaternion operator  ~ (Quaternion v)               { Quaternion r; Normalise (ref v,        out r); return  r; }

        [MI(O.AggressiveInlining)] public static Boolean    Equals            (Quaternion a, Quaternion b) { Boolean    r; Equals            (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Boolean    ApproximateEquals (Quaternion a, Quaternion b) { Boolean    r; ApproximateEquals (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Quaternion Add               (Quaternion a, Quaternion b) { Quaternion r; Add               (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Quaternion Subtract          (Quaternion a, Quaternion b) { Quaternion r; Subtract          (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Quaternion Negate            (Quaternion v)               { Quaternion r; Negate            (ref v,        out r); return r; }
        [MI(O.AggressiveInlining)] public static Quaternion Multiply          (Quaternion a, Quaternion b) { Quaternion r; Multiply          (ref a, ref b, out r); return r; }

        // Utilities //-------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void Lerp (ref Quaternion q1, ref Quaternion q2, ref Double amount, out Quaternion r) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Double remaining = 1 - amount;
            Double f = remaining;
            Double a = amount;
            r.U = (f * q1.U) + (a * q2.U);
            r.I = (f * q1.I) + (a * q2.I);
            r.J = (f * q1.J) + (a * q2.J);
            r.K = (f * q1.K) + (a * q2.K);
        }

        // http://en.wikipedia.org/wiki/Slerp
        [MI(O.AggressiveInlining)] public static void Slerp (ref Quaternion q1, ref Quaternion q2, ref Double amount,out Quaternion r) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Double remaining = 1 - amount;
            Double angle;
            Dot (ref q1, ref q2, out angle);
            if (angle < 0) {
                Negate (ref q1, out q1);
                angle = -angle;
            }
            Double theta = Maths.ArcCos (angle);
            Double f = remaining;
            Double a = amount;
            if (theta > Maths.Epsilon) {
                Double x = Maths.Sin (remaining * theta);
                Double y = Maths.Sin (amount * theta);
                Double z = Maths.Sin (theta);
                f = x / z;
                a = y / z;
            }
            r.U = (f * q1.U) + (a * q2.U);
            r.I = (f * q1.I) + (a * q2.I);
            r.J = (f * q1.J) + (a * q2.J);
            r.K = (f * q1.K) + (a * q2.K);
        }

        [MI(O.AggressiveInlining)] public static void IsUnit (ref Quaternion q, out Boolean r) {
            r = Maths.IsApproximatelyZero((Double) 1 - q.U * q.U - q.I * q.I - q.J * q.J - q.K * q.K);
        }

        [MI(O.AggressiveInlining)] public bool IsUnit () { Boolean r; IsUnit (ref this, out r); return r; }

        [MI(O.AggressiveInlining)] public static Boolean    IsUnit (Quaternion q) { Boolean r; IsUnit (ref q, out r); return r; }
        [MI(O.AggressiveInlining)] public static Quaternion Lerp   (Quaternion a, Quaternion b, Double amount) { Quaternion r; Lerp (ref a, ref b, ref amount, out r); return r; }
        [MI(O.AggressiveInlining)] public static Quaternion Slerp  (Quaternion a, Quaternion b, Double amount) { Quaternion r; Slerp (ref a, ref b, ref amount, out r); return r; }

        // Maths //-----------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void LengthSquared (ref Quaternion q, out Double r) {
            r = (q.I * q.I) + (q.J * q.J) + (q.K * q.K) + (q.U * q.U);
        }

        [MI(O.AggressiveInlining)] public static void Length (ref Quaternion q, out Double r) {
            Double lengthSquared = (q.I * q.I) + (q.J * q.J) + (q.K * q.K) + (q.U * q.U);
            r = Maths.Sqrt (lengthSquared);
        }

        [MI(O.AggressiveInlining)] public static void Conjugate (ref Quaternion value, out Quaternion r) {
            r.I = -value.I; r.J = -value.J;
            r.K = -value.K; r.U = value.U;
        }

        [MI(O.AggressiveInlining)] public static void Inverse (ref Quaternion q, out Quaternion r) {
            Double a = (q.I * q.I) + (q.J * q.J) + (q.K * q.K) + (q.U * q.U);
            Double b = 1 / a;
            r.I = -q.I * b; r.J = -q.J * b;
            r.K = -q.K * b; r.U =  q.U * b;
        }

        [MI(O.AggressiveInlining)] public static void Dot (ref Quaternion q1, ref Quaternion q2, out Double r) {
            r = (q1.I * q2.I) + (q1.J * q2.J) + (q1.K * q2.K) + (q1.U * q2.U);
        }

        [MI(O.AggressiveInlining)] public static void Concatenate (ref Quaternion q1, ref Quaternion q2, out Quaternion r) {
            Double a = (q1.K * q2.J) - (q1.J * q2.K);
            Double b = (q1.I * q2.K) - (q1.K * q2.I);
            Double c = (q1.J * q2.I) - (q1.I * q2.J);
            Double d = (q1.I * q2.I) - (q1.J * q2.J);
            Double i = (q1.U * q2.I) + (q1.I * q2.U) + a;
            Double j = (q1.U * q2.J) + (q1.J * q2.U) + b;
            Double k = (q1.U * q2.K) + (q1.K * q2.U) + c;
            Double u = (q1.U * q2.U) - (q1.K * q2.K) - d;
            r.I = i; r.J = j; r.K = k; r.U = u;
        }

        [MI(O.AggressiveInlining)] public static void Normalise (ref Quaternion q, out Quaternion r) {
            Double a = (q.I * q.I) + (q.J * q.J)
                     + (q.K * q.K) + (q.U * q.U);
            Double b = 1 / Maths.Sqrt (a);
            r.I = q.I * b; r.J = q.J * b;
            r.K = q.K * b; r.U = q.U * b;
        }

        [MI(O.AggressiveInlining)] public static void Transform (ref Quaternion rotation, ref Vector3 vector, out Vector3 r) {
            Double i = rotation.I, j = rotation.J, k = rotation.K, u = rotation.U;
            Double ii = i * i, jj = j * j, kk = k * k;
            Double ui = u * i, uj = u * j, uk = u * k;
            Double ij = i * j, ik = i * k, jk = j * k;
            Double x = vector.X - (2 * vector.X * (jj + kk)) + (2 * vector.Y * (ij - uk)) + (2 * vector.Z * (ik + uj));
            Double y = vector.Y + (2 * vector.X * (ij + uk)) - (2 * vector.Y * (ii + kk)) + (2 * vector.Z * (jk - ui));
            Double z = vector.Z + (2 * vector.X * (ik - uj)) + (2 * vector.Y * (jk + ui)) - (2 * vector.Z * (ii + jj));
            r.X = x; r.Y = y; r.Z = z;
        }

        [MI(O.AggressiveInlining)] public static void Transform (ref Quaternion rotation, ref Vector4 vector, out Vector4 r) {
            Double i = rotation.I, j = rotation.J, k = rotation.K, u = rotation.U;
            Double ii = i * i, jj = j * j, kk = k * k;
            Double ui = u * i, uj = u * j, uk = u * k;
            Double ij = i * j, ik = i * k, jk = j * k;
            Double x = vector.X - (vector.X * 2 * (jj + kk)) + (vector.Y * 2 * (ij - uk)) + (vector.Z * 2 * (ik + uj));
            Double y = vector.Y + (vector.X * 2 * (ij + uk)) - (vector.Y * 2 * (ii + kk)) + (vector.Z * 2 * (jk - ui));
            Double z = vector.Z + (vector.X * 2 * (ik - uj)) + (vector.Y * 2 * (jk + ui)) - (vector.Z * 2 * (ii + jj));
            Double w = vector.W;
            r.X = x; r.Y = y; r.Z = z; r.W = w;
        }

        [MI(O.AggressiveInlining)] public static void ToYawPitchRoll (ref Quaternion q, out Vector3 r) { // Angle of rotation, in radians. Angles are measured anti-clockwise when viewed from the rotation axis (positive side) toward the origin.
            // roll (x-axis rotation)
            Double sinr_cosp = 2 * (q.U * q.K + q.I * q.J);
            Double cosr_cosp = ((Double) 1) - 2 * (q.K * q.K + q.I * q.I);
            r.Z = Maths.ArcTan2 (sinr_cosp, cosr_cosp);
            // pitch (y-axis rotation)
            Double sinp = 2 * (q.U * q.I - q.J * q.K);
            if (Maths.Abs (sinp) >= 1f)
                r.Y = Maths.CopySign (Maths.HalfPi, sinp);
            else
                r.Y = Maths.ArcSin (sinp);
            // yaw (z-axis rotation)
            Double siny_cosp = 2 * (q.U * q.J + q.K * q.I);
            Double cosy_cosp = ((Double) 1) - 2 * (q.I * q.I + q.J * q.J);
            r.X = Maths.ArcTan2 (siny_cosp, cosy_cosp);
        }

        [MI(O.AggressiveInlining)] public Double     LengthSquared  () { Double r; LengthSquared (ref this, out r); return r; }
        [MI(O.AggressiveInlining)] public Double     Length         () { Double r; Length (ref this, out r); return r; }
        [MI(O.AggressiveInlining)] public void       Normalise      () { Normalise (ref this, out this); }
        [MI(O.AggressiveInlining)] public Quaternion Conjugate      () { Conjugate (ref this, out this); return this; }
        [MI(O.AggressiveInlining)] public Quaternion Inverse        () { Inverse (ref this, out this); return this; }
        [MI(O.AggressiveInlining)] public Double     Dot            (Quaternion q) { Double r; Dot (ref this, ref q, out r); return r; }
        [MI(O.AggressiveInlining)] public Quaternion Concatenate    (Quaternion q) { Concatenate (ref this, ref q, out this); return this; }
        [MI(O.AggressiveInlining)] public Vector3    Transform      (Vector3 v) { Vector3 r; Transform (ref this, ref v, out r); return r; }
        [MI(O.AggressiveInlining)] public Vector4    Transform      (Vector4 v) { Vector4 r; Transform (ref this, ref v, out r); return r; }
        [MI(O.AggressiveInlining)] public Vector3    ToYawPitchRoll () { Vector3 r; ToYawPitchRoll (ref this, out r); return r; }

        [MI(O.AggressiveInlining)] public static Double     LengthSquared  (Quaternion q) { Double r; LengthSquared (ref q, out r); return r; }
        [MI(O.AggressiveInlining)] public static Double     Length         (Quaternion q) { Double r; Length (ref q, out r); return r; }
        [MI(O.AggressiveInlining)] public static Quaternion Normalise      (Quaternion q) { Quaternion r; Normalise (ref q, out r); return r; }
        [MI(O.AggressiveInlining)] public static Quaternion Conjugate      (Quaternion q) { Quaternion r; Conjugate (ref q, out r); return r; }
        [MI(O.AggressiveInlining)] public static Quaternion Inverse        (Quaternion q) { Quaternion r; Inverse (ref q, out r); return r; }
        [MI(O.AggressiveInlining)] public static Double     Dot            (Quaternion a, Quaternion b) { Double r; Dot (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Quaternion Concatenate    (Quaternion a, Quaternion b) { Quaternion r; Concatenate (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3    Transform      (Quaternion rotation, Vector3 v) { Vector3 r; Transform (ref rotation, ref v, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4    Transform      (Quaternion rotation, Vector4 v) { Vector4 r; Transform (ref rotation, ref v, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3    ToYawPitchRoll (Quaternion q) { Vector3 r; ToYawPitchRoll (ref q, out r); return r; }
        // Creation //--------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void CreateFromAxisAngle (ref Vector3 axis, ref Double angle, out Quaternion r) {
            Double theta = angle * Maths.Half;
            Double sin = Maths.Sin (theta), cos = Maths.Cos (theta);
            r.I = axis.X * sin;
            r.J = axis.Y * sin;
            r.K = axis.Z * sin;
            r.U = cos;
        }

        [MI(O.AggressiveInlining)] public static void CreateFromYawPitchRoll (ref Double yaw, ref Double pitch, ref Double roll, out Quaternion r) {
            Double hr = roll * Maths.Half, hp = pitch * Maths.Half, hy = yaw * Maths.Half;
            Double shr = Maths.Sin (hr), chr = Maths.Cos (hr);
            Double shp = Maths.Sin (hp), chp = Maths.Cos (hp);
            Double shy = Maths.Sin (hy), chy = Maths.Cos (hy);
            r.I = (chy * shp * chr) + (shy * chp * shr);
            r.J = (shy * chp * chr) - (chy * shp * shr);
            r.K = (chy * chp * shr) - (shy * shp * chr);
            r.U = (chy * chp * chr) + (shy * shp * shr);
        }

        // http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/
        [MI(O.AggressiveInlining)] public static void CreateFromRotationMatrix (ref Matrix44 m, out Quaternion r) {
            Double tr = m.R0C0 + m.R1C1 + m.R2C2;
            if (tr > 0) {
                Double s = Maths.Sqrt (tr + 1) * 2;
                r.U = Maths.Quarter * s;
                r.I = (m.R1C2 - m.R2C1) / s;
                r.J = (m.R2C0 - m.R0C2) / s;
                r.K = (m.R0C1 - m.R1C0) / s;
            }
            else if ((m.R0C0 >= m.R1C1) && (m.R0C0 >= m.R2C2)) {
                Double s = Maths.Sqrt (1 + m.R0C0 - m.R1C1 - m.R2C2) * 2;
                r.U = (m.R1C2 - m.R2C1) / s;
                r.I = Maths.Quarter * s;
                r.J = (m.R0C1 + m.R1C0) / s;
                r.K = (m.R0C2 + m.R2C0) / s;
            }
            else if (m.R1C1 > m.R2C2) {
                Double s = Maths.Sqrt (1 + m.R1C1 - m.R0C0 - m.R2C2) * 2;
                r.U = (m.R2C0 - m.R0C2) / s;
                r.I = (m.R1C0 + m.R0C1) / s;
                r.J = Maths.Quarter * s;
                r.K = (m.R2C1 + m.R1C2) / s;
            }
            else {
                Double s = Maths.Sqrt (1 + m.R2C2 - m.R0C0 - m.R1C1) * 2;
                r.U = (m.R0C1 - m.R1C0) / s;
                r.I = (m.R2C0 + m.R0C2) / s;
                r.J = (m.R2C1 + m.R1C2) / s;
                r.K = Maths.Quarter * s;
            }
        }

        [MI(O.AggressiveInlining)] public static Quaternion CreateFromAxisAngle      (Vector3 axis, Double angle) { Quaternion r; CreateFromAxisAngle (ref axis, ref angle, out r); return r; }
        [MI(O.AggressiveInlining)] public static Quaternion CreateFromYawPitchRoll   (Double yaw, Double pitch, Double roll) { Quaternion r; CreateFromYawPitchRoll (ref yaw, ref pitch, ref roll, out r); return r; }
        [MI(O.AggressiveInlining)] public static Quaternion CreateFromRotationMatrix (Matrix44 matrix) { Quaternion r; CreateFromRotationMatrix (ref matrix, out r); return r; }
    }
    /// <summary>
    /// Double precision Matrix44.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Matrix44 : IEquatable<Matrix44> {
        public Double R0C0, R0C1, R0C2, R0C3;
        public Double R1C0, R1C1, R1C2, R1C3;
        public Double R2C0, R2C1, R2C2, R2C3;
        public Double R3C0, R3C1, R3C2, R3C3;

        [MI(O.AggressiveInlining)] public Matrix44 (
            Double m00, Double m01, Double m02, Double m03, Double m10, Double m11, Double m12, Double m13,
            Double m20, Double m21, Double m22, Double m23, Double m30, Double m31, Double m32, Double m33) {
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

        [MI(O.AggressiveInlining)] public override Int32 GetHashCode () {
            return R0C0.GetHashCode ()                  ^ R0C1.GetHashCode ().ShiftAndWrap (2)
                ^ R0C2.GetHashCode ().ShiftAndWrap (4)  ^ R0C3.GetHashCode ().ShiftAndWrap (6)
                ^ R1C0.GetHashCode ().ShiftAndWrap (8)  ^ R1C1.GetHashCode ().ShiftAndWrap (10)
                ^ R1C2.GetHashCode ().ShiftAndWrap (12) ^ R1C3.GetHashCode ().ShiftAndWrap (14)
                ^ R2C0.GetHashCode ().ShiftAndWrap (16) ^ R2C1.GetHashCode ().ShiftAndWrap (18)
                ^ R2C2.GetHashCode ().ShiftAndWrap (20) ^ R2C3.GetHashCode ().ShiftAndWrap (22)
                ^ R3C0.GetHashCode ().ShiftAndWrap (24) ^ R3C1.GetHashCode ().ShiftAndWrap (26)
                ^ R3C2.GetHashCode ().ShiftAndWrap (28) ^ R3C3.GetHashCode ().ShiftAndWrap (30);
        }

        [MI(O.AggressiveInlining)] public override Boolean Equals (Object obj) { return (obj is Matrix44) ? this.Equals ((Matrix44)obj) : false; }

        [MI(O.AggressiveInlining)] public Boolean Equals (Matrix44 other) { Boolean r; Equals (ref this, ref other, out r); return r; }

        [MI(O.AggressiveInlining)] public Boolean ApproximateEquals (Matrix44 other) { Boolean r; ApproximateEquals (ref this, ref other, out r); return r; }

        [MI(O.AggressiveInlining)] public Boolean IsSymmetric () {
            Matrix44 transpose = this;
            Transpose (ref transpose, out transpose);
            return transpose.Equals (this);
        }

        [MI(O.AggressiveInlining)] public Boolean IsSkewSymmetric () {
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

        [MI(O.AggressiveInlining)] public static void Equals (ref Matrix44 a, ref Matrix44 b, out Boolean r) {
            r = (a.R0C0 == b.R0C0) && (a.R1C1 == b.R1C1) &&
                     (a.R2C2 == b.R2C2) && (a.R3C3 == b.R3C3) &&
                     (a.R0C1 == b.R0C1) && (a.R0C2 == b.R0C2) &&
                     (a.R0C3 == b.R0C3) && (a.R1C0 == b.R1C0) &&
                     (a.R1C2 == b.R1C2) && (a.R1C3 == b.R1C3) &&
                     (a.R2C0 == b.R2C0) && (a.R2C1 == b.R2C1) &&
                     (a.R2C3 == b.R2C3) && (a.R3C0 == b.R3C0) &&
                     (a.R3C1 == b.R3C1) && (a.R3C2 == b.R3C2);
        }

        [MI(O.AggressiveInlining)] public static void ApproximateEquals (ref Matrix44 a, ref Matrix44 b, out Boolean r) {
            r = Maths.ApproximateEquals (a.R0C0, b.R0C0) && Maths.ApproximateEquals (a.R1C1, b.R1C1) &&
                     Maths.ApproximateEquals (a.R2C2, b.R2C2) && Maths.ApproximateEquals (a.R3C3, b.R3C3) &&
                     Maths.ApproximateEquals (a.R0C1, b.R0C1) && Maths.ApproximateEquals (a.R0C2, b.R0C2) &&
                     Maths.ApproximateEquals (a.R0C3, b.R0C3) && Maths.ApproximateEquals (a.R1C0, b.R1C0) &&
                     Maths.ApproximateEquals (a.R1C2, b.R1C2) && Maths.ApproximateEquals (a.R1C3, b.R1C3) &&
                     Maths.ApproximateEquals (a.R2C0, b.R2C0) && Maths.ApproximateEquals (a.R2C1, b.R2C1) &&
                     Maths.ApproximateEquals (a.R2C3, b.R2C3) && Maths.ApproximateEquals (a.R3C0, b.R3C0) &&
                     Maths.ApproximateEquals (a.R3C1, b.R3C1) && Maths.ApproximateEquals (a.R3C2, b.R3C2);
        }

        [MI(O.AggressiveInlining)] public static void Add (ref Matrix44 a, ref Matrix44 b, out Matrix44 r) {
            r.R0C0 = a.R0C0 + b.R0C0; r.R0C1 = a.R0C1 + b.R0C1;
            r.R0C2 = a.R0C2 + b.R0C2; r.R0C3 = a.R0C3 + b.R0C3;
            r.R1C0 = a.R1C0 + b.R1C0; r.R1C1 = a.R1C1 + b.R1C1;
            r.R1C2 = a.R1C2 + b.R1C2; r.R1C3 = a.R1C3 + b.R1C3;
            r.R2C0 = a.R2C0 + b.R2C0; r.R2C1 = a.R2C1 + b.R2C1;
            r.R2C2 = a.R2C2 + b.R2C2; r.R2C3 = a.R2C3 + b.R2C3;
            r.R3C0 = a.R3C0 + b.R3C0; r.R3C1 = a.R3C1 + b.R3C1;
            r.R3C2 = a.R3C2 + b.R3C2; r.R3C3 = a.R3C3 + b.R3C3;
        }

        [MI(O.AggressiveInlining)] public static void Subtract (ref Matrix44 a, ref Matrix44 b, out Matrix44 r) {
            r.R0C0 = a.R0C0 - b.R0C0; r.R0C1 = a.R0C1 - b.R0C1;
            r.R0C2 = a.R0C2 - b.R0C2; r.R0C3 = a.R0C3 - b.R0C3;
            r.R1C0 = a.R1C0 - b.R1C0; r.R1C1 = a.R1C1 - b.R1C1;
            r.R1C2 = a.R1C2 - b.R1C2; r.R1C3 = a.R1C3 - b.R1C3;
            r.R2C0 = a.R2C0 - b.R2C0; r.R2C1 = a.R2C1 - b.R2C1;
            r.R2C2 = a.R2C2 - b.R2C2; r.R2C3 = a.R2C3 - b.R2C3;
            r.R3C0 = a.R3C0 - b.R3C0; r.R3C1 = a.R3C1 - b.R3C1;
            r.R3C2 = a.R3C2 - b.R3C2; r.R3C3 = a.R3C3 - b.R3C3;
        }

        [MI(O.AggressiveInlining)] public static void Negate (ref Matrix44 m, out Matrix44 r) {
            r.R0C0 = -m.R0C0; r.R0C1 = -m.R0C1;
            r.R0C2 = -m.R0C2; r.R0C3 = -m.R0C3;
            r.R1C0 = -m.R1C0; r.R1C1 = -m.R1C1;
            r.R1C2 = -m.R1C2; r.R1C3 = -m.R1C3;
            r.R2C0 = -m.R2C0; r.R2C1 = -m.R2C1;
            r.R2C2 = -m.R2C2; r.R2C3 = -m.R2C3;
            r.R3C0 = -m.R3C0; r.R3C1 = -m.R3C1;
            r.R3C2 = -m.R3C2; r.R3C3 = -m.R3C3;
        }

        [MI(O.AggressiveInlining)] public static void Product (ref Matrix44 a, ref Matrix44 b, out Matrix44 r) {
            Double r0c0 = (a.R0C0 * b.R0C0) + (a.R0C1 * b.R1C0) + (a.R0C2 * b.R2C0) + (a.R0C3 * b.R3C0);
            Double r0c1 = (a.R0C0 * b.R0C1) + (a.R0C1 * b.R1C1) + (a.R0C2 * b.R2C1) + (a.R0C3 * b.R3C1);
            Double r0c2 = (a.R0C0 * b.R0C2) + (a.R0C1 * b.R1C2) + (a.R0C2 * b.R2C2) + (a.R0C3 * b.R3C2);
            Double r0c3 = (a.R0C0 * b.R0C3) + (a.R0C1 * b.R1C3) + (a.R0C2 * b.R2C3) + (a.R0C3 * b.R3C3);
            Double r1c0 = (a.R1C0 * b.R0C0) + (a.R1C1 * b.R1C0) + (a.R1C2 * b.R2C0) + (a.R1C3 * b.R3C0);
            Double r1c1 = (a.R1C0 * b.R0C1) + (a.R1C1 * b.R1C1) + (a.R1C2 * b.R2C1) + (a.R1C3 * b.R3C1);
            Double r1c2 = (a.R1C0 * b.R0C2) + (a.R1C1 * b.R1C2) + (a.R1C2 * b.R2C2) + (a.R1C3 * b.R3C2);
            Double r1c3 = (a.R1C0 * b.R0C3) + (a.R1C1 * b.R1C3) + (a.R1C2 * b.R2C3) + (a.R1C3 * b.R3C3);
            Double r2c0 = (a.R2C0 * b.R0C0) + (a.R2C1 * b.R1C0) + (a.R2C2 * b.R2C0) + (a.R2C3 * b.R3C0);
            Double r2c1 = (a.R2C0 * b.R0C1) + (a.R2C1 * b.R1C1) + (a.R2C2 * b.R2C1) + (a.R2C3 * b.R3C1);
            Double r2c2 = (a.R2C0 * b.R0C2) + (a.R2C1 * b.R1C2) + (a.R2C2 * b.R2C2) + (a.R2C3 * b.R3C2);
            Double r2c3 = (a.R2C0 * b.R0C3) + (a.R2C1 * b.R1C3) + (a.R2C2 * b.R2C3) + (a.R2C3 * b.R3C3);
            Double r3c0 = (a.R3C0 * b.R0C0) + (a.R3C1 * b.R1C0) + (a.R3C2 * b.R2C0) + (a.R3C3 * b.R3C0);
            Double r3c1 = (a.R3C0 * b.R0C1) + (a.R3C1 * b.R1C1) + (a.R3C2 * b.R2C1) + (a.R3C3 * b.R3C1);
            Double r3c2 = (a.R3C0 * b.R0C2) + (a.R3C1 * b.R1C2) + (a.R3C2 * b.R2C2) + (a.R3C3 * b.R3C2);
            Double r3c3 = (a.R3C0 * b.R0C3) + (a.R3C1 * b.R1C3) + (a.R3C2 * b.R2C3) + (a.R3C3 * b.R3C3);
            r.R0C0 = r0c0; r.R0C1 = r0c1; r.R0C2 = r0c2; r.R0C3 = r0c3;
            r.R1C0 = r1c0; r.R1C1 = r1c1; r.R1C2 = r1c2; r.R1C3 = r1c3;
            r.R2C0 = r2c0; r.R2C1 = r2c1; r.R2C2 = r2c2; r.R2C3 = r2c3;
            r.R3C0 = r3c0; r.R3C1 = r3c1; r.R3C2 = r3c2; r.R3C3 = r3c3; 
        }

        [MI(O.AggressiveInlining)] public static void Multiply (ref Matrix44 m, ref Double f, out Matrix44 r) {
            r.R0C0 = m.R0C0 * f; r.R0C1 = m.R0C1 * f;
            r.R0C2 = m.R0C2 * f; r.R0C3 = m.R0C3 * f;
            r.R1C0 = m.R1C0 * f; r.R1C1 = m.R1C1 * f;
            r.R1C2 = m.R1C2 * f; r.R1C3 = m.R1C3 * f;
            r.R2C0 = m.R2C0 * f; r.R2C1 = m.R2C1 * f;
            r.R2C2 = m.R2C2 * f; r.R2C3 = m.R2C3 * f;
            r.R3C0 = m.R3C0 * f; r.R3C1 = m.R3C1 * f;
            r.R3C2 = m.R3C2 * f; r.R3C3 = m.R3C3 * f;
        }

        [MI(O.AggressiveInlining)] public static Boolean  operator == (Matrix44 a, Matrix44 b) { Boolean  r; Equals    (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Boolean  operator != (Matrix44 a, Matrix44 b) { Boolean  r; Equals    (ref a, ref b, out r); return !r; }
        [MI(O.AggressiveInlining)] public static Matrix44 operator  + (Matrix44 a, Matrix44 b) { Matrix44 r; Add       (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Matrix44 operator  - (Matrix44 a, Matrix44 b) { Matrix44 r; Subtract  (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Matrix44 operator  - (Matrix44 m)             { Matrix44 r; Negate    (ref m,        out r); return  r; }
        [MI(O.AggressiveInlining)] public static Matrix44 operator  * (Matrix44 a, Matrix44 b) { Matrix44 r; Product   (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Matrix44 operator  * (Matrix44 m, Double f)   { Matrix44 r; Multiply  (ref m, ref f, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Matrix44 operator  * (Double f, Matrix44 m)   { Matrix44 r; Multiply  (ref m, ref f, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3  operator  * (Vector3 v, Matrix44 m)  { Vector3  r; Transform (ref m, ref v, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector4  operator  * (Vector4 v, Matrix44 m)  { Vector4  r; Transform (ref m, ref v, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3  operator  * (Matrix44 m, Vector3 v)  { Vector3  r; Transform (ref m, ref v, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector4  operator  * (Matrix44 m, Vector4 v)  { Vector4  r; Transform (ref m, ref v, out r); return  r; }

        [MI(O.AggressiveInlining)] public static Boolean  Equals            (Matrix44 a, Matrix44 b) { Boolean  r; Equals            (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Boolean  ApproximateEquals (Matrix44 a, Matrix44 b) { Boolean  r; ApproximateEquals (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 Add               (Matrix44 a, Matrix44 b) { Matrix44 r; Add               (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 Subtract          (Matrix44 a, Matrix44 b) { Matrix44 r; Subtract          (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 Negate            (Matrix44 m)             { Matrix44 r; Negate            (ref m,        out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 Product           (Matrix44 a, Matrix44 b) { Matrix44 r; Product           (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 Multiply          (Matrix44 m, Double f)   { Matrix44 r; Multiply          (ref m, ref f, out r); return r; }

        // Utilities //-------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void Lerp (ref Matrix44 a, ref Matrix44 b, ref Double amount, out Matrix44 r) {
            Debug.Assert (amount > 0 && amount <= 1);
            r.R0C0 = a.R0C0 + ((b.R0C0 - a.R0C0) * amount);
            r.R0C1 = a.R0C1 + ((b.R0C1 - a.R0C1) * amount);
            r.R0C2 = a.R0C2 + ((b.R0C2 - a.R0C2) * amount);
            r.R0C3 = a.R0C3 + ((b.R0C3 - a.R0C3) * amount);
            r.R1C0 = a.R1C0 + ((b.R1C0 - a.R1C0) * amount);
            r.R1C1 = a.R1C1 + ((b.R1C1 - a.R1C1) * amount);
            r.R1C2 = a.R1C2 + ((b.R1C2 - a.R1C2) * amount);
            r.R1C3 = a.R1C3 + ((b.R1C3 - a.R1C3) * amount);
            r.R2C0 = a.R2C0 + ((b.R2C0 - a.R2C0) * amount);
            r.R2C1 = a.R2C1 + ((b.R2C1 - a.R2C1) * amount);
            r.R2C2 = a.R2C2 + ((b.R2C2 - a.R2C2) * amount);
            r.R2C3 = a.R2C3 + ((b.R2C3 - a.R2C3) * amount);
            r.R3C0 = a.R3C0 + ((b.R3C0 - a.R3C0) * amount);
            r.R3C1 = a.R3C1 + ((b.R3C1 - a.R3C1) * amount);
            r.R3C2 = a.R3C2 + ((b.R3C2 - a.R3C2) * amount);
            r.R3C3 = a.R3C3 + ((b.R3C3 - a.R3C3) * amount);
        }
        
        [MI(O.AggressiveInlining)] public static Matrix44 Lerp (Matrix44 a, Matrix44 b, Double amount) { Matrix44 r; Lerp (ref a, ref b, ref amount, out r); return r; }

        // Maths //-----------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void Transpose (ref Matrix44 m, out Matrix44 r) {
            r.R0C0 = m.R0C0; r.R1C1 = m.R1C1;
            r.R2C2 = m.R2C2; r.R3C3 = m.R3C3;
            Double t = m.R0C1; r.R0C1 = m.R1C0; r.R1C0 = t;
                   t = m.R0C2; r.R0C2 = m.R2C0; r.R2C0 = t;
                   t = m.R0C3; r.R0C3 = m.R3C0; r.R3C0 = t;
                   t = m.R1C2; r.R1C2 = m.R2C1; r.R2C1 = t;
                   t = m.R1C3; r.R1C3 = m.R3C1; r.R3C1 = t;
                   t = m.R2C3; r.R2C3 = m.R3C2; r.R3C2 = t;
        }

        [MI(O.AggressiveInlining)] public static void Decompose (ref Matrix44 matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation, out Boolean r) {
            translation.X = matrix.R3C0; translation.Y = matrix.R3C1; translation.Z = matrix.R3C2;
            Vector3 a = new Vector3 (matrix.R0C0, matrix.R1C0, matrix.R2C0);
            Vector3 b = new Vector3 (matrix.R0C1, matrix.R1C1, matrix.R2C1);
            Vector3 c = new Vector3 (matrix.R0C2, matrix.R1C2, matrix.R2C2);
            Double aLen; Vector3.Length (ref a, out aLen); scale.X = aLen;
            Double bLen; Vector3.Length (ref b, out bLen); scale.Y = bLen;
            Double cLen; Vector3.Length (ref c, out cLen); scale.Z = cLen;
            if (Maths.IsApproximatelyZero (scale.X) || Maths.IsApproximatelyZero (scale.Y) || Maths.IsApproximatelyZero (scale.Z)) {
                rotation = Quaternion.Identity;
                r = false;
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
            r = true;
        }

        [MI(O.AggressiveInlining)] public static void Determinant (ref Matrix44 m, out Double r) {
            r = + m.R0C3 * m.R1C2 * m.R2C1 * m.R3C0 - m.R0C2 * m.R1C3 * m.R2C1 * m.R3C0
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

        [MI(O.AggressiveInlining)] public static void Invert (ref Matrix44 m, out Matrix44 r) {
            Double d; Determinant (ref m, out d); Double s = 1 / d;
            Double r0c0 = m.R1C2 * m.R2C3 * m.R3C1 - m.R1C3 * m.R2C2 * m.R3C1 + m.R1C3 * m.R2C1 * m.R3C2 - m.R1C1 * m.R2C3 * m.R3C2 - m.R1C2 * m.R2C1 * m.R3C3 + m.R1C1 * m.R2C2 * m.R3C3;
            Double r0c1 = m.R0C3 * m.R2C2 * m.R3C1 - m.R0C2 * m.R2C3 * m.R3C1 - m.R0C3 * m.R2C1 * m.R3C2 + m.R0C1 * m.R2C3 * m.R3C2 + m.R0C2 * m.R2C1 * m.R3C3 - m.R0C1 * m.R2C2 * m.R3C3;
            Double r0c2 = m.R0C2 * m.R1C3 * m.R3C1 - m.R0C3 * m.R1C2 * m.R3C1 + m.R0C3 * m.R1C1 * m.R3C2 - m.R0C1 * m.R1C3 * m.R3C2 - m.R0C2 * m.R1C1 * m.R3C3 + m.R0C1 * m.R1C2 * m.R3C3;
            Double r0c3 = m.R0C3 * m.R1C2 * m.R2C1 - m.R0C2 * m.R1C3 * m.R2C1 - m.R0C3 * m.R1C1 * m.R2C2 + m.R0C1 * m.R1C3 * m.R2C2 + m.R0C2 * m.R1C1 * m.R2C3 - m.R0C1 * m.R1C2 * m.R2C3;
            Double r1c0 = m.R1C3 * m.R2C2 * m.R3C0 - m.R1C2 * m.R2C3 * m.R3C0 - m.R1C3 * m.R2C0 * m.R3C2 + m.R1C0 * m.R2C3 * m.R3C2 + m.R1C2 * m.R2C0 * m.R3C3 - m.R1C0 * m.R2C2 * m.R3C3;
            Double r1c1 = m.R0C2 * m.R2C3 * m.R3C0 - m.R0C3 * m.R2C2 * m.R3C0 + m.R0C3 * m.R2C0 * m.R3C2 - m.R0C0 * m.R2C3 * m.R3C2 - m.R0C2 * m.R2C0 * m.R3C3 + m.R0C0 * m.R2C2 * m.R3C3;
            Double r1c2 = m.R0C3 * m.R1C2 * m.R3C0 - m.R0C2 * m.R1C3 * m.R3C0 - m.R0C3 * m.R1C0 * m.R3C2 + m.R0C0 * m.R1C3 * m.R3C2 + m.R0C2 * m.R1C0 * m.R3C3 - m.R0C0 * m.R1C2 * m.R3C3;
            Double r1c3 = m.R0C2 * m.R1C3 * m.R2C0 - m.R0C3 * m.R1C2 * m.R2C0 + m.R0C3 * m.R1C0 * m.R2C2 - m.R0C0 * m.R1C3 * m.R2C2 - m.R0C2 * m.R1C0 * m.R2C3 + m.R0C0 * m.R1C2 * m.R2C3;
            Double r2c0 = m.R1C1 * m.R2C3 * m.R3C0 - m.R1C3 * m.R2C1 * m.R3C0 + m.R1C3 * m.R2C0 * m.R3C1 - m.R1C0 * m.R2C3 * m.R3C1 - m.R1C1 * m.R2C0 * m.R3C3 + m.R1C0 * m.R2C1 * m.R3C3;
            Double r2c1 = m.R0C3 * m.R2C1 * m.R3C0 - m.R0C1 * m.R2C3 * m.R3C0 - m.R0C3 * m.R2C0 * m.R3C1 + m.R0C0 * m.R2C3 * m.R3C1 + m.R0C1 * m.R2C0 * m.R3C3 - m.R0C0 * m.R2C1 * m.R3C3;
            Double r2c2 = m.R0C1 * m.R1C3 * m.R3C0 - m.R0C3 * m.R1C1 * m.R3C0 + m.R0C3 * m.R1C0 * m.R3C1 - m.R0C0 * m.R1C3 * m.R3C1 - m.R0C1 * m.R1C0 * m.R3C3 + m.R0C0 * m.R1C1 * m.R3C3;
            Double r2c3 = m.R0C3 * m.R1C1 * m.R2C0 - m.R0C1 * m.R1C3 * m.R2C0 - m.R0C3 * m.R1C0 * m.R2C1 + m.R0C0 * m.R1C3 * m.R2C1 + m.R0C1 * m.R1C0 * m.R2C3 - m.R0C0 * m.R1C1 * m.R2C3;
            Double r3c0 = m.R1C2 * m.R2C1 * m.R3C0 - m.R1C1 * m.R2C2 * m.R3C0 - m.R1C2 * m.R2C0 * m.R3C1 + m.R1C0 * m.R2C2 * m.R3C1 + m.R1C1 * m.R2C0 * m.R3C2 - m.R1C0 * m.R2C1 * m.R3C2;
            Double r3c1 = m.R0C1 * m.R2C2 * m.R3C0 - m.R0C2 * m.R2C1 * m.R3C0 + m.R0C2 * m.R2C0 * m.R3C1 - m.R0C0 * m.R2C2 * m.R3C1 - m.R0C1 * m.R2C0 * m.R3C2 + m.R0C0 * m.R2C1 * m.R3C2;
            Double r3c2 = m.R0C2 * m.R1C1 * m.R3C0 - m.R0C1 * m.R1C2 * m.R3C0 - m.R0C2 * m.R1C0 * m.R3C1 + m.R0C0 * m.R1C2 * m.R3C1 + m.R0C1 * m.R1C0 * m.R3C2 - m.R0C0 * m.R1C1 * m.R3C2;
            Double r3c3 = m.R0C1 * m.R1C2 * m.R2C0 - m.R0C2 * m.R1C1 * m.R2C0 + m.R0C2 * m.R1C0 * m.R2C1 - m.R0C0 * m.R1C2 * m.R2C1 - m.R0C1 * m.R1C0 * m.R2C2 + m.R0C0 * m.R1C1 * m.R2C2;
            r.R0C0 = r0c0; r.R0C1 = r0c1; r.R0C2 = r0c2; r.R0C3 = r0c3;
            r.R1C0 = r1c0; r.R1C1 = r1c1; r.R1C2 = r1c2; r.R1C3 = r1c3;
            r.R2C0 = r2c0; r.R2C1 = r2c1; r.R2C2 = r2c2; r.R2C3 = r2c3;
            r.R3C0 = r3c0; r.R3C1 = r3c1; r.R3C2 = r3c2; r.R3C3 = r3c3; 
            Multiply (ref r, ref s, out r);
        }

        [MI(O.AggressiveInlining)] public static void Transform (ref Matrix44 m, ref Quaternion q, out Matrix44 r) {
            Boolean qIsUnit; Quaternion.IsUnit (ref q, out qIsUnit);
            Debug.Assert (qIsUnit);
            Double twoI = q.I + q.I, twoJ = q.J + q.J, twoK = q.K + q.K;
            Double twoUI = q.U * twoI, twoUJ = q.U * twoJ, twoUK = q.U * twoK;
            Double twoII = q.I * twoI, twoIJ = q.I * twoJ, twoIK = q.I * twoK;
            Double twoJJ = q.J * twoJ, twoJK = q.J * twoK, twoKK = q.K * twoK;
            Double tR0C0 = 1 - twoJJ - twoKK;
            Double tR1C0 = twoIJ - twoUK;
            Double tR2C0 = twoIK + twoUJ;
            Double tR0C1 = twoIJ + twoUK;
            Double tR1C1 = 1 - twoII - twoKK;
            Double tR2C1 = twoJK - twoUI;
            Double tR0C2 = twoIK - twoUJ;
            Double tR1C2 = twoJK + twoUI;
            Double tR2C2 = 1 - twoII - twoJJ;
            Double r0c0 = m.R0C0 * tR0C0 + m.R0C1 * tR1C0 + m.R0C2 * tR2C0;
            Double r0c1 = m.R0C0 * tR0C1 + m.R0C1 * tR1C1 + m.R0C2 * tR2C1;
            Double r0c2 = m.R0C0 * tR0C2 + m.R0C1 * tR1C2 + m.R0C2 * tR2C2;
            Double r1c0 = m.R1C0 * tR0C0 + m.R1C1 * tR1C0 + m.R1C2 * tR2C0;
            Double r1c1 = m.R1C0 * tR0C1 + m.R1C1 * tR1C1 + m.R1C2 * tR2C1;
            Double r1c2 = m.R1C0 * tR0C2 + m.R1C1 * tR1C2 + m.R1C2 * tR2C2;
            Double r2c0 = m.R2C0 * tR0C0 + m.R2C1 * tR1C0 + m.R2C2 * tR2C0;
            Double r2c1 = m.R2C0 * tR0C1 + m.R2C1 * tR1C1 + m.R2C2 * tR2C1;
            Double r2c2 = m.R2C0 * tR0C2 + m.R2C1 * tR1C2 + m.R2C2 * tR2C2;
            Double r3c0 = m.R3C0 * tR0C0 + m.R3C1 * tR1C0 + m.R3C2 * tR2C0;
            Double r3c1 = m.R3C0 * tR0C1 + m.R3C1 * tR1C1 + m.R3C2 * tR2C1;
            Double r3c2 = m.R3C0 * tR0C2 + m.R3C1 * tR1C2 + m.R3C2 * tR2C2;
            r.R0C0 = r0c0; r.R0C1 = r0c1; r.R0C2 = r0c2; r.R0C3 = m.R0C3;
            r.R1C0 = r1c0; r.R1C1 = r1c1; r.R1C2 = r1c2; r.R1C3 = m.R1C3;
            r.R2C0 = r2c0; r.R2C1 = r2c1; r.R2C2 = r2c2; r.R2C3 = m.R2C3;
            r.R3C0 = r3c0; r.R3C1 = r3c1; r.R3C2 = r3c2; r.R3C3 = m.R3C3; 
        }

        [MI(O.AggressiveInlining)] public static void Transform (ref Matrix44 m, ref Vector3 v, out Vector3 r) {
            Double x = (v.X * m.R0C0) + (v.Y * m.R1C0) + (v.Z * m.R2C0) + m.R3C0;
            Double y = (v.X * m.R0C1) + (v.Y * m.R1C1) + (v.Z * m.R2C1) + m.R3C1;
            Double z = (v.X * m.R0C2) + (v.Y * m.R1C2) + (v.Z * m.R2C2) + m.R3C2;
            Double w = (v.X * m.R0C3) + (v.Y * m.R1C3) + (v.Z * m.R2C3) + m.R3C3;
            r.X = x / w; r.Y = y / w; r.Z = z / w;
        }

        [MI(O.AggressiveInlining)] public static void Transform (ref Matrix44 m, ref Vector4 v, out Vector4 r) {
            Double x = (v.X * m.R0C0) + (v.Y * m.R1C0) + (v.Z * m.R2C0) + (v.W * m.R3C0);
            Double y = (v.X * m.R0C1) + (v.Y * m.R1C1) + (v.Z * m.R2C1) + (v.W * m.R3C1);
            Double z = (v.X * m.R0C2) + (v.Y * m.R1C2) + (v.Z * m.R2C2) + (v.W * m.R3C2);
            Double w = (v.X * m.R0C3) + (v.Y * m.R1C3) + (v.Z * m.R2C3) + (v.W * m.R3C3);
            r.X = x; r.Y = y; r.Z = z; r.W = w;
        }

        [MI(O.AggressiveInlining)] public Double   Determinant ()                    { Double r; Determinant (ref this, out r); return r; }
        [MI(O.AggressiveInlining)] public Matrix44 Transpose   ()                    { Transpose (ref this, out this); return this; }
        [MI(O.AggressiveInlining)] public Matrix44 Invert      ()                    { Invert (ref this, out this); return this; }
        [MI(O.AggressiveInlining)] public Matrix44 Transform   (Quaternion rotation) { Matrix44 r; Transform (ref this, ref rotation, out r); return r; }
        [MI(O.AggressiveInlining)] public Vector3  Transform   (Vector3 v)           { Vector3 r; Transform (ref this, ref v, out r); return r; } 
        [MI(O.AggressiveInlining)] public Vector4  Transform   (Vector4 v)           { Vector4 r; Transform (ref this, ref v, out r); return r; } 

        [MI(O.AggressiveInlining)] public static Double   Determinant (Matrix44 matrix)                      { Double r; Determinant (ref matrix, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 Transpose   (Matrix44 input)                       { Matrix44 r; Transpose (ref input, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 Invert      (Matrix44 matrix)                      { Matrix44 r; Invert (ref matrix, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 Transform   (Matrix44 matrix, Quaternion rotation) { Matrix44 r; Transform (ref matrix, ref rotation, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3  Transform   (Matrix44 matrix, Vector3 v)           { Vector3 r; Transform (ref matrix, ref v, out r); return r; } 
        [MI(O.AggressiveInlining)] public static Vector4  Transform   (Matrix44 matrix, Vector4 v)           { Vector4 r; Transform (ref matrix, ref v, out r); return r; } 

        // Creation //--------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void CreateTranslation (ref Vector3 position, out Matrix44 r) {
            r.R0C0 = 1;          r.R0C1 = 0;          r.R0C2 = 0;          r.R0C3 = 0;
            r.R1C0 = 0;          r.R1C1 = 1;          r.R1C2 = 0;          r.R1C3 = 0;
            r.R2C0 = 0;          r.R2C1 = 0;          r.R2C2 = 1;          r.R2C3 = 0;
            r.R3C0 = position.X; r.R3C1 = position.Y; r.R3C2 = position.Z; r.R3C3 = 1;
        }

        [MI(O.AggressiveInlining)] public static void CreateTranslation (ref Double x, ref Double y, ref Double z, out Matrix44 r) {
            r.R0C0 = 1;          r.R0C1 = 0;          r.R0C2 = 0;          r.R0C3 = 0;
            r.R1C0 = 0;          r.R1C1 = 1;          r.R1C2 = 0;          r.R1C3 = 0;
            r.R2C0 = 0;          r.R2C1 = 0;          r.R2C2 = 1;          r.R2C3 = 0;
            r.R3C0 = x;          r.R3C1 = y;          r.R3C2 = z;          r.R3C3 = 1;
        }

        [MI(O.AggressiveInlining)] public static void CreateScale (ref Vector3 scale, out Matrix44 r) {
            r.R0C0 = scale.X;    r.R0C1 = 0;          r.R0C2 = 0;          r.R0C3 = 0;
            r.R1C0 = 0;          r.R1C1 = scale.Y;    r.R1C2 = 0;          r.R1C3 = 0;
            r.R2C0 = 0;          r.R2C1 = 0;          r.R2C2 = scale.Z;    r.R2C3 = 0;
            r.R3C0 = 0;          r.R3C1 = 0;          r.R3C2 = 0;          r.R3C3 = 1;
        }

        [MI(O.AggressiveInlining)] public static void CreateScale (ref Double x, ref Double y, ref Double z, out Matrix44 r) {
            r.R0C0 = x;          r.R0C1 = 0;          r.R0C2 = 0;          r.R0C3 = 0;
            r.R1C0 = 0;          r.R1C1 = y;          r.R1C2 = 0;          r.R1C3 = 0;
            r.R2C0 = 0;          r.R2C1 = 0;          r.R2C2 = z;          r.R2C3 = 0;
            r.R3C0 = 0;          r.R3C1 = 0;          r.R3C2 = 0;          r.R3C3 = 1;
        }

        [MI(O.AggressiveInlining)] public static void CreateScale (ref Double scale, out Matrix44 r) {
            r.R0C0 = scale;      r.R0C1 = 0;          r.R0C2 = 0;          r.R0C3 = 0;
            r.R1C0 = 0;          r.R1C1 = scale;      r.R1C2 = 0;          r.R1C3 = 0;
            r.R2C0 = 0;          r.R2C1 = 0;          r.R2C2 = scale;      r.R2C3 = 0;
            r.R3C0 = 0;          r.R3C1 = 0;          r.R3C2 = 0;          r.R3C3 = 1;
        }

        [MI(O.AggressiveInlining)] public static void CreateRotationX (ref Double radians, out Matrix44 r) {
            Double cos = Maths.Cos (radians), sin = Maths.Sin (radians);
            r.R0C0 = 1;          r.R0C1 = 0;          r.R0C2 = 0;          r.R0C3 = 0;
            r.R1C0 = 0;          r.R1C1 = cos;        r.R1C2 = sin;        r.R1C3 = 0;
            r.R2C0 = 0;          r.R2C1 = -sin;       r.R2C2 = cos;        r.R2C3 = 0;
            r.R3C0 = 0;          r.R3C1 = 0;          r.R3C2 = 0;          r.R3C3 = 1;
        }

        [MI(O.AggressiveInlining)] public static void CreateRotationY (ref Double radians, out Matrix44 r) {
            Double cos = Maths.Cos (radians), sin = Maths.Sin (radians);
            r.R0C0 = cos;        r.R0C1 = 0;          r.R0C2 = -sin;       r.R0C3 = 0;
            r.R1C0 = 0;          r.R1C1 = 1;          r.R1C2 = 0;          r.R1C3 = 0;
            r.R2C0 = sin;        r.R2C1 = 0;          r.R2C2 = cos;        r.R2C3 = 0;
            r.R3C0 = 0;          r.R3C1 = 0;          r.R3C2 = 0;          r.R3C3 = 1;
        }

        [MI(O.AggressiveInlining)] public static void CreateRotationZ (ref Double radians, out Matrix44 r) {
            Double cos = Maths.Cos (radians), sin = Maths.Sin (radians);
            r.R0C0 = cos;       r.R0C1 = sin;         r.R0C2 = 0;          r.R0C3 = 0;
            r.R1C0 = -sin;      r.R1C1 = cos;         r.R1C2 = 0;          r.R1C3 = 0;
            r.R2C0 = 0;         r.R2C1 = 0;           r.R2C2 = 1;          r.R2C3 = 0;
            r.R3C0 = 0;         r.R3C1 = 0;           r.R3C2 = 0;          r.R3C3 = 1;
        }

        [MI(O.AggressiveInlining)] public static void CreateFromAxisAngle (ref Vector3 axis, ref Double angle, out Matrix44 r) {
            Double x = axis.X, y = axis.Y, z = axis.Z;
            Double sin = Maths.Sin (angle), cos = Maths.Cos (angle);
            Double xx = x * x, yy = y * y, zz = z * z;
            Double xy = x * y, xz = x * z, yz = y * z;
            r.R0C0 = xx + (cos * (1 - xx));       r.R0C1 = xy - (cos * xy) + (sin * z); r.R0C2 = xz - (cos * xz) - (sin * y); r.R0C3 = 0;
            r.R1C0 = xy - (cos * xy) - (sin * z); r.R1C1 = yy + (cos * (1 - yy));       r.R1C2 = yz - (cos * yz) + (sin * x); r.R1C3 = 0;
            r.R2C0 = xz - (cos * xz) + (sin * y); r.R2C1 = yz - (cos * yz) - (sin * x); r.R2C2 = zz + (cos * (1 - zz));       r.R2C3 = 0;
            r.R3C0 = 0;                           r.R3C1 = 0;                           r.R3C2 = 0;                           r.R3C3 = 1;
        }

        // Axes must be pair-wise perpendicular and have unit length.
        [MI(O.AggressiveInlining)] public static void CreateFromCartesianAxes (ref Vector3 right, ref Vector3 up, ref Vector3 backward, out Matrix44 r) {
            r.R0C0 = right.X;    r.R0C1 = right.Y;    r.R0C2 = right.Z;    r.R0C3 = 0;
            r.R1C0 = up.X;       r.R1C1 = up.Y;       r.R1C2 = up.Z;       r.R1C3 = 0;
            r.R2C0 = backward.X; r.R2C1 = backward.Y; r.R2C2 = backward.Z; r.R2C3 = 0;
            r.R3C0 = 0;          r.R3C1 = 0;          r.R3C2 = 0;          r.R3C3 = 1;
        }

        [MI(O.AggressiveInlining)] public static void CreateWorld (ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix44 r) {
            Vector3 backward; Vector3.Negate (ref forward, out backward); Vector3.Normalise (ref backward, out backward);
            Vector3 right; Vector3.Cross (ref up, ref backward, out right); Vector3.Normalise (ref right, out right);
            Vector3 finalUp; Vector3.Cross (ref right, ref backward, out finalUp); Vector3.Normalise (ref finalUp, out finalUp);
            r.R0C0 = right.X;    r.R0C1 = right.Y;    r.R0C2 = right.Z;    r.R0C3 = 0;
            r.R1C0 = finalUp.X;  r.R1C1 = finalUp.Y;  r.R1C2 = finalUp.Z;  r.R1C3 = 0;
            r.R2C0 = backward.X; r.R2C1 = backward.Y; r.R2C2 = backward.Z; r.R2C3 = 0;
            r.R3C0 = position.X; r.R3C1 = position.Y; r.R3C2 = position.Z; r.R3C3 = 1;
        }

        // http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToMatrix/
        [MI(O.AggressiveInlining)] public static void CreateFromQuaternion (ref Quaternion q, out Matrix44 r) {
            Boolean qIsUnit; Quaternion.IsUnit (ref q, out qIsUnit); Debug.Assert (qIsUnit);
            Double twoI = q.I + q.I, twoJ = q.J + q.J, twoK = q.K + q.K;
            Double twoUI = q.U * twoI, twoUJ = q.U * twoJ, twoUK = q.U * twoK;
            Double twoII = q.I * twoI, twoIJ = q.I * twoJ, twoIK = q.I * twoK;
            Double twoJJ = q.J * twoJ, twoJK = q.J * twoK, twoKK = q.K * twoK;
            r.R0C0 = 1 - twoJJ - twoKK; r.R1C0 = twoIJ - twoUK;     r.R2C0 = twoIK + twoUJ;     r.R3C0 = 0;
            r.R0C1 = twoIJ + twoUK;     r.R1C1 = 1 - twoII - twoKK; r.R2C1 = twoJK - twoUI;     r.R3C1 = 0;
            r.R0C2 = twoIK - twoUJ;     r.R1C2 = twoJK + twoUI;     r.R2C2 = 1 - twoII - twoJJ; r.R3C2 = 0;
            r.R0C3 = 0;                 r.R1C3 = 0;                 r.R2C3 = 0;                 r.R3C3 = 1;
        }

        // Angle of rotation, in radians. Angles are measured anti-clockwise when viewed from the rotation axis (positive side) toward the origin.
        [MI(O.AggressiveInlining)] public static void CreateFromYawPitchRoll (ref Double yaw, ref Double pitch, ref Double roll, out Matrix44 r) {
            Double cy = Maths.Cos (yaw), sy = Maths.Sin (yaw);
            Double cx = Maths.Cos (pitch), sx = Maths.Sin (pitch);
            Double cz = Maths.Cos (roll), sz = Maths.Sin (roll);
            r.R0C0 =  cz*cy+sz*sx*sy; r.R0C1 =  sz*cx; r.R0C2 = -cz*sy+sz*sx*cy; r.R0C3 = 0;
            r.R1C0 = -sz*cy+cz*sx*sy; r.R1C1 =  cz*cx; r.R1C2 = -cz*sy+sz*sx*cy; r.R1C3 = 0;
            r.R2C0 =  cx*sy;          r.R2C1 = -sx;    r.R2C2 =  cx*cy;          r.R2C3 = 0;
            r.R3C0 = 0;               r.R3C1 = 0;      r.R3C2 = 0;               r.R3C3 = 1;
        }

        // http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
        [MI(O.AggressiveInlining)] public static void CreatePerspectiveFieldOfView (ref Double fieldOfView, ref Double aspectRatio, ref Double nearPlaneDistance, ref Double farPlaneDistance, out Matrix44 r) {
            Debug.Assert (fieldOfView > 0 && fieldOfView < Maths.Pi);
            Debug.Assert (nearPlaneDistance > 0);
            Debug.Assert (farPlaneDistance > 0);
            Debug.Assert (nearPlaneDistance < farPlaneDistance);
            Double yScale = (Double) 1 / (Maths.Tan (fieldOfView * Maths.Half));
            Double xScale = yScale / aspectRatio;
            Double f1 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            Double f2 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            r.R0C0 = xScale; r.R0C1 = 0;      r.R0C2 = 0;  r.R0C3 =  0;
            r.R1C0 = 0;      r.R1C1 = yScale; r.R1C2 = 0;  r.R1C3 =  0;
            r.R2C0 = 0;      r.R2C1 = 0;      r.R2C2 = f1; r.R2C3 = -1;
            r.R3C0 = 0;      r.R3C1 = 0;      r.R3C2 = f2; r.R3C3 =  0;
        }

        // http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
        [MI(O.AggressiveInlining)] public static void CreatePerspective (ref Double width, ref Double height, ref Double nearPlaneDistance, ref Double farPlaneDistance, out Matrix44 r) {
            Debug.Assert (nearPlaneDistance > 0);
            Debug.Assert (farPlaneDistance > 0);
            Debug.Assert (nearPlaneDistance < farPlaneDistance);
            r.R0C0 = (nearPlaneDistance * 2) / width;
            r.R0C1 = r.R0C2 = r.R0C3 = 0;
            r.R1C1 = (nearPlaneDistance * 2) / height;
            r.R1C0 = r.R1C2 = r.R1C3 = 0;
            r.R2C2 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            r.R2C0 = r.R2C1 = 0;
            r.R2C3 = -1;
            r.R3C0 = r.R3C1 = r.R3C3 = 0;
            r.R3C2 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
        }

        // http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx
        [MI(O.AggressiveInlining)] public static void CreatePerspectiveOffCenter (ref Double left, ref Double right, ref Double bottom, ref Double top, ref Double nearPlaneDistance, ref Double farPlaneDistance, out Matrix44 r) {
            Debug.Assert (nearPlaneDistance > 0);
            Debug.Assert (farPlaneDistance > 0);
            Debug.Assert (nearPlaneDistance < farPlaneDistance);
            r.R0C0 = (nearPlaneDistance * 2) / (right - left);
            r.R0C1 = r.R0C2 = r.R0C3 = 0;
            r.R1C1 = (nearPlaneDistance * 2) / (top - bottom);
            r.R1C0 = r.R1C2 = r.R1C3 = 0;
            r.R2C0 = (left + right) / (right - left);
            r.R2C1 = (top + bottom) / (top - bottom);
            r.R2C2 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            r.R2C3 = -1;
            r.R3C2 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            r.R3C0 = r.R3C1 = r.R3C3 = 0;
        }

        // http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
        [MI(O.AggressiveInlining)] public static void CreateOrthographic (ref Double width, ref Double height, ref Double zNearPlane, ref Double zFarPlane, out Matrix44 r) {
            r.R0C0 = 2 / width;
            r.R0C1 = r.R0C2 = r.R0C3 = 0;
            r.R1C1 = 2 / height;
            r.R1C0 = r.R1C2 = r.R1C3 = 0;
            r.R2C2 = 1 / (zNearPlane - zFarPlane);
            r.R2C0 = r.R2C1 = r.R2C3 = 0;
            r.R3C0 = r.R3C1 = 0;
            r.R3C2 = zNearPlane / (zNearPlane - zFarPlane);
            r.R3C3 = 1;
        }

        // http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx
        [MI(O.AggressiveInlining)] public static void CreateOrthographicOffCenter (ref Double left, ref Double right, ref Double bottom, ref Double top, ref Double zNearPlane, ref Double zFarPlane, out Matrix44 r) {
            r.R0C0 = 2 / (right - left);
            r.R0C1 = r.R0C2 = r.R0C3 = 0;
            r.R1C1 = 2 / (top - bottom);
            r.R1C0 = r.R1C2 = r.R1C3 = 0;
            r.R2C2 = 1 / (zNearPlane - zFarPlane);
            r.R2C0 = r.R2C1 = r.R2C3 = 0;
            r.R3C0 = (left + right) / (left - right);
            r.R3C1 = (top + bottom) / (bottom - top);
            r.R3C2 = zNearPlane / (zNearPlane - zFarPlane);
            r.R3C3 = 1;
        }

        // http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx
        [MI(O.AggressiveInlining)] public static void CreateLookAt (ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix44 r) {
            Vector3 forward; Vector3.Subtract (ref cameraPosition, ref cameraTarget, out forward); Vector3.Normalise (ref forward, out forward);
            Vector3 right; Vector3.Cross (ref cameraUpVector, ref forward, out right); Vector3.Normalise (ref right, out right);
            Vector3 up; Vector3.Cross (ref forward, ref right, out up); Vector3.Normalise (ref up, out up);
            Double a; Vector3.Dot (ref right, ref cameraPosition, out a);
            Double b; Vector3.Dot (ref up, ref cameraPosition, out b);
            Double c; Vector3.Dot (ref forward, ref cameraPosition, out c);
            r.R0C0 = right.X;    r.R0C1 = up.X;       r.R0C2 = forward.X;  r.R0C3 = 0;
            r.R1C0 = right.Y;    r.R1C1 = up.Y;       r.R1C2 = forward.Y;  r.R1C3 = 0;
            r.R2C0 = right.Z;    r.R2C1 = up.Z;       r.R2C2 = forward.Z;  r.R2C3 = 0;
            r.R3C0 = -a;         r.R3C1 = -b;         r.R3C2 = -c;         r.R3C3 = 1;
        }

        [MI(O.AggressiveInlining)] public static Matrix44 CreateTranslation            (Double xPosition, Double yPosition, Double zPosition) { Matrix44 r; CreateTranslation (ref xPosition, ref yPosition, ref zPosition, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateTranslation            (Vector3 position) { Matrix44 r; CreateTranslation (ref position, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateScale                  (Double xScale, Double yScale, Double zScale) { Matrix44 r; CreateScale (ref xScale, ref yScale, ref zScale, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateScale                  (Vector3 scales) { Matrix44 r; CreateScale (ref scales, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateScale                  (Double scale) { Matrix44 r; CreateScale (ref scale, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateRotationX              (Double radians) { Matrix44 r; CreateRotationX (ref radians, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateRotationY              (Double radians) { Matrix44 r; CreateRotationY (ref radians, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateRotationZ              (Double radians) { Matrix44 r; CreateRotationZ (ref radians, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateFromAxisAngle          (Vector3 axis, Double angle) { Matrix44 r; CreateFromAxisAngle (ref axis, ref angle, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateFromCartesianAxes      (Vector3 right, Vector3 up, Vector3 backward) { Matrix44 r; CreateFromCartesianAxes (ref right, ref up, ref backward, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateWorld                  (Vector3 position, Vector3 forward, Vector3 up) { Matrix44 r; CreateWorld (ref position, ref forward, ref up, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateFromQuaternion         (Quaternion quaternion) { Matrix44 r; CreateFromQuaternion (ref quaternion, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateFromYawPitchRoll       (Double yaw, Double pitch, Double roll) { Matrix44 r; CreateFromYawPitchRoll (ref yaw, ref pitch, ref roll, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreatePerspectiveFieldOfView (Double fieldOfView,  Double aspectRatio, Double nearPlane, Double farPlane) { Matrix44 r; CreatePerspectiveFieldOfView (ref fieldOfView, ref aspectRatio, ref nearPlane, ref farPlane, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreatePerspective            (Double width, Double height, Double nearPlane, Double farPlane) { Matrix44 r; CreatePerspective (ref width, ref height, ref nearPlane, ref farPlane, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreatePerspectiveOffCenter   (Double left, Double right, Double bottom, Double top, Double nearPlane, Double farPlane) { Matrix44 r; CreatePerspectiveOffCenter (ref left, ref right, ref bottom, ref top, ref nearPlane, ref farPlane, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateOrthographic           (Double width, Double height, Double nearPlane, Double farPlane) { Matrix44 r; CreateOrthographic (ref width, ref height, ref nearPlane, ref farPlane, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateOrthographicOffCenter  (Double left, Double right, Double bottom, Double top, Double nearPlane, Double farPlane) { Matrix44 r; CreateOrthographicOffCenter (ref left, ref right, ref bottom, ref top, ref nearPlane, ref farPlane, out r); return r; }
        [MI(O.AggressiveInlining)] public static Matrix44 CreateLookAt                 (Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector) { Matrix44 r; CreateLookAt (ref cameraPosition, ref cameraTarget, ref cameraUpVector, out r); return r; }

    }

    /// <summary>
    /// Double precision Vector2.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector2 : IEquatable<Vector2> {
        public Double X, Y;

        [MI(O.AggressiveInlining)] public Vector2 (Double x, Double y) { X = x; Y = y; }

        public override String ToString () { return String.Format ("(X:{0}, Y:{1})", X, Y); }

        [MI(O.AggressiveInlining)] public override Int32 GetHashCode () { return X.GetHashCode () ^ Y.GetHashCode ().ShiftAndWrap (2); }

        [MI(O.AggressiveInlining)] public override Boolean Equals (Object obj) { return (obj is Vector2) ? this.Equals ((Vector2) obj) : false; }

        [MI(O.AggressiveInlining)] public Boolean Equals (Vector2 other) { Boolean r; Equals (ref this, ref other, out r); return r; }

        [MI(O.AggressiveInlining)] public Boolean ApproximateEquals (Vector2 other) { Boolean r; ApproximateEquals (ref this, ref other, out r); return r; }

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

        [MI(O.AggressiveInlining)] public static void Equals (ref Vector2 a, ref Vector2 b, out Boolean r) {
            r = (a.X == b.X) && (a.Y == b.Y);
        }

        [MI(O.AggressiveInlining)] public static void ApproximateEquals (ref Vector2 v1, ref Vector2 v2, out Boolean r) {
            r = Maths.ApproximateEquals (v1.X, v2.X) && Maths.ApproximateEquals (v1.Y, v2.Y);
        }

        [MI(O.AggressiveInlining)] public static void Add (ref Vector2 a, ref Vector2 b, out Vector2 r) {
            r.X = a.X + b.X; r.Y = a.Y + b.Y;
        }

        [MI(O.AggressiveInlining)] public static void Subtract (ref Vector2 a, ref Vector2 b, out Vector2 r) {
            r.X = a.X - b.X; r.Y = a.Y - b.Y;
        }

        [MI(O.AggressiveInlining)] public static void Negate (ref Vector2 v, out Vector2 r) {
            r.X = -v.X; r.Y = -v.Y;
        }

        [MI(O.AggressiveInlining)] public static void Multiply (ref Vector2 a, ref Vector2 b, out Vector2 r) {
            r.X = a.X * b.X; r.Y = a.Y * b.Y;
        }

        [MI(O.AggressiveInlining)] public static void Multiply (ref Vector2 v, ref Double f, out Vector2 r) {
            r.X = v.X * f; r.Y = v.Y * f;
        }

        [MI(O.AggressiveInlining)] public static void Divide (ref Vector2 a, ref Vector2 b, out Vector2 r) {
            r.X = a.X / b.X; r.Y = a.Y / b.Y;
        }

        [MI(O.AggressiveInlining)] public static void Divide (ref Vector2 v, ref Double d, out Vector2 r) {
            Double num = 1 / d;
            r.X = v.X * num; r.Y = v.Y * num;
        }

        [MI(O.AggressiveInlining)] public static Boolean operator == (Vector2 a, Vector2 b) { Boolean r; Equals    (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Boolean operator != (Vector2 a, Vector2 b) { Boolean r; Equals    (ref a, ref b, out r); return !r; }
        [MI(O.AggressiveInlining)] public static Vector2 operator  + (Vector2 a, Vector2 b) { Vector2 r; Add       (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector2 operator  - (Vector2 a, Vector2 b) { Vector2 r; Subtract  (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector2 operator  - (Vector2 v)            { Vector2 r; Negate    (ref v,        out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector2 operator  * (Vector2 a, Vector2 b) { Vector2 r; Multiply  (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector2 operator  * (Vector2 v, Double f)  { Vector2 r; Multiply  (ref v, ref f, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector2 operator  * (Double f,  Vector2 v) { Vector2 r; Multiply  (ref v, ref f, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector2 operator  / (Vector2 a, Vector2 b) { Vector2 r; Divide    (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector2 operator  / (Vector2 a, Double d)  { Vector2 r; Divide    (ref a, ref d, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Double  operator  | (Vector2 a, Vector2 d) { Double  r; Dot       (ref a, ref d, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector2 operator  ~ (Vector2 v)            { Vector2 r; Normalise (ref v,        out r); return  r; }

        [MI(O.AggressiveInlining)] public static Boolean Equals            (Vector2 a, Vector2 b) { Boolean r; Equals            (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Boolean ApproximateEquals (Vector2 a, Vector2 b) { Boolean r; ApproximateEquals (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Add               (Vector2 a, Vector2 b) { Vector2 r; Add               (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Subtract          (Vector2 a, Vector2 b) { Vector2 r; Subtract          (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Negate            (Vector2 v)            { Vector2 r; Negate            (ref v,        out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Multiply          (Vector2 a, Vector2 b) { Vector2 r; Multiply          (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Multiply          (Vector2 v, Double f)  { Vector2 r; Multiply          (ref v, ref f, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Divide            (Vector2 a, Vector2 b) { Vector2 r; Divide            (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Divide            (Vector2 a, Double d)  { Vector2 r; Divide            (ref a, ref d, out r); return r; }

        // Utilities //-------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void Min (ref Vector2 a, ref Vector2 b, out Vector2 r) {
            r.X = (a.X < b.X) ? a.X : b.X;
            r.Y = (a.Y < b.Y) ? a.Y : b.Y;
        }

        [MI(O.AggressiveInlining)] public static void Max (ref Vector2 a, ref Vector2 b, out Vector2 r) {
            r.X = (a.X > b.X) ? a.X : b.X;
            r.Y = (a.Y > b.Y) ? a.Y : b.Y;
        }

        [MI(O.AggressiveInlining)] public static void Clamp (ref Vector2 v, ref Vector2 min, ref Vector2 max, out Vector2 r) {
            Double x = v.X; x = (x > max.X) ? max.X : x; x = (x < min.X) ? min.X : x; r.X = x;
            Double y = v.Y; y = (y > max.Y) ? max.Y : y; y = (y < min.Y) ? min.Y : y; r.Y = y;
        }

        [MI(O.AggressiveInlining)] public static void Clamp (ref Vector2 v, ref Double min, ref Double max, out Vector2 r) {
            Double x = v.X; x = (x > max) ? max : x; x = (x < min) ? min : x; r.X = x;
            Double y = v.Y; y = (y > max) ? max : y; y = (y < min) ? min : y; r.Y = y;
        }

        [MI(O.AggressiveInlining)] public static void Lerp (ref Vector2 a, ref Vector2 b, Double amount, out Vector2 r) {
            Debug.Assert (amount >= 0 && amount <= 1);
            r.X = a.X + ((b.X - a.X) * amount);
            r.Y = a.Y + ((b.Y - a.Y) * amount);
        }

        [MI(O.AggressiveInlining)] public static void IsUnit (ref Vector2 vector, out Boolean r) {
            r = Maths.IsApproximatelyZero(1 - vector.X * vector.X - vector.Y * vector.Y);
        }

        [MI(O.AggressiveInlining)] public Boolean IsUnit        () { Boolean r; IsUnit (ref this, out r); return r; }
        [MI(O.AggressiveInlining)] public Vector2 Clamp         (Vector2 min, Vector2 max) { Clamp (ref this, ref min, ref max, out this); return this; }
        [MI(O.AggressiveInlining)] public Vector2 Clamp         (Double min, Double max) { Clamp (ref this, ref min, ref max, out this); return this; }

        [MI(O.AggressiveInlining)] public static Vector2 Min    (Vector2 a, Vector2 b) { Vector2 r; Min (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Max    (Vector2 a, Vector2 b) { Vector2 r; Max (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Clamp  (Vector2 v, Vector2 min, Vector2 max) { Vector2 r; Clamp (ref v, ref min, ref max, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Lerp   (Vector2 a, Vector2 b, Double amount) { Vector2 r; Lerp (ref a, ref b, amount, out r); return r; }
        [MI(O.AggressiveInlining)] public static Boolean IsUnit (Vector2 v) { Boolean r; IsUnit (ref v, out r); return r; }
        
        // Splines //---------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void SmoothStep (ref Vector2 v1, ref Vector2 v2, Double amount, out Vector2 r) {
            Debug.Assert (amount >= 0 && amount <= 1);
            amount = (amount * amount) * (3 - (2 * amount));
            r.X = v1.X + ((v2.X - v1.X) * amount);
            r.Y = v1.Y + ((v2.Y - v1.Y) * amount);
        }

        [MI(O.AggressiveInlining)] public static void CatmullRom (ref Vector2 v1, ref Vector2 v2, ref Vector2 v3, ref Vector2 v4, Double amount, out Vector2 r) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Double squared = amount * amount;
            Double cubed = amount * squared;
            r.X  = 2 * v2.X;
            r.X += (v3.X - v1.X) * amount;
            r.X += ((2 * v1.X) + (4 * v3.X) - (5 * v2.X) - (v4.X)) * squared;
            r.X += ((3 * v2.X) + (v4.X) - (v1.X)  - (3 * v3.X)) * cubed;
            r.X *= Maths.Half;
            r.Y  = 2 * v2.Y;
            r.Y += (v3.Y - v1.Y) * amount;
            r.Y += ((2 * v1.Y) + (4 * v3.Y) - (5 * v2.Y) - (v4.Y)) * squared;
            r.Y += ((3 * v2.Y) + (v4.Y) - (v1.Y) - (3 * v3.Y)) * cubed;
            r.Y *= Maths.Half;
        }

        [MI(O.AggressiveInlining)] public static void Hermite (ref Vector2 v1, ref Vector2 tangent1, ref Vector2 v2, ref Vector2 tangent2, Double amount, out Vector2 r) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector2.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector2.IsUnit (ref tangent2, out tangent2IsUnit);
            Debug.Assert (tangent1IsUnit && tangent2IsUnit);
            Double squared = amount * amount;
            Double cubed = amount * squared;
            Double a = ((cubed * 2) - (squared * 3)) + 1;
            Double b = (-cubed * 2) + (squared * 3);
            Double c = (cubed - (squared * 2)) + amount;
            Double d = cubed - squared;
            r.X = (v1.X * a) + (v2.X * b) + (tangent1.X * c) + (tangent2.X * d);
            r.Y = (v1.Y * a) + (v2.Y * b) + (tangent1.Y * c) + (tangent2.Y * d);
        }

        [MI(O.AggressiveInlining)] public static Vector2 SmoothStep (Vector2 v1, Vector2 v2, Double amount) { Vector2 r; SmoothStep (ref v1, ref v2, amount, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 CatmullRom (Vector2 v1, Vector2 v2, Vector2 v3, Vector2 v4, Double amount) { Vector2 r; CatmullRom (ref v1, ref v2, ref v3, ref v4, amount, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Hermite    (Vector2 v1, Vector2 tangent1, Vector2 v2, Vector2 tangent2, Double amount) { Vector2 r; Hermite (ref v1, ref tangent1, ref v2, ref tangent2, amount, out r); return r; }

        // Maths //-----------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void Distance (ref Vector2 a, ref Vector2 b, out Double r) {
            Double dx = a.X - b.X, dy = a.Y - b.Y;
            Double lengthSquared = (dx * dx) + (dy * dy);
            r = Maths.Sqrt (lengthSquared);
        }

        [MI(O.AggressiveInlining)] public static void DistanceSquared (ref Vector2 a, ref Vector2 b, out Double r) {
            Double dx = a.X - b.X, dy = a.Y - b.Y;
            r = (dx * dx) + (dy * dy);
        }

        [MI(O.AggressiveInlining)] public static void Dot (ref Vector2 a, ref Vector2 b, out Double r) {
            r = (a.X * b.X) + (a.Y * b.Y);
        }

        [MI(O.AggressiveInlining)] public static void Normalise (ref Vector2 vector, out Vector2 r) {
            Double lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y);
            Debug.Assert (lengthSquared > Maths.Epsilon && !Double.IsInfinity(lengthSquared));
            Double multiplier = 1 / Maths.Sqrt (lengthSquared);
            r.X = vector.X * multiplier;
            r.Y = vector.Y * multiplier;
        }

        [MI(O.AggressiveInlining)] public static void Reflect (ref Vector2 vector, ref Vector2 normal, out Vector2 r) {
            Boolean normalIsUnit; Vector2.IsUnit (ref normal, out normalIsUnit);
            Debug.Assert (normalIsUnit);
            Double dot; Dot(ref vector, ref normal, out dot);
            Double twoDot = dot * 2;
            Vector2 m;
            Vector2.Multiply (ref normal, ref twoDot, out m);
            Vector2.Subtract (ref vector, ref m, out r);
        }

        [MI(O.AggressiveInlining)] public static void Length (ref Vector2 vector, out Double r) {
            Double lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y);
            r = Maths.Sqrt (lengthSquared);
        }

        [MI(O.AggressiveInlining)] public static void LengthSquared (ref Vector2 vector, out Double r) {
            r = (vector.X * vector.X) + (vector.Y * vector.Y);
        }

        [MI(O.AggressiveInlining)] public Double  Length        () { Double r; Length (ref this, out r); return r; }
        [MI(O.AggressiveInlining)] public Double  LengthSquared () { Double r; LengthSquared (ref this, out r); return r; }
        [MI(O.AggressiveInlining)] public Vector2 Normalise     () { Normalise (ref this, out this); return this; }

        [MI(O.AggressiveInlining)] public static Double  Distance        (Vector2 a, Vector2 b) { Double r; Distance (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Double  DistanceSquared (Vector2 a, Vector2 b) { Double r; DistanceSquared (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Double  Dot             (Vector2 a, Vector2 b) { Double r; Dot (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Normalise       (Vector2 v) { Vector2 r; Normalise (ref v, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector2 Reflect         (Vector2 v, Vector2 normal) { Vector2 r; Reflect (ref v, ref normal, out r); return r; }
        [MI(O.AggressiveInlining)] public static Double  Length          (Vector2 v) { Double r; Length (ref v, out r); return r; }
        [MI(O.AggressiveInlining)] public static Double  LengthSquared   (Vector2 v) { Double r; LengthSquared (ref v, out r); return r; }
    }

    /// <summary>
    /// Double precision Vector3.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector3 : IEquatable<Vector3> {
        public Double X, Y, Z;

        [MI(O.AggressiveInlining)] public Vector3 (Double x, Double y, Double z) { X = x; Y = y; Z = z; }

        [MI(O.AggressiveInlining)] public Vector3 (Vector2 value, Double z) { X = value.X; Y = value.Y; Z = z; }

        public override String ToString () { return string.Format ("(X:{0}, Y:{1}, Z:{2})", X, Y, Z); }

        [MI(O.AggressiveInlining)] public override Int32 GetHashCode () {
            return X.GetHashCode () ^ Y.GetHashCode ().ShiftAndWrap (2) ^ Z.GetHashCode ().ShiftAndWrap (4);
        }

        [MI(O.AggressiveInlining)] public override Boolean Equals (Object obj) { return (obj is Vector3) ? this.Equals ((Vector3) obj) : false; }

        [MI(O.AggressiveInlining)] public Boolean Equals (Vector3 other) { Boolean r; Equals (ref this, ref other, out r); return r; }

        [MI(O.AggressiveInlining)] public Boolean ApproximateEquals (Vector3 other) { Boolean r; ApproximateEquals (ref this, ref other, out r); return r; }

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

        [MI(O.AggressiveInlining)] public static void Equals (ref Vector3 a, ref Vector3 b, out Boolean r) {
            r = (a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z);
        }

        [MI(O.AggressiveInlining)] public static void ApproximateEquals (ref Vector3 v1, ref Vector3 v2, out Boolean r) {
            r = Maths.ApproximateEquals (v1.X, v2.X) && Maths.ApproximateEquals (v1.Y, v2.Y)
                && Maths.ApproximateEquals (v1.Z, v2.Z);
        }

        [MI(O.AggressiveInlining)] public static void Add (ref Vector3 a, ref Vector3 b, out Vector3 r) {
            r.X = a.X + b.X; r.Y = a.Y + b.Y; r.Z = a.Z + b.Z;
        }

        [MI(O.AggressiveInlining)] public static void Subtract (ref Vector3 a, ref Vector3 b, out Vector3 r) {
            r.X = a.X - b.X; r.Y = a.Y - b.Y; r.Z = a.Z - b.Z;
        }

        [MI(O.AggressiveInlining)] public static void Negate (ref Vector3 value, out Vector3 r) {
            r.X = -value.X; r.Y = -value.Y; r.Z = -value.Z;
        }

        [MI(O.AggressiveInlining)] public static void Multiply (ref Vector3 a, ref Vector3 b, out Vector3 r) {
            r.X = a.X * b.X; r.Y = a.Y * b.Y; r.Z = a.Z * b.Z;
        }

        [MI(O.AggressiveInlining)] public static void Multiply (ref Vector3 a, ref Double f, out Vector3 r) {
            r.X = a.X * f; r.Y = a.Y * f; r.Z = a.Z * f;
        }

        [MI(O.AggressiveInlining)] public static void Divide (ref Vector3 a, ref Vector3 b, out Vector3 r) {
            r.X = a.X / b.X; r.Y = a.Y / b.Y; r.Z = a.Z / b.Z;
        }

        [MI(O.AggressiveInlining)] public static void Divide (ref Vector3 a, ref Double d, out Vector3 r) {
            Double num = 1 / d;
            r.X = a.X * num; r.Y = a.Y * num; r.Z = a.Z * num;
        }

        [MI(O.AggressiveInlining)] public static Boolean operator == (Vector3 a, Vector3 b) { Boolean r; Equals    (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Boolean operator != (Vector3 a, Vector3 b) { Boolean r; Equals    (ref a, ref b, out r); return !r; }
        [MI(O.AggressiveInlining)] public static Vector3 operator  + (Vector3 a, Vector3 b) { Vector3 r; Add       (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3 operator  - (Vector3 a, Vector3 b) { Vector3 r; Subtract  (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3 operator  - (Vector3 v)            { Vector3 r; Negate    (ref v,        out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3 operator  * (Vector3 a, Vector3 b) { Vector3 r; Multiply  (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3 operator  * (Vector3 v, Double f)  { Vector3 r; Multiply  (ref v, ref f, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3 operator  * (Double f,  Vector3 v) { Vector3 r; Multiply  (ref v, ref f, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3 operator  / (Vector3 a, Vector3 b) { Vector3 r; Divide    (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3 operator  / (Vector3 a, Double d)  { Vector3 r; Divide    (ref a, ref d, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3 operator  ^ (Vector3 a, Vector3 d) { Vector3 r; Cross     (ref a, ref d, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Double  operator  | (Vector3 a, Vector3 d) { Double  r; Dot       (ref a, ref d, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector3 operator  ~ (Vector3 v)            { Vector3 r; Normalise (ref v,        out r); return  r; }

        [MI(O.AggressiveInlining)] public static Boolean Equals            (Vector3 a, Vector3 b) { Boolean r; Equals            (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Boolean ApproximateEquals (Vector3 a, Vector3 b) { Boolean r; ApproximateEquals (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3 Add               (Vector3 a, Vector3 b) { Vector3 r; Add               (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3 Subtract          (Vector3 a, Vector3 b) { Vector3 r; Subtract          (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3 Negate            (Vector3 v)            { Vector3 r; Negate            (ref v,        out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3 Multiply          (Vector3 a, Vector3 b) { Vector3 r; Multiply          (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3 Multiply          (Vector3 v, Double f)  { Vector3 r; Multiply          (ref v, ref f, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3 Divide            (Vector3 a, Vector3 b) { Vector3 r; Divide            (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3 Divide            (Vector3 a, Double d)  { Vector3 r; Divide            (ref a, ref d, out r); return r; }

        // Utilities //-------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void Min (ref Vector3 a, ref Vector3 b, out Vector3 r) {
            r.X = (a.X < b.X) ? a.X : b.X; r.Y = (a.Y < b.Y) ? a.Y : b.Y;
            r.Z = (a.Z < b.Z) ? a.Z : b.Z;
        }

        [MI(O.AggressiveInlining)] public static void Max (ref Vector3 a, ref Vector3 b, out Vector3 r) {
            r.X = (a.X > b.X) ? a.X : b.X; r.Y = (a.Y > b.Y) ? a.Y : b.Y;
            r.Z = (a.Z > b.Z) ? a.Z : b.Z;
        }

        [MI(O.AggressiveInlining)] public static void Clamp (ref Vector3 v, ref Vector3 min, ref Vector3 max, out Vector3 r) {
            Double x = v.X; x = (x > max.X) ? max.X : x; x = (x < min.X) ? min.X : x; r.X = x;
            Double y = v.Y; y = (y > max.Y) ? max.Y : y; y = (y < min.Y) ? min.Y : y; r.Y = y;
            Double z = v.Z; z = (z > max.Z) ? max.Z : z; z = (z < min.Z) ? min.Z : z; r.Z = z;
        }

        [MI(O.AggressiveInlining)] public static void Clamp (ref Vector3 v, ref Double min, ref Double max, out Vector3 r) {
            Double x = v.X; x = (x > max) ? max : x; x = (x < min) ? min : x; r.X = x;
            Double y = v.Y; y = (y > max) ? max : y; y = (y < min) ? min : y; r.Y = y;
            Double z = v.Z; z = (z > max) ? max : z; z = (z < min) ? min : z; r.Z = z;
        }

        [MI(O.AggressiveInlining)] public static void Lerp (ref Vector3 a, ref Vector3 b, ref Double amount, out Vector3 r){
            Debug.Assert (amount >= 0 && amount <= 1);
            r.X = a.X + ((b.X - a.X) * amount); r.Y = a.Y + ((b.Y - a.Y) * amount);
            r.Z = a.Z + ((b.Z - a.Z) * amount);
        }

        [MI(O.AggressiveInlining)] public static void IsUnit (ref Vector3 vector, out Boolean r) {
            r = Maths.IsApproximatelyZero (1 - vector.X * vector.X - vector.Y * vector.Y - vector.Z * vector.Z);
        }

        [MI(O.AggressiveInlining)] public Boolean IsUnit        () { Boolean r; IsUnit (ref this, out r); return r; }
        [MI(O.AggressiveInlining)] public Vector3 Clamp         (Vector3 min, Vector3 max) { Clamp (ref this, ref min, ref max, out this); return this; }
        [MI(O.AggressiveInlining)] public Vector3 Clamp         (Double min, Double max) { Clamp (ref this, ref min, ref max, out this); return this; }

        [MI(O.AggressiveInlining)] public static Vector3 Min    (Vector3 a, Vector3 b) { Vector3 r; Min (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3 Max    (Vector3 a, Vector3 b) { Vector3 r; Max (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3 Clamp  (Vector3 v, Vector3 min, Vector3 max) { Vector3 r; Clamp (ref v, ref min, ref max, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3 Lerp   (Vector3 a, Vector3 b, Double amount) { Vector3 r; Lerp (ref a, ref b, ref amount, out r); return r; }
        [MI(O.AggressiveInlining)] public static Boolean IsUnit (Vector3 v) { Boolean r; IsUnit (ref v, out r); return r; }

        // Splines //---------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void SmoothStep (ref Vector3 v1, ref Vector3 v2, ref Double amount, out Vector3 r) {
            Debug.Assert (amount >= 0 && amount <= 1);
            amount = (amount * amount) * (3 - (2 * amount));
            r.X = v1.X + ((v2.X - v1.X) * amount);
            r.Y = v1.Y + ((v2.Y - v1.Y) * amount);
            r.Z = v1.Z + ((v2.Z - v1.Z) * amount);
        }

        [MI(O.AggressiveInlining)] public static void CatmullRom (ref Vector3 v1, ref Vector3 v2, ref Vector3 v3, ref Vector3 v4, ref Double amount, out Vector3 r) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Double squared = amount * amount;
            Double cubed = amount * squared;
            r.X  = 2 * v2.X;
            r.X += (v3.X - v1.X) * amount;
            r.X += ((2 * v1.X) + (4 * v3.X) - (5 * v2.X) - (v4.X)) * squared;
            r.X += ((3 * v2.X) + (v4.X) - (v1.X)  - (3 * v3.X)) * cubed;
            r.X *= Maths.Half;
            r.Y  = 2 * v2.Y;
            r.Y += (v3.Y - v1.Y) * amount;
            r.Y += ((2 * v1.Y) + (4 * v3.Y) - (5 * v2.Y) - (v4.Y)) * squared;
            r.Y += ((3 * v2.Y) + (v4.Y) - (v1.Y) - (3 * v3.Y)) * cubed;
            r.Y *= Maths.Half;
            r.Z  = 2 * v2.Z;
            r.Z += (v3.Z - v1.Z) * amount;
            r.Z += ((2 * v1.Z) + (4 * v3.Z) - (5 * v2.Z) - (v4.Z)) * squared;
            r.Z += ((3 * v2.Z) + (v4.Z) - (v1.Z) - (3 * v3.Z)) * cubed;
            r.Z *= Maths.Half;
        }

        [MI(O.AggressiveInlining)] public static void Hermite (ref Vector3 v1, ref Vector3 tangent1, ref Vector3 v2, ref Vector3 tangent2, ref Double amount, out Vector3 r) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector3.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector3.IsUnit (ref tangent2, out tangent2IsUnit);
            Debug.Assert (tangent1IsUnit && tangent2IsUnit);
            Double squared = amount * amount;
            Double cubed = amount * squared;
            Double a = ((cubed * 2) - (squared * 3)) + 1;
            Double b = (-cubed * 2) + (squared * 3);
            Double c = (cubed - (squared * 2)) + amount;
            Double d = cubed - squared;
            r.X = (v1.X * a) + (v2.X * b) + (tangent1.X * c) + (tangent2.X * d);
            r.Y = (v1.Y * a) + (v2.Y * b) + (tangent1.Y * c) + (tangent2.Y * d);
            r.Z = (v1.Z * a) + (v2.Z * b) + (tangent1.Z * c) + (tangent2.Z * d);
        }

        [MI(O.AggressiveInlining)] public static Vector3 SmoothStep (Vector3 v1, Vector3 v2, Double amount) { Vector3 r; SmoothStep (ref v1, ref v2, ref amount, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3 CatmullRom (Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Double amount) { Vector3 r; CatmullRom (ref v1, ref v2, ref v3, ref v4, ref amount, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector3 Hermite    (Vector3 v1, Vector3 tangent1, Vector3 v2, Vector3 tangent2, Double amount) { Vector3 r; Hermite (ref v1, ref tangent1, ref v2, ref tangent2, ref amount, out r); return r; }

        // Maths //-----------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void Distance (ref Vector3 a, ref Vector3 b, out Double r) {
            Double dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z;
            Double lengthSquared = (dx * dx) + (dy * dy) + (dz * dz);
            r = Maths.Sqrt (lengthSquared);
        }

        [MI(O.AggressiveInlining)] public static void DistanceSquared (ref Vector3 a, ref Vector3 b, out Double r) {
            Double dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z;
            r = (dx * dx) + (dy * dy) + (dz * dz);
        }

        [MI(O.AggressiveInlining)] public static void Dot (ref Vector3 a, ref Vector3 b, out Double r) {
            r = (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        [MI(O.AggressiveInlining)] public static void Normalise (ref Vector3 vector, out Vector3 r) {
            Double lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z);
            Debug.Assert (lengthSquared > Maths.Epsilon && !Double.IsInfinity(lengthSquared));
            Double multiplier = 1 / Maths.Sqrt (lengthSquared);
            r.X = vector.X * multiplier;
            r.Y = vector.Y * multiplier;
            r.Z = vector.Z * multiplier;
        }

        [MI(O.AggressiveInlining)] public static void Cross (ref Vector3 a, ref Vector3 b, out Vector3 r) {
            Double x = (a.Y * b.Z) - (a.Z * b.Y);
            Double y = (a.Z * b.X) - (a.X * b.Z);
            Double z = (a.X * b.Y) - (a.Y * b.X);
            r.X = x; r.Y = y; r.Z = z;
        }

        [MI(O.AggressiveInlining)] public static void Reflect (ref Vector3 vector, ref Vector3 normal, out Vector3 r) {
            Boolean normalIsUnit; Vector3.IsUnit (ref normal, out normalIsUnit);
            Debug.Assert (normalIsUnit);
            Double t = (vector.X * normal.X) + (vector.Y * normal.Y) + (vector.Z * normal.Z);
            Double x = vector.X - ((2 * t) * normal.X);
            Double y = vector.Y - ((2 * t) * normal.Y);
            Double z = vector.Z - ((2 * t) * normal.Z);
            r.X = x; r.Y = y; r.Z = z;
        }

        [MI(O.AggressiveInlining)] public static void Length (ref Vector3 vector, out Double r) {
            Double lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z);
            r = Maths.Sqrt (lengthSquared);
        }

        [MI(O.AggressiveInlining)] public static void LengthSquared (ref Vector3 vector, out Double r) {
            r = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z);
        }

        [MI(O.AggressiveInlining)] public Double  Length        () { Double r; Length (ref this, out r); return r; }
        [MI(O.AggressiveInlining)] public Double  LengthSquared () { Double r; LengthSquared (ref this, out r); return r; }
        [MI(O.AggressiveInlining)] public Vector3 Normalise     () { Normalise (ref this, out this); return this; }

        [MI(O.AggressiveInlining)] public static Double  Distance        (Vector3 a, Vector3 b) { Double r; Distance (ref a, ref b, out r); return r; } 
        [MI(O.AggressiveInlining)] public static Double  DistanceSquared (Vector3 a, Vector3 b) { Double r; DistanceSquared (ref a, ref b, out r); return r; } 
        [MI(O.AggressiveInlining)] public static Double  Dot             (Vector3 a, Vector3 b) { Double r; Dot (ref a, ref b, out r); return r; } 
        [MI(O.AggressiveInlining)] public static Vector3 Cross           (Vector3 a, Vector3 b) { Vector3 r; Cross (ref a, ref b, out r); return r; } 
        [MI(O.AggressiveInlining)] public static Vector3 Normalise       (Vector3 v) { Vector3 r; Normalise (ref v, out r); return r; }
         
        [MI(O.AggressiveInlining)] public static Vector3 Reflect         (Vector3 v, Vector3 normal) { Vector3 r; Reflect (ref v, ref normal, out r); return r; } 
        [MI(O.AggressiveInlining)] public static Double  Length          (Vector3 v) { Double r; Length (ref v, out r); return r; } 
        [MI(O.AggressiveInlining)] public static Double  LengthSquared   (Vector3 v) { Double r; LengthSquared (ref v, out r); return r; }

    }

    /// <summary>
    /// Double precision Vector4.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector4 : IEquatable<Vector4> {
        public Double X, Y, Z, W;

        [MI(O.AggressiveInlining)] public Vector4 (Double x, Double y, Double z, Double w) { X = x; Y = y; Z = z; W = w; }

        [MI(O.AggressiveInlining)] public Vector4 (Vector2 value, Double z, Double w) { X = value.X; Y = value.Y; Z = z; W = w; }

        [MI(O.AggressiveInlining)] public Vector4 (Vector3 value, Double w) { X = value.X; Y = value.Y; Z = value.Z; W = w; }

        public override String ToString () { return string.Format ("(X:{0}, Y:{1}, Z:{2}, W:{3})", X, Y, Z, W); }

        [MI(O.AggressiveInlining)] public override Int32 GetHashCode () {
            return W.GetHashCode ().ShiftAndWrap (6) ^ Z.GetHashCode ().ShiftAndWrap (4)
                 ^ Y.GetHashCode ().ShiftAndWrap (2) ^ X.GetHashCode ();
        }

        [MI(O.AggressiveInlining)] public override Boolean Equals (Object obj) { return (obj is Vector4) ? this.Equals ((Vector4)obj) : false; }

        [MI(O.AggressiveInlining)] public Boolean Equals (Vector4 other) { Boolean r; Equals (ref this, ref other, out r); return r; }

        [MI(O.AggressiveInlining)] public Boolean ApproximateEquals (Vector4 other) { Boolean r; ApproximateEquals (ref this, ref other, out r); return r; }

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

        [MI(O.AggressiveInlining)] public static void Equals (ref Vector4 a, ref Vector4 b, out Boolean r) {
            r = (a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z) && (a.W == b.W);
        }

        [MI(O.AggressiveInlining)] public static void ApproximateEquals (ref Vector4 v1, ref Vector4 v2, out Boolean r) {
            r = Maths.ApproximateEquals (v1.X, v2.X) && Maths.ApproximateEquals (v1.Y, v2.Y)
                && Maths.ApproximateEquals (v1.Z, v2.Z) && Maths.ApproximateEquals (v1.W, v2.W);
        }

        [MI(O.AggressiveInlining)] public static void Add (ref Vector4 a, ref Vector4 b, out Vector4 r) {
            r.X = a.X + b.X; r.Y = a.Y + b.Y; r.Z = a.Z + b.Z; r.W = a.W + b.W;
        }

        [MI(O.AggressiveInlining)] public static void Subtract (ref Vector4 a, ref Vector4 b, out Vector4 r) {
            r.X = a.X - b.X; r.Y = a.Y - b.Y; r.Z = a.Z - b.Z; r.W = a.W - b.W;
        }

        [MI(O.AggressiveInlining)] public static void Negate (ref Vector4 v, out Vector4 r) {
            r.X = -v.X; r.Y = -v.Y; r.Z = -v.Z; r.W = -v.W;
        }

        [MI(O.AggressiveInlining)] public static void Multiply (ref Vector4 a, ref Vector4 b, out Vector4 r) {
            r.X = a.X * b.X; r.Y = a.Y * b.Y; r.Z = a.Z * b.Z; r.W = a.W * b.W;
        }

        [MI(O.AggressiveInlining)] public static void Multiply (ref Vector4 v, ref Double f, out Vector4 r) {
            r.X = v.X * f; r.Y = v.Y * f; r.Z = v.Z * f; r.W = v.W * f;
        }

        [MI(O.AggressiveInlining)] public static void Divide (ref Vector4 a, ref Vector4 b, out Vector4 r) {
            r.X = a.X / b.X; r.Y = a.Y / b.Y; r.Z = a.Z / b.Z; r.W = a.W / b.W;
        }

        [MI(O.AggressiveInlining)] public static void Divide (ref Vector4 v, ref Double d, out Vector4 r) {
            Double num = 1 / d;
            r.X = v.X * num; r.Y = v.Y * num; r.Z = v.Z * num; r.W = v.W * num;
        }

        [MI(O.AggressiveInlining)] public static Boolean operator == (Vector4 a, Vector4 b) { Boolean r; Equals    (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Boolean operator != (Vector4 a, Vector4 b) { Boolean r; Equals    (ref a, ref b, out r); return !r; }
        [MI(O.AggressiveInlining)] public static Vector4 operator  + (Vector4 a, Vector4 b) { Vector4 r; Add       (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector4 operator  - (Vector4 a, Vector4 b) { Vector4 r; Subtract  (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector4 operator  - (Vector4 v)            { Vector4 r; Negate    (ref v,        out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector4 operator  * (Vector4 a, Vector4 b) { Vector4 r; Multiply  (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector4 operator  * (Vector4 v, Double f)  { Vector4 r; Multiply  (ref v, ref f, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector4 operator  * (Double f,  Vector4 v) { Vector4 r; Multiply  (ref v, ref f, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector4 operator  / (Vector4 a, Vector4 b) { Vector4 r; Divide    (ref a, ref b, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector4 operator  / (Vector4 a, Double d)  { Vector4 r; Divide    (ref a, ref d, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Double  operator  | (Vector4 a, Vector4 d) { Double  r; Dot       (ref a, ref d, out r); return  r; }
        [MI(O.AggressiveInlining)] public static Vector4 operator  ~ (Vector4 v)            { Vector4 r; Normalise (ref v,        out r); return  r; }

        [MI(O.AggressiveInlining)] public static Boolean Equals            (Vector4 a, Vector4 b) { Boolean r; Equals            (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Boolean ApproximateEquals (Vector4 a, Vector4 b) { Boolean r; ApproximateEquals (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4 Add               (Vector4 a, Vector4 b) { Vector4 r; Add               (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4 Subtract          (Vector4 a, Vector4 b) { Vector4 r; Subtract          (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4 Negate            (Vector4 v)            { Vector4 r; Negate            (ref v,        out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4 Multiply          (Vector4 a, Vector4 b) { Vector4 r; Multiply          (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4 Multiply          (Vector4 v, Double f)  { Vector4 r; Multiply          (ref v, ref f, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4 Divide            (Vector4 a, Vector4 b) { Vector4 r; Divide            (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4 Divide            (Vector4 a, Double d)  { Vector4 r; Divide            (ref a, ref d, out r); return r; }

        // Utilities //-------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void Min (ref Vector4 a, ref Vector4 b, out Vector4 r) {
            r.X = (a.X < b.X) ? a.X : b.X; r.Y = (a.Y < b.Y) ? a.Y : b.Y;
            r.Z = (a.Z < b.Z) ? a.Z : b.Z; r.W = (a.W < b.W) ? a.W : b.W;
        }

        [MI(O.AggressiveInlining)] public static void Max (ref Vector4 a, ref Vector4 b, out Vector4 r) {
            r.X = (a.X > b.X) ? a.X : b.X; r.Y = (a.Y > b.Y) ? a.Y : b.Y;
            r.Z = (a.Z > b.Z) ? a.Z : b.Z; r.W = (a.W > b.W) ? a.W : b.W;
        }

        [MI(O.AggressiveInlining)] public static void Clamp (ref Vector4 v, ref Vector4 min, ref Vector4 max, out Vector4 r) {
            Double x = v.X; x = (x > max.X) ? max.X : x; x = (x < min.X) ? min.X : x; r.X = x;
            Double y = v.Y; y = (y > max.Y) ? max.Y : y; y = (y < min.Y) ? min.Y : y; r.Y = y;
            Double z = v.Z; z = (z > max.Z) ? max.Z : z; z = (z < min.Z) ? min.Z : z; r.Z = z;
            Double w = v.W; w = (w > max.W) ? max.W : w; w = (w < min.W) ? min.W : w; r.W = w;
        }

        [MI(O.AggressiveInlining)] public static void Clamp (ref Vector4 v, ref Double min, ref Double max, out Vector4 r) {
            Double x = v.X; x = (x > max) ? max : x; x = (x < min) ? min : x; r.X = x;
            Double y = v.Y; y = (y > max) ? max : y; y = (y < min) ? min : y; r.Y = y;
            Double z = v.Z; z = (z > max) ? max : z; z = (z < min) ? min : z; r.Z = z;
            Double w = v.W; w = (w > max) ? max : w; w = (w < min) ? min : w; r.W = w;
        }

        [MI(O.AggressiveInlining)] public static void Lerp (ref Vector4 a, ref Vector4 b, ref Double amount, out Vector4 r){
            Debug.Assert (amount >= 0 && amount <= 1);
            r.X = a.X + ((b.X - a.X) * amount); r.Y = a.Y + ((b.Y - a.Y) * amount);
            r.Z = a.Z + ((b.Z - a.Z) * amount); r.W = a.W + ((b.W - a.W) * amount);
        }

        [MI(O.AggressiveInlining)] public static void IsUnit (ref Vector4 vector, out Boolean r) {
            r = Maths.IsApproximatelyZero (1 - vector.X * vector.X - vector.Y * vector.Y - vector.Z * vector.Z - vector.W * vector.W);
        }

        [MI(O.AggressiveInlining)] public Boolean IsUnit        () { Boolean r; IsUnit (ref this, out r); return r; }
        [MI(O.AggressiveInlining)] public Vector4 Clamp         (Vector4 min, Vector4 max) { Clamp (ref this, ref min, ref max, out this); return this; }
        [MI(O.AggressiveInlining)] public Vector4 Clamp         (Double min, Double max) { Clamp (ref this, ref min, ref max, out this); return this; }

        [MI(O.AggressiveInlining)] public static Vector4 Min    (Vector4 a, Vector4 b) { Vector4 r; Min (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4 Max    (Vector4 a, Vector4 b) { Vector4 r; Max (ref a, ref b, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4 Clamp  (Vector4 v, Vector4 min, Vector4 max) { Vector4 r; Clamp (ref v, ref min, ref max, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4 Lerp   (Vector4 a, Vector4 b, Double amount) { Vector4 r; Lerp (ref a, ref b, ref amount, out r); return r; }
        [MI(O.AggressiveInlining)] public static Boolean IsUnit (Vector4 v) { Boolean r; IsUnit (ref v, out r); return r; }

        // Splines //---------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void SmoothStep (ref Vector4 v1, ref Vector4 v2, ref Double amount, out Vector4 r) {
            Debug.Assert (amount >= 0 && amount <= 1);
            amount = (amount * amount) * (3 - (2 * amount));
            r.X = v1.X + ((v2.X - v1.X) * amount);
            r.Y = v1.Y + ((v2.Y - v1.Y) * amount);
            r.Z = v1.Z + ((v2.Z - v1.Z) * amount);
            r.W = v1.W + ((v2.W - v1.W) * amount);
        }

        [MI(O.AggressiveInlining)] public static void CatmullRom (ref Vector4 v1, ref Vector4 v2, ref Vector4 v3, ref Vector4 v4, ref Double amount, out Vector4 r) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Double squared = amount * amount;
            Double cubed = amount * squared;
            r.X  = 2 * v2.X;
            r.X += (v3.X - v1.X) * amount;
            r.X += ((2 * v1.X) + (4 * v3.X) - (5 * v2.X) - (v4.X)) * squared;
            r.X += ((3 * v2.X) + (v4.X) - (v1.X)  - (3 * v3.X)) * cubed;
            r.X *= Maths.Half;
            r.Y  = 2 * v2.Y;
            r.Y += (v3.Y - v1.Y) * amount;
            r.Y += ((2 * v1.Y) + (4 * v3.Y) - (5 * v2.Y) - (v4.Y)) * squared;
            r.Y += ((3 * v2.Y) + (v4.Y) - (v1.Y) - (3 * v3.Y)) * cubed;
            r.Y *= Maths.Half;
            r.Z  = 2 * v2.Z;
            r.Z += (v3.Z - v1.Z) * amount;
            r.Z += ((2 * v1.Z) + (4 * v3.Z) - (5 * v2.Z) - (v4.Z)) * squared;
            r.Z += ((3 * v2.Z) + (v4.Z) - (v1.Z) - (3 * v3.Z)) * cubed;
            r.Z *= Maths.Half;
            r.W  = 2 * v2.W;
            r.W += (v3.W - v1.W) * amount;
            r.W += ((2 * v1.W) + (4 * v3.W) - (5 * v2.W) - (v4.W)) * squared;
            r.W += ((3 * v2.W) + (v4.W) - (v1.W) - (3 * v3.W)) * cubed;
            r.W *= Maths.Half;
        }

        [MI(O.AggressiveInlining)] public static void Hermite (ref Vector4 v1, ref Vector4 tangent1, ref Vector4 v2, ref Vector4 tangent2, ref Double amount, out Vector4 r) {
            Debug.Assert (amount >= 0 && amount <= 1);
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector4.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector4.IsUnit (ref tangent2, out tangent2IsUnit);
            Debug.Assert (tangent1IsUnit && tangent2IsUnit);
            Double squared = amount * amount;
            Double cubed = amount * squared;
            Double a = ((cubed * 2) - (squared * 3)) + 1;
            Double b = (-cubed * 2) + (squared * 3);
            Double c = (cubed - (squared * 2)) + amount;
            Double d = cubed - squared;
            r.X = (v1.X * a) + (v2.X * b) + (tangent1.X * c) + (tangent2.X * d);
            r.Y = (v1.Y * a) + (v2.Y * b) + (tangent1.Y * c) + (tangent2.Y * d);
            r.Z = (v1.Z * a) + (v2.Z * b) + (tangent1.Z * c) + (tangent2.Z * d);
            r.W = (v1.W * a) + (v2.W * b) + (tangent1.W * c) + (tangent2.W * d);
        }

        [MI(O.AggressiveInlining)] public static Vector4 SmoothStep (Vector4 v1, Vector4 v2, Double amount) { Vector4 r; SmoothStep (ref v1, ref v2, ref amount, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4 CatmullRom (Vector4 v1, Vector4 v2, Vector4 v3, Vector4 v4, Double amount) { Vector4 r; CatmullRom (ref v1, ref v2, ref v3, ref v4, ref amount, out r); return r; }
        [MI(O.AggressiveInlining)] public static Vector4 Hermite    (Vector4 v1, Vector4 tangent1, Vector4 v2, Vector4 tangent2, Double amount) { Vector4 r; Hermite (ref v1, ref tangent1, ref v2, ref tangent2, ref amount, out r); return r; }

        // Maths //-----------------------------------------------------------//

        [MI(O.AggressiveInlining)] public static void Distance (ref Vector4 a, ref Vector4 b, out Double r) {
            Double dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z, dw = a.W - b.W;
            Double lengthSquared = (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
            r = Maths.Sqrt (lengthSquared);
        }

        [MI(O.AggressiveInlining)] public static void DistanceSquared (ref Vector4 a, ref Vector4 b, out Double r) {
            Double dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z, dw = a.W - b.W;
            r = (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
        }

        [MI(O.AggressiveInlining)] public static void Dot (ref Vector4 a, ref Vector4 b, out Double r) {
            r = (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z) + (a.W * b.W);
        }

        [MI(O.AggressiveInlining)] public static void Normalise (ref Vector4 vector, out Vector4 r) {
            Double lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z) + (vector.W * vector.W);
            Debug.Assert (lengthSquared > Maths.Epsilon && !Double.IsInfinity(lengthSquared));
            Double multiplier = 1 / (Maths.Sqrt (lengthSquared));
            r.X = vector.X * multiplier; r.Y = vector.Y * multiplier;
            r.Z = vector.Z * multiplier; r.W = vector.W * multiplier;
        }

        [MI(O.AggressiveInlining)] public static void Length (ref Vector4 vector, out Double r) {
            Double lengthSquared = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z) + (vector.W * vector.W);
            r = Maths.Sqrt (lengthSquared);
        }

        [MI(O.AggressiveInlining)] public static void LengthSquared (ref Vector4 vector, out Double r) {
            r = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z) + (vector.W * vector.W);
        }

        [MI(O.AggressiveInlining)] public Double  Length        () { Double r; Length (ref this, out r); return r; }
        [MI(O.AggressiveInlining)] public Double  LengthSquared () { Double r; LengthSquared (ref this, out r); return r; }
        [MI(O.AggressiveInlining)] public Vector4 Normalise     () { Normalise (ref this, out this); return this; }

        [MI(O.AggressiveInlining)] public static Double  Distance        ( Vector4 a, Vector4 b) { Double r; Distance (ref a, ref b, out r); return r; } 
        [MI(O.AggressiveInlining)] public static Double  DistanceSquared (Vector4 a, Vector4 b) { Double r; DistanceSquared (ref a, ref b, out r); return r; } 
        [MI(O.AggressiveInlining)] public static Double  Dot             (Vector4 a, Vector4 b) { Double r; Dot (ref a, ref b, out r); return r; } 
        [MI(O.AggressiveInlining)] public static Vector4 Normalise       (Vector4 v) { Vector4 r; Normalise (ref v, out r); return r; }
        [MI(O.AggressiveInlining)] public static Double  Length          (Vector4 v) { Double r; Length (ref v, out r); return r; } 
        [MI(O.AggressiveInlining)] public static Double  LengthSquared   (Vector4 v) { Double r; LengthSquared (ref v, out r); return r; }
    }

    /// <summary>
    /// Provides maths functions with consistent function signatures across supported precisions.
    /// </summary>
    public static class Maths {
        public static readonly Double Epsilon = (Double) 0.000001;
        public static readonly Double E = 2.71828182845904523536028747135;
        public static readonly Double Half = 0.5;
        public static readonly Double Quarter = 0.25;
        public static readonly Double Log10E = 0.43429448190325182765112891892;
        public static readonly Double Log2E = 1.44269504088896340735992468100;
        public static readonly Double Pi = 3.14159265358979323846264338328;
        public static readonly Double HalfPi = 1.57079632679489661923132169164;
        public static readonly Double QuarterPi = 0.78539816339744830961566084582;
        public static readonly Double Root2 = 1.41421356237309504880168872421;
        public static readonly Double Root3 = 1.73205080756887729352744634151;
        public static readonly Double Tau = 6.28318530717958647692528676656;
        public static readonly Double Deg2Rad = 0.01745329251994329576923690768;
        public static readonly Double Rad2Deg = 57.29577951308232087679815481409;
        public static readonly Double Zero = 0.0;
        public static readonly Double One = 1.0;

        [MI(O.AggressiveInlining)] public static Double Sqrt (Double v) { return Math.Sqrt (v); }
        [MI(O.AggressiveInlining)] public static Double Abs (Double v) { return Math.Abs (v); }

        [MI(O.AggressiveInlining)] public static Double Sin (Double v) { return Math.Sin (v); }
        [MI(O.AggressiveInlining)] public static Double Cos (Double v) { return Math.Cos (v); }
        [MI(O.AggressiveInlining)] public static Double Tan (Double v) { return Math.Tan (v); }
        [MI(O.AggressiveInlining)] public static Double ArcCos (Double v) { return Math.Acos (v); }
        [MI(O.AggressiveInlining)] public static Double ArcSin (Double v) { return Math.Asin (v); }
        [MI(O.AggressiveInlining)] public static Double ArcTan (Double v) { return Math.Atan (v); }
        [MI(O.AggressiveInlining)] public static Double ArcTan2 (Double y, Double x) { return Math.Atan2 (y, x); }

        
        [MI(O.AggressiveInlining)] public static Double ToRadians            (Double input) { return input * Deg2Rad; }
        [MI(O.AggressiveInlining)] public static Double ToDegrees            (Double input) { return input * Rad2Deg; }
        [MI(O.AggressiveInlining)] public static Double FromFraction         (Int32 numerator, Int32 denominator) { return (Double) numerator / (Double) denominator; }
        [MI(O.AggressiveInlining)] public static Double FromFraction         (Int64 numerator, Int64 denominator) { return (Double) numerator / (Double) denominator; }

        [MI(O.AggressiveInlining)] public static Double Min                  (Double a, Double b) { return a < b ? a : b; }
        [MI(O.AggressiveInlining)] public static Double Max                  (Double a, Double b) { return a > b ? a : b; }
        [MI(O.AggressiveInlining)] public static Double Clamp                (Double value, Double min, Double max) { if (value < min) return min; else if (value > max) return max; else return value; }
        [MI(O.AggressiveInlining)] public static Double Lerp                 (Double a, Double b, Double t) { return a + ((b - a) * t); }

        [MI(O.AggressiveInlining)] public static Double FromString           (String str) { Double result = Zero; Double.TryParse (str, out result); return result; }
        [MI(O.AggressiveInlining)] public static void   FromString           (String str, out Double value) { Double.TryParse (str, out value); }

        [MI(O.AggressiveInlining)] public static Boolean IsApproximatelyZero (Double value) { return Abs(value) < Epsilon; }
        [MI(O.AggressiveInlining)] public static Boolean ApproximateEquals   (Double a, Double b) { Double num = a - b; return ((-Epsilon <= num) && (num <= Epsilon)); }
        
        [MI(O.AggressiveInlining)] public static Int32  Sign                 (Double value) { if (value > 0) return 1; else if (value < 0) return -1; return 0; }
        [MI(O.AggressiveInlining)] public static Double CopySign             (Double x, Double y) { if ((x >= 0 && y >= 0) || (x <= 0 && y <= 0)) return x; else return -x; }
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
