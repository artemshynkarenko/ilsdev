using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public interface ITransactionAction:IAction
	{
		ITransactionContext TransactionContext { get;set;}
	}
}
