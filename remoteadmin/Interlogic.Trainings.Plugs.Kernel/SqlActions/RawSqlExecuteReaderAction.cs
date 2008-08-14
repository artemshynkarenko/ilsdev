using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
    public class RawSqlExecuteReaderAction : RawSqlAction
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

        protected IDataReader _dataReader = null;
        public IDataReader DataReader
        {
            get { return _dataReader; }
        }

        protected override void ExecuteCommand(System.Data.IDbCommand command)
        {
            command.CommandText = _commandText;
            _dataReader = command.ExecuteReader();
        }
    }
}
