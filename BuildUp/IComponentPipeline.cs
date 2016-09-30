namespace BuildUp
{
    using System.Threading.Tasks;

    public interface IComponentPipeline
    {
        bool Raise<TComponentArgs>(TComponentArgs args)
            where TComponentArgs : ComponentArgs;

        Task<bool> RaiseAsync<TComponentArgs>(TComponentArgs args)
            where TComponentArgs : ComponentArgs;
    }
}