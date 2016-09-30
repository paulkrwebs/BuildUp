namespace BuildUp.Tests
{
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class ComponentPipelineTests
    {
        #region Fields

        private Mock<IComponentResolver> _componentResolver;
        private IComponentPipeline _componentPipeline;

        #endregion Fields

        #region Tests

        [SetUp]
        public void Setup()
        {
            _componentResolver = new Mock<IComponentResolver>();
            _componentPipeline = new ComponentPipeline(_componentResolver.Object);
        }

        [Test]
        public void Raise_TComponentsArgs_NoHanldersMatched()
        {
            // Arrange
            _componentResolver
               .Setup(x => x.ResolverAll<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>())
               .Returns(Enumerable.Empty<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>());

            // Act
            bool result = _componentPipeline.Raise(new ComponentArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.False);
            _componentResolver.Verify(x => x.ResolverAll<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Component resolver was one called");
        }

        [Test]
        public void Raise_TComponentsArgs_ComponentsResolvedAndInvoked()
        {
            Mock<IComponent<ComponentArgs<EPiServerModel, ViewModel>>> Component1 = new Mock<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>();
            Mock<IComponent<ComponentArgs<EPiServerModel, ViewModel>>> Component2 = new Mock<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>();

            // Arrange
            _componentResolver
                .Setup(x => x.ResolverAll<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>())
                .Returns(new List<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>()
                             {
                                 Component1.Object,
                                 Component2.Object
                             });

            // Act
            bool result = _componentPipeline.Raise(new ComponentArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.True);
            _componentResolver.Verify(x => x.ResolverAll<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Component resolver was one called");
            Component1.Verify(x => x.Handle(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>()), Times.Once());
            Component2.Verify(x => x.Handle(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>()), Times.Once());
        }

        [Test]
        public async void RaiseAsync_TComponentsArgs_NoHanldersMatched()
        {
            // Arrange
            _componentResolver
               .Setup(x => x.ResolverAll<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>())
               .Returns(Enumerable.Empty<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>());

            // Act
            bool result = await _componentPipeline.RaiseAsync(new ComponentArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.False);
            _componentResolver.Verify(x => x.ResolverAll<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Component resolver was one called");
        }

        [Test]
        public async void RaiseAsync_TComponentsArgs_ComponentsResolvedAndInvoked()
        {
            Mock<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>> Component1 = new Mock<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>();
            Mock<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>> Component2 = new Mock<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>();

            // Arrange
            _componentResolver
                .Setup(x => x.ResolverAll<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>())
                .Returns(new List<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>()
                             {
                                 Component1.Object,
                                 Component2.Object
                             });

            // Act
            bool result = await _componentPipeline.RaiseAsync(new ComponentArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.True);
            _componentResolver.Verify(x => x.ResolverAll<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Component resolver was one called");
            Component1.Verify(x => x.HandleAsync(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>()), Times.Once());
            Component2.Verify(x => x.HandleAsync(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>()), Times.Once());
        }

        #endregion Tests

        #region Data

        public class EPiServerModel
        {
            public string Title { get; set; }
        }

        public class ViewModel
        {
            public string Title { get; set; }
        }

        #endregion Data
    }
}