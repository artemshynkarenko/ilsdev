using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public interface ITransactionContext
	{
		bool ExecutingInTransaction { get;}
		void BeginTransaction();
		void Commit();
		void RollBack();
	}
}
