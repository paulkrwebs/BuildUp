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

        public TToBuild Build<TToBuild>()
           where TToBuild : new()
        {
            var args = new ComponentArgs<TToBuild>();
            _componentPipeline.Raise(args);

            return args.ToBuild;
        }

        public async Task<TToBuild> BuildAsync<TToBuild>()
           where TToBuild : new()
        {
            var args = new ComponentArgs<TToBuild>();
            await _componentPipeline.RaiseAsync(args);

            return args.ToBuild;
        }

        public TToBuild Build<TToBuild, TFrom>(TFrom @from)
            where TToBuild : new()
        {
            var args = new ComponentArgs<TToBuild, TFrom>(@from);
            var handled = _componentPipeline.Raise(args);

            return args.ToBuild;
        }

        public TToBuild Build<TToBuild, TFrom, TFrom2>(TFrom @from, TFrom2 from2)
            where TToBuild : new()
        {
            var args = new ComponentArgs<TToBuild, TFrom, TFrom2>(@from, from2);
            var handled = _componentPipeline.Raise(args);

            return args.ToBuild;
        }

        public async Task<TToBuild> BuildAsync<TToBuild, TFrom>(TFrom @from)
            where TToBuild : new()
        {
            var args = new ComponentArgs<TToBuild, TFrom>(@from);
            var handled = await _componentPipeline.RaiseAsync(args);

            return args.ToBuild;
        }

        public async Task<TToBuild> BuildAsync<TToBuild, TFrom, TFrom2>(TFrom @from, TFrom2 from2)
            where TToBuild : new()
        {
            var args = new ComponentArgs<TToBuild, TFrom, TFrom2>(@from, from2);
            var handled = await _componentPipeline.RaiseAsync(args);

            return args.ToBuild;
        }
    }
}