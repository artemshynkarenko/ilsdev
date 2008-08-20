using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class PlugLocationFactory:DomainFactory
	{
		public override void InstallRequiredEnvironment(Interlogic.Trainings.Plugs.Kernel.SqlActions.ISqlTransactionContext context)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void UpdateRequiredEnvironment(Interlogic.Trainings.Plugs.Kernel.SqlActions.ISqlTransactionContext context)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void UninstallRequiredEnvironment(Interlogic.Trainings.Plugs.Kernel.SqlActions.ISqlTransactionContext context)
		{
			throw new Exception("The method or operation is not implemented.");
		}

        internal void InternalInsert(PlugLocation location)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        internal void InternalUpdate(PlugLocation location)
        {
            throw new Exception("The method or operation is not implemented.");
        }

		internal static PlugLocationFactory GetInstance()
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
