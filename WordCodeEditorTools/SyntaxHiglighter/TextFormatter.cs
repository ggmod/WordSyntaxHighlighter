using System.Drawing;

namespace WordCodeEditorTools.SyntaxHighlighter
{
    /// <summary>
    /// Defines a method to format a given part of a text with the styles specified
    /// </summary>
    public interface TextFormatter
    {
        /// <summary>
        /// Formats part of a text
        /// </summary>
        /// <param name="index">The starting position of the range where the formatting should be applied</param>
        /// <param name="length">The length of the range where the formatting should be applied</param>
        /// <param name="color">The text range should be colored by this</param>
        /// <param name="format">The text range should be formatted using these character styles</param>
        void ColorRangeOfText(int index, int length, Color color, CharacterFormat format); 
    }
}
