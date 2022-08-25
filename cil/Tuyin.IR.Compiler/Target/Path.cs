using LLParserLexerLib;
using String = Tuyin.IR.Reflection.Instructions.String;

namespace Tuyin.IR.Compiler.Target
{
    class Path : List<TokenAST>, IAST, IEnumerable<String>
    {
        public Path(TokenAST nt1_s)
        {
            base.Add(nt1_s);
        }

        internal new IAST Add(TokenAST nt2_s)
        {
            base.Add(nt2_s);
            return this;
        }

        public String[] ToStrings() 
        {
            String[] res = new String[Count];
            for (var i = 0; i < Count; i++)
                res[i] = new String(base[i].tokenStr);

            return res;
        }

        IEnumerator<String> IEnumerable<String>.GetEnumerator()
        {
            return ToStrings().GetEnumerator() as IEnumerator<String>;
        }
    }
}
