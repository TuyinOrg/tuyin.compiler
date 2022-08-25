using LLParserLexerLib;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprFloat : ExprNum
    {
        private TokenAST nt1_s;

        public override AstNodeType AstType => AstNodeType.ExprFloat;

        internal ExprFloat(SourceSpan sourceSpan, TokenAST nt1_s)
            : base(sourceSpan)
        {
            this.nt1_s = nt1_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Float(BigFloat.Parse(nt1_s.strRead));
        }
    }
}