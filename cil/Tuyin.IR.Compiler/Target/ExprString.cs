using LLParserLexerLib;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprString : ExprRoot
    {
        private TokenAST nt1_s;

        public override AstNodeType AstType => AstNodeType.ExprString;

        public ExprString(SourceSpan sourceSpan, TokenAST nt1_s)
            : base(sourceSpan)
        {
            this.nt1_s = nt1_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Reflection.Instructions.String(nt1_s.strRead);
        }
    }
}