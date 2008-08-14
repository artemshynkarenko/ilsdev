using System;
using System.Data;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public interface ISqlTransactionContext:ITransactionContext
	{
		IDbConnection Connection { get;}
		IDbTransaction CurrentTransaction { get;}
	}
}
