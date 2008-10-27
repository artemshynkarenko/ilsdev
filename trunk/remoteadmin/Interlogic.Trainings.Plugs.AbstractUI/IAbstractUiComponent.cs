using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	/// <summary>
	/// Will be class inherited from Instance
	/// </summary>
	public interface IAbstractUiComponent : IInstantiatable
	{
		IAbstractUiContainer ParentComponent { get;set;}
		IAbstractUiContext Context { get;set;}
		string Name { get; set; }
	}
}
