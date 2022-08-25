using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal partial class SelectionControl : ValueEnum
    {
        [Flags]
        public enum Enumerant
        {
            None = 0x0000,
            Flatten = 0x0001,
            DontFlatten = 0x0002,
        }

        public SelectionControl(Enumerant value)
        {
            Value = value;
        }

        public static SelectionControl CreateNone()
        {
            return new SelectionControl(Enumerant.None)
            {
            };
        }

        public SelectionControl AlsoNone()
        {
            Value |= Enumerant.None;
            return this;
        }

        public static SelectionControl CreateFlatten()
        {
            return new SelectionControl(Enumerant.Flatten)
            {
            };
        }

        public SelectionControl AlsoFlatten()
        {
            Value |= Enumerant.Flatten;
            return this;
        }

        public static SelectionControl CreateDontFlatten()
        {
            return new SelectionControl(Enumerant.DontFlatten)
            {
            };
        }

        public SelectionControl AlsoDontFlatten()
        {
            Value |= Enumerant.DontFlatten;
            return this;
        }


        public static implicit operator SelectionControl(SelectionControl.Enumerant value)
        {
            switch (value)
            {
                default:
                    return new SelectionControl(value);
            }
        }

        public Enumerant Value { get; private set; }

        public static SelectionControl Parse(WordReader reader, uint wordCount)
        {
            var end = reader.Position+wordCount;
            var id = (Enumerant) reader.ReadWord();
            var value = new SelectionControl(id);
            value.PostParse(reader, wordCount - 1);
            return value;
        }

        public static SelectionControl ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<SelectionControl> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<SelectionControl>();
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