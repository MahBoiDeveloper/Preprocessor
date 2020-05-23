namespace PreprocessorLib
{
    partial class AddStraightLinesControl
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
            this.endPoint = new System.Windows.Forms.TextBox();
            this.startPoint = new System.Windows.Forms.TextBox();
            this.number = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.errorSamePoints = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // errorMessage2
            // 
            this.errorMessage2.AutoSize = true;
            this.errorMessage2.ForeColor = System.Drawing.Color.Red;
            this.errorMessage2.Location = new System.Drawing.Point(3, 91);
            this.errorMessage2.Name = "errorMessage2";
            this.errorMessage2.Size = new System.Drawing.Size(134, 13);
            this.errorMessage2.TabIndex = 24;
            this.errorMessage2.Text = "Недопустимое значение!";
            this.errorMessage2.Visible = false;
            // 
            // errorMessage1
            // 
            this.errorMessage1.AutoSize = true;
            this.errorMessage1.ForeColor = System.Drawing.Color.Red;
            this.errorMessage1.Location = new System.Drawing.Point(3, 78);
            this.errorMessage1.Name = "errorMessage1";
            this.errorMessage1.Size = new System.Drawing.Size(112, 13);
            this.errorMessage1.TabIndex = 23;
            this.errorMessage1.Text = "Заплоните все поля!";
            this.errorMessage1.Visible = false;
            // 
            // endPoint
            // 
            this.endPoint.Location = new System.Drawing.Point(140, 52);
            this.endPoint.Name = "endPoint";
            this.endPoint.Size = new System.Drawing.Size(79, 20);
            this.endPoint.TabIndex = 22;
            this.endPoint.TextChanged += new System.EventHandler(this.endPoint_TextChanged);
            this.endPoint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.endPoint_KeyPress);
            // 
            // startPoint
            // 
            this.startPoint.Location = new System.Drawing.Point(140, 26);
            this.startPoint.Name = "startPoint";
            this.startPoint.Size = new System.Drawing.Size(79, 20);
            this.startPoint.TabIndex = 21;
            this.startPoint.TextChanged += new System.EventHandler(this.startPoint_TextChanged);
            this.startPoint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.startPoint_KeyPress);
            // 
            // number
            // 
            this.number.Location = new System.Drawing.Point(140, 0);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(79, 20);
            this.number.TabIndex = 20;
            this.number.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.number_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Номер конечной точки:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Номер начальной точки:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Номер линии:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(86, 121);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Закрыть";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(5, 121);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 15;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // errorSamePoints
            // 
            this.errorSamePoints.AutoSize = true;
            this.errorSamePoints.ForeColor = System.Drawing.Color.Red;
            this.errorSamePoints.Location = new System.Drawing.Point(3, 105);
            this.errorSamePoints.Name = "errorSamePoints";
            this.errorSamePoints.Size = new System.Drawing.Size(269, 13);
            this.errorSamePoints.TabIndex = 25;
            this.errorSamePoints.Text = "Начальная и конечная точки не должны совпадать!";
            this.errorSamePoints.Visible = false;
            // 
            // AddStraightLinesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.errorSamePoints);
            this.Controls.Add(this.errorMessage2);
            this.Controls.Add(this.errorMessage1);
            this.Controls.Add(this.endPoint);
            this.Controls.Add(this.startPoint);
            this.Controls.Add(this.number);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Location = new System.Drawing.Point(20, 270);
            this.Name = "AddStraightLinesControl";
            this.Size = new System.Drawing.Size(280, 159);
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
        public System.Windows.Forms.TextBox number;
        public System.Windows.Forms.TextBox endPoint;
        public System.Windows.Forms.TextBox startPoint;
        private System.Windows.Forms.Label errorSamePoints;
    }
}
