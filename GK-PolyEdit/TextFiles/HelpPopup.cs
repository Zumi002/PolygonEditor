using GK_PolyEdit.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK_PolyEdit.TextFiles
{
    public partial class HelpPopup : Form
    {
        string text;
        public HelpPopup(string fileName)
        {
            InitializeComponent();
            text = Resources.ResourceManager.GetString(fileName);

            if (text == null)
            {
                this.Close();
            }
            textBox1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
