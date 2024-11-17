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
            maLop = Convert.ToInt32(cboChonLH.SelectedValue);
            maSinhVien = Convert.ToInt32(cboCSV.SelectedValue);

            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kiểm tra xem sinh viên đã có trong lớp chưa
                    string checkQuery = "SELECT COUNT(*) FROM Ghi_Danh WHERE MaLop = @MaLop AND MaSinhVien = @MaSinhVien";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@MaLop", maLop);
                        checkCommand.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                        int existingRecordCount = (int)checkCommand.ExecuteScalar();

                        if (existingRecordCount > 0)
                        {
                            MessageBox.Show("Sinh viên này đã có trong lớp học.");
                            return;
                        }
                    }

                    // Kiểm tra số lượng sinh viên trong lớp
                    string countQuery = "SELECT COUNT(*) FROM Ghi_Danh WHERE MaLop = @MaLop";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, connection))
                    {
                        countCommand.Parameters.AddWithValue("@MaLop", maLop);
                        int studentCount = (int)countCommand.ExecuteScalar();

                        if (studentCount >= 40)
                        {
                            MessageBox.Show("Lớp học đã đủ sinh viên (tối đa 40 sinh viên).");
                            return;
                        }
                    }

                    // Thêm sinh viên vào lớp
                    string insertQuery = "INSERT INTO Ghi_Danh (MaSinhVien, MaLop, SoLanVangMat) VALUES (@MaSinhVien, @MaLop, 0)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                        command.Parameters.AddWithValue("@MaLop", maLop);
                        command.ExecuteNonQuery();
                    }

                    // Tạo mã điểm mới
                    string insertDiemQuery = "INSERT INTO Diem (MaSinhVien, MaLop, DiemGiuaKy, DiemCuoiKy, DiemTongKet, DiemBaiTap1, DiemBaiTap2, DiemLab1, DiemLab2) " +
                                             "VALUES (@MaSinhVien, @MaLop, 0, 0, 0, 0, 0, 0, 0)";
                    using (SqlCommand insertDiemCommand = new SqlCommand(insertDiemQuery, connection))
                    {
                        insertDiemCommand.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                        insertDiemCommand.Parameters.AddWithValue("@MaLop", maLop);
                        insertDiemCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thêm sinh viên vào lớp thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
