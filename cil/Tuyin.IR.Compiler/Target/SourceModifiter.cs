using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    struct SourceModifiter : IAST, ISourceSpan
    {
        public SourceModifiter(TokenAST tokenAST, Modifiter modifiter)
        {
            TokenAST = tokenAST;
            Modifiter = modifiter;
        }

        public TokenAST TokenAST { get; }

        public Modifiter Modifiter { get; }

        public int StartIndex => TokenAST?.StartIndex ?? -1;

        public int EndIndex => TokenAST?.EndIndex ?? -1;
    }
}
