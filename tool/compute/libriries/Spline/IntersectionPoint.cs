using System;
using System.Numerics;

namespace compute.spline
{
    struct IntersectionPoint : IEquatable<IntersectionPoint>
    {
        public int LeftIndex { get; }

        public int RightIndex { get; }

        public float X { get; }

        public float Y { get; }

        public IntersectionPoint(int leftIndex, int rightIndex, float x, float y) 
        {
            LeftIndex = leftIndex;
            RightIndex = rightIndex;
            X = x;
            Y = y;
        }

        public static implicit operator Vector2(IntersectionPoint pts) 
        {
            return new Vector2(pts.X, pts.Y);
        }

        public override bool Equals(object obj)
        {
            if (obj is IntersectionPoint pts)
                return Equals(pts);

            return false;
        }

        public bool Equals(IntersectionPoint other)
        {
            return LeftIndex == other.LeftIndex &&
                RightIndex == other.RightIndex &&
                X == other.X &&
                Y == other.Y;
        }

        public override int GetHashCode()
        {
            return LeftIndex ^ 392 + RightIndex ^ 392 + X.GetHashCode() ^ 392 + Y.GetHashCode() ^ 392;
        }
    }
}
