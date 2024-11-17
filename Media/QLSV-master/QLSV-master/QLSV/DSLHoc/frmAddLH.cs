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

namespace BaiTap.DSLHoc
{
    public partial class frmAddLH : Form
    {
        private string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLSV;Integrated Security=True";
        public event EventHandler DataUpdated;
        public frmAddLH()
        {
            InitializeComponent();
            LoadMonHoc();
            LoadHocKyNam();
        }
        // Load danh sách môn học vào ComboBox cmbMonHoc
        private void LoadMonHoc()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaMon, TenMon FROM Mon_Hoc WHERE DaXoa = 0";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cboMonHoc.DataSource = dt;
                    cboMonHoc.DisplayMember = "TenMon";
                    cboMonHoc.ValueMember = "MaMon";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu môn học: " + ex.Message);
                }
            }
        }

        // Load danh sách học kỳ và năm vào ComboBox cmbHocKyNam
        private void LoadHocKyNam()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaHocKy, TenHocKy, Nam FROM Hoc_Ky WHERE DaXoa = 0";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Tạo một cột mới để lưu kết hợp giữa tên học kỳ và năm
                    DataColumn colHocKyNam = new DataColumn("HocKyNam", typeof(string));
                    dt.Columns.Add(colHocKyNam);

                    // Lặp qua các dòng và tạo chuỗi kết hợp cho học kỳ và năm
                    foreach (DataRow row in dt.Rows)
                    {
                        row["HocKyNam"] = row["TenHocKy"].ToString() + " (" + row["Nam"].ToString() + ")";
                    }

                    // Đặt nguồn dữ liệu cho ComboBox
                    cboHocKyNam.DataSource = dt;
                    cboHocKyNam.DisplayMember = "HocKyNam";  // Hiển thị học kỳ + năm
                    cboHocKyNam.ValueMember = "MaHocKy";  // Lưu trữ mã học kỳ
                    cboHocKyNam.Text = string.Empty;  // Đặt giá trị ban đầu cho ComboBox
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu học kỳ: " + ex.Message);
                }
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                int maLop = GetNextMaLop();
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Lop_Hoc (MaLop, MaMon, MaHocKy, TenLop, DaXoa) VALUES (@MaLop, @MaMon, @MaHocKy, @TenLop, 0)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Thêm các tham số cho câu lệnh SQL
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    cmd.Parameters.AddWithValue("@MaMon", cboMonHoc.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaHocKy", cboHocKyNam.SelectedValue);
                    cmd.Parameters.AddWithValue("@TenLop", txtTenLop.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm lớp học thành công!");

                    // Đóng form và cập nhật danh sách lớp học nếu cần
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm lớp học: " + ex.Message);
                }
            }
        }
        private int GetNextMaLop()
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT ISNULL(MAX(MaLop), 0) + 1 FROM Lop_Hoc";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddLH_Load(object sender, EventArgs e)
        {

        }
    }
}
