namespace PreprocessorLib
{
    partial class AddCirclesControl
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
            this.errorMessage2 = new System.Windows.Forms.Label();
            this.errorMessage1 = new System.Windows.Forms.Label();
            this.radius = new System.Windows.Forms.TextBox();
            this.centerPoint = new System.Windows.Forms.TextBox();
            this.number = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.errorPointDoesNotExist = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // errorMessage2
            // 
            this.errorMessage2.AutoSize = true;
            this.errorMessage2.ForeColor = System.Drawing.Color.Red;
            this.errorMessage2.Location = new System.Drawing.Point(3, 120);
            this.errorMessage2.Name = "errorMessage2";
            this.errorMessage2.Size = new System.Drawing.Size(134, 13);
            this.errorMessage2.TabIndex = 34;
            this.errorMessage2.Text = "Недопустимое значение!";
            this.errorMessage2.Visible = false;
            // 
            // errorMessage1
            // 
            this.errorMessage1.AutoSize = true;
            this.errorMessage1.ForeColor = System.Drawing.Color.Red;
            this.errorMessage1.Location = new System.Drawing.Point(3, 107);
            this.errorMessage1.Name = "errorMessage1";
            this.errorMessage1.Size = new System.Drawing.Size(112, 13);
            this.errorMessage1.TabIndex = 33;
            this.errorMessage1.Text = "Заплоните все поля!";
            this.errorMessage1.Visible = false;
            // 
            // radius
            // 
            this.radius.Location = new System.Drawing.Point(174, 52);
            this.radius.Name = "radius";
            this.radius.Size = new System.Drawing.Size(65, 20);
            this.radius.TabIndex = 32;
            this.radius.TextChanged += new System.EventHandler(this.radius_TextChanged);
            this.radius.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.radius_KeyPress);
            // 
            // centerPoint
            // 
            this.centerPoint.Location = new System.Drawing.Point(174, 26);
            this.centerPoint.Name = "centerPoint";
            this.centerPoint.Size = new System.Drawing.Size(65, 20);
            this.centerPoint.TabIndex = 31;
            this.centerPoint.TextChanged += new System.EventHandler(this.centerPoint_TextChanged);
            this.centerPoint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.centerPoint_KeyPress);
            // 
            // number
            // 
            this.number.Location = new System.Drawing.Point(174, 0);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(65, 20);
            this.number.TabIndex = 30;
            this.number.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.number_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Радиус:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Номер центральной точки точки:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Номер окружности:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(86, 136);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 26;
            this.cancelButton.Text = "Закрыть";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(5, 136);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 25;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // errorPointDoesNotExist
            // 
            this.errorPointDoesNotExist.AutoSize = true;
            this.errorPointDoesNotExist.ForeColor = System.Drawing.Color.Red;
            this.errorPointDoesNotExist.Location = new System.Drawing.Point(3, 94);
            this.errorPointDoesNotExist.Name = "errorPointDoesNotExist";
            this.errorPointDoesNotExist.Size = new System.Drawing.Size(149, 13);
            this.errorPointDoesNotExist.TabIndex = 35;
            this.errorPointDoesNotExist.Text = "Такая точка не существует!";
            this.errorPointDoesNotExist.Visible = false;
            // 
            // AddCirclesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.errorPointDoesNotExist);
            this.Controls.Add(this.errorMessage2);
            this.Controls.Add(this.errorMessage1);
            this.Controls.Add(this.radius);
            this.Controls.Add(this.centerPoint);
            this.Controls.Add(this.number);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Location = new System.Drawing.Point(20, 270);
            this.Name = "AddCirclesControl";
            this.Size = new System.Drawing.Size(251, 167);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label errorMessage2;
        private System.Windows.Forms.Label errorMessage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label errorPointDoesNotExist;
        public System.Windows.Forms.TextBox radius;
        public System.Windows.Forms.TextBox centerPoint;
        public System.Windows.Forms.TextBox number;
    }
}
