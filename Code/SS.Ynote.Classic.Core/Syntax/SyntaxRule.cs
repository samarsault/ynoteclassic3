using FastColoredTextBoxNS;
using System.Text.RegularExpressions;

namespace SS.Ynote.Classic.Core.Syntax
{
    /// <summary>
    ///     Syntax Rule
    /// </summary>
    public sealed class SyntaxRule
    {
        /// <summary>
        ///     The Regex Options
        /// </summary>
        public RegexOptions Options;

        /// <summary>
        ///     Regex To Highlight
        /// </summary>
        public string Regex;

        /// <summary>
        ///     The Style of the Rule eg. -> Comment
        /// </summary>
        public Style Style;
    }

}