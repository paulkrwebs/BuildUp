namespace BuildUp
{
    public class ContentHandlerArgs<TToHandle> : ContentHandlerArgs
    {
        public ContentHandlerArgs(TToHandle toHandle)
        {
            ToHandle = toHandle;
        }

        public TToHandle ToHandle { get; private set; }
    }
}