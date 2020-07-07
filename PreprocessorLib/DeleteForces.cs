using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ELW.Library.Math;
using ELW.Library.Math.Exceptions;
using ELW.Library.Math.Expressions;
using ELW.Library.Math.Tools;
using ModelComponents;
using PreprocessorUtils;
namespace PreprocessorLib
{
    public partial class DeleteForces : UserControl
    {
        ProjectForm parent;
        public DeleteForces()
        {
            InitializeComponent();
        }
        public DeleteForces(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty) return;

            this.parent.precision = this.parent.DefinePrecision();
            this.parent.showForces.Checked = true;
            int number;
            int currentModel = this.parent.GetCurrentModelIndex();
            MyFiniteElementModel model = this.parent.currentFullModel.FiniteElementModels[currentModel];

            MyLine lineToForces;
            bool error = false;
            if (!int.TryParse(this.textBox1.Text, out number)) error = true;
            lineToForces = parent.currentFullModel.geometryModel.Lines.Find(l => l.Id == number);
            if (lineToForces == null) error = true;
            if (error)
            {
                return;
            }
            UnboundLine(lineToForces);
            parent.ReDrawAll();
        }

        public void UnboundLine(MyLine tempLine)
        {
            int currentModel = this.parent.GetCurrentModelIndex();
            MyFiniteElementModel model = this.parent.currentFullModel.FiniteElementModels[currentModel];

            List<MyNode> nodes = new List<MyNode>();
            if (tempLine is MyStraightLine)
                nodes = model.Nodes.FindAll(node => Mathematics.pointOnLine(node.X, node.Y, (MyStraightLine)tempLine) && model.INOUT[node.Id] != 0);
            else
            {
                double precision = (model.baseType == MyFiniteElementModel.GridType.Delauney || model.type == MyFiniteElementModel.GridType.FrontalMethod) ? 0.01 : -1;
                nodes = model.Nodes.FindAll(node => Mathematics.pointFitsArc(node, (MyArc)tempLine, precision) && model.INOUT[node.Id] != 0);
            }

            foreach (MyNode node in nodes)
            {
                UnboundNode(node);
            }

            this.parent.DrawFEBounds(Color.Brown);

            this.textBox1.Text = "";
            this.textBox1.Select();
        }

        private void UnboundNode(MyNode node)
        {
            node.ForceX = 0;
            node.ForceY = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteForces();
        }

        private void deleteForces()
        {
            int currentModel = this.parent.GetCurrentModelIndex();
            this.parent.currentFullModel.FiniteElementModels[currentModel].R.Clear();
            foreach (MyNode node in this.parent.currentFullModel.FiniteElementModels[currentModel].Nodes)
            {
                node.ForceX = 0;
                node.ForceY = 0;
            }
            this.parent.currentFullModel.geometryModel.highlightStraightLines.Clear();
            this.parent.currentFullModel.geometryModel.highlightArcs.Clear();
            this.parent.ReDrawAll();
            this.parent.clearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
