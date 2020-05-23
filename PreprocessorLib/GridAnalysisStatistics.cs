using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PreprocessorLib
{
    public partial class GridAnalysisStatistics : Form
    {
        public string StatText
        {
            get { return txtStatistics.Text; }
            set { txtStatistics.Text = value; }
        }
        public GridAnalysisStatistics()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GridAnalysisStatistics_Load(object sender, EventArgs e)
        {
            txtStatistics.Select(0, 0);
        }
    }
}
