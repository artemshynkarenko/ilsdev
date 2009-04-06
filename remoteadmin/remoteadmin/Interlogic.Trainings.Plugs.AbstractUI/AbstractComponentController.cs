using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public abstract class AbstractComponentController:InstanceController
	{
		public AbstractComponentController(ITransactionContext context)
			: base(context)
		{
		}
	}
}
