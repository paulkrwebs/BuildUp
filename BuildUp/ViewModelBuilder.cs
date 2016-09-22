namespace BuildUp
{
    using System.Threading.Tasks;

    public class ViewModelBuilder : IViewModelBuilder
    {
        private readonly IPropertyMapper _propertyMapper;
        private readonly IContentHandlerPipeline _contentHandlerPipeline;

        public ViewModelBuilder(IPropertyMapper propertyMapper, IContentHandlerPipeline contentHandlerPipeline)
        {
            _contentHandlerPipeline = contentHandlerPipeline;
            _propertyMapper = propertyMapper;
        }

        public TToCreate Build<TToCreate>()
           where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new HandlerArgs<TToCreate>(to);
            _contentHandlerPipeline.Raise(args);

            return to;
        }

        public async Task<TToCreate> BuildAsync<TToCreate>()
           where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new HandlerArgs<TToCreate>(to);
            await _contentHandlerPipeline.RaiseAsync(args);

            return to;
        }

        public TToCreate Build<TFrom, TToCreate>(TFrom @from)
            where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new HandlerArgs<TFrom, TToCreate>(@from, to);
            var handled = _contentHandlerPipeline.Raise(args);

            if (!handled)
                _propertyMapper.Map(from, to);

            return to;
        }

        public TToCreate Build<TData, TFrom, TToCreate>(TData data, TFrom @from) where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new HandlerArgs<TData, TFrom, TToCreate>(data, @from, to);
            var handled = _contentHandlerPipeline.Raise(args);

            if (!handled)
                _propertyMapper.Map(from, to);

            return to;
        }

        public async Task<TToCreate> BuildAsync<TFrom, TToCreate>(TFrom @from)
            where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new HandlerArgs<TFrom, TToCreate>(@from, to);
            var handled = await _contentHandlerPipeline.RaiseAsync(args);

            if (!handled)
                _propertyMapper.Map(from, to);

            return to;
        }

        public async Task<TToCreate> BuildAsync<TData, TFrom, TToCreate>(TData data, TFrom @from) where TToCreate : new()
        {
            var to = new TToCreate();

            var args = new HandlerArgs<TData, TFrom, TToCreate>(data, @from, to);
            var handled = await _contentHandlerPipeline.RaiseAsync(args);

            if (!handled)
                _propertyMapper.Map(from, to);

            return to;
        }
    }
}