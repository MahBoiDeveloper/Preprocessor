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
    public partial class DivideIntoFE : Form
    {
        ProjectForm parent;

        public DivideIntoFE()
        {
            InitializeComponent();
        }

        public DivideIntoFE(ProjectForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.gridName.Text = "Сетка №" + (this.parent.currentFullModel.FiniteElementModels.Count + 1).ToString();
            this.TopMost = true;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.parent.precision = this.parent.DefinePrecision();
            if (this.parent.currentFullModel.geometryModel.Areas.Count != 0)
            {
                this.errorMessage1.Visible = false;
                this.errorBadGridName.Visible = false;
                if (gridName.Text == string.Empty)
                {
                    MessageBox.Show("Имя сетки не может быть пустым!");
                    return;
                }

                List<MyPoint> nodes = new List<MyPoint>();
                foreach (MyArea area in this.parent.currentFullModel.geometryModel.Areas)
                {
                    for (int i = 0; i < 8; i++)
                        if (nodes.IndexOf(area.Nodes[i]) == -1) nodes.Add(area.Nodes[i]);
                }

                // создаем новый объект "конечно-элементная модель" и добавляем его в список конечно-элементных моделей. 
                foreach (MyFiniteElementModel model in this.parent.currentFullModel.FiniteElementModels) // проверяем, нет ли модели с таким именем
                {
                    if (model.ModelName == this.gridName.Text)
                    {
                        this.errorBadGridName.Visible = true;
                        return;
                    }
                }

                // создаем для новой КЭ модели id
                int id = this.parent.currentFullModel.IdCandidate;
                MyFiniteElementModel newModel = new MyFiniteElementModel(id, this.gridName.Text, Convert.ToInt32(this.NRC.Text), MyFiniteElementModel.GridType.Normal);
                newModel.baseType = newModel.type;                    

                this.parent.currentFullModel.geometryModel.boundaryPointsCount = nodes.Count;

                int NRC = Convert.ToInt32(this.NRC.Text);

                if (NRC < 3 || NRC > 15)
                {
                    MessageBox.Show("Нельзя задать такое NRC!");
                    return;
                }
                parent.StartProgress("Формируется сетка");
                this.Hide();
                int result = Griddm(NRC, this.parent.currentFullModel.geometryModel.boundaryPointsCount, this.parent.currentFullModel.geometryModel.Areas.Count, 1, 3, newModel); // последний парамер - номер рассматриваемой модели, расчитаный по числу моделей в списке

                if (result == -1)
                {
                    MessageBox.Show("Невозможно построить сетку КЭ, зоны были заданы непоследовательно!");
                    return;
                }
                parent.EndProgress();
                parent.clearSelection();
                parent.ModelCreated(newModel);
                this.Close();
            }
            else
            {
                this.errorMessage1.Visible = true;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int Griddm(int NRC, int boundaryPointsCount, int zonesCount, int NMAT, int NCN, MyFiniteElementModel newModel)
        {
            // Эти массивы используются при обходе зон для извлечения координат узлов, образующих каждую из зон
            double[] areaNodesX = new double[10];
            double[] areaNodesY = new double[10];
            // функции формы для квадратичного четырехугольника
            double[] formFunctions = new double[9];
            // номера узлов, определяющих зону
            int[] definingNodesNumbers = new int[9];
            // ?
            int[] LB = new int[4];
            double[] CORD = new double[4];
            double[] CORD1 = new double[4];
            // номера узлов зоны
            int[,] nodeNumbers = new int[53, 53];
            // видимо, номера элементов
            int[] NE = new int[601];
            double[] xElem = new double[601];
            double[] yElem = new double[601];

            int[] NR = new int[5];
            // координаты X и Y узлов зоны
            double[,] xCord = new double[53, 53];
            double[,] yCord = new double[53, 53];
            // трехмерный массив номеров узлов, находящихся на границе. По индексам: 1 - зона, 2 - сторона, 3 - номер узла
            int[, ,] boundaryNodesNumbers = new int[101, 5, 53];

            // Матрица данных о соединениях зон. Строка i = номер зоны, столбец j = номер стороны, элемент на пересечении - граничащая с зоной i зона по стороне j
            int[,] joinTable = new int[101, 5];

            // ооох.
            int[,] ICOMP = 
            { 
                { 0, 0, 0, 0, 0 },
                { 0, -1, 1, 1, -1 },
                { 0, 1, -1, -1, 1 },
                { 0, 1, -1, -1, 1 },
                { 0, -1, 1, 1, -1 }
            };
            double[] GLOB = { 0.0, 0.0, -1.0, 3.0, 0.0, -5.0, -1.0, 0.0, 3.0, 0.0 };

            List<double> CORDD = new List<double>();
            List<int> NOP = new List<int>();
            //List<int> INOUT = new List<int>();

            int NUMOFELEM;

            int NBW = 0;
            int NB = 0;
            int NEL = 0;

            int TR;
            double DETA, DSI, ETA, SI, DIAG1, DIAG2;
            int KN1, KN2, KS1, KS2, neighborArea, JL, JK, K, L, NELBW;
            int neighborAreaSide = 0;
            int J1 = 0;
            int J2 = 0;
            int J3 = 0;

            /*формируем массив joinTable*/
            for (int i = 1; i <= 100; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    joinTable[i, j] = this.parent.currentFullModel.geometryModel.joinTable[i - 1, j - 1];
                }
            }

            /*формируем массив areaDefiningNodes*/
            int p = 1;
            foreach (MyArea area in this.parent.currentFullModel.geometryModel.Areas)
            {
                for (int i = 1; i <= 8; i++)
                {
                    newModel.areaDefiningNodes[i, p] = area.Nodes[i - 1].Id;
                }
                p++;
            }

            /*формируем массив XP и YP. Массивы глобальных координат точек, образующих зоны */
            List<MyPoint> nodes = new List<MyPoint>();
            foreach (MyArea area in this.parent.currentFullModel.geometryModel.Areas)
                for (int i = 1; i <= 8; i++)
                    if (nodes.IndexOf(area.Nodes[i - 1]) == -1) nodes.Add(area.Nodes[i - 1]);

            newModel.XP.Add(0);
            newModel.YP.Add(0);
            for (int i = 1; i <= nodes.Count; i++)
            {
                newModel.XP.Add(nodes[i - 1].X);
                newModel.YP.Add(nodes[i - 1].Y);
            }
            // подсчет количества элементов. 
            NUMOFELEM = NRC * NRC * zonesCount - NRC * (zonesCount - 1);

            for (int i = 0; i <= NUMOFELEM; i++)
            {
                newModel.INOUT.Add(1);
            }

            int MAXNP = 1;

            int nodeRowsCount, nodeColsCount;

            for (int i = 0; i <= NUMOFELEM * NCN * 2; i++)
            {
                CORDD.Add(0.0);
                NOP.Add(0);
            }

            for (int zoneNumber = 1; zoneNumber <= zonesCount; zoneNumber++)
            {
                Application.DoEvents();
                // рассматриваемая зона - zoneNumber

                // количество рядов и строк в будущей сетке
                nodeRowsCount = NRC;
                nodeColsCount = NRC;
                // выбираем точки, образующие текущую зону

                for (int i = 1; i <= 8; i++)
                {
                    definingNodesNumbers[i] = newModel.areaDefiningNodes[i, zoneNumber];
                }

                // генерация сетки КЭ
                // записываем координаты граничных узлов зоны в массивы areaNodes
                for (int i = 1; i <= 8; i++)
                {
                    int nodeNumber = definingNodesNumbers[i];
                    areaNodesX[i] = newModel.XP[nodeNumber];
                    areaNodesY[i] = newModel.YP[nodeNumber];
                }

                areaNodesX[9] = areaNodesX[1];
                areaNodesY[9] = areaNodesY[1];

                // вычисления шага сетки

                TR = nodeRowsCount - 1; // по сути nrc-1
                // буквы D в названии переменных обозначают шаг (по аналогии с dx и т.п.), т.е. DETA - это величина, на которую изменяется ETA
                DETA = 2.0 / TR;    // видимо, этот коэффициент определяет расстояние между узлами на сторонах зоны
                // Зона состоит из 8 узлов и за исходное расстояние принимается расстояние между двумя соседними узлами (не путать с узлами сетки!)
                // При умножении этого расстояния на коэффициенты DETA и DSI, получим реальный шаг распределения узлов.
                // например, для NRC = 5 получим DETA = DSI = 2.0/4 = 1/2, тоесть необходимо расставить узлы сетки
                // на стороне зоны с шагом 0.5 от исходного расстояния между узлами зоны
                TR = nodeColsCount - 1; // то же
                DSI = 2.0 / TR;

                for (int i = 1; i <= nodeRowsCount; i++)
                {
                    // Дальше используются хитрые коэффициенты ETA и SI. Смысл в следующем: разбивая четырехугольник на 4 части, примем координаты пересечения
                    // его центральных линий за (0; 0), координаты его граничных узлов за (-1, -1), (1, -1), (1, 1), (-1, 1). 
                    // Тогда Координаты каждого из узлов будут отличаться друг от друга на величину {±DSI, ±DETA} и получится равномерная сетка в пределах четырехгранника.
                    // Суть дальнейших махинаций - в преобразовании координат каждого узла из системы координат нашего четырехугольника
                    // к глобальным координатам
                    TR = i - 1;
                    ETA = 1.0 - TR * DETA; // координата y текущего узла в системе координат четырехугольника 
                    for (int j = 1; j <= nodeColsCount; j++)
                    {
                        TR = j - 1;
                        SI = -1.0 + TR * DSI;// координата x текущего узла в системе координат четырехугольника
                        // Функции формы квадратичного четырехугольника. Будут использоватсья для получения угловых координат элементов
                        // Комментарии по поводу того, откуда это все берется, заняли бы больше, чем сама подпрограмма.
                        // Жаждущие знаний могут обратиться к циклу статей http://www.exponenta.ru/soft/mathemat/pinega/a1/a1.asp
                        formFunctions[1] = -0.25 * (1.0 - SI) * (1.0 - ETA) * (SI + ETA + 1.0);
                        formFunctions[2] = 0.50 * (1.0 - SI * SI) * (1.0 - ETA);
                        formFunctions[3] = 0.25 * (1.0 + SI) * (1.0 - ETA) * (SI - ETA - 1.0);
                        formFunctions[4] = 0.50 * (1.0 + SI) * (1.0 - ETA * ETA);
                        formFunctions[5] = 0.25 * (1.0 + SI) * (1.0 + ETA) * (SI + ETA - 1.0);
                        formFunctions[6] = 0.50 * (1.0 - SI * SI) * (1.0 + ETA);
                        formFunctions[7] = .25 * (1.0 - SI) * (1.0 + ETA) * (ETA - SI - 1.0);
                        formFunctions[8] = 0.50 * (1.0 - SI) * (1.0 - ETA * ETA);
                        // обнуление начальных координат определяемого узла
                        xCord[i, j] = 0.0;
                        yCord[i, j] = 0.0;

                        // вычисляются координаты узлов сетки
                        // в формировании координаты нового узла участвуют все восемь
                        // базовых (образующих зону) узлов, координаты каждого из которых 
                        // умножаются на коэффицент, соответсвующий номеру узла в зоне 
                        // по сути - афинное преобразование координат.
                        for (int k = 1; k <= 8; k++)
                        {
                            xCord[i, j] = xCord[i, j] + areaNodesX[k] * formFunctions[k];
                            yCord[i, j] = yCord[i, j] + areaNodesY[k] * formFunctions[k];
                        }
                        Application.DoEvents();
                    }
                }

                KN1 = 1;
                KS1 = 1;
                KN2 = nodeRowsCount;
                KS2 = nodeColsCount;

                for (int i = 1; i <= 4; i++)
                {
                    // перебор связей текущей зоны с другими
                    neighborArea = joinTable[zoneNumber, i];

                    // На каждом этапе нас интересуют только те зоны, граничащие с данной, которые уже разбиты на КЭ
                    // Отсюда условие: если со стороны i зона не граничит с другими зонами (neighborArea = 0)
                    // или номер смежной зоны больше чем рассматриваемой, переходим к следующей стороне
                    if (neighborArea == 0 || neighborArea > zoneNumber)
                        continue;
                    else
                    {
                        for (int j = 1; j <= 4; j++)
                        {
                            // запоминаем номер стороны соседней зоны, по которой она граничит с нашей
                            if (joinTable[neighborArea, j] == zoneNumber) neighborAreaSide = j;
                        }
                        K = nodeColsCount;
                        if (i == 2 || i == 4) K = nodeRowsCount;
                        JL = 1;
                        // I - номер смежной стороны рассм зоны,
                        // neighborAreaSide - номер смежной стороны текущей зоны
                        JK = ICOMP[i, neighborAreaSide];
                        if (JK == -1) JL = K;

                        for (int j = 1; j <= K; j++)
                        {
                            switch (i)
                            {
                                case 1:
                                    nodeNumbers[nodeRowsCount, j] = boundaryNodesNumbers[neighborArea, neighborAreaSide, JL];
                                    // узлы на стыке двух зон - внутренние
                                    if (j != 1 && j != nodeColsCount) newModel.INOUT[nodeNumbers[nodeRowsCount, j]] = 0;
                                    KN2 = nodeRowsCount - 1;
                                    break;
                                case 2:
                                    nodeNumbers[j, nodeColsCount] = boundaryNodesNumbers[neighborArea, neighborAreaSide, JL];
                                    // узлы на стыке двух зон - внутренние
                                    if (j != 1 && j != nodeRowsCount) newModel.INOUT[nodeNumbers[j, nodeColsCount]] = 0;
                                    KS2 = nodeColsCount - 1;
                                    break;
                                case 3:
                                    nodeNumbers[1, j] = boundaryNodesNumbers[neighborArea, neighborAreaSide, JL];
                                    // узлы на стыке двух зон - внутренние
                                    if (j != 1 && j != nodeColsCount) newModel.INOUT[nodeNumbers[1, j]] = 0;
                                    KN1 = 2;
                                    break;
                                case 4:
                                    nodeNumbers[j, 1] = boundaryNodesNumbers[neighborArea, neighborAreaSide, JL];
                                    // узлы на стыке двух зон - внутренние
                                    if (j != 1 && j != nodeRowsCount) newModel.INOUT[nodeNumbers[j, 1]] = 0;
                                    KS1 = 2;
                                    break;
                            }
                            JL = JL + JK;
                        }
                    }
                }

                if (KN1 > KN2) { }
                else if (KS1 > KS2) { }
                else
                {
                    for (int i = KN1; i <= KN2; i++)
                    {
                        int j;
                        for (j = KS1; j <= KS2; j++)
                        {
                            NB = NB + 1;
                            nodeNumbers[i, j] = NB;
                        }
                    }

                    for (int i = 1; i <= nodeColsCount; i++)
                    {
                        boundaryNodesNumbers[zoneNumber, 1, i] = nodeNumbers[nodeRowsCount, i];
                        boundaryNodesNumbers[zoneNumber, 3, i] = nodeNumbers[1, i];
                    }

                    for (int i = 1; i <= nodeRowsCount; i++)
                    {
                        boundaryNodesNumbers[zoneNumber, 2, i] = nodeNumbers[i, nodeColsCount];
                        boundaryNodesNumbers[zoneNumber, 4, i] = nodeNumbers[i, 1];
                    }
                }

                /* < Определение внешних и внутренних узлов
                * формируется массив INOUT: если узел внутренний, то соответсвующий
                * элемент INOUT равен 0, если внешний - 1.
                * все неграничные узлы внутренние;
                * если сторона зоны смежна с другой зоной, узлы на этой стороне,
                * кроме угловых, тоже внутренние.
                * узлы не на границе зоны - внутренние */

                try
                {
                    for (int INODE = 2; INODE <= nodeRowsCount - 1; INODE++)
                    {
                        for (int JNODE = 2; JNODE <= nodeColsCount - 1; JNODE++)
                        {
                            newModel.INOUT[nodeNumbers[INODE, JNODE]] = 0;
                        }
                    }
                }
                catch
                {
                    return -1;
                }
                // > Определение внешних и внутренних узлов
                // > завершена генерация сетки конечных элементов

                // < определение конечных элементов (группировка узлов)

                K = 1;
                for (int i = 1; i <= nodeRowsCount; i++)
                {
                    for (int j = 1; j <= nodeColsCount; j++)
                    {
                        xElem[K] = xCord[i, j];
                        yElem[K] = yCord[i, j];
                        //ZE[K] = ZC[i,j];
                        NE[K] = nodeNumbers[i, j];

                        if (NE[K] > MAXNP) MAXNP = NE[K];
                        CORDD[(2 * (NE[K] - 1) + 1)] = xElem[K];
                        CORDD[(2 * (NE[K] - 1) + 2)] = yElem[K];
                        CORD[1] = xElem[K];
                        CORD[2] = yElem[K];
                        K = K + 1;
                    }
                }

                L = nodeRowsCount - 1;
                for (int i = 1; i <= L; i++)
                {
                    for (int j = 2; j <= nodeColsCount; j++)
                    {
                        DIAG1 = Math.Pow((Math.Pow((xCord[i, j] - xCord[i + 1, j - 1]), 2) + Math.Pow((yCord[i, j] - yCord[i + 1, j - 1]), 2)), 0.5);
                        DIAG2 = Math.Pow((Math.Pow((xCord[i + 1, j] - xCord[i, j - 1]), 2) + Math.Pow((yCord[i + 1, j] - yCord[i, j - 1]), 2)), 0.5);

                        //DIAG1 = Math.Pow((Math.Pow((xCord[i, j] - xCord[i + 1, j - 1]), 2) + Math.Pow((yCord[i, j] - yCord[i + 1, j - 1]), 2) + Math.Pow((ZC[i, j] - ZC[i + 1, j - 1]), 2)), 0.5);
                        //DIAG2 = Math.Pow((Math.Pow((xCord[i + 1, j] - xCord[i, j - 1]), 2) + Math.Pow((yCord[i + 1, j] - yCord[i, j - 1]), 2) + Math.Pow((ZC[i + 1, j] - ZC[i, j - 1]), 2)), 0.5);

                        NR[1] = nodeColsCount * i + j - 1;
                        NR[2] = nodeColsCount * i + j;
                        NR[3] = nodeColsCount * (i - 1) + j;
                        NR[4] = nodeColsCount * (i - 1) + j - 1;
                        for (int ij = 1; ij <= 2; ij++)
                        {
                            NEL = NEL + 1;
                            if ((DIAG1 / DIAG2) > 1.01)
                            {
                                J1 = NR[ij];
                                J2 = NR[ij + 1];
                                J3 = NR[4];
                            }
                            else if ((DIAG1 / DIAG2) < 1.01)
                            {
                                J1 = NR[1];
                                J2 = NR[ij + 1];
                                J3 = NR[ij + 2];
                            }
                            else if ((i + j) % 2 == 1)
                            {
                                J1 = NR[ij];
                                J2 = NR[ij + 1];
                                J3 = NR[4];
                            }

                            LB[1] = Math.Abs(NE[J1] - NE[J2]) + 1;
                            LB[2] = Math.Abs(NE[J2] - NE[J3]) + 1;
                            LB[3] = Math.Abs(NE[J1] - NE[J3]) + 1;

                            for (int ik = 1; ik <= 3; ik++)
                            {
                                if (LB[ik] <= NBW) { }
                                else
                                {
                                    NBW = LB[ik];
                                    NELBW = NEL;
                                }
                            }
                            NOP[(NCN * (NEL - 1) + 1)] = NE[J1];
                            NOP[(NCN * (NEL - 1) + 2)] = NE[J2];
                            NOP[(NCN * (NEL - 1) + 3)] = NE[J3];
                            // > завершено определение конечных элементов (группировка узлов) 
                        }
                    }
                }
            }
            fillObjectsFromArrays(newModel, CORDD, NOP, MAXNP, NEL, zonesCount);
            return 0;
        }

        private void fillObjectsFromArrays(MyFiniteElementModel model, List<double> CORDD, List<int> NOP, int MAXNP, int NEL, int zonesCount)
        {
            model.FiniteElements.Clear();
            model.Nodes.Clear();
            for (int i = 1; i <= MAXNP; i++) // MAXNP - число узлов
                model.Nodes.Add(new MyNode(CORDD[2 * (i - 1) + 1], CORDD[2 * (i - 1) + 2], i));

            model.IMAT.Clear();
            for (int i = 0; i <= NEL; i++) model.IMAT.Add(0);

            int numOfFe = 0;
            List<MyNode> tempNodes = new List<MyNode>();
            int elemsPerZone = NEL / zonesCount;
            for (int temp = 1; temp <= NEL; temp++)
            {
                double X1 = CORDD[2 * (NOP[3 * (temp - 1) + 1] - 1) + 1];
                double Y1 = CORDD[2 * (NOP[3 * (temp - 1) + 1] - 1) + 2];
                double X2 = CORDD[2 * (NOP[3 * (temp - 1) + 2] - 1) + 1];
                double Y2 = CORDD[2 * (NOP[3 * (temp - 1) + 2] - 1) + 2];
                double X3 = CORDD[2 * (NOP[3 * (temp - 1) + 3] - 1) + 1];
                double Y3 = CORDD[2 * (NOP[3 * (temp - 1) + 3] - 1) + 2];
                tempNodes.AddRange(model.Nodes.FindAll(node =>
                            Math.Abs(node.X - X1) <= 0.001 && Math.Abs(node.Y - Y1) <= 0.001 ||
                            Math.Abs(node.X - X2) <= 0.001 && Math.Abs(node.Y - Y2) <= 0.001 ||
                            Math.Abs(node.X - X3) <= 0.001 && Math.Abs(node.Y - Y3) <= 0.001));
                model.FiniteElements.Add(new MyFiniteElement(++numOfFe, 0, tempNodes, (temp - 1) / elemsPerZone));
                tempNodes.Clear();
            }

            // сохраняем число КЭ
            model.NE = NEL;

            // сохраняем NP
            model.NP = MAXNP;

            // сохраняем массив CORD
            model.CORD = new List<double>(CORDD);

            // сохраняем массив NOP
            model.NOP = new List<int>(NOP);
        }

        private void DivideIntoFE_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.clearSelection();
        }
    }
}
