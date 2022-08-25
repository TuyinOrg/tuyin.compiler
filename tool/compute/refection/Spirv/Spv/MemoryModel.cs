using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class MemoryModel : ValueEnum
    {
        public enum Enumerant
        {
            [Capability(Capability.Enumerant.Shader)]
            Simple = 0,
            [Capability(Capability.Enumerant.Shader)]
            GLSL450 = 1,
            [Capability(Capability.Enumerant.Kernel)]
            OpenCL = 2,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            Vulkan = 3,
            [Capability(Capability.Enumerant.VulkanMemoryModel)]
            VulkanKHR = 3,
        }

        #region Simple
        public static SimpleImpl Simple()
        {
            return SimpleImpl.Instance;
            
        }

        internal class SimpleImpl: MemoryModel
        {
            public static readonly SimpleImpl Instance = new SimpleImpl();
        
            private  SimpleImpl()
            {
            }
            public override Enumerant Value => MemoryModel.Enumerant.Simple;
            public new static SimpleImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SimpleImpl object.</summary>
            /// <returns>A string that represents the SimpleImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" MemoryModel.Simple()";
            }
        }
        #endregion //Simple

        #region GLSL450
        public static GLSL450Impl GLSL450()
        {
            return GLSL450Impl.Instance;
            
        }

        internal class GLSL450Impl: MemoryModel
        {
            public static readonly GLSL450Impl Instance = new GLSL450Impl();
        
            private  GLSL450Impl()
            {
            }
            public override Enumerant Value => MemoryModel.Enumerant.GLSL450;
            public new static GLSL450Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GLSL450Impl object.</summary>
            /// <returns>A string that represents the GLSL450Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" MemoryModel.GLSL450()";
            }
        }
        #endregion //GLSL450

        #region OpenCL
        public static OpenCLImpl OpenCL()
        {
            return OpenCLImpl.Instance;
            
        }

        internal class OpenCLImpl: MemoryModel
        {
            public static readonly OpenCLImpl Instance = new OpenCLImpl();
        
            private  OpenCLImpl()
            {
            }
            public override Enumerant Value => MemoryModel.Enumerant.OpenCL;
            public new static OpenCLImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the OpenCLImpl object.</summary>
            /// <returns>A string that represents the OpenCLImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" MemoryModel.OpenCL()";
            }
        }
        #endregion //OpenCL

        #region Vulkan
        public static VulkanImpl Vulkan()
        {
            return VulkanImpl.Instance;
            
        }

        internal class VulkanImpl: MemoryModel
        {
            public static readonly VulkanImpl Instance = new VulkanImpl();
        
            private  VulkanImpl()
            {
            }
            public override Enumerant Value => MemoryModel.Enumerant.Vulkan;
            public new static VulkanImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VulkanImpl object.</summary>
            /// <returns>A string that represents the VulkanImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" MemoryModel.Vulkan()";
            }
        }
        #endregion //Vulkan

        #region VulkanKHR
        public static VulkanKHRImpl VulkanKHR()
        {
            return VulkanKHRImpl.Instance;
            
        }

        internal class VulkanKHRImpl: MemoryModel
        {
            public static readonly VulkanKHRImpl Instance = new VulkanKHRImpl();
        
            private  VulkanKHRImpl()
            {
            }
            public override Enumerant Value => MemoryModel.Enumerant.VulkanKHR;
            public new static VulkanKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VulkanKHRImpl object.</summary>
            /// <returns>A string that represents the VulkanKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" MemoryModel.VulkanKHR()";
            }
        }
        #endregion //VulkanKHR

        public abstract Enumerant Value { get; }

        public static MemoryModel Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Simple :
                    return SimpleImpl.Parse(reader, wordCount - 1);
                case Enumerant.GLSL450 :
                    return GLSL450Impl.Parse(reader, wordCount - 1);
                case Enumerant.OpenCL :
                    return OpenCLImpl.Parse(reader, wordCount - 1);
                case Enumerant.Vulkan :
                    return VulkanImpl.Parse(reader, wordCount - 1);
                //VulkanKHR has the same id as another value in enum.
                //case Enumerant.VulkanKHR :
                //    return VulkanKHR.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown MemoryModel "+id);
            }
        }
        
        public static MemoryModel ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<MemoryModel> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<MemoryModel>();
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