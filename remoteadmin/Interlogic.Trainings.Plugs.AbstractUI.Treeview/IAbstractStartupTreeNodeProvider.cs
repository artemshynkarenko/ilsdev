using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.AbstractUI.TreeView;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public interface IAbstractStartupTreeNodeProvider:INavigationListenerComponent
	{
		List<AbstractTreeNode> GetStartTreeNodes();
	}
}
