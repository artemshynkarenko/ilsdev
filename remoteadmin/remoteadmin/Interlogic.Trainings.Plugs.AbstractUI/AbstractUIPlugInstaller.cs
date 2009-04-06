using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.AbstractUI;
using Interlogic.Trainings.Plugs.Kernel.FileActions;
using System.IO;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
    public class AbstractUIPlugInstaller: PlugInstaller
    {
        public override void RegisterPlug(ITransactionContext context)
        {
            FileTransaction trans = new FileTransaction();
            trans.BeginTransaction();

            PlugLocationFactory plugLocFactory = PlugLocationFactory.GetInstance();
            plugLocFactory.Context = context as ISqlTransactionContext;
            string EXEC_DIR = plugLocFactory.InternalLoadByName("EXECUTABLE_DIR").PlugLocationPath;

            PlugIn plug = new PlugIn();
            plug.PlugName = "Interlogic.Trainings.Plugs.AbstractUI";
            plug.PlugVersion = "0.0.0.1";
            plug.PlugFriendlyName = "AbstractUI";
            plug.PlugDescription = "AbstractUI Interfaces";
            plug.Active = true;

            PlugFile file = new PlugFile();
            file.PlugFileName = "Interlogic.Trainings.Plugs.AbstractUI.dll";
            file.RelativeIncomingPath = @"..\..\..\Interlogic.Trainings.Plugs.AbstractUI\bin\Debug";
            file.DestinationPath = "EXECUTABLE_DIR";
            plug.Files.Add(file);
            trans.AddAction(new CopyFileAction(Path.Combine(file.RelativeIncomingPath, file.PlugFileName), Path.Combine(EXEC_DIR, file.PlugFileName), true));

            file = new PlugFile();
            file.PlugFileName = "Interlogic.Trainings.Plugs.AbstractUI.pdb";
            file.RelativeIncomingPath = @"..\..\..\Interlogic.Trainings.Plugs.AbstractUI\bin\Debug";
            file.DestinationPath = "EXECUTABLE_DIR";
            plug.Files.Add(file);
            trans.AddAction(new CopyFileAction(Path.Combine(file.RelativeIncomingPath, file.PlugFileName), Path.Combine(EXEC_DIR, file.PlugFileName), true));

            ClassDefinition classDef = new ClassDefinition();
            classDef.Active = true;
            classDef.ClassName = "Interlogic.Trainings.Plugs.Kernel.IAbstractComponent";
            classDef.FileName = "Interlogic.Trainings.Plugs.AbstractUI.dll";
            classDef.ClassDefinitionDescription = "IAbstractComponent public interface";
            plug.ClassDefinitions.Add(classDef);

            classDef = new ClassDefinition();
            classDef.Active = true;
            classDef.ClassName = "Interlogic.Trainings.Plugs.Kernel.IAbstractContainer";
            classDef.FileName = "Interlogic.Trainings.Plugs.AbstractUI.dll";
            classDef.ClassDefinitionDescription = "IAbstractContainer public interface";
            plug.ClassDefinitions.Add(classDef);

            classDef = new ClassDefinition();
            classDef.Active = true;
            classDef.ClassName = "Interlogic.Trainings.Plugs.Kernel.INavigationComponent";
            classDef.FileName = "Interlogic.Trainings.Plugs.AbstractUI.dll";
            classDef.ClassDefinitionDescription = "INavigationComponent public interface";
            plug.ClassDefinitions.Add(classDef);

            classDef = new ClassDefinition();
            classDef.Active = true;
            classDef.ClassName = "Interlogic.Trainings.Plugs.Kernel.INavigationListenerComponent";
            classDef.FileName = "Interlogic.Trainings.Plugs.AbstractUI.dll";
            classDef.ClassDefinitionDescription = "INavigationListenerComponent public interface";
            plug.ClassDefinitions.Add(classDef);

            BindablePointDefinition bpd = new BindablePointDefinition();
            bpd.BindablePointName = AbstractUiConstants.IAbstractUiContainer_Controls_BindingPointName;
            bpd.BindablePointFriendlyName = AbstractUiConstants.IAbstractUiContainer_Controls_BindingPointName;
            bpd.ClassDefinitionName = "Interlogic.Trainings.Plugs.Kernel.IAbstractContainer";
            plug.BindablePointDefinitions.Add(bpd);

            bpd = new BindablePointDefinition();
            bpd.BindablePointName = AbstractUiConstants.IAbstractUiNavigationControl_NavigationListeners_BindingPointName;
            bpd.BindablePointFriendlyName = AbstractUiConstants.IAbstractUiNavigationControl_NavigationListeners_BindingPointName;
            bpd.ClassDefinitionName = "Interlogic.Trainings.Plugs.Kernel.INavigationComponent";
            plug.BindablePointDefinitions.Add(bpd);

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
                throw new Exception("AbstractUI Installation Process Failed!", e);
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
