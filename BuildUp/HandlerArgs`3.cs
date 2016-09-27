namespace BuildUp
{
    public class HandlerArgs<TData, TFrom, TTo> : HandlerArgs<TFrom, TTo>
    {
        public HandlerArgs(TData data, TFrom @from, TTo to)
            : base(from, to)
        {
            Data = data;
        }

        public TData Data { get; private set; }
    }
}