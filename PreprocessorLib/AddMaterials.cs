using System;
using System.Drawing;
using System.Windows.Forms;
using ModelComponents;

namespace PreprocessorLib
{
    public partial class AddMaterials : Form
    {
        ProjectForm parent;
        public AddMaterials()
        {
            InitializeComponent();
        }

        public AddMaterials(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.TopMost = true;
            this.materialName.Text = "Материал №" + (parent.currentFullModel.materials.ListOfMaterials.Count + 1).ToString();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void OK()
        {
                this.errorFillAll.Visible = false;
                this.errorInvalidMaterialNumber.Visible = false;

                this.parent.currentFullModel.materials.ListOfMaterials.Add(new MyMaterial(parent.currentFullModel.materials.ListOfMaterials.Count, this.materialName.Text, Convert.ToDouble(this.elasticModulus.Text), Convert.ToDouble(this.poissonsRatio.Text), Convert.ToDouble(this.tension.Text), Convert.ToDouble(this.tension2.Text), Convert.ToDouble(this.thickness.Text)));

                if (this.addMore.Checked)
                {
                    this.materialName.Text = "Материал №" + (parent.currentFullModel.materials.ListOfMaterials.Count+1).ToString(); 
                    this.elasticModulus.Text = "";
                    this.poissonsRatio.Text = "";
                    this.tension.Text = "";
                    this.tension2.Text = "";
                    this.thickness.Text = "";
                    this.elasticModulus.Select();
                    
                }
                else
                {
                    this.Close();
                }
            }

        private void elasticModulus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(this.elasticModulus.Text);
                errorInvalidValue.Visible = false;
            }
            catch
            {
                errorInvalidValue.Visible = true;
                this.elasticModulus.Select(0, this.elasticModulus.TextLength);
            }
        }

        private void poissonsRatio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(this.poissonsRatio.Text);
                errorInvalidValue.Visible = false;
            }
            catch
            {
                errorInvalidValue.Visible = true;
                this.poissonsRatio.Select(0, this.poissonsRatio.TextLength);
            }
        }

        private void tension_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(this.tension.Text);
                errorInvalidValue.Visible = false;
            }
            catch
            {
                errorInvalidValue.Visible = true;
                this.tension.Select(0, this.tension.TextLength);
            }
        }

        private void tension2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(this.tension2.Text);
                errorInvalidValue.Visible = false;
            }
            catch
            {
                errorInvalidValue.Visible = true;
                this.tension.Select(0, this.tension2.TextLength);
            }
        }

        private void thickness_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(this.thickness.Text);
                errorInvalidValue.Visible = false;
            }
            catch
            {
                errorInvalidValue.Visible = true;
                this.thickness.Select(0, this.thickness.TextLength);
            }
        }

        private void materialNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) this.Close();
        }

        private void materialName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) this.Close();
        }

        private void elasticModulus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) this.Close();
        }

        private void poissonsRatio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) this.Close();
        }

        private void tension_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) this.Close();
        }
        private void tension2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) this.Close();
        }

        private void thickness_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) this.Close();
        }
    }
}
