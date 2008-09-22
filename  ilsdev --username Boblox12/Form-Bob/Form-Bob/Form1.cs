using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Form_Bob
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void bob(object sender, KeyPressEventArgs e)
    {
      //MessageBox.Show(e.KeyChar.ToString());
    }

    private void bob2(object sender, KeyEventArgs e)
    {
      MessageBox.Show(e.KeyValue.ToString());
    }
  }
}
