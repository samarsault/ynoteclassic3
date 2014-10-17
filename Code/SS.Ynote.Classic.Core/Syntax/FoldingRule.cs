using System.Text.RegularExpressions;

namespace SS.Ynote.Classic.Core.Syntax
{
    /// <summary>
    ///     Folding Rule
    /// </summary>
    public sealed class FoldingRule
    {
        /// <summary>
        ///     The Folding End Marker
        /// </summary>
        public string FoldingEndMarker;

        /// <summary>
        ///     The Folding Start Marker
        /// </summary>
        public string FoldingStartMarker;

        /// <summary>
        ///     The RegexOptions
        /// </summary>
        public RegexOptions Options;
    }
}
