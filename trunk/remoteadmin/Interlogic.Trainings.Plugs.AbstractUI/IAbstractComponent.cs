using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	/// <summary>
	/// Will be class inherited from Instance
	/// </summary>
	public interface IAbstractComponent : IInstantiatable
	{
		IAbstractContainer ParentComponent { get;set;}
		IAbstractUIContext Context { get;set;}
		string Name { get; set; }
		ComponentMeasurement Top { get;set;}
		ComponentMeasurement Left { get;set;}
		ComponentMeasurement Height { get;set;}
		ComponentMeasurement Width { get;set;}
		ComponentPositioning PositioningMethod { get;set;}
	}
}
