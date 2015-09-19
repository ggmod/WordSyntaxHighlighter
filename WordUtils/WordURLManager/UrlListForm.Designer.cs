namespace WordUtils.WordURLManager
{
    partial class UrlListForm
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
            this.listView = new System.Windows.Forms.ListView();
            this.ColumnHeader_URL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader_Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_RecheckEveryURL = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.radioButton_wordHyperlinks = new System.Windows.Forms.RadioButton();
            this.radioButton_autofindUrls = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader_URL,
            this.ColumnHeader_Status});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(13, 82);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(576, 263);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listView_Urls_ColumnWidthChanging);
            this.listView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_Hyperlinks_MouseUp);
            // 
            // ColumnHeader_URL
            // 
            this.ColumnHeader_URL.Text = "URL";
            this.ColumnHeader_URL.Width = 450;
            // 
            // ColumnHeader_Status
            // 
            this.ColumnHeader_Status.Text = "Status";
            this.ColumnHeader_Status.Width = 102;
            // 
            // button_RecheckEveryURL
            // 
            this.button_RecheckEveryURL.Location = new System.Drawing.Point(388, 40);
            this.button_RecheckEveryURL.Name = "button_RecheckEveryURL";
            this.button_RecheckEveryURL.Size = new System.Drawing.Size(75, 23);
            this.button_RecheckEveryURL.TabIndex = 3;
            this.button_RecheckEveryURL.Text = "Recheck";
            this.button_RecheckEveryURL.UseVisualStyleBackColor = true;
            this.button_RecheckEveryURL.Click += new System.EventHandler(this.button_RecheckEveryURL_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(479, 43);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 17);
            this.progressBar.TabIndex = 4;
            // 
            // radioButton_wordHyperlinks
            // 
            this.radioButton_wordHyperlinks.AutoSize = true;
            this.radioButton_wordHyperlinks.Checked = true;
            this.radioButton_wordHyperlinks.Location = new System.Drawing.Point(13, 19);
            this.radioButton_wordHyperlinks.Name = "radioButton_wordHyperlinks";
            this.radioButton_wordHyperlinks.Size = new System.Drawing.Size(103, 17);
            this.radioButton_wordHyperlinks.TabIndex = 5;
            this.radioButton_wordHyperlinks.TabStop = true;
            this.radioButton_wordHyperlinks.Text = "Word Hyperlinks";
            this.radioButton_wordHyperlinks.UseVisualStyleBackColor = true;
            this.radioButton_wordHyperlinks.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton_autofindUrls
            // 
            this.radioButton_autofindUrls.AutoSize = true;
            this.radioButton_autofindUrls.Location = new System.Drawing.Point(13, 43);
            this.radioButton_autofindUrls.Name = "radioButton_autofindUrls";
            this.radioButton_autofindUrls.Size = new System.Drawing.Size(144, 17);
            this.radioButton_autofindUrls.TabIndex = 6;
            this.radioButton_autofindUrls.Text = "Automatic URL detection";
            this.radioButton_autofindUrls.UseVisualStyleBackColor = true;
            this.radioButton_autofindUrls.CheckedChanged += new System.EventHandler(this.radioButton_autofindUrls_CheckedChanged);
            // 
            // UrlListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(604, 361);
            this.Controls.Add(this.radioButton_autofindUrls);
            this.Controls.Add(this.radioButton_wordHyperlinks);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.button_RecheckEveryURL);
            this.Controls.Add(this.listView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UrlListForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "List of URLs in the current document";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UrlListForm_FormClosed);
            this.Shown += new System.EventHandler(this.UrlListForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UrlListForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader ColumnHeader_URL;
        private System.Windows.Forms.ColumnHeader ColumnHeader_Status;
        private System.Windows.Forms.Button button_RecheckEveryURL;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.RadioButton radioButton_wordHyperlinks;
        private System.Windows.Forms.RadioButton radioButton_autofindUrls;

    }
}