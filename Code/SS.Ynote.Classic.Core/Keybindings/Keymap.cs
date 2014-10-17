using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SS.Ynote.Classic.Core.Keybindings
{
    /// <summary>
    /// A Ynote Keymap
    /// </summary>
    public class Keymap
    {
        /// <summary>
        /// The Enumerable Values
        /// </summary>
        public Dictionary<Keys, string> Values
        {
            get { return KeyMapping; }
        }
        private Dictionary<Keys, string> KeyMapping;
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Keymap(string file)
        {
            LoadKeymapFile(file);
        }
        /// <summary>
        /// Load a KeyMap File
        /// </summary>
        /// <param name="file"></param>
        private void LoadKeymapFile(string file)
        {
            string data = File.ReadAllText(file);
            KeyMapping =  JsonConvert.DeserializeObject<Dictionary<Keys, string>>(data);
        }
        /// <summary>
        /// Keymap contains a key
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public bool Contains(Keys k)
        {
            return KeyMapping.ContainsKey(k);
        }
        /// <summary>
        /// Keymap Contains a Method
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(string value)
        {
            return KeyMapping.ContainsValue(value);
        }
        /// <summary>
        /// Get The Method assigned to a key
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public string this[ Keys k]
        {
            get { return KeyMapping[k]; }
        }
    }
}
