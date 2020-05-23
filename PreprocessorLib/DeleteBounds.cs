using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModelComponents;
using PreprocessorUtils;

namespace PreprocessorLib
{
    public partial class DeleteBounds : UserControl
    {
        ProjectForm parent;

        public DeleteBounds()
        {
            InitializeComponent();
        }

        public DeleteBounds(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtLine.Text == string.Empty && txtNode.Text == string.Empty) return;

            this.parent.precision = this.parent.DefinePrecision();
            this.parent.showBounds.Checked = true;
            int number;
            int currentModel = this.parent.GetCurrentModelIndex();
            MyFiniteElementModel model = this.parent.currentFullModel.FiniteElementModels[currentModel];
            
           
            if (rbnOnLine.Checked)
            {
                MyLine lineToBound;
                bool error = false;
                if (!int.TryParse(this.txtLine.Text, out number)) error = true;
                lineToBound = parent.currentFullModel.geometryModel.Lines.Find(l => l.Id == number);
                if (lineToBound == null) error = true;
                if (error)
                {
                    errorNoLine.Visible = true;
                    return;
                }
                UnboundLine(lineToBound);
            }
            else
            {
                MyNode nodeToBound;
                
                bool error = false;
                if (!int.TryParse(this.txtNode.Text, out number)) error = true;
                nodeToBound = model.Nodes.Find(n => n.Id == number);
                if (nodeToBound == null) error = true;
                if (error)
                {
                    lblNoNode.Visible = true;
                    return;
                }
                UnboundNode(nodeToBound);
                renewNfixNB(currentModel);
                this.parent.DrawFEBounds(Color.Brown);

                this.txtNode.Text = "";
                this.txtNode.Select();
            }
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
            // сохраняем закрепления в массив NFIX
            renewNfixNB(currentModel);
            this.parent.DrawFEBounds(Color.Brown);

            this.txtLine.Text = "";
            this.txtLine.Select();
        }

        private void UnboundNode(MyNode node)
        {

            if (!this.cb1.Checked && !this.cb2.Checked)
                node.BoundType = 0;
            if (this.cb1.Checked && this.cb2.Checked)
                node.BoundType = 0;
            if (!this.cb1.Checked && this.cb2.Checked && (node.BoundType == 1))
                node.BoundType = 0;
            if (this.cb1.Checked && !this.cb2.Checked && (node.BoundType == 10))
                node.BoundType = 0;
            if (!this.cb1.Checked && this.cb2.Checked && (node.BoundType == 11))
                node.BoundType = 10;
            if (this.cb1.Checked && !this.cb2.Checked && (node.BoundType == 11))
                node.BoundType = 1;


        }

        private void renewNfixNB(int modelIndex)
        {
            parent.showBounds.Checked = true;
            MyFiniteElementModel currentModel = parent.currentFullModel.FiniteElementModels[modelIndex];
            currentModel.NBC.Clear();
            currentModel.NFIX.Clear();
            currentModel.NBC.Add(0);
            currentModel.NFIX.Add(0);
            List<MyNode> boundedNodes = currentModel.Nodes.FindAll(n => n.BoundType != 0);
            foreach (MyNode node in boundedNodes)
            {
                currentModel.NFIX.Add(node.BoundType);
                currentModel.NBC.Add(node.Id);
            }
            currentModel.NB = boundedNodes.Count;
        
        }

        private void txtLine_Enter(object sender, EventArgs e)
        {
            if (!rbnOnLine.Checked)
                rbnOnLine.Checked = true;
        }

        private void txtNode_Enter(object sender, EventArgs e)
        {
            if (!rbnOnNode.Checked)
                rbnOnNode.Checked = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
         
            int currentModel = this.parent.GetCurrentModelIndex();
            foreach (MyNode node in this.parent.currentFullModel.FiniteElementModels[currentModel].Nodes)
            {
                node.BoundType = 0;
                node.BoundType = 0; 
            }
            parent.currentFullModel.FiniteElementModels[currentModel].BoundedArcs.Clear();
            parent.currentFullModel.FiniteElementModels[currentModel].BoundedLines.Clear();
            this.parent.currentFullModel.geometryModel.highlightStraightLines.Clear();
            this.parent.currentFullModel.geometryModel.highlightArcs.Clear();
            this.parent.ReDrawAll();
            
            this.parent.clearSelection();
        
        }

        private void txtLine_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
