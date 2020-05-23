using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PreprocessorLib
{
    public partial class Settings : Form
    {
        PreprocessorControl parent;
        public Settings()
        {
            InitializeComponent();
        }

        public Settings(PreprocessorControl parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.gridPeriod.Text = this.parent.currentFullModel.settings.GridPeriod.ToString();

            this.showArcs.Checked = this.parent.currentFullModel.settings.ShowArcs;
            this.showAreas.Checked = this.parent.currentFullModel.settings.ShowAreas;
            this.showAxis.Checked = this.parent.currentFullModel.settings.ShowAxis;
            this.showBounds.Checked = this.parent.currentFullModel.settings.ShowBounds;
            this.showCircles.Checked = this.parent.currentFullModel.settings.ShowCircles;
            this.showFE.Checked = this.parent.currentFullModel.settings.ShowFE;
            this.showForces.Checked = this.parent.currentFullModel.settings.ShowForces;
            this.showForceValue.Checked = this.parent.currentFullModel.settings.ShowForceValue;
            this.showGrid.Checked = this.parent.currentFullModel.settings.ShowGrid;
            this.showLines.Checked = this.parent.currentFullModel.settings.ShowLines;
            this.showPoints.Checked = this.parent.currentFullModel.settings.ShowPoints;
            this.showFENumbers.Checked = this.parent.currentFullModel.settings.ShowFENumbers;
            this.showFENodes.Checked = this.parent.currentFullModel.settings.ShowFENodes;
            this.showFEMaterials.Checked = this.parent.currentFullModel.settings.ShowFEMaterials;

            this.showOnlyAreas.Checked = this.parent.currentFullModel.settings.ShowOnlyAreas;
            this.showOnlyGeometry.Checked = this.parent.currentFullModel.settings.ShowOnlyGeometry;
            this.showOnlyFE.Checked = this.parent.currentFullModel.settings.ShowOnlyFE;
        }



        private void saveButton_Click(object sender, EventArgs e)
        {
            this.SAVE();
        }

        private void SAVE()
        {
            if (this.errorMessage2.Visible) return;

            this.parent.currentFullModel.settings.GridPeriod = Convert.ToDouble(this.gridPeriod.Text);

            this.parent.showArcs.Checked = this.showArcs.Checked;
            this.parent.showAreas.Checked = this.showAreas.Checked;
            this.parent.showAxis.Checked = this.showAxis.Checked;
            this.parent.showBounds.Checked = this.showBounds.Checked;
            this.parent.showCircles.Checked = this.showCircles.Checked;
            this.parent.showFE.Checked = this.showFE.Checked;
            this.parent.showForces.Checked = this.showForces.Checked;
            this.parent.showForceValue.Checked = this.showForceValue.Checked;
            this.parent.showGrid.Checked = this.showGrid.Checked;
            this.parent.showLines.Checked = this.showLines.Checked;
            this.parent.showPoints.Checked = this.showPoints.Checked;
            this.parent.showFENumbers.Checked = this.showFENumbers.Checked;
            this.parent.showFENodes.Checked = this.showFENodes.Checked;
            this.parent.showFEMaterials.Checked = this.showFEMaterials.Checked;

            this.parent.showOnlyAreas.Checked = this.showFEMaterials.Checked;
            this.parent.showOnlyGeometry.Checked = this.showOnlyGeometry.Checked;
            this.parent.showOnlyFE.Checked = this.showOnlyFE.Checked;


            this.parent.ReDrawAll();
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridPeriod_TextChanged(object sender, EventArgs e)
        {
            if (this.gridPeriod.TextLength == 0 || this.gridPeriod.Text == "-") return;
            try
            {
                Convert.ToDouble(this.gridPeriod.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                this.gridPeriod.Select(0, this.gridPeriod.TextLength);
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            this.SAVE();
        }

        private void showForces_CheckedChanged(object sender, EventArgs e)
        {
            this.showForceValue.Enabled = this.showForces.Checked;
        }

        private void showFE_CheckedChanged(object sender, EventArgs e)
        {
            this.showFENumbers.Enabled = this.showFE.Checked;
            this.showFENodes.Enabled = this.showFE.Checked;
            this.showFEMaterials.Enabled = this.showFE.Checked;
        }



    }
}
