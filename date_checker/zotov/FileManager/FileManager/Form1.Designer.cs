namespace FileManager
{
  partial class FormFileManager
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFileManager));
      this.ListViewFiles = new System.Windows.Forms.ListView();
      this.ColumnHeaderFileName = new System.Windows.Forms.ColumnHeader();
      this.ColumnHeaderFileExtension = new System.Windows.Forms.ColumnHeader();
      this.ColumnHeaderFileSize = new System.Windows.Forms.ColumnHeader();
      this.TreeViewDirectories = new System.Windows.Forms.TreeView();
      this.SuspendLayout();
      // 
      // ListViewFiles
      // 
      this.ListViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeaderFileName,
            this.ColumnHeaderFileExtension,
            this.ColumnHeaderFileSize});
      this.ListViewFiles.Location = new System.Drawing.Point(321, 12);
      this.ListViewFiles.Name = "ListViewFiles";
      this.ListViewFiles.Size = new System.Drawing.Size(455, 575);
      this.ListViewFiles.TabIndex = 0;
      this.ListViewFiles.UseCompatibleStateImageBehavior = false;
      this.ListViewFiles.View = System.Windows.Forms.View.Details;
      this.ListViewFiles.ItemActivate += new System.EventHandler(this.ListViewFilesItemActivate);
      this.ListViewFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewFilesKeyDown);
      // 
      // ColumnHeaderFileName
      // 
      this.ColumnHeaderFileName.Text = "File Name";
      this.ColumnHeaderFileName.Width = 181;
      // 
      // ColumnHeaderFileExtension
      // 
      this.ColumnHeaderFileExtension.DisplayIndex = 2;
      this.ColumnHeaderFileExtension.Text = "Extension";
      // 
      // ColumnHeaderFileSize
      // 
      this.ColumnHeaderFileSize.DisplayIndex = 1;
      this.ColumnHeaderFileSize.Text = "File Size";
      this.ColumnHeaderFileSize.Width = 131;
      // 
      // TreeViewDirectories
      // 
      this.TreeViewDirectories.Location = new System.Drawing.Point(12, 12);
      this.TreeViewDirectories.Name = "TreeViewDirectories";
      this.TreeViewDirectories.Size = new System.Drawing.Size(303, 575);
      this.TreeViewDirectories.TabIndex = 1;
      this.TreeViewDirectories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewDirectoriesAfterSelect);
      this.TreeViewDirectories.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewDirectoriesAfterExpand);
      // 
      // FormFileManager
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(788, 599);
      this.Controls.Add(this.TreeViewDirectories);
      this.Controls.Add(this.ListViewFiles);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormFileManager";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "File Manager";
      this.Shown += new System.EventHandler(this.FormFileManagerShown);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView ListViewFiles;
    private System.Windows.Forms.TreeView TreeViewDirectories;
    private System.Windows.Forms.ColumnHeader ColumnHeaderFileName;
    private System.Windows.Forms.ColumnHeader ColumnHeaderFileSize;
    private System.Windows.Forms.ColumnHeader ColumnHeaderFileExtension;
  }
}

