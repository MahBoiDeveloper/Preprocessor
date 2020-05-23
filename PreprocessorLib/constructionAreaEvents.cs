using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using ModelComponents;
using PreprocessorUtils;
using System.Text;

namespace PreprocessorLib
{
    public partial class ProjectForm : Form
    {
        [NonSerialized]
        public MyPoint hoveredPoint = null;

        private ToolTip elemInfoTooltip = new ToolTip();

        double mouseXcord, mouseYcord;


        private void constructionArea_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) scalePlus();
            else scaleMinus();
        }

        private void constructionArea_MouseMove(object sender, MouseEventArgs e)
        {
            mouseAboveArea = true;
            MyPoint clickPoint = visualizer.getClickPoint(e.X, e.Y);
            if (mouseXcord.Equal(clickPoint.X) && mouseYcord.Equal(clickPoint.Y)) return;
            mouseXcord = clickPoint.X;
            mouseYcord = clickPoint.Y;
            
            if (e.Button == MouseButtons.Left)
            {
                /* кусочек, который отвечает за перетаскивание области построения */
                if (!this.mouseAboveEditedPoint && !this.mouseAboveEditedNode && this.currentFullModel.geometryModel.editedPoint == null)
                {
                    this.mouseMoved = true;
                    visualizer.MoveBy((e.X - mouseX), (e.Y - mouseY), showGrid.Checked);
                    mouseX = e.X;
                    mouseY = e.Y;
                }

                /* кусочек, который отвечает за перетаскивание точки в режиме редактирования точки*/
                if (this.editPointsControl != null && this.editPointsControl.IsHandleCreated && this.mouseAboveEditedPoint)
                {
                    if (this.currentFullModel.geometryModel.editedPoint != null)
                    {
                        if (currentFullModel.geometryModel.editedPoint.IsCenterOfArc)
                        {
                            MessageBox.Show("Изменение центра дуги невозможно!");
                            this.mouseAboveEditedPoint = false;
                        }
                        else
                        {
                            MyPoint ePoint = currentFullModel.geometryModel.editedPoint;
                            if (currentFullModel.geometryModel.editedPoint.IsStartOfArc || currentFullModel.geometryModel.editedPoint.IsEndOfArc)
                            {
                                MyArc[] incidentArcs = currentFullModel.geometryModel.Arcs.Where(a => a.StartPoint == currentFullModel.geometryModel.editedPoint || a.EndPoint == currentFullModel.geometryModel.editedPoint).ToArray();
                                if (incidentArcs.Length > 0)
                                {
                                    MyPoint commonCenter;
                                    commonCenter = incidentArcs[0].CenterPoint;

                                    double R = Mathematics.FindDist(currentFullModel.geometryModel.editedPoint, commonCenter);
                                    double x;
                                    double y;
                                    double angle = Math.Atan2(clickPoint.Y - commonCenter.Y, clickPoint.X - commonCenter.X);
                                    x = commonCenter.X + R * Math.Cos(angle);
                                    y = commonCenter.Y + R * Math.Sin(angle);

                                    bool PointFits = true;
                                    foreach (MyArc arc in incidentArcs)
                                    {
                                        MyArc testArc;
                                        if (arc.EndPoint == currentFullModel.geometryModel.editedPoint) testArc = new MyArc(0, arc.Clockwise, arc.StartPoint, new MyPoint(x, y, MyPoint.PointType.IsGeometryPoint), arc.CenterPoint);
                                        else testArc = new MyArc(0, arc.Clockwise, new MyPoint(x, y, MyPoint.PointType.IsGeometryPoint), arc.EndPoint, arc.CenterPoint);

                                        if (!TestArc(testArc))
                                        {
                                            PointFits = false;
                                            break;
                                        }
                                    }
                                    if (PointFits)
                                    {
                                        currentFullModel.geometryModel.editedPoint.X = x;
                                        currentFullModel.geometryModel.editedPoint.Y = y;
                                    }
                                }
                                else
                                {
                                    currentFullModel.geometryModel.editedPoint.X = clickPoint.Y;
                                    currentFullModel.geometryModel.editedPoint.Y = clickPoint.Y;
                                }
                            }

                            else
                            {
                                currentFullModel.geometryModel.editedPoint.X = clickPoint.X;
                                currentFullModel.geometryModel.editedPoint.Y = clickPoint.Y;
                            }
                            if (currentFullModel.geometryModel.editedPoint.NodeReference != null) // точка может и не иметь ссылки на узел - если это центральная точка дуги, например
                            {
                                currentFullModel.geometryModel.editedPoint.NodeReference.X = this.currentFullModel.geometryModel.editedPoint.X;
                                currentFullModel.geometryModel.editedPoint.NodeReference.Y = this.currentFullModel.geometryModel.editedPoint.Y;
                            }
                            // отображаем id, x и y точки
                            this.editPointsControl.number.Text = this.currentFullModel.geometryModel.editedPoint.Id.ToString();
                            this.editPointsControl.x.Text = Mathematics.floor(this.currentFullModel.geometryModel.editedPoint.X, Mathematics.accuracy_medium).ToString();
                            this.editPointsControl.y.Text = Mathematics.floor(this.currentFullModel.geometryModel.editedPoint.Y, Mathematics.accuracy_medium).ToString();
                            ReDrawAll(true);
                        }
                    }
                }
                //////////////////////////////////////////////////////////////////  

                //кусочек, который отвечает за перетаскивание узла зоны в режиме редактирования узлов зоны
                if (this.editNodesControl != null && this.editNodesControl.IsHandleCreated && this.mouseAboveEditedNode)
                {
                    if (this.currentFullModel.geometryModel.editedNode != null)
                    {
                        this.currentFullModel.geometryModel.editedNode.X = clickPoint.X;
                        this.currentFullModel.geometryModel.editedNode.Y = clickPoint.Y;
                        if (currentFullModel.geometryModel.editedNode.PointReference != null) // точка может и не иметь ссылки на узел - если это центральная точка дуги, например
                        {
                            if (!(currentFullModel.geometryModel.editedNode.PointReference.IsCenterOfArc || currentFullModel.geometryModel.editedNode.PointReference.IsStartOfArc || currentFullModel.geometryModel.editedNode.PointReference.IsEndOfArc))
                            {
                                currentFullModel.geometryModel.editedNode.PointReference.X = this.currentFullModel.geometryModel.editedNode.X;
                                currentFullModel.geometryModel.editedNode.PointReference.Y = this.currentFullModel.geometryModel.editedNode.Y;
                            }
                        }

                        // отображаем id, x и y точки
                        this.editNodesControl.number.Text = this.currentFullModel.geometryModel.editedNode.Id.ToString();
                        this.editNodesControl.x.Text = Mathematics.floor(this.currentFullModel.geometryModel.editedNode.X, Mathematics.accuracy_medium).ToString();
                        this.editNodesControl.y.Text = Mathematics.floor(this.currentFullModel.geometryModel.editedNode.Y, Mathematics.accuracy_medium).ToString();
                        ReDrawAll(true);
                    }

                }
                //////////////////////////////////////////////////////////////////  
            }
            else
            {
                // подсвечиваем точки и линии

                MyPoint nearestPoint;
                if ((nearestPoint = currentFullModel.geometryModel.Points.Find(p => Math.Abs(clickPoint.X - p.X) < (double)visualizer.pointLocality && Math.Abs(clickPoint.Y - p.Y) < (double)visualizer.pointLocality)) != null)
                {
                    hoveredPoint = nearestPoint;
                    mouseXcord = nearestPoint.X;
                    mouseYcord = nearestPoint.Y;
                    ReDrawAll(true);
                }
                else
                {
                    if (hoveredPoint != null)
                    {
                        hoveredPoint = null;
                        ReDrawAll(true);
                    }
                }

                if (creatingCircle) // если мы в режиме рисования окружности
                {
                    if (this.currentFullModel.geometryModel.centerOfCircle != null)
                    {
                        double x1, y1;
                        if (hoveredPoint == null)
                        {
                            x1 = clickPoint.X; y1 = clickPoint.Y;
                        }
                        else
                        {
                            x1 = hoveredPoint.X; y1 = hoveredPoint.Y;
                        }

                        double radius = Mathematics.FindDist(x1, y1, currentFullModel.geometryModel.centerOfCircle.X, currentFullModel.geometryModel.centerOfCircle.Y);
                        if (this.addCirclesControl != null) this.addCirclesControl.radius.Text = Mathematics.floor(radius, Mathematics.accuracy_medium).ToString();
                        currentFullModel.geometryModel.tempCircle = new MyCircle(this.currentFullModel.geometryModel.NumOfCircles + 1, this.currentFullModel.geometryModel.centerOfCircle, radius);
                        ReDrawAll(true);
                    }
                }
                if (activeControl is GridAnalysis)
                {
                    MyFiniteElementModel model = currentFullModel.FiniteElementModels.Find(m => m.ModelName == currentFullModel.currentGridName);
                    MyFiniteElement elem = model.FiniteElements.Find(el => Mathematics.ContainsPoint(el.Nodes.ToArray(), clickPoint));
                    if (elem != null)
                    {
                        elemInfoTooltip.ToolTipTitle = "Элемент №" + elem.Id.ToString();
                        StringBuilder tooltipText = new StringBuilder();
                        tooltipText.AppendLine("Площадь: " + Mathematics.floor(Mathematics.GeronLaw(elem.Nodes), 0.01));
                        double[] angles = Mathematics.getFEangles(elem);
                        for (int i = 0; i < 3; i++)
                            tooltipText.AppendLine("Угол "+(i+1).ToString() + ": " + angles[i].ToString("###.##"));

                        elemInfoTooltip.Show(tooltipText.ToString(), constructionArea, new Point(e.X, e.Y));
                    }
                    else
                        elemInfoTooltip.Hide(constructionArea);
                }
            }
            this.textBox1.Text = Mathematics.floor(mouseXcord, Mathematics.accuracy_medium).ToString();
            this.textBox2.Text = Mathematics.floor(mouseYcord, Mathematics.accuracy_medium).ToString();
        }

        private void constructionArea_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            MyPoint clickPoint = visualizer.getClickPoint(e.X, e.Y);
            if (this.editPointsControl != null && this.editPointsControl.IsHandleCreated)
            {
                MyPoint nearestPoint = currentFullModel.geometryModel.Points.Find(p => Math.Abs(p.X - clickPoint.X) < (double)visualizer.pointLocality && Math.Abs(p.Y - clickPoint.Y) < (double)visualizer.pointLocality);
                if (nearestPoint != null)
                {
                    if (e.Button == MouseButtons.Left) // если нажали левую кнопку мыши, то выделить точку
                    {
                        this.currentFullModel.geometryModel.editedPoint = nearestPoint;
                        this.mouseAboveEditedPoint = true;
                        ReDrawAll();

                        // отображаем id, x и y точки
                        this.editPointsControl.number.Text = this.currentFullModel.geometryModel.editedPoint.Id.ToString();
                        this.editPointsControl.x.Text = Mathematics.floor(this.currentFullModel.geometryModel.editedPoint.X, 0.01).ToString();
                        this.editPointsControl.y.Text = Mathematics.floor(this.currentFullModel.geometryModel.editedPoint.Y, 0.01).ToString();
                    }
                    return;
                }
            }
            if (this.editNodesControl != null && this.editNodesControl.IsHandleCreated)
            {
                foreach (MyArea area in this.currentFullModel.geometryModel.Areas)
                {
                    MyPoint node = area.Nodes.Find(p => Math.Abs(p.X - clickPoint.X) < visualizer.pointLocality && Math.Abs(p.Y - clickPoint.Y) < visualizer.pointLocality);
                    if (node != null)
                    {
                        if (e.Button == MouseButtons.Left) // если нажали левую кнопку мыши, то выделить точку
                        {
                            this.currentFullModel.geometryModel.editedNode = node;
                            this.mouseAboveEditedNode = true;

                            // отображаем id, x и y точки
                            this.editNodesControl.number.Text = this.currentFullModel.geometryModel.editedNode.Id.ToString();
                            this.editNodesControl.x.Text = Mathematics.floor(this.currentFullModel.geometryModel.editedNode.X, Mathematics.accuracy_medium).ToString();
                            this.editNodesControl.y.Text = Mathematics.floor(this.currentFullModel.geometryModel.editedNode.Y, Mathematics.accuracy_medium).ToString();
                            ReDrawAll();
                        }
                        break;
                    }
                }
            }
        }

        private void constructionArea_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseAboveEditedPoint = false;
            this.mouseAboveEditedNode = false;

        }

        private void constructionArea_MouseLeave(object sender, EventArgs e)
        {
            mouseAboveArea = false;
        }

        private void constructionArea_MouseClick(object sender, MouseEventArgs e)
        {
            MyPoint clickPoint = visualizer.getClickPoint(e.X, e.Y);
            mouseAboveArea = true;
            if (this.mouseMoved)
            {
                this.mouseMoved = false;
                return; // смысл данного действа в том, что если область отображения перемещали мышкой, то ничего рисовать не нужно (перемещение происходит при одновременно нажатой левой клавише мыши и при ее движнии, и комп воспринимает это как событие MouseClick) 
            }
            constructionArea.Focus();

            // создаем точку
            if (addPointControl != null && addPointControl.IsHandleCreated)
            {
                MyPoint newPoint = this.addPointControl.CreatePoint(Convert.ToInt32(this.addPointControl.number.Text), clickPoint.X, clickPoint.Y);
                this.TestOnLineCircleAndArc(ref newPoint); // проверяем, не попала ли точка на линию, окружность или дугу  
            }

            // запоминаем выбранную точку, и если их две, то рисуем линию между ними
            if (addStraightLineControl != null && addStraightLineControl.IsHandleCreated)
            {
                MyPoint point = currentFullModel.geometryModel.Points.Find(p => Math.Abs(p.X - clickPoint.X) < visualizer.pointLocality && Math.Abs(p.Y - clickPoint.Y) < visualizer.pointLocality);
                if (point != null)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        this.currentFullModel.geometryModel.pairOfPoints.Add(point);
                        if (this.currentFullModel.geometryModel.pairOfPoints.Count == 1) this.addStraightLineControl.startPoint.Text = point.Id.ToString();
                        if (this.currentFullModel.geometryModel.pairOfPoints.Count == 2)
                        {
                            this.addStraightLineControl.endPoint.Text = point.Id.ToString();
                            this.addStraightLineControl.CreateStraightLine(Convert.ToInt32(this.addStraightLineControl.number.Text), this.currentFullModel.geometryModel.pairOfPoints[0], this.currentFullModel.geometryModel.pairOfPoints[1]);
                            clearSelection();
                        }
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        if (currentFullModel.geometryModel.pairOfPoints.Count > 0)
                        {
                            addStraightLineControl.startPoint.Text = "";
                            clearSelection();
                        }
                    }
                    ReDrawAll();
                }
            }

            // запоминаем выбранную точку, и если их три то рисуем дугу - режим создания дуги
            if (addArcsControl != null && addArcsControl.IsHandleCreated)
            {
                MyPoint point = currentFullModel.geometryModel.Points.Find(p => Math.Abs(p.X - clickPoint.X) < visualizer.pointLocality && Math.Abs(p.Y - clickPoint.Y) < visualizer.pointLocality);
                if (point != null)
                {

                    if (e.Button == MouseButtons.Left)
                    {
                        this.currentFullModel.geometryModel.tripleOfPoints.Add(point);
                        if (this.currentFullModel.geometryModel.tripleOfPoints.Count == 1) this.addArcsControl.start2Point.Text = point.Id.ToString();
                        if (this.currentFullModel.geometryModel.tripleOfPoints.Count == 2) this.addArcsControl.end2Point.Text = point.Id.ToString();
                        if (this.currentFullModel.geometryModel.tripleOfPoints.Count == 3) this.addArcsControl.center2Point.Text = point.Id.ToString();
                        if (this.currentFullModel.geometryModel.tripleOfPoints.Count == 3)
                        {
                            this.addArcsControl.CreateArc(Convert.ToInt32(this.addArcsControl.number.Text), this.addArcsControl.сlockwise.Checked, currentFullModel.geometryModel.tripleOfPoints[0], currentFullModel.geometryModel.tripleOfPoints[1], currentFullModel.geometryModel.tripleOfPoints[2]);
                            this.currentFullModel.geometryModel.tripleOfPoints.Clear();
                        }
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        if (currentFullModel.geometryModel.tripleOfPoints.Count == 1)
                        {
                            currentFullModel.geometryModel.tripleOfPoints.Remove(point);
                            addArcsControl.start2Point.Text = "";
                        }
                        else
                        {
                            currentFullModel.geometryModel.tripleOfPoints.Remove(point);
                            if (addArcsControl.start2Point.Text == point.Id.ToString())
                            {
                                addArcsControl.start2Point.Text = addArcsControl.end2Point.Text;
                                addArcsControl.end2Point.Text = "";
                            }
                            else
                                addArcsControl.end2Point.Text = "";
                        }
                    }
                }
            }

            // выделяем или снимаем выделение с точки
            if (this.editPointsControl != null && this.editPointsControl.IsHandleCreated)
            {
                if (currentFullModel.geometryModel.Points.Find(p => Math.Abs(p.X - clickPoint.X) < visualizer.pointLocality &&
                                                                    Math.Abs(p.Y - clickPoint.Y) < visualizer.pointLocality) == null)
                {
                    currentFullModel.geometryModel.editedPoint = null;
                    editPointsControl.number.Text = editPointsControl.x.Text = editPointsControl.y.Text = "";
                    ReDrawAll();
                }

                MyPoint point = currentFullModel.geometryModel.Points.Find(p => Math.Abs(p.X - clickPoint.X) < visualizer.pointLocality &&
                                                                    Math.Abs(p.Y - clickPoint.Y) < visualizer.pointLocality);
                if (point != null)
                {
                    if (e.Button == MouseButtons.Right) // если нажали правую кнопку мыши, то снять выделение с точки
                    {
                        if (this.currentFullModel.geometryModel.editedPoint != null && this.currentFullModel.geometryModel.editedPoint == point)
                        {
                            this.currentFullModel.geometryModel.editedPoint = null;

                            // сбрасываем id, x и y                                
                            this.editPointsControl.number.Text = "";
                            this.editPointsControl.x.Text = "";
                            this.editPointsControl.y.Text = "";
                            ReDrawAll();
                        }
                    }

                    if (e.Button == MouseButtons.Left) // если нажали левую кнопку мыши, то выделить точку
                    {
                        this.currentFullModel.geometryModel.editedPoint = point;

                        // отображаем id, x и y точки
                        this.editPointsControl.number.Text = this.currentFullModel.geometryModel.editedPoint.Id.ToString();
                        this.editPointsControl.x.Text = Mathematics.floor(this.currentFullModel.geometryModel.editedPoint.X, Mathematics.accuracy_medium).ToString();
                        this.editPointsControl.y.Text = Mathematics.floor(this.currentFullModel.geometryModel.editedPoint.Y, Mathematics.accuracy_medium).ToString();
                        ReDrawAll();
                    }
                }
            }

            if (this.editNodesControl != null && editNodesControl.IsHandleCreated)
            {
                foreach (MyArea area in this.currentFullModel.geometryModel.Areas)
                {
                    MyPoint node = area.Nodes.Find(p => Math.Abs(p.X - clickPoint.X) < visualizer.pointLocality &&
                                                                    Math.Abs(p.Y - clickPoint.Y) < visualizer.pointLocality);
                    if (node != null)
                    {
                        if (e.Button == MouseButtons.Right) // если нажали правую кнопку мыши, то снять выделение с точки
                        {
                            if (this.currentFullModel.geometryModel.editedNode != null && this.currentFullModel.geometryModel.editedNode == node)
                            {
                                this.currentFullModel.geometryModel.editedNode = null;

                                // сбрасываем id, x и y 
                                this.editNodesControl.number.Text = "";
                                this.editNodesControl.x.Text = "";
                                this.editNodesControl.y.Text = "";
                                ReDrawAll();
                            }
                        }

                        if (e.Button == MouseButtons.Left) // если нажали левую кнопку мыши, то выделить точку
                        {
                            this.currentFullModel.geometryModel.editedNode = node;

                            // отображаем id, x и y точки
                            this.editNodesControl.number.Text = this.currentFullModel.geometryModel.editedNode.Id.ToString();
                            this.editNodesControl.x.Text = Mathematics.floor(this.currentFullModel.geometryModel.editedNode.X, Mathematics.accuracy_medium).ToString();
                            this.editNodesControl.y.Text = Mathematics.floor(this.currentFullModel.geometryModel.editedNode.Y, Mathematics.accuracy_medium).ToString();
                            ReDrawAll();
                        }
                        break;
                    }
                }
            }

            //  удаляем точку
            if (this.deletePointsControl != null && this.deletePointsControl.IsHandleCreated)
            {
                MyPoint point = currentFullModel.geometryModel.Points.Find(p => Math.Abs(p.X - clickPoint.X) < visualizer.pointLocality &&
                                                                       Math.Abs(p.Y - clickPoint.Y) < visualizer.pointLocality);
                if (point != null)
                {
                    this.deletePointsControl.DeletePoint(point.Id);
                }
            }

            //  удаляем линию
            if (this.deleteLinesControl != null && this.deleteLinesControl.IsHandleCreated)
            {
                foreach (MyStraightLine sline in this.currentFullModel.geometryModel.StraightLines)
                {
                    if (pointNearLine(clickPoint, sline))
                    {
                        this.currentFullModel.geometryModel.StraightLines.Remove(sline);
                        ReDrawAll();
                        break;
                    }
                }

                foreach (MyArc arc in this.currentFullModel.geometryModel.Arcs)
                {
                    double radius = Mathematics.FindDist(arc.CenterPoint, arc.StartPoint);

                    if (pointFitsArc(clickPoint, arc, checkType.clickPrecision))
                    {
                        this.currentFullModel.geometryModel.Arcs.Remove(arc);
                        this.ReDrawAll();
                        break;
                    }
                }
            }

            //  закрепляем линию
            if (this.addBoundsControl != null && this.addBoundsControl.IsHandleCreated)
            {
                if (!this.addBoundsControl.xBound.Checked && !this.addBoundsControl.yBound.Checked) { this.addBoundsControl.errorBoundType.Visible = true; return; }
                this.addBoundsControl.errorBoundType.Visible = false;

                foreach (MyStraightLine sline in this.currentFullModel.geometryModel.StraightLines)
                {
                    if (pointNearLine(clickPoint, sline))
                    {
                        this.addBoundsControl.BoundLine(sline);
                        ReDrawAll();
                        break;
                    }
                }

                foreach (MyArc arc in this.currentFullModel.geometryModel.Arcs)
                {
                    double radius = Mathematics.FindDist(arc.CenterPoint, arc.StartPoint);

                    if (pointFitsArc(clickPoint, arc, checkType.clickPrecision))
                    {
                        this.addBoundsControl.BoundLine(arc);
                        ReDrawAll();
                        break;
                    }
                }
            } 
            // удаляем закрепление линии
            if (this.deleteBoundsControl != null && this.deleteBoundsControl.IsHandleCreated)
            {
                foreach (MyStraightLine sline in this.currentFullModel.geometryModel.StraightLines)
                {
                    if (pointNearLine(clickPoint, sline))
                    {
                        this.deleteBoundsControl.UnboundLine(sline);
                        ReDrawAll();
                        break;
                    }
                }

                foreach (MyArc arc in this.currentFullModel.geometryModel.Arcs)
                {
                    double radius = Mathematics.FindDist(arc.CenterPoint, arc.StartPoint);

                    if (pointFitsArc(clickPoint, arc, checkType.clickPrecision))
                    {
                        this.deleteBoundsControl.UnboundLine(arc);
                        ReDrawAll();
                        break;
                    }
                }
            }

            // добавляем плотность для Делоне
            if (delauneyTriangulateForm != null && this.delauneyTriangulateForm.IsHandleCreated)
                delauneyTriangulateForm.AddDensity(Mathematics.floor(mouseXcord,0.01), Mathematics.floor(mouseYcord, 0.01));
            

            if (activeForm != null)
            {
                MyLine line = null;
                bool add = false;
                MyStraightLine sline = currentFullModel.geometryModel.StraightLines.Find(l => pointNearLine(clickPoint, l));
                if (sline != null)
                {
                    line = sline;
                    if (currentFullModel.geometryModel.highlightStraightLines.IndexOf(sline) < 0)
                    {
                        add = true;
                        currentFullModel.geometryModel.highlightStraightLines.Add(sline);
                    }
                    else
                    {
                        add = false;
                        currentFullModel.geometryModel.highlightStraightLines.Remove(sline);
                    }
                    ReDrawAll();
                }
                else
                {
                    MyArc arc = currentFullModel.geometryModel.Arcs.Find(a => pointFitsArc(clickPoint, a, checkType.clickPrecision));
                    if (arc != null)
                    {
                        line = arc;
                        if (currentFullModel.geometryModel.highlightArcs.IndexOf(arc) < 0)
                        {
                            add = true;
                            currentFullModel.geometryModel.highlightArcs.Add(arc);
                        }
                        else
                        {
                            add = false;
                            currentFullModel.geometryModel.highlightArcs.Remove(arc);
                        }
                        ReDrawAll();
                    }
                }
                if (line != null)
                {
                    TextBox targetTxt;
                    if (activeForm is StdOptimisation) targetTxt = StdOptForm.notMoveLines;
                    else if (activeForm is RuppertForm) targetTxt = ruppertForm.notMoveLines;
                    else if (activeForm is Regularization) targetTxt = RegulizeForm.notMoveLines;
                    else targetTxt = forcesForm.number;
                    if (add)
                    {
                        if (targetTxt.Text == "")
                            targetTxt.Text = line.Id.ToString();
                        else
                            targetTxt.Text += "," + line.Id.ToString();
                    }
                    else
                    {
                        string[] fixNumbers = targetTxt.Text.Split(',');
                        if (fixNumbers.Length == 1) targetTxt.Text = "";
                        else
                        {
                            if (fixNumbers.Last() == line.Id.ToString())
                                targetTxt.Text = targetTxt.Text.Replace("," + line.Id.ToString(), "");
                            else
                                targetTxt.Text = targetTxt.Text.Replace(line.Id.ToString() + ",", "");
                        }
                    }
                    ReDrawAll();
                }
            }

            // создаем окружность
            if (addCirclesControl != null && addCirclesControl.IsHandleCreated)
            {
                if (this.creatingCircle) // если мы рисовали окружность, а потом кликнули мышкой, то значит надо завершить рисование  
                {
                    this.addCirclesControl.CreateCircle(this.currentFullModel.geometryModel.NumOfCircles + 1, this.currentFullModel.geometryModel.tempCircle.CenterPoint, this.currentFullModel.geometryModel.tempCircle.Radius);
                    this.creatingCircle = false;
                    this.currentFullModel.geometryModel.centerOfCircle = null;

                    this.addCirclesControl.number.Text = "";
                    this.addCirclesControl.centerPoint.Text = "";
                    this.addCirclesControl.radius.Text = "";
                    this.currentFullModel.geometryModel.tempCircle = null;
                    if (hoveredPoint != null)
                    {
                        MyCircle addedCircle = currentFullModel.geometryModel.Circles.Find(c => c.Id == currentFullModel.geometryModel.NumOfCircles);
                        splitCircleByPoint(addedCircle, hoveredPoint);
                    }
                }

                else
                {
                    MyPoint point = currentFullModel.geometryModel.Points.Find(p => Math.Abs(p.X - clickPoint.X) < visualizer.pointLocality &&
                                                                       Math.Abs(p.Y - clickPoint.Y) < visualizer.pointLocality);
                    if (point != null)
                    {
                        this.currentFullModel.geometryModel.centerOfCircle = point;
                        if (this.addCirclesControl != null)
                        {
                            this.addCirclesControl.number.Text = (this.currentFullModel.geometryModel.NumOfCircles + 1).ToString();
                            this.addCirclesControl.centerPoint.Text = point.Id.ToString();
                        }
                        this.creatingCircle = true;
                    }
                }
                ReDrawAll();
            }

            // удаляем окружнсть
            if (this.deleteCirclesControl != null && this.deleteCirclesControl.IsHandleCreated)
            {
                foreach (MyCircle circle in this.currentFullModel.geometryModel.Circles)
                {
                    // уравнение окружности
                    if (Math.Abs(Mathematics.FindDist(clickPoint, circle.CenterPoint) - circle.Radius) < visualizer.pointLocality)
                    {
                        // удаляем окружность
                        this.deleteCirclesControl.DeleteCircle(circle.Id);
                        break;
                    }
                }
            }

            if (this.deleteAreasControl != null && this.deleteAreasControl.IsHandleCreated)
            {
                foreach (MyArea area in currentFullModel.geometryModel.Areas)
                {
                    if (Mathematics.ContainsPoint(area.Nodes.ConvertAll(n => (MyNode)n).ToArray(), clickPoint))
                    {
                        deleteAreasControl.number.Text = area.Id.ToString();
                        deleteAreasControl.OK();
                        ReDrawAll();
                        return;
                    }
                }
            }
            // создаем зону
            if (this.addAreasControl != null && this.addAreasControl.IsHandleCreated)
            {
                if (addAreasControl.autoSearch.Checked == true)
                {
                    List<MyStraightLine> checkLines = new List<MyStraightLine>();
                    foreach (MyPoint point in currentFullModel.geometryModel.Points)
                        checkLines.Add(new MyStraightLine(0, clickPoint, point));

                    MyArc suspiciousArc = null;
                    List<MyPoint> zonePoints = new List<MyPoint>();
                    foreach (MyStraightLine line in checkLines)
                    {
                        List<MyArc> sarcs;
                        if (currentFullModel.geometryModel.StraightLines.Find(l => LinesCrossing(line, l)) == null)
                        {
                            sarcs = currentFullModel.geometryModel.Arcs.FindAll(a => LineCrossingArc(line, a));
                            if (sarcs.Count == 0)
                                zonePoints.Add(line.EndPoint);
                            else
                                suspiciousArc = sarcs[0];
                        }
                    }
                    if (zonePoints.Count != 4)
                    {
                        if (suspiciousArc != null)
                        {
                            if (zonePoints.IndexOf(suspiciousArc.EndPoint) == -1) zonePoints.Add(suspiciousArc.EndPoint);
                            if (zonePoints.IndexOf(suspiciousArc.StartPoint) == -1) zonePoints.Add(suspiciousArc.StartPoint);
                        }
                    }
                    List<MyStraightLine> lines = currentFullModel.geometryModel.StraightLines.FindAll(l => zonePoints.IndexOf(l.StartPoint) != -1 && zonePoints.IndexOf(l.EndPoint) != -1);
                    List<MyArc> arcs = currentFullModel.geometryModel.Arcs.FindAll(a => zonePoints.IndexOf(a.StartPoint) != -1 && zonePoints.IndexOf(a.EndPoint) != -1);
                    List<string> ids = new List<string>();
                    foreach (MyStraightLine line in lines)
                        ids.Add(line.Id.ToString());

                    foreach (MyArc arc in arcs)
                        ids.Add(arc.Id.ToString());

                    if (ids.Count < 4)
                    {
                        MessageBox.Show("Ошибка поиска зоны!");
                        return;
                    }
                    addAreasControl.line1.Text = ids[0];
                    addAreasControl.line2.Text = ids[1];
                    addAreasControl.line3.Text = ids[2];
                    addAreasControl.line4.Text = ids[3];
                    if (addAreasControl.OK()) ReDrawAll();
                }
                else
                {
                    foreach (MyStraightLine sline in this.currentFullModel.geometryModel.StraightLines)
                    {
                        // уравнение прямой, проходящей через две точки
                        if (pointNearLine(clickPoint, sline))
                        {
                            if (this.currentFullModel.geometryModel.numOfSelectedLines == 0) { this.addAreasControl.line1.Text = sline.Id.ToString(); currentFullModel.geometryModel.numOfSelectedLines++; this.currentFullModel.geometryModel.highlightStraightLines.Add(sline); ReDrawAll(); break; }
                            if (this.currentFullModel.geometryModel.numOfSelectedLines == 1) { this.addAreasControl.line2.Text = sline.Id.ToString(); currentFullModel.geometryModel.numOfSelectedLines++; this.currentFullModel.geometryModel.highlightStraightLines.Add(sline); ReDrawAll(); break; }
                            if (this.currentFullModel.geometryModel.numOfSelectedLines == 2) { this.addAreasControl.line3.Text = sline.Id.ToString(); currentFullModel.geometryModel.numOfSelectedLines++; this.currentFullModel.geometryModel.highlightStraightLines.Add(sline); ReDrawAll(); break; }
                            if (this.currentFullModel.geometryModel.numOfSelectedLines == 3) { this.addAreasControl.line4.Text = sline.Id.ToString(); currentFullModel.geometryModel.numOfSelectedLines++; this.currentFullModel.geometryModel.highlightStraightLines.Add(sline); ReDrawAll(); }

                            if (this.currentFullModel.geometryModel.numOfSelectedLines == 4)
                            {
                                this.addAreasControl.OK(); // вызываем функцию, как будто была нажата кнопка ОК. эта функция обрабатывает значения линий в текстовых полях контрола.
                                ReDrawAll();
                            }
                            break;
                        }
                    }

                    foreach (MyArc arc in this.currentFullModel.geometryModel.Arcs)
                    {
                        double radius = Mathematics.FindDist(arc.CenterPoint, arc.StartPoint);

                        if (pointFitsArc(clickPoint, arc, checkType.clickPrecision))
                        {
                            if (this.currentFullModel.geometryModel.numOfSelectedLines == 0) { this.addAreasControl.line1.Text = arc.Id.ToString(); currentFullModel.geometryModel.numOfSelectedLines++; this.currentFullModel.geometryModel.highlightArcs.Add(arc); ReDrawAll(); break; }
                            if (this.currentFullModel.geometryModel.numOfSelectedLines == 1) { this.addAreasControl.line2.Text = arc.Id.ToString(); currentFullModel.geometryModel.numOfSelectedLines++; this.currentFullModel.geometryModel.highlightArcs.Add(arc); ReDrawAll(); break; }
                            if (this.currentFullModel.geometryModel.numOfSelectedLines == 2) { this.addAreasControl.line3.Text = arc.Id.ToString(); currentFullModel.geometryModel.numOfSelectedLines++; this.currentFullModel.geometryModel.highlightArcs.Add(arc); ReDrawAll(); break; }
                            if (this.currentFullModel.geometryModel.numOfSelectedLines == 3) { this.addAreasControl.line4.Text = arc.Id.ToString(); currentFullModel.geometryModel.numOfSelectedLines++; this.currentFullModel.geometryModel.highlightArcs.Add(arc); ReDrawAll(); }

                            if (this.currentFullModel.geometryModel.numOfSelectedLines == 4)
                            {
                                this.addAreasControl.OK(); // вызываем функцию, как будто была нажата кнопка ОК. эта функция обрабатывает значения линий в текстовых полях контрола.
                                ReDrawAll();
                            }
                            break;
                        }
                    }
                }
            }
        }
        private void constructionArea_Load(object sender, EventArgs e)
        {
            visualizer = new Visualizer(constructionArea);
        }

        private void constructionArea_Resize(object sender, EventArgs e)
        {
            if (visualizer != null)
            {
                visualizer.RefreshBorders();
                visualizer.DrawRecord(showGrid.Checked);
            }
        }
    }
}