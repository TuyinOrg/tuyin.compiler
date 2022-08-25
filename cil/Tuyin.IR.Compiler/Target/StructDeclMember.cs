using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    internal class StructDeclMember : IAST
    {
        private SourceSpan sourceSpan;
        private SourceModifiter nt1_s;
        private TypeNamed nt2_s;

        public StructDeclMember(SourceSpan sourceSpan, SourceModifiter nt1_s, TypeNamed nt2_s)
        {
            this.sourceSpan = sourceSpan;
            this.nt1_s = nt1_s;
            this.nt2_s = nt2_s;
        }
    }
}
