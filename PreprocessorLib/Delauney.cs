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
using DelauneyPaulBourke.Geometry;

namespace PreprocessorLib
{
    public partial class Delauney : Form
    {
        private ProjectForm parent;
        MyFiniteElementModel currentModel;
        DelauneySettings settingsForm = null;
        public List<MyNode> points;
        int nodesCount;
        public Delauney(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            currentModel = parent.currentFullModel.FiniteElementModels.Find(m => m.ModelName == parent.currentFullModel.currentGridName);
            gridName.Text = "Сетка №" + (parent.currentFullModel.IdCandidate).ToString();
            ddlTriangulationMethod.SelectedIndex = ddlGenerationMethod.SelectedIndex = 0;
            if (parent.currentFullModel.FiniteElementModels.Count == 0)
                ddlGenerationMethod.Items.RemoveAt(4);
            settingsForm = new DelauneySettings(parent);
            settingsForm.FormClosing += new FormClosingEventHandler(settingsForm_FormClosing);
        }



        private List<MyNode> findNodesAtStraightLine(List<MyNode> nodes, MyStraightLine sline)
        {
            List<MyNode> result = new List<MyNode>();
            foreach (MyNode node in nodes)
                if (Mathematics.pointOnLine(node.X, node.Y, sline))
                    if (result.IndexOf(node) == -1) result.Add(node);
            return result;
        }

        private List<MyNode> findNodesAtArc(List<MyNode> nodes, MyArc arc)
        {
            List<MyNode> result = new List<MyNode>();
            foreach (MyNode node in nodes)
            {
                // уравнение окружности
                if (parent.pointFitsArc(node.X, node.Y, arc, ProjectForm.checkType.doublePrecision))
                    if (result.IndexOf(node) == -1) result.Add(node);
            }
            return result;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string modelName = gridName.Text;
            int elemId = 0;
            errorBadGridName.Visible = false;

            if (gridName.Text == string.Empty)
            {
                MessageBox.Show("Имя сетки не может быть пустым!");
                return;
            }
            if (parent.currentFullModel.FiniteElementModels.Find(m => m.ModelName == modelName) != null)
            {
                errorBadGridName.Visible = true;
                return;
            }
            if (ddlGenerationMethod.SelectedIndex == -1)
            {
                MessageBox.Show("Не задан метод генерации точек!");
                return;
            }
            this.Hide();
            parent.StartProgress("Построение сетки");
            List<MyFrontSegment> segments = new List<MyFrontSegment>();
            nodesCount = 0;
            MyFiniteElementModel newModel = new MyFiniteElementModel(parent.currentFullModel.IdCandidate, gridName.Text, MyFiniteElementModel.GridType.Normal);
            // если выбрана триангуляция на основе существующих узлов
            if (ddlGenerationMethod.SelectedIndex == 4)
            {
                List<MyFiniteElement> elems = GetTriangulation(currentModel.Nodes);
                newModel.Nodes.AddRange(currentModel.Nodes);
                newModel.Nodes.ForEach(n => n.finiteElements.Clear());
                foreach (MyFiniteElement elem in elems)
                {
                    double cx = elem.Nodes.Sum(s => s.X) / 3;
                    double cy = elem.Nodes.Sum(s => s.Y) / 3;
                    foreach (MyArea area in parent.currentFullModel.geometryModel.Areas)
                    {
                        //if (Mathematics.ContainsPoint(area.Nodes, cx, cy))
                        if (PointFits(area, new List<MyNode>(), new MyPoint(cx, cy), double.MinValue))
                        {
                            elem.Id = ++elemId;
                            elem.areaId = area.Id;
                            elem.LinkNodes();
                            newModel.FiniteElements.Add(elem);
                            break;
                        }
                    }
                }
            }
            // любая другая триангуляция
            else
            {
                foreach (MyArea area in parent.currentFullModel.geometryModel.Areas)
                {
                    List<MyNode> result = new List<MyNode>();
                    for (int j = 0; j < 4; j++)
                    {
                        int neighbor = parent.currentFullModel.geometryModel.joinTable[area.Id - 1, j] - 1;
                        if (neighbor != -1)
                            if (neighbor < area.Id - 1)
                            {
                                List<MyLine> lines = area.Lines.FindAll(l => l.Areas.Contains(area.Id) && l.Areas.Contains(neighbor + 1));
                                foreach (MyLine line in lines)
                                {
                                    if (line is MyStraightLine)
                                        result.AddRange(findNodesAtStraightLine(segments[neighbor].Nodes, line as MyStraightLine));
                                    else
                                        result.AddRange(findNodesAtArc(segments[neighbor].Nodes, line as MyArc));
                                }
                            }
                    }
                    result = result.Distinct().ToList();
                    double h = (double)nudMinDistance.Value;
                    foreach (MyPoint p in area.Nodes)
                        if (result.Find(n => Mathematics.sameNode(n,p)) == null)
                            result.Add(new MyNode(p.X, p.Y, ++nodesCount));
                    GeneratePoints(area, result);
                    MyFrontSegment seg = new MyFrontSegment(area);
                    seg.Nodes = result;
                    segments.Add(seg);
                    AddNodesFromCommonLine(seg, segments);
                }

                elemId = 0;
                if (ddlGenerationMethod.SelectedIndex == 1)
                    GenerateNodesInDensitySectors(segments);

                foreach (MyFrontSegment seg in segments)
                {
                    foreach (MyNode node in seg.Nodes)
                        if (!newModel.Nodes.Contains(node))
                            newModel.Nodes.Add(node);
                }
                if (ddlGenerationMethod.SelectedIndex == 1)
                {
                    foreach (MyFiniteElement elem in GetTriangulation(newModel.Nodes))
                    {
                        double cx = elem.Nodes.Sum(s => s.X) / 3;
                        double cy = elem.Nodes.Sum(s => s.Y) / 3;
                        foreach (MyArea area in parent.currentFullModel.geometryModel.Areas)
                        {
                            //if (Mathematics.ContainsPoint(area.Nodes, cx, cy))
                            if (PointFits(area, new List<MyNode>(), new MyPoint(cx, cy), 0.001))
                            {
                                elem.Id = ++elemId;
                                elem.LinkNodes();
                                newModel.FiniteElements.Add(elem);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (MyFrontSegment seg in segments)
                    {
                        foreach (MyFiniteElement elem in GetTriangulation(seg.Nodes, seg.CorrespondingArea.Id))
                        {
                            double cx = elem.Nodes.Sum(s => s.X) / 3;
                            double cy = elem.Nodes.Sum(s => s.Y) / 3;
                            MyArea area = parent.currentFullModel.geometryModel.Areas[segments.IndexOf(seg)];
                            //if (PointFits(Mathematics.ContainsPoint(area.Nodes, cx, cy))
                            if (PointFits(area, new List<MyNode>(), new MyPoint(cx, cy), 0.001))
                            {
                                elem.Id = ++elemId;
                                elem.LinkNodes();
                                seg.finiteElems.Add(elem);
                            }
                        }
                        newModel.FiniteElements.AddRange(seg.finiteElems);
                    }                    
                }
            }
            newModel.baseType = newModel.type = MyFiniteElementModel.GridType.Delauney;
            parent.currentFullModel.densityPoints.Clear();
            parent.EndProgress();
            parent.ModelCreated(newModel);
            Close();
            // создаем для новой КЭ модели id
        }

        private void GenerateNodesInDensitySectors(List<MyFrontSegment> segments)
        {
            foreach (DensityPoint dPoint in parent.currentFullModel.densityPoints)
            {
                List<MyArea> areas = new List<MyArea>();
                int totalCount = 0;
                foreach (MyFrontSegment seg in segments)
                {
                    List<MyNode> innerNodes = seg.Nodes.FindAll(n => Mathematics.FindDist(n.X, n.Y, dPoint.X, dPoint.Y) < dPoint.R);
                    if (innerNodes.Count > 0)
                    {
                        innerNodes.ForEach(n =>
                        {
                            if (seg.CorrespondingArea.Nodes.Find(p => Mathematics.sameNode(n, p)) == null
                                //&& seg.CorrespondingArea.StraightLines.Find(l => Mathematics.pointOnLine(n, l)) == null &&
                                //seg.CorrespondingArea.Arcs.Find(a => Mathematics.pointFitsArc(n,a,0.01)) == null
                            )
                                seg.Nodes.Remove(n);
                            else
                                totalCount++;
                        }
                        );
                        areas.Add(seg.CorrespondingArea);
                    }
                }
                Random generator = new Random((int)DateTime.Now.Ticks);
                
                for (int i = 0; totalCount < dPoint.val; i++)
                {
                    int triesCount = (int)nudTriesCount.Value;
                    do
                    {
                        double x = dPoint.X - dPoint.R + generator.NextDouble() * dPoint.R * 2.0;
                        double y = dPoint.Y - dPoint.R + generator.NextDouble() * dPoint.R * 2.0;
                        MyNode p = new MyNode(x, y);
                        if (Mathematics.FindDist(x, y, dPoint.X, dPoint.Y) > dPoint.R) continue;

                        p.Id = -1;
                        bool added = false;

                        foreach (MyArea area in areas)
                        {
                            MyFrontSegment seg = segments.Find(s => s.CorrespondingArea == area);
                            if (PointFits(area, seg.Nodes, p, dPoint.h))
                            {
                                seg.Nodes.Add(p);
                                added = true;
                            }
                        }
                        if (added) break;
                    } while (--triesCount > 0);
                    if (triesCount == 0) break;
                    totalCount++;
                }
            }
            List<MyNode> nodesToRenum = new List<MyNode>();
            foreach (MyFrontSegment seg in segments)
                nodesToRenum.AddRange(seg.Nodes);
            nodesToRenum = nodesToRenum.Distinct().ToList();
            for (int i = 0; i < nodesToRenum.Count; i++)
                nodesToRenum[i].Id = i + 1;
        }

        private List<MyFiniteElement> GetTriangulation(List<MyNode> nodes, int correspondingArea = 0)
        {
            List<MyFiniteElement> elements = new List<MyFiniteElement>();
            if (ddlTriangulationMethod.SelectedIndex == 0)
            {
                List<DelaunayTriangulator.Vertex> points = nodes.ConvertAll(n => new DelaunayTriangulator.Vertex((float)n.X, (float)n.Y));
                DelaunayTriangulator.Triangulator worker = new DelaunayTriangulator.Triangulator();
                List<DelaunayTriangulator.Triad> triads = worker.Triangulation(points);
                foreach (DelaunayTriangulator.Triad triad in triads)
                {
                    MyNode n1 = nodes[triad.a];
                    MyNode n2 = nodes[triad.b];
                    MyNode n3 = nodes[triad.c];
                    // НЕ Приввязываем узлы - возможно этот КЭ не пройдет дальнейший проверки, а привязка останется
                    // коллапс будет ещё какой :)
                    MyFiniteElement rElemt = new MyFiniteElement(0, 0, new MyNode[] { n1, n2, n3 }, correspondingArea - 1, false);

                    // Этот алгоритм иногда генерирует элементы из узлов, лежащих на одной прямой. Почему - уже не успею разобраться :)
                    // Скорее всего, проблема в относительной неточности координат точек модели: где-нибудь на геометрической проверке вываливается.
                    // В общем, такие элементы игнорируем.
                    double[] angles = Mathematics.getFEangles(rElemt);
                    bool badAngles = false;
                    if (angles.Any(a =>a.Equal(0))) badAngles = true;
                     if (angles.Any(a=> double.IsNaN(a))) badAngles = true;
                     else if (angles.Any( a=> Math.Abs(a) < 0.01)) badAngles = true;

                    if (!badAngles)
                        elements.Add(rElemt);
                }
            }
            else
            {
                List<DelauneyPaulBourke.Geometry.Point> points = nodes.ConvertAll(n => new DelauneyPaulBourke.Geometry.Point(n.X, n.Y, 0));
                List<Triangle> triads = DelauneyPaulBourke.Delauney.Triangulate(points);
                foreach (Triangle triad in triads)
                {
                    MyNode n1 = nodes[triad.p1];
                    MyNode n2 = nodes[triad.p2];
                    MyNode n3 = nodes[triad.p3];
                    // НЕ Приввязываем узлы - возможно этот КЭ не пройдет дальнейший проверки, а привязка останется
                    // коллапс будет ещё какой :)
                    elements.Add(new MyFiniteElement(0, 0, new MyNode[] { n1, n2, n3 }, correspondingArea - 1, false));
                }
            }
            return elements;
        }

        private void AddNodesFromCommonLine(MyFrontSegment seg, List<MyFrontSegment> allSegs)
        {
            foreach (MyLine line in seg.CorrespondingArea.Lines)
            {
                for (int i = 0; i < line.Areas.Count; i++)
                {
                    int id = line.Areas[i];
                    if (id != seg.CorrespondingArea.Id)
                    {
                        MyFrontSegment targetSeg = allSegs.Find(s => s.CorrespondingArea.Id == id);
                        if (targetSeg != null)
                        {
                            List<MyNode> commonNodes;
                            if (line is MyStraightLine) commonNodes = findNodesAtStraightLine(seg.Nodes, line as MyStraightLine);
                            else commonNodes = findNodesAtArc(seg.Nodes, line as MyArc);

                            foreach (MyNode node in commonNodes)
                                if (!targetSeg.Nodes.Contains(node)) targetSeg.Nodes.Add(node);
                        }
                    }
                }
            }
        }

        private void GeneratePoints(MyArea area, List<MyNode> result)
        {
            if (ddlGenerationMethod.SelectedIndex == 0)
                ChaoticPointGeneration(area, result, (int)nudNodesCount.Value + 8);
            else if (ddlGenerationMethod.SelectedIndex == 1)
                ChaoticWithDensityGeneration(area, result);
            else if (ddlGenerationMethod.SelectedIndex == 2)
                RegularPointGeneration(area, result);
            else if (ddlGenerationMethod.SelectedIndex == 3)
                EqualAngleGeneration(area, result);
        }

        private void ChaoticWithDensityGeneration(MyArea area, List<MyNode> result)
        {
            int density = (int)nudNodesCount.Value;
            double h =(double)nudMinDistance.Value;
            AreaDensity aDens = settingsForm.areaDensity.Find(a => a.areaId == area.Id);
            if (aDens != null)
            {
                density = aDens.density;
                h = aDens.h;
            }

            if (density > 8)
                ChaoticPointGeneration(area, result, density, h);
        }

        private void RegularPointGeneration(MyArea area, List<MyNode> result)
        {
            MyRectangle rect = MyRectangle.GetAreaRectangle(area);

            double h = (double)nudMinDistance.Value;
            for (double x = rect.minX; x.LessOrEqual(rect.maxX); x += h)
                for (double y = rect.minY; y.LessOrEqual(rect.maxY); y += h)
                {
                    MyNode p = new MyNode(x, y);
                    if (PointFits(area, result, p))
                    {
                        p.Id = ++nodesCount;
                        result.Add(p);
                    }
                }
        }

        private void EqualAngleGeneration(MyArea area, List<MyNode> result)
        {
            MyRectangle rect = MyRectangle.GetAreaRectangle(area);
            double h = (double)nudMinDistance.Value;
            bool odd = false;
            double hy = h * Math.Pow(3.0, 0.5) / 2.0;
            for (double y = rect.minY; y.LessOrEqual(rect.maxY); y += hy)
            {
                for (double x = rect.minX; x.LessOrEqual(rect.maxX); x += h)
                {
                    MyNode p = new MyNode(odd ? x + h / 2 : x, y);
                    if (PointFits(area, result, p))
                    {
                        p.Id = ++nodesCount;
                        result.Add(p);
                    }
                }
                odd = !odd;
            }
        }

        private void ChaoticPointGeneration(MyArea area, List<MyNode> result, int count, double h = -1)
        {
            MyRectangle rect = MyRectangle.GetAreaRectangle(area);
            int maxNodes = count;
            Random generator = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; result.Count < maxNodes; i++)
            {
                int triesCount = (int)nudTriesCount.Value;
                do
                {
                    double x = rect.minX + generator.NextDouble() * rect.Width;
                    double y = rect.minY + generator.NextDouble() * rect.Height;
                    MyNode p = new MyNode(x, y);
                    if (PointFits(area, result, p, h))
                    {
                        p.Id = ++nodesCount;
                        result.Add(p);
                        break;
                    }
                } while (--triesCount > 0);
                if (triesCount == 0) break;
            }
        }

        private bool PointFits(MyArea area, List<MyNode> existingNodes, MyNode p, double h = -1)
        {
            double x = p.X, y = p.Y;
            if (h < 0)
                h = (double)nudMinDistance.Value;
            bool inside = Mathematics.ContainsPoint(area.Nodes, x, y) ||
                area.StraightLines.Find(l => Mathematics.pointOnLine(x, y, l)) != null ||
                area.Arcs.Find(a => Mathematics.pointFitsArc(p, a, 0.01)) != null;
            if (!inside) return false;

            foreach (MyStraightLine line in area.StraightLines)
            {
                MyPoint cp;
                if (Mathematics.pointOnLine(x, y, line))
                    cp = p;
                else
                    cp = Mathematics.crossPoint(p, line);
                if (Mathematics.FindDist(cp, p) < h)
                {
                    if (existingNodes.TrueForAll(n => Mathematics.FindDist(n, cp).MoreOrEqual(h)))
                    {
                        p.X = cp.X; p.Y = cp.Y;
                        return true;
                    }
                }
            }

            MyArc arc = area.Arcs.Find(a => Mathematics.pointFitsArc(p, a, h - 0.001));
            if (arc != null)
            {
                MyPoint cp = new MyPoint();
                double k = Mathematics.FindDist(arc.EndPoint, arc.CenterPoint) / Mathematics.FindDist(p, arc.CenterPoint);
                cp.X = arc.CenterPoint.X + (p.X - arc.CenterPoint.X) * k;
                cp.Y = arc.CenterPoint.Y + (p.Y - arc.CenterPoint.Y) * k;
                if (existingNodes.TrueForAll(n => Mathematics.FindDist(n, cp).MoreOrEqual(h)))
                {
                    p.X = cp.X; p.Y = cp.Y;
                    return true;
                }
            }

            if (existingNodes.TrueForAll(n => Mathematics.FindDist(n, p).MoreOrEqual(h))) return true;
            return false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ddlGenerationMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            nudNodesCount.Enabled = ddlGenerationMethod.SelectedIndex < 2;
            nudTriesCount.Enabled = ddlGenerationMethod.SelectedIndex < 2;
            btnSettings.Enabled = ddlGenerationMethod.SelectedIndex == 1;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            settingsForm.Show();
        }

        void settingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            settingsForm.Hide();
            this.Enabled = true;
        }

        public void AddDensity(double x, double y)
        {
            if (settingsForm != null && settingsForm.IsHandleCreated)
                settingsForm.AddSectorDensity(x, y);
        }
    }
}
