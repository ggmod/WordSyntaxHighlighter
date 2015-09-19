using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.Runtime.InteropServices;

namespace WordUtils.WordRegexSearch
{
    /// <summary>
    /// The window of the regex search. I tried to make it as similar to the native Find and Replace dialog of MS Word as possible in every detail.
    /// </summary>
    public partial class RegexFindForm : Form
    {
        private Word.Application wordApp;

        Match previousFindResult;
        string previousReplacement;

        public RegexFindForm(Word.Application wordApp_, string regex)
        {
            InitializeComponent();

            wordApp = wordApp_;
            previousReplacement = "";
            replaced_cntr = 0;

            richTextBox_Regex.Text = regex;
            ColorizeTextBox();
        }

        public string Regex
        {
            get { return richTextBox_Regex.Text; }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close(); // calls dispose
        }

        private int replaced_cntr;

        private void button_FindNext_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(richTextBox_Regex.Text) || wordApp.ActiveDocument == null)
                return;

            Regex regex = CreateRegex();
            if(regex != null)
                Find(regex);
        }

        private void button_Replace_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(richTextBox_Regex.Text) || wordApp.ActiveDocument == null)
                return;

            Regex regex = CreateRegex();
            if(regex != null)
                Replace(regex);

            replaced_cntr = 0;
        }

        private void button_ReplaceAll_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(richTextBox_Regex.Text) || wordApp.ActiveDocument == null)
                return;

            Regex regex = CreateRegex();
            if (regex != null)
            {

                wordApp.ScreenUpdating = false;
                Word.UndoRecord ur = wordApp.UndoRecord;
                ur.StartCustomRecord("Convert all URLs to Hyperlinks");
                replaced_cntr = 0;
                previousFindResult = null;
                previousReplacement = "";
                reachedEnd = false;

                try
                {
                    MoveCursorToBeginningOfDocument();
                    while (!reachedEnd)
                    {
                        Replace(regex);
                    }
                }
                finally
                {
                    previousFindResult = null;
                    previousReplacement = "";
                    reachedEnd = false;
                    replaced_cntr = 0;
                    ur.EndCustomRecord();
                    wordApp.ScreenUpdating = true;
                }
            }
        }


        // -----------
        
        private void MoveCursorToBeginningOfDocument()
        {
            Word.Range rng = wordApp.ActiveDocument.Paragraphs[1].Range;

            rng.Collapse(Word.WdCollapseDirection.wdCollapseStart);

            rng.Select();
        }

        private Regex CreateRegex()
        {
            RegexOptions caseSensitive = RegexOptions.IgnoreCase;
            RegexOptions dotMatchesAll = 0;
            RegexOptions multiline = 0;
            if (checkBox_caseSensitive.Checked)
                caseSensitive = 0;
            if (checkBox_dotMatchesAll.Checked)
                dotMatchesAll = RegexOptions.Singleline;
            if (checkBox_Multiline.Checked)
                multiline = RegexOptions.Multiline;
            RegexOptions regexOptions = caseSensitive | dotMatchesAll | multiline;
            try
            {
                return new Regex(richTextBox_Regex.Text, regexOptions);
            }
            catch (Exception e)
            {
                richTextBox_Regex.BackColor = Color.FromArgb(240, 160, 160);
                return null;
            }
        }

        private bool reachedEnd = false;

        private void Find(Regex regex)
        {
            // the text of the document, and the starting place of the search in it
            Word.Range content = wordApp.ActiveDocument.Content;
            string fulltext = GetFullTextOfDocument(content);

            if (String.IsNullOrWhiteSpace(fulltext))
                return;

            int cursor_index;
            if (reachedEnd)
            {
                cursor_index = 0;
                reachedEnd = false;
            }
            else
            {
                cursor_index = wordApp.Selection.Start;
                if (wordApp.Selection.Range.Hyperlinks.Count > 0 || (wordApp.Selection.Text != null && previousFindResult != null && !String.IsNullOrWhiteSpace(previousFindResult.Value)
                    && (wordApp.Selection.Text.Equals(previousFindResult.Value, StringComparison.InvariantCultureIgnoreCase) || wordApp.Selection.Text.Equals(previousReplacement, StringComparison.InvariantCultureIgnoreCase) )))
                    cursor_index = wordApp.Selection.End;
            }

            // search
            Match match = regex.Match(fulltext, cursor_index);
            if (match.Success)
            {
                Word.Range rng = wordApp.ActiveDocument.Content;
                rng.SetRange(match.Index, match.Index + match.Length);
                if (rng.Hyperlinks.Count > 0)
                {
                    rng.Hyperlinks[1].Range.Select(); // this should be more elegant, but the Word API would overcomplicate it
                }
                else
                {
                    if (!rng.Text.Equals(match.Value, StringComparison.InvariantCultureIgnoreCase))
                        MessageBox.Show("An unexpected error occured", "Regex Find Error");
                    rng.Select();
                }

                previousFindResult = match; // or use Selection.Text? (whitespace after it)
            }
            else
            {
                reachedEnd = true;

                string message = "The Search operation has reached the end of the file.\nDo you want to start over from the beginning?";
                if (replaced_cntr > 0)
                    message = replaced_cntr + " occurences have been replaced.\n" + message;

                string caption = "Regex Search result";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.No)
                    this.Close(); 
                else if (result == DialogResult.Yes)
                    MoveCursorToBeginningOfDocument();
            }
        }

        /* The question arises here: why didn't I just use content.Text to acquire the content. It's because it returns a different text
         * from what the setRange() function works on (and using setRange later is unavoidable). You might be asking then, why not use
         * the TextRetrievalMode and ViewType properties of the content, but turns out they can't give you the same text that setRange()
         * is working on either. So unfortunately the ugly solution here seems unavoidable. The Characters array of content is also not usable.
         * Sadly, this solution produces a noticable slowdown with long documents, and I couldn't find a better solution with the Word API. */
        private string GetFullTextOfDocument(Word.Range content)
        {
            int length = content.End;
            StringBuilder sb = new StringBuilder(content.End);
            for (int i = 0; i < length; i++)
            {
                content.SetRange(i, i + 1);
                /* In certain cases a null is returned instead of a character (for example at the place of the special character representing
                 * the starting point of a Hyperlink block, but in other simpler cases too. There is no information about these in the 
                 * documentation, so I couldn't write a state machine for it. ) But if I didn't take these into consideration then it woulddn't
                 * be aligned correctly with setRange(), so I have to substitute it with something. Later it turned out that in some cases
                 * there are two characters returned by setRange(i, i+1): The '\r\a' symbol representing the end of a cell takes up two character
                 * spaces as a string, but only one in the Range logic. */
                if (content.Text == null)
                {
                    sb.Append(' ');
                }
                else
                {
                    if (content.Text.Length == 1)
                        sb.Append(content.Text);
                    else
                        sb.Append(content.Text[0]);
                }
            }
            return sb.ToString();
        }

        private void Replace(Regex regex)
        {
            if (wordApp.Selection.Text != null && previousFindResult != null && !String.IsNullOrWhiteSpace(previousFindResult.Value)
                && (wordApp.Selection.Text.Equals(previousFindResult.Value, StringComparison.InvariantCultureIgnoreCase) || wordApp.Selection.Text.Equals(previousReplacement, StringComparison.InvariantCultureIgnoreCase) ))
            {
                previousReplacement = ReplacementParser.GetSubstitutedText(textBox_Replace.Text, previousFindResult);
                wordApp.Selection.Text = previousReplacement;
                wordApp.ActiveDocument.Range(wordApp.Selection.Start, wordApp.Selection.Start + previousReplacement.Length).Select();
                replaced_cntr++;
            }

            Find(regex);
            previousReplacement = "";
        }

        // Regex coloring ----------

        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        private void ColorizeTextBox()
        {
            richTextBox_Regex.BackColor = Color.White;

            Color[] colors = RegexColorizer.Colorize(richTextBox_Regex.Text);

            int original_pos = richTextBox_Regex.SelectionStart;
            try
            {
                LockWindowUpdate(richTextBox_Regex.Handle); // to fix the flicker, that's what the try block is for
                for (int i = 0; i < colors.Length; i++)
                {
                    richTextBox_Regex.Select(i, 1);
                    richTextBox_Regex.SelectionColor = colors[i];
                }
                richTextBox_Regex.Select(original_pos, 0);
            }
            catch (Exception ex) { }
            finally
            {
                LockWindowUpdate(IntPtr.Zero);
            }
        }

        private void richTextBox_Regex_TextChanged(object sender, EventArgs e)
        {
            ColorizeTextBox();
        }

    }
}
