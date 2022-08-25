using libtui.utils;
using System;
using System.Collections.Generic;

namespace libtui.drawing
{
    public struct Ellipse : IEquatable<Ellipse>, IGeometry
    {
        public Point Start { get; }

        public Point End { get; }

        public Ellipse(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public override bool Equals(object obj)
        {
            return obj is Ellipse ellipse && Equals(ellipse);
                   
        }

        public bool Equals(Ellipse other)
        {
            return Start.Equals(other.Start) &&
                   End.Equals(other.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        public IEnumerable<GeometryData> GetGeometryDatas()
        {
            var a = Math.Abs(End.X - Start.X);
            var b = Math.Abs(End.Y - Start.Y);
            var edge = (int)(Math.PI * (3 * (a + b) - Math.Sqrt((3 * a + b) * (a + 3 * b))) / 12f);

            float radius = MathTools.GetDistance(End, Start);
            float angle = MathF.PI * 2 / edge;
            float l = radius / MathF.Cos(angle / 2.0f);

            var v2 = End - Start;
            v2.Normalize();
            var sPnt = v2 * l;
            sPnt = RotateInRadian(sPnt, angle / 2);
 
            var points = new GeometryPoint[edge];
            points[0] = new GeometryPoint(0, (Start + sPnt).ToPointF(), 0);

            for (int i = 1; i < edge; ++i)
            {
                var vPnt = RotateInRadian(sPnt, angle * i);
                points[i] = new GeometryPoint(0, (Start + vPnt).ToPointF(), 0);
            }

            yield return new GeometryData(GeometryDataType.Vertex, points);
        }

        private static Point RotateInRadian(Point v, float rad)
        {
            float x = v.X * MathF.Cos(rad) - v.Y * MathF.Sin(rad);
            float y = v.X * MathF.Sin(rad) + v.Y * MathF.Cos(rad);
            return new Point((int)x, (int)y);
        }
    }
}
