using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public class SqlClassTable
	{
		private string _tableName;

		public string TableName
		{
			get { return _tableName; }
			set { _tableName = value; }
		}

		private List<SqlClassTable> _dependentTables = new List<SqlClassTable>();

		public List<SqlClassTable> DependentTables
		{
			get { return _dependentTables; }
			set { _dependentTables = value; }
		}

		private Type _type;

		public Type Type
		{
			get { return _type; }
			set { _type = value; }
		}


	}
}
