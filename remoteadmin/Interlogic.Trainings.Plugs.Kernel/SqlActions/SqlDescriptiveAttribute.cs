using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
	public class SqlDescriptiveAttribute:Attribute
	{
		public SqlDescriptiveAttribute():this(string.Empty, string.Empty)
		{
		}

		public SqlDescriptiveAttribute(string tableName):this(tableName, string.Empty)
		{
		}

		public SqlDescriptiveAttribute(string tableName, string tableField)
		{
			this.TableName = tableName;
			this.TableField = tableField;
		}

		private string _tableName = string.Empty;

		public string TableName
		{
			get { return _tableName; }
			set { _tableName = value; }
		}

		private string _tableField = string.Empty;

		public string TableField
		{
			get { return _tableField; }
			set { _tableField = value; }
		}
	}
}
