using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using WordUtils.WordURLManager;
using System.Net;
using System.Threading;

namespace WordUtils.WordURLManager
{
    /// <summary>
    /// Windows Forms dialog for displaying the URLs of a Word document, and their availability.
    /// </summary>
    public partial class UrlListForm : Form, UrlTester.UrlTestUser
    {
        private string[] hyperlink_urls;
        private HttpWebRequest[] hyperlink_requests;

        private string[] autofind_urls;
        private HttpWebRequest[] autofind_requests;

        // these are just references to one of the previous groups
        private volatile string[] active_urls;
        private volatile HttpWebRequest[] active_requests;

        private volatile Thread urlTesterThread;
        private volatile Thread RestartTestingThread;
        private volatile bool urlTesterThread_shouldStop;

        Word.Document wordDoc;

        public UrlListForm(Word.Document doc)
        {
            InitializeComponent();            

            if (doc == null)
                return;
            wordDoc = doc;

            hyperlink_urls = WordUrlManager.GetListOfHyperlinkUrls(doc);
            hyperlink_requests = new HttpWebRequest[hyperlink_urls.Length];
            autofind_urls = WordUrlManager.GetListOfAllUrls(doc);
            autofind_requests = new HttpWebRequest[autofind_urls.Length];
            active_urls = hyperlink_urls;
            active_requests = hyperlink_requests;

            InitializeUrlViewList();        

            progressBar.Visible = true;
            if (active_urls.Length == 0)
                progressBar.Visible = false;
            progressBar.Minimum = 0;
            progressBar.Maximum = active_urls.Length;
            progressBar.Step = 1;
            progressBar.Value = 0;

            button_RecheckEveryURL.Enabled = false;
            radioButton_autofindUrls.Enabled = false;
            radioButton_wordHyperlinks.Enabled = false;
            listView.BeginUpdate();
            
            urlTesterThread = null;
            RestartTestingThread = null;
            urlTesterThread_shouldStop = false;
        }

        private void UrlListForm_Shown(object sender, EventArgs e)
        {
            RestartTestingThread = new Thread(CheckUrlsForTheFirstTime);
            RestartTestingThread.Start();
        }

        //sub-method of RestartTestingThread
        private void CheckUrlsForTheFirstTime()
        {
            if (!CanConnectToInternet())
            {
                this.Close();
                return;
            }

            urlTesterThread = new Thread(VerifyURLs);
            urlTesterThread.Start();

            Invoke(new ReEnableControlsDelegate(ReEnableControls));
        }

        //sub-method of urlTesterThread
        private void VerifyURLs()
        {
            for(int i = 0; i < active_urls.Length; i++)
            {
                if (urlTesterThread_shouldStop)
                    break;

                active_requests[i] = UrlTester.TestUrl_Async(active_urls[i], i, this);                
            }
        }

        private bool CanConnectToInternet()
        {
            string message = "No Internet connection found, the program is not able to verify the URLs.";
            string caption = "No Internet connection found!";
            MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;

            while (!UrlTester.CheckForInternetConnection())
            {
                DialogResult result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.Cancel)
                    return false;
            }
            return true;
        }

        // called by both the constructor and Restart
        private void InitializeUrlViewList()
        {
            listView.Items.Clear();
            for (int i = 0; i < active_urls.Length; i++)
            {
                string url = active_urls[i];
                if (url.Length >= 259)
                    url = url.Substring(0, 258);
                ListViewItem item = new ListViewItem(url);
                item.SubItems.Add("Checking...");
                item.BackColor = Color.White;
                item.ForeColor = Color.Black;
                item.UseItemStyleForSubItems = false;
                item.ToolTipText = active_urls[i]; // not the 258 characger trimmed version

                listView.Items.Add(item);
            }

            progressBar.Value = 0;
            progressBar.Maximum = active_requests.Length;
            progressBar.Visible = true;
        }

        private delegate void ClearListViewDelegate();

        private void StopAllBackgroundThreads()
        {
            try
            {
                // first I stop the thread producing the HTTP requests (the HTTP requests themselves are also threads!)
                if (urlTesterThread != null && urlTesterThread.IsAlive)
                {
                    urlTesterThread_shouldStop = true;
                    urlTesterThread.Join();
                    urlTesterThread_shouldStop = false;
                }

                // Then I stop every HTTP request thread produced by the previously stopped thread.
                for (int i = 0; i < active_requests.Length; i++)
                {
                    if (active_requests[i] != null)
                    {
                        active_requests[i].Abort();
                    }
                    active_requests[i] = null;
                }
            }
            catch (Exception e) { }
        }

        private object lockObject = new object();
        private void RecheckUrls(object wordHyperlinks)
        {
            try // This method can still be running after the window closed
            {
                lock (lockObject)
                {

                    StopAllBackgroundThreads();

                    bool wordHyperlinks_bool = (bool)wordHyperlinks;
                    if (wordHyperlinks_bool)
                    {
                        hyperlink_urls = WordUrlManager.GetListOfHyperlinkUrls(wordDoc);
                        hyperlink_requests = new HttpWebRequest[hyperlink_urls.Length];
                        active_urls = hyperlink_urls;
                        active_requests = hyperlink_requests;
                    }
                    else
                    {
                        autofind_urls = WordUrlManager.GetListOfAllUrls(wordDoc);
                        autofind_requests = new HttpWebRequest[autofind_urls.Length];
                        active_urls = autofind_urls;
                        active_requests = autofind_requests;
                    }

                    Invoke(new ClearListViewDelegate(InitializeUrlViewList));

                    urlTesterThread = new Thread(VerifyURLs);
                    urlTesterThread.Start();

                    Invoke(new ReEnableControlsDelegate(ReEnableControls));
                }
            } catch (Exception e) { }            
        }

        private delegate void ReEnableControlsDelegate();

        private void ReEnableControls()
        {
            button_RecheckEveryURL.Enabled = true;
            radioButton_autofindUrls.Enabled = true;
            radioButton_wordHyperlinks.Enabled = true;
            listView.EndUpdate();
        }

        private void RestartTestingProcess()
        {
            button_RecheckEveryURL.Enabled = false;
            radioButton_autofindUrls.Enabled = false;
            radioButton_wordHyperlinks.Enabled = false;
            listView.BeginUpdate();

            RestartTestingThread = new Thread(RecheckUrls);
            RestartTestingThread.Start(radioButton_wordHyperlinks.Checked);
        }

        private void button_RecheckEveryURL_Click(object sender, EventArgs e)
        {
            RestartTestingProcess();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RestartTestingProcess();
        }

        private void radioButton_autofindUrls_CheckedChanged(object sender, EventArgs e)
        {
            RestartTestingProcess();
        }

        private void ClosingThreadFunction()
        {
            try
            {
                RestartTestingThread.Join(); // I can't put it on the GUI thread, as that would produce a deadlock
                StopAllBackgroundThreads();
            }
            catch (Exception e) { }
        }

        private void UrlListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Thread t = new Thread(ClosingThreadFunction);
            t.Start();
        }

        // UrlTester callback -----------------

        delegate void UpdateListViewItemStatus_Delegate(int index, Color backColor, Color foreColor, string label);

        public void UrlTestCallback(int index, Color backColor, Color foreColor, string label)
        {
            UpdateListViewItemStatus_Delegate UpdateListViewItemStatus_delegate =
                new UpdateListViewItemStatus_Delegate(UpdateListViewItemStatus);
            try
            {
                Invoke(UpdateListViewItemStatus_delegate, index, backColor, foreColor, label);
            }
            catch (Exception e)
            {
            }
        }

        private void UpdateListViewItemStatus(int index, Color backColor, Color foreColor, string label)
        {
            listView.Items[index].SubItems[1].BackColor = backColor;
            listView.Items[index].SubItems[1].ForeColor = foreColor;
            listView.Items[index].SubItems[1].Text = label;

            progressBar.PerformStep();
            if (progressBar.Value == progressBar.Maximum)
                progressBar.Visible = false;
        }

        // Context Menu on the list

        private void listView_Hyperlinks_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem item = listView.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    item.Selected = true;

                    ContextMenu menu = new ContextMenu();
                    MenuItem copy = new MenuItem("Copy URL to Clipboard");
                    MenuItem open = new MenuItem("Open in Browser");
                    MenuItem find = new MenuItem("Find in Document");
                    copy.Click += new System.EventHandler(CopyUrlToClipboard);
                    open.Click += new System.EventHandler(OpenUrlInBrowser);
                    find.Click += new System.EventHandler(FindUrlInDocument);
                    menu.MenuItems.Add(copy);
                    menu.MenuItems.Add(open);
                    menu.MenuItems.Add(find);
                    menu.Show(listView, e.Location);
                }
            }
        }

        private void CopyUrlToClipboard(object sender, EventArgs e)
        {
            try
            {
                int index = listView.SelectedItems[0].Index;
                string url = active_urls[index];
                Clipboard.SetData(DataFormats.Text, (Object)url);
            }
            catch (Exception exception)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                string message = "The program wasn't able to access the Clipboard.";
                string caption = "Word URL Manager Warning";
                MessageBox.Show(message, caption, buttons);
            }
        }

        private void OpenUrlInBrowser(object sender, EventArgs e)
        {
            try
            {
                int index = listView.SelectedItems[0].Index;
                string url = active_urls[index];

                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                    url = url.Insert(0, "http://");
                System.Diagnostics.Process.Start(url);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                string message = "The program wasn't able to access the default browser.";
                string caption = "Word URL Manager Warning";
                MessageBox.Show(message, caption, buttons);
            }
            catch (Exception exception) { }
        }

        private void FindUrlInDocument(object sender, EventArgs e)
        {
            int index = listView.SelectedItems[0].Index;
            string url = active_urls[index];

            WordUrlManager.FindInDocument(wordDoc, url);
        }

        // UI extras: -----------------

        // this is actually the most elegant way of preventing the column widths from changing...
        private void listView_Urls_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView.Columns[e.ColumnIndex].Width;
        }

        // no CancelButton, so a manual exit is needed
        // requires a KeyPreview property to work
        private void UrlListForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

    }
}
