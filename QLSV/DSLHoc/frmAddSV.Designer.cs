namespace BaiTap.DSLHoc
{
    partial class frmAddSV
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
            this.cboCSV = new System.Windows.Forms.ComboBox();
            this.btnThemSV = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.txtTenLop = new System.Windows.Forms.TextBox();
            this.txtMonHoc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(188, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(429, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thêm Sinh Viên Vào Lớp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(138, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên Lớp Học ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(136, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Chọn Sinh Viên";
            // 
            // cboCSV
            // 
            this.cboCSV.FormattingEnabled = true;
            this.cboCSV.Location = new System.Drawing.Point(298, 312);
            this.cboCSV.Name = "cboCSV";
            this.cboCSV.Size = new System.Drawing.Size(310, 28);
            this.cboCSV.TabIndex = 4;
            // 
            // btnThemSV
            // 
            this.btnThemSV.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnThemSV.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemSV.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnThemSV.Location = new System.Drawing.Point(153, 388);
            this.btnThemSV.Name = "btnThemSV";
            this.btnThemSV.Size = new System.Drawing.Size(166, 78);
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
            this.btnDong.Location = new System.Drawing.Point(428, 388);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(180, 78);
            this.btnDong.TabIndex = 6;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // txtTenLop
            // 
            this.txtTenLop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenLop.Location = new System.Drawing.Point(298, 237);
            this.txtTenLop.Name = "txtTenLop";
            this.txtTenLop.ReadOnly = true;
            this.txtTenLop.Size = new System.Drawing.Size(310, 28);
            this.txtTenLop.TabIndex = 7;
            // 
            // txtMonHoc
            // 
            this.txtMonHoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonHoc.Location = new System.Drawing.Point(298, 165);
            this.txtMonHoc.Name = "txtMonHoc";
            this.txtMonHoc.ReadOnly = true;
            this.txtMonHoc.Size = new System.Drawing.Size(310, 28);
            this.txtMonHoc.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(138, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tên Môn Học ";
            // 
            // AddDSSVvaoLopHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(808, 538);
            this.Controls.Add(this.txtMonHoc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTenLop);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnThemSV);
            this.Controls.Add(this.cboCSV);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddDSSVvaoLopHoc";
            this.Text = "AddDSSVvaoLopHoc";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCSV;
        private System.Windows.Forms.Button btnThemSV;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.TextBox txtTenLop;
        private System.Windows.Forms.TextBox txtMonHoc;
        private System.Windows.Forms.Label label4;
    }
}