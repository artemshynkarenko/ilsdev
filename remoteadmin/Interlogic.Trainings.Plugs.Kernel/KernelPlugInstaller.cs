using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	class KernelPlugInstaller: PlugInstaller
	{
        public override void RegisterPlug(ITransactionContext context)
        {
            PlugIn plug = new PlugIn();
            plug.PlugName = "Interlogic.Trainings.Plugs.Kernel";
            plug.PlugVersion = "0.0.0.1";
            plug.PlugFriendlyName = "RemoteAdmin Kernel";
            plug.PlugDescription = "RemoteAdmin Kernel";
            plug.Active = true;

            PlugLocation loc = new PlugLocation();
            loc.PlugLocationName = "EXECUTABLE_DIR";
            loc.PlugLocationPath = @"c:\Interlogic.Trainings.RemoteAdmin\bin";
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
