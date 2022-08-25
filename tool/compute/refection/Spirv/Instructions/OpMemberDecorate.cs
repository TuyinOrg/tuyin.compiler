using System.Collections.Generic;
using Toe.SPIRV.Spv;


namespace Toe.SPIRV.Instructions
{
    internal partial class OpMemberDecorate: Instruction
    {
        public OpMemberDecorate()
        {
        }

        public override Op OpCode { get { return Op.OpMemberDecorate; } }
        
        /// <summary>
        /// Returns true if instruction has IdResult field.
        /// </summary>
        public override bool HasResultId => false;

        /// <summary>
        /// Returns true if instruction has IdResultType field.
        /// </summary>
        public override bool HasResultType => false;

        public Spv.IdRef StructureType { get; set; }

        public uint Member { get; set; }

        public Spv.Decoration Decoration { get; set; }

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
            StructureType = Spv.IdRef.Parse(reader, end-reader.Position);
            Member = Spv.LiteralInteger.Parse(reader, end-reader.Position);
            Decoration = Spv.Decoration.Parse(reader, end-reader.Position);
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
            wordCount += StructureType.GetWordCount();
            wordCount += Member.GetWordCount();
            wordCount += Decoration.GetWordCount();
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
            StructureType.Write(writer);
            Member.Write(writer);
            Decoration.Write(writer);
        }

        partial void WriteExtras(WordWriter writer);

        public override string ToString()
        {
            return $"{OpCode} {StructureType} {Member} {Decoration}";
        }
    }
}
