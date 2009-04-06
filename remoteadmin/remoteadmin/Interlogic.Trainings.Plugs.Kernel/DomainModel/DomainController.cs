using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;

namespace Interlogic.Trainings.Plugs.Kernel.DomainModel
{
	public abstract class DomainController
	{
		private ITransactionContext _context;

		public DomainController(ITransactionContext context)
		{
			_context = context;
		}

		protected ISqlTransactionContext FactoryContext
		{
			get { return (ISqlTransactionContext)_context; }
		}
	}
}
