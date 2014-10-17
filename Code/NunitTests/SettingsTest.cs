using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using SS.Ynote.Classic.Core.Settings;

namespace NunitTests
{
    [TestFixture]
    public class SettingsTest
    {
        [TestCase]
        public void TestSettings()
        {
            string FOO_SETTINGS = GenerateFooSettings();
            string TEMP_PATH = Path.GetTempPath() + "foo.json";
            File.WriteAllText(TEMP_PATH, FOO_SETTINGS);
            YnoteSettings s = new YnoteSettings(TEMP_PATH);
            Assert.AreEqual(s.GetDictionary(), GetFooDictionary());
        }

        private string GenerateFooSettings()
        {
            return @"{
        'colorScheme':'Themes/Cobalt.ynotetheme',
        'uiTheme':'Themes/Moonrise.uitheme',
        'showMiniMap':true,
        'paddingWidth':17
}";
        }

        private Dictionary<string, dynamic> GetFooDictionary()
        {
            var dic = new Dictionary<string, dynamic>();
            dic.Add("colorScheme", "Themes/Cobalt.ynotetheme");
            dic.Add("uiTheme", "Themes/Moonrise.uitheme");
            dic.Add("showMiniMap", true);
            dic.Add("paddingWidth", 17);
            return dic;
        } 
    }
}
