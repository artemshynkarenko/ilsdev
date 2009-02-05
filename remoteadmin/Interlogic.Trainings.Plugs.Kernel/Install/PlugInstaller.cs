using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public abstract class PlugInstaller
	{
		//TODO: |Do they realy needed?
		//public abstract List<IRegisterPlugAction> RegisterActions { get;}
		//public abstract List<IUpdatePlugAction> UpdateActions { get;}
		//public abstract List<IUnregisterPlugAction> UnregisterActions { get;}

		public abstract void RegisterPlug();
		public abstract void UpdatePlug();
		public abstract void UnregisterPlug();
	}
}
