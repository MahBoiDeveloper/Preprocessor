using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ModelComponents;
using System.Linq;
using System.Collections.Generic;
using PreprocessorUtils;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PreprocessorLib
{
    public static class Util
    {
        public static object getObjectCopy(object source)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            try
            {
                formatter.Serialize(stream, source);
            }
            catch { return null; }
            object result;
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                result = formatter.Deserialize(stream);
            }
            catch { return null; }
            return result;
        }

        public static void restoreArraysForOldMethods(this MyFiniteElementModel model, MyGeometryModel geomModel)
        {
            // Восстанавливаем закрепленные узлы
            List<MyNode> boundedNodes = model.Nodes.FindAll(n => n.BoundType != 0);

            // Восстанавливаем закрепленные узлы
            model.NBC.Clear();
            model.NB = boundedNodes.Count;
            model.NBC = boundedNodes.ConvertAll(n => n.Id);
            model.NBC.Insert(0, 0);

            // Типы закреплений
            model.NFIX.Clear();
            model.NFIX = boundedNodes.ConvertAll(n => n.BoundType);
            model.NFIX.Insert(0, 0);

            // Нагрузки 
            model.R.Clear();
            model.Nodes.ConvertAll(n => new double[] { n.ForceX, n.ForceY }).ForEach(pair => model.R.AddRange(pair));
            model.R.Insert(0, 0.0);

            // Сами узлы
            model.NP = model.Nodes.Count;
            model.Nodes.Sort((n, m) => n.Id.CompareTo(m.Id));
            model.CORD.Clear();
            model.Nodes.ConvertAll(n => new double[] { n.X, n.Y }).ForEach(pair => model.CORD.AddRange(pair));
            model.CORD.Insert(0, 0.0);

            // Конечные элементы
            model.NE = model.FiniteElements.Count;
            model.NOP.Clear();
            model.NOP.Insert(0, 0);
            if (model.IMAT.Count == 0) model.IMAT = new List<int>();
            for (int i = 0; i <= model.NE; i++) model.IMAT.Add(0);
            foreach (MyFiniteElement elem in model.FiniteElements)
            {
                List<MyNode> nodes = new List<MyNode>(elem.Nodes);
                // сортировка узлов КЭ против часовой стрелке //
                double[] angle = new double[3];
                for (int i = 1; i < 3; i++)
                {
                    angle[i] = Math.Atan2(nodes[i].Y - nodes[0].Y, nodes[i].X - nodes[0].X);
                    if (angle[i] < 0) angle[i] += Math.PI * 2;
                }
                model.NOP.Add(nodes[0].Id);
                if (angle[1] > angle[2])
                {
                    if (angle[1] - angle[2] < Math.PI)
                    {
                        model.NOP.Add(nodes[2].Id);
                        model.NOP.Add(nodes[1].Id);
                    }
                    else
                    {
                        model.NOP.Add(nodes[1].Id);
                        model.NOP.Add(nodes[2].Id);
                    }
                }
                else
                {
                    if (angle[2] - angle[1] < Math.PI)
                    {
                        model.NOP.Add(nodes[2].Id);
                        model.NOP.Add(nodes[1].Id);
                    }
                    else
                    {
                        model.NOP.Add(nodes[2].Id);
                        model.NOP.Add(nodes[1].Id);
                    }
                }
            }

            // восстанавливаем принадлежность к зонам
            foreach (MyFiniteElement elem in model.FiniteElements)
                elem.DefineArea(geomModel.Areas);
            
            model.INOUT.Clear();
            model.INOUT.Add(1);
            foreach (MyNode node in model.Nodes)
            {
                int nodeCount = model.INOUT.Count;
                foreach (MyFiniteElement elem in node.finiteElements) {
                    MyArea inspectArea = geomModel.Areas.Find(area => area.Id == elem.areaId + 1);
                    double precision = (model.baseType == MyFiniteElementModel.GridType.Delauney || model.type == MyFiniteElementModel.GridType.FrontalMethod) ? 0.01 : -1;
                    if (inspectArea.StraightLines.Find(line => Mathematics.pointOnLine(node, line) && line.Areas.Count == 1) != null)
                        model.INOUT.Add(1);
                    else if (inspectArea.Arcs.Find(arc => Mathematics.pointFitsArc(node, arc, precision) && arc.Areas.Count == 1) != null)
                        model.INOUT.Add(1);
                    if (model.INOUT.Count != nodeCount) break;
                }
                if (nodeCount == model.INOUT.Count)
                    model.INOUT.Add(0);
            }
        }

        public static void DefineArea(this MyFiniteElement elem, IEnumerable<MyArea> areas)
        {
            double x = elem.Nodes.Sum(n => n.X) / 3;
            double y = elem.Nodes.Sum(n => n.Y) / 3;
            
            foreach (MyArea area in areas)
            {
                if (Mathematics.ContainsPoint(area.Nodes,x,y))
                {
                    elem.areaId = area.Id - 1;
                    break;
                }
            }
            if (elem.areaId < 0)
            {
                int i = 0;
                i++;
            }
        }       
    }
}