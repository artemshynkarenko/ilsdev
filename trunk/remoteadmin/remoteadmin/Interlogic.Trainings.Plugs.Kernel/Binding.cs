using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class Binding:DomainObject
	{
        private int _bindingId;
        public int BindingId
        {
            get { return _bindingId; }
            set { _bindingId = value; }
        }

        private int _bindablePointId;
        public int BindablePointId
        {
            get { return _bindablePointId; }
            set { _bindablePointId = value; }
        }

        private int _implementationId;
        public int ImplementationId
        {
            get { return _implementationId; }
            set { _implementationId = value; }
        }
	}
}
