using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser
{
    class ParserGraph : GraphBox<ushort>
    {
        public ParserGraph(Lexicon lexicon) 
            : base(lexicon)
        {
        }

        public override int GetMetadataCompreValue(ushort metadata)
        {
            return Lexicon.Tokens[metadata].Expression.RepeatLevel();
        }

        public override ushort GetTokenMetadata(Token token)
        {
            return token.Index;
        }
    }
}
