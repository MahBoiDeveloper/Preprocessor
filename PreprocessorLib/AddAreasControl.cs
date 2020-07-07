using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ModelComponents;
using PreprocessorUtils;

namespace PreprocessorLib
{
    public partial class AddAreasControl : UserControl
    {
        ProjectForm parent;
        public AddAreasControl()
        {
            InitializeComponent();
        }
        public AddAreasControl(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            number.ReadOnly = true;
            number.BackColor = SystemColors.Window;
            number.Text = (parent.currentFullModel.geometryModel.NumOfAreas + 1).ToString();
        }

        public void SetCursor()
        {
            line1.Select();
        }

        public bool OK(List<MyPoint> formerNodes = null)
        {
            MyPoint point = null;
            bool success = false;
            int[] lineID = new int[4];
            bool[] lineExists = { false, false, false, false };

            int numOfArcs = 0;
            parent.clearSelection();

            if (number.TextLength == 0 || line1.TextLength == 0 || line2.TextLength == 0 || line3.TextLength == 0 || line4.TextLength == 0)
            {
                errorMessage1.Visible = true;
                return success;
            }
            if (errorMessage2.Visible)
            {
                return success;
            }
            lineID[0] = Convert.ToInt32(line1.Text);
            lineID[1] = Convert.ToInt32(line2.Text);
            lineID[2] = Convert.ToInt32(line3.Text);
            lineID[3] = Convert.ToInt32(line4.Text);
            if (lineID.Distinct().ToArray().Length != 4)
            {
                MessageBox.Show("Зона не может содержать повторяющиеся линии!");
                return success;
            }

            List<MyPoint> points = new List<MyPoint>();
            List<MyStraightLine> slines = new List<MyStraightLine>();
            List<MyArc> arcs = new List<MyArc>();
            List<MyLine> lines = new List<MyLine>();

            // ищем линии
            for (int i = 0; i < 4; i++)
            {
                MyLine currentLine;
                MyStraightLine line = parent.currentFullModel.geometryModel.StraightLines.Find(ln => ln.Id == lineID[i]);
                MyArc arc = parent.currentFullModel.geometryModel.Arcs.Find(a => a.Id == lineID[i]);
                if (line == null) currentLine = arc;
                else currentLine = line;
                if (currentLine != null)
                {
                    lineExists[i] = true;
                    bool neighborLineExists = false;
                    // цикл по всем зонам
                    MyArea area;
                    if (line == null) area = parent.currentFullModel.geometryModel.Areas.Find(a => a.Arcs.IndexOf(arc) != -1);
                    else area = parent.currentFullModel.geometryModel.Areas.Find(a => a.StraightLines.IndexOf(line) != -1);
                    if (area != null)
                    {
                        if (points.IndexOf(currentLine.EndPoint) == -1) points.Add(currentLine.EndPoint);
                        if (points.IndexOf(currentLine.StartPoint) == -1) points.Add(currentLine.StartPoint);
                        neighborLineExists = true;
                    }

                    if (!neighborLineExists)
                    {
                        if (points.IndexOf(currentLine.StartPoint) == -1) points.Add(currentLine.StartPoint);
                        if (points.IndexOf(currentLine.EndPoint) == -1) points.Add(currentLine.EndPoint);
                    }
                    lineExists[i] = true;
                    if (line == null)
                    {
                        arcs.Add(arc);
                        numOfArcs++;
                    }
                    else slines.Add(line);
                    lines.Add(currentLine);
                }
            }

            // закончили поиск линий

            if (!lineExists.Contains(false))
            {
                // указанные линии существуют, но надо еще проверить, образуют ли они замкнутую зону
                errorMessage4.Visible = false;
                errorAreaExists.Visible = false;
                if (points.Count == 4) // условие, определяющее то, что линии образуют замкнутую область
                {
                    errorMessage3.Visible = false;
                    // проверяем число зон, в которым принадлежать линии
                    foreach (MyLine l in lines)
                    {
                        if (l.Areas.Count == 2)
                        {
                            clearLines();
                            errorTwoAreas.Visible = true;
                            return success;
                        }
                    }

                    // проверяем, нет ли такой зоны уже
                    if (lines.Find(l => l.Areas.Count != 1) == null) //lines[0].Areas.Count == 1 && lines[1].Areas.Count == 1 && lines[2].Areas.Count == 1 && lines[3].Areas.Count == 1)
                    {
                        if (lines.ConvertAll(l => parent.currentFullModel.geometryModel.Areas.Find(ar => ar.Id == l.Areas.First()).Id).Distinct().Count() == 1)
                        {
                            errorAreaExists.Visible = true;
                            clearLines();
                            return success;
                        }
                    }

                    // определяем первую линию в списке линий
                    int firstLine = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (lines[i].Areas.Count == 1)
                        {
                            firstLine = i;
                            break;
                        }
                    }
                    if (firstLine != 0)
                    {
                        MyLine tempLine = lines[0];
                        lines[0] = lines[firstLine];
                        lines[firstLine] = tempLine;
                    }

                    // определяем вторую линию в списке линий
                    int secondLine = 1;
                    point = lines[0].EndPoint;
                    for (int i = 1; i < 4; i++)
                    {
                        if (point == lines[i].EndPoint || point == lines[i].StartPoint)
                        {
                            secondLine = i;
                            break;
                        }
                    }

                    if (secondLine != 1)
                    {
                        MyLine tempLine = lines[1];
                        lines[1] = lines[secondLine];
                        lines[secondLine] = tempLine;
                    }

                    // определяем четвертую (последниюю) линию в списке линий. определения третий линии нет, и это не ошибка. она определится автоматом
                    int lastLine = 3;
                    point = lines[0].StartPoint;
                    for (int i = 2; i < 4; i++)
                    {
                        if (point == lines[i].EndPoint || point == lines[i].StartPoint)
                        {
                            lastLine = i;
                            break;
                        }
                    }
                    if (lastLine != 3)
                    {
                        MyLine tempLine = lines[3];
                        lines[3] = lines[lastLine];
                        lines[lastLine] = tempLine;
                    }

                    points.Clear();
                    int newAreaId;
                    if (!int.TryParse(number.Text, out newAreaId))
                    {
                        errorMessage2.Visible = true;
                        number.Focus();
                        return success;
                    }

                    if (parent.currentFullModel.geometryModel.Areas.Count > 0)
                    {
                        if (lines.All(l => l.Areas.Count == 0))
                        {
                            MessageBox.Show("Невозможно образовать зону: не прилегает ни к одной из существующих зон");
                            return success;
                        }
                    }

                    points.Add(lines[0].StartPoint);
                    points.Add(lines[0].EndPoint);
                    if (lines[1].StartPoint != points[1]) points.Add(lines[1].StartPoint);
                    else points.Add(lines[1].EndPoint);

                    if (lines[3].StartPoint != points[0]) points.Add(lines[3].StartPoint);
                    else points.Add(lines[3].EndPoint);

                    double S = Square(points[0], points[1]) + Square(points[1], points[2]) + Square(points[2], points[3]) + Square(points[3], points[0]);

                    if (S > 0.0)
                    {
                        MyPoint tempPoint = points[2];
                        points[2] = points[3];
                        points[3] = tempPoint;

                        tempPoint = points[0];
                        points[0] = points[1];
                        points[1] = tempPoint;

                        MyLine tempLine = lines[1];
                        lines[1] = lines[3];
                        lines[3] = tempLine;
                    }

                    MyPoint[] middlePoints = new MyPoint[4];
                    for (int i = 0; i < 4; i++)
                    {
                        middlePoints[i] = new MyPoint(0, 0, 0);
                    }
                    // если импортируем геометрию, возможно узлы уже двигали
                    // считываем инфу оттуда
                    if (formerNodes != null)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            MyPoint p1 = points[j];
                            MyPoint p2 = points[j == 3 ? 0 : j + 1];
                            for (int i = 1; i < 8; i += 2)
                            {
                                MyPoint f1 = formerNodes[i - 1];
                                MyPoint f2 = formerNodes[i];
                                MyPoint f3 = formerNodes[i == 7 ? 0 : i + 1];
                                if (Mathematics.sameNode(f1, p1) && Mathematics.sameNode(f3, p2) ||
                                    Mathematics.sameNode(f1, p2) && Mathematics.sameNode(f3, p1))
                                {
                                    middlePoints[j].X = f2.X;
                                    middlePoints[j].Y = f2.Y;
                                }
                            }
                        }
                    }
                    // иначе тыкаем узлы на середины линий. Ручками, конечно, ручками :(
                    else
                    {
                        if (numOfArcs == 0)
                        {
                            middlePoints[0].X = points[0].X + (points[1].X - points[0].X) / 2;
                            middlePoints[0].Y = points[0].Y + (points[1].Y - points[0].Y) / 2;

                            middlePoints[1].X = points[1].X + (points[2].X - points[1].X) / 2;
                            middlePoints[1].Y = points[1].Y + (points[2].Y - points[1].Y) / 2;

                            middlePoints[2].X = points[2].X + (points[3].X - points[2].X) / 2;
                            middlePoints[2].Y = points[2].Y + (points[3].Y - points[2].Y) / 2;

                            middlePoints[3].X = points[3].X + (points[0].X - points[3].X) / 2;
                            middlePoints[3].Y = points[3].Y + (points[0].Y - points[3].Y) / 2;
                        }

                        else
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                int m = i + 1;
                                if (m == 4) m = 0;
                                MyArc tempArc = formAnArc(points[i], points[m]);
                                if (tempArc != null)
                                {
                                    double startAngle = (180.0 / Math.PI) * ProjectForm.ArcAngle(tempArc, tempArc.StartPoint); // вычисляем углы начальной и конечной точек дуги - меряются они от оси Х против часовой стрелки (в градусах)
                                    double endAngle = (180.0 / Math.PI) * ProjectForm.ArcAngle(tempArc, tempArc.EndPoint);
                                    double centerAngle = 0; // угол от оси Х до точки, делящей дугу по полам

                                    double sweepAngle = 0; // угол дуги
                                    if (tempArc.Clockwise && startAngle > endAngle) sweepAngle = (startAngle - endAngle);
                                    if (tempArc.Clockwise && startAngle < endAngle) sweepAngle = 360.0 - (endAngle - startAngle);
                                    if (!tempArc.Clockwise && startAngle > endAngle) sweepAngle = 360.0 - (startAngle - endAngle);
                                    if (!tempArc.Clockwise && startAngle < endAngle) sweepAngle = (endAngle - startAngle);

                                    if (sweepAngle > 0.0 && sweepAngle <= 180.0)
                                    {
                                        if (tempArc.Clockwise)
                                        {
                                            centerAngle = startAngle - sweepAngle / 2.0;
                                            double X, Y, R;
                                            R = Mathematics.FindDist(tempArc.CenterPoint, tempArc.StartPoint);
                                            X = R * Math.Cos(centerAngle * (Math.PI / 180.0));
                                            Y = R * Math.Sin(centerAngle * (Math.PI / 180.0));
                                            X += tempArc.CenterPoint.X;
                                            Y += tempArc.CenterPoint.Y;
                                            middlePoints[i].X = X;
                                            middlePoints[i].Y = Y;
                                        }
                                        else
                                        {
                                            centerAngle = startAngle + sweepAngle / 2.0;
                                            double X, Y, R;
                                            R = Mathematics.FindDist(tempArc.CenterPoint, tempArc.StartPoint);
                                            X = R * Math.Cos(centerAngle * (Math.PI / 180.0));
                                            Y = R * Math.Sin(centerAngle * (Math.PI / 180.0));
                                            X += tempArc.CenterPoint.X;
                                            Y += tempArc.CenterPoint.Y;
                                            middlePoints[i].X = X;
                                            middlePoints[i].Y = Y;
                                        }
                                    }
                                    if (sweepAngle < 0.0 && sweepAngle >= -180.0)
                                    {
                                        if (tempArc.Clockwise)
                                        {
                                            centerAngle = startAngle + sweepAngle / 2.0;
                                            double X, Y, R;
                                            R = Mathematics.FindDist(tempArc.CenterPoint, tempArc.StartPoint);
                                            X = R * Math.Cos(centerAngle * (Math.PI / 180.0));
                                            Y = R * Math.Sin(centerAngle * (Math.PI / 180.0));
                                            X += tempArc.CenterPoint.X;
                                            Y += tempArc.CenterPoint.Y;
                                            middlePoints[i].X = X;
                                            middlePoints[i].Y = Y;
                                        }
                                        else
                                        {
                                            centerAngle = startAngle - sweepAngle / 2.0;
                                            double X, Y, R;
                                            R = Mathematics.FindDist(tempArc.CenterPoint, tempArc.StartPoint);
                                            X = R * Math.Cos(centerAngle * (Math.PI / 180.0));
                                            Y = R * Math.Sin(centerAngle * (Math.PI / 180.0));
                                            X += tempArc.CenterPoint.X;
                                            Y += tempArc.CenterPoint.Y;
                                            middlePoints[i].X = X;
                                            middlePoints[i].Y = Y;
                                        }
                                    }
                                }
                                else
                                {
                                    middlePoints[i].X = points[i].X + (points[m].X - points[i].X) / 2;
                                    middlePoints[i].Y = points[i].Y + (points[m].Y - points[i].Y) / 2;
                                }
                            }
                        }
                    }

                    List<MyPoint> Nodes = new List<MyPoint>();

                    for (int i = 0; i < 4; i++) // этим мы обеспечиваем чередование угловых узлов зоны с промежуточными узлами, а также следим за сковозной нумерацией узлов для всех зон пластины
                    {
                        bool pointExists = false;
                        bool middlePointExits = false;
                        // Находим все зоны, содержащие текущую точку
                        List<MyArea> areas = parent.currentFullModel.geometryModel.Areas.FindAll(a => a.Nodes.Any(n => Mathematics.sameNode(n, points[i]) || Mathematics.sameNode(n, middlePoints[i])));
                        areas.Sort((a, b) => a.Id.CompareTo(b.Id));
                        foreach (MyArea area in areas) // просматриваем все зоны
                        {
                            int borderSideNumber = 0; // номер стороны существующей зоны, которая соприкасается с другой зоной. номер человеческий и начинается с 1 а не с 0
                            MyPoint node = area.Nodes.Find(n => Mathematics.sameNode(n, points[i]) && !Nodes.Contains(n));
                            if (node != null)
                            {
                                Nodes.Add(node); // добавляем в список узлов зоны узел, который уже есть в списке узлов другой зоны
                                pointExists = true;
                            }
                            node = area.Nodes.Find(n => Mathematics.sameNode(n, middlePoints[i]) && !Nodes.Contains(n));
                            if (node != null)
                            {
                                Nodes.Add(node); // добавляем в список узлов зоны узел, который уже есть в списке узлов другой зоны
                                middlePointExits = true;
                                borderSideNumber = (area.Nodes.IndexOf(node) + 1) / 2;

                                parent.currentFullModel.geometryModel.joinTable[area.Id - 1, borderSideNumber - 1] = newAreaId; // т.к. area.Id и borderSideNumber - это индексы, а нумерация индексов с 0, то отнимаем 1
                                parent.currentFullModel.geometryModel.joinTable[newAreaId - 1, i] = area.Id;
                            }
                        }

                        if (!pointExists) // если пройдя по всем зонам, мы не нашли там таких узлов, то создаем новый объект и добавляем его в список зоны
                        {
                            parent.currentFullModel.geometryModel.NumOfAreaNodes++;
                            Nodes.Add(new MyPoint(parent.currentFullModel.geometryModel.NumOfAreaNodes, points[i].X, points[i].Y, MyPoint.PointType.IsAreaNode));
                            foreach (MyPoint p in parent.currentFullModel.geometryModel.Points) // задаем узлу зоны в соответствие точку геометрии и наоборот
                            {
                                if (p.X == points[i].X && p.Y == points[i].Y)
                                {
                                    Nodes[Nodes.Count - 1].PointReference = p;
                                    p.NodeReference = Nodes[Nodes.Count - 1];
                                    break;
                                }
                            }
                        }

                        if (!middlePointExits) // если пройдя по всем зонам, мы не нашли там таких узлов, то создаем новый объект и добавляем его в список зоны
                        {
                            parent.currentFullModel.geometryModel.NumOfAreaNodes++;
                            Nodes.Add(new MyPoint(parent.currentFullModel.geometryModel.NumOfAreaNodes, middlePoints[i].X, middlePoints[i].Y, MyPoint.PointType.IsAreaNode));
                            foreach (MyPoint p in parent.currentFullModel.geometryModel.Points) // задаем узлу зоны в соответствие точку геометрии и наоборот
                            {
                                if (p.X == middlePoints[i].X && p.Y == middlePoints[i].Y)
                                {
                                    Nodes[Nodes.Count - 1].PointReference = p;
                                    p.NodeReference = Nodes[Nodes.Count - 1];
                                    break;
                                }
                            }
                        }
                    }

                    List<MyStraightLine> Segments = new List<MyStraightLine>();
                    for (int i = 0; i < 8; i++) // этим мы создаем отрезки и соединяем ими узлы зоны
                    {
                        int k = i + 1;
                        if (k == 8) k = 0;
                        Segments.Add(new MyStraightLine(0, Nodes[i], Nodes[k]));
                    }

                    MyArea newArea = new MyArea(newAreaId, lines, slines, Segments, points, arcs, Nodes);
                   
                    parent.currentFullModel.geometryModel.Areas.Add(newArea);
                    success = true;
                    parent.currentFullModel.geometryModel.NumOfAreas++;

                    foreach (MyLine l in lines)
                    {
                        l.Areas.Add(newAreaId);
                    }

                    number.Text = (parent.currentFullModel.geometryModel.NumOfAreas + 1).ToString();


                    parent.showOnlyAreas.Checked = true;
                }
                else
                {
                    errorMessage3.Visible = true;
                }
            }
            else
            {
                errorMessage4.Visible = true;
            }
            return success;
        }

        public double Square(MyPoint p1, MyPoint p2) // функция вычисляет площадь трапеции, ограниченной вектором p1p2, осью ОХ и  перпендикулярами от p1 и p2 к OX 
        {
            double S = (p2.X - p1.X) * (p1.Y + p2.Y) / 2.0;
            return S;
        }

        private double pointsDistance(MyPoint p1, MyPoint p2)
        {
            return ((double)Math.Pow((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y), 0.5));
        }

        private bool formALine(MyPoint p1, MyPoint p2)
        {
            foreach (MyStraightLine line in parent.currentFullModel.geometryModel.StraightLines)
            {
                if ((line.StartPoint == p1 && line.EndPoint == p2) || (line.StartPoint == p2 && line.EndPoint == p1))
                {
                    return true;
                }
            }
            return false;
        }

        private MyArc formAnArc(MyPoint p1, MyPoint p2)
        {
            foreach (MyArc arc in parent.currentFullModel.geometryModel.Arcs)
            {
                if ((arc.StartPoint == p1 && arc.EndPoint == p2) || (arc.StartPoint == p2 && arc.EndPoint == p1))
                {
                    return arc;
                }
            }
            return null;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            parent.clearSelection();
            Dispose();
        }

        private void line1_TextChanged(object sender, EventArgs e)
        {
            sender.ToString();
            if (line1.TextLength == 0) return;
            try
            {
                Convert.ToInt32(line1.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                line1.Select(0, line1.TextLength);
            }
        }

        private void line2_TextChanged(object sender, EventArgs e)
        {
            if (line2.TextLength == 0) return;
            try
            {
                Convert.ToInt32(line2.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                line2.Select(0, line2.TextLength);
            }
        }

        private void line3_TextChanged(object sender, EventArgs e)
        {
            if (line3.TextLength == 0) return;
            try
            {
                Convert.ToInt32(line3.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                line3.Select(0, line3.TextLength);
            }
        }

        private void line4_TextChanged(object sender, EventArgs e)
        {
            if (line4.TextLength == 0) return;
            try
            {
                Convert.ToInt32(line4.Text);
                errorMessage2.Visible = false;
            }
            catch
            {
                errorMessage2.Visible = true;
                line4.Select(0, line4.TextLength);
            }
        }

        private void clearLines()
        {
            this.line1.Text = "";
            this.line2.Text = "";
            this.line3.Text = "";
            this.line4.Text = "";

            this.line1.Select();
        }

        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) Dispose();
        }

        private void line1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) Dispose();
        }

        private void line2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) Dispose();
        }

        private void line3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) Dispose();
        }

        private void line4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter) OK();
            else if (e.KeyChar == (int)Keys.Escape) Dispose();
        }
    }
}
