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

namespace QLSV.ChuyenNganh
{
    public partial class frmChiTiet : Form
    {
        private int _maChuyenNganh; // ID của chuyên ngành được chọn
        private string _tenChuyenNganh; // Tên chuyên ngành được chọn
        private string _connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
        public frmChiTiet()
        {
            InitializeComponent();
        }
        public frmChiTiet(int maChuyenNganh, string tenChuyenNganh)
        {
            InitializeComponent();
            _maChuyenNganh = maChuyenNganh;
            _tenChuyenNganh = tenChuyenNganh;

            txtTenCN.Text = _tenChuyenNganh;

            // Load dữ liệu khi mở form
            LoadThongTinChuyenNganh();
            LoadDanhSachSinhVien();
        }
        private void LoadThongTinChuyenNganh()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) AS SoLuongSinhVien FROM Sinh_Vien WHERE MaChuyenNganh = @MaChuyenNganh";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaChuyenNganh", _maChuyenNganh);

                    try
                    {
                        connection.Open();
                        int soLuongSinhVien = (int)command.ExecuteScalar();
                        lblSoLuongSinhVien.Text = $"Số lượng sinh viên: {soLuongSinhVien}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void LoadDanhSachSinhVien()
        {
            string query = "SELECT MaSinhVien, Ho + ' ' + Ten AS HoTen, NgaySinh, GioiTinh " +
                           "FROM Sinh_Vien WHERE MaChuyenNganh = @MaChuyenNganh";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@MaChuyenNganh", _maChuyenNganh);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }
        private void frmChiTiet_Load(object sender, EventArgs e)
        {

        }
    }
}
