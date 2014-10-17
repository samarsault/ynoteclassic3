namespace SS.Ynote.Classic.Core.UI
{
    /// <summary>
    /// A Ynote ToolBar Item
    /// </summary>
    public abstract class ToolBarItem
    {
        /// <summary>
        /// Text / ToolTip of the Tool Bar Item
        /// </summary>
        public string Text;

        /// <summary>
        /// The Icon of the Item
        /// </summary>
        public string IconFile;

        /// <summary>
        /// Method
        /// </summary>
        public string Method;
        /// <summary>
        /// Arguments
        /// </summary>
        public dynamic[] Args;
    }
}