using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public interface IAbstractContext:IAbstractUiContainer
	{
		ITransactionContext TransactionContext { get;}
	}
}
