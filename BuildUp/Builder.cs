namespace BuildUp
{
    using System.Threading.Tasks;

    public class Builder : IBuilder
    {
        private readonly IComponentPipeline _componentPipeline;

        public Builder(IComponentPipeline componentPipeline)
        {
            _componentPipeline = componentPipeline;
        }

        public TToCreate Build<TToCreate>()
           where TToCreate : new()
        {
            var to = new TToCreate();
            var args = new ComponentArgs<TToCreate>(to);
            _componentPipeline.Raise(args);

            return to;
        }

        public async Task<TToCreate> BuildAsync<TToCreate>()
           where TToCreate : new()
        {
            var to = new TToCreate();
            var args = new ComponentArgs<TToCreate>(to);
            await _componentPipeline.RaiseAsync(args);

            return to;
        }

        public TToCreate Build<TToCreate, TFrom>(TFrom @from)
            where TToCreate : new()
        {
            var to = new TToCreate();
            var args = new ComponentArgs<TToCreate, TFrom>(to, @from);
            var handled = _componentPipeline.Raise(args);

            return to;
        }

        public TToCreate Build<TToCreate, TFrom, TFrom2>(TFrom @from, TFrom2 from2)
            where TToCreate : new()
        {
            var to = new TToCreate();
            var args = new ComponentArgs<TToCreate, TFrom, TFrom2>(to, @from, from2);
            var handled = _componentPipeline.Raise(args);

            return to;
        }

        public async Task<TToCreate> BuildAsync<TToCreate, TFrom>(TFrom @from)
            where TToCreate : new()
        {
            var to = new TToCreate();
            var args = new ComponentArgs<TToCreate, TFrom>(to, @from);
            var handled = await _componentPipeline.RaiseAsync(args);

            return to;
        }

        public async Task<TToCreate> BuildAsync<TToCreate, TFrom, TFrom2>(TFrom @from, TFrom2 from2)
            where TToCreate : new()
        {
            var to = new TToCreate();
            var args = new ComponentArgs<TToCreate, TFrom, TFrom2>(to, @from, from2);
            var handled = await _componentPipeline.RaiseAsync(args);

            return to;
        }
    }
}