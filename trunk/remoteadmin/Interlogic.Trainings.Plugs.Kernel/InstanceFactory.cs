using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class InstanceFactory : DomainModel.DomainFactory
	{
		internal static InstanceFactory GetInstance()
		{
			return new InstanceFactory();
		}

		protected InstanceFactory()
			: base()
		{
		}

		#region Registration
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
		#endregion
	}
}
