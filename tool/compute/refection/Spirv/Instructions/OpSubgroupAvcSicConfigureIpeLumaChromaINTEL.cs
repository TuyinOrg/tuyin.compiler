using System.Collections.Generic;
using Toe.SPIRV.Spv;


namespace Toe.SPIRV.Instructions
{
    internal partial class OpSubgroupAvcSicConfigureIpeLumaChromaINTEL: InstructionWithId
    {
        public OpSubgroupAvcSicConfigureIpeLumaChromaINTEL()
        {
        }

        public override Op OpCode { get { return Op.OpSubgroupAvcSicConfigureIpeLumaChromaINTEL; } }
        
        /// <summary>
        /// Returns true if instruction has IdResult field.
        /// </summary>
        public override bool HasResultId => true;

        /// <summary>
        /// Returns true if instruction has IdResultType field.
        /// </summary>
        public override bool HasResultType => true;

        public Spv.IdRef IdResultType { get; set; }

        public Spv.IdRef LumaIntraPartitionMask { get; set; }

        public Spv.IdRef IntraNeighbourAvailabilty { get; set; }

        public Spv.IdRef LeftEdgeLumaPixels { get; set; }

        public Spv.IdRef UpperLeftCornerLumaPixel { get; set; }

        public Spv.IdRef UpperEdgeLumaPixels { get; set; }

        public Spv.IdRef UpperRightEdgeLumaPixels { get; set; }

        public Spv.IdRef LeftEdgeChromaPixels { get; set; }

        public Spv.IdRef UpperLeftCornerChromaPixel { get; set; }

        public Spv.IdRef UpperEdgeChromaPixels { get; set; }

        public Spv.IdRef SadAdjustment { get; set; }

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
            LumaIntraPartitionMask = Spv.IdRef.Parse(reader, end-reader.Position);
            IntraNeighbourAvailabilty = Spv.IdRef.Parse(reader, end-reader.Position);
            LeftEdgeLumaPixels = Spv.IdRef.Parse(reader, end-reader.Position);
            UpperLeftCornerLumaPixel = Spv.IdRef.Parse(reader, end-reader.Position);
            UpperEdgeLumaPixels = Spv.IdRef.Parse(reader, end-reader.Position);
            UpperRightEdgeLumaPixels = Spv.IdRef.Parse(reader, end-reader.Position);
            LeftEdgeChromaPixels = Spv.IdRef.Parse(reader, end-reader.Position);
            UpperLeftCornerChromaPixel = Spv.IdRef.Parse(reader, end-reader.Position);
            UpperEdgeChromaPixels = Spv.IdRef.Parse(reader, end-reader.Position);
            SadAdjustment = Spv.IdRef.Parse(reader, end-reader.Position);
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
            wordCount += LumaIntraPartitionMask.GetWordCount();
            wordCount += IntraNeighbourAvailabilty.GetWordCount();
            wordCount += LeftEdgeLumaPixels.GetWordCount();
            wordCount += UpperLeftCornerLumaPixel.GetWordCount();
            wordCount += UpperEdgeLumaPixels.GetWordCount();
            wordCount += UpperRightEdgeLumaPixels.GetWordCount();
            wordCount += LeftEdgeChromaPixels.GetWordCount();
            wordCount += UpperLeftCornerChromaPixel.GetWordCount();
            wordCount += UpperEdgeChromaPixels.GetWordCount();
            wordCount += SadAdjustment.GetWordCount();
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
            LumaIntraPartitionMask.Write(writer);
            IntraNeighbourAvailabilty.Write(writer);
            LeftEdgeLumaPixels.Write(writer);
            UpperLeftCornerLumaPixel.Write(writer);
            UpperEdgeLumaPixels.Write(writer);
            UpperRightEdgeLumaPixels.Write(writer);
            LeftEdgeChromaPixels.Write(writer);
            UpperLeftCornerChromaPixel.Write(writer);
            UpperEdgeChromaPixels.Write(writer);
            SadAdjustment.Write(writer);
            Payload.Write(writer);
        }

        partial void WriteExtras(WordWriter writer);

        public override string ToString()
        {
            return $"{IdResultType} {IdResult} = {OpCode} {LumaIntraPartitionMask} {IntraNeighbourAvailabilty} {LeftEdgeLumaPixels} {UpperLeftCornerLumaPixel} {UpperEdgeLumaPixels} {UpperRightEdgeLumaPixels} {LeftEdgeChromaPixels} {UpperLeftCornerChromaPixel} {UpperEdgeChromaPixels} {SadAdjustment} {Payload}";
        }
    }
}
