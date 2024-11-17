﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV.DSLHoc
{
    public partial class ViewDSSVTrongLopHoc : Form
    {
        private DataTable danhSachSinhVien;
        private string TenLop;

        public ViewDSSVTrongLopHoc(DataTable danhSachSinhVien, string TenLop)
        {
            InitializeComponent();
            this.danhSachSinhVien = danhSachSinhVien;
            this.TenLop = TenLop;
            label2.Text = TenLop;  // Display the class name in Label2

        }

        private void ViewDSSVTrongLopHoc_Load(object sender, EventArgs e)
        {

            // Gán dữ liệu cho DataGridView
            dataGridView1.DataSource = danhSachSinhVien;

            // Đặt tên các cột cho thân thiện với người dùng
            if (danhSachSinhVien.Columns.Contains("MaSinhVien"))
                dataGridView1.Columns["MaSinhVien"].HeaderText = "Mã Sinh Viên";
            if (danhSachSinhVien.Columns.Contains("HoTen"))
                dataGridView1.Columns["HoTen"].HeaderText = "Họ và Tên";
            if (danhSachSinhVien.Columns.Contains("DiemGiuaKy"))
                dataGridView1.Columns["DiemGiuaKy"].HeaderText = "Điểm Giữa Kỳ";
            if (danhSachSinhVien.Columns.Contains("DiemCuoiKy"))
                dataGridView1.Columns["DiemCuoiKy"].HeaderText = "Điểm Cuối Kỳ";
            if (danhSachSinhVien.Columns.Contains("DiemBaiTap1"))
                dataGridView1.Columns["DiemBaiTap1"].HeaderText = "Bài Tập 1";
            if (danhSachSinhVien.Columns.Contains("DiemBaiTap2"))
                dataGridView1.Columns["DiemBaiTap2"].HeaderText = "Bài Tập 2";
            if (danhSachSinhVien.Columns.Contains("DiemLab1"))
                dataGridView1.Columns["DiemLab1"].HeaderText = "Lab 1";
            if (danhSachSinhVien.Columns.Contains("DiemLab2"))
                dataGridView1.Columns["DiemLab2"].HeaderText = "Lab 2";
            if (danhSachSinhVien.Columns.Contains("DiemChuyenCan"))
                dataGridView1.Columns["DiemChuyenCan"].HeaderText = "Điểm Chuyên Cần";
            if (danhSachSinhVien.Columns.Contains("DiemTongKet"))
                dataGridView1.Columns["DiemTongKet"].HeaderText = "Tổng Điểm";

            // Tùy chỉnh chế độ hiển thị
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = false; // Cho phép chỉnh sửa
            dataGridView1.AllowUserToAddRows = false; // Không cho phép thêm dòng mới
            
            if (dataGridView1.Columns["MaSinhVien"] != null)
            {
                dataGridView1.Columns["MaSinhVien"].ReadOnly = true;
            }

            if (dataGridView1.Columns["MaLop"] != null)
            {
                dataGridView1.Columns["MaLop"].ReadOnly = true;
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

                // Lấy các giá trị từ DataGridView
                int maSinhVien = Convert.ToInt32(row.Cells["MaSinhVien"].Value);
                int diemGiuaKy = Convert.ToInt32(row.Cells["DiemGiuaKy"].Value);
                int diemCuoiKy = Convert.ToInt32(row.Cells["DiemCuoiKy"].Value);
                int diemBaiTap1 = Convert.ToInt32(row.Cells["DiemBaiTap1"].Value);
                int diemBaiTap2 = Convert.ToInt32(row.Cells["DiemBaiTap2"].Value);
                int diemLab1 = Convert.ToInt32(row.Cells["DiemLab1"].Value);
                int diemLab2 = Convert.ToInt32(row.Cells["DiemLab2"].Value);
                int diemChuyenCan = Convert.ToInt32(row.Cells["DiemChuyenCan"].Value);

                // Tính tổng điểm dưới dạng double
                double tongDiem = (diemChuyenCan * 1 + diemGiuaKy * 2 + diemCuoiKy * 3 +
                                   diemBaiTap1 * 2 + diemBaiTap2 * 2 + diemLab1 * 2 + diemLab2 * 2) / 14.0;

                // Chuyển tổng điểm thành kiểu int (làm tròn giá trị)
                int diemTongKet = Convert.ToInt32(tongDiem); // Làm tròn và chuyển sang int

                // Xác định trạng thái pass/trượt
                int pass = diemTongKet >= 5 ? 1 : 0;

                // Lấy MaLop từ cơ sở dữ liệu
                int maLop = -1;  // Khởi tạo với giá trị mặc định
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Lấy MaLop từ cơ sở dữ liệu
                        string query = "SELECT MaLop FROM Diem WHERE MaSinhVien = @MaSinhVien";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);

                            var result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                maLop = Convert.ToInt32(result);
                            }
                            else
                            {
                                MessageBox.Show($"Không tìm thấy lớp cho sinh viên có mã {maSinhVien}.");
                                continue;  // Nếu không tìm thấy MaLop, tiếp tục vòng lặp
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lấy MaLop từ cơ sở dữ liệu: " + ex.Message);
                        continue;  // Nếu có lỗi khi truy vấn, tiếp tục vòng lặp
                    }
                }

                // Câu truy vấn cập nhật
                string updateQuery = @"
                            UPDATE Diem 
                            SET 
                                DiemGiuaKy = @DiemGiuaKy,
                                DiemCuoiKy = @DiemCuoiKy,
                                DiemTongKet = @DiemTongKet,
                                DiemBaiTap1 = @DiemBaiTap1,
                                DiemBaiTap2 = @DiemBaiTap2,
                                DiemLab1 = @DiemLab1,
                                DiemLab2 = @DiemLab2
                            WHERE MaSinhVien = @MaSinhVien AND MaLop = @MaLop";

                // Kết nối và thực hiện truy vấn cập nhật
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                        command.Parameters.AddWithValue("@MaLop", maLop);  // Sử dụng MaLop lấy từ cơ sở dữ liệu
                        command.Parameters.AddWithValue("@DiemGiuaKy", diemGiuaKy);
                        command.Parameters.AddWithValue("@DiemCuoiKy", diemCuoiKy);
                        command.Parameters.AddWithValue("@DiemTongKet", diemTongKet); // Sử dụng diemTongKet kiểu int
                        command.Parameters.AddWithValue("@DiemBaiTap1", diemBaiTap1);
                        command.Parameters.AddWithValue("@DiemBaiTap2", diemBaiTap2);
                        command.Parameters.AddWithValue("@DiemLab1", diemLab1);
                        command.Parameters.AddWithValue("@DiemLab2", diemLab2);

                        command.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("Cập nhật điểm thành công!");
        }
    }
}
