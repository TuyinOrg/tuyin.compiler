using System.Collections.Generic;
using Toe.SPIRV.Spv;


namespace Toe.SPIRV.Instructions
{
    internal partial class OpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL: InstructionWithId
    {
        public OpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL()
        {
        }

        public override Op OpCode { get { return Op.OpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL; } }
        
        /// <summary>
        /// Returns true if instruction has IdResult field.
        /// </summary>
        public override bool HasResultId => true;

        /// <summary>
        /// Returns true if instruction has IdResultType field.
        /// </summary>
        public override bool HasResultType => true;

        public Spv.IdRef IdResultType { get; set; }

        public Spv.IdRef LumaModePenalty { get; set; }

        public Spv.IdRef LumaPackedNeighborModes { get; set; }

        public Spv.IdRef LumaPackedNonDcPenalty { get; set; }

        public Spv.IdRef Payload { get; set; }

        /// <summary>
        /// Read complete instruction from the bytecode source.
        /// </summary>
        /// <param name="reader">Bytecode source.</param>
        /// <param name="end">Index of a next word right after this instruction.</param>
        public override void Parse(WordReader reader, uint end)
        {
            IdResultType = Spv.IdResultType.Parse(reader, end-reader.Position);
            IdResult = Spv.IdResult.Parse(reader, end-reader.Position);
            reader.Instructions.Add(this);
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
            LumaModePenalty = Spv.IdRef.Parse(reader, end-reader.Position);
            LumaPackedNeighborModes = Spv.IdRef.Parse(reader, end-reader.Position);
            LumaPackedNonDcPenalty = Spv.IdRef.Parse(reader, end-reader.Position);
            Payload = Spv.IdRef.Parse(reader, end-reader.Position);
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
            wordCount += IdResultType.GetWordCount();
            wordCount += IdResult.GetWordCount();
            wordCount += LumaModePenalty.GetWordCount();
            wordCount += LumaPackedNeighborModes.GetWordCount();
            wordCount += LumaPackedNonDcPenalty.GetWordCount();
            wordCount += Payload.GetWordCount();
            return wordCount;
        }

        /// <summary>
        /// Write instruction into bytecode stream.
        /// </summary>
        /// <param name="writer">Bytecode writer.</param>
        public override void Write(WordWriter writer)
        {
            IdResultType.Write(writer);
            IdResult.Write(writer);
            WriteOperands(writer);
            WriteExtras(writer);
        }

        /// <summary>
        /// Write instruction operands into bytecode stream.
        /// </summary>
        /// <param name="writer">Bytecode writer.</param>
        public override void WriteOperands(WordWriter writer)
        {
            LumaModePenalty.Write(writer);
            LumaPackedNeighborModes.Write(writer);
            LumaPackedNonDcPenalty.Write(writer);
            Payload.Write(writer);
        }

        partial void WriteExtras(WordWriter writer);

        public override string ToString()
        {
            return $"{IdResultType} {IdResult} = {OpCode} {LumaModePenalty} {LumaPackedNeighborModes} {LumaPackedNonDcPenalty} {Payload}";
        }
    }
}
