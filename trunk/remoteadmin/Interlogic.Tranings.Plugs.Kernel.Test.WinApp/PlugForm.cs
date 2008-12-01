using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Data.SqlClient;

namespace Interlogic.Tranings.Plugs.Kernel.Test.WinApp
{
	public partial class PlugForm : Form
	{
		public PlugForm()
		{
			InitializeComponent();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			this.propertyGrid1.SelectedObject = new PlugIn();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			 SqlTransactionContext context = new SqlTransactionContext();
            //context.Connection = new SqlConnection("server=stranger;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
            context.Connection = new SqlConnection("server=localhost;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
			PlugInController controller = new PlugInController(context);
			
				controller.Insert(this.propertyGrid1.SelectedObject as PlugIn);
			
		}

		private void button2_Click(object sender, EventArgs e)
		{
			SqlTransactionContext context = new SqlTransactionContext();
			//context.Connection = new SqlConnection("server=stranger;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
			context.Connection = new SqlConnection("server=localhost;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
			PlugInController controller = new PlugInController(context);
			
				controller.Update(this.propertyGrid1.SelectedObject as PlugIn);
			
		}

		private void button3_Click(object sender, EventArgs e)
		{
			SqlTransactionContext context = new SqlTransactionContext();
			//context.Connection = new SqlConnection("server=stranger;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
			context.Connection = new SqlConnection("server=localhost;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
			PlugInController controller = new PlugInController(context);
			
			controller.Delete(this.propertyGrid1.SelectedObject as PlugIn);
			
		}

		private void button4_Click(object sender, EventArgs e)
		{
			SqlTransactionContext context = new SqlTransactionContext();
			//context.Connection = new SqlConnection("server=stranger;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
			context.Connection = new SqlConnection("server=localhost;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
			PlugInController controller = new PlugInController(context);
			{
				controller.UpdateAll(this.propertyGrid1.SelectedObject as PlugIn);
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			SqlTransactionContext context = new SqlTransactionContext();
			//context.Connection = new SqlConnection("server=stranger;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
			context.Connection = new SqlConnection("server=localhost;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
			PlugInController controller = new PlugInController(context);
			{
				//controller.Get(this.propertyGrid1.SelectedObject as PlugIn);
			}
		}

		private void button5_Click_1(object sender, EventArgs e)
		{
			SqlTransactionContext context = new SqlTransactionContext();
			//context.Connection = new SqlConnection("server=stranger;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
			context.Connection = new SqlConnection("server=localhost;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
			PlugInController controller = new PlugInController(context);
			{
				propertyGrid1.SelectedObject = controller.LoadAll();
			}
		}
	}
}