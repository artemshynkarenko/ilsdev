using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public interface ITrasanctionContext
	{
		bool ExecutingInTranaction { get;}
		void BeginTransaction();
		void Commit();
		void RollBack();
	}
}
