using System;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Analysis.Data
{
    public struct Microcode
    {
        public OpCode OpCode { get; }

        public Union8 Operand { get; }

        public OperandType OperandType => OpCode.GetOperandType();

        public FlowType FlowType => OpCode.GetFlowType();

        public Microcode(OpCode opcode, Union8 operand)
        {
            OpCode = opcode;
            Operand = operand;
        }

        public override string ToString()
        {
            if (OperandType == OperandType.None)
            {
                return OpCode.ToString().ToLower();
            }
            else
            {

                var operand = string.Empty;
                switch (OperandType)
                {
                    case OperandType.Boolean:
                        operand = Operand.Boolean0 ? "true" : "false";
                        break;
                    case OperandType.Int8:
                        operand = Operand.SByte0.ToString();
                        break;
                    case OperandType.Uint8:
                        operand = Operand.Byte0.ToString();
                        break;
                    case OperandType.Int16:
                        operand = Operand.Short0.ToString();
                        break;
                    case OperandType.Uint16:
                        operand = Operand.Ushort0.ToString();
                        break;
                    case OperandType.Int32:
                        operand = Operand.Int0.ToString();
                        break;
                    case OperandType.Uint32:
                        operand = Operand.Uint0.ToString();
                        break;
                    case OperandType.Int64:
                        operand = Operand.Long0.ToString();
                        break;
                    case OperandType.UInt64:
                        operand = Operand.Ulong0.ToString();
                        break;
                    case OperandType.Float32:
                        operand = Operand.Single0.ToString();
                        break;
                    case OperandType.Float64:
                        operand = Operand.Double0.ToString();
                        break;
                    case OperandType.Char:
                        operand = Operand.Ushort0 > 32 ? ((char)Operand.Ushort0).ToString() : Operand.Ushort0.ToString();
                        break;
                    case OperandType.String:
                        operand = $"!{Operand.Int0}";
                        break;
                    default:
                        throw new NotImplementedException();
                }

                return $"{OpCode.GetHelperDescrption().ToLower()} {operand}";
            }
        }

        public string ToString(Metadatas metadatas)
        {
            if (OperandType == OperandType.None)
            {
                return OpCode.ToString().ToLower();
            }
            else
            {

                var operand = string.Empty;
                switch (OperandType)
                {
                    case OperandType.Boolean:
                        operand = Operand.Boolean0 ? "true" : "false";
                        break;
                    case OperandType.Int8:
                        operand = Operand.SByte0.ToString();
                        break;
                    case OperandType.Uint8:
                        operand = Operand.Byte0.ToString();
                        break;
                    case OperandType.Int16:
                        operand = Operand.Short0.ToString();
                        break;
                    case OperandType.Uint16:
                        operand = Operand.Ushort0.ToString();
                        break;
                    case OperandType.Int32:
                        operand = Operand.Int0.ToString();
                        break;
                    case OperandType.Uint32:
                        operand = Operand.Uint0.ToString();
                        break;
                    case OperandType.Int64:
                        operand = Operand.Long0.ToString();
                        break;
                    case OperandType.UInt64:
                        operand = Operand.Ulong0.ToString();
                        break;
                    case OperandType.Float32:
                        operand = Operand.Single0.ToString();
                        break;
                    case OperandType.Float64:
                        operand = Operand.Double0.ToString();
                        break;
                    case OperandType.Char:
                        operand = Operand.Ushort0 > 32 ? ((char)Operand.Ushort0).ToString() : Operand.Ushort0.ToString();
                        break;
                    case OperandType.String:
                        operand = metadatas.GetMetadata(Operand.Int0).Properties[0].Value.Const.ToString();
                        break;
                    default:
                        throw new NotImplementedException();
                }

                return $"{OpCode.GetHelperDescrption().ToLower()} {operand}";
            }
        }
    }
}
