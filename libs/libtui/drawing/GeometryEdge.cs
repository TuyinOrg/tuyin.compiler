namespace libtui.drawing
{
    public struct GeometryEdge
    {
        public GeometryPoint Start { get; }

        public GeometryPoint End { get; }

        public GeometryEdge(GeometryPoint start, GeometryPoint end) 
        {
            Start = start;
            End = end;
        }
    }
}
