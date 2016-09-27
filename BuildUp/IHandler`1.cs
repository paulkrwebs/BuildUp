namespace BuildUp
{
    public interface IHandler<TArgs1> : IHandler
    {
        void Handle(TArgs1 arg1);
    }
}