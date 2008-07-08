using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public abstract class Plug:DomainObject
	{
		public abstract List<BindingPoint> BindablePoints { get;}
		public abstract List<PlugFile> Files { get;}
		public abstract List<ClassDefinition> ClassDefinitions { get;}
		public abstract List<Binding> Bindings { get;}
		public abstract List<PlugLocation> Locations { get;}
	}
}
