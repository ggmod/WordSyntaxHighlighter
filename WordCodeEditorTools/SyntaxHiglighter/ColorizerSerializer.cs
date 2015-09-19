using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WordCodeEditorTools.SyntaxHighlighter
{
    /// <summary>
    /// This class provides static methods to easily Serialize and Deserialize Colorizer objects 
    /// that contain programming language definitions and the styles assigned to their elements.
    /// The serialized files use the XML format.
    /// </summary>
    public class ColorizerSerializer
    {
        /// <summary>
        /// Stores the content of the Colorizer object (language definitions and styles) in an XML file
        /// </summary>
        /// <param name="filePath">The complete file path where the file will be located at</param>
        /// <param name="colorizer">The Colorizer object to be serialized</param>
        public static void Serialize(string filePath, Colorizer colorizer)
        {
            XmlTextWriter writer = null;
            FileStream file = null;
            try
            {
                /* TODO: reference loops can still cause problems, like when a LanguageElement has a Sublanguage that is also stored by the Colorizer 
                   the object appears multiple times in the XML file, yet after deserialization .NET seems to recognize that they are the same */
                DataContractSerializer serializer = new DataContractSerializer(typeof(Colorizer), null, int.MaxValue, true, false, null);
                file = File.Create(filePath);
                writer = new XmlTextWriter(file, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                serializer.WriteObject(writer, colorizer);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Couldn't save the file: \n" + filePath);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                if (filePath != null)
                    file.Close();
            }
        }

        /// <summary>
        /// Loads the content of Colorizer object (language definitions and styles) from an XML file.
        /// If the deserializatoin is unsuccessful, then a Colorizer object with the default languages and styles is returned.
        /// </summary>
        /// <param name="filePath">The complete file path where the file will be located at</param>
        /// <returns>The Colorizer object filled with the contents of the file</returns>
        public static Colorizer Deserialize(string filePath)
        {
            Colorizer colorizer;
            FileStream fs = null;
            XmlDictionaryReader reader = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open);
                reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                DataContractSerializer ser = new DataContractSerializer(typeof(Colorizer));
                colorizer = (Colorizer)ser.ReadObject(reader, true);

                colorizer.Initialize();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Couldn't load the language database file at: \n"
                    + filePath
                    + "\nonly the predefined styles and languages will be avaliable.");
                colorizer = new Colorizer();
                colorizer.LoadPredefinedLanguages();
                colorizer.Initialize();
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (fs != null)
                    fs.Close();
            }
            return colorizer;
        }
    }
}
