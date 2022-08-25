using System;
using System.Collections.Generic;
using System.Linq;

namespace libtui.drawing
{
    public struct PolygonF : IEquatable<PolygonF>, IGeometry
    {
        public PointF[] Points { get; }

        public PolygonF(PointF[] points)
        {
            Points = points;
        }

        public override bool Equals(object obj)
        {
            return obj is PolygonF polygon && Equals(polygon);
        }

        public bool Equals(PolygonF other)
        {
            return Points.SequenceEqual(other.Points);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Points);
        }

        public IEnumerable<GeometryData> GetGeometryDatas()
        {
            var points = new GeometryPoint[Points.Length];
            for (var i = 0; i < points.Length; i++)
                points[i] = new GeometryPoint(0, Points[i], 0);

            yield return new GeometryData(GeometryDataType.Vertex, points);
        }
    }
}
