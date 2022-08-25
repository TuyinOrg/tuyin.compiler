using Tuyin.IR.Compiler.Parser.Generater;
using Tuyin.IR.Compiler.Parser.Productions;

namespace Tuyin.IR.Compiler.Parser
{
    unsafe abstract partial class ParserBase
    {
        private bool[] mSkipTokens;

        public abstract Lexicon Lexicon { get; }

        public bool IsSkip(Token token)
        {
            return mSkipTokens[token.Index];
        }

        public GraphBox<ushort> CreateGraph() 
        {
            var skippedTokens = new List<Token>();
            OnDefineLexer(Lexicon, skippedTokens);

            mSkipTokens = new bool[Lexicon.Tokens.Count];
            for (var i = 0; i < skippedTokens.Count; i++)
            {
                var token = skippedTokens[i];
                token.IsSkip = true;
                mSkipTokens[token.Index] = true;
            }

            // 创建产生式快照
            var grammer = new ConcatenationProduction(OnDefineGrammar(), new EosProduction(Lexicon.Eos));
            var graph = new ParserGraph(Lexicon);
            var figure = graph.Figure("Main");
            grammer.Create(figure, null, new Entry<ushort>(figure));
            return graph;
        }

        protected abstract ProductionBase OnDefineGrammar();

        protected virtual void OnDefineLexer(Lexicon lexicon, ICollection<Token> skippedTokens) 
        {
        }
    }
}
