using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class StmtIf : StmtScope
    {
        private ExprRoot nt3_s;
        private StmtRoot nt5_s;
        private StmtRoot nt7_s;

        public override AstNodeType AstType => AstNodeType.StmtIf;

        public StmtIf(SourceSpan sourceSpan, ExprRoot nt3_s, StmtRoot nt5_s)
            : base(sourceSpan)
        {
            this.nt3_s = nt3_s;
            this.nt5_s = nt5_s;
        }

        public StmtIf(SourceSpan sourceSpan, ExprRoot nt3_s, StmtRoot nt5_s, StmtRoot nt7_s) 
            : this(sourceSpan, nt3_s, nt5_s)
        {
            this.nt7_s = nt7_s;
        }

        internal override void Write(StatmentBuilder stmts)
        {
            var end = new Label();

            // 判断是否为true
            stmts.Add(new Test(end, nt3_s.ToIR(stmts)));

            // 写入文法
            if (nt5_s is StmtScope)
            {
                nt5_s.Write(stmts);
            }
            else 
            {
                stmts.StartScope(nt5_s.StartIndex);
                nt5_s.Write(stmts);
                stmts.EndScope(nt5_s.EndIndex);
            }

            // 写入else
            if (nt7_s != null)
            {
                var elseEnd = new Label();
                stmts.Add(new Goto(elseEnd));

                // 定位label
                stmts.DefineLabel(end);

                if (nt7_s is StmtScope)
                {
                    nt7_s.Write(stmts);
                }
                else
                {
                    stmts.StartScope(nt7_s.StartIndex);
                    nt7_s.Write(stmts);
                    stmts.EndScope(nt7_s.EndIndex);
                }

                stmts.DefineLabel(elseEnd);
            }
            else
            {
                // 定位label
                stmts.DefineLabel(end);
            }
        }
    }
}