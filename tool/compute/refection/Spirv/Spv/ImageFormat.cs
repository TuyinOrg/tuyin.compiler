using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class ImageFormat : ValueEnum
    {
        public enum Enumerant
        {
            Unknown = 0,
            [Capability(Capability.Enumerant.Shader)]
            Rgba32f = 1,
            [Capability(Capability.Enumerant.Shader)]
            Rgba16f = 2,
            [Capability(Capability.Enumerant.Shader)]
            R32f = 3,
            [Capability(Capability.Enumerant.Shader)]
            Rgba8 = 4,
            [Capability(Capability.Enumerant.Shader)]
            Rgba8Snorm = 5,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rg32f = 6,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rg16f = 7,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            R11fG11fB10f = 8,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            R16f = 9,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rgba16 = 10,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rgb10A2 = 11,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rg16 = 12,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rg8 = 13,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            R16 = 14,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            R8 = 15,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rgba16Snorm = 16,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rg16Snorm = 17,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rg8Snorm = 18,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            R16Snorm = 19,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            R8Snorm = 20,
            [Capability(Capability.Enumerant.Shader)]
            Rgba32i = 21,
            [Capability(Capability.Enumerant.Shader)]
            Rgba16i = 22,
            [Capability(Capability.Enumerant.Shader)]
            Rgba8i = 23,
            [Capability(Capability.Enumerant.Shader)]
            R32i = 24,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rg32i = 25,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rg16i = 26,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rg8i = 27,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            R16i = 28,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            R8i = 29,
            [Capability(Capability.Enumerant.Shader)]
            Rgba32ui = 30,
            [Capability(Capability.Enumerant.Shader)]
            Rgba16ui = 31,
            [Capability(Capability.Enumerant.Shader)]
            Rgba8ui = 32,
            [Capability(Capability.Enumerant.Shader)]
            R32ui = 33,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rgb10a2ui = 34,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rg32ui = 35,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rg16ui = 36,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            Rg8ui = 37,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            R16ui = 38,
            [Capability(Capability.Enumerant.StorageImageExtendedFormats)]
            R8ui = 39,
        }

        #region Unknown
        public static UnknownImpl Unknown()
        {
            return UnknownImpl.Instance;
            
        }

        internal class UnknownImpl: ImageFormat
        {
            public static readonly UnknownImpl Instance = new UnknownImpl();
        
            private  UnknownImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Unknown;
            public new static UnknownImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UnknownImpl object.</summary>
            /// <returns>A string that represents the UnknownImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Unknown()";
            }
        }
        #endregion //Unknown

        #region Rgba32f
        public static Rgba32fImpl Rgba32f()
        {
            return Rgba32fImpl.Instance;
            
        }

        internal class Rgba32fImpl: ImageFormat
        {
            public static readonly Rgba32fImpl Instance = new Rgba32fImpl();
        
            private  Rgba32fImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgba32f;
            public new static Rgba32fImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgba32fImpl object.</summary>
            /// <returns>A string that represents the Rgba32fImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgba32f()";
            }
        }
        #endregion //Rgba32f

        #region Rgba16f
        public static Rgba16fImpl Rgba16f()
        {
            return Rgba16fImpl.Instance;
            
        }

        internal class Rgba16fImpl: ImageFormat
        {
            public static readonly Rgba16fImpl Instance = new Rgba16fImpl();
        
            private  Rgba16fImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgba16f;
            public new static Rgba16fImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgba16fImpl object.</summary>
            /// <returns>A string that represents the Rgba16fImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgba16f()";
            }
        }
        #endregion //Rgba16f

        #region R32f
        public static R32fImpl R32f()
        {
            return R32fImpl.Instance;
            
        }

        internal class R32fImpl: ImageFormat
        {
            public static readonly R32fImpl Instance = new R32fImpl();
        
            private  R32fImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R32f;
            public new static R32fImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R32fImpl object.</summary>
            /// <returns>A string that represents the R32fImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R32f()";
            }
        }
        #endregion //R32f

        #region Rgba8
        public static Rgba8Impl Rgba8()
        {
            return Rgba8Impl.Instance;
            
        }

        internal class Rgba8Impl: ImageFormat
        {
            public static readonly Rgba8Impl Instance = new Rgba8Impl();
        
            private  Rgba8Impl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgba8;
            public new static Rgba8Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgba8Impl object.</summary>
            /// <returns>A string that represents the Rgba8Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgba8()";
            }
        }
        #endregion //Rgba8

        #region Rgba8Snorm
        public static Rgba8SnormImpl Rgba8Snorm()
        {
            return Rgba8SnormImpl.Instance;
            
        }

        internal class Rgba8SnormImpl: ImageFormat
        {
            public static readonly Rgba8SnormImpl Instance = new Rgba8SnormImpl();
        
            private  Rgba8SnormImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgba8Snorm;
            public new static Rgba8SnormImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgba8SnormImpl object.</summary>
            /// <returns>A string that represents the Rgba8SnormImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgba8Snorm()";
            }
        }
        #endregion //Rgba8Snorm

        #region Rg32f
        public static Rg32fImpl Rg32f()
        {
            return Rg32fImpl.Instance;
            
        }

        internal class Rg32fImpl: ImageFormat
        {
            public static readonly Rg32fImpl Instance = new Rg32fImpl();
        
            private  Rg32fImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rg32f;
            public new static Rg32fImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rg32fImpl object.</summary>
            /// <returns>A string that represents the Rg32fImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rg32f()";
            }
        }
        #endregion //Rg32f

        #region Rg16f
        public static Rg16fImpl Rg16f()
        {
            return Rg16fImpl.Instance;
            
        }

        internal class Rg16fImpl: ImageFormat
        {
            public static readonly Rg16fImpl Instance = new Rg16fImpl();
        
            private  Rg16fImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rg16f;
            public new static Rg16fImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rg16fImpl object.</summary>
            /// <returns>A string that represents the Rg16fImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rg16f()";
            }
        }
        #endregion //Rg16f

        #region R11fG11fB10f
        public static R11fG11fB10fImpl R11fG11fB10f()
        {
            return R11fG11fB10fImpl.Instance;
            
        }

        internal class R11fG11fB10fImpl: ImageFormat
        {
            public static readonly R11fG11fB10fImpl Instance = new R11fG11fB10fImpl();
        
            private  R11fG11fB10fImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R11fG11fB10f;
            public new static R11fG11fB10fImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R11fG11fB10fImpl object.</summary>
            /// <returns>A string that represents the R11fG11fB10fImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R11fG11fB10f()";
            }
        }
        #endregion //R11fG11fB10f

        #region R16f
        public static R16fImpl R16f()
        {
            return R16fImpl.Instance;
            
        }

        internal class R16fImpl: ImageFormat
        {
            public static readonly R16fImpl Instance = new R16fImpl();
        
            private  R16fImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R16f;
            public new static R16fImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R16fImpl object.</summary>
            /// <returns>A string that represents the R16fImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R16f()";
            }
        }
        #endregion //R16f

        #region Rgba16
        public static Rgba16Impl Rgba16()
        {
            return Rgba16Impl.Instance;
            
        }

        internal class Rgba16Impl: ImageFormat
        {
            public static readonly Rgba16Impl Instance = new Rgba16Impl();
        
            private  Rgba16Impl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgba16;
            public new static Rgba16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgba16Impl object.</summary>
            /// <returns>A string that represents the Rgba16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgba16()";
            }
        }
        #endregion //Rgba16

        #region Rgb10A2
        public static Rgb10A2Impl Rgb10A2()
        {
            return Rgb10A2Impl.Instance;
            
        }

        internal class Rgb10A2Impl: ImageFormat
        {
            public static readonly Rgb10A2Impl Instance = new Rgb10A2Impl();
        
            private  Rgb10A2Impl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgb10A2;
            public new static Rgb10A2Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgb10A2Impl object.</summary>
            /// <returns>A string that represents the Rgb10A2Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgb10A2()";
            }
        }
        #endregion //Rgb10A2

        #region Rg16
        public static Rg16Impl Rg16()
        {
            return Rg16Impl.Instance;
            
        }

        internal class Rg16Impl: ImageFormat
        {
            public static readonly Rg16Impl Instance = new Rg16Impl();
        
            private  Rg16Impl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rg16;
            public new static Rg16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rg16Impl object.</summary>
            /// <returns>A string that represents the Rg16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rg16()";
            }
        }
        #endregion //Rg16

        #region Rg8
        public static Rg8Impl Rg8()
        {
            return Rg8Impl.Instance;
            
        }

        internal class Rg8Impl: ImageFormat
        {
            public static readonly Rg8Impl Instance = new Rg8Impl();
        
            private  Rg8Impl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rg8;
            public new static Rg8Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rg8Impl object.</summary>
            /// <returns>A string that represents the Rg8Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rg8()";
            }
        }
        #endregion //Rg8

        #region R16
        public static R16Impl R16()
        {
            return R16Impl.Instance;
            
        }

        internal class R16Impl: ImageFormat
        {
            public static readonly R16Impl Instance = new R16Impl();
        
            private  R16Impl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R16;
            public new static R16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R16Impl object.</summary>
            /// <returns>A string that represents the R16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R16()";
            }
        }
        #endregion //R16

        #region R8
        public static R8Impl R8()
        {
            return R8Impl.Instance;
            
        }

        internal class R8Impl: ImageFormat
        {
            public static readonly R8Impl Instance = new R8Impl();
        
            private  R8Impl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R8;
            public new static R8Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R8Impl object.</summary>
            /// <returns>A string that represents the R8Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R8()";
            }
        }
        #endregion //R8

        #region Rgba16Snorm
        public static Rgba16SnormImpl Rgba16Snorm()
        {
            return Rgba16SnormImpl.Instance;
            
        }

        internal class Rgba16SnormImpl: ImageFormat
        {
            public static readonly Rgba16SnormImpl Instance = new Rgba16SnormImpl();
        
            private  Rgba16SnormImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgba16Snorm;
            public new static Rgba16SnormImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgba16SnormImpl object.</summary>
            /// <returns>A string that represents the Rgba16SnormImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgba16Snorm()";
            }
        }
        #endregion //Rgba16Snorm

        #region Rg16Snorm
        public static Rg16SnormImpl Rg16Snorm()
        {
            return Rg16SnormImpl.Instance;
            
        }

        internal class Rg16SnormImpl: ImageFormat
        {
            public static readonly Rg16SnormImpl Instance = new Rg16SnormImpl();
        
            private  Rg16SnormImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rg16Snorm;
            public new static Rg16SnormImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rg16SnormImpl object.</summary>
            /// <returns>A string that represents the Rg16SnormImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rg16Snorm()";
            }
        }
        #endregion //Rg16Snorm

        #region Rg8Snorm
        public static Rg8SnormImpl Rg8Snorm()
        {
            return Rg8SnormImpl.Instance;
            
        }

        internal class Rg8SnormImpl: ImageFormat
        {
            public static readonly Rg8SnormImpl Instance = new Rg8SnormImpl();
        
            private  Rg8SnormImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rg8Snorm;
            public new static Rg8SnormImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rg8SnormImpl object.</summary>
            /// <returns>A string that represents the Rg8SnormImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rg8Snorm()";
            }
        }
        #endregion //Rg8Snorm

        #region R16Snorm
        public static R16SnormImpl R16Snorm()
        {
            return R16SnormImpl.Instance;
            
        }

        internal class R16SnormImpl: ImageFormat
        {
            public static readonly R16SnormImpl Instance = new R16SnormImpl();
        
            private  R16SnormImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R16Snorm;
            public new static R16SnormImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R16SnormImpl object.</summary>
            /// <returns>A string that represents the R16SnormImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R16Snorm()";
            }
        }
        #endregion //R16Snorm

        #region R8Snorm
        public static R8SnormImpl R8Snorm()
        {
            return R8SnormImpl.Instance;
            
        }

        internal class R8SnormImpl: ImageFormat
        {
            public static readonly R8SnormImpl Instance = new R8SnormImpl();
        
            private  R8SnormImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R8Snorm;
            public new static R8SnormImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R8SnormImpl object.</summary>
            /// <returns>A string that represents the R8SnormImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R8Snorm()";
            }
        }
        #endregion //R8Snorm

        #region Rgba32i
        public static Rgba32iImpl Rgba32i()
        {
            return Rgba32iImpl.Instance;
            
        }

        internal class Rgba32iImpl: ImageFormat
        {
            public static readonly Rgba32iImpl Instance = new Rgba32iImpl();
        
            private  Rgba32iImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgba32i;
            public new static Rgba32iImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgba32iImpl object.</summary>
            /// <returns>A string that represents the Rgba32iImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgba32i()";
            }
        }
        #endregion //Rgba32i

        #region Rgba16i
        public static Rgba16iImpl Rgba16i()
        {
            return Rgba16iImpl.Instance;
            
        }

        internal class Rgba16iImpl: ImageFormat
        {
            public static readonly Rgba16iImpl Instance = new Rgba16iImpl();
        
            private  Rgba16iImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgba16i;
            public new static Rgba16iImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgba16iImpl object.</summary>
            /// <returns>A string that represents the Rgba16iImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgba16i()";
            }
        }
        #endregion //Rgba16i

        #region Rgba8i
        public static Rgba8iImpl Rgba8i()
        {
            return Rgba8iImpl.Instance;
            
        }

        internal class Rgba8iImpl: ImageFormat
        {
            public static readonly Rgba8iImpl Instance = new Rgba8iImpl();
        
            private  Rgba8iImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgba8i;
            public new static Rgba8iImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgba8iImpl object.</summary>
            /// <returns>A string that represents the Rgba8iImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgba8i()";
            }
        }
        #endregion //Rgba8i

        #region R32i
        public static R32iImpl R32i()
        {
            return R32iImpl.Instance;
            
        }

        internal class R32iImpl: ImageFormat
        {
            public static readonly R32iImpl Instance = new R32iImpl();
        
            private  R32iImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R32i;
            public new static R32iImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R32iImpl object.</summary>
            /// <returns>A string that represents the R32iImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R32i()";
            }
        }
        #endregion //R32i

        #region Rg32i
        public static Rg32iImpl Rg32i()
        {
            return Rg32iImpl.Instance;
            
        }

        internal class Rg32iImpl: ImageFormat
        {
            public static readonly Rg32iImpl Instance = new Rg32iImpl();
        
            private  Rg32iImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rg32i;
            public new static Rg32iImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rg32iImpl object.</summary>
            /// <returns>A string that represents the Rg32iImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rg32i()";
            }
        }
        #endregion //Rg32i

        #region Rg16i
        public static Rg16iImpl Rg16i()
        {
            return Rg16iImpl.Instance;
            
        }

        internal class Rg16iImpl: ImageFormat
        {
            public static readonly Rg16iImpl Instance = new Rg16iImpl();
        
            private  Rg16iImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rg16i;
            public new static Rg16iImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rg16iImpl object.</summary>
            /// <returns>A string that represents the Rg16iImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rg16i()";
            }
        }
        #endregion //Rg16i

        #region Rg8i
        public static Rg8iImpl Rg8i()
        {
            return Rg8iImpl.Instance;
            
        }

        internal class Rg8iImpl: ImageFormat
        {
            public static readonly Rg8iImpl Instance = new Rg8iImpl();
        
            private  Rg8iImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rg8i;
            public new static Rg8iImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rg8iImpl object.</summary>
            /// <returns>A string that represents the Rg8iImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rg8i()";
            }
        }
        #endregion //Rg8i

        #region R16i
        public static R16iImpl R16i()
        {
            return R16iImpl.Instance;
            
        }

        internal class R16iImpl: ImageFormat
        {
            public static readonly R16iImpl Instance = new R16iImpl();
        
            private  R16iImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R16i;
            public new static R16iImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R16iImpl object.</summary>
            /// <returns>A string that represents the R16iImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R16i()";
            }
        }
        #endregion //R16i

        #region R8i
        public static R8iImpl R8i()
        {
            return R8iImpl.Instance;
            
        }

        internal class R8iImpl: ImageFormat
        {
            public static readonly R8iImpl Instance = new R8iImpl();
        
            private  R8iImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R8i;
            public new static R8iImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R8iImpl object.</summary>
            /// <returns>A string that represents the R8iImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R8i()";
            }
        }
        #endregion //R8i

        #region Rgba32ui
        public static Rgba32uiImpl Rgba32ui()
        {
            return Rgba32uiImpl.Instance;
            
        }

        internal class Rgba32uiImpl: ImageFormat
        {
            public static readonly Rgba32uiImpl Instance = new Rgba32uiImpl();
        
            private  Rgba32uiImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgba32ui;
            public new static Rgba32uiImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgba32uiImpl object.</summary>
            /// <returns>A string that represents the Rgba32uiImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgba32ui()";
            }
        }
        #endregion //Rgba32ui

        #region Rgba16ui
        public static Rgba16uiImpl Rgba16ui()
        {
            return Rgba16uiImpl.Instance;
            
        }

        internal class Rgba16uiImpl: ImageFormat
        {
            public static readonly Rgba16uiImpl Instance = new Rgba16uiImpl();
        
            private  Rgba16uiImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgba16ui;
            public new static Rgba16uiImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgba16uiImpl object.</summary>
            /// <returns>A string that represents the Rgba16uiImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgba16ui()";
            }
        }
        #endregion //Rgba16ui

        #region Rgba8ui
        public static Rgba8uiImpl Rgba8ui()
        {
            return Rgba8uiImpl.Instance;
            
        }

        internal class Rgba8uiImpl: ImageFormat
        {
            public static readonly Rgba8uiImpl Instance = new Rgba8uiImpl();
        
            private  Rgba8uiImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgba8ui;
            public new static Rgba8uiImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgba8uiImpl object.</summary>
            /// <returns>A string that represents the Rgba8uiImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgba8ui()";
            }
        }
        #endregion //Rgba8ui

        #region R32ui
        public static R32uiImpl R32ui()
        {
            return R32uiImpl.Instance;
            
        }

        internal class R32uiImpl: ImageFormat
        {
            public static readonly R32uiImpl Instance = new R32uiImpl();
        
            private  R32uiImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R32ui;
            public new static R32uiImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R32uiImpl object.</summary>
            /// <returns>A string that represents the R32uiImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R32ui()";
            }
        }
        #endregion //R32ui

        #region Rgb10a2ui
        public static Rgb10a2uiImpl Rgb10a2ui()
        {
            return Rgb10a2uiImpl.Instance;
            
        }

        internal class Rgb10a2uiImpl: ImageFormat
        {
            public static readonly Rgb10a2uiImpl Instance = new Rgb10a2uiImpl();
        
            private  Rgb10a2uiImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rgb10a2ui;
            public new static Rgb10a2uiImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rgb10a2uiImpl object.</summary>
            /// <returns>A string that represents the Rgb10a2uiImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rgb10a2ui()";
            }
        }
        #endregion //Rgb10a2ui

        #region Rg32ui
        public static Rg32uiImpl Rg32ui()
        {
            return Rg32uiImpl.Instance;
            
        }

        internal class Rg32uiImpl: ImageFormat
        {
            public static readonly Rg32uiImpl Instance = new Rg32uiImpl();
        
            private  Rg32uiImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rg32ui;
            public new static Rg32uiImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rg32uiImpl object.</summary>
            /// <returns>A string that represents the Rg32uiImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rg32ui()";
            }
        }
        #endregion //Rg32ui

        #region Rg16ui
        public static Rg16uiImpl Rg16ui()
        {
            return Rg16uiImpl.Instance;
            
        }

        internal class Rg16uiImpl: ImageFormat
        {
            public static readonly Rg16uiImpl Instance = new Rg16uiImpl();
        
            private  Rg16uiImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rg16ui;
            public new static Rg16uiImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rg16uiImpl object.</summary>
            /// <returns>A string that represents the Rg16uiImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rg16ui()";
            }
        }
        #endregion //Rg16ui

        #region Rg8ui
        public static Rg8uiImpl Rg8ui()
        {
            return Rg8uiImpl.Instance;
            
        }

        internal class Rg8uiImpl: ImageFormat
        {
            public static readonly Rg8uiImpl Instance = new Rg8uiImpl();
        
            private  Rg8uiImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.Rg8ui;
            public new static Rg8uiImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Rg8uiImpl object.</summary>
            /// <returns>A string that represents the Rg8uiImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.Rg8ui()";
            }
        }
        #endregion //Rg8ui

        #region R16ui
        public static R16uiImpl R16ui()
        {
            return R16uiImpl.Instance;
            
        }

        internal class R16uiImpl: ImageFormat
        {
            public static readonly R16uiImpl Instance = new R16uiImpl();
        
            private  R16uiImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R16ui;
            public new static R16uiImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R16uiImpl object.</summary>
            /// <returns>A string that represents the R16uiImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R16ui()";
            }
        }
        #endregion //R16ui

        #region R8ui
        public static R8uiImpl R8ui()
        {
            return R8uiImpl.Instance;
            
        }

        internal class R8uiImpl: ImageFormat
        {
            public static readonly R8uiImpl Instance = new R8uiImpl();
        
            private  R8uiImpl()
            {
            }
            public override Enumerant Value => ImageFormat.Enumerant.R8ui;
            public new static R8uiImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the R8uiImpl object.</summary>
            /// <returns>A string that represents the R8uiImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageFormat.R8ui()";
            }
        }
        #endregion //R8ui

        public abstract Enumerant Value { get; }

        public static ImageFormat Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Unknown :
                    return UnknownImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rgba32f :
                    return Rgba32fImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rgba16f :
                    return Rgba16fImpl.Parse(reader, wordCount - 1);
                case Enumerant.R32f :
                    return R32fImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rgba8 :
                    return Rgba8Impl.Parse(reader, wordCount - 1);
                case Enumerant.Rgba8Snorm :
                    return Rgba8SnormImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rg32f :
                    return Rg32fImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rg16f :
                    return Rg16fImpl.Parse(reader, wordCount - 1);
                case Enumerant.R11fG11fB10f :
                    return R11fG11fB10fImpl.Parse(reader, wordCount - 1);
                case Enumerant.R16f :
                    return R16fImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rgba16 :
                    return Rgba16Impl.Parse(reader, wordCount - 1);
                case Enumerant.Rgb10A2 :
                    return Rgb10A2Impl.Parse(reader, wordCount - 1);
                case Enumerant.Rg16 :
                    return Rg16Impl.Parse(reader, wordCount - 1);
                case Enumerant.Rg8 :
                    return Rg8Impl.Parse(reader, wordCount - 1);
                case Enumerant.R16 :
                    return R16Impl.Parse(reader, wordCount - 1);
                case Enumerant.R8 :
                    return R8Impl.Parse(reader, wordCount - 1);
                case Enumerant.Rgba16Snorm :
                    return Rgba16SnormImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rg16Snorm :
                    return Rg16SnormImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rg8Snorm :
                    return Rg8SnormImpl.Parse(reader, wordCount - 1);
                case Enumerant.R16Snorm :
                    return R16SnormImpl.Parse(reader, wordCount - 1);
                case Enumerant.R8Snorm :
                    return R8SnormImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rgba32i :
                    return Rgba32iImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rgba16i :
                    return Rgba16iImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rgba8i :
                    return Rgba8iImpl.Parse(reader, wordCount - 1);
                case Enumerant.R32i :
                    return R32iImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rg32i :
                    return Rg32iImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rg16i :
                    return Rg16iImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rg8i :
                    return Rg8iImpl.Parse(reader, wordCount - 1);
                case Enumerant.R16i :
                    return R16iImpl.Parse(reader, wordCount - 1);
                case Enumerant.R8i :
                    return R8iImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rgba32ui :
                    return Rgba32uiImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rgba16ui :
                    return Rgba16uiImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rgba8ui :
                    return Rgba8uiImpl.Parse(reader, wordCount - 1);
                case Enumerant.R32ui :
                    return R32uiImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rgb10a2ui :
                    return Rgb10a2uiImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rg32ui :
                    return Rg32uiImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rg16ui :
                    return Rg16uiImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rg8ui :
                    return Rg8uiImpl.Parse(reader, wordCount - 1);
                case Enumerant.R16ui :
                    return R16uiImpl.Parse(reader, wordCount - 1);
                case Enumerant.R8ui :
                    return R8uiImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown ImageFormat "+id);
            }
        }
        
        public static ImageFormat ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<ImageFormat> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<ImageFormat>();
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