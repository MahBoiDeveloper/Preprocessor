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

namespace PreprocessorLib
{
    public partial class StdOptimisation : Form
    {
        ProjectForm parent;
        double GLOBAL_MINSIDE = 0;
        double GLOBAL_MINANG = 0;
        int currentModelIndex;
        MyFiniteElementModel currentModel;

        public StdOptimisation()
        {
            InitializeComponent();
        }

        public StdOptimisation(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;

            currentModel = parent.currentFullModel.FiniteElementModels[parent.GetCurrentModelIndex()];
            currentModelIndex = parent.currentFullModel.FiniteElementModels.Count;

            gridName.Text = "Сетка №" + (currentModelIndex + 1).ToString();
            TopMost = true;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            List<string> lineStrings = new List<string>(this.notMoveLines.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>());
            List<MyLine> fixedLines = new List<MyLine>();
            foreach (string str in lineStrings)
            {
                int idx;
                if (!int.TryParse(str, out idx))
                {
                    MessageBox.Show("Неверно заданы линии!");
                    return;
                }

                MyLine line = (MyLine)parent.currentFullModel.geometryModel.StraightLines.Find(l => l.Id == idx) ?? (MyLine)parent.currentFullModel.geometryModel.Arcs.Find(a => a.Id == idx);
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
                this.Hide();
                parent.StartProgress("Выполняется оптимизация");
                // создаем для новой КЭ модели id
                int id = parent.currentFullModel.IdCandidate;
                currentModel = (MyFiniteElementModel)Util.getObjectCopy(currentModel);
                currentModel.ModelName = gridName.Text;
                currentModel.Id = id;
                currentModel.restoreArraysForOldMethods(parent.currentFullModel.geometryModel);

                int[] notMove = new int[currentModel.INOUT.Count];
                notMove.Initialize();
                if (notMoveLines.TextLength != 0)
                {
                    foreach (MyLine fixedLine in fixedLines)
                    {
                        List<MyNode> tempNodes = new List<MyNode>();
                        if (fixedLine is MyStraightLine)
                        {
                            findNodesAtStraightLine(tempNodes, (MyStraightLine)fixedLine);
                            foreach (MyNode node in tempNodes)
                            {
                                node.Type = NodeType.Fixed;
                                notMove[node.Id] = 1;
                            }
                        }
                        else
                        {
                            findNodesAtArc(tempNodes, (MyArc)fixedLine);
                            foreach (MyNode node in tempNodes)
                            {
                                node.Type = NodeType.Fixed;
                                notMove[node.Id] = 1;
                                //currentModel.INOUT[node.Id] = 1;
                            }
                        }
                    }
                }

                Regularization(currentModel.NRC, parent.currentFullModel.geometryModel.Areas.Count, currentModel, notMove);
                currentModel.FiniteElements.Clear();
                currentModel.Nodes.Clear();

                for (int i = 1; i <= currentModel.NP; i++) // MAXNP - число узлов
                {
                    currentModel.Nodes.Add(new MyNode(currentModel.CORD[2 * (i - 1) + 1], currentModel.CORD[2 * (i - 1) + 2], i));
                }

                for (int temp = 1; temp <= currentModel.NE; temp++)
                {
                    int numOfFE = currentModel.FiniteElements.Count;
                    int numOfNodes = currentModel.Nodes.Count;
                    List<MyNode> tempNodes = new List<MyNode>();

                    double X1 = currentModel.CORD[2 * (currentModel.NOP[3 * (temp - 1) + 1] - 1) + 1];
                    double Y1 = currentModel.CORD[2 * (currentModel.NOP[3 * (temp - 1) + 1] - 1) + 2];
                    double X2 = currentModel.CORD[2 * (currentModel.NOP[3 * (temp - 1) + 2] - 1) + 1];
                    double Y2 = currentModel.CORD[2 * (currentModel.NOP[3 * (temp - 1) + 2] - 1) + 2];
                    double X3 = currentModel.CORD[2 * (currentModel.NOP[3 * (temp - 1) + 3] - 1) + 1];
                    double Y3 = currentModel.CORD[2 * (currentModel.NOP[3 * (temp - 1) + 3] - 1) + 2];

                    foreach (MyNode node in currentModel.Nodes)
                    {
                        if (Math.Abs(node.X - X1) <= 0.001 && Math.Abs(node.Y - Y1) <= 0.001)
                        {
                            tempNodes.Add(node);
                        }
                        if (Math.Abs(node.X - X2) <= 0.001 && Math.Abs(node.Y - Y2) <= 0.001)
                        {
                            tempNodes.Add(node);
                        }
                        if (Math.Abs(node.X - X3) <= 0.001 && Math.Abs(node.Y - Y3) <= 0.001)
                        {
                            tempNodes.Add(node);
                        }
                    }
                    MyFiniteElement elem = new MyFiniteElement(numOfFE + 1, 0, tempNodes);
                    elem.DefineArea(parent.currentFullModel.geometryModel.Areas);
                    currentModel.FiniteElements.Add(elem);
                    tempNodes.Clear();
                }
                parent.EndProgress();
                parent.ModelCreated(currentModel);
                Close();
            }
            else
            {
                errorMessage1.Visible = true;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void findNodesAtStraightLine(List<MyNode> notMoveableNodes, MyStraightLine sline)
        {
            foreach (MyNode node in currentModel.Nodes)
            {
                // уравнение прямой, проходящей через две точки
                if (Mathematics.pointOnLine(node.X, node.Y, sline))
                {
                    if (notMoveableNodes.IndexOf(node) == -1) notMoveableNodes.Add(node);
                }
            }
        }

        private void findNodesAtArc(List<MyNode> notMoveableNodes, MyArc notMoveArc)
        {
            foreach (MyNode node in currentModel.Nodes)
            {
                // уравнение окружности
                if (parent.pointFitsArc(node.X, node.Y, notMoveArc, ProjectForm.checkType.doublePrecision))
                    if (notMoveableNodes.IndexOf(node) == -1) notMoveableNodes.Add(node);
            }
        }

        public void Find_Min_Ang(double[] CDNTFM)
        {

            //C Содержит подпрограмму FIND_MIN_ANG
            //C =================================================================
            //C вычисл-е минимального угла в элементе( треуголнике)
            //C Вызывается из REGULARIZATION, вызываемых модулей нет
            //C =============================================================
            //C  входные параметры:
            //C    CDNTFM - массив координат узлов элемента
            //C  выходные параметры:
            //C    GLOBAL_MINANG - величина минимального угла
            //C    MINSD  - длина минимальной стороны
            //C =================================================================
            //C ============== начало кода FIND_MIN_ANG ==========================
            //C =================================================================

            double SD1, SD2, SD3, TMPSD, COSA, X1, X2, X3, Y1, Y2, Y3;
            //int NUMVAL = 5;

            GLOBAL_MINANG = 1.571;

            //* SD-side
            //* координаты узлов эл-та (вершин треугольника)
            X1 = CDNTFM[1];
            Y1 = CDNTFM[2];
            X2 = CDNTFM[3];
            Y2 = CDNTFM[4];
            X3 = CDNTFM[5];
            Y3 = CDNTFM[6];

            //* вычисл-е длин сторон треуголника
            SD1 = Math.Sqrt((X1 - X2) * (X1 - X2) + (Y1 - Y2) * (Y1 - Y2));
            SD2 = Math.Sqrt((X2 - X3) * (X2 - X3) + (Y2 - Y3) * (Y2 - Y3));
            SD3 = Math.Sqrt((X3 - X1) * (X3 - X1) + (Y3 - Y1) * (Y3 - Y1));

            //*       WRITE(6,*) SD1,SD2,SD3
            //* SD1 - наименьшая сторона
            //* определение минимальной стороны
            if (SD1 > SD2)
            {
                TMPSD = SD1;
                SD1 = SD2;
                SD2 = TMPSD;
            }
            if (SD1 > SD3)
            {
                TMPSD = SD1;
                SD1 = SD3;
                SD3 = TMPSD;
            }
            //* по теореме косинусов определяем мин угол
            COSA = (SD2 * SD2 + SD3 * SD3 - SD1 * SD1) / (2 * SD2 * SD3);
            GLOBAL_MINANG = Math.Acos((SD2 * SD2 + SD3 * SD3 - SD1 * SD1) / (2 * SD2 * SD3));
            GLOBAL_MINSIDE = SD1;
        }

        public void Regularization(int NRC, int zonesCount, MyFiniteElementModel model, int[] notMove)
        {
            //C ===================================================================
            //C REGULARIZATION - процедура оптимизации конечных элементов по углам
            //C вызывается из GRIDDM,
            //C вызывает модули FINDNODD, GET_STAR, FIND_MIN_ANG
            //C ===================================================================
            //C  входные параметры:
            //C    NRC  - параметр разбиения
            //C    zonesCount - число зон
            //C    NOPR - массив номеров узлов, сост-щих элементы
            //C    nodeRowsCount - число строк узлов в зоне ( равно NRC)
            //C    nodeColsCount - число столбцов узлов в зоне ( равно NRC)
            //C    CORDDR  - массив координат узлов
            //C    INOUTR - массив признаков узлов (узел граничный или внутренний)
            //C    NP - число узлов
            //C    NUMOFEL - число элементов
            //C  выходные параметры отсутствуют
            //C =================================================================
            //C ================ начало кода REGULARIZATION ======================


            int[] EX = { 0, 1, 0, -1, 0 };
            int[] EY = { 0, 0, 1, -0, -1 };
            double[] CDNT = new double[7];
            int[] ISTAR = new int[101];
            List<double> CORD = model.CORD;


            List<double> CORDDR_NO_OPT = new List<double>();
            List<int> NOTMOVE = new List<int>();

            int NOUTPNT,
                //IND,
                NSTEL, L, K, INDSTAR, JNDSTAR, KTIMES, MAXTIMES;
            double MINANG, NEWMINANG, NEWMINSIDE, STEP, COORX, COORY, MAXDELTA, CURDELTA, MANG, MODMANG, NEWMODMANG;
            double CURMINANG = 0;
            double CURMINSIDE = 0;
            double MINSIDE = 0;
            int i;

            // NOTMOVE - массив для хранения номеров узлов, которые лежат на границе материалов
            // J - колличество элементов в массиве NOTMOVE

            //double X1,Y1;
            int MAXL = 3;

            int NP = model.NP;


            KTIMES = 0;
            MAXTIMES = 100;

            NOUTPNT = (3 * NRC - 4) * zonesCount + NRC - (zonesCount - 1) * NRC;

            for (i = 0; i <= 2 * NP; i++)
            {
                CORDDR_NO_OPT.Add(0.0);
            }

            for (i = 1; i <= NP; i++)
            {
                CORDDR_NO_OPT[2 * (i - 1) + 1] = CORD[2 * (i - 1) + 1];
                CORDDR_NO_OPT[2 * (i - 1) + 2] = CORD[2 * (i - 1) + 2];
            }

            List<int> nullIndexes = new List<int>();
            int index = -1;
            while ((index = model.INOUT.IndexOf(0, index + 1)) != -1)
            {
                if (notMove[index] == 0)
                    nullIndexes.Add(index);
            }

            // Ищем соседние узлы для всех изменяемых узлов
            Dictionary<int, int[]> stars = new Dictionary<int, int[]>();
            foreach (int IND in nullIndexes)
                stars.Add(IND, Get_Star(IND, model));
            // -- число прогонов процедуры оптимизации
            do
            {
                KTIMES = KTIMES + 1;
                MANG = 1.0472;
                MODMANG = 1.0472;
                NEWMODMANG = 1.0472;
                MAXDELTA = 0.0;
                // -- цикл по узлам
                foreach (int IND in nullIndexes)
                {
                    // если текущий узел не принадлежит границе области и не относится к узлам,
                    // которые лежат на границе материалов      
                    //IF ((INOUTR(IND).EQ.0).AND.(NTM(IND,NOTMOVE,J).EQ.0)) THEN

                    //* -- принадлежит ли узел границе области
                    //C ==============================================================
                    //C -- определяем звезду, соотв-ю данному узлу
                    //C =================================================================

                    ISTAR = stars[IND];
                    NSTEL = ISTAR.Count() / 3;
                    // -- найти более хорошее положение узла
                    // вычисление мин. угла при исходных коор-тах
                    // перебор эл-тов звезды
                    MINANG = 1.0472;
                    for (INDSTAR = 1; INDSTAR <= NSTEL; INDSTAR++)
                    {
                        // перебор узлов текущего эл-та
                        for (JNDSTAR = 1; JNDSTAR <= 3; JNDSTAR++)
                        {
                            // опр-ние коор-т узлов тек. эл-та для вычисл. мин. угла
                            CDNT[2 * (JNDSTAR - 1) + 1] = CORD[2 * (ISTAR[3 * (INDSTAR - 1) + JNDSTAR] - 1) + 1];
                            CDNT[2 * (JNDSTAR - 1) + 2] = CORD[2 * (ISTAR[3 * (INDSTAR - 1) + JNDSTAR] - 1) + 2];
                        }
                        // ==============================================================
                        //     Определение мин. угла в эл-те
                        // =================================================================
                        Find_Min_Ang(CDNT);

                        CURMINANG = GLOBAL_MINANG;
                        CURMINSIDE = GLOBAL_MINSIDE;

                        // определение минимального угла звезды 
                        if (MINANG > CURMINANG)
                        {
                            MINANG = CURMINANG;
                            MINSIDE = CURMINSIDE;
                        }
                    }

                    // осуществляем оптимизацию положения узла
                    STEP = MINSIDE / 3.0;

                    for (L = 1; L <= MAXL; L++)
                    {
                        STEP = STEP / 2.0;

                        for (K = 1; K <= 4; K++)
                        {
                            COORX = CORD[2 * (IND - 1) + 1] + STEP * EX[K];
                            COORY = CORD[2 * (IND - 1) + 2] + STEP * EY[K];

                            // вычисление минимального звезды угла при новом положении узла
                            NEWMINANG = 1.0472;
                            for (INDSTAR = 1; INDSTAR <= NSTEL; INDSTAR++)
                            {
                                // перебор узлов текущего эл-та
                                for (JNDSTAR = 1; JNDSTAR <= 3; JNDSTAR++)
                                {
                                    // опр-ние коор-т узлов тек. эл-та для вычисл. мин. угла
                                    // если это оптимизируемый узел, принять новые координаты
                                    if (ISTAR[3 * (INDSTAR - 1) + JNDSTAR] == IND)
                                    {
                                        CDNT[2 * (JNDSTAR - 1) + 1] = COORX;
                                        CDNT[2 * (JNDSTAR - 1) + 2] = COORY;
                                    }
                                    else
                                    {
                                        CDNT[2 * (JNDSTAR - 1) + 1] = CORD[2 * (ISTAR[3 * (INDSTAR - 1) + JNDSTAR] - 1) + 1];
                                        CDNT[2 * (JNDSTAR - 1) + 2] = CORD[2 * (ISTAR[3 * (INDSTAR - 1) + JNDSTAR] - 1) + 2];
                                    }
                                }
                                // ==============================================================
                                // вычисление мин. угла и стороны тек. эл-та звезды
                                // =================================================================

                                Find_Min_Ang(CDNT);

                                CURMINANG = GLOBAL_MINANG;
                                CURMINSIDE = GLOBAL_MINSIDE;

                                //* определение минимального угла звезды
                                if (NEWMINANG > CURMINANG)
                                {
                                    NEWMINANG = CURMINANG;
                                    NEWMINSIDE = CURMINSIDE;
                                }
                            }

                            // Определение минимального угла из всех
                            // нужно лишь для проверки
                            if (MINANG <= MANG)
                            {
                                MANG = MINANG;
                            }

                            // выяснить, лучше ли нов. коор. узла, т.е. увеличился ли мин. угол
                            if (NEWMINANG > MINANG)
                            {
                                CURDELTA = NEWMINANG - MINANG;
                                if (CURDELTA > MAXDELTA)
                                {
                                    MAXDELTA = CURDELTA;
                                }
                                //* Определение минимального измененного угла из всех
                                //* нужно лишь для проверки
                                if (MODMANG >= MINANG) MODMANG = MINANG;
                                if (NEWMODMANG >= NEWMINANG) NEWMODMANG = NEWMINANG;
                                CORD[2 * (IND - 1) + 1] = COORX;
                                CORD[2 * (IND - 1) + 2] = COORY;
                                MINANG = NEWMINANG;
                                continue;
                            }
                        }
                        Application.DoEvents();
                    }
                }
            } while (MAXDELTA > (0.001) && KTIMES < MAXTIMES);
        }

        public int[] Get_Star(int IND, MyFiniteElementModel model)
        {

            //C Содержит подпрограмму GET_STAR
            //C =============================================================
            //C формирование массива КЭ, сост-щих звезду оптимизируемого КЭ
            //C Вызывается из REGULARIZATION, вызываемых модулей нет
            //C =============================================================
            //C  входные параметры:
            //C    IND  - номер рассматриваемого узла
            //C    NOPT - массив номеров узлов, сост-щих элементы
            //C    NUMOFELT - число элементов
            //C  выходные параметры:
            //C    ITSTAR - массив номеров узлов, сост-щих элементы звезды
            //C    NSTAREL  - число элементов звезды
            //C =============================================================
            //C =============== начало кода GET_STAR ========================= 
            //C =============================================================

            int NODELS, JGT;
            int NSTAREL = 0;
            List<int> nodePoints = model.NOP;
            NODELS = 3;

            JGT = 1;
            List<int> Indexes = new List<int>();
            int index = -1;
            while ((index = nodePoints.IndexOf(IND, index + 1)) != -1)
                Indexes.Add(index - 1);

            int[] ITSTAR = new int[Indexes.Count * 3 + 1];

            foreach (int elemIndex in Indexes)
            {
                int elemNumber = (elemIndex / NODELS);
                ITSTAR[NODELS * (JGT - 1) + 1] = nodePoints[NODELS * (elemNumber) + 1];
                ITSTAR[NODELS * (JGT - 1) + 2] = nodePoints[NODELS * (elemNumber) + 2];
                ITSTAR[NODELS * (JGT - 1) + 3] = nodePoints[NODELS * (elemNumber) + 3];
                JGT++;
            }
            //* запоминаем число элементов звезды
            return ITSTAR;
        }

        private void StdOptimisation_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.clearSelection();
            parent.ReDrawAll();
        }
    }
}
