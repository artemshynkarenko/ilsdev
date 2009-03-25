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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PluginListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuPlugin = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.installNewPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uninstallPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.menuPlugin.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PluginListView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(734, 654);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // PluginListView
            // 
            this.PluginListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader1});
            this.PluginListView.ContextMenuStrip = this.menuPlugin;
            this.PluginListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginListView.FullRowSelect = true;
            this.PluginListView.Location = new System.Drawing.Point(0, 0);
            this.PluginListView.Name = "PluginListView";
            this.PluginListView.Size = new System.Drawing.Size(734, 654);
            this.PluginListView.TabIndex = 0;
            this.PluginListView.UseCompatibleStateImageBehavior = false;
            this.PluginListView.View = System.Windows.Forms.View.Details;
            this.PluginListView.SelectedIndexChanged += new System.EventHandler(this.PluginListView_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Version";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Description";
            this.columnHeader5.Width = 500;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Path";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // menuPlugin
            // 
            this.menuPlugin.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.installNewPluginToolStripMenuItem,
            this.uninstallPluginToolStripMenuItem,
            this.updateToolStripMenuItem});
            this.menuPlugin.Name = "menuPlugin";
            this.menuPlugin.Size = new System.Drawing.Size(135, 70);
            this.menuPlugin.Opening += new System.ComponentModel.CancelEventHandler(this.menuPlugin_Opening);
            // 
            // installNewPluginToolStripMenuItem
            // 
            this.installNewPluginToolStripMenuItem.Name = "installNewPluginToolStripMenuItem";
            this.installNewPluginToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.installNewPluginToolStripMenuItem.Text = "Install ";
            this.installNewPluginToolStripMenuItem.Click += new System.EventHandler(this.installNewPluginToolStripMenuItem_Click);
            // 
            // uninstallPluginToolStripMenuItem
            // 
            this.uninstallPluginToolStripMenuItem.Enabled = false;
            this.uninstallPluginToolStripMenuItem.Name = "uninstallPluginToolStripMenuItem";
            this.uninstallPluginToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.uninstallPluginToolStripMenuItem.Text = "Uninstall ";
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Enabled = false;
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.updateToolStripMenuItem.Text = "Update ";
            // 
            // PlugListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 654);
            this.Controls.Add(this.panel1);
            this.Name = "PlugListForm";
            this.Text = "PlugListForm";
            this.panel1.ResumeLayout(false);
            this.menuPlugin.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView PluginListView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ContextMenuStrip menuPlugin;
        private System.Windows.Forms.ToolStripMenuItem installNewPluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uninstallPluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
	}
}