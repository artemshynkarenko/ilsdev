using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel.Exceptions;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class PlugInController : DomainController
	{
		public PlugInController(ITransactionContext context)
			: base(context)
		{
		}

		public void Insert(PlugIn plug)
		{
			using (PlugInFactory factory = PlugInFactory.GetInstance())
			{
				factory.Context = this.FactoryContext;
				ValidateInstance(plug);
				factory.InternalInsert(plug);
			}
		}

		public void InsertAll(PlugIn plug)
		{
			try
			{
				this.FactoryContext.BeginTransaction();
				ValidateInstance(plug);
				using (PlugInFactory factory = PlugInFactory.GetInstance())
				{
					factory.Context = this.FactoryContext;
					factory.InternalInsert(plug);
				}
                using (PlugLocationFactory locationFactory = PlugLocationFactory.GetInstance())
                {
                    locationFactory.Context = this.FactoryContext;
                    foreach (PlugLocation location in plug.Locations)
                    {
                        locationFactory.InternalInsert(location);
                    }
                }
                using (PlugFileFactory fileFactory = PlugFileFactory.GetInstance())
                {
                    fileFactory.Context = this.FactoryContext;
                    foreach (PlugFile file in plug.Files)
                    {
                        fileFactory.InternalInsert(file);
                    }
                }
                using (BindingFactory bindingFactory = BindingFactory.GetInstance())
                {
                    bindingFactory.Context = this.FactoryContext;
                    foreach (Binding binding in plug.Bindings)
                    {
                        bindingFactory.InternalInsert(binding);
                    }
                }
                using (BindablePointFactory bindablePointFactory = BindablePointFactory.GetInstance())
                {
                    bindablePointFactory.Context = this.FactoryContext;
                    foreach (BindablePoint bindablePoint in plug.BindablePoints)
                    {

                        bindablePointFactory.InternalInsert(bindablePoint);
                    }
                }
                using (ClassDefinitionFactory classDefinitionFactory = ClassDefinitionFactory.GetInstance())
                {
                    classDefinitionFactory.Context = this.FactoryContext;
                    foreach (ClassDefinition classDefinition in plug.ClassDefinitions)
                    {
                        classDefinitionFactory.InternalInsert(classDefinition);
                    }
                }
                // TODO: something else?..
				this.FactoryContext.Commit();
			}
			catch
			{
				this.FactoryContext.RollBack();
				throw;
			}
		}

		public void Update(PlugIn plug)
		{
			using (PlugInFactory factory = PlugInFactory.GetInstance())
			{
				factory.Context = this.FactoryContext;
				ValidateInstance(plug);
				factory.InternalUpdate(plug);
			}
		}

		public void UpdateAll(PlugIn plug)
		{
            try
            {
                this.FactoryContext.BeginTransaction();
                ValidateInstance(plug);
                using (PlugInFactory factory = PlugInFactory.GetInstance())
                {
                    factory.Context = this.FactoryContext;
                    factory.InternalUpdate(plug);
                }
                using (PlugLocationFactory locationFactory = PlugLocationFactory.GetInstance())
                {
                    locationFactory.Context = this.FactoryContext;
                    foreach (PlugLocation location in plug.Locations)
                    {
                        locationFactory.InternalUpdate(location);
                    }
                }
                using (PlugFileFactory fileFactory = PlugFileFactory.GetInstance())
                {
                    fileFactory.Context = this.FactoryContext;
                    foreach (PlugFile file in plug.Files)
                    {
                        fileFactory.InternalUpdate(file);
                    }
                }
                using (BindingFactory bindingFactory = BindingFactory.GetInstance())
                {
                    bindingFactory.Context = this.FactoryContext;
                    foreach (Binding binding in plug.Bindings)
                    {
                        bindingFactory.InternalUpdate(binding);
                    }
                }
                using (BindablePointFactory bindablePointFactory = BindablePointFactory.GetInstance())
                {
                    bindablePointFactory.Context = this.FactoryContext;
                    foreach (BindablePoint bindablePoint in plug.BindablePoints)
                    {

                        bindablePointFactory.InternalUpdate(bindablePoint);
                    }
                }
                using (ClassDefinitionFactory classDefinitionFactory = ClassDefinitionFactory.GetInstance())
                {
                    classDefinitionFactory.Context = this.FactoryContext;
                    foreach (ClassDefinition classDefinition in plug.ClassDefinitions)
                    {
                        classDefinitionFactory.InternalUpdate(classDefinition);
                    }
                }
                // TODO: something else?..
                this.FactoryContext.Commit();
            }
            catch
            {
                this.FactoryContext.RollBack();
                throw;
            }
        }

		public virtual void ValidateInstance(PlugIn plug)
		{
			if (string.IsNullOrEmpty(plug.PlugFriendlyName))
			{
				throw new ValidationException("Plug FriendlyName should be not empty");
			}
			//TODO: continue validation;
		}

		public List<PlugIn> LoadAll()
		{
			using (PlugInFactory factory = PlugInFactory.GetInstance())
			{
				factory.Context = this.FactoryContext;
				return factory.InternalLoadAll();
			}
		}

		void PlugFactory_FixChildren(object sender, DomainFactoryEventArgs e)
		{
			PlugIn plug = (PlugIn)e.Object;
			int plugId = plug.PlugId;
			foreach (PlugLocation location in plug.Locations)
			{
				location.PlugId = plugId;
			}

			foreach (ClassDefinition definition in plug.ClassDefinitions)
			{
				definition.PlugId = plugId;
			}

			foreach (PlugFile file in plug.Files)
			{
				file.PlugId = plugId;
			}

            //foreach (Binding binding in plug.Bindings)
            //{
            //    binding.PlugId = plugId;
            //}

            //foreach (BindablePoint point in plug.BindablePoints)
            //{
            //    point.PlugId = plugId;
            //}
		}
	}
}