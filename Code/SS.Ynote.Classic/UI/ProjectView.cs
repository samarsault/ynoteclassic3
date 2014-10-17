using System.IO;
using SS.Ynote.Classic.Core;
using SS.Ynote.Classic.Core.Plugins;
using SS.Ynote.Classic.Core.UI;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    public class ProjectView : DockContent
    {
        private IYnote _ynote;

        public ProjectView(IYnote ynote)
        {
            InitializeComponent();
            this._ynote = ynote;
        }

        public void OpenProject(string file)
        {
            var project = YnoteProject.Load(file);
            foreach(FolderInfo f in project.Folders)
                OpenDirectory(f.Path);
        }
        public void OpenDirectory(string dir)
        {
            if (dir == null)
                return;
            var node = new FileNode(dir, true);
            treeView.Build(node);
        }

        private void OpenFile(string file)
        {
            _ynote.Open(file);
        }
        /// <summary>
        /// Check if str is a Dir
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        bool IsDir(string str)
        {
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(str);

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                return true;
            return false;
        }
        void treeView_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            foreach (var item in treeView.SelectedNodes)
            {
                FileNode node = (FileNode) item;
                if(!IsDir(node.Path))
                    OpenFile(node.Path);
            }
        }

        #region Windows Forms Designer Generated Code

        private SS.Ynote.Classic.Core.UI.SidebarTree treeView;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectView));
            this.treeView = new SS.Ynote.Classic.Core.UI.SidebarTree(Constants.Theme);
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.AutoScroll = true;
            this.treeView.AutoScrollMinSize = new System.Drawing.Size(0, 59);
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ImageCheckBoxOff = ((System.Drawing.Image)(resources.GetObject("treeView.ImageCheckBoxOff")));
            this.treeView.ImageCheckBoxOn = ((System.Drawing.Image)(resources.GetObject("treeView.ImageCheckBoxOn")));
            this.treeView.ImageDefaultIcon = ((System.Drawing.Image)(resources.GetObject("treeView.ImageDefaultIcon")));
            this.treeView.IsEditMode = false;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.ShowExpandBoxes = true;
            this.treeView.ShowRootNode = true;
            this.treeView.Size = new System.Drawing.Size(284, 262);
            this.treeView.TabIndex = 0;
            this.treeView.MouseClick += treeView_MouseClick;
            // 
            // ProjectView
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.treeView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ProjectView";
            this.ResumeLayout(false);

        }
        #endregion
    }
}
