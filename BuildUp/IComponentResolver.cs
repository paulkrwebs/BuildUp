namespace BuildUp
{
    using System.Collections.Generic;

    public interface IComponentResolver
    {
        IEnumerable<TComponent> ResolverAll<TComponent>() where TComponent : IComponent;
    }
}