# Introduction #

As a _kernel_ of our system we use _abstract plug-in model_. At current stage it is only starting - so any comments with be **highly** appreciated.


# Details #

Main entity is plug-in(**[Plug](http://code.google.com/p/ilsdev/wiki/Interlogic.Trainings.Plugs.Kernel.Plug)**). Each plug-in define it's own interfaces(**[BindingPoints](http://code.google.com/p/ilsdev/wiki/Interlogic.Trainings.Plugs.Kernel.BindingPoint)**) for other plug-ins to connect and binds(**[Bindings](http://code.google.com/p/ilsdev/wiki/Interlogic.Trainings.Plugs.Kernel.BindingPoint)**) its implementation through foreign interfaces to existing plugs. As a store for this system we will use database.

On registering by **[PluginInstaller](http://code.google.com/p/ilsdev/wiki/Interlogic.Trainings.Plugs.Kernel.PluginInstaller)** plug files (**[PlugFiles](http://code.google.com/p/ilsdev/wiki/Interlogic.Trainings.Plugs.Kernel.PlugFiles)**) are copied to requested locations(**[PlugLocation](http://code.google.com/p/ilsdev/wiki/Interlogic.Trainings.Plugs.Kernel.PlugLocation)**), its own interfaces and implementations (**[ClassDefinition](http://code.google.com/p/ilsdev/wiki/Interlogic.Trainings.Plugs.Kernel.ClassDefinition)**) are installed and links ( defining own pluggable points(**[BindingPoints](http://code.google.com/p/ilsdev/wiki/Interlogic.Trainings.Plugs.Kernel.BindingPoint)**) or bindings to existing(**[Bindings](http://code.google.com/p/ilsdev/wiki/Interlogic.Trainings.Plugs.Kernel.BindingPoint)**) are created. Custom actions can be executed using **[IRegisterAction](http://code.google.com/p/ilsdev/wiki/Interlogic.Trainings.Plugs.Kernel.IRegisterAction)**.

On Update of existing plug information on links is updated and custom actions (**[IUpdateAction](http://code.google.com/p/ilsdev/wiki/Interlogic.Trainings.Plugs.Kernel.IUpdateAction)**) are executed.

On Unregistering existing plug all plug data and it's relation are removed and custom actions (**[IUnregisterAction](http://code.google.com/p/ilsdev/wiki/Interlogic.Trainings.Plugs.Kernel.IUnregisterAction)**)

See also: [Action Model](http://code.google.com/p/ilsdev/wiki/ActionModel), [Abstract UI model](http://code.google.com/p/ilsdev/wiki/AbstractUIModel)