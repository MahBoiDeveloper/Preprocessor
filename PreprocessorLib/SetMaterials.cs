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
    public partial class SetMaterials : Form
    {
        ProjectForm parent;
        public SetMaterials()
        {
            InitializeComponent();
        }
        public SetMaterials(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.TopMost = true;

            int currentModel = this.parent.GetCurrentModelIndex();
            foreach (MyMaterial mat in this.parent.currentFullModel.FiniteElementModels[currentModel].Materials)
            {
                object[] row = { "удалить", mat.Id, mat.Name, mat.ElasticModulus, mat.PoissonsRatio, mat.Tension, mat.Tension2, mat.Thickness };
                this.usedMaterials.Rows.Add(row);
            }

            foreach (MyMaterial mat in this.parent.currentFullModel.materials.ListOfMaterials)
            {
                this.materialComboBox.Items.Add(mat.Id.ToString() + " - " + mat.Name);
            }
            if (this.materialComboBox.Items.Count != 0) this.materialComboBox.SelectedItem = this.materialComboBox.Items[0].ToString();

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.SAVE();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OK()
        {
            this.Close();
        }

        private void SAVE()
        {
            if (this.materialComboBox.Items.Count == 0) return;

            int index = 0;
            int materialNumber = Convert.ToInt32(this.materialComboBox.SelectedItem.ToString().Split(' ')[0]);
            foreach (MyMaterial mat in this.parent.currentFullModel.materials.ListOfMaterials)
            {
                if (mat.Id == materialNumber)
                {
                    index = this.parent.currentFullModel.materials.ListOfMaterials.IndexOf(mat);
                    break;
                }
            }


            // разпарсиваем номера зон для назначения материалов
            if (this.parent.currentFullModel.FiniteElementModels.Count != 0 && this.areasRadioBut.Checked)
            {
                int currentModel = this.parent.GetCurrentModelIndex();
                if (this.areaMaterials.TextLength != 0 && this.parent.currentFullModel.FiniteElementModels[currentModel].FiniteElements.Count != 0)
                {
                    List<string> areasNumbers = new List<string>(this.areaMaterials.Text.Split(',').AsEnumerable<string>());

                    bool matExists = false;
                    for (int j = 0; j < usedMaterials.Rows.Count; j++)
                    {                        
                        if (Convert.ToInt32(usedMaterials.Rows[j].Cells["matID"].Value) == materialNumber)
                        {
                            matExists = true;
                        }                        
                    }

                    if (!matExists && this.usedMaterials.Rows.Count == 3)
                    {
                        MessageBox.Show("Для одной модели может быть задано не более трех материалов!");
                        return;
                    }
                    //this.parent.currentFullModel.FiniteElementModels[0].NMAT = this.usedMaterials.Rows.Count;

                    if (this.parent.currentFullModel.FiniteElementModels[currentModel].UsedMaterials.IndexOf(materialNumber) == -1) this.parent.currentFullModel.FiniteElementModels[currentModel].UsedMaterials.Add(materialNumber);
                    if (this.parent.currentFullModel.FiniteElementModels[currentModel].Materials.IndexOf(this.parent.currentFullModel.materials.ListOfMaterials[index]) == -1) this.parent.currentFullModel.FiniteElementModels[currentModel].Materials.Add(this.parent.currentFullModel.materials.ListOfMaterials[index]);

                   

                    // добрались до назначения материалов КЭ
                    
                    matExists = false;
                    for (int i = 0; i < this.parent.currentFullModel.geometryModel.Areas.Count; i++)
                    {
                        foreach (string str in areasNumbers)
                        {
                            try
                            {
                                Convert.ToInt32(str);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                                return;
                            }
                            if (this.parent.currentFullModel.geometryModel.Areas[i].Id == Convert.ToInt32(str))
                            {                                
                                for (int j = 0; j < usedMaterials.Rows.Count; j++)
                                {
                                    if ( Convert.ToInt32(usedMaterials.Rows[j].Cells["matID"].Value) == materialNumber)
                                    {
                                        matExists = true;
                                    }
                                }
                                if (!matExists)
                                {
                                    MyMaterial mat = this.parent.currentFullModel.materials.ListOfMaterials[index];
                                    object[] row = { "удалить", mat.Id, mat.Name, mat.ElasticModulus, mat.PoissonsRatio, mat.Tension, mat.Tension2, mat.Thickness };
                                    this.usedMaterials.Rows.Add(row);
                                }
                                List<MyFiniteElement> elems = parent.currentFullModel.FiniteElementModels[currentModel].FiniteElements.FindAll(fe => fe.areaId == parent.currentFullModel.geometryModel.Areas[i].Id - 1);
                                elems.ForEach(e => e.MaterialPropertyID = materialNumber);
                                
                            }
                        }
                    }
                }
            }

            // разпарсиваем номера КЭ для назначения материалов
            if (this.parent.currentFullModel.FiniteElementModels.Count != 0 && this.FERadioBut.Checked)
            {
                int currentModel = this.parent.GetCurrentModelIndex();
                if (this.FEMaterials.TextLength != 0 && this.parent.currentFullModel.FiniteElementModels[currentModel].FiniteElements.Count != 0)
                {
                    List<string> FENumbers = new List<string>(this.FEMaterials.Text.Split(',').AsEnumerable<string>());

                    bool matExists = false;
                    for (int j = 0; j < usedMaterials.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(usedMaterials.Rows[j].Cells["matID"].Value) == materialNumber)
                        {
                            matExists = true;
                        }
                    }

                    if (!matExists && this.usedMaterials.Rows.Count == 3)
                    {
                        MessageBox.Show("Для одной модели может быть задано не более трех материалов!");
                        return;
                    }

                    if (this.parent.currentFullModel.FiniteElementModels[currentModel].UsedMaterials.IndexOf(materialNumber) == -1) this.parent.currentFullModel.FiniteElementModels[currentModel].UsedMaterials.Add(materialNumber);
                    if (this.parent.currentFullModel.FiniteElementModels[currentModel].Materials.IndexOf(this.parent.currentFullModel.materials.ListOfMaterials[index]) == -1) this.parent.currentFullModel.FiniteElementModels[currentModel].Materials.Add(this.parent.currentFullModel.materials.ListOfMaterials[index]);



                    // добрались до назначения материалов КЭ
                    matExists = false;
                    foreach (MyFiniteElement FE in this.parent.currentFullModel.FiniteElementModels[currentModel].FiniteElements)
                    {
                        foreach (string str in FENumbers)
                        {
                            try
                            {
                                Convert.ToInt32(str);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                                return;
                            }
                            if (FE.Id == Convert.ToInt32(str))
                            {
                                for (int j = 0; j < usedMaterials.Rows.Count; j++)
                                {
                                    if (Convert.ToInt32(usedMaterials.Rows[j].Cells["matID"].Value) == materialNumber)
                                    {
                                        matExists = true;
                                    }
                                }
                                if (!matExists)
                                {
                                    MyMaterial mat = this.parent.currentFullModel.materials.ListOfMaterials[index];
                                    object[] row = { "удалить", mat.Id, mat.Name, mat.ElasticModulus, mat.PoissonsRatio, mat.Tension, mat.Tension2, mat.Thickness };
                                    this.usedMaterials.Rows.Add(row);
                                }                        
                                FE.MaterialPropertyID = materialNumber;                              
                            }
                        }
                    }
                }
            }

            
            this.parent.ReDrawAll();
        }

        private void pickAllAreas_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.areasRadioBut.Checked) return;
            this.areaMaterials.Text = "";
            foreach (MyArea area in this.parent.currentFullModel.geometryModel.Areas)
            {
                if (this.areaMaterials.TextLength == 0) this.areaMaterials.Text += area.Id.ToString();
                else this.areaMaterials.Text += ", " + area.Id.ToString();
            }
        }

        private void usedMaterials_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == 0 && e.RowIndex >= 0) // значит клик был по кнопке, а не по заголовку столбца 
            {
                int currentModel = this.parent.GetCurrentModelIndex();
                foreach (MyMaterial mat in this.parent.currentFullModel.FiniteElementModels[currentModel].Materials)
                {
                    if (mat.Id == Convert.ToInt32(this.usedMaterials.Rows[e.RowIndex].Cells["matID"].Value))
                    {                       
                        this.parent.currentFullModel.FiniteElementModels[currentModel].UsedMaterials.Remove(mat.Id);
                        this.parent.currentFullModel.FiniteElementModels[currentModel].Materials.Remove(mat);
                        this.usedMaterials.Rows.RemoveAt(e.RowIndex);

                        foreach (MyFiniteElement FE in this.parent.currentFullModel.FiniteElementModels[currentModel].FiniteElements)
                        {
                            if (FE.MaterialPropertyID == mat.Id) FE.MaterialPropertyID = 0;                              
                        }
                        this.parent.ReDrawAll();
                        break;
                    }
                }
                
            }
        }

        private void areaRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            if (this.areasRadioBut.Checked)
            {
                this.FERadioBut.Checked = false;
                this.areaMaterials.Enabled = true;
                this.FEMaterials.Enabled = false;
            }
        }

        private void FERadioBut_CheckedChanged(object sender, EventArgs e)
        {
            if (this.FERadioBut.Checked)
            {
                this.areasRadioBut.Checked = false;
                this.areaMaterials.Enabled = false;
                this.FEMaterials.Enabled = true;
            }
        }

        private void pickAllFE_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.FERadioBut.Checked) return;
            this.FEMaterials.Text = "";
            int currentModel = this.parent.GetCurrentModelIndex();
            foreach (MyFiniteElement FE in this.parent.currentFullModel.FiniteElementModels[currentModel].FiniteElements)
            {
                if (this.FEMaterials.TextLength == 0) this.FEMaterials.Text += FE.Id.ToString();
                else this.FEMaterials.Text += ", " + FE.Id.ToString();
            }
        }

        private void materialComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
