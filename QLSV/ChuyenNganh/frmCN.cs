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

namespace QLSV.ChuyenNganh
{
    public partial class frmCN : Form
    {

        public frmCN()
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
            string query = "SELECT MaChuyenNganh, TenChuyenNganh FROM Chuyen_Nganh WHERE DaXoa = 0";

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

                    dataGridView1.Columns["MaChuyenNganh"].HeaderText = "Mã Chuyên Ngành";
                    dataGridView1.Columns["TenChuyenNganh"].HeaderText = "Tên Chuyên Ngành";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn hay không
            if (e.RowIndex >= 0)
            {
                // Lấy mã sinh viên từ cột đầu tiên của hàng đã chọn
                int maChuyenNganh = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MaChuyenNganh"].Value);
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private void LoadCNData()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT MaChuyenNganh, TenChuyenNganh FROM Chuyen_Nganh WHERE DaXoa = 0";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt; // Cập nhật DataGridView với dữ liệu mới
            }
        }

        private void AddForm_DataUpdated(object sender, EventArgs e)
        {
            // Tải lại dữ liệu vào DataGridView
            LoadCNData();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            frmAdd frmAdd = new frmAdd();
            frmAdd.DataUpdated += AddForm_DataUpdated; // Đăng ký sự kiện
            frmAdd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn hay không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy mã sinh viên từ cột đầu tiên của hàng đã chọn
                int maChuyenNganh = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MaChuyenNganh"].Value);

                // Mở form sửa với mã sinh viên đã chọn
                frmEdit frmEdit = new frmEdit(maChuyenNganh);
                frmEdit.DataUpdated += AddForm_DataUpdated; // Đăng ký sự kiện
                frmEdit.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chuyên ngành để sửa.");
            }
        }

        private void DeleteStudent(int maChuyenNganh)
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "UPDATE Chuyen_Nganh SET DaXoa = 1 WHERE MaChuyenNganh = @MaChuyenNganh";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaChuyenNganh", maChuyenNganh);
                        command.ExecuteNonQuery();
                    }

                    LoadCNData();
                    MessageBox.Show("Đã xóa chuyên ngành này.");
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
                // Lấy mã từ cột đầu tiên của hàng đã chọn
                int maSinhVien = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MaChuyenNganh"].Value);

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa chuyên ngành này không?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Thực hiện xóa 
                    DeleteStudent(maSinhVien);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa.");
            }
        }
    }
}
