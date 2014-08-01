namespace InformationDesk
{
    partial class ClientForm
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
            this.ClientList = new System.Windows.Forms.ListView();
            this.ClientListContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TextIP = new System.Windows.Forms.TextBox();
            this.TextName = new System.Windows.Forms.TextBox();
            this.ChkIsAdmin0 = new System.Windows.Forms.RadioButton();
            this.ChkIsAdmin1 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.ClientListContextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ClientList
            // 
            this.ClientList.ContextMenuStrip = this.ClientListContextMenuStrip;
            this.ClientList.FullRowSelect = true;
            this.ClientList.GridLines = true;
            this.ClientList.Location = new System.Drawing.Point(12, 46);
            this.ClientList.MultiSelect = false;
            this.ClientList.Name = "ClientList";
            this.ClientList.Size = new System.Drawing.Size(741, 358);
            this.ClientList.TabIndex = 0;
            this.ClientList.UseCompatibleStateImageBehavior = false;
            this.ClientList.View = System.Windows.Forms.View.Details;
            this.ClientList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ClientList_MouseClick);
            // 
            // ClientListContextMenuStrip
            // 
            this.ClientListContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem});
            this.ClientListContextMenuStrip.Name = "ClientListContextMenuStrip";
            this.ClientListContextMenuStrip.Size = new System.Drawing.Size(153, 48);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DeleteToolStripMenuItem.Text = "删除";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(496, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "类别";
            // 
            // TextIP
            // 
            this.TextIP.Location = new System.Drawing.Point(47, 12);
            this.TextIP.Name = "TextIP";
            this.TextIP.Size = new System.Drawing.Size(190, 21);
            this.TextIP.TabIndex = 4;
            // 
            // TextName
            // 
            this.TextName.Location = new System.Drawing.Point(289, 12);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(190, 21);
            this.TextName.TabIndex = 5;
            // 
            // ChkIsAdmin0
            // 
            this.ChkIsAdmin0.AutoSize = true;
            this.ChkIsAdmin0.Checked = true;
            this.ChkIsAdmin0.Location = new System.Drawing.Point(3, 6);
            this.ChkIsAdmin0.Name = "ChkIsAdmin0";
            this.ChkIsAdmin0.Size = new System.Drawing.Size(47, 16);
            this.ChkIsAdmin0.TabIndex = 6;
            this.ChkIsAdmin0.TabStop = true;
            this.ChkIsAdmin0.Text = "一般";
            this.ChkIsAdmin0.UseVisualStyleBackColor = true;
            // 
            // ChkIsAdmin1
            // 
            this.ChkIsAdmin1.AutoSize = true;
            this.ChkIsAdmin1.Location = new System.Drawing.Point(56, 6);
            this.ChkIsAdmin1.Name = "ChkIsAdmin1";
            this.ChkIsAdmin1.Size = new System.Drawing.Size(59, 16);
            this.ChkIsAdmin1.TabIndex = 7;
            this.ChkIsAdmin1.Text = "服务台";
            this.ChkIsAdmin1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ChkIsAdmin1);
            this.panel1.Controls.Add(this.ChkIsAdmin0);
            this.panel1.Location = new System.Drawing.Point(531, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 28);
            this.panel1.TabIndex = 8;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(665, 15);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.BtnAdd.TabIndex = 9;
            this.BtnAdd.Text = "新增";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 416);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextName);
            this.Controls.Add(this.TextIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClientList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ClientListContextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ClientList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextIP;
        private System.Windows.Forms.TextBox TextName;
        private System.Windows.Forms.RadioButton ChkIsAdmin0;
        private System.Windows.Forms.RadioButton ChkIsAdmin1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.ContextMenuStrip ClientListContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
    }
}