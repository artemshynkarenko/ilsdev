using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.AbstractUI;

namespace Interlogic.Trainings.Plugs.RootContent
{
	public class RootContentTreeNodeProvider : IAbstractTreeNodeProvider, IAbstractStartupTreeNodeProvider
	{
		#region IAbstractTreeNodeProvider Members

		public List<AbstractTreeNode> GetTreeNodes(AbstractTreeNode parentNode)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public bool CheckTreeNodesExists(AbstractTreeNode parentNode)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion

		#region IInstantiatable Members

		public void Setup(Interlogic.Trainings.Plugs.Kernel.Instance dbInstance, Interlogic.Trainings.Plugs.Kernel.ITransactionContext context)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
