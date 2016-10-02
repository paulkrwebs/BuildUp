namespace BuildUp
{
    public class ComponentArgs<TToHandle> : ComponentArgs
    {
        public ComponentArgs()
        {
        }

        public TToHandle ToBuild { get; set; }
    }
}