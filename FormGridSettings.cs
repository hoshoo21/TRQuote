using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TRQuoteCore.GridStructure;

namespace TRQuoteCore.Quotes.Common
{
    public partial class FormGridSettings : Form
    {
        public GridSettings Settings
        {
            get;
            set;
        }
        public FormGridSettings(GridSettings settings)
        {
            InitializeComponent();
            this.propertyGrid.SelectedObject = settings;
            Settings = settings;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GridSettings s = this.propertyGrid.SelectedObject as GridSettings;
            if (s != null)
            {
                s.BackColor = Color.White;
                s.DisplayFont = Font;
                s.ForeColor = Color.Black;
                s.GridLineColor = Color.Silver;
                s.RowHeight = 20;
                
                s.SelectionColor = Color.FromArgb(255,255,192);
                s.SelectionForeColor = Color.Black;

            }
            propertyGrid.Refresh();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GridSettings s = this.propertyGrid.SelectedObject as GridSettings;
            if (s != null)
            {
                s.BackColor = Color.Black;
                s.DisplayFont = new Font("Tahoma", 15f, FontStyle.Bold);
                s.ForeColor = Color.LawnGreen;
                s.GridLineColor = Color.Black;
                s.RowHeight = 25;
                s.SelectionColor = Color.Silver;
                s.SelectionForeColor = Color.White;

            }
            propertyGrid.Refresh();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GridSettings s = this.propertyGrid.SelectedObject as GridSettings;
            if (s != null)
            {
                s.DisplayFont = new Font("Bookman Old Style", 15f, FontStyle.Bold, GraphicsUnit.Pixel);
                s.BackColor = Color.Khaki;
                s.ForeColor = Color.Black;
                s.GridLineColor = Color.Silver;
                s.SelectionColor = Color.Gray;
                s.SelectionForeColor = Color.White;
                s.RowHeight = 20;
            }

            propertyGrid.Refresh();

        }

        private void buttonOK_MouseDown(object sender, MouseEventArgs e)
        {
            if (Settings.BackColor == Color.Transparent || Settings.ForeColor == Color.Transparent || Settings.GridLineColor == Color.Transparent
                || Settings.SelectionColor == Color.Transparent || Settings.SelectionForeColor == Color.Transparent)
            {
                MessageBox.Show("Transparent color is not allowded.Please Select the appropriate colors.");
                buttonOK.DialogResult = DialogResult.Cancel;
                return;
            }
            else
            {
                buttonOK.DialogResult = DialogResult.OK;
            }
        }
    }
}
