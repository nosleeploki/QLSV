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
            string query = "INSERT INTO Ghi_Danh (MaSinhVien, MaLop, SoLanVangMat) VALUES (@MaSinhVien, @MaLop, 0)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                        command.Parameters.AddWithValue("@MaLop", maLop);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Thêm sinh viên vào lớp thành công!");
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
