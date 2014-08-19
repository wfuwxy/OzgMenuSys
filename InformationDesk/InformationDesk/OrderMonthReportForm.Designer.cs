namespace InformationDesk
{
    partial class OrderMonthReportForm
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
            this.BtnGetData = new System.Windows.Forms.Button();
            this.TimeToDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.ReportList = new System.Windows.Forms.ListView();
            this.TimeFromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // BtnGetData
            // 
            this.BtnGetData.Location = new System.Drawing.Point(513, 16);
            this.BtnGetData.Name = "BtnGetData";
            this.BtnGetData.Size = new System.Drawing.Size(75, 23);
            this.BtnGetData.TabIndex = 10;
            this.BtnGetData.Text = "查询";
            this.BtnGetData.UseVisualStyleBackColor = true;
            this.BtnGetData.Click += new System.EventHandler(this.BtnGetData_Click);
            // 
            // TimeToDateTimePicker
            // 
            this.TimeToDateTimePicker.CustomFormat = "yyyy-MM";
            this.TimeToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TimeToDateTimePicker.Location = new System.Drawing.Point(295, 17);
            this.TimeToDateTimePicker.Name = "TimeToDateTimePicker";
            this.TimeToDateTimePicker.ShowUpDown = true;
            this.TimeToDateTimePicker.Size = new System.Drawing.Size(200, 21);
            this.TimeToDateTimePicker.TabIndex = 9;
            this.TimeToDateTimePicker.Value = new System.DateTime(2014, 8, 1, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(261, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "到";
            // 
            // ReportList
            // 
            this.ReportList.Location = new System.Drawing.Point(17, 53);
            this.ReportList.Name = "ReportList";
            this.ReportList.Size = new System.Drawing.Size(580, 291);
            this.ReportList.TabIndex = 6;
            this.ReportList.UseCompatibleStateImageBehavior = false;
            this.ReportList.View = System.Windows.Forms.View.Details;
            // 
            // TimeFromDateTimePicker
            // 
            this.TimeFromDateTimePicker.CustomFormat = "yyyy-MM";
            this.TimeFromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TimeFromDateTimePicker.Location = new System.Drawing.Point(45, 16);
            this.TimeFromDateTimePicker.Name = "TimeFromDateTimePicker";
            this.TimeFromDateTimePicker.ShowUpDown = true;
            this.TimeFromDateTimePicker.Size = new System.Drawing.Size(200, 21);
            this.TimeFromDateTimePicker.TabIndex = 7;
            this.TimeFromDateTimePicker.Value = new System.DateTime(2014, 6, 1, 0, 0, 0, 0);
            // 
            // OrderMonthReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 361);
            this.Controls.Add(this.BtnGetData);
            this.Controls.Add(this.TimeToDateTimePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TimeFromDateTimePicker);
            this.Controls.Add(this.ReportList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderMonthReportForm";
            this.Text = "OrderMonthReportForm";
            this.Load += new System.EventHandler(this.OrderMonthReportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnGetData;
        private System.Windows.Forms.DateTimePicker TimeToDateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView ReportList;
        private System.Windows.Forms.DateTimePicker TimeFromDateTimePicker;
    }
}