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

namespace QLSV.DSLHoc
{
    public partial class DSSV : Form
    {
        private DataTable dataTable;
        private int maLop;
        public DSSV(int maLop, DataTable dt)
        {
            InitializeComponent();
            this.maLop = maLop;
            this.dataTable = dt;
        }

        private void DSSV_Load(object sender, EventArgs e)
        {
            // Đặt DataTable làm nguồn dữ liệu cho DataGridView
            dataGridView1.DataSource = dataTable;

            // Tùy chỉnh DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;

  
        }

  

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void UpdateScoreInDatabase(int maLoaiDiem, double newScore)
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = @"
            UPDATE Diem
            SET GiaTriDiem = @GiaTriDiem
            WHERE MaLoaiDiem = @MaLoaiDiem";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    // Thêm tham số MaLoaiDiem và GiaTriDiem vào câu lệnh SQL
                    command.Parameters.AddWithValue("@MaLoaiDiem", maLoaiDiem);
                    command.Parameters.AddWithValue("@GiaTriDiem", newScore);

                    command.ExecuteNonQuery();
                }
            }
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Lấy thông tin từ dòng được chọn
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];

                // Giả sử cột MaLoaiDiem và GiaTriDiem tồn tại trong DataGridView
                int maLoaiDiem = Convert.ToInt32(row.Cells["MaLoaiDiem"].Value);
                double giaTriDiem = Convert.ToDouble(row.Cells["GiaTriDiem"].Value);

                // Mở form chỉnh sửa điểm
                editScore editForm = new editScore(maLoaiDiem, giaTriDiem);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Lấy giá trị điểm mới từ form chỉnh sửa
                    double newScore = editForm.GiaTriDiem;

                    // Cập nhật lại dữ liệu trong cơ sở dữ liệu
                    UpdateScoreInDatabase(maLoaiDiem, newScore);

                    // Cập nhật lại DataGridView với giá trị điểm mới
                    row.Cells["GiaTriDiem"].Value = newScore;
                }
            }
        }
    }
}
