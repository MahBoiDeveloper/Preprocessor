using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.Platform.Windows;
using ModelComponents;
using PreprocessorUtils;

namespace PreprocessorLib
{
    public partial class ProjectForm : Form
    {
        public double startPrecision = 0.01; // начальная точность сравнения вещественных чисел (применяется для проверки координат узлов КЭ)
        public double precision = 0.01;      // точность сравнения вещественных чисел - должна зависеть от размеров пластины и применяется для проверки координат узлов КЭ.  
        
        int middleValueOfScale { get { return (scale.Maximum + scale.Minimum) / 2; } }
        // эти переменные нужны для разных вариантов работы pointFitsArc. checkType.clickPrecision позволит сравнивать расстояние от центра
        // дуги до точки с большой погрешностью (с учетом того, что пользователь кликает мышкой). Второй вариант - для точных вычислений
        public enum checkType : byte
        {
            clickPrecision = 0,
            doublePrecision = 1,
            boundPrecision = 2
        }

        bool mouseAboveEditedPoint = false; // мышка над выделенной точкой
        bool mouseAboveEditedNode = false; // мышка над выделенным узлом зоны
        public bool creatingCircle = false; // индикатор того, что мы рисуем окружность


        UserControl activeControl; // актинвый контрол
        public Form activeForm = null;
        
        public bool gridOptimization = false;
        const double startPixelPerUnit = 50;
        const double startPixelPerNewton = 0.01;
        const int arrowSize = 10;
        const float lineWidth = 2F;

        double pixelPerUnit;
        public double PixelPerUnit
        {
            get { return pixelPerUnit; }
            set { pixelPerUnit = value; }
        }

        double pixelPerNewton;
        public double PixelPerNewton
        {
            get { return pixelPerNewton; }
            set { pixelPerNewton = value; }
        }

        bool mouseMoved = false; // мышь перемещали нажатой

        bool mouseAboveArea = false;

        public double DefinePrecision()
        {
            return 0.01;
        }

        #region Методы Draw* и Redraw*

        public void DrawFE(Color color)
        {
            // if (!this.showOnlyFE.Checked) return;
            int currentModel = this.GetCurrentModelIndex();
            bool showNodesNumber = showFENodes.Checked;
            visualizer.DrawFE(currentFullModel.FiniteElementModels[currentModel], color, showFENumbers.Checked, showFENodes.Checked, showFEMaterials.Checked);
        }

        public void DrawFEBounds(Color color)
        {
            if (!this.showBounds.Checked) return;
            if (this.currentFullModel.FiniteElementModels.Count == 0) return;
            int currentModel = this.GetCurrentModelIndex();
            visualizer.DrawBounds(currentFullModel.FiniteElementModels[currentModel].Nodes.FindAll(n => n.BoundType != 0).ToArray(), color);
        }

        public void DrawForces(Color color)
        {
            if (!this.showForces.Checked) return;
            if (this.currentFullModel.FiniteElementModels.Count == 0) return;
            int currentModel = this.GetCurrentModelIndex();
            visualizer.DrawForces(currentFullModel.FiniteElementModels[currentModel], pixelPerNewton, color, showForceValue.Checked);
        }

        public void DrawModel() // перерисовка всей модели - всех точек, линий, зон, КЭ, закреплений и т.д. 
        {
            int currentModel = this.GetCurrentModelIndex();

            if (this.activeControl is GridAnalysis && showOnlyFE.Checked)
                visualizer.DrawBadElements(currentFullModel.FiniteElementModels[currentModel].FiniteElements.ToArray(), gridAnalysisControl.MinAllowAngle, gridAnalysisControl.MaxAllowAngle, gridAnalysisControl.MinAllowSquare, gridAnalysisControl.MaxSquare, gridAnalysisControl.ColorGradationType);

            if (this.showPoints.Checked)
            {
                // отрисовка точек - граничных узлов
                if (currentFullModel.geometryModel.Points != null)
                    visualizer.DrawPointsArray(currentFullModel.geometryModel.Points.ToArray(), Color.Blue);

            }
            if (this.showLines.Checked)
            {
                // Отрисовка всех линий
                if (currentFullModel.geometryModel.StraightLines != null)
                    visualizer.DrawLinesArray(currentFullModel.geometryModel.StraightLines.ToArray(), Color.Blue, true, false, 2.0);

            }

            if (this.showCircles.Checked)
            {

                // отрисовка всех окружностей
                if (currentFullModel.geometryModel.Circles != null)
                    foreach (MyCircle circle in currentFullModel.geometryModel.Circles)
                        visualizer.DrawCircle(circle, Color.Blue);
            }

            if (this.showArcs.Checked)
            {
                // отрисовка всех дуг
                if (currentFullModel.geometryModel.Arcs != null)
                    foreach (MyArc arc in currentFullModel.geometryModel.Arcs)
                        visualizer.DrawArc(arc, Color.Blue);
            }

            // отрисовка всех зон
            if (this.showOnlyAreas.Checked)
                if (currentFullModel.geometryModel.Areas != null)
                    visualizer.DrawAreas(currentFullModel.geometryModel.Areas.ToArray(), Color.Green);
            // отрисовка КЭ
            if (this.showOnlyFE.Checked)
            {
                if (currentFullModel.FiniteElementModels.Count != 0)
                {
                    DrawFE(Color.Brown);
                }
            }

            if (showPoints.Checked)
            {
                // отрисовка точек, выделенных красным при создании линии с помощью мышки - если таковые конечно имеются)
                if (this.currentFullModel.geometryModel.pairOfPoints.Count != 0)
                    visualizer.DrawPointsArray(currentFullModel.geometryModel.pairOfPoints.ToArray(), Color.Red);

                // отрисовка точек, выделенных красным при создании дуги с помощью мышки - если таковые конечно имеются)
                if (this.currentFullModel.geometryModel.tripleOfPoints.Count != 0)
                    visualizer.DrawPointsArray(currentFullModel.geometryModel.tripleOfPoints.ToArray(), Color.Red);

                if (hoveredPoint != null)
                    if (currentFullModel.geometryModel.Points.IndexOf(hoveredPoint) != -1)
                    {
                        MyPoint[] p = { hoveredPoint };
                        visualizer.DrawPointsArray(p, Color.Yellow);
                    }

                // отрисовка перемещаемой точки..
                if (this.currentFullModel.geometryModel.editedPoint != null)
                {
                    MyPoint[] p = { currentFullModel.geometryModel.editedPoint };
                    visualizer.DrawPointsArray(p, Color.Red);
                }
            }

            if (showForces.Checked) this.DrawForces(Color.Purple);
            if (showBounds.Checked) this.DrawFEBounds(Color.Brown);

            if (showLines.Checked)
                // Отрисовка всех выделенных цветом линий
                if (this.currentFullModel.geometryModel.highlightStraightLines != null)
                    visualizer.DrawLinesArray(currentFullModel.geometryModel.highlightStraightLines.ToArray(), Color.Red, true);

            if (showArcs.Checked)
                // отрисовка всех подсвеченных цветом дуг
                if (this.currentFullModel.geometryModel.highlightArcs != null)
                    foreach (MyArc arc in this.currentFullModel.geometryModel.highlightArcs)
                        visualizer.DrawArc(arc, Color.Red);

            if (showCircles.Checked)
                // отрисовка рисуемой окружности
                if (this.currentFullModel.geometryModel.tempCircle != null) visualizer.DrawCircle(currentFullModel.geometryModel.tempCircle, Color.Red);

            if (this.delauneyTriangulateForm != null && this.delauneyTriangulateForm.IsHandleCreated)
                if (currentFullModel.densityPoints.Count > 0)
                    visualizer.DrawDensityPoints(currentFullModel.densityPoints.ToArray(), Color.Purple);
        }

        public void ReDrawAll(bool ignoreChanges = false)
        {
            if (visualizer != null)
            {
                visualizer.DropArrays();
                DrawModel();
                visualizer.DrawRecord(showGrid.Checked);
                if (!ignoreChanges) UndoManager.CheckForChanges();
            }
        }

        public void ChangeScale(bool ignoreMouse = false)
        {
            int val = scale.Value;
            double k;
            double px = 0, py = 0;
            PointF p = visualizer.GetCenter();
            double cx = p.X;
            double cy = p.Y;
            double proportion;

            if (!ignoreMouse)
            {
                if (!double.TryParse(textBox1.Text, out px) || !double.TryParse(textBox2.Text, out py))
                    return;
            }
            if (val > middleValueOfScale)
            {
                k = (val - middleValueOfScale + 10)*0.1;
                this.scaleField.Text = k.ToString() + ":1";
                proportion = visualizer.SetScale(1.0f / (float)k);
            }
            else if (val < middleValueOfScale)
            {
                k = (middleValueOfScale - val + 10) * 0.1;
                this.scaleField.Text = "1:" + k.ToString();
                proportion = visualizer.SetScale((float)k);
            }
            else
            {
                k = 1;
                scaleField.Text = "1:1";
                proportion = visualizer.SetScale((float)k);
            }
            if (!ignoreMouse)
            {
                if (mouseAboveArea)
                {
                    cx = (px - (px - cx) / proportion);
                    cy = (py - (py - cy) / proportion);
                    visualizer.MoveTo(cx, cy);
                }
            }

            ReDrawAll(true);
        }

        public void clearSelection()
        {
            currentFullModel.geometryModel.pairOfPoints.Clear();
            currentFullModel.geometryModel.tripleOfPoints.Clear();
            currentFullModel.geometryModel.highlightArcs.Clear();
            currentFullModel.geometryModel.highlightStraightLines.Clear();
            currentFullModel.geometryModel.numOfSelectedLines = 0;
        }

        public void DeSelectPoint()
        {
            if (this.currentFullModel.geometryModel.editedPoint != null)
            {
                this.currentFullModel.geometryModel.editedPoint = null;
                ReDrawAll();
            }
        }

        public void CenterModel() // функция центрирования модели
        {
            if (this.currentFullModel.geometryModel.Points.Count != 0) // сюда заходим, если у нас есть какая-либо модель..
            {
                double minX, maxX, minY, maxY;

                minX = currentFullModel.geometryModel.Points.Min(p => p.X);
                minY = currentFullModel.geometryModel.Points.Min(p => p.Y);
                maxX = currentFullModel.geometryModel.Points.Max(p => p.X);
                maxY = currentFullModel.geometryModel.Points.Max(p => p.Y);

                float k = visualizer.ScaleToFit((maxX - minX), maxY - minY);
                int val;
                if (k < 1.0f)
                {
                    k = 1.0f / k;
                    val = (int)Mathematics.floor(k * 10 + middleValueOfScale - 10, 1.0) - 1;
                }
                else
                    val = (int)Mathematics.floor(middleValueOfScale - k * 10 + 10, 1.0) - 1;
                if (val > scale.Maximum) val = scale.Maximum;
                else if (val < scale.Minimum) val = scale.Minimum;
                scale.Value = val;
                ChangeScale(true);
                visualizer.MoveTo((maxX + minX) / 2, (maxY + minY) / 2);
            }
        }


        #endregion

        #region Геометрические проверки

        private bool LinesCrossing(MyStraightLine l1, MyStraightLine l2)
        {
            double k1 = 0, b1 = 0;
            double k2 = 0, b2 = 0;
            double commonX, commonY;

            if (Math.Abs(l1.EndPoint.X - l1.StartPoint.X) < 0.01)
            {
                if (Math.Abs(l2.EndPoint.X - l2.StartPoint.X) < 0.01)
                    // параллельные прямые все ещё не пересекаются
                    return false;
                else
                {
                    k2 = (double)(l2.EndPoint.Y - l2.StartPoint.Y) / (l2.EndPoint.X - l2.StartPoint.X);
                    b2 = (double)(l2.EndPoint.Y - k2 * l2.EndPoint.X);
                    commonX = l1.StartPoint.X;
                    commonY = commonX * k2 + b2;
                }
            }
            else
            {
                if (Math.Abs(l2.EndPoint.X - l2.StartPoint.X) < 0.01)
                {
                    k1 = (l1.EndPoint.Y - l1.StartPoint.Y) / (l1.EndPoint.X - l1.StartPoint.X);
                    b1 = (l1.EndPoint.Y - k1 * l1.EndPoint.X);
                    commonX = l2.EndPoint.X;
                    commonY = commonX * k1 + b1;
                }
                else
                {
                    k1 = (double)(l1.EndPoint.Y - l1.StartPoint.Y) / (l1.EndPoint.X - l1.StartPoint.X);
                    k2 = (double)(l2.EndPoint.Y - l2.StartPoint.Y) / (l2.EndPoint.X - l2.StartPoint.X);
                    b2 = (double)(l2.EndPoint.Y - k2 * l2.EndPoint.X);
                    b1 = (double)(l1.EndPoint.Y - k1 * l1.EndPoint.X);
                    commonX = (b2 - b1) / (k1 - k2);
                    commonY = commonX * k2 + b2;
                }
            }
            MyPoint commonPoint = new MyPoint(commonX, commonY, MyPoint.PointType.IsGeometryPoint);
            return Mathematics.pointOnLine(commonX, commonY, l1) && Mathematics.pointOnLine(commonX, commonY, l2) && Mathematics.FindDist(commonPoint, l1.EndPoint) > 0.01;
        }

        private bool LineCrossingArc(MyStraightLine line, MyArc arc)
        {
            double r, a, b, c;
            if (Math.Abs(line.StartPoint.X - line.EndPoint.X) < 0.01)
            { b = 0; a = 1; c = -arc.CenterPoint.X; }
            else if (Math.Abs(line.StartPoint.Y - line.EndPoint.Y) < 0.01)
            { b = 1; a = 0; c = -arc.CenterPoint.Y; }
            else
            {
                double k, bx;
                k = (line.EndPoint.Y - line.StartPoint.Y) / (line.EndPoint.X - line.StartPoint.X);
                bx = line.EndPoint.Y - k * line.EndPoint.X;
                a = -k;
                c = -(bx + k * arc.CenterPoint.X - arc.CenterPoint.Y);
                b = 1;
            }
            r = Mathematics.FindDist(arc.StartPoint, arc.CenterPoint);
            double x0 = -a * c / (a * a + b * b), y0 = -b * c / (a * a + b * b);
            if (c * c > r * r * (a * a + b * b) + 0.01)
                return false;
            else if (Math.Abs(c * c - r * r * (a * a + b * b)) < 0.01)
            {
                MyPoint xPoint = new MyPoint(x0, y0, MyPoint.PointType.IsGeometryPoint);
                return pointFitsArc(xPoint, arc, checkType.doublePrecision) && Mathematics.FindDist(xPoint, line.EndPoint) > 0.01 && Mathematics.pointOnLine(xPoint, line);
            }
            else
            {
                double d = r * r - c * c / (a * a + b * b);
                double mult = Math.Sqrt(d / (a * a + b * b));
                double ax, ay, bx, by;
                ax = arc.CenterPoint.X + x0 + b * mult;
                bx = arc.CenterPoint.X + x0 - b * mult;
                ay = arc.CenterPoint.Y + y0 - a * mult;
                by = arc.CenterPoint.Y + y0 + a * mult;
                MyPoint xPoint1 = new MyPoint(ax, ay, MyPoint.PointType.IsGeometryPoint);
                MyPoint xPoint2 = new MyPoint(bx, by, MyPoint.PointType.IsGeometryPoint);
                return (pointFitsArc(xPoint1, arc, checkType.doublePrecision) && Mathematics.FindDist(xPoint1, line.EndPoint) > 0.01 && Mathematics.pointOnLine(xPoint1, line))
                    || (pointFitsArc(xPoint2, arc, checkType.doublePrecision) && Mathematics.FindDist(xPoint2, line.EndPoint) > 0.01 && Mathematics.pointOnLine(xPoint2, line));
            }
        }

        public bool pointNearLine(double x, double y, MyStraightLine sline)
        {
            return pointNearLine(new MyPoint(x, y, MyPoint.PointType.IsGeometryPoint), sline);
        }

        public bool pointNearLine(MyPoint point, MyStraightLine sline)
        {
            MyPoint comPoint = crossPoint(point, sline);
            if (Mathematics.FindDist(point, comPoint) < visualizer.pointLocality)
                if (comPoint.X <= Math.Max(sline.StartPoint.X, sline.EndPoint.X) && comPoint.X >= Math.Min(sline.StartPoint.X, sline.EndPoint.X) &&
                    comPoint.Y <= Math.Max(sline.StartPoint.Y, sline.EndPoint.Y) && comPoint.Y >= Math.Min(sline.StartPoint.Y, sline.EndPoint.Y)) // условие, гарантирующее, что точка от клика мышки лежит на отрезке, а не на прямой
                    return true;

            return false;
        }

        public bool TestArc(MyArc testingArc)
        {
            bool goodArc = false;

            // проверяем расстояния от центральной точки до двух крайних
            double r1 = Mathematics.FindDist(testingArc.CenterPoint, testingArc.StartPoint);
            double r2 = Mathematics.FindDist(testingArc.CenterPoint, testingArc.EndPoint);

            if (Math.Abs(r1 - r2) > 0.1) return false; // если радиусы не равны, то дугу построить невозможно

            double startAngle = (180.0 / Math.PI) * ProjectForm.ArcAngle(testingArc, testingArc.StartPoint); // вычисляем углы начальной и конечной точек дуги - меряются они от оси Х против часовой стрелки (в градусах)
            double endAngle = (180.0 / Math.PI) * ProjectForm.ArcAngle(testingArc, testingArc.EndPoint);

            double sweepAngle = 0; // угол дуги
            if (testingArc.Clockwise && startAngle > endAngle) sweepAngle = (startAngle - endAngle);
            if (testingArc.Clockwise && startAngle < endAngle)
                sweepAngle = 360.0 - (endAngle - startAngle);
            if (!testingArc.Clockwise && startAngle > endAngle) sweepAngle = 360.0 - (startAngle - endAngle);
            if (!testingArc.Clockwise && startAngle < endAngle) sweepAngle = (endAngle - startAngle);

            if (sweepAngle > 0.0 && sweepAngle <= 180.9)
            {
                if (testingArc.Clockwise)
                {
                    goodArc = true;
                }
                else
                {
                    goodArc = true;
                }
            }
            if (sweepAngle < 0.0 && sweepAngle >= -180.9)
            {
                if (testingArc.Clockwise)
                {
                    goodArc = true;
                }
                else
                {
                    goodArc = true;
                }
            }
            return goodArc;
        }



        public bool pointFitsArc(double x, double y, MyArc arc, checkType comparePrecision)
        {
            return pointFitsArc(new MyPoint(x, y, MyPoint.PointType.IsGeometryPoint), arc, comparePrecision);
        }

        void splitCircleByPoint(MyCircle circle, MyPoint point)
        {
            double x = point.X;
            double y = point.Y;
            double x1 = (-1) * (x - circle.CenterPoint.X) + circle.CenterPoint.X;
            double y1 = (-1) * (y - circle.CenterPoint.Y) + circle.CenterPoint.Y;
            if (addPointControl == null)
            {
                addPointControl = new AddPointsControl(this);
                this.Controls.Add(addPointControl);
            }
            MyPoint newAutoPoint = this.addPointControl.CreatePoint(this.currentFullModel.geometryModel.NumOfPoints + 1, x1, y1); // создаем автоточку, диаметрально противоположную нашей новой точке

            // созаем и рисуем первую дугу
            MyArc newArc = new MyArc(this.currentFullModel.geometryModel.NumOfLines + 1, true, point, newAutoPoint, circle.CenterPoint);

            this.currentFullModel.geometryModel.Arcs.Add(newArc);
            this.currentFullModel.geometryModel.NumOfLines++;

            // создаем и рисуем вторую дугу
            newArc = new MyArc(this.currentFullModel.geometryModel.NumOfLines + 1, true, newAutoPoint, point, circle.CenterPoint);

            this.currentFullModel.geometryModel.Arcs.Add(newArc);
            this.currentFullModel.geometryModel.NumOfLines++;

            // удаляем окружность
            circle.CenterPoint.CircleNumbers.Remove(circle.Id);
            this.currentFullModel.geometryModel.Circles.Remove(circle);
            this.ReDrawAll();
            if (addPointControl != null)
                disposeControl(addPointControl);
        }

        public bool pointFitsArc(MyPoint point, MyArc arc, checkType comparePrecision)
        {
            double radius = Mathematics.FindDist(arc.CenterPoint, arc.StartPoint);
            bool pointFits = false;
            switch (comparePrecision)
            {
                case checkType.clickPrecision:
                    pointFits = Math.Abs(Mathematics.FindDist(point, arc.CenterPoint) - radius) < visualizer.pointLocality / 2.0;
                    break;
                case checkType.doublePrecision:
                    pointFits = Math.Abs(Mathematics.FindDist(point, arc.CenterPoint) - radius) < 0.000000001;
                    break;
                case checkType.boundPrecision:
                    pointFits = Math.Abs(Mathematics.FindDist(point, arc.CenterPoint) - radius) < radius * ProjectForm.toch;
                    break;
            }

            if (pointFits)
                return Mathematics.checkValidPointOnArc(point, arc);

            return false;
        }

        

        private MyPoint crossPoint(MyPoint point, MyStraightLine sline)
        {
            double commonX, commonY;
            if (Math.Abs(sline.EndPoint.X - sline.StartPoint.X) < 0.01)
            {
                commonX = sline.StartPoint.X;
                commonY = point.Y;
            }
            else if (Math.Abs(sline.EndPoint.Y - sline.StartPoint.Y) < 0.01)
            {
                commonX = point.X;
                commonY = sline.StartPoint.Y;
            }
            else
            {
                double k1, b1;
                double k2, b2;

                // Находим точку пересечения прямой и перпендикуляра, опущенного на эту прямую из точки point
                k2 = (double)(sline.EndPoint.Y - sline.StartPoint.Y) / (sline.EndPoint.X - sline.StartPoint.X);
                k1 = Math.Tan(Math.Atan(k2) + Math.PI / 2.0);
                b2 = (double)sline.EndPoint.Y - k2 * sline.EndPoint.X;
                b1 = point.Y - k1 * point.X;

                commonX = (b2 - b1) / (k1 - k2);
                commonY = commonX * k2 + b2;
            }
            return new MyPoint(commonX, commonY, MyPoint.PointType.IsGeometryPoint);
        }

        public void TestOnLineCircleAndArc(ref MyPoint newPoint)
        {
            double x = newPoint.X;
            double y = newPoint.Y;
            // случай, если точка оказалась на линии - надо линию разбить этой точкой на две линии
            foreach (MyStraightLine sline in this.currentFullModel.geometryModel.StraightLines)
            {
                if (pointNearLine(x, y, sline))
                {
                    bool removeControl = false;
                    if (addStraightLineControl == null)
                    {
                        addStraightLineControl = new AddStraightLinesControl(this);
                        removeControl = true;
                    }
                    MyPoint xPoint = crossPoint(newPoint, sline);
                    newPoint.X = xPoint.X;
                    newPoint.Y = xPoint.Y;
                    sline.StartPoint.LineNumbers.Remove(sline.Id);
                    sline.EndPoint.LineNumbers.Remove(sline.Id);
                    this.addStraightLineControl.CreateStraightLine(this.currentFullModel.geometryModel.NumOfLines + 1, sline.StartPoint, newPoint);
                    this.addStraightLineControl.CreateStraightLine(this.currentFullModel.geometryModel.NumOfLines + 1, newPoint, sline.EndPoint);
                    this.currentFullModel.geometryModel.StraightLines.Remove(sline);
                    if (removeControl)
                        disposeControl(addStraightLineControl);
                    break;
                }
            }

            // случай, если точка оказалась на окружности, то надо этой точкой разбить окружность на две дуги, + создать вторую точку автоматом
            foreach (MyCircle circle in this.currentFullModel.geometryModel.Circles)
            {
                // уравнение окружности
                if (Math.Abs(Mathematics.FindDist(circle.CenterPoint, newPoint) - circle.Radius) < visualizer.pointLocality)
                {
                    splitCircleByPoint(circle, newPoint);
                    return;
                }
            }

            // случай, если точка оказалась на дуге - надо дугу разбить на две дуги...
            foreach (MyArc arc in this.currentFullModel.geometryModel.Arcs)
            {
                // Потенциально опасный участок :)
                if (pointFitsArc(newPoint, arc, checkType.clickPrecision))
                {
                    double k = Mathematics.FindDist(arc.EndPoint, arc.CenterPoint) / Mathematics.FindDist(newPoint, arc.CenterPoint);
                    newPoint.X = arc.CenterPoint.X + (newPoint.X - arc.CenterPoint.X) * k;
                    newPoint.Y = arc.CenterPoint.Y + (newPoint.Y - arc.CenterPoint.Y) * k;
                    // созаем и рисуем первую дугу
                    MyArc newArc = new MyArc(this.currentFullModel.geometryModel.NumOfLines + 1, arc.Clockwise, arc.StartPoint, newPoint, arc.CenterPoint);
                    this.currentFullModel.geometryModel.Arcs.Add(newArc);
                    this.currentFullModel.geometryModel.NumOfLines++;
                    // создаем и рисуем вторую дугу

                    newArc = new MyArc(this.currentFullModel.geometryModel.NumOfLines + 1, arc.Clockwise, newPoint, arc.EndPoint, arc.CenterPoint);
                    this.currentFullModel.geometryModel.Arcs.Add(newArc);
                    this.currentFullModel.geometryModel.NumOfLines++;
                    // удаляем дугу
                    arc.StartPoint.LineNumbers.Remove(arc.Id);
                    arc.EndPoint.LineNumbers.Remove(arc.Id);
                    arc.CenterPoint.LineNumbers.Remove(arc.Id);
                    this.currentFullModel.geometryModel.Arcs.Remove(arc);
                    break;
                }
            }
            ReDrawAll();
        }

        static public double ArcAngle(MyArc arc, MyPoint point) // функция вычисляет угол между осью Х и прямой проходящей через центр дуги и точку на дуге. точка должна лежать на дуге
        {
            double angle = 0;
            MyPoint centerPoint = arc.CenterPoint;
            angle = Math.Atan2(point.Y - centerPoint.Y, point.X - centerPoint.X);
            return angle;
        }

        #endregion
    }
}