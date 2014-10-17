using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FastTreeNS;

namespace SS.Ynote.Classic.Core.UI
{
	/// <summary>
	/// Custom Tree View
	/// </summary>
	public sealed class SidebarTree : FastTree
	{

		public SidebarTree(UITheme theme)
		{
			ShowRootNode = true;
			var treeViewClass = theme.GetThemeClass("tree_view");
			this.FullItemSelect = treeViewClass["full_item_select"];
			this.BackColor = theme.ToColor(treeViewClass["background"]);
			this.ForeColor = theme.ToColor(treeViewClass["foreground"]);
		    this.SelectionColor = theme.ToColor(treeViewClass["selection_color"]);
		    this.ImageCollapse = theme.ToImage(treeViewClass["image_collapse"]);
		    this.ImageExpand = theme.ToImage(treeViewClass["image_expand"]);
		}
	}

}
