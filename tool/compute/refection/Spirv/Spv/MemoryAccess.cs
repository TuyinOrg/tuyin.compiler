using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal partial class MemoryAccess : ValueEnum
    {
        [Flags]
        public enum Enumerant
        {
            None = 0x0000,
            Volatile = 0x0001,
            Aligned = 0x0002,
            Nontemporal = 0x0004,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            MakePointerAvailable = 0x0008,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            MakePointerAvailableKHR = 0x0008,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            MakePointerVisible = 0x0010,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            MakePointerVisibleKHR = 0x0010,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            NonPrivatePointer = 0x0020,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            NonPrivatePointerKHR = 0x0020,
        }

        public MemoryAccess(Enumerant value)
        {
            Value = value;
        }

        public static MemoryAccess CreateNone()
        {
            return new MemoryAccess(Enumerant.None)
            {
            };
        }

        public MemoryAccess AlsoNone()
        {
            Value |= Enumerant.None;
            return this;
        }

        public static MemoryAccess CreateVolatile()
        {
            return new MemoryAccess(Enumerant.Volatile)
            {
            };
        }

        public MemoryAccess AlsoVolatile()
        {
            Value |= Enumerant.Volatile;
            return this;
        }

        public static MemoryAccess CreateAligned(uint aligned)
        {
            return new MemoryAccess(Enumerant.Aligned)
            {
                Aligned = aligned,
            };
        }

        public MemoryAccess AlsoAligned(uint aligned)
        {
            Value |= Enumerant.Aligned;
            this.Aligned = aligned;
            return this;
        }

        public static MemoryAccess CreateNontemporal()
        {
            return new MemoryAccess(Enumerant.Nontemporal)
            {
            };
        }

        public MemoryAccess AlsoNontemporal()
        {
            Value |= Enumerant.Nontemporal;
            return this;
        }

        public static MemoryAccess CreateMakePointerAvailable(uint makePointerAvailable)
        {
            return new MemoryAccess(Enumerant.MakePointerAvailable)
            {
                MakePointerAvailable = makePointerAvailable,
            };
        }

        public MemoryAccess AlsoMakePointerAvailable(uint makePointerAvailable)
        {
            Value |= Enumerant.MakePointerAvailable;
            this.MakePointerAvailable = makePointerAvailable;
            return this;
        }

        public static MemoryAccess CreateMakePointerAvailableKHR(uint makePointerAvailableKHR)
        {
            return new MemoryAccess(Enumerant.MakePointerAvailableKHR)
            {
                MakePointerAvailableKHR = makePointerAvailableKHR,
            };
        }

        public MemoryAccess AlsoMakePointerAvailableKHR(uint makePointerAvailableKHR)
        {
            Value |= Enumerant.MakePointerAvailableKHR;
            this.MakePointerAvailableKHR = makePointerAvailableKHR;
            return this;
        }

        public static MemoryAccess CreateMakePointerVisible(uint makePointerVisible)
        {
            return new MemoryAccess(Enumerant.MakePointerVisible)
            {
                MakePointerVisible = makePointerVisible,
            };
        }

        public MemoryAccess AlsoMakePointerVisible(uint makePointerVisible)
        {
            Value |= Enumerant.MakePointerVisible;
            this.MakePointerVisible = makePointerVisible;
            return this;
        }

        public static MemoryAccess CreateMakePointerVisibleKHR(uint makePointerVisibleKHR)
        {
            return new MemoryAccess(Enumerant.MakePointerVisibleKHR)
            {
                MakePointerVisibleKHR = makePointerVisibleKHR,
            };
        }

        public MemoryAccess AlsoMakePointerVisibleKHR(uint makePointerVisibleKHR)
        {
            Value |= Enumerant.MakePointerVisibleKHR;
            this.MakePointerVisibleKHR = makePointerVisibleKHR;
            return this;
        }

        public static MemoryAccess CreateNonPrivatePointer()
        {
            return new MemoryAccess(Enumerant.NonPrivatePointer)
            {
            };
        }

        public MemoryAccess AlsoNonPrivatePointer()
        {
            Value |= Enumerant.NonPrivatePointer;
            return this;
        }

        public static MemoryAccess CreateNonPrivatePointerKHR()
        {
            return new MemoryAccess(Enumerant.NonPrivatePointerKHR)
            {
            };
        }

        public MemoryAccess AlsoNonPrivatePointerKHR()
        {
            Value |= Enumerant.NonPrivatePointerKHR;
            return this;
        }


        public static implicit operator MemoryAccess(MemoryAccess.Enumerant value)
        {
            switch (value)
            {
                case Enumerant.Aligned: throw new InvalidOperationException("Can't cast Aligned because it requires additional arguments.");
                case Enumerant.MakePointerAvailable: throw new InvalidOperationException("Can't cast MakePointerAvailable because it requires additional arguments.");
                case Enumerant.MakePointerVisible: throw new InvalidOperationException("Can't cast MakePointerVisible because it requires additional arguments.");
                default:
                    return new MemoryAccess(value);
            }
        }

        public Enumerant Value { get; private set; }

        public uint Aligned { get; set; }

        public uint MakePointerAvailable { get; set; }

        public uint MakePointerAvailableKHR { get; set; }

        public uint MakePointerVisible { get; set; }

        public uint MakePointerVisibleKHR { get; set; }

        public static MemoryAccess Parse(WordReader reader, uint wordCount)
        {
            var end = reader.Position+wordCount;
            var id = (Enumerant) reader.ReadWord();
            var value = new MemoryAccess(id);
            if (Enumerant.Aligned == (id & Enumerant.Aligned))
            {
                value.Aligned = Spv.LiteralInteger.Parse(reader, wordCount - 1);
            }
            if (Enumerant.MakePointerAvailable == (id & Enumerant.MakePointerAvailable))
            {
                value.MakePointerAvailable = Spv.IdScope.Parse(reader, wordCount - 1);
            }
            if (Enumerant.MakePointerAvailableKHR == (id & Enumerant.MakePointerAvailableKHR))
            {
                value.MakePointerAvailableKHR = Spv.IdScope.Parse(reader, wordCount - 1);
            }
            if (Enumerant.MakePointerVisible == (id & Enumerant.MakePointerVisible))
            {
                value.MakePointerVisible = Spv.IdScope.Parse(reader, wordCount - 1);
            }
            if (Enumerant.MakePointerVisibleKHR == (id & Enumerant.MakePointerVisibleKHR))
            {
                value.MakePointerVisibleKHR = Spv.IdScope.Parse(reader, wordCount - 1);
            }
            value.PostParse(reader, wordCount - 1);
            return value;
        }

        public static MemoryAccess ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<MemoryAccess> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<MemoryAccess>();
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
            if (Enumerant.Aligned == (Value & Enumerant.Aligned))
            {
                wordCount += Aligned.GetWordCount();
            }
            if (Enumerant.MakePointerAvailable == (Value & Enumerant.MakePointerAvailable))
            {
                wordCount += MakePointerAvailable.GetWordCount();
            }
            if (Enumerant.MakePointerAvailableKHR == (Value & Enumerant.MakePointerAvailableKHR))
            {
                wordCount += MakePointerAvailableKHR.GetWordCount();
            }
            if (Enumerant.MakePointerVisible == (Value & Enumerant.MakePointerVisible))
            {
                wordCount += MakePointerVisible.GetWordCount();
            }
            if (Enumerant.MakePointerVisibleKHR == (Value & Enumerant.MakePointerVisibleKHR))
            {
                wordCount += MakePointerVisibleKHR.GetWordCount();
            }
            return wordCount;
        }

        public void Write(WordWriter writer)
        {
             writer.WriteWord((uint)Value);
            if (Enumerant.Aligned == (Value & Enumerant.Aligned))
            {
                Aligned.Write(writer);
            }
            if (Enumerant.MakePointerAvailable == (Value & Enumerant.MakePointerAvailable))
            {
                MakePointerAvailable.Write(writer);
            }
            if (Enumerant.MakePointerAvailableKHR == (Value & Enumerant.MakePointerAvailableKHR))
            {
                MakePointerAvailableKHR.Write(writer);
            }
            if (Enumerant.MakePointerVisible == (Value & Enumerant.MakePointerVisible))
            {
                MakePointerVisible.Write(writer);
            }
            if (Enumerant.MakePointerVisibleKHR == (Value & Enumerant.MakePointerVisibleKHR))
            {
                MakePointerVisibleKHR.Write(writer);
            }
        }
    }
}