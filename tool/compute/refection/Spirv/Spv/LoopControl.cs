using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal partial class LoopControl : ValueEnum
    {
        [Flags]
        public enum Enumerant
        {
            None = 0x0000,
            Unroll = 0x0001,
            DontUnroll = 0x0002,
            DependencyInfinite = 0x0004,
            DependencyLength = 0x0008,
            MinIterations = 0x0010,
            MaxIterations = 0x0020,
            IterationMultiple = 0x0040,
            PeelCount = 0x0080,
            PartialCount = 0x0100,
        }

        public LoopControl(Enumerant value)
        {
            Value = value;
        }

        public static LoopControl CreateNone()
        {
            return new LoopControl(Enumerant.None)
            {
            };
        }

        public LoopControl AlsoNone()
        {
            Value |= Enumerant.None;
            return this;
        }

        public static LoopControl CreateUnroll()
        {
            return new LoopControl(Enumerant.Unroll)
            {
            };
        }

        public LoopControl AlsoUnroll()
        {
            Value |= Enumerant.Unroll;
            return this;
        }

        public static LoopControl CreateDontUnroll()
        {
            return new LoopControl(Enumerant.DontUnroll)
            {
            };
        }

        public LoopControl AlsoDontUnroll()
        {
            Value |= Enumerant.DontUnroll;
            return this;
        }

        public static LoopControl CreateDependencyInfinite()
        {
            return new LoopControl(Enumerant.DependencyInfinite)
            {
            };
        }

        public LoopControl AlsoDependencyInfinite()
        {
            Value |= Enumerant.DependencyInfinite;
            return this;
        }

        public static LoopControl CreateDependencyLength(uint dependencyLength)
        {
            return new LoopControl(Enumerant.DependencyLength)
            {
                DependencyLength = dependencyLength,
            };
        }

        public LoopControl AlsoDependencyLength(uint dependencyLength)
        {
            Value |= Enumerant.DependencyLength;
            this.DependencyLength = dependencyLength;
            return this;
        }

        public static LoopControl CreateMinIterations(uint minIterations)
        {
            return new LoopControl(Enumerant.MinIterations)
            {
                MinIterations = minIterations,
            };
        }

        public LoopControl AlsoMinIterations(uint minIterations)
        {
            Value |= Enumerant.MinIterations;
            this.MinIterations = minIterations;
            return this;
        }

        public static LoopControl CreateMaxIterations(uint maxIterations)
        {
            return new LoopControl(Enumerant.MaxIterations)
            {
                MaxIterations = maxIterations,
            };
        }

        public LoopControl AlsoMaxIterations(uint maxIterations)
        {
            Value |= Enumerant.MaxIterations;
            this.MaxIterations = maxIterations;
            return this;
        }

        public static LoopControl CreateIterationMultiple(uint iterationMultiple)
        {
            return new LoopControl(Enumerant.IterationMultiple)
            {
                IterationMultiple = iterationMultiple,
            };
        }

        public LoopControl AlsoIterationMultiple(uint iterationMultiple)
        {
            Value |= Enumerant.IterationMultiple;
            this.IterationMultiple = iterationMultiple;
            return this;
        }

        public static LoopControl CreatePeelCount(uint peelCount)
        {
            return new LoopControl(Enumerant.PeelCount)
            {
                PeelCount = peelCount,
            };
        }

        public LoopControl AlsoPeelCount(uint peelCount)
        {
            Value |= Enumerant.PeelCount;
            this.PeelCount = peelCount;
            return this;
        }

        public static LoopControl CreatePartialCount(uint partialCount)
        {
            return new LoopControl(Enumerant.PartialCount)
            {
                PartialCount = partialCount,
            };
        }

        public LoopControl AlsoPartialCount(uint partialCount)
        {
            Value |= Enumerant.PartialCount;
            this.PartialCount = partialCount;
            return this;
        }


        public static implicit operator LoopControl(LoopControl.Enumerant value)
        {
            switch (value)
            {
                case Enumerant.DependencyLength: throw new InvalidOperationException("Can't cast DependencyLength because it requires additional arguments.");
                case Enumerant.MinIterations: throw new InvalidOperationException("Can't cast MinIterations because it requires additional arguments.");
                case Enumerant.MaxIterations: throw new InvalidOperationException("Can't cast MaxIterations because it requires additional arguments.");
                case Enumerant.IterationMultiple: throw new InvalidOperationException("Can't cast IterationMultiple because it requires additional arguments.");
                case Enumerant.PeelCount: throw new InvalidOperationException("Can't cast PeelCount because it requires additional arguments.");
                case Enumerant.PartialCount: throw new InvalidOperationException("Can't cast PartialCount because it requires additional arguments.");
                default:
                    return new LoopControl(value);
            }
        }

        public Enumerant Value { get; private set; }

        public uint DependencyLength { get; set; }

        public uint MinIterations { get; set; }

        public uint MaxIterations { get; set; }

        public uint IterationMultiple { get; set; }

        public uint PeelCount { get; set; }

        public uint PartialCount { get; set; }

        public static LoopControl Parse(WordReader reader, uint wordCount)
        {
            var end = reader.Position+wordCount;
            var id = (Enumerant) reader.ReadWord();
            var value = new LoopControl(id);
            if (Enumerant.DependencyLength == (id & Enumerant.DependencyLength))
            {
                value.DependencyLength = Spv.LiteralInteger.Parse(reader, wordCount - 1);
            }
            if (Enumerant.MinIterations == (id & Enumerant.MinIterations))
            {
                value.MinIterations = Spv.LiteralInteger.Parse(reader, wordCount - 1);
            }
            if (Enumerant.MaxIterations == (id & Enumerant.MaxIterations))
            {
                value.MaxIterations = Spv.LiteralInteger.Parse(reader, wordCount - 1);
            }
            if (Enumerant.IterationMultiple == (id & Enumerant.IterationMultiple))
            {
                value.IterationMultiple = Spv.LiteralInteger.Parse(reader, wordCount - 1);
            }
            if (Enumerant.PeelCount == (id & Enumerant.PeelCount))
            {
                value.PeelCount = Spv.LiteralInteger.Parse(reader, wordCount - 1);
            }
            if (Enumerant.PartialCount == (id & Enumerant.PartialCount))
            {
                value.PartialCount = Spv.LiteralInteger.Parse(reader, wordCount - 1);
            }
            value.PostParse(reader, wordCount - 1);
            return value;
        }

        public static LoopControl ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<LoopControl> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<LoopControl>();
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
            if (Enumerant.DependencyLength == (Value & Enumerant.DependencyLength))
            {
                wordCount += DependencyLength.GetWordCount();
            }
            if (Enumerant.MinIterations == (Value & Enumerant.MinIterations))
            {
                wordCount += MinIterations.GetWordCount();
            }
            if (Enumerant.MaxIterations == (Value & Enumerant.MaxIterations))
            {
                wordCount += MaxIterations.GetWordCount();
            }
            if (Enumerant.IterationMultiple == (Value & Enumerant.IterationMultiple))
            {
                wordCount += IterationMultiple.GetWordCount();
            }
            if (Enumerant.PeelCount == (Value & Enumerant.PeelCount))
            {
                wordCount += PeelCount.GetWordCount();
            }
            if (Enumerant.PartialCount == (Value & Enumerant.PartialCount))
            {
                wordCount += PartialCount.GetWordCount();
            }
            return wordCount;
        }

        public void Write(WordWriter writer)
        {
             writer.WriteWord((uint)Value);
            if (Enumerant.DependencyLength == (Value & Enumerant.DependencyLength))
            {
                DependencyLength.Write(writer);
            }
            if (Enumerant.MinIterations == (Value & Enumerant.MinIterations))
            {
                MinIterations.Write(writer);
            }
            if (Enumerant.MaxIterations == (Value & Enumerant.MaxIterations))
            {
                MaxIterations.Write(writer);
            }
            if (Enumerant.IterationMultiple == (Value & Enumerant.IterationMultiple))
            {
                IterationMultiple.Write(writer);
            }
            if (Enumerant.PeelCount == (Value & Enumerant.PeelCount))
            {
                PeelCount.Write(writer);
            }
            if (Enumerant.PartialCount == (Value & Enumerant.PartialCount))
            {
                PartialCount.Write(writer);
            }
        }
    }
}