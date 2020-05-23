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
    public partial class DelauneySettings : Form
    {
        private ProjectForm parent;
        public List<AreaDensity> areaDensity = new List<AreaDensity>();
        public List<DensityPoint> densityPoints
        {
            get { return parent.currentFullModel.densityPoints; }
            set { parent.currentFullModel.densityPoints = value; }
        }

        public DelauneySettings(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            nudArea.Maximum = parent.currentFullModel.geometryModel.Areas.Count;
            densityPoints = new List<DensityPoint>();
        }

        private void btnAddAreaDensity_Click(object sender, EventArgs e)
        {
            int areaId = (int)nudArea.Value;
            if (areaDensity.Find(a => a.areaId == areaId) == null)
            {
                int density = (int)nudAreaDensity.Value;
                double h = (double)nudMinAreaDistance.Value;
                areaDensity.Add(new AreaDensity(areaId, density, h));
                lbxAreaDensities.Items.Add("Z: " + areaId.ToString() + ", D: " + density.ToString() + ", h: " + h.ToString());
            }
            else
                MessageBox.Show("Плотность для этой зоны уже задана!");
        }

        private void btnAddSectorDensity_Click(object sender, EventArgs e)
        {
            double x = (double)nudX.Value;
            double y = (double)nudY.Value;
            AddSectorDensity(x, y);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbxAreaDensities.SelectedItem != null)
            {
                int areaId;
                if (int.TryParse((lbxAreaDensities.SelectedItem as string).Split(' ')[1], out areaId))
                    areaDensity.RemoveAll(a => a.areaId == areaId);
                lbxAreaDensities.Items.Remove(lbxAreaDensities.SelectedItem);
            }
        }

        private void btnDeleteAllAreaDensities_Click(object sender, EventArgs e)
        {
            areaDensity.Clear();
            lbxAreaDensities.Items.Clear();
        }

        private void btnRemoveSectorDensity_Click(object sender, EventArgs e)
        {
            if (lbxSectorDensities.SelectedItem != null)
            {
                densityPoints.RemoveAt(lbxSectorDensities.SelectedIndex);
                lbxSectorDensities.Items.Remove(lbxSectorDensities.SelectedItem);
            }
            parent.ReDrawAll(true);
        }

        public void AddSectorDensity(double x, double y)
        {
            double r = (double)nudRadius.Value;
            double density = (double)nudDensitySector.Value;
            double h = (double)nudMinSectorDistance.Value;
            densityPoints.Add(new DensityPoint(x, y, density, r, h));
            lbxSectorDensities.Items.Add("M: (" + x.ToString() + "; " + y.ToString() + "), R: " + r.ToString() + ", D: " + density.ToString() + ", h :" + h.ToString());
            parent.ReDrawAll();
        }

        private void btnRemoveAllSectorDensities_Click(object sender, EventArgs e)
        {
            lbxSectorDensities.Items.Clear();
            densityPoints.Clear();
            parent.ReDrawAll(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
