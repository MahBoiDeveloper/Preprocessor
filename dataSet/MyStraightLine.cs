using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ModelComponents
{
    [Serializable]
    public class MyStraightLine : MyLine
    {
        /*private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        private MyPoint startPoint;
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
        }

        private int boundType; // тип закрепеления линии - удобнее хранить его здесь. а в классе geonetryModel просто храним списко закрепленных линий
        public int BoundType   // 0 - линия не закреплена, 11 - закреплена по Х и У, 10 - закреплена по Х, 1 - закреплена по У.
        {
            get { return boundType; }
            set { boundType = value; }
        }

        private List<int> areas; // список номеров зон, в которые данная линия входит
        public List<int> Areas
        {
            get { return areas; }
            set { areas = value; }
        }*/


        public MyStraightLine(int id, MyPoint start, MyPoint end)      
        {
            this.Id = id;
            this.startPoint = start;
            this.endPoint = end;
            this.boundType = 0;
            this.areas = new List<int>();
            this.startPoint.LineNumbers.Add(id);
            this.endPoint.LineNumbers.Add(id);
        }
    }
}
