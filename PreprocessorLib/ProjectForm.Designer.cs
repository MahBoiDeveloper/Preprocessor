namespace PreprocessorLib
{
    partial class ProjectForm
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Добавить точки");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Редактировать точки");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Удалить точки");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Точки", new System.Windows.Forms.TreeNode[]
            { treeNode1, treeNode2, treeNode });
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Добавить прямые линии");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Добавить окружности");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Добавить дуги");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Удалить линии");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Удалить окружности");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Линии", new System.Windows.Forms.TreeNode[]
            { treeNode5, treeNode6, treeNode7, treeNode8, treeNode9 });
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Импорт модели");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Геометрия модели", new System.Windows.Forms.TreeNode[]
            { treeNode4, treeNode10, treeNode11 });
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Добавить зоны");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Удалить зоны");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Удалить все зоны");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Редактировать узлы");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Зоны", new System.Windows.Forms.TreeNode[]
            { treeNode13, treeNode14, treeNode15, treeNode16 });
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Метод изопарам. координат");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Фронтальный метод");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Метод Делоне");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Триангуляция", new System.Windows.Forms.TreeNode[]
            { treeNode18, treeNode19, treeNode20 });
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Увеличение минимального угла");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Регуляризация");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Методы Рапперта");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Оптимизация", new System.Windows.Forms.TreeNode[]
            { treeNode22, treeNode23, treeNode24 });
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Анализ");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Добавить закрепления");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Удалить закрепления");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Закрепления", new System.Windows.Forms.TreeNode[]
            { treeNode27, treeNode28 });
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Добавить нагрузку");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Удалить нагрузку");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Нагрузки", new System.Windows.Forms.TreeNode[]
            { treeNode30, treeNode31 });
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Добавить материалы");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Редактировать материалы");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Назначение материалов");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("Материалы", new System.Windows.Forms.TreeNode[]
            { treeNode33, treeNode34, treeNode35 });
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("Экспорт в CAE Sigma");
            System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("Препроцессор", new System.Windows.Forms.TreeNode[]
            { treeNode12, treeNode17, treeNode21, treeNode25, treeNode26, treeNode29, treeNode32, treeNode36, treeNode37 });
            System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("Варианты сетки КЭ:");
            this.label3 = new System.Windows.Forms.Label();
            this.scaleField = new System.Windows.Forms.TextBox();
            this.prepTree = new System.Windows.Forms.TreeView();
            this.forceScale = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.gridVariants = new System.Windows.Forms.TreeView();
            this.label5 = new System.Windows.Forms.Label();
            this.currentGridNameBox = new System.Windows.Forms.TextBox();
            this.variant = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.centerModelButton = new System.Windows.Forms.Button();
            this.frontalMethodTimer = new System.Windows.Forms.Timer(this.components);
            this.showOnlyGeometry = new System.Windows.Forms.CheckBox();
            this.showOnlyAreas = new System.Windows.Forms.CheckBox();
            this.showOnlyFE = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.showFEMaterials = new System.Windows.Forms.CheckBox();
            this.showGrid = new System.Windows.Forms.CheckBox();
            this.showFENodes = new System.Windows.Forms.CheckBox();
            this.showFENumbers = new System.Windows.Forms.CheckBox();
            this.showPoints = new System.Windows.Forms.CheckBox();
            this.showForceValue = new System.Windows.Forms.CheckBox();
            this.showLines = new System.Windows.Forms.CheckBox();
            this.showCircles = new System.Windows.Forms.CheckBox();
            this.showBounds = new System.Windows.Forms.CheckBox();
            this.showForces = new System.Windows.Forms.CheckBox();
            this.showArcs = new System.Windows.Forms.CheckBox();
            this.scale = new System.Windows.Forms.TrackBar();
            this.plusButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.constructionArea = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.cmsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.forceScale)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scale)).BeginInit();
            this.cmsGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(366, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Масштаб: ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // scaleField
            // 
            this.scaleField.Location = new System.Drawing.Point(431, 2);
            this.scaleField.Name = "scaleField";
            this.scaleField.Size = new System.Drawing.Size(64, 20);
            this.scaleField.TabIndex = 12;
            // 
            // prepTree
            // 
            this.prepTree.BackColor = System.Drawing.SystemColors.Control;
            this.prepTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.prepTree.HideSelection = false;
            this.prepTree.Location = new System.Drawing.Point(4, 2);
            this.prepTree.Name = "prepTree";
            treeNode1.Name = "AddPoints";
            treeNode1.Text = "Добавить точки";
            treeNode2.Name = "EditPoints";
            treeNode2.Text = "Редактировать точки";
            treeNode3.Name = "DeletePoints";
            treeNode3.Text = "Удалить точки";
            treeNode4.Name = "Points";
            treeNode4.Text = "Точки";
            treeNode5.Name = "AddStraightLines";
            treeNode5.Text = "Добавить прямые линии";
            treeNode6.Name = "AddCircles";
            treeNode6.Text = "Добавить окружности";
            treeNode7.Name = "AddArcs";
            treeNode7.Text = "Добавить дуги";
            treeNode8.Name = "DeleteLines";
            treeNode8.Text = "Удалить линии";
            treeNode9.Name = "DeleteCircles";
            treeNode9.Text = "Удалить окружности";
            treeNode10.Name = "Lines";
            treeNode10.Text = "Линии";
            treeNode11.Name = "Import";
            treeNode11.Text = "Импорт модели";
            treeNode12.Name = "Geometry";
            treeNode12.Text = "Геометрия модели";
            treeNode13.Name = "AddAreas";
            treeNode13.Text = "Добавить зоны";
            treeNode14.Name = "DeleteAreas";
            treeNode14.Text = "Удалить зоны";
            treeNode15.Name = "DeleteAllAreas";
            treeNode15.Text = "Удалить все зоны";
            treeNode16.Name = "EditNodes";
            treeNode16.Text = "Редактировать узлы";
            treeNode17.Name = "Areas";
            treeNode17.Text = "Зоны";
            treeNode18.Name = "standartGrid";
            treeNode18.Text = "Метод изопарам. координат";
            treeNode19.Name = "FrontalMethod";
            treeNode19.Text = "Фронтальный метод";
            treeNode20.Name = "DelaneyTriangulate";
            treeNode20.Text = "Метод Делоне";
            treeNode21.Name = "Triangulation";
            treeNode21.Text = "Триангуляция";
            treeNode22.Name = "standartOpt";
            treeNode22.Text = "Увеличение минимального угла";
            treeNode23.Name = "Regularization";
            treeNode23.Text = "Регуляризация";
            treeNode24.Name = "RuppertOpt";
            treeNode24.Text = "Методы Рапперта";
            treeNode25.Name = "Optimisation";
            treeNode25.Text = "Оптимизация";
            treeNode26.Name = "nodeAnalysis";
            treeNode26.Text = "Анализ";
            treeNode27.Name = "AddBounds";
            treeNode27.Text = "Добавить закрепления";
            treeNode28.Name = "DeleteBounds";
            treeNode28.Text = "Удалить закрепления";
            treeNode29.Name = "Bounds";
            treeNode29.Text = "Закрепления";
            treeNode30.Name = "Forces";
            treeNode30.Text = "Добавить нагрузку";
            treeNode31.Name = "DeleteForces";
            treeNode31.Text = "Удалить нагрузку";
            treeNode32.Name = "Forces2";
            treeNode32.Text = "Нагрузки";
            treeNode33.Name = "AddMaterials";
            treeNode33.Text = "Добавить материалы";
            treeNode34.Name = "EditMaterials";
            treeNode34.Text = "Редактировать материалы";
            treeNode35.Name = "SetMaterials";
            treeNode35.Text = "Назначение материалов";
            treeNode36.Name = "Materials";
            treeNode36.Text = "Материалы";
            treeNode37.Name = "SigmaExport";
            treeNode37.Text = "Экспорт в CAE Sigma";
            treeNode38.Name = "Preprocessor";
            treeNode38.Text = "Препроцессор";
            this.prepTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode38});
            this.prepTree.Size = new System.Drawing.Size(277, 266);
            this.prepTree.TabIndex = 13;
            this.prepTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.prepTree_AfterSelect);
            this.prepTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.prepTree_NodeMouseClick);
            // 
            // forceScale
            // 
            this.forceScale.Location = new System.Drawing.Point(935, 2);
            this.forceScale.Maximum = 50;
            this.forceScale.Name = "forceScale";
            this.forceScale.Size = new System.Drawing.Size(116, 45);
            this.forceScale.TabIndex = 30;
            this.forceScale.TickStyle = System.Windows.Forms.TickStyle.None;
            this.forceScale.Value = 25;
            this.forceScale.ValueChanged += new System.EventHandler(this.forceScale_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(808, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Масштаб нагрузки: ";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(922, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 20);
            this.button2.TabIndex = 33;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1044, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(20, 20);
            this.button3.TabIndex = 32;
            this.button3.Text = "+";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // gridVariants
            // 
            this.gridVariants.BackColor = System.Drawing.SystemColors.Control;
            this.gridVariants.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridVariants.Location = new System.Drawing.Point(7, 717);
            this.gridVariants.Name = "gridVariants";
            treeNode39.Name = "Node0";
            treeNode39.Text = "Варианты сетки КЭ:";
            this.gridVariants.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode39});
            this.gridVariants.Size = new System.Drawing.Size(240, 81);
            this.gridVariants.TabIndex = 37;
            this.gridVariants.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridVariants_MouseDoubleClick);
            this.gridVariants.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridVariants_MouseUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 701);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "КЭ сетка:";
            // 
            // currentGridNameBox
            // 
            this.currentGridNameBox.Location = new System.Drawing.Point(88, 698);
            this.currentGridNameBox.Name = "currentGridNameBox";
            this.currentGridNameBox.Size = new System.Drawing.Size(91, 20);
            this.currentGridNameBox.TabIndex = 39;
            // 
            // variant
            // 
            this.variant.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.variant.Location = new System.Drawing.Point(345, 4);
            this.variant.Name = "variant";
            this.variant.Size = new System.Drawing.Size(18, 18);
            this.variant.TabIndex = 41;
            this.variant.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(287, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Вариант:";
            // 
            // centerModelButton
            // 
            this.centerModelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.centerModelButton.Location = new System.Drawing.Point(657, 2);
            this.centerModelButton.Name = "centerModelButton";
            this.centerModelButton.Size = new System.Drawing.Size(145, 25);
            this.centerModelButton.TabIndex = 42;
            this.centerModelButton.Text = "Расположить по центру ";
            this.centerModelButton.UseVisualStyleBackColor = true;
            this.centerModelButton.Click += new System.EventHandler(this.centerModelButton_Click);
            // 
            // frontalMethodTimer
            // 
            this.frontalMethodTimer.Interval = 1000;
            // 
            // showOnlyGeometry
            // 
            this.showOnlyGeometry.AutoSize = true;
            this.showOnlyGeometry.Checked = true;
            this.showOnlyGeometry.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showOnlyGeometry.Location = new System.Drawing.Point(6, 19);
            this.showOnlyGeometry.Name = "showOnlyGeometry";
            this.showOnlyGeometry.Size = new System.Drawing.Size(81, 17);
            this.showOnlyGeometry.TabIndex = 50;
            this.showOnlyGeometry.Text = "Геометрия";
            this.showOnlyGeometry.UseVisualStyleBackColor = true;
            this.showOnlyGeometry.CheckedChanged += new System.EventHandler(this.showOnlyGeometry_CheckedChanged);
            // 
            // showOnlyAreas
            // 
            this.showOnlyAreas.AutoSize = true;
            this.showOnlyAreas.Checked = true;
            this.showOnlyAreas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showOnlyAreas.Location = new System.Drawing.Point(6, 159);
            this.showOnlyAreas.Name = "showOnlyAreas";
            this.showOnlyAreas.Size = new System.Drawing.Size(53, 17);
            this.showOnlyAreas.TabIndex = 51;
            this.showOnlyAreas.Text = "Зоны";
            this.showOnlyAreas.UseVisualStyleBackColor = true;
            this.showOnlyAreas.CheckedChanged += new System.EventHandler(this.showOnlyAreas_CheckedChanged);
            // 
            // showOnlyFE
            // 
            this.showOnlyFE.AutoSize = true;
            this.showOnlyFE.Checked = true;
            this.showOnlyFE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showOnlyFE.Location = new System.Drawing.Point(143, 19);
            this.showOnlyFE.Name = "showOnlyFE";
            this.showOnlyFE.Size = new System.Drawing.Size(40, 17);
            this.showOnlyFE.TabIndex = 52;
            this.showOnlyFE.Text = "КЭ";
            this.showOnlyFE.UseVisualStyleBackColor = true;
            this.showOnlyFE.CheckedChanged += new System.EventHandler(this.showOnlyFE_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.showOnlyAreas);
            this.groupBox1.Controls.Add(this.showOnlyFE);
            this.groupBox1.Controls.Add(this.showOnlyGeometry);
            this.groupBox1.Controls.Add(this.showFEMaterials);
            this.groupBox1.Controls.Add(this.showGrid);
            this.groupBox1.Controls.Add(this.showFENodes);
            this.groupBox1.Controls.Add(this.showFENumbers);
            this.groupBox1.Controls.Add(this.showPoints);
            this.groupBox1.Controls.Add(this.showForceValue);
            this.groupBox1.Controls.Add(this.showLines);
            this.groupBox1.Controls.Add(this.showCircles);
            this.groupBox1.Controls.Add(this.showBounds);
            this.groupBox1.Controls.Add(this.showForces);
            this.groupBox1.Controls.Add(this.showArcs);
            this.groupBox1.Location = new System.Drawing.Point(11, 497);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 195);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // showFEMaterials
            // 
            this.showFEMaterials.AutoSize = true;
            this.showFEMaterials.Checked = true;
            this.showFEMaterials.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFEMaterials.Location = new System.Drawing.Point(148, 88);
            this.showFEMaterials.Name = "showFEMaterials";
            this.showFEMaterials.Size = new System.Drawing.Size(102, 17);
            this.showFEMaterials.TabIndex = 67;
            this.showFEMaterials.Text = "номер свойств";
            this.showFEMaterials.UseVisualStyleBackColor = true;
            this.showFEMaterials.CheckedChanged += new System.EventHandler(this.showFEMaterials_CheckedChanged);
            // 
            // showGrid
            // 
            this.showGrid.AutoSize = true;
            this.showGrid.Checked = true;
            this.showGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showGrid.Location = new System.Drawing.Point(6, 136);
            this.showGrid.Name = "showGrid";
            this.showGrid.Size = new System.Drawing.Size(130, 17);
            this.showGrid.TabIndex = 55;
            this.showGrid.Text = "Координатная сетка";
            this.showGrid.UseVisualStyleBackColor = true;
            this.showGrid.CheckedChanged += new System.EventHandler(this.showGrid_CheckedChanged);
            // 
            // showFENodes
            // 
            this.showFENodes.AutoSize = true;
            this.showFENodes.Checked = true;
            this.showFENodes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFENodes.Location = new System.Drawing.Point(148, 65);
            this.showFENodes.Name = "showFENodes";
            this.showFENodes.Size = new System.Drawing.Size(96, 17);
            this.showFENodes.TabIndex = 66;
            this.showFENodes.Text = "номера узлов";
            this.showFENodes.UseVisualStyleBackColor = true;
            this.showFENodes.CheckedChanged += new System.EventHandler(this.showFENodes_CheckedChanged);
            // 
            // showFENumbers
            // 
            this.showFENumbers.AutoSize = true;
            this.showFENumbers.Checked = true;
            this.showFENumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFENumbers.Location = new System.Drawing.Point(148, 42);
            this.showFENumbers.Name = "showFENumbers";
            this.showFENumbers.Size = new System.Drawing.Size(81, 17);
            this.showFENumbers.TabIndex = 65;
            this.showFENumbers.Text = "номера КЭ";
            this.showFENumbers.UseVisualStyleBackColor = true;
            this.showFENumbers.CheckedChanged += new System.EventHandler(this.showFENumbers_CheckedChanged);
            // 
            // showPoints
            // 
            this.showPoints.AutoSize = true;
            this.showPoints.Checked = true;
            this.showPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showPoints.Location = new System.Drawing.Point(13, 43);
            this.showPoints.Name = "showPoints";
            this.showPoints.Size = new System.Drawing.Size(56, 17);
            this.showPoints.TabIndex = 56;
            this.showPoints.Text = "Точки";
            this.showPoints.UseVisualStyleBackColor = true;
            this.showPoints.CheckedChanged += new System.EventHandler(this.showPoints_CheckedChanged);
            // 
            // showForceValue
            // 
            this.showForceValue.AutoSize = true;
            this.showForceValue.Checked = true;
            this.showForceValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showForceValue.Location = new System.Drawing.Point(148, 157);
            this.showForceValue.Name = "showForceValue";
            this.showForceValue.Size = new System.Drawing.Size(74, 17);
            this.showForceValue.TabIndex = 64;
            this.showForceValue.Text = "Значения";
            this.showForceValue.UseVisualStyleBackColor = true;
            this.showForceValue.CheckedChanged += new System.EventHandler(this.showForceValue_CheckedChanged);
            // 
            // showLines
            // 
            this.showLines.AutoSize = true;
            this.showLines.Checked = true;
            this.showLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showLines.Location = new System.Drawing.Point(13, 67);
            this.showLines.Name = "showLines";
            this.showLines.Size = new System.Drawing.Size(58, 17);
            this.showLines.TabIndex = 57;
            this.showLines.Text = "Линии";
            this.showLines.UseVisualStyleBackColor = true;
            this.showLines.CheckedChanged += new System.EventHandler(this.showLines_CheckedChanged);
            // 
            // showCircles
            // 
            this.showCircles.AutoSize = true;
            this.showCircles.Checked = true;
            this.showCircles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showCircles.Location = new System.Drawing.Point(13, 111);
            this.showCircles.Name = "showCircles";
            this.showCircles.Size = new System.Drawing.Size(88, 17);
            this.showCircles.TabIndex = 58;
            this.showCircles.Text = "Окружности";
            this.showCircles.UseVisualStyleBackColor = true;
            this.showCircles.CheckedChanged += new System.EventHandler(this.showCircles_CheckedChanged);
            // 
            // showBounds
            // 
            this.showBounds.AutoSize = true;
            this.showBounds.Checked = true;
            this.showBounds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showBounds.Location = new System.Drawing.Point(143, 111);
            this.showBounds.Name = "showBounds";
            this.showBounds.Size = new System.Drawing.Size(93, 17);
            this.showBounds.TabIndex = 62;
            this.showBounds.Text = "Закрепления";
            this.showBounds.UseVisualStyleBackColor = true;
            this.showBounds.CheckedChanged += new System.EventHandler(this.showBounds_CheckedChanged);
            // 
            // showForces
            // 
            this.showForces.AutoSize = true;
            this.showForces.Checked = true;
            this.showForces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showForces.Location = new System.Drawing.Point(143, 134);
            this.showForces.Name = "showForces";
            this.showForces.Size = new System.Drawing.Size(74, 17);
            this.showForces.TabIndex = 63;
            this.showForces.Text = "Нагрузки";
            this.showForces.UseVisualStyleBackColor = true;
            this.showForces.CheckedChanged += new System.EventHandler(this.showForces_CheckedChanged);
            // 
            // showArcs
            // 
            this.showArcs.AutoSize = true;
            this.showArcs.Checked = true;
            this.showArcs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showArcs.Location = new System.Drawing.Point(13, 88);
            this.showArcs.Name = "showArcs";
            this.showArcs.Size = new System.Drawing.Size(51, 17);
            this.showArcs.TabIndex = 59;
            this.showArcs.Text = "Дуги";
            this.showArcs.UseVisualStyleBackColor = true;
            this.showArcs.CheckedChanged += new System.EventHandler(this.showArcs_CheckedChanged);
            // 
            // scale
            // 
            this.scale.Location = new System.Drawing.Point(516, 4);
            this.scale.Maximum = 400;
            this.scale.Name = "scale";
            this.scale.Size = new System.Drawing.Size(112, 45);
            this.scale.TabIndex = 10;
            this.scale.TickStyle = System.Windows.Forms.TickStyle.None;
            this.scale.Value = 200;
            this.scale.ValueChanged += new System.EventHandler(this.scale_ValueChanged);
            // 
            // plusButton
            // 
            this.plusButton.Location = new System.Drawing.Point(621, 4);
            this.plusButton.Name = "plusButton";
            this.plusButton.Size = new System.Drawing.Size(20, 20);
            this.plusButton.TabIndex = 70;
            this.plusButton.Text = "+";
            this.plusButton.UseVisualStyleBackColor = true;
            this.plusButton.Click += new System.EventHandler(this.plusButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(501, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 71;
            this.button1.Text = "-";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.minusButton_Click);
            // 
            // constructionArea
            // 
            this.constructionArea.AccumBits = ((byte)(0));
            this.constructionArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.constructionArea.AutoCheckErrors = false;
            this.constructionArea.AutoFinish = false;
            this.constructionArea.AutoMakeCurrent = false;
            this.constructionArea.AutoSwapBuffers = false;
            this.constructionArea.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.constructionArea.BackColor = System.Drawing.Color.White;
            this.constructionArea.ColorBits = ((byte)(32));
            this.constructionArea.DepthBits = ((byte)(16));
            this.constructionArea.ForeColor = System.Drawing.Color.White;
            this.constructionArea.Location = new System.Drawing.Point(291, 33);
            this.constructionArea.Name = "constructionArea";
            this.constructionArea.Size = new System.Drawing.Size(942, 773);
            this.constructionArea.StencilBits = ((byte)(0));
            this.constructionArea.TabIndex = 72;
            this.constructionArea.Load += new System.EventHandler(this.constructionArea_Load);
            this.constructionArea.MouseClick += new System.Windows.Forms.MouseEventHandler(this.constructionArea_MouseClick);
            this.constructionArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.constructionArea_MouseDown);
            this.constructionArea.MouseLeave += new System.EventHandler(this.constructionArea_MouseLeave);
            this.constructionArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.constructionArea_MouseMove);
            this.constructionArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.constructionArea_MouseUp);
            this.constructionArea.Resize += new System.EventHandler(this.constructionArea_Resize);
            // 
            // cmsGrid
            // 
            this.cmsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDelete});
            this.cmsGrid.Name = "cmsGrid";
            this.cmsGrid.Size = new System.Drawing.Size(119, 26);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(118, 22);
            this.tsmiDelete.Text = "Удалить";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1172, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(50, 20);
            this.textBox2.TabIndex = 76;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1093, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(50, 20);
            this.textBox1.TabIndex = 73;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1149, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 75;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1070, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 74;
            this.label1.Text = "X:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 497);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(243, 13);
            this.label9.TabIndex = 79;
            this.label9.Text = "Графическое отображение элементов модели";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // ProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1234, 811);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.constructionArea);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.plusButton);
            this.Controls.Add(this.variant);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.centerModelButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.prepTree);
            this.Controls.Add(this.currentGridNameBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gridVariants);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.forceScale);
            this.Controls.Add(this.scale);
            this.Controls.Add(this.scaleField);
            this.Controls.Add(this.label3);
            this.MinimizeBox = false;
            this.Name = "ProjectForm";
            this.Activated += new System.EventHandler(this.ProjectForm_Activated);
            this.Deactivate += new System.EventHandler(this.ProjectForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProjectForm_FormClosing);
            this.Load += new System.EventHandler(this.ProjectForm_Load);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.ProjectForm_ControlRemoved);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ProjectForm_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ProjectForm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.forceScale)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scale)).EndInit();
            this.cmsGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox scaleField;
        private System.Windows.Forms.TreeView prepTree;
        private System.Windows.Forms.TrackBar forceScale;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.TreeView gridVariants;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox currentGridNameBox;
        private System.Windows.Forms.TextBox variant;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button centerModelButton;
        private System.Windows.Forms.Timer frontalMethodTimer;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox showFEMaterials;
        public System.Windows.Forms.CheckBox showFENodes;
        public System.Windows.Forms.CheckBox showGrid;
        public System.Windows.Forms.CheckBox showFENumbers;
        public System.Windows.Forms.CheckBox showPoints;
        public System.Windows.Forms.CheckBox showForceValue;
        public System.Windows.Forms.CheckBox showLines;
        public System.Windows.Forms.CheckBox showForces;
        public System.Windows.Forms.CheckBox showCircles;
        public System.Windows.Forms.CheckBox showBounds;
        public System.Windows.Forms.CheckBox showArcs;
        public System.Windows.Forms.CheckBox showOnlyGeometry;
        public System.Windows.Forms.CheckBox showOnlyAreas;
        public System.Windows.Forms.CheckBox showOnlyFE;
        private System.Windows.Forms.TrackBar scale;
        private System.Windows.Forms.Button plusButton;
        private System.Windows.Forms.Button button1;
        public Tao.Platform.Windows.SimpleOpenGlControl constructionArea;
        private System.Windows.Forms.ContextMenuStrip cmsGrid;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
    }
}
