using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public interface IAbstractUIContext:IAbstractContainer
	{
		ITransactionContext TransactionContext { get;}
	}
}
