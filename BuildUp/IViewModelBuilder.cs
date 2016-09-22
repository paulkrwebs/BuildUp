namespace BuildUp
{
    using System.Threading.Tasks;

    public interface IViewModelBuilder
    {
        TToCreate Build<TToCreate>()
            where TToCreate : new();

        Task<TToCreate> BuildAsync<TToCreate>()
            where TToCreate : new();

        TToCreate Build<TFrom, TToCreate>(TFrom @from)
            where TToCreate : new();

        TToCreate Build<TData, TFrom, TToCreate>(TData data, TFrom @from)
            where TToCreate : new();

        Task<TToCreate> BuildAsync<TFrom, TToCreate>(TFrom @from)
            where TToCreate : new();

        Task<TToCreate> BuildAsync<TData, TFrom, TToCreate>(TData data, TFrom @from)
            where TToCreate : new();
    }
}