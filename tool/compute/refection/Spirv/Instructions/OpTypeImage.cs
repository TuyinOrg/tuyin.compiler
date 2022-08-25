using System.Collections.Generic;
using Toe.SPIRV.Spv;


namespace Toe.SPIRV.Instructions
{
    internal partial class OpTypeImage: TypeInstruction
    {
        public OpTypeImage()
        {
        }

        public override Op OpCode { get { return Op.OpTypeImage; } }
        
        /// <summary>
        /// Returns true if instruction has IdResult field.
        /// </summary>
        public override bool HasResultId => true;

        /// <summary>
        /// Returns true if instruction has IdResultType field.
        /// </summary>
        public override bool HasResultType => false;

        public Spv.IdRef SampledType { get; set; }

        public Spv.Dim Dim { get; set; }

        public uint Depth { get; set; }

        public uint Arrayed { get; set; }

        public uint MS { get; set; }

        public uint Sampled { get; set; }

        public Spv.ImageFormat ImageFormat { get; set; }

        public Spv.AccessQualifier AccessQualifier { get; set; }

        /// <summary>
        /// Read complete instruction from the bytecode source.
        /// </summary>
        /// <param name="reader">Bytecode source.</param>
        /// <param name="end">Index of a next word right after this instruction.</param>
        public override void Parse(WordReader reader, uint end)
        {
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
            SampledType = Spv.IdRef.Parse(reader, end-reader.Position);
            Dim = Spv.Dim.Parse(reader, end-reader.Position);
            Depth = Spv.LiteralInteger.Parse(reader, end-reader.Position);
            Arrayed = Spv.LiteralInteger.Parse(reader, end-reader.Position);
            MS = Spv.LiteralInteger.Parse(reader, end-reader.Position);
            Sampled = Spv.LiteralInteger.Parse(reader, end-reader.Position);
            ImageFormat = Spv.ImageFormat.Parse(reader, end-reader.Position);
            AccessQualifier = Spv.AccessQualifier.ParseOptional(reader, end-reader.Position);
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
            wordCount += IdResult.GetWordCount();
            wordCount += SampledType.GetWordCount();
            wordCount += Dim.GetWordCount();
            wordCount += Depth.GetWordCount();
            wordCount += Arrayed.GetWordCount();
            wordCount += MS.GetWordCount();
            wordCount += Sampled.GetWordCount();
            wordCount += ImageFormat.GetWordCount();
            wordCount += AccessQualifier?.GetWordCount() ?? 0u;
            return wordCount;
        }

        /// <summary>
        /// Write instruction into bytecode stream.
        /// </summary>
        /// <param name="writer">Bytecode writer.</param>
        public override void Write(WordWriter writer)
        {
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
            SampledType.Write(writer);
            Dim.Write(writer);
            Depth.Write(writer);
            Arrayed.Write(writer);
            MS.Write(writer);
            Sampled.Write(writer);
            ImageFormat.Write(writer);
            if (AccessQualifier != null) AccessQualifier.Write(writer);
        }

        partial void WriteExtras(WordWriter writer);

        public override string ToString()
        {
            return $"{IdResult} = {OpCode} {SampledType} {Dim} {Depth} {Arrayed} {MS} {Sampled} {ImageFormat} {AccessQualifier}";
        }
    }
}
