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

        TToCreate Build<TToCreate, TFrom, TFrom2>(TFrom @from, TFrom2 from2)
            where TToCreate : new();

        Task<TToCreate> BuildAsync<TToCreate, TFrom>(TFrom @from)
            where TToCreate : new();

        Task<TToCreate> BuildAsync<TToCreate, TFrom, TFrom2>(TFrom @from, TFrom2 from2)
            where TToCreate : new();
    }
}