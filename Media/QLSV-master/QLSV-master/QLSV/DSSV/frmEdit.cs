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
    public partial class frmEdit : Form
    {
        public event EventHandler DataUpdated;
        private int maSinhVien;
        public frmEdit(int maSinhVien)
        {
            InitializeComponent();
            this.maSinhVien = maSinhVien;
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Câu lệnh SQL để lấy thông tin sinh viên
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
                                // Gán dữ liệu vào các TextBox
                                txtHo.Text = reader["Ho"].ToString();
                                txtTen.Text = reader["Ten"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                txtSoDienThoai.Text = reader["SoDienThoai"].ToString();
                                cbChuyenNganh.Text = reader["MaChuyenNganh"].ToString();
                                cbGioiTinh.Text = reader["GioiTinh"].ToString();
                                txtDiaChi.Text = reader["DiaChi"].ToString();
                                txtCMND.Text = reader["CMND"].ToString();
                                txtKhoaHoc.Text = reader["KhoaHoc"].ToString();
                                dtpNgaySinh.Value = Convert.ToDateTime(reader["NgaySinh"]); // Giả sử bạn sử dụng DateTimePicker
                                txtGhiChu.Text = reader["GhiChu"].ToString();
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

        private void frmEdit_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox và điều khiển khác
            string email = txtEmail.Text;
            string soDienThoai = txtSoDienThoai.Text;
            string cmnd = txtCMND.Text;

            // Kiểm tra các TextBox có rỗng không
            if (string.IsNullOrEmpty(txtHo.Text) || string.IsNullOrEmpty(txtTen.Text) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra trùng lặp CMND, Email và Số điện thoại
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string queryCheckDuplicates = "SELECT COUNT(*) FROM Sinh_Vien WHERE (CMND = @CMND OR Email = @Email OR SoDienThoai = @SoDienThoai) AND MaSinhVien != @MaSinhVien";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryCheckDuplicates, connection);
                    cmdCheck.Parameters.AddWithValue("@CMND", cmnd);
                    cmdCheck.Parameters.AddWithValue("@Email", email);
                    cmdCheck.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    cmdCheck.Parameters.AddWithValue("@MaSinhVien", maSinhVien); // Exclude current student

                    int duplicateCount = (int)cmdCheck.ExecuteScalar();

                    if (duplicateCount > 0)
                    {
                        MessageBox.Show("Thông tin CMND, Email hoặc Số điện thoại đã tồn tại trong cơ sở dữ liệu. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Câu lệnh SQL để cập nhật thông tin sinh viên
                    string query = "UPDATE Sinh_Vien SET Ho = @Ho, Ten = @Ten, " +
                                   "Email = @Email, SoDienThoai = @SoDienThoai, MaChuyenNganh = @MaChuyenNganh, " +
                                   "GioiTinh = @GioiTinh, DiaChi = @DiaChi, CMND = @CMND, KhoaHoc = @KhoaHoc, " +
                                   "NgaySinh = @NgaySinh, GhiChu = @GhiChu WHERE MaSinhVien = @MaSinhVien";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaSinhVien", maSinhVien); // Tham số để xác định sinh viên cần cập nhật
                        command.Parameters.AddWithValue("@Ho", txtHo.Text);
                        command.Parameters.AddWithValue("@Ten", txtTen.Text);
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                        command.Parameters.AddWithValue("@MaChuyenNganh", cbChuyenNganh.Text);
                        command.Parameters.AddWithValue("@GioiTinh", cbGioiTinh.Text);
                        command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                        command.Parameters.AddWithValue("@CMND", txtCMND.Text);
                        command.Parameters.AddWithValue("@KhoaHoc", txtKhoaHoc.Text);
                        command.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value); // Giả sử bạn sử dụng DateTimePicker
                        command.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);

                        command.ExecuteNonQuery();
                    }

                    // Gọi sự kiện để thông báo rằng dữ liệu đã được cập nhật
                    DataUpdated?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }
    }
    }

