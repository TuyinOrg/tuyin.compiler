using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Tuyin.IR.Analysis.Data;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Types;
using System.Linq;
using Type = Tuyin.IR.Reflection.Types.Type;

namespace Tuyin.IR.Analysis.Passes
{
    class TypeAnalysis : IAnalysis<int, Type>
    {
        internal TypeAnalysis(DAG dag, Metadatas metadatas)
        {
            DAG = dag;
            Metadatas = metadatas;
        }

        public DAG DAG { get; }

        public Metadatas Metadatas { get; }

        public Type Run(int input)
        {
            var node = Search(DAG.Edges, input);
            if (node.SourceSpan.EndIndex == node.SourceSpan.StartIndex)
                return null;

            return DeducedType(node);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Type DeducedType(AnalysisEdge edge)
        {
            // 向下分析得到类型
            var stack = new Stack<AnalysisEdge>();
            stack.Push(edge);
            while (stack.Count > 0)
            {
                edge = stack.Pop();
                var type = TerminationType(edge);
                if (type != null)
                    return type;

                foreach (var next in edge.Target.Rights)
                    stack.Push(next);
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Type TerminationType(AnalysisEdge edge)
        {
            var op = edge.Target is DAGMicrocodeNode atom ? atom.Microcode.OpCode : OpCode.Load;
            switch (op)
            {
                case OpCode.Add:
                case OpCode.And:
                case OpCode.Or:
                case OpCode.Xor:
                case OpCode.Ceq:
                case OpCode.Clt:
                case OpCode.Cgt:
                case OpCode.Div:
                case OpCode.Mul:
                case OpCode.Rem:
                case OpCode.Shl:
                case OpCode.Shr:
                    return sBinaryResult[TerminationType(edge.Target.Rights[0]), TerminationType(edge.Target.Rights[1])];
                case OpCode.Neg:
                    return sUnaryOperatorResult[TerminationType(edge.Target.Rights[0]), op];
                case OpCode.Ldstr:
                    return PrimitiveType.String;
                case OpCode.Ldc:
                    return PrimitiveType.Int32;
                case OpCode.Ldr:
                    return PrimitiveType.Float32;
                case OpCode.Load:
                    if (edge.Subset is DAGLoadNode subset && subset.Fields.Count() > 0)
                        return new StructType(subset.Fields.Select(x => new StructType.StructMember(x.Name.Identifier, DeducedType(x.Rights[0]))));

                    return TerminationType(edge.Target.Rights[0]);
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private AnalysisEdge Search(IReadOnlyList<AnalysisEdge> nodes, int target)
        {
            int left = 0;
            int right = nodes.Count - 1;
            while (left <= right)
            {
                int mid = (right - left) / 2 + left;
                var edge = nodes[mid];
                if (edge.SourceSpan.StartIndex > target)
                    right = mid - 1;
                else if (edge.SourceSpan.EndIndex < target)
                    left = mid + 1;
                else
                    return edge;
            }
            return default;
        }


        static readonly TwoKeyDictionary<Type, OpCode, bool> sBinaryOperator = new TwoKeyDictionary<Type, OpCode, bool>();
        static readonly TwoKeyDictionary<Type, Type, Type> sBinaryResult = new TwoKeyDictionary<Type, Type, Type>();
        static readonly TwoKeyDictionary<Type, OpCode, Type> sUnaryOperatorResult = new TwoKeyDictionary<Type, OpCode, Type>();

        static TypeAnalysis()
        {
            sBinaryResult[PrimitiveType.Boolean, PrimitiveType.Boolean] = PrimitiveType.Boolean;

            sBinaryResult[PrimitiveType.Char, PrimitiveType.Char] = PrimitiveType.Int32;
            sBinaryResult[PrimitiveType.Char, PrimitiveType.String] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.Char, PrimitiveType.Int8] = PrimitiveType.Int32;
            sBinaryResult[PrimitiveType.Char, PrimitiveType.Int16] = PrimitiveType.Int32;
            sBinaryResult[PrimitiveType.Char, PrimitiveType.Int32] = PrimitiveType.Int32;
            sBinaryResult[PrimitiveType.Char, PrimitiveType.Int64] = PrimitiveType.Int64;
            sBinaryResult[PrimitiveType.Char, PrimitiveType.Uint8] = PrimitiveType.Uint32;
            sBinaryResult[PrimitiveType.Char, PrimitiveType.Uint16] = PrimitiveType.Uint32;
            sBinaryResult[PrimitiveType.Char, PrimitiveType.Uint32] = PrimitiveType.Uint32;
            sBinaryResult[PrimitiveType.Char, PrimitiveType.Uint64] = PrimitiveType.Uint64;
            sBinaryResult[PrimitiveType.Char, PrimitiveType.Float32] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Char, PrimitiveType.Float64] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Char, PrimitiveType.Anonymous] = PrimitiveType.Int32;

            sBinaryResult[PrimitiveType.Int32, PrimitiveType.Char] = PrimitiveType.Int32;
            sBinaryResult[PrimitiveType.Int32, PrimitiveType.String] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.Int32, PrimitiveType.Int8] = PrimitiveType.Int32;
            sBinaryResult[PrimitiveType.Int32, PrimitiveType.Int16] = PrimitiveType.Int32;
            sBinaryResult[PrimitiveType.Int32, PrimitiveType.Int32] = PrimitiveType.Int32;
            sBinaryResult[PrimitiveType.Int32, PrimitiveType.Int64] = PrimitiveType.Int64;
            sBinaryResult[PrimitiveType.Int32, PrimitiveType.Uint8] = PrimitiveType.Int32;
            sBinaryResult[PrimitiveType.Int32, PrimitiveType.Uint16] = PrimitiveType.Int32;
            sBinaryResult[PrimitiveType.Int32, PrimitiveType.Uint32] = PrimitiveType.Uint32;
            sBinaryResult[PrimitiveType.Int32, PrimitiveType.Uint64] = PrimitiveType.Uint64;
            sBinaryResult[PrimitiveType.Int32, PrimitiveType.Float32] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Int32, PrimitiveType.Float64] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Int32, PrimitiveType.Anonymous] = PrimitiveType.Int32;

            sBinaryResult[PrimitiveType.Int64, PrimitiveType.Char] = PrimitiveType.Int64;
            sBinaryResult[PrimitiveType.Int64, PrimitiveType.String] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.Int64, PrimitiveType.Int8] = PrimitiveType.Int64;
            sBinaryResult[PrimitiveType.Int64, PrimitiveType.Int16] = PrimitiveType.Int64;
            sBinaryResult[PrimitiveType.Int64, PrimitiveType.Int32] = PrimitiveType.Int64;
            sBinaryResult[PrimitiveType.Int64, PrimitiveType.Int64] = PrimitiveType.Int64;
            sBinaryResult[PrimitiveType.Int64, PrimitiveType.Uint8] = PrimitiveType.Int64;
            sBinaryResult[PrimitiveType.Int64, PrimitiveType.Uint16] = PrimitiveType.Int64;
            sBinaryResult[PrimitiveType.Int64, PrimitiveType.Uint32] = PrimitiveType.Int64;
            sBinaryResult[PrimitiveType.Int64, PrimitiveType.Uint64] = PrimitiveType.Uint64;
            sBinaryResult[PrimitiveType.Int64, PrimitiveType.Float32] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Int64, PrimitiveType.Float64] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Int64, PrimitiveType.Anonymous] = PrimitiveType.Int64;

            sBinaryResult[PrimitiveType.Float32, PrimitiveType.Char] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Float32, PrimitiveType.String] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.Float32, PrimitiveType.Int8] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Float32, PrimitiveType.Int16] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Float32, PrimitiveType.Int32] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Float32, PrimitiveType.Int64] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Float32, PrimitiveType.Uint8] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Float32, PrimitiveType.Uint16] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Float32, PrimitiveType.Uint32] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Float32, PrimitiveType.Uint64] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Float32, PrimitiveType.Float32] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Float32, PrimitiveType.Float64] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Float32, PrimitiveType.Anonymous] = PrimitiveType.Float32;

            sBinaryResult[PrimitiveType.Float64, PrimitiveType.Char] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Float64, PrimitiveType.String] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.Float64, PrimitiveType.Int8] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Float64, PrimitiveType.Int16] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Float64, PrimitiveType.Int32] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Float64, PrimitiveType.Int64] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Float64, PrimitiveType.Uint8] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Float64, PrimitiveType.Uint16] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Float64, PrimitiveType.Uint32] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Float64, PrimitiveType.Uint64] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Float64, PrimitiveType.Float32] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Float64, PrimitiveType.Float64] = PrimitiveType.Float64;
            sBinaryResult[PrimitiveType.Float64, PrimitiveType.Anonymous] = PrimitiveType.Float64;

            sBinaryResult[PrimitiveType.String, PrimitiveType.Char] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.String, PrimitiveType.String] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.String, PrimitiveType.Int8] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.String, PrimitiveType.Int16] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.String, PrimitiveType.Int32] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.String, PrimitiveType.Int64] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.String, PrimitiveType.Uint8] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.String, PrimitiveType.Uint16] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.String, PrimitiveType.Uint32] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.String, PrimitiveType.Uint64] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.String, PrimitiveType.Float32] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.String, PrimitiveType.Float64] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.String, PrimitiveType.Anonymous] = PrimitiveType.String;

            sBinaryResult[PrimitiveType.Anonymous, PrimitiveType.Char] = PrimitiveType.Char;
            sBinaryResult[PrimitiveType.Anonymous, PrimitiveType.String] = PrimitiveType.String;
            sBinaryResult[PrimitiveType.Anonymous, PrimitiveType.Int8] = PrimitiveType.Int8;
            sBinaryResult[PrimitiveType.Anonymous, PrimitiveType.Int16] = PrimitiveType.Int16;
            sBinaryResult[PrimitiveType.Anonymous, PrimitiveType.Int32] = PrimitiveType.Int32;
            sBinaryResult[PrimitiveType.Anonymous, PrimitiveType.Int64] = PrimitiveType.Int64;
            sBinaryResult[PrimitiveType.Anonymous, PrimitiveType.Uint8] = PrimitiveType.Uint8;
            sBinaryResult[PrimitiveType.Anonymous, PrimitiveType.Uint16] = PrimitiveType.Uint16;
            sBinaryResult[PrimitiveType.Anonymous, PrimitiveType.Uint32] = PrimitiveType.Uint32;
            sBinaryResult[PrimitiveType.Anonymous, PrimitiveType.Uint64] = PrimitiveType.Uint64;
            sBinaryResult[PrimitiveType.Anonymous, PrimitiveType.Float32] = PrimitiveType.Float32;
            sBinaryResult[PrimitiveType.Anonymous, PrimitiveType.Float64] = PrimitiveType.Float64;

            sBinaryOperator[PrimitiveType.Char, OpCode.Add] = true;
            sBinaryOperator[PrimitiveType.Char, OpCode.And] = true;
            sBinaryOperator[PrimitiveType.Char, OpCode.Or] = true;
            sBinaryOperator[PrimitiveType.Char, OpCode.Xor] = true;
            sBinaryOperator[PrimitiveType.Char, OpCode.Div] = true;
            sBinaryOperator[PrimitiveType.Char, OpCode.Ceq] = true; 
            sBinaryOperator[PrimitiveType.Char, OpCode.Cgt] = true;
            sBinaryOperator[PrimitiveType.Char, OpCode.Shl] = true;
            sBinaryOperator[PrimitiveType.Char, OpCode.Clt] = true;
            sBinaryOperator[PrimitiveType.Char, OpCode.Mul] = true;
            sBinaryOperator[PrimitiveType.Char, OpCode.Rem] = true;
            sBinaryOperator[PrimitiveType.Char, OpCode.Shr] = true;
            sBinaryOperator[PrimitiveType.Char, OpCode.Sub] = true;

            sBinaryOperator[PrimitiveType.Int8, OpCode.Add] = true;
            sBinaryOperator[PrimitiveType.Int8, OpCode.And] = true;
            sBinaryOperator[PrimitiveType.Int8, OpCode.Or] = true;
            sBinaryOperator[PrimitiveType.Int8, OpCode.Xor] = true;
            sBinaryOperator[PrimitiveType.Int8, OpCode.Div] = true;
            sBinaryOperator[PrimitiveType.Int8, OpCode.Ceq] = true;
            sBinaryOperator[PrimitiveType.Int8, OpCode.Cgt] = true;
            sBinaryOperator[PrimitiveType.Int8, OpCode.Shl] = true;
            sBinaryOperator[PrimitiveType.Int8, OpCode.Clt] = true;
            sBinaryOperator[PrimitiveType.Int8, OpCode.Mul] = true;
            sBinaryOperator[PrimitiveType.Int8, OpCode.Rem] = true;
            sBinaryOperator[PrimitiveType.Int8, OpCode.Shr] = true;
            sBinaryOperator[PrimitiveType.Int8, OpCode.Sub] = true;

            sBinaryOperator[PrimitiveType.Int16, OpCode.Add] = true;
            sBinaryOperator[PrimitiveType.Int16, OpCode.And] = true;
            sBinaryOperator[PrimitiveType.Int16, OpCode.Or] = true;
            sBinaryOperator[PrimitiveType.Int16, OpCode.Xor] = true;
            sBinaryOperator[PrimitiveType.Int16, OpCode.Div] = true;
            sBinaryOperator[PrimitiveType.Int16, OpCode.Ceq] = true;
            sBinaryOperator[PrimitiveType.Int16, OpCode.Cgt] = true;
            sBinaryOperator[PrimitiveType.Int16, OpCode.Shl] = true;
            sBinaryOperator[PrimitiveType.Int16, OpCode.Clt] = true;
            sBinaryOperator[PrimitiveType.Int16, OpCode.Mul] = true;
            sBinaryOperator[PrimitiveType.Int16, OpCode.Rem] = true;
            sBinaryOperator[PrimitiveType.Int16, OpCode.Shr] = true;
            sBinaryOperator[PrimitiveType.Int16, OpCode.Sub] = true;

            sBinaryOperator[PrimitiveType.Int32, OpCode.Add] = true;
            sBinaryOperator[PrimitiveType.Int32, OpCode.And] = true;
            sBinaryOperator[PrimitiveType.Int32, OpCode.Or] = true;
            sBinaryOperator[PrimitiveType.Int32, OpCode.Xor] = true;
            sBinaryOperator[PrimitiveType.Int32, OpCode.Div] = true;
            sBinaryOperator[PrimitiveType.Int32, OpCode.Ceq] = true;
            sBinaryOperator[PrimitiveType.Int32, OpCode.Cgt] = true;
            sBinaryOperator[PrimitiveType.Int32, OpCode.Shl] = true;
            sBinaryOperator[PrimitiveType.Int32, OpCode.Clt] = true;
            sBinaryOperator[PrimitiveType.Int32, OpCode.Mul] = true;
            sBinaryOperator[PrimitiveType.Int32, OpCode.Rem] = true;
            sBinaryOperator[PrimitiveType.Int32, OpCode.Shr] = true;
            sBinaryOperator[PrimitiveType.Int32, OpCode.Sub] = true;

            sBinaryOperator[PrimitiveType.Int64, OpCode.Add] = true;
            sBinaryOperator[PrimitiveType.Int64, OpCode.And] = true;
            sBinaryOperator[PrimitiveType.Int64, OpCode.Or] = true;
            sBinaryOperator[PrimitiveType.Int64, OpCode.Xor] = true;
            sBinaryOperator[PrimitiveType.Int64, OpCode.Div] = true;
            sBinaryOperator[PrimitiveType.Int64, OpCode.Ceq] = true;  
            sBinaryOperator[PrimitiveType.Int64, OpCode.Cgt] = true;
            sBinaryOperator[PrimitiveType.Int64, OpCode.Shl] = true;    
            sBinaryOperator[PrimitiveType.Int64, OpCode.Clt] = true;
            sBinaryOperator[PrimitiveType.Int64, OpCode.Mul] = true;     
            sBinaryOperator[PrimitiveType.Int64, OpCode.Rem] = true;
            sBinaryOperator[PrimitiveType.Int64, OpCode.Shr] = true;
            sBinaryOperator[PrimitiveType.Int64, OpCode.Sub] = true;

            sBinaryOperator[PrimitiveType.Uint8, OpCode.Add] = true;
            sBinaryOperator[PrimitiveType.Uint8, OpCode.And] = true;
            sBinaryOperator[PrimitiveType.Uint8, OpCode.Or] = true;
            sBinaryOperator[PrimitiveType.Uint8, OpCode.Xor] = true;
            sBinaryOperator[PrimitiveType.Uint8, OpCode.Div] = true;
            sBinaryOperator[PrimitiveType.Uint8, OpCode.Ceq] = true;
            sBinaryOperator[PrimitiveType.Uint8, OpCode.Cgt] = true;
            sBinaryOperator[PrimitiveType.Uint8, OpCode.Shl] = true;
            sBinaryOperator[PrimitiveType.Uint8, OpCode.Clt] = true;
            sBinaryOperator[PrimitiveType.Uint8, OpCode.Mul] = true;
            sBinaryOperator[PrimitiveType.Uint8, OpCode.Rem] = true;
            sBinaryOperator[PrimitiveType.Uint8, OpCode.Shr] = true;
            sBinaryOperator[PrimitiveType.Uint8, OpCode.Sub] = true;

            sBinaryOperator[PrimitiveType.Uint16, OpCode.Add] = true;
            sBinaryOperator[PrimitiveType.Uint16, OpCode.And] = true;
            sBinaryOperator[PrimitiveType.Uint16, OpCode.Or] = true;
            sBinaryOperator[PrimitiveType.Uint16, OpCode.Xor] = true;
            sBinaryOperator[PrimitiveType.Uint16, OpCode.Div] = true;
            sBinaryOperator[PrimitiveType.Uint16, OpCode.Ceq] = true;
            sBinaryOperator[PrimitiveType.Uint16, OpCode.Cgt] = true;
            sBinaryOperator[PrimitiveType.Uint16, OpCode.Shl] = true;
            sBinaryOperator[PrimitiveType.Uint16, OpCode.Clt] = true;
            sBinaryOperator[PrimitiveType.Uint16, OpCode.Mul] = true;
            sBinaryOperator[PrimitiveType.Uint16, OpCode.Rem] = true;
            sBinaryOperator[PrimitiveType.Uint16, OpCode.Shr] = true;
            sBinaryOperator[PrimitiveType.Uint16, OpCode.Sub] = true;

            sBinaryOperator[PrimitiveType.Uint32, OpCode.Add] = true;
            sBinaryOperator[PrimitiveType.Uint32, OpCode.And] = true;
            sBinaryOperator[PrimitiveType.Uint32, OpCode.Or] = true;
            sBinaryOperator[PrimitiveType.Uint32, OpCode.Xor] = true;
            sBinaryOperator[PrimitiveType.Uint32, OpCode.Div] = true;
            sBinaryOperator[PrimitiveType.Uint32, OpCode.Ceq] = true;
            sBinaryOperator[PrimitiveType.Uint32, OpCode.Cgt] = true;
            sBinaryOperator[PrimitiveType.Uint32, OpCode.Shl] = true;
            sBinaryOperator[PrimitiveType.Uint32, OpCode.Clt] = true;
            sBinaryOperator[PrimitiveType.Uint32, OpCode.Mul] = true;
            sBinaryOperator[PrimitiveType.Uint32, OpCode.Rem] = true;
            sBinaryOperator[PrimitiveType.Uint32, OpCode.Shr] = true;
            sBinaryOperator[PrimitiveType.Uint32, OpCode.Sub] = true;

            sBinaryOperator[PrimitiveType.Uint64, OpCode.Add] = true;
            sBinaryOperator[PrimitiveType.Uint64, OpCode.And] = true;
            sBinaryOperator[PrimitiveType.Uint64, OpCode.Or] = true;
            sBinaryOperator[PrimitiveType.Uint64, OpCode.Xor] = true;
            sBinaryOperator[PrimitiveType.Uint64, OpCode.Div] = true;
            sBinaryOperator[PrimitiveType.Uint64, OpCode.Ceq] = true;
            sBinaryOperator[PrimitiveType.Uint64, OpCode.Cgt] = true;
            sBinaryOperator[PrimitiveType.Uint64, OpCode.Shl] = true;
            sBinaryOperator[PrimitiveType.Uint64, OpCode.Clt] = true;
            sBinaryOperator[PrimitiveType.Uint64, OpCode.Mul] = true;
            sBinaryOperator[PrimitiveType.Uint64, OpCode.Rem] = true;
            sBinaryOperator[PrimitiveType.Uint64, OpCode.Shr] = true;
            sBinaryOperator[PrimitiveType.Uint64, OpCode.Sub] = true;

            sBinaryOperator[PrimitiveType.Float32, OpCode.Add] = true;
            sBinaryOperator[PrimitiveType.Float32, OpCode.Div] = true;
            sBinaryOperator[PrimitiveType.Float32, OpCode.Ceq] = true;
            sBinaryOperator[PrimitiveType.Float32, OpCode.Cgt] = true;
            sBinaryOperator[PrimitiveType.Float32, OpCode.Clt] = true;
            sBinaryOperator[PrimitiveType.Float32, OpCode.Mul] = true;
            sBinaryOperator[PrimitiveType.Float32, OpCode.Sub] = true;

            sBinaryOperator[PrimitiveType.Float64, OpCode.Add] = true;
            sBinaryOperator[PrimitiveType.Float64, OpCode.Div] = true;
            sBinaryOperator[PrimitiveType.Float64, OpCode.Ceq] = true;
            sBinaryOperator[PrimitiveType.Float64, OpCode.Cgt] = true;
            sBinaryOperator[PrimitiveType.Float64, OpCode.Clt] = true;
            sBinaryOperator[PrimitiveType.Float64, OpCode.Mul] = true;
            sBinaryOperator[PrimitiveType.Float64, OpCode.Sub] = true;

            sUnaryOperatorResult[PrimitiveType.Boolean, OpCode.Neg] = PrimitiveType.Boolean;
            sUnaryOperatorResult[PrimitiveType.Int8, OpCode.Neg] = PrimitiveType.Int8;
            sUnaryOperatorResult[PrimitiveType.Int16, OpCode.Neg] = PrimitiveType.Int16;
            sUnaryOperatorResult[PrimitiveType.Int32, OpCode.Neg] = PrimitiveType.Int32;
            sUnaryOperatorResult[PrimitiveType.Int64, OpCode.Neg] = PrimitiveType.Int64;
            sUnaryOperatorResult[PrimitiveType.Uint8, OpCode.Neg] = PrimitiveType.Int8;
            sUnaryOperatorResult[PrimitiveType.Uint16, OpCode.Neg] = PrimitiveType.Int16;
            sUnaryOperatorResult[PrimitiveType.Uint32, OpCode.Neg] = PrimitiveType.Int32;
            sUnaryOperatorResult[PrimitiveType.Uint64, OpCode.Neg] = PrimitiveType.Int64;
            sUnaryOperatorResult[PrimitiveType.Float32, OpCode.Neg] = PrimitiveType.Float32;
            sUnaryOperatorResult[PrimitiveType.Float64, OpCode.Neg] = PrimitiveType.Float64;
        }
    }
}
