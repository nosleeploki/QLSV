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
    public partial class frmChiTiet : Form
    {
        public frmChiTiet()
        {
            InitializeComponent();
        }
        private string _maHocKy;
        private string _tenHocKy;
        private string _nam;

        public frmChiTiet(string maHocKy, string tenHocKy, string nam)
        {
            InitializeComponent();
            _maHocKy = maHocKy;
            _tenHocKy = tenHocKy;
            _nam = nam;

            // Hiển thị thông tin học kỳ
            txtTenHK.Text = _tenHocKy;
            txtNam.Text = _nam;

            // Tải dữ liệu danh sách lớp
            LoadDanhSachLop();
        }

        private void LoadDanhSachLop()
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = @"
                SELECT lh.MaLop, lh.TenLop, mh.TenMon
                FROM Lop_Hoc lh
                JOIN Mon_Hoc mh ON lh.MaMon = mh.MaMon
                WHERE lh.MaHocKy = @MaHocKy";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@MaHocKy", _maHocKy);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
