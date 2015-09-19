using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using WordUtils.WordURLManager;

namespace WordUtils
{
    public partial class ThisAddIn
    {
        private WordUrlManager urlManager;
        public WordUrlManager UrlManager
        {
            get { return urlManager; }
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            urlManager = new WordUrlManager(Globals.ThisAddIn.Application);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            urlManager.ResetOriginalSettings(Globals.ThisAddIn.Application);
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
