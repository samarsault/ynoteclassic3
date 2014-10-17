using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.Core.UI.Controls
{
    /// <summary>
    /// Custom Themable Autocomplete Menu
    /// </summary>
    public class Autocomplete : AutocompleteMenu
    {
        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="theme"></param>
        public Autocomplete(FastColoredTextBox tb, UITheme theme) 
            : base(tb)
        {
            ApplyStyling(theme);
            this.AppearInterval = 50;
        }

        private void ApplyStyling(UITheme theme)
        {
            var acclass = theme.GetThemeClass("autocomplete_menu");
            if (acclass == null) return;
            this.SelectedColor = theme.ToColor(acclass["selected_color"]);
            this.BackColor = theme.ToColor(acclass["background"]);
            this.ForeColor = theme.ToColor(acclass["foreground"]);
            this.HoveredColor = theme.ToColor(acclass["hovered_color"]);
        }
    }
}
