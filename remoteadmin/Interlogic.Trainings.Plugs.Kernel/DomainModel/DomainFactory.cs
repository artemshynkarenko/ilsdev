using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;

namespace Interlogic.Trainings.Plugs.Kernel.DomainModel
{
	public abstract class DomainFactory:IDisposable
	{
		public abstract void InstallRequiredEnvironment(ISqlTransactionContext context);
		public abstract void UpdateRequiredEnvironment(ISqlTransactionContext context);
		public abstract void UninstallRequiredEnvironment(ISqlTransactionContext context);

		private ISqlTransactionContext _context;

		public ISqlTransactionContext Context
		{
			get { return _context;}
			set { _context = value;}
		}

		protected virtual void Insert(ISqlAction action)
		{
			ExecuteCommand(action);
			if (Inserted != null)
				Inserted( this, EventArgs.Empty);
		}

		public event EventHandler Inserted;

		protected virtual void Update(ISqlAction action)
		{
			if (this.Updating != null)
				Updating(this, EventArgs.Empty);
			ExecuteCommand(action);
			if (this.Updated != null)
				Updated(this, EventArgs.Empty);
		}

		public event EventHandler Updating;
		public event EventHandler Updated;


		protected virtual void Delete(ISqlAction action)
		{
			if (this.Deleting != null)
				Deleting(this, EventArgs.Empty);
			ExecuteCommand(action);
		}

		public event EventHandler Deleting;


		protected virtual void LoadAll(ISqlAction action)
		{
			ExecuteCommand(action);
			if (this.LoadedAll != null)
				LoadedAll(this, EventArgs.Empty);
		}

		public event EventHandler LoadedAll;


		protected virtual void LoadByPrimaryKey(ISqlAction action)
		{
			ExecuteCommand(action);
			if (this.LoadedByPrimaryKey != null)
				LoadedByPrimaryKey(this, EventArgs.Empty);
		}

		public event EventHandler LoadedByPrimaryKey;


		protected virtual void ExecuteCommand(ISqlAction action)
		{
			if (this.Context != null)
				action.TransactionContext = this.Context;
			action.Execute();
		}


		#region IDisposable Members

		public void Dispose()
		{
			if (this.Context != null)
			{
				this.Context.Connection.Close();
			}
		}
		#endregion
	}
}
