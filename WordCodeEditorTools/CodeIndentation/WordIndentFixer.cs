using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.Windows.Forms;
        
namespace WordCodeEditorTools.CodeIndentation
{
    /// <summary>
    /// This class interacts with Word documents using the Word API, to indent the selected source code samples. 
    /// An instances of this class has a list of IndentingStrategies: a strategy can process the source code sample and 
    /// calculate indentation for it. The task of this class is to implement that calculated indentation on the Word Document.
    /// </summary>
    public class WordIndentFixer
    {
        private Word.Application wordApp;

        private char indentUnit;
        public char IndentUnit
        {
            get { return indentUnit; }
            set 
            {
                if (value != ' ')
                    indentRepeat = 1;
                indentUnit = value; 
            }
        }

        private float indentLength;
        public float IndentLength
        {
            get { return indentLength; }
            set { indentLength = value; }
        }

        private int indentRepeat;
        public int IndentRepeat
        {
            get { return indentRepeat; }
            set { indentRepeat = value; }
        }
        private bool useTabStops;
        public bool UseTabStops
        {
            get { return useTabStops; }
            set { useTabStops = value; }
        }
        private bool useLineWrap;
        public bool UseLineWrap
        {
            get { return useLineWrap; }
            set { useLineWrap = value; }
        }

        private List<IndentingStrategy> indentingStrategies;

        public WordIndentFixer(Word.Application app) { 
            wordApp = app;
            IndentUnit = '\t';
            IndentLength = 17.0f;
            IndentRepeat = 1;
            useTabStops = true;
            useLineWrap = true;

            indentingStrategies = new List<IndentingStrategy>(2);
            indentingStrategies.Add(new C_LikeIndentingStrategy());
            indentingStrategies.Add(new XML_LikeIndentingStrategy());
        }

        public void Indent(int strategy)
        {
            Word.UndoRecord ur = wordApp.UndoRecord;
            ur.StartCustomRecord("Code Indentation");
            //wordApp.ScreenUpdating = false; // could be useful for slow computers

            if (wordApp.Selection.Range.Text != null)   
            {
                wordApp.Selection.NoProofing = 1; // spell checks are usually just add noise to source code samples in Word Documents
                IndentContent(strategy);
            } 

            wordApp.ScreenUpdating = true;
            ur.EndCustomRecord();
        }

        private void IndentContent(int strategy)
        {
            ReplaceVerticalTabsWithNewLine(); // on the original text BEFORE the method counts the number of pharagraphs in it

            /* The Selection can disappear while its contents are processed, and thus the pharagraphs from 'wordApp.Selection.Paragraphs'
             * would also disappear. To solve this, I have to maintain an own list of the pharagraphs. 
             * Also it's necessary to work with pharagraphs instead of working with the whole selection, because this is the only approach that works if the selection is inside a table */
            List<Word.Paragraph> pars = new List<Word.Paragraph>();     
            foreach (Word.Paragraph par in wordApp.Selection.Paragraphs) // it counts the replaced '\v'-s
                pars.Add(par);

            if (InvalidParagraphs(pars))
                return;

            int par_count = pars.Count;
            string selection = getSelectedText(pars);

            if (strategy == -1)
            {
                RemoveExistingIndentation(pars); 
            }
            else if(strategy == 0 || strategy == 1)
            {
                if (useTabStops)
                    AddTabStops(wordApp.Selection);

                RemoveExistingIndentation(pars); // it's not guaranteed, that the Selection is still alive after processing its contents (e.g. calling Delete() on the first character of the selection causes it to disappear)
                int[] indents = indentingStrategies[strategy].CalculateNewIndentation(selection, par_count); // Word independent part, handled in another class
                ApplyNewIndentation(pars, indents);

                if (useTabStops && useLineWrap)
                    WordWrappingWithHangindIndent(pars, indents);
            }
        }

        private void AddTabStops(Word.Selection sel)
        {
            float codeblock_width;
            // in a table tab stops should be placed only where the cell is located horizontally
            if (sel.get_Information(Word.WdInformation.wdWithInTable) == true)
            {
                sel.SelectColumn();
                codeblock_width = wordApp.Selection.Columns[1].Width;   // no too elegant... but there is no better solution in the Word API
            }
            else
            {
                codeblock_width = sel.PageSetup.PageWidth - sel.PageSetup.RightMargin - sel.PageSetup.LeftMargin;
            }

            sel.ParagraphFormat.TabStops.ClearAll();
            
            for (float j = indentLength; j < codeblock_width; j += indentLength)
            {
                Object alignmentType = Word.WdTabAlignment.wdAlignTabLeft;
                Object tabLeader = Word.WdTabLeader.wdTabLeaderSpaces;
                sel.ParagraphFormat.TabStops.Add(j, ref alignmentType, ref tabLeader);
            }
        }

        // no ugly line wraps, where the end of the pharagraph starts at the left border, and not at the tab stops
        private void WordWrappingWithHangindIndent(List<Word.Paragraph> pars, int[] indents)
        {
            int i = 0;
            foreach (Word.Paragraph par in pars)
            {
                float hangind_indent_points = par.LeftIndent - par.FirstLineIndent;
                if ( Math.Abs(hangind_indent_points) < 0.1) // if it already has indentation, there is no point in screwing with it. Also it shouldn't remove the paragraph formatting, as it can contain other things that are needed.
                {
                    par.TabHangingIndent((short)(indents[i] + 1)); // the API is confusing here: it shifts it with this many units, in addition to the existing indentation
                }
                i++;           
            }
        }
            
        /*
         * Because of the strange quirks of the Word API, the text must be acquired as a lsit of pharagraphs, and then merged back together. 
         * In a table the following behaviour appears:
         * Selection.Text - it contains only the text of the first cell
         * Selection.Range.Text - it can contain the text from cells that are not selected.
         */
        private string getSelectedText(List<Word.Paragraph> pars)
        {
            StringBuilder builder = new StringBuilder();
            foreach(Word.Paragraph par in pars)
            {
                string line = par.Range.Text.Replace("\a", ""); // The Word uses '\a' as an end of cell sign
                builder.Append(line);
            }
            return builder.ToString();
        }

        /* A rare error is that in text manupulated programattically or inserted from other applications into Word, some pharagraph signs can remain undetected by Word, and thus pharagraph formatting woul be messed up by this.*/
        private bool InvalidParagraphs(List<Word.Paragraph> pars)
        {
            bool invalid = false;
            foreach (Word.Paragraph par in pars)
            {
                int newline_cntr = 0;
                string line = par.Range.Text;
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '\r')
                    {
                        newline_cntr++;
                        if (newline_cntr > 1)
                        {
                            invalid = true;
                            break;
                        }
                    }
                }
            }
            if (invalid)
            {
                MessageBox.Show("The selected text contains incorrect paragraphs.", "Code Indentation Error");
            }
            return invalid;
        }

        private void ReplaceVerticalTabsWithNewLine()
        {

            /* It's not possible to use C#'s string function here and then replace 'Range.Text' with the result (the Syntax Highlighter does this), 
             * because the returned text would lose its formatting. The Word API's Find and Replace ('\x0B' -> '\r') cannot be used here either, 
             * because Word wouldn't recognize the inserted newlines as paragraph terminations, and this could cause very serious bugs in Word. 
             * The only way to safely insert newlines is by using '.AddParagraph()'. Thus I have to manually process the text character by character, 
             * but the Word API shouldn't be used for that, cause it's extremely slow! */
            if (wordApp.Selection.Range.Text.Contains("\v"))
            {
                int count = wordApp.Selection.Range.Characters.Count;
                string text = wordApp.Selection.Range.Text;

                // It can't handle the case where the selection spans through a table, or even if a whole cell is selected. (But it works if the selection is inside a cell)
                // If the first character is '\v' it removes the selection, and it's impossible to do this, even with the "List<Paragraph>" trick used earlier
                int cell_counter = 0;
                for (int i = 0; i < count; i++)
                {
                    if (text[i] == '\a')
                        cell_counter++;
                }
                if (cell_counter > 0)
                    return;
                
                // Calling InsertParagraph on a selection that contains part of a table causes the selections to disappear

                for (int i = 0; i < count; i++) //foreach doesn't like if the thing it's iterating is changed
                {
                    if (text[i] == '\v')  // the reason for not using Range.Characters[] is that its a hundred times slower
                    {
                        wordApp.Selection.Range.Characters[i + 1].InsertParagraph(); // the Word API is confusing here: it's called Insert, but it replaces. (i+1 is used, because Word start the numbering with 1, not 0)
                    }
                }
            }
        }

        private void RemoveExistingIndentation(List<Word.Paragraph> pars)
        {
            foreach (Word.Paragraph par in pars)
            {
                string line = par.Range.Text;
                if (line[line.Length - 1] == '\a')
                    line = line.Remove(line.Length - 1); // '\a' at the end causes problems, cause it's not whitespace, and thus "\r\a" is not an all whitesapces line

                int whitespace_cntr = 0;
                while (true)
                {
                    if (whitespace_cntr < line.Length && Char.IsWhiteSpace(line[whitespace_cntr]))
                        whitespace_cntr++;
                    else
                        break;
                }

                if (whitespace_cntr == line.Length && line.Length > 0) // all whitespace line ('\r' is whitesapce, '\a' isn't)
                {
                    whitespace_cntr--;
                }

                Word.Range range = par.Range;
                if (line.Length > 1 && whitespace_cntr > 0) 
                {
                    range.SetRange(par.Range.Start, par.Range.Start + whitespace_cntr);
                    range.Delete(); // the reason for using SetRange is that removing the first character of the selection causes it do disappear
                }
            }
        }

        private void ApplyNewIndentation(List<Word.Paragraph> pars, int[] indent) 
        {
            int i = 0;
            foreach (Word.Paragraph par in pars)
            {
                if (indent[i] > 0)
                {
                    string insert = new String(IndentUnit, indentRepeat * indent[i]);
                    Word.Range range = par.Range;
                    range.InsertBefore(insert);
                }
                i++;
            }
        }

    }
}
