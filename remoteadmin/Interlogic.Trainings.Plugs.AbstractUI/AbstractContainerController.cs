using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public abstract class AbstractContainerController : AbstractComponentController
	{
		public AbstractContainerController(ITransactionContext context)
			: base(context)
		{
		}
		public List<IAbstractComponent> LoadChildControls(AbstractContainer container)
		{
			List<IAbstractComponent> children = new List<IAbstractComponent>();
			BindablePoint point = container.GetBindablePointBySystemName(AbstractUiConstants.IAbstractUiContainer_Controls_BindingPointName);
			BindingController bindingController = new BindingController(this.FactoryContext);
			List<Binding> bindings = bindingController.GetByBindablePointId(point.BindablePointId);
			foreach (Binding binding in bindings)
			{
				IAbstractComponent component = this.GetObjectByInstanceId(binding.ImplementationId) as IAbstractComponent;
				component.ParentComponent = container;
				children.Add(component);
			}

			return children;
		}
	}
}
