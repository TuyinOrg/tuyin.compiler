using System.Collections.Generic;
using Toe.SPIRV.Spv;


namespace Toe.SPIRV.Instructions
{
    internal partial class OpBranchConditional: Instruction
    {
        public OpBranchConditional()
        {
        }

        public override Op OpCode { get { return Op.OpBranchConditional; } }
        
        /// <summary>
        /// Returns true if instruction has IdResult field.
        /// </summary>
        public override bool HasResultId => false;

        /// <summary>
        /// Returns true if instruction has IdResultType field.
        /// </summary>
        public override bool HasResultType => false;

        public Spv.IdRef Condition { get; set; }

        public Spv.IdRef TrueLabel { get; set; }

        public Spv.IdRef FalseLabel { get; set; }

        public IList<uint> Branchweights { get; set; }

        /// <summary>
        /// Read complete instruction from the bytecode source.
        /// </summary>
        /// <param name="reader">Bytecode source.</param>
        /// <param name="end">Index of a next word right after this instruction.</param>
        public override void Parse(WordReader reader, uint end)
        {
            ParseOperands(reader, end);
            PostParse(reader, end);
        }

        /// <summary>
        /// Read instruction operands from the bytecode source.
        /// </summary>
        /// <param name="reader">Bytecode source.</param>
        /// <param name="end">Index of a next word right after this instruction.</param>
        public override void ParseOperands(WordReader reader, uint end)
        {
            Condition = Spv.IdRef.Parse(reader, end-reader.Position);
            TrueLabel = Spv.IdRef.Parse(reader, end-reader.Position);
            FalseLabel = Spv.IdRef.Parse(reader, end-reader.Position);
            Branchweights = Spv.LiteralInteger.ParseCollection(reader, end-reader.Position);
        }

        /// <summary>
        /// Process parsed instruction if required.
        /// </summary>
        /// <param name="reader">Bytecode source.</param>
        /// <param name="end">Index of a next word right after this instruction.</param>
        partial void PostParse(WordReader reader, uint end);

        /// <summary>
        /// Calculate number of words to fit complete instruction bytecode.
        /// </summary>
        /// <returns>Number of words in instruction bytecode.</returns>
        public override uint GetWordCount()
        {
            uint wordCount = 0;
            wordCount += Condition.GetWordCount();
            wordCount += TrueLabel.GetWordCount();
            wordCount += FalseLabel.GetWordCount();
            wordCount += Branchweights.GetWordCount();
            return wordCount;
        }

        /// <summary>
        /// Write instruction into bytecode stream.
        /// </summary>
        /// <param name="writer">Bytecode writer.</param>
        public override void Write(WordWriter writer)
        {
            WriteOperands(writer);
            WriteExtras(writer);
        }

        /// <summary>
        /// Write instruction operands into bytecode stream.
        /// </summary>
        /// <param name="writer">Bytecode writer.</param>
        public override void WriteOperands(WordWriter writer)
        {
            Condition.Write(writer);
            TrueLabel.Write(writer);
            FalseLabel.Write(writer);
            Branchweights.Write(writer);
        }

        partial void WriteExtras(WordWriter writer);

        public override string ToString()
        {
            return $"{OpCode} {Condition} {TrueLabel} {FalseLabel} {Branchweights}";
        }
    }
}
