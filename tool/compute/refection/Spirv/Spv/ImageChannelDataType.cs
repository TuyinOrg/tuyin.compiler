using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class ImageChannelDataType : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Kernel)]
            SnormInt8 = 0,
            [Capability(Capability.Enumerant.Kernel)]
            SnormInt16 = 1,
            [Capability(Capability.Enumerant.Kernel)]
            UnormInt8 = 2,
            [Capability(Capability.Enumerant.Kernel)]
            UnormInt16 = 3,
            [Capability(Capability.Enumerant.Kernel)]
            UnormShort565 = 4,
            [Capability(Capability.Enumerant.Kernel)]
            UnormShort555 = 5,
            [Capability(Capability.Enumerant.Kernel)]
            UnormInt101010 = 6,
            [Capability(Capability.Enumerant.Kernel)]
            SignedInt8 = 7,
            [Capability(Capability.Enumerant.Kernel)]
            SignedInt16 = 8,
            [Capability(Capability.Enumerant.Kernel)]
            SignedInt32 = 9,
            [Capability(Capability.Enumerant.Kernel)]
            UnsignedInt8 = 10,
            [Capability(Capability.Enumerant.Kernel)]
            UnsignedInt16 = 11,
            [Capability(Capability.Enumerant.Kernel)]
            UnsignedInt32 = 12,
            [Capability(Capability.Enumerant.Kernel)]
            HalfFloat = 13,
            [Capability(Capability.Enumerant.Kernel)]
            Float = 14,
            [Capability(Capability.Enumerant.Kernel)]
            UnormInt24 = 15,
            [Capability(Capability.Enumerant.Kernel)]
            UnormInt101010_2 = 16,
        }

        #region SnormInt8
        public static SnormInt8Impl SnormInt8()
        {
            return SnormInt8Impl.Instance;
            
        }

        internal class SnormInt8Impl: ImageChannelDataType
        {
            public static readonly SnormInt8Impl Instance = new SnormInt8Impl();
        
            private  SnormInt8Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.SnormInt8;
            public new static SnormInt8Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SnormInt8Impl object.</summary>
            /// <returns>A string that represents the SnormInt8Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.SnormInt8()";
            }
        }
        #endregion //SnormInt8

        #region SnormInt16
        public static SnormInt16Impl SnormInt16()
        {
            return SnormInt16Impl.Instance;
            
        }

        internal class SnormInt16Impl: ImageChannelDataType
        {
            public static readonly SnormInt16Impl Instance = new SnormInt16Impl();
        
            private  SnormInt16Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.SnormInt16;
            public new static SnormInt16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SnormInt16Impl object.</summary>
            /// <returns>A string that represents the SnormInt16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.SnormInt16()";
            }
        }
        #endregion //SnormInt16

        #region UnormInt8
        public static UnormInt8Impl UnormInt8()
        {
            return UnormInt8Impl.Instance;
            
        }

        internal class UnormInt8Impl: ImageChannelDataType
        {
            public static readonly UnormInt8Impl Instance = new UnormInt8Impl();
        
            private  UnormInt8Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.UnormInt8;
            public new static UnormInt8Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UnormInt8Impl object.</summary>
            /// <returns>A string that represents the UnormInt8Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.UnormInt8()";
            }
        }
        #endregion //UnormInt8

        #region UnormInt16
        public static UnormInt16Impl UnormInt16()
        {
            return UnormInt16Impl.Instance;
            
        }

        internal class UnormInt16Impl: ImageChannelDataType
        {
            public static readonly UnormInt16Impl Instance = new UnormInt16Impl();
        
            private  UnormInt16Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.UnormInt16;
            public new static UnormInt16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UnormInt16Impl object.</summary>
            /// <returns>A string that represents the UnormInt16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.UnormInt16()";
            }
        }
        #endregion //UnormInt16

        #region UnormShort565
        public static UnormShort565Impl UnormShort565()
        {
            return UnormShort565Impl.Instance;
            
        }

        internal class UnormShort565Impl: ImageChannelDataType
        {
            public static readonly UnormShort565Impl Instance = new UnormShort565Impl();
        
            private  UnormShort565Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.UnormShort565;
            public new static UnormShort565Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UnormShort565Impl object.</summary>
            /// <returns>A string that represents the UnormShort565Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.UnormShort565()";
            }
        }
        #endregion //UnormShort565

        #region UnormShort555
        public static UnormShort555Impl UnormShort555()
        {
            return UnormShort555Impl.Instance;
            
        }

        internal class UnormShort555Impl: ImageChannelDataType
        {
            public static readonly UnormShort555Impl Instance = new UnormShort555Impl();
        
            private  UnormShort555Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.UnormShort555;
            public new static UnormShort555Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UnormShort555Impl object.</summary>
            /// <returns>A string that represents the UnormShort555Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.UnormShort555()";
            }
        }
        #endregion //UnormShort555

        #region UnormInt101010
        public static UnormInt101010Impl UnormInt101010()
        {
            return UnormInt101010Impl.Instance;
            
        }

        internal class UnormInt101010Impl: ImageChannelDataType
        {
            public static readonly UnormInt101010Impl Instance = new UnormInt101010Impl();
        
            private  UnormInt101010Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.UnormInt101010;
            public new static UnormInt101010Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UnormInt101010Impl object.</summary>
            /// <returns>A string that represents the UnormInt101010Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.UnormInt101010()";
            }
        }
        #endregion //UnormInt101010

        #region SignedInt8
        public static SignedInt8Impl SignedInt8()
        {
            return SignedInt8Impl.Instance;
            
        }

        internal class SignedInt8Impl: ImageChannelDataType
        {
            public static readonly SignedInt8Impl Instance = new SignedInt8Impl();
        
            private  SignedInt8Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.SignedInt8;
            public new static SignedInt8Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SignedInt8Impl object.</summary>
            /// <returns>A string that represents the SignedInt8Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.SignedInt8()";
            }
        }
        #endregion //SignedInt8

        #region SignedInt16
        public static SignedInt16Impl SignedInt16()
        {
            return SignedInt16Impl.Instance;
            
        }

        internal class SignedInt16Impl: ImageChannelDataType
        {
            public static readonly SignedInt16Impl Instance = new SignedInt16Impl();
        
            private  SignedInt16Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.SignedInt16;
            public new static SignedInt16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SignedInt16Impl object.</summary>
            /// <returns>A string that represents the SignedInt16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.SignedInt16()";
            }
        }
        #endregion //SignedInt16

        #region SignedInt32
        public static SignedInt32Impl SignedInt32()
        {
            return SignedInt32Impl.Instance;
            
        }

        internal class SignedInt32Impl: ImageChannelDataType
        {
            public static readonly SignedInt32Impl Instance = new SignedInt32Impl();
        
            private  SignedInt32Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.SignedInt32;
            public new static SignedInt32Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SignedInt32Impl object.</summary>
            /// <returns>A string that represents the SignedInt32Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.SignedInt32()";
            }
        }
        #endregion //SignedInt32

        #region UnsignedInt8
        public static UnsignedInt8Impl UnsignedInt8()
        {
            return UnsignedInt8Impl.Instance;
            
        }

        internal class UnsignedInt8Impl: ImageChannelDataType
        {
            public static readonly UnsignedInt8Impl Instance = new UnsignedInt8Impl();
        
            private  UnsignedInt8Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.UnsignedInt8;
            public new static UnsignedInt8Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UnsignedInt8Impl object.</summary>
            /// <returns>A string that represents the UnsignedInt8Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.UnsignedInt8()";
            }
        }
        #endregion //UnsignedInt8

        #region UnsignedInt16
        public static UnsignedInt16Impl UnsignedInt16()
        {
            return UnsignedInt16Impl.Instance;
            
        }

        internal class UnsignedInt16Impl: ImageChannelDataType
        {
            public static readonly UnsignedInt16Impl Instance = new UnsignedInt16Impl();
        
            private  UnsignedInt16Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.UnsignedInt16;
            public new static UnsignedInt16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UnsignedInt16Impl object.</summary>
            /// <returns>A string that represents the UnsignedInt16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.UnsignedInt16()";
            }
        }
        #endregion //UnsignedInt16

        #region UnsignedInt32
        public static UnsignedInt32Impl UnsignedInt32()
        {
            return UnsignedInt32Impl.Instance;
            
        }

        internal class UnsignedInt32Impl: ImageChannelDataType
        {
            public static readonly UnsignedInt32Impl Instance = new UnsignedInt32Impl();
        
            private  UnsignedInt32Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.UnsignedInt32;
            public new static UnsignedInt32Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UnsignedInt32Impl object.</summary>
            /// <returns>A string that represents the UnsignedInt32Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.UnsignedInt32()";
            }
        }
        #endregion //UnsignedInt32

        #region HalfFloat
        public static HalfFloatImpl HalfFloat()
        {
            return HalfFloatImpl.Instance;
            
        }

        internal class HalfFloatImpl: ImageChannelDataType
        {
            public static readonly HalfFloatImpl Instance = new HalfFloatImpl();
        
            private  HalfFloatImpl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.HalfFloat;
            public new static HalfFloatImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the HalfFloatImpl object.</summary>
            /// <returns>A string that represents the HalfFloatImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.HalfFloat()";
            }
        }
        #endregion //HalfFloat

        #region Float
        public static FloatImpl Float()
        {
            return FloatImpl.Instance;
            
        }

        internal class FloatImpl: ImageChannelDataType
        {
            public static readonly FloatImpl Instance = new FloatImpl();
        
            private  FloatImpl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.Float;
            public new static FloatImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FloatImpl object.</summary>
            /// <returns>A string that represents the FloatImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.Float()";
            }
        }
        #endregion //Float

        #region UnormInt24
        public static UnormInt24Impl UnormInt24()
        {
            return UnormInt24Impl.Instance;
            
        }

        internal class UnormInt24Impl: ImageChannelDataType
        {
            public static readonly UnormInt24Impl Instance = new UnormInt24Impl();
        
            private  UnormInt24Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.UnormInt24;
            public new static UnormInt24Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UnormInt24Impl object.</summary>
            /// <returns>A string that represents the UnormInt24Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.UnormInt24()";
            }
        }
        #endregion //UnormInt24

        #region UnormInt101010_2
        public static UnormInt101010_2Impl UnormInt101010_2()
        {
            return UnormInt101010_2Impl.Instance;
            
        }

        internal class UnormInt101010_2Impl: ImageChannelDataType
        {
            public static readonly UnormInt101010_2Impl Instance = new UnormInt101010_2Impl();
        
            private  UnormInt101010_2Impl()
            {
            }
            public override Enumerant Value => ImageChannelDataType.Enumerant.UnormInt101010_2;
            public new static UnormInt101010_2Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UnormInt101010_2Impl object.</summary>
            /// <returns>A string that represents the UnormInt101010_2Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ImageChannelDataType.UnormInt101010_2()";
            }
        }
        #endregion //UnormInt101010_2

        public abstract Enumerant Value { get; }

        public static ImageChannelDataType Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.SnormInt8 :
                    return SnormInt8Impl.Parse(reader, wordCount - 1);
                case Enumerant.SnormInt16 :
                    return SnormInt16Impl.Parse(reader, wordCount - 1);
                case Enumerant.UnormInt8 :
                    return UnormInt8Impl.Parse(reader, wordCount - 1);
                case Enumerant.UnormInt16 :
                    return UnormInt16Impl.Parse(reader, wordCount - 1);
                case Enumerant.UnormShort565 :
                    return UnormShort565Impl.Parse(reader, wordCount - 1);
                case Enumerant.UnormShort555 :
                    return UnormShort555Impl.Parse(reader, wordCount - 1);
                case Enumerant.UnormInt101010 :
                    return UnormInt101010Impl.Parse(reader, wordCount - 1);
                case Enumerant.SignedInt8 :
                    return SignedInt8Impl.Parse(reader, wordCount - 1);
                case Enumerant.SignedInt16 :
                    return SignedInt16Impl.Parse(reader, wordCount - 1);
                case Enumerant.SignedInt32 :
                    return SignedInt32Impl.Parse(reader, wordCount - 1);
                case Enumerant.UnsignedInt8 :
                    return UnsignedInt8Impl.Parse(reader, wordCount - 1);
                case Enumerant.UnsignedInt16 :
                    return UnsignedInt16Impl.Parse(reader, wordCount - 1);
                case Enumerant.UnsignedInt32 :
                    return UnsignedInt32Impl.Parse(reader, wordCount - 1);
                case Enumerant.HalfFloat :
                    return HalfFloatImpl.Parse(reader, wordCount - 1);
                case Enumerant.Float :
                    return FloatImpl.Parse(reader, wordCount - 1);
                case Enumerant.UnormInt24 :
                    return UnormInt24Impl.Parse(reader, wordCount - 1);
                case Enumerant.UnormInt101010_2 :
                    return UnormInt101010_2Impl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown ImageChannelDataType "+id);
            }
        }
        
        public static ImageChannelDataType ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<ImageChannelDataType> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<ImageChannelDataType>();
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