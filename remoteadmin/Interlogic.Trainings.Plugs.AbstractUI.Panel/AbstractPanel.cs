using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public class AbstractPanel:AbstractContainer
	{
		protected override DomainController GetControllerInstance(Interlogic.Trainings.Plugs.Kernel.ITransactionContext context)
		{
			return new AbstractPanelController(context);
		}
	}
}
