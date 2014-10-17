using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SS.Ynote.Classic.Core.UI
{
    /// <summary>
    /// A Ynote UI Theme
    /// </summary>
    public class UITheme
    {
        // the list of items
        private Dictionary<string,dynamic>[] items;

        /// <summary>
        /// Load the UI Theme
        /// </summary>
        /// <param name="file"></param>
        public UITheme(string file)
        {
            items = JsonConvert.DeserializeObject<Dictionary<string, dynamic>[]>(File.ReadAllText(file)); 
        }

        /// <summary>
        /// Get A Theme Value
        /// </summary>
        /// <param name="class_name">The Name/ID of the class</param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public Dictionary<string, dynamic> GetThemeClass(string class_name)
        {
            foreach (var item in items)
                if (item["class"] == class_name)
                    return item;
            return null;
        }
        public Color ToColor(dynamic setting)
        {
            if (setting.Count == 4)
            {
                Color c = Color.FromArgb(setting[0], setting[1], setting[2], setting[3]);
                return c;
            }
            // exception : dynamic[int] is JValue (uses Json.Linq)
            int setting1 = (int)setting[0].Value;
            int setting2 = (int)setting[1].Value;
            int setting3 = (int)setting[2].Value;
            Color x=  Color.FromArgb(setting1, setting2, setting3);
            return x;
        }

        public Image ToImage(dynamic setting)
        {
            return Image.FromFile(setting);
        }
    }
}
