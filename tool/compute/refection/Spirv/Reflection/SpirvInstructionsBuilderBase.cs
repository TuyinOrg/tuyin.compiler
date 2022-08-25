using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Nodes;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection
{
    internal abstract class SpirvInstructionsBuilderBase
    {
        protected readonly List<InstructionWithId> _results = new List<InstructionWithId>();

        protected readonly Dictionary<Node, Instruction> _instructionMap = new Dictionary<Node, Instruction>();

        internal virtual Instruction Visit(Node node)
        {
            if (node == null) return null;
            if (_instructionMap.TryGetValue(node, out var instruction)) return instruction;
            switch (node.OpCode)
            {
                case Op.OpNop: return VisitNop((Nodes.Nop) node);
                case Op.OpUndef: return VisitUndef((Undef) node);
                case Op.OpSourceContinued: return VisitSourceContinued((Nodes.SourceContinued) node);
                case Op.OpSource: return VisitSource((Nodes.Source) node);
                case Op.OpSourceExtension: return VisitSourceExtension((Nodes.SourceExtension) node);
                case Op.OpName: return VisitName((Nodes.Name) node);
                case Op.OpMemberName: return VisitMemberName((Nodes.MemberName) node);
                case Op.OpString: return VisitString((Nodes.String) node);
                case Op.OpLine: return VisitLine((Nodes.Line) node);
                case Op.OpExtension: return VisitExtension((Nodes.Extension) node);
                case Op.OpExtInstImport: return VisitExtInstImport((Nodes.ExtInstImport) node);
                case Op.OpExtInst: return VisitExtInst((ExtInst) node);
                case Op.OpMemoryModel: return VisitMemoryModel((Nodes.MemoryModel) node);
                case Op.OpEntryPoint: return VisitEntryPoint((Nodes.EntryPoint) node);
                case Op.OpExecutionMode: return VisitExecutionMode((Nodes.ExecutionMode) node);
                case Op.OpCapability: return VisitCapability((Nodes.Capability) node);
                case Op.OpTypeVoid: return VisitTypeVoid((Types.TypeVoid)node);
                case Op.OpTypeBool: return VisitTypeBool((Types.TypeBool)node);
                case Op.OpTypeInt: return VisitTypeInt((Types.TypeInt)node);
                case Op.OpTypeFloat: return VisitTypeFloat((Types.TypeFloat)node);
                case Op.OpTypeVector: return VisitTypeVector((Types.TypeVector)node);
                case Op.OpTypeMatrix: return VisitTypeMatrix((Types.TypeMatrix)node);
                case Op.OpTypeImage: return VisitTypeImage((Types.TypeImage)node);
                case Op.OpTypeSampler: return VisitTypeSampler((Types.TypeSampler)node);
                case Op.OpTypeSampledImage: return VisitTypeSampledImage((Types.TypeSampledImage)node);
                case Op.OpTypeArray: return VisitTypeArray((Types.TypeArray)node);
                case Op.OpTypeRuntimeArray: return VisitTypeRuntimeArray((Types.TypeRuntimeArray)node);
                case Op.OpTypeStruct: return VisitTypeStruct((Types.TypeStruct)node);
                case Op.OpTypeOpaque: return VisitTypeOpaque((Types.TypeOpaque)node);
                case Op.OpTypePointer: return VisitTypePointer((Types.TypePointer)node);
                case Op.OpTypeFunction: return VisitTypeFunction((Types.TypeFunction)node);
                case Op.OpTypeEvent: return VisitTypeEvent((Types.TypeEvent)node);
                case Op.OpTypeDeviceEvent: return VisitTypeDeviceEvent((Types.TypeDeviceEvent)node);
                case Op.OpTypeReserveId: return VisitTypeReserveId((Types.TypeReserveId)node);
                case Op.OpTypeQueue: return VisitTypeQueue((Types.TypeQueue)node);
                case Op.OpTypePipe: return VisitTypePipe((Types.TypePipe)node);
                case Op.OpTypeForwardPointer: return VisitTypeForwardPointer((Types.TypeForwardPointer)node);
                case Op.OpConstantTrue: return VisitConstantTrue((ConstantTrue) node);
                case Op.OpConstantFalse: return VisitConstantFalse((ConstantFalse) node);
                case Op.OpConstant: return VisitConstant((Constant) node);
                case Op.OpConstantComposite: return VisitConstantComposite((ConstantComposite) node);
                case Op.OpConstantSampler: return VisitConstantSampler((ConstantSampler) node);
                case Op.OpConstantNull: return VisitConstantNull((ConstantNull) node);
                case Op.OpSpecConstantTrue: return VisitSpecConstantTrue((SpecConstantTrue) node);
                case Op.OpSpecConstantFalse: return VisitSpecConstantFalse((SpecConstantFalse) node);
                case Op.OpSpecConstant: return VisitSpecConstant((SpecConstant) node);
                case Op.OpSpecConstantComposite: return VisitSpecConstantComposite((SpecConstantComposite) node);
                case Op.OpSpecConstantOp: return VisitSpecConstantOp((SpecConstantOp) node);
                case Op.OpFunction: return VisitFunction((Function) node);
                case Op.OpFunctionParameter: return VisitFunctionParameter((FunctionParameter) node);
                case Op.OpFunctionEnd: return VisitFunctionEnd((FunctionEnd) node);
                case Op.OpFunctionCall: return VisitFunctionCall((FunctionCall) node);
                case Op.OpVariable: return VisitVariable((Variable) node);
                case Op.OpImageTexelPointer: return VisitImageTexelPointer((ImageTexelPointer) node);
                case Op.OpLoad: return VisitLoad((Load) node);
                case Op.OpStore: return VisitStore((Store) node);
                case Op.OpCopyMemory: return VisitCopyMemory((CopyMemory) node);
                case Op.OpCopyMemorySized: return VisitCopyMemorySized((CopyMemorySized) node);
                case Op.OpAccessChain: return VisitAccessChain((AccessChain) node);
                case Op.OpInBoundsAccessChain: return VisitInBoundsAccessChain((InBoundsAccessChain) node);
                case Op.OpPtrAccessChain: return VisitPtrAccessChain((PtrAccessChain) node);
                case Op.OpArrayLength: return VisitArrayLength((ArrayLength) node);
                case Op.OpGenericPtrMemSemantics: return VisitGenericPtrMemSemantics((GenericPtrMemSemantics) node);
                case Op.OpInBoundsPtrAccessChain: return VisitInBoundsPtrAccessChain((InBoundsPtrAccessChain) node);
                case Op.OpDecorate: return VisitDecorate((Nodes.Decorate) node);
                case Op.OpMemberDecorate: return VisitMemberDecorate((Nodes.MemberDecorate) node);
                case Op.OpDecorationGroup: return VisitDecorationGroup((Nodes.DecorationGroup) node);
                case Op.OpGroupDecorate: return VisitGroupDecorate((Nodes.GroupDecorate) node);
                case Op.OpGroupMemberDecorate: return VisitGroupMemberDecorate((Nodes.GroupMemberDecorate) node);
                case Op.OpVectorExtractDynamic: return VisitVectorExtractDynamic((VectorExtractDynamic) node);
                case Op.OpVectorInsertDynamic: return VisitVectorInsertDynamic((VectorInsertDynamic) node);
                case Op.OpVectorShuffle: return VisitVectorShuffle((VectorShuffle) node);
                case Op.OpCompositeConstruct: return VisitCompositeConstruct((CompositeConstruct) node);
                case Op.OpCompositeExtract: return VisitCompositeExtract((CompositeExtract) node);
                case Op.OpCompositeInsert: return VisitCompositeInsert((CompositeInsert) node);
                case Op.OpCopyObject: return VisitCopyObject((CopyObject) node);
                case Op.OpTranspose: return VisitTranspose((Transpose) node);
                case Op.OpSampledImage: return VisitSampledImage((SampledImage) node);
                case Op.OpImageSampleImplicitLod: return VisitImageSampleImplicitLod((ImageSampleImplicitLod) node);
                case Op.OpImageSampleExplicitLod: return VisitImageSampleExplicitLod((ImageSampleExplicitLod) node);
                case Op.OpImageSampleDrefImplicitLod: return VisitImageSampleDrefImplicitLod((ImageSampleDrefImplicitLod) node);
                case Op.OpImageSampleDrefExplicitLod: return VisitImageSampleDrefExplicitLod((ImageSampleDrefExplicitLod) node);
                case Op.OpImageSampleProjImplicitLod: return VisitImageSampleProjImplicitLod((ImageSampleProjImplicitLod) node);
                case Op.OpImageSampleProjExplicitLod: return VisitImageSampleProjExplicitLod((ImageSampleProjExplicitLod) node);
                case Op.OpImageSampleProjDrefImplicitLod: return VisitImageSampleProjDrefImplicitLod((ImageSampleProjDrefImplicitLod) node);
                case Op.OpImageSampleProjDrefExplicitLod: return VisitImageSampleProjDrefExplicitLod((ImageSampleProjDrefExplicitLod) node);
                case Op.OpImageFetch: return VisitImageFetch((ImageFetch) node);
                case Op.OpImageGather: return VisitImageGather((ImageGather) node);
                case Op.OpImageDrefGather: return VisitImageDrefGather((ImageDrefGather) node);
                case Op.OpImageRead: return VisitImageRead((ImageRead) node);
                case Op.OpImageWrite: return VisitImageWrite((ImageWrite) node);
                case Op.OpImage: return VisitImage((Image) node);
                case Op.OpImageQueryFormat: return VisitImageQueryFormat((ImageQueryFormat) node);
                case Op.OpImageQueryOrder: return VisitImageQueryOrder((ImageQueryOrder) node);
                case Op.OpImageQuerySizeLod: return VisitImageQuerySizeLod((ImageQuerySizeLod) node);
                case Op.OpImageQuerySize: return VisitImageQuerySize((ImageQuerySize) node);
                case Op.OpImageQueryLod: return VisitImageQueryLod((ImageQueryLod) node);
                case Op.OpImageQueryLevels: return VisitImageQueryLevels((ImageQueryLevels) node);
                case Op.OpImageQuerySamples: return VisitImageQuerySamples((ImageQuerySamples) node);
                case Op.OpConvertFToU: return VisitConvertFToU((ConvertFToU) node);
                case Op.OpConvertFToS: return VisitConvertFToS((ConvertFToS) node);
                case Op.OpConvertSToF: return VisitConvertSToF((ConvertSToF) node);
                case Op.OpConvertUToF: return VisitConvertUToF((ConvertUToF) node);
                case Op.OpUConvert: return VisitUConvert((UConvert) node);
                case Op.OpSConvert: return VisitSConvert((SConvert) node);
                case Op.OpFConvert: return VisitFConvert((FConvert) node);
                case Op.OpQuantizeToF16: return VisitQuantizeToF16((QuantizeToF16) node);
                case Op.OpConvertPtrToU: return VisitConvertPtrToU((ConvertPtrToU) node);
                case Op.OpSatConvertSToU: return VisitSatConvertSToU((SatConvertSToU) node);
                case Op.OpSatConvertUToS: return VisitSatConvertUToS((SatConvertUToS) node);
                case Op.OpConvertUToPtr: return VisitConvertUToPtr((ConvertUToPtr) node);
                case Op.OpPtrCastToGeneric: return VisitPtrCastToGeneric((PtrCastToGeneric) node);
                case Op.OpGenericCastToPtr: return VisitGenericCastToPtr((GenericCastToPtr) node);
                case Op.OpGenericCastToPtrExplicit: return VisitGenericCastToPtrExplicit((GenericCastToPtrExplicit) node);
                case Op.OpBitcast: return VisitBitcast((Bitcast) node);
                case Op.OpSNegate: return VisitSNegate((SNegate) node);
                case Op.OpFNegate: return VisitFNegate((FNegate) node);
                case Op.OpIAdd: return VisitIAdd((IAdd) node);
                case Op.OpFAdd: return VisitFAdd((FAdd) node);
                case Op.OpISub: return VisitISub((ISub) node);
                case Op.OpFSub: return VisitFSub((FSub) node);
                case Op.OpIMul: return VisitIMul((IMul) node);
                case Op.OpFMul: return VisitFMul((FMul) node);
                case Op.OpUDiv: return VisitUDiv((UDiv) node);
                case Op.OpSDiv: return VisitSDiv((SDiv) node);
                case Op.OpFDiv: return VisitFDiv((FDiv) node);
                case Op.OpUMod: return VisitUMod((UMod) node);
                case Op.OpSRem: return VisitSRem((SRem) node);
                case Op.OpSMod: return VisitSMod((SMod) node);
                case Op.OpFRem: return VisitFRem((FRem) node);
                case Op.OpFMod: return VisitFMod((FMod) node);
                case Op.OpVectorTimesScalar: return VisitVectorTimesScalar((VectorTimesScalar) node);
                case Op.OpMatrixTimesScalar: return VisitMatrixTimesScalar((MatrixTimesScalar) node);
                case Op.OpVectorTimesMatrix: return VisitVectorTimesMatrix((VectorTimesMatrix) node);
                case Op.OpMatrixTimesVector: return VisitMatrixTimesVector((MatrixTimesVector) node);
                case Op.OpMatrixTimesMatrix: return VisitMatrixTimesMatrix((MatrixTimesMatrix) node);
                case Op.OpOuterProduct: return VisitOuterProduct((OuterProduct) node);
                case Op.OpDot: return VisitDot((Dot) node);
                case Op.OpIAddCarry: return VisitIAddCarry((IAddCarry) node);
                case Op.OpISubBorrow: return VisitISubBorrow((ISubBorrow) node);
                case Op.OpUMulExtended: return VisitUMulExtended((UMulExtended) node);
                case Op.OpSMulExtended: return VisitSMulExtended((SMulExtended) node);
                case Op.OpAny: return VisitAny((Any) node);
                case Op.OpAll: return VisitAll((All) node);
                case Op.OpIsNan: return VisitIsNan((IsNan) node);
                case Op.OpIsInf: return VisitIsInf((IsInf) node);
                case Op.OpIsFinite: return VisitIsFinite((IsFinite) node);
                case Op.OpIsNormal: return VisitIsNormal((IsNormal) node);
                case Op.OpSignBitSet: return VisitSignBitSet((SignBitSet) node);
                case Op.OpLessOrGreater: return VisitLessOrGreater((LessOrGreater) node);
                case Op.OpOrdered: return VisitOrdered((Ordered) node);
                case Op.OpUnordered: return VisitUnordered((Unordered) node);
                case Op.OpLogicalEqual: return VisitLogicalEqual((LogicalEqual) node);
                case Op.OpLogicalNotEqual: return VisitLogicalNotEqual((LogicalNotEqual) node);
                case Op.OpLogicalOr: return VisitLogicalOr((LogicalOr) node);
                case Op.OpLogicalAnd: return VisitLogicalAnd((LogicalAnd) node);
                case Op.OpLogicalNot: return VisitLogicalNot((LogicalNot) node);
                case Op.OpSelect: return VisitSelect((Select) node);
                case Op.OpIEqual: return VisitIEqual((IEqual) node);
                case Op.OpINotEqual: return VisitINotEqual((INotEqual) node);
                case Op.OpUGreaterThan: return VisitUGreaterThan((UGreaterThan) node);
                case Op.OpSGreaterThan: return VisitSGreaterThan((SGreaterThan) node);
                case Op.OpUGreaterThanEqual: return VisitUGreaterThanEqual((UGreaterThanEqual) node);
                case Op.OpSGreaterThanEqual: return VisitSGreaterThanEqual((SGreaterThanEqual) node);
                case Op.OpULessThan: return VisitULessThan((ULessThan) node);
                case Op.OpSLessThan: return VisitSLessThan((SLessThan) node);
                case Op.OpULessThanEqual: return VisitULessThanEqual((ULessThanEqual) node);
                case Op.OpSLessThanEqual: return VisitSLessThanEqual((SLessThanEqual) node);
                case Op.OpFOrdEqual: return VisitFOrdEqual((FOrdEqual) node);
                case Op.OpFUnordEqual: return VisitFUnordEqual((FUnordEqual) node);
                case Op.OpFOrdNotEqual: return VisitFOrdNotEqual((FOrdNotEqual) node);
                case Op.OpFUnordNotEqual: return VisitFUnordNotEqual((FUnordNotEqual) node);
                case Op.OpFOrdLessThan: return VisitFOrdLessThan((FOrdLessThan) node);
                case Op.OpFUnordLessThan: return VisitFUnordLessThan((FUnordLessThan) node);
                case Op.OpFOrdGreaterThan: return VisitFOrdGreaterThan((FOrdGreaterThan) node);
                case Op.OpFUnordGreaterThan: return VisitFUnordGreaterThan((FUnordGreaterThan) node);
                case Op.OpFOrdLessThanEqual: return VisitFOrdLessThanEqual((FOrdLessThanEqual) node);
                case Op.OpFUnordLessThanEqual: return VisitFUnordLessThanEqual((FUnordLessThanEqual) node);
                case Op.OpFOrdGreaterThanEqual: return VisitFOrdGreaterThanEqual((FOrdGreaterThanEqual) node);
                case Op.OpFUnordGreaterThanEqual: return VisitFUnordGreaterThanEqual((FUnordGreaterThanEqual) node);
                case Op.OpShiftRightLogical: return VisitShiftRightLogical((ShiftRightLogical) node);
                case Op.OpShiftRightArithmetic: return VisitShiftRightArithmetic((ShiftRightArithmetic) node);
                case Op.OpShiftLeftLogical: return VisitShiftLeftLogical((ShiftLeftLogical) node);
                case Op.OpBitwiseOr: return VisitBitwiseOr((BitwiseOr) node);
                case Op.OpBitwiseXor: return VisitBitwiseXor((BitwiseXor) node);
                case Op.OpBitwiseAnd: return VisitBitwiseAnd((BitwiseAnd) node);
                case Op.OpNot: return VisitNot((Not) node);
                case Op.OpBitFieldInsert: return VisitBitFieldInsert((BitFieldInsert) node);
                case Op.OpBitFieldSExtract: return VisitBitFieldSExtract((BitFieldSExtract) node);
                case Op.OpBitFieldUExtract: return VisitBitFieldUExtract((BitFieldUExtract) node);
                case Op.OpBitReverse: return VisitBitReverse((BitReverse) node);
                case Op.OpBitCount: return VisitBitCount((BitCount) node);
                case Op.OpDPdx: return VisitDPdx((DPdx) node);
                case Op.OpDPdy: return VisitDPdy((DPdy) node);
                case Op.OpFwidth: return VisitFwidth((Fwidth) node);
                case Op.OpDPdxFine: return VisitDPdxFine((DPdxFine) node);
                case Op.OpDPdyFine: return VisitDPdyFine((DPdyFine) node);
                case Op.OpFwidthFine: return VisitFwidthFine((FwidthFine) node);
                case Op.OpDPdxCoarse: return VisitDPdxCoarse((DPdxCoarse) node);
                case Op.OpDPdyCoarse: return VisitDPdyCoarse((DPdyCoarse) node);
                case Op.OpFwidthCoarse: return VisitFwidthCoarse((FwidthCoarse) node);
                case Op.OpEmitVertex: return VisitEmitVertex((EmitVertex) node);
                case Op.OpEndPrimitive: return VisitEndPrimitive((EndPrimitive) node);
                case Op.OpEmitStreamVertex: return VisitEmitStreamVertex((EmitStreamVertex) node);
                case Op.OpEndStreamPrimitive: return VisitEndStreamPrimitive((EndStreamPrimitive) node);
                case Op.OpControlBarrier: return VisitControlBarrier((ControlBarrier) node);
                case Op.OpMemoryBarrier: return VisitMemoryBarrier((MemoryBarrier) node);
                case Op.OpAtomicLoad: return VisitAtomicLoad((AtomicLoad) node);
                case Op.OpAtomicStore: return VisitAtomicStore((AtomicStore) node);
                case Op.OpAtomicExchange: return VisitAtomicExchange((AtomicExchange) node);
                case Op.OpAtomicCompareExchange: return VisitAtomicCompareExchange((AtomicCompareExchange) node);
                case Op.OpAtomicCompareExchangeWeak: return VisitAtomicCompareExchangeWeak((AtomicCompareExchangeWeak) node);
                case Op.OpAtomicIIncrement: return VisitAtomicIIncrement((AtomicIIncrement) node);
                case Op.OpAtomicIDecrement: return VisitAtomicIDecrement((AtomicIDecrement) node);
                case Op.OpAtomicIAdd: return VisitAtomicIAdd((AtomicIAdd) node);
                case Op.OpAtomicISub: return VisitAtomicISub((AtomicISub) node);
                case Op.OpAtomicSMin: return VisitAtomicSMin((AtomicSMin) node);
                case Op.OpAtomicUMin: return VisitAtomicUMin((AtomicUMin) node);
                case Op.OpAtomicSMax: return VisitAtomicSMax((AtomicSMax) node);
                case Op.OpAtomicUMax: return VisitAtomicUMax((AtomicUMax) node);
                case Op.OpAtomicAnd: return VisitAtomicAnd((AtomicAnd) node);
                case Op.OpAtomicOr: return VisitAtomicOr((AtomicOr) node);
                case Op.OpAtomicXor: return VisitAtomicXor((AtomicXor) node);
                case Op.OpPhi: return VisitPhi((Phi) node);
                case Op.OpLoopMerge: return VisitLoopMerge((LoopMerge) node);
                case Op.OpSelectionMerge: return VisitSelectionMerge((SelectionMerge) node);
                case Op.OpLabel: return VisitLabel((Label) node);
                case Op.OpBranch: return VisitBranch((Branch) node);
                case Op.OpBranchConditional: return VisitBranchConditional((BranchConditional) node);
                case Op.OpSwitch: return VisitSwitch((Switch) node);
                case Op.OpKill: return VisitKill((Kill) node);
                case Op.OpReturn: return VisitReturn((Return) node);
                case Op.OpReturnValue: return VisitReturnValue((ReturnValue) node);
                case Op.OpUnreachable: return VisitUnreachable((Unreachable) node);
                case Op.OpLifetimeStart: return VisitLifetimeStart((LifetimeStart) node);
                case Op.OpLifetimeStop: return VisitLifetimeStop((LifetimeStop) node);
                case Op.OpGroupAsyncCopy: return VisitGroupAsyncCopy((GroupAsyncCopy) node);
                case Op.OpGroupWaitEvents: return VisitGroupWaitEvents((GroupWaitEvents) node);
                case Op.OpGroupAll: return VisitGroupAll((GroupAll) node);
                case Op.OpGroupAny: return VisitGroupAny((GroupAny) node);
                case Op.OpGroupBroadcast: return VisitGroupBroadcast((GroupBroadcast) node);
                case Op.OpGroupIAdd: return VisitGroupIAdd((GroupIAdd) node);
                case Op.OpGroupFAdd: return VisitGroupFAdd((GroupFAdd) node);
                case Op.OpGroupFMin: return VisitGroupFMin((GroupFMin) node);
                case Op.OpGroupUMin: return VisitGroupUMin((GroupUMin) node);
                case Op.OpGroupSMin: return VisitGroupSMin((GroupSMin) node);
                case Op.OpGroupFMax: return VisitGroupFMax((GroupFMax) node);
                case Op.OpGroupUMax: return VisitGroupUMax((GroupUMax) node);
                case Op.OpGroupSMax: return VisitGroupSMax((GroupSMax) node);
                case Op.OpReadPipe: return VisitReadPipe((ReadPipe) node);
                case Op.OpWritePipe: return VisitWritePipe((WritePipe) node);
                case Op.OpReservedReadPipe: return VisitReservedReadPipe((ReservedReadPipe) node);
                case Op.OpReservedWritePipe: return VisitReservedWritePipe((ReservedWritePipe) node);
                case Op.OpReserveReadPipePackets: return VisitReserveReadPipePackets((ReserveReadPipePackets) node);
                case Op.OpReserveWritePipePackets: return VisitReserveWritePipePackets((ReserveWritePipePackets) node);
                case Op.OpCommitReadPipe: return VisitCommitReadPipe((CommitReadPipe) node);
                case Op.OpCommitWritePipe: return VisitCommitWritePipe((CommitWritePipe) node);
                case Op.OpIsValidReserveId: return VisitIsValidReserveId((IsValidReserveId) node);
                case Op.OpGetNumPipePackets: return VisitGetNumPipePackets((GetNumPipePackets) node);
                case Op.OpGetMaxPipePackets: return VisitGetMaxPipePackets((GetMaxPipePackets) node);
                case Op.OpGroupReserveReadPipePackets: return VisitGroupReserveReadPipePackets((GroupReserveReadPipePackets) node);
                case Op.OpGroupReserveWritePipePackets: return VisitGroupReserveWritePipePackets((GroupReserveWritePipePackets) node);
                case Op.OpGroupCommitReadPipe: return VisitGroupCommitReadPipe((GroupCommitReadPipe) node);
                case Op.OpGroupCommitWritePipe: return VisitGroupCommitWritePipe((GroupCommitWritePipe) node);
                case Op.OpEnqueueMarker: return VisitEnqueueMarker((EnqueueMarker) node);
                case Op.OpEnqueueKernel: return VisitEnqueueKernel((EnqueueKernel) node);
                case Op.OpGetKernelNDrangeSubGroupCount: return VisitGetKernelNDrangeSubGroupCount((GetKernelNDrangeSubGroupCount) node);
                case Op.OpGetKernelNDrangeMaxSubGroupSize: return VisitGetKernelNDrangeMaxSubGroupSize((GetKernelNDrangeMaxSubGroupSize) node);
                case Op.OpGetKernelWorkGroupSize: return VisitGetKernelWorkGroupSize((GetKernelWorkGroupSize) node);
                case Op.OpGetKernelPreferredWorkGroupSizeMultiple: return VisitGetKernelPreferredWorkGroupSizeMultiple((GetKernelPreferredWorkGroupSizeMultiple) node);
                case Op.OpRetainEvent: return VisitRetainEvent((RetainEvent) node);
                case Op.OpReleaseEvent: return VisitReleaseEvent((ReleaseEvent) node);
                case Op.OpCreateUserEvent: return VisitCreateUserEvent((CreateUserEvent) node);
                case Op.OpIsValidEvent: return VisitIsValidEvent((IsValidEvent) node);
                case Op.OpSetUserEventStatus: return VisitSetUserEventStatus((SetUserEventStatus) node);
                case Op.OpCaptureEventProfilingInfo: return VisitCaptureEventProfilingInfo((CaptureEventProfilingInfo) node);
                case Op.OpGetDefaultQueue: return VisitGetDefaultQueue((GetDefaultQueue) node);
                case Op.OpBuildNDRange: return VisitBuildNDRange((BuildNDRange) node);
                case Op.OpImageSparseSampleImplicitLod: return VisitImageSparseSampleImplicitLod((ImageSparseSampleImplicitLod) node);
                case Op.OpImageSparseSampleExplicitLod: return VisitImageSparseSampleExplicitLod((ImageSparseSampleExplicitLod) node);
                case Op.OpImageSparseSampleDrefImplicitLod: return VisitImageSparseSampleDrefImplicitLod((ImageSparseSampleDrefImplicitLod) node);
                case Op.OpImageSparseSampleDrefExplicitLod: return VisitImageSparseSampleDrefExplicitLod((ImageSparseSampleDrefExplicitLod) node);
                case Op.OpImageSparseSampleProjImplicitLod: return VisitImageSparseSampleProjImplicitLod((ImageSparseSampleProjImplicitLod) node);
                case Op.OpImageSparseSampleProjExplicitLod: return VisitImageSparseSampleProjExplicitLod((ImageSparseSampleProjExplicitLod) node);
                case Op.OpImageSparseSampleProjDrefImplicitLod: return VisitImageSparseSampleProjDrefImplicitLod((ImageSparseSampleProjDrefImplicitLod) node);
                case Op.OpImageSparseSampleProjDrefExplicitLod: return VisitImageSparseSampleProjDrefExplicitLod((ImageSparseSampleProjDrefExplicitLod) node);
                case Op.OpImageSparseFetch: return VisitImageSparseFetch((ImageSparseFetch) node);
                case Op.OpImageSparseGather: return VisitImageSparseGather((ImageSparseGather) node);
                case Op.OpImageSparseDrefGather: return VisitImageSparseDrefGather((ImageSparseDrefGather) node);
                case Op.OpImageSparseTexelsResident: return VisitImageSparseTexelsResident((ImageSparseTexelsResident) node);
                case Op.OpNoLine: return VisitNoLine((NoLine) node);
                case Op.OpAtomicFlagTestAndSet: return VisitAtomicFlagTestAndSet((AtomicFlagTestAndSet) node);
                case Op.OpAtomicFlagClear: return VisitAtomicFlagClear((AtomicFlagClear) node);
                case Op.OpImageSparseRead: return VisitImageSparseRead((ImageSparseRead) node);
                case Op.OpSizeOf: return VisitSizeOf((SizeOf) node);
                case Op.OpTypePipeStorage: return VisitTypePipeStorage((Types.TypePipeStorage)node);
                case Op.OpConstantPipeStorage: return VisitConstantPipeStorage((ConstantPipeStorage) node);
                case Op.OpCreatePipeFromPipeStorage: return VisitCreatePipeFromPipeStorage((CreatePipeFromPipeStorage) node);
                case Op.OpGetKernelLocalSizeForSubgroupCount: return VisitGetKernelLocalSizeForSubgroupCount((GetKernelLocalSizeForSubgroupCount) node);
                case Op.OpGetKernelMaxNumSubgroups: return VisitGetKernelMaxNumSubgroups((GetKernelMaxNumSubgroups) node);
                case Op.OpTypeNamedBarrier: return VisitTypeNamedBarrier((Types.TypeNamedBarrier)node);
                case Op.OpNamedBarrierInitialize: return VisitNamedBarrierInitialize((NamedBarrierInitialize) node);
                case Op.OpMemoryNamedBarrier: return VisitMemoryNamedBarrier((MemoryNamedBarrier) node);
                case Op.OpModuleProcessed: return VisitModuleProcessed((ModuleProcessed) node);
                case Op.OpExecutionModeId: return VisitExecutionModeId((ExecutionModeId) node);
                case Op.OpDecorateId: return VisitDecorateId((Nodes.DecorateId) node);
                case Op.OpGroupNonUniformElect: return VisitGroupNonUniformElect((GroupNonUniformElect) node);
                case Op.OpGroupNonUniformAll: return VisitGroupNonUniformAll((GroupNonUniformAll) node);
                case Op.OpGroupNonUniformAny: return VisitGroupNonUniformAny((GroupNonUniformAny) node);
                case Op.OpGroupNonUniformAllEqual: return VisitGroupNonUniformAllEqual((GroupNonUniformAllEqual) node);
                case Op.OpGroupNonUniformBroadcast: return VisitGroupNonUniformBroadcast((GroupNonUniformBroadcast) node);
                case Op.OpGroupNonUniformBroadcastFirst: return VisitGroupNonUniformBroadcastFirst((GroupNonUniformBroadcastFirst) node);
                case Op.OpGroupNonUniformBallot: return VisitGroupNonUniformBallot((GroupNonUniformBallot) node);
                case Op.OpGroupNonUniformInverseBallot: return VisitGroupNonUniformInverseBallot((GroupNonUniformInverseBallot) node);
                case Op.OpGroupNonUniformBallotBitExtract: return VisitGroupNonUniformBallotBitExtract((GroupNonUniformBallotBitExtract) node);
                case Op.OpGroupNonUniformBallotBitCount: return VisitGroupNonUniformBallotBitCount((GroupNonUniformBallotBitCount) node);
                case Op.OpGroupNonUniformBallotFindLSB: return VisitGroupNonUniformBallotFindLSB((GroupNonUniformBallotFindLSB) node);
                case Op.OpGroupNonUniformBallotFindMSB: return VisitGroupNonUniformBallotFindMSB((GroupNonUniformBallotFindMSB) node);
                case Op.OpGroupNonUniformShuffle: return VisitGroupNonUniformShuffle((GroupNonUniformShuffle) node);
                case Op.OpGroupNonUniformShuffleXor: return VisitGroupNonUniformShuffleXor((GroupNonUniformShuffleXor) node);
                case Op.OpGroupNonUniformShuffleUp: return VisitGroupNonUniformShuffleUp((GroupNonUniformShuffleUp) node);
                case Op.OpGroupNonUniformShuffleDown: return VisitGroupNonUniformShuffleDown((GroupNonUniformShuffleDown) node);
                case Op.OpGroupNonUniformIAdd: return VisitGroupNonUniformIAdd((GroupNonUniformIAdd) node);
                case Op.OpGroupNonUniformFAdd: return VisitGroupNonUniformFAdd((GroupNonUniformFAdd) node);
                case Op.OpGroupNonUniformIMul: return VisitGroupNonUniformIMul((GroupNonUniformIMul) node);
                case Op.OpGroupNonUniformFMul: return VisitGroupNonUniformFMul((GroupNonUniformFMul) node);
                case Op.OpGroupNonUniformSMin: return VisitGroupNonUniformSMin((GroupNonUniformSMin) node);
                case Op.OpGroupNonUniformUMin: return VisitGroupNonUniformUMin((GroupNonUniformUMin) node);
                case Op.OpGroupNonUniformFMin: return VisitGroupNonUniformFMin((GroupNonUniformFMin) node);
                case Op.OpGroupNonUniformSMax: return VisitGroupNonUniformSMax((GroupNonUniformSMax) node);
                case Op.OpGroupNonUniformUMax: return VisitGroupNonUniformUMax((GroupNonUniformUMax) node);
                case Op.OpGroupNonUniformFMax: return VisitGroupNonUniformFMax((GroupNonUniformFMax) node);
                case Op.OpGroupNonUniformBitwiseAnd: return VisitGroupNonUniformBitwiseAnd((GroupNonUniformBitwiseAnd) node);
                case Op.OpGroupNonUniformBitwiseOr: return VisitGroupNonUniformBitwiseOr((GroupNonUniformBitwiseOr) node);
                case Op.OpGroupNonUniformBitwiseXor: return VisitGroupNonUniformBitwiseXor((GroupNonUniformBitwiseXor) node);
                case Op.OpGroupNonUniformLogicalAnd: return VisitGroupNonUniformLogicalAnd((GroupNonUniformLogicalAnd) node);
                case Op.OpGroupNonUniformLogicalOr: return VisitGroupNonUniformLogicalOr((GroupNonUniformLogicalOr) node);
                case Op.OpGroupNonUniformLogicalXor: return VisitGroupNonUniformLogicalXor((GroupNonUniformLogicalXor) node);
                case Op.OpGroupNonUniformQuadBroadcast: return VisitGroupNonUniformQuadBroadcast((GroupNonUniformQuadBroadcast) node);
                case Op.OpGroupNonUniformQuadSwap: return VisitGroupNonUniformQuadSwap((GroupNonUniformQuadSwap) node);
                case Op.OpCopyLogical: return VisitCopyLogical((CopyLogical) node);
                case Op.OpPtrEqual: return VisitPtrEqual((PtrEqual) node);
                case Op.OpPtrNotEqual: return VisitPtrNotEqual((PtrNotEqual) node);
                case Op.OpPtrDiff: return VisitPtrDiff((PtrDiff) node);
                case Op.OpSubgroupBallotKHR: return VisitSubgroupBallotKHR((SubgroupBallotKHR) node);
                case Op.OpSubgroupFirstInvocationKHR: return VisitSubgroupFirstInvocationKHR((SubgroupFirstInvocationKHR) node);
                case Op.OpSubgroupAllKHR: return VisitSubgroupAllKHR((SubgroupAllKHR) node);
                case Op.OpSubgroupAnyKHR: return VisitSubgroupAnyKHR((SubgroupAnyKHR) node);
                case Op.OpSubgroupAllEqualKHR: return VisitSubgroupAllEqualKHR((SubgroupAllEqualKHR) node);
                case Op.OpSubgroupReadInvocationKHR: return VisitSubgroupReadInvocationKHR((SubgroupReadInvocationKHR) node);
                case Op.OpGroupIAddNonUniformAMD: return VisitGroupIAddNonUniformAMD((GroupIAddNonUniformAMD) node);
                case Op.OpGroupFAddNonUniformAMD: return VisitGroupFAddNonUniformAMD((GroupFAddNonUniformAMD) node);
                case Op.OpGroupFMinNonUniformAMD: return VisitGroupFMinNonUniformAMD((GroupFMinNonUniformAMD) node);
                case Op.OpGroupUMinNonUniformAMD: return VisitGroupUMinNonUniformAMD((GroupUMinNonUniformAMD) node);
                case Op.OpGroupSMinNonUniformAMD: return VisitGroupSMinNonUniformAMD((GroupSMinNonUniformAMD) node);
                case Op.OpGroupFMaxNonUniformAMD: return VisitGroupFMaxNonUniformAMD((GroupFMaxNonUniformAMD) node);
                case Op.OpGroupUMaxNonUniformAMD: return VisitGroupUMaxNonUniformAMD((GroupUMaxNonUniformAMD) node);
                case Op.OpGroupSMaxNonUniformAMD: return VisitGroupSMaxNonUniformAMD((GroupSMaxNonUniformAMD) node);
                case Op.OpFragmentMaskFetchAMD: return VisitFragmentMaskFetchAMD((FragmentMaskFetchAMD) node);
                case Op.OpFragmentFetchAMD: return VisitFragmentFetchAMD((FragmentFetchAMD) node);
                case Op.OpReadClockKHR: return VisitReadClockKHR((ReadClockKHR) node);
                case Op.OpImageSampleFootprintNV: return VisitImageSampleFootprintNV((ImageSampleFootprintNV) node);
                case Op.OpGroupNonUniformPartitionNV: return VisitGroupNonUniformPartitionNV((GroupNonUniformPartitionNV) node);
                case Op.OpWritePackedPrimitiveIndices4x8NV: return VisitWritePackedPrimitiveIndices4x8NV((WritePackedPrimitiveIndices4x8NV) node);
                case Op.OpReportIntersectionNV: return VisitReportIntersectionNV((ReportIntersectionNV) node);
                case Op.OpIgnoreIntersectionNV: return VisitIgnoreIntersectionNV((IgnoreIntersectionNV) node);
                case Op.OpTerminateRayNV: return VisitTerminateRayNV((TerminateRayNV) node);
                case Op.OpTraceNV: return VisitTraceNV((TraceNV) node);
                case Op.OpTypeAccelerationStructureNV: return VisitTypeAccelerationStructureNV((Types.TypeAccelerationStructureNV)node);
                case Op.OpTypeRayQueryProvisionalKHR: return VisitTypeRayQueryProvisionalKHR((Types.TypeRayQueryProvisionalKHR)node);
                case Op.OpRayQueryInitializeKHR: return VisitRayQueryInitializeKHR((RayQueryInitializeKHR) node);
                case Op.OpRayQueryTerminateKHR: return VisitRayQueryTerminateKHR((RayQueryTerminateKHR) node);
                case Op.OpRayQueryGenerateIntersectionKHR: return VisitRayQueryGenerateIntersectionKHR((RayQueryGenerateIntersectionKHR) node);
                case Op.OpRayQueryConfirmIntersectionKHR: return VisitRayQueryConfirmIntersectionKHR((RayQueryConfirmIntersectionKHR) node);
                case Op.OpRayQueryProceedKHR: return VisitRayQueryProceedKHR((RayQueryProceedKHR) node);
                case Op.OpRayQueryGetIntersectionTypeKHR: return VisitRayQueryGetIntersectionTypeKHR((RayQueryGetIntersectionTypeKHR) node);
                case Op.OpRayQueryGetRayTMinKHR: return VisitRayQueryGetRayTMinKHR((RayQueryGetRayTMinKHR) node);
                case Op.OpRayQueryGetRayFlagsKHR: return VisitRayQueryGetRayFlagsKHR((RayQueryGetRayFlagsKHR) node);
                case Op.OpRayQueryGetIntersectionTKHR: return VisitRayQueryGetIntersectionTKHR((RayQueryGetIntersectionTKHR) node);
                case Op.OpRayQueryGetIntersectionInstanceCustomIndexKHR: return VisitRayQueryGetIntersectionInstanceCustomIndexKHR((RayQueryGetIntersectionInstanceCustomIndexKHR) node);
                case Op.OpRayQueryGetIntersectionInstanceIdKHR: return VisitRayQueryGetIntersectionInstanceIdKHR((RayQueryGetIntersectionInstanceIdKHR) node);
                case Op.OpRayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR: return VisitRayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR((RayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR) node);
                case Op.OpRayQueryGetIntersectionGeometryIndexKHR: return VisitRayQueryGetIntersectionGeometryIndexKHR((RayQueryGetIntersectionGeometryIndexKHR) node);
                case Op.OpRayQueryGetIntersectionPrimitiveIndexKHR: return VisitRayQueryGetIntersectionPrimitiveIndexKHR((RayQueryGetIntersectionPrimitiveIndexKHR) node);
                case Op.OpRayQueryGetIntersectionBarycentricsKHR: return VisitRayQueryGetIntersectionBarycentricsKHR((RayQueryGetIntersectionBarycentricsKHR) node);
                case Op.OpRayQueryGetIntersectionFrontFaceKHR: return VisitRayQueryGetIntersectionFrontFaceKHR((RayQueryGetIntersectionFrontFaceKHR) node);
                case Op.OpRayQueryGetIntersectionCandidateAABBOpaqueKHR: return VisitRayQueryGetIntersectionCandidateAABBOpaqueKHR((RayQueryGetIntersectionCandidateAABBOpaqueKHR) node);
                case Op.OpRayQueryGetIntersectionObjectRayDirectionKHR: return VisitRayQueryGetIntersectionObjectRayDirectionKHR((RayQueryGetIntersectionObjectRayDirectionKHR) node);
                case Op.OpRayQueryGetIntersectionObjectRayOriginKHR: return VisitRayQueryGetIntersectionObjectRayOriginKHR((RayQueryGetIntersectionObjectRayOriginKHR) node);
                case Op.OpRayQueryGetWorldRayDirectionKHR: return VisitRayQueryGetWorldRayDirectionKHR((RayQueryGetWorldRayDirectionKHR) node);
                case Op.OpRayQueryGetWorldRayOriginKHR: return VisitRayQueryGetWorldRayOriginKHR((RayQueryGetWorldRayOriginKHR) node);
                case Op.OpRayQueryGetIntersectionObjectToWorldKHR: return VisitRayQueryGetIntersectionObjectToWorldKHR((RayQueryGetIntersectionObjectToWorldKHR) node);
                case Op.OpRayQueryGetIntersectionWorldToObjectKHR: return VisitRayQueryGetIntersectionWorldToObjectKHR((RayQueryGetIntersectionWorldToObjectKHR) node);
                case Op.OpExecuteCallableNV: return VisitExecuteCallableNV((ExecuteCallableNV) node);
                case Op.OpTypeCooperativeMatrixNV: return VisitTypeCooperativeMatrixNV((Types.TypeCooperativeMatrixNV)node);
                case Op.OpCooperativeMatrixLoadNV: return VisitCooperativeMatrixLoadNV((CooperativeMatrixLoadNV) node);
                case Op.OpCooperativeMatrixStoreNV: return VisitCooperativeMatrixStoreNV((CooperativeMatrixStoreNV) node);
                case Op.OpCooperativeMatrixMulAddNV: return VisitCooperativeMatrixMulAddNV((CooperativeMatrixMulAddNV) node);
                case Op.OpCooperativeMatrixLengthNV: return VisitCooperativeMatrixLengthNV((CooperativeMatrixLengthNV) node);
                case Op.OpBeginInvocationInterlockEXT: return VisitBeginInvocationInterlockEXT((BeginInvocationInterlockEXT) node);
                case Op.OpEndInvocationInterlockEXT: return VisitEndInvocationInterlockEXT((EndInvocationInterlockEXT) node);
                case Op.OpDemoteToHelperInvocationEXT: return VisitDemoteToHelperInvocationEXT((DemoteToHelperInvocationEXT) node);
                case Op.OpIsHelperInvocationEXT: return VisitIsHelperInvocationEXT((IsHelperInvocationEXT) node);
                case Op.OpSubgroupShuffleINTEL: return VisitSubgroupShuffleINTEL((SubgroupShuffleINTEL) node);
                case Op.OpSubgroupShuffleDownINTEL: return VisitSubgroupShuffleDownINTEL((SubgroupShuffleDownINTEL) node);
                case Op.OpSubgroupShuffleUpINTEL: return VisitSubgroupShuffleUpINTEL((SubgroupShuffleUpINTEL) node);
                case Op.OpSubgroupShuffleXorINTEL: return VisitSubgroupShuffleXorINTEL((SubgroupShuffleXorINTEL) node);
                case Op.OpSubgroupBlockReadINTEL: return VisitSubgroupBlockReadINTEL((SubgroupBlockReadINTEL) node);
                case Op.OpSubgroupBlockWriteINTEL: return VisitSubgroupBlockWriteINTEL((SubgroupBlockWriteINTEL) node);
                case Op.OpSubgroupImageBlockReadINTEL: return VisitSubgroupImageBlockReadINTEL((SubgroupImageBlockReadINTEL) node);
                case Op.OpSubgroupImageBlockWriteINTEL: return VisitSubgroupImageBlockWriteINTEL((SubgroupImageBlockWriteINTEL) node);
                case Op.OpSubgroupImageMediaBlockReadINTEL: return VisitSubgroupImageMediaBlockReadINTEL((SubgroupImageMediaBlockReadINTEL) node);
                case Op.OpSubgroupImageMediaBlockWriteINTEL: return VisitSubgroupImageMediaBlockWriteINTEL((SubgroupImageMediaBlockWriteINTEL) node);
                case Op.OpUCountLeadingZerosINTEL: return VisitUCountLeadingZerosINTEL((UCountLeadingZerosINTEL) node);
                case Op.OpUCountTrailingZerosINTEL: return VisitUCountTrailingZerosINTEL((UCountTrailingZerosINTEL) node);
                case Op.OpAbsISubINTEL: return VisitAbsISubINTEL((AbsISubINTEL) node);
                case Op.OpAbsUSubINTEL: return VisitAbsUSubINTEL((AbsUSubINTEL) node);
                case Op.OpIAddSatINTEL: return VisitIAddSatINTEL((IAddSatINTEL) node);
                case Op.OpUAddSatINTEL: return VisitUAddSatINTEL((UAddSatINTEL) node);
                case Op.OpIAverageINTEL: return VisitIAverageINTEL((IAverageINTEL) node);
                case Op.OpUAverageINTEL: return VisitUAverageINTEL((UAverageINTEL) node);
                case Op.OpIAverageRoundedINTEL: return VisitIAverageRoundedINTEL((IAverageRoundedINTEL) node);
                case Op.OpUAverageRoundedINTEL: return VisitUAverageRoundedINTEL((UAverageRoundedINTEL) node);
                case Op.OpISubSatINTEL: return VisitISubSatINTEL((ISubSatINTEL) node);
                case Op.OpUSubSatINTEL: return VisitUSubSatINTEL((USubSatINTEL) node);
                case Op.OpIMul32x16INTEL: return VisitIMul32x16INTEL((IMul32x16INTEL) node);
                case Op.OpUMul32x16INTEL: return VisitUMul32x16INTEL((UMul32x16INTEL) node);
                case Op.OpDecorateString: return VisitDecorateString((Nodes.DecorateString) node);
                case Op.OpMemberDecorateString: return VisitMemberDecorateString((Nodes.MemberDecorateString) node);
                case Op.OpVmeImageINTEL: return VisitVmeImageINTEL((VmeImageINTEL) node);
                case Op.OpTypeVmeImageINTEL: return VisitTypeVmeImageINTEL((Types.TypeVmeImageINTEL)node);
                case Op.OpTypeAvcImePayloadINTEL: return VisitTypeAvcImePayloadINTEL((Types.TypeAvcImePayloadINTEL)node);
                case Op.OpTypeAvcRefPayloadINTEL: return VisitTypeAvcRefPayloadINTEL((Types.TypeAvcRefPayloadINTEL)node);
                case Op.OpTypeAvcSicPayloadINTEL: return VisitTypeAvcSicPayloadINTEL((Types.TypeAvcSicPayloadINTEL)node);
                case Op.OpTypeAvcMcePayloadINTEL: return VisitTypeAvcMcePayloadINTEL((Types.TypeAvcMcePayloadINTEL)node);
                case Op.OpTypeAvcMceResultINTEL: return VisitTypeAvcMceResultINTEL((Types.TypeAvcMceResultINTEL)node);
                case Op.OpTypeAvcImeResultINTEL: return VisitTypeAvcImeResultINTEL((Types.TypeAvcImeResultINTEL)node);
                case Op.OpTypeAvcImeResultSingleReferenceStreamoutINTEL: return VisitTypeAvcImeResultSingleReferenceStreamoutINTEL((Types.TypeAvcImeResultSingleReferenceStreamoutINTEL)node);
                case Op.OpTypeAvcImeResultDualReferenceStreamoutINTEL: return VisitTypeAvcImeResultDualReferenceStreamoutINTEL((Types.TypeAvcImeResultDualReferenceStreamoutINTEL)node);
                case Op.OpTypeAvcImeSingleReferenceStreaminINTEL: return VisitTypeAvcImeSingleReferenceStreaminINTEL((Types.TypeAvcImeSingleReferenceStreaminINTEL)node);
                case Op.OpTypeAvcImeDualReferenceStreaminINTEL: return VisitTypeAvcImeDualReferenceStreaminINTEL((Types.TypeAvcImeDualReferenceStreaminINTEL)node);
                case Op.OpTypeAvcRefResultINTEL: return VisitTypeAvcRefResultINTEL((Types.TypeAvcRefResultINTEL)node);
                case Op.OpTypeAvcSicResultINTEL: return VisitTypeAvcSicResultINTEL((Types.TypeAvcSicResultINTEL)node);
                case Op.OpSubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL: return VisitSubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL((SubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL) node);
                case Op.OpSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL: return VisitSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL((SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL) node);
                case Op.OpSubgroupAvcMceGetDefaultInterShapePenaltyINTEL: return VisitSubgroupAvcMceGetDefaultInterShapePenaltyINTEL((SubgroupAvcMceGetDefaultInterShapePenaltyINTEL) node);
                case Op.OpSubgroupAvcMceSetInterShapePenaltyINTEL: return VisitSubgroupAvcMceSetInterShapePenaltyINTEL((SubgroupAvcMceSetInterShapePenaltyINTEL) node);
                case Op.OpSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL: return VisitSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL((SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL) node);
                case Op.OpSubgroupAvcMceSetInterDirectionPenaltyINTEL: return VisitSubgroupAvcMceSetInterDirectionPenaltyINTEL((SubgroupAvcMceSetInterDirectionPenaltyINTEL) node);
                case Op.OpSubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL: return VisitSubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL((SubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL) node);
                case Op.OpSubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL: return VisitSubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL((SubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL) node);
                case Op.OpSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL: return VisitSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL((SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL) node);
                case Op.OpSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL: return VisitSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL((SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL) node);
                case Op.OpSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL: return VisitSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL((SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL) node);
                case Op.OpSubgroupAvcMceSetMotionVectorCostFunctionINTEL: return VisitSubgroupAvcMceSetMotionVectorCostFunctionINTEL((SubgroupAvcMceSetMotionVectorCostFunctionINTEL) node);
                case Op.OpSubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL: return VisitSubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL((SubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL) node);
                case Op.OpSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL: return VisitSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL((SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL) node);
                case Op.OpSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL: return VisitSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL((SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL) node);
                case Op.OpSubgroupAvcMceSetAcOnlyHaarINTEL: return VisitSubgroupAvcMceSetAcOnlyHaarINTEL((SubgroupAvcMceSetAcOnlyHaarINTEL) node);
                case Op.OpSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL: return VisitSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL((SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL) node);
                case Op.OpSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL: return VisitSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL((SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL) node);
                case Op.OpSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL: return VisitSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL((SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL) node);
                case Op.OpSubgroupAvcMceConvertToImePayloadINTEL: return VisitSubgroupAvcMceConvertToImePayloadINTEL((SubgroupAvcMceConvertToImePayloadINTEL) node);
                case Op.OpSubgroupAvcMceConvertToImeResultINTEL: return VisitSubgroupAvcMceConvertToImeResultINTEL((SubgroupAvcMceConvertToImeResultINTEL) node);
                case Op.OpSubgroupAvcMceConvertToRefPayloadINTEL: return VisitSubgroupAvcMceConvertToRefPayloadINTEL((SubgroupAvcMceConvertToRefPayloadINTEL) node);
                case Op.OpSubgroupAvcMceConvertToRefResultINTEL: return VisitSubgroupAvcMceConvertToRefResultINTEL((SubgroupAvcMceConvertToRefResultINTEL) node);
                case Op.OpSubgroupAvcMceConvertToSicPayloadINTEL: return VisitSubgroupAvcMceConvertToSicPayloadINTEL((SubgroupAvcMceConvertToSicPayloadINTEL) node);
                case Op.OpSubgroupAvcMceConvertToSicResultINTEL: return VisitSubgroupAvcMceConvertToSicResultINTEL((SubgroupAvcMceConvertToSicResultINTEL) node);
                case Op.OpSubgroupAvcMceGetMotionVectorsINTEL: return VisitSubgroupAvcMceGetMotionVectorsINTEL((SubgroupAvcMceGetMotionVectorsINTEL) node);
                case Op.OpSubgroupAvcMceGetInterDistortionsINTEL: return VisitSubgroupAvcMceGetInterDistortionsINTEL((SubgroupAvcMceGetInterDistortionsINTEL) node);
                case Op.OpSubgroupAvcMceGetBestInterDistortionsINTEL: return VisitSubgroupAvcMceGetBestInterDistortionsINTEL((SubgroupAvcMceGetBestInterDistortionsINTEL) node);
                case Op.OpSubgroupAvcMceGetInterMajorShapeINTEL: return VisitSubgroupAvcMceGetInterMajorShapeINTEL((SubgroupAvcMceGetInterMajorShapeINTEL) node);
                case Op.OpSubgroupAvcMceGetInterMinorShapeINTEL: return VisitSubgroupAvcMceGetInterMinorShapeINTEL((SubgroupAvcMceGetInterMinorShapeINTEL) node);
                case Op.OpSubgroupAvcMceGetInterDirectionsINTEL: return VisitSubgroupAvcMceGetInterDirectionsINTEL((SubgroupAvcMceGetInterDirectionsINTEL) node);
                case Op.OpSubgroupAvcMceGetInterMotionVectorCountINTEL: return VisitSubgroupAvcMceGetInterMotionVectorCountINTEL((SubgroupAvcMceGetInterMotionVectorCountINTEL) node);
                case Op.OpSubgroupAvcMceGetInterReferenceIdsINTEL: return VisitSubgroupAvcMceGetInterReferenceIdsINTEL((SubgroupAvcMceGetInterReferenceIdsINTEL) node);
                case Op.OpSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL: return VisitSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL((SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL) node);
                case Op.OpSubgroupAvcImeInitializeINTEL: return VisitSubgroupAvcImeInitializeINTEL((SubgroupAvcImeInitializeINTEL) node);
                case Op.OpSubgroupAvcImeSetSingleReferenceINTEL: return VisitSubgroupAvcImeSetSingleReferenceINTEL((SubgroupAvcImeSetSingleReferenceINTEL) node);
                case Op.OpSubgroupAvcImeSetDualReferenceINTEL: return VisitSubgroupAvcImeSetDualReferenceINTEL((SubgroupAvcImeSetDualReferenceINTEL) node);
                case Op.OpSubgroupAvcImeRefWindowSizeINTEL: return VisitSubgroupAvcImeRefWindowSizeINTEL((SubgroupAvcImeRefWindowSizeINTEL) node);
                case Op.OpSubgroupAvcImeAdjustRefOffsetINTEL: return VisitSubgroupAvcImeAdjustRefOffsetINTEL((SubgroupAvcImeAdjustRefOffsetINTEL) node);
                case Op.OpSubgroupAvcImeConvertToMcePayloadINTEL: return VisitSubgroupAvcImeConvertToMcePayloadINTEL((SubgroupAvcImeConvertToMcePayloadINTEL) node);
                case Op.OpSubgroupAvcImeSetMaxMotionVectorCountINTEL: return VisitSubgroupAvcImeSetMaxMotionVectorCountINTEL((SubgroupAvcImeSetMaxMotionVectorCountINTEL) node);
                case Op.OpSubgroupAvcImeSetUnidirectionalMixDisableINTEL: return VisitSubgroupAvcImeSetUnidirectionalMixDisableINTEL((SubgroupAvcImeSetUnidirectionalMixDisableINTEL) node);
                case Op.OpSubgroupAvcImeSetEarlySearchTerminationThresholdINTEL: return VisitSubgroupAvcImeSetEarlySearchTerminationThresholdINTEL((SubgroupAvcImeSetEarlySearchTerminationThresholdINTEL) node);
                case Op.OpSubgroupAvcImeSetWeightedSadINTEL: return VisitSubgroupAvcImeSetWeightedSadINTEL((SubgroupAvcImeSetWeightedSadINTEL) node);
                case Op.OpSubgroupAvcImeEvaluateWithSingleReferenceINTEL: return VisitSubgroupAvcImeEvaluateWithSingleReferenceINTEL((SubgroupAvcImeEvaluateWithSingleReferenceINTEL) node);
                case Op.OpSubgroupAvcImeEvaluateWithDualReferenceINTEL: return VisitSubgroupAvcImeEvaluateWithDualReferenceINTEL((SubgroupAvcImeEvaluateWithDualReferenceINTEL) node);
                case Op.OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL: return VisitSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL((SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL) node);
                case Op.OpSubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL: return VisitSubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL((SubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL) node);
                case Op.OpSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL: return VisitSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL((SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL) node);
                case Op.OpSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL: return VisitSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL((SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL) node);
                case Op.OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL: return VisitSubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL((SubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL) node);
                case Op.OpSubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL: return VisitSubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL((SubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL) node);
                case Op.OpSubgroupAvcImeConvertToMceResultINTEL: return VisitSubgroupAvcImeConvertToMceResultINTEL((SubgroupAvcImeConvertToMceResultINTEL) node);
                case Op.OpSubgroupAvcImeGetSingleReferenceStreaminINTEL: return VisitSubgroupAvcImeGetSingleReferenceStreaminINTEL((SubgroupAvcImeGetSingleReferenceStreaminINTEL) node);
                case Op.OpSubgroupAvcImeGetDualReferenceStreaminINTEL: return VisitSubgroupAvcImeGetDualReferenceStreaminINTEL((SubgroupAvcImeGetDualReferenceStreaminINTEL) node);
                case Op.OpSubgroupAvcImeStripSingleReferenceStreamoutINTEL: return VisitSubgroupAvcImeStripSingleReferenceStreamoutINTEL((SubgroupAvcImeStripSingleReferenceStreamoutINTEL) node);
                case Op.OpSubgroupAvcImeStripDualReferenceStreamoutINTEL: return VisitSubgroupAvcImeStripDualReferenceStreamoutINTEL((SubgroupAvcImeStripDualReferenceStreamoutINTEL) node);
                case Op.OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL: return VisitSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL((SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL) node);
                case Op.OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL: return VisitSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL((SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL) node);
                case Op.OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL: return VisitSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL((SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL) node);
                case Op.OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL: return VisitSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL((SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL) node);
                case Op.OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL: return VisitSubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL((SubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL) node);
                case Op.OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL: return VisitSubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL((SubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL) node);
                case Op.OpSubgroupAvcImeGetBorderReachedINTEL: return VisitSubgroupAvcImeGetBorderReachedINTEL((SubgroupAvcImeGetBorderReachedINTEL) node);
                case Op.OpSubgroupAvcImeGetTruncatedSearchIndicationINTEL: return VisitSubgroupAvcImeGetTruncatedSearchIndicationINTEL((SubgroupAvcImeGetTruncatedSearchIndicationINTEL) node);
                case Op.OpSubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL: return VisitSubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL((SubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL) node);
                case Op.OpSubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL: return VisitSubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL((SubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL) node);
                case Op.OpSubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL: return VisitSubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL((SubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL) node);
                case Op.OpSubgroupAvcFmeInitializeINTEL: return VisitSubgroupAvcFmeInitializeINTEL((SubgroupAvcFmeInitializeINTEL) node);
                case Op.OpSubgroupAvcBmeInitializeINTEL: return VisitSubgroupAvcBmeInitializeINTEL((SubgroupAvcBmeInitializeINTEL) node);
                case Op.OpSubgroupAvcRefConvertToMcePayloadINTEL: return VisitSubgroupAvcRefConvertToMcePayloadINTEL((SubgroupAvcRefConvertToMcePayloadINTEL) node);
                case Op.OpSubgroupAvcRefSetBidirectionalMixDisableINTEL: return VisitSubgroupAvcRefSetBidirectionalMixDisableINTEL((SubgroupAvcRefSetBidirectionalMixDisableINTEL) node);
                case Op.OpSubgroupAvcRefSetBilinearFilterEnableINTEL: return VisitSubgroupAvcRefSetBilinearFilterEnableINTEL((SubgroupAvcRefSetBilinearFilterEnableINTEL) node);
                case Op.OpSubgroupAvcRefEvaluateWithSingleReferenceINTEL: return VisitSubgroupAvcRefEvaluateWithSingleReferenceINTEL((SubgroupAvcRefEvaluateWithSingleReferenceINTEL) node);
                case Op.OpSubgroupAvcRefEvaluateWithDualReferenceINTEL: return VisitSubgroupAvcRefEvaluateWithDualReferenceINTEL((SubgroupAvcRefEvaluateWithDualReferenceINTEL) node);
                case Op.OpSubgroupAvcRefEvaluateWithMultiReferenceINTEL: return VisitSubgroupAvcRefEvaluateWithMultiReferenceINTEL((SubgroupAvcRefEvaluateWithMultiReferenceINTEL) node);
                case Op.OpSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL: return VisitSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL((SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL) node);
                case Op.OpSubgroupAvcRefConvertToMceResultINTEL: return VisitSubgroupAvcRefConvertToMceResultINTEL((SubgroupAvcRefConvertToMceResultINTEL) node);
                case Op.OpSubgroupAvcSicInitializeINTEL: return VisitSubgroupAvcSicInitializeINTEL((SubgroupAvcSicInitializeINTEL) node);
                case Op.OpSubgroupAvcSicConfigureSkcINTEL: return VisitSubgroupAvcSicConfigureSkcINTEL((SubgroupAvcSicConfigureSkcINTEL) node);
                case Op.OpSubgroupAvcSicConfigureIpeLumaINTEL: return VisitSubgroupAvcSicConfigureIpeLumaINTEL((SubgroupAvcSicConfigureIpeLumaINTEL) node);
                case Op.OpSubgroupAvcSicConfigureIpeLumaChromaINTEL: return VisitSubgroupAvcSicConfigureIpeLumaChromaINTEL((SubgroupAvcSicConfigureIpeLumaChromaINTEL) node);
                case Op.OpSubgroupAvcSicGetMotionVectorMaskINTEL: return VisitSubgroupAvcSicGetMotionVectorMaskINTEL((SubgroupAvcSicGetMotionVectorMaskINTEL) node);
                case Op.OpSubgroupAvcSicConvertToMcePayloadINTEL: return VisitSubgroupAvcSicConvertToMcePayloadINTEL((SubgroupAvcSicConvertToMcePayloadINTEL) node);
                case Op.OpSubgroupAvcSicSetIntraLumaShapePenaltyINTEL: return VisitSubgroupAvcSicSetIntraLumaShapePenaltyINTEL((SubgroupAvcSicSetIntraLumaShapePenaltyINTEL) node);
                case Op.OpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL: return VisitSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL((SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL) node);
                case Op.OpSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL: return VisitSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL((SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL) node);
                case Op.OpSubgroupAvcSicSetBilinearFilterEnableINTEL: return VisitSubgroupAvcSicSetBilinearFilterEnableINTEL((SubgroupAvcSicSetBilinearFilterEnableINTEL) node);
                case Op.OpSubgroupAvcSicSetSkcForwardTransformEnableINTEL: return VisitSubgroupAvcSicSetSkcForwardTransformEnableINTEL((SubgroupAvcSicSetSkcForwardTransformEnableINTEL) node);
                case Op.OpSubgroupAvcSicSetBlockBasedRawSkipSadINTEL: return VisitSubgroupAvcSicSetBlockBasedRawSkipSadINTEL((SubgroupAvcSicSetBlockBasedRawSkipSadINTEL) node);
                case Op.OpSubgroupAvcSicEvaluateIpeINTEL: return VisitSubgroupAvcSicEvaluateIpeINTEL((SubgroupAvcSicEvaluateIpeINTEL) node);
                case Op.OpSubgroupAvcSicEvaluateWithSingleReferenceINTEL: return VisitSubgroupAvcSicEvaluateWithSingleReferenceINTEL((SubgroupAvcSicEvaluateWithSingleReferenceINTEL) node);
                case Op.OpSubgroupAvcSicEvaluateWithDualReferenceINTEL: return VisitSubgroupAvcSicEvaluateWithDualReferenceINTEL((SubgroupAvcSicEvaluateWithDualReferenceINTEL) node);
                case Op.OpSubgroupAvcSicEvaluateWithMultiReferenceINTEL: return VisitSubgroupAvcSicEvaluateWithMultiReferenceINTEL((SubgroupAvcSicEvaluateWithMultiReferenceINTEL) node);
                case Op.OpSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL: return VisitSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL((SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL) node);
                case Op.OpSubgroupAvcSicConvertToMceResultINTEL: return VisitSubgroupAvcSicConvertToMceResultINTEL((SubgroupAvcSicConvertToMceResultINTEL) node);
                case Op.OpSubgroupAvcSicGetIpeLumaShapeINTEL: return VisitSubgroupAvcSicGetIpeLumaShapeINTEL((SubgroupAvcSicGetIpeLumaShapeINTEL) node);
                case Op.OpSubgroupAvcSicGetBestIpeLumaDistortionINTEL: return VisitSubgroupAvcSicGetBestIpeLumaDistortionINTEL((SubgroupAvcSicGetBestIpeLumaDistortionINTEL) node);
                case Op.OpSubgroupAvcSicGetBestIpeChromaDistortionINTEL: return VisitSubgroupAvcSicGetBestIpeChromaDistortionINTEL((SubgroupAvcSicGetBestIpeChromaDistortionINTEL) node);
                case Op.OpSubgroupAvcSicGetPackedIpeLumaModesINTEL: return VisitSubgroupAvcSicGetPackedIpeLumaModesINTEL((SubgroupAvcSicGetPackedIpeLumaModesINTEL) node);
                case Op.OpSubgroupAvcSicGetIpeChromaModeINTEL: return VisitSubgroupAvcSicGetIpeChromaModeINTEL((SubgroupAvcSicGetIpeChromaModeINTEL) node);
                case Op.OpSubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL: return VisitSubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL((SubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL) node);
                case Op.OpSubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL: return VisitSubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL((SubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL) node);
                case Op.OpSubgroupAvcSicGetInterRawSadsINTEL: return VisitSubgroupAvcSicGetInterRawSadsINTEL((SubgroupAvcSicGetInterRawSadsINTEL) node);
            }

            throw new NotImplementedException(node.OpCode + " not implemented yet.");
        }
        
        protected void VisitDecorations(Node node)
        {
            foreach (var decoration in node.BuildDecorations())
            {
                Visit(decoration);
            }
        }

        protected virtual string Visit(string instruction)
        {
            return instruction;
        }

        protected virtual uint Visit(uint instruction)
        {
            return instruction;
        }

        protected virtual NestedInstruction Visit(NestedNode instruction)
        {
            return new NestedInstruction(instruction.Node, this);
        }

        internal virtual IList<uint> Visit(IList<uint> instructions)
        {
            return instructions;
        }

        protected virtual LiteralContextDependentNumber Visit(Operands.NumberLiteral instruction)
        {
            return instruction.ToLiteral(Visit);
        }

        protected virtual PairIdRefIdRef Visit(Operands.PairNodeNode instruction)
        {
            return new PairIdRefIdRef(){ IdRef = Visit(instruction.Node), IdRef2 = Visit(instruction.Node2) };
        }

        protected virtual Spv.PairLiteralIntegerIdRef Visit(Operands.PairLiteralIntegerNode operand)
        {
            return new Spv.PairLiteralIntegerIdRef()
                {IdRef = Visit(operand.Node), LiteralInteger = operand.LiteralInteger};
        }
        
        protected virtual Spv.PairIdRefLiteralInteger Visit(Operands.PairNodeLiteralInteger operand)
        {
            return new Spv.PairIdRefLiteralInteger()
                { IdRef = Visit(operand.Node), LiteralInteger = operand.LiteralInteger };
        }

        protected virtual LiteralContextDependentNumber Visit(LiteralContextDependentNumber instruction)
        {
            return instruction;
        }

        internal virtual IList<IdRef> Visit(IList<Node> instructions)
        {
            if (instructions == null)
                return null;
            return new ListSegment<IdRef>(instructions.Select(_ => (IdRef)Visit(_)));
        }

        protected virtual Spv.ImageOperands Visit(Spv.ImageOperands operand)
        {
            return operand;
        }
        protected virtual Spv.FPFastMathMode Visit(Spv.FPFastMathMode operand)
        {
            return operand;
        }
        protected virtual Spv.SelectionControl Visit(Spv.SelectionControl operand)
        {
            return operand;
        }
        protected virtual Spv.LoopControl Visit(Spv.LoopControl operand)
        {
            return operand;
        }
        protected virtual Spv.FunctionControl Visit(Spv.FunctionControl operand)
        {
            return operand;
        }
        protected virtual Spv.MemorySemantics Visit(Spv.MemorySemantics operand)
        {
            return operand;
        }
        protected virtual Spv.MemoryAccess Visit(Spv.MemoryAccess operand)
        {
            return operand;
        }
        protected virtual Spv.KernelProfilingInfo Visit(Spv.KernelProfilingInfo operand)
        {
            return operand;
        }
        protected virtual Spv.RayFlags Visit(Spv.RayFlags operand)
        {
            return operand;
        }
        protected virtual Spv.SourceLanguage Visit(Spv.SourceLanguage operand)
        {
            return operand;
        }
        protected virtual Spv.ExecutionModel Visit(Spv.ExecutionModel operand)
        {
            return operand;
        }
        protected virtual Spv.AddressingModel Visit(Spv.AddressingModel operand)
        {
            return operand;
        }
        protected virtual Spv.MemoryModel Visit(Spv.MemoryModel operand)
        {
            return operand;
        }
        protected virtual Spv.ExecutionMode Visit(Spv.ExecutionMode operand)
        {
            return operand;
        }
        protected virtual Spv.StorageClass Visit(Spv.StorageClass operand)
        {
            return operand;
        }
        protected virtual Spv.Dim Visit(Spv.Dim operand)
        {
            return operand;
        }
        protected virtual Spv.SamplerAddressingMode Visit(Spv.SamplerAddressingMode operand)
        {
            return operand;
        }
        protected virtual Spv.SamplerFilterMode Visit(Spv.SamplerFilterMode operand)
        {
            return operand;
        }
        protected virtual Spv.ImageFormat Visit(Spv.ImageFormat operand)
        {
            return operand;
        }
        protected virtual Spv.ImageChannelOrder Visit(Spv.ImageChannelOrder operand)
        {
            return operand;
        }
        protected virtual Spv.ImageChannelDataType Visit(Spv.ImageChannelDataType operand)
        {
            return operand;
        }
        protected virtual Spv.FPRoundingMode Visit(Spv.FPRoundingMode operand)
        {
            return operand;
        }
        protected virtual Spv.LinkageType Visit(Spv.LinkageType operand)
        {
            return operand;
        }
        protected virtual Spv.AccessQualifier Visit(Spv.AccessQualifier operand)
        {
            return operand;
        }
        protected virtual Spv.FunctionParameterAttribute Visit(Spv.FunctionParameterAttribute operand)
        {
            return operand;
        }
        protected virtual Spv.Decoration Visit(Spv.Decoration operand)
        {
            return operand;
        }
        protected virtual Spv.BuiltIn Visit(Spv.BuiltIn operand)
        {
            return operand;
        }
        protected virtual Spv.Scope Visit(Spv.Scope operand)
        {
            return operand;
        }
        protected virtual Spv.GroupOperation Visit(Spv.GroupOperation operand)
        {
            return operand;
        }
        protected virtual Spv.KernelEnqueueFlags Visit(Spv.KernelEnqueueFlags operand)
        {
            return operand;
        }
        protected virtual Spv.Capability Visit(Spv.Capability operand)
        {
            return operand;
        }
        protected virtual Spv.RayQueryIntersection Visit(Spv.RayQueryIntersection operand)
        {
            return operand;
        }
        protected virtual Spv.RayQueryCommittedIntersectionType Visit(Spv.RayQueryCommittedIntersectionType operand)
        {
            return operand;
        }
        protected virtual Spv.RayQueryCandidateIntersectionType Visit(Spv.RayQueryCandidateIntersectionType operand)
        {
            return operand;
        }
        protected virtual IList<Spv.PairLiteralIntegerIdRef> Visit(IList<Operands.PairLiteralIntegerNode> operand)
        {
            var res = new Spv.PairLiteralIntegerIdRef[operand.Count];
            for (int i=0; i<operand.Count; ++i)
            {
                res[i] = Visit(operand[i]);
            }
            return res;
        }
        protected virtual IList<Spv.PairIdRefLiteralInteger> Visit(IList<Operands.PairNodeLiteralInteger> operand)
        {
            var res = new Spv.PairIdRefLiteralInteger[operand.Count];
            for (int i=0; i<operand.Count; ++i)
            {
                res[i] = Visit(operand[i]);
            }
            return res;
        }
        protected virtual IList<Spv.PairIdRefIdRef> Visit(IList<Operands.PairNodeNode> operand)
        {
            var res = new Spv.PairIdRefIdRef[operand.Count];
            for (int i=0; i<operand.Count; ++i)
            {
                res[i] = Visit(operand[i]);
            }
            return res;
        }
        protected virtual OpNop VisitNop(Nodes.Nop node)
        {
            var instruction = new OpNop();
            _instructionMap.Add(node, instruction);

            return instruction;
        }
        protected virtual OpUndef VisitUndef(Nodes.Undef node)
        {
            var instruction = new OpUndef();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpSourceContinued VisitSourceContinued(Nodes.SourceContinued node)
        {
            var instruction = new OpSourceContinued();
            _instructionMap.Add(node, instruction);

            instruction.ContinuedSource = Visit(node.ContinuedSource);
            return instruction;
        }
        protected virtual OpSource VisitSource(Nodes.Source node)
        {
            var instruction = new OpSource();
            _instructionMap.Add(node, instruction);

            instruction.SourceLanguage = Visit(node.SourceLanguage);
            instruction.Version = Visit(node.Version);
            instruction.File = Visit(node.File);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpSourceExtension VisitSourceExtension(Nodes.SourceExtension node)
        {
            var instruction = new OpSourceExtension();
            _instructionMap.Add(node, instruction);

            instruction.Extension = Visit(node.Extension);
            return instruction;
        }
        protected virtual OpName VisitName(Nodes.Name node)
        {
            var instruction = new OpName();
            _instructionMap.Add(node, instruction);

            instruction.Target = Visit(node.Target);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpMemberName VisitMemberName(Nodes.MemberName node)
        {
            var instruction = new OpMemberName();
            _instructionMap.Add(node, instruction);

            instruction.Type = Visit(node.Type);
            instruction.Member = Visit(node.Member);
            instruction.Name = Visit(node.Name);
            return instruction;
        }
        protected virtual OpString VisitString(Nodes.String node)
        {
            var instruction = new OpString();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpLine VisitLine(Nodes.Line node)
        {
            var instruction = new OpLine();
            _instructionMap.Add(node, instruction);

            instruction.File = Visit(node.File);
            instruction.Value = Visit(node.Value);
            instruction.Column = Visit(node.Column);
            return instruction;
        }
        protected virtual OpExtension VisitExtension(Nodes.Extension node)
        {
            var instruction = new OpExtension();
            _instructionMap.Add(node, instruction);

            instruction.Name = Visit(node.Name);
            return instruction;
        }
        protected virtual OpExtInstImport VisitExtInstImport(Nodes.ExtInstImport node)
        {
            var instruction = new OpExtInstImport();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.Name = Visit(node.Name);
            return instruction;
        }
        protected virtual OpExtInst VisitExtInst(Nodes.ExtInst node)
        {
            var instruction = new OpExtInst();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Set = Visit(node.Set);
            instruction.Instruction = Visit(node.Instruction);
            instruction.Operands = Visit(node.Operands);
            return instruction;
        }
        protected virtual OpMemoryModel VisitMemoryModel(Nodes.MemoryModel node)
        {
            var instruction = new OpMemoryModel();
            _instructionMap.Add(node, instruction);

            instruction.AddressingModel = Visit(node.AddressingModel);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpEntryPoint VisitEntryPoint(Nodes.EntryPoint node)
        {
            var instruction = new OpEntryPoint();
            _instructionMap.Add(node, instruction);

            instruction.ExecutionModel = Visit(node.ExecutionModel);
            instruction.Value = Visit(node.Value);
            instruction.Name = Visit(node.Name);
            instruction.Interface = Visit(node.Interface);
            return instruction;
        }
        protected virtual OpExecutionMode VisitExecutionMode(Nodes.ExecutionMode node)
        {
            var instruction = new OpExecutionMode();
            _instructionMap.Add(node, instruction);

            instruction.EntryPoint = Visit(node.EntryPoint);
            instruction.Mode = Visit(node.Mode);
            return instruction;
        }
        protected virtual OpCapability VisitCapability(Nodes.Capability node)
        {
            var instruction = new OpCapability();
            _instructionMap.Add(node, instruction);

            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpTypeVoid VisitTypeVoid(Types.TypeVoid node)
        {
            var instruction = new OpTypeVoid();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            //This type should be handled manually. See SpirvInstructionsBuilder.VisitTypeVoid for details.
            return instruction;
        }
        protected virtual OpTypeBool VisitTypeBool(Types.TypeBool node)
        {
            var instruction = new OpTypeBool();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            //This type should be handled manually. See SpirvInstructionsBuilder.VisitTypeBool for details.
            return instruction;
        }
        protected virtual OpTypeInt VisitTypeInt(Types.TypeInt node)
        {
            var instruction = new OpTypeInt();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            //This type should be handled manually. See SpirvInstructionsBuilder.VisitTypeInt for details.
            return instruction;
        }
        protected virtual OpTypeFloat VisitTypeFloat(Types.TypeFloat node)
        {
            var instruction = new OpTypeFloat();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            //This type should be handled manually. See SpirvInstructionsBuilder.VisitTypeFloat for details.
            return instruction;
        }
        protected virtual OpTypeVector VisitTypeVector(Types.TypeVector node)
        {
            var instruction = new OpTypeVector();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            //This type should be handled manually. See SpirvInstructionsBuilder.VisitTypeVector for details.
            return instruction;
        }
        protected virtual OpTypeMatrix VisitTypeMatrix(Types.TypeMatrix node)
        {
            var instruction = new OpTypeMatrix();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            //This type should be handled manually. See SpirvInstructionsBuilder.VisitTypeMatrix for details.
            return instruction;
        }
        protected virtual OpTypeImage VisitTypeImage(Types.TypeImage node)
        {
            var instruction = new OpTypeImage();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.SampledType = Visit(node.SampledType);
            instruction.Dim = Visit(node.Dim);
            instruction.Depth = Visit(node.Depth);
            instruction.Arrayed = Visit(node.Arrayed);
            instruction.MS = Visit(node.MS);
            instruction.Sampled = Visit(node.Sampled);
            instruction.ImageFormat = Visit(node.ImageFormat);
            instruction.AccessQualifier = Visit(node.AccessQualifier);
            return instruction;
        }
        protected virtual OpTypeSampler VisitTypeSampler(Types.TypeSampler node)
        {
            var instruction = new OpTypeSampler();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeSampledImage VisitTypeSampledImage(Types.TypeSampledImage node)
        {
            var instruction = new OpTypeSampledImage();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.ImageType = Visit(node.ImageType);
            return instruction;
        }
        protected virtual OpTypeArray VisitTypeArray(Types.TypeArray node)
        {
            var instruction = new OpTypeArray();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            //This type should be handled manually. See SpirvInstructionsBuilder.VisitTypeArray for details.
            return instruction;
        }
        protected virtual OpTypeRuntimeArray VisitTypeRuntimeArray(Types.TypeRuntimeArray node)
        {
            var instruction = new OpTypeRuntimeArray();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.ElementType = Visit(node.ElementType);
            return instruction;
        }
        protected virtual OpTypeStruct VisitTypeStruct(Types.TypeStruct node)
        {
            var instruction = new OpTypeStruct();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            //This type should be handled manually. See SpirvInstructionsBuilder.VisitTypeStruct for details.
            return instruction;
        }
        protected virtual OpTypeOpaque VisitTypeOpaque(Types.TypeOpaque node)
        {
            var instruction = new OpTypeOpaque();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.Thenameoftheopaquetype = Visit(node.Thenameoftheopaquetype);
            return instruction;
        }
        protected virtual OpTypePointer VisitTypePointer(Types.TypePointer node)
        {
            var instruction = new OpTypePointer();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.StorageClass = Visit(node.StorageClass);
            instruction.Type = Visit(node.Type);
            return instruction;
        }
        protected virtual OpTypeFunction VisitTypeFunction(Types.TypeFunction node)
        {
            var instruction = new OpTypeFunction();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            //This type should be handled manually. See SpirvInstructionsBuilder.VisitTypeFunction for details.
            return instruction;
        }
        protected virtual OpTypeEvent VisitTypeEvent(Types.TypeEvent node)
        {
            var instruction = new OpTypeEvent();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeDeviceEvent VisitTypeDeviceEvent(Types.TypeDeviceEvent node)
        {
            var instruction = new OpTypeDeviceEvent();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeReserveId VisitTypeReserveId(Types.TypeReserveId node)
        {
            var instruction = new OpTypeReserveId();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeQueue VisitTypeQueue(Types.TypeQueue node)
        {
            var instruction = new OpTypeQueue();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypePipe VisitTypePipe(Types.TypePipe node)
        {
            var instruction = new OpTypePipe();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.Qualifier = Visit(node.Qualifier);
            return instruction;
        }
        protected virtual OpTypeForwardPointer VisitTypeForwardPointer(Types.TypeForwardPointer node)
        {
            var instruction = new OpTypeForwardPointer();
            _instructionMap.Add(node, instruction);
            instruction.PointerType = Visit(node.PointerType);
            instruction.StorageClass = Visit(node.StorageClass);
            return instruction;
        }
        protected virtual OpConstantTrue VisitConstantTrue(Nodes.ConstantTrue node)
        {
            var instruction = new OpConstantTrue();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpConstantFalse VisitConstantFalse(Nodes.ConstantFalse node)
        {
            var instruction = new OpConstantFalse();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpConstant VisitConstant(Nodes.Constant node)
        {
            var instruction = new OpConstant();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpConstantComposite VisitConstantComposite(Nodes.ConstantComposite node)
        {
            var instruction = new OpConstantComposite();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Constituents = Visit(node.Constituents);
            return instruction;
        }
        protected virtual OpConstantSampler VisitConstantSampler(Nodes.ConstantSampler node)
        {
            var instruction = new OpConstantSampler();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SamplerAddressingMode = Visit(node.SamplerAddressingMode);
            instruction.Param = Visit(node.Param);
            instruction.SamplerFilterMode = Visit(node.SamplerFilterMode);
            return instruction;
        }
        protected virtual OpConstantNull VisitConstantNull(Nodes.ConstantNull node)
        {
            var instruction = new OpConstantNull();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpSpecConstantTrue VisitSpecConstantTrue(Nodes.SpecConstantTrue node)
        {
            var instruction = new OpSpecConstantTrue();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpSpecConstantFalse VisitSpecConstantFalse(Nodes.SpecConstantFalse node)
        {
            var instruction = new OpSpecConstantFalse();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpSpecConstant VisitSpecConstant(Nodes.SpecConstant node)
        {
            var instruction = new OpSpecConstant();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpSpecConstantComposite VisitSpecConstantComposite(Nodes.SpecConstantComposite node)
        {
            var instruction = new OpSpecConstantComposite();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Constituents = Visit(node.Constituents);
            return instruction;
        }
        protected virtual OpSpecConstantOp VisitSpecConstantOp(Nodes.SpecConstantOp node)
        {
            var instruction = new OpSpecConstantOp();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Opcode = Visit(node.Opcode);
            return instruction;
        }
        protected virtual OpFunction VisitFunction(Nodes.Function node)
        {
            var instruction = new OpFunction();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.FunctionControl = Visit(node.FunctionControl);
            instruction.FunctionType = Visit(node.FunctionType);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpFunctionParameter VisitFunctionParameter(Nodes.FunctionParameter node)
        {
            var instruction = new OpFunctionParameter();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpFunctionEnd VisitFunctionEnd(Nodes.FunctionEnd node)
        {
            var instruction = new OpFunctionEnd();
            _instructionMap.Add(node, instruction);

            Visit(node.Next);

            return instruction;
        }
        protected virtual OpFunctionCall VisitFunctionCall(Nodes.FunctionCall node)
        {
            var instruction = new OpFunctionCall();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Function = Visit(node.Function);
            instruction.Arguments = Visit(node.Arguments);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpVariable VisitVariable(Nodes.Variable node)
        {
            var instruction = new OpVariable();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.StorageClass = Visit(node.StorageClass);
            instruction.Initializer = Visit(node.Initializer);
            return instruction;
        }
        protected virtual OpImageTexelPointer VisitImageTexelPointer(Nodes.ImageTexelPointer node)
        {
            var instruction = new OpImageTexelPointer();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.Sample = Visit(node.Sample);
            return instruction;
        }
        protected virtual OpLoad VisitLoad(Nodes.Load node)
        {
            var instruction = new OpLoad();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.MemoryAccess = Visit(node.MemoryAccess);
            return instruction;
        }
        protected virtual OpStore VisitStore(Nodes.Store node)
        {
            var instruction = new OpStore();
            _instructionMap.Add(node, instruction);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Object = Visit(node.Object);
            instruction.MemoryAccess = Visit(node.MemoryAccess);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpCopyMemory VisitCopyMemory(Nodes.CopyMemory node)
        {
            var instruction = new OpCopyMemory();
            _instructionMap.Add(node, instruction);

            instruction.Target = Visit(node.Target);
            instruction.Source = Visit(node.Source);
            instruction.MemoryAccess = Visit(node.MemoryAccess);
            instruction.MemoryAccess2 = Visit(node.MemoryAccess2);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpCopyMemorySized VisitCopyMemorySized(Nodes.CopyMemorySized node)
        {
            var instruction = new OpCopyMemorySized();
            _instructionMap.Add(node, instruction);

            instruction.Target = Visit(node.Target);
            instruction.Source = Visit(node.Source);
            instruction.Size = Visit(node.Size);
            instruction.MemoryAccess = Visit(node.MemoryAccess);
            instruction.MemoryAccess2 = Visit(node.MemoryAccess2);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpAccessChain VisitAccessChain(Nodes.AccessChain node)
        {
            var instruction = new OpAccessChain();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Base = Visit(node.Base);
            instruction.Indexes = Visit(node.Indexes);
            return instruction;
        }
        protected virtual OpInBoundsAccessChain VisitInBoundsAccessChain(Nodes.InBoundsAccessChain node)
        {
            var instruction = new OpInBoundsAccessChain();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Base = Visit(node.Base);
            instruction.Indexes = Visit(node.Indexes);
            return instruction;
        }
        protected virtual OpPtrAccessChain VisitPtrAccessChain(Nodes.PtrAccessChain node)
        {
            var instruction = new OpPtrAccessChain();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Base = Visit(node.Base);
            instruction.Element = Visit(node.Element);
            instruction.Indexes = Visit(node.Indexes);
            return instruction;
        }
        protected virtual OpArrayLength VisitArrayLength(Nodes.ArrayLength node)
        {
            var instruction = new OpArrayLength();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Structure = Visit(node.Structure);
            instruction.Arraymember = Visit(node.Arraymember);
            return instruction;
        }
        protected virtual OpGenericPtrMemSemantics VisitGenericPtrMemSemantics(Nodes.GenericPtrMemSemantics node)
        {
            var instruction = new OpGenericPtrMemSemantics();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            return instruction;
        }
        protected virtual OpInBoundsPtrAccessChain VisitInBoundsPtrAccessChain(Nodes.InBoundsPtrAccessChain node)
        {
            var instruction = new OpInBoundsPtrAccessChain();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Base = Visit(node.Base);
            instruction.Element = Visit(node.Element);
            instruction.Indexes = Visit(node.Indexes);
            return instruction;
        }
        protected virtual OpDecorate VisitDecorate(Nodes.Decorate node)
        {
            var instruction = new OpDecorate();
            _instructionMap.Add(node, instruction);

            instruction.Target = Visit(node.Target);
            instruction.Decoration = Visit(node.Decoration);
            return instruction;
        }
        protected virtual OpMemberDecorate VisitMemberDecorate(Nodes.MemberDecorate node)
        {
            var instruction = new OpMemberDecorate();
            _instructionMap.Add(node, instruction);

            instruction.StructureType = Visit(node.StructureType);
            instruction.Member = Visit(node.Member);
            instruction.Decoration = Visit(node.Decoration);
            return instruction;
        }
        protected virtual OpDecorationGroup VisitDecorationGroup(Nodes.DecorationGroup node)
        {
            var instruction = new OpDecorationGroup();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpGroupDecorate VisitGroupDecorate(Nodes.GroupDecorate node)
        {
            var instruction = new OpGroupDecorate();
            _instructionMap.Add(node, instruction);

            instruction.DecorationGroup = Visit(node.DecorationGroup);
            instruction.Targets = Visit(node.Targets);
            return instruction;
        }
        protected virtual OpGroupMemberDecorate VisitGroupMemberDecorate(Nodes.GroupMemberDecorate node)
        {
            var instruction = new OpGroupMemberDecorate();
            _instructionMap.Add(node, instruction);

            instruction.DecorationGroup = Visit(node.DecorationGroup);
            instruction.Targets = Visit(node.Targets);
            return instruction;
        }
        protected virtual OpVectorExtractDynamic VisitVectorExtractDynamic(Nodes.VectorExtractDynamic node)
        {
            var instruction = new OpVectorExtractDynamic();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Vector = Visit(node.Vector);
            instruction.Index = Visit(node.Index);
            return instruction;
        }
        protected virtual OpVectorInsertDynamic VisitVectorInsertDynamic(Nodes.VectorInsertDynamic node)
        {
            var instruction = new OpVectorInsertDynamic();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Vector = Visit(node.Vector);
            instruction.Component = Visit(node.Component);
            instruction.Index = Visit(node.Index);
            return instruction;
        }
        protected virtual OpVectorShuffle VisitVectorShuffle(Nodes.VectorShuffle node)
        {
            var instruction = new OpVectorShuffle();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Vector1 = Visit(node.Vector1);
            instruction.Vector2 = Visit(node.Vector2);
            instruction.Components = Visit(node.Components);
            return instruction;
        }
        protected virtual OpCompositeConstruct VisitCompositeConstruct(Nodes.CompositeConstruct node)
        {
            var instruction = new OpCompositeConstruct();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Constituents = Visit(node.Constituents);
            return instruction;
        }
        protected virtual OpCompositeExtract VisitCompositeExtract(Nodes.CompositeExtract node)
        {
            var instruction = new OpCompositeExtract();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Composite = Visit(node.Composite);
            instruction.Indexes = Visit(node.Indexes);
            return instruction;
        }
        protected virtual OpCompositeInsert VisitCompositeInsert(Nodes.CompositeInsert node)
        {
            var instruction = new OpCompositeInsert();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Object = Visit(node.Object);
            instruction.Composite = Visit(node.Composite);
            instruction.Indexes = Visit(node.Indexes);
            return instruction;
        }
        protected virtual OpCopyObject VisitCopyObject(Nodes.CopyObject node)
        {
            var instruction = new OpCopyObject();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand = Visit(node.Operand);
            return instruction;
        }
        protected virtual OpTranspose VisitTranspose(Nodes.Transpose node)
        {
            var instruction = new OpTranspose();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Matrix = Visit(node.Matrix);
            return instruction;
        }
        protected virtual OpSampledImage VisitSampledImage(Nodes.SampledImage node)
        {
            var instruction = new OpSampledImage();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            instruction.Sampler = Visit(node.Sampler);
            return instruction;
        }
        protected virtual OpImageSampleImplicitLod VisitImageSampleImplicitLod(Nodes.ImageSampleImplicitLod node)
        {
            var instruction = new OpImageSampleImplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSampleExplicitLod VisitImageSampleExplicitLod(Nodes.ImageSampleExplicitLod node)
        {
            var instruction = new OpImageSampleExplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSampleDrefImplicitLod VisitImageSampleDrefImplicitLod(Nodes.ImageSampleDrefImplicitLod node)
        {
            var instruction = new OpImageSampleDrefImplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.D_ref = Visit(node.D_ref);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSampleDrefExplicitLod VisitImageSampleDrefExplicitLod(Nodes.ImageSampleDrefExplicitLod node)
        {
            var instruction = new OpImageSampleDrefExplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.D_ref = Visit(node.D_ref);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSampleProjImplicitLod VisitImageSampleProjImplicitLod(Nodes.ImageSampleProjImplicitLod node)
        {
            var instruction = new OpImageSampleProjImplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSampleProjExplicitLod VisitImageSampleProjExplicitLod(Nodes.ImageSampleProjExplicitLod node)
        {
            var instruction = new OpImageSampleProjExplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSampleProjDrefImplicitLod VisitImageSampleProjDrefImplicitLod(Nodes.ImageSampleProjDrefImplicitLod node)
        {
            var instruction = new OpImageSampleProjDrefImplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.D_ref = Visit(node.D_ref);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSampleProjDrefExplicitLod VisitImageSampleProjDrefExplicitLod(Nodes.ImageSampleProjDrefExplicitLod node)
        {
            var instruction = new OpImageSampleProjDrefExplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.D_ref = Visit(node.D_ref);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageFetch VisitImageFetch(Nodes.ImageFetch node)
        {
            var instruction = new OpImageFetch();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageGather VisitImageGather(Nodes.ImageGather node)
        {
            var instruction = new OpImageGather();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.Component = Visit(node.Component);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageDrefGather VisitImageDrefGather(Nodes.ImageDrefGather node)
        {
            var instruction = new OpImageDrefGather();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.D_ref = Visit(node.D_ref);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageRead VisitImageRead(Nodes.ImageRead node)
        {
            var instruction = new OpImageRead();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageWrite VisitImageWrite(Nodes.ImageWrite node)
        {
            var instruction = new OpImageWrite();
            _instructionMap.Add(node, instruction);

            instruction.Image = Visit(node.Image);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.Texel = Visit(node.Texel);
            instruction.ImageOperands = Visit(node.ImageOperands);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpImage VisitImage(Nodes.Image node)
        {
            var instruction = new OpImage();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            return instruction;
        }
        protected virtual OpImageQueryFormat VisitImageQueryFormat(Nodes.ImageQueryFormat node)
        {
            var instruction = new OpImageQueryFormat();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            return instruction;
        }
        protected virtual OpImageQueryOrder VisitImageQueryOrder(Nodes.ImageQueryOrder node)
        {
            var instruction = new OpImageQueryOrder();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            return instruction;
        }
        protected virtual OpImageQuerySizeLod VisitImageQuerySizeLod(Nodes.ImageQuerySizeLod node)
        {
            var instruction = new OpImageQuerySizeLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            instruction.LevelofDetail = Visit(node.LevelofDetail);
            return instruction;
        }
        protected virtual OpImageQuerySize VisitImageQuerySize(Nodes.ImageQuerySize node)
        {
            var instruction = new OpImageQuerySize();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            return instruction;
        }
        protected virtual OpImageQueryLod VisitImageQueryLod(Nodes.ImageQueryLod node)
        {
            var instruction = new OpImageQueryLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            return instruction;
        }
        protected virtual OpImageQueryLevels VisitImageQueryLevels(Nodes.ImageQueryLevels node)
        {
            var instruction = new OpImageQueryLevels();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            return instruction;
        }
        protected virtual OpImageQuerySamples VisitImageQuerySamples(Nodes.ImageQuerySamples node)
        {
            var instruction = new OpImageQuerySamples();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            return instruction;
        }
        protected virtual OpConvertFToU VisitConvertFToU(Nodes.ConvertFToU node)
        {
            var instruction = new OpConvertFToU();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.FloatValue = Visit(node.FloatValue);
            return instruction;
        }
        protected virtual OpConvertFToS VisitConvertFToS(Nodes.ConvertFToS node)
        {
            var instruction = new OpConvertFToS();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.FloatValue = Visit(node.FloatValue);
            return instruction;
        }
        protected virtual OpConvertSToF VisitConvertSToF(Nodes.ConvertSToF node)
        {
            var instruction = new OpConvertSToF();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SignedValue = Visit(node.SignedValue);
            return instruction;
        }
        protected virtual OpConvertUToF VisitConvertUToF(Nodes.ConvertUToF node)
        {
            var instruction = new OpConvertUToF();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.UnsignedValue = Visit(node.UnsignedValue);
            return instruction;
        }
        protected virtual OpUConvert VisitUConvert(Nodes.UConvert node)
        {
            var instruction = new OpUConvert();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.UnsignedValue = Visit(node.UnsignedValue);
            return instruction;
        }
        protected virtual OpSConvert VisitSConvert(Nodes.SConvert node)
        {
            var instruction = new OpSConvert();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SignedValue = Visit(node.SignedValue);
            return instruction;
        }
        protected virtual OpFConvert VisitFConvert(Nodes.FConvert node)
        {
            var instruction = new OpFConvert();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.FloatValue = Visit(node.FloatValue);
            return instruction;
        }
        protected virtual OpQuantizeToF16 VisitQuantizeToF16(Nodes.QuantizeToF16 node)
        {
            var instruction = new OpQuantizeToF16();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpConvertPtrToU VisitConvertPtrToU(Nodes.ConvertPtrToU node)
        {
            var instruction = new OpConvertPtrToU();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            return instruction;
        }
        protected virtual OpSatConvertSToU VisitSatConvertSToU(Nodes.SatConvertSToU node)
        {
            var instruction = new OpSatConvertSToU();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SignedValue = Visit(node.SignedValue);
            return instruction;
        }
        protected virtual OpSatConvertUToS VisitSatConvertUToS(Nodes.SatConvertUToS node)
        {
            var instruction = new OpSatConvertUToS();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.UnsignedValue = Visit(node.UnsignedValue);
            return instruction;
        }
        protected virtual OpConvertUToPtr VisitConvertUToPtr(Nodes.ConvertUToPtr node)
        {
            var instruction = new OpConvertUToPtr();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.IntegerValue = Visit(node.IntegerValue);
            return instruction;
        }
        protected virtual OpPtrCastToGeneric VisitPtrCastToGeneric(Nodes.PtrCastToGeneric node)
        {
            var instruction = new OpPtrCastToGeneric();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            return instruction;
        }
        protected virtual OpGenericCastToPtr VisitGenericCastToPtr(Nodes.GenericCastToPtr node)
        {
            var instruction = new OpGenericCastToPtr();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            return instruction;
        }
        protected virtual OpGenericCastToPtrExplicit VisitGenericCastToPtrExplicit(Nodes.GenericCastToPtrExplicit node)
        {
            var instruction = new OpGenericCastToPtrExplicit();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Storage = Visit(node.Storage);
            return instruction;
        }
        protected virtual OpBitcast VisitBitcast(Nodes.Bitcast node)
        {
            var instruction = new OpBitcast();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand = Visit(node.Operand);
            return instruction;
        }
        protected virtual OpSNegate VisitSNegate(Nodes.SNegate node)
        {
            var instruction = new OpSNegate();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand = Visit(node.Operand);
            return instruction;
        }
        protected virtual OpFNegate VisitFNegate(Nodes.FNegate node)
        {
            var instruction = new OpFNegate();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand = Visit(node.Operand);
            return instruction;
        }
        protected virtual OpIAdd VisitIAdd(Nodes.IAdd node)
        {
            var instruction = new OpIAdd();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFAdd VisitFAdd(Nodes.FAdd node)
        {
            var instruction = new OpFAdd();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpISub VisitISub(Nodes.ISub node)
        {
            var instruction = new OpISub();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFSub VisitFSub(Nodes.FSub node)
        {
            var instruction = new OpFSub();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpIMul VisitIMul(Nodes.IMul node)
        {
            var instruction = new OpIMul();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFMul VisitFMul(Nodes.FMul node)
        {
            var instruction = new OpFMul();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpUDiv VisitUDiv(Nodes.UDiv node)
        {
            var instruction = new OpUDiv();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpSDiv VisitSDiv(Nodes.SDiv node)
        {
            var instruction = new OpSDiv();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFDiv VisitFDiv(Nodes.FDiv node)
        {
            var instruction = new OpFDiv();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpUMod VisitUMod(Nodes.UMod node)
        {
            var instruction = new OpUMod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpSRem VisitSRem(Nodes.SRem node)
        {
            var instruction = new OpSRem();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpSMod VisitSMod(Nodes.SMod node)
        {
            var instruction = new OpSMod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFRem VisitFRem(Nodes.FRem node)
        {
            var instruction = new OpFRem();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFMod VisitFMod(Nodes.FMod node)
        {
            var instruction = new OpFMod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpVectorTimesScalar VisitVectorTimesScalar(Nodes.VectorTimesScalar node)
        {
            var instruction = new OpVectorTimesScalar();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Vector = Visit(node.Vector);
            instruction.Scalar = Visit(node.Scalar);
            return instruction;
        }
        protected virtual OpMatrixTimesScalar VisitMatrixTimesScalar(Nodes.MatrixTimesScalar node)
        {
            var instruction = new OpMatrixTimesScalar();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Matrix = Visit(node.Matrix);
            instruction.Scalar = Visit(node.Scalar);
            return instruction;
        }
        protected virtual OpVectorTimesMatrix VisitVectorTimesMatrix(Nodes.VectorTimesMatrix node)
        {
            var instruction = new OpVectorTimesMatrix();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Vector = Visit(node.Vector);
            instruction.Matrix = Visit(node.Matrix);
            return instruction;
        }
        protected virtual OpMatrixTimesVector VisitMatrixTimesVector(Nodes.MatrixTimesVector node)
        {
            var instruction = new OpMatrixTimesVector();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Matrix = Visit(node.Matrix);
            instruction.Vector = Visit(node.Vector);
            return instruction;
        }
        protected virtual OpMatrixTimesMatrix VisitMatrixTimesMatrix(Nodes.MatrixTimesMatrix node)
        {
            var instruction = new OpMatrixTimesMatrix();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.LeftMatrix = Visit(node.LeftMatrix);
            instruction.RightMatrix = Visit(node.RightMatrix);
            return instruction;
        }
        protected virtual OpOuterProduct VisitOuterProduct(Nodes.OuterProduct node)
        {
            var instruction = new OpOuterProduct();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Vector1 = Visit(node.Vector1);
            instruction.Vector2 = Visit(node.Vector2);
            return instruction;
        }
        protected virtual OpDot VisitDot(Nodes.Dot node)
        {
            var instruction = new OpDot();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Vector1 = Visit(node.Vector1);
            instruction.Vector2 = Visit(node.Vector2);
            return instruction;
        }
        protected virtual OpIAddCarry VisitIAddCarry(Nodes.IAddCarry node)
        {
            var instruction = new OpIAddCarry();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpISubBorrow VisitISubBorrow(Nodes.ISubBorrow node)
        {
            var instruction = new OpISubBorrow();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpUMulExtended VisitUMulExtended(Nodes.UMulExtended node)
        {
            var instruction = new OpUMulExtended();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpSMulExtended VisitSMulExtended(Nodes.SMulExtended node)
        {
            var instruction = new OpSMulExtended();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpAny VisitAny(Nodes.Any node)
        {
            var instruction = new OpAny();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Vector = Visit(node.Vector);
            return instruction;
        }
        protected virtual OpAll VisitAll(Nodes.All node)
        {
            var instruction = new OpAll();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Vector = Visit(node.Vector);
            return instruction;
        }
        protected virtual OpIsNan VisitIsNan(Nodes.IsNan node)
        {
            var instruction = new OpIsNan();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.x = Visit(node.x);
            return instruction;
        }
        protected virtual OpIsInf VisitIsInf(Nodes.IsInf node)
        {
            var instruction = new OpIsInf();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.x = Visit(node.x);
            return instruction;
        }
        protected virtual OpIsFinite VisitIsFinite(Nodes.IsFinite node)
        {
            var instruction = new OpIsFinite();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.x = Visit(node.x);
            return instruction;
        }
        protected virtual OpIsNormal VisitIsNormal(Nodes.IsNormal node)
        {
            var instruction = new OpIsNormal();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.x = Visit(node.x);
            return instruction;
        }
        protected virtual OpSignBitSet VisitSignBitSet(Nodes.SignBitSet node)
        {
            var instruction = new OpSignBitSet();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.x = Visit(node.x);
            return instruction;
        }
        protected virtual OpLessOrGreater VisitLessOrGreater(Nodes.LessOrGreater node)
        {
            var instruction = new OpLessOrGreater();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.x = Visit(node.x);
            instruction.y = Visit(node.y);
            return instruction;
        }
        protected virtual OpOrdered VisitOrdered(Nodes.Ordered node)
        {
            var instruction = new OpOrdered();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.x = Visit(node.x);
            instruction.y = Visit(node.y);
            return instruction;
        }
        protected virtual OpUnordered VisitUnordered(Nodes.Unordered node)
        {
            var instruction = new OpUnordered();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.x = Visit(node.x);
            instruction.y = Visit(node.y);
            return instruction;
        }
        protected virtual OpLogicalEqual VisitLogicalEqual(Nodes.LogicalEqual node)
        {
            var instruction = new OpLogicalEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpLogicalNotEqual VisitLogicalNotEqual(Nodes.LogicalNotEqual node)
        {
            var instruction = new OpLogicalNotEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpLogicalOr VisitLogicalOr(Nodes.LogicalOr node)
        {
            var instruction = new OpLogicalOr();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpLogicalAnd VisitLogicalAnd(Nodes.LogicalAnd node)
        {
            var instruction = new OpLogicalAnd();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpLogicalNot VisitLogicalNot(Nodes.LogicalNot node)
        {
            var instruction = new OpLogicalNot();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand = Visit(node.Operand);
            return instruction;
        }
        protected virtual OpSelect VisitSelect(Nodes.Select node)
        {
            var instruction = new OpSelect();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Condition = Visit(node.Condition);
            instruction.Object1 = Visit(node.Object1);
            instruction.Object2 = Visit(node.Object2);
            return instruction;
        }
        protected virtual OpIEqual VisitIEqual(Nodes.IEqual node)
        {
            var instruction = new OpIEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpINotEqual VisitINotEqual(Nodes.INotEqual node)
        {
            var instruction = new OpINotEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpUGreaterThan VisitUGreaterThan(Nodes.UGreaterThan node)
        {
            var instruction = new OpUGreaterThan();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpSGreaterThan VisitSGreaterThan(Nodes.SGreaterThan node)
        {
            var instruction = new OpSGreaterThan();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpUGreaterThanEqual VisitUGreaterThanEqual(Nodes.UGreaterThanEqual node)
        {
            var instruction = new OpUGreaterThanEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpSGreaterThanEqual VisitSGreaterThanEqual(Nodes.SGreaterThanEqual node)
        {
            var instruction = new OpSGreaterThanEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpULessThan VisitULessThan(Nodes.ULessThan node)
        {
            var instruction = new OpULessThan();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpSLessThan VisitSLessThan(Nodes.SLessThan node)
        {
            var instruction = new OpSLessThan();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpULessThanEqual VisitULessThanEqual(Nodes.ULessThanEqual node)
        {
            var instruction = new OpULessThanEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpSLessThanEqual VisitSLessThanEqual(Nodes.SLessThanEqual node)
        {
            var instruction = new OpSLessThanEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFOrdEqual VisitFOrdEqual(Nodes.FOrdEqual node)
        {
            var instruction = new OpFOrdEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFUnordEqual VisitFUnordEqual(Nodes.FUnordEqual node)
        {
            var instruction = new OpFUnordEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFOrdNotEqual VisitFOrdNotEqual(Nodes.FOrdNotEqual node)
        {
            var instruction = new OpFOrdNotEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFUnordNotEqual VisitFUnordNotEqual(Nodes.FUnordNotEqual node)
        {
            var instruction = new OpFUnordNotEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFOrdLessThan VisitFOrdLessThan(Nodes.FOrdLessThan node)
        {
            var instruction = new OpFOrdLessThan();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFUnordLessThan VisitFUnordLessThan(Nodes.FUnordLessThan node)
        {
            var instruction = new OpFUnordLessThan();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFOrdGreaterThan VisitFOrdGreaterThan(Nodes.FOrdGreaterThan node)
        {
            var instruction = new OpFOrdGreaterThan();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFUnordGreaterThan VisitFUnordGreaterThan(Nodes.FUnordGreaterThan node)
        {
            var instruction = new OpFUnordGreaterThan();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFOrdLessThanEqual VisitFOrdLessThanEqual(Nodes.FOrdLessThanEqual node)
        {
            var instruction = new OpFOrdLessThanEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFUnordLessThanEqual VisitFUnordLessThanEqual(Nodes.FUnordLessThanEqual node)
        {
            var instruction = new OpFUnordLessThanEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFOrdGreaterThanEqual VisitFOrdGreaterThanEqual(Nodes.FOrdGreaterThanEqual node)
        {
            var instruction = new OpFOrdGreaterThanEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpFUnordGreaterThanEqual VisitFUnordGreaterThanEqual(Nodes.FUnordGreaterThanEqual node)
        {
            var instruction = new OpFUnordGreaterThanEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpShiftRightLogical VisitShiftRightLogical(Nodes.ShiftRightLogical node)
        {
            var instruction = new OpShiftRightLogical();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Base = Visit(node.Base);
            instruction.Shift = Visit(node.Shift);
            return instruction;
        }
        protected virtual OpShiftRightArithmetic VisitShiftRightArithmetic(Nodes.ShiftRightArithmetic node)
        {
            var instruction = new OpShiftRightArithmetic();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Base = Visit(node.Base);
            instruction.Shift = Visit(node.Shift);
            return instruction;
        }
        protected virtual OpShiftLeftLogical VisitShiftLeftLogical(Nodes.ShiftLeftLogical node)
        {
            var instruction = new OpShiftLeftLogical();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Base = Visit(node.Base);
            instruction.Shift = Visit(node.Shift);
            return instruction;
        }
        protected virtual OpBitwiseOr VisitBitwiseOr(Nodes.BitwiseOr node)
        {
            var instruction = new OpBitwiseOr();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpBitwiseXor VisitBitwiseXor(Nodes.BitwiseXor node)
        {
            var instruction = new OpBitwiseXor();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpBitwiseAnd VisitBitwiseAnd(Nodes.BitwiseAnd node)
        {
            var instruction = new OpBitwiseAnd();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpNot VisitNot(Nodes.Not node)
        {
            var instruction = new OpNot();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand = Visit(node.Operand);
            return instruction;
        }
        protected virtual OpBitFieldInsert VisitBitFieldInsert(Nodes.BitFieldInsert node)
        {
            var instruction = new OpBitFieldInsert();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Base = Visit(node.Base);
            instruction.Insert = Visit(node.Insert);
            instruction.Offset = Visit(node.Offset);
            instruction.Count = Visit(node.Count);
            return instruction;
        }
        protected virtual OpBitFieldSExtract VisitBitFieldSExtract(Nodes.BitFieldSExtract node)
        {
            var instruction = new OpBitFieldSExtract();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Base = Visit(node.Base);
            instruction.Offset = Visit(node.Offset);
            instruction.Count = Visit(node.Count);
            return instruction;
        }
        protected virtual OpBitFieldUExtract VisitBitFieldUExtract(Nodes.BitFieldUExtract node)
        {
            var instruction = new OpBitFieldUExtract();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Base = Visit(node.Base);
            instruction.Offset = Visit(node.Offset);
            instruction.Count = Visit(node.Count);
            return instruction;
        }
        protected virtual OpBitReverse VisitBitReverse(Nodes.BitReverse node)
        {
            var instruction = new OpBitReverse();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Base = Visit(node.Base);
            return instruction;
        }
        protected virtual OpBitCount VisitBitCount(Nodes.BitCount node)
        {
            var instruction = new OpBitCount();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Base = Visit(node.Base);
            return instruction;
        }
        protected virtual OpDPdx VisitDPdx(Nodes.DPdx node)
        {
            var instruction = new OpDPdx();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.P = Visit(node.P);
            return instruction;
        }
        protected virtual OpDPdy VisitDPdy(Nodes.DPdy node)
        {
            var instruction = new OpDPdy();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.P = Visit(node.P);
            return instruction;
        }
        protected virtual OpFwidth VisitFwidth(Nodes.Fwidth node)
        {
            var instruction = new OpFwidth();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.P = Visit(node.P);
            return instruction;
        }
        protected virtual OpDPdxFine VisitDPdxFine(Nodes.DPdxFine node)
        {
            var instruction = new OpDPdxFine();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.P = Visit(node.P);
            return instruction;
        }
        protected virtual OpDPdyFine VisitDPdyFine(Nodes.DPdyFine node)
        {
            var instruction = new OpDPdyFine();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.P = Visit(node.P);
            return instruction;
        }
        protected virtual OpFwidthFine VisitFwidthFine(Nodes.FwidthFine node)
        {
            var instruction = new OpFwidthFine();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.P = Visit(node.P);
            return instruction;
        }
        protected virtual OpDPdxCoarse VisitDPdxCoarse(Nodes.DPdxCoarse node)
        {
            var instruction = new OpDPdxCoarse();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.P = Visit(node.P);
            return instruction;
        }
        protected virtual OpDPdyCoarse VisitDPdyCoarse(Nodes.DPdyCoarse node)
        {
            var instruction = new OpDPdyCoarse();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.P = Visit(node.P);
            return instruction;
        }
        protected virtual OpFwidthCoarse VisitFwidthCoarse(Nodes.FwidthCoarse node)
        {
            var instruction = new OpFwidthCoarse();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.P = Visit(node.P);
            return instruction;
        }
        protected virtual OpEmitVertex VisitEmitVertex(Nodes.EmitVertex node)
        {
            var instruction = new OpEmitVertex();
            _instructionMap.Add(node, instruction);

            Visit(node.Next);

            return instruction;
        }
        protected virtual OpEndPrimitive VisitEndPrimitive(Nodes.EndPrimitive node)
        {
            var instruction = new OpEndPrimitive();
            _instructionMap.Add(node, instruction);

            Visit(node.Next);

            return instruction;
        }
        protected virtual OpEmitStreamVertex VisitEmitStreamVertex(Nodes.EmitStreamVertex node)
        {
            var instruction = new OpEmitStreamVertex();
            _instructionMap.Add(node, instruction);

            instruction.Stream = Visit(node.Stream);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpEndStreamPrimitive VisitEndStreamPrimitive(Nodes.EndStreamPrimitive node)
        {
            var instruction = new OpEndStreamPrimitive();
            _instructionMap.Add(node, instruction);

            instruction.Stream = Visit(node.Stream);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpControlBarrier VisitControlBarrier(Nodes.ControlBarrier node)
        {
            var instruction = new OpControlBarrier();
            _instructionMap.Add(node, instruction);

            instruction.Execution = Visit(node.Execution);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpMemoryBarrier VisitMemoryBarrier(Nodes.MemoryBarrier node)
        {
            var instruction = new OpMemoryBarrier();
            _instructionMap.Add(node, instruction);

            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpAtomicLoad VisitAtomicLoad(Nodes.AtomicLoad node)
        {
            var instruction = new OpAtomicLoad();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            return instruction;
        }
        protected virtual OpAtomicStore VisitAtomicStore(Nodes.AtomicStore node)
        {
            var instruction = new OpAtomicStore();
            _instructionMap.Add(node, instruction);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            instruction.Value = Visit(node.Value);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpAtomicExchange VisitAtomicExchange(Nodes.AtomicExchange node)
        {
            var instruction = new OpAtomicExchange();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpAtomicCompareExchange VisitAtomicCompareExchange(Nodes.AtomicCompareExchange node)
        {
            var instruction = new OpAtomicCompareExchange();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Equal = Visit(node.Equal);
            instruction.Unequal = Visit(node.Unequal);
            instruction.Value = Visit(node.Value);
            instruction.Comparator = Visit(node.Comparator);
            return instruction;
        }
        protected virtual OpAtomicCompareExchangeWeak VisitAtomicCompareExchangeWeak(Nodes.AtomicCompareExchangeWeak node)
        {
            var instruction = new OpAtomicCompareExchangeWeak();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Equal = Visit(node.Equal);
            instruction.Unequal = Visit(node.Unequal);
            instruction.Value = Visit(node.Value);
            instruction.Comparator = Visit(node.Comparator);
            return instruction;
        }
        protected virtual OpAtomicIIncrement VisitAtomicIIncrement(Nodes.AtomicIIncrement node)
        {
            var instruction = new OpAtomicIIncrement();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            return instruction;
        }
        protected virtual OpAtomicIDecrement VisitAtomicIDecrement(Nodes.AtomicIDecrement node)
        {
            var instruction = new OpAtomicIDecrement();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            return instruction;
        }
        protected virtual OpAtomicIAdd VisitAtomicIAdd(Nodes.AtomicIAdd node)
        {
            var instruction = new OpAtomicIAdd();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpAtomicISub VisitAtomicISub(Nodes.AtomicISub node)
        {
            var instruction = new OpAtomicISub();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpAtomicSMin VisitAtomicSMin(Nodes.AtomicSMin node)
        {
            var instruction = new OpAtomicSMin();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpAtomicUMin VisitAtomicUMin(Nodes.AtomicUMin node)
        {
            var instruction = new OpAtomicUMin();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpAtomicSMax VisitAtomicSMax(Nodes.AtomicSMax node)
        {
            var instruction = new OpAtomicSMax();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpAtomicUMax VisitAtomicUMax(Nodes.AtomicUMax node)
        {
            var instruction = new OpAtomicUMax();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpAtomicAnd VisitAtomicAnd(Nodes.AtomicAnd node)
        {
            var instruction = new OpAtomicAnd();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpAtomicOr VisitAtomicOr(Nodes.AtomicOr node)
        {
            var instruction = new OpAtomicOr();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpAtomicXor VisitAtomicXor(Nodes.AtomicXor node)
        {
            var instruction = new OpAtomicXor();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpPhi VisitPhi(Nodes.Phi node)
        {
            var instruction = new OpPhi();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.VariableParent = Visit(node.VariableParent);
            return instruction;
        }
        protected virtual OpLoopMerge VisitLoopMerge(Nodes.LoopMerge node)
        {
            var instruction = new OpLoopMerge();
            _instructionMap.Add(node, instruction);

            instruction.MergeBlock = Visit(node.MergeBlock);
            instruction.ContinueTarget = Visit(node.ContinueTarget);
            instruction.LoopControl = Visit(node.LoopControl);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpSelectionMerge VisitSelectionMerge(Nodes.SelectionMerge node)
        {
            var instruction = new OpSelectionMerge();
            _instructionMap.Add(node, instruction);

            instruction.MergeBlock = Visit(node.MergeBlock);
            instruction.SelectionControl = Visit(node.SelectionControl);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpLabel VisitLabel(Nodes.Label node)
        {
            var instruction = new OpLabel();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpBranch VisitBranch(Nodes.Branch node)
        {
            var instruction = new OpBranch();
            _instructionMap.Add(node, instruction);

            instruction.TargetLabel = Visit(node.TargetLabel);
            return instruction;
        }
        protected virtual OpBranchConditional VisitBranchConditional(Nodes.BranchConditional node)
        {
            var instruction = new OpBranchConditional();
            _instructionMap.Add(node, instruction);

            instruction.Condition = Visit(node.Condition);
            instruction.TrueLabel = Visit(node.TrueLabel);
            instruction.FalseLabel = Visit(node.FalseLabel);
            instruction.Branchweights = Visit(node.Branchweights);
            return instruction;
        }
        protected virtual OpSwitch VisitSwitch(Nodes.Switch node)
        {
            var instruction = new OpSwitch();
            _instructionMap.Add(node, instruction);

            instruction.Selector = Visit(node.Selector);
            instruction.Default = Visit(node.Default);
            instruction.Target = Visit(node.Target);
            return instruction;
        }
        protected virtual OpKill VisitKill(Nodes.Kill node)
        {
            var instruction = new OpKill();
            _instructionMap.Add(node, instruction);

            return instruction;
        }
        protected virtual OpReturn VisitReturn(Nodes.Return node)
        {
            var instruction = new OpReturn();
            _instructionMap.Add(node, instruction);

            return instruction;
        }
        protected virtual OpReturnValue VisitReturnValue(Nodes.ReturnValue node)
        {
            var instruction = new OpReturnValue();
            _instructionMap.Add(node, instruction);

            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpUnreachable VisitUnreachable(Nodes.Unreachable node)
        {
            var instruction = new OpUnreachable();
            _instructionMap.Add(node, instruction);

            return instruction;
        }
        protected virtual OpLifetimeStart VisitLifetimeStart(Nodes.LifetimeStart node)
        {
            var instruction = new OpLifetimeStart();
            _instructionMap.Add(node, instruction);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Size = Visit(node.Size);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpLifetimeStop VisitLifetimeStop(Nodes.LifetimeStop node)
        {
            var instruction = new OpLifetimeStop();
            _instructionMap.Add(node, instruction);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Size = Visit(node.Size);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpGroupAsyncCopy VisitGroupAsyncCopy(Nodes.GroupAsyncCopy node)
        {
            var instruction = new OpGroupAsyncCopy();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Destination = Visit(node.Destination);
            instruction.Source = Visit(node.Source);
            instruction.NumElements = Visit(node.NumElements);
            instruction.Stride = Visit(node.Stride);
            instruction.Event = Visit(node.Event);
            return instruction;
        }
        protected virtual OpGroupWaitEvents VisitGroupWaitEvents(Nodes.GroupWaitEvents node)
        {
            var instruction = new OpGroupWaitEvents();
            _instructionMap.Add(node, instruction);

            instruction.Execution = Visit(node.Execution);
            instruction.NumEvents = Visit(node.NumEvents);
            instruction.EventsList = Visit(node.EventsList);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpGroupAll VisitGroupAll(Nodes.GroupAll node)
        {
            var instruction = new OpGroupAll();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Predicate = Visit(node.Predicate);
            return instruction;
        }
        protected virtual OpGroupAny VisitGroupAny(Nodes.GroupAny node)
        {
            var instruction = new OpGroupAny();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Predicate = Visit(node.Predicate);
            return instruction;
        }
        protected virtual OpGroupBroadcast VisitGroupBroadcast(Nodes.GroupBroadcast node)
        {
            var instruction = new OpGroupBroadcast();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            instruction.LocalId = Visit(node.LocalId);
            return instruction;
        }
        protected virtual OpGroupIAdd VisitGroupIAdd(Nodes.GroupIAdd node)
        {
            var instruction = new OpGroupIAdd();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupFAdd VisitGroupFAdd(Nodes.GroupFAdd node)
        {
            var instruction = new OpGroupFAdd();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupFMin VisitGroupFMin(Nodes.GroupFMin node)
        {
            var instruction = new OpGroupFMin();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupUMin VisitGroupUMin(Nodes.GroupUMin node)
        {
            var instruction = new OpGroupUMin();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupSMin VisitGroupSMin(Nodes.GroupSMin node)
        {
            var instruction = new OpGroupSMin();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupFMax VisitGroupFMax(Nodes.GroupFMax node)
        {
            var instruction = new OpGroupFMax();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupUMax VisitGroupUMax(Nodes.GroupUMax node)
        {
            var instruction = new OpGroupUMax();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupSMax VisitGroupSMax(Nodes.GroupSMax node)
        {
            var instruction = new OpGroupSMax();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpReadPipe VisitReadPipe(Nodes.ReadPipe node)
        {
            var instruction = new OpReadPipe();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pipe = Visit(node.Pipe);
            instruction.Pointer = Visit(node.Pointer);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            return instruction;
        }
        protected virtual OpWritePipe VisitWritePipe(Nodes.WritePipe node)
        {
            var instruction = new OpWritePipe();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pipe = Visit(node.Pipe);
            instruction.Pointer = Visit(node.Pointer);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            return instruction;
        }
        protected virtual OpReservedReadPipe VisitReservedReadPipe(Nodes.ReservedReadPipe node)
        {
            var instruction = new OpReservedReadPipe();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pipe = Visit(node.Pipe);
            instruction.ReserveId = Visit(node.ReserveId);
            instruction.Index = Visit(node.Index);
            instruction.Pointer = Visit(node.Pointer);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            return instruction;
        }
        protected virtual OpReservedWritePipe VisitReservedWritePipe(Nodes.ReservedWritePipe node)
        {
            var instruction = new OpReservedWritePipe();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pipe = Visit(node.Pipe);
            instruction.ReserveId = Visit(node.ReserveId);
            instruction.Index = Visit(node.Index);
            instruction.Pointer = Visit(node.Pointer);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            return instruction;
        }
        protected virtual OpReserveReadPipePackets VisitReserveReadPipePackets(Nodes.ReserveReadPipePackets node)
        {
            var instruction = new OpReserveReadPipePackets();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pipe = Visit(node.Pipe);
            instruction.NumPackets = Visit(node.NumPackets);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            return instruction;
        }
        protected virtual OpReserveWritePipePackets VisitReserveWritePipePackets(Nodes.ReserveWritePipePackets node)
        {
            var instruction = new OpReserveWritePipePackets();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pipe = Visit(node.Pipe);
            instruction.NumPackets = Visit(node.NumPackets);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            return instruction;
        }
        protected virtual OpCommitReadPipe VisitCommitReadPipe(Nodes.CommitReadPipe node)
        {
            var instruction = new OpCommitReadPipe();
            _instructionMap.Add(node, instruction);

            instruction.Pipe = Visit(node.Pipe);
            instruction.ReserveId = Visit(node.ReserveId);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpCommitWritePipe VisitCommitWritePipe(Nodes.CommitWritePipe node)
        {
            var instruction = new OpCommitWritePipe();
            _instructionMap.Add(node, instruction);

            instruction.Pipe = Visit(node.Pipe);
            instruction.ReserveId = Visit(node.ReserveId);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpIsValidReserveId VisitIsValidReserveId(Nodes.IsValidReserveId node)
        {
            var instruction = new OpIsValidReserveId();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.ReserveId = Visit(node.ReserveId);
            return instruction;
        }
        protected virtual OpGetNumPipePackets VisitGetNumPipePackets(Nodes.GetNumPipePackets node)
        {
            var instruction = new OpGetNumPipePackets();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pipe = Visit(node.Pipe);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            return instruction;
        }
        protected virtual OpGetMaxPipePackets VisitGetMaxPipePackets(Nodes.GetMaxPipePackets node)
        {
            var instruction = new OpGetMaxPipePackets();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pipe = Visit(node.Pipe);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            return instruction;
        }
        protected virtual OpGroupReserveReadPipePackets VisitGroupReserveReadPipePackets(Nodes.GroupReserveReadPipePackets node)
        {
            var instruction = new OpGroupReserveReadPipePackets();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Pipe = Visit(node.Pipe);
            instruction.NumPackets = Visit(node.NumPackets);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            return instruction;
        }
        protected virtual OpGroupReserveWritePipePackets VisitGroupReserveWritePipePackets(Nodes.GroupReserveWritePipePackets node)
        {
            var instruction = new OpGroupReserveWritePipePackets();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Pipe = Visit(node.Pipe);
            instruction.NumPackets = Visit(node.NumPackets);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            return instruction;
        }
        protected virtual OpGroupCommitReadPipe VisitGroupCommitReadPipe(Nodes.GroupCommitReadPipe node)
        {
            var instruction = new OpGroupCommitReadPipe();
            _instructionMap.Add(node, instruction);

            instruction.Execution = Visit(node.Execution);
            instruction.Pipe = Visit(node.Pipe);
            instruction.ReserveId = Visit(node.ReserveId);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpGroupCommitWritePipe VisitGroupCommitWritePipe(Nodes.GroupCommitWritePipe node)
        {
            var instruction = new OpGroupCommitWritePipe();
            _instructionMap.Add(node, instruction);

            instruction.Execution = Visit(node.Execution);
            instruction.Pipe = Visit(node.Pipe);
            instruction.ReserveId = Visit(node.ReserveId);
            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpEnqueueMarker VisitEnqueueMarker(Nodes.EnqueueMarker node)
        {
            var instruction = new OpEnqueueMarker();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Queue = Visit(node.Queue);
            instruction.NumEvents = Visit(node.NumEvents);
            instruction.WaitEvents = Visit(node.WaitEvents);
            instruction.RetEvent = Visit(node.RetEvent);
            return instruction;
        }
        protected virtual OpEnqueueKernel VisitEnqueueKernel(Nodes.EnqueueKernel node)
        {
            var instruction = new OpEnqueueKernel();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Queue = Visit(node.Queue);
            instruction.Flags = Visit(node.Flags);
            instruction.NDRange = Visit(node.NDRange);
            instruction.NumEvents = Visit(node.NumEvents);
            instruction.WaitEvents = Visit(node.WaitEvents);
            instruction.RetEvent = Visit(node.RetEvent);
            instruction.Invoke = Visit(node.Invoke);
            instruction.Param = Visit(node.Param);
            instruction.ParamSize = Visit(node.ParamSize);
            instruction.ParamAlign = Visit(node.ParamAlign);
            instruction.LocalSize = Visit(node.LocalSize);
            return instruction;
        }
        protected virtual OpGetKernelNDrangeSubGroupCount VisitGetKernelNDrangeSubGroupCount(Nodes.GetKernelNDrangeSubGroupCount node)
        {
            var instruction = new OpGetKernelNDrangeSubGroupCount();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.NDRange = Visit(node.NDRange);
            instruction.Invoke = Visit(node.Invoke);
            instruction.Param = Visit(node.Param);
            instruction.ParamSize = Visit(node.ParamSize);
            instruction.ParamAlign = Visit(node.ParamAlign);
            return instruction;
        }
        protected virtual OpGetKernelNDrangeMaxSubGroupSize VisitGetKernelNDrangeMaxSubGroupSize(Nodes.GetKernelNDrangeMaxSubGroupSize node)
        {
            var instruction = new OpGetKernelNDrangeMaxSubGroupSize();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.NDRange = Visit(node.NDRange);
            instruction.Invoke = Visit(node.Invoke);
            instruction.Param = Visit(node.Param);
            instruction.ParamSize = Visit(node.ParamSize);
            instruction.ParamAlign = Visit(node.ParamAlign);
            return instruction;
        }
        protected virtual OpGetKernelWorkGroupSize VisitGetKernelWorkGroupSize(Nodes.GetKernelWorkGroupSize node)
        {
            var instruction = new OpGetKernelWorkGroupSize();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Invoke = Visit(node.Invoke);
            instruction.Param = Visit(node.Param);
            instruction.ParamSize = Visit(node.ParamSize);
            instruction.ParamAlign = Visit(node.ParamAlign);
            return instruction;
        }
        protected virtual OpGetKernelPreferredWorkGroupSizeMultiple VisitGetKernelPreferredWorkGroupSizeMultiple(Nodes.GetKernelPreferredWorkGroupSizeMultiple node)
        {
            var instruction = new OpGetKernelPreferredWorkGroupSizeMultiple();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Invoke = Visit(node.Invoke);
            instruction.Param = Visit(node.Param);
            instruction.ParamSize = Visit(node.ParamSize);
            instruction.ParamAlign = Visit(node.ParamAlign);
            return instruction;
        }
        protected virtual OpRetainEvent VisitRetainEvent(Nodes.RetainEvent node)
        {
            var instruction = new OpRetainEvent();
            _instructionMap.Add(node, instruction);

            instruction.Event = Visit(node.Event);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpReleaseEvent VisitReleaseEvent(Nodes.ReleaseEvent node)
        {
            var instruction = new OpReleaseEvent();
            _instructionMap.Add(node, instruction);

            instruction.Event = Visit(node.Event);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpCreateUserEvent VisitCreateUserEvent(Nodes.CreateUserEvent node)
        {
            var instruction = new OpCreateUserEvent();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpIsValidEvent VisitIsValidEvent(Nodes.IsValidEvent node)
        {
            var instruction = new OpIsValidEvent();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Event = Visit(node.Event);
            return instruction;
        }
        protected virtual OpSetUserEventStatus VisitSetUserEventStatus(Nodes.SetUserEventStatus node)
        {
            var instruction = new OpSetUserEventStatus();
            _instructionMap.Add(node, instruction);

            instruction.Event = Visit(node.Event);
            instruction.Status = Visit(node.Status);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpCaptureEventProfilingInfo VisitCaptureEventProfilingInfo(Nodes.CaptureEventProfilingInfo node)
        {
            var instruction = new OpCaptureEventProfilingInfo();
            _instructionMap.Add(node, instruction);

            instruction.Event = Visit(node.Event);
            instruction.ProfilingInfo = Visit(node.ProfilingInfo);
            instruction.Value = Visit(node.Value);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpGetDefaultQueue VisitGetDefaultQueue(Nodes.GetDefaultQueue node)
        {
            var instruction = new OpGetDefaultQueue();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpBuildNDRange VisitBuildNDRange(Nodes.BuildNDRange node)
        {
            var instruction = new OpBuildNDRange();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.GlobalWorkSize = Visit(node.GlobalWorkSize);
            instruction.LocalWorkSize = Visit(node.LocalWorkSize);
            instruction.GlobalWorkOffset = Visit(node.GlobalWorkOffset);
            return instruction;
        }
        protected virtual OpImageSparseSampleImplicitLod VisitImageSparseSampleImplicitLod(Nodes.ImageSparseSampleImplicitLod node)
        {
            var instruction = new OpImageSparseSampleImplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSparseSampleExplicitLod VisitImageSparseSampleExplicitLod(Nodes.ImageSparseSampleExplicitLod node)
        {
            var instruction = new OpImageSparseSampleExplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSparseSampleDrefImplicitLod VisitImageSparseSampleDrefImplicitLod(Nodes.ImageSparseSampleDrefImplicitLod node)
        {
            var instruction = new OpImageSparseSampleDrefImplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.D_ref = Visit(node.D_ref);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSparseSampleDrefExplicitLod VisitImageSparseSampleDrefExplicitLod(Nodes.ImageSparseSampleDrefExplicitLod node)
        {
            var instruction = new OpImageSparseSampleDrefExplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.D_ref = Visit(node.D_ref);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSparseSampleProjImplicitLod VisitImageSparseSampleProjImplicitLod(Nodes.ImageSparseSampleProjImplicitLod node)
        {
            var instruction = new OpImageSparseSampleProjImplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSparseSampleProjExplicitLod VisitImageSparseSampleProjExplicitLod(Nodes.ImageSparseSampleProjExplicitLod node)
        {
            var instruction = new OpImageSparseSampleProjExplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSparseSampleProjDrefImplicitLod VisitImageSparseSampleProjDrefImplicitLod(Nodes.ImageSparseSampleProjDrefImplicitLod node)
        {
            var instruction = new OpImageSparseSampleProjDrefImplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.D_ref = Visit(node.D_ref);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSparseSampleProjDrefExplicitLod VisitImageSparseSampleProjDrefExplicitLod(Nodes.ImageSparseSampleProjDrefExplicitLod node)
        {
            var instruction = new OpImageSparseSampleProjDrefExplicitLod();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.D_ref = Visit(node.D_ref);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSparseFetch VisitImageSparseFetch(Nodes.ImageSparseFetch node)
        {
            var instruction = new OpImageSparseFetch();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSparseGather VisitImageSparseGather(Nodes.ImageSparseGather node)
        {
            var instruction = new OpImageSparseGather();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.Component = Visit(node.Component);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSparseDrefGather VisitImageSparseDrefGather(Nodes.ImageSparseDrefGather node)
        {
            var instruction = new OpImageSparseDrefGather();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.D_ref = Visit(node.D_ref);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpImageSparseTexelsResident VisitImageSparseTexelsResident(Nodes.ImageSparseTexelsResident node)
        {
            var instruction = new OpImageSparseTexelsResident();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.ResidentCode = Visit(node.ResidentCode);
            return instruction;
        }
        protected virtual OpNoLine VisitNoLine(Nodes.NoLine node)
        {
            var instruction = new OpNoLine();
            _instructionMap.Add(node, instruction);

            Visit(node.Next);

            return instruction;
        }
        protected virtual OpAtomicFlagTestAndSet VisitAtomicFlagTestAndSet(Nodes.AtomicFlagTestAndSet node)
        {
            var instruction = new OpAtomicFlagTestAndSet();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            return instruction;
        }
        protected virtual OpAtomicFlagClear VisitAtomicFlagClear(Nodes.AtomicFlagClear node)
        {
            var instruction = new OpAtomicFlagClear();
            _instructionMap.Add(node, instruction);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpImageSparseRead VisitImageSparseRead(Nodes.ImageSparseRead node)
        {
            var instruction = new OpImageSparseRead();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpSizeOf VisitSizeOf(Nodes.SizeOf node)
        {
            var instruction = new OpSizeOf();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            return instruction;
        }
        protected virtual OpTypePipeStorage VisitTypePipeStorage(Types.TypePipeStorage node)
        {
            var instruction = new OpTypePipeStorage();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpConstantPipeStorage VisitConstantPipeStorage(Nodes.ConstantPipeStorage node)
        {
            var instruction = new OpConstantPipeStorage();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.PacketSize = Visit(node.PacketSize);
            instruction.PacketAlignment = Visit(node.PacketAlignment);
            instruction.Capacity = Visit(node.Capacity);
            return instruction;
        }
        protected virtual OpCreatePipeFromPipeStorage VisitCreatePipeFromPipeStorage(Nodes.CreatePipeFromPipeStorage node)
        {
            var instruction = new OpCreatePipeFromPipeStorage();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.PipeStorage = Visit(node.PipeStorage);
            return instruction;
        }
        protected virtual OpGetKernelLocalSizeForSubgroupCount VisitGetKernelLocalSizeForSubgroupCount(Nodes.GetKernelLocalSizeForSubgroupCount node)
        {
            var instruction = new OpGetKernelLocalSizeForSubgroupCount();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SubgroupCount = Visit(node.SubgroupCount);
            instruction.Invoke = Visit(node.Invoke);
            instruction.Param = Visit(node.Param);
            instruction.ParamSize = Visit(node.ParamSize);
            instruction.ParamAlign = Visit(node.ParamAlign);
            return instruction;
        }
        protected virtual OpGetKernelMaxNumSubgroups VisitGetKernelMaxNumSubgroups(Nodes.GetKernelMaxNumSubgroups node)
        {
            var instruction = new OpGetKernelMaxNumSubgroups();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Invoke = Visit(node.Invoke);
            instruction.Param = Visit(node.Param);
            instruction.ParamSize = Visit(node.ParamSize);
            instruction.ParamAlign = Visit(node.ParamAlign);
            return instruction;
        }
        protected virtual OpTypeNamedBarrier VisitTypeNamedBarrier(Types.TypeNamedBarrier node)
        {
            var instruction = new OpTypeNamedBarrier();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpNamedBarrierInitialize VisitNamedBarrierInitialize(Nodes.NamedBarrierInitialize node)
        {
            var instruction = new OpNamedBarrierInitialize();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SubgroupCount = Visit(node.SubgroupCount);
            return instruction;
        }
        protected virtual OpMemoryNamedBarrier VisitMemoryNamedBarrier(Nodes.MemoryNamedBarrier node)
        {
            var instruction = new OpMemoryNamedBarrier();
            _instructionMap.Add(node, instruction);

            instruction.NamedBarrier = Visit(node.NamedBarrier);
            instruction.Memory = Visit(node.Memory);
            instruction.Semantics = Visit(node.Semantics);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpModuleProcessed VisitModuleProcessed(Nodes.ModuleProcessed node)
        {
            var instruction = new OpModuleProcessed();
            _instructionMap.Add(node, instruction);

            instruction.Process = Visit(node.Process);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpExecutionModeId VisitExecutionModeId(Nodes.ExecutionModeId node)
        {
            var instruction = new OpExecutionModeId();
            _instructionMap.Add(node, instruction);

            instruction.EntryPoint = Visit(node.EntryPoint);
            instruction.Mode = Visit(node.Mode);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpDecorateId VisitDecorateId(Nodes.DecorateId node)
        {
            var instruction = new OpDecorateId();
            _instructionMap.Add(node, instruction);

            instruction.Target = Visit(node.Target);
            instruction.Decoration = Visit(node.Decoration);
            return instruction;
        }
        protected virtual OpGroupNonUniformElect VisitGroupNonUniformElect(Nodes.GroupNonUniformElect node)
        {
            var instruction = new OpGroupNonUniformElect();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            return instruction;
        }
        protected virtual OpGroupNonUniformAll VisitGroupNonUniformAll(Nodes.GroupNonUniformAll node)
        {
            var instruction = new OpGroupNonUniformAll();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Predicate = Visit(node.Predicate);
            return instruction;
        }
        protected virtual OpGroupNonUniformAny VisitGroupNonUniformAny(Nodes.GroupNonUniformAny node)
        {
            var instruction = new OpGroupNonUniformAny();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Predicate = Visit(node.Predicate);
            return instruction;
        }
        protected virtual OpGroupNonUniformAllEqual VisitGroupNonUniformAllEqual(Nodes.GroupNonUniformAllEqual node)
        {
            var instruction = new OpGroupNonUniformAllEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpGroupNonUniformBroadcast VisitGroupNonUniformBroadcast(Nodes.GroupNonUniformBroadcast node)
        {
            var instruction = new OpGroupNonUniformBroadcast();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            instruction.Id = Visit(node.Id);
            return instruction;
        }
        protected virtual OpGroupNonUniformBroadcastFirst VisitGroupNonUniformBroadcastFirst(Nodes.GroupNonUniformBroadcastFirst node)
        {
            var instruction = new OpGroupNonUniformBroadcastFirst();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpGroupNonUniformBallot VisitGroupNonUniformBallot(Nodes.GroupNonUniformBallot node)
        {
            var instruction = new OpGroupNonUniformBallot();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Predicate = Visit(node.Predicate);
            return instruction;
        }
        protected virtual OpGroupNonUniformInverseBallot VisitGroupNonUniformInverseBallot(Nodes.GroupNonUniformInverseBallot node)
        {
            var instruction = new OpGroupNonUniformInverseBallot();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpGroupNonUniformBallotBitExtract VisitGroupNonUniformBallotBitExtract(Nodes.GroupNonUniformBallotBitExtract node)
        {
            var instruction = new OpGroupNonUniformBallotBitExtract();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            instruction.Index = Visit(node.Index);
            return instruction;
        }
        protected virtual OpGroupNonUniformBallotBitCount VisitGroupNonUniformBallotBitCount(Nodes.GroupNonUniformBallotBitCount node)
        {
            var instruction = new OpGroupNonUniformBallotBitCount();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpGroupNonUniformBallotFindLSB VisitGroupNonUniformBallotFindLSB(Nodes.GroupNonUniformBallotFindLSB node)
        {
            var instruction = new OpGroupNonUniformBallotFindLSB();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpGroupNonUniformBallotFindMSB VisitGroupNonUniformBallotFindMSB(Nodes.GroupNonUniformBallotFindMSB node)
        {
            var instruction = new OpGroupNonUniformBallotFindMSB();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpGroupNonUniformShuffle VisitGroupNonUniformShuffle(Nodes.GroupNonUniformShuffle node)
        {
            var instruction = new OpGroupNonUniformShuffle();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            instruction.Id = Visit(node.Id);
            return instruction;
        }
        protected virtual OpGroupNonUniformShuffleXor VisitGroupNonUniformShuffleXor(Nodes.GroupNonUniformShuffleXor node)
        {
            var instruction = new OpGroupNonUniformShuffleXor();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            instruction.Mask = Visit(node.Mask);
            return instruction;
        }
        protected virtual OpGroupNonUniformShuffleUp VisitGroupNonUniformShuffleUp(Nodes.GroupNonUniformShuffleUp node)
        {
            var instruction = new OpGroupNonUniformShuffleUp();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            instruction.Delta = Visit(node.Delta);
            return instruction;
        }
        protected virtual OpGroupNonUniformShuffleDown VisitGroupNonUniformShuffleDown(Nodes.GroupNonUniformShuffleDown node)
        {
            var instruction = new OpGroupNonUniformShuffleDown();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            instruction.Delta = Visit(node.Delta);
            return instruction;
        }
        protected virtual OpGroupNonUniformIAdd VisitGroupNonUniformIAdd(Nodes.GroupNonUniformIAdd node)
        {
            var instruction = new OpGroupNonUniformIAdd();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformFAdd VisitGroupNonUniformFAdd(Nodes.GroupNonUniformFAdd node)
        {
            var instruction = new OpGroupNonUniformFAdd();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformIMul VisitGroupNonUniformIMul(Nodes.GroupNonUniformIMul node)
        {
            var instruction = new OpGroupNonUniformIMul();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformFMul VisitGroupNonUniformFMul(Nodes.GroupNonUniformFMul node)
        {
            var instruction = new OpGroupNonUniformFMul();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformSMin VisitGroupNonUniformSMin(Nodes.GroupNonUniformSMin node)
        {
            var instruction = new OpGroupNonUniformSMin();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformUMin VisitGroupNonUniformUMin(Nodes.GroupNonUniformUMin node)
        {
            var instruction = new OpGroupNonUniformUMin();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformFMin VisitGroupNonUniformFMin(Nodes.GroupNonUniformFMin node)
        {
            var instruction = new OpGroupNonUniformFMin();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformSMax VisitGroupNonUniformSMax(Nodes.GroupNonUniformSMax node)
        {
            var instruction = new OpGroupNonUniformSMax();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformUMax VisitGroupNonUniformUMax(Nodes.GroupNonUniformUMax node)
        {
            var instruction = new OpGroupNonUniformUMax();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformFMax VisitGroupNonUniformFMax(Nodes.GroupNonUniformFMax node)
        {
            var instruction = new OpGroupNonUniformFMax();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformBitwiseAnd VisitGroupNonUniformBitwiseAnd(Nodes.GroupNonUniformBitwiseAnd node)
        {
            var instruction = new OpGroupNonUniformBitwiseAnd();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformBitwiseOr VisitGroupNonUniformBitwiseOr(Nodes.GroupNonUniformBitwiseOr node)
        {
            var instruction = new OpGroupNonUniformBitwiseOr();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformBitwiseXor VisitGroupNonUniformBitwiseXor(Nodes.GroupNonUniformBitwiseXor node)
        {
            var instruction = new OpGroupNonUniformBitwiseXor();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformLogicalAnd VisitGroupNonUniformLogicalAnd(Nodes.GroupNonUniformLogicalAnd node)
        {
            var instruction = new OpGroupNonUniformLogicalAnd();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformLogicalOr VisitGroupNonUniformLogicalOr(Nodes.GroupNonUniformLogicalOr node)
        {
            var instruction = new OpGroupNonUniformLogicalOr();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformLogicalXor VisitGroupNonUniformLogicalXor(Nodes.GroupNonUniformLogicalXor node)
        {
            var instruction = new OpGroupNonUniformLogicalXor();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.Value = Visit(node.Value);
            instruction.ClusterSize = Visit(node.ClusterSize);
            return instruction;
        }
        protected virtual OpGroupNonUniformQuadBroadcast VisitGroupNonUniformQuadBroadcast(Nodes.GroupNonUniformQuadBroadcast node)
        {
            var instruction = new OpGroupNonUniformQuadBroadcast();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            instruction.Index = Visit(node.Index);
            return instruction;
        }
        protected virtual OpGroupNonUniformQuadSwap VisitGroupNonUniformQuadSwap(Nodes.GroupNonUniformQuadSwap node)
        {
            var instruction = new OpGroupNonUniformQuadSwap();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Value = Visit(node.Value);
            instruction.Direction = Visit(node.Direction);
            return instruction;
        }
        protected virtual OpCopyLogical VisitCopyLogical(Nodes.CopyLogical node)
        {
            var instruction = new OpCopyLogical();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand = Visit(node.Operand);
            return instruction;
        }
        protected virtual OpPtrEqual VisitPtrEqual(Nodes.PtrEqual node)
        {
            var instruction = new OpPtrEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpPtrNotEqual VisitPtrNotEqual(Nodes.PtrNotEqual node)
        {
            var instruction = new OpPtrNotEqual();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpPtrDiff VisitPtrDiff(Nodes.PtrDiff node)
        {
            var instruction = new OpPtrDiff();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpSubgroupBallotKHR VisitSubgroupBallotKHR(Nodes.SubgroupBallotKHR node)
        {
            var instruction = new OpSubgroupBallotKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Predicate = Visit(node.Predicate);
            return instruction;
        }
        protected virtual OpSubgroupFirstInvocationKHR VisitSubgroupFirstInvocationKHR(Nodes.SubgroupFirstInvocationKHR node)
        {
            var instruction = new OpSubgroupFirstInvocationKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpSubgroupAllKHR VisitSubgroupAllKHR(Nodes.SubgroupAllKHR node)
        {
            var instruction = new OpSubgroupAllKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Predicate = Visit(node.Predicate);
            return instruction;
        }
        protected virtual OpSubgroupAnyKHR VisitSubgroupAnyKHR(Nodes.SubgroupAnyKHR node)
        {
            var instruction = new OpSubgroupAnyKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Predicate = Visit(node.Predicate);
            return instruction;
        }
        protected virtual OpSubgroupAllEqualKHR VisitSubgroupAllEqualKHR(Nodes.SubgroupAllEqualKHR node)
        {
            var instruction = new OpSubgroupAllEqualKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Predicate = Visit(node.Predicate);
            return instruction;
        }
        protected virtual OpSubgroupReadInvocationKHR VisitSubgroupReadInvocationKHR(Nodes.SubgroupReadInvocationKHR node)
        {
            var instruction = new OpSubgroupReadInvocationKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Value = Visit(node.Value);
            instruction.Index = Visit(node.Index);
            return instruction;
        }
        protected virtual OpGroupIAddNonUniformAMD VisitGroupIAddNonUniformAMD(Nodes.GroupIAddNonUniformAMD node)
        {
            var instruction = new OpGroupIAddNonUniformAMD();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupFAddNonUniformAMD VisitGroupFAddNonUniformAMD(Nodes.GroupFAddNonUniformAMD node)
        {
            var instruction = new OpGroupFAddNonUniformAMD();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupFMinNonUniformAMD VisitGroupFMinNonUniformAMD(Nodes.GroupFMinNonUniformAMD node)
        {
            var instruction = new OpGroupFMinNonUniformAMD();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupUMinNonUniformAMD VisitGroupUMinNonUniformAMD(Nodes.GroupUMinNonUniformAMD node)
        {
            var instruction = new OpGroupUMinNonUniformAMD();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupSMinNonUniformAMD VisitGroupSMinNonUniformAMD(Nodes.GroupSMinNonUniformAMD node)
        {
            var instruction = new OpGroupSMinNonUniformAMD();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupFMaxNonUniformAMD VisitGroupFMaxNonUniformAMD(Nodes.GroupFMaxNonUniformAMD node)
        {
            var instruction = new OpGroupFMaxNonUniformAMD();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupUMaxNonUniformAMD VisitGroupUMaxNonUniformAMD(Nodes.GroupUMaxNonUniformAMD node)
        {
            var instruction = new OpGroupUMaxNonUniformAMD();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpGroupSMaxNonUniformAMD VisitGroupSMaxNonUniformAMD(Nodes.GroupSMaxNonUniformAMD node)
        {
            var instruction = new OpGroupSMaxNonUniformAMD();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            instruction.Operation = Visit(node.Operation);
            instruction.X = Visit(node.X);
            return instruction;
        }
        protected virtual OpFragmentMaskFetchAMD VisitFragmentMaskFetchAMD(Nodes.FragmentMaskFetchAMD node)
        {
            var instruction = new OpFragmentMaskFetchAMD();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            instruction.Coordinate = Visit(node.Coordinate);
            return instruction;
        }
        protected virtual OpFragmentFetchAMD VisitFragmentFetchAMD(Nodes.FragmentFetchAMD node)
        {
            var instruction = new OpFragmentFetchAMD();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.FragmentIndex = Visit(node.FragmentIndex);
            return instruction;
        }
        protected virtual OpReadClockKHR VisitReadClockKHR(Nodes.ReadClockKHR node)
        {
            var instruction = new OpReadClockKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Execution = Visit(node.Execution);
            return instruction;
        }
        protected virtual OpImageSampleFootprintNV VisitImageSampleFootprintNV(Nodes.ImageSampleFootprintNV node)
        {
            var instruction = new OpImageSampleFootprintNV();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SampledImage = Visit(node.SampledImage);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.Granularity = Visit(node.Granularity);
            instruction.Coarse = Visit(node.Coarse);
            instruction.ImageOperands = Visit(node.ImageOperands);
            return instruction;
        }
        protected virtual OpGroupNonUniformPartitionNV VisitGroupNonUniformPartitionNV(Nodes.GroupNonUniformPartitionNV node)
        {
            var instruction = new OpGroupNonUniformPartitionNV();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpWritePackedPrimitiveIndices4x8NV VisitWritePackedPrimitiveIndices4x8NV(Nodes.WritePackedPrimitiveIndices4x8NV node)
        {
            var instruction = new OpWritePackedPrimitiveIndices4x8NV();
            _instructionMap.Add(node, instruction);

            instruction.IndexOffset = Visit(node.IndexOffset);
            instruction.PackedIndices = Visit(node.PackedIndices);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpReportIntersectionNV VisitReportIntersectionNV(Nodes.ReportIntersectionNV node)
        {
            var instruction = new OpReportIntersectionNV();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Hit = Visit(node.Hit);
            instruction.HitKind = Visit(node.HitKind);
            return instruction;
        }
        protected virtual OpIgnoreIntersectionNV VisitIgnoreIntersectionNV(Nodes.IgnoreIntersectionNV node)
        {
            var instruction = new OpIgnoreIntersectionNV();
            _instructionMap.Add(node, instruction);

            Visit(node.Next);

            return instruction;
        }
        protected virtual OpTerminateRayNV VisitTerminateRayNV(Nodes.TerminateRayNV node)
        {
            var instruction = new OpTerminateRayNV();
            _instructionMap.Add(node, instruction);

            Visit(node.Next);

            return instruction;
        }
        protected virtual OpTraceNV VisitTraceNV(Nodes.TraceNV node)
        {
            var instruction = new OpTraceNV();
            _instructionMap.Add(node, instruction);

            instruction.Accel = Visit(node.Accel);
            instruction.RayFlags = Visit(node.RayFlags);
            instruction.CullMask = Visit(node.CullMask);
            instruction.SBTOffset = Visit(node.SBTOffset);
            instruction.SBTStride = Visit(node.SBTStride);
            instruction.MissIndex = Visit(node.MissIndex);
            instruction.RayOrigin = Visit(node.RayOrigin);
            instruction.RayTmin = Visit(node.RayTmin);
            instruction.RayDirection = Visit(node.RayDirection);
            instruction.RayTmax = Visit(node.RayTmax);
            instruction.PayloadId = Visit(node.PayloadId);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpTypeAccelerationStructureNV VisitTypeAccelerationStructureNV(Types.TypeAccelerationStructureNV node)
        {
            var instruction = new OpTypeAccelerationStructureNV();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeRayQueryProvisionalKHR VisitTypeRayQueryProvisionalKHR(Types.TypeRayQueryProvisionalKHR node)
        {
            var instruction = new OpTypeRayQueryProvisionalKHR();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpRayQueryInitializeKHR VisitRayQueryInitializeKHR(Nodes.RayQueryInitializeKHR node)
        {
            var instruction = new OpRayQueryInitializeKHR();
            _instructionMap.Add(node, instruction);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Accel = Visit(node.Accel);
            instruction.RayFlags = Visit(node.RayFlags);
            instruction.CullMask = Visit(node.CullMask);
            instruction.RayOrigin = Visit(node.RayOrigin);
            instruction.RayTMin = Visit(node.RayTMin);
            instruction.RayDirection = Visit(node.RayDirection);
            instruction.RayTMax = Visit(node.RayTMax);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpRayQueryTerminateKHR VisitRayQueryTerminateKHR(Nodes.RayQueryTerminateKHR node)
        {
            var instruction = new OpRayQueryTerminateKHR();
            _instructionMap.Add(node, instruction);

            instruction.RayQuery = Visit(node.RayQuery);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpRayQueryGenerateIntersectionKHR VisitRayQueryGenerateIntersectionKHR(Nodes.RayQueryGenerateIntersectionKHR node)
        {
            var instruction = new OpRayQueryGenerateIntersectionKHR();
            _instructionMap.Add(node, instruction);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.HitT = Visit(node.HitT);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpRayQueryConfirmIntersectionKHR VisitRayQueryConfirmIntersectionKHR(Nodes.RayQueryConfirmIntersectionKHR node)
        {
            var instruction = new OpRayQueryConfirmIntersectionKHR();
            _instructionMap.Add(node, instruction);

            instruction.RayQuery = Visit(node.RayQuery);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpRayQueryProceedKHR VisitRayQueryProceedKHR(Nodes.RayQueryProceedKHR node)
        {
            var instruction = new OpRayQueryProceedKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionTypeKHR VisitRayQueryGetIntersectionTypeKHR(Nodes.RayQueryGetIntersectionTypeKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionTypeKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpRayQueryGetRayTMinKHR VisitRayQueryGetRayTMinKHR(Nodes.RayQueryGetRayTMinKHR node)
        {
            var instruction = new OpRayQueryGetRayTMinKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            return instruction;
        }
        protected virtual OpRayQueryGetRayFlagsKHR VisitRayQueryGetRayFlagsKHR(Nodes.RayQueryGetRayFlagsKHR node)
        {
            var instruction = new OpRayQueryGetRayFlagsKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionTKHR VisitRayQueryGetIntersectionTKHR(Nodes.RayQueryGetIntersectionTKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionTKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionInstanceCustomIndexKHR VisitRayQueryGetIntersectionInstanceCustomIndexKHR(Nodes.RayQueryGetIntersectionInstanceCustomIndexKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionInstanceCustomIndexKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionInstanceIdKHR VisitRayQueryGetIntersectionInstanceIdKHR(Nodes.RayQueryGetIntersectionInstanceIdKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionInstanceIdKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR VisitRayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR(Nodes.RayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionGeometryIndexKHR VisitRayQueryGetIntersectionGeometryIndexKHR(Nodes.RayQueryGetIntersectionGeometryIndexKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionGeometryIndexKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionPrimitiveIndexKHR VisitRayQueryGetIntersectionPrimitiveIndexKHR(Nodes.RayQueryGetIntersectionPrimitiveIndexKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionPrimitiveIndexKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionBarycentricsKHR VisitRayQueryGetIntersectionBarycentricsKHR(Nodes.RayQueryGetIntersectionBarycentricsKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionBarycentricsKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionFrontFaceKHR VisitRayQueryGetIntersectionFrontFaceKHR(Nodes.RayQueryGetIntersectionFrontFaceKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionFrontFaceKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionCandidateAABBOpaqueKHR VisitRayQueryGetIntersectionCandidateAABBOpaqueKHR(Nodes.RayQueryGetIntersectionCandidateAABBOpaqueKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionCandidateAABBOpaqueKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionObjectRayDirectionKHR VisitRayQueryGetIntersectionObjectRayDirectionKHR(Nodes.RayQueryGetIntersectionObjectRayDirectionKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionObjectRayDirectionKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionObjectRayOriginKHR VisitRayQueryGetIntersectionObjectRayOriginKHR(Nodes.RayQueryGetIntersectionObjectRayOriginKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionObjectRayOriginKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpRayQueryGetWorldRayDirectionKHR VisitRayQueryGetWorldRayDirectionKHR(Nodes.RayQueryGetWorldRayDirectionKHR node)
        {
            var instruction = new OpRayQueryGetWorldRayDirectionKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            return instruction;
        }
        protected virtual OpRayQueryGetWorldRayOriginKHR VisitRayQueryGetWorldRayOriginKHR(Nodes.RayQueryGetWorldRayOriginKHR node)
        {
            var instruction = new OpRayQueryGetWorldRayOriginKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionObjectToWorldKHR VisitRayQueryGetIntersectionObjectToWorldKHR(Nodes.RayQueryGetIntersectionObjectToWorldKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionObjectToWorldKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpRayQueryGetIntersectionWorldToObjectKHR VisitRayQueryGetIntersectionWorldToObjectKHR(Nodes.RayQueryGetIntersectionWorldToObjectKHR node)
        {
            var instruction = new OpRayQueryGetIntersectionWorldToObjectKHR();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RayQuery = Visit(node.RayQuery);
            instruction.Intersection = Visit(node.Intersection);
            return instruction;
        }
        protected virtual OpExecuteCallableNV VisitExecuteCallableNV(Nodes.ExecuteCallableNV node)
        {
            var instruction = new OpExecuteCallableNV();
            _instructionMap.Add(node, instruction);

            instruction.SBTIndex = Visit(node.SBTIndex);
            instruction.CallableDataId = Visit(node.CallableDataId);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpTypeCooperativeMatrixNV VisitTypeCooperativeMatrixNV(Types.TypeCooperativeMatrixNV node)
        {
            var instruction = new OpTypeCooperativeMatrixNV();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.ComponentType = Visit(node.ComponentType);
            instruction.Execution = Visit(node.Execution);
            instruction.Rows = Visit(node.Rows);
            instruction.Columns = Visit(node.Columns);
            return instruction;
        }
        protected virtual OpCooperativeMatrixLoadNV VisitCooperativeMatrixLoadNV(Nodes.CooperativeMatrixLoadNV node)
        {
            var instruction = new OpCooperativeMatrixLoadNV();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Stride = Visit(node.Stride);
            instruction.ColumnMajor = Visit(node.ColumnMajor);
            instruction.MemoryAccess = Visit(node.MemoryAccess);
            return instruction;
        }
        protected virtual OpCooperativeMatrixStoreNV VisitCooperativeMatrixStoreNV(Nodes.CooperativeMatrixStoreNV node)
        {
            var instruction = new OpCooperativeMatrixStoreNV();
            _instructionMap.Add(node, instruction);

            instruction.Pointer = Visit(node.Pointer);
            instruction.Object = Visit(node.Object);
            instruction.Stride = Visit(node.Stride);
            instruction.ColumnMajor = Visit(node.ColumnMajor);
            instruction.MemoryAccess = Visit(node.MemoryAccess);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpCooperativeMatrixMulAddNV VisitCooperativeMatrixMulAddNV(Nodes.CooperativeMatrixMulAddNV node)
        {
            var instruction = new OpCooperativeMatrixMulAddNV();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.A = Visit(node.A);
            instruction.B = Visit(node.B);
            instruction.C = Visit(node.C);
            return instruction;
        }
        protected virtual OpCooperativeMatrixLengthNV VisitCooperativeMatrixLengthNV(Nodes.CooperativeMatrixLengthNV node)
        {
            var instruction = new OpCooperativeMatrixLengthNV();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Type = Visit(node.Type);
            return instruction;
        }
        protected virtual OpBeginInvocationInterlockEXT VisitBeginInvocationInterlockEXT(Nodes.BeginInvocationInterlockEXT node)
        {
            var instruction = new OpBeginInvocationInterlockEXT();
            _instructionMap.Add(node, instruction);

            Visit(node.Next);

            return instruction;
        }
        protected virtual OpEndInvocationInterlockEXT VisitEndInvocationInterlockEXT(Nodes.EndInvocationInterlockEXT node)
        {
            var instruction = new OpEndInvocationInterlockEXT();
            _instructionMap.Add(node, instruction);

            Visit(node.Next);

            return instruction;
        }
        protected virtual OpDemoteToHelperInvocationEXT VisitDemoteToHelperInvocationEXT(Nodes.DemoteToHelperInvocationEXT node)
        {
            var instruction = new OpDemoteToHelperInvocationEXT();
            _instructionMap.Add(node, instruction);

            Visit(node.Next);

            return instruction;
        }
        protected virtual OpIsHelperInvocationEXT VisitIsHelperInvocationEXT(Nodes.IsHelperInvocationEXT node)
        {
            var instruction = new OpIsHelperInvocationEXT();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpSubgroupShuffleINTEL VisitSubgroupShuffleINTEL(Nodes.SubgroupShuffleINTEL node)
        {
            var instruction = new OpSubgroupShuffleINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Data = Visit(node.Data);
            instruction.InvocationId = Visit(node.InvocationId);
            return instruction;
        }
        protected virtual OpSubgroupShuffleDownINTEL VisitSubgroupShuffleDownINTEL(Nodes.SubgroupShuffleDownINTEL node)
        {
            var instruction = new OpSubgroupShuffleDownINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Current = Visit(node.Current);
            instruction.Next = Visit(node.Next);
            instruction.Delta = Visit(node.Delta);
            return instruction;
        }
        protected virtual OpSubgroupShuffleUpINTEL VisitSubgroupShuffleUpINTEL(Nodes.SubgroupShuffleUpINTEL node)
        {
            var instruction = new OpSubgroupShuffleUpINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Previous = Visit(node.Previous);
            instruction.Current = Visit(node.Current);
            instruction.Delta = Visit(node.Delta);
            return instruction;
        }
        protected virtual OpSubgroupShuffleXorINTEL VisitSubgroupShuffleXorINTEL(Nodes.SubgroupShuffleXorINTEL node)
        {
            var instruction = new OpSubgroupShuffleXorINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Data = Visit(node.Data);
            instruction.Value = Visit(node.Value);
            return instruction;
        }
        protected virtual OpSubgroupBlockReadINTEL VisitSubgroupBlockReadINTEL(Nodes.SubgroupBlockReadINTEL node)
        {
            var instruction = new OpSubgroupBlockReadINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Ptr = Visit(node.Ptr);
            return instruction;
        }
        protected virtual OpSubgroupBlockWriteINTEL VisitSubgroupBlockWriteINTEL(Nodes.SubgroupBlockWriteINTEL node)
        {
            var instruction = new OpSubgroupBlockWriteINTEL();
            _instructionMap.Add(node, instruction);

            instruction.Ptr = Visit(node.Ptr);
            instruction.Data = Visit(node.Data);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpSubgroupImageBlockReadINTEL VisitSubgroupImageBlockReadINTEL(Nodes.SubgroupImageBlockReadINTEL node)
        {
            var instruction = new OpSubgroupImageBlockReadINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            instruction.Coordinate = Visit(node.Coordinate);
            return instruction;
        }
        protected virtual OpSubgroupImageBlockWriteINTEL VisitSubgroupImageBlockWriteINTEL(Nodes.SubgroupImageBlockWriteINTEL node)
        {
            var instruction = new OpSubgroupImageBlockWriteINTEL();
            _instructionMap.Add(node, instruction);

            instruction.Image = Visit(node.Image);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.Data = Visit(node.Data);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpSubgroupImageMediaBlockReadINTEL VisitSubgroupImageMediaBlockReadINTEL(Nodes.SubgroupImageMediaBlockReadINTEL node)
        {
            var instruction = new OpSubgroupImageMediaBlockReadINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Image = Visit(node.Image);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.Width = Visit(node.Width);
            instruction.Height = Visit(node.Height);
            return instruction;
        }
        protected virtual OpSubgroupImageMediaBlockWriteINTEL VisitSubgroupImageMediaBlockWriteINTEL(Nodes.SubgroupImageMediaBlockWriteINTEL node)
        {
            var instruction = new OpSubgroupImageMediaBlockWriteINTEL();
            _instructionMap.Add(node, instruction);

            instruction.Image = Visit(node.Image);
            instruction.Coordinate = Visit(node.Coordinate);
            instruction.Width = Visit(node.Width);
            instruction.Height = Visit(node.Height);
            instruction.Data = Visit(node.Data);
            Visit(node.Next);

            return instruction;
        }
        protected virtual OpUCountLeadingZerosINTEL VisitUCountLeadingZerosINTEL(Nodes.UCountLeadingZerosINTEL node)
        {
            var instruction = new OpUCountLeadingZerosINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand = Visit(node.Operand);
            return instruction;
        }
        protected virtual OpUCountTrailingZerosINTEL VisitUCountTrailingZerosINTEL(Nodes.UCountTrailingZerosINTEL node)
        {
            var instruction = new OpUCountTrailingZerosINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand = Visit(node.Operand);
            return instruction;
        }
        protected virtual OpAbsISubINTEL VisitAbsISubINTEL(Nodes.AbsISubINTEL node)
        {
            var instruction = new OpAbsISubINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpAbsUSubINTEL VisitAbsUSubINTEL(Nodes.AbsUSubINTEL node)
        {
            var instruction = new OpAbsUSubINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpIAddSatINTEL VisitIAddSatINTEL(Nodes.IAddSatINTEL node)
        {
            var instruction = new OpIAddSatINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpUAddSatINTEL VisitUAddSatINTEL(Nodes.UAddSatINTEL node)
        {
            var instruction = new OpUAddSatINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpIAverageINTEL VisitIAverageINTEL(Nodes.IAverageINTEL node)
        {
            var instruction = new OpIAverageINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpUAverageINTEL VisitUAverageINTEL(Nodes.UAverageINTEL node)
        {
            var instruction = new OpUAverageINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpIAverageRoundedINTEL VisitIAverageRoundedINTEL(Nodes.IAverageRoundedINTEL node)
        {
            var instruction = new OpIAverageRoundedINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpUAverageRoundedINTEL VisitUAverageRoundedINTEL(Nodes.UAverageRoundedINTEL node)
        {
            var instruction = new OpUAverageRoundedINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpISubSatINTEL VisitISubSatINTEL(Nodes.ISubSatINTEL node)
        {
            var instruction = new OpISubSatINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpUSubSatINTEL VisitUSubSatINTEL(Nodes.USubSatINTEL node)
        {
            var instruction = new OpUSubSatINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpIMul32x16INTEL VisitIMul32x16INTEL(Nodes.IMul32x16INTEL node)
        {
            var instruction = new OpIMul32x16INTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpUMul32x16INTEL VisitUMul32x16INTEL(Nodes.UMul32x16INTEL node)
        {
            var instruction = new OpUMul32x16INTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Operand1 = Visit(node.Operand1);
            instruction.Operand2 = Visit(node.Operand2);
            return instruction;
        }
        protected virtual OpDecorateString VisitDecorateString(Nodes.DecorateString node)
        {
            var instruction = new OpDecorateString();
            _instructionMap.Add(node, instruction);

            instruction.Target = Visit(node.Target);
            instruction.Decoration = Visit(node.Decoration);
            return instruction;
        }
        protected virtual OpMemberDecorateString VisitMemberDecorateString(Nodes.MemberDecorateString node)
        {
            var instruction = new OpMemberDecorateString();
            _instructionMap.Add(node, instruction);

            instruction.StructType = Visit(node.StructType);
            instruction.Member = Visit(node.Member);
            instruction.Decoration = Visit(node.Decoration);
            return instruction;
        }
        protected virtual OpVmeImageINTEL VisitVmeImageINTEL(Nodes.VmeImageINTEL node)
        {
            var instruction = new OpVmeImageINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.ImageType = Visit(node.ImageType);
            instruction.Sampler = Visit(node.Sampler);
            return instruction;
        }
        protected virtual OpTypeVmeImageINTEL VisitTypeVmeImageINTEL(Types.TypeVmeImageINTEL node)
        {
            var instruction = new OpTypeVmeImageINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.ImageType = Visit(node.ImageType);
            return instruction;
        }
        protected virtual OpTypeAvcImePayloadINTEL VisitTypeAvcImePayloadINTEL(Types.TypeAvcImePayloadINTEL node)
        {
            var instruction = new OpTypeAvcImePayloadINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeAvcRefPayloadINTEL VisitTypeAvcRefPayloadINTEL(Types.TypeAvcRefPayloadINTEL node)
        {
            var instruction = new OpTypeAvcRefPayloadINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeAvcSicPayloadINTEL VisitTypeAvcSicPayloadINTEL(Types.TypeAvcSicPayloadINTEL node)
        {
            var instruction = new OpTypeAvcSicPayloadINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeAvcMcePayloadINTEL VisitTypeAvcMcePayloadINTEL(Types.TypeAvcMcePayloadINTEL node)
        {
            var instruction = new OpTypeAvcMcePayloadINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeAvcMceResultINTEL VisitTypeAvcMceResultINTEL(Types.TypeAvcMceResultINTEL node)
        {
            var instruction = new OpTypeAvcMceResultINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeAvcImeResultINTEL VisitTypeAvcImeResultINTEL(Types.TypeAvcImeResultINTEL node)
        {
            var instruction = new OpTypeAvcImeResultINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeAvcImeResultSingleReferenceStreamoutINTEL VisitTypeAvcImeResultSingleReferenceStreamoutINTEL(Types.TypeAvcImeResultSingleReferenceStreamoutINTEL node)
        {
            var instruction = new OpTypeAvcImeResultSingleReferenceStreamoutINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeAvcImeResultDualReferenceStreamoutINTEL VisitTypeAvcImeResultDualReferenceStreamoutINTEL(Types.TypeAvcImeResultDualReferenceStreamoutINTEL node)
        {
            var instruction = new OpTypeAvcImeResultDualReferenceStreamoutINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeAvcImeSingleReferenceStreaminINTEL VisitTypeAvcImeSingleReferenceStreaminINTEL(Types.TypeAvcImeSingleReferenceStreaminINTEL node)
        {
            var instruction = new OpTypeAvcImeSingleReferenceStreaminINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeAvcImeDualReferenceStreaminINTEL VisitTypeAvcImeDualReferenceStreaminINTEL(Types.TypeAvcImeDualReferenceStreaminINTEL node)
        {
            var instruction = new OpTypeAvcImeDualReferenceStreaminINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeAvcRefResultINTEL VisitTypeAvcRefResultINTEL(Types.TypeAvcRefResultINTEL node)
        {
            var instruction = new OpTypeAvcRefResultINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpTypeAvcSicResultINTEL VisitTypeAvcSicResultINTEL(Types.TypeAvcSicResultINTEL node)
        {
            var instruction = new OpTypeAvcSicResultINTEL();
            _instructionMap.Add(node, instruction);
            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL VisitSubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL(Nodes.SubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SliceType = Visit(node.SliceType);
            instruction.Qp = Visit(node.Qp);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL VisitSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL(Nodes.SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL node)
        {
            var instruction = new OpSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.ReferenceBasePenalty = Visit(node.ReferenceBasePenalty);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetDefaultInterShapePenaltyINTEL VisitSubgroupAvcMceGetDefaultInterShapePenaltyINTEL(Nodes.SubgroupAvcMceGetDefaultInterShapePenaltyINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetDefaultInterShapePenaltyINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SliceType = Visit(node.SliceType);
            instruction.Qp = Visit(node.Qp);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceSetInterShapePenaltyINTEL VisitSubgroupAvcMceSetInterShapePenaltyINTEL(Nodes.SubgroupAvcMceSetInterShapePenaltyINTEL node)
        {
            var instruction = new OpSubgroupAvcMceSetInterShapePenaltyINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.PackedShapePenalty = Visit(node.PackedShapePenalty);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL VisitSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL(Nodes.SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SliceType = Visit(node.SliceType);
            instruction.Qp = Visit(node.Qp);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceSetInterDirectionPenaltyINTEL VisitSubgroupAvcMceSetInterDirectionPenaltyINTEL(Nodes.SubgroupAvcMceSetInterDirectionPenaltyINTEL node)
        {
            var instruction = new OpSubgroupAvcMceSetInterDirectionPenaltyINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.DirectionCost = Visit(node.DirectionCost);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL VisitSubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL(Nodes.SubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SliceType = Visit(node.SliceType);
            instruction.Qp = Visit(node.Qp);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL VisitSubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL(Nodes.SubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SliceType = Visit(node.SliceType);
            instruction.Qp = Visit(node.Qp);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL VisitSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL(Nodes.SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL VisitSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL(Nodes.SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL VisitSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL(Nodes.SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpSubgroupAvcMceSetMotionVectorCostFunctionINTEL VisitSubgroupAvcMceSetMotionVectorCostFunctionINTEL(Nodes.SubgroupAvcMceSetMotionVectorCostFunctionINTEL node)
        {
            var instruction = new OpSubgroupAvcMceSetMotionVectorCostFunctionINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.PackedCostCenterDelta = Visit(node.PackedCostCenterDelta);
            instruction.PackedCostTable = Visit(node.PackedCostTable);
            instruction.CostPrecision = Visit(node.CostPrecision);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL VisitSubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL(Nodes.SubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SliceType = Visit(node.SliceType);
            instruction.Qp = Visit(node.Qp);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL VisitSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL(Nodes.SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL VisitSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL(Nodes.SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            return instruction;
        }
        protected virtual OpSubgroupAvcMceSetAcOnlyHaarINTEL VisitSubgroupAvcMceSetAcOnlyHaarINTEL(Nodes.SubgroupAvcMceSetAcOnlyHaarINTEL node)
        {
            var instruction = new OpSubgroupAvcMceSetAcOnlyHaarINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL VisitSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL(Nodes.SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL node)
        {
            var instruction = new OpSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SourceFieldPolarity = Visit(node.SourceFieldPolarity);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL VisitSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL(Nodes.SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL node)
        {
            var instruction = new OpSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.ReferenceFieldPolarity = Visit(node.ReferenceFieldPolarity);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL VisitSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL(Nodes.SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL node)
        {
            var instruction = new OpSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.ForwardReferenceFieldPolarity = Visit(node.ForwardReferenceFieldPolarity);
            instruction.BackwardReferenceFieldPolarity = Visit(node.BackwardReferenceFieldPolarity);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceConvertToImePayloadINTEL VisitSubgroupAvcMceConvertToImePayloadINTEL(Nodes.SubgroupAvcMceConvertToImePayloadINTEL node)
        {
            var instruction = new OpSubgroupAvcMceConvertToImePayloadINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceConvertToImeResultINTEL VisitSubgroupAvcMceConvertToImeResultINTEL(Nodes.SubgroupAvcMceConvertToImeResultINTEL node)
        {
            var instruction = new OpSubgroupAvcMceConvertToImeResultINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceConvertToRefPayloadINTEL VisitSubgroupAvcMceConvertToRefPayloadINTEL(Nodes.SubgroupAvcMceConvertToRefPayloadINTEL node)
        {
            var instruction = new OpSubgroupAvcMceConvertToRefPayloadINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceConvertToRefResultINTEL VisitSubgroupAvcMceConvertToRefResultINTEL(Nodes.SubgroupAvcMceConvertToRefResultINTEL node)
        {
            var instruction = new OpSubgroupAvcMceConvertToRefResultINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceConvertToSicPayloadINTEL VisitSubgroupAvcMceConvertToSicPayloadINTEL(Nodes.SubgroupAvcMceConvertToSicPayloadINTEL node)
        {
            var instruction = new OpSubgroupAvcMceConvertToSicPayloadINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceConvertToSicResultINTEL VisitSubgroupAvcMceConvertToSicResultINTEL(Nodes.SubgroupAvcMceConvertToSicResultINTEL node)
        {
            var instruction = new OpSubgroupAvcMceConvertToSicResultINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetMotionVectorsINTEL VisitSubgroupAvcMceGetMotionVectorsINTEL(Nodes.SubgroupAvcMceGetMotionVectorsINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetMotionVectorsINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetInterDistortionsINTEL VisitSubgroupAvcMceGetInterDistortionsINTEL(Nodes.SubgroupAvcMceGetInterDistortionsINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetInterDistortionsINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetBestInterDistortionsINTEL VisitSubgroupAvcMceGetBestInterDistortionsINTEL(Nodes.SubgroupAvcMceGetBestInterDistortionsINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetBestInterDistortionsINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetInterMajorShapeINTEL VisitSubgroupAvcMceGetInterMajorShapeINTEL(Nodes.SubgroupAvcMceGetInterMajorShapeINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetInterMajorShapeINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetInterMinorShapeINTEL VisitSubgroupAvcMceGetInterMinorShapeINTEL(Nodes.SubgroupAvcMceGetInterMinorShapeINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetInterMinorShapeINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetInterDirectionsINTEL VisitSubgroupAvcMceGetInterDirectionsINTEL(Nodes.SubgroupAvcMceGetInterDirectionsINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetInterDirectionsINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetInterMotionVectorCountINTEL VisitSubgroupAvcMceGetInterMotionVectorCountINTEL(Nodes.SubgroupAvcMceGetInterMotionVectorCountINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetInterMotionVectorCountINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetInterReferenceIdsINTEL VisitSubgroupAvcMceGetInterReferenceIdsINTEL(Nodes.SubgroupAvcMceGetInterReferenceIdsINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetInterReferenceIdsINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL VisitSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL(Nodes.SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL node)
        {
            var instruction = new OpSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.PackedReferenceIds = Visit(node.PackedReferenceIds);
            instruction.PackedReferenceParameterFieldPolarities = Visit(node.PackedReferenceParameterFieldPolarities);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeInitializeINTEL VisitSubgroupAvcImeInitializeINTEL(Nodes.SubgroupAvcImeInitializeINTEL node)
        {
            var instruction = new OpSubgroupAvcImeInitializeINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcCoord = Visit(node.SrcCoord);
            instruction.PartitionMask = Visit(node.PartitionMask);
            instruction.SADAdjustment = Visit(node.SADAdjustment);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeSetSingleReferenceINTEL VisitSubgroupAvcImeSetSingleReferenceINTEL(Nodes.SubgroupAvcImeSetSingleReferenceINTEL node)
        {
            var instruction = new OpSubgroupAvcImeSetSingleReferenceINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RefOffset = Visit(node.RefOffset);
            instruction.SearchWindowConfig = Visit(node.SearchWindowConfig);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeSetDualReferenceINTEL VisitSubgroupAvcImeSetDualReferenceINTEL(Nodes.SubgroupAvcImeSetDualReferenceINTEL node)
        {
            var instruction = new OpSubgroupAvcImeSetDualReferenceINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.FwdRefOffset = Visit(node.FwdRefOffset);
            instruction.BwdRefOffset = Visit(node.BwdRefOffset);
            instruction.SearchWindowConfig = Visit(node.SearchWindowConfig);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeRefWindowSizeINTEL VisitSubgroupAvcImeRefWindowSizeINTEL(Nodes.SubgroupAvcImeRefWindowSizeINTEL node)
        {
            var instruction = new OpSubgroupAvcImeRefWindowSizeINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SearchWindowConfig = Visit(node.SearchWindowConfig);
            instruction.DualRef = Visit(node.DualRef);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeAdjustRefOffsetINTEL VisitSubgroupAvcImeAdjustRefOffsetINTEL(Nodes.SubgroupAvcImeAdjustRefOffsetINTEL node)
        {
            var instruction = new OpSubgroupAvcImeAdjustRefOffsetINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.RefOffset = Visit(node.RefOffset);
            instruction.SrcCoord = Visit(node.SrcCoord);
            instruction.RefWindowSize = Visit(node.RefWindowSize);
            instruction.ImageSize = Visit(node.ImageSize);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeConvertToMcePayloadINTEL VisitSubgroupAvcImeConvertToMcePayloadINTEL(Nodes.SubgroupAvcImeConvertToMcePayloadINTEL node)
        {
            var instruction = new OpSubgroupAvcImeConvertToMcePayloadINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeSetMaxMotionVectorCountINTEL VisitSubgroupAvcImeSetMaxMotionVectorCountINTEL(Nodes.SubgroupAvcImeSetMaxMotionVectorCountINTEL node)
        {
            var instruction = new OpSubgroupAvcImeSetMaxMotionVectorCountINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.MaxMotionVectorCount = Visit(node.MaxMotionVectorCount);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeSetUnidirectionalMixDisableINTEL VisitSubgroupAvcImeSetUnidirectionalMixDisableINTEL(Nodes.SubgroupAvcImeSetUnidirectionalMixDisableINTEL node)
        {
            var instruction = new OpSubgroupAvcImeSetUnidirectionalMixDisableINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeSetEarlySearchTerminationThresholdINTEL VisitSubgroupAvcImeSetEarlySearchTerminationThresholdINTEL(Nodes.SubgroupAvcImeSetEarlySearchTerminationThresholdINTEL node)
        {
            var instruction = new OpSubgroupAvcImeSetEarlySearchTerminationThresholdINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Threshold = Visit(node.Threshold);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeSetWeightedSadINTEL VisitSubgroupAvcImeSetWeightedSadINTEL(Nodes.SubgroupAvcImeSetWeightedSadINTEL node)
        {
            var instruction = new OpSubgroupAvcImeSetWeightedSadINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.PackedSadWeights = Visit(node.PackedSadWeights);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeEvaluateWithSingleReferenceINTEL VisitSubgroupAvcImeEvaluateWithSingleReferenceINTEL(Nodes.SubgroupAvcImeEvaluateWithSingleReferenceINTEL node)
        {
            var instruction = new OpSubgroupAvcImeEvaluateWithSingleReferenceINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.RefImage = Visit(node.RefImage);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeEvaluateWithDualReferenceINTEL VisitSubgroupAvcImeEvaluateWithDualReferenceINTEL(Nodes.SubgroupAvcImeEvaluateWithDualReferenceINTEL node)
        {
            var instruction = new OpSubgroupAvcImeEvaluateWithDualReferenceINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.FwdRefImage = Visit(node.FwdRefImage);
            instruction.BwdRefImage = Visit(node.BwdRefImage);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL VisitSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL(Nodes.SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL node)
        {
            var instruction = new OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.RefImage = Visit(node.RefImage);
            instruction.Payload = Visit(node.Payload);
            instruction.StreaminComponents = Visit(node.StreaminComponents);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL VisitSubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL(Nodes.SubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL node)
        {
            var instruction = new OpSubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.FwdRefImage = Visit(node.FwdRefImage);
            instruction.BwdRefImage = Visit(node.BwdRefImage);
            instruction.Payload = Visit(node.Payload);
            instruction.StreaminComponents = Visit(node.StreaminComponents);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL VisitSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL(Nodes.SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL node)
        {
            var instruction = new OpSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.RefImage = Visit(node.RefImage);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL VisitSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL(Nodes.SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL node)
        {
            var instruction = new OpSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.FwdRefImage = Visit(node.FwdRefImage);
            instruction.BwdRefImage = Visit(node.BwdRefImage);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL VisitSubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL(Nodes.SubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL node)
        {
            var instruction = new OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.RefImage = Visit(node.RefImage);
            instruction.Payload = Visit(node.Payload);
            instruction.StreaminComponents = Visit(node.StreaminComponents);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL VisitSubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL(Nodes.SubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL node)
        {
            var instruction = new OpSubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.FwdRefImage = Visit(node.FwdRefImage);
            instruction.BwdRefImage = Visit(node.BwdRefImage);
            instruction.Payload = Visit(node.Payload);
            instruction.StreaminComponents = Visit(node.StreaminComponents);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeConvertToMceResultINTEL VisitSubgroupAvcImeConvertToMceResultINTEL(Nodes.SubgroupAvcImeConvertToMceResultINTEL node)
        {
            var instruction = new OpSubgroupAvcImeConvertToMceResultINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetSingleReferenceStreaminINTEL VisitSubgroupAvcImeGetSingleReferenceStreaminINTEL(Nodes.SubgroupAvcImeGetSingleReferenceStreaminINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetSingleReferenceStreaminINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetDualReferenceStreaminINTEL VisitSubgroupAvcImeGetDualReferenceStreaminINTEL(Nodes.SubgroupAvcImeGetDualReferenceStreaminINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetDualReferenceStreaminINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeStripSingleReferenceStreamoutINTEL VisitSubgroupAvcImeStripSingleReferenceStreamoutINTEL(Nodes.SubgroupAvcImeStripSingleReferenceStreamoutINTEL node)
        {
            var instruction = new OpSubgroupAvcImeStripSingleReferenceStreamoutINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeStripDualReferenceStreamoutINTEL VisitSubgroupAvcImeStripDualReferenceStreamoutINTEL(Nodes.SubgroupAvcImeStripDualReferenceStreamoutINTEL node)
        {
            var instruction = new OpSubgroupAvcImeStripDualReferenceStreamoutINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL VisitSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL(Nodes.SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            instruction.MajorShape = Visit(node.MajorShape);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL VisitSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL(Nodes.SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            instruction.MajorShape = Visit(node.MajorShape);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL VisitSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL(Nodes.SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            instruction.MajorShape = Visit(node.MajorShape);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL VisitSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL(Nodes.SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            instruction.MajorShape = Visit(node.MajorShape);
            instruction.Direction = Visit(node.Direction);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL VisitSubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL(Nodes.SubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            instruction.MajorShape = Visit(node.MajorShape);
            instruction.Direction = Visit(node.Direction);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL VisitSubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL(Nodes.SubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            instruction.MajorShape = Visit(node.MajorShape);
            instruction.Direction = Visit(node.Direction);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetBorderReachedINTEL VisitSubgroupAvcImeGetBorderReachedINTEL(Nodes.SubgroupAvcImeGetBorderReachedINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetBorderReachedINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.ImageSelect = Visit(node.ImageSelect);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetTruncatedSearchIndicationINTEL VisitSubgroupAvcImeGetTruncatedSearchIndicationINTEL(Nodes.SubgroupAvcImeGetTruncatedSearchIndicationINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetTruncatedSearchIndicationINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL VisitSubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL(Nodes.SubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL VisitSubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL(Nodes.SubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL VisitSubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL(Nodes.SubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL node)
        {
            var instruction = new OpSubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcFmeInitializeINTEL VisitSubgroupAvcFmeInitializeINTEL(Nodes.SubgroupAvcFmeInitializeINTEL node)
        {
            var instruction = new OpSubgroupAvcFmeInitializeINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcCoord = Visit(node.SrcCoord);
            instruction.MotionVectors = Visit(node.MotionVectors);
            instruction.MajorShapes = Visit(node.MajorShapes);
            instruction.MinorShapes = Visit(node.MinorShapes);
            instruction.Direction = Visit(node.Direction);
            instruction.PixelResolution = Visit(node.PixelResolution);
            instruction.SadAdjustment = Visit(node.SadAdjustment);
            return instruction;
        }
        protected virtual OpSubgroupAvcBmeInitializeINTEL VisitSubgroupAvcBmeInitializeINTEL(Nodes.SubgroupAvcBmeInitializeINTEL node)
        {
            var instruction = new OpSubgroupAvcBmeInitializeINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcCoord = Visit(node.SrcCoord);
            instruction.MotionVectors = Visit(node.MotionVectors);
            instruction.MajorShapes = Visit(node.MajorShapes);
            instruction.MinorShapes = Visit(node.MinorShapes);
            instruction.Direction = Visit(node.Direction);
            instruction.PixelResolution = Visit(node.PixelResolution);
            instruction.BidirectionalWeight = Visit(node.BidirectionalWeight);
            instruction.SadAdjustment = Visit(node.SadAdjustment);
            return instruction;
        }
        protected virtual OpSubgroupAvcRefConvertToMcePayloadINTEL VisitSubgroupAvcRefConvertToMcePayloadINTEL(Nodes.SubgroupAvcRefConvertToMcePayloadINTEL node)
        {
            var instruction = new OpSubgroupAvcRefConvertToMcePayloadINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcRefSetBidirectionalMixDisableINTEL VisitSubgroupAvcRefSetBidirectionalMixDisableINTEL(Nodes.SubgroupAvcRefSetBidirectionalMixDisableINTEL node)
        {
            var instruction = new OpSubgroupAvcRefSetBidirectionalMixDisableINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcRefSetBilinearFilterEnableINTEL VisitSubgroupAvcRefSetBilinearFilterEnableINTEL(Nodes.SubgroupAvcRefSetBilinearFilterEnableINTEL node)
        {
            var instruction = new OpSubgroupAvcRefSetBilinearFilterEnableINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcRefEvaluateWithSingleReferenceINTEL VisitSubgroupAvcRefEvaluateWithSingleReferenceINTEL(Nodes.SubgroupAvcRefEvaluateWithSingleReferenceINTEL node)
        {
            var instruction = new OpSubgroupAvcRefEvaluateWithSingleReferenceINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.RefImage = Visit(node.RefImage);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcRefEvaluateWithDualReferenceINTEL VisitSubgroupAvcRefEvaluateWithDualReferenceINTEL(Nodes.SubgroupAvcRefEvaluateWithDualReferenceINTEL node)
        {
            var instruction = new OpSubgroupAvcRefEvaluateWithDualReferenceINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.FwdRefImage = Visit(node.FwdRefImage);
            instruction.BwdRefImage = Visit(node.BwdRefImage);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcRefEvaluateWithMultiReferenceINTEL VisitSubgroupAvcRefEvaluateWithMultiReferenceINTEL(Nodes.SubgroupAvcRefEvaluateWithMultiReferenceINTEL node)
        {
            var instruction = new OpSubgroupAvcRefEvaluateWithMultiReferenceINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.PackedReferenceIds = Visit(node.PackedReferenceIds);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL VisitSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL(Nodes.SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL node)
        {
            var instruction = new OpSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.PackedReferenceIds = Visit(node.PackedReferenceIds);
            instruction.PackedReferenceFieldPolarities = Visit(node.PackedReferenceFieldPolarities);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcRefConvertToMceResultINTEL VisitSubgroupAvcRefConvertToMceResultINTEL(Nodes.SubgroupAvcRefConvertToMceResultINTEL node)
        {
            var instruction = new OpSubgroupAvcRefConvertToMceResultINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicInitializeINTEL VisitSubgroupAvcSicInitializeINTEL(Nodes.SubgroupAvcSicInitializeINTEL node)
        {
            var instruction = new OpSubgroupAvcSicInitializeINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcCoord = Visit(node.SrcCoord);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicConfigureSkcINTEL VisitSubgroupAvcSicConfigureSkcINTEL(Nodes.SubgroupAvcSicConfigureSkcINTEL node)
        {
            var instruction = new OpSubgroupAvcSicConfigureSkcINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SkipBlockPartitionType = Visit(node.SkipBlockPartitionType);
            instruction.SkipMotionVectorMask = Visit(node.SkipMotionVectorMask);
            instruction.MotionVectors = Visit(node.MotionVectors);
            instruction.BidirectionalWeight = Visit(node.BidirectionalWeight);
            instruction.SadAdjustment = Visit(node.SadAdjustment);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicConfigureIpeLumaINTEL VisitSubgroupAvcSicConfigureIpeLumaINTEL(Nodes.SubgroupAvcSicConfigureIpeLumaINTEL node)
        {
            var instruction = new OpSubgroupAvcSicConfigureIpeLumaINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.LumaIntraPartitionMask = Visit(node.LumaIntraPartitionMask);
            instruction.IntraNeighbourAvailabilty = Visit(node.IntraNeighbourAvailabilty);
            instruction.LeftEdgeLumaPixels = Visit(node.LeftEdgeLumaPixels);
            instruction.UpperLeftCornerLumaPixel = Visit(node.UpperLeftCornerLumaPixel);
            instruction.UpperEdgeLumaPixels = Visit(node.UpperEdgeLumaPixels);
            instruction.UpperRightEdgeLumaPixels = Visit(node.UpperRightEdgeLumaPixels);
            instruction.SadAdjustment = Visit(node.SadAdjustment);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicConfigureIpeLumaChromaINTEL VisitSubgroupAvcSicConfigureIpeLumaChromaINTEL(Nodes.SubgroupAvcSicConfigureIpeLumaChromaINTEL node)
        {
            var instruction = new OpSubgroupAvcSicConfigureIpeLumaChromaINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.LumaIntraPartitionMask = Visit(node.LumaIntraPartitionMask);
            instruction.IntraNeighbourAvailabilty = Visit(node.IntraNeighbourAvailabilty);
            instruction.LeftEdgeLumaPixels = Visit(node.LeftEdgeLumaPixels);
            instruction.UpperLeftCornerLumaPixel = Visit(node.UpperLeftCornerLumaPixel);
            instruction.UpperEdgeLumaPixels = Visit(node.UpperEdgeLumaPixels);
            instruction.UpperRightEdgeLumaPixels = Visit(node.UpperRightEdgeLumaPixels);
            instruction.LeftEdgeChromaPixels = Visit(node.LeftEdgeChromaPixels);
            instruction.UpperLeftCornerChromaPixel = Visit(node.UpperLeftCornerChromaPixel);
            instruction.UpperEdgeChromaPixels = Visit(node.UpperEdgeChromaPixels);
            instruction.SadAdjustment = Visit(node.SadAdjustment);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicGetMotionVectorMaskINTEL VisitSubgroupAvcSicGetMotionVectorMaskINTEL(Nodes.SubgroupAvcSicGetMotionVectorMaskINTEL node)
        {
            var instruction = new OpSubgroupAvcSicGetMotionVectorMaskINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SkipBlockPartitionType = Visit(node.SkipBlockPartitionType);
            instruction.Direction = Visit(node.Direction);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicConvertToMcePayloadINTEL VisitSubgroupAvcSicConvertToMcePayloadINTEL(Nodes.SubgroupAvcSicConvertToMcePayloadINTEL node)
        {
            var instruction = new OpSubgroupAvcSicConvertToMcePayloadINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicSetIntraLumaShapePenaltyINTEL VisitSubgroupAvcSicSetIntraLumaShapePenaltyINTEL(Nodes.SubgroupAvcSicSetIntraLumaShapePenaltyINTEL node)
        {
            var instruction = new OpSubgroupAvcSicSetIntraLumaShapePenaltyINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.PackedShapePenalty = Visit(node.PackedShapePenalty);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL VisitSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL(Nodes.SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL node)
        {
            var instruction = new OpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.LumaModePenalty = Visit(node.LumaModePenalty);
            instruction.LumaPackedNeighborModes = Visit(node.LumaPackedNeighborModes);
            instruction.LumaPackedNonDcPenalty = Visit(node.LumaPackedNonDcPenalty);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL VisitSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL(Nodes.SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL node)
        {
            var instruction = new OpSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.ChromaModeBasePenalty = Visit(node.ChromaModeBasePenalty);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicSetBilinearFilterEnableINTEL VisitSubgroupAvcSicSetBilinearFilterEnableINTEL(Nodes.SubgroupAvcSicSetBilinearFilterEnableINTEL node)
        {
            var instruction = new OpSubgroupAvcSicSetBilinearFilterEnableINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicSetSkcForwardTransformEnableINTEL VisitSubgroupAvcSicSetSkcForwardTransformEnableINTEL(Nodes.SubgroupAvcSicSetSkcForwardTransformEnableINTEL node)
        {
            var instruction = new OpSubgroupAvcSicSetSkcForwardTransformEnableINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.PackedSadCoefficients = Visit(node.PackedSadCoefficients);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicSetBlockBasedRawSkipSadINTEL VisitSubgroupAvcSicSetBlockBasedRawSkipSadINTEL(Nodes.SubgroupAvcSicSetBlockBasedRawSkipSadINTEL node)
        {
            var instruction = new OpSubgroupAvcSicSetBlockBasedRawSkipSadINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.BlockBasedSkipType = Visit(node.BlockBasedSkipType);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicEvaluateIpeINTEL VisitSubgroupAvcSicEvaluateIpeINTEL(Nodes.SubgroupAvcSicEvaluateIpeINTEL node)
        {
            var instruction = new OpSubgroupAvcSicEvaluateIpeINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicEvaluateWithSingleReferenceINTEL VisitSubgroupAvcSicEvaluateWithSingleReferenceINTEL(Nodes.SubgroupAvcSicEvaluateWithSingleReferenceINTEL node)
        {
            var instruction = new OpSubgroupAvcSicEvaluateWithSingleReferenceINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.RefImage = Visit(node.RefImage);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicEvaluateWithDualReferenceINTEL VisitSubgroupAvcSicEvaluateWithDualReferenceINTEL(Nodes.SubgroupAvcSicEvaluateWithDualReferenceINTEL node)
        {
            var instruction = new OpSubgroupAvcSicEvaluateWithDualReferenceINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.FwdRefImage = Visit(node.FwdRefImage);
            instruction.BwdRefImage = Visit(node.BwdRefImage);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicEvaluateWithMultiReferenceINTEL VisitSubgroupAvcSicEvaluateWithMultiReferenceINTEL(Nodes.SubgroupAvcSicEvaluateWithMultiReferenceINTEL node)
        {
            var instruction = new OpSubgroupAvcSicEvaluateWithMultiReferenceINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.PackedReferenceIds = Visit(node.PackedReferenceIds);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL VisitSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL(Nodes.SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL node)
        {
            var instruction = new OpSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.SrcImage = Visit(node.SrcImage);
            instruction.PackedReferenceIds = Visit(node.PackedReferenceIds);
            instruction.PackedReferenceFieldPolarities = Visit(node.PackedReferenceFieldPolarities);
            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicConvertToMceResultINTEL VisitSubgroupAvcSicConvertToMceResultINTEL(Nodes.SubgroupAvcSicConvertToMceResultINTEL node)
        {
            var instruction = new OpSubgroupAvcSicConvertToMceResultINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicGetIpeLumaShapeINTEL VisitSubgroupAvcSicGetIpeLumaShapeINTEL(Nodes.SubgroupAvcSicGetIpeLumaShapeINTEL node)
        {
            var instruction = new OpSubgroupAvcSicGetIpeLumaShapeINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicGetBestIpeLumaDistortionINTEL VisitSubgroupAvcSicGetBestIpeLumaDistortionINTEL(Nodes.SubgroupAvcSicGetBestIpeLumaDistortionINTEL node)
        {
            var instruction = new OpSubgroupAvcSicGetBestIpeLumaDistortionINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicGetBestIpeChromaDistortionINTEL VisitSubgroupAvcSicGetBestIpeChromaDistortionINTEL(Nodes.SubgroupAvcSicGetBestIpeChromaDistortionINTEL node)
        {
            var instruction = new OpSubgroupAvcSicGetBestIpeChromaDistortionINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicGetPackedIpeLumaModesINTEL VisitSubgroupAvcSicGetPackedIpeLumaModesINTEL(Nodes.SubgroupAvcSicGetPackedIpeLumaModesINTEL node)
        {
            var instruction = new OpSubgroupAvcSicGetPackedIpeLumaModesINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicGetIpeChromaModeINTEL VisitSubgroupAvcSicGetIpeChromaModeINTEL(Nodes.SubgroupAvcSicGetIpeChromaModeINTEL node)
        {
            var instruction = new OpSubgroupAvcSicGetIpeChromaModeINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL VisitSubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL(Nodes.SubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL node)
        {
            var instruction = new OpSubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL VisitSubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL(Nodes.SubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL node)
        {
            var instruction = new OpSubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
        protected virtual OpSubgroupAvcSicGetInterRawSadsINTEL VisitSubgroupAvcSicGetInterRawSadsINTEL(Nodes.SubgroupAvcSicGetInterRawSadsINTEL node)
        {
            var instruction = new OpSubgroupAvcSicGetInterRawSadsINTEL();
            _instructionMap.Add(node, instruction);

            instruction.IdResult = (uint)_results.Count;
            _results.Add(instruction);
            VisitDecorations(node);
            instruction.IdResultType = Visit(node.ResultType);

            instruction.Payload = Visit(node.Payload);
            return instruction;
        }
    }
}