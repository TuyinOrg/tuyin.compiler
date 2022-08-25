using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal static class LiteralString
    {
        public static string Parse(WordReader reader, uint wordCount)
        {
            return reader.ReadString();
        }

        public static string ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<string> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<string>();
            while (reader.Position < end) res.Add(Parse(reader, end - reader.Position));
            return res;
        }

        public static uint GetWordCount(string name)
        {
            return WordWriter.GetWordCount(name ?? string.Empty);
        }

        public static void Write(WordWriter writer, string name)
        {
            writer.Write(name);
        }
    }
}