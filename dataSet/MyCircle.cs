using System;
using System.Collections.Generic;

using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class MyCircle
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private MyPoint centerPoint;
        public MyPoint CenterPoint
        {
            get { return centerPoint; }
            set { centerPoint = value; }
        }

        private double radius;
        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public MyCircle(int id, MyPoint point, double radius)
        {
            this.Id = id;
            this.centerPoint = point;
            this.Radius = radius;
            this.centerPoint.CircleNumbers.Add(id);
        }
    }
}
