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

        private Mock<IContentHandlerResolver> _handlerResolver;
        private IContentHandlerPipeline _contentHandlerPipeline;

        #endregion Fields

        #region Tests

        [SetUp]
        public void Setup()
        {
            _handlerResolver = new Mock<IContentHandlerResolver>();
            _contentHandlerPipeline = new ContentHandlerPipeline(_handlerResolver.Object);
        }

        [Test]
        public void Raise_TContentHandlerArgs_NoHanldersMatched()
        {
            // Arrange
            _handlerResolver
               .Setup(x => x.ResolverAll<IContentHandler<ContentHandlerArgs<EPiServerModel, ViewModel>>>())
               .Returns(Enumerable.Empty<IContentHandler<ContentHandlerArgs<EPiServerModel, ViewModel>>>());

            // Act
            bool result = _contentHandlerPipeline.Raise(new ContentHandlerArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.False);
            _handlerResolver.Verify(x => x.ResolverAll<IContentHandler<ContentHandlerArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Handler resolver was one called");
        }

        [Test]
        public void Raise_TContentHandlerArgs_HandlersResolvedAndInvoked()
        {
            Mock<IContentHandler<ContentHandlerArgs<EPiServerModel, ViewModel>>> handler1 = new Mock<IContentHandler<ContentHandlerArgs<EPiServerModel, ViewModel>>>();
            Mock<IContentHandler<ContentHandlerArgs<EPiServerModel, ViewModel>>> handler2 = new Mock<IContentHandler<ContentHandlerArgs<EPiServerModel, ViewModel>>>();

            // Arrange
            _handlerResolver
                .Setup(x => x.ResolverAll<IContentHandler<ContentHandlerArgs<EPiServerModel, ViewModel>>>())
                .Returns(new List<IContentHandler<ContentHandlerArgs<EPiServerModel, ViewModel>>>()
                             {
                                 handler1.Object,
                                 handler2.Object
                             });

            // Act
            bool result = _contentHandlerPipeline.Raise(new ContentHandlerArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.True);
            _handlerResolver.Verify(x => x.ResolverAll<IContentHandler<ContentHandlerArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Handler resolver was one called");
            handler1.Verify(x => x.Handle(It.IsAny<ContentHandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
            handler2.Verify(x => x.Handle(It.IsAny<ContentHandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
        }

        [Test]
        public async void RaiseAsync_TContentHandlerArgs_NoHanldersMatched()
        {
            // Arrange
            _handlerResolver
               .Setup(x => x.ResolverAll<IContentHandlerAsync<ContentHandlerArgs<EPiServerModel, ViewModel>>>())
               .Returns(Enumerable.Empty<IContentHandlerAsync<ContentHandlerArgs<EPiServerModel, ViewModel>>>());

            // Act
            bool result = await _contentHandlerPipeline.RaiseAsync(new ContentHandlerArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.False);
            _handlerResolver.Verify(x => x.ResolverAll<IContentHandlerAsync<ContentHandlerArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Handler resolver was one called");
        }

        [Test]
        public async void RaiseAsync_TContentHandlerArgs_HandlersResolvedAndInvoked()
        {
            Mock<IContentHandlerAsync<ContentHandlerArgs<EPiServerModel, ViewModel>>> handler1 = new Mock<IContentHandlerAsync<ContentHandlerArgs<EPiServerModel, ViewModel>>>();
            Mock<IContentHandlerAsync<ContentHandlerArgs<EPiServerModel, ViewModel>>> handler2 = new Mock<IContentHandlerAsync<ContentHandlerArgs<EPiServerModel, ViewModel>>>();

            // Arrange
            _handlerResolver
                .Setup(x => x.ResolverAll<IContentHandlerAsync<ContentHandlerArgs<EPiServerModel, ViewModel>>>())
                .Returns(new List<IContentHandlerAsync<ContentHandlerArgs<EPiServerModel, ViewModel>>>()
                             {
                                 handler1.Object,
                                 handler2.Object
                             });

            // Act
            bool result = await _contentHandlerPipeline.RaiseAsync(new ContentHandlerArgs<EPiServerModel, ViewModel>(new EPiServerModel(), new ViewModel()));

            // Assert
            Assert.That(result, Is.True);
            _handlerResolver.Verify(x => x.ResolverAll<IContentHandlerAsync<ContentHandlerArgs<EPiServerModel, ViewModel>>>(), Times.Once, "Handler resolver was one called");
            handler1.Verify(x => x.HandleAsync(It.IsAny<ContentHandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
            handler2.Verify(x => x.HandleAsync(It.IsAny<ContentHandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
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