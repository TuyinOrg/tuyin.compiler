using LLParserLexerLib;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprInteger : ExprNum
    {
        private TokenAST nt1_s;

        public override AstNodeType AstType => AstNodeType.ExprInteger;

        internal ExprInteger(SourceSpan sourceSpan, TokenAST nt1_s)
            : base(sourceSpan)
        {
            this.nt1_s = nt1_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Integer(BigInteger.Parse(nt1_s.strRead));
        }
    }
}