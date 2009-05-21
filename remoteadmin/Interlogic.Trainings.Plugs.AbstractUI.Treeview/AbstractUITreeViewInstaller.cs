using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.AbstractUI;
using Interlogic.Trainings.Plugs.Kernel.FileActions;
using System.IO;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;

namespace Interlogic.Trainings.Plugs.AbstractUI.TreeView
{
    public class AbstractUITreeViewInstaller : PlugInstaller
    {
        public override void RegisterPlug(ITransactionContext context)
        {
            Console.WriteLine("Installing AbstractUI.Treeview");
            FileTransaction trans = new FileTransaction();
            trans.BeginTransaction();

            PlugLocationFactory plugLocFactory = PlugLocationFactory.GetInstance();
            plugLocFactory.Context = context as ISqlTransactionContext;
            string EXEC_DIR = new PlugLocationController(context).LoadByName("EXECUTABLE_DIR").PlugLocationPath;

            PlugIn plug = new PlugIn();
            plug.PlugName = "Interlogic.Trainings.Plugs.AbstractUI.TreeView";
            plug.PlugVersion = "0.0.0.1";
            plug.PlugFriendlyName = "AbstractUI.TreeView";
            plug.PlugDescription = "AbstractUI.TreeView Interfaces";
            plug.Active = true;

            PlugFile file = new PlugFile();
            file.PlugFileName = "Interlogic.Trainings.Plugs.AbstractUI.TreeView.dll";
            file.RelativeIncomingPath = @"..\..\..\Interlogic.Trainings.Plugs.AbstractUI.TreeView\bin\Debug";
            file.DestinationPath = "EXECUTABLE_DIR";
            plug.Files.Add(file);
            trans.AddAction(new CopyFileAction(Path.Combine(file.RelativeIncomingPath, file.PlugFileName), Path.Combine(EXEC_DIR, file.PlugFileName), true));

            file = new PlugFile();
            file.PlugFileName = "Interlogic.Trainings.Plugs.AbstractUI.TreeView.pdb";
            file.RelativeIncomingPath = @"..\..\..\Interlogic.Trainings.Plugs.AbstractUI.TreeView\bin\Debug";
            file.DestinationPath = "EXECUTABLE_DIR";
            plug.Files.Add(file);
            trans.AddAction(new CopyFileAction(Path.Combine(file.RelativeIncomingPath, file.PlugFileName), Path.Combine(EXEC_DIR, file.PlugFileName), true));

            ClassDefinition classDef = new ClassDefinition();
            classDef.Active = true;
            classDef.ClassName = "Interlogic.Trainings.Plugs.AbstractUI.TreeView.IAbstractStartupTreeNodeProvider";
            classDef.FileName = "Interlogic.Trainings.Plugs.AbstractUI.TreeView.dll";
            classDef.ClassDefinitionDescription = "IAbstractStartupTreeNodeProvider public interface";
            plug.ClassDefinitions.Add(classDef);

            classDef = new ClassDefinition();
            classDef.Active = true;
            classDef.ClassName = "Interlogic.Trainings.Plugs.AbstractUI.TreeView.IAbstractTreeNodeProvider";
            classDef.FileName = "Interlogic.Trainings.Plugs.AbstractUI.TreeView.dll";
            classDef.ClassDefinitionDescription = "IAbstractTreeNodeProvider public interface";
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
                Console.WriteLine("AbstractUI.Treeview installation failed");
                trans.RollBack();
                throw new Exception("AbstractUI.TreeView Installation Process Failed!", e);
            }
            Console.WriteLine("AbstractUI.Treeview successfully installed");
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
