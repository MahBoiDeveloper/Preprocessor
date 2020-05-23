namespace PreprocessorLib
{
    partial class RuppertForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.errorBadGridName = new System.Windows.Forms.Label();
            this.errorMessage1 = new System.Windows.Forms.Label();
            this.notMoveLines = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gridName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(141, 93);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 24;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(60, 93);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 23;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // errorBadGridName
            // 
            this.errorBadGridName.AutoSize = true;
            this.errorBadGridName.ForeColor = System.Drawing.Color.Red;
            this.errorBadGridName.Location = new System.Drawing.Point(12, 55);
            this.errorBadGridName.Name = "errorBadGridName";
            this.errorBadGridName.Size = new System.Drawing.Size(210, 13);
            this.errorBadGridName.TabIndex = 22;
            this.errorBadGridName.Text = "Сетка с таким именем уже существует!";
            this.errorBadGridName.Visible = false;
            // 
            // errorMessage1
            // 
            this.errorMessage1.AutoSize = true;
            this.errorMessage1.ForeColor = System.Drawing.Color.Red;
            this.errorMessage1.Location = new System.Drawing.Point(12, 68);
            this.errorMessage1.Name = "errorMessage1";
            this.errorMessage1.Size = new System.Drawing.Size(155, 13);
            this.errorMessage1.TabIndex = 21;
            this.errorMessage1.Text = "В модели нет ни одной зоны!";
            this.errorMessage1.Visible = false;
            // 
            // notMoveLines
            // 
            this.notMoveLines.Location = new System.Drawing.Point(141, 32);
            this.notMoveLines.Name = "notMoveLines";
            this.notMoveLines.Size = new System.Drawing.Size(102, 20);
            this.notMoveLines.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Зафиксировать линии:";
            // 
            // gridName
            // 
            this.gridName.Location = new System.Drawing.Point(82, 6);
            this.gridName.Name = "gridName";
            this.gridName.Size = new System.Drawing.Size(161, 20);
            this.gridName.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Имя сетки:";
            // 
            // RuppertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 132);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.errorBadGridName);
            this.Controls.Add(this.errorMessage1);
            this.Controls.Add(this.notMoveLines);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gridName);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RuppertForm";
            this.Text = "Оптимизация алгоритмами Рапперта";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RuppertForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label errorBadGridName;
        private System.Windows.Forms.Label errorMessage1;
        public System.Windows.Forms.TextBox notMoveLines;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox gridName;
        private System.Windows.Forms.Label label2;
    }
}