using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class LinkageType : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Linkage)]
            Export = 0,
            [Capability(Capability.Enumerant.Linkage)]
            Import = 1,
        }

        #region Export
        public static ExportImpl Export()
        {
            return ExportImpl.Instance;
            
        }

        internal class ExportImpl: LinkageType
        {
            public static readonly ExportImpl Instance = new ExportImpl();
        
            private  ExportImpl()
            {
            }
            public override Enumerant Value => LinkageType.Enumerant.Export;
            public new static ExportImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ExportImpl object.</summary>
            /// <returns>A string that represents the ExportImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" LinkageType.Export()";
            }
        }
        #endregion //Export

        #region Import
        public static ImportImpl Import()
        {
            return ImportImpl.Instance;
            
        }

        internal class ImportImpl: LinkageType
        {
            public static readonly ImportImpl Instance = new ImportImpl();
        
            private  ImportImpl()
            {
            }
            public override Enumerant Value => LinkageType.Enumerant.Import;
            public new static ImportImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImportImpl object.</summary>
            /// <returns>A string that represents the ImportImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" LinkageType.Import()";
            }
        }
        #endregion //Import

        public abstract Enumerant Value { get; }

        public static LinkageType Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Export :
                    return ExportImpl.Parse(reader, wordCount - 1);
                case Enumerant.Import :
                    return ImportImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown LinkageType "+id);
            }
        }
        
        public static LinkageType ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<LinkageType> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<LinkageType>();
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