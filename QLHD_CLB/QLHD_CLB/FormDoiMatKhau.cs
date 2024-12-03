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

namespace QLHD_CLB
{
    public partial class FormDoiMatKhau : Form
    {

        private FormGiaoDien parentForm;

        public FormDoiMatKhau()
        {
            InitializeComponent();
        }

        public FormDoiMatKhau(FormGiaoDien _parentForm)
        {
            InitializeComponent();
            this.parentForm = _parentForm;
        }

        private void FormDoiMatKhau_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            parentForm.container(new FormThongKe());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string matKhauCu = txtMKCu.Text;
            string matKhauMoi = txtMKMoi.Text;
            string xacNhanMatKhau = txtMKMoiNL.Text;

            if (matKhauCu == "" || matKhauMoi == "" || xacNhanMatKhau == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (matKhauMoi != xacNhanMatKhau)
            {
                MessageBox.Show("Mật khẩu mới không khớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string sql = "SELECT MatKhau FROM NguoiDung WHERE MaNguoiDung = '" + GlobalValue.Ma_NguoiDung + "'";
                DBConnect data = new DBConnect();
                var mkcu = data.getScalar(sql).ToString();

                if (mkcu != matKhauCu)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sqlUpdate = "UPDATE NguoiDung SET MatKhau = '"+ matKhauMoi +"' WHERE MaNguoiDung = '"+ GlobalValue.Ma_NguoiDung +"'";
                    int k = data.getNonQuery(sqlUpdate);

                    if (k > 0)
                    {
                        MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        parentForm.container(new FormThongKe());
                    }
                    else
                    {
                        MessageBox.Show("Đổi mật khẩu thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    }

            }
        }
    }
}
