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
            string maSoSinhVien = "";
            string email = txtEmail.Text;
            string soDienThoai = txtSoDienThoai.Text;
            string maChuyenNganh = cbChuyenNganh.SelectedValue.ToString(); // Lấy MaChuyenNganh từ ComboBox
            string gioiTinh = cbGioiTinh.SelectedItem.ToString(); // Lấy giới tính từ ComboBox
            string diaChi = txtDiaChi.Text;
            string cmnd = txtCMND.Text;
            string khoaHoc = txtKhoaHoc.Text;
            DateTime ngaySinh = dtpNgaySinh.Value; // Lấy ngày sinh từ DateTimePicker
            string ghiChu = txtGhiChu.Text;

            // Kiểm tra các TextBox có rỗng không
            if (string.IsNullOrEmpty(ho) || string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(diaChi))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuỗi kết nối cơ sở dữ liệu
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Câu lệnh SQL để tìm MaSinhVien cao nhất hiện có
            string queryGetMaxMaSinhVien = "SELECT MAX(MaSinhVien) FROM Sinh_Vien";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Tạo một câu lệnh SQL để lấy mã sinh viên lớn nhất
                    SqlCommand cmdMaxMaSinhVien = new SqlCommand(queryGetMaxMaSinhVien, connection);
                    var result = cmdMaxMaSinhVien.ExecuteScalar();
                    int nextMaSinhVien = result == DBNull.Value ? 1 : Convert.ToInt32(result) + 1;

                    // Gán giá trị cho MaSinhVien mới
                    maSinhVien = nextMaSinhVien.ToString(); // MaSinhVien tự động tăng, ví dụ: "1", "2",...

                    maSoSinhVien = "SV" + nextMaSinhVien.ToString("D3");

                    // Câu lệnh SQL để thêm thông tin sinh viên
                    string queryInsert = "INSERT INTO Sinh_Vien (MaSinhVien, MaSoSinhVien, Ho, Ten, Email, SoDienThoai, MaChuyenNganh, GioiTinh, DiaChi, CMND, KhoaHoc, NgaySinh, GhiChu, DaXoa) " +
                                 "VALUES (@MaSinhVien, @MaSoSinhVien, @Ho, @Ten, @Email, @SoDienThoai, @MaChuyenNganh, @GioiTinh, @DiaChi, @CMND, @KhoaHoc, @NgaySinh, @GhiChu, 0);";

                    using (SqlCommand command = new SqlCommand(queryInsert, connection))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                        command.Parameters.AddWithValue("@MaSoSinhVien", maSoSinhVien); // Thêm Mã Số Sinh Viên vào CSDL
                        command.Parameters.AddWithValue("@Ho", ho);
                        command.Parameters.AddWithValue("@Ten", ten);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                        command.Parameters.AddWithValue("@MaChuyenNganh", maChuyenNganh);
                        command.Parameters.AddWithValue("@GioiTinh", gioiTinh == "Nam" ? "M" : "F");
                        command.Parameters.AddWithValue("@DiaChi", diaChi);
                        command.Parameters.AddWithValue("@CMND", cmnd);
                        command.Parameters.AddWithValue("@KhoaHoc", khoaHoc);
                        command.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                        command.Parameters.AddWithValue("@GhiChu", ghiChu);

                        // Thực thi câu lệnh SQL
                        command.ExecuteNonQuery();

                        // Thông báo thành công
                        MessageBox.Show("Thêm sinh viên thành công! Mã số sinh viên: " + maSoSinhVien, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Kích hoạt sự kiện DataUpdated nếu có form nào lắng nghe
                        DataUpdated?.Invoke(this, EventArgs.Empty);

                        // Đóng form hiện tại
                        this.Close();
                    }
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
