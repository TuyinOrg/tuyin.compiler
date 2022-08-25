using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal partial class MemorySemantics : ValueEnum
    {
        [Flags]
        public enum Enumerant
        {
            Relaxed = 0x0000,
            None = 0x0000,
            Acquire = 0x0002,
            Release = 0x0004,
            AcquireRelease = 0x0008,
            SequentiallyConsistent = 0x0010,
            [Capability(Capability.Enumerant.Shader)]
            UniformMemory = 0x0040,
            SubgroupMemory = 0x0080,
            WorkgroupMemory = 0x0100,
            CrossWorkgroupMemory = 0x0200,
            [Capability(Capability.Enumerant.AtomicStorage)]
            AtomicCounterMemory = 0x0400,
            ImageMemory = 0x0800,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            OutputMemory = 0x1000,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            OutputMemoryKHR = 0x1000,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            MakeAvailable = 0x2000,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            MakeAvailableKHR = 0x2000,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            MakeVisible = 0x4000,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            MakeVisibleKHR = 0x4000,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            Volatile = 0x8000,
        }

        public MemorySemantics(Enumerant value)
        {
            Value = value;
        }

        public static MemorySemantics CreateRelaxed()
        {
            return new MemorySemantics(Enumerant.Relaxed)
            {
            };
        }

        public MemorySemantics AlsoRelaxed()
        {
            Value |= Enumerant.Relaxed;
            return this;
        }

        public static MemorySemantics CreateNone()
        {
            return new MemorySemantics(Enumerant.None)
            {
            };
        }

        public MemorySemantics AlsoNone()
        {
            Value |= Enumerant.None;
            return this;
        }

        public static MemorySemantics CreateAcquire()
        {
            return new MemorySemantics(Enumerant.Acquire)
            {
            };
        }

        public MemorySemantics AlsoAcquire()
        {
            Value |= Enumerant.Acquire;
            return this;
        }

        public static MemorySemantics CreateRelease()
        {
            return new MemorySemantics(Enumerant.Release)
            {
            };
        }

        public MemorySemantics AlsoRelease()
        {
            Value |= Enumerant.Release;
            return this;
        }

        public static MemorySemantics CreateAcquireRelease()
        {
            return new MemorySemantics(Enumerant.AcquireRelease)
            {
            };
        }

        public MemorySemantics AlsoAcquireRelease()
        {
            Value |= Enumerant.AcquireRelease;
            return this;
        }

        public static MemorySemantics CreateSequentiallyConsistent()
        {
            return new MemorySemantics(Enumerant.SequentiallyConsistent)
            {
            };
        }

        public MemorySemantics AlsoSequentiallyConsistent()
        {
            Value |= Enumerant.SequentiallyConsistent;
            return this;
        }

        public static MemorySemantics CreateUniformMemory()
        {
            return new MemorySemantics(Enumerant.UniformMemory)
            {
            };
        }

        public MemorySemantics AlsoUniformMemory()
        {
            Value |= Enumerant.UniformMemory;
            return this;
        }

        public static MemorySemantics CreateSubgroupMemory()
        {
            return new MemorySemantics(Enumerant.SubgroupMemory)
            {
            };
        }

        public MemorySemantics AlsoSubgroupMemory()
        {
            Value |= Enumerant.SubgroupMemory;
            return this;
        }

        public static MemorySemantics CreateWorkgroupMemory()
        {
            return new MemorySemantics(Enumerant.WorkgroupMemory)
            {
            };
        }

        public MemorySemantics AlsoWorkgroupMemory()
        {
            Value |= Enumerant.WorkgroupMemory;
            return this;
        }

        public static MemorySemantics CreateCrossWorkgroupMemory()
        {
            return new MemorySemantics(Enumerant.CrossWorkgroupMemory)
            {
            };
        }

        public MemorySemantics AlsoCrossWorkgroupMemory()
        {
            Value |= Enumerant.CrossWorkgroupMemory;
            return this;
        }

        public static MemorySemantics CreateAtomicCounterMemory()
        {
            return new MemorySemantics(Enumerant.AtomicCounterMemory)
            {
            };
        }

        public MemorySemantics AlsoAtomicCounterMemory()
        {
            Value |= Enumerant.AtomicCounterMemory;
            return this;
        }

        public static MemorySemantics CreateImageMemory()
        {
            return new MemorySemantics(Enumerant.ImageMemory)
            {
            };
        }

        public MemorySemantics AlsoImageMemory()
        {
            Value |= Enumerant.ImageMemory;
            return this;
        }

        public static MemorySemantics CreateOutputMemory()
        {
            return new MemorySemantics(Enumerant.OutputMemory)
            {
            };
        }

        public MemorySemantics AlsoOutputMemory()
        {
            Value |= Enumerant.OutputMemory;
            return this;
        }

        public static MemorySemantics CreateOutputMemoryKHR()
        {
            return new MemorySemantics(Enumerant.OutputMemoryKHR)
            {
            };
        }

        public MemorySemantics AlsoOutputMemoryKHR()
        {
            Value |= Enumerant.OutputMemoryKHR;
            return this;
        }

        public static MemorySemantics CreateMakeAvailable()
        {
            return new MemorySemantics(Enumerant.MakeAvailable)
            {
            };
        }

        public MemorySemantics AlsoMakeAvailable()
        {
            Value |= Enumerant.MakeAvailable;
            return this;
        }

        public static MemorySemantics CreateMakeAvailableKHR()
        {
            return new MemorySemantics(Enumerant.MakeAvailableKHR)
            {
            };
        }

        public MemorySemantics AlsoMakeAvailableKHR()
        {
            Value |= Enumerant.MakeAvailableKHR;
            return this;
        }

        public static MemorySemantics CreateMakeVisible()
        {
            return new MemorySemantics(Enumerant.MakeVisible)
            {
            };
        }

        public MemorySemantics AlsoMakeVisible()
        {
            Value |= Enumerant.MakeVisible;
            return this;
        }

        public static MemorySemantics CreateMakeVisibleKHR()
        {
            return new MemorySemantics(Enumerant.MakeVisibleKHR)
            {
            };
        }

        public MemorySemantics AlsoMakeVisibleKHR()
        {
            Value |= Enumerant.MakeVisibleKHR;
            return this;
        }

        public static MemorySemantics CreateVolatile()
        {
            return new MemorySemantics(Enumerant.Volatile)
            {
            };
        }

        public MemorySemantics AlsoVolatile()
        {
            Value |= Enumerant.Volatile;
            return this;
        }


        public static implicit operator MemorySemantics(MemorySemantics.Enumerant value)
        {
            switch (value)
            {
                default:
                    return new MemorySemantics(value);
            }
        }

        public Enumerant Value { get; private set; }

        public static MemorySemantics Parse(WordReader reader, uint wordCount)
        {
            var end = reader.Position+wordCount;
            var id = (Enumerant) reader.ReadWord();
            var value = new MemorySemantics(id);
            value.PostParse(reader, wordCount - 1);
            return value;
        }

        public static MemorySemantics ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<MemorySemantics> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<MemorySemantics>();
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