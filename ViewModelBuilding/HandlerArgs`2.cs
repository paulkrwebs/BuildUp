namespace ViewModelBuilding
{
    public class HandlerArgs<TFrom, TTo> : HandlerArgs
    {
        public HandlerArgs(TFrom @from, TTo to)
        {
            From = @from;
            To = to;
        }

        public TFrom From { get; private set; }

        public TTo To { get; private set; }
    }
}
