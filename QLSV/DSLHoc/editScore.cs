using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV.DSLHoc
{
    public partial class editScore : Form
    {
        public int MaLoaiDiem { get; set; }
        public double GiaTriDiem { get; set; }
        public editScore(int maLoaiDiem, double giaTriDiem)
        {
            InitializeComponent();
            MaLoaiDiem = maLoaiDiem;
            GiaTriDiem = giaTriDiem;
        }

        private void EditScoreForm_Load(object sender, EventArgs e)
        {
            // Hiển thị dữ liệu hiện tại lên form
            txtGiaTriDiem.Text = GiaTriDiem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Cập nhật giá trị điểm mới
            if (double.TryParse(txtGiaTriDiem.Text, out double newScore))
            {
                GiaTriDiem = newScore;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Điểm nhập vào không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
