using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class Dim : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Sampled1D)]
            [Capability(Capability.Enumerant.Image1D)]
            Dim1D = 0,
            [Capability(Capability.Enumerant.Shader)]
            [Capability(Capability.Enumerant.Kernel)]
            [Capability(Capability.Enumerant.ImageMSArray)]
            Dim2D = 1,
            Dim3D = 2,
            [Capability(Capability.Enumerant.Shader)]
            [Capability(Capability.Enumerant.ImageCubeArray)]
            Cube = 3,
            [Capability(Capability.Enumerant.SampledRect)]
            [Capability(Capability.Enumerant.ImageRect)]
            Rect = 4,
            [Capability(Capability.Enumerant.SampledBuffer)]
            [Capability(Capability.Enumerant.ImageBuffer)]
            Buffer = 5,
            [Capability(Capability.Enumerant.InputAttachment)]
            SubpassData = 6,
        }

        #region Dim1D
        public static Dim1DImpl Dim1D()
        {
            return Dim1DImpl.Instance;
            
        }

        internal class Dim1DImpl: Dim
        {
            public static readonly Dim1DImpl Instance = new Dim1DImpl();
        
            private  Dim1DImpl()
            {
            }
            public override Enumerant Value => Dim.Enumerant.Dim1D;
            public new static Dim1DImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Dim1DImpl object.</summary>
            /// <returns>A string that represents the Dim1DImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Dim.Dim1D()";
            }
        }
        #endregion //Dim1D

        #region Dim2D
        public static Dim2DImpl Dim2D()
        {
            return Dim2DImpl.Instance;
            
        }

        internal class Dim2DImpl: Dim
        {
            public static readonly Dim2DImpl Instance = new Dim2DImpl();
        
            private  Dim2DImpl()
            {
            }
            public override Enumerant Value => Dim.Enumerant.Dim2D;
            public new static Dim2DImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Dim2DImpl object.</summary>
            /// <returns>A string that represents the Dim2DImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Dim.Dim2D()";
            }
        }
        #endregion //Dim2D

        #region Dim3D
        public static Dim3DImpl Dim3D()
        {
            return Dim3DImpl.Instance;
            
        }

        internal class Dim3DImpl: Dim
        {
            public static readonly Dim3DImpl Instance = new Dim3DImpl();
        
            private  Dim3DImpl()
            {
            }
            public override Enumerant Value => Dim.Enumerant.Dim3D;
            public new static Dim3DImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Dim3DImpl object.</summary>
            /// <returns>A string that represents the Dim3DImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Dim.Dim3D()";
            }
        }
        #endregion //Dim3D

        #region Cube
        public static CubeImpl Cube()
        {
            return CubeImpl.Instance;
            
        }

        internal class CubeImpl: Dim
        {
            public static readonly CubeImpl Instance = new CubeImpl();
        
            private  CubeImpl()
            {
            }
            public override Enumerant Value => Dim.Enumerant.Cube;
            public new static CubeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CubeImpl object.</summary>
            /// <returns>A string that represents the CubeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Dim.Cube()";
            }
        }
        #endregion //Cube

        #region Rect
        public static RectImpl Rect()
        {
            return RectImpl.Instance;
            
        }

        internal class RectImpl: Dim
        {
            public static readonly RectImpl Instance = new RectImpl();
        
            private  RectImpl()
            {
            }
            public override Enumerant Value => Dim.Enumerant.Rect;
            public new static RectImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RectImpl object.</summary>
            /// <returns>A string that represents the RectImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Dim.Rect()";
            }
        }
        #endregion //Rect

        #region Buffer
        public static BufferImpl Buffer()
        {
            return BufferImpl.Instance;
            
        }

        internal class BufferImpl: Dim
        {
            public static readonly BufferImpl Instance = new BufferImpl();
        
            private  BufferImpl()
            {
            }
            public override Enumerant Value => Dim.Enumerant.Buffer;
            public new static BufferImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BufferImpl object.</summary>
            /// <returns>A string that represents the BufferImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Dim.Buffer()";
            }
        }
        #endregion //Buffer

        #region SubpassData
        public static SubpassDataImpl SubpassData()
        {
            return SubpassDataImpl.Instance;
            
        }

        internal class SubpassDataImpl: Dim
        {
            public static readonly SubpassDataImpl Instance = new SubpassDataImpl();
        
            private  SubpassDataImpl()
            {
            }
            public override Enumerant Value => Dim.Enumerant.SubpassData;
            public new static SubpassDataImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubpassDataImpl object.</summary>
            /// <returns>A string that represents the SubpassDataImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Dim.SubpassData()";
            }
        }
        #endregion //SubpassData

        public abstract Enumerant Value { get; }

        public static Dim Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Dim1D :
                    return Dim1DImpl.Parse(reader, wordCount - 1);
                case Enumerant.Dim2D :
                    return Dim2DImpl.Parse(reader, wordCount - 1);
                case Enumerant.Dim3D :
                    return Dim3DImpl.Parse(reader, wordCount - 1);
                case Enumerant.Cube :
                    return CubeImpl.Parse(reader, wordCount - 1);
                case Enumerant.Rect :
                    return RectImpl.Parse(reader, wordCount - 1);
                case Enumerant.Buffer :
                    return BufferImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubpassData :
                    return SubpassDataImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown Dim "+id);
            }
        }
        
        public static Dim ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<Dim> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<Dim>();
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