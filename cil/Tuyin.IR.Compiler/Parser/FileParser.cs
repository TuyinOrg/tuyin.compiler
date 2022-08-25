using Tuyin.IR.Compiler.Target;

namespace Tuyin.IR.Compiler.Parser
{
    class FileParser : ParserBase
    {
        private Target.Target mFile;

        public TargetLanguage TargetLanguage => mFile.TargetLanguage;

        public override Lexicon Lexicon => mFile.Lexicon;

        public FileParser(Target.Target file) 
        {
            mFile = file;
        }

        protected override ProductionBase OnDefineGrammar()
        {
            throw new NotImplementedException("not implemente parser entry.");
        }
    }
}
