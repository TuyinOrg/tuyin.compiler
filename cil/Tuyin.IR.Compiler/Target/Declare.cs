using LLParserLexerLib;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Compiler.Target
{
    internal abstract class Declare : IAST, ISourceSpan
    {
        public abstract DeclareType DeclareType { get; }

        public abstract int StartIndex { get; }

        public abstract int EndIndex { get; }

        public SourceModifiter Modifiter { get; private set; }

        internal Declare SetModifiter(SourceModifiter modifiter)
        {
            Modifiter = modifiter;
            return this;
        }
    }
}
