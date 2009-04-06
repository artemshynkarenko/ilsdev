using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public abstract class RawSqlAction:SqlAction
	{
		protected abstract string GetExecutionSql();

        private List<string> _paramNames = new List<string>();
        private List<object> _paramValues = new List<object>();
        private List<DbType> _paramTypes = new List<DbType>();
        private List<ParameterDirection> _paramDirections = new List<ParameterDirection>();

        public void AddParameter(string paramName, object paramValue, DbType paramType)
        {
            _paramNames.Add(paramName);
            _paramValues.Add(paramValue != null ? paramValue : DBNull.Value);
            _paramTypes.Add(paramType);
            _paramDirections.Add(ParameterDirection.Input);
        }

        public void AddParameter(string paramName, object paramValue, DbType paramType, ParameterDirection paramDirection)
        {
            _paramNames.Add(paramName);
            _paramValues.Add(paramValue);
            _paramTypes.Add(paramType);
            _paramDirections.Add(paramDirection);
        }

		protected virtual void SetParameters(IDbCommand command)
		{
            IDataParameter param = null;

            for (int i = 0, n = _paramNames.Count; i < n; ++i)
            {
                param = command.CreateParameter();
                param.ParameterName = _paramNames[i];
                param.Value         = _paramValues[i];
                param.DbType        = _paramTypes[i];
                param.Direction     = _paramDirections[i];
                command.Parameters.Add(param);
            }
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
