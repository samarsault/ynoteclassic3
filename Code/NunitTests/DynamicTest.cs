using System.Collections.Generic;
using NUnit.Framework;
using SS.Ynote.Classic.Core.Settings;

namespace NunitTests
{
    [TestFixture]
    class DynamicTest
    {
        public void ImageDetectionTest()
        {
           var settings =  new YnoteSettings("Default.ynotesettings");
           // type of JArray
           var array =  settings["image_files"];
           var collection = array.ToObject<IEnumerable<string>>();
            bool contains = collection.Contains(".png");
            Assert.AreEqual(true, contains);
        }
    }
}
