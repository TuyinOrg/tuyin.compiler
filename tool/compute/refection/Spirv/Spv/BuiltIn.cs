using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class BuiltIn : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Shader)]
            Position = 0,
            [Capability(Capability.Enumerant.Shader)]
            PointSize = 1,
            [Capability(Capability.Enumerant.ClipDistance)]
            ClipDistance = 3,
            [Capability(Capability.Enumerant.CullDistance)]
            CullDistance = 4,
            [Capability(Capability.Enumerant.Shader)]
            VertexId = 5,
            [Capability(Capability.Enumerant.Shader)]
            InstanceId = 6,
            [Capability(Capability.Enumerant.Geometry)]
            [Capability(Capability.Enumerant.Tessellation)]
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            PrimitiveId = 7,
            [Capability(Capability.Enumerant.Geometry)]
            [Capability(Capability.Enumerant.Tessellation)]
            InvocationId = 8,
            [Capability(Capability.Enumerant.Geometry)]
            [Capability(Capability.Enumerant.ShaderLayer)]
            [Capability(Capability.Enumerant.ShaderViewportIndexLayerEXT)]
            Layer = 9,
            [Capability(Capability.Enumerant.MultiViewport)]
            [Capability(Capability.Enumerant.ShaderViewportIndex)]
            [Capability(Capability.Enumerant.ShaderViewportIndexLayerEXT)]
            ViewportIndex = 10,
            [Capability(Capability.Enumerant.Tessellation)]
            TessLevelOuter = 11,
            [Capability(Capability.Enumerant.Tessellation)]
            TessLevelInner = 12,
            [Capability(Capability.Enumerant.Tessellation)]
            TessCoord = 13,
            [Capability(Capability.Enumerant.Tessellation)]
            PatchVertices = 14,
            [Capability(Capability.Enumerant.Shader)]
            FragCoord = 15,
            [Capability(Capability.Enumerant.Shader)]
            PointCoord = 16,
            [Capability(Capability.Enumerant.Shader)]
            FrontFacing = 17,
            [Capability(Capability.Enumerant.SampleRateShading)]
            SampleId = 18,
            [Capability(Capability.Enumerant.SampleRateShading)]
            SamplePosition = 19,
            [Capability(Capability.Enumerant.Shader)]
            SampleMask = 20,
            [Capability(Capability.Enumerant.Shader)]
            FragDepth = 22,
            [Capability(Capability.Enumerant.Shader)]
            HelperInvocation = 23,
            NumWorkgroups = 24,
            WorkgroupSize = 25,
            WorkgroupId = 26,
            LocalInvocationId = 27,
            GlobalInvocationId = 28,
            LocalInvocationIndex = 29,
            [Capability(Capability.Enumerant.Kernel)]
            WorkDim = 30,
            [Capability(Capability.Enumerant.Kernel)]
            GlobalSize = 31,
            [Capability(Capability.Enumerant.Kernel)]
            EnqueuedWorkgroupSize = 32,
            [Capability(Capability.Enumerant.Kernel)]
            GlobalOffset = 33,
            [Capability(Capability.Enumerant.Kernel)]
            GlobalLinearId = 34,
            [Capability(Capability.Enumerant.Kernel)]
            [Capability(Capability.Enumerant.GroupNonUniform)]
            [Capability(Capability.Enumerant.SubgroupBallotKHR)]
            SubgroupSize = 36,
            [Capability(Capability.Enumerant.Kernel)]
            SubgroupMaxSize = 37,
            [Capability(Capability.Enumerant.Kernel)]
            [Capability(Capability.Enumerant.GroupNonUniform)]
            NumSubgroups = 38,
            [Capability(Capability.Enumerant.Kernel)]
            NumEnqueuedSubgroups = 39,
            [Capability(Capability.Enumerant.Kernel)]
            [Capability(Capability.Enumerant.GroupNonUniform)]
            SubgroupId = 40,
            [Capability(Capability.Enumerant.Kernel)]
            [Capability(Capability.Enumerant.GroupNonUniform)]
            [Capability(Capability.Enumerant.SubgroupBallotKHR)]
            SubgroupLocalInvocationId = 41,
            [Capability(Capability.Enumerant.Shader)]
            VertexIndex = 42,
            [Capability(Capability.Enumerant.Shader)]
            InstanceIndex = 43,
            [Capability(Capability.Enumerant.SubgroupBallotKHR)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            SubgroupEqMask = 4416,
            [Capability(Capability.Enumerant.SubgroupBallotKHR)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            SubgroupGeMask = 4417,
            [Capability(Capability.Enumerant.SubgroupBallotKHR)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            SubgroupGtMask = 4418,
            [Capability(Capability.Enumerant.SubgroupBallotKHR)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            SubgroupLeMask = 4419,
            [Capability(Capability.Enumerant.SubgroupBallotKHR)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            SubgroupLtMask = 4420,
            [Capability(Capability.Enumerant.SubgroupBallotKHR)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            SubgroupEqMaskKHR = 4416,
            [Capability(Capability.Enumerant.SubgroupBallotKHR)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            SubgroupGeMaskKHR = 4417,
            [Capability(Capability.Enumerant.SubgroupBallotKHR)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            SubgroupGtMaskKHR = 4418,
            [Capability(Capability.Enumerant.SubgroupBallotKHR)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            SubgroupLeMaskKHR = 4419,
            [Capability(Capability.Enumerant.SubgroupBallotKHR)]
            [Capability(Capability.Enumerant.GroupNonUniformBallot)]
            SubgroupLtMaskKHR = 4420,
            [Capability(Capability.Enumerant.DrawParameters)]
            BaseVertex = 4424,
            [Capability(Capability.Enumerant.DrawParameters)]
            BaseInstance = 4425,
            [Capability(Capability.Enumerant.DrawParameters)]
            [Capability(Capability.Enumerant.MeshShadingNV)]
            DrawIndex = 4426,
            [Capability(Capability.Enumerant.DeviceGroup)]
            DeviceIndex = 4438,
            [Capability(Capability.Enumerant.MultiView)]
            ViewIndex = 4440,
            BaryCoordNoPerspAMD = 4992,
            BaryCoordNoPerspCentroidAMD = 4993,
            BaryCoordNoPerspSampleAMD = 4994,
            BaryCoordSmoothAMD = 4995,
            BaryCoordSmoothCentroidAMD = 4996,
            BaryCoordSmoothSampleAMD = 4997,
            BaryCoordPullModelAMD = 4998,
            [Capability(Capability.Enumerant.StencilExportEXT)]
            FragStencilRefEXT = 5014,
            [Capability(Capability.Enumerant.ShaderViewportMaskNV)]
            [Capability(Capability.Enumerant.MeshShadingNV)]
            ViewportMaskNV = 5253,
            [Capability(Capability.Enumerant.ShaderStereoViewNV)]
            SecondaryPositionNV = 5257,
            [Capability(Capability.Enumerant.ShaderStereoViewNV)]
            SecondaryViewportMaskNV = 5258,
            [Capability(Capability.Enumerant.PerViewAttributesNV)]
            [Capability(Capability.Enumerant.MeshShadingNV)]
            PositionPerViewNV = 5261,
            [Capability(Capability.Enumerant.PerViewAttributesNV)]
            [Capability(Capability.Enumerant.MeshShadingNV)]
            ViewportMaskPerViewNV = 5262,
            [Capability(Capability.Enumerant.FragmentFullyCoveredEXT)]
            FullyCoveredEXT = 5264,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            TaskCountNV = 5274,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            PrimitiveCountNV = 5275,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            PrimitiveIndicesNV = 5276,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            ClipDistancePerViewNV = 5277,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            CullDistancePerViewNV = 5278,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            LayerPerViewNV = 5279,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            MeshViewCountNV = 5280,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            MeshViewIndicesNV = 5281,
            [Capability(Capability.Enumerant.FragmentBarycentricNV)]
            BaryCoordNV = 5286,
            [Capability(Capability.Enumerant.FragmentBarycentricNV)]
            BaryCoordNoPerspNV = 5287,
            [Capability(Capability.Enumerant.FragmentDensityEXT)]
            [Capability(Capability.Enumerant.ShadingRateNV)]
            FragSizeEXT = 5292,
            [Capability(Capability.Enumerant.ShadingRateNV)]
            [Capability(Capability.Enumerant.FragmentDensityEXT)]
            FragmentSizeNV = 5292,
            [Capability(Capability.Enumerant.FragmentDensityEXT)]
            [Capability(Capability.Enumerant.ShadingRateNV)]
            FragInvocationCountEXT = 5293,
            [Capability(Capability.Enumerant.ShadingRateNV)]
            [Capability(Capability.Enumerant.FragmentDensityEXT)]
            InvocationsPerPixelNV = 5293,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            LaunchIdNV = 5319,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            LaunchIdKHR = 5319,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            LaunchSizeNV = 5320,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            LaunchSizeKHR = 5320,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            WorldRayOriginNV = 5321,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            WorldRayOriginKHR = 5321,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            WorldRayDirectionNV = 5322,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            WorldRayDirectionKHR = 5322,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            ObjectRayOriginNV = 5323,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            ObjectRayOriginKHR = 5323,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            ObjectRayDirectionNV = 5324,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            ObjectRayDirectionKHR = 5324,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            RayTminNV = 5325,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            RayTminKHR = 5325,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            RayTmaxNV = 5326,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            RayTmaxKHR = 5326,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            InstanceCustomIndexNV = 5327,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            InstanceCustomIndexKHR = 5327,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            ObjectToWorldNV = 5330,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            ObjectToWorldKHR = 5330,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            WorldToObjectNV = 5331,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            WorldToObjectKHR = 5331,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            HitTNV = 5332,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            HitTKHR = 5332,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            HitKindNV = 5333,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            HitKindKHR = 5333,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            IncomingRayFlagsNV = 5351,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            IncomingRayFlagsKHR = 5351,
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            RayGeometryIndexKHR = 5352,
            [Capability(Capability.Enumerant.ShaderSMBuiltinsNV)]
            WarpsPerSMNV = 5374,
            [Capability(Capability.Enumerant.ShaderSMBuiltinsNV)]
            SMCountNV = 5375,
            [Capability(Capability.Enumerant.ShaderSMBuiltinsNV)]
            WarpIDNV = 5376,
            [Capability(Capability.Enumerant.ShaderSMBuiltinsNV)]
            SMIDNV = 5377,
        }

        #region Position
        public static PositionImpl Position()
        {
            return PositionImpl.Instance;
            
        }

        internal class PositionImpl: BuiltIn
        {
            public static readonly PositionImpl Instance = new PositionImpl();
        
            private  PositionImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.Position;
            public new static PositionImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PositionImpl object.</summary>
            /// <returns>A string that represents the PositionImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.Position()";
            }
        }
        #endregion //Position

        #region PointSize
        public static PointSizeImpl PointSize()
        {
            return PointSizeImpl.Instance;
            
        }

        internal class PointSizeImpl: BuiltIn
        {
            public static readonly PointSizeImpl Instance = new PointSizeImpl();
        
            private  PointSizeImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.PointSize;
            public new static PointSizeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PointSizeImpl object.</summary>
            /// <returns>A string that represents the PointSizeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.PointSize()";
            }
        }
        #endregion //PointSize

        #region ClipDistance
        public static ClipDistanceImpl ClipDistance()
        {
            return ClipDistanceImpl.Instance;
            
        }

        internal class ClipDistanceImpl: BuiltIn
        {
            public static readonly ClipDistanceImpl Instance = new ClipDistanceImpl();
        
            private  ClipDistanceImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.ClipDistance;
            public new static ClipDistanceImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ClipDistanceImpl object.</summary>
            /// <returns>A string that represents the ClipDistanceImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.ClipDistance()";
            }
        }
        #endregion //ClipDistance

        #region CullDistance
        public static CullDistanceImpl CullDistance()
        {
            return CullDistanceImpl.Instance;
            
        }

        internal class CullDistanceImpl: BuiltIn
        {
            public static readonly CullDistanceImpl Instance = new CullDistanceImpl();
        
            private  CullDistanceImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.CullDistance;
            public new static CullDistanceImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CullDistanceImpl object.</summary>
            /// <returns>A string that represents the CullDistanceImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.CullDistance()";
            }
        }
        #endregion //CullDistance

        #region VertexId
        public static VertexIdImpl VertexId()
        {
            return VertexIdImpl.Instance;
            
        }

        internal class VertexIdImpl: BuiltIn
        {
            public static readonly VertexIdImpl Instance = new VertexIdImpl();
        
            private  VertexIdImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.VertexId;
            public new static VertexIdImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VertexIdImpl object.</summary>
            /// <returns>A string that represents the VertexIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.VertexId()";
            }
        }
        #endregion //VertexId

        #region InstanceId
        public static InstanceIdImpl InstanceId()
        {
            return InstanceIdImpl.Instance;
            
        }

        internal class InstanceIdImpl: BuiltIn
        {
            public static readonly InstanceIdImpl Instance = new InstanceIdImpl();
        
            private  InstanceIdImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.InstanceId;
            public new static InstanceIdImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InstanceIdImpl object.</summary>
            /// <returns>A string that represents the InstanceIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.InstanceId()";
            }
        }
        #endregion //InstanceId

        #region PrimitiveId
        public static PrimitiveIdImpl PrimitiveId()
        {
            return PrimitiveIdImpl.Instance;
            
        }

        internal class PrimitiveIdImpl: BuiltIn
        {
            public static readonly PrimitiveIdImpl Instance = new PrimitiveIdImpl();
        
            private  PrimitiveIdImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.PrimitiveId;
            public new static PrimitiveIdImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PrimitiveIdImpl object.</summary>
            /// <returns>A string that represents the PrimitiveIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.PrimitiveId()";
            }
        }
        #endregion //PrimitiveId

        #region InvocationId
        public static InvocationIdImpl InvocationId()
        {
            return InvocationIdImpl.Instance;
            
        }

        internal class InvocationIdImpl: BuiltIn
        {
            public static readonly InvocationIdImpl Instance = new InvocationIdImpl();
        
            private  InvocationIdImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.InvocationId;
            public new static InvocationIdImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InvocationIdImpl object.</summary>
            /// <returns>A string that represents the InvocationIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.InvocationId()";
            }
        }
        #endregion //InvocationId

        #region Layer
        public static LayerImpl Layer()
        {
            return LayerImpl.Instance;
            
        }

        internal class LayerImpl: BuiltIn
        {
            public static readonly LayerImpl Instance = new LayerImpl();
        
            private  LayerImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.Layer;
            public new static LayerImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LayerImpl object.</summary>
            /// <returns>A string that represents the LayerImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.Layer()";
            }
        }
        #endregion //Layer

        #region ViewportIndex
        public static ViewportIndexImpl ViewportIndex()
        {
            return ViewportIndexImpl.Instance;
            
        }

        internal class ViewportIndexImpl: BuiltIn
        {
            public static readonly ViewportIndexImpl Instance = new ViewportIndexImpl();
        
            private  ViewportIndexImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.ViewportIndex;
            public new static ViewportIndexImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ViewportIndexImpl object.</summary>
            /// <returns>A string that represents the ViewportIndexImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.ViewportIndex()";
            }
        }
        #endregion //ViewportIndex

        #region TessLevelOuter
        public static TessLevelOuterImpl TessLevelOuter()
        {
            return TessLevelOuterImpl.Instance;
            
        }

        internal class TessLevelOuterImpl: BuiltIn
        {
            public static readonly TessLevelOuterImpl Instance = new TessLevelOuterImpl();
        
            private  TessLevelOuterImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.TessLevelOuter;
            public new static TessLevelOuterImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the TessLevelOuterImpl object.</summary>
            /// <returns>A string that represents the TessLevelOuterImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.TessLevelOuter()";
            }
        }
        #endregion //TessLevelOuter

        #region TessLevelInner
        public static TessLevelInnerImpl TessLevelInner()
        {
            return TessLevelInnerImpl.Instance;
            
        }

        internal class TessLevelInnerImpl: BuiltIn
        {
            public static readonly TessLevelInnerImpl Instance = new TessLevelInnerImpl();
        
            private  TessLevelInnerImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.TessLevelInner;
            public new static TessLevelInnerImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the TessLevelInnerImpl object.</summary>
            /// <returns>A string that represents the TessLevelInnerImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.TessLevelInner()";
            }
        }
        #endregion //TessLevelInner

        #region TessCoord
        public static TessCoordImpl TessCoord()
        {
            return TessCoordImpl.Instance;
            
        }

        internal class TessCoordImpl: BuiltIn
        {
            public static readonly TessCoordImpl Instance = new TessCoordImpl();
        
            private  TessCoordImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.TessCoord;
            public new static TessCoordImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the TessCoordImpl object.</summary>
            /// <returns>A string that represents the TessCoordImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.TessCoord()";
            }
        }
        #endregion //TessCoord

        #region PatchVertices
        public static PatchVerticesImpl PatchVertices()
        {
            return PatchVerticesImpl.Instance;
            
        }

        internal class PatchVerticesImpl: BuiltIn
        {
            public static readonly PatchVerticesImpl Instance = new PatchVerticesImpl();
        
            private  PatchVerticesImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.PatchVertices;
            public new static PatchVerticesImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PatchVerticesImpl object.</summary>
            /// <returns>A string that represents the PatchVerticesImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.PatchVertices()";
            }
        }
        #endregion //PatchVertices

        #region FragCoord
        public static FragCoordImpl FragCoord()
        {
            return FragCoordImpl.Instance;
            
        }

        internal class FragCoordImpl: BuiltIn
        {
            public static readonly FragCoordImpl Instance = new FragCoordImpl();
        
            private  FragCoordImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.FragCoord;
            public new static FragCoordImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragCoordImpl object.</summary>
            /// <returns>A string that represents the FragCoordImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.FragCoord()";
            }
        }
        #endregion //FragCoord

        #region PointCoord
        public static PointCoordImpl PointCoord()
        {
            return PointCoordImpl.Instance;
            
        }

        internal class PointCoordImpl: BuiltIn
        {
            public static readonly PointCoordImpl Instance = new PointCoordImpl();
        
            private  PointCoordImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.PointCoord;
            public new static PointCoordImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PointCoordImpl object.</summary>
            /// <returns>A string that represents the PointCoordImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.PointCoord()";
            }
        }
        #endregion //PointCoord

        #region FrontFacing
        public static FrontFacingImpl FrontFacing()
        {
            return FrontFacingImpl.Instance;
            
        }

        internal class FrontFacingImpl: BuiltIn
        {
            public static readonly FrontFacingImpl Instance = new FrontFacingImpl();
        
            private  FrontFacingImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.FrontFacing;
            public new static FrontFacingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FrontFacingImpl object.</summary>
            /// <returns>A string that represents the FrontFacingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.FrontFacing()";
            }
        }
        #endregion //FrontFacing

        #region SampleId
        public static SampleIdImpl SampleId()
        {
            return SampleIdImpl.Instance;
            
        }

        internal class SampleIdImpl: BuiltIn
        {
            public static readonly SampleIdImpl Instance = new SampleIdImpl();
        
            private  SampleIdImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SampleId;
            public new static SampleIdImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampleIdImpl object.</summary>
            /// <returns>A string that represents the SampleIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SampleId()";
            }
        }
        #endregion //SampleId

        #region SamplePosition
        public static SamplePositionImpl SamplePosition()
        {
            return SamplePositionImpl.Instance;
            
        }

        internal class SamplePositionImpl: BuiltIn
        {
            public static readonly SamplePositionImpl Instance = new SamplePositionImpl();
        
            private  SamplePositionImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SamplePosition;
            public new static SamplePositionImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SamplePositionImpl object.</summary>
            /// <returns>A string that represents the SamplePositionImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SamplePosition()";
            }
        }
        #endregion //SamplePosition

        #region SampleMask
        public static SampleMaskImpl SampleMask()
        {
            return SampleMaskImpl.Instance;
            
        }

        internal class SampleMaskImpl: BuiltIn
        {
            public static readonly SampleMaskImpl Instance = new SampleMaskImpl();
        
            private  SampleMaskImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SampleMask;
            public new static SampleMaskImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampleMaskImpl object.</summary>
            /// <returns>A string that represents the SampleMaskImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SampleMask()";
            }
        }
        #endregion //SampleMask

        #region FragDepth
        public static FragDepthImpl FragDepth()
        {
            return FragDepthImpl.Instance;
            
        }

        internal class FragDepthImpl: BuiltIn
        {
            public static readonly FragDepthImpl Instance = new FragDepthImpl();
        
            private  FragDepthImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.FragDepth;
            public new static FragDepthImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragDepthImpl object.</summary>
            /// <returns>A string that represents the FragDepthImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.FragDepth()";
            }
        }
        #endregion //FragDepth

        #region HelperInvocation
        public static HelperInvocationImpl HelperInvocation()
        {
            return HelperInvocationImpl.Instance;
            
        }

        internal class HelperInvocationImpl: BuiltIn
        {
            public static readonly HelperInvocationImpl Instance = new HelperInvocationImpl();
        
            private  HelperInvocationImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.HelperInvocation;
            public new static HelperInvocationImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the HelperInvocationImpl object.</summary>
            /// <returns>A string that represents the HelperInvocationImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.HelperInvocation()";
            }
        }
        #endregion //HelperInvocation

        #region NumWorkgroups
        public static NumWorkgroupsImpl NumWorkgroups()
        {
            return NumWorkgroupsImpl.Instance;
            
        }

        internal class NumWorkgroupsImpl: BuiltIn
        {
            public static readonly NumWorkgroupsImpl Instance = new NumWorkgroupsImpl();
        
            private  NumWorkgroupsImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.NumWorkgroups;
            public new static NumWorkgroupsImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NumWorkgroupsImpl object.</summary>
            /// <returns>A string that represents the NumWorkgroupsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.NumWorkgroups()";
            }
        }
        #endregion //NumWorkgroups

        #region WorkgroupSize
        public static WorkgroupSizeImpl WorkgroupSize()
        {
            return WorkgroupSizeImpl.Instance;
            
        }

        internal class WorkgroupSizeImpl: BuiltIn
        {
            public static readonly WorkgroupSizeImpl Instance = new WorkgroupSizeImpl();
        
            private  WorkgroupSizeImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.WorkgroupSize;
            public new static WorkgroupSizeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WorkgroupSizeImpl object.</summary>
            /// <returns>A string that represents the WorkgroupSizeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.WorkgroupSize()";
            }
        }
        #endregion //WorkgroupSize

        #region WorkgroupId
        public static WorkgroupIdImpl WorkgroupId()
        {
            return WorkgroupIdImpl.Instance;
            
        }

        internal class WorkgroupIdImpl: BuiltIn
        {
            public static readonly WorkgroupIdImpl Instance = new WorkgroupIdImpl();
        
            private  WorkgroupIdImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.WorkgroupId;
            public new static WorkgroupIdImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WorkgroupIdImpl object.</summary>
            /// <returns>A string that represents the WorkgroupIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.WorkgroupId()";
            }
        }
        #endregion //WorkgroupId

        #region LocalInvocationId
        public static LocalInvocationIdImpl LocalInvocationId()
        {
            return LocalInvocationIdImpl.Instance;
            
        }

        internal class LocalInvocationIdImpl: BuiltIn
        {
            public static readonly LocalInvocationIdImpl Instance = new LocalInvocationIdImpl();
        
            private  LocalInvocationIdImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.LocalInvocationId;
            public new static LocalInvocationIdImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LocalInvocationIdImpl object.</summary>
            /// <returns>A string that represents the LocalInvocationIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.LocalInvocationId()";
            }
        }
        #endregion //LocalInvocationId

        #region GlobalInvocationId
        public static GlobalInvocationIdImpl GlobalInvocationId()
        {
            return GlobalInvocationIdImpl.Instance;
            
        }

        internal class GlobalInvocationIdImpl: BuiltIn
        {
            public static readonly GlobalInvocationIdImpl Instance = new GlobalInvocationIdImpl();
        
            private  GlobalInvocationIdImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.GlobalInvocationId;
            public new static GlobalInvocationIdImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GlobalInvocationIdImpl object.</summary>
            /// <returns>A string that represents the GlobalInvocationIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.GlobalInvocationId()";
            }
        }
        #endregion //GlobalInvocationId

        #region LocalInvocationIndex
        public static LocalInvocationIndexImpl LocalInvocationIndex()
        {
            return LocalInvocationIndexImpl.Instance;
            
        }

        internal class LocalInvocationIndexImpl: BuiltIn
        {
            public static readonly LocalInvocationIndexImpl Instance = new LocalInvocationIndexImpl();
        
            private  LocalInvocationIndexImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.LocalInvocationIndex;
            public new static LocalInvocationIndexImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LocalInvocationIndexImpl object.</summary>
            /// <returns>A string that represents the LocalInvocationIndexImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.LocalInvocationIndex()";
            }
        }
        #endregion //LocalInvocationIndex

        #region WorkDim
        public static WorkDimImpl WorkDim()
        {
            return WorkDimImpl.Instance;
            
        }

        internal class WorkDimImpl: BuiltIn
        {
            public static readonly WorkDimImpl Instance = new WorkDimImpl();
        
            private  WorkDimImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.WorkDim;
            public new static WorkDimImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WorkDimImpl object.</summary>
            /// <returns>A string that represents the WorkDimImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.WorkDim()";
            }
        }
        #endregion //WorkDim

        #region GlobalSize
        public static GlobalSizeImpl GlobalSize()
        {
            return GlobalSizeImpl.Instance;
            
        }

        internal class GlobalSizeImpl: BuiltIn
        {
            public static readonly GlobalSizeImpl Instance = new GlobalSizeImpl();
        
            private  GlobalSizeImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.GlobalSize;
            public new static GlobalSizeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GlobalSizeImpl object.</summary>
            /// <returns>A string that represents the GlobalSizeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.GlobalSize()";
            }
        }
        #endregion //GlobalSize

        #region EnqueuedWorkgroupSize
        public static EnqueuedWorkgroupSizeImpl EnqueuedWorkgroupSize()
        {
            return EnqueuedWorkgroupSizeImpl.Instance;
            
        }

        internal class EnqueuedWorkgroupSizeImpl: BuiltIn
        {
            public static readonly EnqueuedWorkgroupSizeImpl Instance = new EnqueuedWorkgroupSizeImpl();
        
            private  EnqueuedWorkgroupSizeImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.EnqueuedWorkgroupSize;
            public new static EnqueuedWorkgroupSizeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the EnqueuedWorkgroupSizeImpl object.</summary>
            /// <returns>A string that represents the EnqueuedWorkgroupSizeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.EnqueuedWorkgroupSize()";
            }
        }
        #endregion //EnqueuedWorkgroupSize

        #region GlobalOffset
        public static GlobalOffsetImpl GlobalOffset()
        {
            return GlobalOffsetImpl.Instance;
            
        }

        internal class GlobalOffsetImpl: BuiltIn
        {
            public static readonly GlobalOffsetImpl Instance = new GlobalOffsetImpl();
        
            private  GlobalOffsetImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.GlobalOffset;
            public new static GlobalOffsetImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GlobalOffsetImpl object.</summary>
            /// <returns>A string that represents the GlobalOffsetImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.GlobalOffset()";
            }
        }
        #endregion //GlobalOffset

        #region GlobalLinearId
        public static GlobalLinearIdImpl GlobalLinearId()
        {
            return GlobalLinearIdImpl.Instance;
            
        }

        internal class GlobalLinearIdImpl: BuiltIn
        {
            public static readonly GlobalLinearIdImpl Instance = new GlobalLinearIdImpl();
        
            private  GlobalLinearIdImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.GlobalLinearId;
            public new static GlobalLinearIdImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GlobalLinearIdImpl object.</summary>
            /// <returns>A string that represents the GlobalLinearIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.GlobalLinearId()";
            }
        }
        #endregion //GlobalLinearId

        #region SubgroupSize
        public static SubgroupSizeImpl SubgroupSize()
        {
            return SubgroupSizeImpl.Instance;
            
        }

        internal class SubgroupSizeImpl: BuiltIn
        {
            public static readonly SubgroupSizeImpl Instance = new SubgroupSizeImpl();
        
            private  SubgroupSizeImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupSize;
            public new static SubgroupSizeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupSizeImpl object.</summary>
            /// <returns>A string that represents the SubgroupSizeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupSize()";
            }
        }
        #endregion //SubgroupSize

        #region SubgroupMaxSize
        public static SubgroupMaxSizeImpl SubgroupMaxSize()
        {
            return SubgroupMaxSizeImpl.Instance;
            
        }

        internal class SubgroupMaxSizeImpl: BuiltIn
        {
            public static readonly SubgroupMaxSizeImpl Instance = new SubgroupMaxSizeImpl();
        
            private  SubgroupMaxSizeImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupMaxSize;
            public new static SubgroupMaxSizeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupMaxSizeImpl object.</summary>
            /// <returns>A string that represents the SubgroupMaxSizeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupMaxSize()";
            }
        }
        #endregion //SubgroupMaxSize

        #region NumSubgroups
        public static NumSubgroupsImpl NumSubgroups()
        {
            return NumSubgroupsImpl.Instance;
            
        }

        internal class NumSubgroupsImpl: BuiltIn
        {
            public static readonly NumSubgroupsImpl Instance = new NumSubgroupsImpl();
        
            private  NumSubgroupsImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.NumSubgroups;
            public new static NumSubgroupsImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NumSubgroupsImpl object.</summary>
            /// <returns>A string that represents the NumSubgroupsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.NumSubgroups()";
            }
        }
        #endregion //NumSubgroups

        #region NumEnqueuedSubgroups
        public static NumEnqueuedSubgroupsImpl NumEnqueuedSubgroups()
        {
            return NumEnqueuedSubgroupsImpl.Instance;
            
        }

        internal class NumEnqueuedSubgroupsImpl: BuiltIn
        {
            public static readonly NumEnqueuedSubgroupsImpl Instance = new NumEnqueuedSubgroupsImpl();
        
            private  NumEnqueuedSubgroupsImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.NumEnqueuedSubgroups;
            public new static NumEnqueuedSubgroupsImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NumEnqueuedSubgroupsImpl object.</summary>
            /// <returns>A string that represents the NumEnqueuedSubgroupsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.NumEnqueuedSubgroups()";
            }
        }
        #endregion //NumEnqueuedSubgroups

        #region SubgroupId
        public static SubgroupIdImpl SubgroupId()
        {
            return SubgroupIdImpl.Instance;
            
        }

        internal class SubgroupIdImpl: BuiltIn
        {
            public static readonly SubgroupIdImpl Instance = new SubgroupIdImpl();
        
            private  SubgroupIdImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupId;
            public new static SubgroupIdImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupIdImpl object.</summary>
            /// <returns>A string that represents the SubgroupIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupId()";
            }
        }
        #endregion //SubgroupId

        #region SubgroupLocalInvocationId
        public static SubgroupLocalInvocationIdImpl SubgroupLocalInvocationId()
        {
            return SubgroupLocalInvocationIdImpl.Instance;
            
        }

        internal class SubgroupLocalInvocationIdImpl: BuiltIn
        {
            public static readonly SubgroupLocalInvocationIdImpl Instance = new SubgroupLocalInvocationIdImpl();
        
            private  SubgroupLocalInvocationIdImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupLocalInvocationId;
            public new static SubgroupLocalInvocationIdImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupLocalInvocationIdImpl object.</summary>
            /// <returns>A string that represents the SubgroupLocalInvocationIdImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupLocalInvocationId()";
            }
        }
        #endregion //SubgroupLocalInvocationId

        #region VertexIndex
        public static VertexIndexImpl VertexIndex()
        {
            return VertexIndexImpl.Instance;
            
        }

        internal class VertexIndexImpl: BuiltIn
        {
            public static readonly VertexIndexImpl Instance = new VertexIndexImpl();
        
            private  VertexIndexImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.VertexIndex;
            public new static VertexIndexImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VertexIndexImpl object.</summary>
            /// <returns>A string that represents the VertexIndexImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.VertexIndex()";
            }
        }
        #endregion //VertexIndex

        #region InstanceIndex
        public static InstanceIndexImpl InstanceIndex()
        {
            return InstanceIndexImpl.Instance;
            
        }

        internal class InstanceIndexImpl: BuiltIn
        {
            public static readonly InstanceIndexImpl Instance = new InstanceIndexImpl();
        
            private  InstanceIndexImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.InstanceIndex;
            public new static InstanceIndexImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InstanceIndexImpl object.</summary>
            /// <returns>A string that represents the InstanceIndexImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.InstanceIndex()";
            }
        }
        #endregion //InstanceIndex

        #region SubgroupEqMask
        public static SubgroupEqMaskImpl SubgroupEqMask()
        {
            return SubgroupEqMaskImpl.Instance;
            
        }

        internal class SubgroupEqMaskImpl: BuiltIn
        {
            public static readonly SubgroupEqMaskImpl Instance = new SubgroupEqMaskImpl();
        
            private  SubgroupEqMaskImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupEqMask;
            public new static SubgroupEqMaskImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupEqMaskImpl object.</summary>
            /// <returns>A string that represents the SubgroupEqMaskImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupEqMask()";
            }
        }
        #endregion //SubgroupEqMask

        #region SubgroupGeMask
        public static SubgroupGeMaskImpl SubgroupGeMask()
        {
            return SubgroupGeMaskImpl.Instance;
            
        }

        internal class SubgroupGeMaskImpl: BuiltIn
        {
            public static readonly SubgroupGeMaskImpl Instance = new SubgroupGeMaskImpl();
        
            private  SubgroupGeMaskImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupGeMask;
            public new static SubgroupGeMaskImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupGeMaskImpl object.</summary>
            /// <returns>A string that represents the SubgroupGeMaskImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupGeMask()";
            }
        }
        #endregion //SubgroupGeMask

        #region SubgroupGtMask
        public static SubgroupGtMaskImpl SubgroupGtMask()
        {
            return SubgroupGtMaskImpl.Instance;
            
        }

        internal class SubgroupGtMaskImpl: BuiltIn
        {
            public static readonly SubgroupGtMaskImpl Instance = new SubgroupGtMaskImpl();
        
            private  SubgroupGtMaskImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupGtMask;
            public new static SubgroupGtMaskImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupGtMaskImpl object.</summary>
            /// <returns>A string that represents the SubgroupGtMaskImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupGtMask()";
            }
        }
        #endregion //SubgroupGtMask

        #region SubgroupLeMask
        public static SubgroupLeMaskImpl SubgroupLeMask()
        {
            return SubgroupLeMaskImpl.Instance;
            
        }

        internal class SubgroupLeMaskImpl: BuiltIn
        {
            public static readonly SubgroupLeMaskImpl Instance = new SubgroupLeMaskImpl();
        
            private  SubgroupLeMaskImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupLeMask;
            public new static SubgroupLeMaskImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupLeMaskImpl object.</summary>
            /// <returns>A string that represents the SubgroupLeMaskImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupLeMask()";
            }
        }
        #endregion //SubgroupLeMask

        #region SubgroupLtMask
        public static SubgroupLtMaskImpl SubgroupLtMask()
        {
            return SubgroupLtMaskImpl.Instance;
            
        }

        internal class SubgroupLtMaskImpl: BuiltIn
        {
            public static readonly SubgroupLtMaskImpl Instance = new SubgroupLtMaskImpl();
        
            private  SubgroupLtMaskImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupLtMask;
            public new static SubgroupLtMaskImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupLtMaskImpl object.</summary>
            /// <returns>A string that represents the SubgroupLtMaskImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupLtMask()";
            }
        }
        #endregion //SubgroupLtMask

        #region SubgroupEqMaskKHR
        public static SubgroupEqMaskKHRImpl SubgroupEqMaskKHR()
        {
            return SubgroupEqMaskKHRImpl.Instance;
            
        }

        internal class SubgroupEqMaskKHRImpl: BuiltIn
        {
            public static readonly SubgroupEqMaskKHRImpl Instance = new SubgroupEqMaskKHRImpl();
        
            private  SubgroupEqMaskKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupEqMaskKHR;
            public new static SubgroupEqMaskKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupEqMaskKHRImpl object.</summary>
            /// <returns>A string that represents the SubgroupEqMaskKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupEqMaskKHR()";
            }
        }
        #endregion //SubgroupEqMaskKHR

        #region SubgroupGeMaskKHR
        public static SubgroupGeMaskKHRImpl SubgroupGeMaskKHR()
        {
            return SubgroupGeMaskKHRImpl.Instance;
            
        }

        internal class SubgroupGeMaskKHRImpl: BuiltIn
        {
            public static readonly SubgroupGeMaskKHRImpl Instance = new SubgroupGeMaskKHRImpl();
        
            private  SubgroupGeMaskKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupGeMaskKHR;
            public new static SubgroupGeMaskKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupGeMaskKHRImpl object.</summary>
            /// <returns>A string that represents the SubgroupGeMaskKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupGeMaskKHR()";
            }
        }
        #endregion //SubgroupGeMaskKHR

        #region SubgroupGtMaskKHR
        public static SubgroupGtMaskKHRImpl SubgroupGtMaskKHR()
        {
            return SubgroupGtMaskKHRImpl.Instance;
            
        }

        internal class SubgroupGtMaskKHRImpl: BuiltIn
        {
            public static readonly SubgroupGtMaskKHRImpl Instance = new SubgroupGtMaskKHRImpl();
        
            private  SubgroupGtMaskKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupGtMaskKHR;
            public new static SubgroupGtMaskKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupGtMaskKHRImpl object.</summary>
            /// <returns>A string that represents the SubgroupGtMaskKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupGtMaskKHR()";
            }
        }
        #endregion //SubgroupGtMaskKHR

        #region SubgroupLeMaskKHR
        public static SubgroupLeMaskKHRImpl SubgroupLeMaskKHR()
        {
            return SubgroupLeMaskKHRImpl.Instance;
            
        }

        internal class SubgroupLeMaskKHRImpl: BuiltIn
        {
            public static readonly SubgroupLeMaskKHRImpl Instance = new SubgroupLeMaskKHRImpl();
        
            private  SubgroupLeMaskKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupLeMaskKHR;
            public new static SubgroupLeMaskKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupLeMaskKHRImpl object.</summary>
            /// <returns>A string that represents the SubgroupLeMaskKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupLeMaskKHR()";
            }
        }
        #endregion //SubgroupLeMaskKHR

        #region SubgroupLtMaskKHR
        public static SubgroupLtMaskKHRImpl SubgroupLtMaskKHR()
        {
            return SubgroupLtMaskKHRImpl.Instance;
            
        }

        internal class SubgroupLtMaskKHRImpl: BuiltIn
        {
            public static readonly SubgroupLtMaskKHRImpl Instance = new SubgroupLtMaskKHRImpl();
        
            private  SubgroupLtMaskKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SubgroupLtMaskKHR;
            public new static SubgroupLtMaskKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupLtMaskKHRImpl object.</summary>
            /// <returns>A string that represents the SubgroupLtMaskKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SubgroupLtMaskKHR()";
            }
        }
        #endregion //SubgroupLtMaskKHR

        #region BaseVertex
        public static BaseVertexImpl BaseVertex()
        {
            return BaseVertexImpl.Instance;
            
        }

        internal class BaseVertexImpl: BuiltIn
        {
            public static readonly BaseVertexImpl Instance = new BaseVertexImpl();
        
            private  BaseVertexImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.BaseVertex;
            public new static BaseVertexImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BaseVertexImpl object.</summary>
            /// <returns>A string that represents the BaseVertexImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.BaseVertex()";
            }
        }
        #endregion //BaseVertex

        #region BaseInstance
        public static BaseInstanceImpl BaseInstance()
        {
            return BaseInstanceImpl.Instance;
            
        }

        internal class BaseInstanceImpl: BuiltIn
        {
            public static readonly BaseInstanceImpl Instance = new BaseInstanceImpl();
        
            private  BaseInstanceImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.BaseInstance;
            public new static BaseInstanceImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BaseInstanceImpl object.</summary>
            /// <returns>A string that represents the BaseInstanceImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.BaseInstance()";
            }
        }
        #endregion //BaseInstance

        #region DrawIndex
        public static DrawIndexImpl DrawIndex()
        {
            return DrawIndexImpl.Instance;
            
        }

        internal class DrawIndexImpl: BuiltIn
        {
            public static readonly DrawIndexImpl Instance = new DrawIndexImpl();
        
            private  DrawIndexImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.DrawIndex;
            public new static DrawIndexImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DrawIndexImpl object.</summary>
            /// <returns>A string that represents the DrawIndexImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.DrawIndex()";
            }
        }
        #endregion //DrawIndex

        #region DeviceIndex
        public static DeviceIndexImpl DeviceIndex()
        {
            return DeviceIndexImpl.Instance;
            
        }

        internal class DeviceIndexImpl: BuiltIn
        {
            public static readonly DeviceIndexImpl Instance = new DeviceIndexImpl();
        
            private  DeviceIndexImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.DeviceIndex;
            public new static DeviceIndexImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DeviceIndexImpl object.</summary>
            /// <returns>A string that represents the DeviceIndexImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.DeviceIndex()";
            }
        }
        #endregion //DeviceIndex

        #region ViewIndex
        public static ViewIndexImpl ViewIndex()
        {
            return ViewIndexImpl.Instance;
            
        }

        internal class ViewIndexImpl: BuiltIn
        {
            public static readonly ViewIndexImpl Instance = new ViewIndexImpl();
        
            private  ViewIndexImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.ViewIndex;
            public new static ViewIndexImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ViewIndexImpl object.</summary>
            /// <returns>A string that represents the ViewIndexImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.ViewIndex()";
            }
        }
        #endregion //ViewIndex

        #region BaryCoordNoPerspAMD
        public static BaryCoordNoPerspAMDImpl BaryCoordNoPerspAMD()
        {
            return BaryCoordNoPerspAMDImpl.Instance;
            
        }

        internal class BaryCoordNoPerspAMDImpl: BuiltIn
        {
            public static readonly BaryCoordNoPerspAMDImpl Instance = new BaryCoordNoPerspAMDImpl();
        
            private  BaryCoordNoPerspAMDImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.BaryCoordNoPerspAMD;
            public new static BaryCoordNoPerspAMDImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BaryCoordNoPerspAMDImpl object.</summary>
            /// <returns>A string that represents the BaryCoordNoPerspAMDImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.BaryCoordNoPerspAMD()";
            }
        }
        #endregion //BaryCoordNoPerspAMD

        #region BaryCoordNoPerspCentroidAMD
        public static BaryCoordNoPerspCentroidAMDImpl BaryCoordNoPerspCentroidAMD()
        {
            return BaryCoordNoPerspCentroidAMDImpl.Instance;
            
        }

        internal class BaryCoordNoPerspCentroidAMDImpl: BuiltIn
        {
            public static readonly BaryCoordNoPerspCentroidAMDImpl Instance = new BaryCoordNoPerspCentroidAMDImpl();
        
            private  BaryCoordNoPerspCentroidAMDImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.BaryCoordNoPerspCentroidAMD;
            public new static BaryCoordNoPerspCentroidAMDImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BaryCoordNoPerspCentroidAMDImpl object.</summary>
            /// <returns>A string that represents the BaryCoordNoPerspCentroidAMDImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.BaryCoordNoPerspCentroidAMD()";
            }
        }
        #endregion //BaryCoordNoPerspCentroidAMD

        #region BaryCoordNoPerspSampleAMD
        public static BaryCoordNoPerspSampleAMDImpl BaryCoordNoPerspSampleAMD()
        {
            return BaryCoordNoPerspSampleAMDImpl.Instance;
            
        }

        internal class BaryCoordNoPerspSampleAMDImpl: BuiltIn
        {
            public static readonly BaryCoordNoPerspSampleAMDImpl Instance = new BaryCoordNoPerspSampleAMDImpl();
        
            private  BaryCoordNoPerspSampleAMDImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.BaryCoordNoPerspSampleAMD;
            public new static BaryCoordNoPerspSampleAMDImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BaryCoordNoPerspSampleAMDImpl object.</summary>
            /// <returns>A string that represents the BaryCoordNoPerspSampleAMDImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.BaryCoordNoPerspSampleAMD()";
            }
        }
        #endregion //BaryCoordNoPerspSampleAMD

        #region BaryCoordSmoothAMD
        public static BaryCoordSmoothAMDImpl BaryCoordSmoothAMD()
        {
            return BaryCoordSmoothAMDImpl.Instance;
            
        }

        internal class BaryCoordSmoothAMDImpl: BuiltIn
        {
            public static readonly BaryCoordSmoothAMDImpl Instance = new BaryCoordSmoothAMDImpl();
        
            private  BaryCoordSmoothAMDImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.BaryCoordSmoothAMD;
            public new static BaryCoordSmoothAMDImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BaryCoordSmoothAMDImpl object.</summary>
            /// <returns>A string that represents the BaryCoordSmoothAMDImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.BaryCoordSmoothAMD()";
            }
        }
        #endregion //BaryCoordSmoothAMD

        #region BaryCoordSmoothCentroidAMD
        public static BaryCoordSmoothCentroidAMDImpl BaryCoordSmoothCentroidAMD()
        {
            return BaryCoordSmoothCentroidAMDImpl.Instance;
            
        }

        internal class BaryCoordSmoothCentroidAMDImpl: BuiltIn
        {
            public static readonly BaryCoordSmoothCentroidAMDImpl Instance = new BaryCoordSmoothCentroidAMDImpl();
        
            private  BaryCoordSmoothCentroidAMDImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.BaryCoordSmoothCentroidAMD;
            public new static BaryCoordSmoothCentroidAMDImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BaryCoordSmoothCentroidAMDImpl object.</summary>
            /// <returns>A string that represents the BaryCoordSmoothCentroidAMDImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.BaryCoordSmoothCentroidAMD()";
            }
        }
        #endregion //BaryCoordSmoothCentroidAMD

        #region BaryCoordSmoothSampleAMD
        public static BaryCoordSmoothSampleAMDImpl BaryCoordSmoothSampleAMD()
        {
            return BaryCoordSmoothSampleAMDImpl.Instance;
            
        }

        internal class BaryCoordSmoothSampleAMDImpl: BuiltIn
        {
            public static readonly BaryCoordSmoothSampleAMDImpl Instance = new BaryCoordSmoothSampleAMDImpl();
        
            private  BaryCoordSmoothSampleAMDImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.BaryCoordSmoothSampleAMD;
            public new static BaryCoordSmoothSampleAMDImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BaryCoordSmoothSampleAMDImpl object.</summary>
            /// <returns>A string that represents the BaryCoordSmoothSampleAMDImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.BaryCoordSmoothSampleAMD()";
            }
        }
        #endregion //BaryCoordSmoothSampleAMD

        #region BaryCoordPullModelAMD
        public static BaryCoordPullModelAMDImpl BaryCoordPullModelAMD()
        {
            return BaryCoordPullModelAMDImpl.Instance;
            
        }

        internal class BaryCoordPullModelAMDImpl: BuiltIn
        {
            public static readonly BaryCoordPullModelAMDImpl Instance = new BaryCoordPullModelAMDImpl();
        
            private  BaryCoordPullModelAMDImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.BaryCoordPullModelAMD;
            public new static BaryCoordPullModelAMDImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BaryCoordPullModelAMDImpl object.</summary>
            /// <returns>A string that represents the BaryCoordPullModelAMDImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.BaryCoordPullModelAMD()";
            }
        }
        #endregion //BaryCoordPullModelAMD

        #region FragStencilRefEXT
        public static FragStencilRefEXTImpl FragStencilRefEXT()
        {
            return FragStencilRefEXTImpl.Instance;
            
        }

        internal class FragStencilRefEXTImpl: BuiltIn
        {
            public static readonly FragStencilRefEXTImpl Instance = new FragStencilRefEXTImpl();
        
            private  FragStencilRefEXTImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.FragStencilRefEXT;
            public new static FragStencilRefEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragStencilRefEXTImpl object.</summary>
            /// <returns>A string that represents the FragStencilRefEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.FragStencilRefEXT()";
            }
        }
        #endregion //FragStencilRefEXT

        #region ViewportMaskNV
        public static ViewportMaskNVImpl ViewportMaskNV()
        {
            return ViewportMaskNVImpl.Instance;
            
        }

        internal class ViewportMaskNVImpl: BuiltIn
        {
            public static readonly ViewportMaskNVImpl Instance = new ViewportMaskNVImpl();
        
            private  ViewportMaskNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.ViewportMaskNV;
            public new static ViewportMaskNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ViewportMaskNVImpl object.</summary>
            /// <returns>A string that represents the ViewportMaskNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.ViewportMaskNV()";
            }
        }
        #endregion //ViewportMaskNV

        #region SecondaryPositionNV
        public static SecondaryPositionNVImpl SecondaryPositionNV()
        {
            return SecondaryPositionNVImpl.Instance;
            
        }

        internal class SecondaryPositionNVImpl: BuiltIn
        {
            public static readonly SecondaryPositionNVImpl Instance = new SecondaryPositionNVImpl();
        
            private  SecondaryPositionNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SecondaryPositionNV;
            public new static SecondaryPositionNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SecondaryPositionNVImpl object.</summary>
            /// <returns>A string that represents the SecondaryPositionNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SecondaryPositionNV()";
            }
        }
        #endregion //SecondaryPositionNV

        #region SecondaryViewportMaskNV
        public static SecondaryViewportMaskNVImpl SecondaryViewportMaskNV()
        {
            return SecondaryViewportMaskNVImpl.Instance;
            
        }

        internal class SecondaryViewportMaskNVImpl: BuiltIn
        {
            public static readonly SecondaryViewportMaskNVImpl Instance = new SecondaryViewportMaskNVImpl();
        
            private  SecondaryViewportMaskNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SecondaryViewportMaskNV;
            public new static SecondaryViewportMaskNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SecondaryViewportMaskNVImpl object.</summary>
            /// <returns>A string that represents the SecondaryViewportMaskNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SecondaryViewportMaskNV()";
            }
        }
        #endregion //SecondaryViewportMaskNV

        #region PositionPerViewNV
        public static PositionPerViewNVImpl PositionPerViewNV()
        {
            return PositionPerViewNVImpl.Instance;
            
        }

        internal class PositionPerViewNVImpl: BuiltIn
        {
            public static readonly PositionPerViewNVImpl Instance = new PositionPerViewNVImpl();
        
            private  PositionPerViewNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.PositionPerViewNV;
            public new static PositionPerViewNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PositionPerViewNVImpl object.</summary>
            /// <returns>A string that represents the PositionPerViewNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.PositionPerViewNV()";
            }
        }
        #endregion //PositionPerViewNV

        #region ViewportMaskPerViewNV
        public static ViewportMaskPerViewNVImpl ViewportMaskPerViewNV()
        {
            return ViewportMaskPerViewNVImpl.Instance;
            
        }

        internal class ViewportMaskPerViewNVImpl: BuiltIn
        {
            public static readonly ViewportMaskPerViewNVImpl Instance = new ViewportMaskPerViewNVImpl();
        
            private  ViewportMaskPerViewNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.ViewportMaskPerViewNV;
            public new static ViewportMaskPerViewNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ViewportMaskPerViewNVImpl object.</summary>
            /// <returns>A string that represents the ViewportMaskPerViewNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.ViewportMaskPerViewNV()";
            }
        }
        #endregion //ViewportMaskPerViewNV

        #region FullyCoveredEXT
        public static FullyCoveredEXTImpl FullyCoveredEXT()
        {
            return FullyCoveredEXTImpl.Instance;
            
        }

        internal class FullyCoveredEXTImpl: BuiltIn
        {
            public static readonly FullyCoveredEXTImpl Instance = new FullyCoveredEXTImpl();
        
            private  FullyCoveredEXTImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.FullyCoveredEXT;
            public new static FullyCoveredEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FullyCoveredEXTImpl object.</summary>
            /// <returns>A string that represents the FullyCoveredEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.FullyCoveredEXT()";
            }
        }
        #endregion //FullyCoveredEXT

        #region TaskCountNV
        public static TaskCountNVImpl TaskCountNV()
        {
            return TaskCountNVImpl.Instance;
            
        }

        internal class TaskCountNVImpl: BuiltIn
        {
            public static readonly TaskCountNVImpl Instance = new TaskCountNVImpl();
        
            private  TaskCountNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.TaskCountNV;
            public new static TaskCountNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the TaskCountNVImpl object.</summary>
            /// <returns>A string that represents the TaskCountNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.TaskCountNV()";
            }
        }
        #endregion //TaskCountNV

        #region PrimitiveCountNV
        public static PrimitiveCountNVImpl PrimitiveCountNV()
        {
            return PrimitiveCountNVImpl.Instance;
            
        }

        internal class PrimitiveCountNVImpl: BuiltIn
        {
            public static readonly PrimitiveCountNVImpl Instance = new PrimitiveCountNVImpl();
        
            private  PrimitiveCountNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.PrimitiveCountNV;
            public new static PrimitiveCountNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PrimitiveCountNVImpl object.</summary>
            /// <returns>A string that represents the PrimitiveCountNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.PrimitiveCountNV()";
            }
        }
        #endregion //PrimitiveCountNV

        #region PrimitiveIndicesNV
        public static PrimitiveIndicesNVImpl PrimitiveIndicesNV()
        {
            return PrimitiveIndicesNVImpl.Instance;
            
        }

        internal class PrimitiveIndicesNVImpl: BuiltIn
        {
            public static readonly PrimitiveIndicesNVImpl Instance = new PrimitiveIndicesNVImpl();
        
            private  PrimitiveIndicesNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.PrimitiveIndicesNV;
            public new static PrimitiveIndicesNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PrimitiveIndicesNVImpl object.</summary>
            /// <returns>A string that represents the PrimitiveIndicesNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.PrimitiveIndicesNV()";
            }
        }
        #endregion //PrimitiveIndicesNV

        #region ClipDistancePerViewNV
        public static ClipDistancePerViewNVImpl ClipDistancePerViewNV()
        {
            return ClipDistancePerViewNVImpl.Instance;
            
        }

        internal class ClipDistancePerViewNVImpl: BuiltIn
        {
            public static readonly ClipDistancePerViewNVImpl Instance = new ClipDistancePerViewNVImpl();
        
            private  ClipDistancePerViewNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.ClipDistancePerViewNV;
            public new static ClipDistancePerViewNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ClipDistancePerViewNVImpl object.</summary>
            /// <returns>A string that represents the ClipDistancePerViewNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.ClipDistancePerViewNV()";
            }
        }
        #endregion //ClipDistancePerViewNV

        #region CullDistancePerViewNV
        public static CullDistancePerViewNVImpl CullDistancePerViewNV()
        {
            return CullDistancePerViewNVImpl.Instance;
            
        }

        internal class CullDistancePerViewNVImpl: BuiltIn
        {
            public static readonly CullDistancePerViewNVImpl Instance = new CullDistancePerViewNVImpl();
        
            private  CullDistancePerViewNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.CullDistancePerViewNV;
            public new static CullDistancePerViewNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CullDistancePerViewNVImpl object.</summary>
            /// <returns>A string that represents the CullDistancePerViewNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.CullDistancePerViewNV()";
            }
        }
        #endregion //CullDistancePerViewNV

        #region LayerPerViewNV
        public static LayerPerViewNVImpl LayerPerViewNV()
        {
            return LayerPerViewNVImpl.Instance;
            
        }

        internal class LayerPerViewNVImpl: BuiltIn
        {
            public static readonly LayerPerViewNVImpl Instance = new LayerPerViewNVImpl();
        
            private  LayerPerViewNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.LayerPerViewNV;
            public new static LayerPerViewNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LayerPerViewNVImpl object.</summary>
            /// <returns>A string that represents the LayerPerViewNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.LayerPerViewNV()";
            }
        }
        #endregion //LayerPerViewNV

        #region MeshViewCountNV
        public static MeshViewCountNVImpl MeshViewCountNV()
        {
            return MeshViewCountNVImpl.Instance;
            
        }

        internal class MeshViewCountNVImpl: BuiltIn
        {
            public static readonly MeshViewCountNVImpl Instance = new MeshViewCountNVImpl();
        
            private  MeshViewCountNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.MeshViewCountNV;
            public new static MeshViewCountNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the MeshViewCountNVImpl object.</summary>
            /// <returns>A string that represents the MeshViewCountNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.MeshViewCountNV()";
            }
        }
        #endregion //MeshViewCountNV

        #region MeshViewIndicesNV
        public static MeshViewIndicesNVImpl MeshViewIndicesNV()
        {
            return MeshViewIndicesNVImpl.Instance;
            
        }

        internal class MeshViewIndicesNVImpl: BuiltIn
        {
            public static readonly MeshViewIndicesNVImpl Instance = new MeshViewIndicesNVImpl();
        
            private  MeshViewIndicesNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.MeshViewIndicesNV;
            public new static MeshViewIndicesNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the MeshViewIndicesNVImpl object.</summary>
            /// <returns>A string that represents the MeshViewIndicesNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.MeshViewIndicesNV()";
            }
        }
        #endregion //MeshViewIndicesNV

        #region BaryCoordNV
        public static BaryCoordNVImpl BaryCoordNV()
        {
            return BaryCoordNVImpl.Instance;
            
        }

        internal class BaryCoordNVImpl: BuiltIn
        {
            public static readonly BaryCoordNVImpl Instance = new BaryCoordNVImpl();
        
            private  BaryCoordNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.BaryCoordNV;
            public new static BaryCoordNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BaryCoordNVImpl object.</summary>
            /// <returns>A string that represents the BaryCoordNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.BaryCoordNV()";
            }
        }
        #endregion //BaryCoordNV

        #region BaryCoordNoPerspNV
        public static BaryCoordNoPerspNVImpl BaryCoordNoPerspNV()
        {
            return BaryCoordNoPerspNVImpl.Instance;
            
        }

        internal class BaryCoordNoPerspNVImpl: BuiltIn
        {
            public static readonly BaryCoordNoPerspNVImpl Instance = new BaryCoordNoPerspNVImpl();
        
            private  BaryCoordNoPerspNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.BaryCoordNoPerspNV;
            public new static BaryCoordNoPerspNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the BaryCoordNoPerspNVImpl object.</summary>
            /// <returns>A string that represents the BaryCoordNoPerspNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.BaryCoordNoPerspNV()";
            }
        }
        #endregion //BaryCoordNoPerspNV

        #region FragSizeEXT
        public static FragSizeEXTImpl FragSizeEXT()
        {
            return FragSizeEXTImpl.Instance;
            
        }

        internal class FragSizeEXTImpl: BuiltIn
        {
            public static readonly FragSizeEXTImpl Instance = new FragSizeEXTImpl();
        
            private  FragSizeEXTImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.FragSizeEXT;
            public new static FragSizeEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragSizeEXTImpl object.</summary>
            /// <returns>A string that represents the FragSizeEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.FragSizeEXT()";
            }
        }
        #endregion //FragSizeEXT

        #region FragmentSizeNV
        public static FragmentSizeNVImpl FragmentSizeNV()
        {
            return FragmentSizeNVImpl.Instance;
            
        }

        internal class FragmentSizeNVImpl: BuiltIn
        {
            public static readonly FragmentSizeNVImpl Instance = new FragmentSizeNVImpl();
        
            private  FragmentSizeNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.FragmentSizeNV;
            public new static FragmentSizeNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragmentSizeNVImpl object.</summary>
            /// <returns>A string that represents the FragmentSizeNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.FragmentSizeNV()";
            }
        }
        #endregion //FragmentSizeNV

        #region FragInvocationCountEXT
        public static FragInvocationCountEXTImpl FragInvocationCountEXT()
        {
            return FragInvocationCountEXTImpl.Instance;
            
        }

        internal class FragInvocationCountEXTImpl: BuiltIn
        {
            public static readonly FragInvocationCountEXTImpl Instance = new FragInvocationCountEXTImpl();
        
            private  FragInvocationCountEXTImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.FragInvocationCountEXT;
            public new static FragInvocationCountEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragInvocationCountEXTImpl object.</summary>
            /// <returns>A string that represents the FragInvocationCountEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.FragInvocationCountEXT()";
            }
        }
        #endregion //FragInvocationCountEXT

        #region InvocationsPerPixelNV
        public static InvocationsPerPixelNVImpl InvocationsPerPixelNV()
        {
            return InvocationsPerPixelNVImpl.Instance;
            
        }

        internal class InvocationsPerPixelNVImpl: BuiltIn
        {
            public static readonly InvocationsPerPixelNVImpl Instance = new InvocationsPerPixelNVImpl();
        
            private  InvocationsPerPixelNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.InvocationsPerPixelNV;
            public new static InvocationsPerPixelNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InvocationsPerPixelNVImpl object.</summary>
            /// <returns>A string that represents the InvocationsPerPixelNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.InvocationsPerPixelNV()";
            }
        }
        #endregion //InvocationsPerPixelNV

        #region LaunchIdNV
        public static LaunchIdNVImpl LaunchIdNV()
        {
            return LaunchIdNVImpl.Instance;
            
        }

        internal class LaunchIdNVImpl: BuiltIn
        {
            public static readonly LaunchIdNVImpl Instance = new LaunchIdNVImpl();
        
            private  LaunchIdNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.LaunchIdNV;
            public new static LaunchIdNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LaunchIdNVImpl object.</summary>
            /// <returns>A string that represents the LaunchIdNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.LaunchIdNV()";
            }
        }
        #endregion //LaunchIdNV

        #region LaunchIdKHR
        public static LaunchIdKHRImpl LaunchIdKHR()
        {
            return LaunchIdKHRImpl.Instance;
            
        }

        internal class LaunchIdKHRImpl: BuiltIn
        {
            public static readonly LaunchIdKHRImpl Instance = new LaunchIdKHRImpl();
        
            private  LaunchIdKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.LaunchIdKHR;
            public new static LaunchIdKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LaunchIdKHRImpl object.</summary>
            /// <returns>A string that represents the LaunchIdKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.LaunchIdKHR()";
            }
        }
        #endregion //LaunchIdKHR

        #region LaunchSizeNV
        public static LaunchSizeNVImpl LaunchSizeNV()
        {
            return LaunchSizeNVImpl.Instance;
            
        }

        internal class LaunchSizeNVImpl: BuiltIn
        {
            public static readonly LaunchSizeNVImpl Instance = new LaunchSizeNVImpl();
        
            private  LaunchSizeNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.LaunchSizeNV;
            public new static LaunchSizeNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LaunchSizeNVImpl object.</summary>
            /// <returns>A string that represents the LaunchSizeNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.LaunchSizeNV()";
            }
        }
        #endregion //LaunchSizeNV

        #region LaunchSizeKHR
        public static LaunchSizeKHRImpl LaunchSizeKHR()
        {
            return LaunchSizeKHRImpl.Instance;
            
        }

        internal class LaunchSizeKHRImpl: BuiltIn
        {
            public static readonly LaunchSizeKHRImpl Instance = new LaunchSizeKHRImpl();
        
            private  LaunchSizeKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.LaunchSizeKHR;
            public new static LaunchSizeKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LaunchSizeKHRImpl object.</summary>
            /// <returns>A string that represents the LaunchSizeKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.LaunchSizeKHR()";
            }
        }
        #endregion //LaunchSizeKHR

        #region WorldRayOriginNV
        public static WorldRayOriginNVImpl WorldRayOriginNV()
        {
            return WorldRayOriginNVImpl.Instance;
            
        }

        internal class WorldRayOriginNVImpl: BuiltIn
        {
            public static readonly WorldRayOriginNVImpl Instance = new WorldRayOriginNVImpl();
        
            private  WorldRayOriginNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.WorldRayOriginNV;
            public new static WorldRayOriginNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WorldRayOriginNVImpl object.</summary>
            /// <returns>A string that represents the WorldRayOriginNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.WorldRayOriginNV()";
            }
        }
        #endregion //WorldRayOriginNV

        #region WorldRayOriginKHR
        public static WorldRayOriginKHRImpl WorldRayOriginKHR()
        {
            return WorldRayOriginKHRImpl.Instance;
            
        }

        internal class WorldRayOriginKHRImpl: BuiltIn
        {
            public static readonly WorldRayOriginKHRImpl Instance = new WorldRayOriginKHRImpl();
        
            private  WorldRayOriginKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.WorldRayOriginKHR;
            public new static WorldRayOriginKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WorldRayOriginKHRImpl object.</summary>
            /// <returns>A string that represents the WorldRayOriginKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.WorldRayOriginKHR()";
            }
        }
        #endregion //WorldRayOriginKHR

        #region WorldRayDirectionNV
        public static WorldRayDirectionNVImpl WorldRayDirectionNV()
        {
            return WorldRayDirectionNVImpl.Instance;
            
        }

        internal class WorldRayDirectionNVImpl: BuiltIn
        {
            public static readonly WorldRayDirectionNVImpl Instance = new WorldRayDirectionNVImpl();
        
            private  WorldRayDirectionNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.WorldRayDirectionNV;
            public new static WorldRayDirectionNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WorldRayDirectionNVImpl object.</summary>
            /// <returns>A string that represents the WorldRayDirectionNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.WorldRayDirectionNV()";
            }
        }
        #endregion //WorldRayDirectionNV

        #region WorldRayDirectionKHR
        public static WorldRayDirectionKHRImpl WorldRayDirectionKHR()
        {
            return WorldRayDirectionKHRImpl.Instance;
            
        }

        internal class WorldRayDirectionKHRImpl: BuiltIn
        {
            public static readonly WorldRayDirectionKHRImpl Instance = new WorldRayDirectionKHRImpl();
        
            private  WorldRayDirectionKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.WorldRayDirectionKHR;
            public new static WorldRayDirectionKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WorldRayDirectionKHRImpl object.</summary>
            /// <returns>A string that represents the WorldRayDirectionKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.WorldRayDirectionKHR()";
            }
        }
        #endregion //WorldRayDirectionKHR

        #region ObjectRayOriginNV
        public static ObjectRayOriginNVImpl ObjectRayOriginNV()
        {
            return ObjectRayOriginNVImpl.Instance;
            
        }

        internal class ObjectRayOriginNVImpl: BuiltIn
        {
            public static readonly ObjectRayOriginNVImpl Instance = new ObjectRayOriginNVImpl();
        
            private  ObjectRayOriginNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.ObjectRayOriginNV;
            public new static ObjectRayOriginNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ObjectRayOriginNVImpl object.</summary>
            /// <returns>A string that represents the ObjectRayOriginNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.ObjectRayOriginNV()";
            }
        }
        #endregion //ObjectRayOriginNV

        #region ObjectRayOriginKHR
        public static ObjectRayOriginKHRImpl ObjectRayOriginKHR()
        {
            return ObjectRayOriginKHRImpl.Instance;
            
        }

        internal class ObjectRayOriginKHRImpl: BuiltIn
        {
            public static readonly ObjectRayOriginKHRImpl Instance = new ObjectRayOriginKHRImpl();
        
            private  ObjectRayOriginKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.ObjectRayOriginKHR;
            public new static ObjectRayOriginKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ObjectRayOriginKHRImpl object.</summary>
            /// <returns>A string that represents the ObjectRayOriginKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.ObjectRayOriginKHR()";
            }
        }
        #endregion //ObjectRayOriginKHR

        #region ObjectRayDirectionNV
        public static ObjectRayDirectionNVImpl ObjectRayDirectionNV()
        {
            return ObjectRayDirectionNVImpl.Instance;
            
        }

        internal class ObjectRayDirectionNVImpl: BuiltIn
        {
            public static readonly ObjectRayDirectionNVImpl Instance = new ObjectRayDirectionNVImpl();
        
            private  ObjectRayDirectionNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.ObjectRayDirectionNV;
            public new static ObjectRayDirectionNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ObjectRayDirectionNVImpl object.</summary>
            /// <returns>A string that represents the ObjectRayDirectionNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.ObjectRayDirectionNV()";
            }
        }
        #endregion //ObjectRayDirectionNV

        #region ObjectRayDirectionKHR
        public static ObjectRayDirectionKHRImpl ObjectRayDirectionKHR()
        {
            return ObjectRayDirectionKHRImpl.Instance;
            
        }

        internal class ObjectRayDirectionKHRImpl: BuiltIn
        {
            public static readonly ObjectRayDirectionKHRImpl Instance = new ObjectRayDirectionKHRImpl();
        
            private  ObjectRayDirectionKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.ObjectRayDirectionKHR;
            public new static ObjectRayDirectionKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ObjectRayDirectionKHRImpl object.</summary>
            /// <returns>A string that represents the ObjectRayDirectionKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.ObjectRayDirectionKHR()";
            }
        }
        #endregion //ObjectRayDirectionKHR

        #region RayTminNV
        public static RayTminNVImpl RayTminNV()
        {
            return RayTminNVImpl.Instance;
            
        }

        internal class RayTminNVImpl: BuiltIn
        {
            public static readonly RayTminNVImpl Instance = new RayTminNVImpl();
        
            private  RayTminNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.RayTminNV;
            public new static RayTminNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayTminNVImpl object.</summary>
            /// <returns>A string that represents the RayTminNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.RayTminNV()";
            }
        }
        #endregion //RayTminNV

        #region RayTminKHR
        public static RayTminKHRImpl RayTminKHR()
        {
            return RayTminKHRImpl.Instance;
            
        }

        internal class RayTminKHRImpl: BuiltIn
        {
            public static readonly RayTminKHRImpl Instance = new RayTminKHRImpl();
        
            private  RayTminKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.RayTminKHR;
            public new static RayTminKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayTminKHRImpl object.</summary>
            /// <returns>A string that represents the RayTminKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.RayTminKHR()";
            }
        }
        #endregion //RayTminKHR

        #region RayTmaxNV
        public static RayTmaxNVImpl RayTmaxNV()
        {
            return RayTmaxNVImpl.Instance;
            
        }

        internal class RayTmaxNVImpl: BuiltIn
        {
            public static readonly RayTmaxNVImpl Instance = new RayTmaxNVImpl();
        
            private  RayTmaxNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.RayTmaxNV;
            public new static RayTmaxNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayTmaxNVImpl object.</summary>
            /// <returns>A string that represents the RayTmaxNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.RayTmaxNV()";
            }
        }
        #endregion //RayTmaxNV

        #region RayTmaxKHR
        public static RayTmaxKHRImpl RayTmaxKHR()
        {
            return RayTmaxKHRImpl.Instance;
            
        }

        internal class RayTmaxKHRImpl: BuiltIn
        {
            public static readonly RayTmaxKHRImpl Instance = new RayTmaxKHRImpl();
        
            private  RayTmaxKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.RayTmaxKHR;
            public new static RayTmaxKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayTmaxKHRImpl object.</summary>
            /// <returns>A string that represents the RayTmaxKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.RayTmaxKHR()";
            }
        }
        #endregion //RayTmaxKHR

        #region InstanceCustomIndexNV
        public static InstanceCustomIndexNVImpl InstanceCustomIndexNV()
        {
            return InstanceCustomIndexNVImpl.Instance;
            
        }

        internal class InstanceCustomIndexNVImpl: BuiltIn
        {
            public static readonly InstanceCustomIndexNVImpl Instance = new InstanceCustomIndexNVImpl();
        
            private  InstanceCustomIndexNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.InstanceCustomIndexNV;
            public new static InstanceCustomIndexNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InstanceCustomIndexNVImpl object.</summary>
            /// <returns>A string that represents the InstanceCustomIndexNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.InstanceCustomIndexNV()";
            }
        }
        #endregion //InstanceCustomIndexNV

        #region InstanceCustomIndexKHR
        public static InstanceCustomIndexKHRImpl InstanceCustomIndexKHR()
        {
            return InstanceCustomIndexKHRImpl.Instance;
            
        }

        internal class InstanceCustomIndexKHRImpl: BuiltIn
        {
            public static readonly InstanceCustomIndexKHRImpl Instance = new InstanceCustomIndexKHRImpl();
        
            private  InstanceCustomIndexKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.InstanceCustomIndexKHR;
            public new static InstanceCustomIndexKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InstanceCustomIndexKHRImpl object.</summary>
            /// <returns>A string that represents the InstanceCustomIndexKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.InstanceCustomIndexKHR()";
            }
        }
        #endregion //InstanceCustomIndexKHR

        #region ObjectToWorldNV
        public static ObjectToWorldNVImpl ObjectToWorldNV()
        {
            return ObjectToWorldNVImpl.Instance;
            
        }

        internal class ObjectToWorldNVImpl: BuiltIn
        {
            public static readonly ObjectToWorldNVImpl Instance = new ObjectToWorldNVImpl();
        
            private  ObjectToWorldNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.ObjectToWorldNV;
            public new static ObjectToWorldNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ObjectToWorldNVImpl object.</summary>
            /// <returns>A string that represents the ObjectToWorldNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.ObjectToWorldNV()";
            }
        }
        #endregion //ObjectToWorldNV

        #region ObjectToWorldKHR
        public static ObjectToWorldKHRImpl ObjectToWorldKHR()
        {
            return ObjectToWorldKHRImpl.Instance;
            
        }

        internal class ObjectToWorldKHRImpl: BuiltIn
        {
            public static readonly ObjectToWorldKHRImpl Instance = new ObjectToWorldKHRImpl();
        
            private  ObjectToWorldKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.ObjectToWorldKHR;
            public new static ObjectToWorldKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ObjectToWorldKHRImpl object.</summary>
            /// <returns>A string that represents the ObjectToWorldKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.ObjectToWorldKHR()";
            }
        }
        #endregion //ObjectToWorldKHR

        #region WorldToObjectNV
        public static WorldToObjectNVImpl WorldToObjectNV()
        {
            return WorldToObjectNVImpl.Instance;
            
        }

        internal class WorldToObjectNVImpl: BuiltIn
        {
            public static readonly WorldToObjectNVImpl Instance = new WorldToObjectNVImpl();
        
            private  WorldToObjectNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.WorldToObjectNV;
            public new static WorldToObjectNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WorldToObjectNVImpl object.</summary>
            /// <returns>A string that represents the WorldToObjectNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.WorldToObjectNV()";
            }
        }
        #endregion //WorldToObjectNV

        #region WorldToObjectKHR
        public static WorldToObjectKHRImpl WorldToObjectKHR()
        {
            return WorldToObjectKHRImpl.Instance;
            
        }

        internal class WorldToObjectKHRImpl: BuiltIn
        {
            public static readonly WorldToObjectKHRImpl Instance = new WorldToObjectKHRImpl();
        
            private  WorldToObjectKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.WorldToObjectKHR;
            public new static WorldToObjectKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WorldToObjectKHRImpl object.</summary>
            /// <returns>A string that represents the WorldToObjectKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.WorldToObjectKHR()";
            }
        }
        #endregion //WorldToObjectKHR

        #region HitTNV
        public static HitTNVImpl HitTNV()
        {
            return HitTNVImpl.Instance;
            
        }

        internal class HitTNVImpl: BuiltIn
        {
            public static readonly HitTNVImpl Instance = new HitTNVImpl();
        
            private  HitTNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.HitTNV;
            public new static HitTNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the HitTNVImpl object.</summary>
            /// <returns>A string that represents the HitTNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.HitTNV()";
            }
        }
        #endregion //HitTNV

        #region HitTKHR
        public static HitTKHRImpl HitTKHR()
        {
            return HitTKHRImpl.Instance;
            
        }

        internal class HitTKHRImpl: BuiltIn
        {
            public static readonly HitTKHRImpl Instance = new HitTKHRImpl();
        
            private  HitTKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.HitTKHR;
            public new static HitTKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the HitTKHRImpl object.</summary>
            /// <returns>A string that represents the HitTKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.HitTKHR()";
            }
        }
        #endregion //HitTKHR

        #region HitKindNV
        public static HitKindNVImpl HitKindNV()
        {
            return HitKindNVImpl.Instance;
            
        }

        internal class HitKindNVImpl: BuiltIn
        {
            public static readonly HitKindNVImpl Instance = new HitKindNVImpl();
        
            private  HitKindNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.HitKindNV;
            public new static HitKindNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the HitKindNVImpl object.</summary>
            /// <returns>A string that represents the HitKindNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.HitKindNV()";
            }
        }
        #endregion //HitKindNV

        #region HitKindKHR
        public static HitKindKHRImpl HitKindKHR()
        {
            return HitKindKHRImpl.Instance;
            
        }

        internal class HitKindKHRImpl: BuiltIn
        {
            public static readonly HitKindKHRImpl Instance = new HitKindKHRImpl();
        
            private  HitKindKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.HitKindKHR;
            public new static HitKindKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the HitKindKHRImpl object.</summary>
            /// <returns>A string that represents the HitKindKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.HitKindKHR()";
            }
        }
        #endregion //HitKindKHR

        #region IncomingRayFlagsNV
        public static IncomingRayFlagsNVImpl IncomingRayFlagsNV()
        {
            return IncomingRayFlagsNVImpl.Instance;
            
        }

        internal class IncomingRayFlagsNVImpl: BuiltIn
        {
            public static readonly IncomingRayFlagsNVImpl Instance = new IncomingRayFlagsNVImpl();
        
            private  IncomingRayFlagsNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.IncomingRayFlagsNV;
            public new static IncomingRayFlagsNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the IncomingRayFlagsNVImpl object.</summary>
            /// <returns>A string that represents the IncomingRayFlagsNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.IncomingRayFlagsNV()";
            }
        }
        #endregion //IncomingRayFlagsNV

        #region IncomingRayFlagsKHR
        public static IncomingRayFlagsKHRImpl IncomingRayFlagsKHR()
        {
            return IncomingRayFlagsKHRImpl.Instance;
            
        }

        internal class IncomingRayFlagsKHRImpl: BuiltIn
        {
            public static readonly IncomingRayFlagsKHRImpl Instance = new IncomingRayFlagsKHRImpl();
        
            private  IncomingRayFlagsKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.IncomingRayFlagsKHR;
            public new static IncomingRayFlagsKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the IncomingRayFlagsKHRImpl object.</summary>
            /// <returns>A string that represents the IncomingRayFlagsKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.IncomingRayFlagsKHR()";
            }
        }
        #endregion //IncomingRayFlagsKHR

        #region RayGeometryIndexKHR
        public static RayGeometryIndexKHRImpl RayGeometryIndexKHR()
        {
            return RayGeometryIndexKHRImpl.Instance;
            
        }

        internal class RayGeometryIndexKHRImpl: BuiltIn
        {
            public static readonly RayGeometryIndexKHRImpl Instance = new RayGeometryIndexKHRImpl();
        
            private  RayGeometryIndexKHRImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.RayGeometryIndexKHR;
            public new static RayGeometryIndexKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayGeometryIndexKHRImpl object.</summary>
            /// <returns>A string that represents the RayGeometryIndexKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.RayGeometryIndexKHR()";
            }
        }
        #endregion //RayGeometryIndexKHR

        #region WarpsPerSMNV
        public static WarpsPerSMNVImpl WarpsPerSMNV()
        {
            return WarpsPerSMNVImpl.Instance;
            
        }

        internal class WarpsPerSMNVImpl: BuiltIn
        {
            public static readonly WarpsPerSMNVImpl Instance = new WarpsPerSMNVImpl();
        
            private  WarpsPerSMNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.WarpsPerSMNV;
            public new static WarpsPerSMNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WarpsPerSMNVImpl object.</summary>
            /// <returns>A string that represents the WarpsPerSMNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.WarpsPerSMNV()";
            }
        }
        #endregion //WarpsPerSMNV

        #region SMCountNV
        public static SMCountNVImpl SMCountNV()
        {
            return SMCountNVImpl.Instance;
            
        }

        internal class SMCountNVImpl: BuiltIn
        {
            public static readonly SMCountNVImpl Instance = new SMCountNVImpl();
        
            private  SMCountNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SMCountNV;
            public new static SMCountNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SMCountNVImpl object.</summary>
            /// <returns>A string that represents the SMCountNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SMCountNV()";
            }
        }
        #endregion //SMCountNV

        #region WarpIDNV
        public static WarpIDNVImpl WarpIDNV()
        {
            return WarpIDNVImpl.Instance;
            
        }

        internal class WarpIDNVImpl: BuiltIn
        {
            public static readonly WarpIDNVImpl Instance = new WarpIDNVImpl();
        
            private  WarpIDNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.WarpIDNV;
            public new static WarpIDNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WarpIDNVImpl object.</summary>
            /// <returns>A string that represents the WarpIDNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.WarpIDNV()";
            }
        }
        #endregion //WarpIDNV

        #region SMIDNV
        public static SMIDNVImpl SMIDNV()
        {
            return SMIDNVImpl.Instance;
            
        }

        internal class SMIDNVImpl: BuiltIn
        {
            public static readonly SMIDNVImpl Instance = new SMIDNVImpl();
        
            private  SMIDNVImpl()
            {
            }
            public override Enumerant Value => BuiltIn.Enumerant.SMIDNV;
            public new static SMIDNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SMIDNVImpl object.</summary>
            /// <returns>A string that represents the SMIDNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" BuiltIn.SMIDNV()";
            }
        }
        #endregion //SMIDNV

        public abstract Enumerant Value { get; }

        public static BuiltIn Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Position :
                    return PositionImpl.Parse(reader, wordCount - 1);
                case Enumerant.PointSize :
                    return PointSizeImpl.Parse(reader, wordCount - 1);
                case Enumerant.ClipDistance :
                    return ClipDistanceImpl.Parse(reader, wordCount - 1);
                case Enumerant.CullDistance :
                    return CullDistanceImpl.Parse(reader, wordCount - 1);
                case Enumerant.VertexId :
                    return VertexIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.InstanceId :
                    return InstanceIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.PrimitiveId :
                    return PrimitiveIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.InvocationId :
                    return InvocationIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.Layer :
                    return LayerImpl.Parse(reader, wordCount - 1);
                case Enumerant.ViewportIndex :
                    return ViewportIndexImpl.Parse(reader, wordCount - 1);
                case Enumerant.TessLevelOuter :
                    return TessLevelOuterImpl.Parse(reader, wordCount - 1);
                case Enumerant.TessLevelInner :
                    return TessLevelInnerImpl.Parse(reader, wordCount - 1);
                case Enumerant.TessCoord :
                    return TessCoordImpl.Parse(reader, wordCount - 1);
                case Enumerant.PatchVertices :
                    return PatchVerticesImpl.Parse(reader, wordCount - 1);
                case Enumerant.FragCoord :
                    return FragCoordImpl.Parse(reader, wordCount - 1);
                case Enumerant.PointCoord :
                    return PointCoordImpl.Parse(reader, wordCount - 1);
                case Enumerant.FrontFacing :
                    return FrontFacingImpl.Parse(reader, wordCount - 1);
                case Enumerant.SampleId :
                    return SampleIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.SamplePosition :
                    return SamplePositionImpl.Parse(reader, wordCount - 1);
                case Enumerant.SampleMask :
                    return SampleMaskImpl.Parse(reader, wordCount - 1);
                case Enumerant.FragDepth :
                    return FragDepthImpl.Parse(reader, wordCount - 1);
                case Enumerant.HelperInvocation :
                    return HelperInvocationImpl.Parse(reader, wordCount - 1);
                case Enumerant.NumWorkgroups :
                    return NumWorkgroupsImpl.Parse(reader, wordCount - 1);
                case Enumerant.WorkgroupSize :
                    return WorkgroupSizeImpl.Parse(reader, wordCount - 1);
                case Enumerant.WorkgroupId :
                    return WorkgroupIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.LocalInvocationId :
                    return LocalInvocationIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.GlobalInvocationId :
                    return GlobalInvocationIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.LocalInvocationIndex :
                    return LocalInvocationIndexImpl.Parse(reader, wordCount - 1);
                case Enumerant.WorkDim :
                    return WorkDimImpl.Parse(reader, wordCount - 1);
                case Enumerant.GlobalSize :
                    return GlobalSizeImpl.Parse(reader, wordCount - 1);
                case Enumerant.EnqueuedWorkgroupSize :
                    return EnqueuedWorkgroupSizeImpl.Parse(reader, wordCount - 1);
                case Enumerant.GlobalOffset :
                    return GlobalOffsetImpl.Parse(reader, wordCount - 1);
                case Enumerant.GlobalLinearId :
                    return GlobalLinearIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupSize :
                    return SubgroupSizeImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupMaxSize :
                    return SubgroupMaxSizeImpl.Parse(reader, wordCount - 1);
                case Enumerant.NumSubgroups :
                    return NumSubgroupsImpl.Parse(reader, wordCount - 1);
                case Enumerant.NumEnqueuedSubgroups :
                    return NumEnqueuedSubgroupsImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupId :
                    return SubgroupIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupLocalInvocationId :
                    return SubgroupLocalInvocationIdImpl.Parse(reader, wordCount - 1);
                case Enumerant.VertexIndex :
                    return VertexIndexImpl.Parse(reader, wordCount - 1);
                case Enumerant.InstanceIndex :
                    return InstanceIndexImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupEqMask :
                    return SubgroupEqMaskImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupGeMask :
                    return SubgroupGeMaskImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupGtMask :
                    return SubgroupGtMaskImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupLeMask :
                    return SubgroupLeMaskImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupLtMask :
                    return SubgroupLtMaskImpl.Parse(reader, wordCount - 1);
                //SubgroupEqMaskKHR has the same id as another value in enum.
                //case Enumerant.SubgroupEqMaskKHR :
                //    return SubgroupEqMaskKHR.Parse(reader, wordCount - 1);
                //SubgroupGeMaskKHR has the same id as another value in enum.
                //case Enumerant.SubgroupGeMaskKHR :
                //    return SubgroupGeMaskKHR.Parse(reader, wordCount - 1);
                //SubgroupGtMaskKHR has the same id as another value in enum.
                //case Enumerant.SubgroupGtMaskKHR :
                //    return SubgroupGtMaskKHR.Parse(reader, wordCount - 1);
                //SubgroupLeMaskKHR has the same id as another value in enum.
                //case Enumerant.SubgroupLeMaskKHR :
                //    return SubgroupLeMaskKHR.Parse(reader, wordCount - 1);
                //SubgroupLtMaskKHR has the same id as another value in enum.
                //case Enumerant.SubgroupLtMaskKHR :
                //    return SubgroupLtMaskKHR.Parse(reader, wordCount - 1);
                case Enumerant.BaseVertex :
                    return BaseVertexImpl.Parse(reader, wordCount - 1);
                case Enumerant.BaseInstance :
                    return BaseInstanceImpl.Parse(reader, wordCount - 1);
                case Enumerant.DrawIndex :
                    return DrawIndexImpl.Parse(reader, wordCount - 1);
                case Enumerant.DeviceIndex :
                    return DeviceIndexImpl.Parse(reader, wordCount - 1);
                case Enumerant.ViewIndex :
                    return ViewIndexImpl.Parse(reader, wordCount - 1);
                case Enumerant.BaryCoordNoPerspAMD :
                    return BaryCoordNoPerspAMDImpl.Parse(reader, wordCount - 1);
                case Enumerant.BaryCoordNoPerspCentroidAMD :
                    return BaryCoordNoPerspCentroidAMDImpl.Parse(reader, wordCount - 1);
                case Enumerant.BaryCoordNoPerspSampleAMD :
                    return BaryCoordNoPerspSampleAMDImpl.Parse(reader, wordCount - 1);
                case Enumerant.BaryCoordSmoothAMD :
                    return BaryCoordSmoothAMDImpl.Parse(reader, wordCount - 1);
                case Enumerant.BaryCoordSmoothCentroidAMD :
                    return BaryCoordSmoothCentroidAMDImpl.Parse(reader, wordCount - 1);
                case Enumerant.BaryCoordSmoothSampleAMD :
                    return BaryCoordSmoothSampleAMDImpl.Parse(reader, wordCount - 1);
                case Enumerant.BaryCoordPullModelAMD :
                    return BaryCoordPullModelAMDImpl.Parse(reader, wordCount - 1);
                case Enumerant.FragStencilRefEXT :
                    return FragStencilRefEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.ViewportMaskNV :
                    return ViewportMaskNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.SecondaryPositionNV :
                    return SecondaryPositionNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.SecondaryViewportMaskNV :
                    return SecondaryViewportMaskNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.PositionPerViewNV :
                    return PositionPerViewNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.ViewportMaskPerViewNV :
                    return ViewportMaskPerViewNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.FullyCoveredEXT :
                    return FullyCoveredEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.TaskCountNV :
                    return TaskCountNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.PrimitiveCountNV :
                    return PrimitiveCountNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.PrimitiveIndicesNV :
                    return PrimitiveIndicesNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.ClipDistancePerViewNV :
                    return ClipDistancePerViewNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.CullDistancePerViewNV :
                    return CullDistancePerViewNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.LayerPerViewNV :
                    return LayerPerViewNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.MeshViewCountNV :
                    return MeshViewCountNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.MeshViewIndicesNV :
                    return MeshViewIndicesNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.BaryCoordNV :
                    return BaryCoordNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.BaryCoordNoPerspNV :
                    return BaryCoordNoPerspNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.FragSizeEXT :
                    return FragSizeEXTImpl.Parse(reader, wordCount - 1);
                //FragmentSizeNV has the same id as another value in enum.
                //case Enumerant.FragmentSizeNV :
                //    return FragmentSizeNV.Parse(reader, wordCount - 1);
                case Enumerant.FragInvocationCountEXT :
                    return FragInvocationCountEXTImpl.Parse(reader, wordCount - 1);
                //InvocationsPerPixelNV has the same id as another value in enum.
                //case Enumerant.InvocationsPerPixelNV :
                //    return InvocationsPerPixelNV.Parse(reader, wordCount - 1);
                case Enumerant.LaunchIdNV :
                    return LaunchIdNVImpl.Parse(reader, wordCount - 1);
                //LaunchIdKHR has the same id as another value in enum.
                //case Enumerant.LaunchIdKHR :
                //    return LaunchIdKHR.Parse(reader, wordCount - 1);
                case Enumerant.LaunchSizeNV :
                    return LaunchSizeNVImpl.Parse(reader, wordCount - 1);
                //LaunchSizeKHR has the same id as another value in enum.
                //case Enumerant.LaunchSizeKHR :
                //    return LaunchSizeKHR.Parse(reader, wordCount - 1);
                case Enumerant.WorldRayOriginNV :
                    return WorldRayOriginNVImpl.Parse(reader, wordCount - 1);
                //WorldRayOriginKHR has the same id as another value in enum.
                //case Enumerant.WorldRayOriginKHR :
                //    return WorldRayOriginKHR.Parse(reader, wordCount - 1);
                case Enumerant.WorldRayDirectionNV :
                    return WorldRayDirectionNVImpl.Parse(reader, wordCount - 1);
                //WorldRayDirectionKHR has the same id as another value in enum.
                //case Enumerant.WorldRayDirectionKHR :
                //    return WorldRayDirectionKHR.Parse(reader, wordCount - 1);
                case Enumerant.ObjectRayOriginNV :
                    return ObjectRayOriginNVImpl.Parse(reader, wordCount - 1);
                //ObjectRayOriginKHR has the same id as another value in enum.
                //case Enumerant.ObjectRayOriginKHR :
                //    return ObjectRayOriginKHR.Parse(reader, wordCount - 1);
                case Enumerant.ObjectRayDirectionNV :
                    return ObjectRayDirectionNVImpl.Parse(reader, wordCount - 1);
                //ObjectRayDirectionKHR has the same id as another value in enum.
                //case Enumerant.ObjectRayDirectionKHR :
                //    return ObjectRayDirectionKHR.Parse(reader, wordCount - 1);
                case Enumerant.RayTminNV :
                    return RayTminNVImpl.Parse(reader, wordCount - 1);
                //RayTminKHR has the same id as another value in enum.
                //case Enumerant.RayTminKHR :
                //    return RayTminKHR.Parse(reader, wordCount - 1);
                case Enumerant.RayTmaxNV :
                    return RayTmaxNVImpl.Parse(reader, wordCount - 1);
                //RayTmaxKHR has the same id as another value in enum.
                //case Enumerant.RayTmaxKHR :
                //    return RayTmaxKHR.Parse(reader, wordCount - 1);
                case Enumerant.InstanceCustomIndexNV :
                    return InstanceCustomIndexNVImpl.Parse(reader, wordCount - 1);
                //InstanceCustomIndexKHR has the same id as another value in enum.
                //case Enumerant.InstanceCustomIndexKHR :
                //    return InstanceCustomIndexKHR.Parse(reader, wordCount - 1);
                case Enumerant.ObjectToWorldNV :
                    return ObjectToWorldNVImpl.Parse(reader, wordCount - 1);
                //ObjectToWorldKHR has the same id as another value in enum.
                //case Enumerant.ObjectToWorldKHR :
                //    return ObjectToWorldKHR.Parse(reader, wordCount - 1);
                case Enumerant.WorldToObjectNV :
                    return WorldToObjectNVImpl.Parse(reader, wordCount - 1);
                //WorldToObjectKHR has the same id as another value in enum.
                //case Enumerant.WorldToObjectKHR :
                //    return WorldToObjectKHR.Parse(reader, wordCount - 1);
                case Enumerant.HitTNV :
                    return HitTNVImpl.Parse(reader, wordCount - 1);
                //HitTKHR has the same id as another value in enum.
                //case Enumerant.HitTKHR :
                //    return HitTKHR.Parse(reader, wordCount - 1);
                case Enumerant.HitKindNV :
                    return HitKindNVImpl.Parse(reader, wordCount - 1);
                //HitKindKHR has the same id as another value in enum.
                //case Enumerant.HitKindKHR :
                //    return HitKindKHR.Parse(reader, wordCount - 1);
                case Enumerant.IncomingRayFlagsNV :
                    return IncomingRayFlagsNVImpl.Parse(reader, wordCount - 1);
                //IncomingRayFlagsKHR has the same id as another value in enum.
                //case Enumerant.IncomingRayFlagsKHR :
                //    return IncomingRayFlagsKHR.Parse(reader, wordCount - 1);
                case Enumerant.RayGeometryIndexKHR :
                    return RayGeometryIndexKHRImpl.Parse(reader, wordCount - 1);
                case Enumerant.WarpsPerSMNV :
                    return WarpsPerSMNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.SMCountNV :
                    return SMCountNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.WarpIDNV :
                    return WarpIDNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.SMIDNV :
                    return SMIDNVImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown BuiltIn "+id);
            }
        }
        
        public static BuiltIn ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<BuiltIn> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<BuiltIn>();
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