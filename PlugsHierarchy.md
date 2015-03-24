# Introduction #

Our design will include several plugs that will create playground for future implementations. Here we will mention all of them (**so this page should be updated !**) and make link to detailed explanation of each plug.


# Details #

**Kernel** plug-in - contain basic Dbdesign, common SqlActions, FileActions, Logger, Cache, Installer etc. - all that is needed by any other plugin.

**Abstract UI** plug-in - contains basic abstract UI components, and defines _StartUp_ entry point for plugins that will be shown on our UI at start and all the rest entrypoint that are needed by our components.
Remark - if we want we can define plug that are not shown on our UI - but define some other structures - that is why we create both **Kernel** and **AbstractUI** plug.

**Web UI** plug-in - contains translators from AbstractUI components to web components and vice versa.

**Static nodes** plug-in ( maybe we should call it **Menu** or **Category** plugin?) - it will define entry points to all other plugs that will be shown on UI, and bind itself to AbstractUI as _StartUp_ plug. _OnTreeviewExpand_ node with show it's children.
_OnShowNodeContent_ ( selection node in treeview) - it will show list of underlying nodes, _OnContextMenu_ - it will show some actions that can be done with nodes. etc.

**Kernel presentation** plug-in - binds to **StaticNode** plug and AbstractUI plug ( alternative- bind to _StartUp_ binding point of AbstractUI ) and show content of our kernel tables and add possibilities to add/upodate/remove plugs to our system.

Also we need applications/installations that will execute Installers of this 4 plugs.
> And while testing of our kernel we need to create test Apps (console or windows) or NUnit tests.

Planned workflow - some plug-in on its registration creates StaticNode under existing and binds to AbstractUI entry points ( something like OnTreeviewExpand entrypoint, etc.) - to show it's own content on UI.

In case that we don't want our plug be show on UI ( why do we need that i realy don't know :)  - but maybe it will be used somewhere in future ) we don't bing it to static nodes and it just is registered in our DB.