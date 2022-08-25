using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class RayQueryCandidateIntersectionType : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            RayQueryCandidateIntersectionTriangleKHR = 0,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            RayQueryCandidateIntersectionAABBKHR = 1,
        }

        #region RayQueryCandidateIntersectionTriangleKHR
        public static RayQueryCandidateIntersectionTriangleKHRImpl RayQueryCandidateIntersectionTriangleKHR()
        {
            return RayQueryCandidateIntersectionTriangleKHRImpl.Instance;
            
        }

        public class RayQueryCandidateIntersectionTriangleKHRImpl: RayQueryCandidateIntersectionType
        {
            public static readonly RayQueryCandidateIntersectionTriangleKHRImpl Instance = new RayQueryCandidateIntersectionTriangleKHRImpl();
        
            private  RayQueryCandidateIntersectionTriangleKHRImpl()
            {
            }
            public override Enumerant Value => RayQueryCandidateIntersectionType.Enumerant.RayQueryCandidateIntersectionTriangleKHR;
            public new static RayQueryCandidateIntersectionTriangleKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayQueryCandidateIntersectionTriangleKHRImpl object.</summary>
            /// <returns>A string that represents the RayQueryCandidateIntersectionTriangleKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" RayQueryCandidateIntersectionType.RayQueryCandidateIntersectionTriangleKHR()";
            }
        }
        #endregion //RayQueryCandidateIntersectionTriangleKHR

        #region RayQueryCandidateIntersectionAABBKHR
        public static RayQueryCandidateIntersectionAABBKHRImpl RayQueryCandidateIntersectionAABBKHR()
        {
            return RayQueryCandidateIntersectionAABBKHRImpl.Instance;
            
        }

        public class RayQueryCandidateIntersectionAABBKHRImpl: RayQueryCandidateIntersectionType
        {
            public static readonly RayQueryCandidateIntersectionAABBKHRImpl Instance = new RayQueryCandidateIntersectionAABBKHRImpl();
        
            private  RayQueryCandidateIntersectionAABBKHRImpl()
            {
            }
            public override Enumerant Value => RayQueryCandidateIntersectionType.Enumerant.RayQueryCandidateIntersectionAABBKHR;
            public new static RayQueryCandidateIntersectionAABBKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayQueryCandidateIntersectionAABBKHRImpl object.</summary>
            /// <returns>A string that represents the RayQueryCandidateIntersectionAABBKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" RayQueryCandidateIntersectionType.RayQueryCandidateIntersectionAABBKHR()";
            }
        }
        #endregion //RayQueryCandidateIntersectionAABBKHR

        public abstract Enumerant Value { get; }

        public static RayQueryCandidateIntersectionType Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.RayQueryCandidateIntersectionTriangleKHR :
                    return RayQueryCandidateIntersectionTriangleKHRImpl.Parse(reader, wordCount - 1);
                case Enumerant.RayQueryCandidateIntersectionAABBKHR :
                    return RayQueryCandidateIntersectionAABBKHRImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown RayQueryCandidateIntersectionType "+id);
            }
        }
        
        public static RayQueryCandidateIntersectionType ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<RayQueryCandidateIntersectionType> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<RayQueryCandidateIntersectionType>();
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