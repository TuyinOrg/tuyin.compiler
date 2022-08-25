using System;
using System.Collections.Generic;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Instructions
{
    internal abstract class TypeInstruction : InstructionWithId
    {
        public virtual uint SizeInWords =>
            throw new NotImplementedException("Can't evaluate value size of type " + OpCode);

        public virtual bool TryGetFriendlyName(out string name)
        {
            if (OpCode == Op.OpTypeBool)
            {
                name = "bool";
                return true;
            }

            if (OpCode == Op.OpTypePointer)
            {
                var op = (OpTypePointer) this;
                var element = (TypeInstruction) op.Type.Instruction;
                if (element.TryGetFriendlyName(out var elementName))
                {
                    name = elementName + "*";
                    return true;
                }
            }
            else if (OpCode == Op.OpTypeFloat)
            {
                if (SizeInWords == 1)
                {
                    name = "float";
                    return true;
                }

                if (SizeInWords == 2)
                {
                    name = "double";
                    return true;
                }
            }
            else if (OpCode == Op.OpTypeInt)
            {
                var op = (OpTypeInt) this;
                if (op.Signedness == 0)
                {
                    if (op.Width == 8)
                    {
                        name = "byte";
                        return true;
                    }

                    if (op.Width == 16)
                    {
                        name = "ushort";
                        return true;
                    }

                    if (op.Width == 32)
                    {
                        name = "uint";
                        return true;
                    }

                    if (op.Width == 64)
                    {
                        name = "ulong";
                        return true;
                    }
                }
                else
                {
                    if (op.Width == 8)
                    {
                        name = "sbyte";
                        return true;
                    }

                    if (op.Width == 16)
                    {
                        name = "short";
                        return true;
                    }

                    if (op.Width == 32)
                    {
                        name = "int";
                        return true;
                    }

                    if (op.Width == 64)
                    {
                        name = "long";
                        return true;
                    }
                }
            }
            else if (OpCode == Op.OpTypeVector)
            {
                var op = (OpTypeVector) this;
                var component = (TypeInstruction) op.ComponentType.Instruction;
                if (component.OpCode == Op.OpTypeFloat && component.SizeInWords == 1)
                {
                    if (op.ComponentCount == 2)
                    {
                        name = "vec2";
                        return true;
                    }

                    if (op.ComponentCount == 3)
                    {
                        name = "vec3";
                        return true;
                    }

                    if (op.ComponentCount == 4)
                    {
                        name = "vec4";
                        return true;
                    }
                }
            }
            else if (OpCode == Op.OpTypeMatrix)
            {
                var op = (OpTypeMatrix) this;
                var column = (TypeInstruction) op.ColumnType.Instruction;
                if (column.OpCode == Op.OpTypeFloat)
                {
                }
            }
            else if (OpCode == Op.OpTypeArray)
            {
                var op = (OpTypeArray) this;
                var element = (TypeInstruction) op.ElementType.Instruction;
                if (element.TryGetFriendlyName(out var elementName))
                {
                    var length = (OpConstant) op.Length.Instruction;
                    var arraySize = length.Value.Value.ToInt32();
                    name = elementName + "[" + arraySize + "]";
                    return true;
                }
            }
            else if (OpCode == Op.OpTypeStruct)
            {
                var op = (OpTypeStruct) this;
                var members = new List<string>();
                foreach (var memberType in op.MemberTypes)
                {
                    if (!((TypeInstruction) memberType.Instruction).TryGetFriendlyName(out var memberName))
                    {
                        name = OpCode.ToString();
                        return false;
                    }

                    members.Add(memberName);
                }

                name = "struct{" + string.Join(",", members) + "}";
                return true;
            }

            name = OpCode.ToString();
            return false;
        }
    }
}