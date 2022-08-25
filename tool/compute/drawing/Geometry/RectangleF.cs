// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Numerics;
using System.Collections.Generic;

namespace compute.drawing
{
    /// <summary>
    /// Describes a 2D-rectangle. 
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct RectangleF : IEquatable<RectangleF>, IGeometry
    {
        #region Private Fields

        private static RectangleF emptyRectangle = new RectangleF();

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of the top-left corner of this <see cref="Rectangle"/>.
        /// </summary>
        [DataMember]
        public float X;

        /// <summary>
        /// The y coordinate of the top-left corner of this <see cref="Rectangle"/>.
        /// </summary>
        [DataMember]
        public float Y;

        /// <summary>
        /// The width of this <see cref="Rectangle"/>.
        /// </summary>
        [DataMember]
        public float Width;

        /// <summary>
        /// The height of this <see cref="Rectangle"/>.
        /// </summary>
        [DataMember]
        public float Height;

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns a <see cref="Rectangle"/> with X=0, Y=0, Width=0, Height=0.
        /// </summary>
        public static RectangleF Empty
        {
            get { return emptyRectangle; }
        }

        /// <summary>
        /// Returns the x coordinate of the left edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Left
        {
            get { return this.X; }
        }

        /// <summary>
        /// Returns the x coordinate of the right edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Right
        {
            get { return (this.X + this.Width); }
        }

        /// <summary>
        /// Returns the y coordinate of the top edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Top
        {
            get { return this.Y; }
        }

        /// <summary>
        /// Returns the y coordinate of the bottom edge of this <see cref="Rectangle"/>.
        /// </summary>
        public float Bottom
        {
            get { return (this.Y + this.Height); }
        }

        /// <summary>
        /// Whether or not this <see cref="Rectangle"/> has a <see cref="Width"/> and
        /// <see cref="Height"/> of 0, and a <see cref="Location"/> of (0, 0).
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Width == 0 || Height == 0; // ((((this.Width == 0) && (this.Height == 0)) && (this.X == 0)) && (this.Y == 0));
            }
        }

        /// <summary>
        /// The top-left coordinates of this <see cref="Rectangle"/>.
        /// </summary>
        public PointF Location
        {
            get
            {
                return new PointF(this.X, this.Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// The width-height coordinates of this <see cref="Rectangle"/>.
        /// </summary>
        public PointF Size
        {
            get
            {
                return new PointF(this.Width,this.Height);
            }
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        /// <summary>
        /// A <see cref="Point"/> located in the center of this <see cref="Rectangle"/>.
        /// </summary>
        /// <remarks>
        /// If <see cref="Width"/> or <see cref="Height"/> is an odd number,
        /// the center point will be rounded down.
        /// </remarks>
        public PointF Center
        {
            get
            {
                return new PointF(this.X + (this.Width / 2), this.Y + (this.Height / 2));
            }
        }

        public PointF LeftTop
        {
            get { return new PointF(Left, Top); }
        }

        public PointF RightTop
        {
            get { return new PointF(Right, Top); }
        }

        public PointF RightBottom
        {
            get { return new PointF(Right, Bottom); }
        }

        public PointF LeftBottom
        {
            get { return new PointF(Left, Bottom); }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.X, "  ",
                    this.Y, "  ",
                    this.Width, "  ",
                    this.Height
                    );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="Rectangle"/> struct, with the specified
        /// position, width, and height.
        /// </summary>
        /// <param name="x">The x coordinate of the top-left corner of the created <see cref="Rectangle"/>.</param>
        /// <param name="y">The y coordinate of the top-left corner of the created <see cref="Rectangle"/>.</param>
        /// <param name="width">The width of the created <see cref="Rectangle"/>.</param>
        /// <param name="height">The height of the created <see cref="Rectangle"/>.</param>
        public RectangleF(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Rectangle"/> struct, with the specified
        /// location and size.
        /// </summary>
        /// <param name="location">The x and y coordinates of the top-left corner of the created <see cref="Rectangle"/>.</param>
        /// <param name="size">The width and height of the created <see cref="Rectangle"/>.</param>
        public RectangleF(PointF location, PointF size)
        {
            this.X = location.X + (size.X < 0 ? size.X : 0);
            this.Y = location.Y + (size.Y < 0 ? size.Y : 0);
            this.Width = Math.Abs(size.X);
            this.Height = Math.Abs(size.Y);
        }

        public RectangleF(PointF center, float width, float height)
        {
            this.X = center.X - width / 2;
            this.Y = center.Y - height / 2;
            this.Width = width;
            this.Height = width;
        }


        #endregion

        #region Operators

        /// <summary>
        /// Compares whether two <see cref="Rectangle"/> instances are equal.
        /// </summary>
        /// <param name="a"><see cref="Rectangle"/> instance on the left of the equal sign.</param>
        /// <param name="b"><see cref="Rectangle"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(RectangleF a, RectangleF b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height));
        }

        /// <summary>
        /// Compares whether two <see cref="Rectangle"/> instances are not equal.
        /// </summary>
        /// <param name="a"><see cref="Rectangle"/> instance on the left of the not equal sign.</param>
        /// <param name="b"><see cref="Rectangle"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(RectangleF a, RectangleF b)
        {
            return !(a == b);
        }

        #endregion

        #region Public Methods
       
        /// <summary>
        /// Gets whether or not the provided coordinates lie within the bounds of this <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="x">The x coordinate of the point to check for containment.</param>
        /// <param name="y">The y coordinate of the point to check for containment.</param>
        /// <returns><c>true</c> if the provided coordinates lie inside this <see cref="Rectangle"/>; <c>false</c> otherwise.</returns>
		public bool Contains(int x, int y)
        {
            return ((((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y)) && (y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided coordinates lie within the bounds of this <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="x">The x coordinate of the point to check for containment.</param>
        /// <param name="y">The y coordinate of the point to check for containment.</param>
        /// <returns><c>true</c> if the provided coordinates lie inside this <see cref="Rectangle"/>; <c>false</c> otherwise.</returns>
        public bool Contains(float x, float y)
        {
            return ((((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y)) && (y < (this.Y + this.Height)));
        }
		
        /// <summary>
        /// Gets whether or not the provided <see cref="Point"/> lies within the bounds of this <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="Rectangle"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="Point"/> lies inside this <see cref="Rectangle"/>; <c>false</c> otherwise.</returns>
        public bool Contains(PointF value)
        {
            return ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Point"/> lies within the bounds of this <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="Rectangle"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="Point"/> lies inside this <see cref="Rectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(ref PointF value, out bool result)
        {
            result = ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Vector2"/> lies within the bounds of this <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="Rectangle"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="Vector2"/> lies inside this <see cref="Rectangle"/>; <c>false</c> otherwise.</returns>
        public bool Contains(Vector2 value)
        {
            return ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Vector2"/> lies within the bounds of this <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="Rectangle"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="Vector2"/> lies inside this <see cref="Rectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(ref Vector2 value, out bool result)
        {
            result = ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="RectangleF"/> lies within the bounds of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The <see cref="RectangleF"/> to check for inclusion in this <see cref="RectangleF"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="RectangleF"/>'s bounds lie entirely inside this <see cref="RectangleF"/>; <c>false</c> otherwise.</returns>
        public bool Contains(RectangleF value)
        {
            return ((((this.X <= value.X) && ((value.X + value.Width) <= (this.X + this.Width))) && (this.Y <= value.Y)) && ((value.Y + value.Height) <= (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="RectangleF"/> lies within the bounds of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The <see cref="RectangleF"/> to check for inclusion in this <see cref="RectangleF"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="RectangleF"/>'s bounds lie entirely inside this <see cref="RectangleF"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(ref RectangleF value,out bool result)
        {
            result = ((((this.X <= value.X) && ((value.X + value.Width) <= (this.X + this.Width))) && (this.Y <= value.Y)) && ((value.Y + value.Height) <= (this.Y + this.Height)));
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is RectangleF) && this == ((RectangleF)obj);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="other">The <see cref="RectangleF"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(RectangleF other)
        {
            return this == other;
        }

        /// <summary>
        /// Gets the hash code of this <see cref="RectangleF"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="RectangleF"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Width.GetHashCode();
                hash = hash * 23 + Height.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Adjusts the edges of this <see cref="RectangleF"/> by specified horizontal and vertical amounts. 
        /// </summary>
        /// <param name="horizontalAmount">Value to adjust the left and right edges.</param>
        /// <param name="verticalAmount">Value to adjust the top and bottom edges.</param>
        public void Inflate(int horizontalAmount, int verticalAmount)
        {
            X -= horizontalAmount;
            Y -= verticalAmount;
            Width += horizontalAmount * 2;
            Height += verticalAmount * 2;
        }

        /// <summary>
        /// Adjusts the edges of this <see cref="RectangleF"/> by specified horizontal and vertical amounts. 
        /// </summary>
        /// <param name="horizontalAmount">Value to adjust the left and right edges.</param>
        /// <param name="verticalAmount">Value to adjust the top and bottom edges.</param>
        public void Inflate(float horizontalAmount, float verticalAmount)
        {
            X -= (int)horizontalAmount;
            Y -= (int)verticalAmount;
            Width += (int)horizontalAmount * 2;
            Height += (int)verticalAmount * 2;
        }

        /// <summary>
        /// Gets whether or not the other <see cref="RectangleF"/> intersects with this rectangle.
        /// </summary>
        /// <param name="value">The other rectangle for testing.</param>
        /// <returns><c>true</c> if other <see cref="RectangleF"/> intersects with this rectangle; <c>false</c> otherwise.</returns>
        public bool Intersects(RectangleF value)
        {
            return value.Left < Right &&
                   Left < value.Right &&
                   value.Top < Bottom &&
                   Top < value.Bottom;
        }


        /// <summary>
        /// Gets whether or not the other <see cref="RectangleF"/> intersects with this rectangle.
        /// </summary>
        /// <param name="value">The other rectangle for testing.</param>
        /// <param name="result"><c>true</c> if other <see cref="RectangleF"/> intersects with this rectangle; <c>false</c> otherwise. As an output parameter.</param>
        public void Intersects(ref RectangleF value, out bool result)
        {
            result = value.Left < Right &&
                     Left < value.Right &&
                     value.Top < Bottom &&
                     Top < value.Bottom;
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that contains overlapping region of two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="RectangleF"/>.</param>
        /// <param name="value2">The second <see cref="RectangleF"/>.</param>
        /// <returns>Overlapping region of the two rectangles.</returns>
        public static RectangleF Intersect(RectangleF value1, RectangleF value2)
        {
            RectangleF rectangle;
            Intersect(ref value1, ref value2, out rectangle);
            return rectangle;
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that contains overlapping region of two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="RectangleF"/>.</param>
        /// <param name="value2">The second <see cref="RectangleF"/>.</param>
        /// <param name="result">Overlapping region of the two rectangles as an output parameter.</param>
        public static void Intersect(ref RectangleF value1, ref RectangleF value2, out RectangleF result)
        {
            if (value1.Intersects(value2))
            {
                var right_side = Math.Min(value1.X + value1.Width, value2.X + value2.Width);
                var left_side = Math.Max(value1.X, value2.X);
                var top_side = Math.Max(value1.Y, value2.Y);
                var bottom_side = Math.Min(value1.Y + value1.Height, value2.Y + value2.Height);
                result = new RectangleF(left_side, top_side, right_side - left_side, bottom_side - top_side);
            }
            else
            {
                result = new RectangleF(0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="offsetX">The x coordinate to add to this <see cref="RectangleF"/>.</param>
        /// <param name="offsetY">The y coordinate to add to this <see cref="RectangleF"/>.</param>
        public void Offset(int offsetX, int offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="offsetX">The x coordinate to add to this <see cref="RectangleF"/>.</param>
        /// <param name="offsetY">The y coordinate to add to this <see cref="RectangleF"/>.</param>
        public void Offset(float offsetX, float offsetY)
        {
            X += (int)offsetX;
            Y += (int)offsetY;
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="amount">The x and y components to add to this <see cref="RectangleF"/>.</param>
        public void Offset(Point amount)
        {
            X += amount.X;
            Y += amount.Y;
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="amount">The x and y components to add to this <see cref="RectangleF"/>.</param>
        public void Offset(Vector2 amount)
        {
            X += (int)amount.X;
            Y += (int)amount.Y;
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="Rectangle"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>] Width:[<see cref="Width"/>] Height:[<see cref="Height"/>]}
        /// </summary>
        /// <returns><see cref="String"/> representation of this <see cref="Rectangle"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + " Width:" + Width + " Height:" + Height + "}";
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="RectangleF"/>.</param>
        /// <param name="value2">The second <see cref="RectangleF"/>.</param>
        /// <returns>The union of the two rectangles.</returns>
        public static RectangleF Union(RectangleF value1, RectangleF value2)
        {
            var x = Math.Min(value1.X, value2.X);
            var y = Math.Min(value1.Y, value2.Y);
            return new RectangleF(x, y,
                                 Math.Max(value1.Right, value2.Right) - x,
                                     Math.Max(value1.Bottom, value2.Bottom) - y);
        }

        /// <summary>
        /// Creates a new <see cref="RectangleF"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="RectangleF"/>.</param>
        /// <param name="value2">The second <see cref="RectangleF"/>.</param>
        /// <param name="result">The union of the two rectangles as an output parameter.</param>
        public static void Union(ref RectangleF value1, ref RectangleF value2, out RectangleF result)
        {
            result.X = Math.Min(value1.X, value2.X);
            result.Y = Math.Min(value1.Y, value2.Y);
            result.Width = Math.Max(value1.Right, value2.Right) - result.X;
            result.Height = Math.Max(value1.Bottom, value2.Bottom) - result.Y;
        }

        public IEnumerable<GeometryData> GetGeometryDatas()
        {
            var points = new GeometryPoint[4];
            points[0] = new GeometryPoint(0, LeftTop, 0);
            points[1] = new GeometryPoint(1, RightTop, 0);
            points[2] = new GeometryPoint(2, RightBottom, 0);
            points[3] = new GeometryPoint(3, LeftBottom, 0);

            yield return new GeometryData(GeometryDataType.Vertex, points);
        }

        #endregion
    }
}
