using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using ModelComponents;
using PreprocessorUtils;

namespace PreprocessorLib
{
    public partial class AddBoundsControl : UserControl
    {
        ProjectForm parent;
        public AddBoundsControl()
        {
            InitializeComponent();
        }

        public AddBoundsControl(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void OK()
        {
            if (txtLine.Text == string.Empty && txtNode.Text == string.Empty) return;

            this.parent.precision = this.parent.DefinePrecision();
            this.parent.showBounds.Checked = true;
            int number;
            int currentModel = this.parent.GetCurrentModelIndex();
            MyFiniteElementModel model = this.parent.currentFullModel.FiniteElementModels[currentModel];
            
            if (!this.xBound.Checked && !this.yBound.Checked)
            {
                this.errorBoundType.Visible = true;
                return;
            }
            if (rbnOnLine.Checked)
            {
                MyLine lineToBound;
                this.errorBoundType.Visible = false;
                bool error = false;
                if (!int.TryParse(this.txtLine.Text, out number)) error = true;
                lineToBound = parent.currentFullModel.geometryModel.Lines.Find(l => l.Id == number);
                if (lineToBound == null) error = true;
                if (error)
                {
                    errorNoLine.Visible = true;
                    return;
                }
                BoundLine(lineToBound);
            }
            else
            {
                MyNode nodeToBound;
                this.errorBoundType.Visible = false;
                bool error = false;
                if (!int.TryParse(this.txtNode.Text, out number)) error = true;
                nodeToBound = model.Nodes.Find(n => n.Id == number);
                if (nodeToBound == null) error = true;
                if (error)
                {
                    lblNoNode.Visible = true;
                    return;
                }
                BoundNode(nodeToBound, GetBoundType());
                renewNfixNB(currentModel);
                this.parent.DrawFEBounds(Color.Brown);

                this.txtNode.Text = "";
                this.txtNode.Select();
            }
            parent.ReDrawAll();
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

        private int GetBoundType()
        {
            if (!this.xBound.Checked && this.yBound.Checked) return 1;
            else if (this.xBound.Checked && !this.yBound.Checked) return 10;
            else if (this.xBound.Checked && this.yBound.Checked) return 11;
            else return 0;
        }

        public void BoundLine(MyLine tempLine)
        {
            int currentModel = this.parent.GetCurrentModelIndex();
            MyFiniteElementModel model = this.parent.currentFullModel.FiniteElementModels[currentModel];
            this.errorNoLine.Visible = false;

            tempLine.BoundType = GetBoundType();

            List<MyNode> nodes = new List<MyNode>();
            if (tempLine is MyStraightLine)
                nodes = model.Nodes.FindAll(node => Mathematics.pointOnLine(node.X, node.Y, (MyStraightLine)tempLine) && model.INOUT[node.Id] != 0);
            else
            {
                MyArc a = tempLine as MyArc;
                double r = Mathematics.FindDist(a.CenterPoint, a.StartPoint);
                double precision = NewMethod(model);
                nodes = model.Nodes.FindAll(node => Mathematics.pointFitsArc(node, (MyArc)tempLine, precision) && model.INOUT[node.Id] != 0);
            }

            foreach (MyNode node in nodes)
            {
                BoundNode(node, tempLine.BoundType);
            }
            // сохраняем закрепления в массив NFIX
            renewNfixNB(currentModel);
            this.parent.DrawFEBounds(Color.Brown);

            this.txtLine.Text = "";
            this.txtLine.Select();
        }

        private static double NewMethod(MyFiniteElementModel model)
        {
            return (model.baseType == MyFiniteElementModel.GridType.Delauney || model.type == MyFiniteElementModel.GridType.FrontalMethod) ? 0.01 : -1;
        }

        private void BoundNode(MyNode node, int boundType)
        {
            switch (boundType)
            {
                case 10:
                    if (node.BoundType == 1) node.BoundType = 11;
                    if (node.BoundType == 0) node.BoundType = 10;
                    break;
                case 1:
                    if (node.BoundType == 10) node.BoundType = 11;
                    if (node.BoundType == 0) node.BoundType = 1;
                    break;
                case 11:
                    node.BoundType = 11;
                    break;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.OK();
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

        private void xBound_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
