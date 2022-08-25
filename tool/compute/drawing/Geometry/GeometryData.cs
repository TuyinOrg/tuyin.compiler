using compute.utils;

namespace compute.drawing
{
    public sealed class GeometryData
    {
        public GeometryDataType Type { get; internal set; }

        public GeometryPoint[] Points { get; }

        public GeometryData(GeometryDataType type, GeometryPoint[] points)                                
        {
            Type = type;
            Points = points;
        }

        public bool HitTest(PointF point)                                                  
        {
            return BoundaryTracing.PointIsInPolygon(point.X, point.Y, Points);
        }
    }
}
