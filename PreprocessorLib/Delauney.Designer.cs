namespace PreprocessorLib
{
    partial class Delauney
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
            this.label1 = new System.Windows.Forms.Label();
            this.ddlGenerationMethod = new System.Windows.Forms.ComboBox();
            this.gridName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbxSettings = new System.Windows.Forms.GroupBox();
            this.nudMinDistance = new System.Windows.Forms.NumericUpDown();
            this.lblPlaceHolder = new System.Windows.Forms.Label();
            this.nudTriesCount = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorBadGridName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlTriangulationMethod = new System.Windows.Forms.ComboBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.nudNodesCount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.gbxSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTriesCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNodesCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Генерация точек:";
            // 
            // ddlGenerationMethod
            // 
            this.ddlGenerationMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlGenerationMethod.FormattingEnabled = true;
            this.ddlGenerationMethod.Items.AddRange(new object[] {
            "Хаотическая",
            "Хаотическая с плотностью распределения",
            "Прямоугольная регулярная",
            "Равноугольная регулярная",
            "Узлы имеющейся сетки"});
            this.ddlGenerationMethod.Location = new System.Drawing.Point(113, 46);
            this.ddlGenerationMethod.Name = "ddlGenerationMethod";
            this.ddlGenerationMethod.Size = new System.Drawing.Size(225, 21);
            this.ddlGenerationMethod.TabIndex = 1;
            this.ddlGenerationMethod.SelectedIndexChanged += new System.EventHandler(this.ddlGenerationMethod_SelectedIndexChanged);
            // 
            // gridName
            // 
            this.gridName.Location = new System.Drawing.Point(113, 12);
            this.gridName.Name = "gridName";
            this.gridName.Size = new System.Drawing.Size(226, 20);
            this.gridName.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Имя сетки:";
            // 
            // gbxSettings
            // 
            this.gbxSettings.Controls.Add(this.btnSettings);
            this.gbxSettings.Controls.Add(this.nudNodesCount);
            this.gbxSettings.Controls.Add(this.label5);
            this.gbxSettings.Controls.Add(this.nudMinDistance);
            this.gbxSettings.Controls.Add(this.lblPlaceHolder);
            this.gbxSettings.Controls.Add(this.nudTriesCount);
            this.gbxSettings.Controls.Add(this.label6);
            this.gbxSettings.Location = new System.Drawing.Point(15, 73);
            this.gbxSettings.Name = "gbxSettings";
            this.gbxSettings.Size = new System.Drawing.Size(332, 141);
            this.gbxSettings.TabIndex = 24;
            this.gbxSettings.TabStop = false;
            this.gbxSettings.Text = "Параметры генерации";
            // 
            // nudMinDistance
            // 
            this.nudMinDistance.DecimalPlaces = 2;
            this.nudMinDistance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudMinDistance.Location = new System.Drawing.Point(227, 29);
            this.nudMinDistance.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMinDistance.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMinDistance.Name = "nudMinDistance";
            this.nudMinDistance.Size = new System.Drawing.Size(96, 20);
            this.nudMinDistance.TabIndex = 31;
            this.nudMinDistance.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblPlaceHolder
            // 
            this.lblPlaceHolder.AutoSize = true;
            this.lblPlaceHolder.Location = new System.Drawing.Point(13, 31);
            this.lblPlaceHolder.Name = "lblPlaceHolder";
            this.lblPlaceHolder.Size = new System.Drawing.Size(177, 13);
            this.lblPlaceHolder.TabIndex = 30;
            this.lblPlaceHolder.Text = "Мин. расстояние между точками:";
            // 
            // nudTriesCount
            // 
            this.nudTriesCount.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudTriesCount.Location = new System.Drawing.Point(227, 55);
            this.nudTriesCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudTriesCount.Name = "nudTriesCount";
            this.nudTriesCount.Size = new System.Drawing.Size(96, 20);
            this.nudTriesCount.TabIndex = 28;
            this.nudTriesCount.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Максимальное количество попыток:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(15, 272);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(96, 272);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorBadGridName
            // 
            this.errorBadGridName.AutoSize = true;
            this.errorBadGridName.ForeColor = System.Drawing.Color.Red;
            this.errorBadGridName.Location = new System.Drawing.Point(12, 256);
            this.errorBadGridName.Name = "errorBadGridName";
            this.errorBadGridName.Size = new System.Drawing.Size(210, 13);
            this.errorBadGridName.TabIndex = 20;
            this.errorBadGridName.Text = "Сетка с таким именем уже существует!";
            this.errorBadGridName.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Метод построения сетки:";
            // 
            // ddlTriangulationMethod
            // 
            this.ddlTriangulationMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlTriangulationMethod.FormattingEnabled = true;
            this.ddlTriangulationMethod.Items.AddRange(new object[] {
            "Алгоритм \"Sweep-hull\"",
            "Алгоритм Пола Бурка"});
            this.ddlTriangulationMethod.Location = new System.Drawing.Point(150, 231);
            this.ddlTriangulationMethod.Name = "ddlTriangulationMethod";
            this.ddlTriangulationMethod.Size = new System.Drawing.Size(188, 21);
            this.ddlTriangulationMethod.TabIndex = 26;
            // 
            // btnSettings
            // 
            this.btnSettings.Enabled = false;
            this.btnSettings.Location = new System.Drawing.Point(135, 107);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(188, 23);
            this.btnSettings.TabIndex = 27;
            this.btnSettings.Text = "Параметры плотности";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // nudNodesCount
            // 
            this.nudNodesCount.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudNodesCount.Location = new System.Drawing.Point(227, 81);
            this.nudNodesCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudNodesCount.Name = "nudNodesCount";
            this.nudNodesCount.Size = new System.Drawing.Size(96, 20);
            this.nudNodesCount.TabIndex = 33;
            this.nudNodesCount.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Количество случайных точек на зону:";
            // 
            // Delauney
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 303);
            this.Controls.Add(this.ddlTriangulationMethod);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gbxSettings);
            this.Controls.Add(this.errorBadGridName);
            this.Controls.Add(this.gridName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.ddlGenerationMethod);
            this.Controls.Add(this.label1);
            this.Name = "Delauney";
            this.Text = "Триангуляция Делоне";
            this.TopMost = true;
            this.gbxSettings.ResumeLayout(false);
            this.gbxSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTriesCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNodesCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlGenerationMethod;
        private System.Windows.Forms.TextBox gridName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbxSettings;
        private System.Windows.Forms.NumericUpDown nudMinDistance;
        private System.Windows.Forms.Label lblPlaceHolder;
        private System.Windows.Forms.NumericUpDown nudTriesCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label errorBadGridName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlTriangulationMethod;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.NumericUpDown nudNodesCount;
        private System.Windows.Forms.Label label5;

    }
}