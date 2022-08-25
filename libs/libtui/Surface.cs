using libtui.content;
using libtui.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using Vulkan;

namespace libtui
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WorldViewProjection
    {
        public Matrix4x4 World;
        public Matrix4x4 View;
        public Matrix4x4 Projection;
    }

    class Surface : VulkanApp
    {
        private RenderPass _renderPass;
        private ImageView[] _imageViews;
        private Framebuffer[] _framebuffers;

        private PipelineLayout _pipelineLayout;
        private Pipeline _pipeline;
        private DescriptorSetLayout _descriptorSetLayout;
        private DescriptorPool _descriptorPool;
        private DescriptorSet _descriptorSet;

        private VulkanImage _depthStencilBuffer;

        private Sampler _sampler;
        private VulkanImage _cubeTexture;

        private VulkanBuffer _vertices;
        private VulkanBuffer _indices;

        private VulkanBuffer _uniformBuffer;
        private WorldViewProjection _wvp;
        private List<Vertex> _verticesList = new List<Vertex>();

        protected override void InitializePermanent()
        {
            var cube = GeometricPrimitive.Box(1.0f, 1.0f, 1.0f);

            _cubeTexture = Content.Load<VulkanImage>("resources/IndustryForgedDark512.ktx");
            _vertices = ToDispose(VulkanBuffer.Vertex(Context, cube.Vertices));
            _indices = ToDispose(VulkanBuffer.Index(Context, cube.Indices));
            _sampler = ToDispose(CreateSampler());
            _uniformBuffer = ToDispose(VulkanBuffer.DynamicUniform<WorldViewProjection>(Context, 1));
            _descriptorSetLayout = ToDispose(CreateDescriptorSetLayout());
            _pipelineLayout = ToDispose(CreatePipelineLayout());
            _descriptorPool = ToDispose(CreateDescriptorPool());
            _descriptorSet = CreateDescriptorSet(); // Will be freed when pool is destroyed.
        }

        protected override void InitializeFrame()
        {
            _depthStencilBuffer = ToDispose(VulkanImage.DepthStencil(Context, Window.Width, Window.Height));
            _renderPass = ToDispose(CreateRenderPass());
            _imageViews = ToDispose(CreateImageViews());
            _framebuffers = ToDispose(CreateFramebuffers());
            _pipeline = ToDispose(CreateGraphicsPipeline());
            SetViewProjection();
        }

        protected override void Update(Timer timer)
        {
            /*
            const float twoPi = (float)Math.PI * 2.0f;
            const float yawSpeed = twoPi / 4.0f;
            const float pitchSpeed = 0.0f;
            const float rollSpeed = twoPi / 4.0f;

            _wvp.World = Matrix4x4.CreateFromYawPitchRoll(
                timer.TotalTime * yawSpeed % twoPi,
                timer.TotalTime * pitchSpeed % twoPi,
                timer.TotalTime * rollSpeed % twoPi);

            UpdateUniformBuffers();
            */
        }

        protected override void RecordCommandBuffer(CommandBuffer cmdBuffer, int imageIndex)
        {
            var renderPassBeginInfo = new RenderPassBeginInfo(
                _framebuffers[imageIndex],
                new Rect2D(0, 0, Window.Width, Window.Height),
                new ClearColorValue(new ColorF4(0.39f, 0.58f, 0.93f, 1.0f)),
                new ClearDepthStencilValue(1.0f, 0));

            cmdBuffer.CmdBeginRenderPass(renderPassBeginInfo);
            cmdBuffer.CmdBindDescriptorSet(PipelineBindPoint.Graphics, _pipelineLayout, _descriptorSet);
            cmdBuffer.CmdBindPipeline(PipelineBindPoint.Graphics, _pipeline);
            cmdBuffer.CmdBindVertexBuffer(_vertices);
            cmdBuffer.CmdBindIndexBuffer(_indices);
            cmdBuffer.CmdDrawIndexed(_indices.Count);
            cmdBuffer.CmdEndRenderPass();
        }

        private Sampler CreateSampler()
        {
            var createInfo = new SamplerCreateInfo
            {
                MagFilter = Filter.Linear,
                MinFilter = Filter.Linear,
                MipmapMode = SamplerMipmapMode.Linear
            };
            // We also enable anisotropic filtering. Because that feature is optional, it must be
            // checked if it is supported by the device.
            if (Context.Features.SamplerAnisotropy)
            {
                createInfo.AnisotropyEnable = true;
                createInfo.MaxAnisotropy = Context.Properties.Limits.MaxSamplerAnisotropy;
            }
            else
            {
                createInfo.MaxAnisotropy = 1.0f;
            }
            return Context.Device.CreateSampler(createInfo);
        }

        private void SetViewProjection()
        {
            _wvp.View = Matrix4x4.CreateLookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
            _wvp.Projection = Matrix4x4.CreateOrthographic(Window.Width, Window.Height, 0, 1);
        }

        internal void UpdateUniformBuffers()
        {
            IntPtr ptr = _uniformBuffer.Memory.Map(0, Interop.SizeOf<WorldViewProjection>());
            Interop.Write(ptr, ref _wvp);
            _uniformBuffer.Memory.Unmap();
        }

        internal void UpdateBuffers(Vertex[] vertices) 
        {
            _verticesList.AddRange(vertices);
        }

        internal void Flush() 
        {
            var indices = Enumerable.Range(0, _verticesList.Count).ToArray();
            _vertices = ToDispose(VulkanBuffer.Vertex(Context, _verticesList.ToArray()));
            _indices = ToDispose(VulkanBuffer.Index(Context, indices));
            RecordCommandBuffers();
            _verticesList.Clear();
        }

        private DescriptorPool CreateDescriptorPool()
        {
            var descriptorPoolSizes = new[]
            {
                new DescriptorPoolSize(DescriptorType.UniformBuffer, 1),
                new DescriptorPoolSize(DescriptorType.CombinedImageSampler, 1)
            };
            return Context.Device.CreateDescriptorPool(
                new DescriptorPoolCreateInfo(descriptorPoolSizes.Length, descriptorPoolSizes));
        }

        private DescriptorSet CreateDescriptorSet()
        {
            DescriptorSet descriptorSet = _descriptorPool.AllocateSets(new DescriptorSetAllocateInfo(1, _descriptorSetLayout))[0];
            // Update the descriptor set for the shader binding point.
            var writeDescriptorSets = new[]
            {
                new WriteDescriptorSet(descriptorSet, 0, 0, 1, DescriptorType.UniformBuffer,
                    bufferInfo: new[] { new DescriptorBufferInfo(_uniformBuffer) }),
                new WriteDescriptorSet(descriptorSet, 1, 0, 1, DescriptorType.CombinedImageSampler,
                    imageInfo: new[] { new DescriptorImageInfo(_sampler, _cubeTexture.View, ImageLayout.General) })
            };
            _descriptorPool.UpdateSets(writeDescriptorSets);
            return descriptorSet;
        }

        private DescriptorSetLayout CreateDescriptorSetLayout()
        {
            return Context.Device.CreateDescriptorSetLayout(new DescriptorSetLayoutCreateInfo(
                new DescriptorSetLayoutBinding(0, DescriptorType.UniformBuffer, 1, ShaderStages.Vertex),
                new DescriptorSetLayoutBinding(1, DescriptorType.CombinedImageSampler, 1, ShaderStages.Fragment)));
        }

        private PipelineLayout CreatePipelineLayout()
        {
            return Context.Device.CreatePipelineLayout(new PipelineLayoutCreateInfo(
                new[] { _descriptorSetLayout }));
        }

        private RenderPass CreateRenderPass()
        {
            var attachments = new[]
            {
                // Color attachment.
                new AttachmentDescription
                {
                    Format = Swapchain.Format,
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
                    Format = _depthStencilBuffer.Format,
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
            var imageViews = new ImageView[SwapchainImages.Length];
            for (int i = 0; i < SwapchainImages.Length; i++)
            {
                imageViews[i] = SwapchainImages[i].CreateView(new ImageViewCreateInfo(
                    Swapchain.Format,
                    new ImageSubresourceRange(ImageAspects.Color, 0, 1, 0, 1)));
            }
            return imageViews;
        }

        private Framebuffer[] CreateFramebuffers()
        {
            var framebuffers = new Framebuffer[SwapchainImages.Length];
            for (int i = 0; i < SwapchainImages.Length; i++)
            {
                framebuffers[i] = _renderPass.CreateFramebuffer(new FramebufferCreateInfo(
                    new[] { _imageViews[i], _depthStencilBuffer.View },
                    Window.Width,
                    Window.Height));
            }
            return framebuffers;
        }

        private Pipeline CreateGraphicsPipeline()
        {
            // Create shader modules. Shader modules are one of the objects required to create the
            // graphics pipeline. But after the pipeline is created, we don't need these shader
            // modules anymore, so we dispose them.
            ShaderModule vertexShader = Content.Load<ShaderModule>("resources/Shader.vert.spv");
            ShaderModule fragmentShader = Content.Load<ShaderModule>("resources/Shader.frag.spv");
            var shaderStageCreateInfos = new[]
            {
                new PipelineShaderStageCreateInfo(ShaderStages.Vertex, vertexShader, "main"),
                new PipelineShaderStageCreateInfo(ShaderStages.Fragment, fragmentShader, "main")
            };

            var vertexInputStateCreateInfo = new PipelineVertexInputStateCreateInfo(
                new[] { new VertexInputBindingDescription(0, Interop.SizeOf<Vertex>(), VertexInputRate.Vertex) },
                new[]
                {
                    new VertexInputAttributeDescription(0, 0, Format.R32G32B32SFloat, 0),  // Position.
                    new VertexInputAttributeDescription(1, 0, Format.R32G32B32SFloat, 12), // Normal.
                    new VertexInputAttributeDescription(2, 0, Format.R32G32SFloat, 24)     // TexCoord.
                }
            );
            var inputAssemblyStateCreateInfo = new PipelineInputAssemblyStateCreateInfo(PrimitiveTopology.TriangleList);
            var viewportStateCreateInfo = new PipelineViewportStateCreateInfo(
                new Viewport(0, 0, Window.Width, Window.Height),
                new Rect2D(0, 0, Window.Width, Window.Height));
            var rasterizationStateCreateInfo = new PipelineRasterizationStateCreateInfo
            {
                PolygonMode = PolygonMode.Fill,
                CullMode = CullModes.Back,
                FrontFace = FrontFace.CounterClockwise,
                LineWidth = 1.0f
            };
            var multisampleStateCreateInfo = new PipelineMultisampleStateCreateInfo
            {
                RasterizationSamples = SampleCounts.Count1,
                MinSampleShading = 1.0f
            };
            var depthStencilCreateInfo = new PipelineDepthStencilStateCreateInfo
            {
                DepthTestEnable = true,
                DepthWriteEnable = true,
                DepthCompareOp = CompareOp.LessOrEqual,
                Back = new StencilOpState
                {
                    FailOp = StencilOp.Keep,
                    PassOp = StencilOp.Keep,
                    CompareOp = CompareOp.Always
                },
                Front = new StencilOpState
                {
                    FailOp = StencilOp.Keep,
                    PassOp = StencilOp.Keep,
                    CompareOp = CompareOp.Always
                }
            };
            var colorBlendAttachmentState = new PipelineColorBlendAttachmentState
            {
                SrcColorBlendFactor = BlendFactor.One,
                DstColorBlendFactor = BlendFactor.Zero,
                ColorBlendOp = BlendOp.Add,
                SrcAlphaBlendFactor = BlendFactor.One,
                DstAlphaBlendFactor = BlendFactor.Zero,
                AlphaBlendOp = BlendOp.Add,
                ColorWriteMask = ColorComponents.All
            };
            var colorBlendStateCreateInfo = new PipelineColorBlendStateCreateInfo(
                new[] { colorBlendAttachmentState });

            var pipelineCreateInfo = new GraphicsPipelineCreateInfo(
                _pipelineLayout, _renderPass, 0,
                shaderStageCreateInfos,
                inputAssemblyStateCreateInfo,
                vertexInputStateCreateInfo,
                rasterizationStateCreateInfo,
                viewportState: viewportStateCreateInfo,
                multisampleState: multisampleStateCreateInfo,
                depthStencilState: depthStencilCreateInfo,
                colorBlendState: colorBlendStateCreateInfo);
            return Context.Device.CreateGraphicsPipeline(pipelineCreateInfo);
        }
    }
}
