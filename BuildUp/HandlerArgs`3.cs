namespace BuildUp
{
    public class ContentHandlerArgs<TData, TFrom, TTo> : ContentHandlerArgs<TFrom, TTo>
    {
        public ContentHandlerArgs(TData data, TFrom @from, TTo to)
            : base(from, to)
        {
            Data = data;
        }

        public TData Data { get; private set; }
    }
}