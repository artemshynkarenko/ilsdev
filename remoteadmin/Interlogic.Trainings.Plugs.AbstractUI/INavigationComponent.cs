using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public interface INavigationComponent:IAbstractComponent
	{
		List<INavigationListenerComponent> NavigationListeners { get; }
		DomainObject SelectedObject { get;}
	}
}
