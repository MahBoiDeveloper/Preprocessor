namespace FrontalMethod
{
    partial class MethodForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MethodForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.showGrid = new System.Windows.Forms.CheckBox();
            this.showFEnumbers = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.stepValue = new System.Windows.Forms.TextBox();
            this.speedScrollBar = new System.Windows.Forms.HScrollBar();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.funcValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.drawArea = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.settingsButton = new System.Windows.Forms.Button();
            this.gridBox = new System.Windows.Forms.GroupBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.buildButton = new System.Windows.Forms.Button();
            this.nodesBox = new System.Windows.Forms.GroupBox();
            this.defaultNodes = new System.Windows.Forms.Button();
            this.deleteNodeButton = new System.Windows.Forms.Button();
            this.addNodeButton = new System.Windows.Forms.Button();
            this.stepOkButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.demoBox = new System.Windows.Forms.GroupBox();
            this.buildDelay = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.demoButton = new System.Windows.Forms.Button();
            this.saveAndExitButton = new System.Windows.Forms.Button();
            this.densityBox = new System.Windows.Forms.GroupBox();
            this.Radius = new System.Windows.Forms.Label();
            this.funcRadius = new System.Windows.Forms.TextBox();
            this.deleteFuncButton = new System.Windows.Forms.Button();
            this.addFuncPoint = new System.Windows.Forms.Button();
            this.coordY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.coordX = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.gridName = new System.Windows.Forms.TextBox();
            this.visualBox = new System.Windows.Forms.GroupBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.gridBox.SuspendLayout();
            this.nodesBox.SuspendLayout();
            this.demoBox.SuspendLayout();
            this.densityBox.SuspendLayout();
            this.visualBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            this.toolTip1.AutomaticDelay = 2000;
            this.toolTip1.AutoPopDelay = 2000;
            this.toolTip1.InitialDelay = 1000;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.OwnerDraw = true;
            this.toolTip1.ReshowDelay = 2000;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Параметры треугольника";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            // 
            // showGrid
            // 
            this.showGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showGrid.Checked = true;
            this.showGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showGrid.Location = new System.Drawing.Point(22, 148);
            this.showGrid.Name = "showGrid";
            this.showGrid.Size = new System.Drawing.Size(108, 43);
            this.showGrid.TabIndex = 19;
            this.showGrid.Text = "Показать сетку";
            this.toolTip2.SetToolTip(this.showGrid, "Отобразить координаты узлов, составляющих зоны");
            this.showGrid.UseVisualStyleBackColor = true;
            this.showGrid.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged_1);
            // 
            // showFEnumbers
            // 
            this.showFEnumbers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showFEnumbers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showFEnumbers.Location = new System.Drawing.Point(22, 32);
            this.showFEnumbers.Name = "showFEnumbers";
            this.showFEnumbers.Size = new System.Drawing.Size(105, 31);
            this.showFEnumbers.TabIndex = 17;
            this.showFEnumbers.Text = "Выводить номера КЭ";
            this.toolTip2.SetToolTip(this.showFEnumbers, "Отобразить номера КЭ");
            this.showFEnumbers.UseVisualStyleBackColor = true;
            this.showFEnumbers.CheckedChanged += new System.EventHandler(this.showFEnumbers_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox2.Location = new System.Drawing.Point(22, 89);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(108, 43);
            this.checkBox2.TabIndex = 18;
            this.checkBox2.Text = "Выводить номера узлов зон";
            this.toolTip2.SetToolTip(this.checkBox2, "Отобразить номера узлов, составляющих зоны");
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged_1);
            // 
            // stepValue
            // 
            this.stepValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stepValue.Location = new System.Drawing.Point(48, 74);
            this.stepValue.Name = "stepValue";
            this.stepValue.Size = new System.Drawing.Size(61, 20);
            this.stepValue.TabIndex = 15;
            this.stepValue.Text = "5";
            this.toolTip2.SetToolTip(this.stepValue, "Шаг равномерного разбиения сторон зон");
            // 
            // speedScrollBar
            // 
            this.speedScrollBar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.speedScrollBar.Location = new System.Drawing.Point(18, 45);
            this.speedScrollBar.Maximum = 2000;
            this.speedScrollBar.Minimum = 50;
            this.speedScrollBar.Name = "speedScrollBar";
            this.speedScrollBar.Size = new System.Drawing.Size(197, 17);
            this.speedScrollBar.TabIndex = 80;
            this.speedScrollBar.TabStop = true;
            this.toolTip2.SetToolTip(this.speedScrollBar, "Задержка для демонстрационного построения сетки КЭ");
            this.speedScrollBar.Value = 1000;
            this.speedScrollBar.ValueChanged += new System.EventHandler(this.speedScrollBar_ValueChanged);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // funcValue
            // 
            this.funcValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.funcValue.Location = new System.Drawing.Point(57, 28);
            this.funcValue.Name = "funcValue";
            this.funcValue.Size = new System.Drawing.Size(52, 20);
            this.funcValue.TabIndex = 12;
            this.funcValue.Text = "3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "f(x,y)=";
            // 
            // drawArea
            // 
            this.drawArea.AccumBits = ((byte)(0));
            this.drawArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.drawArea.AutoCheckErrors = false;
            this.drawArea.AutoFinish = false;
            this.drawArea.AutoMakeCurrent = false;
            this.drawArea.AutoSwapBuffers = false;
            this.drawArea.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.drawArea.BackColor = System.Drawing.Color.White;
            this.drawArea.ColorBits = ((byte)(32));
            this.drawArea.DepthBits = ((byte)(16));
            this.drawArea.ForeColor = System.Drawing.Color.White;
            this.drawArea.Location = new System.Drawing.Point(274, 40);
            this.drawArea.Name = "drawArea";
            this.drawArea.Size = new System.Drawing.Size(584, 540);
            this.drawArea.StencilBits = ((byte)(0));
            this.drawArea.TabIndex = 73;
            this.drawArea.Load += new System.EventHandler(this.drawArea_Load);
            this.drawArea.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drawArea_MouseClick);
            this.drawArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawArea_MouseMove);
            this.drawArea.Resize += new System.EventHandler(this.drawArea_Resize);
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(133, 32);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(93, 23);
            this.settingsButton.TabIndex = 74;
            this.settingsButton.Text = "Настройки";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // gridBox
            // 
            this.gridBox.Controls.Add(this.deleteButton);
            this.gridBox.Controls.Add(this.buildButton);
            this.gridBox.Controls.Add(this.settingsButton);
            this.gridBox.Location = new System.Drawing.Point(12, 40);
            this.gridBox.Name = "gridBox";
            this.gridBox.Size = new System.Drawing.Size(235, 120);
            this.gridBox.TabIndex = 81;
            this.gridBox.TabStop = false;
            this.gridBox.Text = "Сетка";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(16, 75);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(93, 23);
            this.deleteButton.TabIndex = 80;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // buildButton
            // 
            this.buildButton.Location = new System.Drawing.Point(16, 32);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(93, 23);
            this.buildButton.TabIndex = 79;
            this.buildButton.Text = "Построить";
            this.buildButton.UseVisualStyleBackColor = true;
            this.buildButton.Click += new System.EventHandler(this.buildButton_Click);
            // 
            // nodesBox
            // 
            this.nodesBox.Controls.Add(this.defaultNodes);
            this.nodesBox.Controls.Add(this.deleteNodeButton);
            this.nodesBox.Controls.Add(this.addNodeButton);
            this.nodesBox.Controls.Add(this.stepOkButton);
            this.nodesBox.Controls.Add(this.stepValue);
            this.nodesBox.Controls.Add(this.label5);
            this.nodesBox.Location = new System.Drawing.Point(12, 392);
            this.nodesBox.Name = "nodesBox";
            this.nodesBox.Size = new System.Drawing.Size(235, 179);
            this.nodesBox.TabIndex = 82;
            this.nodesBox.TabStop = false;
            this.nodesBox.Text = "Узлы";
            // 
            // defaultNodes
            // 
            this.defaultNodes.Location = new System.Drawing.Point(18, 144);
            this.defaultNodes.Name = "defaultNodes";
            this.defaultNodes.Size = new System.Drawing.Size(197, 23);
            this.defaultNodes.TabIndex = 87;
            this.defaultNodes.Text = "Исходная конфигурация";
            this.defaultNodes.UseVisualStyleBackColor = true;
            this.defaultNodes.Click += new System.EventHandler(this.defaultNodes_Click);
            // 
            // deleteNodeButton
            // 
            this.deleteNodeButton.Location = new System.Drawing.Point(129, 22);
            this.deleteNodeButton.Name = "deleteNodeButton";
            this.deleteNodeButton.Size = new System.Drawing.Size(93, 23);
            this.deleteNodeButton.TabIndex = 86;
            this.deleteNodeButton.Text = "Удалить";
            this.deleteNodeButton.UseVisualStyleBackColor = true;
            this.deleteNodeButton.Click += new System.EventHandler(this.deleteNodeButton_Click);
            // 
            // addNodeButton
            // 
            this.addNodeButton.Location = new System.Drawing.Point(18, 22);
            this.addNodeButton.Name = "addNodeButton";
            this.addNodeButton.Size = new System.Drawing.Size(93, 23);
            this.addNodeButton.TabIndex = 85;
            this.addNodeButton.Text = "Добавить";
            this.addNodeButton.UseVisualStyleBackColor = true;
            this.addNodeButton.Click += new System.EventHandler(this.addNodeButton_Click);
            // 
            // stepOkButton
            // 
            this.stepOkButton.Location = new System.Drawing.Point(18, 105);
            this.stepOkButton.Name = "stepOkButton";
            this.stepOkButton.Size = new System.Drawing.Size(197, 23);
            this.stepOkButton.TabIndex = 83;
            this.stepOkButton.Text = "Равномерное распределение";
            this.stepOkButton.UseVisualStyleBackColor = true;
            this.stepOkButton.Click += new System.EventHandler(this.stepOkButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Шаг";
            // 
            // demoBox
            // 
            this.demoBox.Controls.Add(this.buildDelay);
            this.demoBox.Controls.Add(this.label1);
            this.demoBox.Controls.Add(this.demoButton);
            this.demoBox.Controls.Add(this.speedScrollBar);
            this.demoBox.Location = new System.Drawing.Point(12, 166);
            this.demoBox.Name = "demoBox";
            this.demoBox.Size = new System.Drawing.Size(235, 119);
            this.demoBox.TabIndex = 83;
            this.demoBox.TabStop = false;
            this.demoBox.Text = "Демонстрация";
            // 
            // buildDelay
            // 
            this.buildDelay.AutoSize = true;
            this.buildDelay.Location = new System.Drawing.Point(173, 22);
            this.buildDelay.Name = "buildDelay";
            this.buildDelay.Size = new System.Drawing.Size(22, 13);
            this.buildDelay.TabIndex = 104;
            this.buildDelay.Text = "1 с";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 84;
            this.label1.Text = "Задержка построения сетки:";
            // 
            // demoButton
            // 
            this.demoButton.Location = new System.Drawing.Point(70, 75);
            this.demoButton.Name = "demoButton";
            this.demoButton.Size = new System.Drawing.Size(93, 23);
            this.demoButton.TabIndex = 81;
            this.demoButton.Text = "Построить";
            this.demoButton.UseVisualStyleBackColor = true;
            this.demoButton.Click += new System.EventHandler(this.demoButton_Click);
            // 
            // saveAndExitButton
            // 
            this.saveAndExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveAndExitButton.Location = new System.Drawing.Point(937, 322);
            this.saveAndExitButton.Name = "saveAndExitButton";
            this.saveAndExitButton.Size = new System.Drawing.Size(99, 24);
            this.saveAndExitButton.TabIndex = 84;
            this.saveAndExitButton.Text = "Сохранить сетку";
            this.saveAndExitButton.UseVisualStyleBackColor = true;
            this.saveAndExitButton.Click += new System.EventHandler(this.saveAndExitButton_Click);
            // 
            // densityBox
            // 
            this.densityBox.Controls.Add(this.Radius);
            this.densityBox.Controls.Add(this.funcRadius);
            this.densityBox.Controls.Add(this.deleteFuncButton);
            this.densityBox.Controls.Add(this.addFuncPoint);
            this.densityBox.Controls.Add(this.label4);
            this.densityBox.Controls.Add(this.funcValue);
            this.densityBox.Location = new System.Drawing.Point(12, 292);
            this.densityBox.Name = "densityBox";
            this.densityBox.Size = new System.Drawing.Size(235, 94);
            this.densityBox.TabIndex = 85;
            this.densityBox.TabStop = false;
            this.densityBox.Text = "Функция плотности";
            // 
            // Radius
            // 
            this.Radius.AutoSize = true;
            this.Radius.Location = new System.Drawing.Point(128, 30);
            this.Radius.Name = "Radius";
            this.Radius.Size = new System.Drawing.Size(43, 13);
            this.Radius.TabIndex = 18;
            this.Radius.Text = "Радиус";
            // 
            // funcRadius
            // 
            this.funcRadius.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.funcRadius.Location = new System.Drawing.Point(177, 28);
            this.funcRadius.Name = "funcRadius";
            this.funcRadius.Size = new System.Drawing.Size(52, 20);
            this.funcRadius.TabIndex = 17;
            this.funcRadius.Text = "5";
            // 
            // deleteFuncButton
            // 
            this.deleteFuncButton.Location = new System.Drawing.Point(131, 54);
            this.deleteFuncButton.Name = "deleteFuncButton";
            this.deleteFuncButton.Size = new System.Drawing.Size(91, 23);
            this.deleteFuncButton.TabIndex = 16;
            this.deleteFuncButton.Text = "Удалить";
            this.deleteFuncButton.UseVisualStyleBackColor = true;
            this.deleteFuncButton.Click += new System.EventHandler(this.deleteFuncButton_Click);
            // 
            // addFuncPoint
            // 
            this.addFuncPoint.Location = new System.Drawing.Point(18, 56);
            this.addFuncPoint.Name = "addFuncPoint";
            this.addFuncPoint.Size = new System.Drawing.Size(91, 23);
            this.addFuncPoint.TabIndex = 15;
            this.addFuncPoint.Text = "Добавить";
            this.addFuncPoint.UseVisualStyleBackColor = true;
            this.addFuncPoint.Click += new System.EventHandler(this.addFuncPoint_Click);
            // 
            // coordY
            // 
            this.coordY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.coordY.Location = new System.Drawing.Point(789, 15);
            this.coordY.Name = "coordY";
            this.coordY.Size = new System.Drawing.Size(69, 20);
            this.coordY.TabIndex = 89;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(766, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 88;
            this.label6.Text = "Y:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(668, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 87;
            this.label7.Text = "X:";
            // 
            // coordX
            // 
            this.coordX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.coordX.Location = new System.Drawing.Point(691, 15);
            this.coordX.Name = "coordX";
            this.coordX.Size = new System.Drawing.Size(69, 20);
            this.coordX.TabIndex = 86;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(286, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 100;
            this.label9.Text = "Имя сетки:";
            // 
            // gridName
            // 
            this.gridName.Location = new System.Drawing.Point(356, 15);
            this.gridName.Name = "gridName";
            this.gridName.Size = new System.Drawing.Size(198, 20);
            this.gridName.TabIndex = 101;
            this.gridName.Text = "Фронтальный метод 1";
            // 
            // visualBox
            // 
            this.visualBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.visualBox.Controls.Add(this.showFEnumbers);
            this.visualBox.Controls.Add(this.checkBox2);
            this.visualBox.Controls.Add(this.showGrid);
            this.visualBox.Location = new System.Drawing.Point(887, 40);
            this.visualBox.Name = "visualBox";
            this.visualBox.Size = new System.Drawing.Size(201, 208);
            this.visualBox.TabIndex = 102;
            this.visualBox.TabStop = false;
            this.visualBox.Text = "Настройки отображения";
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.Location = new System.Drawing.Point(937, 370);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(99, 24);
            this.exitButton.TabIndex = 103;
            this.exitButton.Text = "Отмена";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // MethodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1124, 608);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.visualBox);
            this.Controls.Add(this.gridName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.coordY);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.coordX);
            this.Controls.Add(this.densityBox);
            this.Controls.Add(this.saveAndExitButton);
            this.Controls.Add(this.demoBox);
            this.Controls.Add(this.nodesBox);
            this.Controls.Add(this.gridBox);
            this.Controls.Add(this.drawArea);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MethodForm";
            this.Text = "Подсистема генерации сетки КЭ фронтальным методом";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MethodForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Form1_Scroll);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.Form1_HelpRequested);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MethodForm_Paint);
            this.gridBox.ResumeLayout(false);
            this.nodesBox.ResumeLayout(false);
            this.nodesBox.PerformLayout();
            this.demoBox.ResumeLayout(false);
            this.demoBox.PerformLayout();
            this.densityBox.ResumeLayout(false);
            this.densityBox.PerformLayout();
            this.visualBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox funcValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox showGrid;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox showFEnumbers;
        public Tao.Platform.Windows.SimpleOpenGlControl drawArea;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.GroupBox gridBox;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button buildButton;
        private System.Windows.Forms.GroupBox nodesBox;
        private System.Windows.Forms.Button stepOkButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox demoBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button demoButton;
        private System.Windows.Forms.HScrollBar speedScrollBar;
        private System.Windows.Forms.Button deleteNodeButton;
        private System.Windows.Forms.Button addNodeButton;
        public System.Windows.Forms.TextBox stepValue;
        private System.Windows.Forms.Button saveAndExitButton;
        private System.Windows.Forms.GroupBox densityBox;
        private System.Windows.Forms.Button addFuncPoint;
        private System.Windows.Forms.Label Radius;
        private System.Windows.Forms.TextBox funcRadius;
        private System.Windows.Forms.Button deleteFuncButton;
        private System.Windows.Forms.TextBox coordY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox coordX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox gridName;
        private System.Windows.Forms.GroupBox visualBox;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label buildDelay;
        private System.Windows.Forms.Button defaultNodes;

    }
}

