using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WordCodeEditorTools.CodeIndentation
{
    /// <summary>
    /// The user can change the style and preferences of how the program creates the indenting of the selected source code. 
    /// </summary>
    public partial class IndentSettingsForm : Form
    {
        private WordIndentFixer indentFixer;

        public IndentSettingsForm(WordIndentFixer _indentFixer)
        {
            InitializeComponent();
            indentFixer = _indentFixer;

            Initialize();
        }

        private void Initialize()
        {
            numericUpDown_Spaces.Value = 4;
            checkBox_UseTabStops.Checked = indentFixer.UseTabStops;
            numericUpDown_TabStopSize.Value = Convert.ToDecimal(indentFixer.IndentLength);
            checkBox_LineWrap.Checked = indentFixer.UseLineWrap;

            if (indentFixer.IndentUnit == '\t')
            {
                radioButton_Tab.Checked = true;
                numericUpDown_Spaces.Enabled = false;
                checkBox_UseTabStops.Enabled = true;
                numericUpDown_TabStopSize.Enabled = false;
                label_TabStopSize.Enabled = false;
                checkBox_LineWrap.Enabled = false;
                if (checkBox_UseTabStops.Checked)
                {
                    numericUpDown_TabStopSize.Enabled = true;
                    label_TabStopSize.Enabled = true;
                    checkBox_LineWrap.Enabled = true;
                }
            }
            else if (indentFixer.IndentUnit == ' ')
            {
                radioButton_Spaces.Checked = true;
                numericUpDown_Spaces.Enabled = true;
                checkBox_UseTabStops.Enabled = false;
                numericUpDown_TabStopSize.Enabled = false;
                label_TabStopSize.Enabled = false;
                checkBox_LineWrap.Enabled = false;

                numericUpDown_Spaces.Value = indentFixer.IndentRepeat;
            }
        }

        private void RefreshEnabling()
        {
            if (radioButton_Tab.Checked)
            {
                numericUpDown_Spaces.Enabled = false;
                checkBox_UseTabStops.Enabled = true;
                if (checkBox_UseTabStops.Checked)
                {
                    numericUpDown_TabStopSize.Enabled = true;
                    label_TabStopSize.Enabled = true;
                    checkBox_LineWrap.Enabled = true;
                }
                else
                {
                    numericUpDown_TabStopSize.Enabled = false;
                    label_TabStopSize.Enabled = false;
                    checkBox_LineWrap.Enabled = false;
                }
            }
            else if (radioButton_Spaces.Checked)
            {
                numericUpDown_Spaces.Enabled = true;
                checkBox_UseTabStops.Enabled = false;
                numericUpDown_TabStopSize.Enabled = false;
                checkBox_LineWrap.Enabled = false;
            }
        }

        private void radioButton_Tab_CheckedChanged(object sender, EventArgs e)
        {
            RefreshEnabling();
        }

        private void radioButton_Spaces_CheckedChanged(object sender, EventArgs e)
        {
            RefreshEnabling();   
        }

        private void checkBox_UseTabStops_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_UseTabStops.Checked)
            {
                numericUpDown_TabStopSize.Enabled = true;
                label_TabStopSize.Enabled = true;
                checkBox_LineWrap.Enabled = true;
            }
            else
            {
                numericUpDown_TabStopSize.Enabled = false;
                label_TabStopSize.Enabled = false;
                checkBox_LineWrap.Enabled = false;
            }
        }

        internal void OK()
        {
            if (radioButton_Tab.Checked)
            {
                indentFixer.IndentUnit = '\t';
                indentFixer.UseTabStops = checkBox_UseTabStops.Checked;
                if (checkBox_UseTabStops.Checked)
                {
                    indentFixer.IndentLength = (float)Convert.ToDouble(numericUpDown_TabStopSize.Value);
                    indentFixer.UseLineWrap = checkBox_LineWrap.Checked;
                }
            }
            else if (radioButton_Spaces.Checked)
            {
                indentFixer.IndentUnit = ' ';
                indentFixer.IndentRepeat = Convert.ToInt32(numericUpDown_Spaces.Value);
            }
        }

        private void IndentSettingsForm_Load(object sender, EventArgs e) { }
    }
}
