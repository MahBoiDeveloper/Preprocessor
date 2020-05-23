using System;
using System.Windows.Forms;
using ModelComponents;
namespace PreprocessorLib
{
    public partial class DeleteCirclesControl : UserControl
    {
        ProjectForm parent; 
        public DeleteCirclesControl()
        {
            InitializeComponent();
        }

        public DeleteCirclesControl(ProjectForm parent)
        {            
            InitializeComponent();
            this.parent = parent;
        }

        public void SetCursor()
        {
            this.number.Select();
        }

        private void OK()
        {
            if (this.number.TextLength == 0)
            {
                this.errorMessage2.Visible = true;
                return;
            }
            this.errorCircleDoesNotExist.Visible = false;

            this.DeleteCircle(Convert.ToInt32(this.number.Text));
        }

        public void DeleteCircle(int number)
        {
            bool circleExits = false;
            foreach (MyCircle circle in this.parent.currentFullModel.geometryModel.Circles)
            {
                if (circle.Id == number)
                {
                    circle.CenterPoint.CircleNumbers.Remove(circle.Id);
                    this.parent.currentFullModel.geometryModel.Circles.Remove(circle);
                    this.parent.ReDrawAll();
                    circleExits = true;

                    this.number.TextChanged -= new System.EventHandler(this.number_TextChanged);
                    this.number.Text = "";
                    this.number.TextChanged += new System.EventHandler(this.number_TextChanged);

                    this.number.Select();
                    

                    break;
                }
            }
            if (!circleExits) this.errorCircleDoesNotExist.Visible = true;
        }


        private void okButton_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
