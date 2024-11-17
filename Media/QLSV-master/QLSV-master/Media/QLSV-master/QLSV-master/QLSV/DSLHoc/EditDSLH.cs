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
    public partial class EditDSLH : Form
    {
        private int MaLop;
        public event EventHandler DataUpdated;

        // Constructor nhận MaLop
        public EditDSLH(int maLop)
        {
            InitializeComponent();
            this.MaLop = maLop;
            LoadMaMon();
            LoadHocKy();
            LoadLopHocData();
        }

        private void LoadMaMon()
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT MaMon, TenMon FROM Mon_Hoc WHERE DaXoa = 0";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cboMaMon.DataSource = dataTable;
                    cboMaMon.DisplayMember = "TenMon";
                    cboMaMon.ValueMember = "MaMon";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách môn học: " + ex.Message);
                }
            }
        }

        private void LoadHocKy()
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT MaHocKy, TenHocKy FROM Hoc_Ky WHERE DaXoa = 0";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    cboMaHocKi.DataSource = dataTable;
                    cboMaHocKi.DisplayMember = "TenHocKy";
                    cboMaHocKi.ValueMember = "MaHocKy";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách học kỳ: " + ex.Message);
                }
            }
        }

        private void LoadLopHocData()
        {
            // Tải thông tin chi tiết của lớp học từ cơ sở dữ liệu để hiển thị lên form
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT * FROM Lop_Hoc WHERE MaLop = @MaLop";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaLop", MaLop);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Hiển thị dữ liệu lên form
                                cboMaMon.SelectedValue = reader["MaMon"];
                                cboMaHocKi.SelectedValue = reader["MaHocKy"];
                                txtTenLop.Text = reader["TenLop"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu lớp học: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int maMon = Convert.ToInt32(cboMaMon.SelectedValue);
            int maHocKy = Convert.ToInt32(cboMaHocKi.SelectedValue);
            string tenLop = txtTenLop.Text;

            if (string.IsNullOrEmpty(tenLop))
            {
                MessageBox.Show("Vui lòng nhập tên lớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "UPDATE Lop_Hoc SET MaMon = @MaMon, MaHocKy = @MaHocKy, TenLop = @TenLop WHERE MaLop = @MaLop";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaMon", maMon);
                        command.Parameters.AddWithValue("@MaHocKy", maHocKy);
                        command.Parameters.AddWithValue("@TenLop", tenLop);
                        command.Parameters.AddWithValue("@MaLop", MaLop);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Sửa lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Gọi sự kiện DataUpdated để thông báo cho form chính
                        DataUpdated?.Invoke(this, EventArgs.Empty);

                        this.Close(); // Đóng form sau khi cập nhật
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi sửa lớp học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void EditDSLH_Load(object sender, EventArgs e)
        {
            
        }
    }
}