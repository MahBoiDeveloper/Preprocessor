namespace PreprocessorLib
{
    partial class SetMaterials
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.usedMaterials = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.pickAllAreas = new System.Windows.Forms.Label();
            this.areaMaterials = new System.Windows.Forms.TextBox();
            this.materialComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.areasRadioBut = new System.Windows.Forms.RadioButton();
            this.FERadioBut = new System.Windows.Forms.RadioButton();
            this.FEMaterials = new System.Windows.Forms.TextBox();
            this.pickAllFE = new System.Windows.Forms.Label();
            this.deleteRow = new System.Windows.Forms.DataGridViewButtonColumn();
            this.matID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matElastic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matPois = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sgat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matThick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.usedMaterials)).BeginInit();
            this.SuspendLayout();
            // 
            // usedMaterials
            // 
            this.usedMaterials.AllowUserToAddRows = false;
            this.usedMaterials.AllowUserToResizeRows = false;
            this.usedMaterials.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usedMaterials.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.usedMaterials.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.usedMaterials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usedMaterials.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.deleteRow,
            this.matID,
            this.matName,
            this.matElastic,
            this.matPois,
            this.matTen,
            this.Sgat,
            this.matThick});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.usedMaterials.DefaultCellStyle = dataGridViewCellStyle8;
            this.usedMaterials.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.usedMaterials.Location = new System.Drawing.Point(12, 38);
            this.usedMaterials.Name = "usedMaterials";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.usedMaterials.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.usedMaterials.RowHeadersVisible = false;
            this.usedMaterials.Size = new System.Drawing.Size(918, 181);
            this.usedMaterials.TabIndex = 0;
            this.usedMaterials.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.usedMaterials_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Используемые материалы:";
            // 
            // pickAllAreas
            // 
            this.pickAllAreas.AutoSize = true;
            this.pickAllAreas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pickAllAreas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pickAllAreas.ForeColor = System.Drawing.Color.Blue;
            this.pickAllAreas.Location = new System.Drawing.Point(290, 272);
            this.pickAllAreas.Name = "pickAllAreas";
            this.pickAllAreas.Size = new System.Drawing.Size(101, 13);
            this.pickAllAreas.TabIndex = 69;
            this.pickAllAreas.Text = "Выбрать все зоны";
            this.pickAllAreas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pickAllAreas_MouseClick);
            // 
            // areaMaterials
            // 
            this.areaMaterials.Location = new System.Drawing.Point(12, 269);
            this.areaMaterials.Name = "areaMaterials";
            this.areaMaterials.Size = new System.Drawing.Size(271, 20);
            this.areaMaterials.TabIndex = 68;
            // 
            // materialComboBox
            // 
            this.materialComboBox.FormattingEnabled = true;
            this.materialComboBox.Location = new System.Drawing.Point(78, 225);
            this.materialComboBox.Name = "materialComboBox";
            this.materialComboBox.Size = new System.Drawing.Size(164, 21);
            this.materialComboBox.TabIndex = 71;
            this.materialComboBox.SelectedIndexChanged += new System.EventHandler(this.materialComboBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 70;
            this.label7.Text = "Материал:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(700, 311);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 72;
            this.cancelButton.Text = "Закрыть";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(619, 311);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 74;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // areasRadioBut
            // 
            this.areasRadioBut.AutoSize = true;
            this.areasRadioBut.Checked = true;
            this.areasRadioBut.Location = new System.Drawing.Point(12, 246);
            this.areasRadioBut.Name = "areasRadioBut";
            this.areasRadioBut.Size = new System.Drawing.Size(275, 17);
            this.areasRadioBut.TabIndex = 75;
            this.areasRadioBut.TabStop = true;
            this.areasRadioBut.Text = "Номера зон с этим материалом (через запятую):";
            this.areasRadioBut.UseVisualStyleBackColor = true;
            this.areasRadioBut.CheckedChanged += new System.EventHandler(this.areaRadioBut_CheckedChanged);
            // 
            // FERadioBut
            // 
            this.FERadioBut.AutoSize = true;
            this.FERadioBut.Location = new System.Drawing.Point(12, 295);
            this.FERadioBut.Name = "FERadioBut";
            this.FERadioBut.Size = new System.Drawing.Size(271, 17);
            this.FERadioBut.TabIndex = 76;
            this.FERadioBut.Text = "Номера КЭ с этим материалом (через запятую):";
            this.FERadioBut.UseVisualStyleBackColor = true;
            this.FERadioBut.CheckedChanged += new System.EventHandler(this.FERadioBut_CheckedChanged);
            // 
            // FEMaterials
            // 
            this.FEMaterials.Enabled = false;
            this.FEMaterials.Location = new System.Drawing.Point(12, 318);
            this.FEMaterials.Name = "FEMaterials";
            this.FEMaterials.Size = new System.Drawing.Size(271, 20);
            this.FEMaterials.TabIndex = 77;
            // 
            // pickAllFE
            // 
            this.pickAllFE.AutoSize = true;
            this.pickAllFE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pickAllFE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pickAllFE.ForeColor = System.Drawing.Color.Blue;
            this.pickAllFE.Location = new System.Drawing.Point(290, 321);
            this.pickAllFE.Name = "pickAllFE";
            this.pickAllFE.Size = new System.Drawing.Size(89, 13);
            this.pickAllFE.TabIndex = 78;
            this.pickAllFE.Text = "Выбрать все КЭ";
            this.pickAllFE.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pickAllFE_MouseClick);
            // 
            // deleteRow
            // 
            this.deleteRow.HeaderText = "Удалить материал";
            this.deleteRow.Name = "deleteRow";
            this.deleteRow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.deleteRow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.deleteRow.Width = 60;
            // 
            // matID
            // 
            this.matID.HeaderText = "ID";
            this.matID.Name = "matID";
            this.matID.ReadOnly = true;
            this.matID.Width = 40;
            // 
            // matName
            // 
            this.matName.HeaderText = "Имя материала";
            this.matName.Name = "matName";
            this.matName.ReadOnly = true;
            this.matName.Width = 140;
            // 
            // matElastic
            // 
            this.matElastic.HeaderText = "Модуль упругости, Н/см^2";
            this.matElastic.Name = "matElastic";
            this.matElastic.ReadOnly = true;
            this.matElastic.Width = 140;
            // 
            // matPois
            // 
            this.matPois.HeaderText = "Коэффициент Пуассона";
            this.matPois.Name = "matPois";
            this.matPois.ReadOnly = true;
            this.matPois.Width = 140;
            // 
            // matTen
            // 
            this.matTen.HeaderText = "Допускаемое напряжение растяжения, Н/см^2";
            this.matTen.Name = "matTen";
            this.matTen.ReadOnly = true;
            this.matTen.Width = 140;
            // 
            // Sgat
            // 
            this.Sgat.HeaderText = "Допускаемое напряжение сжатия, Н/см^2";
            this.Sgat.Name = "Sgat";
            this.Sgat.ReadOnly = true;
            this.Sgat.Width = 140;
            // 
            // matThick
            // 
            this.matThick.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.matThick.HeaderText = "Толщина, см";
            this.matThick.Name = "matThick";
            this.matThick.ReadOnly = true;
            // 
            // SetMaterials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 346);
            this.Controls.Add(this.pickAllFE);
            this.Controls.Add(this.FEMaterials);
            this.Controls.Add(this.FERadioBut);
            this.Controls.Add(this.areasRadioBut);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.materialComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pickAllAreas);
            this.Controls.Add(this.areaMaterials);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usedMaterials);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetMaterials";
            this.ShowInTaskbar = false;
            this.Text = "Назначение материалов";
            ((System.ComponentModel.ISupportInitialize)(this.usedMaterials)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView usedMaterials;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label pickAllAreas;
        private System.Windows.Forms.TextBox areaMaterials;
        private System.Windows.Forms.ComboBox materialComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.RadioButton areasRadioBut;
        private System.Windows.Forms.RadioButton FERadioBut;
        private System.Windows.Forms.TextBox FEMaterials;
        private System.Windows.Forms.Label pickAllFE;
        private System.Windows.Forms.DataGridViewButtonColumn deleteRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn matID;
        private System.Windows.Forms.DataGridViewTextBoxColumn matName;
        private System.Windows.Forms.DataGridViewTextBoxColumn matElastic;
        private System.Windows.Forms.DataGridViewTextBoxColumn matPois;
        private System.Windows.Forms.DataGridViewTextBoxColumn matTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sgat;
        private System.Windows.Forms.DataGridViewTextBoxColumn matThick;
    }
}