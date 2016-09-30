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

        private Mock<IComponentResolver> _handlerResolver;
        private IComponentPipeline _contentHandlerPipeline;

        #endregion Fields

        #region Tests

        [SetUp]
        public void Setup()
        {
            _handlerResolver = new Mock<IComponentResolver>();
            _contentHandlerPipeline = new ComponentPipeline(_handlerResolver.Object);
        }

        [Test]
        public void Raise_TContentHandlerArgs_NoHanldersMatched()
        {
            // Arrange
            _handlerResolver
               .Setup(x => x.ResolverAll<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>())
               .Returns(Enumerable.Empty<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>());

            // Act
            bool result = _contentHandlerPipeline.Raise(new ComponentArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.False);
            _handlerResolver.Verify(x => x.ResolverAll<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Handler resolver was one called");
        }

        [Test]
        public void Raise_TContentHandlerArgs_HandlersResolvedAndInvoked()
        {
            Mock<IComponent<ComponentArgs<EPiServerModel, ViewModel>>> handler1 = new Mock<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>();
            Mock<IComponent<ComponentArgs<EPiServerModel, ViewModel>>> handler2 = new Mock<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>();

            // Arrange
            _handlerResolver
                .Setup(x => x.ResolverAll<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>())
                .Returns(new List<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>()
                             {
                                 handler1.Object,
                                 handler2.Object
                             });

            // Act
            bool result = _contentHandlerPipeline.Raise(new ComponentArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.True);
            _handlerResolver.Verify(x => x.ResolverAll<IComponent<ComponentArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Handler resolver was one called");
            handler1.Verify(x => x.Handle(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>()), Times.Once());
            handler2.Verify(x => x.Handle(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>()), Times.Once());
        }

        [Test]
        public async void RaiseAsync_TContentHandlerArgs_NoHanldersMatched()
        {
            // Arrange
            _handlerResolver
               .Setup(x => x.ResolverAll<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>())
               .Returns(Enumerable.Empty<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>());

            // Act
            bool result = await _contentHandlerPipeline.RaiseAsync(new ComponentArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.False);
            _handlerResolver.Verify(x => x.ResolverAll<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Handler resolver was one called");
        }

        [Test]
        public async void RaiseAsync_TContentHandlerArgs_HandlersResolvedAndInvoked()
        {
            Mock<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>> handler1 = new Mock<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>();
            Mock<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>> handler2 = new Mock<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>();

            // Arrange
            _handlerResolver
                .Setup(x => x.ResolverAll<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>())
                .Returns(new List<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>()
                             {
                                 handler1.Object,
                                 handler2.Object
                             });

            // Act
            bool result = await _contentHandlerPipeline.RaiseAsync(new ComponentArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.True);
            _handlerResolver.Verify(x => x.ResolverAll<IComponentAsync<ComponentArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Handler resolver was one called");
            handler1.Verify(x => x.HandleAsync(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>()), Times.Once());
            handler2.Verify(x => x.HandleAsync(It.IsAny<ComponentArgs<EPiServerModel, ViewModel>>()), Times.Once());
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