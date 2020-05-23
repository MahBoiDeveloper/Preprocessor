namespace PreprocessorLib
{
    partial class GridAnalysis
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
            this.lblMinAngle = new System.Windows.Forms.Label();
            this.tbMinAngle = new System.Windows.Forms.TrackBar();
            this.tbMaxAngle = new System.Windows.Forms.TrackBar();
            this.lblMaxAngle = new System.Windows.Forms.Label();
            this.lblMinSquare = new System.Windows.Forms.Label();
            this.btnStatistics = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rbAngle = new System.Windows.Forms.RadioButton();
            this.rbSquare = new System.Windows.Forms.RadioButton();
            this.nudMinAngle = new System.Windows.Forms.NumericUpDown();
            this.nudMaxAngle = new System.Windows.Forms.NumericUpDown();
            this.nudSquare = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.tbMinAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMaxAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSquare)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMinAngle
            // 
            this.lblMinAngle.AutoSize = true;
            this.lblMinAngle.Location = new System.Drawing.Point(4, 4);
            this.lblMinAngle.Name = "lblMinAngle";
            this.lblMinAngle.Size = new System.Drawing.Size(165, 13);
            this.lblMinAngle.TabIndex = 0;
            this.lblMinAngle.Text = "Минимально допустимый угол:";
            // 
            // tbMinAngle
            // 
            this.tbMinAngle.Location = new System.Drawing.Point(7, 27);
            this.tbMinAngle.Maximum = 180;
            this.tbMinAngle.Name = "tbMinAngle";
            this.tbMinAngle.Size = new System.Drawing.Size(248, 45);
            this.tbMinAngle.TabIndex = 2;
            this.tbMinAngle.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbMinAngle.Value = 20;
            this.tbMinAngle.Scroll += new System.EventHandler(this.tbMinAngle_Scroll);
            // 
            // tbMaxAngle
            // 
            this.tbMaxAngle.Location = new System.Drawing.Point(7, 78);
            this.tbMaxAngle.Maximum = 180;
            this.tbMaxAngle.Name = "tbMaxAngle";
            this.tbMaxAngle.Size = new System.Drawing.Size(248, 45);
            this.tbMaxAngle.TabIndex = 5;
            this.tbMaxAngle.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbMaxAngle.Value = 120;
            this.tbMaxAngle.Scroll += new System.EventHandler(this.tbMaxAngle_Scroll);
            // 
            // lblMaxAngle
            // 
            this.lblMaxAngle.AutoSize = true;
            this.lblMaxAngle.Location = new System.Drawing.Point(4, 55);
            this.lblMaxAngle.Name = "lblMaxAngle";
            this.lblMaxAngle.Size = new System.Drawing.Size(171, 13);
            this.lblMaxAngle.TabIndex = 3;
            this.lblMaxAngle.Text = "Максимально допустимый угол:";
            // 
            // lblMinSquare
            // 
            this.lblMinSquare.AutoSize = true;
            this.lblMinSquare.Location = new System.Drawing.Point(4, 110);
            this.lblMinSquare.Name = "lblMinSquare";
            this.lblMinSquare.Size = new System.Drawing.Size(192, 13);
            this.lblMinSquare.TabIndex = 6;
            this.lblMinSquare.Text = "Минимально допустимая nплощадь:";
            // 
            // btnStatistics
            // 
            this.btnStatistics.Location = new System.Drawing.Point(94, 157);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(75, 23);
            this.btnStatistics.TabIndex = 8;
            this.btnStatistics.Text = "Статистика";
            this.btnStatistics.UseVisualStyleBackColor = true;
            this.btnStatistics.Click += new System.EventHandler(this.btnStatistics_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(180, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Цветовая градация:";
            // 
            // rbAngle
            // 
            this.rbAngle.AutoSize = true;
            this.rbAngle.Checked = true;
            this.rbAngle.Location = new System.Drawing.Point(119, 134);
            this.rbAngle.Name = "rbAngle";
            this.rbAngle.Size = new System.Drawing.Size(50, 17);
            this.rbAngle.TabIndex = 13;
            this.rbAngle.TabStop = true;
            this.rbAngle.Text = "Угол";
            this.rbAngle.UseVisualStyleBackColor = true;
            this.rbAngle.CheckedChanged += new System.EventHandler(this.RadioButton_Changed);
            // 
            // rbSquare
            // 
            this.rbSquare.AutoSize = true;
            this.rbSquare.Location = new System.Drawing.Point(183, 134);
            this.rbSquare.Name = "rbSquare";
            this.rbSquare.Size = new System.Drawing.Size(72, 17);
            this.rbSquare.TabIndex = 14;
            this.rbSquare.Text = "Площадь";
            this.rbSquare.UseVisualStyleBackColor = true;
            this.rbSquare.CheckedChanged += new System.EventHandler(this.RadioButton_Changed);
            // 
            // nudMinAngle
            // 
            this.nudMinAngle.DecimalPlaces = 2;
            this.nudMinAngle.Location = new System.Drawing.Point(180, 2);
            this.nudMinAngle.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nudMinAngle.Name = "nudMinAngle";
            this.nudMinAngle.Size = new System.Drawing.Size(75, 20);
            this.nudMinAngle.TabIndex = 16;
            this.nudMinAngle.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudMinAngle.ValueChanged += new System.EventHandler(this.nudMinAngle_ValueChanged);
            // 
            // nudMaxAngle
            // 
            this.nudMaxAngle.DecimalPlaces = 2;
            this.nudMaxAngle.Location = new System.Drawing.Point(180, 53);
            this.nudMaxAngle.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nudMaxAngle.Name = "nudMaxAngle";
            this.nudMaxAngle.Size = new System.Drawing.Size(75, 20);
            this.nudMaxAngle.TabIndex = 17;
            this.nudMaxAngle.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudMaxAngle.ValueChanged += new System.EventHandler(this.nudMaxAngle_ValueChanged);
            // 
            // nudSquare
            // 
            this.nudSquare.DecimalPlaces = 2;
            this.nudSquare.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudSquare.Location = new System.Drawing.Point(202, 108);
            this.nudSquare.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSquare.Name = "nudSquare";
            this.nudSquare.Size = new System.Drawing.Size(53, 20);
            this.nudSquare.TabIndex = 18;
            this.nudSquare.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudSquare.ValueChanged += new System.EventHandler(this.nudSquare_ValueChanged);
            // 
            // GridAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudSquare);
            this.Controls.Add(this.nudMaxAngle);
            this.Controls.Add(this.nudMinAngle);
            this.Controls.Add(this.rbSquare);
            this.Controls.Add(this.rbAngle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStatistics);
            this.Controls.Add(this.lblMinSquare);
            this.Controls.Add(this.tbMaxAngle);
            this.Controls.Add(this.lblMaxAngle);
            this.Controls.Add(this.tbMinAngle);
            this.Controls.Add(this.lblMinAngle);
            this.Location = new System.Drawing.Point(0, 270);
            this.Name = "GridAnalysis";
            this.Size = new System.Drawing.Size(280, 188);
            this.Load += new System.EventHandler(this.GridAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbMinAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMaxAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSquare)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMinAngle;
        private System.Windows.Forms.TrackBar tbMinAngle;
        private System.Windows.Forms.TrackBar tbMaxAngle;
        private System.Windows.Forms.Label lblMaxAngle;
        private System.Windows.Forms.Label lblMinSquare;
        private System.Windows.Forms.Button btnStatistics;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbAngle;
        private System.Windows.Forms.RadioButton rbSquare;
        private System.Windows.Forms.NumericUpDown nudMinAngle;
        private System.Windows.Forms.NumericUpDown nudMaxAngle;
        private System.Windows.Forms.NumericUpDown nudSquare;
    }
}
