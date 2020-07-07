using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class MyRectangle
    {
        public MyPoint[] Points { get; private set; }
        public double minX { get; private set; }
        public double maxX { get; private set; }
        public double minY { get; private set; }
        public double maxY { get; private set; }
        public double Width { get { return maxX - minX; } }
        public double Height { get { return maxY - minY; } }

        private MyRectangle()
        {
        }

        public MyRectangle(IEnumerable<MyPoint> points)
        {
            Points = points.ToArray();
            maxX = points.Max(n => n.X);
            minX = points.Min(n => n.X);
            maxY = points.Max(n => n.Y);
            minY = points.Min(n => n.X);
        }

        public static MyRectangle GetAreaRectangle(MyArea area)
        {
            MyRectangle result = new MyRectangle();
            result.maxX = area.Nodes.Max(n => n.X);
            result.minX = area.Nodes.Min(n => n.X);
            result.maxY = area.Nodes.Max(n => n.Y);
            result.minY = area.Nodes.Min(n => n.Y);
            result.Points = new MyPoint[4]
            { 
                 new MyPoint(result.minX, result.minY), 
                 new MyPoint(result.minX, result.maxY), 
                 new MyPoint(result.maxX, result.maxY), 
                 new MyPoint(result.maxX, result.minY) 
            };
            return result;
        }
    }
}
