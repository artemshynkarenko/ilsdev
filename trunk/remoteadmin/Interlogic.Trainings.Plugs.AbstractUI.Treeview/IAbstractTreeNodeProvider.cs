using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.AbstractUI.TreeView;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public interface IAbstractTreeNodeProvider : INavigationListenerComponent
	{
		List<AbstractTreeNode> GetTreeNodes(AbstractTreeNode parentNode);
		bool CheckTreeNodesExists(AbstractTreeNode parentNode);
	}
}
