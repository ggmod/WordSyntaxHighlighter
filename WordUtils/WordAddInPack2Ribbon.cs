using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using WordUtils.WordURLManager;
using WordUtils.WordRegexSearch;
using System.Windows.Forms;

namespace WordUtils
{
    /// <summary>
    /// The main User Interface of the Add-Ins. There are additional Windows Forms that can be opened by clicking on some buttons of the Ribbon.
    /// </summary>
    public partial class WordUtilsRibbon
    {
        private Word.Application wordApp;

        private void WordUtilsRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            wordApp = Globals.ThisAddIn.Application;
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            Word.Document doc = wordApp.ActiveDocument;

            IntPtr wordWindowHandle = FindWindowW("OpusApp", Globals.ThisAddIn.Application.ActiveWindow.Caption + " - Microsoft Word");
            IWin32Window wordWindow = Control.FromHandle(wordWindowHandle);

            UrlListForm form = new UrlListForm(doc);
            form.Show(wordWindow);         
        }

        private void button_HyperlinkConverter_Click(object sender, RibbonControlEventArgs e)
        {
            WordUrlManager.ConvertAllUrlsToHyperlinks(wordApp);
        }

        private void button_RemoveHyperlinks_Click(object sender, RibbonControlEventArgs e)
        {            
            WordUrlManager.RemoveAllHyperlinks(wordApp);
        }

        private void button_AutoUrlDetectionON_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.UrlManager.SwitchAutoHyperlinkDetection(wordApp, true);
        }

        private void button_AutoUrlDetectionOFF_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.UrlManager.SwitchAutoHyperlinkDetection(wordApp, false);
        }

        //Regex Search 

        RegexFindForm regexFindForm;
        string regex = "";

        private void button_RegexFind_Click(object sender, RibbonControlEventArgs e)
        {
            if (regexFindForm == null || regexFindForm.IsDisposed || regexFindForm.Disposing)
            {
                IntPtr wordWindowHandle = FindWindowW("OpusApp",Globals.ThisAddIn.Application.ActiveWindow.Caption+" - Microsoft Word");
                IWin32Window wordWindow = Control.FromHandle(wordWindowHandle);

                regexFindForm = new RegexFindForm(Globals.ThisAddIn.Application, regex);
                regexFindForm.FormClosing += new FormClosingEventHandler(regexFindForm_FormClosing);
                regexFindForm.Show(wordWindow);
            }
        }

        // event handler
        private void regexFindForm_FormClosing(object sender, EventArgs e)
        {
            regex = regexFindForm.Regex;
        }

        /* Helper function for windows that stay on top, but only while the parent application is active. 
         * This is how the Find window of Microsoft Word works for example: The user can edit the document, yet the Find window remains
         * on top, even when it's not active. If the user switches to another application however, the Find window should disappear, since it
         * belongs to the MS Word parent window. This behaviour cannot be achieved using only the "TopMost" property of the Form, and unfortunately
         * I couldn't find any way to make this work using only .NET code */
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "FindWindowW")]
        public static extern System.IntPtr FindWindowW(
            [System.Runtime.InteropServices.InAttribute()] 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] 
            string lpClassName, [System.Runtime.InteropServices.InAttribute()] 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] 
            string lpWindowName);

    }
}
