namespace QLSV.ChuyenNganh
{
    partial class frmChiTiet
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenCN = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblSoLuongSinhVien = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(298, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(373, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "THÔNG TIN CHI TIẾT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(170, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 22);
            this.label3.TabIndex = 17;
            this.label3.Text = "Tên chuyên ngành\r\n";
            // 
            // txtTenCN
            // 
            this.txtTenCN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenCN.Location = new System.Drawing.Point(369, 124);
            this.txtTenCN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTenCN.Multiline = true;
            this.txtTenCN.Name = "txtTenCN";
            this.txtTenCN.ReadOnly = true;
            this.txtTenCN.Size = new System.Drawing.Size(420, 38);
            this.txtTenCN.TabIndex = 16;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(39, 241);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(863, 552);
            this.dataGridView1.TabIndex = 18;
            // 
            // lblSoLuongSinhVien
            // 
            this.lblSoLuongSinhVien.AutoSize = true;
            this.lblSoLuongSinhVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuongSinhVien.Location = new System.Drawing.Point(35, 183);
            this.lblSoLuongSinhVien.Name = "lblSoLuongSinhVien";
            this.lblSoLuongSinhVien.Size = new System.Drawing.Size(58, 22);
            this.lblSoLuongSinhVien.TabIndex = 19;
            this.lblSoLuongSinhVien.Text = "label2";
            // 
            // frmChiTiet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 840);
            this.Controls.Add(this.lblSoLuongSinhVien);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTenCN);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(965, 896);
            this.Name = "frmChiTiet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmChiTiet";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmChiTiet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenCN;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblSoLuongSinhVien;
    }
}