using System;
using System.IO;
using Tuyin.IR.Reflection.Symbols;

namespace Tuyin.IR.Reflection.IO
{
    internal class BinaryModuleReader : ModuleReader
    {
        private BinaryReader mReader;
        private DIMetadataManager mManager;
        private string[] mStringTables;

        public BinaryModuleReader(Stream stream)
        {
            mReader = new BinaryReader(stream);
            var count = mReader.ReadUInt32();
            mStringTables = new string[count];
            for (var i = 0; i < count; i++)
                mStringTables[i] = mReader.ReadString();

            //mManager = new DIMetadataManager(new DbgHeader())
        }

        internal DIMetadataManager ReadDebugHeader() 
        {
            //DbgHeader dbg = new DbgHeader(DICompileUnit)
            return null;
        }

        internal override Function ReadFunction()
        {
            return new Function(
                (Linkage)mReader.ReadByte(),
                (Visibility)mReader.ReadByte(), 
                new Instructions.Identifier(mStringTables[mReader.ReadInt32()]), 
                null, 
                null, 
                null);
        }

        private Scope ReadScope() 
        {
            var id = mStringTables[mReader.ReadInt32()];
            return null;
        }

        internal override Import ReadImport()
        {
            var length = mReader.ReadInt32();
            var strs = new Instructions.String[length];
            for (var i = 0; i < length; i++)
            {
                var str = mStringTables[mReader.ReadInt32()];
                strs[i] = new Instructions.String(str, new SourceSpan(mReader.ReadInt32(), mReader.ReadInt32()));
            }

            return new Import(
                strs, 
                new Instructions.Identifier(mStringTables[mReader.ReadInt32()])
                { SourceSpan = new SourceSpan(mReader.ReadInt32(), mReader.ReadInt32()) });
        }

        internal override DIMetadata ReadMetadata()
        {
            var str = mStringTables[mReader.ReadInt32()];
            return null;
        }

        internal override Instructions.String[] ReadNamespace()
        {
            var length = mReader.ReadInt32();
            var strs = new Instructions.String[length];
            for (var i = 0; i < length; i++)
            {
                var str = mStringTables[mReader.ReadInt32()];
                strs[i] = new Instructions.String(str, new SourceSpan(mReader.ReadInt32(), mReader.ReadInt32()));
            }

            return strs;
        }

        internal override ISignature ReadSignature()
        {
            throw new NotImplementedException();
        }
    }
}
