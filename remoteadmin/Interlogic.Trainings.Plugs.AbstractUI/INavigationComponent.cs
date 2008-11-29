using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public interface INavigationComponent:IAbstractUiComponent
	{
		List<IAbstractUiComponent> NavigationListeners { get;}
		DomainObject SelectedObject { get;}
	}
}
