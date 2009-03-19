using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.IO;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class KernelPlugInstaller : PlugInstaller
	{
		public string InitialDir { get; set; }

		public override void RegisterPlug(ITransactionContext context)
        {
            #region Installing Required Environments
            PlugInFactory factoryPlug = PlugInFactory.GetInstance();
			factoryPlug.Context = context as SqlTransactionContext;
			factoryPlug.InstallRequiredEnvironment();

            PlugLocationFactory factoryPlugLocation = PlugLocationFactory.GetInstance();
            factoryPlugLocation.Context = context as SqlTransactionContext;
            factoryPlugLocation.InstallRequiredEnvironment();

            PlugFileFactory factoryPlugFile = PlugFileFactory.GetInstance();
            factoryPlugFile.Context = context as SqlTransactionContext;
            factoryPlugFile.InstallRequiredEnvironment();

            ClassDefinitionFactory factoryClassDefinition = ClassDefinitionFactory.GetInstance();
            factoryClassDefinition.Context = context as SqlTransactionContext;
            factoryClassDefinition.InstallRequiredEnvironment();

            BindablePointDefinitionFactory factoryBindablePointDefinition = BindablePointDefinitionFactory.GetInstance();
            factoryBindablePointDefinition.Context = context as SqlTransactionContext;
            factoryBindablePointDefinition.InstallRequiredEnvironment();

            BindablePointFactory factoryBindablePoint = BindablePointFactory.GetInstance();
            factoryBindablePoint.Context = context as SqlTransactionContext;
            factoryBindablePoint.InstallRequiredEnvironment();

            BindingFactory factoryBinding = BindingFactory.GetInstance();
            factoryBinding.Context = context as SqlTransactionContext;
            factoryBinding.InstallRequiredEnvironment();

            InstanceFactory factoryInstance = InstanceFactory.GetInstance();
            factoryInstance.Context = context as SqlTransactionContext;
            factoryInstance.InstallRequiredEnvironment();
            #endregion

            PlugIn plug = new PlugIn();
			plug.PlugName = "Interlogic.Trainings.Plugs.Kernel";
			plug.PlugVersion = "0.0.0.1";
			plug.PlugFriendlyName = "RemoteAdmin Kernel";
			plug.PlugDescription = "RemoteAdmin Kernel";
			plug.Active = true;

			PlugLocation loc = new PlugLocation();
			loc.PlugLocationName = "EXECUTABLE_DIR";
			loc.PlugLocationPath = Path.Combine(InitialDir, @"bin");
			loc.PlugLocationDescription = "Main executable directory";
			plug.Locations.Add(loc);

			PlugFile file = new PlugFile();
			file.PlugFileName = "Interlogic.Trainings.Plugs.Kernel.dll";
			file.RelativeIncomingPath = @"Interlogic.Trainings.Plugs.Kernel\bin\Debug";
			file.DestinationPath = "EXECUTABLE_DIR";
			plug.Files.Add(file);

			file = new PlugFile();
			file.PlugFileName = "Interlogic.Trainings.Plugs.Kernel.pdb";
			file.RelativeIncomingPath = @"Interlogic.Trainings.Plugs.Kernel\bin\Debug";
			file.DestinationPath = "EXECUTABLE_DIR";
			plug.Files.Add(file);

			ClassDefinition classDef = new ClassDefinition();
			classDef.Active = true;
			classDef.ClassDefinitionDescription = "IInstantiatable public interface";
			classDef.ClassName = "Interlogic.Trainings.Plugs.Kernel.IInstantiatable";
			classDef.FileName = "Interlogic.Trainings.Plugs.Kernel.dll";
			plug.ClassDefinitions.Add(classDef);

			PlugInController plugController = new PlugInController(context);
			plugController.InsertAll(plug);
		}

		public override void UpdatePlug(ITransactionContext context)
		{
			throw new NotImplementedException();
		}

		public override void UnregisterPlug(ITransactionContext context)
		{
			throw new NotImplementedException();
		}
	}
}
