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

			//PlugInFactory factoryPlug = PlugInFactory.GetInstance();
			//factoryPlug.Context = context as SqlTransactionContext;
			//factoryPlug.InstallRequiredEnvironment();
			PlugIn plug = new PlugIn();
			plug.PlugName = "Interlogic.Trainings.Plugs.Kernel";
			plug.PlugVersion = "0.0.0.1";
			plug.PlugFriendlyName = "RemoteAdmin Kernel";
			plug.PlugDescription = "RemoteAdmin Kernel";
			plug.Active = true;

			//PlugLocationFactory factoryPlugLocation = PlugLocationFactory.GetInstance();
			//factoryPlugLocation.Context = context as SqlTransactionContext;
			//factoryPlugLocation.InstallRequiredEnvironment();
			PlugLocation loc = new PlugLocation();
			loc.PlugLocationName = "EXECUTABLE_DIR";
			loc.PlugLocationPath = Path.Combine(InitialDir, @"bin");
			loc.PlugLocationDescription = "Main executable directory";
			plug.Locations.Add(loc);

			//PlugFileFactory factoryPlugFile = PlugFileFactory.GetInstance();
			//factoryPlugFile.Context = context as SqlTransactionContext;
			//factoryPlugFile.InstallRequiredEnvironment();
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

			//ClassDefinitionFactory factoryClassDefinition = ClassDefinitionFactory.GetInstance();
			//factoryClassDefinition.Context = context as SqlTransactionContext;
			//factoryClassDefinition.InstallRequiredEnvironment();
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
