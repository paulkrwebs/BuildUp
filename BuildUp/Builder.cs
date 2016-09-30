namespace BuildUp
{
    using System.Threading.Tasks;

    public class Builder : IBuilder
    {
        private readonly IPropertyMapper _propertyMapper;
        private readonly IComponentPipeline _componentPipeline;

        public Builder(IPropertyMapper propertyMapper, IComponentPipeline componentPipeline)
        {
            _componentPipeline = componentPipeline;
            _propertyMapper = propertyMapper;
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

        public TToCreate Build<TFrom, TToCreate>(TFrom @from)
            where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new ComponentArgs<TFrom, TToCreate>(@from, to);
            var handled = _componentPipeline.Raise(args);

            if (!handled)
                _propertyMapper.Map(from, to);

            return to;
        }

        public TToCreate Build<TData, TFrom, TToCreate>(TData data, TFrom @from) where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new ComponentArgs<TData, TFrom, TToCreate>(data, @from, to);
            var handled = _componentPipeline.Raise(args);

            if (!handled)
                _propertyMapper.Map(from, to);

            return to;
        }

        public async Task<TToCreate> BuildAsync<TFrom, TToCreate>(TFrom @from)
            where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new ComponentArgs<TFrom, TToCreate>(@from, to);
            var handled = await _componentPipeline.RaiseAsync(args);

            if (!handled)
                _propertyMapper.Map(from, to);

            return to;
        }

        public async Task<TToCreate> BuildAsync<TData, TFrom, TToCreate>(TData data, TFrom @from) where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new ComponentArgs<TData, TFrom, TToCreate>(data, @from, to);
            var handled = await _componentPipeline.RaiseAsync(args);

            if (!handled)
                _propertyMapper.Map(from, to);

            return to;
        }
    }
}