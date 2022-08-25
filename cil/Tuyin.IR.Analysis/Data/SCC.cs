using System.Collections.Generic;

namespace Tuyin.IR.Analysis.Data
{
    /// <summary>
    /// 强连通分量
    /// </summary>
    public class SCC
    {
        internal SCC(IReadOnlyList<SCCRange> ranges)
        {
            Ranges = ranges;
        }

        public IReadOnlyList<SCCRange> Ranges { get; }
    }

    public struct SCCRange 
    {
        public SCCRange(int start, int end, bool isLoop)
        {
            Start = start;
            End = end;
            IsLoop = isLoop;
        }

        public int Start { get; }

        public int End { get; }

        public bool IsLoop { get; }

        public override string ToString()
        {
            if (IsLoop)
                return $"loop({Start}-{End})";

            return $"{Start}-{End}";
        }
    }
}
