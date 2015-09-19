using System;

namespace WordCodeEditorTools.SyntaxHighlighter
{
    [Serializable()]
    public class InvalidLanguageException : System.Exception
    {
        private static string basic_massage = "The Syntax Colorizer doesn't recognize the following programming language: ";
        public InvalidLanguageException() : base() { }
        public InvalidLanguageException(string language) : base(basic_massage + language) { }
        public InvalidLanguageException(string language, System.Exception inner) : base(basic_massage + language, inner) { }
        protected InvalidLanguageException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
