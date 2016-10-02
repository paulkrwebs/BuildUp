namespace BuildUp
{
    using System.Threading.Tasks;

    public interface IBuilder
    {
        TToBuild Build<TToBuild>()
            where TToBuild : new();

        Task<TToBuild> BuildAsync<TToBuild>()
            where TToBuild : new();

        TToBuild Build<TToBuild, TFrom>(TFrom @from)
            where TToBuild : new();

        TToBuild Build<TToBuild, TFrom, TFrom2>(TFrom @from, TFrom2 from2)
            where TToBuild : new();

        Task<TToBuild> BuildAsync<TToBuild, TFrom>(TFrom @from)
            where TToBuild : new();

        Task<TToBuild> BuildAsync<TToBuild, TFrom, TFrom2>(TFrom @from, TFrom2 from2)
            where TToBuild : new();
    }
}