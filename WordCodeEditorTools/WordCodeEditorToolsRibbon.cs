using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Office.Tools.Ribbon;
using Word = Microsoft.Office.Interop.Word;
using WordCodeEditorTools.CodeIndentation;
using WordCodeEditorTools.SyntaxHighlighter;
using WordCodeEditorTools.WordSyntaxHighlighter;

namespace WordCodeEditorTools
{
    /// <summary>
    /// The main User Interface of the Add-Ins. There are additional Windows Forms that can be opened by clicking on some buttons of the Ribbon.
    /// </summary>
    public partial class WordCodeEditorToolsRibbon
    {
        private ThisAddIn addIn;

        private void WordCodeEditorToolsRibbon_Load(object sender, RibbonUIEventArgs e) { }

        public void InitializeAddIn(ThisAddIn addIn_)
        {
            addIn = addIn_;
            
            InitializeLanguageList();
        }

        //Syntax Highlighter

        private void InitializeLanguageList()
        {
            dropDown_Languages.Items.Clear();
            foreach (KeyValuePair<string, Language> language in addIn.Colorizer.Languages)
            {
                RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                item.Label = language.Value.Name;
                dropDown_Languages.Items.Add(item);
            }
            int index = addIn.Colorizer.Languages.IndexOfKey("C#");
            if (index >= 0)
                dropDown_Languages.SelectedItemIndex = index;
        }

        private void Frame_style_Click(object sender, RibbonControlEventArgs e)
        {
            FrameStyleForm form = new FrameStyleForm(addIn.Formatter);
            System.Windows.Forms.DialogResult result = form.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                form.OK();
            }
            form.Dispose();
        }

        private void LanguageStylesButton_Click(object sender, RibbonControlEventArgs e)
        {
            LanguageStyleForm form = new LanguageStyleForm(addIn.Colorizer);
            System.Windows.Forms.DialogResult result = form.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                form.SaveCurrentElementIfChanged(false);
                form.SaveLanguagePropertiesIfChanged(false);
            }
            if (form.ColorizerReassigned)
            {
                addIn.Colorizer = form.Colorizer;
            }
            if (form.LanguageListChanged) // already uses the new Colorizer if it was replaced
            {
                InitializeLanguageList();
            }
            form.Dispose();
        }

        private void button_Colorize_Click(object sender, RibbonControlEventArgs e)
        {
            if(dropDown_Languages.SelectedItemIndex >= 0)
                addIn.Formatter.Colorize(dropDown_Languages.SelectedItem.Label, checkBox_Frame.Checked, checkBox_linenumbers.Checked);
        }

        // Indentation

        private void button_IndentCLike_Click(object sender, RibbonControlEventArgs e)
        {
            addIn.IndentFixer.Indent(0);
        }
        
        private void button_IndentXMLLike_Click(object sender, RibbonControlEventArgs e)
        {
            addIn.IndentFixer.Indent(1);
        }

        private void button_RemoveIndent_Click(object sender, RibbonControlEventArgs e)
        {
            addIn.IndentFixer.Indent(-1);
        }

        private void button_IndentationSettings_Click(object sender, RibbonControlEventArgs e)
        {
            IndentSettingsForm form = new IndentSettingsForm(addIn.IndentFixer);
            System.Windows.Forms.DialogResult result = form.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                form.OK();
            }
            form.Dispose();
        }

        // Code Cleaner

        private void buttonReplaceSmartQuotes_Click(object sender, RibbonControlEventArgs e)
        {
            addIn.CodeCleaner.ReplaceSmartQuotesInSelection();
        }

        private void buttonNoSpellCheck_Click(object sender, RibbonControlEventArgs e)
        {
            addIn.CodeCleaner.NoSpellCheckOnSelection();
        }

        private void buttonReSpellCheck_Click(object sender, RibbonControlEventArgs e)
        {
            addIn.CodeCleaner.ReverseNoSpellCheckOnSelection();
        }

        private void button_TurnSmartQuoteOff_Click(object sender, RibbonControlEventArgs e)
        {
            addIn.CodeCleaner.TurnSmartQuoteSettingsOff();
        }

        private void button_TurnSmartQuoteOn_Click(object sender, RibbonControlEventArgs e)
        {
            addIn.CodeCleaner.TurnSmartQuoteSettingsOn();
        }

        private void button_InsertDoubleQuote_Click(object sender, RibbonControlEventArgs e)
        {
            addIn.CodeCleaner.InsertCharacter('"');
        }

        private void button_InsertSingleQuote_Click(object sender, RibbonControlEventArgs e)
        {
            addIn.CodeCleaner.InsertCharacter('\'');
        }

        private void button_InsertBacktick_Click(object sender, RibbonControlEventArgs e)
        {
            addIn.CodeCleaner.InsertCharacter('`');
        }

    }
}
