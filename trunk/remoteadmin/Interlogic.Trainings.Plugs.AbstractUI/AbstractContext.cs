using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public class AbstractContext : AbstractContainer, IAbstractUiContext
	{
		#region IAbstractUiContext Members
		private ITransactionContext _transactionContext;
		public ITransactionContext TransactionContext
		{
			get { return _transactionContext; }
		}

		#endregion

		public override void Setup(Instance dbInstance, ITransactionContext context)
		{
			base.Setup(dbInstance, context);
			this._transactionContext = context;
		}
	}
}
