using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public class ComponentMeasurement
	{
		private bool _nothingSet = true;
		private int _value;

		public int Value
		{
			get { return _value; }
			set
			{
				_value = value;
				_nothingSet = false;
			}
		}

		private ComponentMeasurementUnit _unit;

		public ComponentMeasurementUnit Unit
		{
			get { return _unit; }
			set
			{
				_unit = value;
				_nothingSet = false;
			}
		}

        public ComponentMeasurement()
		{
		}

		public ComponentMeasurement(int value, ComponentMeasurementUnit unit)
			: this()
		{
			this.Unit = unit;
			this.Value = value;
		}

		public static readonly ComponentMeasurement Empty = new ComponentMeasurement();
		public static readonly ComponentMeasurement Rest = new ComponentMeasurement();

		public static bool IsNullOrEmpty(ComponentMeasurement measurement)
		{
			return measurement == null || measurement == Empty || measurement._nothingSet;
		}
	}
}
