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

namespace QLSV.HocKy
{
    public partial class frmEdit : Form
    {
        private string _maHocKy;
        public frmEdit()
        {
            InitializeComponent();
        }
        public frmEdit(string maHocKy, string tenHocKy, string nam)
        {
            InitializeComponent();
            _maHocKy = maHocKy;

            // Hiển thị dữ liệu ban đầu
            txtMa.Text = _maHocKy;
            txtTenHK.Text = tenHocKy;
            txtNam.Text = nam;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "UPDATE Hoc_Ky SET TenHocKy = @TenHocKy, Nam = @Nam WHERE MaHocKy = @MaHocKy";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaHocKy", _maHocKy);
                    command.Parameters.AddWithValue("@TenHocKy", txtTenHK.Text);
                    command.Parameters.AddWithValue("@Nam", txtNam.Text);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Đóng form sau khi sửa
                    }
                    else
                    {
                        MessageBox.Show("Không có gì để sửa hoặc mã học kỳ không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
    }
}
