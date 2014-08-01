namespace InformationDesk
{
    partial class MenuClassForm
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
            this.TextName = new System.Windows.Forms.TextBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.MenuClassList = new System.Windows.Forms.ComboBox();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextName
            // 
            this.TextName.Location = new System.Drawing.Point(47, 27);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(150, 21);
            this.TextName.TabIndex = 2;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(211, 26);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.BtnAdd.TabIndex = 3;
            this.BtnAdd.Text = "button1";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // MenuClassList
            // 
            this.MenuClassList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MenuClassList.FormattingEnabled = true;
            this.MenuClassList.Location = new System.Drawing.Point(47, 67);
            this.MenuClassList.Name = "MenuClassList";
            this.MenuClassList.Size = new System.Drawing.Size(150, 20);
            this.MenuClassList.TabIndex = 4;
            this.MenuClassList.SelectedIndexChanged += new System.EventHandler(this.MenuClassList_SelectedIndexChanged);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(211, 63);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 23);
            this.BtnDelete.TabIndex = 5;
            this.BtnDelete.Text = "button2";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // MenuClassForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 118);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.MenuClassList);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.TextName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuClassForm";
            this.Text = "MenuClassForm";
            this.Load += new System.EventHandler(this.MenuClassForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextName;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.ComboBox MenuClassList;
        private System.Windows.Forms.Button BtnDelete;
    }
}