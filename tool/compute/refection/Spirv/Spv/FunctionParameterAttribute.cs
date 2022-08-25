using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class FunctionParameterAttribute : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Kernel)]
            Zext = 0,
            [Capability(Capability.Enumerant.Kernel)]
            Sext = 1,
            [Capability(Capability.Enumerant.Kernel)]
            ByVal = 2,
            [Capability(Capability.Enumerant.Kernel)]
            Sret = 3,
            [Capability(Capability.Enumerant.Kernel)]
            NoAlias = 4,
            [Capability(Capability.Enumerant.Kernel)]
            NoCapture = 5,
            [Capability(Capability.Enumerant.Kernel)]
            NoWrite = 6,
            [Capability(Capability.Enumerant.Kernel)]
            NoReadWrite = 7,
        }

        #region Zext
        public static ZextImpl Zext()
        {
            return ZextImpl.Instance;
            
        }

        internal class ZextImpl: FunctionParameterAttribute
        {
            public static readonly ZextImpl Instance = new ZextImpl();
        
            private  ZextImpl()
            {
            }
            public override Enumerant Value => FunctionParameterAttribute.Enumerant.Zext;
            public new static ZextImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ZextImpl object.</summary>
            /// <returns>A string that represents the ZextImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" FunctionParameterAttribute.Zext()";
            }
        }
        #endregion //Zext

        #region Sext
        public static SextImpl Sext()
        {
            return SextImpl.Instance;
            
        }

        internal class SextImpl: FunctionParameterAttribute
        {
            public static readonly SextImpl Instance = new SextImpl();
        
            private  SextImpl()
            {
            }
            public override Enumerant Value => FunctionParameterAttribute.Enumerant.Sext;
            public new static SextImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SextImpl object.</summary>
            /// <returns>A string that represents the SextImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" FunctionParameterAttribute.Sext()";
            }
        }
        #endregion //Sext

        #region ByVal
        public static ByValImpl ByVal()
        {
            return ByValImpl.Instance;
            
        }

        internal class ByValImpl: FunctionParameterAttribute
        {
            public static readonly ByValImpl Instance = new ByValImpl();
        
            private  ByValImpl()
            {
            }
            public override Enumerant Value => FunctionParameterAttribute.Enumerant.ByVal;
            public new static ByValImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ByValImpl object.</summary>
            /// <returns>A string that represents the ByValImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" FunctionParameterAttribute.ByVal()";
            }
        }
        #endregion //ByVal

        #region Sret
        public static SretImpl Sret()
        {
            return SretImpl.Instance;
            
        }

        internal class SretImpl: FunctionParameterAttribute
        {
            public static readonly SretImpl Instance = new SretImpl();
        
            private  SretImpl()
            {
            }
            public override Enumerant Value => FunctionParameterAttribute.Enumerant.Sret;
            public new static SretImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SretImpl object.</summary>
            /// <returns>A string that represents the SretImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" FunctionParameterAttribute.Sret()";
            }
        }
        #endregion //Sret

        #region NoAlias
        public static NoAliasImpl NoAlias()
        {
            return NoAliasImpl.Instance;
            
        }

        internal class NoAliasImpl: FunctionParameterAttribute
        {
            public static readonly NoAliasImpl Instance = new NoAliasImpl();
        
            private  NoAliasImpl()
            {
            }
            public override Enumerant Value => FunctionParameterAttribute.Enumerant.NoAlias;
            public new static NoAliasImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NoAliasImpl object.</summary>
            /// <returns>A string that represents the NoAliasImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" FunctionParameterAttribute.NoAlias()";
            }
        }
        #endregion //NoAlias

        #region NoCapture
        public static NoCaptureImpl NoCapture()
        {
            return NoCaptureImpl.Instance;
            
        }

        internal class NoCaptureImpl: FunctionParameterAttribute
        {
            public static readonly NoCaptureImpl Instance = new NoCaptureImpl();
        
            private  NoCaptureImpl()
            {
            }
            public override Enumerant Value => FunctionParameterAttribute.Enumerant.NoCapture;
            public new static NoCaptureImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NoCaptureImpl object.</summary>
            /// <returns>A string that represents the NoCaptureImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" FunctionParameterAttribute.NoCapture()";
            }
        }
        #endregion //NoCapture

        #region NoWrite
        public static NoWriteImpl NoWrite()
        {
            return NoWriteImpl.Instance;
            
        }

        internal class NoWriteImpl: FunctionParameterAttribute
        {
            public static readonly NoWriteImpl Instance = new NoWriteImpl();
        
            private  NoWriteImpl()
            {
            }
            public override Enumerant Value => FunctionParameterAttribute.Enumerant.NoWrite;
            public new static NoWriteImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NoWriteImpl object.</summary>
            /// <returns>A string that represents the NoWriteImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" FunctionParameterAttribute.NoWrite()";
            }
        }
        #endregion //NoWrite

        #region NoReadWrite
        public static NoReadWriteImpl NoReadWrite()
        {
            return NoReadWriteImpl.Instance;
            
        }

        internal class NoReadWriteImpl: FunctionParameterAttribute
        {
            public static readonly NoReadWriteImpl Instance = new NoReadWriteImpl();
        
            private  NoReadWriteImpl()
            {
            }
            public override Enumerant Value => FunctionParameterAttribute.Enumerant.NoReadWrite;
            public new static NoReadWriteImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NoReadWriteImpl object.</summary>
            /// <returns>A string that represents the NoReadWriteImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" FunctionParameterAttribute.NoReadWrite()";
            }
        }
        #endregion //NoReadWrite

        public abstract Enumerant Value { get; }

        public static FunctionParameterAttribute Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Zext :
                    return ZextImpl.Parse(reader, wordCount - 1);
                case Enumerant.Sext :
                    return SextImpl.Parse(reader, wordCount - 1);
                case Enumerant.ByVal :
                    return ByValImpl.Parse(reader, wordCount - 1);
                case Enumerant.Sret :
                    return SretImpl.Parse(reader, wordCount - 1);
                case Enumerant.NoAlias :
                    return NoAliasImpl.Parse(reader, wordCount - 1);
                case Enumerant.NoCapture :
                    return NoCaptureImpl.Parse(reader, wordCount - 1);
                case Enumerant.NoWrite :
                    return NoWriteImpl.Parse(reader, wordCount - 1);
                case Enumerant.NoReadWrite :
                    return NoReadWriteImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown FunctionParameterAttribute "+id);
            }
        }
        
        public static FunctionParameterAttribute ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<FunctionParameterAttribute> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<FunctionParameterAttribute>();
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