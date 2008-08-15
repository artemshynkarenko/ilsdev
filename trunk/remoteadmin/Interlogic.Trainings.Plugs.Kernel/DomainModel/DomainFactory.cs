using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Data;

namespace Interlogic.Trainings.Plugs.Kernel.DomainModel
{
	public abstract class DomainFactory:IDisposable
	{
		public abstract void InstallRequiredEnvironment(ISqlTransactionContext context);
		public abstract void UpdateRequiredEnvironment(ISqlTransactionContext context);
		public abstract void UninstallRequiredEnvironment(ISqlTransactionContext context);

		
		private ISqlTransactionContext _context;
		private bool _contextWasOpened = false;
		public ISqlTransactionContext Context
		{
			get { return _context;}
			set { 
				_context = value;
				_contextWasOpened = _context.Connection.State == ConnectionState.Open;
				}
		}

		protected virtual void Insert(ISqlAction action, DomainObject domainObject)
		{
			ExecuteCommand(action);
			if (Inserted != null)
				Inserted( this, new DomainFactoryEventArgs(domainObject, action));
		}

		public event EventHandler<DomainFactoryEventArgs> Inserted;

		protected virtual void Update(ISqlAction action, DomainObject domainObject)
		{
			if (this.Updating != null)
				Updating(this, new DomainFactoryEventArgs(domainObject, action));
			ExecuteCommand(action);
			if (this.Updated != null)
				Updated(this, new DomainFactoryEventArgs(domainObject, action));
		}

		public event EventHandler<DomainFactoryEventArgs> Updating;
		public event EventHandler<DomainFactoryEventArgs> Updated;


		protected virtual void Delete(ISqlAction action, DomainObject domainObject)
		{
			if (this.Deleting != null)
				Deleting(this, new DomainFactoryEventArgs(domainObject, action));
			ExecuteCommand(action);
		}

		public event EventHandler<DomainFactoryEventArgs> Deleting;


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
			if (this.Context != null && this._contextWasOpened)
			{
				this.Context.Connection.Close();
			}
		}
		#endregion
	}
}
