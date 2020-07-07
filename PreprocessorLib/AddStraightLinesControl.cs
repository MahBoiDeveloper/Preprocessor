using System;
using System.Drawing;
using System.Windows.Forms;
using ModelComponents;

namespace PreprocessorLib
{
    public partial class AddStraightLinesControl : UserControl
    {
        ProjectForm parent; 
        public AddStraightLinesControl()
        {
            InitializeComponent();
        }

        public AddStraightLinesControl(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.number.ReadOnly = true;
            this.number.BackColor = SystemColors.Window;
            this.number.Text = (parent.currentFullModel.geometryModel.NumOfLines + 1).ToString();
        }

        public void SetCursor()
        {
            this.startPoint.Select();
        }

        private void OK()
        {
            int sp, ep; // начальная и конечная точки

            bool startPointExists = false;
            bool endPointExists = false;

            this.errorSamePoints.Visible = false;

            if (this.number.TextLength == 0 || this.startPoint.TextLength == 0 || this.endPoint.TextLength == 0)
            {
                this.errorMessage1.Visible = true;
                return;
            }
            if (this.errorMessage2.Visible)
            {
                return;
            }
            sp = Convert.ToInt32(this.startPoint.Text);
            ep = Convert.ToInt32(this.endPoint.Text);

            MyPoint point1 = null;
            MyPoint point2 = null;
            foreach (MyPoint point in this.parent.currentFullModel.geometryModel.Points)
            {
                if (point.Id == sp)
                {
                    point1 = point;
                    startPointExists = true; // нашли начальную точку
                }

                if (point.Id == ep)
                {
                    point2 = point;
                    endPointExists = true; // нашли конечную точку
                }
            }

            if (startPointExists && endPointExists)
            {
                this.CreateStraightLine(Convert.ToInt32(this.number.Text), point1, point2);                
            }
        }

        public void CreateStraightLine(int number, MyPoint p1, MyPoint p2)
        {
            if (p1 == p2)
            {
                this.errorSamePoints.Visible = true;
                return;
            }
            MyStraightLine newLine = new MyStraightLine(number, p1, p2);
            this.parent.currentFullModel.geometryModel.StraightLines.Add(newLine);

            this.parent.currentFullModel.geometryModel.NumOfLines++;

            this.number.Text = (parent.currentFullModel.geometryModel.NumOfLines + 1).ToString();
            this.startPoint.Text = "";
            this.endPoint.Text = "";
            this.startPoint.Select();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void startPoint_TextChanged(object sender, EventArgs e)
        {
            if (this.startPoint.TextLength == 0) return;
            try
            {
                Convert.ToUInt32(this.startPoint.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                this.startPoint.Select(0, this.startPoint.TextLength);
            }
        }

        private void endPoint_TextChanged(object sender, EventArgs e)
        {
            if (this.endPoint.TextLength == 0) return;
            try
            {
                Convert.ToUInt32(this.endPoint.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                this.endPoint.Select(0, this.endPoint.TextLength);
            }
        }

        private void AddStraightLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                OK();
            }
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose();
        }

        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                OK();
            }
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose();
        }

        private void startPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                OK();
            }
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose();
        }

        private void endPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                OK();
            }
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose();
        }
    }
}
