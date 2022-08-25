using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class ImageChannelOrder : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Kernel)]
            R = 0,
            [Capability(Capability.Enumerant.Kernel)]
            A = 1,
            [Capability(Capability.Enumerant.Kernel)]
            RG = 2,
            [Capability(Capability.Enumerant.Kernel)]
            RA = 3,
            [Capability(Capability.Enumerant.Kernel)]
            RGB = 4,
            [Capability(Capability.Enumerant.Kernel)]
            RGBA = 5,
            [Capability(Capability.Enumerant.Kernel)]
            BGRA = 6,
            [Capability(Capability.Enumerant.Kernel)]
            ARGB = 7,
            [Capability(Capability.Enumerant.Kernel)]
            Intensity = 8,
            [Capability(Capability.Enumerant.Kernel)]
            Luminance = 9,
            [Capability(Capability.Enumerant.Kernel)]
            Rx = 10,
            [Capability(Capability.Enumerant.Kernel)]
            RGx = 11,
            [Capability(Capability.Enumerant.Kernel)]
            RGBx = 12,
            [Capability(Capability.Enumerant.Kernel)]
            Depth = 13,
            [Capability(Capability.Enumerant.Kernel)]
            DepthStencil = 14,
            [Capability(Capability.Enumerant.Kernel)]
            sRGB = 15,
            [Capability(Capability.Enumerant.Kernel)]
            sRGBx = 16,
            [Capability(Capability.Enumerant.Kernel)]
            sRGBA = 17,
            [Capability(Capability.Enumerant.Kernel)]
            sBGRA = 18,
            [Capability(Capability.Enumerant.Kernel)]
            ABGR = 19,
        }

        #region R
        public static RImpl R()
        {
            return RImpl.Instance;
            
        }

        internal class RImpl: ImageChannelOrder
        {
            public static readonly RImpl Instance = new RImpl();
        
            private  RImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.R;
            public new static RImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RImpl object.</summary>
            /// <returns>A string that represents the RImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.R()";
            }
        }
        #endregion //R

        #region A
        public static AImpl A()
        {
            return AImpl.Instance;
            
        }

        internal class AImpl: ImageChannelOrder
        {
            public static readonly AImpl Instance = new AImpl();
        
            private  AImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.A;
            public new static AImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the AImpl object.</summary>
            /// <returns>A string that represents the AImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.A()";
            }
        }
        #endregion //A

        #region RG
        public static RGImpl RG()
        {
            return RGImpl.Instance;
            
        }

        internal class RGImpl: ImageChannelOrder
        {
            public static readonly RGImpl Instance = new RGImpl();
        
            private  RGImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.RG;
            public new static RGImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RGImpl object.</summary>
            /// <returns>A string that represents the RGImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.RG()";
            }
        }
        #endregion //RG

        #region RA
        public static RAImpl RA()
        {
            return RAImpl.Instance;
            
        }

        internal class RAImpl: ImageChannelOrder
        {
            public static readonly RAImpl Instance = new RAImpl();
        
            private  RAImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.RA;
            public new static RAImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RAImpl object.</summary>
            /// <returns>A string that represents the RAImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.RA()";
            }
        }
        #endregion //RA

        #region RGB
        public static RGBImpl RGB()
        {
            return RGBImpl.Instance;
            
        }

        internal class RGBImpl: ImageChannelOrder
        {
            public static readonly RGBImpl Instance = new RGBImpl();
        
            private  RGBImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.RGB;
            public new static RGBImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RGBImpl object.</summary>
            /// <returns>A string that represents the RGBImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.RGB()";
            }
        }
        #endregion //RGB

        #region RGBA
        public static RGBAImpl RGBA()
        {
            return RGBAImpl.Instance;
            
        }

        internal class RGBAImpl: ImageChannelOrder
        {
            public static readonly RGBAImpl Instance = new RGBAImpl();
        
            private  RGBAImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.RGBA;
            public new static RGBAImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RGBAImpl object.</summary>
            /// <returns>A string that represents the RGBAImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.RGBA()";
            }
        }
        #endregion //RGBA

        #region BGRA
        public static BGRAImpl BGRA()
        {
            return BGRAImpl.Instance;
            
        }

        internal class BGRAImpl: ImageChannelOrder
        {
            public static readonly BGRAImpl Instance = new BGRAImpl();
        
            private  BGRAImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.BGRA;
            public new static BGRAImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BGRAImpl object.</summary>
            /// <returns>A string that represents the BGRAImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.BGRA()";
            }
        }
        #endregion //BGRA

        #region ARGB
        public static ARGBImpl ARGB()
        {
            return ARGBImpl.Instance;
            
        }

        internal class ARGBImpl: ImageChannelOrder
        {
            public static readonly ARGBImpl Instance = new ARGBImpl();
        
            private  ARGBImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.ARGB;
            public new static ARGBImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ARGBImpl object.</summary>
            /// <returns>A string that represents the ARGBImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.ARGB()";
            }
        }
        #endregion //ARGB

        #region Intensity
        public static IntensityImpl Intensity()
        {
            return IntensityImpl.Instance;
            
        }

        internal class IntensityImpl: ImageChannelOrder
        {
            public static readonly IntensityImpl Instance = new IntensityImpl();
        
            private  IntensityImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.Intensity;
            public new static IntensityImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the IntensityImpl object.</summary>
            /// <returns>A string that represents the IntensityImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.Intensity()";
            }
        }
        #endregion //Intensity

        #region Luminance
        public static LuminanceImpl Luminance()
        {
            return LuminanceImpl.Instance;
            
        }

        internal class LuminanceImpl: ImageChannelOrder
        {
            public static readonly LuminanceImpl Instance = new LuminanceImpl();
        
            private  LuminanceImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.Luminance;
            public new static LuminanceImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LuminanceImpl object.</summary>
            /// <returns>A string that represents the LuminanceImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.Luminance()";
            }
        }
        #endregion //Luminance

        #region Rx
        public static RxImpl Rx()
        {
            return RxImpl.Instance;
            
        }

        internal class RxImpl: ImageChannelOrder
        {
            public static readonly RxImpl Instance = new RxImpl();
        
            private  RxImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.Rx;
            public new static RxImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RxImpl object.</summary>
            /// <returns>A string that represents the RxImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.Rx()";
            }
        }
        #endregion //Rx

        #region RGx
        public static RGxImpl RGx()
        {
            return RGxImpl.Instance;
            
        }

        internal class RGxImpl: ImageChannelOrder
        {
            public static readonly RGxImpl Instance = new RGxImpl();
        
            private  RGxImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.RGx;
            public new static RGxImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RGxImpl object.</summary>
            /// <returns>A string that represents the RGxImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.RGx()";
            }
        }
        #endregion //RGx

        #region RGBx
        public static RGBxImpl RGBx()
        {
            return RGBxImpl.Instance;
            
        }

        internal class RGBxImpl: ImageChannelOrder
        {
            public static readonly RGBxImpl Instance = new RGBxImpl();
        
            private  RGBxImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.RGBx;
            public new static RGBxImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RGBxImpl object.</summary>
            /// <returns>A string that represents the RGBxImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.RGBx()";
            }
        }
        #endregion //RGBx

        #region Depth
        public static DepthImpl Depth()
        {
            return DepthImpl.Instance;
            
        }

        internal class DepthImpl: ImageChannelOrder
        {
            public static readonly DepthImpl Instance = new DepthImpl();
        
            private  DepthImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.Depth;
            public new static DepthImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DepthImpl object.</summary>
            /// <returns>A string that represents the DepthImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.Depth()";
            }
        }
        #endregion //Depth

        #region DepthStencil
        public static DepthStencilImpl DepthStencil()
        {
            return DepthStencilImpl.Instance;
            
        }

        internal class DepthStencilImpl: ImageChannelOrder
        {
            public static readonly DepthStencilImpl Instance = new DepthStencilImpl();
        
            private  DepthStencilImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.DepthStencil;
            public new static DepthStencilImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DepthStencilImpl object.</summary>
            /// <returns>A string that represents the DepthStencilImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.DepthStencil()";
            }
        }
        #endregion //DepthStencil

        #region sRGB
        public static sRGBImpl sRGB()
        {
            return sRGBImpl.Instance;
            
        }

        internal class sRGBImpl: ImageChannelOrder
        {
            public static readonly sRGBImpl Instance = new sRGBImpl();
        
            private  sRGBImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.sRGB;
            public new static sRGBImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the sRGBImpl object.</summary>
            /// <returns>A string that represents the sRGBImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.sRGB()";
            }
        }
        #endregion //sRGB

        #region sRGBx
        public static sRGBxImpl sRGBx()
        {
            return sRGBxImpl.Instance;
            
        }

        internal class sRGBxImpl: ImageChannelOrder
        {
            public static readonly sRGBxImpl Instance = new sRGBxImpl();
        
            private  sRGBxImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.sRGBx;
            public new static sRGBxImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the sRGBxImpl object.</summary>
            /// <returns>A string that represents the sRGBxImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.sRGBx()";
            }
        }
        #endregion //sRGBx

        #region sRGBA
        public static sRGBAImpl sRGBA()
        {
            return sRGBAImpl.Instance;
            
        }

        internal class sRGBAImpl: ImageChannelOrder
        {
            public static readonly sRGBAImpl Instance = new sRGBAImpl();
        
            private  sRGBAImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.sRGBA;
            public new static sRGBAImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the sRGBAImpl object.</summary>
            /// <returns>A string that represents the sRGBAImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.sRGBA()";
            }
        }
        #endregion //sRGBA

        #region sBGRA
        public static sBGRAImpl sBGRA()
        {
            return sBGRAImpl.Instance;
            
        }

        internal class sBGRAImpl: ImageChannelOrder
        {
            public static readonly sBGRAImpl Instance = new sBGRAImpl();
        
            private  sBGRAImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.sBGRA;
            public new static sBGRAImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the sBGRAImpl object.</summary>
            /// <returns>A string that represents the sBGRAImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.sBGRA()";
            }
        }
        #endregion //sBGRA

        #region ABGR
        public static ABGRImpl ABGR()
        {
            return ABGRImpl.Instance;
            
        }

        internal class ABGRImpl: ImageChannelOrder
        {
            public static readonly ABGRImpl Instance = new ABGRImpl();
        
            private  ABGRImpl()
            {
            }
            public override Enumerant Value => ImageChannelOrder.Enumerant.ABGR;
            public new static ABGRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ABGRImpl object.</summary>
            /// <returns>A string that represents the ABGRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelOrder.ABGR()";
            }
        }
        #endregion //ABGR

        public abstract Enumerant Value { get; }

        public static ImageChannelOrder Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.R :
                    return RImpl.Parse(reader, wordCount - 1);
                case Enumerant.A :
                    return AImpl.Parse(reader, wordCount - 1);
                case Enumerant.RG :
                    return RGImpl.Parse(reader, wordCount - 1);
                case Enumerant.RA :
                    return RAImpl.Parse(reader, wordCount - 1);
                case Enumerant.RGB :
                    return RGBImpl.Parse(reader, wordCount - 1);
                case Enumerant.RGBA :
                    return RGBAImpl.Parse(reader, wordCount - 1);
                case Enumerant.BGRA :
                    return BGRAImpl.Parse(reader, wordCount - 1);
                case Enumerant.ARGB :
                    return ARGBImpl.Parse(reader, wordCount - 1);
                case Enumerant.Intensity :
                    return IntensityImpl.Parse(reader, wordCount - 1);
                case Enumerant.Luminance :
                    return LuminanceImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rx :
                    return RxImpl.Parse(reader, wordCount - 1);
                case Enumerant.RGx :
                    return RGxImpl.Parse(reader, wordCount - 1);
                case Enumerant.RGBx :
                    return RGBxImpl.Parse(reader, wordCount - 1);
                case Enumerant.Depth :
                    return DepthImpl.Parse(reader, wordCount - 1);
                case Enumerant.DepthStencil :
                    return DepthStencilImpl.Parse(reader, wordCount - 1);
                case Enumerant.sRGB :
                    return sRGBImpl.Parse(reader, wordCount - 1);
                case Enumerant.sRGBx :
                    return sRGBxImpl.Parse(reader, wordCount - 1);
                case Enumerant.sRGBA :
                    return sRGBAImpl.Parse(reader, wordCount - 1);
                case Enumerant.sBGRA :
                    return sBGRAImpl.Parse(reader, wordCount - 1);
                case Enumerant.ABGR :
                    return ABGRImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown ImageChannelOrder "+id);
            }
        }
        
        public static ImageChannelOrder ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<ImageChannelOrder> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<ImageChannelOrder>();
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