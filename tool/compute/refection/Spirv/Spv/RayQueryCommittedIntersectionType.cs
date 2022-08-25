using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class RayQueryCommittedIntersectionType : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            RayQueryCommittedIntersectionNoneKHR = 0,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            RayQueryCommittedIntersectionTriangleKHR = 1,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            RayQueryCommittedIntersectionGeneratedKHR = 2,
        }

        #region RayQueryCommittedIntersectionNoneKHR
        public static RayQueryCommittedIntersectionNoneKHRImpl RayQueryCommittedIntersectionNoneKHR()
        {
            return RayQueryCommittedIntersectionNoneKHRImpl.Instance;
            
        }

        public class RayQueryCommittedIntersectionNoneKHRImpl: RayQueryCommittedIntersectionType
        {
            public static readonly RayQueryCommittedIntersectionNoneKHRImpl Instance = new RayQueryCommittedIntersectionNoneKHRImpl();
        
            private  RayQueryCommittedIntersectionNoneKHRImpl()
            {
            }
            public override Enumerant Value => RayQueryCommittedIntersectionType.Enumerant.RayQueryCommittedIntersectionNoneKHR;
            public new static RayQueryCommittedIntersectionNoneKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayQueryCommittedIntersectionNoneKHRImpl object.</summary>
            /// <returns>A string that represents the RayQueryCommittedIntersectionNoneKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" RayQueryCommittedIntersectionType.RayQueryCommittedIntersectionNoneKHR()";
            }
        }
        #endregion //RayQueryCommittedIntersectionNoneKHR

        #region RayQueryCommittedIntersectionTriangleKHR
        public static RayQueryCommittedIntersectionTriangleKHRImpl RayQueryCommittedIntersectionTriangleKHR()
        {
            return RayQueryCommittedIntersectionTriangleKHRImpl.Instance;
            
        }

        public class RayQueryCommittedIntersectionTriangleKHRImpl: RayQueryCommittedIntersectionType
        {
            public static readonly RayQueryCommittedIntersectionTriangleKHRImpl Instance = new RayQueryCommittedIntersectionTriangleKHRImpl();
        
            private  RayQueryCommittedIntersectionTriangleKHRImpl()
            {
            }
            public override Enumerant Value => RayQueryCommittedIntersectionType.Enumerant.RayQueryCommittedIntersectionTriangleKHR;
            public new static RayQueryCommittedIntersectionTriangleKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayQueryCommittedIntersectionTriangleKHRImpl object.</summary>
            /// <returns>A string that represents the RayQueryCommittedIntersectionTriangleKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" RayQueryCommittedIntersectionType.RayQueryCommittedIntersectionTriangleKHR()";
            }
        }
        #endregion //RayQueryCommittedIntersectionTriangleKHR

        #region RayQueryCommittedIntersectionGeneratedKHR
        public static RayQueryCommittedIntersectionGeneratedKHRImpl RayQueryCommittedIntersectionGeneratedKHR()
        {
            return RayQueryCommittedIntersectionGeneratedKHRImpl.Instance;
            
        }

        public class RayQueryCommittedIntersectionGeneratedKHRImpl: RayQueryCommittedIntersectionType
        {
            public static readonly RayQueryCommittedIntersectionGeneratedKHRImpl Instance = new RayQueryCommittedIntersectionGeneratedKHRImpl();
        
            private  RayQueryCommittedIntersectionGeneratedKHRImpl()
            {
            }
            public override Enumerant Value => RayQueryCommittedIntersectionType.Enumerant.RayQueryCommittedIntersectionGeneratedKHR;
            public new static RayQueryCommittedIntersectionGeneratedKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayQueryCommittedIntersectionGeneratedKHRImpl object.</summary>
            /// <returns>A string that represents the RayQueryCommittedIntersectionGeneratedKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" RayQueryCommittedIntersectionType.RayQueryCommittedIntersectionGeneratedKHR()";
            }
        }
        #endregion //RayQueryCommittedIntersectionGeneratedKHR

        public abstract Enumerant Value { get; }

        public static RayQueryCommittedIntersectionType Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.RayQueryCommittedIntersectionNoneKHR :
                    return RayQueryCommittedIntersectionNoneKHRImpl.Parse(reader, wordCount - 1);
                case Enumerant.RayQueryCommittedIntersectionTriangleKHR :
                    return RayQueryCommittedIntersectionTriangleKHRImpl.Parse(reader, wordCount - 1);
                case Enumerant.RayQueryCommittedIntersectionGeneratedKHR :
                    return RayQueryCommittedIntersectionGeneratedKHRImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown RayQueryCommittedIntersectionType "+id);
            }
        }
        
        public static RayQueryCommittedIntersectionType ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<RayQueryCommittedIntersectionType> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<RayQueryCommittedIntersectionType>();
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