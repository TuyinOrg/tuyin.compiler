using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class Decoration : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Shader)]
            RelaxedPrecision = 0,
            [Capability(Capability.Enumerant.Shader)]
            [Capability(Capability.Enumerant.Kernel)]
            SpecId = 1,
            [Capability(Capability.Enumerant.Shader)]
            Block = 2,
            [Capability(Capability.Enumerant.Shader)]
            BufferBlock = 3,
            [Capability(Capability.Enumerant.Matrix)]
            RowMajor = 4,
            [Capability(Capability.Enumerant.Matrix)]
            ColMajor = 5,
            [Capability(Capability.Enumerant.Shader)]
            ArrayStride = 6,
            [Capability(Capability.Enumerant.Matrix)]
            MatrixStride = 7,
            [Capability(Capability.Enumerant.Shader)]
            GLSLShared = 8,
            [Capability(Capability.Enumerant.Shader)]
            GLSLPacked = 9,
            [Capability(Capability.Enumerant.Kernel)]
            CPacked = 10,
            BuiltIn = 11,
            [Capability(Capability.Enumerant.Shader)]
            NoPerspective = 13,
            [Capability(Capability.Enumerant.Shader)]
            Flat = 14,
            [Capability(Capability.Enumerant.Tessellation)]
            Patch = 15,
            [Capability(Capability.Enumerant.Shader)]
            Centroid = 16,
            [Capability(Capability.Enumerant.SampleRateShading)]
            Sample = 17,
            [Capability(Capability.Enumerant.Shader)]
            Invariant = 18,
            Restrict = 19,
            Aliased = 20,
            Volatile = 21,
            [Capability(Capability.Enumerant.Kernel)]
            Constant = 22,
            Coherent = 23,
            NonWritable = 24,
            NonReadable = 25,
            [Capability(Capability.Enumerant.Shader)]
            Uniform = 26,
            [Capability(Capability.Enumerant.Shader)]
            UniformId = 27,
            [Capability(Capability.Enumerant.Kernel)]
            SaturatedConversion = 28,
            [Capability(Capability.Enumerant.GeometryStreams)]
            Stream = 29,
            [Capability(Capability.Enumerant.Shader)]
            Location = 30,
            [Capability(Capability.Enumerant.Shader)]
            Component = 31,
            [Capability(Capability.Enumerant.Shader)]
            Index = 32,
            [Capability(Capability.Enumerant.Shader)]
            Binding = 33,
            [Capability(Capability.Enumerant.Shader)]
            DescriptorSet = 34,
            [Capability(Capability.Enumerant.Shader)]
            Offset = 35,
            [Capability(Capability.Enumerant.TransformFeedback)]
            XfbBuffer = 36,
            [Capability(Capability.Enumerant.TransformFeedback)]
            XfbStride = 37,
            [Capability(Capability.Enumerant.Kernel)]
            FuncParamAttr = 38,
            FPRoundingMode = 39,
            [Capability(Capability.Enumerant.Kernel)]
            FPFastMathMode = 40,
            [Capability(Capability.Enumerant.Linkage)]
            LinkageAttributes = 41,
            [Capability(Capability.Enumerant.Shader)]
            NoContraction = 42,
            [Capability(Capability.Enumerant.InputAttachment)]
            InputAttachmentIndex = 43,
            [Capability(Capability.Enumerant.Kernel)]
            Alignment = 44,
            [Capability(Capability.Enumerant.Addresses)]
            MaxByteOffset = 45,
            [Capability(Capability.Enumerant.Kernel)]
            AlignmentId = 46,
            [Capability(Capability.Enumerant.Addresses)]
            MaxByteOffsetId = 47,
            NoSignedWrap = 4469,
            NoUnsignedWrap = 4470,
            ExplicitInterpAMD = 4999,
            [Capability(Capability.Enumerant.SampleMaskOverrideCoverageNV)]
            OverrideCoverageNV = 5248,
            [Capability(Capability.Enumerant.GeometryShaderPassthroughNV)]
            PassthroughNV = 5250,
            [Capability(Capability.Enumerant.ShaderViewportMaskNV)]
            ViewportRelativeNV = 5252,
            [Capability(Capability.Enumerant.ShaderStereoViewNV)]
            SecondaryViewportRelativeNV = 5256,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            PerPrimitiveNV = 5271,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            PerViewNV = 5272,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            PerTaskNV = 5273,
            [Capability(Capability.Enumerant.FragmentBarycentricNV)]
            PerVertexNV = 5285,
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            NonUniform = 5300,
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            NonUniformEXT = 5300,
            [Capability(Capability.Enumerant.PhysicalStorageBufferAddresses)]
            RestrictPointer = 5355,
            [Capability(Capability.Enumerant.PhysicalStorageBufferAddresses)]
            RestrictPointerEXT = 5355,
            [Capability(Capability.Enumerant.PhysicalStorageBufferAddresses)]
            AliasedPointer = 5356,
            [Capability(Capability.Enumerant.PhysicalStorageBufferAddresses)]
            AliasedPointerEXT = 5356,
            CounterBuffer = 5634,
            HlslCounterBufferGOOGLE = 5634,
            UserSemantic = 5635,
            HlslSemanticGOOGLE = 5635,
            UserTypeGOOGLE = 5636,
        }

        #region RelaxedPrecision
        public static RelaxedPrecisionImpl RelaxedPrecision()
        {
            return RelaxedPrecisionImpl.Instance;
            
        }

        internal class RelaxedPrecisionImpl: Decoration
        {
            public static readonly RelaxedPrecisionImpl Instance = new RelaxedPrecisionImpl();
        
            private  RelaxedPrecisionImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.RelaxedPrecision;
            public new static RelaxedPrecisionImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RelaxedPrecisionImpl object.</summary>
            /// <returns>A string that represents the RelaxedPrecisionImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.RelaxedPrecision()";
            }
        }
        #endregion //RelaxedPrecision

        #region SpecId
        public static SpecIdImpl SpecId(uint specializationConstantID)
        {
            return new SpecIdImpl(specializationConstantID);
            
        }

        internal class SpecIdImpl: Decoration
        {
            public SpecIdImpl()
            {
            }

            public SpecIdImpl(uint specializationConstantID)
            {
                this.SpecializationConstantID = specializationConstantID;
            }
            public override Enumerant Value => Decoration.Enumerant.SpecId;
            public uint SpecializationConstantID { get; set; }
            public new static SpecIdImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new SpecIdImpl();
                res.SpecializationConstantID = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += SpecializationConstantID.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                SpecializationConstantID.Write(writer);
            }

            /// <summary>Returns a string that represents the SpecIdImpl object.</summary>
            /// <returns>A string that represents the SpecIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.SpecId({SpecializationConstantID})";
            }
        }
        #endregion //SpecId

        #region Block
        public static BlockImpl Block()
        {
            return BlockImpl.Instance;
            
        }

        internal class BlockImpl: Decoration
        {
            public static readonly BlockImpl Instance = new BlockImpl();
        
            private  BlockImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.Block;
            public new static BlockImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BlockImpl object.</summary>
            /// <returns>A string that represents the BlockImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Block()";
            }
        }
        #endregion //Block

        #region BufferBlock
        public static BufferBlockImpl BufferBlock()
        {
            return BufferBlockImpl.Instance;
            
        }

        internal class BufferBlockImpl: Decoration
        {
            public static readonly BufferBlockImpl Instance = new BufferBlockImpl();
        
            private  BufferBlockImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.BufferBlock;
            public new static BufferBlockImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BufferBlockImpl object.</summary>
            /// <returns>A string that represents the BufferBlockImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.BufferBlock()";
            }
        }
        #endregion //BufferBlock

        #region RowMajor
        public static RowMajorImpl RowMajor()
        {
            return RowMajorImpl.Instance;
            
        }

        internal class RowMajorImpl: Decoration
        {
            public static readonly RowMajorImpl Instance = new RowMajorImpl();
        
            private  RowMajorImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.RowMajor;
            public new static RowMajorImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RowMajorImpl object.</summary>
            /// <returns>A string that represents the RowMajorImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.RowMajor()";
            }
        }
        #endregion //RowMajor

        #region ColMajor
        public static ColMajorImpl ColMajor()
        {
            return ColMajorImpl.Instance;
            
        }

        internal class ColMajorImpl: Decoration
        {
            public static readonly ColMajorImpl Instance = new ColMajorImpl();
        
            private  ColMajorImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.ColMajor;
            public new static ColMajorImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ColMajorImpl object.</summary>
            /// <returns>A string that represents the ColMajorImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.ColMajor()";
            }
        }
        #endregion //ColMajor

        #region ArrayStride
        public static ArrayStrideImpl ArrayStride(uint arrayStride)
        {
            return new ArrayStrideImpl(arrayStride);
            
        }

        internal class ArrayStrideImpl: Decoration
        {
            public ArrayStrideImpl()
            {
            }

            public ArrayStrideImpl(uint arrayStride)
            {
                this.ArrayStride = arrayStride;
            }
            public override Enumerant Value => Decoration.Enumerant.ArrayStride;
            public new uint ArrayStride { get; set; }
            public new static ArrayStrideImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new ArrayStrideImpl();
                res.ArrayStride = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += ArrayStride.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                ArrayStride.Write(writer);
            }

            /// <summary>Returns a string that represents the ArrayStrideImpl object.</summary>
            /// <returns>A string that represents the ArrayStrideImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.ArrayStride({ArrayStride})";
            }
        }
        #endregion //ArrayStride

        #region MatrixStride
        public static MatrixStrideImpl MatrixStride(uint matrixStride)
        {
            return new MatrixStrideImpl(matrixStride);
            
        }

        internal class MatrixStrideImpl: Decoration
        {
            public MatrixStrideImpl()
            {
            }

            public MatrixStrideImpl(uint matrixStride)
            {
                this.MatrixStride = matrixStride;
            }
            public override Enumerant Value => Decoration.Enumerant.MatrixStride;
            public new uint MatrixStride { get; set; }
            public new static MatrixStrideImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new MatrixStrideImpl();
                res.MatrixStride = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += MatrixStride.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                MatrixStride.Write(writer);
            }

            /// <summary>Returns a string that represents the MatrixStrideImpl object.</summary>
            /// <returns>A string that represents the MatrixStrideImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.MatrixStride({MatrixStride})";
            }
        }
        #endregion //MatrixStride

        #region GLSLShared
        public static GLSLSharedImpl GLSLShared()
        {
            return GLSLSharedImpl.Instance;
            
        }

        internal class GLSLSharedImpl: Decoration
        {
            public static readonly GLSLSharedImpl Instance = new GLSLSharedImpl();
        
            private  GLSLSharedImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.GLSLShared;
            public new static GLSLSharedImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GLSLSharedImpl object.</summary>
            /// <returns>A string that represents the GLSLSharedImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.GLSLShared()";
            }
        }
        #endregion //GLSLShared

        #region GLSLPacked
        public static GLSLPackedImpl GLSLPacked()
        {
            return GLSLPackedImpl.Instance;
            
        }

        internal class GLSLPackedImpl: Decoration
        {
            public static readonly GLSLPackedImpl Instance = new GLSLPackedImpl();
        
            private  GLSLPackedImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.GLSLPacked;
            public new static GLSLPackedImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GLSLPackedImpl object.</summary>
            /// <returns>A string that represents the GLSLPackedImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.GLSLPacked()";
            }
        }
        #endregion //GLSLPacked

        #region CPacked
        public static CPackedImpl CPacked()
        {
            return CPackedImpl.Instance;
            
        }

        internal class CPackedImpl: Decoration
        {
            public static readonly CPackedImpl Instance = new CPackedImpl();
        
            private  CPackedImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.CPacked;
            public new static CPackedImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CPackedImpl object.</summary>
            /// <returns>A string that represents the CPackedImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.CPacked()";
            }
        }
        #endregion //CPacked

        #region BuiltIn
        public static BuiltInImpl BuiltIn(Spv.BuiltIn builtIn)
        {
            return new BuiltInImpl(builtIn);
            
        }

        internal class BuiltInImpl: Decoration
        {
            public BuiltInImpl()
            {
            }

            public BuiltInImpl(Spv.BuiltIn builtIn)
            {
                this.BuiltIn = builtIn;
            }
            public override Enumerant Value => Decoration.Enumerant.BuiltIn;
            public new Spv.BuiltIn BuiltIn { get; set; }
            public new static BuiltInImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new BuiltInImpl();
                res.BuiltIn = Spv.BuiltIn.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += BuiltIn.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                BuiltIn.Write(writer);
            }

            /// <summary>Returns a string that represents the BuiltInImpl object.</summary>
            /// <returns>A string that represents the BuiltInImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.BuiltIn({BuiltIn})";
            }
        }
        #endregion //BuiltIn

        #region NoPerspective
        public static NoPerspectiveImpl NoPerspective()
        {
            return NoPerspectiveImpl.Instance;
            
        }

        internal class NoPerspectiveImpl: Decoration
        {
            public static readonly NoPerspectiveImpl Instance = new NoPerspectiveImpl();
        
            private  NoPerspectiveImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.NoPerspective;
            public new static NoPerspectiveImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NoPerspectiveImpl object.</summary>
            /// <returns>A string that represents the NoPerspectiveImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.NoPerspective()";
            }
        }
        #endregion //NoPerspective

        #region Flat
        public static FlatImpl Flat()
        {
            return FlatImpl.Instance;
            
        }

        internal class FlatImpl: Decoration
        {
            public static readonly FlatImpl Instance = new FlatImpl();
        
            private  FlatImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.Flat;
            public new static FlatImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FlatImpl object.</summary>
            /// <returns>A string that represents the FlatImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Flat()";
            }
        }
        #endregion //Flat

        #region Patch
        public static PatchImpl Patch()
        {
            return PatchImpl.Instance;
            
        }

        internal class PatchImpl: Decoration
        {
            public static readonly PatchImpl Instance = new PatchImpl();
        
            private  PatchImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.Patch;
            public new static PatchImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PatchImpl object.</summary>
            /// <returns>A string that represents the PatchImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Patch()";
            }
        }
        #endregion //Patch

        #region Centroid
        public static CentroidImpl Centroid()
        {
            return CentroidImpl.Instance;
            
        }

        internal class CentroidImpl: Decoration
        {
            public static readonly CentroidImpl Instance = new CentroidImpl();
        
            private  CentroidImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.Centroid;
            public new static CentroidImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CentroidImpl object.</summary>
            /// <returns>A string that represents the CentroidImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Centroid()";
            }
        }
        #endregion //Centroid

        #region Sample
        public static SampleImpl Sample()
        {
            return SampleImpl.Instance;
            
        }

        internal class SampleImpl: Decoration
        {
            public static readonly SampleImpl Instance = new SampleImpl();
        
            private  SampleImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.Sample;
            public new static SampleImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampleImpl object.</summary>
            /// <returns>A string that represents the SampleImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Sample()";
            }
        }
        #endregion //Sample

        #region Invariant
        public static InvariantImpl Invariant()
        {
            return InvariantImpl.Instance;
            
        }

        internal class InvariantImpl: Decoration
        {
            public static readonly InvariantImpl Instance = new InvariantImpl();
        
            private  InvariantImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.Invariant;
            public new static InvariantImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InvariantImpl object.</summary>
            /// <returns>A string that represents the InvariantImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Invariant()";
            }
        }
        #endregion //Invariant

        #region Restrict
        public static RestrictImpl Restrict()
        {
            return RestrictImpl.Instance;
            
        }

        internal class RestrictImpl: Decoration
        {
            public static readonly RestrictImpl Instance = new RestrictImpl();
        
            private  RestrictImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.Restrict;
            public new static RestrictImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RestrictImpl object.</summary>
            /// <returns>A string that represents the RestrictImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Restrict()";
            }
        }
        #endregion //Restrict

        #region Aliased
        public static AliasedImpl Aliased()
        {
            return AliasedImpl.Instance;
            
        }

        internal class AliasedImpl: Decoration
        {
            public static readonly AliasedImpl Instance = new AliasedImpl();
        
            private  AliasedImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.Aliased;
            public new static AliasedImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the AliasedImpl object.</summary>
            /// <returns>A string that represents the AliasedImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Aliased()";
            }
        }
        #endregion //Aliased

        #region Volatile
        public static VolatileImpl Volatile()
        {
            return VolatileImpl.Instance;
            
        }

        internal class VolatileImpl: Decoration
        {
            public static readonly VolatileImpl Instance = new VolatileImpl();
        
            private  VolatileImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.Volatile;
            public new static VolatileImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VolatileImpl object.</summary>
            /// <returns>A string that represents the VolatileImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Volatile()";
            }
        }
        #endregion //Volatile

        #region Constant
        public static ConstantImpl Constant()
        {
            return ConstantImpl.Instance;
            
        }

        internal class ConstantImpl: Decoration
        {
            public static readonly ConstantImpl Instance = new ConstantImpl();
        
            private  ConstantImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.Constant;
            public new static ConstantImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ConstantImpl object.</summary>
            /// <returns>A string that represents the ConstantImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Constant()";
            }
        }
        #endregion //Constant

        #region Coherent
        public static CoherentImpl Coherent()
        {
            return CoherentImpl.Instance;
            
        }

        internal class CoherentImpl: Decoration
        {
            public static readonly CoherentImpl Instance = new CoherentImpl();
        
            private  CoherentImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.Coherent;
            public new static CoherentImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CoherentImpl object.</summary>
            /// <returns>A string that represents the CoherentImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Coherent()";
            }
        }
        #endregion //Coherent

        #region NonWritable
        public static NonWritableImpl NonWritable()
        {
            return NonWritableImpl.Instance;
            
        }

        internal class NonWritableImpl: Decoration
        {
            public static readonly NonWritableImpl Instance = new NonWritableImpl();
        
            private  NonWritableImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.NonWritable;
            public new static NonWritableImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NonWritableImpl object.</summary>
            /// <returns>A string that represents the NonWritableImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.NonWritable()";
            }
        }
        #endregion //NonWritable

        #region NonReadable
        public static NonReadableImpl NonReadable()
        {
            return NonReadableImpl.Instance;
            
        }

        internal class NonReadableImpl: Decoration
        {
            public static readonly NonReadableImpl Instance = new NonReadableImpl();
        
            private  NonReadableImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.NonReadable;
            public new static NonReadableImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NonReadableImpl object.</summary>
            /// <returns>A string that represents the NonReadableImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.NonReadable()";
            }
        }
        #endregion //NonReadable

        #region Uniform
        public static UniformImpl Uniform()
        {
            return UniformImpl.Instance;
            
        }

        internal class UniformImpl: Decoration
        {
            public static readonly UniformImpl Instance = new UniformImpl();
        
            private  UniformImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.Uniform;
            public new static UniformImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UniformImpl object.</summary>
            /// <returns>A string that represents the UniformImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Uniform()";
            }
        }
        #endregion //Uniform

        #region UniformId
        public static UniformIdImpl UniformId(uint execution)
        {
            return new UniformIdImpl(execution);
            
        }

        internal class UniformIdImpl: Decoration
        {
            public UniformIdImpl()
            {
            }

            public UniformIdImpl(uint execution)
            {
                this.Execution = execution;
            }
            public override Enumerant Value => Decoration.Enumerant.UniformId;
            public uint Execution { get; set; }
            public new static UniformIdImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new UniformIdImpl();
                res.Execution = Spv.IdScope.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Execution.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Execution.Write(writer);
            }

            /// <summary>Returns a string that represents the UniformIdImpl object.</summary>
            /// <returns>A string that represents the UniformIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.UniformId({Execution})";
            }
        }
        #endregion //UniformId

        #region SaturatedConversion
        public static SaturatedConversionImpl SaturatedConversion()
        {
            return SaturatedConversionImpl.Instance;
            
        }

        internal class SaturatedConversionImpl: Decoration
        {
            public static readonly SaturatedConversionImpl Instance = new SaturatedConversionImpl();
        
            private  SaturatedConversionImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.SaturatedConversion;
            public new static SaturatedConversionImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SaturatedConversionImpl object.</summary>
            /// <returns>A string that represents the SaturatedConversionImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.SaturatedConversion()";
            }
        }
        #endregion //SaturatedConversion

        #region Stream
        public static StreamImpl Stream(uint streamNumber)
        {
            return new StreamImpl(streamNumber);
            
        }

        internal class StreamImpl: Decoration
        {
            public StreamImpl()
            {
            }

            public StreamImpl(uint streamNumber)
            {
                this.StreamNumber = streamNumber;
            }
            public override Enumerant Value => Decoration.Enumerant.Stream;
            public uint StreamNumber { get; set; }
            public new static StreamImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new StreamImpl();
                res.StreamNumber = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += StreamNumber.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                StreamNumber.Write(writer);
            }

            /// <summary>Returns a string that represents the StreamImpl object.</summary>
            /// <returns>A string that represents the StreamImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Stream({StreamNumber})";
            }
        }
        #endregion //Stream

        #region Location
        public static LocationImpl Location(uint location)
        {
            return new LocationImpl(location);
            
        }

        internal class LocationImpl: Decoration
        {
            public LocationImpl()
            {
            }

            public LocationImpl(uint location)
            {
                this.Location = location;
            }
            public override Enumerant Value => Decoration.Enumerant.Location;
            public new uint Location { get; set; }
            public new static LocationImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new LocationImpl();
                res.Location = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Location.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Location.Write(writer);
            }

            /// <summary>Returns a string that represents the LocationImpl object.</summary>
            /// <returns>A string that represents the LocationImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Location({Location})";
            }
        }
        #endregion //Location

        #region Component
        public static ComponentImpl Component(uint component)
        {
            return new ComponentImpl(component);
            
        }

        internal class ComponentImpl: Decoration
        {
            public ComponentImpl()
            {
            }

            public ComponentImpl(uint component)
            {
                this.Component = component;
            }
            public override Enumerant Value => Decoration.Enumerant.Component;
            public new uint Component { get; set; }
            public new static ComponentImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new ComponentImpl();
                res.Component = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Component.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Component.Write(writer);
            }

            /// <summary>Returns a string that represents the ComponentImpl object.</summary>
            /// <returns>A string that represents the ComponentImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Component({Component})";
            }
        }
        #endregion //Component

        #region Index
        public static IndexImpl Index(uint index)
        {
            return new IndexImpl(index);
            
        }

        internal class IndexImpl: Decoration
        {
            public IndexImpl()
            {
            }

            public IndexImpl(uint index)
            {
                this.Index = index;
            }
            public override Enumerant Value => Decoration.Enumerant.Index;
            public new uint Index { get; set; }
            public new static IndexImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new IndexImpl();
                res.Index = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Index.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Index.Write(writer);
            }

            /// <summary>Returns a string that represents the IndexImpl object.</summary>
            /// <returns>A string that represents the IndexImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Index({Index})";
            }
        }
        #endregion //Index

        #region Binding
        public static BindingImpl Binding(uint bindingPoint)
        {
            return new BindingImpl(bindingPoint);
            
        }

        internal class BindingImpl: Decoration
        {
            public BindingImpl()
            {
            }

            public BindingImpl(uint bindingPoint)
            {
                this.BindingPoint = bindingPoint;
            }
            public override Enumerant Value => Decoration.Enumerant.Binding;
            public uint BindingPoint { get; set; }
            public new static BindingImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new BindingImpl();
                res.BindingPoint = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += BindingPoint.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                BindingPoint.Write(writer);
            }

            /// <summary>Returns a string that represents the BindingImpl object.</summary>
            /// <returns>A string that represents the BindingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Binding({BindingPoint})";
            }
        }
        #endregion //Binding

        #region DescriptorSet
        public static DescriptorSetImpl DescriptorSet(uint descriptorSet)
        {
            return new DescriptorSetImpl(descriptorSet);
            
        }

        internal class DescriptorSetImpl: Decoration
        {
            public DescriptorSetImpl()
            {
            }

            public DescriptorSetImpl(uint descriptorSet)
            {
                this.DescriptorSet = descriptorSet;
            }
            public override Enumerant Value => Decoration.Enumerant.DescriptorSet;
            public new uint DescriptorSet { get; set; }
            public new static DescriptorSetImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new DescriptorSetImpl();
                res.DescriptorSet = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += DescriptorSet.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                DescriptorSet.Write(writer);
            }

            /// <summary>Returns a string that represents the DescriptorSetImpl object.</summary>
            /// <returns>A string that represents the DescriptorSetImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.DescriptorSet({DescriptorSet})";
            }
        }
        #endregion //DescriptorSet

        #region Offset
        public static OffsetImpl Offset(uint byteOffset)
        {
            return new OffsetImpl(byteOffset);
            
        }

        internal class OffsetImpl: Decoration
        {
            public OffsetImpl()
            {
            }

            public OffsetImpl(uint byteOffset)
            {
                this.ByteOffset = byteOffset;
            }
            public override Enumerant Value => Decoration.Enumerant.Offset;
            public uint ByteOffset { get; set; }
            public new static OffsetImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new OffsetImpl();
                res.ByteOffset = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += ByteOffset.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                ByteOffset.Write(writer);
            }

            /// <summary>Returns a string that represents the OffsetImpl object.</summary>
            /// <returns>A string that represents the OffsetImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Offset({ByteOffset})";
            }
        }
        #endregion //Offset

        #region XfbBuffer
        public static XfbBufferImpl XfbBuffer(uint xFBBufferNumber)
        {
            return new XfbBufferImpl(xFBBufferNumber);
            
        }

        internal class XfbBufferImpl: Decoration
        {
            public XfbBufferImpl()
            {
            }

            public XfbBufferImpl(uint xFBBufferNumber)
            {
                this.XFBBufferNumber = xFBBufferNumber;
            }
            public override Enumerant Value => Decoration.Enumerant.XfbBuffer;
            public uint XFBBufferNumber { get; set; }
            public new static XfbBufferImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new XfbBufferImpl();
                res.XFBBufferNumber = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += XFBBufferNumber.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                XFBBufferNumber.Write(writer);
            }

            /// <summary>Returns a string that represents the XfbBufferImpl object.</summary>
            /// <returns>A string that represents the XfbBufferImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.XfbBuffer({XFBBufferNumber})";
            }
        }
        #endregion //XfbBuffer

        #region XfbStride
        public static XfbStrideImpl XfbStride(uint xFBStride)
        {
            return new XfbStrideImpl(xFBStride);
            
        }

        internal class XfbStrideImpl: Decoration
        {
            public XfbStrideImpl()
            {
            }

            public XfbStrideImpl(uint xFBStride)
            {
                this.XFBStride = xFBStride;
            }
            public override Enumerant Value => Decoration.Enumerant.XfbStride;
            public uint XFBStride { get; set; }
            public new static XfbStrideImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new XfbStrideImpl();
                res.XFBStride = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += XFBStride.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                XFBStride.Write(writer);
            }

            /// <summary>Returns a string that represents the XfbStrideImpl object.</summary>
            /// <returns>A string that represents the XfbStrideImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.XfbStride({XFBStride})";
            }
        }
        #endregion //XfbStride

        #region FuncParamAttr
        public static FuncParamAttrImpl FuncParamAttr(Spv.FunctionParameterAttribute functionParameterAttribute)
        {
            return new FuncParamAttrImpl(functionParameterAttribute);
            
        }

        internal class FuncParamAttrImpl: Decoration
        {
            public FuncParamAttrImpl()
            {
            }

            public FuncParamAttrImpl(Spv.FunctionParameterAttribute functionParameterAttribute)
            {
                this.FunctionParameterAttribute = functionParameterAttribute;
            }
            public override Enumerant Value => Decoration.Enumerant.FuncParamAttr;
            public Spv.FunctionParameterAttribute FunctionParameterAttribute { get; set; }
            public new static FuncParamAttrImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new FuncParamAttrImpl();
                res.FunctionParameterAttribute = Spv.FunctionParameterAttribute.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += FunctionParameterAttribute.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                FunctionParameterAttribute.Write(writer);
            }

            /// <summary>Returns a string that represents the FuncParamAttrImpl object.</summary>
            /// <returns>A string that represents the FuncParamAttrImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.FuncParamAttr({FunctionParameterAttribute})";
            }
        }
        #endregion //FuncParamAttr

        #region FPRoundingMode
        public static FPRoundingModeImpl FPRoundingMode(Spv.FPRoundingMode floatingPointRoundingMode)
        {
            return new FPRoundingModeImpl(floatingPointRoundingMode);
            
        }

        internal class FPRoundingModeImpl: Decoration
        {
            public FPRoundingModeImpl()
            {
            }

            public FPRoundingModeImpl(Spv.FPRoundingMode floatingPointRoundingMode)
            {
                this.FloatingPointRoundingMode = floatingPointRoundingMode;
            }
            public override Enumerant Value => Decoration.Enumerant.FPRoundingMode;
            public Spv.FPRoundingMode FloatingPointRoundingMode { get; set; }
            public new static FPRoundingModeImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new FPRoundingModeImpl();
                res.FloatingPointRoundingMode = Spv.FPRoundingMode.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += FloatingPointRoundingMode.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                FloatingPointRoundingMode.Write(writer);
            }

            /// <summary>Returns a string that represents the FPRoundingModeImpl object.</summary>
            /// <returns>A string that represents the FPRoundingModeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.FPRoundingMode({FloatingPointRoundingMode})";
            }
        }
        #endregion //FPRoundingMode

        #region FPFastMathMode
        public static FPFastMathModeImpl FPFastMathMode(Spv.FPFastMathMode fastMathMode)
        {
            return new FPFastMathModeImpl(fastMathMode);
            
        }

        internal class FPFastMathModeImpl: Decoration
        {
            public FPFastMathModeImpl()
            {
            }

            public FPFastMathModeImpl(Spv.FPFastMathMode fastMathMode)
            {
                this.FastMathMode = fastMathMode;
            }
            public override Enumerant Value => Decoration.Enumerant.FPFastMathMode;
            public Spv.FPFastMathMode FastMathMode { get; set; }
            public new static FPFastMathModeImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new FPFastMathModeImpl();
                res.FastMathMode = Spv.FPFastMathMode.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += FastMathMode.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                FastMathMode.Write(writer);
            }

            /// <summary>Returns a string that represents the FPFastMathModeImpl object.</summary>
            /// <returns>A string that represents the FPFastMathModeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.FPFastMathMode({FastMathMode})";
            }
        }
        #endregion //FPFastMathMode

        #region LinkageAttributes
        public static LinkageAttributesImpl LinkageAttributes(string name, Spv.LinkageType linkageType)
        {
            return new LinkageAttributesImpl(name, linkageType);
            
        }

        internal class LinkageAttributesImpl: Decoration
        {
            public LinkageAttributesImpl()
            {
            }

            public LinkageAttributesImpl(string name, Spv.LinkageType linkageType)
            {
                this.Name = name;
                this.LinkageType = linkageType;
            }
            public override Enumerant Value => Decoration.Enumerant.LinkageAttributes;
            public string Name { get; set; }
            public Spv.LinkageType LinkageType { get; set; }
            public new static LinkageAttributesImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new LinkageAttributesImpl();
                res.Name = Spv.LiteralString.Parse(reader, end-reader.Position);
                res.LinkageType = Spv.LinkageType.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Name.GetWordCount();
                wordCount += LinkageType.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Name.Write(writer);
                LinkageType.Write(writer);
            }

            /// <summary>Returns a string that represents the LinkageAttributesImpl object.</summary>
            /// <returns>A string that represents the LinkageAttributesImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.LinkageAttributes({Name}, {LinkageType})";
            }
        }
        #endregion //LinkageAttributes

        #region NoContraction
        public static NoContractionImpl NoContraction()
        {
            return NoContractionImpl.Instance;
            
        }

        internal class NoContractionImpl: Decoration
        {
            public static readonly NoContractionImpl Instance = new NoContractionImpl();
        
            private  NoContractionImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.NoContraction;
            public new static NoContractionImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NoContractionImpl object.</summary>
            /// <returns>A string that represents the NoContractionImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.NoContraction()";
            }
        }
        #endregion //NoContraction

        #region InputAttachmentIndex
        public static InputAttachmentIndexImpl InputAttachmentIndex(uint attachmentIndex)
        {
            return new InputAttachmentIndexImpl(attachmentIndex);
            
        }

        internal class InputAttachmentIndexImpl: Decoration
        {
            public InputAttachmentIndexImpl()
            {
            }

            public InputAttachmentIndexImpl(uint attachmentIndex)
            {
                this.AttachmentIndex = attachmentIndex;
            }
            public override Enumerant Value => Decoration.Enumerant.InputAttachmentIndex;
            public uint AttachmentIndex { get; set; }
            public new static InputAttachmentIndexImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new InputAttachmentIndexImpl();
                res.AttachmentIndex = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += AttachmentIndex.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                AttachmentIndex.Write(writer);
            }

            /// <summary>Returns a string that represents the InputAttachmentIndexImpl object.</summary>
            /// <returns>A string that represents the InputAttachmentIndexImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.InputAttachmentIndex({AttachmentIndex})";
            }
        }
        #endregion //InputAttachmentIndex

        #region Alignment
        public static AlignmentImpl Alignment(uint alignment)
        {
            return new AlignmentImpl(alignment);
            
        }

        internal class AlignmentImpl: Decoration
        {
            public AlignmentImpl()
            {
            }

            public AlignmentImpl(uint alignment)
            {
                this.Alignment = alignment;
            }
            public override Enumerant Value => Decoration.Enumerant.Alignment;
            public new uint Alignment { get; set; }
            public new static AlignmentImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new AlignmentImpl();
                res.Alignment = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Alignment.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Alignment.Write(writer);
            }

            /// <summary>Returns a string that represents the AlignmentImpl object.</summary>
            /// <returns>A string that represents the AlignmentImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.Alignment({Alignment})";
            }
        }
        #endregion //Alignment

        #region MaxByteOffset
        public static MaxByteOffsetImpl MaxByteOffset(uint maxByteOffset)
        {
            return new MaxByteOffsetImpl(maxByteOffset);
            
        }

        internal class MaxByteOffsetImpl: Decoration
        {
            public MaxByteOffsetImpl()
            {
            }

            public MaxByteOffsetImpl(uint maxByteOffset)
            {
                this.MaxByteOffset = maxByteOffset;
            }
            public override Enumerant Value => Decoration.Enumerant.MaxByteOffset;
            public new uint MaxByteOffset { get; set; }
            public new static MaxByteOffsetImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new MaxByteOffsetImpl();
                res.MaxByteOffset = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += MaxByteOffset.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                MaxByteOffset.Write(writer);
            }

            /// <summary>Returns a string that represents the MaxByteOffsetImpl object.</summary>
            /// <returns>A string that represents the MaxByteOffsetImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.MaxByteOffset({MaxByteOffset})";
            }
        }
        #endregion //MaxByteOffset

        #region AlignmentId
        public static AlignmentIdImpl AlignmentId(Spv.IdRef alignment)
        {
            return new AlignmentIdImpl(alignment);
            
        }

        internal class AlignmentIdImpl: Decoration
        {
            public AlignmentIdImpl()
            {
            }

            public AlignmentIdImpl(Spv.IdRef alignment)
            {
                this.Alignment = alignment;
            }
            public override Enumerant Value => Decoration.Enumerant.AlignmentId;
            public Spv.IdRef Alignment { get; set; }
            public new static AlignmentIdImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new AlignmentIdImpl();
                res.Alignment = Spv.IdRef.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Alignment.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Alignment.Write(writer);
            }

            /// <summary>Returns a string that represents the AlignmentIdImpl object.</summary>
            /// <returns>A string that represents the AlignmentIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.AlignmentId({Alignment})";
            }
        }
        #endregion //AlignmentId

        #region MaxByteOffsetId
        public static MaxByteOffsetIdImpl MaxByteOffsetId(Spv.IdRef maxByteOffset)
        {
            return new MaxByteOffsetIdImpl(maxByteOffset);
            
        }

        internal class MaxByteOffsetIdImpl: Decoration
        {
            public MaxByteOffsetIdImpl()
            {
            }

            public MaxByteOffsetIdImpl(Spv.IdRef maxByteOffset)
            {
                this.MaxByteOffset = maxByteOffset;
            }
            public override Enumerant Value => Decoration.Enumerant.MaxByteOffsetId;
            public Spv.IdRef MaxByteOffset { get; set; }
            public new static MaxByteOffsetIdImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new MaxByteOffsetIdImpl();
                res.MaxByteOffset = Spv.IdRef.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += MaxByteOffset.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                MaxByteOffset.Write(writer);
            }

            /// <summary>Returns a string that represents the MaxByteOffsetIdImpl object.</summary>
            /// <returns>A string that represents the MaxByteOffsetIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.MaxByteOffsetId({MaxByteOffset})";
            }
        }
        #endregion //MaxByteOffsetId

        #region NoSignedWrap
        public static NoSignedWrapImpl NoSignedWrap()
        {
            return NoSignedWrapImpl.Instance;
            
        }

        internal class NoSignedWrapImpl: Decoration
        {
            public static readonly NoSignedWrapImpl Instance = new NoSignedWrapImpl();
        
            private  NoSignedWrapImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.NoSignedWrap;
            public new static NoSignedWrapImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NoSignedWrapImpl object.</summary>
            /// <returns>A string that represents the NoSignedWrapImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.NoSignedWrap()";
            }
        }
        #endregion //NoSignedWrap

        #region NoUnsignedWrap
        public static NoUnsignedWrapImpl NoUnsignedWrap()
        {
            return NoUnsignedWrapImpl.Instance;
            
        }

        internal class NoUnsignedWrapImpl: Decoration
        {
            public static readonly NoUnsignedWrapImpl Instance = new NoUnsignedWrapImpl();
        
            private  NoUnsignedWrapImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.NoUnsignedWrap;
            public new static NoUnsignedWrapImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NoUnsignedWrapImpl object.</summary>
            /// <returns>A string that represents the NoUnsignedWrapImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.NoUnsignedWrap()";
            }
        }
        #endregion //NoUnsignedWrap

        #region ExplicitInterpAMD
        public static ExplicitInterpAMDImpl ExplicitInterpAMD()
        {
            return ExplicitInterpAMDImpl.Instance;
            
        }

        internal class ExplicitInterpAMDImpl: Decoration
        {
            public static readonly ExplicitInterpAMDImpl Instance = new ExplicitInterpAMDImpl();
        
            private  ExplicitInterpAMDImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.ExplicitInterpAMD;
            public new static ExplicitInterpAMDImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ExplicitInterpAMDImpl object.</summary>
            /// <returns>A string that represents the ExplicitInterpAMDImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.ExplicitInterpAMD()";
            }
        }
        #endregion //ExplicitInterpAMD

        #region OverrideCoverageNV
        public static OverrideCoverageNVImpl OverrideCoverageNV()
        {
            return OverrideCoverageNVImpl.Instance;
            
        }

        internal class OverrideCoverageNVImpl: Decoration
        {
            public static readonly OverrideCoverageNVImpl Instance = new OverrideCoverageNVImpl();
        
            private  OverrideCoverageNVImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.OverrideCoverageNV;
            public new static OverrideCoverageNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the OverrideCoverageNVImpl object.</summary>
            /// <returns>A string that represents the OverrideCoverageNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.OverrideCoverageNV()";
            }
        }
        #endregion //OverrideCoverageNV

        #region PassthroughNV
        public static PassthroughNVImpl PassthroughNV()
        {
            return PassthroughNVImpl.Instance;
            
        }

        internal class PassthroughNVImpl: Decoration
        {
            public static readonly PassthroughNVImpl Instance = new PassthroughNVImpl();
        
            private  PassthroughNVImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.PassthroughNV;
            public new static PassthroughNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PassthroughNVImpl object.</summary>
            /// <returns>A string that represents the PassthroughNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.PassthroughNV()";
            }
        }
        #endregion //PassthroughNV

        #region ViewportRelativeNV
        public static ViewportRelativeNVImpl ViewportRelativeNV()
        {
            return ViewportRelativeNVImpl.Instance;
            
        }

        internal class ViewportRelativeNVImpl: Decoration
        {
            public static readonly ViewportRelativeNVImpl Instance = new ViewportRelativeNVImpl();
        
            private  ViewportRelativeNVImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.ViewportRelativeNV;
            public new static ViewportRelativeNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ViewportRelativeNVImpl object.</summary>
            /// <returns>A string that represents the ViewportRelativeNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.ViewportRelativeNV()";
            }
        }
        #endregion //ViewportRelativeNV

        #region SecondaryViewportRelativeNV
        public static SecondaryViewportRelativeNVImpl SecondaryViewportRelativeNV(uint offset)
        {
            return new SecondaryViewportRelativeNVImpl(offset);
            
        }

        internal class SecondaryViewportRelativeNVImpl: Decoration
        {
            public SecondaryViewportRelativeNVImpl()
            {
            }

            public SecondaryViewportRelativeNVImpl(uint offset)
            {
                this.Offset = offset;
            }
            public override Enumerant Value => Decoration.Enumerant.SecondaryViewportRelativeNV;
            public uint Offset { get; set; }
            public new static SecondaryViewportRelativeNVImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new SecondaryViewportRelativeNVImpl();
                res.Offset = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Offset.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Offset.Write(writer);
            }

            /// <summary>Returns a string that represents the SecondaryViewportRelativeNVImpl object.</summary>
            /// <returns>A string that represents the SecondaryViewportRelativeNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.SecondaryViewportRelativeNV({Offset})";
            }
        }
        #endregion //SecondaryViewportRelativeNV

        #region PerPrimitiveNV
        public static PerPrimitiveNVImpl PerPrimitiveNV()
        {
            return PerPrimitiveNVImpl.Instance;
            
        }

        internal class PerPrimitiveNVImpl: Decoration
        {
            public static readonly PerPrimitiveNVImpl Instance = new PerPrimitiveNVImpl();
        
            private  PerPrimitiveNVImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.PerPrimitiveNV;
            public new static PerPrimitiveNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PerPrimitiveNVImpl object.</summary>
            /// <returns>A string that represents the PerPrimitiveNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.PerPrimitiveNV()";
            }
        }
        #endregion //PerPrimitiveNV

        #region PerViewNV
        public static PerViewNVImpl PerViewNV()
        {
            return PerViewNVImpl.Instance;
            
        }

        internal class PerViewNVImpl: Decoration
        {
            public static readonly PerViewNVImpl Instance = new PerViewNVImpl();
        
            private  PerViewNVImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.PerViewNV;
            public new static PerViewNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PerViewNVImpl object.</summary>
            /// <returns>A string that represents the PerViewNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.PerViewNV()";
            }
        }
        #endregion //PerViewNV

        #region PerTaskNV
        public static PerTaskNVImpl PerTaskNV()
        {
            return PerTaskNVImpl.Instance;
            
        }

        internal class PerTaskNVImpl: Decoration
        {
            public static readonly PerTaskNVImpl Instance = new PerTaskNVImpl();
        
            private  PerTaskNVImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.PerTaskNV;
            public new static PerTaskNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PerTaskNVImpl object.</summary>
            /// <returns>A string that represents the PerTaskNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.PerTaskNV()";
            }
        }
        #endregion //PerTaskNV

        #region PerVertexNV
        public static PerVertexNVImpl PerVertexNV()
        {
            return PerVertexNVImpl.Instance;
            
        }

        internal class PerVertexNVImpl: Decoration
        {
            public static readonly PerVertexNVImpl Instance = new PerVertexNVImpl();
        
            private  PerVertexNVImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.PerVertexNV;
            public new static PerVertexNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PerVertexNVImpl object.</summary>
            /// <returns>A string that represents the PerVertexNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.PerVertexNV()";
            }
        }
        #endregion //PerVertexNV

        #region NonUniform
        public static NonUniformImpl NonUniform()
        {
            return NonUniformImpl.Instance;
            
        }

        internal class NonUniformImpl: Decoration
        {
            public static readonly NonUniformImpl Instance = new NonUniformImpl();
        
            private  NonUniformImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.NonUniform;
            public new static NonUniformImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NonUniformImpl object.</summary>
            /// <returns>A string that represents the NonUniformImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.NonUniform()";
            }
        }
        #endregion //NonUniform

        #region NonUniformEXT
        public static NonUniformEXTImpl NonUniformEXT()
        {
            return NonUniformEXTImpl.Instance;
            
        }

        internal class NonUniformEXTImpl: Decoration
        {
            public static readonly NonUniformEXTImpl Instance = new NonUniformEXTImpl();
        
            private  NonUniformEXTImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.NonUniformEXT;
            public new static NonUniformEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NonUniformEXTImpl object.</summary>
            /// <returns>A string that represents the NonUniformEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.NonUniformEXT()";
            }
        }
        #endregion //NonUniformEXT

        #region RestrictPointer
        public static RestrictPointerImpl RestrictPointer()
        {
            return RestrictPointerImpl.Instance;
            
        }

        internal class RestrictPointerImpl: Decoration
        {
            public static readonly RestrictPointerImpl Instance = new RestrictPointerImpl();
        
            private  RestrictPointerImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.RestrictPointer;
            public new static RestrictPointerImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RestrictPointerImpl object.</summary>
            /// <returns>A string that represents the RestrictPointerImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.RestrictPointer()";
            }
        }
        #endregion //RestrictPointer

        #region RestrictPointerEXT
        public static RestrictPointerEXTImpl RestrictPointerEXT()
        {
            return RestrictPointerEXTImpl.Instance;
            
        }

        internal class RestrictPointerEXTImpl: Decoration
        {
            public static readonly RestrictPointerEXTImpl Instance = new RestrictPointerEXTImpl();
        
            private  RestrictPointerEXTImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.RestrictPointerEXT;
            public new static RestrictPointerEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RestrictPointerEXTImpl object.</summary>
            /// <returns>A string that represents the RestrictPointerEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.RestrictPointerEXT()";
            }
        }
        #endregion //RestrictPointerEXT

        #region AliasedPointer
        public static AliasedPointerImpl AliasedPointer()
        {
            return AliasedPointerImpl.Instance;
            
        }

        internal class AliasedPointerImpl: Decoration
        {
            public static readonly AliasedPointerImpl Instance = new AliasedPointerImpl();
        
            private  AliasedPointerImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.AliasedPointer;
            public new static AliasedPointerImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the AliasedPointerImpl object.</summary>
            /// <returns>A string that represents the AliasedPointerImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.AliasedPointer()";
            }
        }
        #endregion //AliasedPointer

        #region AliasedPointerEXT
        public static AliasedPointerEXTImpl AliasedPointerEXT()
        {
            return AliasedPointerEXTImpl.Instance;
            
        }

        internal class AliasedPointerEXTImpl: Decoration
        {
            public static readonly AliasedPointerEXTImpl Instance = new AliasedPointerEXTImpl();
        
            private  AliasedPointerEXTImpl()
            {
            }
            public override Enumerant Value => Decoration.Enumerant.AliasedPointerEXT;
            public new static AliasedPointerEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the AliasedPointerEXTImpl object.</summary>
            /// <returns>A string that represents the AliasedPointerEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.AliasedPointerEXT()";
            }
        }
        #endregion //AliasedPointerEXT

        #region CounterBuffer
        public static CounterBufferImpl CounterBuffer(Spv.IdRef counterBuffer)
        {
            return new CounterBufferImpl(counterBuffer);
            
        }

        internal class CounterBufferImpl: Decoration
        {
            public CounterBufferImpl()
            {
            }

            public CounterBufferImpl(Spv.IdRef counterBuffer)
            {
                this.CounterBuffer = counterBuffer;
            }
            public override Enumerant Value => Decoration.Enumerant.CounterBuffer;
            public new Spv.IdRef CounterBuffer { get; set; }
            public new static CounterBufferImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new CounterBufferImpl();
                res.CounterBuffer = Spv.IdRef.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += CounterBuffer.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                CounterBuffer.Write(writer);
            }

            /// <summary>Returns a string that represents the CounterBufferImpl object.</summary>
            /// <returns>A string that represents the CounterBufferImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.CounterBuffer({CounterBuffer})";
            }
        }
        #endregion //CounterBuffer

        #region HlslCounterBufferGOOGLE
        public static HlslCounterBufferGOOGLEImpl HlslCounterBufferGOOGLE(Spv.IdRef counterBuffer)
        {
            return new HlslCounterBufferGOOGLEImpl(counterBuffer);
            
        }

        internal class HlslCounterBufferGOOGLEImpl: Decoration
        {
            public HlslCounterBufferGOOGLEImpl()
            {
            }

            public HlslCounterBufferGOOGLEImpl(Spv.IdRef counterBuffer)
            {
                this.CounterBuffer = counterBuffer;
            }
            public override Enumerant Value => Decoration.Enumerant.HlslCounterBufferGOOGLE;
            public Spv.IdRef CounterBuffer { get; set; }
            public new static HlslCounterBufferGOOGLEImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new HlslCounterBufferGOOGLEImpl();
                res.CounterBuffer = Spv.IdRef.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += CounterBuffer.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                CounterBuffer.Write(writer);
            }

            /// <summary>Returns a string that represents the HlslCounterBufferGOOGLEImpl object.</summary>
            /// <returns>A string that represents the HlslCounterBufferGOOGLEImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.HlslCounterBufferGOOGLE({CounterBuffer})";
            }
        }
        #endregion //HlslCounterBufferGOOGLE

        #region UserSemantic
        public static UserSemanticImpl UserSemantic(string semantic)
        {
            return new UserSemanticImpl(semantic);
            
        }

        internal class UserSemanticImpl: Decoration
        {
            public UserSemanticImpl()
            {
            }

            public UserSemanticImpl(string semantic)
            {
                this.Semantic = semantic;
            }
            public override Enumerant Value => Decoration.Enumerant.UserSemantic;
            public string Semantic { get; set; }
            public new static UserSemanticImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new UserSemanticImpl();
                res.Semantic = Spv.LiteralString.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Semantic.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Semantic.Write(writer);
            }

            /// <summary>Returns a string that represents the UserSemanticImpl object.</summary>
            /// <returns>A string that represents the UserSemanticImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.UserSemantic({Semantic})";
            }
        }
        #endregion //UserSemantic

        #region HlslSemanticGOOGLE
        public static HlslSemanticGOOGLEImpl HlslSemanticGOOGLE(string semantic)
        {
            return new HlslSemanticGOOGLEImpl(semantic);
            
        }

        internal class HlslSemanticGOOGLEImpl: Decoration
        {
            public HlslSemanticGOOGLEImpl()
            {
            }

            public HlslSemanticGOOGLEImpl(string semantic)
            {
                this.Semantic = semantic;
            }
            public override Enumerant Value => Decoration.Enumerant.HlslSemanticGOOGLE;
            public string Semantic { get; set; }
            public new static HlslSemanticGOOGLEImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new HlslSemanticGOOGLEImpl();
                res.Semantic = Spv.LiteralString.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Semantic.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Semantic.Write(writer);
            }

            /// <summary>Returns a string that represents the HlslSemanticGOOGLEImpl object.</summary>
            /// <returns>A string that represents the HlslSemanticGOOGLEImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.HlslSemanticGOOGLE({Semantic})";
            }
        }
        #endregion //HlslSemanticGOOGLE

        #region UserTypeGOOGLE
        public static UserTypeGOOGLEImpl UserTypeGOOGLE(string userType)
        {
            return new UserTypeGOOGLEImpl(userType);
            
        }

        internal class UserTypeGOOGLEImpl: Decoration
        {
            public UserTypeGOOGLEImpl()
            {
            }

            public UserTypeGOOGLEImpl(string userType)
            {
                this.UserType = userType;
            }
            public override Enumerant Value => Decoration.Enumerant.UserTypeGOOGLE;
            public string UserType { get; set; }
            public new static UserTypeGOOGLEImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new UserTypeGOOGLEImpl();
                res.UserType = Spv.LiteralString.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += UserType.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                UserType.Write(writer);
            }

            /// <summary>Returns a string that represents the UserTypeGOOGLEImpl object.</summary>
            /// <returns>A string that represents the UserTypeGOOGLEImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Decoration.UserTypeGOOGLE({UserType})";
            }
        }
        #endregion //UserTypeGOOGLE

        public abstract Enumerant Value { get; }

        public static Decoration Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.RelaxedPrecision :
                    return RelaxedPrecisionImpl.Parse(reader, wordCount - 1);
                case Enumerant.SpecId :
                    return SpecIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.Block :
                    return BlockImpl.Parse(reader, wordCount - 1);
                case Enumerant.BufferBlock :
                    return BufferBlockImpl.Parse(reader, wordCount - 1);
                case Enumerant.RowMajor :
                    return RowMajorImpl.Parse(reader, wordCount - 1);
                case Enumerant.ColMajor :
                    return ColMajorImpl.Parse(reader, wordCount - 1);
                case Enumerant.ArrayStride :
                    return ArrayStrideImpl.Parse(reader, wordCount - 1);
                case Enumerant.MatrixStride :
                    return MatrixStrideImpl.Parse(reader, wordCount - 1);
                case Enumerant.GLSLShared :
                    return GLSLSharedImpl.Parse(reader, wordCount - 1);
                case Enumerant.GLSLPacked :
                    return GLSLPackedImpl.Parse(reader, wordCount - 1);
                case Enumerant.CPacked :
                    return CPackedImpl.Parse(reader, wordCount - 1);
                case Enumerant.BuiltIn :
                    return BuiltInImpl.Parse(reader, wordCount - 1);
                case Enumerant.NoPerspective :
                    return NoPerspectiveImpl.Parse(reader, wordCount - 1);
                case Enumerant.Flat :
                    return FlatImpl.Parse(reader, wordCount - 1);
                case Enumerant.Patch :
                    return PatchImpl.Parse(reader, wordCount - 1);
                case Enumerant.Centroid :
                    return CentroidImpl.Parse(reader, wordCount - 1);
                case Enumerant.Sample :
                    return SampleImpl.Parse(reader, wordCount - 1);
                case Enumerant.Invariant :
                    return InvariantImpl.Parse(reader, wordCount - 1);
                case Enumerant.Restrict :
                    return RestrictImpl.Parse(reader, wordCount - 1);
                case Enumerant.Aliased :
                    return AliasedImpl.Parse(reader, wordCount - 1);
                case Enumerant.Volatile :
                    return VolatileImpl.Parse(reader, wordCount - 1);
                case Enumerant.Constant :
                    return ConstantImpl.Parse(reader, wordCount - 1);
                case Enumerant.Coherent :
                    return CoherentImpl.Parse(reader, wordCount - 1);
                case Enumerant.NonWritable :
                    return NonWritableImpl.Parse(reader, wordCount - 1);
                case Enumerant.NonReadable :
                    return NonReadableImpl.Parse(reader, wordCount - 1);
                case Enumerant.Uniform :
                    return UniformImpl.Parse(reader, wordCount - 1);
                case Enumerant.UniformId :
                    return UniformIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.SaturatedConversion :
                    return SaturatedConversionImpl.Parse(reader, wordCount - 1);
                case Enumerant.Stream :
                    return StreamImpl.Parse(reader, wordCount - 1);
                case Enumerant.Location :
                    return LocationImpl.Parse(reader, wordCount - 1);
                case Enumerant.Component :
                    return ComponentImpl.Parse(reader, wordCount - 1);
                case Enumerant.Index :
                    return IndexImpl.Parse(reader, wordCount - 1);
                case Enumerant.Binding :
                    return BindingImpl.Parse(reader, wordCount - 1);
                case Enumerant.DescriptorSet :
                    return DescriptorSetImpl.Parse(reader, wordCount - 1);
                case Enumerant.Offset :
                    return OffsetImpl.Parse(reader, wordCount - 1);
                case Enumerant.XfbBuffer :
                    return XfbBufferImpl.Parse(reader, wordCount - 1);
                case Enumerant.XfbStride :
                    return XfbStrideImpl.Parse(reader, wordCount - 1);
                case Enumerant.FuncParamAttr :
                    return FuncParamAttrImpl.Parse(reader, wordCount - 1);
                case Enumerant.FPRoundingMode :
                    return FPRoundingModeImpl.Parse(reader, wordCount - 1);
                case Enumerant.FPFastMathMode :
                    return FPFastMathModeImpl.Parse(reader, wordCount - 1);
                case Enumerant.LinkageAttributes :
                    return LinkageAttributesImpl.Parse(reader, wordCount - 1);
                case Enumerant.NoContraction :
                    return NoContractionImpl.Parse(reader, wordCount - 1);
                case Enumerant.InputAttachmentIndex :
                    return InputAttachmentIndexImpl.Parse(reader, wordCount - 1);
                case Enumerant.Alignment :
                    return AlignmentImpl.Parse(reader, wordCount - 1);
                case Enumerant.MaxByteOffset :
                    return MaxByteOffsetImpl.Parse(reader, wordCount - 1);
                case Enumerant.AlignmentId :
                    return AlignmentIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.MaxByteOffsetId :
                    return MaxByteOffsetIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.NoSignedWrap :
                    return NoSignedWrapImpl.Parse(reader, wordCount - 1);
                case Enumerant.NoUnsignedWrap :
                    return NoUnsignedWrapImpl.Parse(reader, wordCount - 1);
                case Enumerant.ExplicitInterpAMD :
                    return ExplicitInterpAMDImpl.Parse(reader, wordCount - 1);
                case Enumerant.OverrideCoverageNV :
                    return OverrideCoverageNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.PassthroughNV :
                    return PassthroughNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.ViewportRelativeNV :
                    return ViewportRelativeNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.SecondaryViewportRelativeNV :
                    return SecondaryViewportRelativeNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.PerPrimitiveNV :
                    return PerPrimitiveNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.PerViewNV :
                    return PerViewNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.PerTaskNV :
                    return PerTaskNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.PerVertexNV :
                    return PerVertexNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.NonUniform :
                    return NonUniformImpl.Parse(reader, wordCount - 1);
                //NonUniformEXT has the same id as another value in enum.
                //case Enumerant.NonUniformEXT :
                //    return NonUniformEXT.Parse(reader, wordCount - 1);
                case Enumerant.RestrictPointer :
                    return RestrictPointerImpl.Parse(reader, wordCount - 1);
                //RestrictPointerEXT has the same id as another value in enum.
                //case Enumerant.RestrictPointerEXT :
                //    return RestrictPointerEXT.Parse(reader, wordCount - 1);
                case Enumerant.AliasedPointer :
                    return AliasedPointerImpl.Parse(reader, wordCount - 1);
                //AliasedPointerEXT has the same id as another value in enum.
                //case Enumerant.AliasedPointerEXT :
                //    return AliasedPointerEXT.Parse(reader, wordCount - 1);
                case Enumerant.CounterBuffer :
                    return CounterBufferImpl.Parse(reader, wordCount - 1);
                //HlslCounterBufferGOOGLE has the same id as another value in enum.
                //case Enumerant.HlslCounterBufferGOOGLE :
                //    return HlslCounterBufferGOOGLE.Parse(reader, wordCount - 1);
                case Enumerant.UserSemantic :
                    return UserSemanticImpl.Parse(reader, wordCount - 1);
                //HlslSemanticGOOGLE has the same id as another value in enum.
                //case Enumerant.HlslSemanticGOOGLE :
                //    return HlslSemanticGOOGLE.Parse(reader, wordCount - 1);
                case Enumerant.UserTypeGOOGLE :
                    return UserTypeGOOGLEImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown Decoration "+id);
            }
        }
        
        public static Decoration ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<Decoration> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<Decoration>();
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