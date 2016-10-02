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
            var args = new ComponentArgs<TToCreate>();
            _componentPipeline.Raise(args);

            return args.ToBuild;
        }

        public async Task<TToCreate> BuildAsync<TToCreate>()
           where TToCreate : new()
        {
            var args = new ComponentArgs<TToCreate>();
            await _componentPipeline.RaiseAsync(args);

            return args.ToBuild;
        }

        public TToCreate Build<TToCreate, TFrom>(TFrom @from)
            where TToCreate : new()
        {
            var args = new ComponentArgs<TToCreate, TFrom>(@from);
            var handled = _componentPipeline.Raise(args);

            return args.ToBuild;
        }

        public TToCreate Build<TToCreate, TFrom, TFrom2>(TFrom @from, TFrom2 from2)
            where TToCreate : new()
        {
            var args = new ComponentArgs<TToCreate, TFrom, TFrom2>(@from, from2);
            var handled = _componentPipeline.Raise(args);

            return args.ToBuild;
        }

        public async Task<TToCreate> BuildAsync<TToCreate, TFrom>(TFrom @from)
            where TToCreate : new()
        {
            var args = new ComponentArgs<TToCreate, TFrom>(@from);
            var handled = await _componentPipeline.RaiseAsync(args);

            return args.ToBuild;
        }

        public async Task<TToCreate> BuildAsync<TToCreate, TFrom, TFrom2>(TFrom @from, TFrom2 from2)
            where TToCreate : new()
        {
            var args = new ComponentArgs<TToCreate, TFrom, TFrom2>(@from, from2);
            var handled = await _componentPipeline.RaiseAsync(args);

            return args.ToBuild;
        }
    }
}