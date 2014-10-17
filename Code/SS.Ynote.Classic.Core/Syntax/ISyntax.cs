using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Core.Syntax
{
    /// <summary>
    /// Syntax Highlighting Helper interface
    /// </summary>
    public interface ISyntax
    {
        /// <summary>
        /// Name of the Syntax
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Comment Prefix
        /// </summary>
        string CommentPrefix { get; }
        /// <summary>
        /// File Types of the Syntax
        /// </summary>
        string[] FileTypes { get; set; }
        /// <summary>
        /// Highlight Syntax
        /// </summary>
        void HighlightSyntax(Range r);
        /// <summary>
        /// Auto indent
        /// </summary>
        void AutoIndent(AutoIndentEventArgs args);
    }
}
