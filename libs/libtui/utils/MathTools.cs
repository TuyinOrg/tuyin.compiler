// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using libtui.drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace libtui.utils
{
    /// <summary>
    /// Contains commonly used precalculated values and mathematical operations.
    /// </summary>
    static class MathTools
    {
        /// <summary>
        /// Represents the mathematical constant e(2.71828175).
        /// </summary>
        public const float E = (float)Math.E;

        /// <summary>
        /// Represents the log base ten of e(0.4342945).
        /// </summary>
        public const float Log10E = 0.4342945f;

        /// <summary>
        /// Represents the log base two of e(1.442695).
        /// </summary>
        public const float Log2E = 1.442695f;

        /// <summary>
        /// Represents the value of pi(3.14159274).
        /// </summary>
        public const float Pi = (float)Math.PI;

        /// <summary>
        /// Represents the value of pi divided by two(1.57079637).
        /// </summary>
        public const float PiOver2 = (float)(Math.PI / 2.0);

        /// <summary>
        /// Represents the value of pi divided by four(0.7853982).
        /// </summary>
        public const float PiOver4 = (float)(Math.PI / 4.0);

        /// <summary>
        /// Represents the value of pi times two(6.28318548).
        /// </summary>
        public const float TwoPi = (float)(Math.PI * 2.0);

        /// <summary>
        /// Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.
        /// </summary>
        /// <param name="value1">The coordinate on one axis of vertex 1 of the defining triangle.</param>
        /// <param name="value2">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
        /// <param name="value3">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
        /// <param name="amount1">The normalized barycentric (areal) coordinate b2, equal to the weighting factor for vertex 2, the coordinate of which is specified in value2.</param>
        /// <param name="amount2">The normalized barycentric (areal) coordinate b3, equal to the weighting factor for vertex 3, the coordinate of which is specified in value3.</param>
        /// <returns>Cartesian coordinate of the specified point with respect to the axis being used.</returns>
        public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
        {
            return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="value1">The first position in the interpolation.</param>
        /// <param name="value2">The second position in the interpolation.</param>
        /// <param name="value3">The third position in the interpolation.</param>
        /// <param name="value4">The fourth position in the interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
        {
            // Using formula from http://www.mvps.org/directx/articles/catmull/
            // Internally using doubles not to lose precission
            double amountSquared = amount * amount;
            double amountCubed = amountSquared * amount;
            return (float)(0.5 * (2.0 * value2 +
                (value3 - value1) * amount +
                (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
                (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>   
        public static float Clamp(float value, float min, float max)
        {
            // First we check to see if we're greater than the max
            value = (value > max) ? max : value;

            // Then we check to see if we're less than the min.
            value = (value < min) ? min : value;

            // There's no check to see if min > max.
            return value;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value, int min, int max)
        {
            value = (value > max) ? max : value;
            value = (value < min) ? min : value;
            return value;
        }

        /// <summary>
        /// Calculates the absolute value of the difference of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>Distance between the two values.</returns>
        public static float Distance(float value1, float value2)
        {
            return Math.Abs(value1 - value2);
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        /// <param name="value1">Source position.</param>
        /// <param name="tangent1">Source tangent.</param>
        /// <param name="value2">Source position.</param>
        /// <param name="tangent2">Source tangent.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of the Hermite spline interpolation.</returns>
        public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            // All transformed to double not to lose precission
            // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
            double v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
            double sCubed = s * s * s;
            double sSquared = s * s;

            if (amount == 0f)
                result = value1;
            else if (amount == 1f)
                result = value2;
            else
                result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed +
                    (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared +
                    t1 * s +
                    v1;
            return (float)result;
        }


        /// <summary>
        /// Linearly interpolates between two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Destination value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
        /// <returns>Interpolated value.</returns> 
        /// <remarks>This method performs the linear interpolation based on the following formula:
        /// <code>value1 + (value2 - value1) * amount</code>.
        /// Passing amount a value of 0 will cause value1 to be returned, a value of 1 will cause value2 to be returned.
        /// See <see cref="MathTools.LerpPrecise"/> for a less efficient version with more precision around edge cases.
        /// </remarks>
        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }


        /// <summary>
        /// Linearly interpolates between two values.
        /// This method is a less efficient, more precise version of <see cref="MathTools.Lerp"/>.
        /// See remarks for more info.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Destination value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
        /// <returns>Interpolated value.</returns>
        /// <remarks>This method performs the linear interpolation based on the following formula:
        /// <code>((1 - amount) * value1) + (value2 * amount)</code>.
        /// Passing amount a value of 0 will cause value1 to be returned, a value of 1 will cause value2 to be returned.
        /// This method does not have the floating point precision issue that <see cref="MathTools.Lerp"/> has.
        /// i.e. If there is a big gap between value1 and value2 in magnitude (e.g. value1=10000000000000000, value2=1),
        /// right at the edge of the interpolation range (amount=1), <see cref="MathTools.Lerp"/> will return 0 (whereas it should return 1).
        /// This also holds for value1=10^17, value2=10; value1=10^18,value2=10^2... so on.
        /// For an in depth explanation of the issue, see below references:
        /// Relevant Wikipedia Article: https://en.wikipedia.org/wiki/Linear_interpolation#Programming_language_support
        /// Relevant StackOverflow Answer: http://stackoverflow.com/questions/4353525/floating-point-linear-interpolation#answer-23716956
        /// </remarks>
        public static float LerpPrecise(float value1, float value2, float amount)
        {
            return ((1 - amount) * value1) + (value2 * amount);
        }

        /// <summary>
        /// Returns the greater of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The greater value.</returns>
        public static float Max(float value1, float value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        /// <summary>
        /// Returns the greater of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The greater value.</returns>
        public static int Max(int value1, int value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        /// <summary>
        /// Returns the lesser of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The lesser value.</returns>
        public static float Min(float value1, float value2)
        {
            return value1 < value2 ? value1 : value2;
        }

        /// <summary>
        /// Returns the lesser of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The lesser value.</returns>
        public static int Min(int value1, int value2)
        {
            return value1 < value2 ? value1 : value2;
        }

        /// <summary>
        /// Interpolates between two values using a cubic equation.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Interpolated value.</returns>
        public static float SmoothStep(float value1, float value2, float amount)
        {
            // It is expected that 0 < amount < 1
            // If amount < 0, return value1
            // If amount > 1, return value2
            float result = MathTools.Clamp(amount, 0f, 1f);
            result = MathTools.Hermite(value1, 0f, value2, 0f, result);

            return result;
        }

        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>The angle in degrees.</returns>
        /// <remarks>
        /// This method uses double precission internally,
        /// though it returns single float
        /// Factor = 180 / pi
        /// </remarks>
        public static float ToDegrees(float radians)
        {
            return (float)(radians * 57.295779513082320876798154814105);
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The angle in radians.</returns>
        /// <remarks>
        /// This method uses double precission internally,
        /// though it returns single float
        /// Factor = pi / 180
        /// </remarks>
        public static float ToRadians(float degrees)
        {
            return (float)(degrees * 0.017453292519943295769236907684886);
        }

        /// <summary>
        /// Reduces a given angle to a value between π and -π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        public static float WrapAngle(float angle)
        {
            if ((angle > -Pi) && (angle <= Pi))
                return angle;
            angle %= TwoPi;
            if (angle <= -Pi)
                return angle + TwoPi;
            if (angle > Pi)
                return angle - TwoPi;
            return angle;
        }

        /// <summary>
        /// Determines if value is powered by two.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns><c>true</c> if <c>value</c> is powered by two; otherwise <c>false</c>.</returns>
        public static bool IsPowerOfTwo(int value)
        {
            return (value > 0) && ((value & (value - 1)) == 0);
        }


        private static Random mRandom = new Random();

        public static int Random(int max)
        {
            return Random(0, max);
        }

        public static int Random(int min, int max)
        {
            return mRandom.Next(min, max);
        }

        public static float Random(float max)
        {
            return Random(0f, max);
        }

        public static float Random(float min, float max)
        {
            return (float)mRandom.NextDouble() * (max - min) + min;
        }

        // Determine whether two vectors v1 and v2 point to the same direction
        // v1 = Cross(AB, AC)
        // v2 = Cross(AB, AP)
        private static bool SameSide(PointF A, PointF B, PointF C, PointF P)
        {
            PointF AB = B - A;
            PointF AC = C - A;
            PointF AP = P - A;

            PointF v1 = PointF.Cross(AB, AC);
            PointF v2 = PointF.Cross(AB, AP);

            // v1 and v2 should point to the same direction
            return PointF.Dot(v1, v2) >= 0;
        }

        public static bool PointinTriangle(PointF A, PointF B, PointF C, PointF P)
        {
            return SameSide(A, B, C, P) &&
                  SameSide(B, C, A, P) &&
                  SameSide(C, A, B, P);
        }

        public static bool PointInLine(PointF pf, PointF p1, PointF p2, double range)
        {

            //range 判断的的误差，不需要误差则赋值0
            double cross = (p2.X - p1.X) * (pf.X - p1.X) + (p2.Y - p1.Y) * (pf.Y - p1.Y);
            if (cross <= 0) return false;
            double d2 = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
            if (cross >= d2) return false;

            double r = cross / d2;
            double px = p1.X + (p2.X - p1.X) * r;
            double py = p1.Y + (p2.Y - p1.Y) * r;
            return Math.Sqrt((pf.X - px) * (pf.X - px) + (py - pf.Y) * (py - pf.Y)) < range;
        }

        public static bool Approximately(float a, float b)
        {
            return Math.Abs(b - a) < Max(0.000001f * Max(Math.Abs(a), Math.Abs(b)), Settings.Epsilon * 8);
        }

        public static PointF Barycentric(PointF A, PointF B, PointF C, PointF P)
        {
            PointF v0 = C - A;
            PointF v1 = B - A;
            PointF v2 = P - A;


            var dot00 = PointF.Dot(v0, v0);
            var dot01 = PointF.Dot(v0, v1);
            var dot02 = PointF.Dot(v0, v2);
            var dot11 = PointF.Dot(v1, v1);
            var dot12 = PointF.Dot(v1, v2);

            float inverDeno = 1 / (dot00 * dot11 - dot01 * dot01);

            float u = (dot11 * dot02 - dot01 * dot12) * inverDeno;
            /*
            if (u < 0 || u > 1) // if u out of range, return directly
            {
                return false;
            }
            */
            float v = (dot00 * dot12 - dot01 * dot02) * inverDeno;
            /*
            if (v < 0 || v > 1) // if v out of range, return directly
            {
                return false;
            }
            */
            return new PointF(u, v);
        }

        /// <summary>
        /// 以中心点旋转Angle角度
        /// </summary>
        /// <param name="center">中心点</param>
        /// <param name="point">待旋转的点</param>
        /// <param name="angle">旋转角度（弧度）</param>
        public static Point Rotate(Point center, Point point, double angle)
        {
            double angleHude = -angle * Math.PI / 180;/*角度变成弧度*/
            double x1 = (point.X - center.X) * Math.Cos(angleHude) + (point.Y - center.Y) * Math.Sin(angleHude) + center.X;
            double y1 = -(point.X - center.X) * Math.Sin(angleHude) + (point.Y - center.Y) * Math.Cos(angleHude) + center.Y;
            return new Point((int)x1, (int)y1);
        }

        /// <summary>
        /// 以中心点旋转Angle角度
        /// </summary>
        /// <param name="center">中心点</param>
        /// <param name="point">待旋转的点</param>
        /// <param name="angle">旋转角度（弧度）</param>
        public static PointF Rotate(PointF center, PointF point, double angle)
        {
            double angleHude = -angle * Math.PI / 180;/*角度变成弧度*/
            double x1 = (point.X - center.X) * Math.Cos(angleHude) + (point.Y - center.Y) * Math.Sin(angleHude) + center.X;
            double y1 = -(point.X - center.X) * Math.Sin(angleHude) + (point.Y - center.Y) * Math.Cos(angleHude) + center.Y;
            return new PointF((float)x1, (float)y1);
        }

        public static Point Translate(Point points3D, Point oldOrigin, Point newOrigin)
        {
            //Moves a 3D point based on a moved reference point
            Point difference = new Point(newOrigin.X - oldOrigin.X, newOrigin.Y - oldOrigin.Y);
            return new Point(points3D.X + difference.X, points3D.Y + difference.Y);
        }

        public static Point[] Translate(Point[] points3D, Point oldOrigin, Point newOrigin)
        {
            for (int i = 0; i < points3D.Length; i++)
            {
                points3D[i] = Translate(points3D[i], oldOrigin, newOrigin);
            }
            return points3D;
        }

        public static PointF Translate(PointF points3D, PointF oldOrigin, PointF newOrigin)
        {
            //Moves a 3D point based on a moved reference point
            PointF difference = new PointF(newOrigin.X - oldOrigin.X, newOrigin.Y - oldOrigin.Y);
            return new PointF(points3D.X + difference.X, points3D.Y + difference.Y);
        }

        public static PointF[] Translate(PointF[] points3D, PointF oldOrigin, PointF newOrigin)
        {
            for (int i = 0; i < points3D.Length; i++)
            {
                points3D[i] = Translate(points3D[i], oldOrigin, newOrigin);
            }
            return points3D;
        }

        public static Point FindCentroid(IList<Point> pts)
        {
            var x = 0;
            var y = 0;
            for (var i = 0; i < pts.Count; i++)
            {
                x = x + pts[i].X;
                y = y + pts[i].Y;
            }

            x = x / pts.Count;
            y = y / pts.Count;

            return new Point(x, y);
        }

        public static PointF FindCentroid(IList<PointF> pts)
        {
            var x = 0f;
            var y = 0f;
            for (var i = 0; i < pts.Count; i++)
            {
                x = x + pts[i].X;
                y = y + pts[i].Y;
            }

            x = x / pts.Count;
            y = y / pts.Count;

            return new PointF(x, y);
        }

        public static double GetArea(IList<Point> points)
        {
            // Initialize area
            double area = 0.0;

            // Calculate value of shoelace formula
            var n = points.Count;
            int j = n - 1;

            for (int i = 0; i < n; i++)
            {
                area += (points[j].X + points[i].X) * (points[j].Y - points[i].Y);

                // j is previous vertex to i
                j = i;
            }

            // Return absolute value
            return Math.Abs(area / 2.0);
        }

        public static double GetArea(IList<PointF> points)
        {
            // Initialize area
            double area = 0.0;

            // Calculate value of shoelace formula
            var n = points.Count;
            int j = n - 1;

            for (int i = 0; i < n; i++)
            {
                area += (points[j].X + points[i].X) * (points[j].Y - points[i].Y);

                // j is previous vertex to i
                j = i;
            }

            // Return absolute value
            return Math.Abs(area / 2.0);
        }

        /// <summary>
        /// 从俩点间获得旋转角度
        /// </summary>
        public static float GetAngle(Point p1, Point p2)
        {
            float xDiff = p2.X - p1.X;
            float yDiff = p2.Y - p1.Y;

            if (xDiff == 0 && yDiff == 0) return 0;

            return (float)(180 - (Math.Atan2(xDiff, yDiff) * 180.0 / Math.PI)) % 360;
        }

        /// <summary>
        /// 从俩点间获得旋转角度
        /// </summary>
        public static float GetAngle(PointF p1, PointF p2)
        {
            float xDiff = p2.X - p1.X;
            float yDiff = p2.Y - p1.Y;

            if (xDiff == 0 && yDiff == 0) return 0;

            return (float)(180 - (Math.Atan2(xDiff, yDiff) * 180.0 / Math.PI)) % 360;
        }

        /// <summary>
        /// 根据余弦定理求两个线段夹角
        /// </summary>
        /// <param name="o">端点</param>
        /// <param name="s">start点</param>
        /// <param name="e">end点</param>
        /// <returns></returns>
        public static double GetIncludedAngle(PointF o, PointF s, PointF e)
        {
            double cosfi = 0, fi = 0, norm = 0;
            double dsx = s.X - o.X;
            double dsy = s.Y - o.Y;
            double dex = e.X - o.X;
            double dey = e.Y - o.Y;

            cosfi = dsx * dex + dsy * dey;
            norm = (dsx * dsx + dsy * dsy) * (dex * dex + dey * dey);
            cosfi /= Math.Sqrt(norm);

            if (cosfi >= 1.0) return 0;
            if (cosfi <= -1.0) return Math.PI;
            fi = Math.Acos(cosfi);

            if (180 * fi / Math.PI < 180)
            {
                return 180 * fi / Math.PI;
            }
            else
            {
                return 360 - 180 * fi / Math.PI;
            }
        }

        public static double GetIncludedAngle(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            //
            // calculate the angle between the line from p1 to p2
            // and the line from p3 to p4
            //
            double x1 = p1.X - p2.X;
            double y1 = p1.Y - p2.Y;
            double x2 = p3.X - p4.X;
            double y2 = p3.Y - p4.Y;
            //
            double angle1, angle2, angle;
            //
            if (x1 != 0.0f)
                angle1 = Math.Atan(y1 / x1);
            else
                angle1 = Math.PI / 2.0; // 90 degrees
                                        //
            if (x2 != 0.0f)
                angle2 = Math.Atan(y2 / x2);
            else
                angle2 = Math.PI / 2.0; // 90 degrees
                                        //
            angle = Math.Abs(angle2 - angle1);
            angle = angle * 180.0 / Math.PI;    // convert to degrees ???
                                                //
            return angle;
        }

        /// <summary>
        /// 根据角度计算出弧度
        /// </summary>
        /// <param name="angle">角度值</param>
        /// <returns>弧度</returns>
        public static double GetRadians(double angle)
        {
            return angle * Math.PI / 180;
        }

        /// <summary>
        /// 将窗口坐标系中的坐标换算成游戏坐标系中的坐标
        /// </summary>
        public static PointF GetGameCoordinate(double angle, PointF p, double gridSize)
        {
            if (angle == 0)
            {
                return new PointF((int)(p.X / gridSize), (int)(p.Y / gridSize));
            }
            else
            {
                double radian = GetRadians(angle);
                return new PointF(
                    (int)((p.Y / (2 * Math.Cos(radian)) + p.X / (2 * Math.Sin(radian))) / gridSize),
                    (int)((p.Y / (2 * Math.Cos(radian)) - p.X / (2 * Math.Sin(radian))) / gridSize)
                );
            }
        }

        /// <summary>
        /// 同GetGameCoordinate,得到最精确值
        /// </summary>
        public static PointF GetAccurateGameCoordinate(double angle, PointF p, double gridSize)
        {
            if (angle == 0)
            {
                return new PointF((float)(p.X / gridSize), (float)(p.Y / gridSize));
            }
            else
            {
                double radian = GetRadians(angle);
                return new PointF(
                    (float)((p.Y / (2 * Math.Cos(radian)) + p.X / (2 * Math.Sin(radian))) / gridSize),
                    (float)((p.Y / (2 * Math.Cos(radian)) - p.X / (2 * Math.Sin(radian))) / gridSize)
                );
            }
        }

        /// <summary>
        /// 将游戏坐标系中的坐标换算成窗口坐标系中的坐标
        /// </summary>
        public static PointF GetWindowCoordinate(double angle, PointF p, double gridSize)
        {
            if (angle == 0)
            {
                return new PointF((float)(p.X * gridSize), (float)(p.Y * gridSize));
            }
            else
            {
                double radian = GetRadians(angle);
                return new PointF(
                    (float)((p.X - p.Y) * Math.Sin(radian) * gridSize),
                    (float)((p.X + p.Y) * Math.Cos(radian) * gridSize)
                );
            }
        }

        public static float GetDistance(float x1, float y1, float x2, float y2)
        {
            var a = x1 - x2;
            var b = y1 - y2;
            var distance = Math.Sqrt(a * a + b * b);
            return (float)distance;
        }

        public static float GetDistance(Point p1, Point p2)
        {
            double a = p1.X - p2.X;
            double b = p1.Y - p2.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return (float)distance;
        }

        /// <summary>
        /// 获得两点间距离
        /// </summary>
        public static float GetDistance(PointF p1, PointF p2)
        {
            double a = p1.X - p2.X;
            double b = p1.Y - p2.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return (float)distance;
        }

        /// <summary>
        /// 判断点是否在线上
        /// </summary>
        /// <returns></returns>
        public static bool GetPointIsInLine(PointF pf, PointF p1, PointF p2, double range)
        {
            double cross = (p2.X - p1.X) * (pf.X - p1.X) + (p2.Y - p1.Y) * (pf.Y - p1.Y);
            if (cross <= 0) return false;
            double d2 = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
            if (cross >= d2) return false;

            double r = cross / d2;
            double px = p1.X + (p2.X - p1.X) * r;
            double py = p1.Y + (p2.Y - p1.Y) * r;

            //判断距离是否小于误差
            return Math.Sqrt((pf.X - px) * (pf.X - px) + (py - pf.Y) * (py - pf.Y)) <= range;
        }

        /// <summary>
        /// 获得p1点对于p2点角度的延伸点
        /// </summary>
        public static Point GetExtendPoint(Point p1, Point p2, double length)
        {
            var rotation = GetAngle(p1, p2);
            var target = Rotate(p1, new Point(p1.X, (int)(p1.Y - length)), rotation);
            return target;
        }

        /// <summary>
        /// 获得p1点对于p2点角度的延伸点
        /// </summary>
        public static PointF GetExtendPoint(PointF p1, PointF p2, double length)
        {
            var rotation = GetAngle(p1, p2);
            var target = Rotate(p1, new PointF(p1.X, (float)(p1.Y - length)), rotation);
            return target;
        }

        public static double DistanceForPointToABLine(PointF p, PointF start, PointF end)//所在点到AB线段的垂线长度
        {
            var x = p.X;
            var y = p.Y;
            var x1 = start.X;
            var y1 = start.Y;
            var x2 = end.X;
            var y2 = end.Y;

            float reVal = 0f;
            bool retData = false;

            float cross = (x2 - x1) * (x - x1) + (y2 - y1) * (y - y1);
            if (cross <= 0)
            {
                reVal = (float)Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1));
                retData = true;
            }

            float d2 = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
            if (cross >= d2)
            {
                reVal = (float)Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
                retData = true;
            }

            if (!retData)
            {
                float r = cross / d2;
                float px = x1 + (x2 - x1) * r;
                float py = y1 + (y2 - y1) * r;
                reVal = (float)Math.Sqrt((x - px) * (x - px) + (py - y) * (py - y));
            }

            return reVal;

        }

        /// <summary>
        ///  点到线段最短距离的那条直线与线段的交点，{x=...,y=...}
        /// </summary>
        /// <param name="x">线段外的点的x坐标</param>
        /// <param name="y">线段外的点的y坐标</param>
        /// <param name="x1">线段顶点1的x坐标</param>
        /// <param name="y1">线段顶点1的y坐标</param>
        /// <param name="x2">线段顶点2的x坐标</param>
        /// <param name="y2">线段顶点2的y坐标</param>
        /// <returns></returns>
        public static PointF PointForPointToABLine(PointF p, PointF start, PointF end)
        {
            var x = p.X;
            var y = p.Y;
            var x1 = start.X;
            var y1 = start.Y;
            var x2 = end.X;
            var y2 = end.Y;

            PointF reVal = new PointF();
            // 直线方程的两点式转换成一般式
            // A = Y2 - Y1
            // B = X1 - X2
            // C = X2*Y1 - X1*Y2
            float a1 = y2 - y1;
            float b1 = x1 - x2;
            float c1 = x2 * y1 - x1 * y2;
            float x3, y3;
            if (a1 == 0)
            {
                // 线段与x轴平行
                reVal = new PointF(x, y1);
                x3 = x;
                y3 = y1;
            }
            else if (b1 == 0)
            {
                // 线段与y轴平行
                reVal = new PointF(x1, y);
                x3 = x1;
                y3 = y;
            }
            else
            {
                // 普通线段
                float k1 = -a1 / b1;
                float k2 = -1 / k1;
                float a2 = k2;
                float b2 = -1;
                float c2 = y - k2 * x;
                // 直线一般式和二元一次方程的一般式转换
                // 直线的一般式为 Ax+By+C=0
                // 二元一次方程的一般式为 Ax+By=C
                c1 = -c1;
                c2 = -c2;

                // 二元一次方程求解(Ax+By=C)
                // a=a1,b=b1,c=c1,d=a2,e=b2,f=c2;
                // X=(ce-bf)/(ae-bd)
                // Y=(af-cd)/(ae-bd)
                x3 = (c1 * b2 - b1 * c2) / (a1 * b2 - b1 * a2);
                y3 = (a1 * c2 - c1 * a2) / (a1 * b2 - b1 * a2);
            }
            // 点(x3,y3)作为点(x,y)到(x1,y1)和(x2,y2)组成的直线距离最近的点,那(x3,y3)是否在(x1,y1)和(x2,y2)的线段之内(包含(x1,y1)和(x2,y2))
            if (((x3 > x1) != (x3 > x2) || x3 == x1 || x3 == x2) && ((y3 > y1) != (y3 > y2) || y3 == y1 || y3 == y2))
            {
                // (x3,y3)在线段上
                reVal = new PointF(x3, y3);
            }
            else
            {
                // (x3,y3)在线段外
                float d1_quadratic = (x - x1) * (x - x1) + (y - y1) * (y - y1);
                float d2_quadratic = (x - x2) * (x - x2) + (y - y2) * (y - y2);
                if (d1_quadratic <= d2_quadratic)
                {
                    reVal = new PointF(x1, y1);
                }
                else
                {
                    reVal = new PointF(x2, y2);
                }
            }
            return reVal;
        }

        /// <summary>
        /// 获得2点间中心点
        /// </summary>
        public static PointF GetCenterPoint(PointF p1, PointF p2)
        {
            return GetExtendPoint(p1, p2, GetDistance(p1, p2) / 2);
        }

        /// <summary>
        /// Returns a positive number if c is to the left of the line going from a to b.
        /// </summary>
        /// <returns>Positive number if point is left, negative if point is right, 
        /// and 0 if points are collinear.</returns>
        public static float Area(PointF a, PointF b, PointF c)
        {
            return Area(ref a, ref b, ref c);
        }

        /// <summary>
        /// Returns a positive number if c is to the left of the line going from a to b.
        /// </summary>
        /// <returns>Positive number if point is left, negative if point is right, 
        /// and 0 if points are collinear.</returns>
        public static float Area(ref PointF a, ref PointF b, ref PointF c)
        {
            return a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y);
        }

        /// <summary>
        /// 计算三角形面积
        /// </summary>
        public static double TriangleArea(PointF p1, PointF p2, PointF p3)
        {
            //为三个边长赋初值
            var a = GetDistance(p1, p2);
            var b = GetDistance(p2, p3);
            var c = GetDistance(p3, p1);
            //计算半周长
            var p = (a + b + c) / 2;
            //计算面积
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public static float Cross(PointF a, PointF b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static void Cross(PointF a, PointF b, out float c)
        {
            c = a.X * b.Y - a.Y * b.X;
        }

        /// <summary>
        /// Determines if three vertices are collinear (ie. on a straight line)
        /// </summary>
        /// <param name="a">First vertex</param>
        /// <param name="b">Second vertex</param>
        /// <param name="c">Third vertex</param>
        /// <param name="tolerance">The tolerance</param>
        /// <returns></returns>
        public static bool IsCollinear(ref PointF a, ref PointF b, ref PointF c, float tolerance = 0)
        {
            return FloatInRange(Area(ref a, ref b, ref c), -tolerance, tolerance);
        }

        public static bool IsHollow(List<PointF> curveloopPoints)
        {
            //使用角度和判断凹凸性：凸多边形的内角和为（n-2）*180° 
            var num = curveloopPoints.Count;
            float angleSum = 0.0f;
            for (int i = 0; i < num; i++)
            {
                PointF e1;
                if (i == 0)
                {
                    e1 = curveloopPoints[num - 1] - curveloopPoints[i];
                }
                else
                {
                    e1 = curveloopPoints[i - 1] - curveloopPoints[i];
                }
                PointF e2;
                if (i == num - 1)
                {
                    e2 = curveloopPoints[0] - curveloopPoints[i];
                }
                else
                {
                    e2 = curveloopPoints[i + 1] - curveloopPoints[i];
                }
                //标准化
                //e1.Normalize(); e2.Normalize();
                //计算点乘
                float mdot = PointF.Dot(e1, e2);
                //计算夹角弧度
                float theta = (float)Math.Acos(mdot);
                //注意计算内角
                angleSum += theta;
            }
            //计算内角和 
            float convexAngleSum = (float)((num - 2)) * (float)Math.PI;
            //判断凹凸性 
            if (angleSum < (convexAngleSum - (num * 0.00001)))
            {
                return true;//是凹 
            }
            return false;//否则是凸 
        }

        public static Rectangle GetBoundingBox(IEnumerable<Point> points)
        {
            int left = int.MaxValue, right = int.MinValue, top = int.MaxValue, bottom = int.MinValue;
            foreach (var point in points)
            {
                var x1 = point.X;
                var y1 = point.Y;

                if (x1 < left) left = x1;
                if (x1 > right) right = x1;
                if (y1 < top) top = y1;
                if (y1 > bottom) bottom = y1;
            }
            return new Rectangle(left, top, right - left, bottom - top);
        }

        public static RectangleF GetBoundingBox(IEnumerable<PointF> points)
        {
            float left = float.MaxValue, right = float.MinValue, top = float.MaxValue, bottom = float.MinValue;
            foreach (var point in points)
            {
                var x1 = point.X;
                var y1 = point.Y;

                if (x1 < left) left = x1;
                if (x1 > right) right = x1;
                if (y1 < top) top = y1;
                if (y1 > bottom) bottom = y1;
            }
            return new RectangleF(left, top, right - left, bottom - top);
        }

        private static bool FloatInRange(float value, float min, float max)
        {
            return (value >= min && value <= max);
        }

        private static bool FloatEquals(float value1, float value2)
        {
            return Math.Abs(value1 - value2) <= Settings.Epsilon;
        }

        private static bool FloatEquals(float value1, float value2, float delta)
        {
            return FloatInRange(value1, value2 - delta, value2 + delta);
        }


        /// <summary>
        /// 获得小数位数
        /// </summary>
        public static int DecimalCount(double value)
        {
            var numStr = value.ToString();
            var decimalIndex = numStr.IndexOf('.');
            if (decimalIndex == -1)
                return 0;
            else
                return numStr.Length - decimalIndex - 1;
        }

        /// <summary>
        /// 获得最大公约数
        /// </summary>
        public static int CommonDivisor(params int[] nums)
        {
            return nums.Aggregate(CommonDivisor);
        }

        /// <summary>
        /// 获得最大公约数
        /// </summary>
        public static int CommonDivisor(int num1, int num2)
        {
            int tmp;
            if (num1 < num2)
            {
                tmp = num1; num1 = num2; num2 = tmp;
            }
            int a = num1; int b = num2;
            while (b != 0)
            {
                tmp = a % b;
                a = b;
                b = tmp;
            }

            return a;
        }

        /// <summary>
        /// 获得最小公倍数
        /// </summary>
        public static int CommonMultiple(int num1, int num2)
        {
            int tmp;
            if (num1 < num2)
            {
                tmp = num1; num1 = num2; num2 = tmp;
            }
            int a = num1; int b = num2;
            while (b != 0)
            {
                tmp = a % b;
                a = b;
                b = tmp;
            }

            return num1 * num2 / a;
        }

        #region 点循序

        /// <summary>
        /// 获得points点循序
        /// </summary>
        public static List<PointF> FindNonIntersecting(List<PointF> vertices)
        {
            // Find the north westmost point.
            PointF to_point = NWPoint(vertices);
            PointF first_point = to_point;
            PointF second_point = new PointF(
                float.PositiveInfinity,
                float.PositiveInfinity);

            // Pretend we are coming from a
            // point directly north of to_point.
            PointF from_point = new PointF(to_point.X, to_point.Y - 10);

            // Repeat until done.
            List<PointF> result = new List<PointF>();
            for (; ; )
            {
                // Find the next segment to visit.
                PointF next_from_point, next_to_point;
                FindNextSegment(vertices, from_point, to_point,
                    out next_from_point, out next_to_point);

                // If we are about to go from start_point
                // to second_point again, then we are done.
                // (The first time we cross this edge, second_point
                // is infinite.)
                if (PointsAreClose(next_from_point, first_point) &&
                    PointsAreClose(next_to_point, second_point))
                {
                    break;
                }

                // If this is the first move, save to_point.
                if (result.Count == 0) second_point = next_to_point;

                // Move to next_from_point.
                result.Add(next_from_point);
                from_point = next_from_point;
                to_point = next_to_point;
            }
            return result;
        }

        private static PointF NWPoint(List<PointF> vertices)
        {
            PointF best_point = vertices[0];
            float best_x = best_point.X;
            float best_y = best_point.Y;
            foreach (PointF test_point in vertices)
            {
                if ((test_point.X < best_x) ||
                    ((test_point.X == best_x) && (test_point.Y < best_y)))
                {
                    best_point = test_point;
                    best_x = best_point.X;
                    best_y = best_point.Y;
                }
            }
            return best_point;
        }

        private static void FindNextSegment(List<PointF> vertices,
           PointF from_point, PointF to_point,
           out PointF next_from_point, out PointF next_to_point)
        {
            // If this segment intersects an edge,
            // use the new segment that we find.
            if (FoundEdgeIntersection(vertices, from_point, to_point,
                out next_from_point, out next_to_point)) return;

            // This segment does not intersect an edge.
            // Find the best edge leading out of to_point.
            next_from_point = to_point;
            FindNextToPoint(vertices, from_point, to_point, out next_to_point);
            next_from_point = to_point;
        }

        private const float TINY = 0.0001f;

        private static bool PointsAreClose(PointF point1, PointF point2)
        {
 

            return (
                (Math.Abs(point1.X - point2.X) < TINY) &&
                (Math.Abs(point1.Y - point2.Y) < TINY));
        }

        private static bool FoundEdgeIntersection(List<PointF> vertices,
            PointF from_point, PointF to_point,
            out PointF new_from_point, out PointF new_to_point)
        {
            // See if this segment intersects an edge.
            PointF best_poi = new PointF(float.NegativeInfinity, float.NegativeInfinity);
            PointF best_from_point = best_poi;
            PointF best_to_point = best_poi;
            double best_angle = double.PositiveInfinity;
            float best_t1 = float.PositiveInfinity;
            for (int i = 0; i < vertices.Count; i++)
            {
                int j = (i + 1) % vertices.Count;
                PointF test_from_point = vertices[i];
                PointF test_to_point = vertices[j];

                // Don't compare this segment with others
                // that share its end points
                if ((from_point == test_from_point) ||
                    (from_point == test_to_point) ||
                    (to_point == test_to_point) ||
                    (to_point == test_from_point)) continue;

                // See if they intersect.
                bool lines_intersect, segments_intersect;
                float test_t1, t2;
                PointF intersection, close_point1, close_point2;
                FindIntersection(
                    from_point, to_point,
                    test_from_point, test_to_point,
                    out lines_intersect,
                    out segments_intersect,
                    out test_t1, out t2,
                    out intersection,
                    out close_point1,
                    out close_point2);

                if (segments_intersect)
                {
                    // See if this point of intersection is closer
                    // to from_point than the previous best intersection.
                    if ((test_t1 > TINY) && (test_t1 < best_t1 + TINY))
                    {
                        // See whether we should head toward
                        // vertices[i] or vertices[j].
                        // Get the angle:
                        // from_point --> intersection --> test_to_point.
                        double test_angle = VectorAngle(
                            from_point, intersection,
                            intersection, test_to_point);
                        if (test_angle > 0)
                        {
                            // The angle is positive so it turns to the right.
                            // Head toward test_from_point (vertices[i]) instead.
                            test_angle = -test_angle;
                            test_to_point = test_from_point;
                        }

                        // If we have found this POI before, see if this
                        // angle is better than the one we had previously.
                        bool use_new_intersection = false;
                        if (test_t1 < best_t1)
                            use_new_intersection = true;
                        else if (Math.Abs(test_t1 - best_t1) < TINY)
                        {
                            if (test_angle < best_angle)
                                use_new_intersection = true;
                        }

                        // See if we should use this intersection.
                        if (use_new_intersection)
                        {
                            // Save this intersection.
                            best_poi = intersection;
                            best_from_point = intersection;
                            best_to_point = test_to_point;
                            best_angle = test_angle;
                            best_t1 = test_t1;
                        }
                    } // if (test_t1 < best_t1 + TINY)
                } // if (segments_intersect)
            } // for (int i = 0; i < vertices.Count; i++)

            // Return the best value we found.
            new_from_point = best_from_point;
            new_to_point = best_to_point;
            return !float.IsInfinity(new_from_point.X);
        }

        private static void FindNextToPoint(List<PointF> vertices,
            PointF from_point, PointF to_point,
            out PointF next_to_point)
        {
            int best_j = -1;
            double best_angle = double.PositiveInfinity;

            // Find to_point.
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i] == to_point)
                {
                    // Consider moving from vertices[i] to
                    // vertices[i - 1] and vertices[i + 1] .
                    for (int incr = -1; incr <= 1; incr += 2)
                    {
                        int j = (i + incr + vertices.Count) % vertices.Count;
                        PointF test_to_point = vertices[j];
                        double test_angle = VectorAngle(
                            from_point, to_point,
                            to_point, test_to_point);

                        // Don't double back.
                        if (Math.Abs(test_angle - Math.PI) < 0.01)
                            test_angle = double.PositiveInfinity;
                        if (Math.Abs(test_angle - -Math.PI) < 0.01)
                            test_angle = double.PositiveInfinity;

                        // See if this angle is an improvement.
                        if (test_angle < best_angle)
                        {
                            best_j = j;
                            best_angle = test_angle;
                        }
                    }
                }
            }

            // Return the result.
            Debug.Assert(best_j >= 0);
            next_to_point = vertices[best_j];
        }

        private static void FindIntersection(
           PointF p1, PointF p2, PointF p3, PointF p4,
           out bool lines_intersect, out bool segments_intersect,
           out float t1, out float t2,
           out PointF intersection,
           out PointF close_p1, out PointF close_p2)
        {
            // Get the segments' parameters.
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);

            t1 = ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34) / denominator;
            if (float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new PointF(float.NaN, float.NaN);
                t1 = float.PositiveInfinity;
                t2 = float.PositiveInfinity;
                close_p1 = new PointF(float.NaN, float.NaN);
                close_p2 = new PointF(float.NaN, float.NaN);
                return;
            }
            lines_intersect = true;

            t2 = ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12) / -denominator;

            // Find the point of intersection.
            intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect =
                ((t1 >= 0) && (t1 <= 1) &&
                 (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new PointF(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }

        private static double VectorAngle(PointF p11, PointF p12, PointF p21, PointF p22)
        {
            // Find the vectors.
            PointF v1 = new PointF(p12.X - p11.X, p12.Y - p11.Y);
            PointF v2 = new PointF(p22.X - p21.X, p22.Y - p21.Y);

            // Calculate the vector lengths.
            double len1 = Math.Sqrt(v1.X * v1.X + v1.Y * v1.Y);
            double len2 = Math.Sqrt(v2.X * v2.X + v2.Y * v2.Y);

            // Use the dot product to get the cosine.
            double dot_product = v1.X * v2.X + v1.Y * v2.Y;
            double cos = dot_product / len1 / len2;

            // Use the cross product to get the sine.
            double cross_product = v1.X * v2.Y - v1.Y * v2.X;
            double sin = cross_product / len1 / len2;

            // Find the angle.
            double angle = Math.Acos(cos);
            if (sin < 0) angle = -angle;
            return angle;
        }

        #endregion

        // Given three colinear points p, q, r,
        // the function checks if point q lies
        // on line segment 'pr'
        static bool onSegment(PointF p, PointF q, PointF r)
        {
            if (q.X <= Math.Max(p.X, r.X) &&
                q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) &&
                q.Y >= Math.Min(p.Y, r.Y))
            {
                return true;
            }
            return false;
        }

        // To find orientation of ordered triplet (p, q, r).
        // The function returns following values
        // 0 --> p, q and r are colinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        static int orientation(PointF p, PointF q, PointF r)
        {
            var val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y);

            if (val == 0)
            {
                return 0; // colinear
            }
            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        // The function that returns true if
        // line segment 'p1q1' and 'p2q2' intersect.
        static bool doIntersect(PointF p1, PointF q1,
                                PointF p2, PointF q2)
        {
            // Find the four orientations needed for
            // general and special cases
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
            {
                return true;
            }

            // Special Cases
            // p1, q1 and p2 are colinear and
            // p2 lies on segment p1q1
            if (o1 == 0 && onSegment(p1, p2, q1))
            {
                return true;
            }

            // p1, q1 and p2 are colinear and
            // q2 lies on segment p1q1
            if (o2 == 0 && onSegment(p1, q2, q1))
            {
                return true;
            }

            // p2, q2 and p1 are colinear and
            // p1 lies on segment p2q2
            if (o3 == 0 && onSegment(p2, p1, q2))
            {
                return true;
            }

            // p2, q2 and q1 are colinear and
            // q1 lies on segment p2q2
            if (o4 == 0 && onSegment(p2, q1, q2))
            {
                return true;
            }

            // Doesn't fall in any of the above cases
            return false;
        }

        public static bool IsInside(float x, float y, IList<PointF> polygon)
        {
            int n = polygon.Count;
            // There must be at least 3 vertices in polygon[]
            if (n < 3)
            {
                return false;
            }

            PointF p = new PointF(x, y);

            // Create a point for line segment from p to infinite
            PointF extreme = new PointF(10000000, p.Y);

            // Count intersections of the above line
            // with sides of polygon
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;

                // Check if the line segment from 'p' to
                // 'extreme' intersects with the line
                // segment from 'polygon[i]' to 'polygon[next]'
                if (doIntersect(polygon[i],
                                polygon[next], p, extreme))
                {
                    // If the point 'p' is colinear with line
                    // segment 'i-next', then check if it lies
                    // on segment. If it lies, return true, otherwise false
                    if (orientation(polygon[i], p, polygon[next]) == 0)
                    {
                        return onSegment(polygon[i], p,
                                        polygon[next]);
                    }
                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise
            return (count % 2 == 1); // Same as (count%2 == 1)
        }

    }
}
