﻿namespace BaiTap.DSLHoc
{
    partial class DSLHoc
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
            this.panelDSLH = new System.Windows.Forms.Panel();
            this.dataDSLH = new System.Windows.Forms.DataGridView();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnThemVoLop = new System.Windows.Forms.Button();
            this.btnNhapDiem = new System.Windows.Forms.Button();
            this.btnDS = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelDSLH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataDSLH)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDSLH
            // 
            this.panelDSLH.Controls.Add(this.dataDSLH);
            this.panelDSLH.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelDSLH.Location = new System.Drawing.Point(0, 0);
            this.panelDSLH.Name = "panelDSLH";
            this.panelDSLH.Size = new System.Drawing.Size(1065, 682);
            this.panelDSLH.TabIndex = 0;
            // 
            // dataDSLH
            // 
            this.dataDSLH.AllowUserToAddRows = false;
            this.dataDSLH.AllowUserToDeleteRows = false;
            this.dataDSLH.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataDSLH.ColumnHeadersHeight = 50;
            this.dataDSLH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataDSLH.Location = new System.Drawing.Point(0, 0);
            this.dataDSLH.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataDSLH.Name = "dataDSLH";
            this.dataDSLH.ReadOnly = true;
            this.dataDSLH.RowHeadersVisible = false;
            this.dataDSLH.RowHeadersWidth = 62;
            this.dataDSLH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataDSLH.Size = new System.Drawing.Size(1065, 682);
            this.dataDSLH.TabIndex = 1;
            this.dataDSLH.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnThem.Location = new System.Drawing.Point(3, 3);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(140, 66);
            this.btnThem.TabIndex = 1;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnEdit.Location = new System.Drawing.Point(3, 75);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(140, 66);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnDelete.Location = new System.Drawing.Point(3, 148);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 66);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Xoá";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnThoat.Location = new System.Drawing.Point(3, 437);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(140, 66);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnThemVoLop
            // 
            this.btnThemVoLop.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnThemVoLop.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemVoLop.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnThemVoLop.Location = new System.Drawing.Point(3, 220);
            this.btnThemVoLop.Name = "btnThemVoLop";
            this.btnThemVoLop.Size = new System.Drawing.Size(140, 66);
            this.btnThemVoLop.TabIndex = 7;
            this.btnThemVoLop.Text = "Thêm Sinh Viên Vào Lớp";
            this.btnThemVoLop.UseVisualStyleBackColor = false;
            this.btnThemVoLop.Click += new System.EventHandler(this.btnThemVoLop_Click);
            // 
            // btnNhapDiem
            // 
            this.btnNhapDiem.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnNhapDiem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhapDiem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnNhapDiem.Location = new System.Drawing.Point(3, 365);
            this.btnNhapDiem.Name = "btnNhapDiem";
            this.btnNhapDiem.Size = new System.Drawing.Size(140, 66);
            this.btnNhapDiem.TabIndex = 8;
            this.btnNhapDiem.Text = "Nhập điểm cho sinh viên";
            this.btnNhapDiem.UseVisualStyleBackColor = false;
            this.btnNhapDiem.Click += new System.EventHandler(this.btnNhapDiem_Click);
            // 
            // btnDS
            // 
            this.btnDS.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnDS.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDS.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnDS.Location = new System.Drawing.Point(3, 292);
            this.btnDS.Name = "btnDS";
            this.btnDS.Size = new System.Drawing.Size(140, 66);
            this.btnDS.TabIndex = 9;
            this.btnDS.Text = "Xem Sinh Viên Trong Lớp";
            this.btnDS.UseVisualStyleBackColor = false;
            this.btnDS.Click += new System.EventHandler(this.btnDS_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnThem);
            this.panel1.Controls.Add(this.btnThoat);
            this.panel1.Controls.Add(this.btnNhapDiem);
            this.panel1.Controls.Add(this.btnDS);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnThemVoLop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1073, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(147, 682);
            this.panel1.TabIndex = 10;
            // 
            // DSLHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1220, 682);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelDSLH);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DSLHoc";
            this.Text = "DSLHoc";
            this.Load += new System.EventHandler(this.DSLHoc_Load);
            this.panelDSLH.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataDSLH)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDSLH;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnThemVoLop;
        private System.Windows.Forms.Button btnNhapDiem;
        private System.Windows.Forms.Button btnDS;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataDSLH;
    }
}