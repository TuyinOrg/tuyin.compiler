using Tuyin.IR.Compiler.Parser.Generater;
using Tuyin.IR.Compiler.Parser.Productions;
using System.Reflection;
using System.Runtime.CompilerServices;
using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Parser
{
    abstract class ProductionBase
    {

        internal static ProductionBase[] EMPTY_CHILDRENS = new ProductionBase[0];

        internal Feature Feature { get; set; }

        public virtual string ProductionName { get; set; }

        public virtual ProductionBase Parent { get; internal set; }

        internal abstract ProductionType ProductionType { get; }

        internal abstract string DebugNamePrefix { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProductionBase Priority(byte priority)
        {
            Feature = new Feature(Feature.Break, Feature.Stack, priority);
            return this;
        }

        internal GraphEdgeStep<TMetadata> Create<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry) where TMetadata : struct
        {
            //if (!(entry is Entry) && this is IUserProduction up && up.UserDefine)
            //{
            //    return InternalCreate(graph, last, new Entry(graph.State(true)));
            //}
            //else
            //{
                return InternalCreate(figure, last, entry);
            //}
        }

        protected abstract GraphEdgeStep<TMetadata> InternalCreate<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry) where TMetadata : struct;

        internal ProductionBase RepairRecursive(IProduction parser)
        {
            var result = InternalRepairRecursive(parser);
            result.Feature = Feature;
            return result;
        }

        protected virtual ProductionBase InternalRepairRecursive(IProduction parser)
        {
            return this;
        }

        internal virtual ProductionBase RepairInvalid()
        {
            return this;
        }

        internal abstract IEnumerable<ProductionBase> GetChildrens();

        public static ProductionBase operator |(ProductionBase p1, ProductionBase p2)
        {
            return new OrProduction(p1, p2);
        }

        public static ProductionBase operator &(ProductionBase p1, ProductionBase p2)
        {
            return new ConcatenationProduction(p1, p2);
        }
    }

    static class ProductionHelper 
    {
        internal static IEnumerable<GraphEdge<TMetadata>> FindCreateHead<TMetadata>(GraphFigure<TMetadata> figure, List<GraphEdge<TMetadata>> paths, GraphState<TMetadata>[] targets) 
            where TMetadata : struct
        {
            var visitors = new bool[figure.GraphBox.States.Count];
            var results = new HashSet<GraphEdge<TMetadata>>();
            var queue = new Queue<GraphState<TMetadata>>(targets);
            while (queue.Count > 0)
            {
                var target = queue.Dequeue();

                visitors[target.Index] = true;

                // 查找下一步
                var items = paths.Where(x => figure.GetRights(target).Contains(x)).ToArray();

                // 如果当前步存在则返回
                foreach (var item in items)
                {
                    if (paths.Contains(item))
                    {
                        results.Add(item);
                    }
                    else
                    {
                        if (!visitors[item.Target.Index])
                        {
                            queue.Enqueue(item.Target);
                        }
                    }
                }
            }

            return results;
        }

        public static bool IsUserProduction(this IStateProduction production)
        {
            
            /*
            var operations = new MSILReader(production.Delegate.Method).ToArray();

            ConstructorInfo ctor = null;
            for (var i = operations.Length - 1; i >= 0; i--)
            {
                var opCode = operations[i];
                if (opCode.OpCode == OpCodes.Newobj)
                {
                    ctor = opCode.Instance as ConstructorInfo;
                    break;
                }
            }

            if (ctor != null)
                return !IsCompilerGenerated(ctor.ReflectedType);
            */

            return false;
        }

        public static ProductionBase GetHeadProduction(this ProductionBase p) 
        {
            var curr = p;
            while (curr.Parent != null)
                curr = curr.Parent;

            return curr;
        }

        private static IEnumerable<System.Type> ExpandType(System.Type type)
        {
            if (type.IsVisible)
            {
                yield return type;
            }
            else
            {
                var fields = type.GetProperties();
                for (var i = 0; i < fields.Length; i++)
                {
                    var field = fields[i];
                    var fieldType = field.PropertyType;

                    if (!fieldType.IsVisible)
                    {
                        foreach (var subType in ExpandType(fieldType))
                        {
                            yield return subType;
                        }
                    }
                    else
                    {
                        yield return fieldType;
                    }
                }
            }
        }

        private static bool IsCompilerGenerated(System.Type type)
        {
            return type.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
        }
    }
}
