namespace BuildUp
{
    public interface IComponent<TArgs1> : IComponent
    {
        void Handle(TArgs1 arg1);
    }
}