using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Core.Syntax
{
    /// <summary>
    /// A Syntax Loaded from a File
    /// </summary>
    public class YnoteSyntax : ISyntax
    {
        private StyleFactory _styles;
        /// <summary>
        /// Folding Rules
        /// </summary>
        public List<FoldingRule> FoldingRules; 
        /// <summary>
        /// Syntax Rules
        /// </summary>
        public List<SyntaxRule> Rules;
        /// <summary>
        /// The File Types of the Syntax
        /// </summary>
        public string[] FileTypes { get; set; }
        /// <summary>
        /// The Comment Prefix
        /// </summary>
        public string CommentPrefix { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file">The YnoteSyntax File</param>
        /// <param name="styles">Style Factory</param>
        public YnoteSyntax(string file, StyleFactory styles)
        {
            Rules = new List<SyntaxRule>();
            FoldingRules = new List<FoldingRule>();
            _styles = styles;
            ParseFile(file);
        }
        private void ParseFile(string file)
        {
            using (var reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Key":
                                string name = reader["Name"];
                                if (name == "FileTypes")
                                    this.FileTypes = reader["Value"].Split(',');
                                if (name == "CommentPrefix")
                                    this.CommentPrefix = reader["Value"];
                                if (name == "SyntaxName")
                                    this.Name = reader["Value"];
                                break;
                            case "Rule":
                                var rule = new SyntaxRule();
                                if(reader["Options"] != null)
                                    rule.Options = (RegexOptions)Enum.Parse(typeof(RegexOptions), reader["Options"]);
                                rule.Style = _styles.GetStyle(reader["Style"]);
                                rule.Regex = reader["Regex"];
                                Rules.Add(rule);
                                break;
                            case "Folding":
                                var foldingRule = new FoldingRule();
                                foldingRule.FoldingStartMarker = reader["Start"];
                                foldingRule.FoldingEndMarker = reader["End"];
                                if (reader["Options"] != null)
                                    foldingRule.Options =
                                        (RegexOptions) Enum.Parse(typeof (RegexOptions), reader["Options"]);
                                FoldingRules.Add(foldingRule);
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Name of the Scope
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Highlight Syntax
        /// </summary>
        /// <param name="r"></param>
        public void HighlightSyntax(Range r)
        {
            // clear styles
            r.ClearStyle(StyleIndex.All);
            // apply styles
            foreach (var rule in Rules)
                r.SetStyle(rule.Style, rule.Regex, rule.Options);
            // add folding markers
            if (FoldingRules != null)
            {
                r.ClearFoldingMarkers();
                foreach (var foldingRule in FoldingRules)
                    r.SetFoldingMarkers(foldingRule.FoldingStartMarker, foldingRule.FoldingEndMarker, foldingRule.Options);
            }
        }

        /// <summary>
        /// Auto Indent
        /// </summary>
        /// <param name="args"></param>
        public void AutoIndent(AutoIndentEventArgs args)
        {
            // TODO:Implement
        }
    }
}
