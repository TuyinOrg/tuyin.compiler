using LLParserLexerLib;
using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Symbols
{
    public class DIMetadata : Statment, IAST
    {
        public DIMetadata(DIHeader nt1_s, DIProperties nt3_s)
        {
            Header = nt1_s;
            Properties = nt3_s;
        }

        public override AstNodeType NodeType => AstNodeType.Metadata;

        public DIHeader Header { get; }

        public DIProperties Properties { get; }

        public string FileName { get; internal set; }

        public override IEnumerable<AstNode> GetNodes()
        {
            yield return this;
        }
    }
}
