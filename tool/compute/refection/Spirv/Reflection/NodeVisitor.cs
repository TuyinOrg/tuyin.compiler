using System.Collections.Generic;
using Toe.SPIRV.Reflection.Nodes;
using Toe.SPIRV.Reflection.Operands;
using Toe.SPIRV.Reflection.Types;

namespace Toe.SPIRV.Reflection
{
    internal abstract class NodeVisitor
    {
        Queue<Node> _nodesToVisit = new Queue<Node>();
        HashSet<Node> _visitedNodes = new HashSet<Node>();

        public virtual void Visit(Node node)
        {
            ScheduleVisit(node);
            while (_nodesToVisit.Count > 0)
            {
                VisitNode(_nodesToVisit.Dequeue());
            }
        }

        protected virtual void ScheduleVisit(Node node)
        {
            if (node != null && _visitedNodes.Add(node))
                _nodesToVisit.Enqueue(node);
        }

        protected virtual void ScheduleVisit(NestedNode node)
        {
            _nodesToVisit.Enqueue(node.Node);
        }

        protected virtual void ScheduleVisit(IEnumerable<Node> nodes)
        {
            foreach (var node in nodes)
            {
                ScheduleVisit(node);
            }
        }

        protected virtual void ScheduleVisit(IEnumerable<PairNodeNode> nodes)
        {
            foreach (var node in nodes)
            {
                ScheduleVisit(node.Node);
                ScheduleVisit(node.Node2);
            }
        }

        protected virtual void ScheduleVisit(IEnumerable<PairLiteralIntegerNode> nodes)
        {
            foreach (var node in nodes)
            {
                ScheduleVisit(node.Node);
            }
        }

        protected virtual void ScheduleVisit(IEnumerable<PairNodeLiteralInteger> nodes)
        {
            foreach (var node in nodes)
            {
                ScheduleVisit(node.Node);
            }
        }

        protected virtual void VisitType(SpirvTypeBase node)
        {
        }

        private void VisitNode(Node node)
        {
            switch (node.OpCode)
            {
                case Spv.Op.OpNop: VisitNop((Nop)node); return;
                case Spv.Op.OpUndef: VisitUndef((Undef)node); return;
                case Spv.Op.OpSourceContinued: VisitSourceContinued((SourceContinued)node); return;
                case Spv.Op.OpSource: VisitSource((Source)node); return;
                case Spv.Op.OpSourceExtension: VisitSourceExtension((SourceExtension)node); return;
                case Spv.Op.OpName: VisitName((Name)node); return;
                case Spv.Op.OpMemberName: VisitMemberName((MemberName)node); return;
                case Spv.Op.OpString: VisitString((String)node); return;
                case Spv.Op.OpLine: VisitLine((Line)node); return;
                case Spv.Op.OpExtension: VisitExtension((Extension)node); return;
                case Spv.Op.OpExtInstImport: VisitExtInstImport((ExtInstImport)node); return;
                case Spv.Op.OpExtInst: VisitExtInst((ExtInst)node); return;
                case Spv.Op.OpMemoryModel: VisitMemoryModel((MemoryModel)node); return;
                case Spv.Op.OpEntryPoint: VisitEntryPoint((EntryPoint)node); return;
                case Spv.Op.OpExecutionMode: VisitExecutionMode((ExecutionMode)node); return;
                case Spv.Op.OpCapability: VisitCapability((Capability)node); return;
                case Spv.Op.OpTypeVoid: VisitTypeVoid((TypeVoid)node); return;
                case Spv.Op.OpTypeBool: VisitTypeBool((TypeBool)node); return;
                case Spv.Op.OpTypeInt: VisitTypeInt((TypeInt)node); return;
                case Spv.Op.OpTypeFloat: VisitTypeFloat((TypeFloat)node); return;
                case Spv.Op.OpTypeVector: VisitTypeVector((TypeVector)node); return;
                case Spv.Op.OpTypeMatrix: VisitTypeMatrix((TypeMatrix)node); return;
                case Spv.Op.OpTypeImage: VisitTypeImage((TypeImage)node); return;
                case Spv.Op.OpTypeSampler: VisitTypeSampler((TypeSampler)node); return;
                case Spv.Op.OpTypeSampledImage: VisitTypeSampledImage((TypeSampledImage)node); return;
                case Spv.Op.OpTypeArray: VisitTypeArray((TypeArray)node); return;
                case Spv.Op.OpTypeRuntimeArray: VisitTypeRuntimeArray((TypeRuntimeArray)node); return;
                case Spv.Op.OpTypeStruct: VisitTypeStruct((TypeStruct)node); return;
                case Spv.Op.OpTypeOpaque: VisitTypeOpaque((TypeOpaque)node); return;
                case Spv.Op.OpTypePointer: VisitTypePointer((TypePointer)node); return;
                case Spv.Op.OpTypeFunction: VisitTypeFunction((TypeFunction)node); return;
                case Spv.Op.OpTypeEvent: VisitTypeEvent((TypeEvent)node); return;
                case Spv.Op.OpTypeDeviceEvent: VisitTypeDeviceEvent((TypeDeviceEvent)node); return;
                case Spv.Op.OpTypeReserveId: VisitTypeReserveId((TypeReserveId)node); return;
                case Spv.Op.OpTypeQueue: VisitTypeQueue((TypeQueue)node); return;
                case Spv.Op.OpTypePipe: VisitTypePipe((TypePipe)node); return;
                case Spv.Op.OpTypeForwardPointer: VisitTypeForwardPointer((TypeForwardPointer)node); return;
                case Spv.Op.OpConstantTrue: VisitConstantTrue((ConstantTrue)node); return;
                case Spv.Op.OpConstantFalse: VisitConstantFalse((ConstantFalse)node); return;
                case Spv.Op.OpConstant: VisitConstant((Constant)node); return;
                case Spv.Op.OpConstantComposite: VisitConstantComposite((ConstantComposite)node); return;
                case Spv.Op.OpConstantSampler: VisitConstantSampler((ConstantSampler)node); return;
                case Spv.Op.OpConstantNull: VisitConstantNull((ConstantNull)node); return;
                case Spv.Op.OpSpecConstantTrue: VisitSpecConstantTrue((SpecConstantTrue)node); return;
                case Spv.Op.OpSpecConstantFalse: VisitSpecConstantFalse((SpecConstantFalse)node); return;
                case Spv.Op.OpSpecConstant: VisitSpecConstant((SpecConstant)node); return;
                case Spv.Op.OpSpecConstantComposite: VisitSpecConstantComposite((SpecConstantComposite)node); return;
                case Spv.Op.OpSpecConstantOp: VisitSpecConstantOp((SpecConstantOp)node); return;
                case Spv.Op.OpFunction: VisitFunction((Function)node); return;
                case Spv.Op.OpFunctionParameter: VisitFunctionParameter((FunctionParameter)node); return;
                case Spv.Op.OpFunctionEnd: VisitFunctionEnd((FunctionEnd)node); return;
                case Spv.Op.OpFunctionCall: VisitFunctionCall((FunctionCall)node); return;
                case Spv.Op.OpVariable: VisitVariable((Variable)node); return;
                case Spv.Op.OpImageTexelPointer: VisitImageTexelPointer((ImageTexelPointer)node); return;
                case Spv.Op.OpLoad: VisitLoad((Load)node); return;
                case Spv.Op.OpStore: VisitStore((Store)node); return;
                case Spv.Op.OpCopyMemory: VisitCopyMemory((CopyMemory)node); return;
                case Spv.Op.OpCopyMemorySized: VisitCopyMemorySized((CopyMemorySized)node); return;
                case Spv.Op.OpAccessChain: VisitAccessChain((AccessChain)node); return;
                case Spv.Op.OpInBoundsAccessChain: VisitInBoundsAccessChain((InBoundsAccessChain)node); return;
                case Spv.Op.OpPtrAccessChain: VisitPtrAccessChain((PtrAccessChain)node); return;
                case Spv.Op.OpArrayLength: VisitArrayLength((ArrayLength)node); return;
                case Spv.Op.OpGenericPtrMemSemantics: VisitGenericPtrMemSemantics((GenericPtrMemSemantics)node); return;
                case Spv.Op.OpInBoundsPtrAccessChain: VisitInBoundsPtrAccessChain((InBoundsPtrAccessChain)node); return;
                case Spv.Op.OpDecorate: VisitDecorate((Decorate)node); return;
                case Spv.Op.OpMemberDecorate: VisitMemberDecorate((MemberDecorate)node); return;
                case Spv.Op.OpDecorationGroup: VisitDecorationGroup((DecorationGroup)node); return;
                case Spv.Op.OpGroupDecorate: VisitGroupDecorate((GroupDecorate)node); return;
                case Spv.Op.OpGroupMemberDecorate: VisitGroupMemberDecorate((GroupMemberDecorate)node); return;
                case Spv.Op.OpVectorExtractDynamic: VisitVectorExtractDynamic((VectorExtractDynamic)node); return;
                case Spv.Op.OpVectorInsertDynamic: VisitVectorInsertDynamic((VectorInsertDynamic)node); return;
                case Spv.Op.OpVectorShuffle: VisitVectorShuffle((VectorShuffle)node); return;
                case Spv.Op.OpCompositeConstruct: VisitCompositeConstruct((CompositeConstruct)node); return;
                case Spv.Op.OpCompositeExtract: VisitCompositeExtract((CompositeExtract)node); return;
                case Spv.Op.OpCompositeInsert: VisitCompositeInsert((CompositeInsert)node); return;
                case Spv.Op.OpCopyObject: VisitCopyObject((CopyObject)node); return;
                case Spv.Op.OpTranspose: VisitTranspose((Transpose)node); return;
                case Spv.Op.OpSampledImage: VisitSampledImage((SampledImage)node); return;
                case Spv.Op.OpImageSampleImplicitLod: VisitImageSampleImplicitLod((ImageSampleImplicitLod)node); return;
                case Spv.Op.OpImageSampleExplicitLod: VisitImageSampleExplicitLod((ImageSampleExplicitLod)node); return;
                case Spv.Op.OpImageSampleDrefImplicitLod: VisitImageSampleDrefImplicitLod((ImageSampleDrefImplicitLod)node); return;
                case Spv.Op.OpImageSampleDrefExplicitLod: VisitImageSampleDrefExplicitLod((ImageSampleDrefExplicitLod)node); return;
                case Spv.Op.OpImageSampleProjImplicitLod: VisitImageSampleProjImplicitLod((ImageSampleProjImplicitLod)node); return;
                case Spv.Op.OpImageSampleProjExplicitLod: VisitImageSampleProjExplicitLod((ImageSampleProjExplicitLod)node); return;
                case Spv.Op.OpImageSampleProjDrefImplicitLod: VisitImageSampleProjDrefImplicitLod((ImageSampleProjDrefImplicitLod)node); return;
                case Spv.Op.OpImageSampleProjDrefExplicitLod: VisitImageSampleProjDrefExplicitLod((ImageSampleProjDrefExplicitLod)node); return;
                case Spv.Op.OpImageFetch: VisitImageFetch((ImageFetch)node); return;
                case Spv.Op.OpImageGather: VisitImageGather((ImageGather)node); return;
                case Spv.Op.OpImageDrefGather: VisitImageDrefGather((ImageDrefGather)node); return;
                case Spv.Op.OpImageRead: VisitImageRead((ImageRead)node); return;
                case Spv.Op.OpImageWrite: VisitImageWrite((ImageWrite)node); return;
                case Spv.Op.OpImage: VisitImage((Image)node); return;
                case Spv.Op.OpImageQueryFormat: VisitImageQueryFormat((ImageQueryFormat)node); return;
                case Spv.Op.OpImageQueryOrder: VisitImageQueryOrder((ImageQueryOrder)node); return;
                case Spv.Op.OpImageQuerySizeLod: VisitImageQuerySizeLod((ImageQuerySizeLod)node); return;
                case Spv.Op.OpImageQuerySize: VisitImageQuerySize((ImageQuerySize)node); return;
                case Spv.Op.OpImageQueryLod: VisitImageQueryLod((ImageQueryLod)node); return;
                case Spv.Op.OpImageQueryLevels: VisitImageQueryLevels((ImageQueryLevels)node); return;
                case Spv.Op.OpImageQuerySamples: VisitImageQuerySamples((ImageQuerySamples)node); return;
                case Spv.Op.OpConvertFToU: VisitConvertFToU((ConvertFToU)node); return;
                case Spv.Op.OpConvertFToS: VisitConvertFToS((ConvertFToS)node); return;
                case Spv.Op.OpConvertSToF: VisitConvertSToF((ConvertSToF)node); return;
                case Spv.Op.OpConvertUToF: VisitConvertUToF((ConvertUToF)node); return;
                case Spv.Op.OpUConvert: VisitUConvert((UConvert)node); return;
                case Spv.Op.OpSConvert: VisitSConvert((SConvert)node); return;
                case Spv.Op.OpFConvert: VisitFConvert((FConvert)node); return;
                case Spv.Op.OpQuantizeToF16: VisitQuantizeToF16((QuantizeToF16)node); return;
                case Spv.Op.OpConvertPtrToU: VisitConvertPtrToU((ConvertPtrToU)node); return;
                case Spv.Op.OpSatConvertSToU: VisitSatConvertSToU((SatConvertSToU)node); return;
                case Spv.Op.OpSatConvertUToS: VisitSatConvertUToS((SatConvertUToS)node); return;
                case Spv.Op.OpConvertUToPtr: VisitConvertUToPtr((ConvertUToPtr)node); return;
                case Spv.Op.OpPtrCastToGeneric: VisitPtrCastToGeneric((PtrCastToGeneric)node); return;
                case Spv.Op.OpGenericCastToPtr: VisitGenericCastToPtr((GenericCastToPtr)node); return;
                case Spv.Op.OpGenericCastToPtrExplicit: VisitGenericCastToPtrExplicit((GenericCastToPtrExplicit)node); return;
                case Spv.Op.OpBitcast: VisitBitcast((Bitcast)node); return;
                case Spv.Op.OpSNegate: VisitSNegate((SNegate)node); return;
                case Spv.Op.OpFNegate: VisitFNegate((FNegate)node); return;
                case Spv.Op.OpIAdd: VisitIAdd((IAdd)node); return;
                case Spv.Op.OpFAdd: VisitFAdd((FAdd)node); return;
                case Spv.Op.OpISub: VisitISub((ISub)node); return;
                case Spv.Op.OpFSub: VisitFSub((FSub)node); return;
                case Spv.Op.OpIMul: VisitIMul((IMul)node); return;
                case Spv.Op.OpFMul: VisitFMul((FMul)node); return;
                case Spv.Op.OpUDiv: VisitUDiv((UDiv)node); return;
                case Spv.Op.OpSDiv: VisitSDiv((SDiv)node); return;
                case Spv.Op.OpFDiv: VisitFDiv((FDiv)node); return;
                case Spv.Op.OpUMod: VisitUMod((UMod)node); return;
                case Spv.Op.OpSRem: VisitSRem((SRem)node); return;
                case Spv.Op.OpSMod: VisitSMod((SMod)node); return;
                case Spv.Op.OpFRem: VisitFRem((FRem)node); return;
                case Spv.Op.OpFMod: VisitFMod((FMod)node); return;
                case Spv.Op.OpVectorTimesScalar: VisitVectorTimesScalar((VectorTimesScalar)node); return;
                case Spv.Op.OpMatrixTimesScalar: VisitMatrixTimesScalar((MatrixTimesScalar)node); return;
                case Spv.Op.OpVectorTimesMatrix: VisitVectorTimesMatrix((VectorTimesMatrix)node); return;
                case Spv.Op.OpMatrixTimesVector: VisitMatrixTimesVector((MatrixTimesVector)node); return;
                case Spv.Op.OpMatrixTimesMatrix: VisitMatrixTimesMatrix((MatrixTimesMatrix)node); return;
                case Spv.Op.OpOuterProduct: VisitOuterProduct((OuterProduct)node); return;
                case Spv.Op.OpDot: VisitDot((Dot)node); return;
                case Spv.Op.OpIAddCarry: VisitIAddCarry((IAddCarry)node); return;
                case Spv.Op.OpISubBorrow: VisitISubBorrow((ISubBorrow)node); return;
                case Spv.Op.OpUMulExtended: VisitUMulExtended((UMulExtended)node); return;
                case Spv.Op.OpSMulExtended: VisitSMulExtended((SMulExtended)node); return;
                case Spv.Op.OpAny: VisitAny((Any)node); return;
                case Spv.Op.OpAll: VisitAll((All)node); return;
                case Spv.Op.OpIsNan: VisitIsNan((IsNan)node); return;
                case Spv.Op.OpIsInf: VisitIsInf((IsInf)node); return;
                case Spv.Op.OpIsFinite: VisitIsFinite((IsFinite)node); return;
                case Spv.Op.OpIsNormal: VisitIsNormal((IsNormal)node); return;
                case Spv.Op.OpSignBitSet: VisitSignBitSet((SignBitSet)node); return;
                case Spv.Op.OpLessOrGreater: VisitLessOrGreater((LessOrGreater)node); return;
                case Spv.Op.OpOrdered: VisitOrdered((Ordered)node); return;
                case Spv.Op.OpUnordered: VisitUnordered((Unordered)node); return;
                case Spv.Op.OpLogicalEqual: VisitLogicalEqual((LogicalEqual)node); return;
                case Spv.Op.OpLogicalNotEqual: VisitLogicalNotEqual((LogicalNotEqual)node); return;
                case Spv.Op.OpLogicalOr: VisitLogicalOr((LogicalOr)node); return;
                case Spv.Op.OpLogicalAnd: VisitLogicalAnd((LogicalAnd)node); return;
                case Spv.Op.OpLogicalNot: VisitLogicalNot((LogicalNot)node); return;
                case Spv.Op.OpSelect: VisitSelect((Select)node); return;
                case Spv.Op.OpIEqual: VisitIEqual((IEqual)node); return;
                case Spv.Op.OpINotEqual: VisitINotEqual((INotEqual)node); return;
                case Spv.Op.OpUGreaterThan: VisitUGreaterThan((UGreaterThan)node); return;
                case Spv.Op.OpSGreaterThan: VisitSGreaterThan((SGreaterThan)node); return;
                case Spv.Op.OpUGreaterThanEqual: VisitUGreaterThanEqual((UGreaterThanEqual)node); return;
                case Spv.Op.OpSGreaterThanEqual: VisitSGreaterThanEqual((SGreaterThanEqual)node); return;
                case Spv.Op.OpULessThan: VisitULessThan((ULessThan)node); return;
                case Spv.Op.OpSLessThan: VisitSLessThan((SLessThan)node); return;
                case Spv.Op.OpULessThanEqual: VisitULessThanEqual((ULessThanEqual)node); return;
                case Spv.Op.OpSLessThanEqual: VisitSLessThanEqual((SLessThanEqual)node); return;
                case Spv.Op.OpFOrdEqual: VisitFOrdEqual((FOrdEqual)node); return;
                case Spv.Op.OpFUnordEqual: VisitFUnordEqual((FUnordEqual)node); return;
                case Spv.Op.OpFOrdNotEqual: VisitFOrdNotEqual((FOrdNotEqual)node); return;
                case Spv.Op.OpFUnordNotEqual: VisitFUnordNotEqual((FUnordNotEqual)node); return;
                case Spv.Op.OpFOrdLessThan: VisitFOrdLessThan((FOrdLessThan)node); return;
                case Spv.Op.OpFUnordLessThan: VisitFUnordLessThan((FUnordLessThan)node); return;
                case Spv.Op.OpFOrdGreaterThan: VisitFOrdGreaterThan((FOrdGreaterThan)node); return;
                case Spv.Op.OpFUnordGreaterThan: VisitFUnordGreaterThan((FUnordGreaterThan)node); return;
                case Spv.Op.OpFOrdLessThanEqual: VisitFOrdLessThanEqual((FOrdLessThanEqual)node); return;
                case Spv.Op.OpFUnordLessThanEqual: VisitFUnordLessThanEqual((FUnordLessThanEqual)node); return;
                case Spv.Op.OpFOrdGreaterThanEqual: VisitFOrdGreaterThanEqual((FOrdGreaterThanEqual)node); return;
                case Spv.Op.OpFUnordGreaterThanEqual: VisitFUnordGreaterThanEqual((FUnordGreaterThanEqual)node); return;
                case Spv.Op.OpShiftRightLogical: VisitShiftRightLogical((ShiftRightLogical)node); return;
                case Spv.Op.OpShiftRightArithmetic: VisitShiftRightArithmetic((ShiftRightArithmetic)node); return;
                case Spv.Op.OpShiftLeftLogical: VisitShiftLeftLogical((ShiftLeftLogical)node); return;
                case Spv.Op.OpBitwiseOr: VisitBitwiseOr((BitwiseOr)node); return;
                case Spv.Op.OpBitwiseXor: VisitBitwiseXor((BitwiseXor)node); return;
                case Spv.Op.OpBitwiseAnd: VisitBitwiseAnd((BitwiseAnd)node); return;
                case Spv.Op.OpNot: VisitNot((Not)node); return;
                case Spv.Op.OpBitFieldInsert: VisitBitFieldInsert((BitFieldInsert)node); return;
                case Spv.Op.OpBitFieldSExtract: VisitBitFieldSExtract((BitFieldSExtract)node); return;
                case Spv.Op.OpBitFieldUExtract: VisitBitFieldUExtract((BitFieldUExtract)node); return;
                case Spv.Op.OpBitReverse: VisitBitReverse((BitReverse)node); return;
                case Spv.Op.OpBitCount: VisitBitCount((BitCount)node); return;
                case Spv.Op.OpDPdx: VisitDPdx((DPdx)node); return;
                case Spv.Op.OpDPdy: VisitDPdy((DPdy)node); return;
                case Spv.Op.OpFwidth: VisitFwidth((Fwidth)node); return;
                case Spv.Op.OpDPdxFine: VisitDPdxFine((DPdxFine)node); return;
                case Spv.Op.OpDPdyFine: VisitDPdyFine((DPdyFine)node); return;
                case Spv.Op.OpFwidthFine: VisitFwidthFine((FwidthFine)node); return;
                case Spv.Op.OpDPdxCoarse: VisitDPdxCoarse((DPdxCoarse)node); return;
                case Spv.Op.OpDPdyCoarse: VisitDPdyCoarse((DPdyCoarse)node); return;
                case Spv.Op.OpFwidthCoarse: VisitFwidthCoarse((FwidthCoarse)node); return;
                case Spv.Op.OpEmitVertex: VisitEmitVertex((EmitVertex)node); return;
                case Spv.Op.OpEndPrimitive: VisitEndPrimitive((EndPrimitive)node); return;
                case Spv.Op.OpEmitStreamVertex: VisitEmitStreamVertex((EmitStreamVertex)node); return;
                case Spv.Op.OpEndStreamPrimitive: VisitEndStreamPrimitive((EndStreamPrimitive)node); return;
                case Spv.Op.OpControlBarrier: VisitControlBarrier((ControlBarrier)node); return;
                case Spv.Op.OpMemoryBarrier: VisitMemoryBarrier((MemoryBarrier)node); return;
                case Spv.Op.OpAtomicLoad: VisitAtomicLoad((AtomicLoad)node); return;
                case Spv.Op.OpAtomicStore: VisitAtomicStore((AtomicStore)node); return;
                case Spv.Op.OpAtomicExchange: VisitAtomicExchange((AtomicExchange)node); return;
                case Spv.Op.OpAtomicCompareExchange: VisitAtomicCompareExchange((AtomicCompareExchange)node); return;
                case Spv.Op.OpAtomicCompareExchangeWeak: VisitAtomicCompareExchangeWeak((AtomicCompareExchangeWeak)node); return;
                case Spv.Op.OpAtomicIIncrement: VisitAtomicIIncrement((AtomicIIncrement)node); return;
                case Spv.Op.OpAtomicIDecrement: VisitAtomicIDecrement((AtomicIDecrement)node); return;
                case Spv.Op.OpAtomicIAdd: VisitAtomicIAdd((AtomicIAdd)node); return;
                case Spv.Op.OpAtomicISub: VisitAtomicISub((AtomicISub)node); return;
                case Spv.Op.OpAtomicSMin: VisitAtomicSMin((AtomicSMin)node); return;
                case Spv.Op.OpAtomicUMin: VisitAtomicUMin((AtomicUMin)node); return;
                case Spv.Op.OpAtomicSMax: VisitAtomicSMax((AtomicSMax)node); return;
                case Spv.Op.OpAtomicUMax: VisitAtomicUMax((AtomicUMax)node); return;
                case Spv.Op.OpAtomicAnd: VisitAtomicAnd((AtomicAnd)node); return;
                case Spv.Op.OpAtomicOr: VisitAtomicOr((AtomicOr)node); return;
                case Spv.Op.OpAtomicXor: VisitAtomicXor((AtomicXor)node); return;
                case Spv.Op.OpPhi: VisitPhi((Phi)node); return;
                case Spv.Op.OpLoopMerge: VisitLoopMerge((LoopMerge)node); return;
                case Spv.Op.OpSelectionMerge: VisitSelectionMerge((SelectionMerge)node); return;
                case Spv.Op.OpLabel: VisitLabel((Label)node); return;
                case Spv.Op.OpBranch: VisitBranch((Branch)node); return;
                case Spv.Op.OpBranchConditional: VisitBranchConditional((BranchConditional)node); return;
                case Spv.Op.OpSwitch: VisitSwitch((Switch)node); return;
                case Spv.Op.OpKill: VisitKill((Kill)node); return;
                case Spv.Op.OpReturn: VisitReturn((Return)node); return;
                case Spv.Op.OpReturnValue: VisitReturnValue((ReturnValue)node); return;
                case Spv.Op.OpUnreachable: VisitUnreachable((Unreachable)node); return;
                case Spv.Op.OpLifetimeStart: VisitLifetimeStart((LifetimeStart)node); return;
                case Spv.Op.OpLifetimeStop: VisitLifetimeStop((LifetimeStop)node); return;
                case Spv.Op.OpGroupAsyncCopy: VisitGroupAsyncCopy((GroupAsyncCopy)node); return;
                case Spv.Op.OpGroupWaitEvents: VisitGroupWaitEvents((GroupWaitEvents)node); return;
                case Spv.Op.OpGroupAll: VisitGroupAll((GroupAll)node); return;
                case Spv.Op.OpGroupAny: VisitGroupAny((GroupAny)node); return;
                case Spv.Op.OpGroupBroadcast: VisitGroupBroadcast((GroupBroadcast)node); return;
                case Spv.Op.OpGroupIAdd: VisitGroupIAdd((GroupIAdd)node); return;
                case Spv.Op.OpGroupFAdd: VisitGroupFAdd((GroupFAdd)node); return;
                case Spv.Op.OpGroupFMin: VisitGroupFMin((GroupFMin)node); return;
                case Spv.Op.OpGroupUMin: VisitGroupUMin((GroupUMin)node); return;
                case Spv.Op.OpGroupSMin: VisitGroupSMin((GroupSMin)node); return;
                case Spv.Op.OpGroupFMax: VisitGroupFMax((GroupFMax)node); return;
                case Spv.Op.OpGroupUMax: VisitGroupUMax((GroupUMax)node); return;
                case Spv.Op.OpGroupSMax: VisitGroupSMax((GroupSMax)node); return;
                case Spv.Op.OpReadPipe: VisitReadPipe((ReadPipe)node); return;
                case Spv.Op.OpWritePipe: VisitWritePipe((WritePipe)node); return;
                case Spv.Op.OpReservedReadPipe: VisitReservedReadPipe((ReservedReadPipe)node); return;
                case Spv.Op.OpReservedWritePipe: VisitReservedWritePipe((ReservedWritePipe)node); return;
                case Spv.Op.OpReserveReadPipePackets: VisitReserveReadPipePackets((ReserveReadPipePackets)node); return;
                case Spv.Op.OpReserveWritePipePackets: VisitReserveWritePipePackets((ReserveWritePipePackets)node); return;
                case Spv.Op.OpCommitReadPipe: VisitCommitReadPipe((CommitReadPipe)node); return;
                case Spv.Op.OpCommitWritePipe: VisitCommitWritePipe((CommitWritePipe)node); return;
                case Spv.Op.OpIsValidReserveId: VisitIsValidReserveId((IsValidReserveId)node); return;
                case Spv.Op.OpGetNumPipePackets: VisitGetNumPipePackets((GetNumPipePackets)node); return;
                case Spv.Op.OpGetMaxPipePackets: VisitGetMaxPipePackets((GetMaxPipePackets)node); return;
                case Spv.Op.OpGroupReserveReadPipePackets: VisitGroupReserveReadPipePackets((GroupReserveReadPipePackets)node); return;
                case Spv.Op.OpGroupReserveWritePipePackets: VisitGroupReserveWritePipePackets((GroupReserveWritePipePackets)node); return;
                case Spv.Op.OpGroupCommitReadPipe: VisitGroupCommitReadPipe((GroupCommitReadPipe)node); return;
                case Spv.Op.OpGroupCommitWritePipe: VisitGroupCommitWritePipe((GroupCommitWritePipe)node); return;
                case Spv.Op.OpEnqueueMarker: VisitEnqueueMarker((EnqueueMarker)node); return;
                case Spv.Op.OpEnqueueKernel: VisitEnqueueKernel((EnqueueKernel)node); return;
                case Spv.Op.OpGetKernelNDrangeSubGroupCount: VisitGetKernelNDrangeSubGroupCount((GetKernelNDrangeSubGroupCount)node); return;
                case Spv.Op.OpGetKernelNDrangeMaxSubGroupSize: VisitGetKernelNDrangeMaxSubGroupSize((GetKernelNDrangeMaxSubGroupSize)node); return;
                case Spv.Op.OpGetKernelWorkGroupSize: VisitGetKernelWorkGroupSize((GetKernelWorkGroupSize)node); return;
                case Spv.Op.OpGetKernelPreferredWorkGroupSizeMultiple: VisitGetKernelPreferredWorkGroupSizeMultiple((GetKernelPreferredWorkGroupSizeMultiple)node); return;
                case Spv.Op.OpRetainEvent: VisitRetainEvent((RetainEvent)node); return;
                case Spv.Op.OpReleaseEvent: VisitReleaseEvent((ReleaseEvent)node); return;
                case Spv.Op.OpCreateUserEvent: VisitCreateUserEvent((CreateUserEvent)node); return;
                case Spv.Op.OpIsValidEvent: VisitIsValidEvent((IsValidEvent)node); return;
                case Spv.Op.OpSetUserEventStatus: VisitSetUserEventStatus((SetUserEventStatus)node); return;
                case Spv.Op.OpCaptureEventProfilingInfo: VisitCaptureEventProfilingInfo((CaptureEventProfilingInfo)node); return;
                case Spv.Op.OpGetDefaultQueue: VisitGetDefaultQueue((GetDefaultQueue)node); return;
                case Spv.Op.OpBuildNDRange: VisitBuildNDRange((BuildNDRange)node); return;
                case Spv.Op.OpImageSparseSampleImplicitLod: VisitImageSparseSampleImplicitLod((ImageSparseSampleImplicitLod)node); return;
                case Spv.Op.OpImageSparseSampleExplicitLod: VisitImageSparseSampleExplicitLod((ImageSparseSampleExplicitLod)node); return;
                case Spv.Op.OpImageSparseSampleDrefImplicitLod: VisitImageSparseSampleDrefImplicitLod((ImageSparseSampleDrefImplicitLod)node); return;
                case Spv.Op.OpImageSparseSampleDrefExplicitLod: VisitImageSparseSampleDrefExplicitLod((ImageSparseSampleDrefExplicitLod)node); return;
                case Spv.Op.OpImageSparseSampleProjImplicitLod: VisitImageSparseSampleProjImplicitLod((ImageSparseSampleProjImplicitLod)node); return;
                case Spv.Op.OpImageSparseSampleProjExplicitLod: VisitImageSparseSampleProjExplicitLod((ImageSparseSampleProjExplicitLod)node); return;
                case Spv.Op.OpImageSparseSampleProjDrefImplicitLod: VisitImageSparseSampleProjDrefImplicitLod((ImageSparseSampleProjDrefImplicitLod)node); return;
                case Spv.Op.OpImageSparseSampleProjDrefExplicitLod: VisitImageSparseSampleProjDrefExplicitLod((ImageSparseSampleProjDrefExplicitLod)node); return;
                case Spv.Op.OpImageSparseFetch: VisitImageSparseFetch((ImageSparseFetch)node); return;
                case Spv.Op.OpImageSparseGather: VisitImageSparseGather((ImageSparseGather)node); return;
                case Spv.Op.OpImageSparseDrefGather: VisitImageSparseDrefGather((ImageSparseDrefGather)node); return;
                case Spv.Op.OpImageSparseTexelsResident: VisitImageSparseTexelsResident((ImageSparseTexelsResident)node); return;
                case Spv.Op.OpNoLine: VisitNoLine((NoLine)node); return;
                case Spv.Op.OpAtomicFlagTestAndSet: VisitAtomicFlagTestAndSet((AtomicFlagTestAndSet)node); return;
                case Spv.Op.OpAtomicFlagClear: VisitAtomicFlagClear((AtomicFlagClear)node); return;
                case Spv.Op.OpImageSparseRead: VisitImageSparseRead((ImageSparseRead)node); return;
                case Spv.Op.OpSizeOf: VisitSizeOf((SizeOf)node); return;
                case Spv.Op.OpTypePipeStorage: VisitTypePipeStorage((TypePipeStorage)node); return;
                case Spv.Op.OpConstantPipeStorage: VisitConstantPipeStorage((ConstantPipeStorage)node); return;
                case Spv.Op.OpCreatePipeFromPipeStorage: VisitCreatePipeFromPipeStorage((CreatePipeFromPipeStorage)node); return;
                case Spv.Op.OpGetKernelLocalSizeForSubgroupCount: VisitGetKernelLocalSizeForSubgroupCount((GetKernelLocalSizeForSubgroupCount)node); return;
                case Spv.Op.OpGetKernelMaxNumSubgroups: VisitGetKernelMaxNumSubgroups((GetKernelMaxNumSubgroups)node); return;
                case Spv.Op.OpTypeNamedBarrier: VisitTypeNamedBarrier((TypeNamedBarrier)node); return;
                case Spv.Op.OpNamedBarrierInitialize: VisitNamedBarrierInitialize((NamedBarrierInitialize)node); return;
                case Spv.Op.OpMemoryNamedBarrier: VisitMemoryNamedBarrier((MemoryNamedBarrier)node); return;
                case Spv.Op.OpModuleProcessed: VisitModuleProcessed((ModuleProcessed)node); return;
                case Spv.Op.OpExecutionModeId: VisitExecutionModeId((ExecutionModeId)node); return;
                case Spv.Op.OpDecorateId: VisitDecorateId((DecorateId)node); return;
                case Spv.Op.OpGroupNonUniformElect: VisitGroupNonUniformElect((GroupNonUniformElect)node); return;
                case Spv.Op.OpGroupNonUniformAll: VisitGroupNonUniformAll((GroupNonUniformAll)node); return;
                case Spv.Op.OpGroupNonUniformAny: VisitGroupNonUniformAny((GroupNonUniformAny)node); return;
                case Spv.Op.OpGroupNonUniformAllEqual: VisitGroupNonUniformAllEqual((GroupNonUniformAllEqual)node); return;
                case Spv.Op.OpGroupNonUniformBroadcast: VisitGroupNonUniformBroadcast((GroupNonUniformBroadcast)node); return;
                case Spv.Op.OpGroupNonUniformBroadcastFirst: VisitGroupNonUniformBroadcastFirst((GroupNonUniformBroadcastFirst)node); return;
                case Spv.Op.OpGroupNonUniformBallot: VisitGroupNonUniformBallot((GroupNonUniformBallot)node); return;
                case Spv.Op.OpGroupNonUniformInverseBallot: VisitGroupNonUniformInverseBallot((GroupNonUniformInverseBallot)node); return;
                case Spv.Op.OpGroupNonUniformBallotBitExtract: VisitGroupNonUniformBallotBitExtract((GroupNonUniformBallotBitExtract)node); return;
                case Spv.Op.OpGroupNonUniformBallotBitCount: VisitGroupNonUniformBallotBitCount((GroupNonUniformBallotBitCount)node); return;
                case Spv.Op.OpGroupNonUniformBallotFindLSB: VisitGroupNonUniformBallotFindLSB((GroupNonUniformBallotFindLSB)node); return;
                case Spv.Op.OpGroupNonUniformBallotFindMSB: VisitGroupNonUniformBallotFindMSB((GroupNonUniformBallotFindMSB)node); return;
                case Spv.Op.OpGroupNonUniformShuffle: VisitGroupNonUniformShuffle((GroupNonUniformShuffle)node); return;
                case Spv.Op.OpGroupNonUniformShuffleXor: VisitGroupNonUniformShuffleXor((GroupNonUniformShuffleXor)node); return;
                case Spv.Op.OpGroupNonUniformShuffleUp: VisitGroupNonUniformShuffleUp((GroupNonUniformShuffleUp)node); return;
                case Spv.Op.OpGroupNonUniformShuffleDown: VisitGroupNonUniformShuffleDown((GroupNonUniformShuffleDown)node); return;
                case Spv.Op.OpGroupNonUniformIAdd: VisitGroupNonUniformIAdd((GroupNonUniformIAdd)node); return;
                case Spv.Op.OpGroupNonUniformFAdd: VisitGroupNonUniformFAdd((GroupNonUniformFAdd)node); return;
                case Spv.Op.OpGroupNonUniformIMul: VisitGroupNonUniformIMul((GroupNonUniformIMul)node); return;
                case Spv.Op.OpGroupNonUniformFMul: VisitGroupNonUniformFMul((GroupNonUniformFMul)node); return;
                case Spv.Op.OpGroupNonUniformSMin: VisitGroupNonUniformSMin((GroupNonUniformSMin)node); return;
                case Spv.Op.OpGroupNonUniformUMin: VisitGroupNonUniformUMin((GroupNonUniformUMin)node); return;
                case Spv.Op.OpGroupNonUniformFMin: VisitGroupNonUniformFMin((GroupNonUniformFMin)node); return;
                case Spv.Op.OpGroupNonUniformSMax: VisitGroupNonUniformSMax((GroupNonUniformSMax)node); return;
                case Spv.Op.OpGroupNonUniformUMax: VisitGroupNonUniformUMax((GroupNonUniformUMax)node); return;
                case Spv.Op.OpGroupNonUniformFMax: VisitGroupNonUniformFMax((GroupNonUniformFMax)node); return;
                case Spv.Op.OpGroupNonUniformBitwiseAnd: VisitGroupNonUniformBitwiseAnd((GroupNonUniformBitwiseAnd)node); return;
                case Spv.Op.OpGroupNonUniformBitwiseOr: VisitGroupNonUniformBitwiseOr((GroupNonUniformBitwiseOr)node); return;
                case Spv.Op.OpGroupNonUniformBitwiseXor: VisitGroupNonUniformBitwiseXor((GroupNonUniformBitwiseXor)node); return;
                case Spv.Op.OpGroupNonUniformLogicalAnd: VisitGroupNonUniformLogicalAnd((GroupNonUniformLogicalAnd)node); return;
                case Spv.Op.OpGroupNonUniformLogicalOr: VisitGroupNonUniformLogicalOr((GroupNonUniformLogicalOr)node); return;
                case Spv.Op.OpGroupNonUniformLogicalXor: VisitGroupNonUniformLogicalXor((GroupNonUniformLogicalXor)node); return;
                case Spv.Op.OpGroupNonUniformQuadBroadcast: VisitGroupNonUniformQuadBroadcast((GroupNonUniformQuadBroadcast)node); return;
                case Spv.Op.OpGroupNonUniformQuadSwap: VisitGroupNonUniformQuadSwap((GroupNonUniformQuadSwap)node); return;
                case Spv.Op.OpCopyLogical: VisitCopyLogical((CopyLogical)node); return;
                case Spv.Op.OpPtrEqual: VisitPtrEqual((PtrEqual)node); return;
                case Spv.Op.OpPtrNotEqual: VisitPtrNotEqual((PtrNotEqual)node); return;
                case Spv.Op.OpPtrDiff: VisitPtrDiff((PtrDiff)node); return;
                case Spv.Op.OpSubgroupBallotKHR: VisitSubgroupBallotKHR((SubgroupBallotKHR)node); return;
                case Spv.Op.OpSubgroupFirstInvocationKHR: VisitSubgroupFirstInvocationKHR((SubgroupFirstInvocationKHR)node); return;
                case Spv.Op.OpSubgroupAllKHR: VisitSubgroupAllKHR((SubgroupAllKHR)node); return;
                case Spv.Op.OpSubgroupAnyKHR: VisitSubgroupAnyKHR((SubgroupAnyKHR)node); return;
                case Spv.Op.OpSubgroupAllEqualKHR: VisitSubgroupAllEqualKHR((SubgroupAllEqualKHR)node); return;
                case Spv.Op.OpSubgroupReadInvocationKHR: VisitSubgroupReadInvocationKHR((SubgroupReadInvocationKHR)node); return;
                case Spv.Op.OpGroupIAddNonUniformAMD: VisitGroupIAddNonUniformAMD((GroupIAddNonUniformAMD)node); return;
                case Spv.Op.OpGroupFAddNonUniformAMD: VisitGroupFAddNonUniformAMD((GroupFAddNonUniformAMD)node); return;
                case Spv.Op.OpGroupFMinNonUniformAMD: VisitGroupFMinNonUniformAMD((GroupFMinNonUniformAMD)node); return;
                case Spv.Op.OpGroupUMinNonUniformAMD: VisitGroupUMinNonUniformAMD((GroupUMinNonUniformAMD)node); return;
                case Spv.Op.OpGroupSMinNonUniformAMD: VisitGroupSMinNonUniformAMD((GroupSMinNonUniformAMD)node); return;
                case Spv.Op.OpGroupFMaxNonUniformAMD: VisitGroupFMaxNonUniformAMD((GroupFMaxNonUniformAMD)node); return;
                case Spv.Op.OpGroupUMaxNonUniformAMD: VisitGroupUMaxNonUniformAMD((GroupUMaxNonUniformAMD)node); return;
                case Spv.Op.OpGroupSMaxNonUniformAMD: VisitGroupSMaxNonUniformAMD((GroupSMaxNonUniformAMD)node); return;
                case Spv.Op.OpFragmentMaskFetchAMD: VisitFragmentMaskFetchAMD((FragmentMaskFetchAMD)node); return;
                case Spv.Op.OpFragmentFetchAMD: VisitFragmentFetchAMD((FragmentFetchAMD)node); return;
                case Spv.Op.OpReadClockKHR: VisitReadClockKHR((ReadClockKHR)node); return;
                case Spv.Op.OpImageSampleFootprintNV: VisitImageSampleFootprintNV((ImageSampleFootprintNV)node); return;
                case Spv.Op.OpGroupNonUniformPartitionNV: VisitGroupNonUniformPartitionNV((GroupNonUniformPartitionNV)node); return;
                case Spv.Op.OpWritePackedPrimitiveIndices4x8NV: VisitWritePackedPrimitiveIndices4x8NV((WritePackedPrimitiveIndices4x8NV)node); return;
                case Spv.Op.OpReportIntersectionNV: VisitReportIntersectionNV((ReportIntersectionNV)node); return;
                case Spv.Op.OpIgnoreIntersectionNV: VisitIgnoreIntersectionNV((IgnoreIntersectionNV)node); return;
                case Spv.Op.OpTerminateRayNV: VisitTerminateRayNV((TerminateRayNV)node); return;
                case Spv.Op.OpTraceNV: VisitTraceNV((TraceNV)node); return;
                case Spv.Op.OpTypeAccelerationStructureNV: VisitTypeAccelerationStructureNV((TypeAccelerationStructureNV)node); return;
                case Spv.Op.OpTypeRayQueryProvisionalKHR: VisitTypeRayQueryProvisionalKHR((TypeRayQueryProvisionalKHR)node); return;
                case Spv.Op.OpRayQueryInitializeKHR: VisitRayQueryInitializeKHR((RayQueryInitializeKHR)node); return;
                case Spv.Op.OpRayQueryTerminateKHR: VisitRayQueryTerminateKHR((RayQueryTerminateKHR)node); return;
                case Spv.Op.OpRayQueryGenerateIntersectionKHR: VisitRayQueryGenerateIntersectionKHR((RayQueryGenerateIntersectionKHR)node); return;
                case Spv.Op.OpRayQueryConfirmIntersectionKHR: VisitRayQueryConfirmIntersectionKHR((RayQueryConfirmIntersectionKHR)node); return;
                case Spv.Op.OpRayQueryProceedKHR: VisitRayQueryProceedKHR((RayQueryProceedKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionTypeKHR: VisitRayQueryGetIntersectionTypeKHR((RayQueryGetIntersectionTypeKHR)node); return;
                case Spv.Op.OpRayQueryGetRayTMinKHR: VisitRayQueryGetRayTMinKHR((RayQueryGetRayTMinKHR)node); return;
                case Spv.Op.OpRayQueryGetRayFlagsKHR: VisitRayQueryGetRayFlagsKHR((RayQueryGetRayFlagsKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionTKHR: VisitRayQueryGetIntersectionTKHR((RayQueryGetIntersectionTKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionInstanceCustomIndexKHR: VisitRayQueryGetIntersectionInstanceCustomIndexKHR((RayQueryGetIntersectionInstanceCustomIndexKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionInstanceIdKHR: VisitRayQueryGetIntersectionInstanceIdKHR((RayQueryGetIntersectionInstanceIdKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR: VisitRayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR((RayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionGeometryIndexKHR: VisitRayQueryGetIntersectionGeometryIndexKHR((RayQueryGetIntersectionGeometryIndexKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionPrimitiveIndexKHR: VisitRayQueryGetIntersectionPrimitiveIndexKHR((RayQueryGetIntersectionPrimitiveIndexKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionBarycentricsKHR: VisitRayQueryGetIntersectionBarycentricsKHR((RayQueryGetIntersectionBarycentricsKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionFrontFaceKHR: VisitRayQueryGetIntersectionFrontFaceKHR((RayQueryGetIntersectionFrontFaceKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionCandidateAABBOpaqueKHR: VisitRayQueryGetIntersectionCandidateAABBOpaqueKHR((RayQueryGetIntersectionCandidateAABBOpaqueKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionObjectRayDirectionKHR: VisitRayQueryGetIntersectionObjectRayDirectionKHR((RayQueryGetIntersectionObjectRayDirectionKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionObjectRayOriginKHR: VisitRayQueryGetIntersectionObjectRayOriginKHR((RayQueryGetIntersectionObjectRayOriginKHR)node); return;
                case Spv.Op.OpRayQueryGetWorldRayDirectionKHR: VisitRayQueryGetWorldRayDirectionKHR((RayQueryGetWorldRayDirectionKHR)node); return;
                case Spv.Op.OpRayQueryGetWorldRayOriginKHR: VisitRayQueryGetWorldRayOriginKHR((RayQueryGetWorldRayOriginKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionObjectToWorldKHR: VisitRayQueryGetIntersectionObjectToWorldKHR((RayQueryGetIntersectionObjectToWorldKHR)node); return;
                case Spv.Op.OpRayQueryGetIntersectionWorldToObjectKHR: VisitRayQueryGetIntersectionWorldToObjectKHR((RayQueryGetIntersectionWorldToObjectKHR)node); return;
                case Spv.Op.OpExecuteCallableNV: VisitExecuteCallableNV((ExecuteCallableNV)node); return;
                case Spv.Op.OpTypeCooperativeMatrixNV: VisitTypeCooperativeMatrixNV((TypeCooperativeMatrixNV)node); return;
                case Spv.Op.OpCooperativeMatrixLoadNV: VisitCooperativeMatrixLoadNV((CooperativeMatrixLoadNV)node); return;
                case Spv.Op.OpCooperativeMatrixStoreNV: VisitCooperativeMatrixStoreNV((CooperativeMatrixStoreNV)node); return;
                case Spv.Op.OpCooperativeMatrixMulAddNV: VisitCooperativeMatrixMulAddNV((CooperativeMatrixMulAddNV)node); return;
                case Spv.Op.OpCooperativeMatrixLengthNV: VisitCooperativeMatrixLengthNV((CooperativeMatrixLengthNV)node); return;
                case Spv.Op.OpBeginInvocationInterlockEXT: VisitBeginInvocationInterlockEXT((BeginInvocationInterlockEXT)node); return;
                case Spv.Op.OpEndInvocationInterlockEXT: VisitEndInvocationInterlockEXT((EndInvocationInterlockEXT)node); return;
                case Spv.Op.OpDemoteToHelperInvocationEXT: VisitDemoteToHelperInvocationEXT((DemoteToHelperInvocationEXT)node); return;
                case Spv.Op.OpIsHelperInvocationEXT: VisitIsHelperInvocationEXT((IsHelperInvocationEXT)node); return;
                case Spv.Op.OpSubgroupShuffleINTEL: VisitSubgroupShuffleINTEL((SubgroupShuffleINTEL)node); return;
                case Spv.Op.OpSubgroupShuffleDownINTEL: VisitSubgroupShuffleDownINTEL((SubgroupShuffleDownINTEL)node); return;
                case Spv.Op.OpSubgroupShuffleUpINTEL: VisitSubgroupShuffleUpINTEL((SubgroupShuffleUpINTEL)node); return;
                case Spv.Op.OpSubgroupShuffleXorINTEL: VisitSubgroupShuffleXorINTEL((SubgroupShuffleXorINTEL)node); return;
                case Spv.Op.OpSubgroupBlockReadINTEL: VisitSubgroupBlockReadINTEL((SubgroupBlockReadINTEL)node); return;
                case Spv.Op.OpSubgroupBlockWriteINTEL: VisitSubgroupBlockWriteINTEL((SubgroupBlockWriteINTEL)node); return;
                case Spv.Op.OpSubgroupImageBlockReadINTEL: VisitSubgroupImageBlockReadINTEL((SubgroupImageBlockReadINTEL)node); return;
                case Spv.Op.OpSubgroupImageBlockWriteINTEL: VisitSubgroupImageBlockWriteINTEL((SubgroupImageBlockWriteINTEL)node); return;
                case Spv.Op.OpSubgroupImageMediaBlockReadINTEL: VisitSubgroupImageMediaBlockReadINTEL((SubgroupImageMediaBlockReadINTEL)node); return;
                case Spv.Op.OpSubgroupImageMediaBlockWriteINTEL: VisitSubgroupImageMediaBlockWriteINTEL((SubgroupImageMediaBlockWriteINTEL)node); return;
                case Spv.Op.OpUCountLeadingZerosINTEL: VisitUCountLeadingZerosINTEL((UCountLeadingZerosINTEL)node); return;
                case Spv.Op.OpUCountTrailingZerosINTEL: VisitUCountTrailingZerosINTEL((UCountTrailingZerosINTEL)node); return;
                case Spv.Op.OpAbsISubINTEL: VisitAbsISubINTEL((AbsISubINTEL)node); return;
                case Spv.Op.OpAbsUSubINTEL: VisitAbsUSubINTEL((AbsUSubINTEL)node); return;
                case Spv.Op.OpIAddSatINTEL: VisitIAddSatINTEL((IAddSatINTEL)node); return;
                case Spv.Op.OpUAddSatINTEL: VisitUAddSatINTEL((UAddSatINTEL)node); return;
                case Spv.Op.OpIAverageINTEL: VisitIAverageINTEL((IAverageINTEL)node); return;
                case Spv.Op.OpUAverageINTEL: VisitUAverageINTEL((UAverageINTEL)node); return;
                case Spv.Op.OpIAverageRoundedINTEL: VisitIAverageRoundedINTEL((IAverageRoundedINTEL)node); return;
                case Spv.Op.OpUAverageRoundedINTEL: VisitUAverageRoundedINTEL((UAverageRoundedINTEL)node); return;
                case Spv.Op.OpISubSatINTEL: VisitISubSatINTEL((ISubSatINTEL)node); return;
                case Spv.Op.OpUSubSatINTEL: VisitUSubSatINTEL((USubSatINTEL)node); return;
                case Spv.Op.OpIMul32x16INTEL: VisitIMul32x16INTEL((IMul32x16INTEL)node); return;
                case Spv.Op.OpUMul32x16INTEL: VisitUMul32x16INTEL((UMul32x16INTEL)node); return;
                case Spv.Op.OpDecorateString: VisitDecorateString((DecorateString)node); return;
                case Spv.Op.OpMemberDecorateString: VisitMemberDecorateString((MemberDecorateString)node); return;
                case Spv.Op.OpVmeImageINTEL: VisitVmeImageINTEL((VmeImageINTEL)node); return;
                case Spv.Op.OpTypeVmeImageINTEL: VisitTypeVmeImageINTEL((TypeVmeImageINTEL)node); return;
                case Spv.Op.OpTypeAvcImePayloadINTEL: VisitTypeAvcImePayloadINTEL((TypeAvcImePayloadINTEL)node); return;
                case Spv.Op.OpTypeAvcRefPayloadINTEL: VisitTypeAvcRefPayloadINTEL((TypeAvcRefPayloadINTEL)node); return;
                case Spv.Op.OpTypeAvcSicPayloadINTEL: VisitTypeAvcSicPayloadINTEL((TypeAvcSicPayloadINTEL)node); return;
                case Spv.Op.OpTypeAvcMcePayloadINTEL: VisitTypeAvcMcePayloadINTEL((TypeAvcMcePayloadINTEL)node); return;
                case Spv.Op.OpTypeAvcMceResultINTEL: VisitTypeAvcMceResultINTEL((TypeAvcMceResultINTEL)node); return;
                case Spv.Op.OpTypeAvcImeResultINTEL: VisitTypeAvcImeResultINTEL((TypeAvcImeResultINTEL)node); return;
                case Spv.Op.OpTypeAvcImeResultSingleReferenceStreamoutINTEL: VisitTypeAvcImeResultSingleReferenceStreamoutINTEL((TypeAvcImeResultSingleReferenceStreamoutINTEL)node); return;
                case Spv.Op.OpTypeAvcImeResultDualReferenceStreamoutINTEL: VisitTypeAvcImeResultDualReferenceStreamoutINTEL((TypeAvcImeResultDualReferenceStreamoutINTEL)node); return;
                case Spv.Op.OpTypeAvcImeSingleReferenceStreaminINTEL: VisitTypeAvcImeSingleReferenceStreaminINTEL((TypeAvcImeSingleReferenceStreaminINTEL)node); return;
                case Spv.Op.OpTypeAvcImeDualReferenceStreaminINTEL: VisitTypeAvcImeDualReferenceStreaminINTEL((TypeAvcImeDualReferenceStreaminINTEL)node); return;
                case Spv.Op.OpTypeAvcRefResultINTEL: VisitTypeAvcRefResultINTEL((TypeAvcRefResultINTEL)node); return;
                case Spv.Op.OpTypeAvcSicResultINTEL: VisitTypeAvcSicResultINTEL((TypeAvcSicResultINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL: VisitSubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL((SubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL: VisitSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL((SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetDefaultInterShapePenaltyINTEL: VisitSubgroupAvcMceGetDefaultInterShapePenaltyINTEL((SubgroupAvcMceGetDefaultInterShapePenaltyINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceSetInterShapePenaltyINTEL: VisitSubgroupAvcMceSetInterShapePenaltyINTEL((SubgroupAvcMceSetInterShapePenaltyINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL: VisitSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL((SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceSetInterDirectionPenaltyINTEL: VisitSubgroupAvcMceSetInterDirectionPenaltyINTEL((SubgroupAvcMceSetInterDirectionPenaltyINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL: VisitSubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL((SubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL: VisitSubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL((SubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL: VisitSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL((SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL: VisitSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL((SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL: VisitSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL((SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceSetMotionVectorCostFunctionINTEL: VisitSubgroupAvcMceSetMotionVectorCostFunctionINTEL((SubgroupAvcMceSetMotionVectorCostFunctionINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL: VisitSubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL((SubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL: VisitSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL((SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL: VisitSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL((SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceSetAcOnlyHaarINTEL: VisitSubgroupAvcMceSetAcOnlyHaarINTEL((SubgroupAvcMceSetAcOnlyHaarINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL: VisitSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL((SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL: VisitSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL((SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL: VisitSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL((SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceConvertToImePayloadINTEL: VisitSubgroupAvcMceConvertToImePayloadINTEL((SubgroupAvcMceConvertToImePayloadINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceConvertToImeResultINTEL: VisitSubgroupAvcMceConvertToImeResultINTEL((SubgroupAvcMceConvertToImeResultINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceConvertToRefPayloadINTEL: VisitSubgroupAvcMceConvertToRefPayloadINTEL((SubgroupAvcMceConvertToRefPayloadINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceConvertToRefResultINTEL: VisitSubgroupAvcMceConvertToRefResultINTEL((SubgroupAvcMceConvertToRefResultINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceConvertToSicPayloadINTEL: VisitSubgroupAvcMceConvertToSicPayloadINTEL((SubgroupAvcMceConvertToSicPayloadINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceConvertToSicResultINTEL: VisitSubgroupAvcMceConvertToSicResultINTEL((SubgroupAvcMceConvertToSicResultINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetMotionVectorsINTEL: VisitSubgroupAvcMceGetMotionVectorsINTEL((SubgroupAvcMceGetMotionVectorsINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetInterDistortionsINTEL: VisitSubgroupAvcMceGetInterDistortionsINTEL((SubgroupAvcMceGetInterDistortionsINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetBestInterDistortionsINTEL: VisitSubgroupAvcMceGetBestInterDistortionsINTEL((SubgroupAvcMceGetBestInterDistortionsINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetInterMajorShapeINTEL: VisitSubgroupAvcMceGetInterMajorShapeINTEL((SubgroupAvcMceGetInterMajorShapeINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetInterMinorShapeINTEL: VisitSubgroupAvcMceGetInterMinorShapeINTEL((SubgroupAvcMceGetInterMinorShapeINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetInterDirectionsINTEL: VisitSubgroupAvcMceGetInterDirectionsINTEL((SubgroupAvcMceGetInterDirectionsINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetInterMotionVectorCountINTEL: VisitSubgroupAvcMceGetInterMotionVectorCountINTEL((SubgroupAvcMceGetInterMotionVectorCountINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetInterReferenceIdsINTEL: VisitSubgroupAvcMceGetInterReferenceIdsINTEL((SubgroupAvcMceGetInterReferenceIdsINTEL)node); return;
                case Spv.Op.OpSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL: VisitSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL((SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeInitializeINTEL: VisitSubgroupAvcImeInitializeINTEL((SubgroupAvcImeInitializeINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeSetSingleReferenceINTEL: VisitSubgroupAvcImeSetSingleReferenceINTEL((SubgroupAvcImeSetSingleReferenceINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeSetDualReferenceINTEL: VisitSubgroupAvcImeSetDualReferenceINTEL((SubgroupAvcImeSetDualReferenceINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeRefWindowSizeINTEL: VisitSubgroupAvcImeRefWindowSizeINTEL((SubgroupAvcImeRefWindowSizeINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeAdjustRefOffsetINTEL: VisitSubgroupAvcImeAdjustRefOffsetINTEL((SubgroupAvcImeAdjustRefOffsetINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeConvertToMcePayloadINTEL: VisitSubgroupAvcImeConvertToMcePayloadINTEL((SubgroupAvcImeConvertToMcePayloadINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeSetMaxMotionVectorCountINTEL: VisitSubgroupAvcImeSetMaxMotionVectorCountINTEL((SubgroupAvcImeSetMaxMotionVectorCountINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeSetUnidirectionalMixDisableINTEL: VisitSubgroupAvcImeSetUnidirectionalMixDisableINTEL((SubgroupAvcImeSetUnidirectionalMixDisableINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeSetEarlySearchTerminationThresholdINTEL: VisitSubgroupAvcImeSetEarlySearchTerminationThresholdINTEL((SubgroupAvcImeSetEarlySearchTerminationThresholdINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeSetWeightedSadINTEL: VisitSubgroupAvcImeSetWeightedSadINTEL((SubgroupAvcImeSetWeightedSadINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeEvaluateWithSingleReferenceINTEL: VisitSubgroupAvcImeEvaluateWithSingleReferenceINTEL((SubgroupAvcImeEvaluateWithSingleReferenceINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeEvaluateWithDualReferenceINTEL: VisitSubgroupAvcImeEvaluateWithDualReferenceINTEL((SubgroupAvcImeEvaluateWithDualReferenceINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL: VisitSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL((SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL: VisitSubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL((SubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL: VisitSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL((SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL: VisitSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL((SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL: VisitSubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL((SubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL: VisitSubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL((SubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeConvertToMceResultINTEL: VisitSubgroupAvcImeConvertToMceResultINTEL((SubgroupAvcImeConvertToMceResultINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetSingleReferenceStreaminINTEL: VisitSubgroupAvcImeGetSingleReferenceStreaminINTEL((SubgroupAvcImeGetSingleReferenceStreaminINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetDualReferenceStreaminINTEL: VisitSubgroupAvcImeGetDualReferenceStreaminINTEL((SubgroupAvcImeGetDualReferenceStreaminINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeStripSingleReferenceStreamoutINTEL: VisitSubgroupAvcImeStripSingleReferenceStreamoutINTEL((SubgroupAvcImeStripSingleReferenceStreamoutINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeStripDualReferenceStreamoutINTEL: VisitSubgroupAvcImeStripDualReferenceStreamoutINTEL((SubgroupAvcImeStripDualReferenceStreamoutINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL: VisitSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL((SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL: VisitSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL((SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL: VisitSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL((SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL: VisitSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL((SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL: VisitSubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL((SubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL: VisitSubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL((SubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetBorderReachedINTEL: VisitSubgroupAvcImeGetBorderReachedINTEL((SubgroupAvcImeGetBorderReachedINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetTruncatedSearchIndicationINTEL: VisitSubgroupAvcImeGetTruncatedSearchIndicationINTEL((SubgroupAvcImeGetTruncatedSearchIndicationINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL: VisitSubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL((SubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL: VisitSubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL((SubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL)node); return;
                case Spv.Op.OpSubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL: VisitSubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL((SubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL)node); return;
                case Spv.Op.OpSubgroupAvcFmeInitializeINTEL: VisitSubgroupAvcFmeInitializeINTEL((SubgroupAvcFmeInitializeINTEL)node); return;
                case Spv.Op.OpSubgroupAvcBmeInitializeINTEL: VisitSubgroupAvcBmeInitializeINTEL((SubgroupAvcBmeInitializeINTEL)node); return;
                case Spv.Op.OpSubgroupAvcRefConvertToMcePayloadINTEL: VisitSubgroupAvcRefConvertToMcePayloadINTEL((SubgroupAvcRefConvertToMcePayloadINTEL)node); return;
                case Spv.Op.OpSubgroupAvcRefSetBidirectionalMixDisableINTEL: VisitSubgroupAvcRefSetBidirectionalMixDisableINTEL((SubgroupAvcRefSetBidirectionalMixDisableINTEL)node); return;
                case Spv.Op.OpSubgroupAvcRefSetBilinearFilterEnableINTEL: VisitSubgroupAvcRefSetBilinearFilterEnableINTEL((SubgroupAvcRefSetBilinearFilterEnableINTEL)node); return;
                case Spv.Op.OpSubgroupAvcRefEvaluateWithSingleReferenceINTEL: VisitSubgroupAvcRefEvaluateWithSingleReferenceINTEL((SubgroupAvcRefEvaluateWithSingleReferenceINTEL)node); return;
                case Spv.Op.OpSubgroupAvcRefEvaluateWithDualReferenceINTEL: VisitSubgroupAvcRefEvaluateWithDualReferenceINTEL((SubgroupAvcRefEvaluateWithDualReferenceINTEL)node); return;
                case Spv.Op.OpSubgroupAvcRefEvaluateWithMultiReferenceINTEL: VisitSubgroupAvcRefEvaluateWithMultiReferenceINTEL((SubgroupAvcRefEvaluateWithMultiReferenceINTEL)node); return;
                case Spv.Op.OpSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL: VisitSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL((SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL)node); return;
                case Spv.Op.OpSubgroupAvcRefConvertToMceResultINTEL: VisitSubgroupAvcRefConvertToMceResultINTEL((SubgroupAvcRefConvertToMceResultINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicInitializeINTEL: VisitSubgroupAvcSicInitializeINTEL((SubgroupAvcSicInitializeINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicConfigureSkcINTEL: VisitSubgroupAvcSicConfigureSkcINTEL((SubgroupAvcSicConfigureSkcINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicConfigureIpeLumaINTEL: VisitSubgroupAvcSicConfigureIpeLumaINTEL((SubgroupAvcSicConfigureIpeLumaINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicConfigureIpeLumaChromaINTEL: VisitSubgroupAvcSicConfigureIpeLumaChromaINTEL((SubgroupAvcSicConfigureIpeLumaChromaINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicGetMotionVectorMaskINTEL: VisitSubgroupAvcSicGetMotionVectorMaskINTEL((SubgroupAvcSicGetMotionVectorMaskINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicConvertToMcePayloadINTEL: VisitSubgroupAvcSicConvertToMcePayloadINTEL((SubgroupAvcSicConvertToMcePayloadINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicSetIntraLumaShapePenaltyINTEL: VisitSubgroupAvcSicSetIntraLumaShapePenaltyINTEL((SubgroupAvcSicSetIntraLumaShapePenaltyINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL: VisitSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL((SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL: VisitSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL((SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicSetBilinearFilterEnableINTEL: VisitSubgroupAvcSicSetBilinearFilterEnableINTEL((SubgroupAvcSicSetBilinearFilterEnableINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicSetSkcForwardTransformEnableINTEL: VisitSubgroupAvcSicSetSkcForwardTransformEnableINTEL((SubgroupAvcSicSetSkcForwardTransformEnableINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicSetBlockBasedRawSkipSadINTEL: VisitSubgroupAvcSicSetBlockBasedRawSkipSadINTEL((SubgroupAvcSicSetBlockBasedRawSkipSadINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicEvaluateIpeINTEL: VisitSubgroupAvcSicEvaluateIpeINTEL((SubgroupAvcSicEvaluateIpeINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicEvaluateWithSingleReferenceINTEL: VisitSubgroupAvcSicEvaluateWithSingleReferenceINTEL((SubgroupAvcSicEvaluateWithSingleReferenceINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicEvaluateWithDualReferenceINTEL: VisitSubgroupAvcSicEvaluateWithDualReferenceINTEL((SubgroupAvcSicEvaluateWithDualReferenceINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicEvaluateWithMultiReferenceINTEL: VisitSubgroupAvcSicEvaluateWithMultiReferenceINTEL((SubgroupAvcSicEvaluateWithMultiReferenceINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL: VisitSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL((SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicConvertToMceResultINTEL: VisitSubgroupAvcSicConvertToMceResultINTEL((SubgroupAvcSicConvertToMceResultINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicGetIpeLumaShapeINTEL: VisitSubgroupAvcSicGetIpeLumaShapeINTEL((SubgroupAvcSicGetIpeLumaShapeINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicGetBestIpeLumaDistortionINTEL: VisitSubgroupAvcSicGetBestIpeLumaDistortionINTEL((SubgroupAvcSicGetBestIpeLumaDistortionINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicGetBestIpeChromaDistortionINTEL: VisitSubgroupAvcSicGetBestIpeChromaDistortionINTEL((SubgroupAvcSicGetBestIpeChromaDistortionINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicGetPackedIpeLumaModesINTEL: VisitSubgroupAvcSicGetPackedIpeLumaModesINTEL((SubgroupAvcSicGetPackedIpeLumaModesINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicGetIpeChromaModeINTEL: VisitSubgroupAvcSicGetIpeChromaModeINTEL((SubgroupAvcSicGetIpeChromaModeINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL: VisitSubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL((SubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL: VisitSubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL((SubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL)node); return;
                case Spv.Op.OpSubgroupAvcSicGetInterRawSadsINTEL: VisitSubgroupAvcSicGetInterRawSadsINTEL((SubgroupAvcSicGetInterRawSadsINTEL)node); return;
            }
        }

        protected virtual void VisitNop(Nop node)
        {
        }

        protected virtual void VisitUndef(Undef node)
        {
        }

        protected virtual void VisitSourceContinued(SourceContinued node)
        {
        }

        protected virtual void VisitSource(Source node)
        {
            ScheduleVisit(node.File);
        }

        protected virtual void VisitSourceExtension(SourceExtension node)
        {
        }

        protected virtual void VisitName(Name node)
        {
            ScheduleVisit(node.Target);
        }

        protected virtual void VisitMemberName(MemberName node)
        {
            ScheduleVisit(node.Type);
        }

        protected virtual void VisitString(String node)
        {
        }

        protected virtual void VisitLine(Line node)
        {
            ScheduleVisit(node.File);
        }

        protected virtual void VisitExtension(Extension node)
        {
        }

        protected virtual void VisitExtInstImport(ExtInstImport node)
        {
        }

        protected virtual void VisitExtInst(ExtInst node)
        {
            ScheduleVisit(node.Set);
            ScheduleVisit(node.Operands);
        }

        protected virtual void VisitMemoryModel(MemoryModel node)
        {
        }

        protected virtual void VisitEntryPoint(EntryPoint node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Interface);
        }

        protected virtual void VisitExecutionMode(ExecutionMode node)
        {
            ScheduleVisit(node.EntryPoint);
        }

        protected virtual void VisitCapability(Capability node)
        {
        }

        protected virtual void VisitTypeVoid(TypeVoid node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeBool(TypeBool node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeInt(TypeInt node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeFloat(TypeFloat node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeVector(TypeVector node)
        {
            VisitType(node);
            ScheduleVisit(node.ComponentType);
        }

        protected virtual void VisitTypeMatrix(TypeMatrix node)
        {
            VisitType(node);
            ScheduleVisit(node.ColumnType);
        }

        protected virtual void VisitTypeImage(TypeImage node)
        {
            VisitType(node);
            ScheduleVisit(node.SampledType);
        }

        protected virtual void VisitTypeSampler(TypeSampler node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeSampledImage(TypeSampledImage node)
        {
            VisitType(node);
            ScheduleVisit(node.ImageType);
        }

        protected virtual void VisitTypeArray(TypeArray node)
        {
            VisitType(node);
            ScheduleVisit(node.ElementType);
            ScheduleVisit(node.Length);
        }

        protected virtual void VisitTypeRuntimeArray(TypeRuntimeArray node)
        {
            VisitType(node);
            ScheduleVisit(node.ElementType);
        }

        protected virtual void VisitTypeStruct(TypeStruct node)
        {
            VisitType(node);
            ScheduleVisit(node.MemberTypes);
        }

        protected virtual void VisitTypeOpaque(TypeOpaque node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypePointer(TypePointer node)
        {
            VisitType(node);
            ScheduleVisit(node.Type);
        }

        protected virtual void VisitTypeFunction(TypeFunction node)
        {
            VisitType(node);
            ScheduleVisit(node.ReturnType);
            ScheduleVisit(node.ParameterTypes);
        }

        protected virtual void VisitTypeEvent(TypeEvent node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeDeviceEvent(TypeDeviceEvent node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeReserveId(TypeReserveId node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeQueue(TypeQueue node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypePipe(TypePipe node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeForwardPointer(TypeForwardPointer node)
        {
            VisitType(node);
            ScheduleVisit(node.PointerType);
        }

        protected virtual void VisitConstantTrue(ConstantTrue node)
        {
        }

        protected virtual void VisitConstantFalse(ConstantFalse node)
        {
        }

        protected virtual void VisitConstant(Constant node)
        {
        }

        protected virtual void VisitConstantComposite(ConstantComposite node)
        {
            ScheduleVisit(node.Constituents);
        }

        protected virtual void VisitConstantSampler(ConstantSampler node)
        {
        }

        protected virtual void VisitConstantNull(ConstantNull node)
        {
        }

        protected virtual void VisitSpecConstantTrue(SpecConstantTrue node)
        {
        }

        protected virtual void VisitSpecConstantFalse(SpecConstantFalse node)
        {
        }

        protected virtual void VisitSpecConstant(SpecConstant node)
        {
        }

        protected virtual void VisitSpecConstantComposite(SpecConstantComposite node)
        {
            ScheduleVisit(node.Constituents);
        }

        protected virtual void VisitSpecConstantOp(SpecConstantOp node)
        {
            ScheduleVisit(node.Opcode);
        }

        protected virtual void VisitFunction(Function node)
        {
            ScheduleVisit(node.FunctionType);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitFunctionParameter(FunctionParameter node)
        {
        }

        protected virtual void VisitFunctionEnd(FunctionEnd node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitFunctionCall(FunctionCall node)
        {
            ScheduleVisit(node.Function);
            ScheduleVisit(node.Arguments);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitVariable(Variable node)
        {
            ScheduleVisit(node.Initializer);
        }

        protected virtual void VisitImageTexelPointer(ImageTexelPointer node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.Sample);
        }

        protected virtual void VisitLoad(Load node)
        {
            ScheduleVisit(node.Pointer);
        }

        protected virtual void VisitStore(Store node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Object);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitCopyMemory(CopyMemory node)
        {
            ScheduleVisit(node.Target);
            ScheduleVisit(node.Source);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitCopyMemorySized(CopyMemorySized node)
        {
            ScheduleVisit(node.Target);
            ScheduleVisit(node.Source);
            ScheduleVisit(node.Size);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitAccessChain(AccessChain node)
        {
            ScheduleVisit(node.Base);
            ScheduleVisit(node.Indexes);
        }

        protected virtual void VisitInBoundsAccessChain(InBoundsAccessChain node)
        {
            ScheduleVisit(node.Base);
            ScheduleVisit(node.Indexes);
        }

        protected virtual void VisitPtrAccessChain(PtrAccessChain node)
        {
            ScheduleVisit(node.Base);
            ScheduleVisit(node.Element);
            ScheduleVisit(node.Indexes);
        }

        protected virtual void VisitArrayLength(ArrayLength node)
        {
            ScheduleVisit(node.Structure);
        }

        protected virtual void VisitGenericPtrMemSemantics(GenericPtrMemSemantics node)
        {
            ScheduleVisit(node.Pointer);
        }

        protected virtual void VisitInBoundsPtrAccessChain(InBoundsPtrAccessChain node)
        {
            ScheduleVisit(node.Base);
            ScheduleVisit(node.Element);
            ScheduleVisit(node.Indexes);
        }

        protected virtual void VisitDecorate(Decorate node)
        {
            ScheduleVisit(node.Target);
        }

        protected virtual void VisitMemberDecorate(MemberDecorate node)
        {
            ScheduleVisit(node.StructureType);
        }

        protected virtual void VisitDecorationGroup(DecorationGroup node)
        {
        }

        protected virtual void VisitGroupDecorate(GroupDecorate node)
        {
            ScheduleVisit(node.DecorationGroup);
            ScheduleVisit(node.Targets);
        }

        protected virtual void VisitGroupMemberDecorate(GroupMemberDecorate node)
        {
            ScheduleVisit(node.DecorationGroup);
            ScheduleVisit(node.Targets);
        }

        protected virtual void VisitVectorExtractDynamic(VectorExtractDynamic node)
        {
            ScheduleVisit(node.Vector);
            ScheduleVisit(node.Index);
        }

        protected virtual void VisitVectorInsertDynamic(VectorInsertDynamic node)
        {
            ScheduleVisit(node.Vector);
            ScheduleVisit(node.Component);
            ScheduleVisit(node.Index);
        }

        protected virtual void VisitVectorShuffle(VectorShuffle node)
        {
            ScheduleVisit(node.Vector1);
            ScheduleVisit(node.Vector2);
        }

        protected virtual void VisitCompositeConstruct(CompositeConstruct node)
        {
            ScheduleVisit(node.Constituents);
        }

        protected virtual void VisitCompositeExtract(CompositeExtract node)
        {
            ScheduleVisit(node.Composite);
        }

        protected virtual void VisitCompositeInsert(CompositeInsert node)
        {
            ScheduleVisit(node.Object);
            ScheduleVisit(node.Composite);
        }

        protected virtual void VisitCopyObject(CopyObject node)
        {
            ScheduleVisit(node.Operand);
        }

        protected virtual void VisitTranspose(Transpose node)
        {
            ScheduleVisit(node.Matrix);
        }

        protected virtual void VisitSampledImage(SampledImage node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Sampler);
        }

        protected virtual void VisitImageSampleImplicitLod(ImageSampleImplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitImageSampleExplicitLod(ImageSampleExplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitImageSampleDrefImplicitLod(ImageSampleDrefImplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.D_ref);
        }

        protected virtual void VisitImageSampleDrefExplicitLod(ImageSampleDrefExplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.D_ref);
        }

        protected virtual void VisitImageSampleProjImplicitLod(ImageSampleProjImplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitImageSampleProjExplicitLod(ImageSampleProjExplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitImageSampleProjDrefImplicitLod(ImageSampleProjDrefImplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.D_ref);
        }

        protected virtual void VisitImageSampleProjDrefExplicitLod(ImageSampleProjDrefExplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.D_ref);
        }

        protected virtual void VisitImageFetch(ImageFetch node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitImageGather(ImageGather node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.Component);
        }

        protected virtual void VisitImageDrefGather(ImageDrefGather node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.D_ref);
        }

        protected virtual void VisitImageRead(ImageRead node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitImageWrite(ImageWrite node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.Texel);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitImage(Image node)
        {
            ScheduleVisit(node.SampledImage);
        }

        protected virtual void VisitImageQueryFormat(ImageQueryFormat node)
        {
            ScheduleVisit(node.Image);
        }

        protected virtual void VisitImageQueryOrder(ImageQueryOrder node)
        {
            ScheduleVisit(node.Image);
        }

        protected virtual void VisitImageQuerySizeLod(ImageQuerySizeLod node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.LevelofDetail);
        }

        protected virtual void VisitImageQuerySize(ImageQuerySize node)
        {
            ScheduleVisit(node.Image);
        }

        protected virtual void VisitImageQueryLod(ImageQueryLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitImageQueryLevels(ImageQueryLevels node)
        {
            ScheduleVisit(node.Image);
        }

        protected virtual void VisitImageQuerySamples(ImageQuerySamples node)
        {
            ScheduleVisit(node.Image);
        }

        protected virtual void VisitConvertFToU(ConvertFToU node)
        {
            ScheduleVisit(node.FloatValue);
        }

        protected virtual void VisitConvertFToS(ConvertFToS node)
        {
            ScheduleVisit(node.FloatValue);
        }

        protected virtual void VisitConvertSToF(ConvertSToF node)
        {
            ScheduleVisit(node.SignedValue);
        }

        protected virtual void VisitConvertUToF(ConvertUToF node)
        {
            ScheduleVisit(node.UnsignedValue);
        }

        protected virtual void VisitUConvert(UConvert node)
        {
            ScheduleVisit(node.UnsignedValue);
        }

        protected virtual void VisitSConvert(SConvert node)
        {
            ScheduleVisit(node.SignedValue);
        }

        protected virtual void VisitFConvert(FConvert node)
        {
            ScheduleVisit(node.FloatValue);
        }

        protected virtual void VisitQuantizeToF16(QuantizeToF16 node)
        {
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitConvertPtrToU(ConvertPtrToU node)
        {
            ScheduleVisit(node.Pointer);
        }

        protected virtual void VisitSatConvertSToU(SatConvertSToU node)
        {
            ScheduleVisit(node.SignedValue);
        }

        protected virtual void VisitSatConvertUToS(SatConvertUToS node)
        {
            ScheduleVisit(node.UnsignedValue);
        }

        protected virtual void VisitConvertUToPtr(ConvertUToPtr node)
        {
            ScheduleVisit(node.IntegerValue);
        }

        protected virtual void VisitPtrCastToGeneric(PtrCastToGeneric node)
        {
            ScheduleVisit(node.Pointer);
        }

        protected virtual void VisitGenericCastToPtr(GenericCastToPtr node)
        {
            ScheduleVisit(node.Pointer);
        }

        protected virtual void VisitGenericCastToPtrExplicit(GenericCastToPtrExplicit node)
        {
            ScheduleVisit(node.Pointer);
        }

        protected virtual void VisitBitcast(Bitcast node)
        {
            ScheduleVisit(node.Operand);
        }

        protected virtual void VisitSNegate(SNegate node)
        {
            ScheduleVisit(node.Operand);
        }

        protected virtual void VisitFNegate(FNegate node)
        {
            ScheduleVisit(node.Operand);
        }

        protected virtual void VisitIAdd(IAdd node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFAdd(FAdd node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitISub(ISub node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFSub(FSub node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitIMul(IMul node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFMul(FMul node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitUDiv(UDiv node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitSDiv(SDiv node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFDiv(FDiv node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitUMod(UMod node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitSRem(SRem node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitSMod(SMod node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFRem(FRem node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFMod(FMod node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitVectorTimesScalar(VectorTimesScalar node)
        {
            ScheduleVisit(node.Vector);
            ScheduleVisit(node.Scalar);
        }

        protected virtual void VisitMatrixTimesScalar(MatrixTimesScalar node)
        {
            ScheduleVisit(node.Matrix);
            ScheduleVisit(node.Scalar);
        }

        protected virtual void VisitVectorTimesMatrix(VectorTimesMatrix node)
        {
            ScheduleVisit(node.Vector);
            ScheduleVisit(node.Matrix);
        }

        protected virtual void VisitMatrixTimesVector(MatrixTimesVector node)
        {
            ScheduleVisit(node.Matrix);
            ScheduleVisit(node.Vector);
        }

        protected virtual void VisitMatrixTimesMatrix(MatrixTimesMatrix node)
        {
            ScheduleVisit(node.LeftMatrix);
            ScheduleVisit(node.RightMatrix);
        }

        protected virtual void VisitOuterProduct(OuterProduct node)
        {
            ScheduleVisit(node.Vector1);
            ScheduleVisit(node.Vector2);
        }

        protected virtual void VisitDot(Dot node)
        {
            ScheduleVisit(node.Vector1);
            ScheduleVisit(node.Vector2);
        }

        protected virtual void VisitIAddCarry(IAddCarry node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitISubBorrow(ISubBorrow node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitUMulExtended(UMulExtended node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitSMulExtended(SMulExtended node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitAny(Any node)
        {
            ScheduleVisit(node.Vector);
        }

        protected virtual void VisitAll(All node)
        {
            ScheduleVisit(node.Vector);
        }

        protected virtual void VisitIsNan(IsNan node)
        {
            ScheduleVisit(node.x);
        }

        protected virtual void VisitIsInf(IsInf node)
        {
            ScheduleVisit(node.x);
        }

        protected virtual void VisitIsFinite(IsFinite node)
        {
            ScheduleVisit(node.x);
        }

        protected virtual void VisitIsNormal(IsNormal node)
        {
            ScheduleVisit(node.x);
        }

        protected virtual void VisitSignBitSet(SignBitSet node)
        {
            ScheduleVisit(node.x);
        }

        protected virtual void VisitLessOrGreater(LessOrGreater node)
        {
            ScheduleVisit(node.x);
            ScheduleVisit(node.y);
        }

        protected virtual void VisitOrdered(Ordered node)
        {
            ScheduleVisit(node.x);
            ScheduleVisit(node.y);
        }

        protected virtual void VisitUnordered(Unordered node)
        {
            ScheduleVisit(node.x);
            ScheduleVisit(node.y);
        }

        protected virtual void VisitLogicalEqual(LogicalEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitLogicalNotEqual(LogicalNotEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitLogicalOr(LogicalOr node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitLogicalAnd(LogicalAnd node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitLogicalNot(LogicalNot node)
        {
            ScheduleVisit(node.Operand);
        }

        protected virtual void VisitSelect(Select node)
        {
            ScheduleVisit(node.Condition);
            ScheduleVisit(node.Object1);
            ScheduleVisit(node.Object2);
        }

        protected virtual void VisitIEqual(IEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitINotEqual(INotEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitUGreaterThan(UGreaterThan node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitSGreaterThan(SGreaterThan node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitUGreaterThanEqual(UGreaterThanEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitSGreaterThanEqual(SGreaterThanEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitULessThan(ULessThan node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitSLessThan(SLessThan node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitULessThanEqual(ULessThanEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitSLessThanEqual(SLessThanEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFOrdEqual(FOrdEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFUnordEqual(FUnordEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFOrdNotEqual(FOrdNotEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFUnordNotEqual(FUnordNotEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFOrdLessThan(FOrdLessThan node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFUnordLessThan(FUnordLessThan node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFOrdGreaterThan(FOrdGreaterThan node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFUnordGreaterThan(FUnordGreaterThan node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFOrdLessThanEqual(FOrdLessThanEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFUnordLessThanEqual(FUnordLessThanEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFOrdGreaterThanEqual(FOrdGreaterThanEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitFUnordGreaterThanEqual(FUnordGreaterThanEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitShiftRightLogical(ShiftRightLogical node)
        {
            ScheduleVisit(node.Base);
            ScheduleVisit(node.Shift);
        }

        protected virtual void VisitShiftRightArithmetic(ShiftRightArithmetic node)
        {
            ScheduleVisit(node.Base);
            ScheduleVisit(node.Shift);
        }

        protected virtual void VisitShiftLeftLogical(ShiftLeftLogical node)
        {
            ScheduleVisit(node.Base);
            ScheduleVisit(node.Shift);
        }

        protected virtual void VisitBitwiseOr(BitwiseOr node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitBitwiseXor(BitwiseXor node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitBitwiseAnd(BitwiseAnd node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitNot(Not node)
        {
            ScheduleVisit(node.Operand);
        }

        protected virtual void VisitBitFieldInsert(BitFieldInsert node)
        {
            ScheduleVisit(node.Base);
            ScheduleVisit(node.Insert);
            ScheduleVisit(node.Offset);
            ScheduleVisit(node.Count);
        }

        protected virtual void VisitBitFieldSExtract(BitFieldSExtract node)
        {
            ScheduleVisit(node.Base);
            ScheduleVisit(node.Offset);
            ScheduleVisit(node.Count);
        }

        protected virtual void VisitBitFieldUExtract(BitFieldUExtract node)
        {
            ScheduleVisit(node.Base);
            ScheduleVisit(node.Offset);
            ScheduleVisit(node.Count);
        }

        protected virtual void VisitBitReverse(BitReverse node)
        {
            ScheduleVisit(node.Base);
        }

        protected virtual void VisitBitCount(BitCount node)
        {
            ScheduleVisit(node.Base);
        }

        protected virtual void VisitDPdx(DPdx node)
        {
            ScheduleVisit(node.P);
        }

        protected virtual void VisitDPdy(DPdy node)
        {
            ScheduleVisit(node.P);
        }

        protected virtual void VisitFwidth(Fwidth node)
        {
            ScheduleVisit(node.P);
        }

        protected virtual void VisitDPdxFine(DPdxFine node)
        {
            ScheduleVisit(node.P);
        }

        protected virtual void VisitDPdyFine(DPdyFine node)
        {
            ScheduleVisit(node.P);
        }

        protected virtual void VisitFwidthFine(FwidthFine node)
        {
            ScheduleVisit(node.P);
        }

        protected virtual void VisitDPdxCoarse(DPdxCoarse node)
        {
            ScheduleVisit(node.P);
        }

        protected virtual void VisitDPdyCoarse(DPdyCoarse node)
        {
            ScheduleVisit(node.P);
        }

        protected virtual void VisitFwidthCoarse(FwidthCoarse node)
        {
            ScheduleVisit(node.P);
        }

        protected virtual void VisitEmitVertex(EmitVertex node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitEndPrimitive(EndPrimitive node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitEmitStreamVertex(EmitStreamVertex node)
        {
            ScheduleVisit(node.Stream);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitEndStreamPrimitive(EndStreamPrimitive node)
        {
            ScheduleVisit(node.Stream);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitControlBarrier(ControlBarrier node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitMemoryBarrier(MemoryBarrier node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitAtomicLoad(AtomicLoad node)
        {
            ScheduleVisit(node.Pointer);
        }

        protected virtual void VisitAtomicStore(AtomicStore node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitAtomicExchange(AtomicExchange node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitAtomicCompareExchange(AtomicCompareExchange node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Comparator);
        }

        protected virtual void VisitAtomicCompareExchangeWeak(AtomicCompareExchangeWeak node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Comparator);
        }

        protected virtual void VisitAtomicIIncrement(AtomicIIncrement node)
        {
            ScheduleVisit(node.Pointer);
        }

        protected virtual void VisitAtomicIDecrement(AtomicIDecrement node)
        {
            ScheduleVisit(node.Pointer);
        }

        protected virtual void VisitAtomicIAdd(AtomicIAdd node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitAtomicISub(AtomicISub node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitAtomicSMin(AtomicSMin node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitAtomicUMin(AtomicUMin node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitAtomicSMax(AtomicSMax node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitAtomicUMax(AtomicUMax node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitAtomicAnd(AtomicAnd node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitAtomicOr(AtomicOr node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitAtomicXor(AtomicXor node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitPhi(Phi node)
        {
            ScheduleVisit(node.VariableParent);
        }

        protected virtual void VisitLoopMerge(LoopMerge node)
        {
            ScheduleVisit(node.MergeBlock);
            ScheduleVisit(node.ContinueTarget);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitSelectionMerge(SelectionMerge node)
        {
            ScheduleVisit(node.MergeBlock);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitLabel(Label node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitBranch(Branch node)
        {
            ScheduleVisit(node.TargetLabel);
        }

        protected virtual void VisitBranchConditional(BranchConditional node)
        {
            ScheduleVisit(node.Condition);
            ScheduleVisit(node.TrueLabel);
            ScheduleVisit(node.FalseLabel);
        }

        protected virtual void VisitSwitch(Switch node)
        {
            ScheduleVisit(node.Selector);
            ScheduleVisit(node.Default);
            ScheduleVisit(node.Target);
        }

        protected virtual void VisitKill(Kill node)
        {
        }

        protected virtual void VisitReturn(Return node)
        {
        }

        protected virtual void VisitReturnValue(ReturnValue node)
        {
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitUnreachable(Unreachable node)
        {
        }

        protected virtual void VisitLifetimeStart(LifetimeStart node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitLifetimeStop(LifetimeStop node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitGroupAsyncCopy(GroupAsyncCopy node)
        {
            ScheduleVisit(node.Destination);
            ScheduleVisit(node.Source);
            ScheduleVisit(node.NumElements);
            ScheduleVisit(node.Stride);
            ScheduleVisit(node.Event);
        }

        protected virtual void VisitGroupWaitEvents(GroupWaitEvents node)
        {
            ScheduleVisit(node.NumEvents);
            ScheduleVisit(node.EventsList);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitGroupAll(GroupAll node)
        {
            ScheduleVisit(node.Predicate);
        }

        protected virtual void VisitGroupAny(GroupAny node)
        {
            ScheduleVisit(node.Predicate);
        }

        protected virtual void VisitGroupBroadcast(GroupBroadcast node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.LocalId);
        }

        protected virtual void VisitGroupIAdd(GroupIAdd node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupFAdd(GroupFAdd node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupFMin(GroupFMin node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupUMin(GroupUMin node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupSMin(GroupSMin node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupFMax(GroupFMax node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupUMax(GroupUMax node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupSMax(GroupSMax node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitReadPipe(ReadPipe node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
        }

        protected virtual void VisitWritePipe(WritePipe node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
        }

        protected virtual void VisitReservedReadPipe(ReservedReadPipe node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.ReserveId);
            ScheduleVisit(node.Index);
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
        }

        protected virtual void VisitReservedWritePipe(ReservedWritePipe node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.ReserveId);
            ScheduleVisit(node.Index);
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
        }

        protected virtual void VisitReserveReadPipePackets(ReserveReadPipePackets node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.NumPackets);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
        }

        protected virtual void VisitReserveWritePipePackets(ReserveWritePipePackets node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.NumPackets);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
        }

        protected virtual void VisitCommitReadPipe(CommitReadPipe node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.ReserveId);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitCommitWritePipe(CommitWritePipe node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.ReserveId);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitIsValidReserveId(IsValidReserveId node)
        {
            ScheduleVisit(node.ReserveId);
        }

        protected virtual void VisitGetNumPipePackets(GetNumPipePackets node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
        }

        protected virtual void VisitGetMaxPipePackets(GetMaxPipePackets node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
        }

        protected virtual void VisitGroupReserveReadPipePackets(GroupReserveReadPipePackets node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.NumPackets);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
        }

        protected virtual void VisitGroupReserveWritePipePackets(GroupReserveWritePipePackets node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.NumPackets);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
        }

        protected virtual void VisitGroupCommitReadPipe(GroupCommitReadPipe node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.ReserveId);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitGroupCommitWritePipe(GroupCommitWritePipe node)
        {
            ScheduleVisit(node.Pipe);
            ScheduleVisit(node.ReserveId);
            ScheduleVisit(node.PacketSize);
            ScheduleVisit(node.PacketAlignment);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitEnqueueMarker(EnqueueMarker node)
        {
            ScheduleVisit(node.Queue);
            ScheduleVisit(node.NumEvents);
            ScheduleVisit(node.WaitEvents);
            ScheduleVisit(node.RetEvent);
        }

        protected virtual void VisitEnqueueKernel(EnqueueKernel node)
        {
            ScheduleVisit(node.Queue);
            ScheduleVisit(node.Flags);
            ScheduleVisit(node.NDRange);
            ScheduleVisit(node.NumEvents);
            ScheduleVisit(node.WaitEvents);
            ScheduleVisit(node.RetEvent);
            ScheduleVisit(node.Invoke);
            ScheduleVisit(node.Param);
            ScheduleVisit(node.ParamSize);
            ScheduleVisit(node.ParamAlign);
            ScheduleVisit(node.LocalSize);
        }

        protected virtual void VisitGetKernelNDrangeSubGroupCount(GetKernelNDrangeSubGroupCount node)
        {
            ScheduleVisit(node.NDRange);
            ScheduleVisit(node.Invoke);
            ScheduleVisit(node.Param);
            ScheduleVisit(node.ParamSize);
            ScheduleVisit(node.ParamAlign);
        }

        protected virtual void VisitGetKernelNDrangeMaxSubGroupSize(GetKernelNDrangeMaxSubGroupSize node)
        {
            ScheduleVisit(node.NDRange);
            ScheduleVisit(node.Invoke);
            ScheduleVisit(node.Param);
            ScheduleVisit(node.ParamSize);
            ScheduleVisit(node.ParamAlign);
        }

        protected virtual void VisitGetKernelWorkGroupSize(GetKernelWorkGroupSize node)
        {
            ScheduleVisit(node.Invoke);
            ScheduleVisit(node.Param);
            ScheduleVisit(node.ParamSize);
            ScheduleVisit(node.ParamAlign);
        }

        protected virtual void VisitGetKernelPreferredWorkGroupSizeMultiple(GetKernelPreferredWorkGroupSizeMultiple node)
        {
            ScheduleVisit(node.Invoke);
            ScheduleVisit(node.Param);
            ScheduleVisit(node.ParamSize);
            ScheduleVisit(node.ParamAlign);
        }

        protected virtual void VisitRetainEvent(RetainEvent node)
        {
            ScheduleVisit(node.Event);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitReleaseEvent(ReleaseEvent node)
        {
            ScheduleVisit(node.Event);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitCreateUserEvent(CreateUserEvent node)
        {
        }

        protected virtual void VisitIsValidEvent(IsValidEvent node)
        {
            ScheduleVisit(node.Event);
        }

        protected virtual void VisitSetUserEventStatus(SetUserEventStatus node)
        {
            ScheduleVisit(node.Event);
            ScheduleVisit(node.Status);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitCaptureEventProfilingInfo(CaptureEventProfilingInfo node)
        {
            ScheduleVisit(node.Event);
            ScheduleVisit(node.ProfilingInfo);
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitGetDefaultQueue(GetDefaultQueue node)
        {
        }

        protected virtual void VisitBuildNDRange(BuildNDRange node)
        {
            ScheduleVisit(node.GlobalWorkSize);
            ScheduleVisit(node.LocalWorkSize);
            ScheduleVisit(node.GlobalWorkOffset);
        }

        protected virtual void VisitImageSparseSampleImplicitLod(ImageSparseSampleImplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitImageSparseSampleExplicitLod(ImageSparseSampleExplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitImageSparseSampleDrefImplicitLod(ImageSparseSampleDrefImplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.D_ref);
        }

        protected virtual void VisitImageSparseSampleDrefExplicitLod(ImageSparseSampleDrefExplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.D_ref);
        }

        protected virtual void VisitImageSparseSampleProjImplicitLod(ImageSparseSampleProjImplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitImageSparseSampleProjExplicitLod(ImageSparseSampleProjExplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitImageSparseSampleProjDrefImplicitLod(ImageSparseSampleProjDrefImplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.D_ref);
        }

        protected virtual void VisitImageSparseSampleProjDrefExplicitLod(ImageSparseSampleProjDrefExplicitLod node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.D_ref);
        }

        protected virtual void VisitImageSparseFetch(ImageSparseFetch node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitImageSparseGather(ImageSparseGather node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.Component);
        }

        protected virtual void VisitImageSparseDrefGather(ImageSparseDrefGather node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.D_ref);
        }

        protected virtual void VisitImageSparseTexelsResident(ImageSparseTexelsResident node)
        {
            ScheduleVisit(node.ResidentCode);
        }

        protected virtual void VisitNoLine(NoLine node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitAtomicFlagTestAndSet(AtomicFlagTestAndSet node)
        {
            ScheduleVisit(node.Pointer);
        }

        protected virtual void VisitAtomicFlagClear(AtomicFlagClear node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitImageSparseRead(ImageSparseRead node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitSizeOf(SizeOf node)
        {
            ScheduleVisit(node.Pointer);
        }

        protected virtual void VisitTypePipeStorage(TypePipeStorage node)
        {
            VisitType(node);
        }

        protected virtual void VisitConstantPipeStorage(ConstantPipeStorage node)
        {
        }

        protected virtual void VisitCreatePipeFromPipeStorage(CreatePipeFromPipeStorage node)
        {
            ScheduleVisit(node.PipeStorage);
        }

        protected virtual void VisitGetKernelLocalSizeForSubgroupCount(GetKernelLocalSizeForSubgroupCount node)
        {
            ScheduleVisit(node.SubgroupCount);
            ScheduleVisit(node.Invoke);
            ScheduleVisit(node.Param);
            ScheduleVisit(node.ParamSize);
            ScheduleVisit(node.ParamAlign);
        }

        protected virtual void VisitGetKernelMaxNumSubgroups(GetKernelMaxNumSubgroups node)
        {
            ScheduleVisit(node.Invoke);
            ScheduleVisit(node.Param);
            ScheduleVisit(node.ParamSize);
            ScheduleVisit(node.ParamAlign);
        }

        protected virtual void VisitTypeNamedBarrier(TypeNamedBarrier node)
        {
            VisitType(node);
        }

        protected virtual void VisitNamedBarrierInitialize(NamedBarrierInitialize node)
        {
            ScheduleVisit(node.SubgroupCount);
        }

        protected virtual void VisitMemoryNamedBarrier(MemoryNamedBarrier node)
        {
            ScheduleVisit(node.NamedBarrier);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitModuleProcessed(ModuleProcessed node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitExecutionModeId(ExecutionModeId node)
        {
            ScheduleVisit(node.EntryPoint);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitDecorateId(DecorateId node)
        {
            ScheduleVisit(node.Target);
        }

        protected virtual void VisitGroupNonUniformElect(GroupNonUniformElect node)
        {
        }

        protected virtual void VisitGroupNonUniformAll(GroupNonUniformAll node)
        {
            ScheduleVisit(node.Predicate);
        }

        protected virtual void VisitGroupNonUniformAny(GroupNonUniformAny node)
        {
            ScheduleVisit(node.Predicate);
        }

        protected virtual void VisitGroupNonUniformAllEqual(GroupNonUniformAllEqual node)
        {
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitGroupNonUniformBroadcast(GroupNonUniformBroadcast node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Id);
        }

        protected virtual void VisitGroupNonUniformBroadcastFirst(GroupNonUniformBroadcastFirst node)
        {
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitGroupNonUniformBallot(GroupNonUniformBallot node)
        {
            ScheduleVisit(node.Predicate);
        }

        protected virtual void VisitGroupNonUniformInverseBallot(GroupNonUniformInverseBallot node)
        {
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitGroupNonUniformBallotBitExtract(GroupNonUniformBallotBitExtract node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Index);
        }

        protected virtual void VisitGroupNonUniformBallotBitCount(GroupNonUniformBallotBitCount node)
        {
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitGroupNonUniformBallotFindLSB(GroupNonUniformBallotFindLSB node)
        {
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitGroupNonUniformBallotFindMSB(GroupNonUniformBallotFindMSB node)
        {
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitGroupNonUniformShuffle(GroupNonUniformShuffle node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Id);
        }

        protected virtual void VisitGroupNonUniformShuffleXor(GroupNonUniformShuffleXor node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Mask);
        }

        protected virtual void VisitGroupNonUniformShuffleUp(GroupNonUniformShuffleUp node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Delta);
        }

        protected virtual void VisitGroupNonUniformShuffleDown(GroupNonUniformShuffleDown node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Delta);
        }

        protected virtual void VisitGroupNonUniformIAdd(GroupNonUniformIAdd node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformFAdd(GroupNonUniformFAdd node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformIMul(GroupNonUniformIMul node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformFMul(GroupNonUniformFMul node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformSMin(GroupNonUniformSMin node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformUMin(GroupNonUniformUMin node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformFMin(GroupNonUniformFMin node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformSMax(GroupNonUniformSMax node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformUMax(GroupNonUniformUMax node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformFMax(GroupNonUniformFMax node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformBitwiseAnd(GroupNonUniformBitwiseAnd node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformBitwiseOr(GroupNonUniformBitwiseOr node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformBitwiseXor(GroupNonUniformBitwiseXor node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformLogicalAnd(GroupNonUniformLogicalAnd node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformLogicalOr(GroupNonUniformLogicalOr node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformLogicalXor(GroupNonUniformLogicalXor node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.ClusterSize);
        }

        protected virtual void VisitGroupNonUniformQuadBroadcast(GroupNonUniformQuadBroadcast node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Index);
        }

        protected virtual void VisitGroupNonUniformQuadSwap(GroupNonUniformQuadSwap node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Direction);
        }

        protected virtual void VisitCopyLogical(CopyLogical node)
        {
            ScheduleVisit(node.Operand);
        }

        protected virtual void VisitPtrEqual(PtrEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitPtrNotEqual(PtrNotEqual node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitPtrDiff(PtrDiff node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitSubgroupBallotKHR(SubgroupBallotKHR node)
        {
            ScheduleVisit(node.Predicate);
        }

        protected virtual void VisitSubgroupFirstInvocationKHR(SubgroupFirstInvocationKHR node)
        {
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitSubgroupAllKHR(SubgroupAllKHR node)
        {
            ScheduleVisit(node.Predicate);
        }

        protected virtual void VisitSubgroupAnyKHR(SubgroupAnyKHR node)
        {
            ScheduleVisit(node.Predicate);
        }

        protected virtual void VisitSubgroupAllEqualKHR(SubgroupAllEqualKHR node)
        {
            ScheduleVisit(node.Predicate);
        }

        protected virtual void VisitSubgroupReadInvocationKHR(SubgroupReadInvocationKHR node)
        {
            ScheduleVisit(node.Value);
            ScheduleVisit(node.Index);
        }

        protected virtual void VisitGroupIAddNonUniformAMD(GroupIAddNonUniformAMD node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupFAddNonUniformAMD(GroupFAddNonUniformAMD node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupFMinNonUniformAMD(GroupFMinNonUniformAMD node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupUMinNonUniformAMD(GroupUMinNonUniformAMD node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupSMinNonUniformAMD(GroupSMinNonUniformAMD node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupFMaxNonUniformAMD(GroupFMaxNonUniformAMD node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupUMaxNonUniformAMD(GroupUMaxNonUniformAMD node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitGroupSMaxNonUniformAMD(GroupSMaxNonUniformAMD node)
        {
            ScheduleVisit(node.X);
        }

        protected virtual void VisitFragmentMaskFetchAMD(FragmentMaskFetchAMD node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitFragmentFetchAMD(FragmentFetchAMD node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.FragmentIndex);
        }

        protected virtual void VisitReadClockKHR(ReadClockKHR node)
        {
        }

        protected virtual void VisitImageSampleFootprintNV(ImageSampleFootprintNV node)
        {
            ScheduleVisit(node.SampledImage);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.Granularity);
            ScheduleVisit(node.Coarse);
        }

        protected virtual void VisitGroupNonUniformPartitionNV(GroupNonUniformPartitionNV node)
        {
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitWritePackedPrimitiveIndices4x8NV(WritePackedPrimitiveIndices4x8NV node)
        {
            ScheduleVisit(node.IndexOffset);
            ScheduleVisit(node.PackedIndices);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitReportIntersectionNV(ReportIntersectionNV node)
        {
            ScheduleVisit(node.Hit);
            ScheduleVisit(node.HitKind);
        }

        protected virtual void VisitIgnoreIntersectionNV(IgnoreIntersectionNV node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitTerminateRayNV(TerminateRayNV node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitTraceNV(TraceNV node)
        {
            ScheduleVisit(node.Accel);
            ScheduleVisit(node.RayFlags);
            ScheduleVisit(node.CullMask);
            ScheduleVisit(node.SBTOffset);
            ScheduleVisit(node.SBTStride);
            ScheduleVisit(node.MissIndex);
            ScheduleVisit(node.RayOrigin);
            ScheduleVisit(node.RayTmin);
            ScheduleVisit(node.RayDirection);
            ScheduleVisit(node.RayTmax);
            ScheduleVisit(node.PayloadId);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitTypeAccelerationStructureNV(TypeAccelerationStructureNV node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeRayQueryProvisionalKHR(TypeRayQueryProvisionalKHR node)
        {
            VisitType(node);
        }

        protected virtual void VisitRayQueryInitializeKHR(RayQueryInitializeKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Accel);
            ScheduleVisit(node.RayFlags);
            ScheduleVisit(node.CullMask);
            ScheduleVisit(node.RayOrigin);
            ScheduleVisit(node.RayTMin);
            ScheduleVisit(node.RayDirection);
            ScheduleVisit(node.RayTMax);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitRayQueryTerminateKHR(RayQueryTerminateKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitRayQueryGenerateIntersectionKHR(RayQueryGenerateIntersectionKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.HitT);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitRayQueryConfirmIntersectionKHR(RayQueryConfirmIntersectionKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitRayQueryProceedKHR(RayQueryProceedKHR node)
        {
            ScheduleVisit(node.RayQuery);
        }

        protected virtual void VisitRayQueryGetIntersectionTypeKHR(RayQueryGetIntersectionTypeKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitRayQueryGetRayTMinKHR(RayQueryGetRayTMinKHR node)
        {
            ScheduleVisit(node.RayQuery);
        }

        protected virtual void VisitRayQueryGetRayFlagsKHR(RayQueryGetRayFlagsKHR node)
        {
            ScheduleVisit(node.RayQuery);
        }

        protected virtual void VisitRayQueryGetIntersectionTKHR(RayQueryGetIntersectionTKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitRayQueryGetIntersectionInstanceCustomIndexKHR(RayQueryGetIntersectionInstanceCustomIndexKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitRayQueryGetIntersectionInstanceIdKHR(RayQueryGetIntersectionInstanceIdKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitRayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR(RayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitRayQueryGetIntersectionGeometryIndexKHR(RayQueryGetIntersectionGeometryIndexKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitRayQueryGetIntersectionPrimitiveIndexKHR(RayQueryGetIntersectionPrimitiveIndexKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitRayQueryGetIntersectionBarycentricsKHR(RayQueryGetIntersectionBarycentricsKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitRayQueryGetIntersectionFrontFaceKHR(RayQueryGetIntersectionFrontFaceKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitRayQueryGetIntersectionCandidateAABBOpaqueKHR(RayQueryGetIntersectionCandidateAABBOpaqueKHR node)
        {
            ScheduleVisit(node.RayQuery);
        }

        protected virtual void VisitRayQueryGetIntersectionObjectRayDirectionKHR(RayQueryGetIntersectionObjectRayDirectionKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitRayQueryGetIntersectionObjectRayOriginKHR(RayQueryGetIntersectionObjectRayOriginKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitRayQueryGetWorldRayDirectionKHR(RayQueryGetWorldRayDirectionKHR node)
        {
            ScheduleVisit(node.RayQuery);
        }

        protected virtual void VisitRayQueryGetWorldRayOriginKHR(RayQueryGetWorldRayOriginKHR node)
        {
            ScheduleVisit(node.RayQuery);
        }

        protected virtual void VisitRayQueryGetIntersectionObjectToWorldKHR(RayQueryGetIntersectionObjectToWorldKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitRayQueryGetIntersectionWorldToObjectKHR(RayQueryGetIntersectionWorldToObjectKHR node)
        {
            ScheduleVisit(node.RayQuery);
            ScheduleVisit(node.Intersection);
        }

        protected virtual void VisitExecuteCallableNV(ExecuteCallableNV node)
        {
            ScheduleVisit(node.SBTIndex);
            ScheduleVisit(node.CallableDataId);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitTypeCooperativeMatrixNV(TypeCooperativeMatrixNV node)
        {
            VisitType(node);
            ScheduleVisit(node.ComponentType);
            ScheduleVisit(node.Rows);
            ScheduleVisit(node.Columns);
        }

        protected virtual void VisitCooperativeMatrixLoadNV(CooperativeMatrixLoadNV node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Stride);
            ScheduleVisit(node.ColumnMajor);
        }

        protected virtual void VisitCooperativeMatrixStoreNV(CooperativeMatrixStoreNV node)
        {
            ScheduleVisit(node.Pointer);
            ScheduleVisit(node.Object);
            ScheduleVisit(node.Stride);
            ScheduleVisit(node.ColumnMajor);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitCooperativeMatrixMulAddNV(CooperativeMatrixMulAddNV node)
        {
            ScheduleVisit(node.A);
            ScheduleVisit(node.B);
            ScheduleVisit(node.C);
        }

        protected virtual void VisitCooperativeMatrixLengthNV(CooperativeMatrixLengthNV node)
        {
            ScheduleVisit(node.Type);
        }

        protected virtual void VisitBeginInvocationInterlockEXT(BeginInvocationInterlockEXT node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitEndInvocationInterlockEXT(EndInvocationInterlockEXT node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitDemoteToHelperInvocationEXT(DemoteToHelperInvocationEXT node)
        {
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitIsHelperInvocationEXT(IsHelperInvocationEXT node)
        {
        }

        protected virtual void VisitSubgroupShuffleINTEL(SubgroupShuffleINTEL node)
        {
            ScheduleVisit(node.Data);
            ScheduleVisit(node.InvocationId);
        }

        protected virtual void VisitSubgroupShuffleDownINTEL(SubgroupShuffleDownINTEL node)
        {
            ScheduleVisit(node.Current);
            ScheduleVisit(node.Next);
            ScheduleVisit(node.Delta);
        }

        protected virtual void VisitSubgroupShuffleUpINTEL(SubgroupShuffleUpINTEL node)
        {
            ScheduleVisit(node.Previous);
            ScheduleVisit(node.Current);
            ScheduleVisit(node.Delta);
        }

        protected virtual void VisitSubgroupShuffleXorINTEL(SubgroupShuffleXorINTEL node)
        {
            ScheduleVisit(node.Data);
            ScheduleVisit(node.Value);
        }

        protected virtual void VisitSubgroupBlockReadINTEL(SubgroupBlockReadINTEL node)
        {
            ScheduleVisit(node.Ptr);
        }

        protected virtual void VisitSubgroupBlockWriteINTEL(SubgroupBlockWriteINTEL node)
        {
            ScheduleVisit(node.Ptr);
            ScheduleVisit(node.Data);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitSubgroupImageBlockReadINTEL(SubgroupImageBlockReadINTEL node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Coordinate);
        }

        protected virtual void VisitSubgroupImageBlockWriteINTEL(SubgroupImageBlockWriteINTEL node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.Data);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitSubgroupImageMediaBlockReadINTEL(SubgroupImageMediaBlockReadINTEL node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.Width);
            ScheduleVisit(node.Height);
        }

        protected virtual void VisitSubgroupImageMediaBlockWriteINTEL(SubgroupImageMediaBlockWriteINTEL node)
        {
            ScheduleVisit(node.Image);
            ScheduleVisit(node.Coordinate);
            ScheduleVisit(node.Width);
            ScheduleVisit(node.Height);
            ScheduleVisit(node.Data);
            ScheduleVisit(node.Next);
        }

        protected virtual void VisitUCountLeadingZerosINTEL(UCountLeadingZerosINTEL node)
        {
            ScheduleVisit(node.Operand);
        }

        protected virtual void VisitUCountTrailingZerosINTEL(UCountTrailingZerosINTEL node)
        {
            ScheduleVisit(node.Operand);
        }

        protected virtual void VisitAbsISubINTEL(AbsISubINTEL node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitAbsUSubINTEL(AbsUSubINTEL node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitIAddSatINTEL(IAddSatINTEL node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitUAddSatINTEL(UAddSatINTEL node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitIAverageINTEL(IAverageINTEL node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitUAverageINTEL(UAverageINTEL node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitIAverageRoundedINTEL(IAverageRoundedINTEL node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitUAverageRoundedINTEL(UAverageRoundedINTEL node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitISubSatINTEL(ISubSatINTEL node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitUSubSatINTEL(USubSatINTEL node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitIMul32x16INTEL(IMul32x16INTEL node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitUMul32x16INTEL(UMul32x16INTEL node)
        {
            ScheduleVisit(node.Operand1);
            ScheduleVisit(node.Operand2);
        }

        protected virtual void VisitDecorateString(DecorateString node)
        {
            ScheduleVisit(node.Target);
        }

        protected virtual void VisitMemberDecorateString(MemberDecorateString node)
        {
            ScheduleVisit(node.StructType);
        }

        protected virtual void VisitVmeImageINTEL(VmeImageINTEL node)
        {
            ScheduleVisit(node.ImageType);
            ScheduleVisit(node.Sampler);
        }

        protected virtual void VisitTypeVmeImageINTEL(TypeVmeImageINTEL node)
        {
            VisitType(node);
            ScheduleVisit(node.ImageType);
        }

        protected virtual void VisitTypeAvcImePayloadINTEL(TypeAvcImePayloadINTEL node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeAvcRefPayloadINTEL(TypeAvcRefPayloadINTEL node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeAvcSicPayloadINTEL(TypeAvcSicPayloadINTEL node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeAvcMcePayloadINTEL(TypeAvcMcePayloadINTEL node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeAvcMceResultINTEL(TypeAvcMceResultINTEL node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeAvcImeResultINTEL(TypeAvcImeResultINTEL node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeAvcImeResultSingleReferenceStreamoutINTEL(TypeAvcImeResultSingleReferenceStreamoutINTEL node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeAvcImeResultDualReferenceStreamoutINTEL(TypeAvcImeResultDualReferenceStreamoutINTEL node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeAvcImeSingleReferenceStreaminINTEL(TypeAvcImeSingleReferenceStreaminINTEL node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeAvcImeDualReferenceStreaminINTEL(TypeAvcImeDualReferenceStreaminINTEL node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeAvcRefResultINTEL(TypeAvcRefResultINTEL node)
        {
            VisitType(node);
        }

        protected virtual void VisitTypeAvcSicResultINTEL(TypeAvcSicResultINTEL node)
        {
            VisitType(node);
        }

        protected virtual void VisitSubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL(SubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL node)
        {
            ScheduleVisit(node.SliceType);
            ScheduleVisit(node.Qp);
        }

        protected virtual void VisitSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL(SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL node)
        {
            ScheduleVisit(node.ReferenceBasePenalty);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetDefaultInterShapePenaltyINTEL(SubgroupAvcMceGetDefaultInterShapePenaltyINTEL node)
        {
            ScheduleVisit(node.SliceType);
            ScheduleVisit(node.Qp);
        }

        protected virtual void VisitSubgroupAvcMceSetInterShapePenaltyINTEL(SubgroupAvcMceSetInterShapePenaltyINTEL node)
        {
            ScheduleVisit(node.PackedShapePenalty);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL(SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL node)
        {
            ScheduleVisit(node.SliceType);
            ScheduleVisit(node.Qp);
        }

        protected virtual void VisitSubgroupAvcMceSetInterDirectionPenaltyINTEL(SubgroupAvcMceSetInterDirectionPenaltyINTEL node)
        {
            ScheduleVisit(node.DirectionCost);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL(SubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL node)
        {
            ScheduleVisit(node.SliceType);
            ScheduleVisit(node.Qp);
        }

        protected virtual void VisitSubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL(SubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL node)
        {
            ScheduleVisit(node.SliceType);
            ScheduleVisit(node.Qp);
        }

        protected virtual void VisitSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL(SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL node)
        {
        }

        protected virtual void VisitSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL(SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL node)
        {
        }

        protected virtual void VisitSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL(SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL node)
        {
        }

        protected virtual void VisitSubgroupAvcMceSetMotionVectorCostFunctionINTEL(SubgroupAvcMceSetMotionVectorCostFunctionINTEL node)
        {
            ScheduleVisit(node.PackedCostCenterDelta);
            ScheduleVisit(node.PackedCostTable);
            ScheduleVisit(node.CostPrecision);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL(SubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL node)
        {
            ScheduleVisit(node.SliceType);
            ScheduleVisit(node.Qp);
        }

        protected virtual void VisitSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL(SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL node)
        {
        }

        protected virtual void VisitSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL(SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL node)
        {
        }

        protected virtual void VisitSubgroupAvcMceSetAcOnlyHaarINTEL(SubgroupAvcMceSetAcOnlyHaarINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL(SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL node)
        {
            ScheduleVisit(node.SourceFieldPolarity);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL(SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL node)
        {
            ScheduleVisit(node.ReferenceFieldPolarity);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL(SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL node)
        {
            ScheduleVisit(node.ForwardReferenceFieldPolarity);
            ScheduleVisit(node.BackwardReferenceFieldPolarity);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceConvertToImePayloadINTEL(SubgroupAvcMceConvertToImePayloadINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceConvertToImeResultINTEL(SubgroupAvcMceConvertToImeResultINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceConvertToRefPayloadINTEL(SubgroupAvcMceConvertToRefPayloadINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceConvertToRefResultINTEL(SubgroupAvcMceConvertToRefResultINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceConvertToSicPayloadINTEL(SubgroupAvcMceConvertToSicPayloadINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceConvertToSicResultINTEL(SubgroupAvcMceConvertToSicResultINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetMotionVectorsINTEL(SubgroupAvcMceGetMotionVectorsINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetInterDistortionsINTEL(SubgroupAvcMceGetInterDistortionsINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetBestInterDistortionsINTEL(SubgroupAvcMceGetBestInterDistortionsINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetInterMajorShapeINTEL(SubgroupAvcMceGetInterMajorShapeINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetInterMinorShapeINTEL(SubgroupAvcMceGetInterMinorShapeINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetInterDirectionsINTEL(SubgroupAvcMceGetInterDirectionsINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetInterMotionVectorCountINTEL(SubgroupAvcMceGetInterMotionVectorCountINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetInterReferenceIdsINTEL(SubgroupAvcMceGetInterReferenceIdsINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL(SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL node)
        {
            ScheduleVisit(node.PackedReferenceIds);
            ScheduleVisit(node.PackedReferenceParameterFieldPolarities);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeInitializeINTEL(SubgroupAvcImeInitializeINTEL node)
        {
            ScheduleVisit(node.SrcCoord);
            ScheduleVisit(node.PartitionMask);
            ScheduleVisit(node.SADAdjustment);
        }

        protected virtual void VisitSubgroupAvcImeSetSingleReferenceINTEL(SubgroupAvcImeSetSingleReferenceINTEL node)
        {
            ScheduleVisit(node.RefOffset);
            ScheduleVisit(node.SearchWindowConfig);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeSetDualReferenceINTEL(SubgroupAvcImeSetDualReferenceINTEL node)
        {
            ScheduleVisit(node.FwdRefOffset);
            ScheduleVisit(node.BwdRefOffset);
            ScheduleVisit(node.SearchWindowConfig);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeRefWindowSizeINTEL(SubgroupAvcImeRefWindowSizeINTEL node)
        {
            ScheduleVisit(node.SearchWindowConfig);
            ScheduleVisit(node.DualRef);
        }

        protected virtual void VisitSubgroupAvcImeAdjustRefOffsetINTEL(SubgroupAvcImeAdjustRefOffsetINTEL node)
        {
            ScheduleVisit(node.RefOffset);
            ScheduleVisit(node.SrcCoord);
            ScheduleVisit(node.RefWindowSize);
            ScheduleVisit(node.ImageSize);
        }

        protected virtual void VisitSubgroupAvcImeConvertToMcePayloadINTEL(SubgroupAvcImeConvertToMcePayloadINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeSetMaxMotionVectorCountINTEL(SubgroupAvcImeSetMaxMotionVectorCountINTEL node)
        {
            ScheduleVisit(node.MaxMotionVectorCount);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeSetUnidirectionalMixDisableINTEL(SubgroupAvcImeSetUnidirectionalMixDisableINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeSetEarlySearchTerminationThresholdINTEL(SubgroupAvcImeSetEarlySearchTerminationThresholdINTEL node)
        {
            ScheduleVisit(node.Threshold);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeSetWeightedSadINTEL(SubgroupAvcImeSetWeightedSadINTEL node)
        {
            ScheduleVisit(node.PackedSadWeights);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeEvaluateWithSingleReferenceINTEL(SubgroupAvcImeEvaluateWithSingleReferenceINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.RefImage);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeEvaluateWithDualReferenceINTEL(SubgroupAvcImeEvaluateWithDualReferenceINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.FwdRefImage);
            ScheduleVisit(node.BwdRefImage);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL(SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.RefImage);
            ScheduleVisit(node.Payload);
            ScheduleVisit(node.StreaminComponents);
        }

        protected virtual void VisitSubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL(SubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.FwdRefImage);
            ScheduleVisit(node.BwdRefImage);
            ScheduleVisit(node.Payload);
            ScheduleVisit(node.StreaminComponents);
        }

        protected virtual void VisitSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL(SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.RefImage);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL(SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.FwdRefImage);
            ScheduleVisit(node.BwdRefImage);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL(SubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.RefImage);
            ScheduleVisit(node.Payload);
            ScheduleVisit(node.StreaminComponents);
        }

        protected virtual void VisitSubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL(SubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.FwdRefImage);
            ScheduleVisit(node.BwdRefImage);
            ScheduleVisit(node.Payload);
            ScheduleVisit(node.StreaminComponents);
        }

        protected virtual void VisitSubgroupAvcImeConvertToMceResultINTEL(SubgroupAvcImeConvertToMceResultINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeGetSingleReferenceStreaminINTEL(SubgroupAvcImeGetSingleReferenceStreaminINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeGetDualReferenceStreaminINTEL(SubgroupAvcImeGetDualReferenceStreaminINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeStripSingleReferenceStreamoutINTEL(SubgroupAvcImeStripSingleReferenceStreamoutINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeStripDualReferenceStreamoutINTEL(SubgroupAvcImeStripDualReferenceStreamoutINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL(SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL node)
        {
            ScheduleVisit(node.Payload);
            ScheduleVisit(node.MajorShape);
        }

        protected virtual void VisitSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL(SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL node)
        {
            ScheduleVisit(node.Payload);
            ScheduleVisit(node.MajorShape);
        }

        protected virtual void VisitSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL(SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL node)
        {
            ScheduleVisit(node.Payload);
            ScheduleVisit(node.MajorShape);
        }

        protected virtual void VisitSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL(SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL node)
        {
            ScheduleVisit(node.Payload);
            ScheduleVisit(node.MajorShape);
            ScheduleVisit(node.Direction);
        }

        protected virtual void VisitSubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL(SubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL node)
        {
            ScheduleVisit(node.Payload);
            ScheduleVisit(node.MajorShape);
            ScheduleVisit(node.Direction);
        }

        protected virtual void VisitSubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL(SubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL node)
        {
            ScheduleVisit(node.Payload);
            ScheduleVisit(node.MajorShape);
            ScheduleVisit(node.Direction);
        }

        protected virtual void VisitSubgroupAvcImeGetBorderReachedINTEL(SubgroupAvcImeGetBorderReachedINTEL node)
        {
            ScheduleVisit(node.ImageSelect);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeGetTruncatedSearchIndicationINTEL(SubgroupAvcImeGetTruncatedSearchIndicationINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL(SubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL(SubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL(SubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcFmeInitializeINTEL(SubgroupAvcFmeInitializeINTEL node)
        {
            ScheduleVisit(node.SrcCoord);
            ScheduleVisit(node.MotionVectors);
            ScheduleVisit(node.MajorShapes);
            ScheduleVisit(node.MinorShapes);
            ScheduleVisit(node.Direction);
            ScheduleVisit(node.PixelResolution);
            ScheduleVisit(node.SadAdjustment);
        }

        protected virtual void VisitSubgroupAvcBmeInitializeINTEL(SubgroupAvcBmeInitializeINTEL node)
        {
            ScheduleVisit(node.SrcCoord);
            ScheduleVisit(node.MotionVectors);
            ScheduleVisit(node.MajorShapes);
            ScheduleVisit(node.MinorShapes);
            ScheduleVisit(node.Direction);
            ScheduleVisit(node.PixelResolution);
            ScheduleVisit(node.BidirectionalWeight);
            ScheduleVisit(node.SadAdjustment);
        }

        protected virtual void VisitSubgroupAvcRefConvertToMcePayloadINTEL(SubgroupAvcRefConvertToMcePayloadINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcRefSetBidirectionalMixDisableINTEL(SubgroupAvcRefSetBidirectionalMixDisableINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcRefSetBilinearFilterEnableINTEL(SubgroupAvcRefSetBilinearFilterEnableINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcRefEvaluateWithSingleReferenceINTEL(SubgroupAvcRefEvaluateWithSingleReferenceINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.RefImage);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcRefEvaluateWithDualReferenceINTEL(SubgroupAvcRefEvaluateWithDualReferenceINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.FwdRefImage);
            ScheduleVisit(node.BwdRefImage);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcRefEvaluateWithMultiReferenceINTEL(SubgroupAvcRefEvaluateWithMultiReferenceINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.PackedReferenceIds);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL(SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.PackedReferenceIds);
            ScheduleVisit(node.PackedReferenceFieldPolarities);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcRefConvertToMceResultINTEL(SubgroupAvcRefConvertToMceResultINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicInitializeINTEL(SubgroupAvcSicInitializeINTEL node)
        {
            ScheduleVisit(node.SrcCoord);
        }

        protected virtual void VisitSubgroupAvcSicConfigureSkcINTEL(SubgroupAvcSicConfigureSkcINTEL node)
        {
            ScheduleVisit(node.SkipBlockPartitionType);
            ScheduleVisit(node.SkipMotionVectorMask);
            ScheduleVisit(node.MotionVectors);
            ScheduleVisit(node.BidirectionalWeight);
            ScheduleVisit(node.SadAdjustment);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicConfigureIpeLumaINTEL(SubgroupAvcSicConfigureIpeLumaINTEL node)
        {
            ScheduleVisit(node.LumaIntraPartitionMask);
            ScheduleVisit(node.IntraNeighbourAvailabilty);
            ScheduleVisit(node.LeftEdgeLumaPixels);
            ScheduleVisit(node.UpperLeftCornerLumaPixel);
            ScheduleVisit(node.UpperEdgeLumaPixels);
            ScheduleVisit(node.UpperRightEdgeLumaPixels);
            ScheduleVisit(node.SadAdjustment);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicConfigureIpeLumaChromaINTEL(SubgroupAvcSicConfigureIpeLumaChromaINTEL node)
        {
            ScheduleVisit(node.LumaIntraPartitionMask);
            ScheduleVisit(node.IntraNeighbourAvailabilty);
            ScheduleVisit(node.LeftEdgeLumaPixels);
            ScheduleVisit(node.UpperLeftCornerLumaPixel);
            ScheduleVisit(node.UpperEdgeLumaPixels);
            ScheduleVisit(node.UpperRightEdgeLumaPixels);
            ScheduleVisit(node.LeftEdgeChromaPixels);
            ScheduleVisit(node.UpperLeftCornerChromaPixel);
            ScheduleVisit(node.UpperEdgeChromaPixels);
            ScheduleVisit(node.SadAdjustment);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicGetMotionVectorMaskINTEL(SubgroupAvcSicGetMotionVectorMaskINTEL node)
        {
            ScheduleVisit(node.SkipBlockPartitionType);
            ScheduleVisit(node.Direction);
        }

        protected virtual void VisitSubgroupAvcSicConvertToMcePayloadINTEL(SubgroupAvcSicConvertToMcePayloadINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicSetIntraLumaShapePenaltyINTEL(SubgroupAvcSicSetIntraLumaShapePenaltyINTEL node)
        {
            ScheduleVisit(node.PackedShapePenalty);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL(SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL node)
        {
            ScheduleVisit(node.LumaModePenalty);
            ScheduleVisit(node.LumaPackedNeighborModes);
            ScheduleVisit(node.LumaPackedNonDcPenalty);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL(SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL node)
        {
            ScheduleVisit(node.ChromaModeBasePenalty);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicSetBilinearFilterEnableINTEL(SubgroupAvcSicSetBilinearFilterEnableINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicSetSkcForwardTransformEnableINTEL(SubgroupAvcSicSetSkcForwardTransformEnableINTEL node)
        {
            ScheduleVisit(node.PackedSadCoefficients);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicSetBlockBasedRawSkipSadINTEL(SubgroupAvcSicSetBlockBasedRawSkipSadINTEL node)
        {
            ScheduleVisit(node.BlockBasedSkipType);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicEvaluateIpeINTEL(SubgroupAvcSicEvaluateIpeINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicEvaluateWithSingleReferenceINTEL(SubgroupAvcSicEvaluateWithSingleReferenceINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.RefImage);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicEvaluateWithDualReferenceINTEL(SubgroupAvcSicEvaluateWithDualReferenceINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.FwdRefImage);
            ScheduleVisit(node.BwdRefImage);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicEvaluateWithMultiReferenceINTEL(SubgroupAvcSicEvaluateWithMultiReferenceINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.PackedReferenceIds);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL(SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL node)
        {
            ScheduleVisit(node.SrcImage);
            ScheduleVisit(node.PackedReferenceIds);
            ScheduleVisit(node.PackedReferenceFieldPolarities);
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicConvertToMceResultINTEL(SubgroupAvcSicConvertToMceResultINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicGetIpeLumaShapeINTEL(SubgroupAvcSicGetIpeLumaShapeINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicGetBestIpeLumaDistortionINTEL(SubgroupAvcSicGetBestIpeLumaDistortionINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicGetBestIpeChromaDistortionINTEL(SubgroupAvcSicGetBestIpeChromaDistortionINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicGetPackedIpeLumaModesINTEL(SubgroupAvcSicGetPackedIpeLumaModesINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicGetIpeChromaModeINTEL(SubgroupAvcSicGetIpeChromaModeINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL(SubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL(SubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL node)
        {
            ScheduleVisit(node.Payload);
        }

        protected virtual void VisitSubgroupAvcSicGetInterRawSadsINTEL(SubgroupAvcSicGetInterRawSadsINTEL node)
        {
            ScheduleVisit(node.Payload);
        }
    }
}