using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	using Kernel;
    using Interlogic.Trainings.Plugs.Kernel.DomainModel;

	public abstract class AbstractComponent : Instance, IAbstractComponent
	{
		protected AbstractComponent()
		{
		}
		
		#region IAbstractComponent Members
		
        private IAbstractContainer _parentComponent;
		
        public virtual IAbstractContainer ParentComponent
		{
			get
			{
				return _parentComponent;
			}
			set
			{
				_parentComponent = value;
				_context = value.Context;
			}
		}
		
        private IAbstractUIContext _context;
		
        public virtual IAbstractUIContext Context
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
		
		string IAbstractComponent.Name
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

		private ComponentMeasurement _top;

		public ComponentMeasurement Top
		{
			get { return _top; }
			set { _top = value; }
		}


		private ComponentMeasurement _left;

		public ComponentMeasurement Left
		{
			get { return _left; }
			set { _left = value; }
		}
		
        
        private ComponentMeasurement _height;

		public ComponentMeasurement Height
		{
			get { return _height; }
			set { _height = value; }
		}
		
        
        private ComponentMeasurement _width;

		public ComponentMeasurement Width
		{
			get { return _width; }
			set { _width = value; }
		}

		
        private ComponentPositioning _positioningMethod;

		public ComponentPositioning PositioningMethod
		{
			get { return _positioningMethod; }
			set { _positioningMethod = value; }
		}

		#endregion
    }
}
