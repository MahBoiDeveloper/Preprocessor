namespace PreprocessorLib
{
    partial class AddMaterials
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
            this.errorInvalidValue = new System.Windows.Forms.Label();
            this.thickness = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tension = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.poissonsRatio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.elasticModulus = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.materialName = new System.Windows.Forms.TextBox();
            this.addMore = new System.Windows.Forms.CheckBox();
            this.errorFillAll = new System.Windows.Forms.Label();
            this.errorInvalidMaterialNumber = new System.Windows.Forms.Label();
            this.tension2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // errorInvalidValue
            // 
            this.errorInvalidValue.AutoSize = true;
            this.errorInvalidValue.ForeColor = System.Drawing.Color.Red;
            this.errorInvalidValue.Location = new System.Drawing.Point(12, 237);
            this.errorInvalidValue.Name = "errorInvalidValue";
            this.errorInvalidValue.Size = new System.Drawing.Size(110, 13);
            this.errorInvalidValue.TabIndex = 41;
            this.errorInvalidValue.Text = "Неверное значение!";
            this.errorInvalidValue.Visible = false;
            // 
            // thickness
            // 
            this.thickness.Location = new System.Drawing.Point(280, 187);
            this.thickness.Name = "thickness";
            this.thickness.Size = new System.Drawing.Size(136, 20);
            this.thickness.TabIndex = 40;
            this.thickness.TextChanged += new System.EventHandler(this.thickness_TextChanged);
            this.thickness.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.thickness_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Толщина, см:";
            // 
            // tension
            // 
            this.tension.Location = new System.Drawing.Point(280, 136);
            this.tension.Name = "tension";
            this.tension.Size = new System.Drawing.Size(136, 20);
            this.tension.TabIndex = 38;
            this.tension.TextChanged += new System.EventHandler(this.tension_TextChanged);
            this.tension.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tension_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(254, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Допускаемое напряжение растяжения, Н/см^2:";
            // 
            // poissonsRatio
            // 
            this.poissonsRatio.Location = new System.Drawing.Point(280, 110);
            this.poissonsRatio.Name = "poissonsRatio";
            this.poissonsRatio.Size = new System.Drawing.Size(136, 20);
            this.poissonsRatio.TabIndex = 36;
            this.poissonsRatio.TextChanged += new System.EventHandler(this.poissonsRatio_TextChanged);
            this.poissonsRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.poissonsRatio_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Коэффициент Пуассона:";
            // 
            // elasticModulus
            // 
            this.elasticModulus.Location = new System.Drawing.Point(280, 84);
            this.elasticModulus.Name = "elasticModulus";
            this.elasticModulus.Size = new System.Drawing.Size(136, 20);
            this.elasticModulus.TabIndex = 34;
            this.elasticModulus.TextChanged += new System.EventHandler(this.elasticModulus_TextChanged);
            this.elasticModulus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.elasticModulus_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Модуль упругости, Н/см^2:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(92, 283);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 32;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(11, 283);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 31;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Имя материала:";
            // 
            // materialName
            // 
            this.materialName.Location = new System.Drawing.Point(280, 58);
            this.materialName.Name = "materialName";
            this.materialName.Size = new System.Drawing.Size(136, 20);
            this.materialName.TabIndex = 43;
            this.materialName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.materialName_KeyPress);
            // 
            // addMore
            // 
            this.addMore.AutoSize = true;
            this.addMore.Checked = true;
            this.addMore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.addMore.Location = new System.Drawing.Point(15, 217);
            this.addMore.Name = "addMore";
            this.addMore.Size = new System.Drawing.Size(152, 17);
            this.addMore.TabIndex = 46;
            this.addMore.Text = "Добавить еще материал";
            this.addMore.UseVisualStyleBackColor = true;
            // 
            // errorFillAll
            // 
            this.errorFillAll.AutoSize = true;
            this.errorFillAll.ForeColor = System.Drawing.Color.Red;
            this.errorFillAll.Location = new System.Drawing.Point(12, 250);
            this.errorFillAll.Name = "errorFillAll";
            this.errorFillAll.Size = new System.Drawing.Size(112, 13);
            this.errorFillAll.TabIndex = 47;
            this.errorFillAll.Text = "Заполните все поля!";
            this.errorFillAll.Visible = false;
            // 
            // errorInvalidMaterialNumber
            // 
            this.errorInvalidMaterialNumber.AutoSize = true;
            this.errorInvalidMaterialNumber.ForeColor = System.Drawing.Color.Red;
            this.errorInvalidMaterialNumber.Location = new System.Drawing.Point(12, 263);
            this.errorInvalidMaterialNumber.Name = "errorInvalidMaterialNumber";
            this.errorInvalidMaterialNumber.Size = new System.Drawing.Size(236, 13);
            this.errorInvalidMaterialNumber.TabIndex = 48;
            this.errorInvalidMaterialNumber.Text = "Материал с таким номером уже существует!";
            this.errorInvalidMaterialNumber.Visible = false;
            // 
            // tension2
            // 
            this.tension2.Location = new System.Drawing.Point(280, 162);
            this.tension2.Name = "tension2";
            this.tension2.Size = new System.Drawing.Size(136, 20);
            this.tension2.TabIndex = 50;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(230, 13);
            this.label6.TabIndex = 49;
            this.label6.Text = "Допускаемое напряжение сжатия, Н/см^2:";
            // 
            // AddMaterials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 402);
            this.Controls.Add(this.tension2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.errorInvalidMaterialNumber);
            this.Controls.Add(this.errorFillAll);
            this.Controls.Add(this.addMore);
            this.Controls.Add(this.materialName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.errorInvalidValue);
            this.Controls.Add(this.thickness);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tension);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.poissonsRatio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.elasticModulus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddMaterials";
            this.ShowInTaskbar = false;
            this.Text = "Добавить материал";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label errorInvalidValue;
        private System.Windows.Forms.TextBox thickness;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tension;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox poissonsRatio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox elasticModulus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox materialName;
        private System.Windows.Forms.CheckBox addMore;
        private System.Windows.Forms.Label errorFillAll;
        private System.Windows.Forms.Label errorInvalidMaterialNumber;
        private System.Windows.Forms.TextBox tension2;
        private System.Windows.Forms.Label label6;
    }
}