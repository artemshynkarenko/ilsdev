using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public class AbstractTreeView:AbstractNavigationComponent
	{

		public override Interlogic.Trainings.Plugs.Kernel.DomainModel.DomainObject SelectedObject
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public override IAbstractUiComponent SelectedComponent
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
	}
}
