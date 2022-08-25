using compute.drawing;
using compute.environment;
using compute.vulkan;
using System;
using System.Collections.Generic;
using System.Linq;
using static compute.environment.content.Loader;

namespace compute
{
    public partial class Surface : IApp
    {
        private PipelineApp mApp;
        private IPipeline[] mPipelines;
        private BufferDescriptor[] mBuffers;
        private ImageDescriptor[] mImages;
        private Dictionary<Shader, ShaderModule> mShaderModules;

        public IPipeline[] Pipelines => mPipelines;

        public BufferDescriptor[] BufferDescriptors => mBuffers;

        public ImageDescriptor[] ImageDescriptors => mImages;

        internal VulkanContext Context => mApp.Context;

        internal ContentManager Content => mApp.Content;

        public unsafe Surface(params IPipeline[] pipelines) 
        {
            mPipelines = pipelines;      
        }

        public void Tick(Timer timer)
        {
            mApp.Tick(timer);
        }

        public unsafe void Const<T>(string name, T[] buffers) 
            where T : struct
        {
            var size = (uint)Interop.SizeOf<T>();
            var index = GetBufferDescriptorIndex(name);
            var descriptor = BufferDescriptors[index];

            if (descriptor.Interface.ShaderType.Size != size)
                throw new ArgumentException("uniforms size not same.");

            if (BufferDescriptors[index].Buffer != null)
                throw new ArgumentException("const can not change.");

            BufferDescriptors[index] = new BufferDescriptor(descriptor.Usage, descriptor.Interface, CreateBuffer(Context, descriptor.Interface, buffers));
        }

        public void Const(string name, IImage image)
        {
            var index = GetImageDescriptorIndex(name);
            var descriptor = ImageDescriptors[index];
            if (descriptor.Interface.InterfaceType == InterfaceType.OpTypeStruct)
                throw new ArgumentException("uniforms type not same.");

            ImageDescriptors[index] = new ImageDescriptor(descriptor.Usage, descriptor.Interface, CreateImage(Context, descriptor.Interface, image), null);
        }

        public void Dynamic<T>(string name, params T[] uniforms) 
            where T : struct
        {
            var size = (uint)Interop.SizeOf<T>();

            var index = GetBufferDescriptorIndex(name);
            var descriptor = BufferDescriptors[index];
            if (descriptor.Interface.ShaderType.Size != size)
                throw new ArgumentException("uniforms size not same.");

            var buffer = BufferDescriptors[index].Buffer;
            if (buffer != null)
            {
                if(buffer.Count != uniforms.Length)
                    throw new ArgumentException("uniforms count not same.");

                IntPtr ptr = buffer.Memory.Map(0, Constant.WholeSize);
                for (var i = 0; i < uniforms.Length; i++)
                    Interop.Write(ptr + (int)(i * descriptor.Interface.ShaderType.Size), ref uniforms[i]);

                buffer.Memory.Unmap();
            }
            else
            {
                BufferDescriptors[index] = new BufferDescriptor(descriptor.Usage, descriptor.Interface, CreateBuffer(Context, descriptor.Interface, uniforms));
            }
        }

        internal BufferDescriptor GetBufferDescriptor(Interface x)
        {
            return BufferDescriptors[GetBufferDescriptorIndex(x.Name)];
        }

        internal ImageDescriptor GetImageDescriptor(Interface x) 
        {
            return ImageDescriptors[GetImageDescriptorIndex(x.Name)];
        }

        internal VulkanBuffer GetVulkanBuffer(string name) 
        {
            return BufferDescriptors[GetBufferDescriptorIndex(name)].Buffer;
        }

        internal Image GetVulkanUmage(string name) 
        {
            return ImageDescriptors[GetImageDescriptorIndex(name)].Image;
        }

        internal ShaderModule GetVulkanShader(ShaderEntry entry)
        {
            if(!mShaderModules.ContainsKey(entry.Shader))
                mShaderModules[entry.Shader] = Context.Device.CreateShaderModule(new ShaderModuleCreateInfo(entry.Shader.ShaderBytes));

            return mShaderModules[entry.Shader];
        }

        private int GetBufferDescriptorIndex(string name) 
        {
            for (var i = 0; i < BufferDescriptors.Length; i++)
                if (BufferDescriptors[i].Interface.Name == name)
                    return i;

            throw new ArgumentException("name index");
        }

        private int GetImageDescriptorIndex(string name)
        {
            for (var i = 0; i < ImageDescriptors.Length; i++)
                if (ImageDescriptors[i].Interface.Name == name)
                    return i;

            throw new ArgumentException("name index");
        }

        private static Image CreateImage(VulkanContext ctx, Interface @interface, IImage image) 
        {
            return LoadUncompressedVulkanImage(null, ctx, image).Image;
        }

        private static VulkanBuffer CreateBuffer<T>(VulkanContext ctx, Interface @interface, params T[] datas)
            where T : struct
        {
            VulkanBuffer buffer = null;
            if (@interface.InterfaceType == InterfaceType.OpTypeStruct)
            {
                if (@interface.InterfaceClass == InterfaceClass.Uniform)
                {
                    buffer = VulkanBuffer.DynamicUniform(ctx, @interface.ShaderType.Size, datas.Length);
                    IntPtr ptr = buffer.Memory.Map(0, Constant.WholeSize);
                    for (var i = 0; i < datas.Length; i++)
                        Interop.Write(ptr + (int)(i * @interface.ShaderType.Size), ref datas[i]);

                    buffer.Memory.Unmap();
                }
                else if (@interface.InterfaceClass == InterfaceClass.Input)
                {
                    buffer = VulkanBuffer.Storage(ctx, datas);
                }
            }

            if (buffer == null)
                throw new NotSupportedException($"{Enum.GetName(typeof(InterfaceClass), @interface.InterfaceClass)}");

            return buffer;
        }

        public void Resize()
        {
            mApp.Resize();
        }

        public void Initialize(IAppHost host)
        {
            mShaderModules = new Dictionary<Shader, ShaderModule>();

            mBuffers = mPipelines.SelectMany(x => x.GetEntryPoints()).SelectMany(x => x.Interfaces).Where(x => x.InterfaceType == InterfaceType.OpTypeStruct).Select(x => new BufferDescriptor(SurfaceDescriptorUsage.None, x, null)).ToArray();
            mImages = mPipelines.SelectMany(x => x.GetEntryPoints()).SelectMany(x => x.Interfaces).Where(x => x.InterfaceType != InterfaceType.OpTypeStruct).Select(x => new ImageDescriptor(SurfaceDescriptorUsage.None, x, null, null)).ToArray();

            mApp = new PipelineApp();
            mApp.Initialize(host, this);
        }

        public void Dispose()
        {
            mApp.Dispose();

            Disposed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when the window is disposed.
        /// </summary>
        public event EventHandler Disposed;
    }
}
