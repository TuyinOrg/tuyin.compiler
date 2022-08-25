using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal partial class KernelProfilingInfo : ValueEnum
    {
        [Flags]
        public enum Enumerant
        {
            None = 0x0000,
            [Capability(Capability.Enumerant.Kernel)]
            CmdExecTime = 0x0001,
        }

        public KernelProfilingInfo(Enumerant value)
        {
            Value = value;
        }

        public static KernelProfilingInfo CreateNone()
        {
            return new KernelProfilingInfo(Enumerant.None)
            {
            };
        }

        public KernelProfilingInfo AlsoNone()
        {
            Value |= Enumerant.None;
            return this;
        }

        public static KernelProfilingInfo CreateCmdExecTime()
        {
            return new KernelProfilingInfo(Enumerant.CmdExecTime)
            {
            };
        }

        public KernelProfilingInfo AlsoCmdExecTime()
        {
            Value |= Enumerant.CmdExecTime;
            return this;
        }


        public static implicit operator KernelProfilingInfo(KernelProfilingInfo.Enumerant value)
        {
            switch (value)
            {
                default:
                    return new KernelProfilingInfo(value);
            }
        }

        public Enumerant Value { get; private set; }

        public static KernelProfilingInfo Parse(WordReader reader, uint wordCount)
        {
            var end = reader.Position+wordCount;
            var id = (Enumerant) reader.ReadWord();
            var value = new KernelProfilingInfo(id);
            value.PostParse(reader, wordCount - 1);
            return value;
        }

        public static KernelProfilingInfo ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<KernelProfilingInfo> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<KernelProfilingInfo>();
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