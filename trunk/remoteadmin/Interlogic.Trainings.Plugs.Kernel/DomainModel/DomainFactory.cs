using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Data;

namespace Interlogic.Trainings.Plugs.Kernel.DomainModel
{
	public abstract class DomainFactory:IDisposable
	{
		public abstract void InstallRequiredEnvironment();
		public abstract void UpdateRequiredEnvironment();
		public abstract void UninstallRequiredEnvironment();

		
		private ISqlTransactionContext _context;
		private bool _contextWasOpened = false;
		public ISqlTransactionContext Context
		{
			get { return _context;}
			set 
            { 
				_context = value;
				_contextWasOpened = _context.Connection.State == ConnectionState.Open;
			}
		}

		protected virtual void ExecuteCommand(ISqlAction action)
		{
			if (this.Context != null)
				action.TransactionContext = this.Context;
			action.Execute();
		}


		#region IDisposable Members

		public void Dispose()
		{
			if (this.Context != null && !this._contextWasOpened)
			{
				this.Context.Connection.Close();
			}
		}
		#endregion
	}
}
