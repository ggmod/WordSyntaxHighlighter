using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WordUtils.WordRegexSearch
{
    /// <summary>
    /// The parser of the expression written into the "replace with" input. It substitutes the special expressions created by the $ character with the results of the regex search.
    /// The parsing is done by a simple state machine.
    /// </summary>
    class ReplacementParser
    {
        private enum SubstitutionState { text, namedGroup, numberedGroup, escapedChar };

        public static string GetSubstitutedText(string text, Match previousFindResult)
        {
            if (previousFindResult == null || String.IsNullOrWhiteSpace(previousFindResult.Value))
                return text;

            StringBuilder result = new StringBuilder();
            StringBuilder buffer = new StringBuilder();

            SubstitutionState state = SubstitutionState.text;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (state == SubstitutionState.text)
                {
                    if (c == '$')
                    {
                        if (i + 1 < text.Length && text[i + 1] == '$')
                            state = SubstitutionState.escapedChar;
                        else if (i + 1 < text.Length && text[i + 1] == '{')
                            state = SubstitutionState.namedGroup;
                        else if (i + 1 < text.Length && Char.IsDigit(text[i + 1]))
                            state = SubstitutionState.numberedGroup;
                    }
                    else
                        result.Append(c);
                }
                else if (state == SubstitutionState.numberedGroup)
                {
                    if (!Char.IsDigit(c))
                    {
                        result.Append(GetGroupResult(buffer.ToString(), previousFindResult));

                        if (c == '$')
                        {
                            if (i + 1 < text.Length && text[i + 1] == '$')
                                state = SubstitutionState.escapedChar;
                            else if (i + 1 < text.Length && text[i + 1] == '{')
                                state = SubstitutionState.namedGroup;
                            else if (i + 1 < text.Length && Char.IsDigit(text[i + 1]))
                                state = SubstitutionState.numberedGroup;
                        }
                        else
                        {
                            state = SubstitutionState.text;
                            result.Append(c);
                        }
                    }
                    else
                    {
                        if (i > 0 && text[i - 1] == '$')
                            buffer.Clear();
                        buffer.Append(c);
                        if (i == text.Length - 1)
                            result.Append(GetGroupResult(buffer.ToString(), previousFindResult));
                    }
                }
                else if (state == SubstitutionState.namedGroup)
                {
                    if (c == '{')
                        buffer.Clear();
                    else if (c == '}')
                    {
                        state = SubstitutionState.text;
                        result.Append(GetGroupResult(buffer.ToString(), previousFindResult));
                    }
                    else
                        buffer.Append(c);
                }
                else if (state == SubstitutionState.escapedChar)
                {
                    result.Append(c);
                    state = SubstitutionState.text;
                }
            }

            return result.ToString();
        }

        private static string GetGroupResult(string buffer, Match previousFindResult)
        {
            bool number = false;
            int i = 0;
            while (i < buffer.Length && Char.IsDigit(buffer[i]))
                i++;
            if (i == buffer.Length)
                number = true;

            if (number)
            {
                try
                {
                    int n = Convert.ToInt32(buffer);
                    return previousFindResult.Groups[n].Value;
                }
                catch (Exception e) { return ""; }
            }
            else
            {
                try
                {
                    return previousFindResult.Groups[buffer].Value;
                }
                catch (Exception e) { return ""; }
            }
        }
    }
}
