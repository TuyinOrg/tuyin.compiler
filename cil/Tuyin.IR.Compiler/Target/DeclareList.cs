using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    internal class DeclareList : List<Declare>,  IAST
    {
        public DeclareList() 
        {

        }

        internal new IAST Add(Declare nt1_s)
        {
            base.Add(nt1_s);
            return this;
        }
    }
}