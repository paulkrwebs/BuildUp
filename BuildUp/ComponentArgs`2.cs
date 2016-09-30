namespace BuildUp
{
    public class ComponentArgs<TFrom, TTo> : ComponentArgs
    {
        public ComponentArgs(TFrom @from, TTo to)
        {
            From = @from;
            To = to;
        }

        public TFrom From { get; private set; }

        public TTo To { get; private set; }
    }
}