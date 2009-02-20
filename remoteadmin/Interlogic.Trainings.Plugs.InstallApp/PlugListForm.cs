using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Interlogic.Trainings.Plugs.InstallApp
{
	public partial class PlugListForm : Form
	{
		public PlugListForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ProgressForm form = new ProgressForm();
			form.Text = "Installing new module";
			form.ShowDialog();
		}
	}
}
