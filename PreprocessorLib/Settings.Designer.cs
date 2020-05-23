namespace PreprocessorLib
{
    partial class Settings
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
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridPeriod = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorMessage2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.showFEMaterials = new System.Windows.Forms.CheckBox();
            this.showFENodes = new System.Windows.Forms.CheckBox();
            this.showFENumbers = new System.Windows.Forms.CheckBox();
            this.showForceValue = new System.Windows.Forms.CheckBox();
            this.showAxis = new System.Windows.Forms.CheckBox();
            this.showForces = new System.Windows.Forms.CheckBox();
            this.showGrid = new System.Windows.Forms.CheckBox();
            this.showBounds = new System.Windows.Forms.CheckBox();
            this.showPoints = new System.Windows.Forms.CheckBox();
            this.showFE = new System.Windows.Forms.CheckBox();
            this.showLines = new System.Windows.Forms.CheckBox();
            this.showAreas = new System.Windows.Forms.CheckBox();
            this.showCircles = new System.Windows.Forms.CheckBox();
            this.showArcs = new System.Windows.Forms.CheckBox();
            this.showOnlyGeometry = new System.Windows.Forms.CheckBox();
            this.showOnlyAreas = new System.Windows.Forms.CheckBox();
            this.showOnlyFE = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(716, 492);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(797, 492);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridPeriod);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 85);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки сетки";
            // 
            // gridPeriod
            // 
            this.gridPeriod.Location = new System.Drawing.Point(74, 25);
            this.gridPeriod.Name = "gridPeriod";
            this.gridPeriod.Size = new System.Drawing.Size(74, 20);
            this.gridPeriod.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Шаг сетки:";
            // 
            // errorMessage2
            // 
            this.errorMessage2.AutoSize = true;
            this.errorMessage2.ForeColor = System.Drawing.Color.Red;
            this.errorMessage2.Location = new System.Drawing.Point(9, 459);
            this.errorMessage2.Name = "errorMessage2";
            this.errorMessage2.Size = new System.Drawing.Size(134, 13);
            this.errorMessage2.TabIndex = 50;
            this.errorMessage2.Text = "Недопустимое значение!";
            this.errorMessage2.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.showOnlyGeometry);
            this.groupBox2.Controls.Add(this.showFEMaterials);
            this.groupBox2.Controls.Add(this.showOnlyAreas);
            this.groupBox2.Controls.Add(this.showOnlyFE);
            this.groupBox2.Controls.Add(this.showFENodes);
            this.groupBox2.Controls.Add(this.showFENumbers);
            this.groupBox2.Controls.Add(this.showForceValue);
            this.groupBox2.Controls.Add(this.showAxis);
            this.groupBox2.Controls.Add(this.showForces);
            this.groupBox2.Controls.Add(this.showGrid);
            this.groupBox2.Controls.Add(this.showBounds);
            this.groupBox2.Controls.Add(this.showPoints);
            this.groupBox2.Controls.Add(this.showFE);
            this.groupBox2.Controls.Add(this.showLines);
            this.groupBox2.Controls.Add(this.showAreas);
            this.groupBox2.Controls.Add(this.showCircles);
            this.groupBox2.Controls.Add(this.showArcs);
            this.groupBox2.Location = new System.Drawing.Point(12, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(576, 326);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Отображаемые элементы";
            // 
            // showFEMaterials
            // 
            this.showFEMaterials.AutoSize = true;
            this.showFEMaterials.Checked = true;
            this.showFEMaterials.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFEMaterials.Location = new System.Drawing.Point(27, 285);
            this.showFEMaterials.Name = "showFEMaterials";
            this.showFEMaterials.Size = new System.Drawing.Size(166, 17);
            this.showFEMaterials.TabIndex = 51;
            this.showFEMaterials.Text = "номер свойств материалов";
            this.showFEMaterials.UseVisualStyleBackColor = true;
            // 
            // showFENodes
            // 
            this.showFENodes.AutoSize = true;
            this.showFENodes.Checked = true;
            this.showFENodes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFENodes.Location = new System.Drawing.Point(27, 262);
            this.showFENodes.Name = "showFENodes";
            this.showFENodes.Size = new System.Drawing.Size(96, 17);
            this.showFENodes.TabIndex = 74;
            this.showFENodes.Text = "номера узлов";
            this.showFENodes.UseVisualStyleBackColor = true;
            // 
            // showFENumbers
            // 
            this.showFENumbers.AutoSize = true;
            this.showFENumbers.Checked = true;
            this.showFENumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFENumbers.Location = new System.Drawing.Point(27, 239);
            this.showFENumbers.Name = "showFENumbers";
            this.showFENumbers.Size = new System.Drawing.Size(81, 17);
            this.showFENumbers.TabIndex = 73;
            this.showFENumbers.Text = "номера КЭ";
            this.showFENumbers.UseVisualStyleBackColor = true;
            // 
            // showForceValue
            // 
            this.showForceValue.AutoSize = true;
            this.showForceValue.Checked = true;
            this.showForceValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showForceValue.Location = new System.Drawing.Point(157, 98);
            this.showForceValue.Name = "showForceValue";
            this.showForceValue.Size = new System.Drawing.Size(74, 17);
            this.showForceValue.TabIndex = 72;
            this.showForceValue.Text = "Значения";
            this.showForceValue.UseVisualStyleBackColor = true;
            // 
            // showAxis
            // 
            this.showAxis.AutoSize = true;
            this.showAxis.Checked = true;
            this.showAxis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showAxis.Location = new System.Drawing.Point(10, 52);
            this.showAxis.Name = "showAxis";
            this.showAxis.Size = new System.Drawing.Size(121, 17);
            this.showAxis.TabIndex = 62;
            this.showAxis.Text = "Координатные оси";
            this.showAxis.UseVisualStyleBackColor = true;
            // 
            // showForces
            // 
            this.showForces.AutoSize = true;
            this.showForces.Checked = true;
            this.showForces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showForces.Location = new System.Drawing.Point(137, 75);
            this.showForces.Name = "showForces";
            this.showForces.Size = new System.Drawing.Size(74, 17);
            this.showForces.TabIndex = 71;
            this.showForces.Text = "Нагрузки";
            this.showForces.UseVisualStyleBackColor = true;
            this.showForces.CheckedChanged += new System.EventHandler(this.showForces_CheckedChanged);
            // 
            // showGrid
            // 
            this.showGrid.AutoSize = true;
            this.showGrid.Checked = true;
            this.showGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showGrid.Location = new System.Drawing.Point(10, 76);
            this.showGrid.Name = "showGrid";
            this.showGrid.Size = new System.Drawing.Size(56, 17);
            this.showGrid.TabIndex = 63;
            this.showGrid.Text = "Сетка";
            this.showGrid.UseVisualStyleBackColor = true;
            // 
            // showBounds
            // 
            this.showBounds.AutoSize = true;
            this.showBounds.Checked = true;
            this.showBounds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showBounds.Location = new System.Drawing.Point(137, 52);
            this.showBounds.Name = "showBounds";
            this.showBounds.Size = new System.Drawing.Size(93, 17);
            this.showBounds.TabIndex = 70;
            this.showBounds.Text = "Закрепления";
            this.showBounds.UseVisualStyleBackColor = true;
            // 
            // showPoints
            // 
            this.showPoints.AutoSize = true;
            this.showPoints.Checked = true;
            this.showPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showPoints.Location = new System.Drawing.Point(10, 100);
            this.showPoints.Name = "showPoints";
            this.showPoints.Size = new System.Drawing.Size(56, 17);
            this.showPoints.TabIndex = 64;
            this.showPoints.Text = "Точки";
            this.showPoints.UseVisualStyleBackColor = true;
            // 
            // showFE
            // 
            this.showFE.AutoSize = true;
            this.showFE.Checked = true;
            this.showFE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFE.Location = new System.Drawing.Point(10, 216);
            this.showFE.Name = "showFE";
            this.showFE.Size = new System.Drawing.Size(40, 17);
            this.showFE.TabIndex = 69;
            this.showFE.Text = "КЭ";
            this.showFE.UseVisualStyleBackColor = true;
            this.showFE.CheckedChanged += new System.EventHandler(this.showFE_CheckedChanged);
            // 
            // showLines
            // 
            this.showLines.AutoSize = true;
            this.showLines.Checked = true;
            this.showLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showLines.Location = new System.Drawing.Point(10, 124);
            this.showLines.Name = "showLines";
            this.showLines.Size = new System.Drawing.Size(58, 17);
            this.showLines.TabIndex = 65;
            this.showLines.Text = "Линии";
            this.showLines.UseVisualStyleBackColor = true;
            // 
            // showAreas
            // 
            this.showAreas.AutoSize = true;
            this.showAreas.Checked = true;
            this.showAreas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showAreas.Location = new System.Drawing.Point(10, 193);
            this.showAreas.Name = "showAreas";
            this.showAreas.Size = new System.Drawing.Size(53, 17);
            this.showAreas.TabIndex = 68;
            this.showAreas.Text = "Зоны";
            this.showAreas.UseVisualStyleBackColor = true;
            // 
            // showCircles
            // 
            this.showCircles.AutoSize = true;
            this.showCircles.Checked = true;
            this.showCircles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showCircles.Location = new System.Drawing.Point(10, 147);
            this.showCircles.Name = "showCircles";
            this.showCircles.Size = new System.Drawing.Size(88, 17);
            this.showCircles.TabIndex = 66;
            this.showCircles.Text = "Окружности";
            this.showCircles.UseVisualStyleBackColor = true;
            // 
            // showArcs
            // 
            this.showArcs.AutoSize = true;
            this.showArcs.Checked = true;
            this.showArcs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showArcs.Location = new System.Drawing.Point(10, 170);
            this.showArcs.Name = "showArcs";
            this.showArcs.Size = new System.Drawing.Size(51, 17);
            this.showArcs.TabIndex = 67;
            this.showArcs.Text = "Дуги";
            this.showArcs.UseVisualStyleBackColor = true;
            // 
            // showOnlyGeometry
            // 
            this.showOnlyGeometry.AutoSize = true;
            this.showOnlyGeometry.Checked = true;
            this.showOnlyGeometry.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showOnlyGeometry.Location = new System.Drawing.Point(10, 19);
            this.showOnlyGeometry.Name = "showOnlyGeometry";
            this.showOnlyGeometry.Size = new System.Drawing.Size(81, 17);
            this.showOnlyGeometry.TabIndex = 53;
            this.showOnlyGeometry.Text = "Геометрия";
            this.showOnlyGeometry.UseVisualStyleBackColor = true;
            // 
            // showOnlyAreas
            // 
            this.showOnlyAreas.AutoSize = true;
            this.showOnlyAreas.Checked = true;
            this.showOnlyAreas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showOnlyAreas.Location = new System.Drawing.Point(97, 19);
            this.showOnlyAreas.Name = "showOnlyAreas";
            this.showOnlyAreas.Size = new System.Drawing.Size(53, 17);
            this.showOnlyAreas.TabIndex = 54;
            this.showOnlyAreas.Text = "Зоны";
            this.showOnlyAreas.UseVisualStyleBackColor = true;
            // 
            // showOnlyFE
            // 
            this.showOnlyFE.AutoSize = true;
            this.showOnlyFE.Checked = true;
            this.showOnlyFE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showOnlyFE.Location = new System.Drawing.Point(156, 19);
            this.showOnlyFE.Name = "showOnlyFE";
            this.showOnlyFE.Size = new System.Drawing.Size(40, 17);
            this.showOnlyFE.TabIndex = 55;
            this.showOnlyFE.Text = "КЭ";
            this.showOnlyFE.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 522);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.errorMessage2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowInTaskbar = false;
            this.Text = "Настройки";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox gridPeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label errorMessage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox showForceValue;
        private System.Windows.Forms.CheckBox showAxis;
        private System.Windows.Forms.CheckBox showForces;
        private System.Windows.Forms.CheckBox showGrid;
        private System.Windows.Forms.CheckBox showBounds;
        private System.Windows.Forms.CheckBox showPoints;
        private System.Windows.Forms.CheckBox showFE;
        private System.Windows.Forms.CheckBox showLines;
        private System.Windows.Forms.CheckBox showAreas;
        private System.Windows.Forms.CheckBox showCircles;
        private System.Windows.Forms.CheckBox showArcs;
        public System.Windows.Forms.CheckBox showFENodes;
        public System.Windows.Forms.CheckBox showFENumbers;
        public System.Windows.Forms.CheckBox showFEMaterials;
        public System.Windows.Forms.CheckBox showOnlyGeometry;
        public System.Windows.Forms.CheckBox showOnlyAreas;
        public System.Windows.Forms.CheckBox showOnlyFE;
    }
}