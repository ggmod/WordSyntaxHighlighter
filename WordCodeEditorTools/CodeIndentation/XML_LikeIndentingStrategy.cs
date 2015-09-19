using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WordCodeEditorTools.CodeIndentation
{
    /// <summary>
    /// An algorithm that calculates indenting for a given XML-like source code snippet. The class contains a simple parser, 
    /// that processes XML-like code. The parser was designed to concentrate only on calculating indenting, so it doesn't bother with the syntax elements that don't influence that.
    /// The algorithm is capable of parsing most sloppy HTML code samples, not just well formatted XML files. 
    /// </summary>
    class XML_LikeIndentingStrategy : IndentingStrategy
    {
        public class XmlTag
        {
            public string name;
            public int start_tag_first_line;
            public int start_tag_last_line;
            public int end_tag_first_line;
            public int end_tag_last_line;
            public bool empty_element;
            public bool invalid;

            public XmlTag()
            {
                // default values are set to -1 
                start_tag_first_line = -1;
                start_tag_last_line = -1;
                end_tag_first_line = -1;
                end_tag_last_line = -1;
            }
        }

        // Enum for the state machine
        enum XmlStates
        {
            text, special_tag, end_tag, normal_tag_name, normal_tag_rest
        };

        public int[] CalculateNewIndentation(string text, int paragraph_count)
        {
            int[] indents = new int[paragraph_count];
            text = CleanInvalidAngledBrackets(text);
            List<XmlTag> tags = CreateListOfTags(text);
            CreateIndent(tags, indents);
            return indents;
        }

        /// <summary>
        /// A simple state machine that goes through the XML (or HTML) code, and builds a list of its Tags. The returned Tag objects
        /// contain all the informations necessary to calculate the indenting.
        /// </summary>
        /// <param name="text">The source code to be parsed</param>
        /// <returns>The list of the tags that the input source code contains</returns>
        private List<XmlTag> CreateListOfTags(string text)
        {
            List<XmlTag> tags = new List<XmlTag>();
            StringBuilder name = new StringBuilder();
            int first_line_of_tag = -1;

            int line_cntr = 0;
            XmlStates state = XmlStates.text;
            for (int i = 0; i < text.Length; i++)
            {
                switch (state)
                {
                    case XmlStates.text:
                        if (text[i] == '<')
                        {
                            if (i < text.Length - 1) 
                            {
                                if (text[i + 1] == '?' || text[i + 1] == '!')
                                {
                                    state = XmlStates.special_tag;
                                }
                                else if (text[i + 1] == '/')
                                {
                                    state = XmlStates.end_tag;
                                    name.Clear();
                                    first_line_of_tag = line_cntr;
                                }
                                else
                                {
                                    state = XmlStates.normal_tag_name;
                                    name.Clear();
                                    first_line_of_tag = line_cntr;
                                }
                            }
                        }
                        break;

                    case XmlStates.special_tag:
                        if (text[i] == '>')
                        {
                            state = XmlStates.text;
                        }
                        break;

                    case XmlStates.end_tag:
                        if (text[i] == '>')
                        {
                            state = XmlStates.text;

                            /* when the parsing of an end tag is finished, we have to find the latest starting tag with this name, that is not closed, and not an empty element */
                            string name_ = name.Remove(0, 1).ToString(); // removing '/' from the beginning of the name
                            bool has_beginning = false;
                            for (int i_list = tags.Count - 1; i_list >= 0; i_list--)
                            {
                                if (tags[i_list].name == name_ && tags[i_list].end_tag_first_line == -1 && !tags[i_list].empty_element)
                                {
                                    tags[i_list].end_tag_first_line = first_line_of_tag;
                                    tags[i_list].end_tag_last_line = line_cntr;
                                    has_beginning = true;
                                    break;
                                }
                                /* If I've just closed an opening element with this, then it can't be correctly closed later, and thus I have to mark it as invalid.
                                 (for example tipically: "<p> ... <br> </p> - <br>", or "<p>...<p>...") */
                                if (!tags[i_list].empty_element && tags[i_list].start_tag_first_line != -1 && tags[i_list].end_tag_first_line == -1)
                                {
                                    tags[i_list].invalid = true;
                                }
                            }
                            if (!has_beginning) // an end tag doesn't necessarily has a starting tag, it might be for example, that the second half of an XML file is parsed
                            {
                                XmlTag tag = new XmlTag();
                                tag.end_tag_first_line = first_line_of_tag;
                                tag.end_tag_last_line = line_cntr;
                                tag.name = name_;
                                tags.Add(tag);
                            }
                            name.Clear();
                        }
                        else if (!Char.IsWhiteSpace(text[i])) // e.g.: </name     > - it can still contain whitspaces at the end, but nothing else
                        {
                            name.Append(text[i]);
                        }
                        break;

                    case XmlStates.normal_tag_name:
                        if (text[i] == '>')
                        {
                            state = XmlStates.text;

                            XmlTag tag = new XmlTag();
                            if (text[i - 1] == '/')
                            {
                                tag.empty_element = true;
                                name = name.Remove(name.Length - 1, 1); // removing '/' from the end
                            }
                            else
                            {
                                tag.empty_element = false;
                            }
                            tag.start_tag_first_line = first_line_of_tag;
                            tag.start_tag_last_line = line_cntr;
                            tag.name = name.ToString();
                            name.Clear();
                            tags.Add(tag);
                        }
                        else if (Char.IsWhiteSpace(text[i]))
                        {
                            state = XmlStates.normal_tag_rest;
                        }
                        else
                        {
                            name.Append(text[i]);
                        }
                        break;

                    case XmlStates.normal_tag_rest:
                        if (text[i] == '>')
                        {
                            state = XmlStates.text;

                            XmlTag tag = new XmlTag();
                            if (text[i - 1] == '/')
                            {
                                tag.empty_element = true;
                            }
                            else
                            {
                                tag.empty_element = false;
                            }
                            tag.start_tag_first_line = first_line_of_tag;
                            tag.start_tag_last_line = line_cntr;
                            tag.name = name.ToString();
                            name.Clear();
                            tags.Add(tag);
                        }
                        break;

                }

                // this part runs regardless of which state it is in
                if (text[i] == '\r')
                {
                    line_cntr++;
                }
            }

            return tags;
        }

        /// <summary>
        /// Calculates the indent from the list of XML tags, that the parser produced from the source code
        /// </summary>
        /// <param name="tags">The list of Tags in the source code, where each tag contains which line it started and ended at</param>
        /// <param name="indents">The calculated indenting</param>
        private void CreateIndent(List<XmlTag> tags, int[] indents)
        {
            for (int i_tag = 0; i_tag < tags.Count; i_tag++)
            {
                // no indenting for "<...  />", "<br>" and the like, and also for "<x>no new line here</x>"
                if (!tags[i_tag].empty_element && !tags[i_tag].invalid && tags[i_tag].start_tag_last_line != tags[i_tag].end_tag_first_line)
                {
                    // it can handle the case, where there is no starting tag, for example when the second half of an XML file is parsed
                    int from, to;
                    if (tags[i_tag].start_tag_last_line != -1)
                        from = tags[i_tag].start_tag_last_line + 1;
                    else
                        from = 0;
                    if (tags[i_tag].end_tag_first_line != -1)
                        to = tags[i_tag].end_tag_first_line;
                    else
                        to = indents.Length;

                    // the indenting generated by the given Tag
                    for (int i_line = from; i_line < to; i_line++)
                    {
                        indents[i_line]++;
                    }

                }
            }
        }

        /// <summary>
        /// Removes the angled brackets from the text, that shouldn't be interpreted as XML syntax elements. 
        /// This could be handled by the parser, but that would create more than three additional states, and make the code much harder to understand.
        /// </summary>
        /// <param name="text">The original source code</param>
        /// <returns></returns>
        private string CleanInvalidAngledBrackets(string text)
        {
            /* Of course this part can cause rare mistakes, if someone uses a 'script' tag in an XML file, that is not related to the usual HTML tag.
             * But on the other hand it solves one of the most common problems that incorrectly written HTML codes pose. */
            string html_js = @"(?<=<script(?:\s[^>]*?)?>).*?(?=</script>)";
            RegexOptions regexoptions = RegexOptions.Singleline;
            Regex regex = new Regex(html_js, regexoptions);
            StringBuilder textb = new StringBuilder(text);
            foreach (Match match in regex.Matches(text))
            {
                for (int i = match.Index; i < match.Index + match.Length; i++)
                {
                    if (text[i] == '<' || text[i] == '>')
                        textb[i] = '-'; // it doesn't matter what character replaces it
                }
            }
            text = textb.ToString();

            //XML CDATA and comments containing '<' or '>'
            string xml_cdata = @"<\!\[[\w\s]*?\[.*?\]\]>";
            string xml_comment = @"<!--.*?-->";
            string xml_exceptions = "(" + xml_cdata + ")|(" + xml_comment + ")";
            regex = new Regex(xml_exceptions, regexoptions);
            textb = new StringBuilder(text);
            foreach (Match match in regex.Matches(text))
            {
                for (int i = match.Index + 1; i < match.Index + match.Length - 1; i++)
                {
                    if (text[i] == '<' || text[i] == '>')
                        textb[i] = '-'; 
                }
            }

            return textb.ToString();
        }

    }
}
