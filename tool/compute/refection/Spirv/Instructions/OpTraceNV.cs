using System.Collections.Generic;
using Toe.SPIRV.Spv;


namespace Toe.SPIRV.Instructions
{
    internal partial class OpTraceNV: Instruction
    {
        public OpTraceNV()
        {
        }

        public override Op OpCode { get { return Op.OpTraceNV; } }
        
        /// <summary>
        /// Returns true if instruction has IdResult field.
        /// </summary>
        public override bool HasResultId => false;

        /// <summary>
        /// Returns true if instruction has IdResultType field.
        /// </summary>
        public override bool HasResultType => false;

        public Spv.IdRef Accel { get; set; }

        public Spv.IdRef RayFlags { get; set; }

        public Spv.IdRef CullMask { get; set; }

        public Spv.IdRef SBTOffset { get; set; }

        public Spv.IdRef SBTStride { get; set; }

        public Spv.IdRef MissIndex { get; set; }

        public Spv.IdRef RayOrigin { get; set; }

        public Spv.IdRef RayTmin { get; set; }

        public Spv.IdRef RayDirection { get; set; }

        public Spv.IdRef RayTmax { get; set; }

        public Spv.IdRef PayloadId { get; set; }

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
            Accel = Spv.IdRef.Parse(reader, end-reader.Position);
            RayFlags = Spv.IdRef.Parse(reader, end-reader.Position);
            CullMask = Spv.IdRef.Parse(reader, end-reader.Position);
            SBTOffset = Spv.IdRef.Parse(reader, end-reader.Position);
            SBTStride = Spv.IdRef.Parse(reader, end-reader.Position);
            MissIndex = Spv.IdRef.Parse(reader, end-reader.Position);
            RayOrigin = Spv.IdRef.Parse(reader, end-reader.Position);
            RayTmin = Spv.IdRef.Parse(reader, end-reader.Position);
            RayDirection = Spv.IdRef.Parse(reader, end-reader.Position);
            RayTmax = Spv.IdRef.Parse(reader, end-reader.Position);
            PayloadId = Spv.IdRef.Parse(reader, end-reader.Position);
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
            wordCount += Accel.GetWordCount();
            wordCount += RayFlags.GetWordCount();
            wordCount += CullMask.GetWordCount();
            wordCount += SBTOffset.GetWordCount();
            wordCount += SBTStride.GetWordCount();
            wordCount += MissIndex.GetWordCount();
            wordCount += RayOrigin.GetWordCount();
            wordCount += RayTmin.GetWordCount();
            wordCount += RayDirection.GetWordCount();
            wordCount += RayTmax.GetWordCount();
            wordCount += PayloadId.GetWordCount();
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
            Accel.Write(writer);
            RayFlags.Write(writer);
            CullMask.Write(writer);
            SBTOffset.Write(writer);
            SBTStride.Write(writer);
            MissIndex.Write(writer);
            RayOrigin.Write(writer);
            RayTmin.Write(writer);
            RayDirection.Write(writer);
            RayTmax.Write(writer);
            PayloadId.Write(writer);
        }

        partial void WriteExtras(WordWriter writer);

        public override string ToString()
        {
            return $"{OpCode} {Accel} {RayFlags} {CullMask} {SBTOffset} {SBTStride} {MissIndex} {RayOrigin} {RayTmin} {RayDirection} {RayTmax} {PayloadId}";
        }
    }
}
