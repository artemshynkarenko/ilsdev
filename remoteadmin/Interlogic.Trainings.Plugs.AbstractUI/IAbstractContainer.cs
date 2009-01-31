using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	/// <summary>
	/// IAbstractUiContainer classDefinition contains one bindablePointDefinition - ChildControl
	/// </summary>
	public interface IAbstractContainer:IAbstractComponent
	{
		List<IAbstractComponent> Controls { get;}
	}
}
