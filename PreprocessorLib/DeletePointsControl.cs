using System;
using System.Windows.Forms;
using ModelComponents;

namespace PreprocessorLib
{
    public partial class DeletePointsControl : UserControl
    {
        ProjectForm parent; 
        public DeletePointsControl()
        {
            InitializeComponent();
        }

        public DeletePointsControl(ProjectForm parent)
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
            this.Dispose();
        }

        private void OK()
        {      
            if (this.number.TextLength == 0)
            {
                this.errorMessage2.Visible = true;
                return;
            }            
            this.errorPointDoesNotExist.Visible = false;

            this.DeletePoint(Convert.ToInt32(this.number.Text));            
        }

        public void DeletePoint(int number)
        {
            bool pointExists = false;
            foreach (MyPoint point in this.parent.currentFullModel.geometryModel.Points)
            {
                if (point.Id == number)
                {
                    foreach (int line in point.LineNumbers)
                    {
                        this.DeleteLine(line, point); // удаляем все линии, которые связаны с этой точкой
                    }
                    foreach (int circle in point.CircleNumbers)
                    {
                        this.DeleteCircle(circle); // удаляем все окружности, которые связаны с этой точкой
                    }
                    this.parent.currentFullModel.geometryModel.Points.Remove(point);
                    this.parent.ReDrawAll();
                    pointExists = true;

                    this.number.TextChanged -= new System.EventHandler(this.number_TextChanged);
                    this.number.Text = "";
                    this.number.TextChanged += new System.EventHandler(this.number_TextChanged);

                    this.number.Select();
                    
                 
                    break;
                }
            }
            if (!pointExists) this.errorPointDoesNotExist.Visible = true;
        }


        public bool DeleteLine(int number, MyPoint UnchangeablePoint) // непростая логика. при удалении линии мы должны удалить ее из номер из тех точек, с которыми она была связана. но в данном случае нам не надо трогать точку, которую мы тут удалаяем, потому что если мы ее тронем, то повердим коллекцию и будет нарушен цикл в вызывающей программе...
        {
            bool lineExists = false;
            foreach (MyStraightLine sline in this.parent.currentFullModel.geometryModel.StraightLines)
            {
                if (sline.Id == number)
                {
                    if (UnchangeablePoint == sline.StartPoint) sline.EndPoint.LineNumbers.Remove(sline.Id);
                    else sline.StartPoint.LineNumbers.Remove(sline.Id);

                    this.parent.currentFullModel.geometryModel.StraightLines.Remove(sline);         
                    lineExists = true;
                    break;
                }
            }
            if (lineExists) return lineExists;
            foreach (MyArc arc in this.parent.currentFullModel.geometryModel.Arcs)
            {
                if (arc.Id == number)
                {
                    if (UnchangeablePoint == arc.StartPoint)
                    {
                        arc.EndPoint.LineNumbers.Remove(arc.Id);
                        arc.CenterPoint.LineNumbers.Remove(arc.Id);
                    }
                    else if (UnchangeablePoint == arc.EndPoint)
                    {
                        arc.StartPoint.LineNumbers.Remove(arc.Id);
                        arc.CenterPoint.LineNumbers.Remove(arc.Id);
                    }
                    else
                    {
                        arc.StartPoint.LineNumbers.Remove(arc.Id);
                        arc.EndPoint.LineNumbers.Remove(arc.Id);
                    }
                   
                    this.parent.currentFullModel.geometryModel.Arcs.Remove(arc);        
                    lineExists = true;
                    break;
                }
            }           

            return lineExists;
        }

        public void DeleteCircle(int number)
        {
            foreach (MyCircle circle in this.parent.currentFullModel.geometryModel.Circles)
            {
                if (circle.Id == number)
                {
                    this.parent.currentFullModel.geometryModel.Circles.Remove(circle);
                    break;
                }
            }

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

        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                OK();
            }
            else if (e.KeyChar == (int)Keys.Escape) this.Dispose();
        }
    }
}
