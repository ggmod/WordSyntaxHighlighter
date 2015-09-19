namespace WordCodeEditorTools.WordSyntaxHighlighter
{
    partial class FrameStyleForm
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
            this.OK_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.Default_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.useAltLines_checkBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BgColorButton = new System.Windows.Forms.Button();
            this.AltColorButton = new System.Windows.Forms.Button();
            this.BorderColorButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_Font = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_Font = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Font)).BeginInit();
            this.SuspendLayout();
            // 
            // OK_button
            // 
            this.OK_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK_button.Location = new System.Drawing.Point(371, 227);
            this.OK_button.Name = "OK_button";
            this.OK_button.Size = new System.Drawing.Size(75, 23);
            this.OK_button.TabIndex = 0;
            this.OK_button.Text = "OK";
            this.OK_button.UseVisualStyleBackColor = true;
            // 
            // Cancel_button
            // 
            this.Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_button.Location = new System.Drawing.Point(290, 227);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_button.TabIndex = 1;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            // 
            // Default_button
            // 
            this.Default_button.Location = new System.Drawing.Point(8, 227);
            this.Default_button.Name = "Default_button";
            this.Default_button.Size = new System.Drawing.Size(75, 23);
            this.Default_button.TabIndex = 2;
            this.Default_button.Text = "Default";
            this.Default_button.UseVisualStyleBackColor = true;
            this.Default_button.Click += new System.EventHandler(this.Default_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Background Color";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Use Alternating Lines";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Alternating Color";
            // 
            // useAltLines_checkBox
            // 
            this.useAltLines_checkBox.AutoSize = true;
            this.useAltLines_checkBox.Location = new System.Drawing.Point(28, 79);
            this.useAltLines_checkBox.Name = "useAltLines_checkBox";
            this.useAltLines_checkBox.Size = new System.Drawing.Size(15, 14);
            this.useAltLines_checkBox.TabIndex = 6;
            this.useAltLines_checkBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Border Color";
            // 
            // BgColorButton
            // 
            this.BgColorButton.BackColor = System.Drawing.Color.White;
            this.BgColorButton.FlatAppearance.BorderSize = 0;
            this.BgColorButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BgColorButton.Location = new System.Drawing.Point(24, 39);
            this.BgColorButton.Margin = new System.Windows.Forms.Padding(0);
            this.BgColorButton.Name = "BgColorButton";
            this.BgColorButton.Size = new System.Drawing.Size(24, 24);
            this.BgColorButton.TabIndex = 8;
            this.BgColorButton.UseVisualStyleBackColor = false;
            this.BgColorButton.Click += new System.EventHandler(this.BgColorButton_Click);
            // 
            // AltColorButton
            // 
            this.AltColorButton.BackColor = System.Drawing.Color.White;
            this.AltColorButton.Location = new System.Drawing.Point(24, 106);
            this.AltColorButton.Margin = new System.Windows.Forms.Padding(0);
            this.AltColorButton.Name = "AltColorButton";
            this.AltColorButton.Size = new System.Drawing.Size(24, 24);
            this.AltColorButton.TabIndex = 9;
            this.AltColorButton.UseVisualStyleBackColor = false;
            this.AltColorButton.Click += new System.EventHandler(this.AltColorButton_Click);
            // 
            // BorderColorButton
            // 
            this.BorderColorButton.BackColor = System.Drawing.Color.White;
            this.BorderColorButton.Location = new System.Drawing.Point(24, 141);
            this.BorderColorButton.Margin = new System.Windows.Forms.Padding(0);
            this.BorderColorButton.Name = "BorderColorButton";
            this.BorderColorButton.Size = new System.Drawing.Size(24, 24);
            this.BorderColorButton.TabIndex = 10;
            this.BorderColorButton.UseVisualStyleBackColor = false;
            this.BorderColorButton.Click += new System.EventHandler(this.BorderColorButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AltColorButton);
            this.groupBox1.Controls.Add(this.BorderColorButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.BgColorButton);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.useAltLines_checkBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 196);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Frame Style";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown_Font);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.comboBox_Font);
            this.groupBox2.Location = new System.Drawing.Point(207, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 196);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Font Style";
            // 
            // numericUpDown_Font
            // 
            this.numericUpDown_Font.Location = new System.Drawing.Point(21, 97);
            this.numericUpDown_Font.Name = "numericUpDown_Font";
            this.numericUpDown_Font.Size = new System.Drawing.Size(86, 20);
            this.numericUpDown_Font.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Font Size:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Font Name:";
            // 
            // comboBox_Font
            // 
            this.comboBox_Font.FormattingEnabled = true;
            this.comboBox_Font.Location = new System.Drawing.Point(21, 45);
            this.comboBox_Font.Name = "comboBox_Font";
            this.comboBox_Font.Size = new System.Drawing.Size(198, 21);
            this.comboBox_Font.TabIndex = 0;
            // 
            // FrameStyleForm
            // 
            this.AcceptButton = this.OK_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.Cancel_button;
            this.ClientSize = new System.Drawing.Size(458, 262);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Default_button);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.OK_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrameStyleForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frame and Font Style Selection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Font)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OK_button;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.Button Default_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox useAltLines_checkBox;
        public System.Windows.Forms.Button BgColorButton;
        public System.Windows.Forms.Button AltColorButton;
        public System.Windows.Forms.Button BorderColorButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox comboBox_Font;
        public System.Windows.Forms.NumericUpDown numericUpDown_Font;

    }
}