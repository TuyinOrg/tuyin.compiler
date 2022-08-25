using System;
using System.Collections.Generic;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Nodes;
using Toe.SPIRV.Reflection.Operands;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;
using Capability = Toe.SPIRV.Reflection.Nodes.Capability;
using ExecutionMode = Toe.SPIRV.Reflection.Nodes.ExecutionMode;
using MemoryModel = Toe.SPIRV.Reflection.Nodes.MemoryModel;

namespace Toe.SPIRV.Reflection
{
    internal class SpirvInstructionTreeBuilder
    {
        private readonly List<KeyValuePair<Instruction, Node>> _nodes = new List<KeyValuePair<Instruction, Node>>();
        private IdRefDictionary<Node> _nodeMap;
        private IdRefDictionary<NodeDecorations> _decorations;

        private Shader _shader;

        /// <summary>
        ///     All OpCapability instructions.
        /// </summary>
        public List<Capability> CapabilityInstructions { get; } = new List<Capability>();

        /// <summary>
        ///     Optional OpExtension instructions (extensions to SPIR-V).
        /// </summary>
        public List<Extension> ExtensionInstructions { get; } = new List<Extension>();

        /// <summary>
        ///     Optional OpExtInstImport instructions.
        /// </summary>
        public List<ExtInstImport> ExtInstImportInstructions { get; } = new List<ExtInstImport>();

        /// <summary>
        ///     The single required OpMemoryModel instruction.
        /// </summary>
        public MemoryModel MemoryModel { get; set; }

        /// <summary>
        ///     All entry point declarations, using OpEntryPoint.
        /// </summary>
        public List<EntryPoint> EntryPointInstructions { get; } = new List<EntryPoint>();

        /// <summary>
        ///     All entry point declarations, using OpEntryPoint.
        /// </summary>
        public List<ExecutionMode> ExecutionModeInstructions { get; } = new List<ExecutionMode>();

        /// <summary>
        ///     All type declarations (OpTypeXXX instructions).
        /// </summary>
        public List<SpirvTypeBase> TypeInstructions { get; } = new List<SpirvTypeBase>();

        public SpirvTypeBase ResolveType(IdRef idRef)
        {
            if (idRef == IdRef.Empty)
                return null;
            return (SpirvTypeBase) _nodeMap[idRef.Id];
        }

        public SpirvTypeBase ResolveType(uint id)
        {
            return _nodeMap[id] as SpirvTypeBase;
        }

        public PairLiteralIntegerNode Parse(PairLiteralIntegerIdRef item)
        {
            return new PairLiteralIntegerNode(item.LiteralInteger, _nodeMap[item.IdRef]);
        }
        public PairNodeNode Parse(PairIdRefIdRef item)
        {
            return new PairNodeNode(_nodeMap[item.IdRef], _nodeMap[item.IdRef2]);
        }

        public uint Parse(uint item)
        {
            return item;
        }

        public void AddType(InstructionWithId instruction, SpirvTypeBase type)
        {
            _nodeMap.Add(instruction.IdResult, type);
            TypeInstructions.Add(type);
        }

        public string GetDebugName(Instruction instruction)
        {
            if (instruction.TryGetResultId(out var id)) return _decorations.GetRef(id).Name?.Value;

            return null;
        }

        public IReadOnlyList<Instruction> GetDecorations(Instruction instruction)
        {
            if (instruction.TryGetResultId(out var id)) return _decorations.GetRef(id).Decorations;

            return Array.Empty<Instruction>();
        }

        public void ParseFloat(Instruction instruction)
        {
            var opTypeFloat = (OpTypeFloat) instruction;
            switch (opTypeFloat.Width)
            {
                case 16:
                    AddType(opTypeFloat, SpirvTypeBase.Half);
                    break;
                case 32:
                    AddType(opTypeFloat, SpirvTypeBase.Float);
                    break;
                case 64:
                    AddType(opTypeFloat, SpirvTypeBase.Double);
                    break;
            }
        }

        public void ParseInt(Instruction instruction)
        {
            var opTypeInt = (OpTypeInt) instruction;
            switch (opTypeInt.Width)
            {
                case 32:
                    if (opTypeInt.Signedness == 0)
                        AddType(opTypeInt, SpirvTypeBase.UInt);
                    else
                        AddType(opTypeInt, SpirvTypeBase.Int);
                    break;
                case 16:
                    if (opTypeInt.Signedness == 0)
                        AddType(opTypeInt, SpirvTypeBase.UShort);
                    else
                        AddType(opTypeInt, SpirvTypeBase.Short);
                    break;
                case 8:
                    if (opTypeInt.Signedness == 0)
                        AddType(opTypeInt, SpirvTypeBase.Byte);
                    else
                        AddType(opTypeInt, SpirvTypeBase.SByte);
                    break;
            }
        }

        public void ParseMatrix(OpTypeMatrix instruction)
        {
            var columnType = ResolveType(instruction.ColumnType);
            var columnCount = instruction.ColumnCount;
            var vector = SpirvTypeBase.ResolveMatrix((TypeVector) columnType, columnCount);
            AddType(instruction, vector);
        }

        public void ParseVector(OpTypeVector instruction)
        {
            var componentType = ResolveType(instruction.ComponentType);
            var instructionComponentCount = instruction.ComponentCount;
            var vector = SpirvTypeBase.ResolveVector(componentType, instructionComponentCount);
            AddType(instruction, vector);
        }

        public Node GetNode(IdRef id)
        {
            if (id == IdRef.Empty)
                return null;
            return _nodeMap[id.Id];
        }

        public IList<Node> GetNodes(IList<IdRef> ids)
        {
            var nodes = new PrintableList<Node>();
            if (ids != null)
                foreach (var idRef in ids)
                    nodes.Add(GetNode(idRef));

            return nodes;
        }

        public void BuildTree(Shader shader)
        {
            _shader = shader;
            _nodeMap = new IdRefDictionary<Node>(_shader.Bound);
            _decorations = new IdRefDictionary<NodeDecorations>(_shader.Bound);
            Function currentFunction = null;
            for (var index = 0; index < _shader.Instructions.Count; index++)
            {
                var instruction = _shader.Instructions[index];
                switch (instruction.OpCode)
                {
                    case Op.OpCapability:
                        CapabilityInstructions.Add((Capability) ParseNode(instruction));
                        break;
                    case Op.OpExtInstImport:
                        ExtInstImportInstructions.Add((ExtInstImport) ParseNode(instruction));
                        break;
                    case Op.OpExtension:
                        ExtensionInstructions.Add((Extension) ParseNode(instruction));
                        break;
                    case Op.OpMemoryModel:
                        MemoryModel = (MemoryModel) ParseNode(instruction);
                        break;
                    case Op.OpEntryPoint:
                    {
                        EntryPointInstructions.Add((EntryPoint) ParseNode(instruction));
                        break;
                    }
                    case Op.OpExecutionMode:
                    {
                        ExecutionModeInstructions.Add((ExecutionMode) ParseNode(instruction));
                        break;
                    }

                    case Op.OpName:
                        _decorations.GetRef(((OpName) instruction).Target).Name = (OpName) instruction;
                        break;
                    case Op.OpMemberName:
                        _decorations.GetRef(((OpMemberName) instruction).Type).AddDecoration(instruction);
                        break;

                    case Op.OpDecorate:
                        _decorations.GetRef(((OpDecorate) instruction).Target).AddDecoration(instruction);
                        break;
                    case Op.OpDecorateId:
                        _decorations.GetRef(((OpDecorateId) instruction).Target).AddDecoration(instruction);
                        break;
                    case Op.OpDecorateString:
                        _decorations.GetRef(((OpDecorateString) instruction).Target).AddDecoration(instruction);
                        break;
                    case Op.OpMemberDecorate:
                        _decorations.GetRef(((OpMemberDecorate) instruction).StructureType).AddDecoration(instruction);
                        break;
                    case Op.OpGroupDecorate:
                    case Op.OpGroupMemberDecorate:
                    case Op.OpDecorationGroup:
                        throw new NotImplementedException(instruction.OpCode + " not implemented yet.");

                    case Op.OpLine:
                    case Op.OpExtInst:
                        break;

                    case Op.OpTypeVoid:
                        ParseVoid((OpTypeVoid) instruction);
                        break;
                    case Op.OpTypeBool:
                        ParseBool((OpTypeBool) instruction);
                        break;
                    case Op.OpTypeInt:
                        ParseInt((OpTypeInt) instruction);
                        break;
                    case Op.OpTypeFloat:
                        ParseFloat((OpTypeFloat) instruction);
                        break;
                    case Op.OpTypeVector:
                        ParseVector((OpTypeVector) instruction);
                        break;
                    case Op.OpTypeMatrix:
                        ParseMatrix((OpTypeMatrix) instruction);
                        break;

                    case Op.OpFunction:
                    {
                        var function = ParseNode(instruction);
                        currentFunction = (Function) function;
                        break;
                    }
                    case Op.OpFunctionParameter:
                    {
                        var param = (FunctionParameter) ParseNode(instruction);
                        currentFunction.Parameters.Add(param);
                        break;
                    }
                    case Op.OpFunctionEnd:
                    {
                        ParseNode(instruction);
                        currentFunction = null;
                        break;
                    }
                    default:
                        ParseNode(instruction);
                        break;
                }
            }

            // Fix forward references.
            INodeWithNext prevSequentialInstruction = null;
            foreach (var instructionAndNode in _nodes)
            {
                var key = instructionAndNode.Key;
                var value = instructionAndNode.Value;
                value.SetUp(key, this);
                if (value is ExecutableNode executableNode)
                {
                    if (prevSequentialInstruction != null)
                        prevSequentialInstruction.Next = executableNode;
                    prevSequentialInstruction = null;
                }

                if (value is INodeWithNext nodeWithNext) prevSequentialInstruction = nodeWithNext;
            }
        }

        private void ParseBool(OpTypeBool instruction)
        {
            AddType(instruction, SpirvTypeBase.Bool);
        }

        private void ParseVoid(OpTypeVoid instruction)
        {
            AddType(instruction, SpirvTypeBase.Void);
        }


        public Node ParseNode(Instruction op)
        {
            //Skip source code line references
            if (op.OpCode == Op.OpLine)
                return null;

            var node = NodeFactory.Create(op.OpCode);
            if (node != null)
            {
                if (op.TryGetResultId(out var id))
                {
                    ref var d = ref _decorations.GetRef(id);
                    node.DebugName = d.Name?.Value;
                    _nodeMap.Add(id, node);
                }

                if (node is SpirvTypeBase type)
                {
                    // Type initialization should be done now as type should has not have forward references and type initialization is essential.
                    type.SetUp(op, this);
                    TypeInstructions.Add(type);
                }
                else
                {
                    // Node initialization should be deferred to later because of potential forward references.
                    _nodes.Add(new KeyValuePair<Instruction, Node>(op, node));
                }
            }

            return node;
        }

        public PairNodeLiteralInteger Parse(PairIdRefLiteralInteger item)
        {
            return new PairNodeLiteralInteger(_nodeMap[item.IdRef], item.LiteralInteger);
        }

        public NestedNode Parse(NestedInstruction instruction)
        {
            var node = NodeFactory.Create(instruction.Instruction.OpCode);
            node.SetUp(instruction.Instruction, this);
            return new NestedNode(node);
        }
    }
}