using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WordUtils.WordRegexSearch
{
    /// <summary>
    /// It parses the regular expression using a simple state machine to determine the coloring.
    /// It is intentionally not a full-fledged regex parser, it only has the logic necessary for coloring the expressions.
    /// The coloring must happen in real-time, as the user is typing it.
    /// </summary>
    class RegexColorizer
    {
        // the states of the state machine
        private enum RegexState
        {
            text, parenthesisStart, backslash, squareBracketBlock, curlyBracketBlock
        }

        /// <summary>
        /// It receives a regular expression, and returns its coloring represented by an array, where the i-th element contains the color of the i-th character of the expression.
        /// </summary>
        /// <param name="regex">The regular expression to color</param>
        /// <returns>The coloring</returns>
        public static Color[] Colorize(string regex) 
        {
            Color[] colors = new Color[regex.Length];

            int buffer_cntr = 0;
            RegexState state = RegexState.text;
            RegexState previous = RegexState.text; // for the "backslash state" to know where it came from

            for (int i = 0; i < regex.Length; i++)
            {
                char c = regex[i];

                switch (state)
                {
                    case RegexState.text:
                        if (c == ')' || c == '|')
                            colors[i] = Color.OrangeRed;
                        else if (c == '?' || c == '+' || c == '*')
                            colors[i] = Color.Purple;
                        else if (c == '.' || c == '^' || c == '$')
                            colors[i] = Color.DarkRed;
                        else if (c == '\\')
                        {
                            previous = RegexState.text;
                            state = RegexState.backslash;
                            colors[i] = Color.Blue;
                        }
                        else if (c == '(')
                        {
                            if (i + 1 < regex.Length && regex[i + 1] == '?')
                                state = RegexState.parenthesisStart;
                            colors[i] = Color.OrangeRed;
                        }
                        else if (c == '[')
                        {
                            state = RegexState.squareBracketBlock;
                            colors[i] = Color.Green;
                        }
                        else if (c == '{')
                        {
                            state = RegexState.curlyBracketBlock;
                            colors[i] = Color.Purple;
                        }
                        else
                            colors[i] = Color.FromArgb(100, 100, 100);
                        break;

                    case RegexState.parenthesisStart:
                        colors[i] = Color.OrangeRed;
                        if (c == '!' || c == ':' || c == '#' || c == '=' || c == '>' ||c == ')')
                            state = RegexState.text;
                        break;

                    case RegexState.backslash: 
                        colors[i] = Color.Blue;
                        if (regex[i - 1] == '\\')
                        {
                            if (c != 'x' && c != 'c' && c != 'u' && c != 'p' && c != 'k' && !(Char.IsDigit(c) && i + 1 < regex.Length && Char.IsDigit(regex[i + 1])))
                                state = previous;
                            else
                            {
                                if (c == 'x') buffer_cntr = 2;
                                else if (c == 'u') buffer_cntr = 4;
                                else if (c == 'c') buffer_cntr = 1;
                                else if (Char.IsDigit(c))
                                {
                                    if (i + 2 < regex.Length && Char.IsDigit(regex[i + 2]))
                                        buffer_cntr = 2;
                                    else
                                        buffer_cntr = 1;
                                }
                            }
                        }
                        else
                        {
                            if (buffer_cntr > 1)
                                buffer_cntr--;
                            else if (buffer_cntr == 1)
                            {
                                buffer_cntr = 0;
                                state = previous;
                            }
                            else if (c == '}' || c == '>')
                                state = previous;                            
                        }
                        break;

                    case RegexState.squareBracketBlock:
                        if (c == '\\')
                        {
                            colors[i] = Color.Blue;
                            previous = RegexState.squareBracketBlock;
                            state = RegexState.backslash;
                        }
                        else if (c == ']')
                        {
                            colors[i] = Color.Green;
                            state = RegexState.text;
                        }
                        else if (c == '^' && regex[i-1] == '[' && (i < 2 || regex[i-2] != '\\'))
                        {
                            colors[i] = Color.Green;
                        }
                        else if (c == '-' && regex[i-1] != '[')
                        {
                            colors[i] = Color.Green;
                        }
                        else
                            colors[i] = Color.DarkGreen;
                        break;

                    case RegexState.curlyBracketBlock:
                        colors[i] = Color.Purple;
                        if (c == '}')
                            state = RegexState.text;
                        break;
                }
            }

            return colors;
        }
    }
}
