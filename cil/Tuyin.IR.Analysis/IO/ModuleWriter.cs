using System.IO;
using Tuyin.IR.Analysis.Data;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;
using Tuyin.IR.Reflection.Symbols;
using String = Tuyin.IR.Reflection.Instructions.String;

namespace Tuyin.IR.Analysis.IO
{
    abstract class ModuleWriter
    {
        protected ModuleWriter(DIMetadataManager diManager, Stream stream) 
        {
            MetadataManager = diManager;
            Stream = stream;
        }

        public DIMetadataManager MetadataManager { get; }

        public Stream Stream { get; }

        internal abstract int WriteString(string str);
        internal abstract void WriteNamespace(String[] path);
        internal abstract void WriteSignature(ISignature signature);
        internal abstract void WriteImport(String[] path, Identifier identifier);
        internal abstract void WriteComputeUnit(ComputeUnit unit);
        internal abstract void WriteMetadata(DIMetadata dIMetadata);
        internal abstract void Flush();
    }
}
