using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public class RawSqlSetAndGoAction:RawSqlExecuteNonQueryAction
	{
		private string _commandText;

		public string CommandText
		{
			get { return _commandText; }
			set { _commandText = value; }
		}

		protected override string GetExecutionSql()
		{
			return this.CommandText;
		}
	}
}
