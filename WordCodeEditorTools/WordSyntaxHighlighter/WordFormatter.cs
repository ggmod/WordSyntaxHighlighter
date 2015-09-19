using System;
using System.Drawing;
using System.Windows.Forms;
using WordCodeEditorTools.SyntaxHighlighter;
using Word = Microsoft.Office.Interop.Word;

namespace WordCodeEditorTools.WordSyntaxHighlighter
{
    /// <summary>
    /// Represents an object that is capable of executing the formatting tasks specified by its Colorizer on a Microsoft Word document.
    /// All Microsoft Word specific code of the Syntax Highlighter is contained in this class. 
    /// </summary>
    public class WordFormatter : TextFormatter
    {
        /// <summary>
        /// The running Microsoft Word application that is used to perform the formattings. The active document of the application is used.
        /// </summary>
        private Word.Application wordApp;

        /// <summary>
        /// The Colorizer object that processes some text from the Word document, and determines its colors based on the programming language's syntax rules.
        /// </summary>
        private Colorizer colorizer;

        /* Style settings for the selected text. These fields can be changed by the user, and they are serialized to preserve these changes. */
        private Color frameBgColor;
        private Color frameAlternatingColor;
        private Color frameBorderColor;
        private bool frameAlternatingLines;
        private string fontName;
        private float fontSize;

        public Color FrameBgColor
        {
            get { return this.frameBgColor; }
            set { this.frameBgColor = value; }
        }

        public Color FrameAlternatingColor
        {
            set { this.frameAlternatingColor = value; }
            get { return this.frameAlternatingColor; }
        }

        public Color FrameBorderColor
        {
            set { this.frameBorderColor = value; }
            get { return this.frameBorderColor; }
        }

        public bool FrameAlternatingLines
        {
            get { return this.frameAlternatingLines; }
            set { this.frameAlternatingLines = value; }
        }

        public string FontName
        {
            set { this.fontName = value; }
            get { return this.fontName; }
        }

        public float FontSize
        {
            set { this.fontSize = value; }
            get { return this.fontSize; }
        }

        /// <summary>
        /// Initializes a new instance of the type WordFormatter class. The default style settings are set. 
        /// </summary>
        public WordFormatter()
        {
            SetToDefault();
        }

        /// <summary>
        /// The formatter needs an application (or more precisely its active document) to format, and a colorizer, to determine what colors to use for the formatting. These references must be initialized before any real work can be done.
        /// </summary>
        /// <param name="colorizer_"></param>
        /// <param name="application_"></param>
        public void Initialize(Colorizer colorizer_, Word.Application application_)
        {
            colorizer = colorizer_;
            wordApp = application_;
        }

        public void SetToDefault()
        {
            frameAlternatingLines = true;
            frameBgColor = Color.White;
            frameAlternatingColor = Color.FromArgb(238, 248, 248);
            frameBorderColor = Color.FromArgb(160, 175, 255);
            fontName = "Consolas";
            fontSize = 9.0f;
        }

        /// <summary>
        /// Creates the paragraph formatting of the selected text using the style settings and user preferences. 
        /// </summary>
        /// <param name="use_frame">determines whether the text is put in a frame</param>
        /// <param name="use_numbering">determines whether line numbering is used</param>
        public void ParagraphFormatting(bool use_frame, bool use_numbering)
        {
            Word.WdColor frameAlternatingColor_ = (Word.WdColor)ColorTranslator.ToOle(frameAlternatingColor);
            Word.WdColor frameBgColor_ = (Word.WdColor)ColorTranslator.ToOle(frameBgColor);
            Word.WdColor frameBorderColor_ = (Word.WdColor)ColorTranslator.ToOle(frameBorderColor);

            Word.Selection sel = wordApp.Selection; // just to make the code cleaner

            sel.ParagraphFormat.LineSpacing = 12.0f;
            sel.ParagraphFormat.SpaceBefore = 0.0f;
            sel.ParagraphFormat.SpaceAfter = 0.0f;

            /* If line numbering is used, then the text block must be converted into a table, which can be quiet resource demanding */
            if (use_numbering)
            {
                object missing = Type.Missing;
                object dontuse = false;
                Word.Table t = sel.ConvertToTable(Word.WdTableFieldSeparator.wdSeparateByParagraphs, ref missing, 1, ref missing, ref missing, ref missing, ref dontuse, ref dontuse, ref dontuse, ref dontuse, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                t.Columns.Add(t.Columns[1]);
                t.Columns[1].Width = 35.0f;
                t.Columns[2].Width = sel.PageSetup.PageWidth - sel.PageSetup.RightMargin - sel.PageSetup.LeftMargin - 35.0f;
                              
                if (use_frame)
                {
                    t.Columns[1].Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    t.Columns[1].Borders[Word.WdBorderType.wdBorderRight].Color = frameBorderColor_;
                    t.Columns[1].Borders[Word.WdBorderType.wdBorderRight].LineWidth = Word.WdLineWidth.wdLineWidth300pt;
                    t.Columns[1].Shading.BackgroundPatternColor = Word.WdColor.wdColorGray05;

                    int i_ = 0;
                    foreach (Word.Cell c in t.Columns[2].Cells)
                    {
                        if (frameAlternatingLines && (i_ % 2 == 1))
                            c.Shading.BackgroundPatternColor = frameAlternatingColor_;
                        else
                            c.Shading.BackgroundPatternColor = frameBgColor_;
                        c.LeftPadding = 7.0f;
                        i_++;
                    }

                    t.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    t.Borders[Word.WdBorderType.wdBorderTop].Color = frameBorderColor_;
                    t.Borders[Word.WdBorderType.wdBorderTop].LineWidth = Word.WdLineWidth.wdLineWidth075pt;
                    t.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    t.Borders[Word.WdBorderType.wdBorderLeft].Color = frameBorderColor_;
                    t.Borders[Word.WdBorderType.wdBorderLeft].LineWidth = Word.WdLineWidth.wdLineWidth075pt;
                    t.Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    t.Borders[Word.WdBorderType.wdBorderRight].Color = frameBorderColor_;
                    t.Borders[Word.WdBorderType.wdBorderRight].LineWidth = Word.WdLineWidth.wdLineWidth075pt;
                    t.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    t.Borders[Word.WdBorderType.wdBorderBottom].Color = frameBorderColor_;
                    t.Borders[Word.WdBorderType.wdBorderBottom].LineWidth = Word.WdLineWidth.wdLineWidth075pt;
                }

                int i = 0;
                foreach (Word.Cell c in t.Columns[1].Cells)
                {
                    c.Range.Font.ColorIndex = Word.WdColorIndex.wdGray50;
                    c.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                    c.RightPadding = 7.0f;
                    c.Range.InsertAfter((i + 1).ToString() + ".");
                    i++;
                }
            }
            else if (use_frame)
            {
                sel.Paragraphs.Last.SpaceAfter = 12.0f;
                sel.Paragraphs.First.SpaceBefore = 12.0f;

                int i_ = 0;
                foreach (Word.Paragraph p in sel.Paragraphs)
                {
                    if (frameAlternatingLines && (i_ % 2 == 1))
                        p.Shading.BackgroundPatternColor = frameAlternatingColor_;
                    else
                        p.Shading.BackgroundPatternColor = frameBgColor_;
                    i_++;
                }

                sel.ParagraphFormat.Borders[Word.WdBorderType.wdBorderTop].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                sel.ParagraphFormat.Borders[Word.WdBorderType.wdBorderTop].Color = frameBorderColor_;
                sel.ParagraphFormat.Borders[Word.WdBorderType.wdBorderTop].LineWidth = Word.WdLineWidth.wdLineWidth075pt;
                sel.ParagraphFormat.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                sel.ParagraphFormat.Borders[Word.WdBorderType.wdBorderLeft].Color = frameBorderColor_;
                sel.ParagraphFormat.Borders[Word.WdBorderType.wdBorderLeft].LineWidth = Word.WdLineWidth.wdLineWidth300pt;
                sel.ParagraphFormat.Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                sel.ParagraphFormat.Borders[Word.WdBorderType.wdBorderRight].Color = frameBorderColor_;
                sel.ParagraphFormat.Borders[Word.WdBorderType.wdBorderRight].LineWidth = Word.WdLineWidth.wdLineWidth075pt;
                sel.ParagraphFormat.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                sel.ParagraphFormat.Borders[Word.WdBorderType.wdBorderBottom].Color = frameBorderColor_;
                sel.ParagraphFormat.Borders[Word.WdBorderType.wdBorderBottom].LineWidth = Word.WdLineWidth.wdLineWidth075pt;

                sel.ParagraphFormat.Borders.DistanceFromLeft = 7;
            }

        }

        /// <summary>
        /// Colorizes the selected text using the selected programming language. This method is usually called when the user clicks on the Colorize button.
        /// The method creates the pharagraph formatting of the text, and then redirects to the Colorizer to determine the syntax based coloring. The Colorizer will then call back the Formatter to color individual words or characters.
        /// </summary>
        /// <param name="language">The programming language that the selected text is written in</param>
        /// <param name="use_frame">Determines whether the code block will be put in a frame</param>
        /// <param name="use_linenumbering">Determines whether line numbering should be used</param>
        public void Colorize(string language, bool use_frame, bool use_linenumbering)
        {
            if (wordApp.Selection.Text.Length > 1) // If there is nothing selected, this property still contains 1 character.
            {
                if (TooLargeInput(use_frame, use_linenumbering, wordApp.Selection.Paragraphs.Count))
                    return;

                // Using an UndoRecord enables the user the "undo" all the coloring at once. Without it every formatting action would be an individual element in the undo stack. 
                Word.UndoRecord ur = wordApp.UndoRecord;
                ur.StartCustomRecord("Syntax Highlighting");
                //wordApp.ScreenUpdating = false; // might be useful on slow computers?

                // Vertical tab characters are replaced with newlines, as they would usually mess up the pharagraph formatting
                if (wordApp.Selection.Text.Contains("\x0B"))
                {
                    string temp = wordApp.Selection.Text.Replace("\x0B", "\n");
                    wordApp.Selection.Text = temp.Remove(temp.Length - 1); // Replace inserts a '\n' at the end that must be removed
                }

                wordApp.Selection.ClearFormatting(); // All previous formatting is removed! Otherwise the result of the formatting could be quiet unpredictable
                wordApp.Selection.NoProofing = 1; // Spell checks usually just add noise to a code block in a document
                wordApp.Selection.Font.Name = this.fontName;
                wordApp.Selection.Font.Size = this.fontSize;

                try
                {
                    // Calculates what colors should be used for each word or character, and calls back the formatter
                    colorizer.Colorize(this, language, wordApp.Selection.Text);
                }
                catch (InvalidLanguageException e)
                {
                    MessageBox.Show(e.Message, "Syntax Highlighter Error");
                }
                catch (InvalidRegexException e)
                {
                    MessageBox.Show(e.Message, "Syntax Highlighter Error");
                }

                this.ParagraphFormatting(use_frame, use_linenumbering);

                wordApp.ScreenUpdating = true;
                ur.EndCustomRecord();
            }
        }

        /// <summary>
        /// If the selected text is large, the formatting process using the Word API could take a very long time. In these cases the program asks the user for comfirmation. 
        /// </summary>
        /// <param name="use_frame"></param>
        /// <param name="use_linenumbering"></param>
        /// <param name="lines_cnt">Number of lines in the selection</param>
        /// <returns>Returns whether the selected text is too large to be processed</returns>
        private bool TooLargeInput(bool use_frame, bool use_linenumbering, int lines_cnt)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            string message;
            string caption = "Syntax Highlighter Warning";

            if (use_frame && use_linenumbering &&  lines_cnt > 80)
            {
                message = "Line numbering converts the selection into a table.\n" +
                    "With so many lines selected this can take quite a few seconds.\n" +
                    "Are you sure you want to continue?\n";
                result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.No)
                    return true;
            }
            if (lines_cnt > 3000)
            {
                message = "With so many lines selected the process can take quite a few seconds.\n" +
                    "Are you sure you want to continue?\n";
                result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.No)
                    return true;
            }

            return false;
        }
               
        /// <summary>
        /// Colors a range of text in the document with the specified color and character formatting. This is the callback function used by the Colorizer:
        /// Every time it founds a language element that must be colored, it calls back this function, that performs the formatting using the Word API. This way the Word specific code is separated from the syntax processing of the text.
        /// </summary>
        /// <param name="index">The starting position of the range that must be colored (0 is the first character of the selection, that is the text that is colorized)</param>
        /// <param name="length">The length of the range to be colored</param>
        /// <param name="color">Color to use for the range of text</param>
        /// <param name="char_format">Character formatting to use for the range of text</param>
        public void ColorRangeOfText(int index, int length, Color color, CharacterFormat char_format)
        {
            Word.WdColor wd_color = (Word.WdColor)ColorTranslator.ToOle(color);
            if (color.Name == "Control")
                wd_color = Word.WdColor.wdColorAutomatic;

            Word.Range range = wordApp.Selection.Range; //It must look weird, but you have to get an existing Range object first, and then narrow it down. (Range is an abstract class)
            range.SetRange(wordApp.Selection.Start + index, wordApp.Selection.Start + index + length);
            range.Font.Color = wd_color;

            if (char_format.Bold) range.Font.Bold = 1;
            if (char_format.Italic) range.Font.Italic = 1;
            if (char_format.Underlined) range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
            if (char_format.Capitalletters) range.Font.AllCaps = 1;
        }

    }
}
