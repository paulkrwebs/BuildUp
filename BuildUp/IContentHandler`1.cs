namespace BuildUp
{
    public interface IContentHandler<TArgs1> : IContentHandler
    {
        void Handle(TArgs1 arg1);
    }
}