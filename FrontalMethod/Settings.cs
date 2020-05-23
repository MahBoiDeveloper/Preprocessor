using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FrontalMethod
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = "";
            for (int i = 0; i < maxAngle.Text.Length - 1; i++)
                text += maxAngle.Text[i];
            if (maxAngle.Text.Length >= 1)
                if (!((maxAngle.Text[maxAngle.Text.Length - 1] >= '0') && (maxAngle.Text[maxAngle.Text.Length - 1] <= '9')) && (!(maxAngle.Text[maxAngle.Text.Length - 1] == '.')))
                    maxAngle.Text = text;
        }
    }
}
