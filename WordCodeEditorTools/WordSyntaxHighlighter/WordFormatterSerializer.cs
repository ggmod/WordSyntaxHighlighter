using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WordCodeEditorTools.WordSyntaxHighlighter
{
    /// <summary>
    /// This class provides static methods to easily Serialize and Deserialize WordFormatter objects.
    /// A WordFormatter object contains the style settings used for pharagraph formatting, and these can be changed by the User.
    /// The serialized files use the XML format.
    /// </summary>
    class WordFormatterSerializer
    {
        /// <summary>
        /// Stores the content of the WordFormatter object (style settings used for pharagraph formatting) in an XML file
        /// </summary>
        /// <param name="filePath">The complete file path where the file will be located at</param>
        /// <param name="colorizer">The WordFormatter object to be serialized</param>
        public static void Serialize(string filePath, WordFormatter formatter)
        {
            FileStream file = null;
            XmlTextWriter writer = null;
            try
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(WordFormatter));
                file = File.Create(filePath);
                writer = new XmlTextWriter(file, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                serializer.WriteObject(writer, formatter);
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
        /// Loads the content of a WordFormatter object  (style settings used for pharagraph formatting) from an XML file.
        /// If the deserializatoin is unsuccessful, then a WordFormatter object with the default languages and styles is returned.
        /// In either case, the returned Formatter is still not initialized, as it has no reference to a Colorizer or to a Word Application.
        /// </summary>
        /// <param name="filePath">The complete file path where the file will be located at</param>
        /// <returns>The WordFormatter object filled with the contents of the file, but without initialization</returns>
        public static WordFormatter Deserialize(string filePath)
        {
            WordFormatter formatter;
            FileStream fs = null;
            XmlDictionaryReader reader = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open);
                reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                DataContractSerializer ser = new DataContractSerializer(typeof(WordFormatter));
                formatter = (WordFormatter)ser.ReadObject(reader, true);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Couldn't load the file at:\n"
                    + filePath
                    + "\n the language independent style will be set to default");
                formatter = new WordFormatter();
                formatter.SetToDefault();
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (fs != null)
                    fs.Close();
            }
            // The WordFormatter object is still not initialized here! (It has no reference to a Colorizer and Word Application)
            return formatter;
        }
    }
}
