using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelComponents
{
    [Serializable]
    public class MyFiniteElementModel
    {
        
        /// <summary>
        /// тип сетки - нужно для корректного посроения списка сеток модели
        /// </summary>
        public enum GridType : byte
        {
            /// <summary>
            /// самая обычная сетка
            /// </summary>
            Normal = 0,
            /// <summary>
            /// Получена из Рапперта
            /// </summary>
            Ruppert = 1,
            /// <summary>
            /// Получена из фронтального метода
            /// </summary>
            FrontalMethod = 2,
            /// <summary>
            /// Построена по Делоне
            /// </summary>
            Delauney = 3
        }

        private string modelName;
        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }

        private int id; // айдишник сетки
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public GridType type; // тип сетки - нужно для корректного посроения списка сеток модели
        public GridType baseType; // тип сетки - нужно для корректного посроения списка сеток модели

        public List<MyNode> Nodes { get; private set; }
        public List<MyFiniteElement> FiniteElements { get; private set; }
        public List<int> UsedMaterials;
        public List<MyMaterial> Materials;

        public int NRC;     // параметр NRC
        public int NLD;     // число случаев нагружения
        public int NP;      // число узлов
        public int NE;      // число КЭ      
        public int NB;      // число закреплений  
        public List<double> CORD = new List<double>();  // массив координат
        public List<int> NOP = new List<int>();  // массив номеров узлов
        public List<int> NFIX = new List<int>();    // массив закреплений
        public List<double> R = new List<double>();    // массив нагрузок
        public int NCN = 3;  // число узлов в элементе
        public int NDF = 2;  // число степеней свободы
        public int NMAT;     // число материалов
        public double[] ORT = new double[22]; // массив характеристик свойств КЭ
        public List<int> IMAT = new List<int>(); // массив номеров свойств КЭ
        public List<int> NBC = new List<int>(); // массив номеров закрепленных узлов
        public List<int> INOUT = new List<int>();      // массив внешних и внутренних узлов
        //public List<int> NOTMOVE = new List<int>(); // массив неперемещаемых узлов при оптимизации сетки
        public List<double> XP = new List<double>();
        public List<double> YP = new List<double>();
        public List<double> ZP = new List<double>();
        public List<MyStraightLine> BoundedLines = new List<MyStraightLine>();
        public List<MyArc> BoundedArcs = new List<MyArc>();
        public int[,] areaDefiningNodes = new int[9, 101];

        public MyFiniteElementModel(int id, string name, GridType type)
        {
            this.Id = id;
            this.NLD = 0;
            this.modelName = name;
            Nodes = new List<MyNode>();
            FiniteElements = new List<MyFiniteElement>();
            UsedMaterials = new List<int>();
            Materials = new List<MyMaterial>();
            this.NRC = 0; // вообще этот конструктор для создания варианта сетки после триангуляции - там параметр NRC теряет смысл..
            this.type = type;            
        }

        public MyFiniteElementModel(int id, string name, int NRC, GridType type)
        {
            this.Id = id;
            this.NLD = 0;
            this.modelName = name;
            Nodes = new List<MyNode>();
            FiniteElements = new List<MyFiniteElement>();
            UsedMaterials = new List<int>();
            Materials = new List<MyMaterial>();
            this.NRC = NRC;
            this.type = type;
        }
    }

        /// <summary>
    /// Тип узла (используется при оптимизации сетки)
    /// </summary>
    public enum NodeType : byte
    {
        /// <summary>
        /// Свободный (может перемещаться в любом направлении)
        /// </summary>
        Free = 0,
        /// <summary>
        /// Может перемещатсья только вдоль линии, на которой он лежит
        /// </summary>
        Movable = 1,
        /// <summary>
        /// Фиксированный (не может перемещаться)
        /// </summary>
        Fixed = 2
    }


    [Serializable]
    public class MyNode
    {
        public static implicit operator MyPoint(MyNode node)
        {
            if (node == null) return null;
            return new MyPoint(node.x, node.y, MyPoint.PointType.IsGeometryPoint);
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

        private int boundType;
        public int BoundType
        {
            get { return boundType; }
            set { boundType = value; }
        }

        private double forceX;
        public double ForceX
        {
            get { return forceX; }
            set { forceX = value; }
        }

        private double forceY;
        public double ForceY
        {
            get { return forceY; }
            set { forceY = value; }
        }

        public NodeType Type { get; set; }

        public List<MyFiniteElement> finiteElements = new List<MyFiniteElement>();

        public MyNode(double x = 0, double y = 0, int id = 0)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Type = NodeType.Free;
            this.boundType = 0;
            this.ForceX = 0.0;
            this.ForceY = 0.0;
        }     
    }

    [Serializable]
    public class MyFiniteElement
    {
        public bool badSquare, badAngles;
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        private int materialPropertyID;
        public int MaterialPropertyID
        {
            get { return materialPropertyID; }
            set { materialPropertyID = value; }
        }

        public List<MyNode> Nodes;
        public int areaId;

        public MyFiniteElement(int id, int materialPropertyID, ICollection<MyNode> nodes, int area = 0, bool linkNodes = true)
        {
            this.Id = id;
            this.MaterialPropertyID = materialPropertyID;
            this.Nodes = nodes.ToList();
            this.areaId = area;
            if (linkNodes) 
            foreach (MyNode node in nodes)
                node.finiteElements.Add(this);
            
        }

        public void LinkNodes()
        {
            foreach (MyNode node in Nodes)
                node.finiteElements.Add(this);
        }
    }
}
