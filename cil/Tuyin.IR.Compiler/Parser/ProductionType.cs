namespace Tuyin.IR.Compiler.Parser
{
    enum ProductionType 
    {
        Terminal,
        Or,
        Mapping,
        Concatenation,
        Empty,
        Recursive,
        Repeat,
        Literall,
        Eos
    }
}
