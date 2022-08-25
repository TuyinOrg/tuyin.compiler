namespace Tuyin.IR.Analysis.Data
{
    public struct Scope
    {
        public Scope(int start, int end) 
            : this(start, end, -1, -1)
        {
        }

        public Scope(int start, int end, int case0, int case1)
        {
            Start = start;
            End = end;
            Case0 = case0;
            Case1 = case1;
        }

        public int Start { get; }

        public int End { get; }

        public int Case0 { get; }

        public int Case1 { get; }

        public override string ToString()
        {
            return $"{Start}-{End}" + (Case0 == -1 ? string.Empty : $" case0:{Case0}") + (Case1 == -1 ? string.Empty : $" case1:{Case1}");
        }
    }
}
