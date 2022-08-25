namespace Tuyin.IR.Compiler.Uitls
{
    public struct Range : IEquatable<Range>
    {
        public int Start { get; }

        public int End { get; }

        public int Length => End - Start;

        public int Index => Start;

        public Range(int start) 
            : this(start, start)
        {
        }

        public Range(int start, int end) 
        {
            Start = start;
            End = end;
        }

        public bool Contains(int index) 
        {
            var n = Normalize();

            return index >= n.Start && index <= n.End;
        }

        public bool Contains(Range range)
        {
            var n = range.Normalize();
            var n2 = this.Normalize();

            return n.Start >= n2.Start && n.End <= n2.End;
        }

        public Range Normalize()
        {
            if (Start > End)
                return new Range(End, Start);

            return this;
        }

        public Range Overlap(Range range) 
        {
            return new Range(Start < range.Start ? range.Start : Start, End > range.End ? range.End : End);
        }

        public Range Intersect(Range range) 
        {
            return new Range(Start, End > range.Start ? range.Start : End);
        }

        public Range Combine(Range range)
        {
            return new Range(Start > range.Start ? range.Start : Start, End < range.End ? range.End : End);
        }

        public Range Remove(Range range)
        {
            if (range.Start >= Start && range.End < End)
            {
                return new Range(Start, End - range.Length);
            }
            else
            {
                var offset = range.Start < Start ? range.Length : 0;
                var start = range.End > Start && range.Start < Start ? range.End : Start;
                var end = range.Start < End && range.End >= End ? range.Start : End;

                return new Range(start - offset, end - offset);
            }
        }

        public static bool operator !=(Range left, Range right)
        {
            return left.Start != right.Start || left.End != right.End;
        }

        public static bool operator ==(Range left, Range right)
        {
            return left.Start == right.Start && left.End == right.End;
        }

        public override bool Equals(object obj)
        {
            if (obj is Range)
                return Equals((Range)obj);

            return false;
        }

        public bool Equals(Range obj)
        {
            return Start == obj.Start && End == obj.End;
        }

        public override int GetHashCode()
        {
            return Start.GetHashCode() ^ End.GetHashCode();
        }
    }
}
