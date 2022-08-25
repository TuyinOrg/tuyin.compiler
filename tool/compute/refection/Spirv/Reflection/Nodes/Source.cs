using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Source : Node
    {
        public Source()
        {
        }

        public Source(Spv.SourceLanguage sourceLanguage, uint version, Node file, string value, string debugName = null)
        {
            this.SourceLanguage = sourceLanguage;
            this.Version = version;
            this.File = file;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSource;

        public Spv.SourceLanguage SourceLanguage { get; set; }

        public uint Version { get; set; }

        public Node File { get; set; }

        public string Value { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return File;
        }

        public Source WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSource)op, treeBuilder);
        }

        public Source SetUp(Action<Source> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSource op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SourceLanguage = op.SourceLanguage;
            Version = op.Version;
            File = treeBuilder.GetNode(op.File);
            Value = op.Value;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Source object.</summary>
        /// <returns>A string that represents the Source object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Source({SourceLanguage}, {Version}, {File}, {Value}, {DebugName})";
        }
    }
}