namespace WordUtils.WordRegexSearch
{
    partial class RegexFindForm
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
            this.richTextBox_Regex = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_caseSensitive = new System.Windows.Forms.CheckBox();
            this.checkBox_dotMatchesAll = new System.Windows.Forms.CheckBox();
            this.checkBox_Multiline = new System.Windows.Forms.CheckBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_FindNext = new System.Windows.Forms.Button();
            this.button_Replace = new System.Windows.Forms.Button();
            this.textBox_Replace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_ReplaceAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox_Regex
            // 
            this.richTextBox_Regex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_Regex.DetectUrls = false;
            this.richTextBox_Regex.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBox_Regex.Location = new System.Drawing.Point(117, 21);
            this.richTextBox_Regex.Margin = new System.Windows.Forms.Padding(10);
            this.richTextBox_Regex.Multiline = false;
            this.richTextBox_Regex.Name = "richTextBox_Regex";
            this.richTextBox_Regex.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox_Regex.Size = new System.Drawing.Size(520, 29);
            this.richTextBox_Regex.TabIndex = 1;
            this.richTextBox_Regex.Text = "";
            this.richTextBox_Regex.TextChanged += new System.EventHandler(this.richTextBox_Regex_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Find what (Regex):";
            // 
            // checkBox_caseSensitive
            // 
            this.checkBox_caseSensitive.AutoSize = true;
            this.checkBox_caseSensitive.Location = new System.Drawing.Point(15, 103);
            this.checkBox_caseSensitive.Name = "checkBox_caseSensitive";
            this.checkBox_caseSensitive.Size = new System.Drawing.Size(96, 17);
            this.checkBox_caseSensitive.TabIndex = 3;
            this.checkBox_caseSensitive.Text = "Case Sensitive";
            this.checkBox_caseSensitive.UseVisualStyleBackColor = true;
            // 
            // checkBox_dotMatchesAll
            // 
            this.checkBox_dotMatchesAll.AutoSize = true;
            this.checkBox_dotMatchesAll.Location = new System.Drawing.Point(117, 103);
            this.checkBox_dotMatchesAll.Name = "checkBox_dotMatchesAll";
            this.checkBox_dotMatchesAll.Size = new System.Drawing.Size(101, 17);
            this.checkBox_dotMatchesAll.TabIndex = 4;
            this.checkBox_dotMatchesAll.Text = "Dot Matches All";
            this.checkBox_dotMatchesAll.UseVisualStyleBackColor = true;
            // 
            // checkBox_Multiline
            // 
            this.checkBox_Multiline.AutoSize = true;
            this.checkBox_Multiline.Location = new System.Drawing.Point(224, 103);
            this.checkBox_Multiline.Name = "checkBox_Multiline";
            this.checkBox_Multiline.Size = new System.Drawing.Size(64, 17);
            this.checkBox_Multiline.TabIndex = 5;
            this.checkBox_Multiline.Text = "Multiline";
            this.checkBox_Multiline.UseVisualStyleBackColor = true;
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(569, 132);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 9;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_FindNext
            // 
            this.button_FindNext.Location = new System.Drawing.Point(488, 132);
            this.button_FindNext.Name = "button_FindNext";
            this.button_FindNext.Size = new System.Drawing.Size(75, 23);
            this.button_FindNext.TabIndex = 8;
            this.button_FindNext.Text = "Next";
            this.button_FindNext.UseVisualStyleBackColor = true;
            this.button_FindNext.Click += new System.EventHandler(this.button_FindNext_Click);
            // 
            // button_Replace
            // 
            this.button_Replace.Location = new System.Drawing.Point(326, 132);
            this.button_Replace.Name = "button_Replace";
            this.button_Replace.Size = new System.Drawing.Size(75, 23);
            this.button_Replace.TabIndex = 6;
            this.button_Replace.Text = "Replace";
            this.button_Replace.UseVisualStyleBackColor = true;
            this.button_Replace.Click += new System.EventHandler(this.button_Replace_Click);
            // 
            // textBox_Replace
            // 
            this.textBox_Replace.Location = new System.Drawing.Point(117, 63);
            this.textBox_Replace.Name = "textBox_Replace";
            this.textBox_Replace.Size = new System.Drawing.Size(520, 20);
            this.textBox_Replace.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Replace with:";
            // 
            // button_ReplaceAll
            // 
            this.button_ReplaceAll.Location = new System.Drawing.Point(407, 132);
            this.button_ReplaceAll.Name = "button_ReplaceAll";
            this.button_ReplaceAll.Size = new System.Drawing.Size(75, 23);
            this.button_ReplaceAll.TabIndex = 7;
            this.button_ReplaceAll.Text = "Replace All";
            this.button_ReplaceAll.UseVisualStyleBackColor = true;
            this.button_ReplaceAll.Click += new System.EventHandler(this.button_ReplaceAll_Click);
            // 
            // RegexFindForm
            // 
            this.AcceptButton = this.button_FindNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(656, 167);
            this.Controls.Add(this.button_ReplaceAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_Replace);
            this.Controls.Add(this.button_Replace);
            this.Controls.Add(this.button_FindNext);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.checkBox_Multiline);
            this.Controls.Add(this.checkBox_dotMatchesAll);
            this.Controls.Add(this.checkBox_caseSensitive);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox_Regex);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegexFindForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find and Replace with Regular Expressions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_Regex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_caseSensitive;
        private System.Windows.Forms.CheckBox checkBox_dotMatchesAll;
        private System.Windows.Forms.CheckBox checkBox_Multiline;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_FindNext;
        private System.Windows.Forms.Button button_Replace;
        private System.Windows.Forms.TextBox textBox_Replace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_ReplaceAll;
    }
}