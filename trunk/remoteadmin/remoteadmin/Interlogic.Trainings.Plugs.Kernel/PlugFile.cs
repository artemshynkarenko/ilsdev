using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class PlugFile: DomainObject
	{
        private int _plugFileId;
        public int PlugFileId
        {
            get { return _plugFileId; }
            set { _plugFileId = value; }
        }

        private string _plugFileName;
        public string PlugFileName
        {
            get { return _plugFileName; }
            set { _plugFileName = value; }
        }

        private string _relativeIncomingPath;
        public string RelativeIncomingPath
        {
            get { return _relativeIncomingPath; }
            set { _relativeIncomingPath = value; }
        }

        private int _destinationLocationId;
        public int DestinationLocationId
        {
            get { return _destinationLocationId; }
            set { _destinationLocationId = value; }
        }

        private string _destinationPath;
        public string DestinationPath
        {
            get { return _destinationPath; }
            set { _destinationPath = value; }
        }

        private int _plugId;
        public int PlugId
        {
            get { return _plugId; }
            set { _plugId = value; }
        }
	}
}
