using QLHD_CLB.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLHD_CLB
{
    public partial class FormThanhVien : Form
    {
        public FormThanhVien()
        {
            InitializeComponent();
        }
        
        DBConnect db = new DBConnect();

        private bool ValidateInputsThemThanhVien()
        {
            if (string.IsNullOrEmpty(inputHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputHoTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(inputEmail.Text))
            {
                MessageBox.Show("Email không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputEmail.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(inputSoDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputSoDienThoai.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(inputDiaChi.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputDiaChi.Focus();
                return false;
            }

            if (!Regex.IsMatch(inputEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Email không đúng định dạng!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputEmail.Focus();
                return false;
            }

            // Kiểm tra họ tên không chứa chữ số
            string regexHoten = @"\d"; // Kiểm tra chữ số
            if (Regex.IsMatch(inputHoTen.Text, regexHoten))
            {
                MessageBox.Show("Họ tên không được chứa chữ số!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputHoTen.Focus();
                return false;
            }

            if (!Regex.IsMatch(inputSoDienThoai.Text, @"^[0-9]{10,11}$"))
            {
                MessageBox.Show("Số điện thoại không chứa kí tự chữ và độ dài 10-11 ký tự!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputSoDienThoai.Focus();
                return false;
            }

            return true;
        }
        private void HienThi_DSThanhVien()
        {
            string sqlquery = "Select MaThanhVien as N'Mã thành viên', HoTen as N'Họ tên', GioiTinh as N'Giới tính', SoDienThoai as N'Số điện thoại', Email as N'Email', DiaChi as N'Địa chỉ', NgayThamGia as N'Ngày tham gia', TrangThai as N'Trạng thái', Ban.TenBan as N'Thuộc ban' from ThanhVien, Ban Where ThanhVien.MaBan = Ban.MaBan";
            DataTable dt = db.getSqlDataAdapter(sqlquery);

            dt.PrimaryKey = new DataColumn[] { dt.Columns["MaThanhVien"] };
            dtg_DSTV.DataSource = dt;
            dtg_DSTV.Columns["Mã thành viên"].Width = 120;  // Đặt chiều rộng 100
            dtg_DSTV.Columns["Họ tên"].Width = 180;         // Đặt chiều rộng 200
            dtg_DSTV.Columns["Giới tính"].Width = 80;       // Đặt chiều rộng 80
            dtg_DSTV.Columns["Số điện thoại"].Width = 120;  // Đặt chiều rộng 120
            dtg_DSTV.Columns["Email"].Width = 200;          // Đặt chiều rộng 200
            dtg_DSTV.Columns["Địa chỉ"].Width = 140;        // Đặt chiều rộng 200
            dtg_DSTV.Columns["Trạng thái"].Width = 160;     // Đặt chiều rộng 120
            dtg_DSTV.Columns["Thuộc ban"].Width = 140;        // Đặt chiều rộng 150
            dtg_DSTV.Columns["Ngày tham gia"].Width = 120;        // Đặt chiều rộng 200

            dtg_DSTV.Columns[0].ReadOnly = true;
            dtg_DSTV.Columns[1].ReadOnly = true;
            dtg_DSTV.Columns[2].ReadOnly = true;
            dtg_DSTV.Columns[3].ReadOnly = true;
            dtg_DSTV.Columns[4].ReadOnly = true;
            dtg_DSTV.Columns[5].ReadOnly = true;
            dtg_DSTV.Columns[6].ReadOnly = true;
            dtg_DSTV.Columns[7].ReadOnly = true;
            dtg_DSTV.Columns[8].ReadOnly = true;
        }

        private void HienThi_ComboBoxTrangThai()
        {
            comboBox_TrangThai.Items.Clear();
            comboBox_TrangThai.Items.Add("Đang hoạt động");
            comboBox_TrangThai.Items.Add("Ngừng hoạt động");
            comboBox_TrangThai.SelectedIndex = 0; 
        }

        private void HienThi_ComboBoxDSBan()
        {
            string query = "Select * from Ban";
            DataTable dt = db.getSqlDataAdapter(query);

            comboBox_DSBan.DataSource = dt;
            comboBox_DSBan.DisplayMember = "TenBan";
            comboBox_DSBan.ValueMember = "MaBan";
            comboBox_DSBan.SelectedIndex = 0;
        }

        private void HienThi_ComboBoxLocBan()
        {
            string query = "Select * from Ban";
            DataTable dt = db.getSqlDataAdapter(query);

            DataRow allrow = dt.NewRow();
            allrow["MaBan"] = "ALL";
            allrow["TenBan"] = "Tất cả";
            dt.Rows.InsertAt(allrow, 0);

            comboBox_locBan.DataSource = dt;
            comboBox_locBan.DisplayMember = "TenBan";
            comboBox_locBan.ValueMember = "MaBan";
            comboBox_locBan.SelectedIndex = 0;
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

        private void FormThanhVien_Load(object sender, EventArgs e)
        {
            HienThi_ComboBoxDSBan();
            HienThi_ComboBoxLocBan();
            HienThi_ComboBoxTrangThai();
            HienThi_DSThanhVien();

            inputTimKiem.Focus();

            DataTable dt = new DataTable();
            dt.Columns.Add("Tên sự kiện");
            dt.Columns.Add("Nhiệm vụ phân công");
            dt.Columns.Add("Mô tả");
            dt.Columns.Add("Ngày hoàn thành");
            dtg_hdthamgia.DataSource = dt;

            if (!(GlobalValue.ChucVu_NguoiDung == "CV001" || GlobalValue.ChucVu_NguoiDung == "CV002"))
            {
                SetControlsEnabledFalse(guna2GroupBox1);
                SetControlsEnabledFalse(groupBox1);
            }    
        }

        private void ResetForm()
        {
            inputHoTen.Clear();
            inputEmail.Clear();
            inputDiaChi.Clear();
            inputSoDienThoai.Clear();
            comboBox_TrangThai.SelectedIndex = 0;  // Chọn lại mục đầu tiên trong ComboBox
            comboBox_DSBan.SelectedIndex = 0;  // Reset ComboBox DS Ban nếu có
            radioButton_Nam.Checked = true;
            radioButton_Nu.Checked = false;
        }

        private void btn_themThanhVien_Click(object sender, EventArgs e)
        {
            comboBox_locBan.SelectedIndexChanged -= comboBox_locBan_SelectedIndexChanged; // Bật lại sự kiện

            DataTable dt = db.getSqlDataAdapter("Select max(MaThanhVien) from ThanhVien");
            var d = dt.Rows[0][0];
            int max = int.Parse(d.ToString().Substring(2));
            string maTV = "TV" + (max + 1).ToString("D3");
            if (!ValidateInputsThemThanhVien())
            {
                return;
            }
            string gioitinh = "";
            if (radioButton_Nam.Checked)
            {
                gioitinh = radioButton_Nam.Tag.ToString();
            }
            if (radioButton_Nu.Checked)
            {
                gioitinh = radioButton_Nu.Tag.ToString();
            }
            string hoten = inputHoTen.Text;
            string email = inputEmail.Text;
            string diachi = inputDiaChi.Text;
            string sodienthoai = inputSoDienThoai.Text;
            string trangthai = comboBox_TrangThai.SelectedItem.ToString();
            string tenBan = comboBox_DSBan.SelectedValue.ToString();

            string insert = "insert into ThanhVien (MaThanhVien, HoTen, GioiTinh, SoDienThoai, Email, DiaChi, NgayThamGia ,TrangThai, MaBan) Values ('" + maTV + "', N'" + hoten + "', N'" + gioitinh + "', '" + sodienthoai + "', '" + email + "', N'" + diachi + "', GETDATE(), N'" + trangthai + "', '" + comboBox_DSBan.SelectedValue.ToString()  + "')";
            try
            {
                db.getSqlDataAdapter(insert);
                HienThi_DSThanhVien();
                ResetForm();
                MessageBox.Show("Thêm thành viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            comboBox_locBan.SelectedIndexChanged += comboBox_locBan_SelectedIndexChanged; // Bật lại sự kiện

        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string search = inputTimKiem.Text.Trim();
            string query = "SELECT MaThanhVien as N'Mã thành viên', HoTen as N'Họ tên', GioiTinh as N'Giới tính', SoDienThoai as N'Số điện thoại', Email as N'Email', DiaChi as N'Địa chỉ', NgayThamGia as N'Ngày tham gia', TrangThai as N'Trạng thái', Ban.TenBan as N'Thuộc ban' FROM ThanhVien, Ban WHERE ThanhVien.MaBan = Ban.MaBan and HoTen LIKE N'%" + search + "%' OR SoDienThoai LIKE '%" + search + "%' OR DiaChi LIKE N'%" + search + "%'";

            try
            {
                // Lấy dữ liệu và cập nhật DataGridView
                DataTable dt = db.getSqlDataAdapter(query);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    inputTimKiem.Focus();
                }
                else
                {
                    dtg_DSTV.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void inputTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inputTimKiem.Text))
            {
                HienThi_DSThanhVien();
            }
        }

        private void comboBox_locBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_locBan.SelectedValue.ToString() != null)
            {
                string tenBan = comboBox_locBan.SelectedValue.ToString();
                string query = "Select MaThanhVien as N'Mã thành viên', HoTen as N'Họ tên', GioiTinh as N'Giới tính', SoDienThoai as N'Số điện thoại', Email as N'Email', DiaChi as N'Địa chỉ', NgayThamGia as N'Ngày tham gia' ,TrangThai as N'Trạng thái', Ban.TenBan as N'Thuộc ban' FROM ThanhVien, Ban Where ThanhVien.MaBan = Ban.MaBan and Ban.MaBan = '" + comboBox_locBan.SelectedValue.ToString() + "'";
                DataTable dt = db.getSqlDataAdapter(query);
                dtg_DSTV.DataSource = dt;
            }
            if (comboBox_locBan.SelectedValue.ToString() == "ALL")
            {
                HienThi_DSThanhVien();
            }
        }

        bool flag = false;
        private void dtg_DSTV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                inputMaTV.Enabled = false;

                comboBox_locBan.SelectedIndexChanged -= comboBox_locBan_SelectedIndexChanged;
                inputMaTV.Text = dtg_DSTV.Rows[e.RowIndex].Cells["Mã thành viên"].Value.ToString();
                if(flag == true)
                {
                    //
                    inputHoTen.Text = dtg_DSTV.Rows[e.RowIndex].Cells["Họ tên"].Value.ToString();
                    inputEmail.Text = dtg_DSTV.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                    inputSoDienThoai.Text = dtg_DSTV.Rows[e.RowIndex].Cells["Số điện thoại"].Value.ToString();
                    inputDiaChi.Text = dtg_DSTV.Rows[e.RowIndex].Cells["Địa chỉ"].Value.ToString();
                    string gioitinh = dtg_DSTV.Rows[e.RowIndex].Cells["Giới tính"].Value.ToString().Trim();

                    comboBox_TrangThai.Text = dtg_DSTV.Rows[e.RowIndex].Cells["Trạng thái"].Value.ToString();
                    comboBox_DSBan.Text = dtg_DSTV.Rows[e.RowIndex].Cells["Thuộc ban"].Value.ToString();

                    radioButton_Nam.Checked = false;
                    radioButton_Nu.Checked = false;
                    if (gioitinh == "Nam")
                    {
                        radioButton_Nam.Checked = true;
                    }
                    else if (gioitinh == "Nữ")
                    {
                        radioButton_Nu.Checked = true;
                    }
                }


                string queryDSThamGiaHoatDong = "SELECT DISTINCT sk.TenSuKien as N'Tên sự kiện', pc.NhiemVu AS N'Nhiệm vụ phân công', pc.MoTa AS N'Mô tả', ctp.NgayHoanThanh as N'Ngày hoàn thành' FROM ThanhVien tv JOIN ChiTietPhanCong ctp ON tv.MaThanhVien = ctp.MaThanhVien JOIN PhanCong pc ON ctp.MaPhanCong = pc.MaPhanCong JOIN SuKien sk on pc.MaSuKien = sk.MaSuKien WHERE tv.MaThanhVien ='" + inputMaTV.Text + "'";
                DataTable dt = db.getSqlDataAdapter(queryDSThamGiaHoatDong);
                dtg_hdthamgia.DataSource = dt;
                dtg_hdthamgia.Columns[0].ReadOnly = true;
                dtg_hdthamgia.Columns[1].ReadOnly = true;
                dtg_hdthamgia.Columns[2].ReadOnly = true;
                dtg_hdthamgia.Columns[3].ReadOnly = true;
                comboBox_locBan.SelectedIndexChanged += comboBox_locBan_SelectedIndexChanged; // Bật lại sự kiện
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            flag = true;
            btn_themThanhVien.Enabled = false;
            inputMaTV.Enabled = true;
            inputHoTen.Enabled = true;
            inputDiaChi.Enabled = true;
            inputEmail.Enabled = true;
            inputSoDienThoai.Enabled = true;
            comboBox_DSBan.Enabled = true;
            comboBox_TrangThai.Enabled = true;
            radioButton_Nam.Enabled = true;
            radioButton_Nu.Enabled = true;
            btn_luu.Enabled = true;
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string gioitinh = "";
            if (radioButton_Nam.Checked)
            {
                gioitinh = radioButton_Nam.Tag.ToString();
            }
            else if (radioButton_Nu.Checked)
            {
                gioitinh = radioButton_Nu.Tag.ToString();
            }
            try
            {
                string update = "Update ThanhVien set HoTen=N'" + inputHoTen.Text + "', GioiTinh=N'" + gioitinh + "', Email='" + inputEmail.Text + "', SoDienThoai='" + inputSoDienThoai.Text + "', DiaChi=N'" + inputDiaChi.Text + "', TrangThai=N'" + comboBox_TrangThai.Text + "', MaBan='" + comboBox_DSBan.SelectedValue.ToString() + "' where MaThanhVien='" + inputMaTV.Text + "'";
                int kq = db.getNonQuery(update);
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienThi_DSThanhVien();
                btn_luu.Enabled = false;
                btn_themThanhVien.Enabled = true;
                flag = false;
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dtg_DSTV.SelectedRows.Count > 0)
            {
                var selectedRow = dtg_DSTV.SelectedRows[0];
                if (selectedRow.Cells["Mã thành viên"].Value == null || string.IsNullOrWhiteSpace(selectedRow.Cells["Mã thành viên"].Value.ToString()))
                {
                    MessageBox.Show("Dòng này không có dữ liệu hợp lệ để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string maThanhVien = (dtg_DSTV.SelectedRows[0].Cells["Mã thành viên"].Value.ToString());

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa thành viên này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    string query = "DELETE FROM ThanhVien WHERE MaThanhVien = @MaThanhVien";
                    string connectionString = @"Data Source = PHAMTHUAN\MSSQLSERVER01; Initial Catalog = QuanLyCauLacBo; User ID = sa; Password = 123";

                    try
                    {
                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (var transaction = connection.BeginTransaction())
                            {
                                try
                                {
                                    using (var command = new SqlCommand(query, connection, transaction))
                                    {
                                        command.Parameters.AddWithValue("@MaThanhVien", maThanhVien);

                                        // Thực hiện xóa
                                        int rowsAffected = command.ExecuteNonQuery();

                                        if (rowsAffected > 0)
                                        {
                                            // Nếu xóa thành công, commit giao dịch
                                            transaction.Commit();
                                            MessageBox.Show("Xóa thành viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            // Cập nhật lại DataGridView
                                            HienThi_DSThanhVien();
                                        }
                                        else
                                        {
                                            // Nếu không có dòng nào bị xóa, rollback giao dịch
                                            transaction.Rollback();
                                            MessageBox.Show("Không tìm thấy thành viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                }
                                catch (SqlException ex)
                                {
                                    // Nếu có lỗi (chẳng hạn ràng buộc khóa ngoại), rollback giao dịch
                                    transaction.Rollback();
                                    if (ex.Number == 547) // Lỗi FK constraint (số 547 là lỗi khóa ngoại)
                                    {
                                        MessageBox.Show("Không thể xóa thành viên vì có ràng buộc với bảng khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Có lỗi xảy ra khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Nếu không có dòng nào được chọn, thông báo cho người dùng
                MessageBox.Show("Vui lòng chọn một thành viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_lamMoi_Click(object sender, EventArgs e)
        {
            flag = false;
            btn_themThanhVien.Enabled = true;
            inputMaTV.Enabled = true;
            inputHoTen.Enabled = true;
            inputDiaChi.Enabled = true;
            inputEmail.Enabled = true;
            inputSoDienThoai.Enabled = true;
            comboBox_DSBan.Enabled = true;
            comboBox_TrangThai.Enabled = true;
            radioButton_Nam.Enabled = true;
            radioButton_Nu.Enabled = true;
            ResetForm();
        }

    }
}
