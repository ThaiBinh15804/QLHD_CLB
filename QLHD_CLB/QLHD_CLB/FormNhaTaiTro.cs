using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QLHD_CLB.Model;

namespace QLHD_CLB
{
    public partial class FormNhaTaiTro : Form
    {
        private FormGiaoDien parentForm;


        public FormNhaTaiTro()
        {
            InitializeComponent();
        }

        // Constructor để tham chiếu đến form cha
        public FormNhaTaiTro(FormGiaoDien _parentForm)
        {
            InitializeComponent();
            parentForm = _parentForm;
        }

        private void SetControlsEnabledFalse(Control container)
        {
            foreach (Control control in container.Controls)
            {
                // Đặt thuộc tính Enabled = false
                control.Enabled = false;

                // Nếu control chứa các control con, gọi đệ quy
                if (control.HasChildren)
                {
                    SetControlsEnabledFalse(control);
                }
            }
        }


        private void LienKetDuLieu(DataTable dt)
        {
            txtMaNTT.DataBindings.Clear();
            txtTenNTT.DataBindings.Clear();
            txtSDTNTT.DataBindings.Clear();
            txtEmailNTT.DataBindings.Clear();
            txtDiaChiNTT.DataBindings.Clear();
            txtGioiThieuNTT.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();

            txtMaNTT.DataBindings.Add("Text", dt, "MaNhaTaiTro");
            txtTenNTT.DataBindings.Add("Text", dt, "TenNhaTaiTro");
            txtSDTNTT.DataBindings.Add("Text", dt, "SoDienThoai");
            txtEmailNTT.DataBindings.Add("Text", dt, "Email");
            txtDiaChiNTT.DataBindings.Add("Text", dt, "DiaChi");
            txtGioiThieuNTT.DataBindings.Add("Text", dt, "GioiThieu");
            txtGhiChu.DataBindings.Add("Text", dt, "GhiChu");
        }

        private void HuyLienKet()
        {
            txtTenNTT.DataBindings.Clear();
            txtSDTNTT.DataBindings.Clear();
            txtEmailNTT.DataBindings.Clear();
            txtDiaChiNTT.DataBindings.Clear();
            txtGioiThieuNTT.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();
        }

        private void ClearAllTextBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Clear(); // Xóa nội dung TextBox
                }
                else if (control.HasChildren)
                {
                    // Gọi đệ quy để xử lý các control con
                    ClearAllTextBoxes(control);
                }
            }
        }



        private void FormNhaTaiTro_Load(object sender, EventArgs e)
        {
            if (GlobalValue.ChucVu_NguoiDung == "CV004" || GlobalValue.ChucVu_NguoiDung == "CV005")
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                SetControlsEnabledFalse(grNTT);
            }    

            DBConnect data = new DBConnect();

            string sql = "SELECT * FROM NhaTaiTro;";
            DataTable dt = data.getSqlDataAdapter(sql);
            dgvDSNTT.DataSource = dt;


            sql = "SELECT DISTINCT DiaChi FROM NhaTaiTro";
            cbDiaChi.DataSource = data.getSqlDataAdapter(sql);
            cbDiaChi.DisplayMember = "DiaChi";
            cbDiaChi.ValueMember = "DiaChi";
            cbDiaChi.SelectedIndex = -1;

        }

        private void dgvDSNTT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ten = txtTenNTT.Text.Trim();
            string sdt = txtSDTNTT.Text.Trim();
            string email = txtEmailNTT.Text.Trim();
            string dc = txtDiaChiNTT.Text.Trim();
            string gt = txtGioiThieuNTT.Text.Trim();
            string ghichu = txtGhiChu.Text.Trim();

            // Kiểm tra xem các trường có bị trống không
            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(dc) || string.IsNullOrEmpty(gt) || string.IsNullOrEmpty(ghichu))
            {
                //parentForm.PreProcessControlMessage(ref Message).icon
                Message.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                Message.Show("Vui lòng điền đầy đủ tất cả các trường.");
                return; // Dừng lại và không thực hiện tiếp các bước dưới
            }

            DBConnect data = new DBConnect();

            var ln = data.getScalar("SELECT MAX(MaNhaTaiTro) AS MA FROM NhaTaiTro");

            string ma;

            if (ln != null && ln != DBNull.Value)
            {
                string lastCode = ln.ToString();

                // Trích xuất phần số và tăng thêm 1
                int lastNumber = int.Parse(lastCode.Substring(3)); // Lấy phần sau "NTT"
                ma = "NTT" + (lastNumber + 1).ToString("D3");  // Tăng số lên 1 và định dạng 3 chữ số
            }
            else
            {
                ma = "NTT001";
            }


            string sql = "INSERT INTO NhaTaiTro VALUES ('" + ma + "', N'" + ten + "', N'" + gt + "', '" + sdt + "', '" + email + "', N'" + dc + "', N'" + ghichu + "')";

            int k = data.getNonQuery(sql);

            if (k == 0)
            {
                Message.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                MessageBox.Show("Thêm nhà tài trợ thất bại, hãy đảm bảo điền đủ các trường và đúng định dạng");
            }
            else
            {
                Message.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                MessageBox.Show("Thêm nhà tài trợ thành công");
                ClearAllTextBoxes(grNTT);
                FormNhaTaiTro_Load(sender, e);
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string ten = txtTen.Text.ToLower();
            string sdt = txtSDT.Text.ToLower();
            string diachi = "";
            if (cbDiaChi.SelectedValue != null)
            {
                diachi = cbDiaChi.SelectedValue.ToString().ToLower();
            }

            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(ten))
            {
                conditions.Add("LOWER(TenNhaTaiTro) LIKE N'%" + ten + "%'");
            }

            if (!string.IsNullOrEmpty(sdt))
            {
                conditions.Add("LOWER(SoDienThoai) LIKE N'%" + sdt + "%'");
            }

            if (!string.IsNullOrEmpty(diachi))
            {
                conditions.Add("LOWER(DiaChi) LIKE N'%" + diachi + "%'");
            }

            string sql = "SELECT * FROM NhaTaiTro";

            if (conditions.Count > 0)
            {
                sql += " WHERE " + string.Join(" AND ", conditions);
            }

            DBConnect data = new DBConnect();

            dgvDSNTT.DataSource = data.getSqlDataAdapter(sql);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvDSNTT.DataSource;
            LienKetDuLieu(dt);
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            grNTT.Text = "Sửa thông tin nhà tài trợ";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ma = txtMaNTT.Text.Trim();
            string ten = txtTenNTT.Text.Trim();
            string sdt = txtSDTNTT.Text.Trim();
            string email = txtEmailNTT.Text.Trim();
            string dc = txtDiaChiNTT.Text.Trim();
            string gt = txtGioiThieuNTT.Text.Trim();
            string ghichu = txtGhiChu.Text.Trim();


            // Kiểm tra xem các trường có bị trống không
            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(dc) || string.IsNullOrEmpty(gt) || string.IsNullOrEmpty(ghichu))
            {
                //parentForm.PreProcessControlMessage(ref Message).icon
                Message.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                Message.Show("Vui lòng điền đầy đủ tất cả các trường.");
                return; // Dừng lại và không thực hiện tiếp các bước dưới
            }

            string sql = "UPDATE NhaTaiTro SET " +
             "TenNhaTaiTro = N'" + ten + "', " +
             "GioiThieu = N'" + gt + "', " +
             "SoDienThoai = '" + sdt + "', " +
             "Email = '" + email + "', " +
             "DiaChi = N'" + dc + "', " +
             "GhiChu = N'" + ghichu + "' " +
             "WHERE MaNhaTaiTro = '" + ma + "'";


            DBConnect data = new DBConnect();

            int k = data.getNonQuery(sql);

            if (k == 0)
            {
                MessageBox.Show("Chỉnh sửa nhà tài trợ thất bại, hãy đảm bảo điền đủ các trường và đúng định dạng");
            }
            else
            {
                MessageBox.Show("Chỉnh sửa nhà tài trợ thành công");
                ClearAllTextBoxes(grNTT);
                HuyLienKet();
                btnThem.Enabled = true;
                btnLuu.Enabled = false;
                grNTT.Text = "Thêm thông tin nhà tài trợ";
                FormNhaTaiTro_Load(sender, e);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes(grNTT);
            HuyLienKet();
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtMaNTT.DataBindings.Clear();
            DataTable dt = (DataTable)dgvDSNTT.DataSource;
            LienKetDuLieu(dt);
            string sql = "DELETE FROM NhaTaiTro WHERE MaNhaTaiTro = '" + txtMaNTT.Text + "'";
            DBConnect data = new DBConnect();
            int k = data.getNonQuery(sql);

            if (k == 0)
            {
                MessageBox.Show("Xóa nhà tài trợ thất bại");
            }
            else
            {
                MessageBox.Show("Xóa nhà tài trợ thành công");
                ClearAllTextBoxes(grNTT);
                HuyLienKet();
                FormNhaTaiTro_Load(sender, e);
            }
            HuyLienKet();
        }
    }
}
