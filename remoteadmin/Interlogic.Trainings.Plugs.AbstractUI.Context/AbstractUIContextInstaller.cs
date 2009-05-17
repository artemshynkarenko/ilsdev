using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.AbstractUI;
using Interlogic.Trainings.Plugs.Kernel.FileActions;
using System.IO;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;

namespace Interlogic.Trainings.Plugs.AbstractUI.Context
{
    public class AbstractUIContextInstaller : PlugInstaller
    {
        public override void RegisterPlug(ITransactionContext context)
        {
            Console.WriteLine("Installing AbstractUI.Context");
            FileTransaction trans = new FileTransaction();
            trans.BeginTransaction();

            PlugLocationFactory plugLocFactory = PlugLocationFactory.GetInstance();
            plugLocFactory.Context = context as ISqlTransactionContext;
            string EXEC_DIR = new PlugLocationController(context).LoadByName("EXECUTABLE_DIR").PlugLocationPath;

            PlugIn plug = new PlugIn();
            plug.PlugName = "Interlogic.Trainings.Plugs.AbstractUI.Context";
            plug.PlugVersion = "0.0.0.1";
            plug.PlugFriendlyName = "AbstractUI.Context";
            plug.PlugDescription = "AbstractUI.Context Interfaces";
            plug.Active = true;

            PlugFile file = new PlugFile();
            file.PlugFileName = "Interlogic.Trainings.Plugs.AbstractUI.Context.dll";
            file.RelativeIncomingPath = @"..\..\..\Interlogic.Trainings.Plugs.AbstractUI.Context\bin\Debug";
            file.DestinationPath = "EXECUTABLE_DIR";
            plug.Files.Add(file);
            trans.AddAction(new CopyFileAction(Path.Combine(file.RelativeIncomingPath, file.PlugFileName), Path.Combine(EXEC_DIR, file.PlugFileName), true));

            file = new PlugFile();
            file.PlugFileName = "Interlogic.Trainings.Plugs.AbstractUI.Context.pdb";
            file.RelativeIncomingPath = @"..\..\..\Interlogic.Trainings.Plugs.AbstractUI.Context\bin\Debug";
            file.DestinationPath = "EXECUTABLE_DIR";
            plug.Files.Add(file);
            trans.AddAction(new CopyFileAction(Path.Combine(file.RelativeIncomingPath, file.PlugFileName), Path.Combine(EXEC_DIR, file.PlugFileName), true));

            try
            {
                trans.Execute();
                trans.Commit();
                PlugInController plugController = new PlugInController(context);
                plugController.InsertAll(plug);
            }
            catch (Exception e)
            {
                Console.WriteLine("AbstractUI.Context installation failed");
                trans.RollBack();
                throw new Exception("AbstractUI.Context Installation Process Failed!", e);
            }
            Console.WriteLine("AbstractUI.Context successfully installed");
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
