using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class SamplerFilterMode : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Kernel)]
            Nearest = 0,
            [Capability(Capability.Enumerant.Kernel)]
            Linear = 1,
        }

        #region Nearest
        public static NearestImpl Nearest()
        {
            return NearestImpl.Instance;
            
        }

        public class NearestImpl: SamplerFilterMode
        {
            public static readonly NearestImpl Instance = new NearestImpl();
        
            private  NearestImpl()
            {
            }
            public override Enumerant Value => SamplerFilterMode.Enumerant.Nearest;
            public new static NearestImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NearestImpl object.</summary>
            /// <returns>A string that represents the NearestImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SamplerFilterMode.Nearest()";
            }
        }
        #endregion //Nearest

        #region Linear
        public static LinearImpl Linear()
        {
            return LinearImpl.Instance;
            
        }

        public class LinearImpl: SamplerFilterMode
        {
            public static readonly LinearImpl Instance = new LinearImpl();
        
            private  LinearImpl()
            {
            }
            public override Enumerant Value => SamplerFilterMode.Enumerant.Linear;
            public new static LinearImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LinearImpl object.</summary>
            /// <returns>A string that represents the LinearImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SamplerFilterMode.Linear()";
            }
        }
        #endregion //Linear

        public abstract Enumerant Value { get; }

        public static SamplerFilterMode Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Nearest :
                    return NearestImpl.Parse(reader, wordCount - 1);
                case Enumerant.Linear :
                    return LinearImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown SamplerFilterMode "+id);
            }
        }
        
        public static SamplerFilterMode ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<SamplerFilterMode> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<SamplerFilterMode>();
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