using System;
using System.IO;
using WordCodeEditorTools.CodeCleaning;
using WordCodeEditorTools.CodeIndentation;
using WordCodeEditorTools.SyntaxHighlighter;
using WordCodeEditorTools.WordSyntaxHighlighter;

namespace WordCodeEditorTools
{
    /// <summary>
    /// The Add-In pack contains three main components: a syntax highligter, a module to create code indentation and a source code cleaner.
    /// This class initializes these components (using their serialization and deserialization if necessary) and ties them to the user interface (Ribbon)
    /// </summary>
    public partial class ThisAddIn
    {
        private string directoryPath = 
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\SyntaxHighlighter\2.1";

        private Colorizer colorizer;
        private WordFormatter formatter;
        private WordIndentFixer indentFixer;
        private CodeCleaner codecleaner;

        public Colorizer Colorizer
        {
            get { return this.colorizer; }
            set
            {
                colorizer = value;
                formatter.Initialize(colorizer, this.Application);
            }
        }

        public WordFormatter Formatter
        {
            get { return this.formatter; }
        }

        public WordIndentFixer IndentFixer
        {
            get { return this.indentFixer; }
        }

        public CodeCleaner CodeCleaner
        {
            get { return this.codecleaner; }
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            if (Directory.Exists(directoryPath))
            {
                colorizer = ColorizerSerializer.Deserialize(directoryPath + @"\default.xml");
                formatter = WordFormatterSerializer.Deserialize(directoryPath + @"\basicformat.xml");
                formatter.Initialize(colorizer, this.Application);
            }
            else // if it's the first starting of this version of the Add-In with this user
            {
                Directory.CreateDirectory(directoryPath);

                colorizer = new Colorizer();
                colorizer.LoadPredefinedLanguages();
                colorizer.Initialize();

                formatter = new WordFormatter();
                formatter.Initialize(colorizer, this.Application);
                formatter.SetToDefault();
            }

            indentFixer = new WordIndentFixer(this.Application);
            codecleaner = new CodeCleaner(this.Application);

            Globals.Ribbons.WordCodeEditorToolsRibbon.InitializeAddIn(this);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            ColorizerSerializer.Serialize(directoryPath + @"\default.xml", colorizer);
            WordFormatterSerializer.Serialize(directoryPath + @"\basicformat.xml", formatter);

            codecleaner.ResetOriginalSettings();
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
