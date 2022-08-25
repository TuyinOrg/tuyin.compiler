using Chromely.Core;
using Chromely.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuyin.Sutdio
{
    internal class Report : IChromelyErrorHandler
    {
        public IChromelyResource HandleError(FileInfo fileInfo, Exception exception = null)
        {
            throw new NotImplementedException();
        }

        public IChromelyResource HandleError(Stream stream, Exception exception = null)
        {
            throw new NotImplementedException();
        }

        public IChromelyResponse HandleError(IChromelyRequest request, Exception exception = null)
        {
            throw new NotImplementedException();
        }

        public IChromelyResponse HandleError(IChromelyRequest request, IChromelyResponse response, Exception exception = null)
        {
            throw new NotImplementedException();
        }

        public Task<IChromelyResource> HandleErrorAsync(string requestUrl, IChromelyResource response, Exception exception = null)
        {
            throw new NotImplementedException();
        }

        public IChromelyResponse HandleRouteNotFound(string requestId, string routePath)
        {
            throw new NotImplementedException();
        }
    }
}
