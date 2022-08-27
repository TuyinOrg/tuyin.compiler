using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;
using Tuyin.IR.Reflection.Symbols;

namespace Tuyin.IR.Analysis.IO
{
    abstract class ModuleReader
    {
        internal abstract String[] ReadNamespace();
        internal abstract ISignature ReadSignature();
        internal abstract Import ReadImport();
        internal abstract Function ReadFunction();
        internal abstract DIMetadata ReadMetadata();
    }
}
