using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Productions
{
    class EosProduction : ProductionBase
    {
        internal override ProductionType ProductionType => ProductionType.Eos;

        public Token Token { get; }

        internal override string DebugNamePrefix => "ε";

        public EosProduction(Token token)
        {
            Token = token;
        }

        protected override GraphEdgeStep<TMetadata> InternalCreate<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry)
        {
            // 申请新的figure
            var tokenMetadata = figure.GraphBox.GetTokenMetadata(Token);

            // 连接到figure
            figure.GraphBox.StartCollect();
            Token.Expression.CreateGraphState(figure, entry, default);
            var edges = figure.GraphBox.EndCollect();
            foreach (var edge in edges)
            {
                edge.Target = figure.GraphBox.Exit;
                edge.Metadata = tokenMetadata;
                edge.Descrption = "ε";
            }

            return new NextStep<TMetadata>(entry.End, figure.GraphBox.Exit);
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
