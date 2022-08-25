using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class ExecutionModel : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Shader)]
            Vertex = 0,
            [Capability(Capability.Enumerant.Tessellation)]
            TessellationControl = 1,
            [Capability(Capability.Enumerant.Tessellation)]
            TessellationEvaluation = 2,
            [Capability(Capability.Enumerant.Geometry)]
            Geometry = 3,
            [Capability(Capability.Enumerant.Shader)]
            Fragment = 4,
            [Capability(Capability.Enumerant.Shader)]
            GLCompute = 5,
            [Capability(Capability.Enumerant.Kernel)]
            Kernel = 6,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            TaskNV = 5267,
            [Capability(Capability.Enumerant.MeshShadingNV)]
            MeshNV = 5268,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            RayGenerationNV = 5313,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            RayGenerationKHR = 5313,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            IntersectionNV = 5314,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            IntersectionKHR = 5314,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            AnyHitNV = 5315,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            AnyHitKHR = 5315,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            ClosestHitNV = 5316,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            ClosestHitKHR = 5316,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            MissNV = 5317,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            MissKHR = 5317,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            CallableNV = 5318,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            CallableKHR = 5318,
        }

        #region Vertex
        public static VertexImpl Vertex()
        {
            return VertexImpl.Instance;
            
        }

        internal class VertexImpl: ExecutionModel
        {
            public static readonly VertexImpl Instance = new VertexImpl();
        
            private  VertexImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.Vertex;
            public new static VertexImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VertexImpl object.</summary>
            /// <returns>A string that represents the VertexImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.Vertex()";
            }
        }
        #endregion //Vertex

        #region TessellationControl
        public static TessellationControlImpl TessellationControl()
        {
            return TessellationControlImpl.Instance;
            
        }

        internal class TessellationControlImpl: ExecutionModel
        {
            public static readonly TessellationControlImpl Instance = new TessellationControlImpl();
        
            private  TessellationControlImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.TessellationControl;
            public new static TessellationControlImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the TessellationControlImpl object.</summary>
            /// <returns>A string that represents the TessellationControlImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.TessellationControl()";
            }
        }
        #endregion //TessellationControl

        #region TessellationEvaluation
        public static TessellationEvaluationImpl TessellationEvaluation()
        {
            return TessellationEvaluationImpl.Instance;
            
        }

        internal class TessellationEvaluationImpl: ExecutionModel
        {
            public static readonly TessellationEvaluationImpl Instance = new TessellationEvaluationImpl();
        
            private  TessellationEvaluationImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.TessellationEvaluation;
            public new static TessellationEvaluationImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the TessellationEvaluationImpl object.</summary>
            /// <returns>A string that represents the TessellationEvaluationImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.TessellationEvaluation()";
            }
        }
        #endregion //TessellationEvaluation

        #region Geometry
        public static GeometryImpl Geometry()
        {
            return GeometryImpl.Instance;
            
        }

        internal class GeometryImpl: ExecutionModel
        {
            public static readonly GeometryImpl Instance = new GeometryImpl();
        
            private  GeometryImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.Geometry;
            public new static GeometryImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GeometryImpl object.</summary>
            /// <returns>A string that represents the GeometryImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.Geometry()";
            }
        }
        #endregion //Geometry

        #region Fragment
        public static FragmentImpl Fragment()
        {
            return FragmentImpl.Instance;
            
        }

        internal class FragmentImpl: ExecutionModel
        {
            public static readonly FragmentImpl Instance = new FragmentImpl();
        
            private  FragmentImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.Fragment;
            public new static FragmentImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragmentImpl object.</summary>
            /// <returns>A string that represents the FragmentImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.Fragment()";
            }
        }
        #endregion //Fragment

        #region GLCompute
        public static GLComputeImpl GLCompute()
        {
            return GLComputeImpl.Instance;
            
        }

        internal class GLComputeImpl: ExecutionModel
        {
            public static readonly GLComputeImpl Instance = new GLComputeImpl();
        
            private  GLComputeImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.GLCompute;
            public new static GLComputeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GLComputeImpl object.</summary>
            /// <returns>A string that represents the GLComputeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.GLCompute()";
            }
        }
        #endregion //GLCompute

        #region Kernel
        public static KernelImpl Kernel()
        {
            return KernelImpl.Instance;
            
        }

        internal class KernelImpl: ExecutionModel
        {
            public static readonly KernelImpl Instance = new KernelImpl();
        
            private  KernelImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.Kernel;
            public new static KernelImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the KernelImpl object.</summary>
            /// <returns>A string that represents the KernelImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.Kernel()";
            }
        }
        #endregion //Kernel

        #region TaskNV
        public static TaskNVImpl TaskNV()
        {
            return TaskNVImpl.Instance;
            
        }

        internal class TaskNVImpl: ExecutionModel
        {
            public static readonly TaskNVImpl Instance = new TaskNVImpl();
        
            private  TaskNVImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.TaskNV;
            public new static TaskNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the TaskNVImpl object.</summary>
            /// <returns>A string that represents the TaskNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.TaskNV()";
            }
        }
        #endregion //TaskNV

        #region MeshNV
        public static MeshNVImpl MeshNV()
        {
            return MeshNVImpl.Instance;
            
        }

        internal class MeshNVImpl: ExecutionModel
        {
            public static readonly MeshNVImpl Instance = new MeshNVImpl();
        
            private  MeshNVImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.MeshNV;
            public new static MeshNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the MeshNVImpl object.</summary>
            /// <returns>A string that represents the MeshNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.MeshNV()";
            }
        }
        #endregion //MeshNV

        #region RayGenerationNV
        public static RayGenerationNVImpl RayGenerationNV()
        {
            return RayGenerationNVImpl.Instance;
            
        }

        internal class RayGenerationNVImpl: ExecutionModel
        {
            public static readonly RayGenerationNVImpl Instance = new RayGenerationNVImpl();
        
            private  RayGenerationNVImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.RayGenerationNV;
            public new static RayGenerationNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayGenerationNVImpl object.</summary>
            /// <returns>A string that represents the RayGenerationNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.RayGenerationNV()";
            }
        }
        #endregion //RayGenerationNV

        #region RayGenerationKHR
        public static RayGenerationKHRImpl RayGenerationKHR()
        {
            return RayGenerationKHRImpl.Instance;
            
        }

        internal class RayGenerationKHRImpl: ExecutionModel
        {
            public static readonly RayGenerationKHRImpl Instance = new RayGenerationKHRImpl();
        
            private  RayGenerationKHRImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.RayGenerationKHR;
            public new static RayGenerationKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayGenerationKHRImpl object.</summary>
            /// <returns>A string that represents the RayGenerationKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.RayGenerationKHR()";
            }
        }
        #endregion //RayGenerationKHR

        #region IntersectionNV
        public static IntersectionNVImpl IntersectionNV()
        {
            return IntersectionNVImpl.Instance;
            
        }

        internal class IntersectionNVImpl: ExecutionModel
        {
            public static readonly IntersectionNVImpl Instance = new IntersectionNVImpl();
        
            private  IntersectionNVImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.IntersectionNV;
            public new static IntersectionNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the IntersectionNVImpl object.</summary>
            /// <returns>A string that represents the IntersectionNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.IntersectionNV()";
            }
        }
        #endregion //IntersectionNV

        #region IntersectionKHR
        public static IntersectionKHRImpl IntersectionKHR()
        {
            return IntersectionKHRImpl.Instance;
            
        }

        internal class IntersectionKHRImpl: ExecutionModel
        {
            public static readonly IntersectionKHRImpl Instance = new IntersectionKHRImpl();
        
            private  IntersectionKHRImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.IntersectionKHR;
            public new static IntersectionKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the IntersectionKHRImpl object.</summary>
            /// <returns>A string that represents the IntersectionKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.IntersectionKHR()";
            }
        }
        #endregion //IntersectionKHR

        #region AnyHitNV
        public static AnyHitNVImpl AnyHitNV()
        {
            return AnyHitNVImpl.Instance;
            
        }

        internal class AnyHitNVImpl: ExecutionModel
        {
            public static readonly AnyHitNVImpl Instance = new AnyHitNVImpl();
        
            private  AnyHitNVImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.AnyHitNV;
            public new static AnyHitNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the AnyHitNVImpl object.</summary>
            /// <returns>A string that represents the AnyHitNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.AnyHitNV()";
            }
        }
        #endregion //AnyHitNV

        #region AnyHitKHR
        public static AnyHitKHRImpl AnyHitKHR()
        {
            return AnyHitKHRImpl.Instance;
            
        }

        internal class AnyHitKHRImpl: ExecutionModel
        {
            public static readonly AnyHitKHRImpl Instance = new AnyHitKHRImpl();
        
            private  AnyHitKHRImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.AnyHitKHR;
            public new static AnyHitKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the AnyHitKHRImpl object.</summary>
            /// <returns>A string that represents the AnyHitKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.AnyHitKHR()";
            }
        }
        #endregion //AnyHitKHR

        #region ClosestHitNV
        public static ClosestHitNVImpl ClosestHitNV()
        {
            return ClosestHitNVImpl.Instance;
            
        }

        internal class ClosestHitNVImpl: ExecutionModel
        {
            public static readonly ClosestHitNVImpl Instance = new ClosestHitNVImpl();
        
            private  ClosestHitNVImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.ClosestHitNV;
            public new static ClosestHitNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ClosestHitNVImpl object.</summary>
            /// <returns>A string that represents the ClosestHitNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.ClosestHitNV()";
            }
        }
        #endregion //ClosestHitNV

        #region ClosestHitKHR
        public static ClosestHitKHRImpl ClosestHitKHR()
        {
            return ClosestHitKHRImpl.Instance;
            
        }

        internal class ClosestHitKHRImpl: ExecutionModel
        {
            public static readonly ClosestHitKHRImpl Instance = new ClosestHitKHRImpl();
        
            private  ClosestHitKHRImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.ClosestHitKHR;
            public new static ClosestHitKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ClosestHitKHRImpl object.</summary>
            /// <returns>A string that represents the ClosestHitKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.ClosestHitKHR()";
            }
        }
        #endregion //ClosestHitKHR

        #region MissNV
        public static MissNVImpl MissNV()
        {
            return MissNVImpl.Instance;
            
        }

        internal class MissNVImpl: ExecutionModel
        {
            public static readonly MissNVImpl Instance = new MissNVImpl();
        
            private  MissNVImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.MissNV;
            public new static MissNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the MissNVImpl object.</summary>
            /// <returns>A string that represents the MissNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.MissNV()";
            }
        }
        #endregion //MissNV

        #region MissKHR
        public static MissKHRImpl MissKHR()
        {
            return MissKHRImpl.Instance;
            
        }

        internal class MissKHRImpl: ExecutionModel
        {
            public static readonly MissKHRImpl Instance = new MissKHRImpl();
        
            private  MissKHRImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.MissKHR;
            public new static MissKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the MissKHRImpl object.</summary>
            /// <returns>A string that represents the MissKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.MissKHR()";
            }
        }
        #endregion //MissKHR

        #region CallableNV
        public static CallableNVImpl CallableNV()
        {
            return CallableNVImpl.Instance;
            
        }

        internal class CallableNVImpl: ExecutionModel
        {
            public static readonly CallableNVImpl Instance = new CallableNVImpl();
        
            private  CallableNVImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.CallableNV;
            public new static CallableNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CallableNVImpl object.</summary>
            /// <returns>A string that represents the CallableNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.CallableNV()";
            }
        }
        #endregion //CallableNV

        #region CallableKHR
        public static CallableKHRImpl CallableKHR()
        {
            return CallableKHRImpl.Instance;
            
        }

        internal class CallableKHRImpl: ExecutionModel
        {
            public static readonly CallableKHRImpl Instance = new CallableKHRImpl();
        
            private  CallableKHRImpl()
            {
            }
            public override Enumerant Value => ExecutionModel.Enumerant.CallableKHR;
            public new static CallableKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CallableKHRImpl object.</summary>
            /// <returns>A string that represents the CallableKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" ExecutionModel.CallableKHR()";
            }
        }
        #endregion //CallableKHR

        public abstract Enumerant Value { get; }

        public static ExecutionModel Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Vertex :
                    return VertexImpl.Parse(reader, wordCount - 1);
                case Enumerant.TessellationControl :
                    return TessellationControlImpl.Parse(reader, wordCount - 1);
                case Enumerant.TessellationEvaluation :
                    return TessellationEvaluationImpl.Parse(reader, wordCount - 1);
                case Enumerant.Geometry :
                    return GeometryImpl.Parse(reader, wordCount - 1);
                case Enumerant.Fragment :
                    return FragmentImpl.Parse(reader, wordCount - 1);
                case Enumerant.GLCompute :
                    return GLComputeImpl.Parse(reader, wordCount - 1);
                case Enumerant.Kernel :
                    return KernelImpl.Parse(reader, wordCount - 1);
                case Enumerant.TaskNV :
                    return TaskNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.MeshNV :
                    return MeshNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.RayGenerationNV :
                    return RayGenerationNVImpl.Parse(reader, wordCount - 1);
                //RayGenerationKHR has the same id as another value in enum.
                //case Enumerant.RayGenerationKHR :
                //    return RayGenerationKHR.Parse(reader, wordCount - 1);
                case Enumerant.IntersectionNV :
                    return IntersectionNVImpl.Parse(reader, wordCount - 1);
                //IntersectionKHR has the same id as another value in enum.
                //case Enumerant.IntersectionKHR :
                //    return IntersectionKHR.Parse(reader, wordCount - 1);
                case Enumerant.AnyHitNV :
                    return AnyHitNVImpl.Parse(reader, wordCount - 1);
                //AnyHitKHR has the same id as another value in enum.
                //case Enumerant.AnyHitKHR :
                //    return AnyHitKHR.Parse(reader, wordCount - 1);
                case Enumerant.ClosestHitNV :
                    return ClosestHitNVImpl.Parse(reader, wordCount - 1);
                //ClosestHitKHR has the same id as another value in enum.
                //case Enumerant.ClosestHitKHR :
                //    return ClosestHitKHR.Parse(reader, wordCount - 1);
                case Enumerant.MissNV :
                    return MissNVImpl.Parse(reader, wordCount - 1);
                //MissKHR has the same id as another value in enum.
                //case Enumerant.MissKHR :
                //    return MissKHR.Parse(reader, wordCount - 1);
                case Enumerant.CallableNV :
                    return CallableNVImpl.Parse(reader, wordCount - 1);
                //CallableKHR has the same id as another value in enum.
                //case Enumerant.CallableKHR :
                //    return CallableKHR.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown ExecutionModel "+id);
            }
        }
        
        public static ExecutionModel ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<ExecutionModel> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<ExecutionModel>();
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