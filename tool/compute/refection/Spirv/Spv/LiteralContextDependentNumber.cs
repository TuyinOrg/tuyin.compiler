using System.Collections.Generic;
using Toe.SPIRV.Instructions;

namespace Toe.SPIRV.Spv
{
    internal class LiteralContextDependentNumber
    {
        public Value Value { get; set; }

        public LiteralContextDependentNumber()
        {
        }

        public LiteralContextDependentNumber(Value value)
        {
            Value = value;
        }

        public static LiteralContextDependentNumber Parse(WordReader reader, uint wordCount, InstructionWithId type)
        {
            var typeInstruction = (TypeInstruction)type;
            var size = typeInstruction.SizeInWords;
            return new LiteralContextDependentNumber(new Value(reader.ReadBytes(size), typeInstruction));
        }

        public static LiteralContextDependentNumber ParseOptional(WordReader reader, uint wordCount,
            InstructionWithId type)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount, type);
        }

        public static IList<LiteralContextDependentNumber> ParseCollection(WordReader reader, uint wordCount,
            InstructionWithId type)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<LiteralContextDependentNumber>();
            while (reader.Position < end) res.Add(Parse(reader, end - reader.Position, type));
            return res;
        }

        public virtual uint GetWordCount()
        {
            return Value.GetWordCount();
        }

        public virtual void Write(WordWriter writer)
        {
            Value.Write(writer);
        }

        public override string ToString()
        {
            if (Value != null)
                return Value.ToString();
            return base.ToString();
        }
    }
}