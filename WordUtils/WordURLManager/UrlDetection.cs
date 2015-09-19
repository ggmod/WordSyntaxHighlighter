using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WordUtils.WordURLManager
{
    /// <summary>
    /// The URL detection algorithm. (It's placed in static functions and it doesn't use any state variables.)
    /// </summary>
    class UrlDetection
    {
        /// <summary>
        /// Finds all the URLs in a given text. In the returned list every url only appears once, regardless how many times it appeared in the text.
        /// </summary>
        /// <param name="fulltext">The input text to search for URLs</param>
        /// <returns>The list of URLs in the text</returns>
        public static string[] FindAllUrls(string fulltext)
        {
            List<string> urls = new List<string>();

            string regex_start = @"\b(http(s)?://)?";
            string regex_end = @".*?(?=\s|$)"; // I wanted to use @"(/.*?)?(?=\s|$)" but it couldn't find 'http://example.com,'
            string regex_hostname = @"([a-zA-z0-9\-]{1,63}\.){1,5}[a-zA-Z]{2,6}";  // I intentionally filter out urls with port numbers in them
            string regex_ipv4 = @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}";  // no IPv6 support yet
            string regexString = regex_start + "(?:(" + regex_hostname + ")|(" + regex_ipv4 + "))" + regex_end;

            RegexOptions regexoptions = RegexOptions.Singleline;
            Regex regex = new Regex(regexString, regexoptions);
            foreach (Match match in regex.Matches(fulltext))
            {
                char previous_char = ' ';
                if (match.Index != 0)
                    previous_char = fulltext[match.Index - 1];

                string url = CutUrlEndingIfSuspicious(match.Value, previous_char);

                if (IsValidHttpUrl(url, previous_char))
                    urls.Add(url);
            }

            return urls.Distinct().ToArray();
        }

        // sub-methods of FindAllUrls, no other public methods here:

        // Aside from the syntactic rules of URLs and hostnames I filter by custom criterias too, see the later comments.
        private static bool IsValidHttpUrl(string url, char previous_char)
        {
            // filter email addresses and urls using other protocols. (ftp://...)
            if (previous_char == '@' || previous_char == '/')
                return false;

            // If a domain name or IP address stands alone without http(s):// or a query string, then don't use it.
            // this is an intentional design decision: usually these are not meant to be hyperlinks, and finding these would produce a lot of false positives
            if (!url.StartsWith("http://") && !url.StartsWith("https://") && url.IndexOf("/") == -1)
                return false;

            string hostname = GetHostnameFromURL(url);

            if (hostname.Length > 255)
                return false;

            bool isIPAddress = true;
            for (int i = 0; i < hostname.Length; i++)
                if (!(Char.IsDigit(hostname[i]) || hostname[i] == '.'))
                    isIPAddress = false;
            if (isIPAddress)
                return true;

            if (!HasValidTopLevelDomain(hostname))
                return false;

            // in real hostnames there can't be a dash right after a dot
            for (int i = 1; i < hostname.Length - 1; i++)
                if (hostname[i] == '.' && (hostname[i - 1] == '-' || hostname[i + 1] == '-'))
                    return false;

            return true;
        }

        private static string[] TLDs = { "edu", "gov", "mil", "arpa", "aero", "asia", "biz", "cat", "com", "coop", 
                                    "info", "int", "jobs", "mobi", "museum", "name", "net", "org", "post",
                                    "pro", "tel", "travel", "xxx" };

        private static bool HasValidTopLevelDomain(string hostname)
        {
            int lastdot_index = hostname.LastIndexOf('.'); // it risk of -1
            string tld = hostname.Substring(lastdot_index + 1); // no risk of overindexing
            if (tld.Length == 2)
            {
                if (Char.IsLetter(tld[0]) && Char.IsLetter(tld[1]))
                    return true;
            }
            else if (tld.Length >= 3)
            {
                for (int j = 0; j < TLDs.Length; j++)
                    if (TLDs[j] == tld)
                        return true;
            }
            return false;
        }

        // only works for http(s) URLS
        private static string GetHostnameFromURL(string url)
        {
            string hostname;
            if (url.StartsWith("http://"))
                hostname = url.Substring(7);
            else if (url.StartsWith("https://"))
                hostname = url.Substring(8);
            else
                hostname = url;

            int slash_index = hostname.IndexOf('/');
            if (slash_index != -1)
                hostname = hostname.Substring(0, slash_index);

            return hostname;
        }

        /* Here comes the biggest problem of URL detection: it's NOT deterministic!! The query string can contain practically any characters, 
         * and it's not obvious for example, if the comma at the tend of "example.com/something," is part of the URL, or a sentence.
         * MS Word can't accurately determine this either, and the heuristics here are actually much smarter the MS Word's built-in solution */
        private static string CutUrlEndingIfSuspicious(string url, char previous_char)
        {
            // If it ends with '.' or ',', '?', '!', then I don't consider it part of the URL (Which is the right solution 95% of the time.)
            if (url[url.Length - 1] == '.' || url[url.Length - 1] == ',' || url[url.Length - 1] == '?' || url[url.Length - 1] == '!')
                url = url.Substring(0, url.Length - 1);

            // e.g. (blah blah example.com/something) - the closing parenthesis is not part of the URL
            if (url[url.Length - 1] == ')' && UrlEndsWithSuspiciousParenthesis(url))
                url = url.Substring(0, url.Length - 1);

            // e.g. <a href="http://example.com/test(3).html">parens</a>"
            int attrEnd_index = url.IndexOf("\">");
            if (attrEnd_index >= 0)
                url = url.Substring(0, attrEnd_index);

            // If it's between quote marks
            if (url[url.Length - 1] == '"' && previous_char == '"')
                url = url.Substring(0, url.Length - 1);
            else if (url[url.Length - 1] == '\'' && previous_char == '\'')
                url = url.Substring(0, url.Length - 1);
            else if (url[url.Length - 1] == ']' && previous_char == '[')
                url = url.Substring(0, url.Length - 1);
            else if (url[url.Length - 1] == '>')
            {
                //e.g. <tag attr="URL">
                if (url[url.Length - 2] == '"' || url[url.Length - 2] == '\'')
                    url = url.Substring(0, url.Length - 2);
                else if (previous_char == '<') // <URL>
                    url = url.Substring(0, url.Length - 1);
                else
                {
                    // e.g. <url>http://.../...</url> 
                    // this can't overindex here
                    int i = url.Length - 2;
                    while (Char.IsLetterOrDigit(url[i]))
                        i--;
                    if (url[i] == '/' && url[i - 1] == '<')
                        url = url.Substring(0, i - 1);
                }
            }

            return url;
        }

        /* It filters out the non-deterministic but surprisingly common case, where someone writes something like:
         * "(url: example.com/...()...)" and the last parenthesis is not part of the URL, even though it's written together
         * For this I assume that people won't write query strings with unmatched parens. */
        private static bool UrlEndsWithSuspiciousParenthesis(string url)
        {
            int i = 0;
            if (url.StartsWith("http://") || url.StartsWith("https://"))
                i = 8;

            int openBrackets_cntr = 0;
            for (; i < url.Length; i++)
            {
                if (url[i] == '(') openBrackets_cntr++;
                else if (url[i] == ')') openBrackets_cntr--;
            }
            if (openBrackets_cntr == -1)
                return true;
            return false;
        }
    }
}
