using System.Collections.Generic;
using Toe.SPIRV.Instructions;

namespace Toe.SPIRV.Spv
{
    internal static class IdResultType
    {
        public static IdRef Parse(WordReader reader, uint wordCount)
        {
            return IdRef.Parse(reader, wordCount);
        }

        public static IdRef ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return IdRef.Empty;
            return Parse(reader, wordCount);
        }

        public static IList<IdRef> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<IdRef>();
            while (reader.Position < end) res.Add(Parse(reader, end - reader.Position));
            return res;
        }
    }
}