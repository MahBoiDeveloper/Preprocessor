using System;
using System.Drawing;
using System.Windows.Forms;
using ModelComponents;

namespace PreprocessorLib
{
    public partial class AddPointsControl : UserControl
    {
        ProjectForm parent; 
        public AddPointsControl()
        {
            InitializeComponent();
        }

        public AddPointsControl(ProjectForm parent)
        {            
            InitializeComponent();
            this.parent = parent;
            this.number.ReadOnly = true;
            this.number.BackColor = SystemColors.Window;
            this.number.Text = (parent.currentFullModel.geometryModel.NumOfPoints + 1).ToString();
        }

        public void SetCursor()
        {
            this.x.Select();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            parent.clearSelection();
            this.Dispose();
        }

        private void OK()
        {
            double x1, y1;
            if (this.number.TextLength == 0 || this.x.TextLength == 0 || this.y.TextLength == 0)
            {
                this.errorMessage1.Visible = true;
                return;
            }
            if (this.errorMessage2.Visible)
            {
                return;
            }
            x1 = Convert.ToDouble(this.x.Text);
            y1 = Convert.ToDouble(this.y.Text);

            MyPoint p = this.CreatePoint(Convert.ToInt32(this.number.Text), x1, y1);
            
            this.parent.TestOnLineCircleAndArc(ref p); // проверяем, не попала ли точка на линию, окружность или дугу
        }

        public MyPoint CreatePoint(int number, double x, double y)
        {
            MyPoint newPoint = new MyPoint(number, x, y, MyPoint.PointType.IsGeometryPoint);
            this.parent.currentFullModel.geometryModel.Points.Add(newPoint);
            this.parent.currentFullModel.geometryModel.NumOfPoints++;

            this.number.Text = (parent.currentFullModel.geometryModel.NumOfPoints + 1).ToString();
            this.x.TextChanged -= new System.EventHandler(this.x_TextChanged);
            this.y.TextChanged -= new System.EventHandler(this.y_TextChanged);
            this.x.Text = "";
            this.y.Text = "";
            this.x.TextChanged += new System.EventHandler(this.x_TextChanged);
            this.y.TextChanged += new System.EventHandler(this.y_TextChanged);
            this.x.Select();
            return newPoint;
        }        

        private void okButton_Click(object sender, EventArgs e)
        {
            OK();
        }        

        private void x_TextChanged(object sender, EventArgs e)
        {
            if (this.x.TextLength == 0 || this.x.Text == "-") return;
            try
            {                
                Convert.ToDouble(this.x.Text); 
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                this.x.Select(0, this.x.TextLength);
            }
        }

        private void y_TextChanged(object sender, EventArgs e)
        {
            if (this.y.TextLength == 0 || this.y.Text == "-") return;
            try
            {
                Convert.ToDouble(this.y.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                this.y.Select(0, this.y.TextLength);
            }
        }

        private void AddPoint_KeyPress(object sender, KeyPressEventArgs e)
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

        private void x_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                OK();
            }
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose();
        }

        private void y_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                OK();
            }
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose(); 
        }


    }
}
