using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Nodes;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;
using Capability = Toe.SPIRV.Reflection.Nodes.Capability;
using ExecutionMode = Toe.SPIRV.Reflection.Nodes.ExecutionMode;
using MemoryModel = Toe.SPIRV.Reflection.Nodes.MemoryModel;

namespace Toe.SPIRV.Reflection
{
    internal class ShaderReflection
    {
        private class InterfaceCollector: NodeVisitor
        {
            public IList<Variable> Inputs { get; } = new PrintableList<Variable>();
            public IList<Variable> Outputs { get; } = new PrintableList<Variable>();
            public IList<Variable> Uniforms { get; } = new PrintableList<Variable>();

            protected override void VisitVariable(Variable node)
            {
                switch (node.StorageClass.Value)
                {
                    case StorageClass.Enumerant.Input:
                        Inputs.Add(node);
                        break;
                    case StorageClass.Enumerant.Output:
                        Outputs.Add(node);
                        break;
                    case StorageClass.Enumerant.Uniform:
                        Uniforms.Add(node);
                        break;
                }

                base.VisitVariable(node);
            }
        }
        public ShaderReflection()
        {
        }

        public ShaderReflection(Shader shader)
        {
            var context = new SpirvInstructionTreeBuilder();
            context.BuildTree(shader);
            
            Structures = context.TypeInstructions.Where(_=>_.TypeCategory == SpirvTypeCategory.Struct).Select(_=>(TypeStruct)_).ToList();
            AddInstruction(context.CapabilityInstructions, CapabilityInstructions);
            AddInstruction(context.ExtensionInstructions, ExtensionInstructions);
            AddInstruction(context.ExtInstImportInstructions, ExtInstImportInstructions);
            MemoryModel = context.MemoryModel;
            AddInstruction(context.EntryPointInstructions, EntryPointInstructions);
            AddInstruction(context.ExecutionModeInstructions, ExecutionModeInstructions);
        }

        private static void AddInstruction<T>(List<T> source, List<T> dst)
        {
            foreach (var entryFunction in source)
            {
                dst.Add(entryFunction);
            }
        }

        public IReadOnlyList<TypeStruct> Structures { get; private set; }

        /// <summary>
        /// All OpCapability instructions. 
        /// </summary>
        public List<Capability> CapabilityInstructions { get; } = new List<Capability>();
        /// <summary>
        /// Optional OpExtension instructions (extensions to SPIR-V).
        /// </summary>
        public List<Extension> ExtensionInstructions { get; } = new List<Extension>();
        /// <summary>
        /// Optional OpExtInstImport instructions.
        /// </summary>
        public List<ExtInstImport> ExtInstImportInstructions { get; } = new List<ExtInstImport>();
        /// <summary>
        /// The single required OpMemoryModel instruction.
        /// </summary>
        public MemoryModel MemoryModel { get; set; }
        /// <summary>
        /// All entry point declarations, using OpEntryPoint.
        /// </summary>
        public List<EntryPoint> EntryPointInstructions = new List<EntryPoint>();
        /// <summary>
        /// All entry point declarations, using OpEntryPoint.
        /// </summary>
        public List<ExecutionMode> ExecutionModeInstructions = new List<ExecutionMode>();

        public ShaderReflection WithCapability(Spv.Capability capability) { return this.With(new Capability(capability)); }

        public ShaderReflection WithExtension(string name) { return this.With(new Extension(name)); }

        public ShaderReflection WithExtInstImport(string name) { return this.With(new ExtInstImport(name)); }

        public ShaderReflection WithMemoryModel(Spv.AddressingModel addressingModel, Spv.MemoryModel memoryModel) { return this.With(new MemoryModel(addressingModel, memoryModel)); }

        public ShaderReflection WithEntryPoint(Spv.ExecutionModel executionModel, Function value, string name, IEnumerable<Node> @interface) { return this.With(new EntryPoint(executionModel, value, name, @interface)); }

        public ShaderReflection WithEntryPoint(Spv.ExecutionModel executionModel, Function value, string name = null)
        {
            var interfaceCollector = new InterfaceCollector();
            interfaceCollector.Visit(value);
            return this.With(new EntryPoint(executionModel, value, name ?? value.DebugName ?? "main", interfaceCollector.Inputs.Concat(interfaceCollector.Outputs).Concat(interfaceCollector.Uniforms)));
        }

        public ShaderReflection WithExecutionMode(Node EntryPoint, Spv.ExecutionMode Mode) { return this.With(new ExecutionMode(EntryPoint, Mode)); }

        public ShaderReflection With(Capability node) { CapabilityInstructions.Add(node); return this; }

        public ShaderReflection With(Extension node) { ExtensionInstructions.Add(node); return this; }

        public ShaderReflection With(ExtInstImport node) { ExtInstImportInstructions.Add(node); return this; }

        public ShaderReflection With(MemoryModel node) { MemoryModel = node; return this; }

        public ShaderReflection With(EntryPoint node) { EntryPointInstructions.Add(node); return this; }

        public ShaderReflection With(ExecutionMode node) { ExecutionModeInstructions.Add(node); return this; }

        public Shader BuildShader()
        {
            var context = new SpirvInstructionsBuilder();
            return context.Build(this);
        }
    }
}