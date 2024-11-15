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

namespace QLSV.NhomMonHoc
{
    public partial class frmNhom : Form
    {
        public frmNhom()
        {
            InitializeComponent();
            LoadDataToDataGridView();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void AddForm_DataUpdated(object sender, EventArgs e)
        {
            // Tải lại dữ liệu vào DataGridView
            LoadNMData();
        }

        private void LoadDataToDataGridView()
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Câu truy vấn lấy dữ liệu từ bảng Sinh_Vien
            string query = "SELECT MaNhomMon, TenNhomMon FROM Nhom_Mon_Hoc WHERE DaXoa = 0";

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

                    dataGridView1.Columns["MaNhomMon"].HeaderText = "Mã Nhóm Môn";
                    dataGridView1.Columns["TenNhomMon"].HeaderText = "Tên Nhóm Môn";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            frmAdd frmAdd = new frmAdd();
            frmAdd.DataUpdated += AddForm_DataUpdated; // Đăng ký sự kiện
            frmAdd.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn hay không
            if (e.RowIndex >= 0)
            {
                // Lấy mã sinh viên từ cột đầu tiên của hàng đã chọn
                int maNhomMon = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MaNhomMon"].Value);
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private void LoadNMData()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT MaNhomMon, TenNhomMon FROM Nhom_Mon_Hoc WHERE DaXoa = 0";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt; // Cập nhật DataGridView với dữ liệu mới
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn hay không
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy mã sinh viên từ cột đầu tiên của hàng đã chọn
                int maNhomMon = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MaNhomMon"].Value);

                // Mở form sửa với mã sinh viên đã chọn
                frmEdit frmEdit = new frmEdit(maNhomMon);
                frmEdit.DataUpdated += AddForm_DataUpdated; // Đăng ký sự kiện
                frmEdit.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhóm môn để sửa.");
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn hay không
            if (e.RowIndex >= 0)
            {
                // Lấy mã sinh viên từ cột đầu tiên của hàng đã chọn
                int maNhomMon = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MaNhomMon"].Value);
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private void DeleteGroup(int maNhomMon)
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "UPDATE Nhom_Mon_Hoc SET DaXoa = 1 WHERE MaNhomMon = @MaNhomMon";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNhomMon", maNhomMon);
                        command.ExecuteNonQuery();
                    }

                    LoadNMData();
                    MessageBox.Show("Đã xóa nhóm môn này.");
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
                int maNhomMon = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MaNhomMon"].Value);

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa chuyên ngành này không?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Thực hiện xóa 
                    DeleteGroup(maNhomMon);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa.");
            }
        }
    }
}
