using System;
using System.Drawing;
using System.Windows.Forms;
using ModelComponents;

namespace PreprocessorLib
{
    public partial class AddCirclesControl : UserControl
    {
        ProjectForm parent;
        public AddCirclesControl()
        {
            InitializeComponent();
        }

        public AddCirclesControl(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.number.ReadOnly = true;
            this.number.BackColor = SystemColors.Window;
            this.number.Text = (parent.currentFullModel.geometryModel.NumOfCircles + 1).ToString();
        }

        public void SetCursor()
        {
            this.centerPoint.Select();
        }

        private void OK()
        {
            double rad;
            int center;
            bool centerPointExists = false;
            if (this.number.TextLength == 0 || this.centerPoint.TextLength == 0 || this.radius.TextLength == 0)
            {
                this.errorMessage1.Visible = true;
                return;
            }
            if (this.errorMessage2.Visible)
            {
                return;
            }
            center = Convert.ToInt32(this.centerPoint.Text);
            rad = Convert.ToDouble(this.radius.Text);

            MyPoint point1 = null;
            foreach (MyPoint point in this.parent.currentFullModel.geometryModel.Points)
            {
                if (point.Id == center)
                {
                    point1 = point;
                    centerPointExists = true; // нашли центральую точку
                }
            }

            if (centerPointExists)
            {
                this.errorPointDoesNotExist.Visible = false;
                CreateCircle(Convert.ToInt32(this.number.Text), point1, rad);
                parent.creatingCircle = false;
                parent.currentFullModel.geometryModel.centerOfCircle = null;
                parent.currentFullModel.geometryModel.tempCircle = null;
                parent.ReDrawAll();
            }
            else
            {
                this.errorPointDoesNotExist.Visible = true;
            }

        }

        public MyCircle CreateCircle(int number, MyPoint point, double radius)
        {
            MyCircle newCircle = new MyCircle(number, point, radius);
            this.parent.currentFullModel.geometryModel.Circles.Add(newCircle);
            parent.visualizer.DrawCircle(newCircle, Color.Blue);
            this.parent.currentFullModel.geometryModel.NumOfCircles++;

            this.number.Text = (parent.currentFullModel.geometryModel.NumOfLines + 1).ToString();
            this.centerPoint.Text = "";
            this.radius.Text = "";
            this.centerPoint.Select();
            
            return newCircle;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void CANCEL()
        {
            if (this.parent.creatingCircle)
            {
                this.parent.creatingCircle = false;
                this.parent.currentFullModel.geometryModel.centerOfCircle = null;
                this.parent.currentFullModel.geometryModel.tempCircle = null;
            }
            this.parent.ReDrawAll();
            this.Dispose();            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            CANCEL();
        }

        private void centerPoint_TextChanged(object sender, EventArgs e)
        {
            if (this.centerPoint.TextLength == 0) return;
            try
            {
                Convert.ToUInt32(this.centerPoint.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                this.centerPoint.Select(0, this.centerPoint.TextLength);
            }
        }

        private void radius_TextChanged(object sender, EventArgs e)
        {
            if (this.radius.TextLength == 0) return;
            try
            {
                Convert.ToDouble(this.radius.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                this.radius.Select(0, this.radius.TextLength);
            }

        }

        private void AddCircle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) CANCEL();
        }

        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) CANCEL();
        }

        private void centerPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) CANCEL();
        }

        private void radius_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) CANCEL();
        }
    }
}
