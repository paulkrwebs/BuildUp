namespace BuildUp
{
    public class ComponentArgs<TTo, TFrom, TFrom2> : ComponentArgs<TTo, TFrom>
    {
        public ComponentArgs(TTo to, TFrom @from, TFrom2 from2)
            : base(to, @from)
        {
            From2 = from2;
        }

        public TFrom2 From2 { get; private set; }
    }
}