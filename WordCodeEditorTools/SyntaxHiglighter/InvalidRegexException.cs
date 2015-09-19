using System;

namespace WordCodeEditorTools.SyntaxHighlighter
{
    [Serializable()]
    public class InvalidRegexException : Exception
    {
        private static string basic_massage = "The Regex definitions of the following programming language contain errors:\n";
        public InvalidRegexException() : base() { }
        public InvalidRegexException(string language) : base(basic_massage + language) { }
        public InvalidRegexException(string language, System.Exception inner) : base(basic_massage + language, inner) { }
        protected InvalidRegexException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
