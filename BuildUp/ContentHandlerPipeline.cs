using System;
using System.Threading.Tasks;

namespace BuildUp
{
    public class ContentHandlerPipeline : IContentHandlerPipeline
    {
        private readonly IHandlerResolver _handlerResolver;

        public ContentHandlerPipeline(IHandlerResolver handlerResolver)
        {
            _handlerResolver = handlerResolver;
        }

        public bool Raise<THandlerArgs>(THandlerArgs args)
            where THandlerArgs : HandlerArgs
        {
            var handlers = _handlerResolver.ResolverAll<IHandler<THandlerArgs>>();
            bool raised = false;

            foreach (var handler in handlers)
            {
                handler.Handle(args);
                raised = true;
            }

            return raised;
        }

        public async Task<bool> RaiseAsync<THandlerArgs>(THandlerArgs args) where THandlerArgs : HandlerArgs
        {
            var handlers = _handlerResolver.ResolverAll<IHandlerAsync<THandlerArgs>>();
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