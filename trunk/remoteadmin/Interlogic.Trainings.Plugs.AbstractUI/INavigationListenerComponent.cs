using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public interface INavigationListenerComponent:IAbstractComponent
	{
		void OnNavigate(INavigationComponent navigator);
	}
}
