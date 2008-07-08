using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public abstract class SqlAction : ISqlAction
	{
		private ISqlTransactionContext _transactionContext = null;

		public virtual ISqlTransactionContext TransactionContext
		{
			get { return _transactionContext; }
			set { _transactionContext = value; }
		}

		protected virtual IDbCommand PrepareCommand()
		{
			IDbCommand command = this.TransactionContext.Connection.CreateCommand();
			if (this.TransactionContext.ExecutingInTranaction)
			{
				command.Transaction = this.TransactionContext.CurrentTransaction;
			}
			return command;
		}

		protected abstract void ExecuteCommand(IDbCommand command);

		public virtual void Execute()
		{
			if (this.TransactionContext == null)
				throw new InvalidOperationException("You should set TransactionContext before executing sql");
			IDbCommand command = PrepareCommand();
			this.ExecuteCommand(command);
		}

		#region ITransactionAction Members

		ITrasanctionContext ITransactionAction.TransactionContext
		{
			get
			{
				return this.TransactionContext;
			}
			set
			{
				this.TransactionContext = value as ISqlTransactionContext;
			}
		}

		#endregion
	}
}
