using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public class SqlPrimaryKeyFieldAttribute:SqlDescriptiveAttribute
	{
		private bool _isIdentity;

		public bool IsIdentity
		{
			get { return _isIdentity; }
			set { _isIdentity = value; }
		}

	}
}
