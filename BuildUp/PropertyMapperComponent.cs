using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildUp
{
    public class PropertyMapperComponent<TToCreate, TFrom> : IComponent<ComponentArgs<TToCreate, TFrom>>
        where TToCreate : class, new()
        where TFrom : class, new()
    {
        #region Fields

        private readonly IPropertyMapper _propertyMapper;

        #endregion Fields

        public PropertyMapperComponent(IPropertyMapper propertyMapper)
        {
            _propertyMapper = propertyMapper;
        }

        public void Handle(ComponentArgs<TToCreate, TFrom> arg1)
        {
            _propertyMapper.Map(arg1.From, arg1.ToBuild);
        }
    }
}