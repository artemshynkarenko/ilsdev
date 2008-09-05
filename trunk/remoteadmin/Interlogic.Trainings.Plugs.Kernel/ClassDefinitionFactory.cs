using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class ClassDefinitionFactory:DomainModel.DomainFactory
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

		internal void InternalInsert(ClassDefinition classDefinition)
		{
		}

		protected void Insert(ClassDefinition classDefinition)
		{
		}
	}
}
