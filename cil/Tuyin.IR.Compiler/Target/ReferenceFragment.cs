using LLParserLexerLib;
using Tuyin.IR.Compiler.Parser;
using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Target
{
    internal class ReferenceMatch : ProductionBase
    {
        private TokenAST id;
        private ProductionBase production;

        internal override ProductionType ProductionType => production.ProductionType;

        internal override string DebugNamePrefix => production.DebugNamePrefix;

        public ProductionBase Production => production;

        public ReferenceMatch(TokenAST id)
        {
            this.id = id;
        }

        public void FindProduction(Target parserFile)
        {
            this.production = parserFile.FindProduction(id);
        }

        protected override GraphEdgeStep<TMetadata> InternalCreate<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry)
        {
            return production.Create(figure, last, entry);
        }

        internal override IEnumerable<ProductionBase> GetChildrens()
        {
            return production.GetChildrens();
        }
    }
}