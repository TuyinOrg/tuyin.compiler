using Tuyin.IR.Analysis;
using Tuyin.IR.Compiler.Uitls;

namespace Tuyin.IR.Compiler.Target.Translates
{
    internal class CSharpTranslater : Translater
    {
        public override TargetLanguage Language => TargetLanguage.CSharp;
    }
}
