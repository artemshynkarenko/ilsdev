using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.IO;
using Interlogic.Trainings.Plugs.Kernel.FileActions;

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

            FileTransaction trans = new FileTransaction();
            trans.BeginTransaction();

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

            string EXEC_DIR = loc.PlugLocationPath;

			PlugFile file = new PlugFile();
			file.PlugFileName = "Interlogic.Trainings.Plugs.Kernel.dll";
            file.RelativeIncomingPath = @"..\..\..\Interlogic.Trainings.Plugs.Kernel\bin\Debug";
			file.DestinationPath = "EXECUTABLE_DIR";
			plug.Files.Add(file);
            trans.AddAction(new CopyFileAction(Path.Combine(file.RelativeIncomingPath, file.PlugFileName), Path.Combine(EXEC_DIR, file.PlugFileName), true));

			file = new PlugFile();
			file.PlugFileName = "Interlogic.Trainings.Plugs.Kernel.pdb";
            file.RelativeIncomingPath = @"..\..\..\Interlogic.Trainings.Plugs.Kernel\bin\Debug";
            file.DestinationPath = "EXECUTABLE_DIR";
			plug.Files.Add(file);
            trans.AddAction(new CopyFileAction(Path.Combine(file.RelativeIncomingPath,file.PlugFileName), Path.Combine(EXEC_DIR,file.PlugFileName), true));

			ClassDefinition classDef = new ClassDefinition();
			classDef.Active = true;
			classDef.ClassDefinitionDescription = "IInstantiatable public interface";
			classDef.ClassName = "Interlogic.Trainings.Plugs.Kernel.IInstantiatable";
			classDef.FileName = "Interlogic.Trainings.Plugs.Kernel.dll";
			plug.ClassDefinitions.Add(classDef);

            try
            {
                trans.Execute();
                trans.Commit();
                PlugInController plugController = new PlugInController(context);
                plugController.InsertAll(plug);
            }
            catch (Exception e)
            {
                trans.RollBack();
                throw new Exception("Kernel Installation Process Failed!", e);
            }
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
