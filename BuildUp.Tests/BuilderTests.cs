namespace BuildUp.Tests
{
    using Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderTests
    {
        #region Fields

        private Mock<IComponentPipeline> _componentPipeline;
        private IBuilder _builder;

        #endregion Fields

        #region Tests

        public void Setup()
        {
            _componentPipeline = new Mock<IComponentPipeline>();
            _builder = new Builder(_componentPipeline.Object);
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
            _componentPipeline.Verify(c => c.RaiseAsync(It.IsAny<ComponentArgs<ViewModel>>()), Times.Once());
        }

        [Test]
        public void Build_EpiServerModel_ModelBuiltAndNotHandledSoDefaultMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _componentPipeline.Setup(
                x => x.Raise(It.IsAny<ComponentArgs<ViewModel, EPiServerModel>>())).Returns(false);

            // Act
            ViewModel viewModel = _builder.Build<ViewModel, EPiServerModel>(new EPiServerModel() { Title = "MoFo" });

            // Assert
            _componentPipeline.Verify(c => c.Raise(It.IsAny<ComponentArgs<ViewModel, EPiServerModel>>()), Times.Once());
        }

        [Test]
        public async void BuildAsync_EpiServerModel_ModelBuiltAndNotHandledSoDefaultMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _componentPipeline.Setup(
                x => x.RaiseAsync(It.IsAny<ComponentArgs<ViewModel, EPiServerModel>>())).ReturnsAsync(false);

            // Act
            ViewModel viewModel = await _builder.BuildAsync<ViewModel, EPiServerModel>(new EPiServerModel() { Title = "MoFo" });

            // Assert
            _componentPipeline.Verify(c => c.RaiseAsync(It.IsAny<ComponentArgs<ViewModel, EPiServerModel>>()), Times.Once());
        }

        [Test]
        public void Build_EpiServerModel_ModelBuiltAndHandledSoDefaultNotMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _componentPipeline.Setup(
                x => x.Raise(It.IsAny<ComponentArgs<ViewModel, EPiServerModel>>())).Returns(true);

            // Act
            ViewModel viewModel = _builder.Build<ViewModel, EPiServerModel>(new EPiServerModel() { Title = "MoFo" });

            // Assert
            _componentPipeline.Verify(c => c.Raise(It.IsAny<ComponentArgs<ViewModel, EPiServerModel>>()), Times.Once());
        }

        [Test]
        public async void BuildAsync_EpiServerModel_ModelBuiltAndHandledSoDefaultNotMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _componentPipeline.Setup(
                x => x.RaiseAsync(It.IsAny<ComponentArgs<ViewModel, EPiServerModel>>())).ReturnsAsync(true);

            // Act
            ViewModel viewModel = await _builder.BuildAsync<ViewModel, EPiServerModel>(new EPiServerModel() { Title = "MoFo" });

            // Assert
            _componentPipeline.Verify(c => c.RaiseAsync(It.IsAny<ComponentArgs<ViewModel, EPiServerModel>>()), Times.Once());
        }

        [Test]
        public void Build_EpiServerModelAndFormModel_ModelBuiltAndNotHandledSoDefaultMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _componentPipeline.Setup(
                x => x.Raise(It.IsAny<ComponentArgs<ViewModel, FormModel, EPiServerModel>>())).Returns(false);

            // Act
            ViewModel viewModel = _builder.Build<ViewModel, EPiServerModel, FormModel>(new EPiServerModel() { Title = "MoFo" }, new FormModel() { Step = 1 });

            // Assert
            _componentPipeline.Verify(c => c.Raise(It.IsAny<ComponentArgs<ViewModel, EPiServerModel, FormModel>>()), Times.Once());
        }

        [Test]
        public async void BuildAsync_EpiServerModelAndFormModel_ModelBuiltAndNotHandledSoDefaultMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _componentPipeline.Setup(
                x => x.RaiseAsync(It.IsAny<ComponentArgs<ViewModel, EPiServerModel, FormModel>>())).ReturnsAsync(false);

            // Act
            ViewModel viewModel = await _builder.BuildAsync<ViewModel, EPiServerModel, FormModel>(new EPiServerModel() { Title = "MoFo" }, new FormModel() { Step = 1 });

            // Assert
            _componentPipeline.Verify(c => c.RaiseAsync(It.IsAny<ComponentArgs<ViewModel, EPiServerModel, FormModel>>()), Times.Once());
        }

        [Test]
        public void Build_EpiServerModelAndFormModel_ModelBuiltAndHandledSoDefaultMappingNotUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _componentPipeline.Setup(
                x => x.Raise(It.IsAny<ComponentArgs<ViewModel, EPiServerModel, FormModel>>())).Returns(true);

            // Act
            ViewModel viewModel = _builder.Build<ViewModel, EPiServerModel, FormModel>(new EPiServerModel() { Title = "MoFo" }, new FormModel() { Step = 1 });

            // Assert
            _componentPipeline.Verify(c => c.Raise(It.IsAny<ComponentArgs<ViewModel, EPiServerModel, FormModel>>()), Times.Once());
        }

        [Test]
        public async void BuildAsync_EpiServerModelAndFormModel_ModelBuiltAndHandledSoDefaultMappingNotUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _componentPipeline.Setup(
                x => x.RaiseAsync(It.IsAny<ComponentArgs<ViewModel, EPiServerModel, FormModel>>())).ReturnsAsync(true);

            // Act
            ViewModel viewModel = await _builder.BuildAsync<ViewModel, EPiServerModel, FormModel>(new EPiServerModel() { Title = "MoFo" }, new FormModel() { Step = 1 });

            // Assert
            _componentPipeline.Verify(c => c.RaiseAsync(It.IsAny<ComponentArgs<ViewModel, EPiServerModel, FormModel>>()), Times.Once());
        }

        #endregion Tests
    }
}