namespace WordUtils
{
    partial class WordUtilsRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public WordUtilsRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordUtilsRibbon));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.button_UrlChecker = this.Factory.CreateRibbonButton();
            this.button_RemoveHyperlinks = this.Factory.CreateRibbonButton();
            this.menu1 = this.Factory.CreateRibbonMenu();
            this.button_AutoUrlDetectionON = this.Factory.CreateRibbonButton();
            this.button_AutoUrlDetectionOFF = this.Factory.CreateRibbonButton();
            this.button_HyperlinkConverter = this.Factory.CreateRibbonButton();
            this.button_RegexFind = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.group2);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.button_UrlChecker);
            this.group1.Items.Add(this.button_RemoveHyperlinks);
            this.group1.Items.Add(this.menu1);
            this.group1.Items.Add(this.button_HyperlinkConverter);
            this.group1.Label = "URL Manager";
            this.group1.Name = "group1";
            // 
            // group2
            // 
            this.group2.Items.Add(this.button_RegexFind);
            this.group2.Label = "Regex Search";
            this.group2.Name = "group2";
            // 
            // button_UrlChecker
            // 
            this.button_UrlChecker.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button_UrlChecker.Image = ((System.Drawing.Image)(resources.GetObject("button_UrlChecker.Image")));
            this.button_UrlChecker.Label = "Check URL avaliability";
            this.button_UrlChecker.Name = "button_UrlChecker";
            this.button_UrlChecker.ShowImage = true;
            this.button_UrlChecker.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // button_RemoveHyperlinks
            // 
            this.button_RemoveHyperlinks.Label = "Remove Hyperlinks";
            this.button_RemoveHyperlinks.Name = "button_RemoveHyperlinks";
            this.button_RemoveHyperlinks.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_RemoveHyperlinks_Click);
            // 
            // menu1
            // 
            this.menu1.Items.Add(this.button_AutoUrlDetectionON);
            this.menu1.Items.Add(this.button_AutoUrlDetectionOFF);
            this.menu1.Label = "Auto Hyperlinking";
            this.menu1.Name = "menu1";
            // 
            // button_AutoUrlDetectionON
            // 
            this.button_AutoUrlDetectionON.Label = "Turn On";
            this.button_AutoUrlDetectionON.Name = "button_AutoUrlDetectionON";
            this.button_AutoUrlDetectionON.ShowImage = true;
            this.button_AutoUrlDetectionON.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_AutoUrlDetectionON_Click);
            // 
            // button_AutoUrlDetectionOFF
            // 
            this.button_AutoUrlDetectionOFF.Label = "Turn Off";
            this.button_AutoUrlDetectionOFF.Name = "button_AutoUrlDetectionOFF";
            this.button_AutoUrlDetectionOFF.ShowImage = true;
            this.button_AutoUrlDetectionOFF.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_AutoUrlDetectionOFF_Click);
            // 
            // button_HyperlinkConverter
            // 
            this.button_HyperlinkConverter.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button_HyperlinkConverter.Image = ((System.Drawing.Image)(resources.GetObject("button_HyperlinkConverter.Image")));
            this.button_HyperlinkConverter.Label = "Convert URLs to Hyperlinks";
            this.button_HyperlinkConverter.Name = "button_HyperlinkConverter";
            this.button_HyperlinkConverter.ShowImage = true;
            this.button_HyperlinkConverter.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_HyperlinkConverter_Click);
            // 
            // button_RegexFind
            // 
            this.button_RegexFind.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button_RegexFind.Image = ((System.Drawing.Image)(resources.GetObject("button_RegexFind.Image")));
            this.button_RegexFind.Label = "Find with Regex";
            this.button_RegexFind.Name = "button_RegexFind";
            this.button_RegexFind.ShowImage = true;
            this.button_RegexFind.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_RegexFind_Click);
            // 
            // WordUtilsRibbon
            // 
            this.Name = "WordUtilsRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.WordUtilsRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_UrlChecker;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_HyperlinkConverter;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_RemoveHyperlinks;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_AutoUrlDetectionON;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_AutoUrlDetectionOFF;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_RegexFind;
    }

    partial class ThisRibbonCollection
    {
        internal WordUtilsRibbon WordUtilsRibbon
        {
            get { return this.GetRibbon<WordUtilsRibbon>(); }
        }
    }
}
