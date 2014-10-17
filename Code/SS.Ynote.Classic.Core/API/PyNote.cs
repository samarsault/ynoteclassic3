using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using IronPython.Hosting;
using IronPython.Modules;
using Microsoft.Scripting.Hosting;
using SS.Ynote.Classic.Core.Plugins;

namespace SS.Ynote.Classic.Core.API
{
    /// <summary>
    /// Python API(default) for Ynote Classic
    /// </summary>
    public class PyNote : IAPI
    {
        private ScriptEngine engine;
        private ScriptScope scope;

        /// <summary>
        /// Create the engine
        /// </summary>
        /// <param name="ynote"></param>
        public void CreateEngine(IYnote ynote)
        {
            engine = Python.CreateEngine(new Dictionary<string, object>{{"Debug",true}});
            scope = engine.CreateScope();
            scope.SetVariable("ynote", ynote);
            scope.SetVariable("editor", ynote.GetActiveEditor());
        }
        /// <summary>
        /// Call a Method
        /// </summary>
        /// <param name="method"></param>
        /// <param name="arguments"></param>
        public void CallMethod(string method, dynamic arguments=null)
        {
            if (string.IsNullOrEmpty(method))
                return;
            var fun = scope.GetVariable(method);
            if (arguments == null)
                fun();
            else
                fun(arguments);
        }

        /// <summary>
        /// Load API Commands
        /// </summary>
        public void LoadCommands()
        {
            string[] scripts = Directory.GetFiles("API","*.py");
            foreach (var script  in scripts)
            {
                engine.ExecuteFile(script,scope);
            }
        }
    }
}
