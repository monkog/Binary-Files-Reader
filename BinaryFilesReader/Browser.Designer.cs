namespace BinaryFilesReader
{
    partial class Browser
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.iconsSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.vS2010StyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.vS2012StyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.treeView = new System.Windows.Forms.TreeView();
			this.buttonCreate = new System.Windows.Forms.Button();
			this.listView = new System.Windows.Forms.ListView();
			this.column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.iconsSetToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(574, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// loadFileToolStripMenuItem
			// 
			this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
			this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.loadFileToolStripMenuItem.Text = "Load file";
			this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.LoadFileClicked);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(116, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitClicked);
			// 
			// iconsSetToolStripMenuItem
			// 
			this.iconsSetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vS2012StyleToolStripMenuItem,
            this.vS2010StyleToolStripMenuItem});
			this.iconsSetToolStripMenuItem.Name = "iconsSetToolStripMenuItem";
			this.iconsSetToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
			this.iconsSetToolStripMenuItem.Text = "Icons set";
			// 
			// vS2010StyleToolStripMenuItem
			// 
			this.vS2010StyleToolStripMenuItem.Checked = true;
			this.vS2010StyleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.vS2010StyleToolStripMenuItem.Name = "vS2010StyleToolStripMenuItem";
			this.vS2010StyleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.vS2010StyleToolStripMenuItem.Text = "VS2017 style";
			this.vS2010StyleToolStripMenuItem.Click += new System.EventHandler(this.IconsTo2017StyleChanged);
			// 
			// vS2012StyleToolStripMenuItem
			// 
			this.vS2012StyleToolStripMenuItem.Name = "vS2012StyleToolStripMenuItem";
			this.vS2012StyleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.vS2012StyleToolStripMenuItem.Text = "VS2012 style";
			this.vS2012StyleToolStripMenuItem.Click += new System.EventHandler(this.IconsTo2012StyleChanged);
			// 
			// treeView
			// 
			this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.treeView.Location = new System.Drawing.Point(0, 27);
			this.treeView.Name = "treeView";
			this.treeView.Size = new System.Drawing.Size(208, 315);
			this.treeView.TabIndex = 1;
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AssemblyObjectSelected);
			// 
			// buttonCreate
			// 
			this.buttonCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCreate.Enabled = false;
			this.buttonCreate.Location = new System.Drawing.Point(447, 28);
			this.buttonCreate.Name = "buttonCreate";
			this.buttonCreate.Size = new System.Drawing.Size(115, 23);
			this.buttonCreate.TabIndex = 3;
			this.buttonCreate.Text = "Create object";
			this.buttonCreate.UseVisualStyleBackColor = true;
			this.buttonCreate.Click += new System.EventHandler(this.CreateClicked);
			// 
			// listView
			// 
			this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column});
			this.listView.ContextMenuStrip = this.contextMenuStrip;
			this.listView.Location = new System.Drawing.Point(215, 28);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(226, 314);
			this.listView.TabIndex = 0;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.List;
			this.listView.DoubleClick += new System.EventHandler(this.OpenInvokeMethodWindow);
			// 
			// column
			// 
			this.column.Text = "Nazwa metody";
			this.column.Width = 150;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listToolStripMenuItem,
            this.detailsToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip1";
			this.contextMenuStrip.Size = new System.Drawing.Size(110, 48);
			// 
			// listToolStripMenuItem
			// 
			this.listToolStripMenuItem.Checked = true;
			this.listToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.listToolStripMenuItem.Name = "listToolStripMenuItem";
			this.listToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.listToolStripMenuItem.Text = "List";
			this.listToolStripMenuItem.Click += new System.EventHandler(this.DisplayMethodList);
			// 
			// detailsToolStripMenuItem
			// 
			this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
			this.detailsToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.detailsToolStripMenuItem.Text = "Details";
			this.detailsToolStripMenuItem.Click += new System.EventHandler(this.DisplayDetailedMethodList);
			// 
			// Browser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(574, 341);
			this.Controls.Add(this.listView);
			this.Controls.Add(this.buttonCreate);
			this.Controls.Add(this.treeView);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Browser";
			this.Text = "Browser of classes from .NET binary files";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.ToolStripMenuItem iconsSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vS2010StyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vS2012StyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        public System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader column;
    }
}

