using compute.drawing;
using compute.environment;
using compute.environment.content;
using compute.vulkan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Constant = compute.vulkan.Constant;

namespace compute
{ 
    partial class Surface
    {
        enum PipelineShaderType                                                 
        {
            None,
            Compute,
            Graphics
        }

        abstract class PipelineShader                                           
        {
            private DescriptorPool mDescriptorPool;
            private DescriptorSetLayout[] mDescriptorSetLayouts;
            private DescriptorSet[] mDescriptorSets;
            private PipelineLayout mPipelineLayout;

            private Pipeline mPipeline;
            private CommandBuffer mCmdBuffer;

            public abstract PipelineShaderType ShaderType { get; }

            public PipelineSubpass Subpass { get; }

            public CommandBuffer Command => mCmdBuffer;

            public VulkanContext Context => Subpass.Context;

            public PipelineShader PrevShader { get; private set; }

            public PipelineShader NextShader { get; private set; }

            public PipelineShader(PipelineSubpass buffer)
            {
                Subpass = buffer;
            }

            public void BindingLevel(PipelineShader prevShader, PipelineShader nextShader) 
            {
                PrevShader = prevShader;
                NextShader = nextShader;
            }

            protected internal virtual void InitializePermanent(DescriptorPool descriptorPool)
            {
                mDescriptorPool = descriptorPool;
                mDescriptorSetLayouts = ToDispose(CreateDescriptorSetLayouts());
                mDescriptorSets = ToDispose(CreateDescriptorSets());
                mPipelineLayout = ToDispose(CreatePipelineLayout());

                mCmdBuffer = Context.ComputeCommandPool.AllocateBuffers(new CommandBufferAllocateInfo(CommandBufferLevel.Primary, 1))[0];
            }

            /// <summary>
            /// 动态信息重置
            /// </summary>
            protected internal virtual void InitializeFrame()
            {
                mPipeline = ToDispose(CreatePipeline(mPipelineLayout));
            }

            private DescriptorSetLayout[] CreateDescriptorSetLayouts()
            {
                var interfaceGroup = GetInterfaces().Where(x => x.InterfaceType != InterfaceType.Unknown).GroupBy(x => x.DescriptorSet);
                var layoutBindings = interfaceGroup.OrderBy(x => x.Key).SelectMany(x => x.Select(y => CreateLayoutBinding(y))).ToArray();
                return layoutBindings.Select(x => Context.Device.CreateDescriptorSetLayout(new DescriptorSetLayoutCreateInfo(x))).ToArray();
            }

            private DescriptorSet[] CreateDescriptorSets()
            {
 
                var setIndex = 0;
                var interfaces = GetInterfaces().ToArray();
                var maxSet = interfaces.Max(x => x.DescriptorSet);
                var descriptorSets = mDescriptorPool.AllocateSets(new DescriptorSetAllocateInfo(maxSet, mDescriptorSetLayouts)).ToDictionary(x => setIndex++, x => x);
                var sets = new WriteDescriptorSet[interfaces.Length];

                for (var i = 0; i < interfaces.Length; i++)
                {
                    var face = interfaces[i];
                    var set = face.DescriptorSet;
                    if (face.InterfaceType == InterfaceType.OpTypeStruct)
                    {
                        var buffer = Subpass.App.Surface.GetBufferDescriptor(face);
                        if (face.InterfaceClass == InterfaceClass.Uniform)
                        {
                            sets[i] = new WriteDescriptorSet(
                                descriptorSets[set],
                                face.Location,
                                0,
                                1,
                                face.DescriptorType,
                                bufferInfo: new[] { new DescriptorBufferInfo(buffer.Buffer) });
                        }
                        else if(face.InterfaceClass == InterfaceClass.Input)
                        {
                            sets[i] = new WriteDescriptorSet(
                                descriptorSets[set],
                                face.Location,
                                0,
                                1,
                                face.DescriptorType,
                                bufferInfo: new[] { new DescriptorBufferInfo(buffer.Buffer) });
                        }

                        throw new NotSupportedException();
                    }
                    else
                    {                 
                        var image = Subpass.App.Surface.GetImageDescriptor(face);
 
                        sets[i] = new WriteDescriptorSet(
                            descriptorSets[set],
                            face.Location,
                            0,
                            1,
                            face.DescriptorType,
                            new[] { new DescriptorImageInfo(image.Sampler, image.Image, ImageLayout.ColorAttachmentOptimal) });
                    }
                }

                mDescriptorPool.UpdateSets(sets);
                return descriptorSets.Values.ToArray();
            }

            private PipelineLayout CreatePipelineLayout()
            {
                return Context.Device.CreatePipelineLayout(new PipelineLayoutCreateInfo(mDescriptorSetLayouts));
            }

            protected abstract Pipeline CreatePipeline(PipelineLayout pipelineLayout);

            internal void RecordComputeCommands() 
            {
                RecordComputeCommandBuffer(mPipeline, mPipelineLayout, mDescriptorSets);
            }

            internal void RecordGraphicsCommands(CommandBuffer cmdBuffer, int imageIndex) 
            {
                RecordGraphicsCommandBuffer(mPipeline, mPipelineLayout, mDescriptorSets, cmdBuffer, imageIndex);
            }

            protected virtual void RecordGraphicsCommandBuffer(Pipeline pipeline, PipelineLayout pipelineLayout, DescriptorSet[] descriptorSets, CommandBuffer cmdBuffer, int imageIndex) 
            {

            }

            protected virtual void RecordComputeCommandBuffer(Pipeline pipeline, PipelineLayout pipelineLayout, DescriptorSet[] descriptorSets) 
            {
            }

            private DescriptorSetLayoutBinding CreateLayoutBinding(Interface @interface)
            {
                return new DescriptorSetLayoutBinding(@interface.BindingPoint, @interface.DescriptorType, 1);
            }

            internal IEnumerable<DescriptorPoolSize> GetDescriptorPoolSize()
            {
                var layoutBindings = GetInterfaces().Where(x => x.InterfaceType != InterfaceType.Unknown).Select(x => CreateLayoutBinding(x)).ToArray();
                var layoutGroups = layoutBindings.GroupBy(x => x.DescriptorType);
                return layoutGroups.Select(x => new DescriptorPoolSize(x.Key, x.Count()));
            }

            internal abstract IEnumerable<ShaderEntry> GetShaderEntries();

            internal IEnumerable<Interface> GetInterfaces() => GetShaderEntries().SelectMany(x => x.Shader.EntryPoints.SelectMany(x => x.Interfaces));

            internal IEnumerable<BufferDescriptor> GetBufferDescriptors() => GetInterfaces().Where(x => x.InterfaceType == InterfaceType.OpTypeStruct).Select(x => Subpass.App.Surface.GetBufferDescriptor(x));

            internal IEnumerable<ImageDescriptor> GetImageDescriptors() => GetInterfaces().Where(x => x.InterfaceType != InterfaceType.OpTypeStruct).Select(x => Subpass.App.Surface.GetImageDescriptor(x));

            protected T ToDispose<T>(T disposable)
            {
                return Subpass.App.ToDispose(disposable);
            }
        }

        class PipelineComputeShader                                             : PipelineShader 
        {
            private ComputeShaderReference mShader;

            public override PipelineShaderType ShaderType => PipelineShaderType.Compute;

            public PipelineComputeShader(PipelineSubpass buffer, ComputeShaderReference shader) 
                : base(buffer)
            {
                mShader = shader;
            }

            protected internal override void InitializePermanent(DescriptorPool descriptorPool)
            {
                base.InitializePermanent(descriptorPool);
            }

            protected override Pipeline CreatePipeline(PipelineLayout pipelineLayout)
            {
                var pipelineCreateInfo = new ComputePipelineCreateInfo(
                    new PipelineShaderStageCreateInfo(ShaderStages.Compute, Subpass.App.Surface.GetVulkanShader(mShader.Entry), mShader.Entry.EntryPoint.EntryPointName),
                    pipelineLayout);

                return Context.Device.CreateComputePipeline(pipelineCreateInfo);
            }

            protected override void RecordComputeCommandBuffer(Pipeline pipeline, PipelineLayout pipelineLayout, DescriptorSet[] descriptorSets)
            {
                // Record particle movements.

                VulkanBuffer[] inputBuffers = null;
                Image[] inputImages = null;
                ImageLayout inputLayout = ImageLayout.ColorAttachmentOptimal;

                var inputStages = PipelineStages.VertexInput;
                var inputAccess = Accesses.VertexAttributeRead;
                switch (PrevShader?.ShaderType ?? PipelineShaderType.None)
                {
                    case PipelineShaderType.None:
                        inputStages = PipelineStages.VertexInput;
                        inputAccess = Accesses.VertexAttributeRead;
                        inputBuffers = GetBufferDescriptors().Where(x => x.Usage == SurfaceDescriptorUsage.Output).Select(x => x.Buffer).ToArray();
                        inputImages = GetImageDescriptors().Where(x => x.Usage == SurfaceDescriptorUsage.Output).Select(x => x.Image).ToArray();
                        break;
                    case PipelineShaderType.Graphics:
                        inputStages = PipelineStages.ColorAttachmentOutput;
                        inputAccess = Accesses.ColorAttachmentRead;
                        inputImages = Subpass.App.SwapchainImages;
                        inputLayout = ImageLayout.PresentSrcKhr;
                        break;
                    case PipelineShaderType.Compute:
                        inputStages = PipelineStages.ComputeShader;
                        inputAccess = Accesses.ShaderRead;
                        inputBuffers = PrevShader?.GetBufferDescriptors().Where(x => x.Usage == SurfaceDescriptorUsage.Output).Select(x => x.Buffer).ToArray();
                        inputImages = PrevShader?.GetImageDescriptors().Where(x => x.Usage == SurfaceDescriptorUsage.Output).Select(x => x.Image).ToArray();
                        break;
                }

                var bufferMemoryBarriers = inputBuffers?.Select(x => new BufferMemoryBarrier(x,
                    inputAccess, Accesses.ShaderWrite,
                    Context.GraphicsQueue.FamilyIndex, Context.ComputeQueue.FamilyIndex)).ToArray();

                var subresourceRange = new ImageSubresourceRange(ImageAspects.Color, 0, 1, 0, 1);
                var imageMemoryBarriers = inputImages?.Select(x => new ImageMemoryBarrier(
                    x, subresourceRange,
                    inputAccess, Accesses.ShaderWrite,
                    inputLayout, ImageLayout.ShaderReadOnlyOptimal)).ToArray();

                Command.Begin();

                Command.CmdPipelineBarrier(inputStages, PipelineStages.ComputeShader,
                    bufferMemoryBarriers: bufferMemoryBarriers,
                    imageMemoryBarriers: imageMemoryBarriers);

                Command.CmdBindPipeline(PipelineBindPoint.Compute, pipeline);
                Command.CmdBindDescriptorSets(PipelineBindPoint.Compute, pipelineLayout, 0, descriptorSets);

                if (mShader.Entry.EntryPoint.Size.X == 0 || mShader.Entry.EntryPoint.Size.Y == 0 || mShader.Entry.EntryPoint.Size.Z == 0)
                    throw new NotSupportedException($"{Settings.Verstion} command buffer group count, some item is 0.");

                var layoutX = mShader.DispatchCount / mShader.Entry.EntryPoint.Size.X;
                if (layoutX == 0)
                    throw new NotSupportedException($"{Settings.Verstion} group layout x size is 0.");

                var layoutY = layoutX / mShader.Entry.EntryPoint.Size.Y;
                if (layoutY == 0)
                    throw new NotSupportedException($"{Settings.Verstion} group layout y size is 0.");

                var layoutZ = layoutY / mShader.Entry.EntryPoint.Size.Z;
                if (layoutZ == 0)
                    throw new NotSupportedException($"{Settings.Verstion} group layout z size is 0.");

                Command.CmdDispatch(layoutX, layoutY, layoutZ);

                if (NextShader != null)
                {
                    VulkanBuffer[] outputBuffers = null;

                    var outputStages = PipelineStages.VertexInput;
                    var outputAccess = Accesses.VertexAttributeRead;
                    switch (NextShader.ShaderType)
                    {
                        case PipelineShaderType.None:
                            throw new NotSupportedException("The next shader type cannot be None");
                        case PipelineShaderType.Graphics:
                            outputStages = PipelineStages.ColorAttachmentOutput;
                            outputAccess = Accesses.ColorAttachmentRead;
                            break;
                        case PipelineShaderType.Compute:
                            outputStages = PipelineStages.ComputeShader;
                            outputAccess = Accesses.ShaderRead;
                            outputBuffers = NextShader.GetBufferDescriptors().Where(x => x.Usage == SurfaceDescriptorUsage.Input).Select(x => x.Buffer).ToArray();
                            break;
                    }

                    var bufferMemoryBarriersNext = outputBuffers.Select(x => new BufferMemoryBarrier(x,
                        Accesses.ShaderWrite, outputAccess,
                        Context.ComputeQueue.FamilyIndex, Context.GraphicsQueue.FamilyIndex)).ToArray();

                    Command.CmdPipelineBarrier(PipelineStages.ComputeShader, outputStages,
                        bufferMemoryBarriers: bufferMemoryBarriersNext);
                }

                Command.End();
            }

            internal override IEnumerable<ShaderEntry> GetShaderEntries()
            {
                yield return mShader.Entry;
            }
        }

        class PipelineGraphicsShader                                            : PipelineShader
        {
            private GraphicsShaderReference mShader;
  
            private RenderPass mRenderPass;
            private ImageView[] mImageViews;
            private Framebuffer[] mFramebuffers;
            private VulkanImage mDepthStencil;

            public override PipelineShaderType ShaderType => PipelineShaderType.Graphics;

            public PipelineGraphicsShader(PipelineSubpass buffer, GraphicsShaderReference shader)
                : base(buffer)
            {
                mShader = shader;
            }

            protected internal override void InitializePermanent(DescriptorPool descriptorPool)
            {
                base.InitializePermanent(descriptorPool);
            }

            protected internal override void InitializeFrame()
            {
                base.InitializeFrame();

                mDepthStencil = ToDispose(VulkanImage.DepthStencil(Context, Subpass.App.Host.Size.Width, Subpass.App.Host.Size.Height));
                mRenderPass = ToDispose(CreateRenderPass());
                mImageViews = ToDispose(CreateImageViews());
                mFramebuffers = ToDispose(CreateFramebuffers());
            }

            protected override Pipeline CreatePipeline(PipelineLayout pipelineLayout)
            {
                // 已binding和类型大小来区分使用数据偏移
                var attributeDescrptions = new List<VertexInputAttributeDescription>();
                var inGroups = GetInterfaces().Where(x => x.InterfaceClass == InterfaceClass.Input).GroupBy(x => x.DescriptorSet);
                foreach (var group in inGroups)
                {
                    var offset = 0;
                    foreach (var face in group.OrderBy(x => x.Location)) 
                    {
                        attributeDescrptions.Add(new VertexInputAttributeDescription(
                            face.Location,
                            face.DescriptorSet,
                            face.ShaderType.GetFormat(),
                            offset));

                        offset = offset + (int)face.ShaderType.Size;
                    }
                }

                var inputAssemblyState = new PipelineInputAssemblyStateCreateInfo(PrimitiveTopology.PointList);
                var vertexInputState = new PipelineVertexInputStateCreateInfo(
                    new[] { new VertexInputBindingDescription(0, 0, VertexInputRate.Vertex) },
                    attributeDescrptions.ToArray());

                var rasterizationState = new PipelineRasterizationStateCreateInfo
                {
                    PolygonMode = PolygonMode.Fill,
                    CullMode = CullModes.None,
                    FrontFace = FrontFace.CounterClockwise,
                    LineWidth = 1.0f
                };
                // Additive blending.
                var blendAttachmentState = new PipelineColorBlendAttachmentState
                {
                    BlendEnable = true,
                    ColorWriteMask = ColorComponents.All,
                    ColorBlendOp = BlendOp.Add,
                    SrcColorBlendFactor = BlendFactor.One,
                    DstColorBlendFactor = BlendFactor.One,
                    AlphaBlendOp = BlendOp.Add,
                    SrcAlphaBlendFactor = BlendFactor.SrcAlpha,
                    DstAlphaBlendFactor = BlendFactor.DstAlpha
                };
                var colorBlendState = new PipelineColorBlendStateCreateInfo(new[] { blendAttachmentState });
                var depthStencilState = new PipelineDepthStencilStateCreateInfo();
                var viewportState = new PipelineViewportStateCreateInfo(
                    new Viewport(0, 0, Subpass.App.Host.Size.Width, Subpass.App.Host.Size.Height),
                    new Rect2D(0, 0, Subpass.App.Host.Size.Width, Subpass.App.Host.Size.Height));
                var multisampleState = new PipelineMultisampleStateCreateInfo { RasterizationSamples = SampleCounts.Count1 };

                var pipelineShaderStages = new[]
                {
                    new PipelineShaderStageCreateInfo(ShaderStages.Vertex, Subpass.App.Surface.GetVulkanShader(mShader.VertexShader), mShader.VertexShader.EntryPoint.EntryPointName),
                    new PipelineShaderStageCreateInfo(ShaderStages.Fragment, Subpass.App.Surface.GetVulkanShader(mShader.FragmentShader), mShader.VertexShader.EntryPoint.EntryPointName),
                };

                var pipelineCreateInfo = new GraphicsPipelineCreateInfo(pipelineLayout, mRenderPass, 0,
                    pipelineShaderStages,
                    inputAssemblyState,
                    vertexInputState,
                    rasterizationState,
                    viewportState: viewportState,
                    multisampleState: multisampleState,
                    depthStencilState: depthStencilState,
                    colorBlendState: colorBlendState);

                return Context.Device.CreateGraphicsPipeline(pipelineCreateInfo);
            }

            private RenderPass CreateRenderPass()
            {
                var attachments = new[]
                {
                    // Color attachment.
                    new AttachmentDescription
                    {
                        Format = Subpass.App.Swapchain.Format,
                        Samples = SampleCounts.Count1,
                        LoadOp = AttachmentLoadOp.Clear,
                        StoreOp = AttachmentStoreOp.Store,
                        StencilLoadOp = AttachmentLoadOp.DontCare,
                        StencilStoreOp = AttachmentStoreOp.DontCare,
                        InitialLayout = ImageLayout.Undefined,
                        FinalLayout = ImageLayout.PresentSrcKhr
                    },
                    // Depth attachment.
                    new AttachmentDescription
                    {
                        Format = mDepthStencil.Format,
                        Samples = SampleCounts.Count1,
                        LoadOp = AttachmentLoadOp.Clear,
                        StoreOp = AttachmentStoreOp.DontCare,
                        StencilLoadOp = AttachmentLoadOp.DontCare,
                        StencilStoreOp = AttachmentStoreOp.DontCare,
                        InitialLayout = ImageLayout.Undefined,
                        FinalLayout = ImageLayout.DepthStencilAttachmentOptimal
                    }
                };
                var subpasses = new[]
                {
                    new SubpassDescription(
                        new[] { new AttachmentReference(0, ImageLayout.ColorAttachmentOptimal) },
                        new AttachmentReference(1, ImageLayout.DepthStencilAttachmentOptimal))
                };
                var dependencies = new[]
                {
                    new SubpassDependency
                    {
                        SrcSubpass = Constant.SubpassExternal,
                        DstSubpass = 0,
                        SrcStageMask = PipelineStages.BottomOfPipe,
                        DstStageMask = PipelineStages.ColorAttachmentOutput,
                        SrcAccessMask = Accesses.MemoryRead,
                        DstAccessMask = Accesses.ColorAttachmentRead | Accesses.ColorAttachmentWrite,
                        DependencyFlags = Dependencies.ByRegion
                    },
                    new SubpassDependency
                    {
                        SrcSubpass = 0,
                        DstSubpass = Constant.SubpassExternal,
                        SrcStageMask = PipelineStages.ColorAttachmentOutput,
                        DstStageMask = PipelineStages.BottomOfPipe,
                        SrcAccessMask = Accesses.ColorAttachmentRead | Accesses.ColorAttachmentWrite,
                        DstAccessMask = Accesses.MemoryRead,
                        DependencyFlags = Dependencies.ByRegion
                    }
                };

                var createInfo = new RenderPassCreateInfo(subpasses, attachments, dependencies);
                return Context.Device.CreateRenderPass(createInfo);
            }

            private ImageView[] CreateImageViews()
            {
                var imageViews = new ImageView[Subpass.App.SwapchainImages.Length];
                for (int i = 0; i < Subpass.App.SwapchainImages.Length; i++)
                {
                    imageViews[i] = Subpass.App.SwapchainImages[i].CreateView(new ImageViewCreateInfo(
                        Subpass.App.Swapchain.Format,
                        new ImageSubresourceRange(ImageAspects.Color, 0, 1, 0, 1)));
                }
                return imageViews;
            }

            private Framebuffer[] CreateFramebuffers()
            {
                var framebuffers = new Framebuffer[Subpass.App.SwapchainImages.Length];
                for (int i = 0; i < Subpass.App.SwapchainImages.Length; i++)
                {
                    framebuffers[i] = mRenderPass.CreateFramebuffer(new FramebufferCreateInfo(
                        new[] { mImageViews[i], mDepthStencil.View },
                        Subpass.App.Host.Size.Width,
                        Subpass.App.Host.Size.Height));
                }
                return framebuffers;
            }

            protected override void RecordGraphicsCommandBuffer(Pipeline pipeline, PipelineLayout pipelineLayout, DescriptorSet[] descriptorSets, CommandBuffer cmdBuffer, int imageIndex)
            {
                Command.CmdBeginRenderPass(new RenderPassBeginInfo(
                    mFramebuffers[imageIndex],
                    new Rect2D(0, 0, Subpass.App.Host.Size.Width, Subpass.App.Host.Size.Height),
                    new ClearColorValue(new ColorF4(0, 0, 0, 0)),
                    new ClearDepthStencilValue(1.0f, 0)));

                // 获得vertex buffer 和 index buffer
                VulkanBuffer indexBuffer = null;
                VulkanBuffer vertexBuffer = null;

                Command.CmdBindPipeline(PipelineBindPoint.Graphics, pipeline);
                Command.CmdBindDescriptorSets(PipelineBindPoint.Graphics, pipelineLayout, 0, descriptorSets);
                Command.CmdBindVertexBuffer(vertexBuffer);

                if (indexBuffer != null)
                {
                    Command.CmdBindIndexBuffer(indexBuffer, 0, vertexBuffer.Count <= UInt16.MaxValue ? IndexType.UInt16 : IndexType.UInt32);
                    Command.CmdDrawIndexed(indexBuffer.Count);
                }
                else 
                {
                    Command.CmdDraw(vertexBuffer.Count);
                }
       
                Command.CmdEndRenderPass();
            }

            internal override IEnumerable<ShaderEntry> GetShaderEntries()
            {
                yield return mShader.VertexShader;
                yield return mShader.FragmentShader;
            }
        }

        class PipelineSubpass                                                   
        {
            private PipelineShader[] mShaders;

            public PipelineApp App { get; }

            public VulkanContext Context => App.Context;

            public PipelineShader[] Shaders => mShaders;

            public PipelineSubpass(PipelineApp app, PipelineShader[] shaders)
            {
                App = app;
                mShaders = shaders;
            }

            public void InitializeFrame()
            {
                foreach (var shader in mShaders)
                    shader.InitializeFrame();
            }

            internal void InitializePermanent(DescriptorPool descriptorPool)
            {
                foreach (var shader in mShaders)
                    shader.InitializePermanent(descriptorPool);
            }

            internal void RecordComputeCommandBuffer()
            {
                for (var i = 0; i < mShaders.Length; i++)
                    mShaders[i].RecordComputeCommands();
            }        

            internal void RecordCommandBuffer(CommandBuffer cmdBuffer, int imageIndex)
            {
                for (var i = 0; i < mShaders.Length; i++)
                    mShaders[i].RecordGraphicsCommands(cmdBuffer, imageIndex);
            }

            internal IEnumerable<CommandBuffer> GetCommands()
            {
                for (var i = 0; i < mShaders.Length; i++)
                    yield return mShaders[i].Command;
            }

            internal IEnumerable<DescriptorPoolSize> GetDescriptorPoolSize()
            {
                return mShaders.SelectMany(x => x.GetDescriptorPoolSize()).GroupBy(x => x.Type).Select(x => new DescriptorPoolSize(x.Key, x.Sum(y => y.DescriptorCount)));
            } 
        }

        class PipelineApp                                                       : VulkanApp
        {
            private Surface mSurface;
            private Fence mComputeFence;
            private PipelineSubpass[] mPipelineSubpass;
            private CommandBuffer[] mPipelineCommands;

            public new Surface Surface => mSurface;

            public void Initialize(IAppHost host, Surface surface)
            {
                mSurface = surface;
    
                List<PipelineSubpass> buffers = new List<PipelineSubpass>();

                // 分割到render段             
                var shaders = new List<PipelineShader>();
                for (var i = 0; i < surface.Pipelines.Length; i++)
                {
                    var pipeline = surface.Pipelines[i];
                    if (pipeline is GraphicsShaderReference gr)
                    {
                        shaders.Add(new PipelineGraphicsShader(buffers[^1], gr));
                        buffers.Add(new PipelineSubpass(this, shaders.ToArray()));
                        shaders.Clear();
                    }
                    else if (pipeline is ComputeShaderReference cr)
                    {
                        shaders.Add(new PipelineComputeShader(buffers[^1], cr));
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }

                if (shaders.Count > 0)
                    buffers.Add(new PipelineSubpass(this, shaders.ToArray()));

                // 整理shader关系层
                var allShaders = buffers.SelectMany(x => x.Shaders).ToArray();
                for (var i = 0; i < allShaders.Length; i++) 
                {
                    var prev = i == 0 ? null : allShaders[i - 1];
                    var curr = allShaders[i];
                    var next = i == allShaders.Length - 1 ? null : allShaders[i + 1];

                    curr.BindingLevel(prev, next);
                }

                // 设置全部buffer
                mPipelineSubpass = buffers.ToArray();

                base.Initialize(host);
            }

            protected override void InitializePermanent()
            {
                mPipelineSubpass.Do(x => x.InitializePermanent(CreateDescriptorPool(x)));
                mComputeFence = ToDispose(Context.Device.CreateFence());
                mPipelineCommands = mPipelineSubpass.SelectMany(x => x.GetCommands()).ToArray();
            }

            protected override void InitializeFrame()
            {
                mPipelineSubpass.Do(x => InitializeFrame());
                RecordComputeCommandBuffer();
            }

            protected override void Update(Timer timer)
            {
            }

            protected override void Draw(Timer timer)
            {
                // Submit compute commands.
                Context.ComputeQueue.Submit(new SubmitInfo(commandBuffers: mPipelineCommands), mComputeFence);
                mComputeFence.Wait();
                mComputeFence.Reset();

                // Submit graphics commands.
                base.Draw(timer);
            }

            protected override void RecordCommandBuffer(CommandBuffer cmdBuffer, int imageIndex)
            {
                mPipelineSubpass.Do(x => x.RecordCommandBuffer(cmdBuffer, imageIndex));
            }

            private void RecordComputeCommandBuffer()
            {
                mPipelineSubpass.Do(x => x.RecordComputeCommandBuffer());
            }

            private DescriptorPool CreateDescriptorPool(PipelineSubpass buffer)
            {
                var pool = buffer.GetDescriptorPoolSize().ToArray();
                return Context.Device.CreateDescriptorPool(new DescriptorPoolCreateInfo(pool.Length, pool));
            }
        }
    }
}
