namespace InformationDesk
{
    partial class MenuAddForm
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
            this.BtnAdd = new System.Windows.Forms.Button();
            this.TextBigImage = new System.Windows.Forms.TextBox();
            this.TextPrice = new System.Windows.Forms.TextBox();
            this.TextName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnSelectBigImage = new System.Windows.Forms.Button();
            this.BtnViewBigImage = new System.Windows.Forms.Button();
            this.MenuClassList = new System.Windows.Forms.ComboBox();
            this.BigImageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(176, 164);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.BtnAdd.TabIndex = 8;
            this.BtnAdd.Text = "新增";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // TextBigImage
            // 
            this.TextBigImage.Location = new System.Drawing.Point(78, 122);
            this.TextBigImage.Name = "TextBigImage";
            this.TextBigImage.ReadOnly = true;
            this.TextBigImage.Size = new System.Drawing.Size(199, 21);
            this.TextBigImage.TabIndex = 7;
            // 
            // TextPrice
            // 
            this.TextPrice.Location = new System.Drawing.Point(78, 54);
            this.TextPrice.Name = "TextPrice";
            this.TextPrice.Size = new System.Drawing.Size(199, 21);
            this.TextPrice.TabIndex = 5;
            // 
            // TextName
            // 
            this.TextName.Location = new System.Drawing.Point(78, 21);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(199, 21);
            this.TextName.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "图片";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "分类";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "价格";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            // 
            // BtnSelectBigImage
            // 
            this.BtnSelectBigImage.Location = new System.Drawing.Point(283, 120);
            this.BtnSelectBigImage.Name = "BtnSelectBigImage";
            this.BtnSelectBigImage.Size = new System.Drawing.Size(45, 23);
            this.BtnSelectBigImage.TabIndex = 9;
            this.BtnSelectBigImage.Text = "选择";
            this.BtnSelectBigImage.UseVisualStyleBackColor = true;
            this.BtnSelectBigImage.Click += new System.EventHandler(this.BtnSelectBigImage_Click);
            // 
            // BtnViewBigImage
            // 
            this.BtnViewBigImage.Location = new System.Drawing.Point(334, 120);
            this.BtnViewBigImage.Name = "BtnViewBigImage";
            this.BtnViewBigImage.Size = new System.Drawing.Size(46, 23);
            this.BtnViewBigImage.TabIndex = 10;
            this.BtnViewBigImage.Text = "查看";
            this.BtnViewBigImage.UseVisualStyleBackColor = true;
            this.BtnViewBigImage.Click += new System.EventHandler(this.BtnViewBigImage_Click);
            // 
            // MenuClassList
            // 
            this.MenuClassList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MenuClassList.FormattingEnabled = true;
            this.MenuClassList.Location = new System.Drawing.Point(78, 88);
            this.MenuClassList.Name = "MenuClassList";
            this.MenuClassList.Size = new System.Drawing.Size(199, 20);
            this.MenuClassList.TabIndex = 11;
            // 
            // BigImageOpenFileDialog
            // 
            this.BigImageOpenFileDialog.Filter = "jpg文件|*.jpg|jpeg文件|*.jpeg";
            this.BigImageOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.BigImageOpenFileDialog_FileOk);
            // 
            // MenuAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 199);
            this.Controls.Add(this.MenuClassList);
            this.Controls.Add(this.BtnViewBigImage);
            this.Controls.Add(this.BtnSelectBigImage);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.TextBigImage);
            this.Controls.Add(this.TextPrice);
            this.Controls.Add(this.TextName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuAddForm";
            this.Text = "MenuAddForm";
            this.Load += new System.EventHandler(this.MenuAddForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextName;
        private System.Windows.Forms.TextBox TextPrice;
        private System.Windows.Forms.TextBox TextBigImage;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnSelectBigImage;
        private System.Windows.Forms.Button BtnViewBigImage;
        private System.Windows.Forms.ComboBox MenuClassList;
        private System.Windows.Forms.OpenFileDialog BigImageOpenFileDialog;
    }
}