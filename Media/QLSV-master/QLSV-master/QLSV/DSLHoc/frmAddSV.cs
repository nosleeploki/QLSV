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
    public partial class frmAddSV : Form
    {
        private string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLSV;Integrated Security=True";
        public int MaLop { get; private set; }
        public frmAddSV()
        {
            InitializeComponent();
        }
        public frmAddSV(int maLop, string tenLopHoc, string tenMonHoc)
        {
            InitializeComponent();

            // Gán giá trị vào các TextBox và thuộc tính
            MaLop = maLop;
            txtTenLop.Text = tenLopHoc;
            txtMonHoc.Text = tenMonHoc;

            // Load danh sách sinh viên chưa ghi danh
            LoadSinhVienList();
        }
        private void LoadLopHocDetails()
        {
            MessageBox.Show($"MaLop hiện tại: {MaLop}");
            // Truy vấn thông tin lớp học và môn học từ MaLop
            string query = "SELECT TenLop, Mon_Hoc.TenMon FROM Lop_Hoc " +
                           "JOIN Mon_Hoc ON Lop_Hoc.MaMon = Mon_Hoc.MaMon WHERE MaLop = @MaLop";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", MaLop);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtTenLop.Text = reader["TenLop"].ToString();
                        txtMonHoc.Text = reader["TenMon"].ToString();
                    }
                }
            }
        }

        private void LoadSinhVienList()
        {
            cboCSV.Items.Clear(); // Xóa các mục hiện có

            // Truy vấn danh sách sinh viên chưa ghi danh vào lớp
            string query = "SELECT MaSinhVien, Ho + ' ' + Ten AS TenSinhVien FROM Sinh_Vien WHERE MaSinhVien NOT IN (SELECT MaSinhVien FROM Ghi_Danh WHERE MaLop = @MaLop)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", MaLop);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // Thêm sinh viên vào ComboBox dưới dạng KeyValuePair
                        cboCSV.Items.Add(new KeyValuePair<int, string>(
                            Convert.ToInt32(reader["MaSinhVien"]),
                            reader["TenSinhVien"].ToString()
                        ));
                    }
                }
            }

            cboCSV.DisplayMember = "Value"; // Hiển thị tên sinh viên
            cboCSV.ValueMember = "Key";    // Giá trị thực là mã sinh viên
        }

        private void btnThemSV_Click(object sender, EventArgs e)
        {
            if (cboCSV.SelectedItem != null)
            {
                var selectedSinhVien = (KeyValuePair<int, string>)cboCSV.SelectedItem;
                int maSinhVien = selectedSinhVien.Key;

                // Thêm sinh viên vào lớp học
                AddSinhVienToLop(maSinhVien, MaLop);

                // Đóng form sau khi thêm thành công
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sinh viên.");
            }
        }
        private void AddSinhVienToLop(int maSinhVien, int maLop)
        {
            string checkQuery = "SELECT COUNT(*) FROM Ghi_Danh WHERE MaLop = @MaLop AND MaSinhVien = @MaSinhVien";
            string countQuery = "SELECT COUNT(*) FROM Ghi_Danh WHERE MaLop = @MaLop";
            string insertQuery = "INSERT INTO Ghi_Danh (MaSinhVien, MaLop, SoLanVangMat) VALUES (@MaSinhVien, @MaLop, 0)";
            string insertDiemQuery = "INSERT INTO Diem (MaSinhVien, MaLop, DiemGiuaKy, DiemCuoiKy, DiemTongKet, DiemBaiTap1, DiemBaiTap2, DiemLab1, DiemLab2, DiemChuyenCan, Pass) " +
                                     "VALUES (@MaSinhVien, @MaLop, 0, 0, 0, 0, 0, 0, 0, 10, 0)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Kiểm tra sinh viên đã tồn tại trong lớp
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@MaLop", maLop);
                    checkCmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                    int existingCount = (int)checkCmd.ExecuteScalar();

                    if (existingCount > 0)
                    {
                        MessageBox.Show("Sinh viên này đã có trong lớp học.");
                        return;
                    }
                }

                // Kiểm tra số lượng sinh viên trong lớp
                using (SqlCommand countCmd = new SqlCommand(countQuery, conn))
                {
                    countCmd.Parameters.AddWithValue("@MaLop", maLop);
                    int studentCount = (int)countCmd.ExecuteScalar();

                    if (studentCount >= 40)
                    {
                        MessageBox.Show("Lớp học đã đủ sinh viên (tối đa 40 sinh viên).");
                        return;
                    }
                }

                // Thêm sinh viên vào lớp
                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                    insertCmd.Parameters.AddWithValue("@MaLop", maLop);
                    insertCmd.ExecuteNonQuery();
                }

                // Tạo bản ghi điểm mới
                using (SqlCommand insertDiemCmd = new SqlCommand(insertDiemQuery, conn))
                {
                    insertDiemCmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                    insertDiemCmd.Parameters.AddWithValue("@MaLop", maLop);
                    insertDiemCmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Sinh viên đã được thêm vào lớp học và tạo điểm mặc định thành công.");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
