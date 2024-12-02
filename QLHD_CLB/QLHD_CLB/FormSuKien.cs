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
    public partial class FormSuKien : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private FormGiaoDien parentForm;

        public FormSuKien()
        {
            InitializeComponent();
        }

        public FormSuKien(FormGiaoDien _parentForm)
        {
            InitializeComponent();
            parentForm = _parentForm;
        }

        private void ClearAllTextBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear(); // Xóa nội dung TextBox
                }
                else if (control.HasChildren)
                {
                    // Gọi đệ quy để xử lý các control con
                    ClearAllTextBoxes(control);
                }
            }
        }


        private void LienKetDuLieu(DataTable dt)
        {
            txtTenSK.DataBindings.Clear();
            txtMoTa.DataBindings.Clear();
            txtDuChi.DataBindings.Clear();
            txtDiaDiem.DataBindings.Clear();
            gioBD.DataBindings.Clear();
            gioKT.DataBindings.Clear();
            dateNgayBD.DataBindings.Clear();
            dateNgayKT.DataBindings.Clear();

            txtMaSK.DataBindings.Add("Text", dt, "MaSuKien");
            txtTenSK.DataBindings.Add("Text", dt, "TenSuKien");
            txtMoTa.DataBindings.Add("Text", dt, "MoTa");
            txtDuChi.DataBindings.Add("Text", dt, "NganSachDuChi");
            txtDiaDiem.DataBindings.Add("Text", dt, "DiaDiem");

            dateNgayBD.DataBindings.Add("Value", dt, "NgayBatDau");
            dateNgayBD.Checked = true;
            dateNgayKT.DataBindings.Add("Value", dt, "NgayKetThuc");
            dateNgayKT.Checked = true;

            gioBD.Text = dateNgayBD.Value.ToString("HH:mm");
            gioKT.Text = dateNgayKT.Value.ToString("HH:mm");
        }

        private void HuyLienKet()
        {
            txtMaSK.DataBindings.Clear();
            txtTenSK.DataBindings.Clear();
            txtMoTa.DataBindings.Clear();
            txtDuChi.DataBindings.Clear();
            txtDiaDiem.DataBindings.Clear();
            dateNgayBD.DataBindings.Clear();
            dateNgayBD.Checked = false;
            dateNgayBD.Value = DateTime.Now;
            dateNgayKT.DataBindings.Clear();
            dateNgayKT.Checked = false;
            dateNgayKT.Value = DateTime.Now;
            gioBD.Text = "";
            gioKT.Text = "";
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormSuKien_Load(object sender, EventArgs e)
        {
            DBConnect data = new DBConnect();
            string sql = "select * from SuKien";
            dgv.DataSource = data.getSqlDataAdapter(sql);

            dateTKNgayBD.Checked = false;
            dateTKNgayKT.Checked = false;
            dateNgayBD.Checked = false;
            dateNgayKT.Checked = false;
            
        }

        private void grTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void btnTImKiem_Click(object sender, EventArgs e)
        {
            string ten = txtTKTenSK.Text;
            string diaDiem = txtTKDiaDiem.Text;
            string bd = dateTKNgayBD.Value.ToString("yyyy-MM-dd");
            string kt = dateTKNgayKT.Value.ToString("yyyy-MM-dd");
            string chitieuStart = txtTKChiTieuStart.Text;
            string chitieuEnd = txtTKChiTieuEnd.Text;

            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(ten))
            {
                conditions.Add("LOWER(TenSuKien) LIKE N'%" + ten + "%'");
            }

            if (!string.IsNullOrEmpty(diaDiem))
            {
                conditions.Add("LOWER(DiaDiem) LIKE N'%" + diaDiem + "%'");
            }
            if (dateTKNgayBD.Checked)
            {
                conditions.Add("NgayBatDau >= '" + bd + "'");
            }

            if (dateTKNgayKT.Checked)
            {
                conditions.Add("NgayKetThuc <= '" + kt + "'");
            }

            if (!string.IsNullOrEmpty(chitieuStart) || !string.IsNullOrEmpty(chitieuEnd))
            {
                // Xử lý trường hợp cả hai giá trị đều có
                if (!string.IsNullOrEmpty(chitieuStart) && !string.IsNullOrEmpty(chitieuEnd))
                {
                    conditions.Add("(NganSachDuChi BETWEEN " + chitieuStart + " AND " + chitieuEnd +
                                   " OR ChiTieuThucTe BETWEEN " + chitieuStart + " AND " + chitieuEnd + ")");
                }
                // Chỉ có giá trị bắt đầu
                else if (!string.IsNullOrEmpty(chitieuStart))
                {
                    conditions.Add("(NganSachDuChi >= " + chitieuStart + " OR ChiTieuThucTe >= " + chitieuStart + ")");
                }
                // Chỉ có giá trị kết thúc
                else if (!string.IsNullOrEmpty(chitieuEnd))
                {
                    conditions.Add("(NganSachDuChi <= " + chitieuEnd + " OR ChiTieuThucTe <= " + chitieuEnd + ")");
                }
            }

            string sql = "SELECT * FROM SuKien";

            if (conditions.Count > 0)
            {
                sql += " WHERE " + string.Join(" AND ", conditions);
            }

            // Kết nối và hiển thị dữ liệu
            DBConnect data = new DBConnect();
            dgv.DataSource = data.getSqlDataAdapter(sql);

            ClearAllTextBoxes(grTimKiem);
            dateTKNgayBD.Checked = false;
            dateTKNgayKT.Checked = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các control
            string ten = txtTenSK.Text.Trim();
            string diaDiem = txtDiaDiem.Text.Trim();
            string tgianBD = gioBD.Text.Trim();
            string tgianKT = gioKT.Text.Trim();

            // Kiểm tra và chuyển đổi ngân sách dự chi
            if (string.IsNullOrWhiteSpace(txtDuChi.Text))
            {
                txtDuChi.Text = "0"; // Mặc định giá trị là 0 nếu trống
            }

            if (!double.TryParse(txtDuChi.Text, out double duchi))
            {
                MessageBox.Show("Vui lòng nhập một giá trị số hợp lệ cho Ngân sách dự chi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDuChi.Focus();
                return;
            }

            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên sự kiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSK.Focus();
                return;
            }

            if (string.IsNullOrEmpty(diaDiem))
            {
                MessageBox.Show("Vui lòng nhập địa điểm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaDiem.Focus();
                return;
            }

            // Kiểm tra ngày bắt đầu và ngày kết thúc
            if (!dateNgayBD.Checked || !dateNgayKT.Checked)
            {
                MessageBox.Show("Vui lòng chọn ngày bắt đầu và ngày kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateNgayBD.Value > dateNgayKT.Value)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateNgayBD.Focus();
                return;
            }

            // Kết hợp ngày và giờ
            string bd;
            string kt;

            try
            {
                DateTime ngayBD = dateNgayBD.Value.Date; // Lấy chỉ ngày
                DateTime ngayKT = dateNgayKT.Value.Date; // Lấy chỉ ngày
                TimeSpan gioBatDau = TimeSpan.Parse(tgianBD); // Chuyển đổi giờ bắt đầu
                TimeSpan gioKetThuc = TimeSpan.Parse(tgianKT); // Chuyển đổi giờ kết thúc

                bd = ngayBD.Add(gioBatDau).ToString("yyyy-MM-dd HH:mm:ss"); // Kết hợp ngày và giờ bắt đầu
                kt = ngayKT.Add(gioKetThuc).ToString("yyyy-MM-dd HH:mm:ss"); // Kết hợp ngày và giờ kết thúc
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giờ bắt đầu và giờ kết thúc đúng định dạng (hh:mm).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (duchi < 0)
            {
                MessageBox.Show("Ngân sách dự chi không được nhỏ hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDuChi.Focus();
                return;
            }

            // Tạo mã sự kiện tự động
            DBConnect data = new DBConnect();
            var ln = data.getScalar("SELECT MAX(MaSuKien) AS MA FROM SuKien");
            string ma;

            if (ln != null && ln != DBNull.Value)
            {
                string lastCode = ln.ToString();
                int lastNumber = int.Parse(lastCode.Substring(2)); // Lấy phần sau "SK"
                ma = "SK" + (lastNumber + 1).ToString("D3");      // Tăng số lên 1 và định dạng 3 chữ số
            }
            else
            {
                ma = "SK001";
            }

            string mota = txtMoTa.Text.Trim();

            // Tạo câu truy vấn
            string sql = "INSERT INTO SuKien(MaSuKien, TenSuKien, DiaDiem, NgayBatDau, NgayKetThuc, NganSachDuChi, MoTa) " +
                         "VALUES('" + ma + "', N'" + ten + "', N'" + diaDiem + "', '" + bd + "', '" + kt + "', " + duchi + ", N'" + mota + "')";

            // Thực hiện truy vấn
            int k = data.getNonQuery(sql);
            if (k == 0)
            {
                MessageBox.Show("Thêm sự kiện thất bại, hãy kiểm tra dữ liệu nhập vào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Thêm sự kiện mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllTextBoxes(grThem); // Reset các textbox
                FormSuKien_Load(sender, e); // Reload lại dữ liệu
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgv.DataSource;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LienKetDuLieu(dt);
            grThem.Text = "Sửa sự kiện";
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Visible = false;
            btnLuu.Visible = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ma = txtMaSK.Text.Trim();
            string ten = txtTenSK.Text.Trim();
            string diaDiem = txtDiaDiem.Text.Trim();
            string tgianBD = gioBD.Text.Trim();
            string tgianKT = gioKT.Text.Trim();
            string mota = txtMoTa.Text.Trim();

            // Kiểm tra và chuyển đổi ngân sách dự chi
            if (string.IsNullOrWhiteSpace(txtDuChi.Text))
            {
                txtDuChi.Text = "0"; // Mặc định giá trị là 0 nếu trống
            }

            if (!double.TryParse(txtDuChi.Text, out double duchi))
            {
                MessageBox.Show("Vui lòng nhập một giá trị số hợp lệ cho Ngân sách dự chi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDuChi.Focus();
                return;
            }

            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên sự kiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSK.Focus();
                return;
            }

            if (string.IsNullOrEmpty(diaDiem))
            {
                MessageBox.Show("Vui lòng nhập địa điểm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaDiem.Focus();
                return;
            }

            // Kiểm tra ngày bắt đầu và ngày kết thúc
            if (!dateNgayBD.Checked || !dateNgayKT.Checked)
            {
                MessageBox.Show("Vui lòng chọn ngày bắt đầu và ngày kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateNgayBD.Value.Date > dateNgayKT.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateNgayBD.Focus();
                return;
            }

            // Kết hợp ngày và giờ
            string bd;
            string kt;

            try
            {
                DateTime ngayBD = dateNgayBD.Value.Date; // Lấy chỉ ngày
                DateTime ngayKT = dateNgayKT.Value.Date; // Lấy chỉ ngày
                TimeSpan gioBatDau = TimeSpan.Parse(tgianBD); // Chuyển đổi giờ bắt đầu
                TimeSpan gioKetThuc = TimeSpan.Parse(tgianKT); // Chuyển đổi giờ kết thúc

                bd = ngayBD.Add(gioBatDau).ToString("yyyy-MM-dd HH:mm:ss"); // Kết hợp ngày và giờ bắt đầu
                kt = ngayKT.Add(gioKetThuc).ToString("yyyy-MM-dd HH:mm:ss"); // Kết hợp ngày và giờ kết thúc
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giờ bắt đầu và giờ kết thúc đúng định dạng (hh:mm).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (duchi < 0)
            {
                MessageBox.Show("Ngân sách dự chi không được nhỏ hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDuChi.Focus();
                return;
            }

            string sql = "UPDATE SuKien SET " +
             "TenSuKien = N'" + ten + "', " +
             "DiaDiem = N'" + diaDiem + "', " +
             "NgayBatDau = '" + bd + "', " +
             "NgayKetThuc = '" + kt + "', " +
             "NganSachDuChi = " + duchi + ", " +
             "MoTa = N'" + mota + "' " +
             "WHERE MaSuKien = '" + ma + "'";

            DBConnect data = new DBConnect();
            int k = data.getNonQuery(sql);
            if (k == 0)
            {
                MessageBox.Show("Sửa sự kiện thất bại, hãy kiểm tra dữ liệu nhập vào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Sửa sự kiện thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllTextBoxes(grThem); // Reset các textbox
                FormSuKien_Load(sender, e); // Reload lại dữ liệu
                HuyLienKet();
                btnThem.Enabled = true;
                btnLuu.Enabled = false;
                btnThem.Visible = true;
                btnLuu.Visible = false;
                grThem.Text = "Thêm sự kiện";
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            txtMaSK.DataBindings.Clear();
            txtMaSK.DataBindings.Add("Text", dgv.DataSource, "MaSuKien");
            GlobalValue.MaSuKien = txtMaSK.Text;
            parentForm.container(new FormSuKien_ChiTiet(parentForm));
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            HuyLienKet();
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnThem.Visible = true;
            btnLuu.Visible = false;
            grThem.Text = "Thêm sự kiện";
            ClearAllTextBoxes(grThem);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtMaSK.DataBindings.Clear();
            DataTable dt = (DataTable)dgv.DataSource;
            LienKetDuLieu(dt);
            string sql = "DELETE FROM SuKien WHERE MaSuKien = '" + txtMaSK.Text + "'";
            DBConnect data = new DBConnect();
            int k = data.getNonQuery(sql);

            if (k == 0)
            {
                MessageBox.Show("Xóa sự kiện thất bại");
            }
            else
            {
                MessageBox.Show("Xóa sự kiện thành công");
                ClearAllTextBoxes(grThem);
                HuyLienKet();
                FormSuKien_Load(sender, e);
            }
            HuyLienKet();
        }
    }
}
