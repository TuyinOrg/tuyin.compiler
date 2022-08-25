using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class FPRoundingMode : ValueEnum
    {
        public enum Enumerant
        {
            RTE = 0,
            RTZ = 1,
            RTP = 2,
            RTN = 3,
        }

        #region RTE
        public static RTEImpl RTE()
        {
            return RTEImpl.Instance;
            
        }

        internal class RTEImpl: FPRoundingMode
        {
            public static readonly RTEImpl Instance = new RTEImpl();
        
            private  RTEImpl()
            {
            }
            public override Enumerant Value => FPRoundingMode.Enumerant.RTE;
            public new static RTEImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RTEImpl object.</summary>
            /// <returns>A string that represents the RTEImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" FPRoundingMode.RTE()";
            }
        }
        #endregion //RTE

        #region RTZ
        public static RTZImpl RTZ()
        {
            return RTZImpl.Instance;
            
        }

        internal class RTZImpl: FPRoundingMode
        {
            public static readonly RTZImpl Instance = new RTZImpl();
        
            private  RTZImpl()
            {
            }
            public override Enumerant Value => FPRoundingMode.Enumerant.RTZ;
            public new static RTZImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RTZImpl object.</summary>
            /// <returns>A string that represents the RTZImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" FPRoundingMode.RTZ()";
            }
        }
        #endregion //RTZ

        #region RTP
        public static RTPImpl RTP()
        {
            return RTPImpl.Instance;
            
        }

        internal class RTPImpl: FPRoundingMode
        {
            public static readonly RTPImpl Instance = new RTPImpl();
        
            private  RTPImpl()
            {
            }
            public override Enumerant Value => FPRoundingMode.Enumerant.RTP;
            public new static RTPImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RTPImpl object.</summary>
            /// <returns>A string that represents the RTPImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" FPRoundingMode.RTP()";
            }
        }
        #endregion //RTP

        #region RTN
        public static RTNImpl RTN()
        {
            return RTNImpl.Instance;
            
        }

        internal class RTNImpl: FPRoundingMode
        {
            public static readonly RTNImpl Instance = new RTNImpl();
        
            private  RTNImpl()
            {
            }
            public override Enumerant Value => FPRoundingMode.Enumerant.RTN;
            public new static RTNImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RTNImpl object.</summary>
            /// <returns>A string that represents the RTNImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" FPRoundingMode.RTN()";
            }
        }
        #endregion //RTN

        public abstract Enumerant Value { get; }

        public static FPRoundingMode Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.RTE :
                    return RTEImpl.Parse(reader, wordCount - 1);
                case Enumerant.RTZ :
                    return RTZImpl.Parse(reader, wordCount - 1);
                case Enumerant.RTP :
                    return RTPImpl.Parse(reader, wordCount - 1);
                case Enumerant.RTN :
                    return RTNImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown FPRoundingMode "+id);
            }
        }
        
        public static FPRoundingMode ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<FPRoundingMode> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<FPRoundingMode>();
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