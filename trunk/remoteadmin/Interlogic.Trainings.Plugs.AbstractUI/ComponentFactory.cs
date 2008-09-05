using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	using Kernel;

	public class ComponentFactory:ClassDefinitionFactory
	{
		internal void InternalInsert()
		{
		}

		protected void Insert(Component component)
		{
			base.Insert(component);
		}
	}
}
