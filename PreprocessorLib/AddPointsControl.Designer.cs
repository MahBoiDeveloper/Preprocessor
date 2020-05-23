namespace PreprocessorLib
{
    partial class AddPointsControl
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
            this.number = new System.Windows.Forms.TextBox();
            this.errorMessage2 = new System.Windows.Forms.Label();
            this.errorMessage1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.y = new System.Windows.Forms.TextBox();
            this.x = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // number
            // 
            this.number.Location = new System.Drawing.Point(79, 0);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(68, 20);
            this.number.TabIndex = 14;
            this.number.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.number_KeyPress);
            // 
            // errorMessage2
            // 
            this.errorMessage2.AutoSize = true;
            this.errorMessage2.ForeColor = System.Drawing.Color.Red;
            this.errorMessage2.Location = new System.Drawing.Point(3, 88);
            this.errorMessage2.Name = "errorMessage2";
            this.errorMessage2.Size = new System.Drawing.Size(134, 13);
            this.errorMessage2.TabIndex = 22;
            this.errorMessage2.Text = "Недопустимое значение!";
            this.errorMessage2.Visible = false;
            // 
            // errorMessage1
            // 
            this.errorMessage1.AutoSize = true;
            this.errorMessage1.ForeColor = System.Drawing.Color.Red;
            this.errorMessage1.Location = new System.Drawing.Point(3, 75);
            this.errorMessage1.Name = "errorMessage1";
            this.errorMessage1.Size = new System.Drawing.Size(112, 13);
            this.errorMessage1.TabIndex = 21;
            this.errorMessage1.Text = "Заплоните все поля!";
            this.errorMessage1.Visible = false;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(84, 104);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 20;
            this.cancelButton.Text = "Закрыть";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(3, 104);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 19;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // y
            // 
            this.y.Location = new System.Drawing.Point(79, 52);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(68, 20);
            this.y.TabIndex = 18;
            this.y.TextChanged += new System.EventHandler(this.y_TextChanged);
            this.y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.y_KeyPress);
            // 
            // x
            // 
            this.x.Location = new System.Drawing.Point(79, 26);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(68, 20);
            this.x.TabIndex = 17;
            this.x.TextChanged += new System.EventHandler(this.x_TextChanged);
            this.x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.x_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "X:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Номер точки:";
            // 
            // AddPointsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.number);
            this.Controls.Add(this.errorMessage2);
            this.Controls.Add(this.errorMessage1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.y);
            this.Controls.Add(this.x);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(20, 270);
            this.Name = "AddPointsControl";
            this.Size = new System.Drawing.Size(188, 151);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label errorMessage2;
        private System.Windows.Forms.Label errorMessage1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox number;
        public System.Windows.Forms.TextBox y;
        public System.Windows.Forms.TextBox x;
    }
}
