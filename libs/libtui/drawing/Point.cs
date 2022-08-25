// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using libtui.utils;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.Serialization;

namespace libtui.drawing
{
    /// <summary>
    /// Describes a 2D-point.
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct Point : IEquatable<Point>, IPoint<int>
    {
        #region Private Fields

        private static readonly Point zeroPoint = new Point();

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of this <see cref="Point"/>.
        /// </summary>
        [DataMember]
        public int X { get; }

        /// <summary>
        /// The y coordinate of this <see cref="Point"/>.
        /// </summary>
        [DataMember]
        public int Y { get; }

        #endregion

        #region Properties

        /// <summary>
        /// Returns a <see cref="Point"/> with coordinates 0, 0.
        /// </summary>
        public static Point Zero
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
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructs a point with X and Y set to the same value.
        /// </summary>
        /// <param name="value">The x and y coordinates in 2d-space.</param>
        public Point(int value)
        {
            this.X = value;
            this.Y = value;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Inverts values in the specified <see cref="Point"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Point"/> on the right of the sub sign.</param>
        /// <returns>Result of the inversion.</returns>
        public static Point operator -(Point value)
        {
            return Negate(value);
        }

        /// <summary>
        /// Adds two points.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="Point"/> on the right of the add sign.</param>
        /// <returns>Sum of the points.</returns>
        public static Point operator +(Point value1, Point value2)
        {
            return new Point(value1.X + value2.X, value1.Y + value2.Y);
        }

        /// <summary>
        /// Adds two points.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="int"/> on the right of the add sign.</param>
        /// <returns>Sum of the points.</returns>
        public static Point operator +(Point value1, int value2)
        {
            return new Point(value1.X + value2, value1.Y + value2);
        }

        /// <summary>
        /// Adds two points.
        /// </summary>
        /// <param name="value1">Source <see cref="int"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="Point"/> on the right of the add sign.</param>
        /// <returns>Sum of the points.</returns>
        public static Point operator +(int value1, Point value2)
        {
            return new Point(value1 + value2.X, value1 + value2.Y);
        }

        /// <summary>
        /// Subtracts a <see cref="Point"/> from a <see cref="Point"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="Point"/> on the right of the sub sign.</param>
        /// <returns>Result of the subtraction.</returns>
        public static Point operator -(Point value1, Point value2)
        {
            return new Point(value1.X - value2.X, value1.Y - value2.Y);
        }

        /// <summary>
        /// Subtracts a <see cref="Point"/> from a <see cref="Point"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="int"/> on the right of the sub sign.</param>
        /// <returns>Result of the subtraction.</returns>
        public static Point operator -(Point value1, int value2)
        {
            return new Point(value1.X - value2, value1.Y - value2);
        }

        /// <summary>
        /// Subtracts a <see cref="Point"/> from a <see cref="Point"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="int"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="Point"/> on the right of the sub sign.</param>
        /// <returns>Result of the subtraction.</returns>
        public static Point operator -(int value1, Point value2)
        {
            return new Point(value1 - value2.X, value1 - value2.Y);
        }

        /// <summary>
        /// Multiplies the components of two points by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="float"/> on the right of the mul sign.</param>
        /// <returns>Result of the multiplication.</returns>
        public static Point operator *(Point value1, float value2)
        {
            return new Point((int)(value1.X * value2), (int)(value1.Y * value2));
        }

        /// <summary>
        /// Multiplies the components of two points by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="float"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="Point"/> on the right of the mul sign.</param>
        /// <returns>Result of the multiplication.</returns>
        public static Point operator *(float value1, Point value2)
        {
            return new Point((int)(value1 * value2.X), (int)(value1 * value2.Y));
        }

        /// <summary>
        /// Multiplies the components of two points by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="Point"/> on the right of the mul sign.</param>
        /// <returns>Result of the multiplication.</returns>
        public static Point operator *(Point value1, Point value2)
        {
            return new Point(value1.X * value2.X, value1.Y * value2.Y);
        }

        /// <summary>
        /// Divides the components of a <see cref="Point"/> by the components of another <see cref="Point"/>.
        /// </summary>
        /// <param name="source">Source <see cref="Point"/> on the left of the div sign.</param>
        /// <param name="divisor">Divisor <see cref="Point"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the points.</returns>
        public static Point operator /(Point source, Point divisor)
        {
            return new Point(source.X / divisor.X, source.Y / divisor.Y);
        }

        /// <summary>
        /// Divides the components of a <see cref="Point"/> by the components of another <see cref="Point"/>.
        /// </summary>
        /// <param name="source">Source <see cref="Point"/> on the left of the div sign.</param>
        /// <param name="divisor">Divisor <see cref="float"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the points.</returns>
        public static Point operator /(Point source, float divisor)
        {
            return new Point((int)(source.X / divisor), (int)(source.Y / divisor));
        }

        /// <summary>
        /// Divides the components of a <see cref="Point"/> by the components of another <see cref="Point"/>.
        /// </summary>
        /// <param name="divisor">Source <see cref="float"/> on the left of the div sign.</param>
        /// <param name="source">Divisor <see cref="Point"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the points.</returns>
        public static Point operator /(float divisor, Point source)
        {
            return new Point((int)(divisor / source.X), (int)(divisor / source.Y));
        }

        /// <summary>
        /// Compares whether two <see cref="Point"/> instances are equal.
        /// </summary>
        /// <param name="a"><see cref="Point"/> instance on the left of the equal sign.</param>
        /// <param name="b"><see cref="Point"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(Point a, Point b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Compares whether two <see cref="Point"/> instances are not equal.
        /// </summary>
        /// <param name="a"><see cref="Point"/> instance on the left of the not equal sign.</param>
        /// <param name="b"><see cref="Point"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
        public static bool operator !=(Point a, Point b)
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
        public static int DistanceSquared(Point value1, Point value2)
        {
            int v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            return (v1 * v1) + (v2 * v2);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static int Distance(Point value1, Point value2)
        {
            float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            return (int)Math.Sqrt((v1 * v1) + (v2 * v2));
        }

        /// <summary>
        /// Returns the length of this <see cref="Point"/>.
        /// </summary>
        /// <returns>The length of this <see cref="Point"/>.</returns>
        public int Length()
        {
            return (int)Math.Sqrt((X * X) + (Y * Y));
        }

        /// <summary>
        /// Returns the squared length of this <see cref="Vector2"/>.
        /// </summary>
        /// <returns>The squared length of this <see cref="Vector2"/>.</returns>
        public int LengthSquared()
        {
            return (X * X) + (Y * Y);
        }

        /// <summary>
        /// Turns this <see cref="Vector2"/> to a unit vector with the same direction.
        /// </summary>
        public Point Normalize()
        {
            float val = 1.0f / (float)Math.Sqrt((X * X) + (Y * Y));
            return new Point((int)(X * val), (int)(Y * val));
        }

        /// <summary>
        /// Turns this <see cref="Point"/> to a unit vector with the same direction.
        /// </summary>
        public static Point Normalize(Point v)
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
        public static Point Lerp(Point value1, Point value2, float amount)
        {
            return new Point(
                (int)MathTools.Lerp(value1.X, value2.X, amount),
                (int)MathTools.Lerp(value1.Y, value2.Y, amount));
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        public static float Dot(Point value1, Point value2)
        {
            return (value1.X * value2.X) + (value1.Y * value2.Y);
        }

        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The cross product of two vectors.</returns>
        public static Point Cross(Point vector1, Point vector2)
        {
            var x = vector1.Y - vector2.Y;
            var y = -(vector1.X - vector2.X);
            return new Point(x, y);
        }

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <returns>The result of the vector addition.</returns>
        public static Point Add(Point value1, Point value2)
        {
            return value1 + value2;
        }

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <returns>The result of the vector addition.</returns>
        public static Point Add(Point value1, int value2)
        {
            return new Point(value1.X + value2, value1.Y + value2);
        }

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <returns>The result of the vector addition.</returns>
        public static Point Add(int value1, Point value2)
        {
            return new Point(value1 + value2.X, value1 + value2.Y);
        }

        /// <summary>
        /// Creates a new <see cref="Point"/> that contains subtraction of on <see cref="Point"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/>.</param>
        /// <param name="value2">Source <see cref="Point"/>.</param>
        /// <returns>The result of the vector subtraction.</returns>
        public static Point Subtract(Point value1, Point value2)
        {
            return value1 - value2;
        }

        /// <summary>
        /// Creates a new <see cref="Point"/> that contains subtraction of on <see cref="Point"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/>.</param>
        /// <param name="value2">Source <see cref="int"/>.</param>
        /// <returns>The result of the vector subtraction.</returns>
        public static Point Subtract(Point value1, int value2)
        {
            return new Point(value1.X - value2, value1.Y - value2);
        }

        /// <summary>
        /// Creates a new <see cref="Point"/> that contains subtraction of on <see cref="Point"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="int"/>.</param>
        /// <param name="value2">Source <see cref="Point"/>.</param>
        /// <returns>The result of the vector subtraction.</returns>
        public static Point Subtract(int value1, Point value2)
        {
            return new Point(value1 - value2.X, value1 - value2.Y);
        }

        /// <summary>
        /// Creates a new <see cref="Point"/> that contains a multiplication of two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/>.</param>
        /// <param name="value2">Source <see cref="Point"/>.</param>
        /// <returns>The result of the vector multiplication.</returns>
        public static Point Multiply(Point value1, Point value2)
        {
            return value1 * value2;
        }

        /// <summary>
        /// Creates a new <see cref="Point"/> that contains a multiplication of <see cref="Point"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>The result of the vector multiplication with a scalar.</returns>
        public static Point Multiply(Point value1, float scaleFactor)
        {
            return value1 * scaleFactor;
        }

        /// <summary>
        /// Creates a new <see cref="PointF"/> that contains a multiplication of <see cref="Point"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="PointF"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>The result of the vector multiplication with a scalar.</returns>
        public static Point Multiply(float value1, Point scaleFactor)
        {
            return value1 * scaleFactor;
        }

        /// <summary>
        /// Divides the components of a <see cref="Point"/> by the components of another <see cref="Point"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/>.</param>
        /// <param name="value2">Divisor <see cref="Point"/>.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static Point Divide(Point value1, Point value2)
        {
            return new Point(value1.X / value2.X, value1.Y / value2.Y);
        }

        /// <summary>
        /// Divides the components of a <see cref="Point"/> by the components of another <see cref="Point"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/>.</param>
        /// <param name="value2">Divisor <see cref="float"/>.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static Point Divide(Point value1, float value2)
        {
            return new Point((int)(value1.X / value2), (int)(value1.Y / value2));
        }

        /// <summary>
        /// Divides the components of a <see cref="Point"/> by the components of another <see cref="Point"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Point"/>.</param>
        /// <param name="value2">Divisor <see cref="float"/>.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static Point Divide(float value1, Point value2)
        {
            return new Point((int)(value1 / value2.X), (int)(value1 / value2.Y));
        }

        /// <summary>
        /// Creates a new <see cref="Point"/> that contains the specified vector inversion.
        /// </summary>
        /// <param name="value">Source <see cref="Point"/>.</param>
        /// <returns>The result of the vector inversion.</returns>
        public static Point Negate(Point value)
        {
            return new Point(-value.X, -value.Y);
        }

        /// <summary>
        /// Creates a new <see cref="Point"/> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Point"/> with maximal values from the two vectors.</returns>
        public static Point Max(Point value1, Point value2)
        {
            return new Point(value1.X > value2.X ? value1.X : value2.X,
                               value1.Y > value2.Y ? value1.Y : value2.Y);
        }

        /// <summary>
        /// Creates a new <see cref="Point"/> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Point"/> with minimal values from the two vectors.</returns>
        public static Point Min(Point value1, Point value2)
        {
            return new Point(value1.X < value2.X ? value1.X : value2.X,
                               value1.Y < value2.Y ? value1.Y : value2.Y);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Point) && Equals((Point)obj);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Point"/>.
        /// </summary>
        /// <param name="other">The <see cref="Point"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(Point other)
        {
            return ((X == other.X) && (Y == other.Y));
        }

        /// <summary>
        /// Gets the hash code of this <see cref="Point"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="Point"/>.</returns>
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
        /// Returns a <see cref="String"/> representation of this <see cref="Point"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>]}
        /// </summary>
        /// <returns><see cref="String"/> representation of this <see cref="Point"/>.</returns>
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
        /// Gets a <see cref="PointF"/> representation for this object.
        /// </summary>
        /// <returns>A <see cref="PointF"/> representation for this object.</returns>
        public PointF ToPointF()
        {
            return new PointF(X, Y);
        }

        #endregion
    }
}


