using LLParserLexerLib;
using Tuyin.IR.Compiler.Target.Visitors;

namespace Tuyin.IR.Compiler.Target
{
    internal abstract class AstNode : IAstNode
    {
        public abstract AstNodeType AstType { get; }

        public virtual int StartIndex { get; }

        public virtual int EndIndex { get; }

        internal AstNode(SourceSpan sourceSpan) 
        {
            StartIndex = sourceSpan.StartIndex;
            EndIndex = sourceSpan.EndIndex;
        }

        public virtual T Visit<T>(AstVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    internal interface IAstNode : IAST, ISourceSpan
    {
        AstNodeType AstType { get; }

        T Visit<T>(AstVisitor<T> visitor);
    }
}
