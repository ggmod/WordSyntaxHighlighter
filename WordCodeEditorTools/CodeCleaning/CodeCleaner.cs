using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;

namespace WordCodeEditorTools.CodeCleaning
{
    /// <summary>
    /// This class interacts with Word documents using the Word API, to perform some simple tasks on the document that the user can request.
    /// </summary>
    public class CodeCleaner
    {
        private Word.Application wordApp;

        private bool originalSettings_AsYouTypeReplaceQuotes;
        private bool originalSettings_ReplaceQuotes;
        private bool originalSettings_AsYoutTypeReplaceSymbols;
        private bool originalSettings_ReplaceSymbols;
        private bool smartQuoteSwitchUsed;

        public CodeCleaner(Word.Application app)
        {
            wordApp = app;

            Word.Options options = wordApp.Application.Options;
            originalSettings_AsYouTypeReplaceQuotes = options.AutoFormatAsYouTypeReplaceQuotes;
            originalSettings_ReplaceQuotes = options.AutoFormatReplaceQuotes;
            originalSettings_AsYoutTypeReplaceSymbols = options.AutoFormatAsYouTypeReplaceSymbols;
            originalSettings_ReplaceSymbols = options.AutoFormatReplaceSymbols;
            smartQuoteSwitchUsed = false;
        }

        public void ResetOriginalSettings()
        {
            if (smartQuoteSwitchUsed) // if the user hasn't used the Add-In but changed these settings in Word then he or she would be quiet surprised to see his settings overwritten
            {
                Word.Options options = wordApp.Application.Options;
                options.AutoFormatAsYouTypeReplaceQuotes = originalSettings_AsYouTypeReplaceQuotes;
                options.AutoFormatReplaceQuotes = originalSettings_ReplaceQuotes;
                options.AutoFormatAsYouTypeReplaceSymbols = originalSettings_AsYoutTypeReplaceSymbols;
                options.AutoFormatReplaceSymbols = originalSettings_ReplaceSymbols;
            }
        }

        private readonly char[,] quotes = {
                { '\u201C' , '\u0022' },    //4 curly double quotes -> "
                { '\u201D' , '\u0022' },
                { '\u201E' , '\u0022' },
                { '\u201F' , '\u0022' },
                { '\u2018' , '\u0027' },    //4 curly single quotes -> '
                { '\u2019' , '\u0027' },
                { '\u201A' , '\u0027' },
                { '\u201B' , '\u0027' },
                { '\u2032' , '\u0022' },    // simple and double prime
                { '\u2033' , '\u0027' }
            };                              // there are about 30 other quote characters, that might appear in different languages

        public void ReplaceSmartQuotesInSelection()
        {
            Word.UndoRecord ur = wordApp.UndoRecord;
            ur.StartCustomRecord("Code Indentation");

            bool smartQuotesAsYouType_old = wordApp.Application.Options.AutoFormatAsYouTypeReplaceQuotes;
            bool smartQuotes_old = wordApp.Application.Options.AutoFormatReplaceQuotes;
            wordApp.Application.Options.AutoFormatAsYouTypeReplaceQuotes = false;
            wordApp.Application.Options.AutoFormatReplaceQuotes = false;

            /* If there is no selection, then Selection.Find searches the entire document!
             * Selection.Text contains one character if only one character is selected of course, but also if there is nothing selected at all!
             * The only thing distinguishing these two completely different states is Selection.Range: if 1 character is selected it has a Range.Text object, if nothing is selected it doesn't have one 
             * Other not documented workings of the Word API: if only one character is selected, and it matches the search criteria, then the search 
             * continues through the entire document, even if it's explicitely set that it has to stop at the end of the selection. */
            if (wordApp.Selection.Range.Text != null) { 
                
                if(wordApp.Selection.Text.Length == 1)      
                    ReplaceQuotes_OneCharacterSelected(quotes);
                else
                    ReplaceQuotes_Deafult(quotes);
            }

            wordApp.Application.Options.AutoFormatAsYouTypeReplaceQuotes = smartQuotesAsYouType_old;
            wordApp.Application.Options.AutoFormatReplaceQuotes = smartQuotes_old;
            ur.EndCustomRecord();
        }

        private void ReplaceQuotes_OneCharacterSelected(char[,] quotes)
        {
            char c = wordApp.Selection.Text[0];
            bool found = false;
            int i;
            for (i = 0; i < quotes.Length / 2; i++)
            {
                if (quotes[i, 0] == c)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                return;

            wordApp.Selection.Range.Text = quotes[i, 1].ToString();
            // inevitably removes the selection from that one character, and the pointer of the Range object becomes invalid too
        }

        private void ReplaceQuotes_Deafult(char[,] quotes)
        {
            object missing = Type.Missing;
            object replaceAll = Word.WdReplace.wdReplaceAll;
            object wrap = Word.WdFindWrap.wdFindStop;
                
            wordApp.Selection.Find.ClearFormatting(); 
            wordApp.Selection.Find.Replacement.ClearFormatting();

            for(int i = 0; i < quotes.Length/2; i++) {
                wordApp.Selection.Find.Text = quotes[i,0].ToString();
                wordApp.Selection.Find.Replacement.Text = quotes[i,1].ToString();
                
                /* This must look crazy, but because of the NoProofing property it either finds only the 
                 * results where NoProofing is applied, or only the ones where it isn't, but obviously most people need both */
                wordApp.Selection.Find.NoProofing = 1;
                wordApp.Selection.Find.Execute(
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref wrap, ref missing, ref missing,
                    ref replaceAll, ref missing, ref missing, ref missing, ref missing);

                wordApp.Selection.Find.NoProofing = 0;
                wordApp.Selection.Find.Execute(
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref wrap, ref missing, ref missing,
                    ref replaceAll, ref missing, ref missing, ref missing, ref missing);
            }
        }
        
        public void InsertCharacter(char c)
        {
            Word.Options options = wordApp.Application.Options;
            bool smartQuotesAsYouType_old = options.AutoFormatAsYouTypeReplaceQuotes;
            bool smartQuotes_old = options.AutoFormatReplaceQuotes;
            options.AutoFormatAsYouTypeReplaceQuotes = false;
            options.AutoFormatReplaceQuotes = false;

            wordApp.Selection.Collapse(Word.WdCollapseDirection.wdCollapseStart);
            wordApp.Selection.InsertAfter(c.ToString());
            wordApp.Selection.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

            options.AutoFormatAsYouTypeReplaceQuotes = smartQuotesAsYouType_old;
            options.AutoFormatReplaceQuotes = smartQuotes_old;
        }

        public void NoSpellCheckOnSelection()
        {
            wordApp.Selection.NoProofing = 1;
        }

        public void ReverseNoSpellCheckOnSelection()
        {
            wordApp.Selection.NoProofing = 0;
        }

        public void TurnSmartQuoteSettingsOn()
        {
            Word.Options options = wordApp.Application.Options;
            options.AutoFormatAsYouTypeReplaceQuotes = true;
            options.AutoFormatReplaceQuotes = true;
            options.AutoFormatAsYouTypeReplaceSymbols = true;

            smartQuoteSwitchUsed = true;
        }

        public void TurnSmartQuoteSettingsOff()
        {
            Word.Options options = wordApp.Application.Options;
            options.AutoFormatAsYouTypeReplaceQuotes = false;
            options.AutoFormatReplaceQuotes = false;
            options.AutoFormatAsYouTypeReplaceSymbols = false;

            smartQuoteSwitchUsed = true;
        }
    }
}
