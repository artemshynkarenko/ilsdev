# Introduction #

Abstract UI (User Interface) - it is a set of classes that realize UI that is not binded to any environment. Using some translators it is translated to real UI elements( e.g. Windows Forms classes or Web controls). Each plug-in ( except real UI plug-ins) should operate only with it and registering of new plug usually take place in abstract UI plug binding point.


# Details #

For now agreed following set of abstract controls:
  * Treeview
  * Grid
  * ContextMenu
  * SingleItemInfo
  * ToolBar