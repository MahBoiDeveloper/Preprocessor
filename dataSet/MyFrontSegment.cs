using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelComponents
{
    public class MyFrontSegment
    {
        public List<MyNode> baseNodes;
        public List<MyNode> Nodes;
        public List<MyFiniteElement> finiteElems = new List<MyFiniteElement>();
        public MyArea CorrespondingArea = null;

        public MyFrontSegment(MyArea area = null)
        {
            Nodes = new List<MyNode>();
            baseNodes = new List<MyNode>();
            CorrespondingArea = area;
        }

        public MyFrontSegment(int count, MyArea area = null)
        {
            Nodes = new List<MyNode>(count);
            baseNodes = new List<MyNode>(count);
            CorrespondingArea = area;
        }

        public MyFrontSegment(List<MyPoint> _points, MyArea area = null)
        {
            createSegment(_points.ConvertAll(p => new MyNode(p.X, p.Y,p.Id)));
            CorrespondingArea = area;
        }

        private void createSegment(ICollection<MyNode> _nodes) {
            Nodes = new List<MyNode>(_nodes.Count);
            baseNodes = new List<MyNode>(_nodes.Count);
            foreach(MyNode node in _nodes)
            {
                Nodes.Add(new MyNode(node.X, node.Y, node.Id));
                baseNodes.Add(new MyNode(node.X, node.Y, node.Id ));
            }
        }

        public static MyFrontSegment createCopy(MyFrontSegment seg)
        {
            MyFrontSegment newSeg = new MyFrontSegment();
            newSeg.baseNodes = seg.baseNodes;
            newSeg.Nodes = new List<MyNode>();
            foreach (MyNode node in seg.Nodes) newSeg.Nodes.Add(node);
            return newSeg;
        }

        public MyFrontSegment(ICollection<MyNode> _nodes, MyArea area = null)
        {
            createSegment(_nodes);
            CorrespondingArea = area;
        }
    }
}
