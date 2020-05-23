using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class MyPoint
    {

        /// <summary>
        /// тип точки - точка геометрии или узел зоны
        /// </summary>
        public enum PointType : byte
        {
            /// <summary>
            /// точка геометрии
            /// </summary>
            IsGeometryPoint = 0,
            /// <summary>
            /// узел зоны
            /// </summary>
            IsAreaNode = 1
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private double x;
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        private double y;
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        private PointType type;
        public PointType Type
        {
            get { return type; }
            set { type = value; }
        }

        private MyPoint pointReference; // сслыка на точку геометрии, имеет место только если этот объект - узел зоны
        public MyPoint PointReference
        {
            get { return pointReference; }
            set { pointReference = value; }
        }

        private MyPoint nodeReference; // сслыка на узел зоны, имеет место только если этот объект - точка геометрии
        public MyPoint NodeReference
        {
            get { return nodeReference; }
            set { nodeReference = value; }
        }

        private List<int> lineNumbers; // список номеров линии, с которыми связана точка
        public List<int> LineNumbers
        {
            get { return lineNumbers; }
            set { lineNumbers = value; }
        }

        private List<int> circleNumbers; // список окружностей, которые связаны с этой точкой
        public List<int> CircleNumbers
        {
            get { return circleNumbers; }
            set { circleNumbers = value; }
        }

        private bool isCenterOfArc;
        public bool IsCenterOfArc
        {
            get { return isCenterOfArc; }
            set { isCenterOfArc = value; }
        }

        private bool isStartOfArc;
        public bool IsStartOfArc
        {
            get { return isStartOfArc; }
            set { isStartOfArc = value; }
        }

        private bool isEndOfArc;
        public bool IsEndOfArc
        {
            get { return isEndOfArc; }
            set { isEndOfArc = value; }
        }

        public static implicit operator MyNode(MyPoint point)
        {
            if (point == null) return null;
            return new MyNode(point.x, point.y);
        }


        public MyPoint(double x = 0, double y = 0, PointType type = MyPoint.PointType.IsGeometryPoint)       
        {      
            this.x = x;
            this.y = y;
            this.type = type;
            this.pointReference = null;
            this.nodeReference = null;
            this.lineNumbers = new List<int>();
            this.circleNumbers = new List<int>();
            this.isCenterOfArc = false;
            this.isStartOfArc = false;
            this.isEndOfArc = false;
        }

        public MyPoint(int id, double x, double y, PointType type) 
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.type = type;
            this.pointReference = null;
            this.nodeReference = null;
            this.lineNumbers = new List<int>();
            this.circleNumbers = new List<int>();
            this.isCenterOfArc = false;
            this.isStartOfArc = false;
            this.isEndOfArc = false;
        }
    }


}
