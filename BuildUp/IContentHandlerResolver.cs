namespace BuildUp
{
    using System.Collections.Generic;

    public interface IContentHandlerResolver
    {
        IEnumerable<THandles> ResolverAll<THandles>() where THandles : IContentHandler;
    }
}