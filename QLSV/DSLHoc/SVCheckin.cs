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
    public partial class SVCheckin : Form
    {
        private int _maLop; // Mã lớp học được truyền vào
        private DataTable _dataTable; // Lưu dữ liệu tạm
        public SVCheckin(int maLop)
        {
            InitializeComponent();
            _maLop = maLop; // Gán mã lớp
            LoadData(); // Tải thông tin điểm danh
        }
        private void LoadData()
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = @"
                SELECT gd.MaSinhVien, 
                       sv.Ho + ' ' + sv.Ten AS HoTen, 
                       gd.SoLanVangMat
                FROM Ghi_Danh gd
                INNER JOIN Sinh_Vien sv ON gd.MaSinhVien = sv.MaSinhVien
                WHERE gd.MaLop = @MaLop";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaLop", _maLop);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        _dataTable = new DataTable();
                        adapter.Fill(_dataTable);

                        dataGridView1.DataSource = _dataTable;

                        // Đặt tiêu đề cột
                        dataGridView1.Columns["MaSinhVien"].HeaderText = "Mã SV";
                        dataGridView1.Columns["HoTen"].HeaderText = "Họ Tên";
                        dataGridView1.Columns["SoLanVangMat"].HeaderText = "Số tiết vắng mặt";

                        // Chỉ cho phép sửa cột "SoLanVangMat"
                        dataGridView1.Columns["MaSinhVien"].ReadOnly = true;
                        dataGridView1.Columns["HoTen"].ReadOnly = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }

            //
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = false; // Cho phép chỉnh sửa
            dataGridView1.AllowUserToAddRows = false; // Không cho phép thêm dòng mới

            if (dataGridView1.Columns["MaSinhVien"] != null)
            {
                dataGridView1.Columns["MaSinhVien"].ReadOnly = true;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                // Lấy thông tin từ DataGridView
                int maSinhVien = Convert.ToInt32(row.Cells["MaSinhVien"].Value);
                int soLanVangMat = Convert.ToInt32(row.Cells["SoLanVangMat"].Value);

                // Lấy số buổi học từ bảng Mon_Hoc dựa trên lớp
                int soBuoiHoc = GetSoBuoiHoc(_maLop, connectionString);

                // Tính điểm chuyên cần
                double diemChuyenCan = Math.Round(((double)(soBuoiHoc - soLanVangMat) / soBuoiHoc) * 10, 2);

                // Cập nhật số lần vắng mặt và điểm chuyên cần
                UpdateSoLanVangMatVaDiemChuyenCan(connectionString, maSinhVien, _maLop, soLanVangMat, diemChuyenCan);

                // Tính và cập nhật trạng thái Pass (nếu DiemChuyenCan < 7, Pass = 0; ngược lại Pass = 1)
                int pass = diemChuyenCan < 7 ? 0 : 1;
                UpdatePass(connectionString, maSinhVien, _maLop, pass);
            }

            MessageBox.Show("Cập nhật điểm chuyên cần thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        // Hàm lấy số buổi học từ bảng Mon_Hoc dựa vào mã lớp
        private int GetSoBuoiHoc(int maLop, string connectionString)
        {
            int soBuoiHoc = 0;
            string query = @"
        SELECT mh.SoBuoi
        FROM Lop_Hoc lh
        JOIN Mon_Hoc mh ON lh.MaMon = mh.MaMon
        WHERE lh.MaLop = @MaLop";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaLop", maLop);
                        soBuoiHoc = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy số buổi học: " + ex.Message);
                }
            }

            return soBuoiHoc;
        }

        private void UpdatePass(string connectionString, int maSinhVien, int maLop, int pass)
        {
            string query = @"
                            UPDATE Diem
                            SET Pass = @Pass
                            WHERE MaSinhVien = @MaSinhVien AND MaLop = @MaLop;
                        ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                        command.Parameters.AddWithValue("@MaLop", maLop);
                        command.Parameters.AddWithValue("@Pass", pass);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật trạng thái Pass: " + ex.Message);
                }
            }
        }

        private void UpdateSoLanVangMatVaDiemChuyenCan(string connectionString, int maSinhVien, int maLop, int soLanVangMat, double diemChuyenCan)
        {
            string query = @"
                            UPDATE Ghi_Danh
                            SET SoLanVangMat = @SoLanVangMat
                            WHERE MaSinhVien = @MaSinhVien AND MaLop = @MaLop;

                            UPDATE Diem
                            SET DiemChuyenCan = @DiemChuyenCan
                            WHERE MaSinhVien = @MaSinhVien AND MaLop = @MaLop;
                        ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                        command.Parameters.AddWithValue("@MaLop", maLop);
                        command.Parameters.AddWithValue("@SoLanVangMat", soLanVangMat);
                        command.Parameters.AddWithValue("@DiemChuyenCan", diemChuyenCan);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message);
                }
            }
        }
    }
}
