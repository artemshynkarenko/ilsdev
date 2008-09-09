using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class BindablePointDefinition
	{
        private int _bindablePointDefinitionId;
        public int BindablePointDefinitionId
        {
            get { return _bindablePointDefinitionId; }
            set { _bindablePointDefinitionId = value; }
        }

        private int _classDefinitionId;
        public int ClassDefinitionId
        {
            get { return _classDefinitionId; }
            set { _classDefinitionId = value; }
        }

        private string _bindablePointName;
        public string BindablePointName
        {
            get { return _bindablePointName; }
            set { _bindablePointName = value; }
        }

        private string _bindablePointFriendlyName;
        public string BindablePointFriendlyName
        {
            get { return _bindablePointFriendlyName; }
            set { _bindablePointFriendlyName = value; }
        }

        private string _bindablePointDescription;
        public string BindablePointDescription
        {
            get { return _bindablePointDescription; }
            set { _bindablePointDescription = value; }
        }

        private int _interfaceId;
        public int InterfaceId
        {
            get { return _interfaceId; }
            set { _interfaceId = value; }
        }
	}
}
