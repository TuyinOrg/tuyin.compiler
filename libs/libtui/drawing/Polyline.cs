using System;
using System.Collections.Generic;
using System.Linq;

namespace libtui.drawing
{
    public struct Polyline : IEquatable<Polyline>, IGeometry
    {
        public Point[] Points { get; }

        public override bool Equals(object obj)
        {
            return obj is Polyline polygon && Equals(polygon);
        }

        public bool Equals(Polyline other)
        {
            return Points.SequenceEqual(other.Points);
        }

        public IEnumerable<GeometryData> GetGeometryDatas()
        {
            var points = new GeometryPoint[Points.Length];
            for (var i = 0; i < points.Length; i++)
                points[i] = new GeometryPoint(0, Points[i].ToPointF(), 0);

            yield return new GeometryData(GeometryDataType.Vertex, points);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Points);
        }
    }
}
