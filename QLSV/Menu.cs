﻿using BaiTap.DSLHoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        public void loadMenu(object Form)
        {
            if (this.mainPanel.Controls.Count > 0)
                this.mainPanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(f);
            this.mainPanel.Tag = f;
            f.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadMenu(new DSSV.DSSV());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadMenu(new ChuyenNganh.frmCN());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            loadMenu(new NhomMonHoc.frmNhom());
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            loadMenu(new DSSV.ThongKe());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadMenu(new HocKy.HocKy());

        }

        private void btnLopHoc_Click(object sender, EventArgs e)
        {
            loadMenu(new DSLHoc());
        }
    }
}
