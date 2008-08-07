using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public abstract class RawSqlExecuteNonQueryAction:RawSqlAction
	{
		protected override void ExecuteCommand(System.Data.IDbCommand command)
		{
			string[] separateActions = null;
			if (command.CommandText.Contains("GO"))
			{
				separateActions = command.CommandText.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
			}
			else
				separateActions = new string[] { command.CommandText };
			foreach (string commandText in separateActions)
			{
				command.CommandText = commandText;
				command.ExecuteNonQuery();
			}
		}
	}
}
