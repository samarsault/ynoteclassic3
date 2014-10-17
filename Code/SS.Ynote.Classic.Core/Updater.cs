using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace SS.Ynote.Classic.Core
{
    /// <summary>
    /// Update Manager
    /// </summary>
    public static class Updater
    {
        /// <summary>
        /// Check for Updates
        /// </summary>
        /// <returns></returns>
        public static void CheckForUpdates(double currentVerion)
        {
            string updateUrl = "https://raw.githubusercontent.com/samarjeet27/ynoteclassic/master/Update.json";
            string tempFile = Path.GetTempFileName();
            var downloader = new Downloader(updateUrl, tempFile);
            downloader.BeginDownload();
            downloader.DownloadComplete += (sender, args) => IsUpdateAvailable(tempFile,currentVerion);
        }
        /// <summary>
        /// Check if 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        static bool IsUpdateAvailable(string file, double version)
        {
            string json = File.ReadAllText(file);
            var update = JsonConvert.DeserializeObject<Update>(json);
            if (update.Version > version)
                return true;
            // remove garbage on user's file system
            File.Delete(file);
            return false;
        }
        /// <summary>
        /// Patch the Contents
        /// </summary>
        /// <param name="fileName"></param>
        private static void Patch(string fileName)
        {
            // run external patch process
            Process.Start("patch.exe", fileName);
        }
        /// <summary>
        /// Download And Install the Update
        /// </summary>
        /// <param name="update"></param>
        public static void Install(Update update)
        {
            string tempFile = Path.GetTempFileName();
            var download = new Downloader(update.InstallUrl, tempFile);
            download.BeginDownload();
            download.DownloadComplete += (sender, args) => Patch(tempFile);
        }
    }
    /// <summary>
    /// An Update Information
    /// </summary>
    public class Update
    {
        /// <summary>
        /// Version of the Update
        /// </summary>
        public double Version;
        /// <summary>
        /// Location of the Installer
        /// </summary>
        public string InstallUrl;
        /// <summary>
        /// ChangeLog of the Update
        /// </summary>
        public string Changelog;
    }
}
