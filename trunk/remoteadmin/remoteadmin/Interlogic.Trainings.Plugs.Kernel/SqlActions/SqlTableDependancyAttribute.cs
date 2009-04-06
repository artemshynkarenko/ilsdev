using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
	public class SqlTableDependancyAttribute: Attribute
	{
		private string _primaryTable;

		public string PrimaryTable
		{
			get { return _primaryTable; }
			set { _primaryTable = value; }
		}

		private string _dependentTable;

		public string DependentTable
		{
			get { return _dependentTable; }
			set { _dependentTable = value; }
		}

		public SqlTableDependancyAttribute(string primaryTable, string dependantTable)
		{
			this.PrimaryTable = primaryTable;
			this.DependentTable = dependantTable;
		}
	
	}
}
