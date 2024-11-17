namespace QLSV.DSLHoc
{
    partial class AddDSSVvaoLopHoc
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboChonLH = new System.Windows.Forms.ComboBox();
            this.cboCSV = new System.Windows.Forms.ComboBox();
            this.btnThemSV = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(125, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thêm Sinh Viên Vào Lớp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(92, 126);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Chọn Lớp Học ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(91, 174);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Chọn Sinh Viên";
            // 
            // cboChonLH
            // 
            this.cboChonLH.FormattingEnabled = true;
            this.cboChonLH.Location = new System.Drawing.Point(199, 124);
            this.cboChonLH.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboChonLH.Name = "cboChonLH";
            this.cboChonLH.Size = new System.Drawing.Size(208, 21);
            this.cboChonLH.TabIndex = 3;
            // 
            // cboCSV
            // 
            this.cboCSV.FormattingEnabled = true;
            this.cboCSV.Location = new System.Drawing.Point(199, 175);
            this.cboCSV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboCSV.Name = "cboCSV";
            this.cboCSV.Size = new System.Drawing.Size(208, 21);
            this.cboCSV.TabIndex = 4;
            // 
            // btnThemSV
            // 
            this.btnThemSV.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnThemSV.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemSV.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnThemSV.Location = new System.Drawing.Point(102, 252);
            this.btnThemSV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnThemSV.Name = "btnThemSV";
            this.btnThemSV.Size = new System.Drawing.Size(111, 51);
            this.btnThemSV.TabIndex = 5;
            this.btnThemSV.Text = "Thêm Sinh Viên Vào Lớp";
            this.btnThemSV.UseVisualStyleBackColor = false;
            this.btnThemSV.Click += new System.EventHandler(this.btnThemSV_Click);
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnDong.Location = new System.Drawing.Point(285, 252);
            this.btnDong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 51);
            this.btnDong.TabIndex = 6;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // AddDSSVvaoLopHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(539, 350);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnThemSV);
            this.Controls.Add(this.cboCSV);
            this.Controls.Add(this.cboChonLH);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AddDSSVvaoLopHoc";
            this.Text = "AddDSSVvaoLopHoc";
            this.Load += new System.EventHandler(this.AddDSSVvaoLopHoc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboChonLH;
        private System.Windows.Forms.ComboBox cboCSV;
        private System.Windows.Forms.Button btnThemSV;
        private System.Windows.Forms.Button btnDong;
    }
}