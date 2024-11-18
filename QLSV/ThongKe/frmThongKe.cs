using QLSV.HocKy;
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

namespace QLSV.ThongKe
{
    public partial class frmThongKe : Form
    {
        private string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

        public frmThongKe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các ComboBox
            string maHocKy = cboHocKy.SelectedValue.ToString();
            string maChuyenNganh = cboChuyenNganh.SelectedValue.ToString();
            string maMon = cboMonHoc.SelectedValue.ToString();
            string maLop = cboLop.SelectedValue.ToString();

            // Kết nối đến cơ sở dữ liệu
          // Thay bằng chuỗi kết nối của bạn
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                                SELECT 
                                    SUM(CASE WHEN d.Pass = 1 THEN 1 ELSE 0 END) AS PassCount,
                                    SUM(CASE WHEN d.Pass = 0 THEN 1 ELSE 0 END) AS FailCount
                                FROM Diem d
                                JOIN Sinh_Vien sv ON d.MaSinhVien = sv.MaSinhVien
                                JOIN Lop_Hoc l ON sv.MaLop = l.MaLop
                                WHERE d.MaMon = @MaMon
                                  AND sv.MaChuyenNganh = @MaChuyenNganh
                                  AND l.MaHocKy = @MaHocKy
                                  AND l.MaLop = @MaLop;
                            ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                cmd.Parameters.AddWithValue("@MaChuyenNganh", maChuyenNganh);
                cmd.Parameters.AddWithValue("@MaMon", maMon);
                cmd.Parameters.AddWithValue("@MaLop", maLop);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int passCount = reader.GetInt32(0);
                    int failCount = reader.GetInt32(1);

                    // Hiển thị kết quả lên Panel hoặc Label
                    panel2.Controls.Clear();
                    Label labelPass = new Label
                    {
                        Text = $"Pass: {passCount}",
                        Location = new Point(10, 10),
                        AutoSize = true
                    };
                    Label labelFail = new Label
                    {
                        Text = $"Fail: {failCount}",
                        Location = new Point(10, 40),
                        AutoSize = true
                    };
                    panel2.Controls.Add(labelPass);
                    panel2.Controls.Add(labelFail);
                    UpdateChart(passCount, failCount);
                }
                conn.Close();
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UpdateChart(int passCount, int failCount)
        {
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series1"].Points.AddXY("Pass", passCount);
            chart1.Series["Series1"].Points.AddXY("Fail", failCount);
        }
        private void frmThongKe_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Lấy dữ liệu cho ComboBox Ngành
                SqlDataAdapter adapterChuyenNganh = new SqlDataAdapter("SELECT MaChuyenNganh, TenChuyenNganh FROM Chuyen_Nganh WHERE DaXoa = 0", conn);
                DataTable dtChuyenNganh = new DataTable();
                adapterChuyenNganh.Fill(dtChuyenNganh);
                cboChuyenNganh.DataSource = dtChuyenNganh;
                cboChuyenNganh.DisplayMember = "TenChuyenNganh";
                cboChuyenNganh.ValueMember = "MaChuyenNganh";

                // Lấy dữ liệu cho ComboBox Học Kỳ
                SqlDataAdapter adapterHocKy = new SqlDataAdapter("SELECT MaHocKy, TenHocKy FROM Hoc_Ky WHERE DaXoa = 0", conn);
                DataTable dtHocKy = new DataTable();
                adapterHocKy.Fill(dtHocKy);
                cboHocKy.DataSource = dtHocKy;
                cboHocKy.DisplayMember = "TenHocKy";
                cboHocKy.ValueMember = "MaHocKy";

                // Lấy dữ liệu cho ComboBox Môn Học
                SqlDataAdapter adapterMonHoc = new SqlDataAdapter("SELECT MaMon, TenMon FROM Mon_Hoc WHERE DaXoa = 0", conn);
                DataTable dtMonHoc = new DataTable();
                adapterMonHoc.Fill(dtMonHoc);
                cboMonHoc.DataSource = dtMonHoc;
                cboMonHoc.DisplayMember = "TenMon";
                cboMonHoc.ValueMember = "MaMon";

                // Lấy dữ liệu cho ComboBox Lớp
                SqlDataAdapter adapterLop = new SqlDataAdapter("SELECT MaLop, TenLop FROM Lop_Hoc WHERE DaXoa = 0", conn);
                DataTable dtLop = new DataTable();
                adapterLop.Fill(dtLop);
                cboLop.DataSource = dtLop;
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";
            }
        }
    }
}
