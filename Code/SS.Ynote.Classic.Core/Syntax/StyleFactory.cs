using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Core.Syntax
{
    /// <summary>
    /// Responsible for Styling Syntax Highlighting 
    /// </summary>
    public class StyleFactory
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="themeFile">The Theme File</param>
        public StyleFactory(string themeFile)
        {
            Styles = new Dictionary<string, Style>();
            Colors = new Dictionary<string, Color>();
            Read(themeFile);
        }
        /// <summary>
        /// Get a Style
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Style GetStyle(string name)
        {
            return Styles[name];
        }

        // The Styles
        private Dictionary<string, Style> Styles;
        // The Colours
        private Dictionary<string, Color> Colors;

        /// <summary>
        /// Reads a ynote theme
        /// NOTE : Structure may change if it reduces performance
        /// </summary>
        /// <param name="file">The File</param>
        /// <returns></returns>
        private void Read(string file)
        {
            using (var reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.Name == "Style")
                        {
                            Style style = GetStyle(reader);
                            Styles.Add(reader["Name"], style);
                        }
                        if (reader.Name == "Key")
                        {
                            Color c = ColorTranslator.FromHtml(reader["Value"]);
                            Colors.Add(reader["Name"], c);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Gets All the Styles
        /// </summary>
        /// <returns></returns>
        public Style[] GetAllStyles()
        {
            return Styles.Values.ToArray();
        }
        /// <summary>
        /// Get a Color from the theme
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Color GetColor(string name)
        {
            return Colors[name];
        }
        /// <summary>
        /// Get Style from Node
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Style GetStyle(XmlReader reader)
        {
            Brush foreground = new SolidBrush(ColorTranslator.FromHtml(reader["Foreground"]));
            Brush background = new SolidBrush(ColorTranslator.FromHtml(reader["Background"]));
            FontStyle fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), reader["Font"]);
            return new TextStyle(foreground, background, fontStyle);
        }
        /// <summary>
        /// Apply Styling to FastColoredTextBox
        /// </summary>
        /// <param name="tb"></param>
        public void ApplyStyling(FastColoredTextBox tb)
        {
            if (Colors.Count == 0) 
                return;
            tb.BackColor = Colors["Background"];
            tb.ForeColor = Colors["Foreground"];
            tb.SelectionColor = Colors["Selection"];
            tb.BookmarkColor = Colors["Bookmark"];
            tb.FoldingIndicatorColor = Colors["FoldingIndicator"];
            tb.LineNumberColor = Colors["LineNumber"];
            tb.ServiceLinesColor = Colors["LineNumberSeperator"];
            tb.IndentBackColor = Colors["LineNumberBackground"];
            tb.CaretColor = Colors["Caret"];
            tb.CurrentLineColor = Colors["CurrentLine"];
        }
    }
}
