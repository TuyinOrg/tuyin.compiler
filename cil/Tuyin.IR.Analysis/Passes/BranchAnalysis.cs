using System;
using System.Collections.Generic;
using Tuyin.IR.Analysis.Data;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Analysis.Passes
{
    class BranchAnalysis : IAnalysis<IReadOnlyList<Statment>, Branch>
    {
        public Branch Run(IReadOnlyList<Statment> input)
        {
            var branchIndex = 1;
            var stackIndex = 0;
            var stack = new List<BranchInfo>();
            stack.Add(new BranchInfo(stackIndex, branchIndex++, 0, input.Count));
            for (int i = 0; i < input.Count; i++)
            {
                // 分支退出
                if (stack.Count > 0)
                    while (stack[stackIndex].End == i)
                        stackIndex--;

                var stmt = input[i];
                if (stmt is Goto br)
                {     
                    var bi = br.Label.Index;
                    if (stack[^1].End == bi)
                        continue;

                    var parent = br.NodeType == AstNodeType.Goto ?
                        (br.Label.Index != bi ? stack[^1].Parent : stack[stackIndex].Parent) :
                        stack[stackIndex++].Index;

                    stack.Add(new BranchInfo(parent, branchIndex++, i + 1, bi));
                }
            }

            var branches = new int[input.Count + 1];
            var parents = new int[input.Count + 1];
            var previews = new int[stack.Count + 1];
            for (var i = 0; i < stack.Count; i++)
            {
                var b = stack[i];
                var c = b.End - b.Start;
                previews[b.Index] = b.Parent;

                // 特例优化
                if (c > 0)
                {
                    switch (c)
                    {
                        case 0:
                            // Do nothing...
                            break;
                        default:
                            Array.Fill(branches, b.Index, b.Start, c);
                            Array.Fill(parents, b.Parent, b.Start, c);
                            break;
                        case 5:
                            branches[b.Start + 4] = b.Index;
                            parents[b.Start + 4] = b.Parent;
                            goto case 4;
                        case 4:
                            branches[b.Start + 3] = b.Index;
                            parents[b.Start + 3] = b.Parent;
                            goto case 3;
                        case 3:
                            branches[b.Start + 2] = b.Index;
                            parents[b.Start + 2] = b.Parent;
                            goto case 2;
                        case 2:
                            branches[b.Start + 1] = b.Index;
                            parents[b.Start + 1] = b.Parent;
                            goto case 1;
                        case 1:
                            branches[b.Start] = b.Index;
                            parents[b.Start] = b.Parent;
                            break;
                    }
                }
            }

            return new Branch(branches, parents, previews); 
        }

        struct BranchInfo
        {
            public BranchInfo(int parent, int index, int start, int end)
            {
                Parent = parent;
                Index = index;
                Start = start;
                End = end;
            }

            public int Parent { get; }

            public int Index { get; }

            public int Start { get; }

            public int End { get; }

            public override string ToString()
            {
                return $"[{Parent}:{Index}]{Start}-{End}";
            }
        }
    }
}
