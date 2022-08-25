using System.Collections.Generic;
using Toe.SPIRV.Spv;


namespace Toe.SPIRV.Instructions
{
    internal partial class OpGetKernelNDrangeMaxSubGroupSize: InstructionWithId
    {
        public OpGetKernelNDrangeMaxSubGroupSize()
        {
        }

        public override Op OpCode { get { return Op.OpGetKernelNDrangeMaxSubGroupSize; } }
        
        /// <summary>
        /// Returns true if instruction has IdResult field.
        /// </summary>
        public override bool HasResultId => true;

        /// <summary>
        /// Returns true if instruction has IdResultType field.
        /// </summary>
        public override bool HasResultType => true;

        public Spv.IdRef IdResultType { get; set; }

        public Spv.IdRef NDRange { get; set; }

        public Spv.IdRef Invoke { get; set; }

        public Spv.IdRef Param { get; set; }

        public Spv.IdRef ParamSize { get; set; }

        public Spv.IdRef ParamAlign { get; set; }

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
            NDRange = Spv.IdRef.Parse(reader, end-reader.Position);
            Invoke = Spv.IdRef.Parse(reader, end-reader.Position);
            Param = Spv.IdRef.Parse(reader, end-reader.Position);
            ParamSize = Spv.IdRef.Parse(reader, end-reader.Position);
            ParamAlign = Spv.IdRef.Parse(reader, end-reader.Position);
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
            wordCount += NDRange.GetWordCount();
            wordCount += Invoke.GetWordCount();
            wordCount += Param.GetWordCount();
            wordCount += ParamSize.GetWordCount();
            wordCount += ParamAlign.GetWordCount();
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
            NDRange.Write(writer);
            Invoke.Write(writer);
            Param.Write(writer);
            ParamSize.Write(writer);
            ParamAlign.Write(writer);
        }

        partial void WriteExtras(WordWriter writer);

        public override string ToString()
        {
            return $"{IdResultType} {IdResult} = {OpCode} {NDRange} {Invoke} {Param} {ParamSize} {ParamAlign}";
        }
    }
}
