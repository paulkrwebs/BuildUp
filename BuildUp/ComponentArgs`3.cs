namespace BuildUp
{
    public class ComponentArgs<TData, TFrom, TTo> : ComponentArgs<TFrom, TTo>
    {
        public ComponentArgs(TData data, TFrom @from, TTo to)
            : base(from, to)
        {
            Data = data;
        }

        public TData Data { get; private set; }
    }
}