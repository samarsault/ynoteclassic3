namespace SS.Ynote.Classic.Core.UI
{
    partial class Commander
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Commander));
            this.tbInput = new System.Windows.Forms.TextBox();
            this.list = new FastTreeNS.FastList();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInput.Location = new System.Drawing.Point(3, 3);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(396, 29);
            this.tbInput.TabIndex = 0;
            this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            // 
            // list
            // 
            this.list.AutoScroll = true;
            this.list.AutoScrollMinSize = new System.Drawing.Size(0, 1902);
            this.list.BackColor = System.Drawing.SystemColors.Window;
            this.list.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.list.FullItemSelect = true;
            this.list.ImageCheckBoxOff = ((System.Drawing.Image)(resources.GetObject("list.ImageCheckBoxOff")));
            this.list.ImageCheckBoxOn = ((System.Drawing.Image)(resources.GetObject("list.ImageCheckBoxOn")));
            this.list.ImageCollapse = ((System.Drawing.Image)(resources.GetObject("list.ImageCollapse")));
            this.list.ImageDefaultIcon = ((System.Drawing.Image)(resources.GetObject("list.ImageDefaultIcon")));
            this.list.ImageExpand = ((System.Drawing.Image)(resources.GetObject("list.ImageExpand")));
            this.list.IsEditMode = false;
            this.list.ItemCount = 100;
            this.list.Location = new System.Drawing.Point(4, 39);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(395, 180);
            this.list.TabIndex = 1;
            // 
            // Commander
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.list);
            this.Controls.Add(this.tbInput);
            this.Name = "Commander";
            this.Size = new System.Drawing.Size(402, 231);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbInput;
        private FastTreeNS.FastList list;
    }
}
