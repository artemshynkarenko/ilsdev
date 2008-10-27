using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	using Kernel;

	public abstract class AbstractComponent : Instance, IAbstractUiComponent
	{
		protected AbstractComponent()
		{
		}
		
		#region IAbstractUiComponent Members
		private IAbstractUiContainer _parentComponent;
		public IAbstractUiContainer ParentComponent
		{
			get
			{
				return _parentComponent;
			}
			set
			{
				_parentComponent = value;
			}
		}
		private IAbstractUiContext _context;
		public IAbstractUiContext Context
		{
			get
			{
				return _context;
			}
			set
			{
				_context = value;
			}
		}
		
		string IAbstractUiComponent.Name
		{
			get
			{
				return this.InstanceName;
			}
			set
			{
				this.InstanceName = value;
			}
		}

		#endregion
	}
}
