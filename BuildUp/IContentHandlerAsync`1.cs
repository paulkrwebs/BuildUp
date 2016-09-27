namespace BuildUp
{
    using System.Threading.Tasks;

    public interface IContentHandlerAsync<TArgs1> : IContentHandler
    {
        Task HandleAsync(TArgs1 arg1);
    }
}