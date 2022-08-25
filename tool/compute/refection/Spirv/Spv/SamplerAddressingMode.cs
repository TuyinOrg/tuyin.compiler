using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class SamplerAddressingMode : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Kernel)]
            None = 0,
            [Capability(Capability.Enumerant.Kernel)]
            ClampToEdge = 1,
            [Capability(Capability.Enumerant.Kernel)]
            Clamp = 2,
            [Capability(Capability.Enumerant.Kernel)]
            Repeat = 3,
            [Capability(Capability.Enumerant.Kernel)]
            RepeatMirrored = 4,
        }

        #region None
        public static NoneImpl None()
        {
            return NoneImpl.Instance;
            
        }

        public class NoneImpl: SamplerAddressingMode
        {
            public static readonly NoneImpl Instance = new NoneImpl();
        
            private  NoneImpl()
            {
            }
            public override Enumerant Value => SamplerAddressingMode.Enumerant.None;
            public new static NoneImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NoneImpl object.</summary>
            /// <returns>A string that represents the NoneImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SamplerAddressingMode.None()";
            }
        }
        #endregion //None

        #region ClampToEdge
        public static ClampToEdgeImpl ClampToEdge()
        {
            return ClampToEdgeImpl.Instance;
            
        }

        public class ClampToEdgeImpl: SamplerAddressingMode
        {
            public static readonly ClampToEdgeImpl Instance = new ClampToEdgeImpl();
        
            private  ClampToEdgeImpl()
            {
            }
            public override Enumerant Value => SamplerAddressingMode.Enumerant.ClampToEdge;
            public new static ClampToEdgeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ClampToEdgeImpl object.</summary>
            /// <returns>A string that represents the ClampToEdgeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SamplerAddressingMode.ClampToEdge()";
            }
        }
        #endregion //ClampToEdge

        #region Clamp
        public static ClampImpl Clamp()
        {
            return ClampImpl.Instance;
            
        }

        public class ClampImpl: SamplerAddressingMode
        {
            public static readonly ClampImpl Instance = new ClampImpl();
        
            private  ClampImpl()
            {
            }
            public override Enumerant Value => SamplerAddressingMode.Enumerant.Clamp;
            public new static ClampImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ClampImpl object.</summary>
            /// <returns>A string that represents the ClampImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SamplerAddressingMode.Clamp()";
            }
        }
        #endregion //Clamp

        #region Repeat
        public static RepeatImpl Repeat()
        {
            return RepeatImpl.Instance;
            
        }

        public class RepeatImpl: SamplerAddressingMode
        {
            public static readonly RepeatImpl Instance = new RepeatImpl();
        
            private  RepeatImpl()
            {
            }
            public override Enumerant Value => SamplerAddressingMode.Enumerant.Repeat;
            public new static RepeatImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RepeatImpl object.</summary>
            /// <returns>A string that represents the RepeatImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SamplerAddressingMode.Repeat()";
            }
        }
        #endregion //Repeat

        #region RepeatMirrored
        public static RepeatMirroredImpl RepeatMirrored()
        {
            return RepeatMirroredImpl.Instance;
            
        }

        public class RepeatMirroredImpl: SamplerAddressingMode
        {
            public static readonly RepeatMirroredImpl Instance = new RepeatMirroredImpl();
        
            private  RepeatMirroredImpl()
            {
            }
            public override Enumerant Value => SamplerAddressingMode.Enumerant.RepeatMirrored;
            public new static RepeatMirroredImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RepeatMirroredImpl object.</summary>
            /// <returns>A string that represents the RepeatMirroredImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SamplerAddressingMode.RepeatMirrored()";
            }
        }
        #endregion //RepeatMirrored

        public abstract Enumerant Value { get; }

        public static SamplerAddressingMode Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.None :
                    return NoneImpl.Parse(reader, wordCount - 1);
                case Enumerant.ClampToEdge :
                    return ClampToEdgeImpl.Parse(reader, wordCount - 1);
                case Enumerant.Clamp :
                    return ClampImpl.Parse(reader, wordCount - 1);
                case Enumerant.Repeat :
                    return RepeatImpl.Parse(reader, wordCount - 1);
                case Enumerant.RepeatMirrored :
                    return RepeatMirroredImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown SamplerAddressingMode "+id);
            }
        }
        
        public static SamplerAddressingMode ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<SamplerAddressingMode> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<SamplerAddressingMode>();
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
            return 1;
        }

        public virtual void Write(WordWriter writer)
        {
            writer.WriteWord((uint)Value);
        }
    }
}