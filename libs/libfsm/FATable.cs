using libgraph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace libfsm
{
    public abstract partial class FATable<T>
    {
        private readonly List<FABuildStep<T>> mBuildSteps = new List<FABuildStep<T>>();
      
        public IReadOnlyList<FABuildStep<T>> BuildSteps => mBuildSteps;

        /// <summary>
        /// 移进表
        /// </summary>
        public IList<FATransition<T>> Transitions 
        { 
            get;
            private set; 
        }

        /// <summary>
        /// 动作表
        /// </summary>
        public FAActionTable Actions 
        { 
            get;
        } = new FAActionTable();

        /// <summary>
        /// SIMD表
        /// </summary>
        public FASMID FASMID 
        {
            get;
            private set;
        }

        /// <summary>
        /// 状态容量
        /// </summary>
        public int StateCapacity 
        {
            get;
            private set;
        }

        /// <summary>
        /// 单状态数据容量
        /// </summary>
        public int StateDataCapacity 
        {
            get;
            private set;
        }

        /// <summary>
        /// 生成数据
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph">用户图</param>
        /// <param name="stateDataCapacity">单个状态数据量</param>
        /// <param name="metadataSelector"></param>
        /// <param name="flags">生成选项</param>
        public void Generate<TVertex, TEdge>(
            IGraph<TVertex, TEdge> graph, 
            int stateDataCapacity, 
            Func<TEdge, T> metadataSelector, 
            FATableFlags flags)
            where TEdge : IEdge<TVertex>
            where TVertex : IVertex
        {
            // 找到所有入口点
            IEnumerable<ushort> GetEntryPoints() 
            {
                var queueGroup = Transitions.AsParallel().
                    GroupBy(x => x.Right).
                    ToDictionary(x => x.Key);

                return Transitions.AsParallel().
                    Where(x => !queueGroup.ContainsKey(x.Left)).
                    Select(x => x.Left).Distinct();
            }

            var connect = new TransitionMetadata<char, bool>();

            StateCapacity = graph.VerticesCount + 1;
            StateDataCapacity = stateDataCapacity;
            Transitions = CreateGraph(graph, StateDataCapacity, StateCapacity, metadataSelector, flags, connect, Actions);

            mBuildSteps.AddRange(Transitions.Select(x => new FABuildStep<T>(FABuildStage.CreateGraph, FABuildType.Add, x)));

            // 合并等效部分
            if (!flags.HasFlag(FATableFlags.NotMergeEquivalent))
                mBuildSteps.AddRange(Minimize(Transitions, GetEntryPoints()));

            // 解决连接冲突
            if (flags.HasFlag(FATableFlags.EdgeConflicts))
            {
                var edgeConflictsFlags = ConflictsDetectionFlags.Next;
                if (flags.HasFlag(FATableFlags.MetadataConflicts))
                    edgeConflictsFlags |= ConflictsDetectionFlags.Metadata;

                if (flags.HasFlag(FATableFlags.SymbolConflicts))
                    edgeConflictsFlags |= ConflictsDetectionFlags.Symbol;

                mBuildSteps.AddRange(ConflictResolution(Transitions, edgeConflictsFlags));

#if DEBUG
                if (Transitions.GroupBy(x => new { A = x.Metadata, B = x.Symbol, C = x.Left, D = x.Right, E = x.Input }).Where(x => x.Count() > 1).Count() > 0)
                    throw new Exception("未知内部错误");
#endif
            }

            // 优化DFA
            if (flags.HasFlag(FATableFlags.Optimize))
            {
                // 找到所有入口点
                var queueGroup = Transitions.AsParallel().
                    GroupBy(x => x.Right).
                    ToDictionary(x => x.Key);

                var queueEntries = Transitions.AsParallel().
                    Where(x => !queueGroup.ContainsKey(x.Left)).
                    Select(x => x.Left).Distinct();

                mBuildSteps.AddRange(Optimize(Transitions, queueEntries, flags));
            }

            // 创建SSE
            FASMID = new FASMID(StateCapacity);

            // 检查SSE冲突,根据输入数量和输入值检测冲突
            var visitor = new bool[Transitions.Max(x => x.InputCount) + 1, ushort.MaxValue];
            for (var i = 0; i < Transitions.Count; i++) 
            {
                var tran = Transitions[i];
                if (tran.InputCount == 0)
                    throw new NotImplementedException("不可能会存在输入为0的状态");

                // 不处理输入为1的移进
                if (tran.InputCount == 1)
                    continue;

                if (visitor[tran.InputCount, tran.Input])
                    throw new NotImplementedException("未解决SSE输入冲突。");

                visitor[tran.InputCount, tran.Input] = true;
                FASMID[tran.Left] = new FASIMDState(tran.InputCount, tran.Input);
            }
        }

        #region 创建视图

        struct SubsetEntry : IEquatable<SubsetEntry>
        {
            public SubsetEntry(FATransition<T>[] transitions)
            {
                Transitions = transitions;
            }

            public FATransition<T>[] Transitions { get; }

            public IEnumerable<FASymbol> Symbols => Transitions.Select(x => x.Symbol);

            /// <summary>
            /// 左侧id
            /// </summary>
            public ushort Left => Transitions[0].Left;

            /// <summary>
            /// 右侧id
            /// </summary>
            public ushort Right => Transitions[0].Right;

            /// <summary>
            /// 原始图左侧id
            /// 用于引用层级关系，被使用在制表函数中
            /// </summary>
            public ushort SourceLeft => Transitions[0].SourceLeft;

            /// <summary>
            /// 原始图右侧id
            /// 用于引用层级关系，被使用在制表函数中
            /// </summary>
            public ushort SourceRight => Transitions[0].SourceRight;

            /// <summary>
            /// 移进符
            /// </summary>
            public char Input => Transitions[^1].Input;

            /// <summary>
            /// 并行扫描数量
            /// </summary>
            public int InputCount => Transitions[^1].InputCount;

            /// <summary>
            /// 连线所带的用户元数据
            /// </summary>
            public T Metadata => Transitions[^1].Metadata;

            public override bool Equals(object obj)
            {
                return obj is SubsetEntry entry && Equals(entry);
            }

            public bool Equals(SubsetEntry other)
            {
                return Transitions.SequenceEqual(other.Transitions);
            }

            public override int GetHashCode()
            {
                int hash = 0;
                for (var i = 0; i < Transitions.Length; i++)
                    hash = HashCode.Combine(hash, Transitions[i]);

                return hash;
            }
        }

        struct SubsetGroup 
        {
            public SubsetGroup(ushort entryPoint, IList<FATransition<T>> transitions)
            {
                EntryPoint = entryPoint;
                Transitions = transitions;
            }

            public ushort EntryPoint { get; }

            public IList<FATransition<T>> Transitions { get; }
        }

        class CreateGraphMemoryModel : IShiftRightMemoryModel
        {
            private readonly static FATransition<T>[] EMPTY = new FATransition<T>[0];

            private IList<FATransition<T>> mTransitions;
            private Dictionary<ushort, SubsetGroup> mGroups;
            private IDictionary<ushort, List<FATransition<T>>> mLeftTrans;
            private IDictionary<ushort, List<FATransition<T>>> mRightTrans;

            public CreateGraphMemoryModel(IList<FATransition<T>> transitions, IList<FATransition<T>> subsets) 
            {
                mTransitions = transitions;
                mLeftTrans = transitions.GroupBy(x => x.Right).ToDictionary(x => x.Key, x => x.ToList());
                mRightTrans = transitions.GroupBy(x => x.Left).ToDictionary(x => x.Key, x => x.ToList());
                mGroups = subsets.GroupBy(x => x.Symbol.Value).Select(x => new SubsetGroup(x.Key, x.ToList())).ToDictionary(x => x.EntryPoint);
            }

            public SubsetGroup? GetOne() 
            {
                if (mGroups.Count > 0)
                {
                    var group = mGroups.First();
                    mGroups.Remove(group.Key);
                    return group.Value;
                }

                return null;
            }

            public IList<FATransition<T>> GetLefts(ushort state)
            {
                return mLeftTrans.ContainsKey(state) ? mLeftTrans[state] : EMPTY;
            }

            public IList<FATransition<T>> GetRights(ushort state)
            {
                return mRightTrans.ContainsKey(state) ? mRightTrans[state] : EMPTY;
            }

            public void Add(FATransition<T> tran)
            {
                if (!mRightTrans.ContainsKey(tran.Left))
                    mRightTrans[tran.Left] = new List<FATransition<T>>();

                if (!mRightTrans[tran.Left].Contains(tran))
                {
                    if (mLeftTrans != null && !mLeftTrans.ContainsKey(tran.Right))
                        mLeftTrans[tran.Right] = new List<FATransition<T>>();

                    mTransitions.Add(tran);
                    mLeftTrans[tran.Right].Add(tran);
                    mRightTrans[tran.Left].Add(tran);

                    if (tran.Symbol.Value != 0 && mGroups.ContainsKey(tran.Symbol.Value))
                    {
                        var group = mGroups[tran.Symbol.Value];
                        group.Transitions.Add(tran);
                    }
                }
            }

            public void Insert(int index, FATransition<T> tran)
            {
                if (!mRightTrans.ContainsKey(tran.Left))
                    mRightTrans[tran.Left] = new List<FATransition<T>>();

                if (mLeftTrans != null && !mLeftTrans.ContainsKey(tran.Right))
                    mLeftTrans[tran.Right] = new List<FATransition<T>>();

                mLeftTrans[tran.Right].Insert(index, tran);
                mRightTrans[tran.Left].Insert(index, tran);
                mTransitions.Add(tran);

                if (tran.Symbol.Value != 0 && mGroups.ContainsKey(tran.Symbol.Value))
                {
                    var group = mGroups[tran.Symbol.Value];
                    group.Transitions.Add(tran);
                }
            }

            public void Remove(FATransition<T> tran)
            {
                if (mLeftTrans != null && mLeftTrans.ContainsKey(tran.Right))
                    mLeftTrans[tran.Right].Remove(tran);

                if (mRightTrans.ContainsKey(tran.Left))
                    mRightTrans[tran.Left].Remove(tran);

                mTransitions.Remove(tran);

                if (tran.Symbol.Value != 0 && mGroups.ContainsKey(tran.Symbol.Value))
                {
                    var group = mGroups[tran.Symbol.Value];
                    group.Transitions.Remove(tran);
                }
            }

            public bool Contains(ushort state)
            {
                return mRightTrans.ContainsKey(state);
            }

            public bool Contains(FATransition<T> tran)
            {
                return mRightTrans[tran.Left].Contains(tran);
            }
        }

        /// <summary>
        /// 从用户Graph中创建FA状态机
        /// </summary>
        private static IList<FATransition<T>> CreateGraph<TVertex, TEdge>(IGraph<TVertex, TEdge> graph, int dataCount, int stateCount, Func<TEdge, T> metadataSelector, FATableFlags flags, TransitionMetadata<char, bool> connect, FAActionTable actionTable)
            where TEdge : IEdge<TVertex>
            where TVertex : IVertex
        {
            // 得到静态
            var transitions = new List<FATransition<T>>();
            var dynamicbag = new List<FATransition<T>>();
            var optional = new TransitionMetadata<char, bool>();

            // 得到原始数据
            foreach (var edge in graph.Edges)
            {
                var symbol = new FASymbol(FASymbolType.None, edge.Subset?.Index ?? 0);
                var metadata = metadataSelector == null ? default : metadataSelector(edge);
                var chars = edge.GetLinkChars(dataCount);
                var trans = chars.Select(x => new FATransition<T>(edge.Source.Index, edge.Target.Index, edge.Source.Index, edge.Target.Index, x, symbol, metadata));

                foreach (var tran in trans)
                {
                    transitions.Add(tran);
                    if (edge.ConnectSubset)
                    {
                        dynamicbag.Add(tran);
                        connect[tran.Left, tran.Right, tran.Input] = true;
                    }

                    if (edge.Optional) optional[tran.Left, tran.Right, tran.Input] = true;
                }            
            }

            var memory = new CreateGraphMemoryModel(transitions, dynamicbag);

            // 设置所有请求/汇报符号和修正本地递归
            SubsetGroup? group = null;
            while((group = memory.GetOne()).HasValue)
            {
                var subset = group.Value.EntryPoint;
                var externals = group.Value.Transitions;
                var internals = new List<FATransition<T>>();
                var recursions = new Dictionary<FATransition<T>, int>();

                // 查找内部头部至有效数据(符号集和单个input)
                IEnumerable<SubsetEntry> ShiftRight(List<FATransition<T>> list, FATransition<T> tran)
                {
                    var isSubset = connect[tran.Left, tran.Right, tran.Input];
                    if (isSubset)
                    {
                        if (recursions.ContainsKey(tran))
                        {
                            recursions[tran]++;
                        }
                    }

                    if (!recursions.ContainsKey(tran))
                    {
                        list.Add(tran);
                        if (!isSubset)
                        {
                            // 修改引用符号,可优化
                            for (var i = 0; i < list.Count - 1; i++)
                            {
                                var item = list[i];
                                System.Diagnostics.Debug.Assert(item.Symbol.Value != 0);

                                // 移除内部左递归
                                if (item.Symbol.Value == subset) 
                                {
                                    list.RemoveAt(i);
                                    i--;
                                    continue;
                                }

                                list[i] = new FATransition<T>(
                                   item.Left,
                                   item.Right,
                                   item.SourceLeft,
                                   item.SourceRight,
                                   item.Input,
                                   new FASymbol(FASymbolType.Request | item.Symbol.Type, item.Symbol.Value), item.Metadata);
                            }

                            yield return new SubsetEntry(list.ToArray());
                        }
                        else
                        {
                            var index = tran.Symbol.Value;
                            recursions[tran] = 0;

                            foreach (var right in memory.GetRights(index))
                            {
                                foreach (var next in ShiftRight(list, right))
                                {
                                    yield return next;
                                }
                            }
                        }

                        list.Remove(tran);
                    }
                }

                var starts = memory.GetRights(subset).SelectMany(x => ShiftRight(new List<FATransition<T>>(), x)).Distinct().ToArray();
                if (starts.Length == 0)
                {
                    // 如果是循环递归的话则抛出异常
                    throw new LeftRecursionOverflowException<T>(recursions.OrderByDescending(x => x.Value).FirstOrDefault().Key);
                }

                // 查找内部尾部
                var ends = FindEnds(memory, subset, stateCount, fa =>
                {
                    if (externals.Contains(fa))
                    {
                        // 如果左递归不包含该移进则填入到内部引用
                        internals.Add(fa);
                        externals.Remove(fa);
                    }

                    return true;
                }).ToList();

                // 外部递归符号修改
                foreach (var refer in externals)
                {
                    foreach (var start in starts)
                    {
                        var dst = new FATransition<T>(
                           refer.Left,
                           refer.Right,
                           refer.SourceLeft,
                           refer.SourceRight,
                           start.Input,
                           CreateAction(new FASymbol(FASymbolType.Request | refer.Symbol.Type, subset), start.Symbols, actionTable),
                           refer.Metadata);

                        memory.Add(dst);
                    }

                    memory.Remove(refer);
                }

                // 填充尾递归
                for (var i = 0; i < ends.Count; i++)
                {
                    var refer = ends[i];
                    if (internals.Contains(refer))
                    {
                        ends.Remove(refer);
                        i--;

                        // 如果是可选的
                        if (optional[refer.Left, refer.Right, refer.Input])
                        {
                            memory.Remove(refer);
                            ends.AddRange(memory.GetLefts(refer.Left));
                        }
                    }
                }

                // 处理左递归
                var leftRecursions = new HashSet<FATransition<T>>();
                foreach (var refer in recursions.Where(x => internals.Contains(x.Key))) 
                {
                    foreach (var end in ends)
                    {
                        var dst = new FATransition<T>(
                            end.Right,
                            refer.Key.Right,
                            refer.Key.SourceLeft,
                            refer.Key.SourceRight,
                            refer.Key.Input,
                            refer.Key.Symbol,
                            refer.Key.Metadata);

                        internals.Add(dst);
                        leftRecursions.Add(dst);
                    }

                    internals.Remove(refer.Key);
                    memory.Remove(refer.Key);
                }
   
                // 处理线性递归改为循环并修改符号
                foreach (var refer in internals)
                {
                    // 移除该递归
                    memory.Remove(refer);

                    // 处理特殊尾递归
                    var list = memory.GetRights(refer.Right).ToArray();

                    // 处理线性递归连接回右侧
                    for (int i = 0; i < list.Length; i++)
                    {
                        FATransition<T> right = list[i];
                        if (refer.Left != right.Left)
                        {
                            memory.Remove(right);
                            if (ends.Contains(right))
                            {
                                ends.Remove(right);
                            }
                            else
                            {
                                var dst = new FATransition<T>(
                                    refer.Left,
                                    right.Right,
                                    right.SourceLeft,
                                    right.SourceRight,
                                    right.Input,
                                    right.Symbol,
                                    right.Metadata);

                                memory.Add(dst);
                            }
                        }
                    }

                    // 应用新转换点
                    if (!leftRecursions.Contains(refer))
                    {
                        foreach (var start in starts)
                        {
                            var dst = new FATransition<T>(
                                refer.Left,
                                start.Right,
                                start.SourceLeft,
                                start.SourceRight,
                                start.Input,
                                CreateAction(default, start.Symbols, actionTable),
                                start.Metadata);

                            memory.Add(dst);
                        }
                    }
                }

                // 出口符号修改
                foreach (var end in ends)
                {
                    var endSymbol = connect[end.Left, end.Right, end.Input] ? new FASymbol(FASymbolType.Request | end.Symbol.Type, end.Symbol.Value) : end.Symbol;

                    var dst = new FATransition<T>(
                        end.Left,
                        end.Right,
                        end.SourceLeft,
                        end.SourceRight,
                        end.Input,
                        CreateAction(endSymbol, new FASymbol[] { new FASymbol(FASymbolType.Report | end.Symbol.Type, subset) }, actionTable),
                        end.Metadata);

                    memory.Remove(end);
                    memory.Add(dst);
                }
            } 

            return transitions;
        }

        // 根据符号获得或创建动作
        private static FASymbol CreateAction(FASymbol first, IEnumerable<FASymbol> symbols, FAActionTable actionTable)
        {
            var id = 0;
            var stack = new Stack<FASymbol>(symbols.Reverse());
            if (!first.Equals(default))
                stack.Push(first);

            var list = new List<FASymbol>(stack.Count);
            while (stack.Count > 0)
            {
                var symbol = stack.Pop();
                if (symbol.Type == FASymbolType.None)
                    continue;

                if (symbol.Type == FASymbolType.Action)
                {
                    throw new NotImplementedException("action can not combine.");
                }
                else
                {
                    id = HashCode.Combine(id, symbol);
                }

                list.Add(symbol);
            }

            if (list.Count == 0)
                return default;

            if (list.Count == 1)
                return list[0];

            if (!actionTable.Contains(id))
                actionTable[id] = new FAAction((ushort)actionTable.Count, list);

            return new FASymbol(FASymbolType.Action, actionTable.FromId(id).Index);
        }

        #endregion

        #region 最小化

        class MinimizeResult
        {
            public IList<MinimizeGroup> Mergeables { get; }

            public MinimizeResult(IList<MinimizeGroup> mergeables)
            {
                Mergeables = mergeables;
            }
        }

        struct MinimizeGroup
        {
            private IList<MinimizeGroupItem> mItems;

            public MinimizeGroup(IList<MinimizeGroupItem> items)
            {
                mItems = items;
            }

            public bool Merged => mItems.Count > 1;

            public MinimizeGroupItem Target => mItems[0];

            public IList<MinimizeGroupItem> Items => mItems;
        }

        struct MinimizeGroupItem
        {
            public MinimizeGroupItem(int hashCode, MinimizeCompareKey compreKey)
            {
                HashCode = hashCode;
                CompareKey = compreKey;
            }

            public int HashCode { get; }

            public MinimizeCompareKey CompareKey { get; }

            public ushort Left => CompareKey.Transition.Left;

            public ushort Right => CompareKey.Transition.Right;
        }

        struct MinimizeCompareKey
        {
            public int BranchIndex { get; }

            public int BranchLevel { get; }

            public int LeftBranchIndex { get; }

            public int CompredCount { get; }

            public FATransition<T> Transition { get; }

            public MinimizeCompareKey(int branch, int branchLevel, int leftBranch, int compredCount, FATransition<T> transition)
            {
                BranchIndex = branch;
                BranchLevel = branchLevel;
                LeftBranchIndex = leftBranch;
                CompredCount = compredCount;
                Transition = transition;
            }

            public override string ToString()
            {
                return Transition.ToString();
            }

            public override bool Equals(object obj)
            {
                return obj is MinimizeCompareKey key && Transition.Equals(key.Transition);
            }

            public override int GetHashCode()
            {
                return Transition.GetHashCode();
            }
        }

        class MinimizeMemoryModel : IShiftRightMemoryModel
        {
            private readonly static FATransition<T>[] EMPTY = new FATransition<T>[0];

            private IList<FATransition<T>> mTransitions;
            private IDictionary<ushort, List<FATransition<T>>> mRightTrans;
            private IDictionary<ushort, List<FATransition<T>>> mLeftTrans;

            public MinimizeMemoryModel(IList<FATransition<T>> transitions)
                : this(transitions, false)
            {
            }

            public MinimizeMemoryModel(IList<FATransition<T>> transitions, bool enableLeft)
            {
                mTransitions = transitions;
                mRightTrans = transitions.GroupBy(x => x.Left).ToDictionary(x => x.Key, x => x.ToList());
                if (enableLeft) mLeftTrans = transitions.GroupBy(x => x.Right).ToDictionary(x => x.Key, x => x.ToList());
            }

            public IList<FATransition<T>> GetLefts(ushort state)
            {
                return mLeftTrans.ContainsKey(state) ? mLeftTrans[state] : EMPTY;
            }

            public IList<FATransition<T>> GetRights(ushort state)
            {
                return mRightTrans.ContainsKey(state) ? mRightTrans[state] : EMPTY;
            }

            public void Add(FATransition<T> tran)
            {
                if (mLeftTrans != null && !mLeftTrans.ContainsKey(tran.Right))
                    mLeftTrans[tran.Right] = new List<FATransition<T>>();

                if (!mRightTrans.ContainsKey(tran.Left))
                    mRightTrans[tran.Left] = new List<FATransition<T>>();

                if (mLeftTrans != null) mLeftTrans[tran.Right].Add(tran);
                mTransitions.Add(tran);
                mRightTrans[tran.Left].Add(tran);
            }

            public void Insert(int index, FATransition<T> tran)
            {
                if (mLeftTrans != null && !mLeftTrans.ContainsKey(tran.Right))
                    mLeftTrans[tran.Right] = new List<FATransition<T>>();

                if (!mRightTrans.ContainsKey(tran.Left))
                    mRightTrans[tran.Left] = new List<FATransition<T>>();

                if (mLeftTrans != null) mLeftTrans[tran.Right].Insert(index, tran);
                mRightTrans[tran.Left].Insert(index, tran);
                mTransitions.Add(tran);
            }

            public void Remove(FATransition<T> tran)
            {
                if (mLeftTrans != null && mLeftTrans.ContainsKey(tran.Right))
                    mLeftTrans[tran.Right].Remove(tran);

                if (mRightTrans.ContainsKey(tran.Left))
                    mRightTrans[tran.Left].Remove(tran);

                mTransitions.Remove(tran);
            }

            public bool Contains(FATransition<T> tran) 
            {
                return mRightTrans[tran.Left].Contains(tran);
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        private static IEnumerable<FABuildStep<T>> Minimize(IList<FATransition<T>> transitions, IEnumerable<ushort> checkPoints)
        {
            var memory = new MinimizeMemoryModel(transitions, true);
            foreach (var point in checkPoints) 
            {
                // 创建初始布局
                var level = 1;
                var branch = 1;
                var branchs = new Dictionary<int, int>();
                int GetNewBranch(int left)
                {
                    branchs[branch] = left;
                    return branch++;
                }

                // 去重
                var nexts = new HashSet<MinimizeCompareKey>(memory.GetRights(point).Select(x => new MinimizeCompareKey(GetNewBranch(0), level, 0, 0, x)));
                do
                {
                    if (nexts.Count == 0) break;
       
                    // 获得可合并分组
                    var mergeResult = GetMinimizeGroups(memory, nexts, branchs);

                    // 清空下次缓存
                    nexts.Clear();

                    // 对可合并分组进行进行合并,首先处理合并的分支
                    for (int i = 0; i < mergeResult.Mergeables.Count; i++)
                    {
                        MinimizeGroup group = mergeResult.Mergeables[i];
                        // 合并成功插入该边左侧,否则保留该边
                        if (group.Merged)
                        {
                            for (int z = 1; z < group.Items.Count; z++)
                            {
                                MinimizeGroupItem item = group.Items[z];

                                var rights = memory.GetRights(item.CompareKey.Transition.Right).ToArray();
                                for (int x = 0; x < rights.Length; x++)
                                {
                                    FATransition<T> right = rights[x];
                                    memory.Remove(right);
                                    yield return new FABuildStep<T>(FABuildStage.Minimize, FABuildType.Delete, right);

                                  
                                    var appendTran = new FATransition<T>(
                                        group.Target.Right,
                                        right.Right,
                                        right.SourceLeft,
                                        right.SourceRight,
                                        right.Input,
                                        right.Symbol,
                                        right.Metadata);

                                    if (!memory.Contains(appendTran))
                                    {
                                        memory.Insert(0, appendTran);
                                        yield return new FABuildStep<T>(FABuildStage.Minimize, FABuildType.Add, appendTran);
                                    }
                                }

                                var lefts = memory.GetLefts(item.CompareKey.Transition.Right).ToArray();
                                for (var x = 0; x < lefts.Length; x++)
                                {
                                    FATransition<T> left = lefts[x];
                                    memory.Remove(left);
                                    yield return new FABuildStep<T>(FABuildStage.Minimize, FABuildType.Delete, left);

                                    var appendTran = new FATransition<T>(
                                        left.Left,
                                        group.Target.Right,
                                        left.SourceLeft,
                                        left.SourceRight,
                                        left.Input,
                                        left.Symbol,
                                        left.Metadata);

                                    if (!memory.Contains(appendTran))
                                    {
                                        memory.Insert(0, appendTran);
                                        yield return new FABuildStep<T>(FABuildStage.Minimize, FABuildType.Add, appendTran);
                                    }
                                }
                            }
                        }

                        {
                            // 保留该次进度
                            nexts.Add(new MinimizeCompareKey(
                                group.Target.CompareKey.BranchIndex,
                                group.Target.CompareKey.BranchLevel,
                                group.Target.CompareKey.LeftBranchIndex,
                                group.Target.CompareKey.CompredCount + 1,
                                group.Target.CompareKey.Transition));

                            // 插入新任务
                            if (group.Target.CompareKey.CompredCount == 0)
                            {
                                var rights = memory.GetRights(group.Target.CompareKey.Transition.Right);
                                var nextLevel = rights.Count > 1 ? level++ : group.Target.CompareKey.BranchLevel;
                                var leftBranch = rights.Count > 1 ? group.Target.CompareKey.BranchIndex : group.Target.CompareKey.LeftBranchIndex;
                                foreach (var next in rights)
                                {
                                    nexts.Add(new MinimizeCompareKey(
                                        rights.Count > 1 ? GetNewBranch(leftBranch) : group.Target.CompareKey.BranchIndex,
                                        nextLevel,
                                        leftBranch,
                                        0,
                                        next));
                                }
                            }
                        }
                    }
                }
                while (nexts.Where(x => x.CompredCount == 0).Count() > 0);
            }
        }

        /// <summary>
        /// 获取可去重分组
        /// </summary>
        private static MinimizeResult GetMinimizeGroups(MinimizeMemoryModel layout, IEnumerable<MinimizeCompareKey> keys, Dictionary<int, int> branchs)
        {
            // 获得索引映射值
            var items = keys.ToArray();
            var indexs = new Dictionary<ushort, int>();
            for (int i = 0; i < items.Length; i++)
            {
                var index = items[i].Transition.Right;
                if (!indexs.ContainsKey(index))
                {
                    indexs[index] = indexs.Count;
                }
            }

            // 创建算子
            var equalHashs = new int[indexs.Count];
            var points = keys.Select(x => x.Transition.Right).ToHashSet();
            var operators = points.SelectMany(x => layout.GetRights(x)).ToList();

            // 得到数据包含信息结构,抽出下级最大一致部分
            var groups = new List<MinimizeGroup>();
            foreach (var oper in operators)
            {
                var index = indexs[oper.Left];
                equalHashs[index] = HashCode.Combine(
                    equalHashs[index],
                    oper.Input,
                    oper.Metadata,
                    oper.Symbol);
            }

            // 一组数据完全相等于另一组数据时可以进行合并
            var mergeIndex = 0;
            var mergeGroups = equalHashs.
                Select(x => new MinimizeGroupItem(x, items[mergeIndex++])).
                GroupBy(x => x.HashCode);

            // 移除上级匹配点,首先获得移除分支集合,并提供分组
            foreach (var group in mergeGroups)
            {
                // 排除所有前置分支,首先进行level排序
                var sortItems = group.OrderByDescending(x => x.CompareKey.BranchLevel).ToList();
                for (var i = 0; i < sortItems.Count; i++) 
                {
                    // 找到所有左侧连接并移除
                    var key = sortItems[i].CompareKey;
                    var index = key.LeftBranchIndex;
                    while (index != -1) 
                    {
                        sortItems.RemoveAll(x => x.CompareKey.BranchIndex == index || (x.CompareKey.BranchIndex == key.BranchIndex && x.CompareKey.CompredCount > key.CompredCount));
                        index = branchs.ContainsKey(index) ? branchs[index] : -1;
                    }
                }
              
                if (sortItems.Count > 0)
                    groups.Add(new MinimizeGroup(sortItems));
            }

            // 处理keeps
            return new MinimizeResult(groups);
        }

        #endregion

        #region 解决冲突

        struct TrigonometricOperator 
        {
            public TrigonometricOperator(FATransition<T> shortSide, FATransition<T> longSide, FATransition<T> bridge)
            {
                ShortSide = shortSide;
                LongSide = longSide;
                Bridge = bridge;
            }

            public FATransition<T> ShortSide { get; }

            public FATransition<T> LongSide { get; }

            public FATransition<T> Bridge { get; }
        }

        class ConflictResolutionMemoryModel : IEnumerable<List<FATransition<T>>>, IShiftRightMemoryModel
        {
            private readonly static FATransition<T>[] EMPTY = new FATransition<T>[0];

            private IList<FATransition<T>> mTransitions;
            private Dictionary<ushort, List<FATransition<T>>> mRigts;  
            private Dictionary<ushort,  Dictionary<char, List<FATransition<T>>>> mDict;

            public ConflictResolutionMemoryModel(IList<FATransition<T>> transitions) 
            {
                mTransitions = transitions;
                mRigts = transitions.GroupBy(x => x.Left).
                    ToDictionary(x => x.Key, x => x.ToList());

                mDict = transitions.GroupBy(x => x.Left).
                    ToDictionary(y => y.Key, y => y.GroupBy(y => y.Input).
                    ToDictionary(z => z.Key, z => z.ToList()));
            }

            public IList<FATransition<T>> GetOne() 
            {
                return this.FirstOrDefault();
            }

            public IList<FATransition<T>> GetRights(ushort state) 
            {
                if (!mRigts.ContainsKey(state))
                    return EMPTY;

                return mRigts[state];
            }

            public void Add(FATransition<T> tran) 
            {
                if (!mDict.ContainsKey(tran.Left))
                    mDict[tran.Left] = new Dictionary<char, List<FATransition<T>>>();

                if (!mDict[tran.Left].ContainsKey(tran.Input))
                    mDict[tran.Left].Add(tran.Input, new List<FATransition<T>>());

                if (!mRigts.ContainsKey(tran.Left))
                    mRigts[tran.Left] = new List<FATransition<T>>();

                mTransitions.Add(tran);
                mRigts[tran.Left].Add(tran);
                mDict[tran.Left][tran.Input].Add(tran);
            }

            public void Remove(FATransition<T> tran) 
            {
                if (!mDict.ContainsKey(tran.Left))
                    return;

                if (!mDict[tran.Left].ContainsKey(tran.Input))
                    return;

                mTransitions.Remove(tran);
                mRigts[tran.Left].Remove(tran);
                mDict[tran.Left][tran.Input].Remove(tran);
            }

            public void Insert(int index, FATransition<T> tran)
            {
                if (!mRigts.ContainsKey(tran.Left))
                    mRigts[tran.Left] = new List<FATransition<T>>();

                mRigts[tran.Left].Insert(index, tran);
                mTransitions.Add(tran);
            }

            public IEnumerator<List<FATransition<T>>> GetEnumerator()
            {
                return  mDict.SelectMany(x => x.Value.Where(y => y.Value.Count > 1).Select(y => y.Value)).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        /// <summary>
        /// 解决连接冲突
        /// </summary>
        /// <param name="transitions">移进状态</param>
        /// <param name="flags">冲突检测选项</param>
        /// 
        protected virtual IEnumerable<FABuildStep<T>> ConflictResolution(IList<FATransition<T>> transitions, ConflictsDetectionFlags flags)
        {
            IEnumerable<TrigonometricOperator> Trigonometric(ConflictResolutionMemoryModel memroy, IList<FATransition<T>> transitions) 
            {
                var edges = ToTwoKeyDictionary(transitions, x => x.Left, x => x.Right, x => x);
                var trigs = new TwoKeyDictionary<ushort, ushort, FATransition<T>>();
                for (var x = 0; x < transitions.Count; x++)
                {
                    for (var y = 0; y < transitions.Count; y++) 
                    {
                        if (x == y) continue;

                        var left = transitions[x];
                        var right = transitions[y];

                        trigs[left.Right, right.Right] = right;
                    }
                }

                var opers = transitions.SelectMany(x => memroy.GetRights(x.Right));
                foreach (var oper in opers) 
                {
                    if (trigs.ContainsKey(oper.Left, oper.Right)) 
                    {
                        var shortSide = trigs[oper.Left, oper.Right];
                        var longSide = edges[transitions[0].Left, oper.Left];
                        yield return new TrigonometricOperator(shortSide, longSide, oper);
                    }   
                } 
            }

            FATransition<T> FindLongSide(IEnumerable<TrigonometricOperator> trigs) 
            {
                var longSide = new List<FATransition<T>>();
                foreach (var trig in trigs)
                    longSide.Add(trig.LongSide);

                foreach (var trig in trigs)
                    longSide.Remove(trig.ShortSide);

                return longSide.First();
            }

            var memory = new ConflictResolutionMemoryModel(transitions);
            var conflictCount = new Dictionary<int, int>();
            var conflicts = memory.GetOne();
            while (conflicts != null) 
            {
                // 移除完全相同部分
                var conflictsCount = conflicts.Count;
                var sameList = conflicts.GroupBy(x => x).Select(x => x.ToArray()).ToArray();
                for (var i = 0; i < sameList.Length; i++)
                {
                    var list = sameList[i];
                    for (var x = 1; x < list.Length; x++)
                    {
                        var tran = list[x];
                        memory.Remove(tran);
                        conflictsCount--;
                    }
                }

                if (conflictsCount < 2) 
                {
                    // 得到下次任务
                    conflicts = memory.GetOne();
                    continue;
                }

                // 获取最大符号冲突
                FASymbol symbol = default;
                var symbolTrans = conflicts.GroupBy(x => x.Symbol).Select(x => new SymbolGroup<T>(x.Key, x.ToArray())).ToArray();
                if (symbolTrans.Length > 1)
                {
                    // 解决符号冲突
                    if (!(flags.HasFlag(ConflictsDetectionFlags.Symbol) && SymbolConflictResolution(memory, symbolTrans, out symbol)))
                        // 符号不同则抛出
                        throw new SymbolConflictException<T>(symbolTrans);
                }
                else symbol = symbolTrans[0].Transitions[0].Symbol;

                // 元数据冲突
                T metadata = default;
                var metadataTrans = conflicts.GroupBy(x => x.Metadata).Select(x => new MetadataGroup<T>(x.Key, x.ToArray())).ToArray();
                if (metadataTrans.Length > 1)
                {
                    // 解决元数据冲突
                    if (!(flags.HasFlag(ConflictsDetectionFlags.Metadata) && MetadataConflictResolution(memory, metadataTrans, out metadata)))
                        // 元数据不同则抛出
                        throw new MetadataConflictException<T>(metadataTrans);
                }
                else metadata = metadataTrans[0].Transitions[0].Metadata;

                // 找到三角冲突
                var trigs = Trigonometric(memory, conflicts).ToArray();
                var keeps = trigs.SelectMany(x => memory.GetRights(x.ShortSide.Right)).ToHashSet();
                var intersection = trigs.Length > 0 ? FindLongSide(trigs) : conflicts[0];

                // 构建状态集
                var conflictsArray = conflicts.ToArray();
                for (int i = 0; i < conflictsArray.Length; i++)
                {
                    FATransition<T> transition = conflictsArray[i];
                    memory.Remove(transition);
                    yield return new FABuildStep<T>(FABuildStage.ConflictResolution, FABuildType.Delete, transition);

                    // 移动所有该点右侧到目标点
                    if (!transition.Equals(intersection)) 
                    {
                        var rights = memory.GetRights(transition.Right).ToArray();
                        foreach (var right in rights) 
                        {
                            if (!keeps.Contains(right))
                            {
                                memory.Remove(right);
                                yield return new FABuildStep<T>(FABuildStage.ConflictResolution, FABuildType.Delete, transition);
                            }

                            var move = new FATransition<T>(intersection.Right, right.Right, right.SourceLeft, right.SourceRight, right.Input, right.Symbol, right.Metadata, right.InputCount);
                            memory.Add(move);
                            yield return new FABuildStep<T>(FABuildStage.ConflictResolution, FABuildType.Add, move);
                        }
                    }

                    // 添加冲突计数
                    for (int i1 = 0; i1 < conflictsArray.Length; i1++)
                    {
                        if (i == i1) continue;

                        FATransition<T> other = conflictsArray[i1];
                        var confictCode = HashCode.Combine(transition.GetHashCode(), other.GetHashCode());
                        if (!conflictCount.ContainsKey(confictCode))
                            conflictCount[confictCode] = 0;

                        if (++conflictCount[confictCode] > 1)
                        {
                            // 如果同一条边超过2次冲突则代表无限冲突
                            throw new ConflictException<T>(transition, other, transitions, Actions);
                        }
                    }
                }

                // 还原合并集
                var merged = new FATransition<T>(
                    intersection.Left,
                    intersection.Right, 
                    intersection.SourceLeft, 
                    intersection.SourceRight, 
                    intersection.Input,
                    symbol,
                    metadata);

                memory.Add(merged);
                yield return new FABuildStep<T>(FABuildStage.ConflictResolution, FABuildType.Add, merged);

                // 得到下次任务
                conflicts = memory.GetOne();
            }
        }

        /// <summary>
        /// 当元数据发生冲突时
        /// </summary>
        protected virtual bool MetadataConflictResolution(IShiftRightMemoryModel memroy, MetadataGroup<T>[] groups, out T result)
        {
            // ??? 元数据可能会被抛弃
            throw new MetadataConflictException<T>(groups);

            // 后移策略,找到符号不同的组，并将小分组后移
            var conflicts = new List<MetadataGroup<T>>();
            var sortGroups = groups.OrderBy(x => x.Transitions.Count).ToArray();
            for (int i = 0; i < sortGroups.Length - 1; i++)
            {
                var conflictTrans = new List<FATransition<T>>();
                var group = sortGroups[i];

                foreach (var next in group.Transitions)
                {
                    var rights = memroy.GetRights(next.Right);
                    if (rights.Count == 0)
                    {
                        conflictTrans.Add(next);
                    }
                    else
                    {
                        foreach (var right in rights)
                        {
                            memroy.Remove(right);
                            memroy.Add(new FATransition<T>(
                                right.Left,
                                right.Right,
                                right.SourceLeft,
                                right.SourceRight,
                                right.Input,
                                right.Symbol,
                                right.Metadata));
                        }
                    }
                }

                if (conflictTrans.Count > 0)
                    conflicts.Add(new MetadataGroup<T>(group.Metadata, conflictTrans));
            }

            if (conflicts.Count > 0)
                throw new MetadataConflictException<T>(conflicts);

            result = default;
            return true;
        }

        /// <summary>
        /// 当符号使用位发生冲突时
        /// </summary>
        protected virtual bool SymbolConflictResolution(IShiftRightMemoryModel memroy, SymbolGroup<T>[] groups, out FASymbol result)
        {
            // 后移策略,找到符号不同的组，并将小分组后移
            var conflicts = new List<SymbolGroup<T>>();
            var sortGroups = groups.OrderBy(x => x.Transitions.Count).ToArray();
            for (int i = 0; i < sortGroups.Length - 1; i++) 
            {
                var conflictTrans = new List<FATransition<T>>(); 
                var group = sortGroups[i];

                foreach (var next in group.Transitions)
                {
                    var rights = memroy.GetRights(next.Right);
                    if (rights.Count == 0)
                    {
                        conflictTrans.Add(next);
                    }
                    else
                    {
                        foreach (var right in rights)
                        {
                            // 如果是向子集请求
                            if (right.Symbol.Value != 0)
                            {
                                // TODO:推向子集下一步
                                var subRights = memroy.GetRights(right.Symbol.Value);
                                foreach (var subRight in subRights) 
                                {
                                    memroy.Add(new FATransition<T>(
                                        subRight.Left,
                                        subRight.Right,
                                        subRight.SourceLeft,
                                        subRight.SourceRight,
                                        subRight.Input,
                                        CreateAction(right.Symbol, new FASymbol[] { group.FASymbol }, Actions),
                                        right.Metadata));
                                }
                            }
                            else
                            {
                                memroy.Remove(right);
                                memroy.Add(new FATransition<T>(
                                    right.Left,
                                    right.Right,
                                    right.SourceLeft,
                                    right.SourceRight,
                                    right.Input,
                                    CreateAction(right.Symbol, new FASymbol[] { group.FASymbol }, Actions),
                                    right.Metadata));
                            }
                        }
                    }
                }

                if (conflictTrans.Count > 0)
                    conflicts.Add(new SymbolGroup<T>(group.FASymbol, conflictTrans));
            }

            if (conflicts.Count > 0)
                throw new SymbolConflictException<T>(conflicts);

            result = default;
            return true;
        }

        #endregion

        #region 优化

        /// <summary>
        /// 优化DFA表
        /// </summary>
        private IEnumerable<FABuildStep<T>> Optimize(IList<FATransition<T>> transitions, IEnumerable<ushort> checkPoints, FATableFlags flags)
        {
            // 从状态1进入,并查找有效的连接
            var groups = transitions.GroupBy(x => x.Left).ToDictionary(x => x.Key);
            var skipSize = flags.HasFlag(FATableFlags.Allow0Rights);
            bool[] visitor = new bool[StateCapacity + 1];
            int[] size = new int[StateCapacity + 1];

            visitor[0] = true;
            Queue<FATransition<T>> queue = new Queue<FATransition<T>>();
            foreach (var point in checkPoints) 
            {
                if (groups.ContainsKey(point))
                {
                    foreach (var item in groups[point])
                    {
                        queue.Enqueue(item);
                    }
                }

                visitor[point] = true;
            }

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                if (visitor[item.Right])
                    continue;

                visitor[item.Right] = true;
                if (groups.ContainsKey(item.Right))
                {
                    var items = groups[item.Right].ToArray();
                    size[item.Right] = items.Length;

                    foreach (var next in items)
                        queue.Enqueue(next);
                }
            }

            IList<FATransition<T>> newSet = new List<FATransition<T>>();
            for (var i = 0; i < transitions.Count; i++)
            {
                var tran = transitions[i];
                if (visitor[tran.Left] && visitor[tran.Right] && (skipSize || tran.Right == 0 || size[tran.Right] > 0))
                {
                    newSet.Add(tran);
                }
            }

            // 记录当前状态数量
            var stateCount = StateCapacity;

            // 拟合段合并
            if (flags.HasFlag(FATableFlags.FitFragmentMerge))
            {
                newSet = FitFragmentMerge(transitions, checkPoints, ref stateCount);
                MoveSubsets(newSet, ref stateCount, Actions);
            }

            // 重新标记号码，优化并安排内存布局
            if (flags.HasFlag(FATableFlags.MemoryLayout))
                newSet = LayoutTransitions(newSet, checkPoints, ref stateCount, Actions);

            foreach (var tran in transitions.Except(newSet))
                yield return new FABuildStep<T>(FABuildStage.Optimize, FABuildType.Delete, tran);

            foreach (var tran in newSet.Except(transitions))
                yield return new FABuildStep<T>(FABuildStage.Optimize, FABuildType.Add, tran);

            StateCapacity = stateCount;
            Transitions = newSet;
        }

        /// <summary>
        /// 线性合并
        /// TODO:当前只能进行线性优化，未来使用Biguns.Vector库进行拟合计算
        /// </summary>
        private static IList<FATransition<T>> FitFragmentMerge(IList<FATransition<T>> transitions, IEnumerable<ushort> checkPoints, ref int stateCount)
        {
            var leftTrans = transitions.GroupBy(x => x.Right).ToDictionary(x => x.Key, x => x.ToArray());
            var rightTrans = transitions.GroupBy(x => x.Left).ToDictionary(x => x.Key, x => x.ToArray());
   
            // 从入口点查询
            var entryVisitor = new Dictionary<ushort, bool>();
            var entries = checkPoints.ToArray();
            var nextEntries = new Dictionary<ushort, bool>();
            var parts = new List<GraphPart>();

            // 得到所有引用值
            var refereces = transitions.Where(x => x.Symbol.Value != 0).Select(x => x.Symbol.Value).ToHashSet();

            while (true) 
            {
                var sc = stateCount;
                for(var index = 0; index < entries.Length; index++)
                {
                    // 获得入口
                    var entry = entries[index];

                    // 循环检测退出
                    if (!entryVisitor.ContainsKey(entry))
                    {
                        entryVisitor[entry] = true;

                        var stateVisitor = new HashSet<ushort>();
                        var states = new List<FATransition<T>>();
                        var state = entry;
                        var setHead = false;
                        var headIndex = 0;

                        while (true)
                        {

                            // 循环检测退出
                            if (state != 0 && !stateVisitor.Contains(state))
                            {
                                stateVisitor.Add(state);
                                if (!rightTrans.ContainsKey(state))
                                    break;
                            
                                // 向右侧查找
                                var rights = rightTrans[state];

                                // 检查分叉状态
                                switch (rights.Length) 
                                {
                                    case 1:
                                        var tran = rights[0];
                                        if (states.Count > 0)
                                        {
                                            var statTran = states[headIndex];
                         
                                            // 检查符号是否相同,可进行同请求组合
                                            var vaild = tran.Symbol.Equals(default(FASymbol)) ||
                                                        statTran.Symbol.Equals(default(FASymbol)) ||
                                                        statTran.Symbol.Equals(tran.Symbol);

                                            if (!vaild)
                                            {
                                                if (tran.Symbol.Value == statTran.Symbol.Value)
                                                {
                                                    if ((statTran.Symbol.Type | tran.Symbol.Type) == FASymbolType.InGraph)
                                                    {
                                                        vaild = true;
                                                    }
                                                }
                                            }

                                            // 检查符合和元数据是否相同
                                            if (vaild) 
                                                vaild = tran.Metadata.Equals(default(T)) ||
                                                    statTran.Metadata.Equals(default(T)) ||
                                                    statTran.Metadata.Equals(tran.Metadata);

                                            // 检查左侧
                                            if (vaild && leftTrans.ContainsKey(state))
                                                vaild = leftTrans[state].Count(x => !entryVisitor.ContainsKey(x.Right) && !stateVisitor.Contains(x.Left)) == 0;

                                            if (!vaild) 
                                            {
                                                nextEntries[tran.Left] = true;
                                                break;
                                            }
                                        }

                                        if (refereces.Contains(tran.Left))
                                        {
                                            nextEntries[tran.Right] = true;
                                            break;
                                        }

                                        if (!setHead && tran.Symbol.Value != 0)
                                        {
                                            headIndex = states.Count;
                                            setHead = true;
                                        }

                                        // 修改状态
                                        state = tran.Right;
                                        states.Add(tran);
                                        continue;
                                    default:
                                        for (var i = 0; i < rights.Length; i++)
                                            nextEntries[rights[i].Right] = true;

                                        break;
                                }
                            }

                            break;
                        }

                        // 如果连接数大于1个则代表可以进行并行
                        if (states.Count > 1)
                            parts.Add(new GraphPart(states));

                        for (int i1 = 0; i1 < states.Count; i1++)
                        {
                            FATransition<T> visitor = states[i1];
                            entryVisitor[visitor.Left] = true;
                        }
                    }
                }

                if (nextEntries.Count <= 0) break;

                // 重设任务
                entries = nextEntries.Select(x => x.Key).ToArray();
                nextEntries.Clear();
            }

            // 整理part
            foreach (var part in parts) 
            {
                // 组合符号
                var symbol = FASymbol.Combine(part.Transitions.Select(x => x.Symbol));

                // 组合元数据
                var metadata = part.Transitions.SingleOrDefault(x => !x.Metadata.Equals(default(T))).Metadata;

                // 移除全部的部分
                foreach (var x in part.Transitions)
                    transitions.Remove(x);

                // 组合SIMD输入
                int input = 0;
                for (var i = 0; i < part.Transitions.Count; i++)
                    input |= input ^ (part.Transitions[i].Input << i);

                char c = (char)input;

                // 添加头尾
                var start = part.Transitions[0];
                var end = part.Transitions[^1];
                transitions.Add(new FATransition<T>(
                    start.Left,
                    end.Right,
                    start.SourceLeft,
                    end.SourceRight,
                    c,
                    symbol,
                    metadata,
                    part.Transitions.Count));
            }

            // 将单通道合并
            return transitions;
        }

        /// <summary>
        /// TODO:移进子集
        /// 原则，内部引用一定在CreateGraph处理成循环,且本函数内所有的Transition不包含递归
        /// </summary>
        private static void MoveSubsets(IList<FATransition<T>> transitions, ref int stateCount, FAActionTable actionTable) 
        {
            // 得到静态
            var states = stateCount;
            var groups = transitions.Where(x => x.Symbol.Value != 0).GroupBy(x => x.Symbol.Value).Select(x => new SubsetGroup(x.Key, x.ToArray())).ToDictionary(x => x.EntryPoint);
            foreach (var group in groups)
            {
                var trans = ForEach(transitions, group.Value.EntryPoint, stateCount).ToArray();
                var move = trans.Length <= 2;

                // 查找子集大小
                if (move) 
                {
                    // 遍历外部引用
                    foreach (var refer in group.Value.Transitions) 
                    {
                        // 得到前后插入的符号
                        var symbols = refer.Symbol.Type == FASymbolType.Action ? actionTable[refer.Symbol.Value].Symbols : new FASymbol[1] { refer.Symbol };
                        var requests = symbols.Where(x => x.Type == FASymbolType.Request).ToArray();
                        var reports = symbols.Where(x => x.Type == FASymbolType.Report).ToArray();


                    }
                }
            }
        }

        /// <summary>
        /// 内存布局优化
        /// </summary>
        private static IList<FATransition<T>> LayoutTransitions(IList<FATransition<T>> transitions, IEnumerable<ushort> checkPoints, ref int stateCount, FAActionTable actionTable) 
        {
            var group = transitions.GroupBy(x => x.Left).ToDictionary(x => x.Key);

            bool[] visitor = new bool[stateCount + 1];
            var index = new Dictionary<ushort, ushort>();
            var stack = new Stack<FATransition<T>>();

            visitor = new bool[stateCount];
            visitor[0] = true;
            index[0] = 0;

            foreach (var point in checkPoints) 
            {
                index[point] = (ushort)index.Count;
                visitor[point] = true;

                if (group.ContainsKey(point))
                {
                    foreach (var item in group[point])
                    {
                        stack.Push(item);
                    }
                }

                while (stack.Count > 0)
                {
                    var item = stack.Pop();
                    if (visitor[item.Right])
                        continue;

                    visitor[item.Right] = true;

                    if (!index.ContainsKey(item.Right))
                        index[item.Right] = (ushort)index.Count;

                    if (group.ContainsKey(item.Right))
                        foreach (var next in group[item.Right])
                            stack.Push(next);
                }
            }

            for (var i = 0; i < transitions.Count; i++)
            {
                var item = transitions[i];
                var symbol = item.Symbol;
                if (symbol.Type != FASymbolType.Action)
                    symbol = new FASymbol(symbol.Type, index[item.Symbol.Value]);

                transitions[i] = new FATransition<T>(
                    index[item.Left],
                    index[item.Right],
                    item.SourceLeft,
                    item.SourceRight,
                    item.Input,
                    symbol,
                    item.Metadata,
                    item.InputCount);
            }

            for (var i = 0; i < actionTable.Count; i++) 
            {
                var action = actionTable[i];
                for (var x = 0; x < action.Symbols.Count; x++) 
                {
                    var symbol = action.Symbols[x];
                    if (symbol.Type == FASymbolType.Action)
                        throw new NotImplementedException();

                    action.Symbols[x] = new FASymbol(symbol.Type, index[symbol.Value]);
                }
            }

            stateCount = index.Count;
            return transitions;
        }

        #endregion

        #region 函数

        private static IEnumerable<FATransition<T>> FindEnds(IShiftRightMemoryModel rights, ushort index, int stateCount) 
        {
            return FindEnds(rights, index, stateCount, null);
        }

        private static IEnumerable<FATransition<T>> FindEnds(IShiftRightMemoryModel rights, ushort index, int stateCount, Func<FATransition<T>, bool> step)
        {
            var visitor = new bool[stateCount];
            var queue = new Queue<ushort>();
            queue.Enqueue(index);
            while (queue.Count > 0)
            {
                var state = queue.Dequeue();
                if (visitor[state])
                    continue;

                visitor[state] = true;
                foreach (var right in rights.GetRights(state))
                {
                    var vaild = step == null || step(right);
                    if (vaild)
                    {
                        if ( rights.GetRights(right.Right).Count <= 0)
                        {
                            yield return right;
                        }
                        else
                        {
                            queue.Enqueue(right.Right);
                        }
                    }
                }
            }
        }

        private static IEnumerable<FATransition<T>> FindStarts(IDictionary<ushort, IList<FATransition<T>>> lefts, int stateCount, ushort index) 
        {
            var visitor = new bool[stateCount];
            var queue = new Queue<ushort>();
            queue.Enqueue(index);
            while (queue.Count > 0)
            {
                var state = queue.Dequeue();
                if (visitor[state])
                    continue;

                visitor[state] = true;
                if (lefts.ContainsKey(state))
                {
                    foreach (var left in lefts[state])
                    {
                        if (!lefts.ContainsKey(left.Left) || lefts[left.Left].Count <= 0)
                        {
                            yield return left;
                        }
                        else
                        {
                            queue.Enqueue(left.Right);
                        }
                    }
                }
            }
        }

        private static IEnumerable<FATransition<T>> FindSubsetRights(IDictionary<ushort, FATransition<T>[]> rights, ushort index, int stateCount, TransitionMetadata<char, bool> connect)
        {
            var visitor = new bool[stateCount];
            var queue = new Queue<ushort>();
            queue.Enqueue(index);

            while (queue.Count > 0)
            {
                var state = queue.Dequeue();
                if (visitor[state])
                    continue;

                visitor[state] = true;
                if (rights.ContainsKey(state))
                {
                    var nexts = rights[state];
                    for (var i = 0; i < nexts.Length; i++)
                    {
                        var next = nexts[i];
                        if (connect[next.Left, next.Right, next.Input])
                        {
                            queue.Enqueue(next.Symbol.Value);
                        }
                        else
                        {
                            yield return next;
                        }
                    }
                }
            }
        }

        private static IList<FATransition<T>> FindSubsetStarts(IDictionary<ushort, IList<FATransition<T>>> rights, ushort index, int stateCount, TransitionMetadata<char, bool> connect)
        {
            var visitor = new bool[stateCount];
            var starts = FindStarts(rights, stateCount, index).ToList();
            while (true)
            {
                var @break = true;
                for (var i = 0; i < starts.Count; i++)
                {
                    var target = starts[i];
                    if (connect[target.Left, target.Right, target.Input])
                    {
                        starts.RemoveAt(i);
                        i--;

                        if (!visitor[target.Left] && rights.ContainsKey(target.Left))
                            starts.AddRange(rights[target.Left]);

                        visitor[target.Left] = true;
                        @break = false;
                    }
                }

                if (@break) break;
            }

            return starts;
        }

        private static IEnumerable<FATransition<T>> FindSubsetEnds(IDictionary<ushort, FATransition<T>[]> rights, ushort index, int stateCount, TransitionMetadata<char, bool> connect)
        {
            if (rights.ContainsKey(index))
            {
                var visitor = new TransitionMetadata<char, bool>();
                var stack = new Stack<FATransition<T>>(rights[index]);
                while (stack.Count > 0)
                {
                    var tran = stack.Pop();
                    if (visitor[tran.Left, tran.Right, tran.Input])
                        continue;
 
                    visitor[tran.Left, tran.Right, tran.Input] = true;

                    //进入右侧
                    if (rights.ContainsKey(tran.Right))
                    {
                        foreach (var right in rights[tran.Right])
                        {
                            stack.Push(right);
                        }
                    }
                    // 进入子集
                    else if (connect[tran.Left, tran.Right, tran.Input])
                    {
                        if (rights.ContainsKey(tran.Symbol.Value))
                        {
                            foreach (var right in rights[tran.Symbol.Value])
                            {
                                stack.Push(right);
                            }
                        }
                    }
                    else
                    {
                        yield return tran;
                    }
                }
            }
        }

        private static IEnumerable<FATransition<T>> ForEach(IList<FATransition<T>> transitions, ushort index, int stateCount) 
        {
            return ForEach(transitions.GroupBy(x => x.Left).ToDictionary(x => x.Key, x => x.ToArray()), index, stateCount);
        }

        private static IEnumerable<FATransition<T>> ForEach(IDictionary<ushort, FATransition<T>[]> rights, ushort index, int stateCount) 
        {
            var visitor = new bool[stateCount];
            var queue = new Queue<ushort>();
            queue.Enqueue(index);
            while (queue.Count > 0)
            {
                var state = queue.Dequeue();
                if (visitor[state])
                    continue;

                visitor[state] = true;

                if (rights.ContainsKey(state))
                {
                    foreach (var next in rights[state]) 
                    {
                        queue.Enqueue(next.Right);
                        yield return next;
                    }
                }
            }
        }

        private static void ForEachSubset(IDictionary<ushort, FATransition<T>[]> rights, ushort index, int stateCount, Action<FATransition<T>> action)
        {
            var visitor = new bool[stateCount];
            var queue = new Queue<ushort>();
            queue.Enqueue(index);
            while (queue.Count > 0)
            {
                var state = queue.Dequeue();
                if (visitor[state])
                    continue;

                visitor[state] = true;

                if (rights.ContainsKey(state))
                {
                    foreach (var next in rights[state])
                    {
                        if(next.Symbol.Value != 0)
                            queue.Enqueue(next.Symbol.Value);

                        queue.Enqueue(next.Right);
                        action(next);
                    }
                }
            }
        }

        private static List<List<TElement>> GetCombinations<TElement>(int currentIndex, List<TElement[]> containers)
        {
            if (currentIndex == containers.Count)
            {
                // Skip the items for the last container
                var combinations2 = new List<List<TElement>>();
                combinations2.Add(new List<TElement>());
                return combinations2;
            }

            var combinations = new List<List<TElement>>();

            var containerItemList = containers[currentIndex];
            while (containerItemList == null)
            {
                containerItemList = containers[++currentIndex];
                if (currentIndex >= containers.Count)
                {
                    break;
                }
            }
            // Get combination from next index
            var suffixList = GetCombinations(currentIndex + 1, containers);
            int size = containerItemList == null ? 0 : containerItemList.Length;
            for (int ii = 0; ii < size; ii++)
            {
                TElement containerItem = containerItemList[ii];
                if (suffixList != null)
                {
                    foreach (var suffix in suffixList)
                    {
                        var nextCombination = new List<TElement>();
                        nextCombination.Add(containerItem);
                        nextCombination.AddRange(suffix);
                        combinations.Add(nextCombination);
                    }
                }
            }
            return combinations;
        }

        static TwoKeyDictionary<TKey, TKey2, TValue> ToTwoKeyDictionary<TSource, TKey, TKey2, TValue>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TKey2> keySelector2, Func<TSource, TValue> valueSelector)
        {
            var dict = new TwoKeyDictionary<TKey, TKey2, TValue>();
            foreach (var item in source)
                dict[keySelector(item), keySelector2(item)] = valueSelector(item);

            return dict;
        }

        public FAValue<T>[,] BuildTable() 
        {
            // 创建dfa
            var shifts = new ushort[StateDataCapacity, StateCapacity];
            var tokens = new T[StateDataCapacity, StateCapacity];
            var values = new FAValue<T>[StateDataCapacity, StateCapacity];

            foreach (var t in Transitions)
            {
                shifts[t.Input, t.Left] = t.Right;
                tokens[t.Input, t.Left] = t.Metadata;
            }

            var to = StateCapacity;
            Parallel.For(0, StateDataCapacity, index =>
            {
                for (var i = 0; i < to; i++)
                    values[index, i] = new FAValue<T>(
                        shifts[index, i],
                        tokens[index, i]);
            });

            return values;
        }

        #endregion

        #region 结构类型

        public interface IShiftRightMemoryModel 
        {
            IList<FATransition<T>> GetRights(ushort state);

            void Insert(int index, FATransition<T> transition);

            void Add(FATransition<T> transition);

            void Remove(FATransition<T> transition);
        }

        struct IntersectionPoint 
        {
            public IntersectionPoint(ushort state, FATransition<T> shortStart, FATransition<T> longStart)
            {
                State = state;
                ShortStart = shortStart;
                LongStart = longStart;
            }

            public ushort State { get; }

            public FATransition<T> ShortStart { get; }

            public FATransition<T> LongStart { get; }
        }

        class GraphPart 
        {
            public GraphPart(IReadOnlyList<FATransition<T>> transitions)
            {
                Transitions = transitions;
            }

            public IReadOnlyList<FATransition<T>> Transitions { get; }
        }

        class TransitionMetadata<TData, TMetadata> : IEnumerable<TMetadata>
        {
            Dictionary<ushort, Dictionary<ushort, Dictionary<TData, TMetadata>>> mDict;

            public TMetadata this[ushort left, ushort right, TData data]
            {
                get
                {
                    if (!mDict.ContainsKey(left))
                        return default;

                    if (!mDict[left].ContainsKey(right))
                        return default;

                    if (!mDict[left][right].ContainsKey(data))
                        return default;

                    return mDict[left][right][data];
                }
                set
                {
                    if (!mDict.ContainsKey(left))
                        mDict[left] = new Dictionary<ushort, Dictionary<TData, TMetadata>>();

                    if (!mDict[left].ContainsKey(right))
                        mDict[left][right] = new Dictionary<TData, TMetadata>();

                    mDict[left][right][data] = value;
                }
            }

            public bool ContainsKey(ushort left, ushort right, TData data) 
            {
                return mDict.ContainsKey(left) && mDict[left].ContainsKey(right) && mDict[left][right].ContainsKey(data);
            }

            public void Remove(ushort left, ushort right, TData data)
            {
                if (ContainsKey(left, right, data)) 
                {
                    mDict[left][right].Remove(data);
                }
            }

            public IEnumerator<TMetadata> GetEnumerator()
            {
                return mDict.SelectMany(x => x.Value.SelectMany(y => y.Value.Select(z => z.Value))).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Claer()
            {
                mDict.Clear();
            }

            public TransitionMetadata() 
            {
                mDict = new Dictionary<ushort, Dictionary<ushort, Dictionary<TData, TMetadata>>>();
            }
        }

        #endregion
    }
}
