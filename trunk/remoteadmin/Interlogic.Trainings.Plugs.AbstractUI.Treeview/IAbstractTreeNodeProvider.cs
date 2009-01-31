using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public interface IAbstractTreeNodeProvider : IInstantiatable
	{
		List<AbstractTreeNode> GetTreeNodes(AbstractTreeNode parentNode);
		bool CheckTreeNodesExists(AbstractTreeNode parentNode);
	}
}
