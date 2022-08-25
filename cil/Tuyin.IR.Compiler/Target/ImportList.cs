using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    internal class ImportList : List<Import>, IAST, IEnumerable<Import>
    {
        public ImportList() 
        {
        }

        public ImportList(Import nt1_s)
        {
            base.Add(nt1_s);
        }

        internal new IAST Add(Import nt1_s)
        {
            base.Add(nt1_s);
            return this;
        }
    }
}