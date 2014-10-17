using System;
using System.ComponentModel;
using System.Net;

namespace SS.Ynote.Classic.Core
{
    /// <summary>
    /// Simple File Downloading Utility
    /// </summary>
    public class Downloader
    {
        /// <summary>
        /// Occurs when Progress is Changed
        /// </summary>
        public event DownloadProgressChangedEventHandler ProgressChanged;
        /// <summary>
        /// Occurs when the Download is Completed
        /// </summary>
        public event AsyncCompletedEventHandler DownloadComplete;
        
        // Url of the File to be Downloaded
        private string url;
        // Output File
        private string outFile;
        // webClient
        private WebClient client;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">Url of the File</param>
        /// <param name="outFile">Output File</param>
        public Downloader(string url, string outFile)
        {
            this.url = url;
            this.outFile = outFile;
        }
        /// <summary>
        /// Begins the Download
        /// </summary>
        public void BeginDownload()
        {
            client = new WebClient();
            client.DownloadProgressChanged += ProgressChanged;
            client.DownloadFileCompleted += DownloadComplete;
            client.DownloadFileAsync(new Uri(url), outFile);
        }
        /// <summary>
        /// Cancels the Download
        /// </summary>
        public void CancelDownload()
        {
            client.CancelAsync();
        }
    }
}
