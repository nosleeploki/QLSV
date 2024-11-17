using QLSV.DSLHoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV.DSLHoc
{
    public partial class DSLHoc : Form
    {
        public DSLHoc()
        {
            InitializeComponent();
            LoadData();
            dataDSLH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataDSLH.MultiSelect = false; // Chỉ cho phép chọn một hàng (nếu cần)
        }

        private void DSLHoc_Load(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = "SELECT MaLop, MaMon, MaHocKy, TenLop FROM dbo.Lop_Hoc WHERE DaXoa = 0";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataDSLH.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            AddLH addForm = new AddLH();
            addForm.FormClosed += (s, args) => LoadData(); // Tải lại dữ liệu khi form đóng
            addForm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataDSLH.CurrentRow != null)
            {
                int MaLop = Convert.ToInt32(dataDSLH.CurrentRow.Cells["MaLop"].Value);
                EditDSLH editForm = new EditDSLH(MaLop);
                editForm.FormClosed += (s, args) => LoadData(); // Tải lại dữ liệu khi form đóng
                editForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn lớp học để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void EditDSLH_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataDSLH.CurrentRow != null)
            {
                int maLop = Convert.ToInt32(dataDSLH.CurrentRow.Cells["MaLop"].Value);
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa lớp học này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
                    string query = "UPDATE dbo.Lop_Hoc SET DaXoa = 1 WHERE MaLop = @MaLop";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@MaLop", maLop);
                                command.ExecuteNonQuery();

                                MessageBox.Show("Xóa lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData(); // Tải lại dữ liệu sau khi xóa
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi xảy ra khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn lớp học để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult Thoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Thoat == DialogResult.Yes)
            {
                Application.Exit();
            }    
        }

        private void btnThemVoLop_Click(object sender, EventArgs e)
        {
            AddDSSVvaoLopHoc add = new AddDSSVvaoLopHoc();
            add.Show();
        }

        private void btnDS_Click(object sender, EventArgs e)
        {
            if (dataDSLH.SelectedRows.Count > 0)
            {
                // Lấy mã lớp học từ dòng được chọn
                int maLop = Convert.ToInt32(dataDSLH.SelectedRows[0].Cells["MaLop"].Value);

                // Lấy danh sách sinh viên và điểm
                DataTable dtSinhVienDiem = GetSinhVienDiem(maLop);

                // Hiển thị trên form ViewDSSVTrongLopHoc
                if (dtSinhVienDiem != null)
                {
                    ViewDSSVTrongLopHoc form = new ViewDSSVTrongLopHoc(dtSinhVienDiem);
                    form.Show();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lớp học để xem danh sách sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private DataTable GetSinhVienDiem(int maLop)
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
            string query = @"
                            SELECT sv.MaSinhVien, 
                                   sv.Ho + ' ' + sv.Ten AS HoTen, 
                                   md.DiemGiuaKy, 
                                   md.DiemCuoiKy, 
                                   md.DiemTongKet, 
                                   md.DiemBaiTap1, 
                                   md.DiemBaiTap2, 
                                   md.DiemLab1, 
                                   md.DiemLab2,
                                   md.DiemChuyenCan
                            FROM Sinh_Vien sv
                            INNER JOIN Ghi_Danh gd ON sv.MaSinhVien = gd.MaSinhVien
                            INNER JOIN Diem md ON sv.MaSinhVien = md.MaSinhVien AND gd.MaLop = md.MaLop
                            WHERE gd.MaLop = @MaLop";

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaLop", maLop);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message);
                }
            }
            return dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataDSLH.Rows[e.RowIndex].Selected = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataDSLH.SelectedRows.Count > 0)
            {
                int maLop = Convert.ToInt32(dataDSLH.SelectedRows[0].Cells["MaLop"].Value);

                // Khởi tạo form SVCheckin và truyền MaLop
                SVCheckin checkinForm = new SVCheckin(maLop);
                checkinForm.ShowDialog(); // Mở form SVCheckin
            }
            else
            {
                MessageBox.Show("Vui lòng chọn lớp học để xem thông tin điểm danh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
