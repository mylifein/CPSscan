namespace CPSscan
{
    partial class MainFm
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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.ProductLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WOBCPrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContainerNumSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BCScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RepeatPrttoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RepositoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InStoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QueryStoredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetPwdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BarcodeScrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.經管ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BAControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProductLineToolStripMenuItem,
            this.RepositoryToolStripMenuItem,
            this.QueryToolStripMenuItem,
            this.經管ToolStripMenuItem,
            this.WindowToolStripMenuItem,
            this.SetupToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.MdiWindowListItem = this.WindowToolStripMenuItem;
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(846, 25);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // ProductLineToolStripMenuItem
            // 
            this.ProductLineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WOBCPrintToolStripMenuItem,
            this.ContainerNumSettingToolStripMenuItem,
            this.BCScanToolStripMenuItem,
            this.RepeatPrttoolStripMenuItem1,
            this.BarcodeScrapToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExitToolStripMenuItem});
            this.ProductLineToolStripMenuItem.Name = "ProductLineToolStripMenuItem";
            this.ProductLineToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.ProductLineToolStripMenuItem.Text = "產線(&A)";
            this.ProductLineToolStripMenuItem.MouseEnter += new System.EventHandler(this.ProductLineToolStripMenuItem_MouseEnter);
            this.ProductLineToolStripMenuItem.MouseLeave += new System.EventHandler(this.ProductLineToolStripMenuItem_MouseLeave);
            // 
            // WOBCPrintToolStripMenuItem
            // 
            this.WOBCPrintToolStripMenuItem.Name = "WOBCPrintToolStripMenuItem";
            this.WOBCPrintToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.WOBCPrintToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.WOBCPrintToolStripMenuItem.Text = "列印工單條碼(&A)";
            this.WOBCPrintToolStripMenuItem.Click += new System.EventHandler(this.WOBCPrintToolStripMenuItem_Click);
            this.WOBCPrintToolStripMenuItem.MouseEnter += new System.EventHandler(this.WOBCPrintToolStripMenuItem_MouseEnter);
            this.WOBCPrintToolStripMenuItem.MouseLeave += new System.EventHandler(this.WOBCPrintToolStripMenuItem_MouseLeave);
            // 
            // ContainerNumSettingToolStripMenuItem
            // 
            this.ContainerNumSettingToolStripMenuItem.Name = "ContainerNumSettingToolStripMenuItem";
            this.ContainerNumSettingToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.ContainerNumSettingToolStripMenuItem.Text = "周轉箱數量設置";
            this.ContainerNumSettingToolStripMenuItem.Click += new System.EventHandler(this.ContainerNumSettingToolStripMenuItem_Click);
            // 
            // BCScanToolStripMenuItem
            // 
            this.BCScanToolStripMenuItem.Name = "BCScanToolStripMenuItem";
            this.BCScanToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.BCScanToolStripMenuItem.Text = "條碼掃描(&B)";
            this.BCScanToolStripMenuItem.Click += new System.EventHandler(this.BCScanToolStripMenuItem_Click);
            this.BCScanToolStripMenuItem.MouseEnter += new System.EventHandler(this.BCScanToolStripMenuItem_MouseEnter);
            this.BCScanToolStripMenuItem.MouseLeave += new System.EventHandler(this.BCScanToolStripMenuItem_MouseLeave);
            // 
            // RepeatPrttoolStripMenuItem1
            // 
            this.RepeatPrttoolStripMenuItem1.Name = "RepeatPrttoolStripMenuItem1";
            this.RepeatPrttoolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
            this.RepeatPrttoolStripMenuItem1.Text = "重印歷史貼紙(&C)";
            this.RepeatPrttoolStripMenuItem1.Click += new System.EventHandler(this.RepeatPrttoolStripMenuItem1_Click);
            this.RepeatPrttoolStripMenuItem1.MouseEnter += new System.EventHandler(this.RepeatPrttoolStripMenuItem1_MouseEnter);
            this.RepeatPrttoolStripMenuItem1.MouseLeave += new System.EventHandler(this.RepeatPrttoolStripMenuItem1_MouseLeave);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(210, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.ExitToolStripMenuItem.Text = "退出(&D)";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            this.ExitToolStripMenuItem.MouseEnter += new System.EventHandler(this.ExitToolStripMenuItem_MouseEnter);
            this.ExitToolStripMenuItem.MouseLeave += new System.EventHandler(this.ExitToolStripMenuItem_MouseLeave);
            // 
            // RepositoryToolStripMenuItem
            // 
            this.RepositoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InStoreToolStripMenuItem});
            this.RepositoryToolStripMenuItem.Name = "RepositoryToolStripMenuItem";
            this.RepositoryToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.RepositoryToolStripMenuItem.Text = "倉庫(&B)";
            this.RepositoryToolStripMenuItem.MouseEnter += new System.EventHandler(this.RepositoryToolStripMenuItem_MouseEnter);
            this.RepositoryToolStripMenuItem.MouseLeave += new System.EventHandler(this.RepositoryToolStripMenuItem_MouseLeave);
            // 
            // InStoreToolStripMenuItem
            // 
            this.InStoreToolStripMenuItem.Name = "InStoreToolStripMenuItem";
            this.InStoreToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.InStoreToolStripMenuItem.Text = "入庫(&A)";
            this.InStoreToolStripMenuItem.Click += new System.EventHandler(this.InStoreToolStripMenuItem_Click);
            this.InStoreToolStripMenuItem.MouseEnter += new System.EventHandler(this.InStoreToolStripMenuItem_MouseEnter);
            this.InStoreToolStripMenuItem.MouseLeave += new System.EventHandler(this.InStoreToolStripMenuItem_MouseLeave);
            // 
            // QueryToolStripMenuItem
            // 
            this.QueryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.QueryStoredToolStripMenuItem});
            this.QueryToolStripMenuItem.Name = "QueryToolStripMenuItem";
            this.QueryToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.QueryToolStripMenuItem.Text = "查詢(&C)";
            this.QueryToolStripMenuItem.MouseEnter += new System.EventHandler(this.QueryToolStripMenuItem_MouseEnter);
            this.QueryToolStripMenuItem.MouseLeave += new System.EventHandler(this.QueryToolStripMenuItem_MouseLeave);
            // 
            // QueryStoredToolStripMenuItem
            // 
            this.QueryStoredToolStripMenuItem.Name = "QueryStoredToolStripMenuItem";
            this.QueryStoredToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.QueryStoredToolStripMenuItem.Text = "入庫查詢(&A)";
            this.QueryStoredToolStripMenuItem.Click += new System.EventHandler(this.QueryStoredToolStripMenuItem_Click);
            this.QueryStoredToolStripMenuItem.MouseEnter += new System.EventHandler(this.QueryStoredToolStripMenuItem_MouseEnter);
            this.QueryStoredToolStripMenuItem.MouseLeave += new System.EventHandler(this.QueryStoredToolStripMenuItem_MouseLeave);
            // 
            // WindowToolStripMenuItem
            // 
            this.WindowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CascadeToolStripMenuItem,
            this.HorizontalToolStripMenuItem,
            this.VerticalToolStripMenuItem});
            this.WindowToolStripMenuItem.Name = "WindowToolStripMenuItem";
            this.WindowToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.WindowToolStripMenuItem.Text = "窗口(&D)";
            // 
            // CascadeToolStripMenuItem
            // 
            this.CascadeToolStripMenuItem.Name = "CascadeToolStripMenuItem";
            this.CascadeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CascadeToolStripMenuItem.Text = "窗口層疊(&A)";
            this.CascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // HorizontalToolStripMenuItem
            // 
            this.HorizontalToolStripMenuItem.Name = "HorizontalToolStripMenuItem";
            this.HorizontalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.HorizontalToolStripMenuItem.Text = "水平平舖(&B)";
            this.HorizontalToolStripMenuItem.Click += new System.EventHandler(this.HorizontalToolStripMenuItem_Click);
            // 
            // VerticalToolStripMenuItem
            // 
            this.VerticalToolStripMenuItem.Name = "VerticalToolStripMenuItem";
            this.VerticalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.VerticalToolStripMenuItem.Text = "垂直平舖(&C)";
            this.VerticalToolStripMenuItem.Click += new System.EventHandler(this.VerticalToolStripMenuItem_Click);
            // 
            // SetupToolStripMenuItem
            // 
            this.SetupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResetPwdToolStripMenuItem});
            this.SetupToolStripMenuItem.Name = "SetupToolStripMenuItem";
            this.SetupToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.SetupToolStripMenuItem.Text = "設置(&E)";
            this.SetupToolStripMenuItem.MouseEnter += new System.EventHandler(this.SetupToolStripMenuItem_MouseEnter);
            this.SetupToolStripMenuItem.MouseLeave += new System.EventHandler(this.SetupToolStripMenuItem_MouseLeave);
            // 
            // ResetPwdToolStripMenuItem
            // 
            this.ResetPwdToolStripMenuItem.Name = "ResetPwdToolStripMenuItem";
            this.ResetPwdToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ResetPwdToolStripMenuItem.Text = "重設密碼(&A)";
            this.ResetPwdToolStripMenuItem.Click += new System.EventHandler(this.ResetPwdToolStripMenuItem_Click);
            this.ResetPwdToolStripMenuItem.MouseEnter += new System.EventHandler(this.ResetPwdToolStripMenuItem_MouseEnter);
            this.ResetPwdToolStripMenuItem.MouseLeave += new System.EventHandler(this.ResetPwdToolStripMenuItem_MouseLeave);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutThisToolStripMenuItem});
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.AboutToolStripMenuItem.Text = "關於(&F)";
            this.AboutToolStripMenuItem.MouseEnter += new System.EventHandler(this.AboutToolStripMenuItem_MouseEnter);
            this.AboutToolStripMenuItem.MouseLeave += new System.EventHandler(this.AboutToolStripMenuItem_MouseLeave);
            // 
            // AboutThisToolStripMenuItem
            // 
            this.AboutThisToolStripMenuItem.Name = "AboutThisToolStripMenuItem";
            this.AboutThisToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AboutThisToolStripMenuItem.Text = "關於本軟件(&A)";
            this.AboutThisToolStripMenuItem.Click += new System.EventHandler(this.AboutThisToolStripMenuItem_Click);
            this.AboutThisToolStripMenuItem.MouseEnter += new System.EventHandler(this.AboutThisToolStripMenuItem_MouseEnter);
            this.AboutThisToolStripMenuItem.MouseLeave += new System.EventHandler(this.AboutThisToolStripMenuItem_MouseLeave);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 566);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(846, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // BarcodeScrapToolStripMenuItem
            // 
            this.BarcodeScrapToolStripMenuItem.Name = "BarcodeScrapToolStripMenuItem";
            this.BarcodeScrapToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.BarcodeScrapToolStripMenuItem.Text = "條碼報廢";
            this.BarcodeScrapToolStripMenuItem.Click += new System.EventHandler(this.BarcodeScrapToolStripMenuItem_Click);
            // 
            // 經管ToolStripMenuItem
            // 
            this.經管ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BAControlToolStripMenuItem});
            this.經管ToolStripMenuItem.Name = "經管ToolStripMenuItem";
            this.經管ToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.經管ToolStripMenuItem.Text = "經管(&G)";
            // 
            // BAControlToolStripMenuItem
            // 
            this.BAControlToolStripMenuItem.Name = "BAControlToolStripMenuItem";
            this.BAControlToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.BAControlToolStripMenuItem.Text = "月結卡控";
            this.BAControlToolStripMenuItem.Click += new System.EventHandler(this.BAControlToolStripMenuItem_Click);
            // 
            // MainFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 588);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainFm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CPSscan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem ProductLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WOBCPrintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BCScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RepositoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QueryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutThisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResetPwdToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem InStoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QueryStoredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RepeatPrttoolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ContainerNumSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BarcodeScrapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 經管ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BAControlToolStripMenuItem;
    }
}