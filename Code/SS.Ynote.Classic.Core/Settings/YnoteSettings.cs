using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SS.Ynote.Classic.Core.Settings
{
    /// <summary>
    /// A Ynote Settings File
    /// </summary>
    public class YnoteSettings
    {
        private Dictionary<string, dynamic> Settings;
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="file">Settings file, eg - User.ynotesettings</param>
        public YnoteSettings(string file)
        {
            this.LoadSettings(file);
        }
        /// <summary>
        /// Constructor with Dictionary
        /// </summary>
        /// <param name="settings"></param>
        private YnoteSettings(Dictionary<string, dynamic> settings)
        {
            Settings = settings;
        }

        /// <summary>
        ///  Load Settings
        /// </summary>
        /// <param name="file"></param>
        void LoadSettings(string file)
        {
            if (Settings == null)
            {
                string json = File.ReadAllText(file);
                Settings = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
            }
        }
        /// <summary>
        /// Save Settings to file
        /// </summary>
        /// <param name="file">file</param>
        public void Save(string file)
        {
            string serialized = JsonConvert.SerializeObject(Settings, Formatting.Indented);
            File.WriteAllText(file,serialized);
        }

        /// <summary>
        /// Get the Settings Dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, dynamic> GetDictionary()
        {
            return Settings;
        } 
        /// <summary>
        /// Get/Set a Setting
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public dynamic this[string option]
        {
            get
            {
                    return Settings[option];
            }
            set
            {
                Settings[option] = value;
            }
        }

        /// <summary>
        /// Delete a Setting
        /// </summary>
        /// <param name="setting"></param>
        public void DeleteSetting(string setting)
        {
            Settings.Remove(setting);
        }
        /// <summary>
        /// Add a Setting
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="value"></param>
        public void AddSetting(string setting, dynamic value)
        {
            Settings.Add(setting, value);
        }

        /// <summary>
        /// Merge these Settings with "settings"
        /// </summary>
        /// <param name="settings"></param>
        public void MergeSettings(YnoteSettings settings)
        {
            Dictionary<string, dynamic> dic = settings.GetDictionary();
            Settings = MergeDictionary(Settings, dic);
        }
        /// <summary>
        /// Merge 2 dictionaries
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        static Dictionary<T1, T2> MergeDictionary<T1, T2>(Dictionary<T1, T2> first, Dictionary<T1, T2> second)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");

            var merged = new Dictionary<T1, T2>();
            first.ToList().ForEach(kv => merged[kv.Key] = kv.Value);
            second.ToList().ForEach(kv => merged[kv.Key] = kv.Value);

            return merged;
        }
        /// <summary>
        /// Clone a Ynote Settings Instance
        /// </summary>
        /// <returns></returns>
        public YnoteSettings Clone()
        {
            return new YnoteSettings(Settings);
        }
    }
}
