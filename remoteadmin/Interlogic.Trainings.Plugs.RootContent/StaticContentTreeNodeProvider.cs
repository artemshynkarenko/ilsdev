using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.AbstractUI;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.RootContent
{
	public class StaticContentTreeNodeProvider : AbstractComponent, IAbstractTreeNodeProvider, IAbstractStartupTreeNodeProvider
	{

		#region IAbstractTreeNodeProvider Members

		public List<AbstractTreeNode> GetTreeNodes(AbstractTreeNode parentNode)
		{
			throw new NotImplementedException();
		}

		public bool CheckTreeNodesExists(AbstractTreeNode parentNode)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region INavigationListenerComponent Members

		public void OnNavigate(INavigationComponent navigator)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IAbstractStartupTreeNodeProvider Members

		public List<AbstractTreeNode> GetStartTreeNodes()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
