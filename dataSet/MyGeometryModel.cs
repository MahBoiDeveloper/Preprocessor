using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class MyGeometryModel
    {
        public int NumOfPoints;
        public int NumOfLines;
        public int NumOfCircles;
        public int NumOfAreas;
        public int NumOfAreaNodes;        
        public List<MyPoint> Points;
        public List<MyStraightLine> StraightLines;
        public List<MyLine> Lines
        {
            get
            {
                return StraightLines.ConvertAll(l => l as MyLine).Concat(Arcs.ConvertAll(a => a as MyLine)).ToList();
            }
        }
        public List<MyCircle> Circles;
        public List<MyArc> Arcs;
        public List<MyArea> Areas;

        public List<MyPoint> pairOfPoints = new List<MyPoint>();
        public List<MyPoint> tripleOfPoints = new List<MyPoint>();
        public int numOfSelectedLines = 0; // число выбранных линий - используется при создании зоны кликами мышки
        public List<MyStraightLine> highlightStraightLines = new List<MyStraightLine>(); // выделенные цветом линии
        public List<MyArc> highlightArcs = new List<MyArc>(); // выделенные цветом дуги        
        public MyPoint editedPoint; // редактируемая точка
        public MyPoint editedNode; // редактируемый узел зоны   
        public MyPoint centerOfCircle; // центр создаваемой окружности
        public MyCircle tempCircle; // рисуемая окружность 
        
        
        public int[,] joinTable;

        public int boundaryPointsCount; // число граничных точек, образующих зоны

        

        public MyGeometryModel()
        {
            this.NumOfCircles = 0;
            this.NumOfPoints = 0;
            this.NumOfLines = 0;
            this.NumOfAreas = 0;
            this.NumOfAreaNodes = 0;
            Points = new List<MyPoint>();
            StraightLines = new List<MyStraightLine>();
            Circles = new List<MyCircle>();
            Arcs = new List<MyArc>();
            Areas = new List<MyArea>();
            this.joinTable = new int[100, 4];
        }

    }
}
