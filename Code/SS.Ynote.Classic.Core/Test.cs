using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SS.Ynote.Classic.Core
{
    public class Foo
    {
        public string re1 { get; set; }
        public string re2 { get; set; }
        public RegexOptions Options { get; set; }
    }
    public class Test
    {
        public static void Bar()
        {
            Foo bar = new Foo();
            bar.re1 = @"("""""".*?"""""")|('''.*?''')|("""""".*)|('''.*)";
            bar.re2 = @"("""""".*?"""""")|('''.*?''')|(.*"""""")|(.*''')";
            bar.Options = RegexOptions.Singleline | RegexOptions.RightToLeft;
            string j = JsonConvert.SerializeObject(bar);
            File.WriteAllText("test.json", j);
        }
    }
}
