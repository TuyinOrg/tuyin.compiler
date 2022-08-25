using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class GroupOperation : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Kernel)]
            [Capability(Capability.Enumerant.GroupNonUniformArithmetic)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            Reduce = 0,
            [Capability(Capability.Enumerant.Kernel)]
            [Capability(Capability.Enumerant.GroupNonUniformArithmetic)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            InclusiveScan = 1,
            [Capability(Capability.Enumerant.Kernel)]
            [Capability(Capability.Enumerant.GroupNonUniformArithmetic)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            ExclusiveScan = 2,
            [Capability(Capability.Enumerant.GroupNonUniformClustered)]
            ClusteredReduce = 3,
            [Capability(Capability.Enumerant.GroupNonUniformPartitionedNV)]
            PartitionedReduceNV = 6,
            [Capability(Capability.Enumerant.GroupNonUniformPartitionedNV)]
            PartitionedInclusiveScanNV = 7,
            [Capability(Capability.Enumerant.GroupNonUniformPartitionedNV)]
            PartitionedExclusiveScanNV = 8,
        }

        #region Reduce
        public static ReduceImpl Reduce()
        {
            return ReduceImpl.Instance;
            
        }

        internal class ReduceImpl: GroupOperation
        {
            public static readonly ReduceImpl Instance = new ReduceImpl();
        
            private  ReduceImpl()
            {
            }
            public override Enumerant Value => GroupOperation.Enumerant.Reduce;
            public new static ReduceImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ReduceImpl object.</summary>
            /// <returns>A string that represents the ReduceImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" GroupOperation.Reduce()";
            }
        }
        #endregion //Reduce

        #region InclusiveScan
        public static InclusiveScanImpl InclusiveScan()
        {
            return InclusiveScanImpl.Instance;
            
        }

        internal class InclusiveScanImpl: GroupOperation
        {
            public static readonly InclusiveScanImpl Instance = new InclusiveScanImpl();
        
            private  InclusiveScanImpl()
            {
            }
            public override Enumerant Value => GroupOperation.Enumerant.InclusiveScan;
            public new static InclusiveScanImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InclusiveScanImpl object.</summary>
            /// <returns>A string that represents the InclusiveScanImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" GroupOperation.InclusiveScan()";
            }
        }
        #endregion //InclusiveScan

        #region ExclusiveScan
        public static ExclusiveScanImpl ExclusiveScan()
        {
            return ExclusiveScanImpl.Instance;
            
        }

        internal class ExclusiveScanImpl: GroupOperation
        {
            public static readonly ExclusiveScanImpl Instance = new ExclusiveScanImpl();
        
            private  ExclusiveScanImpl()
            {
            }
            public override Enumerant Value => GroupOperation.Enumerant.ExclusiveScan;
            public new static ExclusiveScanImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ExclusiveScanImpl object.</summary>
            /// <returns>A string that represents the ExclusiveScanImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" GroupOperation.ExclusiveScan()";
            }
        }
        #endregion //ExclusiveScan

        #region ClusteredReduce
        public static ClusteredReduceImpl ClusteredReduce()
        {
            return ClusteredReduceImpl.Instance;
            
        }

        internal class ClusteredReduceImpl: GroupOperation
        {
            public static readonly ClusteredReduceImpl Instance = new ClusteredReduceImpl();
        
            private  ClusteredReduceImpl()
            {
            }
            public override Enumerant Value => GroupOperation.Enumerant.ClusteredReduce;
            public new static ClusteredReduceImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ClusteredReduceImpl object.</summary>
            /// <returns>A string that represents the ClusteredReduceImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" GroupOperation.ClusteredReduce()";
            }
        }
        #endregion //ClusteredReduce

        #region PartitionedReduceNV
        public static PartitionedReduceNVImpl PartitionedReduceNV()
        {
            return PartitionedReduceNVImpl.Instance;
            
        }

        internal class PartitionedReduceNVImpl: GroupOperation
        {
            public static readonly PartitionedReduceNVImpl Instance = new PartitionedReduceNVImpl();
        
            private  PartitionedReduceNVImpl()
            {
            }
            public override Enumerant Value => GroupOperation.Enumerant.PartitionedReduceNV;
            public new static PartitionedReduceNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PartitionedReduceNVImpl object.</summary>
            /// <returns>A string that represents the PartitionedReduceNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" GroupOperation.PartitionedReduceNV()";
            }
        }
        #endregion //PartitionedReduceNV

        #region PartitionedInclusiveScanNV
        public static PartitionedInclusiveScanNVImpl PartitionedInclusiveScanNV()
        {
            return PartitionedInclusiveScanNVImpl.Instance;
            
        }

        internal class PartitionedInclusiveScanNVImpl: GroupOperation
        {
            public static readonly PartitionedInclusiveScanNVImpl Instance = new PartitionedInclusiveScanNVImpl();
        
            private  PartitionedInclusiveScanNVImpl()
            {
            }
            public override Enumerant Value => GroupOperation.Enumerant.PartitionedInclusiveScanNV;
            public new static PartitionedInclusiveScanNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PartitionedInclusiveScanNVImpl object.</summary>
            /// <returns>A string that represents the PartitionedInclusiveScanNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" GroupOperation.PartitionedInclusiveScanNV()";
            }
        }
        #endregion //PartitionedInclusiveScanNV

        #region PartitionedExclusiveScanNV
        public static PartitionedExclusiveScanNVImpl PartitionedExclusiveScanNV()
        {
            return PartitionedExclusiveScanNVImpl.Instance;
            
        }

        internal class PartitionedExclusiveScanNVImpl: GroupOperation
        {
            public static readonly PartitionedExclusiveScanNVImpl Instance = new PartitionedExclusiveScanNVImpl();
        
            private  PartitionedExclusiveScanNVImpl()
            {
            }
            public override Enumerant Value => GroupOperation.Enumerant.PartitionedExclusiveScanNV;
            public new static PartitionedExclusiveScanNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PartitionedExclusiveScanNVImpl object.</summary>
            /// <returns>A string that represents the PartitionedExclusiveScanNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" GroupOperation.PartitionedExclusiveScanNV()";
            }
        }
        #endregion //PartitionedExclusiveScanNV

        public abstract Enumerant Value { get; }

        public static GroupOperation Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Reduce :
                    return ReduceImpl.Parse(reader, wordCount - 1);
                case Enumerant.InclusiveScan :
                    return InclusiveScanImpl.Parse(reader, wordCount - 1);
                case Enumerant.ExclusiveScan :
                    return ExclusiveScanImpl.Parse(reader, wordCount - 1);
                case Enumerant.ClusteredReduce :
                    return ClusteredReduceImpl.Parse(reader, wordCount - 1);
                case Enumerant.PartitionedReduceNV :
                    return PartitionedReduceNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.PartitionedInclusiveScanNV :
                    return PartitionedInclusiveScanNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.PartitionedExclusiveScanNV :
                    return PartitionedExclusiveScanNVImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown GroupOperation "+id);
            }
        }
        
        public static GroupOperation ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<GroupOperation> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<GroupOperation>();
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