using System.IO;
using Tuyin.IR.Analysis.Data;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;
using Tuyin.IR.Reflection.Symbols;
using String = Tuyin.IR.Reflection.Instructions.String;

namespace Tuyin.IR.Analysis.IO
{
    internal class BinaryModuleReader : ModuleReader
    {
        private BinaryReader mReader;
        private string[] mStringTables;
        private DIMetadataManager mManager;

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
            var p1 = (Linkage)mReader.ReadByte();
            var p2 = (Visibility)mReader.ReadByte();
            var p3 = new Identifier(mStringTables[mReader.ReadInt32()]);

            var pc = mReader.ReadInt32();
            var args = new Parameter[pc];
            for (var i = 0; i < pc; i++)
            {
               var id = new Identifier(mStringTables[mReader.ReadInt32()])
               {
                   SourceSpan = new SourceSpan(mReader.ReadInt32(), mReader.ReadInt32())
               };
                args[i] = new Parameter(id);
            }

            // 解析微码到文法
            var length = mReader.ReadInt32();
            var codes = new Microcode[length];
            for (var i = 0; i < length; i++) 
            {
                var op = (OpCode)mReader.ReadByte();
                switch (op.GetOperandType())
                {
                    case OperandType.None:
                        codes[i] = new Microcode(op, default);
                        break;
                    case OperandType.Boolean:
                        codes[i] = new Microcode(op, mReader.ReadBoolean());
                        break;
                    case OperandType.Int8:
                        codes[i] = new Microcode(op, mReader.ReadSByte());
                        break;
                    case OperandType.Uint8:
                        codes[i] = new Microcode(op, mReader.ReadByte());
                        break;
                    case OperandType.Int16:
                        codes[i] = new Microcode(op, mReader.ReadInt16());
                        break;
                    case OperandType.Uint16:
                        codes[i] = new Microcode(op, mReader.ReadUInt16());
                        break;
                    case OperandType.Int32:
                        codes[i] = new Microcode(op, mReader.ReadInt32());
                        break;
                    case OperandType.Uint32:
                        codes[i] = new Microcode(op, mReader.ReadUInt32());
                        break;
                    case OperandType.Int64:
                        codes[i] = new Microcode(op, mReader.ReadInt64());
                        break;
                    case OperandType.UInt64:
                        codes[i] = new Microcode(op, mReader.ReadUInt64());
                        break;
                    case OperandType.Float32:
                        codes[i] = new Microcode(op, mReader.ReadSingle());
                        break;
                    case OperandType.Float64:
                        codes[i] = new Microcode(op, mReader.ReadDouble());
                        break;
                    case OperandType.Char:
                        codes[i] = new Microcode(op, mReader.ReadChar());
                        break;
                    case OperandType.String:
                        codes[i] = new Microcode(op, mReader.ReadUInt32());
                        break;
                }
            }

            return new Function(p1, p2, p3, args, null);
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
            var strs = new String[length];
            for (var i = 0; i < length; i++)
                strs[i] = new String(
                    mStringTables[mReader.ReadInt32()], 
                    new SourceSpan(mReader.ReadInt32(), mReader.ReadInt32()));

            return new Import(
                strs, 
                new Identifier(
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

        internal override String[] ReadNamespace()
        {
            var length = mReader.ReadInt32();
            var strs = new String[length];
            for (var i = 0; i < length; i++)
                strs[i] = new String(
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
