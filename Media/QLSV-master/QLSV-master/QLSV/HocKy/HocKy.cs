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

namespace QLSV.HocKy
{
    public partial class HocKy : Form
    {
        public HocKy()
        {
            InitializeComponent();
            LoadDataToDataGridView();
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
        }
        private void LoadDataToDataGridView()
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Câu truy vấn lấy dữ liệu từ bảng Sinh_Vien
            string query = "SELECT MaHocKy, TenHocKy, Nam FROM dbo.Hoc_Ky WHERE DaXoa = 0";

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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            frmAdd frmAdd = new frmAdd();
            frmAdd.DataUpdated += AddForm_DataUpdated; // Đăng ký sự kiện
            frmAdd.Show();
        }
        private void AddForm_DataUpdated(object sender, EventArgs e)
        {
            // Tải lại dữ liệu vào DataGridView
            LoadTerminalData();
        }

        private void LoadTerminalData()
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT MaHocKy, TenHocKy, Nam FROM dbo.Hoc_Ky WHERE DaXoa = 0";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt; // Cập nhật DataGridView với dữ liệu mới
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ dòng được chọn
                string maHocKy = dataGridView1.SelectedRows[0].Cells["MaHocKy"].Value.ToString();
                string tenHocKy = dataGridView1.SelectedRows[0].Cells["TenHocKy"].Value.ToString();
                string nam = dataGridView1.SelectedRows[0].Cells["Nam"].Value.ToString();

                // Hiển thị form sửa
                frmEdit frmEdit = new frmEdit(maHocKy, tenHocKy, nam);
                frmEdit.ShowDialog();

                // Sau khi form sửa đóng, tải lại dữ liệu để cập nhật
                LoadDataToDataGridView();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một học kỳ để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string maHocKy = dataGridView1.SelectedRows[0].Cells["MaHocKy"].Value.ToString();
                string tenHocKy = dataGridView1.SelectedRows[0].Cells["TenHocKy"].Value.ToString();
                string nam = dataGridView1.SelectedRows[0].Cells["Nam"].Value.ToString();

                // Hiển thị form DanhSachLopForm với dữ liệu đã chọn
                frmChiTiet frmChiTiet = new frmChiTiet(maHocKy, tenHocKy, nam);
                frmChiTiet.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một học kỳ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy MaHocKy từ dòng được chọn trong DataGridView
                string maHocKy = dataGridView1.SelectedRows[0].Cells["MaHocKy"].Value.ToString();

                // Chuỗi kết nối
                string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
                string query = "UPDATE Hoc_Ky SET DaXoa = 1 WHERE MaHocKy = @MaHocKy";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@MaHocKy", maHocKy);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Xóa học kỳ thành công!");
                        LoadDataToDataGridView(); // Cập nhật lại DataGridView sau khi xóa
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một học kỳ để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
