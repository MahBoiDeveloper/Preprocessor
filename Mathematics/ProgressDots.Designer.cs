namespace PreprocessorUtils
{
    partial class ProgressDots
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
            this.components = new System.ComponentModel.Container();
            this.lblProgressText = new System.Windows.Forms.Label();
            this.dotsTimer = new System.Windows.Forms.Timer(this.components);
            this.lblDots = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblProgressText
            // 
            this.lblProgressText.AutoSize = true;
            this.lblProgressText.Location = new System.Drawing.Point(13, 13);
            this.lblProgressText.Name = "lblProgressText";
            this.lblProgressText.Size = new System.Drawing.Size(0, 13);
            this.lblProgressText.TabIndex = 0;
            // 
            // dotsTimer
            // 
            this.dotsTimer.Interval = 500;
            this.dotsTimer.Tick += new System.EventHandler(this.dotsTimer_Tick);
            // 
            // lblDots
            // 
            this.lblDots.AutoSize = true;
            this.lblDots.Location = new System.Drawing.Point(44, 13);
            this.lblDots.Name = "lblDots";
            this.lblDots.Size = new System.Drawing.Size(16, 13);
            this.lblDots.TabIndex = 1;
            this.lblDots.Text = "...";
            // 
            // ProgressDots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(94, 42);
            this.ControlBox = false;
            this.Controls.Add(this.lblDots);
            this.Controls.Add(this.lblProgressText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressDots";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Подождите";
            this.Load += new System.EventHandler(this.ProgressDots_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProgressText;
        private System.Windows.Forms.Timer dotsTimer;
        private System.Windows.Forms.Label lblDots;
    }
}