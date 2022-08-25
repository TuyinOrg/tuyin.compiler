namespace libtui.drawing
{
    public sealed class PathData
    {
        PointF[] points;
        byte[] types;

        public PathData()
        {
        }

        public PointF[] Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }

        public byte[] Types
        {
            get
            {
                return types;
            }
            set
            {
                types = value;
            }
        }
    }
}
