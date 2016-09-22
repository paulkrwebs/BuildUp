namespace ViewModelBuilding
{
    using System.Threading.Tasks;

    public interface IContentHandlerPipeline
    {
        bool Raise<THandlerArgs>(THandlerArgs args)
            where THandlerArgs : HandlerArgs;

        Task<bool> RaiseAsync<THandlerArgs>(THandlerArgs args)
            where THandlerArgs : HandlerArgs;
    }
}