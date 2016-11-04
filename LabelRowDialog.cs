using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TRQuoteCore.GQuoteMonitor
{
    public partial class LabelRowDialog : Form
    {
        public DialogResult result = DialogResult.Cancel;
        public LabelRowDialog()
        {
            InitializeComponent();
        }

        private void buttonCan_Click(object sender, EventArgs e)
        {
            result = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            result = DialogResult.OK;
        }
    }
}