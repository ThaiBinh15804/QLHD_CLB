using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLHD_CLB.Model;


namespace QLHD_CLB
{
    public partial class FormDangNhap : Form
    {
        private bool ValidateInputs()
        {
            // Không được để trống
            if (string.IsNullOrWhiteSpace(txtTK.Text))
            {
                Message.Show("Tài khoản không được để trống!");
                txtTK.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMK.Text))
            {
                Message.Show("Mật khẩu không được để trống!");
                txtMK.Focus();
                return false;
            }

            // Độ dài tối thiểu
            if (txtTK.Text.Length < 6)
            {
                Message.Show("Tài khoản phải có ít nhất 6 ký tự!");
                txtTK.Focus();
                return false;
            }

            if (txtMK.Text.Length < 6)
            {
                Message.Show("Mật khẩu phải có ít nhất 6 ký tự!");
                txtMK.Focus();
                return false;
            }

            // Ký tự hợp lệ
            if (!Regex.IsMatch(txtTK.Text, "^[a-zA-Z0-9]+$"))
            {
                Message.Show("Tài khoản chỉ được chứa ký tự chữ và số!");
                txtTK.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtMK.Text, "^[a-zA-Z0-9@#$%^&+=]+$"))
            {
                Message.Show("Mật khẩu chỉ được chứa ký tự chữ, số, và các ký tự đặc biệt: @#$%^&+=");
                txtMK.Focus();
                return false;
            }

            return true; // Đạt tất cả các điều kiện
        }
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            txtTK.Focus();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tk = txtTK.Text;
            string mk = txtMK.Text;
            //string tk = "nguyenvana";
            //string mk = "123456";

            //if (!ValidateInputs())
            //{
            //    txtTK.Text = "";
            //    txtMK.Text = "";
            //    txtTK.Focus();
            //    return;
            //}

            DBConnect data = new DBConnect();
            string sql = "SELECT * FROM NguoiDung WHERE TenTaiKhoan = '" + tk + "' AND MatKhau = '" + mk + "'";
            DataTable dt = data.getSqlDataAdapter(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                GlobalValue.Ma_NguoiDung = dt.Rows[0]["MaNguoiDung"].ToString();
                GlobalValue.HoTen_NguoiDung = dt.Rows[0]["HoTen"].ToString();
                GlobalValue.AnhDaiDien_NguoiDung = dt.Rows[0]["AnhDaiDien"].ToString();
                Message.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                Message.Show("Đăng nhập thành công");

                FormGiaoDien p = new FormGiaoDien();
                p.Show();
                this.Hide();
            }
            else
            {
                Message.Show("Đăng nhập thất bại");
                txtTK.Text = "";
                txtMK.Text = "";
                txtTK.Focus();
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
