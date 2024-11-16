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
    public partial class frmChiTiet : Form
    {
        private int _maNhomMon;
        private string _connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QLSV;Integrated Security=True";
        public frmChiTiet()
        {
            InitializeComponent();
        }
        public frmChiTiet(int maNhomMon, string tenNhomMon)
        {
            InitializeComponent();
            _maNhomMon = maNhomMon;

            txtTenNMH.Text = tenNhomMon;
            LoadDanhSachMonHoc();
        }
        private void LoadDanhSachMonHoc()
        {
            string query = "SELECT MaMon, TenMon, SoTinChi FROM Mon_Hoc WHERE MaNhomMon = @MaNhomMon";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@MaNhomMon", _maNhomMon);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }
    }
}
