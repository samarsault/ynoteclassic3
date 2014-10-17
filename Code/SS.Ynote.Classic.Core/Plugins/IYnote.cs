using System.Collections.Generic;

namespace SS.Ynote.Classic.Core.Plugins
{
    /// <summary>
    /// IYnote Interface
    /// </summary>
    public interface IYnote
    {
        /// <summary>
        /// Create New File
        /// </summary>
        void New();
        /// <summary>
        /// Open File
        /// </summary>
        /// <param name="file">File Name</param>
        void Open(string file);
        /// <summary>
        /// Save
        /// </summary>
        void Save();
        /// <summary>
        /// Get A List of Opened Editors
        /// </summary>
        /// <returns></returns>
        IEnumerable<IEditor> GetEditors();
        /// <summary>
        /// Get the Active Editor
        /// </summary>
        /// <returns></returns>
        IEditor GetActiveEditor();
        /// <summary>
        /// Show the Project View
        /// </summary>
        void ShowProjectView(string dir = null);
        /// <summary>
        /// Expand a Variable
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        dynamic ExpandVariable(string variable);
    }
}
