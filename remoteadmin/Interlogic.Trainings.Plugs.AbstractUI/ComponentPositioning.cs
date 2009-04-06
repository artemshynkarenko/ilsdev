using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	/// <summary>
	/// Need some logic on that - but i do not knoew why i have done that
	/// </summary>
	public enum ComponentPositioning
	{
		/// <summary>
		/// Flow position - components are just drawing one by one - coordinated are ignored?
		/// </summary>
		Flow,
		/// <summary>
		/// Relative to parent?
		/// </summary>
		Relative,
		/// <summary>
		/// Absolute position - coordinates are calculated according to context - that's for sure
		/// </summary>
		Abslolute
	}
}
