namespace PreprocessorLib
{
    partial class ExportForm
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
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.FormOnly = new System.Windows.Forms.RadioButton();
            this.WholeModel = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(32, 63);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(113, 63);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // FormOnly
            // 
            this.FormOnly.AutoSize = true;
            this.FormOnly.Checked = true;
            this.FormOnly.Location = new System.Drawing.Point(22, 12);
            this.FormOnly.Name = "FormOnly";
            this.FormOnly.Size = new System.Drawing.Size(182, 17);
            this.FormOnly.TabIndex = 2;
            this.FormOnly.TabStop = true;
            this.FormOnly.Text = "Экспортировать только форму";
            this.FormOnly.UseVisualStyleBackColor = true;
            // 
            // WholeModel
            // 
            this.WholeModel.AutoSize = true;
            this.WholeModel.Location = new System.Drawing.Point(22, 35);
            this.WholeModel.Name = "WholeModel";
            this.WholeModel.Size = new System.Drawing.Size(196, 17);
            this.WholeModel.TabIndex = 3;
            this.WholeModel.TabStop = true;
            this.WholeModel.Text = "Экспортировать модель целиком";
            this.WholeModel.UseVisualStyleBackColor = true;
            this.WholeModel.CheckedChanged += new System.EventHandler(this.WholeModel_CheckedChanged);
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 98);
            this.Controls.Add(this.WholeModel);
            this.Controls.Add(this.FormOnly);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Экспорт";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.RadioButton FormOnly;
        private System.Windows.Forms.RadioButton WholeModel;
    }
}