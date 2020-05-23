namespace PreprocessorLib
{
    partial class AddArcsControl
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
            this.counterСlockwise = new System.Windows.Forms.RadioButton();
            this.сlockwise = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.center2Point = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.errorMessage2 = new System.Windows.Forms.Label();
            this.errorMessage1 = new System.Windows.Forms.Label();
            this.end2Point = new System.Windows.Forms.TextBox();
            this.start2Point = new System.Windows.Forms.TextBox();
            this.number = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // counterСlockwise
            // 
            this.counterСlockwise.AutoSize = true;
            this.counterСlockwise.Location = new System.Drawing.Point(6, 144);
            this.counterСlockwise.Name = "counterСlockwise";
            this.counterСlockwise.Size = new System.Drawing.Size(150, 17);
            this.counterСlockwise.TabIndex = 41;
            this.counterСlockwise.Text = "Против часовой стрелки";
            this.counterСlockwise.UseVisualStyleBackColor = true;
            // 
            // сlockwise
            // 
            this.сlockwise.AutoSize = true;
            this.сlockwise.Checked = true;
            this.сlockwise.Location = new System.Drawing.Point(6, 124);
            this.сlockwise.Name = "сlockwise";
            this.сlockwise.Size = new System.Drawing.Size(127, 17);
            this.сlockwise.TabIndex = 40;
            this.сlockwise.TabStop = true;
            this.сlockwise.Text = "По часовой стрелке";
            this.сlockwise.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 52;
            this.label9.Text = "Направление:";
            // 
            // center2Point
            // 
            this.center2Point.Location = new System.Drawing.Point(155, 81);
            this.center2Point.Name = "center2Point";
            this.center2Point.Size = new System.Drawing.Size(79, 20);
            this.center2Point.TabIndex = 39;
            this.center2Point.TextChanged += new System.EventHandler(this.center2Point_TextChanged);
            this.center2Point.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.center2Point_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 13);
            this.label5.TabIndex = 51;
            this.label5.Text = "Номер центральной точки:";
            // 
            // errorMessage2
            // 
            this.errorMessage2.AutoSize = true;
            this.errorMessage2.ForeColor = System.Drawing.Color.Red;
            this.errorMessage2.Location = new System.Drawing.Point(3, 177);
            this.errorMessage2.Name = "errorMessage2";
            this.errorMessage2.Size = new System.Drawing.Size(134, 13);
            this.errorMessage2.TabIndex = 49;
            this.errorMessage2.Text = "Недопустимое значение!";
            this.errorMessage2.Visible = false;
            // 
            // errorMessage1
            // 
            this.errorMessage1.AutoSize = true;
            this.errorMessage1.ForeColor = System.Drawing.Color.Red;
            this.errorMessage1.Location = new System.Drawing.Point(3, 164);
            this.errorMessage1.Name = "errorMessage1";
            this.errorMessage1.Size = new System.Drawing.Size(112, 13);
            this.errorMessage1.TabIndex = 48;
            this.errorMessage1.Text = "Заплоните все поля!";
            this.errorMessage1.Visible = false;
            this.errorMessage1.Click += new System.EventHandler(this.errorMessage1_Click);
            // 
            // end2Point
            // 
            this.end2Point.Location = new System.Drawing.Point(155, 55);
            this.end2Point.Name = "end2Point";
            this.end2Point.Size = new System.Drawing.Size(79, 20);
            this.end2Point.TabIndex = 38;
            this.end2Point.TextChanged += new System.EventHandler(this.end2Point_TextChanged);
            this.end2Point.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.end2Point_KeyPress);
            // 
            // start2Point
            // 
            this.start2Point.Location = new System.Drawing.Point(155, 29);
            this.start2Point.Name = "start2Point";
            this.start2Point.Size = new System.Drawing.Size(79, 20);
            this.start2Point.TabIndex = 37;
            this.start2Point.TextChanged += new System.EventHandler(this.start2Point_TextChanged);
            this.start2Point.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.start2Point_KeyPress);
            // 
            // number
            // 
            this.number.Location = new System.Drawing.Point(155, 3);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(79, 20);
            this.number.TabIndex = 36;
            this.number.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.number_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "Номер конечной точки:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Номер начальной точки:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Номер дуги:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(87, 193);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(59, 21);
            this.cancelButton.TabIndex = 44;
            this.cancelButton.Text = "Закрыть";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(6, 193);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(66, 21);
            this.okButton.TabIndex = 43;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // AddArcsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.counterСlockwise);
            this.Controls.Add(this.сlockwise);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.center2Point);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.errorMessage2);
            this.Controls.Add(this.errorMessage1);
            this.Controls.Add(this.end2Point);
            this.Controls.Add(this.start2Point);
            this.Controls.Add(this.number);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Location = new System.Drawing.Point(20, 270);
            this.Name = "AddArcsControl";
            this.Size = new System.Drawing.Size(235, 215);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label errorMessage2;
        private System.Windows.Forms.Label errorMessage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        public System.Windows.Forms.RadioButton counterСlockwise;
        public System.Windows.Forms.RadioButton сlockwise;
        public System.Windows.Forms.TextBox center2Point;
        public System.Windows.Forms.TextBox end2Point;
        public System.Windows.Forms.TextBox start2Point;
        public System.Windows.Forms.TextBox number;
    }
}
