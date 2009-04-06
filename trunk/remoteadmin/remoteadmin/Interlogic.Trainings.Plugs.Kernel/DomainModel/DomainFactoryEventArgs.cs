using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;

namespace Interlogic.Trainings.Plugs.Kernel.DomainModel
{
	public class DomainFactoryEventArgs : EventArgs
	{
		private DomainObject _object;

		public DomainObject Object
		{
			get { return _object; }
			set { _object = value; }
		}
		private ISqlAction _action;

		public ISqlAction Action
		{
			get { return _action; }
			set { _action = value; }
		}


		public DomainFactoryEventArgs(DomainObject domainObject, ISqlAction action)
		{
			this.Object = domainObject;
			this.Action = action;
		}
	}
}
