using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class PlugLocation : DomainObject
	{
        private int _plugLocationId;
        public int PlugLocationId
        {
            get { return _plugLocationId; }
            set { _plugLocationId = value; }
        }

        private string _plugLocationName;
        public string PlugLocationName
        {
            get { return _plugLocationName; }
            set { _plugLocationName = value; }
        }

        private string _plugLocationDescription;
        public string PlugLocationDescription
        {
            get { return _plugLocationDescription; }
            set { _plugLocationDescription = value; }
        }

        private string _plugLocationPath;
        public string PlugLocationPath
        {
            get { return _plugLocationPath; }
            set { _plugLocationPath = value; }
        }

        private int _plugId;
        public int PlugId
        {
            get { return _plugId; }
            set { _plugId = value; }
        }
    }
}
