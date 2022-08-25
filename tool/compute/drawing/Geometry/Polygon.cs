using System;
using System.Collections.Generic;
using System.Linq;

namespace compute.drawing
{
    public struct Polygon : IEquatable<Polygon>, IGeometry
    {
        public Point[] Points { get; }

        public Polygon(Point[] points) 
        {
            Points = points;
        }

        public override bool Equals(object obj)
        {
            return obj is Polygon polygon && Equals(polygon);
        }

        public bool Equals(Polygon other)
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
                points[i] = new GeometryPoint(0, Points[i].ToPointF(), 0);

            yield return new GeometryData(GeometryDataType.Vertex, points);
        }
    }
}
