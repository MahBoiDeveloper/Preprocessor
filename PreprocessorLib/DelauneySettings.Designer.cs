namespace PreprocessorLib
{
    partial class DelauneySettings
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
            this.lbxAreaDensities = new System.Windows.Forms.ListBox();
            this.btnDeleteAllAreaDensities = new System.Windows.Forms.Button();
            this.btnDeleteAreaDensities = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudMinAreaDistance = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddAreaDensity = new System.Windows.Forms.Button();
            this.nudAreaDensity = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudArea = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudMinSectorDistance = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddSectorDensity = new System.Windows.Forms.Button();
            this.nudDensitySector = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudRadius = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRemoveSectorDensity = new System.Windows.Forms.Button();
            this.btnRemoveAllSectorDensities = new System.Windows.Forms.Button();
            this.lbxSectorDensities = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinAreaDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaDensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArea)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinSectorDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDensitySector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRadius)).BeginInit();
            this.SuspendLayout();
            // 
            // lbxAreaDensities
            // 
            this.lbxAreaDensities.FormattingEnabled = true;
            this.lbxAreaDensities.Location = new System.Drawing.Point(12, 12);
            this.lbxAreaDensities.Name = "lbxAreaDensities";
            this.lbxAreaDensities.Size = new System.Drawing.Size(196, 108);
            this.lbxAreaDensities.TabIndex = 14;
            // 
            // btnDeleteAllAreaDensities
            // 
            this.btnDeleteAllAreaDensities.Location = new System.Drawing.Point(114, 126);
            this.btnDeleteAllAreaDensities.Name = "btnDeleteAllAreaDensities";
            this.btnDeleteAllAreaDensities.Size = new System.Drawing.Size(94, 23);
            this.btnDeleteAllAreaDensities.TabIndex = 27;
            this.btnDeleteAllAreaDensities.Text = "Удалить все";
            this.btnDeleteAllAreaDensities.UseVisualStyleBackColor = true;
            this.btnDeleteAllAreaDensities.Click += new System.EventHandler(this.btnDeleteAllAreaDensities_Click);
            // 
            // btnDeleteAreaDensities
            // 
            this.btnDeleteAreaDensities.Location = new System.Drawing.Point(12, 126);
            this.btnDeleteAreaDensities.Name = "btnDeleteAreaDensities";
            this.btnDeleteAreaDensities.Size = new System.Drawing.Size(94, 23);
            this.btnDeleteAreaDensities.TabIndex = 28;
            this.btnDeleteAreaDensities.Text = "Удалить";
            this.btnDeleteAreaDensities.UseVisualStyleBackColor = true;
            this.btnDeleteAreaDensities.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudMinAreaDistance);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnAddAreaDensity);
            this.groupBox1.Controls.Add(this.nudAreaDensity);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.nudArea);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(228, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 135);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Плотность в зоне";
            this.groupBox1.UseCompatibleTextRendering = true;
            // 
            // nudMinAreaDistance
            // 
            this.nudMinAreaDistance.DecimalPlaces = 2;
            this.nudMinAreaDistance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMinAreaDistance.Location = new System.Drawing.Point(141, 74);
            this.nudMinAreaDistance.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMinAreaDistance.Name = "nudMinAreaDistance";
            this.nudMinAreaDistance.Size = new System.Drawing.Size(51, 20);
            this.nudMinAreaDistance.TabIndex = 27;
            this.nudMinAreaDistance.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Минимальный отступ:";
            // 
            // btnAddAreaDensity
            // 
            this.btnAddAreaDensity.Location = new System.Drawing.Point(102, 106);
            this.btnAddAreaDensity.Name = "btnAddAreaDensity";
            this.btnAddAreaDensity.Size = new System.Drawing.Size(90, 23);
            this.btnAddAreaDensity.TabIndex = 25;
            this.btnAddAreaDensity.Text = "Добавить";
            this.btnAddAreaDensity.UseVisualStyleBackColor = true;
            this.btnAddAreaDensity.Click += new System.EventHandler(this.btnAddAreaDensity_Click);
            // 
            // nudAreaDensity
            // 
            this.nudAreaDensity.Location = new System.Drawing.Point(141, 48);
            this.nudAreaDensity.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudAreaDensity.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudAreaDensity.Name = "nudAreaDensity";
            this.nudAreaDensity.Size = new System.Drawing.Size(51, 20);
            this.nudAreaDensity.TabIndex = 24;
            this.nudAreaDensity.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Плотность";
            // 
            // nudArea
            // 
            this.nudArea.Location = new System.Drawing.Point(141, 22);
            this.nudArea.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudArea.Name = "nudArea";
            this.nudArea.Size = new System.Drawing.Size(51, 20);
            this.nudArea.TabIndex = 22;
            this.nudArea.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Зона";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudY);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.nudX);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.nudMinSectorDistance);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnAddSectorDensity);
            this.groupBox2.Controls.Add(this.nudDensitySector);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.nudRadius);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(228, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 177);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Плотность в секторе";
            // 
            // nudY
            // 
            this.nudY.DecimalPlaces = 2;
            this.nudY.Location = new System.Drawing.Point(141, 120);
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(51, 20);
            this.nudY.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Y";
            // 
            // nudX
            // 
            this.nudX.DecimalPlaces = 2;
            this.nudX.Location = new System.Drawing.Point(36, 120);
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(51, 20);
            this.nudX.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Координаты центра";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "X";
            // 
            // nudMinSectorDistance
            // 
            this.nudMinSectorDistance.DecimalPlaces = 2;
            this.nudMinSectorDistance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMinSectorDistance.Location = new System.Drawing.Point(141, 78);
            this.nudMinSectorDistance.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMinSectorDistance.Name = "nudMinSectorDistance";
            this.nudMinSectorDistance.Size = new System.Drawing.Size(51, 20);
            this.nudMinSectorDistance.TabIndex = 37;
            this.nudMinSectorDistance.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Минимальный отступ:";
            // 
            // btnAddSectorDensity
            // 
            this.btnAddSectorDensity.Location = new System.Drawing.Point(104, 148);
            this.btnAddSectorDensity.Name = "btnAddSectorDensity";
            this.btnAddSectorDensity.Size = new System.Drawing.Size(90, 23);
            this.btnAddSectorDensity.TabIndex = 26;
            this.btnAddSectorDensity.Text = "Добавить";
            this.btnAddSectorDensity.UseVisualStyleBackColor = true;
            this.btnAddSectorDensity.Click += new System.EventHandler(this.btnAddSectorDensity_Click);
            // 
            // nudDensitySector
            // 
            this.nudDensitySector.Location = new System.Drawing.Point(141, 52);
            this.nudDensitySector.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDensitySector.Name = "nudDensitySector";
            this.nudDensitySector.Size = new System.Drawing.Size(51, 20);
            this.nudDensitySector.TabIndex = 30;
            this.nudDensitySector.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "Плотность";
            // 
            // nudRadius
            // 
            this.nudRadius.DecimalPlaces = 2;
            this.nudRadius.Location = new System.Drawing.Point(141, 25);
            this.nudRadius.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudRadius.Name = "nudRadius";
            this.nudRadius.Size = new System.Drawing.Size(51, 20);
            this.nudRadius.TabIndex = 28;
            this.nudRadius.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Радиус";
            // 
            // btnRemoveSectorDensity
            // 
            this.btnRemoveSectorDensity.Location = new System.Drawing.Point(12, 276);
            this.btnRemoveSectorDensity.Name = "btnRemoveSectorDensity";
            this.btnRemoveSectorDensity.Size = new System.Drawing.Size(94, 23);
            this.btnRemoveSectorDensity.TabIndex = 33;
            this.btnRemoveSectorDensity.Text = "Удалить";
            this.btnRemoveSectorDensity.UseVisualStyleBackColor = true;
            this.btnRemoveSectorDensity.Click += new System.EventHandler(this.btnRemoveSectorDensity_Click);
            // 
            // btnRemoveAllSectorDensities
            // 
            this.btnRemoveAllSectorDensities.Location = new System.Drawing.Point(114, 276);
            this.btnRemoveAllSectorDensities.Name = "btnRemoveAllSectorDensities";
            this.btnRemoveAllSectorDensities.Size = new System.Drawing.Size(94, 23);
            this.btnRemoveAllSectorDensities.TabIndex = 32;
            this.btnRemoveAllSectorDensities.Text = "Удалить все";
            this.btnRemoveAllSectorDensities.UseVisualStyleBackColor = true;
            this.btnRemoveAllSectorDensities.Click += new System.EventHandler(this.btnRemoveAllSectorDensities_Click);
            // 
            // lbxSectorDensities
            // 
            this.lbxSectorDensities.FormattingEnabled = true;
            this.lbxSectorDensities.HorizontalScrollbar = true;
            this.lbxSectorDensities.Location = new System.Drawing.Point(12, 162);
            this.lbxSectorDensities.Name = "lbxSectorDensities";
            this.lbxSectorDensities.Size = new System.Drawing.Size(196, 108);
            this.lbxSectorDensities.TabIndex = 31;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 316);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DelauneySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 352);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRemoveSectorDensity);
            this.Controls.Add(this.btnRemoveAllSectorDensities);
            this.Controls.Add(this.lbxSectorDensities);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDeleteAreaDensities);
            this.Controls.Add(this.btnDeleteAllAreaDensities);
            this.Controls.Add(this.lbxAreaDensities);
            this.Name = "DelauneySettings";
            this.Text = "Настройки";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinAreaDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaDensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArea)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinSectorDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDensitySector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRadius)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxAreaDensities;
        private System.Windows.Forms.Button btnDeleteAllAreaDensities;
        private System.Windows.Forms.Button btnDeleteAreaDensities;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddAreaDensity;
        private System.Windows.Forms.NumericUpDown nudAreaDensity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudArea;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudDensitySector;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudRadius;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnAddSectorDensity;
        private System.Windows.Forms.Button btnRemoveSectorDensity;
        private System.Windows.Forms.Button btnRemoveAllSectorDensities;
        private System.Windows.Forms.ListBox lbxSectorDensities;
        private System.Windows.Forms.NumericUpDown nudMinAreaDistance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudMinSectorDistance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}