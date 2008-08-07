using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public abstract class RawSqlAction:SqlAction
	{
		protected abstract string GetExecutionSql();

		protected virtual void SetParameters(IDbCommand command)
		{
		}
		
		protected override IDbCommand PrepareCommand()
		{
			IDbCommand command = base.PrepareCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = this.GetExecutionSql();
			SetParameters(command);
			return command;
		}
	}
}
