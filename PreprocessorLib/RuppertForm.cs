using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModelComponents;
using PreprocessorUtils;
using System.IO;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace PreprocessorLib
{
    public partial class RuppertForm : Form
    {
        ProjectForm parent;
        int currentModelIndex;
        MyFiniteElementModel currentModel;
        Process ruppertProc;


        public RuppertForm(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;

            if (this.parent.currentFullModel.FiniteElementModels.Count == 0)
            {
                MessageBox.Show("Нельзя экспортировать модель в систему Рапперта, пока она не разбита на КЭ!");
                Close();
            }

            currentModel = parent.currentFullModel.FiniteElementModels[parent.GetCurrentModelIndex()];
            currentModelIndex = parent.currentFullModel.FiniteElementModels.Count;

            gridName.Text = "Рапперт_Сетка №" + (currentModelIndex + 1).ToString();
            TopMost = true;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            List<string> lineNumbers = new List<string>(this.notMoveLines.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>());
            List<MyLine> fixedLines = new List<MyLine>();
            if (gridName.Text == string.Empty)
            {
                MessageBox.Show("Имя сетки не может быть пустым!");
                return;
            }
            foreach (string str in lineNumbers)
            {
                int idx;
                if (!int.TryParse(str, out idx))
                {
                    MessageBox.Show("Неверно заданы линии!");
                    return;
                }
                MyLine line = parent.currentFullModel.geometryModel.Lines.Find(l => l.Id == idx);
                if (line == null)
                {
                    MessageBox.Show("Не найдена линия с номером " + idx.ToString());
                    return;
                }
                fixedLines.Add(line);
            }
            parent.precision = parent.DefinePrecision();
            if (parent.currentFullModel.geometryModel.Areas.Count != 0)
            {
                errorMessage1.Visible = false;
                errorBadGridName.Visible = false;

                // создаем новый объект "конечно-элементная модель" и добавляем его в список конечно-элементных моделей. 
                foreach (MyFiniteElementModel model in parent.currentFullModel.FiniteElementModels) // проверяем, нет ли модели с таким именем
                {
                    if (model.ModelName == gridName.Text)
                    {
                        errorBadGridName.Visible = true;
                        return;
                    }
                }

                // создаем для новой КЭ модели id
                int id = parent.currentFullModel.IdCandidate;
                currentModel = (MyFiniteElementModel)Util.getObjectCopy(currentModel);
                currentModel.ModelName = gridName.Text;
                currentModel.Id = id;
                currentModel.type = MyFiniteElementModel.GridType.Ruppert;
                currentModel.restoreArraysForOldMethods(parent.currentFullModel.geometryModel);
                parent.clearSelection();
                parent.ReDrawAll();
                this.Hide();
                Optimize(fixedLines);
            }
            else
            {
                errorMessage1.Visible = true;
            }
        }

        private List<MyNode> findNodesAtStraightLine(MyStraightLine sline)
        {
            List<MyNode> notMoveableNodes = new List<MyNode>();
            foreach (MyNode node in currentModel.Nodes)
            {
                // уравнение прямой, проходящей через две точки
                if (Mathematics.pointOnLine(node.X, node.Y, sline))
                {
                    if (notMoveableNodes.IndexOf(node) == -1) notMoveableNodes.Add(node);
                }
            }
            return notMoveableNodes;
        }

        private void Optimize(List<MyLine> fixedLines)
        {
            List<MyNode> dontMove = new List<MyNode>();

            foreach (MyLine line in fixedLines)
            {
                if (line is MyStraightLine)
                    dontMove.AddRange(findNodesAtStraightLine(line as MyStraightLine));
                else
                    dontMove.AddRange(findNodesAtArc(line as MyArc));
            }

            foreach (MyNode node in dontMove)
                currentModel.INOUT[node.Id] = 1;

            PrepareForRuppert();
            this.Hide();
            ruppertProc = new Process();
            ruppertProc.EnableRaisingEvents = true;
            ruppertProc.StartInfo.FileName = Application.StartupPath + "\\PreprocessorRuppertOpt.exe";
            ruppertProc.StartInfo.Arguments = Path.GetDirectoryName(this.parent.FullProjectFileName);
            ruppertProc.Start();
            parent.Enabled = false;
            ruppertProc.WaitForExit();
            string rup = Path.GetDirectoryName(this.parent.FullProjectFileName) + "\\grid.Ralg";
            if (File.Exists(rup))
                ReadRuppertVariants(rup);
            parent.Enabled = true;
        }

        private List<MyNode> findNodesAtArc(MyArc notMoveArc)
        {
            List<MyNode> notMoveableNodes = new List<MyNode>();
            foreach (MyNode node in currentModel.Nodes)
            {
                // уравнение окружности
                if (parent.pointFitsArc(node.X, node.Y, notMoveArc, ProjectForm.checkType.doublePrecision))
                    if (notMoveableNodes.IndexOf(node) == -1) notMoveableNodes.Add(node);
            }
            return notMoveableNodes;
        }


        public void PrepareForRuppert()
        {
            int i;
            string workDir = Path.GetDirectoryName(this.parent.FullProjectFileName);
            if (File.Exists(workDir + "\\grid.Ralg")) File.Delete(workDir + "\\grid.Ralg");

            // создадим файл griddm.inout

            StringBuilder sb = new StringBuilder();
            StreamWriter sw = new StreamWriter(workDir + "\\griddm.inout");

            for (i = 1; i <= currentModel.NP; i++)
                sb.AppendLine(currentModel.INOUT[i].ToString());

            sw.Write(sb.ToString());
            sw.Dispose();

            // создадим бинарные файлы RESULT1.BIN и RESUL2.BIN

            byte boundtype;
            double xnode, ynode, wEnd, r1, r2;
            FileStream F1, F2;
            Int16 wEnd2 = -1;
            Int16 temp;
            wEnd = -1;


            F1 = new FileStream(workDir + "\\RESULT1.BIN", FileMode.Create, FileAccess.ReadWrite);
            F2 = new FileStream(workDir + "\\RESULT2.BIN", FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter writer1 = new BinaryWriter(F1);
            BinaryWriter writer2 = new BinaryWriter(F2);

            // result 1
            for (i = 1; i <= currentModel.NP; i++)
            {
                xnode = currentModel.CORD[2 * (i - 1) + 1];
                ynode = currentModel.CORD[2 * (i - 1) + 2];

                writer1.Write(xnode);
                writer1.Write(ynode);
                // 9*8*2 = 144
            }


            writer1.Write(wEnd);
            // 144 + 8 = 152;


            for (i = 1; i <= currentModel.NB; i++)
            {
                xnode = currentModel.CORD[2 * (currentModel.NBC[i] - 1) + 1];
                ynode = currentModel.CORD[2 * (currentModel.NBC[i] - 1) + 2];
                writer1.Write(xnode);
                writer1.Write(ynode);
                boundtype = (byte)currentModel.NFIX[i];
                writer1.Write(boundtype);

                // 3* 2 * 8 + 3* 1 = 51

            }
            writer1.Write(wEnd);

            // 51 + 8 + 152 = 211;


            for (i = 1; i <= currentModel.NP; i++)
            {
                if (currentModel.NLD != 0)
                {
                    r1 = currentModel.R[(i - 1) * currentModel.NDF + 1];
                    r2 = currentModel.R[(i - 1) * currentModel.NDF + 2];
                }
                else
                {
                    r1 = 0;
                    r2 = 0;
                }
                if (r1 == 0 && r2 == 0) continue;
                xnode = currentModel.CORD[2 * (i - 1) + 1];
                ynode = currentModel.CORD[2 * (i - 1) + 2];
                writer1.Write(xnode);
                writer1.Write(ynode);
                writer1.Write(r1);
                writer1.Write(r2);
                // 3*8*4 = 96
            }
            writer1.Write(wEnd);

            // 211 + 96 + 8 = 315

            for (i = 1; i <= currentModel.NP; i++)
            {
                xnode = currentModel.CORD[2 * (i - 1) + 1];
                ynode = currentModel.CORD[2 * (i - 1) + 2];
                writer1.Write(xnode);
                writer1.Write(ynode);
                // 9*2*8 = 144
            }
            writer1.Write(wEnd);
            // 315 + 144 + 8 = 467

            for (i = 1; i <= currentModel.NP; i++)
            {
                if (currentModel.NLD != 0)
                {
                    r1 = currentModel.R[(i - 1) * currentModel.NDF + 1];
                    r2 = currentModel.R[(i - 1) * currentModel.NDF + 2];
                }
                else
                {
                    r1 = 0;
                    r2 = 0;
                }
                writer1.Write(r1);
                writer1.Write(r2);
                // 9*2 *8 = 144;
            }
            writer1.Write(wEnd);

            // 144 + 8 + 467 = 619; // гы, хоть размер правльно расчитал..  

            // result 2
            for (i = 1; i <= currentModel.NE; i++)
            {
                temp = (Int16)currentModel.NOP[currentModel.NCN * (i - 1) + 1];
                writer2.Write(temp);
                temp = (Int16)currentModel.NOP[currentModel.NCN * (i - 1) + 2];
                writer2.Write(temp);
                temp = (Int16)currentModel.NOP[currentModel.NCN * (i - 1) + 3];
                writer2.Write(temp);
            }
            writer2.Write(wEnd2);

            for (i = 1; i <= currentModel.NE; i++)
            {
                writer2.Write(0.0);
                writer2.Write(0.0);
                writer2.Write(0.0);
                writer2.Write(0.0);
                writer2.Write(0.0);
                writer2.Write(0.0);
                writer2.Write(0.0);
            }
            writer2.Write(wEnd);

            for (i = 1; i <= 21; i++)
            {
                writer2.Write(currentModel.ORT[i]);
            }
            writer2.Write(wEnd);

            for (i = 1; i <= currentModel.NE; i++)
            {
                temp = (Int16)currentModel.IMAT[i];
                writer2.Write(temp);
            }
            writer2.Write(wEnd);

            F1.Dispose();
            F2.Dispose();
        }

        // чтение вариантов сетки из файла (Рапперт)
        public void ReadRuppertVariants(string file)
        {
            string[] lines = File.ReadAllLines(file, Encoding.Default);

            int NRC = Convert.ToInt32(lines[0]);
            int countOfZones = Convert.ToInt32(lines[1]);
            int CountOfBasePoints = Convert.ToInt32(lines[2]);

            int j = 3;
            // создаем для новой сетки id
            MyFiniteElementModel model = new MyFiniteElementModel(currentModel.Id, currentModel.ModelName, MyFiniteElementModel.GridType.Ruppert);
            int cur = parent.GetCurrentModelIndex();
            model.INOUT = parent.currentFullModel.FiniteElementModels[cur].INOUT;

            model.NP = Convert.ToInt32(lines[j++]); // число узлов в варианте сетки

            model.CORD.Add(0.0);
            for (int n = 1; n <= model.NP; n++)
            {
                int Number = Convert.ToInt32(lines[j++]);
                double X = Convert.ToDouble(lines[j++].Replace(".", ","));
                double Y = Convert.ToDouble(lines[j++].Replace(".", ","));
                //int SeNum = Convert.ToInt32(slines[j++]);
                //int ZoneNum = Convert.ToInt32(slines[j++]);
                //string s = slines[j++];
                //s = slines[j++];
                j += 9;

                model.Nodes.Add(new MyNode(X, Y, Number));
                model.CORD.Add(X);
                model.CORD.Add(Y);
            }

            model.NE = Convert.ToInt32(lines[j++]); // число КЭ в варианте сетки

            for (int e = 1; e <= model.NE; e++)
            {
                int Number = Convert.ToInt32(lines[j++]);
                int Node1 = Convert.ToInt32(lines[j++]);
                int Node2 = Convert.ToInt32(lines[j++]);
                int Node3 = Convert.ToInt32(lines[j++]);
                int Material = Convert.ToInt32(lines[j++]);

                List<MyNode> nodes = new List<MyNode>();
                nodes = model.Nodes.FindAll(n => n.Id == Node1 || n.Id == Node2 || n.Id == Node3);
                MyFiniteElement elem = new MyFiniteElement(Number, Material, nodes);
                elem.DefineArea(parent.currentFullModel.geometryModel.Areas);
                model.FiniteElements.Add(elem);
                nodes.Clear();
            }

            model.NOP.Add(0);
            for (int i = 0; i < model.NE; i++)
            {
                int n1 = model.FiniteElements[i].Nodes[0].Id;
                int n2 = model.FiniteElements[i].Nodes[1].Id;
                int n3 = model.FiniteElements[i].Nodes[2].Id;
                model.NOP.Add(n1);
                model.NOP.Add(n2);
                model.NOP.Add(n3);
            }

            File.Delete(Path.GetDirectoryName(this.parent.FullProjectFileName) + "\\grid.Ralg");
            parent.ModelCreated(model);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RuppertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.activeForm = null;
            parent.clearSelection();
            parent.ReDrawAll();
        }
    }
}
