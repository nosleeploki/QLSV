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

namespace QLSV.DSLHoc
{
    public partial class NhapDiem : Form
    {
        private int MaLop; // Mã lớp học
        private SqlConnection connection;

        public NhapDiem(int maLop)
        {
            InitializeComponent();
            MaLop = maLop;
            connection = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True"); // Kết nối đến cơ sở dữ liệu
        }

        private void LoadSinhVienData()
        {
            string query = @"
                SELECT s.MaSinhVien, s.Ho, s.Ten, gd.MaLop
                FROM Sinh_Vien s
                INNER JOIN Ghi_Danh gd ON s.MaSinhVien = gd.MaSinhVien
                WHERE gd.MaLop = @MaLop";

            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.Parameters.AddWithValue("@MaLop", MaLop);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void NhapDiem_Load(object sender, EventArgs e)
        {
            LoadSinhVienData(); // Load dữ liệu sinh viên
        }
     


      
       

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Lấy mã sinh viên và điểm nhập từ các ô trong hàng
                int maSinhVien = Convert.ToInt32(row.Cells["MaSinhVien"].Value);
                decimal diem = Convert.ToDecimal(row.Cells["Diem"].Value); // Cột điểm cần được thêm vào DataGridView

                // Lưu điểm vào bảng Diem
                string query = @"
                SELECT s.MaSinhVien, s.Ho, s.Ten, gd.MaLop, ISNULL(d.GiaTriDiem, 0) AS Diem
                FROM Sinh_Vien s
                INNER JOIN Ghi_Danh gd ON s.MaSinhVien = gd.MaSinhVien
                LEFT JOIN Diem d ON s.MaSinhVien = d.MaSinhVien AND d.MaMon = @MaMon
                WHERE gd.MaLop = @MaLop";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                command.Parameters.AddWithValue("@MaLoaiDiem", 1); // Giả sử bạn sẽ nhập loại điểm mặc định
                command.Parameters.AddWithValue("@GiaTriDiem", diem);
                command.Parameters.AddWithValue("@MaMon", 1); // Giả sử bạn đang làm việc với môn học có mã 1

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            MessageBox.Show("Điểm đã được lưu thành công!");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
