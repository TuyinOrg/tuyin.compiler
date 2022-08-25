using libtui.content;
using System;
using System.Collections.Generic;
using System.IO;
using Vulkan;
using static libtui.content.Loader;

namespace libtui
{
    class ContentManager : IDisposable
    {
        private readonly Window _window;
        private readonly VulkanContext _ctx;
        private readonly Dictionary<string, IDisposable> _cachedContent = new Dictionary<string, IDisposable>();

        public ContentManager(Window window, VulkanContext ctx)
        {
            _window = window;
            _ctx = ctx;
        }

        public T Load<T>(string path)
        {
            if (_cachedContent.TryGetValue(path, out IDisposable value))
                return (T)value;

            string extension = Path.GetExtension(path);

            Type type = typeof(T);
            if (type == typeof(ShaderModule))
                value = LoadShaderModule(_window, _ctx, path);
            else if (type == typeof(VulkanImage))
                if (extension.Equals(".ktx", StringComparison.OrdinalIgnoreCase))
                    value = LoadKtxVulkanImage(_window, _ctx, path);

            if (value == null)
                throw new NotImplementedException("Content type or extension not implemented.");

            _cachedContent.Add(path, value);
            return (T)value;
        }

        public void Dispose()
        {
            foreach (IDisposable value in _cachedContent.Values)
                value.Dispose();
            _cachedContent.Clear();
        }
    }
}
