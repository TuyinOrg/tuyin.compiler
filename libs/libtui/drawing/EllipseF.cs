using libtui.utils;
using System;
using System.Collections.Generic;

namespace libtui.drawing
{
    public struct EllipseF : IEquatable<EllipseF>, IGeometry
    {
        public PointF Start { get; }

        public PointF End { get; }

        public EllipseF(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }

        public EllipseF(float x1, float y1, float x2, float y2)
            : this(new PointF(x1, y1), new PointF(x2, y2))
        {
        }

        public override bool Equals(object obj)
        {
            return obj is EllipseF ellipse && Equals(ellipse);

        }

        public bool Equals(EllipseF other)
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

            float radius = MathTools.GetDistance(End, Start) / 2;
            float step = MathF.PI * 2 / edge;

            var half = edge / 2;
            var points = new GeometryPoint[edge];
            for (int i = 0; i < half; ++i)
            {
                var t = MathF.PI - step * i;
                var p = RotateInRadian(Start, t);
                points[i] = new GeometryPoint(0, p, 0);
            }

            for (int i = half; i < edge; ++i)
            {
                var t = step * (i - half);
                var p = RotateInRadian(Start, t);
                points[i] = new GeometryPoint(0, p, 0);
            }

            yield return new GeometryData(GeometryDataType.Vertex, points);
        }

        private static PointF RotateInRadian(PointF v, float rad)
        {
            float x = v.X * MathF.Cos(rad) - v.Y * MathF.Sin(rad);
            float y = v.X * MathF.Sin(rad) + v.Y * MathF.Cos(rad);
            return new PointF(x, y);
        }
    }
}
