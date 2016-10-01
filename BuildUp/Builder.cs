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

        public TToCreate Build<TToCreate, TFrom>(TFrom @from)
            where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new ComponentArgs<TToCreate, TFrom>(to, @from);
            var handled = _componentPipeline.Raise(args);

            // throw exception if no handlers raised?
            if (!handled)
                _propertyMapper.Map(from, to);

            return to;
        }

        public TToCreate Build<TToCreate, TFrom, TData>(TFrom @from, TData data)
            where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new ComponentArgs<TToCreate, TFrom, TData>(to, @from, data);
            var handled = _componentPipeline.Raise(args);

            if (!handled)
                _propertyMapper.Map(from, to);

            return to;
        }

        public async Task<TToCreate> BuildAsync<TToCreate, TFrom>(TFrom @from)
            where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new ComponentArgs<TToCreate, TFrom>(to, @from);
            var handled = await _componentPipeline.RaiseAsync(args);

            if (!handled)
                _propertyMapper.Map(from, to);

            return to;
        }

        public async Task<TToCreate> BuildAsync<TToCreate, TFrom, TData>(TFrom @from, TData data)
            where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new ComponentArgs<TToCreate, TFrom, TData>(to, @from, data);
            var handled = await _componentPipeline.RaiseAsync(args);

            if (!handled)
                _propertyMapper.Map(from, to);

            return to;
        }
    }
}