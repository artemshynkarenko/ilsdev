using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public class AbstractContextController:InstanceController
	{
		public AbstractContextController(ITransactionContext context)
			: base(context)
		{
		}

		public AbstractContext GetByName(string uiName)
		{
			AbstractContext context = this.GetObjectByInstanceName(uiName) as AbstractContext;
			return context;
		}

	}
}
