using LLParserLexerLib;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprIdentifier : ExprRoot
    {
        private TokenAST nt1_s;

        public override AstNodeType AstType => AstNodeType.ExprIdentifier;

        internal ExprIdentifier(SourceSpan sourceSpan, TokenAST nt1_s)
            : base(sourceSpan)
        {
            this.nt1_s = nt1_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Identifier(nt1_s.strRead);
        }
    }
}