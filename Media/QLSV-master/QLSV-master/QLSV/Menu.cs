using QLSV.DSLHoc;
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
        private Dictionary<Button, Point> buttonOriginalPositions = new Dictionary<Button, Point>();
        private Button lastMovedButton = null;
        public Menu()
        {
            InitializeComponent();
            label1.BackColor = Color.FromArgb(49, 75, 255);
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
            ApplyButtonStyles(this);
            // Lưu vị trí ban đầu và đăng ký sự kiện cho tất cả các nút
            StoreButtonOriginalPositions(this);
            RegisterButtonClickEvent(this);
        }

        private void ApplyButtonStyles(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control is Button button)
                {
                    Style.ApplyStyle(button);
                }
                else if (control.HasChildren)
                {
                    ApplyButtonStyles(control); // Đệ quy vào các container khác (như Panel)
                }
            }
        }

        private void StoreButtonOriginalPositions(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control is Button button)
                {
                    buttonOriginalPositions[button] = button.Location;
                }
                else if (control.HasChildren)
                {
                    StoreButtonOriginalPositions(control); // Đệ quy cho các container con
                }
            }
        }

        private void RegisterButtonClickEvent(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control is Button button)
                {
                    button.Click += Button_Click;
                }
                else if (control.HasChildren)
                {
                    RegisterButtonClickEvent(control); // Đệ quy cho các container con
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                // Nếu nút đang được click là nút trước đó, không làm gì thêm
                if (lastMovedButton == clickedButton)
                    return;

                // Đưa nút trước đó (nếu có) về vị trí ban đầu
                if (lastMovedButton != null)
                {
                    lastMovedButton.Location = buttonOriginalPositions[lastMovedButton];
                }

                // Dịch chuyển nút hiện tại
                MoveButtonRight(clickedButton, 20); // Dịch 20 pixel sang phải
                lastMovedButton = clickedButton; // Lưu lại nút vừa di chuyển
            }
        }

        // Phương thức dịch chuyển nút
        private void MoveButtonRight(Button button, int step)
        {
            button.Location = new Point(button.Location.X + step, button.Location.Y);
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
            loadMenu(new DSLHoc.DSLHoc());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
