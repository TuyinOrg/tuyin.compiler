using System;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection;
using Toe.SPIRV.Reflection.Nodes;

namespace Toe.SPIRV.Spv
{
    internal class NestedInstruction
    {
        private readonly Instruction _instruction;

        public NestedInstruction(Instruction instruction)
        {
            _instruction = instruction;
        }

        public NestedInstruction(Node node, SpirvInstructionsBuilderBase builder)
        {
            switch (node.OpCode)
            {
                case Op.OpAccessChain:
                {
                    var n = (AccessChain) node;
                    var instruction = new OpAccessChain();
                    _instruction = instruction;
                    instruction.Base = builder.Visit(n.Base);
                    instruction.Indexes = builder.Visit(n.Indexes);
                    break;
                }
                case Op.OpInBoundsAccessChain:
                {
                    var n = (InBoundsAccessChain) node;
                    var instruction = new OpInBoundsAccessChain();
                    _instruction = instruction;
                    instruction.Base = builder.Visit(n.Base);
                    instruction.Indexes = builder.Visit(n.Indexes);
                    break;
                }
                case Op.OpPtrAccessChain:
                {
                    var n = (PtrAccessChain) node;
                    var instruction = new OpPtrAccessChain();
                    _instruction = instruction;
                    instruction.Base = builder.Visit(n.Base);
                    instruction.Element = builder.Visit(n.Element);
                    instruction.Indexes = builder.Visit(n.Indexes);
                    break;
                }
                case Op.OpInBoundsPtrAccessChain:
                {
                    var n = (InBoundsPtrAccessChain) node;
                    var instruction = new OpInBoundsPtrAccessChain();
                    _instruction = instruction;
                    instruction.Base = builder.Visit(n.Base);
                    instruction.Element = builder.Visit(n.Element);
                    instruction.Indexes = builder.Visit(n.Indexes);
                    break;
                }
                case Op.OpVectorShuffle:
                {
                    var n = (VectorShuffle) node;
                    var instruction = new OpVectorShuffle();
                    _instruction = instruction;
                    instruction.Vector1 = builder.Visit(n.Vector1);
                    instruction.Vector2 = builder.Visit(n.Vector2);
                    instruction.Components = builder.Visit(n.Components);
                    break;
                }
                case Op.OpCompositeExtract:
                {
                    var n = (CompositeExtract) node;
                    var instruction = new OpCompositeExtract();
                    _instruction = instruction;
                    instruction.Composite = builder.Visit(n.Composite);
                    instruction.Indexes = builder.Visit(n.Indexes);
                    break;
                }
                case Op.OpCompositeInsert:
                {
                    var n = (CompositeInsert) node;
                    var instruction = new OpCompositeInsert();
                    _instruction = instruction;
                    instruction.Object = builder.Visit(n.Object);
                    instruction.Composite = builder.Visit(n.Composite);
                    instruction.Indexes = builder.Visit(n.Indexes);
                    break;
                }
                case Op.OpConvertFToU:
                {
                    var n = (ConvertFToU) node;
                    var instruction = new OpConvertFToU();
                    _instruction = instruction;
                    instruction.FloatValue = builder.Visit(n.FloatValue);
                    break;
                }
                case Op.OpConvertFToS:
                {
                    var n = (ConvertFToS) node;
                    var instruction = new OpConvertFToS();
                    _instruction = instruction;
                    instruction.FloatValue = builder.Visit(n.FloatValue);
                    break;
                }
                case Op.OpConvertSToF:
                {
                    var n = (ConvertSToF) node;
                    var instruction = new OpConvertSToF();
                    _instruction = instruction;
                    instruction.SignedValue = builder.Visit(n.SignedValue);
                    break;
                }
                case Op.OpConvertUToF:
                {
                    var n = (ConvertUToF) node;
                    var instruction = new OpConvertUToF();
                    _instruction = instruction;
                    instruction.UnsignedValue = builder.Visit(n.UnsignedValue);
                    break;
                }
                case Op.OpUConvert:
                {
                    var n = (UConvert) node;
                    var instruction = new OpUConvert();
                    _instruction = instruction;
                    instruction.UnsignedValue = builder.Visit(n.UnsignedValue);
                    break;
                }
                case Op.OpSConvert:
                {
                    var n = (SConvert) node;
                    var instruction = new OpSConvert();
                    _instruction = instruction;
                    instruction.SignedValue = builder.Visit(n.SignedValue);
                    break;
                }
                case Op.OpFConvert:
                {
                    var n = (FConvert) node;
                    var instruction = new OpFConvert();
                    _instruction = instruction;
                    instruction.FloatValue = builder.Visit(n.FloatValue);
                    break;
                }
                case Op.OpQuantizeToF16:
                {
                    var n = (QuantizeToF16) node;
                    var instruction = new OpQuantizeToF16();
                    _instruction = instruction;
                    instruction.Value = builder.Visit(n.Value);
                    break;
                }
                case Op.OpConvertPtrToU:
                {
                    var n = (ConvertPtrToU) node;
                    var instruction = new OpConvertPtrToU();
                    _instruction = instruction;
                    instruction.Pointer = builder.Visit(n.Pointer);
                    break;
                }
                case Op.OpConvertUToPtr:
                {
                    var n = (ConvertUToPtr) node;
                    var instruction = new OpConvertUToPtr();
                    _instruction = instruction;
                    instruction.IntegerValue = builder.Visit(n.IntegerValue);
                    break;
                }
                case Op.OpPtrCastToGeneric:
                {
                    var n = (PtrCastToGeneric) node;
                    var instruction = new OpPtrCastToGeneric();
                    _instruction = instruction;
                    instruction.Pointer = builder.Visit(n.Pointer);
                    break;
                }
                case Op.OpGenericCastToPtr:
                {
                    var n = (GenericCastToPtr) node;
                    var instruction = new OpGenericCastToPtr();
                    _instruction = instruction;
                    instruction.Pointer = builder.Visit(n.Pointer);
                    break;
                }
                case Op.OpBitcast:
                {
                    var n = (Bitcast) node;
                    var instruction = new OpBitcast();
                    _instruction = instruction;
                    instruction.Operand = builder.Visit(n.Operand);
                    break;
                }
                case Op.OpSNegate:
                {
                    var n = (SNegate) node;
                    var instruction = new OpSNegate();
                    _instruction = instruction;
                    instruction.Operand = builder.Visit(n.Operand);
                    break;
                }
                case Op.OpFNegate:
                {
                    var n = (FNegate) node;
                    var instruction = new OpFNegate();
                    _instruction = instruction;
                    instruction.Operand = builder.Visit(n.Operand);
                    break;
                }
                case Op.OpIAdd:
                {
                    var n = (IAdd) node;
                    var instruction = new OpIAdd();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpFAdd:
                {
                    var n = (FAdd) node;
                    var instruction = new OpFAdd();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpISub:
                {
                    var n = (ISub) node;
                    var instruction = new OpISub();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpFSub:
                {
                    var n = (FSub) node;
                    var instruction = new OpFSub();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpIMul:
                {
                    var n = (IMul) node;
                    var instruction = new OpIMul();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpFMul:
                {
                    var n = (FMul) node;
                    var instruction = new OpFMul();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpUDiv:
                {
                    var n = (UDiv) node;
                    var instruction = new OpUDiv();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpSDiv:
                {
                    var n = (SDiv) node;
                    var instruction = new OpSDiv();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpFDiv:
                {
                    var n = (FDiv) node;
                    var instruction = new OpFDiv();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpUMod:
                {
                    var n = (UMod) node;
                    var instruction = new OpUMod();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpSRem:
                {
                    var n = (SRem) node;
                    var instruction = new OpSRem();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpSMod:
                {
                    var n = (SMod) node;
                    var instruction = new OpSMod();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpFRem:
                {
                    var n = (FRem) node;
                    var instruction = new OpFRem();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpFMod:
                {
                    var n = (FMod) node;
                    var instruction = new OpFMod();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpLogicalEqual:
                {
                    var n = (LogicalEqual) node;
                    var instruction = new OpLogicalEqual();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpLogicalNotEqual:
                {
                    var n = (LogicalNotEqual) node;
                    var instruction = new OpLogicalNotEqual();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpLogicalOr:
                {
                    var n = (LogicalOr) node;
                    var instruction = new OpLogicalOr();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpLogicalAnd:
                {
                    var n = (LogicalAnd) node;
                    var instruction = new OpLogicalAnd();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpSelect:
                {
                    var n = (Select) node;
                    var instruction = new OpSelect();
                    _instruction = instruction;
                    instruction.Condition = builder.Visit(n.Condition);
                    instruction.Object1 = builder.Visit(n.Object1);
                    instruction.Object2 = builder.Visit(n.Object2);
                    break;
                }
                case Op.OpIEqual:
                {
                    var n = (IEqual) node;
                    var instruction = new OpIEqual();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpINotEqual:
                {
                    var n = (INotEqual) node;
                    var instruction = new OpINotEqual();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpUGreaterThan:
                {
                    var n = (UGreaterThan) node;
                    var instruction = new OpUGreaterThan();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpSGreaterThan:
                {
                    var n = (SGreaterThan) node;
                    var instruction = new OpSGreaterThan();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpUGreaterThanEqual:
                {
                    var n = (UGreaterThanEqual) node;
                    var instruction = new OpUGreaterThanEqual();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpSGreaterThanEqual:
                {
                    var n = (SGreaterThanEqual) node;
                    var instruction = new OpSGreaterThanEqual();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpULessThan:
                {
                    var n = (ULessThan) node;
                    var instruction = new OpULessThan();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpSLessThan:
                {
                    var n = (SLessThan) node;
                    var instruction = new OpSLessThan();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpULessThanEqual:
                {
                    var n = (ULessThanEqual) node;
                    var instruction = new OpULessThanEqual();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpSLessThanEqual:
                {
                    var n = (SLessThanEqual) node;
                    var instruction = new OpSLessThanEqual();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpShiftRightLogical:
                {
                    var n = (ShiftRightLogical) node;
                    var instruction = new OpShiftRightLogical();
                    _instruction = instruction;
                    instruction.Base = builder.Visit(n.Base);
                    instruction.Shift = builder.Visit(n.Shift);
                    break;
                }
                case Op.OpShiftRightArithmetic:
                {
                    var n = (ShiftRightArithmetic) node;
                    var instruction = new OpShiftRightArithmetic();
                    _instruction = instruction;
                    instruction.Base = builder.Visit(n.Base);
                    instruction.Shift = builder.Visit(n.Shift);
                    break;
                }
                case Op.OpShiftLeftLogical:
                {
                    var n = (ShiftLeftLogical) node;
                    var instruction = new OpShiftLeftLogical();
                    _instruction = instruction;
                    instruction.Base = builder.Visit(n.Base);
                    instruction.Shift = builder.Visit(n.Shift);
                    break;
                }
                case Op.OpBitwiseOr:
                {
                    var n = (BitwiseOr) node;
                    var instruction = new OpBitwiseOr();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpBitwiseXor:
                {
                    var n = (BitwiseXor) node;
                    var instruction = new OpBitwiseXor();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpBitwiseAnd:
                {
                    var n = (BitwiseAnd) node;
                    var instruction = new OpBitwiseAnd();
                    _instruction = instruction;
                    instruction.Operand1 = builder.Visit(n.Operand1);
                    instruction.Operand2 = builder.Visit(n.Operand2);
                    break;
                }
                case Op.OpNot:
                {
                    var n = (Not) node;
                    var instruction = new OpNot();
                    _instruction = instruction;
                    instruction.Operand = builder.Visit(n.Operand);
                    break;
                }
                default:
                    throw new NotImplementedException(node.OpCode +" is not implemented yet.");
            }
        }

        public Instruction Instruction => _instruction;

        public uint GetWordCount()
        {
            uint wordCount = 1;
            switch (_instruction.OpCode)
            {
                case Op.OpAccessChain:
                {
                    var i = (OpAccessChain) _instruction;
                    wordCount += i.Base.GetWordCount();
                    wordCount += i.Indexes.GetWordCount();
                    return wordCount;
                }
                case Op.OpInBoundsAccessChain:
                {
                    var i = (OpInBoundsAccessChain) _instruction;
                    wordCount += i.Base.GetWordCount();
                    wordCount += i.Indexes.GetWordCount();
                    return wordCount;
                }
                case Op.OpPtrAccessChain:
                {
                    var i = (OpPtrAccessChain) _instruction;
                    wordCount += i.Base.GetWordCount();
                    wordCount += i.Element.GetWordCount();
                    wordCount += i.Indexes.GetWordCount();
                    return wordCount;
                }
                case Op.OpInBoundsPtrAccessChain:
                {
                    var i = (OpInBoundsPtrAccessChain) _instruction;
                    wordCount += i.Base.GetWordCount();
                    wordCount += i.Element.GetWordCount();
                    wordCount += i.Indexes.GetWordCount();
                    return wordCount;
                }
                case Op.OpVectorShuffle:
                {
                    var i = (OpVectorShuffle) _instruction;
                    wordCount += i.Vector1.GetWordCount();
                    wordCount += i.Vector2.GetWordCount();
                    wordCount += i.Components.GetWordCount();
                    return wordCount;
                }
                case Op.OpCompositeExtract:
                {
                    var i = (OpCompositeExtract) _instruction;
                    wordCount += i.Composite.GetWordCount();
                    wordCount += i.Indexes.GetWordCount();
                    return wordCount;
                }
                case Op.OpCompositeInsert:
                {
                    var i = (OpCompositeInsert) _instruction;
                    wordCount += i.Object.GetWordCount();
                    wordCount += i.Composite.GetWordCount();
                    wordCount += i.Indexes.GetWordCount();
                    return wordCount;
                }
                case Op.OpConvertFToU:
                {
                    var i = (OpConvertFToU) _instruction;
                    wordCount += i.FloatValue.GetWordCount();
                    return wordCount;
                }
                case Op.OpConvertFToS:
                {
                    var i = (OpConvertFToS) _instruction;
                    wordCount += i.FloatValue.GetWordCount();
                    return wordCount;
                }
                case Op.OpConvertSToF:
                {
                    var i = (OpConvertSToF) _instruction;
                    wordCount += i.SignedValue.GetWordCount();
                    return wordCount;
                }
                case Op.OpConvertUToF:
                {
                    var i = (OpConvertUToF) _instruction;
                    wordCount += i.UnsignedValue.GetWordCount();
                    return wordCount;
                }
                case Op.OpUConvert:
                {
                    var i = (OpUConvert) _instruction;
                    wordCount += i.UnsignedValue.GetWordCount();
                    return wordCount;
                }
                case Op.OpSConvert:
                {
                    var i = (OpSConvert) _instruction;
                    wordCount += i.SignedValue.GetWordCount();
                    return wordCount;
                }
                case Op.OpFConvert:
                {
                    var i = (OpFConvert) _instruction;
                    wordCount += i.FloatValue.GetWordCount();
                    return wordCount;
                }
                case Op.OpQuantizeToF16:
                {
                    var i = (OpQuantizeToF16) _instruction;
                    wordCount += i.Value.GetWordCount();
                    return wordCount;
                }
                case Op.OpConvertPtrToU:
                {
                    var i = (OpConvertPtrToU) _instruction;
                    wordCount += i.Pointer.GetWordCount();
                    return wordCount;
                }
                case Op.OpConvertUToPtr:
                {
                    var i = (OpConvertUToPtr) _instruction;
                    wordCount += i.IntegerValue.GetWordCount();
                    return wordCount;
                }
                case Op.OpPtrCastToGeneric:
                {
                    var i = (OpPtrCastToGeneric) _instruction;
                    wordCount += i.Pointer.GetWordCount();
                    return wordCount;
                }
                case Op.OpGenericCastToPtr:
                {
                    var i = (OpGenericCastToPtr) _instruction;
                    wordCount += i.Pointer.GetWordCount();
                    return wordCount;
                }
                case Op.OpBitcast:
                {
                    var i = (OpBitcast) _instruction;
                    wordCount += i.Operand.GetWordCount();
                    return wordCount;
                }
                case Op.OpSNegate:
                {
                    var i = (OpSNegate) _instruction;
                    wordCount += i.Operand.GetWordCount();
                    return wordCount;
                }
                case Op.OpFNegate:
                {
                    var i = (OpFNegate) _instruction;
                    wordCount += i.Operand.GetWordCount();
                    return wordCount;
                }
                case Op.OpIAdd:
                {
                    var i = (OpIAdd) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpFAdd:
                {
                    var i = (OpFAdd) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpISub:
                {
                    var i = (OpISub) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpFSub:
                {
                    var i = (OpFSub) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpIMul:
                {
                    var i = (OpIMul) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpFMul:
                {
                    var i = (OpFMul) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpUDiv:
                {
                    var i = (OpUDiv) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpSDiv:
                {
                    var i = (OpSDiv) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpFDiv:
                {
                    var i = (OpFDiv) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpUMod:
                {
                    var i = (OpUMod) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpSRem:
                {
                    var i = (OpSRem) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpSMod:
                {
                    var i = (OpSMod) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpFRem:
                {
                    var i = (OpFRem) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpFMod:
                {
                    var i = (OpFMod) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpLogicalEqual:
                {
                    var i = (OpLogicalEqual) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpLogicalNotEqual:
                {
                    var i = (OpLogicalNotEqual) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpLogicalOr:
                {
                    var i = (OpLogicalOr) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpLogicalAnd:
                {
                    var i = (OpLogicalAnd) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpSelect:
                {
                    var i = (OpSelect) _instruction;
                    wordCount += i.Condition.GetWordCount();
                    wordCount += i.Object1.GetWordCount();
                    wordCount += i.Object2.GetWordCount();
                    return wordCount;
                }
                case Op.OpIEqual:
                {
                    var i = (OpIEqual) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpINotEqual:
                {
                    var i = (OpINotEqual) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpUGreaterThan:
                {
                    var i = (OpUGreaterThan) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpSGreaterThan:
                {
                    var i = (OpSGreaterThan) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpUGreaterThanEqual:
                {
                    var i = (OpUGreaterThanEqual) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpSGreaterThanEqual:
                {
                    var i = (OpSGreaterThanEqual) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpULessThan:
                {
                    var i = (OpULessThan) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpSLessThan:
                {
                    var i = (OpSLessThan) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpULessThanEqual:
                {
                    var i = (OpULessThanEqual) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpSLessThanEqual:
                {
                    var i = (OpSLessThanEqual) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpShiftRightLogical:
                {
                    var i = (OpShiftRightLogical) _instruction;
                    wordCount += i.Base.GetWordCount();
                    wordCount += i.Shift.GetWordCount();
                    return wordCount;
                }
                case Op.OpShiftRightArithmetic:
                {
                    var i = (OpShiftRightArithmetic) _instruction;
                    wordCount += i.Base.GetWordCount();
                    wordCount += i.Shift.GetWordCount();
                    return wordCount;
                }
                case Op.OpShiftLeftLogical:
                {
                    var i = (OpShiftLeftLogical) _instruction;
                    wordCount += i.Base.GetWordCount();
                    wordCount += i.Shift.GetWordCount();
                    return wordCount;
                }
                case Op.OpBitwiseOr:
                {
                    var i = (OpBitwiseOr) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpBitwiseXor:
                {
                    var i = (OpBitwiseXor) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpBitwiseAnd:
                {
                    var i = (OpBitwiseAnd) _instruction;
                    wordCount += i.Operand1.GetWordCount();
                    wordCount += i.Operand2.GetWordCount();
                    return wordCount;
                }
                case Op.OpNot:
                {
                    var i = (OpNot) _instruction;
                    wordCount += i.Operand.GetWordCount();
                    return wordCount;
                }
            }
            throw new NotImplementedException(_instruction.OpCode+" nested instruction not implemented.");
        }

        public void Write(WordWriter writer)
        {
            writer.WriteWord((uint)_instruction.OpCode);
            _instruction.WriteOperands(writer);
        }

        public override string ToString()
        {
            return _instruction.ToString();
        }
    }
}