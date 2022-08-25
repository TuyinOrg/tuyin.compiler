using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal class PairLiteralIntegerIdRef
    {
        public uint LiteralInteger { get; set; }
        public IdRef IdRef { get; set; }

        public static PairLiteralIntegerIdRef Parse(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PairLiteralIntegerIdRef();
            res.LiteralInteger = Spv.LiteralInteger.Parse(reader, end - reader.Position);
            res.IdRef = IdRef.Parse(reader, end - reader.Position);
            return res;
        }

        public static PairLiteralIntegerIdRef ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<PairLiteralIntegerIdRef> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<PairLiteralIntegerIdRef>();
            while (reader.Position < end) res.Add(Parse(reader, end - reader.Position));
            return res;
        }

        public virtual uint GetWordCount()
        {
            uint wordCount = 0;
            wordCount += LiteralInteger.GetWordCount();
            wordCount += IdRef.GetWordCount();
            return wordCount;
        }


        public virtual void Write(WordWriter writer)
        {
            LiteralInteger.Write(writer);
            IdRef.Write(writer);
        }

        public override string ToString()
        {
            return $"{{ {LiteralInteger} {IdRef} }}";
        }
    }
}