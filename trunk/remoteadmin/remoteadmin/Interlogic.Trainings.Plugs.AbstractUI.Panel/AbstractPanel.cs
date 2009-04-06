using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
    public class AbstractPanel : AbstractContainer
	{
		protected override DomainController GetControllerInstance(ITransactionContext context)
		{
			return new AbstractPanelController(context);
		}
	}
}
