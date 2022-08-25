using Tuyin.IR.Compiler.Parser.Expressions;
using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Productions
{
    class LiterallProuction : ProductionBase
    {
        internal override ProductionType ProductionType => ProductionType.Literall;

        internal override string DebugNamePrefix => mExpression.GetDescrption();

        private object mMetadata;
        private RegularExpression mExpression;

        public LiterallProuction(RegularExpression expression, object metadata) 
        {
            mMetadata = metadata;
            mExpression = expression;
        }

        protected override GraphEdgeStep<TMetadata> InternalCreate<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry)
        {
            TMetadata metadata = default;
            if(mMetadata != null)
                metadata = (TMetadata)mMetadata;

            return mExpression.CreateGraphState(figure, entry, metadata);
        }

        internal override IEnumerable<ProductionBase> GetChildrens()
        {
            return new ProductionBase[0];
        }

        public override string ToString()
        {
            return mExpression.GetDescrption();
        }
    }
}
