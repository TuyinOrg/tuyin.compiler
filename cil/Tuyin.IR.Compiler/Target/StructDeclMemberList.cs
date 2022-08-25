using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    internal class StructDeclMemberList : List<StructDeclMember>, IAST
    {
        public new IAST Add(StructDeclMember member) 
        {
            base.Add(member);
            return this;
        }
    }
}
