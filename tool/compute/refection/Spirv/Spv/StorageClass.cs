using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class StorageClass : ValueEnum
    {
        public enum Enumerant
        {
            UniformConstant = 0,
            Input = 1,
            [Capability(Capability.Enumerant.Shader)]
            Uniform = 2,
            [Capability(Capability.Enumerant.Shader)]
            Output = 3,
            Workgroup = 4,
            CrossWorkgroup = 5,
            [Capability(Capability.Enumerant.Shader)]
            Private = 6,
            Function = 7,
            [Capability(Capability.Enumerant.GenericPointer)]
            Generic = 8,
            [Capability(Capability.Enumerant.Shader)]
            PushConstant = 9,
            [Capability(Capability.Enumerant.AtomicStorage)]
            AtomicCounter = 10,
            Image = 11,
            [Capability(Capability.Enumerant.Shader)]
            StorageBuffer = 12,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            CallableDataNV = 5328,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            CallableDataKHR = 5328,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            IncomingCallableDataNV = 5329,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            IncomingCallableDataKHR = 5329,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            RayPayloadNV = 5338,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            RayPayloadKHR = 5338,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            HitAttributeNV = 5339,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            HitAttributeKHR = 5339,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            IncomingRayPayloadNV = 5342,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            IncomingRayPayloadKHR = 5342,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            ShaderRecordBufferNV = 5343,
            [Capability(Capability.Enumerant.RayTracingNV)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            ShaderRecordBufferKHR = 5343,
            [Capability(Capability.Enumerant.PhysicalStorageBufferAddresses)]
            PhysicalStorageBuffer = 5349,
            [Capability(Capability.Enumerant.PhysicalStorageBufferAddresses)]
            PhysicalStorageBufferEXT = 5349,
        }

        #region UniformConstant
        public static UniformConstantImpl UniformConstant()
        {
            return UniformConstantImpl.Instance;
            
        }

        public class UniformConstantImpl: StorageClass
        {
            public static readonly UniformConstantImpl Instance = new UniformConstantImpl();
        
            private  UniformConstantImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.UniformConstant;
            public new static UniformConstantImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UniformConstantImpl object.</summary>
            /// <returns>A string that represents the UniformConstantImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.UniformConstant()";
            }
        }
        #endregion //UniformConstant

        #region Input
        public static InputImpl Input()
        {
            return InputImpl.Instance;
            
        }

        public class InputImpl: StorageClass
        {
            public static readonly InputImpl Instance = new InputImpl();
        
            private  InputImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.Input;
            public new static InputImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InputImpl object.</summary>
            /// <returns>A string that represents the InputImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.Input()";
            }
        }
        #endregion //Input

        #region Uniform
        public static UniformImpl Uniform()
        {
            return UniformImpl.Instance;
            
        }

        public class UniformImpl: StorageClass
        {
            public static readonly UniformImpl Instance = new UniformImpl();
        
            private  UniformImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.Uniform;
            public new static UniformImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UniformImpl object.</summary>
            /// <returns>A string that represents the UniformImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.Uniform()";
            }
        }
        #endregion //Uniform

        #region Output
        public static OutputImpl Output()
        {
            return OutputImpl.Instance;
            
        }

        public class OutputImpl: StorageClass
        {
            public static readonly OutputImpl Instance = new OutputImpl();
        
            private  OutputImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.Output;
            public new static OutputImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the OutputImpl object.</summary>
            /// <returns>A string that represents the OutputImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.Output()";
            }
        }
        #endregion //Output

        #region Workgroup
        public static WorkgroupImpl Workgroup()
        {
            return WorkgroupImpl.Instance;
            
        }

        public class WorkgroupImpl: StorageClass
        {
            public static readonly WorkgroupImpl Instance = new WorkgroupImpl();
        
            private  WorkgroupImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.Workgroup;
            public new static WorkgroupImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the WorkgroupImpl object.</summary>
            /// <returns>A string that represents the WorkgroupImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.Workgroup()";
            }
        }
        #endregion //Workgroup

        #region CrossWorkgroup
        public static CrossWorkgroupImpl CrossWorkgroup()
        {
            return CrossWorkgroupImpl.Instance;
            
        }

        public class CrossWorkgroupImpl: StorageClass
        {
            public static readonly CrossWorkgroupImpl Instance = new CrossWorkgroupImpl();
        
            private  CrossWorkgroupImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.CrossWorkgroup;
            public new static CrossWorkgroupImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CrossWorkgroupImpl object.</summary>
            /// <returns>A string that represents the CrossWorkgroupImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.CrossWorkgroup()";
            }
        }
        #endregion //CrossWorkgroup

        #region Private
        public static PrivateImpl Private()
        {
            return PrivateImpl.Instance;
            
        }

        public class PrivateImpl: StorageClass
        {
            public static readonly PrivateImpl Instance = new PrivateImpl();
        
            private  PrivateImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.Private;
            public new static PrivateImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PrivateImpl object.</summary>
            /// <returns>A string that represents the PrivateImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.Private()";
            }
        }
        #endregion //Private

        #region Function
        public static FunctionImpl Function()
        {
            return FunctionImpl.Instance;
            
        }

        public class FunctionImpl: StorageClass
        {
            public static readonly FunctionImpl Instance = new FunctionImpl();
        
            private  FunctionImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.Function;
            public new static FunctionImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FunctionImpl object.</summary>
            /// <returns>A string that represents the FunctionImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.Function()";
            }
        }
        #endregion //Function

        #region Generic
        public static GenericImpl Generic()
        {
            return GenericImpl.Instance;
            
        }

        public class GenericImpl: StorageClass
        {
            public static readonly GenericImpl Instance = new GenericImpl();
        
            private  GenericImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.Generic;
            public new static GenericImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GenericImpl object.</summary>
            /// <returns>A string that represents the GenericImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.Generic()";
            }
        }
        #endregion //Generic

        #region PushConstant
        public static PushConstantImpl PushConstant()
        {
            return PushConstantImpl.Instance;
            
        }

        public class PushConstantImpl: StorageClass
        {
            public static readonly PushConstantImpl Instance = new PushConstantImpl();
        
            private  PushConstantImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.PushConstant;
            public new static PushConstantImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PushConstantImpl object.</summary>
            /// <returns>A string that represents the PushConstantImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.PushConstant()";
            }
        }
        #endregion //PushConstant

        #region AtomicCounter
        public static AtomicCounterImpl AtomicCounter()
        {
            return AtomicCounterImpl.Instance;
            
        }

        public class AtomicCounterImpl: StorageClass
        {
            public static readonly AtomicCounterImpl Instance = new AtomicCounterImpl();
        
            private  AtomicCounterImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.AtomicCounter;
            public new static AtomicCounterImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the AtomicCounterImpl object.</summary>
            /// <returns>A string that represents the AtomicCounterImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.AtomicCounter()";
            }
        }
        #endregion //AtomicCounter

        #region Image
        public static ImageImpl Image()
        {
            return ImageImpl.Instance;
            
        }

        public class ImageImpl: StorageClass
        {
            public static readonly ImageImpl Instance = new ImageImpl();
        
            private  ImageImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.Image;
            public new static ImageImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageImpl object.</summary>
            /// <returns>A string that represents the ImageImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.Image()";
            }
        }
        #endregion //Image

        #region StorageBuffer
        public static StorageBufferImpl StorageBuffer()
        {
            return StorageBufferImpl.Instance;
            
        }

        public class StorageBufferImpl: StorageClass
        {
            public static readonly StorageBufferImpl Instance = new StorageBufferImpl();
        
            private  StorageBufferImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.StorageBuffer;
            public new static StorageBufferImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageBufferImpl object.</summary>
            /// <returns>A string that represents the StorageBufferImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.StorageBuffer()";
            }
        }
        #endregion //StorageBuffer

        #region CallableDataNV
        public static CallableDataNVImpl CallableDataNV()
        {
            return CallableDataNVImpl.Instance;
            
        }

        public class CallableDataNVImpl: StorageClass
        {
            public static readonly CallableDataNVImpl Instance = new CallableDataNVImpl();
        
            private  CallableDataNVImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.CallableDataNV;
            public new static CallableDataNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CallableDataNVImpl object.</summary>
            /// <returns>A string that represents the CallableDataNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.CallableDataNV()";
            }
        }
        #endregion //CallableDataNV

        #region CallableDataKHR
        public static CallableDataKHRImpl CallableDataKHR()
        {
            return CallableDataKHRImpl.Instance;
            
        }

        public class CallableDataKHRImpl: StorageClass
        {
            public static readonly CallableDataKHRImpl Instance = new CallableDataKHRImpl();
        
            private  CallableDataKHRImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.CallableDataKHR;
            public new static CallableDataKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CallableDataKHRImpl object.</summary>
            /// <returns>A string that represents the CallableDataKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.CallableDataKHR()";
            }
        }
        #endregion //CallableDataKHR

        #region IncomingCallableDataNV
        public static IncomingCallableDataNVImpl IncomingCallableDataNV()
        {
            return IncomingCallableDataNVImpl.Instance;
            
        }

        public class IncomingCallableDataNVImpl: StorageClass
        {
            public static readonly IncomingCallableDataNVImpl Instance = new IncomingCallableDataNVImpl();
        
            private  IncomingCallableDataNVImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.IncomingCallableDataNV;
            public new static IncomingCallableDataNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the IncomingCallableDataNVImpl object.</summary>
            /// <returns>A string that represents the IncomingCallableDataNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.IncomingCallableDataNV()";
            }
        }
        #endregion //IncomingCallableDataNV

        #region IncomingCallableDataKHR
        public static IncomingCallableDataKHRImpl IncomingCallableDataKHR()
        {
            return IncomingCallableDataKHRImpl.Instance;
            
        }

        public class IncomingCallableDataKHRImpl: StorageClass
        {
            public static readonly IncomingCallableDataKHRImpl Instance = new IncomingCallableDataKHRImpl();
        
            private  IncomingCallableDataKHRImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.IncomingCallableDataKHR;
            public new static IncomingCallableDataKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the IncomingCallableDataKHRImpl object.</summary>
            /// <returns>A string that represents the IncomingCallableDataKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.IncomingCallableDataKHR()";
            }
        }
        #endregion //IncomingCallableDataKHR

        #region RayPayloadNV
        public static RayPayloadNVImpl RayPayloadNV()
        {
            return RayPayloadNVImpl.Instance;
            
        }

        public class RayPayloadNVImpl: StorageClass
        {
            public static readonly RayPayloadNVImpl Instance = new RayPayloadNVImpl();
        
            private  RayPayloadNVImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.RayPayloadNV;
            public new static RayPayloadNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayPayloadNVImpl object.</summary>
            /// <returns>A string that represents the RayPayloadNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.RayPayloadNV()";
            }
        }
        #endregion //RayPayloadNV

        #region RayPayloadKHR
        public static RayPayloadKHRImpl RayPayloadKHR()
        {
            return RayPayloadKHRImpl.Instance;
            
        }

        public class RayPayloadKHRImpl: StorageClass
        {
            public static readonly RayPayloadKHRImpl Instance = new RayPayloadKHRImpl();
        
            private  RayPayloadKHRImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.RayPayloadKHR;
            public new static RayPayloadKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayPayloadKHRImpl object.</summary>
            /// <returns>A string that represents the RayPayloadKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.RayPayloadKHR()";
            }
        }
        #endregion //RayPayloadKHR

        #region HitAttributeNV
        public static HitAttributeNVImpl HitAttributeNV()
        {
            return HitAttributeNVImpl.Instance;
            
        }

        public class HitAttributeNVImpl: StorageClass
        {
            public static readonly HitAttributeNVImpl Instance = new HitAttributeNVImpl();
        
            private  HitAttributeNVImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.HitAttributeNV;
            public new static HitAttributeNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the HitAttributeNVImpl object.</summary>
            /// <returns>A string that represents the HitAttributeNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.HitAttributeNV()";
            }
        }
        #endregion //HitAttributeNV

        #region HitAttributeKHR
        public static HitAttributeKHRImpl HitAttributeKHR()
        {
            return HitAttributeKHRImpl.Instance;
            
        }

        public class HitAttributeKHRImpl: StorageClass
        {
            public static readonly HitAttributeKHRImpl Instance = new HitAttributeKHRImpl();
        
            private  HitAttributeKHRImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.HitAttributeKHR;
            public new static HitAttributeKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the HitAttributeKHRImpl object.</summary>
            /// <returns>A string that represents the HitAttributeKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.HitAttributeKHR()";
            }
        }
        #endregion //HitAttributeKHR

        #region IncomingRayPayloadNV
        public static IncomingRayPayloadNVImpl IncomingRayPayloadNV()
        {
            return IncomingRayPayloadNVImpl.Instance;
            
        }

        public class IncomingRayPayloadNVImpl: StorageClass
        {
            public static readonly IncomingRayPayloadNVImpl Instance = new IncomingRayPayloadNVImpl();
        
            private  IncomingRayPayloadNVImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.IncomingRayPayloadNV;
            public new static IncomingRayPayloadNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the IncomingRayPayloadNVImpl object.</summary>
            /// <returns>A string that represents the IncomingRayPayloadNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.IncomingRayPayloadNV()";
            }
        }
        #endregion //IncomingRayPayloadNV

        #region IncomingRayPayloadKHR
        public static IncomingRayPayloadKHRImpl IncomingRayPayloadKHR()
        {
            return IncomingRayPayloadKHRImpl.Instance;
            
        }

        public class IncomingRayPayloadKHRImpl: StorageClass
        {
            public static readonly IncomingRayPayloadKHRImpl Instance = new IncomingRayPayloadKHRImpl();
        
            private  IncomingRayPayloadKHRImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.IncomingRayPayloadKHR;
            public new static IncomingRayPayloadKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the IncomingRayPayloadKHRImpl object.</summary>
            /// <returns>A string that represents the IncomingRayPayloadKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.IncomingRayPayloadKHR()";
            }
        }
        #endregion //IncomingRayPayloadKHR

        #region ShaderRecordBufferNV
        public static ShaderRecordBufferNVImpl ShaderRecordBufferNV()
        {
            return ShaderRecordBufferNVImpl.Instance;
            
        }

        public class ShaderRecordBufferNVImpl: StorageClass
        {
            public static readonly ShaderRecordBufferNVImpl Instance = new ShaderRecordBufferNVImpl();
        
            private  ShaderRecordBufferNVImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.ShaderRecordBufferNV;
            public new static ShaderRecordBufferNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderRecordBufferNVImpl object.</summary>
            /// <returns>A string that represents the ShaderRecordBufferNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.ShaderRecordBufferNV()";
            }
        }
        #endregion //ShaderRecordBufferNV

        #region ShaderRecordBufferKHR
        public static ShaderRecordBufferKHRImpl ShaderRecordBufferKHR()
        {
            return ShaderRecordBufferKHRImpl.Instance;
            
        }

        public class ShaderRecordBufferKHRImpl: StorageClass
        {
            public static readonly ShaderRecordBufferKHRImpl Instance = new ShaderRecordBufferKHRImpl();
        
            private  ShaderRecordBufferKHRImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.ShaderRecordBufferKHR;
            public new static ShaderRecordBufferKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderRecordBufferKHRImpl object.</summary>
            /// <returns>A string that represents the ShaderRecordBufferKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.ShaderRecordBufferKHR()";
            }
        }
        #endregion //ShaderRecordBufferKHR

        #region PhysicalStorageBuffer
        public static PhysicalStorageBufferImpl PhysicalStorageBuffer()
        {
            return PhysicalStorageBufferImpl.Instance;
            
        }

        public class PhysicalStorageBufferImpl: StorageClass
        {
            public static readonly PhysicalStorageBufferImpl Instance = new PhysicalStorageBufferImpl();
        
            private  PhysicalStorageBufferImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.PhysicalStorageBuffer;
            public new static PhysicalStorageBufferImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PhysicalStorageBufferImpl object.</summary>
            /// <returns>A string that represents the PhysicalStorageBufferImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.PhysicalStorageBuffer()";
            }
        }
        #endregion //PhysicalStorageBuffer

        #region PhysicalStorageBufferEXT
        public static PhysicalStorageBufferEXTImpl PhysicalStorageBufferEXT()
        {
            return PhysicalStorageBufferEXTImpl.Instance;
            
        }

        public class PhysicalStorageBufferEXTImpl: StorageClass
        {
            public static readonly PhysicalStorageBufferEXTImpl Instance = new PhysicalStorageBufferEXTImpl();
        
            private  PhysicalStorageBufferEXTImpl()
            {
            }
            public override Enumerant Value => StorageClass.Enumerant.PhysicalStorageBufferEXT;
            public new static PhysicalStorageBufferEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PhysicalStorageBufferEXTImpl object.</summary>
            /// <returns>A string that represents the PhysicalStorageBufferEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" StorageClass.PhysicalStorageBufferEXT()";
            }
        }
        #endregion //PhysicalStorageBufferEXT

        public abstract Enumerant Value { get; }

        public static StorageClass Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.UniformConstant :
                    return UniformConstantImpl.Parse(reader, wordCount - 1);
                case Enumerant.Input :
                    return InputImpl.Parse(reader, wordCount - 1);
                case Enumerant.Uniform :
                    return UniformImpl.Parse(reader, wordCount - 1);
                case Enumerant.Output :
                    return OutputImpl.Parse(reader, wordCount - 1);
                case Enumerant.Workgroup :
                    return WorkgroupImpl.Parse(reader, wordCount - 1);
                case Enumerant.CrossWorkgroup :
                    return CrossWorkgroupImpl.Parse(reader, wordCount - 1);
                case Enumerant.Private :
                    return PrivateImpl.Parse(reader, wordCount - 1);
                case Enumerant.Function :
                    return FunctionImpl.Parse(reader, wordCount - 1);
                case Enumerant.Generic :
                    return GenericImpl.Parse(reader, wordCount - 1);
                case Enumerant.PushConstant :
                    return PushConstantImpl.Parse(reader, wordCount - 1);
                case Enumerant.AtomicCounter :
                    return AtomicCounterImpl.Parse(reader, wordCount - 1);
                case Enumerant.Image :
                    return ImageImpl.Parse(reader, wordCount - 1);
                case Enumerant.StorageBuffer :
                    return StorageBufferImpl.Parse(reader, wordCount - 1);
                case Enumerant.CallableDataNV :
                    return CallableDataNVImpl.Parse(reader, wordCount - 1);
                //CallableDataKHR has the same id as another value in enum.
                //case Enumerant.CallableDataKHR :
                //    return CallableDataKHR.Parse(reader, wordCount - 1);
                case Enumerant.IncomingCallableDataNV :
                    return IncomingCallableDataNVImpl.Parse(reader, wordCount - 1);
                //IncomingCallableDataKHR has the same id as another value in enum.
                //case Enumerant.IncomingCallableDataKHR :
                //    return IncomingCallableDataKHR.Parse(reader, wordCount - 1);
                case Enumerant.RayPayloadNV :
                    return RayPayloadNVImpl.Parse(reader, wordCount - 1);
                //RayPayloadKHR has the same id as another value in enum.
                //case Enumerant.RayPayloadKHR :
                //    return RayPayloadKHR.Parse(reader, wordCount - 1);
                case Enumerant.HitAttributeNV :
                    return HitAttributeNVImpl.Parse(reader, wordCount - 1);
                //HitAttributeKHR has the same id as another value in enum.
                //case Enumerant.HitAttributeKHR :
                //    return HitAttributeKHR.Parse(reader, wordCount - 1);
                case Enumerant.IncomingRayPayloadNV :
                    return IncomingRayPayloadNVImpl.Parse(reader, wordCount - 1);
                //IncomingRayPayloadKHR has the same id as another value in enum.
                //case Enumerant.IncomingRayPayloadKHR :
                //    return IncomingRayPayloadKHR.Parse(reader, wordCount - 1);
                case Enumerant.ShaderRecordBufferNV :
                    return ShaderRecordBufferNVImpl.Parse(reader, wordCount - 1);
                //ShaderRecordBufferKHR has the same id as another value in enum.
                //case Enumerant.ShaderRecordBufferKHR :
                //    return ShaderRecordBufferKHR.Parse(reader, wordCount - 1);
                case Enumerant.PhysicalStorageBuffer :
                    return PhysicalStorageBufferImpl.Parse(reader, wordCount - 1);
                //PhysicalStorageBufferEXT has the same id as another value in enum.
                //case Enumerant.PhysicalStorageBufferEXT :
                //    return PhysicalStorageBufferEXT.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown StorageClass "+id);
            }
        }
        
        public static StorageClass ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<StorageClass> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<StorageClass>();
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