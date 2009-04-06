using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public interface IInstantiatable
	{
		int InstanceId { get; set; }
		string InstanceName { get; set; }
		void Setup(Instance dbInstance, ITransactionContext context);
	}
}
