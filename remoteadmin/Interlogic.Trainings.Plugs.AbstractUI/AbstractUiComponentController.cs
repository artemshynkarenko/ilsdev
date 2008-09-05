using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	using Kernel;

	public class AbstractUiComponentController
	{
		/// <summary>
		/// Create rows in ClassDefinition table. Next - inserts BindablePointDefinition attached to that component. Next Insert record into componenttable
		/// ( that can be just invocation of ComponentContoller.InsertAll method)
		/// </summary>
		/// <param name="component"></param>
		public void RegisterComponent(Component component)
		{
		}

		public IAbstractUiContext Context;
		/// <summary>
		/// Here we will do main work on creation hierarchy of UI objects
		/// </summary>
		/// <param name="component"></param>
		/// <param name="parentComponent"></param>
		/// <param name="parentBindablePoint"></param>
		/// <returns></returns>
		public IAbstractUiComponent RegisterChildControl(Component component, string componentSystemName, IAbstractUiContainer parentComponent)
		{
			
			ClassDefinitionController instantinator = new ClassDefinitionController();
			IAbstractUiComponent uiComponent = instantinator.GetInstance(component, componentSystemName) as IAbstractUiComponent;
			uiComponent.Context = this.Context;
			uiComponent.ParentComponent = parentComponent;
			/*
			Binding b = new Binding();
			b.BindablePointId = parentComponent.ChildControl_BindablePointId;
			b.ImplemetationId = uiComponent.InstanceId;
			BindingController bcontroller = new BindingController();
			bcontroller.Insert(b);*/

			return uiComponent;

		}
	}
}
