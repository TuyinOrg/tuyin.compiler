using System;

namespace compute
{
    public struct ShaderEntryPointSize                                  
    {
        public ShaderEntryPointSize(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }

        public int Y { get; }

        public int Z { get; }

        public override bool Equals(object obj)
        {
            return obj is ShaderEntryPointSize size &&
                   X == size.X &&
                   Y == size.Y &&
                   Z == size.Z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }
}
