using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLHD_CLB
{
    public partial class FormGIaoDien : Form
    {
        public FormGIaoDien()
        {
            InitializeComponent();
        }

        private void guna2Panel2_top_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormGIaoDien_Load(object sender, EventArgs e)
        {
            label_title_page.Text = "Thống kê";
            label_title_page.Font = new Font("Segoe UI", 16, FontStyle.Bold); // Đặt font Arial, kích thước 16, kiểu chữ thường
            container(new FormThongKe());
        }

        private void container(object _form)
        {
            if (guna2Panel_container.Controls.Count > 0) guna2Panel_container.Controls.Clear();

            Form fm = _form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            guna2Panel_container.Controls.Add(fm);
            guna2Panel_container.Tag = fm;
            fm.Show();
        }
    }
}
