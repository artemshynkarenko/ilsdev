using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public abstract class AbstractContainerController : InstanceController
	{
		public AbstractContainerController(ITransactionContext context)
			: base(context)
		{
		}
		public List<IAbstractUiComponent> LoadChildControls(AbstractContainer container)
		{
			List<IAbstractUiComponent> children = new List<IAbstractUiComponent>();
			BindablePoint point = container.GetBindablePointBySystemName(AbstractUiConstants.IAbstractUiContainer_Controls_BindingPointName);
			BindingController bindingController = new BindingController(this.FactoryContext);
			List<Binding> bindings = bindingController.GetByBindablePointId(point.BindablePointId);
			foreach (Binding binding in bindings)
			{
				IAbstractUiComponent component = this.GetObjectByInstanceId(binding.ImplementationId) as IAbstractUiComponent;
				component.ParentComponent = container;
				children.Add(component);
			}

			return children;
		}
	}
}
