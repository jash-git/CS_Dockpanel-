using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace WeifenLuoUISample
{
    public partial class MainWnd : Form
    {
        private static string _dockpanelConfigFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockManager.config");
        private Form1 _form1 = new Form1();
        private Form2 _form2 = new Form2();

        public MainWnd()
        {
            InitializeComponent();
        }

        private void MainWnd_Load(object sender, EventArgs e)
        {         
            try
            {
                if (File.Exists(_dockpanelConfigFile))
                    _dockPanel.LoadFromXml(_dockpanelConfigFile, new DeserializeDockContent(GetDeserializeDockContent));
            }
            catch (Exception)
            {
            }

        }

        public IDockContent GetDeserializeDockContent(string persistString)
        {
            if (persistString == typeof(Form1).ToString())
                return _form1;
            if (persistString == typeof(Form2).ToString())
                return _form2;
            return null;
        }    

        private void MainWnd_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dockPanel.SaveAsXml(_dockpanelConfigFile);    
        }

        private void form1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _form1.Show(_dockPanel, DockState.DockLeft);            
        }

        private void form2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _form2.Show(_dockPanel, DockState.Document);
        }
    }
}
