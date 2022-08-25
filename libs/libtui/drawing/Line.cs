using System;
using System.Collections.Generic;

namespace libtui.drawing
{
    public struct Line : IEquatable<Line>, IGeometry
    {
        public Point Start { get; }

        public Point End { get; }

        public Line(Point start, Point end) 
        {
            Start = start;
            End = end;
        }

        public override bool Equals(object obj)
        {
            return obj is Line line && Equals(line);
        }

        public bool Equals(Line line)
        {
            return Start.Equals(line.Start) &&
                   End.Equals(line.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        public IEnumerable<GeometryData> GetGeometryDatas()
        {
            yield return new GeometryData(GeometryDataType.Vertex, new GeometryPoint[] { new GeometryPoint(0, Start.ToPointF(), 0), new GeometryPoint(1, End.ToPointF(), 0) });
        }
    }
}
