using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    class Args : List<TypeNamed>, IAST
    {
        public Args()
        {
        }

        public Args(TypeNamed nt1_s)
        {
            base.Add(nt1_s);
        }

        internal new IAST Add(TypeNamed nt2_s)
        {
            base.Add(nt2_s);
            return this;
        }
    }
}