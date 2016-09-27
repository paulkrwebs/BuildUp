namespace BuildUp
{
    using System.Threading.Tasks;

    public interface IContentHandlerPipeline
    {
        bool Raise<TContentHandlerArgs>(TContentHandlerArgs args)
            where TContentHandlerArgs : ContentHandlerArgs;

        Task<bool> RaiseAsync<TContentHandlerArgs>(TContentHandlerArgs args)
            where TContentHandlerArgs : ContentHandlerArgs;
    }
}