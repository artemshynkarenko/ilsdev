using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public abstract class SqlAction : ISqlAction
	{
        public static readonly string CommandDelimiter = Environment.NewLine + "GO" + Environment.NewLine;
		private ISqlTransactionContext _transactionContext = null;

		public virtual ISqlTransactionContext TransactionContext
		{
			get { return _transactionContext; }
			set { _transactionContext = value; }
		}

		protected virtual IDbCommand PrepareCommand()
		{
			IDbCommand command = this.TransactionContext.Connection.CreateCommand();
			if (this.TransactionContext.ExecutingInTransaction)
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

			if (this.TransactionContext.Connection.State != ConnectionState.Open)
			{
				this.TransactionContext.Connection.Open();
			}
			IDbCommand command = PrepareCommand();
			this.ExecuteCommand(command);
		}

		#region ITransactionAction Members

		ITransactionContext ITransactionAction.TransactionContext
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

        #region ITransactionContext Members

        public bool ExecutingInTransaction
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public void BeginTransaction()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Commit()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RollBack()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
