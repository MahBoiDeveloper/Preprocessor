using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PreprocessorUtils
{
    public partial class ProgressDots : Form
    {
        string text;

        public ProgressDots(string text)
        {
            InitializeComponent();
            this.text = text;
            this.Hide();
        }

        private void dotsTimer_Tick(object sender, EventArgs e)
        {
            this.Show();
            if (lblDots.Text.Length < 3) lblDots.Text += '.';
            else lblDots.Text = string.Empty;
            Application.DoEvents();
        }

        private void ProgressDots_Load(object sender, EventArgs e)
        {
            lblProgressText.Text = text;
            lblDots.Left = lblProgressText.Left + lblProgressText.Width;
            this.Width = lblDots.Left + lblDots.Width + lblProgressText.Left;
            dotsTimer.Start();
        }
    }
}
