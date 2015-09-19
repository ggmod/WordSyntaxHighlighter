using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCodeEditorTools.CodeIndentation
{
    /// <summary>
    /// Represents and Indenting strategy, that is an algorithm which can calculate the indenting for a given source code 
    /// snippet written in a particular programming language.
    /// </summary>
    public interface IndentingStrategy
    {
        int[] CalculateNewIndentation(string text, int paragraph_count);
    }
}
