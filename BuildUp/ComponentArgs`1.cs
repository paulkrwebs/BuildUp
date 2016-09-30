namespace BuildUp
{
    public class ComponentArgs<TToHandle> : ComponentArgs
    {
        public ComponentArgs(TToHandle toHandle)
        {
            ToHandle = toHandle;
        }

        public TToHandle ToHandle { get; private set; }
    }
}