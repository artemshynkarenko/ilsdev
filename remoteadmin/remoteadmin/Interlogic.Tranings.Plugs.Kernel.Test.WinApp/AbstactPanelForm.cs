using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Interlogic.Trainings.Plugs.AbstractUI;


namespace Interlogic.Tranings.Plugs.Kernel.Test.WinApp
{
	public partial class AbstactPanelForm : Form
	{
		public AbstactPanelForm()
		{
			InitializeComponent();
			AbstractPanel panel = new AbstractPanel();
			propertyGrid1.SelectedObject = panel;
		}

	}
}