using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public class AbstractContext : AbstractContainer, IAbstractContext
	{
		#region IAbstractUiContext Members
		private ITransactionContext _transactionContext;
		public ITransactionContext TransactionContext
		{
			get { return _transactionContext; }
		}

		#endregion

		public override IAbstractUiContainer ParentComponent
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException("You can not set this property in  AbstractContext");
			}
		}

		public override IAbstractContext Context
		{
			get
			{
				return this;
			}
			set
			{
				throw new InvalidOperationException("You can not set this property in  AbstractContext");
			}
		}

		protected override DomainController GetControllerInstance(ITransactionContext context)
		{
			return new AbstractContextController(context);
		}

		public override void Setup(Instance dbInstance, ITransactionContext context)
		{
			this._transactionContext = context;
			base.Setup(dbInstance, context);
		}
	}
}
