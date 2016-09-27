using System;
using System.Threading.Tasks;

namespace BuildUp
{
    public class ContentHandlerPipeline : IContentHandlerPipeline
    {
        private readonly IContentHandlerResolver _handlerResolver;

        public ContentHandlerPipeline(IContentHandlerResolver handlerResolver)
        {
            _handlerResolver = handlerResolver;
        }

        public bool Raise<TContentHandlerArgs>(TContentHandlerArgs args)
            where TContentHandlerArgs : ContentHandlerArgs
        {
            var handlers = _handlerResolver.ResolverAll<IContentHandler<TContentHandlerArgs>>();
            bool raised = false;

            foreach (var handler in handlers)
            {
                handler.Handle(args);
                raised = true;
            }

            return raised;
        }

        public async Task<bool> RaiseAsync<TContentHandlerArgs>(TContentHandlerArgs args)
            where TContentHandlerArgs : ContentHandlerArgs
        {
            var handlers = _handlerResolver.ResolverAll<IContentHandlerAsync<TContentHandlerArgs>>();
            bool raised = false;

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(args);
                raised = true;
            }

            return raised;
        }
    }
}