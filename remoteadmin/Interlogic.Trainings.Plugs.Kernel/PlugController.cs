using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel.Exceptions;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class PlugController : DomainController
	{
		public PlugController(ITransactionContext context)
			: base(context)
		{
		}

		public void Insert(Plug plug)
		{
			using (PlugFactory factory = PlugFactory.GetInstance())
			{
				factory.Context = this.FactoryContext;
				ValidateInstance(plug);
				factory.InternalInsert(plug);
			}
		}

		public void InsertAll(Plug plug)
		{
			try
			{
				this.FactoryContext.BeginTransaction();
				ValidateInstance(plug);
				using (PlugFactory factory = PlugFactory.GetInstance())
				{
					factory.Context = this.FactoryContext;
					factory.Inserted += new EventHandler<DomainFactoryEventArgs>(PlugFactory_FixChildren);
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
                //TODO: Uncomment this when classes will be implemented
                //using (PlugFileFactory fileFactory = PlugFileFactory.GetInstance())
                //{
                //    fileFactory.Context = this.FactoryContext;
                //    foreach (PlugFile file in plug.Files)
                //    {
                //        fileFactory.InternalInsert(file);
                //    }
                //}
                //using (BindingFactory bindingFactory = BindingFactory.GetInstance())
                //{
                //    bindingFactory.Context = this.FactoryContext;
                //    foreach (Binding binding in plug.Bindings)
                //    {
                //        bindingFactory.InternalInsert(binding);
                //    }
                //}
                //using (BindingPointFactory bindingPointFactory = BindingPointFactory.GetInstance())
                //{
                //    bindingPointFactory.Context = this.FactoryContext;
                //    foreach (BindingPoint bindingPoint in plug.BindablePoints)
                //    {

                //        bindingPointFactory.InternalInsert(bindingPoint);
                //    }
                //}
                //using (ClassDefinitionFactory classDefinitionFactory = ClassDefinitionFactory.GetInstance())
                //{
                //    classDefinitionFactory.Context = this.FactoryContext;
                //    foreach (ClassDefinition classDefinition in plug.ClassDefinitions)
                //    {
                //        classDefinitionFactory.InternalInsert(classDefinition);
                //    }
                //}
                // TODO: something else?..
				this.FactoryContext.Commit();
			}
			catch
			{
				this.FactoryContext.RollBack();
				throw;
			}
		}

		public void Update(Plug plug)
		{
			using (PlugFactory factory = PlugFactory.GetInstance())
			{
				factory.Context = this.FactoryContext;
				ValidateInstance(plug);
				factory.InternalUpdate(plug);
			}
		}

		public void UpdateAll(Plug plug)
		{
            try
            {
                this.FactoryContext.BeginTransaction();
                ValidateInstance(plug);
                using (PlugFactory factory = PlugFactory.GetInstance())
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
                //TODO: Uncomment this when classes will be implemented
                //using (PlugFileFactory fileFactory = PlugFileFactory.GetInstance())
                //{
                //    fileFactory.Context = this.FactoryContext;
                //    foreach (PlugFile file in plug.Files)
                //    {
                //        fileFactory.InternalUpdate(file);
                //    }
                //}
                //using (BindingFactory bindingFactory = BindingFactory.GetInstance())
                //{
                //    bindingFactory.Context = this.FactoryContext;
                //    foreach (Binding binding in plug.Bindings)
                //    {
                //        bindingFactory.InternalUpdate(binding);
                //    }
                //}
                //using (BindingPointFactory bindingPointFactory = BindingPointFactory.GetInstance())
                //{
                //    bindingPointFactory.Context = this.FactoryContext;
                //    foreach (BindingPoint bindingPoint in plug.BindablePoints)
                //    {

                //        bindingPointFactory.InternalUpdate(bindingPoint);
                //    }
                //}
                //using (ClassDefinitionFactory classDefinitionFactory = ClassDefinitionFactory.GetInstance())
                //{
                //    classDefinitionFactory.Context = this.FactoryContext;
                //    foreach (ClassDefinition classDefinition in plug.ClassDefinitions)
                //    {
                //        classDefinitionFactory.InternalUpdate(classDefinition);
                //    }
                //}
                // TODO: something else?..
                this.FactoryContext.Commit();
            }
            catch
            {
                this.FactoryContext.RollBack();
                throw;
            }
        }

		public virtual void ValidateInstance(Plug plug)
		{
			if (string.IsNullOrEmpty(plug.PlugFriendlyName))
			{
				throw new ValidationException("Plug FriendlyName should be not empty");
			}
			//TODO: continue validation;
		}

		public List<Plug> LoadAll()
		{
			using (PlugFactory factory = PlugFactory.GetInstance())
			{
				factory.Context = this.FactoryContext;
				return factory.InternalLoadAll();
			}
		}

		void PlugFactory_FixChildren(object sender, DomainFactoryEventArgs e)
		{
			Plug plug = (Plug)e.Object;
			int plugId = plug.PlugId;
			foreach (PlugLocation location in plug.Locations)
			{
				//TODO: Uncomment this when class will be implemented
				//location.PlugId = plugId;
			}

			foreach (ClassDefinition definition in plug.ClassDefinitions)
			{
				//TODO: Uncomment this when class will be implemented
				//definition.PlugId = plugId;
			}

			foreach (PlugFile file in plug.Files)
			{
				//TODO: Uncomment this when class will be implemented
				//file.PlugId = plugId;
			}

			foreach (Binding binding in plug.Bindings)
			{
				//TODO: Uncomment this when class will be implemented
				//binding.PlugId = plugId;
			}

			foreach (BindingPoint point in plug.BindablePoints)
			{
				//TODO: Uncomment this when class will be implemented
				//point.PlugId = plugId;
			}
		}
	}
}
