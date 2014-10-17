using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SS.Ynote.Classic.Core.Plugins;
using SS.Ynote.Classic.Core.Syntax;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic.UI
{
    class ImagePreview : DockContent, IEditor
    {
        public ImagePreview(StyleFactory styles)
        {
            InitializeComponent();
            viewer.BackColor = styles.GetColor("Background");
            viewer.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        public void OpenFile(string path)
        {
            this.Text = Path.GetFileName(path);
            Image img = Image.FromFile(path);
            viewer.Image = img;
        }
        private System.Windows.Forms.PictureBox viewer;
    
        public string FileName { get; private set; }

        public Core.Settings.YnoteSettings Settings
        {
            get { throw new NotImplementedException(); }
        }

        public FastColoredTextBoxNS.FastColoredTextBox Tb
        {
            get { throw new NotImplementedException(); }
        }

        private void InitializeComponent()
        {
            this.viewer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.viewer)).BeginInit();
            this.SuspendLayout();
            // 
            // viewer
            // 
            this.viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer.Location = new System.Drawing.Point(0, 0);
            this.viewer.Name = "viewer";
            this.viewer.Size = new System.Drawing.Size(284, 262);
            this.viewer.TabIndex = 0;
            this.viewer.TabStop = false;
            // 
            // ImagePreview
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.viewer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ImagePreview";
            this.Text = "Image Preview";
            ((System.ComponentModel.ISupportInitialize)(this.viewer)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
