using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using SS.Ynote.Classic.Core.API;
using SS.Ynote.Classic.Core.Keybindings;
using SS.Ynote.Classic.Core.Plugins;
using SS.Ynote.Classic.Core.Settings;
using SS.Ynote.Classic.Core.Syntax;
using SS.Ynote.Classic.Core.UI;
using SS.Ynote.Classic.UI;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic
{
    public partial class Window : Form,IYnote
    {
        private IAPI pythonApi;
        private Keymap keymap;
        public static SyntaxHighlighter highlighter;
        private StyleFactory styleFactory;
        private System.Windows.Forms.Timer status_updater;
        /// <summary>
        /// Initialize the Main Window
        /// </summary>
        public Window()
        {
            InitializeComponent();
            LoadSettings();
            // load syntaxes
            styleFactory = new StyleFactory(Constants.GlobalSettings["color_scheme"]);
            highlighter = new SyntaxHighlighter(styleFactory);
            Constants.Syntaxes = highlighter.LoadedSyntaxes;
            InitializePynote();
            // load menu
            var menu = new YnoteMenu("Package/Default.ynotemenu");
            menu.AddMenuItems(this.Menu,pythonApi);
            // load keymap
            keymap = new Keymap("Package/Default.ynotekeys");
            this.KeyPreview = true;
            AddKeysToMenu();
            // status timer
            status_updater = new System.Windows.Forms.Timer();
            status_updater.Tick += status_updater_Tick;
            status_updater.Start();
            // load default theme
            Constants.Theme = new UITheme(Constants.GlobalSettings["theme"]);
            New();
        }

        void status_updater_Tick(object sender, EventArgs e)
        {
            var edit = GetActiveEditor();
            if (edit == null) return;
            string line = (edit.Tb.Selection.Start.iLine + 1).ToString();
            string column = (edit.Tb.Selection.Start.iChar + 1).ToString();
            status.Text = string.Format("Line {0} Column {1}", line, column);
        }

        private void LoadSettings()
        {
            var settingsFile = "Package/Default.ynotesettings";
            Constants.GlobalSettings = new YnoteSettings(settingsFile);
        }
        /// <summary>
        /// Initialize ynote's Python API
        /// </summary>
        private void InitializePynote()
        {
            pythonApi = new PyNote();
            pythonApi.CreateEngine(this);
            ThreadPool.QueueUserWorkItem(delegate
            {
                pythonApi.LoadCommands();
            });
        }

        public void New()
        {
            var edit = new Editor();
            edit.Show(panel);
        }

        public void Open(string file)
        {
            foreach(DockContent f in panel.Documents) if (f.Name == file) {f.Show();return;}
            if (File.Exists(file))
            {
               string extension = Path.GetExtension(file).ToLower();
               var image_extensions = Constants.GlobalSettings["image_files"].ToObject<IEnumerable<string>>();
               if (image_extensions.Contains(extension))
               {
                   var viewer = new ImagePreview(styleFactory);
                   viewer.Show(panel);
                   viewer.OpenFile(file);
               }
               else
               {
                   var edit = new Editor();
                   edit.Tb.OpenFile(file);
                   edit.FileName = file;
                   edit.Show(panel);
                   foreach (var syntax in highlighter.LoadedSyntaxes)
                       if (syntax.FileTypes.Contains(extension))
                           edit.Syntax = syntax;
                }
            }
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }


        public IEnumerable<IEditor> GetEditors()
        {
            return panel.Documents.OfType<IEditor>();
        }

        public IEditor GetActiveEditor()
        {
            return panel.ActiveDocument as Editor;
        }


        public void ShowProjectView(string dir = null)
        {
            var projectView = new ProjectView(this);
            projectView.Show(panel, DockState.DockLeft);
            if(!string.IsNullOrEmpty(dir))
                projectView.OpenDirectory(dir);
        }
        /// <summary>
        /// Add the User-Modified Keys to Menu for Ease
        /// </summary>
        private void AddKeysToMenu()
        {
            var converter = new KeysConverter();
            foreach (MenuItem mnu in menu.MenuItems)
            {
                foreach (MenuItem m in mnu.MenuItems)
                {
                    foreach (var pair in keymap.Values)
                    {
                        if (m.Name == pair.Value)
                            m.Text += "\t" + converter.ConvertToString(pair.Key);
                    }
                }
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if(keymap.Contains(e.KeyData))
                pythonApi.CallMethod(keymap[e.KeyData]);
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Expand Variable to dynamic object
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public dynamic ExpandVariable(string variable)
        {
            return Constants.ExpandVariable(variable);
        }
    }
}
