using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace WordCodeEditorTools.WordSyntaxHighlighter
{
    /// <summary>
    /// In this Form the User can change the style preferences related to the pharagraph formatting, the frame the code blcok is placed in.
    /// The preferences changed here are stored in the WordFormatter object, and the contents of that are serialized and loaded back at startup, 
    /// thus they remain intact even after the User close the Word application.
    /// </summary>
    public partial class FrameStyleForm : Form
    {
        /// <summary>
        /// The WordFormatter object that stores the style preferences that can be changed in this form. It uses these values to format parts of the Word Document. 
        /// </summary>
        private WordFormatter formatter;

        /// <summary>
        /// Creates an instance of the FrameStyleForm class. The input fields of the Form are initialized based on the formatter's existing settings.
        /// </summary>
        /// <param name="formatter_"></param>
        public FrameStyleForm(WordFormatter formatter_)
        {
            InitializeComponent();

            // fills the combobox with the avaliable fonts
            foreach (FontFamily font in FontFamily.Families)
            {
                comboBox_Font.Items.Add(font.Name);
            }

            formatter = formatter_;
            Initialize();
        }

        /// <summary>
        /// Initializes the values displayed in the Form based on the TextFormatter's content
        /// </summary>
        private void Initialize()
        {         
            BgColorButton.BackColor = formatter.FrameBgColor;
            AltColorButton.BackColor = formatter.FrameAlternatingColor;
            BorderColorButton.BackColor = formatter.FrameBorderColor;
            useAltLines_checkBox.Checked = formatter.FrameAlternatingLines;
            comboBox_Font.Text = formatter.FontName; // What if no such font exists anymore? Could cause a rare problem
            numericUpDown_Font.Value = (int)formatter.FontSize;
        }

        private void ColorButton_Click(Button button)
        {
            ColorDialog colorchooser = new ColorDialog();
            colorchooser.FullOpen = true;
            colorchooser.ShowHelp = true;
            colorchooser.Color = button.BackColor;
            if (colorchooser.ShowDialog() == DialogResult.OK)
            {
                button.BackColor = colorchooser.Color;
            }
            colorchooser.Dispose();
        }

        private void BgColorButton_Click(object sender, EventArgs e)
        {
            ColorButton_Click(BgColorButton);
        }

        private void AltColorButton_Click(object sender, EventArgs e)
        {
            ColorButton_Click(AltColorButton);
        }

        private void BorderColorButton_Click(object sender, EventArgs e)
        {
            ColorButton_Click(BorderColorButton);
        }

        /// <summary>
        /// Loads back the hardcoded default values for the style settings, so that the user can return to these whenever he or she wants.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Default_button_Click(object sender, EventArgs e)
        {
            WordFormatter temp = new WordFormatter();
            temp.SetToDefault();

            BgColorButton.BackColor = temp.FrameBgColor;
            AltColorButton.BackColor = temp.FrameAlternatingColor;
            BorderColorButton.BackColor = temp.FrameBorderColor;
            useAltLines_checkBox.Checked = temp.FrameAlternatingLines;
            comboBox_Font.Text = temp.FontName;
            numericUpDown_Font.Value = (int)temp.FontSize;
        }

        /* This method is called from outside, when this Form returns with the DialogResult OK */
        internal void OK()
        {
            formatter.FrameBgColor = BgColorButton.BackColor;
            formatter.FrameAlternatingLines = useAltLines_checkBox.Checked;
            formatter.FrameAlternatingColor = AltColorButton.BackColor;
            formatter.FrameBorderColor = BorderColorButton.BackColor;
            formatter.FontName = comboBox_Font.Text;
            formatter.FontSize = (float)numericUpDown_Font.Value;
        }
    }
}
