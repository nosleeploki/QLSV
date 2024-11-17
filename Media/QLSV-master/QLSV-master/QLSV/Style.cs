using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    internal class Style
    {
        public static void ApplyStyle(Button button)
        {
            button.BackColor = Color.FromArgb(0, 122, 204); // Blue
            button.ForeColor = Color.White; // White text
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0; // No border
            button.Font = new Font("Segoe UI", 8, FontStyle.Bold); // Font style
        }

    }
}
