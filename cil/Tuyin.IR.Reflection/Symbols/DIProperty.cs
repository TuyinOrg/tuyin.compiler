using LLParserLexerLib;

namespace Tuyin.IR.Reflection.Symbols;

public class DIProperty
{
    public DIProperty(DIToken nt1_s, DIExpression nt3_s)
    {
        Name = nt1_s;
        Value = nt3_s;
    }

    public DIToken Name { get; }

    public DIExpression Value { get; }
}