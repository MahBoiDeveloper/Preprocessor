using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModelComponents;
using PreprocessorUtils;

namespace PreprocessorLib
{
    public partial class EditPointsControl : UserControl
    {
        ProjectForm parent; 
        public EditPointsControl()
        {
            InitializeComponent();
        }

        public EditPointsControl(ProjectForm parent)
        {            
            InitializeComponent();
            this.parent = parent;
        }

        public void SetCursor()
        {
            this.number.Select();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (this.parent.currentFullModel.geometryModel.editedPoint != null)
            {
                this.parent.currentFullModel.geometryModel.editedPoint = null;
                parent.ReDrawAll();
            }            
            this.Dispose();
        }

        private void OK()
        {
            double newX1, newY1;
            if (this.number.TextLength == 0 || this.x.TextLength == 0 || this.y.TextLength == 0)
            {
                this.errorMessage1.Visible = true;
                return;
            }
            if (this.errorMessage2.Visible)
            {
                return;
            }
            this.errorPointDoesNotExist.Visible = false;
            newX1 = Mathematics.floor(Convert.ToDouble(this.x.Text),Mathematics.accuracy_high);
            newY1 = Mathematics.floor(Convert.ToDouble(this.y.Text),Mathematics.accuracy_high);
            this.EditPoint(Convert.ToInt32(this.number.Text), newX1, newY1);            
        }

        public void EditPoint(int number, double x, double y)
        {
            MyPoint point = parent.currentFullModel.geometryModel.Points.Find(p => p.Id == number);
            if (point != null)
            {
                if (point.IsCenterOfArc)
                {
                    MessageBox.Show("Нельзя перемещать центр дуги!");
                    return;
                }
                else if (point.IsEndOfArc || point.IsStartOfArc)
                {
                    MyArc[] incidentArcs = parent.currentFullModel.geometryModel.Arcs.Where(a => a.StartPoint == point || a.EndPoint == point).ToArray();

                    if (incidentArcs.Length > 0)
                    {
                        bool PointFits = true;
                        foreach (MyArc arc in incidentArcs)
                        {
                            MyArc testArc;
                            if (arc.EndPoint == point) testArc = new MyArc(0, arc.Clockwise, arc.StartPoint, new MyPoint(x, y, MyPoint.PointType.IsGeometryPoint), arc.CenterPoint);
                            else testArc = new MyArc(0, arc.Clockwise, new MyPoint(x, y, MyPoint.PointType.IsGeometryPoint), arc.EndPoint, arc.CenterPoint);

                            if (!parent.TestArc(testArc))
                            {
                                PointFits = false;
                                break;
                            }
                        }
                        if (PointFits)
                        {
                            point.X = x;
                            point.Y = y;
                        }
                        else
                            MessageBox.Show("Новые координаты точки не удовлетворяют условию принадлежности точки к дуге!");
                    }
                    else
                    {
                        point.X = x;
                        point.Y = y;
                    }
                }
                else
                {
                    point.X = x;
                    point.Y = y;
                }

                if (point.NodeReference != null) // точка может и не иметь ссылки на узел - если это центральная точка дуги, например
                {
                    point.NodeReference.X = x;
                    point.NodeReference.Y = y;
                }
                this.parent.ReDrawAll();

                this.number.Text = "";
                this.x.Text = "";
                this.y.Text = "";

                this.number.TextChanged += new System.EventHandler(this.number_TextChanged);
                this.x.TextChanged += new System.EventHandler(this.x_TextChanged);
                this.y.TextChanged += new System.EventHandler(this.y_TextChanged);

                this.number.Select();
            }
            else this.errorPointDoesNotExist.Visible = true;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void number_TextChanged(object sender, EventArgs e)
        {
            if (this.number.TextLength == 0) return;
            try
            {
                Convert.ToInt32(this.number.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                this.number.Select(0, this.number.TextLength);
            }
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
