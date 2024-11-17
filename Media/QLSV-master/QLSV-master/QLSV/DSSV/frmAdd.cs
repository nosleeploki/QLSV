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
    public partial class frmAdd : Form
    {
        public frmAdd()
        {
            InitializeComponent();
        }

        public event EventHandler DataUpdated;


        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox và điều khiển khác
            string maSinhVien = ""; // Mã sinh viên tự động sẽ được tạo
            string ho = txtHo.Text;
            string ten = txtTen.Text;
            string email = txtEmail.Text;
            string soDienThoai = txtSoDienThoai.Text;
            string cmnd = txtCMND.Text;

            // Kiểm tra các TextBox có rỗng không
            if (string.IsNullOrEmpty(ho) || string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(cmnd))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra trùng lặp CMND, Email và Số điện thoại
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string queryCheckDuplicates = "SELECT COUNT(*) FROM Sinh_Vien WHERE CMND = @CMND OR Email = @Email OR SoDienThoai = @SoDienThoai";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryCheckDuplicates, connection);
                    cmdCheck.Parameters.AddWithValue("@CMND", cmnd);
                    cmdCheck.Parameters.AddWithValue("@Email", email);
                    cmdCheck.Parameters.AddWithValue("@SoDienThoai", soDienThoai);

                    int duplicateCount = (int)cmdCheck.ExecuteScalar();

                    if (duplicateCount > 0)
                    {
                        MessageBox.Show("Thông tin CMND, Email hoặc Số điện thoại đã tồn tại trong cơ sở dữ liệu. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Câu lệnh SQL để tìm MaSinhVien cao nhất hiện có
                    string queryGetMaxMaSinhVien = "SELECT MAX(MaSinhVien) FROM Sinh_Vien";
                    SqlCommand cmdMaxMaSinhVien = new SqlCommand(queryGetMaxMaSinhVien, connection);
                    var result = cmdMaxMaSinhVien.ExecuteScalar();
                    int nextMaSinhVien = result == DBNull.Value ? 1 : Convert.ToInt32(result) + 1;
                    maSinhVien = nextMaSinhVien.ToString();
                    string maSoSinhVien = "SV" + nextMaSinhVien.ToString("D3");

                    // Câu lệnh SQL để thêm thông tin sinh viên
                    string queryInsert = "INSERT INTO Sinh_Vien (MaSinhVien, MaSoSinhVien, Ho, Ten, Email, SoDienThoai, MaChuyenNganh, GioiTinh, DiaChi, CMND, KhoaHoc, NgaySinh, GhiChu, DaXoa) " +
                                         "VALUES (@MaSinhVien, @MaSoSinhVien, @Ho, @Ten, @Email, @SoDienThoai, @MaChuyenNganh, @GioiTinh, @DiaChi, @CMND, @KhoaHoc, @NgaySinh, @GhiChu, 0);";

                    SqlCommand command = new SqlCommand(queryInsert, connection);
                    command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                    command.Parameters.AddWithValue("@MaSoSinhVien", maSoSinhVien);
                    command.Parameters.AddWithValue("@Ho", ho);
                    command.Parameters.AddWithValue("@Ten", ten);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    command.Parameters.AddWithValue("@MaChuyenNganh", cbChuyenNganh.SelectedValue);
                    command.Parameters.AddWithValue("@GioiTinh", cbGioiTinh.SelectedItem.ToString() == "Nam" ? "M" : "F");
                    command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    command.Parameters.AddWithValue("@CMND", cmnd);
                    command.Parameters.AddWithValue("@KhoaHoc", txtKhoaHoc.Text);
                    command.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    command.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);

                    // Thực thi câu lệnh SQL
                    command.ExecuteNonQuery();

                    // Thông báo thành công
                    MessageBox.Show("Thêm sinh viên thành công! Mã số sinh viên: " + maSoSinhVien, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Kích hoạt sự kiện DataUpdated nếu có form nào lắng nghe
                    DataUpdated?.Invoke(this, EventArgs.Empty);

                    // Đóng form hiện tại
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmAdd_Load(object sender, EventArgs e)
        {
            // Kết nối với CSDL và lấy danh sách chuyên ngành
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT MaChuyenNganh, TenChuyenNganh FROM Chuyen_Nganh WHERE DaXoa = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbChuyenNganh.DisplayMember = "TenChuyenNganh";
                cbChuyenNganh.ValueMember = "MaChuyenNganh";
                cbChuyenNganh.DataSource = dt;
            }

            // Cập nhật ComboBox giới tính
            cbGioiTinh.Items.Add("Nam");
            cbGioiTinh.Items.Add("Nữ");
            cbGioiTinh.SelectedIndex = 0; // Mặc định chọn Nam
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
