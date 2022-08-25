using Tuyin.IR.Compiler.Parser;

namespace Tuyin.IR.Compiler.Tests
{
    abstract class BaseTest : ParserBase
    {
        public override Lexicon Lexicon { get; } = new Lexicon();
    }
}
