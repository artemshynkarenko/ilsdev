using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.IO;


namespace FileManager
{
  public partial class FormFileManager : Form
  {
    public FormFileManager()
    {
      InitializeComponent();
    }

    public static void ShowDirectory(TreeNode node,int status)
    {
      if (status <= 1)
      {
        DirectoryInfo directory = new DirectoryInfo(node.FullPath + "\\");        
        foreach (DirectoryInfo subDirectory in directory.GetDirectories())
        {
          if (subDirectory.Name != " " && subDirectory.Exists && !((subDirectory.Attributes & FileAttributes.System) == FileAttributes.System))
          {
            node.Nodes.Add(subDirectory.Name);                        
            ShowDirectory(node.Nodes[node.Nodes.Count - 1], status + 1);
          }
        }
      }
    }

    private void FormFileManagerShown(object sender, EventArgs e)
    {
      TreeViewDirectories.BeginUpdate();

      foreach (DriveInfo drive in DriveInfo.GetDrives())
      {
        string s = drive.Name.Remove(drive.Name.Length - 1);
        TreeViewDirectories.Nodes.Add(s);
        if (drive.IsReady)
        {
          ShowDirectory(TreeViewDirectories.Nodes[TreeViewDirectories.Nodes.Count - 1],0);
        }
      }

      TreeViewDirectories.EndUpdate();
    }

    private void TreeViewDirectoriesAfterExpand(object sender, TreeViewEventArgs e)
    {
      e.Node.TreeView.BeginUpdate();
      
      e.Node.Nodes.Clear();
      ShowDirectory(e.Node, 0);

      e.Node.TreeView.EndUpdate();
    }

    private void TreeViewDirectoriesAfterSelect(object sender, TreeViewEventArgs e)
    {
      DirectoryInfo directory = new DirectoryInfo(e.Node.FullPath+"\\");
      if (directory.Exists)
      {
        ListViewFiles.Items.Clear();
        foreach (FileInfo file in directory.GetFiles())
        {
          if(file.Name != " " && file.Exists)
          {
            ListViewItem item = new ListViewItem(file.Name);
            item.SubItems.Add(file.Extension.ToString());
            item.SubItems.Add(file.Length.ToString());          
            ListViewFiles.Items.Add(item);
          }
        }
      }
      else
      {
        ListViewFiles.Items.Clear();
        if (e.Node.Parent != null)
        {
          e.Node.Remove();
        }
      }
    }

    private void ListViewFilesKeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyValue == 46 && ListViewFiles.SelectedItems.Count>0)
      {
        DialogResult result = MessageBox.Show("Ви дійсно хочете  видалити ці файли?", "File Mananger", MessageBoxButtons.OKCancel);
        if (result == DialogResult.OK)
        {
          foreach (ListViewItem item in ListViewFiles.SelectedItems)
          {
            FileInfo file = new FileInfo(TreeViewDirectories.SelectedNode.FullPath + "\\" + item.Text);
            item.Remove();
            if (file.Exists)
            {
              file.Delete();
              //MessageBox.Show(file.FullName, "File Mananger");
            }
            else
            {
              MessageBox.Show("Файлу "+file.FullName+" не існує!", "File Mananger");
            }
          }
        }
      }
    }

    private void ListViewFilesItemActivate(object sender, EventArgs e)
    {
      try
      {
        FileInfo file = new FileInfo(TreeViewDirectories.SelectedNode.FullPath + "\\" + ListViewFiles.FocusedItem.Text);
        if (file.Exists)
        {
          System.Diagnostics.Process.Start(file.FullName);
        }
        else
        {
          MessageBox.Show("Файлу " + file.FullName + " не існує!", "File Mananger");
        }
      }
      catch{
        MessageBox.Show("Неможливо відкрити файл!", "File Mananger");
      }
    }
  }
}
