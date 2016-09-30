namespace BuildUp
{
    using System.Threading.Tasks;

    public interface IComponentAsync<TArgs1> : IComponent
    {
        Task HandleAsync(TArgs1 arg1);
    }
}