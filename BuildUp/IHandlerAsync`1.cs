namespace BuildUp
{
    using System.Threading.Tasks;

    public interface IHandlerAsync<TArgs1> : IHandler
    {
        Task HandleAsync(TArgs1 arg1);
    }
}