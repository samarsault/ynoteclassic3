using System.IO;
using FastColoredTextBoxNS;
using System.Collections.Generic;

namespace SS.Ynote.Classic.Core.Syntax
{
    /// <summary>
    /// Syntax Highlighter
    /// </summary>
    public class SyntaxHighlighter
    {
        public StyleFactory Styles;
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="styles"></param>
        public SyntaxHighlighter(StyleFactory styles)
        {
            Styles = styles;
            LoadSyntaxes();
        }
        /// <summary>
        /// List of Loaded Syntaxes
        /// </summary>
        public List<ISyntax> LoadedSyntaxes; 
        /// <summary>
        /// Load all Syntaxes
        /// </summary>
        private void LoadSyntaxes()
        {
            LoadedSyntaxes = new List<ISyntax>();
            string[] syntax_files = Directory.GetFiles("Syntaxes", "*.ynotesyntax");
            foreach (string syntax_file in syntax_files)
                LoadedSyntaxes.Add(new YnoteSyntax(syntax_file, Styles));
        }
        /// <summary>
        /// Highlight Syntax
        /// </summary>
        /// <param name="syntax">The Syntax</param>
        /// <param name="range">The Range to Highlight</param>
        public void HighlightSyntax(ISyntax syntax, Range range)
        {
            syntax.HighlightSyntax(range);
        }
        /// <summary>
        /// Highlight Syntax
        /// </summary>
        /// <param name="name">Name of the Syntax</param>
        /// <param name="range">The Range to Highlight</param>
        public void HighlightSyntax(string name, Range range)
        {
            foreach (var syntax in LoadedSyntaxes)
            {
                if (syntax.Name ==  name)
                {
                    HighlightSyntax(syntax, range);
                    break;
                }
            }
        }

        public ISyntax GetSyntax(string name)
        {
            foreach (ISyntax syntax in LoadedSyntaxes)
            {
                if (syntax.Name == name)
                    return syntax;
            }
            return null;
        }
    }
}
