using System;

namespace libtui.drawing
{
    public struct Padding : IEquatable<Padding>
    {
        public Padding(int left, int right, int top, int bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public int Left { get; }

        public int Right { get; }

        public int Top { get; }

        public int Bottom { get; }

        public override bool Equals(object obj)
        {
            return obj is Padding padding && Equals(padding);
        }

        public bool Equals(Padding other)
        {
            return Left == other.Left &&
                   Right == other.Right &&
                   Top == other.Top &&
                   Bottom == other.Bottom;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Left, Right, Top, Bottom);
        }

        public static bool operator ==(Padding left, Padding right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Padding left, Padding right)
        {
            return !(left == right);
        }
    }
}
