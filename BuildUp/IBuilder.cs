namespace BuildUp
{
    using System.Threading.Tasks;

    public interface IBuilder
    {
        TToCreate Build<TToCreate>()
            where TToCreate : new();

        Task<TToCreate> BuildAsync<TToCreate>()
            where TToCreate : new();

        TToCreate Build<TToCreate, TFrom>(TFrom @from)
            where TToCreate : new();

        TToCreate Build<TToCreate, TFrom, TData>(TFrom @from, TData data)
            where TToCreate : new();

        Task<TToCreate> BuildAsync<TToCreate, TFrom>(TFrom @from)
            where TToCreate : new();

        Task<TToCreate> BuildAsync<TToCreate, TFrom, TData>(TFrom @from, TData data)
            where TToCreate : new();
    }
}