using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ModelComponents;
using System.Drawing.Drawing2D;

namespace PreprocessorUtils
{
    public static class Mathematics
    {
        public static double accuracy_low = 0.1;
        public static double accuracy_medium = 0.01;
        public static double accuracy_high = 0.001;
        public static double floor(double number, double round_to)
        {
            if (number > 0)
                return Math.Floor(number / round_to + 0.5) * round_to;
            else
                return Math.Ceiling(number / round_to - 0.5) * round_to;
        }

        public static double ArcLength(MyPoint p1, MyPoint p2, MyPoint pc)
        {
            return ArcLength(p1.X, p1.Y, p2.X, p2.Y, pc.X, pc.Y);
        }

        public static double ArcLength(double x1, double y1, double x2, double y2, double xc, double yc)
        {
            double a, b, c, angle;
            a = Mathematics.FindDist(x1, y1, x2, y2);
            b = Mathematics.FindDist(xc, yc, x1, y1);
            c = Mathematics.FindDist(xc, yc, x2, y2);

            angle = Math.Acos((b * b + c * c - a * a) / (2.0 * b * c));

            return b * angle;
        }

        public static bool LessOrEqual(this double a, double b)
        {
            return (a - b < 0.0001);
        }

        public static bool Equal(this double a, double b)
        {
            return Math.Abs(a - b) < 0.0001;
        }

        public static bool MoreOrEqual(this double a, double b)
        {
            return (b - a < 0.0001);
        }

        public static double[] getFEangles(MyFiniteElement fe)
        {
            double[] angles = new double[3];
            double a, b, c;  //  Стороны треугольника
            a = FindDist(fe.Nodes[0], fe.Nodes[1]);
            b = FindDist(fe.Nodes[1], fe.Nodes[2]);
            c = FindDist(fe.Nodes[2], fe.Nodes[0]);
            angles[0] = CosineLaw(a, b, c);
            angles[1] = CosineLaw(b, c, a);
            angles[2] = CosineLaw(c, a, b);
            return angles;
        }

        public static MyPoint crossPoint(MyPoint point, MyStraightLine sline)
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

        /// <summary>
        /// Нахождение расстояния между двумя точками
        /// </summary>
        /// <param name="p1">Точка 1</param>
        /// <param name="p2">Точка 2</param>
        /// <returns>расстояние между точками</returns>
        public static double FindDist(MyPoint p1, MyPoint p2)
        {
            double Dist = Math.Sqrt(((p1.X - p2.X) * (p1.X - p2.X)) + ((p1.Y - p2.Y) * (p1.Y - p2.Y)));
            return Dist;
        }

        /// <summary>
        /// Закон косинуса для нахождения угла треугольника
        /// </summary>
        /// <param name="a">сторона напротив угла</param>
        /// <param name="b">сторона 2</param>
        /// <param name="c">сторона 3</param>
        /// <returns>угол треугольника, град</returns>
        public static double CosineLaw(double a, double b, double c)
        {
            double angle = Math.Acos((b * b + c * c - a * a) / (2 * b * c));
            angle = (angle * 180 / Math.PI);
            return angle;
        }

        /// <summary>
        /// Закон косинуса для нахождения одной из сторон треугольника
        /// </summary>
        /// <param name="angle">угол напротив искомой стороны</param>
        /// <param name="b">сторона 1</param>
        /// <param name="c">сторона 2</param>
        /// <returns>искомая сторона</returns>
        public static double CosineLaw2(double angle, double b, double c)
        {
            angle = ((angle * Math.PI) / 180);
            double a = Math.Sqrt(b * b + c * c - 2 * b * c * Math.Cos(angle));
            return a;
        }

        public static bool ContainsPoint(IEnumerable<MyNode> nodesPolygon, MyPoint point)
        {
            return ContainsPoint(nodesPolygon.ToList().ConvertAll(n => (MyPoint)n), point);
        }

        public static bool ContainsPoint(IEnumerable<MyPoint> nodesPolygon, MyPoint point)
        {
            return ContainsPoint(nodesPolygon, point.X, point.Y);
        }

        public static bool ContainsPoint(IEnumerable<MyPoint> nodesPolygon, double x, double y)
        {
            MyPoint[] polygon = nodesPolygon.ToArray();
            int polySides = polygon.Length;
            int i, j = polySides - 1;
            bool oddNodes = false;
            for (i = 0; i < polySides; i++)
            {
                if ((polygon[i].Y < y && polygon[j].Y >= y
                || polygon[j].Y < y && polygon[i].Y >= y)
                && (polygon[i].X <= x || polygon[j].X <= x))
                {
                    if (polygon[i].X + (y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < x)
                    {
                        oddNodes = !oddNodes;
                    }
                }
                j = i;
            }
            return oddNodes;
        }

        /// <summary>
        /// Нахождение шага разбиения стороны
        /// </summary>
        /// <param name="fSide">длина стороны</param>
        /// <param name="step">первоначальный шаг разбиения</param>
        /// <returns>новый шаг разбиения</returns>
        public static double FindNewStep(double fSide, double step)
        {
            double newStep = 0;
            double n, step1, step2;
            n = fSide / step;
            if (n <= 1.4f) newStep = fSide;
            else if (fSide % step <= 0.3f * step) newStep = step;
            else
            {
                newStep = fSide / Mathematics.floor(fSide / step, 1.0);
                //step1 = step;
                //step2 = step;
                //while ((fSide % step) > (0.3f * step))
                //{
                //    step1 = step1 + 0.1f;
                //    if ((fSide % step1) <= (0.3f * step1))
                //        return step1;
                //    step2 = step2 - 0.1f;
                //    if ((fSide % step2) <= (0.3f * step2))
                //        return step2;
                //}
            }
            return newStep;
        }

        /// <summary>
        /// Нахождение расстояния от точки до прямой, заданной двумя точками
        /// </summary>
        /// <param name="p">точка</param>
        /// <param name="lp1">точка прямой 1</param>
        /// <param name="lp2">точка прямой 2</param>
        /// <returns>расстояние от точки до прямой</returns>
        public static double FindDistPoint2Line(MyPoint p, MyPoint lp1, MyPoint lp2)
        {
            if (lp1.X == lp2.X) return Math.Abs(p.X - lp1.X);
            double fDist = 0;
            double k, b;
            k = 1.0 * (lp2.Y - lp1.Y) / (1.0 * (lp2.X - lp1.X));
            b = lp2.Y - k * lp2.X;
            fDist = Math.Abs(p.Y - k * p.X - b) / Math.Sqrt(k * k + 1);
            return fDist;
        }

        /// <summary>
        /// Проверка условия расположения точки напротив прямой
        /// </summary>
        /// <param name="d1">расстояние от первой точки прямой до точки</param>
        /// <param name="d2">расстояние от второй точки прямой до точки</param>
        /// <param name="h">расстояние от точки до прямой</param>
        /// <param name="Len">расстояние между точками прямой</param>
        /// <returns>признак расположения точки напротив прямой</returns>
        public static bool NearPoint(double d1, double d2, double h, double Len)
        {
            bool bIsNear = false;
            double l1, l2;
            l1 = Math.Sqrt(d1 * d1 - h * h);
            l2 = Math.Sqrt(d2 * d2 - h * h);
            if ((l1 + l2 <= Len + 0.0001f) && (l1 + l2 >= Len - 0.0001f))
                bIsNear = true;
            return bIsNear;
        }


        public static void Swap(ref double v1, ref double v2)
        {
            double tmp = v1;
            v1 = v2;
            v2 = tmp;
        }

        public static bool pointOnLine(double x, double y, MyStraightLine sline)
        {
            return pointOnLine(new MyPoint(x, y, MyPoint.PointType.IsGeometryPoint), sline);
        }

        public static bool pointOnLine(MyPoint point, MyStraightLine sline)
        {

            double x = point.X, x1 = sline.StartPoint.X, x2 = sline.EndPoint.X;
            double y = point.Y, y1 = sline.StartPoint.Y, y2 = sline.EndPoint.Y;
            if (Math.Abs(y2 - y1) < 0.01 && x.MoreOrEqual(Math.Min(x1, x2)) && x.LessOrEqual(Math.Max(x1, x2)) && Math.Abs(y - y1) < 0.01) return true;
            if (Math.Abs(x2 - x1) < 0.01 && y.MoreOrEqual(Math.Min(y1, y2)) && y.LessOrEqual(Math.Max(y1, y2)) && Math.Abs(x - x1) < 0.01) return true;
            bool onLine = (Math.Abs((x - x1) * (y2 - y1) - (y - y1) * (x2 - x1)) < 0.1);
            onLine = (onLine && x.LessOrEqual(Math.Max(x1, x2))
                                && x.MoreOrEqual(Math.Min(x1, x2))
                                && y.LessOrEqual(Math.Max(y1, y2))
                                && y.MoreOrEqual(Math.Min(y1, y2)));
            return onLine;
        }





        public static MyNode FindPointOnCircle(MyPoint pO, MyPoint p1, MyPoint p2, double angle)
        {
            double angleP1 = Math.Atan2(p1.Y - pO.Y, p1.X - pO.X);
            double angleP2 = Math.Atan2(p2.Y - pO.Y, p2.X - pO.X);
            if (angleP2 < angleP1) Swap(ref angleP1, ref angleP2);

            double radius = (double)FindDist(pO, p1);
            return new MyNode(pO.X + radius * (double)Math.Cos(angleP1 + angle), pO.Y + radius * (double)Math.Sin(angleP1 + angle));
        }

        /// <summary>
        /// Проверка положения трёх точек на окружности заданного радиуса
        /// </summary>
        /// <param name="p1">первая точка</param>
        /// <param name="p2">вторая точка</param>
        /// <param name="p3">третья точка</param>
        /// <param name="R">радиус окружности</param>
        /// <returns>координаты центра окружности, иначе точка (-1000;-1000)</returns>
        public static MyNode IsCircle(MyNode p1, MyNode p2, MyNode p3)
        {

            double x1 = p1.X, y1 = p1.Y, x2 = p2.X, y2 = p2.Y, x3 = p3.X, y3 = p3.Y;
            double xc, yc;
            if (Math.Abs(x1 * y2 + y1 * x3 - y2 * x3 - x1 * y3 - x2 * y1 + x2 * y3) > 0.1) // если знаменатель не равен нулю                      
            {
                double R1Sqr = x1 * x1 + y1 * y1;
                double R2Sqr = x2 * x2 + y2 * y2;
                double R3Sqr = x3 * x3 + y3 * y3;
                xc = ((R3Sqr - R1Sqr) * (y2 - y1) - (R2Sqr - R1Sqr) * (y3 - y1)) / ((x3 - x1) * (y2 - y1) - (x2 - x1) * (y3 - y1)) / 2.0f;
                yc = ((R2Sqr - R1Sqr) * (x3 - x1) - (R3Sqr - R1Sqr) * (x2 - x1)) / ((x3 - x1) * (y2 - y1) - (x2 - x1) * (y3 - y1)) / 2.0f;
                return new MyNode(xc, yc);
            }
            return null;
        }

        /// <summary>
        /// Нахождение площади треугольник по формуле Герона
        /// </summary>
        /// <param name="p1">Координаты 1-й точки треугольника</param>
        /// <param name="p2">Координаты 2-й точки треугольника</param>
        /// <param name="p3">Координаты 3-й точки треугольника</param>
        /// <returns>Площадь треугольника</returns>
        /// 
        public static double GeronLaw(List<MyPoint> points)
        {
            return GeronLaw(points[0], points[1], points[2]);
        }

        public static double GeronLaw(List<MyNode> nodes)
        {
            List<MyPoint> points = nodes.ConvertAll(n => (MyPoint)n);
            return GeronLaw(points);
        }
        public static double GeronLaw(MyPoint p1, MyPoint p2, MyPoint p3)
        {
            double Square = -1;
            double a, b, c, p;
            a = FindDist(p1, p2);
            b = FindDist(p2, p3);
            c = FindDist(p3, p1);
            p = (a + b + c) / 2.0;
            Square = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            return Square;
        }

        /// <summary>
        /// Нахождение уравнения прямой в виде y=kx+b, заданной двумя точками
        /// </summary>
        /// <param name="p1">Точка 1 прямой</param>
        /// <param name="p2">Точка 2 прямой</param>
        /// <param name="k">Коэффициент k</param>
        /// <param name="b">Коэффициент b</param>
        public static void FindLineEquation(MyPoint p1, MyPoint p2, out double k, out double b)
        {
            if (p2.X == p1.X)
                k = 0;
            else
                k = 1.0 * (p2.Y - p1.Y) / (p2.X - p1.X);
            b = p1.Y - k * p1.X;
        }

        /// <summary>
        /// Нахождение значения функции плотности в некоторой точке
        /// </summary>
        /// <param name="p">Координаты точки, в которой осуществляется поиск функции плотности</param>
        /// <param name="points">Список координат точек текущей зоны, для которых задана функция плотности</param>
        /// <param name="values">Значения функций плотности в точках текущей зоны, для которых задана функция плотности</param>
        /// <param name="avg">Значение функции плотности по умолчанию</param>
        /// <param name="multiplier">множитель</param>
        /// <returns>Значение функции плотности в искомой точке</returns>
        public static double FindDensityFuncInPoint(MyPoint p, List<MyPoint> points, List<double> values, double avg, double actradius)
        {
            double density = 0;
            List<double> NearValues = new List<double>(1);
            List<double> Koefs = new List<double>(1);
            for (int i = 0; i < points.Count; i++)
                if (FindDist(p, points[i]) <= actradius)
                {
                    NearValues.Add(values[i]);
                    Koefs.Add(1.0 - FindDist(p, points[i]));
                }
            if (NearValues.Count == 0) return avg;

            for (int i = 0; i < NearValues.Count; i++)
                density = density + NearValues[i] * Koefs[i];
            density += avg * 0.75;
            density = density / (NearValues.Count + 1);
            return density;
        }

        /// <summary>
        /// Нахождение координат неизвестной вершины треугольника по трем сторонам и известным координатам двух других вершин
        /// </summary>
        /// <param name="p1">Первая вершина треугольника</param>
        /// <param name="p2">Вторая вершина треугольника</param>
        /// <param name="l1">Расстояние от первой вершины до третьей</param>
        /// <param name="l2">Расстояние от второй вершины до третьей</param>
        /// <returns></returns>
        public static MyNode findThirdPoint(MyNode p1, MyNode p2, double l1, double l2)
        {
            double baseAngle = (CosineLaw(l2, l1, FindDist(p1, p2)) * Math.PI / 180.0);
            float addAngle = (float)Math.Atan2((p2.Y - p1.Y), (p2.X - p1.X));

            // Результат
            MyNode p = new MyNode(p1.X + (float)(l1 * Math.Cos(baseAngle + addAngle)), p1.Y + (float)(l1 * Math.Sin(baseAngle + addAngle)));
            return p;
        }

        public static bool sameNode(MyNode p1, MyNode p2)
        {
            return Math.Abs(p1.X - p2.X) < 0.01 && Math.Abs(p1.Y - p2.Y) < 0.01;
        }


        public static double FindDist(double x1, double y1, double x2, double y2)
        {
            return (double)Math.Pow((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2), 0.5);
        }

        // метод для общих целей
        public static bool pointFitsArc(MyPoint point, MyArc arc, double precision)
        {
            double radius = Mathematics.FindDist(arc.CenterPoint, arc.StartPoint);
            bool pointFits = false;
            if (precision < 0)
            {
                double r = Mathematics.FindDist(arc.CenterPoint, arc.StartPoint);
                precision = r * 0.05;
            }
            pointFits = Math.Abs(Mathematics.FindDist(point, arc.CenterPoint) - radius) < precision;
            if (pointFits) return checkValidPointOnArc(point, arc);
            else return false;
        }

        public static bool checkValidPointOnArc(MyPoint point, MyArc arc)
        {
            if (arc.Clockwise)
            {
                if (point.X.LessOrEqual(arc.StartPoint.X) && point.X.MoreOrEqual(arc.EndPoint.X))
                {
                    if (point.Y.LessOrEqual(arc.StartPoint.Y) || point.Y.LessOrEqual(arc.EndPoint.Y))
                        return true;
                }
                else if (point.X.LessOrEqual(arc.EndPoint.X) && point.X.MoreOrEqual(arc.StartPoint.X))
                {
                    if (point.Y.MoreOrEqual(arc.StartPoint.Y) || point.Y.MoreOrEqual(arc.EndPoint.Y))
                        return true;
                }
                else if (point.Y.LessOrEqual(arc.EndPoint.Y) && point.Y.MoreOrEqual(arc.StartPoint.Y))
                {
                    if (point.X.LessOrEqual(arc.EndPoint.X) || point.X.LessOrEqual(arc.StartPoint.X))
                        return true;
                }
                else if (point.Y.LessOrEqual(arc.StartPoint.Y) && point.Y.MoreOrEqual(arc.EndPoint.Y))
                {
                    if (point.X.MoreOrEqual(arc.EndPoint.X) || point.X.MoreOrEqual(arc.StartPoint.X))
                        return true;
                }
            }
            else if (!arc.Clockwise)
            {
                if (point.X.LessOrEqual(arc.EndPoint.X) && point.X.MoreOrEqual(arc.StartPoint.X))
                {
                    if (point.Y.LessOrEqual(arc.EndPoint.Y) || point.Y.LessOrEqual(arc.StartPoint.Y))
                        return true;
                }
                else if (point.X.LessOrEqual(arc.StartPoint.X) && point.X.MoreOrEqual(arc.EndPoint.X))
                {
                    if (point.Y.MoreOrEqual(arc.StartPoint.Y) || point.Y.MoreOrEqual(arc.EndPoint.Y))
                        return true;
                }
                else if (point.Y.LessOrEqual(arc.EndPoint.Y) && point.Y.MoreOrEqual(arc.StartPoint.Y))
                {
                    if (point.X.MoreOrEqual(arc.EndPoint.X) || point.X.MoreOrEqual(arc.StartPoint.X))
                        return true;
                }
                else if (point.Y.LessOrEqual(arc.StartPoint.Y) && point.Y.MoreOrEqual(arc.EndPoint.Y))
                {
                    if (point.X.LessOrEqual(arc.EndPoint.X) || point.X.LessOrEqual(arc.StartPoint.X))
                        return true;
                }
            }
            return false;
        }
    }
}