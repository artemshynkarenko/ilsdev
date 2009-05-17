using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.AbstractUI;
using Interlogic.Trainings.Plugs.RootContent;

namespace Interlogic.Trainings.Plugs.KernelEditor
{
	/// <summary>
	/// Install editor for kernel entities + default AbstactUI
	/// </summary>
	public class KernelEditorInstaller:PlugInstaller
	{
		#region DefaultContext

		public static AbstractContext GetDefaultContext()
		{
			AbstractContext context = new AbstractContext();
			context.InstanceName = "AbstractUI.DefaultUI.Context";
			context.Left = new ComponentMeasurement(0, ComponentMeasurementUnit.Percentage);
			context.Top = new ComponentMeasurement(0, ComponentMeasurementUnit.Percentage);
			context.Height = new ComponentMeasurement(100, ComponentMeasurementUnit.Percentage);
			context.Width = new ComponentMeasurement(100, ComponentMeasurementUnit.Percentage);
			context.PositioningMethod = ComponentPositioning.Absolute;

			#region Top panel components

			AbstractPanel panelTop = new AbstractPanel();
			panelTop.InstanceName = "AbstractUI.DefaultUI.TopPanel";
			panelTop.Left = new ComponentMeasurement(0, ComponentMeasurementUnit.Pixel);
			panelTop.Top = new ComponentMeasurement(0, ComponentMeasurementUnit.Pixel);
			panelTop.Height = new ComponentMeasurement(10, ComponentMeasurementUnit.Percentage);
			panelTop.Width = new ComponentMeasurement(100, ComponentMeasurementUnit.Percentage);
			panelTop.ParentComponent = context;
			context.Controls.Add(panelTop);

			#endregion

			#region Left panel components

			AbstractPanel panelLeft = new AbstractPanel();
			panelLeft.InstanceName = "AbstractUI.DefaultUI.LeftPanel";
			panelLeft.Left = new ComponentMeasurement(0, ComponentMeasurementUnit.Pixel);
			panelLeft.Top = new ComponentMeasurement(10, ComponentMeasurementUnit.Percentage);
			panelLeft.Height = new ComponentMeasurement(90, ComponentMeasurementUnit.Percentage);
			panelLeft.Width = new ComponentMeasurement(25, ComponentMeasurementUnit.Percentage);
			panelLeft.ParentComponent = context;
			context.Controls.Add(panelLeft);

			AbstractTreeView navigationTree = new AbstractTreeView();
			navigationTree.InstanceName = "AbstractUI.DefaultUI.NafigationTree";//;)
			navigationTree.Left = new ComponentMeasurement(0, ComponentMeasurementUnit.Percentage);
			navigationTree.Top = new ComponentMeasurement(0, ComponentMeasurementUnit.Percentage);
			navigationTree.Height = new ComponentMeasurement(100, ComponentMeasurementUnit.Percentage);
			navigationTree.Width = new ComponentMeasurement(100, ComponentMeasurementUnit.Percentage);
			navigationTree.ParentComponent = panelLeft;
			panelLeft.Controls.Add(navigationTree);

			#endregion

			#region Content panel components

			AbstractPanel panelContent = new AbstractPanel();
			panelContent.InstanceName = "AbstractUI.DefaultUI.ContentPanel";
			panelContent.Left = new ComponentMeasurement(25, ComponentMeasurementUnit.Percentage);
			panelContent.Top = new ComponentMeasurement(10, ComponentMeasurementUnit.Percentage);
			panelContent.Height = new ComponentMeasurement(90, ComponentMeasurementUnit.Percentage);
			panelContent.Width = new ComponentMeasurement(75, ComponentMeasurementUnit.Percentage);
			panelContent.ParentComponent = context;
			context.Controls.Add(panelContent);

			#endregion

			//this is very-very important
			#region Providers

			StaticContentTreeNodeProvider staticContentProvider = new StaticContentTreeNodeProvider();
			staticContentProvider.InstanceName = "AbstractUI.DefaultUI.NavigationTree.StaticContent";
			staticContentProvider.ParentComponent = context;
			navigationTree.NavigationListeners.Add(staticContentProvider);

			#endregion

			return context;
		}

		#endregion

		public static StaticContent GetKernelEditorContent()
		{
			StaticContent root = new StaticContent();
			root.InstanceName = "KernelEditor.StaticContent";
			root.ParentContent = null;
			root.ContentFriendlyName = "Kernel Editor";
			root.ContentImageSrc = "folder.gif";
			root.ContentDescription = "Root node for editing Plugs.Kernel items";

			StaticContent plugs = new StaticContent();
			plugs.InstanceName = "KernelEditor.StaticContent.Plugs";
			plugs.ParentContent = root;
			plugs.ContentFriendlyName = "Registered plugs";
			plugs.ContentImageSrc = "folder.gif";
			plugs.ContentDescription = "Shown registered in system plugs";
			root.ChildContent.Add(plugs);

			StaticContent locations = new StaticContent();
			locations.InstanceName = "KernelEditor.StaticContent.Locations";
			locations.ParentContent = root;
			locations.ContentFriendlyName = "File locations";
			locations.ContentImageSrc = "folder.gif";
			locations.ContentDescription = "Shown registered in system locations";
			root.ChildContent.Add(locations);

			StaticContent uis = new StaticContent();
			uis.InstanceName = "KernelEditor.StaticContent.AbstactUIs";
			uis.ParentContent = root;
			uis.ContentFriendlyName = "Defined UIs";
			uis.ContentImageSrc = "folder.gif";
			uis.ContentDescription = "View and edit UIs";
			root.ChildContent.Add(uis);

			return root;
		}

        public override void RegisterPlug(ITransactionContext context)
        {
            throw new NotImplementedException();
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
