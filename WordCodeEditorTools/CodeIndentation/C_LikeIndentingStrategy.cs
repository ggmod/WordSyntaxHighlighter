using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace WordCodeEditorTools.CodeIndentation
{
    /// <summary>
    /// An algorithm that calculates indenting for a given source code snippet written in a typical "curly bracket" language.
    /// It can handle most of the C-like programming languages, and the algorithm can correctly interpret most of the common indenting styles when it processes the line wrappings.
    /// </summary>
    class C_LikeIndentingStrategy : IndentingStrategy
    {
        public int[] CalculateNewIndentation(string text, int paragraph_count)
        {
            int[] indents = new int[paragraph_count];
            text = RemoveCommentAndStringBlocks(text);
            CalculateIndentByCurlyBrackets(text, indents);
            
            string[] text2 = ConvertText(text);          
            if (text2.Length != indents.Length) // in this case some serious error occured, but I never encountered this
                return indents;

            IndentSwitchCase(text2, indents);
            IndentBlocklessStatements(text2, 0, text.Length - 1, indents);
            return indents;
        }        
        
        /// <summary>
        /// It makes it much easier to process the code for calculating indent, if the comment and string blocks, 
        /// whcih can contain most special characters, are removed first, thus not interfering with the other syntax elements of the code.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string RemoveCommentAndStringBlocks(string text)
        {
            string c_like_comment1 = @"//.*?(?=\r|\n|$)";
            string csharp_string1 = @"(?<!"")""([^""\\]|\\.)*""|'([^'\\]|\\.)*'";
            string csharp_string2 = @"@"".*?""|(?<="")"".*?""";
            string regexString = "(" + c_like_comment1 + ")|(" + csharp_string1 + ")|(" + csharp_string2 + ")";
            RegexOptions regexoptions = RegexOptions.Singleline;
            Regex regex = new Regex(regexString, regexoptions);
            text = regex.Replace(text, "");

            string c_like_comment2 = @"/\*.*?\*/"; // multiline comments can contain newlines, and those shouldn't be deleted
            regex = new Regex(c_like_comment2, regexoptions);
            StringBuilder textb = new StringBuilder(text);
            foreach (Match match in regex.Matches(text))
            {
                for (int i = match.Index; i < match.Index + match.Length; i++) 
                {
                    if (text[i] != '\r')
                        textb[i] = ' '; 
                }
            }
            text = textb.ToString();
            
            return text;
        }

        private void CalculateIndentByCurlyBrackets(string text, int[] indents)
        {
            using (StringReader reader = new StringReader(text))
            {
                int indent_currentline = 0;
                int indent_nextline = 0;

                string line;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    
                    if (line.Length > 0)
                    {
                        if (line[line.Length - 1] == '{')
                            indent_nextline++;

                        if (line[0] == '}')
                        {
                            indent_currentline--;
                            indent_nextline--;
                        }
                        else if (line[line.Length - 1] == '}')
                        {
                            bool valid_ending = true;
                            for (int j = line.Length - 1; j >= 0; j--)
                            {
                                if (line[j] == '{') 
                                    valid_ending = false;
                            }
                            if (valid_ending)
                            {
                                indent_nextline--;
                            }
                        }
                    }
                    
                    indents[i] = indent_currentline;

                    indent_currentline = indent_nextline; 
                    i++;
                }
            }
            // this handles the case, when there are more closing brackets than opening ones, for example if the second half of a source file is processed
            int min = indents.Min();
            if (min < 0)
            {
                for (int i = 0; i < indents.Length; i++)
                    indents[i] += -min;
            }

        }

        private void IndentSwitchCase(string[] text, int[] indents)
        {            
            bool inside_switch = false;
            int switch_end = 0;
            int switch_start = 0;

            for(int line_cntr = 0; line_cntr < text.Length; line_cntr++)
            {
                if (inside_switch && line_cntr == switch_end)
                {
                    inside_switch = false;
                }
                else if (inside_switch) 
                {
                    if (!(text[line_cntr].StartsWith("case ") || text[line_cntr].StartsWith("default:")
                        || (line_cntr == switch_start + 1 && text[line_cntr] == "{")
                        || (line_cntr == switch_end && text[line_cntr] == "}" )))
                        indents[line_cntr]++;
                }
                else
                {
                    if (text[line_cntr].StartsWith("switch ") || text[line_cntr].StartsWith("switch("))
                    {
                        inside_switch = true;
                        switch_start = line_cntr;
                        switch_end = line_cntr + NumberOfLinesInBlock(text, line_cntr) -1;
                    }
                }                    
            }             
        }

        private string[] ConvertText(string text_)
        {
            // the text doesn't have any comments in it at this point
            text_ = text_.Remove(text_.Length - 1); // there is always an unnecessary '\r' at the end of the string
            char[] separator = new char[] { '\r' };
            string[] text = text_.Split(separator, StringSplitOptions.None);
            for (int i = 0; i < text.Length; i++)
                text[i] = text[i].Trim();
            
            return text;
        }

        //------ Everyting after this point deals with the blockless statements, and mostly with nesting them in each other (e.g. if() if() ... )

        private void IndentBlocklessStatements(string[] text, int text_firstLine, int text_lastLine, int[] indents)
        {
            // length is the length of the subtree of the current statement (1 if it has no nested element)
            int line_cntr = text_firstLine;
            int length = 1;
            while (line_cntr < text_lastLine) // it's needless to do it for the last line
            {
                length = NumberOfLinesInSubtree_AndIndentOnTheFly(text, line_cntr, indents);
                line_cntr += length;
            }
        }

        enum Statements
        {
            if_, else_, other_, none
        };

        // The algorithm searches the tree of nested elements recursively, and indents them when it knows their height
        // (The previous function takes part in the recursion too, since if a tree ends with a "if {... \r ...}" block, then it has to search for the nested trees inside that block the same way.)
        private int NumberOfLinesInSubtree_AndIndentOnTheFly(string[] text, int line_cntr, int[] indents)
        {
            int length = 1; // this returns values only to "IndentBlocklessStatements"
            
            Statements statement = FirstStatementOfLine(text, line_cntr);
            if (statement != Statements.none)
            {
                if(statement != Statements.else_)
                    line_cntr = LastLineOfStatement(text, line_cntr);

                if (!IsBlocklessStatement(text, line_cntr)) 
                {
                    length = NumberOfLinesInBlock(text, line_cntr);
                                        
                    IndentBlocklessStatements(text, line_cntr + 1, line_cntr + length - 1, indents); 

                    if (statement == Statements.if_ && HasElseBranch(text, line_cntr + length))
                    {
                        length += NumberOfLinesInSubtree_AndIndentOnTheFly(text, line_cntr + length, indents);
                    }
                    else if (statement == Statements.if_ && HasElseBranchInPreviousLine(text, line_cntr + length))
                    {
                        length += NumberOfLinesInSubtree_AndIndentOnTheFly(text, line_cntr + length - 1, indents) - 1; //! -1
                    }
                }
                else 
                {
                    if(IsOneLineStatement(text, line_cntr)) //e.g. if(..) ..; in one line
                    {
                        length = 1; 

                        if (statement == Statements.if_ && HasElseBranch(text, line_cntr + 1))
                        {
                            length += NumberOfLinesInSubtree_AndIndentOnTheFly(text, line_cntr + 1, indents);
                        }
                    }
                    else if(IsOneLineNesting(text, line_cntr))
                    {
                        length = 2; 
                        indents[line_cntr + 1]++;

                        if (statement == Statements.if_ && HasElseBranch(text, line_cntr + 2))
                        {
                            length += NumberOfLinesInSubtree_AndIndentOnTheFly(text, line_cntr + 2, indents);
                        }
                        
                    }
                    else // if there are still nested elements in it
                    {
                        int subtree_length = NumberOfLinesInSubtree_AndIndentOnTheFly(text, line_cntr + 1, indents);
                        length = 1 + subtree_length;

                        if (statement == Statements.if_ && HasElseBranch(text, line_cntr + subtree_length + 1))
                        {
                            length += NumberOfLinesInSubtree_AndIndentOnTheFly(text, line_cntr + subtree_length + 1, indents);
                        }

                        // create indenting
                        for (int i = line_cntr + 1; i < line_cntr + 1 + subtree_length; i++)
                            indents[i]++;
                    }
                }
            }

            return length;
        }

        // Helper functions for processing the blockless statements

        private bool IsOneLineStatement(string[] text, int line)
        {
            int line_length = text[line].Length;
            if (line_length > 0)
                if (text[line][line_length - 1] == ';')
                    return true;
            return false;
        }

        private bool IsOneLineNesting(string[] text, int line)
        {
            if (line + 1 < text.Length && text[line + 1].Length > 0)
                if (text[line + 1][text[line + 1].Length - 1] == ';')
                    return true;
            return false;
        }

        private bool IsBlocklessStatement(string[] text, int line)
        {
            if(text[line][text[line].Length - 1] == '{' )
                return false;
            if (line < text.Length - 1 && text[line + 1].Length > 0 && text[line + 1][0] == '{')
                return false;
            return true;
        }

        private bool HasElseBranch(string[] text, int line) 
        {
            if (line < text.Length - 1)
            {
                if (text[line] == "else" ||
                    text[line].StartsWith("else ") ||
                    text[line].StartsWith("else{"))
                    return true;
            }            
            return false;            
        }

        private bool HasElseBranchInPreviousLine(string[] text, int line)
        {
            StringBuilder lineb = new StringBuilder(text[line-1]);
            lineb.Replace('{', ' ');
            lineb.Replace('}', ' ');
            string line2 = lineb.ToString().Trim();
            if (line2 == "else") // "} else ({)", but not "} else {..}
                return true;
            return false;
        }

        private Statements FirstStatementOfLine(string[] text, int line) 
        {
            if (line < text.Length - 1) // the recursive function returns '1' for the last line
            {
                // I can't just wrute '.StartsWith("for")', because a variable name (and thus a line) could start with 'for'
                if (text[line].StartsWith("for ") || text[line].StartsWith("for("))
                    return Statements.other_;
                if (text[line].StartsWith("foreach ") || text[line].StartsWith("foreach("))
                    return Statements.other_;
                if (text[line].StartsWith("while ") || text[line].StartsWith("while("))
                    return Statements.other_;
                if (text[line].StartsWith("do ") || text[line] == "do")
                    return Statements.other_;
                if (text[line].StartsWith("if ") || text[line].StartsWith("if("))
                    return Statements.if_;

                StringBuilder lineb = new StringBuilder(text[line]);
                lineb.Replace('{', ' ');
                lineb.Replace('}', ' ');
                string line2 = lineb.ToString().Trim();
                if (line2 == "else" || line2.StartsWith("else ")) // else if() (
                    return Statements.else_;
            }
            return Statements.none;
        }

        private int NumberOfLinesInBlock(string[] text, int line)
        {
            if (text.Length - 1 == line)
                return 1; // if I'm standing on the last line, there is nothing to count

            int line_start = line;
            int i = 0;
            if (text[line + 1][0] == '{')
                i = 1;
            line++;

            int bracket_depth = 1;
            while (bracket_depth != 0)
            {
                while (text[line].Length == i)
                {
                    i = 0;
                    line++;
                    if (text.Length - 1 == line) 
                        return 1 + (line - line_start);
                }
                if (text[line][i] == '{') bracket_depth++;
                else if (text[line][i] == '}') bracket_depth--;

                i++;
            }

            return 1 + (line - line_start); // the line of the statement counts to, that's way there is a "1 +"
        }

        private int LastLineOfStatement(string[] text, int line)
        {
            int i = 0;
            while(i < text[line].Length && text[line][i] != '(')
                i++;
            if (i == text[line].Length) //if there was no '(' in the line, e.g. "do" statement
                return line;


            i++; // skips the current '(' character it's standing on, so as not to count it again
            int bracket_depth = 1; 
            while (bracket_depth != 0)
            {
                while (text[line].Length == i)
                {
                    i = 0;
                    line++;
                    if (text.Length - 1 == line) 
                        return line;
                }
                if (text[line][i] == '(') bracket_depth++;
                else if (text[line][i] == ')') bracket_depth--;
                
                i++;
            }
            return line;
        }

    }
}
