using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModelComponents;

namespace PreprocessorLib
{
    public partial class EditMaterials : Form
    {
        ProjectForm parent;
        public EditMaterials()
        {
            InitializeComponent();
        }

        public EditMaterials(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.TopMost = true;

            if (parent.currentFullModel.materials.ListOfMaterials.Count != 0)
            {
                foreach (MyMaterial mat in this.parent.currentFullModel.materials.ListOfMaterials)
                {
                    this.materialComboBox.Items.Add(parent.currentFullModel.materials.ListOfMaterials.IndexOf(mat) + 1 + " - " + mat.Name);
                }

                if (materialComboBox.Items.Count != 0)
                    materialComboBox.SelectedItem = materialComboBox.Items[0].ToString();
                else
                {
                    materialName.Clear();
                    elasticModulus.Clear();
                    poissonsRatio.Clear();
                    tension.Clear();
                    tension2.Clear();
                    thickness.Clear();
                    materialComboBox.Text = "";
                }
            }
        }

        private void materialComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (materialComboBox.Items.Count != 0)
            {
                materialName.Text = parent.currentFullModel.materials.ListOfMaterials[materialComboBox.SelectedIndex].Name;
                elasticModulus.Text = parent.currentFullModel.materials.ListOfMaterials[materialComboBox.SelectedIndex].ElasticModulus.ToString();
                poissonsRatio.Text = parent.currentFullModel.materials.ListOfMaterials[materialComboBox.SelectedIndex].PoissonsRatio.ToString();
                tension.Text = parent.currentFullModel.materials.ListOfMaterials[materialComboBox.SelectedIndex].Tension.ToString();
                tension2.Text = parent.currentFullModel.materials.ListOfMaterials[materialComboBox.SelectedIndex].Tension2.ToString();
                thickness.Text = parent.currentFullModel.materials.ListOfMaterials[materialComboBox.SelectedIndex].Thickness.ToString();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.SAVE();
        }

        private void SAVE()
        {
            if(materialComboBox.Items.Count != 0)
                parent.currentFullModel.materials.ListOfMaterials[materialComboBox.SelectedIndex].UpdateMaterial(materialName.Text, Convert.ToDouble(elasticModulus.Text), Convert.ToDouble(poissonsRatio.Text), Convert.ToDouble(tension.Text), Convert.ToDouble(tension2.Text), Convert.ToDouble(thickness.Text)); 
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void OK() 
        {
            this.Close();
        }

        private void elasticModulus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (materialComboBox.Items.Count != 0)
                {
                    Convert.ToDouble(this.elasticModulus.Text);
                    errorInvalidValue.Visible = false;
                }
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
                if (materialComboBox.Items.Count != 0)
                {
                    Convert.ToDouble(this.poissonsRatio.Text);
                    errorInvalidValue.Visible = false;
                }
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
                if (materialComboBox.Items.Count != 0)
                {
                    Convert.ToDouble(this.tension.Text);
                    errorInvalidValue.Visible = false;
                }
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
                if (materialComboBox.Items.Count != 0)
                {
                    Convert.ToDouble(this.tension2.Text);
                    errorInvalidValue.Visible = false;
                }
            }
            catch
            {
                errorInvalidValue.Visible = true;
                this.tension2.Select(0, this.tension2.TextLength);
            }
        }

        private void thickness_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (materialComboBox.Items.Count != 0)
                {
                    Convert.ToDouble(this.thickness.Text);
                    errorInvalidValue.Visible = false;
                }
            }
            catch
            {
                errorInvalidValue.Visible = true;
                this.thickness.Select(0, this.thickness.TextLength);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void deleteMaterialButton_Click(object sender, EventArgs e)
        {
            if (parent.currentFullModel.materials.ListOfMaterials.Count != 0)
            {

                parent.currentFullModel.materials.ListOfMaterials.RemoveAt(materialComboBox.SelectedIndex);

                materialComboBox.Items.Clear();

                foreach (MyMaterial mat in this.parent.currentFullModel.materials.ListOfMaterials)
                {
                    this.materialComboBox.Items.Add(parent.currentFullModel.materials.ListOfMaterials.IndexOf(mat) + 1 + " - " + mat.Name);
                    mat.Id = parent.currentFullModel.materials.ListOfMaterials.IndexOf(mat) + 1;
                }

                if (materialComboBox.Items.Count != 0)
                    materialComboBox.SelectedItem = materialComboBox.Items[0].ToString();
                else
                {
                    materialName.Clear();
                    elasticModulus.Clear();
                    poissonsRatio.Clear();
                    tension.Clear();
                    tension2.Clear();
                    thickness.Clear();
                    materialComboBox.Text = "";
                }
            }
        }

        private void materialName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
