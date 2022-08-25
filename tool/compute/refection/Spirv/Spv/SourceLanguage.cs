using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class SourceLanguage : ValueEnum
    {
        public enum Enumerant
        {
            Unknown = 0,
            ESSL = 1,
            GLSL = 2,
            OpenCL_C = 3,
            OpenCL_CPP = 4,
            HLSL = 5,
        }

        #region Unknown
        public static UnknownImpl Unknown()
        {
            return UnknownImpl.Instance;
            
        }

        public class UnknownImpl: SourceLanguage
        {
            public static readonly UnknownImpl Instance = new UnknownImpl();
        
            private  UnknownImpl()
            {
            }
            public override Enumerant Value => SourceLanguage.Enumerant.Unknown;
            public new static UnknownImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UnknownImpl object.</summary>
            /// <returns>A string that represents the UnknownImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SourceLanguage.Unknown()";
            }
        }
        #endregion //Unknown

        #region ESSL
        public static ESSLImpl ESSL()
        {
            return ESSLImpl.Instance;
            
        }

        public class ESSLImpl: SourceLanguage
        {
            public static readonly ESSLImpl Instance = new ESSLImpl();
        
            private  ESSLImpl()
            {
            }
            public override Enumerant Value => SourceLanguage.Enumerant.ESSL;
            public new static ESSLImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ESSLImpl object.</summary>
            /// <returns>A string that represents the ESSLImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SourceLanguage.ESSL()";
            }
        }
        #endregion //ESSL

        #region GLSL
        public static GLSLImpl GLSL()
        {
            return GLSLImpl.Instance;
            
        }

        public class GLSLImpl: SourceLanguage
        {
            public static readonly GLSLImpl Instance = new GLSLImpl();
        
            private  GLSLImpl()
            {
            }
            public override Enumerant Value => SourceLanguage.Enumerant.GLSL;
            public new static GLSLImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GLSLImpl object.</summary>
            /// <returns>A string that represents the GLSLImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SourceLanguage.GLSL()";
            }
        }
        #endregion //GLSL

        #region OpenCL_C
        public static OpenCL_CImpl OpenCL_C()
        {
            return OpenCL_CImpl.Instance;
            
        }

        public class OpenCL_CImpl: SourceLanguage
        {
            public static readonly OpenCL_CImpl Instance = new OpenCL_CImpl();
        
            private  OpenCL_CImpl()
            {
            }
            public override Enumerant Value => SourceLanguage.Enumerant.OpenCL_C;
            public new static OpenCL_CImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the OpenCL_CImpl object.</summary>
            /// <returns>A string that represents the OpenCL_CImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SourceLanguage.OpenCL_C()";
            }
        }
        #endregion //OpenCL_C

        #region OpenCL_CPP
        public static OpenCL_CPPImpl OpenCL_CPP()
        {
            return OpenCL_CPPImpl.Instance;
            
        }

        public class OpenCL_CPPImpl: SourceLanguage
        {
            public static readonly OpenCL_CPPImpl Instance = new OpenCL_CPPImpl();
        
            private  OpenCL_CPPImpl()
            {
            }
            public override Enumerant Value => SourceLanguage.Enumerant.OpenCL_CPP;
            public new static OpenCL_CPPImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the OpenCL_CPPImpl object.</summary>
            /// <returns>A string that represents the OpenCL_CPPImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SourceLanguage.OpenCL_CPP()";
            }
        }
        #endregion //OpenCL_CPP

        #region HLSL
        public static HLSLImpl HLSL()
        {
            return HLSLImpl.Instance;
            
        }

        public class HLSLImpl: SourceLanguage
        {
            public static readonly HLSLImpl Instance = new HLSLImpl();
        
            private  HLSLImpl()
            {
            }
            public override Enumerant Value => SourceLanguage.Enumerant.HLSL;
            public new static HLSLImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the HLSLImpl object.</summary>
            /// <returns>A string that represents the HLSLImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" SourceLanguage.HLSL()";
            }
        }
        #endregion //HLSL

        public abstract Enumerant Value { get; }

        public static SourceLanguage Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Unknown :
                    return UnknownImpl.Parse(reader, wordCount - 1);
                case Enumerant.ESSL :
                    return ESSLImpl.Parse(reader, wordCount - 1);
                case Enumerant.GLSL :
                    return GLSLImpl.Parse(reader, wordCount - 1);
                case Enumerant.OpenCL_C :
                    return OpenCL_CImpl.Parse(reader, wordCount - 1);
                case Enumerant.OpenCL_CPP :
                    return OpenCL_CPPImpl.Parse(reader, wordCount - 1);
                case Enumerant.HLSL :
                    return HLSLImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown SourceLanguage "+id);
            }
        }
        
        public static SourceLanguage ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<SourceLanguage> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<SourceLanguage>();
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