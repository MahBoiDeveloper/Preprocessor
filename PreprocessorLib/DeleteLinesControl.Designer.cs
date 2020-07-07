namespace PreprocessorLib
{
    partial class DeleteLinesControl
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
            this.errorLineDoesNotExist = new System.Windows.Forms.Label();
            this.number = new System.Windows.Forms.TextBox();
            this.errorMessage2 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // errorLineDoesNotExist
            // 
            this.errorLineDoesNotExist.AutoSize = true;
            this.errorLineDoesNotExist.ForeColor = System.Drawing.Color.Red;
            this.errorLineDoesNotExist.Location = new System.Drawing.Point(3, 36);
            this.errorLineDoesNotExist.Name = "errorLineDoesNotExist";
            this.errorLineDoesNotExist.Size = new System.Drawing.Size(151, 13);
            this.errorLineDoesNotExist.TabIndex = 50;
            this.errorLineDoesNotExist.Text = "Такая линия не существует!";
            this.errorLineDoesNotExist.Visible = false;
            // 
            // number
            // 
            this.number.Location = new System.Drawing.Point(97, 0);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(68, 20);
            this.number.TabIndex = 46;
            this.number.TextChanged += new System.EventHandler(this.number_TextChanged);
            // 
            // errorMessage2
            // 
            this.errorMessage2.AutoSize = true;
            this.errorMessage2.ForeColor = System.Drawing.Color.Red;
            this.errorMessage2.Location = new System.Drawing.Point(3, 21);
            this.errorMessage2.Name = "errorMessage2";
            this.errorMessage2.Size = new System.Drawing.Size(134, 13);
            this.errorMessage2.TabIndex = 49;
            this.errorMessage2.Text = "Недопустимое значение!";
            this.errorMessage2.Visible = false;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(79, 52);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 48;
            this.cancelButton.Text = "Закрыть";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(3, 52);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 47;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Удалить линию:";
            // 
            // DeleteLinesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.errorLineDoesNotExist);
            this.Controls.Add(this.number);
            this.Controls.Add(this.errorMessage2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(20, 270);
            this.Name = "DeleteLinesControl";
            this.Size = new System.Drawing.Size(280, 91);
            this.Load += new System.EventHandler(this.DeleteLinesControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label errorLineDoesNotExist;
        public System.Windows.Forms.TextBox number;
        private System.Windows.Forms.Label errorMessage2;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
    }
}
