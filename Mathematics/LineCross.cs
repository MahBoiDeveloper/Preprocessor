using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PreprocessorUtils
{
    public class LineCross
    {
        /*Взято с http://doc-for-prog.narod.ru/topics/math/crossing.html */
        //**************************************************************
        //  RealPoint
        //  представление точки на плоскости
        //**************************************************************
        public struct RealPoint
        {
            public double x;
            public double y;
        };

        //**************************************************************
        //  CrossType
        //  типы пересечения прямых
        //**************************************************************
        public enum enumCrossType
        {
            ctParallel,    // отрезки лежат на параллельных прямых
            ctSameLine,    // отрезки лежат на одной прямой
            ctOnBounds,    // прямые пересекаются в конечных точках отрезков
            ctInBounds,    // прямые пересекаются в   пределах отрезков
            ctOutBounds    // прямые пересекаются вне пределов отрезков
        };

        //**************************************************************
        //  CrossResultRec
        //  результат проверки пересечения прямых
        //**************************************************************
        public struct CrossResultRec
        {
            public enumCrossType type;  // тип пересечения
            public RealPoint pt;    // точка пересечения
        };

        //**************************************************************
        //  Crossing()
        //  проверка пересечения двух отрезков
        //**************************************************************
        public static CrossResultRec Crossing(
            RealPoint p11, RealPoint p12,   // координаты первого отрезка
            RealPoint p21, RealPoint p22)  // координаты второго отрезка
        {
            CrossResultRec result;
            result.type = enumCrossType.ctSameLine;
            result.pt.x = 0;
            result.pt.y = 0;

            //memset((void*)&result, 0, sizeof(result));

            // знаменатель
            double Z = (p12.y - p11.y) * (p21.x - p22.x) - (p21.y - p22.y) * (p12.x - p11.x);
            // числитель 1
            double Ca = (p12.y - p11.y) * (p21.x - p11.x) - (p21.y - p11.y) * (p12.x - p11.x);
            // числитель 2
            double Cb = (p21.y - p11.y) * (p21.x - p22.x) - (p21.y - p22.y) * (p21.x - p11.x);

            // если числители и знаменатель = 0, прямые совпадают
            if ((Z == 0) && (Ca == 0) && (Cb == 0))
            {
                result.type = enumCrossType.ctSameLine;
                return result;
            }


            // если знаменатель = 0, прямые параллельны
            if (Z == 0)
            {
                result.type = enumCrossType.ctParallel;
                return result;
            }


            double Ua = Ca / Z;
            double Ub = Cb / Z;

            result.pt.x = p11.x + (p12.x - p11.x) * Ub;
            result.pt.y = p11.y + (p12.y - p11.y) * Ub;

            // если 0<=Ua<=1 и 0<=Ub<=1, точка пересечения в пределах отрезков
            if ((0 <= Ua) && (Ua <= 1) && (0 <= Ub) && (Ub <= 1))
            {
                if ((Ua == 0) || (Ua == 1) || (Ub == 0) || (Ub == 1))
                {
                    result.type = enumCrossType.ctOnBounds;
                }
                else
                {
                    result.type = enumCrossType.ctInBounds;
                }
            }
            // иначе точка пересечения за пределами отрезков
            else
            {
                result.type = enumCrossType.ctOutBounds;
            }

            return result;
        }
    }
}
