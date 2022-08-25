using System.Collections.Generic;
using Toe.SPIRV.Instructions;

namespace Toe.SPIRV.Spv
{
    internal static class LiteralSpecConstantOpInteger
    {
        public static NestedInstruction Parse(WordReader reader, uint wordCount)
        {
            var readWord = reader.ReadWord();
            var instruction = InstructionFactory.Create((Spv.Op)readWord);
            instruction.ParseOperands(reader, wordCount);
            return new NestedInstruction(instruction);
        }

        public static NestedInstruction ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<NestedInstruction> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<NestedInstruction>();
            while (reader.Position < end) res.Add(Parse(reader, end - reader.Position));
            return res;
        }
    }
}