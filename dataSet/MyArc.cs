using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class MyArc : MyLine
    {
        /*private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }*/

        private MyPoint centerPoint;
        public MyPoint CenterPoint
        {
            get { return centerPoint; }
            set { centerPoint = value; }
        }

        /*private MyPoint startPoint;
        public MyPoint StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        private MyPoint endPoint;
        public MyPoint EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }*/

        private bool clockwise;
        public bool Clockwise
        {
            get { return clockwise; }
            set { clockwise = value; }
        }

        /*private List<int> areas; // список номеров зон, в которые данная линия входит
        public List<int> Areas
        {
            get { return areas; }
            set { areas = value; }
        }

        private int boundType; // тип закрепеления линии - удобнее хранить его здесь. а в классе geonetryModel просто храним списко закрепленных линий
        public int BoundType   // 0 - линия не закреплена, 11 - закреплена по Х и У, 10 - закреплена по Х, 1 - закреплена по У.
        {
            get { return boundType; }
            set { boundType = value; }
        }*/

        public MyArc(int id, bool direction, MyPoint startPoint, MyPoint endPoint, MyPoint centerPoint)
        {
            this.Id = id;      
            this.Clockwise = direction;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.centerPoint = centerPoint;
            this.areas = new List<int>();
            this.boundType = 0;
            this.startPoint.LineNumbers.Add(id);
            this.endPoint.LineNumbers.Add(id);
            this.centerPoint.LineNumbers.Add(id);

            this.StartPoint.IsStartOfArc = true;
            this.EndPoint.IsEndOfArc = true;
            this.centerPoint.IsCenterOfArc = true;

        }
    }
}
