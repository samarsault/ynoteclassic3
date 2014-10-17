using System.IO;
using Newtonsoft.Json;
using SS.Ynote.Classic.Core.Settings;

namespace SS.Ynote.Classic.Core
{
    /// <summary>
    /// Structure of a Ynote Project
    /// </summary>
    public class YnoteProject
    {
        /// <summary>
        /// The Folders Included in the Project
        /// </summary>
        public FolderInfo[] Folders;

        /// <summary>
        /// Settings for the Project
        /// </summary>
        [JsonIgnore]
        public YnoteSettings Settings;

        /// <summary>
        /// Load the Project File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static YnoteProject Load(string file)
        {
            string json = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<YnoteProject>(json);
        }
    }
    /// <summary>
    /// A Project Folder Info
    /// </summary>
    public class FolderInfo
    {
        /// <summary>
        /// Excluded File Types
        /// </summary>
        public string[] ExcludedFileTypes { get; set; }
        /// <summary>
        /// Included File Types
        /// </summary>
        public string[] IncludedFileTypes { get; set; }
        /// <summary>
        /// The Path of the Folder
        /// </summary>
        public string Path { get; set; }
    }
}
