using System;

namespace SS.Ynote.Classic.Core.Completions
{
    /// <summary>
    /// A Ynote Snippet
    /// </summary>
    public class YnoteSnippet
    {
        /// <summary>
        /// Tab Trigger of the Snippet
        /// </summary>
        public string TabTrigger { get; set; }
        /// <summary>
        /// Content of the Snippet
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Scope Name of the Snippet
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// Read a Snippet File
        /// </summary>
        public YnoteSnippet FromFile()
        {
            throw new NotImplementedException();
        }
    }
}
