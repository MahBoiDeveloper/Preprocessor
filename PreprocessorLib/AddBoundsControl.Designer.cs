namespace PreprocessorLib
{
    partial class AddBoundsControl
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
            this.errorNoLine = new System.Windows.Forms.Label();
            this.yBound = new System.Windows.Forms.CheckBox();
            this.xBound = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.errorBoundType = new System.Windows.Forms.Label();
            this.txtNode = new System.Windows.Forms.TextBox();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.rbnOnNode = new System.Windows.Forms.RadioButton();
            this.rbnOnLine = new System.Windows.Forms.RadioButton();
            this.lblNoNode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // errorNoLine
            // 
            this.errorNoLine.AutoSize = true;
            this.errorNoLine.ForeColor = System.Drawing.Color.Red;
            this.errorNoLine.Location = new System.Drawing.Point(-3, 172);
            this.errorNoLine.Name = "errorNoLine";
            this.errorNoLine.Size = new System.Drawing.Size(94, 13);
            this.errorNoLine.TabIndex = 71;
            this.errorNoLine.Text = "Нет такой линии!";
            this.errorNoLine.Visible = false;
            // 
            // yBound
            // 
            this.yBound.AutoSize = true;
            this.yBound.Location = new System.Drawing.Point(54, 108);
            this.yBound.Name = "yBound";
            this.yBound.Size = new System.Drawing.Size(33, 17);
            this.yBound.TabIndex = 65;
            this.yBound.Text = "Y";
            this.yBound.UseVisualStyleBackColor = true;
            // 
            // xBound
            // 
            this.xBound.AutoSize = true;
            this.xBound.Location = new System.Drawing.Point(8, 109);
            this.xBound.Name = "xBound";
            this.xBound.Size = new System.Drawing.Size(33, 17);
            this.xBound.TabIndex = 64;
            this.xBound.Text = "X";
            this.xBound.UseVisualStyleBackColor = true;
            this.xBound.CheckedChanged += new System.EventHandler(this.xBound_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 70;
            this.label2.Text = "Тип закрепления:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 69;
            this.label1.Text = "Закрепить:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(84, 130);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 68;
            this.cancelButton.Text = "Закрыть";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(3, 130);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 67;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // errorBoundType
            // 
            this.errorBoundType.AutoSize = true;
            this.errorBoundType.ForeColor = System.Drawing.Color.Red;
            this.errorBoundType.Location = new System.Drawing.Point(-3, 185);
            this.errorBoundType.Name = "errorBoundType";
            this.errorBoundType.Size = new System.Drawing.Size(144, 13);
            this.errorBoundType.TabIndex = 72;
            this.errorBoundType.Text = "Укажите тип закрепления!";
            this.errorBoundType.Visible = false;
            // 
            // txtNode
            // 
            this.txtNode.Location = new System.Drawing.Point(126, 50);
            this.txtNode.Name = "txtNode";
            this.txtNode.Size = new System.Drawing.Size(72, 20);
            this.txtNode.TabIndex = 86;
            this.txtNode.Enter += new System.EventHandler(this.txtNode_Enter);
            // 
            // txtLine
            // 
            this.txtLine.Location = new System.Drawing.Point(126, 28);
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(72, 20);
            this.txtLine.TabIndex = 85;
            this.txtLine.Enter += new System.EventHandler(this.txtLine_Enter);
            // 
            // rbnOnNode
            // 
            this.rbnOnNode.AutoSize = true;
            this.rbnOnNode.Location = new System.Drawing.Point(6, 51);
            this.rbnOnNode.Name = "rbnOnNode";
            this.rbnOnNode.Size = new System.Drawing.Size(106, 17);
            this.rbnOnNode.TabIndex = 84;
            this.rbnOnNode.Text = "по номеру узла:";
            this.rbnOnNode.UseVisualStyleBackColor = true;
            // 
            // rbnOnLine
            // 
            this.rbnOnLine.AutoSize = true;
            this.rbnOnLine.Checked = true;
            this.rbnOnLine.Location = new System.Drawing.Point(6, 28);
            this.rbnOnLine.Name = "rbnOnLine";
            this.rbnOnLine.Size = new System.Drawing.Size(113, 17);
            this.rbnOnLine.TabIndex = 83;
            this.rbnOnLine.TabStop = true;
            this.rbnOnLine.Text = "по номеру линии:";
            this.rbnOnLine.UseVisualStyleBackColor = true;
            // 
            // lblNoNode
            // 
            this.lblNoNode.AutoSize = true;
            this.lblNoNode.ForeColor = System.Drawing.Color.Red;
            this.lblNoNode.Location = new System.Drawing.Point(-3, 159);
            this.lblNoNode.Name = "lblNoNode";
            this.lblNoNode.Size = new System.Drawing.Size(92, 13);
            this.lblNoNode.TabIndex = 87;
            this.lblNoNode.Text = "Нет такого узла!";
            this.lblNoNode.Visible = false;
            // 
            // AddBoundsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNoNode);
            this.Controls.Add(this.txtNode);
            this.Controls.Add(this.txtLine);
            this.Controls.Add(this.rbnOnNode);
            this.Controls.Add(this.rbnOnLine);
            this.Controls.Add(this.errorBoundType);
            this.Controls.Add(this.errorNoLine);
            this.Controls.Add(this.yBound);
            this.Controls.Add(this.xBound);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Location = new System.Drawing.Point(20, 270);
            this.Name = "AddBoundsControl";
            this.Size = new System.Drawing.Size(218, 203);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label errorNoLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        public System.Windows.Forms.CheckBox yBound;
        public System.Windows.Forms.CheckBox xBound;
        public System.Windows.Forms.Label errorBoundType;
        private System.Windows.Forms.TextBox txtNode;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.RadioButton rbnOnNode;
        private System.Windows.Forms.RadioButton rbnOnLine;
        private System.Windows.Forms.Label lblNoNode;
    }
}
