using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModelComponents;

namespace PreprocessorLib
{
    public partial class EditNodesControl : UserControl
    {
 ProjectForm parent; 
        public EditNodesControl()
        {
            InitializeComponent();
        }

        public EditNodesControl(ProjectForm parent)
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
            if (this.parent.currentFullModel.geometryModel.editedNode != null)
            {
                this.parent.currentFullModel.geometryModel.editedNode = null;
            }
            parent.ReDrawAll();
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
            this.errorNodeDoesNotExist.Visible = false;
            newX1 = Convert.ToDouble(this.x.Text);
            newY1 = Convert.ToDouble(this.y.Text);

            this.EditNode(Convert.ToInt32(this.number.Text), newX1, newY1);            
        }

        public void EditNode(int number, double x, double y)
        {
            bool nodeExists = false;
            foreach (MyArea area in this.parent.currentFullModel.geometryModel.Areas)
            {
                foreach (MyPoint node in area.Nodes)
                {
                    if (node.Id == number)
                    {
                        node.X = x;
                        node.Y = y;
                        if (node.PointReference != null) // узел может и не иметь ссылки на точку - если это промежуточный узел зоны
                        {
                            node.PointReference.X = x;
                            node.PointReference.Y = y;
                        }
                        this.parent.ReDrawAll();
                        nodeExists = true;

                        if (this.parent.currentFullModel.geometryModel.editedNode == null)
                        {
                            this.number.TextChanged -= new System.EventHandler(this.number_TextChanged);
                            this.number.Text = "";
                            this.number.TextChanged += new System.EventHandler(this.number_TextChanged);

                            this.x.TextChanged -= new System.EventHandler(this.x_TextChanged);
                            this.x.Text = "";
                            this.x.TextChanged += new System.EventHandler(this.x_TextChanged);

                            this.y.TextChanged -= new System.EventHandler(this.y_TextChanged);
                            this.y.Text = "";
                            this.y.TextChanged += new System.EventHandler(this.y_TextChanged);

                            this.number.Select();
                                                    }
                        break;
                    }
                }
            }
            if (!nodeExists) this.errorNodeDoesNotExist.Visible = true;
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
