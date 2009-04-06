using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public class AbstractPanelController: AbstractContainerController
	{
		public AbstractPanelController(ITransactionContext context)
			: base(context)
		{
		}
	}
}
