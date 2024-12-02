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
    public partial class FormGiaoDien : Form
    {
        private Stack<Form> backStack = new Stack<Form>();
        private Stack<Form> forwardStack = new Stack<Form>();

        public FormGiaoDien()
        {
            InitializeComponent();
        }

        private void ShowForm(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            guna2Panel_container.Controls.Add(form);
            guna2Panel_container.Tag = form;
            form.Show();
        }

        // Hiển thị form mới
        public void container(object _form)
        {
            if (guna2Panel_container.Controls.Count > 0)
            {
                Form currentForm = guna2Panel_container.Controls[0] as Form;

                backStack.Push(currentForm);
                forwardStack.Clear();

                guna2Panel_container.Controls.Clear();
            }

            ShowForm(_form as Form);
        }


        private void FormGIaoDien_Load(object sender, EventArgs e)
        {
            container(new FormSuKien(this));
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
            container(new FormThongKe());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            container(new FormBanCLB());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            container(new FormChucVuCLB());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            container(new FormNguoiDungCLB());
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            container(new FormThanhVien());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            container(new FormSuKien(this));
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            container(new FormNhaTaiTro(this));
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
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

        private void guna2Panel_container_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BackForm_Click(object sender, EventArgs e)
        {
            if (backStack.Count > 0)
            {
                Form cur = guna2Panel_container.Controls[0] as Form;
                guna2Panel_container.Controls.Clear();

                forwardStack.Push(cur);

                Form pre = backStack.Pop();
                ShowForm(pre);

            }
        }

        private void ForwardForm_Click(object sender, EventArgs e)
        {
            if (forwardStack.Count > 0)
            {
                Form cur = guna2Panel_container.Controls[0] as Form;
                guna2Panel_container.Controls.Clear();

                backStack.Push(cur);

                Form pre = forwardStack.Pop();
                ShowForm(pre);

            }
        }


    }
}
