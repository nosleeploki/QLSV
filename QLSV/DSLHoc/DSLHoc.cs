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

namespace BaiTap.DSLHoc
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

        private void btnNhapDiem_Click(object sender, EventArgs e)
        {
            NhapDiem nhapDiem = new NhapDiem();
            nhapDiem.Show();
        }

        private void btnDS_Click(object sender, EventArgs e)
        {
            if (dataDSLH.SelectedRows.Count > 0)
            {
                // Lấy MaLop của lớp được chọn
                int maLop = Convert.ToInt32(dataDSLH.SelectedRows[0].Cells["MaLop"].Value);

                // Chuỗi kết nối cơ sở dữ liệu
                string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";

                // Truy vấn để lấy danh sách sinh viên trong lớp học cùng với loại điểm
                string query = @"
            SELECT 
                MaSinhVien, 
                HoTen,
                [Điểm Giữa Kỳ] AS DiemGiuaKy,
                [Điểm Cuối Kỳ] AS DiemCuoiKy,
                [Điểm Chuyên Cần] AS DiemChuyenCan,
                [Điểm thi kết thúc môn] AS DiemThiKetThucMon
            FROM 
            (
                SELECT 
                    SV.MaSinhVien, 
                    SV.Ho + ' ' + SV.Ten AS HoTen, 
                    LD.TenLoaiDiem, 
                    D.GiaTriDiem
                FROM 
                    Sinh_Vien SV 
                INNER JOIN 
                    Ghi_Danh GD ON SV.MaSinhVien = GD.MaSinhVien 
                INNER JOIN 
                    Lop_Hoc LH ON GD.MaLop = LH.MaLop
                LEFT JOIN 
                    Diem D ON SV.MaSinhVien = D.MaSinhVien
                LEFT JOIN 
                    Loai_Dau_Diem LD ON D.MaLoaiDiem = LD.MaLoaiDiem
                WHERE 
                    LH.MaLop = @MaLop
            ) AS SourceTable
            PIVOT
            (
                MAX(GiaTriDiem)
                FOR TenLoaiDiem IN ([Điểm Giữa Kỳ], [Điểm Cuối Kỳ], [Điểm Chuyên Cần], [Điểm thi kết thúc môn])
            ) AS PivotTable;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@MaLop", maLop);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Hiển thị danh sách sinh viên trong một form mới
                        Form danhSachSinhVienForm = new Form();
                        danhSachSinhVienForm.Text = "Danh Sách Sinh Viên Trong Lớp";

                        DataGridView dgvSinhVien = new DataGridView
                        {
                            DataSource = dataTable,
                            Dock = DockStyle.Fill,
                            ReadOnly = true,
                            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                        };

                        danhSachSinhVienForm.Controls.Add(dgvSinhVien);
                        danhSachSinhVienForm.Size = new Size(600, 400); // Tăng kích thước để đủ hiển thị
                        danhSachSinhVienForm.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi xảy ra khi lấy danh sách sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lớp học để xem danh sách sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataDSLH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataDSLH.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}
