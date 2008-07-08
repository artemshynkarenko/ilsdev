using System;
using System.Data;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public interface ISqlTransactionContext:ITrasanctionContext
	{
		IDbConnection Connection { get;}
		IDbTransaction CurrentTransaction { get;}
	}
}
