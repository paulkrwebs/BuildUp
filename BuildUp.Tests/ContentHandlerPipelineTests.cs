namespace BuildUp.Tests
{
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class ContentHandlerPipelineTests
    {
        #region Fields

        private Mock<IHandlerResolver> _handlerResolver;
        private IContentHandlerPipeline _contentHandlerPipeline;

        #endregion Fields

        #region Tests

        [SetUp]
        public void Setup()
        {
            _handlerResolver = new Mock<IHandlerResolver>();
            _contentHandlerPipeline = new ContentHandlerPipeline(_handlerResolver.Object);
        }

        [Test]
        public void Raise_THandlerArgs_NoHanldersMatched()
        {
            // Arrange
            _handlerResolver
               .Setup(x => x.ResolverAll<IHandler<HandlerArgs<EPiServerModel, ViewModel>>>())
               .Returns(Enumerable.Empty<IHandler<HandlerArgs<EPiServerModel, ViewModel>>>());

            // Act
            bool result = _contentHandlerPipeline.Raise(new HandlerArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.False);
            _handlerResolver.Verify(x => x.ResolverAll<IHandler<HandlerArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Handler resolver was one called");
        }

        [Test]
        public void Raise_THandlerArgs_HandlersResolvedAndInvoked()
        {
            Mock<IHandler<HandlerArgs<EPiServerModel, ViewModel>>> handler1 = new Mock<IHandler<HandlerArgs<EPiServerModel, ViewModel>>>();
            Mock<IHandler<HandlerArgs<EPiServerModel, ViewModel>>> handler2 = new Mock<IHandler<HandlerArgs<EPiServerModel, ViewModel>>>();

            // Arrange
            _handlerResolver
                .Setup(x => x.ResolverAll<IHandler<HandlerArgs<EPiServerModel, ViewModel>>>())
                .Returns(new List<IHandler<HandlerArgs<EPiServerModel, ViewModel>>>()
                             {
                                 handler1.Object,
                                 handler2.Object
                             });

            // Act
            bool result = _contentHandlerPipeline.Raise(new HandlerArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.True);
            _handlerResolver.Verify(x => x.ResolverAll<IHandler<HandlerArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Handler resolver was one called");
            handler1.Verify(x => x.Handle(It.IsAny<HandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
            handler2.Verify(x => x.Handle(It.IsAny<HandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
        }

        [Test]
        public async void RaiseAsync_THandlerArgs_NoHanldersMatched()
        {
            // Arrange
            _handlerResolver
               .Setup(x => x.ResolverAll<IHandlerAsync<HandlerArgs<EPiServerModel, ViewModel>>>())
               .Returns(Enumerable.Empty<IHandlerAsync<HandlerArgs<EPiServerModel, ViewModel>>>());

            // Act
            bool result = await _contentHandlerPipeline.RaiseAsync(new HandlerArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.False);
            _handlerResolver.Verify(x => x.ResolverAll<IHandlerAsync<HandlerArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Handler resolver was one called");
        }

        [Test]
        public async void RaiseAsync_THandlerArgs_HandlersResolvedAndInvoked()
        {
            Mock<IHandlerAsync<HandlerArgs<EPiServerModel, ViewModel>>> handler1 = new Mock<IHandlerAsync<HandlerArgs<EPiServerModel, ViewModel>>>();
            Mock<IHandlerAsync<HandlerArgs<EPiServerModel, ViewModel>>> handler2 = new Mock<IHandlerAsync<HandlerArgs<EPiServerModel, ViewModel>>>();

            // Arrange
            _handlerResolver
                .Setup(x => x.ResolverAll<IHandlerAsync<HandlerArgs<EPiServerModel, ViewModel>>>())
                .Returns(new List<IHandlerAsync<HandlerArgs<EPiServerModel, ViewModel>>>()
                             {
                                 handler1.Object,
                                 handler2.Object
                             });

            // Act
            bool result = await _contentHandlerPipeline.RaiseAsync(new HandlerArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.True);
            _handlerResolver.Verify(x => x.ResolverAll<IHandlerAsync<HandlerArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Handler resolver was one called");
            handler1.Verify(x => x.HandleAsync(It.IsAny<HandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
            handler2.Verify(x => x.HandleAsync(It.IsAny<HandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
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