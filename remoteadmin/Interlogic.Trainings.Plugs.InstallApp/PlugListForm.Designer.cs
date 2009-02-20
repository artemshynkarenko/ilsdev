namespace Interlogic.Trainings.Plugs.InstallApp
{
	partial class PlugListForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBoxInstallNew = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBoxInstallNew.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.listView1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(734, 554);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.groupBoxInstallNew);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 554);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(734, 100);
			this.panel2.TabIndex = 1;
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(734, 554);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.List;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "PlugIn Name";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "PlugIn Alias";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(24, 19);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(284, 20);
			this.textBox1.TabIndex = 0;
			// 
			// groupBoxInstallNew
			// 
			this.groupBoxInstallNew.Controls.Add(this.button2);
			this.groupBoxInstallNew.Controls.Add(this.button1);
			this.groupBoxInstallNew.Controls.Add(this.textBox1);
			this.groupBoxInstallNew.Location = new System.Drawing.Point(125, 0);
			this.groupBoxInstallNew.Name = "groupBoxInstallNew";
			this.groupBoxInstallNew.Size = new System.Drawing.Size(436, 100);
			this.groupBoxInstallNew.TabIndex = 1;
			this.groupBoxInstallNew.TabStop = false;
			this.groupBoxInstallNew.Text = "Install New";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(24, 45);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Install";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(325, 16);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Browse";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// PlugListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(734, 654);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Name = "PlugListForm";
			this.Text = "PlugListForm";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.groupBoxInstallNew.ResumeLayout(false);
			this.groupBoxInstallNew.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.GroupBox groupBoxInstallNew;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}