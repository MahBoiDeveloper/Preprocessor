using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModelComponents;
using PreprocessorUtils;

namespace PreprocessorLib
{
    public partial class Regularization : Form
    {
        ProjectForm parent;
        int currentModelIndex;
        MyFiniteElementModel currentModel;

        public Regularization(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;

            currentModel = parent.currentFullModel.FiniteElementModels[this.parent.GetCurrentModelIndex()];
            currentModelIndex = parent.currentFullModel.FiniteElementModels.Count;

            gridName.Text = "Сетка №" + (currentModelIndex + 1).ToString();
            TopMost = true;

        }

        /// <summary>
        /// Регуляризация
        /// </summary>
        private void Regularize(List<MyNode> dontMove)   //  Регуляризация
        {
            MyNode p = new MyNode();  //  новая точка
            double delta;
            this.Hide();
            parent.StartProgress("Выполняется регуляризация");
            int maxCycles = 500;
            Dictionary<MyNode, List<MyNode>> nodeStars = new Dictionary<MyNode, List<MyNode>>();
            foreach (MyNode node in currentModel.Nodes)
            {
                List<MyNode> nearestNodes = new List<MyNode>();
                List<MyFiniteElement> nearestElems = currentModel.FiniteElements.FindAll(fe => fe.Nodes.Contains(node));
                foreach (MyFiniteElement fe in nearestElems)
                    foreach (MyNode nearestNode in fe.Nodes) if (!nearestNodes.Contains(nearestNode) && nearestNode != node) nearestNodes.Add(nearestNode);
                nodeStars.Add(node, nearestNodes);
                Application.DoEvents();
            }
            do
            {
                delta = -1;
                foreach (MyNode node in currentModel.Nodes)
                {
                    if (dontMove.Contains(node)) continue;
                    List<MyNode> starNodes = nodeStars[node];

                    //  Вычисляем новые координаты точки
                    double X = 0, Y = 0;    //  новые координаты точки                       
                    for (int k = 0; k < starNodes.Count; k++)
                    {
                        X += starNodes[k].X;
                        Y += starNodes[k].Y;
                    }
                    X = X / starNodes.Count;
                    Y = Y / starNodes.Count;
                    double newDelta = Math.Max(Math.Abs(X - node.X), Math.Abs(Y - node.Y));
                    if (delta < 0)
                        delta = newDelta;
                    else
                        delta = Math.Max(delta, newDelta);

                    node.X = X;
                    node.Y = Y;

                }
            } while (delta > 0.01 && maxCycles-- > 0);
            parent.EndProgress();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            List<string> lineStrings = new List<string>(this.notMoveLines.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>());
            List<MyLine> fixedLines = new List<MyLine>();
            if (gridName.Text == string.Empty)
            {
                MessageBox.Show("Имя сетки не может быть пустым!");
                return;
            }
            foreach (string str in lineStrings)
            {
                int idx;
                if (!int.TryParse(str, out idx))
                {
                    MessageBox.Show("Неверно заданы линии!");
                    return;
                }
                MyLine line = parent.currentFullModel.geometryModel.Lines.Find(l => l.Id == idx);
                if (line == null)
                {
                    MessageBox.Show("Не найдена линия с номером " + idx.ToString());
                    return;
                }
                fixedLines.Add(line);
            }
            parent.precision = parent.DefinePrecision();
            if (parent.currentFullModel.geometryModel.Areas.Count != 0)
            {
                errorMessage1.Visible = false;
                errorBadGridName.Visible = false;

                // создаем новый объект "конечно-элементная модель" и добавляем его в список конечно-элементных моделей. 
                foreach (MyFiniteElementModel model in parent.currentFullModel.FiniteElementModels) // проверяем, нет ли модели с таким именем
                {
                    if (model.ModelName == gridName.Text)
                    {
                        errorBadGridName.Visible = true;
                        return;
                    }
                }

                // создаем для новой КЭ модели id
                int id = parent.currentFullModel.IdCandidate;
                currentModel = (MyFiniteElementModel)Util.getObjectCopy(currentModel);
                currentModel.ModelName = gridName.Text;
                currentModel.Id = id;
                currentModel.restoreArraysForOldMethods(parent.currentFullModel.geometryModel);
                List<MyNode> dontMove = new List<MyNode>();
                for (int i = 1; i < currentModel.INOUT.Count; i++)
                {
                    if (currentModel.INOUT[i] == 0) continue;
                    MyNode node = currentModel.Nodes.Find(n => n.Id == i);
                    if (node != null) dontMove.Add(node);
                }
                foreach (MyLine line in fixedLines)
                {
                    if (line is MyStraightLine)
                        dontMove.AddRange(findNodesAtStraightLine(line as MyStraightLine));
                    else
                        dontMove.AddRange(findNodesAtArc(line as MyArc));
                }

                Regularize(dontMove);
                parent.clearSelection();
                parent.ModelCreated(currentModel);
                Close();
            }
            else
            {
                errorMessage1.Visible = true;
            }
        }

        private List<MyNode> findNodesAtStraightLine(MyStraightLine sline)
        {
            List<MyNode> notMoveableNodes = new List<MyNode>();
            foreach (MyNode node in currentModel.Nodes)
            {
                // уравнение прямой, проходящей через две точки
                if (Mathematics.pointOnLine(node.X, node.Y, sline))
                {
                    if (notMoveableNodes.IndexOf(node) == -1) notMoveableNodes.Add(node);
                }
            }
            return notMoveableNodes;
        }

        private List<MyNode> findNodesAtArc(MyArc notMoveArc)
        {
            List<MyNode> notMoveableNodes = new List<MyNode>();
            foreach (MyNode node in currentModel.Nodes)
            {
                // уравнение окружности
                if (parent.pointFitsArc(node.X, node.Y, notMoveArc, ProjectForm.checkType.doublePrecision))
                    if (notMoveableNodes.IndexOf(node) == -1) notMoveableNodes.Add(node);
            }
            return notMoveableNodes;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Regularization_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.activeForm = null;
            parent.clearSelection();
            parent.ReDrawAll();
            
        }
    }
}
