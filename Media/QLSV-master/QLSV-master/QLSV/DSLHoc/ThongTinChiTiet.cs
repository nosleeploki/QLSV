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
    public partial class ThongTinChiTiet : Form
    {
        private int MaLop;
        public ThongTinChiTiet(int maLop)
        {
            InitializeComponent();
            this.MaLop = maLop;
            LoadLopHocData();
        }

        private void ThongTinChiTiet_Load(object sender, EventArgs e)
        {

        }
        private void LoadLopHocData()
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT * FROM Lop_Hoc WHERE MaLop = @MaLop";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaLop", MaLop);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Giả sử bạn có các TextBox hoặc Label để hiển thị thông tin
                                txtMaLop.Text = reader["MaLop"].ToString();
                                cboMaMon.Text = reader["MaMon"].ToString();
                                cboMaHocKi.Text = reader["MaHocKy"].ToString();
                                txtTenLop.Text = reader["TenLop"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải thông tin lớp học: " + ex.Message);
                }
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {

        }
    }
}

