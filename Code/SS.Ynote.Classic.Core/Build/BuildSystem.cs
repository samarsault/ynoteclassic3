using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using IronPython.Modules;
using Newtonsoft.Json;

namespace SS.Ynote.Classic.Core.Build
{
    // Ynote Build System
    // Rough Format
    // {
    //      // Valid for only C# Syntax (optional parameter)
    //      "syntax":"{syntax_name}"
    //      "cmd":{
    //          "process":"{process_name}",
    //          "args": [ '${PROJECT_DIR} or ${FILE_PATH}' ], // if project dir is null FILE PATH is used
    //          "file_regex":"" // regex to capture file output (optional parameter)
    //
    //      },
    //      // Run Another process after the first
    //      "cmd2":{
    //          "process":"{process2_name}",
    //          "args":[${args_seperated_by_spaces} ]
    //          "shell":true // whether the build should be run in the system's shell(bash,cmd) or ynote's (default_value:false)
    //      },
    // }
    /// <summary>
    /// A Ynote Build System
    /// </summary>
    public class BuildSystem
    {
        private Dictionary<string, dynamic> BuildOptions;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file">Build File</param>
        public BuildSystem(string file)
        {
            BuildOptions = new Dictionary<string, dynamic>();
            string json = File.ReadAllText(file);
            BuildOptions = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
        }
        /// <summary>
        /// Substitute Variables in a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string SubstituteVariables(string input)
        {
            return input;
        }
        private void StartCmd(string proc, string args, string workingDir)
        {
            ProcessStartInfo info = new ProcessStartInfo(proc, args);
            if(!string.IsNullOrEmpty(workingDir))
                info.WorkingDirectory = workingDir;
            Process.Start(info);
        }
        /// <summary>
        /// Run the Build
        /// </summary>
        public void Run()
        {
            foreach (var option in BuildOptions)
            {
                if (option.Key == "cmd")
                {
                    StartCmd(option.Value["proc"],option.Value["args"],option.Value["workingDir"]);
                }
            }
        }
    }
}