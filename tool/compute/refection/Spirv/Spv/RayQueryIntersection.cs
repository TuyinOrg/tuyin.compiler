using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class RayQueryIntersection : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            RayQueryCandidateIntersectionKHR = 0,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            RayQueryCommittedIntersectionKHR = 1,
        }

        #region RayQueryCandidateIntersectionKHR
        public static RayQueryCandidateIntersectionKHRImpl RayQueryCandidateIntersectionKHR()
        {
            return RayQueryCandidateIntersectionKHRImpl.Instance;
            
        }

        public class RayQueryCandidateIntersectionKHRImpl: RayQueryIntersection
        {
            public static readonly RayQueryCandidateIntersectionKHRImpl Instance = new RayQueryCandidateIntersectionKHRImpl();
        
            private  RayQueryCandidateIntersectionKHRImpl()
            {
            }
            public override Enumerant Value => RayQueryIntersection.Enumerant.RayQueryCandidateIntersectionKHR;
            public new static RayQueryCandidateIntersectionKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayQueryCandidateIntersectionKHRImpl object.</summary>
            /// <returns>A string that represents the RayQueryCandidateIntersectionKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" RayQueryIntersection.RayQueryCandidateIntersectionKHR()";
            }
        }
        #endregion //RayQueryCandidateIntersectionKHR

        #region RayQueryCommittedIntersectionKHR
        public static RayQueryCommittedIntersectionKHRImpl RayQueryCommittedIntersectionKHR()
        {
            return RayQueryCommittedIntersectionKHRImpl.Instance;
            
        }

        public class RayQueryCommittedIntersectionKHRImpl: RayQueryIntersection
        {
            public static readonly RayQueryCommittedIntersectionKHRImpl Instance = new RayQueryCommittedIntersectionKHRImpl();
        
            private  RayQueryCommittedIntersectionKHRImpl()
            {
            }
            public override Enumerant Value => RayQueryIntersection.Enumerant.RayQueryCommittedIntersectionKHR;
            public new static RayQueryCommittedIntersectionKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayQueryCommittedIntersectionKHRImpl object.</summary>
            /// <returns>A string that represents the RayQueryCommittedIntersectionKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" RayQueryIntersection.RayQueryCommittedIntersectionKHR()";
            }
        }
        #endregion //RayQueryCommittedIntersectionKHR

        public abstract Enumerant Value { get; }

        public static RayQueryIntersection Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.RayQueryCandidateIntersectionKHR :
                    return RayQueryCandidateIntersectionKHRImpl.Parse(reader, wordCount - 1);
                case Enumerant.RayQueryCommittedIntersectionKHR :
                    return RayQueryCommittedIntersectionKHRImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown RayQueryIntersection "+id);
            }
        }
        
        public static RayQueryIntersection ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<RayQueryIntersection> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<RayQueryIntersection>();
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