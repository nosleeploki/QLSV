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
    public partial class frmEdit : Form
    {
        public event EventHandler DataUpdated;
        private int maNhomMon;
        public frmEdit(int maNhomMon)
        {
            InitializeComponent();
            this.maNhomMon = maNhomMon;
            LoadNMData();

        }

        private void LoadNMData()
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Câu lệnh SQL để lấy thông tin sinh viên
            string query = "SELECT * FROM Nhom_Mon_Hoc WHERE MaNhomMon = @MaNhomMon";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNhomMon", maNhomMon);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Gán dữ liệu vào các TextBox
                                txtTenCN.Text = reader["TenNhomMon"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lưu thông tin sinh viên vào cơ sở dữ liệu
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "UPDATE Nhom_Mon_Hoc SET TenNhomMon = @TenNhomMon WHERE MaNhomMon = @MaNhomMon";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNhomMon", maNhomMon); // Tham số để xác định sinh viên cần cập nhật
                        command.Parameters.AddWithValue("@TenNhomMon", txtTenCN.Text);


                        command.ExecuteNonQuery();
                    }

                    // Gọi sự kiện để thông báo rằng dữ liệu đã được cập nhật
                    DataUpdated?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }
    }
}
