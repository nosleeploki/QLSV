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
    public partial class AddDSSVvaoLopHoc : Form
    {
        private int maLop;
        private int maSinhVien;
        public AddDSSVvaoLopHoc()
        {
            InitializeComponent();
            LoadDanhSachLop();
            LoadDanhSachSinhVien();
        }
        private void LoadDanhSachLop()
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT MaLop, TenLop FROM dbo.Lop_Hoc WHERE DaXoa = 0";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                cboChonLH.DataSource = dataTable;
                cboChonLH.DisplayMember = "TenLop";
                cboChonLH.ValueMember = "MaLop";
            }
        }
        private void LoadDanhSachSinhVien()
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT MaSinhVien, Ho + ' ' + Ten AS HoTen FROM dbo.Sinh_Vien WHERE DaXoa = 0";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                cboCSV.DataSource = dataTable;
                cboCSV.DisplayMember = "HoTen";
                cboCSV.ValueMember = "MaSinhVien";
            }
        }

        private void AddDSSVvaoLopHoc_Load(object sender, EventArgs e)
        {

        }

        private void btnThemSV_Click(object sender, EventArgs e)
        {
            int maLop = Convert.ToInt32(cboChonLH.SelectedValue);
            int maSinhVien = Convert.ToInt32(cboCSV.SelectedValue);

            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Truy vấn số lượng sinh viên trong lớp
            string checkQuery = "SELECT COUNT(*) FROM Ghi_Danh WHERE MaLop = @MaLop";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Kiểm tra số lượng sinh viên trong lớp
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@MaLop", maLop);
                        int studentCount = (int)checkCommand.ExecuteScalar();

                        // Nếu số lượng sinh viên trong lớp đã đạt 40, không cho thêm
                        if (studentCount >= 40)
                        {
                            MessageBox.Show("Lớp học đã đủ sinh viên (tối đa 40 sinh viên).");
                            return;
                        }
                    }

                    // Thêm sinh viên vào lớp nếu lớp chưa đủ 40 sinh viên
                    string query = "INSERT INTO Ghi_Danh (MaSinhVien, MaLop, SoLanVangMat) VALUES (@MaSinhVien, @MaLop, 0)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                        command.Parameters.AddWithValue("@MaLop", maLop);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Thêm sinh viên vào lớp thành công!");

                        // Truy vấn môn học duy nhất trong lớp
                        string getMonHocQuery = @"
                    SELECT TOP 1 MaMon 
                    FROM Mon_Hoc 
                    WHERE MaNhomMon = (SELECT MaNhomMon FROM Lop_Hoc WHERE MaLop = @MaLop)";

                        using (SqlCommand getMonHocCommand = new SqlCommand(getMonHocQuery, connection))
                        {
                            getMonHocCommand.Parameters.AddWithValue("@MaLop", maLop);
                            int maMon = Convert.ToInt32(getMonHocCommand.ExecuteScalar()); // Lấy MaMon duy nhất

                            // Lấy mã điểm tối đa hiện tại và tăng lên
                            string maxDiemQuery = "SELECT ISNULL(MAX(MaDiem), 0) + 1 FROM Diem";
                            using (SqlCommand maxDiemCommand = new SqlCommand(maxDiemQuery, connection))
                            {
                                int maDiem = (int)maxDiemCommand.ExecuteScalar(); // Lấy MaDiem tiếp theo

                                // Tạo 5 loại điểm cho sinh viên trong môn học duy nhất
                                for (int i = 1; i <= 5; i++)
                                {
                                    string insertDiemQuery = "INSERT INTO Diem (MaDiem, MaSinhVien, MaLoaiDiem, GiaTriDiem, MaMon) " +
                                                             "VALUES (@MaDiem, @MaSinhVien, @MaLoaiDiem, 0, @MaMon);";

                                    using (SqlCommand insertDiemCommand = new SqlCommand(insertDiemQuery, connection))
                                    {
                                        insertDiemCommand.Parameters.AddWithValue("@MaDiem", maDiem); // MaDiem được tạo thủ công
                                        insertDiemCommand.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                                        insertDiemCommand.Parameters.AddWithValue("@MaLoaiDiem", i); // Loại điểm từ 1 đến 5
                                        insertDiemCommand.Parameters.AddWithValue("@MaMon", maMon); // Chỉ cho môn học duy nhất

                                        insertDiemCommand.ExecuteNonQuery();  // Chạy câu lệnh INSERT
                                        maDiem++;  // Tăng MaDiem để dùng cho bản ghi tiếp theo
                                    }
                                }
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

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
