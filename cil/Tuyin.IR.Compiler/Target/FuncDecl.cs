using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class FuncDecl : Declare
    {
        private TypeNamed nt3_s;
        private Args nt5_s;
        private SourceSpan sourceSpan;

        internal FuncDecl(SourceSpan sourceSpan, TypeNamed nt3_s, Args nt5_s, StmtRoot nt7_s)
        {
            this.nt3_s = nt3_s;
            this.nt5_s = nt5_s;
            this.sourceSpan = sourceSpan;

            Body = nt7_s;
        }

        public override DeclareType DeclareType => DeclareType.Function;

        public AstNodeType AstType => AstNodeType.FuncDecl;

        public override int StartIndex => sourceSpan.StartIndex;

        public override int EndIndex => sourceSpan.EndIndex;

        public StmtRoot Body { get; }
        internal Function ToIR()
        {
            Linkage linkage = Modifiter.Modifiter switch
            {
                Compiler.Target.Modifiter.Private => Linkage.Private,
                Compiler.Target.Modifiter.Internal => Linkage.Internal,
                _ => Linkage.External
            };

            Visibility visibility = Modifiter.Modifiter switch
            {
                Compiler.Target.Modifiter.Protected => Visibility.Protected,
                Compiler.Target.Modifiter.Private => Visibility.Hidden,
                _ => Visibility.Default
            };

            var builder = new StatmentBuilder();

            builder.StartScope(sourceSpan.StartIndex);
            if (Body is StmtScope)
            {
                Body.Write(builder);
            }
            else 
            {
                builder.StartScope(Body.StartIndex);
                Body.Write(builder);
                builder.EndScope(Body.EndIndex);
            }
            var scope = builder.EndScope(sourceSpan.EndIndex);

            var func = new Function(
                linkage,
                visibility,
                new Identifier(nt3_s.Name.strRead),
                scope,
                nt5_s.Select(x => new Parameter(new Identifier(x.Name.strRead))), builder);

            return func;
        }
    }
}