using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Drawing;

namespace WordUtils.WordURLManager
{
    /// <summary>
    /// URL availability checking algorithm, including all the code that uses HTTP requests.
    /// It's a stateless class, the algorithm is contained in the static methods.
    /// </summary>
    class UrlTester
    {
        /// <summary>
        /// Checks if there is internet connection available. I couldn't find a more elegant tool in .NET for this, so I just ping google...
        /// </summary>
        /// <returns>true if there is a working internet connection</returns>
        public static bool CheckForInternetConnection()
        {
            bool connectionExits = true;

            HttpWebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.google.com");
                request.Timeout = 5000;
                request.AllowAutoRedirect = true;
                request.MaximumAutomaticRedirections = 3;
                
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                connectionExits = false;
            }
            finally
            {
                if(response != null) response.Close();                
            }

            return connectionExits;
        }

        public interface UrlTestUser
        {
            void UrlTestCallback(int index, Color backColor, Color foreColor, string label);
        }

        private class RequestState
        {
            public HttpWebRequest request;
            public HttpWebResponse response;
            public int index;
            public bool connectionFailure;
            public UrlTestUser caller;
            public RequestState()
            {
                index = -1;
                request = null;
                response = null;
                connectionFailure = false;
                caller = null;
            }
        }

        // Internet Explorer 10 (If I don't use a user agent header, then a lot of webpages just drop the request without answering
        private static string user_agent = @"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/5.0)";

        public static HttpWebRequest TestUrl_Async(string url, int index, UrlTestUser caller)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = url.Insert(0, "http://");

            RequestState requestState = new RequestState();
            requestState.index = index;
            requestState.caller = caller;
            HttpWebRequest request = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                System.Net.Cache.HttpRequestCachePolicy policy =
                    new System.Net.Cache.HttpRequestCachePolicy(System.Net.Cache.HttpRequestCacheLevel.BypassCache);
                request.CachePolicy = policy;
                request.UserAgent = user_agent;
                request.AllowAutoRedirect = false;
                requestState.request = request;

                IAsyncResult result = (IAsyncResult)request.BeginGetResponse(new AsyncCallback(TestUrl_AsyncCallback), requestState);
            }
            catch (WebException we)
            {
                requestState.connectionFailure = true;
                if (we.Status != WebExceptionStatus.RequestCanceled) // old requests that are invalid now shouldn't write into the ListView
                    ResolveUrlTestResult(requestState);
            }
            catch (Exception e)
            {
                requestState.connectionFailure = true;
                ResolveUrlTestResult(requestState);
            }
            return request;
        }

        private static void TestUrl_AsyncCallback(IAsyncResult asynchronousResult)
        {
            RequestState requestState = (RequestState)asynchronousResult.AsyncState;
            HttpWebRequest request = requestState.request;

            try
            {
                requestState.response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
                ResolveUrlTestResult(requestState);
            }
            catch (WebException we)
            {
                requestState.response = (HttpWebResponse)we.Response;
                if (we.Status != WebExceptionStatus.RequestCanceled) // old requests that are invalid now shouldn't write into the ListView
                    ResolveUrlTestResult(requestState);
            }
            catch (Exception e)
            {
                requestState.connectionFailure = true;
                ResolveUrlTestResult(requestState);
            }
            finally
            {
                if (requestState.response != null)
                    requestState.response.Close();
            }
        }

        private static void ResolveUrlTestResult(RequestState requestState)
        {
            Color backColor = Color.White;
            Color foreColor = Color.Black;
            string label = "Unkown";


            if (requestState.connectionFailure || requestState.response == null)
            {
                backColor = Color.Black;
                foreColor = Color.White;
                label = "Connection Error";
            }
            else if (requestState.response.StatusCode == HttpStatusCode.OK)
            {
                backColor = Color.Green;
                foreColor = Color.White;
                label = "OK";
            }
            else if (requestState.response.StatusCode == HttpStatusCode.NotFound)
            {
                backColor = Color.Red;
                foreColor = Color.White;
                label = "404";
            }
            else if (requestState.response.StatusCode == HttpStatusCode.Moved
                || requestState.response.StatusCode == HttpStatusCode.Found
                || requestState.response.StatusCode == HttpStatusCode.SeeOther)
            {
                backColor = Color.Yellow;
                foreColor = Color.Black;
                label = "Redirected";
            }
            else
            {
                int statusCode = (int)requestState.response.StatusCode;
                if (statusCode >= 400)
                {
                    backColor = Color.DarkRed;
                    foreColor = Color.White;
                    label = statusCode.ToString();
                }
                else if (statusCode >= 300)
                {
                    backColor = Color.Orange;
                    foreColor = Color.Black;
                    label = statusCode.ToString();
                }
                else if (statusCode >= 200)
                {
                    backColor = Color.DarkGreen;
                    foreColor = Color.White;
                    label = statusCode.ToString();
                }
                else if (statusCode >= 100)
                {
                    backColor = Color.LightSkyBlue;
                    foreColor = Color.Black;
                    label = statusCode.ToString();
                }
            }

            requestState.caller.UrlTestCallback(requestState.index, backColor, foreColor, label);
        }
    }
}
