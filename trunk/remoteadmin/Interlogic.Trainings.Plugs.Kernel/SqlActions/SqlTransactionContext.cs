using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public class SqlTransactionContext : ISqlTransactionContext
	{
		#region ISqlTransactionContext Members
		private IDbConnection _connection = null;
		public virtual IDbConnection Connection
		{
			get { return _connection; }
			set { _connection = value; }
		}
		private IDbTransaction _transaction = null;

		protected virtual IDbTransaction Transaction
		{
			get { return _transaction; }
			set { _transaction = value; }
		}

		public IDbTransaction CurrentTransaction
		{
			get { return this.Transaction; }
		}

		#endregion

		#region ITrasanctionContext Members

		public virtual bool ExecutingInTransaction
		{
			get { return this.CurrentTransaction != null; }
		}

		public virtual void BeginTransaction()
		{
			if (this.ExecutingInTransaction)
				throw new InvalidOperationException("You should finish previous transaction before creating new");
			this.Transaction = this.Connection.BeginTransaction();
		}

		public void Commit()
		{
			if (!this.ExecutingInTransaction)
				throw new InvalidOperationException("You should start transaction before comminitng it");
			this.Transaction.Commit();
		}

		public void RollBack()
		{
			if (!this.ExecutingInTransaction)
				throw new InvalidOperationException("You should start transaction before comminitng it");
			this.Transaction.Rollback();
		}

		#endregion
	}
}
