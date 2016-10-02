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
               .Setup(x => x.ResolverAll<IComponent<ComponentArgs<ViewModel, EPiServerModel>>>())
               .Returns(Enumerable.Empty<IComponent<ComponentArgs<ViewModel, EPiServerModel>>>());

            // Act
            bool result = _componentPipeline.Raise(new ComponentArgs<ViewModel, EPiServerModel>(new EPiServerModel()));

            // Assert
            Assert.That(result, Is.False);
            _componentResolver.Verify(x => x.ResolverAll<IComponent<ComponentArgs<ViewModel, EPiServerModel>>>(), Times.Once, "Component resolver was one called");
        }

        [Test]
        public void Raise_TComponentsArgs_ComponentsResolvedAndInvoked()
        {
            Mock<IComponent<ComponentArgs<ViewModel, EPiServerModel>>> Component1 = new Mock<IComponent<ComponentArgs<ViewModel, EPiServerModel>>>();
            Mock<IComponent<ComponentArgs<ViewModel, EPiServerModel>>> Component2 = new Mock<IComponent<ComponentArgs<ViewModel, EPiServerModel>>>();

            // Arrange
            _componentResolver
                .Setup(x => x.ResolverAll<IComponent<ComponentArgs<ViewModel, EPiServerModel>>>())
                .Returns(new List<IComponent<ComponentArgs<ViewModel, EPiServerModel>>>()
                             {
                                 Component1.Object,
                                 Component2.Object
                             });

            // Act
            bool result = _componentPipeline.Raise(new ComponentArgs<ViewModel, EPiServerModel>(new EPiServerModel()));

            // Assert
            Assert.That(result, Is.True);
            _componentResolver.Verify(x => x.ResolverAll<IComponent<ComponentArgs<ViewModel, EPiServerModel>>>(), Times.Once, "Component resolver was one called");
            Component1.Verify(x => x.Handle(It.IsAny<ComponentArgs<ViewModel, EPiServerModel>>()), Times.Once());
            Component2.Verify(x => x.Handle(It.IsAny<ComponentArgs<ViewModel, EPiServerModel>>()), Times.Once());
        }

        [Test]
        public async void RaiseAsync_TComponentsArgs_NoHanldersMatched()
        {
            // Arrange
            _componentResolver
               .Setup(x => x.ResolverAll<IComponentAsync<ComponentArgs<ViewModel, EPiServerModel>>>())
               .Returns(Enumerable.Empty<IComponentAsync<ComponentArgs<ViewModel, EPiServerModel>>>());

            // Act
            bool result = await _componentPipeline.RaiseAsync(new ComponentArgs<ViewModel, EPiServerModel>(new EPiServerModel()));

            // Assert
            Assert.That(result, Is.False);
            _componentResolver.Verify(x => x.ResolverAll<IComponentAsync<ComponentArgs<ViewModel, EPiServerModel>>>(), Times.Once, "Component resolver was one called");
        }

        [Test]
        public async void RaiseAsync_TComponentsArgs_ComponentsResolvedAndInvoked()
        {
            Mock<IComponentAsync<ComponentArgs<ViewModel, EPiServerModel>>> Component1 = new Mock<IComponentAsync<ComponentArgs<ViewModel, EPiServerModel>>>();
            Mock<IComponentAsync<ComponentArgs<ViewModel, EPiServerModel>>> Component2 = new Mock<IComponentAsync<ComponentArgs<ViewModel, EPiServerModel>>>();

            // Arrange
            _componentResolver
                .Setup(x => x.ResolverAll<IComponentAsync<ComponentArgs<ViewModel, EPiServerModel>>>())
                .Returns(new List<IComponentAsync<ComponentArgs<ViewModel, EPiServerModel>>>()
                             {
                                 Component1.Object,
                                 Component2.Object
                             });

            // Act
            bool result = await _componentPipeline.RaiseAsync(new ComponentArgs<ViewModel, EPiServerModel>(new EPiServerModel()));

            // Assert
            Assert.That(result, Is.True);
            _componentResolver.Verify(x => x.ResolverAll<IComponentAsync<ComponentArgs<ViewModel, EPiServerModel>>>(), Times.Once, "Component resolver was one called");
            Component1.Verify(x => x.HandleAsync(It.IsAny<ComponentArgs<ViewModel, EPiServerModel>>()), Times.Once());
            Component2.Verify(x => x.HandleAsync(It.IsAny<ComponentArgs<ViewModel, EPiServerModel>>()), Times.Once());
        }

        #endregion Tests

        #region From2

        public class EPiServerModel
        {
            public string Title { get; set; }
        }

        public class ViewModel
        {
            public string Title { get; set; }
        }

        #endregion From2
    }
}