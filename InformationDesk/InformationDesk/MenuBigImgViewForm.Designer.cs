namespace InformationDesk
{
    partial class MenuBigImgViewForm
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
            this.BigImagePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BigImagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // BigImagePictureBox
            // 
            this.BigImagePictureBox.Location = new System.Drawing.Point(0, 0);
            this.BigImagePictureBox.Name = "BigImagePictureBox";
            this.BigImagePictureBox.Size = new System.Drawing.Size(1136, 908);
            this.BigImagePictureBox.TabIndex = 0;
            this.BigImagePictureBox.TabStop = false;
            // 
            // MenuBigImgViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 908);
            this.Controls.Add(this.BigImagePictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuBigImgViewForm";
            this.Text = "MenuBigImgViewForm";
            this.Load += new System.EventHandler(this.MenuBigImgViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BigImagePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BigImagePictureBox;
    }
}