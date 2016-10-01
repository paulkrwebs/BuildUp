namespace BuildUp.Tests
{
    using Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class PropertyMapperComponentTests
    {
        private Mock<IPropertyMapper> _propertyMapper;
        private PropertyMapperComponent<ViewModel, EPiServerModel> _propertyMapperComponent;

        [SetUp]
        public void Setup()
        {
            _propertyMapper = new Mock<IPropertyMapper>();
            _propertyMapperComponent = new PropertyMapperComponent<ViewModel, EPiServerModel>(_propertyMapper.Object);
        }

        [Test]
        public void Handle_ComponentArgs_PropertyMapperCalled()
        {
            ComponentArgs<ViewModel, EPiServerModel> args = new ComponentArgs<ViewModel, EPiServerModel>(new ViewModel(), new EPiServerModel());

            _propertyMapperComponent.Handle(args);

            _propertyMapper.Verify(p => p.Map<EPiServerModel, ViewModel>(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), "Property Mapper was not called");
        }
    }
}