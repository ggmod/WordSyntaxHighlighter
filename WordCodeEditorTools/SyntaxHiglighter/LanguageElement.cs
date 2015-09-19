using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace WordCodeEditorTools.SyntaxHighlighter
{
    /// <summary>
    /// Represents a language element of a programming language, with the syntax rule describing the element and a style assigned to it. 
    /// </summary>
    [DataContract(Name = "LanguageElement", Namespace = "")]
    public class LanguageElement
    {
        /// <summary>
        /// Stores the name of the language element
        /// </summary>
        [DataMember()]
        private string name;

        /// <summary>
        /// Stores the color assigned to the language element
        /// </summary>
        [DataMember()]
        private Color color;

        /// <summary>
        /// Stores the character format style assigned to the language element
        /// </summary>
        [DataMember()]
        private CharacterFormat charFormat;

        /// <summary>
        /// Stores whether the language element is a list of keywords or not
        /// </summary>
        [DataMember()]
        private bool keywords = false;

        /// <summary>
        /// Stores the main regular expression that represents the syntax rule describing the element
        /// </summary>
        [DataMember()]
        private string mainRegex; 
        
        /// <summary>
        /// Stores the regular expression that describes what must preceed the language element in the text (can be empty)
        /// </summary>
        [DataMember()]
        private string separatorRegex_start;
        
        /// <summary>
        /// Stores the regular expression that describes what must follow the language element in the text (can be empty)
        /// </summary>
        [DataMember()]
        private string separatorRegex_end;

        /// <summary>
        /// Stores the regular expression that contains all syntactic rules needed to find the language element in a text
        /// </summary>
        /// <remarks>
        /// This is a combination of the mainRegex, separatorRegex_start and separatorRegex_end and some connective expressions.
        /// </remarks>
        private string masterRegex; 
        
        /// <summary>
        /// The programming language assigned to the language element. This is optional, and should only be used, when the element marks
        /// a code block that contains source code written in a different language than the element's parent language.
        /// </summary>
        [DataMember()]
        private Language subLanguage;

        /// <summary>
        /// Initializes a new instance of the LanguageElement class. 
        /// </summary>
        /// <remarks>
        /// Required for the deserialization by the DataContract
        /// </remarks>
        public LanguageElement() 
        {
            this.charFormat = new CharacterFormat();
        }

        /// <summary>
        /// Initializes a new instance of the LanguageElement class. 
        /// </summary>
        /// <param name="name">Name of the language element</param>
        /// <param name="color">The color the element will appear in</param>
        /// <param name="keywords">True if the element is a list of keywords</param>
        /// <param name="mainRegex">The main regex describing the element</param>
        /// <param name="separatorRegex_start">The regex describing what must preceed the element in the text</param>
        /// <param name="separatorRegex_end">The regex describing what must follow the element in the text</param>
        public LanguageElement(string name, Color color, bool keywords, string mainRegex, string separatorRegex_start, string separatorRegex_end)
        {
            this.name = name;
            this.color = color;
            this.keywords = keywords;
            this.mainRegex = mainRegex;
            this.separatorRegex_start = separatorRegex_start;
            this.separatorRegex_end = separatorRegex_end;

            this.charFormat = new CharacterFormat(); 

            this.Initialize();
        }

        /// <summary>
        /// Gets or sets the name of the language element
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets the color assigned to the language element
        /// </summary>
        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        /// <summary>
        /// Gets or sets the character style assigned to the language element
        /// </summary>
        public CharacterFormat CharFormat
        {
            get { return this.charFormat; }
            set { this.charFormat = value; }
        }

        /// <summary>
        /// Gets or sets the sublanguage assigned to the language element. This should only be used, when the element marks
        /// a code block that contains source code written in a different language than the element's parent language.
        /// </summary>
        public Language SubLanguage
        {
            get { return this.subLanguage; }
            set { this.subLanguage = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the element is a list of keywords or not
        /// </summary>
        public bool Keywords
        {
            get { return this.keywords; }
            set { this.keywords = value; }
        }

        /// <summary>
        /// Gets or sets the main regular expression describing the syntax rules of the language element
        /// </summary>
        public string MainRegex
        {
            get { return this.mainRegex; }
            set { this.mainRegex = value; }
        }

        /// <summary>
        /// Gets or sets the regular expression that describes what must preceed the language element in the text (can be empty)
        /// </summary>
        public string SeparatorRegex_start
        {
            get { return this.separatorRegex_start; }
            set { this.separatorRegex_start = value; }
        }

        /// <summary>
        /// Gets or sets regular expression that describes what must follow the language element in the text (can be empty)
        /// </summary>
        public string SeparatorRegex_end
        {
            get { return this.separatorRegex_end; }
            set { this.separatorRegex_end = value; }
        }

        /// <summary>
        /// Gets the regular expression that contains all syntactic rules needed to find the language element in a text
        /// </summary>
        public string MasterRegex
        {
            get { return this.masterRegex; }
        }

        /// <summary>
        /// Initializes the language element by creating the MasterRegex, that combines the mainRegex, separatorRegex_start and separatorRegex_end
        /// into an expression that contains all the syntactic rules needed to find the language element in a text. 
        /// </summary>
        /// <remarks>
        /// This method is automatically called by the parent language's Initialize method for all of its elements.
        /// </remarks>
        public void Initialize()
        {
            StringBuilder sb = new StringBuilder("");

            if (!String.IsNullOrWhiteSpace(mainRegex))
            {
                if (!String.IsNullOrWhiteSpace(separatorRegex_start))
                {
                    sb.Append(@"(?<=");
                    sb.Append(Regex.Replace(separatorRegex_start.Trim(), @" +", @"|"));
                    sb.Append(@")");
                }
                sb.Append(@"(");

                if (keywords == true)
                {
                    sb.Append(Regex.Replace(mainRegex.Trim(), @" +", @"|"));
                }
                else
                {
                    sb.Append(mainRegex.Trim());
                }

                sb.Append(@")");
                if (!String.IsNullOrWhiteSpace(separatorRegex_end))
                {
                    sb.Append(@"(?=");
                    sb.Append(Regex.Replace(separatorRegex_end.Trim(), @" +", @"|"));
                    sb.Append(@")");
                }
            }
            masterRegex = sb.ToString();
        }
    }
}
