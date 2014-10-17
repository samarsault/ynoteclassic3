using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using SS.Ynote.Classic.Core.API;

namespace SS.Ynote.Classic.Core.UI
{
    /// <summary>
    /// The Ynote Classic Menu Base
    /// </summary>
    public class YnoteMenu
    {
        private YnoteMenuItem[] MenuItems;
        /// <summary>
        /// Default Constructor
        /// </summary>
        public YnoteMenu(string file)
        {
            try
            {
                string json = File.ReadAllText(file);
                MenuItems = JsonConvert.DeserializeObject<YnoteMenuItem[]>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Load Menu, Error in File\n"+ex.Message);
            }
        }
        /// <summary>
        /// Add Menu Items to the Menu
        /// </summary>
        /// <param name="menu">The Menu</param>
        /// <param name="api">The API</param>
        public void AddMenuItems(Menu menu, IAPI api)
        {
            if (MenuItems == null) 
                return;
            foreach (var menuitem in MenuItems)
                menu.MenuItems.Add(menuitem.ToMenuItem(api));
        }

        public void MergeMenu(Menu menu1, Menu menu2)
        {
            menu1.MergeMenu(menu2);
        }
    }

    internal class YnoteMenuItem
    {
        /// <summary>
        /// Name of the Menu Item
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Command of The Menu Item
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// Arguments
        /// </summary>
        public dynamic[] Arguments { get; set; }
        /// <summary>
        /// Whether the Menu is Checked/Unchecked on click
        /// </summary>
        public bool CheckOnClick { get; set; }
        /// <summary>
        /// Children of the Menu Item
        /// </summary>
        public YnoteMenuItem[] Children { get; set; }

        public MenuItem ToMenuItem(IAPI api)
        {
            var item = new MenuItem(Name);
            item.Name = Method;
            if (!string.IsNullOrEmpty(Method))
                item.Click += delegate{
                    api.CallMethod(Method, Arguments);
                    if (CheckOnClick)
                        item.Checked = !item.Checked;
                };
            if(Children != null)
                foreach (YnoteMenuItem menuitem in Children)
                    item.MenuItems.Add(menuitem.ToMenuItem(api));
            return item;
        }
    }
}
