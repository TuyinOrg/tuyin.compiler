using System.Collections.Generic;
using Toe.SPIRV.Spv;


namespace Toe.SPIRV.Instructions
{
    internal partial class OpRayQueryInitializeKHR: Instruction
    {
        public OpRayQueryInitializeKHR()
        {
        }

        public override Op OpCode { get { return Op.OpRayQueryInitializeKHR; } }
        
        /// <summary>
        /// Returns true if instruction has IdResult field.
        /// </summary>
        public override bool HasResultId => false;

        /// <summary>
        /// Returns true if instruction has IdResultType field.
        /// </summary>
        public override bool HasResultType => false;

        public Spv.IdRef RayQuery { get; set; }

        public Spv.IdRef Accel { get; set; }

        public Spv.IdRef RayFlags { get; set; }

        public Spv.IdRef CullMask { get; set; }

        public Spv.IdRef RayOrigin { get; set; }

        public Spv.IdRef RayTMin { get; set; }

        public Spv.IdRef RayDirection { get; set; }

        public Spv.IdRef RayTMax { get; set; }

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
            RayQuery = Spv.IdRef.Parse(reader, end-reader.Position);
            Accel = Spv.IdRef.Parse(reader, end-reader.Position);
            RayFlags = Spv.IdRef.Parse(reader, end-reader.Position);
            CullMask = Spv.IdRef.Parse(reader, end-reader.Position);
            RayOrigin = Spv.IdRef.Parse(reader, end-reader.Position);
            RayTMin = Spv.IdRef.Parse(reader, end-reader.Position);
            RayDirection = Spv.IdRef.Parse(reader, end-reader.Position);
            RayTMax = Spv.IdRef.Parse(reader, end-reader.Position);
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
            wordCount += RayQuery.GetWordCount();
            wordCount += Accel.GetWordCount();
            wordCount += RayFlags.GetWordCount();
            wordCount += CullMask.GetWordCount();
            wordCount += RayOrigin.GetWordCount();
            wordCount += RayTMin.GetWordCount();
            wordCount += RayDirection.GetWordCount();
            wordCount += RayTMax.GetWordCount();
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
            RayQuery.Write(writer);
            Accel.Write(writer);
            RayFlags.Write(writer);
            CullMask.Write(writer);
            RayOrigin.Write(writer);
            RayTMin.Write(writer);
            RayDirection.Write(writer);
            RayTMax.Write(writer);
        }

        partial void WriteExtras(WordWriter writer);

        public override string ToString()
        {
            return $"{OpCode} {RayQuery} {Accel} {RayFlags} {CullMask} {RayOrigin} {RayTMin} {RayDirection} {RayTMax}";
        }
    }
}
