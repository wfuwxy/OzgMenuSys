namespace InformationDesk
{
    partial class OrderDayReportForm
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
            this.ReportList = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.TimeFromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.TimeToDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.BtnGetData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ReportList
            // 
            this.ReportList.Location = new System.Drawing.Point(12, 53);
            this.ReportList.Name = "ReportList";
            this.ReportList.Size = new System.Drawing.Size(580, 291);
            this.ReportList.TabIndex = 0;
            this.ReportList.UseCompatibleStateImageBehavior = false;
            this.ReportList.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "从";
            // 
            // TimeFromDateTimePicker
            // 
            this.TimeFromDateTimePicker.Location = new System.Drawing.Point(40, 16);
            this.TimeFromDateTimePicker.Name = "TimeFromDateTimePicker";
            this.TimeFromDateTimePicker.Size = new System.Drawing.Size(200, 21);
            this.TimeFromDateTimePicker.TabIndex = 2;
            this.TimeFromDateTimePicker.Value = new System.DateTime(2014, 6, 1, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "到";
            // 
            // TimeToDateTimePicker
            // 
            this.TimeToDateTimePicker.Location = new System.Drawing.Point(290, 17);
            this.TimeToDateTimePicker.Name = "TimeToDateTimePicker";
            this.TimeToDateTimePicker.Size = new System.Drawing.Size(200, 21);
            this.TimeToDateTimePicker.TabIndex = 4;
            this.TimeToDateTimePicker.Value = new System.DateTime(2014, 8, 1, 0, 0, 0, 0);
            // 
            // BtnGetData
            // 
            this.BtnGetData.Location = new System.Drawing.Point(508, 16);
            this.BtnGetData.Name = "BtnGetData";
            this.BtnGetData.Size = new System.Drawing.Size(75, 23);
            this.BtnGetData.TabIndex = 5;
            this.BtnGetData.Text = "查询";
            this.BtnGetData.UseVisualStyleBackColor = true;
            this.BtnGetData.Click += new System.EventHandler(this.BtnGetData_Click);
            // 
            // OrderDayReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 361);
            this.Controls.Add(this.BtnGetData);
            this.Controls.Add(this.TimeToDateTimePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TimeFromDateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ReportList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderDayReportForm";
            this.Text = "OrderDayReportForm";
            this.Load += new System.EventHandler(this.OrderDayReportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ReportList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker TimeFromDateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker TimeToDateTimePicker;
        private System.Windows.Forms.Button BtnGetData;
    }
}