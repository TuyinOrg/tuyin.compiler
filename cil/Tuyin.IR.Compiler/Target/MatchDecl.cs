using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    class MatchDecl : Declare
    {
        public TokenAST Identifier { get; }

        public SourceProduction Production { get; }

        public override int StartIndex { get; }

        public override int EndIndex { get; }

        public override DeclareType DeclareType => DeclareType.Match;

        public MatchDecl(SourceSpan sourceSpan, TokenAST id, SourceProduction production)
        {
            Identifier = id;
            Production = production;
            StartIndex = sourceSpan.StartIndex;
            EndIndex = sourceSpan.EndIndex;
        }
    }
}