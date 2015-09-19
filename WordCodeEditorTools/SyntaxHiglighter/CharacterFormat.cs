using System.Runtime.Serialization;

namespace WordCodeEditorTools.SyntaxHighlighter
{

    /// <summary>
    /// Contains the usual formatting styles for a language element
    /// </summary>
    [DataContract(Name = "Colorizer", Namespace = "")]
    public class CharacterFormat
    {
        [DataMember()]
        public bool Bold;
        [DataMember()]
        public bool Italic;
        [DataMember()]
        public bool Underlined;
        [DataMember()]
        public bool Capitalletters;

        /// <summary>
        /// Initializes a new instance of the CharacterFormat class without any formatting styles assigned.
        /// </summary>
        public CharacterFormat() { }
    }
}
