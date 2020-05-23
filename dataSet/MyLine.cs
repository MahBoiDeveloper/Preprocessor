using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class MyLine
    {
        protected int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        protected MyPoint startPoint;
        public MyPoint StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        protected MyPoint endPoint;
        public MyPoint EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }


        protected List<int> areas; // список номеров зон, в которые данная линия входит
        public List<int> Areas
        {
            get { return areas; }
            set { areas = value; }
        }

        protected int boundType; // тип закрепеления линии - удобнее хранить его здесь. а в классе geonetryModel просто храним списко закрепленных линий
        public int BoundType   // 0 - линия не закреплена, 11 - закреплена по Х и У, 10 - закреплена по Х, 1 - закреплена по У.
        {
            get { return boundType; }
            set { boundType = value; }
        }

        public MyLine() { }

        public MyLine(int id)
        {
            this.id = id;
        }
    }
}
