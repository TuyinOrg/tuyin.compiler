using System.IO;
using Tuyin.IR.Reflection.Instructions;
using Tuyin.IR.Reflection.Symbols;

namespace Tuyin.IR.Reflection.IO
{
    internal class TextModuleReader : ModuleReader
    {
        private StreamReader mReader;

        public TextModuleReader(Stream stream)
        {
            mReader = new StreamReader(stream);
        }

        internal override Function ReadFunction()
        {
            throw new System.NotImplementedException();
        }

        internal override Import ReadImport()
        {
            throw new System.NotImplementedException();
        }

        internal override DIMetadata ReadMetadata()
        {
            throw new System.NotImplementedException();
        }

        internal override String[] ReadNamespace()
        {
            throw new System.NotImplementedException();
        }

        internal override ISignature ReadSignature()
        {
            throw new System.NotImplementedException();
        }
    }
}
