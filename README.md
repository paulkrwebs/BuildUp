# BuildUp

**NOTE: This Documentation is not complete needs to be re-written so its not EPiServer specific**

Build up is a view model building framwork. It's main aim is to facilitate the modular, decoupled, build up of view models to promote DRY and SOLID practices.

The basic underlying idea is that each view model is built, and has its value properites populated, by one class known as a "Component". If a view model exists as a property of another view model. A component building the outer view model will invoke the component(s) for the property view model and deligate all responsbility for building the property to that component(s).

This framework is ideally suited to CMS driven website, where a lot of code is written to build view model object graphs from content models delivered by the CMS framework (e.g. EPiServer code first content models)

TODO: [Example project here]

## Overview

The diagram below shows a logical overview of the BuildUp framework 
![alt text][overview]
