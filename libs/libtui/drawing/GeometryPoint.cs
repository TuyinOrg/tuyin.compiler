using System;

namespace libtui.drawing
{
    public struct GeometryPoint : IEquatable<GeometryPoint>, IPoint<float>
    {
        public int Index 
        {
            get;
        }

        public PointF Location 
        {
            get;
        }

        public int Attribute 
        {
            get;
        }

        public float X => Location.X;

        public float Y => Location.Y;

        internal GeometryPoint(int index, PointF loc, int attribute) 
        {
            Index = index;
            Location = loc;
            Attribute = attribute;
        }

        public override bool Equals(object obj)
        {
            return obj is GeometryPoint point && Equals(point);
        }

        public bool Equals(GeometryPoint point)
        {
            return Index == point.Index &&
                   Attribute == point.Attribute &&
                   Location.Equals(point.Location) &&
                   X == point.X &&
                   Y == point.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Index, Location, X, Y, Attribute);
        }
    }
}
