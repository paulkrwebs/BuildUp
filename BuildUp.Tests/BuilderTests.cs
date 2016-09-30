namespace BuildUp.Tests
{
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderTests
    {
        #region Fields

        private Mock<IComponentPipeline> _componentPipeline;
        private Mock<IPropertyMapper> _propertyMapper;
        private IBuilder _builder;

        #endregion Fields

        #region Tests

        public void Setup()
        {
            _componentPipeline = new Mock<IComponentPipeline>();
            _propertyMapper = new Mock<IPropertyMapper>();
            _builder = new Builder(_propertyMapper.Object, _componentPipeline.Object);
        }

        [Test]
        public void Build_NoParams_ModelBuiltAndComponentsRaisedOnPipeline()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _componentPipeline.Setup(
                x => x.Raise(It.IsAny<ComponentArgs<ViewModel>>()));

            // Act
            ViewModel viewModel = _builder.Build<ViewModel>();

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _componentPipeline.Verify(c => c.Raise(It.IsAny<ComponentArgs<ViewModel>>()), Times.Once());
        }

        [Test]
        public async void BuildAsync_NoParams_ModelBuiltAndComponentsRaisedOnPipeline()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _componentPipeline.Setup(
                x => x.RaiseAsync(It.IsAny<ComponentArgs<ViewModel>>())).ReturnsAsync(true);

            // Act
            ViewModel viewModel = await _builder.BuildAsync<ViewModel>();

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _componentPipeline.Verify(c => c.RaiseAsync(It.IsAny<ComponentArgs<ViewModel>>()), Times.Once());
        }

        [Test]
        public void Build_EpiServerModel_ModelBuiltAndNotHandledSoDefaultMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _propertyMapper.Setup(
                x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            _componentPipeline.Setup(
                x => x.Raise(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>())).Returns(false);

            // Act
            ViewModel viewModel = _builder.Build<EPiServerModel, ViewModel>(new EPiServerModel() { Title = "MoFo" });

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _componentPipeline.Verify(c => c.Raise(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.Once());
        }

        [Test]
        public async void BuildAsync_EpiServerModel_ModelBuiltAndNotHandledSoDefaultMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _propertyMapper.Setup(
                x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            _componentPipeline.Setup(
                x => x.RaiseAsync(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>())).ReturnsAsync(false);

            // Act
            ViewModel viewModel = await _builder.BuildAsync<EPiServerModel, ViewModel>(new EPiServerModel() { Title = "MoFo" });

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _componentPipeline.Verify(c => c.RaiseAsync(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.Once());
        }

        [Test]
        public void Build_EpiServerModel_ModelBuiltAndHandledSoDefaultNotMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _propertyMapper.Setup(
                x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            _componentPipeline.Setup(
                x => x.Raise(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>())).Returns(true);

            // Act
            ViewModel viewModel = _builder.Build<EPiServerModel, ViewModel>(new EPiServerModel() { Title = "MoFo" });

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _componentPipeline.Verify(c => c.Raise(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.Never());
        }

        [Test]
        public async void BuildAsync_EpiServerModel_ModelBuiltAndHandledSoDefaultNotMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _propertyMapper.Setup(
                x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            _componentPipeline.Setup(
                x => x.RaiseAsync(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>())).ReturnsAsync(true);

            // Act
            ViewModel viewModel = await _builder.BuildAsync<EPiServerModel, ViewModel>(new EPiServerModel() { Title = "MoFo" });

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _componentPipeline.Verify(c => c.RaiseAsync(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.Never());
        }

        [Test]
        public void Build_EpiServerModelAndFormModel_ModelBuiltAndNotHandledSoDefaultMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _propertyMapper.Setup(
                x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            _componentPipeline.Setup(
                x => x.Raise(It.IsAny<ComponentArgs<FormModel, EPiServerModel, ViewModel>>())).Returns(false);

            // Act
            ViewModel viewModel = _builder.Build<FormModel, EPiServerModel, ViewModel>(new FormModel() { Step = 1 }, new EPiServerModel() { Title = "MoFo" });

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _componentPipeline.Verify(c => c.Raise(It.IsAny<ComponentArgs<FormModel, EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.Once());
        }

        [Test]
        public async void BuildAsync_EpiServerModelAndFormModel_ModelBuiltAndNotHandledSoDefaultMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _propertyMapper.Setup(
                x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            _componentPipeline.Setup(
                x => x.RaiseAsync(It.IsAny<ComponentArgs<FormModel, EPiServerModel, ViewModel>>())).ReturnsAsync(false);

            // Act
            ViewModel viewModel = await _builder.BuildAsync<FormModel, EPiServerModel, ViewModel>(new FormModel() { Step = 1 }, new EPiServerModel() { Title = "MoFo" });

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _componentPipeline.Verify(c => c.RaiseAsync(It.IsAny<ComponentArgs<FormModel, EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.Once());
        }

        [Test]
        public void Build_EpiServerModelAndFormModel_ModelBuiltAndHandledSoDefaultMappingNotUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _propertyMapper.Setup(
                x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            _componentPipeline.Setup(
                x => x.Raise(It.IsAny<ComponentArgs<FormModel, EPiServerModel, ViewModel>>())).Returns(true);

            // Act
            ViewModel viewModel = _builder.Build<FormModel, EPiServerModel, ViewModel>(new FormModel() { Step = 1 }, new EPiServerModel() { Title = "MoFo" });

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _componentPipeline.Verify(c => c.Raise(It.IsAny<ComponentArgs<FormModel, EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.Never());
        }

        [Test]
        public async void BuildAsync_EpiServerModelAndFormModel_ModelBuiltAndHandledSoDefaultMappingNotUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _propertyMapper.Setup(
                x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            _componentPipeline.Setup(
                x => x.RaiseAsync(It.IsAny<ComponentArgs<FormModel, EPiServerModel, ViewModel>>())).ReturnsAsync(true);

            // Act
            ViewModel viewModel = await _builder.BuildAsync<FormModel, EPiServerModel, ViewModel>(new FormModel() { Step = 1 }, new EPiServerModel() { Title = "MoFo" });

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _componentPipeline.Verify(c => c.RaiseAsync(It.IsAny<ComponentArgs<FormModel, EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.Never());
        }

        #endregion Tests

        #region Data

        private class EPiServerModel
        {
            public string Title { get; set; }
        }

        private class FormModel
        {
            public int Step { get; set; }
        }

        private class ViewModel
        {
            public string Title { get; set; }
        }

        #endregion Data
    }
}