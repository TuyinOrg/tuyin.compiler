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

        internal override Import ReadImport()
        {
            if (mReader.BaseStream.Position >= mReader.BaseStream.Length - 4)
                return null;

            var index = mReader.ReadInt32();
            if (index >= mStringTables.Length || mStringTables[index] != "import") 
            {
                mReader.BaseStream.Position -= 4;
                return null;
            }

            var length = mReader.ReadInt32();
            var strs = new Instructions.String[length];
            for (var i = 0; i < length; i++)
                strs[i] = new Instructions.String(
                    mStringTables[mReader.ReadInt32()], 
                    new SourceSpan(mReader.ReadInt32(), mReader.ReadInt32()));

            return new Import(
                strs, 
                new Instructions.Identifier(
                    mStringTables[mReader.ReadInt32()]) 
                { 
                    SourceSpan = new SourceSpan(mReader.ReadInt32(), mReader.ReadInt32()) 
                });
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
                strs[i] = new Instructions.String(
                    mStringTables[mReader.ReadInt32()], 
                    new SourceSpan(mReader.ReadInt32(), mReader.ReadInt32()));

            return strs;
        }

        internal override ISignature ReadSignature()
        {
            return new ModuleSignature(mStringTables[mReader.ReadInt32()]);
        }
    }
}
