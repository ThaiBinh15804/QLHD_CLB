using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QLHD_CLB.Model;
using System.Data.SqlClient;
using Guna.Charts.WinForms;
using System.Diagnostics.Eventing.Reader;

namespace QLHD_CLB
{
    public partial class FormSuKien_ChiTiet : Form
    {
        private FormGiaoDien parentForm;
        private string fileNameHoaDon;
        private bool isEditPhanCong = false;

        public FormSuKien_ChiTiet()
        {
            InitializeComponent();
        }

        public FormSuKien_ChiTiet(FormGiaoDien _parentForm)
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

        private void SetControlsEnabledTrue(Control container)
        {
            foreach (Control control in container.Controls)
            {
                // Đặt thuộc tính Enabled = false
                control.Enabled = true;

                // Nếu control chứa các control con, gọi đệ quy
                if (control.HasChildren)
                {
                    SetControlsEnabledTrue(control);
                }
            }
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


        private void LienKet_TaiTro(DataTable dt)
        {
            cbNhaTT.DataBindings.Clear();
            cbHangTT.DataBindings.Clear();
            txtHienKim.DataBindings.Clear();
            txtHienVat.DataBindings.Clear();
            txtTongTien.DataBindings.Clear();

            cbNhaTT.DataBindings.Add("Text", dt, "Tên nhà tài trợ");
            cbHangTT.DataBindings.Add("SelectedValue", dt, "Hạng tài trợ");
            txtHienKim.DataBindings.Add("Text", dt, "Hiện kim");
            txtHienVat.DataBindings.Add("Text", dt, "Hiện vật");
            txtTongTien.DataBindings.Add("Text", dt, "Tổng tài trợ");

        }


        private void HuyLienKet_TaiTro()
        {

            cbNhaTT.DataBindings.Clear();
            cbHangTT.DataBindings.Clear();
            txtHienKim.DataBindings.Clear();
            txtHienVat.DataBindings.Clear();
            txtTongTien.DataBindings.Clear();

        }

        private void UpdatePictureBoxImage(string fileName)
        {
            try
            {
                string relativePath = @"HinhAnh\"; // Đường dẫn tương đối từ thư mục gốc dự án
                string projectDirectory = Directory.GetParent(Application.StartupPath).Parent.FullName; // Thư mục gốc
                string absolutePath = Path.Combine(projectDirectory, relativePath, fileName);

                if (File.Exists(absolutePath))
                {
                    // Sử dụng MemoryStream để không khóa file
                    using (var stream = new MemoryStream(File.ReadAllBytes(absolutePath)))
                    {
                        picHoaDon_CT.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    picHoaDon_CT.Image = null; // Xóa ảnh nếu file không tồn tại
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                picHoaDon_CT.Image = null;
            }
        }

        private void LienKet_ChiTieu(DataTable dt)
        {
            txtMa_CT.DataBindings.Clear();
            cbHTTT_CT.DataBindings.Clear();
            txtLyDo_CT.DataBindings.Clear();
            txtDuChi_CT.DataBindings.Clear();
            txtThucChi_CT.DataBindings.Clear();
            picHoaDon_CT.DataBindings.Clear();
            txtfileName.DataBindings.Clear();

            txtMa_CT.DataBindings.Add("Text", dt, "Mã chi tiêu");
            cbHTTT_CT.DataBindings.Add("Text", dt, "Hình thức thanh toán");
            txtLyDo_CT.DataBindings.Add("Text", dt, "Lý do chi tiêu");
            txtDuChi_CT.DataBindings.Add("Text", dt, "Dự chi");
            txtThucChi_CT.DataBindings.Add("Text", dt, "Thực chi");
            txtfileName.DataBindings.Add("Text", dt, "Ảnh hoá đơn");
            dateNgayTT_CT.DataBindings.Add("Value", dt, "Ngày thực hiện");
            dateNgayTT_CT.Checked = true;

            fileNameHoaDon = txtfileName.Text;

            // Lắng nghe sự kiện PositionChanged của BindingManagerBase để cập nhật ảnh
            BindingManagerBase bindingManager = this.BindingContext[dt];
            bindingManager.PositionChanged += (sender, e) =>
            {
                UpdatePictureBoxImage(txtfileName.Text);
            };

            // Tải ảnh lần đầu khi liên kết
            UpdatePictureBoxImage(txtfileName.Text);
        }

        private void HuyLienKet_ChiTieu()
        {
            txtMa_CT.DataBindings.Clear();
            cbHTTT_CT.DataBindings.Clear();
            txtLyDo_CT.DataBindings.Clear();
            txtDuChi_CT.DataBindings.Clear();
            txtThucChi_CT.DataBindings.Clear();
            txtfileName.DataBindings.Clear();
            dateNgayTT_CT.DataBindings.Clear();
            dateNgayTT_CT.Value = DateTime.Now;
            dateNgayTT_CT.Checked = false;
            picHoaDon_CT.Image = null;
            fileNameHoaDon = null;
        }

        private void LoadTabThongTin()
        {
            string sql = "SELECT * FROM SuKien WHERE MaSuKien = '" + GlobalValue.MaSuKien + "'";
            DBConnect data = new DBConnect();
            DataTable dt = data.getSqlDataAdapter(sql);
            if (dt.Rows.Count > 0)
            {
                lbMaSK.Text = dt.Rows[0]["MaSuKien"].ToString();
                lbTenSK.Text = dt.Rows[0]["TenSuKien"].ToString();
                lbDiaDiem.Text = dt.Rows[0]["DiaDiem"].ToString();
                lbTgian.Text = "Từ   " + dt.Rows[0]["NgayBatDau"].ToString() + "  đến  " + dt.Rows[0]["NgayKetThuc"].ToString();
                lbMoTa.Text = dt.Rows[0]["MoTa"].ToString();
                // Xử lý định dạng lbDuChi
                decimal nganSachDuChi;
                if (decimal.TryParse(dt.Rows[0]["NganSachDuChi"].ToString(), out nganSachDuChi))
                {
                    lbDuChi.Text = nganSachDuChi.ToString("N0");
                }
                else
                {
                    lbDuChi.Text = "0";
                }

                decimal chiTieuThucTe;
                // Xử lý định dạng lbChiTieu
                if (decimal.TryParse(dt.Rows[0]["ChiTieuThucTe"].ToString(), out chiTieuThucTe))
                {
                    lbChiTieu.Text = chiTieuThucTe.ToString("N0");
                }
                else
                {
                    lbChiTieu.Text = "0";
                }
            }

            sql = "SELECT COUNT(*) AS SoLuongPhanCong FROM PhanCong WHERE MaSuKien = '" + GlobalValue.MaSuKien + "';";
            var pc = int.Parse(data.getScalar(sql).ToString());
            if (pc > 0)
            {
                lbPhanCong.Text = "Đã tạo danh sách phân công. Chưa phân công chi tiết cho từng nhiệm vụ";
            }
            else
            {
                lbPhanCong.Text = "Chưa tạo danh sách phân công";
            }

            sql = "SELECT COUNT(*) AS SoLuongChiTietPhanCong FROM ChiTietPhanCong WHERE MaPhanCong IN (SELECT MaPhanCong FROM PhanCong WHERE MaSuKien = '" + GlobalValue.MaSuKien + "');";

            var ctpc = int.Parse(data.getScalar(sql).ToString());
            if (ctpc > 0)
            {
                lbPhanCong.Text = "Đã phân công chi tiết cho từng nhiệm vụ";
            }

            sql = "SElECT COUNT(*) FROM TaiTro WHERE MaSuKien = '" + GlobalValue.MaSuKien + "';";
            var tt = data.getScalar(sql).ToString();
            lbSoNTT.Text = tt;

            sql = "SElECT SUM(TongTaiTro) FROM TaiTro WHERE MaSuKien = '" + GlobalValue.MaSuKien + "';";
            decimal tongTaiTro;
            if (decimal.TryParse(data.getScalar(sql).ToString(), out tongTaiTro))
            {
                lbTongTienTT.Text = tongTaiTro.ToString("N0");
            }
            else
            {
                lbTongTienTT.Text = "0";
            }
        }

        private void LoadTabTaiTro()
        {
            string sql = "SELECT N.TenNhaTaiTro as N'Tên nhà tài trợ', T.HangTaiTro as N'Hạng tài trợ', T.HienKim as N'Hiện kim', T.HienVat as N'Hiện vật', T.TongTaiTro as N'Tổng tài trợ', T.NgayTaiTro as N'Ngày tài trợ'" +
                         "FROM TaiTro T JOIN NhaTaiTro N ON T.MaNhaTaiTro = N.MaNhaTaiTro " +
                         "WHERE T.MaSuKien = '" + GlobalValue.MaSuKien + "';";

            DBConnect data = new DBConnect();
            DataTable dt = data.getSqlDataAdapter(sql);

            DataTable tmp = dt.Copy();
            dgv.DataSource = dt;
            dgv.Columns["Hiện kim"].DefaultCellStyle.Format = "N0";
            dgv.Columns["Tổng tài trợ"].DefaultCellStyle.Format = "N0";

            sql = "SELECT MaNhaTaiTro, TenNhaTaiTro FROM NhaTaiTro";
            dt = data.getSqlDataAdapter(sql);

            cbNhaTT.DataSource = dt;
            cbNhaTT.DisplayMember = "TenNhaTaiTro";
            cbNhaTT.ValueMember = "MaNhaTaiTro";

            cbNhaTT.SelectedIndex = -1;


            DataTable dtForTKNhaTT = dt.Copy();
            DataRow emptyRow = dtForTKNhaTT.NewRow();
            emptyRow["MaNhaTaiTro"] = DBNull.Value;
            emptyRow["TenNhaTaiTro"] = "";
            dtForTKNhaTT.Rows.InsertAt(emptyRow, 0);

            cbTKNhaTT.DataSource = dtForTKNhaTT;
            cbTKNhaTT.DisplayMember = "TenNhaTaiTro";
            cbTKNhaTT.ValueMember = "MaNhaTaiTro";

            cbTKNhaTT.SelectedIndex = -1;

            Dictionary<string, string> nhaTaiTroDict = new Dictionary<string, string>
             {
                { "Vàng", "Nhà tài trợ Vàng" },
                { "Bạc", "Nhà tài trợ Bạc" },
                { "Đồng", "Nhà tài trợ Đồng" }
            };

            cbHangTT.DataSource = new BindingSource(nhaTaiTroDict, null);
            cbHangTT.DisplayMember = "Value";
            cbHangTT.ValueMember = "Key";

            cbHangTT.SelectedIndex = -1;

            if (GlobalValue.TrangThai_SuKien == "Ngừng hoạt động")
            {
                LienKet_TaiTro(dgv.DataSource as DataTable);
                SetControlsEnabledFalse(guna2Panel6);

                SetControlsEnabledFalse(grThongTin);
            }
        }

        private void LoadTabChiTieu()
        {
            string sql = "SELECT MaChiTieu as N'Mã chi tiêu', LyDoChiTieu as N'Lý do chi tiêu', HinhThucThanhToan as N'Hình thức thanh toán', DuChi as N'Dự chi', ThucChi as N'Thực chi', AnhHoaDon as N'Ảnh hoá đơn' ,NgayThucHien as N'Ngày thực hiện' FROM ChiTieu WHERE MaSuKien = '" + GlobalValue.MaSuKien + "'";

            DBConnect data = new DBConnect();
            DataTable dt = data.getSqlDataAdapter(sql);
            dgvCT.DataSource = dt;
            dgvCT.Columns["Dự chi"].DefaultCellStyle.Format = "N0";
            dgvCT.Columns["Thực chi"].DefaultCellStyle.Format = "N0";

            barThucChi.DataPoints.Clear();
            barDuChi.DataPoints.Clear();
            // Danh sách màu sắc
            Color[] thucChiColors = { Color.FromArgb(255, 99, 132), Color.FromArgb(54, 162, 235) }; // Màu cho Thực Chi
            Color[] duChiColors = { Color.FromArgb(75, 192, 192), Color.FromArgb(255, 206, 86) };   // Màu cho Dự Chi
            int colorIndex = 0;

            foreach (DataRow r in dt.Rows)
            {
                // Kiểm tra giá trị 'Thực chi' và 'Dự chi' có phải là null hay không
                double thucChi = 0;
                double duChi = 0;

                // Chỉ thực hiện phép toán nếu giá trị không phải null hoặc rỗng
                if (!string.IsNullOrEmpty(r["Thực chi"].ToString()))
                {
                    double.TryParse(r["Thực chi"].ToString(), out thucChi);
                }

                if (!string.IsNullOrEmpty(r["Dự chi"].ToString()))
                {
                    double.TryParse(r["Dự chi"].ToString(), out duChi);
                }

                // Thêm dữ liệu vào chart
                string label = r["Lý do chi tiêu"].ToString();
                barThucChi.DataPoints.Add(label, thucChi);
                barDuChi.DataPoints.Add(label, duChi);

                // Gán màu sắc cho từng cột
                barThucChi.FillColors.Add(thucChiColors[colorIndex % thucChiColors.Length]);
                barDuChi.FillColors.Add(duChiColors[colorIndex % duChiColors.Length]);

                colorIndex++;

            }

            dateNgayTT_CT.Checked = false;

            cbHTTT_CT.Items.Clear();
            cbTK_HTTT_CT.Items.Clear();

            cbTK_HTTT_CT.Items.Add("");
            cbTK_HTTT_CT.Items.Add("Chuyển khoản");
            cbTK_HTTT_CT.Items.Add("Tiền mặt");

            cbHTTT_CT.Items.Add("Chuyển khoản");
            cbHTTT_CT.Items.Add("Tiền mặt");

            if (GlobalValue.TrangThai_SuKien == "Ngừng hoạt động")
            {
                LienKet_ChiTieu(dgvCT.DataSource as DataTable);
                SetControlsEnabledFalse(grThongTin_ChiTieu);

                SetControlsEnabledFalse(guna2Panel9);
            }
        }

        private void LoadTabPhanCong()
        {
            SetControlsEnabledFalse(panel4);
            string sql = "SELECT MaPhanCong, NhiemVu FROM PhanCong WHERE MaSuKien = '" + GlobalValue.MaSuKien + "'";
            DBConnect data = new DBConnect();
            DataTable dt = data.getSqlDataAdapter(sql);

            treePhanCong.Nodes.Clear();
            foreach (DataRow r in dt.Rows)
            {
                TreeNode node = new TreeNode(r["NhiemVu"].ToString());
                node.Tag = r["MaPhanCong"].ToString();
                treePhanCong.Nodes.Add(node);
            }


            foreach (TreeNode item in treePhanCong.Nodes)
            {
                sql = "SELECT C.MaChiTietPhanCong, C.NhiemVu FROM ChiTietPhanCong C JOIN ThanhVien T on C.MaThanhVien = T.MaThanhVien WHERE C.MaPhanCong = '" + item.Tag + "'";
                DataTable dt2 = data.getSqlDataAdapter(sql);
                foreach (DataRow r in dt2.Rows)
                {
                    TreeNode node = new TreeNode(r["NhiemVu"].ToString()); //+ " - " + r["HoTen"].ToString() + " - " + r["MaThanhVien"].ToString()
                    node.Tag = r["MaChiTietPhanCong"].ToString();
                    item.Nodes.Add(node);
                }
            }

            treePhanCong.ExpandAll();

            treePhanCong.SelectedNode = null;

            dateNgayHT_CTPC.Checked = false;
            dateNgayHT_PC.Checked = false;

            sql = "SELECT * FROM Ban";
            dt = data.getSqlDataAdapter(sql);

            cbBan_PC.DataSource = dt;
            cbBan_PC.DisplayMember = "TenBan";
            cbBan_PC.ValueMember = "MaBan";

            cbBan_PC.SelectedIndex = 0;


            if (GlobalValue.TrangThai_SuKien == "Ngừng hoạt động")
            {
                isEditPhanCong = true;
                SetControlsEnabledFalse(panel2);

                SetControlsEnabledFalse(panel4);
            }

        }

        private void HideTabPage(TabControl tabControl, TabPage tabPage)
        {
            tabControl.TabPages.Remove(tabPage); // Loại bỏ TabPage khỏi TabControl
        }

        private void ShowTabPage(TabControl tabControl, TabPage tabPage)
        {
            tabControl.TabPages.Add(tabPage); // Thêm TabPage vào lại TabControl
        }

        private void FormSuKien_ChiTiet_Load(object sender, EventArgs e)
        {
            LoadTabThongTin();
            LoadTabTaiTro();
            LoadTabChiTieu();
            LoadTabPhanCong();
            
            if (GlobalValue.ChucVu_NguoiDung == "CV004"|| GlobalValue.ChucVu_NguoiDung == "CV005")
            {
                HideTabPage(guna2TabControl1, tabPage2);
                HideTabPage(guna2TabControl1, tabPage3);
            }    

            if (GlobalValue.ChucVu_NguoiDung == "CV003")
            {
                HideTabPage(guna2TabControl1, tabPage4);
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            string ntt = cbTKNhaTT.SelectedValue.ToString();
            string tongtien = txtTKTongTien.Text;

            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(tongtien))
            {
                double tmp;
                if (double.TryParse(tongtien, out tmp))
                {
                    string tmpStart = (tmp / 10).ToString().Replace(',', '.');
                    string tmpEnd = (tmp * 10).ToString().Replace(',', '.');

                    conditions.Add("(TongTaiTro BETWEEN " + tmpStart + " AND " + tmpEnd + ")");
                }
                else
                {
                    MessageBox.Show("Tổng tiền phải là một số hợp lệ.");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(ntt))
            {
                conditions.Add("T.MaNhaTaiTro = '" + ntt + "'");
            }


            string sql = "SELECT N.TenNhaTaiTro as N'Tên nhà tài trợ', " +
                        "T.HangTaiTro as N'Hạng tài trợ', " +
                        "T.HienKim as N'Hiện kim', " +
                        "T.HienVat as N'Hiện vật', " +
                        "T.TongTaiTro as N'Tổng tài trợ', " +
                        "T.NgayTaiTro as N'Ngày tài trợ' " +
                        "FROM TaiTro T JOIN NhaTaiTro N ON T.MaNhaTaiTro = N.MaNhaTaiTro " +
                        "WHERE T.MaSuKien = '" + GlobalValue.MaSuKien + "'";

            if (conditions.Count > 0)
            {
                sql += " AND " + string.Join(" AND ", conditions);
            }

            DBConnect data = new DBConnect();
            dgv.DataSource = data.getSqlDataAdapter(sql);

            // Reset lại các control sau tìm kiếm
            cbTKNhaTT.SelectedIndex = 0;
            txtTKTongTien.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ntt = cbNhaTT.SelectedValue.ToString();
            string hangtt = cbHangTT.SelectedValue.ToString();
            string hienkim = txtHienKim.Text;
            string hienvat = txtHienVat.Text;
            string tongtien = txtTongTien.Text;

            if (string.IsNullOrEmpty(hienkim) || string.IsNullOrEmpty(hienvat) || string.IsNullOrEmpty(tongtien))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            double hienKimValue;
            if (!double.TryParse(hienkim, out hienKimValue) || hienKimValue < 0)
            {
                MessageBox.Show("Hiện kim phải là một số không âm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHienKim.Focus();
                return;
            }

            double tongTienValue;
            if (!double.TryParse(tongtien, out tongTienValue) || tongTienValue < 0)
            {
                MessageBox.Show("Tổng tài trợ phải là một số không âm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTongTien.Focus();
                return;
            }

            if (tongTienValue < hienKimValue)
            {
                MessageBox.Show("Tổng tài trợ phải lớn hơn hoặc bằng hiện kim.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTongTien.Focus();
                return;
            }


            DBConnect data = new DBConnect();
            var check = data.getScalar("SELECT COUNT(*) FROM TaiTro WHERE MaSuKien = '" + GlobalValue.MaSuKien + "' AND MaNhaTaiTro = '" + ntt + "'");
            if (int.Parse(check.ToString()) > 0)
            {
                MessageBox.Show("Nhà tài trợ này đã tồn tại trong sự kiện này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = "INSERT INTO TaiTro(MaSuKien, MaNhaTaiTro, HangTaiTro, HienKim, HienVat, TongTaiTro, NgayTaiTro) " +
                         "VALUES('" + GlobalValue.MaSuKien + "', '" + ntt + "', N'" + hangtt + "', " + hienkim + ", N'" + hienvat + "', " + tongtien + ", GETDATE())";

            if (data.getNonQuery(sql) > 0)
            {
                MessageBox.Show("Thêm nhà tài trợ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllTextBoxes(grThongTin);
                LoadTabTaiTro();
            }
            else
            {
                MessageBox.Show("Thêm nhà tài trợ thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            HuyLienKet_TaiTro();
            btnThem.Enabled = true;
            cbNhaTT.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            LienKet_TaiTro(dgv.DataSource as DataTable);
            btnThem.Enabled = false;
            cbNhaTT.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ntt = cbNhaTT.SelectedValue.ToString();
            string hangtt = cbHangTT.SelectedValue.ToString();
            string hienkim = txtHienKim.Text;
            string hienvat = txtHienVat.Text;
            string tongtien = txtTongTien.Text;

            // Kiểm tra tính hợp lệ của dữ liệu nhập
            if (string.IsNullOrEmpty(hienkim) || string.IsNullOrEmpty(hienvat) || string.IsNullOrEmpty(tongtien))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double hienKimValue;
            if (!double.TryParse(hienkim, out hienKimValue) || hienKimValue < 0)
            {
                MessageBox.Show("Hiện kim phải là một số không âm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHienKim.Focus();
                return;
            }

            double tongTienValue;
            if (!double.TryParse(tongtien, out tongTienValue) || tongTienValue < 0)
            {
                MessageBox.Show("Tổng tài trợ phải là một số không âm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTongTien.Focus();
                return;
            }

            if (tongTienValue < hienKimValue)
            {
                MessageBox.Show("Tổng tài trợ phải lớn hơn hoặc bằng hiện kim.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTongTien.Focus();
                return;
            }

            // Kiểm tra nếu nhà tài trợ chưa tồn tại
            DBConnect data = new DBConnect();
            var check = data.getScalar("SELECT COUNT(*) FROM TaiTro WHERE MaSuKien = '" + GlobalValue.MaSuKien + "' AND MaNhaTaiTro = '" + ntt + "'");
            if (int.Parse(check.ToString()) == 0)
            {
                MessageBox.Show("Nhà tài trợ này không tồn tại trong sự kiện này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thực hiện câu lệnh UPDATE
            string sql = "UPDATE TaiTro " +
                         "SET HangTaiTro = N'" + hangtt + "', " +
                         "HienKim = " + hienkim + ", " +
                         "HienVat = N'" + hienvat + "', " +
                         "TongTaiTro = " + tongtien + ", " +
                         "NgayTaiTro = GETDATE() " +
                         "WHERE MaSuKien = '" + GlobalValue.MaSuKien + "' AND MaNhaTaiTro = '" + ntt + "'";

            if (data.getNonQuery(sql) > 0)
            {
                MessageBox.Show("Cập nhật thông tin nhà tài trợ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllTextBoxes(grThongTin);
                cbNhaTT.Enabled = true;
                btnThem.Enabled = true;
                HuyLienKet_TaiTro();
                LoadTabTaiTro();
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin nhà tài trợ thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {


            if (cbNhaTT.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng click sửa và chọn vào nhà tài trợ muốn xoá trên bảng.");
                return;
            }

            LienKet_TaiTro(dgv.DataSource as DataTable);
            string ntt = cbNhaTT.SelectedValue.ToString();

            string sql = "DELETE FROM TaiTro WHERE MaNhaTaiTro = '" + ntt + "' AND MaSuKien = '" + GlobalValue.MaSuKien + "'";
            DBConnect dBConnect = new DBConnect();
            if (dBConnect.getNonQuery(sql) > 0)
            {
                MessageBox.Show("Xóa nhà tài trợ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllTextBoxes(grThongTin);
                LoadTabTaiTro();
            }
            else
            {
                MessageBox.Show("Xóa nhà tài trợ thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            HuyLienKet_TaiTro();
        }

        private void btnTK_CT_Click(object sender, EventArgs e)
        {
            string hinhttt = cbTK_HTTT_CT.Text;
            string sotien = txtTK_SoTien_CT.Text;

            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(hinhttt))
            {
                conditions.Add("HinhThucThanhToan = N'" + hinhttt + "'");
            }

            double tmp;
            if (!string.IsNullOrEmpty(sotien))
            {
                if (double.TryParse(sotien, out tmp))
                {
                    string tmpStart = (tmp / 10).ToString().Replace(',', '.');
                    string tmpEnd = (tmp * 10).ToString().Replace(',', '.');
                    conditions.Add("(DuChi BETWEEN " + tmpStart + " AND " + tmpEnd + ")");
                }
                else
                {
                    MessageBox.Show("Số tiền phải là một số hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string sql = "SELECT MaChiTieu as N'Mã chi tiêu', LyDoChiTieu as N'Lý do chi tiêu', HinhThucThanhToan as N'Hình thức thanh toán', DuChi as N'Dự chi', ThucChi as N'Thực chi', AnhHoaDon as N'Ảnh hoá đơn' ,NgayThucHien as N'Ngày thực hiện' FROM ChiTieu WHERE MaSuKien = '" + GlobalValue.MaSuKien + "'";

            if (conditions.Count > 0)
            {
                sql += " AND " + string.Join(" AND ", conditions);
            }

            DBConnect data = new DBConnect();
            dgvCT.DataSource = data.getSqlDataAdapter(sql);


            cbTK_HTTT_CT.SelectedIndex = 0;
            txtTK_SoTien_CT.Text = "";
        }

        private void btnThem_CT_Click(object sender, EventArgs e)
        {
            string lydo = txtLyDo_CT.Text;
            string hinhttt = cbHTTT_CT.Text;
            string duchi = txtDuChi_CT.Text;
            string thucchi = txtThucChi_CT.Text;
            string ngaythuchien = dateNgayTT_CT.Checked ? dateNgayTT_CT.Value.ToString("yyyy-MM-dd") : null;
            string anhhd = null;  // Khởi tạo là null, nếu có ảnh thì mới thay đổi

            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(lydo) || string.IsNullOrEmpty(duchi))
            {
                MessageBox.Show("Lý do chi tiêu và dự chi là bắt buộc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Kiểm tra giá trị của Dự chi (phải là số hợp lệ)
            double duChiValue;
            if (!double.TryParse(duchi, out duChiValue))
            {
                MessageBox.Show("Dự chi phải là số hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra giá trị thực chi (nếu có, phải là số hợp lệ)
            double thucChiValue = 0;
            if (!string.IsNullOrEmpty(thucchi) && !double.TryParse(thucchi, out thucChiValue))
            {
                MessageBox.Show("Thực chi phải là số hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thư mục lưu ảnh (đảm bảo thư mục tồn tại)
            string projectDirectory = Directory.GetParent(Application.StartupPath).Parent.FullName;
            string imageDirectory = Path.Combine(projectDirectory, "HinhAnh");

            // Nếu có ảnh, lưu ảnh vào thư mục và lưu tên ảnh vào biến anhhd
            if (picHoaDon_CT.Image != null)
            {
                // Tạo đường dẫn đến thư mục lưu ảnh
                string filePath = Path.Combine(imageDirectory, fileNameHoaDon);

                // Lưu ảnh vào thư mục
                picHoaDon_CT.Image.Save(filePath);

                // Lưu tên ảnh vào biến anhhd để sử dụng khi insert vào cơ sở dữ liệu
                anhhd = fileNameHoaDon;
            }


            DBConnect data = new DBConnect();
            var ln = data.getScalar("SELECT MAX(MaChiTieu) AS MA FROM ChiTieu");
            string ma;

            if (ln != null && ln != DBNull.Value)
            {
                string lastCode = ln.ToString();
                int lastNumber = int.Parse(lastCode.Substring(2));
                ma = "CT" + (lastNumber + 1).ToString("D3");
            }
            else
            {
                ma = "CT001";
            }

            try
            {
                string sql = "INSERT INTO ChiTieu (MaChiTieu, MaSuKien, LyDoChiTieu, HinhThucThanhToan, DuChi, ThucChi, AnhHoaDon, NgayThucHien) " +
                             "VALUES ('" + ma + "', '" + GlobalValue.MaSuKien + "', N'" + lydo + "', N'" + hinhttt + "', " + duChiValue + ", " +
                             (string.IsNullOrEmpty(thucchi) ? "NULL" : thucChiValue.ToString()) + ", " +
                             (string.IsNullOrEmpty(anhhd) ? "NULL" : "'" + anhhd + "'") + ", '" + ngaythuchien + "')";

                int rowsAffected = data.getNonQuery(sql);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm chi tiêu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAllTextBoxes(grThongTin_ChiTieu);
                    picHoaDon_CT.Image = null;
                    dateNgayTT_CT.Checked = false;
                    LoadTabChiTieu();
                }
                else
                {
                    MessageBox.Show("Thêm chi tiêu thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm chi tiêu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnUpload_CT_Click(object sender, EventArgs e)
        {
            // Tạo một đối tượng OpenFileDialog để chọn file
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Chỉ cho phép chọn file ảnh
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff";

            // Hiển thị hộp thoại mở file và kiểm tra người dùng có chọn file không
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Lấy đường dẫn file được chọn
                    string filePath = openFileDialog.FileName;

                    // Tải ảnh từ file vào PictureBox
                    picHoaDon_CT.Image = Image.FromFile(filePath);

                    fileNameHoaDon = Path.GetFileName(filePath);

                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có khi mở file
                    MessageBox.Show("Không thể mở tệp hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_CT_Click(object sender, EventArgs e)
        {
            LienKet_ChiTieu(dgvCT.DataSource as DataTable);
            btnThem_CT.Enabled = false;

        }

        private void btnLua_CT_Click(object sender, EventArgs e)
        {
            string maChiTieu = txtMa_CT.Text;
            string lydo = txtLyDo_CT.Text;
            string hinhttt = cbHTTT_CT.Text;
            string duchi = txtDuChi_CT.Text;
            string thucchi = txtThucChi_CT.Text;
            string anhhd = txtfileName.Text;
            string ngaythuchien = dateNgayTT_CT.Checked ? dateNgayTT_CT.Value.ToString("yyyy-MM-dd") : null;

            if (string.IsNullOrEmpty(maChiTieu))
            {
                MessageBox.Show("Mã chi tiêu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(lydo) || string.IsNullOrEmpty(duchi))
            {
                MessageBox.Show("Lý do chi tiêu và dự chi là bắt buộc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double duChiValue;
            if (!double.TryParse(duchi, out duChiValue))
            {
                MessageBox.Show("Dự chi phải là số hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double thucChiValue = 0;
            if (!string.IsNullOrEmpty(thucchi) && !double.TryParse(thucchi, out thucChiValue))
            {
                MessageBox.Show("Thực chi phải là số hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string projectDirectory = Directory.GetParent(Application.StartupPath).Parent.FullName;
            string imageDirectory = Path.Combine(projectDirectory, "HinhAnh");

            if (txtfileName.Text != fileNameHoaDon)
            {
                try
                {
                    // Đường dẫn lưu ảnh mới
                    string filePath = Path.Combine(imageDirectory, fileNameHoaDon);
                    picHoaDon_CT.Image.Save(filePath);
                    anhhd = fileNameHoaDon; // Cập nhật tên ảnh
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                string sql = "UPDATE ChiTieu SET " +
                             "LyDoChiTieu = N'" + lydo + "', " +
                             "HinhThucThanhToan = N'" + hinhttt + "', " +
                             "DuChi = " + duChiValue + ", " +
                             "ThucChi = " + (string.IsNullOrEmpty(thucchi) ? "NULL" : thucChiValue.ToString()) + ", " +
                             "AnhHoaDon = " + (string.IsNullOrEmpty(anhhd) ? "NULL" : "'" + anhhd + "'") + ", " +
                             "NgayThucHien = " + (string.IsNullOrEmpty(ngaythuchien) ? "NULL" : "'" + ngaythuchien + "'") + " " +
                             "WHERE MaChiTieu = '" + maChiTieu + "'";

                DBConnect data = new DBConnect();
                int rowsAffected = data.getNonQuery(sql);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật chi tiêu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAllTextBoxes(grThongTin_ChiTieu);
                    picHoaDon_CT.Image = null;
                    HuyLienKet_ChiTieu();
                    btnThem_CT.Enabled = true;
                    cbHTTT_CT.SelectedIndex = -1;
                    dateNgayTT_CT.Checked = false;
                    LoadTabChiTieu();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã chi tiêu cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa chi tiêu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_CT_Click(object sender, EventArgs e)
        {
            HuyLienKet_ChiTieu();
            ClearAllTextBoxes(grThongTin_ChiTieu);
            btnThem_CT.Enabled = true;
            cbHTTT_CT.SelectedIndex = -1;
            dateNgayTT_CT.Checked = false;
        }

        private void btnXoa_CT_Click(object sender, EventArgs e)
        {
            if (txtMa_CT.Text == "")
            {
                MessageBox.Show("Vui lòng chọn vào nút sửa và chọn vào chi tiêu muốn xoá trên bảng.");
                return;
            }
            txtMa_CT.DataBindings.Clear();
            txtMa_CT.DataBindings.Add("Text", dgvCT.DataSource as DataTable, "Mã chi tiêu");
            string maChiTieu = txtMa_CT.Text;
            string sql = "DELETE FROM ChiTieu WHERE MaChiTieu = '" + maChiTieu + "'";
            DBConnect data = new DBConnect();
            int rowsAffected = data.getNonQuery(sql);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Xóa chi tiêu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllTextBoxes(grThongTin_ChiTieu);
                picHoaDon_CT.Image = null;
                HuyLienKet_ChiTieu();
                LoadTabChiTieu();
            }
            else
            {
                MessageBox.Show("Không tìm thấy mã chi tiêu cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }


        private void txtMoTa_CTPC_TextChanged(object sender, EventArgs e)
        {

        }

        private string Ban_NguoiDung()
        {
            string ban = "SELECT TOP 1 MaBan FROM DamNhiem WHERE MaNguoiDung = '" + GlobalValue.Ma_NguoiDung + "' ORDER BY MaBan ASC";
            DBConnect data = new DBConnect();
 
            return data.getScalar(ban).ToString();
        }

        private void btnThem_PC_Click(object sender, EventArgs e)
        {
            if (GlobalValue.ChucVu_NguoiDung == "CV003" || GlobalValue.ChucVu_NguoiDung == "CV004")
            {
                MessageBox.Show("Bạn không có quyền thêm phân công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    

            string maban = cbBan_PC.SelectedValue.ToString();
            string nhiemvu = txtNhiemVu_PC.Text;
            string ngayht = dateNgayHT_PC.Checked ? dateNgayHT_PC.Value.ToString("yyyy-MM-dd") : null;
            string mota = txtMoTa_PC.Text;

            if (string.IsNullOrEmpty(maban))
            {
                MessageBox.Show("Vui lòng chọn ban phụ trách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(nhiemvu))
            {
                MessageBox.Show("Nhiệm vụ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!dateNgayHT_PC.Checked)
            {
                MessageBox.Show("Ngày hoàn thành không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            DBConnect data = new DBConnect();
            var ln = data.getScalar("SELECT MAX(MaPhanCong) AS MA FROM PhanCong");
            string ma;

            if (ln != null && ln != DBNull.Value)
            {
                string lastCode = ln.ToString();
                int lastNumber = int.Parse(lastCode.Substring(2));
                ma = "PC" + (lastNumber + 1).ToString("D3");
            }
            else
            {
                ma = "PC001";
            }

            try
            {
                string sql = "INSERT INTO PhanCong (MaPhanCong, MaSuKien, MaBan, NhiemVu, NgayHoanThanh, MoTa, NgayPhanCong) " +
                             "VALUES ('" + ma + "', '" + GlobalValue.MaSuKien + "', '" + maban + "', N'" + nhiemvu + "', " +
                             (string.IsNullOrEmpty(ngayht) ? "NULL" : "'" + ngayht + "'") + ", N'" + mota + "', GETDATE())";
                int rowsAffected = data.getNonQuery(sql);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm phân công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAllTextBoxes(grPhanCong);
                    treePhanCong.SelectedNode = null;
                    LoadTabPhanCong();
                }
                else
                {
                    MessageBox.Show("Thêm phân công thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phân công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSua_PC_Click(object sender, EventArgs e)
        {
            isEditPhanCong = true;

            MessageBox.Show("Click vào kế hoạch trên tree view để chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            treePhanCong.SelectedNode = null;

            string sql = "SELECT * FROM ThanhVien";
            DBConnect data = new DBConnect();
            DataTable dt1 = data.getSqlDataAdapter(sql);

            dt1.Columns.Add("DisplayMember", typeof(string));
            foreach (DataRow row in dt1.Rows)
            {
                row["DisplayMember"] = row["MaThanhVien"].ToString() + " - " + row["HoTen"].ToString();
            }

            cbThanhVien_CTPC.DataSource = dt1;
            cbThanhVien_CTPC.DisplayMember = "DisplayMember";
            cbThanhVien_CTPC.ValueMember = "MaThanhVien";

        }

        private void LienKet_PhanCong(DataTable dt)
        {
            cbBan_PC.DataBindings.Clear();
            txtNhiemVu_PC.DataBindings.Clear();
            dateNgayHT_PC.DataBindings.Clear();
            txtMoTa_PC.DataBindings.Clear();
            txtMa_PC.DataBindings.Clear();

            txtMa_PC.DataBindings.Add("Text", dt, "MaPhanCong");
            cbBan_PC.DataBindings.Add("SelectedValue", dt, "MaBan");
            txtNhiemVu_PC.DataBindings.Add("Text", dt, "NhiemVu");
            dateNgayHT_PC.DataBindings.Add("Value", dt, "NgayHoanThanh");
            dateNgayHT_PC.Checked = true;
            txtMoTa_PC.DataBindings.Add("Text", dt, "MoTa");
        }

        private void HuyLienKet_PhanCong()
        {
            txtMa_PC.DataBindings.Clear();
            cbBan_PC.DataBindings.Clear();
            txtNhiemVu_PC.DataBindings.Clear();
            dateNgayHT_PC.DataBindings.Clear();
            txtMoTa_PC.DataBindings.Clear();
        }

        private void LienKet_CTPhanCong(DataTable dt)
        {
            txtMa_CTPC.DataBindings.Clear();
            cbThanhVien_CTPC.DataBindings.Clear();
            txtNhiemVu_CTPC.DataBindings.Clear();
            dateNgayHT_CTPC.DataBindings.Clear();
            txtMoTa_CTPC.DataBindings.Clear();
            txtDanhGia_CTPC.DataBindings.Clear();

            txtMa_CTPC.DataBindings.Add("Text", dt, "MaChiTietPhanCong");
            cbThanhVien_CTPC.DataBindings.Add("SelectedValue", dt, "MaThanhVien");
            txtNhiemVu_CTPC.DataBindings.Add("Text", dt, "NhiemVu");
            dateNgayHT_CTPC.DataBindings.Add("Value", dt, "NgayHoanThanh");
            dateNgayHT_CTPC.Checked = true;
            txtMoTa_CTPC.DataBindings.Add("Text", dt, "MoTa");
            txtDanhGia_CTPC.DataBindings.Add("Text", dt, "DanhGia");

        }

        private void HuyLienKet_CTPhanCong()
        {
            txtMa_CTPC.DataBindings.Clear();
            cbThanhVien_CTPC.DataBindings.Clear();
            txtNhiemVu_CTPC.DataBindings.Clear();
            dateNgayHT_CTPC.DataBindings.Clear();
            dateNgayHT_CTPC.Checked = false;
            dateNgayHT_CTPC.Value = DateTime.Now;
            txtMoTa_CTPC.DataBindings.Clear();
            txtDanhGia_CTPC.DataBindings.Clear();
        }

        private void treePhanCong_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!isEditPhanCong)
            {
                return;
            }

            string ma = treePhanCong.SelectedNode.Tag.ToString();

            if (GlobalValue.TrangThai_SuKien == "Ngừng hoạt động")
            {
                if (ma.StartsWith("PC"))
                {
                    string sql = "SELECT * FROM PhanCong WHERE MaPhanCong = '" + treePhanCong.SelectedNode.Tag + "'";
                    DBConnect data = new DBConnect();
                    DataTable dt = data.getSqlDataAdapter(sql);
                    LienKet_PhanCong(dt);
                }    
                else
                {
                    string sql = "SELECT * FROM ThanhVien";
                    DBConnect data = new DBConnect();
                    DataTable dt1 = data.getSqlDataAdapter(sql);

                    dt1.Columns.Add("DisplayMember", typeof(string));
                    foreach (DataRow row in dt1.Rows)
                    {
                        row["DisplayMember"] = row["MaThanhVien"].ToString() + " - " + row["HoTen"].ToString();
                    }

                    cbThanhVien_CTPC.DataSource = dt1;
                    cbThanhVien_CTPC.DisplayMember = "DisplayMember";
                    cbThanhVien_CTPC.ValueMember = "MaThanhVien";

                    sql = "SELECT * FROM ChiTietPhanCong WHERE MaChiTietPhanCong = '" + treePhanCong.SelectedNode.Tag + "'";
                    
                    DataTable dt = data.getSqlDataAdapter(sql);
                    LienKet_CTPhanCong(dt);
                }
                return;
            }    


            if (ma.StartsWith("PC"))
            {
                SetControlsEnabledTrue(panel2);
                SetControlsEnabledTrue(panel4);
                string sql = "SELECT * FROM PhanCong WHERE MaPhanCong = '" + treePhanCong.SelectedNode.Tag + "'";
                DBConnect data = new DBConnect();
                DataTable dt = data.getSqlDataAdapter(sql);

                LienKet_PhanCong(dt);
                ClearAllTextBoxes(grCTPC);
                btnThem_CTPC.Enabled = true;
                btnHuy_CTPC.Enabled = true;
                dateNgayHT_CTPC.Checked = false;
                dateNgayHT_CTPC.Value = DateTime.Now;

            }
            else
            {
                SetControlsEnabledFalse(panel2);
                SetControlsEnabledTrue(panel4);
                string sql = "SELECT * FROM ChiTietPhanCong WHERE MaChiTietPhanCong = '" + treePhanCong.SelectedNode.Tag + "'";
                DBConnect data = new DBConnect();
                DataTable dt = data.getSqlDataAdapter(sql);

                LienKet_CTPhanCong(dt);

                string mapc = dt.Rows[0]["MaPhanCong"].ToString();

                sql = "SELECT * FROM PhanCong WHERE MaPhanCong = '" + mapc + "'";
                dt = data.getSqlDataAdapter(sql);


                LienKet_PhanCong(dt);

                SetControlsEnabledFalse(panel2);

                btnXoa_CTPC.Enabled = true;
                btnLuu_CTPC.Enabled = true;
                btnThem_CTPC.Enabled = false;

                

            }

            if (GlobalValue.ChucVu_NguoiDung == "CV004" || GlobalValue.ChucVu_NguoiDung == "CV005")
            {
                SetControlsEnabledFalse(panel2);
            }    

            if (Ban_NguoiDung() != cbBan_PC.SelectedValue.ToString() && (GlobalValue.ChucVu_NguoiDung == "CV004" || GlobalValue.ChucVu_NguoiDung == "CV005"))
            {
                SetControlsEnabledFalse(panel4);
                SetControlsEnabledFalse(panel2);
                return;
            }

        }

        private void btnLuu_PC_Click(object sender, EventArgs e)
        {

            if (GlobalValue.ChucVu_NguoiDung == "CV003" || GlobalValue.ChucVu_NguoiDung == "CV004")
            {
                MessageBox.Show("Bạn không có quyền sửa phân công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMa_PC.Text == "")
            {
                MessageBox.Show("Vui lòng chọn phân công cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maban = cbBan_PC.SelectedValue.ToString();
            string nhiemvu = txtNhiemVu_PC.Text;
            string ngayht = dateNgayHT_PC.Checked ? dateNgayHT_PC.Value.ToString("yyyy-MM-dd") : null;
            string mota = txtMoTa_PC.Text;
            string mapc = txtMa_PC.Text;

            if (string.IsNullOrEmpty(maban))
            {
                MessageBox.Show("Vui lòng chọn ban phụ trách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(nhiemvu))
            {
                MessageBox.Show("Nhiệm vụ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!dateNgayHT_PC.Checked)
            {
                MessageBox.Show("Ngày hoàn thành không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DBConnect data = new DBConnect();

            try
            {
                string sql = "UPDATE PhanCong SET " +
                             "MaBan = '" + maban + "', " +
                             "NhiemVu = N'" + nhiemvu + "', " +
                             "NgayHoanThanh = " + (string.IsNullOrEmpty(ngayht) ? "NULL" : "'" + ngayht + "'") + ", " +
                             "MoTa = N'" + mota + "' " +
                             "WHERE MaPhanCong = '" + mapc + "'";
                int rowsAffected = data.getNonQuery(sql);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật phân công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAllTextBoxes(grPhanCong);
                    LoadTabPhanCong();
                }
                else
                {
                    MessageBox.Show("Cập nhật phân công thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật phân công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_PC_Click(object sender, EventArgs e)
        {
            isEditPhanCong = false;
            ClearAllTextBoxes(grPhanCong);
            ClearAllTextBoxes(grCTPC);
            HuyLienKet_PhanCong();
            HuyLienKet_CTPhanCong();
            cbBan_PC.SelectedIndex = 0;
            dateNgayHT_PC.Checked = false;
            dateNgayHT_PC.Value = DateTime.Now;
            LoadTabPhanCong();
        }

        private void btnXoa_PC_Click(object sender, EventArgs e)
        {
            if (GlobalValue.ChucVu_NguoiDung == "CV003" || GlobalValue.ChucVu_NguoiDung == "CV004")
            {
                MessageBox.Show("Bạn không có quyền sửa phân công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMa_PC.Text == "")
            {
                MessageBox.Show("Vui lòng chọn phân công cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa phân công này? Tất cả các chi tiết phân công liên quan cũng sẽ bị xóa.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            string mapc = txtMa_PC.Text;

            string sql = "DELETE FROM ChiTietPhanCong WHERE MaPhanCong = '" + mapc + "'" +
                         "DELETE FROM PhanCong WHERE MaPhanCong = '" + mapc + "';";

            DBConnect data = new DBConnect();
            int rowsAffected = data.getNonQuery(sql);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Xóa phân công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllTextBoxes(grPhanCong);
                LoadTabPhanCong();
            }
            else
            {
                MessageBox.Show("Xóa phân công thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_CTPC_Click(object sender, EventArgs e)
        {
            if (Ban_NguoiDung() != cbBan_PC.SelectedValue.ToString() && (GlobalValue.ChucVu_NguoiDung == "CV004" || GlobalValue.ChucVu_NguoiDung == "CV005"))
            {
                MessageBox.Show("Bạn không có quyền thêm chi tiết phân công cho phân công không thuộc về ban của mình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    

            string mapc = txtMa_PC.Text;
            string matv = cbThanhVien_CTPC.SelectedValue.ToString();
            string nhiemvu = txtNhiemVu_CTPC.Text;
            string ngayht = dateNgayHT_CTPC.Checked ? dateNgayHT_CTPC.Value.ToString("yyyy-MM-dd") : null;
            string mota = txtMoTa_CTPC.Text;
            string danhgia = txtDanhGia_CTPC.Text;

            if (string.IsNullOrEmpty(mapc))
            {
                MessageBox.Show("Vui lòng chọn phân công cần thêm chi tiết", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(matv))
            {
                MessageBox.Show("Vui lòng chọn thành viên thực hiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(nhiemvu))
            {
                MessageBox.Show("Nhiệm vụ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!dateNgayHT_CTPC.Checked)
            {
                MessageBox.Show("Ngày hoàn thành không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DBConnect data = new DBConnect();
            var ln = data.getScalar("SELECT MAX(MaChiTietPhanCong) AS MA FROM ChiTietPhanCong");
            string ma;

            if (ln != null && ln != DBNull.Value)
            {
                string lastCode = ln.ToString();
                int lastNumber = int.Parse(lastCode.Substring(4));
                ma = "CTPC" + (lastNumber + 1).ToString("D3");
            }
            else
            {
                ma = "CTPC001";
            }

            try
            {
                string sql = "INSERT INTO ChiTietPhanCong (MaChiTietPhanCong, MaPhanCong, MaThanhVien, NhiemVu, NgayHoanThanh, MoTa, DanhGia, NguoiTao, NgayTao) " +
                             "VALUES ('" + ma + "', '" + mapc + "', '" + matv + "', N'" + nhiemvu + "', " +
                             (string.IsNullOrEmpty(ngayht) ? "NULL" : "'" + ngayht + "'") + ", N'" + mota + "', N'" + danhgia + "', '" + GlobalValue.Ma_NguoiDung + "', GETDATE())";
                int rowsAffected = data.getNonQuery(sql);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm chi tiết phân công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAllTextBoxes(grCTPC);
                    LoadTabPhanCong();
                }
                else
                {
                    MessageBox.Show("Thêm chi tiết phân công thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm chi tiết phân công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnXoa_CTPC_Click(object sender, EventArgs e)
        {
            if (Ban_NguoiDung() != cbBan_PC.SelectedValue.ToString())
            {
                MessageBox.Show("Bạn không có quyền xoá chi tiết phân công cho phân công không thuộc về ban của mình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ma = txtMa_CTPC.Text;
            string sql = "DELETE FROM ChiTietPhanCong WHERE MaChiTietPhanCong = '" + ma + "'";
            DBConnect data = new DBConnect();
            int rowsAffected = data.getNonQuery(sql);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Xóa chi tiết phân công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllTextBoxes(grCTPC);
                LoadTabPhanCong();
            }
            else
            {
                MessageBox.Show("Xóa chi tiết phân công thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_CTPC_Click(object sender, EventArgs e)
        {
            if (Ban_NguoiDung() != cbBan_PC.SelectedValue.ToString() && (GlobalValue.ChucVu_NguoiDung == "CV004" || GlobalValue.ChucVu_NguoiDung == "CV005"))
            {
                MessageBox.Show("Bạn không có quyền sửa chi tiết phân công cho phân công không thuộc về ban của mình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ma = txtMa_CTPC.Text;
            string matv = cbThanhVien_CTPC.SelectedValue.ToString();
            string nhiemvu = txtNhiemVu_CTPC.Text;
            string ngayht = dateNgayHT_CTPC.Checked ? dateNgayHT_CTPC.Value.ToString("yyyy-MM-dd") : null;
            string mota = txtMoTa_CTPC.Text;
            string danhgia = txtDanhGia_CTPC.Text;

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn chi tiết phân công cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(matv))
            {
                MessageBox.Show("Vui lòng chọn thành viên thực hiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            if (string.IsNullOrEmpty(nhiemvu))
            {
                MessageBox.Show("Nhiệm vụ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!dateNgayHT_CTPC.Checked)
            {
                MessageBox.Show("Ngày hoàn thành không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DBConnect data = new DBConnect();

            string sql = "UPDATE ChiTietPhanCong SET " +
                         "MaThanhVien = '" + matv + "', " +
                         "NhiemVu = N'" + nhiemvu + "', " +
                         "NgayHoanThanh = " + (string.IsNullOrEmpty(ngayht) ? "NULL" : "'" + ngayht + "'") + ", " +
                         "MoTa = N'" + mota + "', " +
                         "DanhGia = N'" + danhgia + "' " +
                         "WHERE MaChiTietPhanCong = '" + ma + "'";
            int rowsAffected = data.getNonQuery(sql);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Cập nhật chi tiết phân công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllTextBoxes(grCTPC);
                cbThanhVien_CTPC.SelectedIndex = 0;
                dateNgayHT_CTPC.Checked = false;
                btnThem_CTPC.Enabled = true;
            }
            else
            {
                MessageBox.Show("Cập nhật chi tiết phân công thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnHuy_CTPC_Click(object sender, EventArgs e)
        {

            ClearAllTextBoxes(grCTPC);
            ClearAllTextBoxes(grPhanCong);
            cbThanhVien_CTPC.SelectedIndex = 0;
            dateNgayHT_CTPC.Checked = false;
            btnThem_CTPC.Enabled = true;
            SetControlsEnabledFalse(panel4);
            SetControlsEnabledTrue(panel2);
            isEditPhanCong = false;
            LoadTabPhanCong();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picHoaDon_CT_Click(object sender, EventArgs e)
        {
            Image imageToShow = picHoaDon_CT.Image;

            if (imageToShow == null)
            {
                MessageBox.Show("Không có ảnh nào để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            else
            {
                FormXemAnh formXemAnh = new FormXemAnh(imageToShow);
                formXemAnh.ShowDialog();
            }    

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}
