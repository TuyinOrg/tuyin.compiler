using LLParserLexerLib;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class StmtVarDecl : StmtRoot
    {
        private TokenAST nt2_s;
        private ExprRoot nt4_s;

        public override AstNodeType AstType => AstNodeType.StmtVarDecl;

        internal StmtVarDecl(SourceSpan sourceSpan, TokenAST nt2_s, ExprRoot nt4_s)
            : base(sourceSpan)
        {
            this.nt2_s = nt2_s;
            this.nt4_s = nt4_s;
        }

        internal override void Write(StatmentBuilder stmts)
        {
            var set = new Store(new Identifier(nt2_s.strRead), nt4_s.ToIR(stmts));
            set.SourceSpan = new SourceSpan(this);
            stmts.Add( set);
        }
    }
}