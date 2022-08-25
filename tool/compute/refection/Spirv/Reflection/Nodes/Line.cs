using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Line : Node
    {
        public Line()
        {
        }

        public Line(Node file, uint value, uint column, string debugName = null)
        {
            this.File = file;
            this.Value = value;
            this.Column = column;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpLine;

        public Node File { get; set; }

        public uint Value { get; set; }

        public uint Column { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return File;
        }

        public Line WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpLine)op, treeBuilder);
        }

        public Line SetUp(Action<Line> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpLine op, SpirvInstructionTreeBuilder treeBuilder)
        {
            File = treeBuilder.GetNode(op.File);
            Value = op.Value;
            Column = op.Column;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Line object.</summary>
        /// <returns>A string that represents the Line object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Line({File}, {Value}, {Column}, {DebugName})";
        }
    }
}