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
                        location.PlugId = plug.PlugId;
                        locationFactory.InternalInsert(location);
                    }
                    using (PlugFileFactory fileFactory = PlugFileFactory.GetInstance())
                    {
                        fileFactory.Context = this.FactoryContext;
                        foreach (PlugFile file in plug.Files)
                        {
                            file.DestinationLocationId = locationFactory.InternalLoadByName(file.DestinationPath).PlugLocationId;
                            file.PlugId = plug.PlugId;
                            fileFactory.InternalInsert(file);
                        }

                        using (ClassDefinitionFactory classDefinitionFactory = ClassDefinitionFactory.GetInstance())
                        {
                            classDefinitionFactory.Context = this.FactoryContext;
                            foreach (ClassDefinition classDefinition in plug.ClassDefinitions)
                            {
                                classDefinition.PlugId = plug.PlugId;
                                int fileId = -1;
                                foreach (PlugFile file in plug.Files)
                                {
                                    if (file.PlugFileName == classDefinition.FileName)
                                    {
                                        fileId = file.PlugFileId;
                                        break;
                                    }
                                }
                                if (fileId == -1)
                                    throw new Exception("Not found corresponding file for class definition!");
                                classDefinition.FileId = fileId;
                                classDefinitionFactory.InternalInsert(classDefinition);
                            }
                        }
                    }
                }

				using (BindablePointDefinitionFactory bindablePointDefinitionFactory = BindablePointDefinitionFactory.GetInstance())
                {
					bindablePointDefinitionFactory.Context = this.FactoryContext;
					foreach (BindablePointDefinition bindablePointDefinition in plug.BindablePointDefinitions)
                    {
						bindablePointDefinitionFactory.InternalInsert(bindablePointDefinition);
                    }
                }
                // TODO: something else?..
				this.FactoryContext.Commit();
			}
			catch (Exception e)
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

				using (BindablePointDefinitionFactory bindablePointDefinitionFactory = BindablePointDefinitionFactory.GetInstance())
                {
					bindablePointDefinitionFactory.Context = this.FactoryContext;
					foreach (BindablePointDefinition bindablePointDefinition in plug.BindablePointDefinitions)
                    {

						bindablePointDefinitionFactory.InternalUpdate(bindablePointDefinition);
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

		public void Delete(PlugIn plug)
		{
			using (PlugInFactory factory = PlugInFactory.GetInstance())
			{
				factory.Context = this.FactoryContext;
				ValidateInstance(plug);
				factory.InternalDelete(plug);
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

		public PlugIn LoadByPrimaryKey(int plugInId)
		{
			using (PlugInFactory factory = PlugInFactory.GetInstance())
			{
				factory.Context = this.FactoryContext;
				return factory.InternalLoadByPrimaryKey(plugInId);
			}
		}

		public PlugIn LoadByName(string plugName)
		{
			using (PlugInFactory factory = PlugInFactory.GetInstance())
			{
				factory.Context = this.FactoryContext;
				return factory.InternalLoadByName(plugName);
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
