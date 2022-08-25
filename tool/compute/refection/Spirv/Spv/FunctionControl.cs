using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal partial class FunctionControl : ValueEnum
    {
        [Flags]
        public enum Enumerant
        {
            None = 0x0000,
            Inline = 0x0001,
            DontInline = 0x0002,
            Pure = 0x0004,
            Const = 0x0008,
        }

        public FunctionControl(Enumerant value)
        {
            Value = value;
        }

        public static FunctionControl CreateNone()
        {
            return new FunctionControl(Enumerant.None)
            {
            };
        }

        public FunctionControl AlsoNone()
        {
            Value |= Enumerant.None;
            return this;
        }

        public static FunctionControl CreateInline()
        {
            return new FunctionControl(Enumerant.Inline)
            {
            };
        }

        public FunctionControl AlsoInline()
        {
            Value |= Enumerant.Inline;
            return this;
        }

        public static FunctionControl CreateDontInline()
        {
            return new FunctionControl(Enumerant.DontInline)
            {
            };
        }

        public FunctionControl AlsoDontInline()
        {
            Value |= Enumerant.DontInline;
            return this;
        }

        public static FunctionControl CreatePure()
        {
            return new FunctionControl(Enumerant.Pure)
            {
            };
        }

        public FunctionControl AlsoPure()
        {
            Value |= Enumerant.Pure;
            return this;
        }

        public static FunctionControl CreateConst()
        {
            return new FunctionControl(Enumerant.Const)
            {
            };
        }

        public FunctionControl AlsoConst()
        {
            Value |= Enumerant.Const;
            return this;
        }


        public static implicit operator FunctionControl(FunctionControl.Enumerant value)
        {
            switch (value)
            {
                default:
                    return new FunctionControl(value);
            }
        }

        public Enumerant Value { get; private set; }

        public static FunctionControl Parse(WordReader reader, uint wordCount)
        {
            var end = reader.Position+wordCount;
            var id = (Enumerant) reader.ReadWord();
            var value = new FunctionControl(id);
            value.PostParse(reader, wordCount - 1);
            return value;
        }

        public static FunctionControl ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<FunctionControl> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<FunctionControl>();
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