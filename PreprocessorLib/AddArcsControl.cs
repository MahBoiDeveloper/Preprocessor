using System;
using System.Drawing;
using System.Windows.Forms;
using ModelComponents;

namespace PreprocessorLib
{
    public partial class AddArcsControl : UserControl
    {
        ProjectForm parent;
        public AddArcsControl()
        {
            InitializeComponent();
        }

        public AddArcsControl(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.number.ReadOnly = true;
            this.number.BackColor = SystemColors.Window;
            this.number.Text = (parent.currentFullModel.geometryModel.NumOfLines + 1).ToString(); // вводим сквозную нумерацию прямых линий и дуг
        }

        public void SetCursor()
        {
            this.start2Point.Select();
        }

        private void OK()
        {
            int center, start, end;
            bool centerPointExists = false;
            bool startPointExists = false;
            bool endPointExists = false;     
            bool cw = false;

            if (this.сlockwise.Checked) cw = true;
            else cw = false;

            if (this.number.TextLength == 0 || this.center2Point.TextLength == 0 || this.start2Point.TextLength == 0 || this.end2Point.TextLength == 0)
            {
                this.errorMessage1.Visible = true;

                return;
            }
            if (this.errorMessage2.Visible)
            {
                return;
            }

            start = Convert.ToInt32(this.start2Point.Text);
            end = Convert.ToInt32(this.end2Point.Text);
            center = Convert.ToInt32(this.center2Point.Text);


            MyPoint point1 = null;
            MyPoint point2 = null;
            MyPoint point3 = null;
            foreach (MyPoint point in this.parent.currentFullModel.geometryModel.Points)
            {
                if (point.Id == start)
                {
                    point1 = point;
                    startPointExists = true; // нашли начальную точку
                }

                if (point.Id == end)
                {
                    point2 = point;
                    endPointExists = true; // нашли конечную точку
                }

                if (point.Id == center)
                {
                    point3 = point;
                    centerPointExists = true; // нашли центральую точку
                }
            }

            if (centerPointExists && startPointExists && endPointExists)
            {
                this.CreateArc(Convert.ToInt32(this.number.Text), cw, point1, point2, point3);                              
            }
        }

        public MyArc CreateArc(int number, bool cw, MyPoint p1, MyPoint p2, MyPoint p3)
        {
            MyArc newArc = new MyArc(number, cw, p1, p2, p3);
            if (this.parent.TestArc(newArc))
            {
                this.parent.currentFullModel.geometryModel.Arcs.Add(newArc);
                this.parent.currentFullModel.geometryModel.NumOfLines++;

                this.number.Text = (parent.currentFullModel.geometryModel.NumOfLines + 1).ToString();

                this.start2Point.TextChanged -= new System.EventHandler(this.start2Point_TextChanged);
                this.end2Point.TextChanged -= new System.EventHandler(this.end2Point_TextChanged);
                this.center2Point.TextChanged -= new System.EventHandler(this.center2Point_TextChanged);

                this.start2Point.Text = "";
                this.end2Point.Text = "";
                this.center2Point.Text = "";

                this.start2Point.TextChanged += new System.EventHandler(this.start2Point_TextChanged);
                this.end2Point.TextChanged += new System.EventHandler(this.end2Point_TextChanged);
                this.center2Point.TextChanged += new System.EventHandler(this.center2Point_TextChanged);

                this.start2Point.Select();
                
                return newArc;
            }
            else
            {
                if (parent.currentFullModel.geometryModel.Arcs.FindAll(p => p.StartPoint == p1).Count == 0) p1.IsStartOfArc = false;
                if (parent.currentFullModel.geometryModel.Arcs.FindAll(p => p.EndPoint == p1).Count == 0) p1.IsEndOfArc = false;
                if (parent.currentFullModel.geometryModel.Arcs.FindAll(p => p.CenterPoint == p1).Count == 0) p1.IsCenterOfArc = false;
                if (parent.currentFullModel.geometryModel.Arcs.FindAll(p => p.StartPoint == p2).Count == 0) p2.IsStartOfArc = false;
                if (parent.currentFullModel.geometryModel.Arcs.FindAll(p => p.EndPoint == p2).Count == 0) p2.IsEndOfArc = false;
                if (parent.currentFullModel.geometryModel.Arcs.FindAll(p => p.CenterPoint == p2).Count == 0) p2.IsCenterOfArc = false;
                if (parent.currentFullModel.geometryModel.Arcs.FindAll(p => p.StartPoint == p3).Count == 0) p3.IsStartOfArc = false;
                if (parent.currentFullModel.geometryModel.Arcs.FindAll(p => p.EndPoint == p3).Count == 0) p3.IsEndOfArc = false;
                if (parent.currentFullModel.geometryModel.Arcs.FindAll(p => p.CenterPoint == p3).Count == 0) p3.IsCenterOfArc = false;
                MessageBox.Show("Невозможно создать дугу!");
                return null;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void start2Point_TextChanged(object sender, EventArgs e)
        {
            if (this.start2Point.TextLength == 0) return;
            try
            {
                Convert.ToUInt32(this.start2Point.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                this.start2Point.Select(0, this.start2Point.TextLength);
            }
        }

        private void end2Point_TextChanged(object sender, EventArgs e)
        {
            if (this.end2Point.TextLength == 0) return;
            try
            {
                Convert.ToUInt32(this.end2Point.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                this.end2Point.Select(0, this.end2Point.TextLength);
            }
        }

        private void center2Point_TextChanged(object sender, EventArgs e)
        {
            if (this.center2Point.TextLength == 0) return;
            try
            {
                Convert.ToUInt32(this.center2Point.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                this.center2Point.Select(0, this.center2Point.TextLength);
            }
        }

        private void AddArc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose();
        }

        private void start2Point_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose();
        }

        private void end2Point_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose();
        }

        private void center2Point_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose();
        }      

        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose();
        }

        private void errorMessage1_Click(object sender, EventArgs e)
        {
        }
    }
}
