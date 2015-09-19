using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using WordUtils.WordURLManager;
using System.Text.RegularExpressions;

namespace WordUtils.WordURLManager
{
    /// <summary>
    /// Contains the part of the URL manager module that deals with the Word API.
    /// This way the other parts of the program don't have a dependency on MS Word.
    /// It's a stateless class with only static methods.
    /// </summary>
    public class WordUrlManager
    {
        bool wordAppOptionsChanged;
        bool originalSettings_AsYouTypeReplaceHyperlinks;
        bool originalSettings_ReplaceHyperlinks;

        public WordUrlManager(Word.Application wordApp)
        {
            Word.Options options = wordApp.Application.Options;
            originalSettings_ReplaceHyperlinks = options.AutoFormatReplaceHyperlinks;
            originalSettings_AsYouTypeReplaceHyperlinks = options.AutoFormatAsYouTypeReplaceHyperlinks;
            wordAppOptionsChanged = false;
        }

        public void SwitchAutoHyperlinkDetection(Word.Application wordApp, bool state)
        {
            Word.Options options = wordApp.Application.Options;
            options.AutoFormatReplaceHyperlinks = state;
            options.AutoFormatAsYouTypeReplaceHyperlinks = state;

            wordAppOptionsChanged = true;
        }

        public void ResetOriginalSettings(Word.Application wordApp)
        {
            if (wordAppOptionsChanged) // don't override what the user sets in the Word settings GUI
            {
                Word.Options options = wordApp.Application.Options;
                options.AutoFormatReplaceHyperlinks = originalSettings_ReplaceHyperlinks;
                options.AutoFormatAsYouTypeReplaceHyperlinks = originalSettings_AsYouTypeReplaceHyperlinks;
            }
        }

        //----

        public static void FindInDocument(Word.Document doc, string url)
        {
            if(doc == null || String.IsNullOrWhiteSpace(url))
                return;
        
            Word.Range content = doc.Content;
            content.Find.ClearFormatting();
            content.Find.Forward = true;
            content.Find.Text = url;

            object missing = System.Type.Missing;
            bool success = content.Find.Execute(
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing);
            if(success)
                content.Select();
        }

        public static string[] GetListOfHyperlinkUrls(Word.Document doc)
        {
            if (doc == null)
                return null;

            List<string> urls = new List<string>(100);
            foreach (Word.Hyperlink link in doc.Hyperlinks)
            {
                string address;
                try
                {
                   address = link.Address;
                }
                catch (Exception e)
                {
                    address = null; 
                }
                /* This catch block might seem absurd, but it's because the Word API is: the text <tag>http://example.com</tag>
                   kills MS Word 2010. The link will include the closing tag, but then that causes an error and the COM object becomes invalid.
                   In the Word document the link appears in blue, but mouse hover doesn't show any target. */

                if(!String.IsNullOrWhiteSpace(address) && !String.IsNullOrWhiteSpace(link.TextToDisplay) &&
                   (address.StartsWith("http://") || address.StartsWith("https://")))
                        urls.Add(link.TextToDisplay);
            }
            return urls.Distinct().ToArray();
        }

        public static string[] GetListOfAllUrls(Word.Document doc)
        {
            if (doc == null)
                return null;

            string fulltext = doc.Content.Text; // this could be enormous, but processing it paragraph by pharagraph is too slow.
            return UrlDetection.FindAllUrls(fulltext);
        }


        public static void ConvertAllUrlsToHyperlinks(Word.Application wordApp)
        {
            if (wordApp.ActiveDocument == null)
                return;

            wordApp.ScreenUpdating = false;
            Word.UndoRecord ur = wordApp.UndoRecord;
            ur.StartCustomRecord("Convert all URLs to Hyperlinks");

            try
            {
                string fulltext = wordApp.ActiveDocument.Content.Text; // this could be enormous, but processing it paragraph by pharagraph is too slow.
                string[] urls = UrlDetection.FindAllUrls(fulltext);
                Array.Sort(urls, ComapreStringsByLength);

                object missing = System.Type.Missing;
                for (int i = 0; i < urls.Length; i++)
                {
                    Word.Range content = wordApp.ActiveDocument.Content;
                    content.Find.ClearFormatting();
                    content.Find.Forward = false; // true causes an infinite cycle, yet that's what the example uses on msdn
                    content.Find.Text = urls[i];

                    content.Find.Execute(
                        ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing);

                    while (content.Find.Found)
                    {
                        if (content.Hyperlinks.Count == 0)
                        {
                            string url = urls[i];
                            if(!url.StartsWith("http://") && !url.StartsWith("https://"))
                                url = url.Insert(0, "http://");

                            content.Hyperlinks.Add(content, url, null, url, missing, missing);
                        }

                        content.Find.Execute(
                            ref missing, ref missing, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing, ref missing);
                    }

                }
            }
            catch (Exception e) { }
            finally // The whole thing is only needed to make sure this runs (ScreenUpdate can get stuck)
            {
                wordApp.ScreenUpdating = true; 
                ur.EndCustomRecord();
            }
        }
        
        // helper function of ConvertAllUrlsToHyperlinks
        // The longest string comes first, and the shortest last.
        /* This is needed because for example with "[www.example.com 2, www.example.com/valamilink=www.example.com]"
         * the first link would be converted to a hyperlink twice as part of the second one, before converting the entire second link.
         * And removing existing hyperlinks with the Word API is surprusingly problematic */
        private static int ComapreStringsByLength(string s1, string s2)
        {
            if (s1 == null && s2 == null)
                return 0;
            if (s1 == null)
                return 1;
            if (s2 == null)
                return -1;
            return s2.Length.CompareTo(s1.Length);
        }

        
        public static void RemoveAllHyperlinks(Word.Application wordApp)
        {
            Word.Document doc = wordApp.ActiveDocument;
            if (wordApp.ActiveDocument == null || 
                wordApp.ActiveDocument.Hyperlinks == null || 
                wordApp.ActiveDocument.Hyperlinks.Count == 0)
                return;

            wordApp.ScreenUpdating = false;
            Word.UndoRecord ur = wordApp.UndoRecord;
            ur.StartCustomRecord("Remove all URL Hyperlinks");
            try
            {
                // It needs a copy of the list, because I can't remove anything from the original while iterating it
                Word.Hyperlink[] links = new Word.Hyperlink[doc.Hyperlinks.Count]; 
                for(int i = 0; i < doc.Hyperlinks.Count; i++)
                    links[i] = doc.Hyperlinks[i+1];

                for (int i = 0; i < links.Length; i++)
                {
                    string address;
                    try
                    {
                        address = links[i].Address;
                    }
                    catch (Exception e) // for this same reason as above
                    {
                        address = null;
                    }
                    if (!String.IsNullOrWhiteSpace(address) &&
                        (address.StartsWith("http://") || address.StartsWith("https://")))
                        links[i].Delete();
                }
            } 
            catch (Exception e) { }
            finally // The whole thing is only needed to make sure this runs (ScreenUpdate can get stuck)
            {
                wordApp.ScreenUpdating = true; 
                ur.EndCustomRecord();
            }

        }


    }
}
