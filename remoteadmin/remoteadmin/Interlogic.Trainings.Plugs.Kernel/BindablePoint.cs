using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class BindablePoint : DomainObject
	{
        private int _bindablePointId;
        public int BindablePointId
        {
            get { return _bindablePointId; }
            set { _bindablePointId = value; }
        }

        private int _bindablePointDefinitionId;
        public int BindablePointDefinitionId
        {
            get { return _bindablePointDefinitionId; }
            set { _bindablePointDefinitionId = value; }
        }

        private int _instanceId;
        public int InstanceId
        {
            get { return _instanceId; }
            set { _instanceId = value; }
        }

        private bool _active;
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

		private string _systemName;

		public string SystemName
		{
			get { return _systemName; }
			set { _systemName = value; }
		}

	}
}
