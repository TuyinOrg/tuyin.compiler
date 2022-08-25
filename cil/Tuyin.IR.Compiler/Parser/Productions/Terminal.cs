using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Productions
{
    class Terminal : ProductionBase
    {
        internal override ProductionType ProductionType => ProductionType.Terminal;

        public Token Token { get; }

        internal override string DebugNamePrefix => Token.Description;

        public Terminal(Token token)
        {
            Token = token;
        }

        protected override GraphEdgeStep<TMetadata> InternalCreate<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry)
        {
            // 申请新的figure
            var tokenFigure = figure.GraphBox.GetTokenFigure(last, Token);
            var tokenMetadata = figure.GraphBox.GetTokenMetadata(Token);

            // 连接到figure
            var end = figure.State();
            foreach (var start in entry.End)
            {
                var edge = figure.Edge(start, end, new GraphEdgeValue(tokenFigure.Index), tokenMetadata);
                edge.Subset = tokenFigure;
                edge.Descrption = tokenFigure.DisplayName;
            }

            return new NextStep<TMetadata>(entry.End, end);
        }

        internal override IEnumerable<ProductionBase> GetChildrens()
        {
            return new ProductionBase[0];
        }

        public override string ToString()
        {
            return Token.Description ?? Token.Expression.GetDescrption();
        }
    }
}
