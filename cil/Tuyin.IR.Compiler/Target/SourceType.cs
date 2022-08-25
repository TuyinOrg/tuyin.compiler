using Tuyin.IR.Compiler.Target.Visitors;
using Type = Tuyin.IR.Reflection.Types.Type;

namespace Tuyin.IR.Compiler.Target
{
    internal class SourceType : Type, IAstNode
    {
        private Type mType;

        public SourceType(SourceSpan sourceSpan, Type type)
        {
            mType = type;
            StartIndex = sourceSpan.StartIndex;
            EndIndex = sourceSpan.EndIndex;
        }

        public AstNodeType AstType => AstNodeType.Type;

        public int StartIndex { get; }

        public int EndIndex { get; }

        public override string Name => mType.Name;

        public override uint BitsSize => mType.BitsSize;

        public override bool IsArray => mType.IsArray;

        public override bool IsPointer => mType.IsPointer;

        public override bool IsStruct => mType.IsStruct;

        public override bool IsAuto => mType.IsAuto;

        public override bool IsMutable => mType.IsMutable;

        public T Visit<T>(AstVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
