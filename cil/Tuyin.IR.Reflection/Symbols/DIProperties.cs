using LLParserLexerLib;
using System.Collections.Generic;
using System.Linq;

namespace Tuyin.IR.Reflection.Symbols;

public class DIProperties : List<DIProperty>, IAST
{
    public DIProperties() 
    {
    }

    public DIProperties(params DIProperty[] nt1_s)
    {
        base.AddRange(nt1_s);
    }

    internal new IAST Add(DIProperty property)
    {
        base.Add(property);
        return this;
    }

    public string Parse(DIMetadataManager manager)
    {
        return string.Join(",", base.ToArray().Select(x => x.Value.Const.ToString()));
    }
}