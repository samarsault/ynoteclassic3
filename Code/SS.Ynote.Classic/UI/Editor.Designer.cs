using System.Runtime.Serialization;
using System.Windows.Forms;

namespace SS.Ynote.Classic.UI
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textArea = new FastColoredTextBoxNS.FastColoredTextBox();
            this.vScrollBar = new SS.Ynote.Classic.Core.UI.MyScrollBar();
            this.hScrollBar = new SS.Ynote.Classic.Core.UI.MyScrollBar();
            this.container = new System.Windows.Forms.SplitContainer();
            this.map = new FastColoredTextBoxNS.DocumentMap();
            ((System.ComponentModel.ISupportInitialize)(this.textArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.container)).BeginInit();
            this.container.Panel1.SuspendLayout();
            this.container.Panel2.SuspendLayout();
            this.container.SuspendLayout();
            this.SuspendLayout();
            // 
            // textArea
            // 
            this.textArea.AutoCompleteBrackets = true;
            this.textArea.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.textArea.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.textArea.BackBrush = null;
            this.textArea.CharHeight = 14;
            this.textArea.CharWidth = 8;
            this.textArea.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textArea.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textArea.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.textArea.IsReplaceMode = false;
            this.textArea.Location = new System.Drawing.Point(0, 0);
            this.textArea.Name = "textArea";
            this.textArea.Paddings = new System.Windows.Forms.Padding(0);
            this.textArea.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textArea.ShowScrollBars = false;
            this.textArea.Size = new System.Drawing.Size(455, 330);
            this.textArea.TabIndex = 0;
            this.textArea.Zoom = 100;
            // 
            // vScrollBar
            // 
            this.vScrollBar.BorderColor = System.Drawing.Color.Silver;
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(536, 0);
            this.vScrollBar.Maximum = 100;
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Orientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.vScrollBar.Size = new System.Drawing.Size(13, 330);
            this.vScrollBar.TabIndex = 1;
            this.vScrollBar.ThumbColor = System.Drawing.Color.Gray;
            this.vScrollBar.ThumbSize = 10;
            this.vScrollBar.Value = 0;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
            // 
            // hScrollBar
            // 
            this.hScrollBar.BorderColor = System.Drawing.Color.Silver;
            this.hScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar.Location = new System.Drawing.Point(0, 330);
            this.hScrollBar.Maximum = 100;
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Orientation = System.Windows.Forms.ScrollOrientation.HorizontalScroll;
            this.hScrollBar.Size = new System.Drawing.Size(549, 15);
            this.hScrollBar.TabIndex = 2;
            this.hScrollBar.ThumbColor = System.Drawing.Color.Gray;
            this.hScrollBar.ThumbSize = 10;
            this.hScrollBar.Value = 0;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
            // 
            // container
            // 
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(0, 0);
            this.container.Name = "container";
            // 
            // container.Panel1
            // 
            this.container.Panel1.Controls.Add(this.textArea);
            // 
            // container.Panel2
            // 
            this.container.Panel2.Controls.Add(this.map);
            this.container.Size = new System.Drawing.Size(536, 330);
            this.container.SplitterDistance = 455;
            this.container.SplitterWidth = 1;
            this.container.TabIndex = 4;
            // 
            // map
            // 
            this.map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map.ForeColor = System.Drawing.Color.Maroon;
            this.map.Location = new System.Drawing.Point(0, 0);
            this.map.Name = "map";
            this.map.ScrollbarVisible = false;
            this.map.Size = new System.Drawing.Size(80, 330);
            this.map.TabIndex = 4;
            this.map.Target = this.textArea;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 345);
            this.Controls.Add(this.container);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.hScrollBar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Editor";
            this.Text = "untitled";
            ((System.ComponentModel.ISupportInitialize)(this.textArea)).EndInit();
            this.container.Panel1.ResumeLayout(false);
            this.container.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.container)).EndInit();
            this.container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox textArea;
        private SS.Ynote.Classic.Core.UI.MyScrollBar vScrollBar;
        private SS.Ynote.Classic.Core.UI.MyScrollBar hScrollBar;
        private SplitContainer container;
        private FastColoredTextBoxNS.DocumentMap map;


    }
}