# BuildUp

**NOTE: This Documentation is not complete and the examples have not been compiled yet**

Build up is a view model building framwork. It's main aim is to facilitate the modular, decoupled, build up of view models to promote DRY and SOLID practices.

The basic underlying idea is that each view model is built, and has its value properites populated, by one class known as a "Content Handler". If the view model exists as a property of another view model. The content handler building the outer view model will invoke the content handler for the property view model.

This framework is ideally suited to CMS driven website, where a lot of code is written to build view model object graphs from content models delivered by the CMS framework (e.g. EPiServer code first content models)

TODO: [Example project here]

## Overview

The diagram below shows a logical overview of the BuildUp framework 
![alt text][overview]


## Details

The key concepts in the diagram above are the ViewModelBuilder and ContentHandlers (or view model builder components). 

The view model builder class itself performs two main jobs:

1. Create the view model
2. Map the content model properties directly to the view model properties (call to auto mapper)

If additional logic is needs to populate the data in a view model, handlers are then invoked to perform that work. Each content handler has a specific job and together they build-up the view model. All presentation logic (including form validation) should be in these handlers or in domain logic.

Content Handlers do not inherit from other handlers but they can invoke other handlers to build up the current view model (composition over inheritance).

There are two patterns for invoking handlers:

1. Multiple Invocations – More than one handler works on the same view model invoked by the pipeline. Only used for building up elements of a page e.g. Header, footer etc.
2. Orchestration - Handlers call other handlers to build complex properties or inherited properties. Mostly used pattern, if a model is has properties that are types of other view models it delegates the building of that property to another handler

To de-couple handlers from each other there is a simple pipeline (ContentHandlerPipeline) that is responsible for resolving the required handler(s) from the IOC container. Handlers are registered to their handler interfaces in the IOC container (the handler interface can be seen as the handler’s signature). The pipeline provides a simple event framework or Pub \ Sub pattern.

![alt text][sequence]

## Getting Started

### Implementing IContentHandlerResolver

Before the BuildUp framework can be used the IContentHandlerResolver must be added to the project and injected into the IContentHandlerPipeline (injection should be via IOC container). A basic implementation of IContentHandlerResolver would be

```c#
    public class ContentHandlerResolver : IContentHandlerResolver
    {
        public IEnumerable<THandles> ResolverAll<THandles>() where THandles : IHandler
        {
            // MVC default dependency resolver (this could equally be a call to the Unity / StructureMap / Ninject container)
            return DependencyResolver.Current.GetServices<THandles>();
        }
    }
```

Creat an implementation of the Property Mapper Interface

``` c#

    public class PropertyMapper : IPropertyMapper
    {
        public void Map<TFrom, TTo>(TFrom @from, TTo to)
        {
            Mapper.Map(from, to);
        }
    }

```

Register the BuildUp class in the IOC container (StructureMap example below)

```c#

  container.For<BuildUp.IPropertyMapper>().Use<PropertyMapper>();
  container.For<BuildUp.IContentHandlerResolver>().Use<ContentHandlerResolver>();
  container.For<BuildUp.IContentHandlerPipeline>().Use<BuildUp.ContentHandlerPipeline>();
  container.For<BuildUp.IViewModelBuilder>().Use<BuildUp.ViewModelBuilder>();

```

### Register the content handlers

The basic premise in for content handlers is that they are simply resolved by the "content handler" interface that they implement. The content handler interface can be seen as the handler signature. To allow content handlers to be invoked (raised) for a given type of signature the content handlers need to be registered to their interface signatures in the IOC container.

Example handler

```c#
    public class ASpecialContentHandler<TCmsModel, TViewModel> : IContentHandler<HandlerArgs<TCmsModel, TViewModel>>
        where TCmsModel : AContentModel
        where TViewModel : AViewModel
    {
        public void Handle(ContentHandlerArgs<TCmsModel, TViewModel> args)
        {
            // the "_propertyMapper" class below will simply be a wrapper of automapper
            _propertyMapper.Map<TCmsModel, TViewModel>(args.From, args.To);
            
            // do other stuff
        }
    }
```

Example registration in structure map

```c#

    ConfigurationExpression.For<IContentHandler<HandlerArgs<AContentModel, AViewModel>>>()
                .Use<ASpecialContentHandler<AContentModel, AViewModel>>()
                .Named("ASpecialContentHandler");

```

## Advanced Registrations

The above registration can be simplied using a fluent API wrapping the StructureMap ConfigurationExpression class

```c#

    public class FluentHanderRegistration<TDomain, TViewModel>
    {
        protected readonly ConfigurationExpression ConfigurationExpression;

        public FluentHanderRegistration(ConfigurationExpression configurationExpression)
        {
            ConfigurationExpression = configurationExpression;
        }

        public ConfigurationExpression IocConainer()
        {
            return ConfigurationExpression;
        }

        public FluentHanderRegistration<TDomain, TViewModel> AndRegister<TContentHandler>(string ContentHandlerName)
            where TContentHandler : IContentHandler<ContentHandlerArgs<TDomain, TViewModel>>
        {
            return Register<TContentHandler>(contentHandlerName);
        }

        public FluentHanderRegistration<TDomain, TViewModel> Register<TContentHandler>(string contentHandlerName)
            where TContentHandler : IContentHandler<ContentHandlerArgs<TDomain, TViewModel>>
        {
            ConfigurationExpression.For<IContentHandler<ContentHandlerArgs<TDomain, TViewModel>>>()
                .Use<TContentHandler>()
                .Named(contentHandlerName);

            return this;
        }
    }

```

The fluent API can be wrapped in a extension method.

```c#
    public static class ConfigurationExpressionExtensions
    {
      public static FluentHanderRegistration<TDomain, TViewModel> RegisterContentHandler<TDomain, TViewModel, TContentHandler>(this ConfigurationExpression container, string contentHandlerName)
            where TContentHandler : IContentHandler<ContentHandlerArgs<TDomain, TViewModel>>
        {
            // Create an automapper mapping for this domain to view model
            Mapper.CreateMap<TDomain, TViewModel>();

            var accumulator = new FluentHanderRegistration<TDomain, TViewModel>(container);

            return accumulator.Register<TContentHandler>(contentHandlerName);
        }
    }
```

Extension method invoked as follows

```c#

container.RegisterContentHandler<AContentModel, AViewModel, ASpecialContentHandler>("ASpecialContentHandler");

```

The fluent API becomes particularly helpful if you want to use the "Multiple Invocations" pattern for building up a Page. Have a look at the "RegisterForPage" extension method in the example below:

```c#

    public static class ConfigurationExpressionExtensions
    {
        /// <summary>
        /// Registers a page domain model and page view model in the StructureMap DI container.
        /// The domain model will be mapped onto the view model using the default page handlers.
        /// This method is used for pages which don't require a handler; either no additional mapping
        /// logic is required, or mapping has already been handled by a previous registration.
        /// </summary>
        /// <typeparam name="TDomain">Page domain model type</typeparam>
        /// <typeparam name="TViewModel">Page view model type</typeparam>
        /// <param name="container">StructureMap DI container></param>
        /// <returns>Handler registration for chaining with subsequent registrations</returns>
        public static FluentHanderRegistration<TDomain, TViewModel> RegisterForPage<TDomain, TViewModel>(this ConfigurationExpression container)
            where TDomain : SitePage
            where TViewModel : PageViewModel
        {
            return
                container.RegisterContentHandler<TDomain, TViewModel, MetadataComponentHandler<TDomain, TViewModel>>("MetadataHandler")
                    .AndRegister<PageContextHandler<TDomain, TViewModel>>("PageContextHandler")
                    .AndRegister<PageNavigationHandler<TDomain, TViewModel>>("PageNavigationHandler")
                    .AndRegister<ContactDetailsComponentHandler<TDomain, TViewModel>>("ContactDetailsHandler")
                    .AndRegister<NewsFlashHandler<TDomain, TViewModel>>("NewsFlashHandler")
                    .AndRegister<CookiePolicyComponentHandler<TDomain, TViewModel>>("CookiePolicyComponentHandler");
        }

        /// <summary>
        /// Registers a page handler in the StructureMap DI container.
        /// The handler maps a page domain model onto a page view model.
        /// </summary>
        /// <typeparam name="TDomain">Page domain model type</typeparam>
        /// <typeparam name="TViewModel">Page view model type</typeparam>
        /// <typeparam name="THandler">Handler type</typeparam>
        /// <param name="container">StructureMap DI container></param>
        /// <param name="contentHandlerName">Name of the handler to register</param>
        /// <returns>Handler registration for chaining with subsequent registrations</returns>
        public static FluentHanderRegistration<TDomain, TViewModel> RegisterForPage<TDomain, TViewModel, TContentHandler>(this ConfigurationExpression container, string contentHandlerName)
            where TDomain : SitePage
            where TViewModel : PageViewModel
            where TContentHandler : IContentHandler<HandlerArgs<TDomain, TViewModel>>
        {
            return
                container.RegisterForPage<TDomain, TViewModel>()
                         .AndRegister<TContentHandler>(contentHandlerName);
        }

        /// <summary>
        /// Registers a handler in the StructureMap DI container.
        /// The handler maps a domain model onto a view model using no additional data.
        /// </summary>
        /// <typeparam name="TDomain">Domain model type</typeparam>
        /// <typeparam name="TViewModel">View model type</typeparam>
        /// <typeparam name="TContentHandler">Handler type</typeparam>
        /// <param name="container">StructureMap DI container></param>
        /// <param name="contentHandlerName">Name of the handler to register</param>
        /// <returns>Handler registration for chaining with subsequent registrations</returns>
        public static FluentHanderRegistration<TDomain, TViewModel> RegisterHandler<TDomain, TViewModel, TContentHandler>(this ConfigurationExpression container, string contentHandlerName)
            where TContentHandler : IContentHandler<HandlerArgs<TDomain, TViewModel>>
        {
            // Create an automapper mapping for this domain to view model
            Mapper.CreateMap<TDomain, TViewModel>();

            var accumulator = new FluentHanderRegistration<TDomain, TViewModel>(container);

            return accumulator.Register<TContentHandler>(contentHandlerName);
        }

        /// <summary>
        /// Registers a handler in the StructureMap DI container.
        /// The handler maps a domain model onto a view model using additional data of type TData.
        /// </summary>
        /// <typeparam name="TData">Data type</typeparam>
        /// <typeparam name="TDomain">Domain model type</typeparam>
        /// <typeparam name="TViewModel">View model type</typeparam>
        /// <typeparam name="TContentHandler">Handler type</typeparam>
        /// <param name="container">StructureMap DI container></param>
        /// <param name="contentHandlerName">Name of the handler to register</param>
        /// <returns>Handler registration for chaining with subsequent registrations</returns>
        public static FluentHanderRegistration<TData, TDomain, TViewModel> RegisterDataHandler<TData, TDomain, TViewModel, TContentHandler>(this ConfigurationExpression container, string contentHandlerName)
            where TContentHandler : IContentHandler<ContentHandlerArgs<TData, TDomain, TViewModel>>
        {
            // Create an automapper mapping for this domain to view model
            Mapper.CreateMap<TDomain, TViewModel>();

            var accumulator = new FluentHanderRegistration<TData, TDomain, TViewModel>(container);

            return accumulator.RegisterData<TContentHandler>(contentHandlerName);
        }
    }

```

[overview]: https://github.com/paulkrwebs/BuildUp/blob/develop/Overview.png "Overview of framework"
[sequence]: https://github.com/paulkrwebs/BuildUp/blob/develop/Sequence%20Diagram.png "Sequence diagram"
