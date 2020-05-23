using System;
using System.Windows.Forms;
using ModelComponents;
namespace PreprocessorLib
{
    public partial class DeleteLinesControl : UserControl
    {
        ProjectForm parent; 
        public DeleteLinesControl()
        {
            InitializeComponent();
        }

        public DeleteLinesControl(ProjectForm parent)
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
            this.errorLineDoesNotExist.Visible = false;

            this.DeleteLine(Convert.ToInt32(this.number.Text));            
        }

        public void DeleteLine(int number)
        {
            bool lineExists = false;
            foreach (MyStraightLine sline in this.parent.currentFullModel.geometryModel.StraightLines)
            {
                if (sline.Id == number)
                {
                    sline.StartPoint.LineNumbers.Remove(sline.Id);
                    sline.EndPoint.LineNumbers.Remove(sline.Id);
                    this.parent.currentFullModel.geometryModel.StraightLines.Remove(sline);
                    this.parent.ReDrawAll();
                    lineExists = true;

                    this.number.TextChanged -= new System.EventHandler(this.number_TextChanged);
                    this.number.Text = "";
                    this.number.TextChanged += new System.EventHandler(this.number_TextChanged);

                    this.number.Select();
                    
                    break;
                }
            }
            foreach (MyArc arc in this.parent.currentFullModel.geometryModel.Arcs)
            {
                if (arc.Id == number)
                {
                    arc.StartPoint.LineNumbers.Remove(arc.Id);
                    arc.EndPoint.LineNumbers.Remove(arc.Id);
                    arc.CenterPoint.LineNumbers.Remove(arc.Id);
                    this.parent.currentFullModel.geometryModel.Arcs.Remove(arc);
                    this.parent.ReDrawAll();
                    lineExists = true;

                    this.number.TextChanged -= new System.EventHandler(this.number_TextChanged);
                    this.number.Text = "";
                    this.number.TextChanged += new System.EventHandler(this.number_TextChanged);

                    this.number.Select();
                    
                    break;
                }
            }
            if (!lineExists) this.errorLineDoesNotExist.Visible = true;
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

        private void DeleteLinesControl_Load(object sender, EventArgs e)
        {

        }
    }
}
