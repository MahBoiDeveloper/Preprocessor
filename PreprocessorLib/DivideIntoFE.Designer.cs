namespace PreprocessorLib
{
    partial class DivideIntoFE
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
            this.NRC = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.errorMessage1 = new System.Windows.Forms.Label();
            this.gridName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.errorBadGridName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "NRC:";
            // 
            // NRC
            // 
            this.NRC.Location = new System.Drawing.Point(51, 15);
            this.NRC.Name = "NRC";
            this.NRC.Size = new System.Drawing.Size(45, 20);
            this.NRC.TabIndex = 1;
            this.NRC.Text = "3";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(92, 87);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(172, 87);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // errorMessage1
            // 
            this.errorMessage1.AutoSize = true;
            this.errorMessage1.ForeColor = System.Drawing.Color.Red;
            this.errorMessage1.Location = new System.Drawing.Point(12, 60);
            this.errorMessage1.Name = "errorMessage1";
            this.errorMessage1.Size = new System.Drawing.Size(155, 13);
            this.errorMessage1.TabIndex = 4;
            this.errorMessage1.Text = "В модели нет ни одной зоны!";
            this.errorMessage1.Visible = false;
            // 
            // gridName
            // 
            this.gridName.Location = new System.Drawing.Point(172, 15);
            this.gridName.Name = "gridName";
            this.gridName.Size = new System.Drawing.Size(158, 20);
            this.gridName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Имя сетки:";
            // 
            // errorBadGridName
            // 
            this.errorBadGridName.AutoSize = true;
            this.errorBadGridName.ForeColor = System.Drawing.Color.Red;
            this.errorBadGridName.Location = new System.Drawing.Point(12, 47);
            this.errorBadGridName.Name = "errorBadGridName";
            this.errorBadGridName.Size = new System.Drawing.Size(210, 13);
            this.errorBadGridName.TabIndex = 7;
            this.errorBadGridName.Text = "Сетка с таким именем уже существует!";
            this.errorBadGridName.Visible = false;
            // 
            // DivideIntoFE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(340, 125);
            this.Controls.Add(this.errorBadGridName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gridName);
            this.Controls.Add(this.errorMessage1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.NRC);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DivideIntoFE";
            this.ShowInTaskbar = false;
            this.Text = "Создание сетки КЭ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DivideIntoFE_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NRC;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label errorMessage1;
        private System.Windows.Forms.TextBox gridName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label errorBadGridName;
    }
}