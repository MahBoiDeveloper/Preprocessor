using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class MyArea
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private List<MyPoint> points;
        public List<MyPoint> Points
        {
            get { return points; }
            set { points = value; }
        }

        private List<MyPoint> nodes;  // узлы зоны
        public List<MyPoint> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }

        private List<MyStraightLine> segments; // отрезки, соединяющие узлы зоны
        public List<MyStraightLine> Segments
        {
            get { return segments; }
            set { segments = value; }
        }
        

        private List<MyStraightLine> straightLines;
        public List<MyStraightLine> StraightLines
        {
            get { return straightLines; }
            set { straightLines = value; }
        }

        private List<MyArc> arcs;
        public List<MyArc> Arcs
        {
            get { return arcs; }
            set { arcs = value; }
        }

        private List<MyLine> lines;
        public List<MyLine> Lines
        {
            get { return lines; }
            set { lines = value; }
        }


        public MyArea(int id, List<MyLine> lines, List<MyStraightLine> slines, List<MyStraightLine> segments, List<MyPoint> points, List<MyArc> arcs, List<MyPoint> nodes)
        {
            this.Id = id;
            this.points = new List<MyPoint>(points);
            this.straightLines = new List<MyStraightLine>(slines);
            this.arcs = new List<MyArc>(arcs);
            this.nodes = new List<MyPoint>(nodes);
            this.segments = new List<MyStraightLine>(segments);
            this.lines = new List<MyLine>(lines);
        }

        public MyArea(MyArea areaToCopy)
        {

            this.Lines = new List<MyLine>();
            foreach (MyLine line in areaToCopy.Lines)
            {
                line.Areas.Clear();
                this.Lines.Add(new MyLine(line.Id));
            }
      
        }
    }
}
