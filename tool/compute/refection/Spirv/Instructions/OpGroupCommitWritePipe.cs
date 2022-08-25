using System.Collections.Generic;
using Toe.SPIRV.Spv;


namespace Toe.SPIRV.Instructions
{
    internal partial class OpGroupCommitWritePipe: Instruction
    {
        public OpGroupCommitWritePipe()
        {
        }

        public override Op OpCode { get { return Op.OpGroupCommitWritePipe; } }
        
        /// <summary>
        /// Returns true if instruction has IdResult field.
        /// </summary>
        public override bool HasResultId => false;

        /// <summary>
        /// Returns true if instruction has IdResultType field.
        /// </summary>
        public override bool HasResultType => false;

        public uint Execution { get; set; }

        public Spv.IdRef Pipe { get; set; }

        public Spv.IdRef ReserveId { get; set; }

        public Spv.IdRef PacketSize { get; set; }

        public Spv.IdRef PacketAlignment { get; set; }

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
            Execution = Spv.IdScope.Parse(reader, end-reader.Position);
            Pipe = Spv.IdRef.Parse(reader, end-reader.Position);
            ReserveId = Spv.IdRef.Parse(reader, end-reader.Position);
            PacketSize = Spv.IdRef.Parse(reader, end-reader.Position);
            PacketAlignment = Spv.IdRef.Parse(reader, end-reader.Position);
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
            wordCount += Execution.GetWordCount();
            wordCount += Pipe.GetWordCount();
            wordCount += ReserveId.GetWordCount();
            wordCount += PacketSize.GetWordCount();
            wordCount += PacketAlignment.GetWordCount();
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
            Execution.Write(writer);
            Pipe.Write(writer);
            ReserveId.Write(writer);
            PacketSize.Write(writer);
            PacketAlignment.Write(writer);
        }

        partial void WriteExtras(WordWriter writer);

        public override string ToString()
        {
            return $"{OpCode} {Execution} {Pipe} {ReserveId} {PacketSize} {PacketAlignment}";
        }
    }
}
