using System.IO;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;
using Tuyin.IR.Reflection.Symbols;

namespace Tuyin.IR.Analysis.IO
{
    class TextModuleWriter : ModuleWriter
    {
        public TextModuleWriter(DIMetadataManager metadata, Stream stream)
            : base(metadata, stream)
        {
        }

        internal override void Flush()
        {
            throw new System.NotImplementedException();
        }

        internal override void WriteComputeUnit(ComputeUnit unit)
        {
        }

        internal override void WriteImport(String[] path, Identifier id)
        {
        }

        internal override void WriteMetadata(DIMetadata dIMetadata)
        {
        }

        internal override void WriteNamespace(String[] path)
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
