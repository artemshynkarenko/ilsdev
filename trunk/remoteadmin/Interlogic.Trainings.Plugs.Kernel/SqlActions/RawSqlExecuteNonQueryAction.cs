using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public class RawSqlExecuteNonQueryAction:RawSqlAction
	{
        protected string _commandText;

        public string CommandText
        {
            get { return _commandText; }
            set { _commandText = value; }
        }

        protected override string GetExecutionSql()
        {
            return this.CommandText;
        }
        
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
