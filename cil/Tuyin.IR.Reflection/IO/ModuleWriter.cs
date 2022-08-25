using System.IO;
using Tuyin.IR.Reflection.Instructions;
using Tuyin.IR.Reflection.Symbols;
using String = Tuyin.IR.Reflection.Instructions.String;

namespace Tuyin.IR.Reflection.IO
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
        internal abstract void WriteFunction(Function function);
        internal abstract void WriteMetadata(DIMetadata dIMetadata);
        internal abstract void Flush();
    }
}
