using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Interlogic.Trainings.Plugs.Kernel;
using System.Reflection;

namespace Interlogic.Trainings.Plugs.InstallApp
{
	public partial class PlugListForm : Form
	{
        ITransactionContext _context;
		public PlugListForm()
		{
			InitializeComponent();
		}
        public void LoadPlugin(string pluginPath)
        {
            bool found=false ;
            PlugInController controller = new PlugInController(_context);
            Assembly assembly = Assembly.LoadFile(pluginPath);
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(PlugInstaller)))
                {
                    Console.WriteLine("Install {0}", type.Name);
                    PlugInstaller installer= (PlugInstaller) Activator.CreateInstance(type);
                    
                    installer.RegisterPlug(_context);
                    found = true;
                    
                }
            }
            if (found == false)
            {
                Console.WriteLine("No plugins found in {0}", pluginPath);
            }
            else
            {
                
            }
            
        }
        public  void ShowPlugins(ITransactionContext context)
        {
            _context = context;
            RefreshPlugins();
        }

        private void RefreshPlugins()
        {
            PlugInController controller = new PlugInController(_context);
            List<PlugIn> plugs = controller.LoadAll();
            PluginListView.Items.Clear();
            foreach (PlugIn plugin in plugs)
            {
                ListViewItem item;
                item = PluginListView.Items.Add(plugin.PlugFriendlyName);
                item.SubItems.Add(plugin.PlugVersion);
                item.SubItems.Add(plugin.PlugDescription);
                item.Tag = plugin.PlugId;

            }
        }

void InstallNewPlugin()
{
             openFileDialog.Filter = "Assembly|*.dll";
             if (openFileDialog.ShowDialog() == DialogResult.OK)
             {
                // folderBrowserDialog.Description = "Choose initial folder";
                // if (folderBrowserDialog.ShowDialog() = DialogResult.OK)
                // {
                     ProgressForm form = new ProgressForm();
                     form.Text = "Installing new plugin";
                     form.Show();
                     string pluginPath = openFileDialog.FileName;

                     try
                     {
                         LoadPlugin(pluginPath);//,folderBrowserDialog.SelectedPath );
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(ex);
                     }
                     form.Hide();
                     form.ShowDialog();
                     RefreshPlugins();
                 //}
             }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            {
                /*textFileName.Text = openFileDialog.FileName;*/
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuPlugin_Opening(object sender, CancelEventArgs e)
        {
            if (PluginListView.SelectedItems.Count > 0)
            {
                /*Uninstall*/
            }
            else
            {

            }
        }

        private void PluginListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void installNewPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstallNewPlugin();
        }
	}
}
