using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Interlogic.Tranings.Plugs.Kernel.Test.WinApp
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			PlugForm form = new PlugForm();
			form.ShowDialog();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			AbstactPanelForm form = new AbstactPanelForm();
			form.ShowDialog();
		}
	}
}