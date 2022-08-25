using System.Collections.Generic;
using System.IO;
using Tuyin.IR.Reflection.Symbols;

namespace Tuyin.IR.Reflection.IO
{
    class BinaryModuleWriter : ModuleWriter
    {
        private BinaryWriter mBinaryWriter;
        private Dictionary<string, int> mStringTable; 

        public BinaryModuleWriter(DIMetadataManager metadata, Stream stream) 
            : base(metadata, stream)
        {
            mBinaryWriter = new BinaryWriter(new MemoryStream());
            mStringTable = new Dictionary<string, int>();
        }

        internal override void Flush()
        {
            var br = new BinaryWriter(Stream);
            br.Write(mStringTable.Count);
            foreach (var str in mStringTable)
                br.Write(str.Key);

            mBinaryWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            mBinaryWriter.BaseStream.CopyTo(br.BaseStream, (int)mBinaryWriter.BaseStream.Length);
            mBinaryWriter.BaseStream.Close();
        }

        internal override void WriteFunction(Function func)
        {
            mBinaryWriter.Write((byte)func.Linkage);
            mBinaryWriter.Write((byte)func.Visibility);
            mBinaryWriter.Write(WriteString(func.Identifier.Value));
        }

        internal override void WriteImport(Instructions.String[] path, Instructions.Identifier id)
        {
            mBinaryWriter.Write(WriteString("import"));
            mBinaryWriter.Write(path.Length);
            for (var i = 0; i < path.Length; i++)
            {
                var p = path[i];
                mBinaryWriter.Write(WriteString(p.Value));
                mBinaryWriter.Write(p.SourceSpan.StartIndex);
                mBinaryWriter.Write(p.SourceSpan.EndIndex);
            }

            mBinaryWriter.Write(WriteString(id.Value));
            mBinaryWriter.Write(id.SourceSpan.StartIndex);
            mBinaryWriter.Write(id.SourceSpan.EndIndex);
        }

        internal override void WriteMetadata(DIMetadata metadata)
        {
            mBinaryWriter.Write(metadata.ToString());
        }

        internal override void WriteNamespace(Instructions.String[] path)
        {
            mBinaryWriter.Write(path.Length);
            for (var i = 0; i < path.Length; i++)
            {
                var p = path[i];
                mBinaryWriter.Write(WriteString(p.Value));
                mBinaryWriter.Write(p.SourceSpan.StartIndex);
                mBinaryWriter.Write(p.SourceSpan.EndIndex);
            }
        }

        internal override void WriteSignature(ISignature signature)
        {
            mBinaryWriter.Write(WriteString(signature.Content));
        }

        internal override int WriteString(string str)
        {
            if (!mStringTable.ContainsKey(str))
                mStringTable.Add(str, mStringTable.Count);

            return mStringTable[str];
        }
    }
}
