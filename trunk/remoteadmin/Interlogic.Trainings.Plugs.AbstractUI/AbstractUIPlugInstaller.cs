using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
    class AbstractUIPlugInstaller: PlugInstaller
    {
        public override void RegisterPlug(ITransactionContext context)
        {
            PlugIn plug = new PlugIn();
            plug.PlugName = "Interlogic.Trainings.Plugs.AbstractUI";
            plug.PlugVersion = "0.0.0.1";
            plug.PlugFriendlyName = "AbstractUI";
            plug.PlugDescription = "AbstractUI Interfaces";
            plug.Active = true;

            PlugFile file = new PlugFile();
            file.PlugFileName = "Interlogic.Trainings.Plugs.AbstractUI.dll";
            file.RelativeIncomingPath = @"Interlogic.Trainings.Plugs.AbstractUI\bin\Debug";
            file.DestinationPath = "EXECUTABLE_DIR";
            plug.Files.Add(file);

            file = new PlugFile();
            file.PlugFileName = "Interlogic.Trainings.Plugs.AbstractUI.pdb";
            file.RelativeIncomingPath = @"Interlogic.Trainings.Plugs.AbstractUI\bin\Debug";
            file.DestinationPath = "EXECUTABLE_DIR";
            plug.Files.Add(file);

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
