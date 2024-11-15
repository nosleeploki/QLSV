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
    public partial class frmEdit : Form
    {
        public event EventHandler DataUpdated;
        private int maChuyenNganh;
        public frmEdit(int maChuyenNganh)
        {
            InitializeComponent();
            this.maChuyenNganh = maChuyenNganh;
            LoadCNData();

        }

        private void LoadCNData()
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            // Câu lệnh SQL để lấy thông tin sinh viên
            string query = "SELECT * FROM Chuyen_Nganh WHERE MaChuyenNganh = @MaChuyenNganh";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaChuyenNganh", maChuyenNganh);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Gán dữ liệu vào các TextBox
                                txtTenCN.Text = reader["TenChuyenNganh"].ToString();                                
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
            string query = "UPDATE Chuyen_Nganh SET TenChuyenNganh = @TenChuyenNganh WHERE MaChuyenNganh = @MaChuyenNganh";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaChuyenNganh", maChuyenNganh); // Tham số để xác định sinh viên cần cập nhật
                        command.Parameters.AddWithValue("@TenChuyenNganh", txtTenCN.Text);
                       

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
