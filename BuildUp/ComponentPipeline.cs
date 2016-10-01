using System;
using System.Threading.Tasks;

namespace BuildUp
{
    public class ComponentPipeline : IComponentPipeline
    {
        private readonly IComponentResolver _componentResolver;

        public ComponentPipeline(IComponentResolver componentResolver)
        {
            _componentResolver = componentResolver;
        }

        public bool Raise<TComponentArgs>(TComponentArgs args)
            where TComponentArgs : ComponentArgs
        {
            var components = _componentResolver.ResolverAll<IComponent<TComponentArgs>>();
            bool raised = false;

            foreach (var component in components)
            {
                component.Handle(args);
                raised = true;
            }

            return raised;
        }

        public async Task<bool> RaiseAsync<TComponentArgs>(TComponentArgs args)
            where TComponentArgs : ComponentArgs
        {
            var components = _componentResolver.ResolverAll<IComponentAsync<TComponentArgs>>();
            bool raised = false;

            foreach (var component in components)
            {
                await component.HandleAsync(args);
                raised = true;
            }

            return raised;
        }
    }
}