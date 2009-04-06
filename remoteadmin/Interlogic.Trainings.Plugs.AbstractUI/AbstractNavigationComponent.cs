using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public abstract class AbstractNavigationComponent : AbstractComponent, INavigationComponent
	{
		private List<INavigationListenerComponent> _navigationListeners = null;

		public List<INavigationListenerComponent> NavigationListeners
		{
			get
			{
				if (_navigationListeners == null)
				{
					this.LoadListeners(this.Context.TransactionContext);
				}
				return _navigationListeners;
			}
			set { _navigationListeners = value; }
		}

		public abstract DomainObject SelectedObject { get;}
		public abstract IAbstractComponent SelectedComponent { get;}

		public override void Setup(Instance dbInstance, ITransactionContext context)
		{
			base.Setup(dbInstance, context);
			this.LoadListeners(context);
		}

		protected virtual void LoadListeners(ITransactionContext context)
		{
			AbstractNavigationComponentController controller = this.GetControllerInstance(context) as AbstractNavigationComponentController;
			this._navigationListeners = controller.LoadNavigationListeners(this);
		}
	}
}
