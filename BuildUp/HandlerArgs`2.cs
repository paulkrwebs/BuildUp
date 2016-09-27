namespace BuildUp
{
    public class ContentHandlerArgs<TFrom, TTo> : ContentHandlerArgs
    {
        public ContentHandlerArgs(TFrom @from, TTo to)
        {
            From = @from;
            To = to;
        }

        public TFrom From { get; private set; }

        public TTo To { get; private set; }
    }
}