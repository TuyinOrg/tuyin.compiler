using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class AddressingModel : ValueEnum
    {
        public enum Enumerant
        {
            Logical = 0,
            [Capability(Capability.Enumerant.Addresses)]
            Physical32 = 1,
            [Capability(Capability.Enumerant.Addresses)]
            Physical64 = 2,
            [Capability(Capability.Enumerant.PhysicalStorageBufferAddresses)]
            PhysicalStorageBuffer64 = 5348,
            [Capability(Capability.Enumerant.PhysicalStorageBufferAddresses)]
            PhysicalStorageBuffer64EXT = 5348,
        }

        #region Logical
        public static LogicalImpl Logical()
        {
            return LogicalImpl.Instance;
            
        }

        internal class LogicalImpl: AddressingModel
        {
            public static readonly LogicalImpl Instance = new LogicalImpl();
        
            private  LogicalImpl()
            {
            }
            public override Enumerant Value => AddressingModel.Enumerant.Logical;
            public new static LogicalImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LogicalImpl object.</summary>
            /// <returns>A string that represents the LogicalImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" AddressingModel.Logical()";
            }
        }
        #endregion //Logical

        #region Physical32
        public static Physical32Impl Physical32()
        {
            return Physical32Impl.Instance;
            
        }

        internal class Physical32Impl: AddressingModel
        {
            public static readonly Physical32Impl Instance = new Physical32Impl();
        
            private  Physical32Impl()
            {
            }
            public override Enumerant Value => AddressingModel.Enumerant.Physical32;
            public new static Physical32Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Physical32Impl object.</summary>
            /// <returns>A string that represents the Physical32Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" AddressingModel.Physical32()";
            }
        }
        #endregion //Physical32

        #region Physical64
        public static Physical64Impl Physical64()
        {
            return Physical64Impl.Instance;
            
        }

        internal class Physical64Impl: AddressingModel
        {
            public static readonly Physical64Impl Instance = new Physical64Impl();
        
            private  Physical64Impl()
            {
            }
            public override Enumerant Value => AddressingModel.Enumerant.Physical64;
            public new static Physical64Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Physical64Impl object.</summary>
            /// <returns>A string that represents the Physical64Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" AddressingModel.Physical64()";
            }
        }
        #endregion //Physical64

        #region PhysicalStorageBuffer64
        public static PhysicalStorageBuffer64Impl PhysicalStorageBuffer64()
        {
            return PhysicalStorageBuffer64Impl.Instance;
            
        }

        internal class PhysicalStorageBuffer64Impl: AddressingModel
        {
            public static readonly PhysicalStorageBuffer64Impl Instance = new PhysicalStorageBuffer64Impl();
        
            private  PhysicalStorageBuffer64Impl()
            {
            }
            public override Enumerant Value => AddressingModel.Enumerant.PhysicalStorageBuffer64;
            public new static PhysicalStorageBuffer64Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PhysicalStorageBuffer64Impl object.</summary>
            /// <returns>A string that represents the PhysicalStorageBuffer64Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" AddressingModel.PhysicalStorageBuffer64()";
            }
        }
        #endregion //PhysicalStorageBuffer64

        #region PhysicalStorageBuffer64EXT
        public static PhysicalStorageBuffer64EXTImpl PhysicalStorageBuffer64EXT()
        {
            return PhysicalStorageBuffer64EXTImpl.Instance;
            
        }

        internal class PhysicalStorageBuffer64EXTImpl: AddressingModel
        {
            public static readonly PhysicalStorageBuffer64EXTImpl Instance = new PhysicalStorageBuffer64EXTImpl();
        
            private  PhysicalStorageBuffer64EXTImpl()
            {
            }
            public override Enumerant Value => AddressingModel.Enumerant.PhysicalStorageBuffer64EXT;
            public new static PhysicalStorageBuffer64EXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PhysicalStorageBuffer64EXTImpl object.</summary>
            /// <returns>A string that represents the PhysicalStorageBuffer64EXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" AddressingModel.PhysicalStorageBuffer64EXT()";
            }
        }
        #endregion //PhysicalStorageBuffer64EXT

        public abstract Enumerant Value { get; }

        public static AddressingModel Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Logical :
                    return LogicalImpl.Parse(reader, wordCount - 1);
                case Enumerant.Physical32 :
                    return Physical32Impl.Parse(reader, wordCount - 1);
                case Enumerant.Physical64 :
                    return Physical64Impl.Parse(reader, wordCount - 1);
                case Enumerant.PhysicalStorageBuffer64 :
                    return PhysicalStorageBuffer64Impl.Parse(reader, wordCount - 1);
                //PhysicalStorageBuffer64EXT has the same id as another value in enum.
                //case Enumerant.PhysicalStorageBuffer64EXT :
                //    return PhysicalStorageBuffer64EXT.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown AddressingModel "+id);
            }
        }
        
        public static AddressingModel ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<AddressingModel> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<AddressingModel>();
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