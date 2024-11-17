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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QLSV.HocKy
{
    public partial class frmAdd : Form
    {
        public frmAdd()
        {
            InitializeComponent();
        }

        public event EventHandler DataUpdated;

        private void frmAdd_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string maHocKy= ""; // Mã sinh viên tự động sẽ được tạo
            string ten = txtTenHK.Text;
            string nam = txtNam.Text;

            // Kiểm tra các TextBox có rỗng không
            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(nam))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin học kỳ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuỗi kết nối cơ sở dữ liệu
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Câu lệnh SQL để tìm MaHocKy cao nhất hiện có
            string queryGetMaxMaHocKy = "SELECT MAX(MaHocKy) FROM Hoc_Ky";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Tạo một câu lệnh SQL để lấy mã học kỳ lớn nhất
                    SqlCommand cmdMaxMaHocKy = new SqlCommand(queryGetMaxMaHocKy, connection);
                    var result = cmdMaxMaHocKy.ExecuteScalar();
                    int nextMaHocKy = result == DBNull.Value ? 1 : Convert.ToInt32(result) + 1;

                    // Gán giá trị cho MaHocKy mới
                    maHocKy = nextMaHocKy.ToString(); // MaHocKy tự động tăng, ví dụ: "1", "2",...

                    // Câu lệnh SQL để thêm thông tin học kỳ
                    string queryInsert = "INSERT INTO Hoc_Ky (MaHocKy, TenHocKy, Nam, DaXoa) " +
                                 "VALUES (@MaHocKy, @TenHocKy, @Nam, 0);";

                    using (SqlCommand command = new SqlCommand(queryInsert, connection))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        command.Parameters.AddWithValue("@MaHocKy", maHocKy);
                        command.Parameters.AddWithValue("@TenHocKy", ten);
                        command.Parameters.AddWithValue("@Nam", nam);

                        // Thực thi câu lệnh SQL
                        command.ExecuteNonQuery();

                        // Thông báo thành công
                        MessageBox.Show("Thêm học kỳ thành công!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Kích hoạt sự kiện DataUpdated nếu có form nào lắng nghe
                        DataUpdated?.Invoke(this, EventArgs.Empty);

                        // Đóng form hiện tại
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (kq == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
