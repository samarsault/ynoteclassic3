using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using SS.Ynote.Classic.Core.Plugins;
using SS.Ynote.Classic.Core.Settings;
using SS.Ynote.Classic.Core.Syntax;
using SS.Ynote.Classic.Core.UI;
using SS.Ynote.Classic.Core.UI.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public partial class Editor : DockContent, IEditor
    {
        private bool _isChanged;
        private ISyntax _syntax;
        private AutocompleteMenu _autocomplete;
        // whether the text content has been changed
        bool IsChanged
        {
            get { return _isChanged;  }
            set
            {
                if (IsChanged == value)
                    return;
                _isChanged = value;
                if (_isChanged) this.Text += "*";
                else this.Text = this.Text.Replace("*", "");
            }
        }
        /// <summary>
        /// The Syntax of the Editor
        /// </summary>
        public ISyntax Syntax {
            get { return _syntax; }
            set
            {
                if (_syntax == value) return;
                _syntax = value;
                textArea.CommentPrefix = _syntax.CommentPrefix;
                LoadSyntaxSpeceficSettings();
                _syntax.HighlightSyntax(textArea.Range);
            }
        }

        public FastColoredTextBox Tb
        {
            get { return textArea; }
        }
        /// <summary>
        /// File Name
        /// </summary>
        public string FileName
        {
            get { return this.Name; }
            set
            {
                this.Name = value;
                this.Text = Path.GetFileName(value);
            }
        }
        
        public YnoteSettings Settings { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Editor()
        {
            InitializeComponent();
            ApplyStyleColors();
            this.textArea.AutoIndentNeeded += textArea_AutoIndentNeeded;
            this.textArea.TextChangedDelayed += textArea_TextChangedDelayed;
            this.textArea.KeyDown += textArea_KeyDown;
            this.textArea.ScrollbarsUpdated += new System.EventHandler(this.textArea_ScrollbarsUpdated);
            this.map.BackColor = textArea.BackColor;
            this.map.ForeColor = textArea.SelectionColor;
            Syntax = Window.highlighter.GetSyntax(Constants.GlobalSettings["default_syntax"]);
            this.vScrollBar.Theme = Constants.Theme;
            this.hScrollBar.Theme = Constants.Theme;
            _autocomplete = new Autocomplete(textArea, Constants.Theme);
        }

        private void LoadSyntaxSpeceficSettings()
        {
            string fileName = Path.Combine(Constants.DATA_DIR, _syntax.Name + ".ynotesettings");
            if (!File.Exists(fileName))
                Settings = Constants.GlobalSettings.Clone();
            else
            {
                YnoteSettings settings = new YnoteSettings(fileName);
                Settings.MergeSettings(settings);
            }
            LoadSettings();
        }
        private void ApplyStyleColors()
        {
            Window.highlighter.Styles.ApplyStyling(textArea);
            this.container.BackColor = textArea.BackColor;
        }
       /// <summary>
       /// convert byte array to hexadecimal string
       /// </summary>
       /// <param name="bytes"></param>
       /// <returns></returns>
       string ByteToHex(byte[] bytes)
        {
            var hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private void LoadSettings()
        {
            textArea.ShowLineNumbers = Settings["line_numbers"];
            textArea.ShowFoldingLines = Settings["folding_lines"];
            textArea.CaretVisible = Settings["caret_visible"];
            textArea.CaretBlinking = Settings["caret_blinking"];
            textArea.WideCaret = Settings["block_caret"];
            textArea.WordWrap = Settings["word_wrap"];
            textArea.AutoIndent = Settings["auto_indent"];
            textArea.TabLength = (int)Settings["tab_length"];
            textArea.Zoom = (int)Settings["zoom"];
            textArea.Font = new Font(Settings["font_face"], (float)Settings["font_size"]);
            textArea.AutoCompleteBrackets = Settings["autocomplete_brackets"];
            textArea.HighlightFoldingIndicator = Settings["highlight_folding_indicator"];
            textArea.VirtualSpace = Settings["virtual_space"];
            textArea.LeftPadding = (int) Settings["left_padding"];
        }
        private void AdjustScrollbars()
        {
            AdjustScrollbar(vScrollBar, textArea.VerticalScroll.Maximum, textArea.VerticalScroll.Value, textArea.ClientSize.Height);
            AdjustScrollbar(hScrollBar, textArea.HorizontalScroll.Maximum, textArea.HorizontalScroll.Value, textArea.ClientSize.Width);
        }

        private void AdjustScrollbar(MyScrollBar scrollBar, int max, int value, int clientSize)
        {
            scrollBar.Maximum = max;
            scrollBar.Visible = max > 0;
            scrollBar.Value = Math.Min(scrollBar.Maximum, value);
        }

        void textArea_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            if(Syntax != null)
                Window.highlighter.HighlightSyntax(_syntax, e.ChangedRange);
            IsChanged = textArea.IsChanged;
        }

        void textArea_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
                _autocomplete.Items.AddItem(new AutocompleteItem(textArea.Range.GetFragment(@"\w+").Text));
        }

        void textArea_AutoIndentNeeded(object sender, AutoIndentEventArgs e)
        {
            if(Syntax != null)
                Syntax.AutoIndent(e);
        }

        private void textArea_ScrollbarsUpdated(object sender, EventArgs e)
        {
            AdjustScrollbars();
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            textArea.OnScroll(e, e.Type != ScrollEventType.ThumbTrack && e.Type != ScrollEventType.ThumbPosition);
        }
    }
}
