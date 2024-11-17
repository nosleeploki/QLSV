using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTap.DSLHoc
{
    public partial class NhapDiem : Form
    {
        public NhapDiem()
        {
            InitializeComponent();
            LoadDanhSachLop();
            LoadDanhSachLoaiDiem();

            txtNhapDiem.KeyPress += TxtNhapDiem_KeyPress;
            txtNhapDiem.Leave += TxtNhapDiem_Leave;
        }

        private void NhapDiem_Load(object sender, EventArgs e)
        {

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
        private void LoadDanhSachLoaiDiem()
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT MaLoaiDiem, TenLoaiDiem FROM dbo.Loai_Dau_Diem WHERE DaXoa = 0";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                cboLoaiDiem.DataSource = dataTable;
                cboLoaiDiem.DisplayMember = "TenLoaiDiem";
                cboLoaiDiem.ValueMember = "MaLoaiDiem";
            }
        }

        private void btnNhapDiem_Click(object sender, EventArgs e)
        {
            string diemText = txtNhapDiem.Text.Trim().Replace(",", ".");
            decimal diem;

            // Kiểm tra nếu giá trị nhập không hợp lệ
            if (!decimal.TryParse(diemText, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out diem))
            {
                MessageBox.Show("Vui lòng nhập một giá trị số hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra nếu điểm không nằm trong khoảng hợp lệ
            if (diem < 0 || diem > 10)
            {
                MessageBox.Show("Vui lòng nhập một giá trị điểm từ 0 đến 10.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tiếp tục với mã xử lý điểm nếu nhập hợp lệ
            int maSinhVien = Convert.ToInt32(cboCSV.SelectedValue);
            int maLoaiDiem = Convert.ToInt32(cboLoaiDiem.SelectedValue);

            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "INSERT INTO Diem (MaSinhVien, MaLoaiDiem, GiaTriDiem) VALUES (@MaSinhVien, @MaLoaiDiem, @GiaTriDiem)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                        command.Parameters.AddWithValue("@MaLoaiDiem", maLoaiDiem);
                        command.Parameters.AddWithValue("@GiaTriDiem", diem);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Nhập điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cboChonLH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChonLH.SelectedValue is DataRowView rowView)
            {
                int maLop = Convert.ToInt32(rowView["MaLop"]);
                LoadDanhSachSinhVien(maLop);
            }
            else if (cboChonLH.SelectedValue != null)
            {
                int maLop = Convert.ToInt32(cboChonLH.SelectedValue);
                LoadDanhSachSinhVien(maLop);
            }
        }
        private void LoadDanhSachSinhVien(int maLop)
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT SV.MaSinhVien, SV.Ho + ' ' + SV.Ten AS HoTen FROM Sinh_Vien SV " + "INNER JOIN Ghi_Danh GD ON SV.MaSinhVien = GD.MaSinhVien WHERE GD.MaLop = @MaLop";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@MaLop", maLop);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                cboCSV.DataSource = dataTable;
                cboCSV.DisplayMember = "HoTen";
                cboCSV.ValueMember = "MaSinhVien";
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void TxtNhapDiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép số, dấu chấm và phím xóa
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            // Không cho phép nhập quá một dấu chấm
            if (e.KeyChar == '.' && txtNhapDiem.Text.Contains("."))
            {
                e.Handled = true;
            }
        }
        private void TxtNhapDiem_Leave(object sender, EventArgs e)
        {
            // Thay thế dấu phẩy thành dấu chấm nếu có
            txtNhapDiem.Text = txtNhapDiem.Text.Replace(",", ".");
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txtNhapDiem.Text = txtNhapDiem.Text.Replace(",", ".");
        }
    }
}
