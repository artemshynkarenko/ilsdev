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

		internal List<IAbstractComponent> LoadNavigationListeners(AbstractNavigationComponent navigationComponent)
		{
			List<IAbstractComponent> listeners = new List<IAbstractComponent>();
			BindablePoint point = navigationComponent.GetBindablePointBySystemName(AbstractUiConstants.IAbstractUiNavigationControl_NavigationListeners_BindingPointName);
			BindingController bindingController = new BindingController(this.FactoryContext);
			List<Binding> bindings = bindingController.GetByBindablePointId(point.BindablePointId);
			foreach (Binding binding in bindings)
			{
				IAbstractComponent component = this.GetObjectByInstanceId(binding.ImplementationId) as IAbstractComponent;
				listeners.Add(component);
			}

			return listeners;
		}
	}
}
