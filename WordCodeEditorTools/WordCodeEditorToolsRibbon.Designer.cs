namespace WordCodeEditorTools
{
    partial class WordCodeEditorToolsRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public WordCodeEditorToolsRibbon()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordCodeEditorToolsRibbon));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.SyntaxHighlighter = this.Factory.CreateRibbonGroup();
            this.dropDown_Languages = this.Factory.CreateRibbonDropDown();
            this.checkBox_Frame = this.Factory.CreateRibbonCheckBox();
            this.checkBox_linenumbers = this.Factory.CreateRibbonCheckBox();
            this.Indentation = this.Factory.CreateRibbonGroup();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.LanguageStylesButton = this.Factory.CreateRibbonButton();
            this.FrameStyleButton = this.Factory.CreateRibbonButton();
            this.button_Colorize = this.Factory.CreateRibbonButton();
            this.button_IndentationSettings = this.Factory.CreateRibbonButton();
            this.menu_Indent = this.Factory.CreateRibbonMenu();
            this.button_IndentCLike = this.Factory.CreateRibbonButton();
            this.button_IndentXMLlike = this.Factory.CreateRibbonButton();
            this.menu1 = this.Factory.CreateRibbonMenu();
            this.buttonReSpellCheck = this.Factory.CreateRibbonButton();
            this.buttonNoSpellCheck = this.Factory.CreateRibbonButton();
            this.menu2 = this.Factory.CreateRibbonMenu();
            this.button_TurnSmartQuoteOff = this.Factory.CreateRibbonButton();
            this.button_TurnSmartQuoteOn = this.Factory.CreateRibbonButton();
            this.menu3 = this.Factory.CreateRibbonMenu();
            this.button_InsertDoubleQuote = this.Factory.CreateRibbonButton();
            this.button_InsertSingleQuote = this.Factory.CreateRibbonButton();
            this.button_InsertBacktick = this.Factory.CreateRibbonButton();
            this.button_ReplaceSmartQuotes = this.Factory.CreateRibbonButton();
            this.button_RemoveIndent = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.SyntaxHighlighter.SuspendLayout();
            this.Indentation.SuspendLayout();
            this.group1.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.SyntaxHighlighter);
            this.tab1.Groups.Add(this.Indentation);
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "Code Style";
            this.tab1.Name = "tab1";
            // 
            // SyntaxHighlighter
            // 
            this.SyntaxHighlighter.Items.Add(this.dropDown_Languages);
            this.SyntaxHighlighter.Items.Add(this.checkBox_Frame);
            this.SyntaxHighlighter.Items.Add(this.checkBox_linenumbers);
            this.SyntaxHighlighter.Items.Add(this.LanguageStylesButton);
            this.SyntaxHighlighter.Items.Add(this.FrameStyleButton);
            this.SyntaxHighlighter.Items.Add(this.button_Colorize);
            this.SyntaxHighlighter.Label = "Syntax Highlighter";
            this.SyntaxHighlighter.Name = "SyntaxHighlighter";
            // 
            // dropDown_Languages
            // 
            this.dropDown_Languages.Label = " ";
            this.dropDown_Languages.Name = "dropDown_Languages";
            this.dropDown_Languages.ScreenTip = "Programming Languages";
            this.dropDown_Languages.ShowLabel = false;
            this.dropDown_Languages.SuperTip = "List of the programming languages the Syntax Highlighter currently recognizes.";
            // 
            // checkBox_Frame
            // 
            this.checkBox_Frame.Checked = true;
            this.checkBox_Frame.Label = "Frame";
            this.checkBox_Frame.Name = "checkBox_Frame";
            this.checkBox_Frame.ScreenTip = "Add Frame";
            this.checkBox_Frame.SuperTip = "Check if you want the colorized code to appear in a frame.";
            // 
            // checkBox_linenumbers
            // 
            this.checkBox_linenumbers.Label = "Line numbers";
            this.checkBox_linenumbers.Name = "checkBox_linenumbers";
            this.checkBox_linenumbers.ScreenTip = "Add Line Numbering";
            this.checkBox_linenumbers.SuperTip = "Warning: Adding line numbers to the selection transforms it into a table. This ca" +
    "n take a few seconds and make it harder to use formatting on the text  later.";
            // 
            // Indentation
            // 
            this.Indentation.Items.Add(this.button_IndentationSettings);
            this.Indentation.Items.Add(this.button_RemoveIndent);
            this.Indentation.Items.Add(this.menu_Indent);
            this.Indentation.Label = "Code Indentation";
            this.Indentation.Name = "Indentation";
            // 
            // group1
            // 
            this.group1.Items.Add(this.menu1);
            this.group1.Items.Add(this.menu2);
            this.group1.Items.Add(this.menu3);
            this.group1.Items.Add(this.button_ReplaceSmartQuotes);
            this.group1.Label = "Code Cleaner";
            this.group1.Name = "group1";
            // 
            // LanguageStylesButton
            // 
            this.LanguageStylesButton.Label = "Edit Language Styles";
            this.LanguageStylesButton.Name = "LanguageStylesButton";
            this.LanguageStylesButton.ScreenTip = "Edit Language Styles";
            this.LanguageStylesButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.LanguageStylesButton_Click);
            // 
            // FrameStyleButton
            // 
            this.FrameStyleButton.Label = "Edit General Style";
            this.FrameStyleButton.Name = "FrameStyleButton";
            this.FrameStyleButton.ScreenTip = "Edit General Style";
            this.FrameStyleButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Frame_style_Click);
            // 
            // button_Colorize
            // 
            this.button_Colorize.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button_Colorize.Image = ((System.Drawing.Image)(resources.GetObject("button_Colorize.Image")));
            this.button_Colorize.Label = "Colorize Selection";
            this.button_Colorize.Name = "button_Colorize";
            this.button_Colorize.ScreenTip = "Colorize Selection";
            this.button_Colorize.ShowImage = true;
            this.button_Colorize.SuperTip = "Applies syntax highlighting and additional features to the selected text.";
            this.button_Colorize.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_Colorize_Click);
            // 
            // button_IndentationSettings
            // 
            this.button_IndentationSettings.Label = "Indentation Settings";
            this.button_IndentationSettings.Name = "button_IndentationSettings";
            this.button_IndentationSettings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_IndentationSettings_Click);
            // 
            // menu_Indent
            // 
            this.menu_Indent.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.menu_Indent.Image = ((System.Drawing.Image)(resources.GetObject("menu_Indent.Image")));
            this.menu_Indent.Items.Add(this.button_IndentCLike);
            this.menu_Indent.Items.Add(this.button_IndentXMLlike);
            this.menu_Indent.Label = "Indent Selection";
            this.menu_Indent.Name = "menu_Indent";
            this.menu_Indent.ShowImage = true;
            // 
            // button_IndentCLike
            // 
            this.button_IndentCLike.Label = "Indent C-like language";
            this.button_IndentCLike.Name = "button_IndentCLike";
            this.button_IndentCLike.ShowImage = true;
            this.button_IndentCLike.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_IndentCLike_Click);
            // 
            // button_IndentXMLlike
            // 
            this.button_IndentXMLlike.Label = "Indent XML-like language";
            this.button_IndentXMLlike.Name = "button_IndentXMLlike";
            this.button_IndentXMLlike.ShowImage = true;
            this.button_IndentXMLlike.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_IndentXMLLike_Click);
            // 
            // menu1
            // 
            this.menu1.Items.Add(this.buttonReSpellCheck);
            this.menu1.Items.Add(this.buttonNoSpellCheck);
            this.menu1.Label = "Spell Check on Selection";
            this.menu1.Name = "menu1";
            // 
            // buttonReSpellCheck
            // 
            this.buttonReSpellCheck.Label = "Turn Off";
            this.buttonReSpellCheck.Name = "buttonReSpellCheck";
            this.buttonReSpellCheck.ShowImage = true;
            this.buttonReSpellCheck.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonNoSpellCheck_Click);
            // 
            // buttonNoSpellCheck
            // 
            this.buttonNoSpellCheck.Label = "Turn On";
            this.buttonNoSpellCheck.Name = "buttonNoSpellCheck";
            this.buttonNoSpellCheck.ShowImage = true;
            this.buttonNoSpellCheck.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonReSpellCheck_Click);
            // 
            // menu2
            // 
            this.menu2.Items.Add(this.button_TurnSmartQuoteOff);
            this.menu2.Items.Add(this.button_TurnSmartQuoteOn);
            this.menu2.Label = "Use Smart Quotes";
            this.menu2.Name = "menu2";
            // 
            // button_TurnSmartQuoteOff
            // 
            this.button_TurnSmartQuoteOff.Label = "Turn Off";
            this.button_TurnSmartQuoteOff.Name = "button_TurnSmartQuoteOff";
            this.button_TurnSmartQuoteOff.ShowImage = true;
            this.button_TurnSmartQuoteOff.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_TurnSmartQuoteOff_Click);
            // 
            // button_TurnSmartQuoteOn
            // 
            this.button_TurnSmartQuoteOn.Label = "Turn On";
            this.button_TurnSmartQuoteOn.Name = "button_TurnSmartQuoteOn";
            this.button_TurnSmartQuoteOn.ShowImage = true;
            this.button_TurnSmartQuoteOn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_TurnSmartQuoteOn_Click);
            // 
            // menu3
            // 
            this.menu3.Items.Add(this.button_InsertDoubleQuote);
            this.menu3.Items.Add(this.button_InsertSingleQuote);
            this.menu3.Items.Add(this.button_InsertBacktick);
            this.menu3.Label = "Insert Quote Character";
            this.menu3.Name = "menu3";
            // 
            // button_InsertDoubleQuote
            // 
            this.button_InsertDoubleQuote.Label = "\" - programmer\'s double quote";
            this.button_InsertDoubleQuote.Name = "button_InsertDoubleQuote";
            this.button_InsertDoubleQuote.ShowImage = true;
            this.button_InsertDoubleQuote.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_InsertDoubleQuote_Click);
            // 
            // button_InsertSingleQuote
            // 
            this.button_InsertSingleQuote.Label = "\' - programmer\'s single quote";
            this.button_InsertSingleQuote.Name = "button_InsertSingleQuote";
            this.button_InsertSingleQuote.ShowImage = true;
            this.button_InsertSingleQuote.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_InsertSingleQuote_Click);
            // 
            // button_InsertBacktick
            // 
            this.button_InsertBacktick.Label = "` - backtick";
            this.button_InsertBacktick.Name = "button_InsertBacktick";
            this.button_InsertBacktick.ShowImage = true;
            this.button_InsertBacktick.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_InsertBacktick_Click);
            // 
            // button_ReplaceSmartQuotes
            // 
            this.button_ReplaceSmartQuotes.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button_ReplaceSmartQuotes.Image = ((System.Drawing.Image)(resources.GetObject("button_ReplaceSmartQuotes.Image")));
            this.button_ReplaceSmartQuotes.Label = "Replace Quotes in Selection";
            this.button_ReplaceSmartQuotes.Name = "button_ReplaceSmartQuotes";
            this.button_ReplaceSmartQuotes.ShowImage = true;
            this.button_ReplaceSmartQuotes.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonReplaceSmartQuotes_Click);
            // 
            // button_RemoveIndent
            // 
            this.button_RemoveIndent.Label = "Remove Indentation";
            this.button_RemoveIndent.Name = "button_RemoveIndent";
            this.button_RemoveIndent.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_RemoveIndent_Click);
            // 
            // WordCodeEditorToolsRibbon
            // 
            this.Name = "WordCodeEditorToolsRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.WordCodeEditorToolsRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.SyntaxHighlighter.ResumeLayout(false);
            this.SyntaxHighlighter.PerformLayout();
            this.Indentation.ResumeLayout(false);
            this.Indentation.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup SyntaxHighlighter;
        private Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBox_Frame;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBox_linenumbers;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton LanguageStylesButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton FrameStyleButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDown_Languages;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_Colorize;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Indentation;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_IndentationSettings;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu_Indent;
        private Microsoft.Office.Tools.Ribbon.RibbonButton button_IndentCLike;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_ReplaceSmartQuotes;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonNoSpellCheck;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonReSpellCheck;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_TurnSmartQuoteOn;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_TurnSmartQuoteOff;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_InsertDoubleQuote;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_InsertSingleQuote;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_InsertBacktick;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu1;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu2;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu3;
        private Microsoft.Office.Tools.Ribbon.RibbonButton button_IndentXMLlike;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_RemoveIndent;
    }

    partial class ThisRibbonCollection
    {
        internal WordCodeEditorToolsRibbon WordCodeEditorToolsRibbon
        {
            get { return this.GetRibbon<WordCodeEditorToolsRibbon>(); }
        }
    }
}
