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
    public partial class Forces : Form
    {
        ProjectForm parent;

        public Forces()
        {
            InitializeComponent();
        }

        public Forces(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.TopMost = true;
        }

        private enum LineAngleType : byte
        {
            right_up,
            left_up
        }

        private enum FESearchSpace : byte
        {
            up,
            down,
            inside,
            outside
        }
        /// <summary>
        /// Тип прямой линии
        /// </summary>
        public enum LineType : byte
        {
            /// <summary>
            /// Наклонная
            /// </summary>
            NotParallel = 0,
            /// <summary>
            /// Параллельная оси OX
            /// </summary>
            ParallelOX = 1,
            /// <summary>
            /// Параллельная оси OY
            /// </summary>
            ParallelOY = 2,
            /// <summary>
            /// Дуга
            /// </summary>
            IsArc = 3
        }

        private void OK()
        {
            if (this.number.Text == "")
            {
                MessageBox.Show("Не введены номера линий!");
                return;
            }

            if (this.function.Enabled == true && this.function.Text == "")
            {
                MessageBox.Show("Не введена функция нагрузки!");
                return;
            }
            if (trytocalculateExpression() == false)
            {
                MessageBox.Show("Неверный формат функции нагрузки!");
                return;
            }
            int x=0;
            double x1;
            if (this.tb.Text == "")
            {
                x++;
            }
            else
            { 
            if (double.TryParse(tb.Text, out x1))
            {
                ProjectForm.toch = x1;
            }
            else
            {
                MessageBox.Show("Неверный формат ввода точности нагрузки!");
                return;
            }
            }
            this.parent.precision = this.parent.DefinePrecision();

            int currentModel = this.parent.GetCurrentModelIndex();

            string[] textlinesnumbers = this.number.Text.Split(',');
            List<MyStraightLine> lines = new List<MyStraightLine>();
            List<MyArc> arcs = new List<MyArc>();
            foreach (string textlinenumber in textlinesnumbers)
            {
                MyStraightLine line = this.parent.currentFullModel.geometryModel.StraightLines.Find(
                    thisline => thisline.Id == Convert.ToInt32(textlinenumber));
                MyArc arc = this.parent.currentFullModel.geometryModel.Arcs.Find(
                    thisline => thisline.Id == Convert.ToInt32(textlinenumber));
                if (line == null && arc == null)
                {
                    //this.number.Clear();
                    this.parent.clearSelection();
                    MessageBox.Show("Неверно заданы линии!");
                    return;
                }
                else if (line != null && arc == null)
                    lines.Add(line);
                else if (line == null && arc != null)
                    arcs.Add(arc);
            }
            if (lines.Count != 0 && arcs.Count != 0)
            {
                //this.number.Clear();
                this.parent.clearSelection();
                MessageBox.Show("Неверно заданы линии!");
                return;
            }
            else if (lines.Count != 0)
            {
                List<MyPoint> points = new List<MyPoint>();
                if (checkLinesConnect(ref lines, ref points, currentModel) == false)
                {
                    //this.number.Clear();
                    this.parent.clearSelection();
                    MessageBox.Show("Неверно заданы линии!");
                    return;
                }
                LineType linesposition = checkLinesPosition(points);
                if (checkLinesOnLine(points, linesposition) == false)
                {
                    //this.number.Clear();
                    this.parent.clearSelection();
                    MessageBox.Show("Неверно заданы линии!");
                    return;
                }
                List<MyNode> nodes = new List<MyNode>();
                getNodesOnLine(lines, ref nodes, currentModel, linesposition);
                this.calculateForceAtNodes(nodes, linesposition, null, currentModel);
            }
            else if (arcs.Count != 0)
            {
                List<MyPoint> points = new List<MyPoint>();
                if (checkArcsConnect(ref arcs, ref points, currentModel) == false)
                {
                    //this.number.Clear();
                    this.parent.clearSelection();
                    MessageBox.Show("Неверно заданы линии!");
                    return;
                }
                LineType linesposition = LineType.IsArc;
                List<MyNode> nodes = new List<MyNode>();
                getNodesOnArc(arcs, ref nodes, currentModel, linesposition);
                this.number.Text = Convert.ToString(nodes.Count);
                this.calculateForceAtNodes(nodes, LineType.IsArc, arcs.ElementAt(0), currentModel);
            }
            parent.clearSelection();
            if (this.parent.currentFullModel.FiniteElementModels[currentModel].R.Count == 0)
            {
                for (int i = 0; i <= this.parent.currentFullModel.FiniteElementModels[currentModel].Nodes.Count * 2; i++)
                {
                    this.parent.currentFullModel.FiniteElementModels[currentModel].R.Add(0.0);

                }
            }
            int m = 1;
            foreach (MyNode node in this.parent.currentFullModel.FiniteElementModels[currentModel].Nodes)
            {
                this.parent.currentFullModel.FiniteElementModels[currentModel].R[2 * (m - 1) + 1] = node.ForceX;
                this.parent.currentFullModel.FiniteElementModels[currentModel].R[2 * (m - 1) + 2] = node.ForceY;
                m++;
            }

            this.parent.DrawForces(Color.Purple);
            this.parent.currentFullModel.FiniteElementModels[currentModel].NLD++;
            if (this.parent.showForces.Checked != true) this.parent.showForces.Checked = true;

            this.number.TextChanged -= new System.EventHandler(this.number_TextChanged);
            this.number.Text = "";
            this.number.TextChanged += new System.EventHandler(this.number_TextChanged);
            this.number.Select();

            this.parent.currentFullModel.geometryModel.highlightStraightLines.Clear();
            this.parent.currentFullModel.geometryModel.highlightArcs.Clear();
            this.parent.ReDrawAll();
            this.parent.clearSelection();
        }

        private void getNodesOnLine(List<MyStraightLine> lines, ref List<MyNode> nodes, int currentModel, LineType linesposition)
        {
            foreach (MyStraightLine line in lines)
            { findNodesAtStraightLine(ref nodes, line, currentModel); }
        }

        private void getNodesOnArc(List<MyArc> arcs, ref List<MyNode> nodes, int currentModel, LineType lineposition)
        {
            foreach (MyArc arc in arcs)
            { findNodesAtArc(ref nodes, arc, currentModel); }
        }

        private LineType checkLinesPosition(List<MyPoint> points)
        {
            if (points.ElementAt(0).X == points.ElementAt(points.Count - 1).X) return LineType.ParallelOY;
            else if (points.ElementAt(0).Y == points.ElementAt(points.Count - 1).Y) return LineType.ParallelOX;
            else return LineType.NotParallel;
        }

        private bool checkLinesOnLine(List<MyPoint> points, LineType linesposition)
        {
            MyPoint startpoint = points.ElementAt(0);
            MyPoint endpoint = points.ElementAt(points.Count - 1);
            foreach (MyPoint point in points)
            {
                if (linesposition == LineType.ParallelOY)
                {
                    if (point.X != startpoint.X) return false;
                }
                else if (linesposition == LineType.ParallelOX)
                {
                    if (point.Y != startpoint.Y) return false;
                }
                else
                {
                    if (Math.Abs((point.X - Mathematics.floor((((((point.Y - startpoint.Y) * (endpoint.X - startpoint.X)) /
                        (endpoint.Y - startpoint.Y))) + startpoint.X), Mathematics.accuracy_medium))) > 0.9)
                        return false;
                }
            }
            return true;
        }

        private bool checkLinesConnect(ref List<MyStraightLine> somelines, ref List<MyPoint> points, int currentModel)
        {
            foreach (MyStraightLine line in somelines)
            {
                MyPoint pointlinestart = this.parent.currentFullModel.geometryModel.Points.Find(
                    thispointlines => ((thispointlines.X == line.StartPoint.X) && (thispointlines.Y == line.StartPoint.Y)));
                MyPoint pointlineend = this.parent.currentFullModel.geometryModel.Points.Find(
                    thispointlinee => ((thispointlinee.X == line.EndPoint.X) && (thispointlinee.Y == line.EndPoint.Y)));
                MyPoint fpoint = points.Find(
                    fthispoint => ((fthispoint.X == pointlinestart.X) && (fthispoint.Y == pointlinestart.Y)));
                if (fpoint == null)
                    points.Add(pointlinestart);
                MyPoint spoint = points.Find(
                    sthispoint => ((sthispoint.X == pointlineend.X) && (sthispoint.Y == pointlineend.Y)));
                if (spoint == null)
                    points.Add(pointlineend);
            }
            points.Sort(delegate(MyPoint point1, MyPoint point2)
            {
                return point1.X.CompareTo(point2.X);
            });

            if (somelines.Count == points.Count - 1)
                return true;
            else return false;
        }

        private bool checkArcsConnect(ref List<MyArc> somearc, ref List<MyPoint> points, int currentModel)
        {
            foreach (MyArc arc in somearc)
            {
                MyPoint pointarcstart = this.parent.currentFullModel.geometryModel.Points.Find(
                    thispointarc => ((thispointarc.X == arc.StartPoint.X) && (thispointarc.Y == arc.StartPoint.Y)));
                MyPoint pointarcend = this.parent.currentFullModel.geometryModel.Points.Find(
                    thispointarce => ((thispointarce.X == arc.EndPoint.X) && (thispointarce.Y == arc.EndPoint.Y)));
                MyPoint fpoint = points.Find(
                    fthispoint => ((fthispoint.X == pointarcstart.X) && (fthispoint.Y == pointarcstart.Y)));
                if (fpoint == null)
                    points.Add(pointarcstart);
                MyPoint spoint = points.Find(
                    sthispoint => ((sthispoint.X == pointarcend.X) && (sthispoint.Y == pointarcend.Y)));
                if (spoint == null)
                    points.Add(pointarcend);
            }
            points.Sort(delegate(MyPoint point1, MyPoint point2)
            {
                return point1.X.CompareTo(point2.X);
            });
            if (somearc.Count <= points.Count)
                return true;
            else return false;
        }

        private double CalculateIntegral(double a, double b, int n)
        {
            double h = (b - a) / n, x = a, result = 0;
            result += (calculateExpression(a) + calculateExpression(b))*h/3.0;
            for (int i = 1; i < n ; i++)
            {
                x = a + h * i;
                if (i % 2 == 0) result += 2.0 * calculateExpression(x) * h / 3.0;
                else result += 4.0 * calculateExpression(x) * h / 3.0; ;
            }
            return result;
        }

        /// <summary>
        /// функция, реализующая расчет приложенной нагрузки в узлах, лежащих на прямой 
        /// </summary>
        /// <param name="nodes">Список узлов, лежащих на линии</param>
        /// <param name="type">Тип линии</param>
        private void calculateForceAtNodes(List<MyNode> nodes, LineType type, MyArc arc, int currentModel)
        {
            // количество итераций для алгоритма симпсона
            int splitCount = 500;
            // мы имеем список узлов, лежащих на стороне, к которой приложенна нагрузка. надо их отсортировать по возрастанию координат
            switch (type)
            {
                case LineType.ParallelOX:
                    if (this.ascSort.Checked)
                        nodes.Sort(delegate(MyNode node1, MyNode node2)
                        { return node1.X.CompareTo(node2.X); });
                    else
                        nodes.Sort(delegate(MyNode node1, MyNode node2)
                        { return -node1.X.CompareTo(node2.X); });
                    break;
                case LineType.ParallelOY:
                    if (this.ascSort.Checked)
                        nodes.Sort(delegate(MyNode node1, MyNode node2)
                        { return node1.Y.CompareTo(node2.Y); });
                    else
                        nodes.Sort(delegate(MyNode node1, MyNode node2)
                        { return -node1.Y.CompareTo(node2.Y); });
                    break;
                case LineType.NotParallel:
                    if (this.ascSort.Checked)
                    {
                        if (this.sortByX.Checked == true)
                            nodes.Sort(delegate(MyNode node1, MyNode node2)
                            { return node1.X.CompareTo(node2.X); });
                        else
                            nodes.Sort(delegate(MyNode node1, MyNode node2)
                            { return node1.Y.CompareTo(node2.Y); });
                    }
                    else
                    {
                        if (this.sortByX.Checked == true)
                            nodes.Sort(delegate(MyNode node1, MyNode node2)
                            { return -node1.X.CompareTo(node2.X); });
                        else
                            nodes.Sort(delegate(MyNode node1, MyNode node2)
                            { return -node1.Y.CompareTo(node2.Y); });
                    }
                    break;
                case LineType.IsArc:
                    // упорядочиваем точки дуги по часовой
                    if (ascSort.Checked)
                    {
                        nodes.Sort(delegate(MyNode point1, MyNode point2)
                        {
                            double phi1 = Math.Atan2(point1.Y - arc.CenterPoint.Y, point1.X - arc.CenterPoint.X);
                            double phi2 = Math.Atan2(point2.Y - arc.CenterPoint.Y, point2.X - arc.CenterPoint.X);
                            if (Math.Abs(phi2 - phi1) < Math.PI)
                                return -phi1.CompareTo(phi2);
                            else
                                return phi1.CompareTo(phi2);
                        });
                    }
                    else
                    {
                        nodes.Sort(delegate(MyNode point1, MyNode point2)
                        {
                            double phi1 = Math.Atan2(point1.Y - arc.CenterPoint.Y, point1.X - arc.CenterPoint.X);
                            double phi2 = Math.Atan2(point2.Y - arc.CenterPoint.Y, point2.X - arc.CenterPoint.X);
                            if (Math.Abs(phi2 - phi1) < Math.PI)
                                return phi1.CompareTo(phi2);
                            else
                                return -phi1.CompareTo(phi2);
                        });
                    }
                    break;
            }
            List<MyNode> excludedNodes = new List<MyNode>();
            if (cbxExcludeNodes.Checked)
            {
                string[] strNumbers = txtExcludedNodes.Text.Split(',');
                for (int i = 0; i < strNumbers.Length; i++)
                {
                    int num;
                    MyNode node;
                    bool error = false;
                    if (!int.TryParse(strNumbers[i], out num)) error = true;
                    else
                    {
                        node = nodes.Find(n => n.Id == num);
                        if (node == null)
                            error = true;
                        else
                            excludedNodes.Add(node);
                    }
                    if (error)
                    {
                        MessageBox.Show("Неверно заданы узлы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            bool excludeFirstNode = false, excludeLastNode = false;
            foreach (MyNode node in excludedNodes)
            {
                if (nodes.IndexOf(node) == 0) excludeFirstNode = true;
                else if (nodes.IndexOf(node) == nodes.Count - 1) excludeLastNode = true;
                else nodes.Remove(node);
            }

            if (type != LineType.IsArc)
            {
                // далее расчитываем значение нагрузки в узле по методу трапеции
                double x1 = 0;
                double y1 = 0;
                double x2 = 0;
                double y2 = 0;
                double x3 = 0;
                double y3 = 0;
                double d21 = 0;
                double d13 = 0;
                double S = 0;
                double d0 = 0.0;
                double Sum, A, B;
                double CH = (this.parent.currentFullModel.geometryModel.Points.Max(point => point.Y) - this.parent.currentFullModel.geometryModel.Points.Min(point => point.Y)) / 2.0;
                CH = CH + this.parent.currentFullModel.geometryModel.Points.Min(point => point.Y);
                double CW = (this.parent.currentFullModel.geometryModel.Points.Max(point => point.X) - this.parent.currentFullModel.geometryModel.Points.Min(point => point.X)) / 2.0;
                CW = CW + this.parent.currentFullModel.geometryModel.Points.Min(point => point.X);
                double k = (nodes[1].Y - nodes[0].Y) / (nodes[1].X - nodes[0].X);
                //double test = 2 * 3.14 - k;
                //test = test + 1;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if ((i == 0 && excludeFirstNode) || (i == nodes.Count - 1 && excludeLastNode)) continue;
                    //Координата текущей точки
                    x1 = nodes[i].X;
                    y1 = nodes[i].Y;
                    //Координата предыдущей точки
                    if (i != 0)
                    {
                        x3 = nodes[i - 1].X;
                        y3 = nodes[i - 1].Y;
                        if (i == 1 && excludeFirstNode)
                        {
                            x3 -= (x1 - x3);
                            y3 -= (y1 - y3);
                        }
                    }
                    else
                    {
                        x3 = x1;
                        y3 = y1;
                    }
                    //Координата следующей точки
                    if (i != nodes.Count - 1)
                    {
                        x2 = nodes[i + 1].X;
                        y2 = nodes[i + 1].Y;
                        if (i == nodes.Count - 2 && excludeLastNode)
                        {
                            x2 += (x2 - x1);
                            y2 += (y2 - y1);
                        }
                    }
                    else
                    {
                        x2 = x1;
                        y2 = y1;                        
                    }

                    // Сила приложенная к текущему узлу (вычисляется через площадь трапеции)
                    // S-размер участка поверхности между силами  

                    d21 = Mathematics.FindDist(x1, y1, x2, y2);
                    d13 = Mathematics.FindDist(x1, y1, x3, y3);

                    S = (d21 + d13) / 2.0;

                    Sum = CalculateIntegral(d0, d0 + S, splitCount);
                    
                    d0 += S;
                    if (type == LineType.ParallelOX)
                    {
                        if (radioButton1.Checked == true && nodes[0].Y < CH)
                            nodes[i].ForceY -= Sum;
                        else if (radioButton1.Checked == true)
                            nodes[i].ForceY += Sum;
                        if (radioButton2.Checked == true && nodes[0].Y < CH)
                            nodes[i].ForceY += Sum;
                        else if (radioButton2.Checked == true)
                            nodes[i].ForceY -= Sum;
                    }
                    if (type == LineType.ParallelOY)
                    {
                        if (radioButton1.Checked == true && nodes[0].X < CW)
                            nodes[i].ForceX -= Sum;
                        else if (radioButton1.Checked == true)
                            nodes[i].ForceX += Sum;
                        if (radioButton2.Checked == true && nodes[0].X < CW)
                            nodes[i].ForceX += Sum;
                        else if (radioButton2.Checked == true)
                            nodes[i].ForceX -= Sum;
                    }
                    if (type == LineType.NotParallel)
                    {
                        if (radioButton1.Checked == true)
                        {
                            if ((nodes[0].Y < nodes[1].Y && nodes[0].X < nodes[1].X)
                                || (nodes[0].Y > nodes[1].Y && nodes[0].X > nodes[1].X))
                            {
                                if (getFEnumber(nodes, LineAngleType.right_up, FESearchSpace.up, currentModel)
                                    > getFEnumber(nodes, LineAngleType.right_up, FESearchSpace.down, currentModel))
                                {
                                    nodes[i].ForceX += Sum * Math.Sin(Math.Atan(Math.Abs(k)));
                                    nodes[i].ForceY -= Sum * Math.Cos(Math.Atan(Math.Abs(k)));
                                }
                                else
                                {
                                    nodes[i].ForceX -= Sum * Math.Sin(Math.Atan(Math.Abs(k)));
                                    nodes[i].ForceY += Sum * Math.Cos(Math.Atan(Math.Abs(k)));
                                }
                            }
                            else if ((nodes[0].Y < nodes[1].Y && nodes[0].X > nodes[1].X)
                                || (nodes[0].Y > nodes[1].Y && nodes[0].X < nodes[1].X))
                            {
                                if (getFEnumber(nodes, LineAngleType.left_up, FESearchSpace.up, currentModel)
                                    > getFEnumber(nodes, LineAngleType.left_up, FESearchSpace.down, currentModel))
                                {
                                    nodes[i].ForceX -= Sum * Math.Sin(Math.Atan(Math.Abs(k)));
                                    nodes[i].ForceY -= Sum * Math.Cos(Math.Atan(Math.Abs(k)));
                                }
                                else
                                {
                                    nodes[i].ForceX += Sum * Math.Sin(Math.Atan(Math.Abs(k)));
                                    nodes[i].ForceY += Sum * Math.Cos(Math.Atan(Math.Abs(k)));
                                }
                            }
                        }
                        else if (radioButton2.Checked == true)
                        {
                            if ((nodes[0].Y < nodes[1].Y && nodes[0].X < nodes[1].X)
                                || (nodes[0].Y > nodes[1].Y && nodes[0].X > nodes[1].X))
                            {
                                if (getFEnumber(nodes, LineAngleType.right_up, FESearchSpace.up, currentModel)
                                    > getFEnumber(nodes, LineAngleType.right_up, FESearchSpace.down, currentModel))
                                {
                                    nodes[i].ForceX -= Sum * Math.Sin(Math.Atan(Math.Abs(k)));
                                    nodes[i].ForceY += Sum * Math.Cos(Math.Atan(Math.Abs(k)));
                                }
                                else
                                {
                                    nodes[i].ForceX += Sum * Math.Sin(Math.Atan(Math.Abs(k)));
                                    nodes[i].ForceY -= Sum * Math.Cos(Math.Atan(Math.Abs(k)));
                                }
                            }
                            else if ((nodes[0].Y < nodes[1].Y && nodes[0].X > nodes[1].X)
                                || (nodes[0].Y > nodes[1].Y && nodes[0].X < nodes[1].X))
                            {
                                if (getFEnumber(nodes, LineAngleType.left_up, FESearchSpace.up, currentModel)
                                    > getFEnumber(nodes, LineAngleType.left_up, FESearchSpace.down, currentModel))
                                {
                                    nodes[i].ForceX += Sum * Math.Sin(Math.Atan(Math.Abs(k)));
                                    nodes[i].ForceY += Sum * Math.Cos(Math.Atan(Math.Abs(k)));
                                }
                                else
                                {
                                    nodes[i].ForceX -= Sum * Math.Sin(Math.Atan(Math.Abs(k)));
                                    nodes[i].ForceY -= Sum * Math.Cos(Math.Atan(Math.Abs(k)));
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //расчет нагрузки на дуге
                double x1 = 0;
                double y1 = 0;
                double x2 = 0;
                double y2 = 0;
                double x3 = 0;
                double y3 = 0;
                double L31 = 0;
                double L12 = 0;
                double S;
                double d0 = 0;

                for (int i = 0; i < nodes.Count; i++)
                {
                    if ((i == 0 && excludeFirstNode) || (i == nodes.Count - 1 && excludeLastNode)) continue;
                    //Координата текущей точк                    
                    x1 = nodes[i].X;
                    y1 = nodes[i].Y;
                    //Координата предыдущей точки
                    if (i != 0)
                    {
                        x3 = nodes[i - 1].X;
                        y3 = nodes[i - 1].Y;
                        if (i == 1 && excludeFirstNode)
                        {
                            x3 -= (x1 - x3);
                            y3 -= (y1 - y3);
                        }
                    }
                    else
                    {
                        x3 = x1;
                        y3 = y1;
                    }

                    //Координата следующей точки
                    if (i != nodes.Count - 1)
                    {
                        x2 = nodes[i + 1].X;
                        y2 = nodes[i + 1].Y;
                        if (i == nodes.Count - 2 && excludeLastNode)
                        {
                            x2 += (x2 - x1);
                            y2 += (y2 - y1);
                        }
                    }
                    else
                    {
                        x2 = x1;
                        y2 = y1;
                    }

                    // R1 - сила приложенная к текущему узлу
                    double r = Mathematics.FindDist(arc.CenterPoint, arc.StartPoint);
                    L31 = Mathematics.ArcLength(x1, y1, x3, y3, arc.CenterPoint.X, arc.CenterPoint.Y);
                    L12 = Mathematics.ArcLength(x1, y1, x2, y2, arc.CenterPoint.X, arc.CenterPoint.Y);

                    double R1;

                    S = (L12 + L31) / 2.0;
                    R1 = CalculateIntegral(d0, d0 + S, splitCount);
                    d0 += S;
                    if (radioButton1.Checked == true)
                    {
                        if (getFEnumber(nodes, arc.CenterPoint, FESearchSpace.inside, currentModel)
                            > getFEnumber(nodes, arc.CenterPoint, FESearchSpace.outside, currentModel))
                        {
                            nodes[i].ForceX += R1 * (x1 - arc.CenterPoint.X) / r;
                            nodes[i].ForceY += R1 * (y1 - arc.CenterPoint.Y) / r;
                        }
                        else
                        {
                            nodes[i].ForceX -= R1 * (x1 - arc.CenterPoint.X) / r;
                            nodes[i].ForceY -= R1 * (y1 - arc.CenterPoint.Y) / r;
                        }
                    }
                    if (radioButton2.Checked == true)
                    {
                        if (getFEnumber(nodes, arc.CenterPoint, FESearchSpace.inside, currentModel)
                            > getFEnumber(nodes, arc.CenterPoint, FESearchSpace.outside, currentModel))
                        {
                            nodes[i].ForceX -= R1 * (x1 - arc.CenterPoint.X) / r;
                            nodes[i].ForceY -= R1 * (y1 - arc.CenterPoint.Y) / r;
                        }
                        else
                        {
                            nodes[i].ForceX += R1 * (x1 - arc.CenterPoint.X) / r;
                            nodes[i].ForceY += R1 * (y1 - arc.CenterPoint.Y) / r;
                        }
                    }
                }
            }
        }

        private int getFEnumber(List<MyNode> nodes, MyPoint CenterPoint, FESearchSpace space, int currentModel)
        {
            int numberFE = 0;
            double radius = Mathematics.FindDist(new MyPoint(nodes[0].X, nodes[0].Y, MyPoint.PointType.IsAreaNode), CenterPoint);
            foreach (MyNode thisnode in nodes)
            {
                MyFiniteElement thisFE = this.parent.currentFullModel.FiniteElementModels[currentModel].FiniteElements.Find(
                    FE => (FE.Nodes[0].X == thisnode.X && FE.Nodes[0].Y == thisnode.Y)
                        || (FE.Nodes[1].X == thisnode.X && FE.Nodes[1].Y == thisnode.Y)
                            || (FE.Nodes[2].X == thisnode.X && FE.Nodes[2].Y == thisnode.Y));
                foreach (MyNode FEnode in thisFE.Nodes)
                {
                    if (space == FESearchSpace.outside)
                    {
                        if (Mathematics.FindDist(new MyPoint(FEnode.X, FEnode.Y, MyPoint.PointType.IsAreaNode), CenterPoint) > radius)
                            numberFE++;
                    }
                    else if (space == FESearchSpace.inside)
                    {
                        if (Mathematics.FindDist(new MyPoint(FEnode.X, FEnode.Y, MyPoint.PointType.IsAreaNode), CenterPoint) < radius)
                            numberFE++;
                    }
                }
            }
            return numberFE;
        }

        private int getFEnumber(List<MyNode> nodes, LineAngleType angleType, FESearchSpace space, int currentModel)
        {
            int numberFE = 0;
            foreach (MyNode thisnode in nodes)
            {
                MyFiniteElement thisFE = this.parent.currentFullModel.FiniteElementModels[currentModel].FiniteElements.Find(
                    FE => (FE.Nodes[0].X == thisnode.X && FE.Nodes[0].Y == thisnode.Y)
                        || (FE.Nodes[1].X == thisnode.X && FE.Nodes[1].Y == thisnode.Y)
                            || (FE.Nodes[2].X == thisnode.X && FE.Nodes[2].Y == thisnode.Y));
                foreach (MyNode FEnode in thisFE.Nodes)
                {
                    if (angleType == LineAngleType.right_up)
                    {
                        if (space == FESearchSpace.up)
                        {
                            if ((FEnode.X < thisnode.X && FEnode.Y <= thisnode.Y)
                                || (FEnode.X < thisnode.X && FEnode.Y >= thisnode.Y))
                                numberFE++;
                        }
                        else if (space == FESearchSpace.down)
                        {
                            if ((FEnode.Y < thisnode.Y && FEnode.X <= thisnode.X)
                                || (FEnode.Y < thisnode.Y && FEnode.X >= thisnode.X))
                                numberFE++;
                        }
                    }
                    else if (angleType == LineAngleType.left_up)
                    {
                        if (space == FESearchSpace.up)
                        {
                            if ((FEnode.Y > thisnode.Y && FEnode.X <= thisnode.X)
                                || (FEnode.Y > thisnode.Y && FEnode.X >= thisnode.X))
                                numberFE++;
                        }
                        else if (space == FESearchSpace.down)
                        {
                            if ((FEnode.X < thisnode.X && FEnode.Y <= thisnode.X)
                                || (FEnode.X < thisnode.X && FEnode.Y >= thisnode.X))
                                numberFE++;
                        }
                    }
                }
            }
            return numberFE;
        }

        private void findNodesAtArc(ref List<MyNode> forcedNodes, MyArc arc, int currentModel)
        {
            foreach (MyNode node in this.parent.currentFullModel.FiniteElementModels[currentModel].Nodes)
            {
                if (this.parent.pointFitsArc(new MyPoint(node.X, node.Y, MyPoint.PointType.IsGeometryPoint), arc, ProjectForm.checkType.boundPrecision) == true)
                {
                    if (forcedNodes.IndexOf(node) == -1) forcedNodes.Add(node);
                }
            }
        }

        private void findNodesAtStraightLine(ref List<MyNode> forcedNodes, MyStraightLine sline, int currentModel)
        {
            foreach (MyNode node in this.parent.currentFullModel.FiniteElementModels[currentModel].Nodes)
            {
                // уравнение прямой, проходящей через две точки
                if (Math.Abs((node.X - sline.StartPoint.X) * (sline.StartPoint.Y - sline.EndPoint.Y) - (node.Y - sline.StartPoint.Y) * (sline.StartPoint.X - sline.EndPoint.X)) < 0.001)
                {
                    List<MyPoint> tempPoints = new List<MyPoint>();
                    tempPoints.Add(sline.StartPoint);
                    tempPoints.Add(sline.EndPoint);

                    double minX, maxX, minY, maxY;
                    minX = tempPoints[0].X;
                    maxX = tempPoints[0].X;
                    minY = tempPoints[0].Y;
                    maxY = tempPoints[0].Y;
                    foreach (MyPoint point in tempPoints)  // тут определяем мин и макс координаты точек
                    {
                        if (point.X < minX) minX = point.X;
                        if (point.X > maxX) maxX = point.X;
                        if (point.Y < minY) minY = point.Y;
                        if (point.Y > maxY) maxY = point.Y;
                    }
                    /*if (!(node.X < minX || node.Y < minY || node.X > maxX || node.Y > maxY)) // условие, гарантирующее, что точка лежит на отрезке, а не на прямой
                    {
                        if (forcedNodes.IndexOf(node) == -1) forcedNodes.Add(node);
                    }*/

                    if (Math.Abs(minX - maxX) < 0.001) // если линия вертикальная
                    {
                        if (!(node.Y < minY || node.Y > maxY)) // условие, гарантирующее, что точка от клика мышки лежит на отрезке, а не на прямой
                        {
                            if (forcedNodes.IndexOf(node) == -1) forcedNodes.Add(node);
                        }
                    }
                    if (Math.Abs(minY - maxY) < 0.001) // если линия горизонтальная
                    {
                        if (!(node.X < minX || node.X > maxX)) // условие, гарантирующее, что точка от клика мышки лежит на отрезке, а не на прямой
                        {
                            if (forcedNodes.IndexOf(node) == -1) forcedNodes.Add(node);
                        }
                    }
                    if (!(node.X < minX || node.Y < minY || node.X > maxX || node.Y > maxY)) // условие, гарантирующее, что точка от клика мышки лежит на отрезке, а не на прямой
                    {
                        if (forcedNodes.IndexOf(node) == -1) forcedNodes.Add(node);
                    }


                }
            }
        }

        private bool trytocalculateExpression()
        {
            try
            {
                PreparedExpression preparedExpression = ToolsHelper.Parser.Parse(function.Text.Replace(',', '.').ToLower());
                CompiledExpression compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);
                CompiledExpression optimizedExpression = ToolsHelper.Optimizer.Optimize(compiledExpression);
                List<VariableValue> variables = new List<VariableValue>();
                variables.Add(new VariableValue(0, "x"));
                double res = ToolsHelper.Calculator.Calculate(compiledExpression, variables);
            }
            catch
            {
                return false;
            }
            return true;
        }

        // взято с http://habrahabr.ru/blogs/net/50158/
        private double calculateExpression(double firstValue)
        {
            /*try
            {*/
            // Compiling an expression
            PreparedExpression preparedExpression = ToolsHelper.Parser.Parse(function.Text.Replace(',', '.').ToLower());
            CompiledExpression compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);
            // Optimizing an expression
            CompiledExpression optimizedExpression = ToolsHelper.Optimizer.Optimize(compiledExpression);
            // Creating list of variables specified
            List<VariableValue> variables = new List<VariableValue>();


            variables.Add(new VariableValue(firstValue, "x"));

            // Do it !
            double res = ToolsHelper.Calculator.Calculate(compiledExpression, variables);
            return res;
            /*}
            catch (CompilerSyntaxException ex)
            {
                MessageBox.Show(String.Format("Compiler syntax error: {0}", ex.Message));

            }
            catch (MathProcessorException ex)
            {
                MessageBox.Show(String.Format("Error: {0}", ex.Message));

            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error in input data.");

            }
            catch (Exception)
            {
                MessageBox.Show("Unexpected exception.");
                throw;
            }*/
            //return 0.0;
        }


        private void okButton_Click(object sender, EventArgs e)
        {
            this.OK();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void number_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (number.Text != "")
                {
                    string[] linesnumbers = this.number.Text.Split(',');
                    for (int i = 0; i < linesnumbers.Length; i++)
                    {
                        if (linesnumbers[i] == "" && i < linesnumbers.Length - 1)
                            throw (null);
                        else
                        {
                            for (int j = 0; j < linesnumbers[i].Length; j++)
                            {
                                if (linesnumbers[i].ElementAt(j) < 48 || linesnumbers[i].ElementAt(j) > 57)
                                    throw (null);
                            }
                        }
                    }
                }
            }
            catch
            {
               // this.number.Clear();
                MessageBox.Show("Недопустимое значение!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void Forces_Load(object sender, EventArgs e)
        {
            parent.applyingForce = true;
            parent.showForces.Checked = true;
        }

        private void Forces_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.applyingForce = false;
            parent.activeForm = null;
            parent.clearSelection();
        }

        private void cbxExcludeNodes_CheckedChanged(object sender, EventArgs e)
        {
            txtExcludedNodes.Enabled = cbxExcludeNodes.Checked;
        }

        private void function_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
