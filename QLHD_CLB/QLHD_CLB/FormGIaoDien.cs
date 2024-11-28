using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLHD_CLB.Model;
using System.IO;

namespace QLHD_CLB
{
    public partial class FormGIaoDien : Form
    {
        public FormGIaoDien()
        {
            InitializeComponent();
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

        private void FormGIaoDien_Load(object sender, EventArgs e)
        {
            label_title_page.Text = "Thống kê";
            label_title_page.Font = new Font("Segoe UI", 16, FontStyle.Bold); // Đặt font Arial, kích thước 16, kiểu chữ thường
            container(new FormThongKe());
            label_TenNguoiDung.Text = GlobalValue.HoTen_NguoiDung;

            string relativePath = @"HinhAnh\AnhDaiDien\"; // Đường dẫn tương đối từ thư mục gốc dự án
            string projectDirectory = Directory.GetParent(Application.StartupPath).Parent.FullName; // Quay lại 3 cấp
            string absolutePath = Path.Combine(projectDirectory, relativePath, GlobalValue.AnhDaiDien_NguoiDung);

            if (File.Exists(absolutePath))
            {
                guna2CirclePictureBox1.Image = Image.FromFile(absolutePath);
            }
            else
            {
                MessageBox.Show("Ảnh đại diện không tồn tại!");
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            label_title_page.Text = "Thống kê";
            label_title_page.Font = new Font("Segoe UI", 16, FontStyle.Bold); // Đặt font Arial, kích thước 16, kiểu chữ thường
            container(new FormThongKe());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            label_title_page.Text = "Quản lý ban";
            label_title_page.Font = new Font("Segoe UI", 16, FontStyle.Bold); // Đặt font Arial, kích thước 16, kiểu chữ thường
            container(new FormBanCLB());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            label_title_page.Text = "Quản lý chức vụ";
            label_title_page.Font = new Font("Segoe UI", 16, FontStyle.Bold); // Đặt font Arial, kích thước 16, kiểu chữ thường
            container(new FormChucVuCLB());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            label_title_page.Text = "Quản lý người dùng";
            label_title_page.Font = new Font("Segoe UI", 16, FontStyle.Bold); // Đặt font Arial, kích thước 16, kiểu chữ thường
            container(new FormNguoiDungCLB());
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            label_title_page.Text = "Quản lý thành viên";
            label_title_page.Font = new Font("Segoe UI", 16, FontStyle.Bold); // Đặt font Arial, kích thước 16, kiểu chữ thường
            container(new FormThanhVien());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            label_title_page.Text = "Quản lý sự kiện";
            label_title_page.Font = new Font("Segoe UI", 16, FontStyle.Bold); // Đặt font Arial, kích thước 16, kiểu chữ thường
            container(new FormSuKien());
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            label_title_page.Text = "Quản lý nhà tài trợ";
            label_title_page.Font = new Font("Segoe UI", 16, FontStyle.Bold); // Đặt font Arial, kích thước 16, kiểu chữ thường
            container(new FormNhaTaiTro());
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            label_title_page.Text = "Quản lý đóng quỹ";
            label_title_page.Font = new Font("Segoe UI", 16, FontStyle.Bold); // Đặt font Arial, kích thước 16, kiểu chữ thường
            container(new FormDongQuy());
        }

        private void guna2Panel_sidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            FormDangNhap formDangNhap = new FormDangNhap();
            formDangNhap.Show();
            this.Close();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
