using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK_PolyEdit.Relations
{
    public partial class LengthRelationDialog : Form
    {
        public int value;
        public LengthRelationDialog(int value)
        {
            InitializeComponent();
            this.value = value;
            DialogResult = DialogResult.Cancel;
            LengthBox.Value = value;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            value = (int)LengthBox.Value;
            DialogResult = DialogResult.OK;
        }
    }
}
