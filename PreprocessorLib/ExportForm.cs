using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace PreprocessorLib
{
    public partial class ExportForm : Form
    {
        ProjectForm parent;
        public ExportForm(ProjectForm parent)
        {
            this.parent = parent;
            InitializeComponent();
            if (parent.currentFullModel.FiniteElementModels.Count == 0)
                WholeModel.Enabled = false;
            else
                WholeModel.Checked = true;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Path.GetDirectoryName(parent.FullProjectFileName);
            saveFileDialog.FileName = "geom_model";
            saveFileDialog.Filter = "Sigma Geometry Files (*.sfm)|*.sfm|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                if (FormOnly.Checked)
                {
                    parent.CreateSFMFile(FileName);
                }
                else
                {
                    parent.CreateSFMFile(FileName);
                    parent.CreateFEAndNodesFiles(FileName);
                    parent.CreateBoundsFile(FileName);
                    parent.CreateForceFile(FileName);
                    parent.CreateMaterialsFile(FileName);
                }
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WholeModel_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
