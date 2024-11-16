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
    public partial class AddLH : Form
    {
        public event EventHandler DataUpdated;
        public AddLH()
        {
            InitializeComponent();
            LoadMaMon();
            LoadHocKy();
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenLop = txtTenLop.Text;
            int maMon = Convert.ToInt32(cboMaMon.SelectedValue);
            int maHocKy = Convert.ToInt32(cboMaHocKi.SelectedValue);
            int maLop = GetNextMaLop(); // Lấy giá trị MaLop mới

            // Kiểm tra các trường nhập liệu không được bỏ trống
            if (string.IsNullOrEmpty(tenLop))
            {
                MessageBox.Show("Vui lòng điền tên lớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuỗi kết nối cơ sở dữ liệu
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Thêm lớp học
                        string queryLopHoc = "INSERT INTO Lop_Hoc (MaLop, MaMon, MaHocKy, TenLop, DaXoa) VALUES (@MaLop, @MaMon, @MaHocKy, @TenLop, 0)";
                        using (SqlCommand command = new SqlCommand(queryLopHoc, connection, transaction))
                        {
                            command.CommandTimeout = 120; // Tăng thời gian chờ
                            command.Parameters.AddWithValue("@MaLop", maLop);
                            command.Parameters.AddWithValue("@MaMon", maMon);
                            command.Parameters.AddWithValue("@MaHocKy", maHocKy);
                            command.Parameters.AddWithValue("@TenLop", tenLop);
                            command.ExecuteNonQuery();
                        }

                        // Thêm các loại điểm mặc định
                        string queryLoaiDiem = @"
                                                INSERT INTO Loai_Dau_Diem (MaLoaiDiem, MaMon, TenLoaiDiem, TrongSo, DaXoa)
                                                VALUES 
                                                    (NEWID(), @MaMon, N'Điểm giữa kỳ', 0.3, 0),
                                                    (NEWID(), @MaMon, N'Điểm cuối kỳ', 0.4, 0),
                                                    (NEWID(), @MaMon, N'Điểm chuyên cần', 0.1, 0),
                                                    (NEWID(), @MaMon, N'Điểm bài tập', 0.1, 0),
                                                    (NEWID(), @MaMon, N'Điểm thi kết thúc môn', 0.1, 0);";


                        using (SqlCommand commandLoaiDiem = new SqlCommand(queryLoaiDiem, connection, transaction))
                        {
                            commandLoaiDiem.CommandTimeout = 120; // Tăng thời gian chờ
                            commandLoaiDiem.Parameters.AddWithValue("@MaMon", maMon);
                            commandLoaiDiem.ExecuteNonQuery();
                        }

                        // Commit transaction
                        transaction.Commit();
                        MessageBox.Show("Thêm lớp học và các đầu điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Kích hoạt sự kiện DataUpdated để thông báo cho form chính
                        DataUpdated?.Invoke(this, EventArgs.Empty);

                        this.Close(); // Đóng form sau khi lưu
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction nếu có lỗi
                        transaction.Rollback();
                        MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private int GetNextMaLoaiDiem()
        {
            int nextId = 0;
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ISNULL(MAX(MaLoaiDiem), 0) + 1 FROM Loai_Dau_Diem";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    nextId = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return nextId;
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
