// Portable for 1-dir development
#define PORTABLE

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SS.Ynote.Classic.Core.Settings;
using SS.Ynote.Classic.Core.Syntax;
using SS.Ynote.Classic.Core.UI;

namespace SS.Ynote.Classic
{
    public static class Constants
    {
        /// <summary>
        /// THE DATA DIRECTORY
        /// </summary>
#if PORTABLE
        public static string DATA_DIR = @"Package\";
#else
        public static string DATA_DIR = Environment.ExpandEnvironmentVariables(@"%appdata%\Ynote Classic\");
#endif
        public static IEnumerable<ISyntax> Syntaxes;
        /// <summary>
        ///  Default Global Settigns
        /// </summary>
        public static YnoteSettings GlobalSettings;
        /// <summary>
        /// The Active UI Theme
        /// </summary>
        public static UITheme Theme;

        /// <summary>
        /// Expand a Variable like ${DATA_DIR}
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static dynamic ExpandVariable(string variable)
        {
            if (Regex.IsMatch(variable, @"\${.*?}"))
            {
                string varName = variable.Replace('$', Char.MinValue).Split(new[] { '{', '}' })[1];
                return GetProperty(varName);
            }
            return null;
        }

        public static dynamic GetProperty(string name)
        {
            Type CLASS_TYPE = typeof(Constants);
            var property = CLASS_TYPE.GetField(name);
            if(property != null)
                return property.GetValue(CLASS_TYPE);
            return null;
        }
    }
}
