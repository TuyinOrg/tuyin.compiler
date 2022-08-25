using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    struct TypeNamed : IEquatable<TypeNamed>, ISourceSpan
    {
        public TypeNamed(TokenAST name, SourceType type)
        {
            Name = name;
            Type = type;
        }

        public TokenAST Name { get; }

        public SourceType Type { get; }

        public int StartIndex => Name.StartIndex;

        public int EndIndex => Type.EndIndex;

        public override bool Equals(object obj)
        {
            return obj is TypeNamed typed && Equals(typed);
        }

        public bool Equals(TypeNamed other)
        {
            return Type.Equals(other.Type) && Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Name);
        }
    }
}
