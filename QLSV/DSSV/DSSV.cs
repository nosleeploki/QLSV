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

namespace QLSV.DSSV
{
    public partial class DSSV : Form
    {
        public DSSV()
        {
            InitializeComponent();
            LoadDataToDataGridView();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void LoadDataToDataGridView()
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Câu truy vấn lấy dữ liệu từ bảng Sinh_Vien
            string query = "SELECT MaSinhVien, Ho, Ten, MaSoSinhVien, KhoaHoc FROM Sinh_Vien WHERE DaXoa = 0";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo SqlDataAdapter để thực hiện câu truy vấn và lấy dữ liệu
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    // Tạo DataTable để lưu dữ liệu tạm thời
                    DataTable dataTable = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    adapter.Fill(dataTable);

                    // Gán DataTable vào DataGridView
                    dataGridView1.DataSource = dataTable;

                    dataGridView1.Columns["MaSinhVien"].HeaderText = "STT";
                    dataGridView1.Columns["Ho"].HeaderText = "Họ";
                    dataGridView1.Columns["Ten"].HeaderText = "Tên";
                    dataGridView1.Columns["MaSoSinhVien"].HeaderText = "Mã Sinh Viên";
                    dataGridView1.Columns["KhoaHoc"].HeaderText = "Khóa học";


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void ShowStudentDetails(int maSinhVien)
        {
            // Chuỗi kết nối tới SQL Server
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Câu lệnh SQL để lấy thông tin đầy đủ của sinh viên
            string query = "SELECT * FROM Sinh_Vien WHERE MaSinhVien = @MaSinhVien";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Lấy thông tin sinh viên từ kết quả truy vấn
                                string ho = reader["Ho"].ToString();
                                string ten = reader["Ten"].ToString();
                                string maSoSinhVien = reader["MaSoSinhVien"].ToString();
                                string email = reader["Email"].ToString();
                                string soDienThoai = reader["SoDienThoai"].ToString();
                                string gioiTinh = reader["GioiTinh"].ToString();
                                string diaChi = reader["DiaChi"].ToString();
                                string cmnd = reader["CMND"].ToString();
                                string khoaHoc = reader["KhoaHoc"].ToString();
                                DateTime ngaySinh = Convert.ToDateTime(reader["NgaySinh"]);
                                string ghiChu = reader["GhiChu"].ToString();

                                // Hiển thị thông tin trong MessageBox (hoặc bạn có thể tạo một Form mới để hiển thị)
                                string studentInfo = $"Mã Sinh Viên: {maSinhVien}\n" +
                                                     $"Họ và Tên: {ho} {ten}\n" +
                                                     $"MSSV: {maSoSinhVien}\n" +
                                                     $"Email: {email}\n" +
                                                     $"SĐT: {soDienThoai}\n" +
                                                     $"Giới Tính: {gioiTinh}\n" +
                                                     $"Địa Chỉ: {diaChi}\n" +
                                                     $"CMND: {cmnd}\n" +
                                                     $"Khóa Học: {khoaHoc}\n" +
                                                     $"Ngày Sinh: {ngaySinh.ToShortDateString()}\n" +
                                                     $"Ghi Chú: {ghiChu}";

                                MessageBox.Show(studentInfo, "Thông Tin Sinh Viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }


        private void btn_Add_Click(object sender, EventArgs e)
        {
            frmAdd frmAdd = new frmAdd();
            frmAdd.DataUpdated += AddForm_DataUpdated; // Đăng ký sự kiện
            frmAdd.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn hay không
            if (e.RowIndex >= 0)
            {
                // Lấy mã sinh viên từ cột đầu tiên của hàng đã chọn
                int maSinhVien = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MaSinhVien"].Value);

                dataGridView1.Rows[e.RowIndex].Selected = true;

                // Gọi hàm để hiển thị thông tin chi tiết sinh viên
                ShowStudentDetails(maSinhVien);
            }
        }


        private void AddForm_DataUpdated(object sender, EventArgs e)
        {
            // Tải lại dữ liệu vào DataGridView
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT MaSinhVien, Ho, Ten, MaSoSinhVien, KhoaHoc FROM Sinh_Vien WHERE DaXoa = 0";
                        

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt; // Cập nhật DataGridView với dữ liệu mới
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn hay không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy mã sinh viên từ cột đầu tiên của hàng đã chọn
                int maSinhVien = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MaSinhVien"].Value);

                // Mở form sửa với mã sinh viên đã chọn
                frmEdit frmEdit = new frmEdit(maSinhVien);
                frmEdit.DataUpdated += AddForm_DataUpdated; // Đăng ký sự kiện
                frmEdit.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để sửa.");
            }
        }

        //Hàm xóa sinh viên
        private void DeleteStudent(int maSinhVien)
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "UPDATE Sinh_Vien SET DaXoa = 1 WHERE MaSinhVien = @MaSinhVien";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                        command.ExecuteNonQuery();
                    }

                    LoadStudentData();
                    MessageBox.Show("Đã xóa sinh viên thành công.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn hay không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy mã sinh viên từ cột đầu tiên của hàng đã chọn
                int maSinhVien = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MaSinhVien"].Value);

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Thực hiện xóa sinh viên
                    DeleteStudent(maSinhVien);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa.");
            }
        }

        private void DSSV_Load(object sender, EventArgs e)
        {
           

        }
    }
}
