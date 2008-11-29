using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public interface INavigationListenerComponent:IAbstractUiComponent
	{
		void OnNavigate(INavigationComponent navigator);
	}
}
