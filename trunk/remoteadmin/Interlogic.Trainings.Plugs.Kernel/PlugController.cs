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
				//TODO: Continue
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
			//TODO: same as on insert
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
