using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal partial class FPFastMathMode : ValueEnum
    {
        [Flags]
        public enum Enumerant
        {
            None = 0x0000,
            [Capability(Capability.Enumerant.Kernel)]
            NotNaN = 0x0001,
            [Capability(Capability.Enumerant.Kernel)]
            NotInf = 0x0002,
            [Capability(Capability.Enumerant.Kernel)]
            NSZ = 0x0004,
            [Capability(Capability.Enumerant.Kernel)]
            AllowRecip = 0x0008,
            [Capability(Capability.Enumerant.Kernel)]
            Fast = 0x0010,
        }

        public FPFastMathMode(Enumerant value)
        {
            Value = value;
        }

        public static FPFastMathMode CreateNone()
        {
            return new FPFastMathMode(Enumerant.None)
            {
            };
        }

        public FPFastMathMode AlsoNone()
        {
            Value |= Enumerant.None;
            return this;
        }

        public static FPFastMathMode CreateNotNaN()
        {
            return new FPFastMathMode(Enumerant.NotNaN)
            {
            };
        }

        public FPFastMathMode AlsoNotNaN()
        {
            Value |= Enumerant.NotNaN;
            return this;
        }

        public static FPFastMathMode CreateNotInf()
        {
            return new FPFastMathMode(Enumerant.NotInf)
            {
            };
        }

        public FPFastMathMode AlsoNotInf()
        {
            Value |= Enumerant.NotInf;
            return this;
        }

        public static FPFastMathMode CreateNSZ()
        {
            return new FPFastMathMode(Enumerant.NSZ)
            {
            };
        }

        public FPFastMathMode AlsoNSZ()
        {
            Value |= Enumerant.NSZ;
            return this;
        }

        public static FPFastMathMode CreateAllowRecip()
        {
            return new FPFastMathMode(Enumerant.AllowRecip)
            {
            };
        }

        public FPFastMathMode AlsoAllowRecip()
        {
            Value |= Enumerant.AllowRecip;
            return this;
        }

        public static FPFastMathMode CreateFast()
        {
            return new FPFastMathMode(Enumerant.Fast)
            {
            };
        }

        public FPFastMathMode AlsoFast()
        {
            Value |= Enumerant.Fast;
            return this;
        }


        public static implicit operator FPFastMathMode(FPFastMathMode.Enumerant value)
        {
            switch (value)
            {
                default:
                    return new FPFastMathMode(value);
            }
        }

        public Enumerant Value { get; private set; }

        public static FPFastMathMode Parse(WordReader reader, uint wordCount)
        {
            var end = reader.Position+wordCount;
            var id = (Enumerant) reader.ReadWord();
            var value = new FPFastMathMode(id);
            value.PostParse(reader, wordCount - 1);
            return value;
        }

        public static FPFastMathMode ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<FPFastMathMode> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<FPFastMathMode>();
            while (reader.Position < end)
            {
                res.Add(Parse(reader, end-reader.Position));
            }
            return res;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public virtual uint GetWordCount()
        {
            uint wordCount = 1;
            return wordCount;
        }

        public void Write(WordWriter writer)
        {
             writer.WriteWord((uint)Value);
        }
    }
}