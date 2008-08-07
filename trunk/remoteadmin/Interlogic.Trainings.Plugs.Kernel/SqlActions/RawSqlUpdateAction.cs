using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public class RawSqlUpdateAction : RawSqlExecuteNonQueryAction
	{
		protected override string GetExecutionSql()
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
