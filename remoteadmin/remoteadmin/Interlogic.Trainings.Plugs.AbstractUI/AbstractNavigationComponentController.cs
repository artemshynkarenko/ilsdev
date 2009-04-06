using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public abstract class AbstractNavigationComponentController : AbstractComponentController
	{
		public AbstractNavigationComponentController(ITransactionContext context)
			: base(context)
		{
		}

		internal List<INavigationListenerComponent> LoadNavigationListeners(AbstractNavigationComponent navigationComponent)
		{
			List<INavigationListenerComponent> listeners = new List<INavigationListenerComponent>();
			BindablePoint point = navigationComponent.GetBindablePointBySystemName(AbstractUiConstants.IAbstractUiNavigationControl_NavigationListeners_BindingPointName);
			BindingController bindingController = new BindingController(this.FactoryContext);
			List<Binding> bindings = bindingController.GetByBindablePointId(point.BindablePointId);
			foreach (Binding binding in bindings)
			{
				INavigationListenerComponent component = this.GetObjectByInstanceId(binding.ImplementationId) as INavigationListenerComponent;
				listeners.Add(component);
			}

			return listeners;
		}
	}
}
