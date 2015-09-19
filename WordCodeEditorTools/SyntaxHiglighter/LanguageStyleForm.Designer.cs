namespace WordCodeEditorTools
{
    partial class LanguageStyleForm
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
            this.languageListBox = new System.Windows.Forms.ListBox();
            this.langelementListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.styleGroupBox = new System.Windows.Forms.GroupBox();
            this.checkBox_allcaps = new System.Windows.Forms.CheckBox();
            this.checkBox_underlined = new System.Windows.Forms.CheckBox();
            this.checkBox_italic = new System.Windows.Forms.CheckBox();
            this.checkBox_bold = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.colorButton = new System.Windows.Forms.Button();
            this.groupBox_regex = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_separatorafter = new System.Windows.Forms.TextBox();
            this.textBox_mainregex = new System.Windows.Forms.TextBox();
            this.textBox_separatorbefore = new System.Windows.Forms.TextBox();
            this.checkBox_keyword = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_casesensitive = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_LoadFile = new System.Windows.Forms.Button();
            this.button_SaveFile = new System.Windows.Forms.Button();
            this.button_LoadDefault = new System.Windows.Forms.Button();
            this.textBox_AddLanguage = new System.Windows.Forms.TextBox();
            this.button_Addlanguage = new System.Windows.Forms.Button();
            this.button_AddElement = new System.Windows.Forms.Button();
            this.textBox_AddElement = new System.Windows.Forms.TextBox();
            this.button_SaveElement = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox_subLanguage = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.styleGroupBox.SuspendLayout();
            this.groupBox_regex.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Languages:";
            // 
            // languageListBox
            // 
            this.languageListBox.FormattingEnabled = true;
            this.languageListBox.Location = new System.Drawing.Point(13, 30);
            this.languageListBox.Name = "languageListBox";
            this.languageListBox.Size = new System.Drawing.Size(120, 303);
            this.languageListBox.TabIndex = 1;
            this.languageListBox.SelectedIndexChanged += new System.EventHandler(this.languageListBox_SelectedIndexChanged);
            this.languageListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.languageListBox_MouseDown);
            this.languageListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.languageListBox_MouseUp);
            // 
            // langelementListBox
            // 
            this.langelementListBox.FormattingEnabled = true;
            this.langelementListBox.Location = new System.Drawing.Point(159, 30);
            this.langelementListBox.Name = "langelementListBox";
            this.langelementListBox.Size = new System.Drawing.Size(120, 199);
            this.langelementListBox.TabIndex = 2;
            this.langelementListBox.SelectedIndexChanged += new System.EventHandler(this.langelementListBox_SelectedIndexChanged);
            this.langelementListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.langelementListBox_MouseDown);
            this.langelementListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.langelementListBox_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Language Elements:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(140, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = ">";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(285, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = ">";
            // 
            // styleGroupBox
            // 
            this.styleGroupBox.Controls.Add(this.checkBox_allcaps);
            this.styleGroupBox.Controls.Add(this.checkBox_underlined);
            this.styleGroupBox.Controls.Add(this.checkBox_italic);
            this.styleGroupBox.Controls.Add(this.checkBox_bold);
            this.styleGroupBox.Controls.Add(this.label5);
            this.styleGroupBox.Controls.Add(this.colorButton);
            this.styleGroupBox.Location = new System.Drawing.Point(11, 19);
            this.styleGroupBox.Name = "styleGroupBox";
            this.styleGroupBox.Size = new System.Drawing.Size(131, 199);
            this.styleGroupBox.TabIndex = 6;
            this.styleGroupBox.TabStop = false;
            this.styleGroupBox.Text = "Style";
            // 
            // checkBox_allcaps
            // 
            this.checkBox_allcaps.AutoSize = true;
            this.checkBox_allcaps.Location = new System.Drawing.Point(26, 132);
            this.checkBox_allcaps.Name = "checkBox_allcaps";
            this.checkBox_allcaps.Size = new System.Drawing.Size(91, 17);
            this.checkBox_allcaps.TabIndex = 5;
            this.checkBox_allcaps.Text = "All Capitalized";
            this.checkBox_allcaps.UseVisualStyleBackColor = true;
            this.checkBox_allcaps.CheckedChanged += new System.EventHandler(this.checkBox_allcaps_CheckedChanged);
            // 
            // checkBox_underlined
            // 
            this.checkBox_underlined.AutoSize = true;
            this.checkBox_underlined.Location = new System.Drawing.Point(26, 109);
            this.checkBox_underlined.Name = "checkBox_underlined";
            this.checkBox_underlined.Size = new System.Drawing.Size(77, 17);
            this.checkBox_underlined.TabIndex = 4;
            this.checkBox_underlined.Text = "Underlined";
            this.checkBox_underlined.UseVisualStyleBackColor = true;
            this.checkBox_underlined.CheckedChanged += new System.EventHandler(this.checkBox_underlined_CheckedChanged);
            // 
            // checkBox_italic
            // 
            this.checkBox_italic.AutoSize = true;
            this.checkBox_italic.Location = new System.Drawing.Point(26, 86);
            this.checkBox_italic.Name = "checkBox_italic";
            this.checkBox_italic.Size = new System.Drawing.Size(48, 17);
            this.checkBox_italic.TabIndex = 3;
            this.checkBox_italic.Text = "Italic";
            this.checkBox_italic.UseVisualStyleBackColor = true;
            this.checkBox_italic.CheckedChanged += new System.EventHandler(this.checkBox_italic_CheckedChanged);
            // 
            // checkBox_bold
            // 
            this.checkBox_bold.AutoSize = true;
            this.checkBox_bold.Location = new System.Drawing.Point(26, 63);
            this.checkBox_bold.Name = "checkBox_bold";
            this.checkBox_bold.Size = new System.Drawing.Size(47, 17);
            this.checkBox_bold.TabIndex = 2;
            this.checkBox_bold.Text = "Bold";
            this.checkBox_bold.UseVisualStyleBackColor = true;
            this.checkBox_bold.CheckedChanged += new System.EventHandler(this.checkBox_bold_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Color";
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(23, 26);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(23, 23);
            this.colorButton.TabIndex = 0;
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // groupBox_regex
            // 
            this.groupBox_regex.Controls.Add(this.label8);
            this.groupBox_regex.Controls.Add(this.label7);
            this.groupBox_regex.Controls.Add(this.label6);
            this.groupBox_regex.Controls.Add(this.textBox_separatorafter);
            this.groupBox_regex.Controls.Add(this.textBox_mainregex);
            this.groupBox_regex.Controls.Add(this.textBox_separatorbefore);
            this.groupBox_regex.Controls.Add(this.checkBox_keyword);
            this.groupBox_regex.Location = new System.Drawing.Point(148, 19);
            this.groupBox_regex.Name = "groupBox_regex";
            this.groupBox_regex.Size = new System.Drawing.Size(321, 303);
            this.groupBox_regex.TabIndex = 7;
            this.groupBox_regex.TabStop = false;
            this.groupBox_regex.Text = "Regular Expressions";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 247);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(213, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Succeeded by (list of expressions, optional):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Main expression:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(204, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Preceded by (list of expressions, optional):";
            // 
            // textBox_separatorafter
            // 
            this.textBox_separatorafter.Location = new System.Drawing.Point(20, 265);
            this.textBox_separatorafter.Name = "textBox_separatorafter";
            this.textBox_separatorafter.Size = new System.Drawing.Size(280, 20);
            this.textBox_separatorafter.TabIndex = 3;
            this.textBox_separatorafter.TextChanged += new System.EventHandler(this.textBox_separatorafter_TextChanged);
            // 
            // textBox_mainregex
            // 
            this.textBox_mainregex.Location = new System.Drawing.Point(20, 143);
            this.textBox_mainregex.Multiline = true;
            this.textBox_mainregex.Name = "textBox_mainregex";
            this.textBox_mainregex.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_mainregex.Size = new System.Drawing.Size(280, 83);
            this.textBox_mainregex.TabIndex = 2;
            this.textBox_mainregex.TextChanged += new System.EventHandler(this.textBox_mainregex_TextChanged);
            // 
            // textBox_separatorbefore
            // 
            this.textBox_separatorbefore.Location = new System.Drawing.Point(20, 54);
            this.textBox_separatorbefore.Name = "textBox_separatorbefore";
            this.textBox_separatorbefore.Size = new System.Drawing.Size(280, 20);
            this.textBox_separatorbefore.TabIndex = 1;
            this.textBox_separatorbefore.TextChanged += new System.EventHandler(this.textBox_separatorbefore_TextChanged);
            // 
            // checkBox_keyword
            // 
            this.checkBox_keyword.AutoSize = true;
            this.checkBox_keyword.Location = new System.Drawing.Point(23, 122);
            this.checkBox_keyword.Name = "checkBox_keyword";
            this.checkBox_keyword.Size = new System.Drawing.Size(102, 17);
            this.checkBox_keyword.TabIndex = 0;
            this.checkBox_keyword.Text = "List of keywords";
            this.checkBox_keyword.UseVisualStyleBackColor = true;
            this.checkBox_keyword.CheckedChanged += new System.EventHandler(this.checkBox_keyword_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_casesensitive);
            this.groupBox1.Location = new System.Drawing.Point(159, 279);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 54);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Language Properties";
            // 
            // checkBox_casesensitive
            // 
            this.checkBox_casesensitive.AutoSize = true;
            this.checkBox_casesensitive.Location = new System.Drawing.Point(15, 24);
            this.checkBox_casesensitive.Name = "checkBox_casesensitive";
            this.checkBox_casesensitive.Size = new System.Drawing.Size(96, 17);
            this.checkBox_casesensitive.TabIndex = 0;
            this.checkBox_casesensitive.Text = "Case Sensitive";
            this.checkBox_casesensitive.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(139, 298);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = ">";
            // 
            // button_OK
            // 
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(706, 398);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 10;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(625, 398);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 11;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // button_LoadFile
            // 
            this.button_LoadFile.Location = new System.Drawing.Point(13, 398);
            this.button_LoadFile.Name = "button_LoadFile";
            this.button_LoadFile.Size = new System.Drawing.Size(81, 23);
            this.button_LoadFile.TabIndex = 12;
            this.button_LoadFile.Text = "Load from file";
            this.button_LoadFile.UseVisualStyleBackColor = true;
            this.button_LoadFile.Click += new System.EventHandler(this.button_LoadFile_Click);
            // 
            // button_SaveFile
            // 
            this.button_SaveFile.Location = new System.Drawing.Point(100, 398);
            this.button_SaveFile.Name = "button_SaveFile";
            this.button_SaveFile.Size = new System.Drawing.Size(75, 23);
            this.button_SaveFile.TabIndex = 13;
            this.button_SaveFile.Text = "Save to file";
            this.button_SaveFile.UseVisualStyleBackColor = true;
            this.button_SaveFile.Click += new System.EventHandler(this.button_SaveFile_Click);
            // 
            // button_LoadDefault
            // 
            this.button_LoadDefault.Location = new System.Drawing.Point(181, 398);
            this.button_LoadDefault.Name = "button_LoadDefault";
            this.button_LoadDefault.Size = new System.Drawing.Size(75, 23);
            this.button_LoadDefault.TabIndex = 14;
            this.button_LoadDefault.Text = "Load default";
            this.button_LoadDefault.UseVisualStyleBackColor = true;
            this.button_LoadDefault.Click += new System.EventHandler(this.button_LoadDefault_Click);
            // 
            // textBox_AddLanguage
            // 
            this.textBox_AddLanguage.Location = new System.Drawing.Point(13, 339);
            this.textBox_AddLanguage.Name = "textBox_AddLanguage";
            this.textBox_AddLanguage.Size = new System.Drawing.Size(77, 20);
            this.textBox_AddLanguage.TabIndex = 15;
            this.textBox_AddLanguage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_AddLanguage_KeyPress);
            // 
            // button_Addlanguage
            // 
            this.button_Addlanguage.Location = new System.Drawing.Point(92, 337);
            this.button_Addlanguage.Name = "button_Addlanguage";
            this.button_Addlanguage.Size = new System.Drawing.Size(41, 23);
            this.button_Addlanguage.TabIndex = 16;
            this.button_Addlanguage.Text = "Add";
            this.button_Addlanguage.UseVisualStyleBackColor = true;
            this.button_Addlanguage.Click += new System.EventHandler(this.button_Addlanguage_Click);
            // 
            // button_AddElement
            // 
            this.button_AddElement.Location = new System.Drawing.Point(238, 233);
            this.button_AddElement.Name = "button_AddElement";
            this.button_AddElement.Size = new System.Drawing.Size(41, 23);
            this.button_AddElement.TabIndex = 18;
            this.button_AddElement.Text = "Add";
            this.button_AddElement.UseVisualStyleBackColor = true;
            this.button_AddElement.Click += new System.EventHandler(this.button_AddElement_Click);
            // 
            // textBox_AddElement
            // 
            this.textBox_AddElement.Location = new System.Drawing.Point(159, 235);
            this.textBox_AddElement.Name = "textBox_AddElement";
            this.textBox_AddElement.Size = new System.Drawing.Size(77, 20);
            this.textBox_AddElement.TabIndex = 17;
            this.textBox_AddElement.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_AddElement_KeyPress);
            // 
            // button_SaveElement
            // 
            this.button_SaveElement.Location = new System.Drawing.Point(385, 328);
            this.button_SaveElement.Name = "button_SaveElement";
            this.button_SaveElement.Size = new System.Drawing.Size(84, 23);
            this.button_SaveElement.TabIndex = 19;
            this.button_SaveElement.Text = "Save Element";
            this.button_SaveElement.UseVisualStyleBackColor = true;
            this.button_SaveElement.Click += new System.EventHandler(this.button_SaveElement_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox_subLanguage);
            this.groupBox2.Location = new System.Drawing.Point(11, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 100);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sub-Language";
            // 
            // comboBox_subLanguage
            // 
            this.comboBox_subLanguage.FormattingEnabled = true;
            this.comboBox_subLanguage.Location = new System.Drawing.Point(6, 59);
            this.comboBox_subLanguage.Name = "comboBox_subLanguage";
            this.comboBox_subLanguage.Size = new System.Drawing.Size(119, 21);
            this.comboBox_subLanguage.TabIndex = 0;
            this.comboBox_subLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBox_subLanguage_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_SaveElement);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.styleGroupBox);
            this.groupBox3.Controls.Add(this.groupBox_regex);
            this.groupBox3.Location = new System.Drawing.Point(302, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(479, 359);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Selected Element";
            // 
            // LanguageStyleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(794, 433);
            this.Controls.Add(this.button_AddElement);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_AddElement);
            this.Controls.Add(this.button_Addlanguage);
            this.Controls.Add(this.textBox_AddLanguage);
            this.Controls.Add(this.button_LoadDefault);
            this.Controls.Add(this.button_SaveFile);
            this.Controls.Add(this.button_LoadFile);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.langelementListBox);
            this.Controls.Add(this.languageListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LanguageStyleForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Language Style Editor";
            this.styleGroupBox.ResumeLayout(false);
            this.styleGroupBox.PerformLayout();
            this.groupBox_regex.ResumeLayout(false);
            this.groupBox_regex.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox languageListBox;
        private System.Windows.Forms.ListBox langelementListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox styleGroupBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.CheckBox checkBox_allcaps;
        private System.Windows.Forms.CheckBox checkBox_underlined;
        private System.Windows.Forms.CheckBox checkBox_italic;
        private System.Windows.Forms.CheckBox checkBox_bold;
        private System.Windows.Forms.GroupBox groupBox_regex;
        private System.Windows.Forms.TextBox textBox_separatorafter;
        private System.Windows.Forms.TextBox textBox_mainregex;
        private System.Windows.Forms.TextBox textBox_separatorbefore;
        private System.Windows.Forms.CheckBox checkBox_keyword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_casesensitive;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_LoadFile;
        private System.Windows.Forms.Button button_SaveFile;
        private System.Windows.Forms.Button button_LoadDefault;
        private System.Windows.Forms.TextBox textBox_AddLanguage;
        private System.Windows.Forms.Button button_Addlanguage;
        private System.Windows.Forms.Button button_AddElement;
        private System.Windows.Forms.TextBox textBox_AddElement;
        private System.Windows.Forms.Button button_SaveElement;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox_subLanguage;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}