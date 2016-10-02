namespace BuildUp
{
    public class ComponentArgs<TTo, TFrom> : ComponentArgs
    {
        public ComponentArgs(TFrom @from)
        {
            From = @from;
        }

        public TFrom From { get; private set; }

        public TTo ToBuild { get; private set; }
    }
}