namespace InformationDesk
{
    partial class MenuForm
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
            this.MenuDataList = new System.Windows.Forms.ListView();
            this.MenuClassList = new System.Windows.Forms.ComboBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.LabMenuClass = new System.Windows.Forms.Label();
            this.MenuDataListContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDataListContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuDataList
            // 
            this.MenuDataList.ContextMenuStrip = this.MenuDataListContextMenuStrip;
            this.MenuDataList.FullRowSelect = true;
            this.MenuDataList.GridLines = true;
            this.MenuDataList.Location = new System.Drawing.Point(12, 39);
            this.MenuDataList.MultiSelect = false;
            this.MenuDataList.Name = "MenuDataList";
            this.MenuDataList.Size = new System.Drawing.Size(560, 360);
            this.MenuDataList.TabIndex = 0;
            this.MenuDataList.UseCompatibleStateImageBehavior = false;
            this.MenuDataList.View = System.Windows.Forms.View.Details;
            this.MenuDataList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuDataList_MouseClick);
            // 
            // MenuClassList
            // 
            this.MenuClassList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MenuClassList.FormattingEnabled = true;
            this.MenuClassList.Location = new System.Drawing.Point(73, 13);
            this.MenuClassList.Name = "MenuClassList";
            this.MenuClassList.Size = new System.Drawing.Size(408, 20);
            this.MenuClassList.TabIndex = 1;
            this.MenuClassList.SelectedIndexChanged += new System.EventHandler(this.MenuClassList_SelectedIndexChanged);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(487, 10);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.BtnAdd.TabIndex = 2;
            this.BtnAdd.Text = "button1";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // LabMenuClass
            // 
            this.LabMenuClass.AutoSize = true;
            this.LabMenuClass.Location = new System.Drawing.Point(12, 21);
            this.LabMenuClass.Name = "LabMenuClass";
            this.LabMenuClass.Size = new System.Drawing.Size(41, 12);
            this.LabMenuClass.TabIndex = 3;
            this.LabMenuClass.Text = "label1";
            // 
            // MenuDataListContextMenuStrip
            // 
            this.MenuDataListContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem});
            this.MenuDataListContextMenuStrip.Name = "MenuDataListContextMenuStrip";
            this.MenuDataListContextMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.DeleteToolStripMenuItem.Text = "删除";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.LabMenuClass);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.MenuClassList);
            this.Controls.Add(this.MenuDataList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.MenuDataListContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView MenuDataList;
        private System.Windows.Forms.ComboBox MenuClassList;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Label LabMenuClass;
        private System.Windows.Forms.ContextMenuStrip MenuDataListContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
    }
}