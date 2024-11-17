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
            LoadDataToDataGridView();
        }
        private void LoadDataToDataGridView()
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Câu truy vấn lấy dữ liệu thống kê sinh viên qua môn, trượt môn, điểm danh theo kỳ, ngành, lớp
            string query = @"
       SELECT 
    mh.TenMon AS MonHoc,
    hk.TenHocKy AS HocKy,
    cn.TenChuyenNganh AS ChuyenNganh,
    lh.TenLop AS LopHoc,
    COUNT(DISTINCT sv.MaSinhVien) AS SoSinhVien, 
    COUNT(CASE WHEN d.GiaTriDiem >= 5 THEN 1 ELSE NULL END) AS SoSinhVienQuaMon,
    COUNT(CASE WHEN d.GiaTriDiem < 5 THEN 1 ELSE NULL END) AS SoSinhVienTruotMon,
    SUM(CASE WHEN gd.SoLanVangMat IS NULL THEN 0 ELSE gd.SoLanVangMat END) AS SoLanVangMat
FROM 
    Sinh_Vien sv
JOIN 
    Ghi_Danh gd ON sv.MaSinhVien = gd.MaSinhVien
JOIN 
    Lop_Hoc lh ON gd.MaLop = lh.MaLop
JOIN 
    Hoc_Ky hk ON lh.MaHocKy = hk.MaHocKy
JOIN 
    Mon_Hoc mh ON lh.MaMon = mh.MaMon  -- Ensure this join is correct
JOIN 
    Chuyen_Nganh cn ON sv.MaChuyenNganh = cn.MaChuyenNganh
LEFT JOIN 
    Diem d ON sv.MaSinhVien = d.MaSinhVien -- Removed MaMon from here
WHERE 
    (@MonHoc IS NULL OR mh.TenMon = @MonHoc) AND
    (@HocKy IS NULL OR hk.TenHocKy = @HocKy) AND
    (@ChuyenNganh IS NULL OR cn.TenChuyenNganh = @ChuyenNganh) AND
    (@LopHoc IS NULL OR lh.TenLop = @LopHoc)
GROUP BY 
    mh.TenMon, hk.TenHocKy, cn.TenChuyenNganh, lh.TenLop
ORDER BY 
    hk.TenHocKy, cn.TenChuyenNganh, lh.TenLop, mh.TenMon";



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Tạo SqlCommand với truy vấn và thêm các tham số
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MonHoc", DBNull.Value);  // Thêm tham số nếu có giá trị cụ thể
                    command.Parameters.AddWithValue("@HocKy", DBNull.Value);   // Thêm tham số nếu có giá trị cụ thể
                    command.Parameters.AddWithValue("@ChuyenNganh", DBNull.Value); // Thêm tham số nếu có giá trị cụ thể
                    command.Parameters.AddWithValue("@LopHoc", DBNull.Value); // Thêm tham số nếu có giá trị cụ thể

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
    }
}
