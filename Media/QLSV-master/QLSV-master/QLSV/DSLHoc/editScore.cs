using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV.DSLHoc
{
    public partial class editScore : Form
    {
        private int maSinhVien;
        private int maLoaiDiem;
        private double diemHienTai;
        public double NewScore { get; set; }

        public editScore(int maSinhVien, int maLoaiDiem, double diemHienTai)
        {
            InitializeComponent();
            this.maSinhVien = maSinhVien;
            this.maLoaiDiem = maLoaiDiem;
            this.diemHienTai = diemHienTai;

            // Hiển thị thông tin trên form
            txtGiaTriDiem.Text = diemHienTai.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtGiaTriDiem.Text, out double newScore))
            {
                this.NewScore = newScore;
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            else
            {
                MessageBox.Show("Điểm nhập không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editScore_Load(object sender, EventArgs e)
        {
        }
    }
}
