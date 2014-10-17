using FastColoredTextBoxNS;
using SS.Ynote.Classic.Core.Settings;

namespace SS.Ynote.Classic.Core.Plugins
{
    public interface IEditor
    {
        /// <summary>
        /// The Name of the File Opened
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// The Current Configuration of the Editor
        /// </summary>
        YnoteSettings Settings { get; }
        /// <summary>
        /// The TextArea
        /// </summary>
        FastColoredTextBox Tb { get; }
    }
}
