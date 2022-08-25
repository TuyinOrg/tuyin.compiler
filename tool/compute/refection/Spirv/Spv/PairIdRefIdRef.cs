using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal class PairIdRefIdRef
    {
        public IdRef IdRef { get; set; }
        public IdRef IdRef2 { get; set; }

        public static PairIdRefIdRef Parse(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PairIdRefIdRef();
            res.IdRef = IdRef.Parse(reader, end - reader.Position);
            res.IdRef2 = IdRef.Parse(reader, end - reader.Position);
            return res;
        }

        public static PairIdRefIdRef ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }


        public static IList<PairIdRefIdRef> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<PairIdRefIdRef>();
            while (reader.Position < end) res.Add(Parse(reader, end - reader.Position));
            return res;
        }
        public virtual uint GetWordCount()
        {
            uint wordCount = 0;
            wordCount += IdRef.GetWordCount();
            wordCount += IdRef2.GetWordCount();
            return wordCount;
        }

        public virtual void Write(WordWriter writer)
        {
            IdRef.Write(writer);
            IdRef2.Write(writer);
        }
        public override string ToString()
        {
            return $"{{ {IdRef} {IdRef2} }}";
        }
    }
}