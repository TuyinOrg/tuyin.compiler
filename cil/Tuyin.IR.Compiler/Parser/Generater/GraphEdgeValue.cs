using Tuyin.IR.Compiler.Parser.Expressions;
using System.Text.RegularExpressions;

namespace Tuyin.IR.Compiler.Parser.Generater
{
    partial class GraphEdgeValue : IComparable<GraphEdgeValue>
    {
        private int mHashCode;

        public virtual bool Xor { get; }

        public virtual char[] Chars 
        {
            get;
            private set;
        }

        internal GraphEdgeValue() 
        {
        }

        internal GraphEdgeValue(int c)
            : this(false, new char[] { (char)c })
        {
            if (c > char.MaxValue)
                throw new NotSupportedException($"max index {char.MaxValue}");
        }

        internal GraphEdgeValue(ushort c)
           : this((char)c)
        {
        }

        internal GraphEdgeValue(char c)
            : this(false, new char[] { c })
        {
        }

        internal GraphEdgeValue(bool xor, char c)
            : this(xor, new char[] { c })
        {
        }

        internal GraphEdgeValue(bool xor, IEnumerable<char> chars)
        {
            var count = chars.Count();
            chars = chars.OrderBy(x => x);
            var optimize = count > char.MaxValue / 2;
            Xor = optimize ? !xor : xor;
            if (optimize)
            {
                var offset = 0;
                Chars = new char[char.MaxValue - count];
                for (var i = 0; i < char.MaxValue; i++)
                {
                    var c = (char)i;
                    if (!chars.Contains(c))
                    {
                        Chars[offset++] = c;
                    }
                }
            }
            else
            {
                Chars = chars.ToArray();
            }

            mHashCode = GetCompareValue();
        }

        public GraphEdgeValue GetCommonDivisor(GraphEdgeValue other)
        {
            var otherToken = other;
            var otherChars = otherToken.Chars;
            if (!Xor)
            {
                var chars = new List<char>();
                if (!otherToken.Xor)
                {
                    for (var i = 0; i < Chars.Length; i++)
                    {
                        var c = Chars[i];
                        if (otherChars.Contains(c))
                        {
                            if (!chars.Contains(c))
                            {
                                chars.Add(c);
                            }
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < Chars.Length; i++)
                    {
                        var c = Chars[i];
                        if (!otherChars.Contains(c))
                        {
                            if (!chars.Contains(c))
                            {
                                chars.Add(c);
                            }
                        }
                    }
                }

                return new GraphEdgeValue(false, chars);
            }
            else
            {
                if (!otherToken.Xor)
                {
                    var chars = new List<char>();
                    for (var i = 0; i < otherChars.Length; i++)
                    {
                        var c = otherChars[i];
                        if (!Chars.Contains(c))
                        {
                            if (!chars.Contains(c))
                            {
                                chars.Add(c);
                            }
                        }
                    }

                    return new GraphEdgeValue(false, chars);
                }
                else
                {
                    var chars = new List<char>(Chars);
                    for (var i = 0; i < otherChars.Length; i++)
                    {
                        var c = otherChars[i];
                        if (!chars.Contains(c))
                        {
                            chars.Add(c);
                        }
                    }

                    return new GraphEdgeValue(true, chars);
                }
            }
        }

        public GraphEdgeValue Eliminate(GraphEdgeValue other) 
        {
            GraphEdgeValue result = null;
            EliminateIntersection(other, out result);
            return result;
        }

        public virtual bool EliminateIntersection(GraphEdgeValue other, out GraphEdgeValue vector)
        {
            var selfChars = new List<char>(Chars);
            var selfXor = Xor;
            var otherToken = other;
            var otherChars = otherToken.Chars;
            if (!Xor)
            {
                if (!otherToken.Xor)
                {
                    for (var i = 0; i < otherChars.Length; i++)
                    {
                        var c = otherChars[i];
                        selfChars.Remove(c);
                    }
                }
                else
                {
                    for (var i = 0; i < selfChars.Count; i++)
                    {
                        var c = selfChars[i];
                        if (!otherChars.Contains(c))
                        {
                            selfChars.Remove(c);
                        }
                    }
                }
            }
            else
            {
                if (!otherToken.Xor)
                {
                    for (var i = 0; i < otherChars.Length; i++)
                    {
                        var c = otherChars[i];
                        if (!selfChars.Contains(c))
                        {
                            selfChars.Add(c);
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < otherChars.Length; i++)
                    {
                        var c = otherChars[i];
                        if (selfChars.Contains(c))
                        {
                            selfChars.Remove(c);
                        }
                    }

                    selfXor = false;
                }
            }

            vector = new GraphEdgeValue(selfXor, selfChars);
            return selfChars.Count == 0;
        }

        public virtual GraphEdgeValue Combine(GraphEdgeValue other) 
        {
            var otherToken = other;
            var otherChars = otherToken.Chars;
            if (!Xor)
            {
                if (!otherToken.Xor)
                {
                    var newChars = new List<char>(Chars);
                    for (var i = 0; i < otherChars.Length; i++)
                    {
                        var c = otherChars[i];
                        if (!Chars.Contains(c))
                        {
                            newChars.Add(c);
                        }
                    }
                    return new GraphEdgeValue(false, newChars);
                }
                else
                {
                    var newChars = new List<char>(otherChars);
                    for (var i = 0; i < Chars.Length; i++)
                    {
                        var c = Chars[i];
                        if (newChars.Contains(c))
                        {
                            newChars.Remove(c);
                        }
                    }

                    return new GraphEdgeValue(true, newChars);
                }
            }
            else
            {
                if (!otherToken.Xor)
                {
                    var newChars = new List<char>(Chars);
                    for (var i = 0; i < otherChars.Length; i++)
                    {
                        var c = otherChars[i];
                        if (newChars.Contains(c))
                        {
                            newChars.Remove(c);
                        }
                    }
                    return new GraphEdgeValue(true, newChars);
                }
                else
                {
                    var newChars = new List<char>(Chars);
                    for (var i = 0; i < otherChars.Length; i++)
                    {
                        var c = otherChars[i];
                        if (!newChars.Contains(c))
                        {
                            newChars.Add(c);
                        }
                    }

                    return new GraphEdgeValue(true, newChars);
                }
            }
        }

        public virtual bool IsVaild()
        {
            if (Chars == null)
                return false;

            return Chars.Length > 0;
        }

        public override string ToString()
        {
            if (Chars == null)
                return string.Empty;

            if (Chars.Length == 0)
                return Xor ? "^*" : "*";

            if (Chars.Length == 1)
                return (Xor ? "^" : string.Empty) + Chars[0].ToString();

            if (Chars.Length == 2)
                return $"{(Xor ? "^" : string.Empty)}({ Chars[0]}|{Chars[1]})";


            // 查找段落
            return (Xor ? "^" : string.Empty) + RangesToString(Chars); // $"[{Chars.First()}..{Chars.Last()}]" + (xorChars.Count > 0 ? "^" + string.Join('|', xorChars) : string.Empty);
        }

        static string RangesToString(char[] a) 
        {
            return string.Join(' ', CombineRanges(a));
        }

        static List<string> CombineRanges(char[] a) 
        {
            const int xorCount = 3;
            const int showCount = 4;
            var list = new List<string>();

            var ranges = ConsecutiveRanges(a);
            if (ranges.Count > 0)
            {
                var regions = new List<(List<(char, char)>, List<char>)>();
                regions.Add((new List<(char, char)>(), new List<char>()));
                regions[0].Item1.Add(ranges[0]);

                for (var i = 0; i < ranges.Count - 1; i++)
                {
                    var curr = ranges[i];
                    var next = ranges[i + 1];
                    var start = curr.Item2 + 1;
                    var count = next.Item1 - start;
                    if (count < xorCount && count >= 0)
                    {
                        // 合并区间
                        regions[regions.Count - 1].Item1.Add(next);

                        // 得到xor字符
                        regions[regions.Count - 1].Item2.AddRange(Enumerable.Range(start, count).Select(x => (Char)x));
                    }
                    else
                    {
                        // 新建区间
                        regions.Add((new List<(char, char)>(), new List<char>()));
                        regions[regions.Count - 1].Item1.Add(next);
                    }
                }

                foreach (var region in regions) 
                {
                    var start = region.Item1.First().Item1;
                    var end = region.Item1.Last().Item2;
                    var diff = end - start;

                    var head = string.Empty;
                    if (diff == 0)
                    {
                        head = start.ToString();
                    }
                    else if (diff < showCount)
                    {
                        head = string.Join("|", Enumerable.Range(start, end - start).Select(x => (Char)x));
                    }
                    else  
                    {
                        head = $"[{start}..{end}]";
                    }

                    list.Add(Regex.Escape(head + (region.Item2.Count > 0 ? "^" + RangesToString(region.Item2.ToArray()) : string.Empty)));
                }
            }

            return list;
        }

        static List<(char, char)> ConsecutiveRanges(char[] a)
        {
            int length = 1;
            var list = new List<(char, char)>();

            // If the array is empty,
            // return the list
            if (a.Length == 0)
            {
                return list;
            }

            // Traverse the array from first position
            for (int i = 1; i <= a.Length; i++)
            {

                // Check the difference between the
                // current and the previous elements
                // If the difference doesn't equal to 1
                // just increment the length variable.
                if (i == a.Length || a[i] - a[i - 1] != 1)
                {
                    // If the range contains
                    // only one element.
                    // add it into the list.
                    if (length == 1)
                    {
                        list.Add((a[i - length], a[i - length]));
                    }
                    else
                    {

                        // Build the range between the first
                        // element of the range and the
                        // current previous element as the
                        // last range.
                        list.Add((a[i - length], a[i - 1]));
                    }

                    // After finding the first range
                    // initialize the length by 1 to
                    // build the next range.
                    length = 1;
                }
                else
                {
                    length++;
                }
            }
            return list;
        }

        protected int GetCompareValue() 
        {
            int result = 0;
            foreach(var c in Chars.OrderBy(x => x))
            {
                result = 37 ^ result + c;
            }

            if (Xor)
            {
                result = int.MaxValue - result;
            }

            return result;
        }

        public override int GetHashCode()
        {
            return mHashCode;
        }

        public int CompareTo(GraphEdgeValue other)
        {
            return mHashCode.CompareTo(other.mHashCode);
        }

        public IEnumerable<char> GetChars(int length)
        {
            if (!Xor)
            {
                if (Chars != null)
                {
                    foreach (var c in Chars)
                    {
                        yield return c;
                    }
                }
            }
            else 
            {
                for (var i = 0; i < length; i++)
                {
                    var c = (char)i;
                    if (!Chars.Contains(c))
                    {
                        yield return c;
                    }
                }
            }
        }

        public bool Contains(char val) 
        {
            return Xor ? !Chars.Contains(val) : Chars.Contains(val);
        }

        public virtual GraphEdgeValue Clone()
        {
            return new GraphEdgeValue(Xor, Chars.ToArray());
        }

        internal CharSetExpression CreateCharSetExpression()
        {
            return new CharSetExpression(Xor, Chars.ToArray());
        }

        public int GetLength()
        {
            return Xor ? char.MaxValue - Chars.Length : Chars.Length;
        }

        public GraphEdgeValue Reverse()
        {
            if (Chars.Length == 0)
                return Xor ? GraphEdgeValue.Empty : GraphEdgeValue.Any;

            return new GraphEdgeValue(!Xor, Chars);
        }
    }
}
