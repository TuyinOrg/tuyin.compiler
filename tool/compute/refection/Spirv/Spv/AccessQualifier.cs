using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class AccessQualifier : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Kernel)]
            ReadOnly = 0,
            [Capability(Capability.Enumerant.Kernel)]
            WriteOnly = 1,
            [Capability(Capability.Enumerant.Kernel)]
            ReadWrite = 2,
        }

        #region ReadOnly
        public static ReadOnlyImpl ReadOnly()
        {
            return ReadOnlyImpl.Instance;
            
        }

        internal class ReadOnlyImpl: AccessQualifier
        {
            public static readonly ReadOnlyImpl Instance = new ReadOnlyImpl();
        
            private  ReadOnlyImpl()
            {
            }
            public override Enumerant Value => AccessQualifier.Enumerant.ReadOnly;
            public new static ReadOnlyImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ReadOnlyImpl object.</summary>
            /// <returns>A string that represents the ReadOnlyImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" AccessQualifier.ReadOnly()";
            }
        }
        #endregion //ReadOnly

        #region WriteOnly
        public static WriteOnlyImpl WriteOnly()
        {
            return WriteOnlyImpl.Instance;
            
        }

        internal class WriteOnlyImpl: AccessQualifier
        {
            public static readonly WriteOnlyImpl Instance = new WriteOnlyImpl();
        
            private  WriteOnlyImpl()
            {
            }
            public override Enumerant Value => AccessQualifier.Enumerant.WriteOnly;
            public new static WriteOnlyImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WriteOnlyImpl object.</summary>
            /// <returns>A string that represents the WriteOnlyImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" AccessQualifier.WriteOnly()";
            }
        }
        #endregion //WriteOnly

        #region ReadWrite
        public static ReadWriteImpl ReadWrite()
        {
            return ReadWriteImpl.Instance;  
        }

        internal class ReadWriteImpl: AccessQualifier
        {
            public static readonly ReadWriteImpl Instance = new ReadWriteImpl();
        
            private  ReadWriteImpl()
            {
            }
            public override Enumerant Value => AccessQualifier.Enumerant.ReadWrite;
            public new static ReadWriteImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ReadWriteImpl object.</summary>
            /// <returns>A string that represents the ReadWriteImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" AccessQualifier.ReadWrite()";
            }
        }
        #endregion //ReadWrite

        public abstract Enumerant Value { get; }

        public static AccessQualifier Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.ReadOnly :
                    return ReadOnlyImpl.Parse(reader, wordCount - 1);
                case Enumerant.WriteOnly :
                    return WriteOnlyImpl.Parse(reader, wordCount - 1);
                case Enumerant.ReadWrite :
                    return ReadWriteImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown AccessQualifier "+id);
            }
        }
        
        public static AccessQualifier ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<AccessQualifier> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<AccessQualifier>();
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