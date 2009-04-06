using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple=true, Inherited=true)]
	public class SqlTableFieldAttribute:SqlDescriptiveAttribute
	{
	}
}
