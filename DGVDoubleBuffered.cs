using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace TRQuoteCore.Quotes.Common
{
    public class DGVDoubleBuffered:System.Windows.Forms.DataGridView
    {
        public DGVDoubleBuffered()
        {
            DoubleBuffered = true;
        }
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


    }
}
