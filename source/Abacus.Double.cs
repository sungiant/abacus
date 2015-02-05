﻿// ┌────────────────────────────────────────────────────────────────────────┐ \\
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
// │ Copyright © 2013 A.J.Pook (http://www.blimey3d.com)                    │ \\
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

namespace Abacus.DoublePrecision
{
    internal static class Int32Extensions
    {
        // http://msdn.microsoft.com/en-us/library/system.object.gethashcode(v=vs.110).aspx
        public static Int32 ShiftAndWrap (this Int32 value, Int32 positions = 2)
        {
            positions = positions & 0x1F;
    
            // Save the existing bit pattern, but interpret it as an unsigned integer. 
            uint number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
            // Preserve the bits to be discarded. 
            uint wrapped = number >> (32 - positions);
            // Shift and wrap the discarded bits. 
            return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
        }
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
        public static void E (out Double value)
        {
            value = 2.71828182845904523536028747135266249775724709369995;
        }

        /// <summary>
        /// Provides the constant Epsilon.
        /// </summary>
        public static void Epsilon (out Double value)
        {
            value = 1.0e-6;
        }

        /// <summary>
        /// Provides the constant Half.
        /// </summary>
        public static void Half (out Double value)
        {
            value = 0.5;
        }

        /// <summary>
        /// Provides the constant Log10E.
        /// </summary>
        public static void Log10E (out Double value)
        {
            value = 0.4342944821;
        }

        /// <summary>
        /// Provides the constant Log2E.
        /// </summary>
        public static void Log2E (out Double value)
        {
            value = 1.442695;
        }

        /// <summary>
        /// Provides the constant Pi.
        /// </summary>
        public static void Pi (out Double value)
        {
            value = 3.14159265358979323846264338327950288;
        }

        /// <summary>
        /// Provides the constant Root2.
        /// </summary>
        public static void Root2 (out Double value)
        {
            value = 1.414213562;
        }

        /// <summary>
        /// Provides the constant Root3.
        /// </summary>
        public static void Root3 (out Double value)
        {
            value = 1.732050808;
        }

        /// <summary>
        /// Provides the constant Tau.
        /// </summary>
        public static void Tau (out Double value)
        {
            value = 6.283185;
        }

        /// <summary>
        /// Provides the constant Zero.
        /// </summary>
        public static void Zero (out Double value)
        {
            value = 0;
        }

        /// <summary>
        /// Provides the constant One.
        /// </summary>
        public static void One (out Double value)
        {
            value = 1;
        }


        /// <summary>
        /// ArcCos.
        /// </summary>
        public static Double ArcCos (Double value)
        {
            return Math.Acos(value);
        }

        /// <summary>
        /// ArcSin.
        /// </summary>
        public static Double ArcSin (Double value)
        {
            return Math.Asin(value);
        }

        /// <summary>
        /// ArcTan.
        /// </summary>
        public static Double ArcTan (Double value)
        {
            return Math.Atan(value);
        }

        /// <summary>
        /// Cos.
        /// </summary>
        public static Double Cos (Double value)
        {
            return Math.Cos(value);
        }

        /// <summary>
        /// Sin.
        /// </summary>
        public static Double Sin (Double value)
        {
            return Math.Sin(value);
        }

        /// <summary>
        /// Tan.
        /// </summary>
        public static Double Tan (Double value)
        {
            return Math.Tan(value);
        }

        /// <summary>
        /// Sqrt.
        /// </summary>
        public static Double Sqrt (Double value)
        {
            return Math.Sqrt(value);
        }

        /// <summary>
        /// Square.
        /// </summary>
        public static Double Square (Double value)
        {
            return value * value;
        }

        /// <summary>
        /// Abs.
        /// </summary>
        public static Double Abs (Double value)
        {
            return Math.Abs(value);
        }


        /// <summary>
        /// ToRadians
        /// </summary>
        public static Double ToRadians(Double input)
        {
            Double tau; Tau(out tau);
            return input * tau / ((Double)360);
        }

        /// <summary>
        /// ToDegrees
        /// </summary>
        public static Double ToDegrees(Double input)
        {
            Double tau; Tau(out tau);
            return input / tau * ((Double)360);
        }

        /// <summary>
        /// FromFraction
        /// </summary>
        public static void FromFraction(Int32 numerator, Int32 denominator, out Double value)
        {
            value = (Double) numerator / (Double) denominator;
        }

        /// <summary>
        /// FromString
        /// </summary>
        public static void FromString(String str, out Double value)
        {
            Double.TryParse(str, out value);
        }

        /// <summary>
        /// IsZero
        /// </summary>
        public static Boolean IsZero(Double value)
        {
            Double ep;
            Epsilon(out ep);
            return Abs(value) < ep;
        }

        /// <summary>
        /// Min
        /// </summary>
        public static Double Min(Double a, Double b)
        {
            return a < b ? a : b;
        }

        /// <summary>
        /// Max
        /// </summary>
        public static Double Max(Double a, Double b)
        {
            return a > b ? a : b;
        }

        /// <summary>
        /// WithinEpsilon
        /// </summary>
        public static Boolean WithinEpsilon(Double a, Double b)
        {
            Double num = a - b;
            return ((-Double.Epsilon <= num) && (num <= Double.Epsilon));
        }

        /// <summary>
        /// Sign
        /// </summary>
        public static Int32 Sign(Double value)
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
    /// Double precision Vector2.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector2
        : IEquatable<Vector2>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector2.
        /// </summary>
        public Double X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector2.
        /// </summary>
        public Double Y;

        /// <summary>
        /// Initilises a new instance of Vector2 from two Double values
        /// representing X and Y respectively.
        /// </summary>
        public Vector2 (Double x, Double y)
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
            ref Vector2 vector1, ref Vector2 vector2, out Double result)
        {
            Double dx = vector1.X - vector2.X;
            Double dy = vector1.Y - vector2.Y;

            Double lengthSquared = (dx * dx) + (dy * dy);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector2 vector1, ref Vector2 vector2, out Double result)
        {
            Double dx = vector1.X - vector2.X;
            Double dy = vector1.Y - vector2.Y;

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
            ref Vector2 vector1, ref Vector2 vector2, out Double result)
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
            Double lengthSquared =
                (vector.X * vector.X) + (vector.Y * vector.Y);

            Double epsilon; Maths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Double.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double one = 1;
            Double multiplier = one / Maths.Sqrt (lengthSquared);

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
            Double dot;
            Dot(ref vector, ref normal, out dot);

            Double two = 2;
            Double twoDot = dot * two;

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
            Double x =
                (vector.X * matrix.R0C0) +
                (vector.Y * matrix.R1C0) +
                matrix.R3C0;

            Double y =
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
            Double two = 2;

            Double i = rotation.I;
            Double j = rotation.J;
            Double k = rotation.K;
            Double u = rotation.U;

            Double ii = i * i;
            Double jj = j * j;
            Double kk = k * k;

            Double uk = u * k;
            Double ij = i * j;

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

            Double x = (normal.X * matrix.R0C0) + (normal.Y * matrix.R1C0);
            Double y = (normal.X * matrix.R0C1) + (normal.Y * matrix.R1C1);

            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Calculates the length of the Vector2.
        /// </summary>
        public static void Length (
            ref Vector2 vector, out Double result)
        {
            Double lengthSquared =
                (vector.X * vector.X) + (vector.Y * vector.Y);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the length of the Vector2 squared.
        /// </summary>
        public static void LengthSquared (
            ref Vector2 vector, out Double result)
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
        /// Performs multiplication of a Vector2 object and a Double
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Vector2 vector, ref Double scaleFactor, out Vector2 result)
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
        /// Performs division of a Vector2 object and a Double precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Vector2 vector1, ref Double divider, out Vector2 result)
        {
            Double one = 1;
            Double num = one / divider;
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
            ref Double amount,
            out Vector2 result)
        {
            Double zero = 0;
            Double one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double two = 2;
            Double three = 3;

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
            ref Double amount,
            out Vector2 result)
        {
            Double zero = 0;
            Double one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double two = 2;
            Double three = 3;
            Double four = 4;
            Double five = 5;
            Double half; Maths.Half(out half);

            Double squared = amount * amount;
            Double cubed = amount * squared;

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
            ref Double amount,
            out Vector2 result)
        {
            Double zero = 0;
            Double one = 1;

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

            Double two = 2;
            Double three = 3;

            Double squared = amount * amount;
            Double cubed = amount * squared;

            Double a = ((two * cubed) - (three * squared)) + one;
            Double b = (-two * cubed) + (three * squared);
            Double c = (cubed - (two * squared)) + amount;
            Double d = cubed - squared;

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
            Double x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;

            Double y = a.Y;
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
            ref Double amount,
            out Vector2 result)
        {
            Double zero = 0;
            Double one = 1;
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
            Double one = 1;
            result = Maths.IsZero(
                one - vector.X * vector.X - vector.Y * vector.Y);
        }


#if (VARIANTS_ENABLED)

        // Variant Maths //---------------------------------------------------//

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Double Distance (
            Vector2 vector1, Vector2 vector2)
        {
            Double result;
            Distance (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Double DistanceSquared (
            Vector2 vector1, Vector2 vector2)
        {
            Double result;
            DistanceSquared (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Double Dot (
            Vector2 vector1, Vector2 vector2)
        {
            Double result;
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
        public static Double Length (Vector2 vector)
        {
            Double result;
            Length (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Double LengthSquared (Vector2 vector)
        {
            Double result;
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
            Vector2 vector, Double scaleFactor)
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
            Vector2 vector, Double scaleFactor)
        {
            Vector2 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector2 operator * (
            Double scaleFactor, Vector2 vector)
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
            Vector2 vector1, Double divider)
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
        public static Vector2 operator / (Vector2 vector1, Double divider)
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
            Double amount)
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
            Double amount)
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
            Double amount)
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
            Double amount)
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
        public Double Length ()
        {
            Double result;
            Length (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Double LengthSquared ()
        {
            Double result;
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
    /// Double precision Vector3.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector3
        : IEquatable<Vector3>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector3.
        /// </summary>
        public Double X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector3.
        /// </summary>
        public Double Y;

        /// <summary>
        /// Gets or sets the Z-component of the Vector3.
        /// </summary>
        public Double Z;

        /// <summary>
        /// Initilises a new instance of Vector3 from three Double values
        /// representing X, Y and Z respectively.
        /// </summary>
        public Vector3 (Double x, Double y, Double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Initilises a new instance of Vector3 from one Vector2 value
        /// representing X and Y and one Double value representing Z.
        /// </summary>
        public Vector3 (Vector2 value, Double z)
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
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Double result)
        {
            Double dx = vector1.X - vector2.X;
            Double dy = vector1.Y - vector2.Y;
            Double dz = vector1.Z - vector2.Z;

            Double lengthSquared = (dx * dx) + (dy * dy) + (dz * dz);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Double result)
        {
            Double dx = vector1.X - vector2.X;
            Double dy = vector1.Y - vector2.Y;
            Double dz = vector1.Z - vector2.Z;

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
            ref Vector3 vector1,
            ref Vector3 vector2,
            out Double result)
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
            Double lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z);

            Double epsilon; Maths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Double.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double one = 1;
            Double multiplier = one / Maths.Sqrt (lengthSquared);

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
        }

        /// <summary>
        /// Calculates the cross product of two vectors.
        /// </summary>
        public static void Cross (
            ref Vector3 vector1,
            ref Vector3 vector2,
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
            ref Vector3 vector,
            ref Vector3 normal,
            out Vector3 result)
        {
            Boolean normalIsUnit;
            Vector3.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double two = 2;

            Double num =
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
            ref Vector3 vector,
            ref Matrix44 matrix,
            out Vector3 result)
        {
            Double x =
                (vector.X * matrix.R0C0) +
                (vector.Y * matrix.R1C0) +
                (vector.Z * matrix.R2C0) + matrix.R3C0;

            Double y =
                (vector.X * matrix.R0C1) +
                (vector.Y * matrix.R1C1) +
                (vector.Z * matrix.R2C1) + matrix.R3C1;

            Double z =
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
            ref Vector3 vector,
            ref Quaternion rotation,
            out Vector3 result)
        {
            Double two = 2;

            Double i = rotation.I;
            Double j = rotation.J;
            Double k = rotation.K;
            Double u = rotation.U;

            Double ii = i * i;
            Double jj = j * j;
            Double kk = k * k;

            Double ui = u * i;
            Double uj = u * j;
            Double uk = u * k;
            Double ij = i * j;
            Double ik = i * k;
            Double jk = j * k;

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
            ref Vector3 normal,
            ref Matrix44 matrix,
            out Vector3 result)
        {
            Boolean normalIsUnit;
            Vector3.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Double x =
                (normal.X * matrix.R0C0) +
                (normal.Y * matrix.R1C0) +
                (normal.Z * matrix.R2C0);

            Double y =
                (normal.X * matrix.R0C1) +
                (normal.Y * matrix.R1C1) +
                (normal.Z * matrix.R2C1);

            Double z =
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
            ref Vector3 vector, out Double result)
        {
            Double lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the length of the Vector3 squared.
        /// </summary>
        public static void LengthSquared (
            ref Vector3 vector, out Double result)
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
        /// Performs multiplication of a Vector3 object and a Double
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Vector3 value1, ref Double scaleFactor, out Vector3 result)
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
        /// Performs division of a Vector3 object and a Double precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Vector3 value1, ref Double vector2, out Vector3 result)
        {
            Double one = 1;
            Double num = one / vector2;
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
            ref Double amount,
            out Vector3 result)
        {
            Double zero = 0;
            Double one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double two = 2;
            Double three = 3;

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
            ref Double amount,
            out Vector3 result)
        {
            Double zero = 0;
            Double one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double two = 2;
            Double three = 3;
            Double four = 4;
            Double five = 5;
            Double half; Maths.Half(out half);

            Double squared = amount * amount;
            Double cubed = amount * squared;

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
            ref Double amount,
            out Vector3 result)
        {
            Double zero = 0;
            Double one = 1;

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

            Double two = 2;
            Double three = 3;

            Double squared = amount * amount;
            Double cubed = amount * squared;

            Double a = ((two * cubed) - (three * squared)) + one;
            Double b = (-two * cubed) + (three * squared);
            Double c = (cubed - (two * squared)) + amount;
            Double d = cubed - squared;

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
            Double x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            Double y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            Double z = a.Z;
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
            ref Double amount,
            out Vector3 result)
        {
            Double zero = 0;
            Double one = 1;
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
            Double one = 1;
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
        public static Double Distance (
            Vector3 vector1, Vector3 vector2)
        {
            Double result;
            Distance (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Double DistanceSquared (
            Vector3 vector1, Vector3 vector2)
        {
            Double result;
            DistanceSquared (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Double Dot (
            Vector3 vector1, Vector3 vector2)
        {
            Double result;
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
        public static Double Length (Vector3 vector)
        {
            Double result;
            Length (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Double LengthSquared (Vector3 vector)
        {
            Double result;
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
            Vector3 vector, Double scaleFactor)
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
            Vector3 vector, Double scaleFactor)
        {
            Vector3 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector3 operator * (
            Double scaleFactor, Vector3 vector)
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
            Vector3 vector1, Double divider)
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
        public static Vector3 operator / (Vector3 vector1, Double divider)
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
            Double amount)
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
            Double amount)
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
            Double amount)
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
            Double amount)
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
        public Double Length ()
        {
            Double result;
            Length (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Double LengthSquared ()
        {
            Double result;
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
    /// Double precision Vector4.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Vector4
        : IEquatable<Vector4>
    {
        /// <summary>
        /// Gets or sets the X-component of the Vector4.
        /// </summary>
        public Double X;

        /// <summary>
        /// Gets or sets the Y-component of the Vector4.
        /// </summary>
        public Double Y;

        /// <summary>
        /// Gets or sets the Z-component of the Vector4.
        /// </summary>
        public Double Z;

        /// <summary>
        /// Gets or sets the W-component of the Vector4.
        /// </summary>
        public Double W;

        /// <summary>
        /// Initilises a new instance of Vector4 from four Double values
        /// representing X, Y, Z and W respectively.
        /// </summary>
        public Vector4 (
            Double x,
            Double y,
            Double z,
            Double w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initilises a new instance of Vector4 from one Vector2 value
        /// representing X and Y and two Double values representing Z and
        /// W respectively.
        /// </summary>
        public Vector4 (Vector2 value, Double z, Double w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initilises a new instance of Vector4 from one Vector3 value
        /// representing X, Y and Z and one Double value representing W.
        /// </summary>
        public Vector4 (Vector3 value, Double w)
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
            ref Vector4 vector1,
            ref Vector4 vector2,
            out Double result)
        {
            Double dx = vector1.X - vector2.X;
            Double dy = vector1.Y - vector2.Y;
            Double dz = vector1.Z - vector2.Z;
            Double dw = vector1.W - vector2.W;

            Double lengthSquared =
                (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);

            result = Maths.Sqrt (lengthSquared);
        }

        /// <summary>
        /// Calculates the distance between two vectors squared.
        /// </summary>
        public static void DistanceSquared (
            ref Vector4 vector1,
            ref Vector4 vector2,
            out Double result)
        {
            Double dx = vector1.X - vector2.X;
            Double dy = vector1.Y - vector2.Y;
            Double dz = vector1.Z - vector2.Z;
            Double dw = vector1.W - vector2.W;

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
            ref Vector4 vector1,
            ref Vector4 vector2,
            out Double result)
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
            ref Vector4 vector,
            out Vector4 result)
        {
            Double lengthSquared =
                (vector.X * vector.X) +
                (vector.Y * vector.Y) +
                (vector.Z * vector.Z) +
                (vector.W * vector.W);

            Double epsilon; Maths.Epsilon(out epsilon);

            if( lengthSquared <= epsilon ||
                Double.IsInfinity(lengthSquared) )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double one = 1;
            Double multiplier = one / (Maths.Sqrt (lengthSquared));

            result.X = vector.X * multiplier;
            result.Y = vector.Y * multiplier;
            result.Z = vector.Z * multiplier;
            result.W = vector.W * multiplier;
        }

        /// <summary>
        /// Transforms a Vector4 by the specified Matrix44.
        /// </summary>
        public static void Transform (
            ref Vector4 vector,
            ref Matrix44 matrix,
            out Vector4 result)
        {
            Double x =
                (vector.X * matrix.R0C0) +
                (vector.Y * matrix.R1C0) +
                (vector.Z * matrix.R2C0) +
                (vector.W * matrix.R3C0);

            Double y =
                (vector.X * matrix.R0C1) +
                (vector.Y * matrix.R1C1) +
                (vector.Z * matrix.R2C1) +
                (vector.W * matrix.R3C1);

            Double z =
                (vector.X * matrix.R0C2) +
                (vector.Y * matrix.R1C2) +
                (vector.Z * matrix.R2C2) +
                (vector.W * matrix.R3C2);

            Double w =
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
            ref Vector4 vector,
            ref Quaternion rotation,
            out Vector4 result)
        {
            Double two = 2;

            Double i = rotation.I;
            Double j = rotation.J;
            Double k = rotation.K;
            Double u = rotation.U;

            Double ii = i * i;
            Double jj = j * j;
            Double kk = k * k;

            Double ui = u * i;
            Double uj = u * j;
            Double uk = u * k;
            Double ij = i * j;
            Double ik = i * k;
            Double jk = j * k;

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
            ref Vector4 normal,
            ref Matrix44 matrix,
            out Vector4 result)
        {
            Boolean normalIsUnit;
            Vector4.IsUnit (ref normal, out normalIsUnit);
            if( !normalIsUnit )
            {
                throw new ArgumentOutOfRangeException(
                    "The normal vector: " + normal + " must be normalised.");
            }

            Double x =
                (normal.X * matrix.R0C0) + (normal.Y * matrix.R1C0) +
                (normal.Z * matrix.R2C0) + (normal.W * matrix.R3C0);

            Double y =
                (normal.X * matrix.R0C1) + (normal.Y * matrix.R1C1) +
                (normal.Z * matrix.R2C1) + (normal.W * matrix.R3C1);

            Double z =
                (normal.X * matrix.R0C2) + (normal.Y * matrix.R1C2) +
                (normal.Z * matrix.R2C2) + (normal.W * matrix.R3C2);

            Double w =
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
        public static void Length (ref Vector4 vector, out Double result)
        {
            Double lengthSquared =
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
            ref Vector4 vector, out Double result)
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
        /// Performs multiplication of a Vector4 object and a Double
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Vector4 value1, ref Double scaleFactor, out Vector4 result)
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
        /// Performs division of a Vector4 object and a Double precision
        /// scaling factor.
        /// </summary>
        public static void Divide (
            ref Vector4 value1, ref Double divider, out Vector4 result)
        {
            Double one = 1;
            Double num = one / divider;
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
            ref Double amount,
            out Vector4 result)
        {
            Double zero = 0;
            Double one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double two = 2;
            Double three = 3;

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
            ref Double amount,
            out Vector4 result)
        {
            Double zero = 0;
            Double one = 1;

            // Make sure that the weighting vector is within the supported
            // range.
            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double two = 2;
            Double three = 3;
            Double four = 4;
            Double five = 5;
            Double half; Maths.Half(out half);

            Double squared = amount * amount;
            Double cubed = amount * squared;

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
            ref Double amount,
            out Vector4 result)
        {
            Double zero = 0;
            Double one = 1;

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

            Double two = 2;
            Double three = 3;

            Double squared = amount * amount;
            Double cubed = amount * squared;

            Double a = ((two * cubed) - (three * squared)) + one;
            Double b = (-two * cubed) + (three * squared);
            Double c = (cubed - (two * squared)) + amount;
            Double d = cubed - squared;

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
            Double x = a.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            Double y = a.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            Double z = a.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            Double w = a.W;
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
            ref Double amount,
            out Vector4 result)
        {
            Double zero = 0;
            Double one = 1;
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
            Double one = 1;
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
        public static Double Distance (
            Vector4 vector1, Vector4 vector2)
        {
            Double result;
            Distance (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Double DistanceSquared (
            Vector4 vector1, Vector4 vector2)
        {
            Double result;
            DistanceSquared (ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Double Dot (
            Vector4 vector1, Vector4 vector2)
        {
            Double result;
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
        public static Double Length (Vector4 vector)
        {
            Double result;
            Length (ref vector, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Double LengthSquared (Vector4 vector)
        {
            Double result;
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
            Vector4 vector, Double scaleFactor)
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
            Vector4 vector, Double scaleFactor)
        {
            Vector4 result;
            Multiply (ref vector, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Vector4 operator * (
            Double scaleFactor, Vector4 vector)
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
            Vector4 vector1, Double divider)
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
        public static Vector4 operator / (Vector4 vector1, Double divider)
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
            Double amount)
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
            Double amount)
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
            Double amount)
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
            Double amount)
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
        public Double Length ()
        {
            Double result;
            Length (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Double LengthSquared ()
        {
            Double result;
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
    /// Double precision Quaternion.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Quaternion
        : IEquatable<Quaternion>
    {
        /// <summary>
        /// Gets or sets the imaginary I-component of the Quaternion.
        /// </summary>
        public Double I;

        /// <summary>
        /// Gets or sets the imaginary J-component of the Quaternion.
        /// </summary>
        public Double J;

        /// <summary>
        /// Gets or sets the imaginary K-component of the Quaternion.
        /// </summary>
        public Double K;

        /// <summary>
        /// Gets or sets the real U-component of the Quaternion.
        /// </summary>
        public Double U;

        /// <summary>
        /// Initilises a new instance of Quaternion from three imaginary
        /// Double values and one real Double value representing
        /// I, J, K and U respectively.
        /// </summary>
        public Quaternion (
            Double i, Double j, Double k, Double u)
        {
            this.I = i;
            this.J = j;
            this.K = k;
            this.U = u;
        }

        /// <summary>
        /// Initilises a new instance of Quaternion from a Vector3 representing
        /// the imaginary parts of the quaternion (I, J & K) and one
        /// Double value representing the real part of the
        /// Quaternion (U).
        /// </summary>
        public Quaternion (Vector3 vectorPart, Double scalarPart)
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
            ref Vector3 axis, ref Double angle, out Quaternion result)
        {
            Double half; Maths.Half(out half);
            Double theta = angle * half;

            Double sin = Maths.Sin (theta);
            Double cos = Maths.Cos (theta);

            result.I = axis.X * sin;
            result.J = axis.Y * sin;
            result.K = axis.Z * sin;

            result.U = cos;
        }

        /// <summary>
        /// Creates a new Quaternion from specified yaw, pitch, and roll angles.
        /// </summary>
        public static void CreateFromYawPitchRoll (
            ref Double yaw, ref Double pitch,
            ref Double roll, out Quaternion result)
        {
            Double half; Maths.Half(out half);

            Double hr = roll * half;
            Double hp = pitch * half;
            Double hy = yaw * half;

            Double shr = Maths.Sin (hr);
            Double chr = Maths.Cos (hr);
            Double shp = Maths.Sin (hp);
            Double chp = Maths.Cos (hp);
            Double shy = Maths.Sin (hy);
            Double chy = Maths.Cos (hy);

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
            Double zero = 0;
            Double half; Maths.Half(out half);
            Double one = 1;

            Double tr = (m.R0C0 + m.R1C1) + m.R2C2;

            if (tr > zero)
            {
                Double s = Maths.Sqrt (tr + one);
                result.U = s * half;
                s = half / s;
                result.I = (m.R1C2 - m.R2C1) * s;
                result.J = (m.R2C0 - m.R0C2) * s;
                result.K = (m.R0C1 - m.R1C0) * s;
            }
            else if ((m.R0C0 >= m.R1C1) && (m.R0C0 >= m.R2C2))
            {
                Double s7 = Maths.Sqrt (((one + m.R0C0) - m.R1C1) - m.R2C2);
                Double s4 = half / s7;

                result.U = (m.R1C2 - m.R2C1) * s4;
                result.I = half * s7;
                result.J = (m.R0C1 + m.R1C0) * s4;
                result.K = (m.R0C2 + m.R2C0) * s4;
            }
            else if (m.R1C1 > m.R2C2)
            {
                Double s6 =Maths.Sqrt (((one + m.R1C1) - m.R0C0) - m.R2C2);
                Double s3 = half / s6;

                result.U = (m.R2C0 - m.R0C2) * s3;
                result.I = (m.R1C0 + m.R0C1) * s3;
                result.J = half * s6;
                result.K = (m.R2C1 + m.R1C2) * s3;
            }
            else
            {
                Double s5 = Maths.Sqrt (((one + m.R2C2) - m.R0C0) - m.R1C1);
                Double s2 = half / s5;

                result.U = (m.R0C1 - m.R1C0) * s2;
                result.I = (m.R2C0 + m.R0C2) * s2;
                result.J = (m.R2C1 + m.R1C2) * s2;
                result.K = half * s5;
            }
        }
        /// <summary>
        /// Calculates the length² of a Quaternion.
        /// </summary>
        public static void LengthSquared (
            ref Quaternion quaternion, out Double result)
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
            ref Quaternion quaternion, out Double result)
        {
            Double lengthSquared =
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
        /// todo
        /// </summary>
        public static void Inverse (
            ref Quaternion quaternion, out Quaternion result)
        {
            Double one = 1;
            Double a =
                (quaternion.I * quaternion.I) +
                (quaternion.J * quaternion.J) +
                (quaternion.K * quaternion.K) +
                (quaternion.U * quaternion.U);

            Double b = one / a;

            result.I = -quaternion.I * b;
            result.J = -quaternion.J * b;
            result.K = -quaternion.K * b;
            result.U =  quaternion.U * b;
        }

        /// <summary>
        /// Calculates the dot product of two Quaternions.
        /// </summary>
        public static void Dot (
            ref Quaternion q1, ref Quaternion q2, out Double result)
        {
            result =
                (q1.I * q2.I) +
                (q1.J * q2.J) +
                (q1.K * q2.K) +
                (q1.U * q2.U);
        }

        /// <summary>
        /// Concatenates two Quaternions; the result represents the first
        /// rotation followed by the second rotation.
        /// </summary>
        public static void Concatenate (
            ref Quaternion q1, ref Quaternion q2, out Quaternion result)
        {
            Double i1 = q1.I;
            Double j1 = q1.J;
            Double k1 = q1.K;
            Double u1 = q1.U;

            Double i2 = q2.I;
            Double j2 = q2.J;
            Double k2 = q2.K;
            Double u2 = q2.U;

            Double a = (j2 * k1) - (k2 * j1);
            Double b = (k2 * i1) - (i2 * k1);
            Double c = (i2 * j1) - (j2 * i1);
            Double d = (i2 * i1) - (j2 * j1);

            result.I = (i2 * u1) + (i1 * u2) + a;
            result.J = (j2 * u1) + (j1 * u2) + b;
            result.K = (k2 * u1) + (k1 * u2) + c;
            result.U = (u2 * u1) - (k2 * k1) - d;
        }

        /// <summary>
        /// Divides each component of the quaternion by the length of the
        /// quaternion.
        /// </summary>
        public static void Normalise (
            ref Quaternion quaternion, out Quaternion result)
        {
            Double one = 1;

            Double a =
                (quaternion.I * quaternion.I) +
                (quaternion.J * quaternion.J) +
                (quaternion.K * quaternion.K) +
                (quaternion.U * quaternion.U);

            Double b = one / Maths.Sqrt (a);

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
            ref Double amount,
            out Quaternion result)
        {
            Double zero = 0;
            Double one = 1;
            Double epsilon; Maths.Epsilon (out epsilon);

            if( amount < zero || amount > one )
            {
                throw new ArgumentOutOfRangeException();
            }

            Double remaining = one - amount;

            Double angle;
            Dot (ref quaternion1, ref quaternion2, out angle);

            if (angle < zero)
            {
                Negate (ref quaternion1, out quaternion1);
                angle = -angle;
            }

            Double theta = Maths.ArcCos (angle);


            Double r = remaining;
            Double a = amount;

            // To avoid division by 0 and by very small numbers the
            // Lerp is used when theta is small.
            if (theta > epsilon)
            {
                Double x = Maths.Sin (remaining * theta);
                Double y = Maths.Sin (amount * theta);
                Double z = Maths.Sin (theta);

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
            ref Double amount,
            out Quaternion result)
        {
            Double zero = 0;
            Double one = 1;

            if (amount < zero || amount > one)
            {
                throw new ArgumentOutOfRangeException();
            }

            Double remaining = one - amount;

            Double r = remaining;
            Double a = amount;

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
            Double one = 1;

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
            Double angle)
        {
            Quaternion result;
            CreateFromAxisAngle (ref axis, ref angle, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Quaternion CreateFromYawPitchRoll (
            Double yaw,
            Double pitch,
            Double roll)
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
        public static Double LengthSquared (Quaternion quaternion)
        {
            Double result;
            LengthSquared (ref quaternion, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Double Length (Quaternion quaternion)
        {
            Double result;
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
        public static Double Dot (
            Quaternion quaternion1,
            Quaternion quaternion2)
        {
            Double result;
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
            Double amount)
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
            Double amount)
        {
            Quaternion result;
            Lerp (ref quaternion1, ref quaternion2, ref amount, out result);
            return result;
        }


        /// <summary>
        /// Variant function.
        /// </summary>
        public Double LengthSquared ()
        {
            Double result;
            LengthSquared (ref this, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public Double Length ()
        {
            Double result;
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
    /// Double precision Matrix44.
    /// </summary>
    [StructLayout (LayoutKind.Sequential), Serializable]
    public struct Matrix44
        : IEquatable<Matrix44>
    {
        /// <summary>
        /// Gets or sets (Row 0, Column 0) of the Matrix44.
        /// </summary>
        public Double R0C0;

        /// <summary>
        /// Gets or sets (Row 0, Column 1) of the Matrix44.
        /// </summary>
        public Double R0C1;

        /// <summary>
        /// Gets or sets (Row 0, Column 2) of the Matrix44.
        /// </summary>
        public Double R0C2;

        /// <summary>
        /// Gets or sets (Row 0, Column 3) of the Matrix44.
        /// </summary>
        public Double R0C3;

        /// <summary>
        /// Gets or sets (Row 1, Column 0) of the Matrix44.
        /// </summary>
        public Double R1C0;

        /// <summary>
        /// Gets or sets (Row 1, Column 1) of the Matrix44.
        /// </summary>
        public Double R1C1;

        /// <summary>
        /// Gets or sets (ow 1, Column 2) of the Matrix44.
        /// </summary>
        public Double R1C2;

        /// <summary>
        /// Gets or sets (Row 1, Column 3) of the Matrix44.
        /// </summary>
        public Double R1C3;

        /// <summary>
        /// Gets or sets (Row 2, Column 0) of the Matrix44.
        /// </summary>
        public Double R2C0;

        /// <summary>
        /// Gets or sets (Row 2, Column 1) of the Matrix44.
        /// </summary>
        public Double R2C1;

        /// <summary>
        /// Gets or sets (Row 2, Column 2) of the Matrix44.
        /// </summary>
        public Double R2C2;

        /// <summary>
        /// Gets or sets (Row 2, Column 3) of the Matrix44.
        /// </summary>
        public Double R2C3;

        /// <summary>
        /// Gets or sets (Row 3, Column 0) of the Matrix44.
        /// </summary>
        public Double R3C0; // translation.x

        /// <summary>
        /// Gets or sets (Row 3, Column 1) of the Matrix44.
        /// </summary>
        public Double R3C1; // translation.y

        /// <summary>
        /// Gets or sets (Row 3, Column 2) of the Matrix44.
        /// </summary>
        public Double R3C2; // translation.z

        /// <summary>
        /// Gets or sets (Row 3, Column 3) of the Matrix44.
        /// </summary>
        public Double R3C3;

        /// <summary>
        /// Initilises a new instance of Matrix44 from sixteen Double
        /// values representing the matrix, in row major order, respectively.
        /// </summary>
        public Matrix44 (
            Double m00,
            Double m01,
            Double m02,
            Double m03,
            Double m10,
            Double m11,
            Double m12,
            Double m13,
            Double m20,
            Double m21,
            Double m22,
            Double m23,
            Double m30,
            Double m31,
            Double m32,
            Double m33)
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
        /// todo
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
        /// todo
        /// </summary>
        public static void CreateTranslation (
            ref Double xPosition,
            ref Double yPosition,
            ref Double zPosition,
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
            ref Double xScale,
            ref Double yScale,
            ref Double zScale,
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
        /// Create a scaling matrix consistant along each axis
        /// </summary>
        public static void CreateScale (
            ref Double scale,
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
        /// http://en.wikipedia.org/wiki/Rotation_matrix
        /// </summary>
        public static void CreateRotationX (
            ref Double radians,
            out Matrix44 result)
        {
            Double cos = Maths.Cos (radians);
            Double sin = Maths.Sin (radians);

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
        /// http://en.wikipedia.org/wiki/Rotation_matrix
        /// </summary>
        public static void CreateRotationY (
            ref Double radians,
            out Matrix44 result)
        {
            Double cos = Maths.Cos (radians);
            Double sin = Maths.Sin (radians);

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
        /// http://en.wikipedia.org/wiki/Rotation_matrix
        /// </summary>
        public static void CreateRotationZ (
            ref Double radians,
            out Matrix44 result)
        {
            Double cos = Maths.Cos (radians);
            Double sin = Maths.Sin (radians);

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
        /// todo
        /// </summary>
        public static void CreateFromAxisAngle (
            ref Vector3 axis,
            ref Double angle,
            out Matrix44 result)
        {
            Double one = 1;

            Double x = axis.X;
            Double y = axis.Y;
            Double z = axis.Z;

            Double sin = Maths.Sin (angle);
            Double cos = Maths.Cos (angle);

            Double xx = x * x;
            Double yy = y * y;
            Double zz = z * z;

            Double xy = x * y;
            Double xz = x * z;
            Double yz = y * z;

            result.R0C0 = xx + (cos * (one - xx));
            result.R0C1 = (xy - (cos * xy)) + (sin * z);
            result.R0C2 = (xz - (cos * xz)) - (sin * y);
            result.R0C3 = 0;

            result.R1C0 = (xy - (cos * xy)) - (sin * z);
            result.R1C1 = yy + (cos * (one - yy));
            result.R1C2 = (yz - (cos * yz)) + (sin * x);
            result.R1C3 = 0;

            result.R2C0 = (xz - (cos * xz)) + (sin * y);
            result.R2C1 = (yz - (cos * yz)) - (sin * x);
            result.R2C2 = zz + (cos * (one - zz));
            result.R2C3 = 0;

            result.R3C0 = 0;
            result.R3C1 = 0;
            result.R3C2 = 0;
            result.R3C3 = one;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void CreateFromAllAxis (
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
            {
                throw new ArgumentException(
                    "The input vertors must be normalised.");
            }

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
        /// todo  ???????????
        /// </summary>
        public static void CreateWorldNew (
            ref Vector3 position,
            ref Vector3 forward,
            ref Vector3 up,
            out Matrix44 result)
        {
            Vector3 backward;
            Vector3.Negate (ref forward, out backward);

            Vector3 right;
            Vector3.Cross (ref up, ref backward, out right);

            Vector3.Normalise(ref right, out right);

            Matrix44.CreateFromAllAxis(
                ref right, ref up, ref backward, out result);

            result.R3C0 = position.X;
            result.R3C1 = position.Y;
            result.R3C2 = position.Z;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void CreateWorld (
            ref Vector3 position,
            ref Vector3 forward,
            ref Vector3 up,
            out Matrix44 result)
        {
            Boolean isForwardUnit;
            Vector3.IsUnit (ref forward, out isForwardUnit);

            Boolean isUpUnit;
            Vector3.IsUnit (ref up, out isUpUnit);

            if(!isForwardUnit || !isUpUnit )
            {
                throw new ArgumentException(
                    "The input vertors must be normalised.");
            }

            Vector3 backward; Vector3.Negate (ref forward, out backward);

            Vector3 vector; Vector3.Normalise (ref backward, out vector);

            Vector3 cross; Vector3.Cross (ref up, ref vector, out cross);

            Vector3 vector2; Vector3.Normalise (ref cross, out vector2);

            Vector3 vector3;
            Vector3.Cross (ref vector, ref vector2, out vector3);

            result.R0C0 = vector2.X;
            result.R0C1 = vector2.Y;
            result.R0C2 = vector2.Z;
            result.R0C3 = 0;
            result.R1C0 = vector3.X;
            result.R1C1 = vector3.Y;
            result.R1C2 = vector3.Z;
            result.R1C3 = 0;
            result.R2C0 = vector.X;
            result.R2C1 = vector.Y;
            result.R2C2 = vector.Z;
            result.R2C3 = 0;
            result.R3C0 = position.X;
            result.R3C1 = position.Y;
            result.R3C2 = position.Z;
            result.R3C3 = 1;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void CreateFromQuaternion (
            ref Quaternion quaternion,
            out Matrix44 result)
        {
            Boolean quaternionIsUnit;
            Quaternion.IsUnit (ref quaternion, out quaternionIsUnit);
            if(!quaternionIsUnit)
            {
                throw new ArgumentException(
                    "Input quaternion must be normalised.");
            }

            Double zero = 0;
            Double one = 1;

            Double ii = quaternion.I + quaternion.I;
            Double jj = quaternion.J + quaternion.J;
            Double kk = quaternion.K + quaternion.K;

            Double uii = quaternion.U * ii;
            Double ujj = quaternion.U * jj;
            Double ukk = quaternion.U * kk;
            Double iii = quaternion.I * ii;
            Double ijj = quaternion.I * jj;
            Double ikk = quaternion.I * kk;
            Double jjj = quaternion.J * jj;
            Double jkk = quaternion.J * kk;
            Double kkk = quaternion.K * kk;

            result.R0C0 = one - (jjj + kkk);
            result.R1C0 = ijj - ukk;
            result.R2C0 = ikk + ujj;
            result.R3C0 = zero;

            result.R0C1 = ijj + ukk;
            result.R1C1 = one - (iii + kkk);
            result.R2C1 = jkk - uii;
            result.R3C1 = zero;

            result.R0C2 = ikk - ujj;
            result.R1C2 = jkk + uii;
            result.R2C2 = one - (iii + jjj);
            result.R3C2 = zero;

            result.R0C3 = zero;
            result.R1C3 = zero;
            result.R2C3 = zero;
            result.R3C3 = one;
        }

        /// <summary>
        /// todo
        /// </summary>
        public static void CreateFromYawPitchRoll (
            ref Double yaw,
            ref Double pitch,
            ref Double roll,
            out Matrix44 result)
        {
            Quaternion quaternion;

            Quaternion.CreateFromYawPitchRoll (
                ref yaw, ref pitch, ref roll, out quaternion);

            CreateFromQuaternion (ref quaternion, out result);
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Creates a cylindrical billboard that rotates around a specified axis.
        /// This method computes the facing direction of the billboard from the object position and camera position.
        /// When the object and camera positions are too close, the matrix will not be accurate.
        /// To avoid this problem, the method uses the optional camera forward vector if the positions are too close.
        /// </summary>
        public static void CreateBillboard (
            ref Vector3 ObjectPosition,
            ref Vector3 cameraPosition,
            ref Vector3 cameraUpVector,
            ref Vector3? cameraForwardVector,
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;

            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            vector.X = ObjectPosition.X - cameraPosition.X;
            vector.Y = ObjectPosition.Y - cameraPosition.Y;
            vector.Z = ObjectPosition.Z - cameraPosition.Z;
            Double num = vector.LengthSquared ();
            Double limit; Maths.FromString("0.0001", out limit);

            if (num < limit) {
                vector = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
            } else {
                var t = (Double)(one / (Maths.Sqrt (num)));
                Vector3.Multiply (ref vector, ref t, out vector);
            }
            Vector3.Cross (ref cameraUpVector, ref vector, out vector3);

            Vector3.Normalise (ref vector3, out vector3);

            Vector3.Cross (ref vector, ref vector3, out vector2);
            result.R0C0 = vector3.X;
            result.R0C1 = vector3.Y;
            result.R0C2 = vector3.Z;
            result.R0C3 = zero;
            result.R1C0 = vector2.X;
            result.R1C1 = vector2.Y;
            result.R1C2 = vector2.Z;
            result.R1C3 = zero;
            result.R2C0 = vector.X;
            result.R2C1 = vector.Y;
            result.R2C2 = vector.Z;
            result.R2C3 = zero;
            result.R3C0 = ObjectPosition.X;
            result.R3C1 = ObjectPosition.Y;
            result.R3C2 = ObjectPosition.Z;
            result.R3C3 = one;
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// todo
        /// </summary>
        public static void CreateConstrainedBillboard (
            ref Vector3 objectPosition,
            ref Vector3 cameraPosition,
            ref Vector3 rotateAxis,
            ref Vector3? cameraForwardVector,
            ref Vector3? objectForwardVector,
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;

            Double num;
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            vector2.X = objectPosition.X - cameraPosition.X;
            vector2.Y = objectPosition.Y - cameraPosition.Y;
            vector2.Z = objectPosition.Z - cameraPosition.Z;
            Double num2 = vector2.LengthSquared ();
            Double limit; Maths.FromString("0.0001", out limit);

            if (num2 < limit) {
                vector2 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
            } else {
                var t = (Double)(one / (Maths.Sqrt (num2)));
                Vector3.Multiply (ref vector2, ref t, out vector2);
            }
            Vector3 vector4 = rotateAxis;
            Vector3.Dot (ref rotateAxis, ref vector2, out num);

            Double realHorrid; Maths.FromString("0.9982547", out realHorrid);

            if (Maths.Abs (num) > realHorrid) {
                if (objectForwardVector.HasValue) {
                    vector = objectForwardVector.Value;
                    Vector3.Dot (ref rotateAxis, ref vector, out num);
                    if (Maths.Abs (num) > realHorrid) {
                        num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
                        vector = (Maths.Abs (num) > realHorrid) ? Vector3.Right : Vector3.Forward;
                    }
                } else {
                    num = ((rotateAxis.X * Vector3.Forward.X) + (rotateAxis.Y * Vector3.Forward.Y)) + (rotateAxis.Z * Vector3.Forward.Z);
                    vector = (Maths.Abs (num) > realHorrid) ? Vector3.Right : Vector3.Forward;
                }
                Vector3.Cross (ref rotateAxis, ref vector, out vector3);
                Vector3.Normalise (ref vector3, out vector3);
                Vector3.Cross (ref vector3, ref rotateAxis, out vector);
                Vector3.Normalise (ref vector, out vector);
            } else {
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
        /// http://msdn.microsoft.com/en-us/library/bb205351(v=vs.85).aspx
        /// </summary>
        public static void CreatePerspectiveFieldOfView (
            ref Double fieldOfView,
            ref Double aspectRatio,
            ref Double nearPlaneDistance,
            ref Double farPlaneDistance,
            out Matrix44 result)
        {
            Double zero = 0;
            Double half; Maths.Half(out half);
            Double one = 1;
            Double pi; Maths.Pi(out pi);

            if ((fieldOfView <= zero) || (fieldOfView >= pi))
            {
                throw new ArgumentOutOfRangeException ("fieldOfView");
            }

            if (nearPlaneDistance <= zero)
            {
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");
            }

            if (farPlaneDistance <= zero)
            {
                throw new ArgumentOutOfRangeException ("farPlaneDistance");
            }

            if (nearPlaneDistance >= farPlaneDistance)
            {
                throw new ArgumentOutOfRangeException ("nearPlaneDistance");
            }

            //
            // xScale     0          0              0
            // 0        yScale       0              0
            // 0        0        zf/(zn-zf)        -1
            // 0        0        zn*zf/(zn-zf)      0
            //
            // where:
            //
            // yScale = cot(fovY/2)
            //
            // xScale = yScale / aspect ratio
            //

            // yScale = cot(fovY/2)
            Double yScale = one / ( Maths.Tan ( fieldOfView * half ) );

            // xScale = yScale / aspect ratio
            Double xScale = yScale / aspectRatio;

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
            result.R2C2 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance); // zf/(zn-zf)
            result.R2C3 = -one;

            result.R3C0 = zero;
            result.R3C1 = zero;
            result.R3C2 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance); // zn*zf/(zn-zf)
            result.R3C3 = zero;
        }



        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205355(v=vs.85).aspx
        /// </summary>
        public static void CreatePerspective (
            ref Double width,
            ref Double height,
            ref Double nearPlaneDistance,
            ref Double farPlaneDistance,
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;
            Double two = 2;

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
            result.R2C2 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.R2C0 = result.R2C1 = zero;
            result.R2C3 = -one;
            result.R3C0 = result.R3C1 = result.R3C3 = zero;
            result.R3C2 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205354(v=vs.85).aspx
        /// </summary>
        public static void CreatePerspectiveOffCenter (
            ref Double left,
            ref Double right,
            ref Double bottom,
            ref Double top,
            ref Double nearPlaneDistance,
            ref Double farPlaneDistance,
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;
            Double two = 2;

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
            result.R2C2 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.R2C3 = -one;
            result.R3C2 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            result.R3C0 = result.R3C1 = result.R3C3 = zero;
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205349(v=vs.85).aspx
        /// </summary>
        public static void CreateOrthographic (
            ref Double width,
            ref Double height,
            ref Double zNearPlane,
            ref Double zFarPlane,
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;
            Double two = 2;

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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205348(v=vs.85).aspx
        /// </summary>
        public static void CreateOrthographicOffCenter (
            ref Double left,
            ref Double right,
            ref Double bottom,
            ref Double top,
            ref Double zNearPlane,
            ref Double zFarPlane,
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;
            Double two = 2;

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

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/bb205343(v=VS.85).aspx
        /// </summary>
        public static void CreateLookAt (
            ref Vector3 cameraPosition,
            ref Vector3 cameraTarget,
            ref Vector3 cameraUpVector,
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;

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

            Double a;
            Double b;
            Double c;

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
        public static void Transpose (ref Matrix44 input, out Matrix44 output)
        {
            output.R0C0 = input.R0C0;
            output.R1C1 = input.R1C1;
            output.R2C2 = input.R2C2;
            output.R3C3 = input.R3C3;

            Double temp = input.R0C1;
            output.R0C1 = input.R1C0;
            output.R1C0 = temp;

            temp = input.R0C2;
            output.R0C2 = input.R2C0;
            output.R2C0 = temp;

            temp = input.R0C3;
            output.R0C3 = input.R3C0;
            output.R3C0 = temp;

            temp = input.R1C2;
            output.R1C2 = input.R2C1;
            output.R2C1 = temp;

            temp = input.R1C3;
            output.R1C3 = input.R3C1;
            output.R3C1 = temp;

            temp =  input.R2C3;
            output.R2C3 = input.R3C2;
            output.R3C2 = temp;
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

            Double aLen; Vector3.Length(ref a, out aLen); scale.X = aLen;
            Double bLen; Vector3.Length(ref b, out bLen); scale.Y = bLen;
            Double cLen; Vector3.Length(ref c, out cLen); scale.Z = cLen;

            if ( Maths.IsZero(scale.X) ||
                 Maths.IsZero(scale.Y) ||
                 Maths.IsZero(scale.Z) )
            {
                rotation = Quaternion.Identity;
                result = false;
            }

            Double epsilon; Maths.Epsilon(out epsilon);

            if (aLen < epsilon) a = Vector3.Zero;
            else Vector3.Normalise(ref a, out a);

            if (bLen < epsilon) b = Vector3.Zero;
            else Vector3.Normalise(ref b, out b);

            if (cLen < epsilon) c = Vector3.Zero;
            else Vector3.Normalise(ref c, out c);

            Vector3 right = new Vector3(a.X, b.X, c.X);
            Vector3 up = new Vector3(a.Y, b.Y, c.Y);
            Vector3 backward = new Vector3(a.Z, b.Z, c.Z);

            if (right == Vector3.Zero) right = Vector3.Right;
            if (up == Vector3.Zero) up = Vector3.Up;
            if (backward == Vector3.Zero) backward = Vector3.Backward;

            Vector3.Normalise(ref right, out right);
            Vector3.Normalise(ref up, out up);
            Vector3.Normalise(ref backward, out backward);

            Matrix44 rotMat;
            Matrix44.CreateFromAllAxis(
                ref right, ref up, ref backward, out rotMat);

            Quaternion.CreateFromRotationMatrix(ref rotMat, out rotation);

            result = true;
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// todo
        /// </summary>
        public void Determinant (ref Matrix44 matrix, out Double result)
        {
            Double num22 = matrix.R0C0;
            Double num21 = matrix.R0C1;
            Double num20 = matrix.R0C2;
            Double num19 = matrix.R0C3;
            Double num12 = matrix.R1C0;
            Double num11 = matrix.R1C1;
            Double num10 = matrix.R1C2;
            Double num9 = matrix.R1C3;
            Double num8 = matrix.R2C0;
            Double num7 = matrix.R2C1;
            Double num6 = matrix.R2C2;
            Double num5 = matrix.R2C3;
            Double num4 = matrix.R3C0;
            Double num3 = matrix.R3C1;
            Double num2 = matrix.R3C2;
            Double num = matrix.R3C3;

            Double num18 = (num6 * num) - (num5 * num2);
            Double num17 = (num7 * num) - (num5 * num3);
            Double num16 = (num7 * num2) - (num6 * num3);
            Double num15 = (num8 * num) - (num5 * num4);
            Double num14 = (num8 * num2) - (num6 * num4);
            Double num13 = (num8 * num3) - (num7 * num4);

            result = ((((num22 * (((num11 * num18) - (num10 * num17)) + (num9 * num16))) - (num21 * (((num12 * num18) - (num10 * num15)) + (num9 * num14)))) + (num20 * (((num12 * num17) - (num11 * num15)) + (num9 * num13)))) - (num19 * (((num12 * num16) - (num11 * num14)) + (num10 * num13))));
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// todo
        /// </summary>
        public static void Invert (ref Matrix44 matrix, out Matrix44 result)
        {
            Double one = 1;
            Double num5 = matrix.R0C0;
            Double num4 = matrix.R0C1;
            Double num3 = matrix.R0C2;
            Double num2 = matrix.R0C3;
            Double num9 = matrix.R1C0;
            Double num8 = matrix.R1C1;
            Double num7 = matrix.R1C2;
            Double num6 = matrix.R1C3;
            Double num17 = matrix.R2C0;
            Double num16 = matrix.R2C1;
            Double num15 = matrix.R2C2;
            Double num14 = matrix.R2C3;
            Double num13 = matrix.R3C0;
            Double num12 = matrix.R3C1;
            Double num11 = matrix.R3C2;
            Double num10 = matrix.R3C3;
            Double num23 = (num15 * num10) - (num14 * num11);
            Double num22 = (num16 * num10) - (num14 * num12);
            Double num21 = (num16 * num11) - (num15 * num12);
            Double num20 = (num17 * num10) - (num14 * num13);
            Double num19 = (num17 * num11) - (num15 * num13);
            Double num18 = (num17 * num12) - (num16 * num13);
            Double num39 = ((num8 * num23) - (num7 * num22)) + (num6 * num21);
            Double num38 = -(((num9 * num23) - (num7 * num20)) + (num6 * num19));
            Double num37 = ((num9 * num22) - (num8 * num20)) + (num6 * num18);
            Double num36 = -(((num9 * num21) - (num8 * num19)) + (num7 * num18));
            Double num = one / ((((num5 * num39) + (num4 * num38)) + (num3 * num37)) + (num2 * num36));
            result.R0C0 = num39 * num;
            result.R1C0 = num38 * num;
            result.R2C0 = num37 * num;
            result.R3C0 = num36 * num;
            result.R0C1 = -(((num4 * num23) - (num3 * num22)) + (num2 * num21)) * num;
            result.R1C1 = (((num5 * num23) - (num3 * num20)) + (num2 * num19)) * num;
            result.R2C1 = -(((num5 * num22) - (num4 * num20)) + (num2 * num18)) * num;
            result.R3C1 = (((num5 * num21) - (num4 * num19)) + (num3 * num18)) * num;
            Double num35 = (num7 * num10) - (num6 * num11);
            Double num34 = (num8 * num10) - (num6 * num12);
            Double num33 = (num8 * num11) - (num7 * num12);
            Double num32 = (num9 * num10) - (num6 * num13);
            Double num31 = (num9 * num11) - (num7 * num13);
            Double num30 = (num9 * num12) - (num8 * num13);
            result.R0C2 = (((num4 * num35) - (num3 * num34)) + (num2 * num33)) * num;
            result.R1C2 = -(((num5 * num35) - (num3 * num32)) + (num2 * num31)) * num;
            result.R2C2 = (((num5 * num34) - (num4 * num32)) + (num2 * num30)) * num;
            result.R3C2 = -(((num5 * num33) - (num4 * num31)) + (num3 * num30)) * num;
            Double num29 = (num7 * num14) - (num6 * num15);
            Double num28 = (num8 * num14) - (num6 * num16);
            Double num27 = (num8 * num15) - (num7 * num16);
            Double num26 = (num9 * num14) - (num6 * num17);
            Double num25 = (num9 * num15) - (num7 * num17);
            Double num24 = (num9 * num16) - (num8 * num17);
            result.R0C3 = -(((num4 * num29) - (num3 * num28)) + (num2 * num27)) * num;
            result.R1C3 = (((num5 * num29) - (num3 * num26)) + (num2 * num25)) * num;
            result.R2C3 = -(((num5 * num28) - (num4 * num26)) + (num2 * num24)) * num;
            result.R3C3 = (((num5 * num27) - (num4 * num25)) + (num3 * num24)) * num;
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        // TODO: FROM XNA, NEEDS REVIEW
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Transforms a Matrix by applying a Quaternion rotation.
        /// </summary>
        public static void Transform (ref Matrix44 value, ref Quaternion rotation, out Matrix44 result)
        {
            Double one = 1;

            Double num21 = rotation.I + rotation.I;
            Double num11 = rotation.J + rotation.J;
            Double num10 = rotation.K + rotation.K;

            Double num20 = rotation.U * num21;
            Double num19 = rotation.U * num11;
            Double num18 = rotation.U * num10;
            Double num17 = rotation.I * num21;
            Double num16 = rotation.I * num11;
            Double num15 = rotation.I * num10;
            Double num14 = rotation.J * num11;
            Double num13 = rotation.J * num10;
            Double num12 = rotation.K * num10;

            Double num9 = (one - num14) - num12;

            Double num8 = num16 - num18;
            Double num7 = num15 + num19;
            Double num6 = num16 + num18;

            Double num5 = (one - num17) - num12;

            Double num4 = num13 - num20;
            Double num3 = num15 - num19;
            Double num2 = num13 + num20;

            Double num = (one - num17) - num14;

            Double num37 = ((value.R0C0 * num9) + (value.R0C1 * num8)) + (value.R0C2 * num7);
            Double num36 = ((value.R0C0 * num6) + (value.R0C1 * num5)) + (value.R0C2 * num4);
            Double num35 = ((value.R0C0 * num3) + (value.R0C1 * num2)) + (value.R0C2 * num);

            Double num34 = value.R0C3;

            Double num33 = ((value.R1C0 * num9) + (value.R1C1 * num8)) + (value.R1C2 * num7);
            Double num32 = ((value.R1C0 * num6) + (value.R1C1 * num5)) + (value.R1C2 * num4);
            Double num31 = ((value.R1C0 * num3) + (value.R1C1 * num2)) + (value.R1C2 * num);

            Double num30 = value.R1C3;

            Double num29 = ((value.R2C0 * num9) + (value.R2C1 * num8)) + (value.R2C2 * num7);
            Double num28 = ((value.R2C0 * num6) + (value.R2C1 * num5)) + (value.R2C2 * num4);
            Double num27 = ((value.R2C0 * num3) + (value.R2C1 * num2)) + (value.R2C2 * num);

            Double num26 = value.R2C3;

            Double num25 = ((value.R3C0 * num9) + (value.R3C1 * num8)) + (value.R3C2 * num7);
            Double num24 = ((value.R3C0 * num6) + (value.R3C1 * num5)) + (value.R3C2 * num4);
            Double num23 = ((value.R3C0 * num3) + (value.R3C1 * num2)) + (value.R3C2 * num);

            Double num22 = value.R3C3;

            result.R0C0 = num37;
            result.R0C1 = num36;
            result.R0C2 = num35;
            result.R0C3 = num34;
            result.R1C0 = num33;
            result.R1C1 = num32;
            result.R1C2 = num31;
            result.R1C3 = num30;
            result.R2C0 = num29;
            result.R2C1 = num28;
            result.R2C2 = num27;
            result.R2C3 = num26;
            result.R3C0 = num25;
            result.R3C1 = num24;
            result.R3C2 = num23;
            result.R3C3 = num22;
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
        /// Performs multiplication of a Matrix44 object and a Double
        /// precision scaling factor.
        /// </summary>
        public static void Multiply (
            ref Matrix44 matrix,
            ref Double scaleFactor,
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
            ref Matrix44 m1, ref Matrix44 m2, ref Double amount,
            out Matrix44 result)
        {
            Double zero = 0;
            Double one = 1;

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
            Double xPosition,
            Double yPosition,
            Double zPosition)
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
            Double xScale,
            Double yScale,
            Double zScale)
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
        public static Matrix44 CreateScale (Double scale)
        {
            Matrix44 result;
            CreateScale (ref scale, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateRotationX (Double radians)
        {
            Matrix44 result;
            CreateRotationX (ref radians, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateRotationY (Double radians)
        {
            Matrix44 result;
            CreateRotationY (ref radians, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateRotationZ (Double radians)
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
            Double angle)
        {
            Matrix44 result;
            CreateFromAxisAngle (ref axis, ref angle, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 CreateFromAllAxis (
            Vector3 right,
            Vector3 up,
            Vector3 backward)
        {
            Matrix44 result;
            CreateFromAllAxis (
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
            Double yaw,
            Double pitch,
            Double roll)
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
            Double fieldOfView,
            Double aspectRatio,
            Double nearPlane,
            Double farPlane)
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
            Double width,
            Double height,
            Double nearPlane,
            Double farPlane)
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
            Double left,
            Double right,
            Double bottom,
            Double top,
            Double nearPlane,
            Double farPlane)
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
            Double width,
            Double height,
            Double nearPlane,
            Double farPlane)
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
            Double left,
            Double right,
            Double bottom,
            Double top,
            Double nearPlane,
            Double farPlane)
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
        public Double Determinant (Matrix44 matrix)
        {
            Double result;
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
            Matrix44 matrix, Double scaleFactor)
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
            Matrix44 matrix, Double scaleFactor)
        {
            Matrix44 result;
            Multiply (ref matrix, ref scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Variant function.
        /// </summary>
        public static Matrix44 operator * (
            Double scaleFactor, Matrix44 matrix)
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
            Double amount)
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


}