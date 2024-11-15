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
    public partial class frmAdd : Form
    {
        public event EventHandler DataUpdated;

        public frmAdd()
        {
            InitializeComponent();
        }

        private void frmAdd_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox và điều khiển khác
            string maNhomMon = ""; // Mã sinh viên tự động sẽ được tạo
            string tenNhomMon = txtTenCN.Text;


            // Kiểm tra các TextBox có rỗng không
            if (string.IsNullOrEmpty(tenNhomMon))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuỗi kết nối cơ sở dữ liệu
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Câu lệnh SQL để tìm MaSinhVien cao nhất hiện có
            string queryGetMaxID = "SELECT MAX(MaNhomMon) FROM Nhom_Mon_Hoc";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Tạo một câu lệnh SQL để lấy mã sinh viên lớn nhất
                    SqlCommand cmdMaxID = new SqlCommand(queryGetMaxID, connection);
                    var result = cmdMaxID.ExecuteScalar();
                    int nextMaNM = result == DBNull.Value ? 1 : Convert.ToInt32(result) + 1;

                    // Gán giá trị cho mã CN mới
                    maNhomMon = nextMaNM.ToString();


                    // Câu lệnh SQL để thêm thông tin sinh viên
                    string queryInsert = "INSERT INTO Nhom_Mon_Hoc (MaNhomMon, TenNhomMon, DaXoa) " +
                                 "VALUES (@MaNhomMon, @TenNhomMon, 0);";

                    using (SqlCommand command = new SqlCommand(queryInsert, connection))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        command.Parameters.AddWithValue("@MaNhomMon", maNhomMon);
                        command.Parameters.AddWithValue("@TenNhomMon", tenNhomMon);

                        // Thực thi câu lệnh SQL
                        command.ExecuteNonQuery();

                        // Thông báo thành công
                        MessageBox.Show("Thêm nhóm môn " + tenNhomMon + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
    }
}
