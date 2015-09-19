namespace WordCodeEditorTools.CodeIndentation
{
    partial class IndentSettingsForm
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
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_LineWrap = new System.Windows.Forms.CheckBox();
            this.label_TabStopSize = new System.Windows.Forms.Label();
            this.numericUpDown_TabStopSize = new System.Windows.Forms.NumericUpDown();
            this.checkBox_UseTabStops = new System.Windows.Forms.CheckBox();
            this.numericUpDown_Spaces = new System.Windows.Forms.NumericUpDown();
            this.radioButton_Tab = new System.Windows.Forms.RadioButton();
            this.radioButton_Spaces = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TabStopSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Spaces)).BeginInit();
            this.SuspendLayout();
            // 
            // button_OK
            // 
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(292, 202);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 0;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(211, 202);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 1;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_LineWrap);
            this.groupBox1.Controls.Add(this.label_TabStopSize);
            this.groupBox1.Controls.Add(this.numericUpDown_TabStopSize);
            this.groupBox1.Controls.Add(this.checkBox_UseTabStops);
            this.groupBox1.Controls.Add(this.numericUpDown_Spaces);
            this.groupBox1.Controls.Add(this.radioButton_Tab);
            this.groupBox1.Controls.Add(this.radioButton_Spaces);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 184);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Indenting unit";
            // 
            // checkBox_LineWrap
            // 
            this.checkBox_LineWrap.AutoSize = true;
            this.checkBox_LineWrap.Location = new System.Drawing.Point(147, 136);
            this.checkBox_LineWrap.Name = "checkBox_LineWrap";
            this.checkBox_LineWrap.Size = new System.Drawing.Size(181, 17);
            this.checkBox_LineWrap.TabIndex = 12;
            this.checkBox_LineWrap.Text = "Wrap Lines With Hanging Indent";
            this.checkBox_LineWrap.UseVisualStyleBackColor = true;
            // 
            // label_TabStopSize
            // 
            this.label_TabStopSize.AutoSize = true;
            this.label_TabStopSize.Location = new System.Drawing.Point(144, 112);
            this.label_TabStopSize.Name = "label_TabStopSize";
            this.label_TabStopSize.Size = new System.Drawing.Size(78, 13);
            this.label_TabStopSize.TabIndex = 11;
            this.label_TabStopSize.Text = "Size (in points):";
            // 
            // numericUpDown_TabStopSize
            // 
            this.numericUpDown_TabStopSize.Location = new System.Drawing.Point(228, 110);
            this.numericUpDown_TabStopSize.Name = "numericUpDown_TabStopSize";
            this.numericUpDown_TabStopSize.Size = new System.Drawing.Size(64, 20);
            this.numericUpDown_TabStopSize.TabIndex = 10;
            // 
            // checkBox_UseTabStops
            // 
            this.checkBox_UseTabStops.AutoSize = true;
            this.checkBox_UseTabStops.Location = new System.Drawing.Point(125, 90);
            this.checkBox_UseTabStops.Name = "checkBox_UseTabStops";
            this.checkBox_UseTabStops.Size = new System.Drawing.Size(97, 17);
            this.checkBox_UseTabStops.TabIndex = 9;
            this.checkBox_UseTabStops.Text = "Use Tab Stops";
            this.checkBox_UseTabStops.UseVisualStyleBackColor = true;
            this.checkBox_UseTabStops.CheckedChanged += new System.EventHandler(this.checkBox_UseTabStops_CheckedChanged);
            // 
            // numericUpDown_Spaces
            // 
            this.numericUpDown_Spaces.Location = new System.Drawing.Point(125, 36);
            this.numericUpDown_Spaces.Name = "numericUpDown_Spaces";
            this.numericUpDown_Spaces.Size = new System.Drawing.Size(64, 20);
            this.numericUpDown_Spaces.TabIndex = 8;
            // 
            // radioButton_Tab
            // 
            this.radioButton_Tab.AutoSize = true;
            this.radioButton_Tab.Location = new System.Drawing.Point(21, 89);
            this.radioButton_Tab.Name = "radioButton_Tab";
            this.radioButton_Tab.Size = new System.Drawing.Size(49, 17);
            this.radioButton_Tab.TabIndex = 7;
            this.radioButton_Tab.TabStop = true;
            this.radioButton_Tab.Text = "Tabs";
            this.radioButton_Tab.UseVisualStyleBackColor = true;
            this.radioButton_Tab.CheckedChanged += new System.EventHandler(this.radioButton_Tab_CheckedChanged);
            // 
            // radioButton_Spaces
            // 
            this.radioButton_Spaces.AutoSize = true;
            this.radioButton_Spaces.Location = new System.Drawing.Point(21, 36);
            this.radioButton_Spaces.Name = "radioButton_Spaces";
            this.radioButton_Spaces.Size = new System.Drawing.Size(61, 17);
            this.radioButton_Spaces.TabIndex = 6;
            this.radioButton_Spaces.TabStop = true;
            this.radioButton_Spaces.Text = "Spaces";
            this.radioButton_Spaces.UseVisualStyleBackColor = true;
            this.radioButton_Spaces.CheckedChanged += new System.EventHandler(this.radioButton_Spaces_CheckedChanged);
            // 
            // IndentSettingsForm
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(379, 237);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IndentSettingsForm";
            this.ShowInTaskbar = false;
            this.Text = "Indentation Settings";
            this.Load += new System.EventHandler(this.IndentSettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TabStopSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Spaces)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.NumericUpDown numericUpDown_Spaces;
        public System.Windows.Forms.RadioButton radioButton_Tab;
        public System.Windows.Forms.RadioButton radioButton_Spaces;
        private System.Windows.Forms.Label label_TabStopSize;
        private System.Windows.Forms.NumericUpDown numericUpDown_TabStopSize;
        private System.Windows.Forms.CheckBox checkBox_UseTabStops;
        private System.Windows.Forms.CheckBox checkBox_LineWrap;
    }
}