using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;
using ModelComponents;
using PreprocessorUtils;

namespace FrontalMethod
{
    public partial class MethodForm : Form
    {
        private enum EditingState
        {
            none, density, delDensity, add, delete
        }

        EditingState state = EditingState.none, prevstate = EditingState.none;
        double mouseXcord, mouseYcord;
        int feNumber, currentNodes, totalNodes;
        MyNode highlightedNode = null;

        public event Action<MyFiniteElementModel> ModelCreated;
        public event Action Cancel;

        bool scaled = false;
        public int cPointsInZone = 8;           //  Количество узлов в зоне
        public int cZoneCount = 0;              //  Количество зон
        double MeshStep = 10;                   //  Шаг разбиения зон
        float fMiterAngle = 90;                 //  Угол сглаживания
        int iMeshCriterion = 0;                 //  Критерий построения сетки 
        //(0 - по двум сторонам с делением угла пополам, 1 - по четырём сторонам с делением угла пополам, 2 - по четырём сторонам без деления угла пополам)
        double MinAllowAngle = 5;               //  Минимальный допустимый угол
        double MaxAllowAngle = 175;             //  Максимальный допустимый угол
        double MinAllowSquare = 10;             //  Минимальная допустимая площадь треугольника  
        Form owner = null;
        FullModel parentModel = null;
        Dictionary<MyNode, double> currentSegAngles;

        List<DensityPoint> DensityPoints = new List<DensityPoint>();

        ProcessForm meshProgress = new ProcessForm();
        double MinAngle = 0;                    //  Минимальный угол КЭ в сетке
        double MinSquare = 0;                   //  Минимальная площадь КЭ в сетке
        int iZoneMinAngle = 0;                  //  Номер зоны с минимальным углом
        int iZoneMinSquare = 0;                 //  Номер зоны с минимальной площадью КЭ

        List<MyNode> arcCenters = new List<MyNode>();

        List<MyFrontSegment> Front = new List<MyFrontSegment>();
        List<MyFrontSegment> ExternalFront = new List<MyFrontSegment>();                //  Фронт, меняющийся во время построения сетки КЭ

        double AvgDensityFunction = 3;          //  Значение функции плотности по умолчанию


        Settings sts;                           //  Форма настроек
        MyNode curDensityFunc = new MyNode();     //  Текущая точка задания функции плотности

        int iTimeDelay = 20;                    //  Временная задержка для демонстрационного построения сетки КЭ
        int multiplier = 5;                     //  Множитель, необходимый для корректного отображения пластины

        NumberFormatInfo provider = new NumberFormatInfo();
        Visualizer visualizer;
        List<int[]> joinTable = new List<int[]>();

        public MethodForm(Form owner, FullModel model)
        {
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            InitializeComponent();
            parentModel = model;
            this.owner = owner;
            Init(model.geometryModel);
            drawArea.MouseWheel += new MouseEventHandler(drawArea_MouseWheel);
        }

        void drawArea_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) visualizer.DecreaseScale();
            else visualizer.IncreaseScale();
            Draw();
            scaled = true;
        }

        /// <summary>
        /// Чтение структуры зон из объекта геометрии
        /// </summary>
        /// <param name="geometry"></param>
        private void Init(MyGeometryModel geometry)
        {
            cZoneCount = geometry.Areas.Count;
            joinTable = new List<int[]>();
            Front = new List<MyFrontSegment>();
            for (int i = 0; i < cZoneCount; i++)
            {
                MyArea area = geometry.Areas[i];
                Front.Add(new MyFrontSegment(area.Nodes));

                joinTable.Add(new int[4]);
                for (int j = 0; j < 4; j++)
                {
                    joinTable[i][j] = geometry.joinTable[i, j];
                    int neighbor = joinTable[i][j] - 1;
                    if (neighbor != -1 && neighbor < i)
                    {
                        for (int k = j * 2; k < j * 2 + 3; k++)
                        {
                            int index = (k == cPointsInZone) ? 0 : k;
                            Front[i].Nodes[index] = Front[neighbor].Nodes.Find(n => Mathematics.sameNode(n, Front[i].Nodes[index]));
                            Front[i].baseNodes[index] = Front[neighbor].baseNodes.Find(n => Mathematics.sameNode(n, Front[i].baseNodes[index]));
                        }
                    }
                }
            }
        }

        private bool AddElem(int frontNum)
        {
            int IndexMinAngle;
            if (currentSegAngles == null)
            {
                currentSegAngles = new Dictionary<MyNode, double>();
                for (int k = 0; k < ExternalFront[frontNum].Nodes.Count; k++)
                {
                    double angle = FindAngle(k, frontNum);
                    currentSegAngles.Add(ExternalFront[frontNum].Nodes[k], angle);
                }
            }
            currentSegAngles = (from entry in currentSegAngles orderby entry.Value ascending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            IndexMinAngle = ExternalFront[frontNum].Nodes.IndexOf(currentSegAngles.First().Key);
            MinAngle = currentSegAngles.First().Value;
            if ((MinAngle <= fMiterAngle) || Math.Abs(MinAngle - fMiterAngle) < 0.1)
            {
                int iLeftPoint = -1, iRightPoint = -1;
                FindLeftRightIndex2(IndexMinAngle, out iLeftPoint, out iRightPoint, frontNum);
                MyNode[] Triangle = new MyNode[3];    //  Добавление треугольника к списку
                Triangle[0] = ExternalFront[frontNum].Nodes[iLeftPoint];
                Triangle[1] = ExternalFront[frontNum].Nodes[iRightPoint];
                Triangle[2] = ExternalFront[frontNum].Nodes[IndexMinAngle];
                MyFiniteElement fe = new MyFiniteElement(feNumber++, 0, Triangle, frontNum);
                Front[frontNum].finiteElems.Add(fe);
                currentSegAngles.Remove(ExternalFront[frontNum].Nodes[IndexMinAngle]);
                ExternalFront[frontNum].Nodes.RemoveAt(IndexMinAngle);
                currentSegAngles[Triangle[0]] = FindAngle(ExternalFront[frontNum].Nodes.IndexOf(Triangle[0]), frontNum);
                currentSegAngles[Triangle[1]] = FindAngle(ExternalFront[frontNum].Nodes.IndexOf(Triangle[1]), frontNum);
                currentNodes--;
            }
            else
            {
                int iLeftPoint = -1, iRightPoint = -1;
                FindLeftRightIndex2(IndexMinAngle, out iLeftPoint, out iRightPoint, frontNum);
                MyNode oldPoint = ExternalFront[frontNum].Nodes[IndexMinAngle];
                MyNode newPoint = AddPoint(IndexMinAngle, MinAngle, frontNum);
                if (newPoint == null)
                    return false;
                MyNode[] Triangle = new MyNode[3];
                Triangle[0] = ExternalFront[frontNum].Nodes[iLeftPoint];
                Triangle[1] = oldPoint; //  Формируем координаты треугольника для добавления к списку
                Triangle[2] = newPoint;
                Front[frontNum].finiteElems.Add(new MyFiniteElement(feNumber++, 0, Triangle, frontNum));
                Triangle[0] = ExternalFront[frontNum].Nodes[iRightPoint];
                Triangle[1] = oldPoint; //  Формируем координаты треугольника для добавления к списку
                Triangle[2] = newPoint;
                Front[frontNum].finiteElems.Add(new MyFiniteElement(feNumber++, 0, Triangle, frontNum));
                currentSegAngles.Remove(oldPoint);
                currentSegAngles[ExternalFront[frontNum].Nodes[iRightPoint]] = FindAngle(iRightPoint, frontNum);
                currentSegAngles[ExternalFront[frontNum].Nodes[iLeftPoint]] = FindAngle(iLeftPoint, frontNum);
                currentSegAngles[ExternalFront[frontNum].Nodes[IndexMinAngle]] = FindAngle(IndexMinAngle, frontNum);
            }
            return true;
        }

        private void EnableControls()
        {
            gridBox.Enabled = true;
            nodesBox.Enabled = true;
            visualBox.Enabled = true;
            demoBox.Enabled = true;
            if (iMeshCriterion == 4) densityBox.Enabled = true;
            saveAndExitButton.Enabled = true;
            exitButton.Enabled = true;
            state = prevstate;
        }

        private void DisableControls()
        {
            prevstate = state;
            state = EditingState.none;
            gridBox.Enabled = false;
            nodesBox.Enabled = false;
            visualBox.Enabled = false;
            demoBox.Enabled = false;
            densityBox.Enabled = false;
            saveAndExitButton.Enabled = false;
            exitButton.Enabled = false;
        }

        /// <summary>
        /// Формирование и построение сетки КЭ
        /// </summary>
        private void Mesh() //  Формирование и построение сетки КЭ
        {
            feNumber = 1;
            ExternalFront = new List<MyFrontSegment>();
            this.Paint -= MethodForm_Paint;
            DisableControls();
            for (int i = 0; i < Front.Count; i++)
            {
                Front[i].finiteElems.Clear();
                ExternalFront.Add(MyFrontSegment.createCopy(Front[i]));
            }
            meshProgress.Show();
            meshProgress.progressBar1.Value = 0;
            totalNodes = Front.Sum(s => s.Nodes.Count);
            currentNodes = totalNodes;
            for (int j = 0; j < cZoneCount; j++)   //  Выполняем цикл для каждой зоны
            {
                currentSegAngles = null;
                while (ExternalFront[j].Nodes.Count > 3)  //  Если длина фронта ещё может быть разбита
                {
                    if (!AddElem(j)) return;
                    meshProgress.progressBar1.Value = (int)(100.0 * (1.0 - (double)currentNodes / totalNodes));
                    meshProgress.Refresh();
                    Application.DoEvents();
                }
                //  Добавляем последний треугольник к списку треугольников
                Front[j].finiteElems.Add(new MyFiniteElement(feNumber++, 0, ExternalFront[j].Nodes, j));
                currentNodes -= 3;
                meshProgress.progressBar1.Value = (int)(100.0 * (1.0 - (double)currentNodes / totalNodes));
                meshProgress.Refresh();
                Application.DoEvents();
            }
            endMesh();
            this.Paint += MethodForm_Paint;
            meshProgress.Hide();
        }

        /// <summary>
        /// Первый критерий поиска нового узла
        /// </summary>
        /// <param name="index">позиция текущего узла</param>
        /// <param name="AngleMin">величина минимального угла</param>
        /// <param name="FrontNum">Номер текущего фронта</param>
        /// <returns>новая координата узла</returns>
        /// 
        private MyNode FirstCriterion(int index, double AngleMin, int FrontNum)  //  Первый критерий поиска нового узла
        {
            int left, right;
            FindLeftRightIndex2(index, out left, out right, FrontNum);
            MyNode p1 = ExternalFront[FrontNum].Nodes[left];
            MyNode p2 = ExternalFront[FrontNum].Nodes[index];
            MyNode p3 = ExternalFront[FrontNum].Nodes[right];

            double R = (Mathematics.FindDist(p1, p2) + Mathematics.FindDist(p2, p3)) / 2.0;
            double l = Mathematics.CosineLaw2(AngleMin / 2, Mathematics.FindDist(p1, p2), R);
            return Mathematics.findThirdPoint(p1, p2, l, R);
        }

        /// <summary>
        /// Второй критерий поиска нового узла
        /// </summary>
        /// <param name="index">позиция текущего узла</param>
        /// <param name="AngleMin">величина минимального угла</param>
        /// <param name="FrontNum">Номер текущего фронта</param>
        /// <returns>новая координата узла</returns>
        private MyNode SecondCriterion(int index, double AngleMin, int FrontNum)  //  Второй критерий поиска нового узла
        {
            int left, right, left2, right2;
            double R, l;
            FindLeftRightIndex2(index, out left, out right, FrontNum);
            List<MyNode> nodes = ExternalFront[FrontNum].Nodes;
            MyNode p1 = nodes[left];
            MyNode p2 = nodes[index];

            R = Mathematics.FindDist(nodes[index], nodes[left]) + Mathematics.FindDist(nodes[index], nodes[right]);
            FindLeftRightIndex2(left, out left2, out right2, FrontNum);
            R += Mathematics.FindDist(nodes[left], nodes[left2]);
            FindLeftRightIndex2(right, out left2, out right2, FrontNum);
            R += Mathematics.FindDist(nodes[right], nodes[right2]);
            R = R / 4.0;
            l = Mathematics.CosineLaw2(AngleMin / 2, Mathematics.FindDist(p1, p2), R);
            return Mathematics.findThirdPoint(p1, p2, l, R);
        }

        /// <summary>
        /// Третий критерий поиска нового узла
        /// </summary>
        /// <param name="index">позиция текущего узла</param>
        /// <param name="AngleMin">величина минимального угла</param>
        /// <param name="FrontNum">Номер текущего фронта</param>
        /// <returns>новая координата узла</returns>
        private MyNode ThirdCriterion(int index, double AngleMin, int FrontNum)  //  Третий критерий поиска нового узла
        {
            int left, right, left2, right2;
            double l1, l2;

            List<MyNode> nodes = ExternalFront[FrontNum].Nodes;
            FindLeftRightIndex2(index, out left, out right, FrontNum);
            MyNode p1 = nodes[left];
            MyNode p2 = nodes[right];

            FindLeftRightIndex2(left, out left2, out right2, FrontNum);
            l1 = (Mathematics.FindDist(nodes[index], nodes[left]) + Mathematics.FindDist(nodes[left], nodes[left2])) / 2.0;
            FindLeftRightIndex2(right, out left2, out right2, FrontNum);
            l2 = (Mathematics.FindDist(nodes[index], nodes[right]) + Mathematics.FindDist(nodes[right], nodes[right2])) / 2.0;

            MyNode p3 = Mathematics.findThirdPoint(p1, p2, l1, l2);
            return p3;
        }

        /// <summary>
        /// Четвертый критерий поиска нового узла
        /// </summary>
        /// <param name="index">позиция текущего узла</param>
        /// <param name="AngleMin">величина минимального угла</param>
        /// <param name="FrontNum">Номер текущего фронта</param>
        /// <returns>новая координата узла</returns>
        private MyNode FourthCriterion(int index, double AngleMin, int FrontNum)  //  Четвертый критерий поиска нового узла
        {
            int left, right, left2, right2;
            double l1, l2, R;
            List<MyNode> nodes = ExternalFront[FrontNum].Nodes;
            FindLeftRightIndex2(index, out left, out right, FrontNum);
            MyNode p1 = nodes[left];
            MyNode p2 = nodes[right];
            FindLeftRightIndex2(left, out left2, out right2, FrontNum);
            l1 = (Mathematics.FindDist(nodes[index], nodes[left]) + Mathematics.FindDist(nodes[left], nodes[left2])) / 2.0;
            FindLeftRightIndex2(right, out left2, out right2, FrontNum);
            l2 = (Mathematics.FindDist(nodes[index], nodes[right]) + Mathematics.FindDist(nodes[right], nodes[right2])) / 2.0;
            R = (l1 + l2) / 2;
            MyNode p3 = Mathematics.findThirdPoint(p1, p2, l1, l2);
            double R1 = Mathematics.FindDist(nodes[index], p3);
            double angle = Math.Atan2(p3.Y - nodes[index].Y, p3.X - nodes[index].X);
            return new MyNode(p3.X - (R1 - R) * Math.Cos(angle), p3.Y - (R1 - R) * Math.Sin(angle));
        }

        /// <summary>
        /// Пятый критерий поиска нового узла
        /// </summary>
        /// <param name="index">позиция текущего узла</param>
        /// <param name="AngleMin">величина минимального угла</param>
        /// <param name="FrontNum">Номер текущего фронта</param>
        /// <returns>новая координата узла</returns>
        private MyNode FifthCriterion(int index, double AngleMin, int FrontNum)  //  Пятый критерий поиска нового узла
        {
            int left, right;
            double R;
            List<MyNode> nodes = ExternalFront[FrontNum].Nodes;
            FindLeftRightIndex2(index, out left, out right, FrontNum);
            MyNode p1 = ExternalFront[FrontNum].Nodes[left];
            MyNode p2 = ExternalFront[FrontNum].Nodes[index];
            MyNode p3 = ExternalFront[FrontNum].Nodes[right];

            // вычисляем значение функции плотности
            List<KeyValuePair<DensityPoint, double>> dpts = new List<KeyValuePair<DensityPoint, double>>();
            foreach (DensityPoint pt in DensityPoints)
            {
                double dist = Mathematics.FindDist(new MyNode(pt.X, pt.Y), nodes[index]);
                if (dist <= pt.R)
                    dpts.Add(new KeyValuePair<DensityPoint, double>(pt, dist));
            }
            if (dpts.Count > 0)
            {
                R = 0.75 * AvgDensityFunction;
                foreach (KeyValuePair<DensityPoint, double> dpt in dpts)
                    R += (1.0 - dpt.Value / dpt.Key.R) * dpt.Key.val;
                R = R / (dpts.Count + 1);
            }
            else R = AvgDensityFunction;
            MyNode p;
            do
            {
                double l = Mathematics.CosineLaw2(AngleMin / 2, Mathematics.FindDist(p1, p2), R);
                p = Mathematics.findThirdPoint(p1, p2, l, R);
                if (!Mathematics.ContainsPoint(ExternalFront[FrontNum].Nodes, p)) p = null;
                else break;
                R -= 0.05;
            } while (R > 0.1);
            return p;
        }

        delegate MyNode criterion(int index, double anglemin, int frontnum);
        /// <summary>
        /// Построение нового узла
        /// </summary>
        /// <param name="index">позиция текущего узла</param>
        /// <param name="AngleMin">величина минимального угла</param>
        /// <param name="FrontNum">номер текущего фронта</param>
        /// <returns>координаты нового узла</returns>
        private MyNode AddPoint(int index, double AngleMin, int FrontNum)    //  Построение нового узла
        {
            criterion[] criterions = new criterion[4];
            criterions[0] = FirstCriterion;
            criterions[1] = SecondCriterion;
            criterions[2] = ThirdCriterion;
            criterions[3] = FourthCriterion;

            MyNode p = new MyNode();
            if (iMeshCriterion == 4) p = FifthCriterion(index, AngleMin, FrontNum);
            else
            {
                p = criterions[iMeshCriterion](index, AngleMin, FrontNum);
                if (!Mathematics.ContainsPoint(ExternalFront[FrontNum].Nodes, p))
                {
                    p = null;
                }
                int i = 0;
                while (p == null && i < 2)
                {

                    if (i != iMeshCriterion)
                    {
                        p = criterions[i](index, AngleMin, FrontNum);
                        if (!Mathematics.ContainsPoint(ExternalFront[FrontNum].Nodes, p))
                            p = null;
                    }
                    i++;
                }
            }
            if (p != null)
                ExternalFront[FrontNum].Nodes[index] = p;
            else
                endMesh(true);
            return p;
        }

        private void endMesh(bool fail = false)
        {
            timer2.Stop();
            if (fail)
            {
                foreach (MyFrontSegment seg in Front)
                {
                    seg.finiteElems.Clear();
                }
                meshProgress.Hide();
                MessageBox.Show("Не удалось построить сетку");
            }
            EnableControls();
            Draw();
        }

        /// <summary>
        /// Нахождение минимального угла
        /// </summary>
        /// <param name="IndexInFront">позиция узла во фронте</param>
        /// <param name="FrontNum">номер текущего фронта</param>
        /// <returns>величина угла</returns>
        private double FindAngle(int IndexInFront, int FrontNum)    //  Нахождение минимального угла
        {
            double angle = 0;
            int iLeftPoint = -1, iRightPoint = -1;
            FindLeftRightIndex2(IndexInFront, out iLeftPoint, out iRightPoint, FrontNum);
            MyNode[] frontNodes = ExternalFront[FrontNum].Nodes.ToArray();
            MyNode p1 = frontNodes[iLeftPoint];
            MyNode p2 = frontNodes[IndexInFront];
            MyNode p3 = frontNodes[iRightPoint];
            double x1 = p1.X - p2.X, x2 = p3.X - p2.X;
            double y1 = p1.Y - p2.Y, y2 = p3.Y - p2.Y;
            double cos = (x1 * x2 + y1 * y2) / Math.Sqrt(x1 * x1 + y1 * y1) / Math.Sqrt(x2 * x2 + y2 * y2);
            if (Math.Abs(cos) > 1.0) cos = cos / Math.Abs(cos) * 1.0;
            angle = Math.Acos(cos) * 180.0 / Math.PI;

            MyNode p = new MyNode();
            p.X = p1.X + (p3.X - p1.X) / 2;
            p.Y = p1.Y + (p3.Y - p1.Y) / 2;
            if (!Mathematics.ContainsPoint(ExternalFront[FrontNum].Nodes, p))
                angle = 360 - angle;
            else
            {
                int tempLeft = iLeftPoint;
                FindLeftRightIndex2(iRightPoint, out iLeftPoint, out iRightPoint, FrontNum);
                p = frontNodes[iRightPoint];
                if (tempLeft != iRightPoint && Mathematics.pointOnLine(p, new MyStraightLine(0, p1, p3)))
                    angle = 360 - angle;
            }
            return angle;
        }

        /// <summary>
        /// Нахождение индексов соседних точек для изменяющегося фронта
        /// </summary>
        /// <param name="IndexInFront">позиция узла во фронте</param>
        /// <param name="iLeftPoint">позиция левого узла во фронте</param>
        /// <param name="iRightPoint">позиция правого узла во фронте</param>
        /// <param name="FrontNum">номер текущего фронта</param>
        private void FindLeftRightIndex(int IndexInFront, out int iLeftPoint, out int iRightPoint, int FrontNum)
        {
            if (IndexInFront > 0) iLeftPoint = IndexInFront - 1;
            else iLeftPoint = Front[FrontNum].Nodes.Count - 1;
            if (IndexInFront < Front[FrontNum].Nodes.Count - 1) iRightPoint = IndexInFront + 1;
            else iRightPoint = 0;
        }

        /// <summary>
        /// Нахождение индексов соседних точек для неизменяющегося фронта
        /// </summary>
        /// <param name="IndexInFront">позиция узла во фронте</param>
        /// <param name="iLeftPoint">позиция левого узла во фронте</param>
        /// <param name="iRightPoint">позиция правого узла во фронте</param>
        /// <param name="FrontNum">номер текущего фронта</param>
        private void FindLeftRightIndex2(int IndexInFront, out int iLeftPoint, out int iRightPoint, int FrontNum)
        {
            if (IndexInFront > 0) iLeftPoint = IndexInFront - 1;
            else iLeftPoint = ExternalFront[FrontNum].Nodes.Count - 1;
            if (IndexInFront < ExternalFront[FrontNum].Nodes.Count - 1) iRightPoint = IndexInFront + 1;
            else iRightPoint = 0;
        }

        /// <summary>
        /// Разбиение сторон зон на равные отрезки длиной step
        /// </summary>
        /// <param name="step">величина шага разбиения</param>
        private void EqualMesh(double step) //  Разбиение сторон зон на равные отрезки длиной step
        {
            List<MyNode[]> PointPair = new List<MyNode[]>(1);
            MyNode p = new MyNode();
            double fSide, newStep;
            int maxIdx = 0;
            Init(parentModel.geometryModel);
            for (int j = 0; j < Front.Count; j++)
            {
                MyFrontSegment frontSegment = Front[j];
                for (int i = 0; i < cPointsInZone; i += 2) //  И для каждой точки
                {
                    List<MyNode> TempPoints = new List<MyNode>(1);    //  Все добавляемые точки

                    MyNode p1, p2, p3, pO;
                    int count;
                    p1 = frontSegment.baseNodes[i];
                    p1 = frontSegment.Nodes.Find(pt => Mathematics.sameNode(pt, p1));
                    p2 = frontSegment.baseNodes[i + 1];
                    p2 = frontSegment.Nodes.Find(pt => Mathematics.sameNode(pt, p2));
                    p3 = frontSegment.baseNodes[(i + 2 == cPointsInZone) ? 0 : i + 2];
                    p3 = frontSegment.Nodes.Find(pt => Mathematics.sameNode(pt, p3));
                    int neighbor = joinTable[j][i / 2] - 1;
                    if (neighbor != -1)
                    {
                        if (neighbor < j)
                        {
                            MyFrontSegment seg = Front[neighbor];
                            {
                                int newIdx1 = seg.Nodes.IndexOf(seg.Nodes.Find(n => Mathematics.sameNode(p1, n)));
                                int newIdx2 = seg.Nodes.IndexOf(seg.Nodes.Find(n => Mathematics.sameNode(p3, n)));
                                int curIdx1 = frontSegment.Nodes.IndexOf(p1);
                                int curIdx2 = frontSegment.Nodes.IndexOf(p3);
                                int baseIdx1 = seg.baseNodes.IndexOf(seg.baseNodes.Find(n => Mathematics.sameNode(p1, n)));
                                int baseIdx2 = seg.baseNodes.IndexOf(seg.baseNodes.Find(n => Mathematics.sameNode(p3, n)));

                                if (i == 6)
                                    frontSegment.Nodes.RemoveRange(curIdx1, frontSegment.Nodes.Count - curIdx1);
                                else
                                    frontSegment.Nodes.RemoveRange(curIdx1, Math.Abs(curIdx1 - curIdx2) + 1);

                                List<MyNode> toInsert;
                                if (newIdx1 > newIdx2)
                                {
                                    toInsert = seg.Nodes.GetRange(Math.Min(newIdx1, newIdx2), Math.Abs(newIdx2 - newIdx1) + 1);
                                    toInsert.Reverse();
                                }
                                else
                                {
                                    if (baseIdx1 == 0 && baseIdx2 == 6)
                                    {
                                        toInsert = seg.Nodes.GetRange(newIdx2, seg.Nodes.Count - newIdx2);
                                        toInsert.Add(seg.Nodes[0]);
                                        toInsert.Reverse();
                                    }
                                    else
                                        toInsert = seg.Nodes.GetRange(Math.Min(newIdx1, newIdx2), Math.Abs(newIdx2 - newIdx1) + 1);
                                }

                                frontSegment.Nodes.InsertRange(curIdx1, toInsert);
                                if (i == 6)
                                {
                                    frontSegment.Nodes[0] = frontSegment.Nodes[frontSegment.Nodes.Count - 1];
                                    frontSegment.Nodes.RemoveAt(frontSegment.Nodes.Count - 1);
                                }
                                continue;
                            }
                        }
                    }
                    double x1 = p1.X, y1 = p1.Y, x2 = p2.X, y2 = p2.Y, x3 = p3.X, y3 = p3.Y;
                    double k1 = Math.Cos(Math.Atan2(p2.Y - p1.Y, p2.X - p1.X));
                    double k2 = Math.Cos(Math.Atan2(p3.Y - p2.Y, p3.X - p2.X));
                    MyNode[] pts = new MyNode[3];
                    pts[0] = p1; pts[1] = p2; pts[2] = p3;
                    List<MyNode> reNumPoints = new List<MyNode>();
                    if (Math.Abs(k1 - k2) < 0.1)
                    {
                        for (int n = 0; n < 2; n++)
                        {
                            fSide = Mathematics.FindDist(pts[n], pts[n + 1]);  //  Сторона разбиения
                            newStep = Mathematics.FindNewStep(fSide, step);
                            count = (int)Math.Round(fSide / newStep);
                            float stepX, stepY;
                            double angle = Math.Atan2((pts[n + 1].Y - pts[n].Y), (pts[n + 1].X - pts[n].X));
                            stepX = (float)(newStep * Math.Cos(angle));
                            stepY = (float)(newStep * Math.Sin(angle));
                            TempPoints.Clear();
                            if (pts[n].Id >= maxIdx) maxIdx = pts[n].Id + 1;
                            for (int k = 1; k < count; k++)   //  Координаты всех добавляемых точек
                            {
                                TempPoints.Add(new MyNode(pts[n].X + stepX * k, pts[n].Y + stepY * k, maxIdx++));
                            }

                            int index = frontSegment.Nodes.IndexOf(pts[n]) + 1;
                            frontSegment.Nodes.InsertRange(index, TempPoints);
                            if (frontSegment.Nodes.IndexOf(pts[n + 1]) > frontSegment.Nodes.IndexOf(pts[n])) pts[n + 1].Id = maxIdx;
                        }
                    }
                    else
                    {
                        // =====================на дуге
                        pO = Mathematics.IsCircle(p1, p2, p3); //  Находим координаты центра окружности
                        if (pO != null)  //  Если точки лежат на дуге
                        {
                            double radius = Mathematics.FindDist(pO, p1);
                            for (int n = 0; n < 2; n++)
                            {
                                double fCircleLen;   //  Длина фрагмента дуги
                                double fAngle;       //  Угол напротив фрагмента дуги
                                fAngle = Mathematics.CosineLaw(Mathematics.FindDist(pts[n], pts[n + 1]), Mathematics.FindDist(pO, pts[n]), Mathematics.FindDist(pO, pts[n + 1]));
                                fAngle *= Math.PI / 180;
                                fCircleLen = radius * fAngle;
                                newStep = Mathematics.FindNewStep(fCircleLen, step);
                                count = (int)Math.Round(fCircleLen / newStep);
                                if (pts[n].Id >= maxIdx) maxIdx = pts[n].Id + 1;
                                TempPoints.Clear();
                                for (int k = 1; k < count; k++)   //  Координаты всех добавляемых точек
                                {
                                    double alpha;
                                    alpha = fAngle * (newStep * k) / fCircleLen;
                                    p = Mathematics.FindPointOnCircle(pO, pts[n], pts[n + 1], alpha);
                                    TempPoints.Add(p);
                                }
                                int posc = frontSegment.Nodes.IndexOf(pts[n]) + 1;
                                //TempPoints.Reverse();
                                foreach (MyNode pt in TempPoints) pt.Id = maxIdx++;
                                frontSegment.Nodes.InsertRange(posc, TempPoints);
                                if (frontSegment.Nodes.IndexOf(pts[n + 1]) > frontSegment.Nodes.IndexOf(pts[n])) pts[n + 1].Id = maxIdx;
                            }
                        }
                    }
                }
                maxIdx = frontSegment.Nodes.Max(n => n.Id) + 1;
            }
        }

        private void Form1_Load(object sender, EventArgs e) //  Загрузка формы
        {
            provider.NumberDecimalSeparator = ".";
            sts = new Settings();
            if (iMeshCriterion == 4)
            {
                densityBox.Enabled = true;
            }
            else
            {
                densityBox.Enabled = false;
            }
            MeshStep = Convert.ToSingle(stepValue.Text, provider) * multiplier;
            Draw();
        }

        private RectangleF getModelRect()
        {
            List<MyNode> points = new List<MyNode>();
            foreach (MyFrontSegment seg in Front)
                points.AddRange(seg.baseNodes);
            return new RectangleF((float)points.Min(p => p.X), (float)points.Min(p => p.Y), (float)points.Max(p => p.X) - (float)points.Min(p => p.X), (float)points.Max(p => p.Y) - (float)points.Min(p => p.Y));
        }

        private void Draw()
        {
            List<MyNode> points = new List<MyNode>();
            List<MyStraightLine> baseLines = new List<MyStraightLine>();
            List<DensityPoint> denPoints = new List<DensityPoint>();
            visualizer.DropArrays();
            foreach (MyFrontSegment seg in Front)
            {
                points.AddRange(seg.Nodes);
                for (int i = 0; i < seg.Nodes.Count; i++)
                    baseLines.Add(new MyStraightLine(0, seg.Nodes[i], seg.Nodes[(i == seg.Nodes.Count - 1) ? 0 : i + 1]));
            }

            RectangleF rec = getModelRect();
            visualizer.Cls();
            visualizer.MoveTo((rec.Left + rec.Right) / 2.0, (rec.Top + rec.Bottom) / 2.0);

            if (iMeshCriterion == 4)
                visualizer.DrawDensityPoints(DensityPoints.ToArray(), Color.DarkMagenta);
            visualizer.DrawLinesArray(baseLines.ToArray(), Color.Green, false);
            visualizer.DrawPointsArray(points.ConvertAll(p => new MyPoint(p.Id, p.X, p.Y, MyPoint.PointType.IsGeometryPoint)).ToArray(), Color.Green, checkBox2.Checked);
            foreach (MyFrontSegment seg in Front)
                visualizer.DrawElements(seg.finiteElems.ToArray(), Color.Brown, this.showFEnumbers.Checked);
            if (highlightedNode != null) visualizer.DrawPointsArray(new MyPoint[] { new MyPoint(highlightedNode.Id, highlightedNode.X, highlightedNode.Y, MyPoint.PointType.IsGeometryPoint) }, Color.Red, checkBox2.Checked);
            visualizer.DrawRecord(showGrid.Checked);
        }

        private void buildButton_Click(object sender, EventArgs e)  //  Построение сетки КЭ
        {
            Mesh();
            Draw();
        }

        private void deleteButton_Click(object sender, EventArgs e)    //  Удаление сетки КЭ
        {
            foreach (MyFrontSegment seg in Front)
                seg.finiteElems.Clear();
            Draw();
            drawArea.Refresh();
        }

        private void stepButton_Click(object sender, EventArgs e)  //  Задать шаг первичного разбиения сторон зон
        {
            stepValue.Enabled = true;
            stepOkButton.Visible = true;
        }

        private void stepOkButton_Click(object sender, EventArgs e)  //  Подтверждение первичного разбиения сторон зон
        {
            try
            {
                MeshStep = Convert.ToDouble(stepValue.Text);
                if (MeshStep < 0) throw new Exception();
                EqualMesh(MeshStep);
                Draw();
            }
            catch
            {
                MessageBox.Show("Неверно задан шаг!");
                stepValue.Text = "1";
            }

        }

        private void addNodeButton_Click(object sender, EventArgs e)  //  Добавить узлы
        {
            switchButton(addNodeButton, EditingState.add, "Добавить");
        }

        private void deleteNodeButton_Click(object sender, EventArgs e)  //  Удалить узлы
        {
            state = EditingState.delete;
        }

        public bool pointNearLine(double x, double y, MyStraightLine sline)
        {
            return pointNearLine(new MyPoint(x, y, MyPoint.PointType.IsGeometryPoint), sline);
        }

        public bool pointNearLine(MyPoint point, MyStraightLine sline)
        {
            MyPoint comPoint = Mathematics.crossPoint(point, sline);
            if (Mathematics.FindDist(point, comPoint) < visualizer.pointLocality)
                if (comPoint.X <= Math.Max(sline.StartPoint.X, sline.EndPoint.X) && comPoint.X >= Math.Min(sline.StartPoint.X, sline.EndPoint.X) &&
                    comPoint.Y <= Math.Max(sline.StartPoint.Y, sline.EndPoint.Y) && comPoint.Y >= Math.Min(sline.StartPoint.Y, sline.EndPoint.Y)) // условие, гарантирующее, что точка от клика мышки лежит на отрезке, а не на прямой
                    return true;

            return false;
        }

        private void drawArea_MouseClick(object sender, MouseEventArgs e) //  Добавление/удаление/выделение узлов/задание функции плотности
        {
            MyPoint clickPoint = visualizer.getClickPoint(e.X, e.Y);
            switch (state)
            {
                case EditingState.density:
                    {
                        double value = 0;
                        double radius = 0;
                        string fValue = funcValue.Text.Replace('.', ',');
                        try
                        {
                            value = Convert.ToDouble(fValue);
                            radius = Convert.ToDouble(funcRadius.Text);
                            if (radius < 0 || value < 0) throw (new Exception());
                        }
                        catch
                        {
                            MessageBox.Show("Недопустимые значения параметров функции плотности");
                            return;
                        }

                        MyFrontSegment seg = Front.Find(f => Mathematics.ContainsPoint(f.Nodes, clickPoint));
                        if (seg == null) return;
                        DensityPoints.Add(new DensityPoint(clickPoint.X, clickPoint.Y, value, radius));
                        Draw();
                        break;
                    }
                case EditingState.delDensity:
                    {

                        DensityPoints.RemoveAll(p => Mathematics.FindDist(clickPoint, new MyNode(p.X, p.Y)) < visualizer.pointLocality);
                        Draw();
                        break;
                    }
                case EditingState.add:
                    {
                        foreach (MyFrontSegment fseg in Front)
                        {
                            for (int i = 0; i < fseg.Nodes.Count; i++)
                            {
                                // проверяем каждую пару узлов
                                MyNode n1 = fseg.Nodes[i];
                                MyNode n2 = fseg.Nodes[(i + 1 == fseg.Nodes.Count) ? 0 : i + 1];
                                MyStraightLine line = new MyStraightLine(0, n1, n2);
                                // если кликнули рядом с линией, образованной этими узлами
                                if (pointNearLine(clickPoint, line))
                                {
                                    // чтобы получить координаты нового узла, проводим перпендикуляр из точки клика на линию зоны
                                    MyPoint cross = Mathematics.crossPoint(clickPoint, line);
                                    MyNode[] arcBase = null;
                                    // и расстояние до образующих этой линии узлов больше минимально допустимого
                                    if (Mathematics.FindDist(cross, n1) > 1.5 * visualizer.pointLocality &&
                                        Mathematics.FindDist(cross, n2) > 1.5 * visualizer.pointLocality)
                                    {

                                        bool onLine = false;
                                        for (int k = 0; k < cPointsInZone; k += 2)
                                        {
                                            int l = (k + 2 == cPointsInZone) ? 0 : k + 2;
                                            if (Mathematics.pointOnLine(cross, new MyStraightLine(0, fseg.baseNodes[k], fseg.baseNodes[l])))
                                            {
                                                onLine = true;
                                                break;
                                            }
                                            else if (!Mathematics.pointOnLine(fseg.baseNodes[k + 1], new MyStraightLine(0, fseg.baseNodes[k], fseg.baseNodes[l])))
                                            {
                                                MyPoint bCenter = Mathematics.IsCircle(fseg.baseNodes[k], fseg.baseNodes[k + 1], fseg.baseNodes[l]);
                                                MyPoint n1center = Mathematics.IsCircle(fseg.baseNodes[k], n1, fseg.baseNodes[l]);
                                                MyPoint n2center = Mathematics.IsCircle(fseg.baseNodes[k], n2, fseg.baseNodes[l]);
                                                if (n1center == null) n1center = n2center;
                                                if (n2center == null) n2center = n1center;
                                                if (n1center == null) continue;
                                                if (Mathematics.FindDist(bCenter, n1center) < visualizer.pointLocality &&
                                                    Mathematics.FindDist(bCenter, n2center) < visualizer.pointLocality)
                                                {
                                                    arcBase = new MyNode[3] { fseg.baseNodes[k], fseg.baseNodes[k + 1], fseg.baseNodes[l] };
                                                    break;
                                                }
                                            }
                                        }
                                        int lowId = Math.Min(n1.Id, n2.Id);
                                        // выбирем наименьшей из номеров образующих узлов
                                        if (fseg.Nodes.IndexOf(n1) + 1 == fseg.Nodes.Count)
                                            lowId = n1.Id;
                                        // выбираем все узлы модели, у которых придется увеличить номер при добавлении узла
                                        List<MyNode> toReNum = new List<MyNode>();
                                        for (int j = 0; j < Front.Count; j++)
                                            foreach (MyNode node in Front[j].Nodes)
                                                if (node.Id > lowId && !toReNum.Contains(node)) toReNum.Add(node);

                                        MyNode newNode = new MyNode(cross.X, cross.Y, lowId + 1);
                                        if (!onLine && arcBase != null)
                                        {
                                            MyPoint pO = Mathematics.IsCircle(arcBase[0], arcBase[1], arcBase[2]);
                                            double k = Mathematics.FindDist(pO, arcBase[0]) / Mathematics.FindDist(cross, pO);
                                            newNode.X = pO.X + (newNode.X - pO.X) * k;
                                            newNode.Y = pO.Y + (newNode.Y - pO.Y) * k;
                                        }
                                        // находим позицию в массиве узлов, куда надо вставить новый узел
                                        int pos = (lowId == n1.Id) ? fseg.Nodes.IndexOf(n1) + 1 : fseg.Nodes.IndexOf(n2) + 1;
                                        if (Math.Abs(fseg.Nodes.IndexOf(n1) - fseg.Nodes.IndexOf(n2)) == 1) pos = Math.Min(fseg.Nodes.IndexOf(n1), fseg.Nodes.IndexOf(n2)) + 1;
                                        fseg.Nodes.Insert(pos, newNode);

                                        // осталось добавить узел к сегменту, соседнему с текущим (если такой есть)
                                        int sIdx = Front.IndexOf(fseg);
                                        for (int j = 0; j < 4; j++)
                                        {
                                            int neighbor = joinTable[sIdx][j] - 1;
                                            if (neighbor != -1)
                                            {
                                                MyFrontSegment nSeg = Front[neighbor];
                                                pos = Math.Min(nSeg.Nodes.IndexOf(n1) + 1, nSeg.Nodes.IndexOf(n2) + 1);

                                                if (pos != 0)
                                                {
                                                    if (nSeg.Nodes.IndexOf(n2) - nSeg.Nodes.IndexOf(n1) == nSeg.Nodes.Count - 1)
                                                        pos = nSeg.Nodes.IndexOf(n2) + 1;
                                                    nSeg.Nodes.Insert(pos, newNode);
                                                    break;
                                                }
                                            }
                                        }
                                        // изменяем нумерцию узлов
                                        foreach (MyNode node in toReNum)
                                            node.Id++;
                                        Draw();
                                        return;
                                    }
                                }
                            }
                        }
                        break;
                    }
                case EditingState.delete:
                    {
                        MyNode delNode = null;
                        foreach (MyFrontSegment seg in Front)
                        {
                            delNode = seg.Nodes.Find(n => Mathematics.FindDist(n, clickPoint) < visualizer.pointLocality);
                            if (delNode != null)
                            {
                                // запрещаем удаление узлов, образуюших зону
                                if (seg.baseNodes.Find(n => Mathematics.sameNode(n, delNode)) != null) return;
                                // иначе выходим из цикла поиска и продолжаем удалять узел
                                break;
                            }
                        }
                        if (delNode != null)
                        {
                            int id = delNode.Id;
                            List<MyNode> toReNum = new List<MyNode>();
                            for (int j = 0; j < Front.Count; j++)
                                foreach (MyNode node in Front[j].Nodes)
                                    if (node.Id > id && !toReNum.Contains(node)) toReNum.Add(node);
                            foreach (MyFrontSegment seg in Front)
                                seg.Nodes.Remove(delNode);
                            foreach (MyNode node in toReNum) node.Id--;
                            Draw();
                        }
                        break;
                    }

            }
        }

        private void saveAndExitButton_Click(object sender, EventArgs e)  //  Сохранить и выйти
        {
            if (gridName.Text == string.Empty)
            {
                MessageBox.Show("Имя сетки не может быть пустым!");
                return;
            }
            MyFiniteElementModel result = new MyFiniteElementModel(0, gridName.Text, MyFiniteElementModel.GridType.FrontalMethod);
            result.baseType = result.type;
            int nodeId = 1;
            foreach (MyFrontSegment fs in Front)
            {
                result.FiniteElements.AddRange(fs.finiteElems);
                foreach (MyFiniteElement fe in fs.finiteElems)
                    foreach (MyNode node in fe.Nodes)
                        if (!result.Nodes.Contains(node))
                        {
                            node.Id = nodeId++;
                            result.Nodes.Add(node);
                        }
            }
            result.Nodes.Sort((i, j) => i.Id.CompareTo(j.Id));
            if (result.FiniteElements.Count > 0)
            {
                if (ModelCreated != null) ModelCreated(result);
                this.Close();
                owner.Show();
            }
            else
            {
                if (MessageBox.Show("Сетка не была создана! Выйти?", "Внимание", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (this.Cancel != null) Cancel();
                    this.Close();
                    owner.Show();
                }
            }
        }

        static ProcessForm procform = new ProcessForm();

        private void stepValue_TextChanged(object sender, EventArgs e)
        {
            string text = "";
            for (int i = 0; i < stepValue.Text.Length - 1; i++)
                text += stepValue.Text[i];
            if (stepValue.Text.Length >= 1)
                if (!((stepValue.Text[stepValue.Text.Length - 1] >= '0') && (stepValue.Text[stepValue.Text.Length - 1] <= '9')) && (!(stepValue.Text[stepValue.Text.Length - 1] == '.')))
                    stepValue.Text = text;
        }

        int iZone = 0;

        private void demoButton_Click(object sender, EventArgs e) //  Демонстрация построения сетки КЭ
        {
            if (!timer2.Enabled)
            {
                timer2.Interval = speedScrollBar.Value;
                iZone = 0;
                feNumber = 1;
                DisableControls();
                demoBox.Enabled = true;
                ExternalFront = new List<MyFrontSegment>();
                for (int i = 0; i < Front.Count; i++)
                {
                    Front[i].finiteElems.Clear();
                    ExternalFront.Add(MyFrontSegment.createCopy(Front[i]));
                }
                currentSegAngles = null;
                timer2.Start();
            }
            else
            {
                timer2.Stop();
                buildButton_Click(this, new EventArgs());
            }
        }

        private void timer2_Tick(object sender, EventArgs e)    //  Таймер демонстрационного построения сетки КЭ
        {
            if (ExternalFront[iZone].Nodes.Count > 3)
            {
                AddElem(iZone);
                Draw();
                return;
            }

            if (ExternalFront[iZone].Nodes.Count == 3)
            {
                //  Добавляем последний треугольник к списку треугольников
                Front[iZone].finiteElems.Add(new MyFiniteElement(feNumber++, 0, ExternalFront[iZone].Nodes, iZone));
                iZone++;
                currentSegAngles = null;
                if (iZone == cZoneCount)
                    endMesh(false);
            }
            Draw();
        }

        private void speedScrollBar_Scroll(object sender, ScrollEventArgs e)   //  Настройка задержки демонстрации
        {
            iTimeDelay = speedScrollBar.Value;
            if (timer2.Enabled) timer2.Interval = iTimeDelay;
        }

        private void Form1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Help.ShowHelp(this, "Help/FrontalMethodHelp.chm");
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            sts.ShowDialog();
            if (sts.DialogResult == DialogResult.OK)
            {
                if (sts.radioButton1.Checked) iMeshCriterion = 0;
                else if (sts.radioButton2.Checked) iMeshCriterion = 1;
                else if (sts.radioButton3.Checked) iMeshCriterion = 2;
                else if (sts.radioButton4.Checked) iMeshCriterion = 3;
                else if (sts.radioButton5.Checked)
                {
                    iMeshCriterion = 4;
                    densityBox.Enabled = true;
                }
                if (iMeshCriterion != 4)
                {
                    densityBox.Enabled = false;
                }
                AvgDensityFunction = Convert.ToSingle(sts.textBox2.Text, provider);
                fMiterAngle = Convert.ToInt32(sts.maxAngle.Text);
                Draw();
            }
        }


        private void drawArea_Validated(object sender, EventArgs e)
        {
            visualizer.DrawRecord(showGrid.Checked);
        }

        private void Form1_Scroll(object sender, ScrollEventArgs e)
        {
            visualizer.DrawRecord(showGrid.Checked);
        }

        private void switchButton(Button toSwitch, EditingState toCheck, string defaultText)
        {
            if (state != toCheck)
            {
                state = toCheck;
            }
            else
            {
                state = EditingState.none;
            }
        }

        private void addFuncPoint_Click(object sender, EventArgs e)
        {
            switchButton(addFuncPoint, EditingState.density, "Добавить");
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            visualizer.DrawRecord(showGrid.Checked);
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            Draw();
        }

        private void drawArea_Resize(object sender, EventArgs e)
        {
            if (visualizer != null)
            {
                visualizer.RefreshBorders();
                if (!scaled) visualizer.ScaleToFit(getModelRect().Width, getModelRect().Height);
                //visualizer.DrawRecord(showGrid.Checked);
                Draw();
            }
        }

        private void MethodForm_Paint(object sender, PaintEventArgs e)
        {
            drawArea_Resize(this, new EventArgs());
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (this.Cancel != null) Cancel();
            this.Close();
            owner.Show();
        }

        private void drawArea_MouseMove(object sender, MouseEventArgs e)
        {
            MyPoint clickPoint = visualizer.getClickPoint(e.X, e.Y);
            mouseXcord = clickPoint.X;
            mouseYcord = clickPoint.Y;
            if (state == EditingState.none || state == EditingState.delete)
            {
                List<MyNode> nodes = new List<MyNode>();
                foreach (MyFrontSegment seg in Front) nodes.AddRange(seg.Nodes);
                MyNode hoveredNode = nodes.Find(n => Mathematics.FindDist(n, clickPoint) < visualizer.pointLocality);
                if (highlightedNode != hoveredNode)
                {
                    highlightedNode = hoveredNode;
                    Draw();
                }
                if (hoveredNode != null)
                {
                    mouseXcord = hoveredNode.X;
                    mouseYcord = hoveredNode.Y;
                }

            }
            coordX.Text = Math.Round(mouseXcord, 2).ToString();
            coordY.Text = Math.Round(mouseYcord, 2).ToString();
        }

        private void speedScrollBar_ValueChanged(object sender, EventArgs e)
        {
            buildDelay.Text = Math.Round((double)speedScrollBar.Value / 1000.0, 2).ToString() + " c";
            iTimeDelay = speedScrollBar.Value;
            timer2.Interval = iTimeDelay;
        }

        private void drawArea_Load(object sender, EventArgs e)
        {
            visualizer = new Visualizer(drawArea);
        }

        private void deleteFuncButton_Click(object sender, EventArgs e)
        {
            state = EditingState.delDensity;
        }

        private void defaultNodes_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите вернуться к исходной конфигурации узлов?", "Внимание", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Init(parentModel.geometryModel);
                Draw();
            }
        }

        private void MethodForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            owner.Show();
        }

        private void showFEnumbers_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "Help/FrontalMethodHelp.chm");
        }

    }
}