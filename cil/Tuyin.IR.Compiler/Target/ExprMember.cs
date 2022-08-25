using LLParserLexerLib;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprMember : ExprRoot
    {
        private ExprRoot tmp_23_i;
        private TokenAST nt2_s;

        public ExprMember(SourceSpan sourceSpan, ExprRoot tmp_23_i, TokenAST nt2_s) : base(sourceSpan)
        {
            this.tmp_23_i = tmp_23_i;
            this.nt2_s = nt2_s;
        }

        public override AstNodeType AstType => AstNodeType.ExprMember;

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Member(tmp_23_i.ToIR(cache), new Reflection.Instructions.String(nt2_s.strRead, new SourceSpan(nt2_s)));
        }
    }
}