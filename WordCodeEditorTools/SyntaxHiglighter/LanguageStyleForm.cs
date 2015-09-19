using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WordCodeEditorTools.SyntaxHighlighter;

namespace WordCodeEditorTools
{
    /// <summary>
    /// In this form the User can edit the language definition data: what color and formatting to use for a language element, 
    /// the regex that identifies it in a text. The User can also create new languages or language elements, 
    /// and define its properties, or delete existing ones. All the present here can be exported or imported using XML files.
    /// </summary>
    public partial class LanguageStyleForm : Form
    {
        /// <summary>
        /// The edited language definition data is stored in a Colorizer object
        /// </summary>
        private Colorizer colorizer;

        private bool element_changed = false;
        private bool manual_change = true;
        private int selectedLanguage_pre;
        private int selectedElement_pre;

        // variables meant for the outside world, that can be used after the window is closed
        private bool languageListChanged = false;
        public bool LanguageListChanged
        {
            get { return languageListChanged; }
        }
        
        // the new Colorizer loaded by deserialization must be sent to the AddIn
        private bool colorizerReassigned;
        public bool ColorizerReassigned
        {
            get { return colorizerReassigned; }
        }
        public Colorizer Colorizer
        {
            get { return colorizer; }
        }

        public LanguageStyleForm(Colorizer colorizer_)
        {
            InitializeComponent();

            colorizer = colorizer_;
            InitializeLanguageList();
        }

        private void InitializeLanguageList()
        {
            // language list in the left listbox
            languageListBox.Items.Clear();
            comboBox_subLanguage.Items.Clear();
            foreach (string lang_name in colorizer.Languages.Keys)
            {
                languageListBox.Items.Add(lang_name);
                comboBox_subLanguage.Items.Add(lang_name);
            }
            comboBox_subLanguage.Items.Add("<None>");

            if (languageListBox.Items.Count > 0)
                languageListBox.SelectedIndex = 0;
        }

        // lists the elements of the chosen language in the language element listbox, and selects the first one
        private void languageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveLanguagePropertiesIfChanged(true);

            if (languageListBox.SelectedIndex >= 0)
            {

                //SaveCurrentElementIfChanged(false); // is this needed here?

                string selected = languageListBox.SelectedItem.ToString();
                Language selected_language = colorizer.Languages[selected];

                langelementListBox.BeginUpdate();
                langelementListBox.Items.Clear();
                foreach (LanguageElement element in selected_language.Elements)
                {
                    langelementListBox.Items.Add(element.Name);
                }

                int temp_index = langelementListBox.SelectedIndex;
                if (langelementListBox.Items.Count > 0)
                {
                    langelementListBox.SelectedIndex = 0;
                }
                else
                {
                    langelementListBox.SelectedIndex = -1;
                    this.langelementListBox_SelectedIndexChanged(sender, EventArgs.Empty); // why isn't it triggered by -1 ?

                }

                langelementListBox.EndUpdate();

                checkBox_casesensitive.Checked = selected_language.CaseSensitive;

                //selectedElement_pre = temp_index; 
            }
            else
            {
                langelementListBox.Items.Clear();
                langelementListBox.SelectedIndex = -1;
                this.langelementListBox_SelectedIndexChanged(sender, EventArgs.Empty); // why isn't it triggered by -1 ?

                checkBox_casesensitive.Checked = false;
            }

            selectedLanguage_pre = languageListBox.SelectedIndex;
        }

        private void langelementListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveCurrentElementIfChanged(true);
            manual_change = false;

            if (langelementListBox.SelectedIndex >= 0)
            {
                // I query the style of the selected element of the selected language, and I set the panel outputs based on them
                LanguageElement element = colorizer.Languages[languageListBox.SelectedItem.ToString()].Elements[langelementListBox.SelectedIndex];
                colorButton.BackColor = element.Color;
                checkBox_bold.Checked = element.CharFormat.Bold;
                checkBox_italic.Checked = element.CharFormat.Italic;
                checkBox_underlined.Checked = element.CharFormat.Underlined;
                checkBox_allcaps.Checked = element.CharFormat.Capitalletters;
                checkBox_keyword.Checked = element.Keywords;
                textBox_mainregex.Text = element.MainRegex;
                textBox_separatorbefore.Text = element.SeparatorRegex_start;
                textBox_separatorafter.Text = element.SeparatorRegex_end;
                comboBox_subLanguage.Enabled = true;
                if (element.SubLanguage != null) comboBox_subLanguage.SelectedItem = element.SubLanguage.Name;
                else comboBox_subLanguage.SelectedIndex = -1;
            }
            else // if there is no selected language element
            {
                colorButton.BackColor = Color.FromKnownColor(KnownColor.Control);
                checkBox_bold.Checked = false;
                checkBox_italic.Checked = false;
                checkBox_underlined.Checked = false;
                checkBox_allcaps.Checked = false;
                checkBox_keyword.Checked = false;
                textBox_mainregex.Text = "";
                textBox_separatorbefore.Text = "";
                textBox_separatorafter.Text = "";
                comboBox_subLanguage.SelectedIndex = -1;
                comboBox_subLanguage.Enabled = false;
            }

            manual_change = true;
            selectedElement_pre = langelementListBox.SelectedIndex;
        }

        // just flips a switch to change an element
        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorchooser = new ColorDialog();
            colorchooser.FullOpen = true;
            colorchooser.ShowHelp = true;
            colorchooser.Color = colorButton.BackColor;
            if (colorchooser.ShowDialog() == DialogResult.OK)
            {
                colorButton.BackColor = colorchooser.Color;
                ElementChanged();
            }
            colorchooser.Dispose();
        }
        private void checkBox_bold_CheckedChanged(object sender, EventArgs e) { ElementChanged(); }
        private void checkBox_italic_CheckedChanged(object sender, EventArgs e) { ElementChanged(); }
        private void checkBox_underlined_CheckedChanged(object sender, EventArgs e) { ElementChanged(); }
        private void checkBox_allcaps_CheckedChanged(object sender, EventArgs e) { ElementChanged(); }
        private void textBox_separatorbefore_TextChanged(object sender, EventArgs e) { ElementChanged(); }
        private void checkBox_keyword_CheckedChanged(object sender, EventArgs e) { ElementChanged(); }
        private void textBox_mainregex_TextChanged(object sender, EventArgs e) { ElementChanged(); }
        private void textBox_separatorafter_TextChanged(object sender, EventArgs e) { ElementChanged(); }
        private void comboBox_subLanguage_SelectedIndexChanged(object sender, EventArgs e) { ElementChanged(); }

        private void ElementChanged() 
        {
            if (manual_change && languageListBox.SelectedIndex >= 0 && langelementListBox.SelectedIndex >= 0)
                element_changed = true;
        }

        private void button_SaveElement_Click(object sender, EventArgs e)
        {
            SaveCurrentElementIfChanged(false);
        }

        public void SaveCurrentElementIfChanged(bool use_dialog)
        {
            if (element_changed) // is a language_changed check needed?
            {
                if (selectedElement_pre >= 0 && selectedLanguage_pre >= 0)
                {
                    string name = languageListBox.Items[selectedLanguage_pre].ToString();
                    string element_name = colorizer.Languages[name].Elements[selectedElement_pre].Name;

                    if (use_dialog)
                    {
                        DialogResult result = MessageBox.Show("Do you want to save changes made to the following element: \n" + name + " - " + element_name + "?", "Syntax Highlighter", MessageBoxButtons.YesNo);
                        if (result != DialogResult.Yes)
                        {
                            element_changed = false;
                            return;
                        }
                    }

                    LanguageElement element = colorizer.Languages[name].Elements[selectedElement_pre];
                    element.Color = colorButton.BackColor;
                    element.CharFormat.Bold = checkBox_bold.Checked;
                    element.CharFormat.Italic = checkBox_italic.Checked;
                    element.CharFormat.Underlined = checkBox_underlined.Checked;
                    element.CharFormat.Capitalletters = checkBox_allcaps.Checked;
                    element.SeparatorRegex_start = textBox_separatorbefore.Text;
                    element.Keywords = checkBox_keyword.Checked;
                    element.MainRegex = textBox_mainregex.Text;
                    element.SeparatorRegex_end = textBox_separatorafter.Text;
                    if (comboBox_subLanguage.SelectedIndex >= 0)
                    {
                        if (comboBox_subLanguage.SelectedItem.Equals("<None>"))
                            element.SubLanguage = null;
                        else
                            element.SubLanguage = colorizer.Languages[comboBox_subLanguage.SelectedItem.ToString()];
                    }

                    Language language = colorizer.Languages[name];
                    language.Initialize();
                    if (language.InvalidRegex)
                    {
                        MessageBox.Show("The following element contains invalid regular expressions:\n" + name + " - " + element.Name, "Syntax Highlighter Error");
                    }
                }
                element_changed = false;
            }
        }

        public void SaveLanguagePropertiesIfChanged(bool use_dialog)
        {
            if (selectedLanguage_pre >= 0)
            {
                string name = languageListBox.Items[selectedLanguage_pre].ToString();
                if (colorizer.Languages[name].CaseSensitive != this.checkBox_casesensitive.Checked)
                {
                    if (use_dialog)
                    {
                        DialogResult result = MessageBox.Show("Do you want to save changes made to the following language: \n" + name + "?", "Syntax Highlighter", MessageBoxButtons.YesNo);
                        if (result != DialogResult.Yes)
                        {
                            element_changed = false;
                            return;
                        }
                    }

                    colorizer.Languages[name].CaseSensitive = this.checkBox_casesensitive.Checked;
                    colorizer.Languages[name].Initialize();
                }
            }
        }

        // Change whole language database

        private void button_LoadFile_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save the current language database, before you load the new one?", "Syntax Highlighter", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                button_SaveFile_Click(sender, EventArgs.Empty);
            }
            else if (result != DialogResult.Cancel)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = @"c:\";
                openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    colorizer = ColorizerSerializer.Deserialize(openFileDialog.FileName);
                    this.InitializeLanguageList();
                    languageListChanged = true;
                    colorizerReassigned = true;
                }
            }
        }

        private void button_SaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = @"c:\";
            saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ColorizerSerializer.Serialize(saveFileDialog.FileName, colorizer);
            }
        }

        private void button_LoadDefault_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save the current language database, before you load the new one?", "Syntax Highlighter", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                button_SaveFile_Click(sender, EventArgs.Empty);
            }
            else if (result != DialogResult.Cancel)
            {
                colorizer.LoadPredefinedLanguages();
                this.InitializeLanguageList();
                languageListChanged = true;
            }
        }

        // Add

        private void button_Addlanguage_Click(object sender, EventArgs e)
        {
            string name = textBox_AddLanguage.Text;
            if (name.Length > 0)
            {
                try
                {
                    // model
                    colorizer.Languages.Add(name, new Language());
                    colorizer.Languages[name].Name = name;
                    colorizer.Languages[name].Initialize();
                    languageListChanged = true;
                    // gui
                    int index = languageListBox.Items.Add(name);
                    languageListBox.SelectedIndex = index;
                    comboBox_subLanguage.Items.Add(name);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Couldn't add a language with the name \"" + name + "\"\n"
                        + "Are you sure a language with that name doesn't exist yet?", "Syntax Highlighter");
                }
            }
        }

        private void textBox_AddLanguage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button_Addlanguage.PerformClick();
                e.Handled = true; // avoid the beep
            }
        }

        private void button_AddElement_Click(object sender, EventArgs e)
        {
            string name = textBox_AddElement.Text;
            if (name.Length > 0)
            {
                try
                {
                    // model
                    string lang = languageListBox.SelectedItem.ToString();
                    LanguageElement element = new LanguageElement();
                    element.Name = name;
                    colorizer.Languages[lang].Elements.Add(element);
                    colorizer.Languages[lang].Initialize();
                    // here
                    int index = langelementListBox.Items.Add(name);
                    langelementListBox.SelectedIndex = index;
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Couldn't add the element: " + name, "Syntax Highlighter");
                }
            }
        }

        private void textBox_AddElement_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button_AddElement.PerformClick();
                e.Handled = true; // avoid the beep
            }
        }

        // Remove

        private void languageListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = languageListBox.IndexFromPoint(e.Location);
                if (index >= 0 && languageListBox.SelectedIndex != index)
                {
                    languageListBox.SelectedIndex = index;
                }
            }
        }

        private void languageListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = languageListBox.IndexFromPoint(e.Location);
                if (index >= 0 && languageListBox.SelectedIndex == index)
                {
                    ContextMenu menu = new ContextMenu();
                    MenuItem remove = new MenuItem("Remove");
                    remove.Visible = true;
                    menu.MenuItems.Add(remove);
                    menu.Show(languageListBox, e.Location);

                    remove.Click += new System.EventHandler(this.RemoveLanguage);
                }

            }
        }

        private void RemoveLanguage(object sender, EventArgs e)
        {
            string name = languageListBox.SelectedItem.ToString();
            DialogResult result = MessageBox.Show("Are you sure you want to remove the following language:\n" + name + " ?", "Syntax Highlighter", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                bool sublanguage = false;
                foreach (KeyValuePair<string, Language> language in colorizer.Languages)
                    foreach (LanguageElement element in language.Value.Elements)
                        if (element.SubLanguage != null && element.SubLanguage.Name.Equals(name))
                            sublanguage = true;
                if (sublanguage)
                {
                    MessageBox.Show("The following language cannot be removed:\n" + name +
                        "\nbecause it is used as a sublanguage by another language. You have to remove that reference first", "Syntax Highlighter Error");
                    return;
                }

                // model then output
                colorizer.Languages.Remove(name);
                selectedLanguage_pre = -1;
                languageListBox.Items.Remove(languageListBox.SelectedItem);
                comboBox_subLanguage.Items.Remove(name);

                languageListChanged = true;
            }
        }


        private void langelementListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = langelementListBox.IndexFromPoint(e.Location);
                if (index >= 0 && langelementListBox.SelectedIndex != index)
                {
                    langelementListBox.SelectedIndex = index;
                }
            }
        }

        private void langelementListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = langelementListBox.IndexFromPoint(e.Location);
                if (index >= 0 && langelementListBox.SelectedIndex == index)
                {
                    ContextMenu menu = new ContextMenu();
                    MenuItem remove = new MenuItem("Remove");
                    remove.Visible = true;
                    menu.MenuItems.Add(remove);
                    menu.Show(langelementListBox, e.Location);

                    remove.Click += new System.EventHandler(this.RemoveLanguageElement);
                }

            }
        }

        private void RemoveLanguageElement(object sender, EventArgs e)
        {
            int index = langelementListBox.SelectedIndex;
            // model then output
            colorizer.Languages[languageListBox.SelectedItem.ToString()].Elements.RemoveAt(index);
            colorizer.Languages[languageListBox.SelectedItem.ToString()].Initialize();
            selectedElement_pre = -1;
            langelementListBox.Items.Remove(langelementListBox.SelectedItem);
            langelementListBox_SelectedIndexChanged(sender, EventArgs.Empty);
        }

    }
}
