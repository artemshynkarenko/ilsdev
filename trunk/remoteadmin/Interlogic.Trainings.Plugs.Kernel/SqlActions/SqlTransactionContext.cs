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
		private bool _connectionWasOpened = false;
		public virtual IDbConnection Connection
		{
			get { return _connection; }
			set { 
				_connection = value;
				_connectionWasOpened = _connection.State == ConnectionState.Open;
			}
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
			if (this.Connection.State != ConnectionState.Open)
				this.Connection.Open();
			this.Transaction = this.Connection.BeginTransaction();
		}

		public void Commit()
		{
			if (!this.ExecutingInTransaction)
				throw new InvalidOperationException("You should start transaction before commiting it");
			this.Transaction.Commit();
			if (!this._connectionWasOpened)
				this.Connection.Close();
		}

		public void RollBack()
		{
			if (!this.ExecutingInTransaction)
                throw new InvalidOperationException("You should start transaction before rolling it back");
			this.Transaction.Rollback();
			if (!this._connectionWasOpened)
				this.Connection.Close();
		}

		#endregion
	}
}
