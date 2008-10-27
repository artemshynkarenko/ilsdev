using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public interface IInstantiatable
	{
		void Setup(Instance dbInstance, ITransactionContext context);
	}
}
