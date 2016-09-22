namespace ViewModelBuilding
{
    public class HandlerArgs<TToHandle> : HandlerArgs
    {
        public HandlerArgs(TToHandle toHandle)
        {
            ToHandle = toHandle;
        }

        public TToHandle ToHandle { get; private set; }
    }
}