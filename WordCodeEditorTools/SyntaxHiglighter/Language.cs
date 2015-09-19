using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace WordCodeEditorTools.SyntaxHighlighter 
{
    /// <summary>
    /// Represents a programming languages, with all its syntax rules and the styles assigned to them
    /// </summary>
    [DataContract(Name = "Language", Namespace = "")]
    public class Language
    {
        /// <summary>
        /// Stores the name of the programming language
        /// </summary>
        [DataMember()]
        private string name;

        /// <summary>
        /// The collection of the language elements
        /// </summary>
        [DataMember()]
        private List<LanguageElement> elements;
        
        /// <summary>
        /// Marks if the Language is case sensitive
        /// </summary>
        [DataMember()]
        private bool caseSensitive;

        /// <summary>
        /// Marks if the the Master Regex representing the sum of the sintactic rules is invalid
        /// </summary>
        /// <remarks>
        /// If the value is true, then you have to fix the incorrect regular expressions of its elements, and then Initalize the language
        /// </remarks>
        [DataMember()]
        private bool invalidRegex;

        /// <summary>
        /// Stores the sum of the syntactic rules that describe the programming language
        /// </summary>
        private Regex masterRegex;

        /// <summary>
        /// Initializes a new instance of the Language class.
        /// </summary>
        /// <remarks>
        /// Required for the deserialization by the DataContract
        /// </remarks>
        public Language()
        {
            this.elements = new List<LanguageElement>();
        }

        /// <summary>
        /// Initializes a new instance of the Language class with the assigned name
        /// </summary>
        /// <param name="name">The name of the programming language</param>
        public Language(string name)
        {
            this.name = name;
            this.caseSensitive = true;
            this.elements = new List<LanguageElement>();
        }

        /// <summary>
        /// Gets or sets the name of the programming language
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets the collection of the language elements that form the programming language
        /// </summary>
        public List<LanguageElement> Elements
        {
            get { return this.elements; }
        }

        /// <summary>
        /// Gets the regex that represents the sum of the syntactic rules that describe the programming language
        /// </summary>
        public Regex MasterRegex
        {
            get 
            {
                if (this.invalidRegex)
                    return new Regex("");
                else
                    return this.masterRegex; 
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the programming language is case sensitive
        /// </summary>
        public bool CaseSensitive
        {
            get { return this.caseSensitive; }
            set { this.caseSensitive = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the regex representing the syntactic rules of the language is invalid or not. 
        /// If yes, the language can't be used in its current form.
        /// </summary>
        public bool InvalidRegex
        {
            get { return this.invalidRegex; }
        }

        /// <summary>
        /// Initalizes the language object by constructing the master regex representing the sum of 
        /// syntactic rules of the language from the regexes of all its elements.
        /// If the resulting regular expression contains any errors the invalidRegex flag is set to true
        /// </summary>
        /// <remarks>
        /// This method should be called when the definition (regular expressions) of any of its elements changes.
        /// </remarks>
        public void Initialize()
        {
            this.invalidRegex = false;
            StringBuilder sb = new StringBuilder("");

            foreach (LanguageElement element in this.elements)
            {
                element.Initialize();
                if (!String.IsNullOrWhiteSpace(element.MasterRegex)) 
                    sb.Append(@"(?<" + element.Name + ">" + element.MasterRegex + ")|");
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1); // Removes the unnecessary '|' sign from the end

            // The regex uses the Singleline setting, this makes it easier to handle elements that span several lines (comments for example)
            RegexOptions regexoptions = RegexOptions.Singleline | (this.caseSensitive ? 0 : RegexOptions.IgnoreCase);
            try
            {
                this.masterRegex = new Regex(sb.ToString(), regexoptions);
            }
            catch (Exception e)
            {
                this.masterRegex = new Regex("", regexoptions);
                this.invalidRegex = true;
            }
        }
    }
}
