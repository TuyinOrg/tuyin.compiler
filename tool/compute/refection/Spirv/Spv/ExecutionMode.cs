using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class ExecutionMode : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Geometry)]
            Invocations = 0,
            [Capability(Capability.Enumerant.Tessellation)]
            SpacingEqual = 1,
            [Capability(Capability.Enumerant.Tessellation)]
            SpacingFractionalEven = 2,
            [Capability(Capability.Enumerant.Tessellation)]
            SpacingFractionalOdd = 3,
            [Capability(Capability.Enumerant.Tessellation)]
            VertexOrderCw = 4,
            [Capability(Capability.Enumerant.Tessellation)]
            VertexOrderCcw = 5,
            [Capability(Capability.Enumerant.Shader)]
            PixelCenterInteger = 6,
            [Capability(Capability.Enumerant.Shader)]
            OriginUpperLeft = 7,
            [Capability(Capability.Enumerant.Shader)]
            OriginLowerLeft = 8,
            [Capability(Capability.Enumerant.Shader)]
            EarlyFragmentTests = 9,
            [Capability(Capability.Enumerant.Tessellation)]
            PointMode = 10,
            [Capability(Capability.Enumerant.TransformFeedback)]
            Xfb = 11,
            [Capability(Capability.Enumerant.Shader)]
            DepthReplacing = 12,
            [Capability(Capability.Enumerant.Shader)]
            DepthGreater = 14,
            [Capability(Capability.Enumerant.Shader)]
            DepthLess = 15,
            [Capability(Capability.Enumerant.Shader)]
            DepthUnchanged = 16,
            LocalSize = 17,
            [Capability(Capability.Enumerant.Kernel)]
            LocalSizeHint = 18,
            [Capability(Capability.Enumerant.Geometry)]
            InputPoints = 19,
            [Capability(Capability.Enumerant.Geometry)]
            InputLines = 20,
            [Capability(Capability.Enumerant.Geometry)]
            InputLinesAdjacency = 21,
            [Capability(Capability.Enumerant.Geometry)]
            [Capability(Capability.Enumerant.Tessellation)]
            Triangles = 22,
            [Capability(Capability.Enumerant.Geometry)]
            InputTrianglesAdjacency = 23,
            [Capability(Capability.Enumerant.Tessellation)]
            Quads = 24,
            [Capability(Capability.Enumerant.Tessellation)]
            Isolines = 25,
            [Capability(Capability.Enumerant.Geometry)]
            [Capability(Capability.Enumerant.Tessellation)]
            [Capability(Capability.Enumerant.MeshShadingNV)]
            OutputVertices = 26,
            [Capability(Capability.Enumerant.Geometry)]
            [Capability(Capability.Enumerant.MeshShadingNV)]
            OutputPoints = 27,
            [Capability(Capability.Enumerant.Geometry)]
            OutputLineStrip = 28,
            [Capability(Capability.Enumerant.Geometry)]
            OutputTriangleStrip = 29,
            [Capability(Capability.Enumerant.Kernel)]
            VecTypeHint = 30,
            [Capability(Capability.Enumerant.Kernel)]
            ContractionOff = 31,
            [Capability(Capability.Enumerant.Kernel)]
            Initializer = 33,
            [Capability(Capability.Enumerant.Kernel)]
            Finalizer = 34,
            [Capability(Capability.Enumerant.SubgroupDispatch)]
            SubgroupSize = 35,
            [Capability(Capability.Enumerant.SubgroupDispatch)]
            SubgroupsPerWorkgroup = 36,
            [Capability(Capability.Enumerant.SubgroupDispatch)]
            SubgroupsPerWorkgroupId = 37,
            LocalSizeId = 38,
            [Capability(Capability.Enumerant.Kernel)]
            LocalSizeHintId = 39,
            [Capability(Capability.Enumerant.SampleMaskPostDepthCoverage)]
            PostDepthCoverage = 4446,
            [Capability(Capability.Enumerant.DenormPreserve)]
            DenormPreserve = 4459,
            [Capability(Capability.Enumerant.DenormFlushToZero)]
            DenormFlushToZero = 4460,
            [Capability(Capability.Enumerant.SignedZeroInfNanPreserve)]
            SignedZeroInfNanPreserve = 4461,
            [Capability(Capability.Enumerant.RoundingModeRTE)]
            RoundingModeRTE = 4462,
            [Capability(Capability.Enumerant.RoundingModeRTZ)]
            RoundingModeRTZ = 4463,
            [Capability(Capability.Enumerant.StencilExportEXT)]
            StencilRefReplacingEXT = 5027,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            OutputLinesNV = 5269,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            OutputPrimitivesNV = 5270,
            [Capability(Capability.Enumerant.ComputeDerivativeGroupQuadsNV)]
            DerivativeGroupQuadsNV = 5289,
            [Capability(Capability.Enumerant.ComputeDerivativeGroupLinearNV)]
            DerivativeGroupLinearNV = 5290,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            OutputTrianglesNV = 5298,
            [Capability(Capability.Enumerant.FragmentShaderPixelInterlockEXT)]
            PixelInterlockOrderedEXT = 5366,
            [Capability(Capability.Enumerant.FragmentShaderPixelInterlockEXT)]
            PixelInterlockUnorderedEXT = 5367,
            [Capability(Capability.Enumerant.FragmentShaderSampleInterlockEXT)]
            SampleInterlockOrderedEXT = 5368,
            [Capability(Capability.Enumerant.FragmentShaderSampleInterlockEXT)]
            SampleInterlockUnorderedEXT = 5369,
            [Capability(Capability.Enumerant.FragmentShaderShadingRateInterlockEXT)]
            ShadingRateInterlockOrderedEXT = 5370,
            [Capability(Capability.Enumerant.FragmentShaderShadingRateInterlockEXT)]
            ShadingRateInterlockUnorderedEXT = 5371,
        }

        #region Invocations
        public static InvocationsImpl Invocations(uint numberofInvocations)
        {
            return new InvocationsImpl(numberofInvocations);
            
        }

        internal class InvocationsImpl: ExecutionMode
        {
            public InvocationsImpl()
            {
            }

            public InvocationsImpl(uint numberofInvocations)
            {
                this.NumberofInvocations = numberofInvocations;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.Invocations;
            public uint NumberofInvocations { get; set; }
            public new static InvocationsImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new InvocationsImpl();
                res.NumberofInvocations = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += NumberofInvocations.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                NumberofInvocations.Write(writer);
            }

            /// <summary>Returns a string that represents the InvocationsImpl object.</summary>
            /// <returns>A string that represents the InvocationsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.Invocations({NumberofInvocations})";
            }
        }
        #endregion //Invocations

        #region SpacingEqual
        public static SpacingEqualImpl SpacingEqual()
        {
            return SpacingEqualImpl.Instance;
            
        }

        internal class SpacingEqualImpl: ExecutionMode
        {
            public static readonly SpacingEqualImpl Instance = new SpacingEqualImpl();
        
            private  SpacingEqualImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.SpacingEqual;
            public new static SpacingEqualImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SpacingEqualImpl object.</summary>
            /// <returns>A string that represents the SpacingEqualImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.SpacingEqual()";
            }
        }
        #endregion //SpacingEqual

        #region SpacingFractionalEven
        public static SpacingFractionalEvenImpl SpacingFractionalEven()
        {
            return SpacingFractionalEvenImpl.Instance;
            
        }

        internal class SpacingFractionalEvenImpl: ExecutionMode
        {
            public static readonly SpacingFractionalEvenImpl Instance = new SpacingFractionalEvenImpl();
        
            private  SpacingFractionalEvenImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.SpacingFractionalEven;
            public new static SpacingFractionalEvenImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SpacingFractionalEvenImpl object.</summary>
            /// <returns>A string that represents the SpacingFractionalEvenImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.SpacingFractionalEven()";
            }
        }
        #endregion //SpacingFractionalEven

        #region SpacingFractionalOdd
        public static SpacingFractionalOddImpl SpacingFractionalOdd()
        {
            return SpacingFractionalOddImpl.Instance;
            
        }

        internal class SpacingFractionalOddImpl: ExecutionMode
        {
            public static readonly SpacingFractionalOddImpl Instance = new SpacingFractionalOddImpl();
        
            private  SpacingFractionalOddImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.SpacingFractionalOdd;
            public new static SpacingFractionalOddImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SpacingFractionalOddImpl object.</summary>
            /// <returns>A string that represents the SpacingFractionalOddImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.SpacingFractionalOdd()";
            }
        }
        #endregion //SpacingFractionalOdd

        #region VertexOrderCw
        public static VertexOrderCwImpl VertexOrderCw()
        {
            return VertexOrderCwImpl.Instance;
            
        }

        internal class VertexOrderCwImpl: ExecutionMode
        {
            public static readonly VertexOrderCwImpl Instance = new VertexOrderCwImpl();
        
            private  VertexOrderCwImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.VertexOrderCw;
            public new static VertexOrderCwImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VertexOrderCwImpl object.</summary>
            /// <returns>A string that represents the VertexOrderCwImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.VertexOrderCw()";
            }
        }
        #endregion //VertexOrderCw

        #region VertexOrderCcw
        public static VertexOrderCcwImpl VertexOrderCcw()
        {
            return VertexOrderCcwImpl.Instance;
            
        }

        internal class VertexOrderCcwImpl: ExecutionMode
        {
            public static readonly VertexOrderCcwImpl Instance = new VertexOrderCcwImpl();
        
            private  VertexOrderCcwImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.VertexOrderCcw;
            public new static VertexOrderCcwImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VertexOrderCcwImpl object.</summary>
            /// <returns>A string that represents the VertexOrderCcwImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.VertexOrderCcw()";
            }
        }
        #endregion //VertexOrderCcw

        #region PixelCenterInteger
        public static PixelCenterIntegerImpl PixelCenterInteger()
        {
            return PixelCenterIntegerImpl.Instance;
            
        }

        internal class PixelCenterIntegerImpl: ExecutionMode
        {
            public static readonly PixelCenterIntegerImpl Instance = new PixelCenterIntegerImpl();
        
            private  PixelCenterIntegerImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.PixelCenterInteger;
            public new static PixelCenterIntegerImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PixelCenterIntegerImpl object.</summary>
            /// <returns>A string that represents the PixelCenterIntegerImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.PixelCenterInteger()";
            }
        }
        #endregion //PixelCenterInteger

        #region OriginUpperLeft
        public static OriginUpperLeftImpl OriginUpperLeft()
        {
            return OriginUpperLeftImpl.Instance;
            
        }

        internal class OriginUpperLeftImpl: ExecutionMode
        {
            public static readonly OriginUpperLeftImpl Instance = new OriginUpperLeftImpl();
        
            private  OriginUpperLeftImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.OriginUpperLeft;
            public new static OriginUpperLeftImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the OriginUpperLeftImpl object.</summary>
            /// <returns>A string that represents the OriginUpperLeftImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.OriginUpperLeft()";
            }
        }
        #endregion //OriginUpperLeft

        #region OriginLowerLeft
        public static OriginLowerLeftImpl OriginLowerLeft()
        {
            return OriginLowerLeftImpl.Instance;
            
        }

        internal class OriginLowerLeftImpl: ExecutionMode
        {
            public static readonly OriginLowerLeftImpl Instance = new OriginLowerLeftImpl();
        
            private  OriginLowerLeftImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.OriginLowerLeft;
            public new static OriginLowerLeftImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the OriginLowerLeftImpl object.</summary>
            /// <returns>A string that represents the OriginLowerLeftImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.OriginLowerLeft()";
            }
        }
        #endregion //OriginLowerLeft

        #region EarlyFragmentTests
        public static EarlyFragmentTestsImpl EarlyFragmentTests()
        {
            return EarlyFragmentTestsImpl.Instance;
            
        }

        internal class EarlyFragmentTestsImpl: ExecutionMode
        {
            public static readonly EarlyFragmentTestsImpl Instance = new EarlyFragmentTestsImpl();
        
            private  EarlyFragmentTestsImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.EarlyFragmentTests;
            public new static EarlyFragmentTestsImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the EarlyFragmentTestsImpl object.</summary>
            /// <returns>A string that represents the EarlyFragmentTestsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.EarlyFragmentTests()";
            }
        }
        #endregion //EarlyFragmentTests

        #region PointMode
        public static PointModeImpl PointMode()
        {
            return PointModeImpl.Instance;
            
        }

        internal class PointModeImpl: ExecutionMode
        {
            public static readonly PointModeImpl Instance = new PointModeImpl();
        
            private  PointModeImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.PointMode;
            public new static PointModeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PointModeImpl object.</summary>
            /// <returns>A string that represents the PointModeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.PointMode()";
            }
        }
        #endregion //PointMode

        #region Xfb
        public static XfbImpl Xfb()
        {
            return XfbImpl.Instance;
            
        }

        internal class XfbImpl: ExecutionMode
        {
            public static readonly XfbImpl Instance = new XfbImpl();
        
            private  XfbImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.Xfb;
            public new static XfbImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the XfbImpl object.</summary>
            /// <returns>A string that represents the XfbImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.Xfb()";
            }
        }
        #endregion //Xfb

        #region DepthReplacing
        public static DepthReplacingImpl DepthReplacing()
        {
            return DepthReplacingImpl.Instance;
            
        }

        internal class DepthReplacingImpl: ExecutionMode
        {
            public static readonly DepthReplacingImpl Instance = new DepthReplacingImpl();
        
            private  DepthReplacingImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.DepthReplacing;
            public new static DepthReplacingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DepthReplacingImpl object.</summary>
            /// <returns>A string that represents the DepthReplacingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.DepthReplacing()";
            }
        }
        #endregion //DepthReplacing

        #region DepthGreater
        public static DepthGreaterImpl DepthGreater()
        {
            return DepthGreaterImpl.Instance;
            
        }

        internal class DepthGreaterImpl: ExecutionMode
        {
            public static readonly DepthGreaterImpl Instance = new DepthGreaterImpl();
        
            private  DepthGreaterImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.DepthGreater;
            public new static DepthGreaterImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DepthGreaterImpl object.</summary>
            /// <returns>A string that represents the DepthGreaterImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.DepthGreater()";
            }
        }
        #endregion //DepthGreater

        #region DepthLess
        public static DepthLessImpl DepthLess()
        {
            return DepthLessImpl.Instance;
            
        }

        internal class DepthLessImpl: ExecutionMode
        {
            public static readonly DepthLessImpl Instance = new DepthLessImpl();
        
            private  DepthLessImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.DepthLess;
            public new static DepthLessImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DepthLessImpl object.</summary>
            /// <returns>A string that represents the DepthLessImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.DepthLess()";
            }
        }
        #endregion //DepthLess

        #region DepthUnchanged
        public static DepthUnchangedImpl DepthUnchanged()
        {
            return DepthUnchangedImpl.Instance;
            
        }

        internal class DepthUnchangedImpl: ExecutionMode
        {
            public static readonly DepthUnchangedImpl Instance = new DepthUnchangedImpl();
        
            private  DepthUnchangedImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.DepthUnchanged;
            public new static DepthUnchangedImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DepthUnchangedImpl object.</summary>
            /// <returns>A string that represents the DepthUnchangedImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.DepthUnchanged()";
            }
        }
        #endregion //DepthUnchanged

        #region LocalSize
        public static LocalSizeImpl LocalSize(uint xsize, uint ysize, uint zsize)
        {
            return new LocalSizeImpl(xsize, ysize, zsize);
            
        }

        internal class LocalSizeImpl: ExecutionMode
        {
            public LocalSizeImpl()
            {
            }

            public LocalSizeImpl(uint xsize, uint ysize, uint zsize)
            {
                this.xsize = xsize;
                this.ysize = ysize;
                this.zsize = zsize;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.LocalSize;
            public uint xsize { get; set; }
            public uint ysize { get; set; }
            public uint zsize { get; set; }
            public new static LocalSizeImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new LocalSizeImpl();
                res.xsize = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                res.ysize = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                res.zsize = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += xsize.GetWordCount();
                wordCount += ysize.GetWordCount();
                wordCount += zsize.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                xsize.Write(writer);
                ysize.Write(writer);
                zsize.Write(writer);
            }

            /// <summary>Returns a string that represents the LocalSizeImpl object.</summary>
            /// <returns>A string that represents the LocalSizeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.LocalSize({xsize}, {ysize}, {zsize})";
            }
        }
        #endregion //LocalSize

        #region LocalSizeHint
        public static LocalSizeHintImpl LocalSizeHint(uint xsize, uint ysize, uint zsize)
        {
            return new LocalSizeHintImpl(xsize, ysize, zsize);
            
        }

        internal class LocalSizeHintImpl: ExecutionMode
        {
            public LocalSizeHintImpl()
            {
            }

            public LocalSizeHintImpl(uint xsize, uint ysize, uint zsize)
            {
                this.xsize = xsize;
                this.ysize = ysize;
                this.zsize = zsize;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.LocalSizeHint;
            public uint xsize { get; set; }
            public uint ysize { get; set; }
            public uint zsize { get; set; }
            public new static LocalSizeHintImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new LocalSizeHintImpl();
                res.xsize = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                res.ysize = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                res.zsize = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += xsize.GetWordCount();
                wordCount += ysize.GetWordCount();
                wordCount += zsize.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                xsize.Write(writer);
                ysize.Write(writer);
                zsize.Write(writer);
            }

            /// <summary>Returns a string that represents the LocalSizeHintImpl object.</summary>
            /// <returns>A string that represents the LocalSizeHintImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.LocalSizeHint({xsize}, {ysize}, {zsize})";
            }
        }
        #endregion //LocalSizeHint

        #region InputPoints
        public static InputPointsImpl InputPoints()
        {
            return InputPointsImpl.Instance;
            
        }

        internal class InputPointsImpl: ExecutionMode
        {
            public static readonly InputPointsImpl Instance = new InputPointsImpl();
        
            private  InputPointsImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.InputPoints;
            public new static InputPointsImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InputPointsImpl object.</summary>
            /// <returns>A string that represents the InputPointsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.InputPoints()";
            }
        }
        #endregion //InputPoints

        #region InputLines
        public static InputLinesImpl InputLines()
        {
            return InputLinesImpl.Instance;
            
        }

        internal class InputLinesImpl: ExecutionMode
        {
            public static readonly InputLinesImpl Instance = new InputLinesImpl();
        
            private  InputLinesImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.InputLines;
            public new static InputLinesImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InputLinesImpl object.</summary>
            /// <returns>A string that represents the InputLinesImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.InputLines()";
            }
        }
        #endregion //InputLines

        #region InputLinesAdjacency
        public static InputLinesAdjacencyImpl InputLinesAdjacency()
        {
            return InputLinesAdjacencyImpl.Instance;
            
        }

        internal class InputLinesAdjacencyImpl: ExecutionMode
        {
            public static readonly InputLinesAdjacencyImpl Instance = new InputLinesAdjacencyImpl();
        
            private  InputLinesAdjacencyImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.InputLinesAdjacency;
            public new static InputLinesAdjacencyImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InputLinesAdjacencyImpl object.</summary>
            /// <returns>A string that represents the InputLinesAdjacencyImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.InputLinesAdjacency()";
            }
        }
        #endregion //InputLinesAdjacency

        #region Triangles
        public static TrianglesImpl Triangles()
        {
            return TrianglesImpl.Instance;
            
        }

        internal class TrianglesImpl: ExecutionMode
        {
            public static readonly TrianglesImpl Instance = new TrianglesImpl();
        
            private  TrianglesImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.Triangles;
            public new static TrianglesImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the TrianglesImpl object.</summary>
            /// <returns>A string that represents the TrianglesImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.Triangles()";
            }
        }
        #endregion //Triangles

        #region InputTrianglesAdjacency
        public static InputTrianglesAdjacencyImpl InputTrianglesAdjacency()
        {
            return InputTrianglesAdjacencyImpl.Instance;
            
        }

        internal class InputTrianglesAdjacencyImpl: ExecutionMode
        {
            public static readonly InputTrianglesAdjacencyImpl Instance = new InputTrianglesAdjacencyImpl();
        
            private  InputTrianglesAdjacencyImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.InputTrianglesAdjacency;
            public new static InputTrianglesAdjacencyImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InputTrianglesAdjacencyImpl object.</summary>
            /// <returns>A string that represents the InputTrianglesAdjacencyImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.InputTrianglesAdjacency()";
            }
        }
        #endregion //InputTrianglesAdjacency

        #region Quads
        public static QuadsImpl Quads()
        {
            return QuadsImpl.Instance;
            
        }

        internal class QuadsImpl: ExecutionMode
        {
            public static readonly QuadsImpl Instance = new QuadsImpl();
        
            private  QuadsImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.Quads;
            public new static QuadsImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the QuadsImpl object.</summary>
            /// <returns>A string that represents the QuadsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.Quads()";
            }
        }
        #endregion //Quads

        #region Isolines
        public static IsolinesImpl Isolines()
        {
            return IsolinesImpl.Instance;
            
        }

        internal class IsolinesImpl: ExecutionMode
        {
            public static readonly IsolinesImpl Instance = new IsolinesImpl();
        
            private  IsolinesImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.Isolines;
            public new static IsolinesImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the IsolinesImpl object.</summary>
            /// <returns>A string that represents the IsolinesImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.Isolines()";
            }
        }
        #endregion //Isolines

        #region OutputVertices
        public static OutputVerticesImpl OutputVertices(uint vertexcount)
        {
            return new OutputVerticesImpl(vertexcount);
            
        }

        internal class OutputVerticesImpl: ExecutionMode
        {
            public OutputVerticesImpl()
            {
            }

            public OutputVerticesImpl(uint vertexcount)
            {
                this.Vertexcount = vertexcount;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.OutputVertices;
            public uint Vertexcount { get; set; }
            public new static OutputVerticesImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new OutputVerticesImpl();
                res.Vertexcount = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Vertexcount.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Vertexcount.Write(writer);
            }

            /// <summary>Returns a string that represents the OutputVerticesImpl object.</summary>
            /// <returns>A string that represents the OutputVerticesImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.OutputVertices({Vertexcount})";
            }
        }
        #endregion //OutputVertices

        #region OutputPoints
        public static OutputPointsImpl OutputPoints()
        {
            return OutputPointsImpl.Instance;
            
        }

        internal class OutputPointsImpl: ExecutionMode
        {
            public static readonly OutputPointsImpl Instance = new OutputPointsImpl();
        
            private  OutputPointsImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.OutputPoints;
            public new static OutputPointsImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the OutputPointsImpl object.</summary>
            /// <returns>A string that represents the OutputPointsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.OutputPoints()";
            }
        }
        #endregion //OutputPoints

        #region OutputLineStrip
        public static OutputLineStripImpl OutputLineStrip()
        {
            return OutputLineStripImpl.Instance;
            
        }

        internal class OutputLineStripImpl: ExecutionMode
        {
            public static readonly OutputLineStripImpl Instance = new OutputLineStripImpl();
        
            private  OutputLineStripImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.OutputLineStrip;
            public new static OutputLineStripImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the OutputLineStripImpl object.</summary>
            /// <returns>A string that represents the OutputLineStripImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.OutputLineStrip()";
            }
        }
        #endregion //OutputLineStrip

        #region OutputTriangleStrip
        public static OutputTriangleStripImpl OutputTriangleStrip()
        {
            return OutputTriangleStripImpl.Instance;
            
        }

        internal class OutputTriangleStripImpl: ExecutionMode
        {
            public static readonly OutputTriangleStripImpl Instance = new OutputTriangleStripImpl();
        
            private  OutputTriangleStripImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.OutputTriangleStrip;
            public new static OutputTriangleStripImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the OutputTriangleStripImpl object.</summary>
            /// <returns>A string that represents the OutputTriangleStripImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.OutputTriangleStrip()";
            }
        }
        #endregion //OutputTriangleStrip

        #region VecTypeHint
        public static VecTypeHintImpl VecTypeHint(uint vectortype)
        {
            return new VecTypeHintImpl(vectortype);
            
        }

        internal class VecTypeHintImpl: ExecutionMode
        {
            public VecTypeHintImpl()
            {
            }

            public VecTypeHintImpl(uint vectortype)
            {
                this.Vectortype = vectortype;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.VecTypeHint;
            public uint Vectortype { get; set; }
            public new static VecTypeHintImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new VecTypeHintImpl();
                res.Vectortype = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Vectortype.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Vectortype.Write(writer);
            }

            /// <summary>Returns a string that represents the VecTypeHintImpl object.</summary>
            /// <returns>A string that represents the VecTypeHintImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.VecTypeHint({Vectortype})";
            }
        }
        #endregion //VecTypeHint

        #region ContractionOff
        public static ContractionOffImpl ContractionOff()
        {
            return ContractionOffImpl.Instance;
            
        }

        internal class ContractionOffImpl: ExecutionMode
        {
            public static readonly ContractionOffImpl Instance = new ContractionOffImpl();
        
            private  ContractionOffImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.ContractionOff;
            public new static ContractionOffImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ContractionOffImpl object.</summary>
            /// <returns>A string that represents the ContractionOffImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.ContractionOff()";
            }
        }
        #endregion //ContractionOff

        #region Initializer
        public static InitializerImpl Initializer()
        {
            return InitializerImpl.Instance;
            
        }

        internal class InitializerImpl: ExecutionMode
        {
            public static readonly InitializerImpl Instance = new InitializerImpl();
        
            private  InitializerImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.Initializer;
            public new static InitializerImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InitializerImpl object.</summary>
            /// <returns>A string that represents the InitializerImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.Initializer()";
            }
        }
        #endregion //Initializer

        #region Finalizer
        public static FinalizerImpl Finalizer()
        {
            return FinalizerImpl.Instance;
            
        }

        internal class FinalizerImpl: ExecutionMode
        {
            public static readonly FinalizerImpl Instance = new FinalizerImpl();
        
            private  FinalizerImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.Finalizer;
            public new static FinalizerImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FinalizerImpl object.</summary>
            /// <returns>A string that represents the FinalizerImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.Finalizer()";
            }
        }
        #endregion //Finalizer

        #region SubgroupSize
        public static SubgroupSizeImpl SubgroupSize(uint subgroupSize)
        {
            return new SubgroupSizeImpl(subgroupSize);
            
        }

        internal class SubgroupSizeImpl: ExecutionMode
        {
            public SubgroupSizeImpl()
            {
            }

            public SubgroupSizeImpl(uint subgroupSize)
            {
                this.SubgroupSize = subgroupSize;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.SubgroupSize;
            public new uint SubgroupSize { get; set; }
            public new static SubgroupSizeImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new SubgroupSizeImpl();
                res.SubgroupSize = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += SubgroupSize.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                SubgroupSize.Write(writer);
            }

            /// <summary>Returns a string that represents the SubgroupSizeImpl object.</summary>
            /// <returns>A string that represents the SubgroupSizeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.SubgroupSize({SubgroupSize})";
            }
        }
        #endregion //SubgroupSize

        #region SubgroupsPerWorkgroup
        public static SubgroupsPerWorkgroupImpl SubgroupsPerWorkgroup(uint subgroupsPerWorkgroup)
        {
            return new SubgroupsPerWorkgroupImpl(subgroupsPerWorkgroup);
            
        }

        internal class SubgroupsPerWorkgroupImpl: ExecutionMode
        {
            public SubgroupsPerWorkgroupImpl()
            {
            }

            public SubgroupsPerWorkgroupImpl(uint subgroupsPerWorkgroup)
            {
                this.SubgroupsPerWorkgroup = subgroupsPerWorkgroup;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.SubgroupsPerWorkgroup;
            public new uint SubgroupsPerWorkgroup { get; set; }
            public new static SubgroupsPerWorkgroupImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new SubgroupsPerWorkgroupImpl();
                res.SubgroupsPerWorkgroup = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += SubgroupsPerWorkgroup.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                SubgroupsPerWorkgroup.Write(writer);
            }

            /// <summary>Returns a string that represents the SubgroupsPerWorkgroupImpl object.</summary>
            /// <returns>A string that represents the SubgroupsPerWorkgroupImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.SubgroupsPerWorkgroup({SubgroupsPerWorkgroup})";
            }
        }
        #endregion //SubgroupsPerWorkgroup

        #region SubgroupsPerWorkgroupId
        public static SubgroupsPerWorkgroupIdImpl SubgroupsPerWorkgroupId(Spv.IdRef subgroupsPerWorkgroup)
        {
            return new SubgroupsPerWorkgroupIdImpl(subgroupsPerWorkgroup);
            
        }

        internal class SubgroupsPerWorkgroupIdImpl: ExecutionMode
        {
            public SubgroupsPerWorkgroupIdImpl()
            {
            }

            public SubgroupsPerWorkgroupIdImpl(Spv.IdRef subgroupsPerWorkgroup)
            {
                this.SubgroupsPerWorkgroup = subgroupsPerWorkgroup;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.SubgroupsPerWorkgroupId;
            public Spv.IdRef SubgroupsPerWorkgroup { get; set; }
            public new static SubgroupsPerWorkgroupIdImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new SubgroupsPerWorkgroupIdImpl();
                res.SubgroupsPerWorkgroup = Spv.IdRef.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += SubgroupsPerWorkgroup.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                SubgroupsPerWorkgroup.Write(writer);
            }

            /// <summary>Returns a string that represents the SubgroupsPerWorkgroupIdImpl object.</summary>
            /// <returns>A string that represents the SubgroupsPerWorkgroupIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.SubgroupsPerWorkgroupId({SubgroupsPerWorkgroup})";
            }
        }
        #endregion //SubgroupsPerWorkgroupId

        #region LocalSizeId
        public static LocalSizeIdImpl LocalSizeId(Spv.IdRef xsize, Spv.IdRef ysize, Spv.IdRef zsize)
        {
            return new LocalSizeIdImpl(xsize, ysize, zsize);
            
        }

        internal class LocalSizeIdImpl: ExecutionMode
        {
            public LocalSizeIdImpl()
            {
            }

            public LocalSizeIdImpl(Spv.IdRef xsize, Spv.IdRef ysize, Spv.IdRef zsize)
            {
                this.xsize = xsize;
                this.ysize = ysize;
                this.zsize = zsize;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.LocalSizeId;
            public Spv.IdRef xsize { get; set; }
            public Spv.IdRef ysize { get; set; }
            public Spv.IdRef zsize { get; set; }
            public new static LocalSizeIdImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new LocalSizeIdImpl();
                res.xsize = Spv.IdRef.Parse(reader, end-reader.Position);
                res.ysize = Spv.IdRef.Parse(reader, end-reader.Position);
                res.zsize = Spv.IdRef.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += xsize.GetWordCount();
                wordCount += ysize.GetWordCount();
                wordCount += zsize.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                xsize.Write(writer);
                ysize.Write(writer);
                zsize.Write(writer);
            }

            /// <summary>Returns a string that represents the LocalSizeIdImpl object.</summary>
            /// <returns>A string that represents the LocalSizeIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.LocalSizeId({xsize}, {ysize}, {zsize})";
            }
        }
        #endregion //LocalSizeId

        #region LocalSizeHintId
        public static LocalSizeHintIdImpl LocalSizeHintId(Spv.IdRef localSizeHint)
        {
            return new LocalSizeHintIdImpl(localSizeHint);
            
        }

        internal class LocalSizeHintIdImpl: ExecutionMode
        {
            public LocalSizeHintIdImpl()
            {
            }

            public LocalSizeHintIdImpl(Spv.IdRef localSizeHint)
            {
                this.LocalSizeHint = localSizeHint;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.LocalSizeHintId;
            public Spv.IdRef LocalSizeHint { get; set; }
            public new static LocalSizeHintIdImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new LocalSizeHintIdImpl();
                res.LocalSizeHint = Spv.IdRef.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += LocalSizeHint.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                LocalSizeHint.Write(writer);
            }

            /// <summary>Returns a string that represents the LocalSizeHintIdImpl object.</summary>
            /// <returns>A string that represents the LocalSizeHintIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.LocalSizeHintId({LocalSizeHint})";
            }
        }
        #endregion //LocalSizeHintId

        #region PostDepthCoverage
        public static PostDepthCoverageImpl PostDepthCoverage()
        {
            return PostDepthCoverageImpl.Instance;
            
        }

        internal class PostDepthCoverageImpl: ExecutionMode
        {
            public static readonly PostDepthCoverageImpl Instance = new PostDepthCoverageImpl();
        
            private  PostDepthCoverageImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.PostDepthCoverage;
            public new static PostDepthCoverageImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PostDepthCoverageImpl object.</summary>
            /// <returns>A string that represents the PostDepthCoverageImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.PostDepthCoverage()";
            }
        }
        #endregion //PostDepthCoverage

        #region DenormPreserve
        public static DenormPreserveImpl DenormPreserve(uint targetWidth)
        {
            return new DenormPreserveImpl(targetWidth);
            
        }

        internal class DenormPreserveImpl: ExecutionMode
        {
            public DenormPreserveImpl()
            {
            }

            public DenormPreserveImpl(uint targetWidth)
            {
                this.TargetWidth = targetWidth;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.DenormPreserve;
            public uint TargetWidth { get; set; }
            public new static DenormPreserveImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new DenormPreserveImpl();
                res.TargetWidth = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += TargetWidth.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                TargetWidth.Write(writer);
            }

            /// <summary>Returns a string that represents the DenormPreserveImpl object.</summary>
            /// <returns>A string that represents the DenormPreserveImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.DenormPreserve({TargetWidth})";
            }
        }
        #endregion //DenormPreserve

        #region DenormFlushToZero
        public static DenormFlushToZeroImpl DenormFlushToZero(uint targetWidth)
        {
            return new DenormFlushToZeroImpl(targetWidth);
            
        }

        internal class DenormFlushToZeroImpl: ExecutionMode
        {
            public DenormFlushToZeroImpl()
            {
            }

            public DenormFlushToZeroImpl(uint targetWidth)
            {
                this.TargetWidth = targetWidth;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.DenormFlushToZero;
            public uint TargetWidth { get; set; }
            public new static DenormFlushToZeroImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new DenormFlushToZeroImpl();
                res.TargetWidth = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += TargetWidth.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                TargetWidth.Write(writer);
            }

            /// <summary>Returns a string that represents the DenormFlushToZeroImpl object.</summary>
            /// <returns>A string that represents the DenormFlushToZeroImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.DenormFlushToZero({TargetWidth})";
            }
        }
        #endregion //DenormFlushToZero

        #region SignedZeroInfNanPreserve
        public static SignedZeroInfNanPreserveImpl SignedZeroInfNanPreserve(uint targetWidth)
        {
            return new SignedZeroInfNanPreserveImpl(targetWidth);
            
        }

        internal class SignedZeroInfNanPreserveImpl: ExecutionMode
        {
            public SignedZeroInfNanPreserveImpl()
            {
            }

            public SignedZeroInfNanPreserveImpl(uint targetWidth)
            {
                this.TargetWidth = targetWidth;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.SignedZeroInfNanPreserve;
            public uint TargetWidth { get; set; }
            public new static SignedZeroInfNanPreserveImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new SignedZeroInfNanPreserveImpl();
                res.TargetWidth = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += TargetWidth.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                TargetWidth.Write(writer);
            }

            /// <summary>Returns a string that represents the SignedZeroInfNanPreserveImpl object.</summary>
            /// <returns>A string that represents the SignedZeroInfNanPreserveImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.SignedZeroInfNanPreserve({TargetWidth})";
            }
        }
        #endregion //SignedZeroInfNanPreserve

        #region RoundingModeRTE
        public static RoundingModeRTEImpl RoundingModeRTE(uint targetWidth)
        {
            return new RoundingModeRTEImpl(targetWidth);
            
        }

        internal class RoundingModeRTEImpl: ExecutionMode
        {
            public RoundingModeRTEImpl()
            {
            }

            public RoundingModeRTEImpl(uint targetWidth)
            {
                this.TargetWidth = targetWidth;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.RoundingModeRTE;
            public uint TargetWidth { get; set; }
            public new static RoundingModeRTEImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new RoundingModeRTEImpl();
                res.TargetWidth = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += TargetWidth.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                TargetWidth.Write(writer);
            }

            /// <summary>Returns a string that represents the RoundingModeRTEImpl object.</summary>
            /// <returns>A string that represents the RoundingModeRTEImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.RoundingModeRTE({TargetWidth})";
            }
        }
        #endregion //RoundingModeRTE

        #region RoundingModeRTZ
        public static RoundingModeRTZImpl RoundingModeRTZ(uint targetWidth)
        {
            return new RoundingModeRTZImpl(targetWidth);
            
        }

        internal class RoundingModeRTZImpl: ExecutionMode
        {
            public RoundingModeRTZImpl()
            {
            }

            public RoundingModeRTZImpl(uint targetWidth)
            {
                this.TargetWidth = targetWidth;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.RoundingModeRTZ;
            public uint TargetWidth { get; set; }
            public new static RoundingModeRTZImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new RoundingModeRTZImpl();
                res.TargetWidth = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += TargetWidth.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                TargetWidth.Write(writer);
            }

            /// <summary>Returns a string that represents the RoundingModeRTZImpl object.</summary>
            /// <returns>A string that represents the RoundingModeRTZImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.RoundingModeRTZ({TargetWidth})";
            }
        }
        #endregion //RoundingModeRTZ

        #region StencilRefReplacingEXT
        public static StencilRefReplacingEXTImpl StencilRefReplacingEXT()
        {
            return StencilRefReplacingEXTImpl.Instance;
            
        }

        internal class StencilRefReplacingEXTImpl: ExecutionMode
        {
            public static readonly StencilRefReplacingEXTImpl Instance = new StencilRefReplacingEXTImpl();
        
            private  StencilRefReplacingEXTImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.StencilRefReplacingEXT;
            public new static StencilRefReplacingEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StencilRefReplacingEXTImpl object.</summary>
            /// <returns>A string that represents the StencilRefReplacingEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.StencilRefReplacingEXT()";
            }
        }
        #endregion //StencilRefReplacingEXT

        #region OutputLinesNV
        public static OutputLinesNVImpl OutputLinesNV()
        {
            return OutputLinesNVImpl.Instance;
            
        }

        internal class OutputLinesNVImpl: ExecutionMode
        {
            public static readonly OutputLinesNVImpl Instance = new OutputLinesNVImpl();
        
            private  OutputLinesNVImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.OutputLinesNV;
            public new static OutputLinesNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the OutputLinesNVImpl object.</summary>
            /// <returns>A string that represents the OutputLinesNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.OutputLinesNV()";
            }
        }
        #endregion //OutputLinesNV

        #region OutputPrimitivesNV
        public static OutputPrimitivesNVImpl OutputPrimitivesNV(uint primitivecount)
        {
            return new OutputPrimitivesNVImpl(primitivecount);
            
        }

        internal class OutputPrimitivesNVImpl: ExecutionMode
        {
            public OutputPrimitivesNVImpl()
            {
            }

            public OutputPrimitivesNVImpl(uint primitivecount)
            {
                this.Primitivecount = primitivecount;
            }
            public override Enumerant Value => ExecutionMode.Enumerant.OutputPrimitivesNV;
            public uint Primitivecount { get; set; }
            public new static OutputPrimitivesNVImpl Parse(WordReader reader, uint wordCount)
            {
                var end = reader.Position+wordCount;
                var res = new OutputPrimitivesNVImpl();
                res.Primitivecount = Spv.LiteralInteger.Parse(reader, end-reader.Position);
                return res;
            }
            public override uint GetWordCount()
            {
                uint wordCount = base.GetWordCount();
                wordCount += Primitivecount.GetWordCount();
                return wordCount;
            }

            public override void Write(WordWriter writer)
            {
                base.Write(writer);
                Primitivecount.Write(writer);
            }

            /// <summary>Returns a string that represents the OutputPrimitivesNVImpl object.</summary>
            /// <returns>A string that represents the OutputPrimitivesNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.OutputPrimitivesNV({Primitivecount})";
            }
        }
        #endregion //OutputPrimitivesNV

        #region DerivativeGroupQuadsNV
        public static DerivativeGroupQuadsNVImpl DerivativeGroupQuadsNV()
        {
            return DerivativeGroupQuadsNVImpl.Instance;
            
        }

        internal class DerivativeGroupQuadsNVImpl: ExecutionMode
        {
            public static readonly DerivativeGroupQuadsNVImpl Instance = new DerivativeGroupQuadsNVImpl();
        
            private  DerivativeGroupQuadsNVImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.DerivativeGroupQuadsNV;
            public new static DerivativeGroupQuadsNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DerivativeGroupQuadsNVImpl object.</summary>
            /// <returns>A string that represents the DerivativeGroupQuadsNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.DerivativeGroupQuadsNV()";
            }
        }
        #endregion //DerivativeGroupQuadsNV

        #region DerivativeGroupLinearNV
        public static DerivativeGroupLinearNVImpl DerivativeGroupLinearNV()
        {
            return DerivativeGroupLinearNVImpl.Instance;
            
        }

        internal class DerivativeGroupLinearNVImpl: ExecutionMode
        {
            public static readonly DerivativeGroupLinearNVImpl Instance = new DerivativeGroupLinearNVImpl();
        
            private  DerivativeGroupLinearNVImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.DerivativeGroupLinearNV;
            public new static DerivativeGroupLinearNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DerivativeGroupLinearNVImpl object.</summary>
            /// <returns>A string that represents the DerivativeGroupLinearNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.DerivativeGroupLinearNV()";
            }
        }
        #endregion //DerivativeGroupLinearNV

        #region OutputTrianglesNV
        public static OutputTrianglesNVImpl OutputTrianglesNV()
        {
            return OutputTrianglesNVImpl.Instance;
            
        }

        internal class OutputTrianglesNVImpl: ExecutionMode
        {
            public static readonly OutputTrianglesNVImpl Instance = new OutputTrianglesNVImpl();
        
            private  OutputTrianglesNVImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.OutputTrianglesNV;
            public new static OutputTrianglesNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the OutputTrianglesNVImpl object.</summary>
            /// <returns>A string that represents the OutputTrianglesNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.OutputTrianglesNV()";
            }
        }
        #endregion //OutputTrianglesNV

        #region PixelInterlockOrderedEXT
        public static PixelInterlockOrderedEXTImpl PixelInterlockOrderedEXT()
        {
            return PixelInterlockOrderedEXTImpl.Instance;
            
        }

        internal class PixelInterlockOrderedEXTImpl: ExecutionMode
        {
            public static readonly PixelInterlockOrderedEXTImpl Instance = new PixelInterlockOrderedEXTImpl();
        
            private  PixelInterlockOrderedEXTImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.PixelInterlockOrderedEXT;
            public new static PixelInterlockOrderedEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PixelInterlockOrderedEXTImpl object.</summary>
            /// <returns>A string that represents the PixelInterlockOrderedEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.PixelInterlockOrderedEXT()";
            }
        }
        #endregion //PixelInterlockOrderedEXT

        #region PixelInterlockUnorderedEXT
        public static PixelInterlockUnorderedEXTImpl PixelInterlockUnorderedEXT()
        {
            return PixelInterlockUnorderedEXTImpl.Instance;
            
        }

        internal class PixelInterlockUnorderedEXTImpl: ExecutionMode
        {
            public static readonly PixelInterlockUnorderedEXTImpl Instance = new PixelInterlockUnorderedEXTImpl();
        
            private  PixelInterlockUnorderedEXTImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.PixelInterlockUnorderedEXT;
            public new static PixelInterlockUnorderedEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PixelInterlockUnorderedEXTImpl object.</summary>
            /// <returns>A string that represents the PixelInterlockUnorderedEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.PixelInterlockUnorderedEXT()";
            }
        }
        #endregion //PixelInterlockUnorderedEXT

        #region SampleInterlockOrderedEXT
        public static SampleInterlockOrderedEXTImpl SampleInterlockOrderedEXT()
        {
            return SampleInterlockOrderedEXTImpl.Instance;
            
        }

        internal class SampleInterlockOrderedEXTImpl: ExecutionMode
        {
            public static readonly SampleInterlockOrderedEXTImpl Instance = new SampleInterlockOrderedEXTImpl();
        
            private  SampleInterlockOrderedEXTImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.SampleInterlockOrderedEXT;
            public new static SampleInterlockOrderedEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampleInterlockOrderedEXTImpl object.</summary>
            /// <returns>A string that represents the SampleInterlockOrderedEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.SampleInterlockOrderedEXT()";
            }
        }
        #endregion //SampleInterlockOrderedEXT

        #region SampleInterlockUnorderedEXT
        public static SampleInterlockUnorderedEXTImpl SampleInterlockUnorderedEXT()
        {
            return SampleInterlockUnorderedEXTImpl.Instance;
            
        }

        internal class SampleInterlockUnorderedEXTImpl: ExecutionMode
        {
            public static readonly SampleInterlockUnorderedEXTImpl Instance = new SampleInterlockUnorderedEXTImpl();
        
            private  SampleInterlockUnorderedEXTImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.SampleInterlockUnorderedEXT;
            public new static SampleInterlockUnorderedEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampleInterlockUnorderedEXTImpl object.</summary>
            /// <returns>A string that represents the SampleInterlockUnorderedEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.SampleInterlockUnorderedEXT()";
            }
        }
        #endregion //SampleInterlockUnorderedEXT

        #region ShadingRateInterlockOrderedEXT
        public static ShadingRateInterlockOrderedEXTImpl ShadingRateInterlockOrderedEXT()
        {
            return ShadingRateInterlockOrderedEXTImpl.Instance;
            
        }

        internal class ShadingRateInterlockOrderedEXTImpl: ExecutionMode
        {
            public static readonly ShadingRateInterlockOrderedEXTImpl Instance = new ShadingRateInterlockOrderedEXTImpl();
        
            private  ShadingRateInterlockOrderedEXTImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.ShadingRateInterlockOrderedEXT;
            public new static ShadingRateInterlockOrderedEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShadingRateInterlockOrderedEXTImpl object.</summary>
            /// <returns>A string that represents the ShadingRateInterlockOrderedEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.ShadingRateInterlockOrderedEXT()";
            }
        }
        #endregion //ShadingRateInterlockOrderedEXT

        #region ShadingRateInterlockUnorderedEXT
        public static ShadingRateInterlockUnorderedEXTImpl ShadingRateInterlockUnorderedEXT()
        {
            return ShadingRateInterlockUnorderedEXTImpl.Instance;
            
        }

        internal class ShadingRateInterlockUnorderedEXTImpl: ExecutionMode
        {
            public static readonly ShadingRateInterlockUnorderedEXTImpl Instance = new ShadingRateInterlockUnorderedEXTImpl();
        
            private  ShadingRateInterlockUnorderedEXTImpl()
            {
            }
            public override Enumerant Value => ExecutionMode.Enumerant.ShadingRateInterlockUnorderedEXT;
            public new static ShadingRateInterlockUnorderedEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShadingRateInterlockUnorderedEXTImpl object.</summary>
            /// <returns>A string that represents the ShadingRateInterlockUnorderedEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionMode.ShadingRateInterlockUnorderedEXT()";
            }
        }
        #endregion //ShadingRateInterlockUnorderedEXT

        public abstract Enumerant Value { get; }

        public static ExecutionMode Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Invocations :
                    return InvocationsImpl.Parse(reader, wordCount - 1);
                case Enumerant.SpacingEqual :
                    return SpacingEqualImpl.Parse(reader, wordCount - 1);
                case Enumerant.SpacingFractionalEven :
                    return SpacingFractionalEvenImpl.Parse(reader, wordCount - 1);
                case Enumerant.SpacingFractionalOdd :
                    return SpacingFractionalOddImpl.Parse(reader, wordCount - 1);
                case Enumerant.VertexOrderCw :
                    return VertexOrderCwImpl.Parse(reader, wordCount - 1);
                case Enumerant.VertexOrderCcw :
                    return VertexOrderCcwImpl.Parse(reader, wordCount - 1);
                case Enumerant.PixelCenterInteger :
                    return PixelCenterIntegerImpl.Parse(reader, wordCount - 1);
                case Enumerant.OriginUpperLeft :
                    return OriginUpperLeftImpl.Parse(reader, wordCount - 1);
                case Enumerant.OriginLowerLeft :
                    return OriginLowerLeftImpl.Parse(reader, wordCount - 1);
                case Enumerant.EarlyFragmentTests :
                    return EarlyFragmentTestsImpl.Parse(reader, wordCount - 1);
                case Enumerant.PointMode :
                    return PointModeImpl.Parse(reader, wordCount - 1);
                case Enumerant.Xfb :
                    return XfbImpl.Parse(reader, wordCount - 1);
                case Enumerant.DepthReplacing :
                    return DepthReplacingImpl.Parse(reader, wordCount - 1);
                case Enumerant.DepthGreater :
                    return DepthGreaterImpl.Parse(reader, wordCount - 1);
                case Enumerant.DepthLess :
                    return DepthLessImpl.Parse(reader, wordCount - 1);
                case Enumerant.DepthUnchanged :
                    return DepthUnchangedImpl.Parse(reader, wordCount - 1);
                case Enumerant.LocalSize :
                    return LocalSizeImpl.Parse(reader, wordCount - 1);
                case Enumerant.LocalSizeHint :
                    return LocalSizeHintImpl.Parse(reader, wordCount - 1);
                case Enumerant.InputPoints :
                    return InputPointsImpl.Parse(reader, wordCount - 1);
                case Enumerant.InputLines :
                    return InputLinesImpl.Parse(reader, wordCount - 1);
                case Enumerant.InputLinesAdjacency :
                    return InputLinesAdjacencyImpl.Parse(reader, wordCount - 1);
                case Enumerant.Triangles :
                    return TrianglesImpl.Parse(reader, wordCount - 1);
                case Enumerant.InputTrianglesAdjacency :
                    return InputTrianglesAdjacencyImpl.Parse(reader, wordCount - 1);
                case Enumerant.Quads :
                    return QuadsImpl.Parse(reader, wordCount - 1);
                case Enumerant.Isolines :
                    return IsolinesImpl.Parse(reader, wordCount - 1);
                case Enumerant.OutputVertices :
                    return OutputVerticesImpl.Parse(reader, wordCount - 1);
                case Enumerant.OutputPoints :
                    return OutputPointsImpl.Parse(reader, wordCount - 1);
                case Enumerant.OutputLineStrip :
                    return OutputLineStripImpl.Parse(reader, wordCount - 1);
                case Enumerant.OutputTriangleStrip :
                    return OutputTriangleStripImpl.Parse(reader, wordCount - 1);
                case Enumerant.VecTypeHint :
                    return VecTypeHintImpl.Parse(reader, wordCount - 1);
                case Enumerant.ContractionOff :
                    return ContractionOffImpl.Parse(reader, wordCount - 1);
                case Enumerant.Initializer :
                    return InitializerImpl.Parse(reader, wordCount - 1);
                case Enumerant.Finalizer :
                    return FinalizerImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupSize :
                    return SubgroupSizeImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupsPerWorkgroup :
                    return SubgroupsPerWorkgroupImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupsPerWorkgroupId :
                    return SubgroupsPerWorkgroupIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.LocalSizeId :
                    return LocalSizeIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.LocalSizeHintId :
                    return LocalSizeHintIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.PostDepthCoverage :
                    return PostDepthCoverageImpl.Parse(reader, wordCount - 1);
                case Enumerant.DenormPreserve :
                    return DenormPreserveImpl.Parse(reader, wordCount - 1);
                case Enumerant.DenormFlushToZero :
                    return DenormFlushToZeroImpl.Parse(reader, wordCount - 1);
                case Enumerant.SignedZeroInfNanPreserve :
                    return SignedZeroInfNanPreserveImpl.Parse(reader, wordCount - 1);
                case Enumerant.RoundingModeRTE :
                    return RoundingModeRTEImpl.Parse(reader, wordCount - 1);
                case Enumerant.RoundingModeRTZ :
                    return RoundingModeRTZImpl.Parse(reader, wordCount - 1);
                case Enumerant.StencilRefReplacingEXT :
                    return StencilRefReplacingEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.OutputLinesNV :
                    return OutputLinesNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.OutputPrimitivesNV :
                    return OutputPrimitivesNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.DerivativeGroupQuadsNV :
                    return DerivativeGroupQuadsNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.DerivativeGroupLinearNV :
                    return DerivativeGroupLinearNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.OutputTrianglesNV :
                    return OutputTrianglesNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.PixelInterlockOrderedEXT :
                    return PixelInterlockOrderedEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.PixelInterlockUnorderedEXT :
                    return PixelInterlockUnorderedEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.SampleInterlockOrderedEXT :
                    return SampleInterlockOrderedEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.SampleInterlockUnorderedEXT :
                    return SampleInterlockUnorderedEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.ShadingRateInterlockOrderedEXT :
                    return ShadingRateInterlockOrderedEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.ShadingRateInterlockUnorderedEXT :
                    return ShadingRateInterlockUnorderedEXTImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown ExecutionMode "+id);
            }
        }
        
        public static ExecutionMode ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<ExecutionMode> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<ExecutionMode>();
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