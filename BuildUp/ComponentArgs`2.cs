namespace BuildUp
{
    public class ComponentArgs<TTo, TFrom> : ComponentArgs
    {
        public ComponentArgs(TTo to, TFrom @from)
        {
            From = @from;
            To = to;
        }

        public TFrom From { get; private set; }

        public TTo To { get; private set; }
    }
}