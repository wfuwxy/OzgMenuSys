namespace InformationDesk
{
    partial class MainForm
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
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClientListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OrderReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlineList = new System.Windows.Forms.ListView();
            this.ClientItemMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.MenuToolStripMenuItem,
            this.ClientToolStripMenuItem,
            this.OrderToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.FileToolStripMenuItem.Text = "文件(F)";
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // MenuToolStripMenuItem
            // 
            this.MenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuClassToolStripMenuItem,
            this.MenuDataToolStripMenuItem});
            this.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem";
            this.MenuToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            this.MenuToolStripMenuItem.Text = "菜单(M)";
            // 
            // MenuClassToolStripMenuItem
            // 
            this.MenuClassToolStripMenuItem.Name = "MenuClassToolStripMenuItem";
            this.MenuClassToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.MenuClassToolStripMenuItem.Text = "菜单分类";
            this.MenuClassToolStripMenuItem.Click += new System.EventHandler(this.MenuClassToolStripMenuItem_Click);
            // 
            // MenuDataToolStripMenuItem
            // 
            this.MenuDataToolStripMenuItem.Name = "MenuDataToolStripMenuItem";
            this.MenuDataToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.MenuDataToolStripMenuItem.Text = "菜单数据";
            this.MenuDataToolStripMenuItem.Click += new System.EventHandler(this.MenuDataToolStripMenuItem_Click);
            // 
            // ClientToolStripMenuItem
            // 
            this.ClientToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClientListToolStripMenuItem});
            this.ClientToolStripMenuItem.Name = "ClientToolStripMenuItem";
            this.ClientToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            this.ClientToolStripMenuItem.Text = "客户端(C)";
            // 
            // ClientListToolStripMenuItem
            // 
            this.ClientListToolStripMenuItem.Name = "ClientListToolStripMenuItem";
            this.ClientListToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ClientListToolStripMenuItem.Text = "管理";
            this.ClientListToolStripMenuItem.Click += new System.EventHandler(this.ClientListToolStripMenuItem_Click);
            // 
            // OrderToolStripMenuItem
            // 
            this.OrderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OrderReportToolStripMenuItem});
            this.OrderToolStripMenuItem.Name = "OrderToolStripMenuItem";
            this.OrderToolStripMenuItem.Size = new System.Drawing.Size(62, 21);
            this.OrderToolStripMenuItem.Text = "订单(O)";
            // 
            // OrderReportToolStripMenuItem
            // 
            this.OrderReportToolStripMenuItem.Name = "OrderReportToolStripMenuItem";
            this.OrderReportToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.OrderReportToolStripMenuItem.Text = "订单日销售报表";
            this.OrderReportToolStripMenuItem.Click += new System.EventHandler(this.OrderReportToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.HelpToolStripMenuItem.Text = "帮助(H)";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.AboutToolStripMenuItem.Text = "关于 Ozg点餐系统";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // OnlineList
            // 
            this.OnlineList.FullRowSelect = true;
            this.OnlineList.GridLines = true;
            this.OnlineList.Location = new System.Drawing.Point(13, 29);
            this.OnlineList.MultiSelect = false;
            this.OnlineList.Name = "OnlineList";
            this.OnlineList.Size = new System.Drawing.Size(609, 350);
            this.OnlineList.TabIndex = 1;
            this.OnlineList.UseCompatibleStateImageBehavior = false;
            this.OnlineList.View = System.Windows.Forms.View.Details;
            this.OnlineList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnlineList_MouseClick);
            // 
            // ClientItemMenuStrip
            // 
            this.ClientItemMenuStrip.Name = "ClientItemMenuStrip";
            this.ClientItemMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(634, 391);
            this.Controls.Add(this.OnlineList);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClientListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OrderReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ListView OnlineList;
        private System.Windows.Forms.ContextMenuStrip ClientItemMenuStrip;
    }
}