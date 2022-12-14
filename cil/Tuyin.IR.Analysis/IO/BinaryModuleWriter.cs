using System.Collections.Generic;
using System.IO;
using Tuyin.IR.Analysis.Data;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;
using Tuyin.IR.Reflection.Symbols;
using System.Linq;

namespace Tuyin.IR.Analysis.IO
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

        internal override void WriteComputeUnit(ComputeUnit unit)
        {
            var func = unit.Function;   
            mBinaryWriter.Write((byte)func.Linkage);
            mBinaryWriter.Write((byte)func.Visibility);
            mBinaryWriter.Write(WriteString(func.Identifier.Value));

            mBinaryWriter.Write(func.Parameters.Count);
            for (var i = 0; i < func.Parameters.Count; i++) 
            {
                var p = func.Parameters[i].Identifier;
                mBinaryWriter.Write(WriteString(p.Value));
                mBinaryWriter.Write(p.SourceSpan.StartIndex);
                mBinaryWriter.Write(p.SourceSpan.EndIndex);
            }

            var codes = GenerateMicrocodes(unit.CFG, unit.DAG);
            mBinaryWriter.Write(codes.Length);
            foreach (var code in codes) 
            {
                mBinaryWriter.Write((byte)code.OpCode);
                switch (code.OperandType)
                {
                    case OperandType.None:
                        break;
                    case OperandType.Boolean:
                        mBinaryWriter.Write(code.Operand.Boolean0);
                        break;
                    case OperandType.Int8:
                        mBinaryWriter.Write(code.Operand.SByte0);
                        break;
                    case OperandType.Uint8:
                        mBinaryWriter.Write(code.Operand.Byte0);
                        break;
                    case OperandType.Int16:
                        mBinaryWriter.Write(code.Operand.Short0);
                        break;
                    case OperandType.Uint16:
                        mBinaryWriter.Write(code.Operand.Ushort0);
                        break;
                    case OperandType.Int32:
                        mBinaryWriter.Write(code.Operand.Int0);
                        break;
                    case OperandType.Uint32:
                        mBinaryWriter.Write(code.Operand.Uint0);
                        break;
                    case OperandType.Int64:
                        mBinaryWriter.Write(code.Operand.Long0);
                        break;
                    case OperandType.UInt64:
                        mBinaryWriter.Write(code.Operand.Ulong0);
                        break;
                    case OperandType.Float32:
                        mBinaryWriter.Write(code.Operand.Single0);
                        break;
                    case OperandType.Float64:
                        mBinaryWriter.Write(code.Operand.Double0);
                        break;
                    case OperandType.Char:
                        mBinaryWriter.Write(code.Operand.Ushort0);
                        break;
                    case OperandType.String:
                        mBinaryWriter.Write(code.Operand.Int0);
                        break;
                }
            }
        }

        /// <summary>
        /// 生产微码
        /// </summary>
        private IReadOnlyArray<Microcode> GenerateMicrocodes(CFG cfg, DAG dag)
        {
            var dagNodes = dag.Vertices.GroupBy(x => x.StatmentIndex).ToDictionary(x => x.Key, x => x.ToArray());
            var codes = new DynamicArray<Microcode>(dag.Vertices.Count);
            var nodes = new Stack<CFGBlockNode>();
            nodes.Push(cfg.Vertices[0]);
            while (nodes.Count > 0) 
            {
                var node = nodes.Pop();
                for (var i = node.Scope.Start; i < node.Vaild; i++) 
                {
                    if (dagNodes.ContainsKey(i))
                    {
                        var dags = dagNodes[i];
                        for (int x = 0; x < dags.Length; x++)
                        {
                            if (dags[x] is DAGMicrocodeNode mn)
                            {
                                if (codes.Count == 0)
                                    codes.Add(mn.Microcode);
                                else
                                    codes.Insert(0, mn.Microcode);
                            }
                            else if (dags[x] is DAGStoreNode sn)
                            {

                            }
                            else throw new System.NotImplementedException();
                        }
                    }
                }

                foreach (var right in node.Rights)
                    nodes.Push(right.Target as CFGBlockNode);
            }

            return codes;
        }

        internal override void WriteImport(String[] path, Identifier id)
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

        internal override void WriteNamespace(String[] path)
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
