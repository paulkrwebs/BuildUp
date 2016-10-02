using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildUp
{
    public class PropertyMapperComponent<TToBuild, TFrom> : IComponent<ComponentArgs<TToBuild, TFrom>>
        where TToBuild : class, new()
        where TFrom : class, new()
    {
        #region Fields

        private readonly IPropertyMapper _propertyMapper;

        #endregion Fields

        public PropertyMapperComponent(IPropertyMapper propertyMapper)
        {
            _propertyMapper = propertyMapper;
        }

        public void Handle(ComponentArgs<TToBuild, TFrom> arg1)
        {
            arg1.ToBuild = arg1.ToBuild ?? new TToBuild();

            _propertyMapper.Map(arg1.From, arg1.ToBuild);
        }
    }
}