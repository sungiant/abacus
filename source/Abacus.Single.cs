// ┌────────────────────────────────────────────────────────────────────────┐ \\
// │    _____ ___.                                                          │ \\
// │   /  _  \\_ |__ _____    ____  __ __  ______                           │ \\
// │  /  /_\  \| __ \\__  \ _/ ___\|  |  \/  ___/                           │ \\
// │ /    |    \ \_\ \/ __ \\  \___|  |  /\___ \                            │ \\
// │ \____|__  /___  (____  /\___  >____//____  >                           │ \\
// │         \/    \/     \/     \/           \/                            │ \\
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
// │ Copyright © 2012 - 2015 ~ Blimey3D (http://www.blimey.io)              │ \\
// ├────────────────────────────────────────────────────────────────────────┤ \\
// │ Authors:                                                               │ \\
// │ ~ Ash Pook (http://www.ajpook.com)                                     │ \\
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

#define VARIANTS_ENABLED

using System;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

namespace Abacus.SinglePrecision
{
    /// <summary>
    /// Single precision Quaternion.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Quaternion
        : IEquatable<Quaternion>
    {
        /// <summary>
        /// Gets or sets the imaginary I-component of the Quaternion.
        /// </summary>
        public Single I;

        /// <summary>
        /// Gets or sets the imaginary J-component of the Quaternion.
        /// </summary>
        public Single J;

        /// <summary>
        /// Gets or sets the imaginary K-component of the Quaternion.
        /// </summary>
        public Single K;

        /// <summary>
        /// Gets or sets the real U-component of the Quaternion.
        /// </summary>
        public Single U;

        /// <summary>
        /// Initilises a new instance of Quaternion from three imaginary
        /// Single values and one real Single value representing
        /// I, J, K and U respectively.
        /// </summary>
        public Quaternion (
            Single i, Single j, Single k, Single u)
        {
            this.I = i;
            this.J = j;
            this.K = k;
            this.U = u;
        }

        /// <summary>
        /// Initilises a new instance of Quaternion from a Vector3 representing
        /// the imaginary parts of the quaternion (I, J & K) and one
        /// Single value representing the real part of the
        /// Quaternion (U).
        /// </summary>
        public Quaternion (Vector3 vectorPart, Single scalarPart)
        {
            this.I = vectorPart.X;
            this.J = vectorPart.Y;
            this.K = vectorPart.Z;
            this.U = scalarPart;
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return String.Format ("{{I:{0} J:{1} K:{2} U:{3}}}",
                I.ToString (), J.ToString (), K.ToString (), U.ToString ());
        }

        /// <summary>
        /// Gets the hash code of the Quaternion object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return U.GetHashCode ().ShiftAndWrap (6)
                 ^ K.GetHashCode ().ShiftAndWrap (4)
                 ^ J.GetHashCode ().ShiftAndWrap (2)
                 ^ I.GetHashCode ();
        }

        /// <summary>
        /// Determines whether or not this Quaternion object is equal to another
        /// object
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            return (obj is Quaternion)
                ? this.Equals ((Quaternion) obj)
                : false;
        }

        #region IEquatable<Quaternion>

        /// <summary>
        /// Determines whether or not this Quaternion object is equal to another
        /// Quaternion object.
        /// </summary>
        public Boolean Equals (Quaternion other)
        {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        #endregion

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines the identity quaternion.
        /// </summary>
        static Quaternion identity;

        /// <summary>
        /// Defines the zero quaternion.
        /// </summary>
        static Quaternion zero;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Quaternion ()
        {
            identity = new Quaternion (0, 0, 0, 1);
            zero = new Quaternion (0, 0, 0, 0);
        }

        /// <summary>
        /// Returns the identity Quaternion.
        /// </summary>
        public static Quaternion Identity
        {
            get { return identity; }
        }

        /// <summary>
        /// Returns the zero Quaternion.
        /// </summary>
        public static Quaternion Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Creates a Quaternion from a vector and an angle to rotate about
        /// the vector.
        /// </summary>
        public static void CreateFromAxisAngle (
            ref Vector3 axis, ref Single angle, out Quaternion result)
        {
            Single half; Maths.Half (out half);
            Single theta = angle * half;

            Single sin = Maths.Sin (theta);
            Single cos = Maths.Cos (theta);

            result.I = axis.X * sin;
            result.J = axis.Y * sin;
            result.K = axis.Z * sin;

            result.U = cos;
        }

        /// <summary>
        /// Creates a new Quaternion from specified yaw, pitch, and roll angles.
        /// </summary>
        public static void CreateFromYawPitchRoll (
            ref Single yaw, ref Single pitch,
            ref Single roll, out Quaternion result)
        {
            Single half; Maths.Half(out half);

            Single hr = roll * half;
            Single hp = pitch * half;
            Single hy = yaw * half;

            Single shr = Maths.Sin (hr);
            Single chr = Maths.Cos (hr);
            Single shp = Maths.Sin (hp);
            Single chp = Maths.Cos (hp);
            Single shy = Maths.Sin (hy);
            Single chy = Maths.Cos (hy);

            result.I = (chy * shp * chr) + (shy * chp * shr);
            result.J = (shy * chp * chr) - (chy * shp * shr);
            result.K = (chy * chp * shr) - (shy * shp * chr);
            result.U = (chy * chp * chr) + (shy * shp * shr);
        }

        /// <summary>
        /// Creates a Quaternion from a rotation Matrix44.
        /// </summary>
        public static void CreateFromRotationMatrix (
            ref Matrix44 m, out Quaternion result)
        {
            // http://www.euclideanspace.com/maths/geometry/rotations/conversions/mToQuaternion/
            Single zero = 0;
            Single one = 1;
            Single two = 2;
            Single quarter; Maths.Quarter (out quarter);

            Single tr = (m.R0C0 + m.R1C1) + m.R2C2;

            if (tr > zero)
            {
                Single s = Maths.Sqrt (tr + one) * two;
                result.U = quarter * s;
                result.I = (m.R1C2 - m.R2C1) / s;
                result.J = (m.R2C0 - m.R0C2) / s;
                result.K = (m.R0C1 - m.R1C0) / s;
            }
            else if ((m.R0C0 >= m.R1C1) && (m.R0C0 >= m.R2C2))
            {
                Single s = Maths.Sqrt (one + m.R0C0 - m.R1C1 - m.R2C2) * two;
                result.U = (m.R1C2 - m.R2C1) / s;
                result.I = quarter * s;
                result.J = (m.R0C1 + m.R1C0) / s;
                result.K = (m.R0C2 + m.R2C0) / s;
            }
            else if (m.R1C1 > m.R2C2)
            {
                Single s = Maths.Sqrt (one + m.R1C1 - m.R0C0 - m.R2C2) * two;
                result.U = (m.R2C0 - m.R0C2) / s;
                result.I = (m.R1C0 + m.R0C1) / s;
                result.J = quarter * s;
                result.K = (m.R2C1 + m.R1C2) / s;
            }
            else
            {
                Single s = Maths.Sqrt (one + m.R2C2 - m.R0C0 - m.R1C1) * two;
                result.U = (m.R0C1 - m.R1C0) / s;
                result.I = (m.R2C0 + m.R0C2) / s;
                result.J = (m.R2C1 + m.R1C2) / s;
                result.K = quarter * s;
            }
        }
        /// <summary>
        /// Calculates the length² of a Quaternion.
        /// </summary>
        public static void LengthSquared (
            ref Quaternion quaternion, out Single result)
        {
            result =
                (quaternion.I * quaternion.I) +
                (quaternion.J * quaternion.J) +
                (quaternion.K * quaternion.K) +
                (quaternion.U * quaternion.U);
        }

        /// <summary>
        /// Calculates the length of a Quaternion.
        /// </summary>
        public static void Length (
            ref Quaternion quaternion, out Single result)
        {
            Single lengthSquared =
                (quaternion.I * quaternion.I) +
                (quaternion.J * quaternion.J) +
                (quaternion.K * quaternion.K) +
                (quaternion.U * quaternion.U);

            result = Maths.Sqrt (lengthSquared);
        }


        /// <summary>
        /// Calculates the conjugate of a Quaternion.
        /// </summary>
        public static void Conjugate (
            ref Quaternion value, out Quaternion result)
        {
            result.I = -value.I;
            result.J = -value.J;
            result.K = -value.K;
            result.U = value.U;
        }

        /// <summary>
        /// Calculates the inverse of two Quaternions.
        /// </summary>
        public static void Inverse (
            ref Quaternion quaternion, out Quaternion result)
        {
            Single one = 1;
            Single a =
                (quaternion.I * quaternion.I) +
                (quaternion.J * quaternion.J) +
                (quaternion.K * quaternion.K) +
                (quaternion.U * quaternion.U);

            Single b = one / a;

            result.I = -quaternion.I * b;
            result.J = -quaternion.J * b;
            result.K = -quaternion.K * b;
            result.U =  quaternion.U * b;
        }

        /// <summary>
        /// Calculates the dot product of two Quaternions.
        /// </summary>
        public static void Dot (
            ref Quaternion q1, ref Quaternion q2, out Single result)
        {
            result =
                (q1.I * q2.I) + (q1.J * q2.J) +
                (q1.K * q2.K) + (q1.U * q2.U);
        }

        /// <summary>
        /// Concatenates two Quaternions; the result represents the first
        /// rotation followed by the second rotation.
        /// </summary>
        public static void Concatenate (
            ref Quaternion q1, ref Quaternion q2, out Quaternion result)
        {
            Single a = (q2.J * q1.K) - (q2.K * q1.J);
            Single b = (q2.K * q1.I) - (q2.I * q1.K);
            Single c = (q2.I * q1.J) - (q2.J * q1.I);
            Single d = (q2.I * q1.I) - (q2.J * q1.J);

            result.I = (q2.I * q1.U) + (q1.I * q2.U) + a;
            result.J = (q2.J * q1.U) + (q1.J * q2.U) + b;
            result.K = (q2.K * q1.U) + (q1.K * q2.U) + c;
            result.U = (q2.U * q1.U) - (q2.K * q1.K) - d;
        }

        /// <summary>
        /// Divides each component of the quaternion by the length of the
        /// quaternion.
        /// </summary>
        public static void Normalise (
            ref Quaternion quaternion, out Quaternion result)
        {
            Single one = 1;

            Single a =
                (quaternion.I * quaternion.I) +
                (quaternion.J * quaternion.J) +
                (quaternion.K * quaternion.K) +
                (quaternion.U * quaternion.U);

            Single b = one / Maths.Sqrt (a);

            result.I = quaternion.I * b;
            result.J = quaternion.J * b;
            result.K = quaternion.K * b;
            result.U = quaternion.U * b;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not two Quaternion objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static void Equals (
            ref Quaternion q1, ref Quaternion q2, out Boolean result)
        {
            result =
                (q1.I == q2.I) && (q1.J == q2.J) &&
                (q1.K == q2.K) && (q1.U == q2.U);
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Quaternion objects.
        /// </summary>
        public static void Add (
            ref Quaternion q1, ref Quaternion q2, out Quaternion result)
        {
            result.I = q1.I + q2.I;
            result.J = q1.J + q2.J;
            result.K = q1.K + q2.K;
            result.U = q1.U + q2.U;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Quaternion objects.
        /// </summary>
        public static void Subtract (
            ref Quaternion q1, ref Quaternion q2, out Quaternion result)
        {
            result.I = q1.I - q2.I;
            result.J = q1.J - q2.J;
            result.K = q1.K - q2.K;
            result.U = q1.U - q2.U;
        }

        // Negation Operators //----------------------------------------------//

        /// <summary>
        /// Performs negation of a Quaternion object.
        /// </summary>
        public static void Negate (
            ref Quaternion quaternion, out Quaternion result)
        {
            result.I = -quaternion.I;
            result.J = -quaternion.J;
            result.K = -quaternion.K;
            result.U = -quaternion.U;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Quaternion objects,
        /// (Quaternion multiplication is not commutative),
        /// (i^2 = j^2 = k^2 = i j k = -1).
        ///
        /// For Quaternion division the notation q1 / q2 is not ideal, since
        /// Quaternion multiplication is not commutative we need to be able
        /// to distinguish between q1*(q2^-1) and (q2^-1)*q1. This is why
        /// Abacus does not have a division opperator.  If you need
        /// a divide operation just multiply by the inverse.
        /// </summary>
        public static void Multiply (
            ref Quaternion q1, ref Quaternion q2, out Quaternion result)
        {
            // http://www.euclideanspace.com/maths/algebra/realNormedAlgebra/quaternions/arithmetic/index.htm

            result.I = q1.I * q2.U + q1.U * q2.I + q1.J * q2.K - q1.K * q2.J;
            result.J = q1.U * q2.J - q1.I * q2.K + q1.J * q2.U + q1.K * q2.I;
            result.K = q1.U * q2.K + q1.I * q2.J - q1.J * q2.I + q1.K * q2.U;
            result.U = q1.U * q2.U - q1.I * q2.I - q1.J * q2.J - q1.K * q2.K;
        }

        /// <summary>
        /// Perform a spherical linear interpolation between two Quaternions.
        /// Provides a constant-speed motion along a unit-radius great circle
        /// arc, given the ends and an interpolation parameter between 0 and 1.
        /// http://en.wikipedia.org/wiki/Slerp
        /// </summary>
        public static void Slerp (
            ref Quaternion quaternion1,
            ref Quaternion quaternion2,
            ref Single amount,
            out Quaternion result)
        {
            Single zero = 0;
            Single one = 1;
            Single epsilon; Maths.Epsilon (out epsilon);

            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single remaining = one - amount;

            Single angle;
            Dot (ref quaternion1, ref quaternion2, out angle);

            if (angle < zero)
            {
                Negate (ref quaternion1, out quaternion1);
                angle = -angle;
            }

            Single theta = Maths.ArcCos (angle);


            Single r = remaining;
            Single a = amount;

            // To avoid division by 0 and by very small numbers the
            // Lerp is used when theta is small.
            if (theta > epsilon)
            {
                Single x = Maths.Sin (remaining * theta);
                Single y = Maths.Sin (amount * theta);
                Single z = Maths.Sin (theta);

                r = x / z;
                a = y / z;
            }

            result.U = (r * quaternion1.U) + (a * quaternion2.U);
            result.I = (r * quaternion1.I) + (a * quaternion2.I);
            result.J = (r * quaternion1.J) + (a * quaternion2.J);
            result.K = (r * quaternion1.K) + (a * quaternion2.K);
        }

        /// <summary>
        /// Perform a linear interpolation between two Quaternions.
        /// </summary>
        public static void Lerp (
            ref Quaternion quaternion1,
            ref Quaternion quaternion2,
            ref Single amount,
            out Quaternion result)
        {
            Single zero = 0;
            Single one = 1;

            if (amount < zero || amount > one)
            {
                throw new ArgumentOutOfRangeException();
            }

            Single remaining = one - amount;

            Single r = remaining;
            Single a = amount;

            result.U = (r * quaternion1.U) + (a * quaternion2.U);
            result.I = (r * quaternion1.I) + (a * quaternion2.I);
            result.J = (r * quaternion1.J) + (a * quaternion2.J);
            result.K = (r * quaternion1.K) + (a * quaternion2.K);
        }

        /// <summary>
        /// Detemines whether or not the Vector2 is of unit length.
        /// </summary>
        public static void IsUnit (
            ref Quaternion quaternion,
            out Boolean result)
        {
            Single one = 1;

            result = Maths.IsZero(
                one -
                quaternion.U * quaternion.U -
                quaternion.I * quaternion.I -
                quaternion.J * quaternion.J -
                quaternion.K * quaternion.K);
        }

#if (VARIANTS_ENABLED)

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion CreateFromAxisAngle (
            Vector3 axis,
            Single angle)
        {
            Quaternion result;
            CreateFromAxisAngle (ref axis, ref angle, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion CreateFromYawPitchRoll (
            Single yaw,
            Single pitch,
            Single roll)
        {
            Quaternion result;
            CreateFromYawPitchRoll (ref yaw, ref pitch, ref roll, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion CreateFromRotationMatrix (
            Matrix44 matrix)
        {
            Quaternion result;
            CreateFromRotationMatrix (ref matrix, out result);
            return result;
        }
        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single LengthSquared (Quaternion quaternion)
        {
            Single result;
            LengthSquared (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single Length (Quaternion quaternion)
        {
            Single result;
            Length (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean IsUnit (Quaternion quaternion)
        {
            Boolean result;
            IsUnit (ref quaternion, out result);
            return result;
        }


        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Conjugate (Quaternion quaternion)
        {
            Quaternion result;
            Conjugate (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Inverse (Quaternion quaternion)
        {
            Quaternion result;
            Inverse (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single Dot (
            Quaternion quaternion1,
            Quaternion quaternion2)
        {
            Single result;
            Dot (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Concatenate (
            Quaternion quaternion1,
            Quaternion quaternion2)
        {
            Quaternion result;
            Concatenate (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Normalise (Quaternion quaternion)
        {
            Quaternion result;
            Normalise (ref quaternion, out result);
            return result;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean Equals (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Boolean result;
            Equals (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator == (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Boolean result;
            Equals (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator != (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Boolean result;
            Equals (ref quaternion1, ref quaternion2, out result);
            return !result;
        }

        // Variant Addition Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Add (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Add (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion operator + (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Add (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        // Variant Subtraction Operators //-----------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Subtract (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Subtract (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion operator - (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Subtract (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        // Variant Negation Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Negate (Quaternion quaternion)
        {
            Quaternion result;
            Negate (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion operator - (Quaternion quaternion)
        {
            Quaternion result;
            Negate (ref quaternion, out result);
            return result;
        }

        // Variant Multiplication Operators //--------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Multiply (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Multiply (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion operator * (
            Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Multiply (ref quaternion1, ref quaternion2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Slerp (
            Quaternion quaternion1,
            Quaternion quaternion2,
            Single amount)
        {
            Quaternion result;
            Slerp (ref quaternion1, ref quaternion2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion Lerp (
            Quaternion quaternion1,
            Quaternion quaternion2,
            Single amount)
        {
            Quaternion result;
            Lerp (ref quaternion1, ref quaternion2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Single LengthSquared ()
        {
            Single result;
            LengthSquared (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Single Length ()
        {
            Single result;
            Length (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public void Normalise ()
        {
            Normalise (ref this, out this);
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Boolean IsUnit()
        {
            Boolean result;
            IsUnit (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public void Conjugate ()
        {
            Conjugate (ref this, out this);
        }

#endif
    }
    /// <summary>
    /// Single precision Vector2.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector2
        : IEquatable<Vector2>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector2.
        /// </summary>
        public Single X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector2.
        /// </summary>
        public Single Y;

        /// <summary>
        /// Initilises a new instance of Vector2 from two Single values
        /// representing X and Y respectively.
        /// </summary>
        public Vector2 (Single x, Single y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return String.Format ("{{X:{0} Y:{1}}}",
                X.ToString (), Y.ToString ());
        }

        /// <summary>
        /// Gets the hash code of the Vector2 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return X.GetHashCode ()
                 ^ Y.GetHashCode ().ShiftAndWrap (2);
        }

        /// <summary>
        /// Determines whether or not this Vector2 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            return (obj is Vector2)
                ? this.Equals ((Vector2) obj)
                : false;
        }

        #region IEquatable<Vector2>

        /// <summary>
        /// Determines whether or not this Vector2 object is equal to another
        /// Vector2 object.
        /// </summary>
        public Boolean Equals (Vector2 other)
        {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        #endregion

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Vector2 with all of its components set to zero.
        /// </summary>
        readonly static Vector2 zero;

        /// <summary>
        /// Defines a Vector2 with all of its components set to one.
        /// </summary>
        readonly static Vector2 one;

        /// <summary>
        /// Defines the unit Vector2 for the X-axis.
        /// </summary>
        readonly static Vector2 unitX;

        /// <summary>
        /// Defines the unit Vector2 for the Y-axis.
        /// </summary>
        readonly static Vector2 unitY;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Vector2 ()
        {
            zero =      new Vector2 ();
            one =       new Vector2 (1, 1);

            unitX =     new Vector2 (1, 0);
            unitY =     new Vector2 (0, 1);
        }

        /// <summary>
        /// Returns a Vector2 with all of its components set to zero.
        /// </summary>
        public static Vector2 Zero
        {
            get { return zero; }
        }
        
        /// <summary>
        /// Returns a Vector2 with all of its components set to one.
        /// </summary>
        public static Vector2 One
        {
            get { return one; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the X-axis.
        /// </summary>
        public static Vector2 UnitX
        {
            get { return unitX; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the Y-axis.
        /// </summary>
        public static Vector2 UnitY
        {
            get { return unitY; }
        }

        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static void Distance (
            ref Vector2 vector1, ref Vector2 vector2, out Single result)
        {
            Single dx = vector1.X - vector2.X;
            Single dy = vector1.Y - vector2.Y;

            Single lengthSquared = (dx * dx) + (dy * dy);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector2 vector1, ref Vector2 vector2, out Single result)
        {
            Single dx = vector1.X - vector2.X;
            Single dy = vector1.Y - vector2.Y;

            result = (dx * dx) + (dy * dy);
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static void Dot (
            ref Vector2 vector1, ref Vector2 vector2, out Single result)
        {
            result = (vector1.X * vector2.X) + (vector1.Y * vector2.Y);
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static void Normalise (ref Vector2 vector, out Vector2 result)
        {
            Single lengthSquared =
                (vector.X * vector.X) + (vector.Y * vector.Y);

            Single epsilon; Maths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Single.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single one = 1;
            Single multiplier = one / Maths.Sqrt (lengthSquared);

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;

        }

        /// <summary>
        /// Returns the vector of an incident vector reflected across the a
        /// specified normal vector.
        /// </summary>
        public static void Reflect (
            ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            Boolean normalIsUnit;
            Vector2.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
            {
                throw new ArgumentOutOfRangeException();
            }

            // dot = vector . normal
            //     = |vector| * [normal] * cosθ
            //     = |vector| * cosθ
            //     = adjacent
            Single dot;
            Dot(ref vector, ref normal, out dot);

            Single two = 2;
            Single twoDot = dot * two;

            // Starting vector minus twice the length of the adjcent projected
            // along the normal.
            Vector2 m;
            Vector2.Multiply (ref normal, ref twoDot, out m);
            Vector2.Subtract (ref vector, ref m, out result);
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector2 vector, ref Matrix44 matrix, out Vector2 result)
        {
            Single x =
                (vector.X * matrix.R0C0) +
                (vector.Y * matrix.R1C0) +
                matrix.R3C0;

            Single y =
                (vector.X * matrix.R0C1) +
                (vector.Y * matrix.R1C1) +
                matrix.R3C1;

            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Transforms a Vector2 by the specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector2 vector, ref Quaternion rotation, out Vector2 result)
        {
            Single two = 2;

            Single i = rotation.I;
            Single j = rotation.J;
            Single k = rotation.K;
            Single u = rotation.U;

            Single ii = i * i;
            Single jj = j * j;
            Single kk = k * k;

            Single uk = u * k;
            Single ij = i * j;

            result.X =
                + vector.X
                - (two * vector.X  * (jj + kk))
                + (two * vector.Y  * (ij - uk));

            result.Y =
                + vector.Y
                + (two * vector.X * (ij + uk))
                - (two * vector.Y * (ii + kk));
        }

        /// <summary>
        /// Transforms a normalised Vector2 by the specified Matrix.
        /// </summary>
        public static void TransformNormal (
            ref Vector2 normal, ref Matrix44 matrix, out Vector2 result)
        {
            Boolean normalIsUnit;
            Vector2.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Single x = (normal.X * matrix.R0C0) + (normal.Y * matrix.R1C0);
            Single y = (normal.X * matrix.R0C1) + (normal.Y * matrix.R1C1);

            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Calculates the length of the Vector2.
        /// </summary>
        public static void Length (
            ref Vector2 vector, out Single result)
        {
            Single lengthSquared =
                (vector.X * vector.X) + (vector.Y * vector.Y);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the length of the Vector2 squared.
        /// </summary>
        public static void LengthSquared (
            ref Vector2 vector, out Single result)
        {
            result = (vector.X * vector.X) + (vector.Y * vector.Y);
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not two Vector2 objects are equal.
        /// </summary>
        public static void Equals (
            ref Vector2 vector1, ref Vector2 vector2, out Boolean result)
        {
            result = (vector1.X == vector2.X) && (vector1.Y == vector2.Y);
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector2 objects.
        /// </summary>
        public static void Add (
            ref Vector2 vector1, ref Vector2 vector2, out Vector2 result)
        {
            result.X = vector1.X + vector2.X;
            result.Y = vector1.Y + vector2.Y;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector2 objects.
        /// </summary>
        public static void Subtract (
            ref Vector2 vector1, ref Vector2 vector2, out Vector2 result)
        {
            result.X = vector1.X - vector2.X;
            result.Y = vector1.Y - vector2.Y;
        }

        // Negation Operators //----------------------------------------------//

        /// <summary>
        /// Performs negation of a Vector2 object.
        /// </summary>
        public static void Negate (ref Vector2 vector, out Vector2 result)
        {
            result.X = -vector.X;
            result.Y = -vector.Y;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector2 objects.
        /// </summary>
        public static void Multiply (
            ref Vector2 vector1, ref Vector2 vector2, out Vector2 result)
        {
            result.X = vector1.X * vector2.X;
            result.Y = vector1.Y * vector2.Y;
        }

        /// <summary>
        /// Performs multiplication of a Vector2 object and a Single
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Vector2 vector, ref Single scaleFactor, out Vector2 result)
        {
            result.X = vector.X * scaleFactor;
            result.Y = vector.Y * scaleFactor;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector2 objects.
        /// </summary>
        public static void Divide (
            ref Vector2 vector1, ref Vector2 vector2, out Vector2 result)
        {
            result.X = vector1.X / vector2.X;
            result.Y = vector1.Y / vector2.Y;
        }

        /// <summary>
        /// Performs division of a Vector2 object and a Single precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Vector2 vector1, ref Single divider, out Vector2 result)
        {
            Single one = 1;
            Single num = one / divider;
            result.X = vector1.X * num;
            result.Y = vector1.Y * num;
        }

        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector2 vector1,
            ref Vector2 vector2,
            ref Single amount,
            out Vector2 result)
        {
            Single zero = 0;
            Single one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single two = 2;
            Single three = 3;

            amount = (amount * amount) * (three - (two * amount));

            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// Features:
        /// - The spline passes through all of the control points.
        /// - The spline is C^1 continuous, meaning that there are no
        ///   discontinuities in the tangent direction and magnitude.
        /// - The spline is not C^2 continuous.  The second derivative is
        ///   linearly interpolated within each segment, causing the curvature
        ///   to vary linearly over the length of the segment.
        /// </summary>
        public static void CatmullRom (
            ref Vector2 vector1,
            ref Vector2 vector2,
            ref Vector2 vector3,
            ref Vector2 vector4,
            ref Single amount,
            out Vector2 result)
        {
            Single zero = 0;
            Single one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single two = 2;
            Single three = 3;
            Single four = 4;
            Single five = 5;
            Single half; Maths.Half(out half);

            Single squared = amount * amount;
            Single cubed = amount * squared;

            ///////
            // X //
            ///////

            // (2 * P2)
            result.X = (two * vector2.X);

            // (-P1 + P3) * t
            result.X += (
                    - vector1.X
                    + vector3.X
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.X += (
                    + (two * vector1.X)
                    - (five * vector2.X)
                    + (four * vector3.X)
                    - (vector4.X)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.X += (
                    - (vector1.X)
                    + (three * vector2.X)
                    - (three * vector3.X)
                    + (vector4.X)
                ) * cubed;

            // 0.5
            result.X *= half;

            ///////
            // Y //
            ///////

            // (2 * P2)
            result.Y = (two * vector2.Y);

            // (-P1 + P3) * t
            result.Y += (
                    - vector1.Y
                    + vector3.Y
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.Y += (
                    + (two * vector1.Y)
                    - (five * vector2.Y)
                    + (four * vector3.Y)
                    - (vector4.Y)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.Y += (
                    - (vector1.Y)
                    + (three * vector2.Y)
                    - (three * vector3.Y)
                    + (vector4.Y)
                ) * cubed;

            // 0.5
            result.Y *= half;
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector2 vector1,
            ref Vector2 tangent1,
            ref Vector2 vector2,
            ref Vector2 tangent2,
            ref Single amount,
            out Vector2 result)
        {
            Single zero = 0;
            Single one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector2.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector2.IsUnit (ref tangent2, out tangent2IsUnit);
            if( !tangent1IsUnit || !tangent2IsUnit )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single two = 2;
            Single three = 3;

            Single squared = amount * amount;
            Single cubed = amount * squared;

            Single a = ((two * cubed) - (three * squared)) + one;
            Single b = (-two * cubed) + (three * squared);
            Single c = (cubed - (two * squared)) + amount;
            Single d = cubed - squared;

            result.X =
                (vector1.X * a) + (vector2.X * b) +
                (tangent1.X * c) + (tangent2.X * d);

            result.Y =
                (vector1.Y * a) + (vector2.Y * b) +
                (tangent1.Y * c) + (tangent2.Y * d);
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching
        /// pair of components.
        /// </summary>
        public static void Min (
            ref Vector2 a,
            ref Vector2 b,
            out Vector2 result)
        {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching
        /// pair of components.
        /// </summary>
        public static void Max (
            ref Vector2 a,
            ref Vector2 b,
            out Vector2 result)
        {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static void Clamp (
            ref Vector2 a,
            ref Vector2 min,
            ref Vector2 max,
            out Vector2 result)
        {
            Single x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;

            Single y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;

            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static void Lerp (
            ref Vector2 a,
            ref Vector2 b,
            ref Single amount,
            out Vector2 result)
        {
            Single zero = 0;
            Single one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
        }

        /// <summary>
        /// Detemines whether or not the Vector2 is of unit length.
        /// </summary>
        public static void IsUnit (ref Vector2 vector, out Boolean result)
        {
            Single one = 1;
            result = Maths.IsZero(
                one - vector.X * vector.X - vector.Y * vector.Y);
        }

#if (VARIANTS_ENABLED)

        // Variant Maths //---------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single Distance (
            Vector2 vector1, Vector2 vector2)
        {
            Single result;
            Distance (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single DistanceSquared (
            Vector2 vector1, Vector2 vector2)
        {
            Single result;
            DistanceSquared (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single Dot (
            Vector2 vector1, Vector2 vector2)
        {
            Single result;
            Dot (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Normalise (Vector2 vector)
        {
            Vector2 result;
            Normalise (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Reflect (
            Vector2 vector, Vector2 normal)
        {
            Vector2 result;
            Reflect (ref vector, ref normal, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Transform (
            Vector2 vector, Matrix44 matrix)
        {
            Vector2 result;
            Transform (ref vector, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Transform (
            Vector2 vector, Quaternion rotation)
        {
            Vector2 result;
            Transform (ref vector, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 TransformNormal (
            Vector2 normal, Matrix44 matrix)
        {
            Vector2 result;
            TransformNormal (ref normal, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single Length (Vector2 vector)
        {
            Single result;
            Length (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single LengthSquared (Vector2 vector)
        {
            Single result;
            LengthSquared (ref vector, out result);
            return result;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean Equals (Vector2 vector1, Vector2 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator == (Vector2 vector1, Vector2 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator != (Vector2 vector1, Vector2 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return !result;
        }

        // Variant Addition Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Add (
            Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Add (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator + (Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Add (ref vector1, ref vector2, out result);
            return result;
        }

        // Variant Subtraction Operators //-----------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Subtract (
            Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Subtract (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator - (Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Subtract (ref vector1, ref vector2, out result);
            return result;
        }

        // Variant Negation Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Negate (Vector2 vector)
        {
            Vector2 result;
            Negate (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator - (Vector2 vector)
        {
            Vector2 result;
            Negate (ref vector, out result);
            return result;
        }

        // Variant Multiplication Operators //--------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Multiply (
            Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Multiply (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Multiply (
            Vector2 vector, Single scaleFactor)
        {
            Vector2 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator * (
            Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Multiply (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator * (
            Vector2 vector, Single scaleFactor)
        {
            Vector2 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator * (
            Single scaleFactor, Vector2 vector)
        {
            Vector2 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        // Variant Division Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Divide (
            Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Divide (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Divide (
            Vector2 vector1, Single divider)
        {
            Vector2 result;
            Divide (ref vector1, ref divider, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator / (Vector2 vector1, Vector2 vector2)
        {
            Vector2 result;
            Divide (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator / (Vector2 vector1, Single divider)
        {
            Vector2 result;
            Divide (ref vector1, ref divider, out result);
            return result;
        }

        // Variant Splines //-------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 SmoothStep (
            Vector2 vector1,
            Vector2 vector2,
            Single amount)
        {
            Vector2 result;
            SmoothStep (ref vector1, ref vector2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 CatmullRom (
            Vector2 vector1,
            Vector2 vector2,
            Vector2 vector3,
            Vector2 vector4,
            Single amount)
        {
            Vector2 result;
            CatmullRom (
                ref vector1, ref vector2, ref vector3, ref vector4,
                ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Hermite (
            Vector2 vector1,
            Vector2 tangent1,
            Vector2 vector2,
            Vector2 tangent2,
            Single amount)
        {
            Vector2 result;
            Hermite (
                ref vector1, ref tangent1,
                ref vector2, ref tangent2,
                ref amount, out result);
            return result;
        }

        // Variant Utilities //-----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Min (
            Vector2 vector1,
            Vector2 vector2)
        {
            Vector2 result;
            Min (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Max (
            Vector2 vector1,
            Vector2 vector2)
        {
            Vector2 result;
            Max (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Clamp (
            Vector2 vector,
            Vector2 min,
            Vector2 max)
        {
            Vector2 result;
            Clamp (ref vector, ref min, ref max, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 Lerp (
            Vector2 vector1,
            Vector2 vector2,
            Single amount)
        {
            Vector2 result;
            Lerp (ref vector1, ref vector2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean IsUnit (Vector2 vector)
        {
            Boolean result;
            IsUnit (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Single Length ()
        {
            Single result;
            Length (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Single LengthSquared ()
        {
            Single result;
            LengthSquared (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public void Normalise ()
        {
            Normalise (ref this, out this);
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Boolean IsUnit()
        {
            Boolean result;
            IsUnit (ref this, out result);
            return result;
        }

#endif

    }

    /// <summary>
    /// Single precision Vector3.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector3
        : IEquatable<Vector3>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector3.
        /// </summary>
        public Single X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector3.
        /// </summary>
        public Single Y;

        /// <summary>
        /// Gets or sets the Z-component of the Vector3.
        /// </summary>
        public Single Z;

        /// <summary>
        /// Initilises a new instance of Vector3 from three Single values
        /// representing X, Y and Z respectively.
        /// </summary>
        public Vector3 (Single x, Single y, Single z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Initilises a new instance of Vector3 from one Vector2 value
        /// representing X and Y and one Single value representing Z.
        /// </summary>
        public Vector3 (Vector2 value, Single z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format ("{{X:{0} Y:{1} Z:{2}}}",
                X.ToString (), Y.ToString (), Z.ToString ());
        }

        /// <summary>
        /// Gets the hash code of the Vector3 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return X.GetHashCode ()
                 ^ Y.GetHashCode ().ShiftAndWrap (2)
                 ^ Z.GetHashCode ().ShiftAndWrap (4);
        }

        /// <summary>
        /// Determines whether or not this Vector3 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            return (obj is Vector3)
                ? this.Equals ((Vector3) obj)
                : false;
        }

        #region IEquatable<Vector3>

        /// <summary>
        /// Determines whether or not this Vector3 object is equal to another
        /// Vector3 object.
        /// </summary>
        public Boolean Equals (Vector3 other)
        {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        #endregion

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Vector3 with all of its components set to zero.
        /// </summary>
        static Vector3 zero;

        /// <summary>
        /// Defines a Vector3 with all of its components set to one.
        /// </summary>
        static Vector3 one;

        /// <summary>
        /// Defines the unit Vector3 for the X-axis.
        /// </summary>
        static Vector3 unitX;

        /// <summary>
        /// Defines the unit Vector3 for the Y-axis.
        /// </summary>
        static Vector3 unitY;

        /// <summary>
        /// Defines the unit Vector3 for the Z-axis.
        /// </summary>
        static Vector3 unitZ;

        /// <summary>
        /// Defines a unit Vector3 designating up (0, 1, 0).
        /// </summary>
        static Vector3 up;

        /// <summary>
        /// Defines a unit Vector3 designating down (0, −1, 0).
        /// </summary>
        static Vector3 down;

        /// <summary>
        /// Defines a unit Vector3 pointing to the right (1, 0, 0).
        /// </summary>
        static Vector3 right;

        /// <summary>
        /// Defines a unit Vector3 designating left (−1, 0, 0).
        /// </summary>
        static Vector3 left;

        /// <summary>
        /// Defines a unit Vector3 designating forward in a right-handed 
        /// coordinate system (0, 0, -1).
        /// </summary>
        static Vector3 forward;

        /// <summary>
        /// Defines a unit Vector3 designating backward in a right-handed 
        /// coordinate system (0, 0, 1).
        /// </summary>
        static Vector3 backward;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Vector3 ()
        {
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
        
        /// <summary>
        /// Returns a Vector3 with all of its components set to zero.
        /// </summary>
        public static Vector3 Zero
        {
            get { return zero; }
        }
        
        /// <summary>
        /// Returns a Vector3 with all of its components set to one.
        /// </summary>
        public static Vector3 One
        {
            get { return one; }
        }
        
        /// <summary>
        /// Returns the unit Vector3 for the X-axis.
        /// </summary>
        public static Vector3 UnitX
        {
            get { return unitX; }
        }

        /// <summary>
        /// Returns the unit Vector3 for the Y-axis.
        /// </summary>
        public static Vector3 UnitY
        {
            get { return unitY; }
        }
        
        /// <summary>
        /// Returns the unit Vector3 for the Z-axis.
        /// </summary>
        public static Vector3 UnitZ
        {
            get { return unitZ; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating up (0, 1, 0).
        /// </summary>
        public static Vector3 Up
        {
            get { return up; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating down (0, −1, 0).
        /// </summary>
        public static Vector3 Down
        {
            get { return down; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 pointing to the right (1, 0, 0).
        /// </summary>
        public static Vector3 Right
        {
            get { return right; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating left (−1, 0, 0).
        /// </summary>
        public static Vector3 Left
        {
            get { return left; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating forward in a right-handed 
        /// coordinate system (0, 0, -1).
        /// </summary>
        public static Vector3 Forward
        {
            get { return forward; }
        }
        
        /// <summary>
        /// Returns a unit Vector3 designating backward in a right-handed 
        /// coordinate system (0, 0, 1).
        /// </summary>
        public static Vector3 Backward
        {
            get { return backward; }
        }
        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static void Distance (
            ref Vector3 vector1, ref Vector3 vector2, out Single result)
        {
            Single dx = vector1.X - vector2.X;
            Single dy = vector1.Y - vector2.Y;
            Single dz = vector1.Z - vector2.Z;

            Single lengthSquared = (dx * dx) + (dy * dy) + (dz * dz);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector3 vector1, ref Vector3 vector2,
            out Single result)
        {
            Single dx = vector1.X - vector2.X;
            Single dy = vector1.Y - vector2.Y;
            Single dz = vector1.Z - vector2.Z;

            result = (dx * dx) + (dy * dy) + (dz * dz);
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static void Dot (
            ref Vector3 vector1, ref Vector3 vector2, out Single result)
        {
            result =
                (vector1.X * vector2.X) +
                (vector1.Y * vector2.Y) +
                (vector1.Z * vector2.Z);
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static void Normalise (ref Vector3 vector, out Vector3 result)
        {
            Single lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z);

            Single epsilon; Maths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Single.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single one = 1;
            Single multiplier = one / Maths.Sqrt (lengthSquared);

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
        }

        /// <summary>
        /// Calculates the cross product of two vectors.
        /// </summary>
        public static void Cross (
            ref Vector3 vector1, ref Vector3 vector2,
            out Vector3 result)
        {
            result.X = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y);
            result.Y = (vector1.Z * vector2.X) - (vector1.X * vector2.Z);
            result.Z = (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
        }

        /// <summary>
        /// Returns the vector of an incident vector reflected across the a
        /// specified normal vector.
        /// </summary>
        public static void Reflect (
            ref Vector3 vector, ref Vector3 normal, out Vector3 result)
        {
            Boolean normalIsUnit;
            Vector3.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single two = 2;

            Single num =
                (vector.X * normal.X) +
                (vector.Y * normal.Y) +
                (vector.Z * normal.Z);

            result.X = vector.X - ((two * num) * normal.X);
            result.Y = vector.Y - ((two * num) * normal.Y);
            result.Z = vector.Z - ((two * num) * normal.Z);
        }

        /// <summary>
        /// Transforms a Vector3 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector3 vector, ref Matrix44 matrix, out Vector3 result)
        {
            Single x =
                (vector.X * matrix.R0C0) +
                (vector.Y * matrix.R1C0) +
                (vector.Z * matrix.R2C0) + matrix.R3C0;

            Single y =
                (vector.X * matrix.R0C1) +
                (vector.Y * matrix.R1C1) +
                (vector.Z * matrix.R2C1) + matrix.R3C1;

            Single z =
                (vector.X * matrix.R0C2) +
                (vector.Y * matrix.R1C2) +
                (vector.Z * matrix.R2C2) + matrix.R3C2;

            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Transforms a vector by a specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector3 vector, ref Quaternion rotation, out Vector3 result)
        {
            Single two = 2;

            Single i = rotation.I;
            Single j = rotation.J;
            Single k = rotation.K;
            Single u = rotation.U;

            Single ii = i * i;
            Single jj = j * j;
            Single kk = k * k;

            Single ui = u * i;
            Single uj = u * j;
            Single uk = u * k;
            Single ij = i * j;
            Single ik = i * k;
            Single jk = j * k;

            result.X =
                + vector.X
                - (two * vector.X * (jj + kk))
                + (two * vector.Y * (ij - uk))
                + (two * vector.Z * (ik + uj));

            result.Y =
                + vector.Y
                + (two * vector.X * (ij + uk))
                - (two * vector.Y * (ii + kk))
                + (two * vector.Z * (jk - ui));

            result.Z =
                + vector.Z
                + (two * vector.X * (ik - uj))
                + (two * vector.Y * (jk + ui))
                - (two * vector.Z * (ii + jj));
        }

        /// <summary>
        /// Transforms a normalised Vector3 by a Matrix44.
        /// </summary>
        public static void TransformNormal (
            ref Vector3 normal, ref Matrix44 matrix, out Vector3 result)
        {
            Boolean normalIsUnit;
            Vector3.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Single x =
                (normal.X * matrix.R0C0) +
                (normal.Y * matrix.R1C0) +
                (normal.Z * matrix.R2C0);

            Single y =
                (normal.X * matrix.R0C1) +
                (normal.Y * matrix.R1C1) +
                (normal.Z * matrix.R2C1);

            Single z =
                (normal.X * matrix.R0C2) +
                (normal.Y * matrix.R1C2) +
                (normal.Z * matrix.R2C2);

            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Calculates the length of the Vector3.
        /// </summary>
        public static void Length (
            ref Vector3 vector, out Single result)
        {
            Single lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the length of the Vector3 squared.
        /// </summary>
        public static void LengthSquared (
            ref Vector3 vector, out Single result)
        {
            result =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z);
        }
        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not two Vector3 objects are equal.
        /// </summary>
        public static void Equals (
            ref Vector3 value1, ref Vector3 vector2, out Boolean result)
        {
            result =
                (value1.X == vector2.X) &&
                (value1.Y == vector2.Y) &&
                (value1.Z == vector2.Z);
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector3 objects.
        /// </summary>
        public static void Add (
            ref Vector3 value1, ref Vector3 vector2, out Vector3 result)
        {
            result.X = value1.X + vector2.X;
            result.Y = value1.Y + vector2.Y;
            result.Z = value1.Z + vector2.Z;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector3 objects.
        /// </summary>
        public static void Subtract (
            ref Vector3 value1, ref Vector3 vector2, out Vector3 result)
        {
            result.X = value1.X - vector2.X;
            result.Y = value1.Y - vector2.Y;
            result.Z = value1.Z - vector2.Z;
        }

        // Negation Operators //----------------------------------------------//

        /// <summary>
        /// Performs negation of a Vector3 object.
        /// </summary>
        public static void Negate (ref Vector3 value, out Vector3 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector3 objects.
        /// </summary>
        public static void Multiply (
            ref Vector3 value1, ref Vector3 vector2, out Vector3 result)
        {
            result.X = value1.X * vector2.X;
            result.Y = value1.Y * vector2.Y;
            result.Z = value1.Z * vector2.Z;
        }

        /// <summary>
        /// Performs multiplication of a Vector3 object and a Single
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Vector3 value1, ref Single scaleFactor, out Vector3 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector3 objects.
        /// </summary>
        public static void Divide (
            ref Vector3 value1, ref Vector3 vector2, out Vector3 result)
        {
            result.X = value1.X / vector2.X;
            result.Y = value1.Y / vector2.Y;
            result.Z = value1.Z / vector2.Z;
        }

        /// <summary>
        /// Performs division of a Vector3 object and a Single precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Vector3 value1, ref Single vector2, out Vector3 result)
        {
            Single one = 1;
            Single num = one / vector2;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
        }

        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector3 vector1,
            ref Vector3 vector2,
            ref Single amount,
            out Vector3 result)
        {
            Single zero = 0;
            Single one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single two = 2;
            Single three = 3;

            amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
            amount = (amount * amount) * (three - (two * amount));

            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
            result.Z = vector1.Z + ((vector2.Z - vector1.Z) * amount);
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// Features:
        /// - The spline passes through all of the control points.
        /// - The spline is C^1 continuous, meaning that there are no
        ///   discontinuities in the tangent direction and magnitude.
        /// - The spline is not C^2 continuous.  The second derivative is
        ///   linearly interpolated within each segment, causing the curvature
        ///   to vary linearly over the length of the segment.
        /// </summary>
        public static void CatmullRom (
            ref Vector3 vector1,
            ref Vector3 vector2,
            ref Vector3 vector3,
            ref Vector3 vector4,
            ref Single amount,
            out Vector3 result)
        {
            Single zero = 0;
            Single one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single two = 2;
            Single three = 3;
            Single four = 4;
            Single five = 5;
            Single half; Maths.Half(out half);

            Single squared = amount * amount;
            Single cubed = amount * squared;

            ///////
            // X //
            ///////

            // (2 * P2)
            result.X = (two * vector2.X);

            // (-P1 + P3) * t
            result.X += (
                    - vector1.X
                    + vector3.X
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.X += (
                    + (two * vector1.X)
                    - (five * vector2.X)
                    + (four * vector3.X)
                    - (vector4.X)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.X += (
                    - (vector1.X)
                    + (three * vector2.X)
                    - (three * vector3.X)
                    + (vector4.X)
                ) * cubed;

            // 0.5
            result.X *= half;

            ///////
            // Y //
            ///////

            // (2 * P2)
            result.Y = (two * vector2.Y);

            // (-P1 + P3) * t
            result.Y += (
                    - vector1.Y
                    + vector3.Y
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.Y += (
                    + (two * vector1.Y)
                    - (five * vector2.Y)
                    + (four * vector3.Y)
                    - (vector4.Y)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.Y += (
                    - (vector1.Y)
                    + (three * vector2.Y)
                    - (three * vector3.Y)
                    + (vector4.Y)
                ) * cubed;

            // 0.5
            result.Y *= half;

            ///////
            // Z //
            ///////

            // (2 * P2)
            result.Z = (two * vector2.Z);

            // (-P1 + P3) * t
            result.Z += (
                    - vector1.Z
                    + vector3.Z
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.Z += (
                    + (two * vector1.Z)
                    - (five * vector2.Z)
                    + (four * vector3.Z)
                    - (vector4.Z)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.Z += (
                    - (vector1.Z)
                    + (three * vector2.Z)
                    - (three * vector3.Z)
                    + (vector4.Z)
                ) * cubed;

            // 0.5
            result.Z *= half;
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector3 vector1,
            ref Vector3 tangent1,
            ref Vector3 vector2,
            ref Vector3 tangent2,
            ref Single amount,
            out Vector3 result)
        {
            Single zero = 0;
            Single one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector3.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector3.IsUnit (ref tangent2, out tangent2IsUnit);
            if( !tangent1IsUnit || !tangent2IsUnit )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single two = 2;
            Single three = 3;

            Single squared = amount * amount;
            Single cubed = amount * squared;

            Single a = ((two * cubed) - (three * squared)) + one;
            Single b = (-two * cubed) + (three * squared);
            Single c = (cubed - (two * squared)) + amount;
            Single d = cubed - squared;

            result.X =
                (vector1.X * a) + (vector2.X * b) +
                (tangent1.X * c) + (tangent2.X * d);

            result.Y =
                (vector1.Y * a) + (vector2.Y * b) +
                (tangent1.Y * c) + (tangent2.Y * d);

            result.Z =
                (vector1.Z * a) + (vector2.Z * b) +
                (tangent1.Z * c) + (tangent2.Z * d);
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching
        /// pair of components.
        /// </summary>
        public static void Min (
            ref Vector3 a,
            ref Vector3 b,
            out Vector3 result)
        {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
            result.Z = (a.Z < b.Z) ? a.Z : b.Z;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching
        /// pair of components.
        /// </summary>
        public static void Max (
            ref Vector3 a,
            ref Vector3 b,
            out Vector3 result)
        {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
            result.Z = (a.Z > b.Z) ? a.Z : b.Z;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static void Clamp (
            ref Vector3 a,
            ref Vector3 min,
            ref Vector3 max,
            out Vector3 result)
        {
            Single x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            Single y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            Single z = a.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static void Lerp (
            ref Vector3 a,
            ref Vector3 b,
            ref Single amount,
            out Vector3 result)
        {
            Single zero = 0;
            Single one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
            result.Z = a.Z + ((b.Z - a.Z) * amount);
        }

        /// <summary>
        /// Detemines whether or not the Vector3 is of unit length.
        /// </summary>
        public static void IsUnit (ref Vector3 vector, out Boolean result)
        {
            Single one = 1;
            result = Maths.IsZero(
                one
                - vector.X * vector.X
                - vector.Y * vector.Y
                - vector.Z * vector.Z);
        }
#if (VARIANTS_ENABLED)

        // Variant Maths //---------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single Distance (
            Vector3 vector1, Vector3 vector2)
        {
            Single result;
            Distance (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single DistanceSquared (
            Vector3 vector1, Vector3 vector2)
        {
            Single result;
            DistanceSquared (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single Dot (
            Vector3 vector1, Vector3 vector2)
        {
            Single result;
            Dot (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Normalise (Vector3 vector)
        {
            Vector3 result;
            Normalise (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Reflect (
            Vector3 vector, Vector3 normal)
        {
            Vector3 result;
            Reflect (ref vector, ref normal, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Transform (
            Vector3 vector, Matrix44 matrix)
        {
            Vector3 result;
            Transform (ref vector, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Transform (
            Vector3 vector, Quaternion rotation)
        {
            Vector3 result;
            Transform (ref vector, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 TransformNormal (
            Vector3 normal, Matrix44 matrix)
        {
            Vector3 result;
            TransformNormal (ref normal, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single Length (Vector3 vector)
        {
            Single result;
            Length (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single LengthSquared (Vector3 vector)
        {
            Single result;
            LengthSquared (ref vector, out result);
            return result;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean Equals (Vector3 vector1, Vector3 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator == (Vector3 vector1, Vector3 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator != (Vector3 vector1, Vector3 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return !result;
        }

        // Variant Addition Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Add (
            Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Add (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator + (Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Add (ref vector1, ref vector2, out result);
            return result;
        }

        // Variant Subtraction Operators //-----------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Subtract (
            Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Subtract (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator - (Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Subtract (ref vector1, ref vector2, out result);
            return result;
        }

        // Variant Negation Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Negate (Vector3 vector)
        {
            Vector3 result;
            Negate (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator - (Vector3 vector)
        {
            Vector3 result;
            Negate (ref vector, out result);
            return result;
        }

        // Variant Multiplication Operators //--------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Multiply (
            Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Multiply (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Multiply (
            Vector3 vector, Single scaleFactor)
        {
            Vector3 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator * (
            Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Multiply (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator * (
            Vector3 vector, Single scaleFactor)
        {
            Vector3 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator * (
            Single scaleFactor, Vector3 vector)
        {
            Vector3 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        // Variant Division Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Divide (
            Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Divide (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Divide (
            Vector3 vector1, Single divider)
        {
            Vector3 result;
            Divide (ref vector1, ref divider, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator / (Vector3 vector1, Vector3 vector2)
        {
            Vector3 result;
            Divide (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator / (Vector3 vector1, Single divider)
        {
            Vector3 result;
            Divide (ref vector1, ref divider, out result);
            return result;
        }

        // Variant Splines //-------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 SmoothStep (
            Vector3 vector1,
            Vector3 vector2,
            Single amount)
        {
            Vector3 result;
            SmoothStep (ref vector1, ref vector2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 CatmullRom (
            Vector3 vector1,
            Vector3 vector2,
            Vector3 vector3,
            Vector3 vector4,
            Single amount)
        {
            Vector3 result;
            CatmullRom (
                ref vector1, ref vector2, ref vector3, ref vector4,
                ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Hermite (
            Vector3 vector1,
            Vector3 tangent1,
            Vector3 vector2,
            Vector3 tangent2,
            Single amount)
        {
            Vector3 result;
            Hermite (
                ref vector1, ref tangent1,
                ref vector2, ref tangent2,
                ref amount, out result);
            return result;
        }

        // Variant Utilities //-----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Min (
            Vector3 vector1,
            Vector3 vector2)
        {
            Vector3 result;
            Min (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Max (
            Vector3 vector1,
            Vector3 vector2)
        {
            Vector3 result;
            Max (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Clamp (
            Vector3 vector,
            Vector3 min,
            Vector3 max)
        {
            Vector3 result;
            Clamp (ref vector, ref min, ref max, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 Lerp (
            Vector3 vector1,
            Vector3 vector2,
            Single amount)
        {
            Vector3 result;
            Lerp (ref vector1, ref vector2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean IsUnit (Vector3 vector)
        {
            Boolean result;
            IsUnit (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Single Length ()
        {
            Single result;
            Length (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Single LengthSquared ()
        {
            Single result;
            LengthSquared (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public void Normalise ()
        {
            Normalise (ref this, out this);
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Boolean IsUnit()
        {
            Boolean result;
            IsUnit (ref this, out result);
            return result;
        }

#endif
    }

    /// <summary>
    /// Single precision Vector4.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector4
        : IEquatable<Vector4>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector4.
        /// </summary>
        public Single X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector4.
        /// </summary>
        public Single Y;

        /// <summary>
        /// Gets or sets the Z-component of the Vector4.
        /// </summary>
        public Single Z;

        /// <summary>
        /// Gets or sets the W-component of the Vector4.
        /// </summary>
        public Single W;

        /// <summary>
        /// Initilises a new instance of Vector4 from four Single values
        /// representing X, Y, Z and W respectively.
        /// </summary>
        public Vector4 (
            Single x,
            Single y,
            Single z,
            Single w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initilises a new instance of Vector4 from one Vector2 value
        /// representing X and Y and two Single values representing Z and
        /// W respectively.
        /// </summary>
        public Vector4 (Vector2 value, Single z, Single w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initilises a new instance of Vector4 from one Vector3 value
        /// representing X, Y and Z and one Single value representing W.
        /// </summary>
        public Vector4 (Vector3 value, Single w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return string.Format ("{{X:{0} Y:{1} Z:{2} W:{3}}}",
                X.ToString (), Y.ToString (), Z.ToString (), W.ToString ());
        }

        /// <summary>
        /// Gets the hash code of the Vector4 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return X.GetHashCode ()
                 ^ Y.GetHashCode ().ShiftAndWrap (2)
                 ^ Z.GetHashCode ().ShiftAndWrap (4)
                 ^ W.GetHashCode ().ShiftAndWrap (6);
        }

        /// <summary>
        /// Determines whether or not this Vector4 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            return (obj is Vector4)
                ? this.Equals ((Vector4)obj)
                : false;
        }

        #region IEquatable<Vector4>

        /// <summary>
        /// Determines whether or not this Vector4 object is equal to another
        /// Vector4 object.
        /// </summary>
        public Boolean Equals (Vector4 other)
        {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        #endregion

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines a Vector2 with all of its components set to zero.
        /// </summary>
        static Vector4 zero;

        /// <summary>
        /// Defines a Vector2 with all of its components set to one.
        /// </summary>
        static Vector4 one;

        /// <summary>
        /// Defines the unit Vector2 for the X-axis.
        /// </summary>
        static Vector4 unitX;

        /// <summary>
        /// Defines the unit Vector2 for the Y-axis.
        /// </summary>
        static Vector4 unitY;

        /// <summary>
        /// Defines the unit Vector2 for the Z-axis.
        /// </summary>
        static Vector4 unitZ;

        /// <summary>
        /// Defines the unit Vector2 for the W-axis.
        /// </summary>
        static Vector4 unitW;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Vector4 ()
        {
            zero =      new Vector4 ();
            one =       new Vector4 (1, 1, 1, 1);

            unitX =     new Vector4 (1, 0, 0, 0);
            unitY =     new Vector4 (0, 1, 0, 0);
            unitZ =     new Vector4 (0, 0, 1, 0);
            unitW =     new Vector4 (0, 0, 0, 1);
        }

        /// <summary>
        /// Returns a Vector4 with all of its components set to zero.
        /// </summary>
        public static Vector4 Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Returns a Vector4 with all of its components set to one.
        /// </summary>
        public static Vector4 One
        {
            get { return one; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the X-axis.
        /// </summary>
        public static Vector4 UnitX
        {
            get { return unitX; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the Y-axis.
        /// </summary>
        public static Vector4 UnitY
        {
            get { return unitY; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the Z-axis.
        /// </summary>
        public static Vector4 UnitZ
        {
            get { return unitZ; }
        }
        
        /// <summary>
        /// Returns the unit Vector2 for the W-axis.
        /// </summary>
        public static Vector4 UnitW
        {
            get { return unitW; }
        }
        
        // Maths //-----------------------------------------------------------//

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        public static void Distance (
            ref Vector4 vector1, ref Vector4 vector2, out Single result)
        {
            Single dx = vector1.X - vector2.X;
            Single dy = vector1.Y - vector2.Y;
            Single dz = vector1.Z - vector2.Z;
            Single dw = vector1.W - vector2.W;

            Single lengthSquared =
                (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector4 vector1, ref Vector4 vector2, out Single result)
        {
            Single dx = vector1.X - vector2.X;
            Single dy = vector1.Y - vector2.Y;
            Single dz = vector1.Z - vector2.Z;
            Single dw = vector1.W - vector2.W;

            result = (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
        }

        /// <summary>
        /// Calculates the dot product of two vectors. If the two vectors are
        /// unit vectors, the dot product returns a floating point vector between
        /// -1 and 1 that can be used to determine some properties of the angle
        /// between two vectors. For example, it can show whether the vectors
        /// are orthogonal, parallel, or have an acute or obtuse angle between
        /// them.
        /// </summary>
        public static void Dot (
            ref Vector4 vector1, ref Vector4 vector2, out Single result)
        {
            result =
                (vector1.X * vector2.X) +
                (vector1.Y * vector2.Y) +
                (vector1.Z * vector2.Z) +
                (vector1.W * vector2.W);
        }

        /// <summary>
        /// Creates a unit vector from the specified vector. The result is a
        /// vector one unit in length pointing in the same direction as the
        /// original vector.
        /// </summary>
        public static void Normalise (
            ref Vector4 vector, out Vector4 result)
        {
            Single lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z) +
                (vector.W * vector.W);

            Single epsilon; Maths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Single.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single one = 1;
            Single multiplier = one / (Maths.Sqrt (lengthSquared));

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
            result.W = vector.W * multiplier;
        }

        /// <summary>
        /// Transforms a Vector4 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector4 vector, ref Matrix44 matrix, out Vector4 result)
        {
            Single x =
                (vector.X * matrix.R0C0) +
                (vector.Y * matrix.R1C0) +
                (vector.Z * matrix.R2C0) +
                (vector.W * matrix.R3C0);

            Single y =
                (vector.X * matrix.R0C1) +
                (vector.Y * matrix.R1C1) +
                (vector.Z * matrix.R2C1) +
                (vector.W * matrix.R3C1);

            Single z =
                (vector.X * matrix.R0C2) +
                (vector.Y * matrix.R1C2) +
                (vector.Z * matrix.R2C2) +
                (vector.W * matrix.R3C2);

            Single w =
                (vector.X * matrix.R0C3) +
                (vector.Y * matrix.R1C3) +
                (vector.Z * matrix.R2C3) +
                (vector.W * matrix.R3C3);

            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }

        /// <summary>
        /// Transforms a Vector4 by the specified Quaternion.
        /// </summary>
        public static void Transform (
            ref Vector4 vector, ref Quaternion rotation, out Vector4 result)
        {
            Single two = 2;

            Single i = rotation.I;
            Single j = rotation.J;
            Single k = rotation.K;
            Single u = rotation.U;

            Single ii = i * i;
            Single jj = j * j;
            Single kk = k * k;

            Single ui = u * i;
            Single uj = u * j;
            Single uk = u * k;
            Single ij = i * j;
            Single ik = i * k;
            Single jk = j * k;

            result.X =
                + vector.X
                - (two * vector.X * (jj + kk))
                + (two * vector.Y * (ij - uk))
                + (two * vector.Z * (ik + uj));

            result.Y =
                + vector.Y
                + (two * vector.X * (ij + uk))
                - (two * vector.Y * (ii + kk))
                + (two * vector.Z * (jk - ui));

            result.Z =
                + vector.Z
                + (two * vector.X * (ik - uj))
                + (two * vector.Y * (jk + ui))
                - (two * vector.Z * (ii + jj));

            result.W = vector.W;
        }

        /// <summary>
        /// Transforms a normalised Vector4 by a Matrix44.
        /// </summary>
        public static void TransformNormal (
            ref Vector4 normal, ref Matrix44 matrix, out Vector4 result)
        {
            Boolean normalIsUnit;
            Vector4.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Single x =
                (normal.X * matrix.R0C0) + (normal.Y * matrix.R1C0) +
                (normal.Z * matrix.R2C0) + (normal.W * matrix.R3C0);

            Single y =
                (normal.X * matrix.R0C1) + (normal.Y * matrix.R1C1) +
                (normal.Z * matrix.R2C1) + (normal.W * matrix.R3C1);

            Single z =
                (normal.X * matrix.R0C2) + (normal.Y * matrix.R1C2) +
                (normal.Z * matrix.R2C2) + (normal.W * matrix.R3C2);

            Single w =
                (normal.X * matrix.R0C3) + (normal.Y * matrix.R1C3) +
                (normal.Z * matrix.R2C3) + (normal.W * matrix.R3C3);

            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }

        /// <summary>
        /// Calculates the length of the Vector4.
        /// </summary>
        public static void Length (ref Vector4 vector, out Single result)
        {
            Single lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z) +
                (vector.W * vector.W);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the length of the Vector4 squared.
        /// </summary>
        public static void LengthSquared (
            ref Vector4 vector, out Single result)
        {
            result =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z) +
                (vector.W * vector.W);
        }
        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not two Vector4 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static void Equals (
            ref Vector4 value1, ref Vector4 value2, out Boolean result)
        {
            result =
                (value1.X == value2.X) &&
                (value1.Y == value2.Y) &&
                (value1.Z == value2.Z) &&
                (value1.W == value2.W);
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Vector4 objects.
        /// </summary>
        public static void Add (
            ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
            result.W = value1.W + value2.W;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Vector4 objects.
        /// </summary>
        public static void Subtract (
            ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
            result.W = value1.W - value2.W;
        }

        // Negation Operators //----------------------------------------------//

        /// <summary>
        /// Performs negation of a Vector4 object.
        /// </summary>
        public static void Negate (
            ref Vector4 value, out Vector4 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Vector4 objects.
        /// </summary>
        public static void Multiply (
            ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
            result.W = value1.W * value2.W;
        }

        /// <summary>
        /// Performs multiplication of a Vector4 object and a Single
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Vector4 value1, ref Single scaleFactor, out Vector4 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
            result.W = value1.W * scaleFactor;
        }

        // Division Operators //----------------------------------------------//

        /// <summary>
        /// Performs division of two Vector4 objects.
        /// </summary>
        public static void Divide (
            ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
            result.W = value1.W / value2.W;
        }

        /// <summary>
        /// Performs division of a Vector4 object and a Single precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Vector4 value1, ref Single divider, out Vector4 result)
        {
            Single one = 1;
            Single num = one / divider;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
            result.W = value1.W * num;
        }

        // Splines //---------------------------------------------------------//

        /// <summary>
        /// Interpolates between two vectors using a cubic equation.
        /// </summary>
        public static void SmoothStep (
            ref Vector4 vector1,
            ref Vector4 vector2,
            ref Single amount,
            out Vector4 result)
        {
            Single zero = 0;
            Single one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single two = 2;
            Single three = 3;

            amount = (amount > one) ? one : ((amount < zero) ? zero : amount);
            amount = (amount * amount) * (three - (two * amount));

            result.X = vector1.X + ((vector2.X - vector1.X) * amount);
            result.Y = vector1.Y + ((vector2.Y - vector1.Y) * amount);
            result.Z = vector1.Z + ((vector2.Z - vector1.Z) * amount);
            result.W = vector1.W + ((vector2.W - vector1.W) * amount);
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// Features:
        /// - The spline passes through all of the control points.
        /// - The spline is C^1 continuous, meaning that there are no
        ///   discontinuities in the tangent direction and magnitude.
        /// - The spline is not C^2 continuous.  The second derivative is
        ///   linearly interpolated within each segment, causing the curvature
        ///   to vary linearly over the length of the segment.
        /// </summary>
        public static void CatmullRom (
            ref Vector4 vector1,
            ref Vector4 vector2,
            ref Vector4 vector3,
            ref Vector4 vector4,
            ref Single amount,
            out Vector4 result)
        {
            Single zero = 0;
            Single one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single two = 2;
            Single three = 3;
            Single four = 4;
            Single five = 5;
            Single half; Maths.Half(out half);

            Single squared = amount * amount;
            Single cubed = amount * squared;

            ///////
            // X //
            ///////

            // (2 * P2)
            result.X = (two * vector2.X);

            // (-P1 + P3) * t
            result.X += (
                    - vector1.X
                    + vector3.X
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.X += (
                    + (two * vector1.X)
                    - (five * vector2.X)
                    + (four * vector3.X)
                    - (vector4.X)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.X += (
                    - (vector1.X)
                    + (three * vector2.X)
                    - (three * vector3.X)
                    + (vector4.X)
                ) * cubed;

            // 0.5
            result.X *= half;

            ///////
            // Y //
            ///////

            // (2 * P2)
            result.Y = (two * vector2.Y);

            // (-P1 + P3) * t
            result.Y += (
                    - vector1.Y
                    + vector3.Y
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.Y += (
                    + (two * vector1.Y)
                    - (five * vector2.Y)
                    + (four * vector3.Y)
                    - (vector4.Y)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.Y += (
                    - (vector1.Y)
                    + (three * vector2.Y)
                    - (three * vector3.Y)
                    + (vector4.Y)
                ) * cubed;

            // 0.5
            result.Y *= half;

            ///////
            // Z //
            ///////

            // (2 * P2)
            result.Z = (two * vector2.Z);

            // (-P1 + P3) * t
            result.Z += (
                    - vector1.Z
                    + vector3.Z
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.Z += (
                    + (two * vector1.Z)
                    - (five * vector2.Z)
                    + (four * vector3.Z)
                    - (vector4.Z)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.Z += (
                    - (vector1.Z)
                    + (three * vector2.Z)
                    - (three * vector3.Z)
                    + (vector4.Z)
                ) * cubed;

            // 0.5
            result.Z *= half;

            ///////
            // W //
            ///////

            // (2 * P2)
            result.W = (two * vector2.W);

            // (-P1 + P3) * t
            result.W += (
                    - vector1.W
                    + vector3.W
                ) * amount;

            // (2*P1 - 5*P2 + 4*P3 - P4) * t^2
            result.W += (
                    + (two * vector1.W)
                    - (five * vector2.W)
                    + (four * vector3.W)
                    - (vector4.W)
                ) * squared;

            // (-P1 + 3*P2- 3*P3 + P4) * t^3
            result.W += (
                    - (vector1.W)
                    + (three * vector2.W)
                    - (three * vector3.W)
                    + (vector4.W)
                ) * cubed;

            // 0.5
            result.W *= half;
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        public static void Hermite (
            ref Vector4 vector1,
            ref Vector4 tangent1,
            ref Vector4 vector2,
            ref Vector4 tangent2,
            ref Single amount,
            out Vector4 result)
        {
            Single zero = 0;
            Single one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            // Make sure that the tangents have been normalised.
            Boolean tangent1IsUnit;
            Boolean tangent2IsUnit;
            Vector4.IsUnit (ref tangent1, out tangent1IsUnit);
            Vector4.IsUnit (ref tangent2, out tangent2IsUnit);
            if( !tangent1IsUnit || !tangent2IsUnit )
            {
                throw new ArgumentOutOfRangeException();
            }

            Single two = 2;
            Single three = 3;

            Single squared = amount * amount;
            Single cubed = amount * squared;

            Single a = ((two * cubed) - (three * squared)) + one;
            Single b = (-two * cubed) + (three * squared);
            Single c = (cubed - (two * squared)) + amount;
            Single d = cubed - squared;

            result.X =
                (vector1.X * a) + (vector2.X * b) +
                (tangent1.X * c) + (tangent2.X * d);

            result.Y =
                (vector1.Y * a) + (vector2.Y * b) +
                (tangent1.Y * c) + (tangent2.Y * d);

            result.Z =
                (vector1.Z * a) + (vector2.Z * b) +
                (tangent1.Z * c) + (tangent2.Z * d);

            result.W =
                (vector1.W * a) + (vector2.W * b) +
                (tangent1.W * c) + (tangent2.W * d);
        }

        // Utilities //-------------------------------------------------------//

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching
        /// pair of components.
        /// </summary>
        public static void Min (
            ref Vector4 a,
            ref Vector4 b,
            out Vector4 result)
        {
            result.X = (a.X < b.X) ? a.X : b.X;
            result.Y = (a.Y < b.Y) ? a.Y : b.Y;
            result.Z = (a.Z < b.Z) ? a.Z : b.Z;
            result.W = (a.W < b.W) ? a.W : b.W;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching
        /// pair of components.
        /// </summary>
        public static void Max (
            ref Vector4 a,
            ref Vector4 b,
            out Vector4 result)
        {
            result.X = (a.X > b.X) ? a.X : b.X;
            result.Y = (a.Y > b.Y) ? a.Y : b.Y;
            result.Z = (a.Z > b.Z) ? a.Z : b.Z;
            result.W = (a.W > b.W) ? a.W : b.W;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        public static void Clamp (
            ref Vector4 a,
            ref Vector4 min,
            ref Vector4 max,
            out Vector4 result)
        {
            Single x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            Single y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            Single z = a.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            Single w = a.W;
            w = (w > max.W) ? max.W : w;
            w = (w < min.W) ? min.W : w;
            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        public static void Lerp (
            ref Vector4 a,
            ref Vector4 b,
            ref Single amount,
            out Vector4 result)
        {
            Single zero = 0;
            Single one = 1;
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            result.X = a.X + ((b.X - a.X) * amount);
            result.Y = a.Y + ((b.Y - a.Y) * amount);
            result.Z = a.Z + ((b.Z - a.Z) * amount);
            result.W = a.W + ((b.W - a.W) * amount);
        }

        /// <summary>
        /// Detemines whether or not the Vector4 is of unit length.
        /// </summary>
        public static void IsUnit (ref Vector4 vector, out Boolean result)
        {
            Single one = 1;
            result = Maths.IsZero(
                one
                - vector.X * vector.X
                - vector.Y * vector.Y
                - vector.Z * vector.Z
                - vector.W * vector.W);
        }

#if (VARIANTS_ENABLED)

        // Variant Maths //---------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single Distance (
            Vector4 vector1, Vector4 vector2)
        {
            Single result;
            Distance (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single DistanceSquared (
            Vector4 vector1, Vector4 vector2)
        {
            Single result;
            DistanceSquared (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single Dot (
            Vector4 vector1, Vector4 vector2)
        {
            Single result;
            Dot (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Normalise (Vector4 vector)
        {
            Vector4 result;
            Normalise (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Transform (
            Vector4 vector, Matrix44 matrix)
        {
            Vector4 result;
            Transform (ref vector, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Transform (
            Vector4 vector, Quaternion rotation)
        {
            Vector4 result;
            Transform (ref vector, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 TransformNormal (
            Vector4 normal, Matrix44 matrix)
        {
            Vector4 result;
            TransformNormal (ref normal, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single Length (Vector4 vector)
        {
            Single result;
            Length (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Single LengthSquared (Vector4 vector)
        {
            Single result;
            LengthSquared (ref vector, out result);
            return result;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean Equals (Vector4 vector1, Vector4 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator == (Vector4 vector1, Vector4 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator != (Vector4 vector1, Vector4 vector2)
        {
            Boolean result;
            Equals (ref vector1, ref vector2, out result);
            return !result;
        }

        // Variant Addition Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Add (
            Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Add (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator + (Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Add (ref vector1, ref vector2, out result);
            return result;
        }

        // Variant Subtraction Operators //-----------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Subtract (
            Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Subtract (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator - (Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Subtract (ref vector1, ref vector2, out result);
            return result;
        }

        // Variant Negation Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Negate (Vector4 vector)
        {
            Vector4 result;
            Negate (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator - (Vector4 vector)
        {
            Vector4 result;
            Negate (ref vector, out result);
            return result;
        }

        // Variant Multiplication Operators //--------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Multiply (
            Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Multiply (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Multiply (
            Vector4 vector, Single scaleFactor)
        {
            Vector4 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator * (
            Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Multiply (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator * (
            Vector4 vector, Single scaleFactor)
        {
            Vector4 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator * (
            Single scaleFactor, Vector4 vector)
        {
            Vector4 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        // Variant Division Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Divide (
            Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Divide (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Divide (
            Vector4 vector1, Single divider)
        {
            Vector4 result;
            Divide (ref vector1, ref divider, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator / (Vector4 vector1, Vector4 vector2)
        {
            Vector4 result;
            Divide (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator / (Vector4 vector1, Single divider)
        {
            Vector4 result;
            Divide (ref vector1, ref divider, out result);
            return result;
        }

        // Variant Splines //-------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 SmoothStep (
            Vector4 vector1,
            Vector4 vector2,
            Single amount)
        {
            Vector4 result;
            SmoothStep (ref vector1, ref vector2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 CatmullRom (
            Vector4 vector1,
            Vector4 vector2,
            Vector4 vector3,
            Vector4 vector4,
            Single amount)
        {
            Vector4 result;
            CatmullRom (
                ref vector1, ref vector2, ref vector3, ref vector4,
                ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Hermite (
            Vector4 vector1,
            Vector4 tangent1,
            Vector4 vector2,
            Vector4 tangent2,
            Single amount)
        {
            Vector4 result;
            Hermite (
                ref vector1, ref tangent1,
                ref vector2, ref tangent2,
                ref amount, out result);
            return result;
        }

        // Variant Utilities //-----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Min (
            Vector4 vector1,
            Vector4 vector2)
        {
            Vector4 result;
            Min (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Max (
            Vector4 vector1,
            Vector4 vector2)
        {
            Vector4 result;
            Max (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Clamp (
            Vector4 vector,
            Vector4 min,
            Vector4 max)
        {
            Vector4 result;
            Clamp (ref vector, ref min, ref max, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 Lerp (
            Vector4 vector1,
            Vector4 vector2,
            Single amount)
        {
            Vector4 result;
            Lerp (ref vector1, ref vector2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean IsUnit (Vector4 vector)
        {
            Boolean result;
            IsUnit (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Single Length ()
        {
            Single result;
            Length (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Single LengthSquared ()
        {
            Single result;
            LengthSquared (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public void Normalise ()
        {
            Normalise (ref this, out this);
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Boolean IsUnit()
        {
            Boolean result;
            IsUnit (ref this, out result);
            return result;
        }

#endif
    }

    /// <summary>
    /// This class provides maths functions with consistent function
    /// signatures across all supported precisions.  The idea being
    /// the more you use this, the more you will be able to write
    /// code once and easily change the precision later.
    /// </summary>
    public static class Maths
    {
        /// <summary>
        /// Provides the constant E.
        /// </summary>
        public static void E (out Single value)
        {
            value = 2.71828183f;
        }

        /// <summary>
        /// Provides the constant Epsilon.
        /// </summary>
        public static void Epsilon (out Single value)
        {
            value = 1.0e-6f;
        }

        /// <summary>
        /// Provides the constant Half.
        /// </summary>
        public static void Half (out Single value)
        {
            value = 0.5f;
        }

        /// <summary>
        /// Provides the constant Quarter.
        /// </summary>
        public static void Quarter (out Single value)
        {
            value = 0.25f;
        }

        /// <summary>
        /// Provides the constant Log10E.
        /// </summary>
        public static void Log10E (out Single value)
        {
            value = 0.4342944821f;
        }

        /// <summary>
        /// Provides the constant Log2E.
        /// </summary>
        public static void Log2E (out Single value)
        {
            value = 1.442695f;
        }

        /// <summary>
        /// Provides the constant Pi.
        /// </summary>
        public static void Pi (out Single value)
        {
            value = 3.1415926536f;
        }

        /// <summary>
        /// Provides the constant Root2.
        /// </summary>
        public static void Root2 (out Single value)
        {
            value = 1.414213562f;
        }

        /// <summary>
        /// Provides the constant Root3.
        /// </summary>
        public static void Root3 (out Single value)
        {
            value = 1.732050808f;
        }

        /// <summary>
        /// Provides the constant Tau.
        /// </summary>
        public static void Tau (out Single value)
        {
            value = 6.283185f;
        }

        /// <summary>
        /// Provides the constant Zero.
        /// </summary>
        public static void Zero (out Single value)
        {
            value = 0f;
        }

        /// <summary>
        /// Provides the constant One.
        /// </summary>
        public static void One (out Single value)
        {
            value = 1f;
        }


        /// <summary>
        /// ArcCos.
        /// </summary>
        public static Single ArcCos (Single value)
        {
            return (Single) Math.Acos((Single) value);
        }

        /// <summary>
        /// ArcSin.
        /// </summary>
        public static Single ArcSin (Single value)
        {
            return (Single) Math.Asin((Single) value);
        }

        /// <summary>
        /// ArcTan.
        /// </summary>
        public static Single ArcTan (Single value)
        {
            return (Single) Math.Atan((Single) value);
        }

        /// <summary>
        /// Cos.
        /// </summary>
        public static Single Cos (Single value)
        {
            return (Single) Math.Cos((Single) value);
        }

        /// <summary>
        /// Sin.
        /// </summary>
        public static Single Sin (Single value)
        {
            return (Single) Math.Sin((Single) value);
        }

        /// <summary>
        /// Tan.
        /// </summary>
        public static Single Tan (Single value)
        {
            return (Single) Math.Tan((Single) value);
        }

        /// <summary>
        /// Sqrt.
        /// </summary>
        public static Single Sqrt (Single value)
        {
            return (Single) Math.Sqrt((Single) value);
        }

        /// <summary>
        /// Square.
        /// </summary>
        public static Single Square (Single value)
        {
            return value * value;
        }

        /// <summary>
        /// Abs.
        /// </summary>
        public static Single Abs (Single value)
        {
            return (Single)Math.Abs((Single)value);
        }


        /// <summary>
        /// ToRadians
        /// </summary>
        public static Single ToRadians(Single input)
        {
            Single tau; Tau(out tau);
            return input * tau / ((Single)360);
        }

        /// <summary>
        /// ToDegrees
        /// </summary>
        public static Single ToDegrees(Single input)
        {
            Single tau; Tau(out tau);
            return input / tau * ((Single)360);
        }

        /// <summary>
        /// FromFraction
        /// </summary>
        public static void FromFraction(
            Int32 numerator, Int32 denominator, out Single value)
        {
            value = (Single) numerator / (Single) denominator;
        }

        /// <summary>
        /// FromString
        /// </summary>
        public static void FromString(String str, out Single value)
        {
            Single.TryParse(str, out value);
        }

        /// <summary>
        /// IsZero
        /// </summary>
        public static Boolean IsZero(Single value)
        {
            Single ep;
            Epsilon(out ep);
            return Abs(value) < ep;
        }

        /// <summary>
        /// Min
        /// </summary>
        public static Single Min(Single a, Single b)
        {
            return a < b ? a : b;
        }

        /// <summary>
        /// Max
        /// </summary>
        public static Single Max(Single a, Single b)
        {
            return a > b ? a : b;
        }

        /// <summary>
        /// WithinEpsilon
        /// </summary>
        public static Boolean WithinEpsilon(Single a, Single b)
        {
            Single num = a - b;
            return ((-Single.Epsilon <= num) && (num <= Single.Epsilon));
        }

        /// <summary>
        /// Sign
        /// </summary>
        public static Int32 Sign(Single value)
        {
            if (value > 0)
            {
                return 1;
            }
            else if (value < 0)
            {
                return -1;
            }

            return 0;
        }
    }
    /// <summary>
    /// Single precision Matrix44.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Matrix44
        : IEquatable<Matrix44>
    {
        /// <summary>
        /// Gets or sets (Row 0, Column 0) of the Matrix44.
        /// </summary>
        public Single R0C0;

        /// <summary>
        /// Gets or sets (Row 0, Column 1) of the Matrix44.
        /// </summary>
        public Single R0C1;

        /// <summary>
        /// Gets or sets (Row 0, Column 2) of the Matrix44.
        /// </summary>
        public Single R0C2;

        /// <summary>
        /// Gets or sets (Row 0, Column 3) of the Matrix44.
        /// </summary>
        public Single R0C3;

        /// <summary>
        /// Gets or sets (Row 1, Column 0) of the Matrix44.
        /// </summary>
        public Single R1C0;

        /// <summary>
        /// Gets or sets (Row 1, Column 1) of the Matrix44.
        /// </summary>
        public Single R1C1;

        /// <summary>
        /// Gets or sets (ow 1, Column 2) of the Matrix44.
        /// </summary>
        public Single R1C2;

        /// <summary>
        /// Gets or sets (Row 1, Column 3) of the Matrix44.
        /// </summary>
        public Single R1C3;

        /// <summary>
        /// Gets or sets (Row 2, Column 0) of the Matrix44.
        /// </summary>
        public Single R2C0;

        /// <summary>
        /// Gets or sets (Row 2, Column 1) of the Matrix44.
        /// </summary>
        public Single R2C1;

        /// <summary>
        /// Gets or sets (Row 2, Column 2) of the Matrix44.
        /// </summary>
        public Single R2C2;

        /// <summary>
        /// Gets or sets (Row 2, Column 3) of the Matrix44.
        /// </summary>
        public Single R2C3;

        /// <summary>
        /// Gets or sets (Row 3, Column 0) of the Matrix44.
        /// </summary>
        public Single R3C0; // translation.x

        /// <summary>
        /// Gets or sets (Row 3, Column 1) of the Matrix44.
        /// </summary>
        public Single R3C1; // translation.y

        /// <summary>
        /// Gets or sets (Row 3, Column 2) of the Matrix44.
        /// </summary>
        public Single R3C2; // translation.z

        /// <summary>
        /// Gets or sets (Row 3, Column 3) of the Matrix44.
        /// </summary>
        public Single R3C3;

        /// <summary>
        /// Initilises a new instance of Matrix44 from sixteen Single
        /// values representing the matrix, in row major order, respectively.
        /// </summary>
        public Matrix44 (
            Single m00,
            Single m01,
            Single m02,
            Single m03,
            Single m10,
            Single m11,
            Single m12,
            Single m13,
            Single m20,
            Single m21,
            Single m22,
            Single m23,
            Single m30,
            Single m31,
            Single m32,
            Single m33)
        {
            this.R0C0 = m00;
            this.R0C1 = m01;
            this.R0C2 = m02;
            this.R0C3 = m03;
            this.R1C0 = m10;
            this.R1C1 = m11;
            this.R1C2 = m12;
            this.R1C3 = m13;
            this.R2C0 = m20;
            this.R2C1 = m21;
            this.R2C2 = m22;
            this.R2C3 = m23;
            this.R3C0 = m30;
            this.R3C1 = m31;
            this.R3C2 = m32;
            this.R3C3 = m33;
        }

        /// <summary>
        /// Retrieves a string representation of the current object.
        /// </summary>
        public override String ToString ()
        {
            return
                "{ " +
                String.Format (
                    "{{R0C0:{0} R0C1:{1} R0C2:{2} R0C3:{3}}} ",
                    new Object[] {
                        this.R0C0.ToString (),
                        this.R0C1.ToString (),
                        this.R0C2.ToString (),
                        this.R0C3.ToString ()}) +
                String.Format (
                    "{{R1C0:{0} R1C1:{1} R1C2:{2} R1C3:{3}}} ",
                    new Object[] {
                        this.R1C0.ToString (),
                        this.R1C1.ToString (),
                        this.R1C2.ToString (),
                        this.R1C3.ToString ()}) +
                String.Format (
                    "{{R2C0:{0} R2C1:{1} R2C2:{2} R2C3:{3}}} ",
                    new Object[] {
                        this.R2C0.ToString (),
                        this.R2C1.ToString (),
                        this.R2C2.ToString (),
                        this.R2C3.ToString ()}) +
                String.Format (
                    "{{R3C0:{0} R3C1:{1} R3C2:{2} R3C3:{3}}} ",
                    new Object[] {
                        this.R3C0.ToString (),
                        this.R3C1.ToString (),
                        this.R3C2.ToString (),
                        this.R3C3.ToString ()}) +
                "}";
        }

        /// <summary>
        /// Gets the hash code of the Matrix44 object.
        /// </summary>
        public override Int32 GetHashCode ()
        {
            return R0C0.GetHashCode ()
                ^ R0C1.GetHashCode ().ShiftAndWrap (2)
                ^ R0C2.GetHashCode ().ShiftAndWrap (4)
                ^ R0C3.GetHashCode ().ShiftAndWrap (6)
                ^ R1C0.GetHashCode ().ShiftAndWrap (8)
                ^ R1C1.GetHashCode ().ShiftAndWrap (10)
                ^ R1C2.GetHashCode ().ShiftAndWrap (12)
                ^ R1C3.GetHashCode ().ShiftAndWrap (14)
                ^ R2C0.GetHashCode ().ShiftAndWrap (16)
                ^ R2C1.GetHashCode ().ShiftAndWrap (18)
                ^ R2C2.GetHashCode ().ShiftAndWrap (20)
                ^ R2C3.GetHashCode ().ShiftAndWrap (22)
                ^ R3C0.GetHashCode ().ShiftAndWrap (24)
                ^ R3C1.GetHashCode ().ShiftAndWrap (26)
                ^ R3C2.GetHashCode ().ShiftAndWrap (28)
                ^ R3C3.GetHashCode ().ShiftAndWrap (30);
        }

        /// <summary>
        /// Determines whether or not this Matrix44 object is equal to another
        /// object.
        /// </summary>
        public override Boolean Equals (Object obj)
        {
            return (obj is Matrix44)
                ? this.Equals ((Matrix44)obj)
                : false;
        }

        #region IEquatable<Matrix44>

        /// <summary>
        /// Determines whether or not this Matrix44 object is equal to another
        /// Matrix44 object.
        /// </summary>
        public Boolean Equals (Matrix44 other)
        {
            Boolean result;
            Equals (ref this, ref other, out result);
            return result;
        }

        #endregion

        /// <summary>
        /// Gets and sets the up vector of the Matrix44.
        /// </summary>
        public Vector3 Up
        {
            get
            {
                Vector3 vector;
                vector.X = this.R1C0;
                vector.Y = this.R1C1;
                vector.Z = this.R1C2;
                return vector;
            }
            set
            {
                this.R1C0 = value.X;
                this.R1C1 = value.Y;
                this.R1C2 = value.Z;
            }
        }

        /// <summary>
        /// Gets and sets the down vector of the Matrix44.
        /// </summary>
        public Vector3 Down
        {
            get
            {
                Vector3 vector;
                vector.X = -this.R1C0;
                vector.Y = -this.R1C1;
                vector.Z = -this.R1C2;
                return vector;
            }
            set
            {
                this.R1C0 = -value.X;
                this.R1C1 = -value.Y;
                this.R1C2 = -value.Z;
            }
        }

        /// <summary>
        /// Gets and sets the right vector of the Matrix44.
        /// </summary>
        public Vector3 Right
        {
            get
            {
                Vector3 vector;
                vector.X = this.R0C0;
                vector.Y = this.R0C1;
                vector.Z = this.R0C2;
                return vector;
            }
            set
            {
                this.R0C0 = value.X;
                this.R0C1 = value.Y;
                this.R0C2 = value.Z;
            }
        }

        /// <summary>
        /// Gets and sets the left vector of the Matrix44.
        /// </summary>
        public Vector3 Left
        {
            get
            {
                Vector3 vector;
                vector.X = -this.R0C0;
                vector.Y = -this.R0C1;
                vector.Z = -this.R0C2;
                return vector;
            }
            set
            {
                this.R0C0 = -value.X;
                this.R0C1 = -value.Y;
                this.R0C2 = -value.Z;
            }
        }

        /// <summary>
        /// Gets and sets the forward vector of the Matrix44.
        /// </summary>
        public Vector3 Forward
        {
            get
            {
                Vector3 vector;
                vector.X = -this.R2C0;
                vector.Y = -this.R2C1;
                vector.Z = -this.R2C2;
                return vector;
            }
            set
            {
                this.R2C0 = -value.X;
                this.R2C1 = -value.Y;
                this.R2C2 = -value.Z;
            }
        }

        /// <summary>
        /// Gets and sets the backward vector of the Matrix44.
        /// </summary>
        public Vector3 Backward
        {
            get
            {
                Vector3 vector;
                vector.X = this.R2C0;
                vector.Y = this.R2C1;
                vector.Z = this.R2C2;
                return vector;
            }
            set
            {
                this.R2C0 = value.X;
                this.R2C1 = value.Y;
                this.R2C2 = value.Z;
            }
        }

        /// <summary>
        /// Gets and sets the translation vector of the Matrix44.
        /// </summary>
        public Vector3 Translation
        {
            get
            {
                Vector3 vector;
                vector.X = this.R3C0;
                vector.Y = this.R3C1;
                vector.Z = this.R3C2;
                return vector;
            }
            set
            {
                this.R3C0 = value.X;
                this.R3C1 = value.Y;
                this.R3C2 = value.Z;
            }
        }

        // Constants //-------------------------------------------------------//

        /// <summary>
        /// Defines the identity matrix.
        /// </summary>
        static Matrix44 identity;

        /// <summary>
        /// Defines the zero matrix.
        /// </summary>
        static Matrix44 zero;

        /// <summary>
        /// Static constructor used to initilise static constants.
        /// </summary>
        static Matrix44 ()
        {
            identity = new Matrix44 (
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

            zero = new Matrix44 (
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0);
        }

        /// <summary>
        /// Returns the identity matrix.
        /// </summary>
        public static Matrix44 Identity
        {
            get { return identity; }
        }

        /// <summary>
        /// Returns the zero matrix.
        /// </summary>
        public static Matrix44 Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Creates a translation matrix from a position.
        /// </summary>
        public static void CreateTranslation (
            ref Vector3 position,
            out Matrix44 result)
        {
            result.R0C0 = 1;
            result.R0C1 = 0;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = 1;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = 0;
            result.R2C2 = 1;
            result.R2C3 = 0;
            result.R3C0 = position.X;
            result.R3C1 = position.Y;
            result.R3C2 = position.Z;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a translation matrix from a position.
        /// </summary>
        public static void CreateTranslation (
            ref Single xPosition,
            ref Single yPosition,
            ref Single zPosition,
            out Matrix44 result)
        {
            result.R0C0 = 1;
            result.R0C1 = 0;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = 1;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = 0;
            result.R2C2 = 1;
            result.R2C3 = 0;
            result.R3C0 = xPosition;
            result.R3C1 = yPosition;
            result.R3C2 = zPosition;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a scaling matrix based on x, y, z.
        /// </summary>
        public static void CreateScale (
            ref Single xScale,
            ref Single yScale,
            ref Single zScale,
            out Matrix44 result)
        {
            result.R0C0 = xScale;
            result.R0C1 = 0;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = yScale;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = 0;
            result.R2C2 = zScale;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a scaling matrix based on a vector.
        /// </summary>
        public static void CreateScale (
            ref Vector3 scales,
            out Matrix44 result)
        {
            result.R0C0 = scales.X;
            result.R0C1 = 0;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = scales.Y;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = 0;
            result.R2C2 = scales.Z;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Create a scaling matrix consistant along each axis.
        /// </summary>
        public static void CreateScale (
            ref Single scale,
            out Matrix44 result)
        {
            result.R0C0 = scale;
            result.R0C1 = 0;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = scale;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = 0;
            result.R2C2 = scale;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a matrix representing a given rotation about the X axis.
        /// </summary>
        public static void CreateRotationX (
            ref Single radians,
            out Matrix44 result)
        {
            // http://en.wikipedia.org/wiki/Rotation_matrix
            Single cos = Maths.Cos (radians);
            Single sin = Maths.Sin (radians);

            result.R0C0 = 1;
            result.R0C1 = 0;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = cos;
            result.R1C2 = sin;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = -sin;
            result.R2C2 = cos;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a matrix representing a given rotation about the Y axis.
        /// </summary>
        public static void CreateRotationY (
            ref Single radians,
            out Matrix44 result)
        {
            // http://en.wikipedia.org/wiki/Rotation_matrix
            Single cos = Maths.Cos (radians);
            Single sin = Maths.Sin (radians);

            result.R0C0 = cos;
            result.R0C1 = 0;
            result.R0C2 = -sin;
            result.R0C3 = 0;
            result.R1C0 = 0;
            result.R1C1 = 1;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = sin;
            result.R2C1 = 0;
            result.R2C2 = cos;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a matrix representing a given rotation about the Z axis.
        /// </summary>
        public static void CreateRotationZ (
            ref Single radians,
            out Matrix44 result)
        {
            // http://en.wikipedia.org/wiki/Rotation_matrix
            Single cos = Maths.Cos (radians);
            Single sin = Maths.Sin (radians);

            result.R0C0 = cos;
            result.R0C1 = sin;
            result.R0C2 = 0;
            result.R0C3 = 0;
            result.R1C0 = -sin;
            result.R1C1 = cos;
            result.R1C2 = 0;
            result.R1C3 = 0;
            result.R2C0 = 0;
            result.R2C1 = 0;
            result.R2C2 = 1;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a new Matrix44 that rotates around an arbitrary vector.
        /// </summary>
        public static void CreateFromAxisAngle (
            ref Vector3 axis,
            ref Single angle,
            out Matrix44 result)
        {
            Single x = axis.X;
            Single y = axis.Y;
            Single z = axis.Z;

            Single sin = Maths.Sin (angle);
            Single cos = Maths.Cos (angle);

            Single xx = x * x;
            Single yy = y * y;
            Single zz = z * z;

            Single xy = x * y;
            Single xz = x * z;
            Single yz = y * z;

            result.R0C0 = xx + (cos * (1 - xx));
            result.R0C1 = xy - (cos * xy) + (sin * z);
            result.R0C2 = xz - (cos * xz) - (sin * y);
            result.R0C3 = 0;

            result.R1C0 = xy - (cos * xy) - (sin * z);
            result.R1C1 = yy + (cos * (1 - yy));
            result.R1C2 = yz - (cos * yz) + (sin * x);
            result.R1C3 = 0;

            result.R2C0 = xz - (cos * xz) + (sin * y);
            result.R2C1 = yz - (cos * yz) - (sin * x);
            result.R2C2 = zz + (cos * (1 - zz));
            result.R2C3 = 0;

            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a new Matrix44 from an ordered triplet of vectors (axes)
        /// that are pair-wise perpendicular, have unit length and have an
        /// orientation for each axis.
        /// </summary>
        public static void CreateFromCartesianAxes (
            ref Vector3 right,
            ref Vector3 up,
            ref Vector3 backward,
            out Matrix44 result)
        {
            Boolean isRightUnit; Vector3.IsUnit (ref right, out isRightUnit);
            Boolean isUpUnit; Vector3.IsUnit (ref up, out isUpUnit);
            Boolean isBackwardUnit;
            Vector3.IsUnit (ref backward, out isBackwardUnit);

            if(!isRightUnit || !isUpUnit || !isBackwardUnit )
                throw new ArgumentException(
                    "The input vertors must be normalised.");

            // Perhaps we shd assert here is the Vectors are not pair-wise
            // perpendicular.

            result.R0C0 = right.X;
            result.R0C1 = right.Y;
            result.R0C2 = right.Z;
            result.R0C3 = 0;
            result.R1C0 = up.X;
            result.R1C1 = up.Y;
            result.R1C2 = up.Z;
            result.R1C3 = 0;
            result.R2C0 = backward.X;
            result.R2C1 = backward.Y;
            result.R2C2 = backward.Z;
            result.R2C3 = 0;
            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a world matrix.
        /// This matrix includes rotation and translation, but not scaling.
        /// </summary>
        public static void CreateWorld (
            ref Vector3 position,
            ref Vector3 forward,
            ref Vector3 up,
            out Matrix44 result)
        {
            Vector3 backward;
            Vector3.Negate (ref forward, out backward);
            Vector3.Normalise (ref backward, out backward);

            Vector3 right;
            Vector3.Cross (ref up, ref backward, out right);
            Vector3.Normalise (ref right, out right);

            // We don't know if the inputs were actually perpendicular,
            // best make sure.
            Vector3 finalUp;
            Vector3.Cross (ref right, ref backward, out finalUp);
            Vector3.Normalise (ref finalUp, out finalUp);

            result.R0C0 = right.X;
            result.R0C1 = right.Y;
            result.R0C2 = right.Z;
            result.R0C3 = 0;
            result.R1C0 = finalUp.X;
            result.R1C1 = finalUp.Y;
            result.R1C2 = finalUp.Z;
            result.R1C3 = 0;
            result.R2C0 = backward.X;
            result.R2C1 = backward.Y;
            result.R2C2 = backward.Z;
            result.R2C3 = 0;
            result.R3C0 = position.X;
            result.R3C1 = position.Y;
            result.R3C2 = position.Z;
            result.R3C3 = 1;
        }

        /// <summary>
        /// Creates a rotation matrix from the given quaternion.
        /// </summary>
        public static void CreateFromQuaternion (
            ref Quaternion q, out Matrix44 result)
        {
            // http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToMatrix/

            Boolean qIsUnit;
            Quaternion.IsUnit (ref q, out qIsUnit);

            if(!qIsUnit)
                throw new ArgumentException(
                    "Input quaternion must be normalised.");

            Single zero = 0;
            Single one = 1;

            Single twoI = q.I + q.I;
            Single twoJ = q.J + q.J;
            Single twoK = q.K + q.K;

            Single twoUI = q.U * twoI;
            Single twoUJ = q.U * twoJ;
            Single twoUK = q.U * twoK;
            Single twoII = q.I * twoI;
            Single twoIJ = q.I * twoJ;
            Single twoIK = q.I * twoK;
            Single twoJJ = q.J * twoJ;
            Single twoJK = q.J * twoK;
            Single twoKK = q.K * twoK;

            result.R0C0 = one - twoJJ - twoKK;
            result.R1C0 = twoIJ - twoUK;
            result.R2C0 = twoIK + twoUJ;
            result.R3C0 = zero;

            result.R0C1 = twoIJ + twoUK;
            result.R1C1 = one - twoII - twoKK;
            result.R2C1 = twoJK - twoUI;
            result.R3C1 = zero;

            result.R0C2 = twoIK - twoUJ;
            result.R1C2 = twoJK + twoUI;
            result.R2C2 = one - twoII - twoJJ;
            result.R3C2 = zero;

            result.R0C3 = zero;
            result.R1C3 = zero;
            result.R2C3 = zero;
            result.R3C3 = one;
        }

        /// <summary>
        /// Creates a new rotation matrix from a specified yaw, pitch, and roll.
        /// </summary>
        public static void CreateFromYawPitchRoll (
            ref Single yaw,
            ref Single pitch,
            ref Single roll,
            out Matrix44 result)
        {
            Quaternion quaternion;

            Quaternion.CreateFromYawPitchRoll (
                ref yaw, ref pitch, ref roll, out quaternion);

            CreateFromQuaternion (ref quaternion, out result);
        }

        /// <summary>
        /// Creates a cylindrical billboard that rotates around a specified
        /// axis.  This method computes the facing direction of the billboard
        /// from the object position and camera position.  When the object and
        /// camera positions are too close, the matrix will not be accurate.
        /// To avoid this problem, the method uses the optional camera forward
        /// vector if the positions are too close.
        /// </summary>
        public static void CreateBillboard (
            ref Vector3 objectPosition,
            ref Vector3 cameraPosition,
            ref Vector3 cameraUpVector,
            ref Vector3? cameraForwardVector,
            out Matrix44 result)
        {
            Single zero = 0;
            Single one = 1;
            Single epsilon; Maths.Epsilon (out epsilon);

            Vector3 camToObjVec = objectPosition - cameraPosition;
            Single camToObjVecLL = camToObjVec.LengthSquared ();

            Vector3 v1;
            if (camToObjVecLL < epsilon)
            {
                v1 = cameraForwardVector.HasValue
                   ? -cameraForwardVector.Value
                   : Vector3.Forward;
            }
            else
            {
                Single t = one / Maths.Sqrt (camToObjVecLL);
                Vector3.Multiply (ref camToObjVec, ref t, out v1);
            }

            Vector3 v2;
            Vector3.Cross (ref cameraUpVector, ref v1, out v2);
            Vector3.Normalise (ref v2, out v2);

            Vector3 v3;
            Vector3.Cross (ref v1, ref v2, out v3);

            result.R0C0 = v2.X;
            result.R0C1 = v2.Y;
            result.R0C2 = v2.Z;
            result.R0C3 = zero;
            result.R1C0 = v3.X;
            result.R1C1 = v3.Y;
            result.R1C2 = v3.Z;
            result.R1C3 = zero;
            result.R2C0 = v1.X;
            result.R2C1 = v1.Y;
            result.R2C2 = v1.Z;
            result.R2C3 = zero;
            result.R3C0 = objectPosition.X;
            result.R3C1 = objectPosition.Y;
            result.R3C2 = objectPosition.Z;
            result.R3C3 = one;
        }

        /// <summary>
        /// Creates a cylindrical billboard that rotates around a specified
        /// axis.
        /// </summary>
        /// <remarks>
        /// This method computes the facing direction of the billboard from the
        /// object position and camera position. When the object and camera
        /// positions are too close, the matrix will not be accurate. To avoid
        /// this problem, the method uses the optional camera forward vector if
        /// the positions are too close.
        /// </remarks>
        public static void CreateConstrainedBillboard (
            ref Vector3 objectPosition,
            ref Vector3 cameraPosition,
            ref Vector3 rotateAxis,
            ref Vector3? cameraForwardVector,
            ref Vector3? objectForwardVector,
            out Matrix44 result)
        {
            Single zero = 0;
            Single one = 1;

            Single num;
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            vector2.X = objectPosition.X - cameraPosition.X;
            vector2.Y = objectPosition.Y - cameraPosition.Y;
            vector2.Z = objectPosition.Z - cameraPosition.Z;
            Single num2 = vector2.LengthSquared ();
            Single limit;
            Maths.FromString("0.0001", out limit);

            if (num2 < limit)
            {
                vector2 = cameraForwardVector.HasValue
                        ? -cameraForwardVector.Value
                        : Vector3.Forward;
            }
            else
            {
                Single t = one / Maths.Sqrt (num2);
                Vector3.Multiply (ref vector2, ref t, out vector2);
            }

            Vector3 vector4 = rotateAxis;
            Vector3.Dot (ref rotateAxis, ref vector2, out num);

            Single realHorrid;
            Maths.FromString("0.9982547", out realHorrid);

            if (Maths.Abs (num) > realHorrid)
            {
                if (objectForwardVector.HasValue)
                {
                    vector = objectForwardVector.Value;
                    Vector3.Dot (ref rotateAxis, ref vector, out num);

                    if (Maths.Abs (num) > realHorrid)
                    {
                        num = (rotateAxis.X * Vector3.Forward.X)
                            + (rotateAxis.Y * Vector3.Forward.Y)
                            + (rotateAxis.Z * Vector3.Forward.Z);

                        vector = (Maths.Abs (num) > realHorrid)
                               ? Vector3.Right
                               : Vector3.Forward;
                    }
                }
                else
                {
                    num = (rotateAxis.X * Vector3.Forward.X)
                        + (rotateAxis.Y * Vector3.Forward.Y)
                        + (rotateAxis.Z * Vector3.Forward.Z);

                    vector = (Maths.Abs (num) > realHorrid)
                           ? Vector3.Right
                           : Vector3.Forward;
                }

                Vector3.Cross (ref rotateAxis, ref vector, out vector3);
                Vector3.Normalise (ref vector3, out vector3);
                Vector3.Cross (ref vector3, ref rotateAxis, out vector);
                Vector3.Normalise (ref vector, out vector);
            }
            else
            {
                Vector3.Cross (ref rotateAxis, ref vector2, out vector3);
                Vector3.Normalise (ref vector3, out vector3);
                Vector3.Cross (ref vector3, ref vector4, out vector);
                Vector3.Normalise (ref vector, out vector);
            }

            result.R0C0 = vector3.X;
            result.R0C1 = vector3.Y;
            result.R0C2 = vector3.Z;
            result.R0C3 = zero;
            result.R1C0 = vector4.X;
            result.R1C1 = vector4.Y;
            result.R1C2 = vector4.Z;
            result.R1C3 = zero;
            result.R2C0 = vector.X;
            result.R2C1 = vector.Y;
            result.R2C2 = vector.Z;
            result.R2C3 = zero;
            result.R3C0 = objectPosition.X;
            result.R3C1 = objectPosition.Y;
            result.R3C2 = objectPosition.Z;
            result.R3C3 = one;
        }

        /// <summary>
        /// Builds a perspective projection matrix based on a field of view.
        /// </summary>
        /// <remarks>
        /// Projection space refers to the space after applying projection
        /// transformation from view space. After the projection transformation,
        /// visible content has x- and y-coordinates ranging from −1 to 1, and a
        /// z-coordinate ranging from 0 to 1.
        /// </remarks>
        public static void CreatePerspectiveFieldOfView (
            ref Single fieldOfView,
            ref Single aspectRatio,
            ref Single nearPlaneDistance,
            ref Single farPlaneDistance,
            out Matrix44 result)
        {
            // http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
            Single zero = 0;
            Single half; Maths.Half (out half);
            Single one = 1;
            Single pi; Maths.Pi (out pi);

            if (fieldOfView <= zero || fieldOfView >= pi)
                throw new ArgumentOutOfRangeException ("fieldOfView");

            if (nearPlaneDistance <= zero)
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");

            if (farPlaneDistance <= zero)
                throw new ArgumentOutOfRangeException ("farPlaneDistance");

            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");

            // xScale     0          0              0
            // 0        yScale       0              0
            // 0        0        zf/(zn-zf)        -1
            // 0        0        zn*zf/(zn-zf)      0

            // where:
            // yScale = cot(fovY/2)
            // xScale = yScale / aspect ratio

            // yScale = cot(fovY/2)
            Single yScale = one / (Maths.Tan (fieldOfView * half));

            // xScale = yScale / aspect ratio
            Single xScale = yScale / aspectRatio;

            result.R0C0 = xScale;
            result.R0C1 = zero;
            result.R0C2 = zero;
            result.R0C3 = zero;

            result.R1C0 = zero;
            result.R1C1 = yScale;
            result.R1C2 = zero;
            result.R1C3 = zero;

            result.R2C0 = zero;
            result.R2C1 = zero;
            // zf/(zn-zf)
            result.R2C2 = farPlaneDistance
                        / (nearPlaneDistance - farPlaneDistance);
            result.R2C3 = -one;

            result.R3C0 = zero;
            result.R3C1 = zero;
            // zn*zf/(zn-zf)
            result.R3C2 = (nearPlaneDistance * farPlaneDistance)
                        / (nearPlaneDistance - farPlaneDistance);
            result.R3C3 = zero;
        }

        /// <summary>
        /// Builds a perspective projection matrix.
        /// </summary>
        /// <remarks>
        /// Projection space refers to the space after applying projection
        /// transformation from view space. After the projection transformation,
        /// visible content has x- and y-coordinates ranging from −1 to 1, and a
        /// z-coordinate ranging from 0 to 1.
        /// </remarks>
        public static void CreatePerspective (
            ref Single width,
            ref Single height,
            ref Single nearPlaneDistance,
            ref Single farPlaneDistance,
            out Matrix44 result)
        {
            // http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
            Single zero = 0;
            Single one = 1;
            Single two = 2;

            if (nearPlaneDistance <= zero) {
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");
            }
            if (farPlaneDistance <= zero) {
                throw new ArgumentOutOfRangeException ("farPlaneDistance");
            }
            if (nearPlaneDistance >= farPlaneDistance) {
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");
            }
            result.R0C0 = (two * nearPlaneDistance) / width;
            result.R0C1 = result.R0C2 = result.R0C3 = zero;
            result.R1C1 = (two * nearPlaneDistance) / height;
            result.R1C0 = result.R1C2 = result.R1C3 = zero;
            result.R2C2 = farPlaneDistance
                        / (nearPlaneDistance - farPlaneDistance);
            result.R2C0 = result.R2C1 = zero;
            result.R2C3 = -one;
            result.R3C0 = result.R3C1 = result.R3C3 = zero;
            result.R3C2 = (nearPlaneDistance * farPlaneDistance)
                        / (nearPlaneDistance - farPlaneDistance);
        }

        /// <summary>
        /// Builds a customized, perspective projection matrix.
        /// </summary>
        /// <remarks>
        /// Projection space refers to the space after applying projection
        /// transformation from view space. After the projection transformation,
        /// visible content has x- and y-coordinates ranging from −1 to 1, and a
        /// z-coordinate ranging from 0 to 1.
        /// </remarks>
        public static void CreatePerspectiveOffCenter (
            ref Single left,
            ref Single right,
            ref Single bottom,
            ref Single top,
            ref Single nearPlaneDistance,
            ref Single farPlaneDistance,
            out Matrix44 result)
        {
            // http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx

            Single zero = 0;
            Single one = 1;
            Single two = 2;

            if (nearPlaneDistance <= zero) {
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");
            }
            if (farPlaneDistance <= zero) {
                throw new ArgumentOutOfRangeException ("farPlaneDistance");
            }
            if (nearPlaneDistance >= farPlaneDistance) {
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");
            }
            result.R0C0 = (two * nearPlaneDistance) / (right - left);
            result.R0C1 = result.R0C2 = result.R0C3 = zero;
            result.R1C1 = (two * nearPlaneDistance) / (top - bottom);
            result.R1C0 = result.R1C2 = result.R1C3 = zero;
            result.R2C0 = (left + right) / (right - left);
            result.R2C1 = (top + bottom) / (top - bottom);
            result.R2C2 = farPlaneDistance
                        / (nearPlaneDistance - farPlaneDistance);
            result.R2C3 = -one;
            result.R3C2 = (nearPlaneDistance * farPlaneDistance)
                        / (nearPlaneDistance - farPlaneDistance);
            result.R3C0 = result.R3C1 = result.R3C3 = zero;
        }

        /// <summary>
        /// Builds an orthogonal projection matrix.
        /// </summary>
        /// <remarks>
        /// Projection space refers to the space after applying projection
        /// transformation from view space. After the projection transformation,
        /// visible content has x and y coordinates ranging from -1 to 1, and z
        /// coordinates ranging from 0 to 1.
        ///
        /// Unlike perspective projection, in orthographic projection there is
        /// no perspective foreshortening.
        ///
        /// The viewable area of this orthographic projection extends from left
        /// to right on the x-axis, bottom to top on the y-axis, and zNearPlane
        /// to zFarPlane on the z-axis. These values are relative to the
        /// position and x, y, and z-axes of the view.
        /// </remarks>
        public static void CreateOrthographic (
            ref Single width,
            ref Single height,
            ref Single zNearPlane,
            ref Single zFarPlane,
            out Matrix44 result)
        {
            // http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
            Single zero = 0;
            Single one = 1;
            Single two = 2;

            result.R0C0 = two / width;
            result.R0C1 = result.R0C2 = result.R0C3 = zero;
            result.R1C1 = two / height;
            result.R1C0 = result.R1C2 = result.R1C3 = zero;
            result.R2C2 = one / (zNearPlane - zFarPlane);
            result.R2C0 = result.R2C1 = result.R2C3 = zero;
            result.R3C0 = result.R3C1 = zero;
            result.R3C2 = zNearPlane / (zNearPlane - zFarPlane);
            result.R3C3 = one;
        }

        /// <summary>
        /// Builds a customized, orthogonal projection matrix.
        /// </summary>
        /// <remarks>
        /// Projection space refers to the space after applying projection
        /// transformation from view space. After the projection transformation,
        /// visible content has x and y coordinates ranging from -1 to 1, and z
        /// coordinates ranging from 0 to 1.
        ///
        /// Unlike perspective projection, in orthographic projection there is
        /// no perspective foreshortening.
        ///
        /// The viewable area of this orthographic projection extends from left
        /// to right on the x-axis, bottom to top on the y-axis, and zNearPlane
        /// to zFarPlane on the z-axis. These values are relative to the
        /// position and x, y, and z-axes of the view.
        /// </remarks>
        public static void CreateOrthographicOffCenter (
            ref Single left,
            ref Single right,
            ref Single bottom,
            ref Single top,
            ref Single zNearPlane,
            ref Single zFarPlane,
            out Matrix44 result)
        {
            // http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx

            Single zero = 0;
            Single one = 1;
            Single two = 2;

            result.R0C0 = two / (right - left);
            result.R0C1 = result.R0C2 = result.R0C3 = zero;
            result.R1C1 = two / (top - bottom);
            result.R1C0 = result.R1C2 = result.R1C3 = zero;
            result.R2C2 = one / (zNearPlane - zFarPlane);
            result.R2C0 = result.R2C1 = result.R2C3 = zero;
            result.R3C0 = (left + right) / (left - right);
            result.R3C1 = (top + bottom) / (bottom - top);
            result.R3C2 = zNearPlane / (zNearPlane - zFarPlane);
            result.R3C3 = one;
        }

        /// <summary>
        /// Creates a view matrix.
        /// </summary>
        /// <remarks>
        /// View space, sometimes called camera space, is similar to world space
        /// in that it is typically used for the entire scene. However, in view
        /// space, the origin is at the viewer or camera.
        /// </remarks>
        public static void CreateLookAt (
            ref Vector3 cameraPosition,
            ref Vector3 cameraTarget,
            ref Vector3 cameraUpVector,
            out Matrix44 result)
        {
            // http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx

            Single zero = 0;
            Single one = 1;

            Vector3 forward = cameraPosition - cameraTarget;
            Vector3.Normalise (ref forward, out forward);

            Vector3 right;
            Vector3.Cross (ref cameraUpVector, ref forward, out right);
            Vector3.Normalise (ref right, out right);

            Vector3 up;
            Vector3.Cross (ref forward, ref right, out up);
            Vector3.Normalise (ref up, out up);

            result.R0C0 = right.X;
            result.R0C1 = up.X;
            result.R0C2 = forward.X;
            result.R0C3 = zero;

            result.R1C0 = right.Y;
            result.R1C1 = up.Y;
            result.R1C2 = forward.Y;
            result.R1C3 = zero;

            result.R2C0 = right.Z;
            result.R2C1 = up.Z;
            result.R2C2 = forward.Z;
            result.R2C3 = zero;

            Single a;
            Single b;
            Single c;

            Vector3.Dot (ref right, ref cameraPosition, out a);
            Vector3.Dot (ref up, ref cameraPosition, out b);
            Vector3.Dot (ref forward, ref cameraPosition, out c);

            result.R3C0 = -a;
            result.R3C1 = -b;
            result.R3C2 = -c;

            result.R3C3 = one;
        }

        /// <summary>
        /// Transposes the rows and columns of a matrix.  The transpose of a
        /// given matrix is the matrix which is formed by turning all the rows
        /// of a given matrix into columns and vice-versa.
        /// N.B. On a computer, one can often avoid explicitly transposing a
        /// matrix in memory by simply accessing the same data in a
        /// different order.
        /// </summary>
        public static void Transpose (ref Matrix44 m, out Matrix44 result)
        {
            result.R0C0 = m.R0C0;
            result.R1C1 = m.R1C1;
            result.R2C2 = m.R2C2;
            result.R3C3 = m.R3C3;

            Single t = m.R0C1;
            result.R0C1 = m.R1C0;
            result.R1C0 = t;

            t = m.R0C2;
            result.R0C2 = m.R2C0;
            result.R2C0 = t;

            t = m.R0C3;
            result.R0C3 = m.R3C0;
            result.R3C0 = t;

            t = m.R1C2;
            result.R1C2 = m.R2C1;
            result.R2C1 = t;

            t = m.R1C3;
            result.R1C3 = m.R3C1;
            result.R3C1 = t;

            t =  m.R2C3;
            result.R2C3 = m.R3C2;
            result.R3C2 = t;
        }

        /// <summary>
        /// Reference Implementation:
        /// Essential Mathemathics For Games & Interactive Applications
        /// </summary>
        public static void Decompose (
            ref Matrix44 matrix,
            out Vector3 scale,
            out Quaternion rotation,
            out Vector3 translation,
            out Boolean result)
        {
            translation.X = matrix.R3C0;
            translation.Y = matrix.R3C1;
            translation.Z = matrix.R3C2;

            Vector3 a = new Vector3(matrix.R0C0, matrix.R1C0, matrix.R2C0);
            Vector3 b = new Vector3(matrix.R0C1, matrix.R1C1, matrix.R2C1);
            Vector3 c = new Vector3(matrix.R0C2, matrix.R1C2, matrix.R2C2);

            Single aLen; Vector3.Length(ref a, out aLen); scale.X = aLen;
            Single bLen; Vector3.Length(ref b, out bLen); scale.Y = bLen;
            Single cLen; Vector3.Length(ref c, out cLen); scale.Z = cLen;

            if ( Maths.IsZero(scale.X) ||
                 Maths.IsZero(scale.Y) ||
                 Maths.IsZero(scale.Z) )
            {
                rotation = Quaternion.Identity;
                result = false;
            }

            Single epsilon; Maths.Epsilon(out epsilon);

            if (aLen < epsilon) a = Vector3.Zero;
            else Vector3.Normalise(ref a, out a);

            if (bLen < epsilon) b = Vector3.Zero;
            else Vector3.Normalise(ref b, out b);

            if (cLen < epsilon) c = Vector3.Zero;
            else Vector3.Normalise(ref c, out c);

            Vector3 right = new Vector3 (a.X, b.X, c.X);
            Vector3 up = new Vector3 (a.Y, b.Y, c.Y);
            Vector3 backward = new Vector3 (a.Z, b.Z, c.Z);

            if (right == Vector3.Zero) right = Vector3.Right;
            if (up == Vector3.Zero) up = Vector3.Up;
            if (backward == Vector3.Zero) backward = Vector3.Backward;

            Vector3.Normalise (ref right, out right);
            Vector3.Normalise (ref up, out up);
            Vector3.Normalise (ref backward, out backward);

            Matrix44 rotMat;
            Matrix44.CreateFromCartesianAxes(
                ref right, ref up, ref backward, out rotMat);

            Quaternion.CreateFromRotationMatrix (ref rotMat, out rotation);

            result = true;
        }

        /// <summary>
        /// A determinant is a scalar number which is calculated from a matrix.
        /// This number can determine whether a set of linear equations are
        /// solvable, in other words whether the matrix can be inverted.
        /// </summary>
        public static void Determinant (ref Matrix44 m, out Single result)
        {
            result =
                + m.R0C3 * m.R1C2 * m.R2C1 * m.R3C0
                - m.R0C2 * m.R1C3 * m.R2C1 * m.R3C0
                - m.R0C3 * m.R1C1 * m.R2C2 * m.R3C0
                + m.R0C1 * m.R1C3 * m.R2C2 * m.R3C0
                + m.R0C2 * m.R1C1 * m.R2C3 * m.R3C0
                - m.R0C1 * m.R1C2 * m.R2C3 * m.R3C0
                - m.R0C3 * m.R1C2 * m.R2C0 * m.R3C1
                + m.R0C2 * m.R1C3 * m.R2C0 * m.R3C1
                + m.R0C3 * m.R1C0 * m.R2C2 * m.R3C1
                - m.R0C0 * m.R1C3 * m.R2C2 * m.R3C1
                - m.R0C2 * m.R1C0 * m.R2C3 * m.R3C1
                + m.R0C0 * m.R1C2 * m.R2C3 * m.R3C1
                + m.R0C3 * m.R1C1 * m.R2C0 * m.R3C2
                - m.R0C1 * m.R1C3 * m.R2C0 * m.R3C2
                - m.R0C3 * m.R1C0 * m.R2C1 * m.R3C2
                + m.R0C0 * m.R1C3 * m.R2C1 * m.R3C2
                + m.R0C1 * m.R1C0 * m.R2C3 * m.R3C2
                - m.R0C0 * m.R1C1 * m.R2C3 * m.R3C2
                - m.R0C2 * m.R1C1 * m.R2C0 * m.R3C3
                + m.R0C1 * m.R1C2 * m.R2C0 * m.R3C3
                + m.R0C2 * m.R1C0 * m.R2C1 * m.R3C3
                - m.R0C0 * m.R1C2 * m.R2C1 * m.R3C3
                - m.R0C1 * m.R1C0 * m.R2C2 * m.R3C3
                + m.R0C0 * m.R1C1 * m.R2C2 * m.R3C3;
        }

        /// <summary>
        /// The inverse of a matrix is another matrix that when multiplied
        /// by the original matrix yields the identity matrix.
        /// </summary>
        public static void Invert (ref Matrix44 m, out Matrix44 result)
        {
            Single one = 1;
            Single d;
            Determinant (ref m, out d);
            Single s = one / d;

            result.R0C0 =
                + m.R1C2 * m.R2C3 * m.R3C1 - m.R1C3 * m.R2C2 * m.R3C1
                + m.R1C3 * m.R2C1 * m.R3C2 - m.R1C1 * m.R2C3 * m.R3C2
                - m.R1C2 * m.R2C1 * m.R3C3 + m.R1C1 * m.R2C2 * m.R3C3;

            result.R0C1 =
                + m.R0C3 * m.R2C2 * m.R3C1 - m.R0C2 * m.R2C3 * m.R3C1
                - m.R0C3 * m.R2C1 * m.R3C2 + m.R0C1 * m.R2C3 * m.R3C2
                + m.R0C2 * m.R2C1 * m.R3C3 - m.R0C1 * m.R2C2 * m.R3C3;

            result.R0C2 =
                + m.R0C2 * m.R1C3 * m.R3C1 - m.R0C3 * m.R1C2 * m.R3C1
                + m.R0C3 * m.R1C1 * m.R3C2 - m.R0C1 * m.R1C3 * m.R3C2
                - m.R0C2 * m.R1C1 * m.R3C3 + m.R0C1 * m.R1C2 * m.R3C3;

            result.R0C3 =
                + m.R0C3 * m.R1C2 * m.R2C1 - m.R0C2 * m.R1C3 * m.R2C1
                - m.R0C3 * m.R1C1 * m.R2C2 + m.R0C1 * m.R1C3 * m.R2C2
                + m.R0C2 * m.R1C1 * m.R2C3 - m.R0C1 * m.R1C2 * m.R2C3;

            result.R1C0 =
                + m.R1C3 * m.R2C2 * m.R3C0 - m.R1C2 * m.R2C3 * m.R3C0
                - m.R1C3 * m.R2C0 * m.R3C2 + m.R1C0 * m.R2C3 * m.R3C2
                + m.R1C2 * m.R2C0 * m.R3C3 - m.R1C0 * m.R2C2 * m.R3C3;

            result.R1C1 =
                + m.R0C2 * m.R2C3 * m.R3C0 - m.R0C3 * m.R2C2 * m.R3C0
                + m.R0C3 * m.R2C0 * m.R3C2 - m.R0C0 * m.R2C3 * m.R3C2
                - m.R0C2 * m.R2C0 * m.R3C3 + m.R0C0 * m.R2C2 * m.R3C3;

            result.R1C2 =
                + m.R0C3 * m.R1C2 * m.R3C0 - m.R0C2 * m.R1C3 * m.R3C0
                - m.R0C3 * m.R1C0 * m.R3C2 + m.R0C0 * m.R1C3 * m.R3C2
                + m.R0C2 * m.R1C0 * m.R3C3 - m.R0C0 * m.R1C2 * m.R3C3;

            result.R1C3 =
                + m.R0C2 * m.R1C3 * m.R2C0 - m.R0C3 * m.R1C2 * m.R2C0
                + m.R0C3 * m.R1C0 * m.R2C2 - m.R0C0 * m.R1C3 * m.R2C2
                - m.R0C2 * m.R1C0 * m.R2C3 + m.R0C0 * m.R1C2 * m.R2C3;

            result.R2C0 =
                + m.R1C1 * m.R2C3 * m.R3C0 - m.R1C3 * m.R2C1 * m.R3C0
                + m.R1C3 * m.R2C0 * m.R3C1 - m.R1C0 * m.R2C3 * m.R3C1
                - m.R1C1 * m.R2C0 * m.R3C3 + m.R1C0 * m.R2C1 * m.R3C3;

            result.R2C1 =
                + m.R0C3 * m.R2C1 * m.R3C0 - m.R0C1 * m.R2C3 * m.R3C0
                - m.R0C3 * m.R2C0 * m.R3C1 + m.R0C0 * m.R2C3 * m.R3C1
                + m.R0C1 * m.R2C0 * m.R3C3 - m.R0C0 * m.R2C1 * m.R3C3;

            result.R2C2 =
                + m.R0C1 * m.R1C3 * m.R3C0 - m.R0C3 * m.R1C1 * m.R3C0
                + m.R0C3 * m.R1C0 * m.R3C1 - m.R0C0 * m.R1C3 * m.R3C1
                - m.R0C1 * m.R1C0 * m.R3C3 + m.R0C0 * m.R1C1 * m.R3C3;

            result.R2C3 =
                + m.R0C3 * m.R1C1 * m.R2C0 - m.R0C1 * m.R1C3 * m.R2C0
                - m.R0C3 * m.R1C0 * m.R2C1 + m.R0C0 * m.R1C3 * m.R2C1
                + m.R0C1 * m.R1C0 * m.R2C3 - m.R0C0 * m.R1C1 * m.R2C3;

            result.R3C0 =
                + m.R1C2 * m.R2C1 * m.R3C0 - m.R1C1 * m.R2C2 * m.R3C0
                - m.R1C2 * m.R2C0 * m.R3C1 + m.R1C0 * m.R2C2 * m.R3C1
                + m.R1C1 * m.R2C0 * m.R3C2 - m.R1C0 * m.R2C1 * m.R3C2;

            result.R3C1 =
                + m.R0C1 * m.R2C2 * m.R3C0 - m.R0C2 * m.R2C1 * m.R3C0
                + m.R0C2 * m.R2C0 * m.R3C1 - m.R0C0 * m.R2C2 * m.R3C1
                - m.R0C1 * m.R2C0 * m.R3C2 + m.R0C0 * m.R2C1 * m.R3C2;

            result.R3C2 =
                + m.R0C2 * m.R1C1 * m.R3C0 - m.R0C1 * m.R1C2 * m.R3C0
                - m.R0C2 * m.R1C0 * m.R3C1 + m.R0C0 * m.R1C2 * m.R3C1
                + m.R0C1 * m.R1C0 * m.R3C2 - m.R0C0 * m.R1C1 * m.R3C2;

            result.R3C3 =
                + m.R0C1 * m.R1C2 * m.R2C0 - m.R0C2 * m.R1C1 * m.R2C0
                + m.R0C2 * m.R1C0 * m.R2C1 - m.R0C0 * m.R1C2 * m.R2C1
                - m.R0C1 * m.R1C0 * m.R2C2 + m.R0C0 * m.R1C1 * m.R2C2;


            Multiply (ref result, ref s, out result);
        }

        /// <summary>
        /// Transforms a Matrix (m) by applying a Quaternion rotation (q).
        /// </summary>
        public static void Transform (
            ref Matrix44 m, ref Quaternion q, out Matrix44 result)
        {
            Boolean qIsUnit;
            Quaternion.IsUnit (ref q, out qIsUnit);

            if(!qIsUnit)
                throw new ArgumentException(
                    "Input quaternion must be normalised.");

            // Could just do Matrix44.CreateFromQuaternionHere, but we won't
            // use all of the data, so just calculate what we need.
            Single zero = 0;
            Single one = 1;

            Single twoI = q.I + q.I;
            Single twoJ = q.J + q.J;
            Single twoK = q.K + q.K;

            Single twoUI = q.U * twoI;
            Single twoUJ = q.U * twoJ;
            Single twoUK = q.U * twoK;
            Single twoII = q.I * twoI;
            Single twoIJ = q.I * twoJ;
            Single twoIK = q.I * twoK;
            Single twoJJ = q.J * twoJ;
            Single twoJK = q.J * twoK;
            Single twoKK = q.K * twoK;

            Single tR0C0 = one - twoJJ - twoKK;
            Single tR1C0 = twoIJ - twoUK;
            Single tR2C0 = twoIK + twoUJ;
            //Single tR3C0 = zero;

            Single tR0C1 = twoIJ + twoUK;
            Single tR1C1 = one - twoII - twoKK;
            Single tR2C1 = twoJK - twoUI;
            //Single tR3C1 = zero;

            Single tR0C2 = twoIK - twoUJ;
            Single tR1C2 = twoJK + twoUI;
            Single tR2C2 = one - twoII - twoJJ;
            //Single tR3C2 = zero;

            //Single tR0C3 = zero;
            //Single tR1C3 = zero;
            //Single tR2C3 = zero;
            //Single tR3C3 = zero;


            // Could just multiply here, but we know a bunch of stuff in `t`
            // will be zero, so doing the following is the same, but with less
            // operations.
            result.R0C0 = m.R0C0 * tR0C0 + m.R0C1 * tR1C0 + m.R0C2 * tR2C0;
            result.R0C1 = m.R0C0 * tR0C1 + m.R0C1 * tR1C1 + m.R0C2 * tR2C1;
            result.R0C2 = m.R0C0 * tR0C2 + m.R0C1 * tR1C2 + m.R0C2 * tR2C2;
            result.R0C3 = m.R0C3;

            result.R1C0 = m.R1C0 * tR0C0 + m.R1C1 * tR1C0 + m.R1C2 * tR2C0;
            result.R1C1 = m.R1C0 * tR0C1 + m.R1C1 * tR1C1 + m.R1C2 * tR2C1;
            result.R1C2 = m.R1C0 * tR0C2 + m.R1C1 * tR1C2 + m.R1C2 * tR2C2;
            result.R1C3 = m.R1C3;

            result.R2C0 = m.R2C0 * tR0C0 + m.R2C1 * tR1C0 + m.R2C2 * tR2C0;
            result.R2C1 = m.R2C0 * tR0C1 + m.R2C1 * tR1C1 + m.R2C2 * tR2C1;
            result.R2C2 = m.R2C0 * tR0C2 + m.R2C1 * tR1C2 + m.R2C2 * tR2C2;
            result.R2C3 = m.R2C3;

            result.R3C0 = m.R3C0 * tR0C0 + m.R3C1 * tR1C0 + m.R3C2 * tR2C0;
            result.R3C1 = m.R3C0 * tR0C1 + m.R3C1 * tR1C1 + m.R3C2 * tR2C1;
            result.R3C2 = m.R3C0 * tR0C2 + m.R3C1 * tR1C2 + m.R3C2 * tR2C2;
            result.R3C3 = m.R3C3;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Determines whether or not two Matrix44 objects are equal using the
        /// (X==Y) operator.
        /// </summary>
        public static void Equals (
            ref Matrix44 m1, ref Matrix44 m2, out Boolean result)
        {
            result =
                (m1.R0C0 == m2.R0C0) && (m1.R1C1 == m2.R1C1) &&
                (m1.R2C2 == m2.R2C2) && (m1.R3C3 == m2.R3C3) &&
                (m1.R0C1 == m2.R0C1) && (m1.R0C2 == m2.R0C2) &&
                (m1.R0C3 == m2.R0C3) && (m1.R1C0 == m2.R1C0) &&
                (m1.R1C2 == m2.R1C2) && (m1.R1C3 == m2.R1C3) &&
                (m1.R2C0 == m2.R2C0) && (m1.R2C1 == m2.R2C1) &&
                (m1.R2C3 == m2.R2C3) && (m1.R3C0 == m2.R3C0) &&
                (m1.R3C1 == m2.R3C1) && (m1.R3C2 == m2.R3C2);
        }

        // Addition Operators //----------------------------------------------//

        /// <summary>
        /// Performs addition of two Matrix44 objects.
        /// </summary>
        public static void Add (
            ref Matrix44 m1, ref Matrix44 m2, out Matrix44 result)
        {
            result.R0C0 = m1.R0C0 + m2.R0C0;
            result.R0C1 = m1.R0C1 + m2.R0C1;
            result.R0C2 = m1.R0C2 + m2.R0C2;
            result.R0C3 = m1.R0C3 + m2.R0C3;
            result.R1C0 = m1.R1C0 + m2.R1C0;
            result.R1C1 = m1.R1C1 + m2.R1C1;
            result.R1C2 = m1.R1C2 + m2.R1C2;
            result.R1C3 = m1.R1C3 + m2.R1C3;
            result.R2C0 = m1.R2C0 + m2.R2C0;
            result.R2C1 = m1.R2C1 + m2.R2C1;
            result.R2C2 = m1.R2C2 + m2.R2C2;
            result.R2C3 = m1.R2C3 + m2.R2C3;
            result.R3C0 = m1.R3C0 + m2.R3C0;
            result.R3C1 = m1.R3C1 + m2.R3C1;
            result.R3C2 = m1.R3C2 + m2.R3C2;
            result.R3C3 = m1.R3C3 + m2.R3C3;
        }

        // Subtraction Operators //-------------------------------------------//

        /// <summary>
        /// Performs subtraction of two Matrix44 objects.
        /// </summary>
        public static void Subtract (
            ref Matrix44 m1, ref Matrix44 m2, out Matrix44 result)
        {
            result.R0C0 = m1.R0C0 - m2.R0C0;
            result.R0C1 = m1.R0C1 - m2.R0C1;
            result.R0C2 = m1.R0C2 - m2.R0C2;
            result.R0C3 = m1.R0C3 - m2.R0C3;
            result.R1C0 = m1.R1C0 - m2.R1C0;
            result.R1C1 = m1.R1C1 - m2.R1C1;
            result.R1C2 = m1.R1C2 - m2.R1C2;
            result.R1C3 = m1.R1C3 - m2.R1C3;
            result.R2C0 = m1.R2C0 - m2.R2C0;
            result.R2C1 = m1.R2C1 - m2.R2C1;
            result.R2C2 = m1.R2C2 - m2.R2C2;
            result.R2C3 = m1.R2C3 - m2.R2C3;
            result.R3C0 = m1.R3C0 - m2.R3C0;
            result.R3C1 = m1.R3C1 - m2.R3C1;
            result.R3C2 = m1.R3C2 - m2.R3C2;
            result.R3C3 = m1.R3C3 - m2.R3C3;
        }

        // Negation Operators //----------------------------------------------//

        /// <summary>
        /// Performs negation of a Matrix44 object.
        /// </summary>
        public static void Negate (ref Matrix44 matrix, out Matrix44 result)
        {
            result.R0C0 = -matrix.R0C0;
            result.R0C1 = -matrix.R0C1;
            result.R0C2 = -matrix.R0C2;
            result.R0C3 = -matrix.R0C3;
            result.R1C0 = -matrix.R1C0;
            result.R1C1 = -matrix.R1C1;
            result.R1C2 = -matrix.R1C2;
            result.R1C3 = -matrix.R1C3;
            result.R2C0 = -matrix.R2C0;
            result.R2C1 = -matrix.R2C1;
            result.R2C2 = -matrix.R2C2;
            result.R2C3 = -matrix.R2C3;
            result.R3C0 = -matrix.R3C0;
            result.R3C1 = -matrix.R3C1;
            result.R3C2 = -matrix.R3C2;
            result.R3C3 = -matrix.R3C3;
        }

        // Multiplication Operators //----------------------------------------//

        /// <summary>
        /// Performs muliplication of two Matrix44 objects.
        /// </summary>
        public static void Multiply (
            ref Matrix44 m1, ref Matrix44 m2, out Matrix44 result)
        {
            result.R0C0 =
                (m1.R0C0 * m2.R0C0) + (m1.R0C1 * m2.R1C0) +
                (m1.R0C2 * m2.R2C0) + (m1.R0C3 * m2.R3C0);

            result.R0C1 =
                (m1.R0C0 * m2.R0C1) + (m1.R0C1 * m2.R1C1) +
                (m1.R0C2 * m2.R2C1) + (m1.R0C3 * m2.R3C1);

            result.R0C2 =
                (m1.R0C0 * m2.R0C2) + (m1.R0C1 * m2.R1C2) +
                (m1.R0C2 * m2.R2C2) + (m1.R0C3 * m2.R3C2);

            result.R0C3 =
                (m1.R0C0 * m2.R0C3) + (m1.R0C1 * m2.R1C3) +
                (m1.R0C2 * m2.R2C3) + (m1.R0C3 * m2.R3C3);

            result.R1C0 =
                (m1.R1C0 * m2.R0C0) + (m1.R1C1 * m2.R1C0) +
                (m1.R1C2 * m2.R2C0) + (m1.R1C3 * m2.R3C0);

            result.R1C1 =
                (m1.R1C0 * m2.R0C1) + (m1.R1C1 * m2.R1C1) +
                (m1.R1C2 * m2.R2C1) + (m1.R1C3 * m2.R3C1);

            result.R1C2 =
                (m1.R1C0 * m2.R0C2) + (m1.R1C1 * m2.R1C2) +
                (m1.R1C2 * m2.R2C2) + (m1.R1C3 * m2.R3C2);

            result.R1C3 =
                (m1.R1C0 * m2.R0C3) + (m1.R1C1 * m2.R1C3) +
                (m1.R1C2 * m2.R2C3) + (m1.R1C3 * m2.R3C3);

            result.R2C0 =
                (m1.R2C0 * m2.R0C0) + (m1.R2C1 * m2.R1C0) +
                (m1.R2C2 * m2.R2C0) + (m1.R2C3 * m2.R3C0);

            result.R2C1 =
                (m1.R2C0 * m2.R0C1) + (m1.R2C1 * m2.R1C1) +
                (m1.R2C2 * m2.R2C1) + (m1.R2C3 * m2.R3C1);

            result.R2C2 =
                (m1.R2C0 * m2.R0C2) + (m1.R2C1 * m2.R1C2) +
                (m1.R2C2 * m2.R2C2) + (m1.R2C3 * m2.R3C2);

            result.R2C3 =
                (m1.R2C0 * m2.R0C3) + (m1.R2C1 * m2.R1C3) +
                (m1.R2C2 * m2.R2C3) + (m1.R2C3 * m2.R3C3);

            result.R3C0 =
                (m1.R3C0 * m2.R0C0) + (m1.R3C1 * m2.R1C0) +
                (m1.R3C2 * m2.R2C0) + (m1.R3C3 * m2.R3C0);

            result.R3C1 =
                (m1.R3C0 * m2.R0C1) + (m1.R3C1 * m2.R1C1) +
                (m1.R3C2 * m2.R2C1) + (m1.R3C3 * m2.R3C1);

            result.R3C2 =
                (m1.R3C0 * m2.R0C2) + (m1.R3C1 * m2.R1C2) +
                (m1.R3C2 * m2.R2C2) + (m1.R3C3 * m2.R3C2);

            result.R3C3 =
                (m1.R3C0 * m2.R0C3) + (m1.R3C1 * m2.R1C3) +
                (m1.R3C2 * m2.R2C3) + (m1.R3C3 * m2.R3C3);
        }

        /// <summary>
        /// Performs multiplication of a Matrix44 object and a Single
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Matrix44 matrix,
            ref Single scaleFactor,
            out Matrix44 result)
        {
            result.R0C0 = matrix.R0C0 * scaleFactor;
            result.R0C1 = matrix.R0C1 * scaleFactor;
            result.R0C2 = matrix.R0C2 * scaleFactor;
            result.R0C3 = matrix.R0C3 * scaleFactor;
            result.R1C0 = matrix.R1C0 * scaleFactor;
            result.R1C1 = matrix.R1C1 * scaleFactor;
            result.R1C2 = matrix.R1C2 * scaleFactor;
            result.R1C3 = matrix.R1C3 * scaleFactor;
            result.R2C0 = matrix.R2C0 * scaleFactor;
            result.R2C1 = matrix.R2C1 * scaleFactor;
            result.R2C2 = matrix.R2C2 * scaleFactor;
            result.R2C3 = matrix.R2C3 * scaleFactor;
            result.R3C0 = matrix.R3C0 * scaleFactor;
            result.R3C1 = matrix.R3C1 * scaleFactor;
            result.R3C2 = matrix.R3C2 * scaleFactor;
            result.R3C3 = matrix.R3C3 * scaleFactor;
        }

        /// <summary>
        /// Doing this might not produce what you expect, perhaps you should
        /// lerp between quaternions.
        /// </summary>
        public static void Lerp (
            ref Matrix44 m1, ref Matrix44 m2, ref Single amount,
            out Matrix44 result)
        {
            Single zero = 0;
            Single one = 1;

            if (amount < zero || amount > one)
                throw new ArgumentOutOfRangeException ();

            result.R0C0 = m1.R0C0 + ((m2.R0C0 - m1.R0C0) * amount);
            result.R0C1 = m1.R0C1 + ((m2.R0C1 - m1.R0C1) * amount);
            result.R0C2 = m1.R0C2 + ((m2.R0C2 - m1.R0C2) * amount);
            result.R0C3 = m1.R0C3 + ((m2.R0C3 - m1.R0C3) * amount);
            result.R1C0 = m1.R1C0 + ((m2.R1C0 - m1.R1C0) * amount);
            result.R1C1 = m1.R1C1 + ((m2.R1C1 - m1.R1C1) * amount);
            result.R1C2 = m1.R1C2 + ((m2.R1C2 - m1.R1C2) * amount);
            result.R1C3 = m1.R1C3 + ((m2.R1C3 - m1.R1C3) * amount);
            result.R2C0 = m1.R2C0 + ((m2.R2C0 - m1.R2C0) * amount);
            result.R2C1 = m1.R2C1 + ((m2.R2C1 - m1.R2C1) * amount);
            result.R2C2 = m1.R2C2 + ((m2.R2C2 - m1.R2C2) * amount);
            result.R2C3 = m1.R2C3 + ((m2.R2C3 - m1.R2C3) * amount);
            result.R3C0 = m1.R3C0 + ((m2.R3C0 - m1.R3C0) * amount);
            result.R3C1 = m1.R3C1 + ((m2.R3C1 - m1.R3C1) * amount);
            result.R3C2 = m1.R3C2 + ((m2.R3C2 - m1.R3C2) * amount);
            result.R3C3 = m1.R3C3 + ((m2.R3C3 - m1.R3C3) * amount);
        }

        /// <summary>
        /// A square matrix whose transpose is equal to itself is called a
        /// symmetric matrix.
        /// </summary>
        public Boolean IsSymmetric ()
        {
            Matrix44 transpose = this;
            Transpose (ref transpose, out transpose);
            return (transpose == this);
        }

        /// <summary>
        /// A square matrix whose transpose is equal to its negative is called
        /// a skew-symmetric matrix.
        /// </summary>
        public Boolean IsSkewSymmetric ()
        {
            Matrix44 transpose = this;
            Transpose (ref transpose, out transpose);
            return (transpose == -this);
        }

#if (VARIANTS_ENABLED)

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateTranslation (Vector3 position)
        {
            Matrix44 result;
            CreateTranslation (ref position, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateTranslation (
            Single xPosition,
            Single yPosition,
            Single zPosition)
        {
            Matrix44 result;
            CreateTranslation (
                ref xPosition, ref yPosition, ref zPosition,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateScale (
            Single xScale,
            Single yScale,
            Single zScale)
        {
            Matrix44 result;
            CreateScale (
                ref xScale, ref yScale, ref zScale,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateScale (Vector3 scales)
        {
            Matrix44 result;
            CreateScale (ref scales, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateScale (Single scale)
        {
            Matrix44 result;
            CreateScale (ref scale, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateRotationX (Single radians)
        {
            Matrix44 result;
            CreateRotationX (ref radians, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateRotationY (Single radians)
        {
            Matrix44 result;
            CreateRotationY (ref radians, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateRotationZ (Single radians)
        {
            Matrix44 result;
            CreateRotationZ (ref radians, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateFromAxisAngle (
            Vector3 axis,
            Single angle)
        {
            Matrix44 result;
            CreateFromAxisAngle (ref axis, ref angle, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateFromCartesianAxes (
            Vector3 right,
            Vector3 up,
            Vector3 backward)
        {
            Matrix44 result;
            CreateFromCartesianAxes (
                ref right, ref up, ref backward, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateWorld (
            Vector3 position,
            Vector3 forward,
            Vector3 up)
        {
            Matrix44 result;
            CreateWorld (ref position, ref forward, ref up, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateFromQuaternion (Quaternion quaternion)
        {
            Matrix44 result;
            CreateFromQuaternion (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateFromYawPitchRoll (
            Single yaw,
            Single pitch,
            Single roll)
        {
            Matrix44 result;
            CreateFromYawPitchRoll (ref yaw, ref pitch, ref roll, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateBillboard (
            Vector3 objectPosition,
            Vector3 cameraPosition,
            Vector3 cameraUpVector,
            Vector3? cameraForwardVector)
        {
            Matrix44 result;
            CreateBillboard (
                ref objectPosition, ref cameraPosition,
                ref cameraUpVector, ref cameraForwardVector,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateConstrainedBillboard (
            Vector3 objectPosition,
            Vector3 cameraPosition,
            Vector3 rotateAxis,
            Vector3? cameraForwardVector,
            Vector3? objectForwardVector)
        {
            Matrix44 result;
            CreateConstrainedBillboard (
                ref objectPosition, ref cameraPosition,
                ref rotateAxis, ref cameraForwardVector, ref objectForwardVector,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreatePerspectiveFieldOfView (
            Single fieldOfView,
            Single aspectRatio,
            Single nearPlane,
            Single farPlane)
        {
            Matrix44 result;
            CreatePerspectiveFieldOfView (
                ref fieldOfView, ref aspectRatio, ref nearPlane, ref farPlane,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreatePerspective (
            Single width,
            Single height,
            Single nearPlane,
            Single farPlane)
        {
            Matrix44 result;
            CreatePerspective (
                ref width, ref height, ref nearPlane, ref farPlane,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreatePerspectiveOffCenter (
            Single left,
            Single right,
            Single bottom,
            Single top,
            Single nearPlane,
            Single farPlane)
        {
            Matrix44 result;
            CreatePerspectiveOffCenter (
                ref left, ref right, ref bottom,
                ref top, ref nearPlane, ref farPlane,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateOrthographic (
            Single width,
            Single height,
            Single nearPlane,
            Single farPlane)
        {
            Matrix44 result;
            CreateOrthographic (
                ref width, ref height, ref nearPlane, ref farPlane,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateOrthographicOffCenter (
            Single left,
            Single right,
            Single bottom,
            Single top,
            Single nearPlane,
            Single farPlane)
        {
            Matrix44 result;
            CreateOrthographicOffCenter (
                ref left, ref right, ref bottom,
                ref top, ref nearPlane, ref farPlane,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateLookAt (
            Vector3 cameraPosition,
            Vector3 cameraTarget,
            Vector3 cameraUpVector)
        {
            Matrix44 result;
            CreateLookAt (
                ref cameraPosition, ref cameraTarget, ref cameraUpVector,
                out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Transpose (Matrix44 input)
        {
            Matrix44 result;
            Transpose (ref input, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Single Determinant (Matrix44 matrix)
        {
            Single result;
            Determinant (ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Invert (Matrix44 matrix)
        {
            Matrix44 result;
            Invert (ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Transform (Matrix44 value, Quaternion rotation)
        {
            Matrix44 result;
            Transform (ref value, ref rotation, out result);
            return result;
        }

        // Equality Operators //----------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean Equals (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Boolean result;
            Equals (ref matrix1, ref matrix2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator == (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Boolean result;
            Equals (ref matrix1, ref matrix2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Boolean operator != (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Boolean result;
            Equals (ref matrix1, ref matrix2, out result);
            return !result;
        }

        // Variant Addition Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Add (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            Add (ref matrix1, ref matrix2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator + (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            Add (ref matrix1, ref matrix2, out result);
            return result;
        }

        // Variant Subtraction Operators //-----------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Subtract (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            Subtract (ref matrix1, ref matrix2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator - (Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            Subtract (ref matrix1, ref matrix2, out result);
            return result;
        }

        // Variant Negation Operators //--------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Negate (Matrix44 matrix)
        {
            Matrix44 result;
            Negate (ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator - (Matrix44 matrix)
        {
            Matrix44 result;
            Negate (ref matrix, out result);
            return result;
        }

        // Variant Multiplication Operators //--------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Multiply (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            Multiply (ref matrix1, ref matrix2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Multiply (
            Matrix44 matrix, Single scaleFactor)
        {
            Matrix44 result;
            Multiply (ref matrix, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator * (
            Matrix44 matrix1, Matrix44 matrix2)
        {
            Matrix44 result;
            Multiply (ref matrix1, ref matrix2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator * (
            Matrix44 matrix, Single scaleFactor)
        {
            Matrix44 result;
            Multiply (ref matrix, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator * (
            Single scaleFactor, Matrix44 matrix)
        {
            Matrix44 result;
            Multiply (ref matrix, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 Lerp (
            Matrix44 matrix1,
            Matrix44 matrix2,
            Single amount)
        {
            Matrix44 result;
            Lerp (ref matrix1, ref matrix2, ref amount, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public void Transpose ()
        {
            Transpose (ref this, out this);
        }

#endif
    }

    internal static class Int32Extensions
    {
        // http://msdn.microsoft.com/en-us/library/system.object.gethashcode(v=vs.110).aspx
        public static Int32 ShiftAndWrap (
            this Int32 value, Int32 positions = 2)
        {
            positions = positions & 0x1F;

            // Save the existing bit pattern, but interpret it as an unsigned
            // integer.
            uint number = BitConverter.ToUInt32(
                BitConverter.GetBytes(value), 0);
            // Preserve the bits to be discarded.
            uint wrapped = number >> (32 - positions);
            // Shift and wrap the discarded bits.
            return BitConverter.ToInt32 (
                BitConverter.GetBytes ((number << positions) | wrapped), 0);
        }
    }

    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a random Single between 0.0 & 1.0
        /// </summary>
        public static Single NextSingle (this System.Random r)
        {
            return (Single) r.NextDouble();
        }
    }
}
