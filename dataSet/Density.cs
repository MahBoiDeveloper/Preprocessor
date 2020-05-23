using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class DensityPoint
    {
        public double X, Y, val, R, h;
        public DensityPoint(double x, double y, double val, double R, double h = 0)
        {
            this.X = x;
            this.Y = y;
            this.val = val;
            this.R = R;
            this.h = h;
        }
    }

    [Serializable]
    public class AreaDensity
    {
        public int areaId;
        public int density;
        public double h;
        public AreaDensity(int areaId, int density, double h)
        {
            this.areaId = areaId;
            this.density = density;
            this.h = h;
        }
    }
}
