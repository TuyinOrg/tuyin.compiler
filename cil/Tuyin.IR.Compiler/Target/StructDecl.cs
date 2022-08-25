namespace Tuyin.IR.Compiler.Target
{
    internal class StructDecl : Declare
    {
        public StructDecl(SourceSpan sourceSpan, TypeNamed nt2_s, StructDeclMemberList nt4_s)
        {
            Interface = nt2_s;
            Members = nt4_s;
            StartIndex = sourceSpan.StartIndex;
            EndIndex = sourceSpan.EndIndex;
        }

        public TypeNamed Interface { get; }

        public StructDeclMemberList Members { get; }

        public override int StartIndex { get; }

        public override int EndIndex { get; }

        public override DeclareType DeclareType => DeclareType.Struct;
    }
}
