namespace PreprocessorLib
{
    partial class EditMaterials
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
            this.materialName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.thickness = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tension = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.poissonsRatio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.elasticModulus = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.materialComboBox = new System.Windows.Forms.ComboBox();
            this.errorInvalidValue = new System.Windows.Forms.Label();
            this.deleteMaterialButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tension2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // materialName
            // 
            this.materialName.Location = new System.Drawing.Point(272, 80);
            this.materialName.Name = "materialName";
            this.materialName.Size = new System.Drawing.Size(133, 20);
            this.materialName.TabIndex = 57;
            this.materialName.TextChanged += new System.EventHandler(this.materialName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 56;
            this.label4.Text = "Имя материала:";
            // 
            // thickness
            // 
            this.thickness.Location = new System.Drawing.Point(272, 212);
            this.thickness.Name = "thickness";
            this.thickness.Size = new System.Drawing.Size(133, 20);
            this.thickness.TabIndex = 55;
            this.thickness.TextChanged += new System.EventHandler(this.thickness_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "Толщина, см:";
            // 
            // tension
            // 
            this.tension.Location = new System.Drawing.Point(272, 160);
            this.tension.Name = "tension";
            this.tension.Size = new System.Drawing.Size(133, 20);
            this.tension.TabIndex = 53;
            this.tension.TextChanged += new System.EventHandler(this.tension_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(254, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Допускаемое напряжение растяжения, Н/см^2:";
            // 
            // poissonsRatio
            // 
            this.poissonsRatio.Location = new System.Drawing.Point(272, 134);
            this.poissonsRatio.Name = "poissonsRatio";
            this.poissonsRatio.Size = new System.Drawing.Size(133, 20);
            this.poissonsRatio.TabIndex = 51;
            this.poissonsRatio.TextChanged += new System.EventHandler(this.poissonsRatio_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Коэффициент Пуассона:";
            // 
            // elasticModulus
            // 
            this.elasticModulus.Location = new System.Drawing.Point(272, 108);
            this.elasticModulus.Name = "elasticModulus";
            this.elasticModulus.Size = new System.Drawing.Size(133, 20);
            this.elasticModulus.TabIndex = 49;
            this.elasticModulus.TextChanged += new System.EventHandler(this.elasticModulus_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Модуль упругости, Н/см^2:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(204, 285);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 47;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(114, 285);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 46;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "Материал:";
            // 
            // materialComboBox
            // 
            this.materialComboBox.FormattingEnabled = true;
            this.materialComboBox.Location = new System.Drawing.Point(78, 29);
            this.materialComboBox.Name = "materialComboBox";
            this.materialComboBox.Size = new System.Drawing.Size(148, 21);
            this.materialComboBox.TabIndex = 61;
            this.materialComboBox.SelectedIndexChanged += new System.EventHandler(this.materialComboBox_SelectedIndexChanged);
            // 
            // errorInvalidValue
            // 
            this.errorInvalidValue.AutoSize = true;
            this.errorInvalidValue.ForeColor = System.Drawing.Color.Red;
            this.errorInvalidValue.Location = new System.Drawing.Point(142, 269);
            this.errorInvalidValue.Name = "errorInvalidValue";
            this.errorInvalidValue.Size = new System.Drawing.Size(110, 13);
            this.errorInvalidValue.TabIndex = 63;
            this.errorInvalidValue.Text = "Неверное значение!";
            this.errorInvalidValue.Visible = false;
            // 
            // deleteMaterialButton
            // 
            this.deleteMaterialButton.Location = new System.Drawing.Point(232, 29);
            this.deleteMaterialButton.Name = "deleteMaterialButton";
            this.deleteMaterialButton.Size = new System.Drawing.Size(61, 21);
            this.deleteMaterialButton.TabIndex = 67;
            this.deleteMaterialButton.Text = "удалить";
            this.deleteMaterialButton.UseVisualStyleBackColor = true;
            this.deleteMaterialButton.Click += new System.EventHandler(this.deleteMaterialButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(227, 13);
            this.label6.TabIndex = 68;
            this.label6.Text = "Допускаемое напряжение сжатия, Н/см^2";
            // 
            // tension2
            // 
            this.tension2.Location = new System.Drawing.Point(272, 186);
            this.tension2.Name = "tension2";
            this.tension2.Size = new System.Drawing.Size(133, 20);
            this.tension2.TabIndex = 69;
            this.tension2.TextChanged += new System.EventHandler(this.tension2_TextChanged);
            // 
            // EditMaterials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 333);
            this.Controls.Add(this.tension2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.deleteMaterialButton);
            this.Controls.Add(this.errorInvalidValue);
            this.Controls.Add(this.materialComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.materialName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.thickness);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tension);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.poissonsRatio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.elasticModulus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditMaterials";
            this.ShowInTaskbar = false;
            this.Text = "Редактирование материалов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox materialName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox thickness;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tension;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox poissonsRatio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox elasticModulus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox materialComboBox;
        private System.Windows.Forms.Label errorInvalidValue;
        private System.Windows.Forms.Button deleteMaterialButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tension2;
    }
}