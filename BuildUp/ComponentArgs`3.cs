namespace BuildUp
{
    public class ComponentArgs<TTo, TFrom, TData> : ComponentArgs<TTo, TFrom>
    {
        public ComponentArgs(TTo to, TFrom @from, TData data)
            : base(to, @from)
        {
            Data = data;
        }

        public TData Data { get; private set; }
    }
}