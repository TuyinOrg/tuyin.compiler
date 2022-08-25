using System;
using System.Collections.Generic;

namespace compute.drawing
{
    public struct LineF : IEquatable<LineF>, IGeometry
    {
        public PointF Start { get; }

        public PointF End { get; }

        public LineF(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }

        public override bool Equals(object obj)
        {
            return obj is LineF line && Equals(line);
        }

        public bool Equals(LineF line)
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
            yield return new GeometryData(GeometryDataType.Vertex, new GeometryPoint[] { new GeometryPoint(0, Start, 0), new GeometryPoint(1, End, 0) });
        }
    }
}
