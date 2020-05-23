namespace PreprocessorLib
{
    partial class AddAreasControl
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
            this.errorMessage4 = new System.Windows.Forms.Label();
            this.errorMessage3 = new System.Windows.Forms.Label();
            this.line4 = new System.Windows.Forms.TextBox();
            this.line3 = new System.Windows.Forms.TextBox();
            this.errorMessage2 = new System.Windows.Forms.Label();
            this.errorMessage1 = new System.Windows.Forms.Label();
            this.line2 = new System.Windows.Forms.TextBox();
            this.line1 = new System.Windows.Forms.TextBox();
            this.number = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.errorAreaExists = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.autoSearch = new System.Windows.Forms.RadioButton();
            this.manualSearch = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.errorTwoAreas = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // errorMessage4
            // 
            this.errorMessage4.AutoSize = true;
            this.errorMessage4.ForeColor = System.Drawing.Color.Red;
            this.errorMessage4.Location = new System.Drawing.Point(3, 151);
            this.errorMessage4.Name = "errorMessage4";
            this.errorMessage4.Size = new System.Drawing.Size(180, 13);
            this.errorMessage4.TabIndex = 83;
            this.errorMessage4.Text = "Указанные линии не существуют!";
            this.errorMessage4.Visible = false;
            // 
            // errorMessage3
            // 
            this.errorMessage3.AutoSize = true;
            this.errorMessage3.ForeColor = System.Drawing.Color.Red;
            this.errorMessage3.Location = new System.Drawing.Point(3, 138);
            this.errorMessage3.Name = "errorMessage3";
            this.errorMessage3.Size = new System.Drawing.Size(192, 13);
            this.errorMessage3.TabIndex = 82;
            this.errorMessage3.Text = "Линии не образуют замкнутую зону!";
            this.errorMessage3.Visible = false;
            // 
            // line4
            // 
            this.line4.Location = new System.Drawing.Point(174, 89);
            this.line4.Name = "line4";
            this.line4.Size = new System.Drawing.Size(50, 20);
            this.line4.TabIndex = 71;
            this.line4.TextChanged += new System.EventHandler(this.line4_TextChanged);
            this.line4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.line4_KeyPress);
            // 
            // line3
            // 
            this.line3.Location = new System.Drawing.Point(118, 89);
            this.line3.Name = "line3";
            this.line3.Size = new System.Drawing.Size(50, 20);
            this.line3.TabIndex = 70;
            this.line3.TextChanged += new System.EventHandler(this.line3_TextChanged);
            this.line3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.line3_KeyPress);
            // 
            // errorMessage2
            // 
            this.errorMessage2.AutoSize = true;
            this.errorMessage2.ForeColor = System.Drawing.Color.Red;
            this.errorMessage2.Location = new System.Drawing.Point(3, 125);
            this.errorMessage2.Name = "errorMessage2";
            this.errorMessage2.Size = new System.Drawing.Size(134, 13);
            this.errorMessage2.TabIndex = 79;
            this.errorMessage2.Text = "Недопустимое значение!";
            this.errorMessage2.Visible = false;
            // 
            // errorMessage1
            // 
            this.errorMessage1.AutoSize = true;
            this.errorMessage1.ForeColor = System.Drawing.Color.Red;
            this.errorMessage1.Location = new System.Drawing.Point(3, 112);
            this.errorMessage1.Name = "errorMessage1";
            this.errorMessage1.Size = new System.Drawing.Size(112, 13);
            this.errorMessage1.TabIndex = 78;
            this.errorMessage1.Text = "Заплоните все поля!";
            this.errorMessage1.Visible = false;
            // 
            // line2
            // 
            this.line2.Location = new System.Drawing.Point(62, 89);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(50, 20);
            this.line2.TabIndex = 69;
            this.line2.TextChanged += new System.EventHandler(this.line2_TextChanged);
            this.line2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.line2_KeyPress);
            // 
            // line1
            // 
            this.line1.Location = new System.Drawing.Point(6, 89);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(50, 20);
            this.line1.TabIndex = 68;
            this.line1.TextChanged += new System.EventHandler(this.line1_TextChanged);
            this.line1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.line1_KeyPress);
            // 
            // number
            // 
            this.number.Location = new System.Drawing.Point(155, 51);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(69, 20);
            this.number.TabIndex = 67;
            this.number.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.number_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Номер зоны:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(87, 191);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 74;
            this.cancelButton.Text = "Закрыть";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(6, 191);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 73;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // errorAreaExists
            // 
            this.errorAreaExists.AutoSize = true;
            this.errorAreaExists.ForeColor = System.Drawing.Color.Red;
            this.errorAreaExists.Location = new System.Drawing.Point(3, 164);
            this.errorAreaExists.Name = "errorAreaExists";
            this.errorAreaExists.Size = new System.Drawing.Size(119, 13);
            this.errorAreaExists.TabIndex = 85;
            this.errorAreaExists.Text = "Зона уже существует!";
            this.errorAreaExists.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 86;
            this.label4.Text = "Режим поиска линий:";
            // 
            // autoSearch
            // 
            this.autoSearch.AutoSize = true;
            this.autoSearch.Checked = true;
            this.autoSearch.Location = new System.Drawing.Point(6, 20);
            this.autoSearch.Name = "autoSearch";
            this.autoSearch.Size = new System.Drawing.Size(109, 17);
            this.autoSearch.TabIndex = 87;
            this.autoSearch.TabStop = true;
            this.autoSearch.Text = "Автоматический";
            this.autoSearch.UseVisualStyleBackColor = true;
            // 
            // manualSearch
            // 
            this.manualSearch.AutoSize = true;
            this.manualSearch.Location = new System.Drawing.Point(122, 20);
            this.manualSearch.Name = "manualSearch";
            this.manualSearch.Size = new System.Drawing.Size(60, 17);
            this.manualSearch.TabIndex = 88;
            this.manualSearch.Text = "Ручной";
            this.manualSearch.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 89;
            this.label2.Text = "Номера линий:";
            // 
            // errorTwoAreas
            // 
            this.errorTwoAreas.AutoSize = true;
            this.errorTwoAreas.ForeColor = System.Drawing.Color.Red;
            this.errorTwoAreas.Location = new System.Drawing.Point(3, 177);
            this.errorTwoAreas.Name = "errorTwoAreas";
            this.errorTwoAreas.Size = new System.Drawing.Size(255, 13);
            this.errorTwoAreas.TabIndex = 90;
            this.errorTwoAreas.Text = "Линия может принадлежать только двум зонам!";
            this.errorTwoAreas.Visible = false;
            // 
            // AddAreasControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.errorTwoAreas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.manualSearch);
            this.Controls.Add(this.autoSearch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.errorAreaExists);
            this.Controls.Add(this.errorMessage4);
            this.Controls.Add(this.errorMessage3);
            this.Controls.Add(this.line4);
            this.Controls.Add(this.line3);
            this.Controls.Add(this.errorMessage2);
            this.Controls.Add(this.errorMessage1);
            this.Controls.Add(this.line2);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.number);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Location = new System.Drawing.Point(20, 270);
            this.Name = "AddAreasControl";
            this.Size = new System.Drawing.Size(280, 220);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label errorMessage4;
        private System.Windows.Forms.Label errorMessage3;
        private System.Windows.Forms.Label errorMessage2;
        private System.Windows.Forms.Label errorMessage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        public System.Windows.Forms.TextBox line4;
        public System.Windows.Forms.TextBox line3;
        public System.Windows.Forms.TextBox line2;
        public System.Windows.Forms.TextBox line1;
        public System.Windows.Forms.TextBox number;
        private System.Windows.Forms.Label errorAreaExists;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.RadioButton autoSearch;
        public System.Windows.Forms.RadioButton manualSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label errorTwoAreas;
    }
}
