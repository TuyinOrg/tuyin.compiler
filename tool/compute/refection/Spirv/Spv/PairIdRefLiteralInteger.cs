using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal class PairIdRefLiteralInteger
    {
        public IdRef IdRef { get; set; }
        public uint LiteralInteger { get; set; }

        public static PairIdRefLiteralInteger Parse(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PairIdRefLiteralInteger();
            res.IdRef = IdRef.Parse(reader, end - reader.Position);
            res.LiteralInteger = Spv.LiteralInteger.Parse(reader, end - reader.Position);
            return res;
        }

        public static PairIdRefLiteralInteger ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }


        public static IList<PairIdRefLiteralInteger> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<PairIdRefLiteralInteger>();
            while (reader.Position < end) res.Add(Parse(reader, end - reader.Position));
            return res;
        }
        public virtual uint GetWordCount()
        {
            uint wordCount = 0;
            wordCount += IdRef.GetWordCount();
            wordCount += LiteralInteger.GetWordCount();
            return wordCount;
        }

        public virtual void Write(WordWriter writer)
        {
            IdRef.Write(writer);
            LiteralInteger.Write(writer);
        }

        public override string ToString()
        {
            return $"{{ {IdRef} {LiteralInteger} }}";
        }
    }
}