using System.IO;
using Tuyin.IR.Reflection.Symbols;

namespace Tuyin.IR.Reflection.IO
{
    class TextModuleWriter : ModuleWriter
    {
        public TextModuleWriter(DIMetadataManager metadata, Stream stream)
            : base(metadata, stream)
        {
        }

        internal override void WriteFunction(Function function)
        {
        }

        internal override void WriteImport(Instructions.String[] path, Instructions.Identifier id)
        {
        }

        internal override void WriteMetadata(DIMetadata dIMetadata)
        {
        }

        internal override void WriteNamespace(Instructions.String[] path)
        {
        }

        internal override void WriteSignature(ISignature signature)
        {
        }

        internal override int WriteString(string str)
        {
            throw new System.NotImplementedException();
        }
    }
}
