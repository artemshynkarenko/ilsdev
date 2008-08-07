using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class Plug : DomainObject
	{
		private int _plugId;

		public int PlugId
		{
			get { return _plugId; }
			set { _plugId = value; }
		}

		private string _plugName;

		public string PlugName
		{
			get { return _plugName; }
			set { _plugName = value; }
		}

		private string _plugFriendlyName;

		public string PlugFriendlyName
		{
			get { return _plugFriendlyName; }
			set { _plugFriendlyName = value; }
		}
		private string _plugDescription;

		public string PlugDescription
		{
			get { return _plugDescription; }
			set { _plugDescription = value; }
		}

		private string _plugVersion;

		public string PlugVersion
		{
			get { return _plugVersion; }
			set { _plugVersion = value; }
		}

		private bool _active;

		public bool Active
		{
			get { return _active; }
			set { _active = value; }
		}

		private List<BindingPoint> _bindablePoints = new List<BindingPoint>();
		public List<BindingPoint> BindablePoints { get { return _bindablePoints; } }

		private List<PlugFile> _files = new List<PlugFile>();
		public List<PlugFile> Files { get { return _files; } }

		private List<ClassDefinition> _classDefinitions = new List<ClassDefinition>();
		public List<ClassDefinition> ClassDefinitions { get { return _classDefinitions; } }

		private List<Binding> _bindings = new List<Binding>();
		public List<Binding> Bindings { get { return _bindings; } }

		private List<PlugLocation> _locations = new List<PlugLocation>();
		public List<PlugLocation> Locations { get { return _locations; } }
		
	}
}
