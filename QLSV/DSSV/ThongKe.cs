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
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            LoadDataToDataGridView();
        }
        private void LoadDataToDataGridView()
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Câu truy vấn lấy dữ liệu từ bảng Sinh_Vien, Ghi_Danh, Lop_Hoc, Hoc_Ky, Mon_Hoc, Chuyen_Nganh
            string query = @"
                SELECT 
                    sv.MaSinhVien, sv.Ho + ' ' + sv.Ten AS HoTen, mh.TenMon, hk.TenHocKy, cn.TenChuyenNganh, lh.TenLop,
                    CASE WHEN d.GiaTriDiem >= 5 THEN 'Qua' ELSE 'Truot' END AS KetQua
                FROM 
                    Sinh_Vien sv
                JOIN 
                    Ghi_Danh gd ON sv.MaSinhVien = gd.MaSinhVien
                JOIN 
                    Lop_Hoc lh ON gd.MaLop = lh.MaLop
                JOIN 
                    Hoc_Ky hk ON lh.MaHocKy = hk.MaHocKy
                JOIN 
                    Mon_Hoc mh ON lh.MaMon = mh.MaMon
                JOIN 
                    Chuyen_Nganh cn ON sv.MaChuyenNganh = cn.MaChuyenNganh
                LEFT JOIN 
                    Diem d ON sv.MaSinhVien = d.MaSinhVien 
                WHERE 
                    (@MonHoc IS NULL OR mh.TenMon = @MonHoc) AND
                    (@HocKy IS NULL OR hk.TenHocKy = @HocKy) AND
                    (@ChuyenNganh IS NULL OR cn.TenChuyenNganh = @ChuyenNganh) AND
                    (@LopHoc IS NULL OR lh.TenLop = @LopHoc)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Tạo SqlCommand với truy vấn và thêm các tham số
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MonHoc", DBNull.Value);  // Hoặc thay bằng giá trị cụ thể nếu có
                    command.Parameters.AddWithValue("@HocKy", DBNull.Value);   // Hoặc thay bằng giá trị cụ thể nếu có
                    command.Parameters.AddWithValue("@ChuyenNganh", DBNull.Value); // Hoặc thay bằng giá trị cụ thể nếu có
                    command.Parameters.AddWithValue("@LopHoc", DBNull.Value); // Hoặc thay bằng giá trị cụ thể nếu có

                    // Tạo SqlDataAdapter và DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    // Đổ dữ liệu vào DataTable và gán vào DataGridView
                    adapter.Fill(dataTable);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
