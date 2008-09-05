using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	/// <summary>
	/// Will be class inherited from Instance
	/// </summary>
	public interface IAbstractUiComponent
	{
		IAbstractUiContainer ParentComponent { get;set;}
		IAbstractUiContext Context { get;set;}
		int InstanceId { get; set;}
		int InstanceName { get;set;}
	}
}
