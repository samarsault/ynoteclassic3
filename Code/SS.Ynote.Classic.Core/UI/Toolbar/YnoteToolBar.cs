using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SS.Ynote.Classic.Core.UI
{
    /// <summary>
    /// The Ynote Tool Bar
    /// </summary>
    public class YnoteToolbar
    {
        /// <summary>
        /// Add Items to the ToolBar
        /// </summary>
        /// <param name="dataDir"></param>
        /// <param name="tool"></param>
        public static void AddItems(string dataDir, ToolStrip tool)
        {
            var items = GetToolBarItems(dataDir);
            if (items == null) return;
            foreach (var item in items)
            {
                Image img = Image.FromFile(Path.Combine(dataDir, item.IconFile));
                var button = new ToolStripButton(item.Text);//, img, (sender, args) => Commander.RunCommand(Globals.Ynote, item.Command));
                button.DisplayStyle = ToolStripItemDisplayStyle.Image;
                tool.Items.Add(button);
            }
        }
        static IEnumerable<ToolBarItem> GetToolBarItems(string toolBarFile)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<ToolBarItem>>(File.ReadAllText(toolBarFile));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Parsing ToolBar File\n " + ex.Message, null, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
