// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using compute.utils;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.Serialization;

namespace compute.drawing
{
    /// <summary>
    /// Describes a 2D-point.
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct PointF : IEquatable<PointF>, IPoint<float>
    {
        #region Private Fields

        private static readonly PointF zeroPoint = new PointF();

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of this <see cref="PointF"/>.
        /// </summary>
        [DataMember]
        public float X { get; }

        /// <summary>
        /// The y coordinate of this <see cref="PointF"/>.
        /// </summary>
        [DataMember]
        public float Y { get; }

        #endregion

        #region Properties

        /// <summary>
        /// Returns a <see cref="Point"/> with coordinates 0, 0.
        /// </summary>
        public static PointF Zero
        {
            get { return zeroPoint; }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.X.ToString(), "  ",
                    this.Y.ToString()
                );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a point with X and Y from two values.
        /// </summary>
        /// <param name="x">The x coordinate in 2d-space.</param>
        /// <param name="y">The y coordinate in 2d-space.</param>
        public PointF(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructs a point with X and Y set to the same value.
        /// </summary>
        /// <param name="value">The x and y coordinates in 2d-space.</param>
        public PointF(float value)
        {
            this.X = value;
            this.Y = value;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Inverts values in the specified <see cref="PointF"/>.
        /// </summary>
        /// <param name="value">Source <see cref="PointF"/> on the right of the sub sign.</param>
        /// <returns>Result of the inversion.</returns>
        public static PointF operator -(PointF value)
        {
            return Negate(value);
        }

        /// <summary>
        /// Adds two points.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="PointF"/> on the right of the add sign.</param>
        /// <returns>Sum of the points.</returns>
        public static PointF operator +(PointF value1, PointF value2)
        {
            return new PointF(value1.X + value2.X, value1.Y + value2.Y);
        }

        /// <summary>
        /// Adds two points.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="int"/> on the right of the add sign.</param>
        /// <returns>Sum of the points.</returns>
        public static PointF operator +(PointF value1, float value2)
        {
            return new PointF(value1.X + value2, value1.Y + value2);
        }

        /// <summary>
        /// Adds two points.
        /// </summary>
        /// <param name="value1">Source <see cref="float"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="PointF"/> on the right of the add sign.</param>
        /// <returns>Sum of the points.</returns>
        public static PointF operator +(int value1, PointF value2)
        {
            return new PointF(value1 + value2.X, value1 + value2.Y);
        }

        /// <summary>
        /// Subtracts a <see cref="PointF"/> from a <see cref="PointF"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="PointF"/> on the right of the sub sign.</param>
        /// <returns>Result of the subtraction.</returns>
        public static PointF operator -(PointF value1, PointF value2)
        {
            return new PointF(value1.X - value2.X, value1.Y - value2.Y);
        }

        /// <summary>
        /// Subtracts a <see cref="PointF"/> from a <see cref="PointF"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="float"/> on the right of the sub sign.</param>
        /// <returns>Result of the subtraction.</returns>
        public static PointF operator -(PointF value1, float value2)
        {
            return new PointF(value1.X - value2, value1.Y - value2);
        }

        /// <summary>
        /// Subtracts a <see cref="PointF"/> from a <see cref="PointF"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="float"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="PointF"/> on the right of the sub sign.</param>
        /// <returns>Result of the subtraction.</returns>
        public static PointF operator -(float value1, PointF value2)
        {
            return new PointF(value1 - value2.X, value1 - value2.Y);
        }

        /// <summary>
        /// Multiplies the components of two points by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="PointF"/> on the right of the mul sign.</param>
        /// <returns>Result of the multiplication.</returns>
        public static PointF operator *(PointF value1, PointF value2)
        {
            return new PointF(value1.X * value2.X, value1.Y * value2.Y);
        }

        /// <summary>
        /// Multiplies the components of two points by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="Point"/> on the right of the mul sign.</param>
        /// <returns>Result of the multiplication.</returns>
        public static PointF operator *(PointF value1, float value2)
        {
            return new PointF(value1.X * value2, value1.Y * value2);
        }

        /// <summary>
        /// Multiplies the components of two points by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="float"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="Point"/> on the right of the mul sign.</param>
        /// <returns>Result of the multiplication.</returns>
        public static PointF operator *(float value1, PointF value2)
        {
            return new PointF(value1 * value2.X, value1 * value2.Y);
        }

        /// <summary>
        /// Divides the components of a <see cref="PointF"/> by the components of another <see cref="Point"/>.
        /// </summary>
        /// <param name="source">Source <see cref="PointF"/> on the left of the div sign.</param>
        /// <param name="divisor">Divisor <see cref="PointF"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the points.</returns>
        public static PointF operator /(PointF source, PointF divisor)
        {
            return new PointF(source.X / divisor.X, source.Y / divisor.Y);
        }

        /// <summary>
        /// Divides the components of a <see cref="PointF"/> by the components of another <see cref="PointF"/>.
        /// </summary>
        /// <param name="source">Source <see cref="PointF"/> on the left of the div sign.</param>
        /// <param name="divisor">Divisor <see cref="float"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the points.</returns>
        public static PointF operator /(PointF source, float divisor)
        {
            return new PointF(source.X / divisor, source.Y / divisor);
        }

        /// <summary>
        /// Divides the components of a <see cref="PointF"/> by the components of another <see cref="PointF"/>.
        /// </summary>
        /// <param name="divisor">Source <see cref="float"/> on the left of the div sign.</param>
        /// <param name="source">Divisor <see cref="PointF"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the points.</returns>
        public static PointF operator /(float divisor, PointF source)
        {
            return new PointF(divisor / source.X, divisor / source.Y);
        }

        /// <summary>
        /// Compares whether two <see cref="PointF"/> instances are equal.
        /// </summary>
        /// <param name="a"><see cref="PointF"/> instance on the left of the equal sign.</param>
        /// <param name="b"><see cref="PointF"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(PointF a, PointF b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Compares whether two <see cref="PointF"/> instances are not equal.
        /// </summary>
        /// <param name="a"><see cref="PointF"/> instance on the left of the not equal sign.</param>
        /// <param name="b"><see cref="PointF"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
        public static bool operator !=(PointF a, PointF b)
        {
            return !a.Equals(b);
        }

        #endregion

        #region Public methods

        public float Magnitude()
        {
            return MathF.Sqrt(X * X + Y * Y);
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static float DistanceSquared(PointF value1, PointF value2)
        {
            float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            return (v1 * v1) + (v2 * v2);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static float Distance(PointF value1, PointF value2)
        {
            float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            return (float)Math.Sqrt((v1 * v1) + (v2 * v2));
        }

        /// <summary>
        /// Returns the length of this <see cref="PointF"/>.
        /// </summary>
        /// <returns>The length of this <see cref="PointF"/>.</returns>
        public float Length()
        {
            return (float)Math.Sqrt((X * X) + (Y * Y));
        }

        /// <summary>
        /// Returns the squared length of this <see cref="PointF"/>.
        /// </summary>
        /// <returns>The squared length of this <see cref="PointF"/>.</returns>
        public float LengthSquared()
        {
            return (X * X) + (Y * Y);
        }

        /// <summary>
        /// Turns this <see cref="PointF"/> to a unit vector with the same direction.
        /// </summary>
        public PointF Normalize()
        {
            float val = 1.0f / (float)Math.Sqrt((X * X) + (Y * Y));
            return new PointF(X * val, Y * val);
        }

        /// <summary>
        /// Turns this <see cref="PointF"/> to a unit vector with the same direction.
        /// </summary>
        public static PointF Normalize(PointF v)
        {
            return v.Normalize();
        }

        /// <summary>
        /// Creates a new <see cref="PointF"/> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        public static PointF Lerp(PointF value1, PointF value2, float amount)
        {
            return new PointF(
                MathTools.Lerp(value1.X, value2.X, amount),
                MathTools.Lerp(value1.Y, value2.Y, amount));
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        public static float Dot(PointF value1, PointF value2)
        {
            return (value1.X * value2.X) + (value1.Y * value2.Y);
        }

        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The cross product of two vectors.</returns>
        public static PointF Cross(PointF vector1, PointF vector2)
        {
            var x = vector1.Y - vector2.Y;
            var y = -(vector1.X - vector2.X);
            return new PointF(x, y);
        }

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <returns>The result of the vector addition.</returns>
        public static PointF Add(PointF value1, PointF value2)
        {
            return value1 + value2;
        }

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <returns>The result of the vector addition.</returns>
        public static PointF Add(PointF value1, float value2)
        {
            return new PointF(value1.X + value2, value1.Y + value2);
        }

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <returns>The result of the vector addition.</returns>
        public static PointF Add(float value1, PointF value2)
        {
            return new PointF(value1 + value2.X, value1 + value2.Y);
        }

        /// <summary>
        /// Creates a new <see cref="PointF"/> that contains subtraction of on <see cref="PointF"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/>.</param>
        /// <param name="value2">Source <see cref="PointF"/>.</param>
        /// <returns>The result of the vector subtraction.</returns>
        public static PointF Subtract(PointF value1, PointF value2)
        {
            return value1 - value2;
        }

        /// <summary>
        /// Creates a new <see cref="PointF"/> that contains subtraction of on <see cref="PointF"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/>.</param>
        /// <param name="value2">Source <see cref="float"/>.</param>
        /// <returns>The result of the vector subtraction.</returns>
        public static PointF Subtract(PointF value1, float value2)
        {
            return new PointF(value1.X - value2, value1.Y - value2);
        }

        /// <summary>
        /// Creates a new <see cref="PointF"/> that contains subtraction of on <see cref="PointF"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/>.</param>
        /// <param name="value2">Source <see cref="float"/>.</param>
        /// <returns>The result of the vector subtraction.</returns>
        public static PointF Subtract(float value1, PointF value2)
        {
            return new PointF(value1 - value2.X, value1 - value2.Y);
        }

        /// <summary>
        /// Creates a new <see cref="PointF"/> that contains a multiplication of two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/>.</param>
        /// <param name="value2">Source <see cref="PointF"/>.</param>
        /// <returns>The result of the vector multiplication.</returns>
        public static PointF Multiply(PointF value1, PointF value2)
        {
            return value1 * value2;
        }

        /// <summary>
        /// Creates a new <see cref="PointF"/> that contains a multiplication of <see cref="PointF"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>The result of the vector multiplication with a scalar.</returns>
        public static PointF Multiply(PointF value1, float scaleFactor)
        {
            return value1 * scaleFactor;
        }

        /// <summary>
        /// Creates a new <see cref="PointF"/> that contains a multiplication of <see cref="PointF"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>The result of the vector multiplication with a scalar.</returns>
        public static PointF Multiply(float value1, PointF scaleFactor)
        {
            return value1 * scaleFactor;
        }

        /// <summary>
        /// Divides the components of a <see cref="PointF"/> by the components of another <see cref="PointF"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/>.</param>
        /// <param name="value2">Divisor <see cref="PointF"/>.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static PointF Divide(PointF value1, PointF value2)
        {
            return new PointF(value1.X / value2.X, value1.Y / value2.Y);
        }

        /// <summary>
        /// Divides the components of a <see cref="PointF"/> by the components of another <see cref="PointF"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/>.</param>
        /// <param name="value2">Divisor <see cref="float"/>.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static PointF Divide(PointF value1, float value2)
        {
            return new PointF(value1.X / value2, value1.Y / value2);
        }

        /// <summary>
        /// Divides the components of a <see cref="PointF"/> by the components of another <see cref="PointF"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/>.</param>
        /// <param name="value2">Divisor <see cref="float"/>.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static PointF Divide(float value1, PointF value2)
        {
            return new PointF(value1 / value2.X, value1 / value2.Y);
        }

        /// <summary>
        /// Creates a new <see cref="PointF"/> that contains the specified vector inversion.
        /// </summary>
        /// <param name="value">Source <see cref="PointF"/>.</param>
        /// <returns>The result of the vector inversion.</returns>
        public static PointF Negate(PointF value)
        {
            return new PointF(-value.X, -value.Y);
        }

        /// <summary>
        /// Creates a new <see cref="PointF"/> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="PointF"/> with maximal values from the two vectors.</returns>
        public static PointF Max(PointF value1, PointF value2)
        {
            return new PointF(value1.X > value2.X ? value1.X : value2.X,
                               value1.Y > value2.Y ? value1.Y : value2.Y);
        }

        /// <summary>
        /// Creates a new <see cref="PointF"/> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="PointF"/> with minimal values from the two vectors.</returns>
        public static PointF Min(PointF value1, PointF value2)
        {
            return new PointF(value1.X < value2.X ? value1.X : value2.X,
                               value1.Y < value2.Y ? value1.Y : value2.Y);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is PointF) && Equals((PointF)obj);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Point"/>.
        /// </summary>
        /// <param name="other">The <see cref="Point"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(PointF other)
        {
            return ((X == other.X) && (Y == other.Y));
        }

        /// <summary>
        /// Gets the hash code of this <see cref="PointF"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="PointF"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }

        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="PointF"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>]}
        /// </summary>
        /// <returns><see cref="String"/> representation of this <see cref="PointF"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + "}";
        }

        /// <summary>
        /// Gets a <see cref="Vector2"/> representation for this object.
        /// </summary>
        /// <returns>A <see cref="Vector2"/> representation for this object.</returns>
        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        /// <summary>
        /// Gets a <see cref="Point"/> representation for this object.
        /// </summary>
        /// <returns>A <see cref="Point"/> representation for this object.</returns>
        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }

        #endregion
    }
}


