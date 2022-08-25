using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    internal class Import : StmtRoot
    {
        private Path nt2_s;

        public Import(SourceSpan sourceSpan, Path nt2_s) 
            : base(sourceSpan)
        {
            this.nt2_s = nt2_s;
        }

        public override AstNodeType AstType => AstNodeType.Import;

        public Path Path => nt2_s;
    }
}