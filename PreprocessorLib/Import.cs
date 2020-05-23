using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ELW.Library.Math;
using ELW.Library.Math.Exceptions;
using ELW.Library.Math.Expressions;
using ELW.Library.Math.Tools;
using System.Diagnostics;
using ModelComponents;
using PreprocessorUtils;

namespace PreprocessorLib
{
    public partial class ProjectForm
    {
        public void ReadSFMFile(string FileName)
        {
            int i;

            // откроем бинарный файл *.sfm
            FileStream F1 = null;
            try
            {
                F1 = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            BinaryReader reader1 = new BinaryReader(F1);

            byte[] array1 = reader1.ReadBytes(10); // читаем префикс
            string prefix = "";
            for (i = 0; i < 10; i++)
            {
                prefix += Convert.ToChar(array1[i]).ToString();
            }
            if (prefix != "Sigma Form")
            {
                MessageBox.Show("Данный файл не является файлом формы!");
                return;
            }

            int version = reader1.ReadInt32(); // читаем версию 

            int bytes = reader1.ReadInt16(); // читаем количество байт отведенных под хранение числа с плавающей точкой - 2 байта

            byte taskType = reader1.ReadByte();   // тип задачи - 1 байт    

            this.variant.Text = reader1.ReadByte().ToString(); // вариант - 1 байт

            int NRC = reader1.ReadInt16();   // NRC - 2 байта (не имеет никакого значения - параметр все равно задает пользователь при создании сетки)

            // читаем магические 4 байта - не понятно что это... в спецификации нет, в исходниках тоже, откуда взялись - не понятно. если не прочитать, то не заработает...
            int magic = reader1.ReadInt32();

            //24 байта до DB

            double DB = reader1.ReadDouble();  // ширина - 8 байт (не имеет никакого значения - определяется автоматически, если надо)
            double DH = reader1.ReadDouble();  // выстока - 8 байт (не имеет никакого значения - определяется автоматически, если надо)

            double radius = reader1.ReadDouble();   // радиус отверстия - 8 байт (не имеет никакого значения - определяется автоматически, если надо)

            double thickness = reader1.ReadDouble(); // толщина - 8 байт. Такие параметры, как толщина и свойства материала мы не можем применить ко всей пластине из-за устаревшей версии sfm файла, поэтому мы просто сохраним их в библиотеку материалов

            double RsumX = reader1.ReadDouble();
            double RsumY = reader1.ReadDouble();

            Int32 NLD = reader1.ReadInt32(); // число случаев нагружения - 4 байта

            Int32 NDF = reader1.ReadInt32(); // число степеней свободы - 4 байта

            Int32 NCN = reader1.ReadInt32(); // число узлов в элементе - 4 байта

            Int32 NMAT = reader1.ReadInt32(); // число материалов - 4 байта

            double E = reader1.ReadDouble();  // свойства материалов - модуль упругости
            double p = reader1.ReadDouble();  // коэффициент Пуассона
            double T = reader1.ReadDouble();  // допускаемое напряжение
            double L = reader1.ReadDouble();  // 

            Int32 numOfAreas = reader1.ReadInt32(); // число зон - 4 байта

            Int32 numOfPoints = reader1.ReadInt32(); // число узлов зон - 4 байта

            List<MyPoint> allAreaNodes = new List<MyPoint>(); // список всех узлов, но они тут никак не объединены в зоны
            //reader1.ReadInt32();
            for (i = 0; i < numOfPoints; i++)
            {
                int id = reader1.ReadInt16();
                reader1.ReadBytes(6); // магические 6 байт. таже фигня что и выше.. и теперь я кажется знаю что это за ересь. в делфях тип record занимет больше памяти чем сумма памяти под ее члены. и для этого случая аж на 6 байт больше. и располагаются они между integer и real......

                double x = reader1.ReadDouble();
                double y = reader1.ReadDouble();
                allAreaNodes.Add(new MyPoint(id, x, y, MyPoint.PointType.IsAreaNode));
            }
            allAreaNodes.Sort((n1, n2) => n1.Id.CompareTo(n2.Id));

            AddAreasControl control = new AddAreasControl(this);

            for (i = 0; i < numOfAreas; i++)
            {
                List<MyPoint> areaNodes = new List<MyPoint>();  // список из 8 узлов, составляющих зону.
                for (int j = 0; j < 8; j++)
                {
                    int nodeId = reader1.ReadInt16(); // номер узла
                    MyPoint node = allAreaNodes.Find(n => n.Id == nodeId);
                    if (node != null) areaNodes.Add(node);
                }

                // после того, как мы знаем из каких узлов состоит зона, то мы можем создать точки, линии и дуги - т.е. создать гемоетрию по зоне.



                // создаем сначала точки
                List<MyPoint> points = new List<MyPoint>(); // угловые точки зоны
                for (int j = 0; j < 8; j += 2)
                {
                    MyPoint existing = currentFullModel.geometryModel.Points.Find(pt => Mathematics.sameNode(pt, areaNodes[j]));
                    if (existing != null)
                        points.Add(existing);

                    else
                    {
                        MyPoint newPoint = new MyPoint(this.currentFullModel.geometryModel.NumOfPoints + 1, areaNodes[j].X, areaNodes[j].Y, MyPoint.PointType.IsGeometryPoint);
                        this.currentFullModel.geometryModel.Points.Add(newPoint);
                        points.Add(newPoint);
                        //this.DrawPoint(newPoint, System.Drawing.Color.Blue);
                        this.currentFullModel.geometryModel.NumOfPoints++;
                    }
                }

                List<MyStraightLine> slines = new List<MyStraightLine>();
                List<MyArc> arcs = new List<MyArc>();
                List<MyLine> lines = new List<MyLine>();

                // цикл по четырем с сторонам зоны - каждый раз берем три точки с одной стороны - 1,2,3; 3,4,5; 5,6,7 и 7,8,1
                for (int j = 0; j <= 6; j = j + 2)
                {
                    MyPoint p1 = areaNodes[j], p2 = areaNodes[j + 1], p3 = areaNodes[j == 6 ? 0 : j + 2];

                    double xc, yc;

                    double k1 = Math.Cos(Math.Atan2(p2.Y - p1.Y, p2.X - p1.X));
                    double k2 = Math.Cos(Math.Atan2(p3.Y - p2.Y, p3.X - p2.X));
                    if (Math.Abs(k1 - k2) < 0.01) // если коэффициенты наклона совпадают, значит точки лежат на прямой (условие непрерывности выполняется, т.к. известно что все точки принадлежат одной стороне зоны)
                    {
                        // создаем прямую линию
                        MyPoint start = null;
                        MyPoint end = null;

                        foreach (MyPoint point in this.currentFullModel.geometryModel.Points)
                        {
                            if (Mathematics.sameNode(point, p1)) start = point;
                            else if (Mathematics.sameNode(point, p3)) end = point;
                        }

                        MyStraightLine line = currentFullModel.geometryModel.StraightLines.Find(
                                sline => (sline.StartPoint == start && sline.EndPoint == end) || (sline.StartPoint == end && sline.EndPoint == start)
                            );

                        if (line != null)
                        {
                            slines.Add(line);
                            lines.Add(line);
                        }

                        else
                        {
                            MyStraightLine newLine = new MyStraightLine(this.currentFullModel.geometryModel.NumOfLines + 1, start, end);
                            this.currentFullModel.geometryModel.StraightLines.Add(newLine);
                            slines.Add(newLine);
                            lines.Add(newLine);
                            this.currentFullModel.geometryModel.NumOfLines++;
                        }
                    }
                    else
                    {
                        double x1 = p1.X, x2 = p2.X, x3 = p3.X;
                        double y1 = p1.Y, y2 = p2.Y, y3 = p3.Y;
                        // затем проверяем каждую сторону зоны - может ли она быть дугой или нет. для этого пытаемся расчитать центр окружности по трем точкам, и если получается, то создаем дугу.
                        if (Math.Abs(x1 * y2 + y1 * x3 - y2 * x3 - x1 * y3 - x2 * y1 + x2 * y3) > 0.1) // если знаменатель не равен нулю                      
                        {
                            // адские формулы центра окржуности - расчитал Великий Maple
                            // yc = -1.0 / 2.0 * (-x2 * x2 * x1 - y2 * y2 * x1 - x2 * x3 * x3 - x1 * x1 * x3 - y1 * y1 * x3 + x1 * x3 * x3 + x2 * x2 * x3 + y2 * y2 * x3 + x2 * x1 * x1 + x2 * y1 * y1 + x1 * y3 * y3 - x2 * y3 * y3) / (x1 * y2 + y1 * x3 - y2 * x3 - x1 * y3 - x2 * y1 + x2 * y3);
                            // xc = 1.0 / 2.0 * (-x1 * x1 * y3 + x1 * x1 * y2 - x3 * x3 * y2 + y1 * y1 * y2 + y1 * y3 * y3 + y3 * x2 * x2 - y1 * x2 * x2 - y1 * y2 * y2 + y1 * x3 * x3 - y3 * y3 * y2 - y1 * y1 * y3 + y3 * y2 * y2) / (x1 * y2 + y1 * x3 - y2 * x3 - x1 * y3 - x2 * y1 + x2 * y3);

                            // есть формулы попроще:

                            double R1Sqr = x1 * x1 + y1 * y1;
                            double R2Sqr = x2 * x2 + y2 * y2;
                            double R3Sqr = x3 * x3 + y3 * y3;
                            xc = ((R3Sqr - R1Sqr) * (y2 - y1) - (R2Sqr - R1Sqr) * (y3 - y1)) / ((x3 - x1) * (y2 - y1) - (x2 - x1) * (y3 - y1)) / 2.0;
                            yc = ((R2Sqr - R1Sqr) * (x3 - x1) - (R3Sqr - R1Sqr) * (x2 - x1)) / ((x3 - x1) * (y2 - y1) - (x2 - x1) * (y3 - y1)) / 2.0;


                            // ищем в точках точку с такими координатами, если не найдем, то созадим новую
                            MyPoint definedCenter = new MyPoint(xc, yc);

                            MyPoint centerPoint = currentFullModel.geometryModel.Points.Find(pt => Mathematics.sameNode(pt, definedCenter));
                            // если нет, тогда создаем новую точку - центр дуги
                            if (centerPoint == null)
                            {
                                centerPoint = new MyPoint(this.currentFullModel.geometryModel.NumOfPoints + 1, xc, yc, MyPoint.PointType.IsGeometryPoint);
                                this.currentFullModel.geometryModel.Points.Add(centerPoint);
                                //this.DrawPoint(centerPoint, System.Drawing.Color.Blue);
                                this.currentFullModel.geometryModel.NumOfPoints++;
                            }

                            // и проводим дугу...
                            MyPoint start = null;
                            //MyPoint p2 = null;
                            MyPoint end = null;
                            foreach (MyPoint point in this.currentFullModel.geometryModel.Points)
                            {
                                if (Mathematics.sameNode(point, p1)) start = point;
                                else if (Mathematics.sameNode(point, p3)) end = point;
                            }

                            bool lineExists = false;
                            foreach (MyArc arc in this.currentFullModel.geometryModel.Arcs)
                            {
                                if ((arc.StartPoint == start && arc.EndPoint == end) || (arc.StartPoint == end && arc.EndPoint == start))
                                {
                                    lineExists = true;
                                    arcs.Add(arc);
                                    lines.Add(arc);
                                    break;
                                }
                            }

                            if (!lineExists)
                            {
                                // пробуем создать дугу с поворотом по часовой стрелке. если это невозоможно, то создадим с поворотом против)
                                //MyArc newArc = new MyArc(this.currentFullModel.geometryModel.NumOfLines + 1, true, start, p2, centerPoint);
                                MyArc newArc = new MyArc(this.currentFullModel.geometryModel.NumOfLines + 1, true, start, end, centerPoint);
                                if (this.TestArc(newArc))
                                {
                                    this.currentFullModel.geometryModel.Arcs.Add(newArc);
                                    arcs.Add(newArc);
                                    lines.Add(newArc);
                                    //this.DrawArc(newArc, Color.Blue);
                                    this.currentFullModel.geometryModel.NumOfLines++;
                                }
                                else
                                {
                                    //newArc = new MyArc(this.currentFullModel.geometryModel.NumOfLines + 1, false, start, p2, centerPoint);
                                    newArc = new MyArc(this.currentFullModel.geometryModel.NumOfLines + 1, false, start, end, centerPoint);
                                    if (this.TestArc(newArc))
                                    {
                                        this.currentFullModel.geometryModel.Arcs.Add(newArc);
                                        arcs.Add(newArc);
                                        lines.Add(newArc);
                                        //this.DrawArc(newArc, Color.Blue);
                                        this.currentFullModel.geometryModel.NumOfLines++;
                                    }
                                }
                            }

                        }
                    }
                }

                List<MyStraightLine> Segments = new List<MyStraightLine>();
                for (int l = 0; l < 8; l++) // этим мы создаем отрезки и соединяем ими узлы зоны
                {
                    int k = l + 1;
                    if (k == 8) k = 0;
                    Segments.Add(new MyStraightLine(0, areaNodes[l], areaNodes[k]));
                }

                // эмулируем создание зоны так, как если бы мы создавали ее вручную
                control.number.Text = (this.currentFullModel.geometryModel.NumOfAreas + 1).ToString();
                control.line1.Text = lines[0].Id.ToString();
                control.line2.Text = lines[1].Id.ToString();
                control.line3.Text = lines[2].Id.ToString();
                control.line4.Text = lines[3].Id.ToString();
                control.OK(areaNodes);
            }
        }
    }
}
