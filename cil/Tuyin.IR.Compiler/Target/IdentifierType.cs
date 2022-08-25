using LLParserLexerLib;
using Type = Tuyin.IR.Reflection.Types.Type;

namespace Tuyin.IR.Compiler.Target
{
    internal class IdentifierType : Type
    {
        private TokenAST mTypeName;
        private Type mType;

        public IdentifierType(TokenAST typeName)
        {
            this.mTypeName = typeName;
        }

        public override string Name => mType?.Name ?? $"[ReferenceType]{mTypeName.strRead}";

        public override uint BitsSize => mType?.BitsSize ?? 0;

        public override bool IsArray => mType?.IsArray ?? false;

        public override bool IsAuto => mType?.IsAuto ?? false;

        public override bool IsMutable => mType?.IsMutable ?? false;

        public override bool IsPointer => mType?.IsPointer ?? false;

        public override bool IsStruct => mType?.IsStruct ?? false;
    }
}