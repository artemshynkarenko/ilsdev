using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public class RawSqlInsertAction : RawSqlExecuteNonQueryAction
	{
		private string _commandText;

		public string CommandText
		{
			get { return _commandText; }
			set { _commandText = value; }
		}

		protected override string GetExecutionSql()
		{
			string insert = this.CommandText;
			insert += Environment.NewLine + "GO" + Environment.NewLine;
			insert += "SELECT @InsertedIdentity = @@Identity";
			return insert;
		}

		protected override void SetParameters(System.Data.IDbCommand command)
		{
			base.SetParameters(command);
			_insertedIdentityParameter = command.CreateParameter();
			_insertedIdentityParameter.ParameterName = "@InsertedIdentity";
			_insertedIdentityParameter.Direction = ParameterDirection.Output;
			_insertedIdentityParameter.DbType = DbType.Int32;
			command.Parameters.Add(_insertedIdentityParameter);
		}

		IDataParameter _insertedIdentityParameter;

		protected override void ExecuteCommand(IDbCommand command)
		{
			base.ExecuteCommand(command);
			this.InsertedIdentity = (int)_insertedIdentityParameter.Value;
		}

		private int _insertedIdentity;

		public int InsertedIdentity
		{
			get { return _insertedIdentity; }
			set { _insertedIdentity = value; }
		}

	}
}
