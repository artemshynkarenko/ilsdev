using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public interface ITransactionAction:IAction
	{
		ITrasanctionContext TransactionContext { get;set;}
	}
}
